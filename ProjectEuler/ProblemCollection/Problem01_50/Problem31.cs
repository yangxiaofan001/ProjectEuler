using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem31 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 31;
            }
        }

        public override string Description
        {
            get
            {
                return @"
                    In England the currency is made up of pound, £, and pence, p, and there are eight coins in general circulation:

                    1p, 2p, 5p, 10p, 20p, 50p, £1 (100p) and £2 (200p).
                    It is possible to make £2 in the following way:

                    1×£1 + 1×50p + 2×20p + 1×5p + 1×2p + 3×1p
                    How many different ways can £2 be made using any number of coins?
                    ";
            }
        }


        public override string Solution1()
        {
            // people are too smart
            // don't understand how it works

            int[] coins = new int[] { 200, 100, 50, 20, 10, 5, 2, 1 };
            int amount = 200;

            System.Numerics.BigInteger[] ways = new System.Numerics.BigInteger[amount + 1];
            ways [0] = 1;

            foreach(int coin in coins)
            {
                for(int j = coin; j <= amount; j++)
                {
                    ways[j] = ways[j] + ways [j - coin];
                }
            }

            return ways[amount].ToString();
        }
    }
}
