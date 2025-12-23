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
    public partial class frmDistribuicaoDescargaPendente : Form
    {
        clsLancamentosDados oLancamentoDados = new clsLancamentosDados();
        clsLancamentoMTRDados oLancamentoMTRDados = new clsLancamentoMTRDados();
        private BindingSource bindingSource = new BindingSource();
        clsAterroSanitario oAterroSanitario = new clsAterroSanitario();
        clsAterroSanitarioDados oAterroSanitarioDados = new clsAterroSanitarioDados();
        clsCaminhoes oCaminhao = new clsCaminhoes();
        clsDestinoFinal oAterro = new clsDestinoFinal();
        clsDestinoFinalDados oDestinoFinalDados = new clsDestinoFinalDados();

        decimal ValorTotalQuantidadeDescarga;
        decimal a = 0;
        decimal b = 0;
        decimal c = 0;
        decimal vPerc = 0;
        decimal bb = 0;
        decimal cc = 0;
        DataTable _dt = new DataTable();

        public frmDistribuicaoDescargaPendente()
        {
            InitializeComponent();
        }

        private void LimpaCampos()
        {
            caminhao1.txtCodigo.Text = "";
            funcionario1.txtCodigo.Text = "";
            moePesoTicket.VALOR.Text = "";
            lblDiferenca.Text = "";
            lblDiferencaPercentual.Text = "";
            lblPesoTotalColetado.Text = "";
            lblTotalDescargaCalculada.Text = "";
        }

        private void PreencheGrade(bool bPegaPesoTotalColetado = true)
        {
            btnSalvar.Enabled = false;
            btnExcluir.Enabled = false;
            lblDiferencaPercentual.Text = "";
            decimal _PesoTotalEncontrado = 0;
            if (txtTicket.Text != "" && dtpData.Text != "" && destinofinal1.txtCodigo.Text != "")
            {
                clsAterroSanitarioDados oAterroSanitarioDados = new clsAterroSanitarioDados();
                if (bPegaPesoTotalColetado)
                {
                    _PesoTotalEncontrado = oAterroSanitarioDados.PegaPesoTotal(txtTicket.Text, dtpData.Value.Year.ToString());
                    if (_PesoTotalEncontrado > 0)
                    {
                        moePesoTicket.VALOR.Text = _PesoTotalEncontrado.ToString("N2");
                        funcionario1.txtCodigo.Text = oAterroSanitarioDados.PegaCodigoMotoristaQueDescarregou(txtTicket.Text, dtpData.Value.Year.ToString()).ToString();
                        caminhao1.txtCodigo.Text = oAterroSanitarioDados.PegaCodigoCaminhaoQueDescarregou(txtTicket.Text, dtpData.Value.Year.ToString()).ToString();
                        btnExcluir.Enabled = true;
                    }
                }
                lblTotalDescargaCalculada.Text = moePesoTicket.VALOR.Text;
                _dt = oLancamentoDados.PreencheDataTableDescargaPendente(txtTicket.Text, dtpData.Text);
                bindingSource.DataSource = _dt;
                Grade.DataSource = bindingSource.DataSource;
                EstiloGrades(Grade);
                int i = 0;
                double a = 0.00;
                double b = 0.00;
                double c = 0.00;
                double vPerc = 0.000000;
                double vPesoTotalColetado = 0.00;
                foreach (DataRow dr in _dt.Rows)
                {
                    i++;
                    dr[0] = i;

                    if (Convert.ToDouble(dr["QuantidadeDescarga"]) == 0)
                        dr["QuantidadeDescarga"] = dr["QuantidadeColetada"];
                    a = Convert.ToDouble(dr["QuantidadeDescarga"]); 
                    b = Convert.ToDouble(dr["QuantidadeColetada"]); 
                    c = a - b;
                    if (b == 0)
                        b = 0.01;
                    vPerc = c / b * 100;
                    vPesoTotalColetado = vPesoTotalColetado + b;
                    dr["PercentualDiferenca"] = Convert.ToDouble(vPerc.ToString().Replace(".", ",")).ToString("N4");
                }
                Grade.Refresh();
                lblPesoTotalColetado.Text = Convert.ToDouble(vPesoTotalColetado.ToString().Replace(".", ",")).ToString("N2");
                if (moePesoTicket.VALOR.Text != "" && lblPesoTotalColetado.Text != "")
                    lblDiferenca.Text = (Convert.ToDouble(moePesoTicket.VALOR.Text) - Convert.ToDouble(lblPesoTotalColetado.Text)).ToString("N6");
                if (lblDiferenca.Text != "0,00" && lblPesoTotalColetado.Text != "0,00" && lblDiferenca.Text != "" && lblPesoTotalColetado.Text != "")
                    lblDiferencaPercentual.Text = (Convert.ToDouble(lblDiferenca.Text) / Convert.ToDouble(lblPesoTotalColetado.Text) * 100).ToString("N6");
            }
        }
        private void frmListaReprogramacao_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " - Usuário logado: " + geral.UsuarioAtual;
            intSegundo.VALOR.Text = "00";
            btnSalvar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        private void EstiloGrades(DataGridView oGrade)
        {
            oGrade.Columns["Sequencial"].Width = 82;
            oGrade.Columns["Sequencial"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            oGrade.Columns["NumeroLancamento"].Width = 86;
            oGrade.Columns["NumeroLancamento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oGrade.Columns["NumeroLancamento"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            oGrade.Columns["NumeroLancamento"].HeaderText = "Nº Lançamento";

            oGrade.Columns["NumeroMTR"].Width = 76;
            oGrade.Columns["NumeroMTR"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            oGrade.Columns["NumeroMTR"].HeaderText = "Nº MTR";

            oGrade.Columns["CodigoResiduo"].Width = 50;
            oGrade.Columns["CodigoResiduo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oGrade.Columns["CodigoResiduo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            oGrade.Columns["CodigoResiduo"].HeaderText = "Código";

            oGrade.Columns["DescricaoReduzida"].Width = 240;
            oGrade.Columns["DescricaoReduzida"].HeaderText = "Descricao do Resíduo";

            oGrade.Columns["QuantidadeColetada"].HeaderText = "Qt.Coletada";
            oGrade.Columns["QuantidadeColetada"].Width = 88;
            oGrade.Columns["QuantidadeColetada"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oGrade.Columns["QuantidadeColetada"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            oGrade.Columns["QuantidadeColetada"].DefaultCellStyle.Format = "N2";

            oGrade.Columns["QuantidadeDescarga"].HeaderText = "Qt.Descarga";
            oGrade.Columns["QuantidadeDescarga"].Width = 88;
            oGrade.Columns["QuantidadeDescarga"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oGrade.Columns["QuantidadeDescarga"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            oGrade.Columns["QuantidadeDescarga"].DefaultCellStyle.Format = "N2";

            oGrade.Columns["CodigoCliente"].Width = 50;
            oGrade.Columns["CodigoCliente"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            oGrade.Columns["CodigoCliente"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oGrade.Columns["CodigoCliente"].HeaderText = "Código";

            oGrade.Columns["NomeFantasia"].Width = 250;
            oGrade.Columns["NomeFantasia"].HeaderText = "Cliente";

            oGrade.Columns["PercentualDiferenca"].Width = 90;
            oGrade.Columns["PercentualDiferenca"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            oGrade.Columns["PercentualDiferenca"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            oGrade.Columns["PercentualDiferenca"].HeaderText = "% Diferença";
            oGrade.Columns["PercentualDiferenca"].DefaultCellStyle.Format = "N2";

            oGrade.Columns["QuantidadeCalculada"].Visible = false;
        }

        private void txtTicket_Leave(object sender, EventArgs e)
        {
            LimpaCampos();
            PreencheGrade();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            PreencheGrade(false);
            CalcularQuatidadesDescarga();
        }

        private void CalcularQuatidadesDescarga()
        {
            oAterroSanitario = new clsAterroSanitario();
            oCaminhao = new clsCaminhoes();
            oAterro = new clsDestinoFinal();
            if (moePesoTicket.VALOR.Text != "")
                ValorTotalQuantidadeDescarga = Convert.ToDecimal(moePesoTicket.VALOR.Text);
            a = 0;
            b = 0;
            c = 0;
            vPerc = 0;
            bb = 0;
            cc = 0;

            if (moePesoTicket.VALOR.Text == "0" || moePesoTicket.VALOR.Text == "")
                MessageBox.Show("Peso Total inválido!");
            else if (dtpData.Value < Convert.ToDateTime("01/06/2017"))
                MessageBox.Show("Opção válida para descargas depois do dia 01/06/2017.");
            else if (Grade.Rows.Count - 1 <= 0)
                MessageBox.Show("Não há dados!");
            else
            {
                // 1º - verificar se o calculo vai ser percentual parcial ou total
                bool percentualTotal = true;
                int qtComPesoEncontrada = 0;
                decimal ptComPesoEncontrada = 0;
                for (int i = 0; i < Grade.Rows.Count - 1; i++)
                {
                    if (Convert.ToInt32(Grade.Rows[i].Cells[5].Value) > 0)
                    {
                        percentualTotal = false;
                        qtComPesoEncontrada = qtComPesoEncontrada + 1;
                        ptComPesoEncontrada = ptComPesoEncontrada + Convert.ToDecimal(Grade.Rows[i].Cells[5].Value);
                    }
                }
                decimal v1 = 0;
                decimal q1 = 0;
                decimal p1 = 0;

                if (percentualTotal || ValorTotalQuantidadeDescarga == 0)
                {
                    v1 = Convert.ToDecimal(moePesoTicket.VALOR.Text);
                    q1 = Grade.Rows.Count - 1;
                    p1 = 100 / q1; // percentual

                    for (int i = 0; i < Grade.Rows.Count - 1; i++)
                    {
                        Grade.Rows[i].Cells[9].Value = p1.ToString("N2");
                        Grade.Rows[i].Cells[6].Value = Math.Round(v1 * p1 / 100, 2);
                    }
                }
                else
                {
                    v1 = ptComPesoEncontrada - Convert.ToDecimal(moePesoTicket.VALOR.Text);
                    q1 = Grade.Rows.Count - 1 - qtComPesoEncontrada;
                    ValorTotalQuantidadeDescarga = 0;
                    v1 = Convert.ToDecimal(lblPesoTotalColetado.Text);
                    q1 = 1;
                    p1 = (ptComPesoEncontrada - Convert.ToDecimal(moePesoTicket.VALOR.Text)) * 100 / ptComPesoEncontrada;

                    ValorTotalQuantidadeDescarga = 0;
                    for (int i = 0; i < Grade.Rows.Count - 1; i++)
                    {
                        Grade.Rows[i].Cells[10].Value = p1.ToString("N2");
                        Grade.Rows[i].Cells[6].Value = Math.Round(Convert.ToDecimal(Grade.Rows[i].Cells[5].Value) - p1 * Convert.ToDecimal(Grade.Rows[i].Cells[5].Value) / 100, 2);
                        ValorTotalQuantidadeDescarga = ValorTotalQuantidadeDescarga + Convert.ToDecimal(Grade.Rows[i].Cells[6].Value);
                    }
                    lblTotalDescargaCalculada.Text = ValorTotalQuantidadeDescarga.ToString("N2");
                }

                bb = 0;
                cc = 0;
        
                for (int i = 0; i < Grade.Rows.Count - 1; i++)
		        {
                    // Arredondando com 2 casas após a vírgula
                    a = Math.Round(Convert.ToDecimal(Grade.Rows[i].Cells[6].Value), 2); // qt.descarga
                    b = Convert.ToDecimal(Grade.Rows[i].Cells[5].Value); // qt.coleta
                    c = a - b;
                    cc = cc + c;
                    bb = bb + b;
                    if (b > 0)
                    {
                        vPerc = c / b * 100;
                        Grade.Rows[i].Cells[9].Value = vPerc.ToString();
                    }
                }

                if (Convert.ToDecimal(moePesoTicket.VALOR.Text) > 0)
                {
                    lblDiferenca.Text = (Convert.ToDecimal(moePesoTicket.VALOR.Text) - bb).ToString("N2");
                    if (bb > 0)
                        lblDiferencaPercentual.Text = (cc / bb * 100).ToString("N2");
                }
                if (Grade.Rows.Count - 1 > 0)
                    btnSalvar.Enabled = true;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (moePesoTicket.VALOR.Text == "")
                MessageBox.Show("Valor não pode ser zero!");
            else if (destinofinal1.txtCodigo.Text == "")
                MessageBox.Show("Digite o Código do Destino Fiscal!");
            else if (txtTicket.Text == "")
                MessageBox.Show("É necessário digitação do Número do Ticket!");
            else if (caminhao1.txtCodigo.Text == "")
                MessageBox.Show("É necessário o Código do Caminhão!");
            else if (funcionario1.txtCodigo.Text == "")
                MessageBox.Show("É necessário o Código do Motorista!");
            else
            {
                // salvar
                oAterroSanitario = new clsAterroSanitario();
                oAterroSanitarioDados = new clsAterroSanitarioDados();
                if (destinofinal1.txtCodigo.Text != "")
                    oAterroSanitario.CodigoAterro = Convert.ToInt32(destinofinal1.txtCodigo.Text);
                oAterroSanitario.Data = dtpData.Text;
                oAterroSanitario.Hora = intHora.VALOR.Text + ":" + intMinuto.VALOR.Text + ":" + intSegundo.VALOR.Text;
                oAterroSanitario.NumeroTicket = txtTicket.Text;
                if (caminhao1.txtCodigo.Text != "")
                    oAterroSanitario.CodigoCaminhao = Convert.ToInt32(caminhao1.txtCodigo.Text);
                oAterroSanitario.CodigoCliente = 0;
                oAterroSanitario.NumeroCaixa = "";
                if (moePesoTicket.VALOR.Text != "")
                    oAterroSanitario.TotalPeso = Convert.ToDecimal(moePesoTicket.VALOR.Text);
                oAterroSanitario.NumeroLancamento = 0;
                oAterroSanitario.NumeroMTR = 0;
                oAterroSanitario.CodigoResiduo = 999;
                oAterroSanitario.Status = 1;
                oAterroSanitario.Codigo = 0;
                if (funcionario1.txtCodigo.Text != "")
                    oAterroSanitario.CodigoMotorista = Convert.ToInt32(funcionario1.txtCodigo.Text);
                decimal _PesoTotal = oAterroSanitarioDados.PegaPesoTotal(txtTicket.Text, dtpData.Value.Year.ToString());
                if (_PesoTotal > 0)
                {
                    MessageBox.Show("Já existe descarregamento!");
                    btnExcluir.Enabled = true;
                }
                else
                {
                    oAterroSanitarioDados.Inserir(oAterroSanitario);
                    // distribuir
                    foreach (DataGridViewRow dgr in Grade.Rows)
                    {
                        // redistribuir peso novo para LancamentoMTR
                        oLancamentoMTRDados = new clsLancamentoMTRDados();
                        if (dgr.Cells["NumeroLancamento"].Value != null && dgr.Cells["NumeroMTR"].Value != null && 
                            dgr.Cells["CodigoResiduo"].Value != null    && dgr.Cells["QuantidadeDescarga"].Value != null)
                            oLancamentoMTRDados.AlterarQuantidadeDescarrega(Convert.ToInt32(dgr.Cells["NumeroLancamento"].Value),
                                                                            Convert.ToInt32(dgr.Cells["NumeroMTR"].Value),
                                                                            Convert.ToInt32(dgr.Cells["CodigoResiduo"].Value), 
                                                                            Convert.ToDecimal(dgr.Cells["QuantidadeDescarga"].Value), 
                                                                            dtpData.Text);
                    }
                    this.Close();
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DialogResult _result = new DialogResult();
            _result = MessageBox.Show("Confirma exclusão dessa descarga?", "Confirme", MessageBoxButtons.YesNo);
            if (_result == DialogResult.Yes)
            {
                if (oAterroSanitario.NumeroTicket != "" && dtpData.Value.Year > 0)
                    if (oAterroSanitarioDados.ExcluirDescarga(txtTicket.Text, dtpData.Value.Year.ToString()) == "")
                        btnExcluir.Enabled = false;
            }
        }

        private void txtTicket_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == "'"[0])
                e.Handled = true;
        }

        private void destinofinal1_Leave(object sender, EventArgs e)
        {
            lblLocal.Text = "";
            oDestinoFinalDados = new clsDestinoFinalDados();
            oAterro = new clsDestinoFinal();
            if (destinofinal1.txtCodigo.Text != "")
            {
                oDestinoFinalDados.PegaDados(oAterro, Convert.ToInt32(destinofinal1.txtCodigo.Text));
                lblLocal.Text = oAterro.NomeFantasia;
            }
        }

        private void caminhao1_Enter(object sender, EventArgs e)
        {
            geral.VoltaForm = "frmDistribuicaoDescargaPendenteCAMINHAO";
        }

        private void funcionario1_Enter(object sender, EventArgs e)
        {
            geral.VoltaForm = "frmDistribuicaoDescargaPendenteFUNCIONARIO";

        }

        private void destinofinal1_Enter(object sender, EventArgs e)
        {
            geral.VoltaForm = "frmDistribuicaoDescargaPendenteDESTINOFINAL";
        }
    }
}
