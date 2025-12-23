using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using LibSILC;
using SILCNegocios;
using System.Windows.Forms;

namespace formSILC
{
    public partial class frmModeloMTRe : Form
    {
        clsDestinoFinal oDestinoFinal = new clsDestinoFinal();
        clsDestinoFinalDados oDestinoFinalDados = new clsDestinoFinalDados();
        clsClientes oCliente = new clsClientes();
        clsClienteDados oClienteDados = new clsClienteDados();
        clsModeloMTRe oModelo = new clsModeloMTRe();
        clsModeloMTReDados oModeloDados = new clsModeloMTReDados();
        clsModeloMTReResiduosDados oModeloResiduosDados = new clsModeloMTReResiduosDados();

        DataTable _dt = new DataTable();
        private BindingSource bindingSource = new BindingSource();
        private BindingSource bindingSourceResiduos = new BindingSource();
        private int _LinhaSelecionadaResiduo = -1;
        private int _LinhaSelecionadaModeloMTRe = -1;
        public frmModeloMTRe()
        {
            InitializeComponent();
            grvModelos.AutoGenerateColumns = false;
            grvResiduos.AutoGenerateColumns = false;
            LimpaCampos();
            PreencheGridModelos();

            if (grvModelos.Rows.Count > 0)
            {
                PreencheGradeResiduos(grvModelos.Rows[0].Cells["Codigo"].Value.ToString());
                AtribuiDaClasseModeloMTRe(grvModelos.Rows[0].Cells["Codigo"].Value.ToString());
            }
        }

        private void frmModeloMTRe_Resize(object sender, EventArgs e)
        {
            grbItem2.Width = grbItem1.Width;
            grbItem3.Width = grbItem1.Width;
            grbItem4.Width = grbItem1.Width;
        }

        private void butPesquisaDestinoFinal_Click(object sender, EventArgs e)
        {
            frmProcura frmListaDestinoFinal = new frmProcura("DESTINOFINALMTRe");
            frmListaDestinoFinal.ShowDialog();
            if (geral.CodigoDESTINOFINAL > 0)
            {
                oDestinoFinal = new clsDestinoFinal();
                oDestinoFinalDados = new clsDestinoFinalDados();
                oDestinoFinalDados.PegaDados(oDestinoFinal, geral.CodigoDESTINOFINAL);
                txtCNPJ_Armazenador.Text = oDestinoFinal.CNPJ;
                lblNomeArmazenador.Text = oDestinoFinal.Nome;
            }
        }
        private void grvModelos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void PreencheGradeResiduos(string pCodigoModeloMTRe)
        {
            DataTable _dtResiduos = new DataTable();
            _dtResiduos = oModeloResiduosDados.PreencheDataTable("mr.Codigo", pCodigoModeloMTRe);
            bindingSourceResiduos.DataSource = _dtResiduos;
            grvResiduos.DataSource = bindingSourceResiduos;
        }
        private void grbItem1_Enter(object sender, EventArgs e)
        {

        }
        private void txtCNPJ_Armazenador_Enter(object sender, EventArgs e)
        {
            rdbSim.Checked = true;
            rdbNao.Checked = false;
        }
        private void butOk_Click(object sender, EventArgs e)
        {
            rdbSim.Checked = true;
            rdbNao.Checked = false;
            clsDestinoFinalDados oDF_dados = new clsDestinoFinalDados();
            string[] _s;
            _s = oDF_dados.PegaNomeCNPJPeloCNPJ(txtCNPJ_Armazenador.Text);
            lblNomeArmazenador.Text = _s[0];
            txtCNPJ_Armazenador.Text = _s[1];
        }
        private void rdbNao_Click(object sender, EventArgs e)
        {
            txtCNPJ_Armazenador.Text = "";
            lblNomeArmazenador.Text = "";
        }
        private void butOkTransportador_Click(object sender, EventArgs e)
        {
            clsDestinoFinalDados oDF_dados = new clsDestinoFinalDados();
            string[] _s;
            _s = oDF_dados.PegaNomeCNPJPeloCNPJ(txtCNPJ_CPF_Transportador.Text);
            lblNomeTransportador.Text = _s[0];
            txtCNPJ_CPF_Transportador.Text = _s[1];
        }

