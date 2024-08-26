using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

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
            string idea = @"";
            Console.WriteLine(idea);

            int sqrt = (int)Math.Sqrt(upperLimit);
            List<long> primes = Utils.IntSieveOfEratosthenes(sqrt);

            int [] countArray = new int[upperLimit + 1];
            for(int i = 0; i <= upperLimit; i ++) countArray[i] = 0;

Console.WriteLine($"sqrt = {sqrt}");
Console.WriteLine(gcd(570, 76));
            for(int m = 1; m <= sqrt; m ++)
            {
                if (m % 10 == 0) Console.WriteLine($"m = {m} ");
                for(int n = m + 1; n <= sqrt; n ++)
                {
                    if (n % 100 == 0) Console.WriteLine($"n = {n} ");
                    if (gcd(m, n) > 1) continue;

                    int a = n * n - m * m;
                    int b = 2 * m * n;
                    int c = n * n + m * m;
                    int l = a + b + c;
                    if (l > upperLimit) break;
                    countArray[l] = 1;
                    for(int x = 2 * l; x <= upperLimit; x ++)
                        countArray[x]=countArray[x] + 1;
                }
            }
       

            return countArray.Count(x => x == 1).ToString();
        }
    }
}
