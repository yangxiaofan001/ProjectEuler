using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace EulerProject.ProblemCollection
{
    public class Problem74 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 74;
            }
        }

        public override string Description
        {
            get
            {
                return @"Problem 74 - Digit Factorial Chains
";
            }
        }

        int upperLimit = 1000000;

        public override string Solution1()
        {
            string idea = @"Just do it. 1136 milliseconds.";
Console.WriteLine(idea);

int answer = 0;
            int [] factorials = new int[10];
            factorials[0] = 1;
            for(int i = 1; i < 10; i ++)
                factorials[i] = i * factorials[i - 1];

            int [] chainLengthArray = new int[upperLimit + 1];
            for(int i = 0; i <= upperLimit; i++) chainLengthArray[i] = 0;

            for(int i = 0; i <= upperLimit; i ++)
            {

                List<int> chain = new List<int>();
                int sum = 0;
                int x = i;
                while(chain.Count < 60)
                {
                    sum = 0;
                    foreach(char c in x.ToString())
                        sum += factorials[c - '0'];

                    if (chain.Contains(sum))
                    {
                        chainLengthArray[i] = chain.Count;
                        break;
                    } 

                    chain.Add(sum);
                    x = sum;
                }

                if (chain.Count == 59) answer ++;
            }
            
            return answer.ToString();
        }
    }
}
