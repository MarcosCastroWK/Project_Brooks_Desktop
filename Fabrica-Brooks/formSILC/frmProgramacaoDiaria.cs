using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using LibSILC;
using SILCNegocios;
using System.Net;

namespace formSILC
{
    public partial class frmProgramacaoDiaria : Form
    {
        private BindingSource bindingSource = new BindingSource();
        clsProgramacaoFechadaDados oProgramacaoFechadaDados = new clsProgramacaoFechadaDados();
        clsProgramacaoFechada oProgramacaoFechada = new clsProgramacaoFechada();
        clsProgramacaoDados oProgramacaoDados = new clsProgramacaoDados();
        clsCaminhoes oCaminhao = new clsCaminhoes();
        clsCaminhoesDados oCaminhaoDados = new clsCaminhoesDados();
        clsFuncionarios oMotorista = new clsFuncionarios();
        clsFuncionarioDados oMotoristaDados = new clsFuncionarioDados();
        clsReprogramacaoDados oReprogramacao = new clsReprogramacaoDados();
        clsStatusCorProgramacao oStatusCorProg = new clsStatusCorProgramacao();
        clsStatusCorProgramacaoDados oStatusCorProgDados = new clsStatusCorProgramacaoDados();
        clsServicosFutura oServicosFutura = new clsServicosFutura();
        clsServicosFuturaDados oServicosFuturaDados = new clsServicosFuturaDados();
        clsDestinoFinal oDestinoFinal = new clsDestinoFinal();
        clsDestinoFinalDados oDestinoFinalDados = new clsDestinoFinalDados();

        private DataTable _dt = new DataTable();
        private DataTable _dtParaImprimirExecutados = new DataTable();
        private DataTable _dtParaImprimirProgramados = new DataTable();

        private string _ano = "";
        private string _mes = "";
        private string _dia = "";
        private string _bloqPor;
        private bool bPodeEncerrar = true;

        private int LinhaSelecionadaGrade2 = 0;
        private int LinhaSelecionadaGrade1 = 0;

        public frmProgramacaoDiaria()
        {
            InitializeComponent();
        }

