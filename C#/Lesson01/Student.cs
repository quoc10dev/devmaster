using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    internal class Student 
    {
       public string id;
        public string name;
        public int age;
    
        public Student() {
            id = "123";
            name = "test";
            age = 5;
        }
    public void Display()
        {
            Console.WriteLine("ID : " + this.id);
            Console.WriteLine(" Name :  " + this.name);
            Console.WriteLine("Tuoi : " + this.age); 
   

        }
    
    
    
    }
}



