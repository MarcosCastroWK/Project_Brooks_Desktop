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
    public partial class RESIDUO : UserControl
    {
        private clsResiduoDados oResiduoDados = new clsResiduoDados();
        private string Tabela;
        private string _unidade;

        public string Unidade
        {
            get { return _unidade; }
            set { _unidade = value; }
        }
        public int TabIndexCodigo
        {
            set { txtCodigo.TabIndex = value; }
            get { return txtCodigo.TabIndex; }
        }
        public RESIDUO(string pTabela)
        {
            InitializeComponent();
            Tabela = pTabela;
        }
        public RESIDUO()
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
                    geral.VoltaForm = "RESIDUO";
                frmProcura frm = new frmProcura(geral.VoltaForm);
                frm.ShowDialog();
                AtribuiCamposControle();
            }
        }

        private void txtCodigo_Leave(object sender, EventArgs e)
        {
            AtribuiCamposControle();
        }

        private void AtribuiCamposControle()
        {
            if (geral.CodigoResiduo > 0)
                txtCodigo.Text = geral.CodigoResiduo.ToString();
            clsResiduos oResiduo = new clsResiduos();
            if (txtCodigo.Text.Length > 0)
            {
                oResiduo = oResiduoDados.PegaDados(oResiduo, Convert.ToInt32(txtCodigo.Text));
                txtDescricao.Text = oResiduo.DescricaoReduzida;
                _unidade = oResiduo.Unidade;
            }
            geral.CodigoResiduo = 0;
        }
    }
}
