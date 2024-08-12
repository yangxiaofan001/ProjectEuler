using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem20 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 20;
            }
        }

        public override string Description
        {
            get
            {
                return @"n! means n × (n − 1) × ... × 3 × 2 × 1

                For example, 10! = 10 × 9 × ... × 3 × 2 × 1 = 3628800,
                and the sum of the digits in the number 10! is 3 + 6 + 2 + 8 + 8 + 0 + 0 = 27.

                Find the sum of the digits in the number 100!";
            }
        }

        public override string Solution1()
        {
            string result = "1";
            for (int i = 2; i <= 100; i++)
            {
                result = Utils.stringMultiply(result, i);
            }

            long sum = 0;
            foreach (char c in result)
            {
                sum += (c - '0');
            }

            return sum.ToString();
        }
    }
}
