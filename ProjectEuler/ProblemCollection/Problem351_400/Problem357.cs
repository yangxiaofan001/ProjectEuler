using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace EulerProject.ProblemCollection
{
    public class Problem357 : ProblemBase
    {
        public Problem357() : base() { }

        public override int ProblemNumber
        {
            get
            {
                return 357;
            }
        }
        private const long upperLimit = 100000000;
        public override string Description
        {
            get
            {
                return "Consider the divisors of 30: 1, 2, 3, 5, 6, 10, 15, 30.\nIt can be seen that for every divisor d of 30, d + 30 / d is prime.\n\nFind the sum of all positive integers n not exceeding " +
                    
                    upperLimit.ToString() + " such that for every divisor d of n, d + n / d is prime.";
            }
        }
        public override string Solution1()
        { 
            return Solution1(upperLimit).ToString(); 
        }

        List<long> primes;
        List<bool> primeChecker;

        long CalcSum(List<long> primeFactorList, int lastPrimeIndex)
        {
            long prod = 1;
            foreach (long l in primeFactorList) prod *= l;
            if (prod > upperLimit) return 0;

            long sum = 0;

            int pIndex = lastPrimeIndex + 1;
            while(pIndex < primes.Count) 
            { 
                long nextPrime = primes[pIndex];
                long tryingNumber = prod * nextPrime;
                if (tryingNumber < upperLimit)
                {
                    List<long> tryingPrimeList = new List<long>();
                    tryingPrimeList.AddRange(primeFactorList);
                    tryingPrimeList.Add(nextPrime);
                    if (IsValidNumber(tryingPrimeList, tryingNumber))
                        sum += tryingNumber;

                    sum += CalcSum(tryingPrimeList, pIndex);
                }
                else
                    break;
                pIndex++;
            }

            return sum;
        }

        private bool IsValidNumber(List<long> tryingPrimeList, long tryingNumber)
        {
            executeCount++;
            bool bRet = true;
            for(int i10 = 0; i10 < (long)(Math.Pow(2, tryingPrimeList.Count - 1)); i10++)
            {
                List<bool> i2 = IntToBoolArray(i10, tryingPrimeList.Count);
                long prod0 = 1;
                long prod1 = 1;
                for (int bIndex = 0; bIndex < i2.Count; bIndex ++)
                {
                    if (i2[bIndex]) 
                        prod1 *= tryingPrimeList[bIndex];
                    else
                        prod0 *= tryingPrimeList[bIndex];
                }

                bRet = bRet && primeChecker[(int)(prod0 + prod1)];
            }

            if (bRet)
            {
                validNumberCount++;
                validNumbers.Add(tryingNumber);
            }

            return bRet;
        }

        long executeCount;
        long validNumberCount;
        List<long> validNumbers;
        private List<bool> IntToBoolArray(int i10, int paddingCount)
        {
            List<bool> list = new List<bool>();
            int i = i10;
            while(i > 0)
            {
                list.Insert(0, (i%2==1));
                i = i / 2;
            }

            int lCount = list.Count;
            for(int r = 0; r < paddingCount - lCount; r ++)
                list.Insert(0, false);

            return list;
        }

        private long Solution1(long x)
        {
            executeCount = 0;
            validNumberCount = 0;
            validNumbers = new List<long>();
            Console.WriteLine("IntSieveOfEratosthenes...");
            primes = Utils.IntSieveOfEratosthenes((int)x / 2);
            Console.WriteLine("BoolSieveOfEratosthenes...");
            primeChecker = Utils.BoolSieveOfEratosthenes((int)x);
            Console.WriteLine("Preparation completed");

            // valid numbers are 1, 2, and
            // 2p1p2...p9, where p9 > p8 > p7 > p5 > p4 > p3 > p2 > p1 > 2
            List<long> primeFactorList = new List<long> { 2 };
            int pIndex = 0;
            long sum = CalcSum(primeFactorList, pIndex) + 2 + 1;

            Console.WriteLine($"IsValidNumber was executed {executeCount} times, there are a total of {validNumberCount} valid numbers.");
            for(int i = 0; i < 100; i ++)
            {
                Console.Write($"{validNumbers[i]} ");
            }
            Console.WriteLine();

            return sum;
        }

        public override string Solution2()
        {
            return Solution2(upperLimit).ToString();
        }

        private long Solution2(long x)
        {
            long numberof3s = (long)((x - 1) / 3);
            long numberof5s = (long)((x - 1) / 5);
            long numberof15s = (long)((x - 1) / 15);

            long sumof3s = 3 * numberof3s * (numberof3s + 1) / 2;
            long sumof5s = 5 * numberof5s * (numberof5s + 1) / 2;
            long sumof15s = 15 * numberof15s * (numberof15s + 1) / 2;

            return sumof3s + sumof5s - sumof15s;
        }
    }
}
