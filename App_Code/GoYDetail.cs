using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;

/// <summary>
/// Summary description for GoYDetail
/// </summary>
/// 
[Table(Name="GoYDetail")]
public class GoYDetail
{
    [Column(IsPrimaryKey = true, Name = "ClubID", DbType = "CHAR(3) NOT NULL", CanBeNull = false)]
    public string ClubID;
    [Column(IsPrimaryKey = true, Name = "EventID", DbType = "CHAR(14) NOT NULL", CanBeNull = false)]
    public string EventID;
    [Column(IsPrimaryKey = true, Name = "MemberName", DbType = "VARCHAR(50) NOT NULL", CanBeNull = false)]
    public string MemberName;
    [Column]
    public int EventType;
    [Column]
    public double Points;
    [Column]
    public string Gross;
    [Column]
    public string Hcp;
    [Column]
    public string Net;

}