        private void butOkDestinador_Click(object sender, EventArgs e)
        {
            clsDestinoFinalDados oDF_dados = new clsDestinoFinalDados();
            string[] _s;
            if (int3NossoCodigo.VALOR.Text == "" || int3NossoCodigo.VALOR.Text == "000" || int3NossoCodigo.VALOR.Text == "0")
            {
                _s = oDF_dados.PegaNomeCNPJPeloCNPJ(txtCNPJ_CPF_Destinador.Text);
                lblNomeDestinador.Text = _s[0] + " - " + _s[3];
                txtCNPJ_CPF_Destinador.Text = _s[1];
                int3NossoCodigo.VALOR.Text = "";
                if (_s[2] != "")
                    int3NossoCodigo.VALOR.Text = Convert.ToInt32(_s[2]).ToString("000");
            }
            else if (int3NossoCodigo.VALOR.Text != "")
            {
                oDF_dados.PegaDados(oDestinoFinal, Convert.ToInt32(int3NossoCodigo.VALOR.Text));
                lblNomeDestinador.Text = oDestinoFinal.NomeFantasia + " - " + oDestinoFinal.CodigoUnidadeDoIMA.ToString();
                txtCNPJ_CPF_Destinador.Text = oDestinoFinal.CNPJ;
            }
        }

        private void butClienteArmazenador_Click(object sender, EventArgs e)
        {
            frmProcura frmListaClientes = new frmProcura("CLIENTEMTRe");
            frmListaClientes.ShowDialog();
            if (geral.CodigoCliente > 0)
            {
                oCliente = new clsClientes();
                oClienteDados = new clsClienteDados();
                oClienteDados.PegaDados(oCliente, geral.CodigoCliente);
                txtCNPJ_Armazenador.Text = oCliente.CNPJ_CPF;
                lblNomeArmazenador.Text = oCliente.Nome;
            }
        }

