using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EulerProject.ProblemCollection.Problem051_100
{
    public class Problem059 : ProblemBase
    {
        public override string Description
        {
            get
            {
                return @"
Problem 59 - XOR Decryption

Each character on a computer is assigned a unique code and the preferred standard is ASCII (American Standard Code for Information Interchange). For example, uppercase A = 65, asterisk (*) = 42, and lowercase k = 107.

A modern encryption method is to take a text file, convert the bytes to ASCII, then XOR each byte with a given value, taken from a secret key. The advantage with the XOR function is that using the same encryption key on the cipher text, restores the plain text; for example, 65 XOR 42 = 107, then 107 XOR 42 = 65.

For unbreakable encryption, the key is the same length as the plain text message, and the key is made up of random bytes. The user would keep the encrypted message and the encryption key in different locations, and without both 'halves', it is impossible to decrypt the message.

Unfortunately, this method is impractical for most users, so the modified method is to use a password as a key. If the password is shorter than the message, which is likely, the key is repeated cyclically throughout the message. The balance for this method is using a sufficiently long password key for security, but short enough to be memorable.

Your task has been made easy, as the encryption key consists of three lower case characters. Using 0059_cipher.txt (right click and 'Save Link/Target As...'), a file containing the encrypted ASCII codes, and the knowledge that the plain text must contain common English words, decrypt the message and find the sum of the ASCII values in the original text.
";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 59;
            }
        }

        int DecryptXOR(int c, int x)
        {
            List<bool> cInBinary = new List<bool>();
            List<bool> xInBinary = new List<bool>();

            while (c > 0)
            {
                cInBinary.Insert(0, c%2==1);
                c/=2;
            }

            while(x > 0)
            {
                xInBinary.Insert(0, x%2==1);
                x/=2;
            }

            int outputLength = Math.Max(cInBinary.Count, xInBinary.Count);
            int cInBinaryCount = cInBinary.Count;
            int xInBinaryCount = xInBinary.Count;
            for(int i = 0; i < outputLength - cInBinaryCount; i ++) cInBinary.Insert(0, false);
            for(int i = 0; i < outputLength - xInBinaryCount; i ++) xInBinary.Insert(0, false);

            List<bool> returnValueInBinary = new List<bool>();
            for(int i = 0; i < outputLength; i ++)
            {
                returnValueInBinary.Add(cInBinary[i] == xInBinary[i]);
            }

            int decryptedCode = 0;
            for(int i = outputLength - 1; i >= 0; i --)
            {
                decryptedCode = 2 * decryptedCode + (returnValueInBinary[i] ? 1 : 0);
            }

            return decryptedCode;

        }


        public override string Solution1()
        {
            // c(26, 3) = 2600

            System.IO.StreamReader sr = new StreamReader("Files/0059_cipher.txt");
            string line = sr.ReadLine();
            sr.Close();
            string [] codeArray = line.Split(new char []{','}, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine($"{codeArray.Length} characters");

            List<int> possibleP1List = new List<int>();
            for(char p1 = 'a'; p1 <= 'z'; p1++)
            {
                int d = DecryptXOR(Int32.Parse(codeArray[0]), p1);
                if ((d >='a' && d <='z')||(d>='A'&&d<='Z'))
                {
                    possibleP1List.Add(p1);
                }
            }

            List<int> possibleP2List = new List<int>();
            for(char p1 = 'a'; p1 <= 'z'; p1++)
            {
                int d = DecryptXOR(Int32.Parse(codeArray[1]), p1);
                if ((d >='a' && d <='z')||(d>='A'&&d<='Z'))
                {
                    possibleP2List.Add(p1);
                }
            }

            List<int> possibleP3List = new List<int>();
            for(char p1 = 'a'; p1 <= 'z'; p1++)
            {
                int d = DecryptXOR(Int32.Parse(codeArray[2]), p1);
                if ((d >='a' && d <='z')||(d>='A'&&d<='Z'))
                {
                    possibleP3List.Add(p1);
                }
            }

            Console.Write("Possible p1: ");
            foreach(int c in possibleP1List) Console.Write($"{(char)c} ");
            Console.WriteLine();

            Console.Write("Possible p2: ");
            foreach(int c in possibleP2List) Console.Write($"{(char)c} ");
            Console.WriteLine();

            Console.Write("Possible p3: ");
            foreach(int c in possibleP3List) Console.Write($"{(char)c} ");
            Console.WriteLine();

            List<int> toBeRemovedFromP1List = new List<int>();
            foreach (char p1 in possibleP1List)
            {
                int d = DecryptXOR(Int32.Parse(codeArray[3]), p1);
                if ((d <='a' || d >='z') &&(d <='A' || d >= 'Z'))
                    toBeRemovedFromP1List.Add(p1);
            }

            foreach(char p1 in toBeRemovedFromP1List) possibleP1List.Remove(p1);

            List<int> toBeRemovedFromP2List = new List<int>();
            foreach (char p2 in possibleP2List)
            {
                int d = DecryptXOR(Int32.Parse(codeArray[4]), p2);
                if ((d <='a' || d >='z') &&(d <='A' || d >= 'Z'))
                    toBeRemovedFromP2List.Add(p2);
            }

            foreach(char p2 in toBeRemovedFromP2List) possibleP2List.Remove(p2);

            List<int> toBeRemovedFromP3List = new List<int>();
            foreach (char p3 in possibleP3List)
            {
                int d = DecryptXOR(Int32.Parse(codeArray[5]), p3);
                if ((d <='a' || d >='z') &&(d <='A' || d >= 'Z'))
                    toBeRemovedFromP3List.Add(p3);
            }

            foreach(char p3 in toBeRemovedFromP3List) possibleP3List.Remove(p3);

            Console.Write("Possible p1: ");
            foreach(int c in possibleP1List) Console.Write($"{(char)c} ");
            Console.WriteLine();

            Console.Write("Possible p2: ");
            foreach(int c in possibleP2List) Console.Write($"{(char)c} ");
            Console.WriteLine();

            Console.Write("Possible p3: ");
            foreach(int c in possibleP3List) Console.Write($"{(char)c} ");
            Console.WriteLine();

            // string password = "god";
            // bool solved = false;
            // for(char p1 = 'a'; p1 <= 'z'; p1++)
            // {
            //     for(char p2 = 'a'; p2 <= 'z'; p2 ++)
            //     {
            //         for(char p3 = 'a'; p3 <= 'z'; p3 ++)
            //         {
            //             StringBuilder sb = new StringBuilder();
            //             sb.Append(p1);
            //             sb.Append(p2);
            //             sb.Append(p3);
            //             password = sb.ToString();
            //             int index = 0;
            //             string decryptedText = "";
            //             bool failed = false;

            //             foreach(string code in codeArray)
            //             {
            //                 char c = (char)DecryptXOR(Int32.Parse(code), (int)password[index % 3]);
            //                 if ((c < 'a' || c > 'z') && (c<'A' || c > 'Z')) {
            //                     failed = true;
            //                     break;
            //                 }
            //                 decryptedText = decryptedText + c;
            //                 index ++;
            //             }

            //             string result = failed ? "failed" : "succeeded";
            //             Console.WriteLine($"password = {password} {result}: {decryptedText}");

            //             if (!failed)
            //             {
            //                 solved = true;
            //                 break;
            //             }
            //         }

            //         if (solved) break;
            //     }
            //     if (solved) break;
            // }
            
            string answer = "Working on it...";

            return answer;
        }
    }
}
