using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;

/// <summary>
/// Summary description for GoYSummary
/// </summary>
/// 
[Table(Name="GoYSummary")]

public class GoYSummary
{
    [Column(IsPrimaryKey=true)]
    public string MemberName;
    [Column]
    public double TotalPoints;
    [Column]
    public int Rank;
    [Column]
    public int HomeCount;
    [Column]
    public int AwayCount;
    [Column]
    public int MISGACount;
    [Column]
    public int ClubCount;

}