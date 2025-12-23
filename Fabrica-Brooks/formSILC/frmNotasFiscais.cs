using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LibSILC;
using SILCNegocios;
using System.IO;

namespace formSILC
{
    public partial class frmNotasFiscais : Form
    {
        protected TextBox[] _descricao = new TextBox[22];
        protected MOEDA4[] _quantidade = new MOEDA4[22];
        protected TextBox[] _unidade = new TextBox[22];
        protected MOEDA[] _precounitario = new MOEDA[22];
        protected MOEDA[] _valor = new MOEDA[22];

        private clsConversor oMoeda = new clsConversor();
        private decimal ValorTotal = 0;
        private BindingSource bindingSource = new BindingSource();
        private clsClientes oCliente = new clsClientes();
        private clsClientes oClienteFatCNPJ_Diferente = new clsClientes();
        private clsClienteDados oClienteDados = new clsClienteDados();
        private clsNotasFiscais oNotaFiscal = new clsNotasFiscais();
        private clsNotasFiscaisDados oNotaFiscalDados = new clsNotasFiscaisDados();
        private clsCorpoNotasFiscaisDados oCorpoNFDados = new clsCorpoNotasFiscaisDados();
        private clsCorpoNotasFiscais oCorpoNF = new clsCorpoNotasFiscais();
        private clsAliquotaImpostosDados oAliquotaImpostosDados = new clsAliquotaImpostosDados();
        private clsEnderecos oEndereco = new clsEnderecos();
        private clsEnderecosDados oEnderecoDados = new clsEnderecosDados();
        private clsMunicipios oMunicipio = new clsMunicipios();
        private clsMunicipiosDados oMunicipioDados = new clsMunicipiosDados();
        private clsAliquotaImpostosDados oRetencoesDados = new clsAliquotaImpostosDados();
        private clsAliquotaImpostos oRetencao = new clsAliquotaImpostos();
        private clsTipoCobranca oTipoCobranca = new clsTipoCobranca();
        private clsTipoCobrancaDados oTipoCobrancaDados = new clsTipoCobrancaDados();

        private decimal ValorISS = 0;
        private decimal ValorIRRF = 0;
        private decimal ValorPIS = 0;
        private decimal ValorCOFINS = 0;
        private decimal ValorContrSocial = 0;
        private decimal ValorCRF = 0;
        private decimal ValorINSS = 0;

        enum Botoes
        {
            Novo, Salvar, Excluir, Anular, Nulo
        }
        private Botoes eBotoes;

        public frmNotasFiscais()
        {
            InitializeComponent();
        }
        private void SalvarLog(string pOperacao, string pLog)
        {
            clsLog oLog = new clsLog();
            clsLogDados oLogDados = new clsLogDados();
            oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
            oLog.CodigoUsuario = geral.CodigoUsuarioAtual;
            oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
            oLog.LocalOperacao = "Lançamento Notas Fiscais";
            oLog.Operacao = pOperacao;
            oLog.Log = "Nº Lançamento: " + intNumeroLancamento.VALOR.Text + " \n";
            oLog.Log = oLog.Log + pLog;
            oLogDados.Inserir(oLog);
        }

