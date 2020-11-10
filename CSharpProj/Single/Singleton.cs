using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpProj.Single
{
    public class Singleton<T> where T :Singleton<T>,new()
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    Console.WriteLine("create new instance");
                    instance = new T();
                }
                else
                {
                    Console.WriteLine("instance alread created");
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }
    }
}
