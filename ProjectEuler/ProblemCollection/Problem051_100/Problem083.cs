using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace EulerProject.ProblemCollection
{
    public class Problem83 : ProblemBase
    {
        public override int ProblemNumber
        {
            get
            {
                return 83;
            }
        }

        public override string Description
        {
            get
            {
                return @"Problem 83 - Path Sum: Three Ways
NOTE: This problem is a significantly more challenging version of Problem 81.

In the 5 by 5 matrix below, the minimal path sum from the top left to the bottom right, by moving left, right, up, and down, 
is indicated in bold red and is equal to 2297.

[131]     673       [234]       [103]     [18]
[201]     [96]      [342]       965       [150]
630         803     746         [422]     [111]
537         699     497         [121]     956
805         732     524         [37]      [331]

Find the minimal path sum from the left column to the right column in matrix.txt (right click and 'Save Link/Target As...'), a 31K text file containing an 80 by 80 matrix.
";
            }
        }



        List<List<int>> numbersList;

        void PrintNodeList(List<Node> nodeList, int size) 
        {
            for(int r = 0; r < size; r ++)
            {
                List<Node> row = nodeList.Where(n => n.X == r).ToList();
                for(int c = 0; c < size; c ++)
                {
                    Node node = row.FirstOrDefault(n => n.Y == c);
                    if (node == null)
                        Console.Write("          ");
                    else
                    {
                        string s = (node.ShortestPath == Int32.MaxValue ? node.Value : node.ShortestPath).ToString();
                        if (node.Visited) s = '{' + s + '}';
                        for(int pad = 8 - s.Length; pad < 8; pad++)
                            Console.Write(" ");
                        Console.Write(s);
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();
        }

        public override string Solution1()
        {
            string idea = @"
Dijkstra's algorithm, wiki
";
            Console.WriteLine(idea);

            #region read numbers from file
            System.IO.StreamReader sr = new System.IO.StreamReader("Files/0083_matrix.txt");
            string line;
            numbersList = new List<List<int>>();
            while ((line = sr.ReadLine()) != null)
            {
                string[] numberStrings = line.Split(new char[] { ',' });
                List<int> numbers = new List<int>();
                foreach (string s in numberStrings) numbers.Add(Convert.ToInt32(s));
                numbersList.Add(numbers);
            }
            sr.Close();
            #endregion

            #region test with sample data
            numbersList = new List<List<int>>{
                new List<int>{131, 673, 234, 103, 18},
                new List<int>{201, 96, 342, 965, 150},
                new List<int>{630, 803, 746, 422, 111},
                new List<int>{537, 699, 497, 121, 956},
                new List<int>{805, 732, 524, 37, 331}
            };
            #endregion

            #region initialize node set
            List<Node> nodeList = new List<Node>();
            for(int r = 0; r < numbersList.Count; r ++)
            {
                for(int c = 0; c < numbersList.Count; c++)
                {
                    nodeList.Add(new Node{X = r, Y = c, Value = numbersList[r][c], Visited = false, ShortestPath = int.MaxValue});
                }
            }
            nodeList[0].Visited = true;
            nodeList[0].ShortestPath = nodeList[0].Value;
            #endregion
            PrintNodeList(nodeList, numbersList.Count);

            Node currentNode = null;

            while(true)
            {
                List<Node> visitedNodes = nodeList.Where(n => n.Visited).ToList();
                int minPath = visitedNodes.Min(n => n.ShortestPath);
                currentNode = visitedNodes.First(n => n.ShortestPath == minPath);

                if (currentNode.X == (numbersList.Count - 1) && currentNode.Y == (numbersList.Count - 1)) break;

                // update all adjcent node
                List<int[]> incrementList = new List<int[]>{
                    new int[]{-1, 0},
                    new int[]{1, 0},
                    new int[]{0, -1},
                    new int[]{0, 1},
                };
                foreach(int[] inc in incrementList)
                {
                    int x1 = currentNode.X + inc[0];
                    int y1 = currentNode.Y + inc[1];
                    Node n1 = nodeList.FirstOrDefault(n => n.X == x1 && n.Y == y1);
                    if (n1!=null && !n1.Visited)
                    {
                        n1.ShortestPath = n1.Value + currentNode.ShortestPath;
                        n1.Visited = true;
                    }
                }

                nodeList.Remove(currentNode);
                PrintNodeList(nodeList, numbersList.Count);                
                if (nodeList.Count == 0)
                {
                    Console.WriteLine("Path not found. Failed.");
                    break;
                }
            }

            Console.WriteLine(currentNode.ToString());

            return currentNode.ShortestPath.ToString();
        }
    }

    public class Node
    {
        public int X {get;set;}

        public int Y {get;set;}  

        public int Value {get;set;}

        public int ShortestPath {get;set;}

        public bool Visited {get;set;}

        override public string ToString()
        {
            return $"[{X}, {Y}]={Value}, visited={Visited}, shortestPath={ShortestPath}";
        }
    }
}
