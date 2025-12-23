using System;
public partial class forms_MOEDA : System.Web.UI.UserControl
{
    public string Valor
    {
        set 
        {
            if (value == "")
                value = "0,00";
            if (value != "")
                value = Convert.ToDecimal(value).ToString();
            txtMoeda.Text = value; 
        }
        get
        {
            try
            {
                if (txtMoeda.Text == "")
                    txtMoeda.Text = "0,00";
                if (txtMoeda.Text != "")
                    txtMoeda.Text = Convert.ToDecimal(txtMoeda.Text).ToString();
            }
            catch
            {
                txtMoeda.Text = "0,00";
            }
            return txtMoeda.Text;
        }
    }
    public short IndiceTab
    {
        set { txtMoeda.TabIndex = value; }
        get { return txtMoeda.TabIndex; }
    }
    public System.Drawing.Color ForeColor
    {
        set { txtMoeda.ForeColor = value; }
        get { return txtMoeda.ForeColor; }
    }    
    public bool Enabled
    {
        set { txtMoeda.Enabled = value; }
        get { return txtMoeda.Enabled; }
    }
    public System.Drawing.Color BackColor
    {
        set { txtMoeda.BackColor = value; }
        get { return txtMoeda.BackColor; }
    }
    public string CssClass
    {
        set { txtMoeda.CssClass = value; }
        get { return txtMoeda.CssClass; }

    }
    public string Style 
    {
        set { txtMoeda.Style.Value = value; }
        get { return txtMoeda.Style.Value; }

    }
    public System.Web.UI.WebControls.BorderStyle BorderStyle
    {
        set { txtMoeda.BorderStyle = value; }
        get { return txtMoeda.BorderStyle; }

    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}