        private void butPesquisaClienteTransportador_Click(object sender, EventArgs e)
        {
            frmProcura frmListaClientes = new frmProcura("CLIENTEMTRe");
            frmListaClientes.ShowDialog();
            if (geral.CodigoCliente > 0)
            {
                oCliente = new clsClientes();
                oClienteDados = new clsClienteDados();
                oClienteDados.PegaDados(oCliente, geral.CodigoCliente);
                txtCNPJ_CPF_Transportador.Text = oCliente.CNPJ_CPF;
                lblNomeTransportador.Text = oCliente.Nome;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmProcura frmListaClientes = new frmProcura("CLIENTEMTRe");
            frmListaClientes.ShowDialog();
            if (geral.CodigoCliente > 0)
            {
                oCliente = new clsClientes();
                oClienteDados = new clsClienteDados();
                oClienteDados.PegaDados(oCliente, geral.CodigoCliente);
                txtCNPJ_CPF_Destinador.Text = oCliente.CNPJ_CPF;
                lblNomeDestinador.Text = oCliente.Nome;
            }
        }

        private void AtribuiDaClasseModeloMTRe(string pCodigoModelo)
        {
            string[] _s;
            if (pCodigoModelo != "")
            {
                LimpaCampos();
                oModelo = new clsModeloMTRe();
                oModeloDados = new clsModeloMTReDados();
                if (pCodigoModelo != "")
                {
                    lblCodigoModelo.Text = pCodigoModelo;
                    oModelo.Codigo = Convert.ToInt32(pCodigoModelo);
                    oModeloDados.PegaDados(oModelo, oModelo.Codigo);
                    if (oModelo.Nome != "")
                    {
                        if (oModelo.CNPJ_CPF_Armazenador != "")
                        {
                            rdbNao.Checked = false;
                            rdbSim.Checked = true;
                            txtCNPJ_Armazenador.Text = oModelo.CNPJ_CPF_Armazenador;

                            _s = oDestinoFinalDados.PegaNomeCNPJPeloCNPJ(txtCNPJ_Armazenador.Text);
                            lblNomeArmazenador.Text = _s[0];
                            if (lblNomeArmazenador.Text != "")
                                txtCNPJ_Armazenador.Text = _s[1];

                            if (lblNomeArmazenador.Text == "")
                            {
                                _s = oClienteDados.PegaNomeCNPJPeloCNPJ(txtCNPJ_Armazenador.Text);
                                lblNomeArmazenador.Text = _s[0];
                                txtCNPJ_Armazenador.Text = _s[1];
                            }
                        }

                        //inicio destinador
                        clsDestinoFinal oDestino = new clsDestinoFinal();
                        clsDestinoFinalDados oDestinoDados = new clsDestinoFinalDados();
                        if (oModelo.CodigoDestinoFinal > 0)
                        {
                            oDestino = oDestinoDados.PegaDados(oDestino, oModelo.CodigoDestinoFinal);
                            txtCNPJ_CPF_Destinador.Text = oModelo.CNPJ_CPF_Destinador;
                            lblNomeDestinador.Text = oDestino.NomeFantasia + "-" + oDestino.CodigoUnidadeDoIMA;
                            txtCNPJ_CPF_Destinador.Text = oDestino.CNPJ;
                            int3NossoCodigo.VALOR.Text = oModelo.CodigoDestinoFinal.ToString("000");
                        }
                        else
                        {
                            txtCNPJ_CPF_Destinador.Text = oModelo.CNPJ_CPF_Destinador;
                            _s = oDestinoFinalDados.PegaNomeCNPJPeloCNPJ(txtCNPJ_CPF_Destinador.Text);
                            lblNomeDestinador.Text = _s[0] + "-" + _s[3];
                            if (oModelo.CodigoDestinoFinal == 0)
                                int3NossoCodigo.VALOR.Text = _s[2];
                            else
                                int3NossoCodigo.VALOR.Text = oModelo.CodigoDestinoFinal.ToString("000");
                        }
                        // fim destinador

                        txtCNPJ_CPF_Gerador.Text = oModelo.CNPJ_CPF_Gerador;
                        _s = oClienteDados.PegaNomeCNPJPeloCNPJ(txtCNPJ_CPF_Gerador.Text, true);
                        lblNomeGerador.Text = _s[0];
                        txtCNPJ_CPF_Gerador.Text = _s[1];

                        txtCNPJ_CPF_Transportador.Text = oModelo.CNPJ_CPF_Transportador;
                        _s = oDestinoFinalDados.PegaNomeCNPJPeloCNPJ(txtCNPJ_CPF_Transportador.Text);
                        lblNomeTransportador.Text = _s[0];
                        if (lblNomeTransportador.Text != "")
                            txtCNPJ_CPF_Transportador.Text = _s[1];
                        if (lblNomeTransportador.Text == "")
                        {
                            _s = oClienteDados.PegaNomeCNPJPeloCNPJ(txtCNPJ_CPF_Transportador.Text);
                            lblNomeTransportador.Text = _s[0];
                            txtCNPJ_CPF_Transportador.Text = _s[1];
                        }

                        txtNomeModelo.Text = oModelo.Nome;
                       
                    }
                }
            }
        }
        private void butPesquisaDestinoTransportador_Click(object sender, EventArgs e)
        {
            frmProcura frmListaDestinoFinal = new frmProcura("DESTINOFINALMTRe");
            frmListaDestinoFinal.ShowDialog();
            if (geral.CodigoDESTINOFINAL > 0)
            {
                oDestinoFinal = new clsDestinoFinal();
                oDestinoFinalDados = new clsDestinoFinalDados();
                oDestinoFinalDados.PegaDados(oDestinoFinal, geral.CodigoDESTINOFINAL);
                txtCNPJ_CPF_Transportador.Text = oDestinoFinal.CNPJ;
                lblNomeTransportador.Text = oDestinoFinal.Nome;
            }
        }

        private void butPesquisaCNPJ_CPF_Destinador_Click(object sender, EventArgs e)
        {
            frmProcura frmListaDestinoFinal = new frmProcura("DESTINOFINALMTRe");
            frmListaDestinoFinal.ShowDialog();
            if (geral.CodigoDESTINOFINAL > 0)
            {
                oDestinoFinal = new clsDestinoFinal();
                oDestinoFinalDados = new clsDestinoFinalDados();
                oDestinoFinalDados.PegaDados(oDestinoFinal, geral.CodigoDESTINOFINAL);
                txtCNPJ_CPF_Destinador.Text = oDestinoFinal.CNPJ;
                lblNomeDestinador.Text = oDestinoFinal.NomeFantasia + " - " + oDestinoFinal.CodigoUnidadeDoIMA;
                int3NossoCodigo.VALOR.Text = oDestinoFinal.Codigo.ToString("000");
            }
        }
        private void butInserirResiduo_Click(object sender, EventArgs e)
        {
            if (lblCodigoModelo.Text == "")
            {
                MessageBox.Show("Nenhum modelo selecionado para inserir resíduo!");
            }
            else if (grvResiduos.Rows.Count > 1 && rdbSim.Checked)
                MessageBox.Show("Armazenamento temporário pode ter apenas 1 (um) resíduo!");
            else
            {
                // chamar form de inserção de resíduos
                frmModeloMTReResiduosInserir ofrmInsereResiduoModeloMTRe = new frmModeloMTReResiduosInserir();
                ofrmInsereResiduoModeloMTRe.pCodigoModeloMTReResiduo = Convert.ToInt32(lblCodigoModelo.Text);
                ofrmInsereResiduoModeloMTRe.ShowDialog();
                if (lblCodigoModelo.Text != "")
                    PreencheGradeResiduos(lblCodigoModelo.Text);
            }
        }
        private void butOkGerador_Click(object sender, EventArgs e)
        {
            oClienteDados = new clsClienteDados();
            string[] _s;
            if (txtCNPJ_CPF_Gerador.Text.Length >= 18)
                _s = oClienteDados.PegaNomeCNPJPeloCNPJ(txtCNPJ_CPF_Gerador.Text, true);
            else
                _s = oClienteDados.PegaNomeCNPJPeloCNPJ(txtCNPJ_CPF_Gerador.Text);
            lblNomeGerador.Text = _s[0];
            txtCNPJ_CPF_Gerador.Text = _s[1];
        }
        private void butPesquisaClienteGerador_Click(object sender, EventArgs e)
        {
            frmProcura frmListaClientes = new frmProcura("CLIENTEMTRe");
            frmListaClientes.ShowDialog();
            if (geral.CodigoCliente > 0)
            {
                oCliente = new clsClientes();
                oClienteDados = new clsClienteDados();
                oClienteDados.PegaDados(oCliente, geral.CodigoCliente);
                txtCNPJ_CPF_Gerador.Text = oCliente.CNPJ_CPF;
                lblNomeGerador.Text = oCliente.Nome;
            }
        }
        private void butSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                butSalvar.Enabled = false;
                butSalvar.Refresh();
            }
            finally
            {
                // salvar modelo 
                oModelo = new clsModeloMTRe();
                oModeloDados = new clsModeloMTReDados();
                oModelo.Codigo = 0;
                if (lblCodigoModelo.Text != "")
                    oModelo.Codigo = Convert.ToInt32(lblCodigoModelo.Text);
                oModelo.CNPJ_CPF_Armazenador = txtCNPJ_Armazenador.Text;
                oModelo.CNPJ_CPF_Destinador = txtCNPJ_CPF_Destinador.Text;
                oModelo.CNPJ_CPF_Gerador = txtCNPJ_CPF_Gerador.Text;
                oModelo.CNPJ_CPF_Transportador = txtCNPJ_CPF_Transportador.Text;
                oModelo.Nome = txtNomeModelo.Text;
                if (int3NossoCodigo.VALOR.Text != "")
                    oModelo.CodigoDestinoFinal = Convert.ToInt32(int3NossoCodigo.VALOR.Text);
                if (rdbSim.Checked && txtCNPJ_Armazenador.Text == "")
                    MessageBox.Show("CNPJ do Armazenador inválido!");
                else if (txtCNPJ_CPF_Destinador.Text == "")
                    MessageBox.Show("CNPJ do Destinador inválido!");
                else if (txtCNPJ_CPF_Transportador.Text == "")
                    MessageBox.Show("CNPJ do Transportador inválido!");
                else if (txtNomeModelo.Text == "")
                    MessageBox.Show("Nome do Modelo inválido!");
                else if (lblCodigoModelo.Text == "")
                {
                    // incluir
                    if (oModelo.Codigo == 0)
                    {
                        try
                        {
                            oModeloDados.Inserir(oModelo);
                        }
                        finally
                        {
                            int _UltimoCodigo = oModeloDados.PegaUltimoCodigo();
                            lblCodigoModelo.Text = "";
                            if (_UltimoCodigo > 0)
                            {
                                lblCodigoModelo.Text = _UltimoCodigo.ToString();
                                butInserirResiduo.Enabled = true;
                                PreencheGridModelos();
                            }
                        }
                    }
                }
                else if (lblCodigoModelo.Text != "")
                {
                    // alterar
                    if (oModelo.Codigo > 0)
                    {
                        oModeloDados.Alterar(oModelo, oModelo.Codigo);
                        PreencheGridModelos();
                    }
                }
                butSalvar.Enabled = true;
            }
        }
        private void PreencheGridModelos()
        {
            _dt = oModeloDados.PreencheDataTable("Nome");
            bindingSource.DataSource = _dt;
            grvModelos.DataSource = bindingSource;
        }
        private void butNovo_Click(object sender, EventArgs e)
        {
            DataTable _dtResiduos = new DataTable();
            _dtResiduos = oModeloResiduosDados.PreencheDataTable("mr.Codigo", "00");
            bindingSourceResiduos.DataSource = _dtResiduos;
            grvResiduos.DataSource = bindingSourceResiduos;
            LimpaCampos();
        }
        private void LimpaCampos()
        {
            rdbNao.Checked = true;
            rdbSim.Checked = false;
            lblCodigoModelo.Text = "";
            txtNomeModelo.Text = "";
            butInserirResiduo.Enabled = false;
            txtCNPJ_Armazenador.Text = "";
            lblNomeArmazenador.Text = "";
            txtCNPJ_CPF_Destinador.Text = "";
            lblNomeDestinador.Text = "";
            txtCNPJ_CPF_Gerador.Text = "";
            lblNomeGerador.Text = "";
            txtCNPJ_CPF_Transportador.Text = "";
            lblNomeTransportador.Text = "";
            int3NossoCodigo.VALOR.Text = "";
        }
        private void frmModeloMTRe_Click(object sender, EventArgs e)
        {
            butInserirResiduo.Enabled = false;
            if (txtCNPJ_CPF_Destinador.Text != "" && txtCNPJ_CPF_Transportador.Text != "")
                butInserirResiduo.Enabled = true;
        }

