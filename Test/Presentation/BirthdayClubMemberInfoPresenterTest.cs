using System;
using System.Drawing;
using System.Web.UI.WebControls;
using NUnit.Framework;
using Rhino.Mocks;
using Presentation;
using Business;

namespace Test.Presentation
{
    [TestFixture()] public class BirthdayClubMemberInfoPresenterTest
    {
        private MockRepository mocks;
        private BirthdayClubMemberInfoPresenter presenter;
        private IBirthdayClubMemberInfoView view;
        private BirthdayClubMemberInfo birthdayClubMemberInfo;

        [SetUp()] public void SetUp()
        {
            this.mocks = new MockRepository();
            this.view = mocks.CreateMock<IBirthdayClubMemberInfoView>();
            this.birthdayClubMemberInfo = mocks.CreateMock<BirthdayClubMemberInfo>();
            this.presenter = new BirthdayClubMemberInfoPresenter(this.view, birthdayClubMemberInfo);
        }

        [TearDown()] public void TearDown()
        {
            this.mocks.VerifyAll();
        }

    }
}
