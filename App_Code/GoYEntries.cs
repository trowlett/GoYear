using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;

/// <summary>
/// Summary description for GoYEntries
/// </summary>
public class GoYEntries
{
    private static readonly char[] delimiterChars = { ';' };

    private Collection<GoYEntry> details = new Collection<GoYEntry>();

    public Collection<GoYEntry> Details
    {
        get
        {
            return this.details;
        }
    }

    public string FileName { get; private set; }

    public DateTime CreateTime { get; private set; }
    private static string[] lines;

    
    public static GoYEntries LoadPointsFromTxt(string fileName)
    {
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB mdb = new MRMISGADB(MRMISGADBConn);

        string GoYConn = ConfigurationManager.ConnectionStrings["GoYConnect"].ToString();
        GoY db = new GoY(GoYConn);

        GoYEntries target = new GoYEntries();
        target.FileName = fileName;
        target.CreateTime = System.IO.File.GetLastWriteTime(fileName);    
        lines = System.IO.File.ReadAllLines(fileName);
        int seqno = 0;
        string prevID = "";
        foreach (String line in lines)
        {
            string[] fields = line.Split(delimiterChars);
            if (fields.Length != 8)
            {
                throw new InvalidOperationException("DERP: Incorrect number of fields in schedule.txt");
            }

            if (fields[0].Trim() != prevID.Trim())
            {
                int deletedCount = purgeDetail(fields[0]);
                prevID = fields[0];
            }

            if (fields[2] != "")
            {
                seqno++;
                GoYEntry e = new GoYEntry()
                {
                    Eseq = seqno,
                    EClubID = fields[0].Substring(0,3),
                    EEventID = fields[0],
                    EMemberName = fields[1],
                    EEventType = 0,
                    EPoints = 0.0,
                    EGross = fields[2],
                    EHcp = fields[3],
                    ENet = fields[4]
                };
                if (string.IsNullOrWhiteSpace(fields[7]))
                {
                    e.EPoints = 0.0;
                }
                else{
                    e.EPoints = Convert.ToDouble(fields[7]);
                }
                string evi = fields[0];
                var x = mdb.Events.FirstOrDefault(n => n.ClubID == evi.Substring(0,3) && n.EventID.Trim() == evi);
                if (x == null)
                {
                    string emsg = String.Format("DERP: {0} Event not in Event Database.", evi);
                    throw new InvalidOperationException(emsg);
                }

                e.EEventType = GetEventType(x.Type.ToUpper().Trim());
/*
                string evType = x.Type.ToUpper().Trim();
                if (evType == "HOME") e.EEventType = 1;
                if (evType == "AWAY") e.EEventType = 2;
                if (evType == "MISGA") e.EEventType = 3;
                if (evType == "CLUB") e.EEventType = 4;
                */
                
                target.Details.Add(e);

                UpdateGoYDetailDatabase(e);
            }
        }

        return target;
    }

    public static int GetEventType(string EventType)
    {
        if (EventType == "HOME") return 1;
        if (EventType == "AWAY") return 2;
        if (EventType == "MISGA") return 3;
        if (EventType == "CLUB") return 4;
        return 9;
    }

    public static GoYEntries LoadPlayers(string EventID)
    {
        GoYEntries target = new GoYEntries();
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);

        Events ev = db.Events.FirstOrDefault(e => (e.ClubID == EventID.Substring(0,3)) && (e.EventID == EventID));

        var slist = 
            from pl in db.PlayersList
            join pn in db.Players on pl.PlayerID equals pn.PlayerID
            where  pl.EventID == EventID 
            orderby pn.Name
            select new { pl.EventID, pn.Name, pn.Hcp, pn.MemberID };

        int seqNo = 0;

