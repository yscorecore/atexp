using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atexp
{
    public class Time
    {
        public DateTimeOffset Datetime { get; set; }

        public static Time FromDatetimeOffset(DateTimeOffset dt)
        {
            return new Time
            {
                Datetime = dt,
            };
        }
    }
}
