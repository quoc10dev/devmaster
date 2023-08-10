using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Lesson04
{
    internal class Window
    {

        public int top;
        public int left;

        /// Constucture
        /// 
        public Window(int top, int left) 
        {
            this.top = top;
            this.left = left;
         }
        

            public virtual void DrawWindow()
        {
            Console.WriteLine("Window Draw : Left {0} - Top {1} ", left, top);
        }

    }

    class Button : Window
    {
        public Button(int top, int left) : base(top, left)
        {

        }
        public override void DrawWindow()
        {
            Console.Write("Drawing a button at {0}, {1}  \n", top, left);
        }
    }

    class ListBox : Window
        {
            public string ListBoxContent;

            public ListBox(int top, int left, string content) : base(top, left)
            {
                   ListBoxContent = content;
            }

            public override void DrawWindow()
            {
                base.DrawWindow();
                Console.WriteLine("Writing string to the list box : {0}", ListBoxContent);
            }
        }
    
}
