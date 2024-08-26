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
    public class Problem70 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 70;
            }
        }

        public override string Description
        {
            get
            {
                return @"
";
            }
        }

        int upperLimit = 10000000;

        string SortedDigits(int n)
        {
            char[] ca = n.ToString().ToCharArray();
            Array.Sort(ca);

            return new string(ca);
        }


        public override string Solution1()
        {
            string idea = @"
similar to finding primes under N, use sieve to find phi(n) for all n < 10^7
loop through each n, if SortedDigits(n) == SortedDigits(phiArray[n]), calculate ratio n/phi(n), find n with the smallest ratio
";
Console.WriteLine(idea);

DateTime dt1 = DateTime.Now;

            List<int> phiArray = Utils.GetAllPhiUnderP(upperLimit);
DateTime dt2 = DateTime.Now;
Console.WriteLine($"1. Calculate phi from 2 to {upperLimit}: {(dt2 - dt1).TotalMilliseconds}");
dt1 = DateTime.Now;


            double minNPhiNRatio = 100;
            int answer = 0;

            for(int i = 2; i <= upperLimit; i ++)
            {
                if (SortedDigits(i) == SortedDigits(phiArray[i]))
                {
                    double nPhiRatio = (double) i / (double)phiArray[i];

                    if (nPhiRatio < minNPhiNRatio)
                    {
                        answer = i;
                        minNPhiNRatio = nPhiRatio;
                    }
                }
            }
dt2 = DateTime.Now;
Console.WriteLine($"2. Calculate nPhiRation and find the smallest: {(dt2 - dt1).TotalMilliseconds}");
// Console.WriteLine($"{TotalMilliseconds} were spent on SortedDigits(n)");

            return answer.ToString();
        }

        public override string Solution2()
        {
DateTime dt1 = DateTime.Now;
            int sqrt = (int)Math.Sqrt(upperLimit);

            bool[] primeChecker = new bool[sqrt + 1];
            for(int i = 0; i <= sqrt; i++) primeChecker[i] = true;

DateTime dt2 = DateTime.Now;
Console.WriteLine($"1. initialize primeChecker[{sqrt}]: {(dt2 - dt1).TotalMilliseconds}");
dt1 = dt2;
            Dictionary<int, string> permutationMap = new Dictionary<int, string>();
            for(int i = 0; i <= upperLimit; i ++)
            {
                char[] ca = i.ToString().ToCharArray();
                Array.Sort(ca);

                permutationMap.Add(i, new string(ca));
            } 
dt2 = DateTime.Now;
Console.WriteLine($"2. build permutationmap: {(dt2 - dt1).TotalMilliseconds}");
dt1 = dt2;            
        
            Dictionary<int, List<int>> primeFactorMap = new Dictionary<int, List<int>>();
            for(int i = 0; i <= upperLimit; i ++) primeFactorMap.Add(i, new List<int>());
dt2 = DateTime.Now;
Console.WriteLine($"3.0. initialize primeFactorMap: {(dt2 - dt1).TotalMilliseconds}");
dt1 = dt2;    
            for(int i = 2; i <= sqrt; i ++)
            {
                if (primeChecker[i])
                {
                    primeFactorMap[i].Add(i);
                    for(int j = i * 2; j <= upperLimit; j += i)
                    {
                        if (j <= sqrt) primeChecker[j] = false;
                        primeFactorMap[j].Add(i);
                    }
                }
            }
dt2 = DateTime.Now;
Console.WriteLine($"3. Build primeFactorMap, prime factor over {sqrt} are not added: {(dt2 - dt1).TotalMilliseconds}");
dt1 = dt2;            

            for(int i = 2; i <= upperLimit; i ++)
            {
                int x = i;
                foreach(int p in primeFactorMap[i])
                {
                    while(x % p == 0) 
                    {
                        x /= p;
                    }
                }
                if (x > sqrt) primeFactorMap[i].Add(x);
            }
dt2 = DateTime.Now;
Console.WriteLine($"4. Continue building primeFactorMap, added prime factor over {sqrt}: {(dt2 - dt1).TotalMilliseconds}");
dt1 = dt2;            

            double minNPhiNRatio = 100;
            int answer = 0;

            for(int i = 2; i <= upperLimit; i ++)
            {
                int phi = i;
                foreach(int p in primeFactorMap[i])
                {

                    phi = phi / p * (p - 1);

                }
                if (permutationMap[i] == permutationMap[phi])
                {
                    
                    double nPhiRatio = (double) i / (double)phi;

                    if (nPhiRatio < minNPhiNRatio)
                    {
                        answer = i;
                        minNPhiNRatio = nPhiRatio;
                    }
                }
            }
dt2 = DateTime.Now;
Console.WriteLine($"5. Calculate nPhiRation and find the smallest: {(dt2 - dt1).TotalMilliseconds}");

            return answer.ToString() + ";" + "Brutal force took 20 seconds, looking for a better solution";
        }
    }
}
