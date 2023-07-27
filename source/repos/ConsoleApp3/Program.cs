using System;
using System.Collections;
namespace VietTutsCsharp
{
    class Program
    {
        public void Main(string[] args)
        {
            int i;
            Console.Write("10 so dau tien cua day so Fibonacci: \n");
            for (i = 0; i < 10; i++)
            {
                Console.Write("{0} ", fibonacci(i));
            }

            Console.WriteLine();
            Console.ReadKey();
        }


        /**
         * Tinh so Fibonacci thu n
         * 
         * @param n: chi so cua so Fibonacci tinh tu 0 
         *           vd: F0 = 0, F1 = 1, F2 = 1, F3 = 2
         * @return So Fibonacci thu n
         */
        static int fibonacci(int n)
        {
            int f0 = 0;
            int f1 = 1;
            int fn = 1;
            int i;

            if (n < 0)
            {
                return -1;
            }
            else if (n == 0 || n == 1)
            {
                return n;
            }
            else
            {
                for (i = 2; i < n; i++)
                {
                    f0 = f1;
                    f1 = fn;
                    fn = f0 + f1;
                }
            }
            return fn;
        }
    }
}