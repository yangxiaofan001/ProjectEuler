using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem37 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 37;
            }
        }

        public override string Description
        {
            get
            {
                return @"
                    Truncatable primes
                    Problem 37
                    The number 3797 has an interesting property. Being prime itself, it is possible to continuously remove digits from left to right, and remain prime at each stage: 3797, 797, 97, and 7. Similarly we can work from right to left: 3797, 379, 37, and 3.

                    Find the sum of the only eleven primes that are both truncatable from left to right and right to left.

                    NOTE: 2, 3, 5, and 7 are not considered to be truncatable primes.
                    ";
            }
        }

        public override string Solution1()
        {
            List<int> trunctablePrimes = new List<int> { 23, 37, 53, 73 };
            int digits = 3;

            while (trunctablePrimes.Count < 11)
            {
                List<int> currDigitsPossibleTPrimeList = PossibleTPrimeList(digits, digits);
                foreach(int i in currDigitsPossibleTPrimeList)
                {
                    if (RightTruncatable(i) && LeftTruncatable(i, digits))
                        trunctablePrimes.Add(i);
                }

                digits++;
            }

            int sum = 0;
            foreach (int p in trunctablePrimes)
            {
                sum += p;
                Console.WriteLine(p);
            }

            return sum.ToString();
        }

        bool RightTruncatable(int number)
        {
            while (number > 0)
            {
                if (!Utils.IsPrime(number))
                    return false;
                number /= 10;
            }

            return true;
        }

        bool LeftTruncatable(int number, int digits)
        {
            while (number > 10)
            {
                if (!Utils.IsPrime(number))
                    return false;

                int pow = (int)(Math.Pow(10, digits--));

                number = number % pow;
            }

            if (!Utils.IsPrime(number))
                return false;

            return true;
        }

        List<int> PossibleTPrimeList(int totalDigits, int digits)
        {
            // 1 digit: 2, 3, 5, 7
            // 2 digits: 23, 37, 53, 73
            // 3 digits: [2,3,5,7][1,3,7,9][3,7]
            // 4 digits: [2,3,5,7][1,3,7,9][1,3,7,9][3,7]
            // 3 + n digits (n >= 0)
            //      [2,3,5,7][1,3,7,9[...]][3,7]


            if (digits == 1)
                return new List<int> { 3, 7 };

            if (digits == totalDigits)
            {
                List<int> returnList = new List<int>();
                foreach (int f in new List<int> { 2, 3, 5, 7 })
                {
                    foreach (int d in PossibleTPrimeList(totalDigits, digits - 1))
                        returnList.Add((int)(Math.Pow(10, totalDigits - 1) * f) + d);
                }

                return returnList;
            }
            else
            {
                List<int> returnList = new List<int>();
                foreach (int f in new List<int> { 1, 3, 5, 7, 9 })
                {
                    foreach (int d in PossibleTPrimeList(totalDigits, digits - 1))
                        returnList.Add((int)(Math.Pow(10, digits - 1) * f) + d);
                }

                return returnList;
            }
        }
    }
}
