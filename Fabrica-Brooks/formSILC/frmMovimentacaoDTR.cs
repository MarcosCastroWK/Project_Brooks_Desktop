using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using SILCNegocios;
using LibSILC;
using System.Windows.Forms;

namespace formSILC
{
    public partial class frmMovimentacaoDTR : Form
    {
        private clsMovimentacaoDTR oMovDTR = new clsMovimentacaoDTR();
        private clsMovimentacaoDTRDados oMovDTRdados = new clsMovimentacaoDTRDados();
        private clsCacambaDados oContainerDados = new clsCacambaDados();

        public string pNumeroLancamento = "";
        public string pCodigoResiduo = "";
        public string pCodigoCliente = "";
        public string pContainerLocal = "";

        private DataTable _dt = new DataTable();

        private BindingSource bindingSource = new BindingSource();

        public frmMovimentacaoDTR()
        {
            InitializeComponent();
        }

        private void frmMovimentacaoDTR_Load(object sender, EventArgs e)
        {
            Grade.AutoGenerateColumns = false;
            geral.Ordem = "Data";
            _dt = oMovDTRdados.PreencheDataTableMovimentacaoDTR(geral.Ordem, pNumeroLancamento, pCodigoResiduo, pCodigoCliente);
            DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn();
            cmb.HeaderText = "Para";
            cmb.DataPropertyName = "MoviCxPara";
            cmb.Name = "cboMoviCxPara";
            cmb.MaxDropDownItems = 4;
            cmb.Width = 90;
            cmb.ReadOnly = false;
            cmb.SortMode = DataGridViewColumnSortMode.Automatic;
            cmb.Items.Clear();
            cmb.Items.Add("");
            foreach (DataRow _drCx in oContainerDados.PreencheDataTableCacambas("Numero").Rows)
                cmb.Items.Add(_drCx["Numero"].ToString());
            Grade.Columns.Add(cmb);

            NovaLinhaParaDt();
            bindingSource.DataSource = _dt;
            Grade.DataSource = bindingSource.DataSource;

            DataGridViewComboBoxCell cmc = new DataGridViewComboBoxCell();
            cmc = (DataGridViewComboBoxCell) Grade.Rows[0].Cells["cboMoviCxPara"];
            if (Grade.Rows.Count > 1)
                cmc.ReadOnly = true;

        }
        private void NovaLinhaParaDt()
        {
            _dt.NewRow();
            _dt.Rows.Add();
            _dt.Rows[_dt.Rows.Count - 1]["Codigo"] = 0;
            _dt.Rows[_dt.Rows.Count - 1]["Data"] = DateTime.Now.ToString("dd/MM/yyyy");
            _dt.Rows[_dt.Rows.Count - 1]["MoviCxDe"] = pContainerLocal;
            _dt.Rows[_dt.Rows.Count - 1]["MoviCxPara"] = "";
            _dt.Rows[_dt.Rows.Count - 1]["NumeroLancamento"] = pNumeroLancamento;
            _dt.Rows[_dt.Rows.Count - 1]["CodigoResiduo"] = pCodigoResiduo;
            _dt.Rows[_dt.Rows.Count - 1]["CodigoCliente"] = pCodigoCliente;
        }
        private void SalvarLog(string pOperacao, clsMovimentacaoDTR pMovimentoDTR)
        {
            clsLog oLog = new clsLog();
            clsLogDados oLogDados = new clsLogDados();
            oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
            oLog.CodigoUsuario = geral.CodigoUsuarioAtual;
            if (geral.CodigoUsuarioAtual == 0)
            {
                clsUsuarioDados oUsuarioDados = new clsUsuarioDados();
                oLog.CodigoUsuario = oUsuarioDados.PegaCodigoUsuario(geral.UsuarioAtual, oUsuarioDados.PegaSenha(geral.UsuarioAtual, 1), 1);
                geral.CodigoUsuarioAtual= oLog.CodigoUsuario;
            }
            oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
            oLog.LocalOperacao = "Movimentação DTR";
            oLog.Operacao = pOperacao;
            oLog.Log = oLog.Log + "Data: " + pMovimentoDTR.Data + " ";
            oLog.Log = oLog.Log + "De  : " + pMovimentoDTR.MoviCxDe + " ";
            oLog.Log = oLog.Log + "Para: " + pMovimentoDTR.MoviCxPara + " ";
            oLog.Log = oLog.Log + "Cliente: " + pMovimentoDTR.CodigoCliente + " ";
            oLog.Log = oLog.Log + "Resíduo: " + pMovimentoDTR.CodigoResiduo + " ";
            oLog.Log = oLog.Log + "Nº Lanç: " + pMovimentoDTR.NumeroLancamento + " ";
            oLogDados.Inserir(oLog);
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            DataGridViewComboBoxCell cmc = new DataGridViewComboBoxCell();
            cmc = (DataGridViewComboBoxCell)Grade.Rows[Grade.Rows.Count - 1].Cells["cboMoviCxPara"];
            oMovDTR.MoviCxPara = cmc.Value.ToString();
            if (oMovDTR.MoviCxPara != "")
            {
                pContainerLocal = oMovDTR.MoviCxPara;
                oMovDTR.Data = Grade.Rows[Grade.Rows.Count - 1].Cells["Data"].Value.ToString();
                if (Grade.Rows.Count > 1)
                    cmc = (DataGridViewComboBoxCell)Grade.Rows[Grade.Rows.Count - 2].Cells["cboMoviCxPara"];
                else
                    cmc.Value = pContainerLocal;
                oMovDTR.MoviCxDe = Grade.Rows[Grade.Rows.Count - 1].Cells["MoviCxDe"].Value.ToString();
                oMovDTR.NumeroLancamento = Convert.ToInt32(Grade.Rows[Grade.Rows.Count - 1].Cells["NumeroLancamento"].Value.ToString());
                oMovDTR.CodigoResiduo = Convert.ToInt32(Grade.Rows[Grade.Rows.Count - 1].Cells["CodigoResiduo"].Value.ToString());
                oMovDTR.CodigoCliente = Convert.ToInt32(Grade.Rows[Grade.Rows.Count - 1].Cells["CodigoCliente"].Value.ToString());
                oMovDTRdados.Inserir(oMovDTR);
                SalvarLog("Inclusão", oMovDTR);
                this.Close();
            }
            else
            {
                MessageBox.Show("Container inválido!");
            }
        }
        private void Grade_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridViewComboBoxCell cmc = new DataGridViewComboBoxCell();
            if (Grade.Rows.Count > 1)
            {
                for (int i = 0; i <= Grade.Rows.Count - 3; i++)
                {
                    cmc = (DataGridViewComboBoxCell)Grade.Rows[i].Cells["cboMoviCxPara"];
                    cmc.ReadOnly = true;
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DialogResult _result = new DialogResult();
            if (Grade.Rows.Count > 2)
            {
                DataGridViewComboBoxCell cmc = new DataGridViewComboBoxCell();
                cmc = (DataGridViewComboBoxCell)Grade.Rows[Grade.Rows.Count - 2].Cells["cboMoviCxPara"];

                _result = MessageBox.Show("Excluir movimento: " + Convert.ToDateTime(Grade.Rows[Grade.Rows.Count - 2].Cells[0].Value.ToString()).ToString("dd/MM/yyyy") + " de: " +
                                           Grade.Rows[Grade.Rows.Count - 2].Cells[1].Value.ToString() + " para: " + cmc.Value.ToString(), "Excluir", MessageBoxButtons.YesNo);
                if (_result == DialogResult.Yes)
                {
                    pContainerLocal = Grade.Rows[Grade.Rows.Count - 2].Cells["MoviCxDe"].Value.ToString();
                    oMovDTRdados.ExcluirMovimentoIncorreto(Grade.Rows[Grade.Rows.Count - 2].Cells["NumeroLancamento"].Value.ToString(),
                                                           Grade.Rows[Grade.Rows.Count - 2].Cells["CodigoCliente"].Value.ToString(),
                                                           Grade.Rows[Grade.Rows.Count - 2].Cells["CodigoResiduo"].Value.ToString(),
                                                           Grade.Rows[Grade.Rows.Count - 2].Cells["Data"].Value.ToString(),
                                                           Grade.Rows[Grade.Rows.Count - 2].Cells["MoviCxDe"].Value.ToString(),
                                                           cmc.Value.ToString());
                    oMovDTR.CodigoCliente = Convert.ToInt32(Grade.Rows[Grade.Rows.Count - 2].Cells["CodigoCliente"].Value.ToString());
                    oMovDTR.CodigoResiduo = Convert.ToInt32(Grade.Rows[Grade.Rows.Count - 2].Cells["CodigoResiduo"].Value.ToString());
                    oMovDTR.NumeroLancamento = Convert.ToInt32(Grade.Rows[Grade.Rows.Count - 2].Cells["NumeroLancamento"].Value.ToString());
                    oMovDTR.Data = Grade.Rows[Grade.Rows.Count - 2].Cells["Data"].Value.ToString();
                    oMovDTR.MoviCxDe = Grade.Rows[Grade.Rows.Count - 2].Cells["MoviCxDe"].Value.ToString();
                    oMovDTR.MoviCxPara = cmc.Value.ToString();
                    SalvarLog("Exclusão", oMovDTR);
                    this.Close();
                   
                }
            }
        }
    }
}
