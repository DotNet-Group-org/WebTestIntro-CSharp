using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Globalization;

using Presentation;

public partial class BirthdayClubMemberInfo : System.Web.UI.Page , IBirthdayClubMemberInfoView
{
        private BirthdayClubMemberInfoPresenter presenter;

    public DropDownList Languages
    {
        get {
            return this.ddlLanguages;
        }
}
    public TextBox MemberName
    {
        get
        {
            return this.txtMemberName;
        }
    }
   public Label Birthdate
{
        get
{
            return this.lblBirthdate;
}
        set
{
            this.lblBirthdate = value;
}
}
    public System.Web.UI.WebControls.Calendar BirthdatePicker
{
        get
{
            return this.calBirthDatePicker;
}
        set{
            this.calBirthDatePicker = value;
}
}
    public System.Web.UI.WebControls.Calendar BirthdateSchedule
    {
        get
        {
            return this.calBirthDateSchedule;
        }
        set
        {
            this.calBirthDateSchedule = value;
        }
    }

    public string ErrorMessage
    {
        set
        {
            this.lblMessage.Text = value;
            this.lblMessage.CssClass = "error";
        }
    }

    public string InfoMessage
    {
        set
        {
            this.lblMessage.Text = value;
            this.lblMessage.CssClass = "message";
        }
    }

    public void BindLanguages() 
    {

        this.AddCulture("en-US");
        this.AddCulture("es-MX");
        this.AddCulture("ja-JP");

    }

    private void AddCulture(string name)
    {
        CultureInfo ci = new CultureInfo(name);
        this.ddlLanguages.Items.Add(new ListItem(ci.NativeName, ci.Name));

    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        this.presenter = new BirthdayClubMemberInfoPresenter(this, this.Server.MapPath("~/App_Data/"));

        ddlLanguages.SelectedIndexChanged += new EventHandler(presenter.ddlLanguages_SelectedIndexChanged);
        btnAdd.Click += new EventHandler(presenter.btnAdd_Click);
        calBirthDatePicker.SelectionChanged += new EventHandler(presenter.calBirthDatePicker_SelectionChanged);
        calBirthDateSchedule.DayRender += new DayRenderEventHandler(presenter.calBirthDateSchedule_DayRender);

    }

    protected override  void InitializeCulture()
    {
        if (this.Request.Form["ddlLanguages"] != null)
        {
            this.UICulture = this.Request.Form["ddlLanguages"];
            this.Culture = this.Request.Form["ddlLanguages"];
        }

        base.InitializeCulture();

    }

    protected void Page_Load(object sender, EventArgs e)
    {

        this.lblMessage.Text = "";

        if (this.IsPostBack) return;

        this.txtMemberName.Focus();

        this.SetLanguageList();

    }

    private void SetLanguageList()
    {
        string currentBrowserCulture = this.Request.UserLanguages[0];

        if (currentBrowserCulture != this.ddlLanguages.SelectedValue)
        {
            this.ddlLanguages.SelectedValue = currentBrowserCulture;
        }

    }
}
