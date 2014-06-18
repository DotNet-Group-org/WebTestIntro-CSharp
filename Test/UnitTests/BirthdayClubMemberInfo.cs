using System;
using System.Data;
using NUnit.Framework;

using Business;

[TestFixture()] public class BirthdayClubMemberInfoFixture
{
    private BirthdayClubMemberInfo birthdayClubMemberInfo;

    [SetUp()] protected virtual void SetUp()
    {
        this.birthdayClubMemberInfo = new BirthdayClubMemberInfo(@"C:\data\programs\examples\WebTestingIntro\SourceCode\WebTestIntro\Test\bin\Debug\Data\");

    }

    [Test()] public void List()
    {
        this.GetBirthdayClubMemberList();

    }

    private DataTable GetBirthdayClubMemberList()
    {
        DataTable birthdayClubMemberTable = this.birthdayClubMemberInfo.List();
        Assert.IsNotNull(birthdayClubMemberTable, "BirthdayClubMember Table NULL");
        Assert.Greater(birthdayClubMemberTable.Rows.Count, 0);
        return birthdayClubMemberTable;

    }

    [Test()] public void Add()
    {
        DataTable birthdayClubMemberTable = this.GetBirthdayClubMemberList();
        Int32 originalRowCount = birthdayClubMemberTable.Rows.Count;

        this.AddTestBirthdayClubMember();

        Int32 newRowCount = this.GetBirthdayClubMemberList().Rows.Count;
        Assert.AreEqual(originalRowCount + 1, newRowCount, "Wrong Row count after Add");

        this.DeleteNewestBirthdayClubMember();

    }

    private void AddTestBirthdayClubMember()
    {
        this.birthdayClubMemberInfo.MemberName = "Test";
        this.birthdayClubMemberInfo.Birthdate = DateTime.Today;
        this.birthdayClubMemberInfo.Add();

    }

    [Test()] public void Delete()
    {
        this.AddTestBirthdayClubMember();

        DataTable birthdayClubMemberTable = this.GetBirthdayClubMemberList();
        Int32 originalRowCount = birthdayClubMemberTable.Rows.Count;

        this.DeleteNewestBirthdayClubMember();

        Int32 newRowCount = this.GetBirthdayClubMemberList().Rows.Count;
        Assert.AreEqual(originalRowCount - 1, newRowCount, "Wrong Row count after Delete");

    }

    private void DeleteNewestBirthdayClubMember()
    {
        Int32 maxMemberID = this.birthdayClubMemberInfo.GetMaxMemberID();
        this.birthdayClubMemberInfo.MemberID = maxMemberID;
        this.birthdayClubMemberInfo.Delete();

    }

}
