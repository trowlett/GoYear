using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GoYEvents
/// </summary>
public class GoYEvents
{
    public static string GetLastEvent()
    {
        //        return "All 2014 Events";
        string GoYConn = ConfigurationManager.ConnectionStrings["GoYConnect"].ToString();
        GoY db = new GoY(GoYConn);
        string EventID = "";
        var eid = from e in db.GoYFiles
                  where e.Name != EventID
                  orderby e.Name
                  select new { e.Name };
        if (eid == null)
        {
            throw new InvalidOperationException("Derp:  No GoY Details Available");
        }

        foreach (var item in eid)
        {
            EventID = item.Name;    // go thru list of Events and retrieve the most recent one
        }
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB mdb = new MRMISGADB(MRMISGADBConn);
        Events ev = mdb.Events.FirstOrDefault(p => p.EventID == EventID);

        string eventProperties = string.Format("Through {0}, {1} - {2}", ev.Date.ToString("MMM d"), ev.Type, ev.Title);
        return eventProperties;

    }
	public GoYEvents()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}