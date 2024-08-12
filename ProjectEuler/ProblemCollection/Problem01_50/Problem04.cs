using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EulerProject.ProblemCollection
{
    public class Problem4 : ProblemBase
    {
        public Problem4() : base() { }

        public override int ProblemNumber
        {
            get
            {
                return 4;
            }
        }
        private const long upperLimit = 3;
        public override string Description
        {
            get
            {
                return "A palindromic number reads the same both ways. The largest palindrome made from the product of two 2-digit numbers is 9009 = 91 * 99.\n" +
                    "Find the largest palindrome made from the product of two " +
                    upperLimit.ToString() +
                    "-digit numbers. (3-digit in original question)";
            }
        }

        public override string Solution1()
        {
            return Solution1(upperLimit).ToString();
        }

        private long Solution1(long digits)
        {
            long smallestNumber = (long)(Math.Pow(10, digits - 1));
            long largestNumber = (long)(Math.Pow(10, digits)) - 1;
            long answer = 0;

            for (long x = smallestNumber; x <= largestNumber; x++)
            {
                for (long y = smallestNumber; y <= largestNumber; y++)
                {
                    long production = x * y;
                    if (IsPalindromicNumber(production) && production > answer)
                        answer = production;
                }
            }
            return answer;
        }

        private bool IsPalindromicNumber(long n)
        {
            string s = n.ToString();

            for (int i = 0; i < (s.Length / 2); i++)
            {
                if (s[i] != s[s.Length - i - 1])
                    return false;
            }

            return true;
        }
    }
}
