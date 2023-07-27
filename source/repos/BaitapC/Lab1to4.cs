using System;
using System.Collections;
using System.Text;
using System.Linq;
using lab05.cs;
namespace Baitap

{ 
    class Program
    {
        public void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Submain(); //Goi ham Submain truoc
            int so = 10;
            so += 5;
            Console.WriteLine("Gia tri cua so la : {0}", so);
            Console.ReadLine();
            int so1 = 23;
            int so2 = 7;
            so1 -= so2;
            so1 += so1;
            Console.WriteLine("Gia tri cua so 1 la: {0}", so1);
            Console.ReadLine();
            int so3 = 10;
            so3++;  //Gia tri so3 = 11
            Console.WriteLine("Gia tri cua so3: {0}", so3);   
            Console.WriteLine("Lenh so3++ xuat truc roi cong sau: {0}",so3++);
            Console.WriteLine("Lenh ++so3 cong truoc roi xuat sau: {0}", ++so3);
            Console.ReadLine();

            PTbac1();

        }
        static void Submain()
        {
            Console.WriteLine("Goi Submantruoc");
        }

        static void PTbac1()
        {
            Console.WriteLine("CHUONG TRINH TINH PHUONG TRINH BAC 1");
            Console.WriteLine("ax + b = 0");
            Console.WriteLine("Nhập vào hệ số a: ");
            float a = float.Parse(Console.ReadLine());
            Console.WriteLine("Nhập vào hệ số b: ");
            float b = float.Parse(Console.ReadLine());
            Console.WriteLine("Gia tri nghiem x la: {0}", -b / a);
            Console.ReadLine();
        }
    }

}

