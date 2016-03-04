using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;

/// <summary>
/// Summary description for GoYFiles
/// </summary>
/// 
[Table(Name="GoYFiles")]
public class GoYFiles
{
    [Column(IsPrimaryKey = true, Name="Name", DbType="CHAR(15)")]
    public string Name;
    [Column]
    public DateTime Date_Loaded;

}