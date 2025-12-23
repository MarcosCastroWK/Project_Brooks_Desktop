using System;
using System.Data;
using System.Windows.Forms;
using LibSILC;
using SILCNegocios;

namespace formSILC
{
    public partial class frmInsertProgramacao : Form
    {
        public bool pInsercaoEmProgramacaoFechada = false;
        public SILCNegocios.clsProgramacaoDiariaServicos oProgramacaoDiaria = new SILCNegocios.clsProgramacaoDiariaServicos();
        private SILCNegocios.clsResiduos oRes = new SILCNegocios.clsResiduos();
        private SILCNegocios.clsReprogramacaoServicos oReprogramacao = new SILCNegocios.clsReprogramacaoServicos();
        private clsReprogramacaoDados oReprogramacaoDados = new clsReprogramacaoDados();
        private clsResiduoDados oResDados = new clsResiduoDados();
        private BindingSource bindingSource = new BindingSource();
        private clsTABMotivosOBSDados oTabMotivosObs = new clsTABMotivosOBSDados();
        private SILCNegocios.clsStatusCorProgramacao oStatusCorProg = new SILCNegocios.clsStatusCorProgramacao();
        private clsStatusCorProgramacaoDados oStatusCorProgDados = new clsStatusCorProgramacaoDados();
        private clsClientes oClientes = new clsClientes();
        private clsClienteDados oClienteDados = new clsClienteDados();
        public frmInsertProgramacao()
        {
            geral.VoltaForm = "";
            geral.CodigoCliente = 0;
            geral.CodigoResiduo = 0;
            geral.CodigoCaminhao = 0;
            geral.CodigoMotorista = 0;
            InitializeComponent();
            intQuantidade.VALOR.Text = "1,00";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                btnOk.Enabled = false;
                this.Enabled = false;
            }
            finally
            {
                DialogResult dlgResult = new DialogResult();
                dlgResult = DialogResult.Yes;
                if (btnInserirRetirar.Enabled)
                {
                    dlgResult = DialogResult.No;
                    dlgResult = MessageBox.Show("Você precisa programar uma Retirada. Continuar assim mesmo?", "Retirar", MessageBoxButtons.YesNo);
                }
                // Salvar programação manual
                if (txtSolicitante.Text == "")
                {
                    MessageBox.Show("Nome do solicitante inválido!");
                    txtSolicitante.Focus();
                }
                else if (cliente1.txtCodigo.Text == "")
                {
                    MessageBox.Show("Código do cliente inválido!");
                    cliente1.txtCodigo.Focus();
                }
                else if (residuo1.txtCodigo.Text == "")
                {
                    MessageBox.Show("Código do resíduo inválido!");
                    residuo1.txtCodigo.Focus();
                }
                else if (txtServicoAExecutar.Text == "")
                {
                    MessageBox.Show("Serviço a executar inválido!");
                    txtServicoAExecutar.Focus();
                }
                else if (dlgResult == DialogResult.Yes)
                {
                    clsProgramacaoDados oProgramacaoDados = new clsProgramacaoDados();
                    if (caminhao1.txtCodigo.Text != "")
                        oProgramacaoDiaria.CodigoCaminhao = Convert.ToInt32(caminhao1.txtCodigo.Text);
                    oProgramacaoDiaria.CodigoCliente = Convert.ToInt32(cliente1.txtCodigo.Text);
                    oProgramacaoDiaria.CorObservacao = "";
                    oProgramacaoDiaria.Data = dtpData.Text;
                    oProgramacaoDiaria.DataProgramada = dtpDataProgramada.Text;
                    if (residuo1.txtCodigo.Text != "")
                        oProgramacaoDiaria.CodigoResiduo = Convert.ToInt32(residuo1.txtCodigo.Text);
                    oProgramacaoDiaria.ExecutarServico = residuo1.txtDescricao.Text;
                    oProgramacaoDiaria.Hora = txtHora.Text;
                    if (caminhao1.txtCodigo.Text != "")
                        oProgramacaoDiaria.CodigoCaminhao = Convert.ToInt16(caminhao1.txtCodigo.Text);
                    oProgramacaoDiaria.ModeloCaminhao = caminhao1.txtDescricao.Text;
                    oProgramacaoDiaria.NomeCliente = cliente1.txtDescricao.Text;
                    if (funcionario1.txtCodigo.Text != "")
                        oProgramacaoDiaria.CodigoMotorista = Convert.ToInt16(funcionario1.txtCodigo.Text);
                    oProgramacaoDiaria.NomeMotorista = funcionario1.txtDescricao.Text;
                    oProgramacaoDiaria.Observacao = cboTabMotivosOBS.Text;
                    oProgramacaoDiaria.DestinoFinal = cboDestinoFinal.Text;
                    oProgramacaoDiaria.Quadro = 2;
                    if (intQuantidade.VALOR.Text.IndexOf(".") >= 0)
                        oProgramacaoDiaria.Quantidade = Convert.ToInt32(intQuantidade.VALOR.Text.Replace(".", "")) / 100;
                    else if (intQuantidade.VALOR.Text.IndexOf(",") >= 0)
                        oProgramacaoDiaria.Quantidade = Convert.ToInt32(intQuantidade.VALOR.Text.Replace(",", "")) / 100;
                    else
                        oProgramacaoDiaria.Quantidade = Convert.ToInt32(intQuantidade.VALOR.Text);
                    oProgramacaoDiaria.ServicoExecutado = txtServicoAExecutar.Text;
                    oProgramacaoDiaria.Solicitante = txtSolicitante.Text;
                    oProgramacaoDiaria.StatusCor = lblStatus.Text;
                    oProgramacaoDiaria.TipoProgramacao = 2;
                    oProgramacaoDiaria.Unidade = txtUnidade.Text;
                    if (this.Text == "Programação manual - alteração")
                    {
                        //salvar alteração na programação manual
                        oProgramacaoDados.Alterar(oProgramacaoDiaria, oProgramacaoDiaria.Sequencial);
                        geral.VoltaForm = "ProgramaçãoAlterada";
                        int iRepExisteSeq = oReprogramacaoDados.DadoExisteRetornaSequencial(oProgramacaoDiaria.AnoMesDia, oProgramacaoDiaria.Hora, oProgramacaoDiaria.CodigoCliente);
                        if (iRepExisteSeq == 0)
                            SalvarReprogramacaoManualFutura(true);
                        else if (iRepExisteSeq > 0)
                            SalvarReprogramacaoManualFutura(true, true, iRepExisteSeq);
                        //if (e != EventArgs.Empty) 
                        this.Close();
                    }
                    else
                    {
                        if (oProgramacaoDiaria.StatusCor == "8427929" || oProgramacaoDiaria.StatusCor == "16761024")
                        {
                            try
                            {
                                oProgramacaoDados.Inserir(oProgramacaoDiaria);
                                SalvarReprogramacaoManualFutura(true);
                                
                                if (pInsercaoEmProgramacaoFechada)
                                {
                                    clsAvisoInsercaoProgramacaoFechada oAviso = new clsAvisoInsercaoProgramacaoFechada();
                                    clsAvisoInsercaoProgFechadaDados oAvisoDados = new clsAvisoInsercaoProgFechadaDados();
                                    oAviso.Data = DateTime.Now.ToShortDateString();
                                    oAviso.DataExecutado = dtpDataProgramada.Text;
                                    oAviso.Mensagem = "";
                                    oAviso.Mensagem = oAviso.Mensagem + "Na Data de: " + DateTime.Now.ToShortDateString() + " \n";
                                    oAviso.Mensagem = oAviso.Mensagem + "Solicitante: " + txtSolicitante.Text + " \n";
                                    oAviso.Mensagem = oAviso.Mensagem + "Cliente (" + Convert.ToInt32(cliente1.txtCodigo.Text).ToString("000000") + "): " + cliente1.txtDescricao.Text + " \n";
                                    oAviso.Mensagem = oAviso.Mensagem + "Resíduo (" + Convert.ToInt32(residuo1.txtCodigo.Text).ToString("000000") + "): " + residuo1.txtDescricao.Text + " \n";
                                    oAviso.Mensagem = oAviso.Mensagem + "Data executado: " + dtpDataProgramada.Text + " \n";
                                    oAviso.Mensagem = oAviso.Mensagem + "Serviço executado: " + txtServicoAExecutar.Text + " \n";
                                    oAviso.Mensagem = oAviso.Mensagem + "Caminhão: " + caminhao1.txtDescricao.Text + " \n";
                                    oAviso.Mensagem = oAviso.Mensagem + "Motorista: " + funcionario1.txtDescricao.Text + " \n";
                                    oAviso.Mensagem = oAviso.Mensagem + "Observação: " + cboTabMotivosOBS.Text + " \n";
                                    oAviso.Usuario = geral.UsuarioAtual;
                                    oAvisoDados.Inserir(oAviso);
                                    pInsercaoEmProgramacaoFechada = false;
                                }
                            }
                            finally
                            {
                                geral.VoltaForm = "ProgramaçãoInserida";
                                lblNrSequencial.Text = oProgramacaoDados.SequencialAdd(oProgramacaoDiaria.AnoMesDia, oProgramacaoDiaria.CodigoCliente, oProgramacaoDiaria.Hora);
                            }
                            //if (e != EventArgs.Empty)
                            this.Close();
                        }
                        if (oProgramacaoDiaria.StatusCor == "8421631" && oProgramacaoDiaria.Sequencial > 0)
                        {
                            if (cboTabMotivosOBS.Text == "")
                                MessageBox.Show("Observação inválida!");
                            else
                            {
                                oStatusCorProg.SequencialProgramacao = oProgramacaoDiaria.Sequencial;
                                oStatusCorProg.StatusCor = "8421631";
                                oStatusCorProgDados.Inserir(oStatusCorProg);

                                oProgramacaoDados.Alterar(oProgramacaoDiaria, oProgramacaoDiaria.Sequencial);

                                SalvarReprogramacaoManualFutura(false);
                                //if (e != EventArgs.Empty)
                                this.Close();
                            }
                        }
                    }
                }
            }
        }
        private void SalvarReprogramacaoManualFutura(bool pManualFutura = false, bool pAlterar = false, int pSequencial = 0)
        {
            //Salvando na tabela de reprogramação
            oReprogramacao.AnoMesDia = oProgramacaoDiaria.AnoMesDia;
            oReprogramacao.CodigoCaminhao = oProgramacaoDiaria.CodigoCaminhao;
            oReprogramacao.CodigoCliente = oProgramacaoDiaria.CodigoCliente;
            oReprogramacao.CodigoMotorista = oProgramacaoDiaria.CodigoMotorista;
            oReprogramacao.CodigoResiduo = oProgramacaoDiaria.CodigoResiduo;
            oReprogramacao.Data = oProgramacaoDiaria.Data;
            oReprogramacao.ExecutarServico = oProgramacaoDiaria.ExecutarServico;
            oReprogramacao.DataProgramada = oProgramacaoDiaria.DataProgramada;
            oReprogramacao.Hora = oProgramacaoDiaria.Hora;
            oReprogramacao.HoraProgramada = oProgramacaoDiaria.ServicoExecutado;
            oReprogramacao.Observacao = oProgramacaoDiaria.Observacao;
            oReprogramacao.DestinoFinal = oProgramacaoDiaria.DestinoFinal;
            oReprogramacao.Quantidade = oProgramacaoDiaria.Quantidade;
            oReprogramacao.SequencialProgramacaoDiaria = oProgramacaoDiaria.Sequencial;
            oReprogramacao.Solicitante = oProgramacaoDiaria.Solicitante;
            if (pManualFutura)
                oReprogramacao.StatusCor = "8427929";
            else
                oReprogramacao.StatusCor = "8421631";
            oReprogramacao.TipoProgramacao = oProgramacaoDiaria.TipoProgramacao;
            oReprogramacao.Unidade = oProgramacaoDiaria.Unidade;
            if (pAlterar)
                oReprogramacaoDados.Alterar(oReprogramacao, pSequencial);
            else
                oReprogramacaoDados.Inserir(oReprogramacao);
        }
        private void frmInsertProgramacao_Load(object sender, EventArgs e)
        {
            if (lblStatus.Text == "8427929")
                lblNrSequencial.Text = "000000";

            clsTABMotivosOBSDados oTabMotivosObs = new clsTABMotivosOBSDados();
            foreach (DataRow _dr in oTabMotivosObs.PegaDados(0).Rows)
                cboTabMotivosOBS.Items.Add(_dr["Descricao"]);
            PreencheCamposUnidadeDestinoFinal();
            btnInserirRetirar.Enabled = false;
        }

