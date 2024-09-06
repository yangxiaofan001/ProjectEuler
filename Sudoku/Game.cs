using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.Serialization.Formatters;

public class Game
{
    public Game()
    {
        this.Nodes = new List<Node>();
    }

    public String LogFileName
    {
        get; set;
    }

    public void InitLog()
    {
        if (string.IsNullOrEmpty(this.LogFileName)) return;

        System.IO.StreamWriter sw = new System.IO.StreamWriter(this.LogFileName, false);
        sw.Close();
    }

    public void LogWrite(string msg)
    {
        if (string.IsNullOrEmpty(this.LogFileName)) Console.Write(msg);

        System.IO.StreamWriter sw = new System.IO.StreamWriter(this.LogFileName, true);
        sw.Write(msg);
        sw.Close();
    }

    public void LogWriteLine(string msg)
    {
        if (string.IsNullOrEmpty(this.LogFileName)) Console.WriteLine(msg);

        System.IO.StreamWriter sw = new System.IO.StreamWriter(this.LogFileName, true);
        sw.WriteLine(msg);
        sw.Close();
    }

    public Game(int[] initialNumbers)
    {
        Initialize(initialNumbers);
    }

    public int CandidatesCount
    {
        get { return this.Nodes.Sum(n => n.PossibleNumbers.Count); }
    }

    public void Initialize(int[] initialNumbers)
    {
        if (initialNumbers == null || initialNumbers.Length != 81)
            throw new Exception("A sudoku game has 9 rows, 9 columns");

        this.Nodes = new List<Node>();
        for (int i = 0; i < 81; i++)
        {
            this.Nodes.Add(new Node
            {
                Game = this,
                Index = i,
                PossibleNumbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
            });
        }

        for (int i = 0; i < 81; i++)
        {
            if (initialNumbers[i] > 0)
            {
                this.Nodes.First(n => n.Index == i).Number = initialNumbers[i];
            }
        }
    }

    public int Id { get; set; }

    public List<Node> Nodes { get; set; }

    public void Print()
    {
        if (Nodes.Count != 81) throw new Exception("a sudoku game has 9 rows, 9 clumns, in 9 zones");
        for (int i = 0; i < 9; i++)
        {
            if (Nodes.Count(n => n.Row == i) != 9) throw new Exception("a sudoku game has 9 rows, 9 clumns, in 9 zones");
            if (Nodes.Count(n => n.Column == i) != 9) throw new Exception("a sudoku game has 9 rows, 9 clumns, in 9 zones");
            if (Nodes.Count(n => n.Zone == i) != 9) throw new Exception("a sudoku game has 9 rows, 9 clumns, in 9 zones");
        }

        if (!string.IsNullOrEmpty(LogFileName)) Utils.InitLog(LogFileName);

        string horizontalLine = "";
        for (int i = 0; i < 60; i++) horizontalLine = horizontalLine + "-";

        for (int row = 0; row < 9; row++)
        {
            if (row % 3 == 0)
                LogWriteLine(horizontalLine);

            LogWriteLine(horizontalLine);

            // display a row - 3 lines, each line is 1/3 of 9 columns
            for (int lineNumber = 0; lineNumber < 3; lineNumber++)
            {
                string line = "";
                for (int column = 0; column < 9; column++)
                {
                    if (column % 3 == 0)
                    {
                        line = line + "|";
                    }

                    line = line + "|";

                    Node n = Nodes.First(n => n.Row == row && n.Column == column);
                    if (n.Number.HasValue)
                    {
                        string s = lineNumber == 1 ? "  " + n.Number.Value.ToString() + "  " : "     ";
                        line = line + s;
                    }
                    else
                    {
                        string numberString = "";
                        for (int i = 0; i < 3 && i + 3 * lineNumber < n.PossibleNumbers.Count; i++)
                        {
                            numberString = numberString + n.PossibleNumbers[i + 3 * lineNumber].ToString();
                        }
                        line = line + " " + numberString;
                        for (int x = 0; x < 4 - numberString.Length; x++)
                            line = line + " ";
                    }
                }

                line = line + "||";

                LogWriteLine(line);
                line = "";
            }
        }


        LogWriteLine(horizontalLine);
        LogWriteLine(horizontalLine);
    }

