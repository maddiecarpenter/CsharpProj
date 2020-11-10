using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpProj.Single
{
    public class SingletonInherit<T> : Singleton<T> where T : SingletonInherit<T>, new()
    {
        public float GameLevel = 1;
    }
}
