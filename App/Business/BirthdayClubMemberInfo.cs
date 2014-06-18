using System;
using System.Data;

namespace Business
{

    public class BirthdayClubMemberInfo
    {
        private Int32 memberID;
        private string memberName;
        private DateTime birthdate;

        private DataTable birthdayClubMemberTable;

        public Int32 MemberID
        {
            get { return this.memberID; }
            set { this.memberID = value; }
        }
        public string MemberName
        {
            get { return this.memberName; }
            set { this.memberName = value; }
        }
        public DateTime Birthdate
        {
            get { return this.birthdate; }
            set { this.birthdate = value; }
        }

        private string saveFileName;

        public BirthdayClubMemberInfo()
            : this(@"C:\data\programs\examples\WebTestingIntro\SourceCode\WebTestIntroC#\WebSite\App_Data\")
        {

        }

        public BirthdayClubMemberInfo(string dataPath)
        {
            this.GetData(dataPath);

        }

        private void GetData(string dataPath)
        {
            // why can't data table retrieve any records??! check p246 - maybe something's missing from my xml . . . 
            DataSet birthdayClubMemberTable;
            birthdayClubMemberTable = new DataSet("BirthdayClubMembers");
            this.saveFileName = dataPath + "BirthdayClubMembers.xml";
            birthdayClubMemberTable.ReadXml(this.saveFileName);
            this.birthdayClubMemberTable = birthdayClubMemberTable.Tables[0];
        }

        public DataTable List()
        {
            return this.birthdayClubMemberTable;
        }

        public void Add()
        {
            DataRow newRow = this.birthdayClubMemberTable.NewRow();
            newRow["MemberID"] = this.GetMaxMemberID() + 1;
            newRow["MemberName"] = this.memberName;
            newRow["Birthdate"] = this.birthdate;
            this.birthdayClubMemberTable.Rows.Add(newRow);

            this.birthdayClubMemberTable.WriteXml(this.saveFileName, XmlWriteMode.WriteSchema);
        }

        public Int32 GetMaxMemberID()
        {
            DataRow[] maxMemberRow = this.birthdayClubMemberTable.Select("", "MemberID DESC");
            if (maxMemberRow != null && maxMemberRow.Length > 0)
                return Convert.ToInt32(maxMemberRow[0]["MemberID"]);

            return 0;
        }

        public void Delete()
        {
            DataRow[] deleteRow = this.birthdayClubMemberTable.Select("MemberID = " + this.memberID.ToString());
            if (deleteRow != null && deleteRow.Length > 0)
            {
                deleteRow[0].Delete();
                this.birthdayClubMemberTable.AcceptChanges();
                this.birthdayClubMemberTable.WriteXml(this.saveFileName, XmlWriteMode.WriteSchema);
            }
        }
    }
}
