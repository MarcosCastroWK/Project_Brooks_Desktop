using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SILC.Web.forms
{
    public partial class INTEIROMES : System.Web.UI.UserControl
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
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }

}