using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem39: ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 39;
            }
        }

        public override string Description
        {
            get
            {
                return @"
                    Integer right triangles
                    Problem 39
                    If p is the perimeter of a right angle triangle with integral length sides, {a,b,c}, there are exactly three solutions for p = 120.

                    {20,48,52}, {24,45,51}, {30,40,50}

                    For which value of p ≤ 1000, is the number of solutions maximised?

                    ";
            }
        }

        int upperLimit = 1000;
        public override string Solution1()
        {
            int maxS = (int)(Math.Sqrt(upperLimit * upperLimit / 2));
            List<int> perimeterList = new List<int>();

            for (int i = 1; i <= maxS; i++)
            {
                for (int j = i; j <= maxS; j++)
                {
                    double k = Math.Sqrt(i * i + j * j);
                    if (k != (int)k) continue;
                    if (i + j + k > upperLimit) continue;

                    perimeterList.Add(i + j + (int)k);
                }
            }

            perimeterList.Sort();

            Dictionary<int, int> pCountDict = new Dictionary<int, int>();
            int p = -1;
            int count = 1;

            foreach (int i in perimeterList)
            {
                if (i != p)
                {
                    pCountDict.Add(p, count);
                    p = i;
                    count = 1;
                }
                else
                {
                    count++;
                }

            }

            int answer = 0;
            int max = 0;
            foreach (int i in pCountDict.Keys)
            {
                if (pCountDict[i] > max)
                {
                    max = pCountDict[i];
                    answer = i;
                }
            }

            return answer.ToString();
        }
    }
}
