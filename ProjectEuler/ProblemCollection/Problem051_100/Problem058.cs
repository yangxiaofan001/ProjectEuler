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
    public class Problem058 : ProblemBase
    {
        public override string Description
        {
            get
            {
                return @"
Problem 58 - Spiral Primes

Starting with 1 and spiralling anticlockwise in the following way, a square spiral with side length 7 is formed.

37 36 35 34 33 32 31
38 17 16 15 14 13 30
39 18  5  4  3 12 29
40 19  6  1  2 11 28
41 20  7  8  9 10 27
42 21 22 23 24 25 26
43 44 45 46 47 48 49

It is interesting to note that the odd squares lie along the bottom right diagonal, but what is more interesting is that 8 out of the 13 numbers lying along both diagonals are prime; that is, a ratio of 8 / 13, about 62%.

If one complete new layer is wrapped around the spiral above, a square spiral with side length 9 will be formed. If this process is continued, what is the side length of the square spiral for which the ratio of primes along both diagonals first falls below 10%?
";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 58;
            }
        }

        public override string Solution1()
        {
            
            List<long> list = new List<long>{1};
            long lastNumber = 1;
            long primeCount = 0;
            long l=3;
            while(true)
            {
                for(int i = 1; i <= 4; i++)
                {
                    lastNumber += (l -1);
                    list.Add(lastNumber);
                    if (Utils.IsPrime(lastNumber)) primeCount ++;
                }

                if (primeCount * 10 < l * 2 - 1) break;
                
                l += 2;
            }

            string answer = l.ToString();

            return answer;
        }
    }
}