    public int Solve()
    {
        int prevPossibleNumbersCount = 729;
        int possibleNumbersCount = this.Nodes.Sum(n => n.PossibleNumbers.Count);

        while (prevPossibleNumbersCount > possibleNumbersCount)
        {
            prevPossibleNumbersCount = possibleNumbersCount;
            Node[] unsolvedNodes = this.Nodes.Where(n => !n.Number.HasValue).ToArray();
            Node node;

            for (int i = 0; i < unsolvedNodes.Length; i++)
            {
                node = unsolvedNodes[i];

                if (node.PossibleNumbers.Count == 1)
                {
                    node.Number = node.PossibleNumbers[0];
                    continue;
                }

                foreach (int number in node.PossibleNumbers)
                {
                    int setnumber = 0;
                    if (!unsolvedNodes.Any(n => n.Index != node.Index && n.Row == node.Row && n.PossibleNumbers.Contains(number)))
                    {
                        // no other node in the same row can use this number
                        setnumber = 1;
                    }
                    else if (!unsolvedNodes.Any(n => n.Index != node.Index && n.Column == node.Column && n.PossibleNumbers.Contains(number)))
                    {
                        // no other node in the same column can use this number
                        setnumber = 2;
                    }
                    else if (!unsolvedNodes.Any(n => n.Index != node.Index && n.Zone == node.Zone && n.PossibleNumbers.Contains(number)))
                    {
                        // no other node in the same row can use this number
                        setnumber = 3;
                    }

                    if (setnumber > 0)
                    {
                        node.Number = number;
                        break;
                    }

                }
            }


            List<Node> earlyTwinNodeList = new List<Node>();
            List<Node> laterTwinNodeList = new List<Node>();
            List<int> pairModeList = new List<int>();

            foreach (Node n0 in this.Nodes.Where(nn => nn.PossibleNumbers.Count == 2).ToList())
            {
                Node n1 = this.Nodes.FirstOrDefault(nn =>
                        nn.Index > n0.Index && nn.Row == n0.Row && nn.PossibleNumbers.Count == 2
                        && nn.PossibleNumbers[0] == n0.PossibleNumbers[0]
                        && nn.PossibleNumbers[1] == n0.PossibleNumbers[1]
                        );
                if (n1 != null)
                {
                    earlyTwinNodeList.Add(n0);
                    laterTwinNodeList.Add(n1);
                    pairModeList.Add(1);
                }

                Node n2 = this.Nodes.FirstOrDefault(nn =>
                    nn.Index > n0.Index && nn.Column == n0.Column && nn.PossibleNumbers.Count == 2
                    && nn.PossibleNumbers[0] == n0.PossibleNumbers[0]
                    && nn.PossibleNumbers[1] == n0.PossibleNumbers[1]
                    );
                if (n2 != null)
                {
                    earlyTwinNodeList.Add(n0);

                    laterTwinNodeList.Add(n2);
                    pairModeList.Add(2);
                }

                Node n3 = this.Nodes.FirstOrDefault(nn =>
                nn.Index > n0.Index && nn.Zone == n0.Zone && nn.PossibleNumbers.Count == 2
                && nn.PossibleNumbers[0] == n0.PossibleNumbers[0]
                && nn.PossibleNumbers[1] == n0.PossibleNumbers[1]
                );
                if (n3 != null)
                {
                    earlyTwinNodeList.Add(n0);

                    laterTwinNodeList.Add(n3);
                    pairModeList.Add(3);
                }


            }

            for (int i = 0; i < earlyTwinNodeList.Count; i++)
            {
                Node n1 = laterTwinNodeList[i];
                if (n1 == null) continue;
                Node n0 = earlyTwinNodeList[i];
                int pairMode = pairModeList[i];

                if (n0.PossibleNumbers.Count == 2)
                {
                    foreach (Node n in Nodes.Where(nn =>
                                                    nn.Index != n0.Index && nn.Index != n1.Index
                                                    && (pairMode == 1 ? nn.Row == n0.Row : (pairMode == 2 ? nn.Column == n0.Column : nn.Zone == n0.Zone)
                            )))
                    {
                        n.PossibleNumbers.Remove(n0.PossibleNumbers[0]);
                        n.PossibleNumbers.Remove(n0.PossibleNumbers[1]);
                    }
                }
            }

            possibleNumbersCount = this.Nodes.Sum(n => n.PossibleNumbers.Count);
        }

        return possibleNumbersCount > 0 ? -1 : 1;
    }

