using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibSILC;
using SILCNegocios;

namespace formSILC

{
    public partial class FUNCIONARIO : UserControl
    {
        private clsFuncionarioDados oFuncionarioDados = new clsFuncionarioDados();
        private string Tabela;
        
        public int TabIndexCodigo
        {
            set { txtCodigo.TabIndex = value; }
            get { return txtCodigo.TabIndex; }
        }
        public FUNCIONARIO(string pTabela)
        {
            InitializeComponent();
            Tabela = pTabela;
        }
        public FUNCIONARIO()
        {
            InitializeComponent();
        }
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Char.IsLetter(e.KeyChar)) || (Char.IsWhiteSpace(e.KeyChar)) ||
                (Char.IsPunctuation(e.KeyChar)) || (Char.IsSymbol(e.KeyChar)))
                e.Handled = true;
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (geral.VoltaForm == "")
                    geral.VoltaForm = "FUNCIONARIO";
                frmProcura frm = new frmProcura(geral.VoltaForm);
                frm.ShowDialog();
                AtribuiCamposControle();
            }
        }

        private void txtCodigo_Leave(object sender, EventArgs e)
        {
            AtribuiCamposControle();
        }

        public void AtribuiCamposControle()
        {
            if (geral.CodigoMotorista > 0)
                txtCodigo.Text = geral.CodigoMotorista.ToString();
            clsFuncionarios oFuncionario = new clsFuncionarios();
            if (txtCodigo.Text.Length > 0)
            {
                oFuncionario = oFuncionarioDados.PegaDados(oFuncionario, Convert.ToInt32(txtCodigo.Text));
                txtDescricao.Text = oFuncionario.Nome;
            }
            geral.CodigoMotorista = 0;
        }

    }
}
