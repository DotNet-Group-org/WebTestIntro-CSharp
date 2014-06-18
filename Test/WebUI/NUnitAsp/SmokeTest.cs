using NUnit.Framework;
using NUnit.Extensions.Asp;
using NUnit.Extensions.Asp.AspTester;

namespace NUnitAsp
{

    [TestFixture()]
    public class SmokeTestFixture : NUnit.Extensions.Asp.WebFormTestCase
    {
        private string testURL;

        protected override void SetUp()
        {
            this.testURL = "http://localhost/test/WebTestIntro/SmokeTest.aspx";

        }

        [Test(), Category("SmokeTest")]
        public void SmokeTest()
        {
            base.Browser.GetPage(this.testURL);
            Assert.AreEqual(this.testURL, base.Browser.CurrentUrl.ToString());

        }

    }

}