    public int Tech01_NakedSingle()
    {
        // A cell with only one candidate
        // Set the Number property of this cell to the only possible number, thus removing this number from other cells in the same row, column and zone
        Node nakedSingleNode = Nodes.FirstOrDefault(n => n.PossibleNumbers.Count == 1);
        if (nakedSingleNode != null)
        {
            LogWriteLine($"Tech01_NakedSingle: cell [{nakedSingleNode.Row}, {nakedSingleNode.Column}] only has 1 possible number {nakedSingleNode.PossibleNumbers[0]}, set {nakedSingleNode.PossibleNumbers[0]} as the number.");
            nakedSingleNode.Number = nakedSingleNode.PossibleNumbers[0];
            return 1;
        }
        else
        {
            return -1;
        }
    }

    public int Tech02_HiddenSingle()
    {
        // A cell has mutiple candidates; but one of the candidates only appears in this cell in a row/column/block
        // Set the Number property of this cell to this candidate number, thus removing this number from other cells in the same row, column and zone
        for (int number = 1; number <= 9; number++)
        {
            for (int checkingType = 0; checkingType < 3; checkingType++)
            {
                for (int setIndex = 0; setIndex < 9; setIndex++)
                {
                    Node[] nodes = Nodes.Where(n => n.PossibleNumbers.Contains(number)
                        && (checkingType == 0 ? n.Row : (checkingType == 1 ? n.Column : n.Zone)) == setIndex).ToArray();
                    if (nodes.Length == 1)
                    {
                        string ctString = checkingType == 0 ? "row" : (checkingType == 1 ? "column" : "zone");
                        LogWriteLine($"Tech02_HiddenSingle: cell [{nodes[0].Row}, {nodes[0].Column}] is the only cell in {ctString} {setIndex} that has {number} as a possible number, set {number} as the number.");
                        nodes[0].Number = number;
                        return 1;
                    }
                }
            }
        }

        return -1;
    }

