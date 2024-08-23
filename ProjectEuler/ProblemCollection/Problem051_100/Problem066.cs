using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;

namespace EulerProject.ProblemCollection.Problem051_100
{
    public class Problem066 : ProblemBase
    {
        public override string Description
        {
            get
            {
                return @"
Problem 66 - Diophantine Equation

Consider quadratic Diophantine equations of the form: x^2 - Dy^2 = 1

For example, when D = 13, the minimal solution in x is 649^2 - 13 * 180^2 = 1.

It can be assumed that there are no solutions in positive integers when D is square.

By finding minimal solutions in x for D = {2, 3, 5, 6, 7}, we obtain the following:

The first ten terms in the sequence of convergents for e are:
    3^2 - 2 * 2^2 = 1
    2^2 - 3 * 1^1 = 1
    9^2 - 5 * 4^2 = 1
    5^2 - 6 * 2^2 = 1
    8^2 - 7 * 3^2 = 1

Hence, by considering minimal solutions in x for D <= 7, the largest x is obtained when D = 5.

Find the value of D <= 1000 in minimal solutions of x for which the largest value of x is obtained.
";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 66;
            }
        }
        int upperLimit = 1000;
        public override string Solution1()
        {
            string idea = @"
Copied the algorithm from Wiki (do not understand it)

To find the minimal x in x^2 - Dy^2 = 1
1. Find the continued fraction of D
2. If the period has an odd length, repeat the whole period
3. Remove the last number in the period
4. Calculate d + 1 / (a1 + 1 / (a2 + 1 / (a3 + 1 / (a4 + ...)))
5. The numerator is the minimal x

For example, solve x^2 - 7y^2 = 1
1. Continued fraction of 7 is [2; (1, 1, 1, 4)]
2. The period (1, 1, 1, 4) has an even length of 4, skip this step
3. Remove the last number '4' in the period, get the alist {1, 1, 1}
4. Calculate 2 + 1 / (1 + 1 / (1 + 1 / 1)) = 8 / 3
5. The numenator 8 is the minimal x, the denomenator 3 is the minimal y

For example, solve x^2 - 13y^2 = 1
1. Continued fraction of 7 is [3; (1, 1, 1, 1, 6)]
2. The period (1, 1, 1, 4) has an odd length of 4, repeat the whole period, get {1, 1, 1, 1, 6, 1, 1, 1, 1, 6}
3. Remove the last number '6' in the period, get the alist {1, 1, 1, 1, 6, 1, 1, 1, 1}
4. Calculate 3 + 1 / (1 + 1 / (1 + 1 / (1 + 1 / (1 + 1 / (6 + 1 / (1 + 1 / (1 + 1 / (1 + 1 / (1)))))))) = 649/180
5. The numenator 649 is the minimal x, the denomenator 180 is the minimal y
";

Console.WriteLine(idea);
            BigInteger maxX = 0;
            int answerD = 0;
            for(int D = 2; D <= upperLimit; D++)
            {
                if (IsSquare(D)) continue;

                BigInteger[] minimalSolution = FindMinimalX(D);
                BigInteger x = minimalSolution[0];
                BigInteger y = minimalSolution[1];
                // Console.WriteLine($"{x}^2 - {D} * {y}^2 = 1");

                if (x > maxX) 
                {
                    maxX = x;
                    answerD = D;
                }
            }

            return answerD.ToString();
        }

        BigInteger[] FindMinimalX(int D)
        {
            int sqrtD = (int)Math.Sqrt(D);
            List<int> aList = CalcSqrtAList(D, sqrtD);
            if (aList.Count % 2 == 1)
            {
                List<int> copyOfAList = new List<int>(aList);
                aList.AddRange(copyOfAList);
            }
            aList.RemoveAt(aList.Count - 1);

            BigInteger n = aList[aList.Count - 1];
            BigInteger d = 1;

            for(int i = aList.Count - 2; i >= 0; i --)
            {
                BigInteger t = n;
                n = d;
                d = t;

                n += aList[i] * d;
            }

            BigInteger x = sqrtD * n + d;
            BigInteger y = d;

            return new BigInteger[]{x, y};
        }

        bool IsSquare(int n)
        {
            double sqrt = Math.Sqrt((double) n);
            return sqrt == (int)sqrt;
        }

        List<int> CalcSqrtAList(int N, int a0)
        {
            int d1_0 = 1;
            int d2_0 = -1 * a0;
            int d3_0 = 1;

            int d1 = d1_0;
            int d2 = d2_0;
            int d3 = d3_0;

            int a = a0;
            List<int> aList = new List<int>();

            while(true)
            {
                int cd3 = d1 * d1 * N - d2 * d2;
                a = (int)((d3 * d1 * a0 - d3 * d2) / cd3);

                d1 = d3 * d1;
                d2 = -1 * d3 * d2 - a * cd3;
                d3 = cd3;

                if (d3 % d1 == 0 && d2 % d1 == 0)
                {
                    d3 /=d1;
                    d2 /=d1;
                    d1 = 1;
                }

                aList.Add(a);

                if (d1 == d1_0 && d2 == d2_0 && d3 == d3_0)
                {
                    break;
                }
            }

            return aList;
        }
    

    }
}
