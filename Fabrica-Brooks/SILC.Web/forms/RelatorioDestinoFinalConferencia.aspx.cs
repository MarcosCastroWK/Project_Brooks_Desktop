using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;
using System.Drawing;

namespace SILC.Web.forms
{
    public partial class RelatorioDestinoFinalConferencia : System.Web.UI.Page
    {
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
            // basta que seja diferente, desta forma existe um usuário logado
            if (!IsPostBack)
            {
                txtCodigoDestinoFinal.Text = "";
                if (Session["CodigoCliente"] != null)
                    txtCodigoDestinoFinal.Text = Session["CodigoCliente"].ToString();
                if (Session["oUsuario"] != null)
                    txtCodigoDestinoFinal.Text = geral.RetiraLetras(((clsUsuarios)Session["oUsuario"]).Nome.ToUpper());

                GradeMTR.AutoGenerateColumns = false;
                rt = Request.QueryString["Codigo"];
                if (rt == null || rt == "")
                    rt = txtCodigoDestinoFinal.Text;

                txtDataInicial.Text = DateTime.Now.ToString("yyyy-MM-01");
                txtDataFinal.Text = Convert.ToDateTime(txtDataInicial.Text).AddDays(-1).ToString("yyyy-MM-dd");
                txtDataInicial.Text = Convert.ToDateTime(txtDataInicial.Text).AddDays(-1).ToString("yyyy-MM-01");
                if (rt != null && rt != "")
                {
                    if (rt.Length > 0)
                    {
                        if (Convert.ToInt32(rt) > 0)
                            MostraDadosDestinoFinal(Convert.ToInt32(rt));
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
        private void MostraDadosDestinoFinal(int pCodigo)
        {
            oDestino = new clsDestinoFinal();
            oDestino = oDestinoDados.PegaDados(oDestino, pCodigo);
            txtCodigoDestinoFinal.Text = oDestino.Codigo.ToString("000000");
            lblNomeDestinoFinal.Text = oDestino.Nome;
            lblCNPJ.Text = " - CNPJ/CPF: " + oDestino.CNPJ;

            //lblEndereco.Text = "";
            if (!EmConferenciaOuEventual())
            {
                //oEndereco = new clsEnderecos();
                //oEnderecoDados = new clsEnderecosDados();
                //oEndereco = oEnderecoDados.PegaDados(oEndereco, oCliente.Codigo, 2, 0);
                //lblEndereco.Text = lblEndereco.Text + oEndereco.endereco;
                //if (oEndereco.Bairro != "")
                //    lblEndereco.Text = lblEndereco.Text + " - Bairro: " + oEndereco.Bairro;
                //if (oEndereco.CEP != "")
                ///    lblEndereco.Text = lblEndereco.Text + " - CEP: " + oEndereco.CEP;
                //clsMunicipios oMunicipio = new clsMunicipios();
                //clsMunicipiosDados oMunicipioDados = new clsMunicipiosDados();
                //if (oEndereco.CodigoMunicipio > 0)
                //{
                //    oMunicipio = oMunicipioDados.PegaDados(oMunicipio, oEndereco.CodigoMunicipio);
                //lblEndereco.Text = lblEndereco.Text + " - Cidade: " + oMunicipio.Nome + " - Estado: " + oMunicipio.UF;
                //}

            }
            //if (oBloqueioFinanceiroDados.ExisteBloqueio(oCliente.Codigo))
            //{
            //lblEndereco.Text = "Documento indisponível. Favor entrar em Contato com financeiro@brooksambiental.com.br ou telefone: (48)-3344-1515 \n ";
            //lblEndereco.Font.Size = 12;

            //GradeMTR.Visible = false;
            //lblEnderecoT.Visible = false;
            //GradeDestinador.Visible = false;
            //lblDadosLAO.Font.Size = 12;
            //lblDadosLAO.Text = "";
            //lblDestinador.Visible = false;
            //}
        }

        private bool EmConferenciaOuEventual()
        {
            lblDadosLAO.Text = " Informações do(s) Resíduo(s)";

            clsDDR_Conferencia oDDRConf = new clsDDR_Conferencia();

            bool bConferencia = false;
            bool bMesLiberado = false;
            if (Convert.ToDateTime(txtDataFinal.Text).Month >= DateTime.Now.Month && Convert.ToDateTime(txtDataFinal.Text).Year >= DateTime.Now.Year)
            {
                if (Request.QueryString["BROOKS"] != "BRO000935")
                {
                    //lblEndereco.Text = "Relatório disponível no início do mês subseqüênte!";
                    return false;
                }
            }
            bMesLiberado = oDDRConf.MesLiberado(oCliente.Codigo, Convert.ToDateTime(txtDataFinal.Text).Month, Convert.ToDateTime(txtDataFinal.Text).Year);

            clsDocumentacaoAplicavelDados oDocAplic = new clsDocumentacaoAplicavelDados();

            if (oDocAplic.ConferirDDRAteDia(oCliente.Codigo) > 0 && !bMesLiberado)
                bConferencia = true;

            string rtBROOKS = Request.QueryString["BROOKS"];
            if (rtBROOKS == "BRO000935")
            {
                bConferencia = false;
                lblTitulo.Text = "Relatório por Destino Final para Conferência";
                txtCodigoDestinoFinal.Enabled = true;
                chkTotalResiduo.Visible = true;
                lblTotalResiduo.Visible = true;
                GradeMTR.Columns[17].Visible = true;
                if (chkTotalResiduo.Checked)
                {
                    lblTitulo.Text = "Relatório por Destino Final para Conferência";
                    GradeMTR.Columns[17].Visible = false;
                }
            }
            else
            {
                lblTitulo.Text = "Relatório por Destino Final para Conferência";
                txtCodigoDestinoFinal.Enabled = false;
                chkTotalResiduo.Visible = false;
                GradeMTR.Columns[17].Visible = false;
                lblTotalResiduo.Visible = false;
            }
            if ((rtBROOKS != "BRO000935" && oCliente.TiposDeContrato == 1) || (rtBROOKS != "BRO000935" && bConferencia))
            {
                if (oCliente.TiposDeContrato == 1 && !bMesLiberado && rtBROOKS != "BRO000935")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>window.open('../forms/DDR_Indisponivel.aspx');</script>");
                    //lblEndereco.Text = "Dados indisponíveis para este perfil de cadastro. Solicite informações através do link abaixo. \n ";
                    //lblEndereco.Font.Size = 12;
                    //btnMontaDDR.Visible = false;
                    GradeMTR.Visible = false;
                    //lblEnderecoT.Visible = false;
                    //GradeDestinador.Visible = false;
                    lblDadosLAO.Font.Size = 12;
                    lblDadosLAO.Text = "<a href='http://www.brooksambiental.com.br/contato'>www.brooksambiental.com.br/contato</a>";
                    //lblDestinador.Visible = false;
                }
                if (bConferencia)
                {
                    //lblEndereco.Text = "<center>EM CONFERÊNCIA!</center>";
                    //lblEndereco.Font.Size = 16;
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
            if (Convert.ToDateTime(txtDataFinal.Text).Month >= DateTime.Now.Month && Convert.ToDateTime(txtDataFinal.Text).Year >= DateTime.Now.Year && Request.QueryString["BROOKS"] != "BRO000935")
            {
                //lblEndereco.Text = "Relatório disponível no início do mês subseqüênte!";
                //lblEnderecoT.Visible = false;
                GradeMTR.Visible = false;
                //lblEnderecoT.Visible = false;
                //GradeDestinador.Visible = false;
                //lblDestinador.Visible = false;
                _dt.Clear();
                GradeMTR.DataSource = _dt;
                GradeMTR.DataBind();
                //GradeDestinador.DataSource = _dt;
                //GradeDestinador.DataBind();
            }
            else
            {
                //lblEndereco.Font.Size = 9;
                //lblEnderecoT.Font.Size = 9;
                //lblEnderecoT.Visible = true;
                GradeMTR.Visible = true;
                //lblEnderecoT.Visible = true;
                //GradeDestinador.Visible = true;
                lblDadosLAO.Font.Size = 9;
                //lblDestinador.Visible = true;
                lblDadosLAO.Text = "Informações do(s) Resíduo(s)";
                if (txtCodigoDestinoFinal.Text != "" && txtCodigoDestinoFinal.Text != "0")
                {
                    MostraDadosDestinoFinal(Convert.ToInt32(txtCodigoDestinoFinal.Text));

                    string mesExtenso = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(Convert.ToInt16(DateTime.Now.ToString("MM"))).ToLower();
                    //lblDataEmissao.Text = DateTime.Now.ToString("dd") + " de " + mesExtenso[0].ToString().ToUpper() + mesExtenso.Substring(1) + " de " + DateTime.Now.ToString("yyyy") + ".";

                    string rtBROOKS = Request.QueryString["BROOKS"];
                    if (rtBROOKS == "BRO000935" && !chkTotalResiduo.Checked)
                        _dt = oLancamentosMTRDados.PreencheDTRelatorioDestinoFinalConferencia("Nome, CodigoGrupoResiduo, CodigoResiduo, DescricaoResiduo asc, Codigo, DataColocacao", 0,
                                                   0, txtDataInicial.Text, txtDataFinal.Text, true, Convert.ToInt32(txtCodigoDestinoFinal.Text));
                    else
                        _dt = oLancamentosMTRDados.PreencheDTRelatorioDestinoFinalConferencia("Nome, CodigoGrupoResiduo, CodigoResiduo, DescricaoResiduo asc, Codigo, DataColocacao", 0,
                                                   0, txtDataInicial.Text, txtDataFinal.Text, false, Convert.ToInt32(txtCodigoDestinoFinal.Text));
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
                        string CodigoClienteAnterior = _dt.Rows[0]["CodigoCliente"].ToString();
                        string repete;
                        int iConta = 0;
                        foreach (DataRow _dr in _dt.Rows)
                        {
                            string _sDR = _dr["DescricaoResiduo"].ToString().Trim();
                            string _qDe = _dr["QtdeDestinada"].ToString();
                            string _qCo = _dr["QtdeColetada"].ToString();
                            if (_sDR == "" && _qDe == "0,0000" && _qCo == "0,0000")
                            {
                                _dr.Delete();
                            }
                            else
                            {
                                if (_dr["QtdeCDF"].ToString() != "" && _dr["QtdeCDF"].ToString() != "0,00")
                                    xQtCDF++;
                                if (_dr["CodigoGrupoResiduo"].ToString() != CodigoGrupoResiduoAnterior) // && CodigoClienteAnterior == _dr["CodigoCliente"].ToString())
                                {
                                    iConta++;
                                    oResiduo = oResiduoDados.PegaDados(oResiduo, Convert.ToInt32(_dr["CodigoGrupoResiduo"].ToString()));
                                    repete = new string(' ', 100 - oResiduo.DescricaoReduzida.Length);
                                    _dr["DescricaoGrupo"] = iConta.ToString() + "-" + oResiduo.DescricaoReduzida.Trim();
                                }
                                if (_dr["CodigoResiduo"].ToString() != CodigoResiduoAnterior) // && CodigoClienteAnterior == _dr["CodigoCliente"].ToString())
                                {
                                    DescricaoResiduoAnterior = _dr["DescricaoResiduo"].ToString();
                                    SubTotalGerado = 0;
                                    SubTotalAmazendado = 0;
                                    SubTotalDestinado = 0;
                                }
                                CodigoGrupoResiduoAnterior = _dr["CodigoGrupoResiduo"].ToString();
                                CodigoResiduoAnterior = _dr["CodigoResiduo"].ToString();
                                CodigoClienteAnterior = _dr["CodigoCliente"].ToString();
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
                    }

                    GradeMTR.Columns[13].Visible = true;

                    if (xQtCDF == 0)
                        GradeMTR.Columns[13].Visible = false;

                    rtBROOKS = Request.QueryString["BROOKS"];
                    if (bCDFeNumeroBranco && rtBROOKS != "BRO000935")
                        GradeMTR.Columns[11].Visible = false;

                    GradeMTR.Columns[18].Visible = true; // situacao
                    GradeMTR.Columns[6].Visible = false;
                    GradeMTR.DataSource = _dt;
                    GradeMTR.DataBind();
                    GradeMTR.Columns[18].Visible = false; // situacao

                    /*foreach (DataRow _drDF in _dtDestinadorFinal.Rows)
                    {
                        if (_drDF["CodigoAterro"].ToString() != "")
                        {
                            DataRow[] _dr = _dt.Select("CodigoDestinoFinal = " + _drDF["CodigoAterro"].ToString());
                            if (_dr.Count() == 0)
                                _drDF.Delete();
                        }
                    }*/
                }
            }

        }
        protected void btnMontaDDR_Click(object sender, EventArgs e)
        {
            SubTotalGerado = 0;
            SubTotalAmazendado = 0;
            SubTotalDestinado = 0;
            SubTotalCDFe = 0;
            string rtBROOKS = Request.QueryString["BROOKS"];
            if (Convert.ToDateTime(txtDataInicial.Text).Year <= 2017 ||
               (Convert.ToDateTime(txtDataInicial.Text).Year == 2018 &&
                Convert.ToDateTime(txtDataInicial.Text).Month <= 8 && rtBROOKS != "BRO000935"))
            {
                MessageBox(this.Page, "Período inicial não permitido. Apenas a partir de 09/2018.");
            }
            else
            {
                if (btnMontaDDR.Text == "Ok")
                    MontaDDR();
                if (txtDataInicial.Visible)
                {
                    lblPeriodoDesejado.Text = Convert.ToDateTime(txtDataInicial.Text).ToShortDateString() + " a " + Convert.ToDateTime(txtDataFinal.Text).ToShortDateString();
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

                if (e.Row.Cells[13].Text == "0")
                    e.Row.Cells[13].Text = "";
                if (e.Row.Cells[3].Text == "0")
                    e.Row.Cells[3].Text = "";
                if (e.Row.Cells[4].Text == "0")
                    e.Row.Cells[4].Text = "";

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
            }
        }
    }
}