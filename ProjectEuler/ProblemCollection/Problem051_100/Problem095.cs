using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection.Problem051_100
{
    public class Problem095 : ProblemBase
    {
        public override string Description
        {
            get
            {
                return "095";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 95;
            }
        }

        public override string Solution1()
        {
            List<int> divisorSumList = new List<int>();
            int upperLimit = 1000000;
            int sqrtUpperLimit = (int)(Math.Sqrt(upperLimit));

            for (int i = 0; i <= upperLimit; i++)
                divisorSumList.Add(1);

            for (int d = 2; d <= sqrtUpperLimit; d++)
            {
                int d2 = d * d;
                divisorSumList[d2] += d;

                for (int x = d2 + d; x <= upperLimit; x += d)
                {
                    divisorSumList[x] += d + x / d;
                }
            }

            List<int?> chainSteps = new List<int?>();
            for (int i = 0; i <= upperLimit; i++)
                chainSteps.Add(null);

            chainSteps[0] = -1;
            chainSteps[1] = -1;
            int maxStep = 0;
            int answer = 0;

            for (int x = 2; x <= upperLimit; x++)
            {
                if (x == 12496)
                {
                    int y = 1;
                }
                if (chainSteps[x].HasValue) continue;

                int currX = x;
                int dsum = divisorSumList[x];
                int step = 1;
                List<int> chain = new List<int> { x };

                if (dsum == x)
                    chainSteps[x] = step;
                else
                {
                    while (true)
                    {
                        if (dsum == 12496)
                        {
                            int z = 1;
                        }
                        if (dsum == x)
                            break;

                        if (dsum > upperLimit || chainSteps[dsum].HasValue || chain.Contains(dsum))
                        {
                            step = -1;
                            break;
                        }

                        chain.Add(dsum);
                        currX = dsum;
                        dsum = divisorSumList[currX];
                        step++;
                    }
                }

                //foreach (int c in chain)
                    chainSteps[x] = step;

                if (step > maxStep)
                {
                    maxStep = step;
                    answer = x;
                }
            }

            List<int?> validChainSteps = chainSteps.Where(c => c.HasValue && c.Value > 0).ToList();

            return answer.ToString();
        }
    }
}
