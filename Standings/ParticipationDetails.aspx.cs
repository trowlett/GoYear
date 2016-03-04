using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ParticipationDetails : System.Web.UI.Page
{
    public static string Name { get; set; }
    public MemberEventList mel { get; set; }
    public GoYEntries pl { get; set; }
    public int DBCount { get; set; }
    public GoYEntries evp { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        Name = Request.QueryString["Name"];
//        lblName.Text = Name;
        GetDetails(Name);
    }
    protected void GetDetails(string member)
    {
        this.mel = MemberEventList.GetMemberEvents(member);
        int away = mel.AwayMixers;
        int home = mel.HomeMixers;
        double points = mel.TotalPoints;
        if (this.mel.Details.Count > 0)
        {
//            lblResults.Text = string.Format("has participated in {0} Home events and {1} Away events with a Total of {2:##,##0.000} points, is Ranked Number {3}.", home, away, points, mel.Rank);
            lblResults.Text = string.Format("is in position {3} of the Golfer of the Year Standings with {2:##,##0.000} points, and<br />has paricipated in {0} Home events and {1} Away events.", home, away, points, mel.Rank);
            this.MemberListRepeater.DataSource = new MemberEventList[] { this.mel };
            this.MemberListRepeater.DataBind();
        }
    }

}