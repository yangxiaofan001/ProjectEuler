using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EulerProject.ProblemCollection
{
    public class Problem7 : ProblemBase
    {
        private const long upperLimit = 10001;
        public override string Description
        {
            get
            {
                return "By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13.\n" +
                    "What is the " + upperLimit.ToString() + " (10001st in the original question) prime number?";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 7;
            }
        }

        public override string Solution1()
        {
            return Solution1(upperLimit).ToString();
        }

        private object Solution1(long upperLimit)
        {
            int i = 1;
            int p = 2;

            while (i < upperLimit)
            {
                p++;
                if (Utils.IsPrime(p))
                    i++;
            }

            return p;
        }

        public override string Solution2()
        {
            return Solution2(upperLimit).ToString();
        }

        private object Solution2(long upperLimit)
        {
            int i = 2;
            int p = 3;

            while (i < upperLimit)
            {
                p+= 2;
                if (Utils.IsPrime(p))
                    i++;
            }

            return p;
        }

        public override string Solution3()
        {
            return Solution3(upperLimit).ToString();
        }

        private long Solution3(long upperLimit)
        {
            List<long> allPrimes = new List<long>() { 2 };
            long p = 3;

            while (allPrimes.Count < upperLimit)
            {
                bool isPrime = true;

                foreach (long prime in allPrimes)
                {
                    if (prime * prime <= p && p % prime == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                    allPrimes.Add(p);

                p += 2;
            }

            return allPrimes[allPrimes.Count - 1];
        }

        public override string Solution4()
        {
            return Solution4(upperLimit).ToString();
        }

        private long Solution4(long upperLimit)
        {
            List<long> allPrimes = Utils.AllPrimesUnder2Million_CheatSheet();

            if (allPrimes.Count >= upperLimit)
                return allPrimes[(int)upperLimit - 1];

            return -1;
        }

        public override string Solution5()
        {
            return Solution5(upperLimit).ToString();
        }

        private long Solution5(long upperLimit)
        {
            System.Collections.BitArray allBits = new System.Collections.BitArray(2000000);

            // intialize - set all bit to 1, except the first bit, cause 1 is not a prime number
            allBits[0] = false;
            for (int i = 1; i < allBits.Length; i++)
            {
                allBits[i] = true;
            }

            int smallestPrime = 2;
            while (smallestPrime <= Math.Floor(Math.Sqrt(2000000)))
            {
                // exclude the numbers
                for (int i = smallestPrime; i < allBits.Length; i++)
                {
                    if (allBits[i] && (i + 1) % smallestPrime == 0)
                        allBits[i] = false;
                }

                // find next prime number
                int nextPrime = -1;
                for (int j = smallestPrime; j < Math.Floor(Math.Sqrt(2000000)); j++)
                {
                    if (allBits[j] == true)
                    {
                        nextPrime = j + 1;
                        break;
                    }
                }

                if (nextPrime != -1)
                {
                    // nextPrime is still less than sqrt(p), next loop
                    smallestPrime = nextPrime;
                }
                else
                {
                    // we are done, all the 1s left are what we need.
                    break;
                }
            }

            int ii = 0;
            int index = 0;
            long answer = 0;

            while (ii < upperLimit)
            {
                if (allBits[index])
                {
                    answer = index + 1;
                    ii++;
                }
                index ++;
            }

            return answer;
        }


    }
}
