using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementGameCore
{
    public class Credit
    {
        public Money Amount { get; private set; }
        public int Period { get; private set; }
        public Credit(Money amount, int period)
        {
            Amount = amount;
            Period = period;
        }

        public bool PeriodDec()
        {
            return --Period == 0;
        }

        public Money Percent()
        {
            return Amount % 1;
        }
    }
}
