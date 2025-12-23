using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class forms_INTEIRO : System.Web.UI.UserControl
{
    public string Valor
    {
        set { txtInteiro.Text = value; }
        get { return txtInteiro.Text; }
    }
    public short IndiceTab
    {
        set { txtInteiro.TabIndex = value; }
        get { return txtInteiro.TabIndex; }
    }
    public System.Drawing.Color ForeColor
    {
        set { txtInteiro.ForeColor = value; }
        get { return txtInteiro.ForeColor; }
    }
    public System.Drawing.Color BackColor
    {
        set { txtInteiro.BackColor = value; }
        get { return txtInteiro.BackColor; }
    }
    public bool Enabled
    {
        set { txtInteiro.Enabled = value; }
        get { return txtInteiro.Enabled; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}