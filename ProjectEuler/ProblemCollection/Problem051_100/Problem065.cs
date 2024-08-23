using System;
using System.Collections.Generic;

namespace EulerProject.ProblemCollection.Problem051_100
{
    public class Problem065 : ProblemBase
    {
        public override string Description
        {
            get
            {
                return @"
Problem 65 - Convergents of e

e = [2;1, 2, 1, 1, 4, 1, 1, 6, 1, 1, 8, ..., 1, 1, 2k, 1, 1, 2k+2, ...]

The first ten terms in the sequence of convergents for e are:


2, 3, 8/3, 11/4, 19/7,87/32, 193/71,1264/465, 1257/536, ...

The sum of digits in the numerator of the 10th convergent is 1 + 4 + 5 + 7 = 17.

Find the sum of digits in the numerator of the 100th convergent of the continued fraction for e.
";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 65;
            }
        }

        public override string Solution1()
        {
            string answer = @"
idea: 
build list of a, {2, 1, 2, 1, 1, 4, ...}, with 100 items
calculate backwards, in each iteration, flip d and n, then n = a * d + n

use System.Numerics.BigInteger
";
            Console.WriteLine(answer);

            List<int> aList = new List<int>{2, 1};
            int i = 0;

            while(aList.Count < 99)
            {
                if (i % 3 == 2)
                    aList.Insert(0, 2 * (i / 3 + 2));
                else 
                    aList.Insert(0, 1);

                i ++;
            }
            aList.Add(2);

            System.Numerics.BigInteger d = aList[0];
            System.Numerics.BigInteger n = 1;

            for(i = 1; i < aList.Count; i ++)
            {
                System.Numerics.BigInteger t = n;
                n = d;
                d = t;

                n += aList[i] * d;
            }

            Console.WriteLine($"{n}/{d}");
            int sum = 0;
            while(n > 0)
            {
                sum += (int)(n % 10);
                n /= 10;
            }

            return sum.ToString();
        }

    }
}
