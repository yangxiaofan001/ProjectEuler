using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EulerProject.ProblemCollection
{
    public class Problem5 : ProblemBase
    {
        public Problem5() : base() { }

        public override int ProblemNumber
        {
            get
            {
                return 5;
            }
        }
        private const long upperLimit = 20;
        public override string Description
        {
            get
            {
                return "2520 is the smallest number that can be divided by each of the numbers from 1 to 10 without any remainder.\n" +
                        "What is the smallest positive number that is evenly divisible (divisible with no remainder) by all of the numbers from 1 to " +
                        upperLimit.ToString() + " (20 in oiginal question)?";
            }
        }

        //public override string Solution1()
        //{
        //    return Solution1(upperLimit).ToString();
        //}

        //private long Solution1(long p)
        //{
        //    List<long> allPrimes = Utils.GetAllPrimeUnderP(p);
        //    List<long> testers = new List<long>();

        //    foreach (long prime in allPrimes)
        //    {
        //        long x = FindLargestProductUnderP(p, prime);
        //        testers.Add(x);
        //    }

        //    long production = p;
        //    while (true)
        //    {
        //        bool done = true;
        //        foreach (long tester in testers)
        //        {
        //            if (production % tester != 0)
        //            {
        //                done = false;
        //                break;
        //            }
        //        }
        //        if (done)
        //            break;
        //        else
        //            production += p;
        //    }

        //    return production;
        //}

        public override string Solution2()
        {
            return Solution2(upperLimit).ToString();
        }

        private long Solution2(long p)
        {
            long production = 1;
            List<long> numbersToUse = new List<long>();

            for (long i = 2; i <= p; i++)
            {
                if (Utils.IsPrime(i))
                {
                    long x = FindLargestProductUnderP(p, i);
                    if (!numbersToUse.Contains(x))
                    {
                        numbersToUse.Add(x);
                    }
                }
            }

            foreach (long number in numbersToUse) production *= number;

            return production;
        }

        private long FindLargestProductUnderP(long p, long i)
        {
            long number = i;

            while (number * i < p)
            {
                number *= i;
            }

            return number;
        }

        public override string Solution3()
        {
            return Solution3(upperLimit).ToString();
        }

        private long Solution3(long upperLimit)
        {
            long production = 1;

            for (long i = 2; i <= upperLimit; i++)
            {
                if (Utils.IsPrime(i))
                {
                    // x is the largest production of i that's less than upperLimit
                    // log(upperLimit) gets upperlimit is nth power of 2
                    // log(i) gets i is mth power of 2
                    // log(upperLimit) / log(i) gets upperLimit is pth power of i
                    // pow(i, p) gets the largest product of i that's less than upperLimit
                    long x = (long)(Math.Pow(i, (long)(Math.Log(upperLimit) / Math.Log(i))));
                    production *= x;
                }
            }

            return production;
        }
    }
}
