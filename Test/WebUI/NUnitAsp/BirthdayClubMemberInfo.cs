using System;
using System.Text.RegularExpressions;

using NUnit.Framework;
using NUnit.Extensions.Asp;
using NUnit.Extensions.Asp.AspTester;

namespace NUnitAsp
{

    [TestFixture()]
    public class BirthdayClubMemberInfoFixture : NUnit.Extensions.Asp.WebFormTestCase
    {
        private string testURL;

        private WebFormTester form;
        private TextBoxTester txtMemberName;
        private DropDownListTester ddlLanguages;
        private ButtonTester btnAdd;
        private LabelTester lblBirthdate;
        private LabelTester lblMessage;

        protected override void SetUp()
        {
            base.Browser.UserLanguages = new string[1];
            base.Browser.UserLanguages[0] = "en-US";

            this.testURL = @"http://localhost/test/WebTestIntro/BirthdayClubMemberInfo.aspx";

            this.form = new WebFormTester(base.Browser);
            this.txtMemberName = new TextBoxTester("txtMemberName");
            this.ddlLanguages = new DropDownListTester("ddlLanguages");
            this.btnAdd = new ButtonTester("btnAdd");
            this.lblBirthdate = new LabelTester("lblBirthdate");
            this.lblMessage = new LabelTester("lblMessage");

            base.Browser.GetPage(this.testURL);
            Assert.AreEqual(this.testURL, base.Browser.CurrentUrl.ToString());

        }

        [Test()]
        public void SmokeTest()
        {
            AssertVisibility(this.txtMemberName, true);

        }

        [Test()]
        public void PickDate()
        {
            Assert.AreEqual(this.lblBirthdate.Text, "Select Your Birthday from the Calendar");
            this.form.PostBack("javascript:__doPostBack('calBirthDatePicker','2870')");
            Assert.IsTrue(IsDate(this.lblBirthdate.Text), "No Date Selected");

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
            this.txtMemberName.Text = "NUnitAsp Test";
            this.PickDate();
            this.btnAdd.Click();
            Assert.AreEqual(this.lblMessage.Text, "New Member Added");

            Regex re = new Regex(this.txtMemberName.Text);
            Assert.AreEqual(2, re.Matches(base.Browser.CurrentPageText).Count, "Should be exactly 2 instances of newly added member on page");

        }

        [Test()]
        public void ChooseDifferentLanguage()
        {
            this.ddlLanguages.SelectedValue = "ja-JP";
            string expectedBirthdateLabelText = Test.Properties.Resources.SelectBirthdayCalendarJP;
            Assert.AreEqual(expectedBirthdateLabelText, this.lblBirthdate.Text);

        }

        [Test()]
        public void UseDifferentUserLanguage()
        {
            base.Browser.UserLanguages[0] = "ja-JP";
            base.Browser.GetPage(this.testURL);
            string expectedBirthdateLabelText = Test.Properties.Resources.SelectBirthdayCalendarJP;
            Assert.AreEqual(expectedBirthdateLabelText, this.lblBirthdate.Text);

        }

        protected override void TearDown()
        {
            string appDataFolder = @"C:\data\programs\examples\WebTestingIntro\SourceCode\WebTestIntroC#\WebSite\App_Data\";
            System.IO.File.Copy(appDataFolder + "BACKUPBirthdayClubMembers.xml", appDataFolder + "BirthdayClubMembers.xml", true);
        }

        [Test()]
        public void GridViewExample()
        {
            DataGridTester gridview = new DataGridTester("gv");

            base.Browser.GetPage(@"http://localhost/test/WebTestIntro/GridViewExample.aspx");

            AssertVisibility(gridview, true);
            Assert.Greater(gridview.RowCount, 0, "No rows in grid view");

        }

    }

}