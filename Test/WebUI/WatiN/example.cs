using NUnit.Framework;

using WatiN.Core;
using WatiN.Core.DialogHandlers;

namespace WatiN
{
    [TestFixture()]
    public class ExampleFixture
    {
        private IE browser;

        private string testURL;

        // define test objects that correspond to server side controls on web page
        // note: I'm defining these objects based on the client-side equivalent of the server-side control
        // divs for panels, spans for labels, etc
        private Div pnlEntry;
        private TextField txtName;
        private Button btnSubmit;
        private Div pnlResults;
        private Span lblName;

        // SetUp is called before every test method
        // NOTE: for WatiN we do need to use the <SetUp> attribute
        [SetUp()]
        protected virtual void SetUp()
        {
            // set test URL
            this.testURL = "http://localhost/test/WebTestIntro/NUnitAspExample.aspx"; // I'm using the same example as NUnitAsp

            // navigate to the page
            this.browser = new IE(this.testURL);

            // verify we made it to the page
            Assert.AreEqual(this.testURL, this.browser.Url);

            this.GetControls(false);

        }

        private void GetControls(bool isPostBack)
        {
            // instantiate testers.
            if (isPostBack)
            {
                this.pnlResults = new Div(this.browser.Element(Find.ById("pnlResults")));
                this.lblName = new Span(this.browser.Element(Find.ById("lblName")));
            }
            else
            {
                this.pnlEntry = new Div(this.browser.Element(Find.ById("pnlEntry")));
                this.txtName = new TextField(this.browser.Element(Find.ById("txtName")));
                this.btnSubmit = new Button(this.browser.Element(Find.ById("btnSubmit")));
            }

        }

        // does our page load as expected?
        [Test()]
        public void SmokeTest()
        {
            // These is no AssertVisibility convenience method provided by WatiN, but it's so useful I created my own
            AssertVisibility(this.pnlEntry.Id, true);

            AssertVisibility(this.txtName.Id, true);
            AssertVisibility(this.btnSubmit.Id, true);

            // AssertVisibility(this.pnlResults.Id, False)
            // AssertVisibility(this.lblName.Id, False)

        }

        private void AssertVisibility(string id, bool shouldBeVisible)
        {
            Assert.AreEqual(shouldBeVisible, this.browser.Element(Find.ById(id)).Exists);

        }

        protected void AssertVisibility(Element element, bool shouldBeVisible)
        {
            Assert.AreEqual(shouldBeVisible, this.browser.Element(Find.ById(element.Id)).Exists);

        }

        // Let's try some interaction
        [Test()]
        public void EnterName()
        {
            string name = "John Hilts";

            // put some text in a text box
            this.txtName.TypeText(name);

            // click the submit button
            this.btnSubmit.Click();
            // NOTE: alternatively, we could have done this to submit the form:
            // this.Browser.Form("FormID").Submit()

            // click the confirm dialog OK button
            //ConfirmDialogHandler confirm = new ConfirmDialogHandler();
            //this.Browser.AddDialogHandler(confirm);
            //confirm.WaitUntilExists(5);
            //confirm.OKButton.Click();

            // NOTE: In WatiN, I need to instantiate (re-instantiate) any controls I want to reference after post-back
            this.GetControls(true);

            // after post back, verify that the label displays the name
            Assert.AreEqual(name, this.lblName.Text, "Wrong Name");

            // verify visibility of the rest of the controls
            AssertVisibility(this.pnlEntry, false);
            AssertVisibility(this.txtName, false);
            AssertVisibility(this.btnSubmit, false);

            AssertVisibility(this.pnlResults, true);
            AssertVisibility(this.lblName, true);

        }

        [TearDown()]
        protected void TearDown()
        {
            this.browser.Close();

        }

    }

}