using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EulerProject.ProblemCollection
{
    public class Problem3 : ProblemBase
    {
        public Problem3() : base() { }

        public override int ProblemNumber
        {
            get
            {
                return 3;
            }
        }
        private const long upperLimit = 600851475143;
        public override string Description
        {
            get
            {
                return "The prime factors of 13195 are 5, 7, 13 and 29. \n" +
                    "What is the largest prime factor of the number " +
                    upperLimit.ToString() + " (600851475143 in oiginal question)?";
            }
        }

        public override string Solution1()
        {
            return Solution1(upperLimit).ToString();
        }

        private long Solution1(long x)
        {
            long upper = (long)(Math.Sqrt(x));

            for (long i = upper; i >= 1; i--)
            {
                if ((x % i == 0) && Utils.IsPrime(i))
                    return i;
            }

            return 1;
        }


        public override string Solution2()
        {
            return Solution2(upperLimit).ToString();
        }

        private long Solution2(long x)
        {
            long newNumber = x;
            long counter = 2;

            long largestPrime = 1;

            // Starting from x =2
            // devide the current number by x, until it cannot be devided by x
            // increase x by 1
            // devide the current number by x, until it cannot be devided by x
            // when x = 4, current number will not be able to be devided by x
            // loop until the remainder is also a prime

            while (counter * counter <= newNumber)
            {
                if (newNumber % counter == 0)
                {
                    newNumber /= counter;
                    largestPrime = counter;
                }
                else
                    counter++;
            }

            return Math.Max(newNumber, largestPrime);
        }
    }
}
