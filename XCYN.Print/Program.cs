﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using XCYN.Print.DesignPattern.ChainOfResponsibility;
using XCYN.Print.DesignPattern.Command;
using XCYN.Print.DesignPattern.Factory;
using XCYN.Print.DesignPattern.Filter;
using XCYN.Print.DesignPattern.Mediator;
using XCYN.Print.DesignPattern.Memento;
using XCYN.Print.DesignPattern.Observer;
using XCYN.Print.DesignPattern.Proxy;
using XCYN.Print.DesignPattern.State;
using XCYN.Print.DesignPattern.Strategy;
using XCYN.Print.MultiThread;
using XCYN.Print.Redis;

namespace XCYN.Print
{
    class Program
    {
        
        static void Main(string[] args)
        {
            DemoWait p = new DemoWait();
            p.Fun8();
            Console.Read();

        }

        /// <summary>
        /// 责任链模式
        /// </summary>
        private static void ChainCommand()
        {
            AbstractHandler handler = new ConcreteHander1();
            AbstractHandler handler2 = new ConcreteHander2();
            AbstractHandler handler3 = new ConcreteHander3();
            //链式调用 handler1保存handler2的引用，handler2保存handler3的引用，这样就能逐级调用
            handler.SetHandler(handler2);
            handler2.SetHandler(handler3);
            //执行
            handler.Request(2);
            Console.Read();
        }

        /// <summary>
        /// 命令模式
        /// </summary>
        private static void HandleCommand()
        {
            Received recieved = new Received();
            //创建命令
            ICommand add = new AddCommand(recieved);
            ICommand remove = new RemoveCommand(recieved);
            //添加命令
            Invoker invoker = new Invoker();
            invoker.SetCommand(add);
            invoker.SetCommand(remove);
            invoker.SetCommand(add);
            invoker.SetCommand(add);

            //撤销
            invoker.Redo();
            //执行
            invoker.execute();
            Console.Read();
        }

        /// <summary>
        /// 状态模式
        /// </summary>
        private static void HandleState()
        {
            Context context = new Context(9, new MorningState());
            context.request();
            Console.Read();
        }

        /// <summary>
        /// 备忘录模式
        /// </summary>
        private static void HandleMemento()
        {
            //初始化
            Originator o = new Originator();
            o.msg = "Hello World";

            //创建一个备份
            Memento backup = o.CreateMemento();
            //将备份保存到Caretaker中去
            Caretaker c = new Caretaker()
            {
                memento = backup,
            };
            Console.WriteLine("备份中:"+o.msg);

            //修改
            o.msg = "Good Bye";
            Console.WriteLine("修改中:" + o.msg);

            //恢复
            o.RecoverMemento(c.memento);
            Console.WriteLine("恢复中:" + o.msg);
            Console.Read();
        }

        /// <summary>
        /// 工厂方法
        /// </summary>
        private static void HandleFactory()
        {
            IFactory sqlserver = new SqlserverFactory();
            sqlserver.CreateInstance().Create();

            IFactory sqlite = new SqliteFactory();
            sqlite.CreateInstance().Remove();

            Console.Read();
        }

        /// <summary>
        /// 中介者模式
        /// </summary>
        private static void HandleMediator()
        {
            AbstractMediator mediator = new QQMediator();
            
            AbstractColleague colleague = new Colleague(mediator);
            colleague.UserName = "紫涵";

            AbstractColleague colleague2 = new Colleague(mediator);
            colleague2.UserName = "灵儿";

            mediator.Add(colleague);
            mediator.Add(colleague2);

            //发送消息
            colleague.Send("灵儿", "你好");
            colleague.Send("紫涵", "早上好~");
            Console.Read();
        }

        private static void HandleObserver()
        {
            ISubject subject = new ConcreteSubject();
            IObserver observer = new ConcreteObserver1(subject,"观察者1");
            IObserver observer2 = new ConcreteObserver1(subject, "观察者2");

            subject.Add(observer);
            subject.Add(observer2);

            subject.SutjectState = "服务器崩溃了!";
            subject.Notify();

            Console.Read();
        }

        private static void HandleStrategy()
        {
            //创建一个策略
            StrategyContext context = new StrategyContext(new FileLog());
            context.Write("aaaaaabbbbbb");

            //创建另一个策略
            context = new StrategyContext(new DBLog());
            context.Write("aaaaaabbbbbb");
            Console.Read();
        }

        private static void HandleFilter()
        {
            List<DesignPattern.Filter.Person> list_person = new List<DesignPattern.Filter.Person>() {
                new DesignPattern.Filter.Person()
                {
                   age = 19,
                   name = "cheng",
                   sex = 0
                },
                new DesignPattern.Filter.Person()
                {
                   age = 18,
                   name = "xheng",
                   sex = 0
                },
                new DesignPattern.Filter.Person()
                {
                   age = 19,
                   name = "xie",
                   sex = 0
                },
                new DesignPattern.Filter.Person()
                {
                   age = 19,
                   name = "cheng",
                   sex = 1
                },
            };
            //NameFilter namefilter = new NameFilter();
            //list_person = namefilter.Filter(list_person);
            //AndFilter andfilter = new AndFilter(new List<IFilter>()
            //{
            //     new NameFilter(),
            //     new AgeFilter()
            //});
            //list_person = andfilter.Filter(list_person);
            OrFilter orfilter = new OrFilter(new List<IFilter>()
            {
                 new NameFilter(),
                 new AgeFilter()
            });
            list_person = orfilter.Filter(list_person);

        }

        private static void HandleService()
        {
            ServiceHost host = new ServiceHost(typeof(DataService));
            host.Open();
            Console.WriteLine("服务启动");
            Console.Read();
        }

        private void HandleSignleton()
        {
            Console.WriteLine("主线程时间：{0}", DateTime.Now);
            //var db = XCYN.Print.DesignPattern.Singleton.hungry.DB.GetInstance();
            //Console.WriteLine("Show调用的时间：{0}", db.Show());
            //Console.WriteLine("Show调用的时间：{0}", XCYN.Print.DesignPattern.Singleton.hungry.DB.Show());

            //var db2 = XCYN.Print.DesignPattern.Singleton.hungry.DB.GetInstance();
            //Console.WriteLine("Show调用的时间：{0}", db2.Show());
            //Console.WriteLine("Show调用的时间：{0}", XCYN.Print.DesignPattern.Singleton.hungry.DB.Show());

            //多线程调用单例模式的例子
            Task.Factory.StartNew(() =>
            {
                var db = XCYN.Print.DesignPattern.Singleton.lazy.DB.GetInstance();
            });
            Task.Factory.StartNew(() =>
            {
                var db = XCYN.Print.DesignPattern.Singleton.lazy.DB.GetInstance();
            });
            Task.Factory.StartNew(() =>
            {
                var db = XCYN.Print.DesignPattern.Singleton.lazy.DB.GetInstance();
            });
            Task.Factory.StartNew(() =>
            {
                var db = XCYN.Print.DesignPattern.Singleton.lazy.DB.GetInstance();
            });
            Console.Read();
        }
    }
}