    public int Tech03_NakedPair()
    {
        // two cells in the same row/column/block, each has only same two candidateds
        for (int checkingType = 0; checkingType < 3; checkingType++)
        {
            for (int checkingIndex = 0; checkingIndex < 9; checkingIndex++)
            {
                Node[] nodes = Nodes.Where(n => n.PossibleNumbers.Count == 2
                                && (checkingType == 0 ? n.Row : (checkingType == 1 ? n.Column : n.Zone)) == checkingIndex).ToArray();

                for (int n1Index = 0; n1Index < nodes.Length - 1; n1Index++)
                {
                    Node n1 = nodes[n1Index];
                    for (int n2Index = n1Index + 1; n2Index < nodes.Length; n2Index++)
                    {
                        Node n2 = nodes[n2Index];

                        if (n1.PossibleNumbers[0] == n2.PossibleNumbers[0] && n1.PossibleNumbers[1] == n2.PossibleNumbers[1])
                        {
                            // a pair is found, now check if any possible number can be removed from other cells
                            Node[] nodesWithCandidatesToBeRemoved =
                                Nodes.Where(n =>
                                        (checkingType == 0 ? n.Row : (checkingType == 1 ? n.Column : n.Zone)) == (checkingType == 0 ? n1.Row : (checkingType == 1 ? n1.Column : n1.Zone))
                                        && n.Index != n1.Index && n.Index != n2.Index
                                        && (n.PossibleNumbers.Contains(n1.PossibleNumbers[0]) || n.PossibleNumbers.Contains(n1.PossibleNumbers[1]))
                                        ).ToArray();
                            if (nodesWithCandidatesToBeRemoved.Length > 0)
                            {
                                string ctString = checkingType == 0 ? "row" : (checkingType == 1 ? "column" : "zone");
                                LogWriteLine($"Tech03_NakedPair: nodes [{n1.Row}, {n1.Column}] and [{n2.Row}, {n2.Column}] form a naked pair ({n1.PossibleNumbers[0]}, {n1.PossibleNumbers[1]}) in {ctString} {checkingIndex}. Remove ({n1.PossibleNumbers[0]}, {n1.PossibleNumbers[1]}) from all other nodes ");
                                // there are candidates to be removed in other node in the same row/column/zone
                                foreach (Node n in nodesWithCandidatesToBeRemoved)
                                {
                                    n.PossibleNumbers.Remove(n1.PossibleNumbers[0]);
                                    n.PossibleNumbers.Remove(n1.PossibleNumbers[1]);
                                }

                                return 1;
                            }
                        }
                    }
                }
            }
        }

        return -1;
    }

    public int Tech04_PointingPairOrTriple()
    {
        for (int zone = 0; zone < 9; zone++)
        {
            List<int> usedNumbers = Nodes.Where(n => n.Zone == zone && n.Number.HasValue).Select(n => n.Number.Value).ToList();
            List<int> candidates = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            foreach (int number in usedNumbers) candidates.Remove(number);
            List<Node> nodesInZone = Nodes.Where(n => n.Zone == zone && !n.Number.HasValue).ToList();

            foreach (int number in candidates)
            {
                List<Node> nodes = nodesInZone.Where(n => n.PossibleNumbers.Contains(number)).ToList();
                if (nodes.Count > 3 || nodes.Count < 2) continue;

                for (int checkingtype = 0; checkingtype < 2; checkingtype++)
                {
                    bool isValid = true;
                    Node n1 = nodes[0];
                    for (int n2Index = 1; n2Index < nodes.Count; n2Index++)
                    {
                        Node n2 = nodes[n2Index];

                        if ((checkingtype == 0 ? n1.Row : n1.Column) != (checkingtype == 0 ? n2.Row : n2.Column))
                        {
                            // not fit, check next number; 
                            isValid = false;
                            break;
                        }
                    }

                    if (isValid)
                    {
                        List<Node> otherNodesInSameRowOrSameColumn =
                            Nodes.Where(n => n.Zone != zone
                                && (checkingtype == 0 ? n.Row : n.Column) == (checkingtype == 0 ? n1.Row : n1.Column)
                                && n.PossibleNumbers.Contains(number)).ToList();
                        if (otherNodesInSameRowOrSameColumn.Count > 0)
                        {
                            // able to move forward
                            string stype = checkingtype == 0 ? "row" : "column";
                            int itype = checkingtype == 0 ? n1.Row : n1.Column;
                            LogWriteLine($"Tech04_PointingPairOrTriple: in zone {zone}, number {number} only appears in {stype} {itype}. Remove {number} from other cells in the same {stype}.");

                            foreach (Node n in otherNodesInSameRowOrSameColumn)
                                n.PossibleNumbers.Remove(number);

                            return 1;
                        }
                    }
                }
            }
        }

        return -1;
    }

