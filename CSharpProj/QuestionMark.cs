using System;
using System.Collections.Generic;
using System.Text;
using CSharpProj.Single;
namespace CSharpProj
{
    class QuestionMark:Singleton<QuestionMark>
    {
        public void Example1()
        {
            int? a = null;
            //int b = null;
            a = 10;
            Console.WriteLine("可空类型"+a);
            Instance = null;
        }
        public void Example2()
        {
            Console.WriteLine("可空运算符");
        }
        public void Example3()
        {
            int res = true == false ? 1 : 0;
            Console.WriteLine("三元运算符结果"+res);
        }
        int key = 0;
        string str = "";
        public void Example4()
        {
            int key2 = Instance?.key ?? 100;//有点搞不明白 https://www.cnblogs.com/iNeXTs/p/10023996.html
            str = "str";
            string str2 = Instance.str ?? "str2";//预期结果是str，正确
            Console.WriteLine("空合并运算符");
            Console.WriteLine("str2= " + str2);

            str = null;
            str2 = (Instance.str != null) ? Instance.str : "str2";//这两句意义是一样的
            str2 = Instance.str ?? "str2";//预期结果是str2，结果是str2
            Console.WriteLine("空合并运算符");
            Console.WriteLine("str2= " + str2);
        }
    }
}
