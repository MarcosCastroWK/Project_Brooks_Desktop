using System;
using System.Data;
using System.Windows.Forms;
using LibSILC;
using SILCNegocios;

namespace formSILC
{
    public partial class frmProcura : Form
    {        
        clsClienteDados oClientesDados = new clsClienteDados();
        clsResiduoDados oResiduoDados = new clsResiduoDados();
        clsResiduos oResiduo = new clsResiduos();
        clsFuncionarios oFuncionario = new clsFuncionarios();
        clsFuncionarioDados oFuncionarioDados = new clsFuncionarioDados();
        clsCaminhoes oCaminhao = new clsCaminhoes();
        clsCaminhoesDados oCaminhoesDados = new clsCaminhoesDados();
        clsDestinoFinal oDestinoFinal = new clsDestinoFinal();
        clsDestinoFinalDados oDestinoFinalDados = new clsDestinoFinalDados();

        private BindingSource bindingSource = new BindingSource();
        DataTable _dt = new DataTable();
        private string _origem;

        public frmProcura(string pOrigem)
        {
            _origem = pOrigem;
            InitializeComponent();
        }

        private void frmProcura_Load(object sender, EventArgs e)
        {
            cboNomeDescricao.Items.Clear();
            if (_origem == "CLIENTE" || _origem == "CLIENTEPROGRAMACAO" || 
                _origem == "frmLocacoesCLIENTE" || _origem == "frmNotasFiscaisCLIENTE" ||
                _origem == "frmAtualizacaoDadosCLIENTE" || _origem == "frmCDFeImportacao" || _origem == "CLIENTEMTRe")
            {

                cboNomeDescricao.Items.Add("Código");
                cboNomeDescricao.Items.Add("Nome fantasia");
                cboNomeDescricao.SelectedIndex = 0;
                _dt = oClientesDados.PreencheDataTable("NomeFantasia", true);
                bindingSource.DataSource = _dt;
                Grade.DataSource = bindingSource.DataSource;
                
                Grade.Columns["Codigo"].Width = 60;
                Grade.Columns["Codigo"].HeaderText = "Código";

                Grade.Columns["NomeFantasia"].Width = 400;
                Grade.Columns["NomeFantasia"].HeaderText = "Nome fantasia";

                Grade.Columns["Nome"].Width = 500;
                Grade.Columns["Nome"].HeaderText = "Nome/Razão social";

                Grade.Columns["DataCadastro"].Visible = false;
                Grade.Columns["Inativo"].Visible = false;

            }
            else if (_origem == "RESIDUO" || _origem == "frmLocacaoMTRRESIDUO" || _origem == "frmDestinoFinal" || _origem == "RESIDUOPROGRAMACAO")
            {
                cboNomeDescricao.Items.Add("Descrição");
                cboNomeDescricao.SelectedIndex = 0;
                cboNomeDescricao.Enabled = false;
                _dt = oResiduoDados.PegaListaComResiduosContratados("", 0, geral.CodigoCliente);
                bindingSource.DataSource = _dt;
                Grade.DataSource = bindingSource.DataSource;
                Grade.Columns["Descricao"].Width = 400;
                Grade.Columns["Descricao"].HeaderText = "Descrição";
                Grade.Columns["Codigo"].HeaderText = "Código";
                Grade.Columns["Grupo"].Width = 300;
                Grade.Columns["Contratado"].Visible = false;
                Grade.Columns["Unidade"].Width = 50;
                Grade.Columns["Classe"].Width = 50;
                Grade.Columns["CodigoIBama"].Width = 72;
                Grade.Columns["CodigoIbama"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Grade.Columns["DestinoFinal"].Width = 280;
                foreach (DataGridViewRow gvr in Grade.Rows)
                {
                    if (gvr.Cells["Contratado"].Value != null)
                    {
                        if (gvr.Cells["Contratado"].Value.ToString() == "1")
                        {
                            foreach (DataGridViewColumn gvc in Grade.Columns)
                                gvr.Cells[gvc.Name].Style.BackColor = System.Drawing.Color.GreenYellow;
                        }
                    }
                }
                Grade.Refresh();
            }
            else if (_origem == "FUNCIONARIO" || _origem == "MOTORISTAPROGRAMACAO" || _origem == "frmLocacoesMOTORISTA" || _origem== "frmDistribuicaoDescargaPendenteFUNCIONARIO")
            {
                cboNomeDescricao.Items.Add("Nome");
                cboNomeDescricao.SelectedIndex = 0;
                cboNomeDescricao.Enabled = false;
                _dt = oFuncionarioDados.PegaDadosLista(0, "");
                bindingSource.DataSource = _dt;
                Grade.DataSource = bindingSource.DataSource;
                Grade.Columns["Nome"].Width = 400;
                Grade.Columns["Codigo"].HeaderText = "Código";
            }
            else if (_origem == "CAMINHAO" || _origem == "CAMINHAOPROGRAMACAO" || _origem == "frmLocacoesCAMINHAO" || _origem== "frmDistribuicaoDescargaPendenteCAMINHAO")
            {
                cboNomeDescricao.Items.Add("Caminhão");
                cboNomeDescricao.SelectedIndex = 0;
                cboNomeDescricao.Enabled = false;
                _dt = oCaminhoesDados.PegaDadosLista(0, "");
                bindingSource.DataSource = _dt;
                Grade.DataSource = bindingSource.DataSource;
                Grade.Columns["Codigo"].HeaderText = "Código";
            }
            else if (_origem == "DESTINOFINAL" || _origem == "frmDistribuicaoDescargaPendenteDESTINOFINAL" || _origem == "DESTINOFINALMTRe" ||
                     _origem == "DESTINOFINALMTRe")
            {
                cboNomeDescricao.Items.Add("Destino Final");
                cboNomeDescricao.SelectedIndex = 0;
                cboNomeDescricao.Enabled = false;
                _dt = oDestinoFinalDados.PegaDadosLista(0, "");
                bindingSource.DataSource = _dt;
                Grade.DataSource = bindingSource.DataSource;
                Grade.Columns["Codigo"].HeaderText = "Código";
                Grade.Columns["Codigo"].Width = 60;
                Grade.Columns["Codigo"].HeaderText = "Código";

                Grade.Columns["Nome"].Width = 500;
                Grade.Columns["Nome"].HeaderText = "Destino final";
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (_origem == "CLIENTE" || _origem == "CLIENTEPROGRAMACAO" || _origem == "frmLocacoesCLIENTE" ||
                _origem == "frmNotasFiscaisCLIENTE" || _origem == "frmAtualizacaoDadosCLIENTE" || _origem == "frmCDFeImportacao" || 
                _origem == "CLIENTEMTRe")
            {
                if (cboNomeDescricao.SelectedIndex == 0)
                {
                    if (geral.IsNumeric(txtNomeDescricao.Text))
                        _dt = oClientesDados.PreencheDTNomeFantasia(Convert.ToInt32(geral.Left(txtNomeDescricao.Text, 6)));
                    else
                        _dt = oClientesDados.PreencheDTNomeFantasia(0); // quando procurar por nome, com combo Código selecionado. 
                }
                else
                {
                    _dt = oClientesDados.PreencheDTNomeFantasia(txtNomeDescricao.Text);
                }
                bindingSource.DataSource = _dt;
                Grade.DataSource = bindingSource.DataSource;
            }
            else if (_origem == "RESIDUO" || _origem == "frmNotasFiscaisCLIENTE" || _origem == "frmLocacaoMTRRESIDUO" || 
                     _origem == "frmDestinoFinal" || _origem == "RESIDUOPROGRAMACAO")
            {
                _dt = oResiduoDados.PegaDadosLista(txtNomeDescricao.Text, 0);
                bindingSource.DataSource = _dt;
                Grade.DataSource = bindingSource.DataSource;
            }
            else if (_origem == "FUNCIONARIO" || _origem == "MOTORISTAPROGRAMACAO" || _origem == "frmLocacoesMOTORISTA" || _origem == "frmDistribuicaoDescargaPendenteFUNCIONARIO")
            {
                _dt = oFuncionarioDados.PegaDadosLista(0, txtNomeDescricao.Text);
                bindingSource.DataSource = _dt;
                Grade.DataSource = bindingSource.DataSource;
            }
            else if (_origem == "CAMINHAO" || _origem == "CAMINHAOPROGRAMACAO" || _origem == "frmLocacoesCAMINHAO" || _origem == "frmDistribuicaoDescargaPendenteCAMINHAO")
            {
                _dt = oCaminhoesDados.PegaDadosLista(0, txtNomeDescricao.Text);
                bindingSource.DataSource = _dt;
                Grade.DataSource = bindingSource.DataSource;
            }
            else if (_origem == "DESTINOFINAL" || _origem == "frmDistribuicaoDescargaPendenteDESTINOFINAL" || _origem== "DESTINOFINALMTRe")
            {
                _dt = oDestinoFinalDados.PegaDadosLista(0, txtNomeDescricao.Text);
                bindingSource.DataSource = _dt;
                Grade.DataSource = bindingSource.DataSource;
            }
        }

        private void Grade_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (_origem == "CLIENTE" || _origem == "CLIENTEMTRe")
                {
                    geral.CodigoCliente = 0;
                    geral.NomeClienteFantasia = "";
                    if (((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value.ToString() != "")
                    {
                        geral.CodigoCliente = Convert.ToInt32(((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value);
                        geral.NomeClienteFantasia = ((DataGridView)sender).Rows[e.RowIndex].Cells["NomeFantasia"].Value.ToString();
                    }
                }
                else if (_origem == "frmAtualizacaoDadosCLIENTE")
                {
                    geral.CodigoCliente = 0;
                    geral.NomeClienteFantasia = "";
                    if (((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value.ToString() != "")
                    {
                        geral.CodigoCliente = Convert.ToInt32(((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value);
                        geral.NomeClienteFantasia = ((DataGridView)sender).Rows[e.RowIndex].Cells["NomeFantasia"].Value.ToString();
                    }
                }
                else if (_origem == "RESIDUO")
                {
                    geral.CodigoResiduo = 0;
                    if (((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value.ToString() != "")
                    {
                        geral.CodigoResiduo = Convert.ToInt32(((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value);
                    }
                }
                else if (_origem == "FUNCIONARIO")
                {
                    geral.CodigoMotorista = 0;
                    if (((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value.ToString() != "")
                    {
                        geral.CodigoMotorista = Convert.ToInt32(((DataGridView)sender).Rows[e.RowIndex].Cells["Codigo"].Value);
                    }
                }
                else if (_origem == "CAMINHAO")
                {
                    geral.CodigoCaminhao = 0;
                    if (((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value.ToString() != "")
                    {
                        geral.CodigoCaminhao = Convert.ToInt32(((DataGridView)sender).Rows[e.RowIndex].Cells["Codigo"].Value);
                    }
                }
                else if (_origem == "CLIENTEPROGRAMACAO")
                {
                    geral.CodigoCliente = 0;
                    geral.NomeClienteFantasia = "";
                    if (((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value.ToString() != "")
                    {
                        geral.CodigoCliente = Convert.ToInt32(((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value);
                        geral.NomeClienteFantasia = ((DataGridView)sender).Rows[e.RowIndex].Cells["NomeFantasia"].Value.ToString();
                    }
                }
                else if (_origem == "RESIDUOPROGRAMACAO")
                {
                    geral.CodigoResiduo = 0;
                    if (((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value.ToString() != "")
                    {
                        geral.CodigoResiduo = Convert.ToInt32(((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value);
                    }
                }
                else if (_origem == "CAMINHAOPROGRAMACAO" || _origem == "frmLocacoesCAMINHAO" || _origem == "frmDistribuicaoDescargaPendenteCAMINHAO")
                {
                    geral.CodigoCaminhao = 0;
                    if (((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value.ToString() != "")
                    {
                        geral.CodigoCaminhao = Convert.ToInt32(((DataGridView)sender).Rows[e.RowIndex].Cells["Codigo"].Value);
                    }
                }
                else if (_origem == "DESTINOFINAL" || _origem == "frmDistribuicaoDescargaPendenteDESTINOFINAL" || 
                         _origem == "DESTINOFINALMTRe")
                {
                    geral.CodigoDESTINOFINAL = 0;
                    if (((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value.ToString() != "")
                    {
                        geral.CodigoDESTINOFINAL= Convert.ToInt32(((DataGridView)sender).Rows[e.RowIndex].Cells["Codigo"].Value);
                    }
                }
                else if (_origem == "MOTORISTAPROGRAMACAO" || _origem == "frmLocacoesMOTORISTA" || _origem == "frmDistribuicaoDescargaPendenteFUNCIONARIO")
                {
                    geral.CodigoMotorista = 0;
                    if (((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value.ToString() != "")
                    {
                        geral.CodigoMotorista = Convert.ToInt32(((DataGridView)sender).Rows[e.RowIndex].Cells["Codigo"].Value);
                    }
                }
                else if (_origem == "CLIENTE" || _origem == "frmLocacoesCLIENTE" || _origem == "frmNotasFiscaisCLIENTE" || _origem == "frmCDFeImportacao")
                {
                    geral.CodigoCliente = 0;
                    geral.NomeClienteFantasia = "";
                    if (((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value.ToString() != "")
                    {
                        geral.CodigoCliente = Convert.ToInt32(((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value);
                        geral.NomeClienteFantasia = ((DataGridView)sender).Rows[e.RowIndex].Cells["NomeFantasia"].Value.ToString();
                    }
                }
                else if (_origem == "RESIDUO" || _origem == "frmLocacoesRESIDUO" || _origem == "frmLocacaoMTRRESIDUO" || 
                         _origem == "frmDestinoFinal" || _origem == "RESIDUOPROGRAMACAO")
                {
                    geral.CodigoResiduo = 0;
                    if (((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value.ToString() != "")
                    {
                        geral.CodigoResiduo = Convert.ToInt32(((DataGridView)sender).Rows[e.RowIndex].Cells[0].Value);
                    }
                }
            }
            this.Close();
        }

        private void Grade_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {           
                int _rowindex = 0;
                foreach (DataGridViewRow _dgr in ((DataGridView)sender).Rows)
                {
                    if (_dgr.Cells[1].Selected)
                    {
                        _rowindex = _dgr.Index-1;
                        break;
                    }
                }

                if (_rowindex >= 0)
                {
                    if (_origem == "CLIENTE" || _origem == "CLIENTEMTRe")
                    {
                        geral.CodigoCliente = 0;
                        geral.NomeClienteFantasia = "";
                        if (((DataGridView)sender).Rows[_rowindex].Cells[0].Value.ToString() != "")
                        {
                            geral.CodigoCliente = Convert.ToInt32(((DataGridView)sender).Rows[_rowindex].Cells[0].Value);
                            geral.NomeClienteFantasia = ((DataGridView)sender).Rows[_rowindex].Cells["NomeFantasia"].Value.ToString();
                        }
                    }
                    else if (_origem == "frmAtualizacaoDadosCLIENTE")
                    {
                        geral.CodigoCliente = 0;
                        geral.NomeClienteFantasia = "";
                        if (((DataGridView)sender).Rows[_rowindex].Cells[0].Value.ToString() != "")
                        {
                            geral.CodigoCliente = Convert.ToInt32(((DataGridView)sender).Rows[_rowindex].Cells[0].Value);
                            geral.NomeClienteFantasia = ((DataGridView)sender).Rows[_rowindex].Cells["NomeFantasia"].Value.ToString();
                        }
                    }
                    else if (_origem == "RESIDUO")
                    {
                        geral.CodigoResiduo = 0;
                        if (((DataGridView)sender).Rows[_rowindex].Cells[0].Value.ToString() != "")
                        {
                            geral.CodigoResiduo = Convert.ToInt32(((DataGridView)sender).Rows[_rowindex].Cells[0].Value);
                        }
                    }
                    else if (_origem == "FUNCIONARIO")
                    {
                        geral.CodigoMotorista = 0;
                        if (((DataGridView)sender).Rows[_rowindex].Cells[0].Value.ToString() != "")
                        {
                            geral.CodigoMotorista = Convert.ToInt32(((DataGridView)sender).Rows[_rowindex].Cells["Codigo"].Value);
                        }
                    }
                    else if (_origem == "CAMINHAO")
                    {
                        geral.CodigoCaminhao = 0;
                        if (((DataGridView)sender).Rows[_rowindex].Cells[0].Value.ToString() != "")
                        {
                            geral.CodigoCaminhao = Convert.ToInt32(((DataGridView)sender).Rows[_rowindex].Cells["Codigo"].Value);
                        }
                    }
                    else if (_origem == "CLIENTEPROGRAMACAO")
                    {
                        geral.CodigoCliente = 0;
                        geral.NomeClienteFantasia = "";
                        if (((DataGridView)sender).Rows[_rowindex].Cells[0].Value.ToString() != "")
                        {
                            geral.CodigoCliente = Convert.ToInt32(((DataGridView)sender).Rows[_rowindex].Cells[0].Value);
                            geral.NomeClienteFantasia = ((DataGridView)sender).Rows[_rowindex].Cells["NomeFantasia"].Value.ToString();
                        }
                    }
                    else if (_origem == "CAMINHAOPROGRAMACAO" || _origem == "frmLocacoesCAMINHAO" || _origem == "frmDistribuicaoDescargaPendenteCAMINHAO")
                    {
                        geral.CodigoCaminhao = 0;
                        if (((DataGridView)sender).Rows[_rowindex].Cells[0].Value.ToString() != "")
                        {
                            geral.CodigoCaminhao = Convert.ToInt32(((DataGridView)sender).Rows[_rowindex].Cells["Codigo"].Value);
                        }
                    }
                    else if (_origem == "MOTORISTAPROGRAMACAO" || _origem == "frmLocacoesMOTORISTA" || _origem == "frmDistribuicaoDescargaPendenteFUNCIONARIO")
                    {
                        geral.CodigoMotorista = 0;
                        if (((DataGridView)sender).Rows[_rowindex].Cells[0].Value.ToString() != "")
                        {
                            geral.CodigoMotorista = Convert.ToInt32(((DataGridView)sender).Rows[_rowindex].Cells["Codigo"].Value);
                        }
                    }
                    else if (_origem == "frmDistribuicaoDescargaPendenteDESTINOFINAL" || _origem == "DESTINOFINALMTRe")
                    {
                        geral.CodigoDESTINOFINAL = 0;
                        if (((DataGridView)sender).Rows[_rowindex].Cells[0].Value.ToString() != "")
                        {
                            geral.CodigoDESTINOFINAL = Convert.ToInt32(((DataGridView)sender).Rows[_rowindex].Cells["Codigo"].Value);
                        }
                    }
                }
                this.Close();
            }
        }

        private void Grade_Sorted(object sender, EventArgs e)
        {
            if (_origem == "frmLocacaoMTRRESIDUO" || _origem == "frmDestinoFinal" || _origem == "RESIDUOPROGRAMACAO")
            {
                foreach (DataGridViewRow gvr in Grade.Rows)
                {
                    if (gvr.Cells["Contratado"].Value != null)
                    {
                        if (gvr.Cells["Contratado"].Value.ToString() == "1")
                        {
                            foreach (DataGridViewColumn gvc in Grade.Columns)
                                gvr.Cells[gvc.Name].Style.BackColor = System.Drawing.Color.GreenYellow;
                        }
                    }
                }
                Grade.Refresh();
            }
        }

        private void cboNomeDescricao_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNomeDescricao.Text = "";
        }
    }
}
