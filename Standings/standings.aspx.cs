using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblLastEvent.Text = GoYEvents.GetLastEvent();
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = GridView1.SelectedRow;
        string selectedName = row.Cells[2].Text;
        lblSelectedName.Text = "Selected name:  " + selectedName;
        Session["SelectedPlayer"] = selectedName;
        Response.Redirect("ParticipationDetails.aspx?Name=" + selectedName.Trim());

    }
}