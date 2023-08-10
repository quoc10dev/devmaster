using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lesson05
{
    internal class Program
    {
        static void Main(string[] args)

        {
            Console.OutputEncoding = Encoding.UTF8;
            Employee[] employees = new Employee[100];
            Console.WriteLine("Nhập số lượng Mảng SV : ");
            int a = int.Parse(Console.ReadLine());
            for (int i = 0; i < a; i++)
            {
                employees[i] = new Employee();
                Console.WriteLine("Nhập ID: ");
                employees[i].ID = int.Parse(Console.ReadLine());
                Console.WriteLine("Nhập Name: ");
                employees[i].Name = Console.ReadLine();
                Console.WriteLine("Nhập Age: ");
                employees[i].Age = int.Parse(Console.ReadLine());
                Console.WriteLine("Nhập Salary: ");
                employees[i].Salary = double.Parse(Console.ReadLine());
            }
            Console.WriteLine("In danh sách sinh viên");
            for (int i = 0; i < a; i++)
            {
                Console.WriteLine("\n {0}, {1}, {2}, {3}", employees[i].ID, employees[i].Name, employees[i].Age, employees[i].Salary);


            }
            Array.Sort(new AgeComparer());

            Console.WriteLine("Sắp xếp nhân viên theo Lương tăng dần");

            for (int i = 0; i < a; i++)
            {
                Console.WriteLine("\n {0}, {1}, {2}, {3}", employees[i].ID, employees[i].Name, employees[i].Age, employees[i].Salary);


            }

            Console.ReadLine();
            Console.ReadLine();
        }
    }
}
