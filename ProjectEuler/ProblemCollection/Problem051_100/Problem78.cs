using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem78 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 78;
            }
        }

        public override string Description
        {
            get
            {
                return @"Problem 78 - Coin Partitions
Let p(n) represent the number of different ways in which n coins can be separated into piles. 
For example, five coins can be separated into piles in exactly seven different ways, so p(5) = 7.
    5
    4 + 1
    3 + 2
    3 + 1 + 1
    2 + 2 + 1
    2 + 1 + 1 + 1
    1 + 1 + 1 + 1 + 1

Find the least value of n for which p(n) is divisible by one million.
                    ";
            }
        }
        int upperLimit = 1000000;

        public override string Solution1()
        {
            string idea = @"
copy code from problem 76,
assume the upperLimit is 1000000
build CountArray, return if countArray[i] % 1000000 == 0
";

Console.WriteLine(idea);
            BigInteger[] countArray = new BigInteger[upperLimit + 1];
            countArray[0] = 1;
            countArray[1] = 1;

            for (int i = 2; i <= upperLimit; i++)
            {
                // p(n) = sum(k=1 to infinity) (-1)^(k+1)(p[n - k(3k-1)/2]) + p[n-k(3k+1)/2]
                int k = 1;
                while (true)
                {
                    int i1 = i - k * (3 * k - 1) / 2;
                    int i2 = i - k * (3 * k + 1) / 2;
                    int sign = (k % 2 == 0 ? -1 : 1);
                    if (i1 >= 0) countArray[i] = countArray[i] + sign * countArray[i1];
                    if (i2 >= 0) countArray[i] = countArray[i] + sign * countArray[i2];
                    if (i1 < 0 && i2 < 0) break;
                    k++;
                }

                if (countArray[i] % 1000000 == 0) return i.ToString();
            }
            return $"No solution under {upperLimit}";
        }
    }
}
