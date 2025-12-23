using System;
using System.Data;
using System.Windows.Forms;
using LibSILC;
using SILCNegocios;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace formSILC
{
    public partial class frmIMA : Form
    {
        private BindingSource bindingSource = new BindingSource();
        clsProgramacaoFechadaDados oProgramacaoFechadaDados = new clsProgramacaoFechadaDados();
        clsProgramacaoFechada oProgramacaoFechada = new clsProgramacaoFechada();
        clsProgramacaoDados oProgramacaoDados = new clsProgramacaoDados();
        clsCaminhoes oCaminhao = new clsCaminhoes();
        clsCaminhoesDados oCaminhaoDados = new clsCaminhoesDados();
        clsFuncionarios oMotorista = new clsFuncionarios();
        clsFuncionarioDados oMotoristaDados = new clsFuncionarioDados();
        clsServicosFutura oServicosFutura = new clsServicosFutura();
        clsServicosFuturaDados oServicosFuturaDados = new clsServicosFuturaDados();
        clsDestinoFinalDados oDestinoFinalDados = new clsDestinoFinalDados();
        clsLancamentosDados oLancDados = new clsLancamentosDados();
        clsClientes oClientes = new clsClientes();
        clsClienteDados oClienteDados = new clsClienteDados();

        frmLocacoes ofrmLocacao = new frmLocacoes();

        private DataTable _dt = new DataTable();
        private DataTable _dtParaImprimirExecutados = new DataTable();

        private string _ano = "";
        private string _mes = "";
        private string _dia = "";
        private string _bloqPor;
        public frmIMA()
        {
            InitializeComponent();
        }
        private void frmLocacaoProgramacao_Load(object sender, EventArgs e)
        {
            // verificar quem é o usuário logado
            lblBloqueadoPeloUsuario.Text = geral.UsuarioAtual;
            oProgramacaoFechada.BloqueadaNomeUsuario = geral.UsuarioAtual;
            _bloqPor = oProgramacaoFechadaDados.UltimoRegistroNomeBloqueado();

            if (_bloqPor.Length == 0 || _bloqPor == "0")
            {
                _bloqPor = geral.UsuarioAtual; // nenhum usuário utilizando
                lblBloqueadoPeloUsuario.Text = geral.UsuarioAtual;
            }
            else
            {
                lblBloqueadoPeloUsuario.Text = _bloqPor;
                oProgramacaoFechada.BloqueadaNomeUsuario = _bloqPor;
            }

            DataProgAberta.Text = oProgramacaoFechadaDados.DataUltimaProgAberta();

            int nMes = DataProgAberta.Value.Month;
            if (nMes == 1)
                nMes = 12;
            treeView1.Nodes.Clear();
            for (int i = 1; i <= 2; i++)
            {
                treeView1.Nodes.Add((DataProgAberta.Value.Year - i + 1).ToString());
                int f = 0;
                for (int j = 1; j <= nMes; j++)
                {
                    TreeNode parentNode = treeView1.Nodes[i - 1];
                    if (parentNode != null)
                    {
                        parentNode.Nodes.Add(j.ToString("00"));
                        int nDias = 0;
                        if ((j == 1 || j == 3 || j == 5 || j == 7 || j == 8 || j == 10 || j == 12))
                            nDias = 31;
                        else if (j == 2)
                            nDias = Convert.ToDateTime("01/03/" + (DataProgAberta.Value.Year - i + 2).ToString()).AddDays(-1).Day;
                        else if (j == 4 || j == 6 || j == 9 || j == 11)
                            nDias = 30;
                        for (int k = 1; k <= nDias; k++)
                        {
                            TreeNode parentNodeMes = parentNode.Nodes[f];
                            parentNodeMes.Nodes.Add(k.ToString("00"));
                        }
                        f++;
                    }
                }
            }
            _dia = DataProgAberta.Value.Day.ToString("00");
            _mes = DataProgAberta.Value.Month.ToString("00");
            _ano = DataProgAberta.Value.Year.ToString();
            PreencheGrades(DataProgAberta.Text);
            ApontaParaDiaNoTreeView(DataProgAberta.Text);
        }
        private void PreencheGrades(string pDataProgAtual)
        {
            clsResiduoDados oResiduosDados = new clsResiduoDados();
            DataTable _dtResiduos = new DataTable();
            _dtResiduos = oResiduosDados.PreencheDataTableResiduos("Codigo", "");

            SILCNegocios.clsProgramacaoDiariaServicos oProgDiaria = new clsProgramacaoDiariaServicos();
            if (geral.IsNumeric(pDataProgAtual.Replace("/", "")))
            {
                string _strInClientes = "";
                _dtParaImprimirExecutados = oProgramacaoDados.PegaDados(Convert.ToInt32(Convert.ToDateTime(pDataProgAtual).ToString("yyyyMMdd")), 1, false);
                bindingSource.DataSource = _dtParaImprimirExecutados;

                foreach (DataRow _dr in _dtParaImprimirExecutados.Rows)
                {
                    if (_dr["NomeMotoristaOuDescricao"].ToString() != "")
                        _dr["NomeMotorista"] = _dr["NomeMotoristaOuDescricao"];
                    if (_dr["NomeMotorista"].ToString() != "")
                    {
                        if (_dr["NomeMotorista"].ToString().Split(" "[0]).Length > 1)
                            _dr["NomeMotorista"] = _dr["NomeMotorista"].ToString().Split(" "[0])[0];
                    }
                    _strInClientes = _strInClientes + ", " + _dr["CodigoCliente"].ToString();
                }
                DataTable _dtServicosFutura = new DataTable();
                if (_strInClientes.Length > 0)
                    _dtServicosFutura = oServicosFuturaDados.PreencheDataTable(_strInClientes.Substring(1), _dia + "/" + _mes + "/" + _ano);

                DataTable _dtParticularidadeContrato = new DataTable();
                clsContratoResiduosDados oContratoDados = new clsContratoResiduosDados();
                _dtParticularidadeContrato = oContratoDados.RetornaParticularidade();
                foreach (DataRow _dr in _dtParaImprimirExecutados.Rows)
                {
                    oProgDiaria.CodigoCliente = Convert.ToInt32(_dr["CodigoCliente"]);
                    oProgDiaria.CodigoResiduo = Convert.ToInt32(_dr["CodigoResiduo"]);
                    if (oProgDiaria.CodigoCliente == 73)
                    {
                        oProgDiaria.CodigoCliente = 73;
                    }
                    DataRow[] _drPart = _dtParticularidadeContrato.Select("CodigoCliente = " + oProgDiaria.CodigoCliente + " and CodigoResiduo = " + oProgDiaria.CodigoResiduo);
                    if (_drPart.Length > 0)
                        _dr["Particularidade"] = _drPart[0]["Particularidade"].ToString();

                    if (_dr["CodigoResiduo"].ToString() != "" && _dr["CodigoResiduo"].ToString() != "0" &&
                        _dr["DestinoFinal"].ToString() == "")
                    {
                        oProgDiaria.CodigoResiduo = Convert.ToInt32(_dr["CodigoResiduo"]);
                        DataRow[] _drResiduo = _dtResiduos.Select("Codigo = " + oProgDiaria.CodigoResiduo);
                        if (_drResiduo.Length > 0)
                            _dr["DestinoFinal"] = _drResiduo[0]["DestinoFinal"].ToString();
                    }
                    else if (_dr["ExecutarServico"].ToString() != "" && _dr["DestinoFinal"].ToString() == "")
                    {
                        DataRow[] _drResiduo = _dtResiduos.Select("");
                        if (_dr["ExecutarServico"].ToString().IndexOf("%") > -1)
                            _drResiduo = _dtResiduos.Select("DescricaoReduzida like '%" + _dr["ExecutarServico"].ToString().Split("%"[0])[0] + "%'");
                        else
                            _drResiduo = _dtResiduos.Select("DescricaoReduzida like '%" + _dr["ExecutarServico"].ToString() + "%'");
                        if (_drResiduo.Length > 0)
                            _dr["DestinoFinal"] = _drResiduo[0]["DestinoFinal"].ToString();
                    }
                    // Dados do Serviço que foram adicionados no futuro
                    DataRow[] _drServicosFutura;
                    if (_dr["CodigoResiduo"].ToString() != "" && _dr["CodigoResiduo"].ToString() != "0")
                    {
                        if (_dr["Hora"].ToString().IndexOf(":") > -1)
                        {
                            _drServicosFutura = _dtServicosFutura.Select(" CodigoCliente = " + _dr["CodigoCliente"].ToString() +
                                                                         " and CodigoResiduo = '" + _dr["CodigoResiduo"].ToString() + "'" +
                                                                         " and DataProgramada = '" + _dia + "/" + _mes + "/" + _ano + "'" +
                                                                         " and Hora = '" + _dr["Hora"].ToString() + "'", "Sequencial desc");
                        }
                        else
                        {
                            _drServicosFutura = _dtServicosFutura.Select(" CodigoCliente = " + _dr["CodigoCliente"].ToString() +
                                                                         " and CodigoResiduo = '" + _dr["CodigoResiduo"].ToString() + "'" +
                                                                         " and DataProgramada = '" + _dia + "/" + _mes + "/" + _ano + "'", "Sequencial desc");
                        }
                    }
                    else
                    {
                        _drServicosFutura = _dtServicosFutura.Select(" CodigoCliente = " + _dr["CodigoCliente"].ToString() +
                                                                     " and DescricaoResiduo = '" + _dr["ExecutarServico"].ToString() + "'" +
                                                                     " and DataProgramada = '" + _dia + "/" + _mes + "/" + _ano + "'" +
                                                                     " and Hora = '" + _dr["Hora"].ToString() + "'", "Sequencial desc");
                    }
                    if (_drServicosFutura.Length > 0)
                    {
                        int iReg = 0;
                        if (_drServicosFutura.Length > 1)
                            if (_drServicosFutura[iReg]["CodigoCaminhao"].ToString() == "0" || _drServicosFutura[iReg]["CodigoCaminhao"].ToString() == "")
                                iReg = 1;
                        _dr["CodigoCaminhao"] = _drServicosFutura[iReg]["CodigoCaminhao"];

                        if (_drServicosFutura[iReg]["CodigoResiduo"].ToString() == "0" || _drServicosFutura[iReg]["CodigoResiduo"].ToString() == "")
                        {
                            int _codigoResiduo = 0;
                            _codigoResiduo = oResiduosDados.PegaCodigoResiduo(_drServicosFutura[iReg]["DescricaoResiduo"].ToString(), true);
                            _dr["CodigoResiduo"] = _codigoResiduo;
                        }
                        _dr["ExecutarServico"] = _drServicosFutura[iReg]["DescricaoResiduo"];
                        _dr["ModeloCaminhao"] = _drServicosFutura[iReg]["ModeloCaminhao"];
                        _dr["CodigoMotorista"] = _drServicosFutura[iReg]["CodigoMotorista"];
                        _dr["NomeMotorista"] = _drServicosFutura[iReg]["NomeMotorista"];
                        if (_dr["NomeMotorista"].ToString() != "")
                        {
                            if (_dr["NomeMotorista"].ToString().Split(" "[0]).Length > 1)
                                _dr["NomeMotorista"] = _dr["NomeMotorista"].ToString().Split(" "[0])[0];
                        }
                        _dr["Map"] = "";
                        _dr["Observacao"] = _drServicosFutura[iReg]["Observacao"];
                        _dr["Solicitante"] = _drServicosFutura[iReg]["Solicitante"];
                        if (_drServicosFutura[iReg]["MapaMarcado"].ToString() == "1")
                            _dr["Map"] = "X";
                        _dr["HoraProgramada"] = _drServicosFutura[iReg]["ServicoAExecutar"];
                    }
                    if (_dr["Sequencial"].ToString() != "")
                    {
                        oLancDados = new clsLancamentosDados();
                        string _numerolancamento = oLancDados.PegaNumeroLancamento(Convert.ToInt32(_dr["Sequencial"]));
                        if (_numerolancamento != "" && _numerolancamento != "0")
                        {
                            _dr["HoraProgramada"] = "Lançado: " + _numerolancamento;
                        }
                        else if (_numerolancamento == "0")
                        {
                            string sVisto = oLancDados.PegaVistoProgramacaoLancamento(_numerolancamento, _dr["CodigoCliente"].ToString(), _dr["Sequencial"].ToString());
                            if (sVisto != "")
                                _dr["HoraProgramada"] = _dr["HoraProgramada"].ToString() + " - Visto";
                        }
                    }
                }

                _dtParaImprimirExecutados.DefaultView.Sort = "CodigoCaminhao, Linha";
                Grade1.DataSource = bindingSource.DataSource;
                EstiloGrades(Grade1);
                lblLinhasGrade2.Text = (Grade1.Rows.Count).ToString();

                RefreshCores();

            }
        }
        private void EstiloGrades(DataGridView oGrade)
        {
            oGrade.Columns["Sequencial"].Visible = false;
            oGrade.Columns["AnoMesDia"].Visible = false;
            oGrade.Columns["Quadro"].Visible = false;
            oGrade.Columns["Linha"].Visible = false;
            oGrade.Columns["StatusCor"].Visible = false;
            oGrade.Columns["SequencialParaQuadro1"].Visible = false;
            oGrade.Columns["Quantidade2"].Visible = false;
            oGrade.Columns["Unidade2"].Visible = false;
            oGrade.Columns["Map"].Visible = false;
            oGrade.Columns["CorObservacao"].Visible = false;
            oGrade.Columns["Origem"].Visible = false;
            oGrade.Columns["CodigoResiduo"].Visible = false;
            oGrade.Columns["CodigoMotorista"].Visible = false;
            oGrade.Columns["CodigoCaminhao"].Visible = false;
            oGrade.Columns["CodigoCliente"].Visible = false;
            oGrade.Columns["NomeMotoristaOuDescricao"].Visible = false;
            oGrade.Columns["RotaMapa"].Visible = false;
            oGrade.Columns["NovaLinha"].Visible = false;

            oGrade.Columns["Rp"].Width = 28;
            oGrade.Columns["Rp"].ReadOnly = true;
            oGrade.Columns["Data"].Width = 72;
            oGrade.Columns["Data"].ReadOnly = true;
            oGrade.Columns["Hora"].Width = 76;
            oGrade.Columns["Hora"].ReadOnly = true;
            oGrade.Columns["NomeFantasiaCliente"].Width = 200;
            oGrade.Columns["NomeFantasiaCliente"].HeaderText = "Cliente";
            oGrade.Columns["NomeFantasiaCliente"].ReadOnly = true;

            oGrade.Columns["Solicitante"].Width = 90;
            oGrade.Columns["Solicitante"].ReadOnly = true;

            oGrade.Columns["ExecutarServico"].Width = 200;
            oGrade.Columns["ExecutarServico"].HeaderText = "Resíduo";
            oGrade.Columns["ExecutarServico"].ReadOnly = true;

            oGrade.Columns["DataProgramada"].HeaderText = "Data Execução";
            oGrade.Columns["DataProgramada"].Width = 72;
            oGrade.Columns["DataProgramada"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            oGrade.Columns["DataProgramada"].ReadOnly = true;

            oGrade.Columns["HoraProgramada"].HeaderText = "Serviço Executado";

            oGrade.Columns["CidadeBairroEndereco"].Visible = false;
            oGrade.Columns["Rp"].Visible = false;

            oGrade.Columns["HoraProgramada"].Width = 150;

            oGrade.Columns["Quantidade"].HeaderText = "Quantidade";
            oGrade.Columns["Quantidade"].Width = 60;
            oGrade.Columns["Quantidade"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            oGrade.Columns["DestinoFinal"].ReadOnly = true;
            oGrade.Columns["Particularidade"].ReadOnly = true;
            oGrade.Columns["Observacao"].ReadOnly = true;
            oGrade.Columns["Unidade"].ReadOnly = true;

            oGrade.Columns["ModeloCaminhao"].HeaderText = "Caminhão";
            oGrade.Columns["ModeloCaminhao"].Width = 70;
            oGrade.Columns["ModeloCaminhao"].ReadOnly = true;
            oGrade.Columns["NomeMotorista"].HeaderText = "Motorista";
            oGrade.Columns["NomeMotorista"].Width = 90;
            oGrade.Columns["NomeMotorista"].ReadOnly = true;

            oGrade.Columns["Map"].Width = 32;
            oGrade.Columns["Map"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            oGrade.Columns["Map"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            oGrade.Columns["Map"].Visible = false;

            oGrade.Columns["TipoProgramacao"].HeaderText = "TP";
            oGrade.Columns["TipoProgramacao"].Width = 25;
            oGrade.Columns["TipoProgramacao"].Visible = false;

            oGrade.Columns["Unidade"].HeaderText = "Un";
            oGrade.Columns["Unidade"].Width = 25;
        }
        private void btnExecutado_Click(object sender, EventArgs e)
        {
            frmRelatorioExecutados frm = new frmRelatorioExecutados();
            frm.DataEmissao = _dia + "/" + _mes + "/" + _ano;
            PreencheGrades(_dia + "/" + _mes + "/" + _ano);
            frm.dtExecutados = _dtParaImprimirExecutados;
            frm.Show();
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (e.Node.Level == 0)
                _ano = e.Node.Text;
            if (e.Node.Level == 1)
                _mes = e.Node.Text;
            if (e.Node.Level == 2)
                _dia = e.Node.Text;
            if (_ano != "" && _mes != "" && _dia != "" && e.Node.Level == 2)
            {
                PreencheGrades(_dia + "/" + _mes + "/" + _ano);
            }
            this.Cursor = Cursors.Default;
        }
        private void btnDadosCliente_Click(object sender, EventArgs e)
        {
            frmClienteDados frmClientes = new frmClienteDados();
            frmClientes.lblNrCodigo.Text = lblCodigoCliente.Text;
            frmClientes.Show();
        }
        private void btnServicosCliente_Click(object sender, EventArgs e)
        {
            frmServicosRealizados frmServicosRealizados = new frmServicosRealizados();
            frmServicosRealizados.lblNrCodigo.Text = lblCodigoCliente.Text;
            frmServicosRealizados.txtNomeFantasia.Text = cdnmCliente.Text;
            frmServicosRealizados.Show();
        }
        private void btnDadosContrato_Click(object sender, EventArgs e)
        {
            frmContratoDados frmContratoDados = new frmContratoDados();
            frmContratoDados.lblNrCodigo.Text = lblCodigoCliente.Text;
            frmContratoDados.txtNomeFantasia.Text = cdnmCliente.Text;
            frmContratoDados.Show();
        }
        private void ApontaParaDiaNoTreeView(string pProximaData)
        {
            _ano = pProximaData.Substring(6, 4);
            _mes = pProximaData.Substring(3, 2);
            _dia = pProximaData.Substring(0, 2);
            for (int i = 1; i <= 2; i++)
            {
                TreeNode parentNode = treeView1.Nodes[i - 1];
                if (parentNode != null)
                {
                    if (parentNode.Text == _ano)
                    {
                        parentNode.Expand();
                        parentNode.Checked = true;
                        parentNode.Text = _ano;
                        for (int j = 1; j <= 12; j++)
                        {
                            TreeNode parentNodeMes;
                            try
                            {
                                parentNodeMes = parentNode.Nodes[j - 1];
                                if (parentNodeMes.Text == _mes)
                                {
                                    parentNodeMes.Expand();
                                    parentNodeMes.Checked = true;
                                    parentNodeMes.Text = _mes;

                                    for (int k = 1; k <= 31; k++)
                                    {
                                        TreeNode parentNodeDia = parentNodeMes.Nodes[k - 1];
                                        if (parentNodeDia.Text == _dia)
                                        {
                                            parentNodeDia.Expand();
                                            parentNodeDia.Checked = true;
                                            parentNodeDia.Text = _dia;
                                            i = 9;
                                            j = 12;
                                            break;
                                        }
                                    }
                                }

                            }
                            catch
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0)
                _ano = e.Node.Text;
            if (e.Node.Level == 1)
                _mes = e.Node.Text;
            if (e.Node.Level == 2)
                _dia = e.Node.Text;
            if (_ano != "" && _mes != "" && _dia != "" && e.Node.Level == 2)
            {
                PreencheGrades(_dia + "/" + _mes + "/" + _ano);
            }
        }
        private void frmProgramacaoDiaria_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
        private void btnRotaMapa_Click(object sender, EventArgs e)
        {
            if (MapSelecionado())
            {
                frmRotaMapa frm = new frmRotaMapa();
                frm.pAnoMesDia = Convert.ToInt32(_ano + _mes + _dia);
                frm.pDataProgramacaoAberta = _dia + "/" + _mes + "/" + _ano;
                frm._dt = new DataTable();
                frm.ShowDialog();
            }
            else
                MessageBox.Show("Nenhum serviço foi selecionado!", "SILC", MessageBoxButtons.OK);
        }
        private string ProcuraMotoristaJaSelecionado()
        {
            string sRet = "";
            return sRet;
        }
        private bool MapSelecionado()
        {
            bool bRet = false;
            return bRet;
        }
        private void RefreshCores()
        {
            for (int x = 0; x < Grade1.Rows.Count; x++)
            {
                if (Grade1.Rows[x].Cells[14].Value.ToString().IndexOf("Lançado:") > -1)
                {
                    for (int i = 0; i < Grade1.Columns.Count - 1; i++)
                    {
                        Grade1.Rows[x].Cells[i].Style.ForeColor = System.Drawing.Color.White;
                        Grade1.Rows[x].Cells[i].Style.BackColor = System.Drawing.Color.Green;
                    }
                }
                else if (Grade1.Rows[x].Cells[14].Value.ToString().IndexOf("Visto") > -1 && 
                    (Grade1.Rows[x].Cells["HoraProgramada"].Value.ToString().IndexOf("COLOCAR") > -1 ||
                     Grade1.Rows[x].Cells["HoraProgramada"].Value.ToString().IndexOf("RETIRAR") > -1 ||
                     Grade1.Rows[x].Cells["HoraProgramada"].Value.ToString().IndexOf("TROCAR") > -1  ||
                     Grade1.Rows[x].Cells["HoraProgramada"].Value.ToString().IndexOf("COLETAR") > -1))
                {
                    for (int i = 0; i < Grade1.Columns.Count - 1; i++)
                    {
                        Grade1.Rows[x].Cells[i].Style.ForeColor = System.Drawing.Color.White;
                        Grade1.Rows[x].Cells[i].Style.BackColor = System.Drawing.Color.Green;
                    }
                }
                else if (Grade1.Rows[x].Cells[14].Value.ToString().IndexOf("Visto") > -1)
                {
                    for (int i = 0; i < Grade1.Columns.Count - 1; i++)
                    {
                        Grade1.Rows[x].Cells[i].Style.ForeColor = System.Drawing.Color.White;
                        Grade1.Rows[x].Cells[i].Style.BackColor = System.Drawing.Color.YellowGreen;
                    }
                }
            }
        }

        private void Grade1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            oClientes = new clsClientes();
            oClienteDados = new clsClienteDados();
            if (e.RowIndex > -1)
            {
                oClienteDados.PegaDados(oClientes, Convert.ToInt32(Grade1.Rows[e.RowIndex].Cells["CodigoCliente"].Value));
                if (Grade1.Rows[e.RowIndex].Cells["CodigoCliente"].Value.ToString() != "")
                {
                    geral.CodigoCliente = oClientes.Codigo;
                    lblCodigoCliente.Text = oClientes.Codigo.ToString("000000");
                    cdnmCliente.Text = oClientes.NomeFantasia;
                    txtCNPJ_CPF.Text = geral.RetiraLetras(oClientes.CNPJ_CPF);
                    txtSenhaAcessoFatma.Text = oClientes.SenhaAcessoFatima;
                    if (Grade1.Rows[e.RowIndex].Cells["HoraProgramada"].Value.ToString().IndexOf("Lançado:") > -1)
                    {
                        string _nlanc = Grade1.Rows[e.RowIndex].Cells["HoraProgramada"].Value.ToString().Replace("Lançado: ", "");
                        clsLancamentoMTRDados oLancMTRDados = new clsLancamentoMTRDados();
                        txtNumeroMTRe.Text = oLancMTRDados.PegaNumeroMTRe(Convert.ToInt64(_nlanc), Convert.ToInt32(Grade1.Rows[e.RowIndex].Cells["CodigoResiduo"].Value)).ToString();
                    }
                    
                }
            }

        }
        private void btnMTRe_Click(object sender, EventArgs e)
        {

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://mtr.ima.sc.gov.br/mtrservice/verificaSituacaoManifesto");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            clsMTReLogin oLogin = new clsMTReLogin();

            string x = oLogin.manifestoCodigo.ToString();

            string json = "";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                json = json + "{\"manifestoCodigo\":" + txtNumeroMTRe.Text + ", ";
                json = json + "\"cnpGerador\":\"" + geral.RetiraCharsCNPJCPF(txtCNPJ_CPF.Text) + "\", ";
                json = json + "\"cnpTransportador\":\"" + oLogin.cnpTransportador + "\", ";
                json = json + "\"cnpDestinador\":\"" + txtCNPJDestinador.Text + "\", ";
                json = json + "\"login\":\"" + geral.RetiraCharsCNPJCPF(txtCNPJ_CPF.Text) + "\", ";
                json = json + "\"senha\":\"" + txtSenhaAcessoFatma.Text + "\"}";
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string xjson;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                xjson = result.ToString();
            }
            xjson = xjson.Replace("null", "-1");
            var post = JsonConvert.DeserializeObject<clsPostMTReConsulta>(xjson);

            MessageBox.Show(post.situacaoManifestoDescricao + " - retorno: " + post.retorno);
        }

        private void btnGera_Click(object sender, EventArgs e)
        {
            //var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://miramichi.procergs.com.br/mtrservice/salvarManifestoLote");
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://mtr.ima.sc.gov.br/mtrservice/salvarManifestoLote");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            clsParametros oPar = new clsParametros();
            clsParametrosDados oParDados = new clsParametrosDados();
            oPar = oParDados.PegaDados(oPar, 1);

            clsMTReLogin oLogin = new clsMTReLogin();
            oLogin.login = geral.RetiraLetras(txtCNPJ_CPF.Text);
            oLogin.senha = txtSenhaAcessoFatma.Text;
            

            string json = "";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                clsJsonCriar _oJsonCriar = new clsJsonCriar();
                ManifestoJSONDto _manifesto = new ManifestoJSONDto();

                _manifesto.cnpArmazenador = geral.RetiraCharsCNPJCPF(txtCNPJDestinador.Text);
                _manifesto.cnpDestinador = geral.RetiraCharsCNPJCPF(txtCNPJDestinador.Text);
                _manifesto.cnpGerador = geral.RetiraCharsCNPJCPF(txtCNPJ_CPF.Text);
                _manifesto.cnpTransportador = oLogin.cnpTransportador;

                _manifesto.codUnidadeArmazenador = 0;
                _manifesto.codUnidadeDestinador = "4";
                _manifesto.codUnidadeGerador = 0;
                _manifesto.codUnidadeTransportador = 0;
                _manifesto.manifData = DateTime.Now.ToString();
                _manifesto.manifDataExpedicao = DateTime.Now.ToString();
                _manifesto.manifestoCodigo = 9999999;
                _manifesto.manifGeradorCargoResponsavel = "Assistente administrativo";
                _manifesto.manifGeradorNomeResponsavel = geral.UsuarioAtual;
                _manifesto.manifObservacao = "";
                _manifesto.manifTransportadorDataExpedicao = Grade1.Rows[0].Cells["DataProgramada"].Value.ToString();
                oMotorista = new clsFuncionarios();
                if (Grade1.Rows[0].Cells["CodigoMotorista"].Value.ToString() != "")
                    oMotoristaDados.PegaDados(oMotorista, Convert.ToInt32(Grade1.Rows[0].Cells["CodigoMotorista"].Value.ToString()));
                _manifesto.manifTransportadorNomeMotorista = oMotorista.Nome;
                oCaminhao = new clsCaminhoes();
                if (Grade1.Rows[0].Cells["CodigoCaminhao"].Value.ToString() != "")
                {
                    oCaminhaoDados.PegaDados(oCaminhao, Convert.ToInt32(Grade1.Rows[0].Cells["CodigoCaminhao"].Value.ToString()));
                    _manifesto.manifTransportadorPlacaVeiculo = oCaminhao.Placas;
                }
                _manifesto.situacaoManifestoCodigo = 0;

                ItemManifestoJSONDto oitem = new ItemManifestoJSONDto();
                oitem.codigoAcondicionamento = 1;
                oitem.codigoClasse = 2;
                oitem.codigoInterno = "999";
                oitem.codigoSequencial = 1;
                oitem.codigoTecnologia = 7;
                oitem.codigoTipoEstado = 1;
                oitem.codigoUnidade = 4;
                oitem.justificativa = null;
                oitem.manifestoItemCodInterno = "";
                oitem.manifestoItemCodInternoDestinador = Grade1.Rows[0].Cells["CodigoResiduo"].Value.ToString();
                oitem.manifestoItemObservacao = Grade1.Rows[0].Cells["Observacao"].Value.ToString();
                oitem.quantidade = 0.01;

                clsResiduos oResiduo = new clsResiduos();
                clsResiduoDados oResiduoDados = new clsResiduoDados();
                oResiduoDados.PegaDados(oResiduo, Convert.ToInt32(Grade1.Rows[0].Cells["CodigoResiduo"].Value.ToString()));

                clsIBAMA oIbama = new clsIBAMA();
                clsIBAMADados oIbamaDados = new clsIBAMADados();
                oIbamaDados.PegaDados(oIbama, oResiduo.CodigoIBAMA);

                oitem.residuo = oIbama.CodigoIBAMA;
                oitem.tipoDensidadeUnidade = "1";
                oitem.tipoDensidadeValor = "1";
                _manifesto.itemManifestoJSONs.Add(oitem);

                json = _oJsonCriar.CriarJsonGeraLote(_manifesto, oLogin.login, oLogin.senha, Grade1.Rows[0].Cells["Sequencial"].Value.ToString());

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string xjson;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                xjson = result.ToString();
            }
            xjson = xjson.Replace("null", "-1");
            var post = JsonConvert.DeserializeObject<LoteManifestoJSON>(xjson);

            ManifestoJSONDto omanif = new ManifestoJSONDto();
            omanif = post.manifestoJSONDtos[0];
            MessageBox.Show("retorno: " + omanif.retorno + 
                            " \n" + "manifesto codigo: " + omanif.manifestoCodigo +
                            " \n" + "retorno  codigo: " + omanif.retornoCodigo +
                            " \n" + "situacao manifesto : " + omanif.situacaoManifestoCodigo);

        }
    }
}