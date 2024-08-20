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
    public class Problem055 : ProblemBase
    {
        public override string Description
        {
            get
            {
                return @"
Problem 55 - Lychrel Numbers

If we take 47, reverse and add, 47+74=121, which is palindromic.

Not all numbers produce palindromes so quickly. For example,

349+943=1292
1292+2921=4213
4213+3124=7337

That is, 349 took three iterations to arrive at a palindrome.

Although no one has proved it yet, it is thought that some numbers, like 196, never produce a palindrome. A number that never forms a palindrome through the reverse and add process is called a Lychrel number. Due to the theoretical nature of these numbers, and for the purpose of this problem, we shall assume that a number is Lychrel until proven otherwise. In addition you are given that for every number below ten-thousand, it will either (i) become a palindrome in less than fifty iterations, or, (ii) no one, with all the computing power that exists, has managed so far to map it to a palindrome. In fact, 10677 is the first number to be shown to require over fifty iterations before producing a palindrome: 4668731596684224866951378664 (53 iterations, 28-digits).

Surprisingly, there are palindromic numbers that are themselves Lychrel numbers; the first example is 4994.

How many Lychrel numbers are there below ten-thousand?

NOTE: Wording was modified slightly on 24 April 2007 to emphasise the theoretical nature of Lychrel numbers.
";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 55;
            }
        }

        System.Numerics.BigInteger Calc(System.Numerics.BigInteger n)
        {
            System.Numerics.BigInteger k = ReverseNumber(n);

            return n + k;
        }

        System.Numerics.BigInteger ReverseNumber(System.Numerics.BigInteger n)
        {
            System.Numerics.BigInteger k = n;
            System.Numerics.BigInteger r = 0;

            while (k > 0)
            {
                r = r * 10 + k % 10;
                k /= 10;
            }

            return r;
        }

        public override string Solution1()
        {
            string idea = "No idea. Brutal force using c# System.Numeric.BigInteger";
            Console.WriteLine(idea);
                        
            int lychrelNumberCount = 0;
            for(System.Numerics.BigInteger n = 1; n < 10000; n ++)
            {
                System.Numerics.BigInteger k = n;
                int calcCount = 0;
                while (calcCount < 50)
                {
                    k = Calc(k);
                    calcCount ++;
                    if (ReverseNumber(k) == k) break;
                }

                if (calcCount == 50) lychrelNumberCount ++;
            }

            string answer = lychrelNumberCount.ToString();

            return answer;
        }

    }
}
