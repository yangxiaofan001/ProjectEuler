using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem38 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 38;
            }
        }

        public override string Description
        {
            get
            {
                return @"
                    Pandigital multiples
                    Problem 38
                    Take the number 192 and multiply it by each of 1, 2, and 3:

                    192 × 1 = 192
                    192 × 2 = 384
                    192 × 3 = 576
                    By concatenating each product we get the 1 to 9 pandigital, 192384576. 
                    We will call 192384576 the concatenated product of 192 and (1,2,3)

                    The same can be achieved by starting with 9 and multiplying by 1, 2, 3, 4, and 5, giving the pandigital, 
                    918273645, which is the concatenated product of 9 and (1,2,3,4,5).

                    What is the largest 1 to 9 pandigital 9-digit number that can be formed as the concatenated product of 
                    an integer with (1,2, ... , n) where n > 1?
                    ";
            }
        }

        public override string Solution1()
        {
            string maxP = "";

            for (int i = 1; i <= 9999; i++)
            {
                if (i == 192)
                {
                    int x = 1;
                }

                int j = 1;
                string s = "";
                while (true)
                {
                    s = s + Utils.stringMultiply(i.ToString(), j++);
                    if (s.Length >= 9)
                        break;
                }

                if (Utils.IsPandigital(s, 9))
                {
                    if (string.Compare(s, maxP) > 0)
                    {
                        Console.WriteLine(i + " " + j + " " + s);
                        maxP = s;
                    }
                }
            }

            return maxP;
        }


             
    }
}
