using System;
using System.Collections.Generic;

namespace EulerProject.ProblemCollection.Problem051_100
{
    public class Problem064 : ProblemBase
    {
        public override string Description
        {
            get
            {
                return @"
Problem 64 - Odd Period Square Roots

All square roots are periodic when written as continued fractions and can be written in the form:

sqrt(N) = a0 + 1 / (a1 + 1 / (a2 + 1 / (a3 + 1 / (a4 + ....))))

For example, let us consider sqrt(23)
sqrt(23) = 4 + 1 / (1 + 1/ (3 + 1 / (1 + 1 / (8 + ....))))

The process can be summarised as follows:
    sqrt(23) = a0 + f0
    a0 = (int)(sqrt(23)) = 4
    f0 = s23 - 4

    a1 + f1 = 1 / f0 = 1 / (s23 - 4) = (s23 + 4) / 7
    a1 = (int)((s23 + 4) / 7) = (int)((a0 + 4) / 7) = 1
    f1 = (s23 + 4) / 7 - a1 = (s23 + 4) / 7 - 1 = (s23 - 3) / 7

    a2 + f2 = 1 / f1 = 7 / (s23 - 3) = 7 (s23 + 3) / 14 = (s23 + 3) / 2
    a2 = (int)((s23 + 3) / 2) = (int)((a0 + 3) / 2) = 3
    f2 = (s23 + 3) / 2 - a2 = (s23 + 3) / 2 - 3 = (s23 - 3) / 2

    a3 + f3 = 1 / f2 = 2 / (s23 - 3) = 2(s23 + 3) / 14 = (s23 + 3) / 7
    a3 = (int)((s323 + 3) / 7) = (int)((a0 + 3) / 7) = 1
    f3 = (s23 + 3) / 7 - a3 = (s23 + 3) / 7 - 1 = (s23 - 4) / 7

    a4 + f4 = 1 / f3 = 7 / (s23 - 4) = 7 (s23 + 4) / 7 = s23 + 4
    a4 = (int)(s23 + 4) = (int)(a0 + 4) = 8
    f4 = s23 + 4 - a4 = s23 + 4 - 8 = s23 - 4       *** equal f0, starts looping

It can be seen that the sequence is repeating. For conciseness, we use the notation sqrt(23) = [4;(1, 3, 1, 8)], to indicate that the block (1,3,1,8) repeats indefinitely.

The first ten continued fraction representations of (irrational) square roots are:
    sqrt(2) = [1; (2)], period = 1
    sqrt(3) = [1; (1, 2)], period = 2
    sqrt(5) = [2; (4)], period = 1
    sqrt(6) = [2; (2, 4)], period = 2
    sqrt(7) = [2; (1, 1, 1, 4)], period = 4
    sqrt(8) = [2; (1, 4)], period = 2
    sqrt(10) = [3; (6)], period = 1
    sqrt(11) = [3; (3, 6)], period = 2
    sqrt(12) = [3; (2, 6)], period = 2
    sqrt(13) = [3; (1, 1, 1, 1, 6)], period = 5

Exactly four continued fractions, for N <= 13, have an odd period.    

How many continued fractions for N <= 10000 have an odd period?
";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 64;
            }
        }

        public override string Solution1()
        {
            string answer = @"
Learning Square Root Continued Fraction...
";
            Console.WriteLine(answer);

            CalcSqrt(23, 4);

            return answer;
        }

        void CalcSqrt(int N, int a0)
        {
            Console.WriteLine($"sqrt({N}) = {a0} + f0");
            Console.WriteLine($"f0 = sqrt({N}) - {a0}");

            int d1_0 = 1;
            int d2_0 = -1 * a0;
            int d3_0 = 1;

            int d1 = d1_0;
            int d2 = d2_0;
            int d3 = d3_0;

            int a = a0;

            Console.WriteLine($"f0 = (d1 * sqrt({N}) + d2) / d3, where d1 = {d1_0}, d2 = {d2_0} * a0, d3 = {d3_0}");

            while(true)
            {
                a = (int)(d3 * (a0 - d2) / (N - d2 * d2));
                
            // Console.WriteLine("a1 + f1 = 1 / f0");
            // Console.WriteLine($"a1 + f1 = {d3} / {d1} * (sqrt({N}) + {d2})");
            // Console.WriteLine($"a1 + f1 = {d3} * (sqrt({N}) - {d2}) / {N - d2 * d2}");
            // Console.WriteLine($"a1 = (int)({d3} * (sqrt({N}) - {d2}) / {N - d2 * d2})");
            // Console.WriteLine($"a1 = (int)({d3} * ({a0} - {d2}) / {N - d2 * d2})");
            // int a1 = (int)(d3 * (a0 - d2) / (N - d2 * d2));
            // Console.WriteLine($"a1 = {a1}");
            // Console.WriteLine($"f1 = {d3} * (sqrt({N}) - {d2}) / {N - d2 * d2} - {a1}");
            // d3 = N - d2 * d2;



                if (d1 == d1_0 && d2 == d2_0 && d3 == d3_0)
                {
                    Console.WriteLine("Start repeating.");
                    break;
                }
            }


        }
    }
}
