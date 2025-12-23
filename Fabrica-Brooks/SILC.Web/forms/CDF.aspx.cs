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
    public partial class CDF : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        private DataTable _dtDestinadorFinal = new DataTable();
        clsLancamentoMTRDados oLancamentosMTRDados = new clsLancamentoMTRDados();
        clsUsuarios oUsuario = new clsUsuarios();
        clsClientes oCliente = new clsClientes();
        clsClienteDados oClienteDados = new clsClienteDados();
        clsEnderecos oEndereco = new clsEnderecos();
        clsEnderecosDados oEnderecoDados = new clsEnderecosDados();
        clsResiduoDados oResiduoDados = new clsResiduoDados();
        clsBloqFinanceiroDados oBloqueioFinanceiroDados = new clsBloqFinanceiroDados();

        DataTable _dt = new DataTable();
        string rt = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
            clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
            if (oUsuario != null)
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "23");
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
                intMes.Valor = DateTime.Now.AddMonths(-1).ToString("MM");
                if (intMes.Valor == "01" || intMes.Valor == "1")
                    intAno.Valor = (DateTime.Now.Year - 1).ToString();
                else
                    intAno.Valor = DateTime.Now.Year.ToString();
                rt = Request.QueryString["Codigo"];
                if (rt == null || rt == "")
                    rt = txtCodigoCliente.Text;

                if (rt != null && rt != "")
                {
                    if (rt.Length > 0)
                    {
                        if (Convert.ToInt32(rt) > 0)
                            MostraDadosCliente(Convert.ToInt32(rt));
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
            if (oBloqueioFinanceiroDados.ExisteBloqueio(oCliente.Codigo))
            {
                lblEndereco.Text = "Documento indisponível. Favor entrar em Contato com financeiro@brooksambiental.com.br ou telefone: (48)-3344-1515 \n ";
                lblEndereco.Font.Size = 12;

                GradeMTR.Visible = false;
                lblEnderecoT.Visible = false;
                GradeDestinador.Visible = false;
                lblDataEmissao.Visible = false;
                lblDadosLAO.Font.Size = 12;
                lblDadosLAO.Text = "";
                lblDestinador.Visible = false;
                Image2.Visible = false;
            }
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
        private void MontaCDF()
        {
            lblEndereco.Font.Size = 9;
            lblEnderecoT.Font.Size = 9;
            lblEnderecoT.Visible = true;
            GradeMTR.Visible = true;
            lblEnderecoT.Visible = true;
            GradeDestinador.Visible = true;
            lblDataEmissao.Visible = true;
            lblDadosLAO.Font.Size = 9;
            lblDestinador.Visible = true;
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
                string rtBROOKS = Request.QueryString["BROOKS"];
                string sDataInicial = "01/" + intMes.Valor + "/" + intAno.Valor;
                string sDataFinal = Convert.ToDateTime(sDataInicial).AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy");
                _dt = oLancamentosMTRDados.PreencheDataTableDDROrdem("CodigoGrupoResiduo, CodigoResiduo, DescricaoResiduo asc, Codigo, NumeroMTRFatima", 0,
                                                                      Convert.ToInt32(txtCodigoCliente.Text), sDataInicial, sDataFinal, false, 7);
                lblPeriodoDesejado.Text = sDataInicial + " a " + sDataFinal;
                if (_dt.Rows.Count > 0)
                {
                    foreach (DataRow _dr in _dt.Rows)
                    {
                        if (_dr["CodigoGrupoResiduo"].ToString() != "")
                            _dr[0] = oResiduoDados.PegaDescricao(Convert.ToInt32(_dr["CodigoGrupoResiduo"]));
                        if (_dr["Unidade"].ToString() != "")
                        {
                            if (_dr["Unidade"].ToString().ToUpper() == "CX")
                            {
                                _dr["Unidade"] = "M3";
                                if (_dr["Capacidade"].ToString() != "")
                                {
                                    _dr["QtdeDestinada"] = Convert.ToDecimal(_dr["QtdeDestinada"]) * Convert.ToDecimal(_dr["Capacidade"]) * Convert.ToDecimal(_dr["M3PorTon"]) * 1000;
                                    _dr["QtdeColetada"] = Convert.ToDecimal(_dr["QtdeColetada"]) * Convert.ToDecimal(_dr["Capacidade"]) * Convert.ToDecimal(_dr["M3PorTon"]) * 1000;
                                    _dr["Unidade"] = "KG";
                                }
                                else
                                {
                                    _dr["QtdeDestinada"] = 0;
                                    _dr["QtdeColetada"] = 0;
                                }
                            }
                            else if (_dr["Unidade"].ToString().ToUpper() == "M3")
                            {
                                _dr["QtdeDestinada"] = Convert.ToDecimal(_dr["QtdeDestinada"]) * Convert.ToDecimal(_dr["M3PorTon"]) * 1000;
                                _dr["QtdeColetada"] = Convert.ToDecimal(_dr["QtdeColetada"]) * Convert.ToDecimal(_dr["M3PorTon"]) * 1000;
                                _dr["Unidade"] = "KG";
                            }
                        }
                    }
                }

                GradeMTR.DataSource = _dt;
                GradeMTR.DataBind();

                foreach (DataRow _drDF in _dtDestinadorFinal.Rows)
                {
                    if (_drDF["CodigoAterro"].ToString() != "")
                    {
                        DataRow[] _dr = _dt.Select("CodigoDestinoFinal = " + _drDF["CodigoAterro"].ToString());
                        if (_dr.Count() == 0)
                            _drDF.Delete();
                    }
                }

                GradeDestinador.DataSource = _dtDestinadorFinal;
                GradeDestinador.DataBind();
            }
            Image2.Visible = false;
            if (GradeMTR.Rows.Count > 0)
            {
                lblCidadeEmpresa.Text = "Palhoça/SC, ";
                Image2.Visible = true;
            }
            else
            {
                lblDataEmissao.Text = "Não há registro(s)!";
                lblCidadeEmpresa.Text = "";
            }
        }
        protected void btnMontaCDF_Click(object sender, EventArgs e)
        {
            string rtBROOKS = Request.QueryString["BROOKS"];
            if (Convert.ToInt16(intAno.Valor) < 2020 && rtBROOKS != "BRO000935")
            {
                MessageBox(this.Page, "Ano não permitido. Apenas a partir de 2020.");
            }
            else
            {
                if (btnMontaCDF.Text == "Ok")
                    MontaCDF();
                if (intMes.Visible)
                {
                    intMes.Visible = false;
                    intAno.Visible = false;
                    lblPeriodoDesejado.ForeColor = Color.Black;
                    btnMontaCDF.Text = "Novo Período";
                }
                else
                {
                    lblPeriodoDesejado.Text = "Selecione mês/ano desejado:";
                    intMes.Visible = true;
                    intAno.Visible = true;
                    lblPeriodoDesejado.ForeColor = Color.IndianRed;
                    btnMontaCDF.Text = "Ok";
                }
            }
        }

        protected void GradeMTR_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                clsIBAMADados oIBAMADados = new clsIBAMADados();
                e.Row.Cells[2].Text = oIBAMADados.PegaDados(e.Row.Cells[1].Text).Replace("-", "").Replace("─", "");

                if (e.Row.Cells[3].Text == "0")
                    e.Row.Cells[3].Text = "";
                if (e.Row.Cells[4].Text == "0")
                    e.Row.Cells[4].Text = "";

                if (!e.Row.Cells[1].Text.ToUpper().Contains("TOTAL") && !e.Row.Cells[4].Text.ToUpper().Contains("TOTAL"))
                {
                    for (int i = 0; i <= GradeMTR.Columns.Count - 1; i++)
                    {
                        e.Row.Cells[i].BorderWidth = 1; // colocou a linha abaixo
                        e.Row.Cells[i].BorderColor = Color.DimGray;
                    }
                }
                DataRow[] _dr = _dtDestinadorFinal.Select("CodigoAterro = 7");
                if (_dr.Count() > 0)
                {
                    //e.Row.Cells[6].Text = "BROOKS AMBIENTAL";
                    _dt.Rows[e.Row.RowIndex]["CodigoDestinoFinal"] = "7";
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
                                    e.Row.Cells[1].Text = "<a target='_blank' href='..\\dados\\" + dr["Arquivo"].ToString() + "'>" + e.Row.Cells[1].Text + "</a>";
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
}