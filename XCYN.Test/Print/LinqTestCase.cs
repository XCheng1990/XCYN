﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XCYN.Test.Print
{
    [TestClass]
    public class LinqTestCase
    {
       
        public LinqTestCase()
        {
            
            //
            //TODO:  在此处添加构造函数逻辑
            //
        }

        [TestMethod]
        public void TestAll()
        {
            Product[] list = new Product[] {
            new Product
            {
                Name = "we"
            },
            new Product
            {
                Name = "fsdfasfwe"
            }
            };

            var flag = list.All(m => {
                var last = m.Name.Last();
                return last.Equals('e');
            });
            Assert.IsTrue(flag);
        }

        [TestMethod]
        public void TestAny()
        {
            Product[] list = new Product[] {
            new Product
            {
                Name = "we"
            },
            new Product
            {
                Name = "fsdfasfwe"
            }
            };

            var flag = list.Any(m => m.Name.Equals("we"));
            Assert.IsTrue(flag);
            flag = list.Any(m => m.Name.Equals("wo"));
            Assert.IsFalse(flag);
        }

        [TestMethod]
        public void TestContains()
        {
            Product[] list = new Product[] {
            new Product
            {
                Name = "we"
            },
            new Product
            {
                Name = "fsdfasfwe"
            }
            };

            var flag = list.Contains(new Product
            {
                Name = "we"
            },new ProductEqualityComparer());
            Assert.IsTrue(flag);    //对于复杂类型，需要实现IEqualityComparer才能比较

            flag = list.Contains(new Product
            {
                Name = "we"
            });
            Assert.IsFalse(flag);

            var str = "we";
            Assert.IsTrue(str.Contains("we"));  //简单类型，可以直接比较

        }
        
        [TestMethod]
        public void TestCount()
        {
            Product[] list = new Product[] {
            new Product
            {
                Name = "we"
            },
            new Product
            {
                Name = "fsdfasfwe"
            }
            };

            var count = list.Count();
            Assert.AreEqual(2, count);
            Assert.AreEqual(1, list.Count(m => m.Name.Equals("we")));
            
        }

        [TestMethod]
        public void TestFirst()
        {
            Product[] list = new Product[] {
            new Product
            {
                Name = "we"
            },
            new Product
            {
                Name = "fsdfasfwe"
            }
            };
            
            Assert.AreEqual("we", list.First().Name);
            Assert.AreEqual("fsdfasfwe", list.First(m => m.Name.Equals("fsdfasfwe")).Name);

        }

        [TestMethod]
        public void TestFirstOrDefault()
        {
            Product[] list = new Product[] {
            };

            Assert.AreEqual(default(string), list.FirstOrDefault());
            
        }
        
        [TestMethod]
        public void TestLast()
        {
            Product[] list = new Product[] {
            new Product
            {
                Name = "Wu"
            },
            new Product
            {
                Name = "Li"
            }
            };

            Assert.AreEqual("Li", list.Last().Name);
            Assert.AreEqual("Wu", list.Last(m => m.Name.Equals("Wu")).Name);
            try
            {
                var last = list.Last(m => m.Name.Equals("Cheng"));
            }
            catch(Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        [TestMethod]
        public void TestLastOrDefault()
        {
            Product[] list = new Product[] {
            };

            Assert.AreEqual(default(string), list.LastOrDefault());

        }

        [TestMethod]
        public void TestMaxAndMin()
        {
            Product[] list = new Product[] {
                new Product
                {
                    Name = "Jack",
                    Age = 10,
                },
                new Product
                {
                    Name = "Jim",
                    Age = 12,
                },
                new Product
                {
                    Name = "John",
                    Age = 20,
                },
            };

            Assert.AreEqual(20, list.Max(m => m.Age));
            Assert.AreEqual(10, list.Min(m => m.Age));
        }

        [TestMethod]
        public void TestOrderBy()
        {
            Product[] list = new Product[] {
                new Product
                {
                    Name = "Jack",
                    Age = 10,
                },
                new Product
                {
                    Name = "Jim",
                    Age = 12,
                },
                new Product
                {
                    Name = "John",
                    Age = 20,
                },
                new Product
                {
                    Name = "Anne",
                    Age = 15,
                },
                 new Product
                {
                    Name = "Worker",
                    Age = 15,
                },
            };

            Assert.AreEqual("Jack", list.OrderBy(m => m.Age).First().Name);
            Assert.AreEqual("John", list.OrderByDescending(m => m.Age).First().Name);
            Assert.AreEqual("Worker", list.OrderBy(m => m.Name,new ProductComparer()).First().Name);
            Assert.AreEqual("Jim", list.OrderByDescending(m => m.Name, new ProductComparer()).First().Name);
        }

        /// <summary>
        /// 体现延迟加载
        /// </summary>
        [TestMethod]
        public void TestOrderBy2()
        {
            List<Product> list = new List<Product>(){
                new Product
                {
                    Name = "Jack",
                    Age = 10,
                },
                new Product
                {
                    Name = "Jim",
                    Age = 12,
                },
                new Product
                {
                    Name = "John",
                    Age = 20,
                },
            };
            var obj = list.OrderBy(m => m.Age);
            
            list.Add(new Product
            {
                Name = "Buke",
                Age = 1
            });
            Assert.AreEqual("Buke", obj.First().Name);
         
        }

        [TestMethod]
        public void TestReverse()
        {
            List<Product> list = new List<Product>(){
                new Product
                {
                    Name = "Jack",
                    Age = 10,
                },
                new Product
                {
                    Name = "Jim",
                    Age = 12,
                },
                new Product
                {
                    Name = "John",
                    Age = 20,
                },
            };
            list.Reverse(0, 2);//反转前两个元素
            Assert.AreEqual("Jim", list[0].Name);
            Assert.AreEqual("John", list[2].Name);
        }

        [TestMethod]
        public void TestSelect()
        {
            List<Product> list = new List<Product>(){
                new Product
                {
                    Name = "Jack",
                    Age = 10,
                },
                new Product
                {
                    Name = "Jim",
                    Age = 12,
                },
                new Product
                {
                    Name = "John",
                    Age = 20,
                },
            };
            Assert.AreEqual("Jack", list.Select(m => new { Name = m.Name,Age = m.Age }).ElementAt(0).Name);
            var obj = list.Select(m => new { Name = m.Name, Age = m.Age }); //延迟加载
            list.Insert(0, new Product
            {
                Name = "Ann",
                Age = 10,
            });
            Assert.AreEqual("Ann", obj.ElementAt(0).Name);
        }

        [TestMethod]
        public void TestSelectMany()
        {
            List<Product> list = new List<Product>(){
                new Product
                {
                    Name = "Jack",
                    Age = 10,
                    Ability = new List<string>(){"Eat","Sport"}
                },
                new Product
                {
                    Name = "Jim",
                    Age = 12,
                    Ability = new List<string>(){"Swim","Fly"}
                },
                new Product
                {
                    Name = "John",
                    Age = 20,
                    Ability = new List<string>(){"Talk","Write"}
                },
            };
            var obj = list.SelectMany(m => m.Ability).Where(m => m.Equals("Eat"));
            Assert.AreEqual("Eat", obj.First());
            var obj2 = list.SelectMany((m, n) => {
                if(n > 0)
                {
                    //跳过第一个元素
                    return m.Ability;
                }
                return new List<string>();
            }).Where(m => m.Equals("Fly"));
            Assert.AreEqual(1, obj2.Count());

            var obj3 = list.SelectMany(m =>
            {
                //返回每个Ability的单词数
                List<int> nums = new List<int>();
                m.Ability.ForEach((a) =>
                {
                    nums.Add(a.Length);
                });
                return nums;
            }, 
            (m, n) =>
            {
                Dictionary<string, int> dict = new Dictionary<string, int>();
                if (n > 4)
                {
                    //跳过单词数小于4的元素
                    dict[m.Name] = m.Age;
                    return dict;
                }
                return dict;
            });
            var r = obj3.ToList();
        }

        [TestMethod]
        public void TestSingle()
        {
            List<Product> list = new List<Product>(){
                new Product
                {
                    Name = "Jack",
                    Age = 10,
                    Ability = new List<string>(){"Eat","Sport"}
                },
                new Product
                {
                    Name = "Jim",
                    Age = 12,
                    Ability = new List<string>(){"Swim","Fly"}
                },
                new Product
                {
                    Name = "John",
                    Age = 20,
                    Ability = new List<string>(){"Talk","Write"}
                },
            };
            try
            {
                var obj = list.Single();//返回超过一个元素，即抛出异常
            }
            catch(InvalidOperationException ex)
            {
                Assert.IsTrue(true);
            }
            var obj2 = list.Take(1).Single();
            Assert.IsNotNull(obj2);

        }

        [TestMethod]
        public void TestSingleOrDefault()
        {
            List<Product> list = new List<Product>(){
                new Product
                {
                    Name = "Jack",
                    Age = 10,
                    Ability = new List<string>(){"Eat","Sport"}
                },
                new Product
                {
                    Name = "Jim",
                    Age = 12,
                    Ability = new List<string>(){"Swim","Fly"}
                },
                new Product
                {
                    Name = "John",
                    Age = 20,
                    Ability = new List<string>(){"Talk","Write"}
                },
            };
            var obj = list.Take(1).SingleOrDefault();
            Assert.IsNotNull(obj);
            list = new List<Product>();
            var obj2 = list.SingleOrDefault();  //SingleOrDefault将返回List<string>的默认值，即Null
            Assert.AreEqual(obj2, default(List<string>));
        }

        [TestMethod]
        public void TestSkip()
        {
            List<Product> list = new List<Product>(){
                new Product
                {
                    Name = "Jack",
                    Age = 10,
                    Ability = new List<string>(){"Eat","Sport"}
                },
                new Product
                {
                    Name = "Jim",
                    Age = 12,
                    Ability = new List<string>(){"Swim","Fly"}
                },
                new Product
                {
                    Name = "John",
                    Age = 20,
                    Ability = new List<string>(){"Talk","Write"}
                },
            };
            //SkipWhile 跳过满足谓词的元素
            var count = list.SkipWhile(m => m.Age <= 12).Count();
            Assert.AreEqual(1, count);
            var count2 = list.SkipWhile(m => m.Name.Equals("Jack")).Count();
            Assert.AreEqual(2, count2);
        }

        [TestMethod]
        public void TestTake()
        {
            List<Product> list = new List<Product>(){
                new Product
                {
                    Name = "Jack",
                    Age = 10,
                    Ability = new List<string>(){"Eat","Sport"}
                },
                new Product
                {
                    Name = "Jim",
                    Age = 12,
                    Ability = new List<string>(){"Swim","Fly"}
                },
                new Product
                {
                    Name = "John",
                    Age = 20,
                    Ability = new List<string>(){"Talk","Write"}
                },
            };
            var obj = list.TakeWhile(m => m.Age <= 12); //获取满足谓词条件的元素，直到不符合筛选条件
            Assert.AreEqual(2, obj.Count());
        }

        [TestMethod]
        public void TestToDictionaryOrToList()
        {
            List<Product> list = new List<Product>(){
                new Product
                {
                    Name = "Jack",
                    Age = 10,
                    Ability = new List<string>(){"Eat","Sport"}
                },
                new Product
                {
                    Name = "Jim",
                    Age = 12,
                    Ability = new List<string>(){"Swim","Fly"}
                },
                new Product
                {
                    Name = "John",
                    Age = 20,
                    Ability = new List<string>(){"Talk","Write"}
                },
            };
            
            var dict = list.ToDictionary(m => Guid.NewGuid());  //返回值为键，入参为值
            var list2 = dict.Select(m => m.Value);  //利用Select函数可以重新分离出Product列表
        }

    }

    class ProductComparer : IComparer<string>
    {
        /// <summary>
        /// 求出最常的字符串
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(string x, string y)
        {
            return -(x.Length - y.Length);
        }
    }

    class ProductEqualityComparer : IEqualityComparer<Product>
    {
        //如果名字和年龄都相等则这两个对象是相等的。
        public bool Equals(Product x, Product y)
        {
            return x.Name.Equals(y.Name) && x.Age == y.Age;
        }

        public int GetHashCode(Product obj)
        {
            return obj.GetHashCode();
        }
    }

    class Product
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<string> Ability { get; set; }
    }
}