using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem48 : ProblemBase
    {
        public override string Description
        {
            get
            {
                return @"
                    Self powers
                    Problem 48
                    The series, 11 + 22 + 33 + ... + 1010 = 10405071317.

                    Find the last ten digits of the series, 11 + 22 + 33 + ... + 10001000.
                    ";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 48;
            }
        }

        public override string Solution1()
        {
            long max = 10000000000;
            long sum = 0;

            for (long i = 1; i <= 1000; i++)
            {
                long y = i;
                for (long p = 1; p < i; p++)
                {
                    y *= i;
                    if (y > max)
                        y = y % max;
                }

                sum += y;
                if (sum > max)
                    sum = sum % max;
            }
                
            return sum.ToString();
        }
    }
}
