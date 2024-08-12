using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EulerProject.ProblemCollection
{
    public class Problem14 : ProblemBase
    {
        const long upperLimit = 1000000;

        public override string Description
        {
            get
            {
                return 
                    "Longest Collatz Sequence\n" +
                    "The following iterative sequence is defined for the set of positive integers:\n" +
                    "n → n/2 (n is even)\n" +
                    "n → 3n + 1 (n is odd)\n" +
                    "Using the rule above and starting with 13, we generate the following sequence:\n" +
                    "13 → 40 → 20 → 10 → 5 → 16 → 8 → 4 → 2 → 1\n" +
                    "It can be seen that this sequence (starting at 13 and finishing at 1) contains 10 terms. Although it has not been proved yet (Collatz Problem), it is thought that all starting numbers finish at 1.\n" +
                    "Which starting number, under " + upperLimit.ToString() + " (1 million in the original question), produces the longest chain?";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 14;
            }
        }

        public override string Solution1()
        {
            long maxSteps = -1;
            long answer = -1;
            for (long i = 1; i <= upperLimit; i++)
            {
                long j = i;
                long steps = 0;
                while (j != 1)
                {
                    if (j % 2 == 0)
                        j /= 2;
                    else
                        j = j * 3 + 1;
                    steps++;
                }

                if (steps > maxSteps)
                {
                    maxSteps = steps;
                    answer = i;
                }
            }

            return "Starting number " + answer.ToString() + " produces the longest chain of " + maxSteps.ToString() + " steps.";
        }


        public override string Solution2()
        {
            Dictionary<long, long> solvedSteps = new Dictionary<long, long>();

            long maxSteps = -1;
            long answer = -1;

            for (long i = 2; i <= upperLimit; i++)
            {
                long j = i;
                long steps = 0;

                while (j >= i)
                {
                    if (j % 2 == 0)
                        j /= 2;
                    else
                        j = j * 3 + 1;
                    steps++;
                }

                if (j > 1)
                {
                    steps = steps + solvedSteps[j];
                }

                solvedSteps.Add(i, steps);

                if (steps > maxSteps)
                {
                    maxSteps = steps;
                    answer = i;
                }
            }

            return "Starting number " + answer.ToString() + " produces the longest chain of " + maxSteps.ToString() + " steps.";
        }
    }
}
