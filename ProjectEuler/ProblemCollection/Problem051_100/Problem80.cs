using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace EulerProject.ProblemCollection
{
    public class Problem80 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 80;
            }
        }

        public override string Description
        {
            get
            {
                return @"Problem 80 - Square Root Digital Expansion

It is well known that if the square root of a natural number is not an integer, then it is irrational. The decimal expansion of such square roots is infinite without any repeating pattern at all.

The square root of two is 1.41421356237309504880..., and the digital sum of the first one hundred decimal digits is 475.

For the first one hundred natural numbers, find the total of the digital sums of the first one hundred decimal digits for all the irrational square roots.
";
            }
        }

        string SquareRoot(int n, int precision)
        {
            
            BigInteger d0=(int)Math.Sqrt(n);
            BigInteger r0 = n - d0 * d0;
            BigInteger a0 = d0;
            BigInteger d = d0;
            BigInteger r = r0;
            BigInteger a = a0;

            while(r > 0 && a.ToString().Length - a0.ToString().Length < precision )
            {
                r *= 100;
                BigInteger t = a * 2;
                d = 9;
                while((t * 10 + d) * d > r) d --;
                a = a * 10 + d;

                // Console.WriteLine($"r = {r}, t = {t}, d = {d}, a = {a}");

                r = r - (t * 10 + d) * d;
            }

            return a0.ToString() + "." + a.ToString().Substring(a0.ToString().Length);
        }

        public override string Solution1()
        {
            string idea = @"
calculate square root with long division

    initial values:
        d = (int) Math.Sqrt(n)
        a = d
        r = n - d^2

    each iteration
        r = r * 100, t = a * 2, find d so that (t*10+d)* d < r, a = a * 10 + d
    loop until r = 0 or 100 digits

Note the text in the problem:
    . 'for all the irrational square roots', 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 does not count
    . 'the digital sums of the first one hundred decimal digits', this includes digits both before and after the decimal place
";
Console.WriteLine(idea);

BigInteger sum = 0;
        for(int i = 1; i <= 100; i ++)
        {
            string s = SquareRoot(i, 99);   // 99 digits afetr decimal place, 1 digit before decimal place
            if (s[s.Length - 1] == '.') continue;
            s = s.Replace(".", "");

            foreach(char c in s)
            sum += c - '0';
        }

// string s1 = "4142135623730950488016887242096980785696718753769480731766797379907324784621070388503875343276415727";
// string s2 = "414213562373095048801688724209698078569671875376948073176679737990732478462107038850387534327641573";

// sum = 0;
// foreach(char c in s1) sum+=c-'0';

// Console.WriteLine($"s1: {sum}");

// sum = 0;
// foreach(char c in s2) sum+=c-'0';

// Console.WriteLine($"s1: {sum}");


            return sum.ToString();
        }
    }
}
