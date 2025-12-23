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
    public partial class frmDestinoFinal : Form
    {
        public bool bSalvar = false;
        private clsResiduoDados oResDados = new clsResiduoDados();
        private SILCNegocios.clsResiduos oRes = new SILCNegocios.clsResiduos();
        private BindingSource bindingSource = new BindingSource();

        public frmDestinoFinal()
        {
            InitializeComponent();
        }

        private void frmDestinoFinal_Load(object sender, EventArgs e)
        {            
            PreencheCamposUnidadeDestinoFinal();
        }

        private void PreencheCamposUnidadeDestinoFinal()
        {
            if (residuo1.txtCodigo.Text != "")
            {
                oRes = oResDados.PegaDados(oRes, Convert.ToInt32(residuo1.txtCodigo.Text));
                cboDestinoFinal.Items.Clear();
                cboDestinoFinal.Items.Add(cboDestinoFinal.Text);
                cboDestinoFinal.Text = cboDestinoFinal.Text;
                cboDestinoFinal.Items.Add(oRes.DescricaoDestinoFinal);
                cboDestinoFinal.Items.Add("");
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //salvar cancelamento
            bSalvar = true;
            this.Close();               
        }

        private void Residuo1_Leave(object sender, EventArgs e)
        {
            PreencheCamposUnidadeDestinoFinal();
        }

        private void Residuo1_Enter(object sender, EventArgs e)
        {
            geral.VoltaForm = this.Name;
        }

        private void cboDestinoFinal_Leave(object sender, EventArgs e)
        {
            cboDestinoFinal.Text = cboDestinoFinal.Text.ToUpper();
        }
    }
}
