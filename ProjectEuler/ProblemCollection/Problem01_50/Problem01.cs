using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EulerProject.ProblemCollection
{
    public class Problem1 : ProblemBase
    {
        public Problem1() : base() { }

        public override int ProblemNumber
        {
            get
            {
                return 1;
            }
        }
        private const long upperLimit = 1000;
        public override string Description
        {
            get
            {
                return "Find the sum of all the multiples of 3 or 5 below " +
                    upperLimit.ToString() + " (1000 in oiginal question).";
            }
        }
        public override string Solution1()
        { 
            return Solution1(upperLimit).ToString(); 
        }

        private long Solution1(long x)
        {
            long sum = 0;
            for (long i = 1; i < x; i++)
            {
                if (i % 3 == 0 || i % 5 == 0)
                    sum += i;
            }

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
