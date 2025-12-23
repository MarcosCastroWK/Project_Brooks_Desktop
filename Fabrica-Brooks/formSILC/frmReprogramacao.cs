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
    public partial class frmReprogramacao : Form
    {
        public bool bSalvar = false;
        public string pDataProgramacaoAberta = "";
        public string pDataProgramada = "";
        private clsTABMotivosOBSDados oTabMotivosObs = new clsTABMotivosOBSDados();
        private BindingSource bindingSource = new BindingSource();

        public frmReprogramacao()
        {
            InitializeComponent();
        }

        private void frmReprogramacao_Load(object sender, EventArgs e)
        {
            clsTABMotivosOBSDados oTabMotivosObs = new clsTABMotivosOBSDados();
            foreach (DataRow _dr in oTabMotivosObs.PegaDados(0).Rows)
                cboTabMotivosOBS.Items.Add(_dr["Descricao"]);            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cboTabMotivosOBS.Text == "")
                MessageBox.Show("Observação inválida!");
            else if (Convert.ToDateTime(dtpDataReprogramada.Text) < Convert.ToDateTime(pDataProgramacaoAberta))
                MessageBox.Show("Data reprogramação inválida!");
            else if (Convert.ToDateTime(dtpDataReprogramada.Text) <= Convert.ToDateTime(pDataProgramada)) // verificar amanhã com mais carinho
                MessageBox.Show("Data reprogramação inválida! Reprograme somente para data futura (excluir no dia para voltar).");  
            else
            {
                pDataProgramada = dtpDataReprogramada.Text;
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
