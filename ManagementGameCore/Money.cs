using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementGameCore
{
    public class Money : IComparable
    {
        int cents;

        void Reduce()
        {
            Dollars += cents / 100;
            cents %= 100;
        }

        public int Dollars { private set; get; }
        public int Cents 
        {
            get => cents;
            set
            {
                cents = value;
                Reduce();
            }
                
        }

        public Money(int dollars, int cents)
        {
            Dollars = dollars;
            Cents = cents;
        }
        public Money() : this(0, 0) { }

        
        public static implicit operator int(Money money)
        {
            return money.Dollars;
        }

        public static Money operator+(Money left, Money right)
        {
            return new Money(left.Dollars + right.Dollars, left.Cents + right.Cents);
        }

        public static Money operator-(Money left, Money right)
        {
            return new Money(0, (left.Dollars * 100 + left.Cents) - (right.Dollars * 100 + right.Cents));
        }

        public static Money operator*(Money money, int number)
        {
            return new Money(money.Dollars *  number, money.Cents * number);
        }

        public static Money operator/(Money money, int number)
        {
            return new Money(0, (money.Dollars * 100 + money.Cents) / number);
        }

        public static Money operator%(Money money, int number)
        {
            return new Money(0, (money.Dollars * 100 + money.Cents) * number / 100);
        }

        public static bool operator>(Money left, Money right)
        {
            return left.Dollars * 100 + left.Cents > right.Dollars * 100 + right.Cents;
        }

        public static bool operator<(Money left, Money right)
        {
            return left.Dollars * 100 + left.Cents < right.Dollars * 100 + right.Cents;
        }

        public static bool operator>=(Money left, Money right)
        {
            return left.Dollars * 100 + left.Cents >= right.Dollars * 100 + right.Cents;
        }

        public static bool operator<=(Money left, Money right)
        {
            return left.Dollars * 100 + left.Cents <= right.Dollars * 100 + right.Cents;
        }

        public static bool operator==(Money left, Money right)
        {
            return left.Dollars * 100 + left.Cents == right.Dollars * 100 + right.Cents;
        }

        public static bool operator!=(Money left, Money right)
        {
            return left.Dollars * 100 + left.Cents != right.Dollars * 100 + right.Cents;
        }




        public override string ToString()
        {
            return $"${Dollars}.{Cents}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;

            if (obj is Money money)
                return (Dollars * 100 + Cents).Equals(money.Dollars * 100 + money.Cents);
            else
                return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Dollars, Cents);
        }

        public int CompareTo(object? obj)
        {
            if (obj is null) 
                throw new ArgumentNullException(nameof(obj));

            if (obj is Money money)
                return (Dollars * 100 + Cents).CompareTo(money.Dollars * 100 + money.Cents);
            else
                throw new ArgumentException();
        }
    }
}
