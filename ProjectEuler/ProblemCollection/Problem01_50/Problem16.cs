using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EulerProject.ProblemCollection
{
    public class Problem16 : ProblemBase
    {
        const int upperLimit = 1000;

        public override string Description
        {
            get
            {
                return @"2^15 = 32768 and the sum of its digits is 3 + 2 + 7 + 6 + 8 = 26.

                    What is the sum of the digits of the number 2^1000?";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 16;
            }
        }

        public override string Solution1()
        {
            // assume there is no BigInteger
            // double does not fit, it loses precision
            // use string, with help of a stringMultiply function

            string stringPow = "1";
            for (int i = 0; i < upperLimit; i++)
                stringPow = Utils.stringMultiply(stringPow, 2);

            int sum = 0;
            for (int i = 0; i < stringPow.Length; i++)
                sum += (stringPow[i] - '0');

            return sum.ToString();
        }


        public override string Solution2()
        {
            // Cheating with BigInteger
            // It is pretty big. still works when upperLimit = 20,000
            int result = 0;

            System.Numerics.BigInteger number = System.Numerics.BigInteger.Pow(2, upperLimit);
            while (number > 0)
            {
                result += (int)(number % 10);
                number /= 10;
            }

            return result.ToString();
        }
    }
}
