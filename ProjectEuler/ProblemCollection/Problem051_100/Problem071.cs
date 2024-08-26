using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection
{
    public class Problem71 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 71;
            }
        }

        public override string Description
        {
            get
            {
                return @"Problem 71 - Ordered Fractions

By listing the set of reduced proper fractions for d <= 1000000 in ascending order of size, find the numerator of the fraction immediately to the left of 3/7.                
";
            }
        }

        int upperLimit = 1000000;


        public override string Solution1()
        {
            string idea = @"
            p % (p - 1) > 0
";
Console.WriteLine(idea);
            int answer = upperLimit / 7 * 3 - 1;

            return answer.ToString();
        }

    }
}