        foreach (var item in slist)
        {
            seqNo++;
            GoYEntry entry = new GoYEntry()
            {
                Eseq = seqNo,
                EClubID= item.EventID.Substring(0,3),
                EEventID = item.EventID,
                EMemberName = item.Name,
 //               EMemberID = item.MemberID,
                EHcp = item.Hcp,
                EPoints = 0.0,
                EGross = "",
                ENet = ""
            };
            entry.EEventType = GetEventType(ev.Type.ToUpper().Trim());
            
//            entry.EEventType = 1;
//            if (ev.Type.ToUpper().Trim() == "AWAY") entry.EEventType = 2;
//            if (ev.Type.ToUpper().Trim() == "MISGA") entry.EEventType = 3;          

            target.details.Add(entry);
        }
        return target;
    }
    

    public static GoYEntries LoadPointsDetail(string EventID)
    {
        GoYEntries target = new GoYEntries();
        string GoYConn = ConfigurationManager.ConnectionStrings["GoYConnect"].ToString();
        GoY gdb = new GoY(GoYConn);

        var dlist =
            from d in gdb.GoYDetail
            where ((d.EventID == EventID) && (d.Points > 0.0))
            orderby d.ClubID ascending, d.EventID ascending, d.Points descending, d.Net ascending, d.MemberName ascending
            select new { d.ClubID, d.EventID, d.MemberName, d.EventType, d.Points, d.Gross, d.Hcp, d.Net };

        if (dlist != null)
        {
            int seqNo = 0;
            foreach (var item in dlist)
            {
                seqNo++;
                    GoYEntry ge = new GoYEntry()
                    {
                        Eseq = seqNo,
                        EEventID = item.EventID,
                        EEventType = item.EventType,
                        EMemberName = item.MemberName,
                        EPoints = item.Points,
                        EGross = item.Gross,
                        EHcp = item.Hcp,
                        ENet = item.Net
                    };
                    target.details.Add(ge);
            }
        }
        else
        {
            target.details.Clear();
        }
        return target;
    }
    

    public static GoYEntries LoadDataFromCSVFile(string FileName)
    {
        GoYEntries target = new GoYEntries();
        return target;
    }

    public static GoYEntries LoadDataFromTXTFile(string FileName)
    {
        GoYEntries target = new GoYEntries();
        return target;
    }

    protected static void UpdateGoYDetailDatabase(GoYEntry e)
    {
        string GoYConn = ConfigurationManager.ConnectionStrings["GoYConnect"].ToString();
        GoY db = new GoY(GoYConn);
        GoYDetail ev = db.GoYDetail.FirstOrDefault(p => p.ClubID==e.EClubID && p.EventID == e.EEventID && p.MemberName == e.EMemberName);
        if (ev == null)
        {
            GoYDetail newDetail = new GoYDetail()
            {
                ClubID = e.EClubID,
                EventID = e.EEventID,
                EventType = e.EEventType,
                MemberName = e.EMemberName,
                Points = e.EPoints,
                Gross = e.EGross,
                Hcp = e.EHcp,
                Net = e.ENet

            };
//            if (newDetail.Points > 0.0) 
            db.GoYDetail.InsertOnSubmit(newDetail);
        }
        else
        {
            ev.Points = e.EPoints;              // event and Name the same:  Change points
            ev.Gross = e.EGross;
            ev.Hcp = e.EHcp;
            ev.Net = e.ENet;
//            if (ev.Points == 0.0) db.GoYDetail.DeleteOnSubmit(ev);
        }
        db.SubmitChanges();

    }
    private static int purgeDetail(string eventID)
    {
        string GoYConn = ConfigurationManager.ConnectionStrings["GoYConnect"].ToString();
        GoY db = new GoY(GoYConn);
        int deleteCount = 0;
        var de = from d in db.GoYDetail
                 where (d.EventID == eventID)
                 select d;
        if (de != null)
        {
            foreach (var item in de)
            {
                string ed = item.EventID;
                string name = item.MemberName;
                db.GoYDetail.DeleteOnSubmit(item);
                deleteCount++;
            }
            db.SubmitChanges();
        }
        return deleteCount;
/*        bool notDone = true;
        while (notDone)
        {
            var de = db.GoYDetail.FirstOrDefault(d => d.EventID == eventID);
            if (de != null)
            {
                db.GoYDetail.DeleteOnSubmit(de);
                db.SubmitChanges();
                deleteCount++;
            }
            else
            {
                notDone = false;
            }
        }
 * */

    }
}