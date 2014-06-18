using NUnit.Framework;
using Selenium;

namespace Selenium
{
    [TestFixture()]
    public class SmokeTestFixture
    {
        private DefaultSelenium browser;

        private string testURL;

        [Test(), Category("SmokeTest")]
        public void SmokeTest()
        {
            this.testURL = "http://localhost/test/WebTestIntro/SmokeTest.aspx";

            this.browser = new DefaultSelenium("localhost", 4444, "*iexplore", this.testURL);
            this.browser.Start();
            this.browser.Open(this.testURL);
            Assert.AreEqual(this.testURL, this.browser.GetLocation());

        }

        [TearDown()]
        protected virtual void TearDown()
        {
            this.browser.Stop();

        }

    }

}