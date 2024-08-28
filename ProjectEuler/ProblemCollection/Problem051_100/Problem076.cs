using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Globalization;

namespace EulerProject.ProblemCollection
{
    public class Problem76 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 76;
            }
        }

        public override string Description
        {
            get
            {
                return @"Problem 76 - Counting Summations

How many different ways can one hundred be written as a sum of at least two positive integers?                
";
            }
        }

        int upperLimit = 100;

        public override string Solution1()
        {
            string idea = @"
Wiki, don't understand.     https://en.wikipedia.org/wiki/Partition_function_(number_theory)

p[n] is the count of ways to represent n as a sum, including an extra way {n} which contains 1 number
p[0] = 1
p[1] = 1
p[m] = 0 for all m < 0
p[n] = sum(k=1 to infinity) (-1)^(k+1)(p[n - k(3k-1)/2]) + p[n-k(3k+1)/2]

k = 1 to infinity does not mean this expression goes on forever. When k grows, n - k(3k-1)/2 or n - k(3k+1)/2 will reach negative number, and p[m] = 0 for all m < 0

For the purpose of this problem, substract 1 from the result, excluding 1 way - {100}
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
            }



            return (countArray[upperLimit] - 1).ToString();
        }
    }
}
