using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;
using System.Configuration;

/// <summary>
/// Summary description for MemberEventList
/// </summary>
public class MemberEventList
{
/*    private static int awayMixers;
    private static int homeMixers;
    private static double totalPoints;
    */
    public int AwayMixers { get; private set; }
    public int HomeMixers { get; private set; }
    public double TotalPoints { get; private set; }
    public int Rank { get; set; }

    private Collection<MemberEvent> details = new Collection<MemberEvent>();

    public Collection<MemberEvent> Details
    {
        get
        {
            return this.details;
        }
    }

    public static MemberEventList GetMemberEvents(string memberName)
    {
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB mdb = new MRMISGADB(MRMISGADBConn);

        string GoYConn = ConfigurationManager.ConnectionStrings["GoYConnect"].ToString();
        GoY db = new GoY(GoYConn);

        MemberEventList target = new MemberEventList();
        target.AwayMixers = 0;
        target.HomeMixers = 0;
        target.TotalPoints = 0.0;

        var ge = from d in db.GoYDetail
                     where ((d.MemberName == memberName) && (d.Points > 0.0))
                     orderby d.EventID
                     select d;
        if (ge != null)
        {          
            foreach (var item in ge)
            {
                    var se = mdb.Events.FirstOrDefault(e => e.EventID == item.EventID);                   
                    MemberEvent me = new MemberEvent()
                    {
                        MemberName = item.MemberName,
                        EventID = se.EventID,
                        Date = se.Date,
                        Title = se.Title,
                        HostID = se.HostID,
                        Type = se.Type,
                        Gross = item.Gross,
                        Hcp = item.Hcp,
                        Net = item.Net,
                        Points = item.Points
                    };
                    target.details.Add(me);
                    if (me.Type.Trim() == "Away") target.AwayMixers++;
                    if (me.Type.Trim() == "Home") target.HomeMixers++;

                    target.TotalPoints = target.TotalPoints + me.Points;
            }
        }
        var ms = db.GoYSummary.FirstOrDefault(s => s.MemberName == memberName);
        target.Rank = ms.Rank;

        return target;
    }

	public MemberEventList()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}