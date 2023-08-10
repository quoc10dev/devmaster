using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Lesson05
{
    internal class Employee : IComparable<Employee>
    {
        public int ID;
        public string Name;
        public int Age;
        public double Salary;
    
        public Employee()
        {

        }
    public Employee(int ID, string Name, int Age, double Salary)
        {
            this.ID = ID;
            this.Name = Name;
            this.Age = Age;
            this.Salary = Salary;        


        }

        public int CompareTo(Employee other)
        {
            return this.Salary.CompareTo(other.Salary);
        }
    }
    public class AgeComparer : IComparer<Employee>
    {   
        int IComparer<Employee>.Compare(Employee x, Employee y)
        {
            return x.Age.CompareTo(y.Age);
        }
    }

}
