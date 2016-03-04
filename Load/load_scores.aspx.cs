using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Load_load_scores : System.Web.UI.Page
{
        private static readonly char[] delimiterChars = { ',',';' };

//    private string path;
    string sourceDirectory;
    string fileName;
    string filter;
//    string[] files;

    protected void Page_Load(object sender, EventArgs e)
    {
        sourceDirectory = Server.MapPath("~\\App_Data");
//        filter = "*.csv";
        lblItems.Text = "";
        if (!IsPostBack)
        {
            ddlFiles.Items.Clear();
            LoadDDLFileName(sourceDirectory, "*.txt");
            LoadDDLFileName(sourceDirectory, "*.csv");
        }
    }
    public void LoadDDLFileName(string directory, string filter)
    {
        string[] filelist = Directory.GetFiles(directory, filter);
        int listCount = filelist.Count();
        if (filelist.Count() > 0)
        {
            foreach (string fn in filelist)
            {
                ddlFiles.Items.Add(fn.Substring(directory.Length + 1));
            }
        }
    }


    protected void btnSelectFile_Click(object sender, EventArgs e)
    {
        bool haveTxtFile = false;
        bool haveCSVFile = false;
        fileName = ddlFiles.SelectedValue;
        lblFileName.Text = fileName;
        fileName = Path.Combine(sourceDirectory, fileName);
        string ext = Path.GetExtension(fileName);
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
        string filetype = Path.GetExtension(fileName).ToLower();
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
            ///
            /// read csv file and load data and calculate GoY points
            /// 

        }
        if (haveTxtFile)
        {
        }
        string[] lines = System.IO.File.ReadAllLines(fileName);
        int i = 0;
        foreach (string line in lines)
        {
            if (i > 4)
            {
                string[] fields = line.Split(delimiterChars);
                if (fields[5] != "")
                {
                    GoYScoreEntry se = new GoYScoreEntry
                    {
                        Name = fields[2] +", "+fields[3],
                        Score = fields[4],
                        HCP = fields[5]
                    };
                    lblItems.Text += se.Name + ", " + se.Score + ", " + se.HCP + "<br />";
                }
                if (fields[10] != "")
                {
                    GoYScoreEntry se = new GoYScoreEntry
                    {
                        Name = fields[8] + ", " + fields[9],
                        Score = fields[10],
                        HCP = fields[11]
                    };
                    lblItems.Text += se.Name + ", " + se.Score + ", " + se.HCP + "<br />";
                }
            }
            i++;
        }

    }
}