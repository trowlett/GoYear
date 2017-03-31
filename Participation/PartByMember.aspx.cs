using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PartByMember : System.Web.UI.Page
{
    public MemberEventList mel { get; set; }
    public GoYEntries pl { get; set; }
    public int DBCount { get; set; }
    public GoYEntries evp { get; set; }
    private string myLastVisit;
    private string myID;
    private string myIndex;
//    private string memberDisplay;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDDLMembers();
            ddlMembers.SelectedIndex = GetMyIndexCookie();
        }

    }
    protected void LoadDDLMembers()
    {
        string GoYConn = ConfigurationManager.ConnectionStrings["GoYConnect"].ToString();
        GoY goyDB = new GoY(GoYConn);

        var ml = (from d in goyDB.GoYSummary
                  where ((d.MemberName != "") && (d.TotalPoints > 0.0))
                  orderby d.MemberName
                  select new { d.MemberName }).Distinct();
        ddlMembers.Items.Clear();
        foreach (var member in ml)
        {
            string x = member.MemberName;
            ddlMembers.Items.Add(new ListItem(x,x));
        }
        if (ddlMembers.Items.Count > 0)
        {
            btnSelect.Visible = true;
            btnSelect.Enabled = true;
        }
        else
        {
            lblNoMembers.Visible = true;
        }
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        int selectedMemberIndex = ddlMembers.SelectedIndex;
        string selectedMemberID = ddlMembers.SelectedValue;
        SaveMyID(selectedMemberIndex, selectedMemberID);
        string member = ddlMembers.SelectedValue;
        this.mel = MemberEventList.GetMemberEvents(member);
        int away = mel.AwayMixers;
        int home = mel.HomeMixers;
        double points = mel.TotalPoints;
        if (this.mel.Details.Count > 0)
        {
//            lblResults.Text = string.Format("{0} has participated in {1} Home events and {2} Away events for a Total of {3} points, is Ranked Number {4}.", member, home, away, points, mel.Rank);
            lblResults.Text = string.Format("{0}<br />is in position {4} of the Golfer of the Year Standings with {3:##,##0.000} points, and<br />has participated in {1} Home events and {2} Away events.", member, home, away, points, mel.Rank);
            this.MemberListRepeater.DataSource = new MemberEventList[] { this.mel };
            this.MemberListRepeater.DataBind();
        }
        else
        {
            lblResults.Text = string.Format("{0} has no participation points.", member);

        }
    }
    protected int GetMyIndexCookie()
    {
        int i = 0;
        if (Request.Cookies["GoYUserInfo"] != null)
        {
            myIndex =
                Server.HtmlEncode(Request.Cookies["GoYUserInfo"]["MyIndex"]);
            myID =
                 Server.HtmlEncode(Request.Cookies["GoYUserInfo"]["MyID"]);
            myLastVisit =
                Server.HtmlEncode(Request.Cookies["GoyUserInfo"]["lastVisit"]);
            i = Convert.ToInt32(myIndex);
        }
        return i;
    }

    protected void SaveMyID(int myIndex, string myID)
    {
        HttpCookie aCookie = new HttpCookie("GoYUserInfo");
        aCookie.Values["MyIndex"] = myIndex.ToString();
        aCookie.Values["MyID"] = myID.ToString();
        aCookie.Values["lastVisit"] = DateTime.Now.ToString();
        aCookie.Expires = DateTime.Now.AddDays(120);
        Response.Cookies.Add(aCookie);
    }
}