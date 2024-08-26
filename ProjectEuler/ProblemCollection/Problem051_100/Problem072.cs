using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace EulerProject.ProblemCollection
{
    public class Problem72 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 72;
            }
        }

        public override string Description
        {
            get
            {
                return @"Problem 72 - Counting Fractions

Consider the fraction, n/d, where n and d are positive integers. If n < d and GCD(n,d) = 1, it is called a reduced proper fraction.

If we list the set of reduced proper fractions for d<=8 in ascending order of size, we get:
1/8,1/7,1/6,...2/7,3/7,3/8,5/8,7/8,...


It can be seen that there are 21 elements in this set.

How many elements would be contained in the set of reduced proper fractions for d <= 1000000?
";
            }
        }

        int upperLimit = 1000000;

        public override string Solution1()
        {
            string idea = @"
For each d, there are phi(d) proper fractions. So the answer to this question is the sum of phi(d) for 2<=d<=1000000

Find phi(d) for all 2<=d<=1000000, add them together.
";
Console.WriteLine(idea);
            int answer = upperLimit / 7 * 3 - 1;
            BigInteger sum = 0;


            List<int> phiArray = Utils.GetAllPhiUnderP(upperLimit);

            for(int i = 2; i <= upperLimit; i++)
                sum += phiArray[i];


            return sum.ToString();
        }

    }
}
