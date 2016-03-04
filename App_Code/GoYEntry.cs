using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GoYEntry
/// </summary>
public class GoYEntry
{
    public int Eseq { get; set; }
    public string EEventID { get; set; }
    public int EEventType { get; set; }
    public string EMemberName { get; set; }
    public double EPoints { get; set; }
    public string EGross { get; set; }
    public string EHcp { get; set; }
    public string ENet { get; set; }
//    public string EMemberID { get; set; }

//    public bool CanSignUp(DateTime lastDate)
//    {
//        return this.EType != "MISGA" && this.EDate <= lastDate;
//    }
}