using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;

public class Node
{
    public Game Game{get;set;}

    int row;
    public int Row {  get {  return row; } }

    int column;
    public int Column{ get {return column;} }

    int zone;
    public int Zone{ get {return zone; }}

    int index;
    public int Index 
    { 
        get { return index; } 
        set
        {
            index = value;
            row = value / 9;
            column = value % 9;
            zone = row / 3 * 3 + column /3;
        }
    }

    private int? number;
    public int? Number
    {
        get {return number;}
        set
        {
            Node nRow = Game.Nodes.FirstOrDefault(n => n.Index != this.Index && n.Row == this.Row && n.Number.HasValue && n.Number.Value == value);
            if (nRow != null)
                throw new Exception($"One number can only appear once in each row. The number {value} has already appear in [{nRow.Row}, {nRow.Column}], it cannot be set on [{this.Row}, {this.Column}]");
            Node nColumn = Game.Nodes.FirstOrDefault(n => n.Index != this.Index && n.Column == this.Column && n.Number.HasValue && n.Number.Value == value);
            if (nColumn != null)
                throw new Exception($"One number can only appear once in each Column. The number {value} has already appear in [{nColumn.Row}, {nColumn.Column}], it cannot be set on [{this.Row}, {this.Column}]");
            Node nZone = Game.Nodes.FirstOrDefault(n => n.Index != this.Index && n.Zone == this.Zone && n.Number.HasValue && n.Number.Value == value);
            if (nZone != null)
                throw new Exception($"One number can only appear once in each Zone. The number {value} has already appear in [{nZone.Row}, {nZone.Column}], it cannot be set on [{this.Row}, {this.Column}]");

            number = value.Value;
            PossibleNumbers.Clear();

            foreach(Node n in Game.Nodes.Where(nn =>  nn.Index != this.Index &&  (nn.Row == this.Row || nn.Column == this.Column || nn.Zone == this.Zone)))
                n.PossibleNumbers.Remove(value.Value);
        }
    } 

    public List<int> PossibleNumbers{get;set;}
}