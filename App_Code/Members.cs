using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Data.SqlTypes;

/// <summary>
/// Summary description for Members
/// </summary>
/// 
[Table(Name="Members")]
public class Members
{
    [Column(IsPrimaryKey = true)]
    public string MemberID;
    [Column]
    public string Name;
    [Column]
    public string Title;
    [Column]
    public string EMail;
    [Column]
    public int Gender;
    [Column]
    public int pID;
}