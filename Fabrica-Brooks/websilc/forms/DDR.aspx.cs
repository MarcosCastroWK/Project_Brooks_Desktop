using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;
using System.Drawing;

public partial class DDR : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    private DataTable _dtDestinadorFinal = new DataTable();
    clsLancamentoMTRDados oLancamentosMTRDados = new clsLancamentoMTRDados();
    clsUsuarios oUsuario = new clsUsuarios();
    clsClientes oCliente = new clsClientes();
    clsClienteDados oClienteDados = new clsClienteDados();
    clsDestinoFinal oDestino = new clsDestinoFinal();
    clsDestinoFinalDados oDestinoDados = new clsDestinoFinalDados();
    clsEnderecos oEndereco = new clsEnderecos();
    clsEnderecosDados oEnderecoDados = new clsEnderecosDados();
    clsResiduoDados oResiduoDados = new clsResiduoDados();
    clsResiduos oResiduo = new clsResiduos();
    clsBloqFinanceiroDados oBloqueioFinanceiroDados = new clsBloqFinanceiroDados();
    clsCDFe oCDFe = new clsCDFe();
    clsCDFeDados oCDFeDados = new clsCDFeDados();

    DataTable _dt = new DataTable();

    string rt = "";
    decimal SubTotalGerado = 0;
    decimal SubTotalAmazendado = 0;
    decimal SubTotalDestinado = 0;
    decimal SubTotalCDFe = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        oUsuario = (clsUsuarios)Session["oUsuario"];
        clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
        if (oUsuario != null)
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "22");
        if (oItensMenuPermissoes.Consultar == 0 && oUsuario.Codigo > 0)
            Response.Redirect("sempermissao.aspx");

        if (!IsPostBack)
        {
            txtCodigoCliente.Text = "";
            if (Session["CodigoCliente"] != null)
                txtCodigoCliente.Text = Session["CodigoCliente"].ToString();
            if (Session["oUsuario"] != null)
                txtCodigoCliente.Text = geral.RetiraLetras(((clsUsuarios)Session["oUsuario"]).Nome.ToUpper());

            GradeMTR.AutoGenerateColumns = false;
            rt = Request.QueryString["Codigo"];
            if (rt == null || rt == "")
                rt = txtCodigoCliente.Text;

            txtDataInicial.Data = DateTime.Now.ToString("01/MM/yyyy");
            txtDataFinal.Data = Convert.ToDateTime(txtDataInicial.Data).AddDays(-1).ToString("dd/MM/yyyy");
            txtDataInicial.Data = Convert.ToDateTime(txtDataInicial.Data).AddDays(-1).ToString("01/MM/yyyy");
            if (rt != null && rt != "")
            {
                if (rt.Length > 0)
                {
                    if (Convert.ToInt32(rt) > 0 )
                        MostraDadosCliente(Convert.ToInt32(rt));
                    SubTotalGerado = 0;
                    SubTotalAmazendado = 0;
                    SubTotalDestinado = 0;
                    SubTotalCDFe = 0;
                }
            }
            else
            {
                Response.Redirect("brooks/loginaplicativo.aspx", true);
            }
        }
    }

    private void MostraDadosCliente(int pCodigo)
    {
        oCliente = new clsClientes();
        oCliente = oClienteDados.PegaDados(oCliente, pCodigo);
        txtCodigoCliente.Text = oCliente.Codigo.ToString("000000");
        lblNomeCliente.Text = oCliente.Nome;
        lblCNPJ.Text = " - CNPJ/CPF: " + oCliente.CNPJ_CPF;

        lblEndereco.Text = "";
        if (!EmConferenciaOuEventual())
        {
            oEndereco = new clsEnderecos();
            oEnderecoDados = new clsEnderecosDados();
            oEndereco = oEnderecoDados.PegaDados(oEndereco, oCliente.Codigo, 2, 0);
            lblEndereco.Text = lblEndereco.Text + oEndereco.endereco;
            if (oEndereco.Bairro != "")
                lblEndereco.Text = lblEndereco.Text + " - Bairro: " + oEndereco.Bairro;
            if (oEndereco.CEP != "")
                lblEndereco.Text = lblEndereco.Text + " - CEP: " + oEndereco.CEP;
            clsMunicipios oMunicipio = new clsMunicipios();
            clsMunicipiosDados oMunicipioDados = new clsMunicipiosDados();
            if (oEndereco.CodigoMunicipio > 0)
            {
                oMunicipio = oMunicipioDados.PegaDados(oMunicipio, oEndereco.CodigoMunicipio);
                lblEndereco.Text = lblEndereco.Text + " - Cidade: " + oMunicipio.Nome + " - Estado: " + oMunicipio.UF;
            }
        }
        if (oBloqueioFinanceiroDados.ExisteBloqueio(oCliente.Codigo) && Request.QueryString["BROOKS"] != "BRO000935")
        {
            lblEndereco.Text = "Documento indisponível. Favor entrar em Contato com financeiro@brooksambiental.com.br ou telefone: (48)-3344-1515 \n ";
            lblEndereco.Font.Size = 12;

            GradeMTR.Visible = false;
            lblEnderecoT.Visible = false;
            GradeDestinador.Visible = false;
            lblCidadeEmpresa.Visible = false;
            lblDataEmissao.Visible = false;
            lblDadosLAO.Font.Size = 12;
            lblDadosLAO.Text = "";
            lblDestinador.Visible = false;
            lblDeclaracao.Visible = false;
            lblDeclaracao1.Visible = false;
            lblDeclaracao2.Visible = false;
            Image2.Visible = false;
        }
    }

    private bool EmConferenciaOuEventual()
    {
        lblDadosLAO.Text = " Informações do(s) Resíduo(s)";

        clsDDR_Conferencia oDDRConf = new clsDDR_Conferencia();

        bool bConferencia = false;
        bool bMesLiberado = false;
        if (Convert.ToDateTime(txtDataFinal.Data).Month >= DateTime.Now.Month && Convert.ToDateTime(txtDataFinal.Data).Year >= DateTime.Now.Year)
        {
            bMesLiberado = true;
            bConferencia = true;
            if (Request.QueryString["BROOKS"] != "BRO000935")
            {
                lblEndereco.Text = "Relatório disponível no início do mês subseqüênte!";
                //return false;
            }
        }
        else
            bMesLiberado = oDDRConf.MesLiberado(oCliente.Codigo, Convert.ToDateTime(txtDataFinal.Data).Month, Convert.ToDateTime(txtDataFinal.Data).Year);

        clsDocumentacaoAplicavelDados oDocAplic = new clsDocumentacaoAplicavelDados();

        if (oDocAplic.ConferirDDRAteDia(oCliente.Codigo) > 0 && !bMesLiberado)
            bConferencia = true;

        string rtBROOKS = Request.QueryString["BROOKS"];
        if (rtBROOKS == "BRO000935")
        {
            bConferencia = false;
            lblTitulo.Text = "DDR - EM CONFERÊNCIA!";
            txtCodigoCliente.Enabled = true;
            chkTotalResiduo.Visible = true;
            lblTotalResiduo.Visible = true;
            GradeMTR.Columns[17].Visible = true;
            if (chkTotalResiduo.Checked)
            {
                lblTitulo.Text = "DDR - DECLARAÇÃO DE DESTINAÇÃO DE RESÍDUOS";
                GradeMTR.Columns[17].Visible = false;
            }
        }
        else
        {
            lblTitulo.Text = "DDR - DECLARAÇÃO DE DESTINAÇÃO DE RESÍDUOS";
            txtCodigoCliente.Enabled = false;
            chkTotalResiduo.Visible = false;
            GradeMTR.Columns[17].Visible = false;
            lblTotalResiduo.Visible = false;
        }
        if ((rtBROOKS != "BRO000935" && oCliente.TiposDeContrato == 1) || (rtBROOKS != "BRO000935" && bConferencia))
        {
            if (oCliente.TiposDeContrato == 1 && !bMesLiberado && rtBROOKS != "BRO000935")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>window.open('../forms/DDR_Indisponivel.aspx');</script>");
                lblEndereco.Text = "Dados indisponíveis para este perfil de cadastro. Solicite informações através do link abaixo. \n ";
                lblEndereco.Font.Size = 12;
                //btnMontaDDR.Visible = false;
                GradeMTR.Visible = false;
                lblEnderecoT.Visible = false;
                GradeDestinador.Visible = false;
                lblCidadeEmpresa.Visible = false;
                lblDataEmissao.Visible = false;
                lblDadosLAO.Font.Size = 12;
                lblDadosLAO.Text = "<a href='http://www.brooksambiental.com.br/contato'>www.brooksambiental.com.br/contato</a>";
                lblDestinador.Visible = false;
                lblDeclaracao.Visible = false;
                lblDeclaracao1.Visible = false;
                lblDeclaracao2.Visible = false;
                Image2.Visible = false;
            }
            if (bConferencia)
            {
                lblEndereco.Text = "<center>EM CONFERÊNCIA!</center>";
                lblEndereco.Font.Size = 16;
            }
        }
        return bConferencia;
    }

    private static void MessageBox(Page _page, string Message)
    {
        _page.ClientScript.RegisterStartupScript
        (
            _page.GetType(),
            "MessageBox",
            "<script language='javascript'>alert('" + Message + "');</script>"
        );
    }
    private void MontaDDR()
    {        
        /*if (Convert.ToDateTime(txtDataFinal.Data).Month >= DateTime.Now.Month && Convert.ToDateTime(txtDataFinal.Data).Year >= DateTime.Now.Year && Request.QueryString["BROOKS"] != "BRO000935")
        {
            lblEndereco.Text = "Relatório disponível no início do mês subseqüênte!";
            lblEnderecoT.Visible = false;
            GradeMTR.Visible = false;
            lblEnderecoT.Visible = false;
            GradeDestinador.Visible = false;
            lblCidadeEmpresa.Visible = false;
            lblDataEmissao.Visible = false;
            lblDestinador.Visible = false;
            lblDeclaracao.Visible = false;
            lblDeclaracao1.Visible = false;
            lblDeclaracao2.Visible = false;
            Image2.Visible = false;
            _dt.Clear();
            GradeMTR.DataSource = _dt;
            GradeMTR.DataBind();
            GradeDestinador.DataSource = _dt;
            GradeDestinador.DataBind();
        }
        else
        */
        //{
            lblEndereco.Font.Size = 9;
            lblEnderecoT.Font.Size = 9;
            lblEnderecoT.Visible = true;
            GradeMTR.Visible = true;
            lblEnderecoT.Visible = true;
            GradeDestinador.Visible = true;
            lblCidadeEmpresa.Visible = true;
            lblDataEmissao.Visible = true;
            lblDadosLAO.Font.Size = 9;
            lblDestinador.Visible = true;
            lblDeclaracao.Visible = true;
            lblDeclaracao1.Visible = true;
            lblDeclaracao2.Visible = true;
            lblDadosLAO.Text = "Informações do(s) Resíduo(s)";
            if (txtCodigoCliente.Text != "" && txtCodigoCliente.Text != "0")
            {
                MostraDadosCliente(Convert.ToInt32(txtCodigoCliente.Text));
                
                string mesExtenso = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(Convert.ToInt16(DateTime.Now.ToString("MM"))).ToLower();
                lblDataEmissao.Text = DateTime.Now.ToString("dd") + " de " + mesExtenso[0].ToString().ToUpper() + mesExtenso.Substring(1) + " de " + DateTime.Now.ToString("yyyy") + ".";

                clsLicencaAmbientalDados oLAO = new clsLicencaAmbientalDados();
                _dtDestinadorFinal = oLAO.PreencheDataTableLicencaAmbiental("Nome");
                lblLAOs.Text = "";
                foreach (DataRow _drLAOsBROOKS in _dtDestinadorFinal.Rows)
                {
                    if (_drLAOsBROOKS["CodigoAterro"].ToString() == "7")
                        lblLAOs.Text = lblLAOs.Text + _drLAOsBROOKS["NumeroLicenca"].ToString().Replace("LAO", "") + ", ";
                }
                string rtBROOKS = Request.QueryString["BROOKS"];
                if (rtBROOKS == "BRO000935" && !chkTotalResiduo.Checked)
                    _dt = oLancamentosMTRDados.PreencheDataTableDDROrdem("CodigoGrupoResiduo, CodigoResiduo, DescricaoResiduo asc, Codigo, NumeroMTRFatima", 0,
                                                                          Convert.ToInt32(txtCodigoCliente.Text), txtDataInicial.Data, txtDataFinal.Data, true);
                else
                    _dt = oLancamentosMTRDados.PreencheDataTableDDROrdem("CodigoGrupoResiduo, CodigoResiduo, DescricaoResiduo asc, Codigo, NumeroMTRFatima", 0,
                                                                          Convert.ToInt32(txtCodigoCliente.Text), txtDataInicial.Data, txtDataFinal.Data, false);
                SubTotalGerado = 0;
                SubTotalAmazendado = 0;
                SubTotalDestinado = 0;
                SubTotalCDFe = 0;
                int xQtCDF = 0;
                bool bCDFeNumeroBranco = false;
                if (_dt.Rows.Count > 0)
                {
                    string CodigoGrupoResiduoAnterior = "";
                    string CodigoResiduoAnterior = _dt.Rows[0]["CodigoResiduo"].ToString();
                    string DescricaoResiduoAnterior = _dt.Rows[0]["DescricaoResiduo"].ToString();
                    string repete;
                    int iConta = 0;
                    foreach (DataRow _dr in _dt.Rows)
                    {
                        if (_dr["CDFe"].ToString().IndexOf("-") > -1)
                            _dr["CDFe"] = "";

                        if (_dr["QtdeCDF"].ToString() != "" && _dr["QtdeCDF"].ToString() != "0,00")
                            xQtCDF++;
                        if (_dr["CodigoGrupoResiduo"].ToString() != CodigoGrupoResiduoAnterior)
                        {
                            iConta++;
                            oResiduo = oResiduoDados.PegaDados(oResiduo, Convert.ToInt32(_dr["CodigoGrupoResiduo"].ToString()));
                            repete = new string(' ', 100 - oResiduo.DescricaoReduzida.Length);
                            _dr["DescricaoGrupo"] = iConta.ToString() + "-" + oResiduo.DescricaoReduzida.Trim();
                        }
                        if (_dr["CodigoResiduo"].ToString() != CodigoResiduoAnterior)
                        {

                            DescricaoResiduoAnterior = _dr["DescricaoResiduo"].ToString();
                            SubTotalGerado = 0;
                            SubTotalAmazendado = 0;
                            SubTotalDestinado = 0;
                        }
                        CodigoGrupoResiduoAnterior = _dr["CodigoGrupoResiduo"].ToString();
                        CodigoResiduoAnterior = _dr["CodigoResiduo"].ToString();
                        if (_dr["Unidade"].ToString() != "")
                        {
                            if (_dr["Unidade"].ToString().ToUpper() == "CX")
                            {
                                _dr["Unidade"] = "M3";
                                if (_dr["Capacidade"].ToString() != "")
                                {
                                    _dr["QtdeColetada"] = Convert.ToDecimal(_dr["QtdeColetada"]) * Convert.ToDecimal(_dr["Capacidade"]) * Convert.ToDecimal(_dr["M3PorTon"]) * 1000;
                                    _dr["QtdeDestinada"] = Convert.ToDecimal(_dr["QtdeDestinada"]) * Convert.ToDecimal(_dr["Capacidade"]) * Convert.ToDecimal(_dr["M3PorTon"]) * 1000;
                                    _dr["QtdeDTR"] = Convert.ToDecimal(_dr["QtdeDTR"]) * Convert.ToDecimal(_dr["Capacidade"]) * Convert.ToDecimal(_dr["M3PorTon"]) * 1000;
                                    _dr["Unidade"] = "KG";
                                }
                                else
                                {
                                    _dr["QtdeColetada"] = 0;
                                    _dr["QtdeDestinada"] = 0;
                                    _dr["QtdeDTR"] = 0;
                                }
                            }
                            else if (_dr["Unidade"].ToString().ToUpper() == "M3")
                            {
                                _dr["QtdeColetada"] = Convert.ToDecimal(_dr["QtdeColetada"]) * Convert.ToDecimal(_dr["M3PorTon"]) * 1000;
                                _dr["QtdeDestinada"] = Convert.ToDecimal(_dr["QtdeDestinada"]) * Convert.ToDecimal(_dr["M3PorTon"]) * 1000;
                                _dr["QtdeDTR"] = Convert.ToDecimal(_dr["QtdeDTR"]) * Convert.ToDecimal(_dr["M3PorTon"]) * 1000;
                                _dr["Unidade"] = "KG";
                            }
                        }
                        SubTotalGerado = SubTotalGerado + Convert.ToDecimal(_dr["QtdeColetada"]);
                        SubTotalAmazendado = SubTotalAmazendado + Convert.ToDecimal(_dr["QtdeDTR"]);
                        SubTotalDestinado = SubTotalDestinado + Convert.ToDecimal(_dr["QtdeDestinada"]);

                        if (_dr["NumeroCDFe"].ToString() == "" || _dr["NumeroCDFe"].ToString() == "&nbsp;")
                            bCDFeNumeroBranco = true;
                    }
                }

                GradeMTR.Columns[13].Visible = true;

                if (xQtCDF == 0)
                    GradeMTR.Columns[13].Visible = false;

                rtBROOKS = Request.QueryString["BROOKS"];
                
                //if (bCDFeNumeroBranco && rtBROOKS != "BRO000935")
                    //GradeMTR.Columns[11].Visible = false; // coluna tecnologia aplicada - sempre tem que aparecer

                GradeMTR.Columns[18].Visible = true; // situacao
                GradeMTR.Columns[6].Visible = false;
                GradeMTR.DataSource = _dt;
                GradeMTR.DataBind();
                GradeMTR.Columns[18].Visible = false; // situacao

                int xxx = 0;
                foreach (DataRow _drDF in _dtDestinadorFinal.Rows)
                {
                    if (_drDF["CodigoAterro"].ToString() != "")
                    {
                        if (_drDF["CodigoAterro"].ToString() == "2")
                            xxx = 0;
                        DataRow[] _dr = _dt.Select("CodigoDestinoFinal = " + _drDF["CodigoAterro"].ToString());
                         if (_dr.Count() == 0)
                            _drDF.Delete();
                    }
                }

                GradeDestinador.DataSource = _dtDestinadorFinal;
                GradeDestinador.DataBind();
            }
        //}
        Image2.Visible = false;
        if (GradeMTR.Rows.Count > 1)
            Image2.Visible = true;

    }
    protected void btnMontaDDR_Click(object sender, EventArgs e)
    {
        SubTotalGerado = 0;
        SubTotalAmazendado = 0;
        SubTotalDestinado = 0;
        SubTotalCDFe = 0;
        string rtBROOKS = Request.QueryString["BROOKS"];
        if (Convert.ToDateTime(txtDataInicial.Data).Year <= 2017 || 
           (Convert.ToDateTime(txtDataInicial.Data).Year == 2018 &&
            Convert.ToDateTime(txtDataInicial.Data).Month <= 8 && rtBROOKS != "BRO000935"))
        {
            MessageBox(this.Page, "Período inicial não permitido. Apenas a partir de 09/2018.");
        }
        else
        {
            if (btnMontaDDR.Text == "Ok")
                MontaDDR();
            if (txtDataInicial.Visible)
            {
                lblPeriodoDesejado.Text = Convert.ToDateTime(txtDataInicial.Data).ToShortDateString() + " a " + Convert.ToDateTime(txtDataFinal.Data).ToShortDateString();
                txtDataInicial.Visible = false;
                txtDataFinal.Visible = false;
                lblPeriodoDesejado.ForeColor = Color.Black;
                btnMontaDDR.Text = "Novo Período";
            }
            else
            {
                lblPeriodoDesejado.Text = "Selecione período desejado: ";
                txtDataInicial.Visible = true;
                txtDataFinal.Visible = true;
                lblPeriodoDesejado.ForeColor = Color.IndianRed;
                btnMontaDDR.Text = "Ok";
            }
        }
    }

    protected void GradeMTR_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            if (e.Row.Cells[3].Text != "" && e.Row.Cells[4].Text != "" && e.Row.Cells[3].Text != "&nbsp;" && e.Row.Cells[4].Text != "&nbsp;" &&
                Request.QueryString["BROOKS"] == "BRO000935" && !e.Row.Cells[1].Text.ToUpper().Contains("TOTAL") && !e.Row.Cells[4].Text.ToUpper().Contains("TOTAL"))
            {
                oCDFe = new clsCDFe();
                oCDFeDados = new clsCDFeDados();
                
                if (oCDFeDados.ExisteMTReComCodigoIbamaDiferente(e.Row.Cells[4].Text))
                {
                    e.Row.Cells[3].BackColor = Color.Yellow;
                    //e.Row.Cells[3].ForeColor = Color.White;
                }
            }

            if (e.Row.Cells[12].Text.IndexOf("DTR") == 0)
            {
                e.Row.Cells[8].Text = (0).ToString("n2");  // Destinada
                e.Row.Cells[9].Text = ""; //Data retirada
                /*
                // quando em DTR mostra destino final habitualmente usado que está no cadastro de resíduo
                if (!e.Row.Cells[1].Text.ToUpper().Contains("TOTAL") && !e.Row.Cells[4].Text.ToUpper().Contains("TOTAL"))
                {
                    oResiduo = new clsResiduos();
                    int _CodigoResiduo = oResiduoDados.PegaCodigoResiduoDescricaoSemAcento(e.Row.Cells[1].Text);
                    oResiduoDados.PegaDados(oResiduo, _CodigoResiduo);
                    e.Row.Cells[12].Text = oResiduo.CodigoDestinoFinal.ToString() + "-" + oResiduo.DescricaoDestinoFinal;
                }
                */
            }
            else
            {
                e.Row.Cells[7].Text = (0).ToString("n2");  // armazenada
            }
            if (e.Row.Cells[17].Text != "" && e.Row.Cells[17].Text != "&nbsp;")
                SubTotalCDFe = SubTotalCDFe + Convert.ToDecimal(e.Row.Cells[17].Text);

            if (e.Row.Cells[13].Text == "0" || e.Row.Cells[13].Text.IndexOf("-") > 0 || e.Row.Cells[13].Text == "&nbsp;")
                e.Row.Cells[13].Text = "";
            else
            {
                string _lnk = "";
                _lnk = _lnk + "<span style='cursor: pointer; color: blue; text-decoration: underline;' onclick=_linkCDFe(";
                _lnk = _lnk + e.Row.Cells[13].Text + ")";
                _lnk = _lnk + ">" + e.Row.Cells[13].Text + "</span>";
                e.Row.Cells[13].Text = _lnk;

            }
            if (e.Row.Cells[3].Text == "0")
                e.Row.Cells[3].Text = "";
            if (e.Row.Cells[4].Text == "0")
                e.Row.Cells[4].Text = "";
            else
            {
                string _lnk0 = "";
                _lnk0 = _lnk0 + "<span style='cursor: pointer; color: blue; text-decoration: underline;' onclick=_linkMTRe(";
                _lnk0 = _lnk0 + e.Row.Cells[4].Text + ")";
                _lnk0 = _lnk0 + ">" + e.Row.Cells[4].Text + "</span>";
                e.Row.Cells[4].Text = _lnk0;
            }

            if (!e.Row.Cells[1].Text.ToUpper().Contains("TOTAL") && !e.Row.Cells[4].Text.ToUpper().Contains("TOTAL"))
            {
                for (int i = 0; i <= 17; i++)
                {
                    e.Row.Cells[i].BorderWidth = 1; // colocou a linha abaixo
                    e.Row.Cells[i].BorderColor = Color.DimGray;
                }
                SubTotalAmazendado = SubTotalAmazendado + Convert.ToDecimal(e.Row.Cells[7].Text);
                SubTotalDestinado = SubTotalDestinado + Convert.ToDecimal(e.Row.Cells[8].Text);
                e.Row.Cells[17].Text = e.Row.Cells[18].Text;
                e.Row.Cells[17].Font.Size = 5;
            }
            else if (e.Row.Cells[1].Text.ToUpper().Contains("TOTAL") || e.Row.Cells[4].Text.ToUpper().Contains("TOTAL"))
            {
                if (e.Row.Cells[1].Text.ToUpper().Contains("TOTAL"))
                {
                    e.Row.Cells[1].Text = "Total resíduo";
                    e.Row.Cells[1].Font.Bold = true;
                    e.Row.Cells[1].ForeColor = Color.DimGray;
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                }
                else if (e.Row.Cells[4].Text.ToUpper().Contains("TOTAL"))
                {
                    e.Row.Cells[3].Text = "";
                    e.Row.Cells[4].Text = "Total MTR-e";
                    e.Row.Cells[4].Font.Bold = true;
                    e.Row.Cells[4].ForeColor = Color.DimGray;
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                }
                //e.Row.Cells[6].ForeColor = Color.DimGray;
                e.Row.Cells[7].ForeColor = Color.DimGray;
                e.Row.Cells[8].ForeColor = Color.DimGray;
                e.Row.Cells[0].BorderWidth = 1; // colocou a linha abaixo
                e.Row.Cells[0].BorderColor = Color.DimGray;
                e.Row.Cells[5].BorderWidth = 1; // colocou a linha abaixo
                e.Row.Cells[5].BorderColor = Color.DimGray;
                //e.Row.Cells[6].BorderWidth = 1; // colocou a linha abaixo
                //e.Row.Cells[6].BorderColor = Color.DimGray;
                e.Row.Cells[7].BorderWidth = 1; // colocou a linha abaixo
                e.Row.Cells[7].BorderColor = Color.DimGray;
                e.Row.Cells[8].BorderWidth = 1; // colocou a linha abaixo
                e.Row.Cells[8].BorderColor = Color.DimGray;
                e.Row.Cells[13].BorderWidth = 1; // colocou a linha abaixo
                e.Row.Cells[13].BorderColor = Color.DimGray;
                e.Row.Cells[16].BorderWidth = 1; // colocou a linha abaixo
                e.Row.Cells[16].BorderColor = Color.DimGray;
                e.Row.Cells[17].BorderWidth = 1; // colocou a linha abaixo
                e.Row.Cells[17].BorderColor = Color.DimGray;

                //e.Row.Cells[6].Font.Bold = true;
                e.Row.Cells[7].Font.Bold = true;
                e.Row.Cells[8].Font.Bold = true;
                //e.Row.Cells[6].Text = SubTotalGerado.ToString("N2");
                e.Row.Cells[7].Text = SubTotalAmazendado.ToString("N2");
                e.Row.Cells[8].Text = SubTotalDestinado.ToString("N2");
                //e.Row.Cells[17].Text = SubTotalCDFe.ToString("N2");
                e.Row.Cells[12].Text = "";
                //e.Row.Cells[14].Text = "";
                //e.Row.Cells[14].BackColor = Color.White;
                
                //e.Row.CssClass = "sembordadireita";
                //e.Row.Cells[8].BorderWidth = 0; // tirou a linha abaixo
                //e.Row.Cells[8].BorderStyle = BorderStyle.None;
                //e.Row.BorderStyle = BorderStyle.None;
                //e.Row.BorderWidth = 0;

                //GradeMTR.Columns[8].ItemStyle.BorderWidth = 0; // coluna inteira

                SubTotalGerado = 0;
                SubTotalAmazendado = 0;
                SubTotalDestinado = 0;
                SubTotalCDFe = 0;
                string rtBROOKS = Request.QueryString["BROOKS"];
                if (e.Row.Cells[17].Text != "" && e.Row.Cells[17].Text != "&nbsp;")
                {
                    // tem que ser diferente do destinado pra mudar cor
                    if (e.Row.Cells[8].Text == "" || e.Row.Cells[8].Text == "0,00" || e.Row.Cells[8].Text == "&nbsp;")
                    {
                        if (e.Row.Cells[8].Text != Convert.ToDecimal(e.Row.Cells[17].Text).ToString("N2"))
                        {
                            if (rtBROOKS == "BRO000935")
                            {
                                e.Row.Cells[17].BackColor = Color.IndianRed;
                                e.Row.Cells[17].ForeColor = Color.White;
                                e.Row.Cells[17].Font.Bold = true;
                            }
                        }
                    }
                    else  // usar qt coletada qdo não haver qt destinada
                    {
                        if (e.Row.Cells[8].Text != Convert.ToDecimal(e.Row.Cells[17].Text).ToString("N2"))
                        {
                            if (rtBROOKS == "BRO000935")
                            {
                                e.Row.Cells[17].BackColor = Color.IndianRed;
                                e.Row.Cells[17].ForeColor = Color.White;
                                e.Row.Cells[17].Font.Bold = true;
                            }
                        }
                    }
                }
            }
            if (e.Row.Cells[13].Text == "" || e.Row.Cells[13].Text == "&nbsp;")
            {
                if (!e.Row.Cells[1].Text.ToUpper().Contains("TOTAL") && !e.Row.Cells[4].Text.ToUpper().Contains("TOTAL") && 
                    Request.QueryString["BROOKS"] == "BRO000935" && !chkTotalResiduo.Checked)
                {
                    e.Row.Cells[13].Text = "";
                    e.Row.Cells[13].BackColor = Color.Yellow;
                    e.Row.Cells[13].ForeColor = Color.White;
                    e.Row.Cells[13].Font.Bold = true;                    
                }
            }

            string[] _CodigoAterro;
            _CodigoAterro = e.Row.Cells[12].Text.Split("-"[0]);
            if (_CodigoAterro.Length > 0)
            {
                if (_CodigoAterro[0].IndexOf("DTR") == 0)
                {
                    e.Row.Cells[12].Text = "";
                }
                else
                {
                    oDestino.Nome = "";
                    if (_CodigoAterro[0] != "" && _CodigoAterro[0] != "&nbsp;")
                    {
                        DataRow[] _dr = _dtDestinadorFinal.Select("CodigoAterro = " + _CodigoAterro[0]);
                        if (_dr.Count() == 0)
                        {
                            clsDestinoFinal oAterro = new clsDestinoFinal();
                            clsDestinoFinalDados oAterroDados = new clsDestinoFinalDados();
                            oAterroDados.PegaDados(oAterro, Convert.ToInt32(_CodigoAterro[0]));
                            e.Row.Cells[12].Text = oAterro.Nome;
                            e.Row.Cells[15].Text = oAterro.Codigo.ToString();
                            _dt.Rows[e.Row.RowIndex]["CodigoDestinoFinal"] = _CodigoAterro[0];
                        }
                        else if (_dr.Count() > 0)
                        {
                            e.Row.Cells[12].Text = _dr[0]["NomeDestinoFinal"].ToString();
                            e.Row.Cells[15].Text = _dr[0]["CodigoAterro"].ToString();
                            _dt.Rows[e.Row.RowIndex]["CodigoDestinoFinal"] = _dr[0]["CodigoAterro"];
                        }
                    }
                }
            }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GradeDestinador_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            e.Row.Cells[5].ForeColor = Color.White;
            if (e.Row.Cells[1].Text.Replace(" ", "") != "")
            {
                if (e.Row.Cells[0].Text != "" && e.Row.Cells[5].Text != "")
                {
                    clsLicencaAmbientalDados oLAOdados = new clsLicencaAmbientalDados();
                    foreach (DataRow dr in oLAOdados.PreencheDataTableLicencaAmbiental("", Convert.ToInt32(e.Row.Cells[5].Text), "").Rows)
                    {
                        if (e.Row.Cells[1].Text == dr["NumeroLicenca"].ToString())
                        {
                            if (dr["Arquivo"].ToString() != "")
                                e.Row.Cells[1].Text = "<a target='_blank' href='http://200.98.129.47\\dados\\" + dr["Arquivo"].ToString() + "'>" + e.Row.Cells[1].Text + "</a>";
                        }
                    }
                }
            }
        }
    }
    protected void butVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("brooks/arquivos.aspx");
    }
}