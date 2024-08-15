using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace EulerProject.ProblemCollection
{
    public class Problem203 : ProblemBase
    {
        public Problem203() : base() { }

        public override int ProblemNumber
        {
            get
            {
                return 203;
            }
        }
        private const long upperLimit = 51;
        public override string Description
        {
            get
            {
                return @"
Problem 203 - Squarefree Binomial Coefficients. 

The binomial coefficients c(n, k) can be arranged in triangular form, Pascal's triangle, like this:
1
1   1
1   2   1
1   3   3   1
1   4   6   4   1
1   5   10  10  5   1
1   6   15  20  15  6   1
1   7   21  35  35  21  7   1
....

It can be seen that the first eight rows of Pascal's triangle contain twelve distinct numbers:
1, 2, 3, 4, 5, 6, 7, 10, 15, 20, 21 and 35.

A positive integer n is called squarefree if no square of a prime divides n. Of the twelve distinct numbers in the first eight rows of Pascal's triangle, all except 4 and 20 are squarefree. The sum of the distinct squarefree numbers in the first eight rows is 105.

Find the sum of the distinct squarefree numbers in the first 51 rows of Pascal's triangle.
";
            }
        }
        public override string Solution1()
        {
            List<List<long>> coefficients = new List<List<long>>();
            coefficients.Add(new List<long>{1});
            coefficients.Add(new List<long>{1, 1});

            for(int r = 2; r < upperLimit; r ++)
            {
                List<long> lastRow = coefficients[r - 1];
                List<long> currentRow = new List<long>(){1};
                for(int i = 0; i < lastRow.Count - 1; i ++)
                {
                    currentRow.Add(lastRow[i] + lastRow[i+1]);
                }
                currentRow.Add(1);
                coefficients.Add(currentRow);
            }

            List<long> allNumbers = new List<long>();
            foreach(List<long> row in coefficients)
            {
                allNumbers.AddRange(row);
            }

            allNumbers = allNumbers.Distinct().OrderBy(p=> p).ToList();
            long maxN = allNumbers.Max(p => p);

            // in the prime factorization of any of these number
            // te largest p < 51
            List<long> primes = Utils.GetAllPrimeUnderP(51);//Utils.GetAllPrimeUnderP((long)(Math.Sqrt(maxN)));


long sum = 0;
            foreach(long n in allNumbers)
            {
                bool squareFree = true;
                foreach(long p in primes)
                {
                    long psquare = p * p;
                    if (n < psquare) break;
                    if (n % psquare == 0)
                    {
                        squareFree = false;
                        break;
                    }
                }

                if (squareFree) sum += n;
            }

            string answer = sum.ToString();
            return answer;
        }

    }
}
