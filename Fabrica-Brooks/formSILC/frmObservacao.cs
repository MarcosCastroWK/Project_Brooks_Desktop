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
    public partial class frmObservacao : Form
    {
        public bool bSalvar = false;
        private clsTABMotivosOBSDados oTabMotivosObs = new clsTABMotivosOBSDados();
        private BindingSource bindingSource = new BindingSource();

        public frmObservacao()
        {
            InitializeComponent();
        }

        private void frmObservacao_Load(object sender, EventArgs e)
        {
            clsTABMotivosOBSDados oTabMotivosObs = new clsTABMotivosOBSDados();
            foreach (DataRow _dr in oTabMotivosObs.PegaDados(0).Rows)
                cboTabMotivosOBS.Items.Add(_dr["Descricao"]);            

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //salvar cancelamento
            bSalvar = true;
            this.Close();               
        }

        private void cboTabMotivosOBS_Leave(object sender, EventArgs e)
        {
            cboTabMotivosOBS.Text = cboTabMotivosOBS.Text.ToUpper();
        }
    }
}
