using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
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

        public override string Solution1()
        {
            string idea = @"
the key is 'the plain text must contain common English words'

To verify this, check the decrypted text contains ' a ' and ' the '
            ";

            Console.WriteLine(idea);
            
            System.IO.StreamReader sr = new StreamReader("Files/0059_cipher.txt");
            string line = sr.ReadLine();
            sr.Close();
            string [] codeCharArray = line.Split(new char []{','}, StringSplitOptions.RemoveEmptyEntries);
            uint [] codeArray = new uint [codeCharArray.Length];
            for(int i = 0; i < codeCharArray.Length; i ++)
            {
                codeArray[i] = uint.Parse(codeCharArray[i]);
            }

            uint [] password = new uint[3];
            bool solved = false;
            long sum = 0;

            for(char p1 = 'a'; p1 <= 'z'; p1++)
            {
                for(char p2 = 'a'; p2 <= 'z'; p2 ++)
                {
                    for(char p3 = 'a'; p3 <= 'z'; p3 ++)
                    {
                        password[0] = p1;
                        password[1] = p2;
                        password[2] = p3;
                        int index = 0;
                        string decryptedText = "";

                        foreach(uint code in codeArray)
                        {
                            uint c = code ^ password[index % 3];
                            decryptedText = decryptedText + (char)c;
                            index ++;
                        }

                        // the plain text must contain common English words
                        // chech the decrypted text contain " a " and " the "
                        if (decryptedText.IndexOf(" a ") >= 0 && decryptedText.IndexOf(" the ") >= 0)
                        {
                            Console.WriteLine($"password = {p1.ToString() + p2.ToString() + p3.ToString()} : {decryptedText}");

                            solved = true;

                            foreach(char c in decryptedText)    
                            {
                                sum +=c;
                            }
                            break;
                        }
                    }

                    if (solved) break;
                }
                if (solved) break;
            }
            
            string answer = sum.ToString();

            return answer;
        }
    }
}
