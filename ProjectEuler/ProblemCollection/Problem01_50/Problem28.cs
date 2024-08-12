using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem28 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 28;
            }
        }

        public override string Description
        {
            get
            {
                return @"
                            Starting with the number 1 and moving to the right in a clockwise direction a 5 by 5 spiral is 
                            formed as follows:

                            21 22 23 24 25
                            20  7  8  9 10
                            19  6  1  2 11
                            18  5  4  3 12
                            17 16 15 14 13

                            It can be verified that the sum of the numbers on the diagonals is 101.

                            What is the sum of the numbers on the diagonals in a 1001 by 1001 spiral formed in the same way?
                            ";
            }
        }

        ulong upperLimit = 1001;

        public override string Solution1()
        {
            ulong start = 1;
            System.Numerics.BigInteger sum = 1;

            for (ulong size = 3; size <= upperLimit; size += 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    start += size - 1;
                    sum += start;
                }
            }

            return sum.ToString();
        }

        public override string Solution2()
        {
            System.Numerics.BigInteger sum = 1;
            for (ulong size = 3; size <= upperLimit; size += 2)
            {
                sum += size * size * 4 - 6 * (size - 1);
            }

            return sum.ToString();
        }
    }
}