        private void PreencheCamposUnidadeDestinoFinal()
        {
            if (residuo1.txtCodigo.Text != "")
            {
                oRes = oResDados.PegaDados(oRes, Convert.ToInt32(residuo1.txtCodigo.Text));
                txtUnidade.Text = oRes.Unidade;
                cboDestinoFinal.Items.Clear();
                cboDestinoFinal.Items.Add(oRes.DescricaoDestinoFinal);
                cboDestinoFinal.Items.Add("DTR-BROOKS");
                cboDestinoFinal.Items.Add("");
                cboDestinoFinal.Text = oRes.DescricaoDestinoFinal;
            }
        }
        private void residuo1_Leave(object sender, EventArgs e)
        {
            SILCNegocios.clsContratoResiduos oContratoResiduos = new SILCNegocios.clsContratoResiduos();
            clsContratoResiduosDados oContratoResiduosDados = new clsContratoResiduosDados();
            SILCNegocios.clsContratos oContrato = new SILCNegocios.clsContratos();
            clsContratosDados oContratoDados = new clsContratosDados();
            clsContratosReajustesDados oContratoReajustesDados = new clsContratosReajustesDados();
            DataTable _dt = new DataTable();

            if (cliente1.txtCodigo.Text != "" && residuo1.txtCodigo.Text != "")
            {
                clsResiduos oRes = new clsResiduos();
                clsResiduoDados oResDados = new clsResiduoDados();
                if (residuo1.txtCodigo.Text != "")
                {
                    oResDados.PegaDados(oRes, Convert.ToInt32(residuo1.txtCodigo.Text));
                    if (oRes.Ativo == 0)
                    {
                        MessageBox.Show("Resíduo inativo!");
                        residuo1.txtCodigo.Focus();
                    }
                    else if (oRes.CodigoGrupoResiduo == 0)
                    {
                        MessageBox.Show(oRes.DescricaoReduzida + " é um Grupo de Resíduo. Utilize o Resíduo que pertence a esse Grupo.");
                        residuo1.txtCodigo.Focus();
                    }
                }
                oContrato = oContratoDados.PegaDados(oContrato, 0, Convert.ToInt32(cliente1.txtCodigo.Text));
                if (oContrato.Codigo > 0)
                {
                    _dt = oContratoReajustesDados.PreencheDataTable("Data desc", oContrato.Codigo);
                    if (_dt.Rows.Count > 0)
                    {
                        string DataUltimoReajuste = _dt.Rows[0]["Data"].ToString();
                        _dt = oContratoResiduosDados.PreencheDataTableContratoResiduos("CodigoResiduo", oContrato.Codigo, DataUltimoReajuste, oContrato.CodigoCliente);
                        if (residuo1.txtCodigo.Text != "")
                        {
                            DataRow[] ddr = _dt.Select("CodigoResiduo = " + residuo1.txtCodigo.Text);
                            if (ddr.Length > 0)
                                if (ddr[0]["QuantidadeFranquia"].ToString().IndexOf(",") > 0)
                                    intQuantidade.VALOR.Text = ddr[0]["QuantidadeFranquia"].ToString().Split(","[0])[0];
                                else
                                    intQuantidade.VALOR.Text = ddr[0]["QuantidadeFranquia"].ToString();
                        }
                    }
                }
            }

            PreencheCamposUnidadeDestinoFinal();

        }

