using System;
using System.Text.RegularExpressions;

using NUnit.Framework;
using Selenium;

namespace Selenium
{

    [TestFixture()] public class BirthdayClubMemberInfoFixture
    {
        private DefaultSelenium browser;

        private string testURL ;

        private string txtMemberName = "txtMemberName";
        private string ddlLanguages = "ddlLanguages";
        private string btnAdd = "btnAdd";
        private string lblBirthdate = "lblBirthdate";
        private string lblMessage = "lblMessage";

        [SetUp()] protected void SetUp()
        {
            this.testURL = @"http://localhost/test/WebTestIntro/BirthdayClubMemberInfo.aspx";

            // 4444 is the default port for the Selenium Server            
            this.browser = new DefaultSelenium("localhost", 4444, "*iexplore", this.testURL);
            this.browser.Start();
            this.browser.Open(this.testURL);
            Assert.AreEqual(this.testURL, this.browser.GetLocation());

        }

        [Test()] public void SmokeTest()
        {
            Assert.GreaterOrEqual(this.browser.GetElementIndex(this.txtMemberName), 0, "Element " + this.txtMemberName + " not visible");

        }

        [Test()] public void PickDate()
        {
            Assert.AreEqual(this.browser.GetText(this.lblBirthdate), "Select Your Birthday from the Calendar");
            this.browser.Click("link=14");
            this.browser.WaitForPageToLoad("6000");
            Assert.IsTrue(IsDate(this.browser.GetText(this.lblBirthdate)), "No Date Selected");

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

        [Test()] public void AddMember()
        {
            this.browser.Type(this.txtMemberName, "SeleniumTest");
            this.PickDate();
            this.browser.Click(this.btnAdd);
            this.browser.WaitForPageToLoad("6000");

            Assert.AreEqual(this.browser.GetText(this.lblMessage), "New Member Added");

            Regex re = new Regex(this.browser.GetValue(this.txtMemberName));
            //            Assert.AreEqual(2, re.Matches(this.Browser.GetBodyText).Count, "Should be exactly 2 instances of newly added member on page")

        }

        [Test()] public void ChooseDifferentLanguage()
        {
            this.browser.Select(this.ddlLanguages, "value=ja-JP");
            this.browser.WaitForPageToLoad("6000");
            string expectedBirthdateLabelText = Test.Properties.Resources.SelectBirthdayCalendarJP;
            Assert.AreEqual(expectedBirthdateLabelText, this.browser.GetText(this.lblBirthdate));

        }

        /*
        '<Test()> Public Sub UseDifferentUserLanguage()

        '    MyBase.Browser.UserLanguages(0) = "ja-JP"
        '    MyBase.Browser.GetPage(this.mTestURL)
        '    Dim expectedBirthdateLabelText As String = My.Resources.SelectBirthdayCalendarJP
        '    Assert.AreEqual(expectedBirthdateLabelText, this.lblBirthdate.Text)

        'End Sub
        */

        [TearDown()] protected void TearDown()
        {
            this.browser.Stop();

            string appDataFolder = @"C:\data\programs\examples\WebTestingIntro\SourceCode\WebTestIntro\WebSite\App_Data\";
            System.IO.File.Copy(appDataFolder + "BACKUPBirthdayClubMembers.xml", appDataFolder + "BirthdayClubMembers.xml", true);

        }

    }

}