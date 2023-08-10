using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    internal class Lesson01
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ho va ten : Nguyen Viet Quoc");
            Console.WriteLine("Ngay sinh ;: 21/04/1985");
            Console.WriteLine("Dia chi :: Nha Trang");
            Console.WriteLine("Dien thoai : 0979541383");
            Console.WriteLine("So thich : Love C#");
           

            byte age = 18;
            Console.WriteLine("Tuoi : {0}+", age);
            char gender = 'M';
            Console.WriteLine("Gender :" + gender);
            Console.WriteLine("");
            

            Console.WriteLine("    ++++++++++                 ++++     ++++");
            Console.WriteLine("  ++++++++++++++              ++++     ++++");
            Console.WriteLine(" +++++       ++++     ++++++++++++++++++++++++++");
            Console.WriteLine("+++++                 ++++++++++++++++++++++++++ ");
            Console.WriteLine("++++                       ++++      ++++");
            Console.WriteLine("++++                      ++++      ++++");
            Console.WriteLine("++++                  +++++++++++++++++++++++++");
            Console.WriteLine(" ++++                 +++++++++++++++++++++++++");
            Console.WriteLine(" +++++                  ++++      ++++");
            Console.WriteLine("  +++++      ++++      ++++      ++++");
            Console.WriteLine("   +++++++++++++      ++++      ++++");
            Console.WriteLine("     +++++++++  ");
         

            Student student = new Student();
            student.Display();

            Console.WriteLine("////////////////////////////////////");

            napchong nap = new napchong();
            int r = nap.Sum(10);
            Console.WriteLine("Tong: " + r);

            int a = nap.Add(3, 5);
            Console.WriteLine(a);

            Console.ReadLine();
        }
    }
}

