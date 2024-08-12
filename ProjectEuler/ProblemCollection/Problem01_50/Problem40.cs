using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem40 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 40;
            }
        }

        public override string Description
        {
            get
            {
                return @"
                    Champernowne's constant
                    Problem 40
                    An irrational decimal fraction is created by concatenating the positive integers:

                    0.123456789101112131415161718192021...

                    It can be seen that the 12th digit of the fractional part is 1.

                    If dn represents the nth digit of the fractional part, find the value of the following expression.

                    d1 × d10 × d100 × d1000 × d10000 × d100000 × d1000000
                    ";
            }
        }

        public override string Solution1()
        {
            int digits = 1;
            long totalChar = 0;
            int upperLimit = 1000000;

            int pow = 1;

            List<char> nthDigits = new List<char>();
            
            while (totalChar < upperLimit)
            {
                long lower = (long)Math.Pow(10, digits - 1);
                long upper = (long)Math.Pow(10, digits) - 1;

                for (long i = lower; i <= upper; i++)
                {
                    totalChar += digits;
                    if (totalChar > upperLimit)
                        break;

                    while (totalChar >= (long)(Math.Pow(10, pow - 1)))
                    {
                        if (totalChar == (long)(Math.Pow(10, pow - 1)))
                        {
                            nthDigits.Add(i.ToString()[digits - 1]);
                            pow++;
                        }
                        else if (totalChar > (long)(Math.Pow(10, pow - 1)) && totalChar - digits < (long)(Math.Pow(10, pow - 1)))
                        {
                            for (int x = 1; x < digits; x++)
                            {
                                if (totalChar - x == (long)(Math.Pow(10, pow - 1)))
                                {
                                    nthDigits.Add(i.ToString()[digits - 1 - x]);
                                    pow++;
                                    break;
                                }
                            }

                        }
                    }
                }
                digits++;
            }

            long product = 1;
            foreach (char c in nthDigits)
            { 
                product *= (c - '0');
            }

            return product.ToString();
        }
    }
}
