using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace formSILC
{
    public partial class INTEIRO2 : UserControl
    {
        public INTEIRO2()
        {
            InitializeComponent();
        }
        private void txtCodigoBarras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
            {
                e.Handled = true;
                return;
            }
            if ((Char.IsLetter(e.KeyChar)) || (Char.IsWhiteSpace(e.KeyChar)) ||
                (Char.IsSymbol(e.KeyChar)) || (Char.IsPunctuation(e.KeyChar)))
                e.Handled = true;
        }

        private void VALOR_Leave(object sender, EventArgs e)
        {
            if (VALOR.Text.Length > 0)
                VALOR.Text = Convert.ToDecimal(VALOR.Text).ToString("0");
        }

        private void VALOR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == "'"[0])
                e.Handled = true;
        }
    }
}
