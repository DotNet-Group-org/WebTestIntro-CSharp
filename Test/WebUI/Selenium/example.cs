using NUnit.Framework;
using Selenium;

namespace Selenium
{
    [TestFixture()]
    public class ExampleFixture
    {
        private DefaultSelenium browser;

        private string testURL;

        // there are no "tester" objects like in NUnitAsp - Selenium will parse HTML output with string identifiers
        private string pnlEntry = "pnlEntry";
        private string txtName = "txtName";
        private string btnSubmit = "btnSubmit";
        private string pnlResults = "pnlResults";
        private string lblName = "lblName";

        // SetUp is called before every test method
        // NOTE: for Selenium we do need to use the <SetUp> attribute
        [SetUp()]
        protected void SetUp()
        {
            // set test URL
            this.testURL = "http://localhost/test/WebTestIntro/SeleniumExample.aspx";

            // Instantiate Selenium and start the browser (I believe this is where it actually creates a browser window)
            // 4444 is the default port for the Selenium Server            
            this.browser = new DefaultSelenium("localhost", 4444, "*iexplore", this.testURL);
            this.browser.Start();

            // navigate to the page
            this.browser.Open(this.testURL);

            // verify we made it to the page
            Assert.AreEqual(this.testURL, this.browser.GetLocation());

        }

        // does our page load as expected?
        [Test()]
        public void SmokeTest()
        {
            // These is no AssertVisibility convenience method provided by Selenium, but it's so useful I created my own
            AssertVisibility(this.pnlEntry, true);

            AssertVisibility(this.txtName, true);
            AssertVisibility(this.btnSubmit, true);

            AssertVisibility(this.pnlResults, false);
            AssertVisibility(this.lblName, false);

        }

        private void AssertVisibility(string controlID, bool shouldBeVisible)
        {
            if (shouldBeVisible)
            {
                try
                {
                    if (this.browser.GetElementIndex(controlID) >= 0) return;
                }
                catch (System.Exception ex)
                {
                    Assert.Fail("Control '" + controlID + "' should be Visible.");
                }
            }
            else
            {
                try
                {
                    if (this.browser.GetElementIndex(controlID) < 0) return;
                    Assert.Fail("Control '" + controlID + "' should NOT be Visible.");
                }
                catch (System.Exception ex)
                {
                    return;
                }
            }

        }

        // Let's try some interaction
        [Test()]
        public void EnterName()
        {
            string name = "John Hilts";

            // put some text in a text box
            this.browser.Type(this.txtName, name);

            // click the submit button
            this.browser.Click(this.btnSubmit);
            // NOTE: alternatively, we could have done this to submit the form:
            // this.browser.Submit()

            // Selenium will time out a lot but that can be prevented with wait calls
            // this isn't a problem with NUnitAsp or WatiN
            this.browser.WaitForPageToLoad("6000");

            // Retrieves the message of a JavaScript confirmation dialog generated during the previous action. 
            // By default, the confirm function will return true, having the same effect as manually clicking OK. 
            this.browser.GetConfirmation();

            // after post back, verify that the label displays the name
            Assert.AreEqual(name, this.browser.GetText(this.lblName), "Wrong Name");

            // verify visibility of the rest of the controls
            AssertVisibility(this.pnlEntry, false);
            AssertVisibility(this.txtName, false);
            AssertVisibility(this.btnSubmit, false);

            AssertVisibility(this.pnlResults, true);
            AssertVisibility(this.lblName, true);

        }

        [TearDown()]
        protected virtual void TearDown()
        {
            // this closes the browser window
            this.browser.Stop();
            // if you want to see what the browser looks like, comment out the .Stop call

        }

    }

}
