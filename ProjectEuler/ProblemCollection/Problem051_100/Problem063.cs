using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection.Problem051_100
{
    public class Problem063 : ProblemBase
    {
        public override string Description
        {
            get
            {
                return @"
Problem 63 - Powerful Digit Counts

The 5-digit number, 16807 = 7^5, is also a fifth power. Similarly, the 9-digit number, 134217728 = 8^9, is a ninth power.

How many n-digit positive integers exist which are also an n-th power?
";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 63;
            }
        }

        public override string Solution1()
        {
            string answer = @"
n-digit positive integers that are also an n-th power. so
    0 < 10^(n - 1) < x^n < 10^n
    from x^n < 10^n, we have x < 10
    from x^n > 0, we have x > 0, so
    x is one of 1, 2, 3, 4, 5, 6, 7, 8, 9

    0 < 10^(n - 1) <= x^n < 10^n

    log:
        n -1 <= n * log x < n
";
Console.WriteLine(answer);
            int count = 0;
            for (int x = 1; x <= 9; x++)
            {
                double logx = Math.Log10(x);
                int n = 1;

                while ((double)(n * logx) >= n - 1 && (double)(n * logx) < n)
                    n++;

                Console.WriteLine($"{x}: {n - 1}");
                count += n - 1;
            }

            return count.ToString();
        }
    }
}
