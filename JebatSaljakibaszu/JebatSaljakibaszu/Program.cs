using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieTime
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var time1 = new Time(12, 32, 20, 25);
            var time2 = new Time(15, 25, 48, 333);
            var time3 = new Time("4:22:14:254");

            Console.WriteLine(Equals(time1, time2));
            Console.WriteLine(time3);

            var milis = time1.ChangeToMiliseconds();
            var time4 = Time.ChangeFromMiliseconds(milis);
            Console.WriteLine(time4.Minus(time2));

           
            

        }
    }
}
