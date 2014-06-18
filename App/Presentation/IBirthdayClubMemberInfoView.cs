using System.Web.UI.WebControls;

namespace Presentation
{
    public interface IBirthdayClubMemberInfoView
    {
        DropDownList Languages { get;}
        TextBox MemberName { get;}
        Label Birthdate { get;}
        Calendar BirthdatePicker { get;}

        Calendar BirthdateSchedule { get;}

        string ErrorMessage { set;}
        string InfoMessage { set;}

        void BindLanguages();
    }
}