        private void SelecionaSituacaoTributaria()
        {
            if (geral.Left(cboRetencoesNF.Text, 1) == "G" && oEndereco.CodigoMunicipio == 83275)
                cboSituacaoTributaria.SelectedIndex = 0;
            else if (oCliente.Codigo == 2354)  //2354 - condominio patio das flores 
                cboSituacaoTributaria.SelectedIndex = 2;
            else if (oMunicipio.Codigo == 82651 && geral.Left(cboRetencoesNF.Text, 1) != "G") // qdo não for orgãos públicos em porto bello
                cboSituacaoTributaria.SelectedIndex = 2;
            else if (oMunicipio.Codigo == 82651) // || oMunicipio.Codigo == 8265) // porto bello não aceita 2 qdo condominio
                cboSituacaoTributaria.SelectedIndex = 1;
            else
                cboSituacaoTributaria.SelectedIndex = 2;
        }
        private void frmNotasFiscais_Load(object sender, EventArgs e)
        {
            lblDB.Visible = false;
            lblDB.Text = "db: " + geral.BancoUsado.ToString();
            if (geral.UsuarioAtual.ToLower() == "teixeira")
                lblDB.Visible = true;

            clsUsuarioDados oUsuarioDados = new clsUsuarioDados();
            geral.CodigoUsuarioAtual = oUsuarioDados.PegaCodigoUsuario(geral.UsuarioAtual, oUsuarioDados.PegaSenha(geral.UsuarioAtual, 1), 1);

            eBotoes = Botoes.Nulo;

            bindingSource.DataSource = oClienteDados.PreencheSoNomeFantasia("");
            GradeClientes.DataSource = bindingSource.DataSource;
            EstiloGradeClientes();

            foreach (DataRow _dr in oAliquotaImpostosDados.PreencheDataTableRetencoes("CodigoBROOKS").Rows)
            {
                cboRetencoesNF.Items.Add(_dr["cdDesc"]);
            }
            cboRetencoesNF.SelectedIndex = 3;

            CriaControlesCorpoNotaFiscal();
            HabilitaOuDesabilitaCampos(false);            
            int _numeroNotaFiscal = oNotaFiscalDados.PegaUltimoNumeroNotaFiscal(true);
            oNotaFiscal = new clsNotasFiscais();
            oNotaFiscal = oNotaFiscalDados.PegaDados(oNotaFiscal, _numeroNotaFiscal);
            bindingSource.DataSource = oClienteDados.PreencheNomeFantasiaComCodigo(oNotaFiscal.CodigoCliente, false);
            GradeClientes.DataSource = bindingSource.DataSource;
            LimpaCampos();
            MostraDadosCliente();
            LeDadosCorpoNotaFiscal(_numeroNotaFiscal);
        }
        private void EstiloGradeClientes()
        {
            GradeClientes.Columns["Codigo"].Visible = false;
            GradeClientes.Columns["NomeFantasia"].HeaderText = "Nome fantasia";
            GradeClientes.Columns["NomeFantasia"].Width = 232;
        }
        private string SubstituirPalavraErrada(string pTextoErrado)
        {
            string _textoCerto = pTextoErrado;
            _textoCerto = _textoCerto.Replace("M?S", "MÊS");
            _textoCerto = _textoCerto.Replace("DESTINA??O", "DESTINAÇÃO");
            _textoCerto = _textoCerto.Replace("RES?DUOS", "RESÍDUOS");
            _textoCerto = _textoCerto.Replace("UTILIZA??O", "UTILIZAÇÃO");
            _textoCerto = _textoCerto.Replace("EST?CION?RIA", "ESTÁCIONÁRIA");
            _textoCerto = _textoCerto.Replace("QU?MICOS", "QUÍMICOS");
            _textoCerto = _textoCerto.Replace("SERVI?OS", "SERVIÇOS");
            _textoCerto = _textoCerto.Replace("AL?QUOTA", "ALÍQUOTA");
            _textoCerto = _textoCerto.Replace("RETEN??O", "RETENÇÃO");
            _textoCerto = _textoCerto.Replace("TRIBUT?RIA", "TRIBUTÁRIA");
            _textoCerto = _textoCerto.Replace("SE??O", "SEÇÃO");
            return _textoCerto;
        }
        private void LeDadosCorpoNotaFiscal(int pNumeroNotaFiscal)
        {
            int j = 0;
            if (pNumeroNotaFiscal > 0)
            {
                chkReterImpostosFederais.Checked = false;
                foreach (DataRow _dr in oCorpoNFDados.PreencheDataTable("Linha", pNumeroNotaFiscal.ToString()).Rows)
                {
                    _descricao[j].Text = SubstituirPalavraErrada(_dr["Descricao"].ToString());
                    if (_descricao[0].Text.IndexOf("709") > -1 || _descricao[0].Text.IndexOf("710") > -1)
                        chkReterImpostosFederais.Checked = true;
                    if (!btnNovo.Enabled && _descricao[j].Text.ToUpper().IndexOf("MÊS") > -1) 
                    {
                        _descricao[j].Text = PegaNovoMesDeReferencia(_descricao[j].Text);
                    }
                    if (_dr["Quantidade"].ToString() != "" && _dr["Quantidade"].ToString() != "0")
                        _quantidade[j].VALOR.Text = _dr["Quantidade"].ToString();
                    _unidade[j].Text = _dr["Unidade"].ToString();
                    if (_dr["PrecoUnitario"].ToString() != "" && _dr["PrecoUnitario"].ToString() != "0,0000")
                        _precounitario[j].VALOR.Text = _dr["PrecoUnitario"].ToString();
                    if (_dr["Valor"].ToString() != "" && _dr["Valor"].ToString() != "0,0000")
                        _valor[j].VALOR.Text = _dr["Valor"].ToString();
                    j++;
                }
                if (_descricao[1].Text.Length == 0)
                    _descricao[1].Text = "COLETA  E  DESTINAÇÃO  FINAL  DE  RESÍDUOS  COM  A";
                if (_descricao[2].Text.Length == 0)
                    _descricao[2].Text = "UTILIZAÇÃO DE CONTEINERES ESTACIONÁRIAS";
                if (_descricao[3].Text.Length == 0)
                    _descricao[3].Text = "REF. MÊS: " + DataReferencia.Value.AddMonths(-1) .Month.ToString("00") + "/" + DataReferencia.Value.Year.ToString() + " - CONTRATO";

            }
        }
        private void CriaControlesCorpoNotaFiscal()
        {
            int k = 2;
            for (int i = 0; i <= 21; i++)
            {
                _descricao[i] = new TextBox();
                _descricao[i].Name = "txtDescricao" + i.ToString();
                _descricao[i].Location = new System.Drawing.Point(4, (k * 18));
                _descricao[i].Multiline = false;
                _descricao[i].Size = new System.Drawing.Size(400, 18);
                _descricao[i].MaxLength = 60;
                _descricao[i].TabIndex = 0;
                _descricao[i].BackColor = Color.Beige;
                _descricao[i].Leave += new System.EventHandler(Descricao_Leave);
                grbItensNF.Controls.Add(_descricao[i]);

                _quantidade[i] = new MOEDA4();
                _quantidade[i].Name = "moeQuantidade" + i.ToString();
                _quantidade[i].Location = new System.Drawing.Point(406, (k * 18));
                _quantidade[i].Size = new System.Drawing.Size(97, 18);
                _quantidade[i].TabIndex = 0;
                _quantidade[i].Leave += new System.EventHandler(Calculo_Leave);
                grbItensNF.Controls.Add(_quantidade[i]);

                _unidade[i] = new TextBox();
                _unidade[i].Name = "txtUnidade" + i.ToString();
                _unidade[i].Location = new System.Drawing.Point(502, (k * 18));
                _unidade[i].Multiline = false;
                _unidade[i].Size = new System.Drawing.Size(34, 18);
                _unidade[i].MaxLength = 3;
                _unidade[i].TabIndex = 0;
                _unidade[i].BackColor = Color.Beige;
                grbItensNF.Controls.Add(_unidade[i]);

                _precounitario[i] = new MOEDA();
                _precounitario[i].Name = "moePrecounitario" + i.ToString();
                _precounitario[i].Location = new System.Drawing.Point(538, (k * 18));
                _precounitario[i].Size = new System.Drawing.Size(97, 18);
                _precounitario[i].TabIndex = 0;
                _precounitario[i].Leave += new System.EventHandler(Calculo_Leave);
                grbItensNF.Controls.Add(_precounitario[i]);

                _valor[i] = new MOEDA();
                _valor[i].Name = "moeValor" + i.ToString();
                _valor[i].Location = new System.Drawing.Point(628, (k * 18));
                _valor[i].Size = new System.Drawing.Size(97, 18);
                _valor[i].TabIndex = 0;
                grbItensNF.Controls.Add(_valor[i]);

                k++;
                
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            bindingSource.DataSource = oClienteDados.PreencheSoNomeFantasia(txtProcuraCliente.Text);
            GradeClientes.DataSource = bindingSource.DataSource;
        }

        private void cliente1_Enter(object sender, EventArgs e)
        {
            geral.VoltaForm = this.Name + "CLIENTE";
        }
        private void Descricao_Leave(object sender, EventArgs e)
        {
            if (_descricao[0].Text == "710")
                chkReterImpostosFederais.Checked = true;
            //MessageBox.Show("evento descricao");   
        }
        private void Calculo_Leave(object sender, EventArgs e)
        {
            decimal vt = 0;
            for (int i = 0; i <= 21; i++)
            {
                if (_precounitario[i].VALOR.Text != "" && _quantidade[i].VALOR.Text != "")
                {
                    _valor[i].VALOR.Text = Math.Round(Convert.ToDecimal(_precounitario[i].VALOR.Text) * Convert.ToDecimal(_quantidade[i].VALOR.Text), 2).ToString("N2").Replace(".", "");
                    vt = vt + Math.Round(Convert.ToDecimal(_precounitario[i].VALOR.Text) * Convert.ToDecimal(_quantidade[i].VALOR.Text), 2);
                }

            }
            moeValorTotal.VALOR.Text = vt.ToString("N2").Replace(".", "");

            if (moeAliquota.VALOR.Text != "")
                moeValorISS.VALOR.Text = Math.Round(vt * Convert.ToDecimal(moeAliquota.VALOR.Text) / 100, 2).ToString("N2").Replace(".", "");

            PegaAliquotaISS();

        }

        private void intNumeroNF_Leave(object sender, EventArgs e)
        {
            if (intNumeroNF.VALOR.Text != "" && intNumeroNF.VALOR.Text != "0")
            {
                oNotaFiscal = oNotaFiscalDados.PegaDados(oNotaFiscal, 0, Convert.ToInt32(intNumeroNF.VALOR.Text), true);
                LimpaCampos();
                MostraDadosCliente();
            }
        }

        private void LimpaCampos()
        {
            intNumeroLancamento.VALOR.Text = "";
            DataLancamento.Text = DateTime.Now.ToString("yyyy-MM-dd");
            DataEmissao.Text = "";
            DataReferencia.Text = "";
            DataVencimento.Text = "";
            cboRetencoesNF.Text = "";
            moeValorTotal.VALOR.Text = "";
            moeValorISS.VALOR.Text = "";
            moeValorLiquido.VALOR.Text = "";
            moeAliquota.VALOR.Text = "";
            moeValorISS.VALOR.Text = "";
            cboTipoDocumento.Text = "";
            intClassificacao.VALOR.Text = "";
            intContaGerencial.VALOR.Text = "";
            chkImportacaoRadar.Checked = false;
            chkReterImpostosFederais.Checked = false;
            moeDesconto.VALOR.Text = "";
            for (int i = 0; i <= 21; i++)
            {
                _descricao[i].Text = "";
                _quantidade[i].VALOR.Text = "";
                _unidade[i].Text = "";
                _precounitario[i].VALOR.Text = "";
                _valor[i].VALOR.Text = "";
            }
            if (_descricao[1].Text.Length == 0)
                _descricao[1].Text = "COLETA E DESTINAÇÃO FINAL DE RESÍDUOS COM A";
            if (_descricao[2].Text.Length == 0)
                _descricao[2].Text = "UTILIZAÇÃO DE CONTEINERES ESTACIONÁRIAS";
            if (_descricao[3].Text.Length == 0)
                _descricao[3].Text = "REF. MÊS: " + DataReferencia.Value.AddMonths(-1).Month.ToString("00") + "/" + DataReferencia.Value.Year.ToString() + " - CONTRATO";

            lblPorExtenso.Text = "";
            intDiasEntreVctos.VALOR.Text = "15";
            intParcelas.VALOR.Text = "1";
            txtHistorico.Text = geral.RemoverAcentos("REF. MES: " + DataReferencia.Value.AddMonths(-1).Month.ToString("00") + "/" + DataReferencia.Value.Year.ToString());
            cboTipoCobranca.Text = "";
            cboTipoCobranca.Items.Clear();
            oTipoCobrancaDados = new clsTipoCobrancaDados();
            foreach(DataRow dr in oTipoCobrancaDados.PreencheDataTable("Codigo").Rows)
            {
                cboTipoCobranca.Items.Add(Convert.ToInt16(dr[0]).ToString("00") + "-" + dr[1].ToString());
            }
            cboTipoCobranca.SelectedIndex = 0;
        }

        private void HabilitaOuDesabilitaCampos(bool pAtiva)
        {
            intNumeroLancamento.VALOR.Enabled = pAtiva;
            DataEmissao.Enabled = pAtiva;
            DataReferencia.Enabled = pAtiva;
            DataVencimento.Enabled = pAtiva;
            cboRetencoesNF.Enabled = pAtiva;
            chkReterImpostosFederais.Enabled = pAtiva;
            moeValorTotal.VALOR.Enabled = pAtiva;
            moeValorISS.VALOR.Enabled = pAtiva;
            moeValorLiquido.VALOR.Enabled = pAtiva;
            moeAliquota.VALOR.Enabled = pAtiva;
            moeValorISS.VALOR.Enabled = pAtiva;
            cboTipoDocumento.Enabled = pAtiva;
            intDiasEntreVctos.VALOR.Enabled = pAtiva;
            intParcelas.VALOR.Enabled = pAtiva;
            intClassificacao.VALOR.Enabled = pAtiva;
            intContaGerencial.VALOR.Enabled = pAtiva;
            chkImportacaoRadar.Enabled = pAtiva;
            txtHistorico.Enabled = pAtiva;
            moeDesconto.Enabled = pAtiva;
            for (int i = 0; i <= 21; i++)
            {
                _descricao[i].Enabled = pAtiva;
                _quantidade[i].Enabled = pAtiva;
                _unidade[i].Enabled = pAtiva;
                _precounitario[i].VALOR.Enabled = pAtiva;
                _valor[i].VALOR.Enabled = pAtiva;
            }
            btnNovo.Enabled = !pAtiva;
            btnSalvar.Enabled = pAtiva;
            if (geral.CodigoUsuarioAtual == 10)
                btnSalvar.Enabled = true;
            btnFatura.Enabled = pAtiva;
            btnAnular.Enabled = pAtiva;
            GradeClientes.Enabled = !pAtiva;
            btnCancelar.Enabled = !pAtiva;

            btnAnterior.Enabled = !pAtiva;
            btnProximo.Enabled = !pAtiva;
        }

        private void SetVencimento()
        {
            string sDtVcto = "";
            int nuDiaVcto = 0;
            clsContratosDados oContratoDados = new clsContratosDados();
            if (cliente1.txtCodigo.Text != "")
                nuDiaVcto = oContratoDados.PegaDiaDoVencimento(Convert.ToInt32(cliente1.txtCodigo.Text));
            if (DateTime.Now.Day + 7 <= nuDiaVcto)
            {
                if (DateTime.Now.Month == 2 && nuDiaVcto > 28)
                    sDtVcto = Convert.ToDateTime("28/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year).ToString("dd/MM/yyyy");
                else
                    sDtVcto = Convert.ToDateTime(nuDiaVcto.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year).ToString("dd/MM/yyyy");
                DataVencimento.Value = Convert.ToDateTime(sDtVcto);
            }
            else
            {
                DataVencimento.Value = DateTime.Now.AddDays(7);
            }
            if (geral.Left(cboRetencoesNF.Text, 1) == "H")
            {
                cboTipoDocumento.Text = "5-Fatura";
                cboTipoDocumento.Focus();
            }
        }

        private void MostraDadosCliente()
        {
            if (oNotaFiscal.CodigoCliente > 0)
            {
                intNumeroLancamento.VALOR.Text = oNotaFiscal.NumeroNotaFiscal.ToString();
                intNumeroNF.VALOR.Text = oNotaFiscal.NumeroNF.ToString();
                LeDadosCorpoNotaFiscal(oNotaFiscal.NumeroNotaFiscal);
                if (btnNovo.Enabled && oNotaFiscal.DataEmissao != null && oNotaFiscal.DataEmissao != "")
                    DataLancamento.Text = Convert.ToDateTime(oNotaFiscal.DataEmissao).ToString("yyyy-MM-dd");
                DataEmissao.Text = oNotaFiscal.DataEmissao;
                DataReferencia.Text = oNotaFiscal.DataReferencia;
                DataVencimento.Text = oNotaFiscal.Vencimento;

                cliente1.txtCodigo.Text = oNotaFiscal.CodigoCliente.ToString();
                oCliente = oClienteDados.PegaDados(oCliente, oNotaFiscal.CodigoCliente);

                if (oCliente.Nome != "" && oCliente.Nome != null)
                    cliente1.txtDescricao.Text = oCliente.Nome;
                else
                {
                    MessageBox.Show("Cliente inválido!");
                    cliente1.Focus();
                    return;
                }
                if (oCliente.CodigoBROOKS_Retencoes != "")
                    cboRetencoesNF.Text = geral.Left(oCliente.CodigoBROOKS_Retencoes, 1) + "-" + oRetencoesDados.PegaDescricao(geral.Left(oCliente.CodigoBROOKS_Retencoes, 1));
                else 
                {
                    if (!btnNovo.Enabled)
                    {
                        MessageBox.Show("Parâmetro da alíquota do Imposto Federal no cadastro de cliente está inválido!");
                        cliente1.Focus();
                        cliente1.txtCodigo.Text = "";
                        cliente1.txtDescricao.Text = "";
                        return;
                    }
                }
                    
                if (oNotaFiscal.CodigoBROOKS_Impostos != "" && oNotaFiscal.CodigoBROOKS_Impostos != null)
                    cboRetencoesNF.Text = oNotaFiscal.CodigoBROOKS_Impostos + "-" + oRetencoesDados.PegaDescricao(oNotaFiscal.CodigoBROOKS_Impostos);

                moeValorTotal.VALOR.Text = oNotaFiscal.ValorTotal.ToString("N2").Replace(".", "");
                moeValorISS.VALOR.Text = oNotaFiscal.ValorISS.ToString("N2").Replace(".", "");

                if (oCliente.CNPJ_CPF != null && oCliente.CNPJ_CPF != "")
                {
                    if (geral.RetiraCharsCNPJCPF(oCliente.CNPJ_CPF).Length < 12)
                    {
                        oNotaFiscal.ValorISS = 0;
                        ValorISS = 0;
                        moeValorISS.VALOR.Text = "0,00";
                    }
                }
                // não diminuir valor do iss pois é fatura
                if (geral.Left(cboTipoDocumento.Text, 1) == "5") // fatura
                {                    
                    if (chkReterImpostosFederais.Checked)
                        moeValorLiquido.VALOR.Text = (oNotaFiscal.ValorTotal - oNotaFiscal.ValorCOFINS - oNotaFiscal.ValorContrSocial - oNotaFiscal.ValorPIS - oNotaFiscal.ValorINSS - oNotaFiscal.ValorIRRF).ToString("N2").Replace(".", "");
                    else
                        moeValorLiquido.VALOR.Text = (oNotaFiscal.ValorTotal - oNotaFiscal.ValorISS).ToString("N2").Replace(".", "");
                }
                else
                {
                    oEndereco = oEnderecoDados.PegaDados(oEndereco, Convert.ToInt32(cliente1.txtCodigo.Text), 2, 0);

                    if (oEndereco.CodigoMunicipio == 83275 && oNotaFiscal.CodigoBROOKS_Impostos == "G")
                        oNotaFiscal.ValorISS = 0;
                    if (chkReterImpostosFederais.Checked)
                        moeValorLiquido.VALOR.Text = (oNotaFiscal.ValorTotal - oNotaFiscal.ValorCOFINS - oNotaFiscal.ValorContrSocial - oNotaFiscal.ValorISS - oNotaFiscal.ValorPIS - oNotaFiscal.ValorINSS - oNotaFiscal.ValorIRRF).ToString("N2").Replace(".", "");
                    else
                        moeValorLiquido.VALOR.Text = (oNotaFiscal.ValorTotal - oNotaFiscal.ValorISS - oNotaFiscal.ValorINSS).ToString("N2").Replace(".", "");
                }
                moeAliquota.VALOR.Text = oNotaFiscal.PercentualISS.ToString("N2").Replace(".", "");
                if (oNotaFiscal.PercentualISS > 0 && oNotaFiscal.ValorISS == 0)
                    moeValorISS.VALOR.Text = (oNotaFiscal.PercentualISS * oNotaFiscal.ValorTotal / 100).ToString("N2").Replace(".", "");
                else if (oNotaFiscal.ValorISS > 0)
                    moeValorISS.VALOR.Text = oNotaFiscal.ValorISS.ToString("N2").Replace(".", "");
                moeDesconto.VALOR.Text = oNotaFiscal.ValorDescontoIncondicionado.ToString("N2").Replace(".", "");
                if (oNotaFiscal.TipoDocumento == 4)
                    cboTipoDocumento.Text = "4-Recibo";
                else if (oNotaFiscal.TipoDocumento == 5)
                {
                    cboTipoDocumento.Text = "5-Fatura";
                    btnFatura.Enabled = true;
                }
                else if (oNotaFiscal.TipoDocumento == 8)
                    cboTipoDocumento.Text = "8-NF";

                if (oNotaFiscal.DiasEntreVctos <= 0)
                { 
                    intDiasEntreVctos.VALOR.Text = "15"; 
                }
                else
                    intDiasEntreVctos.VALOR.Text = oNotaFiscal.DiasEntreVctos.ToString();
                if (oNotaFiscal.NumeroParcelas == 0)
                    intParcelas.VALOR.Text = "1";
                else
                    intParcelas.VALOR.Text = oNotaFiscal.NumeroParcelas.ToString();
                if (oNotaFiscal.Classificacao > 0)
                    intClassificacao.VALOR.Text = oNotaFiscal.Classificacao.ToString();
                else
                    intClassificacao.VALOR.Text = oCliente.Classificacao.ToString();

                if (oNotaFiscal.ContaGerencial > 0)
                    intContaGerencial.VALOR.Text = oNotaFiscal.ContaGerencial.ToString();
                else
                    intContaGerencial.VALOR.Text = oCliente.ContaGerencial.ToString();
                if (oNotaFiscal.ArquivoParaImportacaoGerado == 1)
                    chkImportacaoRadar.Checked = true;
                else
                    chkImportacaoRadar.Checked = false;
                txtHistorico.Text = geral.RemoverAcentos(oNotaFiscal.Historico);
                if (!btnNovo.Enabled)
                {
                    txtHistorico.Text = geral.RemoverAcentos(PegaNovoMesDeReferencia(oNotaFiscal.Historico));
                }
                if (oCliente.CodigoTipoCobranca > 0)
                {
                    oTipoCobranca = new clsTipoCobranca();
                    oTipoCobrancaDados = new clsTipoCobrancaDados();
                    oTipoCobrancaDados.PegaDados(oTipoCobranca, oCliente.CodigoTipoCobranca);
                    cboTipoCobranca.Text = oCliente.CodigoTipoCobranca.ToString("00") + "-" + oTipoCobranca.Descricao;
                }
                if (oNotaFiscal.Historico == null)
                    oNotaFiscal.Historico = "";
                if (oNotaFiscal.Historico.Length == 0)
                    oNotaFiscal.Historico = "REF. MÊS: " + DataReferencia.Value.AddMonths(-1).Month.ToString("00") + "/" + DataReferencia.Value.Year.ToString();
                if (oNotaFiscal.Cancelada == 1)
                    btnCancelar.Text = "Cancelada";
                else
                    btnCancelar.Text = "Cancelar";
                lblPorExtenso.Text = oNotaFiscal.ValorPorExtenso;
            }
            else if (oNotaFiscal.CodigoCliente == 0 && cliente1.txtCodigo.Text != "")
            {
                oClienteDados.PegaDados(oCliente, Convert.ToInt32(cliente1.txtCodigo.Text));
                intClassificacao.VALOR.Text = oCliente.Classificacao.ToString();
                intContaGerencial.VALOR.Text = oCliente.ContaGerencial.ToString();
                cboRetencoesNF.Text = oCliente.CodigoBROOKS_Retencoes;
                if (oCliente.CodigoTipoCobranca > 0)
                {
                    oTipoCobranca = new clsTipoCobranca();
                    oTipoCobrancaDados = new clsTipoCobrancaDados();
                    oTipoCobrancaDados.PegaDados(oTipoCobranca, oCliente.CodigoTipoCobranca);
                    cboTipoCobranca.Text = oCliente.CodigoTipoCobranca.ToString("00") + "-" + oTipoCobranca.Descricao;
                }
            }
            if (geral.Left(cboTipoDocumento.Text, 1) == "4")
                btnRecibo.Enabled = true;
            else
                btnRecibo.Enabled = false;
            if (geral.Left(cboTipoDocumento.Text, 1) == "5")
                btnFatura.Enabled = true;
            else
                btnFatura.Enabled = false;
            MostrarEnderecoFaturamento();

            SelecionaSituacaoTributaria();

        }

        private string PegaNovoMesDeReferencia(string pHistorico)
        {
            string sRet = "";
            int nColPos = 0;
            if (pHistorico != null && pHistorico != "")
            {
                nColPos = pHistorico.ToUpper().IndexOf("MÊS: ") + 5;
                if (nColPos <= 5)
                {
                    nColPos = pHistorico.ToUpper().IndexOf("MÊS ") + 5;
                    pHistorico = pHistorico.ToUpper().Replace("MÊS ", "MÊS: ");
                    if (nColPos <= 5)
                    {
                        nColPos = pHistorico.ToUpper().IndexOf("MÊS:") + 5;
                        pHistorico = pHistorico.ToUpper().Replace("MÊS:", "MÊS: ");
                    }
                }
                string sMesHistorico = (DateTime.Now.Month - 1).ToString("00");
                if (DateTime.Now.Month == 1)
                    sMesHistorico = "12";
                if (pHistorico.Length > nColPos)
                {
                    sMesHistorico = pHistorico.Substring(nColPos, 2).Trim();
                    if (sMesHistorico.Length <= 1)
                        sMesHistorico = pHistorico.Substring(nColPos+1, 2).Trim();
                    if (sMesHistorico == "12")
                    {
                        sRet = pHistorico.Replace("MÊS: " + pHistorico.Substring(nColPos, 7), "MÊS: 01/" + DateTime.Now.Year.ToString());
                    }
                    else if (sMesHistorico.Length > 0 && geral.IsNumeric(sMesHistorico))
                    {
                        sRet = pHistorico.Replace("MÊS: " + sMesHistorico, "MÊS: " + (Convert.ToInt32(sMesHistorico) + 1).ToString("00"));
                        if (pHistorico.IndexOf("MÊS: " + sMesHistorico) == -1)
                            sRet = pHistorico.Replace("MÊS:  " + sMesHistorico, "MÊS: " + (Convert.ToInt32(sMesHistorico) + 1).ToString("00"));
                    }
                }
                else
                    sRet = "REF. MÊS: " + sMesHistorico + "/" + (DateTime.Now.Year - 1).ToString();
            }
            return sRet;
        }
        private void MostrarEnderecoFaturamento()
        {
            if (cliente1.txtCodigo.Text != "" && cliente1.txtCodigo.Text != "0")
            {
                oEndereco = oEnderecoDados.PegaDados(oEndereco, Convert.ToInt32(cliente1.txtCodigo.Text), 1, 0);
                txtEnderecoFaturamento.Text = oEndereco.endereco;
                if (oEndereco.Numero != "")
                    txtEnderecoFaturamento.Text = txtEnderecoFaturamento.Text + " - " + oEndereco.Numero;
                if (oEndereco.Complemento != "")
                    txtEnderecoFaturamento.Text = txtEnderecoFaturamento.Text + " - " + oEndereco.Complemento;
                if (oEndereco.Bairro != "")
                    txtEnderecoFaturamento.Text = txtEnderecoFaturamento.Text + " - " + oEndereco.Bairro;
                if (oEndereco.CEP != "")
                    txtEnderecoFaturamento.Text = txtEnderecoFaturamento.Text + " - " + oEndereco.CEP;
                oMunicipio = oMunicipioDados.PegaDados(oMunicipio, oEndereco.CodigoMunicipio);
                txtEnderecoFaturamento.Text = txtEnderecoFaturamento.Text + " - " + oMunicipio.Nome;
                txtEnderecoFaturamento.Text = txtEnderecoFaturamento.Text + " - " + oMunicipio.UF;
                if (oEndereco.Fone1 != "")
                    txtEnderecoFaturamento.Text = txtEnderecoFaturamento.Text + " - " + oEndereco.DDD1 + " " + oEndereco.Fone1;

                lblInstrFat.Text = oEndereco.InstrucoesFat;
                
                lblDocAplicavel.Text = "";
                clsDocumentacaoAplicavel oDocAplicavel = new clsDocumentacaoAplicavel();
                clsDocumentacaoAplicavelDados oDocAplicavelDados = new clsDocumentacaoAplicavelDados();
                oDocAplicavel = oDocAplicavelDados.PegaDados(3, 2022, oNotaFiscal.CodigoCliente);
                if (oDocAplicavel.EnviarPlanFatAteDia > 0)
                {
                    lblDocAplicavel.Text = lblDocAplicavel.Text + "Enviar PLANFAT ";
                    lblDocAplicavel.Text = lblDocAplicavel.Text + " até " + oDocAplicavel.EnviarPlanFatAteDia.ToString() + "\n";
                }
                if (oDocAplicavel.AguardarAprovacaoPlanFat > 0)
                {
                    lblDocAplicavel.Text = lblDocAplicavel.Text + "Cliente Aprova ";
                    lblDocAplicavel.Text = lblDocAplicavel.Text + " até " + oDocAplicavel.AguardarAprovacaoPlanFat.ToString() + "\n";
                }
                if (oDocAplicavel.EnviarRelGer > 0)
                    lblDocAplicavel.Text = lblDocAplicavel.Text + "Enviar RELGER \n";
                if (oDocAplicavel.AguardarOrdemCompra > 0)
                    lblDocAplicavel.Text = lblDocAplicavel.Text + "Cliente Envia OC " + oDocAplicavel.AguardarOrdemCompra.ToString() + "\n";
            }
        }
        private void GradeClientes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (GradeClientes.Rows[e.RowIndex].Cells["Codigo"].Value.ToString() != "")
                {
                    intNumeroLancamento.VALOR.Text = "";
                    intNumeroNF.VALOR.Text = "";
                    cliente1.txtCodigo.Text = "";
                    geral.CodigoCliente = 0;
                    cliente1.txtDescricao.Text = "";
                    LimpaCampos();
                    oNotaFiscal = oNotaFiscalDados.PegaDados(oNotaFiscal, Convert.ToInt32(GradeClientes.Rows[e.RowIndex].Cells["Codigo"].Value.ToString()), 0, true);
                    MostraDadosCliente();
                    btnGeraArquivoExport.Enabled = true;
                }
            }
        }
        private void cliente1_Leave(object sender, EventArgs e)
        {
            LimpaCampos();
            oClienteFatCNPJ_Diferente = new clsClientes();
            if (cliente1.txtCodigo.Text != "" && cliente1.txtCodigo.Text != "0")
            {
                oCliente = new clsClientes();
                oClienteDados.PegaDados(oCliente, Convert.ToInt32(cliente1.txtCodigo.Text));
                if (oCliente.CNPJ_Faturamento != "")
                {
                    oClienteDados.PegaDados(oClienteFatCNPJ_Diferente, Convert.ToInt32(cliente1.txtCodigo.Text));
                    // guardar endereço (cidade/sc) onde foi realizado o serviço para calcular o imposto
                    MessageBox.Show("O faturamento será realizado para o CNPJ: " + oCliente.CNPJ_Faturamento);
                    oCliente = oClienteDados.PegaDadosPeloCNPJFaturamento(oCliente, oCliente.CNPJ_Faturamento);
                    cliente1.txtCodigo.Text = oCliente.Codigo.ToString();

                    // mudar o codigo do municipio de faturamento - quando é faturamento com cnpj diferente - alterado 11/04/2024
                    // inicio
                    if (oCliente.Codigo != oClienteFatCNPJ_Diferente.Codigo && oClienteFatCNPJ_Diferente.Codigo > 0)
                    {
                        oEndereco = oEnderecoDados.PegaDados(oEndereco, oClienteFatCNPJ_Diferente.Codigo, 2, 0);
                        oMunicipio = oMunicipioDados.PegaDados(oMunicipio, oEndereco.CodigoMunicipio);
                        PegaAliquotaISS();
                        CalcularImpostos();
                    }
                    // fim
                }
            }
            if (cliente1.txtCodigo.Text != "" && cliente1.txtCodigo.Text != "0" && eBotoes == Botoes.Novo)
            {
                bindingSource.DataSource = oClienteDados.PreencheSoNomeFantasia(cliente1.txtDescricao.Text, false);
                GradeClientes.DataSource = bindingSource.DataSource;
                LimpaCampos();

                oNotaFiscal = oNotaFiscalDados.PegaDados(oNotaFiscal, Convert.ToInt32(cliente1.txtCodigo.Text), 0, true);
                oNotaFiscal.CodigoCliente = Convert.ToInt32(cliente1.txtCodigo.Text);
                MostraDadosCliente();
                PegaAliquotaISS();

                DataEmissao.Text = DateTime.Now.ToShortDateString();
                DataReferencia.Text = DateTime.Now.ToShortDateString();
                PegaDataReferencia();
                intNumeroNF.VALOR.Text = "";
                if (!btnNovo.Enabled)
                    SetVencimento();
                btnFatura.Enabled = false;
            }
            else if(cliente1.txtCodigo.Text != "" && cliente1.txtCodigo.Text != "0")
            {
                bindingSource.DataSource = oClienteDados.PreencheSoNomeFantasia(cliente1.txtDescricao.Text, false);
                GradeClientes.DataSource = bindingSource.DataSource;
                MostrarEnderecoFaturamento();

                LimpaCampos();
                oNotaFiscal = oNotaFiscalDados.PegaDados(oNotaFiscal, Convert.ToInt32(cliente1.txtCodigo.Text), 0, true);
                oNotaFiscal.CodigoCliente = Convert.ToInt32(cliente1.txtCodigo.Text);
                MostraDadosCliente();
            }


        }
        private void PegaDataReferencia()
        {
            //Solicitação do Sérgio em 10/03/2015
            DateTime _dataemissao = new DateTime();
            _dataemissao = DataEmissao.Value;
            if (_dataemissao.Day <= 10)
            {
                if (_dataemissao.Month == 1)
                    DataReferencia.Value = Convert.ToDateTime("31/12/" + (_dataemissao.Year - 1).ToString());
                else
                {
                    string _ultimodiames = "";
                    if (_dataemissao.Month > 1)
                        _ultimodiames = DateTime.DaysInMonth(_dataemissao.Year, _dataemissao.Month - 1).ToString();
                    DataReferencia.Value = Convert.ToDateTime(_ultimodiames + "/" + (_dataemissao.Month - 1).ToString("00") + "/" + _dataemissao.Year.ToString());
                }
            }
            else
            {
                DataReferencia.Value = _dataemissao;
            }
        }

