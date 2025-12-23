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
    public partial class frmDTR : Form
    {
        private clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        private clsDTR oDTR = new clsDTR();
        private clsDTRDados oDTRDados = new clsDTRDados();
        private clsUsuarios oUsuario = new clsUsuarios();
        private clsDestinoFinalDados oDestinoFinalDados = new clsDestinoFinalDados();
        string pCodigoResiduo = "";
        private DataTable _dt = new DataTable();
        private int regs = 0;
        private decimal pagina = 0;
        private string _destinoFinalSelecionado = "";
        private string _dataSaidaSelecionada = "";
        public bool bSalvar = false;
        private BindingSource bindingSource = new BindingSource();

        public frmDTR()
        {
            InitializeComponent();
        }

        private void frmDTR_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " - Usuário logado: " + geral.UsuarioAtual;
            this.Cursor = Cursors.WaitCursor;
            btnEnviar.Enabled = false;
            datDataInicial.Value = DateTime.Now.AddDays(-200);
            Consultar();
            DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn();
            cmb.HeaderText = "Destino Final";
            cmb.DataPropertyName = "DestinoFinal";
            cmb.Name = "cboDestinoFinal";
            cmb.MaxDropDownItems = 4;
            cmb.Width = 160;
            cmb.SortMode = DataGridViewColumnSortMode.Automatic;
            cmb.Items.Clear();
            cmb.Items.Add("");
            foreach (DataRow _drDestino in oDestinoFinalDados.PreencheDTDestinoSoCodigoNome("NomeFantasia").Rows)
                cmb.Items.Add(_drDestino["NomeFantasia"].ToString());
            Grade.Columns.Add(cmb);

            Grade.Columns.Add("Imprimido", "I");
            Grade.Columns["Imprimido"].SortMode = DataGridViewColumnSortMode.Automatic;
            Grade.Columns["Imprimido"].DataPropertyName = "Imprimido";
            Grade.Columns["Imprimido"].Width = 20;
            Grade.Columns["Imprimido"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grade.Columns["Imprimido"].ReadOnly = true;

            bindingSource.DataSource = _dt;
            Grade.DataSource = bindingSource.DataSource;
            SomenteLeitura();
            HabilitaEnviar();
            this.Cursor = Cursors.Default;
        }

        private void SomenteLeitura()
        {
            foreach (DataGridViewRow dgvr in Grade.Rows)
            {
                if (dgvr.Cells["Imprimido"].Value != null)
                {
                    if (dgvr.Cells["Imprimido"].Value.ToString() == "I")
                    {
                        DataGridViewComboBoxCell cbo = new DataGridViewComboBoxCell();
                        cbo = (DataGridViewComboBoxCell)dgvr.Cells["cboDestinoFinal"];
                        cbo.ReadOnly = true;
                        dgvr.Cells["Imprimido"].ReadOnly = true;
                        dgvr.Cells["DataSaida"].ReadOnly = true;
                    }
                }
            }
        }
        private void HabilitaEnviar()
        {
            btnEnviar.Enabled = false;
            foreach (DataGridViewRow dgvr in Grade.Rows)
            {
                if (dgvr.Cells["Imprimido"].Value != null)
                {
                    if (dgvr.Cells["Imprimido"].Value.ToString() == "I")
                        btnEnviar.Enabled = true;
                }
            }

        }
        private void Consultar()
        {
            Grade.AutoGenerateColumns = false;
            geral.Ordem = "Residuo, DataColeta";
            _dt = oDTRDados.PegaDadosArmazenados(oDTR, 0, false, datDataInicial.Value.ToString("yyyy-MM-dd"), geral.Ordem, pCodigoResiduo);
            foreach (DataRow _dr in _dt.Rows)
            {
                _dr["DataSaida"] = Convert.ToDateTime(_dr["DataSaida"]).ToString("dd/MM/yyyy");
                if (_dr["DataSaida"].ToString() == "01/01/0100" || _dr["DataSaida"].ToString() == "01/01/0001" || _dr["DataSaida"].ToString() == "01/01/1900")
                    _dr["DataSaida"] = "";
                if (_dr["Imprimido"].ToString() == "1")
                    _dr["Imprimido"] = "I";
                else
                    _dr["Imprimido"] = "";
            }

            lblLote.Text = (oDTRDados.UltimoRegistro() + 1).ToString();
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Consultar();
            bindingSource.DataSource = _dt;
            Grade.DataSource = bindingSource.DataSource;
            HabilitaEnviar();
            this.Cursor = Cursors.Default;
        }
        private void SalvarLog(string pOperacao, clsDTR pDTR)
        {
            clsLog oLog = new clsLog();
            clsLogDados oLogDados = new clsLogDados();
            oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
            oLog.CodigoUsuario = oUsuario.Codigo;
            oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
            oLog.LocalOperacao = "DTR";
            oLog.Operacao = pOperacao;
            oLog.Log = oLog.Log + "Sequencial: " + pDTR.Sequencial + " ";
            oLog.Log = oLog.Log + "Data Coleta: " + pDTR.DataColeta + " ";
            oLog.Log = oLog.Log + "Código Cliente: " + pDTR.CodigoClienteColetado + " ";
            oLog.Log = oLog.Log + "Cliente: " + pDTR.ClienteColetado + " \n";
            oLog.Log = oLog.Log + "Código resíduo: " + pDTR.CodigoTipoResiduo + " \n";
            oLog.Log = oLog.Log + "Resíduo: " + pDTR.TipoResiduo + " \n";
            oLog.Log = oLog.Log + "Data Saída: " + pDTR.DataSaida + " \n";
            oLog.Log = oLog.Log + "Local de entrega: " + pDTR.LocalEntrega + " \n";
            oLogDados.Inserir(oLog);
        }
        protected void Salvar()
        {
            // varrer linhas e ver se foi colocada DataSaida e ou LocalEntrega, desta forma salvar uma nova linha na tabela DTR, caso não exista, 
            // verificar se a linha existe na tabela DTR, mas se DataSaida e LocalEntrega, nulos excluir
            foreach (DataGridViewRow _gdr in Grade.Rows)
            {
                if (_gdr.Cells["Lote"].Value != null)
                {
                    oDTR.NumeroLancamento = Convert.ToInt32(_gdr.Cells["Lote"].Value.ToString().Split("-"[0])[0]);
                    oDTR.NumeroMTR = Convert.ToInt32(_gdr.Cells["Lote"].Value.ToString().Split("-"[0])[1]);
                    oDTR.CodigoTipoResiduo = Convert.ToInt32(_gdr.Cells["Lote"].Value.ToString().Split("-"[0])[2]);
                    oDTR.CodigoClienteColetado = Convert.ToInt32(_gdr.Cells["Lote"].Value.ToString().Split("-"[0])[3]);
                    oDTR.Sequencial = Convert.ToInt32(_gdr.Cells["Lote"].Value.ToString().Split("-"[0])[4]);
                    if (oDTR.Sequencial <= 0)
                        oDTR.Sequencial = Convert.ToInt32(lblLote.Text);
                    oDTR.DataColeta = _gdr.Cells[0].Value.ToString();

                    clsClienteDados oClienteDado = new clsClienteDados();
                    oDTR.ClienteColetado = oClienteDado.PegaNomeFantasia(oDTR.CodigoClienteColetado);

                    clsResiduoDados oResiduoDado = new clsResiduoDados();
                    oDTR.TipoResiduo = oResiduoDado.PegaDescricao(oDTR.CodigoTipoResiduo);

                    if (_gdr.Cells["Quantidade"].Value.ToString() != "")
                        oDTR.TotalKg = Convert.ToDecimal(_gdr.Cells["Quantidade"].Value);
                    oDTR.LocalDTR = _gdr.Cells["LocalDTR"].Value.ToString();
                    oDTR.DataSaida = _gdr.Cells["DataSaida"].Value.ToString();

                    DataGridViewComboBoxCell _cbo = new DataGridViewComboBoxCell();
                    _cbo = (DataGridViewComboBoxCell)_gdr.Cells["cboDestinoFinal"];
                    oDTR.LocalEntrega = _cbo.Value.ToString();

                    oDTR.Imprimido = 0;
                    if (_gdr.Cells["Imprimido"].Value.ToString() == "I")
                    {
                        oDTR.Imprimido = 1;
                    }
                    if (oDTRDados.DadoExiste(oDTR.Sequencial, oDTR.NumeroLancamento, oDTR.CodigoTipoResiduo, oDTR.NumeroMTR) == "Incluir")
                    {
                        if (oDTR.DataSaida != "" || oDTR.LocalEntrega != "")
                        {
                            SalvarLog("Inclusão", oDTR);
                            oDTRDados.Inserir(oDTR);
                        }
                    }
                    else
                    {
                        if (oDTR.DataSaida == "" && oDTR.LocalEntrega == "")
                        {
                            SalvarLog("Exclusão", oDTR);
                            // excluir linha
                            oDTRDados.Excluir(oDTR.Sequencial, oDTR.NumeroLancamento, oDTR.NumeroMTR, oDTR.CodigoTipoResiduo);
                        }
                        else
                        {
                            SalvarLog("Alteração", oDTR);
                            // alterar data saida e ou local entrega
                            oDTRDados.AlterarArmazenados(oDTR, oDTR.Sequencial, oDTR.NumeroLancamento, oDTR.NumeroMTR, oDTR.CodigoTipoResiduo);
                        }
                    }
                }
            }
            Consultar();
            bindingSource.DataSource = _dt;
            Grade.DataSource = bindingSource.DataSource;
        }

        private void Grade_MouseClick(object sender, MouseEventArgs e)
        {            
            DataGridViewComboBoxCell cbc = new DataGridViewComboBoxCell();
            foreach (DataGridViewRow gvr in Grade.Rows)
            {
                if (gvr.Cells["DataSaida"].Selected)
                {
                    _dataSaidaSelecionada = gvr.Cells["DataSaida"].Value.ToString();
                    cbc = (DataGridViewComboBoxCell) gvr.Cells["cboDestinoFinal"];
                    if (cbc.Value.ToString() != "")
                        _destinoFinalSelecionado = cbc.Value.ToString();
                    break;
                }
            }
            int iSels = 0;
            foreach (DataGridViewRow gvr in Grade.Rows)
            {
                if (gvr.Cells["DataSaida"].Selected)
                    iSels++;
            }
            foreach (DataGridViewRow gvr in Grade.Rows)
            {
                if (gvr.Cells["LocalDTR"].Selected)
                {
                    frmMovimentacaoDTR ofrmMovDTR = new frmMovimentacaoDTR();
                    ofrmMovDTR.pCodigoCliente = gvr.Cells["CodigoCliente"].Value.ToString();
                    ofrmMovDTR.pCodigoResiduo = gvr.Cells["CodigoResiduo"].Value.ToString();
                    ofrmMovDTR.pContainerLocal = gvr.Cells["LocalDTR"].Value.ToString();
                    ofrmMovDTR.pNumeroLancamento = gvr.Cells["NumeroLancamento"].Value.ToString();
                    ofrmMovDTR.Grade.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
                    ofrmMovDTR.ShowDialog();
                    gvr.Cells["LocalDTR"].Value = ofrmMovDTR.pContainerLocal;
                    break;
                }
                else if (gvr.Cells["DataSaida"].Selected && gvr.Cells["Imprimido"].Value.ToString() == "")
                {
                    if (_dataSaidaSelecionada == "" && iSels == 1)
                        gvr.Cells["DataSaida"].Value = DateTime.Now.AddDays(-1).ToShortDateString();
                    else
                        gvr.Cells["DataSaida"].Value = _dataSaidaSelecionada;
                    cbc = (DataGridViewComboBoxCell)gvr.Cells["cboDestinoFinal"];
                    if (_dataSaidaSelecionada == "" && gvr.Cells["DataSaida"].Value.ToString() == "")
                        cbc.Value = "";
                    else
                        cbc.Value = _destinoFinalSelecionado;
                }
            }
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Salvar();
            this.Cursor = Cursors.Default;
        }
        private void btnImprimirMTR_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (geral.CodigoUsuarioAtual != 10)
                Salvar();
            bool bContinuarAteAcabar = true;
            while (bContinuarAteAcabar)
            {
                string _ResiduoDataSaidaLocalEntrega = "";
                string _ResiduoDataSaidaLocalEntregaAnterior = "0";

                // pegar novos dados e continuar imprimindo
                DataTable _dtRelacao = new DataTable();
                _dtRelacao.Columns.Add("DataColeta");
                _dtRelacao.Columns.Add("CodigoCliente");
                _dtRelacao.Columns.Add("CNPJ_CPF");
                _dtRelacao.Columns.Add("Gerador");
                _dtRelacao.Columns.Add("PesoKg");
                _dtRelacao.Columns.Add("Percentual");
                _dtRelacao.Columns.Add("NumeroMTRe");
                _dtRelacao.Columns.Add("DescricaoResiduo");
                _dtRelacao.Columns.Add("DataSaida");
                _dtRelacao.Columns.Add("LocalEntrega");
                _dtRelacao.Columns.Add("CodigoResiduo");
                _dtRelacao.Columns.Add("Sequencial");
                _dtRelacao.Columns.Add("NumeroLancamento");

                clsResiduoDados oResiduoDado = new clsResiduoDados();
                int _NumeroLancamento = 0;
                int _CodigoResiduo = 0;
                int _CodigoCliente = 0;
                int _numeroMTR = 0;
                string _DescricaoResiduo = "";
                bool _EncontrouItemsParaImprimir = false;
                // pegar o primeiro residuo com datasaida e local entrega
                foreach (DataGridViewRow gvr in Grade.Rows)
                {
                    if (gvr.Cells["Lote"].Value != null)
                    {
                        _CodigoResiduo = Convert.ToInt32(gvr.Cells["Lote"].Value.ToString().Split("-"[0])[2]);
                        _DescricaoResiduo = oResiduoDado.PegaDescricao(_CodigoResiduo);
                        _ResiduoDataSaidaLocalEntrega = _DescricaoResiduo + "-" + gvr.Cells["DataSaida"].Value.ToString() + "-" + ((DataGridViewComboBoxCell)gvr.Cells["cboDestinoFinal"]).Value.ToString();

                        if (gvr.Cells["DataSaida"].Value.ToString() != "" && ((DataGridViewComboBoxCell)gvr.Cells["cboDestinoFinal"]).Value.ToString() != "" &&
                            _ResiduoDataSaidaLocalEntregaAnterior != _ResiduoDataSaidaLocalEntrega && (gvr.Cells["Imprimido"].Value.ToString() != "I"))
                        {
                            _EncontrouItemsParaImprimir = true;
                            break;
                        }
                    }

                }
                if (_EncontrouItemsParaImprimir)
                {
                    clsClientes oCliente = new clsClientes();
                    clsClienteDados oClienteDados = new clsClienteDados();
                    // adiciona todos do primeiro selecionado 
                    foreach (DataGridViewRow gvr in Grade.Rows)
                    {
                        if (gvr.Cells["Lote"].Value != null)
                        {
                            _NumeroLancamento = Convert.ToInt32(gvr.Cells["Lote"].Value.ToString().Split("-"[0])[0]);
                            _numeroMTR = Convert.ToInt32(gvr.Cells["Lote"].Value.ToString().Split("-"[0])[1]);
                            _CodigoResiduo = Convert.ToInt32(gvr.Cells["Lote"].Value.ToString().Split("-"[0])[2]);
                            _DescricaoResiduo = oResiduoDado.PegaDescricao(_CodigoResiduo);
                            if (_ResiduoDataSaidaLocalEntrega == (_DescricaoResiduo + "-" + (gvr.Cells["DataSaida"].Value.ToString() + "-" + ((DataGridViewComboBoxCell)gvr.Cells["cboDestinoFinal"]).Value.ToString())))
                            {
                                _CodigoCliente = Convert.ToInt32(gvr.Cells["Lote"].Value.ToString().Split("-"[0])[3]);
                                oCliente = oClienteDados.PegaDados(oCliente, _CodigoCliente);
                                _dtRelacao.NewRow();
                                _dtRelacao.Rows.Add();
                                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["DataColeta"] = gvr.Cells["DataColeta"].Value.ToString();
                                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["CodigoCliente"] = Convert.ToInt32(gvr.Cells["Lote"].Value.ToString().Split("-"[0])[3]);
                                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["CNPJ_CPF"] = oCliente.CNPJ_CPF;
                                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["Gerador"] = geral.Left(oCliente.Nome, 58);
                                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["PesoKg"] = gvr.Cells["Quantidade"].Value.ToString();
                                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["Percentual"] = 0; // ok
                                clsLancamentoMTRDados oLancMTRDados = new clsLancamentoMTRDados();
                                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["NumeroMTRe"] = oLancMTRDados.RetornaNumeroMTRe(_NumeroLancamento, _CodigoResiduo, _numeroMTR);
                                _CodigoResiduo = Convert.ToInt32(gvr.Cells["Lote"].Value.ToString().Split("-"[0])[2]);
                                _DescricaoResiduo = oResiduoDado.PegaDescricao(_CodigoResiduo);
                                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["DescricaoResiduo"] = _DescricaoResiduo;
                                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["DataSaida"] = geral.DataFormatada(gvr.Cells["DataSaida"].Value.ToString());
                                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["LocalEntrega"] = ((DataGridViewComboBoxCell)gvr.Cells["cboDestinoFinal"]).Value.ToString();
                                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["CodigoResiduo"] = _CodigoResiduo.ToString();
                                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["Sequencial"] = gvr.Cells["Lote"].Value.ToString().Split("-"[0])[4];
                                _dtRelacao.Rows[_dtRelacao.Rows.Count - 1]["NumeroLancamento"] = _NumeroLancamento.ToString();
                            }
                        }
                    }
                    pagina = 0;
                    regs = 0;

                    frmVisualPrintDTR ofrmVisual = new frmVisualPrintDTR();
                    ofrmVisual._dt = _dtRelacao;
                    ofrmVisual.ShowDialog();

                    frmVisualPrintMTR ofrmVisualMTR = new frmVisualPrintMTR();
                    ofrmVisualMTR._dt = _dtRelacao;
                    ofrmVisualMTR.ShowDialog();
                    if (ofrmVisualMTR.bImprimiu)
                    {
                        ofrmVisualMTR.bImprimiu = false;
                        clsDTR oDTR = new clsDTR();
                        clsDTRDados oDTRDados = new clsDTRDados();
                        int _ultimoNumeroImpressao = oDTRDados.PegaUltimoNumeroImpressao() + 1;
                        foreach (DataRow dr in _dtRelacao.Rows)
                        {
                            oDTRDados.SalvarComoImprimido(Convert.ToInt32(dr["Sequencial"]), Convert.ToInt32(dr["NumeroLancamento"]),
                                                          Convert.ToInt32(dr["CodigoResiduo"]), ofrmVisualMTR.iCodigoCaminhao, ofrmVisualMTR.iCodigoMotorista, _ultimoNumeroImpressao);

                            foreach (DataGridViewRow dgvr in Grade.Rows)
                            {
                                if (dgvr.Cells["Lote"].Value != null)
                                {
                                    if (dgvr.Cells["Lote"].Value.ToString().Split("-"[0])[4] == dr["Sequencial"].ToString() &&
                                        dgvr.Cells["Lote"].Value.ToString().Split("-"[0])[0] == dr["NumeroLancamento"].ToString() &&
                                        dgvr.Cells["Lote"].Value.ToString().Split("-"[0])[2] == dr["CodigoResiduo"].ToString())
                                    {
                                        DataGridViewComboBoxCell cbo = new DataGridViewComboBoxCell();
                                        cbo = (DataGridViewComboBoxCell)dgvr.Cells["cboDestinoFinal"];
                                        cbo.ReadOnly = true;
                                        dgvr.Cells["Imprimido"].Value = "I";
                                        dgvr.Cells["DataSaida"].ReadOnly = true;
                                        btnEnviar.Enabled = true;
                                    }
                                }
                            }
                        }
                        ofrmVisualMTR.Close();
                    }
                    else
                    {
                        DialogResult _result = new DialogResult();
                        _result = MessageBox.Show("Continuar?", "Imprimindo pendentes", MessageBoxButtons.YesNo);
                        if (_result == DialogResult.No)
                        {
                            bContinuarAteAcabar = false;
                            break;
                        }
                    }
                }
                else
                {
                    bContinuarAteAcabar = false;
                    MessageBox.Show("Não há mais dados ou não existe selecionados para imprimir!");
                }
            }
            this.Cursor = Cursors.Default;
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            DataTable _dtOrdenado = new DataTable();
            foreach (DataColumn dc in _dt.Columns)
                _dtOrdenado.Columns.Add(dc.ColumnName);

            foreach (DataGridViewRow gvr in Grade.Rows)
            {
                _dtOrdenado.NewRow();
                _dtOrdenado.Rows.Add();
                foreach (DataColumn dc in _dt.Columns)
                {
                    if (dc.ColumnName != "Sequencial" && dc.ColumnName != "NumeroMTR")
                    {
                        if (dc.ColumnName == "DestinoFinal")
                        {
                            _dtOrdenado.Rows[_dtOrdenado.Rows.Count - 1][dc.ColumnName] = ((DataGridViewComboBoxCell)gvr.Cells["cbo" + dc.ColumnName]).Value;
                        }
                        else
                        {
                            if (gvr.Cells[dc.ColumnName].Value != null)
                                _dtOrdenado.Rows[_dtOrdenado.Rows.Count - 1][dc.ColumnName] = gvr.Cells[dc.ColumnName].Value.ToString();
                        }
                    }
                }
            }
            Salvar();
            this.Cursor = Cursors.Default;

            frmVisualPrintEstoque ofrmVisualPrintEstoqueDTR = new frmVisualPrintEstoque();
            ofrmVisualPrintEstoqueDTR._dt = _dtOrdenado;
            ofrmVisualPrintEstoqueDTR.ShowDialog();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            int _NumeroLancamento = 0;
            int _CodigoResiduo = 0;
            int _Sequencial = 0;
            string _NumeroMTR = "";
            string _CodigoDestino = "";
            // salvar como fechado para aparecer nos enviados e não apareceber mais aqui
            // tirar de lancamentomtr do deposito para destino enviado.
            foreach (DataGridViewRow gvr in Grade.Rows)
            {
                if (gvr.Cells["Lote"].Value != null)
                {
                    _NumeroLancamento = Convert.ToInt32(gvr.Cells["Lote"].Value.ToString().Split("-"[0])[0]);
                    _NumeroMTR = gvr.Cells["Lote"].Value.ToString().Split("-"[0])[1];
                    _CodigoResiduo = Convert.ToInt32(gvr.Cells["Lote"].Value.ToString().Split("-"[0])[2]);
                    _Sequencial = Convert.ToInt32(gvr.Cells["Lote"].Value.ToString().Split("-"[0])[4]);
                    _CodigoDestino = oDestinoFinalDados.PegaCodigo(((DataGridViewComboBoxCell)gvr.Cells["cboDestinoFinal"]).Value.ToString());

                    if (gvr.Cells["DataSaida"].Value.ToString() != "" && ((DataGridViewComboBoxCell)gvr.Cells["cboDestinoFinal"]).Value.ToString() != "" &&
                        gvr.Cells["Imprimido"].Value.ToString() == "I")
                    {
                        oDTRDados.SalvarComoFechado(_Sequencial, _NumeroLancamento, _CodigoResiduo, ((DataGridViewComboBoxCell)gvr.Cells["cboDestinoFinal"]).Value.ToString(),
                                                                                                     _CodigoDestino, _NumeroMTR);
                        clsLancamentoMTRDados oLancamentoMTRDados = new clsLancamentoMTRDados();
                        oLancamentoMTRDados.SalvarDestinoDataDescarga(_NumeroLancamento, Convert.ToInt32(_NumeroMTR), _CodigoResiduo,
                                                                      gvr.Cells["DataSaida"].Value.ToString(),
                                                                      Convert.ToInt32(_CodigoDestino), ((DataGridViewComboBoxCell)gvr.Cells["cboDestinoFinal"]).Value.ToString());
                    }
                }
            }
            Consultar();
            bindingSource.DataSource = _dt;
            Grade.DataSource = bindingSource.DataSource;
            HabilitaEnviar();
            this.Cursor = Cursors.Default;
        }

        private void Grade_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DataGridViewComboBoxCell cbc = new DataGridViewComboBoxCell();
                foreach (DataGridViewRow gvr in Grade.Rows)
                {
                    if (gvr.Cells["Imprimido"].Selected && gvr.Cells["Imprimido"].Value.ToString() == "I")
                    {
                        gvr.Cells["Imprimido"].Value = "";
                    }
                    if (gvr.Cells["DataSaida"].Selected && gvr.Cells["Imprimido"].Value.ToString() == "")
                    {
                        gvr.Cells["DataSaida"].Value = "";
                        cbc = (DataGridViewComboBoxCell)gvr.Cells["cboDestinoFinal"];
                        cbc.Value = "";
                        break;
                    }
                }
            }
            if (e.KeyCode == Keys.I)
            {
                DataGridViewComboBoxCell cbc = new DataGridViewComboBoxCell();
                foreach (DataGridViewRow gvr in Grade.Rows)
                {
                    if (gvr.Cells["Imprimido"].Selected && gvr.Cells["Imprimido"].Value.ToString() == "")
                    {
                        cbc = (DataGridViewComboBoxCell)gvr.Cells["cboDestinoFinal"];
                        if (cbc.Value.ToString() != "" && gvr.Cells["DataSaida"].Value.ToString() != "")
                        {
                            gvr.Cells["Imprimido"].Value = "I";
                        }
                    }
                }
            }
        }
    }
}
