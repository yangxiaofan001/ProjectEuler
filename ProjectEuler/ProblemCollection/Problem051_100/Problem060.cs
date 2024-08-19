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

        int ConcateTwoNumbers(int a, int b)
        {
            int powerOf10 = 1;
            while (powerOf10 < b) powerOf10 *= 10;

            return a * powerOf10 + b;
        }

        private bool IsValidPair(int p1, int p2)
        {
            return primeChecker[ConcateTwoNumbers(p1, p2)] &&
                primeChecker[ConcateTwoNumbers(p2, p1)];
        }

        public override string Solution1()
        {

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
                // Console.WriteLine($"looking for sets of 5 within the limit of {limit}:");
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

            Console.WriteLine($"now we have a definite limit {limit}");

            setsOfFive = LookForSetsOfFive_sumlimitknown(limit);
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


            return sum.ToString();
        }

        private List<List<int>> LookForSetsOfFive(int limit)
        {
            List<List<int>> setsOfFive = new List<List<int>>();
            List<long> primesUnderLimits = primes.Where(p => p <= limit).ToList();
            Console.WriteLine($"calculating within limit of {limit}, {primesUnderLimits.Count} primes");
            try
            {

                for (int i1 = 0; i1 < primesUnderLimits.Count - 4; i1++)
                {
                    int p1 = (int)primesUnderLimits[i1];
                    // if (p1 >= limit / 5) 
                    //     break;
                    for (int i2 = i1 + 1; i2 < primesUnderLimits.Count - 3; i2++)
                    {
                        int p2 = (int)primesUnderLimits[i2];
                        // if (p2 >= (limit - p1) / 4) 
                        //     break;
                        if (!IsValidPair(p1, p2)) continue;
                        for (int i3 = i2 + 1; i3 < primesUnderLimits.Count - 2; i3++)
                        {
                            int p3 = (int)primesUnderLimits[i3];
                            // if (p3 >= (limit - p1 - p2) / 3) 
                            //     break;
                            if (!IsValidPair(p2, p3) || !IsValidPair(p1, p3)) continue;
                            for (int i4 = i3 + 1; i4 < primesUnderLimits.Count - 1; i4++)
                            {
                                int p4 = (int)primesUnderLimits[i4];
                                // if (p4 >= (limit - p1 - p2 - p3) / 2) 
                                //     break;
                                if (!IsValidPair(p1, p4) || !IsValidPair(p2, p4) || !IsValidPair(p3, p4)) continue;
                                for (int i5 = i4 + 1; i5 < primesUnderLimits.Count; i5++)
                                {
                                    int p5 = (int)primesUnderLimits[i5];
                                    // if (p5 >= (limit - p1 - p2 - p3 - p4)) 
                                    //     break;
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

        private List<List<int>> LookForSetsOfFive_sumlimitknown(int sumLimit)
        {
            List<List<int>> setsOfFive = new List<List<int>>();
            List<long> primesUnderLimits = primes.Where(p => p <= sumLimit).ToList();
            Console.WriteLine($"calculating within limit of {sumLimit}, {primesUnderLimits.Count} primes");
            try
            {

                for (int i1 = 0; i1 < primesUnderLimits.Count - 4; i1++)
                {
                    int p1 = (int)primesUnderLimits[i1];
                    if (p1 > sumLimit / 5)
                        break;
                    for (int i2 = i1 + 1; i2 < primesUnderLimits.Count - 3; i2++)
                    {
                        int p2 = (int)primesUnderLimits[i2];
                        if (p2 > (sumLimit - p1) / 4)
                            break;
                        if (!IsValidPair(p1, p2)) continue;
                        for (int i3 = i2 + 1; i3 < primesUnderLimits.Count - 2; i3++)
                        {
                            int p3 = (int)primesUnderLimits[i3];
                            if (p3 > (sumLimit - p1 - p2) / 3)
                                break;
                            if (!IsValidPair(p2, p3) || !IsValidPair(p1, p3)) continue;
                            for (int i4 = i3 + 1; i4 < primesUnderLimits.Count - 1; i4++)
                            {
                                int p4 = (int)primesUnderLimits[i4];
                                if (p4 > (sumLimit - p1 - p2 - p3) / 2)
                                    break;
                                if (!IsValidPair(p1, p4) || !IsValidPair(p2, p4) || !IsValidPair(p3, p4)) continue;
                                for (int i5 = i4 + 1; i5 < primesUnderLimits.Count; i5++)
                                {
                                    int p5 = (int)primesUnderLimits[i5];
                                    if (p5 > (sumLimit - p1 - p2 - p3 - p4))
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