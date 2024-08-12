using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem35 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 35;
            }
        }

        public override string Description
        {
            get
            {
                return @"
                    Circular primes
                    Problem 35
                    The number, 197, is called a circular prime because all rotations of the digits: 197, 971, and 719, are themselves prime.

                    There are thirteen such primes below 100: 2, 3, 5, 7, 11, 13, 17, 31, 37, 71, 73, 79, and 97.

                    How many circular primes are there below one million?
                ";
            }
        }

        public override string Solution1()
        {
            List<long> allPrimeList = Utils.GetAllPrimeUnderP(1000000);
            List<long> allPossibleCircularPrimeList = new List<long>();
            int count = 0;

            foreach (long p in allPrimeList)
            {
                bool bAdd = true;
                foreach (char c in p.ToString())
                {
                    if (("024568").Contains(c))
                    {
                        bAdd = false;
                        break;
                    }
                }

                if (bAdd)
                    allPossibleCircularPrimeList.Add(p);
            }

            allPossibleCircularPrimeList.Insert(0, 2);
            allPossibleCircularPrimeList.Insert(2, 5);

            while  (allPossibleCircularPrimeList.Count > 0)
            {
                List<char> cList = new List<char>();
                foreach (char c in allPossibleCircularPrimeList[0].ToString())
                    cList.Add(c);

                List<List<char>> permutationCharArrayList = Utils.PermutationList<char>(cList);
                List<long> permutationLongList = new List<long>();
                bool bIsCircularPrime = true;
                long number = 0;
                List<long> removedPrime = new List<long>();
                foreach (List<char> list in permutationCharArrayList)
                {
                    number = 0;
                    int pow = 1;
                    for (int l = list.Count - 1; l >= 0; l--)
                    {
                        number += (list[l] - '0') * pow;
                        pow *= 10;
                    }

                    if (number == 179)
                    {
                        int x = 1;
                    }

                    permutationLongList.Add(number);
                    if (allPossibleCircularPrimeList[0] == number)
                        continue;

                    if (!allPossibleCircularPrimeList.Contains(number) && !removedPrime.Contains(number))
                    {
                        bIsCircularPrime = false;
                    }
                    else
                    {
                        allPossibleCircularPrimeList.Remove(number);
                        removedPrime.Add(number);
                    }
                }

                if (bIsCircularPrime)
                {
                    foreach (long n in permutationLongList.Distinct())
                    {
                        Console.WriteLine(n);
                        count++;
                    }
                }

                allPossibleCircularPrimeList.Remove(allPossibleCircularPrimeList[0]);
            }



            return "Permutation prime: " + count.ToString();
        }

        public override string Solution2()
        {
            List<long> allPrimeList = Utils.IntSieveOfEratosthenes(1000000);
            List<long> allPossibleCircularPrimeList = new List<long>();
            int count = 0;

            foreach (long p in allPrimeList)
            {
                bool bAdd = true;
                foreach (char c in p.ToString())
                {
                    if (("024568").Contains(c))
                    {
                        bAdd = false;
                        break;
                    }
                }

                if (bAdd)
                    allPossibleCircularPrimeList.Add(p);
            }

            allPossibleCircularPrimeList.Insert(0, 2);
            allPossibleCircularPrimeList.Insert(2, 5);

            while (allPossibleCircularPrimeList.Count > 0)
            {
                List<char> cList = new List<char>();
                foreach (char c in allPossibleCircularPrimeList[0].ToString())
                    cList.Add(c);

                List<List<char>> circularCharArrayList = Utils.CircularList<char>(cList);
                List<long> circularLongList = new List<long>();
                bool bIsCircularPrime = true;
                long number = 0;
                List<long> removedPrime = new List<long>();
                foreach (List<char> list in circularCharArrayList)
                {
                    number = 0;
                    int pow = 1;
                    for (int l = list.Count - 1; l >= 0; l--)
                    {
                        number += (list[l] - '0') * pow;
                        pow *= 10;
                    }


                    circularLongList.Add(number);
                    if (allPossibleCircularPrimeList[0] == number)
                        continue;

                    if (!allPossibleCircularPrimeList.Contains(number) && !removedPrime.Contains(number))
                    {
                        bIsCircularPrime = false;
                    }
                    else
                    {
                        allPossibleCircularPrimeList.Remove(number);
                        removedPrime.Add(number);
                    }
                }

                if (bIsCircularPrime)
                {
                    foreach (long n in circularLongList.Distinct())
                    {
                        Console.WriteLine(n);
                        count++;
                    }
                }

                allPossibleCircularPrimeList.Remove(allPossibleCircularPrimeList[0]);
            }



            return "Circular prime: " + count.ToString();
        }

    }
}
