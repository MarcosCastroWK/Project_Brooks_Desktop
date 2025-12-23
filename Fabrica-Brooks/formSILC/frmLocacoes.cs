using System;
using System.Data;
using System.Windows.Forms;
using LibSILC;
using SILCNegocios;

// teste edson
namespace formSILC
{
    public partial class frmLocacoes : Form
    {
        private BindingSource bindingSource = new BindingSource();
        private clsClientes oCliente = new clsClientes();
        private clsClienteDados oClienteDados = new clsClienteDados();
        private clsLancamentos oLancamentos = new clsLancamentos();
        private clsLancamentosDados oLancamentosDados = new clsLancamentosDados();
        private clsLancamentoMTRDados oLancamentosMTRDados = new clsLancamentoMTRDados();
        private clsLancamentoMTR oLancamentoMTR = new clsLancamentoMTR();
        private clsCaminhoesDados oCaminhaoDados = new clsCaminhoesDados();
        private clsCaminhoes oCaminhao = new clsCaminhoes();
        private clsFuncionarios oMotorista = new clsFuncionarios();
        private clsFuncionarioDados oMotoristaDados = new clsFuncionarioDados();
        private clsDestinoFinalDados oDestinoFinalDados = new clsDestinoFinalDados();
        private int NumeroLancamentoDoContainerRetirado = 0;
        private frmPermissao ofrmPermissao = new frmPermissao();
        private bool permissaoNoAlterar = false;

        public enum Botoes
        {
            Novo, Colocar, Retirar, Trocar, Alterar, Salvar, Excluir, Anular, Nulo
        }
        public Botoes eAcaoLocacaoProgramacao = Botoes.Nulo;
        public int pSequencialProgramacao = 0;
        public int pCodigoCliente = 0;
        public int pNumeroLancado = 0;
        public int pCodigoResiduo = 0;
        public int pCodigoDestinoFinal = 0;
        public decimal pQuantidade = 0;
        public int pCodigoMotorista = 0;
        public int pCodigoCaminhao = 0;
        public string pDataProgramacao = "";
        public string pObservacaoLogistica;
        public string pNumeroMTRe = "";
        private Botoes eBotoes;
        protected int pLinhaGradeMTR = -1;

        public frmLocacoes()
        {
            InitializeComponent();
        }

        private void frmLocacoes_Load(object sender, EventArgs e)
        {
            lblDB.Text = "db: " + geral.BancoUsado.ToString();
            using (clsDB _DB = new clsDB())
            {
                _DB.ConectaMySql();
                this.Text = "Locações - Servidor atual: " + _DB.NomeServidor;
                _DB.DesconectaMySql();
            }
            clsUsuarioDados oUsuarioDados = new clsUsuarioDados();
            if (geral.CodigoUsuarioAtual == 0)
                geral.CodigoUsuarioAtual = oUsuarioDados.PegaCodigoUsuario(geral.UsuarioAtual, oUsuarioDados.PegaSenha(geral.UsuarioAtual, 1), 1);

            this.Text = this.Text + " - Usuário logado: " + geral.UsuarioAtual;
            DataMostragemInicial.Text = DateTime.Now.AddDays(-31).ToString();
            CamposHabilitaOuDesabilita(false);
            PreencherCombosContaineres();

            try
            {
                if (pNumeroLancado > 0)
                {
                    intNumeroLancamento.VALOR.Text = pNumeroLancado.ToString();
                    MostraLancamento(pNumeroLancado);
                }
                else if (pCodigoCliente == 0)
                    MostraLancamento(oLancamentosDados.UltimoRegistro(false));
                else if (eAcaoLocacaoProgramacao != Botoes.Colocar)
                {
                    pNumeroLancado = oLancamentosDados.PegaNumeroLancamento(pCodigoCliente, "");
                    intNumeroLancamento.VALOR.Text = pNumeroLancado.ToString();
                    MostraLancamento(pNumeroLancado);
                }
                else if (eAcaoLocacaoProgramacao == Botoes.Colocar)
                {
                    MostraClienteLancamentos();
                }
            }
            finally
            {
                int i = 0;
                foreach (DataGridViewRow dgr in GradeLancamentos.Rows)
                {
                    if (dgr.Cells[0].Value != null)
                    {
                        i = 0;
                        break;
                    }
                    if (dgr.Cells[0].Value != null)
                        if (dgr.Cells[0].Value.ToString() == oLancamentos.NumeroLancamento.ToString())
                            break;
                    i++;
                }
                if (i > 0 && GradeLancamentos.Rows.Count < i)
                    MostraLancamentosMTR(i);
                if (pCodigoCliente > 0)
                {
                    txtProcuraCliente.Enabled = false;
                    btnOk.Enabled = false;
                    GradeClientes.Enabled = false;
                    GradeMovContainer.CellContentDoubleClick -= new System.Windows.Forms.DataGridViewCellEventHandler(GradeMovContainer_CellContentDoubleClick);
                }
            }
            if (eAcaoLocacaoProgramacao == Botoes.Colocar && funcionarioColocacao.txtCodigo.Text != "")
            {
                btnColocar_Click(new object(), EventArgs.Empty);
            }
            else if (eAcaoLocacaoProgramacao == Botoes.Retirar && funcionarioRetirada.txtCodigo.Text != "")
            {
                btnRetirar_Click(new object(), EventArgs.Empty);
            }
            else if (eAcaoLocacaoProgramacao == Botoes.Trocar && funcionarioRetirada.txtCodigo.Text != "")
            {
                btnTrocar_Click(new object(), EventArgs.Empty);
            }
            else if (eAcaoLocacaoProgramacao == Botoes.Novo && funcionarioColocacao.txtCodigo.Text != "" && funcionarioRetirada.txtCodigo.Text != "")
            {
                btnNovo_Click(new object(), EventArgs.Empty);
            }
            else if (eAcaoLocacaoProgramacao == Botoes.Alterar && funcionarioColocacao.txtCodigo.Text != "" && funcionarioRetirada.txtCodigo.Text != "")
            {
                btnAlterar_Click(new object(), EventArgs.Empty);
            }
            btnNovo.Focus();
        }

        private void MostraLancamento(int pNumeroLancamento)
        {
            oLancamentos = new clsLancamentos();
            oLancamentosDados.PegaDados(oLancamentos, pNumeroLancamento);
            if (oLancamentos.NumeroLancamento > 0)
            {                
                intNumeroLancamento.VALOR.Text = oLancamentos.NumeroLancamento.ToString("0000000");
                DataLancamento.Text = oLancamentos.Data;
                cboContainerColocacao.Text = oLancamentos.NumeroCaixa;
                cliente1.txtCodigo.Text = oLancamentos.CodigoCliente.ToString();
                cliente1.txtDescricao.Text = (oClienteDados.PegaDados(oCliente, Convert.ToInt32(cliente1.txtCodigo.Text))).NomeFantasia;
                caminhaoColocacao.txtCodigo.Text = oLancamentos.CodigoCaminhaoColoca.ToString();
                caminhaoColocacao.txtDescricao.Text = (oCaminhaoDados.PegaDados(oCaminhao, Convert.ToInt32(caminhaoColocacao.txtCodigo.Text))).Modelo;
                funcionarioColocacao.txtCodigo.Text = oLancamentos.CodigoMotoristaColocou.ToString();
                funcionarioColocacao.txtDescricao.Text = (oMotoristaDados.PegaDados(oMotorista, Convert.ToInt32(funcionarioColocacao.txtCodigo.Text))).Nome;

                DataColocacao.Text = oLancamentos.DataColocacao;

                if (DataMostragemInicial.Value > DataColocacao.Value)
                    DataMostragemInicial.Value = DataColocacao.Value;

                if (oLancamentos.DataRetirada != "" && oLancamentos.DataRetirada != null && oLancamentos.DataRetirada != "01/01/0001" && 
                    oLancamentos.DataRetirada != "01/01/0100" && oLancamentos.DataRetirada != "01/01/1900")
                    DataRetirada.Text = oLancamentos.DataRetirada;
                else
                    DataRetirada.Text = "01/01/1900";
                caminhaoRetirada.txtCodigo.Text = oLancamentos.CodigoCaminhoRetirada.ToString();
                cboContainerRetirada.Text = oLancamentos.NumeroCaixa;
                funcionarioRetirada.txtCodigo.Text = oLancamentos.CodigoCaminhoRetirada.ToString();

                bindingSource.DataSource = oClienteDados.PreencheSoNomeFantasia(cliente1.txtDescricao.Text);
                GradeClientes.DataSource = bindingSource.DataSource;
                EstiloGradeClientes();

                DataTable dtColocacoes = new DataTable();
                DataTable dtRetiradas = new DataTable();
                geral.CodigoCliente = Convert.ToInt32(cliente1.txtCodigo.Text);

                dtColocacoes = oLancamentosDados.PreencheDadosLocacao(DataMostragemInicial.Text, DataMostragemFinal.Text, geral.CodigoCliente, "DataColocacao asc", 
                                                                      false, false, true, true, true, true, true, true, false, false, false, false, true, true, false,
                                                                      oLancamentos.NumeroLancamento);
                dtRetiradas = oLancamentosDados.PreencheDadosLocacao(DataMostragemInicial.Text, DataMostragemFinal.Text, geral.CodigoCliente, "DataRetirada  desc", false, 
                                                                     false, true, true, true, true, true, true, false, false, false, false, true, false, true,
                                                                     oLancamentos.NumeroLancamento);
                dtColocacoes.Merge(dtRetiradas);
                bindingSource.DataSource = dtColocacoes;
                GradeLancamentos.DataSource = bindingSource.DataSource;
                EstiloGradeLancamentos();
            }
        }

        private void MostraClienteLancamentos()
        {
            bindingSource.DataSource = oClienteDados.PreencheSoNomeFantasia(cliente1.txtDescricao.Text);
            GradeClientes.DataSource = bindingSource.DataSource;
            EstiloGradeClientes();

            DataTable dtColocacoes = new DataTable();
            DataTable dtRetiradas = new DataTable();
            geral.CodigoCliente = Convert.ToInt32(cliente1.txtCodigo.Text);

            dtColocacoes = oLancamentosDados.PreencheDadosLocacao(DataMostragemInicial.Text, DataMostragemFinal.Text, geral.CodigoCliente, "DataColocacao asc",
                                                                  false, false, true, true, true, true, true, true, false, false, false, false, true, true, false,
                                                                  oLancamentos.NumeroLancamento);
            dtRetiradas = oLancamentosDados.PreencheDadosLocacao(DataMostragemInicial.Text, DataMostragemFinal.Text, geral.CodigoCliente, "DataRetirada  desc", false,
                                                                 false, true, true, true, true, true, true, false, false, false, false, true, false, true,
                                                                 oLancamentos.NumeroLancamento);
            dtColocacoes.Merge(dtRetiradas);
            bindingSource.DataSource = dtColocacoes;
            GradeLancamentos.DataSource = bindingSource.DataSource;
            EstiloGradeLancamentos();
        }

        private void PreencherCombosContaineres()
        {
            clsCacambaDados oContaineresDados = new clsCacambaDados();
            string _PrimeiroContainer = "";
            foreach (DataRow _dr in oContaineresDados.PreencheDataTableCacambas("Numero").Rows)
            {
                if (_PrimeiroContainer.Length == 0)
                    _PrimeiroContainer = _dr["Numero"].ToString();
                cboContainerColocacao.Items.Add(_dr["Numero"]);
                cboMovContainer.Items.Add(_dr["Numero"]);
                cboContainerRetirada.Items.Add(_dr["Numero"]);
            }
            cboMovContainer.Text = _PrimeiroContainer;
            cboContainerColocacao.Text = _PrimeiroContainer;
        }

