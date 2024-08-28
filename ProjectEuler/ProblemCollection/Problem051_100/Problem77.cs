using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem77 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 77;
            }
        }

        public override string Description
        {
            get
            {
                return @"Problem 77 - Prime Summations
It is possible to write 10 as the sum of primes in exactly five different ways:
    7 + 3
    5 + 5
    5 + 3 + 2
    3 + 3 + 2 + 2
    2 + 2 + 2 + 2 + 2

What is the first value which can be written as the sum of primes in over five thousand different ways?
                    ";
            }
        }

        BigInteger CountWays(int n)
        {
            long[] coins = Utils.IntSieveOfEratosthenes(n).ToArray();
            long amount = n;
            BigInteger[] ways = new BigInteger[amount + 1];
            ways [0] = 1;
            foreach(int coin in coins)
            {
                for(int j = coin; j <= amount; j++)
                {
                    ways[j] = ways[j] + ways [j - coin];
                }
            }

            return ways[amount];
        }

        public override string Solution1()
        {
            string idea = @"
copy code from problem 31, 'coins sum', which count the ways of making up $2 using coins $2, $1, 50c, 20c, 10c, 4c, 2c, and 1c

create a method, take an integer n as parameter, return the ways of making it up with primes under n - as the different values of coina

comment in problem 31 code is still valid
    // people are too smart
    // don't understand how it works
";

Console.WriteLine(idea);

            int n = 10;
            BigInteger ways = 0;
            while(ways <= 5000)
            {
                n++;
                ways = CountWays(n);
            }

            return n.ToString();
        }
    }
}