        private void dtpDataProgramada_Leave(object sender, EventArgs e)
        {
            //if (dtpDataProgramada.Value <= dtpProgramacaoAberta.Value)
            //{ 
            //    // data programada menor que a data programacao aberta
            //    MessageBox.Show("Data programada inválida!");
            //    dtpDataProgramada.Focus();
            //}
        }

        private void cliente1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                frmProcura frmListaClientes = new frmProcura("CLIENTE");
                frmListaClientes.ShowDialog();
            }
        }

        private void residuo1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                frmProcura frmListaResiduos = new frmProcura("RESIDUO");
                frmListaResiduos.ShowDialog();
            }

        }

        private void cliente1_Enter(object sender, EventArgs e)
        {
            geral.VoltaForm = "CLIENTE";
        }

        private void residuo1_Enter(object sender, EventArgs e)
        {
            geral.VoltaForm = "RESIDUO";
        }

        private void caminhao1_Enter(object sender, EventArgs e)
        {
            geral.VoltaForm = "CAMINHAO";
        }

        private void funcionario1_Enter(object sender, EventArgs e)
        {
            geral.VoltaForm = "FUNCIONARIO";
        }

        private void cboTabMotivosOBS_Leave(object sender, EventArgs e)
        {
            cboTabMotivosOBS.Text = cboTabMotivosOBS.Text.ToUpper();
        }

        private void cboDestinoFinal_Leave(object sender, EventArgs e)
        {
            cboDestinoFinal.Text = cboDestinoFinal.Text.ToUpper();
        }

        private void OperacaoDeixarInserirRetirada()
        {
            if (txtServicoAExecutar.Text.ToUpper().IndexOf("COLOCAR") > -1 && 
                dtpDataRetirar.Value >= dtpProgramacaoAberta.Value && dtpDataProgramada.Enabled)
            {
                btnInserirRetirar.Enabled = true;
                //dtpDataRetirar.Enabled = true; // está dando problema quando altera data a retirar, depois que altera
            }
            else
            {
                btnInserirRetirar.Enabled = false;
                dtpDataRetirar.Enabled = false;
            }
        }

        private void dtpDataRetirar_ValueChanged(object sender, EventArgs e)
        {
            OperacaoDeixarInserirRetirada();
        }

        private void txtServicoAExecutar_KeyPress(object sender, KeyPressEventArgs e)
        {
            OperacaoDeixarInserirRetirada();
        }

        private void txtServicoAExecutar_Leave(object sender, EventArgs e)
        {
            OperacaoDeixarInserirRetirada();
        }

        private void btnInserirRetirar_Click(object sender, EventArgs e)
        {
            btnInserirRetirar.Enabled = false;
            try
            {
                btnOk_Click(sender, EventArgs.Empty);
                if (dtpDataRetirar.Value > dtpDataProgramada.Value)
                {
                    try
                    {
                        // insert ServicoFutura colocar
                        // txtServicoAExecutar.Text = "COLOCAR" ou "COLOCAR algo"
                        oProgramacaoDiaria.ServicoExecutado = txtServicoAExecutar.Text;
                        InserirServicosFutura(dtpDataProgramada.Text, residuo1.txtDescricao.Text);
                    }
                    finally
                    {
                        // insert ServicoFutura retirar
                        oProgramacaoDiaria.ServicoExecutado = txtServicoAExecutar.Text.Replace("COLOCAR", "RETIRAR");
                        InserirServicosFutura(dtpDataRetirar.Text, residuo1.txtDescricao.Text, true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                clsProgramacaoDados oProgramacaoDados = new clsProgramacaoDados();
                oProgramacaoDiaria.Data = dtpData.Text;
                oProgramacaoDiaria.DataProgramada = dtpDataRetirar.Text;
                oProgramacaoDiaria.Hora = DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00");
                if (cliente1.txtCodigo.Text != "")
                    oProgramacaoDiaria.CodigoCliente = Convert.ToInt32(cliente1.txtCodigo.Text);
                oProgramacaoDiaria.ExecutarServico = txtServicoAExecutar.Text;
                if (oProgramacaoDados.ExisteProgramacao(Convert.ToInt32(Convert.ToDateTime(oProgramacaoDiaria.Data).ToString("yyyyMMdd")),
                                                        oProgramacaoDiaria.CodigoCliente, oProgramacaoDiaria.ExecutarServico, "RETIRAR") == "Incluir")
                {
                    oProgramacaoDiaria.AnoMesDia = Convert.ToInt32(Convert.ToDateTime(oProgramacaoDiaria.Data).ToString("yyyyMMdd"));
                    oProgramacaoDiaria.TipoProgramacao = 2;
                    oProgramacaoDiaria.Quadro = 2;
                    if (caminhao1.txtCodigo.Text != "")
                        oProgramacaoDiaria.CodigoCaminhao = Convert.ToInt32(caminhao1.txtCodigo.Text);
                    if (funcionario1.txtCodigo.Text != "")
                        oProgramacaoDiaria.CodigoMotorista = Convert.ToInt32(funcionario1.txtCodigo.Text);
                    if (residuo1.txtCodigo.Text != "")
                        oProgramacaoDiaria.CodigoResiduo = Convert.ToInt32(residuo1.txtCodigo.Text);
                    oProgramacaoDiaria.DestinoFinal = cboDestinoFinal.Text;
                    oProgramacaoDiaria.ExecutarServico = residuo1.txtDescricao.Text;
                    oProgramacaoDiaria.Observacao = cboTabMotivosOBS.Text;
                    if (intQuantidade.VALOR.Text != "")
                        oProgramacaoDiaria.Quantidade = Convert.ToInt32(intQuantidade.VALOR.Text.Replace(",00", ""));
                    oProgramacaoDiaria.Solicitante = txtSolicitante.Text;
                    oProgramacaoDiaria.StatusCor = geral.RetornaCodigoCor("Cinza");
                    oProgramacaoDiaria.Unidade = txtUnidade.Text;
                    oProgramacaoDiaria.ServicoExecutado = txtServicoAExecutar.Text.Replace("COLOCAR", "RETIRAR");
                    try
                    {
                        oProgramacaoDados.Inserir(oProgramacaoDiaria, false, true);
                        // insert ServicoFutura 
                        InserirServicosFutura(oProgramacaoDiaria.DataProgramada, residuo1.txtDescricao.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        txtHora.Text = oProgramacaoDiaria.Hora;
                        if (Convert.ToDateTime(dtpDataRetirar.Text) >= Convert.ToDateTime(oProgramacaoDiaria.Data))
                        {
                            clsReprogramacaoServicos oReprogramacao = new clsReprogramacaoServicos();
                            oReprogramacao.AnoMesDia = oProgramacaoDiaria.AnoMesDia;
                            oReprogramacao.DescricaoResiduo = oProgramacaoDiaria.ExecutarServico;
                            oReprogramacao.CodigoCaminhao = oProgramacaoDiaria.CodigoCaminhao;
                            oReprogramacao.CodigoCliente = oProgramacaoDiaria.CodigoCliente;
                            oReprogramacao.CodigoMotorista = oProgramacaoDiaria.CodigoMotorista;
                            oReprogramacao.Data = oProgramacaoDiaria.Data;
                            oReprogramacao.ExecutarServico = oProgramacaoDiaria.ExecutarServico;
                            oReprogramacao.DataProgramada = oProgramacaoDiaria.DataProgramada;
                            oReprogramacao.Hora = txtHora.Text;
                            oReprogramacao.HoraProgramada = txtServicoAExecutar.Text.Replace("COLOCAR", "RETIRAR");
                            oReprogramacao.Observacao = oProgramacaoDiaria.Observacao;
                            oReprogramacao.Quantidade = oProgramacaoDiaria.Quantidade;
                            oReprogramacao.SequencialProgramacaoDiaria = oProgramacaoDiaria.Sequencial;
                            oReprogramacao.Solicitante = oProgramacaoDiaria.Solicitante;
                            oReprogramacao.StatusCor = geral.RetornaCodigoCor("Cinza");
                            oReprogramacao.TipoProgramacao = oProgramacaoDiaria.TipoProgramacao;
                            oReprogramacao.Unidade = oProgramacaoDiaria.Unidade;
                            clsReprogramacaoDados oReprogramacaoDados = new clsReprogramacaoDados();
                            oReprogramacaoDados.Inserir(oReprogramacao);
                            if (dtpDataRetirar.Value > dtpDataProgramada.Value)
                            {
                                // serviço de retirada no futuro
                                oProgramacaoDiaria.ServicoExecutado = txtServicoAExecutar.Text.Replace("COLOCAR", "RETIRAR");
                                InserirServicosFutura(dtpDataRetirar.Text, residuo1.txtDescricao.Text);
                            }
                        }
                    }
                    this.Close();
                }
                else
                    MessageBox.Show("Já existe um programação dessa colocação!");
            }
        }
        private void InserirServicosFutura(string pDataProgramada, string pDescricaoResiduo, bool bInserir = false)
        {
            // insert ServicoFutura 
            clsServicosFutura oServicosFutura = new clsServicosFutura();
            clsServicosFuturaDados oServicosFuturaDados = new clsServicosFuturaDados();
            oServicosFutura.CodigoCaminhao = oProgramacaoDiaria.CodigoCaminhao;
            oServicosFutura.CodigoCliente = oProgramacaoDiaria.CodigoCliente;
            oServicosFutura.CodigoMotorista = oProgramacaoDiaria.CodigoMotorista;
            oServicosFutura.CodigoResiduo = oProgramacaoDiaria.CodigoResiduo;
            oServicosFutura.DataProgramada = pDataProgramada;
            oServicosFutura.MapaMarcado = 0;
            if (oProgramacaoDiaria.RotaMapa == "X")
                oServicosFutura.MapaMarcado = 1;
            oServicosFutura.Observacao = oProgramacaoDiaria.Observacao;
            oServicosFutura.DestinoFinal = oProgramacaoDiaria.DestinoFinal;
            oServicosFutura.DescricaoResiduo = pDescricaoResiduo;
            oServicosFutura.ServicoAExecutar = oProgramacaoDiaria.ServicoExecutado;
            oServicosFutura.Hora = oProgramacaoDiaria.Hora;
            oServicosFutura.Solicitante = oProgramacaoDiaria.Solicitante;
            if (bInserir)
            {
                oServicosFuturaDados.Inserir(oServicosFutura);
            }
            else if (oServicosFuturaDados.DadoExiste(oServicosFutura.CodigoCliente,
                                                oServicosFutura.CodigoResiduo,
                                                oServicosFutura.DataProgramada,
                                                oServicosFutura.DescricaoResiduo, oServicosFutura.Hora) == "Incluir")
            {
                oServicosFuturaDados.Inserir(oServicosFutura);
            }
            else
            {
                oServicosFuturaDados.Alterar(oServicosFutura, oServicosFutura.CodigoCliente, oServicosFutura.DataProgramada, oServicosFutura.Hora, false);
            }
        }

        private void cliente1_Leave(object sender, EventArgs e)
        {
            if (cliente1.txtCodigo.Text != "" && cliente1.txtCodigo.Text != null)
            {
                oClientes = new clsClientes();
                oClientes = oClienteDados.PegaDados(oClientes, Convert.ToInt32(cliente1.txtCodigo.Text));
                if (oClientes.Inativo == 1)
                {
                    MessageBox.Show("Cliente inativo!");
                    cliente1.txtCodigo.Focus();
                }
            }
        }
    }
}
