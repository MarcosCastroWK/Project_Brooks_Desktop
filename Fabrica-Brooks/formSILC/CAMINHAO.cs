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
    public partial class CAMINHAO : UserControl
    {
        private clsCaminhoesDados oCaminhoesDados = new clsCaminhoesDados();
        private string Tabela;

        public int TabIndexCodigo
        {
            set { txtCodigo.TabIndex = value; }
            get { return txtCodigo.TabIndex; }
        }

        public CAMINHAO(string pTabela)
        {
            InitializeComponent();
            Tabela = pTabela;
        }
        public CAMINHAO()
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
                    geral.VoltaForm = "CAMINHAO";
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
            if (geral.CodigoCaminhao > 0)
                txtCodigo.Text = geral.CodigoCaminhao.ToString();
            clsCaminhoes oCaminhao = new clsCaminhoes();
            if (txtCodigo.Text.Length > 0)
            {
                oCaminhao = oCaminhoesDados.PegaDados(oCaminhao, Convert.ToInt32(txtCodigo.Text));
                txtDescricao.Text = oCaminhao.Modelo;
            }
            geral.CodigoCaminhao = 0;
        }
    }
}
