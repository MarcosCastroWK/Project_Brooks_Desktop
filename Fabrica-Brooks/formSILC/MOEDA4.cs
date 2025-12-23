using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace formSILC
{
    public partial class MOEDA4 : UserControl
    {
        public MOEDA4()
        {
            InitializeComponent();
        }

        private void txtCodigoBarras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == "'"[0])
                e.Handled = true;

            if (e.KeyChar == '.')
            {
                e.Handled = true;
                return;
            }
            if ((Char.IsLetter(e.KeyChar)) || (Char.IsWhiteSpace(e.KeyChar)) ||
                (Char.IsSymbol(e.KeyChar)))
                e.Handled = true;
            if (e.KeyChar == ',')
                if (((TextBox)sender).Text.IndexOf(",") > -1) e.Handled = true;
        }

        private void VALOR_Leave(object sender, EventArgs e)
        {
            if (VALOR.Text.Length > 0)
                VALOR.Text = Convert.ToDecimal(VALOR.Text.Replace("R$", "")).ToString("N4");
        }
    }
}
