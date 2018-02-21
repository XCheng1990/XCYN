﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCYN.Print.DesignPattern.Bridge;
using XCYN.Print.DesignPattern.ChainOfResponsibility;
using XCYN.Print.DesignPattern.Command;
using XCYN.Print.DesignPattern.Composite;
using XCYN.Print.DesignPattern.Factory;
using XCYN.Print.DesignPattern.Filter;
using XCYN.Print.DesignPattern.Flyweight;
using XCYN.Print.DesignPattern.Mediator;
using XCYN.Print.DesignPattern.Memento;
using XCYN.Print.DesignPattern.Observer;
using XCYN.Print.DesignPattern.Proxy;
using XCYN.Print.DesignPattern.State;
using XCYN.Print.DesignPattern.Strategy;
using XCYN.Print.MultiThread;
using XCYN.Print.rabbitmq;
using XCYN.Print.Redis;
using XCYN.Print;
using System.Net.Http;
using System.Web.Http.SelfHost;
using System.Web.Http;
using System.Threading;
using XCYN.Print.Operators;

namespace XCYN.Print
{
    class Program
    {
        static void Main(string[] args)
        {
            DemoMonitor();
        }

        private static void DemoMonitor()
        {
            for (int i = 0; i < 5; i++)
            {
                Task.Factory.StartNew(() => {
                    XCYN.Print.MultiThread.DemoMonitor.Fun2();
                });
            }
            
            
            Console.Read();
        }

        private static void RelationOperators()
        {
            RelationOperators operators = new Operators.RelationOperators();
            operators.Fun1();
        }

        private static void DemoShiftOperators()
        {
            ShiftOperators operators = new ShiftOperators();
            operators.Fun2();
        }

        private static void DemoMultiplicationOperations()
        {
            MultiplicationOperators operators = new MultiplicationOperators();
            operators.Fun3();
        }

        private static void DemoCountdownEvent()
        {
            DemoCountdownEvent demo = new MultiThread.DemoCountdownEvent();
            demo.Fun1();
        }

        /// <summary>
        /// 读写锁
        /// </summary>
        private static void DemoReadWriteLock()
        {
            DemoReadWriteLock demo = new MultiThread.DemoReadWriteLock();
            //demo.Fun1();
            //demo.Fun2();
            demo.Fun3();
            Console.Read();
        }

        private static void DemoLock()
        {
            DemoLock demo = new DemoLock();
            
            for (int i = 0; i < 5; i++)
            {
                Task.Factory.StartNew(() => {
                    demo.Fun7();
                });
            }

            Console.Read();
        }

        /// <summary>
        /// WebAPI托管(Windows自托管)
        /// </summary>
        private static void WebAPIHost()
        {
            //宿主
            var config = new HttpSelfHostConfiguration(new Uri("http://localhost:55898"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            var host = new HttpSelfHostServer(config);
            host.OpenAsync().Wait();
            Console.WriteLine("Press any key to exit");
            Console.Read();
            host.CloseAsync().Wait();
        }
        
        /// <summary>
        /// WebAPI客户端
        /// </summary>
        private static void WebAPIClient()
        {
            var greetingServiceAddress = new Uri("http://localhost:55898/api/greeting");

            var client = new HttpClient();

            var result = client.GetAsync(greetingServiceAddress).Result;

            var greeting = result.Content.ReadAsStringAsync().Result;

            Console.WriteLine(greeting);

            Console.Read();
        }

        private void RunTaskScheduler()
        {
            Application.Run(new DemoTaskScheduler());
        }

        /// <summary>
        /// 组合模式
        /// </summary>
        private static void Composite()
        {
            var root = new Composite("root");
            var net = new Composite("net");
            var program = new Composite("program");
            root.Add(net);
            root.Add(program);
            var c = new Composite("c#");
            var core = new Composite("core");
            net.Add(c);
            net.Add(core);
            var java = new Composite("java");
            program.Add(java);
            root.Display(1);
        }

        /// <summary>
        /// 享元模式
        /// </summary>
        private static void FlyWeightCommand()
        {
            var model = Factory.GetInstance(1);

            var model2 = Factory.GetInstance(1);

            var model3 = Factory.GetInstance(2);

            Console.WriteLine("model1 == model2?{0}",model.Equals(model2));

            Console.WriteLine("model2 == model3?{0}", model2.Equals(model3));
        }

        /// <summary>
        /// 桥接模式
        /// </summary>
        private static void BridgeCommand()
        {
            PhoneBrand xiaomi = new PhoneXiaoMi();
            Soft map = new Map();
            Soft game = new Game();
            xiaomi.AddSoft(map);
            xiaomi.AddSoft(game);
            xiaomi.run();
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
