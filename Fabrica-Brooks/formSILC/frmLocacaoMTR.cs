using System;
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
    public partial class frmLocacaoMTR : Form
    {
        public SILCNegocios.clsLancamentos oLancamento = new SILCNegocios.clsLancamentos();
        public SILCNegocios.clsLancamentoMTR oLancamentoMTR = new SILCNegocios.clsLancamentoMTR();
        private SILCNegocios.clsResiduos oRes = new SILCNegocios.clsResiduos();
        private SILCNegocios.clsDestinoFinal oDestinoFinal = new SILCNegocios.clsDestinoFinal();
        private clsDestinoFinalDados oDestinoFinalDados = new clsDestinoFinalDados();
        private clsResiduoDados oResDados = new clsResiduoDados();
        private BindingSource bindingSource = new BindingSource();

        public int pCodigoCliente = 0;
        public frmLocacaoMTR()
        {
            InitializeComponent();            
        }
        private void frmLocacaoMTR_Load(object sender, EventArgs e)
        {
            if (moeQuantidadeColetada.VALOR.Text == "")
                moeQuantidadeColetada.VALOR.Text = "1,00";
            residuo1.txtCodigo.TabIndex = residuo1.TabIndex;
            residuo1.txtDescricao.TabIndex = residuo1.TabIndex;
            cboDestino.Items.Add("DTR");
            foreach (DataRow _dr in oDestinoFinalDados.PreencheDataTableAterro("Codigo", true).Rows)
            {
                if (_dr["NomeFantasia"].ToString().Length > 15)
                    cboDestino.Items.Add(_dr["Codigo"].ToString() + "-" + _dr["NomeFantasia"].ToString().Substring(0, 15));
                else
                    cboDestino.Items.Add(_dr["Codigo"].ToString() + "-" + _dr["NomeFantasia"].ToString());
            }
            bool bAchou = false;
            foreach (string _proc in cboDestino.Items)
            {
                if (_proc == cboDestino.Text)
                {
                    bAchou = true;
                    break;
                }
            }
            if (!bAchou)
            {
                cboDestino.Items.Add(cboDestino.Text);
            }
        }
        private void SalvarLog(string pOperacao, string pLog)
        {
            clsLog oLog = new clsLog();
            clsLogDados oLogDados = new clsLogDados();
            oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
            oLog.CodigoUsuario = geral.CodigoUsuarioAtual;
            oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
            oLog.LocalOperacao = "Locações-Resíduos";
            oLog.Operacao = pOperacao;
            oLog.Log = pLog;
            oLogDados.Inserir(oLog);
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            // Salvar Lancamento MTR
            if (intNumeroMTR.VALOR.Text == "")
            {
                MessageBox.Show("Número MTR inválida!");
                intNumeroMTR.VALOR.Focus();
            }
            else if (residuo1.txtCodigo.Text == "")
            {
                MessageBox.Show("Código do resíduo inválido!");
                residuo1.txtCodigo.Focus();
            }
            else
            {
                if (residuo1.txtCodigo.Text != "")
                {
                    oRes = new clsResiduos();
                    oResDados = new clsResiduoDados();
                    oResDados.PegaDados(oRes, Convert.ToInt32(residuo1.txtCodigo.Text));
                    if (oRes.Ativo == 0)
                    {
                        MessageBox.Show("Resíduo inativado!");
                        residuo1.txtCodigo.Focus();
                    }
                    else
                    {
                        if (pCodigoCliente > 0)
                        {
                            // verificar se MTR eletrônica pertence a outro cliente, caso encontre não pode incluir
                            clsLancamentoMTRDados oLancamentoMTRDados2 = new clsLancamentoMTRDados();
                            if (oLancamentoMTRDados2.MTReExisteEmClienteDiferente(intNumeroMTRe.Text, pCodigoCliente) && intNumeroMTRe.Text != "-1" && intNumeroMTRe.Text != "0")
                            {
                                MessageBox.Show("Não é possível Salvar a MTR-e " + intNumeroMTRe.Text + " está em cliente diferente!");
                            }
                            else
                            {
                                oLancamentoMTR.NumeroLancamento = Convert.ToInt32(lblNrLancamento.Text);
                                oLancamentoMTR.NumeroMTR = Convert.ToInt32(intNumeroMTR.VALOR.Text);
                                clsLancamentoMTRDados oLancamentoMTRDados = new clsLancamentoMTRDados();
                                if (residuo1.txtCodigo.Text != "")
                                    oLancamentoMTR.CodigoResiduo = Convert.ToInt32(residuo1.txtCodigo.Text);
                                oLancamentoMTR.Unidade = txtUnidade.Text;
                                oLancamentoMTR.Franquia = Convert.ToDecimal(moeQuantidadeColetada.VALOR.Text);
                                if (moeQuantidadeDescarregada.VALOR.Text == "")
                                    moeQuantidadeDescarregada.VALOR.Text = "0";
                                if (moeQuantidadeDescarregada.VALOR.Text != "")
                                    oLancamentoMTR.Quantidade = Convert.ToDecimal(moeQuantidadeDescarregada.VALOR.Text);
                                oLancamentoMTR.Ticket = txtTicket.Text;
                                if (moeValorTotal.VALOR.Text == "")
                                    moeValorTotal.VALOR.Text = "0";
                                oLancamentoMTR.ValorTotal = Convert.ToDecimal(moeValorTotal.VALOR.Text);
                                if (moeValorUnitario.VALOR.Text == "")
                                    moeValorUnitario.VALOR.Text = "0";
                                oLancamentoMTR.ValorUnitario = Convert.ToDecimal(moeValorUnitario.VALOR.Text);
                                oLancamentoMTR.observacao = txtObservacao.Text;
                                if (intNumeroMTRe.Text == "")
                                    intNumeroMTRe.Text = "0";
                                oLancamentoMTR.NumeroMTRFatima = Convert.ToInt64(intNumeroMTRe.Text);
                                oLancamentoMTR.Motivo = txtMotivoMTRe.Text;
                                oLancamentoMTR.Deposito = cboDestino.Text;
                                oLancamentoMTR.DataDescarga = dtpDataDescarga.Text;
                                oLancamentoMTR.DescargaMTRe = txtDescargaMTRe.Text;
                                oLancamentoMTR.ControleInternoDescarga = txtControleInternoDescarga.Text;

                                if (cboDestino.Text.Split("-"[0]).ToString() == "DTR")
                                    oLancamentoMTR.CodigoAterroSanitario = Convert.ToInt32(cboDestino.Text.Split("-"[0])[0]);
                                else
                                    oLancamentoMTR.CodigoAterroSanitario = 0;
                                string slog = "";
                                slog = slog + "Nº Lançamento: " + lblNrLancamento.Text + " Código resíduo: " + oLancamentoMTR.CodigoResiduo + "\n";
                                slog = slog + "Nº MTR: " + oLancamentoMTR.NumeroMTR + "\n";
                                slog = slog + "Código resíduo: " + oLancamentoMTR.CodigoResiduo + "\n";
                                slog = slog + "Qtde: " + oLancamentoMTR.Franquia + "\n";
                                slog = slog + "DataDescarga: " + oLancamentoMTR.DataDescarga + "\n";
                                slog = slog + "Descarga MTR-e: " + oLancamentoMTR.DescargaMTRe + "\n";
                                slog = slog + "Controle Interno Descarga: " + oLancamentoMTR.ControleInternoDescarga + "\n";
                                slog = slog + "Qtde descarga: " + oLancamentoMTR.Quantidade + "\n";
                                slog = slog + "Unidade: " + oLancamentoMTR.Unidade + "\n";
                                slog = slog + "Destino: " + oLancamentoMTR.Deposito + "\n";
                                slog = slog + "Nº Ticket: " + oLancamentoMTR.Ticket + "\n";
                                slog = slog + "Valor Unitário: " + oLancamentoMTR.ValorUnitario + "\n";
                                slog = slog + "Valor Total: " + oLancamentoMTR.ValorTotal + "\n";
                                slog = slog + "Nº MTR-e: " + oLancamentoMTR.NumeroMTRFatima + "\n";
                                slog = slog + "Observação: " + oLancamentoMTR.observacao + "\n";
                                if (oLancamentoMTRDados.DadoExiste(oLancamentoMTR.NumeroLancamento, oLancamentoMTR.NumeroMTR, oLancamentoMTR.CodigoResiduo) == "Alterar")
                                {
                                    SalvarLog("Alteração", slog);
                                    oLancamentoMTRDados.Alterar(oLancamentoMTR, oLancamentoMTR.NumeroLancamento, oLancamentoMTR.NumeroMTR, oLancamentoMTR.CodigoResiduo);
                                }
                                else
                                {
                                    SalvarLog("Inclusão", slog);
                                    oLancamentoMTRDados.Inserir(oLancamentoMTR);
                                }

                                clsAterroSanitario oAterroSanitario = new clsAterroSanitario();
                                clsAterroSanitarioDados oAterroSanitarioDados = new clsAterroSanitarioDados();
                                // gravar aterro sanitario
                                if (cboDestino.Text != "" && geral.Left(cboDestino.Text, 3) != "DTR" && cboDestino.Text.IndexOf("-") > -1)
                                {
                                    if (geral.IsNumeric(cboDestino.Text.Split("-"[0])[0]))
                                        oAterroSanitario.CodigoAterro = Convert.ToInt32(cboDestino.Text.Split("-"[0])[0]);
                                }
                                oAterroSanitario.Data = dtpDataDescarga.Text;
                                oAterroSanitario.Hora = txtHoraDescarga.Text;
                                oAterroSanitario.NumeroTicket = txtTicket.Text;
                                oAterroSanitario.CodigoMotorista = oLancamento.CodigoMotoristaRetirou;
                                oAterroSanitario.CodigoCaminhao = oLancamento.CodigoCaminhaoColoca;
                                oAterroSanitario.CodigoCliente = oLancamento.CodigoCliente;
                                oAterroSanitario.NumeroCaixa = oLancamento.NumeroCaixa;
                                if (oLancamentoMTR.Unidade.ToUpper() == "KG")
                                    oAterroSanitario.TotalPeso = oLancamentoMTR.Quantidade;
                                else
                                    oAterroSanitario.TotalPeso = 0;
                                oAterroSanitario.NumeroLancamento = oLancamentoMTR.NumeroLancamento;
                                if (intNumeroMTR.VALOR.Text != "")
                                    oAterroSanitario.NumeroMTR = Convert.ToInt32(intNumeroMTR.VALOR.Text);
                                if (residuo1.txtCodigo.Text != "")
                                    oAterroSanitario.CodigoResiduo = Convert.ToInt32(residuo1.txtCodigo.Text);

                                if (oAterroSanitario.NumeroTicket != "" && oAterroSanitario.CodigoAterro > 0 && oAterroSanitario.NumeroLancamento > 0)
                                {
                                    //int iCodigoAterroSanitarioOuCodigoErro = oAterroSanitarioDados.ExisteLancamentoNoAterroSanitario(oLancamentoMTR.NumeroLancamento, oLancamento.CodigoCliente, oLancamentoMTR.CodigoResiduo);
                                    int iCodigoAterroSanitarioOuCodigoErro = oAterroSanitarioDados.ExisteLancamentoNoAterroSanitario(oLancamentoMTR.NumeroLancamento, oLancamentoMTR.CodigoResiduo);
                                    if (iCodigoAterroSanitarioOuCodigoErro > 0)
                                        oAterroSanitarioDados.Alterar(oAterroSanitario, iCodigoAterroSanitarioOuCodigoErro);
                                    else if (iCodigoAterroSanitarioOuCodigoErro == 0)
                                        oAterroSanitarioDados.Inserir(oAterroSanitario);
                                    else if (iCodigoAterroSanitarioOuCodigoErro == -1)
                                        MessageBox.Show("Erro ao inserir/alterar na Tabela AterroSanitario");
                                }
                                this.Close();
                            }
                        }
                    }
                }
            }
        }

        private void txtCliente_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                frmProcura frmListaClientes = new frmProcura("CLIENTE");
                frmListaClientes.ShowDialog();
            }
        }

        private void residuo1_Leave(object sender, EventArgs e)
        {
            oRes = new clsResiduos();
            if (residuo1.txtCodigo.Text == "" || residuo1.txtCodigo.Text == "0")
            {
                MessageBox.Show("Código do Resíduo inválido!");
                residuo1.txtCodigo.Text = "";
                residuo1.txtCodigo.Focus();
            }
            else if (residuo1.txtCodigo.Text != "")
            {
                oRes = new clsResiduos();
                oResDados = new clsResiduoDados();
                oResDados.PegaDados(oRes, Convert.ToInt32(residuo1.txtCodigo.Text));
                if (oRes.Ativo == 0)
                {
                    MessageBox.Show("Resíduo inativo!");
                    residuo1.txtCodigo.Focus();
                }
                else
                    txtUnidade.Text = residuo1.Unidade;
            }
        }

        private void residuo1_Enter(object sender, EventArgs e)
        {
            geral.VoltaForm = this.Name + "RESIDUO";
            geral.CodigoResiduo = 0;
            geral.CodigoCliente = pCodigoCliente;
        }

        private void cboDestino_Leave(object sender, EventArgs e)
        {
            SILCNegocios.clsDestinoFinal oAterro = new SILCNegocios.clsDestinoFinal();
            clsDestinoFinalDados oAterroDados = new clsDestinoFinalDados();
            string CodDestino = "";
            cboDestino.Text = cboDestino.Text.ToUpper();
            if (cboDestino.Text != "" && geral.Left(cboDestino.Text, 3) != "DTR")
            {
                if (cboDestino.Text.IndexOf("-") > -1)
                {
                    if (geral.IsNumeric(cboDestino.Text.Split("-"[0])[0]))
                        CodDestino = cboDestino.Text.Split("-"[0])[0];
                }
                else
                {
                    if (geral.IsNumeric(cboDestino.Text))
                        CodDestino = cboDestino.Text;
                }
                if (CodDestino != "")
                {
                    oAterroDados.PegaDados(oAterro, Convert.ToInt32(CodDestino));
                    if (oAterro.NomeFantasia != null)
                    {
                        if (oAterro.NomeFantasia.Length > 15)
                            cboDestino.Text = oAterro.Codigo + "-" + oAterro.NomeFantasia.Substring(0, 15);
                        else
                            cboDestino.Text = oAterro.Codigo + "-" + oAterro.NomeFantasia;

                        if (oAterro.Ativo == 0)
                        {
                            MessageBox.Show("Destino inativo!");
                            cboDestino.Text = "";
                            cboDestino.Focus();
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Destino inválido!");
                        cboDestino.Text = "";
                        cboDestino.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Destino inválido!");
                    cboDestino.Text = "";
                    cboDestino.Focus();
                }
            }
            else if (geral.Left(cboDestino.Text, 3) == "DTR")
            {
                frmLocalDTR ofrmLocalDTR = new frmLocalDTR();
                ofrmLocalDTR.cboDTRLocal.Text = cboDestino.Text.Replace("DTR-", "");
                ofrmLocalDTR.ShowDialog();
                if (ofrmLocalDTR.cboDTRLocal.Text.IndexOf("DTR-") > -1)
                    cboDestino.Text = ofrmLocalDTR.cboDTRLocal.Text;
                else
                    cboDestino.Text = "DTR-" + ofrmLocalDTR.cboDTRLocal.Text;
            }
            cboDestino.Text = cboDestino.Text.ToUpper();
        }

        private void txtHoraDescarga_Leave(object sender, EventArgs e)
        {
            string _sh = geral.VerificaHora(txtHoraDescarga.Text);
            if (_sh == "Hora inválida!")
            {
                MessageBox.Show(_sh);
                txtHoraDescarga.Text = "";
                txtHoraDescarga.Focus();
            }
            else
                txtHoraDescarga.Text = _sh;
        }

        private void txtObservacao_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F && (txtObservacao.Text == "F" || txtObservacao.Text == ""))
            {
                txtObservacao.Text = "FALTA TICKET PESO";
            }
        }

        private void txtTicket_Leave(object sender, EventArgs e)
        {
            if ((txtTicket.Text == "" || geral.Left(cboDestino.Text, 3) == "DTR") && txtHoraDescarga.Text != "")
                MessageBox.Show("Sem o Número do Ticket e ou sem Destino a hora da descarga digitada será desconsidera!");
        }

        private void moeValorUnitario_Leave(object sender, EventArgs e)
        {
            if (moeValorUnitario.VALOR.Text != "" && moeQuantidadeDescarregada.VALOR.Text != "" &&
                moeValorUnitario.VALOR.Text != "0,00" && moeQuantidadeDescarregada.VALOR.Text != "0,00" &&
                moeValorUnitario.VALOR.Text != "0,0000" && moeQuantidadeDescarregada.VALOR.Text != "0,0000")
            {
                decimal dValorTotal = Convert.ToDecimal(moeValorUnitario.VALOR.Text) * Convert.ToDecimal(moeQuantidadeDescarregada.VALOR.Text);
                if (moeValorTotal.VALOR.Text == "" || moeValorTotal.VALOR.Text == "0,00" || moeValorTotal.VALOR.Text == "0,0000")
                    moeValorTotal.VALOR.Text = dValorTotal.ToString("N2");
                else if (Convert.ToDecimal(moeValorTotal.VALOR.Text).ToString("N2") != dValorTotal.ToString("N2"))
                {
                    MessageBox.Show("Valor total calculado diferente do valor atual!");
                    moeValorTotal.VALOR.Text = dValorTotal.ToString("N2");
                }
            }
        }

        private void moeQuantidadeDescarregada_Leave(object sender, EventArgs e)
        {
            if (moeQuantidadeDescarregada.VALOR.Text == "" || moeQuantidadeDescarregada.VALOR.Text == "0,00" || moeQuantidadeDescarregada.VALOR.Text == "0,0000")
            {
                txtObservacao.Text = "FALTA TICKET PESO";
            }
        }

        private void txtDescargaMTRe_Leave(object sender, EventArgs e)
        {
            txtDescargaMTRe.Text = txtDescargaMTRe.Text.ToUpper();
        }

        private void txtControleInternoDescarga_Leave(object sender, EventArgs e)
        {
            txtControleInternoDescarga.Text = txtControleInternoDescarga.Text.ToUpper();
        }
    }
}
