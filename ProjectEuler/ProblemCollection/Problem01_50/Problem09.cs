using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EulerProject.ProblemCollection
{
    public class Problem9 : ProblemBase
    {
        long aNumber = 1000;
        public override string Description
        {
            get
            {
                return
                    "A Pythagorean triplet is a set of three natural numbers, a < b < c, for which, \n" +
                    "a^2 + b^2 = c^2\n" +
                    "For example, 3^2 + 4^2 = 9 + 16 = 25 = 5^2.\n" +
                    "Find the product abc of " + aNumber.ToString() + 
                    "(original question reads: There exists exactly one Pythagorean triplet for which a + b + c = 1000. Find the product abc of 1000)";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 9;
            }
        }

        public override string Solution1()
        {
            List<string> answers = new List<string>();

            long maxA = aNumber / 3 - 1;
            long maxB = aNumber / 2 - 1;

            for (long a = 1; a <= maxA; a++)
            {
                for (long b = a + 1; b <= maxB; b++)
                {
                    long c = aNumber - a - b;
                    if (c * c == a * a + b * b)
                        answers.Add(a.ToString() + "^2 + " + b.ToString() + "^2 = " + c.ToString() + "^2; " + "abc = " + (a * b * c).ToString());
                }
            }

            if (answers.Count == 0)
            {
                return " no answer ";
            }
            else
            {
                string ret = "\n";
                foreach (string answer in answers)
                    ret = ret + answer + "\n";

                return ret;
            }
        }

        public override string Solution2()
        { 
            // Find m, n, d where
            // m and n are coprime
            // m + n is odd
            // 2m(m +n) * d = aNumber
            // then calculate a, b, c
            // a = m^2 - n^2
            // b = 2 * m * n
            // c = m^2 + n^2

            List<string> answers = new List<string>();

            long upperM = (long)(Math.Sqrt(aNumber / 2));

            // m is between 1 and sqrt(aNumber / 2)
            // d is between aNumber / 4m^2 and aNumber / 2m^2

            for (long m = 2; m <= upperM; m++)
            {
                if (aNumber / 2 % m > 0) continue;

                double doubleLowerD = (double)aNumber / 4 / m / m;
                long lowerD = (long)(Math.Ceiling(doubleLowerD));
                
                double doubleUpperD = (double)aNumber / 2 / m / m;
                long upperD = (long)(Math.Floor(doubleUpperD));

                long d = lowerD;
                
                d = lowerD;
                for (;d <= upperD; d++)
                {
                    if (aNumber / 2 / m % d == 0)
                    {
                        long k = aNumber / 2 / m / d;

                        if (k % 2 == 0) continue;
                        if (!coprime(m, k)) continue;

                        long n = k - m;
                        long a = d*(m * m - n * n);
                        long b = d*(2 * m * n);
                        long c = d*(m * m + n * n);

                        answers.Add(a.ToString() + "^2 + " + b.ToString() + "^2 = " + c.ToString() + "^2; " + "abc = " + (a * b * c).ToString());
                    }
                }
            }


            if (answers.Count == 0)
            {
                return " no answer ";
            }
            else
            {
                string ret = "\n";
                foreach (string answer in answers)
                    ret = ret + answer + "\n";

                return ret;
            }

        }

        private bool coprime(long m, long k)
        {
            for (long x = 2; x <= Math.Min(m, k); x++)
            {
                if (m % x == 0 && k % x == 0)
                    return false;
            }

            return true;
        }

        public override string Solution3()
        {
            if (aNumber % 2 == 1) return "no answer.";
            List<string> answers = new List<string>();
            List<long> allDivisors = Utils.AllDivisors(aNumber / 2);

            foreach (long d in allDivisors)
            {
                if (d < 6) continue;

                for (long m = 2; m < Math.Sqrt(d); m++)
                {
                    for (long n = m % 2 + 1; n < m; n += 2)
                    {
                        if (m * (m + n) == d && coprime(m, n))
                        {
                            long dd = aNumber / 2 / d;
                            long a = dd * (m * m - n * n);
                            long b = dd * (2 * m * n);
                            long c = dd * (m * m + n * n);

                            answers.Add(a.ToString() + "^2 + " + b.ToString() + "^2 = " + c.ToString() + "^2; " + "abc = " + (a * b * c).ToString());
                        }
                    }
                }
            }

            if (answers.Count == 0)
            {
                return " no answer ";
            }
            else
            {
                string ret = "\n";
                foreach (string answer in answers)
                    ret = ret + answer + "\n";

                return ret;
            }

        }

        

    }
}
