using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for MemberSummary
/// </summary>
public class MemberSummary
{
	public string Name { get; set; }
	public double Points { get; set; }
	public int Rank { get; set; }
	public int Home { get; set; }
	public int Away { get; set; }
	public int Misga { get; set; }
    public int Club { get; set; }

	public MemberSummary(string name, double points, int rank, int home, int away, int misga, int club)
	{
		Name = name;
		Points = points;
		Rank = Rank;
		Home = home;
		Away = away;
		Misga = misga;
        Club = club;
	}

	public MemberSummary()
	{
		//
		// TODO: Add constructor logic here
		//
	}

	public static List<MemberSummary> LoadSummary()
	{
		List<MemberSummary> target = new List<MemberSummary>();
		string GoYConn = ConfigurationManager.ConnectionStrings["GoYConnect"].ToString();
		GoY db = new GoY(GoYConn);

		var recs = from d in db.GoYDetail
                   where d.Points > 0.0
					 orderby d.MemberName
					 select d;
		if (recs == null)
		{
			MemberSummary s = new MemberSummary("*** NO DATA ***", 0.0, 0, 0, 0, 0, 0);
			target.Add(s);
			return target;
		}
		string prevName = "";
		double prevPoints = 0.0;
		int prevHome = 0;
		int prevAway = 0;
		int prevMISGA = 0;
        int prevClub = 0;

		foreach (var rec in recs)
		{
			if (rec.MemberName != prevName)
			{
				if (prevName != "")
				{
					MemberSummary s = new MemberSummary(prevName, prevPoints, 0, prevHome, prevAway, prevMISGA, prevClub);
                    if (s.Points > 0.0) target.Add(s);
				}
				prevName = rec.MemberName;
				prevPoints = 0.0;
				prevHome = 0; prevAway = 0; prevMISGA = 0;
			}
			prevPoints += rec.Points;
			if (rec.EventType == 1) prevHome++;
			if (rec.EventType == 2) prevAway++;
			if (rec.EventType == 3) prevMISGA++;
            if (rec.EventType == 4) prevClub++;
		}
		MemberSummary ms = new MemberSummary(prevName, prevPoints, 0, prevHome, prevAway, prevMISGA, prevClub);
		if (ms.Points > 0.0) target.Add(ms);
		return target;
	}
}