        private void grvResiduos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (_LinhaSelecionadaResiduo >= 0)
                {
                    if (grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[0].Selected ||
                        grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[1].Selected ||
                        grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[2].Selected ||
                        grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[3].Selected ||
                        grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[4].Selected ||
                        grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[5].Selected ||
                        grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[6].Selected ||
                        grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[7].Selected ||
                        grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[8].Selected ||
                        grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[9].Selected)
                    {
                        if (grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[0].Value.ToString() != "")
                        {
                            string _msg = "Excluir \n";
                            _msg = _msg + "Código: " + grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[0].Value.ToString() + " \n";
                            _msg = _msg + "Código IBAMA: " + grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[1].Value.ToString() + " \n";
                            _msg = _msg + "Descrição: " + grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[2].Value.ToString() + " \n";
                            _msg = _msg + "" + grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[3].Value.ToString() + " \n";
                            _msg = _msg + "" + grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[4].Value.ToString() + " \n";
                            _msg = _msg + "" + grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[5].Value.ToString() + " \n";

                            DialogResult _result = new DialogResult();
                            _result = MessageBox.Show(_msg, "Confirma exclusão?", MessageBoxButtons.YesNo);
                            if (_result == DialogResult.Yes)
                            {
                                oModeloResiduosDados.Excluir(Convert.ToInt32(lblCodigoModelo.Text),
                                                             Convert.ToInt32(grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[0].Value.ToString()));
                                PreencheGradeResiduos(lblCodigoModelo.Text);
                                MessageBox.Show("Excluído!");

                            }
                        }
                    }
                }
            }
        }

        private void grvResiduos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void grvModelos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (_LinhaSelecionadaModeloMTRe >= 0)
                {
                    if (grvModelos.Rows[_LinhaSelecionadaModeloMTRe].Cells[0].Selected || grvModelos.Rows[_LinhaSelecionadaModeloMTRe].Cells[1].Selected ||
                        grvModelos.Rows[_LinhaSelecionadaModeloMTRe].Cells[2].Selected || grvModelos.Rows[_LinhaSelecionadaModeloMTRe].Cells[3].Selected)
                    {
                        if (grvModelos.Rows[_LinhaSelecionadaModeloMTRe].Cells[0].Value.ToString() != "")
                        {
                            string _msg = "Excluir \n";
                            _msg = _msg + "Código: " + grvModelos.Rows[_LinhaSelecionadaModeloMTRe].Cells[0].Value.ToString() + " \n";
                            _msg = _msg + "Descrição: " + grvModelos.Rows[_LinhaSelecionadaModeloMTRe].Cells[1].Value.ToString() + " \n";
                            _msg = _msg + "" + grvModelos.Rows[_LinhaSelecionadaModeloMTRe].Cells[2].Value.ToString() + " \n";

                            DialogResult _result = new DialogResult();
                            _result = MessageBox.Show(_msg, "Confirma exclusão?", MessageBoxButtons.YesNo);
                            if (_result == DialogResult.Yes)
                            {
                                oModeloDados.Excluir(Convert.ToInt32(lblCodigoModelo.Text));
                                foreach (DataGridViewRow _dgv in grvResiduos.Rows)
                                {
                                    if (_dgv.Cells["CodigoResiduo"].Value != null)
                                    {
                                        oModeloResiduosDados.Excluir(Convert.ToInt32(grvModelos.Rows[_LinhaSelecionadaModeloMTRe].Cells[0].Value.ToString()),
                                                                     Convert.ToInt32(_dgv.Cells["CodigoResiduo"].Value.ToString()));
                                    }
                                }
                                LimpaCampos();
                                PreencheGridModelos();
                                PreencheGradeResiduos("0");
                                MessageBox.Show("Excluído!");
                            }
                        }
                    }
                }
            }

        }

        private void grvModelos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _LinhaSelecionadaModeloMTRe = e.RowIndex;
            if (_LinhaSelecionadaModeloMTRe > -1)
            {
                if (grvModelos.Rows[e.RowIndex].Cells["Codigo"].Value.ToString() != "")
                {
                    AtribuiDaClasseModeloMTRe(grvModelos.Rows[e.RowIndex].Cells["Codigo"].Value.ToString());
                    butInserirResiduo.Enabled = true;
                    PreencheGradeResiduos(grvModelos.Rows[e.RowIndex].Cells["Codigo"].Value.ToString());
                }
            }

        }

        private void grvResiduos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _LinhaSelecionadaResiduo = e.RowIndex;
        }

        private void butOkResiduos_Click(object sender, EventArgs e)
        {
            clsModeloMTReResiduos oModeloMTReResiduos = new clsModeloMTReResiduos();
            clsTipoAcondicionamento oAcondicionamento = new clsTipoAcondicionamento();
            clsTipoAcondicionamentoDados oAcondDados = new clsTipoAcondicionamentoDados();

            if (_LinhaSelecionadaResiduo >= 0)
            {
                if (grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[9].Value == null  ||
                    grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[12].Value == null ||
                    grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[13].Value == null ||
                    grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[14].Value == null ||
                    grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[0].Value == null  ||
                    grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[15].Value == null ||
                    grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[11].Value == null ||
                    grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[10].Value == null ||
                    grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[8].Value == null  ||
                    grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[16].Value == null)
                {
                    MessageBox.Show("Um coluna ou mais com digitação inválida!");
                    return;
                }
                if (grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[9].Value.ToString() != "")
                {
                    oModeloMTReResiduos.ClasseRisco = grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[9].Value.ToString();  // txtClasseRisco.Text;
                }
                if (grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[12].Value.ToString() != "")
                {
                    oModeloMTReResiduos.CodigoAcondicionamento = Convert.ToInt32(grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[12].Value.ToString()); // cboAcondicionamento.SelectedIndex;
                }
                if (grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[13].Value.ToString() != "")
                {
                    oModeloMTReResiduos.CodigoClasse = Convert.ToInt32(grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[13].Value.ToString()); // cboClasse.SelectedIndex;
                }
                if (grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[14].Value.ToString() != "")
                {
                    oModeloMTReResiduos.CodigoEstadoFisico = Convert.ToInt32(grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[14].Value.ToString()); // cboEstadoFisico.SelectedIndex;
                }
                if (grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[0].Value.ToString() != "")
                {
                    oModeloMTReResiduos.CodigoResiduo = Convert.ToInt32(grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[0].Value.ToString()); // Convert.ToInt32(residuo1.txtCodigo.Text);
                }
                if (grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[15].Value.ToString() != "")
                {
                    oModeloMTReResiduos.CodigoTecnologia = Convert.ToInt32(grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[15].Value.ToString()); // cboTecnologia.SelectedIndex;
                }
                if (grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[11].Value.ToString() != "")
                {
                    oModeloMTReResiduos.GrupoEmbalagem = grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[11].Value.ToString(); //11-oModeloMTReResiduos.GrupoEmbalagem = txtGrupoEmbalagem.Text;
                }
                if (grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[10].Value.ToString() != "")
                {
                    oModeloMTReResiduos.NomeEmbarque = grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[10].Value.ToString(); //oModeloMTReResiduos.NomeEmbarque = txtNomEmbarque.Text;
                }
                if (grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[8].Value.ToString() != "")
                {
                    oModeloMTReResiduos.NumeroONU = grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[8].Value.ToString(); //oModeloMTReResiduos.NumeroONU = txtNumeroONU.Text;
                }
                if (lblCodigoModelo.Text != "")
                {
                    oModeloMTReResiduos.CodigoModeloMTRe = Convert.ToInt32(lblCodigoModelo.Text);
                }
                if (grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[16].Value.ToString() != "")
                {
                    oModeloMTReResiduos.CodigoUnidade = Convert.ToInt32(grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[16].Value.ToString()); //oModeloMTReResiduos.CodigoUnidade = cboUnidade.SelectedIndex;
                }

                if (oModeloMTReResiduos.CodigoResiduo == 0)
                {
                    MessageBox.Show("Código Resíduo Inválido!");
                }
                else if (grvResiduos.Rows[_LinhaSelecionadaResiduo].Cells[1].Value.ToString() == "")
                {
                    MessageBox.Show("Resíduo Código IBAMA Inválido! Selecione.");
                }
                else if (oModeloMTReResiduos.CodigoUnidade == 0)
                {
                    MessageBox.Show("Unidade Inválida! Selecione.");
                }
                else if (oModeloMTReResiduos.CodigoEstadoFisico == 0)
                {
                    MessageBox.Show("Estado Físico Inválido! Selecione.");
                }
                else if (oModeloMTReResiduos.CodigoClasse == 0)
                {
                    MessageBox.Show("Classe inválida! Selecione.");
                }
                else if (oModeloMTReResiduos.CodigoAcondicionamento == 0)
                {
                    MessageBox.Show("Tipo Acondicionamento inválido! Selecione.");
                }
                else if (oModeloMTReResiduos.CodigoTecnologia == 0)
                {
                    MessageBox.Show("Tecnologia Inválida! Selecione.");
                }
                else
                {
                    clsModeloMTReResiduosDados oModeloMTReResiduosDados = new clsModeloMTReResiduosDados();
                    if (oModeloMTReResiduosDados.DadoExiste(oModeloMTReResiduos.CodigoModeloMTRe, oModeloMTReResiduos.CodigoResiduo) == "Incluir")
                    {
                        oModeloMTReResiduosDados.Inserir(oModeloMTReResiduos);
                    }
                    else if (oModeloMTReResiduosDados.DadoExiste(oModeloMTReResiduos.CodigoModeloMTRe, oModeloMTReResiduos.CodigoResiduo) == "Alterar")
                    {
                        oModeloMTReResiduosDados.Alterar(oModeloMTReResiduos, oModeloMTReResiduos.CodigoModeloMTRe, oModeloMTReResiduos.CodigoResiduo.ToString());
                    }
                    if (grvModelos.Rows.Count > 0)
                    {
                        PreencheGradeResiduos(oModeloMTReResiduos.CodigoModeloMTRe.ToString());
                        AtribuiDaClasseModeloMTRe(oModeloMTReResiduos.CodigoModeloMTRe.ToString());
                    }
                }
            }
        }
    }
}