using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem32 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 32;
            }
        }

        public override string Description
        {
            get
            {
                return @"
                    Problem 32
                    We shall say that an n-digit number is pandigital if it makes use of all the digits 1 to n exactly once; for example, the 5-digit number, 15234, is 1 through 5 pandigital.

                    The product 7254 is unusual, as the identity, 39 × 186 = 7254, containing multiplicand, multiplier, and product is 1 through 9 pandigital.

                    Find the sum of all products whose multiplicand/multiplier/product identity can be written as a 1 through 9 pandigital.

                    HINT: Some products can be obtained in more than one way so be sure to only include it once in your sum.
                    ";
            }
        }

        public override string Solution1()
        {
            List<long> productList = new List<long>();
            // only possibilities
            //      1 digit number X 4 digits number = 4 digits number
            //      2 digit number X 3 digits number = 4 digits number
            for (long i = 1; i <= 98; i++)
            {
                long maxJ = 9999 / i;
                long minJ = 1000 / i;
                for (long j = minJ; j <= maxJ; j++)
                {
                    long product = i * j;
                    char[] cArray = (i.ToString() + j.ToString() + (product).ToString()).ToCharArray();
                    Array.Sort(cArray);
                    if (new string(cArray) == "123456789")
                    {
                        Console.WriteLine(i + " X " + j + " = " + product);
                        productList.Add(product);
                    }
                }
            }

            long result = productList.Distinct().Sum(p => p);

            return result.ToString();
        }
    }
}
