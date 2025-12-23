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
    public partial class frmConfirmaTroca : Form
    {
        public bool bSalvar = false;
        private BindingSource bindingSource = new BindingSource();

        public frmConfirmaTroca()
        {
            InitializeComponent();
        }

        private void frmConfirmaTroca_Load(object sender, EventArgs e)
        {
            //
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (intNumeroLancamentoTroca.VALOR.Text != "")
            {
                clsLancamentosDados oLancDados = new clsLancamentosDados();
                clsLancamentos oLancs = new clsLancamentos();
                oLancDados.PegaDados(oLancs, Convert.ToInt32(intNumeroLancamentoTroca.VALOR.Text));
                if (geral.TratarData(oLancs.DataColocacao).ToString() == "" || oLancs.DataColocacao == null)
                    MessageBox.Show("Número do Lançamento inválido!");
                else
                    bSalvar = true;
            }
            this.Close();
        }
    }
}
