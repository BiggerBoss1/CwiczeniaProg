using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieTime
{
    struct Time : IEquatable<Time>, IComparable<Time>
    {
        private readonly byte _hours;
        private readonly byte _minutes;
        private readonly byte _seconds;
        private readonly long _miliseconds;

        public byte Hours => _hours;
        public byte Minutes => _minutes;
        public byte Seconds => _seconds;
        public long Miliseconds => _miliseconds;

        public Time(byte Hours, byte Minutes, byte Seconds, long Miliseconds) : this()
        {
            _hours = Hours;
            _minutes = Minutes;
            _seconds = Seconds;
            _miliseconds = Miliseconds;
            ValidatingValues();
        }

        public Time(byte Hours, byte Minutes, byte Seconds) : this ()
        {
            _hours = Hours;
            _minutes = Minutes;
            _seconds = Seconds;
            _miliseconds = 0;
            ValidatingValues();
        }

        public Time(byte Hours, byte Minutes) : this()
        {
            _hours = Hours;
            _minutes = Minutes;
            _seconds = 0;
            _miliseconds = 0;
            ValidatingValues();
        }

        public Time(byte Hours) : this ()
        {
            _hours = Hours;
            _minutes = 0;
            _seconds = 0;
            _miliseconds = 0;
            ValidatingValues();
        }

        public Time(string s) : this()
        {
            var stringArray = s.Split(':');
            if(stringArray.Length < 4)
            {
                throw new ArgumentException(message: "Wrong data format, should be HH:mm:ss:MM");
            }

            _hours = Convert.ToByte(stringArray[0]);
            _minutes = Convert.ToByte(stringArray[1]);
            _seconds = Convert.ToByte(stringArray[2]);
            _miliseconds = Convert.ToUInt16(stringArray[3]);

            ValidatingValues();


        }


        private void ValidatingValues()
        {
            if (Hours > 23)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (Minutes > 59)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (Seconds > 59)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (Miliseconds > 999)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public long ChangeToMiliseconds()
        {
           
            return ((Hours * 60 + Minutes) * 60 + Seconds) * 1000 + Miliseconds;
            
        }

        public int CompareTo(Time other)
        {
            var timeThis = this.ChangeToMiliseconds();
            var timeOther = other.ChangeToMiliseconds();
            return timeThis.CompareTo(timeOther);
        }

        public bool Equals(Time other)
        {
            long temp1 = this.ChangeToMiliseconds();
            long temp2 = other.ChangeToMiliseconds();
            return temp1 == temp2;
            
        }

        public override string ToString()
        {
            return Hours + ":" + Minutes + ":" + Seconds + ":" + Miliseconds;
        }

        public static Time ChangeFromMiliseconds(long milis)
        {
            var miliseconds = milis % 1000;
            var tempSeconds = milis / 1000;
            var seconds = tempSeconds % 60;
            var tempMinutes = tempSeconds / 60;
            var minutes = tempMinutes % 60;
            var tempHours = tempMinutes / 60;
            var hours = tempHours % 24;
            return new Time((byte)hours, (byte)minutes, (byte)seconds, miliseconds);
        }

        public Time Plus(Time time)
        {
            return Plus(hour: time.Hours, minute: time.Minutes, second: time.Seconds, miliseconds: time.Miliseconds);
        }

        public Time Plus(byte hour, byte minute, byte second, long miliseconds)
        {
            var secondsToAdd = ((hour * 60 + minute) * 60 + second) * 1000 + miliseconds;
            var actualSeconds = this.ChangeToMiliseconds();

            actualSeconds += secondsToAdd;
            return ChangeFromMiliseconds(actualSeconds);
        }


        public Time Minus(Time time)
        {
            return Minus(hour: time.Hours, minute: time.Minutes, second: time.Seconds, miliseconds: time.Miliseconds);
        }

        public Time Minus(byte hour, byte minute, byte second, long miliseconds)
        {
            var secondsToAdd = ((hour * 60 + minute) * 60 + second) * 1000 + miliseconds;
            var actualSeconds = this.ChangeToMiliseconds();

            actualSeconds -= secondsToAdd;

            if (actualSeconds < 0)
                throw new ArgumentException("You cannot substract lower from greater");

            return ChangeFromMiliseconds(actualSeconds);
        }
    }
}
