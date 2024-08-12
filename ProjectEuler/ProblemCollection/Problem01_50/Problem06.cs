using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EulerProject.ProblemCollection
{
    public class Problem6 : ProblemBase
    {
        public Problem6() : base() { }

        public override int ProblemNumber
        {
            get
            {
                return 6;
            }
        }
        private const long upperLimit = 100;
        public override string Description
        {
            get
            {
                return
                    "The sum of the squares of the first ten natural numbers is,\n" +
                    "1 + 4 + 9 + … + 100 = 385\n" +
                    "The square of the sum of the first ten natural numbers is,\n" +
                    "(1 + 2 + … + 10)^2 = 55^2 = 3025\n" +
                    "Hence the difference between the sum of the squares of the first ten natural numbers and the square of the sum is 3025 – 385 = 2640.\n" +
                    "Find the difference between the sum of the squares of the first " + 
                    upperLimit.ToString() + 
                    " (100 in orginal question) natural numbers and the square of the sum. ";
            }
        }

        public override string Solution1()
        {
            return Solution1(upperLimit).ToString();
        }

        private long Solution1(long upperLimit)
        {
            long sum1 = 0;
            long sum2 = 0;

            for (int i = 1; i <= upperLimit; i++)
            {
                sum1 += i;
                sum2 += i * i;
            }

            sum1 *= sum1;

            return sum1 - sum2;
        }

        public override string Solution2()
        {
            return Solution2(upperLimit).ToString();
        }

        private long Solution2(long upperLimit)
        {
            long sum1 = (upperLimit + 1) * upperLimit / 2;
            long sum2 = upperLimit * (upperLimit + 1) * (2 * upperLimit + 1) / 6;

            return sum1 * sum1 - sum2;
        }
    }
}
