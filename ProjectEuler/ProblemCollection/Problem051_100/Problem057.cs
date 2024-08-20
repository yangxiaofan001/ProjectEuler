using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection.Problem051_100
{
    public class Problem057 : ProblemBase
    {
        public override string Description
        {
            get
            {
                return @"
Problem 57 - Square Root Convergents

It is possible to show that the square root of two can be expressed as an infinite continued fraction.

sqrt(2) = 1 + 1 / (2 + 1 / (2 + 1 / (2 + 1 / ...))))

By expanding this for the first four iterations, we get:
1 + 1 / 2 = 3 / 2 = 1.5
1 + 1 / (2 + 1 / 2) = 7 / 5 = 1.4
1 + 1 / (2 + 1 / (2 + 1 / 2)) = 17 / 12 = 1.41666...
1 + 1 / (2 + 1 / (2 + 1 / (2 + 1 / 2))) = 41 / 29 = 1.41379...

The next three expansions are 99/70, 239/169, and 577/408, but the eighth expansion, 1393/985, is the first example where the number of digits in the numerator exceeds the number of digits in the denominator.

In the first one-thousand expansions, how many fractions contain a numerator with more digits than the denominator?
";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 57;
            }
        }

        int GetDigits(BigInteger n)
        {
            int d = 0;
            while(n > 0)
            {
                d += 1;
                n /= 10;
            }

            return d;
        }

        public override string Solution1()
        {
            string idea = @"
Idea: In each expansion e(n), numerator en(n) = en(n - 1) + 2 * ed(n-1), and denomenator ed(n) = en(n - 1) + ed(n-1)
            ";            
            Console.WriteLine(idea);

            BigInteger lastNumerator = 1;
            BigInteger lastDenominator = 1;
            long count = 0;

            for(int i = 1; i <= 1000; i ++)
            {
                BigInteger numerator = lastDenominator * 2 + lastNumerator;
                BigInteger denominator = lastDenominator + lastNumerator;

                if (GetDigits(numerator) > GetDigits(denominator)) count ++;

                lastNumerator = numerator;
                lastDenominator = denominator;
            }

            
            string answer = count.ToString();

            return answer;
        }
    }
}
