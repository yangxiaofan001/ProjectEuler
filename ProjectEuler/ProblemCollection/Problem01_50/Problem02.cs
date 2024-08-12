﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EulerProject.ProblemCollection
{
    public class Problem2 : ProblemBase
    {
        public Problem2() : base() { }

        public override int ProblemNumber
        {
            get
            {
                return 2;
            }
        }
        private const long upperLimit = 4000000;
        public override string Description
        {
            get
            {
                return "Each new term in the Fibonacci sequence is generated by adding the previous two terms. " +
                    "By starting with 1 and 2, the first 10 terms will be:\n" +
                    "1, 2, 3, 5, 8, 13, 21, 34, 55, 89, … \n" +
                    "Find the sum of all the even-valued terms in the sequence which do not exceed " +
                    upperLimit.ToString() + " (4000000 in oiginal question).";
            }
        }

        public override string Solution1()
        {
            return Solution1(upperLimit).ToString();
        }

        private long Solution1(long x)
        {
            long sum = 0;

            long previousF = 1;
            long currentF = previousF + 1;

            while (currentF < x)
            {
                if (currentF % 2 == 0)
                    sum += currentF;

                long temp = currentF;
                currentF += previousF;
                previousF = temp;
            }

            return sum;
        }

        public override string Solution2()
        {
            return Solution2(upperLimit).ToString();
        }

        private long Solution2(long x)
        {
            // fib(3*(n+2)) = 4 * fib(3 * (n + 1)) + fib(3n);
            /* Proof:
             * fib(3 * (n +2))
             * = fib(3n + 6)
             * = fib(3n +5) + fib(3n + 4)
             * = fib(3n + 4) + fib(3n + 4) + fib(3n + 3)
             * = 2 X fib(3n + 4) + fib(3n + 3)
             * = 2 X (fib(3n + 3) + fib(3n + 2)) + fib(3n + 3)
             * = 3 X fib(3n + 3) + 2 X fib(3n + 2)
             * = 3 X fib(3n + 3) + fib(3n + 2) + fib(3n + 2)
             * = 3 X fib(3n + 3) + fib(3n + 2) + fib(3n + 1) + fib(3n)
             * = 3 X fib(3n + 3) + fib(3n + 3) + fib(3n)
             * = 4 X fib(3n + 3) + fib(3n)
           */
            long sum = 0;

            long fib3 = 2;
            long fib6 = 8;

            sum = fib3;

            while (fib6 < x)
            {
                sum += fib6;

                long temp = fib6;
                fib6 = 4 * fib6 + fib3;
                fib3 = temp;
            }

            return sum;
        }
    }
}
