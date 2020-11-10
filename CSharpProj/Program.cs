using System;
using System.Collections;
using System.Collections.Generic;
using CSharpProj.IA_Virtual_Override;
using CSharpProj.Serialize;
using CSharpProj.Single;

using newProj=NewCSharpProj.Global;
/* CannotUsingGlobal不能使用的原因是什么 */
namespace CSharpProj
{
    public class System
    {
        //这是用户自定义的，通常在使用系统类如System里面的方法时，需要使用global::来避免命名冲突
        public static class Console
        {
            public static void WriteLine(string str)
            {
                global::System.Console.WriteLine("my: "+str);//这里调用的是全局的system，因此不会陷入循环当中
            }
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            string[] strs = Console.ReadLine().Split(' ');
            int ctrl = 0;
            foreach(string s in strs)
            {
                ctrl = int.Parse(s);
                switch (ctrl)
                {
                    case 1:
                        _TestDelegateEvent();
                        break;
                    case 2:
                        _TestSingleton();
                        break;
                    case 3:
                        IA ia = new C();
                        Console.WriteLine("子类对接口实现");
                        ia.H();
                        //需要明白父类和子类之间是如何实现多态的
                        /*
                        在运行时，会根据实际创建的对象类型决定使用那个方法。
                        一般将这称为动态绑定。   
                        */
                        Console.WriteLine("不同子类对同一个类的继承，是如何形成多态的");
                        D d;
                        d= new C();
                        d.H();
                        d = new E();
                        d.H();
                        break;
                    case 4:
                        System sys = new System();
                        System.Console.WriteLine("hello world");
                        //global::Console.WriteLine("global");//在全局命名空间里面没有找到命名空间是Console的
                        global::System.Console.WriteLine("global");
                        /*
                            如果global是为了避免命名冲突，但是为什么又说要通知编译器从根目录开始寻找这个namespace或者class
                            global::System 来表示根命名空间，而非是自定义类System
                            It's often added to generated code to avoid name clashes with user code.
                        */
                        break;
                    case 5:
                        QuestionMark.Instance.Example1();
                        QuestionMark.Instance?.Example2();
                        QuestionMark.Instance.Example3();
                        QuestionMark.Instance.Example4();
                        break;
                    case 6:
                        newProj.UsingGlobal.Print("hello world 6");//使用命名空间别名
                        break;
                    case 7:
                        Dictionary<string, Dictionary<string,string>> dic = new Dictionary<string, Dictionary<string,string>>();
                        Dictionary<string, string> dataItem = new Dictionary<string, string>();
                        Dictionary<string, string> msgItem = new Dictionary<string, string>();
                        dataItem.Add("weekTask", "完成消耗500金币");
                        dataItem.Add("seasonTask", "一共完成十场周任务");
                        dataItem.Add("battlePassportLv", "积分/战星是1000");
                        dataItem.Add("shopItem", "任务卡");
                        dic.Add("data", dataItem);//数据主要是服务器记录的信息，包括玩家的通行证等级、购买数据、一般是比较持久存储，不断根据玩家游戏经历更新的数据
                        msgItem.Add("buyLv", "购买等级成功");
                        msgItem.Add("sharePassport", "分享成功");
                        dic.Add("msg", msgItem);//消息主要是玩家和游戏交互，传递的信息，包括点击购买等级、分享后等从服务器及时返回的消息
                        //dic.Add("code", codeItem);
                        string jsonStr= JsonUntity.SerializeDictionaryToJsonString<string,Dictionary<string,string>>(dic);
                        dic.Clear();
                        dic=JsonUntity.DeserializeStringToDictionary<string, Dictionary<string, string>>(jsonStr);
                        foreach(var item in dic)
                        {
                            Console.WriteLine($"item.key={item.Key},item.Value={item.Value}");
                            foreach (var tmp in item.Value)
                            {
                                Console.WriteLine($"tmp.key={tmp.Key},tmp.Value={tmp.Value}");
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            Console.ReadKey();
        }

        public delegate int delegateAction(int i);
        public static event delegateAction OnActionEvent;//以下两句话效果都一样，去掉event一样的
        public static delegateAction daNew;
        private static void _TestSingleton()
        {
            Console.WriteLine("GameLevel: " + SingletonInherit2.Instance.GameLevel);
            Console.WriteLine("GameTime: " + SingletonInherit2.Instance.GameTime);
        }
        private static void _TestDelegateEvent()
        {
            //声明委托，这个委托是一个类，类名叫做delegateAction，他有一个参数类型是方法的构造函数
            //委托定义了方法的参数和返回类型，可以把符合定义的方法赋值给委托
            //同时委托delegateAction中提供了一个实例方法Invoke，可以用来触发委托的执行
            //也就是说第一句声明委托就可以像一个类一样的使用它
            delegateAction test = new delegateAction(_Test2);
            test(0);
            test.Invoke(0);

            //测试已定义好的委托Action
            Action<int, string> method = new Action<int, string>(_Test);
            method(1, "字符串1");
            method.Invoke(1, "字符串1");

            //测试自己定义的事件
            OnActionEvent += _Test2;
            OnActionEvent += _Test22;

            OnActionEvent(2);
            OnActionEvent.Invoke(2);

            //Action委托作为一个函数参数，并且省略定义_Test方法
            _Test3("test3", (i, str) =>
            {
                Console.WriteLine($"test Action委托作为一个函数参数,i={i},str={str}");
            });
            _Test3("test3", _Test);

            daNew += _Test2;
            //daNew -= _Test2;//如果减去了，说明事件不带有任何方法，是空的
            daNew(4);
            daNew.Invoke(4);
        }
        private static void _Test(int i, string str)
        {
            Console.WriteLine($"test Action,i={i},str={str}");
        }

        private static int _Test2(int i)
        {
            Console.WriteLine($"test delegateAction，i={i}");
            return i;
        }

        private static int _Test22(int i)
        {
            Console.WriteLine("test delegateAction,i=22");
            return i;
        }

        private static void _Test3(string str, Action<int, string> regist)
        {
            Console.WriteLine(str);
            regist(3, "3");
        }
    }
}
