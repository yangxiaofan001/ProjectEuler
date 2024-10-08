﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem21 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 21;
            }
        }

        public override string Description
        {
            get
            {
                return @"Amicable numbers
                    Problem 21
                    Let d(n) be defined as the sum of proper divisors of n (numbers less than n which divide evenly into n).
                    If d(a) = b and d(b) = a, where a ≠ b, then a and b are an amicable pair and each of a and b are called amicable numbers.

                    For example, the proper divisors of 220 are 1, 2, 4, 5, 10, 11, 20, 22, 44, 55 and 110; therefore d(220) = 284. The proper divisors of 284 are 1, 2, 4, 71 and 142; so d(284) = 220.

                    Evaluate the sum of all the amicable numbers under 10000.";
            }
        }

        long upperLimit = 10000;

        public override string Solution1()
        {
            List<string> AmicableList = new List<string>();
            long grandTotal = 0;
            for (long i = 1; i <= upperLimit; i++)
            {
                long sum = Utils.SumOfProperDivisors(i);
                if (sum > i)
                {
                    if (Utils.SumOfProperDivisors(sum) == i)
                    {
                        grandTotal = grandTotal + i + sum;
                        AmicableList.Add(i.ToString() + " - " + sum.ToString());
                    }
                }
            }

            foreach (string amicableNumber in AmicableList)
                Console.WriteLine(amicableNumber);

            return grandTotal.ToString();
        }
    }
}
