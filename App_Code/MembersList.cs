using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;

/// <summary>
/// Summary description for MembersList
/// </summary>
public class MembersList
{
    private static readonly char[] delimiterChars = { ',' };

    private Collection<Member> mrMembers = new Collection<Member>();

    public Collection<Member> MRMembers
    {
        get
        {
            return this.mrMembers;
        }
    }

    public string FileName { get; private set; }

    public DateTime CreateTime { get; private set; }

	public MembersList()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public MembersList LoadMembersFromCSVFile(string fileName)
    {
        MembersList nml = new MembersList();
        string MRMISGADBConn = ConfigurationManager.ConnectionStrings["MRMISGADBConnect"].ToString();
        MRMISGADB db = new MRMISGADB(MRMISGADBConn);

        nml.FileName = fileName;
        nml.CreateTime = System.IO.File.GetLastWriteTime(fileName);
        string[] lines = System.IO.File.ReadAllLines(fileName);
        foreach (String line in lines)
        {
            if (line.Trim() == "")
            {
                // Ignore this line
            }
            else if (line.Substring(0, 1) == "/")
            {
                // Ignore comment line
            }
            else
            {
                string[] fields = line.Split(delimiterChars);
                if (fields.Length != 3)
                //                if (fields.Length != 11)
                {
                    throw new InvalidOperationException("DERP: Incorrect number of fields in schedule.txt");
                }
                Member m = new Member()
                {
                    MemberID = fields[0].Trim(),
                    Name = fields[1].Trim(),
                    Title = fields[2].Trim(),
                    EMail = fields[3].Trim(),
                    Gender = Convert.ToInt32(fields[4]),
                    pID = Convert.ToInt32(fields[5])
                };

                nml.mrMembers.Add(m);
                Members mem = db.Members.FirstOrDefault(p => p.MemberID.Trim() == m.MemberID.Trim());
                if (mem == null)
                {
                    Members newMember = new Members()
                    {
                        MemberID = mem.MemberID,
                        Name = mem.Name,
                        Title = mem.Title,
                        EMail = mem.EMail,
                        Gender = mem.Gender,
                        pID = mem.pID
                    };
                    db.Members.InsertOnSubmit(newMember);
                }
                else
                {
//                    mem.MemberID = m.MemberID;
                    mem.Name = m.Name;
                    mem.EMail = m.EMail;
                    mem.Gender = m.Gender;
                    mem.Title = m.Title;
                    mem.pID = m.pID;
                }
                db.SubmitChanges();
            }
        }

        return nml;
    }

    public MembersList LoadMembers()
    {
        MembersList ml = new MembersList();
        return ml;
    }
}