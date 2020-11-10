using System;
using System.Collections.Generic;
using System.Text;
using sc = global::System.Console;//这是类型别名
using sys = global::System;//这是命名空间别名
namespace NewCSharpProj.Global
{
    //合理的运用命名空间别名和类型别名
    class UsingGlobal
    {
        public static void Print(string str = null)
        {
            sc.WriteLine(str);
        }
    }
}