        private void EstiloGradeLancamentos()
        {
            GradeLancamentos.Columns["NumeroLancamento"].HeaderText = "Nº Lançamento";
            GradeLancamentos.Columns["NumeroLancamento"].Width = 80;
            GradeLancamentos.Columns["NumeroLancamento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            GradeLancamentos.Columns["NumeroLancamento"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            GradeLancamentos.Columns["NomeFantasia"].HeaderText = "Nome fantasia";
            GradeLancamentos.Columns["NomeFantasia"].Width = 200;

            GradeLancamentos.Columns["CodigoCliente"].HeaderText = "Código";
            GradeLancamentos.Columns["CodigoCliente"].Width = 60;
            GradeLancamentos.Columns["CodigoCliente"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            GradeLancamentos.Columns["Container"].Width = 60;
           
            GradeLancamentos.Columns["DataColocacao"].HeaderText = "Data Colocação";
            GradeLancamentos.Columns["DataColocacao"].Width = 90;
            GradeLancamentos.Columns["DataColocacao"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GradeLancamentos.Columns["DataColocacao"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            GradeLancamentos.Columns["DataRetirada"].HeaderText = "Data Retirada";
            GradeLancamentos.Columns["DataRetirada"].Width = 90;
            GradeLancamentos.Columns["DataRetirada"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GradeLancamentos.Columns["DataRetirada"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // em várias lançamentos está mostrando ou gravou data errada - vou deixar invisível, ou width 1, assim não vai aparecer
            GradeLancamentos.Columns["Data"].HeaderText = "Data Lançamento";
            GradeLancamentos.Columns["Data"].Width = 1;
            GradeLancamentos.Columns["Data"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GradeLancamentos.Columns["Data"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // fim data lançamento

            GradeLancamentos.Columns["NuLancColocacao"].HeaderText = "NºTroca F7-Confirma";
            GradeLancamentos.Columns["NuLancColocacao"].Width = 70;
            GradeLancamentos.Columns["NuLancColocacao"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            GradeLancamentos.Columns["NuLancColocacao"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            GradeLancamentos.Columns["NumeroMTR"].HeaderText = "Nº MTR";
            GradeLancamentos.Columns["NumeroMTR"].Width = 70;
            GradeLancamentos.Columns["NumeroMTR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            GradeLancamentos.Columns["NumeroMTR"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            GradeLancamentos.Columns["Quantidade"].Width = 70;
            GradeLancamentos.Columns["Quantidade"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            GradeLancamentos.Columns["Quantidade"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            GradeLancamentos.Columns["Residuo"].Width = 200;
            GradeLancamentos.Columns["obsLogistica"].HeaderText = "Obs Logística";

            //GradeLancamentos.Columns["Caminhao"].HeaderText = "Caminhão";
            GradeLancamentos.Columns["Caminhao"].Visible = false;
            
            GradeLancamentos.Columns["CodigoCaminhaoColoca"].Visible = false;
            GradeLancamentos.Columns["Motorista"].Visible = false;

            GradeLancamentos.Columns["CaminhaoRetirada"].HeaderText = "Caminhão";
            GradeLancamentos.Columns["MotoristaRetirada"].HeaderText = "Motorista";
            GradeLancamentos.Columns["CodigoCaminhoRetirada"].Visible = false;
            GradeLancamentos.Columns["CodigoMotoristaColocou"].Visible = false;
            GradeLancamentos.Columns["CodigoMotoristaRetirou"].Visible = false;
        }
        
        private void CamposHabilitaOuDesabilita(bool pAtivo)
        {
            cboContainerColocacao.Enabled = pAtivo;
            cliente1.txtCodigo.Enabled = pAtivo;
            caminhaoColocacao.txtCodigo.Enabled = pAtivo;
            caminhaoColocacao.txtDescricao.Enabled = pAtivo;
            funcionarioColocacao.txtCodigo.Enabled = pAtivo;
            funcionarioColocacao.txtDescricao.Enabled = pAtivo;
            DataColocacao.Enabled = pAtivo;
            DataRetirada.Enabled = pAtivo;
            caminhaoRetirada.txtCodigo.Enabled = pAtivo;
            caminhaoRetirada.txtDescricao.Enabled = pAtivo;
            cboContainerRetirada.Enabled = pAtivo;
            funcionarioRetirada.txtCodigo.Enabled = pAtivo;
            funcionarioRetirada.txtDescricao.Enabled = pAtivo;
            txtInformacoesLogistica.Enabled = pAtivo;

            GradeClientes.Enabled = !pAtivo;

            btnSalvar.Enabled = pAtivo;
            btnAnular.Enabled = pAtivo;
            btnExcluir.Enabled = !pAtivo;
            btnAlterar.Enabled = !pAtivo;
            btnColocar.Enabled = !pAtivo;
            btnNovo.Enabled = !pAtivo;
            btnRetirar.Enabled = !pAtivo;
            btnTrocar.Enabled = !pAtivo;
        }

        private void GradeLancamentosMTR_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                int _currentrowindex = GradeLancamentosMTR.CurrentRow.Index;
                string _horadescarga = "";
                if (e.KeyCode == Keys.Enter)
                    _currentrowindex = GradeLancamentosMTR.CurrentRow.Index - 1;
                if (GradeLancamentosMTR.Rows.Count > 0)
                    _horadescarga = geral.VerificaHora(GradeLancamentosMTR.Rows[_currentrowindex].Cells["HoraDescarga"].Value.ToString());
                if (e.KeyCode == Keys.Insert)
                {
                    InsertLocacaoMTR();
                    RefreshGradeLancamento_MTR();
                }
                else if (e.KeyCode == Keys.Tab)
                {
                    if (_horadescarga == "Hora inválida!")
                    {
                        GradeLancamentosMTR.Rows[_currentrowindex].Cells["HoraDescarga"].Value = "";
                        MessageBox.Show("Hora descarga inválida!");
                    }
                    else
                        GradeLancamentosMTR.Rows[_currentrowindex].Cells["HoraDescarga"].Value = _horadescarga;
                }
                if (GradeLancamentosMTR.CurrentRow != null)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        // chave primaria
                        // NumeroLancamento
                        // NumeroMTR
                        // CodigoResiduo
                        if (GradeLancamentosMTR.Rows.Count > 0 && GradeLancamentosMTR.CurrentRow.Index > 0 && intNumeroLancamento.VALOR.Text != "")
                        {

                            clsResiduoDados oResiduoDados = new clsResiduoDados();
                            oDestinoFinalDados = new clsDestinoFinalDados();
                            bool bEditar = true;        
                            if (GradeLancamentosMTR.Rows[_currentrowindex].Cells["CodigoResiduo"].Value.ToString() != "")
                            {
                                if (!oResiduoDados.CodigoExiste(Convert.ToInt32(GradeLancamentosMTR.Rows[_currentrowindex].Cells["CodigoResiduo"].Value.ToString())))
                                {
                                    bEditar = false;
                                    MessageBox.Show("Código do resíduo inválido!");
                                }
                                else if (GradeLancamentosMTR.Rows[_currentrowindex].Cells["Deposito"].Value.ToString().Length == 0)
                                {
                                    // sem nada digitado - ok
                                    bEditar = true;
                                }
                                else if (oDestinoFinalDados.PegaNomeFantasia(GradeLancamentosMTR.Rows[_currentrowindex].Cells["Deposito"].Value.ToString()) == "" &&
                                         GradeLancamentosMTR.Rows[_currentrowindex].Cells["Deposito"].Value.ToString().Substring(0, 3) != "DTR")
                                {
                                    bEditar = false;
                                    MessageBox.Show("Código do destino final inválido!");
                                }
                                else if (_horadescarga == "Hora inválida!")
                                {
                                    bEditar = false;
                                    GradeLancamentosMTR.Rows[_currentrowindex].Cells["HoraDescarga"].Value = _horadescarga;
                                    MessageBox.Show("Hora descarga inválida!");
                                }
                                if (bEditar)
                                {
                                    GradeLancamentosMTR.Rows[_currentrowindex].Cells["HoraDescarga"].Value = _horadescarga;
                                    using (frmLocacaoMTR o_frmLocacaoMTR = new frmLocacaoMTR())
                                    {
                                        o_frmLocacaoMTR.lblNrLancamento.Text = Convert.ToInt32(intNumeroLancamento.VALOR.Text).ToString("0000000");
                                        o_frmLocacaoMTR.intNumeroMTR.VALOR.Text = GradeLancamentosMTR.Rows[_currentrowindex].Cells["NumeroMTR"].Value.ToString();
                                        o_frmLocacaoMTR.residuo1.txtCodigo.Text = GradeLancamentosMTR.Rows[_currentrowindex].Cells["CodigoResiduo"].Value.ToString();
                                        o_frmLocacaoMTR.moeQuantidadeColetada.VALOR.Text = Convert.ToDecimal(GradeLancamentosMTR.Rows[_currentrowindex].Cells["QtColetada"].Value).ToString();
                                        o_frmLocacaoMTR.moeQuantidadeDescarregada.VALOR.Text = Convert.ToDecimal(GradeLancamentosMTR.Rows[_currentrowindex].Cells["QtDescarga"].Value).ToString();
                                        o_frmLocacaoMTR.moeValorTotal.VALOR.Text = Convert.ToDecimal(GradeLancamentosMTR.Rows[_currentrowindex].Cells["ValorTotal"].Value).ToString();
                                        o_frmLocacaoMTR.moeValorUnitario.VALOR.Text = Convert.ToDecimal(GradeLancamentosMTR.Rows[_currentrowindex].Cells["ValorUnitario"].Value).ToString();
                                        if (GradeLancamentosMTR.Rows[_currentrowindex].Cells["DataDescarga"].Value.ToString() != "01/01/1900" &&
                                            GradeLancamentosMTR.Rows[_currentrowindex].Cells["DataDescarga"].Value.ToString() != "01/01/0100 00:00:00")
                                            o_frmLocacaoMTR.dtpDataDescarga.Text = GradeLancamentosMTR.Rows[_currentrowindex].Cells["DataDescarga"].Value.ToString();
                                        else
                                            o_frmLocacaoMTR.dtpDataDescarga.Text = "01/01/1900";
                                        o_frmLocacaoMTR.txtHoraDescarga.Text = GradeLancamentosMTR.Rows[_currentrowindex].Cells["HoraDescarga"].Value.ToString();
                                        o_frmLocacaoMTR.txtMotivoMTRe.Text = GradeLancamentosMTR.Rows[_currentrowindex].Cells["Motivo"].Value.ToString();
                                        o_frmLocacaoMTR.txtDescargaMTRe.Text = GradeLancamentosMTR.Rows[_currentrowindex].Cells["DescargaMTRe"].Value.ToString();
                                        o_frmLocacaoMTR.txtControleInternoDescarga.Text = GradeLancamentosMTR.Rows[_currentrowindex].Cells["ControleInternoDescarga"].Value.ToString();
                                        o_frmLocacaoMTR.txtObservacao.Text = GradeLancamentosMTR.Rows[_currentrowindex].Cells["Observacao"].Value.ToString();
                                        o_frmLocacaoMTR.txtTicket.Text = GradeLancamentosMTR.Rows[_currentrowindex].Cells["Ticket"].Value.ToString();
                                        o_frmLocacaoMTR.txtUnidade.Text = GradeLancamentosMTR.Rows[_currentrowindex].Cells["Unidade"].Value.ToString();

                                        if (pNumeroMTRe == "" && GradeLancamentosMTR.Rows[_currentrowindex].Cells["NumeroMTRFatima"].Value.ToString() != "")
                                            o_frmLocacaoMTR.intNumeroMTRe.Text = GradeLancamentosMTR.Rows[_currentrowindex].Cells["NumeroMTRFatima"].Value.ToString();
                                        else
                                            o_frmLocacaoMTR.intNumeroMTRe.Text = pNumeroMTRe; // esse vem da Locações a partir da Programação

                                        if (geral.Left(GradeLancamentosMTR.Rows[_currentrowindex].Cells["Deposito"].Value.ToString(), 3) != "DTR")
                                            o_frmLocacaoMTR.cboDestino.Text = oDestinoFinalDados.PegaNomeFantasia(GradeLancamentosMTR.Rows[_currentrowindex].Cells["Deposito"].Value.ToString());
                                        else
                                            o_frmLocacaoMTR.cboDestino.Text = GradeLancamentosMTR.Rows[_currentrowindex].Cells["Deposito"].Value.ToString();
                                        o_frmLocacaoMTR.oLancamento = oLancamentos;
                                        if (cliente1.txtCodigo.Text != "")
                                            o_frmLocacaoMTR.pCodigoCliente = Convert.ToInt32(cliente1.txtCodigo.Text);
                                        o_frmLocacaoMTR.intNumeroMTRe.Text = pNumeroMTRe;
                                        o_frmLocacaoMTR.ShowDialog();
                                        o_frmLocacaoMTR.Close();
                                        o_frmLocacaoMTR.Dispose();
                                    }
                                }
                            }
                            RefreshGradeLancamento_MTR();
                        }
                    }
                    else if (e.KeyCode == Keys.Delete)
                    {
                        // chave primaria
                        // NumeroLancamento
                        // NumeroMTR
                        // CodigoResiduo
                        _currentrowindex = GradeLancamentosMTR.CurrentRow.Index;
                        if (GradeLancamentosMTR.Rows.Count > 0 && GradeLancamentosMTR.CurrentRow.Index >= 0 && intNumeroLancamento.VALOR.Text != "" &&
                            GradeLancamentosMTR.Rows[_currentrowindex].Cells["NumeroMTR"].Value.ToString() != "" &&
                            GradeLancamentosMTR.Rows[_currentrowindex].Cells["CodigoResiduo"].Value.ToString() != "")
                        {
                            DialogResult Sim = MessageBox.Show("Confirma exclusão?", "Confirmação", MessageBoxButtons.YesNo);
                            if (Sim == System.Windows.Forms.DialogResult.Yes)
                            {
                                if (GradeLancamentosMTR.Rows[0].Cells[0].Value != null)
                                {
                                    string sLog = "";
                                    foreach (DataGridViewRow gvr in GradeLancamentosMTR.Rows)
                                    {
                                        if (gvr.Cells["NumeroMTR"].Value != null)
                                        {
                                            if (GradeLancamentosMTR.Rows[_currentrowindex].Cells["CodigoResiduo"].Value.ToString() == gvr.Cells["CodigoResiduo"].Value.ToString())
                                            {
                                                sLog = sLog + "Nº Lançamento: " + intNumeroLancamento.VALOR.Text + " \n";
                                                sLog = sLog + "Nº MTR: " + gvr.Cells["NumeroMTR"].Value.ToString() + " \n";
                                                sLog = sLog + "Código resíduo: " + gvr.Cells["CodigoResiduo"].Value.ToString() + " \n";
                                                sLog = sLog + "Quantidade :" + gvr.Cells["QtDescarga"].Value.ToString() + " \n";
                                                sLog = sLog + "Valor unitário: " + gvr.Cells["ValorUnitario"].Value.ToString() + " \n";
                                                sLog = sLog + "Valor total: " + gvr.Cells["ValorTotal"].Value.ToString() + " \n";
                                                sLog = sLog + "Destino final: " + gvr.Cells["Deposito"].Value.ToString() + " \n";
                                                sLog = sLog + "Unidade: " + gvr.Cells["Unidade"].Value.ToString() + " \n";
                                                sLog = sLog + "Ticket:" + gvr.Cells["Ticket"].Value.ToString() + " \n";
                                                sLog = sLog + "Data descarga: " + gvr.Cells["DataDescarga"].Value.ToString() + " \n";
                                                sLog = sLog + "Nº MTR-e: " + gvr.Cells["NumeroMTRFatima"].Value.ToString() + " \n";
                                                sLog = sLog + "Observação: " + gvr.Cells["Observacao"].Value.ToString() + " \n";
                                            }
                                        }
                                    }
                                    sLog = sLog + " \n";
                                    SalvarLog("Exclusão", sLog, "Exclusão-Lançamento-Resíduo");
                                }

                                oLancamentosMTRDados.Excluir(Convert.ToInt32(intNumeroLancamento.VALOR.Text),
                                                             Convert.ToInt32(GradeLancamentosMTR.Rows[_currentrowindex].Cells["NumeroMTR"].Value),
                                                             Convert.ToInt32(GradeLancamentosMTR.Rows[_currentrowindex].Cells["CodigoResiduo"].Value));
                                RefreshGradeLancamento_MTR();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RefreshGradeLancamento_MTR();
                //MessageBox.Show(ex.Message, "Digitação inválida!");
            }
        }

        private void btnMovContainer_Click(object sender, EventArgs e)
        {
            if (cboMovContainer.Text.Length > 0)
            {
                bindingSource.DataSource = oLancamentosDados.PreencheDadosMovimentacaoContainer(cboMovContainer.Text, DataMostragemInicial.Text);
                GradeMovContainer.DataSource = bindingSource.DataSource;
                GradeMovContainer.Columns["NomeFantasia"].Width = 100;
                GradeMovContainer.Columns["NomeFantasia"].HeaderText = "Nome fantasia";

                GradeMovContainer.Columns["DataColocacao"].Width = 86;
                GradeMovContainer.Columns["DataColocacao"].HeaderText = "Data Colocação";
                GradeMovContainer.Columns["DataColocacao"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GradeMovContainer.Columns["DataColocacao"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                GradeMovContainer.Columns["DataRetirada"].Width = 86;
                GradeMovContainer.Columns["DataRetirada"].HeaderText = "Data Retirada";
                GradeMovContainer.Columns["DataRetirada"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GradeMovContainer.Columns["DataRetirada"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                GradeMovContainer.Columns["NomeMotorista"].Width = 86;
                GradeMovContainer.Columns["NomeMotorista"].HeaderText = "Motorista Retirou";

                GradeMovContainer.Columns["Modelo"].Width = 60;
                GradeMovContainer.Columns["Modelo"].HeaderText = "Caminhão Retirou";

                GradeMovContainer.Columns["NumeroLancamento"].Width = 60;
                GradeMovContainer.Columns["NumeroLancamento"].HeaderText = "NºLanç";
                GradeMovContainer.Columns["NumeroLancamento"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                GradeMovContainer.Columns["NumeroLancamento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                int _width = this.Width;
                if (_width <= 1329)
                    _width = 1400;
                if ((_width - grbColocacao.Width - GradeClientes.Width - 90) > 450)
                    panel2.Width = _width - grbColocacao.Width - GradeClientes.Width - 90;
                else
                    panel2.Width = 500;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            bindingSource.DataSource = oClienteDados.PreencheSoNomeFantasia(txtProcuraCliente.Text);
            GradeClientes.DataSource = bindingSource.DataSource;
        }

        private void EstiloGradeClientes()
        {
            GradeClientes.Columns["Codigo"].Visible = false;
            GradeClientes.Columns["NomeFantasia"].HeaderText = "Nome fantasia";
            GradeClientes.Columns["NomeFantasia"].Width = 232;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            eBotoes = Botoes.Novo;
            CamposHabilitaOuDesabilita(true);
            grbRetirada.Enabled = true;
            if (eAcaoLocacaoProgramacao == Botoes.Nulo)
                LimpaCampos();
            else
                cboContainerColocacao.Text = "";
            geral.CodigoCliente = 0;
            bindingSource.DataSource = new DataTable();
            GradeLancamentosMTR.DataSource = bindingSource.DataSource;
            if (eAcaoLocacaoProgramacao == Botoes.Nulo && pDataProgramacao == "")
            { 
                DataColocacao.Text = DateTime.Now.AddDays(-1).ToString();
                DataRetirada.Text = DateTime.Now.AddDays(-1).ToString();
            }
            else if (pDataProgramacao != "")
            {
                DataColocacao.Text = pDataProgramacao;
                DataRetirada.Text = pDataProgramacao;
            }
            if (pCodigoCliente > 0)
            {
                cliente1.txtCodigo.Text = pCodigoCliente.ToString();
                cliente1.txtDescricao.Text = oClienteDados.PegaNomeFantasia(pCodigoCliente);
            }
            if (pCodigoCaminhao > 0)
            {
                caminhaoColocacao.txtCodigo.Text = pCodigoCaminhao.ToString();
                caminhaoColocacao.txtDescricao.Text = oCaminhaoDados.PegaDados(oCaminhao, pCodigoCaminhao).Modelo;
                caminhaoRetirada.txtCodigo.Text = pCodigoCaminhao.ToString();
                caminhaoRetirada.txtDescricao.Text = oCaminhaoDados.PegaDados(oCaminhao, pCodigoCaminhao).Modelo;
            }
            if (pCodigoMotorista > 0)
            {
                funcionarioColocacao.txtCodigo.Text = pCodigoMotorista.ToString();
                funcionarioColocacao.txtDescricao.Text = oMotoristaDados.PegaDados(oMotorista, pCodigoMotorista).Nome;
                funcionarioRetirada.txtCodigo.Text = pCodigoMotorista.ToString();
                funcionarioRetirada.txtDescricao.Text = oMotoristaDados.PegaDados(oMotorista, pCodigoMotorista).Nome;
            }
            cboContainerColocacao.Focus();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            permissaoNoAlterar = false;
            ofrmPermissao = new frmPermissao();
            ofrmPermissao.bPermitido = true;
            ofrmPermissao.Text = "Data da Retirada inválida! Digite a senha do Supervisor:";
            geral.VoltaForm = this.Name;
            if (!AlteracaoValida(DataRetirada.Value) && DataRetirada.Text != "01/01/1900")
            {
                ofrmPermissao.bPermitido = false;
                ofrmPermissao.ShowDialog();
                if (ofrmPermissao.bPermitido) 
                    permissaoNoAlterar = true;
            }
            if (ofrmPermissao.bPermitido)
            {
                if (intNumeroLancamento.VALOR.Text == "" || intNumeroLancamento.VALOR.Text == "0")
                    MessageBox.Show("Lançamento não selecionado ou inválido!");
                else
                {
                    CamposHabilitaOuDesabilita(true);
                    GradeLancamentosMTR.Enabled = false;
                    eBotoes = Botoes.Alterar;
                    grbRetirada.Enabled = true;
                }
            }
        }

        private void SalvarLog(string pOperacao, string pLog, string pLocalOperacao = "")
        {
            clsLog oLog = new clsLog();
            clsLogDados oLogDados = new clsLogDados();
            oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
            oLog.CodigoUsuario = geral.CodigoUsuarioAtual;
            oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
            if (pLocalOperacao == "")
                oLog.LocalOperacao = "Lançamento de Locações";
            else
                oLog.LocalOperacao = pLocalOperacao;
            oLog.Operacao = pOperacao;
            oLog.Log = pLog;
            oLogDados.Inserir(oLog);
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (cliente1.txtCodigo.Text == "")
            {
                MessageBox.Show("Código do cliente inválido!");
                cliente1.txtCodigo.Text = "";
                cboContainerColocacao.Text = "";
                cboContainerRetirada.Text = "";
                CamposHabilitaOuDesabilita(false);
                btnNovo.Focus();
            }
            else if (DataRetirada.Value < DataColocacao.Value && DataRetirada.Value.Year > 1900 && eBotoes != Botoes.Excluir && eBotoes != Botoes.Colocar)
            {
                MessageBox.Show("Data da Retirada inválida, não pode ser menor que a Data da Colocação!");
            }
            else
            {
                if (Botoes.Novo == eBotoes || (permissaoNoAlterar == false && Botoes.Alterar == eBotoes) || Botoes.Colocar == eBotoes || Botoes.Retirar == eBotoes)
                {
                    ofrmPermissao = new frmPermissao();
                    ofrmPermissao.bPermitido = true;
                    geral.VoltaForm = this.Name;
                    if (Botoes.Colocar != eBotoes)
                    {
                        ofrmPermissao.Text = "Data da Retirada inválida! Digite a senha do Supervisor:";
                        geral.VoltaForm = this.Name;
                        if (!AlteracaoValida(DataRetirada.Value) && DataRetirada.Text != "01/01/1900")
                        {
                            ofrmPermissao.bPermitido = false;
                            ofrmPermissao.ShowDialog();
                        }
                    }
                }
                if (ofrmPermissao.bPermitido)
                {
                    if (eBotoes != Botoes.Trocar)
                    {
                        ofrmPermissao = new frmPermissao();
                        ofrmPermissao.bPermitido = false;
                    }
                    CamposHabilitaOuDesabilita(false);
                    btnNovo.Focus();

                    if (intNumeroLancamento.VALOR.Text != "")
                        oLancamentos.NumeroLancamento = Convert.ToInt32(intNumeroLancamento.VALOR.Text);

                    oLancamentos.NumeroCaixa = cboContainerColocacao.Text;
                    oLancamentos.CodigoCliente = Convert.ToInt32(cliente1.txtCodigo.Text);
                    oLancamentos.CodigoCaminhaoColoca = Convert.ToInt32(caminhaoColocacao.txtCodigo.Text);
                    oLancamentos.CodigoMotoristaColocou = Convert.ToInt32(funcionarioColocacao.txtCodigo.Text);
                    oLancamentos.Data = DataLancamento.Text;
                    oLancamentos.DataColocacao = DataColocacao.Text;
                    if (DataRetirada.Value.Year == 1900)
                        oLancamentos.DataRetirada = "01/01/0001";
                    else
                        oLancamentos.DataRetirada = DataRetirada.Text;
                    if (funcionarioRetirada.txtCodigo.Text != "")
                        oLancamentos.CodigoMotoristaRetirou = Convert.ToInt32(funcionarioRetirada.txtCodigo.Text);
                    if (caminhaoRetirada.txtCodigo.Text != "")
                        oLancamentos.CodigoCaminhoRetirada = Convert.ToInt32(caminhaoRetirada.txtCodigo.Text);
                    oLancamentos.HoraRetirada = DateTime.Now.ToString("hh:mm");
                    oLancamentos.Horas = 0;
                    if (eBotoes == Botoes.Novo)
                    {
                        oLancamentos.HorasColocacao = DateTime.Now.ToString("hh:mm");
                        oLancamentos.TipoOperacao = 0;
                        oLancamentos.Horas = Convert.ToInt32(DateTime.Now.Ticks.ToString().Substring(2, 8));
                        intNumeroLancamento.VALOR.Text = "";
                    }
                    else if (eBotoes == Botoes.Colocar)
                    {
                        oLancamentos.TipoOperacao = 1;
                        oLancamentos.Horas = Convert.ToInt32(DateTime.Now.Ticks.ToString().Substring(2, 8));
                    }
                    else if (eBotoes == Botoes.Retirar)
                    {
                        oLancamentos.TipoOperacao = 2;
                        oLancamentos.Horas = Convert.ToInt32(DateTime.Now.Ticks.ToString().Substring(2, 8));
                    }
                    else if (eBotoes == Botoes.Trocar)
                    {
                        oLancamentos.TipoOperacao = 3;
                        oLancamentos.Horas = Convert.ToInt32(DateTime.Now.Ticks.ToString().Substring(2, 8));
                    }
                    if (oLancamentos.NumeroCaixa == "")
                    {
                        MessageBox.Show("Container inválido!");
                    }
                    else
                    {
                        // quando sair da operação troca (3) para colocação (1), justamente quando não têm data retirada
                        if (eBotoes == Botoes.Alterar && oLancamentos.TipoOperacao == 3)
                        {
                            if (oLancamentos.DataRetirada == "" || oLancamentos.DataRetirada == "01/01/0100" ||
                                oLancamentos.DataRetirada == "01/01/1900" || oLancamentos.DataRetirada == "01/01/0001")
                                oLancamentos.TipoOperacao = 1;
                        }
                        string sLog = "";
                        sLog = sLog + "Nº Lançamento: " + oLancamentos.NumeroLancamento + " \n";
                        sLog = sLog + "Nº Caixa: " + oLancamentos.NumeroCaixa + " \n";
                        sLog = sLog + "Código Cliente: " + oLancamentos.CodigoCliente + " \n";
                        sLog = sLog + "Código Caminhão Colocou: " + oLancamentos.CodigoCaminhaoColoca + " \n";
                        sLog = sLog + "Código Motorista Colocou: " + oLancamentos.CodigoMotoristaColocou + " \n";
                        sLog = sLog + "Data lançamento: " + oLancamentos.Data + " \n";
                        sLog = sLog + "Data Colocação: " + oLancamentos.DataColocacao + " \n";
                        sLog = sLog + "Data Retirada: " + oLancamentos.DataRetirada + " \n";
                        sLog = sLog + "Código Motorista Retirou: " + oLancamentos.CodigoMotoristaRetirou + " \n";
                        sLog = sLog + "Código Caminhão Retirada: " + oLancamentos.CodigoCaminhoRetirada + " \n";
                        sLog = sLog + "Hora Retirada: " + oLancamentos.HoraRetirada + " \n";
                        sLog = sLog + "Horas Colocacão: " + oLancamentos.HorasColocacao + " \n";
                        sLog = sLog + "Tipo operação: " + oLancamentos.TipoOperacao + " \n";
                        sLog = sLog + "Hora lançamento: " + oLancamentos.Horas + " \n";

                        if (intNumeroLancamento.VALOR.Text != "" && (eBotoes == Botoes.Alterar || eBotoes == Botoes.Retirar || eBotoes == Botoes.Trocar))
                        {
                            if (eBotoes == Botoes.Alterar)
                                SalvarLog("Alteração", sLog);
                            oLancamentosDados.Alterar(oLancamentos, Convert.ToInt32(intNumeroLancamento.VALOR.Text));
                            // Salvar em LancamentoProgrogramacao
                            if (eAcaoLocacaoProgramacao != Botoes.Nulo && pSequencialProgramacao > 0)
                            {
                                oLancamentosDados.InserirLancamentoProgramacao(Convert.ToInt32(intNumeroLancamento.VALOR.Text),
                                                                               Convert.ToInt32(cliente1.txtCodigo.Text),
                                                                               DataRetirada.Text, pSequencialProgramacao);
                            }
                            if (eBotoes == Botoes.Retirar || eBotoes == Botoes.Trocar)
                            {
                                InsertLocacaoMTR();
                                if (eBotoes == Botoes.Trocar)
                                {
                                    SalvarLog("Troca", sLog);
                                    CamposHabilitaOuDesabilita(true);
                                    grbRetirada.Enabled = false;
                                    if (eAcaoLocacaoProgramacao == Botoes.Nulo)
                                    {
                                        LimpaCampos();
                                        if (pCodigoCliente > 0)
                                        {
                                            cliente1.txtCodigo.Text = pCodigoCliente.ToString();
                                            cliente1.txtDescricao.Text = oClienteDados.PegaNomeFantasia(pCodigoCliente);
                                        }
                                        if (pCodigoCaminhao > 0)
                                        {
                                            caminhaoColocacao.txtCodigo.Text = pCodigoCaminhao.ToString();
                                            if (caminhaoColocacao.txtCodigo.Text != "")
                                                caminhaoColocacao.txtDescricao.Text = oCaminhaoDados.PegaDados(oCaminhao, Convert.ToInt32(caminhaoColocacao.txtCodigo.Text)).Modelo;
                                        }
                                        if (pCodigoMotorista > 0)
                                        {
                                            funcionarioColocacao.txtCodigo.Text = pCodigoMotorista.ToString();
                                            if (funcionarioColocacao.txtCodigo.Text != "")
                                                funcionarioColocacao.txtDescricao.Text = oMotoristaDados.PegaDados(oMotorista, Convert.ToInt32(funcionarioColocacao.txtCodigo.Text)).Nome;
                                        }
                                    }
                                    else
                                    {
                                        caminhaoColocacao.txtCodigo.Text = caminhaoRetirada.txtCodigo.Text;
                                        if (caminhaoColocacao.txtCodigo.Text != "")
                                            caminhaoColocacao.txtDescricao.Text = oCaminhaoDados.PegaDados(oCaminhao, Convert.ToInt32(caminhaoColocacao.txtCodigo.Text)).Modelo;
                                        funcionarioColocacao.txtCodigo.Text = funcionarioRetirada.txtCodigo.Text;
                                        if (funcionarioColocacao.txtCodigo.Text != "")
                                            funcionarioColocacao.txtDescricao.Text = oMotoristaDados.PegaDados(oMotorista, Convert.ToInt32(funcionarioColocacao.txtCodigo.Text)).Nome;

                                        DataColocacao.Text = DataRetirada.Text;
                                        intNumeroLancamento.VALOR.Text = "";
                                        DataLancamento.Text = "";
                                        DataRetirada.Text = "1900-01-01";
                                        caminhaoRetirada.txtCodigo.Text = "";
                                        caminhaoRetirada.txtDescricao.Text = "";
                                        cboContainerRetirada.Text = "";
                                        funcionarioRetirada.txtCodigo.Text = "";
                                        funcionarioRetirada.txtDescricao.Text = "";
                                        txtInformacoesLogistica.Text = "";
                                        txtProcuraCliente.Text = "";
                                    }
                                    grbColocacao.Enabled = true;
                                    cboContainerColocacao.Text = "";
                                    cboContainerColocacao.Focus();
                                }
                                else if (eBotoes == Botoes.Retirar)
                                    SalvarLog("Retirada", sLog);
                            }
                        }
                        else if (intNumeroLancamento.VALOR.Text == "" && (eBotoes == Botoes.Novo || eBotoes == Botoes.Colocar || eBotoes == Botoes.Trocar))
                        {
                            if (eBotoes == Botoes.Trocar)
                            {
                                oLancamentos.DataRetirada = "01/01/1900";
                                oLancamentos.CodigoMotoristaRetirou = 0;
                                oLancamentos.CodigoCaminhoRetirada = 0;
                                intNumeroLancamento.VALOR.Text = oLancamentosDados.Inserir(oLancamentos).ToString();
                                // Salvar em LancamentoProgrogramacao
                                if (eAcaoLocacaoProgramacao != Botoes.Nulo && pSequencialProgramacao > 0)
                                {
                                    oLancamentosDados.InserirLancamentoProgramacao(Convert.ToInt32(intNumeroLancamento.VALOR.Text),
                                                                                   Convert.ToInt32(cliente1.txtCodigo.Text),
                                                                                   DataRetirada.Text, pSequencialProgramacao);
                                }
                                // salvar o Numero da Colocacao no Registro da retirada e a operação
                                oLancamentosDados.SalvarNumeroTroca(NumeroLancamentoDoContainerRetirado, Convert.ToInt32(intNumeroLancamento.VALOR.Text));
                                eBotoes = Botoes.Nulo;
                            }
                            else
                            {
                                intNumeroLancamento.VALOR.Text = oLancamentosDados.Inserir(oLancamentos).ToString();
                                // Salvar em LancamentoProgrogramacao
                                if (eAcaoLocacaoProgramacao != Botoes.Nulo && pSequencialProgramacao > 0)
                                {
                                    oLancamentosDados.InserirLancamentoProgramacao(Convert.ToInt32(intNumeroLancamento.VALOR.Text),
                                                                                   Convert.ToInt32(cliente1.txtCodigo.Text),
                                                                                   DataColocacao.Text, pSequencialProgramacao);
                                }
                                SalvarLog("Inclusão", sLog);
                                if (eBotoes == Botoes.Novo)
                                    InsertLocacaoMTR();
                                MostraLancamento(Convert.ToInt32(intNumeroLancamento.VALOR.Text));
                                if (eBotoes == Botoes.Trocar)
                                {
                                    oLancamentos.DataRetirada = "01/01/1900";
                                    DataRetirada.Text = "01/01/1900";
                                }
                            }
                        }
                        if (eBotoes != Botoes.Trocar)
                        {
                            RefreshGradeLancamento_MTR();
                        }
                    }
                    if (eBotoes != Botoes.Trocar)
                    {
                        grbColocacao.Enabled = true;
                        grbRetirada.Enabled = true;
                    }
                }
            }
        }

        private void RefreshGradeLancamento_MTR()
        {
            if (cliente1.txtCodigo.Text != "")
            {
                DataTable dtColocacoes = new DataTable();
                DataTable dtRetiradas = new DataTable();
                geral.CodigoCliente = Convert.ToInt32(cliente1.txtCodigo.Text);

                dtColocacoes = oLancamentosDados.PreencheDadosLocacao(DataMostragemInicial.Text, DataMostragemFinal.Text,
                               geral.CodigoCliente, "DataColocacao asc", false, false, true, true, true, true, true, true, false, false, false, false, true, true);
                dtRetiradas = oLancamentosDados.PreencheDadosLocacao(DataMostragemInicial.Text, DataMostragemFinal.Text,
                              geral.CodigoCliente, "DataRetirada  desc", false, false, true, true, true, true, true, true, false, false, false, false, true, false, true);
                dtColocacoes.Merge(dtRetiradas);
                bindingSource.DataSource = dtColocacoes;
                GradeLancamentos.DataSource = bindingSource.DataSource;

                EstiloGradeLancamentos();
                if (GradeLancamentos.Rows[0].Cells["NomeFantasia"].Value.ToString() != "")
                {
                    bindingSource.DataSource = oClienteDados.PreencheSoNomeFantasia(GradeLancamentos.Rows[0].Cells["NomeFantasia"].Value.ToString());
                    GradeClientes.DataSource = bindingSource.DataSource;
                }
                if (intNumeroLancamento.VALOR.Text != "")
                {
                    bindingSource.DataSource = oLancamentosMTRDados.PegaDados(oLancamentoMTR, Convert.ToInt32(intNumeroLancamento.VALOR.Text), false);
                    GradeLancamentosMTR.DataSource = bindingSource.DataSource;
                    EstiloGradeLancMTR();
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            /*
            ofrmPermissao = new frmPermissao();
            ofrmPermissao.bPermitido = true;
            ofrmPermissao.Text = "Data da Retirada inválida! Digite a senha do Supervisor:";
            geral.VoltaForm = this.Name;
            if (!AlteracaoValida(DataRetirada.Value))
            {
                ofrmPermissao.bPermitido = false;
                ofrmPermissao.ShowDialog();
            }
            */
            ofrmPermissao = new frmPermissao();
            ofrmPermissao.bPermitido = true;
            ofrmPermissao.Text = "Data da Colocação inválida! Digite a senha do Supervisor:";
            geral.VoltaForm = this.Name;
            if (!AlteracaoValida(DataColocacao.Value))
            {
                ofrmPermissao.bPermitido = false;
                ofrmPermissao.ShowDialog();
            }
            if (!ofrmPermissao.bPermitido)
            {
                ofrmPermissao.Text = "Data da Retirada inválida! Digite a senha do Supervisor:";
                geral.VoltaForm = this.Name;
                if (!AlteracaoValida(DataRetirada.Value))
                {
                    ofrmPermissao.bPermitido = false;
                    ofrmPermissao.ShowDialog();
                }
            }
            if (ofrmPermissao.bPermitido)
            {
                if (intNumeroLancamento.VALOR.Text == "" || intNumeroLancamento.VALOR.Text == "0")
                    MessageBox.Show("Lançamento não selecionado ou inválido!");
                else if (GradeLancamentosMTR.Rows.Count > 0)
                {
                    eBotoes = Botoes.Excluir;
                    DialogResult oYes = MessageBox.Show("Excluir lançamento atual e seus resíduos?", "Confirmação", MessageBoxButtons.YesNo);
                    if (oYes == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (intNumeroLancamento.VALOR.Text != "")
                        {

                            string sLog = "";
                            sLog = sLog + "Nº Lançamento: " + intNumeroLancamento.VALOR.Text + " \n";
                            sLog = sLog + "Nº Caixa: " + cboContainerColocacao.Text + " \n";
                            sLog = sLog + "Código Cliente: " + cliente1.txtCodigo.Text + " \n";
                            sLog = sLog + "Código Caminhão Colocou: " + caminhaoColocacao.txtCodigo.Text + " \n";
                            sLog = sLog + "Código Motorista Colocou: " + funcionarioColocacao.txtCodigo.Text + " \n";
                            sLog = sLog + "Data lançamento: " + DataLancamento.Text + " \n";
                            sLog = sLog + "Data Colocação: " + DataColocacao.Text + " \n";
                            sLog = sLog + "Data Retirada: " + DataRetirada.Text + " \n";
                            sLog = sLog + "Código Motorista Retirou: " + funcionarioRetirada.txtCodigo.Text + " \n";
                            sLog = sLog + "Código Caminhão Retirada: " + caminhaoRetirada.txtCodigo.Text + " \n";
                            if (GradeLancamentosMTR.Rows[0].Cells[0].Value != null)
                            {
                                foreach (DataGridViewRow gvr in GradeLancamentosMTR.Rows)
                                {
                                    if (gvr.Cells["NumeroMTR"].Value != null)
                                    {
                                        sLog = sLog + "Nº Lançamento: " + intNumeroLancamento.VALOR.Text + " \n";
                                        sLog = sLog + "Nº MTR: " + gvr.Cells["NumeroMTR"].Value.ToString() + " \n";
                                        sLog = sLog + "Código resíduo: " + gvr.Cells["CodigoResiduo"].Value.ToString() + " \n";
                                        sLog = sLog + "Quantidade :" + gvr.Cells["QtDescarga"].Value.ToString() + " \n";
                                        sLog = sLog + "Valor unitário: " + gvr.Cells["ValorUnitario"].Value.ToString() + " \n";
                                        sLog = sLog + "Valor total: " + gvr.Cells["ValorTotal"].Value.ToString() + " \n";
                                        sLog = sLog + "Destino final: " + gvr.Cells["Deposito"].Value.ToString() + " \n";
                                        sLog = sLog + "Unidade: " + gvr.Cells["Unidade"].Value.ToString() + " \n";
                                        sLog = sLog + "Ticket:" + gvr.Cells["Ticket"].Value.ToString() + " \n";
                                        sLog = sLog + "Data descarga: " + gvr.Cells["DataDescarga"].Value.ToString() + " \n";
                                        sLog = sLog + "Nº MTR-e: " + gvr.Cells["NumeroMTRFatima"].Value.ToString() + " \n";
                                        sLog = sLog + "Observação: " + gvr.Cells["Observacao"].Value.ToString() + " \n";
                                    }
                                }
                            }
                            sLog = sLog + " \n";
                            SalvarLog("Exclusão", sLog, "Exclusão-Lançamento");

                            // excluir o lançamento
                            oLancamentosDados.Excluir(Convert.ToInt32(intNumeroLancamento.VALOR.Text));

                            // excluir residuos do lançamento
                            if (GradeLancamentosMTR.Rows.Count > 0)
                            {
                                if (GradeLancamentosMTR.Rows[0].Cells[0].Value != null)
                                {
                                    foreach (DataGridViewRow gvr in GradeLancamentosMTR.Rows)
                                    {
                                        if (gvr.Cells["CodigoResiduo"].Value != null)
                                            oLancamentosMTRDados.Excluir(Convert.ToInt32(intNumeroLancamento.VALOR.Text),
                                                                         Convert.ToInt32(gvr.Cells["NumeroMTR"].Value.ToString()),
                                                                         Convert.ToInt32(gvr.Cells["CodigoResiduo"].Value.ToString()));
                                    }
                                }
                            }
                            bindingSource.DataSource = oLancamentosDados.PreencheDadosLocacao(DataMostragemInicial.Text, DataMostragemFinal.Text,
                                                        geral.CodigoCliente, "NomeFantasia asc", false, false, true, true, true, true, true, true, false, false, false, false, true);
                            GradeLancamentos.DataSource = bindingSource.DataSource;
                            EstiloGradeLancamentos();
                            bindingSource.DataSource = new DataTable();
                            GradeLancamentosMTR.DataSource = bindingSource.DataSource;
                            LimpaCampos();
                        }
                    }
                }
            }
        }
        
        private void btnAnular_Click(object sender, EventArgs e)
        {
            ofrmPermissao.bPermitido = false;
            eBotoes = Botoes.Anular;
            CamposHabilitaOuDesabilita(false);
            GradeLancamentosMTR.Enabled = true;
            grbRetirada.Enabled = true;
            grbColocacao.Enabled = true;
            DataColocacao.Text = DateTime.Now.AddDays(-1).ToString();
            btnNovo.Focus();

            if (intNumeroLancamento.VALOR.Text != "")
            {
                MostraLancamento(Convert.ToInt32(intNumeroLancamento.VALOR.Text));
            }
            else if (Convert.ToInt32(cliente1.txtCodigo.Text) == 0)
                MostraLancamento(oLancamentosDados.UltimoRegistro(false));
            else if (Convert.ToInt32(cliente1.txtCodigo.Text) > 0)
            {
                pNumeroLancado = oLancamentosDados.PegaNumeroLancamento(pCodigoCliente, "");
                intNumeroLancamento.VALOR.Text = pNumeroLancado.ToString();
                MostraLancamento(pNumeroLancado);
            }
        }
        private void btnColocar_Click(object sender, EventArgs e)
        {
            eBotoes = Botoes.Colocar;
            CamposHabilitaOuDesabilita(true);
            GradeLancamentosMTR.Enabled = false;
            grbRetirada.Enabled = false;
            cboContainerColocacao.Text = "";
            if (e != EventArgs.Empty)
                LimpaCampos();
            DataRetirada.Text = "1900-01-01";
            if (pDataProgramacao != "")
                DataColocacao.Text = pDataProgramacao;
            cboContainerColocacao.Focus();
            if (pCodigoCliente > 0)
            {
                cliente1.txtCodigo.Text = pCodigoCliente.ToString();
                cliente1.txtDescricao.Text = oClienteDados.PegaNomeFantasia(pCodigoCliente);
            }
            if (pCodigoCaminhao > 0)
            {
                caminhaoColocacao.txtCodigo.Text = pCodigoCaminhao.ToString();
                caminhaoColocacao.txtDescricao.Text = oCaminhaoDados.PegaDados(oCaminhao, pCodigoCaminhao).Modelo;
            }
            if (pCodigoMotorista > 0)
            {
                funcionarioColocacao.txtCodigo.Text = pCodigoMotorista.ToString();
                funcionarioColocacao.txtDescricao.Text = oMotoristaDados.PegaDados(oMotorista, pCodigoMotorista).Nome;
            }
        }

        private void btnRetirar_Click(object sender, EventArgs e)
        {
            eBotoes = Botoes.Retirar;
            if (eAcaoLocacaoProgramacao == Botoes.Nulo)
                LimpaCampos();
            else
            {
                cboContainerColocacao.Text = "";
                funcionarioColocacao.txtCodigo.Text = "";
                funcionarioColocacao.txtDescricao.Text = "";
                cliente1.txtCodigo.Text = "";
                cliente1.txtDescricao.Text = "";
                caminhaoColocacao.txtCodigo.Text = "";
                DataColocacao.Text = "";
            }
            CamposHabilitaOuDesabilita(true);
            GradeLancamentosMTR.Enabled = false;
            cboContainerRetirada.Enabled = true;
            grbColocacao.Enabled = false;
            grbRetirada.Enabled = true;
            cboContainerRetirada.Focus();
            if (eAcaoLocacaoProgramacao == Botoes.Nulo)
                DataRetirada.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            else if (pDataProgramacao != "")
                DataRetirada.Text = pDataProgramacao;
            if (pCodigoCliente > 0)
            {
                cliente1.txtCodigo.Text = pCodigoCliente.ToString();
                cliente1.txtDescricao.Text = oClienteDados.PegaNomeFantasia(pCodigoCliente);
            }
            if (pCodigoCaminhao > 0)
            {
                caminhaoRetirada.txtCodigo.Text = pCodigoCaminhao.ToString();
                caminhaoRetirada.txtDescricao.Text = oCaminhaoDados.PegaDados(oCaminhao, pCodigoCaminhao).Modelo;
            }
            if (pCodigoMotorista > 0)
            {
                funcionarioRetirada.txtCodigo.Text = pCodigoMotorista.ToString();
                funcionarioRetirada.txtDescricao.Text = oMotoristaDados.PegaDados(oMotorista, pCodigoMotorista).Nome;
            }
        }

        private void btnTrocar_Click(object sender, EventArgs e)
        {
            ofrmPermissao.bPermitido = true;
            eBotoes = Botoes.Trocar;
            if (eAcaoLocacaoProgramacao == Botoes.Nulo)
                LimpaCampos();
            else
            {
                cboContainerColocacao.Text = "";
                funcionarioColocacao.txtCodigo.Text = "";
                funcionarioColocacao.txtDescricao.Text = "";
                cliente1.txtCodigo.Text = "";
                cliente1.txtDescricao.Text = "";
                caminhaoColocacao.txtCodigo.Text = "";
                DataColocacao.Text = "1900-01-01";
            }
            grbColocacao.Enabled = false;
            grbRetirada.Enabled = true;

            btnSalvar.Enabled = true;
            btnAnular.Enabled = true;
            btnExcluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnColocar.Enabled = false;
            btnNovo.Enabled = false;
            btnRetirar.Enabled = false;
            btnTrocar.Enabled = false;
            GradeLancamentosMTR.Enabled = false;

            cboContainerRetirada.Enabled = true;
            cboContainerRetirada.Text = "";
            cboContainerRetirada.Focus();

            DataRetirada.Enabled = true;
            caminhaoRetirada.txtCodigo.Enabled = true;
            caminhaoRetirada.txtDescricao.Enabled = true;
            funcionarioRetirada.txtCodigo.Enabled = true;
            funcionarioRetirada.txtDescricao.Enabled = true;
            txtInformacoesLogistica.Enabled = true;
            if (eAcaoLocacaoProgramacao == Botoes.Nulo && pDataProgramacao == "")
                DataRetirada.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            else if (pDataProgramacao != "")
                DataRetirada.Text = pDataProgramacao;
            if (pCodigoCliente > 0)
            {
                cliente1.txtCodigo.Text = pCodigoCliente.ToString();
                cliente1.txtDescricao.Text = oClienteDados.PegaNomeFantasia(pCodigoCliente);
            }
            if (pCodigoCaminhao > 0)
            {
                caminhaoRetirada.txtCodigo.Text = pCodigoCaminhao.ToString();
                caminhaoRetirada.txtDescricao.Text = oCaminhaoDados.PegaDados(oCaminhao, pCodigoCaminhao).Modelo;
            }
            if (pCodigoMotorista > 0)
            {
                funcionarioRetirada.txtCodigo.Text = pCodigoMotorista.ToString();
                funcionarioRetirada.txtDescricao.Text = oMotoristaDados.PegaDados(oMotorista, pCodigoMotorista).Nome;
            }
        }

        private void LimpaCampos()
        {
            intNumeroLancamento.VALOR.Text = "";
            DataLancamento.Text = "";
            cboContainerColocacao.Text = "";
            cliente1.txtCodigo.Text = "";
            cliente1.txtDescricao.Text = "";
            caminhaoColocacao.txtCodigo.Text = "";
            caminhaoColocacao.txtDescricao.Text = "";
            funcionarioColocacao.txtCodigo.Text = "";
            funcionarioColocacao.txtDescricao.Text = "";
            DataColocacao.Text = "";
            DataRetirada.Text = "1900-01-01"; 
            caminhaoRetirada.txtCodigo.Text = "";
            caminhaoRetirada.txtDescricao.Text = "";
            cboContainerRetirada.Text = "";
            funcionarioRetirada.txtCodigo.Text = "";
            funcionarioRetirada.txtDescricao.Text = "";
            txtInformacoesLogistica.Text = "";
            txtProcuraCliente.Text = "";
        }

        private void GradeClientes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MostrarLancamentoClienteSelecionado(e.RowIndex);
        }

        private void MostrarLancamentoClienteSelecionado(int pLinhaCliente)
        {
            if (pLinhaCliente >= 0)
            {
                DataTable dtColocacoes = new DataTable();
                DataTable dtRetiradas = new DataTable();
                geral.CodigoCliente = Convert.ToInt32(GradeClientes.Rows[pLinhaCliente].Cells["Codigo"].Value);

                dtColocacoes = oLancamentosDados.PreencheDadosLocacao(DataMostragemInicial.Text, DataMostragemFinal.Text,
                                           geral.CodigoCliente, "DataColocacao asc", false, false, true, true, true, true, true, true, false, false, false, false, true, true);
                dtRetiradas = oLancamentosDados.PreencheDadosLocacao(DataMostragemInicial.Text, DataMostragemFinal.Text,
                                           geral.CodigoCliente, "DataRetirada  desc", false, false, true, true, true, true, true, true, false, false, false, false, true, false, true);
                dtColocacoes.Merge(dtRetiradas);
                foreach (DataRow dr in dtColocacoes.Rows)
                {
                    if (geral.Left(dr["Destino"].ToString(), 3) == "DTR")
                    {
                        clsMovimentacaoDTRDados oMovDtrDados = new clsMovimentacaoDTRDados();
                        clsResiduoDados oResDados = new clsResiduoDados();
                        string _r = oMovDtrDados.PegaMovimentacaoDTRPara(dr["NumeroLancamento"].ToString(), oResDados.PegaCodigoResiduo(dr["Residuo"].ToString()).ToString(), dr["CodigoCliente"].ToString());
                        if (_r != "")
                            dr["Destino"] = "DTR-" + _r;
                    }
                }
                bindingSource.DataSource = dtColocacoes;
                GradeLancamentos.DataSource = bindingSource.DataSource;
                EstiloGradeLancamentos();
                bindingSource.DataSource = new DataTable();
                GradeLancamentosMTR.DataSource = bindingSource.DataSource;
                LimpaCampos();
            }
        }

        private void MostraLancamentosMTR(int pLinha)
        {
            EstiloGradeLancMTR();
            DataLancamento.Text = Convert.ToDateTime(GradeLancamentos.Rows[pLinha].Cells["Data"].Value).ToString("yyyy-MM-dd");
            cboContainerColocacao.Text = GradeLancamentos.Rows[pLinha].Cells["Container"].Value.ToString();
            cliente1.txtCodigo.Text = GradeLancamentos.Rows[pLinha].Cells["CodigoCliente"].Value.ToString();
            if (GradeLancamentos.Rows[pLinha].Cells["NomeFantasia"].Value.ToString() != "")
                cliente1.txtDescricao.Text = GradeLancamentos.Rows[pLinha].Cells["NomeFantasia"].Value.ToString();
            else
                cliente1.txtDescricao.Text = (oClienteDados.PegaDados(oCliente, Convert.ToInt32(cliente1.txtCodigo.Text))).NomeFantasia;

            caminhaoColocacao.txtCodigo.Text = GradeLancamentos.Rows[pLinha].Cells["CodigoCaminhaoColoca"].Value.ToString();
            if (GradeLancamentos.Rows[pLinha].Cells["Caminhao"].Value.ToString() != "")
            {
                caminhaoColocacao.txtCodigo.Text = GradeLancamentos.Rows[pLinha].Cells["CodigoCaminhaoColoca"].Value.ToString();
                caminhaoColocacao.txtDescricao.Text = GradeLancamentos.Rows[pLinha].Cells["Caminhao"].Value.ToString();
            }
            else
                caminhaoColocacao.txtDescricao.Text = (oCaminhaoDados.PegaDados(oCaminhao, Convert.ToInt32(caminhaoColocacao.txtCodigo.Text))).Modelo;
            funcionarioColocacao.txtCodigo.Text = GradeLancamentos.Rows[pLinha].Cells["CodigoMotoristaColocou"].Value.ToString();
            if (GradeLancamentos.Rows[pLinha].Cells["Motorista"].Value.ToString() != "")
                funcionarioColocacao.txtDescricao.Text = GradeLancamentos.Rows[pLinha].Cells["Motorista"].Value.ToString();
            else
                funcionarioColocacao.txtDescricao.Text = (oMotoristaDados.PegaDados(oMotorista, Convert.ToInt32(funcionarioColocacao.txtCodigo.Text))).Nome;

            DataColocacao.Text = Convert.ToDateTime(GradeLancamentos.Rows[pLinha].Cells["DataColocacao"].Value).ToString("yyyy-MM-dd");
            if (GradeLancamentos.Rows[pLinha].Cells["DataRetirada"].Value.ToString() != "" &&
                GradeLancamentos.Rows[pLinha].Cells["DataRetirada"].Value.ToString().Substring(0, 10) != "01/01/0001" &&
                GradeLancamentos.Rows[pLinha].Cells["DataRetirada"].Value.ToString().Substring(0, 10) != "01/01/0100" &&
                GradeLancamentos.Rows[pLinha].Cells["DataRetirada"].Value.ToString().Substring(0, 10) != "01/01/1900")
                DataRetirada.Text = Convert.ToDateTime(GradeLancamentos.Rows[pLinha].Cells["DataRetirada"].Value).ToString("yyyy-MM-dd");
            else
                DataRetirada.Text = "01/01/1900";
            if (GradeLancamentos.Rows[pLinha].Cells["CodigoCaminhoRetirada"].Value.ToString() != "")
                caminhaoRetirada.txtCodigo.Text = GradeLancamentos.Rows[pLinha].Cells["CodigoCaminhoRetirada"].Value.ToString();
            caminhaoRetirada.txtDescricao.Text = GradeLancamentos.Rows[pLinha].Cells["CaminhaoRetirada"].Value.ToString();

            cboContainerRetirada.Text = "";
            if (caminhaoRetirada.txtCodigo.Text != "" && funcionarioRetirada.txtCodigo.Text != "" &&
                caminhaoRetirada.txtCodigo.Text != "0" && funcionarioRetirada.txtCodigo.Text != "0")
                cboContainerRetirada.Text = GradeLancamentos.Rows[pLinha].Cells["Container"].Value.ToString();
            
            if (GradeLancamentos.Rows[pLinha].Cells["CodigoMotoristaRetirou"].Value.ToString() != "")
                funcionarioRetirada.txtCodigo.Text = GradeLancamentos.Rows[pLinha].Cells["CodigoMotoristaRetirou"].Value.ToString();
            funcionarioRetirada.txtDescricao.Text = GradeLancamentos.Rows[pLinha].Cells["MotoristaRetirada"].Value.ToString();
            txtInformacoesLogistica.Text = GradeLancamentos.Rows[pLinha].Cells["ObsLogistica"].Value.ToString();
        }
        private void GradeLancamentos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            pLinhaGradeMTR = -1;
            if (e.RowIndex >= 0)
            {
                intNumeroLancamento.VALOR.Text = Convert.ToInt32(GradeLancamentos.Rows[e.RowIndex].Cells["NumeroLancamento"].Value).ToString("0000000");
                DataLancamento.Text = GradeLancamentos.Rows[e.RowIndex].Cells["Data"].Value.ToString();
                DataTable _dtlancMTR = new DataTable();
                _dtlancMTR = oLancamentosMTRDados.PegaDados(oLancamentoMTR, Convert.ToInt32(GradeLancamentos.Rows[e.RowIndex].Cells["NumeroLancamento"].Value), false);
                foreach (DataRow dr in _dtlancMTR.Rows)
                {
                    if (geral.Left(dr["Deposito"].ToString(), 3) == "DTR")
                    {
                        clsMovimentacaoDTRDados oMovDtrDados = new clsMovimentacaoDTRDados();
                        if (geral.CodigoCliente > 0)
                        {
                            string _r = oMovDtrDados.PegaMovimentacaoDTRPara(dr["NumeroLancamento"].ToString(), dr["CodigoResiduo"].ToString(), geral.CodigoCliente.ToString());
                            if (_r != "")
                                dr["Deposito"] = "DTR-" + _r;
                        }
                    }
                }
                bindingSource.DataSource = _dtlancMTR;
                GradeLancamentosMTR.DataSource = bindingSource.DataSource;
                MostraLancamentosMTR(e.RowIndex);
                GradeLancamentosMTR.Enabled = true;
            }
        }

        private void EstiloGradeLancMTR()
        {
            GradeLancamentosMTR.Columns["NumeroLancamento"].Width = 86;
            GradeLancamentosMTR.Columns["NumeroLancamento"].HeaderText = "Nº Lançamento";
            GradeLancamentosMTR.Columns["NumeroLancamento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            GradeLancamentosMTR.Columns["NumeroLancamento"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GradeLancamentosMTR.Columns["NumeroLancamento"].ReadOnly = true;

            GradeLancamentosMTR.Columns["NumeroMTR"].Width = 60;
            GradeLancamentosMTR.Columns["NumeroMTR"].HeaderText = "Nº MTR";
            GradeLancamentosMTR.Columns["NumeroMTR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            GradeLancamentosMTR.Columns["NumeroMTR"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            GradeLancamentosMTR.Columns["CodigoResiduo"].Width = 60;
            GradeLancamentosMTR.Columns["CodigoResiduo"].HeaderText = "Código Resíduo";
            GradeLancamentosMTR.Columns["CodigoResiduo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            GradeLancamentosMTR.Columns["CodigoResiduo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            GradeLancamentosMTR.Columns["DescricaoReduzida"].Width = 170;
            GradeLancamentosMTR.Columns["DescricaoReduzida"].HeaderText = "Resíduo";
            GradeLancamentosMTR.Columns["DescricaoReduzida"].ReadOnly = true;

            GradeLancamentosMTR.Columns["QtColetada"].Width = 80;
            GradeLancamentosMTR.Columns["QtColetada"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            GradeLancamentosMTR.Columns["QtColetada"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            GradeLancamentosMTR.Columns["QtDescarga"].Width = 80;
            GradeLancamentosMTR.Columns["QtDescarga"].HeaderText = "Quantidade Descarga";
            GradeLancamentosMTR.Columns["QtDescarga"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            GradeLancamentosMTR.Columns["QtDescarga"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            GradeLancamentosMTR.Columns["ValorUnitario"].Width = 80;
            GradeLancamentosMTR.Columns["ValorUnitario"].HeaderText = "Valor Unitário";
            GradeLancamentosMTR.Columns["ValorUnitario"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            GradeLancamentosMTR.Columns["ValorUnitario"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            GradeLancamentosMTR.Columns["ValorTotal"].Width = 80;
            GradeLancamentosMTR.Columns["ValorTotal"].HeaderText = "Valor Total";
            GradeLancamentosMTR.Columns["ValorTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            GradeLancamentosMTR.Columns["ValorTotal"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            GradeLancamentosMTR.Columns["observacao"].Width = 170;
            GradeLancamentosMTR.Columns["observacao"].HeaderText = "Observação";

            GradeLancamentosMTR.Columns["Deposito"].Width = 170;
            GradeLancamentosMTR.Columns["Deposito"].HeaderText = "Dep/Local Arm";

            GradeLancamentosMTR.Columns["CodigoAterroSanitario"].Visible = false;

            GradeLancamentosMTR.Columns["DataDescarga"].Width = 90;
            GradeLancamentosMTR.Columns["DataDescarga"].HeaderText = "Data Descarga";
            GradeLancamentosMTR.Columns["DataDescarga"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GradeLancamentosMTR.Columns["DataDescarga"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            GradeLancamentosMTR.Columns["DescargaMTRe"].Width = 90;
            GradeLancamentosMTR.Columns["DescargaMTRe"].HeaderText = "Destino MTR-e";
            GradeLancamentosMTR.Columns["DescargaMTRe"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            GradeLancamentosMTR.Columns["ControleInternoDescarga"].Width = 120;
            GradeLancamentosMTR.Columns["ControleInternoDescarga"].HeaderText = "Contr.Interno Descarga";
            GradeLancamentosMTR.Columns["ControleInternoDescarga"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            GradeLancamentosMTR.Columns["HoraDescarga"].Width = 70;
            GradeLancamentosMTR.Columns["HoraDescarga"].HeaderText = "Hora Descarga";
            GradeLancamentosMTR.Columns["HoraDescarga"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            GradeLancamentosMTR.Columns["NumeroMTRFatima"].Width = 90;
            GradeLancamentosMTR.Columns["NumeroMTRFatima"].HeaderText = "Nº MTR-e";
            GradeLancamentosMTR.Columns["NumeroMTRFatima"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            GradeLancamentosMTR.Columns["NumeroMTRFatima"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            GradeLancamentosMTR.Columns["Motivo"].Visible = false;
        }

        private void cliente1_Enter(object sender, EventArgs e)
        {
            geral.VoltaForm = this.Name + "CLIENTE";
            geral.CodigoCliente = 0;
        }

        private void caminhaoColocacao_Enter(object sender, EventArgs e)
        {
            geral.VoltaForm = this.Name + "CAMINHAO";
            geral.CodigoCaminhao = 0;
        }

        private void funcionarioColocacao_Enter(object sender, EventArgs e)
        {
            geral.VoltaForm = this.Name + "MOTORISTA";
            geral.CodigoMotorista = 0;
        }

        private void GradeLancamentos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                InsertLocacaoMTR();
                RefreshGradeLancamento_MTR();
            }
            else if (e.KeyCode == Keys.F7)
            {
                using (frmConfirmaTroca ofrmConfirmaTroca = new frmConfirmaTroca())
                {
                    ofrmConfirmaTroca.lblNumeroLancamento.Text = PegaNumeroLancamentoSelecionado();
                    if (ofrmConfirmaTroca.lblNumeroLancamento.Text == "")
                        MessageBox.Show("Selecione a coluna da data da retirada!");
                    else
                    {
                        ofrmConfirmaTroca.ShowDialog();
                        if (ofrmConfirmaTroca.bSalvar)
                        {
                            if (ofrmConfirmaTroca.intNumeroLancamentoTroca.VALOR.Text == "")
                                ofrmConfirmaTroca.intNumeroLancamentoTroca.VALOR.Text = "0";
                            oLancamentosDados.SalvarNumeroTroca(Convert.ToInt32(ofrmConfirmaTroca.lblNumeroLancamento.Text),
                                                                Convert.ToInt32(ofrmConfirmaTroca.intNumeroLancamentoTroca.VALOR.Text));
                            oLancamentosDados.SalvarComoTroca(Convert.ToInt32(ofrmConfirmaTroca.intNumeroLancamentoTroca.VALOR.Text));
                            SetNumeroLancamentoSelecionado(ofrmConfirmaTroca.intNumeroLancamentoTroca.VALOR.Text);
                        }
                        ofrmConfirmaTroca.Close();
                        ofrmConfirmaTroca.Dispose();
                    }
                }
            }
        }
        private string SetNumeroLancamentoSelecionado(string pNumeroLancamentoQueColocou)
        {
            string sRet = "";
            foreach (DataGridViewRow gvr in GradeLancamentos.Rows)
            {
                if (gvr.Cells[6].Selected)
                {
                    gvr.Cells[6].Value = pNumeroLancamentoQueColocou;
                    break;
                }
            }
            return sRet;
        }
        private string PegaNumeroLancamentoSelecionado()
        {
            string sRet = "";
            foreach (DataGridViewRow gvr in GradeLancamentos.Rows)
            {
                if (gvr.Cells[0].Selected || gvr.Cells[1].Selected || gvr.Cells[2].Selected || gvr.Cells[3].Selected || gvr.Cells[4].Selected || gvr.Cells[5].Selected || gvr.Cells[6].Selected)
                {
                    sRet = gvr.Cells[0].Value.ToString();
                    break;
                }
            }
            return sRet;
        }
        private void InsertLocacaoMTR()
        {
            if (intNumeroLancamento.VALOR.Text != "" && intNumeroLancamento.VALOR.Text != "0")
            {
                using (frmLocacaoMTR o_frmLocacaoMTR = new frmLocacaoMTR())
                {
                    o_frmLocacaoMTR.dtpDataDescarga.Value = DataRetirada.Value;
                    o_frmLocacaoMTR.residuo1.txtCodigo.Text = pCodigoResiduo.ToString();
                    o_frmLocacaoMTR.cboDestino.Text = pCodigoDestinoFinal.ToString();
                    o_frmLocacaoMTR.lblNrLancamento.Text = Convert.ToInt32(intNumeroLancamento.VALOR.Text).ToString("0000000");
                    if (cliente1.txtCodigo.Text != "")
                        o_frmLocacaoMTR.pCodigoCliente = Convert.ToInt32(cliente1.txtCodigo.Text);
                    if (pQuantidade > 0)
                    {
                        o_frmLocacaoMTR.moeQuantidadeColetada.VALOR.Text = pQuantidade.ToString();
                        o_frmLocacaoMTR.moeQuantidadeDescarregada.VALOR.Text = pQuantidade.ToString();
                    }
                    o_frmLocacaoMTR.txtObservacao.Text = pObservacaoLogistica;
                    o_frmLocacaoMTR.intNumeroMTRe.Text = pNumeroMTRe;
                    o_frmLocacaoMTR.ShowDialog();
                    o_frmLocacaoMTR.Close();
                    o_frmLocacaoMTR.Dispose();
                }
            }
            else
                MessageBox.Show("Selecione um lançamento!");
        }

        private void cboContainerColocacao_Leave(object sender, EventArgs e)
        {
            cboContainerColocacao.Text = cboContainerColocacao.Text.Replace("'","").ToUpper();
            if (cboContainerColocacao.Text == "")
            {
                CamposHabilitaOuDesabilita(false);
                LimpaCampos();
                btnNovo.Focus();
            }
            else
            {
                clsCacambaDados oContainerDados = new clsCacambaDados();
                if (oContainerDados.ContainerExiste(cboContainerColocacao.Text))
                {
                    // verificar se o container está colocado
                    if (eBotoes == Botoes.Colocar || eBotoes == Botoes.Trocar || eBotoes == Botoes.Novo)
                    {
                        clsLancamentos oLanc = new clsLancamentos();
                        oLanc = oLancamentosDados.ContainerLocadoCliente(cboContainerColocacao.Text, 0);
                        if (oLanc.CodigoCliente > 0)
                        {
                            MessageBox.Show("Container " + cboContainerColocacao.Text + 
                                            " Colocado no Cliente: " + oLanc.CodigoCliente.ToString() + " em: " + oLanc.DataColocacao);
                            if (geral.UsuarioAtual.ToUpper() == "TEIXEIRA")
                            {
                                DialogResult _dr = DialogResult.No;
                                _dr = MessageBox.Show("Excluir para continuar...", oLanc.NumeroLancamento.ToString(), MessageBoxButtons.YesNo);
                                if (_dr == DialogResult.Yes)
                                {
                                    oLancamentosDados.Excluir(oLanc.NumeroLancamento);
                                }
                            }
                            cboContainerColocacao.Text = "";
                            cboContainerColocacao.Focus();
                        }
                        else
                        {
                            cboContainerRetirada.Text = cboContainerColocacao.Text;
                            cboContainerRetirada.Enabled = false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Inexistente inválido!");
                    cboContainerColocacao.Text = "";
                    cboContainerColocacao.Focus();
                }
            }
        }

        private void cliente1_Leave(object sender, EventArgs e)
        {
            if (cliente1.txtCodigo.Text == "" && cliente1.txtCodigo.Enabled)
            {
                MessageBox.Show("Código do cliente inválido!");
                cliente1.txtCodigo.Focus();
            }
            else if (cliente1.txtCodigo.Text != "" && cliente1.txtCodigo.Text != null)
            {
                oCliente = new clsClientes();
                oCliente = oClienteDados.PegaDados(oCliente, Convert.ToInt32(cliente1.txtCodigo.Text));
                if (oCliente.Inativo == 1)
                {
                    MessageBox.Show("Cliente inativo!");
                    cliente1.txtCodigo.Focus();
                }
            }
            else if (cliente1.txtCodigo.Enabled)
                geral.CodigoCliente = Convert.ToInt32(cliente1.txtCodigo.Text);
        }

        private void caminhaoColocacao_Leave(object sender, EventArgs e)
        {
            if (caminhaoColocacao.txtCodigo.Text == "" && caminhaoColocacao.txtCodigo.Enabled)
            {
                MessageBox.Show("Código do caminhão inválido!");
                caminhaoColocacao.Focus();
            }
        }

        private void funcionarioColocacao_Leave(object sender, EventArgs e)
        {
            if (funcionarioColocacao.txtCodigo.Text == "" && funcionarioColocacao.txtCodigo.Enabled)
            {
                MessageBox.Show("Código do motorista inválido!");
                funcionarioColocacao.Focus();
            }
        }

        private void cboContainerRetirada_Leave(object sender, EventArgs e)
        {
            cboContainerRetirada.Text = cboContainerRetirada.Text.Replace("'", "").ToUpper();
            if (eBotoes != Botoes.Alterar)
            {
                // verificar container locado
                if (cboContainerRetirada.Text == "")
                {
                    CamposHabilitaOuDesabilita(false);
                    LimpaCampos();
                    btnNovo.Focus();
                }
                else if (cboContainerRetirada.Text != "" && (eBotoes == Botoes.Retirar || eBotoes == Botoes.Trocar))
                {
                    NumeroLancamentoDoContainerRetirado = 0;
                    oLancamentos = new clsLancamentos();
                    oLancamentos = oLancamentosDados.ContainerLocadoCliente(cboContainerRetirada.Text, 0);
                    if (oLancamentos.NumeroCaixa == "" || oLancamentos.NumeroCaixa == null)
                    {
                        MessageBox.Show("Container não colocado!");
                        cboContainerRetirada.Text = "";
                        cboContainerRetirada.Focus();
                    }
                    else
                    {
                        if (oLancamentos.CodigoCliente != pCodigoCliente && pCodigoCliente > 0)
                        {
                            MessageBox.Show("Container está locado em um cliente diferente!");
                            cboContainerRetirada.Text = "";
                            cboContainerRetirada.Focus();
                        }
                        else
                        {
                            intNumeroLancamento.VALOR.Text = oLancamentos.NumeroLancamento.ToString();
                            NumeroLancamentoDoContainerRetirado = oLancamentos.NumeroLancamento;
                            cboContainerColocacao.Text = oLancamentos.NumeroCaixa;
                            cliente1.txtCodigo.Text = oLancamentos.CodigoCliente.ToString();
                            geral.CodigoCliente = oLancamentos.CodigoCliente;
                            cliente1.AtribuirCamposControle();
                            caminhaoColocacao.txtCodigo.Text = oLancamentos.CodigoCaminhaoColoca.ToString();
                            geral.CodigoCaminhao = oLancamentos.CodigoCaminhaoColoca;
                            caminhaoColocacao.AtribuiCamposControle();
                            funcionarioColocacao.txtCodigo.Text = oLancamentos.CodigoMotoristaColocou.ToString();
                            geral.CodigoMotorista = oLancamentos.CodigoMotoristaColocou;
                            funcionarioColocacao.AtribuiCamposControle();
                            DataColocacao.Text = oLancamentos.DataColocacao;
                        }
                    }
                }
            }
        }

        private void GradeLancamentosMTR_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //
            // chave primaria
            // NumeroLancamento
            // NumeroMTR
            // CodigoResiduo
            if (GradeLancamentosMTR.Rows.Count > 0 && GradeLancamentosMTR.CurrentRow.Index >= 0 && intNumeroLancamento.VALOR.Text != "")
            {
                int _currentrowindex = GradeLancamentosMTR.CurrentRow.Index;
                _currentrowindex = GradeLancamentosMTR.CurrentRow.Index;
                using (frmLocacaoMTR o_frmLocacaoMTR = new frmLocacaoMTR())
                {
                    o_frmLocacaoMTR.lblNrLancamento.Text = Convert.ToInt32(intNumeroLancamento.VALOR.Text).ToString("0000000");
                    o_frmLocacaoMTR.intNumeroMTR.VALOR.Text = GradeLancamentosMTR.Rows[_currentrowindex].Cells["NumeroMTR"].Value.ToString();
                    o_frmLocacaoMTR.residuo1.txtCodigo.Text = GradeLancamentosMTR.Rows[_currentrowindex].Cells["CodigoResiduo"].Value.ToString();
                    o_frmLocacaoMTR.moeQuantidadeColetada.VALOR.Text = Convert.ToDecimal(GradeLancamentosMTR.Rows[_currentrowindex].Cells["QtColetada"].Value).ToString();
                    o_frmLocacaoMTR.moeQuantidadeDescarregada.VALOR.Text = Convert.ToDecimal(GradeLancamentosMTR.Rows[_currentrowindex].Cells["QtDescarga"].Value).ToString();
                    o_frmLocacaoMTR.moeValorTotal.VALOR.Text = Convert.ToDecimal(GradeLancamentosMTR.Rows[_currentrowindex].Cells["ValorTotal"].Value).ToString();
                    o_frmLocacaoMTR.moeValorUnitario.VALOR.Text = Convert.ToDecimal(GradeLancamentosMTR.Rows[_currentrowindex].Cells["ValorUnitario"].Value).ToString();
                    if (GradeLancamentosMTR.Rows[_currentrowindex].Cells["DataDescarga"].Value.ToString() != "01/01/1900" &&
                        GradeLancamentosMTR.Rows[_currentrowindex].Cells["DataDescarga"].Value.ToString() != "01/01/0100 00:00:00" &&
                        GradeLancamentosMTR.Rows[_currentrowindex].Cells["DataDescarga"].Value.ToString() != "01/01/0001 00:00:00")
                        o_frmLocacaoMTR.dtpDataDescarga.Text = GradeLancamentosMTR.Rows[_currentrowindex].Cells["DataDescarga"].Value.ToString();
                    else
                        o_frmLocacaoMTR.dtpDataDescarga.Text = "01/01/1900";
                    o_frmLocacaoMTR.txtHoraDescarga.Text = GradeLancamentosMTR.Rows[_currentrowindex].Cells["HoraDescarga"].Value.ToString();
                    o_frmLocacaoMTR.txtMotivoMTRe.Text = GradeLancamentosMTR.Rows[_currentrowindex].Cells["Motivo"].Value.ToString();
                    o_frmLocacaoMTR.txtDescargaMTRe.Text = GradeLancamentosMTR.Rows[_currentrowindex].Cells["DescargaMTRe"].Value.ToString();
                    o_frmLocacaoMTR.txtControleInternoDescarga.Text = GradeLancamentosMTR.Rows[_currentrowindex].Cells["ControleInternoDescarga"].Value.ToString();
                    o_frmLocacaoMTR.txtObservacao.Text = GradeLancamentosMTR.Rows[_currentrowindex].Cells["Observacao"].Value.ToString();
                    o_frmLocacaoMTR.txtTicket.Text = GradeLancamentosMTR.Rows[_currentrowindex].Cells["Ticket"].Value.ToString();
                    o_frmLocacaoMTR.txtUnidade.Text = GradeLancamentosMTR.Rows[_currentrowindex].Cells["Unidade"].Value.ToString();
                    o_frmLocacaoMTR.intNumeroMTRe.Text = GradeLancamentosMTR.Rows[_currentrowindex].Cells["NumeroMTRFatima"].Value.ToString();
                    oDestinoFinalDados = new clsDestinoFinalDados();
                    if (geral.Left(GradeLancamentosMTR.Rows[_currentrowindex].Cells["Deposito"].Value.ToString(), 3) != "DTR")
                        o_frmLocacaoMTR.cboDestino.Text = oDestinoFinalDados.PegaNomeFantasia(GradeLancamentosMTR.Rows[_currentrowindex].Cells["Deposito"].Value.ToString());
                    else
                        o_frmLocacaoMTR.cboDestino.Text = GradeLancamentosMTR.Rows[_currentrowindex].Cells["Deposito"].Value.ToString();
                    if (cliente1.txtCodigo.Text != "")
                        o_frmLocacaoMTR.pCodigoCliente = Convert.ToInt32(cliente1.txtCodigo.Text);
                    o_frmLocacaoMTR.ShowDialog();
                    o_frmLocacaoMTR.Close();
                    o_frmLocacaoMTR.Dispose();
                }
                RefreshGradeLancamento_MTR();
            }
        }

        private void funcionarioColocacao_Load(object sender, EventArgs e)
        {

        }

        private void funcionarioRetirada_Leave(object sender, EventArgs e)
        {
            if (eBotoes != Botoes.Alterar)
            {
                if (funcionarioRetirada.txtCodigo.Text == "" && funcionarioRetirada.txtCodigo.Enabled)
                {
                    MessageBox.Show("Código do motorista inválido!");
                    funcionarioRetirada.Focus();
                }
            }
        }


        private void caminhaoRetirada_Leave(object sender, EventArgs e)
        {
            if (eBotoes != Botoes.Alterar)
            {
                if (caminhaoRetirada.txtCodigo.Text == "" && caminhaoRetirada.txtCodigo.Enabled)
                {
                    MessageBox.Show("Código do caminhão inválido!");
                    caminhaoRetirada.Focus();
                }
            }
        }

        private void btnDescargaPendente_Click(object sender, EventArgs e)
        {
            using (frmDistribuicaoDescargaPendente frmNovo = new frmDistribuicaoDescargaPendente())
            {
                frmNovo.ShowDialog();
                frmNovo.Close();
                frmNovo.Dispose();
            }
        }

        private void GradeLancamentosMTR_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (GradeLancamentosMTR.CurrentCell is DataGridViewCheckBoxCell)
            {
                GradeLancamentosMTR.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void funcionarioRetirada_Enter(object sender, EventArgs e)
        {
            geral.VoltaForm = this.Name + "MOTORISTA";
            geral.CodigoMotorista = 0;
        }

        private void frmLocacoes_KeyUp(object sender, KeyEventArgs e)
        {
            if (Keys.F10 == e.KeyCode && btnColocar.Enabled)
                btnColocar_Click(new object(), EventArgs.Empty);
            if (Keys.F11 == e.KeyCode && btnRetirar.Enabled)
                btnRetirar_Click(new object(), EventArgs.Empty);
            if (Keys.F12 == e.KeyCode && btnTrocar.Enabled)
                btnTrocar_Click(new object(), EventArgs.Empty);
        }

        private void txtProcuraCliente_Leave(object sender, EventArgs e)
        {
            btnOk.Focus();
        }

        private void btnReplicarLancamentos_Click(object sender, EventArgs e)
        {
            if (GradeLancamentosMTR.Rows.Count >= 0)
            {
                if (pLinhaGradeMTR >= 0 &&
                    GradeLancamentosMTR.Rows[pLinhaGradeMTR].Cells["Deposito"].Value.ToString().IndexOf("DTR") == -1 &&
                    GradeLancamentosMTR.Rows[pLinhaGradeMTR].Cells["CodigoResiduo"].Value.ToString() != "")
                {
                    using (frmReplicarLancamentos frmReplicar = new frmReplicarLancamentos())
                    {
                        frmReplicar.pNumeroLancamento = intNumeroLancamento.VALOR.Text;
                        frmReplicar.pCodigoResiduo = GradeLancamentosMTR.Rows[pLinhaGradeMTR].Cells["CodigoResiduo"].Value.ToString();
                        frmReplicar.ShowDialog();
                        frmReplicar.Close();
                        frmReplicar.Dispose();
                        if (GradeClientes.Rows.Count > 0)
                            MostraLancamento(Convert.ToInt32(intNumeroLancamento.VALOR.Text));
                        else
                            MostrarLancamentoClienteSelecionado(0);
                    }
                }
                else
                {
                    MessageBox.Show("Escolha um Resíduo não pode estar em DTR!");
                }
            }
        }
        private void GradeLancamentosMTR_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            pLinhaGradeMTR = e.RowIndex;
        }

        private void GradeMovContainer_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            LimpaCampos();
            try
            {
                pNumeroLancado = Convert.ToInt32(GradeMovContainer.Rows[e.RowIndex].Cells["NumeroLancamento"].Value);
                MostraLancamento(pNumeroLancado);
            }
            finally
            {
                int i = 0;
                foreach (DataGridViewRow dgr in GradeLancamentos.Rows)
                {
                    if (dgr.Cells[0].Value != null)
                    {
                        i = 0;
                        break;
                    }
                    if (dgr.Cells[0].Value.ToString() == oLancamentos.NumeroLancamento.ToString())
                        break;
                    i++;
                }
                if (i > 0)
                    MostraLancamentosMTR(i);
                else
                {
                    bindingSource.DataSource = oLancamentosMTRDados.PegaDados(oLancamentoMTR, pNumeroLancado, false);
                    GradeLancamentosMTR.DataSource = bindingSource.DataSource;
                    EstiloGradeLancMTR();
                }
            }

        }

        private void btnMTRe_Click(object sender, EventArgs e)
        {
            if (txtProcuraCliente.Text != "")
            {
                if (geral.IsNumeric(txtProcuraCliente.Text))
                {
                    pNumeroLancado = oLancamentosMTRDados.PegaNumeroLancamentoPelaMTRe(Convert.ToInt64(txtProcuraCliente.Text));
                    if (pNumeroLancado > 0)
                    {
                        intNumeroLancamento.VALOR.Text = pNumeroLancado.ToString();
                        MostraLancamento(pNumeroLancado);
                        int _ln = ApontaPegaLinhaLancamentoResiduoMTR();
                        bindingSource.DataSource = oLancamentosMTRDados.PegaDados(oLancamentoMTR, Convert.ToInt32(intNumeroLancamento.VALOR.Text), false);
                        GradeLancamentosMTR.DataSource = bindingSource.DataSource;
                        MostraLancamentosMTR(_ln);
                        GradeLancamentosMTR.Enabled = true;
                    }
                }
            }
        }

        private int ApontaPegaLinhaLancamentoResiduoMTR()
        {
            int i = 0;
            foreach (DataGridViewRow dgr in GradeLancamentos.Rows)
            {
                if (dgr.Cells[0].Value != null)
                    if (dgr.Cells[0].Value.ToString() == oLancamentos.NumeroLancamento.ToString())
                        break;
                i++;
            }
            return i;
        }

        private void btnMTR_Click(object sender, EventArgs e)
        {
            if (txtProcuraCliente.Text != "")
            {
                if (geral.IsNumeric(txtProcuraCliente.Text))
                {
                    pNumeroLancado = oLancamentosMTRDados.PegaNumeroLancamentoPelaMTR(Convert.ToInt64(txtProcuraCliente.Text));
                    if (pNumeroLancado > 0)
                    {
                        intNumeroLancamento.VALOR.Text = pNumeroLancado.ToString();
                        MostraLancamento(pNumeroLancado);
                    }
                }
            }

        }
        private bool AlteracaoValida(DateTime pDataConsiderada)
        {
            bool bRet = false;
            
            // somente pode fazer alteração quando for dentro do mesmo mês ou até o dia 05 do mês subsequente e só o mês anterior não mais que isso,
            // ou seja, exemplo: hoje é 25/11/2022 posso alterar todos registros somente deste mês, se fosse dia 05/11/2022, conseguiria alterar também
            // do dia 01/10/2022 até 05/11/2022 e ou novembro inteiro.
            
            int _anoAtual = DateTime.Now.Year;
            int _mesAtual = DateTime.Now.Month;
            int _diaAtual = DateTime.Now.Day;

            if (pDataConsiderada.Year == _anoAtual && pDataConsiderada.Month == _mesAtual) // mesmo ano e mesmo mês 
            {
                bRet = true;
            }
            else if (pDataConsiderada.Year == _anoAtual && pDataConsiderada.Month == (_mesAtual - 1) && _diaAtual <= 5) // mesmo ano e mês anterior - antes do dia 5 atual.
            {
                bRet = true;
            }
            else if (pDataConsiderada.Year == (_anoAtual - 1) && pDataConsiderada.Month == 12 && _mesAtual == 1 && _diaAtual <= 5) // ano anterior no mês de janeiro
            {
                bRet = true;
            }
            return bRet;

        }
        private void btnMTRe2_Click(object sender, EventArgs e)
        {
            clsResiduos oRes = new clsResiduos();
            clsResiduoDados oResDados = new clsResiduoDados();
            if (pLinhaGradeMTR >= 0)
            {
                if (cliente1.txtCodigo.Text != "" && GradeLancamentosMTR.Rows.Count > 0)
                    oCliente = oClienteDados.PegaDados(oCliente, Convert.ToInt32(cliente1.txtCodigo.Text));
                else
                {
                    MessageBox.Show("Número da MTR eletrônica já existe ou nenhuma linha de Resíduos selecionada!");
                    return;
                }
                if (GradeLancamentosMTR.Rows[pLinhaGradeMTR].Cells["CodigoResiduo"].Value.ToString() != "")
                {
                    oResDados.PegaDados(oRes, Convert.ToInt32(GradeLancamentosMTR.Rows[pLinhaGradeMTR].Cells["CodigoResiduo"].Value));
                }

                if (funcionarioRetirada.txtCodigo.Text == "" ||
                    funcionarioRetirada.txtCodigo.Text == "0") // não emitir MTR-e serviço sem o Motorista
                {
                    MessageBox.Show("Não emitir MTR-e sem Motorista!");
                }
                else if (caminhaoRetirada.txtCodigo.Text == "" ||
                         caminhaoRetirada.txtCodigo.Text == "0") // não emitir MTR-e serviço sem o Caminhao
                {
                    MessageBox.Show("Não emitir MTR-e sem Caminhão!");
                }
                else if (oCliente.SenhaAcessoFatima.ToUpper() == "CLIENTE" || oCliente.SenhaAcessoFatima.ToUpper() == "") // quando for sem senha o cliente emite
                {
                    MessageBox.Show("O Cliente Emite a MTR-e!");
                }
                else if (oRes.EhServico == 1)
                {
                    MessageBox.Show("Não emitimos MTR-e de Serviços!");
                }
                else if ((GradeLancamentosMTR.Rows[pLinhaGradeMTR].Cells["NumeroMTRFatima"].Value.ToString() == "" ||
                          GradeLancamentosMTR.Rows[pLinhaGradeMTR].Cells["NumeroMTRFatima"].Value.ToString() == "0" ||
                          GradeLancamentosMTR.Rows[pLinhaGradeMTR].Cells["NumeroMTRFatima"].Value.ToString() == "-1") && 
                          pLinhaGradeMTR >= 0)
                {
                    frmPesquisaModeloMTRe ofrmPesquisaModeloMTRe = new frmPesquisaModeloMTRe();
                    if (GradeLancamentosMTR.Rows[pLinhaGradeMTR].Cells["CodigoResiduo"].Value.ToString() != "")
                        ofrmPesquisaModeloMTRe.pCodigoResiduo = Convert.ToInt32(GradeLancamentosMTR.Rows[pLinhaGradeMTR].Cells["CodigoResiduo"].Value);
                    ofrmPesquisaModeloMTRe.pCNPJ_CPFGerador = geral.RetiraLetras(oCliente.CNPJ_CPF);
                    ofrmPesquisaModeloMTRe.pLoginClienteIMA = geral.RetiraLetras(oCliente.CNPJ_CPF);
                    ofrmPesquisaModeloMTRe.pSenhaClienteIMA = oCliente.SenhaAcessoFatima;
                    if (cliente1.txtCodigo.Text != "")
                        ofrmPesquisaModeloMTRe.pCodigoCliente = Convert.ToInt32(cliente1.txtCodigo.Text);
                    if (caminhaoRetirada.txtCodigo.Text != "")
                        ofrmPesquisaModeloMTRe.pCodigoCaminhao = Convert.ToInt32(caminhaoRetirada.txtCodigo.Text);
                    if (funcionarioRetirada.txtCodigo.Text != "")
                        ofrmPesquisaModeloMTRe.pCodigoMotorista = Convert.ToInt32(funcionarioRetirada.txtCodigo.Text);
                    ofrmPesquisaModeloMTRe.pDataProgramada = DataRetirada.Value.ToString("dd/MM/yyyy");
                    ofrmPesquisaModeloMTRe.pCNPJDestinador = "";
                    ofrmPesquisaModeloMTRe.pGrade = GradeLancamentosMTR;
                    ofrmPesquisaModeloMTRe.pRowIndex = pLinhaGradeMTR;
                    ofrmPesquisaModeloMTRe.pChamouPelaProgramacao = false;
                    ofrmPesquisaModeloMTRe.pNumeroLancamento = Convert.ToInt32(intNumeroLancamento.VALOR.Text);
                    ofrmPesquisaModeloMTRe.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Número da MTR eletrônica já existe ou nenhuma linha selecionada!");
                }
            }
        }
    }
}
