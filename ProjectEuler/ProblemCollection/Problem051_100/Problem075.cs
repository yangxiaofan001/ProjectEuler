using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Globalization;

namespace EulerProject.ProblemCollection
{
    public class Problem75 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 75;
            }
        }

        public override string Description
        {
            get
            {
                return @"Problem 75 - Singular Integer Right Triangles
";
            }
        }

        int upperLimit = 1500000;

        int gcd(int m, int n)
        {
            int t = Math.Max(m, n);
            n = Math.Min(m, n);
            m = t;

            while(m % n > 0)
            {
                t = m % n;
                m = n;
                n = t;
            }

            return n;
        }

        public override string Solution1()
        {
            string idea = @"
problem 75 forum, user 'euler'
I never realised that the Pythagorean triplet producing formulae: a=m^2+n^2, b=m^2-n^2;n2, c=2mn, where m>n, do not produce every triplet.

My method was to only consider primitives: 
    if m+n≡1 mod 2 AND HCF(m,n)=1, it will be primitive (it is fairly easy to prove). 
    Then for a given perimeter: p=a+b+c, use an array to store the number of solutions found for p and multiples of p. 
            ";
            Console.WriteLine(idea);

            int sqrt = (int)Math.Sqrt(upperLimit);

             int [] countArray = new int[upperLimit + 1];
             for(int i = 0; i <= upperLimit; i ++) countArray[i] = 0;

            for(int m = 1; m <= sqrt; m ++)
            {
                for(int n = m + 1; n <= sqrt; n ++)
                {
                    if (m > 1 && gcd(m, n) > 1) continue;
                    if ((m + n)%2 == 0) continue;       // don't understand, why (m + n) % 2 == 0 is not 'primitive'

                    int a = n * n - m * m;
                    int b = 2 * m * n;
                    int c = n * n + m * m;
                    int l = a + b + c;

                    if (l > upperLimit) break;

                    // this is wrong. an l can be produced from 'primitive' values and the same time produced from other 'multiples'
                    // countArray[l] = 1;

                    for(int x = l; x <= upperLimit; x +=l) countArray[x]=countArray[x] + 1;
                }
            }

            return countArray.Count(x => x == 1).ToString();
        }
    }
}
