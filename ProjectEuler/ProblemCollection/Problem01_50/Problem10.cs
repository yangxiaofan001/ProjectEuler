using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EulerProject.ProblemCollection
{
    public class Problem10 : ProblemBase
    {
        const long upperLimit = 2000000;

        public override string Description
        {
            get
            {
                return "The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.\n" +
                        "Find the sum of all the primes below " + upperLimit.ToString() + "(two million in the original question).";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 10;
            }
        }

        public override string Solution1()
        {
            // Brutal force
            long sum = 0;
            for (long i = 2; i <= upperLimit; i++)
            {
                if (Utils.IsPrime(i))
                    sum += i;
            }
            return sum.ToString();
        }

        public override string Solution2()
        {
            // Sieve of Eratosthenes
            long sum = 0;
            System.Collections.BitArray allBits = new System.Collections.BitArray((int)upperLimit);

            for (int i = 0; i < allBits.Length; i++) allBits[i] = true;
            allBits[0] = false;

            for (int i = 2; i <= Math.Sqrt(upperLimit); i++)
            {
                if (Utils.IsPrime(i))
                {
                    for (int j = 2 * i - 1; j < allBits.Length; j += i)
                    {
                        allBits[j] = false;
                    }
                }
            }
            for (int i = 0; i < allBits.Length; i++)
            {
                if (allBits[i]) sum += i+1;
            }

            return sum.ToString();
        }
    }
}
