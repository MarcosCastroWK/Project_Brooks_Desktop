using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SILC.Web.forms
{
    public partial class INTEIRO7 : System.Web.UI.UserControl
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
        public bool Enabled
        {
            set { txtInteiro.Enabled = value; }
            get { return txtInteiro.Enabled; }
        }
        public override void Focus()
        {
            txtInteiro.Focus();
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
        public string CssClass
        {
            set { txtInteiro.CssClass = value; }
            get { return txtInteiro.CssClass; }
        }
        public string Style
        {
            set { txtInteiro.Style.Value = value; }
            get { return txtInteiro.Style.Value; }
        }
        public System.Web.UI.WebControls.BorderStyle BorderStyle
        {
            set { txtInteiro.BorderStyle = value; }
            get { return txtInteiro.BorderStyle; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}