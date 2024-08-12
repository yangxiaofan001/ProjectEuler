using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem17 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 17;
            }
        }

        public override string Description
        {
            get
            {
                return @"
                    If the numbers 1 to 5 are written out in words: one, two, three, four, five, then there are 3 + 3 + 5 + 4 + 4 = 19 letters used in total.

                    If all the numbers from 1 to 1000 (one thousand) inclusive were written out in words, how many letters would be used?


                    NOTE: Do not count spaces or hyphens. For example, 342 (three hundred and forty-two) contains 23 letters and 115 (one hundred and fifteen) contains 20 letters. The use of 'and' when writing out numbers is in compliance with British usage.";
            }
        }

        public override string Solution1()
        {
            // spell out each number, count
            string result = spellNumberUnder1Million(95837);
            int count = 0;
            for (int i = 1; i <= 1000; i++)
            {
                string spell = spellNumberUnder1Million(i);
                count += spell.Length;
            }
            
            return count.ToString();
        }

        string spellNumberUnder1Million(int number)
        {
            if (number <= 0) throw new ApplicationException("Number starts with 1");
            Dictionary<int, string> translate = new Dictionary<int, string>
            {
                {1, "one"},
                {2, "two"},
                {3, "three"},
                {4, "four"},
                {5, "five"},
                {6, "six"},
                {7, "seven"},
                {8, "eight"},
                {9, "nine"},
                {10, "ten"},
                {11, "eleven"},
                {12, "twelve"},
                {13, "thirteen"},
                {14, "fourteen"},
                {15, "fifteen"},
                {16, "sixteen"},
                {17, "seventeen"},
                {18, "eighteen"},
                {19, "nineteen"},
                {20, "twenty"},
                {30, "thirty"},
                {40, "forty"},
                {50, "fifty"},
                {60, "sixty"},
                {70, "seventy"},
                {80, "eighty"},
                {90, "ninety"},
                {100, "hundred"},
                {1000, "thousand"},
            };

            string result = "";

            if (number >= 1000)
            {
                result = spellNumberUnder1Million(number / 1000) + translate[1000]
                            + ((number % 1000 != 0) ? spellNumberUnder1Million(number % 1000) : "");
            }
            else if (number >= 100)
            {
                result = spellNumberUnder1Million(number / 100) + translate[100]
                            + ((number % 100 != 0) ? ("and" + spellNumberUnder1Million(number % 100)) : "");
            }
            else if (number >= 90)
                result = translate[90] + ((number - 90 > 0) ? translate[number - 90] : "");
            else if (number >= 80)
                result = translate[80] + ((number - 80 > 0) ? translate[number - 80] : "");
            else if (number >= 70)
                result = translate[70] + ((number - 70 > 0) ? translate[number - 70] : "");
            else if (number >= 60)
                result = translate[60] + ((number - 60 > 0) ? translate[number - 60] : "");
            else if (number >= 50)
                result = translate[50] + ((number - 50 > 0) ? translate[number - 50] : "");
            else if (number >= 40)
                result = translate[40] + ((number - 40 > 0) ? translate[number - 40] : "");
            else if (number >= 30)
                result = translate[30] + ((number - 30 > 0) ? translate[number - 30] : "");
            else if (number >= 20)
                result = translate[20] + ((number - 20 > 0) ? translate[number - 20] : "");
            else
                result = translate[number];

            return result;
        }
    }
}
