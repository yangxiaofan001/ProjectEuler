using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection.Problem051_100
{
    public class Problem056 : ProblemBase
    {
        public override string Description
        {
            get
            {
                return @"
Problem 56 - Powerful Digit Sum

A googol (10^100) is a massive number: one followed by one-hundred zeros; 100^100 is almost unimaginably large: one followed by two-hundred zeros. Despite their size, the sum of the digits in each number is only 1.

Considering natural numbers of the form, a^b, where a, b < 100, what is the maximum digital sum?
";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 56;
            }
        }

        int DigitSum(System.Numerics.BigInteger n)
        {
            int s = 0;
            while (n > 0)
            {
                s += (int)(n % 10);
                n /= 10;
            }

            return s;
        }

        int upperLimit = 100;
        public override string Solution1()
        {
            int maxDigitSum = 0;
            int m_a = 0;
            int m_b = 0;
            System.Numerics.BigInteger m_n = 0;
            for(int a = 1; a < upperLimit; a ++)
            {
                for(int b = 1; b < upperLimit; b ++)
                {
                    System.Numerics.BigInteger prod = 1;
                    for(int c = 1; c <= b; c ++)
                        prod *= a;
                    int x = DigitSum(prod);
                    if (x > maxDigitSum) 
                    {
                        m_a = a;
                        m_b = b;
                        m_n = prod;
                        maxDigitSum = x;
                    }
                }
            }
            string answer = $"{m_a}^{m_b} produces the max digit sum {maxDigitSum}: {m_a}^{m_b} = {m_n}";

            return answer;
        }
    }
}
