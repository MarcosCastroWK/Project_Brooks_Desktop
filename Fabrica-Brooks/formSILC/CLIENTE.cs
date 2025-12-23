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
    public partial class CLIENTE : UserControl
    {
        private clsClienteDados oClienteDados = new clsClienteDados();
        private string Tabela;

        public int TabIndexCodigo
        {
            set { txtCodigo.TabIndex  = value; }
            get { return txtCodigo.TabIndex; }
        }
        public CLIENTE(string pTabela)
        {
            InitializeComponent();
            Tabela = pTabela;
        }
        public CLIENTE()
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
                    geral.VoltaForm = "CLIENTE";
                frmProcura frm = new frmProcura(geral.VoltaForm);
                frm.ShowDialog();
                if (geral.CodigoCliente > 0)
                {
                    AtribuirCamposControle();
                }
            }
        }
        public void AtribuirCamposControle()
        {
            if (geral.CodigoCliente > 0)
                txtCodigo.Text = geral.CodigoCliente.ToString();
            clsClientes oCliente = new clsClientes();
            if (txtCodigo.Text.Length > 0)
            {
                oCliente = oClienteDados.PegaDados(oCliente, Convert.ToInt32(txtCodigo.Text));
                txtDescricao.Text = oCliente.NomeFantasia;
            }
            geral.CodigoCliente = 0;
        }
        private void txtCodigo_Leave(object sender, EventArgs e)
        {
            AtribuirCamposControle();
        }
    }
}
