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
    public class Problem054 : ProblemBase
    {
        public override string Description
        {
            get
            {
                return @"
Problem 54 - Poker Hands

In the card game poker, a hand consists of five cards and are ranked, from lowest to highest, in the following way:

High Card: Highest value card.
One Pair: Two cards of the same value.
Two Pairs: Two different pairs.
Three of a Kind: Three cards of the same value.
Straight: All cards are consecutive values.
Flush: All cards of the same suit.
Full House: Three of a kind and a pair.
Four of a Kind: Four cards of the same value.
Straight Flush: All cards are consecutive values of same suit.
Royal Flush: Ten, Jack, Queen, King, Ace, in same suit.
The cards are valued in the order:
2, 3, 4, 5, 6, 7, 8, 9, 10, Jack, Queen, King, Ace.                    

The file, 0054_poker.txt, contains one-thousand random hands dealt to two players. Each line of the file contains ten cards (separated by a single space): 
the first five are Player 1's cards and the last five are Player 2's cards. 
You can assume that all hands are valid (no invalid characters or repeated cards), 
each player's hand is in no specific order, and in each hand there is a clear winner.

How many hands does Player 1 win?";
            }
        }

        public override int ProblemNumber
        {
            get
            {
                return 54;
            }
        }

        public override string Solution1()
        {
            string line = @"";

            int player1Count = 0;
            System.IO.StreamReader sr = new System.IO.StreamReader("Files/0054_poker.txt");
            while ((line = sr.ReadLine()) != null)
            {
                string[] cards = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (cards.Length != 10) throw new InvalidDataException($"Invalid line {line}");

                int[] player1Cards = new int[5];
                int[] player2Cards = new int[5];

                for (int i = 0; i < 5; i++)
                {
                    player1Cards[i] = CardValue(cards[i]);
                    player2Cards[i] = CardValue(cards[i + 5]);
                }

                Array.Sort(player1Cards);
                Array.Sort(player2Cards);

                

                if (HandValue(player1Cards) >= HandValue(player2Cards))
                    player1Count++;
            }

            sr.Close();

            string answer = player1Count.ToString();

            return answer;
        }

        int CardValue(String card)
        {
            string suiteOrder = "XCDHS";
            int value = 0;
            card = card.ToUpper();
            if (card.Length != 2) throw new InvalidDataException($"Invalid Card {card}");
            if (suiteOrder.IndexOf(card[1]) < 0) throw new InvalidDataException($"Invalid Card {card}");

            if (card[0] >= '2' && card[0] <= '9')
                value = (card[0] - '0') * 10 + (suiteOrder).IndexOf(card[1]);
            else if (card[0] == 'T')
                value = 100 + (suiteOrder).IndexOf(card[1]);
            else if (card[0] == 'J')
                value = 110 + (suiteOrder).IndexOf(card[1]);
            else if (card[0] == 'Q')
                value = 120 + (suiteOrder).IndexOf(card[1]);
            else if (card[0] == 'K')
                value = 130 + (suiteOrder).IndexOf(card[1]);
            else if (card[0] == 'A')
                value = 140 + (suiteOrder).IndexOf(card[1]);
            else
                throw new InvalidDataException($"Invalid Card {card}");

            return value;
        }

        long HandValue(int[] cards)
        {
            if (cards.Length != 5) throw new InvalidDataException($"Invalid cards");

            if (cards[4] / 10 - cards[3] / 10 == 1
            && cards[3] / 10 - cards[2] / 10 == 1
            && cards[2] / 10 - cards[1] / 10 == 1
            && cards[1] / 10 - cards[0] / 10 == 1
            )
            {
                if (cards[0] % 10 == cards[1] % 10 && cards[1] % 10 == cards[2] % 10 && cards[2] % 10 == cards[3] % 10 && cards[3] % 10 == cards[3] % 10)
                {
                    if (cards[4] / 10 == 14)
                    {
                        // royal flush
                        // the player with the higher suite wins
                        return 10000000000 + cards[4] % 10;
                    }
                    else
                    {
                        // straight flush
                        // 1. highest card
                        // 2. higher suite
                        return 9000000000 + cards[4] / 10 * 100 + cards[4] % 10;
                    }
                }
                else
                {
                    // straight
                    return 5000000000 + cards[4] / 10 * 100 + cards[4] % 10;
                }
            }
            else if (cards[0] / 10 == cards[3] / 10 || cards[1] / 10 == cards[4] / 10)
            {
                // four of a kind
                // higher card value in four of a kind, cards[2] is always one of the three of a kind
                return 8000000000 + cards[2] / 10 * 100;
            }
            else if ((cards[0] / 10 == cards[2] / 10 && cards[3] / 10 == cards[4] / 10) || (cards[0] / 10 == cards[1] / 10 && cards[2] / 10 == cards[4] / 10))
            {
                // full house
                // higher card value in three of a kind, cards[2] is always one of the three of a kind
                if (cards[0] / 10 == cards[2] / 10)
                    return 7000000000 + cards[2] / 10 * 100;
                else
                    return 7000000000 + cards[4] / 10 * 100;
            }
            else if (cards[0] % 10 == cards[1] % 10 && cards[1] % 10 == cards[2] % 10 && cards[2] % 10 == cards[3] % 10 && cards[3] % 10 == cards[4] % 10)
            {
                // flush

                return 6000000000 + cards[4] / 10 * (int)(Math.Pow(15, 5)) + cards[3] / 10 * (int)(Math.Pow(15, 4)) + cards[2] / 10 * (int)(Math.Pow(15, 3))
                + cards[1] / 10 * (int)(Math.Pow(15, 2)) + cards[0] / 10 * 14 + cards[4] % 10;
            }
            else if (cards[0] / 10 == cards[2] / 10 || cards[1] / 10 == cards[3] / 10 || cards[2] / 10 == cards[4] / 10)
            {
                // three of a kind
                // higher card value in three of a kind, cards[2] is always one of the three of a kind
                return 4000000000 + cards[2] / 10 * 100;
            }
            else if ((cards[0] / 10 == cards[1] / 10 && cards[2] / 10 == cards[3] / 10)
             || (cards[0] / 10 == cards[1] / 10 && cards[3] / 10 == cards[4] / 10)
             || (cards[1] / 10 == cards[2] / 10 && cards[3] / 10 == cards[4] / 10))
            {
                // two pairs
                // highest pair
                // second highest pair
                // higher single card 
                // highest suite of the highest pair

                int kicker = 0;
                int suiteValue = 0;
                if (cards[0] / 10 == cards[1] / 10 && cards[2] / 10 == cards[3] / 10)
                {
                    kicker = cards[4] / 10 * 10;
                    suiteValue = cards[3] % 10;
                }
                else if (cards[0] / 10 == cards[1] / 10 && cards[3] / 10 == cards[4] / 10)
                {
                    kicker = cards[2] / 10 * 10;
                    suiteValue = cards[2] % 10;
                }
                else
                {
                    kicker = cards[0] / 10 * 10;
                    suiteValue = cards[2] % 10;
                }

                return 3000000000 + cards[3] / 10 * 10000 + cards[1] / 10 * 100 + kicker + suiteValue;
            }
            else if (cards[0] / 10 == cards[1] / 10 || cards[1] / 10 == cards[2] / 10 || cards[2] / 10 == cards[3] / 10 || cards[3] / 10 == cards[4] / 10)
            {
                // 1 pair
                int pairValue = 0;
                int s1 = 0;
                int s2 = 0;
                int s3 = 0;
                int suiteValue = 0;

                if (cards[0] / 10 == cards[1] / 10)
                {
                    pairValue = cards[1] / 10 * (int)(Math.Pow(15, 5));
                    s1 = cards[4] / 10 * (int)(Math.Pow(15, 4));
                    s2 = cards[3] / 10 * (int)(Math.Pow(15, 3));
                    s3 = cards[2] / 10 * (int)(Math.Pow(15, 2));
                    suiteValue = cards[1] % 10;
                }
                else if (cards[1] / 10 == cards[2] / 10)
                {
                    pairValue = cards[2] / 10 * (int)(Math.Pow(15, 5));
                    s1 = cards[4] / 10 * (int)(Math.Pow(15, 4));
                    s2 = cards[3] / 10 * (int)(Math.Pow(15, 3));
                    s3 = cards[0] / 10 * (int)(Math.Pow(15, 2));
                    suiteValue = cards[2] % 10;
                }
                else if (cards[2] / 10 == cards[3] / 10)
                {
                    pairValue = cards[3] / 10 * (int)(Math.Pow(15, 5));
                    s1 = cards[4] / 10 * (int)(Math.Pow(15, 4));
                    s2 = cards[1] / 10 * (int)(Math.Pow(15, 3));
                    s3 = cards[0] / 10 * (int)(Math.Pow(15, 2));
                    suiteValue = cards[3] % 10;
                }
                else if (cards[3] / 10 == cards[4] / 10)
                {
                    pairValue = cards[4] / 10 * (int)(Math.Pow(15, 5));
                    s1 = cards[2] / 10 * (int)(Math.Pow(15, 4));
                    s2 = cards[1] / 10 * (int)(Math.Pow(15, 3));
                    s3 = cards[0] / 10 * (int)(Math.Pow(15, 2));
                    suiteValue = cards[4] % 10;
                }

                return 2000000000 + pairValue + s1 + s2 + s3 + suiteValue;
            }
            else
            {
                // nothing, high card
                return 1000000000 + cards[4] / 10 * (int)(Math.Pow(15, 5)) + cards[3] / 10 * (int)(Math.Pow(15, 4)) + cards[2] / 10 * (int)(Math.Pow(15, 3))
                + cards[1] / 10 * (int)(Math.Pow(15, 2)) + cards[0] / 10 * 14 + cards[4] % 10;
            }
        }
    }
}
