using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Summary : System.Web.UI.Page
{
    public List<MemberSummary> ms { get; set; }
    public List<MemberSummary> rankings { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
//        this.ms = MemberSummary.LoadSummary();
//        RankByPoints();
//        UpdateDatabase();

    }

    protected void RankByPoints()
    {
        rankings = new List<MemberSummary>();
        if (ms.Count > 0)
        {
            var summary = from a in ms
                          where a.Points > 0.0
                          orderby a.Points, a.Home, a.Away, a.Name descending
                          select new { a.Name, a.Points, a.Rank, a.Home, a.Away, a.Misga };
                     
            
            rankings = new List<MemberSummary>();
            int i = ms.Count;

            foreach (var item in summary)
            {
                MemberSummary entry = new MemberSummary
                {
                    Name = item.Name,
                    Points = item.Points,
                    Rank = i,
                    Home = item.Home,
                    Away = item.Away,
                    Misga = item.Misga
                };
                rankings.Add(entry);
                i = i - 1;
            }
        }
    }

    protected void UpdateDatabase()
    {
        string GoYConn = ConfigurationManager.ConnectionStrings["GoYConnect"].ToString();
        GoY db = new GoY(GoYConn);

        if (rankings.Count > 0)
        {
            foreach (MemberSummary mem in rankings)
            {
                var smry = db.GoYSummary.FirstOrDefault(p => p.MemberName == mem.Name);
                if (smry != null)
                {
                    smry.TotalPoints = mem.Points;
                    smry.Rank = mem.Rank;
                    smry.HomeCount = mem.Home;
                    smry.AwayCount = mem.Away;
                    smry.MISGACount = mem.Misga;
                }
                else
                {
                    GoYSummary gs = new GoYSummary
                    {
                        MemberName = mem.Name,
                        Rank = mem.Rank,
                        TotalPoints = mem.Points,
                        HomeCount = mem.Home,
                        AwayCount = mem.Away,
                        MISGACount = mem.Misga
                    };
                    db.GoYSummary.InsertOnSubmit(gs);
                }
                db.SubmitChanges();
            }
        }
    
    }
    protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
}