        private void frmProgramacaoDiaria_Load(object sender, EventArgs e)
        {
            // verificar quem é o usuário logado
            lblBloqueadoPeloUsuario.Text = geral.UsuarioAtual; 
            oProgramacaoFechada.BloqueadaNomeUsuario = geral.UsuarioAtual;
            _bloqPor = oProgramacaoFechadaDados.UltimoRegistroNomeBloqueado();
            clsUsuarioDados oUsuarioDados = new clsUsuarioDados();
            if (geral.CodigoUsuarioAtual == 0)
                geral.CodigoUsuarioAtual = oUsuarioDados.PegaCodigoUsuario(geral.UsuarioAtual, oUsuarioDados.PegaSenha(geral.UsuarioAtual, 1), 1);
            clsDB oDB = new clsDB();
            oDB.ConectaMySql();
            this.Text = "Programação Diária de Serviços - Usuário logado: " + geral.CodigoUsuarioAtual.ToString() + " - Servidor atual: " + oDB.NomeServidor;
            oDB.DesconectaMySql();

            if (_bloqPor.Length == 0 || _bloqPor == "0")
            {
                _bloqPor = geral.UsuarioAtual; // nenhum usuário utilizando
                lblBloqueadoPeloUsuario.Text = geral.UsuarioAtual;
                oProgramacaoFechadaDados.UsuarioQueBloqueouProgramacaoAberta(geral.UsuarioAtual);
            }
            else
            {
                lblBloqueadoPeloUsuario.Text = _bloqPor;
                oProgramacaoFechada.BloqueadaNomeUsuario = _bloqPor;
            }

            if (geral.UsuarioAtual.ToLower().IndexOf("geferson") > -1)
                ckbOcultarServicoRealizado.Checked = true;
            else
                ckbOcultarServicoRealizado.Checked = false;

            DataProgAberta.Text = oProgramacaoFechadaDados.DataUltimaProgAberta();

            int nMes = 12;
            treeView1.Nodes.Clear();
            for (int i = 1; i <= 9; i++)
            {
                treeView1.Nodes.Add((DateTime.Now.Year - i + 2).ToString());
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
                            nDias = Convert.ToDateTime("01/03/" + (DateTime.Now.Year - i + 2).ToString()).AddDays(-1).Day;
                        else if (j == 4 || j == 6 || j == 9 || j == 11)
                            nDias = 30;
                        for (int k = 1; k <= nDias; k++)
                        {
                            TreeNode parentNodeMes = parentNode.Nodes[j - 1];
                            parentNodeMes.Nodes.Add(k.ToString("00"));
                        }
                    }
                }
            }
            ApontaParaDiaNoTreeView(DataProgAberta.Text);
            PreencheGrades(DataProgAberta.Text);
        }

        private void PreencheGrades(string pDataProgAtual)
        {
            Grade1.DataSource = "";
            Grade1.Refresh();

            Grade2.DataSource = "";
            Grade2.Refresh();

            clsResiduoDados oResiduosDados = new clsResiduoDados();
            DataTable _dtResiduos = new DataTable();
            _dtResiduos = oResiduosDados.PreencheDataTableResiduos("Codigo", "");

            SILCNegocios.clsProgramacaoDiariaServicos oProgDiaria = new clsProgramacaoDiariaServicos();
            if (geral.IsNumeric(pDataProgAtual.Replace("/", "")))
            {
                string _strInClientes = "";
                
                // executados
                _dtParaImprimirExecutados = oProgramacaoDados.PegaDados(Convert.ToInt32(Convert.ToDateTime(pDataProgAtual).ToString("yyyyMMdd")), 1, false);
                bindingSource.DataSource = _dtParaImprimirExecutados;
                foreach (DataRow _dr in _dtParaImprimirExecutados.Rows)
                {
                    if (_dr["NomeMotoristaOuDescricao"].ToString() != "")
                        _dr["NomeMotorista"] = _dr["NomeMotoristaOuDescricao"];
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
                    if (oProgDiaria.CodigoCliente == 2708)
                    {
                        string xtst = "2708";
                    }
                    DataRow[] _drPart = _dtParticularidadeContrato.Select("CodigoCliente = " + oProgDiaria.CodigoCliente.ToString() + " and CodigoResiduo = " + oProgDiaria.CodigoResiduo.ToString());
                    if (_drPart.Length > 0)
                        _dr["Particularidade"] = _drPart[0]["Particularidade"].ToString();

                    if (_dr["CodigoResiduo"].ToString() != "" && _dr["CodigoResiduo"].ToString() != "0" &&
                        _dr["DestinoFinal"].ToString()  == "")
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
                    if (_dr["CodigoCliente"].ToString() == "2708")
                    {
                        string xtst = "2708";
                    }
                    // Dados do Serviço que foram adicionados no futuro
                    string _cons = "";
                    DataRow[] _drServicosFutura;
                    if (_dr["CodigoResiduo"].ToString() != "" && _dr["CodigoResiduo"].ToString() != "0")
                    {
                        _drServicosFutura = _dtServicosFutura.Select(" CodigoCliente = " + _dr["CodigoCliente"].ToString() +
                                                                     " and CodigoResiduo = '" + _dr["CodigoResiduo"].ToString() + "'" +
                                                                     " and DataProgramada = '" + _mes  + "/" + _dia + "/" + _ano + "'" +
                                                                     " and Hora = '" + _dr["Hora"].ToString() + "'", "Sequencial desc");
                        if (_drServicosFutura.Length == 0)
                        {
                            _cons = "";
                            _cons = _cons + " CodigoCliente = " + _dr["CodigoCliente"].ToString();
                            _cons = _cons + " and CodigoResiduo = '" + _dr["CodigoResiduo"].ToString() + "'";
                            _cons = _cons + " and DataProgramada = '" + _dia + "/" + _mes + "/" + _ano + "'";
                            if (_dr["Hora"].ToString().IndexOf(":") > 0)
                                _cons = _cons + " and Hora = '" + _dr["Hora"].ToString() + "'";
                            else
                                _cons = _cons + " and not hora like '%:%'";
                            _drServicosFutura = _dtServicosFutura.Select(_cons, "Sequencial desc");
                        }
                    }
                    else
                    {
                        _drServicosFutura = _dtServicosFutura.Select(" CodigoCliente = " + _dr["CodigoCliente"].ToString() +
                                                                     " and DescricaoResiduo = '" + _dr["ExecutarServico"].ToString() + "'" +
                                                                     " and DataProgramada = '" + _dia + "/" + _mes + "/" + _ano + "'" +
                                                                     " and Hora = '" + _dr["Hora"].ToString() + "'", "Sequencial desc");
                        if (_drServicosFutura.Length == 0)
                        {
                            _cons = "";
                            _cons = _cons + " CodigoCliente = " + _dr["CodigoCliente"].ToString();
                            _cons = _cons + " and DescricaoResiduo = '" + _dr["ExecutarServico"].ToString() + "'";
                            _cons = _cons + " and DataProgramada = '" + _dia + "/" + _mes + "/" + _ano + "'";
                            if (_dr["Hora"].ToString().IndexOf(":") > 0)
                                _cons = _cons + " and Hora = '" + _dr["Hora"].ToString() + "'";
                            else
                                _cons = _cons + " and not hora like '%:%'";
                            _drServicosFutura = _dtServicosFutura.Select(_cons, "Sequencial desc");
                        }
                    }
                    if (_drServicosFutura.Length > 0)
                    {
                        int iReg = 0;
                        if (_drServicosFutura.Length > 1)
                            if (_drServicosFutura[iReg]["CodigoCaminhao"].ToString() == "0" || _drServicosFutura[iReg]["CodigoCaminhao"].ToString() == "")
                                iReg = 1;
                        _dr["CodigoCaminhao"] = _drServicosFutura[iReg]["CodigoCaminhao"];
                        _dr["CodigoResiduo"] = _drServicosFutura[iReg]["CodigoResiduo"];
                        _dr["ExecutarServico"] = _drServicosFutura[iReg]["DescricaoResiduo"];
                        _dr["ModeloCaminhao"] = _drServicosFutura[iReg]["ModeloCaminhao"];
                        _dr["CodigoMotorista"] = _drServicosFutura[iReg]["CodigoMotorista"];
                        _dr["NomeMotorista"] = _drServicosFutura[iReg]["NomeMotorista"];
                        _dr["Map"] = "";
                        _dr["Observacao"] = _drServicosFutura[iReg]["Observacao"];
                        _dr["Solicitante"] = _drServicosFutura[iReg]["Solicitante"];
                        if (_drServicosFutura[iReg]["MapaMarcado"].ToString() == "1")
                            _dr["Map"] = "X";
                        _dr["HoraProgramada"] = _drServicosFutura[iReg]["ServicoAExecutar"];
                        _dr["NumeroMTRe"] = _drServicosFutura[iReg]["NumeroMTRe"];
                    }
                }
                Grade1.DataSource = bindingSource.DataSource;
                EstiloGrades(Grade1);

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
                    oProgDiaria.CodigoResiduo = Convert.ToInt32(_dr["CodigoResiduo"]);
                    if (oProgDiaria.CodigoCliente == 2748)
                    {
                        oProgDiaria.CodigoCliente = 2748;
                    }
                    DataRow[] _drPart = _dtParticularidadeContrato.Select("CodigoCliente = " + oProgDiaria.CodigoCliente.ToString() + " and CodigoResiduo = " + oProgDiaria.CodigoResiduo.ToString());
                    if (_drPart.Length > 0)
                        _dr["Particularidade"] = _drPart[0]["Particularidade"].ToString();

                    oProgDiaria.NomeCliente = _dr["NomeFantasiaCliente"].ToString();
                    oProgDiaria.ExecutarServico = _dr["ExecutarServico"].ToString();
                    oProgDiaria.StatusCor = _dr["StatusCor"].ToString();
                    if ((oProgDiaria.StatusCor == geral.RetornaCodigoCor("Vermelho") || oProgDiaria.StatusCor == geral.RetornaCodigoCor("Verde") ||
                         oProgDiaria.StatusCor == geral.RetornaCodigoCor("AzulClaro")) &&
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
                DataTable _dtBloqFinanceiros = oBloqFinanceiroDados.PreencheDataTableBloqueioFinanceiroInClientes("0"); 
                if (_strInClientes.Length > 0)
                    _dtBloqFinanceiros = oBloqFinanceiroDados.PreencheDataTableBloqueioFinanceiroInClientes(_strInClientes.Substring(1));

                foreach (DataRow _dr in _dtParaImprimirProgramados.Rows)
                {
                    oProgDiaria.AnoMesDia = Convert.ToInt32(_dr["AnoMesDia"]);
                    oProgDiaria.Data = _dr["Data"].ToString();
                    oProgDiaria.CodigoCliente = Convert.ToInt32(_dr["CodigoCliente"]);
                    if (oProgDiaria.CodigoCliente == 3195)
                    {
                        oProgDiaria.CodigoCliente = 3195;
                    }

                    oProgDiaria.NomeCliente = _dr["NomeFantasiaCliente"].ToString();
                    oProgDiaria.ExecutarServico = _dr["ExecutarServico"].ToString();
                    oProgDiaria.StatusCor = _dr["StatusCor"].ToString();
                    if (oProgDiaria.StatusCor == geral.RetornaCodigoCor("Vermelho") || oProgDiaria.StatusCor == geral.RetornaCodigoCor("Verde") || 
                        oProgDiaria.StatusCor == geral.RetornaCodigoCor("AzulClaro") || oProgDiaria.StatusCor == geral.RetornaCodigoCor("Amarelo"))
                    {
                        DataRow[] drrReprogs = drReprogramacoes.Select("AnoMesDia = " + Convert.ToDateTime(oProgDiaria.Data).ToString("yyyyMMdd") + " and Hora = '" + _dr["Hora"].ToString() + "' and CodigoCliente = " + _dr["CodigoCliente"].ToString());
                        _dr["Rp"] = drrReprogs.Length;
                    }
                    if (_strInClientes.Length > 0)
                    {
                        DataRow[] _drBloq = _dtBloqFinanceiros.Select("CodigoCliente = " + _dr["CodigoCliente"].ToString());
                        if (_drBloq.Length > 0)
                            _dr["Observacao"] = "BLOQUEIO FINANCEIRO DESDE: " + Convert.ToDateTime(_drBloq[0]["DataBloqueio"]).ToString("dd/MM/yyyy");
                        DataRow[] _drPart = _dtParticularidadeContrato.Select("CodigoCliente = " + oProgDiaria.CodigoCliente.ToString() + " and CodigoResiduo = " + oProgDiaria.CodigoResiduo);
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
                        if (_dr["CodigoCliente"].ToString() == "2748")
                        {
                            string tst = "teste monstrando motorista e caminhao errado!!! quando feita antecipação pro dia da prog.aberta. Codigo residuo = " + _dr["CodigoResiduo"].ToString();
                        }
                        // Dados do Serviço que foram adicionados no futuro
                        string _cons = "";
                        DataRow[] _drServicosFutura;
                        if (_dr["CodigoResiduo"].ToString() != "" && _dr["CodigoResiduo"].ToString() != "0")
                        { 
                            _drServicosFutura = _dtServicosFutura.Select(" CodigoCliente = " + _dr["CodigoCliente"].ToString() +
                                                                         " and CodigoResiduo = " + _dr["CodigoResiduo"].ToString() +
                                                                         " and DataProgramada = '" + Convert.ToDateTime(_dr["DataProgramada"]).ToString("dd/MM/yyyy") + "'" +
                                                                         " and Hora = '" + _dr["Hora"].ToString() + "'", "DataProgramada desc");
                            if (_drServicosFutura.Length == 0)
                            {
                                _cons = "";
                                _cons = _cons + " CodigoCliente = " + _dr["CodigoCliente"].ToString();
                                _cons = _cons + " and CodigoResiduo = '" + _dr["CodigoResiduo"].ToString() + "'";
                                _cons = _cons + " and DataProgramada = '" + _dia + "/" + _mes + "/" + _ano + "'";
                                //_cons = _cons + " and DataProgramada = '" + Convert.ToDateTime(_dr["DataProgramada"]).ToString("dd/MM/yyyy") + "'";
                                if (_dr["Hora"].ToString().IndexOf(":") > 0)
                                    _cons = _cons + " and Hora = '" + _dr["Hora"].ToString() + "'";
                                else
                                    _cons = _cons + " and not hora like '%:%'"; 
                                _drServicosFutura = _dtServicosFutura.Select(_cons, "Sequencial desc");
                            }
                        }
                        else
                        {
                            _drServicosFutura = _dtServicosFutura.Select(" CodigoCliente = " + _dr["CodigoCliente"].ToString() +
                                                                         " and DescricaoResiduo = '" + _dr["ExecutarServico"].ToString() + "'" +
                                                                         " and DataProgramada = '" + _dia + "/" + _mes + "/" + _ano + "'" +
                                                                         " and Hora = '" + _dr["Hora"].ToString() + "'", "Sequencial desc");
                            if (_drServicosFutura.Length == 0)
                            {
                                _cons = "";
                                _cons = _cons + " CodigoCliente = " + _dr["CodigoCliente"].ToString();
                                _cons = _cons + " and DescricaoResiduo = '" + _dr["ExecutarServico"].ToString() + "'";
                                //_cons = _cons + " and DataProgramada >= '" + _dia + "/" + _mes + "/" + _ano + "'";
                                _cons = _cons + " and DataProgramada = '" + Convert.ToDateTime(_dr["DataProgramada"]).ToString("dd/MM/yyyy") + "'";
                                if (_dr["Hora"].ToString().IndexOf(":") > 0)
                                    _cons = _cons + " and Hora = '" + _dr["Hora"].ToString() + "'";
                                else
                                    _cons = _cons + " and not hora like '%:%'";
                                _drServicosFutura = _dtServicosFutura.Select(_cons, "Sequencial desc");
                            }
                        }
                        if (_drServicosFutura.Length == 1)
                        {
                            if ( (_dr["Hora"].ToString() == _drServicosFutura[0]["Hora"].ToString() && 
                                  _dr["CodigoCliente"].ToString() == _drServicosFutura[0]["CodigoCliente"].ToString() &&
                                  _dr["CodigoResiduo"].ToString() == _drServicosFutura[0]["CodigoResiduo"].ToString())
                                  ||
                                 (_dr["CodigoCliente"].ToString() == _drServicosFutura[0]["CodigoCliente"].ToString() &&
                                  _dr["CodigoResiduo"].ToString() == _drServicosFutura[0]["CodigoResiduo"].ToString() && 
                                  _dr["TipoProgramacao"].ToString() == "A")
                                  ||
                                 (_dr["CodigoCliente"].ToString() == _drServicosFutura[0]["CodigoCliente"].ToString() &&
                                  _dr["ExecutarServico"].ToString() == _drServicosFutura[0]["DescricaoResiduo"].ToString() &&
                                  _dr["TipoProgramacao"].ToString() == "A")
                               )
                            {
                                _dr["CodigoCaminhao"] = _drServicosFutura[0]["CodigoCaminhao"];
                                _dr["ModeloCaminhao"] = _drServicosFutura[0]["ModeloCaminhao"];
                                _dr["CodigoMotorista"] = _drServicosFutura[0]["CodigoMotorista"];
                                _dr["NomeMotorista"] = _drServicosFutura[0]["NomeMotorista"];
                                _dr["Map"] = "";
                                _dr["DestinoFinal"] = _drServicosFutura[0]["DestinoFinal"];  // está linha estava comentada - têm que descobrir porque? não faz sentido 13/03/2024

                                _dr["Observacao"] = _drServicosFutura[0]["Observacao"];
                                _dr["Solicitante"] = _drServicosFutura[0]["Solicitante"];
                                if (_drServicosFutura[0]["MapaMarcado"].ToString() == "1")
                                    _dr["Map"] = "X";
                                else
                                    _dr["Map"] = "";
                                _dr["HoraProgramada"] = _drServicosFutura[0]["ServicoAExecutar"];
                                _dr["NumeroMTRe"] = _drServicosFutura[0]["NumeroMTRe"];
                            }
                        }
                        else if (_drServicosFutura.Length > 0)
                        {
                            int x0 = _drServicosFutura.Length;
                        }
                    }
                }
                foreach (DataRow _dr in _dtParaImprimirProgramados.Rows)
                {
                    if (_dr["CodigoCliente"].ToString() == "1407")
                    {
                        _dr["CodigoCliente"] = 1407;
                    }
                    if (_dr["Rp"].ToString() != "" && _dr["Rp"].ToString() != "0" || _dr["TipoProgramacao"].ToString() == "M" || _dr["TipoProgramacao"].ToString() == "2")
                    {
                        DataTable _dtUlt = oReprogramacao.PegaUltimaReprogramacaoAntecipacao("", _dr["Hora"].ToString(), _dr["CodigoCliente"].ToString());
                        if (_dtUlt.Rows.Count > 0)
                        {

                            _dr["DataProgramada"] = _dtUlt.Rows[0]["DataProgramada"];
                            if (_dr["StatusCor"].ToString() != geral.RetornaCodigoCor("Verde"))
                                _dr["StatusCor"] = _dtUlt.Rows[0]["StatusCor"];

                        }
                    }
                    if (_dr["Rp"].ToString() != "" && _dr["Rp"].ToString() != "0" || _dr["TipoProgramacao"].ToString() == "M" || _dr["TipoProgramacao"].ToString() == "2" ||
                        _dr["StatusCor"].ToString() == geral.RetornaCodigoCor("AzulClaro"))
                    {
                        if (pDataProgAtual == _dia + "/" + _mes + "/" + _ano && pDataProgAtual == DataProgAberta.Text)
                        {
                            if (Convert.ToDateTime(_dr["DataProgramada"]) < DataProgAberta.Value)
                                _dr["StatusCor"] = Color.Aquamarine.ToString();
                            /*
                            string _codigoResiduo = _dr["CodigoResiduo"].ToString();
                            if (_codigoResiduo == "0" || _codigoResiduo == "")
                            {
                                _codigoResiduo = oResiduosDados.PegaCodigoResiduo(_dr["ExecutarServico"].ToString()).ToString();
                            }
                            if (oProgramacaoDados.ExisteProgramacaoFechada(_dr["Hora"].ToString(), _dr["CodigoCliente"].ToString(), _codigoResiduo, _dr["Solicitante"].ToString()))
                            {
                                // marca como feito - azul marine
                                _dr["StatusCor"] = Color.Aquamarine.ToString();
                            }
                            */
                        }
                    }
                }
                bindingSource.DataSource = _dtParaImprimirProgramados;
                Grade2.DataSource = bindingSource.DataSource;
                EstiloGrades(Grade2);
                lblLinhasGrade2.Text = (Grade1.Rows.Count).ToString() + " | " + (Grade2.Rows.Count).ToString();

                if (ckbOcultarServicoRealizado.Checked)
                    OcultarServicoRealizado();
            }
        }
        private void OcultarServicoRealizado()
        {
            // remove linha(s) com serviço já feito.
            for (int iGd2 = 0; iGd2 <= Grade2.Rows.Count - 1; iGd2++)
            {
                if (Grade2.Rows[iGd2].Cells["StatusCor"].Value.ToString().IndexOf("Aquamarine") > -1) // serviço feito
                {
                    Grade2.Rows.RemoveAt(iGd2);
                    iGd2--;
                }
            }
            Grade2.Refresh();
            lblLinhasGrade2.Text = (Grade1.Rows.Count).ToString() + " | " + (Grade2.Rows.Count).ToString();
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
            if (oGrade.Name == "Grade1")
                oGrade.Columns["Map"].Visible = false;
            oGrade.Columns["CorObservacao"].Visible = false;
            oGrade.Columns["Origem"].Visible = false;
            oGrade.Columns["CodigoResiduo"].Visible = false;
            oGrade.Columns["CodigoMotorista"].Visible = false;
            oGrade.Columns["CodigoCaminhao"].Visible = false;
            oGrade.Columns["CodigoCliente"].Visible = false;
            oGrade.Columns["NomeMotoristaOuDescricao"].Visible = false;
            oGrade.Columns["RotaMapa"].Visible = false;

            oGrade.Columns["Rp"].Width = 28;
            oGrade.Columns["Data"].Width = 70;
            oGrade.Columns["Hora"].Width = 66;

            oGrade.Columns["NomeFantasiaCliente"].Width = 200;
            oGrade.Columns["NomeFantasiaCliente"].HeaderText = "Cliente";

            oGrade.Columns["Solicitante"].Width = 90;

            oGrade.Columns["ExecutarServico"].Width = 200;
            oGrade.Columns["ExecutarServico"].HeaderText = "Resíduo";

            oGrade.Columns["DataProgramada"].HeaderText = "Data Programada";
            oGrade.Columns["DataProgramada"].Width = 70;
            oGrade.Columns["DataProgramada"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (oGrade.Name == "Grade1")
            {
                oGrade.Columns["HoraProgramada"].HeaderText = "Serviço Executado";
                oGrade.Columns["CidadeBairroEndereco"].Visible = false;
                oGrade.Columns["Rp"].Visible = false;
            }
            else if (oGrade.Name == "Grade2")
                oGrade.Columns["HoraProgramada"].HeaderText = "Serviço a Executar";

            oGrade.Columns["HoraProgramada"].Width = 150;

            oGrade.Columns["Quantidade"].HeaderText = "Quantidade";
            oGrade.Columns["Quantidade"].Width = 60;
            //oGrade.Columns["Quantidade"].DefaultCellStyle.Format = "#.##0";

            oGrade.Columns["ModeloCaminhao"].HeaderText = "Cam";
            oGrade.Columns["ModeloCaminhao"].Width = 80;
            oGrade.Columns["NomeMotorista"].HeaderText = "Motorista";
            oGrade.Columns["NomeMotorista"].Width = 120;

            //if (oGrade.Name == "Grade2")
            //{
            //    oGrade.ReadOnly = false;
            //    oGrade.Columns["ModeloCaminhao"].ReadOnly = false;
            //    oGrade.Columns["NomeMotorista"].ReadOnly = false;
            //}

            oGrade.Columns["Map"].Width = 32;
            oGrade.Columns["Map"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            oGrade.Columns["Map"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            oGrade.Columns["TipoProgramacao"].HeaderText = "TP";
            oGrade.Columns["TipoProgramacao"].Width = 25;

            oGrade.Columns["Unidade"].HeaderText = "Un";
            oGrade.Columns["Unidade"].Width = 25;

            if (oGrade.Name == "Grade2")
            {
                oGrade.Columns["CidadeBairroEndereco"].HeaderText = "Cidade-Bairro-Endereço";
                oGrade.Columns["CidadeBairroEndereco"].Width = 400;
            }
        }
        private void Grade2_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex < Grade2.Rows.Count)
            {
                string sCor = Grade2.Rows[e.RowIndex].Cells["StatusCor"].Value.ToString();
                if (sCor == "65535")
                    Grade2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                else if (sCor == "65280")
                    Grade2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.GreenYellow;
                else if (sCor == "8427929")
                    Grade2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Gray;
                else if (sCor == "15000000")
                {
                    Grade2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.MediumAquamarine;
                    Grade2.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.WhiteSmoke;
                }
                else if (sCor == "8421631")
                {
                    Grade2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                    Grade2.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                }
                else if (sCor.IndexOf("Aquamarine") > -1)
                {
                    Grade2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Aquamarine;
                    Grade2.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
                }
                else if (sCor == "064444")
                {
                    Grade2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Magenta;
                    Grade2.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                }
                else
                    Grade2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                
                // bloqueio financeiro é em preto
                if (Grade2.Rows[e.RowIndex].Cells["Observacao"].Value.ToString().ToUpper().IndexOf("BLOQUEIO FINANCEIRO") >= 0)
                {
                    Grade2.Rows[e.RowIndex].Cells["Observacao"].Style.BackColor = Color.Black;
                    Grade2.Rows[e.RowIndex].Cells["Observacao"].Style.ForeColor = Color.White;
                }
            }
            /*
            if (e.RowIndex == (Grade2.Rows.Count-1))
            {
                for (int i = 0; i <= Grade2.Columns.Count - 1; i++)
                {
                    if (i <= 5 )
                        Grade2.Columns[i].Frozen = true;
                    else
                        Grade2.Columns[i].Frozen = false;
                }
            }
            */
        }
        private Color RetornaCorPreEstabelecidas(string pCodigoCor)
        {
            Color _cor;
            if (pCodigoCor == "65535")
                _cor = Color.Yellow;
            else if (pCodigoCor == "65280")
                _cor = Color.GreenYellow;
            else if (pCodigoCor == "8427929")
                _cor = Color.Gray;
            else if (pCodigoCor == "8421631")
                _cor = Color.Red;
            else if (pCodigoCor == "15000000")
            {
                _cor = Color.Aquamarine;
            }
            else if (pCodigoCor == "064444")
            {
                _cor = Color.Magenta;
            }
            else
                _cor = Color.White;
            return _cor;
        }

        private void btnExecutado_Click(object sender, EventArgs e)
        {
            frmRelatorioExecutados frm = new frmRelatorioExecutados();
            frm.DataEmissao = _dia + "/" + _mes + "/" + _ano;
            frm.Programados = Grade2.Rows.Count - 1;
            frm.dtExecutados = _dtParaImprimirExecutados;
            frm.dtProgramados = _dtParaImprimirProgramados;
            frm.Show();
        }
        private void btnProgramado_Click(object sender, EventArgs e)
        {
            frmRelatorioProgramados frm = new frmRelatorioProgramados();
            if (_ano != "" && _mes != "" && _dia != "")
            {
                frm.DataEmissao = _dia + "/" + _mes + "/" + _ano;
                try
                {
                    // programação futura
                    if (Convert.ToDateTime(_dia + "/" + _mes + "/" + _ano) > Convert.ToDateTime(DataProgAberta.Text))
                    {
                        string _dtAbertaBanco = geral.Left(oProgramacaoFechadaDados.DataUltimaProgAberta(), 10);
                        if (DataProgAberta.Text != _dtAbertaBanco)
                        {
                            MessageBox.Show("A programação aberta está diferente da atual. \nReinicie a Programação Diária de Serviços. ");
                        }
                        else
                        {
                            Grade2.DataSource = "";
                            Grade2.Refresh();
                            MontaProgramacaoFutura();
                        }
                    }
                    else
                        PreencheGrades(_dia + "/" + _mes + "/" + _ano);
                }
                finally
                {
                    frm.dtProgramados = _dtParaImprimirProgramados;
                    frm.Show();
                }
            }
            else
                MessageBox.Show("Selecione um dia!", "SILC", MessageBoxButtons.OK);
        }

        private void MontaProgramacaoFutura()
        {
            clsResiduoDados oResiduosDados = new clsResiduoDados();
            DataTable _dtResiduos = new DataTable();
            _dtResiduos = oResiduosDados.PreencheDataTableResiduos("Codigo", "");

            _dtParaImprimirExecutados = oProgramacaoDados.PegaDados(100000101, 1, false); //pegar de um dia que não existe para montar somente as colunas, pois no futuro não existe serviço fechado
            bindingSource.DataSource = _dtParaImprimirExecutados;
            Grade1.DataSource = bindingSource.DataSource;
            EstiloGrades(Grade1);

            oProgramacaoDados.ExcluirProgramacaoDia(Convert.ToDateTime(_dia + "/" + _mes + "/" + _ano).ToString("yyyyMMdd"));
            _dt = oProgramacaoFechadaDados.MontaProgramacao((_dia + "/" + _mes + "/" + _ano), _bloqPor);
            string _strInClientes = "";
            foreach (DataRow _dr in _dt.Rows)
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

            string strInAnoMesDia = "";
            foreach (DataRow _dr in _dt.Rows)
            {
                if (_dr["CodigoCliente"].ToString() == "1609")
                {
                    string xteste = _dr["Hora"].ToString();
                }
                if (_dr["StatusCor"].ToString() == geral.RetornaCodigoCor("Vermelho") || _dr["StatusCor"].ToString() == geral.RetornaCodigoCor("Verde") ||
                     _dr["StatusCor"].ToString() == geral.RetornaCodigoCor("AzulClaro"))
                {
                    if (strInAnoMesDia.IndexOf(_dr["AnoMesDia"].ToString()) < 0)
                        strInAnoMesDia = strInAnoMesDia + ", " + _dr["AnoMesDia"].ToString();
                    if (strInAnoMesDia.IndexOf(Convert.ToDateTime(_dr["Data"]).ToString("yyyyMMdd")) < 0)
                        strInAnoMesDia = strInAnoMesDia + ", " + Convert.ToDateTime(_dr["Data"]).ToString("yyyyMMdd");
                }
            }
            DataTable drReprogramacoes = oReprogramacao.PegaReprogramacoes("11110101");
            if (strInAnoMesDia.Length > 0)
            {
                drReprogramacoes = oReprogramacao.PegaReprogramacoes(strInAnoMesDia.Substring(1));
            }
            // colocar endereço, bloqueiro financeiro, Dados da tabela ServicosFutura
            foreach (DataRow _dr in _dt.Rows)
            {
                if (_dr["CodigoCliente"].ToString() == "3144")
                {
                    string xteste = _dr["Hora"].ToString();
                }
                if (_dr["StatusCor"].ToString() == geral.RetornaCodigoCor("Vermelho") || _dr["StatusCor"].ToString() == geral.RetornaCodigoCor("AzulClaro") || 
                    _dr["StatusCor"].ToString() == geral.RetornaCodigoCor("Cinza"))
                {
                    DataRow[] drrReprogs = drReprogramacoes.Select("Hora = '" + _dr["Hora"].ToString() + "' and CodigoCliente = " + _dr["CodigoCliente"].ToString());
                    _dr["Rp"] = drrReprogs.Length;
                }

                string _xhora = _dr["Hora"].ToString();

                DataRow[] _drEnd = _dtEnderecos.Select("Codigo = " + _dr["CodigoCliente"].ToString());
                if (_drEnd.Length > 0)
                    _dr["CidadeBairroEndereco"] = _drEnd[0]["CidadeBairroEndereco"].ToString();
                DataRow[] _drBloq = _dtBloqFinanceiros.Select("CodigoCliente = " + _dr["CodigoCliente"].ToString());
                if (_drBloq.Length > 0)
                    _dr["Observacao"] = "BLOQUEIO FINANCEIRO DESDE: " + Convert.ToDateTime(_drBloq[0]["DataBloqueio"]).ToString("dd/MM/yyyy");
                DataRow[] _drServicosFutura;

                string _cons = "";
                if (_dr["CodigoResiduo"].ToString() != "" && _dr["CodigoResiduo"].ToString() != "0")
                {
                    _cons = "";
                    _cons = _cons + " CodigoCliente = " + _dr["CodigoCliente"].ToString();
                    _cons = _cons + " and CodigoResiduo = " + _dr["CodigoResiduo"].ToString();
                    _cons = _cons + " and DataProgramada = '" + _dia + "/" + _mes + "/" + _ano + "'";
                    if (_dr["Hora"].ToString().IndexOf(":") > 0 || _dr["StatusCor"].ToString() == "15000000")
                        _cons = _cons + " and Hora = '" + _dr["Hora"].ToString() + "'";
                    else
                        _cons = _cons + " and not hora like '%:%'";
                    _drServicosFutura = _dtServicosFutura.Select(_cons, "Sequencial desc");
                }
                else
                {
                    _cons = "";
                    _cons = _cons + " CodigoCliente = " + _dr["CodigoCliente"].ToString();
                    _cons = _cons + " and DescricaoResiduo = '" + _dr["ExecutarServico"].ToString() + "'";
                    _cons = _cons + " and DataProgramada = '" + _dia + "/" + _mes + "/" + _ano + "'";
                    if (_dr["Hora"].ToString().IndexOf(":") > 0 || _dr["StatusCor"].ToString() == "15000000")
                        _cons = _cons + " and Hora = '" + _dr["Hora"].ToString() + "'";
                    else
                        _cons = _cons + " and not hora like '%:%'";
                    _drServicosFutura = _dtServicosFutura.Select(_cons, "Sequencial desc");
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
                    else if (Convert.ToDateTime(_dr["DataProgramada"]) > DataProgAberta.Value && _dr["StatusCor"].ToString() == "15000000")
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
                    _dr["DestinoFinal"] = _drServicosFutura[0]["DestinoFinal"];
                    _dr["Observacao"] = _drServicosFutura[0]["Observacao"];
                    _dr["Solicitante"] = _drServicosFutura[0]["Solicitante"];
                    if (_drServicosFutura[0]["MapaMarcado"].ToString() == "1")
                        _dr["Map"] = "X";
                    _dr["HoraProgramada"] = _drServicosFutura[0]["ServicoAExecutar"];
                }
                string _cdResdiuoAdequado = _dr["CodigoResiduo"].ToString();
                if (_dr["CodigoResiduo"].ToString() == "0" || _dr["CodigoResiduo"].ToString() == "")
                    _cdResdiuoAdequado = oResiduosDados.PegaCodigoResiduo(_dr["ExecutarServico"].ToString().Trim(), true).ToString();
                DataRow[] _drPart = _dtParticularidadeContrato.Select("CodigoCliente = " + _dr["CodigoCliente"].ToString() + " and CodigoResiduo = " + _cdResdiuoAdequado);
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
            _dt.Columns.Add("idxOrdemCor");
            foreach (DataRow _drow in _dt.Rows)
            {
                string _tstHora = "";
                string _tstCodigoCliente = "";
                string _CodigoResiduo = "";
                string _tstDataProgramada = "";
                string _tstStatusCor = "";
                string _tstDtProgramada = "";
                string _tstStCor = "";

                if (_drow["CodigoCliente"].ToString() == "2811")
                {
                    _tstHora = "teste";
                }
                if (_drow["StatusCor"].ToString() != geral.RetornaCodigoCor("Branco"))
                {
                    DataTable _dtUlt = oReprogramacao.PegaUltimaReprogramacaoAntecipacao("", _drow["Hora"].ToString(), _drow["CodigoCliente"].ToString());
                    if (_dtUlt.Rows.Count > 0)
                    {
                        _tstHora = _drow["Hora"].ToString();
                        _tstCodigoCliente = _drow["CodigoCliente"].ToString();
                        _tstDataProgramada = _drow["DataProgramada"].ToString();
                        _tstStatusCor = _drow["StatusCor"].ToString();
                        _tstDtProgramada = _dtUlt.Rows[0]["DataProgramada"].ToString();
                        _tstStCor = _dtUlt.Rows[0]["StatusCor"].ToString();
                        _CodigoResiduo = _drow["CodigoResiduo"].ToString();
                        if (_CodigoResiduo == "" || _CodigoResiduo == "0")
                        {
                            _CodigoResiduo = oResiduosDados.PegaCodigoResiduo(_drow["ExecutarServico"].ToString()).ToString();
                        }
                        _drow["DataProgramada"] = _dtUlt.Rows[0]["DataProgramada"];
                        _drow["StatusCor"] = _dtUlt.Rows[0]["StatusCor"];
                    }
                    if (Convert.ToDateTime(_drow["DataProgramada"]) < DataProgAberta.Value)
                    {
                        // marca como feito - azul marine
                        _drow["StatusCor"] = Color.Aquamarine.ToString();
                    }
                    else if (oProgramacaoDados.ExisteProgramacaoFechada(_drow["Hora"].ToString(), _drow["CodigoCliente"].ToString(), _CodigoResiduo, _drow["Solicitante"].ToString()))
                    {
                        // marca como feito - azul marine
                        _drow["StatusCor"] = Color.Aquamarine.ToString();
                    }
                    string sStatusCor = _drow["StatusCor"].ToString();
                    if (sStatusCor == geral.RetornaCodigoCor("Branco"))
                        _drow["idxOrdemCor"] = 4;
                    else if (sStatusCor == "")
                        _drow["idxOrdemCor"] = 4;
                    else if (sStatusCor == Color.Aquamarine.ToString())
                        _drow["idxOrdemCor"] = 5;
                    else if (sStatusCor == "15000000")
                        _drow["idxOrdemCor"] = 1;
                    else if (sStatusCor == geral.RetornaCodigoCor("Cinza"))
                        _drow["idxOrdemCor"] = 2;
                    else if (sStatusCor == geral.RetornaCodigoCor("Vermelho"))
                        _drow["idxOrdemCor"] = 3;
                }
                else
                    _drow["idxOrdemCor"] = 4;
            }
            _dtParaImprimirProgramados = _dt;
            bindingSource.DataSource = _dt;
            Grade2.DataSource = bindingSource.DataSource;
            EstiloGrades(Grade2);
            Grade2.Sort(Grade2.Columns["idxOrdemCor"], System.ComponentModel.ListSortDirection.Ascending);

            lblLinhasGrade2.Text = (Grade1.Rows.Count).ToString() + " | " + (Grade2.Rows.Count).ToString();

            if (ckbOcultarServicoRealizado.Checked)
                OcultarServicoRealizado();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            LinhaSelecionadaGrade2 = 0;
            this.Cursor = Cursors.WaitCursor;
            if (e.Node.Level == 0)
                _ano = e.Node.Text;
            if (e.Node.Level == 1)
                _mes = e.Node.Text;
            if (e.Node.Level == 2)
                _dia = e.Node.Text;
            if (_ano != "" && _mes != "" && _dia != "" && e.Node.Level == 2)
            {
                btnEncerrarDia.Enabled = false;
                if (_dia + "/" + _mes + "/" + _ano == DataProgAberta.Text)
                {
                    btnEncerrarDia.Enabled = true;
                    PreencheGrades(_dia + "/" + _mes + "/" + _ano);
                }
                else if (_dia + "/" + _mes + "/" + _ano != DataProgAberta.Text)
                {
                    // programação futura
                    if (Convert.ToDateTime(_dia + "/" + _mes + "/" + _ano) > Convert.ToDateTime(DataProgAberta.Text))
                    {
                        string _dtAbertaBanco = geral.Left(oProgramacaoFechadaDados.DataUltimaProgAberta(), 10);
                        if (DataProgAberta.Text != _dtAbertaBanco)
                        {
                            MessageBox.Show("A programação aberta está diferente da atual. \nReinicie a Programação Diária de Serviços. ");
                        }
                        else
                            MontaProgramacaoFutura();
                    }
                    else
                        PreencheGrades(_dia + "/" + _mes + "/" + _ano);
                }
                else
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
        private void btnEncerrarDia_Click(object sender, EventArgs e)
        {
            EncerrouProgramacaoEmOutraEstacao();
            if (DateTime.Now.Date == DataProgAberta.Value)
            {
                MessageBox.Show("Você não pode encerrar para um dia futuro!", "SILC", MessageBoxButtons.OK);
                return;
            }
            DateTime DataProgramacaoAtual = Convert.ToDateTime(DataProgAberta.Text);
            bPodeEncerrar = true;
            string _mensagemServicoAberto = "";
            for (int i = 0; i < Grade2.Rows.Count; i++)
            {
                if ((Grade2.Rows[i].Cells["StatusCor"].Value.ToString() == "0" || Grade2.Rows[i].Cells["StatusCor"].Value.ToString() == "-2147483647" ||
                     Grade2.Rows[i].Cells["StatusCor"].Value.ToString() == ""  || Grade2.Rows[i].Cells["StatusCor"].Value.ToString() == "65280") ||
                     Grade2.Rows[i].Cells["StatusCor"].Value == null ||
                   (Grade2.Rows[i].Cells["StatusCor"].Value.ToString() == "8421631"  && Convert.ToDateTime(Grade2.Rows[i].Cells["DataProgramada"].Value) <= DataProgramacaoAtual) ||
                   (Grade2.Rows[i].Cells["StatusCor"].Value.ToString() == "8427929"  && Convert.ToDateTime(Grade2.Rows[i].Cells["DataProgramada"].Value) <= DataProgramacaoAtual) ||
                   (Grade2.Rows[i].Cells["StatusCor"].Value.ToString() == "15000000" && Convert.ToDateTime(Grade2.Rows[i].Cells["DataProgramada"].Value) == DataProgramacaoAtual &&
                    Grade2.Rows[i].Cells["TipoProgramacao"].Value.ToString() == "A"))
                {
                    _mensagemServicoAberto = _mensagemServicoAberto + "Data: " + geral.Left(Grade2.Rows[i].Cells["Data"].Value.ToString(), 10) + "\n";
                    _mensagemServicoAberto = _mensagemServicoAberto + "Hora: " + Grade2.Rows[i].Cells["Hora"].Value.ToString() + "\n";
                    _mensagemServicoAberto = _mensagemServicoAberto + "Cliente: (" + Grade2.Rows[i].Cells["CodigoCliente"].Value.ToString() + ")";
                    _mensagemServicoAberto = _mensagemServicoAberto + " " + Grade2.Rows[i].Cells["NomeFantasiaCliente"].Value.ToString() + " \n";
                    _mensagemServicoAberto = _mensagemServicoAberto + "Resíduo: (" + Grade2.Rows[i].Cells["CodigoResiduo"].Value.ToString() + ")";
                    _mensagemServicoAberto = _mensagemServicoAberto + " " + Grade2.Rows[i].Cells["ExecutarServico"].Value.ToString() + " \n";
                    _mensagemServicoAberto = _mensagemServicoAberto + "Data Programada: " + geral.Left(Grade2.Rows[i].Cells["DataProgramada"].Value.ToString(), 10);
                    bPodeEncerrar = false;
                    break;
                }
            }

            if (!bPodeEncerrar)
            {
                MessageBox.Show("Você não pode encerrar o dia, existe serviço em aberto! \n\n" + _mensagemServicoAberto, "SILC", MessageBoxButtons.OK);
            }
            else
            {
                DialogResult dlg1 = new DialogResult();
                dlg1 = MessageBox.Show("Favor anotar quantidade programada antes de encerrar.  \n \n Têm certeza que você deseja encerrar programação?", "Confirmação", MessageBoxButtons.YesNo);
                if (dlg1 == DialogResult.Yes)
                {
                    // fecha o dia atual 
                    oProgramacaoFechadaDados.FecharProgramacaoAberta(_bloqPor);

                    // abre próximo dia
                    DataProgAberta.Value = DataProgAberta.Value.AddDays(+1);
                    string _proximoData = DataProgAberta.Text;
                    oProgramacaoFechadaDados.AbreProgramacao(_bloqPor, DataProgAberta.Value.ToString());

                    // monta dados da programação no encerramento
                    oProgramacaoFechadaDados.MontaProgramacao(_proximoData, _bloqPor, true);

                    _dia = DataProgAberta.Value.Day.ToString("00");
                    _mes = DataProgAberta.Value.Month.ToString("00");
                    _ano = DataProgAberta.Value.Year.ToString();
                    PreencheGrades(_proximoData);
                    ApontaParaDiaNoTreeView(_proximoData);

                }
            }
        }
        private void ApontaParaDiaNoTreeView(string pProximaData)
        {

            if (DateTime.TryParse(pProximaData, out DateTime data))
            {

                _ano = data.Year.ToString();
                _mes = data.Month.ToString();
                _dia = data.Day.ToString();
                for (int i = 1; i <= 9; i++)
                {
                    TreeNode parentNode = treeView1.Nodes[i - 1];
                    if (parentNode != null)
                    {
                        if (parentNode.Text == _ano)
                        {
                            parentNode.Checked = true;
                            parentNode.Expand();
                            for (int j = 1; j <= 12; j++)
                            {
                                TreeNode parentNodeMes = parentNode.Nodes[j - 1];
                                if (parentNodeMes.Text == _mes)
                                {
                                    treeView1.SelectedNode = parentNodeMes; //seleciona o mês

                                    parentNodeMes.Checked = true;
                                    parentNodeMes.Expand();
                                    for (int k = 1; k <= 31; k++)
                                    {
                                        TreeNode parentNodeDia = parentNodeMes.Nodes[k - 1];
                                        if (parentNodeDia.Text == _dia)
                                        {
                                            parentNodeDia.Checked = true;
                                            parentNodeDia.Expand();
                                            i = 9;
                                            j = 12;
                                            btnEncerrarDia.Enabled = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private string RetornaLinhasSelecionadasGrade2(string pCampo)
        {
            string sRet = "";
            if (pCampo == "")
                pCampo = "ModeloCaminhao";
            for (int i = 0; i < Grade2.Rows.Count; i++)
            {
                if (Grade2.Rows[i].Cells[pCampo].Selected)
                {
                    sRet = sRet + i.ToString() + "|";
                }
            }
            return sRet;
        }
        private void Grade2_KeyUp(object sender, KeyEventArgs e)
        {
            string _cdResiduo = Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoResiduo"].Value.ToString();
            if (_cdResiduo == "" || _cdResiduo == "0")
            {
                clsResiduoDados oResDados = new clsResiduoDados();
                _cdResiduo = oResDados.PegaCodigoResiduo(Grade2.Rows[Grade2.CurrentRow.Index].Cells["ExecutarServico"].Value.ToString()).ToString();
            }
            bool bPreencherGrades = false;
            if (e.KeyCode.Equals(Keys.F10))
            {
                if (Grade2.Rows.Count > 0)
                {
                    if (lblBloqueadoPeloUsuario.Text != _bloqPor)
                        MessageBox.Show("Não é possível fazer alterações, pois a Programação Diária está sendo usada pelo usuário: " + oProgramacaoFechada.BloqueadaCodigoUsuario, "SILC", MessageBoxButtons.OK);
                    else if (Convert.ToDateTime( _dia + "/" + _mes + "/" + _ano) <= Convert.ToDateTime(DataProgAberta.Text ))
                        MessageBox.Show("Operação inválida! (Não é data futura).", "SILC", MessageBoxButtons.OK);
                    else if (oProgramacaoDados.ExisteProgramacaoFechada(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Hora"].Value.ToString(),
                                                                        Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCliente"].Value.ToString(),
                                                                        _cdResiduo,
                                                                        Grade2.Rows[Grade2.CurrentRow.Index].Cells["Solicitante"].Value.ToString()))
                    {
                        MessageBox.Show("Não é possível antecipar uma programação que já foi fechada! Inclusive o mesmo Solicitante.", "SILC", MessageBoxButtons.OK);
                    }
                    else
                    {
                        frmAntecipacao o_frmAntecipacao = new frmAntecipacao();
                        o_frmAntecipacao.lblNrSequencial.Text = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString();
                        o_frmAntecipacao.dtpDataReprogramada.Text = Convert.ToDateTime(Grade2.Rows[Grade2.CurrentRow.Index].Cells["DataProgramada"].Value).ToString("yyyy-MM-dd");
                        o_frmAntecipacao.cboTabMotivosOBS.Text = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Observacao"].Value.ToString();
                        o_frmAntecipacao.pDataProgramacaoAberta = DataProgAberta.Text;                        
                        o_frmAntecipacao.ShowDialog();
                        if (o_frmAntecipacao.bSalvar)
                        {
                            for (int i = 0; i < Grade2.Columns.Count; i++)
                            {
                                Grade2.Rows[Grade2.CurrentRow.Index].Cells[i].Style.BackColor = Color.Aquamarine;
                            }
                            Grade2.Rows[Grade2.CurrentRow.Index].Cells["DataProgramada"].Value = Convert.ToDateTime(o_frmAntecipacao.dtpDataReprogramada.Text).ToString("yyyy-MM-dd");
                            Grade2.Rows[Grade2.CurrentRow.Index].Cells["StatusCor"].Value = geral.RetornaCodigoCor("AzulClaro");
                            Grade2.Rows[Grade2.CurrentRow.Index].Cells["Observacao"].Value = o_frmAntecipacao.cboTabMotivosOBS.Text;

                            clsProgramacaoDiariaServicos oProgramacaoDiaria = new clsProgramacaoDiariaServicos();
                            clsProgramacaoDados oProgramacaoDados = new clsProgramacaoDados();
                            if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString() != "")
                                oProgramacaoDiaria.Sequencial = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value);
                            oProgramacaoDiaria.AnoMesDia = Convert.ToInt32(_ano + _mes + _dia);
                            oProgramacaoDiaria.Solicitante = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Solicitante"].Value.ToString();
                            oProgramacaoDiaria.CodigoCliente = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCliente"].Value);
                            if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoResiduo"] != null)
                                oProgramacaoDiaria.CodigoResiduo = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoResiduo"].Value);
                            if (oProgramacaoDiaria.CodigoResiduo == 0)
                            {
                                clsResiduoDados oResiduoDados = new clsResiduoDados();
                                oProgramacaoDiaria.CodigoResiduo = Convert.ToInt32(oResiduoDados.PegaCodigoResiduo(Grade2.Rows[Grade2.CurrentRow.Index].Cells["ExecutarServico"].Value.ToString()));
                            }
                            oProgramacaoDiaria.ExecutarServico = Grade2.Rows[Grade2.CurrentRow.Index].Cells["ExecutarServico"].Value.ToString();
                            if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["Quantidade"].Value.ToString() != "")
                                oProgramacaoDiaria.Quantidade = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Quantidade"].Value);
                            oProgramacaoDiaria.Unidade = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Unidade"].Value.ToString();
                            if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCaminhao"].Value.ToString() != "")
                                oProgramacaoDiaria.CodigoCaminhao = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCaminhao"].Value);
                            if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoMotorista"].Value.ToString() != "")
                                oProgramacaoDiaria.CodigoMotorista = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoMotorista"].Value);
                            oProgramacaoDiaria.Observacao = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Observacao"].Value.ToString();
                            oProgramacaoDiaria.StatusCor = geral.RetornaCodigoCor("AzulClaro");
                            oProgramacaoDiaria.DataProgramada = Grade2.Rows[Grade2.CurrentRow.Index].Cells["DataProgramada"].Value.ToString();
                            oProgramacaoDiaria.Data = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Data"].Value.ToString();
                            oProgramacaoDiaria.Hora = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Hora"].Value.ToString();                            
                            SalvarLog("Antecipou", "Programação de Serviços", Grade2);
                            if (oProgramacaoDados.ExisteProgramacao(Convert.ToInt32(Convert.ToDateTime(DataProgAberta.Text).ToString("yyyyMMdd")), oProgramacaoDiaria.Hora, oProgramacaoDiaria.CodigoCliente, oProgramacaoDiaria.DataProgramada) == "Incluir")
                            {
                                oProgramacaoDiaria.AnoMesDia = Convert.ToInt32(Convert.ToDateTime(DataProgAberta.Text).ToString("yyyyMMdd"));
                                oProgramacaoDiaria.Quadro = 2;
                                oProgramacaoDados.Inserir(oProgramacaoDiaria);
                                oProgramacaoDiaria.AnoMesDia = Convert.ToInt32(_ano + _mes + _dia);
                            }
                            else 
                            {
                                oProgramacaoDados.SalvarDataReprogramada(oProgramacaoDiaria.Sequencial.ToString(), oProgramacaoDiaria.DataProgramada);
                                oProgramacaoDados.SalvarStatusCor(oProgramacaoDiaria.Sequencial.ToString(), geral.RetornaCodigoCor("AzulClaro"));
                            }
                            clsReprogramacaoDados oReprogDados = new clsReprogramacaoDados();
                            if (oReprogDados.ExisteReprogramacao(oProgramacaoDiaria.AnoMesDia, oProgramacaoDiaria.Hora, oProgramacaoDiaria.CodigoCliente, oProgramacaoDiaria.DataProgramada) == "Incluir")
                            {
                                oProgramacaoDados.SalvarDataReprogramada(oProgramacaoDiaria.Sequencial.ToString(), oProgramacaoDiaria.DataProgramada);
                                SalvarReprogramacaoManualFuturaAntecipacao(oProgramacaoDiaria, geral.RetornaCodigoCor("AzulClaro"));
                            }
                            SalvarServicoFutura(Grade2.CurrentRow.Index, Grade2, Grade2.Rows[Grade2.CurrentRow.Index].Cells["NumeroMTRe"].Value.ToString());
                            if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString() != "")
                                oStatusCorProg.SequencialProgramacao = (int)Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value;
                            //oStatusCorProg.StatusCor = geral.RetornaCodigoCor("Vermelho");
                            //oStatusCorProgDados.Inserir(oStatusCorProg);
                        }
                    }
                }
            }
            else if (e.KeyCode.Equals(Keys.F4))
            {
                if (Grade2.Rows.Count > 0)
                {
                    if (oProgramacaoDados.ExisteProgramacaoFechada(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Hora"].Value.ToString(), 
                                                                   Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCliente"].Value.ToString(),
                                                                   Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoResiduo"].Value.ToString(),
                                                                   Grade2.Rows[Grade2.CurrentRow.Index].Cells["Solicitante"].Value.ToString()))
                    {
                        MessageBox.Show("Não é possível fechar uma programação que já foi fechada! Inclusive o mesmo Solicitante.", "SILC", MessageBoxButtons.OK);
                    }
                    else if (_dia + "/" + _mes + "/" + _ano != DataProgAberta.Text)
                        MessageBox.Show("Está não é a programação aberta!", "SILC", MessageBoxButtons.OK);
                    else if (lblBloqueadoPeloUsuario.Text != _bloqPor)
                        MessageBox.Show("Não é possível fazer alterações, pois a Programação Diária está sendo usada pelo usuário: " + oProgramacaoFechada.BloqueadaCodigoUsuario, "SILC", MessageBoxButtons.OK);
                    else if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["ModeloCaminhao"].Value.ToString() == "")
                        MessageBox.Show("Caminhão inválido!", "SILC", MessageBoxButtons.OK);
                    else if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["NomeMotorista"].Value.ToString() == "")
                        MessageBox.Show("Motorista inválido!", "SILC", MessageBoxButtons.OK);
                    else
                    {
                        DialogResult dlg1 = new DialogResult();
                        dlg1 = MessageBox.Show("Confirma serviço realizado?", "Serviço realizado", MessageBoxButtons.YesNo);
                        if (dlg1 == DialogResult.Yes)
                        {
                            SalvarLog("Finalizou serviço", "Programação de Serviços", Grade2);
                            // Enviar para Grade 1
                            if (oProgramacaoDados.SalvarNoQuadro1(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString(), Grade1.Rows.Count, DataProgAberta.Text))
                            {
                                // retirar do quadro 2
                                foreach (DataRow dr in _dtParaImprimirProgramados.Rows)
                                {
                                    if (dr["Sequencial"].ToString() == Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString() &&
                                        dr["Hora"].ToString() == Grade2.Rows[Grade2.CurrentRow.Index].Cells["Hora"].Value.ToString())
                                    {

                                        // quando o serviço for no futuro e fechado antes, mudar a cor para serviço antecipado, azul claro
                                        if (_dtParaImprimirProgramados.Rows[Grade2.CurrentRow.Index]["StatusCor"].ToString() == geral.RetornaCodigoCor("Cinza"))
                                        {
                                            clsReprogramacaoDados _oReprogDados = new clsReprogramacaoDados();
                                            _oReprogDados.SalvarDataProgramacaoAbertaCorDeAntecipacao(dr["Hora"].ToString(), dr["CodigoCliente"].ToString(), dr["Sequencial"].ToString());
                                        }

                                        // insere no quadro 1
                                        _dtParaImprimirProgramados.Rows[Grade2.CurrentRow.Index]["StatusCor"] = geral.RetornaCodigoCor("Azul");
                                        _dtParaImprimirProgramados.Rows[Grade2.CurrentRow.Index]["DataProgramada"] = DataProgAberta.Text;

                                        DataRow dr2 = _dtParaImprimirExecutados.NewRow();
                                        for (int i2 = 0; i2 <= _dtParaImprimirExecutados.Columns.Count - 1; i2++)
                                            dr2[i2] = dr[i2];

                                        _dtParaImprimirExecutados.Rows.Add(dr2);

                                        _dtParaImprimirProgramados.Rows.Remove(dr);
                                        break;
                                    }
                                }
                                Grade1.Refresh();
                                Grade2.Refresh();
                                lblLinhasGrade2.Text = (Grade1.Rows.Count).ToString() + " | " + (Grade2.Rows.Count).ToString();
                            }
                        }
                    }
                }
            }
            else if (e.KeyCode.Equals(Keys.F9))
            {
                if (Grade2.Rows.Count > 0)
                {
                    if (_dia + "/" + _mes + "/" + _ano != DataProgAberta.Text)
                    {
                        MessageBox.Show("Está não é programação aberta!", "SILC", MessageBoxButtons.OK);
                    }
                    else
                    {
                        if (lblBloqueadoPeloUsuario.Text != _bloqPor)
                            MessageBox.Show("Não é possível fazer alterações, pois a Programação Diária está sendo usada pelo usuário: " + oProgramacaoFechada.BloqueadaCodigoUsuario, "SILC", MessageBoxButtons.OK);
                        else
                        {
                            if (oProgramacaoDados.ExisteProgramacaoFechada(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Hora"].Value.ToString(),
                                                                           Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCliente"].Value.ToString(),
                                                                           Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoResiduo"].Value.ToString(),
                                                                           Grade2.Rows[Grade2.CurrentRow.Index].Cells["Solicitante"].Value.ToString()))
                            {
                                MessageBox.Show("Não é possível executar uma programação que já foi fechada! Inclusive o mesmo Solicitante.", "SILC", MessageBoxButtons.OK);
                            }
                            else if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["ModeloCaminhao"].Value.ToString() == "")
                                MessageBox.Show("Caminhão inválido!", "SILC", MessageBoxButtons.OK);
                            else if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["NomeMotorista"].Value.ToString() == "")
                                MessageBox.Show("Motorista inválido!", "SILC", MessageBoxButtons.OK);
                            else
                            {
                                for (int i = 0; i < Grade2.Columns.Count; i++)
                                {
                                    Grade2.Rows[Grade2.CurrentRow.Index].Cells[i].Style.BackColor = Color.GreenYellow;
                                    Grade2.Rows[Grade2.CurrentRow.Index].Cells[i].Style.ForeColor = Color.Black;
                                }
                                Grade2.Rows[Grade2.CurrentRow.Index].Cells["StatusCor"].Value = "65280";
                                oProgramacaoDados.SalvarExecutando(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString());
                                oStatusCorProg.SequencialProgramacao = (int)Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value;
                                oStatusCorProg.StatusCor = "65280";
                                oStatusCorProgDados.Inserir(oStatusCorProg);
                            }
                        }
                    }
                }
            }
            else if (e.KeyCode.Equals(Keys.F2))
            {
                /*
                   -> Quando a Data da Programação Aberta for <= (menor ou igual) a atual utilizada/mostrada 
                   -  pegar dados da tabela: ProgramacaoDiariaServicos e quando for ">" maior pegar dados da tabela: ServicosFutura
                */
                if (Grade2.Rows.Count > 0)
                {

                    if (lblBloqueadoPeloUsuario.Text != _bloqPor)
                        MessageBox.Show("Não é possível fazer alterações, pois a Programação Diária está sendo usada pelo usuário: " + oProgramacaoFechada.BloqueadaCodigoUsuario, "SILC", MessageBoxButtons.OK);
                    else
                    {
                        if (Grade2.Columns[Grade2.CurrentCell.ColumnIndex].HeaderText == "Solicitante")
                        {
                            int _cdCliente = 0;
                            int _icdResiduo = 0;

                            frmSolicitante o_frmSolicitante = new frmSolicitante();
                            if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCliente"].Value.ToString() != "")
                                _cdCliente = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCliente"].Value.ToString());
                            if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoResiduo"].Value.ToString() != "")
                            {
                                _icdResiduo = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoResiduo"].Value.ToString());
                            }
                            if (_icdResiduo == 0)
                            {
                                clsResiduoDados oResDados = new clsResiduoDados();
                                _icdResiduo = oResDados.PegaCodigoResiduo(Grade2.Rows[Grade2.CurrentRow.Index].Cells["ExecutarServico"].Value.ToString());
                            }
                            if (_cdCliente > 0)
                            {
                                DateTime dataMostrada = Convert.ToDateTime(_dia + "/" + _mes + "/" + _ano);
                                if (dataMostrada <= DataProgAberta.Value)
                                {
                                    SILCNegocios.clsProgramacaoDiariaServicos oPrg = new clsProgramacaoDiariaServicos();
                                    oProgramacaoDados.PegaDados(oPrg, Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString()), 0, 0);
                                    o_frmSolicitante.txtSolicitante.Text = oPrg.Solicitante;
                                    if (oPrg.Solicitante == "" || oPrg.Solicitante == null)
                                    {
                                        if (_icdResiduo > 0)
                                        {
                                            oServicosFutura.Solicitante = oServicosFuturaDados.PegaSolicitante(_cdCliente, _icdResiduo, "", "", Grade2.Rows[Grade2.CurrentRow.Index].Cells["hora"].Value.ToString());
                                            o_frmSolicitante.txtSolicitante.Text = oServicosFutura.Solicitante;
                                        }
                                    }
                                }
                                else if (dataMostrada > DataProgAberta.Value)
                                {
                                    if (_icdResiduo > 0)
                                    {
                                        if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["Hora"].Value.ToString().IndexOf(":") > -1)
                                            oServicosFutura.Solicitante = oServicosFuturaDados.PegaSolicitante(_cdCliente, _icdResiduo, Grade2.Rows[Grade2.CurrentRow.Index].Cells["DataProgramada"].Value.ToString(), "", 
                                                                                                               Grade2.Rows[Grade2.CurrentRow.Index].Cells["Hora"].Value.ToString());
                                        else
                                            oServicosFutura.Solicitante = oServicosFuturaDados.PegaSolicitante(_cdCliente, _icdResiduo, Grade2.Rows[Grade2.CurrentRow.Index].Cells["DataProgramada"].Value.ToString(), "", "");
                                        o_frmSolicitante.txtSolicitante.Text = oServicosFutura.Solicitante;
                                    }
                                }
                                o_frmSolicitante.ShowDialog();
                                if (o_frmSolicitante.bSalvar)
                                {
                                    Grade2.Rows[Grade2.CurrentRow.Index].Cells["Solicitante"].Value = o_frmSolicitante.txtSolicitante.Text;
                                    SalvarLog("Alterou solicitante", "Programação de Serviços", Grade2);
                                    oProgramacaoDados.SalvarSolicitante(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString(), o_frmSolicitante.txtSolicitante.Text);
                                    SalvarServicoFutura(Grade2.CurrentRow.Index, Grade2, Grade2.Rows[Grade2.CurrentRow.Index].Cells["NumeroMTRe"].Value.ToString());
                                }
                            }
                        }
                        if (Grade2.Columns[Grade2.CurrentCell.ColumnIndex].HeaderText == "Cliente")
                        {
                            // não pode mais trocar o cliente - tem que cancelar com F7 e inserir uma nova programação

                            //frmProcura frmListaClientes = new frmProcura("CLIENTEPROGRAMACAO");
                            //frmListaClientes.ShowDialog();
                            //if (geral.CodigoCliente > 0 && geral.NomeClienteFantasia != "" && Grade2.Rows[Grade2.CurrentRow.Index].Cells["TipoProgramacao"].Value.ToString() == "M")
                            //{
                            //    Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCliente"].Value = geral.CodigoCliente;
                            //    Grade2.Rows[Grade2.CurrentRow.Index].Cells["NomeFantasiaCliente"].Value = geral.NomeClienteFantasia;
                            //    SalvarLog("Alterou Cliente", "Programação de Serviços", Grade2);
                            //    oProgramacaoDados.SalvarCliente(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString(), geral.CodigoCliente);
                            //}
                        }
                        if (Grade2.Columns[Grade2.CurrentCell.ColumnIndex].HeaderText == "Cam") // caminhão
                        {
                            frmProcura frmLista = new frmProcura("CAMINHAOPROGRAMACAO");
                            frmLista.ShowDialog();
                            if (geral.CodigoCaminhao > 0)
                            {
                                // verifica se há mais de uma linha selecionada
                                string[] sColSelected = RetornaLinhasSelecionadasGrade2("ModeloCaminhao").Split("|"[0]);
                                if (sColSelected.Length <= 2)
                                {
                                    Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCaminhao"].Value = geral.CodigoCaminhao;
                                    oCaminhao = oCaminhaoDados.PegaDados(oCaminhao, geral.CodigoCaminhao);
                                    if (oCaminhao.Modelo != "")
                                    {
                                        Grade2.Rows[Grade2.CurrentRow.Index].Cells["ModeloCaminhao"].Value = oCaminhao.Modelo;
                                        SalvarLog("Alterou caminhão", "Programação de Serviços", Grade2);
                                        oProgramacaoDados.SalvarCaminhao(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString(), geral.CodigoCaminhao);
                                        SalvarServicoFutura(Grade2.CurrentRow.Index, Grade2, Grade2.Rows[Grade2.CurrentRow.Index].Cells["NumeroMTRe"].Value.ToString());
                                    }
                                }
                                else if (sColSelected.Length > 2)
                                {
                                    oCaminhao = oCaminhaoDados.PegaDados(oCaminhao, geral.CodigoCaminhao);
                                    foreach (string iSel in sColSelected)
                                    {
                                        if (iSel != "")
                                        {
                                            int iLn = Convert.ToInt32(iSel);
                                            if (Grade2.Rows[iLn].Cells["ModeloCaminhao"].Value.ToString() == "" && oCaminhao.Modelo != "")
                                            {
                                                Grade2.Rows[iLn].Cells["CodigoCaminhao"].Value = geral.CodigoCaminhao;
                                                Grade2.Rows[iLn].Cells["ModeloCaminhao"].Value = oCaminhao.Modelo;
                                                SalvarLog("Alterou caminhão", "Programação de Serviços", Grade2);
                                                oProgramacaoDados.SalvarCaminhao(Grade2.Rows[iLn].Cells["Sequencial"].Value.ToString(), geral.CodigoCaminhao);
                                                SalvarServicoFutura(iLn, Grade2, Grade2.Rows[Grade2.CurrentRow.Index].Cells["NumeroMTRe"].Value.ToString());
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (Grade2.Columns[Grade2.CurrentCell.ColumnIndex].HeaderText == "Motorista") // Motorista
                        {
                            frmProcura frmLista = new frmProcura("MOTORISTAPROGRAMACAO");
                            frmLista.ShowDialog();
                            string[] sColSelected = RetornaLinhasSelecionadasGrade2("NomeMotorista").Split("|"[0]);
                            if (sColSelected.Length <= 2)
                            {
                                if (geral.CodigoMotorista > 0)
                                {
                                    Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoMotorista"].Value = geral.CodigoMotorista;
                                    oMotorista = oMotoristaDados.PegaDados(oMotorista, geral.CodigoMotorista);
                                    if (oMotorista.Nome != "")
                                    {
                                        Grade2.Rows[Grade2.CurrentRow.Index].Cells["NomeMotorista"].Value = oMotorista.Nome;
                                        oProgramacaoDados.SalvarMotorista(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString(), geral.CodigoMotorista, oMotorista.Nome);
                                        SalvarLog("Alterou motorista", "Programação de Serviços", Grade2);
                                        SalvarServicoFutura(Grade2.CurrentRow.Index, Grade2, Grade2.Rows[Grade2.CurrentRow.Index].Cells["NumeroMTRe"].Value.ToString());
                                    }
                                }
                            }
                            else if (sColSelected.Length > 2)
                            {
                                if (geral.CodigoMotorista > 0)
                                {                                    
                                    oMotorista = oMotoristaDados.PegaDados(oMotorista, geral.CodigoMotorista);
                                    foreach (string sLn in sColSelected)
                                    {
                                        if (oMotorista.Nome != "")
                                        {
                                            if (sLn != "")
                                            {
                                                int iLn = Convert.ToInt32(sLn);
                                                if (Grade2.Rows[iLn].Cells["NomeMotorista"].Value.ToString() == "")
                                                {
                                                    Grade2.Rows[iLn].Cells["CodigoMotorista"].Value = geral.CodigoMotorista;
                                                    Grade2.Rows[iLn].Cells["NomeMotorista"].Value = oMotorista.Nome;
                                                    SalvarLog("Alterou motorista", "Programação de Serviços", Grade2);
                                                    oProgramacaoDados.SalvarMotorista(Grade2.Rows[iLn].Cells["Sequencial"].Value.ToString(), geral.CodigoMotorista, oMotorista.Nome);
                                                    SalvarServicoFutura(iLn, Grade2, Grade2.Rows[Grade2.CurrentRow.Index].Cells["NumeroMTRe"].Value.ToString());
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (Grade2.Columns[Grade2.CurrentCell.ColumnIndex].HeaderText == "Serviço a Executar") // Serviço a executar
                        {
                            frmServicoExecutar o_frmServicoAExecutar = new frmServicoExecutar();
                            o_frmServicoAExecutar.oProgDiaria.AnoMesDia = Convert.ToInt32(_ano + _mes + _dia);
                            o_frmServicoAExecutar.oProgDiaria.Solicitante = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Solicitante"].Value.ToString();
                            o_frmServicoAExecutar.oProgDiaria.CodigoCliente = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCliente"].Value);
                            if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoResiduo"] != null)
                                o_frmServicoAExecutar.oProgDiaria.CodigoResiduo = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoResiduo"].Value);
                            o_frmServicoAExecutar.oProgDiaria.ExecutarServico = Grade2.Rows[Grade2.CurrentRow.Index].Cells["ExecutarServico"].Value.ToString();
                            if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["Quantidade"].Value.ToString() != "")
                                o_frmServicoAExecutar.oProgDiaria.Quantidade = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Quantidade"].Value.ToString().Replace(",00", ""));
                            o_frmServicoAExecutar.oProgDiaria.Unidade = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Unidade"].Value.ToString();
                            if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCaminhao"].Value.ToString() != "")
                                o_frmServicoAExecutar.oProgDiaria.CodigoCaminhao = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCaminhao"].Value);
                            if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoMotorista"].Value.ToString() != "")
                                o_frmServicoAExecutar.oProgDiaria.CodigoMotorista = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoMotorista"].Value);
                            o_frmServicoAExecutar.oProgDiaria.Observacao = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Observacao"].Value.ToString();
                            o_frmServicoAExecutar.oProgDiaria.StatusCor = geral.RetornaCodigoCor("Cinza");
                            o_frmServicoAExecutar.oProgDiaria.DataProgramada = Grade2.Rows[Grade2.CurrentRow.Index].Cells["DataProgramada"].Value.ToString();
                            o_frmServicoAExecutar.oProgDiaria.Data = DataProgAberta.Text; //era esse antes. Grade2.Rows[Grade2.CurrentRow.Index].Cells["Data"].Value.ToString();
                            o_frmServicoAExecutar.oProgDiaria.Hora = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Hora"].Value.ToString();
                            o_frmServicoAExecutar.pDataProgramacaoAberta = DataProgAberta.Text;


                            o_frmServicoAExecutar.pServicoExecutar = Grade2.Rows[Grade2.CurrentRow.Index].Cells["HoraProgramada"].Value.ToString();

                            o_frmServicoAExecutar.ShowDialog();
                            if (o_frmServicoAExecutar.bSalvar)
                            {
                                Grade2.Rows[Grade2.CurrentRow.Index].Cells["HoraProgramada"].Value = o_frmServicoAExecutar.cboServicoExecutar.Text;
                                SalvarLog("Alterou serviço a executar", "Programação de Serviços", Grade2);
                                oProgramacaoDados.SalvarServicoAExecutar(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString(),
                                                                            o_frmServicoAExecutar.cboServicoExecutar.Text);
                                SalvarServicoFutura(Grade2.CurrentRow.Index, Grade2, Grade2.Rows[Grade2.CurrentRow.Index].Cells["NumeroMTRe"].Value.ToString());
                                if (o_frmServicoAExecutar.bIncluiuServico)
                                    bPreencherGrades = true;
                            }
                        }
                        if (Grade2.Columns[Grade2.CurrentCell.ColumnIndex].HeaderText == "Observacao" ||
                            Grade2.Columns[Grade2.CurrentCell.ColumnIndex].HeaderText == "Observação") // Observação
                        {
                            int _cdCliente = 0;
                            int _icdResiduo = 0;
                            if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCliente"].Value.ToString() != "")
                                _cdCliente = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCliente"].Value.ToString());

                            if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoResiduo"].Value.ToString() != "")
                            {
                                _icdResiduo = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoResiduo"].Value.ToString());
                            }
                            if (_icdResiduo == 0)
                            {
                                clsResiduoDados oResDados = new clsResiduoDados();
                                _icdResiduo = oResDados.PegaCodigoResiduo(Grade2.Rows[Grade2.CurrentRow.Index].Cells["ExecutarServico"].Value.ToString());
                            }
                            if (_cdCliente > 0)
                            {
                                DateTime dataMostrada = Convert.ToDateTime(_dia + "/" + _mes + "/" + _ano);
                                frmObservacao o_frmObservacao = new frmObservacao();
                                if (dataMostrada <= DataProgAberta.Value)
                                {
                                    SILCNegocios.clsProgramacaoDiariaServicos oPrg = new clsProgramacaoDiariaServicos();
                                    oProgramacaoDados.PegaDados(oPrg, Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString()), 0, 0);
                                    if (oPrg.Observacao != Grade2.Rows[Grade2.CurrentRow.Index].Cells["Observacao"].Value.ToString())
                                    {
                                        oServicosFutura.Observacao = oServicosFuturaDados.PegaObservacao(_cdCliente, _icdResiduo, "", "", Grade2.Rows[Grade2.CurrentRow.Index].Cells["hora"].Value.ToString());
                                        o_frmObservacao.cboTabMotivosOBS.Text = oServicosFutura.Observacao.ToUpper();
                                    }
                                    else                                    
                                        o_frmObservacao.cboTabMotivosOBS.Text = oPrg.Observacao;
                                }
                                else if (dataMostrada > DataProgAberta.Value)
                                {
                                    if (_icdResiduo > 0)
                                    {
                                        //oServicosFutura.Observacao = oServicosFuturaDados.PegaObservacao(_cdCliente, _icdResiduo, Grade2.Rows[Grade2.CurrentRow.Index].Cells["DataProgramada"].Value.ToString(), "", "");
                                        if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["Hora"].Value.ToString().IndexOf(":") > -1)
                                            oServicosFutura.Observacao = oServicosFuturaDados.PegaObservacao(_cdCliente, _icdResiduo, Grade2.Rows[Grade2.CurrentRow.Index].Cells["DataProgramada"].Value.ToString(), "",
                                                                                                             Grade2.Rows[Grade2.CurrentRow.Index].Cells["Hora"].Value.ToString());
                                        else
                                            oServicosFutura.Observacao = oServicosFuturaDados.PegaObservacao(_cdCliente, _icdResiduo, Grade2.Rows[Grade2.CurrentRow.Index].Cells["DataProgramada"].Value.ToString(), "", "");
                                        o_frmObservacao.cboTabMotivosOBS.Text = oServicosFutura.Observacao;
                                    }
                                }
                                o_frmObservacao.ShowDialog();
                                if (o_frmObservacao.bSalvar)
                                {
                                    Grade2.Rows[Grade2.CurrentRow.Index].Cells["Observacao"].Value = o_frmObservacao.cboTabMotivosOBS.Text;
                                    SalvarLog("Alterou observação", "Programação de Serviços", Grade2);
                                    oProgramacaoDados.SalvarObservacao(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString(),
                                                                        o_frmObservacao.cboTabMotivosOBS.Text);

                                    SalvarServicoFutura(Grade2.CurrentRow.Index, Grade2, Grade2.Rows[Grade2.CurrentRow.Index].Cells["NumeroMTRe"].Value.ToString());

                                }
                            }
                        }
                        if (Grade2.Columns[Grade2.CurrentCell.ColumnIndex].HeaderText == "DestinoFinal" ||
                            Grade2.Columns[Grade2.CurrentCell.ColumnIndex].HeaderText == "DestinoFinal") // Destino Final
                        {
                            int _cdCliente = 0;
                            int _icdResiduo = 0;
                            if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCliente"].Value.ToString() != "")
                                _cdCliente = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCliente"].Value.ToString());

                            if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoResiduo"].Value.ToString() != "")
                            {
                                _icdResiduo = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoResiduo"].Value.ToString());
                            }
                            if (_icdResiduo == 0)
                            {
                                clsResiduoDados oResDados = new clsResiduoDados();
                                _icdResiduo = oResDados.PegaCodigoResiduo(Grade2.Rows[Grade2.CurrentRow.Index].Cells["ExecutarServico"].Value.ToString());
                            }

                            frmDestinoFinal o_frmDestinoFinal = new frmDestinoFinal();
                            if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoResiduo"].Value.ToString() != "")
                            {
                                o_frmDestinoFinal.residuo1.txtCodigo.Text = _icdResiduo.ToString();
                            }
                            oServicosFutura.DestinoFinal = oServicosFuturaDados.PegaDestinoFinal(_cdCliente, _icdResiduo, "", "", Grade2.Rows[Grade2.CurrentRow.Index].Cells["hora"].Value.ToString());

                            if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["DestinoFinal"].Value.ToString() != oServicosFutura.DestinoFinal)
                                o_frmDestinoFinal.cboDestinoFinal.Text = Grade2.Rows[Grade2.CurrentRow.Index].Cells["DestinoFinal"].Value.ToString();
                            else
                                o_frmDestinoFinal.cboDestinoFinal.Text = oServicosFutura.DestinoFinal;

                            o_frmDestinoFinal.ShowDialog();
                            if (o_frmDestinoFinal.bSalvar)
                            {
                                Grade2.Rows[Grade2.CurrentRow.Index].Cells["DestinoFinal"].Value = o_frmDestinoFinal.cboDestinoFinal.Text;
                                SalvarLog("Alterou destino final", "Programação de Serviços", Grade2);
                                oProgramacaoDados.SalvarDestinoFinal(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString(),
                                                                     o_frmDestinoFinal.cboDestinoFinal.Text);
                                SalvarServicoFutura(Grade2.CurrentRow.Index, Grade2, Grade2.Rows[Grade2.CurrentRow.Index].Cells["NumeroMTRe"].Value.ToString());
                            }
                        }
                    }
                }
            }
            else if (e.KeyCode.Equals(Keys.Delete))
            {
                if (lblBloqueadoPeloUsuario.Text != _bloqPor)
                    MessageBox.Show("Não é possível fazer alterações, pois a Programação Diária está sendo usada pelo usuário: " + oProgramacaoFechada.BloqueadaCodigoUsuario);
                else
                {
                    if (Grade2.Columns[Grade2.CurrentCell.ColumnIndex].HeaderText == "Cam") // caminhão
                    {
                        Grade2.Rows[Grade2.CurrentRow.Index].Cells["ModeloCaminhao"].Value = "";
                        oProgramacaoDados.SalvarCaminhao(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString(), 0);
                        SalvarLog("Apagou caminhão", "Programação de Serviços", Grade2);
                        oServicosFuturaDados.SalvarCaminhao(Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCliente"].Value.ToString()),
                                                            Grade2.Rows[Grade2.CurrentRow.Index].Cells["DataProgramada"].Value.ToString(),
                                                            Grade2.Rows[Grade2.CurrentRow.Index].Cells["Hora"].Value.ToString(),
                                                            Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoResiduo"].Value.ToString()),
                                                            0);
                    }
                    if (Grade2.Columns[Grade2.CurrentCell.ColumnIndex].HeaderText == "Motorista") // Motorista
                    {
                        Grade2.Rows[Grade2.CurrentRow.Index].Cells["NomeMotorista"].Value = "";
                        oProgramacaoDados.SalvarMotorista(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString(), 0, "");
                        SalvarLog("Apagou motorista", "Programação de Serviços", Grade2);
                        oServicosFuturaDados.SalvarMotorista(Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCliente"].Value.ToString()),
                                                            Grade2.Rows[Grade2.CurrentRow.Index].Cells["DataProgramada"].Value.ToString(),
                                                            Grade2.Rows[Grade2.CurrentRow.Index].Cells["Hora"].Value.ToString(),
                                                            Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoResiduo"].Value.ToString()),
                                                            0);
                    }
                }
            }
            else if (e.KeyCode.Equals(Keys.F7))
            {
                if (Grade2.Rows.Count > 0)
                {
                    if (_dia + "/" + _mes + "/" + _ano != DataProgAberta.Text)
                    {
                        MessageBox.Show("Está não é programação aberta!", "SILC", MessageBoxButtons.OK);
                    }
                    else
                    {
                        if (lblBloqueadoPeloUsuario.Text != _bloqPor)
                            MessageBox.Show("Não é possível fazer alterações, pois a Programação Diária está sendo usada pelo usuário: " + oProgramacaoFechada.BloqueadaCodigoUsuario, "SILC", MessageBoxButtons.OK);
                        else if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString() == "")
                        {
                            MessageBox.Show("Sequencial de programação inválida! Iniciarei refresh para gerá-lo.", "SILC", MessageBoxButtons.OK);
                            PreencheGrades(DataProgAberta.Text);
                        }
                        else
                        { 
                            frmObservacao o_frmObservacao = new frmObservacao();
                            o_frmObservacao.lblNrSequencial.Text = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString();
                            o_frmObservacao.cboTabMotivosOBS.Text = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Observacao"].Value.ToString();
                            o_frmObservacao.ShowDialog();
                            if (o_frmObservacao.bSalvar)
                            {
                                for (int i = 0; i < Grade2.Columns.Count; i++)
                                {
                                    Grade2.Rows[Grade2.CurrentRow.Index].Cells[i].Style.BackColor = Color.Yellow;
                                    Grade2.Rows[Grade2.CurrentRow.Index].Cells[i].Style.ForeColor = Color.Black;
                                }
                                Grade2.Rows[Grade2.CurrentRow.Index].Cells["StatusCor"].Value = "65535";
                                Grade2.Rows[Grade2.CurrentRow.Index].Cells["Observacao"].Value = o_frmObservacao.cboTabMotivosOBS.Text;
                                oProgramacaoDados.SalvarCancelar(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString(), o_frmObservacao.cboTabMotivosOBS.Text);

                                //incluido dia 30-05-2023 - edson
                                SalvarServicoFutura(Grade2.CurrentRow.Index, Grade2, Grade2.Rows[Grade2.CurrentRow.Index].Cells["NumeroMTRe"].Value.ToString());

                                if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString() != "")
                                    oStatusCorProg.SequencialProgramacao = (int)Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value;
                                oStatusCorProg.StatusCor = "65535";
                                oStatusCorProgDados.Inserir(oStatusCorProg);
                                // excluir reprogramacao, se houver 
                                if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCliente"].Value.ToString() != "")
                                {
                                    SalvarLog("Cancelou serviço", "Programação de Serviços", Grade2);
                                    oReprogramacao.Excluir(Grade2.Rows[Grade2.CurrentRow.Index].Cells["DataProgramada"].Value.ToString(),
                                                                       Grade2.Rows[Grade2.CurrentRow.Index].Cells["Hora"].Value.ToString(),
                                                                       Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCliente"].Value.ToString()));
                                }
                            }
                        }
                    }
                }
            }
            else if (e.KeyCode.Equals(Keys.F8))
            {
                if (Grade2.Rows.Count > 0)
                {
                    if (lblBloqueadoPeloUsuario.Text != _bloqPor)
                        MessageBox.Show("Não é possível fazer alterações, pois a Programação Diária está sendo usada pelo usuário: " + oProgramacaoFechada.BloqueadaCodigoUsuario, "SILC", MessageBoxButtons.OK);
                    else if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["StatusCor"].Value.ToString() == geral.RetornaCodigoCor("Branco") ||
                             Grade2.Rows[Grade2.CurrentRow.Index].Cells["StatusCor"].Value.ToString() == geral.RetornaCodigoCor("Cinza"))
                        MessageBox.Show("Lista de reprogramação solicitada inválida!", "SILC", MessageBoxButtons.OK);
                    else
                    {
                        frmListaReprogramacao frmListaReprog = new frmListaReprogramacao();
                        frmListaReprog.Sequencial = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString());
                        frmListaReprog.pCodigoCliente = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCliente"].Value.ToString());
                        frmListaReprog.pCodigoResiduo = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoResiduo"].Value.ToString());
                        frmListaReprog.pAnoMesDia = Convert.ToInt32(Convert.ToDateTime(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Data"].Value).ToString("yyyyMMdd"));
                        frmListaReprog.pDataProgramada = (DateTime)Grade2.Rows[Grade2.CurrentRow.Index].Cells["DataProgramada"].Value;
                        frmListaReprog.pExecutarServico = Grade2.Rows[Grade2.CurrentRow.Index].Cells["ExecutarServico"].Value.ToString();
                        frmListaReprog.pHora = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Hora"].Value.ToString();
                        frmListaReprog.pNomeCliente = Grade2.Rows[Grade2.CurrentRow.Index].Cells["NomeFantasiaCliente"].Value.ToString();
                        frmListaReprog.pStatusCor = Grade2.Rows[Grade2.CurrentRow.Index].Cells["StatusCor"].Value.ToString();
                        frmListaReprog.pTipoContrato = Grade2.Rows[Grade2.CurrentRow.Index].Cells["TipoProgramacao"].Value.ToString();
                        frmListaReprog.pDataProgramaAbertaAtual = DataProgAberta.Text; 
                        frmListaReprog.ShowDialog();
                        if (frmListaReprog.bSalvarRefresh)
                        {
                            PreencheGrades(DataProgAberta.Text);
                        }
                    }
                }
            }
            else if (e.KeyCode.ToString().Equals("M") || e.KeyCode.ToString().Equals("m"))
            {
                if (Grade2.Rows.Count > 0)
                {                  
                    if (_dia + "/" + _mes + "/" + _ano != DataProgAberta.Text)
                    {
                        MessageBox.Show("Está não é programação aberta!", "SILC", MessageBoxButtons.OK);
                    }
                    else
                    {
                        if (lblBloqueadoPeloUsuario.Text != _bloqPor)
                            MessageBox.Show("Não é possível fazer alterações, pois a Programação Diária está sendo usada pelo usuário: " + oProgramacaoFechada.BloqueadaCodigoUsuario, "SILC", MessageBoxButtons.OK);
                        else
                        {
                            if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["TipoProgramacao"].Value.ToString().Equals("A") ||
                                Grade2.Rows[Grade2.CurrentRow.Index].Cells["TipoProgramacao"].Value.ToString().Equals("1"))
                            {
                                MessageBox.Show("Não é possível alterar uma programação automática para manual!", "SILC", MessageBoxButtons.OK);
                            }
                            else if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["StatusCor"].Value.ToString().Equals("8421631"))
                                MessageBox.Show("Reprogramação. F8 pra voltar ou excluir programação!", "SILC", MessageBoxButtons.OK);
                            else if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["StatusCor"].Value.ToString().Equals("8427929"))
                                MessageBox.Show("Já é Programação manual!", "SILC", MessageBoxButtons.OK);
                            else if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["StatusCor"].Value.ToString().Equals("65535"))
                                MessageBox.Show("Utilize F6 para retornar!", "SILC", MessageBoxButtons.OK);
                            else if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["StatusCor"].Value.ToString().Equals("65280"))
                                MessageBox.Show("Utilize F6 para retornar!", "SILC", MessageBoxButtons.OK);
                            else
                            {
                                DialogResult dlg1 = new DialogResult();
                                dlg1 = MessageBox.Show("Você vai forçar voltar para programação manual confirma?", "Força programação manual", MessageBoxButtons.YesNo);
                                if (dlg1 == DialogResult.Yes)
                                {
                                    for (int i = 0; i < Grade2.Columns.Count; i++)
                                    {
                                        Grade2.Rows[Grade2.CurrentRow.Index].Cells[i].Style.BackColor = Color.Gray;
                                    }
                                    SalvarLog("Marcou como serviço manual", "Programação de Serviços", Grade2);
                                    oProgramacaoDados.SalvarCorManual(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString());
                                }
                            }
                        }
                    }
                }
            }
            else if (e.KeyCode.Equals(Keys.Insert))
            {
                if (_dia + "/" + _mes + "/" + _ano != DataProgAberta.Text)
                {
                    MessageBox.Show("Está não é programação aberta!", "SILC", MessageBoxButtons.OK);
                }
                else
                {
                    if (lblBloqueadoPeloUsuario.Text != _bloqPor)
                        MessageBox.Show("Não é possível fazer alterações, pois a Programação Diária está sendo usada pelo usuário: " + oProgramacaoFechada.BloqueadaCodigoUsuario, "SILC", MessageBoxButtons.OK);
                    else
                    {
                        frmInsertProgramacao o_frmInsertProgramacao = new frmInsertProgramacao();
                        o_frmInsertProgramacao.oProgramacaoDiaria.AnoMesDia = Convert.ToInt32(_ano + _mes + _dia);
                        o_frmInsertProgramacao.dtpData.Text = DataProgAberta.Text;
                        o_frmInsertProgramacao.dtpProgramacaoAberta.Value = DataProgAberta.Value;
                        o_frmInsertProgramacao.txtHora.Text = DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00");
                        o_frmInsertProgramacao.lblStatus.Text = geral.RetornaCodigoCor("Cinza");
                        o_frmInsertProgramacao.ShowDialog();
                        o_frmInsertProgramacao.Close();
                        if (geral.VoltaForm.IndexOf("ProgramaçãoInserida") >= 0)
                        {
                            bPreencherGrades = true;
                        }
                    }
                }
            }
            else if (e.KeyCode.Equals(Keys.F11)) // alteração de programação manual
            {
                if (_dia + "/" + _mes + "/" + _ano != DataProgAberta.Text)
                {
                    MessageBox.Show("Está não é programação aberta!", "SILC", MessageBoxButtons.OK);
                }
                else
                {
                    if (lblBloqueadoPeloUsuario.Text != _bloqPor)
                        MessageBox.Show("Não é possível fazer alterações, pois a Programação Diária está sendo usada pelo usuário: " + oProgramacaoFechada.BloqueadaNomeUsuario, "SILC", MessageBoxButtons.OK);
                    else
                    {
                        if (Grade2.Rows.Count <= 0)
                            MessageBox.Show("Quantidade de items inválida!", "SILC", MessageBoxButtons.OK);
                        if ((Grade2.Rows[Grade2.CurrentRow.Index].Cells["TipoProgramacao"].Value.ToString() == "A" && Grade2.Rows[Grade2.CurrentRow.Index].Cells["Hora"].Value.ToString().IndexOf(":") == -1) || 
                            Grade2.Rows[Grade2.CurrentRow.Index].Cells["TipoProgramacao"].Value.ToString() == "1")
                            MessageBox.Show("Tipo de programação inválida!", "SILC", MessageBoxButtons.OK);
                        else
                        {
                            // quando dá o enter - pula um item - então uma solução retornar ao item anterior
                            Grade2.Rows[Grade2.CurrentRow.Index].Selected = false;
                            Grade2.Rows[Grade2.CurrentRow.Index].Selected = true;
                            geral.VoltaForm = "";
                            frmInsertProgramacao o_frmInsertProgramacao = new frmInsertProgramacao();
                            o_frmInsertProgramacao.Text = "Programação manual - alteração";
                            if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString() != "")
                                o_frmInsertProgramacao.lblNrSequencial.Text = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value).ToString("0000000");
                            o_frmInsertProgramacao.oProgramacaoDiaria.AnoMesDia = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["AnoMesDia"].Value);
                            if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString() != "")
                                o_frmInsertProgramacao.oProgramacaoDiaria.Sequencial = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value);
                            o_frmInsertProgramacao.txtSolicitante.Text = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Solicitante"].Value.ToString();
                            o_frmInsertProgramacao.cliente1.txtCodigo.Text = Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCliente"].Value.ToString();
                            o_frmInsertProgramacao.cliente1.txtDescricao.Text = Grade2.Rows[Grade2.CurrentRow.Index].Cells["NomeFantasiaCliente"].Value.ToString();
                            if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoResiduo"] != null)
                                o_frmInsertProgramacao.residuo1.txtCodigo.Text = Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoResiduo"].Value.ToString();
                            o_frmInsertProgramacao.residuo1.txtDescricao.Text = Grade2.Rows[Grade2.CurrentRow.Index].Cells["ExecutarServico"].Value.ToString();
                            o_frmInsertProgramacao.txtServicoAExecutar.Text = Grade2.Rows[Grade2.CurrentRow.Index].Cells["HoraProgramada"].Value.ToString();
                            if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["Quantidade"].Value.ToString() != "")
                                o_frmInsertProgramacao.intQuantidade.VALOR.Text = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Quantidade"].Value.ToString();
                            o_frmInsertProgramacao.intQuantidade.Enabled = false;
                            o_frmInsertProgramacao.txtUnidade.Text = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Unidade"].Value.ToString();
                            o_frmInsertProgramacao.dtpData.Text = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Data"].Value.ToString();
                            if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCaminhao"] != null)
                                o_frmInsertProgramacao.caminhao1.txtCodigo.Text = Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCaminhao"].Value.ToString();
                            if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoMotorista"] != null)
                                o_frmInsertProgramacao.funcionario1.txtCodigo.Text = Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoMotorista"].Value.ToString();
                            o_frmInsertProgramacao.cboTabMotivosOBS.Text = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Observacao"].Value.ToString();
                            o_frmInsertProgramacao.lblStatus.Text = Grade2.Rows[Grade2.CurrentRow.Index].Cells["StatusCor"].Value.ToString();
                            o_frmInsertProgramacao.dtpDataProgramada.Text = Grade2.Rows[Grade2.CurrentRow.Index].Cells["DataProgramada"].Value.ToString();
                            o_frmInsertProgramacao.dtpProgramacaoAberta.Value = DataProgAberta.Value;
                            o_frmInsertProgramacao.txtHora.Text = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Hora"].Value.ToString();
                            o_frmInsertProgramacao.cboDestinoFinal.Text = Grade2.Rows[Grade2.CurrentRow.Index].Cells["DestinoFinal"].Value.ToString();
                            o_frmInsertProgramacao.ShowDialog();
                            if (geral.VoltaForm == "ProgramaçãoAlterada")
                            {
                                Grade2.Rows[Grade2.CurrentRow.Index].Cells["DataProgramada"].Value = o_frmInsertProgramacao.dtpDataProgramada.Text;
                                Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoResiduo"].Value = o_frmInsertProgramacao.residuo1.txtCodigo.Text;
                                Grade2.Rows[Grade2.CurrentRow.Index].Cells["ExecutarServico"].Value = o_frmInsertProgramacao.residuo1.txtDescricao.Text;
                                Grade2.Rows[Grade2.CurrentRow.Index].Cells["HoraProgramada"].Value = o_frmInsertProgramacao.txtServicoAExecutar.Text;
                                Grade2.Rows[Grade2.CurrentRow.Index].Cells["Solicitante"].Value = o_frmInsertProgramacao.txtSolicitante.Text;
                                Grade2.Rows[Grade2.CurrentRow.Index].Cells["StatusCor"].Value = o_frmInsertProgramacao.lblStatus.Text;
                                Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCliente"].Value = o_frmInsertProgramacao.cliente1.txtCodigo.Text;
                                Grade2.Rows[Grade2.CurrentRow.Index].Cells["NomeFantasiaCliente"].Value = o_frmInsertProgramacao.cliente1.txtDescricao.Text;
                                if (o_frmInsertProgramacao.intQuantidade.VALOR.Text != "")
                                    Grade2.Rows[Grade2.CurrentRow.Index].Cells["Quantidade"].Value = o_frmInsertProgramacao.intQuantidade.VALOR.Text;
                                Grade2.Rows[Grade2.CurrentRow.Index].Cells["Unidade"].Value = o_frmInsertProgramacao.txtUnidade.Text;
                                Grade2.Rows[Grade2.CurrentRow.Index].Cells["Observacao"].Value = o_frmInsertProgramacao.cboTabMotivosOBS.Text;
                                Grade2.Rows[Grade2.CurrentRow.Index].Cells["DestinoFinal"].Value = o_frmInsertProgramacao.cboDestinoFinal.Text;
                                Grade2.Rows[Grade2.CurrentRow.Index].Cells["StatusCor"].Value = o_frmInsertProgramacao.lblStatus.Text;

                                clsEnderecosDados oEnderecoDados = new clsEnderecosDados();
                                DataTable _dtEnderecos = new DataTable();
                                _dtEnderecos = oEnderecoDados.PreencheDataTableEnderecosClientes(o_frmInsertProgramacao.cliente1.txtCodigo.Text, 2);

                                DataTable _dtParticularidadeContrato = new DataTable();
                                clsContratoResiduosDados oContratoDados = new clsContratoResiduosDados();
                                _dtParticularidadeContrato = oContratoDados.RetornaParticularidade();

                                DataRow[] _drEnd = _dtEnderecos.Select("Codigo = " + o_frmInsertProgramacao.cliente1.txtCodigo.Text);
                                if (_drEnd.Length > 0)
                                    Grade2.Rows[Grade2.CurrentRow.Index].Cells["CidadeBairroEndereco"].Value = _drEnd[0]["CidadeBairroEndereco"].ToString();

                                DataRow[] _drPart = _dtParticularidadeContrato.Select("CodigoCliente = " + o_frmInsertProgramacao.cliente1.txtCodigo.Text + " and CodigoResiduo = " + o_frmInsertProgramacao.residuo1.txtCodigo.Text);
                                if (_drPart.Length > 0)
                                    Grade2.Rows[Grade2.CurrentRow.Index].Cells["Particularidade"].Value = _drPart[0]["Particularidade"].ToString();

                                if (o_frmInsertProgramacao.caminhao1.txtCodigo.Text != "" &&
                                    o_frmInsertProgramacao.caminhao1.txtCodigo.Text != "0")
                                {
                                    Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCaminhao"].Value = o_frmInsertProgramacao.caminhao1.txtCodigo.Text;
                                    clsCaminhoes oCaminhao = new clsCaminhoes();
                                    clsCaminhoesDados oCaminhaoDados = new clsCaminhoesDados();
                                    if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCaminhao"].Value.ToString().Length > 0)
                                    {
                                        oCaminhao = oCaminhaoDados.PegaDados(oCaminhao, Convert.ToUInt16(o_frmInsertProgramacao.caminhao1.txtCodigo.Text));
                                        Grade2.Rows[Grade2.CurrentRow.Index].Cells["ModeloCaminhao"].Value = oCaminhao.Modelo;
                                    }
                                    else
                                        Grade2.Rows[Grade2.CurrentRow.Index].Cells["ModeloCaminhao"].Value = o_frmInsertProgramacao.caminhao1.txtDescricao.Text;
                                }
                                if (o_frmInsertProgramacao.funcionario1.txtCodigo.Text != "" &&
                                    o_frmInsertProgramacao.funcionario1.txtCodigo.Text != "0")
                                {
                                    Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoMotorista"].Value = o_frmInsertProgramacao.funcionario1.txtCodigo.Text;
                                    clsFuncionarios oFuncionario = new clsFuncionarios();
                                    clsFuncionarioDados oFuncDados = new clsFuncionarioDados();
                                    if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoMotorista"].Value.ToString().Length > 0)
                                    {
                                        oFuncionario = oFuncDados.PegaDados(oFuncionario, Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoMotorista"].Value));
                                        Grade2.Rows[Grade2.CurrentRow.Index].Cells["NomeMotorista"].Value = oFuncionario.Nome;
                                    }
                                    else
                                        Grade2.Rows[Grade2.CurrentRow.Index].Cells["NomeMotorista"].Value = o_frmInsertProgramacao.funcionario1.txtDescricao.Text;
                                }
                                SalvarLog("Alterou com F11", "Programação de Serviços", Grade2);
                                SalvarServicoFutura(Grade2.CurrentRow.Index, Grade2, Grade2.Rows[Grade2.CurrentRow.Index].Cells["NumeroMTRe"].Value.ToString());
                            }
                            o_frmInsertProgramacao.Close();
                        }
                    }
                }
            }
            else if (e.KeyCode.Equals(Keys.F5))
            {
                if (Grade2.Rows.Count > 0)
                {
                    _cdResiduo = Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoResiduo"].Value.ToString();
                    if (_cdResiduo == "" || _cdResiduo == "0")
                    {
                        clsResiduoDados oResDados = new clsResiduoDados();
                        _cdResiduo = oResDados.PegaCodigoResiduo(Grade2.Rows[Grade2.CurrentRow.Index].Cells["ExecutarServico"].Value.ToString()).ToString();
                    }
                    if (_dia + "/" + _mes + "/" + _ano != DataProgAberta.Text &&
                        Convert.ToDateTime(_dia + "/" + _mes + "/" + _ano) > Convert.ToDateTime(DataProgAberta.Text) &&
                        Grade2.Rows[Grade2.CurrentRow.Index].Cells["StatusCor"].Value.ToString() != geral.RetornaCodigoCor("Vermelho") &&
                        Grade2.Rows[Grade2.CurrentRow.Index].Cells["StatusCor"].Value.ToString() != geral.RetornaCodigoCor("Branco") &&
                        Grade2.Rows[Grade2.CurrentRow.Index].Cells["StatusCor"].Value.ToString() != geral.RetornaCodigoCor("AzulClaro"))
                    {
                        MessageBox.Show("Está não é programação aberta!", "SILC", MessageBoxButtons.OK);
                    }
                    else if (oProgramacaoDados.ExisteProgramacaoFechada(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Hora"].Value.ToString(),
                                                                        Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCliente"].Value.ToString(),
                                                                        _cdResiduo,
                                                                        Grade2.Rows[Grade2.CurrentRow.Index].Cells["Solicitante"].Value.ToString()))
                    {
                        MessageBox.Show("Não é possível reprogramar uma programação que já foi fechada! Inclusive o mesmo Solicitante.", "SILC", MessageBoxButtons.OK);
                    }
                    else
                    {
                        if (lblBloqueadoPeloUsuario.Text != _bloqPor)
                            MessageBox.Show("Não é possível fazer alterações, pois a Programação Diária está sendo usada pelo usuário: " + oProgramacaoFechada.BloqueadaCodigoUsuario, "SILC", MessageBoxButtons.OK);
                        else
                        {                            
                            frmReprogramacao o_frmReprogramacao = new frmReprogramacao();
                            o_frmReprogramacao.lblNrSequencial.Text = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString();
                            o_frmReprogramacao.dtpDataReprogramada.Text = Convert.ToDateTime(Grade2.Rows[Grade2.CurrentRow.Index].Cells["DataProgramada"].Value).ToString("yyyy-MM-dd");
                            o_frmReprogramacao.cboTabMotivosOBS.Text = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Observacao"].Value.ToString();
                            o_frmReprogramacao.pDataProgramacaoAberta = DataProgAberta.Text;
                            o_frmReprogramacao.pDataProgramada = Convert.ToDateTime(Grade2.Rows[Grade2.CurrentRow.Index].Cells["DataProgramada"].Value).ToString("yyyy-MM-dd");
                            o_frmReprogramacao.ShowDialog();
                            if (o_frmReprogramacao.bSalvar)
                            {
                                for (int i = 0; i < Grade2.Columns.Count; i++)
                                {
                                    Grade2.Rows[Grade2.CurrentRow.Index].Cells[i].Style.BackColor = Color.Red;
                                    Grade2.Rows[Grade2.CurrentRow.Index].Cells[i].Style.ForeColor = Color.White;
                                }
                                Grade2.Rows[Grade2.CurrentRow.Index].Cells["DataProgramada"].Value = Convert.ToDateTime(o_frmReprogramacao.dtpDataReprogramada.Text).ToString("yyyy-MM-dd");
                                Grade2.Rows[Grade2.CurrentRow.Index].Cells["StatusCor"].Value = geral.RetornaCodigoCor("Vermelho");
                                Grade2.Rows[Grade2.CurrentRow.Index].Cells["Observacao"].Value = o_frmReprogramacao.cboTabMotivosOBS.Text;
                                clsProgramacaoDiariaServicos oProgramacaoDiaria = new clsProgramacaoDiariaServicos();
                                if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString() != "")
                                    oProgramacaoDiaria.Sequencial = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value);
                                oProgramacaoDiaria.AnoMesDia = Convert.ToInt32(_ano + _mes + _dia);
                                oProgramacaoDiaria.Solicitante = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Solicitante"].Value.ToString();
                                oProgramacaoDiaria.CodigoCliente = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCliente"].Value);
                                if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoResiduo"] != null)
                                    oProgramacaoDiaria.CodigoResiduo = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoResiduo"].Value);
                                oProgramacaoDiaria.ExecutarServico = Grade2.Rows[Grade2.CurrentRow.Index].Cells["ExecutarServico"].Value.ToString();
                                if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["Quantidade"].Value.ToString() != "")
                                {
                                    if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["Quantidade"].Value.ToString().IndexOf(".") >= 0)
                                        oProgramacaoDiaria.Quantidade = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Quantidade"].Value.ToString().Replace(".", "")) / 10000;
                                    else
                                        oProgramacaoDiaria.Quantidade = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Quantidade"].Value.ToString().Replace(".", ""));
                                }
                                oProgramacaoDiaria.Unidade = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Unidade"].Value.ToString();
                                if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCaminhao"].Value.ToString() != "")
                                    oProgramacaoDiaria.CodigoCaminhao = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCaminhao"].Value);
                                if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoMotorista"].Value.ToString() != "")
                                    oProgramacaoDiaria.CodigoMotorista = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoMotorista"].Value);
                                oProgramacaoDiaria.Observacao = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Observacao"].Value.ToString();
                                oProgramacaoDiaria.StatusCor = "8421631";
                                oProgramacaoDiaria.DataProgramada = Grade2.Rows[Grade2.CurrentRow.Index].Cells["DataProgramada"].Value.ToString();
                                oProgramacaoDiaria.Data = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Data"].Value.ToString();
                                oProgramacaoDiaria.Hora = Grade2.Rows[Grade2.CurrentRow.Index].Cells["Hora"].Value.ToString();
                                oProgramacaoDiaria.ServicoExecutado = Grade2.Rows[Grade2.CurrentRow.Index].Cells["HoraProgramada"].Value.ToString();
                                SalvarLog("Reprogramou", "Programação de Serviços", Grade2);
                                if (DataProgAberta.Text == _dia + "/" + _mes + "/" + _ano  || DataProgAberta.Text == o_frmReprogramacao.pDataProgramada)
                                {
                                    clsProgramacaoDados oProgramacaoDados = new clsProgramacaoDados();
                                    if (oProgramacaoDados.ExisteProgramacao(Convert.ToInt32(Convert.ToDateTime(DataProgAberta.Text).ToString("yyyyMMdd")), oProgramacaoDiaria.Hora, oProgramacaoDiaria.CodigoCliente, oProgramacaoDiaria.DataProgramada) == "Incluir")
                                    {
                                        oProgramacaoDiaria.AnoMesDia = Convert.ToInt32(Convert.ToDateTime(DataProgAberta.Text).ToString("yyyyMMdd"));
                                        oProgramacaoDiaria.Quadro = 2;
                                        oProgramacaoDados.Inserir(oProgramacaoDiaria);
                                        oProgramacaoDiaria.AnoMesDia = Convert.ToInt32(_ano + _mes + _dia);
                                    }
                                    else //alterar salvar data programada nova e cor vermelho para statuscor
                                    {
                                        oProgramacaoDados.SalvarDataReprogramada(oProgramacaoDiaria.Sequencial.ToString(), oProgramacaoDiaria.DataProgramada);
                                        oProgramacaoDados.SalvarStatusCor(oProgramacaoDiaria.Sequencial.ToString(), oProgramacaoDiaria.StatusCor);
                                    }
                                }
                                clsReprogramacaoDados oReprogDados = new clsReprogramacaoDados();
                                oProgramacaoDiaria.AnoMesDia = Convert.ToInt32(Convert.ToDateTime(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Data"].Value).ToString("yyyyMMdd"));
                                if (oReprogDados.ExisteReprogramacao(oProgramacaoDiaria.AnoMesDia, oProgramacaoDiaria.Hora, oProgramacaoDiaria.CodigoCliente, oProgramacaoDiaria.DataProgramada) == "Incluir")
                                {
                                    oProgramacaoDados.SalvarDataReprogramada(oProgramacaoDiaria.Sequencial.ToString(), oProgramacaoDiaria.DataProgramada);                                    
                                    SalvarReprogramacaoManualFuturaAntecipacao(oProgramacaoDiaria, geral.RetornaCodigoCor("Vermelho"));
                                }
                                if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString() != "")
                                    oStatusCorProg.SequencialProgramacao = (int)Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value;

                                // linha colocada dia 30/05/23
                                SalvarServicoFutura(Grade2.CurrentRow.Index, Grade2, Grade2.Rows[Grade2.CurrentRow.Index].Cells["NumeroMTRe"].Value.ToString());   

                            }
                        }
                    }
                }
            }
            else if (e.KeyCode.Equals(Keys.F6))
            {
                if (Grade2.Rows.Count > 0)
                { 
                    string _statuscoratual = Grade2.Rows[Grade2.CurrentRow.Index].Cells["StatusCor"].Value.ToString();
                    // Retorna Status Programacao Diaria
                    if (_dia + "/" + _mes + "/" + _ano != DataProgAberta.Text)
                    {
                        MessageBox.Show("Está não é programação aberta!", "SILC", MessageBoxButtons.OK);
                    }
                    else if (_statuscoratual == "" || _statuscoratual == null || _statuscoratual == "0" || _statuscoratual == "8427929" ||
                             _statuscoratual == "-2147483647"  || _statuscoratual == "064444")
                    {
                        MessageBox.Show("Retorno inválido!", "SILC", MessageBoxButtons.OK);
                    }
                    else if (_statuscoratual == "8421631" && Grade2.Rows[Grade2.CurrentRow.Index].Cells["Rp"].Value.ToString() != "0")
                    {
                        MessageBox.Show("Há reprogramações. Retorno inválido!", "SILC", MessageBoxButtons.OK);
                    }
                    else if (oProgramacaoDados.ExisteProgramacaoFechada(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Hora"].Value.ToString(),
                                                                        Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCliente"].Value.ToString(),
                                                                        Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoResiduo"].Value.ToString(),
                                                                        Grade2.Rows[Grade2.CurrentRow.Index].Cells["Solicitante"].Value.ToString()))
                    {
                        MessageBox.Show("Não é possível retornar uma programação que já foi fechada! Inclusive o mesmo Solicitante.", "SILC", MessageBoxButtons.OK);
                    }
                    else
                    {
                        if (lblBloqueadoPeloUsuario.Text != _bloqPor)
                            MessageBox.Show("Não é possível fazer alterações, pois a Programação Diária está sendo usada pelo usuário: " + oProgramacaoFechada.BloqueadaCodigoUsuario, "SILC", MessageBoxButtons.OK);
                        else
                        {
                            int _sequencial = Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value);
                            if (geral.RetornaCodigoCor("AzulClaro") == Grade2.Rows[Grade2.CurrentRow.Index].Cells["StatusCor"].Value.ToString())
                            {
                                DialogResult dlg1 = new DialogResult();
                                dlg1 = MessageBox.Show("Retornar antecipação do serviço para seu dia de origem?", "Retornar serviço", MessageBoxButtons.YesNo);
                                if (dlg1 == DialogResult.Yes)
                                {
                                    oProgramacaoDados.Excluir(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Hora"].Value.ToString(), 
                                                              Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCliente"].Value));
                                    Grade2.Rows.RemoveAt(Grade2.CurrentRow.Index);
                                }
                            }
                            else
                            {
                                oStatusCorProg = oStatusCorProgDados.PegaDados(oStatusCorProg, _sequencial);
                                oStatusCorProgDados.Excluir(oStatusCorProg.Codigo);
                                oStatusCorProg = oStatusCorProgDados.PegaDados(oStatusCorProg, _sequencial);
                                if (oStatusCorProg.StatusCor == "" || oStatusCorProg.StatusCor == null || oStatusCorProg.StatusCor == "-2147483647")
                                {
                                    for (int i = 0; i < Grade2.Columns.Count; i++)
                                    {
                                        if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["TipoProgramacao"].Value.ToString() == "A")
                                        {
                                            oStatusCorProg.StatusCor = "-2147483647";
                                            Grade2.Rows[Grade2.CurrentRow.Index].Cells[i].Style.BackColor = RetornaCorPreEstabelecidas(oStatusCorProg.StatusCor);
                                        }
                                        else
                                        {
                                            oStatusCorProg.StatusCor = "8427929";
                                            Grade2.Rows[Grade2.CurrentRow.Index].Cells[i].Style.BackColor = RetornaCorPreEstabelecidas(oStatusCorProg.StatusCor);
                                        }
                                    }
                                    Grade2.Rows[Grade2.CurrentRow.Index].Cells["StatusCor"].Value = oStatusCorProg.StatusCor;
                                    oProgramacaoDados.SalvarStatusCor(_sequencial.ToString(), oStatusCorProg.StatusCor);
                                }
                                else if (oStatusCorProg.StatusCor != "" && oStatusCorProg.StatusCor != "-2147483647" &&
                                         oStatusCorProg.StatusCor != null)
                                {
                                    for (int i = 0; i < Grade2.Columns.Count; i++)
                                    {
                                        Grade2.Rows[Grade2.CurrentRow.Index].Cells[i].Style.BackColor = RetornaCorPreEstabelecidas(oStatusCorProg.StatusCor);
                                        if (oStatusCorProg.StatusCor == "8421631")
                                            Grade2.Rows[Grade2.CurrentRow.Index].Cells[i].Style.ForeColor = Color.White;
                                        else
                                            Grade2.Rows[Grade2.CurrentRow.Index].Cells[i].Style.ForeColor = Color.Black;
                                    }
                                    Grade2.Rows[Grade2.CurrentRow.Index].Cells["StatusCor"].Value = oStatusCorProg.StatusCor;
                                    oProgramacaoDados.SalvarStatusCor(_sequencial.ToString(), oStatusCorProg.StatusCor);
                                }
                            }
                        }
                    }
                }
            }
            if (bPreencherGrades)
            {
                // programação futura
                if (Convert.ToDateTime(_dia + "/" + _mes + "/" + _ano) > Convert.ToDateTime(DataProgAberta.Text))
                {
                    string _dtAbertaBanco = geral.Left(oProgramacaoFechadaDados.DataUltimaProgAberta(), 10);
                    if (DataProgAberta.Text != _dtAbertaBanco)
                    {
                        MessageBox.Show("A programação aberta está diferente da atual. \nReinicie a Programação Diária de Serviços. ");
                    }
                    else
                        MontaProgramacaoFutura();
                }
                else
                {
                    PreencheGrades(_dia + "/" + _mes + "/" + _ano);
                    if (geral.VoltaForm.IndexOf("ProgramaçãoInserida") >= 0)
                    {
                        SalvarLog("Inseriu serviço manual", "Programação de Serviços", Grade2);
                    }
                }
            }
        }
        private void SalvarLog(string pOperacao, string pLocalOperacao, DataGridView pGrade)
        {
            clsLog oLog = new clsLog();
            clsLogDados oLogDados = new clsLogDados();
            oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
            if (geral.CodigoUsuarioAtual == 0)
               geral.CodigoUsuarioAtual = 10;
            oLog.CodigoUsuario = geral.CodigoUsuarioAtual;
            oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
            oLog.LocalOperacao = pLocalOperacao;
            oLog.Operacao = pOperacao;
            if (pOperacao == "Inseriu serviço manual")
                oLog.Log = PegaLogObjetoProgramacao(pGrade, pGrade.Rows.Count);
            else
                oLog.Log = PegaLogObjetoProgramacao(pGrade);
            oLogDados.Inserir(oLog);
        }
        private string PegaLogObjetoProgramacao(DataGridView pGrade, int pLinha = 0)
        {
            string sRet = "";
            clsProgramacaoDiariaServicos oProgramacaoDiaria = new clsProgramacaoDiariaServicos();
            if (pLinha == 0)
                pLinha = pGrade.CurrentRow.Index;
            if (pLinha > pGrade.Rows.Count - 1)
                pLinha = pGrade.Rows.Count - 1;
            if (pGrade.Rows[pLinha].Cells["Sequencial"].Value.ToString() != "")
            {
                oProgramacaoDiaria.Sequencial = Convert.ToInt32(pGrade.Rows[pLinha].Cells["Sequencial"].Value);
                sRet = sRet + "Sequencial: " + oProgramacaoDiaria.Sequencial + " \n";
            }
            oProgramacaoDiaria.AnoMesDia = Convert.ToInt32(_ano + _mes + _dia);
            sRet = sRet + "Ano Mês Dia: " + oProgramacaoDiaria.AnoMesDia + " \n";
            oProgramacaoDiaria.Solicitante = pGrade.Rows[pLinha].Cells["Solicitante"].Value.ToString();
            sRet = sRet + "Solicitante: " + oProgramacaoDiaria.Solicitante + " \n";
            oProgramacaoDiaria.CodigoCliente = Convert.ToInt32(pGrade.Rows[pLinha].Cells["CodigoCliente"].Value);
            sRet = sRet + "Código Cliente: " + oProgramacaoDiaria.CodigoCliente + " \n";
            if (pGrade.Rows[pLinha].Cells["CodigoResiduo"] != null)
            {
                oProgramacaoDiaria.CodigoResiduo = Convert.ToInt32(pGrade.Rows[pLinha].Cells["CodigoResiduo"].Value);
                sRet = sRet + "Código Resíduo: " + oProgramacaoDiaria.CodigoResiduo + " \n";
            }
            oProgramacaoDiaria.ExecutarServico = pGrade.Rows[pLinha].Cells["ExecutarServico"].Value.ToString();
            sRet = sRet + "Executar serviço: " + oProgramacaoDiaria.ExecutarServico + " \n";
            oProgramacaoDiaria.ServicoExecutado = pGrade.Rows[pLinha].Cells["HoraProgramada"].Value.ToString();
            sRet = sRet + "Executado: " + oProgramacaoDiaria.ServicoExecutado + " \n";
            if (pGrade.Rows[pLinha].Cells["Quantidade"].Value.ToString() != "")
            {
                oProgramacaoDiaria.Quantidade = Convert.ToInt32(pGrade.Rows[pLinha].Cells["Quantidade"].Value);
                sRet = sRet + "Quantidade: " + oProgramacaoDiaria.Quantidade + " \n";
            }
            oProgramacaoDiaria.Unidade = pGrade.Rows[pLinha].Cells["Unidade"].Value.ToString();
            sRet = sRet + "Unidade: " + oProgramacaoDiaria.Unidade + " \n";
            if (pGrade.Rows[pLinha].Cells["CodigoCaminhao"].Value.ToString() != "")
            {
                oProgramacaoDiaria.CodigoCaminhao = Convert.ToInt32(pGrade.Rows[pLinha].Cells["CodigoCaminhao"].Value);
                sRet = sRet + "Código caminhão: " + oProgramacaoDiaria.CodigoCaminhao + " \n";
            }
            if (pGrade.Rows[pLinha].Cells["CodigoMotorista"].Value.ToString() != "")
            {
                oProgramacaoDiaria.CodigoMotorista = Convert.ToInt32(pGrade.Rows[pLinha].Cells["CodigoMotorista"].Value);
                sRet = sRet + "Código motorista: " + oProgramacaoDiaria.CodigoMotorista + " \n";
            }
            oProgramacaoDiaria.Observacao = pGrade.Rows[pLinha].Cells["Observacao"].Value.ToString();
            sRet = sRet + "Observação: " + oProgramacaoDiaria.Observacao + " \n";
            oProgramacaoDiaria.StatusCor = pGrade.Rows[pLinha].Cells["StatusCor"].Value.ToString();
            sRet = sRet + "StatusCor: " + oProgramacaoDiaria.StatusCor + " \n";
            oProgramacaoDiaria.DataProgramada = pGrade.Rows[pLinha].Cells["DataProgramada"].Value.ToString();
            sRet = sRet + "Data programada: " + oProgramacaoDiaria.DataProgramada + " \n";
            oProgramacaoDiaria.Data = pGrade.Rows[pLinha].Cells["Data"].Value.ToString();
            sRet = sRet + "Data: " + oProgramacaoDiaria.Data + " \n";
            oProgramacaoDiaria.Hora = pGrade.Rows[pLinha].Cells["Hora"].Value.ToString();
            sRet = sRet + "Hora: " + oProgramacaoDiaria.Hora + " \n";
            return sRet;
        }
        private void SalvarServicoFutura(int pRowIndex, DataGridView pGrade, string pNumeroMTRe = "")
        {
            // insert ServicoFutura 
            oServicosFutura = new clsServicosFutura();
            oServicosFuturaDados = new clsServicosFuturaDados();
            if (pGrade.Rows[pRowIndex].Cells["CodigoCliente"].Value.ToString() != "")
            {
                if (pGrade.Rows[pRowIndex].Cells["CodigoCaminhao"].Value.ToString() != "")
                    oServicosFutura.CodigoCaminhao = Convert.ToInt32(pGrade.Rows[pRowIndex].Cells["CodigoCaminhao"].Value.ToString());
                oServicosFutura.CodigoCliente = Convert.ToInt32(pGrade.Rows[pRowIndex].Cells["CodigoCliente"].Value.ToString());
                if (pGrade.Rows[pRowIndex].Cells["CodigoMotorista"].Value.ToString() != "")
                    oServicosFutura.CodigoMotorista = Convert.ToInt32(pGrade.Rows[pRowIndex].Cells["CodigoMotorista"].Value.ToString());
                oServicosFutura.DescricaoResiduo = pGrade.Rows[pRowIndex].Cells["ExecutarServico"].Value.ToString();
                oServicosFutura.CodigoResiduo = Convert.ToInt32(pGrade.Rows[pRowIndex].Cells["CodigoResiduo"].Value.ToString());
                if (oServicosFutura.CodigoResiduo == 0)
                {
                    clsResiduos _oRes = new clsResiduos();
                    clsResiduoDados _oResDados = new clsResiduoDados();                    
                    oServicosFutura.CodigoResiduo = _oResDados.PegaCodigoResiduo(oServicosFutura.DescricaoResiduo);
                }
                oServicosFutura.DataProgramada = pGrade.Rows[pRowIndex].Cells["DataProgramada"].Value.ToString();
                oServicosFutura.MapaMarcado = 0;
                if (pGrade.Rows[pRowIndex].Cells["Map"].Value.ToString() != "")
                    oServicosFutura.MapaMarcado = 1;
                oServicosFutura.Observacao = pGrade.Rows[pRowIndex].Cells["Observacao"].Value.ToString();
                oServicosFutura.DestinoFinal = pGrade.Rows[pRowIndex].Cells["DestinoFinal"].Value.ToString();
                oServicosFutura.ServicoAExecutar = pGrade.Rows[pRowIndex].Cells["HoraProgramada"].Value.ToString();
                oServicosFutura.Hora = pGrade.Rows[pRowIndex].Cells["Hora"].Value.ToString();
                oServicosFutura.Solicitante = pGrade.Rows[pRowIndex].Cells["Solicitante"].Value.ToString();
                if (pGrade.Rows[pRowIndex].Cells["NumeroMTRe"].Value.ToString() != "")
                    oServicosFutura.NumeroMTRe = pGrade.Rows[pRowIndex].Cells["NumeroMTRe"].Value.ToString();
                else if (pNumeroMTRe != "")
                    oServicosFutura.NumeroMTRe = pNumeroMTRe;
                if (oServicosFuturaDados.DadoExiste(oServicosFutura.CodigoCliente,
                                                    oServicosFutura.CodigoResiduo,
                                                    oServicosFutura.DataProgramada,
                                                    oServicosFutura.DescricaoResiduo, oServicosFutura.Hora) == "Incluir")
                {
                    oServicosFuturaDados.Inserir(oServicosFutura);
                }
                else
                {
                    bool bFiltrarCampoHora = false;
                    if (pGrade.Rows[pRowIndex].Cells["StatusCor"].Value.ToString() == "15000000")
                        bFiltrarCampoHora = true;
                    oServicosFuturaDados.Alterar(oServicosFutura, oServicosFutura.CodigoCliente, oServicosFutura.DataProgramada, oServicosFutura.Hora, bFiltrarCampoHora);
                }
            }
        }
        private void SalvarReprogramacaoManualFuturaAntecipacao(clsProgramacaoDiariaServicos oProgramacaoDiaria, string pStatusCor)
        {
            //Salvando na tabela de reprogramação
            clsReprogramacaoServicos oReprogramacao = new clsReprogramacaoServicos();
            oReprogramacao.AnoMesDia = oProgramacaoDiaria.AnoMesDia;
            oReprogramacao.CodigoCaminhao = oProgramacaoDiaria.CodigoCaminhao;
            oReprogramacao.CodigoCliente = oProgramacaoDiaria.CodigoCliente;
            oReprogramacao.CodigoResiduo = oProgramacaoDiaria.CodigoResiduo;
            oReprogramacao.CodigoMotorista = oProgramacaoDiaria.CodigoMotorista;
            oReprogramacao.Data = oProgramacaoDiaria.Data;
            oReprogramacao.ExecutarServico = oProgramacaoDiaria.ExecutarServico;
            oReprogramacao.DataProgramada = oProgramacaoDiaria.DataProgramada;
            oReprogramacao.Hora = oProgramacaoDiaria.Hora;
            oReprogramacao.HoraProgramada = oProgramacaoDiaria.ServicoExecutado;
            oReprogramacao.Observacao = oProgramacaoDiaria.Observacao;
            oReprogramacao.Quantidade = oProgramacaoDiaria.Quantidade;
            oReprogramacao.SequencialProgramacaoDiaria = oProgramacaoDiaria.Sequencial;
            oReprogramacao.Solicitante = oProgramacaoDiaria.Solicitante;
            oReprogramacao.StatusCor = pStatusCor;
            oReprogramacao.TipoProgramacao = oProgramacaoDiaria.TipoProgramacao;
            oReprogramacao.Unidade = oProgramacaoDiaria.Unidade;
            clsReprogramacaoDados oReprogramacaoDados = new clsReprogramacaoDados();
            oReprogramacaoDados.Inserir(oReprogramacao);
        }
        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            LinhaSelecionadaGrade2 = 0;
            if (e.Node.Level == 0)
                _ano = e.Node.Text;
            if (e.Node.Level == 1)
                _mes = e.Node.Text;
            if (e.Node.Level == 2)
                _dia = e.Node.Text;
            if (_ano != "" && _mes != "" && _dia != "" && e.Node.Level == 2)
            {
                PreencheGrades(_dia + "/" + _mes + "/" + _ano);
                btnEncerrarDia.Enabled = false;
                if (_dia + "/" + _mes + "/" + _ano == DataProgAberta.Text)
                {
                    btnEncerrarDia.Enabled = true;
                }
            }
        }
        private void frmProgramacaoDiaria_FormClosed(object sender, FormClosedEventArgs e)
        {
            oProgramacaoFechadaDados.LiberaProgramacaoParaOutroUsuario(geral.UsuarioAtual);
        }
        private void btnRotaMapa_Click(object sender, EventArgs e)
        {
            try
            {
                // programação futura
                if (Convert.ToDateTime(_dia + "/" + _mes + "/" + _ano) > Convert.ToDateTime(DataProgAberta.Text))
                {
                    string _dtAbertaBanco = geral.Left(oProgramacaoFechadaDados.DataUltimaProgAberta(), 10);
                    if (DataProgAberta.Text != _dtAbertaBanco)
                    {
                        MessageBox.Show("A programação aberta está diferente da atual. \nReinicie a Programação Diária de Serviços. ");
                    }
                    else
                    {
                        // Inicio
                        // Como é programação futura ficou muito demorado. Verificar melhor tirar até Fim - e está errado destino final
                        // Grade2.DataSource = "";
                        // Grade2.Refresh();
                        // MontaProgramacaoFutura();
                        // Fim
                    }
                }
            }
            finally
            {
                if (MapSelecionado())
                {
                    frmRotaMapa frm = new frmRotaMapa();
                    frm.pAnoMesDia = Convert.ToInt32(_ano + _mes + _dia);
                    frm.pDataProgramacaoAberta = _dia + "/" + _mes + "/" + _ano;
                    frm._dt = new DataTable();
                    frm._dt = _dtParaImprimirProgramados;
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Nenhum serviço foi selecionado!", "SILC", MessageBoxButtons.OK);
                }
            }
        }
        //private void SalvarRotaNoDataTable(string pHora, string pMap)
        //{
        //    foreach (DataRow _dr in _dtParaImprimirProgramados.Rows)
        //    {
        //        if (_dr["Hora"].ToString() == pHora)
        //        {
        //            _dr["Map"] = pMap;
        //            break;
        //        }
        //    }
        //}
        private string ProcuraMotoristaJaSelecionado()
        {
            string sRet = "";
            for (int i = 0; i < Grade2.Rows.Count - 1;i++)
            {
                if (Grade2.Rows[i].Cells["CodigoMotorista"].Value.ToString() != "0" &&
                    Grade2.Rows[i].Cells["Map"].Value.ToString() == "X")
                {
                    sRet = Grade2.Rows[i].Cells["NomeMotorista"].Value.ToString();
                    break;
                }
            }
            return sRet;
        }
        private bool MapSelecionado()
        {
            bool bRet = false;
            for (int i = 0; i < Grade2.Rows.Count; i++)
            {
                if (Grade2.Rows[i].Cells["Map"].Value.ToString().ToUpper() == "X")
                {
                    bRet = true;
                    break;
                }
            }
            return bRet;
        }
        private void Grade2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int _LinhaIndex = 0;
            if (e.RowIndex != -1)
                _LinhaIndex = e.RowIndex;
            if (e.ColumnIndex == 21 && _LinhaIndex >= 0) // rowindex pra limpar a rota tem que ser 0
            {
                // coluna do mapa rota
                if (Convert.ToDateTime(DataProgAberta.Text) > Convert.ToDateTime(_dia + "/" + _mes + "/" + _ano))
                    MessageBox.Show("Data inválida!", "SILC", MessageBoxButtons.OK);
                else if (Grade2.Rows[_LinhaIndex].Cells["StatusCor"].Value.ToString() == geral.RetornaCodigoCor("Amarelo"))
                {
                    MessageBox.Show("Cor inválida!", "SILC", MessageBoxButtons.OK);
                }
                else if (Convert.ToDateTime(Grade2.Rows[_LinhaIndex].Cells["DataProgramada"].Value.ToString()) > Convert.ToDateTime(_dia + "/" + _mes + "/" + _ano))
                {
                    MessageBox.Show("Data programada inválida!", "SILC", MessageBoxButtons.OK);
                }
                else if (Grade2.Rows[_LinhaIndex].Cells["NomeMotorista"].Value.ToString() == "" ||
                         Grade2.Rows[_LinhaIndex].Cells["NomeMotorista"].Value.ToString() == null)
                {
                    MessageBox.Show("Motorista inválido!", "SILC", MessageBoxButtons.OK);
                }
                else
                {
                    // edson - tst 27/04/2023
                    // reavalindo, pois está pegando os dados do futuro na tabela ServicosFutura - o serviço futuro continua, pois passa a ser presente quando encerra programação.
                    // não posso simplesmente gravar na tabela ProgramacaoDiariaServicos - uma solução é gravar tudo na ServicosFutura 
                    if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["Map"].Value.ToString() == "X")
                    {
                        Grade2.Rows[Grade2.CurrentRow.Index].Cells["Map"].Value = "";
                        if (Convert.ToDateTime(DataProgAberta.Text) == Convert.ToDateTime(_dia + "/" + _mes + "/" + _ano))
                        {
                            oProgramacaoDados.SalvarRotaMapa(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString(), "");
                            if (oServicosFuturaDados.DadoExiste(Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCliente"].Value.ToString()),
                                                                Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoResiduo"].Value.ToString()),
                                                                Grade2.Rows[Grade2.CurrentRow.Index].Cells["DataProgramada"].Value.ToString(), "", 
                                                                Grade2.Rows[Grade2.CurrentRow.Index].Cells["Hora"].Value.ToString())  == "Alterar")
                            {
                                SalvarServicoFutura(Grade2.CurrentRow.Index, Grade2, Grade2.Rows[Grade2.CurrentRow.Index].Cells["NumeroMTRe"].Value.ToString());
                            }
                        }
                        else if (Convert.ToDateTime(_dia + "/" + _mes + "/" + _ano) > Convert.ToDateTime(DataProgAberta.Text))
                            SalvarServicoFutura(Grade2.CurrentRow.Index, Grade2, Grade2.Rows[Grade2.CurrentRow.Index].Cells["NumeroMTRe"].Value.ToString());
                    }
                    else
                    {
                        if (Grade2.Rows[Grade2.CurrentRow.Index].Cells["Observacao"].Value.ToString().ToUpper().IndexOf("BLOQUEIO FINANCEIRO") > -1)
                            MessageBox.Show("Cliente com Bloqueio Financeiro!", "SILC", MessageBoxButtons.OK);
                        else
                        {
                            string sProcSelect = ProcuraMotoristaJaSelecionado();
                            // retirado a pedido da logística - 21/07/2020
                            if (sProcSelect != Grade2.Rows[Grade2.CurrentRow.Index].Cells["NomeMotorista"].Value.ToString() &&
                                sProcSelect != null && sProcSelect != "")
                            {
                                MessageBox.Show("Motorista inválido ou diferente do solicionado!", "SILC", MessageBoxButtons.OK);
                            }
                            else
                                Grade2.Rows[Grade2.CurrentRow.Index].Cells["Map"].Value = "X";
                        }
                        if (Convert.ToDateTime(DataProgAberta.Text) == Convert.ToDateTime(_dia + "/" + _mes + "/" + _ano))
                        { 
                            oProgramacaoDados.SalvarRotaMapa(Grade2.Rows[Grade2.CurrentRow.Index].Cells["Sequencial"].Value.ToString(),
                                                             Grade2.Rows[Grade2.CurrentRow.Index].Cells["Map"].Value.ToString());
                            if (oServicosFuturaDados.DadoExiste(Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoCliente"].Value.ToString()),
                                                                Convert.ToInt32(Grade2.Rows[Grade2.CurrentRow.Index].Cells["CodigoResiduo"].Value.ToString()),
                                                                Grade2.Rows[Grade2.CurrentRow.Index].Cells["DataProgramada"].Value.ToString(), "",
                                                                Grade2.Rows[Grade2.CurrentRow.Index].Cells["Hora"].Value.ToString()) == "Alterar")
                            {
                                SalvarServicoFutura(Grade2.CurrentRow.Index, Grade2, Grade2.Rows[Grade2.CurrentRow.Index].Cells["NumeroMTRe"].Value.ToString());
                            }
                        }
                        else if (Convert.ToDateTime(_dia + "/" + _mes + "/" + _ano) > Convert.ToDateTime(DataProgAberta.Text))
                            SalvarServicoFutura(Grade2.CurrentRow.Index, Grade2, Grade2.Rows[Grade2.CurrentRow.Index].Cells["NumeroMTRe"].Value.ToString());
                    }
                }
            }
        }
        private void Grade1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToDateTime(DataProgAberta.Text) > Convert.ToDateTime(_dia + "/" + _mes + "/" + _ano))
                MessageBox.Show("Data programação atual inválida!");
            else
            {
                // retornar a programacao - grade 2
                DialogResult dlg1 = new DialogResult();
                dlg1 = MessageBox.Show("Retornar serviço?", "Retornar serviço", MessageBoxButtons.YesNo);
                if (dlg1 == DialogResult.Yes)
                {
                    SalvarLog("Retornou serviço", "Programação de Serviços", Grade1);
                    // retornar para Grade 2
                    string sCodigoCor = "";
                    if (_dtParaImprimirExecutados.Rows[Grade1.CurrentRow.Index]["TipoProgramacao"].ToString() == "1")
                        sCodigoCor = geral.RetornaCodigoCor("Branco");
                    else if (_dtParaImprimirExecutados.Rows[Grade1.CurrentRow.Index]["TipoProgramacao"].ToString() == "2")
                        sCodigoCor = geral.RetornaCodigoCor("Cinza");
                    if (oProgramacaoDados.RetornaParaQuadro2(Grade1.Rows[Grade1.CurrentRow.Index].Cells["Sequencial"].Value.ToString(),
                                                             Grade1.Rows[Grade1.CurrentRow.Index].Cells["Hora"].Value.ToString(), sCodigoCor))
                    {
                        // retirar do quadro 1
                        for (int idx = 0; idx <= _dtParaImprimirExecutados.Rows.Count - 1; idx++)
                        {
                            DataRow dr = _dtParaImprimirExecutados.Rows[idx];
                            if (dr["Hora"].ToString() == Grade1.Rows[Grade1.CurrentRow.Index].Cells["Hora"].Value.ToString())
                            {
                                // inserir no quadro 2
                                _dtParaImprimirExecutados.Rows[Grade1.CurrentRow.Index]["StatusCor"] = sCodigoCor;
                                _dtParaImprimirExecutados.Rows[Grade1.CurrentRow.Index]["DataProgramada"] = DataProgAberta.Text;

                                DataRow dr2 = _dtParaImprimirProgramados.NewRow();
                                for (int i2 = 0; i2 <= _dtParaImprimirProgramados.Columns.Count - 1; i2++)
                                    dr2[i2] = dr[i2];
                                _dtParaImprimirProgramados.Rows.Add(dr2);

                                _dtParaImprimirExecutados.Rows.Remove(dr);
                                break;
                            }
                        }
                        Grade1.Refresh();
                        Grade2.Refresh();

                        lblLinhasGrade2.Text = (Grade1.Rows.Count).ToString() + " | " + (Grade2.Rows.Count).ToString();
                    }
                }
            }
        }
        private void btnMTRe_Click(object sender, EventArgs e)
        {
            geral.cnpj_cpf = txtCNPJ_CPF.Text;
            geral.senhamtre = txtSenhaAcessoFatma.Text;            
            frmMTReGecko45 ofrmMTRe = new frmMTReGecko45();
            ofrmMTRe.ShowDialog();
        }
        private void Grade1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void Grade1_KeyUp(object sender, KeyEventArgs e)
        {
            frmPermissao o_frmPermissao = new frmPermissao();
            if (e.KeyCode.Equals(Keys.F2) || e.KeyCode.Equals(Keys.F7) || e.KeyCode.Equals(Keys.Insert))
            {
                if (Convert.ToDateTime(_dia + "/" + _mes + "/" + _ano) >= Convert.ToDateTime(DataProgAberta.Text) && !e.KeyCode.Equals(Keys.F2))
                {
                    MessageBox.Show("Solicitação inválida!", "SILC", MessageBoxButtons.OK);
                }
                else
                {
                    o_frmPermissao.ShowDialog();
                }
            }
            if (o_frmPermissao.bPermitido)
            {
                if (e.KeyCode.Equals(Keys.F2))
                {
                    if (Grade1.Rows.Count > 0)
                    {
                        if (Grade1.Columns[Grade1.CurrentCell.ColumnIndex].HeaderText == "Cliente")
                        {
                            frmProcura frmListaClientes = new frmProcura("CLIENTEPROGRAMACAO");
                            frmListaClientes.ShowDialog();
                            if (geral.CodigoCliente > 0 && Grade1.Rows[Grade1.CurrentRow.Index].Cells["TipoProgramacao"].Value.ToString() == "2")
                            {
                                Grade1.Rows[Grade1.CurrentRow.Index].Cells["CodigoCliente"].Value = geral.CodigoCliente;
                                Grade1.Rows[Grade1.CurrentRow.Index].Cells["NomeFantasiaCliente"].Value = geral.NomeClienteFantasia;
                                SalvarLog("Alterou cliente executado", "Programação de Serviços", Grade1);
                                oProgramacaoDados.SalvarCliente(Grade1.Rows[Grade1.CurrentRow.Index].Cells["Sequencial"].Value.ToString(), geral.CodigoCliente);

                                SalvarServicoFutura(Grade1.CurrentRow.Index, Grade1, Grade1.Rows[Grade1.CurrentRow.Index].Cells["NumeroMTRe"].Value.ToString());
                            }
                        }
                        if (Grade1.Columns[Grade1.CurrentCell.ColumnIndex].HeaderText == "Resíduo")
                        {
                            frmProcura frmListaResiduos = new frmProcura("RESIDUOPROGRAMACAO");
                            frmListaResiduos.ShowDialog();
                            if (geral.CodigoResiduo > 0 && Grade1.Rows[Grade1.CurrentRow.Index].Cells["TipoProgramacao"].Value.ToString() == "2")
                            {
                                Grade1.Rows[Grade1.CurrentRow.Index].Cells["CodigoResiduo"].Value = geral.CodigoResiduo;
                                clsResiduoDados oResiduoDados = new clsResiduoDados();
                                Grade1.Rows[Grade1.CurrentRow.Index].Cells["ExecutarServico"].Value = oResiduoDados.PegaDescricao(geral.CodigoResiduo);
                                SalvarLog("Alterou resíduo executado", "Programação de Serviços", Grade1);
                                oProgramacaoDados.SalvarCodigoResiduo(geral.CodigoResiduo, Convert.ToInt32(Grade1.Rows[Grade1.CurrentRow.Index].Cells["Sequencial"].Value.ToString()));
                                SalvarServicoFutura(Grade1.CurrentRow.Index, Grade1, Grade1.Rows[Grade1.CurrentRow.Index].Cells["NumeroMTRe"].Value.ToString());
                            }
                        }
                        if (Grade1.Columns[Grade1.CurrentCell.ColumnIndex].HeaderText == "Cam") // caminhão
                        {
                            frmProcura frmLista = new frmProcura("CAMINHAOPROGRAMACAO");
                            frmLista.ShowDialog();
                            if (geral.CodigoCaminhao > 0)
                            {
                                Grade1.Rows[Grade1.CurrentRow.Index].Cells["CodigoCaminhao"].Value = geral.CodigoCaminhao;
                                oCaminhao = oCaminhaoDados.PegaDados(oCaminhao, geral.CodigoCaminhao);
                                if (oCaminhao.Modelo != "")
                                {
                                    Grade1.Rows[Grade1.CurrentRow.Index].Cells["ModeloCaminhao"].Value = oCaminhao.Modelo;
                                    SalvarLog("Alterou caminhão executado", "Programação de Serviços", Grade1);
                                    oProgramacaoDados.SalvarCaminhao(Grade1.Rows[Grade1.CurrentRow.Index].Cells["Sequencial"].Value.ToString(), geral.CodigoCaminhao);

                                    SalvarServicoFutura(Grade1.CurrentRow.Index, Grade1, Grade1.Rows[Grade1.CurrentRow.Index].Cells["NumeroMTRe"].Value.ToString());
                                }
                            }

                        }
                        if (Grade1.Columns[Grade1.CurrentCell.ColumnIndex].HeaderText == "Motorista") // Motorista
                        {
                            frmProcura frmLista = new frmProcura("MOTORISTAPROGRAMACAO");
                            frmLista.ShowDialog();
                            if (geral.CodigoMotorista > 0)
                            {
                                Grade1.Rows[Grade1.CurrentRow.Index].Cells["CodigoMotorista"].Value = geral.CodigoMotorista;

                                oMotorista = oMotoristaDados.PegaDados(oMotorista, geral.CodigoMotorista);
                                if (oMotorista.Nome != "")
                                {
                                    Grade1.Rows[Grade1.CurrentRow.Index].Cells["NomeMotorista"].Value = oMotorista.Nome;
                                    SalvarLog("Alterou motorista executado", "Programação de Serviços", Grade1);
                                    oProgramacaoDados.SalvarMotorista(Grade1.Rows[Grade1.CurrentRow.Index].Cells["Sequencial"].Value.ToString(), geral.CodigoMotorista, oMotorista.Nome);

                                    SalvarServicoFutura(Grade1.CurrentRow.Index, Grade1, Grade1.Rows[Grade1.CurrentRow.Index].Cells["NumeroMTRe"].Value.ToString());
                                }
                            }
                        }
                        if (Grade1.Columns[Grade1.CurrentCell.ColumnIndex].HeaderText == "Serviço Executado") // Serviço a executar
                        {
                            frmServicoExecutar o_frmServicoAExecutar = new frmServicoExecutar();
                            o_frmServicoAExecutar.ShowDialog();
                            if (o_frmServicoAExecutar.bSalvar)
                            {
                                Grade1.Rows[Grade1.CurrentRow.Index].Cells["HoraProgramada"].Value = o_frmServicoAExecutar.cboServicoExecutar.Text;
                                SalvarLog("Alterou serviço executado", "Programação de Serviços", Grade1);
                                oProgramacaoDados.SalvarServicoAExecutar(Grade1.Rows[Grade1.CurrentRow.Index].Cells["Sequencial"].Value.ToString(),
                                                                         o_frmServicoAExecutar.cboServicoExecutar.Text);

                                SalvarServicoFutura(Grade1.CurrentRow.Index, Grade1, Grade1.Rows[Grade1.CurrentRow.Index].Cells["NumeroMTRe"].Value.ToString());
                            }
                        }
                        if (Grade1.Columns[Grade1.CurrentCell.ColumnIndex].HeaderText == "Observacao" ||
                            Grade1.Columns[Grade1.CurrentCell.ColumnIndex].HeaderText == "Observação") // Observação
                        {
                            frmObservacao o_frmObservacao = new frmObservacao();
                            o_frmObservacao.ShowDialog();
                            if (o_frmObservacao.bSalvar)
                            {
                                Grade1.Rows[Grade1.CurrentRow.Index].Cells["Observacao"].Value = o_frmObservacao.cboTabMotivosOBS.Text;
                                SalvarLog("Alterou Obs executado", "Programação de Serviços", Grade1);
                                oProgramacaoDados.SalvarObservacao(Grade1.Rows[Grade1.CurrentRow.Index].Cells["Sequencial"].Value.ToString(),
                                                                    o_frmObservacao.cboTabMotivosOBS.Text);

                                SalvarServicoFutura(Grade1.CurrentRow.Index, Grade1, Grade1.Rows[Grade1.CurrentRow.Index].Cells["NumeroMTRe"].Value.ToString());
                            }
                        }
                        if (Grade1.Columns[Grade1.CurrentCell.ColumnIndex].HeaderText == "DestinoFinal" ||
                            Grade1.Columns[Grade1.CurrentCell.ColumnIndex].HeaderText == "DestinoFinal") // Destino Final
                        {
                            frmDestinoFinal o_frmDestinoFinal = new frmDestinoFinal();
                            if (Grade1.Rows[Grade1.CurrentRow.Index].Cells["CodigoResiduo"].Value.ToString() != "")
                                o_frmDestinoFinal.residuo1.txtCodigo.Text = Grade1.Rows[Grade1.CurrentRow.Index].Cells["CodigoResiduo"].Value.ToString();
                            o_frmDestinoFinal.ShowDialog();
                            if (o_frmDestinoFinal.bSalvar)
                            {
                                Grade1.Rows[Grade1.CurrentRow.Index].Cells["DestinoFinal"].Value = o_frmDestinoFinal.cboDestinoFinal.Text;
                                SalvarLog("Alterou destino executado", "Programação de Serviços", Grade1);
                                oProgramacaoDados.SalvarDestinoFinal(Grade1.Rows[Grade1.CurrentRow.Index].Cells["Sequencial"].Value.ToString(),
                                                                     o_frmDestinoFinal.cboDestinoFinal.Text);
                                SalvarServicoFutura(Grade1.CurrentRow.Index, Grade1, Grade1.Rows[Grade1.CurrentRow.Index].Cells["NumeroMTRe"].Value.ToString());
                            }
                        }
                    }
                }
                else if (e.KeyCode.Equals(Keys.F7))
                {
                    if (Grade1.Rows.Count > 0)
                    {
                        int _sequencial = Convert.ToInt32(Grade1.Rows[Grade1.CurrentRow.Index].Cells["Sequencial"].Value);
                        if (geral.RetornaCodigoCor("Azul") == Grade1.Rows[Grade1.CurrentRow.Index].Cells["StatusCor"].Value.ToString())
                        {
                            DialogResult dlg1 = new DialogResult();
                            dlg1 = MessageBox.Show("Cancelar serviço do cliente \n" + 
                                                   Grade1.Rows[Grade1.CurrentRow.Index].Cells["CodigoCliente"].Value + " \n" +
                                                   Grade1.Rows[Grade1.CurrentRow.Index].Cells["NomeFantasiaCliente"].Value + "\n" +
                                                   Grade1.Rows[Grade1.CurrentRow.Index].Cells["ExecutarServico"].Value + "?", "Cancela serviço", MessageBoxButtons.YesNo);
                            if (dlg1 == DialogResult.Yes)
                            {
                                SalvarLog("Cancelou serviço executado", "Programação de Serviços", Grade1);
                                oProgramacaoDados.SalvarStatusCor(_sequencial.ToString(), geral.RetornaCodigoCor("Amarelo"));

                                // retirar do quadro 1
                                for (int idx = 0; idx <= _dtParaImprimirExecutados.Rows.Count - 1; idx++)
                                {
                                    DataRow dr = _dtParaImprimirExecutados.Rows[idx];
                                    if (dr["Hora"].ToString() == Grade1.Rows[Grade1.CurrentRow.Index].Cells["Hora"].Value.ToString())
                                    {
                                        // inserir no quadro 2
                                        _dtParaImprimirExecutados.Rows[Grade1.CurrentRow.Index]["StatusCor"] = geral.RetornaCodigoCor("Amarelo");
                                        DataRow dr2 = _dtParaImprimirProgramados.NewRow();
                                        for (int i2 = 0; i2 <= _dtParaImprimirProgramados.Columns.Count - 1; i2++)
                                            dr2[i2] = dr[i2];
                                        _dtParaImprimirProgramados.Rows.Add(dr2);
                                        _dtParaImprimirExecutados.Rows.Remove(dr);
                                        break;
                                    }
                                }
                                Grade1.Refresh();
                                Grade2.Refresh();
                                lblLinhasGrade2.Text = (Grade1.Rows.Count).ToString() + " | " + (Grade2.Rows.Count).ToString();
                            }
                        }

                    }
                }
                else if (e.KeyCode.Equals(Keys.Insert))
                {
                    frmInsertProgramacao o_frmInsertProgramacao = new frmInsertProgramacao();
                    o_frmInsertProgramacao.oProgramacaoDiaria.AnoMesDia = Convert.ToInt32(_ano + _mes + _dia);
                    o_frmInsertProgramacao.dtpData.Text = _ano + "/" + _mes + "/" + _dia;
                    o_frmInsertProgramacao.dtpDataProgramada.Text = _ano + "/" + _mes + "/" + _dia;
                    o_frmInsertProgramacao.dtpDataProgramada.Enabled = false;
                    o_frmInsertProgramacao.txtHora.Text = DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00");
                    o_frmInsertProgramacao.lblStatus.Text = geral.RetornaCodigoCor("Azul");
                    o_frmInsertProgramacao.pInsercaoEmProgramacaoFechada = true;
                    o_frmInsertProgramacao.ShowDialog();

                    if (geral.VoltaForm.IndexOf("ProgramaçãoInserida") >= 0)
                    {
                        DataRow dr2 = _dtParaImprimirExecutados.NewRow();
                        dr2["AnoMesDia"] = Convert.ToInt32(_ano + _mes + _dia);
                        dr2["Data"] = o_frmInsertProgramacao.dtpData.Text;
                        dr2["DataProgramada"] = o_frmInsertProgramacao.dtpDataProgramada.Text;
                        dr2["CodigoCliente"] = o_frmInsertProgramacao.cliente1.txtCodigo.Text;
                        dr2["NomeFantasiaCliente"] = o_frmInsertProgramacao.cliente1.txtDescricao.Text;
                        dr2["Hora"] = o_frmInsertProgramacao.txtHora.Text;
                        dr2["CodigoResiduo"] = o_frmInsertProgramacao.residuo1.txtCodigo.Text;
                        dr2["ExecutarServico"] = o_frmInsertProgramacao.residuo1.txtDescricao.Text;
                        dr2["HoraProgramada"] = o_frmInsertProgramacao.txtServicoAExecutar.Text;
                        dr2["Solicitante"] = o_frmInsertProgramacao.txtSolicitante.Text;
                        dr2["StatusCor"] = geral.RetornaCodigoCor("Cinza");
                        dr2["Sequencial"] = o_frmInsertProgramacao.lblNrSequencial.Text;
                        dr2["NomeMotorista"] = o_frmInsertProgramacao.funcionario1.txtDescricao.Text;
                        dr2["ModeloCaminhao"] = o_frmInsertProgramacao.caminhao1.txtDescricao.Text;
                        dr2["TipoProgramacao"] = "M";
                        dr2["Observacao"] = o_frmInsertProgramacao.cboTabMotivosOBS.Text;
                        dr2["DestinoFinal"] = o_frmInsertProgramacao.cboDestinoFinal.Text;
                        if (o_frmInsertProgramacao.intQuantidade.VALOR.Text != "")
                            dr2["Quantidade"] = o_frmInsertProgramacao.intQuantidade.VALOR.Text;
                        clsEnderecosDados oEnderecoDados = new clsEnderecosDados();
                        DataTable _dtEnderecos = new DataTable();
                        _dtEnderecos = oEnderecoDados.PreencheDataTableEnderecosClientes(o_frmInsertProgramacao.cliente1.txtCodigo.Text, 2);

                        DataTable _dtParticularidadeContrato = new DataTable();
                        clsContratoResiduosDados oContratoDados = new clsContratoResiduosDados();
                        _dtParticularidadeContrato = oContratoDados.RetornaParticularidade();

                        DataRow[] _drEnd = _dtEnderecos.Select("Codigo = " + o_frmInsertProgramacao.cliente1.txtCodigo.Text);
                        if (_drEnd.Length > 0)
                            dr2["CidadeBairroEndereco"] = _drEnd[0]["CidadeBairroEndereco"].ToString();

                        DataRow[] _drPart = _dtParticularidadeContrato.Select("CodigoCliente = " + o_frmInsertProgramacao.cliente1.txtCodigo.Text + " and CodigoResiduo = " + o_frmInsertProgramacao.residuo1.txtCodigo.Text);
                        if (_drPart.Length > 0)
                            dr2["Particularidade"] = _drPart[0]["Particularidade"].ToString();
                        _dtParaImprimirExecutados.Rows.Add(dr2);

                    }
                    o_frmInsertProgramacao.Close();
                    Grade1.Refresh();
                    lblLinhasGrade2.Text = (Grade1.Rows.Count).ToString() + " | " + (Grade2.Rows.Count).ToString();
                }
            }
        }
        private void ckbOcultarServicoRealizado_Click(object sender, EventArgs e)
        {
            if (ckbOcultarServicoRealizado.Checked)
            {
                OcultarServicoRealizado();
            }
            else
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    btnEncerrarDia.Enabled = false;
                    if (_dia + "/" + _mes + "/" + _ano == DataProgAberta.Text)
                    {
                        btnEncerrarDia.Enabled = true;
                        PreencheGrades(_dia + "/" + _mes + "/" + _ano);
                    }
                    else if (_dia + "/" + _mes + "/" + _ano != DataProgAberta.Text)
                    {
                        // programação futura
                        if (Convert.ToDateTime(_dia + "/" + _mes + "/" + _ano) > Convert.ToDateTime(DataProgAberta.Text))
                        {
                            string _dtAbertaBanco = geral.Left(oProgramacaoFechadaDados.DataUltimaProgAberta(), 10);
                            if (DataProgAberta.Text != _dtAbertaBanco)
                            {
                                MessageBox.Show("A programação aberta está diferente da atual. \nReinicie a Programação Diária de Serviços. ");
                            }
                            else
                                MontaProgramacaoFutura();
                        }
                        else
                            PreencheGrades(_dia + "/" + _mes + "/" + _ano);
                    }
                    else
                        PreencheGrades(_dia + "/" + _mes + "/" + _ano);

                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }
        private void btnPesquisaCliente_Click(object sender, EventArgs e)
        {
            frmProcura frmListaClientes = new frmProcura("CLIENTEPROGRAMACAO");
            frmListaClientes.ShowDialog();
            clsClientes oCliente = new clsClientes();
            clsClienteDados oClienteDados = new clsClienteDados();
            oClienteDados.PegaDados(oCliente, geral.CodigoCliente);
            if (oCliente.Codigo > 0)
            {
                lblCodigoCliente.Text = oCliente.Codigo.ToString("000000");
                cdnmCliente.Text = oCliente.NomeFantasia;
                txtCNPJ_CPF.Text = oCliente.CNPJ_CPF;
                txtSenhaAcessoFatma.Text = oCliente.SenhaAcessoFatima;
            }
        }
        private void butAbrir_Click(object sender, EventArgs e)
        {
            string _Url = "";
            if (txtNumeroMTRe.Text != "" && txtNumeroMTRe.Text != "0")
            {
                if (!System.IO.File.Exists("\\\\servidor\\WinSILC\\temp\\" + txtNumeroMTRe.Text + ".pdf"))
                {
                    _Url = "http://mtr.ima.sc.gov.br/ControllerServlet?acao=relatorio&nomeRelatorio=manifesto&manifesto=" + txtNumeroMTRe.Text + "&condicao=N";
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(_Url, "\\\\servidor\\WinSILC\\temp\\" + txtNumeroMTRe.Text + ".pdf");
                    }
                }
                if (System.IO.File.Exists("\\\\servidor\\WinSILC\\temp\\" + txtNumeroMTRe.Text + ".pdf"))
                {
                    System.Diagnostics.Process.Start("\\\\servidor\\WinSILC\\temp\\" + txtNumeroMTRe.Text + ".pdf");
                }
            }
            else
            {
                MessageBox.Show("Número manifesto inválido!");
            }
        }
        private void btnMTRe2_Click(object sender, EventArgs e)
        {
            clsResiduos oRes = new clsResiduos();
            clsResiduoDados oResDados = new clsResiduoDados();
            if (LinhaSelecionadaGrade1 > 0)
            {
                if (Grade1.Rows[LinhaSelecionadaGrade1].Cells["CodigoResiduo"].Value.ToString() != "")
                {
                    oResDados.PegaDados(oRes, Convert.ToInt32(Grade1.Rows[LinhaSelecionadaGrade1].Cells["CodigoResiduo"].Value));
                }
                if (Grade1.Rows[LinhaSelecionadaGrade1].Cells["CodigoMotorista"].Value.ToString() == "" ||
                    Grade1.Rows[LinhaSelecionadaGrade1].Cells["CodigoMotorista"].Value.ToString() == "0") // não emitir MTR-e serviço sem o Motorista
                {
                    MessageBox.Show("Não emitir MTR-e sem Motorista!");
                }
                else if (Grade1.Rows[LinhaSelecionadaGrade1].Cells["CodigoCaminhao"].Value.ToString() == "" ||
                    Grade1.Rows[LinhaSelecionadaGrade1].Cells["CodigoCaminhao"].Value.ToString() == "0") // não emitir MTR-e serviço sem o Caminhao
                {
                    MessageBox.Show("Não emitir MTR-e sem Caminhão!");
                }
                else if (Grade1.Rows[LinhaSelecionadaGrade1].Cells["StatusCor"].Value.ToString() == geral.RetornaCodigoCor("Amarelo")) // não emitir MTR-e serviço cancelado
                {
                    MessageBox.Show("Não emitir MTR-e para Serviço Cancelado!");
                }
                else if (Grade1.Rows[LinhaSelecionadaGrade1].Cells["HoraProgramada"].Value.ToString().IndexOf("COLOCAR") > -1) // quando for COLOCAR não emitir MTR-e
                {
                    MessageBox.Show("Na colocação não emitir MTR-e!");
                }
                else if (txtSenhaAcessoFatma.Text.ToUpper() == "CLIENTE" || txtSenhaAcessoFatma.Text.ToUpper() == "") // quando for sem senha o cliente emite
                {
                    MessageBox.Show("O Cliente Emite a MTR-e!");
                }
                else if (oRes.EhServico == 1)
                {
                    MessageBox.Show("Não emitimos MTR-e de Serviços!");
                }
                else if ((txtNumeroMTRe.Text == "" || txtNumeroMTRe.Text == "0") && LinhaSelecionadaGrade1 >= 0)
                {
                    frmPesquisaModeloMTRe ofrmPesquisaModeloMTRe = new frmPesquisaModeloMTRe();
                    if (Grade1.Rows[LinhaSelecionadaGrade1].Cells["CodigoResiduo"].Value.ToString() != "")
                        ofrmPesquisaModeloMTRe.pCodigoResiduo = Convert.ToInt32(Grade1.Rows[LinhaSelecionadaGrade1].Cells["CodigoResiduo"].Value);
                    ofrmPesquisaModeloMTRe.pCNPJ_CPFGerador = txtCNPJ_CPF.Text;
                    ofrmPesquisaModeloMTRe.pLoginClienteIMA = txtCNPJ_CPF.Text;
                    ofrmPesquisaModeloMTRe.pSenhaClienteIMA = txtSenhaAcessoFatma.Text;
                    if (Grade1.Rows[LinhaSelecionadaGrade1].Cells["CodigoCliente"].Value.ToString() != "")
                        ofrmPesquisaModeloMTRe.pCodigoCliente = Convert.ToInt32(Grade1.Rows[LinhaSelecionadaGrade1].Cells["CodigoCliente"].Value);
                    if (Grade1.Rows[LinhaSelecionadaGrade1].Cells["CodigoCaminhao"].Value.ToString() != "")
                        ofrmPesquisaModeloMTRe.pCodigoCaminhao = Convert.ToInt32(Grade1.Rows[LinhaSelecionadaGrade1].Cells["CodigoCaminhao"].Value);
                    if (Grade1.Rows[LinhaSelecionadaGrade1].Cells["CodigoMotorista"].Value.ToString() != "")
                        ofrmPesquisaModeloMTRe.pCodigoMotorista = Convert.ToInt32(Grade1.Rows[LinhaSelecionadaGrade1].Cells["CodigoMotorista"].Value);
                    ofrmPesquisaModeloMTRe.pDataProgramada = Convert.ToDateTime(Grade1.Rows[LinhaSelecionadaGrade1].Cells["DataProgramada"].Value).ToString("dd/MM/yyyy");
                    ofrmPesquisaModeloMTRe.pCNPJDestinador = ""; 
                    ofrmPesquisaModeloMTRe.pGrade = Grade1;
                    ofrmPesquisaModeloMTRe.pRowIndex = LinhaSelecionadaGrade1;
                    ofrmPesquisaModeloMTRe.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Número da MTR eletrônica já existe (Grade1) ou nenhuma linha selecionada!");
                }
            }
            else if (LinhaSelecionadaGrade2 > 0) // Grade 2 - em baixo
            {
                if (Grade2.Rows[LinhaSelecionadaGrade2].Cells["CodigoResiduo"].Value.ToString() != "")
                {
                    oResDados.PegaDados(oRes, Convert.ToInt32(Grade2.Rows[LinhaSelecionadaGrade2].Cells["CodigoResiduo"].Value));
                }

                if (Grade2.Rows[LinhaSelecionadaGrade2].Cells["CodigoMotorista"].Value.ToString() == "" ||
                    Grade2.Rows[LinhaSelecionadaGrade2].Cells["CodigoMotorista"].Value.ToString() == "0") // não emitir MTR-e serviço sem o Motorista
                {
                    MessageBox.Show("Não emitir MTR-e sem Motorista!");
                }
                else if (Grade2.Rows[LinhaSelecionadaGrade2].Cells["CodigoCaminhao"].Value.ToString() == "" ||
                    Grade2.Rows[LinhaSelecionadaGrade2].Cells["CodigoCaminhao"].Value.ToString() == "0") // não emitir MTR-e serviço sem o Caminhao
                {
                    MessageBox.Show("Não emitir MTR-e sem Caminhão!");
                }
                else if (Grade2.Rows[LinhaSelecionadaGrade2].Cells["StatusCor"].Value.ToString() == geral.RetornaCodigoCor("Amarelo")) // não emitir MTR-e serviço cancelado
                {
                    MessageBox.Show("Não emitir MTR-e para Serviço Cancelado!");
                }
                else if (Grade2.Rows[LinhaSelecionadaGrade2].Cells["HoraProgramada"].Value.ToString().IndexOf("COLOCAR") > -1) // quando for COLOCAR não emitir MTR-e
                {
                    MessageBox.Show("Na colocação não emitir MTR-e!");
                }
                else if (txtSenhaAcessoFatma.Text.ToUpper() == "CLIENTE" || txtSenhaAcessoFatma.Text.ToUpper() == "") // quando for sem senha o cliente emite
                {
                    MessageBox.Show("O Cliente Emite a MTR-e!");
                }
                else if (oRes.EhServico == 1)
                {
                    MessageBox.Show("Não emitimos MTR-e de Serviços!");
                }
                else if ((txtNumeroMTRe.Text == "" || txtNumeroMTRe.Text == "0") && LinhaSelecionadaGrade2 >= 0)
                {
                    frmPesquisaModeloMTRe ofrmPesquisaModeloMTRe = new frmPesquisaModeloMTRe();
                    if (Grade2.Rows[LinhaSelecionadaGrade2].Cells["CodigoResiduo"].Value.ToString() != "")
                        ofrmPesquisaModeloMTRe.pCodigoResiduo = Convert.ToInt32(Grade2.Rows[LinhaSelecionadaGrade2].Cells["CodigoResiduo"].Value);
                    ofrmPesquisaModeloMTRe.pCNPJ_CPFGerador = txtCNPJ_CPF.Text;
                    ofrmPesquisaModeloMTRe.pLoginClienteIMA = txtCNPJ_CPF.Text;
                    ofrmPesquisaModeloMTRe.pSenhaClienteIMA = txtSenhaAcessoFatma.Text;
                    if (Grade2.Rows[LinhaSelecionadaGrade2].Cells["CodigoCliente"].Value.ToString() != "")
                        ofrmPesquisaModeloMTRe.pCodigoCliente = Convert.ToInt32(Grade2.Rows[LinhaSelecionadaGrade2].Cells["CodigoCliente"].Value);
                    if (Grade2.Rows[LinhaSelecionadaGrade2].Cells["CodigoCaminhao"].Value.ToString() != "")
                        ofrmPesquisaModeloMTRe.pCodigoCaminhao = Convert.ToInt32(Grade2.Rows[LinhaSelecionadaGrade2].Cells["CodigoCaminhao"].Value);
                    if (Grade2.Rows[LinhaSelecionadaGrade2].Cells["CodigoMotorista"].Value.ToString() != "")
                        ofrmPesquisaModeloMTRe.pCodigoMotorista = Convert.ToInt32(Grade2.Rows[LinhaSelecionadaGrade2].Cells["CodigoMotorista"].Value);
                    ofrmPesquisaModeloMTRe.pDataProgramada = Convert.ToDateTime(Grade2.Rows[LinhaSelecionadaGrade2].Cells["DataProgramada"].Value).ToString("dd/MM/yyyy");
                    ofrmPesquisaModeloMTRe.pCNPJDestinador = ""; // oDestinoFinalDados.PegaCNPJ(Grade2.Rows[LinhaSelecionadaGrade2].Cells["DestinoFinal"].Value.ToString().Replace("DTR-", "").Replace("DTR ", ""));
                    ofrmPesquisaModeloMTRe.pGrade = Grade2;
                    ofrmPesquisaModeloMTRe.pRowIndex = LinhaSelecionadaGrade2;
                    ofrmPesquisaModeloMTRe.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Número da MTR eletrônica já existe (Grade2) ou nenhuma linha selecionada!");
                }
            }
        }
        private void EncerrouProgramacaoEmOutraEstacao()
        {
            clsProgramacaoFechadaDados oProgFechadaDados = new clsProgramacaoFechadaDados();
            if (geral.Left(oProgFechadaDados.DataUltimaProgAberta(), 10) != DataProgAberta.Text)
            {
                MessageBox.Show("A programação foi encerrada em outra estação e a data da programação aberta não é mais a mesma. Preciso fechar.", "Refresh", MessageBoxButtons.OK);
                this.Close();
            }
        }

        private void Grade2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LinhaSelecionadaGrade1 = 0;
            LinhaSelecionadaGrade2 = e.RowIndex;
            // coluna da rota mapa, não funciona, por causa do dbclick para acioná-la
            // 
            DataGridView d;
            d = (DataGridView)sender;
            if (e.RowIndex == -1)
            {
                // quando for igual a -1 é por que, foi clicado no titulo do grid, ou seja, é somente ordenação
                // deixar passar sem fazer nada pois é só ordenação
            }
            else if (e.ColumnIndex == 21 && d.Columns[21].Name != "Map")
            {
                MessageBox.Show("Coluna da Rota está errado falar com desenvolvedor! " + d.Columns[21].Name);
            }
            else if (e.ColumnIndex != 21 && Grade2.Rows[e.RowIndex].Cells["StatusCor"].Value.ToString().IndexOf("Aquamarine") > -1)
            {
                string xtst = "064444";
            }
            else if (e.ColumnIndex != 21) 
            {
                txtNumeroMTRe.Text = "";

                // verifica se está encerrando a programação em outra terminal.
                EncerrouProgramacaoEmOutraEstacao();

                // dar refresh da linha
                RefreshDaLinhaDaGrade2(e.RowIndex);

                // Grade2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Aqua;
                clsClientes oClientes = new clsClientes();
                clsClienteDados oClienteDados = new clsClienteDados();
                if (e.RowIndex > -1)
                {
                    oClienteDados.PegaDados(oClientes, Convert.ToInt32(Grade2.Rows[e.RowIndex].Cells["CodigoCliente"].Value));
                    if (Grade2.Rows[e.RowIndex].Cells["CodigoCliente"].Value.ToString() != "")
                    {
                        LinhaSelecionadaGrade2 = e.RowIndex;
                        geral.DataProgramada = Convert.ToDateTime(Grade2.Rows[e.RowIndex].Cells["DataProgramada"].Value).ToString("dd/MM/yyyy");
                        if (Grade2.Rows[e.RowIndex].Cells["CodigoMotorista"].Value.ToString() != "")
                            geral.CodigoMotorista = Convert.ToInt32(Grade2.Rows[e.RowIndex].Cells["CodigoMotorista"].Value);
                        if (Grade2.Rows[e.RowIndex].Cells["CodigoCaminhao"].Value.ToString() != "")
                            geral.CodigoCaminhao = Convert.ToInt32(Grade2.Rows[e.RowIndex].Cells["CodigoCaminhao"].Value);
                        geral.CodigoCliente = oClientes.Codigo;
                        lblCodigoCliente.Text = oClientes.Codigo.ToString("000000");
                        cdnmCliente.Text = oClientes.NomeFantasia;
                        txtCNPJ_CPF.Text = oClientes.CNPJ_CPF;
                        txtSenhaAcessoFatma.Text = oClientes.SenhaAcessoFatima;
                        if (Grade2.Rows[e.RowIndex].Cells["NumeroMTRe"].Value.ToString() != "")
                            txtNumeroMTRe.Text = Grade2.Rows[e.RowIndex].Cells["NumeroMTRe"].Value.ToString();
                    }
                }
            }
        }
        private void RefreshDaLinhaDaGrade2(int pLinha)
        {
            if (pLinha >= 0)
            {
                // quando clicar pegar dados da tabela ProgramacaoDiariaServicos e se houver dados na Tabela ServicosFutura - sobrepor.

                int _cdCliente = 0;
                int _icdResiduo = 0;

                if (Grade2.Rows[pLinha].Cells["CodigoCliente"].Value.ToString() != "")
                    _cdCliente = Convert.ToInt32(Grade2.Rows[pLinha].Cells["CodigoCliente"].Value.ToString());
                if (Grade2.Rows[pLinha].Cells["CodigoResiduo"].Value.ToString() != "")
                {
                    _icdResiduo = Convert.ToInt32(Grade2.Rows[pLinha].Cells["CodigoResiduo"].Value.ToString());
                }
                if (_icdResiduo == 0)
                {
                    clsResiduoDados oResDados = new clsResiduoDados();
                    _icdResiduo = oResDados.PegaCodigoResiduo(Grade2.Rows[pLinha].Cells["ExecutarServico"].Value.ToString());
                }
                if (_cdCliente > 0)
                {
                    DataTable _ldt = new DataTable();
                    // verificar se existe ainda diferenças na Tabela ServicosFutura                        
                    // caso não existir, onde _ldt.Rows.Count == 0 - atribuir dados da tabela programacaodiariaservicos

                    DateTime dataMostrada = Convert.ToDateTime(_dia + "/" + _mes + "/" + _ano);
                    string _sDiferentes = "";

                    oServicosFutura = new clsServicosFutura();
                    _ldt = oServicosFuturaDados.GetDados(oServicosFutura, _cdCliente, _icdResiduo, Grade2.Rows[pLinha].Cells["Hora"].Value.ToString(), geral.Left(Grade2.Rows[pLinha].Cells["DataProgramada"].Value.ToString(), 10));
                    //if (_ldt.Rows.Count == 0 && dataMostrada > DataProgAberta.Value) //anterior de 13/03/2024.
                    //if (_ldt.Rows.Count == 0 && dataMostrada >= DataProgAberta.Value)  //novo - em teste - não deu certo
                    if (_ldt.Rows.Count == 0 && dataMostrada > DataProgAberta.Value) //mais novo 21/03/2024 em teste
                    {
                        string _DataReprogramada = "";
                        _DataReprogramada = oReprogramacao.GetDataReprogramacao(Grade2.Rows[pLinha].Cells["AnoMesDia"].Value.ToString(), _cdCliente, _icdResiduo.ToString(), Grade2.Rows[pLinha].Cells["Hora"].Value.ToString());
                        if (_DataReprogramada == "")
                            _DataReprogramada = geral.Left(Grade2.Rows[pLinha].Cells["DataProgramada"].Value.ToString(), 10);
                        oServicosFutura = new clsServicosFutura();
                         _ldt = oServicosFuturaDados.GetDados(oServicosFutura, _cdCliente, _icdResiduo, "", _DataReprogramada);
                    }

                    if (dataMostrada == DataProgAberta.Value && _ldt.Rows.Count == 0)
                    {
                        SILCNegocios.clsProgramacaoDiariaServicos oPrg = new clsProgramacaoDiariaServicos();
                        oProgramacaoDados.PegaDados(oPrg, Convert.ToInt32(Grade2.Rows[pLinha].Cells["Sequencial"].Value.ToString()), 0, 0);

                        if (Grade2.Rows[pLinha].Cells["Solicitante"].Value.ToString() != oPrg.Solicitante)
                        {
                            _sDiferentes = _sDiferentes + "Solicitante diferente! \n";
                            if (_ldt.Rows.Count == 0)
                                Grade2.Rows[pLinha].Cells["Solicitante"].Value = oPrg.Solicitante;
                        }
                        if (Grade2.Rows[pLinha].Cells["CodigoCliente"].Value.ToString() != oPrg.CodigoCliente.ToString())
                        {
                            _sDiferentes = _sDiferentes + "Codigo Cliente diferente! \n";
                            if (_ldt.Rows.Count == 0)
                                Grade2.Rows[pLinha].Cells["CodigoCliente"].Value = oPrg.CodigoCliente.ToString();
                        }
                        clsClienteDados oClienteDados = new clsClienteDados();
                        oPrg.NomeCliente = oClienteDados.PegaNomeFantasia(Convert.ToInt32(Grade2.Rows[pLinha].Cells["CodigoCliente"].Value.ToString()));
                        if (Grade2.Rows[pLinha].Cells["NomeFantasiaCliente"].Value.ToString() != oPrg.NomeCliente)
                        {
                            _sDiferentes = _sDiferentes + "Nome Fantasia Cliente diferente! \n";
                            if (_ldt.Rows.Count == 0)
                                Grade2.Rows[pLinha].Cells["NomeFantasiaCliente"].Value = oPrg.NomeCliente;
                        }
                        if (geral.Left(Grade2.Rows[pLinha].Cells["DataProgramada"].Value.ToString(), 10) != oPrg.DataProgramada)
                        {
                            _sDiferentes = _sDiferentes + "Data Programada diferente! \n";
                            if (_ldt.Rows.Count == 0)
                            {
                                Grade2.Rows[pLinha].Cells["DataProgramada"].Value = oPrg.DataProgramada;
                            }
                        }
                        if (Grade2.Rows[pLinha].Cells["HoraProgramada"].Value.ToString() != oPrg.ServicoExecutado)
                        {
                            _sDiferentes = _sDiferentes + "Executar Servico diferente! \n";
                            if (_ldt.Rows.Count == 0)
                                Grade2.Rows[pLinha].Cells["HoraProgramada"].Value = oPrg.ServicoExecutado;
                        }
                        if (Grade2.Rows[pLinha].Cells["CodigoCaminhao"].Value.ToString() != oPrg.CodigoCaminhao.ToString())
                        {
                            if (oPrg.CodigoCaminhao > 0)
                            {
                                _sDiferentes = _sDiferentes + "Codigo Caminhão diferente! \n";
                                if (_ldt.Rows.Count == 0)
                                    Grade2.Rows[pLinha].Cells["CodigoCaminhao"].Value = oPrg.CodigoCaminhao.ToString();
                            }
                        }
                        if (Grade2.Rows[pLinha].Cells["ModeloCaminhao"].Value.ToString() != oPrg.ModeloCaminhao)
                        {
                            if (oPrg.ModeloCaminhao != "")
                            {
                                _sDiferentes = _sDiferentes + "Caminhão diferente! \n";
                                if (_ldt.Rows.Count == 0)
                                    Grade2.Rows[pLinha].Cells["ModeloCaminhao"].Value = oPrg.ModeloCaminhao;
                            }
                        }
                        if (Grade2.Rows[pLinha].Cells["CodigoMotorista"].Value.ToString() != oPrg.CodigoMotorista.ToString())
                        {
                            if (oPrg.CodigoMotorista > 0)
                            {
                                _sDiferentes = _sDiferentes + "Codigo Motorista diferente! \n";
                                if (_ldt.Rows.Count == 0)
                                    Grade2.Rows[pLinha].Cells["CodigoMotorista"].Value = oPrg.CodigoMotorista.ToString();
                            }
                        }
                        if (Grade2.Rows[pLinha].Cells["NomeMotorista"].Value.ToString() != oPrg.NomeMotorista)
                        {
                            if (oPrg.NomeMotorista != "")
                            {
                                _sDiferentes = _sDiferentes + "Motorista diferente! \n";
                                if (_ldt.Rows.Count == 0)
                                    Grade2.Rows[pLinha].Cells["NomeMotorista"].Value = oPrg.NomeMotorista;
                            }
                        }

                        clsResiduos oRes = new clsResiduos();
                        clsResiduoDados oResDados = new clsResiduoDados();
                        oResDados.PegaDados(oRes, _icdResiduo);
                        if (_ldt.Rows.Count == 0 && Grade2.Rows[pLinha].Cells["DestinoFinal"].Value.ToString() == "")
                        {
                            if (oPrg.DestinoFinal == "")
                                Grade2.Rows[pLinha].Cells["DestinoFinal"].Value = oRes.DescricaoDestinoFinal;
                            else
                                Grade2.Rows[pLinha].Cells["DestinoFinal"].Value = oPrg.DestinoFinal;
                        }
                        else if (_ldt.Rows.Count == 0 && Grade2.Rows[pLinha].Cells["DestinoFinal"].Value.ToString() != oPrg.DestinoFinal && oPrg.DestinoFinal != "")
                            Grade2.Rows[pLinha].Cells["DestinoFinal"].Value = oPrg.DestinoFinal;

                        if (Grade2.Rows[pLinha].Cells["Map"].Value.ToString() != oPrg.RotaMapa)
                        {
                            _sDiferentes = _sDiferentes + "Rota Mapa diferente! \n";
                            if (_ldt.Rows.Count == 0) 
                                Grade2.Rows[pLinha].Cells["Map"].Value = oPrg.RotaMapa;
                        }
                        if (Grade2.Rows[pLinha].Cells["Observacao"].Value.ToString() != oPrg.Observacao)
                        {
                            _sDiferentes = _sDiferentes + "Observacao diferente! \n";
                            if (_ldt.Rows.Count == 0)
                                Grade2.Rows[pLinha].Cells["Observacao"].Value = oPrg.Observacao;
                        }
                        if (Grade2.Rows[pLinha].Cells["StatusCor"].Value.ToString() != oPrg.StatusCor)
                        {
                            _sDiferentes = _sDiferentes + "StatusCor diferente! \n";
                            if (oPrg.StatusCor == "oPrg.StatusCor")
                            {
                                // tirar da grade2 e enviar pra grade1, pois o serviço foi fechado.

                                Grade2.Rows.RemoveAt(pLinha);
                            }
                            else
                            {
                                Grade2.Rows[pLinha].Cells["StatusCor"].Value = oPrg.StatusCor;
                                for (int iCr = 0; iCr < Grade2.Columns.Count; iCr++)
                                {
                                    Grade2.Rows[pLinha].Cells[iCr].Style.ForeColor = Color.Black;
                                }
                            }
                        }
                        if (_sDiferentes != "" && geral.UsuarioAtual == "teixeira")
                        {
                            // quando é programação diária pode ter registro na tabela servicosfuturo ou não
                            MessageBox.Show(_sDiferentes + " mensagem só pra mim. Aqui pode ter diferença, pois é da tabela programacaodiariaservicos 1ª tabela (" + geral.UsuarioAtual + ")");
                        }
                    }
                    if (_ldt.Rows.Count > 0 && oServicosFutura.DataProgramada != null && oServicosFutura.NomeCliente != null)
                    {
                        _sDiferentes = "";

                        if (Grade2.Rows[pLinha].Cells["Solicitante"].Value.ToString() != oServicosFutura.Solicitante)
                            _sDiferentes = _sDiferentes + "Solicitante- diferente(2)! \n";

                        if (geral.Left(Grade2.Rows[pLinha].Cells["DataProgramada"].Value.ToString(), 10) != oServicosFutura.DataProgramada)
                        {
                            _sDiferentes = _sDiferentes + "Data Programada diferente(2)! \n";

                            if (Convert.ToDateTime(oServicosFutura.DataProgramada) > Convert.ToDateTime(geral.Left(Grade2.Rows[pLinha].Cells["DataProgramada"].Value.ToString(), 10)))
                            {
                                Grade2.Rows[pLinha].Cells["StatusCor"].Value = geral.RetornaCodigoCor("Vermelho");
                                for (int iCr = 0; iCr < Grade2.Columns.Count; iCr++)
                                {
                                    Grade2.Rows[pLinha].Cells[iCr].Style.ForeColor = Color.White;
                                }
                            }
                            else
                            {
                                Grade2.Rows[pLinha].Cells["StatusCor"].Value = geral.RetornaCodigoCor("AzulClaro");
                                for (int iCr = 0; iCr < Grade2.Columns.Count; iCr++)
                                {
                                    Grade2.Rows[pLinha].Cells[iCr].Style.ForeColor = Color.White;
                                }
                            }
                        }

                        if (Grade2.Rows[pLinha].Cells["HoraProgramada"].Value.ToString() != oServicosFutura.ServicoAExecutar)
                            _sDiferentes = _sDiferentes + "Serviço a Executar diferente(2)! \n";

                        if (Grade2.Rows[pLinha].Cells["CodigoCaminhao"].Value.ToString() != oServicosFutura.CodigoCaminhao.ToString())
                            _sDiferentes = _sDiferentes + "Codigo Caminhão diferente(2)! \n";

                        if (Grade2.Rows[pLinha].Cells["ModeloCaminhao"].Value.ToString() != oServicosFutura.ModeloCaminhao)
                            _sDiferentes = _sDiferentes + "Caminhão diferente(2)! \n";

                        if (Grade2.Rows[pLinha].Cells["CodigoMotorista"].Value.ToString() != oServicosFutura.CodigoMotorista.ToString())
                            _sDiferentes = _sDiferentes + "Codigo Motorista diferente(2)! \n";

                        if (Grade2.Rows[pLinha].Cells["NomeMotorista"].Value.ToString() != oServicosFutura.NomeMotorista)
                            _sDiferentes = _sDiferentes + "Motorista diferente(2)! \n";

                        if (Grade2.Rows[pLinha].Cells["DestinoFinal"].Value.ToString() != oServicosFutura.DestinoFinal)
                        {
                            _sDiferentes = _sDiferentes + "Destino Final diferente(2)! \n";
                            Grade2.Rows[pLinha].Cells["DestinoFinal"].Value = oServicosFutura.DestinoFinal;
                        }
                        if (Grade2.Rows[pLinha].Cells["Observacao"].Value.ToString() != oServicosFutura.Observacao)
                        {
                            _sDiferentes = _sDiferentes + "Observacao diferente(2)! \n";
                            Grade2.Rows[pLinha].Cells["Observacao"].Value = oServicosFutura.Observacao;
                        }
                        if (Grade2.Rows[pLinha].Cells["StatusCor"].Value.ToString() == geral.RetornaCodigoCor("Azul"))
                        {
                            // insere no quadro 1
                            foreach (DataRow dr in _dtParaImprimirProgramados.Rows)
                            {
                                if (dr["Sequencial"].ToString() == Grade2.Rows[pLinha].Cells["Sequencial"].Value.ToString() &&
                                    dr["Hora"].ToString() == Grade2.Rows[pLinha].Cells["Hora"].Value.ToString())
                                {

                                    _dtParaImprimirProgramados.Rows[pLinha]["StatusCor"] = geral.RetornaCodigoCor("Azul");
                                    _dtParaImprimirProgramados.Rows[pLinha]["DataProgramada"] = DataProgAberta.Text;

                                    DataRow dr2 = _dtParaImprimirExecutados.NewRow();
                                    for (int i2 = 0; i2 <= _dtParaImprimirExecutados.Columns.Count - 1; i2++)
                                        dr2[i2] = dr[i2];

                                    _dtParaImprimirExecutados.Rows.Add(dr2);

                                    _dtParaImprimirProgramados.Rows.Remove(dr);

                                    break;

                                }
                            }
                            Grade1.Refresh();
                            Grade2.Refresh();
                        }
                        // quando a rota marcada estiver diferente tem que gravar
                        bool bSalvaMap = false;

                        // com rota marcada
                        if (Grade2.Rows[pLinha].Cells["Map"].Value.ToString() == "X" &&
                            oServicosFutura.MapaMarcado != 1)
                        {
                            _sDiferentes = _sDiferentes + "Rota Mapa diferente(2)! \n";
                            bSalvaMap = true;
                        }
                        // sem rota marcada
                        if (Grade2.Rows[pLinha].Cells["Map"].Value.ToString() == "" &&
                            oServicosFutura.MapaMarcado == 1)
                        {
                            _sDiferentes = _sDiferentes + "Rota Mapa diferente(2)! \n";
                            
                            bSalvaMap = true;
                        }
                        Grade2.Rows[pLinha].Cells["NumeroMTRe"].Value = oServicosFutura.NumeroMTRe;
                        if (_sDiferentes != "")
                        {

                            if (geral.UsuarioAtual == "teixeira")
                                MessageBox.Show(_sDiferentes);

                            Grade2.Rows[pLinha].Cells["Solicitante"].Value = oServicosFutura.Solicitante;
                            
                            // dataprogramada não pode ser alterada na tabela ServicosFutura - pois está tabela é para os campos complementares
                            // Grade2.Rows[pLinha].Cells["DataProgramada"].Value = oServicosFutura.DataProgramada;

                            Grade2.Rows[pLinha].Cells["HoraProgramada"].Value = oServicosFutura.ServicoAExecutar;
                            Grade2.Rows[pLinha].Cells["CodigoCaminhao"].Value = oServicosFutura.CodigoCaminhao;
                            Grade2.Rows[pLinha].Cells["ModeloCaminhao"].Value = oServicosFutura.ModeloCaminhao;
                            Grade2.Rows[pLinha].Cells["CodigoMotorista"].Value = oServicosFutura.CodigoMotorista;
                            Grade2.Rows[pLinha].Cells["NomeMotorista"].Value = oServicosFutura.NomeMotorista;
                            Grade2.Rows[pLinha].Cells["DestinoFinal"].Value = oServicosFutura.DestinoFinal;
                            if (oServicosFutura.MapaMarcado == 1)
                                Grade2.Rows[pLinha].Cells["Map"].Value = "X";
                            else
                                Grade2.Rows[pLinha].Cells["Map"].Value = "";
                            Grade2.Rows[pLinha].Cells["Observacao"].Value = oServicosFutura.Observacao;

                            if (bSalvaMap)
                                SalvarServicoFutura(pLinha, Grade2, Grade2.Rows[Grade2.CurrentRow.Index].Cells["NumeroMTRe"].Value.ToString());
                        }
                    }
                }
            }
        }

        private void Grade2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 21)
            {
                DialogResult dlg1 = new DialogResult();
                dlg1 = MessageBox.Show("Deseja Limpar todos os Maps?", "Confirmação", MessageBoxButtons.YesNo);
                if (dlg1 == DialogResult.Yes)
                {
                    foreach (DataRow _dr in _dtParaImprimirProgramados.Rows)
                    {
                        _dr["Map"] = "";
                    }
                    Grade2.Refresh();

                    if (DataProgAberta.Value == Convert.ToDateTime(_dia + "/" + _mes + "/" + _ano))
                        oProgramacaoDados.LimparMapa(_ano + _mes + _dia);

                    oServicosFuturaDados.LimparMapa(_ano + "-" + _mes + "-" + _dia);

                }
            }
        }

        private void Grade1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EncerrouProgramacaoEmOutraEstacao();
            LinhaSelecionadaGrade2 = 0;
            LinhaSelecionadaGrade1 = e.RowIndex;
            clsClientes oClientes = new clsClientes();
            clsClienteDados oClienteDados = new clsClienteDados();
            if (e.RowIndex > -1)
            {
                oClienteDados.PegaDados(oClientes, Convert.ToInt32(Grade1.Rows[e.RowIndex].Cells["CodigoCliente"].Value));
                if (Grade1.Rows[e.RowIndex].Cells["CodigoCliente"].Value.ToString() != "")
                {
                    geral.CodigoCliente = oClientes.Codigo;
                    geral.DataProgramada = Convert.ToDateTime(Grade1.Rows[e.RowIndex].Cells["DataProgramada"].Value).ToString("dd/MM/yyyy");
                    if (Grade1.Rows[e.RowIndex].Cells["CodigoMotorista"].Value.ToString() != "")
                        geral.CodigoMotorista = Convert.ToInt32(Grade1.Rows[e.RowIndex].Cells["CodigoMotorista"].Value);
                    if (Grade1.Rows[e.RowIndex].Cells["CodigoCaminhao"].Value.ToString() != "")
                        geral.CodigoCaminhao = Convert.ToInt32(Grade1.Rows[e.RowIndex].Cells["CodigoCaminhao"].Value);
                    lblCodigoCliente.Text = oClientes.Codigo.ToString("000000");
                    cdnmCliente.Text = oClientes.NomeFantasia;
                    txtCNPJ_CPF.Text = oClientes.CNPJ_CPF;
                    txtSenhaAcessoFatma.Text = oClientes.SenhaAcessoFatima;
                    txtNumeroMTRe.Text = Grade1.Rows[e.RowIndex].Cells["NumeroMTRe"].Value.ToString();

                    clsLancamentosDados oLancDados = new clsLancamentosDados();
                    string _numerolancamento = oLancDados.PegaNumeroLancamento(Convert.ToInt32(Grade1.Rows[e.RowIndex].Cells["Sequencial"].Value));
                    if (_numerolancamento != "" && _numerolancamento != "0")
                    {
                        clsLancamentoMTRDados oLancMTRDados = new clsLancamentoMTRDados();
                        txtNumeroMTRe.Text = oLancMTRDados.PegaNumeroMTRe(Convert.ToInt64(_numerolancamento), Convert.ToInt32(Grade1.Rows[e.RowIndex].Cells["CodigoResiduo"].Value)).ToString();
                    }
                }
            }
        }
    }
}