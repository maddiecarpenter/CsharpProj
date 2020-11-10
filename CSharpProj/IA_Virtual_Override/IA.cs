using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpProj.IA_Virtual_Override
{
    public interface IA
    {
        void H();
    }

    public interface IB
    {
        void H();
    }

    public abstract class D
    {
        //public void H() { }//没有标识成virtual，abstract或者override，因此它的子类不能进行重写
        public virtual void H() { }
        public void M() { }
    }

    public class C : D, IA, IB
    {
        void IA.H()
        {
            Console.WriteLine("all a.h");
        }

        void IB.H()
        {
            Console.WriteLine("all a.h");
        }

        public override void H()//T
        {
            Console.WriteLine("all d.h");
        }
    }

    public class E : D
    {
        public override void H()
        {
            Console.WriteLine("all e.h");
        }
    }
}
