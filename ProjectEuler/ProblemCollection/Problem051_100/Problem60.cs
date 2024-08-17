using System;
using System.Collections.Generic;
using System.Data;
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

        List<List<long>> FindSets(int setmemberCount, List<long> primes)
        {
            List<List<long>> listOfSets = new List<List<long>>();
            if (setmemberCount < 1) throw new InvalidDataException("set contains at least one prime");
            if (setmemberCount == 1) 
            {
                foreach(long prime in primes)
                {
                    if (prime != 2 && prime != 5)
                        listOfSets.Add(new List<long>{prime});
                }

                return listOfSets;
            }

            List<List<long>> listOfSmallerSets = FindSets(setmemberCount - 1, primes);
            foreach(List<long> s in listOfSmallerSets)
            {
                foreach(long p in primes)
                {
                    if (p ==2 || p == 5) continue;
                    // if (s.Contains(p)) continue;
                    bool bIsValid = true;
                    foreach (long pInS in s)
                    {
                        if (p <= pInS || !IsValidPair(p, pInS))
                        {
                            bIsValid = false;
                            break;
                        }
                    }

                    if (bIsValid)
                    {
                        List<long> newSet = new List<long>();
                        newSet.AddRange(s);
                        newSet.Add(p);
                        listOfSets.Add(newSet);
                    }
                }
            }

            return listOfSets;
        }

        private bool IsValidPair(long p1, long p2)
        {
            return Utils.IsPrime(ConcateTwoNumbers(p1, p2)) 
                && Utils.IsPrime(ConcateTwoNumbers(p2, p1)) ;
        }

        long ConcateTwoNumbers(long a, long b)
        {
            int powerOf10 = 1;
            while(powerOf10 < b) powerOf10 *= 10;

            return a * powerOf10 + b;
        }
        public override string Solution1()
        {
            int upperLimit = 10000;
            int increament = 5000;
            List<List<long>> listOfSets = new List<List<long>>();
            while(listOfSets.Count == 0)
            {
                List<long> primes = Utils.IntSieveOfEratosthenes(upperLimit);
                listOfSets = FindSets(5, primes);

                Console.WriteLine($"{upperLimit}: {listOfSets.Count}");
                if (listOfSets.Count > 0)
                {
                    for(int i = 0; i < Math.Min(1000, listOfSets.Count); i++)
                    {
                        foreach(long l in listOfSets[i])
                        Console.Write($"{l} ");
                        Console.WriteLine();
                    }
                }

                upperLimit += increament;
            }

            long sum = 0;
            foreach(long l in listOfSets[0])
                sum += l;

            string answer = sum.ToString();

            return answer;
        }
    }
}
