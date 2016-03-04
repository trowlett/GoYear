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
    [Column(IsPrimaryKey = true, DbType="INT NOT NULL IDENTITY", CanBeNull=false, IsDbGenerated=true)]
    public int Seq;
    [Column]
    public string EventID;
    [Column]
    public int EventType;
    [Column]
    public string MemberName;
    [Column]
    public double Points;
    [Column]
    public string Gross;
    [Column]
    public string Hcp;
    [Column]
    public string Net;

}