using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem41 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 41;
            }
        }

        public override string Description
        {
            get
            {
                return @"
                    Pandigital prime
                    Problem 41
                    We shall say that an n-digit number is pandigital if it makes use of all the digits 1 to n exactly once. For example, 2143 is a 4-digit pandigital and is also prime.

                    What is the largest n-digit pandigital prime that exists?
                    ";
            }
        }

        public override string Solution1()
        {
            List<int> possibleLastDigitNumbers = new List<int> { 1, 3, 7, 9 };
            List<int> one2eight = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };

            // only 4 digit number and 7 digit number
            // 2 digits: 12 or 21
            // 3 digits: 1 + 2 + 3 = 6, the number will be dividable by 3
            // 5 digits: 1 + 2 + 3 + 4 + 5= 15, the number will be dividable by 3
            // 6 digits: 1 + 2 + 3 + 4 + 5 + 6 = 21, the number will be dividable by 3
            // 8 digits: 1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 = 36, the number will be dividable by 3
            // 9 digits: 1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9 = 45, the number will be dividable by 3

            List<int> possibleTotalDigits = new List<int>{4, 7};

            long maxPrime = 2;
            foreach(int totalDigits in possibleTotalDigits)
            {
                foreach(int lastDigitNumber in possibleLastDigitNumbers)
                {
                    if (lastDigitNumber > totalDigits)
                        continue;

                    List<int> possibleOtherDigitNumbers = new List<int>();
                    foreach(int number in one2eight)
                    {
                        if (number <= totalDigits && number != lastDigitNumber)
                        {
                            possibleOtherDigitNumbers.Add(number);
                        }
                    }

                    List<List<int>> otherDigitsList = Utils.PermutationList(possibleOtherDigitNumbers);

                    foreach(List<int> otherDigits in otherDigitsList)
                    {
                        int pandigitalNumber = lastDigitNumber;
                        int pow = 1;

                        foreach (int d in otherDigits)
                        {
                            pow *= 10;
                            pandigitalNumber += pow * d;
                        }

                        if (pandigitalNumber > maxPrime)
                        {
                            if (Utils.IsPrime(pandigitalNumber))
                            {
                                Console.WriteLine(pandigitalNumber);
                                maxPrime = pandigitalNumber;
                            }
                        }
                    }
                }
            }


            return maxPrime.ToString();
        }
    }
}
