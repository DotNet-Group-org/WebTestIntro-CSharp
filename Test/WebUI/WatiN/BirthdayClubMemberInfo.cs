using System;
using System.Text.RegularExpressions;

using NUnit.Framework;
using WatiN.Core;

namespace WatiN
{
    [TestFixture()]
    public class BirthdayClubMemberInfoFixture
    {
        private IE browser;

        private string testURL;

        private TextField txtMemberName;
        private SelectList ddlLanguages;
        private Button btnAdd;
        private Span lblBirthdate;
        private Span lblMessage;

        [SetUp()]
        protected void SetUp()
        {
            this.testURL = "http://localhost/test/WebTestIntro/BirthdayClubMemberInfo.aspx";

            this.browser = new IE(this.testURL);
            Assert.AreEqual(this.testURL, this.browser.Url);

            this.txtMemberName = new TextField(this.browser.Element(Find.ById("txtMemberName")));
            this.ddlLanguages = new SelectList(this.browser.Element(Find.ById("ddlLanguages")));
            this.btnAdd = new Button(this.browser.Element(Find.ById("btnAdd")));
            this.lblBirthdate = new Span(this.browser.Element(Find.ById("lblBirthdate")));
            this.lblMessage = new Span(this.browser.Element(Find.ById("lblMessage")));

        }

        [Test()]
        public void SmokeTest()
        {
            Assert.IsTrue(this.txtMemberName.Exists, "Element " + this.txtMemberName.Id + " not visible");
            Assert.IsTrue(this.ddlLanguages.Exists, "Element " + this.ddlLanguages.Id + " not visible");
            Assert.IsTrue(this.lblBirthdate.Exists, "Element " + this.lblBirthdate.Id + " not visible");

        }

        [Test()]
        public void PickDate()
        {
            Assert.AreEqual(this.lblBirthdate.Text, "Select Your Birthday from the Calendar");
            UtilityClass.RunScript("javascript:__doPostBack('calBirthDatePicker','2870')", "javascript", this.browser.HtmlDocument.parentWindow);
            this.lblBirthdate = new Span(this.browser.Element(Find.ById(this.lblBirthdate.Id)));
            Assert.IsTrue(IsDate(this.lblBirthdate.Text), "No Date Selected (value = " + this.lblBirthdate.Text + ")");

        }

        private bool IsDate(string date)
        {
            bool valid = false;
            try
            {
                DateTime dt = DateTime.Parse(date);
                valid = true;
            }
            catch (FormatException)
            { }
            return valid;

        }

        [Test()]
        public void AddMember()
        {
            this.txtMemberName.TypeText("WatiN Test");
            this.PickDate();
            this.btnAdd = new Button(this.browser.Element(Find.ById("btnAdd")));
            this.btnAdd.Click();
            this.lblMessage = new Span(this.browser.Element(Find.ById("lblMessage")));
            Assert.AreEqual(this.lblMessage.Text, "New Member Added");

            Regex re = new Regex(this.txtMemberName.Text);
            Assert.AreEqual(2, re.Matches(this.browser.HtmlDocument.body.innerHTML).Count, "Should be exactly 2 instances of newly added member on page");

        }

        [Test()]
        public void ChooseDifferentLanguage()
        {
            this.ddlLanguages.SelectByValue("ja-JP");
            string expectedBirthdateLabelText = Test.Properties.Resources.SelectBirthdayCalendarJP;
            this.lblBirthdate = new Span(this.browser.Element(Find.ById("lblBirthdate")));
            Assert.AreEqual(expectedBirthdateLabelText, this.lblBirthdate.Text);

        }

        /*
        '<Test()> Public Sub UseDifferentUserLanguage()

        '    MyBase.Browser.UserLanguages(0) = "ja-JP"
        '    MyBase.Browser.GetPage(this.mTestURL)
        '    Dim expectedBirthdateLabelText As String = My.Resources.SelectBirthdayCalendarJP
        '    Assert.AreEqual(expectedBirthdateLabelText, this.lblBirthdate.Text)

        'End Sub
        */

        [TearDown()]
        protected void TearDown()
        {
            this.browser.Close();

            string appDataFolder = @"C:\data\programs\examples\WebTestingIntro\SourceCode\WebTestIntro\WebSite\App_Data\";
            System.IO.File.Copy(appDataFolder + "BACKUPBirthdayClubMembers.xml", appDataFolder + "BirthdayClubMembers.xml", true);

        }

    }

}