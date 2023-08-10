using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    internal class napchong
    {

        public int Sum(int a)
        {
            int s = 0;
            for (int i = 0; i < a; i++)
            {
                s += i;
            }
            return s;
        }

        static int Sum(int a, int b)
        {
            int s = 0;
            for (int i = a; i < b; i++)
            {
                s += i;
            }
            return s;
        }

        public int Add(int a, int b)
        {
            return a + b;
        }

        static double Add( double a, double b)
        {
            return a + b;
        }          

    }
}
