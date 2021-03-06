﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCYN.Common.Dapper;
using Dapper;

namespace XCYN.Winform.MeiTuan
{
    public partial class ChangeCity : Form
    {
        public ChangeCity()
        {
            InitializeComponent();
        }
        
        HtmlWeb webClient = new HtmlWeb();
        
        

        public static object sync = new object();

        private void 启动SToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            listBox1.Items.Insert(0,$"{DateTime.Now.ToShortTimeString()} 请求数据中...");
                   
            try
            {
                Task task = new Task(() => {
                    LoadCity();
                    UpdateMeiShi();
                });
                task.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        /// <summary>
        /// 获取所有城市列表
        /// </summary>
        public void LoadCity()
        {
            List<City> list_target = new List<City>();
            HtmlAgilityPack.HtmlDocument doc = null;
            try
            {
                //加载城市列表
                doc = webClient.Load(Global.uri_city);
            }
            catch(Exception ex)
            {
                MyLog.logger.Error(ex.Message);
            }
            //Dom节点获取列表
            HtmlNodeCollection hrefList = doc.DocumentNode.SelectNodes("//div[@id='app']//a[@href]");
            foreach (var item in hrefList)
            {
                var url = item.Attributes["href"];
                listBox1.Invoke(new Action(() =>
                {
                    listBox1.Items.Insert(0, url.Value);
                }));
                var name = item.InnerText;
                list_target.Add(new City() { URL = url.Value.Insert(0, "http:"), Name = name });
            }
            
            int count = 0;
            listBox1.Invoke(new Action(() =>
            {
                listBox1.Items.Insert(0, $"{DateTime.Now.ToShortTimeString()} 导入数据中...");
            }));
            IDbConnection conn = null;
            try
            {
                conn = DapperManager.GetConnection();
                conn.Open();
                var list_source = conn.Query<City>("SELECT Name,URL FROM T_City");
                var list_except = list_target.Except(list_source);
                count = conn.Execute("INSERT INTO T_City(Name,URL) VALUES(@Name,@URL)", list_except);
            }
            catch (Exception ex)
            {
                MyLog.logger.Error(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            listBox1.Invoke(new Action(() =>
            {
                listBox1.Items.Insert(0, $"{DateTime.Now.ToShortTimeString()} 解析完毕，导入{count}条数据...");
            }));
        }
        
        /// <summary>
        /// 更新美食模块的URL
        /// </summary>
        public void UpdateMeiShi()
        {
            HtmlAgilityPack.HtmlDocument doc = null;
            IDbConnection conn = null;
            try
            {
                conn = DapperManager.GetConnection();
                conn.Open();
                var list_source = conn.Query<City>("SELECT ID,Name,URL,MeiShiURL FROM T_City WHERE State = @State", new { State = 1 });
                //一个一个访问URL，并获取美食模块的URL
                for (int i = 0; i < list_source.Count(); i++)
                {
                    if (list_source.ElementAt(i).MeiShiURL != null)
                        continue;
                    try
                    {
                        doc = webClient.Load(list_source.ElementAt(i).URL);
                    }
                    catch (System.Net.WebException webEx)
                    {
                        listBox1.Invoke(new Action(() =>
                        {
                            listBox1.Items.Insert(0, webEx.Message);
                        }));
                        continue;
                    }
                    var hrefList = doc.DocumentNode.SelectNodes("//div[@class='category-nav-content-wrapper']//a[@href]");
                    foreach (var item in hrefList)
                    {
                        if (item.InnerText.Equals("美食"))
                        {
                            var MeiShiURL = item.Attributes["href"].Value;
                            list_source.ElementAt(i).MeiShiURL = MeiShiURL;
                            var count = conn.Execute("UPDATE T_City SET MeiShiURL = @MeiShiURL WHERE ID = @ID", list_source.ElementAt(i));
                            listBox1.Invoke(new Action(() =>
                            {
                                listBox1.Items.Insert(0, $"{DateTime.Now } 更新了{list_source.ElementAt(i).Name}的美食地址");
                            }));
                            break;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MyLog.logger.Error(ex.Message);
            }
            finally
            {
                conn.Close();
            }
           
            //hrefList = doc.DocumentNode.SelectNodes("//div[@id='app']//a[@href]");
        }

        private void 清空LToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}
