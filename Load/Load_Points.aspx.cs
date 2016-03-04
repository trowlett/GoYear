using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Load_Load_Points : System.Web.UI.Page
{
    public List<MemberSummary> ms { get; set; }
    public List<MemberSummary> rankings { get; set; }
    public DateTime LastDate { get; set; }
    public GoYEntries Details { get; set; }
    private string path;
    private string filename;
    public string pointFileName;
    string url;
    string sourceDirectory;
    bool accessOK;


    protected void load_pointFile()
    {
        
        string fn = pointFileName;
        filename = System.IO.Path.Combine(path, fn);
        this.Details = GoYEntries.LoadPointsFromTxt(filename);
        this.GoYPointsRepeater.DataSource = new GoYEntries[] { this.Details };
        this.GoYPointsRepeater.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //        MrResources mr = new MrResources();
        path = Server.MapPath("~\\App_Data");
        url = "~\\Summary.aspx";
        //        path = Server.MapPath("");
        sourceDirectory = Server.MapPath("~\\App_Data");

        accessOK = false;
        Panel1.Visible = false;
        tbAccessCode.Focus();
        if (Session["AccessCode"] != null)
        {
            if (AccessControl.ValidAccessCode(Session["AccessCode"].ToString()))
            {
                accessOK = true;
                AccessPanel.Visible = false;
                Panel1.Visible = true;
            }
        }
        if (!IsPostBack)
        {
            ddlFiles.Items.Clear();
            LoadDDLFileName(sourceDirectory, "*.txt");
            LoadDDLFileName(sourceDirectory, "*.csv");
        }

        btnLoad.Enabled = true;
        lblDbLoadStatus.Text = "";
        lblBadAccess.Visible = false;
        if (!IsPostBack)
        {
            if (accessOK)
            {
                Panel1.Visible = true;
                AccessPanel.Visible = false;
            }
            btnTryAgain.Visible = false;
        }

        btnApply.Enabled = false;
        btnApply.Visible = false;
    }

    public void LoadDDLFileName(string directory, string filter)
    {
        string[] filelist = Directory.GetFiles(directory, filter);
        var fns = from fn in filelist
                  orderby fn descending
                  select fn;

        int listCount = filelist.Count();
        if (filelist.Count() > 0)
        {
            foreach (string fn in fns)
            {
                ddlFiles.Items.Add(fn.Substring(directory.Length + 1));
            }
        }
    }

    protected void btnLoad_Click(object sender, EventArgs e)
    {
//        txtFileToLoad.Text = FileUpload1.FileName;
            string tempName = txtFileToLoad.Text;
            string EventID = parseFileName(txtFileToLoad.Text.Trim());
            pointFileName = EventID + ".txt";
            string filename = System.IO.Path.Combine(path, pointFileName);
        if (!System.IO.File.Exists(filename))
        {
            lblFileName.Text = string.Format("{0} does not exits!",pointFileName);
            return;
        }
            lblFileName.Text = "File to load = " + pointFileName;
            string GoYConn = ConfigurationManager.ConnectionStrings["GoYConnect"].ToString();
            GoY db = new GoY(GoYConn);
            bool loadFile = false;
            GoYFiles f = db.GoYFiles.FirstOrDefault(p => p.Name == EventID);
            if (f != null)
            {

                // File already loaded
                //
                // ask if want to overwrite it
                loadFile = true;

            }
            else
            {
                // add eventid to files
                GoYFiles gf = new GoYFiles();
                gf.Name = EventID;
                gf.Date_Loaded = DateTime.Now;
                db.GoYFiles.InsertOnSubmit(gf);
                loadFile = true;
            }

            if (loadFile)
            {
                load_pointFile();
                db.SubmitChanges();
                btnLoad.Enabled = false;
                DataBind();
//                btnApply.Enabled = true;
//                btnApply.Visible = true;
                ApplyPoints();
                lblDbLoadStatus.Text = "Event Points loaded and Applied to database successfully";
            }

    }

    protected void ApplyPoints()
    {
        this.ms = MemberSummary.LoadSummary();
        RankByPoints();
        UpdateDatabase();

    }
    protected string parseFileName(string txt)
    {
        string rslt = "";
        if (txt.Length < 1)
            return rslt;
        int i = 0;
        while (i < txt.Length)
        {
            if (txt[i] != '.')
            {
                rslt += txt[i];
            }
            else
            {
                return rslt;
            }
            i++;
        }
        return rslt;
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        Server.Transfer(url);
    }
    protected void btnEnterAccess_Click(object sender, EventArgs e)
    {
        if (AccessControl.ValidAccessCode(tbAccessCode.Text))
        {
            Session["AccessCode"] = tbAccessCode.Text;
            AccessPanel.Visible = false;
            Panel1.Visible = true;
            accessOK = true;
        }
        else
        {
            lblBadAccess.Visible = true;
            btnEnterAccess.Visible = false;
            btnTryAgain.Visible = true;
        }

    }
    protected void btnTryAgain_Click(object sender, EventArgs e)
    {
        Server.Transfer("Load_Points.aspx");
    }
    protected void btnSelectFile_Click(object sender, EventArgs e)
    {
        bool haveTxtFile = false;
        bool haveCSVFile = false;
        filename = ddlFiles.SelectedValue;
        lblFileName.Text = filename;
        filename = Path.Combine(sourceDirectory, filename);
        string ext = Path.GetExtension(filename);
        ///
        /// Determine the Event ID
        /// 
        ///
        /// If file is a .txt file, then the points are already calculated
        /// read them and place them in a list to be applied.  The Event ID
        /// is the file name.
        /// 
        /// If file is a .csv file, load the scores and calculate the points
        /// place the results in a list to be applices.  The Event ID needs to be determined
        /// by finding the date in Row 2, Column 2 then finding all the Events for that date and
        /// asking the user to select the Event.  Once the Event is selected, the ID can be added 
        /// to the data from the .csv file.
        /// 
        string filetype = Path.GetExtension(filename).ToLower();
        if (filetype == ".txt") haveTxtFile = true;
        if (filetype == ".csv") haveCSVFile = true;
        if (haveCSVFile == haveTxtFile)
        {
            ///
            /// INVALID FILE EXTENSION.    Have another file selected
            /// 
        }
        if (haveCSVFile)
        {
            this.Details = GoYEntries.LoadDataFromCSVFile(filename);
            ///
            /// read csv file, load data in collection and calculate GoY points
            /// 

        }
        if (haveTxtFile)
        {
            ///
            /// Read txt file, load data in collection and calculate GoYpoints
        }
        string fn = parseFileName(ddlFiles.SelectedValue);
        txtFileToLoad.Text = fn;
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
                    smry.ClubCount = mem.Club;
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
                        MISGACount = mem.Misga,
                        ClubCount = mem.Club
                    };
                    db.GoYSummary.InsertOnSubmit(gs);
                }
                db.SubmitChanges();
            }
        }

    }

}