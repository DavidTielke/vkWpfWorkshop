using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDelegates
{

    class Program
    {
        static void Main(string[] args)
        {
            var button = new Button();
            button.Clicked += ClickHandler;

            button.Click();

            //button.Clicked = null;
            //button.Clicked.Invoke();

            Console.ReadKey();
        }

        static void ClickHandler()
        {
            Console.WriteLine("Hallo Welt der Events");
        }
        static void ClickHandler2()
        {
            Console.WriteLine("Hallo Welt der Events");
        }
    }

    delegate void MyClickHandler();
    class Button
    {
        public event MyClickHandler Clicked;
        
        public void Click()
        {
            OnClicked();
        }

        protected virtual void OnClicked()
        {
            var del = Clicked;
            if (del != null)
            {
                del();
            }
        }
    }

    class DoubleClickButton : Button
    {
        public void DoubleClick()
        {
            OnClicked();
            OnClicked();
        }
    }
}
