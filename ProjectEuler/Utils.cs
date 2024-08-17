using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EulerProject
{
    public class Utils
    {
        public static bool IsPrime(long numm)
        {
            if (numm <= 1)
            {
                return false;
            }

            if (numm == 2)
            {
                return true;
            }

            if (numm % 2 == 0)
            {
                return false;
            }

            int counter = 3;

            while ((counter * counter) <= numm)
            {
                if (numm % counter == 0)
                {
                    return false;
                }
                else
                {
                    counter += 2;
                }
            }

            return true;
        }

        public static List<long> GetAllPrimeUnderP(long p)
        {
            List<long> list = new List<long>();

            for (long i = 2; i <= p; i++)
            {
                if (IsPrime(i))
                    list.Add(i);
            }

            return list;
        }

        public static List<long> IntSieveOfEratosthenes(int p)
        {
            List<bool> bList = new List<bool>();
            for (int i = 0; i <= p; i++)
                bList.Add(true);

            long upperLimit = (long)(Math.Sqrt(p));

            for (int i = 2; i <= upperLimit; i++)
            {
                if (bList[i])
                {
                    for (int k = i + i; k <= p; k += i)
                        bList[k] = false;
                }
            }

            List<long> returnList = new List<long>();
            for (int i = 2; i <= p; i++)
                if (bList[i])
                    returnList.Add(i);

            return returnList;
        }

        public static List<bool> BoolSieveOfEratosthenes(int p)
        {
            List<bool> bList = new List<bool>();
            for (int i = 0; i <= p; i++)
                bList.Add(true);

            long upperLimit = (long)(Math.Sqrt(p));

            for (int i = 2; i <= upperLimit; i++)
            {
                if (IsPrime(i))
                {
                    for (int k = i + i; k <= p; k += i)
                        bList[k] = false;
                }
            }

            return bList;
        }

        public static void SieveOfEratosthenes(int p, ref List<long> primeList, ref List<bool> primeCheckerList)
        {
            List<bool> bList = new List<bool>();
            for (int i = 0; i <= p; i++) bList.Add(true);

            long upperLimit = (long)Math.Sqrt(p);

            for (int i = 2; i <= upperLimit; i++)
                if (bList[i])
                    for (int k = i + i; k <= p; k += i) bList[k] = false;

            primeList = new List<long>();
            for (int i = 2; i <= p; i++)
                if (bList[i])
                    primeList.Add(i);
            
            primeCheckerList = bList.ToList();
        }

        public static void ContinueSieveOfEratosthens(int n, ref List<long> primeList, ref List<bool> primeCheckerList)
        {
            if (n < primeCheckerList.Count) return;
            List<bool> bList = new List<bool>();
            for (int i = 0; i < primeCheckerList.Count; i++) bList.Add(primeCheckerList[i]);
            for (int i = primeCheckerList.Count; i < n; i++) bList.Add(true);

            long upperLimit = (long)Math.Sqrt(n);

            for (int i = 2; i <= upperLimit; i++)
                if (bList[i])
                {
                    int start = primeCheckerList.Count - 1 + i - ((primeCheckerList.Count - 1) % i);
                    for (int k = start; k < n; k += i) bList[k] = false;
                }

            primeList = new List<long>();
            for (int i = 2; i < n; i++)
                if (bList[i])
                    primeList.Add(i);
            
            primeCheckerList = bList.ToList();

        }

        public static List<long> AllPrimesUnder2Million_CheatSheet()
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(@"C:\Users\yangj\Test\temp\EulerProject\EulerProject\a.txt");
            string s = sr.ReadToEnd();
            sr.Close();
            List<long> allPrimesUnder2Million = new List<long>();

            for (long i = 0; i < s.Length; i++)
            {
                if (s[(int)i] == '1')
                {
                    allPrimesUnder2Million.Add(i + 1);
                }
            }

            return allPrimesUnder2Million;
        }

        public static List<long> AllDivisors(long n)
        {
            List<long> list = new List<long>();
            for (long d = 1; d < Math.Sqrt(n); d++)
            {
                if (n % d == 0) { list.Add(d); list.Add(n / d); }
            }

            if (Math.Sqrt(n) == (long)(Math.Sqrt(n)))
                list.Add((long)(Math.Sqrt(n)));

            return list;
        }

        public static int NumberOfDivisors(long n)
        {
            int ret = 0;
            for (long d = 1; d < Math.Sqrt(n); d++)
            {
                if (n % d == 0) { ret += 2; }
            }

            if (Math.Sqrt(n) == (long)(Math.Sqrt(n)))
                ret++;

            return ret;
        }

        public static int GetGcd(int m, int n)
        {
            if (m % n == 0) return n;
            if (m > n) return GetGcd(m, m % n);
            return GetGcd(n, n % m);
        }

        public static Dictionary<int, int> GetPrimeFactor(long m)
        {
            int i = 2;
            Dictionary<int, int> ret = new Dictionary<int, int>();

            while (i < Math.Sqrt(m))
            {
                while (m % i == 0)
                {
                    if (!ret.Keys.Contains(i))
                        ret.Add(i, 1);
                    else
                        ret[i]++;

                    m /= i;
                }

                i++;
            }

            if (m > 1)
            {
                ret.Add((int)m, 1);
            }

            return ret;
        }

        public static long GetNumberOfDivisors(long m)
        {
            Dictionary<int, int> primeFactor = GetPrimeFactor(m);

            long ret = 1;
            foreach (int key in primeFactor.Keys)
            {
                ret *= primeFactor[key] + 1;
            }

            return ret;
        }

        public static string stringMultiply(string number, int by)
        {
            int[] digitsAfterMultiply = new int[number.Length];
            for (int i = 0; i < number.Length; i++)
                digitsAfterMultiply[i] = (number[i] - '0') * by;

            StringBuilder sbResult = new StringBuilder();
            int next = 0;
            for (int i = digitsAfterMultiply.Length - 1; i >= 0; i--)
            {
                digitsAfterMultiply[i] += next;
                next = digitsAfterMultiply[i] / 10;
                sbResult.Insert(0, (char)(digitsAfterMultiply[i] % 10 + '0'));
            }

            if (next > 0) sbResult.Insert(0, next.ToString());

            return sbResult.ToString();
        }

        public static string StringAddition(string sNumber1, string sNumber2)
        {

            if (sNumber1.Length > sNumber2.Length)
                sNumber2 = sNumber2.PadLeft(sNumber1.Length, '0');
            else if (sNumber1.Length < sNumber2.Length)
                sNumber1 = sNumber1.PadLeft(sNumber2.Length, '0');

            int[] digitsAfterAddition = new int[sNumber1.Length];

            for (int index = sNumber1.Length - 1; index >= 0; index--)
                digitsAfterAddition[index] = sNumber1[index] + sNumber2[index] - '0' - '0';

            // 进位
            StringBuilder sbResult = new StringBuilder();
            int next = 0; 
            for (int i = digitsAfterAddition.Length - 1; i >= 0; i--)
            {
                digitsAfterAddition[i] += next;
                next = digitsAfterAddition[i] / 10;
                sbResult.Insert(0, (char)(digitsAfterAddition[i] % 10 + '0'));
            }

            if (next > 0) sbResult.Insert(0, next.ToString());

            return sbResult.ToString();
        }

        public static string StringDivision(int numerator, int denominator)
        {
            int placeHolder = int.MaxValue;

            string result = "";
            result = (numerator / denominator).ToString() + ".";

            List<int> remainderList = new List<int> { };
            int remainder = numerator % denominator;
            int iStart = 0;
            int iStartRepeatRemainder = 0;

            while (remainder > 0)
            {
                if (remainderList.Contains(remainder))
                {
                    iStartRepeatRemainder = remainder;
                    for (int i = 0; i < remainderList.Count; i++)
                    {
                        if (remainderList[i] == remainder)
                        {
                            iStart = i;
                            break;
                        }
                    }

                    result = result + "(" + (remainderList.Count - iStart).ToString() + "...)";
                    break;
                }

                remainderList.Add(remainder);

                numerator = remainder * 10;
                while (numerator < denominator)
                {
                    remainderList.Add(placeHolder --);

                    result = result + '0';
                    numerator *= 10;
                }

                result = result + (numerator / denominator).ToString();
                remainder = numerator % denominator;
            }

            return result;
        }

        public static long SumOfProperDivisors(long number)
        {
            long intSqrt = (long)(Math.Sqrt(number));
            long sum = 1;

            for (long i = 2; i <= intSqrt; i++)
            {
                if (number % i == 0)
                {
                    sum += i;
                    long j = (number / i);
                    if (j > i)
                        sum += j;
                }
            }

            return sum;
        }

        public static System.Numerics.BigInteger Combination(int m, int n)
        {
            System.Numerics.BigInteger result = Permutation(m) / Permutation(n) / Permutation(m - n);

            return result;
        }

        public static System.Numerics.BigInteger Permutation(int n)
        {
            System.Numerics.BigInteger result = 1;
            for (long i = 1; i <= n; i++)
                result *= i;

            return result;
        }

        public static System.Numerics.BigInteger Pow(System.Numerics.BigInteger root, int power)
        {
            System.Numerics.BigInteger result = 1;
            for (int i = 0; i < power; i++)
                result *= root;

            return result;
        }

        public static List<List<T>> PermutationList<T>(List<T> list)
        {
            List<List<T>> resultList = new List<List<T>>();
            if (list.Count == 1)
            {
                resultList.Add(list);
                return resultList;
            }

            for (int i = 0; i < list.Count; i++)
            {
                List<T> tempList = new List<T>();
                foreach (T t in list)
                    tempList.Add(t);

                T f = list[i];
                tempList.Remove(list[i]);
                List<List<T>> subList = PermutationList(tempList);

                foreach (List<T> subListResult in subList)
                {
                    List<T> newList = new List<T> { f };
                    foreach (T t in subListResult)
                    {
                        newList.Add(t);
                    }

                    resultList.Add(newList);
                }
            }

            return resultList;
        }

        public static List<List<T>> CircularList<T>(List<T> list)
        {
            List<List<T>> resultList = new List<List<T>>();
            if (list.Count == 1)
            {
                resultList.Add(list);
                return resultList;
            }

            resultList.Add(list);

            for (int i = 1; i <= list.Count - 1; i++)
            {
                List<T> tempList = new List<T>();
                for (int j = 0; j < list.Count; j++)
                {
                    tempList.Add(list[(j + i) % list.Count]);
                }
                resultList.Add(tempList);
            }

            return resultList;
        }

        public static List<List<T>> CombinationList<T>(List<T> list, int m)
        {
            #region final
            List<List<T>> resultList = new List<List<T>>();
            if (list.Count < m)
            {
                return resultList;
            }
            else if (list.Count == m)
            {
                resultList.Add(list);
                return resultList;
            }
            else if (m == 1)
            {
                foreach (T t in list)
                {
                    resultList.Add(new List<T> { t });
                }

                return resultList;
            }
            else if (m == 0)
            {
                return new List<List<T>>();
            }
            #endregion

            for (int i = 0; i < list.Count; i++)
            {
                List<T> tempList = new List<T>();
                foreach (T t in list)
                    tempList.Add(t);

                T f = list[i];
                for (int j = 0; j <= i; j++)
                    tempList.Remove(list[j]);

                List<List<T>> subList = CombinationList(tempList, m - 1).Distinct().ToList();

                foreach (List<T> subListResult in subList)
                {
                    List<T> newList = new List<T> { f };
                    foreach (T t in subListResult)
                    {
                        newList.Add(t);
                    }

                    resultList.Add(newList);
                }
            }

            return resultList;
        }

        public static bool IsPandigital(string s, int p)
        {
            if (s.Length != p)
                return false;

            for (char c = '1'; c <= '0' + p; c++)
            {
                if (!s.Contains(c))
                    return false;
            }

            return true;
        }
    }
}
