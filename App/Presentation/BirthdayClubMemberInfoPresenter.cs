using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Globalization;

using Business;

namespace Presentation
{
    public class BirthdayClubMemberInfoPresenter
    {
        private IBirthdayClubMemberInfoView view;
        private BirthdayClubMemberInfo birthdayClubMemberInfo;
        public const string FeedbackMessage = "New Member Added";

        private DataTable birthdayClubMemberRecords;

        public BirthdayClubMemberInfoPresenter(IBirthdayClubMemberInfoView view, BirthdayClubMemberInfo birthdayClubMemberInfo)
        {
            this.view = view;
            this.birthdayClubMemberInfo = birthdayClubMemberInfo;
            this.LoadSchedule();
            this.BindLanguages();
        }

        public BirthdayClubMemberInfoPresenter(IBirthdayClubMemberInfoView view, string dataPath)
            : this(view, new BirthdayClubMemberInfo(dataPath))
        {
        }

        private void LoadSchedule()
        {
            this.birthdayClubMemberRecords = this.birthdayClubMemberInfo.List();
        }

        public void calBirthDateSchedule_DayRender(Object sender, System.Web.UI.WebControls.DayRenderEventArgs e)
        {
            DataRow[] birthdaysOnThisDay = this.birthdayClubMemberRecords.Select("Birthdate = '" + e.Day.Date.ToShortDateString() + "'");
            if (birthdaysOnThisDay != null)
            {
                Literal lit = new Literal();
                lit.Visible = true;
                lit.Text = "<br />";
                e.Cell.Controls.Add(lit);
                System.Text.StringBuilder memberNames = new System.Text.StringBuilder(256);
                foreach (DataRow row in birthdaysOnThisDay)
                {
                    memberNames.AppendFormat("{0}<br>", row["MemberName"].ToString());
                }
                Label lbl = new Label();
                lbl.Visible = true;
                lbl.Text = string.Format("<font color='red'>{0}</font>", memberNames.ToString());
                e.Cell.Controls.Add(lbl);
            }
        }

        public void calBirthDatePicker_SelectionChanged(Object sender, System.EventArgs e)
        {
            this.view.Birthdate.Text = this.view.BirthdatePicker.SelectedDate.ToShortDateString();
        }

        public void BindLanguages()
        {
            this.view.BindLanguages();
        }

        public void ddlLanguages_SelectedIndexChanged(Object sender, System.EventArgs e)
        {
            string specificLanguageSelection = this.view.Languages.SelectedValue;
            string specificLanguage = CultureInfo.GetCultureInfo(specificLanguageSelection).EnglishName;
        }

        public void btnAdd_Click(Object sender, System.EventArgs e)
        {
            this.birthdayClubMemberInfo.MemberName = view.MemberName.Text;
            this.birthdayClubMemberInfo.Birthdate = Convert.ToDateTime(view.Birthdate.Text);
            this.birthdayClubMemberInfo.Add();
            this.view.InfoMessage = FeedbackMessage;
            this.view.BirthdateSchedule.VisibleDate = this.birthdayClubMemberInfo.Birthdate;
        }
    }
}
