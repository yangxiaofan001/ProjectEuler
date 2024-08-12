using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem36 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 36;
            }
        }

        public override string Description
        {
            get
            {
                return @"
                        Double-base palindromes
                        Problem 36
                        The decimal number, 585 = 10010010012 (binary), is palindromic in both bases.

                        Find the sum of all numbers, less than one million, which are palindromic in base 10 and base 2.

                        (Please note that the palindromic number, in either base, may not include leading zeros.)
                        ";
            }
        }

        public override string Solution1()
        {

            ClearLog();

            int sum = 0;
            for (int i = 1; i <= 9; i++)
            { 
                // 1 digit number
                int number = i;
                List<bool> binaryInt = IntToBinary(number);
                if (IsPalindromic(binaryInt))
                {
                    string log = number.ToString() + "\t\t";
                    foreach (bool b in binaryInt)
                        log = log + (b ? '1' : '0');

                    WriteLog(log);
                    sum += number;
                }

                // 2 digits number
                number = 11 * i;
                binaryInt = IntToBinary(number);
                if (IsPalindromic(binaryInt))
                {
                    string log = number.ToString() + "\t\t";
                    foreach (bool b in binaryInt)
                        log = log + (b ? '1' : '0');

                    WriteLog(log);
                    sum += number;
                }
            }

            for (int i = 0; i <= 9; i++)
            {
                for (int j = 1; j <= 9; j++)
                { 
                    // 3 digits number
                    int number = j * 100 + i * 10 + j;
                    List<bool> binaryInt = IntToBinary(number);
                    if (IsPalindromic(binaryInt))
                    {
                        string log = number.ToString() + "\t\t";
                        foreach (bool b in binaryInt)
                            log = log + (b ? '1' : '0');

                        WriteLog(log);
                        sum += number;
                    }

                    // 4 digits number
                    number = j * 1000 + i * 100 + i * 10 + j;
                    binaryInt = IntToBinary(number);
                    if (IsPalindromic(binaryInt))
                    {
                        string log = number.ToString() + "\t\t";
                        foreach (bool b in binaryInt)
                            log = log + (b ? '1' : '0');

                        WriteLog(log);
                        sum += number;
                    }
                }
            }

            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    for (int k = 1; k <= 9; k++)
                    { 
                        // 5 digits number
                        int number = k * 10000 + j * 1000 + i * 100 + j * 10 + k;
                        List<bool> binaryInt = IntToBinary(number);
                        if (IsPalindromic(binaryInt))
                        {
                            string log = number.ToString() + "\t\t";
                            foreach (bool b in binaryInt)
                                log = log + (b ? '1' : '0');

                            WriteLog(log);
                            sum += number;
                        }

                        // 6 digits number
                        number = k * 100000 + j * 10000 + i * 1000 + i * 100 + j * 10 + k;
                        binaryInt = IntToBinary(number);
                        if (IsPalindromic(binaryInt))
                        {
                            string log = number.ToString() + "\t\t";
                            foreach (bool b in binaryInt)
                                log = log + (b ? '1' : '0');

                            WriteLog(log);
                            sum += number;
                        }
                    }
                }
            }

            return sum.ToString();
        }

        private static bool IsPalindromic(List<bool> binaryInt)
        {
            bool bPalindromic = true;
            int midIndex = (int)(Math.Round(binaryInt.Count / 2.0));
            for (int x = 0; x < midIndex; x++)
            {
                if (binaryInt[x] != binaryInt[binaryInt.Count - 1 - x])
                {
                    bPalindromic = false;
                    break;
                }
            }
            return bPalindromic;
        }

        List<bool> IntToBinary(int i)
        {
            List<bool> returnList = new List<bool>();
            while (i > 1)
            {
                returnList.Insert(0, (i % 2 != 0));
                i /= 2;
            }

            if (i == 1)
                returnList.Insert(0, true);

            return returnList;
        }

        void WriteLog(string line)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(@"c:\temp\log.txt", true);
            sw.WriteLine(line);
            sw.Close();
        }

        void ClearLog()
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(@"c:\temp\log.txt", false);
            sw.Close();
        }

    }
}
