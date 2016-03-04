using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

/// <summary>
/// Summary description for GoY
/// </summary>
public class GoY : DataContext
{
    public Table<GoYDetail> GoYDetail;
    public Table<GoYSummary> GoYSummary;
    public Table<GoYFiles> GoYFiles;

    public GoY(string connection) : base(connection) { }
}