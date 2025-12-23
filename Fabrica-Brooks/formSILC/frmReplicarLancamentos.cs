using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using LibSILC;
using SILCNegocios;

namespace formSILC
{
    public partial class frmReplicarLancamentos : Form
    {
        public string pNumeroLancamento;
        public string pCodigoResiduo;
        private clsLancamentoMTR oLancamentoDTR = new clsLancamentoMTR();
        private clsLancamentoMTRDados oLancamentoMTRDados = new clsLancamentoMTRDados();

        private BindingSource bindingSource = new BindingSource();
        private DataTable _dt = new DataTable();
        private int regs = 0;
        private int pagina = 0;
        private int LimiteLnImpressora = 1040;
        
        public frmReplicarLancamentos()
        {
            InitializeComponent();
        }
        private void frmReplicarLancamentos_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - 50;
            Grade.Height = this.Height - 150;
            lblLinhas.Top = Grade.Height + 20;
            lblReplicarLancAtual.Top = Grade.Height + 20;
            in2ReplicarVezes.Top = Grade.Height + 18;
            in2ReplicarVezes.VALOR.Text = "1";
            btnReplicar.Top = Grade.Height + 40;
            lblMensagem.Top = Grade.Height + 40;
            lblMensagem.Text = "...";
            btnGravar.Top = Grade.Height + 66;
            Grade.AutoGenerateColumns = false;
            this.Grade.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            PreencheGrade("NumeroLancamento Desc");
        }
        private void PreencheGrade(string pOrdem)
        {
            oLancamentoMTRDados = new clsLancamentoMTRDados();
            _dt = oLancamentoMTRDados.PreencheDT_Replicar(pOrdem, pNumeroLancamento, pCodigoResiduo);
            bindingSource.DataSource = _dt;
            Grade.DataSource = bindingSource;
            lblLinhas.Text = "Linhas grade: " + _dt.Rows.Count.ToString();
        }
        private void Grade_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

            }
        }
        private void Grade_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (Grade.CurrentCell is DataGridViewCheckBoxCell)
            {
                Grade.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        private void btnImprimirSelecionados_Click(object sender, EventArgs e)
        {

        }
        private void Grade_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Grade.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
                Grade.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        private void btnReplicar_Click(object sender, EventArgs e)
        {
            if (in2ReplicarVezes.VALOR.Text != "")
            {
                for (int v = 1; v <= Convert.ToInt16(in2ReplicarVezes.VALOR.Text); v++)
                {
                    _dt.NewRow();
                    _dt.Rows.Add();
                    for (int c = 0; c < _dt.Columns.Count; c++)
                    {
                        _dt.Rows[_dt.Rows.Count - 1][c] = _dt.Rows[_dt.Rows.Count - 2][c];
                    }
                }
                bindingSource.DataSource = _dt;
                Grade.DataSource = bindingSource;
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            bool bSalvar = false;
            for (int ii = 0; ii < Grade.Rows.Count - 1; ii++)
            {
                if (Grade.Rows[ii].Cells["NumeroMTR"].Value.ToString() != "")
                {
                    bSalvar = true;
                }
            }
            if (!bSalvar)
                MessageBox.Show("Falta digitar MTR!");
            else
            {
                Salvar();
                this.Close();
            }
        }
        private void Salvar()
        {
            clsLancamentos oLancamentos = new clsLancamentos();
            clsLancamentoMTR oLancMTR = new clsLancamentoMTR();

            for (int ii = 0; ii < Grade.Rows.Count - 1; ii++)
            {
                // primeiro que ter o lançamento novo - pois não pode ter um mesmo número de resíduo
                oLancamentos.NumeroLancamento = 0;
                oLancamentos.NumeroCaixa = Grade.Rows[ii].Cells["NumeroCaixa"].Value.ToString();
                oLancamentos.CodigoCliente = Convert.ToInt32(Grade.Rows[ii].Cells["CodigoCliente"].Value.ToString());
                oLancamentos.CodigoCaminhaoColoca = Convert.ToInt32(Grade.Rows[ii].Cells["CodigoCaminhaoColoca"].Value.ToString());
                oLancamentos.CodigoMotoristaColocou = Convert.ToInt32(Grade.Rows[ii].Cells["CodigoMotoristaColocou"].Value.ToString());
                oLancamentos.Data = DateTime.Now.ToString();
                oLancamentos.DataColocacao = Grade.Rows[ii].Cells["DataColocacao"].Value.ToString();
                oLancamentos.DataRetirada = "01/01/0001";
                if (Grade.Rows[ii].Cells["DataRetirada"].Value.ToString() != "")
                    oLancamentos.DataRetirada = Grade.Rows[ii].Cells["DataRetirada"].Value.ToString();
                if (Grade.Rows[ii].Cells["CodigoMotoristaRetirou"].Value.ToString() != "")
                    oLancamentos.CodigoMotoristaRetirou = Convert.ToInt32(Grade.Rows[ii].Cells["CodigoMotoristaRetirou"].Value.ToString());
                if (Grade.Rows[ii].Cells["CodigoCaminhoRetirada"].Value.ToString() != "")
                    oLancamentos.CodigoCaminhoRetirada = Convert.ToInt32(Grade.Rows[ii].Cells["CodigoCaminhoRetirada"].Value.ToString());
                oLancamentos.HoraRetirada = DateTime.Now.ToString("hh:mm");
                oLancamentos.Horas = 0;
                oLancamentos.HorasColocacao = DateTime.Now.ToString("hh:mm");
                oLancamentos.TipoOperacao = 0;
                oLancamentos.Horas = Convert.ToInt32(DateTime.Now.Ticks.ToString().Substring(2, 8));

                clsLancamentosDados oLancamentoDados = new clsLancamentosDados();
                oLancMTR.NumeroLancamento = oLancamentoDados.Inserir(oLancamentos);

                //
                oLancMTR.CodigoAterroSanitario = 0;
                if (Grade.Rows[ii].Cells["CodigoAterroSanitario"].Value.ToString() != "")
                    oLancMTR.CodigoAterroSanitario = Convert.ToInt32(Grade.Rows[ii].Cells["CodigoAterroSanitario"].Value.ToString());
                if (pCodigoResiduo != "") // chave
                    oLancMTR.CodigoResiduo = Convert.ToInt32(pCodigoResiduo);
                oLancMTR.DataDescarga = "01/01/0001"; // chave
                if (Grade.Rows[ii].Cells["DataDescarga"].Value.ToString() != "")
                    oLancMTR.DataDescarga = Grade.Rows[ii].Cells["DataDescarga"].Value.ToString();
                oLancMTR.Deposito = Grade.Rows[ii].Cells["Deposito"].Value.ToString();
                if (Grade.Rows[ii].Cells["Franquia"].Value.ToString() != "")
                    oLancMTR.Franquia = Convert.ToDecimal(Grade.Rows[ii].Cells["Franquia"].Value.ToString());
                oLancMTR.Motivo = Grade.Rows[ii].Cells["Motivo"].Value.ToString();
                if (Grade.Rows[ii].Cells["NumeroMTR"].Value.ToString() != "") // chave
                    oLancMTR.NumeroMTR = Convert.ToInt32(Grade.Rows[ii].Cells["NumeroMTR"].Value.ToString());
                if (Grade.Rows[ii].Cells["NumeroMTRFatima"].Value.ToString() != "") 
                    oLancMTR.NumeroMTRFatima = Convert.ToInt64(Grade.Rows[ii].Cells["NumeroMTRFatima"].Value.ToString());
                oLancMTR.observacao = Grade.Rows[ii].Cells["observacao"].Value.ToString();
                if (Grade.Rows[ii].Cells["Quantidade"].Value.ToString() != "")
                    oLancMTR.Quantidade = Convert.ToDecimal(Grade.Rows[ii].Cells["Quantidade"].Value.ToString());
                oLancMTR.Ticket = Grade.Rows[ii].Cells["Ticket"].Value.ToString();
                oLancMTR.Unidade = Grade.Rows[ii].Cells["Unidade"].Value.ToString();
                if (Grade.Rows[ii].Cells["ValorTotal"].Value.ToString() != "")
                    oLancMTR.ValorTotal = Convert.ToDecimal(Grade.Rows[ii].Cells["ValorTotal"].Value.ToString());
                if (Grade.Rows[ii].Cells["ValorUnitario"].Value.ToString() != "")
                    oLancMTR.ValorUnitario = Convert.ToDecimal(Grade.Rows[ii].Cells["ValorUnitario"].Value.ToString());

                clsLancamentoMTRDados oLancamentoMTRDados = new clsLancamentoMTRDados();
                if (oLancMTR.CodigoResiduo > 0 && oLancMTR.NumeroMTR > 0 && oLancMTR.NumeroLancamento > 0)
                    oLancamentoMTRDados.Inserir(oLancMTR);
            }
        }
    }
}
