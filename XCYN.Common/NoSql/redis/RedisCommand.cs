﻿using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Common.NoSql.redis
{
    
    public class RedisCommand
    {

        #region 通用命令

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Del(string key)
        {
            return RedisManager.WriteDataBase().KeyDelete(key);
        }


        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public long Del(string[] keys)
        {
            var rkeys = new RedisKey[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                rkeys[i] = keys[i];
            }
            return RedisManager.WriteDataBase().KeyDelete(rkeys);
            
        }

        #endregion

        #region String命令

        /// <summary>
        /// 追加一个值到key上
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <remarks>如果 key 已经存在，并且值为字符串，那么这个命令会把 value 追加到原来值（value）的结尾。 如果 key 不存在，那么它将首先创建一个空字符串的key，再执行追加操作，这种情况 APPEND 将类似于 SET 操作。</remarks>
        /// <returns>返回append后字符串值（value）的长度。</returns>
        public long StringAppend(string key,string value)
        {
            return RedisManager.WriteDataBase().StringAppend(key, value);
        }

        /// <summary>
        /// 返回key的value
        /// </summary>
        /// <remarks>返回key的value。如果key不存在，返回特殊值nil。如果key的value不是string，就返回错误，因为GET只处理string类型的values。</remarks>
        /// <returns>key对应的value，或者nil（key不存在时）</returns>
        public string StringGet(string key)
        {
            return RedisManager.ReadDataBase().StringGet(key);
        }

        /// <summary>
        /// 获取存储在key上的值的一个子字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start">起始位置</param>
        /// <param name="end">结束位置</param>
        /// <remarks>返回key对应的字符串value的子串，这个子串是由start和end位移决定的（两者都在string内）。可以用负的位移来表示从string尾部开始数的下标。所以-1就是最后一个字符，-2就是倒数第二个，以此类推。</remarks>
        /// <returns></returns>
        public string StringGetRange(string key,int start,int end)
        {
            return RedisManager.ReadDataBase().StringGetRange(key, start, end);
        }

        /// <summary>
        /// 设置一个key的value，并获取设置之前的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <remarks>如果key存在但是对应的value不是字符串，就返回错误</remarks>
        /// <returns></returns>
        public string StringGetSet(string key,string value)
        {
            return RedisManager.WriteDataBase().StringGetSet(key, value);
        }

        /// <summary>
        /// 设置一个key的value值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <remarks>返回key的value。如果key不存在，返回特殊值nil。如果key的value不是string，就返回错误，因为GET只处理string类型的values。</remarks>
        /// <returns>如果SET命令正常执行那么回返回OK，否则如果加了NX 或者 XX选项，但是没有设置条件。那么会返回nil。</returns>
        public bool StringSet(string key,string value)
        {
            bool flag = RedisManager.WriteDataBase().StringSet(key: key, value: value);
            return flag;
        }


        #endregion

        #region List命令
        
        /// <summary>
        /// 获取指定下标的元素
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public string ListIndex(string key, int index)
        {
            return RedisManager.ReadDataBase().ListGetByIndex(key, index);
        }

        public enum RedisCommandDirection
        {
            BEFORE = 0,
            AFTER = 1
        }

        public long ListInsert(string key, RedisCommandDirection direct, string pivot, string value)
        {
            if (direct == RedisCommandDirection.BEFORE)
            {
                return RedisManager.WriteDataBase().ListInsertBefore(key, pivot, value);
            }
            else if (direct == RedisCommandDirection.AFTER)
            {
                return RedisManager.WriteDataBase().ListInsertAfter(key, pivot, value);
            }
            else
            {
                throw new Exception("插入值的方向错误!");
            }
        }

        /// <summary>
        /// 返回List的长度，如果key不存在返回0
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long ListLength(string key)
        {
            return RedisManager.ReadDataBase().ListLength(key);
        }

        /// <summary>
        /// 往队列左边出队一个元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ListLeftPop(string key)
        {
            return RedisManager.WriteDataBase().ListLeftPop(key);
        }

        /// <summary>
        /// 从队列左边入队一个元素
        /// </summary>
        /// <param name="key">列表名称</param>
        /// <param name="value">插入的值</param>
        /// <returns></returns>
        public long ListLeftPush(string key, string value)
        {
            return RedisManager.WriteDataBase().ListLeftPush(key, value);
        }

        /// <summary>
        /// 从队列左边入队一个元素
        /// </summary>
        /// <param name="key">列表名称</param>
        /// <param name="values">插入的值数组</param>
        /// <returns></returns>
        public long ListLeftPush(string key, string[] values)
        {
            var rvalues = new RedisValue[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                rvalues[i] = values[i];
            }
            return RedisManager.WriteDataBase().ListLeftPush(key, rvalues);
        }

        /// <summary>
        /// 当队列存在时，从队列左边入队一个元素
        /// </summary>
        /// <param name="key">列表名称</param>
        /// <param name="value">插入的值</param>
        /// <returns></returns>
        public long ListLeftPushX(string key,string value)
        {
            return RedisManager.WriteDataBase().ListLeftPush(key, value, When.Exists);
        }

        /// <summary>
        /// 从列表中获取指定范围的元素
        /// </summary>
        /// <param name="key">列表名称</param>
        /// <param name="start">起始下标</param>
        /// <param name="stop">结束下标</param>
        /// <returns></returns>
        public ArrayList ListRange(string key,int start,int stop)
        {
            var list = RedisManager.ReadDataBase().ListRange(key, start, stop);
            var result_list = new ArrayList();
            foreach (var item in list)
            {
                result_list.Add(item);
            }
            return result_list;
        }

        /// <summary>
        /// 从队列中删除元素
        /// </summary>
        /// <param name="key">列表名称</param>
        /// <param name="count">删除元素的个数和方向</param>
        /// <param name="value">删除的元素</param>
        /// <returns></returns>
        public long ListRemove(string key, int count, string value)
        {
            return RedisManager.WriteDataBase().ListRemove(key, value, count);
        }

        /// <summary>
        /// 设置队列里面一个元素的值
        /// </summary>
        /// <param name="key">列表名称</param>
        /// <param name="index">下标位置</param>
        /// <param name="value">修改的元素值</param>
        public void ListSetByIndex(string key,int index,string value)
        {
            RedisManager.WriteDataBase().ListSetByIndex(key, index, value);
        }

        /// <summary>
        /// 修建一个已存在的list，这样list就只包含指定范围的元素
        /// </summary>
        /// <param name="key">列表名称</param>
        /// <param name="start">起始下标</param>
        /// <param name="stop">结束下标</param>
        public void ListTrim(string key,int start,int stop)
        {
            RedisManager.WriteDataBase().ListTrim(key, start, stop);
        }

        /// <summary>
        /// 移除source列表中的最后一个元素，添加到destination的第一个元素
        /// </summary>
        /// <param name="source">源队列</param>
        /// <param name="destination">目标队列</param>
        /// <remarks>可用来做安全的队列，循环列表</remarks>
        /// <returns></returns>
        public string ListRightPopLeftPush(string source,string destination)
        {
            return RedisManager.WriteDataBase().ListRightPopLeftPush(source, destination);
        }
        
        /// <summary>
        /// 往队列左边出队一个元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ListRightPop(string key)
        {
            return RedisManager.WriteDataBase().ListRightPop(key);
        }

        /// <summary>
        /// 从队列右边入队一个元素
        /// </summary>
        /// <param name="key">列表名称</param>
        /// <param name="value">插入的值</param>
        /// <returns></returns>
        public long ListRightPush(string key, string value)
        {
            return RedisManager.WriteDataBase().ListRightPush(key, value);
        }

        /// <summary>
        /// 从队列右边入队一个元素
        /// </summary>
        /// <param name="key">列表名称</param>
        /// <param name="values">插入的值数组</param>
        /// <returns></returns>
        public long ListRightPush(string key, string[] values)
        {
            var rvalues = new RedisValue[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                rvalues[i] = values[i];
            }
            return RedisManager.WriteDataBase().ListRightPush(key, rvalues);
        }

        /// <summary>
        /// 当队列存在时，从队列右边入队一个元素
        /// </summary>
        /// <param name="key">列表名称</param>
        /// <param name="value">插入的值</param>
        /// <returns></returns>
        public long ListRightPushX(string key, string value)
        {
            return RedisManager.WriteDataBase().ListRightPush(key, value, When.Exists);
        }

        #endregion

        #region Set命令

        /// <summary>
        /// 添加一个或多个元素到集合里
        /// </summary>
        /// <param name="key">集合名称</param>
        /// <param name="value">插入的值</param>
        /// <returns></returns>
        public bool SetAdd(string key,string value)
        {
            return RedisManager.WriteDataBase().SetAdd(key, value);
        }

        /// <summary>
        /// 添加一个或多个元素到集合里
        /// </summary>
        /// <param name="key">集合名称</param>
        /// <param name="value">插入的值</param>
        /// <returns></returns>
        public long SetAdd(string key,string[] values)
        {
            var rvalues = new RedisValue[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                rvalues[i] = values[i];
            }
            return RedisManager.WriteDataBase().SetAdd(key, rvalues);
        }

        /// <summary>
        /// 返回集合的基数
        /// </summary>
        /// <param name="key">集合名称</param>
        /// <returns></returns>
        public long SetLength(string key)
        {
            return RedisManager.ReadDataBase().SetLength(key);
        }

        /// <summary>
        /// 获取集合里面所有的key
        /// </summary>
        /// <param name="key">集合名称</param>
        /// <returns></returns>
        public ArrayList SetMembers(string key)
        {
            var sets = RedisManager.ReadDataBase().SetMembers(key);
            ArrayList list = new ArrayList();
            foreach (var item in sets)
            {
                list.Add(item.ToString());
            }
            return list;
        }

        #endregion
    }
}
