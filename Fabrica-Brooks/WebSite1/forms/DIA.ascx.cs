using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class forms_DIA : System.Web.UI.UserControl
{
    public string Valor
    {
        set { txtDia.Text = value; }
        get { return txtDia.Text; }
    }
    public short IndiceTab
    {
        set { txtDia.TabIndex = value; }
        get { return txtDia.TabIndex; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}