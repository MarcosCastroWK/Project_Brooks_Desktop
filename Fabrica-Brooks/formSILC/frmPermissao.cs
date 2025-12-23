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
    public partial class frmPermissao : Form
    {
        public bool bPermitido = false;
        clsUsuarioDados oUDados = new clsUsuarioDados();
        public string sSenhaSupervisor = "2511";
        public frmPermissao()
        {
            InitializeComponent();
            sSenhaSupervisor = oUDados.PegaSenha("Cris", 1);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if ((txtSenha.Text == "01" && geral.VoltaForm != "frmLocacoes") || (txtSenha.Text == sSenhaSupervisor && geral.VoltaForm == "frmLocacoes"))
                bPermitido = true;
            this.Close();
        }
    }
}
