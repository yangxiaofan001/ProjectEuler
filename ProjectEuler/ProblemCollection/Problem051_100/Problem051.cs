using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection.Problem051_100
{
    public class Problem051 : ProblemBase
    {
        public override string Description
        {
            get
            {
                return @"
                    Prime digit replacements
                    Problem 51

                    By replacing the 1st digit of the 2-digit number *3, it turns out that six of the nine possible values: 
                        13, 23, 43, 53, 73, and 83, are all prime.

                    By replacing the 3rd and 4th digits of 56**3 with the same digit, this 5-digit number is the first example having seven primes 
                        among the ten generated numbers, yielding the family: 56003, 56113, 56333, 56443, 56663, 56773, and 56993. 
                        Consequently 56003, being the first member of this family, is the smallest prime with this property.

                    Find the smallest prime which, by replacing part of the number (not necessarily adjacent digits) with the same digit, is part of an 
                        eight prime value family.
                    ";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 51;
            }
        }

        public override string Solution1()
        {
            List<bool> boolPrimeList = Utils.BoolSieveOfEratosthenes(999999);

            int td = 2;
            bool bFound = false;
            string answer = "";

            while (!bFound)
            {
                // process numbers that has a total of td digits

                for (int r = 1; r < td; r++)
                {
                    List<int> allDigits = new List<int>();
                    for (int i = 1; i <= td; i++) allDigits.Add(i);

                    // replace r out of td digits with '0' - '0'
                    List<List<int>> rList = Utils.CombinationList(allDigits, r);

                    // fixed digit count
                    int fd = td - r;
                    string sFormat = "";
                    for (int i = 1; i <= fd; i++) sFormat = sFormat + "0";

                    // fixed digits number list;
                    List<string> fnList = new List<string>();
                    int lower = 0;
                    int upper = (int)(Math.Pow(10, fd)) - 1;
                    for (int i = lower; i <= upper; i++)
                        fnList.Add(i.ToString(sFormat));

                    foreach (List<int> list in rList)
                    {


                        foreach (string fNumberString in fnList)
                        {
                            int primeCount = 0;
                            int nonPrimeCount = 0;
                            

                            for (int j = 0; j <= 9; j++)
                            {
                                string s = fNumberString;
                                foreach (int k in list)
                                    s = s.Insert(k - 1, j.ToString());

                                if (s[0] == '0') continue;

                                if (boolPrimeList[Convert.ToInt32(s)])
                                    primeCount++;
                                else
                                    nonPrimeCount++;

                                if (nonPrimeCount > 2)
                                    break;
                            }
                            if (primeCount == 8)
                            {
                                bFound = true;
                                string a = fNumberString;
                                foreach (int k in list)
                                    a = a.Insert(k - 1, "x");
                                answer = a;
                            }

                            if (bFound) break;
                        }
                        if (bFound) break;
                    }
                    if (bFound) break;
                }

                if (bFound)
                    break;
                td++;
            }

            return answer;

        }

        public override string Solution2()
        {
            foreach (int i in new List<int> { 121313, 222323, 323333, 424343, 525353, 626363, 727373, 828383, 929393 })
            {
                Console.WriteLine(i + ": " + Utils.IsPrime(i));
            }
            return "";
        }
    }
}
