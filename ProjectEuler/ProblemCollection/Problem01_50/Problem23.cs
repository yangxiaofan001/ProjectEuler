using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem23 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 23;
            }
        }

        public override string Description
        {
            get
            {
                return @"Non-abundant sums
                    Problem 23
                    A perfect number is a number for which the sum of its proper divisors is exactly equal to the number. For example, the sum of the proper divisors of 28 would be 1 + 2 + 4 + 7 + 14 = 28, which means that 28 is a perfect number.

                    A number n is called deficient if the sum of its proper divisors is less than n and it is called abundant if this sum exceeds n.

                    As 12 is the smallest abundant number, 1 + 2 + 3 + 4 + 6 = 16, the smallest number that can be written as the sum of two abundant numbers is 24. By mathematical analysis, it can be shown that all integers greater than 28123 can be written as the sum of two abundant numbers. However, this upper limit cannot be reduced any further by analysis even though it is known that the greatest number that cannot be expressed as the sum of two abundant numbers is less than this limit.

                    Find the sum of all the positive integers which cannot be written as the sum of two abundant numbers.";
            }
        }

        public override string Solution1()
        {
            DateTime dtStart = DateTime.Now;
            List<long> abundantNumberList = new List<long>();

            for (long i = 12; i <= 28123; i++)
            {
                if (Utils.SumOfProperDivisors(i) > i)
                    abundantNumberList.Add(i);
            }

            bool[] sumOfTwoAbundantNumbersList = new bool[28123];
            int abundantNumberListCount = abundantNumberList.Count;
            for (int i = 0; i < abundantNumberListCount; i++)
            {
                if (abundantNumberList[i] > 14061) 
                    break;

                for (int j = i; j < abundantNumberListCount; j++)
                {
                    long sumOfTwoNumbers = abundantNumberList[i] + abundantNumberList[j];

                    if (sumOfTwoNumbers <= 28123)
                        sumOfTwoAbundantNumbersList[sumOfTwoNumbers - 1] = true;
                    else
                        break;
                }
            }

            long sum = 28123 * 28124 / 2;
            for (int i = 1; i <= 28123; i++)
            {
                if (sumOfTwoAbundantNumbersList[i - 1])
                    sum -= i;
            }

            return sum.ToString();
        }
    }
}
