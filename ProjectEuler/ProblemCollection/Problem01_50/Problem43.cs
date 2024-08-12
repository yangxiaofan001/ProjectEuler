using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem43 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 43;
            }
        }

        public override string Description
        {
            get
            {
                return @"
                    Sub-string divisibility
                    Problem 43
                    The number, 1406357289, is a 0 to 9 pandigital number because it is made up of each of the digits 0 to 9 
                    in some order, but it also has a rather interesting sub-string divisibility property.

                    Let d1 be the 1st digit, d2 be the 2nd digit, and so on. In this way, we note the following:

                    d2d3d4=406 is divisible by 2
                    d3d4d5=063 is divisible by 3
                    d4d5d6=635 is divisible by 5
                    d5d6d7=357 is divisible by 7
                    d6d7d8=572 is divisible by 11
                    d7d8d9=728 is divisible by 13
                    d8d9d10=289 is divisible by 17
                    Find the sum of all 0 to 9 pandigital numbers with this property.
                    ";
            }
        }

        public override string Solution1()
        {
            int[] primes = new int[] { 17, 13, 11, 7, 5, 3, 2, 1 };
            List<string> possibleLastDigits = new List<string>();
            int index = 1;
            string sum = "0";
            List<string> tempList;

            while (index * 17 < 1000)
            {
                int x = index * 17;
                string s = x.ToString("000");

                // ignore 119, 221..., where there are character appearing more than once
                if (s[0] != s[1] && s[0] != s[2] && s[1] != s[2])
                    possibleLastDigits.Add(s);

                index++;
            }

            // form a 9 digits pandigital number
            for (int totalDigits = 3; totalDigits <= 9; totalDigits++)
            {
                tempList = new List<string>();
                foreach (string s in possibleLastDigits)
                    tempList.Add(s);
                possibleLastDigits.Clear();

                foreach (string lastDigitsNumber in tempList)
                {
                    string last2Digits = lastDigitsNumber.Substring(0, 2);

                    for (int i = 0; i <= 9; i++)
                    {
                        char c = (char)(i + '0');
                        if (lastDigitsNumber.Contains(c)) continue;

                        int x = Convert.ToInt32(c + last2Digits);
                        if (x % primes[totalDigits - 2] == 0)
                        {
                            possibleLastDigits.Add(c + lastDigitsNumber);
                        }
                    }
                }
            }

            foreach (string s in possibleLastDigits)
            {
                Console.WriteLine(s);
                sum = Utils.StringAddition(sum, s);
            }

            return sum;
        }

    }
}
