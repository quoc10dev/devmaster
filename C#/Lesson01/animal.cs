using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    internal class animal
    {
        public void Eat()
        {
            Console.WriteLine("Animail Eat");
        }

        public void Do()
        {
            Console.WriteLine("Animail Do");
        }
    }

    public class Dog: animal
    {
   
        public void Eat()
        {
            Console.WriteLine("Dog Eat");
        }

        public void Doaction()
        {
            Dog choj = new Dog();
            choj.Eat();
            base.Eat();
        }

    }

}
