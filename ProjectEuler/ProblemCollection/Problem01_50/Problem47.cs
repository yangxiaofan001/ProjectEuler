using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem47 : ProblemBase
    {
        public override string Description
        {
            get
            {
                return @"
                    Distinct primes factors
                    Problem 47
                    The first two consecutive numbers to have two distinct prime factors are:

                    14 = 2 × 7
                    15 = 3 × 5

                    The first three consecutive numbers to have three distinct prime factors are:

                    644 = 2² × 7 × 23
                    645 = 3 × 5 × 43
                    646 = 2 × 17 × 19.

                    Find the first four consecutive integers to have four distinct prime factors each. 
                    What is the first of these numbers?
                    ";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 47;
            }
        }

        List<long> primeList = Utils.IntSieveOfEratosthenes((int)(Math.Sqrt(9999999)));

        public override string Solution1()
        {
            int answer = 0;
            int i = 1000;

            int i1 = i;
            int i2 = i1 + 1;
            int i3 = i1 + 2;
            int i4 = i1 + 3;


            for (; ; )
            { 

                if (!Utils.IsPrime(i1))
                {
                    if (!Utils.IsPrime(i2))
                    {
                        if (!Utils.IsPrime(i3))
                        {
                            if (!Utils.IsPrime(i4))
                            {
                                if (GetDistinctPrimeFactors(i1).Count == 4)
                                {
                                    if (GetDistinctPrimeFactors(i2).Count == 4)
                                    {
                                        if (GetDistinctPrimeFactors(i3).Count == 4)
                                        {
                                            if (GetDistinctPrimeFactors(i4).Count == 4)
                                            {
                                                answer = i1;
                                                break;
                                            }
                                        }
                                    }
                                }

                                i1++;
                            }
                            else
                                i1 = i4 + 1;
                        }
                        else
                            i1 = i4;
                    }
                    else
                        i1 = i3;
                }
                else
                    i1 = i2;

                i2 = i1 + 1;
                i3 = i1 + 2;
                i4 = i1 + 3;
            }


            return answer.ToString();
        }

        private List<int> GetDistinctPrimeFactors(int n)
        {
            List<int> retList = new List<int>();

            for (int i = 0; i < primeList.Count; i++)
            {
                if (n <= 3) break;

                int primeNumber = (int)(primeList[i]);
                if (n % primeNumber > 0) continue;

                while (n % primeNumber == 0)
                    n /= primeNumber;

                retList.Add(primeNumber);
            }

            //for (int p = 2; ; p++)
            //{
            //    if (!primeList.Contains(p))
            //    {
            //        if (!Utils.IsPrime(p))
            //            continue;
            //        else
            //            primeList.Add(p);
            //    }


            //    if (n <= 3) break;

            //    if (n % p > 0) continue;

            //    while (n % p == 0)
            //        n /= p;

            //    retList.Add(p);
            //}

            return retList;
        }
    }
}
