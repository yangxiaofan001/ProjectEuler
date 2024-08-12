using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem50 : ProblemBase
    {
        public override string Description
        {
            get
            {
                return @"
                    Consecutive prime sum
                    Problem 50

                    The prime 41, can be written as the sum of six consecutive primes:

                    41 = 2 + 3 + 5 + 7 + 11 + 13
                    This is the longest sum of consecutive primes that adds to a prime below one-hundred.

                    The longest sum of consecutive primes below one-thousand that adds to a prime, contains 21 terms, 
                    and is equal to 953.

                    Which prime, below one-million, can be written as the sum of the most consecutive primes?
                    ";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 50;
            }
        }

        public override string Solution1()
        {
            int sum = 0;
            int maxSteps = 0;
            string answer = "";
            int upperLimit = 1000000;
            List<bool> boolPrimeList = Utils.BoolSieveOfEratosthenes(upperLimit);
            //List<long> intPrimeList = Utils.IntSieveOfEratosthenes(upperLimit);


            for (int i = 2; i <= upperLimit; i++)
            {
                int nextPrime = i + 1;
                sum = i;
                List<int> steps = new List<int>();
                steps.Add(i);
                while (nextPrime < upperLimit)
                {
                    while (!boolPrimeList[nextPrime] && nextPrime < upperLimit)
                        nextPrime++;

                    sum += nextPrime;
                    steps.Add(nextPrime);
                    if (sum > upperLimit)
                        break;

                    if (boolPrimeList[sum])
                    {
                        if (steps.Count > maxSteps)
                        {
                            string s = sum + " = ";
                            foreach (int x in steps)
                                s = s + x.ToString() + " + ";
                            answer = s;
                            maxSteps = steps.Count;
                        }
                    }

                    nextPrime++;

                }
                steps.Clear();
            }

            return answer;
        }
    }

}
