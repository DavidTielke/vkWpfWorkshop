using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDelegates
{
    public class Wetterstation
    {
    }

    public delegate void NeuerMesswertHandler(double wert);

    public class Sensor
    {
        public event NeuerMesswertHandler NeuerMesswert;

        protected virtual void OnNeuerMesswert(double wert)
        {
            var del = NeuerMesswert;
            if(del != null)
            {
                del(wert);
            }
        }

        public void Messen()
        {
            var random = new Random();
            OnNeuerMesswert(random.Next(0, 1000));
        }
    }
}
