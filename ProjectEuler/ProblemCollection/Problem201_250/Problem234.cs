using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection.Problem051_100
{
    public class Problem234 : ProblemBase
    {
        public override string Description
        {
            get
            {
                return @"
Problem 234 - Semidivisible Numbers

For an integer n >= 4, we define the lower prime square root of n, denoted by lps(n), as the largest prime <= sqr(n) and the upper prime square root of n, ups(n), as the smallest prime >= sqrt(n).

So, for example, lps(4) = 2 = ups(4), lps(1000) = 31, ups(1000) = 37.
Let us call an integer n >= 4 semidivisible, if one of lps(n) and ups(n) divides n, but not both.

The sum of the semidivisible numbers not exceeding 15 is 30, the numbers are 8, 10, and 12.

15 is not semidivisible because it is a multiple of both lps(15) = 3 and ups(15) = 5 and .
As a further example, the sum of the 92 semidivisible numbers up to 1000 is 34825.

What is the sum of all semidivisible numbers not exceeding 999966663333?
";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 234;
            }
        }

        long upperLimit = 999966663333;

        public override string Solution1()
        {
            List<long> primes;
            double sqrtOfUpperLimit = Math.Sqrt(upperLimit);
            if (sqrtOfUpperLimit > (long)sqrtOfUpperLimit)
            {
                // need 1 more prime number. assume there is alway at least 1 prime number between any n and 2n (assumption works for n = (long)(sqrt(999966663333)))
                primes = Utils.GetAllPrimeUnderP(2 * (long)sqrtOfUpperLimit);
                long maxPrime = primes.Where(p => p > sqrtOfUpperLimit).Min(p => p);
                primes = primes.Where (p => p <= maxPrime).ToList();
            }
            else
            {
                primes = Utils.GetAllPrimeUnderP((long)sqrtOfUpperLimit);
            }

            long sum = 0;
            int p1Index;
            int p2Index;
            for(p1Index = 0; p1Index <= primes.Count - 2; p1Index ++)
            {
                p2Index = p1Index + 1;
                 long lps = primes[p1Index];
                long ups = primes[p2Index];
                long a = lps * lps;
                long b = ups * ups;
                long t = b / a;

                long k = a + lps;
                while(k < b && k < upperLimit)
                {
                    if (k % ups > 0)
                        sum += k;

                    k+=lps;
                }

                k = b - ups;
                while (k > a && k < upperLimit)
                {
                    if (k % lps > 0)
                        sum += k;
                    k-=ups;
                }
            }

            string answer = sum.ToString();
            return answer;
        }

    }
}
