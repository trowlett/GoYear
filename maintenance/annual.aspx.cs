using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Maintenance_annual : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private string purgeAllLoadedFileNames()
    {
        MrTimeZone etz = new MrTimeZone();
        DateTime goyToday = etz.eastTimeNow();
        string rslt = "";
        string GoYConn = ConfigurationManager.ConnectionStrings["GoYConnect"].ToString();
        GoY db = new GoY(GoYConn);
        int countOfEntries = 0;
        var fi = from gf in db.GoYFiles
                  where gf.Date_Loaded < goyToday
                  orderby gf.Date_Loaded
                  select gf;


        foreach (GoYFiles item in fi)
        {
           db.GoYFiles.DeleteOnSubmit(item);
           countOfEntries++;
        }
        db.SubmitChanges();
        rslt = string.Format("{0} Sign Up Entries deleted from GoYFile that were prior to date {1}.", countOfEntries, goyToday.ToShortDateString());
        return rslt;
    }

    private string purgeAllGoySummary()
    {
        string rslt = "";
        string highName = "ZZZZZZZZZZ";
        string GoYConn = ConfigurationManager.ConnectionStrings["GoYConnect"].ToString();
        GoY db = new GoY(GoYConn);
        int countOfEntries = 0;
        var fs = from gs in db.GoYSummary
                 where gs.MemberName != highName
                 select gs;
        foreach (GoYSummary item in fs)
        {
            db.GoYSummary.DeleteOnSubmit(item);
            countOfEntries++;
        }
        db.SubmitChanges();
        rslt = string.Format("{0} Summary entries deleted from GoYSummary Database", countOfEntries);
        return rslt;

    }

    private string purgeAllGoyDetail()
    {
        string rslt = "";
//        string highName = "ZZZZZZZZZZ";
        string GoYConn = ConfigurationManager.ConnectionStrings["GoYConnect"].ToString();
        GoY db = new GoY(GoYConn);
        int countOfEntries = 0;
        var fd = from gd in db.GoYDetail
                 where gd.ClubID != "000"
                 select gd;
        foreach(GoYDetail item in fd)
        {
            db.GoYDetail.DeleteOnSubmit(item);
            countOfEntries++;
        }
        db.SubmitChanges();
        rslt = string.Format("{0} Entried deleted from GoYDetail Database", countOfEntries);
        return rslt;
    }

    protected void btnFiles_Click(object sender, EventArgs e)
    {
        lblFiles.Text = purgeAllLoadedFileNames();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        lblSummary.Text = purgeAllGoySummary();
    }

    protected void btnDetail_Click(object sender, EventArgs e)
    {
        lblDetail.Text = purgeAllGoyDetail();
    }
}