using NUnit.Framework;
// these are the imports for NUnitAsp
using NUnit.Extensions.Asp;
// this maps the the Asp Server Side control testers
using NUnit.Extensions.Asp.AspTester;
// NUnit.Extensions.Asp.HtmlTester maps to HTML Server Side Control testers

namespace NUnitAsp
{
    // All test NUnitAsp test fixtures need to inherit the WebFormTestCase class, which is defined in NUnitAdapter
    [TestFixture()]
    public class ExampleFixture : NUnit.Extensions.Asp.WebFormTestCase
    {
        private string testURL;

        // define test objects that correspond to server side controls on web page
        private PanelTester pnlEntry;
        private TextBoxTester txtName;
        private ButtonTester btnSubmit;
        private PanelTester pnlResults;
        private LabelTester lblName;

        /*
        ' SetUp is called before every test method
        ' NOTE: NUnitAsp's WebFormTestCase already has a method with the <SetUp> attribute, so applying it here causes 
        ' problems
        */
        protected override void SetUp()
        {
            // set test URL
            this.testURL = @"http://localhost/test/WebTestIntro/NUnitAspExample.aspx";

            // instantiate testers.  Note that the naming container can be specified
            this.pnlEntry = new PanelTester("pnlEntry");
            this.txtName = new TextBoxTester("txtName", this.pnlEntry);
            this.btnSubmit = new ButtonTester("btnSubmit", this.pnlEntry);
            this.pnlResults = new PanelTester("pnlResults");
            this.lblName = new LabelTester("lblName", this.pnlResults);

            // navigate to the page
            base.Browser.GetPage(this.testURL);

            // verify we made it to the page
            Assert.AreEqual(this.testURL, base.Browser.CurrentUrl.ToString());

        }

        // does our page load as expected?
        [Test()]
        public void SmokeTest()
        {
            // These first 2 methods are functionally equivalent.  AssertVisibility is a convenience method provided by 
            // NUnitAsp
            Assert.IsTrue(this.pnlEntry.Visible, "Entry Panel should be visible");
            AssertVisibility(this.pnlEntry, true);

            AssertVisibility(this.txtName, true);
            AssertVisibility(this.btnSubmit, true);

            AssertVisibility(this.pnlResults, false);
            AssertVisibility(this.lblName, false);

        }

        // Let's try some interaction
        [Test()]
        public void EnterName()
        {
            string name = "John Hilts";

            // put some text in a text box
            this.txtName.Text = name;

            // click the submit button
            // NOTE: NUnitAsp also provides a form tester, so if there wasn't a button available, you could still post
            // the form
            this.btnSubmit.Click();

            // after post back, verify that the label displays the name
            Assert.AreEqual(name, this.lblName.Text, "Wrong Name");

            // verify visibility of the rest of the controls
            AssertVisibility(this.pnlEntry, false);
            AssertVisibility(this.txtName, false);
            AssertVisibility(this.btnSubmit, false);

            AssertVisibility(this.pnlResults, true);
            AssertVisibility(this.lblName, true);

        }

    }

}