        private void btnMaisNFs_Click(object sender, EventArgs e)
        {
            if (cliente1.txtCodigo.Text != "" && cliente1.txtCodigo.Text != "0")
            {
                frmMaisNFs o_frmMaisNFs = new frmMaisNFs();
                o_frmMaisNFs.txtCodigo.Text = cliente1.txtCodigo.Text;
                o_frmMaisNFs.txtNomeCliente.Text = cliente1.txtDescricao.Text;
                o_frmMaisNFs.ShowDialog();
                LimpaCampos();
                geral.CodigoCliente = Convert.ToInt32(cliente1.txtCodigo.Text);
                oNotaFiscal = oNotaFiscalDados.PegaDados(oNotaFiscal, geral.CodigoCliente, geral.NumeroNF, false);
                MostraDadosCliente();
                geral.CodigoCliente = 0;
                geral.NumeroNF = 0;
                btnGeraArquivoExport.Enabled = true;
            }
            else
                MessageBox.Show("Código do cliente inválido!");
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            HabilitaOuDesabilitaCampos(true);
            btnFatura.Enabled = false;
            LimpaCampos();
            intNumeroNF.VALOR.Text = "";
            intNumeroNF.Enabled = false;
            eBotoes = Botoes.Novo;
            cliente1.txtCodigo.Focus();
            chkReterImpostosFederais.Checked = false;
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            HabilitaOuDesabilitaCampos(false);
            int _numeroNotaFiscal = oNotaFiscalDados.PegaUltimoNumeroNotaFiscal(true);
            oNotaFiscal = new clsNotasFiscais();
            oNotaFiscal = oNotaFiscalDados.PegaDados(oNotaFiscal, _numeroNotaFiscal);
            bindingSource.DataSource = oClienteDados.PreencheNomeFantasiaComCodigo(oNotaFiscal.CodigoCliente, false);
            GradeClientes.DataSource = bindingSource.DataSource;
            LimpaCampos();
            MostraDadosCliente();
            LeDadosCorpoNotaFiscal(_numeroNotaFiscal);
            eBotoes = Botoes.Anular;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            bool bCorpoNotaValido = false;
            for (int i = 0; i <= 16; i++)
            {
                if (_descricao[i].Text != "" && _quantidade[i].VALOR.Text != "" && _precounitario[i].VALOR.Text != "" && _valor[i].VALOR.Text != "")
                    bCorpoNotaValido = true;
            }

            if (cliente1.txtCodigo.Text == "")
            {
                MessageBox.Show("Código ou cliente inválido!");
                cliente1.Focus();
            }
            else if (txtHistorico.Text == "" || txtHistorico.Text == null)
            {
                MessageBox.Show("Histórico inválido!");
                txtHistorico.Focus();
            }
            else if (!bCorpoNotaValido)
            {
                MessageBox.Show("Descrição do serviços no corpo da NF estão inválidos!");
                _descricao[0].Focus();
            }
            else if (File.Exists("c:\\eletron\\work\\rps.xml") && cboTipoDocumento.Text.IndexOf("8") > -1)
            {
                MessageBox.Show("Não é possível salvar, pois existe arquivo rps.xml na pasta \\eletron\\work\\ da unidade C:");
            }
            else
            {
                HabilitaOuDesabilitaCampos(false);
                intNumeroNF.Enabled = true;
                eBotoes = Botoes.Salvar;
                oNotaFiscal.Cancelada = 0;
                oNotaFiscal.Classificacao = Convert.ToInt32(intClassificacao.VALOR.Text);
                oNotaFiscal.CodigoBROOKS_Impostos = cboRetencoesNF.Text[0].ToString();
                oNotaFiscal.CodigoCliente = Convert.ToInt32(cliente1.txtCodigo.Text);
                oNotaFiscal.ContaGerencial = Convert.ToInt32(intContaGerencial.VALOR.Text);
                oNotaFiscal.DataEmissao = DataEmissao.Text;
                oNotaFiscal.DataReferencia = DataReferencia.Text;
                if (intDiasEntreVctos.VALOR.Text != "")
                    oNotaFiscal.DiasEntreVctos = Convert.ToInt32(intDiasEntreVctos.VALOR.Text);
                oNotaFiscal.Historico = txtHistorico.Text;
                oNotaFiscal.NFImpressa = 0;

                if (geral.Left(cboTipoDocumento.Text, 1) == "8")      // nf
                {
                    if (geral.CodigoUsuarioAtual == 10)
                    {
                        DialogResult _drPegarNovo;
                        _drPegarNovo = MessageBox.Show("Edson gerar novo número da nota fiscal?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (_drPegarNovo == DialogResult.Yes)
                            oNotaFiscal.NumeroNF = oNotaFiscalDados.PegaUltimoNumeroNF() + 1;
                    }
                    else
                        oNotaFiscal.NumeroNF = oNotaFiscalDados.PegaUltimoNumeroNF() + 1;
                }
                else if (geral.Left(cboTipoDocumento.Text, 1) == "4") // recibo
                    oNotaFiscal.NumeroNF = oNotaFiscalDados.PegaUltimoNumeroRecibo() + 1;
                else if (geral.Left(cboTipoDocumento.Text, 1) == "5") // fatura
                    oNotaFiscal.NumeroNF = oNotaFiscalDados.PegaUltimoNumeroFatura() + 1;

                if (intParcelas.VALOR.Text != "")
                    oNotaFiscal.NumeroParcelas = Convert.ToInt32(intParcelas.VALOR.Text);
                oNotaFiscal.NumeroRecibo = 0;
                oRetencao = new clsAliquotaImpostos();
                if (cboRetencoesNF.Text != "" && (_descricao[0].Text.IndexOf("709") > -1 || _descricao[0].Text.IndexOf("710") > -1 || chkReterImpostosFederais.Checked))
                    oRetencao = oRetencoesDados.PegaAliquotas(oRetencao, cboRetencoesNF.Text[0].ToString());

                oNotaFiscal.ValorPIS = ValorPIS;
                oNotaFiscal.ValorCOFINS = ValorCOFINS;
                oNotaFiscal.ValorContrSocial = ValorContrSocial;
                oNotaFiscal.PercentualCRF = oRetencao.AliquotaPIS_Retido + oRetencao.AliquotaCOFINS_Retido + oRetencao.AliquotaContribSocial;

                oNotaFiscal.ValorBaseINSS = 0;
                oNotaFiscal.PercentualINSS = 0;
                oNotaFiscal.PercentualIRRF = 0;
                if (moeValorBaseCalculoINSS.VALOR.Text != "")
                    oNotaFiscal.ValorBaseINSS = Convert.ToDecimal(moeValorBaseCalculoINSS.VALOR.Text);
                if (moePercINSS.VALOR.Text != "")
                    oNotaFiscal.PercentualINSS = Convert.ToDecimal(moePercINSS.VALOR.Text);
                if (moeIRRFperc.VALOR.Text != "")
                    oNotaFiscal.PercentualIRRF = Convert.ToDecimal(moeIRRFperc.VALOR.Text);
                if (moeAliquota.VALOR.Text != "")
                    oNotaFiscal.PercentualISS = Convert.ToDecimal(moeAliquota.VALOR.Text);
                oNotaFiscal.ValorDescontoIncondicionado = 0;
                if (moeDesconto.VALOR.Text != "")
                    oNotaFiscal.ValorDescontoIncondicionado = Convert.ToDecimal(moeDesconto.VALOR.Text);
                oNotaFiscal.TipoDocumento = Convert.ToInt16(cboTipoDocumento.Text[0].ToString());
                if (moeValorISS.VALOR.Text != "")
                    oNotaFiscal.ValorISS = Convert.ToDecimal(moeValorISS.VALOR.Text);
                oNotaFiscal.ValorPorExtenso = "";
                if (moeValorTotal.VALOR.Text != "")
                    oNotaFiscal.ValorTotal = Convert.ToDecimal(moeValorTotal.VALOR.Text);
                oNotaFiscal.Vencimento = DataVencimento.Text;

                oNotaFiscal.ValorPorExtenso = oMoeda.EscreverExtenso(Convert.ToDecimal(moeValorLiquido.VALOR.Text));
                string sLog = "";
                try
                {
                    oNotaFiscal.NumeroNotaFiscal = 0; // tem que ser igual 0 para inserir
                    intNumeroLancamento.VALOR.Text = oNotaFiscalDados.Inserir(oNotaFiscal).ToString();
                    sLog = sLog + "Nº NF: " + oNotaFiscal.NumeroNF + " \n";
                    sLog = sLog + "Data Emissão: " + oNotaFiscal.DataEmissao + " \n";
                    sLog = sLog + "Código Cliente: " + oNotaFiscal.CodigoCliente + " \n";
                    sLog = sLog + "Nome Cliente: " + oNotaFiscal.NomeCliente + " \n";
                    sLog = sLog + "Código BROOKS Retenção: " + oNotaFiscal.CodigoBROOKS_Impostos + " \n";
                    sLog = sLog + "Conta Gerencial: " + oNotaFiscal.ContaGerencial + " \n";
                    sLog = sLog + "Classificação: " + oNotaFiscal.Classificacao + " \n";
                    sLog = sLog + "Data Referência: " + oNotaFiscal.DataReferencia + " \n";
                    sLog = sLog + "Vencimento: " + oNotaFiscal.Vencimento + " \n";
                    sLog = sLog + "Dias Entre Vctos: " + oNotaFiscal.DiasEntreVctos + " \n";
                    sLog = sLog + "Histórico: " + oNotaFiscal.Historico + " \n";
                    sLog = sLog + "Nº Parcelas: " + oNotaFiscal.NumeroParcelas + " \n";
                    sLog = sLog + "Nº Recibo: " + oNotaFiscal.NumeroRecibo + " \n";
                    sLog = sLog + "Percentual CRF: " + oNotaFiscal.PercentualCRF + " \n";
                    sLog = sLog + "Percentual INSS: " + oNotaFiscal.PercentualINSS + " \n";
                    sLog = sLog + "Percentual IRRF: " + oNotaFiscal.PercentualIRRF + " \n";
                    sLog = sLog + "Percentual ISS: " + oNotaFiscal.PercentualISS + " \n";
                    sLog = sLog + "TipoDocumento: " + oNotaFiscal.TipoDocumento + " \n";
                    sLog = sLog + "Valor Base INSS: " + oNotaFiscal.ValorBaseINSS + " \n";
                    sLog = sLog + "Valor COFINS: " + oNotaFiscal.ValorCOFINS + " \n";
                    sLog = sLog + "Valor Contribuição Social: " + oNotaFiscal.ValorContrSocial + " \n";
                    sLog = sLog + "Valor Desconto: " + oNotaFiscal.ValorDescontoIncondicionado + " \n";
                    sLog = sLog + "Valor INSS: " + oNotaFiscal.ValorINSS + " \n";
                    sLog = sLog + "Valor IRRF: " + oNotaFiscal.ValorIRRF + " \n";
                    sLog = sLog + "Valor ISS: " + oNotaFiscal.ValorISS + " \n";
                    sLog = sLog + "Valor PIS: " + oNotaFiscal.ValorPIS + " \n";
                    sLog = sLog + "Valor Total: " + oNotaFiscal.ValorTotal + " \n";
                    if (geral.Left(cboTipoDocumento.Text, 1) == "8")      // nf
                        oNotaFiscal.NumeroNF = oNotaFiscalDados.PegaUltimoNumeroNF();
                    else if (geral.Left(cboTipoDocumento.Text, 1) == "4") // recibo
                        oNotaFiscal.NumeroNF = oNotaFiscalDados.PegaUltimoNumeroRecibo();
                    else if (geral.Left(cboTipoDocumento.Text, 1) == "5") // fatura
                        oNotaFiscal.NumeroNF = oNotaFiscalDados.PegaUltimoNumeroFatura();
                    intNumeroNF.VALOR.Text = oNotaFiscal.NumeroNF.ToString();
                }
                finally
                {
                    if (intNumeroLancamento.VALOR.Text != "" && intNumeroLancamento.VALOR.Text != "0")
                    {
                        SalvarCorpoNotaFiscal(Convert.ToInt32(intNumeroLancamento.VALOR.Text), sLog);
                        if (cboTipoDocumento.Text[0].ToString() == "8") // nota fiscal
                        {
                            if (!File.Exists("c:\\eletron\\work\\rps.xml"))
                            {
                                EnviarOuCancelarNF(false);
                                frmBrowserNF o_frmNF = new frmBrowserNF();
                                o_frmNF.intNNF.Visible = false;
                                o_frmNF.label1.Visible = false;
                                geral.NotaFiscalEnviada = false;
                                o_frmNF.cliente1Codigo = cliente1.txtCodigo.Text;
                                o_frmNF.CodigoClienteQueVaiEmail = "";

                                if (oClienteFatCNPJ_Diferente.Codigo.ToString() != cliente1.txtCodigo.Text && oClienteFatCNPJ_Diferente.Codigo > 0)
                                    o_frmNF.CodigoClienteQueVaiEmail = oClienteFatCNPJ_Diferente.Codigo.ToString();

                                o_frmNF.MotivoCancelamento = txtMotivoCancelamento.Text;
                                o_frmNF.ValorLiquido = moeValorLiquido.VALOR.Text;
                                o_frmNF.Parcelas = intParcelas.VALOR.Text;
                                o_frmNF.DataVencimento = DataVencimento.Text;
                                o_frmNF.DiasEntreVctos = intDiasEntreVctos.VALOR.Text;
                                o_frmNF.intNNF.VALOR.Text = oNotaFiscal.NumeroNF.ToString();
                                o_frmNF.butConsultaNF.Text = "Enviar RPS IPM";
                                o_frmNF.btnEnviarParaRadar.Visible = true;
                                o_frmNF.ShowDialog();
                                
                                // se retornar verdadeiro a nota foi emitida no ipm
                                if (geral.NotaFiscalEnviada)
                                {
                                    // gerar arquivo para o radar MovimentoServicosInclusao.txt
                                    SalvaDadosParaExportacao();
                                }
                                else
                                {
                                    // excluir nota fiscal não foi gerada no IPM
                                    try
                                    {
                                        oNotaFiscalDados.Excluir(Convert.ToInt32(intNumeroLancamento.VALOR.Text));
                                        this.Close(); 
                                        //MessageBox.Show("Exclusão realizada. Preciso fechar este formulário.");
                                    }
                                    finally
                                    {
                                        frmNotasFiscais ofrmnfs = new frmNotasFiscais();
                                        ofrmnfs.ShowDialog();
                                    }
                                }
                                btnNovo.Focus();
                            }
                            else if (File.Exists("c:\\eletron\\work\\rps.xml"))
                            {
                                MessageBox.Show("Não foi possível gerar o arquivo rps.xml, pois já existe na pasta \\eletron\\work\\ da unidade C:");
                            }
                        }
                        else if (cboTipoDocumento.Text[0].ToString() == "5") // fatura
                        {
                            frmFaturaPDF ofrmFatura = new frmFaturaPDF();
                            ofrmFatura.CodigoCliente = Convert.ToInt32(cliente1.txtCodigo.Text);
                            
                            ofrmFatura.lblNumeroFatura.Text = Convert.ToInt32(intNumeroNF.VALOR.Text).ToString("000000");
                            string sDisc = "";
                            for (int i = 1; i <= 14; i++)
                            {
                                if (_descricao[i].Text != "")
                                    sDisc = sDisc + _descricao[i].Text + " \n";
                            }
                            ofrmFatura.lblDiscriminacao.Text = sDisc;
                            ofrmFatura.lblValorTotalCima.Text = Convert.ToDecimal(moeValorLiquido.VALOR.Text).ToString("N2");
                            ofrmFatura.lblValorTotalBaixo.Text = Convert.ToDecimal(moeValorLiquido.VALOR.Text).ToString("N2");
                            ofrmFatura.Parcelas = Convert.ToInt16(intParcelas.VALOR.Text);
                            ofrmFatura.Vencimento = Convert.ToDateTime(DataVencimento.Text);
                            ofrmFatura.DiasEntreVencimentos = Convert.ToInt32(intDiasEntreVctos.VALOR.Text);
                            ofrmFatura.lblDataEmissao.Text = DataEmissao.Text;
                            ofrmFatura.pApenasGerarFatura = true;
                            try
                            {
                                ofrmFatura.ShowDialog();
                            }
                            finally
                            {
                                ofrmFatura.Close();
                            }
                            // enviar fatura
                            EnviarEmailFaturaParaCliente(false, Convert.ToInt32(intNumeroNF.VALOR.Text));

                            // gerar arquivo para o radar 
                            SalvaDadosParaExportacao();

                        }
                        else if (geral.Left(cboTipoDocumento.Text, 1) == "4")
                        {
                            btnRecibo.Enabled = true;
                            SalvaDadosParaExportacao("Recibo");
                        }
                        else
                            btnRecibo.Enabled = false;
                    }
                }
            }
        }
         
        private void SalvaDadosParaExportacao(string pTabela = "NotasFiscais")
        {
            if (geral.BancoUsado == 1)
            {
                if (geral.Left(cboTipoDocumento.Text, 1) == "5")
                    pTabela = "Fatura";
                clsExportacaoRadar oExpRadar = new clsExportacaoRadar();
                clsExportacaoRadarDados oExpRadarDados = new clsExportacaoRadarDados();
                if (intNumeroLancamento.VALOR.Text != "")
                    oExpRadar.CodigoNumero = Convert.ToInt32(intNumeroLancamento.VALOR.Text);
                oExpRadar.DataSolicitacao = DateTime.Now.ToShortDateString();
                oExpRadar.Tabela = pTabela;
                if (oExpRadar.CodigoNumero > 0)
                    oExpRadarDados.Inserir(oExpRadar);
            }
            else if (geral.BancoUsado == 4) // fastcompost
            {
                SalvaArquivoReciboFaturaImportacaoRadar(intNumeroLancamento.VALOR.Text);
            }
        }

        private void SalvaArquivoReciboFaturaImportacaoRadar(string pNumeroReciboNotaFiscal)
        {
            //C:\\formSILC\\fatura\\
            string _s = "";
            string pathExportacao = "\\formSILC\\fatura";
            if (geral.BancoUsado == 4) // fastcompost
            {
                pathExportacao = "fatura";
            }
            if (pathExportacao != "")
            {
                string ArquivoExportacao = pathExportacao + "\\ContasReceber.txt";
                int i = 1;
                while (true)
                {
                    if (!File.Exists(ArquivoExportacao))
                        break;
                    else
                        ArquivoExportacao = pathExportacao + "\\ContasReceber" + i + ".txt";
                    i++;
                    if (i > 10000)
                        break;
                }

                if (pNumeroReciboNotaFiscal != "")
                {
                    clsNotasFiscais oNotasFiscais = new clsNotasFiscais();
                    clsNotasFiscaisDados oNotasFiscaisDados = new clsNotasFiscaisDados();
                    oNotasFiscaisDados.PegaDados(oNotasFiscais, Convert.ToInt32(pNumeroReciboNotaFiscal));
                    if (oNotasFiscais.NumeroNotaFiscal > 0)
                    {
                        oCliente = new clsClientes();
                        oClienteDados = new clsClienteDados();
                        oClienteDados.PegaDados(oCliente, oNotasFiscais.CodigoCliente);

                        // gera arquivo para importação
                        StreamWriter x = File.CreateText(ArquivoExportacao);

                        string sLn = "";
                        string sDelimitador = ";";

                        for (int nParcs = 1; nParcs <= Convert.ToInt16(oNotasFiscais.NumeroParcelas); nParcs++)
                        {
                            // Tipo de Linha
                            if (oCliente.CNPJ_Faturamento != "")
                                sLn = sLn + "T" + geral.RetiraCharsCNPJCPF(oCliente.CNPJ_Faturamento) + ";";
                            else
                                sLn = sLn + "T" + geral.RetiraCharsCNPJCPF(oCliente.CNPJ_CPF) + ";";

                            sLn = sLn + Convert.ToDateTime(oNotasFiscais.DataEmissao).ToString("dd/MM/yy") + ";";

                            if (nParcs == 1 || oNotasFiscais.NumeroParcelas == 1)
                                sLn = sLn + Convert.ToDateTime(oNotasFiscais.Vencimento).ToString("dd/MM/yy") + ";";
                            else
                                sLn = sLn + Convert.ToDateTime(oNotasFiscais.Vencimento).AddDays(oNotasFiscais.DiasEntreVctos * (nParcs - 1)).ToString("dd/MM/yy") + ";";

                            // 2 - Filial
                            if (oNotasFiscais.TipoDocumento == 4) // recibo
                                sLn = sLn + "2" + sDelimitador;
                            else
                                sLn = sLn + "1" + sDelimitador;

                            if (oNotasFiscais.TipoDocumento == 4)
                                sLn = sLn + "4S" + oNotasFiscais.NumeroNF + ";";  // número documento 4 - recibo
                            else
                            {
                                if (oNotasFiscais.NumeroParcelas == 1)
                                    sLn = sLn + "5S" + oNotasFiscais.NumeroNF + ";";  // número documento 5 - fatura
                                else
                                    sLn = sLn + "5S" + oNotasFiscais.NumeroNF + "/" + nParcs.ToString() + ";";  // número documento 5 - fatura
                            }
                            sLn = sLn + oNotasFiscais.TipoDocumento + ";"; //' tipo documento
                            decimal vParcelaArredondada = 0;
                            vParcelaArredondada = oNotasFiscais.ValorTotal / oNotasFiscais.NumeroParcelas;
                            decimal vTotalCalculado = 0;
                            for (int xv = 1; xv <= oNotasFiscais.NumeroParcelas; xv++)
                            {
                                vTotalCalculado = vTotalCalculado + vParcelaArredondada;
                            }
                            if (vTotalCalculado < oNotasFiscais.ValorTotal && nParcs == 1)
                            {
                                vParcelaArredondada = vParcelaArredondada + Convert.ToDecimal(0.01);
                            }

                            decimal valorLiquido = oNotasFiscais.ValorTotal - oNotasFiscais.ValorDescontoIncondicionado;
                            if (oNotasFiscais.NumeroParcelas == 1)
                                sLn = sLn + valorLiquido.ToString("N2") + ";";
                            else
                                sLn = sLn + vParcelaArredondada.ToString("N2") + ";";

                            sLn = sLn + Convert.ToDateTime(oNotasFiscais.DataEmissao).ToString("dd/MM/yy") + ";"; // data saída
                            sLn = sLn + "\"" + oNotasFiscais.Historico + "\";"; // complemento histórico

                            if (oNotasFiscais.NumeroParcelas == 1)
                                sLn = sLn + oNotasFiscais.NumeroNF + "/" + Convert.ToDateTime(oNotasFiscais.DataEmissao).ToString("yy") + ";"; // número fatura
                            else
                                sLn = sLn + oNotasFiscais.NumeroNF + "/" + nParcs.ToString() + "/" + Convert.ToDateTime(oNotasFiscais.DataEmissao).ToString("yy") + ";"; // número fatura

                            if (oNotasFiscais.NumeroParcelas == 1)
                                sLn = sLn + valorLiquido.ToString("N2") + ";";
                            else
                                sLn = sLn + vParcelaArredondada.ToString("N2") + ";";
                            sLn = sLn + oNotasFiscais.Classificacao + ";";
                            sLn = sLn + "2;";
                            sLn = sLn + oNotasFiscais.ContaGerencial + ";";
                            sLn = sLn + oCliente.CodigoTipoCobranca.ToString("00") + ";" + Environment.NewLine;

                            if (oNotasFiscais.TipoDocumento == 5) // fatura
                            {
                                sLn = sLn + "O";
                                sLn = sLn + oNotasFiscais.NumeroNF + "/" + nParcs.ToString() + "/" + Convert.ToDateTime(oNotasFiscais.DataEmissao).ToString("yy") + ";"; // número fatura
                                sLn = sLn + oNotasFiscais.DataEmissao + ";";
                                sLn = sLn + vParcelaArredondada.ToString("N2") + ";";
                                if (oNotasFiscais.NumeroParcelas == 1)
                                    sLn = sLn + valorLiquido.ToString("N2") + ";";
                                else
                                    sLn = sLn + vParcelaArredondada.ToString("N2") + ";";
                                sLn = sLn + 0.ToString("N2") + ";";
                                sLn = sLn + 0.ToString("N2") + ";";
                                sLn = sLn + 0.ToString("N2") + ";";
                                sLn = sLn + 0.ToString("N2") + ";";
                                sLn = sLn + vParcelaArredondada.ToString("N2") + ";";
                                sLn = sLn + vParcelaArredondada.ToString("N2") + ";";
                                sLn = sLn + 0.ToString("N2") + ";" + Environment.NewLine;
                            }
                        }
                        sLn = sLn + "I" + geral.Left(oCliente.NomeFantasia, 10) + ";";
                        sLn = sLn + "1.001.005" + ";";
                        sLn = sLn + Convert.ToDateTime(oNotasFiscais.DataReferencia).ToString("MM/yyyy");

                        // escreve
                        x.WriteLine(sLn);

                        // salva e fecha o arquivo texto 
                        x.Close();

                    }
                }
            }
        }

        private void SalvarCorpoNotaFiscal(int pNumeroNotaFiscal, string pLog)
        {
            if (pNumeroNotaFiscal > 0)
            {                
                for (int i = 0; i <= 21; i++)
                {
                    oCorpoNF.Descricao = _descricao[i].Text.ToUpper();
                    oCorpoNF.Quantidade = 0;
                    oCorpoNF.PrecoUnitario = 0;
                    oCorpoNF.Valor = 0;
                    if (_quantidade[i].VALOR.Text != "")
                        oCorpoNF.Quantidade = Convert.ToDecimal(_quantidade[i].VALOR.Text);
                    oCorpoNF.Unidade = _unidade[i].Text.ToUpper();
                    if (_precounitario[i].VALOR.Text != "")
                        oCorpoNF.PrecoUnitario = Convert.ToDecimal(_precounitario[i].VALOR.Text);
                    if (_valor[i].VALOR.Text != "")
                        oCorpoNF.Valor = Convert.ToDecimal(_valor[i].VALOR.Text);
                    oCorpoNF.Linha = i;
                    if (oCorpoNF.Descricao != "")
                    {
                        pLog = pLog + "Sequencial NF: " + intNumeroLancamento.VALOR.Text + " ";
                        pLog = pLog + "Descrição: " + oCorpoNF.Descricao + " ";
                        pLog = pLog + "Unidade: " + oCorpoNF.Unidade + " ";
                        pLog = pLog + "Preço Unitário: " + oCorpoNF.PrecoUnitario + " ";
                        pLog = pLog + "Quantidade: " + oCorpoNF.Quantidade + " ";
                        pLog = pLog + "Valor: " + oCorpoNF.Valor + " \n";
                    }
                    oCorpoNFDados.Inserir(oCorpoNF, pNumeroNotaFiscal);
                }
                SalvarLog("Inclusão", pLog);
            }
        }

        private void moeValorTotal_Leave(object sender, EventArgs e)
        {
            //
            PegaAliquotaISS();
        }

        private void PegaAliquotaISS()
        {
            decimal PercentualISS = 0;
            if (cboTipoDocumento.Text != "")
            {
                if (eBotoes == Botoes.Novo && (cboTipoDocumento.Text[0].ToString() == "1" || cboTipoDocumento.Text[0].ToString() == "8"))
                {
                    // mudar o codigo do municipio de faturamento - quando é faturamento com cnpj diferente
                    if (oCliente.Codigo != oClienteFatCNPJ_Diferente.Codigo && oClienteFatCNPJ_Diferente.Codigo > 0)
                    {
                        oEndereco = oEnderecoDados.PegaDados(oEndereco, oClienteFatCNPJ_Diferente.Codigo, 2, 0);
                        oMunicipio = oMunicipioDados.PegaDados(oMunicipio, oEndereco.CodigoMunicipio);
                        CalcularImpostos();
                    }
                    else
                    {
                        if (cliente1.txtCodigo.Text != "")
                            oEnderecoDados.PegaDados(oEndereco, Convert.ToInt32(cliente1.txtCodigo.Text), 2, 0);
                        oMunicipioDados.PegaDados(oMunicipio, oEndereco.CodigoMunicipio);
                    }
                    if (oMunicipio.AliquotaISS < 0)
                        PercentualISS = oMunicipio.AliquotaISS * -1;
                    else
                        PercentualISS = oMunicipio.AliquotaISS;

                    if (oCliente.CNPJ_CPF != null && oCliente.CNPJ_CPF != "")
                    {
                        if (geral.RetiraCharsCNPJCPF(oCliente.CNPJ_CPF).Length < 12)
                        {
                            // quando pessoa física o percentual é sempre 5%
                            PercentualISS = 5;
                        }
                    }

                    moeAliquota.VALOR.Text = PercentualISS.ToString("N2").Replace(".", "");
                }
                // Recibo a alíquota é e sempre 0 - zero. 
                if (cboTipoDocumento.Text[0].ToString() == "4")
                {
                    PercentualISS = 0;
                    oMunicipio.AliquotaISS = 0;
                }
                CalcularImpostos();
            }
        }

        private void CalcularImpostos()
        {
            // Só calcula quando for nota fiscal nova
            if (!btnNovo.Enabled)
            {
                ValorIRRF = 0;
                ValorPIS = 0;
                ValorCOFINS = 0;
                ValorContrSocial = 0;
                ValorCRF = 0;
                ValorINSS = 0;

                oRetencao.AliquotaCOFINS_Retido = 0;
                oRetencao.AliquotaContribSocial = 0;
                oRetencao.AliquotaIR_Retido = 0;
                oRetencao.AliquotaPIS_Retido = 0;
                oRetencao.ValorLimiteCRF = 0;
                oRetencao.ValorLimiteIR = 0;

                if (cboRetencoesNF.Text != "" && (_descricao[0].Text.IndexOf("709") > -1 || chkReterImpostosFederais.Checked))
                    oRetencao = oRetencoesDados.PegaAliquotas(oRetencao, cboRetencoesNF.Text[0].ToString());

                if (cliente1.txtCodigo.Text != "")
                {
                    oCliente = oClienteDados.PegaDados(oCliente, Convert.ToInt32(cliente1.txtCodigo.Text));
                    decimal ValorTotalMesCRF = 0;
                    if (oCliente.CNPJ_Faturamento != "")
                    {
                        ValorTotalMesCRF = oNotaFiscalDados.PegaValorTotalMesParaCalcularCRF(Convert.ToInt32(Convert.ToDateTime(DataEmissao.Text).ToString("MM")),
                                                                                             Convert.ToInt32(Convert.ToDateTime(DataEmissao.Text).ToString("yyyy")),
                                                                                             geral.Left(oCliente.CNPJ_Faturamento, 14));
                    }
                    else
                    {
                        ValorTotalMesCRF = oNotaFiscalDados.PegaValorTotalMesParaCalcularCRF(Convert.ToInt32(Convert.ToDateTime(DataEmissao.Text).ToString("MM")),
                                                                                             Convert.ToInt32(Convert.ToDateTime(DataEmissao.Text).ToString("yyyy")),
                                                                                             geral.Left(oCliente.CNPJ_CPF, 11));
                    }

                    ValorTotal = 0;

                    if (moeValorTotal.VALOR.Text != "")
                        ValorTotal = Convert.ToDecimal(moeValorTotal.VALOR.Text);

                    if (moeAliquota.VALOR.Text != "")
                    {
                        ValorISS = Math.Round(ValorTotal * Convert.ToDecimal(moeAliquota.VALOR.Text) / 100, 2);
                        moeValorISS.VALOR.Text = ValorISS.ToString("N2").Replace(".", "");
                    }

                    if (ValorTotal >= oRetencao.ValorLimiteIR && oRetencao.ValorLimiteIR > 0)
                    {
                        moeIRRFperc.VALOR.Text = oRetencao.AliquotaIR_Retido.ToString("N2").Replace(".", "");
                        ValorIRRF = Math.Round(ValorTotal * oRetencao.AliquotaIR_Retido / 100, 2);
                    }
                    if ((ValorTotal >= oRetencao.ValorLimiteCRF && oRetencao.ValorLimiteCRF > 0) ||
                        (ValorTotalMesCRF > 0 && (ValorTotal + ValorTotalMesCRF) >= oRetencao.ValorLimiteCRF))
                    {
                        if (ValorTotalMesCRF > 0 && ValorTotalMesCRF >= oRetencao.ValorLimiteCRF && Botoes.Novo == eBotoes)
                            ValorTotalMesCRF = 0;

                        if (ValorTotal + ValorTotalMesCRF < oRetencao.ValorLimiteCRF)
                            ValorTotalMesCRF = 0;

                        ValorPIS = Math.Round(ValorTotal * oRetencao.AliquotaPIS_Retido / 100, 2);
                        ValorCOFINS = Math.Round(ValorTotal * oRetencao.AliquotaCOFINS_Retido / 100, 2);
                        ValorContrSocial = Math.Round(ValorTotal * oRetencao.AliquotaContribSocial / 100, 2);
                        ValorCRF = ValorPIS + ValorCOFINS + ValorContrSocial;

                        moeCRFperc.VALOR.Text = (oRetencao.AliquotaCOFINS_Retido + oRetencao.AliquotaPIS_Retido + oRetencao.AliquotaContribSocial).ToString("N2").Replace(".", "");
                    }
                    else
                    {
                        oRetencao.AliquotaCOFINS_Retido = 0;
                        oRetencao.AliquotaContribSocial = 0;
                        oRetencao.AliquotaPIS_Retido = 0;
                        moeCRFperc.VALOR.Text = "0,00";
                    }
                    decimal BaseCalculoINSS = 0;
                    decimal PercentualINSS = 0;
                    ValorINSS = 0;
                    if (moeValorBaseCalculoINSS.VALOR.Text != "" && moePercINSS.VALOR.Text != "")
                    {
                        BaseCalculoINSS = Convert.ToDecimal(moeValorBaseCalculoINSS.VALOR.Text);
                        PercentualINSS = Convert.ToDecimal(moePercINSS.VALOR.Text);
                        if (BaseCalculoINSS > 0 && PercentualINSS > 0)
                            ValorINSS = BaseCalculoINSS * PercentualINSS / 100;
                    }

                    // Maiusculas para todas linhas de descricao e unidades
                    for (int iLns = 0; iLns < _descricao.Length; iLns++)
                    {
                        _descricao[iLns].Text = _descricao[iLns].Text.ToUpper();
                        _unidade[iLns].Text = _unidade[iLns].Text.ToUpper();
                    }

                    if (ValorIRRF > 0)
                        _descricao[15].Text = "IRRF " + oRetencao.AliquotaIR_Retido.ToString("N2").Replace(".", "") + "% .............................. R$ " + ValorIRRF.ToString("N2").Replace(".", "");
                    else
                        _descricao[15].Text = "";

                    decimal PercentualCRF = oRetencao.AliquotaPIS_Retido + oRetencao.AliquotaCOFINS_Retido + oRetencao.AliquotaContribSocial;
                    if (PercentualCRF > 0)
                        _descricao[16].Text = "CRF  " + PercentualCRF.ToString("N2").Replace(".", "") + "% .............................. R$ " + ValorCRF.ToString("N2").Replace(".", "");
                    else
                        _descricao[16].Text = "";

                    if (PercentualINSS > 0 && BaseCalculoINSS > 0)
                        _descricao[17].Text = "INSS " + PercentualINSS + "% INCIDENTE S/SERVIÇOS (*) .... R$ " + ValorINSS.ToString("N2").Replace(".", "");
                    else
                        _descricao[17].Text = "";

                    _descricao[18].Text = "ALÍQUOTA DE ISS " + moeAliquota.VALOR.Text + "% ................... R$ " + ValorISS.ToString("N2").Replace(".", "");
                    _descricao[19].Text = "RETENÇÃO DE ISS CFE LEI COMPLEMENTAR TRIBUTÁRIA 166/03";
                    _descricao[20].Text = "SEÇÃO V ART 64.";

                    if (oCliente.CNPJ_CPF != null && oCliente.CNPJ_CPF != "")
                    {
                        if (geral.RetiraCharsCNPJCPF(geral.RetiraLetras(oCliente.CNPJ_CPF)).Length < 12 || geral.Left(cboRetencoesNF.Text, 1) == "A")
                        {
                            ValorISS = 0;
                        }
                        if (oEndereco.CodigoMunicipio == 83275 && geral.Left(cboRetencoesNF.Text, 1) == "G")
                            ValorISS = 0;
                    }
                    if (geral.Left(cboTipoDocumento.Text, 1) == "5")
                    {
                        //não diminiu valor iss, pois é fatura
                        moeValorLiquido.VALOR.Text = (ValorTotal - ValorIRRF - ValorCOFINS - ValorContrSocial - ValorPIS - ValorINSS).ToString("N2").Replace(".", "");
                    }
                    else
                    {
                        moeValorLiquido.VALOR.Text = (ValorTotal - ValorIRRF - ValorCOFINS - ValorContrSocial - ValorISS - ValorPIS - ValorINSS).ToString("N2").Replace(".", "");
                    }
                }
            }
        }

        private void butNFIPM_Click(object sender, EventArgs e)
        {
            if (cboTipoDocumento.Text == "")
                MessageBox.Show("Tipo de Documento inválido! Consulta apenas para Nota Fiscal IPM!");
            else if (cboTipoDocumento.Text[0].ToString() != "8")
                MessageBox.Show("Tipo de Documento inválido! Consulta apenas para Nota Fiscal IPM!");
            else if (intNumeroNF.VALOR.Text == "")
                MessageBox.Show("Número da Nota Fiscal inválido!");
            else
            {
                geral.NotaFiscalEnviada = false;
                frmBrowserNF frmNFipm = new frmBrowserNF();
                if (geral.UsuarioAtual == "teixeira")
                    frmNFipm.btnEnviarParaRadar.Visible = true;
                else
                    frmNFipm.btnEnviarParaRadar.Visible = false;
                frmNFipm.ControlBox = true;
                frmNFipm.MinimizeBox = false;
                frmNFipm.MaximizeBox = false;
                frmNFipm.Height = this.Height;
                frmNFipm.intNNF.VALOR.Text = intNumeroNF.VALOR.Text;
                //frmNFipm.btnExcluir.Visible = false;
                //if (geral.UsuarioAtual == "teixeira")
                //{
                //    frmNFipm.cliente1Codigo = cliente1.txtCodigo.Text;
                //    frmNFipm.MotivoCancelamento = txtMotivoCancelamento.Text;
                //    frmNFipm.ValorLiquido = moeValorLiquido.VALOR.Text;
                //    frmNFipm.Parcelas = intParcelas.VALOR.Text;
                //    frmNFipm.DataVencimento = DataVencimento.Text;
                //    frmNFipm.DiasEntreVctos = intDiasEntreVctos.VALOR.Text;
                //}
                frmNFipm.ShowDialog();
            }
        }
        private bool EnviarOuCancelarNF(bool pCancelar)
        {
            bool bRet = false;
            for (int i = 0; i <= 20; i++)
            {
                if (_descricao[i].Text == "" && _valor[i].VALOR.Text != "")
                {
                    MessageBox.Show("Linha com valor está com a descrição inválida ou nula! \n");
                    break;
                }
            }

            string sInfAssinar = "";

            CalcularImpostos();
            sInfAssinar = sInfAssinar + "<nfse>\n" + Environment.NewLine;

            //Se tirar como comentário somente vem resposta se tá tudo ok - não emite
            //sInfAssinar = sInfAssinar + "<nfse_teste>1</nfse_teste>\n" + Environment.NewLine;

            sInfAssinar = sInfAssinar + "<nf>\n" + Environment.NewLine; 

            if (pCancelar)
            {
                sInfAssinar = sInfAssinar + "<numero>" + intNumeroNF.Text + "</numero>\n" + Environment.NewLine;
                sInfAssinar = sInfAssinar + "<situacao>C</situacao>\n" + Environment.NewLine;
            }
            //sInfAssinar = sInfAssinar + "<valor_total>" + ValorTotal.ToString("N2").Replace(".", "") + "</valor_total>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<valor_total>" + moeValorTotal.VALOR.Text + "</valor_total>\n" + Environment.NewLine;
            if (moeDesconto.VALOR.Text == "")
                moeDesconto.VALOR.Text = "0,00";
            sInfAssinar = sInfAssinar + "<valor_desconto>" + Convert.ToDecimal(moeDesconto.VALOR.Text).ToString("N2").Replace(".", "") + "</valor_desconto>\n" + Environment.NewLine;

            if (ValorCRF > 0)
            {
                sInfAssinar = sInfAssinar + "<valor_pis>" + ValorPIS.ToString("N2").Replace(".", "") + "</valor_pis>\n" + Environment.NewLine;
                sInfAssinar = sInfAssinar + "<valor_cofins>" + ValorCOFINS.ToString("N2").Replace(".", "") + "</valor_cofins>\n" + Environment.NewLine;
                sInfAssinar = sInfAssinar + "<valor_contribuicao_social>" + ValorContrSocial.ToString("N2").Replace(".", "") + "</valor_contribuicao_social>\n" + Environment.NewLine;
            }

            if (ValorIRRF > 0)
                sInfAssinar = sInfAssinar + "<valor_ir>" + ValorIRRF.ToString("N2").Replace(".", "") + "</valor_ir>\n" + Environment.NewLine;

            if (ValorINSS > 0)
                sInfAssinar = sInfAssinar + "<valor_inss>" + ValorINSS.ToString("N2").Replace(".", "") + "</valor_inss>\n" + Environment.NewLine;


            if (pCancelar)
                sInfAssinar = sInfAssinar + "<observacao> + txtMotivoCancelamento.Text + </observacao>\n" + Environment.NewLine;
            else
                sInfAssinar = sInfAssinar + "<observacao></observacao>\n" + Environment.NewLine;

            sInfAssinar = sInfAssinar + "</nf>\n" + Environment.NewLine;
            geral.oEmpresa = geral.oEmpresaDados.PegaDados(geral.oEmpresa, 1);

            sInfAssinar = sInfAssinar + "<prestador>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<cpfcnpj>" + geral.RetiraCharsCNPJCPF(geral.oEmpresa.CNPJ_CPF) + "</cpfcnpj>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<cidade>8233</cidade>\n" + Environment.NewLine;

            sInfAssinar = sInfAssinar + "</prestador>\n" + Environment.NewLine;

            clsMunicipios oMunicipio = new clsMunicipios();

            // Código IPM
            if (cliente1.txtCodigo.Text != "")
            {
                oCliente = oClienteDados.PegaDados(oCliente, Convert.ToInt32(cliente1.txtCodigo.Text));
                oEndereco = oEnderecoDados.PegaDados(oEndereco, Convert.ToInt32(cliente1.txtCodigo.Text), 2, 0);
                oMunicipio = oMunicipioDados.PegaDados(oMunicipio, oEndereco.CodigoMunicipio);
            }

            sInfAssinar = sInfAssinar + "<tomador>\n" + Environment.NewLine;

            // Quando for pessoa física a cidade é a da Empresa (Brooks) que está na tabela parâmetros
            // o imposto é pago pela Empresa
            if (geral.RetiraCharsCNPJCPF(oCliente.CNPJ_CPF).Length > 12)
            {
                oMunicipio.Codigo = oEndereco.CodigoMunicipio;
                sInfAssinar = sInfAssinar + "<tipo>J</tipo>\n" + Environment.NewLine;
            }
            else
            {
                oMunicipio.Codigo = Convert.ToInt32(geral.oEmpresa.CodigoMunicipio);
                sInfAssinar = sInfAssinar + "<tipo>F</tipo>\n" + Environment.NewLine;
            }

            //apenas diz se vai ser informado o endereço - sempre vamos informar, pois não vamos controlar alterações de cadastro do cliente

            // apenas para identificação estrangeira
            sInfAssinar = sInfAssinar + "<identificador></identificador>\n" + Environment.NewLine;

            // Endereço do faturamento - para colocar tomador
            oEndereco = oEnderecoDados.PegaDados(oEndereco, Convert.ToInt32(cliente1.txtCodigo.Text), 1, 0);
            oMunicipio = oMunicipioDados.PegaDados(oMunicipio, oEndereco.CodigoMunicipio);

            sInfAssinar = sInfAssinar + "<estado>" + oMunicipio.UF + "</estado>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<pais>Brasil</pais>\n" + Environment.NewLine;

            if (oCliente.CNPJ_Faturamento != "")
                sInfAssinar = sInfAssinar + "<cpfcnpj>" + geral.RetiraLetras(geral.RetiraCharsCNPJCPF(oCliente.CNPJ_Faturamento)) + "</cpfcnpj>\n" + Environment.NewLine;
            else
                sInfAssinar = sInfAssinar + "<cpfcnpj>" + geral.RetiraLetras(geral.RetiraCharsCNPJCPF(oCliente.CNPJ_CPF)) + "</cpfcnpj>\n" + Environment.NewLine;


            sInfAssinar = sInfAssinar + "<ie>" + geral.Left(oCliente.RG_IE, 16) + "</ie>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<nome_razao_social>" + geral.RemoverAcentos(geral.Left(oCliente.Nome, 200)) + "</nome_razao_social>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<sobrenome_nome_fantasia>" + geral.RemoverAcentos(geral.Left(oCliente.NomeFantasia, 100)) + "</sobrenome_nome_fantasia>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<logradouro>" + geral.Left(oEndereco.endereco, 70) + "</logradouro>\n" + Environment.NewLine;

            string[] sp_email;
            sp_email = oEndereco.email.Replace(",", ";").Split(";"[0]);
            if (sp_email.Length <= 1)
                sInfAssinar = sInfAssinar + "<email>" + oEndereco.email + "</email>\n" + Environment.NewLine;
            else
            {
                sInfAssinar = sInfAssinar + "<email>";
                for (int i = 0; i < 4; i++)
                {
                    if (i < sp_email.Length)
                        sInfAssinar = sInfAssinar + sp_email[i] + ";";
                }
                sInfAssinar = sInfAssinar.Substring(0, sInfAssinar.Length - 1);
                sInfAssinar = sInfAssinar + "</email>\n" + Environment.NewLine;
            }

            sInfAssinar = sInfAssinar + "<numero_residencia>" + geral.Left(oEndereco.Numero, 8) + "</numero_residencia>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<complemento></complemento>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<ponto_referencia>" + geral.Left(oCliente.PontoReferencia, 100) + "</ponto_referencia>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<bairro>" + geral.Left(oEndereco.Bairro, 30) + "</bairro>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<cidade>" + geral.Left(oMunicipio.CodigoIPM.ToString(), 4) + "</cidade>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<cep>" + geral.Left(oEndereco.CEP.Replace("-", ""), 8) + "</cep>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<ddd_fone_comercial>" + geral.Left(oEndereco.DDD1.ToString(), 3) + "</ddd_fone_comercial>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<fone_comercial>" + geral.Left(oEndereco.Fone1, 9) + "</fone_comercial>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<ddd_fone_residencial>" + geral.Left(oEndereco.DDD2.ToString(), 3) + "</ddd_fone_residencial>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<fone_residencial>" + geral.Left(oEndereco.Fone2.ToString(), 9) + "</fone_residencial>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<ddd_fax>" + geral.Left(oEndereco.DDDF.ToString(), 3) + "</ddd_fax>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<fone_fax>" + geral.Left(oEndereco.Fax.ToString(), 9) + "</fone_fax>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "</tomador>\n" + Environment.NewLine;

            oEndereco = oEnderecoDados.PegaDados(oEndereco, Convert.ToInt32(cliente1.txtCodigo.Text), 2, 0);
            oMunicipio = oMunicipioDados.PegaDados(oMunicipio, oEndereco.CodigoMunicipio);

            int ip = 0;
            DateTime Vcto2Parcela;

            //' Calcular novamente os novos impostos
            CalcularImpostos();

            //' Soma total sem impostos
            decimal SomaTotalSemImpostos = 0;
            if (moeDesconto.VALOR.Text == "")
                moeDesconto.VALOR.Text = "0";

            SomaTotalSemImpostos = ValorTotal - ValorISS - ValorIRRF - ValorCRF - ValorINSS - Convert.ToDecimal(moeDesconto.VALOR.Text);

            oNotaFiscal = oNotaFiscalDados.PegaDados(oNotaFiscal, Convert.ToInt32(cliente1.txtCodigo.Text), Convert.ToInt32(intNumeroNF.VALOR.Text), false);

            int j = 7;

            decimal vlISSret = 0;

            vlISSret = ValorISS;

            if (_descricao[15].Text.IndexOf("IRRF").ToString().Length > 0)
                _descricao[15].Text = "";

            if (_descricao[16].Text.IndexOf("CRF").ToString().Length > 0)
                _descricao[16].Text = "";

            if (_descricao[17].Text.IndexOf("INSS").ToString().Length > 0)
                _descricao[17].Text = "";

            if (_descricao[18].Text.IndexOf("ALÍQUOTA DE ISS").ToString().Length > 0)
                _descricao[18].Text = "";

            if (_descricao[19].Text.IndexOf("RETENÇÃO DE ISS CFE LEI").ToString().Length > 0)
                _descricao[19].Text = "";

            if (_descricao[20].Text.IndexOf("SEÇÃO V ART 64.").ToString().Length > 0)
                _descricao[20].Text = " \n" + Environment.NewLine;

            string sDescricao = "";

            sInfAssinar = sInfAssinar + "<itens>\n" + Environment.NewLine;
            for (int i = 0; i <= 20; i++)
            {
                if (_descricao[i].Text.Trim() != "")
                {
                    if (_quantidade[i].VALOR.Text == "" && _precounitario[i].VALOR.Text == "")
                    {
                        sDescricao = sDescricao + _descricao[i].Text.Trim() + " \n" + Environment.NewLine;
                        sDescricao = geral.RemoverAcentos(sDescricao);
                    }
                    else
                    {
                        sDescricao = sDescricao + _descricao[i].Text.Trim() + " \n" + Environment.NewLine;
                        sDescricao = geral.RemoverAcentos(sDescricao);
                        sInfAssinar = sInfAssinar + "<lista>\n" + Environment.NewLine;

                        sInfAssinar = sInfAssinar + "<tributa_municipio_prestador>";

                        if (geral.RetiraCharsCNPJCPF(oCliente.CNPJ_CPF).Length > 11)   // pessoa juridica
                        {
                            if (sDescricao.Substring(1, 3) == "301" || sDescricao.Substring(2, 3) == "301")
                                sInfAssinar = sInfAssinar + "S";
                            else
                                sInfAssinar = sInfAssinar + "N";
                        }
                        else
                            sInfAssinar = sInfAssinar + "S";

                        sInfAssinar = sInfAssinar + "</tributa_municipio_prestador>\n" + Environment.NewLine;

                        sInfAssinar = sInfAssinar + "<codigo_local_prestacao_servico>";

                        // mudar o codigo do municipio de faturamento - quando é faturamento com cnpj diferente
                        if (oCliente.Codigo != oClienteFatCNPJ_Diferente.Codigo && oClienteFatCNPJ_Diferente.Codigo > 0)
                        {
                            oEndereco = oEnderecoDados.PegaDados(oEndereco, oClienteFatCNPJ_Diferente.Codigo, 2, 0);
                            oMunicipio = oMunicipioDados.PegaDados(oMunicipio, oEndereco.CodigoMunicipio);
                            CalcularImpostos();
                        }

                        if (oMunicipio.CodigoIPM.ToString().Length >= 4)
                            sInfAssinar = sInfAssinar + oMunicipio.CodigoIPM.ToString().Substring(0, 4);
                        else
                            sInfAssinar = sInfAssinar + oMunicipio.CodigoIPM.ToString();

                        sInfAssinar = sInfAssinar + "</codigo_local_prestacao_servico>\n" + Environment.NewLine;

                        if (_unidade[i].Text.ToUpper() == "TON" || _unidade[i].Text.ToUpper() == "TN")
                            sInfAssinar = sInfAssinar + "<unidade_codigo>9</unidade_codigo>\n" + Environment.NewLine;
                        else
                            sInfAssinar = sInfAssinar + "<unidade_codigo>1</unidade_codigo>\n" + Environment.NewLine;

                        sInfAssinar = sInfAssinar + "<unidade_quantidade>";
                        if (_quantidade[i].VALOR.Text != "")
                            sInfAssinar = sInfAssinar + Convert.ToDecimal(_quantidade[i].VALOR.Text).ToString("N4").Replace(".", "");
                        sInfAssinar = sInfAssinar + "</unidade_quantidade>\n" + Environment.NewLine;

                        sInfAssinar = sInfAssinar + "<unidade_valor_unitario>";
                        if (_precounitario[i].VALOR.Text != "")
                            sInfAssinar = sInfAssinar + Convert.ToDecimal(_precounitario[i].VALOR.Text).ToString("N2").Replace(".", "");
                        sInfAssinar = sInfAssinar + "</unidade_valor_unitario>\n" + Environment.NewLine;

                        if (_descricao[i].Text.Length > 0 && i == 0)
                        {
                            sDescricao = geral.RemoverAcentos(sDescricao);
                            sInfAssinar = sInfAssinar + "<codigo_item_lista_servico>" + sDescricao + "</codigo_item_lista_servico>\n" + Environment.NewLine;
                            sInfAssinar = sInfAssinar + "<descritivo>" + sDescricao.Substring(5, sDescricao.Length - 5).Trim() + "</descritivo>\n" + Environment.NewLine;
                        }
                        else
                        {
                            sDescricao = geral.RemoverAcentos(sDescricao);
                            sInfAssinar = sInfAssinar + "<codigo_item_lista_servico>709</codigo_item_lista_servico>\n" + Environment.NewLine;
                            sInfAssinar = sInfAssinar + "<descritivo>" + sDescricao.Replace(" \n", "\n") + "</descritivo>\n" + Environment.NewLine;
                        }

                        if (sDescricao.Substring(1, 4) == "301")
                        {
                            sInfAssinar = sInfAssinar + "<aliquota_item_lista_servico>0</aliquota_item_lista_servico>\n" + Environment.NewLine;
                            sInfAssinar = sInfAssinar + "<situacao_tributaria>0</situacao_tributaria>\n" + Environment.NewLine;
                            sInfAssinar = sInfAssinar + "<valor_tributavel>" + ValorTotal.ToString("N2").Replace(".", "") + "</valor_tributavel>\n" + Environment.NewLine;
                            moeAliquota.VALOR.Text = "";
                            moeValorISS.VALOR.Text = "";
                            CalcularImpostos();
                        }
                        //' situação tributária = não tributável = 14
                        else if (sDescricao.Substring(1, 4) == "304")
                        {
                            sInfAssinar = sInfAssinar + "<aliquota_item_lista_servico>0</aliquota_item_lista_servico>\n" + Environment.NewLine;
                            sInfAssinar = sInfAssinar + "<situacao_tributaria>14</situacao_tributaria>\n" + Environment.NewLine;
                            sInfAssinar = sInfAssinar + "<valor_tributavel>0,00</valor_tributavel>\n" + Environment.NewLine;
                            moeAliquota.VALOR.Text = "";
                            moeValorISS.VALOR.Text = "";
                            CalcularImpostos();
                        }
                        else
                        {
                            // Quando for pessoa física o ISS é a Aliguota da Empresa (Brooks) que está na tabela parâmetros e a cidade também
                            // o imposto é pago pela Empresa
                            if (geral.RetiraCharsCNPJCPF(oCliente.CNPJ_CPF).Length > 12)
                            {
                                if (moeAliquota.VALOR.Text.IndexOf(",00") > -1)
                                    sInfAssinar = sInfAssinar + "<aliquota_item_lista_servico>" + Convert.ToDecimal(moeAliquota.VALOR.Text).ToString("N0").Replace(".", "") + "</aliquota_item_lista_servico>\n" + Environment.NewLine;
                                else 
                                    sInfAssinar = sInfAssinar + "<aliquota_item_lista_servico>" + Convert.ToDecimal(moeAliquota.VALOR.Text).ToString("N2").Replace(".", "") + "</aliquota_item_lista_servico>\n" + Environment.NewLine;

                                if (cboSituacaoTributaria.Text != "")
                                    sInfAssinar = sInfAssinar + "<situacao_tributaria>" + geral.Left(cboSituacaoTributaria.Text, 1) + "</situacao_tributaria>\n" + Environment.NewLine;
                                else if (geral.Left(cboRetencoesNF.Text, 1) == "G" && oEndereco.CodigoMunicipio == 83275)
                                    sInfAssinar = sInfAssinar + "<situacao_tributaria>0</situacao_tributaria>\n" + Environment.NewLine;
                                else if (oCliente.Codigo == 2354)  //2354 - condominio patio das flores 
                                    sInfAssinar = sInfAssinar + "<situacao_tributaria>2</situacao_tributaria>\n" + Environment.NewLine;
                                else if (oMunicipio.Codigo == 82651 && geral.Left(cboRetencoesNF.Text, 1) != "G") // qdo não for orgãos públicos em porto bello
                                    sInfAssinar = sInfAssinar + "<situacao_tributaria>2</situacao_tributaria>\n" + Environment.NewLine;
                                else if (oMunicipio.Codigo == 82651) // || oMunicipio.Codigo == 8265) // porto bello não aceita 2 qdo condominio
                                    sInfAssinar = sInfAssinar + "<situacao_tributaria>1</situacao_tributaria>\n" + Environment.NewLine;
                                else
                                    sInfAssinar = sInfAssinar + "<situacao_tributaria>2</situacao_tributaria>\n" + Environment.NewLine;
                            }
                            else
                            {
                                // quando é pessoa física não tem aliquota
                                if (moeAliquota.VALOR.Text.IndexOf(",00") > -1)
                                    sInfAssinar = sInfAssinar + "<aliquota_item_lista_servico>" + Convert.ToDecimal(moeAliquota.VALOR.Text).ToString("N0").Replace(".", "") + "</aliquota_item_lista_servico>\n" + Environment.NewLine;
                                else
                                    sInfAssinar = sInfAssinar + "<aliquota_item_lista_servico>" + Convert.ToDecimal(moeAliquota.VALOR.Text).ToString("N2").Replace(".", "") + "</aliquota_item_lista_servico>\n" + Environment.NewLine;
                                sInfAssinar = sInfAssinar + "<situacao_tributaria>0</situacao_tributaria>\n" + Environment.NewLine;
                            }

                            sInfAssinar = sInfAssinar + "<valor_tributavel>" + Math.Round(Convert.ToDecimal(_quantidade[i].VALOR.Text) * Convert.ToDecimal(_precounitario[i].VALOR.Text), 2).ToString("N2").Replace(".", "");
                            sInfAssinar = sInfAssinar + "</valor_tributavel>\n" + Environment.NewLine;
                            sInfAssinar = sInfAssinar + "<valor_deducao>0,00</valor_deducao>\n" + Environment.NewLine;
                            if (geral.RetiraCharsCNPJCPF(oCliente.CNPJ_CPF).Length > 12 && _valor[i].VALOR.Text != "")
                            {
                                if (oEndereco.CodigoMunicipio == 83275 && geral.Left(cboRetencoesNF.Text, 1) == "G")
                                    sInfAssinar = sInfAssinar + "<valor_issrf>" + 0.ToString("N2").Replace(".", "") + "</valor_issrf>\n" + Environment.NewLine;
                                else
                                    sInfAssinar = sInfAssinar + "<valor_issrf>" + Math.Round(Convert.ToDecimal(_valor[i].VALOR.Text) * Convert.ToDecimal(moeAliquota.VALOR.Text) / 100, 2).ToString("N2").Replace(".", "") + "</valor_issrf>\n" + Environment.NewLine;
                            }
                            else
                                sInfAssinar = sInfAssinar + "<valor_issrf>" + 0.ToString("N2").Replace(".", "") + "</valor_issrf>\n" + Environment.NewLine;
                        }
                        sInfAssinar = sInfAssinar + "</lista>\n" + Environment.NewLine;
                        sDescricao = "";
                    }
                }
            }
            sInfAssinar = sInfAssinar + "</itens>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<genericos>";

            SomaTotalSemImpostos = Convert.ToDecimal(moeValorLiquido.VALOR.Text);
            //aqui vai as parcelas da nota fiscal
            decimal ValorParcela = SomaTotalSemImpostos;
            if (intParcelas.VALOR.Text != "0")
                ValorParcela = SomaTotalSemImpostos / Convert.ToDecimal(intParcelas.VALOR.Text);

            Vcto2Parcela = Convert.ToDateTime(DataVencimento.Text).AddDays(Convert.ToInt32(intDiasEntreVctos.VALOR.Text));

            sInfAssinar = sInfAssinar + "<linha>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<titulo>NF nr</titulo>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<descricao>Parcela - Vencimento - Valor     </descricao>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "</linha>\n" + Environment.NewLine;

            for (int _ip = 1; _ip <= Convert.ToDecimal(intParcelas.VALOR.Text); _ip++)
            {
                sInfAssinar = sInfAssinar + "<linha>\n" + Environment.NewLine;

                sInfAssinar = sInfAssinar + "<titulo>" + Convert.ToInt32(intNumeroNF.VALOR.Text).ToString("00000") + "</titulo>\n" + Environment.NewLine;

                sInfAssinar = sInfAssinar + "<descricao>";
                sInfAssinar = sInfAssinar + _ip.ToString("00000");
                if (_ip == 2)
                    sInfAssinar = sInfAssinar + " - " + Vcto2Parcela;
                else if (_ip >= 3)
                    sInfAssinar = sInfAssinar + " - " + Vcto2Parcela.AddDays(Convert.ToInt16(intDiasEntreVctos.VALOR.Text));
                else if (intParcelas.VALOR.Text == "1")
                    sInfAssinar = sInfAssinar + " - " + DataVencimento.Text;
                else if (_ip == 1)
                    sInfAssinar = sInfAssinar + " - " + DataVencimento.Text;

                sInfAssinar = sInfAssinar + " - " + ValorParcela.ToString("N2");
                sInfAssinar = sInfAssinar + "</descricao>";

                sInfAssinar = sInfAssinar + "</linha>\n" + Environment.NewLine;

            }
            // linha em branco
            sInfAssinar = sInfAssinar + "<linha>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<titulo></titulo>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<descricao></descricao>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "</linha>\n" + Environment.NewLine;
            
            if (txtTitulo1InfCompl.Text != "" || txtDescricao1InfCompl.Text != "")
            {
                sInfAssinar = sInfAssinar + "<linha>\n" + Environment.NewLine;
                sInfAssinar = sInfAssinar + "<titulo>" + geral.RemoverAcentos(txtTitulo1InfCompl.Text) + "</titulo>\n" + Environment.NewLine;
                sInfAssinar = sInfAssinar + "<descricao>" + geral.RemoverAcentos(txtDescricao1InfCompl.Text) + "</descricao>\n" + Environment.NewLine;
                sInfAssinar = sInfAssinar + "</linha>\n" + Environment.NewLine;
            }

            if (txtDescricao2InfCompl.Text != "")
            {
                sInfAssinar = sInfAssinar + "<linha>\n" + Environment.NewLine;
                sInfAssinar = sInfAssinar + "<titulo>" + geral.RemoverAcentos(txtTitulo2InfCompl.Text) + "</titulo>\n" + Environment.NewLine;
                sInfAssinar = sInfAssinar + "<descricao>" + geral.RemoverAcentos(txtDescricao2InfCompl.Text) + "</descricao>\n" + Environment.NewLine;
                sInfAssinar = sInfAssinar + "</linha>\n" + Environment.NewLine;
            }

            if (txtDescricao3InfCompl.Text != "")
            {
                sInfAssinar = sInfAssinar + "<linha>\n" + Environment.NewLine;
                sInfAssinar = sInfAssinar + "<titulo>" + geral.RemoverAcentos(txtTitulo3InfCompl.Text) + "</titulo>\n" + Environment.NewLine;
                sInfAssinar = sInfAssinar + "<descricao>" + geral.RemoverAcentos(txtDescricao3InfCompl.Text) + "</descricao>\n" + Environment.NewLine;
                sInfAssinar = sInfAssinar + "</linha>\n" + Environment.NewLine;
            }

            // linha em branco
            sInfAssinar = sInfAssinar + "<linha>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<titulo></titulo>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<descricao></descricao>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "</linha>\n" + Environment.NewLine;

            sInfAssinar = sInfAssinar + "<linha>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<titulo>Retencao Dispensada de Pis, Cofins, CSLL e IR - Base legal:</titulo>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "<descricao>Art. 3o do Decreto-Lei nr. 2.462/1988; Art. 55 da Lei nr. 7.713/1988; Art. 716 do RIR/2018; Art. 70, inciso I, alinea d, da Lei nr. 11.196/2005; ADN Cosit nr. 9/1990; Art. 30 a 36 da Lei nr. 10.833/2003; Instrucao Normativa SRF nr. 459/2004.</descricao>\n" + Environment.NewLine;
            sInfAssinar = sInfAssinar + "</linha>\n" + Environment.NewLine;

            sInfAssinar = sInfAssinar + "</genericos>";

            sInfAssinar = sInfAssinar + "</nfse>\n" + Environment.NewLine;

            // Código IPM
            sInfAssinar = "<?xml version='1.0' encoding='ISO-8859-1' standalone='yes' ?>\n" + Environment.NewLine + sInfAssinar;

            sInfAssinar = sInfAssinar.Replace("..", ""); // tira pontos pares

            sInfAssinar = sInfAssinar.Replace("&", "e"); // Não aceita e comercial (&)

            StreamWriter writer = new StreamWriter("c:\\eletron\\work\\rps.xml", false, Encoding.UTF8);
            writer.WriteLine(sInfAssinar);
            writer.Close();
            writer.Dispose();

            if (File.Exists("c:\\eletron\\work\\rps.xml"))
                bRet = true;
            return bRet;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult oYes = new DialogResult();

            if (intNumeroNF.VALOR.Text != "")
            {

                if (Convert.ToInt32(intNumeroNF.VALOR.Text) > 0)
                {
                    if (txtMotivoCancelamento.Text == "" && btnCancelar.Text == "Cancelar")
                    {
                        if (cboTipoDocumento.Text[0].ToString() == "8")
                            MessageBox.Show("Informe o motivo do cancelamento para Cancelar a NF no IPM.");
                        else if (cboTipoDocumento.Text[0].ToString() == "5")
                            MessageBox.Show("Informe o motivo do cancelamento da Fatura.");
                        else if (cboTipoDocumento.Text[0].ToString() == "4")
                            MessageBox.Show("Informe o motivo do cancelamento do Recibo.");
                        txtMotivoCancelamento.Focus();
                    }
                    else
                    {
                        // Recibo
                        if (cboTipoDocumento.Text[0].ToString() == "4")
                        {
                            if (btnCancelar.Text == "Cancelar")
                                oYes = MessageBox.Show("Confirma cancelamento do Recibo nº " + Convert.ToInt32(intNumeroNF.VALOR.Text), "", MessageBoxButtons.YesNo);
                            else
                                oYes = MessageBox.Show("Confirma mudança de status para Recibo Emitido?" + Convert.ToInt32(intNumeroNF.VALOR.Text), "", MessageBoxButtons.YesNo);

                            if (oYes == DialogResult.Yes) // sim
                            {
                                if (oNotaFiscal.NumeroNF > 0)
                                {
                                    if (btnCancelar.Text == "Cancelar")
                                    {
                                        oNotaFiscal.Cancelada = 1;
                                        oNotaFiscalDados.SalvarCancelamento(oNotaFiscal, Convert.ToInt32(intNumeroLancamento.VALOR.Text));
                                        btnCancelar.Text = "Cancelado";
                                        txtMotivoCancelamento.Text = "";
                                    }
                                    else
                                    {
                                        oNotaFiscal.Cancelada = 0;
                                        oNotaFiscalDados.SalvarCancelamento(oNotaFiscal, Convert.ToInt32(intNumeroLancamento.VALOR.Text));
                                        btnCancelar.Text = "Cancelar";
                                    }
                                }
                            }
                        }
                        else
                        {
                            // fatura      
                            if (btnCancelar.Text == "Cancelar")
                                oYes = MessageBox.Show("Confirma cancelamento da Nota Fiscal / Fatura nº " + Convert.ToInt32(intNumeroNF.VALOR.Text), "", MessageBoxButtons.YesNo);
                            else
                                oYes = MessageBox.Show("Confirma mudança de status para Nota Fiscal / Fatura Emitida?", "", MessageBoxButtons.YesNo);

                            if (oYes == DialogResult.Yes) // sim
                            {
                                if (oNotaFiscal.NumeroNF > 0)
                                {
                                    if (btnCancelar.Text == "Cancelar")
                                    {
                                        if (cboTipoDocumento.Text[0].ToString() == "8") // nota fiscal - enviar para ipm
                                        {
                                            oNotaFiscal.Cancelada = 1;
                                            oNotaFiscalDados.SalvarCancelamento(oNotaFiscal, Convert.ToInt32(intNumeroLancamento.VALOR.Text));
                                            btnCancelar.Text = "Cancelada";
                                            EnviarOuCancelarNF(true);
                                        }
                                        else if (cboTipoDocumento.Text[0].ToString() == "5")
                                        {
                                            oNotaFiscal.Cancelada = 1;
                                            oNotaFiscalDados.SalvarCancelamento(oNotaFiscal, Convert.ToInt32(intNumeroLancamento.VALOR.Text));
                                            btnCancelar.Text = "Cancelada";
                                            EnviarEmailFaturaParaCliente(true, Convert.ToInt32(intNumeroNF.VALOR.Text));
                                        }
                                    }
                                    else
                                    {
                                        if (cboTipoDocumento.Text[0].ToString() == "5")
                                        {
                                            oNotaFiscal.Cancelada = 0;
                                            oNotaFiscalDados.SalvarCancelamento(oNotaFiscal, Convert.ToInt32(intNumeroLancamento.VALOR.Text));
                                            btnCancelar.Text = "Cancelar";
                                            //EnviarEmailFaturaParaCliente Descancelamento true
                                        }
                                    }
                                    txtMotivoCancelamento.Text = "";
                                }
                            }
                        }
                    }
                }
            }
            else
                MessageBox.Show("Nenhuma nota selecionada!");

        }

        private string EnviarEmailFaturaParaCliente(bool pCancelamento, int pNumeroNF)
        {

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587; // 587; // 465;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("financeiro@brooksambiental.com.br", "edidfdimasqccvva");

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();            

            if (pCancelamento)
                mail.Subject = "Cancelamento Fatura nº " + intNumeroNF.VALOR.Text + " BROOKS "; // Assunto da mensagem
            else
                mail.Subject = "Fatura nº " + intNumeroNF.VALOR.Text + " BROOKS "; // Assunto da mensagem
            
            // mail.Attachments.Add();
            //.Adjunto = "" // aqui eh o anexo, se quiser colocar algum anexo, coloque o local do arquivo, ex: C:\arquivo.exe, caso nao queira anexo, deixe como esta
            //.de = "<BROOKS Financeiro>" // Nome do remetente

            string s = "";
            s = s + "Dados do Emissor \n";
            geral.oEmpresa = geral.oEmpresaDados.PegaDados(geral.oEmpresa, 1);
            s = s + geral.oEmpresa.Nome + " \n";
            s = s + geral.oEmpresa.Endereco + " - " + geral.oEmpresa.Cidade + " - " + geral.oEmpresa.UF + " \n";
            s = s + geral.oEmpresa.Telefones + " \n";
            s = s + geral.oEmpresa.CNPJ_CPF + " \n";
            s = s + " \n";
            
            if (cliente1.txtCodigo.Text != "")
            {
                oCliente = oClienteDados.PegaDados(oCliente, Convert.ToInt32(cliente1.txtCodigo.Text));
                oEndereco = oEnderecoDados.PegaDados(oEndereco, Convert.ToInt32(cliente1.txtCodigo.Text), 1, 0);
                oMunicipio = oMunicipioDados.PegaDados(oMunicipio, oEndereco.CodigoMunicipio);
                s = s + "Dados do cliente \n";
                s = s + oCliente.Nome2 + " \n";
                s = s + oEndereco.endereco + " \n";
                if (oEndereco.Numero != "")
                    s = s + ", n° " + oEndereco.Numero.ToString() + " \n";
                if (oEndereco.Complemento != "")
                    s = s + " " + oEndereco.Complemento;

                s = s + " - " + oMunicipio.Nome + " - " + oMunicipio.UF + " \n";
                if (oEndereco.Fone1.Length > 0)
                {
                    if (oEndereco.DDD1 > 0)
                    {
                        s = s + "(" + oEndereco.DDD1 + ") ";
                        s = s + oEndereco.Fone1.ToString() + " \n";
                    }
                    else
                        s = s + oEndereco.Fone1.ToString() + " \n";
                }
                if (oEndereco.Fone2.Length > 0)
                {
                    if (oEndereco.DDD2 > 0)
                    {
                        s = s + "(" + oEndereco.DDD2 + ") ";
                        s = s + oEndereco.Fone2.ToString() + " \n";
                    }
                    else
                        s = s + oEndereco.Fone2.ToString() + " \n";
                }
                s = s + geral.RetiraLetras(geral.oEmpresa.CNPJ_CPF) + " \n";

                s = s + " \n";

                string[] oSplitMail;
                if (txtMotivoCancelamento.Text == "tstedson" || geral.UsuarioAtual.ToLower() == "teixeira")
                {
                    mail.To.Add("megasis.edson@gmail.com");
                }
                else
                {
                    // informar e-mail(s) do cliente cadastrado
                    //oSplitMail = (oEndereco.email + ";fiscal@base.cnt.br").Replace(",", ";").Split(";"[0]);
                    oSplitMail = (oEndereco.email).Replace(",", ";").Split(";"[0]);
                    for (int i = 0; i <= oSplitMail.Length - 1; i++)
                    {
                        mail.To.Add(oSplitMail[i].Trim()); //e-mail
                    }
                }
            }

            if (pCancelamento)
            {
                s = s + " \n";
                s = s + "Cancelamos Fatura nº " + intNumeroNF.VALOR.Text + " \n";
                s = s + " \n";
                s = s + "Motivo: " + txtMotivoCancelamento.Text + " \n";
                s = s + " \n";
            }

            decimal ValorParcela = 0;
            DateTime Vcto2Parcela;
            decimal SomaTotalSemImpostos = 0;

            SomaTotalSemImpostos = Convert.ToDecimal(moeValorLiquido.VALOR.Text);

            s = s + "Valor total .: " + SomaTotalSemImpostos.ToString("N2") + " \n";
            s = s + " \n";

            if (intParcelas.VALOR.Text != "")
                ValorParcela = SomaTotalSemImpostos / Convert.ToInt16(intParcelas.VALOR.Text);
            else
                ValorParcela = SomaTotalSemImpostos;

            Vcto2Parcela = Convert.ToDateTime(DataVencimento.Text).AddDays(Convert.ToInt32(intDiasEntreVctos.VALOR.Text));

            string sParcs = "";
            sParcs = sParcs + " Parcela     Vencimento    Valor \n";

            for (int _ip = 1; _ip <= Convert.ToDecimal(intParcelas.VALOR.Text); _ip++)
            {
                sParcs = sParcs + " \n";

                sParcs = sParcs + Convert.ToInt32(intNumeroNF.VALOR.Text).ToString("00000");

                sParcs = sParcs + "-" + _ip.ToString("0");
                if (_ip == 2)
                    sParcs = sParcs + " - " + Vcto2Parcela;
                else if (_ip >= 3)
                    sParcs = sParcs + " - " + Vcto2Parcela.AddDays(Convert.ToInt16(intDiasEntreVctos.VALOR.Text));
                else if (intParcelas.VALOR.Text == "1")
                    sParcs = sParcs + " - " + DataVencimento.Text;
                else if (_ip == 1)
                    sParcs = sParcs + " - " + DataVencimento.Text;

                sParcs = sParcs + " - " + ValorParcela.ToString("N2").Replace(".", "");

                sParcs = sParcs + " \n";

            }
            sParcs = sParcs + " \n";

            s = s + sParcs + " \n";

            s = s + " \n";
            s = s + "Obrigado. \n";

            mail.From = new System.Net.Mail.MailAddress("financeiro@brooksambiental.com.br", "Financeiro BROOKS", Encoding.UTF8);

            mail.Body = s;
            System.Net.Mail.Attachment _att = new System.Net.Mail.Attachment("arquivo" + pNumeroNF.ToString("000000") + ".pdf");
            mail.Attachments.Add(_att);
            mail.Priority = System.Net.Mail.MailPriority.Normal;

            try
            {
                smtp.Send(mail);
                MessageBox.Show("e-mail enviado com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
            return "";

        }

        private void btnFatura_Click(object sender, EventArgs e)
        {
            if (cboTipoDocumento.Text.Length > 0)
            {
                if (cboTipoDocumento.Text[0].ToString() != "5")
                {
                    MessageBox.Show("Selecione um Fatura!");
                }
                else
                {
                    if (cliente1.txtCodigo.Text != "" && intNumeroNF.VALOR.Text != "" && btnNovo.Enabled)
                    {
                        frmFaturaPDF ofrmFatura = new frmFaturaPDF();
                        ofrmFatura.CodigoCliente = Convert.ToInt32(cliente1.txtCodigo.Text);
                        ofrmFatura.lblNumeroFatura.Text = Convert.ToInt32(intNumeroNF.VALOR.Text).ToString("000000");
                        string sDisc = "";
                        for (int i = 1; i <= 14; i++)
                        {
                            if (_descricao[i].Text != "")
                                sDisc = sDisc + _descricao[i].Text + " \n";
                        }
                        ofrmFatura.lblDiscriminacao.Text = sDisc;
                        ofrmFatura.lblValorTotalCima.Text = Convert.ToDecimal(moeValorLiquido.VALOR.Text).ToString("N2");
                        ofrmFatura.lblValorTotalBaixo.Text = Convert.ToDecimal(moeValorLiquido.VALOR.Text).ToString("N2");
                        ofrmFatura.Parcelas = Convert.ToInt16(intParcelas.VALOR.Text);
                        ofrmFatura.Vencimento = Convert.ToDateTime(DataVencimento.Text);
                        ofrmFatura.DiasEntreVencimentos = Convert.ToInt32(intDiasEntreVctos.VALOR.Text);
                        ofrmFatura.lblDataEmissao.Text = DataEmissao.Text;
                        ofrmFatura.pApenasGerarFatura = false;
                        ofrmFatura.ShowDialog();
                        ofrmFatura.Close();
                    }
                }
            }
        }

        private void btnGeraArquivoExport_Click(object sender, EventArgs e)
        {
            try
            {
                btnGeraArquivoExport.Enabled = false;
                SalvaDadosParaExportacao();
            }
            finally
            {
                btnGeraArquivoExport.Enabled = true;
            }
        }

        private void DataEmissao_Leave(object sender, EventArgs e)
        {
            PegaDataReferencia();
        }

        private void chkReterImpostosFederais_Click(object sender, EventArgs e)
        {
            if (!btnNovo.Enabled)
            {
                _descricao[0].Text = "";
                if (chkReterImpostosFederais.Checked)
                    _descricao[0].Text = "709";
                CalcularImpostos();
            }
        }

        private void btnRPS_Click(object sender, EventArgs e)
        {
            //
            EnviarOuCancelarNF(false);
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (intNumeroLancamento.VALOR.Text != "")
            {
                int _n = Convert.ToInt32(intNumeroLancamento.VALOR.Text) - 1;
                int x = 0;
                if (_n > 0)
                {
                    LimpaCampos();
                    int iRg = _n;
                    while (true)
                    {
                        intNumeroLancamento.VALOR.Text = iRg.ToString();
                        oNotaFiscal = oNotaFiscalDados.PegaDados(oNotaFiscal, iRg);
                        if (oNotaFiscal.CodigoCliente > 0 && oNotaFiscal.NumeroNotaFiscal > 0)
                        {
                            MostraDadosCliente();
                            //LeDadosCorpoNotaFiscal(oNotaFiscal.NumeroNotaFiscal);
                            bindingSource.DataSource = oClienteDados.PreencheNomeFantasiaComCodigo(oNotaFiscal.CodigoCliente, false);
                            GradeClientes.DataSource = bindingSource.DataSource;
                            break;
                        }
                        iRg--;
                        x++;
                        if (iRg <= 0 || x > 10)
                            break;
                    }
                }
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (intNumeroLancamento.VALOR.Text != "")
            {
                int iUltimoRegistro = oNotaFiscalDados.PegaUltimoNumeroNotaFiscal();
                int _n = Convert.ToInt32(intNumeroLancamento.VALOR.Text) + 1;
                int iRg = _n;
                if (iRg <= iUltimoRegistro)
                {
                    if (_n > 0)
                    {
                        while (true)
                        {
                            LimpaCampos();
                            intNumeroLancamento.VALOR.Text = iRg.ToString();
                            oNotaFiscal = oNotaFiscalDados.PegaDados(oNotaFiscal, iRg);
                            if (oNotaFiscal.CodigoCliente > 0 && oNotaFiscal.NumeroNotaFiscal > 0)
                            {
                                MostraDadosCliente();
                                //LeDadosCorpoNotaFiscal(oNotaFiscal.NumeroNotaFiscal);
                                bindingSource.DataSource = oClienteDados.PreencheNomeFantasiaComCodigo(oNotaFiscal.CodigoCliente, false);
                                GradeClientes.DataSource = bindingSource.DataSource;
                                break;
                            }
                            iRg++;
                            if (iRg > iUltimoRegistro)
                                break;
                        }
                    }
                }
            }
        }

        private void btnRecibo_Click(object sender, EventArgs e)
        {
            if (cliente1.txtCodigo.Text != "")
            {
                frmRecibo ofrmRecibo = new frmRecibo();
                ofrmRecibo.pCodigoCliente = Convert.ToInt32(cliente1.txtCodigo.Text);
                ofrmRecibo.pValor = Convert.ToDecimal(moeValorLiquido.VALOR.Text);
                ofrmRecibo.pHistorico = txtHistorico.Text;
                ofrmRecibo.ShowDialog();
            }
        }

        private void txtHistorico_Leave(object sender, EventArgs e)
        {
            txtHistorico.Text = geral.RemoverAcentos(txtHistorico.Text.ToUpper());
        }

        private void cboTipoDocumento_Click(object sender, EventArgs e)
        {

        }

        private void cboTipoDocumento_Leave(object sender, EventArgs e)
        {
            if (geral.Left(cboTipoDocumento.Text, 1) == "8")
                PegaAliquotaISS();
            if (geral.Left(cboTipoDocumento.Text, 1) != "5" && geral.BancoUsado == 4)
            {
                MessageBox.Show("Só fatura foi implementada. Bloqueado pelo desenvolvedor!", "");
                cboTipoDocumento.Text = "5-Fatura";
            }
            else if (geral.Left(cboTipoDocumento.Text, 1) == "4")
            {
                moeAliquota.VALOR.Text = "0,00";
                oMunicipio.AliquotaISS = 0;
                CalcularImpostos();
            }
        }
    }
}
