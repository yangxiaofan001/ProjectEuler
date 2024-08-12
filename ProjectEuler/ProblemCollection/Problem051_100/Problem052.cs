using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection.Problem051_100
{
    public class Problem052 : ProblemBase
    {
        public override string Description
        {
            get
            {
                return @"
                    Permuted multiples
                    Problem 52
                    It can be seen that the number, 125874, and its double, 251748, contain exactly the same digits, but in a different order.

                    Find the smallest positive integer, x, such that 2x, 3x, 4x, 5x, and 6x, contain the same digits.
                    ";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 52;
            }
        }

        public override string Solution1()
        {
            bool bFound = false;
            int i1 = 1;
            string answer = "";

            while (!bFound)
            {
                int i2 = i1 * 2;

                char[] ca1 = i1.ToString().ToCharArray();
                char[] ca2 = i2.ToString().ToCharArray();
                Array.Sort(ca1);
                Array.Sort(ca2);
                string s1 = new string(ca1);
                string s2 = new string(ca2);
                if (s1 == s2)
                {
                    int i3 = i1 * 3;
                    char[] ca3 = i3.ToString().ToCharArray();
                    Array.Sort(ca3);
                    string s3 = new string(ca3);
                    if (s2 == s3)
                    {
                        int i4 = i1 * 4;
                        char[] ca4 = i4.ToString().ToCharArray();
                        Array.Sort(ca4);
                        string s4 = new string(ca4);
                        if (s3 == s4)
                        {
                            int i5 = i1 * 5;
                            char[] ca5 = i5.ToString().ToCharArray();
                            Array.Sort(ca5);
                            string s5 = new string(ca5);
                            if (s4 == s5)
                            {
                                int i6 = i1 * 6;
                                char[] ca6 = i6.ToString().ToCharArray();
                                Array.Sort(ca6);
                                string s6 = new string(ca6);
                                if (s5 == s6)
                                {
                                    answer = i1.ToString();
                                    break;
                                }
                            }
                        }
                    }
                }

                i1++;
            }

            return answer;
        }
    }
}
