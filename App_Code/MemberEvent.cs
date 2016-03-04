using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MemberEvent
/// </summary>
public class MemberEvent
{
    public string MemberName { get; set; }
    public string EventID { get; set; }
    public DateTime Date { get; set; }
    public string Title { get; set; }
    public string HostID { get; set; }
    public string Type { get; set; }
    public string Gross { get; set; }
    public string Hcp { get; set; }
    public string Net { get; set; }
    public double Points { get; set; }
    public int Rank { get; set; }

	public MemberEvent()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}