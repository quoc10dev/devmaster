using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Window cuaso = new Window(10,20);
            cuaso.DrawWindow();
           
            Button nut = new Button(40,50);
            nut.DrawWindow();

            ListBox lb = new ListBox(30, 90, "Nội dung listbox");

            lb.DrawWindow();

            Window[] winArray = new Window[3];
            winArray[0] = new Window(10, 20);
            winArray[1] = new Window(30, 40);
            winArray[2] = new ListBox(50, 60, "Thêm nội dung Listbox");

            for(int i = 0;i< 3; i++)
            {
                winArray[i].DrawWindow();
            }

           Console.ReadLine();
        }
    }
}
