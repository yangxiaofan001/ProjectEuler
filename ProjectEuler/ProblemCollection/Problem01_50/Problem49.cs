using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem49 : ProblemBase
    {
        public override string Description
        {
            get
            {
                return @"
                    Prime permutations
                    Problem 49

                    The arithmetic sequence, 1487, 4817, 8147, in which each of the terms increases by 3330, is unusual 
                    in two ways: (i) each of the three terms are prime, and, (ii) each of the 4-digit numbers are 
                    permutations of one another.

                    There are no arithmetic sequences made up of three 1-, 2-, or 3-digit primes, exhibiting this property, 
                    but there is one other 4-digit increasing sequence.

                    What 12-digit number do you form by concatenating the three terms in this sequence?
                    ";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 49;
            }
        }

        public override string Solution1()
        {
            List<long> primeList = Utils.IntSieveOfEratosthenes(9999);
            List<long> ignoreList = new List<long>();
            List<long> toBeProcessedList = new List<long>();
            string answer = "";

            foreach (long prime in primeList.Where(p => p > 1000))
            {
                if (ignoreList.Contains(prime))
                    continue;

                if (prime == 1487)
                    continue;

                List<long> digitList = new List<long>();
                digitList.Add(prime / 1000);
                digitList.Add(prime % 1000 / 100);
                digitList.Add(prime % 100 / 10);
                digitList.Add(prime % 10);

                List<List<long>> permutationList = Utils.PermutationList<long>(digitList);
                List<long> twentyFourNumbers = new List<long>();

                foreach (List<long> permu in permutationList)
                {
                    long number = permu[0] * 1000 + permu[1] * 100 + permu[2] * 10 + permu[3];
                    if (number < 1000)
                        continue;

                    if (primeList.Contains(number))
                    {
                        if (!twentyFourNumbers.Contains(number))
                            twentyFourNumbers.Add(number);

                        if (number != prime)
                        {
                            if (!ignoreList.Contains(number))
                                ignoreList.Add(number);
                        }
                    }
                }

                if (twentyFourNumbers.Count >= 3)
                {
                    List<List<long>> combinationList = Utils.CombinationList<long>(twentyFourNumbers, 3);
                    foreach (List<long> threeNumbers in combinationList)
                    {
                        threeNumbers.Sort();
                        if (threeNumbers[2] - threeNumbers[1] == threeNumbers[1] - threeNumbers[0])
                        {
                            answer = threeNumbers[0].ToString() + threeNumbers[1].ToString() + threeNumbers[2].ToString();
                        }
                    }
                }
            }



            return answer;
        }
    }
}