    public int Tech05_ClaimingPairOrTriple()
    {
        for (int number = 1; number < 9; number++)
        {
            for (int checkingType = 0; checkingType < 2; checkingType++)
            {
                for (int index = 0; index < 9; index++)
                {
                    List<Node> nodes = Nodes.Where(n => (checkingType == 0 ? n.Row : n.Column) == index && n.PossibleNumbers.Contains(number)).ToList();
                    if (nodes.Count < 2 || nodes.Count > 3) continue;

                    bool isValid = true;

                    for (int ni = 1; ni < nodes.Count; ni++)
                    {
                        if (nodes[0].Zone != nodes[ni].Zone)
                        {
                            string stype = checkingType == 0 ? "row" : "column";

                            isValid = false;
                            break;
                        }
                    }

                    if (isValid)
                    {
                        List<Node> nodesToRemoveNumber = Nodes.Where(n =>
                            n.Zone == nodes[0].Zone
                            && (checkingType == 0 ? n.Row : n.Column) != (checkingType == 0 ? nodes[0].Row : nodes[0].Column)
                            && n.PossibleNumbers.Contains(number))
                            .ToList();
                        if (nodesToRemoveNumber.Count > 0)
                        {
                            LogWriteLine($"Tech05_ClaimingPairOrTriple: in {checkingType} {index}, number {number} only appears in zone {nodes[0].Zone}. Remove {number} from other cells in the same zone.");

                            foreach (Node nn in nodesToRemoveNumber)
                                nn.PossibleNumbers.Remove(number);
                            return 1;
                        }
                    }

                }
            }
        }

        return -1;
    }

    public int Tech06_NakedTripple()
    {
        for (int checkingType = 0; checkingType < 3; checkingType++)
        {
            for (int index = 0; index < 9; index++)
            {
                List<Node> nodes = Nodes.Where(n => (checkingType == 0 ? n.Row : (checkingType == 1 ? n.Column : n.Zone)) == index 
                            && (n.PossibleNumbers.Count == 2 || n.PossibleNumbers.Count == 3)).ToList();
                if (nodes.Count < 3) continue;

                for (int n1Index = 0; n1Index < nodes.Count - 2; n1Index++)
                {
                    Node n1 = nodes[n1Index];
                    for (int n2Index = 1; n2Index < nodes.Count - 1; n2Index++)
                    {
                        Node n2 = nodes[n2Index];
                        List<int> numberSet = n1.PossibleNumbers.Union(n2.PossibleNumbers).ToList();
                        if (numberSet.Count != 3) continue;
                        for (int n3Index = 2; n3Index < nodes.Count; n3Index++)
                        {
                            Node n3 = nodes[n3Index];
                            if (n3.PossibleNumbers.Any(number => !numberSet.Contains(number))) continue;

                            // Found one set
                            List<Node> nodesToRemoveCandidate = Nodes.Where(n =>
                                                                    (checkingType == 0 ? n.Row : (checkingType == 1 ? n.Column : n.Zone)) == index
                                                                    && n.Index != n1.Index && n.Index != n2Index && n.Index != n3.Index
                                                                    && n.PossibleNumbers.Any(num => numberSet.Contains(num))).ToList();
                            if (nodesToRemoveCandidate.Count > 0)
                            {
                                string ctString = checkingType == 0 ? "row" : (checkingType == 1 ? "column" : "zone");
                                LogWriteLine($"Tech06_NakedTripple: The 3 ndoes [{n1.Row}, {n1.Column}], [{n2.Row}, {n2.Column}], and [{n3.Row}, {n3.Column}] form a naked tripple ({numberSet[0]}, {numberSet[1]}, {numberSet[2]}) in {ctString} {index}. Remove {numberSet[0]}, {numberSet[1]} and {numberSet[2]} from all other nodes in the same {ctString}.");
                                // able to remove some candidates
                                foreach (Node n in nodesToRemoveCandidate)
                                {
                                    foreach (int num in numberSet)
                                        n.PossibleNumbers.Remove(num);
                                }

                                return 1;
                            }
                        }
                    }
                }
            }
        }

        return -1;
    }

