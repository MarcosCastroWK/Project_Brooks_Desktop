using System;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using SILCNegocios;
using LibSILC;

namespace SILC.Web.forms
{
    public partial class programacao : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsProgramacaoFechadaDados oProgramacaoFechadaDados = new clsProgramacaoFechadaDados();
        clsProgramacaoDados oProgramacaoDados = new clsProgramacaoDados();
        clsProgramacaoDiariaServicos oProgDiaria = new clsProgramacaoDiariaServicos();
        clsReprogramacaoDados oReprogramacao = new clsReprogramacaoDados();
        clsServicosFuturaDados oServicosFuturaDados = new clsServicosFuturaDados();
        clsUsuarios oUsuario = new clsUsuarios();
        private string DataProgramada;
        private string _ano = "";
        private string _mes = "";
        private string _dia = "";
        private string _bloqPor = "";
        protected DataTable _dtParaImprimirProgramados;
        protected DataTable _dtParaImprimirExecutados;

        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
            clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
            if (oUsuario != null)
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "15");
            if (oItensMenuPermissoes.Consultar == 0)
                Response.Redirect("sempermissao.aspx");

            if (!IsPostBack)
            {
                txtDataProgramacaoAberta.Text = Convert.ToDateTime(oProgramacaoFechadaDados.DataUltimaProgAberta()).ToString("dd/MM/yyyy");
                DataProgramada = txtDataProgramacaoAberta.Text;
                if (txtDataProgramacaoAberta.Text != "" && txtDataProgramacaoAberta.Text != "&nbsp;")
                {
                    _ano = Convert.ToDateTime(txtDataProgramacaoAberta.Text).Year.ToString();
                    _mes = Convert.ToDateTime(txtDataProgramacaoAberta.Text).Month.ToString();
                    _dia = Convert.ToDateTime(txtDataProgramacaoAberta.Text).Day.ToString();
                }
                if (geral.Demonstracao)
                {
                    //Salvar.Enabled = false;
                }

                oUsuario = (clsUsuarios)Session["oUsuario"];
                if (oUsuario == null)
                {
                    Response.Redirect("brooks/loginaplicativo.aspx", true);
                }
                else
                {
                    try
                    {
                        if (oUsuario.Aplicativo == true)
                            menu.Visible = false;
                        else
                            menu.Visible = true;

                        trvPeriodo.Nodes.Clear();
                        TreeNode _tnRoot = new TreeNode();
                        TreeNode _tnChild = new TreeNode();
                        TreeNode _tnChild2 = new TreeNode();
                        int j = DateTime.Now.Year - 1;
                        for (int i = (DateTime.Now.Year + 1); i >= j; i--)
                        {
                            _tnRoot = new TreeNode();
                            for (int k = 1; k <= 12; k++)
                            {
                                _tnChild = new TreeNode();
                                _tnRoot.Text = i.ToString();
                                _tnChild.Text = k.ToString();
                                _tnChild.Target = "mes";
                                int diasMes = 28;  // Falta ver quando é bi-sexto
                                if (k == 4 || k == 6 || k == 9 || k == 11)
                                    diasMes = 30;
                                else if (k == 1 || k == 3 || k == 5 || k == 7 || k == 8 || k == 10 || k == 12)
                                    diasMes = 31;
                                for (int a = 1; a <= diasMes; a++)
                                {
                                    _tnChild2 = new TreeNode();
                                    _tnChild2.Text = a.ToString();
                                    _tnChild.ChildNodes.Add(_tnChild2);
                                }
                                _tnRoot.ChildNodes.Add(_tnChild);
                            }
                            trvPeriodo.Nodes.Add(_tnRoot);
                        }
                        trvPeriodo.CollapseAll();

                        Grade1.DataSource = oProgramacaoDados.PegaDados(Convert.ToInt32(Convert.ToDateTime(DataProgramada).ToString("yyyyMMdd")), 1, false);
                        Grade1.DataBind();

                        Grade2.DataSource = oProgramacaoDados.PegaDados(Convert.ToInt32(Convert.ToDateTime(DataProgramada).ToString("yyyyMMdd")), 2, true);
                        Grade2.DataBind();

                        lblLinhas.Text = "Linhas: " + (Grade1.Rows.Count).ToString() + " | " + (Grade2.Rows.Count).ToString();
                    }
                    catch (Exception ex)
                    {
                        //lblMensagem.Text = ex.Message;
                    }
                }
            }
        }
        protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string _cor = e.CommandName;
        }
        protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string _cor = e.Row.ToString();
            string _motorista = "";
            GridViewRow _grdviewrow = e.Row;
            _motorista = _grdviewrow.Cells[13].Text;
            if (_motorista != "")
            {
                if (_motorista.Length > 0)
                    _motorista = _motorista.Split(" "[0])[0];
                _grdviewrow.Cells[13].Text = _motorista;
            }
            _cor = _grdviewrow.Cells[19].Text;
            if (_cor != "StatusCor")
            {
                _grdviewrow.BackColor = System.Drawing.Color.BlueViolet;
                _grdviewrow.ForeColor = System.Drawing.Color.White;
                if (_grdviewrow.Cells[14].Text.IndexOf("BLOQUEIO FINANCEIRO") > -1)
                {
                    _grdviewrow.BackColor = System.Drawing.Color.Black;
                    _grdviewrow.ForeColor = System.Drawing.Color.White;
                }
                if (_grdviewrow.Cells[16].Text == "2")
                    _grdviewrow.Cells[16].Text = "Manual";
                else
                    _grdviewrow.Cells[16].Text = "Automática";
            }
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            trvPeriodo.CollapseAll();
            PreencheGrades(txtDataProgramacaoAberta.Text, ddlFiltro.Text, txtFiltro.Text);
        }
        protected void Grade2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //
        }
        protected void Grade2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string _cor = e.Row.ToString();
            string _motorista = "";
            GridViewRow _grdviewrow = e.Row;
            _motorista = _grdviewrow.Cells[13].Text;
            if (_motorista != "")
            {
                if (_motorista.Length > 0)
                    _motorista = _motorista.Split(" "[0])[0];
                _grdviewrow.Cells[13].Text = _motorista;
            }
            _cor = _grdviewrow.Cells[19].Text;
            if (_cor != "StatusCor")
            {
                _grdviewrow.BackColor = System.Drawing.Color.White;
                _grdviewrow.ForeColor = System.Drawing.Color.Black;
                if (_cor == "15000000")
                {
                    _grdviewrow.BackColor = System.Drawing.Color.CadetBlue;
                    _grdviewrow.ForeColor = System.Drawing.Color.White;
                }
                else if (_cor == "8421631")
                {
                    _grdviewrow.BackColor = System.Drawing.Color.Red;
                    _grdviewrow.ForeColor = System.Drawing.Color.White;
                }
                else if (_cor == "8427929")
                {
                    //TipoProgramacao
                    if (_grdviewrow.Cells[16].Text == "2" || _grdviewrow.Cells[16].Text == "M")
                    {
                        _grdviewrow.BackColor = System.Drawing.Color.Silver;
                        _grdviewrow.ForeColor = System.Drawing.Color.Black;
                    }
                }
                else if (_cor == "65280" || _cor == "65535")
                {
                    _grdviewrow.BackColor = System.Drawing.Color.Yellow;
                    _grdviewrow.ForeColor = System.Drawing.Color.Black;
                }
                else if (_cor == "16761024")
                {
                    _grdviewrow.BackColor = System.Drawing.Color.BlueViolet;
                    _grdviewrow.ForeColor = System.Drawing.Color.White;
                }
                if (_grdviewrow.Cells[14].Text.IndexOf("BLOQUEIO FINANCEIRO") > -1)
                {
                    _grdviewrow.BackColor = System.Drawing.Color.Black;
                    _grdviewrow.ForeColor = System.Drawing.Color.White;
                }
                if (_grdviewrow.Cells[16].Text == "2")
                    _grdviewrow.Cells[16].Text = "Manual";
                else
                    _grdviewrow.Cells[16].Text = "Automática";
            }
        }
        protected void trvPeriodo_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (trvPeriodo.SelectedNode.Text.Length <= 2)
            {
                txtFiltro.Text = "";
                if (geral.IsNumeric(trvPeriodo.SelectedNode.Text))
                {
                    if (Convert.ToDateTime(DataProgramada).Year < 2020)
                        DataProgramada = txtDataProgramacaoAberta.Text;
                    if (trvPeriodo.SelectedNode.Target == "mes")
                    {
                        DataProgramada = "01/" + Convert.ToInt16(trvPeriodo.SelectedNode.Text).ToString("00") + "/" + Convert.ToDateTime(DataProgramada).ToString("yyyy");
                        txtDataProgramacaoAberta.Text = DataProgramada;
                    }
                    else if (Convert.ToInt16(trvPeriodo.SelectedNode.Text) >= 1 && Convert.ToInt16(trvPeriodo.SelectedNode.Text) <= geral.UltimoDiaMes(DataProgramada))
                    {
                        txtDataProgramacaoAberta.Text = Convert.ToInt16(trvPeriodo.SelectedNode.Text).ToString("00") + "/" + Convert.ToDateTime(DataProgramada).ToString("MM/yyyy");
                        DataProgramada = txtDataProgramacaoAberta.Text;
                    }
                    if (DataProgramada != "" && DataProgramada != "&nbsp;")
                    {
                        _ano = Convert.ToDateTime(DataProgramada).Year.ToString();
                        _mes = Convert.ToDateTime(DataProgramada).Month.ToString();
                        _dia = Convert.ToDateTime(DataProgramada).Day.ToString();
                    }

                    string _dataAberta = geral.Left(oProgramacaoFechadaDados.DataUltimaProgAberta(), 10);
                    if (_dia + "/" + _mes + "/" + _ano == _dataAberta)
                    {
                        PreencheGrades(_dia + "/" + _mes + "/" + _ano, "", "");
                    }
                    else if (_dia + "/" + _mes + "/" + _ano != _dataAberta)
                    {
                        // programação futura
                        if (Convert.ToDateTime(_dia + "/" + _mes + "/" + _ano) > Convert.ToDateTime(_dataAberta))
                            MontaProgramacaoFutura();
                        else
                            PreencheGrades(_dia + "/" + _mes + "/" + _ano, "", "");
                    }
                }
            }
        }
        protected void trvPeriodo_Load(object sender, EventArgs e)
        {
            //txtFiltro.Text = "";
        }
        private void PreencheGrades(string pDataProgAtual, string pFiltro, string pTextoDoFiltro)
        {
            clsResiduoDados oResiduosDados = new clsResiduoDados();
            clsContratoResiduosDados oContratoDados = new clsContratoResiduosDados();
            DataTable _dtResiduos = new DataTable();
            _dtResiduos = oResiduosDados.PreencheDataTableResiduos("Codigo", "");
            if (geral.IsNumeric(pDataProgAtual.Replace("/", "")))
            {
                string _strInClientes = "";
                // executados
                if (pFiltro == "" && pTextoDoFiltro == "")
                    _dtParaImprimirExecutados = oProgramacaoDados.PegaDados(Convert.ToInt32(Convert.ToDateTime(pDataProgAtual).ToString("yyyyMMdd")), 1, false);
                else
                    _dtParaImprimirExecutados = oProgramacaoDados.PreencheDataTableProgramacao(pFiltro, pTextoDoFiltro);
                foreach (DataRow _dr in _dtParaImprimirExecutados.Rows)
                {
                    if (_dr["NomeMotoristaOuDescricao"].ToString() != "")
                        _dr["NomeMotorista"] = _dr["NomeMotoristaOuDescricao"];
                    _strInClientes = _strInClientes + ", " + _dr["CodigoCliente"].ToString();
                }
                DataTable _dtServicosFutura = new DataTable();
                DataTable _dtParticularidadeContrato = new DataTable();
                if (pFiltro == "" && pTextoDoFiltro == "")
                {
                    if (_strInClientes.Length > 0)
                        _dtServicosFutura = oServicosFuturaDados.PreencheDataTable(_strInClientes.Substring(1), _dia + "/" + _mes + "/" + _ano);

                    _dtParticularidadeContrato = oContratoDados.RetornaParticularidade();
                    foreach (DataRow _dr in _dtParaImprimirExecutados.Rows)
                    {
                        oProgDiaria.CodigoCliente = Convert.ToInt32(_dr["CodigoCliente"]);
                        if (oProgDiaria.CodigoCliente == 1366)
                        {
                            oProgDiaria.CodigoCliente = 1366;
                        }
                        DataRow[] _drPart = _dtParticularidadeContrato.Select("CodigoCliente = " + oProgDiaria.CodigoCliente);
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
                            _drServicosFutura = _dtServicosFutura.Select(" CodigoCliente = " + _dr["CodigoCliente"].ToString() +
                                                                            " and CodigoResiduo = '" + _dr["CodigoResiduo"].ToString() + "'" +
                                                                            " and DataProgramada = '" + _dia + "/" + _mes + "/" + _ano + "'" +
                                                                            " and Hora = '" + _dr["Hora"].ToString() + "'", "Sequencial desc");
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
                            _dr["CodigoCaminhao"] = _drServicosFutura[0]["CodigoCaminhao"];
                            _dr["CodigoResiduo"] = _drServicosFutura[0]["CodigoResiduo"];
                            _dr["ExecutarServico"] = _drServicosFutura[0]["DescricaoResiduo"];
                            _dr["ModeloCaminhao"] = _drServicosFutura[0]["ModeloCaminhao"];
                            _dr["CodigoMotorista"] = _drServicosFutura[0]["CodigoMotorista"];
                            _dr["NomeMotorista"] = _drServicosFutura[0]["NomeMotorista"];
                            _dr["Map"] = "";
                            _dr["Observacao"] = _drServicosFutura[0]["Observacao"];
                            _dr["Solicitante"] = _drServicosFutura[0]["Solicitante"];
                            if (_drServicosFutura[0]["MapaMarcado"].ToString() == "1")
                                _dr["Map"] = "X";
                            _dr["HoraProgramada"] = _drServicosFutura[0]["ServicoAExecutar"];
                        }

                    }
                }
                Grade1.DataSource = _dtParaImprimirExecutados;
                Grade1.DataBind();
                if (pFiltro != "" && pTextoDoFiltro != "")
                {
                    Grade2.DataSource = new DataTable();
                    Grade2.DataBind();
                }
                if (pFiltro == "" && pTextoDoFiltro == "")
                {
                    _strInClientes = "";
                    DataRow[] drAntecipacoes = oProgramacaoFechadaDados.PegaAntecipacoes(pDataProgAtual);
                    string strInAnoMesDia = "";

                    // programados
                    _dtParaImprimirProgramados = oProgramacaoDados.PegaDados(Convert.ToInt32(Convert.ToDateTime(pDataProgAtual).ToString("yyyyMMdd")), 2, true, "Convert(StatusCor, unsigned), Sequencial");
                    for (int i = 0; i < _dtParaImprimirProgramados.Rows.Count; i++)
                    {
                        DataRow _dr = _dtParaImprimirProgramados.Rows[i];
                        // Programação Automática
                        if (_dr["TipoProgramacao"].ToString() == "1" || _dr["TipoProgramacao"].ToString() == "0" || _dr["TipoProgramacao"].ToString() == "")
                            _dr["TipoProgramacao"] = "A";
                        // Programação Manual
                        if (_dr["TipoProgramacao"].ToString() == "2" || _dr["StatusCor"].ToString() == geral.RetornaCodigoCor("Cinza"))
                            _dr["TipoProgramacao"] = "M";
                        _strInClientes = _strInClientes + ", " + _dr["CodigoCliente"].ToString();

                        oProgDiaria.AnoMesDia = Convert.ToInt32(_dr["AnoMesDia"]);
                        oProgDiaria.Data = _dr["Data"].ToString();
                        oProgDiaria.CodigoCliente = Convert.ToInt32(_dr["CodigoCliente"]);
                        if (oProgDiaria.CodigoCliente == 1366)
                        {
                            oProgDiaria.CodigoCliente = 1366;
                        }

                        oProgDiaria.NomeCliente = _dr["NomeFantasiaCliente"].ToString();
                        oProgDiaria.ExecutarServico = _dr["ExecutarServico"].ToString();
                        oProgDiaria.StatusCor = _dr["StatusCor"].ToString();
                        if ((oProgDiaria.StatusCor == geral.RetornaCodigoCor("Vermelho") || oProgDiaria.StatusCor == geral.RetornaCodigoCor("Verde")) &&
                            strInAnoMesDia.IndexOf(Convert.ToDateTime(oProgDiaria.Data).ToString("yyyyMMdd")) < 0)
                            strInAnoMesDia = strInAnoMesDia + ", " + Convert.ToDateTime(oProgDiaria.Data).ToString("yyyyMMdd");
                        if ((oProgDiaria.StatusCor == geral.RetornaCodigoCor("Branco") || oProgDiaria.StatusCor == "") &&
                             _dr["TipoProgramacao"].ToString() == "A")
                        {
                            if (oProgramacaoFechadaDados.ExisteAntecipacao(drAntecipacoes, oProgDiaria))
                            {
                                _dtParaImprimirProgramados.Rows.RemoveAt(i);
                            }
                        }
                    }

                    oContratoDados = new clsContratoResiduosDados();
                    DataTable drReprogramacoes = oReprogramacao.PegaReprogramacoes("11110101");
                    if (strInAnoMesDia.Length > 0)
                    {
                        drReprogramacoes = oReprogramacao.PegaReprogramacoes(strInAnoMesDia.Substring(1));
                    }

                    _dtServicosFutura = new DataTable();
                    if (_strInClientes.Length > 0)
                        _dtServicosFutura = oServicosFuturaDados.PreencheDataTable(_strInClientes.Substring(1), _dia + "/" + _mes + "/" + _ano);
                    // colocar bloqueio financeiro
                    clsBloqFinanceiroDados oBloqFinanceiroDados = new clsBloqFinanceiroDados();
                    DataTable _dtBloqFinanceiros = oBloqFinanceiroDados.PreencheDataTableBloqueioFinanceiroInClientes(" 0");
                    if (_strInClientes.Length > 0)
                        _dtBloqFinanceiros = oBloqFinanceiroDados.PreencheDataTableBloqueioFinanceiroInClientes(_strInClientes.Substring(1));

                    foreach (DataRow _dr in _dtParaImprimirProgramados.Rows)
                    {
                        oProgDiaria.AnoMesDia = Convert.ToInt32(_dr["AnoMesDia"]);
                        oProgDiaria.Data = _dr["Data"].ToString();
                        oProgDiaria.CodigoCliente = Convert.ToInt32(_dr["CodigoCliente"]);
                        if (oProgDiaria.CodigoCliente == 1366)
                        {
                            oProgDiaria.CodigoCliente = 1366;
                        }

                        oProgDiaria.NomeCliente = _dr["NomeFantasiaCliente"].ToString();
                        oProgDiaria.ExecutarServico = _dr["ExecutarServico"].ToString();
                        oProgDiaria.StatusCor = _dr["StatusCor"].ToString();
                        if (oProgDiaria.StatusCor == geral.RetornaCodigoCor("Vermelho") || oProgDiaria.StatusCor == geral.RetornaCodigoCor("Verde"))
                        {
                            DataRow[] drrReprogs = drReprogramacoes.Select("AnoMesDia = " + Convert.ToDateTime(oProgDiaria.Data).ToString("yyyyMMdd") + " and Hora = '" + _dr["Hora"].ToString() + "' and CodigoCliente = " + _dr["CodigoCliente"].ToString());
                            _dr["Rp"] = drrReprogs.Length;
                        }
                        if (_strInClientes.Length > 0)
                        {
                            DataRow[] _drBloq = _dtBloqFinanceiros.Select("CodigoCliente = " + _dr["CodigoCliente"].ToString());
                            if (_drBloq.Length > 0)
                                _dr["Observacao"] = "BLOQUEIO FINANCEIRO DESDE: " + Convert.ToDateTime(_drBloq[0]["DataBloqueio"]).ToString("dd/MM/yyyy");
                            DataRow[] _drPart = _dtParticularidadeContrato.Select("CodigoCliente = " + oProgDiaria.CodigoCliente.ToString());
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
                                _drServicosFutura = _dtServicosFutura.Select(" CodigoCliente = " + _dr["CodigoCliente"].ToString() +
                                                                                " and CodigoResiduo = '" + _dr["CodigoResiduo"].ToString() + "'" +
                                                                                " and DataProgramada = '" + _dia + "/" + _mes + "/" + _ano + "'" +
                                                                                " and Hora = '" + _dr["Hora"].ToString() + "'", "Sequencial desc");
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
                                _dr["CodigoCaminhao"] = _drServicosFutura[0]["CodigoCaminhao"];
                                _dr["ModeloCaminhao"] = _drServicosFutura[0]["ModeloCaminhao"];
                                _dr["CodigoMotorista"] = _drServicosFutura[0]["CodigoMotorista"];
                                _dr["NomeMotorista"] = _drServicosFutura[0]["NomeMotorista"];
                                _dr["Map"] = "";
                                _dr["Observacao"] = _drServicosFutura[0]["Observacao"];
                                _dr["Solicitante"] = _drServicosFutura[0]["Solicitante"];
                                if (_drServicosFutura[0]["MapaMarcado"].ToString() == "1")
                                    _dr["Map"] = "X";
                                _dr["HoraProgramada"] = _drServicosFutura[0]["ServicoAExecutar"];
                            }
                        }
                    }

                    Grade2.DataSource = _dtParaImprimirProgramados;
                    Grade2.DataBind();
                }
                lblLinhas.Text = "Linhas: " + (Grade1.Rows.Count).ToString() + " | " + (Grade2.Rows.Count).ToString();
            }
        }
        private void MontaProgramacaoFutura()
        {
            if (_dia != "" && _dia != "&nbsp;" && geral.IsNumeric(_dia))
                _dia = Convert.ToInt16(_dia).ToString("00");
            if (_mes != "" & _mes != "&nbsp;" && geral.IsNumeric(_mes))
                _mes = Convert.ToInt16(_mes).ToString("00");

            clsResiduoDados oResiduosDados = new clsResiduoDados();
            DataTable _dtResiduos = new DataTable();
            _dtResiduos = oResiduosDados.PreencheDataTableResiduos("Codigo", "");

            _dtParaImprimirExecutados = oProgramacaoDados.PegaDados(Convert.ToInt32(Convert.ToDateTime(_dia + "/" + _mes + "/" + _ano).ToString("yyyyMMdd")), 1, false);

            Grade1.DataSource = _dtParaImprimirExecutados;
            Grade1.DataBind();

            oProgramacaoDados.ExcluirProgramacaoDia(Convert.ToDateTime(_dia + "/" + _mes + "/" + _ano).ToString("yyyyMMdd"));

            _dtParaImprimirProgramados = oProgramacaoFechadaDados.MontaProgramacao((_dia + "/" + _mes + "/" + _ano), _bloqPor);
            string _strInClientes = "";
            foreach (DataRow _dr in _dtParaImprimirProgramados.Rows)
            {
                if (_dr["TipoProgramacao"].ToString() == "1") // Programação Automática
                    _dr["TipoProgramacao"] = "A";
                if (_dr["TipoProgramacao"].ToString() == "2" || _dr["StatusCor"].ToString() == geral.RetornaCodigoCor("Cinza")) // Programação Manual
                    _dr["TipoProgramacao"] = "M";
                _strInClientes = _strInClientes + ", " + _dr["CodigoCliente"].ToString();
            }

            // colocar bloqueio financeiro
            clsBloqFinanceiroDados oBloqFinanceiroDados = new clsBloqFinanceiroDados();
            DataTable _dtBloqFinanceiros = oBloqFinanceiroDados.PreencheDataTableBloqueioFinanceiroInClientes(_strInClientes.Substring(1));

            clsEnderecosDados oEnderecoDados = new clsEnderecosDados();
            DataTable _dtEnderecos = new DataTable();
            _dtEnderecos = oEnderecoDados.PreencheDataTableEnderecosClientes(_strInClientes.Substring(1), 2);

            DataTable _dtServicosFutura = new DataTable();
            _dtServicosFutura = oServicosFuturaDados.PreencheDataTable(_strInClientes.Substring(1), _dia + "/" + _mes + "/" + _ano);
            DataTable _dtParticularidadeContrato = new DataTable();
            clsContratoResiduosDados oContratoDados = new clsContratoResiduosDados();
            _dtParticularidadeContrato = oContratoDados.RetornaParticularidade();

            // colocar endereço, bloqueiro financeiro, Dados da tabela ServicosFutura
            foreach (DataRow _dr in _dtParaImprimirProgramados.Rows)
            {
                if (_dr["CodigoCliente"].ToString() == "1366")
                {
                    int xteste = 0;
                }
                DataRow[] _drEnd = _dtEnderecos.Select("Codigo = " + _dr["CodigoCliente"].ToString());
                if (_drEnd.Length > 0)
                    _dr["CidadeBairroEndereco"] = _drEnd[0]["CidadeBairroEndereco"].ToString();
                DataRow[] _drBloq = _dtBloqFinanceiros.Select("CodigoCliente = " + _dr["CodigoCliente"].ToString());
                if (_drBloq.Length > 0)
                    _dr["Observacao"] = "BLOQUEIO FINANCEIRO DESDE: " + Convert.ToDateTime(_drBloq[0]["DataBloqueio"]).ToString("dd/MM/yyyy");
                DataRow[] _drServicosFutura;
                if (_dr["CodigoResiduo"].ToString() != "" && _dr["CodigoResiduo"].ToString() != "0")
                {
                    if (_dr["Hora"].ToString().IndexOf(":") > 0)
                        _drServicosFutura = _dtServicosFutura.Select(" CodigoCliente = " + _dr["CodigoCliente"].ToString() +
                                                                     " and CodigoResiduo = '" + _dr["CodigoResiduo"].ToString() + "'" +
                                                                     " and DataProgramada = '" + _dia + "/" + _mes + "/" + _ano + "'" +
                                                                     " and Hora = '" + _dr["Hora"].ToString() + "'", "Sequencial desc");
                    else
                        _drServicosFutura = _dtServicosFutura.Select(" CodigoCliente = " + _dr["CodigoCliente"].ToString() +
                                                                     " and CodigoResiduo = '" + _dr["CodigoResiduo"].ToString() + "'" +
                                                                     " and DataProgramada = '" + _dia + "/" + _mes + "/" + _ano + "'", "Sequencial desc");
                }
                else
                {
                    if (_dr["Hora"].ToString().IndexOf(":") > 0)
                        _drServicosFutura = _dtServicosFutura.Select(" CodigoCliente = " + _dr["CodigoCliente"].ToString() +
                                                                     " and DescricaoResiduo = '" + _dr["ExecutarServico"].ToString() + "'" +
                                                                     " and DataProgramada = '" + _dia + "/" + _mes + "/" + _ano + "'" +
                                                                     " and Hora = '" + _dr["Hora"].ToString() + "'", "Sequencial desc");
                    else
                        _drServicosFutura = _dtServicosFutura.Select(" CodigoCliente = " + _dr["CodigoCliente"].ToString() +
                                                                     " and DescricaoResiduo = '" + _dr["ExecutarServico"].ToString() + "'" +
                                                                     " and DataProgramada = '" + _dia + "/" + _mes + "/" + _ano + "'");

                }
                if (_drServicosFutura.Length > 0)
                {
                    if (_dr["StatusCor"].ToString() != "15000000")
                    {
                        _dr["CodigoCaminhao"] = _drServicosFutura[0]["CodigoCaminhao"];
                        _dr["ModeloCaminhao"] = _drServicosFutura[0]["ModeloCaminhao"];
                        _dr["CodigoMotorista"] = _drServicosFutura[0]["CodigoMotorista"];
                        _dr["NomeMotorista"] = _drServicosFutura[0]["NomeMotorista"];
                        _dr["Map"] = "";
                    }
                    else if (Convert.ToDateTime(_dr["DataProgramada"]) > Convert.ToDateTime(DataProgramada) && _dr["StatusCor"].ToString() == "15000000")
                    {
                        _dr["CodigoCaminhao"] = _drServicosFutura[0]["CodigoCaminhao"];
                        _dr["ModeloCaminhao"] = _drServicosFutura[0]["ModeloCaminhao"];
                        _dr["CodigoMotorista"] = _drServicosFutura[0]["CodigoMotorista"];
                        _dr["NomeMotorista"] = _drServicosFutura[0]["NomeMotorista"];
                        _dr["Map"] = "";
                    }
                    else if (_dr["StatusCor"].ToString() == "15000000")
                    {
                        _dr["CodigoCaminhao"] = 0;
                        _dr["ModeloCaminhao"] = "";
                        _dr["CodigoMotorista"] = 0;
                        _dr["NomeMotorista"] = "";
                        _dr["Map"] = "";
                    }
                    _dr["Observacao"] = _drServicosFutura[0]["Observacao"];
                    _dr["Solicitante"] = _drServicosFutura[0]["Solicitante"];
                    if (_drServicosFutura[0]["MapaMarcado"].ToString() == "1")
                        _dr["Map"] = "X";
                    _dr["HoraProgramada"] = _drServicosFutura[0]["ServicoAExecutar"];
                }
                DataRow[] _drPart = _dtParticularidadeContrato.Select("CodigoCliente = " + _dr["CodigoCliente"].ToString());
                if (_drPart.Length > 0)
                    _dr["Particularidade"] = _drPart[0]["Particularidade"].ToString();

                if (_dr["CodigoResiduo"].ToString() != "" && _dr["CodigoResiduo"].ToString() != "0" &&
                _dr["DestinoFinal"].ToString() == "")
                {
                    DataRow[] _drResiduo = _dtResiduos.Select("Codigo = " + Convert.ToInt32(_dr["CodigoResiduo"]));
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
            }

            Grade2.DataSource = _dtParaImprimirProgramados;
            Grade2.DataBind();

            lblLinhas.Text = "Linhas: " + (Grade1.Rows.Count).ToString() + " | " + (Grade2.Rows.Count).ToString();
        }

        protected void imbExcel1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (Grade1.HeaderRow == null)
            {
                lblTitulo0.Text = "Não há dados!";
                return;
            }

            Table table = new Table();
            TableRow row = new TableRow();

            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);

            Label lblEmpresa = new Label();
            Label lblTitulo = new Label();
            Label lblEmBranco = new Label();

            lblTitulo.ID = "lblTitulo";
            lblEmBranco.ID = "lblEmBranco";

            string NomeArq = "Programacao_Executados.xls";
            Grade1.EnableViewState = true;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", NomeArq));
            Grade1.EnableViewState = false;
            for (int i = 0; i < Grade1.HeaderRow.Cells.Count; i++)
                Grade1.HeaderRow.Cells[i].Text = geral.RemoverAcentos(Grade1.HeaderRow.Cells[i].Text);

            row = new TableRow();
            row.Cells.Add(new TableCell());
            lblEmpresa.ID = "lblEmpresa";
            lblEmpresa.Text = geral.NomeEmpresa(geral.CodigoEmpresa);
            row.Cells[0].Controls.Add(lblEmpresa);
            table.Rows.Add(row);

            lblEmBranco.Text = " \n";
            row = new TableRow();
            row.Cells.Add(new TableCell());
            row.Cells[0].Controls.Add(lblEmBranco);
            table.Rows.Add(row);

            lblTitulo.Text = "Programacao diaria de servicos - Executado de: " + txtDataProgramacaoAberta.Text;
            row = new TableRow();
            row.Cells.Add(new TableCell());
            row.Cells[0].Controls.Add(lblTitulo);
            table.Rows.Add(row);

            lblEmBranco.Text = " \n";
            row = new TableRow();
            row.Cells.Add(new TableCell());
            row.Cells[0].Controls.Add(lblEmBranco);
            table.Rows.Add(row);

            row = new TableRow();
            row.Cells.Add(new TableCell());
            row.Cells[0].Controls.Add(Grade1);
            table.Rows.Add(row);

            table.RenderControl(hw);
            HttpContext.Current.Response.Write(hw.InnerWriter);
            HttpContext.Current.Response.End();
        }

        protected void imbExcel0_Click(object sender, ImageClickEventArgs e)
        {
            if (Grade2.HeaderRow == null)
            {
                lblTitulo0.Text = "Não há dados!";
                return;
            }

            Table table = new Table();
            TableRow row = new TableRow();

            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);

            Label lblEmpresa = new Label();
            Label lblTitulo = new Label();
            Label lblEmBranco = new Label();

            lblTitulo.ID = "lblTitulo";
            lblEmBranco.ID = "lblEmBranco";

            string NomeArq = "Programacao_Programados.xls";
            Grade2.EnableViewState = true;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", NomeArq));
            Grade2.EnableViewState = false;
            for (int i = 0; i < Grade2.HeaderRow.Cells.Count; i++)
                Grade2.HeaderRow.Cells[i].Text = geral.RemoverAcentos(Grade2.HeaderRow.Cells[i].Text);

            row = new TableRow();
            row.Cells.Add(new TableCell());
            lblEmpresa.ID = "lblEmpresa";
            lblEmpresa.Text = geral.NomeEmpresa(geral.CodigoEmpresa);
            row.Cells[0].Controls.Add(lblEmpresa);
            table.Rows.Add(row);

            lblEmBranco.Text = " \n";
            row = new TableRow();
            row.Cells.Add(new TableCell());
            row.Cells[0].Controls.Add(lblEmBranco);
            table.Rows.Add(row);

            lblTitulo.Text = "Programacao diaria de servicos - Programacao de: " + txtDataProgramacaoAberta.Text;
            row = new TableRow();
            row.Cells.Add(new TableCell());
            row.Cells[0].Controls.Add(lblTitulo);
            table.Rows.Add(row);

            lblEmBranco.Text = " \n";
            row = new TableRow();
            row.Cells.Add(new TableCell());
            row.Cells[0].Controls.Add(lblEmBranco);
            table.Rows.Add(row);

            row = new TableRow();
            row.Cells.Add(new TableCell());
            row.Cells[0].Controls.Add(Grade2);
            table.Rows.Add(row);

            table.RenderControl(hw);
            HttpContext.Current.Response.Write(hw.InnerWriter);
            HttpContext.Current.Response.End();
        }
    }
}