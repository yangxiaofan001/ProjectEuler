using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection.Problem051_100
{
    public class Problem060 : ProblemBase
    {
        public override string Description
        {
            get
            {
                return @"
Problem 60 - Prime Pair Sets

The primes 3, 7, 109, and 673, are quite remarkable. By taking any two primes and concatenating them in any order the result will always be prime. For example, taking 7 and 109, both 7109 and 1097 are prime. The sum of these four primes, 792, represents the lowest sum for a set of four primes with this property.

Find the lowest sum for a set of five primes for which any two primes concatenate to produce another prime.
";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 60;
            }
        }

        List<long> primes;
        List<bool> primeChecker;

        long ConcateTwoNumbers(long a, long b)
        {
            int powerOf10 = 1;
            while (powerOf10 < b) powerOf10 *= 10;

            return a * powerOf10 + b;
        }

        private bool IsValidPair(long p1, long p2)
        {
            long p1p2 = ConcateTwoNumbers(p1, p2);
            long p2p1 = ConcateTwoNumbers(p2, p1);
            try
            {
                bool isp1p2Prime = p1p2 < primeChecker.Count ? primeChecker[(int)p1p2] : Utils.IsPrime(p1p2);
                bool isp2p1Prime = p2p1 < primeChecker.Count ? primeChecker[(int)p2p1] : Utils.IsPrime(p2p1);
                return isp1p2Prime && isp2p1Prime;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"{p1} {p2} {ex.Message}");
                throw;
            }



        }

        public override string Solution1()
        {
            string idea = @"
from the question, upperlimit is unknown, check starts with upperlimit = 1000, the increase the upperlimit by 1000 until a solution is found.

1. prepare all prime numbers under 10^8 - here is still an assumption that the solutionn is under 10^8, may need to extend.
2. init upperlimit as 1000, loop until a solution is found. In each loop, increase the upperlimit by 1000
    loop through all primes under limit except for the 4 largest - p1
        loop through all primes under limit except for the 3 largest - p2 > p1, verify p1p2 and p2p1 are both prime
            loop through all primes under limit except for the 2largest - p3 > p2, verify p1p3, p3p1, p2p3, and p3p2 are both prime
                loop through all primes under limit except for the 1 largest - p4 > p3, verify p1p4, p4p1, p2p4, p4p2, p3p4 and p4p3 are all prime
                    loop through all primes under limit - p5 > p4, verify p1p5, p5p1, p2p5, p5p2, p3p5, p5p3, p4p5 and p5p4 are all prime
                        return set of 5
3. now we have a set, not neccessarily the set with smallest sum
4. repeat step 2, only return the set with a sum smaller
            ";

            Console.WriteLine(idea);

            primes = new List<long>();
            primeChecker = new List<bool>();

            Console.WriteLine("Preparing primes under 10^8.");
            Utils.SieveOfEratosthenes((int)Math.Pow(10, 8), ref primes, ref primeChecker);
            Console.WriteLine($"Primes under 10^8 are ready to use, {primes.Count} of them");

            primes.Remove(2);
            primes.Remove(5);

            int limit = 0;
            List<List<int>> setsOfFive = new List<List<int>>();

            while (setsOfFive.Count == 0)
            {
                limit += 1000;
                setsOfFive = LookForSetsOfFive(limit);
            }

            int sum = int.MaxValue;
            foreach (List<int> set in setsOfFive)
            {
                int setSum = 0;
                foreach (int n in set)
                {
                    Console.Write($"{n} ");
                    setSum += n;
                }
                Console.WriteLine($": {setSum}");
                sum = Math.Min(sum, setSum);
            }

            limit = sum;

            Console.WriteLine($"now we have a definite limit {limit}, the sum should not exceed this number either");

            setsOfFive = LookForSetsOfFive(limit, true);
            if (setsOfFive.Count == 0)
            {
                Console.WriteLine($"No new set of five primes were found. The answer is {limit}");
                sum = limit;
            }
            else
            {
                Console.WriteLine($"New set(s) of five primes were found.");

                sum = int.MaxValue;
                foreach (List<int> set in setsOfFive)
                {
                    int setSum = 0;
                    foreach (int n in set)
                    {
                        Console.Write($"{n} ");
                        setSum += n;
                    }
                    Console.WriteLine($": {setSum}");
                    sum = Math.Min(sum, setSum);
                }

                Console.WriteLine($"The answer is {sum}");
            }

            return sum.ToString();
        }

        private List<List<int>> LookForSetsOfFive(long limit, bool subLimitKnown = false)
        {
            List<List<int>> setsOfFive = new List<List<int>>();
            List<long> primesUnderLimits = primes.Where(p => p <= limit).ToList();
            Console.WriteLine($"calculating within limit of {limit}, {primesUnderLimits.Count} primes");
            try
            {

                for (int i1 = 0; i1 < primesUnderLimits.Count - 4; i1++)
                {
                    int p1 = (int)primesUnderLimits[i1];
                    if (subLimitKnown && p1 >= limit / 5)
                        break;
                    for (int i2 = i1 + 1; i2 < primesUnderLimits.Count - 3; i2++)
                    {
                        int p2 = (int)primesUnderLimits[i2];
                        if (subLimitKnown && p2 >= (limit - p1) / 4)
                            break;
                        if (!IsValidPair(p1, p2)) continue;
                        for (int i3 = i2 + 1; i3 < primesUnderLimits.Count - 2; i3++)
                        {
                            int p3 = (int)primesUnderLimits[i3];
                            if (subLimitKnown && p3 >= (limit - p1 - p2) / 3)
                                break;
                            if (!IsValidPair(p2, p3) || !IsValidPair(p1, p3)) continue;
                            for (int i4 = i3 + 1; i4 < primesUnderLimits.Count - 1; i4++)
                            {
                                int p4 = (int)primesUnderLimits[i4];
                                if (subLimitKnown && p4 >= (limit - p1 - p2 - p3) / 2)
                                    break;
                                if (!IsValidPair(p1, p4) || !IsValidPair(p2, p4) || !IsValidPair(p3, p4)) continue;
                                for (int i5 = i4 + 1; i5 < primesUnderLimits.Count; i5++)
                                {
                                    int p5 = (int)primesUnderLimits[i5];
                                    if (subLimitKnown && p5 >= (limit - p1 - p2 - p3 - p4))
                                        break;
                                    if (IsValidPair(p1, p5) && IsValidPair(p2, p5) && IsValidPair(p3, p5) && IsValidPair(p4, p5))
                                    {
                                        setsOfFive.Add(new List<int> { p1, p2, p3, p4, p5 });
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                return setsOfFive;
            }

            return setsOfFive;
        }
    }
}