    public int Tech07_XWing() 
    {
        for(int checkingType = 0; checkingType < 2; checkingType ++) 
        {
            for(int number = 1; number <= 9; number ++)
            {
                List<List<Node>> nodesWithNumberAdCandidateList = new List<List<Node>>();
                for(int rowOrColumnIndex = 0; rowOrColumnIndex < 9; rowOrColumnIndex ++)
                {
                    List<Node> nodesWithNumberAdCandidate = Nodes.Where(n => 
                                (checkingType == 0 ? n.Row : n.Column) == rowOrColumnIndex 
                                && n.PossibleNumbers.Contains(number)).ToList();
                    if (nodesWithNumberAdCandidate.Count == 2) nodesWithNumberAdCandidateList.Add(nodesWithNumberAdCandidate);
                }

                if (nodesWithNumberAdCandidateList.Count < 2) continue;

                for(int r1Index = 0; r1Index < nodesWithNumberAdCandidateList.Count - 1; r1Index ++)
                {
                    List<Node> r1NodesWithNumberAdCandidate = nodesWithNumberAdCandidateList[r1Index];
                    for(int r2Index = 0; r2Index < nodesWithNumberAdCandidateList.Count; r2Index ++)
                    {
                        List<Node> r2NodesWithNumberAdCandidate = nodesWithNumberAdCandidateList[r2Index];
                        bool isValid =
                            checkingType == 0 ? 
                                (r1NodesWithNumberAdCandidate[0].Column == r2NodesWithNumberAdCandidate[0].Column &&
                                r1NodesWithNumberAdCandidate[1].Column == r2NodesWithNumberAdCandidate[1].Column) :
                                (r1NodesWithNumberAdCandidate[0].Row == r2NodesWithNumberAdCandidate[0].Row &&
                                r1NodesWithNumberAdCandidate[1].Row == r2NodesWithNumberAdCandidate[1].Row);
                                        ;
                        if (isValid)
                        {
                            List<Node> c1NodesToRemveCandidates = Nodes.Where(n => 
                                (checkingType == 0 ? n.Column : n.Row) == (checkingType == 0 ? r1NodesWithNumberAdCandidate[0].Column : r1NodesWithNumberAdCandidate[0].Row)
                                && n.Index != r1NodesWithNumberAdCandidate[0].Index
                                && n.Index != r2NodesWithNumberAdCandidate[0].Index
                                && n.PossibleNumbers.Contains(number)
                            ).ToList();
                            List<Node> c2NodesToRemveCandidates = Nodes.Where(n => 
                                (checkingType == 0 ? n.Column : n.Row) == (checkingType == 0 ? r1NodesWithNumberAdCandidate[1].Column : r1NodesWithNumberAdCandidate[1].Row)
                                && n.Index != r1NodesWithNumberAdCandidate[1].Index
                                && n.Index != r2NodesWithNumberAdCandidate[1].Index
                                && n.PossibleNumbers.Contains(number)
                            ).ToList();

                            if (c1NodesToRemveCandidates.Count > 0 || c2NodesToRemveCandidates.Count > 0)
                            {
                                string ctString = checkingType == 0 ? "row" : "column";
                                string ctDiagonal = checkingType == 0 ? "column" : "row";
                                int ctIndex1 = checkingType == 0 ? r1NodesWithNumberAdCandidate[0].Row : r1NodesWithNumberAdCandidate[0].Column;
                                int ctIndex2 = checkingType == 0 ? r1NodesWithNumberAdCandidate[1].Row : r1NodesWithNumberAdCandidate[1].Column;
                                int ctDiagonalIndex1 = checkingType == 0 ? r1NodesWithNumberAdCandidate[0].Column : r1NodesWithNumberAdCandidate[0].Row;
                                int ctDiagonalIndex2 = checkingType == 0 ? r1NodesWithNumberAdCandidate[1].Column : r1NodesWithNumberAdCandidate[1].Row;

                                LogWriteLine($"Tech07_XWing: Two nodes [{r1NodesWithNumberAdCandidate[0].Row}, {r1NodesWithNumberAdCandidate[0].Column}] and [{r1NodesWithNumberAdCandidate[1].Row}, {r1NodesWithNumberAdCandidate[1].Column}] in {ctString} {ctIndex1} are the only nodes in the {ctString} that contains {number}; also two nodes [{r2NodesWithNumberAdCandidate[0].Row}, {r2NodesWithNumberAdCandidate[0].Column}] and [{r2NodesWithNumberAdCandidate[1].Row}, {r2NodesWithNumberAdCandidate[1].Column}] in {ctString} {ctIndex2} are the only nodes in the {ctString} that contains {number}; These 4 nodes form an X-Wing. Remove {number} from all other nodes in {ctDiagonal} {ctDiagonalIndex1} and {ctDiagonalIndex2}");

                                // able to remove some candidates
                                foreach(Node n in c1NodesToRemveCandidates) n.PossibleNumbers.Remove(number);
                                foreach(Node n in c2NodesToRemveCandidates) n.PossibleNumbers.Remove(number);

                                return 1;
                            }
                        }
                    }
                }
            }
        }

        return -1; 
    }

