using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lesson04
{
    abstract class Student
    {
        public string name;
        public string year;
       
    public Student()
    {

    }
    public Student (string name, string year)
    {
        this.name = name;   
        this.year = year;   
    }

    public void Display()
    {
        Console.WriteLine("Ho va ten : {0} ", name);

        Console.WriteLine("Năm sinh  : {0} ", year);
    }

        public abstract double Avg();

    }
}
