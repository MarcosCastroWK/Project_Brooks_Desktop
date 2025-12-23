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
using SILCNegocios;

namespace formSILC
{
    public partial class frmAntecipacao : Form
    {
        public bool bSalvar = false;
        public string pDataProgramacaoAberta = "";
        private clsTABMotivosOBSDados oTabMotivosObs = new clsTABMotivosOBSDados();
        private BindingSource bindingSource = new BindingSource();
        private DateTime dataProgRecebida;

        public frmAntecipacao()
        {
            InitializeComponent();
        }

        private void frmReprogramacao_Load(object sender, EventArgs e)
        {
            dataProgRecebida = Convert.ToDateTime(dtpDataReprogramada.Text);
            clsTABMotivosOBSDados oTabMotivosObs = new clsTABMotivosOBSDados();
            foreach (DataRow _dr in oTabMotivosObs.PegaDados(0).Rows)
                cboTabMotivosOBS.Items.Add(_dr["Descricao"]);            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cboTabMotivosOBS.Text == "")
                MessageBox.Show("Observação inválida!");
            else if (Convert.ToDateTime(dtpDataReprogramada.Text) < Convert.ToDateTime(pDataProgramacaoAberta))
                MessageBox.Show("Data para antecipação inválida (inferior)!");
            else if (Convert.ToDateTime(dtpDataReprogramada.Text) >= Convert.ToDateTime(dataProgRecebida))
                MessageBox.Show("Data para antecipação inválida (igual/superior)!");
            else
            {
                //salvar reprogramacao
                bSalvar = true;
                this.Close();
            }                
        }

        private void cboTabMotivosOBS_Leave(object sender, EventArgs e)
        {
            cboTabMotivosOBS.Text = cboTabMotivosOBS.Text.ToUpper();
        }
    }
}
