using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class SeleniumExample : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

                if (!this.IsPostBack )return;

        string name = this.txtName.Text;
        this.pnlEntry.Visible = false;

        this.pnlResults.Visible = true;

        this.lblName.Text = this.Server.HtmlEncode(name);

    }
}
