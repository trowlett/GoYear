using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ParticipationByEvent : System.Web.UI.Page
{
    public GoYEntries pl { get; set; }
    public int DBCount { get; set; }
    public GoYEntries evp { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        InputPanel.Visible = true;
        PlayerPanel.Visible = false;
        ControlPanel.Visible = false;
        if (!IsPostBack)
        {
            if (LoadDDLEvents())
            {
                btnSelect.Visible = true;
                btnSelect.Enabled = true;
            }
            else
            {
                lblNoEvents.Visible = true;
            }
        }
    }

    protected bool LoadDDLEvents()
    {
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);
        string GoYConn = ConfigurationManager.ConnectionStrings["GoYConnect"].ToString();
        GoY goyDB = new GoY(GoYConn);

        var evlist = from id in goyDB.GoYFiles select new { id.Name };
        foreach (var evID in evlist)
        {
            var query =
                 from ev in db.Events
                 where ev.EventID == evID.Name
                 orderby ev.EventID descending
                 select new { ev.EventID, ev.Date, ev.Type, ev.Title, ev.Deadline };

            if (query != null)
            {
                foreach (var item in query)
                {
                    if (item.Type.Trim().ToUpper() != "MISGA")
                    {
                        //                if (item.Deadline <= nowDate) closed = " | CLOSED ";
                        //                string tx = item.EventID.Trim() + " | " + item.Date.ToShortDateString() + " | " + item.Type.Trim() + " | " + item.Title.Trim() + closed;
                        string dt = item.Date.ToString("M/d");
                        while (dt.Length < 5) dt = dt + ' ';
                        //                string tx = String.Format("{0} | {1} | {2}", dt, item.Type.Trim(), item.Title.Trim());
                        string tx = dt + " | " + item.Type.Trim() + " | " + item.Title.Trim();
                        ddlEvents.Items.Add(new ListItem(tx, item.EventID));
                    }
                }
            }
           
        }
        return (ddlEvents.Items.Count > 0);

    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        string eid = ddlEvents.SelectedValue;
        lblID.Text = string.Format("Event {0} - ID:  {1} has been selected.",ddlEvents.SelectedItem.Text,eid);
        lblID.Visible = true;
        LoadEventPoints(eid);
//        PlayerPanel.Visible = true;
    }

    public void LoadPlayers(string id)
    {
        this.pl = GoYEntries.LoadPlayers(id);
        this.PlayersListRepeater.DataSource = new GoYEntries[] { this.pl };
        this.PlayersListRepeater.DataBind();
        //        int dbCount = this.sl.Entries.Count;
        //        DBCount = dbCount==0 ? "No": dbCount.ToString();
        DBCount = this.pl.Details.Count;
    }

    public void LoadEventPoints(string EventID)
    {
        this.evp = GoYEntries.LoadPointsDetail(EventID);
        lblEventPoints.Text = string.Format("{0} Players were awarded points.", this.evp.Details.Count);
        lblEventPoints.Visible = true;
        if (this.evp.Details.Count == 0)
        {
            this.PlayerPanel.Visible = false;
        }
        else
        {
            if (this.evp.Details.Count > 0)
            {
                this.PlayerPanel.Visible = true;
                this.PlayersListRepeater.DataSource = new GoYEntries[] { this.evp };
                this.PlayersListRepeater.DataBind();
            }
        }
    }

    protected void ddlEvents_SelectedIndexChanged(object sender, EventArgs e)
    {
//        int ndx = ddlEvents.SelectedIndex;
//        SetUpSelection();
    }
    protected void SetUpSelection()
    {
        this.PlayerPanel.Visible = false;
        lblEventPoints.Visible = false;
        lblID.Visible = false;
        string evid = ddlEvents.SelectedValue;
//        btnSelect.Text = IsEventEntered(evid) ? "EDIT Event" :  "ADD Event";
    }
    protected bool IsEventEntered(string EventID)
    {
        string GoYConn = ConfigurationManager.ConnectionStrings["GoYConnect"].ToString();
        GoY db = new GoY(GoYConn);
        GoYFiles ev = db.GoYFiles.FirstOrDefault(f => f.Name == EventID);
        if (ev == null)
        {
            return false;
        }
        else
        {
            return (ev.Name.Trim() == EventID.Trim()) ? true:false;
        }
    }
}