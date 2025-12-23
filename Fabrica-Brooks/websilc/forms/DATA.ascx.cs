using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class forms_DATA : System.Web.UI.UserControl
{
    public string Data
    {
        set { txtData.Text = value; }
        get { return txtData.Text; }
    }
    public short IndiceTab
    {
        set { txtData.TabIndex = value; }
        get { return txtData.TabIndex; }
    }
    public bool Enabled
    {
        set { txtData.Enabled = value; }
        get { return txtData.Enabled; }
    }
    public override void Focus()
    {
        base.Focus();
        txtData.Focus();
    }
    public System.Web.UI.WebControls.BorderStyle BorderStyle
    {
        set { txtData.BorderStyle = value; }
        get { return txtData.BorderStyle; }
    }
    public string CssClass
    {
        set { txtData.CssClass = value; }
        get { return txtData.CssClass; }
    }
    public string Style
    {
        set { txtData.Style.Value = value; }
        get { return txtData.Style.Value; }
    }
    public System.Drawing.Color ForeColor
    {
        set { txtData.ForeColor = value; }
        get { return txtData.ForeColor; }
    }
    public System.Drawing.Color BackColor
    {
        set { txtData.BackColor = value; }
        get { return txtData.BackColor; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}