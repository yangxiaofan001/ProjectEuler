using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection.Problem051_100
{
    public class Problem053 : ProblemBase
    {
        public override string Description
        {
            get
            {
                return @"
Problem 53 - Combinatoric Selections
How many, not necessarily distinct, values of c(n, m) for 1 <=n <= 100, are greater than one-million?.
                    ";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 53;
            }
        }

        long upperLimit = 1000000;

        public override string Solution1()
        {
            Console.WriteLine("Theorem: c(n, m) = c(n - 1, m) + c(n -1, n - m)");
            int lastN = 1;
            int count = 0;
            Dictionary<int, long> lastCN = new Dictionary<int, long>{
                {0, 1}, {1, 1}
            };
            
            for(int n = 2; n <= 100; n ++)
            {
                Dictionary<int, long> cn = new Dictionary<int, long>();
                for(int m = 1; m < n; m ++)
                {
                    long cnm = lastCN[m] + lastCN[n - m]; 
                    if (cnm > upperLimit) 
                    {
                        // set it to 1M, otherwise it will exceed long.MaxValue, which causes trouble
                        // it does not matter if 1M is incorrect, we only need to know if the number is over 1M
                        cnm = upperLimit; 
                        count ++;
                    }
                    cn.Add(m, cnm);
                }
                cn.Add(n, 1);

                lastN = n;
                lastCN.Clear();
                foreach(int k in cn.Keys)  
                    lastCN.Add(k, cn[k]);
            }

            string answer = count.ToString();

            return answer;
        }
    }
}