    public int Tech08_HiddePair() 
    { 
        for(int checkingType = 0; checkingType < 3; checkingType++)  
        {
            for(int checkingIndex = 0; checkingIndex < 9; checkingIndex ++)
            {
                List<int> unusedNumbers = new List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9};
                List<Node> checkingNodeList = Nodes.Where(nn => (checkingType == 0 ? nn.Row : (checkingType == 1 ? nn.Column : nn.Zone)) == checkingIndex).ToList();
                foreach(Node n in checkingNodeList.Where(nn => nn.Number.HasValue)) unusedNumbers.Remove(n.Number.Value);
                
                for(int num1Index = 0; num1Index < unusedNumbers.Count - 1; num1Index ++)
                {
                    int num1 = unusedNumbers[num1Index];
                    for(int num2Index = 0; num2Index < unusedNumbers.Count; num2Index ++)
                    {
                        int num2 = unusedNumbers[num2Index];
                        List<Node> nodesWithThese2Numbers = checkingNodeList.Where(n => n.PossibleNumbers.Contains(num1) && n.PossibleNumbers.Contains(num2)).ToList();
                        if (nodesWithThese2Numbers.Count == 2)                        
                        {
                            // found a naked pair
                            if (nodesWithThese2Numbers[0].PossibleNumbers.Count > 2 || nodesWithThese2Numbers[1].PossibleNumbers.Count > 2)
                            {
                                string ctString = checkingType == 0 ? "row" : (checkingType == 1 ? "column" : "zone");
                                LogWriteLine($"Tech08_HiddePair: The two nodes [{nodesWithThese2Numbers[0].Row}, {nodesWithThese2Numbers[0].Column}] and [{nodesWithThese2Numbers[1].Row}, {nodesWithThese2Numbers[1].Column}] are the only two nodes in {ctString} {checkingIndex} that can have the numbers ({num1}, {num2}). Remove all other numbers from these two nodes.");

                                // able to remove some number, generate a naked pair
                                nodesWithThese2Numbers[0].PossibleNumbers.Remove(num1);
                                nodesWithThese2Numbers[0].PossibleNumbers.Remove(num2);
                                nodesWithThese2Numbers[1].PossibleNumbers.Remove(num1);
                                nodesWithThese2Numbers[1].PossibleNumbers.Remove(num2);

                                return 1;
                            }
                        }
                    }
                }
            }
        }
        return -1; 
    }

    public int Tech09_NakesQuad() 
    { 
            for (int zoneIndex = 0; zoneIndex < 9; zoneIndex++)
            {
                List<Node> nodes = Nodes.Where(n => n.Zone == zoneIndex 
                            && n.PossibleNumbers.Count >= 2 && n.PossibleNumbers.Count <= 4).ToList();
                if (nodes.Count < 4) continue;

                for (int n1Index = 0; n1Index < nodes.Count - 3; n1Index++)
                {
                    Node n1 = nodes[n1Index];
                    for (int n2Index = 1; n2Index < nodes.Count - 2; n2Index++)
                    {
                        Node n2 = nodes[n2Index];
                        for (int n3Index = 2; n3Index < nodes.Count - 1; n3Index++)
                        {
                            Node n3 = nodes[n3Index];
                            List<int> numberSet = n1.PossibleNumbers.Union(n2.PossibleNumbers).Union(n3.PossibleNumbers).ToList();
                            if (numberSet.Count != 4) continue;

                            for (int n4Index = 2; n4Index < nodes.Count; n4Index++)
                            {
                                Node n4 = nodes[n4Index];

                                if (n4.PossibleNumbers.Any(number => !numberSet.Contains(number))) continue;

                                // Found one set
                                List<Node> nodesToRemoveCandidate = Nodes.Where(n =>
                                                                    n.Zone == zoneIndex
                                                                    && n.Index != n1.Index && n.Index != n2Index && n.Index != n3.Index && n.Index != n4.Index
                                                                    && n.PossibleNumbers.Any(num => numberSet.Contains(num))).ToList();
                                if (nodesToRemoveCandidate.Count > 0)
                                {
                                    LogWriteLine($"Tech09_NakesQuad: The 4 ndoes [{n1.Row}, {n1.Column}], [{n2.Row}, {n2.Column}], [{n3.Row}, {n3.Column}], and [{n4.Row}, {n4.Column}] form a naked quad ({numberSet[0]}, {numberSet[1]}, {numberSet[2]}, and {numberSet[3]}) in zone {zoneIndex}. Remove {numberSet[0]}, {numberSet[1]}, {numberSet[2]}, {numberSet[3]} from all other nodes in the same zone.");
                                    
                                    // able to remove some candidates
                                    foreach (Node n in nodesToRemoveCandidate)
                                    {
                                        foreach (int num in numberSet)
                                            n.PossibleNumbers.Remove(num);
                                    }

                                    return 1;
                                }
                            }
                        }
                    }
                }
            }


        return -1; 
    }

    public int SolveStepByStep()
    {

        // techxx - solved game, return; solved 1 step, continue; cannot go further, next step
        List<System.Reflection.MethodInfo> solvingTechnics = this.GetType().GetMethods().ToList();

        int prevPossibleNumbersCount = this.CandidatesCount;
        int endResult = 0;

        while (true)
        {
            foreach (System.Reflection.MethodInfo mi in solvingTechnics)
            {
                if (mi.Name.StartsWith("Tech"))
                {
                    Console.Write($"Try one step of {mi.Name}: ");
                    int ret = (int)(mi.Invoke(this, null));
                    if (ret == 1) // moved forward
                    {
                        Console.WriteLine("moved forward, continue next step starting with technic 01.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Cannot move any further, try the next technic.");
                    }

                    // ret == -1, cannot go further, try next technic
                }
            }
            int possibleNumbersCount = this.CandidatesCount;
            if (possibleNumbersCount == prevPossibleNumbersCount)   // tried everything, cannot go any further
            {
                endResult = -1;
                break;
            }

            if (possibleNumbersCount == 0)      // game is solved
            {
                endResult = 1;
                break;
            }

            // moved forward, continue
            prevPossibleNumbersCount = possibleNumbersCount;
        }

        return endResult;
    }
}