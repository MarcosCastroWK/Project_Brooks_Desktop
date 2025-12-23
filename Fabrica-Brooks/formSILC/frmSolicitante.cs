using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibSILC;

namespace formSILC
{
    public partial class frmSolicitante : Form
    {
        public bool bSalvar = false;

        public frmSolicitante()
        {
            InitializeComponent();
        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            //salvar cancelamento
            bSalvar = true;
            this.Close();               
        }

        private void txtSolicitante_Leave(object sender, EventArgs e)
        {
            txtSolicitante.Text = txtSolicitante.Text.ToUpper();
        }

        private void frmSolicitante_Load(object sender, EventArgs e)
        {
            //
        }
    }
}
