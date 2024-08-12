using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace EulerProject.ProblemCollection
{
    public class Problem231 : ProblemBase
    {
        public Problem231() : base() { }

        public override int ProblemNumber
        {
            get
            {
                return 231;
            }
        }
        private const long upperLimit = 20000000;
        public override string Description
        {
            get
            {
                return "Problem 231 - Prime Factorisation of Binomial Coefficients. The binomial coefficient c(10, 3) = 120. 120 = 2^3 * 3 * 5 = 2 * 2 * 2 * 3 * 5, and 2 + 2 + 2 + 3 + 5 = 14. So the sum of the terms in the prime factorisation of c(10, 3) is 14. Find the sum of the terms in the prime factorisation of c(20 000 000, 15 000 000).";
            }
        }
        public override string Solution1()
        {
            return Solution1(upperLimit).ToString();
        }

        List<long> primes;

        private long Solution1(long x)
        {
            primes = Utils.IntSieveOfEratosthenes((int)upperLimit);

            long c1 = 5000000;
            long c2 = upperLimit - c1;

            Console.WriteLine($"Calculate for {c1}");
            long sum_c1 = 0;
            foreach (long p in primes.Where(pp => pp <= c1))
                sum_c1 += PrimeFactorExpInPerm(c1, p);

            Console.WriteLine($"sum_c1 = {sum_c1}");

            Console.WriteLine($"Calculate for {c2}");
            long sum_c2 = 0;
            foreach (long p in primes.Where(pp => pp <= c2))
                sum_c2 += PrimeFactorExpInPerm(c2, p);
            Console.WriteLine($"sum_c2 = {sum_c2}");

            Console.WriteLine($"Calculate for {upperLimit}");
            long sum_upperlimit = 0;
            foreach (long p in primes.Where(pp => pp <= upperLimit))
                sum_upperlimit += PrimeFactorExpInPerm(upperLimit, p);
            Console.WriteLine($"sum_upperlimit = {sum_upperlimit}");

            long sum = sum_upperlimit - sum_c2 - sum_c1;
            return sum;
        }

        int PrimeFactorExpInPerm(long n, long p)
        {
            long sqrtN = (long)(Math.Sqrt(n));
            int exp = (int)(n / p);
            if (p <= sqrtN)
            {
                long l = p * p;
                while (l <= n)
                {
                    exp += (int)(n / l);
                    l *= p;
                }
            }

            return (int)(exp * p);
        }

    }
}
