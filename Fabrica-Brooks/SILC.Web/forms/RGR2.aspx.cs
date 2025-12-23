using System;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;
using System.Drawing;

namespace SILC.Web.forms
{
    public partial class RGR2 : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
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

        DataTable _dtDestinadorFinal = new DataTable();
        DataTable _dt = new DataTable();
        DataTable _dtTodos = new DataTable();
        DataTable _dtConversao = new DataTable();

        string rt = "";
        string _datainicial = "";
        string _datafinal = "";
        decimal SubTotalGerado = 0;
        decimal SubTotalAmazendado = 0;
        decimal SubTotalDestinado = 0;
        decimal SubTotalCDFe = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
            clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
            if (oUsuario != null)
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "24");
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
                lblMesAnoReferente.Visible = false;
                lblPeriodoApuracao.Visible = false;
                imgEtapas.Visible = false;
                intMes.Valor = (DateTime.Now.Month - 1).ToString();
                intAno.Valor = (DateTime.Now.Year - 2000).ToString();
                DropDownList1.Items.Clear();
                DropDownList1.Items.Add("1");
                DropDownList1.Items.Add("2");
                DropDownList1.Items.Add("3");
                DropDownList1.Items.Add("4");
                DropDownList1.Items.Add("5");
                DropDownList1.Items.Add("6");
                DropDownList1.Items.Add("7");
                DropDownList1.Items.Add("8");
                DropDownList1.Items.Add("9");
                DropDownList1.Items.Add("10");
                DropDownList1.Items.Add("11");
                DropDownList1.Items.Add("12");
                DropDownList1.Items.Add("13");
                DropDownList1.Items.Add("14");
                DropDownList1.Items.Add("15");
                DropDownList1.Items.Add("16");
                DropDownList1.Items.Add("17");
                DropDownList1.Items.Add("18");
                DropDownList1.Items.Add("19");
                DropDownList1.Items.Add("20");
                DropDownList1.Items.Add("21");
                DropDownList1.Items.Add("22");
                DropDownList1.Items.Add("23");
                DropDownList1.Items.Add("24");
                DropDownList1.Items.Add("25");
                DropDownList1.Text = "6";
            }
        }
        private void MostraDadosCliente(int pCodigo)
        {
            oCliente = new clsClientes();
            oCliente = oClienteDados.PegaDados(oCliente, pCodigo);
            txtCodigoCliente.Text = oCliente.Codigo.ToString("000000");
            lblNomeCliente.Text = oCliente.Nome;
            lblCNPJ.Text = " - CNPJ/CPF: " + oCliente.CNPJ_CPF;
            imgLogoCliente.Visible = false;
            if (oCliente.Codigo == 73)
            {
                imgLogoCliente.ImageUrl = "~/Images/logoBeimar.png";
                imgLogoCliente.Visible = true;
            }
            else if (oCliente.Codigo == 1407)
            {
                imgLogoCliente.ImageUrl = "~/Images/logoCostao.png";
                imgLogoCliente.Visible = true;
            }
            else if (oCliente.Codigo == 2306)
            {
                imgLogoCliente.ImageUrl = "~/Images/logoFloripa.png";
                imgLogoCliente.Visible = true;
            }
            else if (oCliente.Codigo == 1414)
            {
                imgLogoCliente.ImageUrl = "~/Images/logoIntech.png";
                imgLogoCliente.Visible = true;
            }
            else if (oCliente.Codigo == 2644)
            {
                imgLogoCliente.ImageUrl = "~/Images/logoIFashion.png";
                imgLogoCliente.Visible = true;
            }
            else if (oCliente.Codigo == 2322)
            {
                imgLogoCliente.ImageUrl = "~/Images/logoPortobello.png";
                imgLogoCliente.Visible = true;
            }
            else if (oCliente.Codigo == 1440)
            {
                imgLogoCliente.ImageUrl = "~/Images/logoSCPar.png";
                imgLogoCliente.Visible = true;
            }
            else if (oCliente.Codigo == 2624)
            {
                imgLogoCliente.ImageUrl = "~/Images/logoItaguacu.png";
                imgLogoCliente.Visible = true;
            }
            else if (oCliente.Codigo == 2364)
            {
                imgLogoCliente.ImageUrl = "~/Images/logoSquare.png";
                imgLogoCliente.Visible = true;
            }
            else if (oCliente.Codigo == 927)
            {
                imgLogoCliente.ImageUrl = "~/Images/logoViaCatarina.png";
                imgLogoCliente.Visible = true;
            }
            else if (oCliente.Codigo == 1527)
            {
                imgLogoCliente.ImageUrl = "~/Images/logoVotorantim.png";
                imgLogoCliente.Visible = true;
            }
            else if (oCliente.Codigo == 1329)
            {
                imgLogoCliente.ImageUrl = "~/Images/logoVotorantim.png";
                imgLogoCliente.Visible = true;
            }
            lblMesAnoReferente.Text = geral.PegaMesExtenso(Convert.ToInt16(intMes.Valor)).ToUpper() + " - 20" + intAno.Valor;
            lblPeriodoApuracao.Text = "Periodo de apuração: 01/" + intMes.Valor + "/20" + intAno.Valor +
                                      " a " + geral.UltimoDiaMes("01/" + intMes.Valor + "/" + intAno.Valor) + "/" + intMes.Valor + "/20" + intAno.Valor;
        }
        private bool EmConferenciaOuEventual()
        {
            clsDDR_Conferencia oDDRConf = new clsDDR_Conferencia();

            bool bConferencia = false;
            bool bMesLiberado = false;
            if (Convert.ToDateTime(_datafinal).Month >= DateTime.Now.Month && Convert.ToDateTime(_datafinal).Year >= DateTime.Now.Year)
            {
                bMesLiberado = true;
                bConferencia = true;
            }
            else
                bMesLiberado = oDDRConf.MesLiberado(oCliente.Codigo, Convert.ToDateTime(_datafinal).Month, Convert.ToDateTime(_datafinal).Year);

            clsDocumentacaoAplicavelDados oDocAplic = new clsDocumentacaoAplicavelDados();

            if (oDocAplic.ConferirDDRAteDia(oCliente.Codigo) > 0 && !bMesLiberado)
                bConferencia = true;
            return bConferencia;
        }

        protected void btnMontaRGR_Click(object sender, EventArgs e)
        {
            _datainicial = "20" + intAno.Valor + "-" + intMes.Valor + "-01";
            _datafinal = "20" + intAno.Valor + "-" + intMes.Valor + "-" + geral.UltimoDiaMes("01/" + intMes.Valor + "/" + intAno.Valor);

            SubTotalGerado = 0;
            SubTotalAmazendado = 0;
            SubTotalDestinado = 0;
            SubTotalCDFe = 0;

            string rtBROOKS = Request.QueryString["BROOKS"];
            if (Convert.ToDateTime(_datainicial).Year < 2020 && rtBROOKS != "BRO000935")
            {
                lblPeriodoDesejado.Text = "Ano não permitido. Apenas a partir de 2020.";
            }
            else
            {
                if (btnMontaRGR.Text == "Ok")
                    MontaRGR();
                if (intAno.Visible)
                {
                    lblPeriodoDesejado.Text = Convert.ToDateTime(_datainicial).Month.ToString("00") + "/" +
                                              Convert.ToDateTime(_datainicial).Year.ToString();
                    lblPeriodoDesejado.Visible = false;
                    intAno.Visible = false;
                    intMes.Visible = false;
                    lblBarra.Visible = false;
                    lblMesAnoReferente.Visible = true;
                    lblPeriodoApuracao.Visible = true;
                    imgLogoCliente.Visible = true;
                    GradeMTR.Visible = true;
                    imgEtapas.Visible = true;
                    lblPeriodoDesejado.ForeColor = Color.Black;
                    btnMontaRGR.Text = "Novo Ano";
                }
                else
                {
                    lblPeriodoDesejado.Text = "Digite Mês/Ano desejado: ";
                    lblPeriodoDesejado.Visible = true;
                    intAno.Visible = true;
                    intMes.Visible = true;
                    lblBarra.Visible = true;
                    lblMesAnoReferente.Visible = false;
                    lblPeriodoApuracao.Visible = false;
                    imgLogoCliente.Visible = false;
                    GradeMTR.Visible = false;
                    imgEtapas.Visible = false;
                    lblPeriodoDesejado.ForeColor = Color.IndianRed;
                    btnMontaRGR.Text = "Ok";
                }
            }
        }

        private void ZeroTrocaPorIfem(GridView pGrade)
        {
            foreach (GridViewRow e in pGrade.Rows)
            {
                if (e.Cells[2].Text == "0" || e.Cells[2].Text == "" || e.Cells[2].Text == "&nbsp;")
                {
                    e.Cells[2].Text = "-";
                    e.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                }
                if (e.Cells[3].Text == "0" || e.Cells[3].Text == "" || e.Cells[3].Text == "&nbsp;")
                {
                    e.Cells[3].Text = "-";
                    e.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                }
                if (e.Cells[4].Text == "0" || e.Cells[4].Text == "" || e.Cells[4].Text == "&nbsp;")
                {
                    e.Cells[4].Text = "-";
                    e.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                }
                if (e.Cells[5].Text == "0" || e.Cells[5].Text == "" || e.Cells[5].Text == "&nbsp;")
                {
                    e.Cells[5].Text = "-";
                    e.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                }
                if (e.Cells[6].Text == "0" || e.Cells[6].Text == "" || e.Cells[6].Text == "&nbsp;")
                {
                    e.Cells[6].Text = "-";
                    e.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                }
                if (e.Cells[7].Text == "0" || e.Cells[7].Text == "" || e.Cells[7].Text == "&nbsp;")
                {
                    e.Cells[7].Text = "-";
                    e.Cells[7].HorizontalAlign = HorizontalAlign.Center;
                }
                if (e.Cells[8].Text == "0" || e.Cells[8].Text == "" || e.Cells[8].Text == "&nbsp;")
                {
                    e.Cells[8].Text = "-";
                    e.Cells[8].HorizontalAlign = HorizontalAlign.Center;
                }
                if (e.Cells[9].Text == "0" || e.Cells[9].Text == "" || e.Cells[9].Text == "&nbsp;")
                {
                    e.Cells[9].Text = "-";
                    e.Cells[9].HorizontalAlign = HorizontalAlign.Center;
                }
                if (e.Cells[10].Text == "0" || e.Cells[10].Text == "" || e.Cells[10].Text == "&nbsp;")
                {
                    e.Cells[10].Text = "-";
                    e.Cells[10].HorizontalAlign = HorizontalAlign.Center;
                }
                if (e.Cells[11].Text == "0" || e.Cells[11].Text == "" || e.Cells[11].Text == "&nbsp;")
                {
                    e.Cells[11].Text = "-";
                    e.Cells[11].HorizontalAlign = HorizontalAlign.Center;
                }
                if (e.Cells[12].Text == "0" || e.Cells[12].Text == "" || e.Cells[12].Text == "&nbsp;")
                {
                    e.Cells[12].Text = "-";
                    e.Cells[12].HorizontalAlign = HorizontalAlign.Center;
                }
                if (e.Cells[13].Text == "0" || e.Cells[13].Text == "" || e.Cells[13].Text == "&nbsp;")
                {
                    e.Cells[13].Text = "-";
                    e.Cells[13].HorizontalAlign = HorizontalAlign.Center;
                }
                if (e.Cells[14].Text == "0" || e.Cells[14].Text == "" || e.Cells[14].Text == "&nbsp;")
                {
                    e.Cells[14].Text = "-";
                    e.Cells[14].HorizontalAlign = HorizontalAlign.Center;
                }
                if (e.Cells[15].Text == "0" || e.Cells[15].Text == "" || e.Cells[15].Text == "&nbsp;")
                {
                    e.Cells[15].Text = "-";
                    e.Cells[15].HorizontalAlign = HorizontalAlign.Center;
                }
            }
        }

        private DataTable MontaDtGrafico(DataTable dtGrafico, DataTable _dt)
        {
            dtGrafico = new DataTable();
            DataColumn dcgrafico = new DataColumn();

            dcgrafico = new DataColumn();
            dcgrafico.ColumnName = "Mes";
            dtGrafico.Columns.Add(dcgrafico);
            for (int i = 0; i <= _dt.Rows.Count - 1; i++)
            {
                DataRow dr = _dt.Rows[i];
                dcgrafico = new DataColumn();
                dcgrafico.ColumnName = geral.RemoverAcentos(dr["DescricaoReduzida"].ToString().Replace("-", "")).Replace(" ", "_").Replace(",", "");
                dcgrafico.DataType = Type.GetType("System.Decimal");
                dtGrafico.Columns.Add(dcgrafico);
            }

            dtGrafico.NewRow();
            dtGrafico.Rows.Add();
            dtGrafico.Rows[dtGrafico.Rows.Count - 1][0] = "Jan";
            for (int i = 0; i <= _dt.Rows.Count - 1; i++)
            {
                DataRow dr = _dt.Rows[i];
                dtGrafico.Rows[dtGrafico.Rows.Count - 1][geral.RemoverAcentos(dr["DescricaoReduzida"].ToString().Replace("-", "")).Replace(" ", "_").Replace(",", "")] = dr["JanQuantidade"];
            }
            dtGrafico.NewRow();
            dtGrafico.Rows.Add();
            dtGrafico.Rows[dtGrafico.Rows.Count - 1][0] = "Fev";
            for (int i = 0; i <= _dt.Rows.Count - 1; i++)
            {
                DataRow dr = _dt.Rows[i];
                dtGrafico.Rows[dtGrafico.Rows.Count - 1][geral.RemoverAcentos(dr["DescricaoReduzida"].ToString().Replace("-", "")).Replace(" ", "_").Replace(",", "")] = dr["FevQuantidade"];
            }
            dtGrafico.NewRow();
            dtGrafico.Rows.Add();
            dtGrafico.Rows[dtGrafico.Rows.Count - 1][0] = "Mar";
            for (int i = 0; i <= _dt.Rows.Count - 1; i++)
            {
                DataRow dr = _dt.Rows[i];
                dtGrafico.Rows[dtGrafico.Rows.Count - 1][geral.RemoverAcentos(dr["DescricaoReduzida"].ToString().Replace("-", "")).Replace(" ", "_").Replace(",", "")] = dr["MarQuantidade"];
            }
            dtGrafico.NewRow();
            dtGrafico.Rows.Add();
            dtGrafico.Rows[dtGrafico.Rows.Count - 1][0] = "Abr";
            for (int i = 0; i <= _dt.Rows.Count - 1; i++)
            {
                DataRow dr = _dt.Rows[i];
                dtGrafico.Rows[dtGrafico.Rows.Count - 1][geral.RemoverAcentos(dr["DescricaoReduzida"].ToString().Replace("-", "")).Replace(" ", "_").Replace(",", "")] = dr["AbrQuantidade"];
            }
            dtGrafico.NewRow();
            dtGrafico.Rows.Add();
            dtGrafico.Rows[dtGrafico.Rows.Count - 1][0] = "Mai";
            for (int i = 0; i <= _dt.Rows.Count - 1; i++)
            {
                DataRow dr = _dt.Rows[i];
                dtGrafico.Rows[dtGrafico.Rows.Count - 1][geral.RemoverAcentos(dr["DescricaoReduzida"].ToString().Replace("-", "")).Replace(" ", "_").Replace(",", "")] = dr["MaiQuantidade"];
            }
            dtGrafico.NewRow();
            dtGrafico.Rows.Add();
            dtGrafico.Rows[dtGrafico.Rows.Count - 1][0] = "Jun";
            for (int i = 0; i <= _dt.Rows.Count - 1; i++)
            {
                DataRow dr = _dt.Rows[i];
                dtGrafico.Rows[dtGrafico.Rows.Count - 1][geral.RemoverAcentos(dr["DescricaoReduzida"].ToString().Replace("-", "")).Replace(" ", "_").Replace(",", "")] = dr["JunQuantidade"];
            }
            dtGrafico.NewRow();
            dtGrafico.Rows.Add();
            dtGrafico.Rows[dtGrafico.Rows.Count - 1][0] = "Jul";
            for (int i = 0; i <= _dt.Rows.Count - 1; i++)
            {
                DataRow dr = _dt.Rows[i];
                dtGrafico.Rows[dtGrafico.Rows.Count - 1][geral.RemoverAcentos(dr["DescricaoReduzida"].ToString().Replace("-", "")).Replace(" ", "_").Replace(",", "")] = dr["JulQuantidade"];
            }
            dtGrafico.NewRow();
            dtGrafico.Rows.Add();
            dtGrafico.Rows[dtGrafico.Rows.Count - 1][0] = "Ago";
            for (int i = 0; i <= _dt.Rows.Count - 1; i++)
            {
                DataRow dr = _dt.Rows[i];
                dtGrafico.Rows[dtGrafico.Rows.Count - 1][geral.RemoverAcentos(dr["DescricaoReduzida"].ToString().Replace("-", "")).Replace(" ", "_").Replace(",", "")] = dr["AgoQuantidade"];
            }
            dtGrafico.NewRow();
            dtGrafico.Rows.Add();
            dtGrafico.Rows[dtGrafico.Rows.Count - 1][0] = "Set";
            for (int i = 0; i <= _dt.Rows.Count - 1; i++)
            {
                DataRow dr = _dt.Rows[i];
                dtGrafico.Rows[dtGrafico.Rows.Count - 1][geral.RemoverAcentos(dr["DescricaoReduzida"].ToString().Replace("-", "")).Replace(" ", "_").Replace(",", "")] = dr["SetQuantidade"];
            }
            dtGrafico.NewRow();
            dtGrafico.Rows.Add();
            dtGrafico.Rows[dtGrafico.Rows.Count - 1][0] = "Out";
            for (int i = 0; i <= _dt.Rows.Count - 1; i++)
            {
                DataRow dr = _dt.Rows[i];
                dtGrafico.Rows[dtGrafico.Rows.Count - 1][geral.RemoverAcentos(dr["DescricaoReduzida"].ToString().Replace("-", "")).Replace(" ", "_").Replace(",", "")] = dr["OutQuantidade"];
            }
            dtGrafico.NewRow();
            dtGrafico.Rows.Add();
            dtGrafico.Rows[dtGrafico.Rows.Count - 1][0] = "Nov";
            for (int i = 0; i <= _dt.Rows.Count - 1; i++)
            {
                DataRow dr = _dt.Rows[i];
                dtGrafico.Rows[dtGrafico.Rows.Count - 1][geral.RemoverAcentos(dr["DescricaoReduzida"].ToString().Replace("-", "")).Replace(" ", "_").Replace(",", "")] = dr["NovQuantidade"];
            }
            dtGrafico.NewRow();
            dtGrafico.Rows.Add();
            dtGrafico.Rows[dtGrafico.Rows.Count - 1][0] = "Dez";
            for (int i = 0; i <= _dt.Rows.Count - 1; i++)
            {
                DataRow dr = _dt.Rows[i];
                dtGrafico.Rows[dtGrafico.Rows.Count - 1][geral.RemoverAcentos(dr["DescricaoReduzida"].ToString().Replace("-", "")).Replace(" ", "_").Replace(",", "")] = dr["DezQuantidade"];
            }
            return dtGrafico;
        }
        private decimal AddSomaMediaNaGrade(string sInCodigosResiduos, DataTable _dt, bool bAddLinhaTotal)
        {
            decimal qtTotalAno = 0;
            decimal qtTotalJaneiro = 0;
            decimal qtTotalFevereiro = 0;
            decimal qtTotalMarco = 0;
            decimal qtTotalAbril = 0;
            decimal qtTotalMaio = 0;
            decimal qtTotalJunho = 0;
            decimal qtTotalJulho = 0;
            decimal qtTotalAgosto = 0;
            decimal qtTotalSetembro = 0;
            decimal qtTotalOutubro = 0;
            decimal qtTotalNovembro = 0;
            decimal qtTotalDezembro = 0;

            decimal qtTotalAnoLinha = 0;
            decimal qtTotalJaneiroLinha = 0;
            decimal qtTotalFevereiroLinha = 0;
            decimal qtTotalMarcoLinha = 0;
            decimal qtTotalAbrilLinha = 0;
            decimal qtTotalMaioLinha = 0;
            decimal qtTotalJunhoLinha = 0;
            decimal qtTotalJulhoLinha = 0;
            decimal qtTotalAgostoLinha = 0;
            decimal qtTotalSetembroLinha = 0;
            decimal qtTotalOutubroLinha = 0;
            decimal qtTotalNovembroLinha = 0;
            decimal qtTotalDezembroLinha = 0;
            foreach (DataRow dr in _dt.Rows)
            {
                foreach (DataRow drConv in _dtConversao.Rows)
                {
                    if (drConv["Codigo"].ToString() == dr["CodigoResiduo"].ToString())
                    {
                        if (Convert.ToDecimal(drConv["Peso"]) != 1)
                        {
                            dr[2] = "KG";
                            if (dr[0].ToString() == "160" && sInCodigosResiduos == "160")
                            {
                                dr[2] = "";
                                dr[4] = Convert.ToDecimal(dr[4]) / Convert.ToDecimal(drConv["Peso"]) / 1000;
                                dr[5] = Convert.ToDecimal(dr[5]) / Convert.ToDecimal(drConv["Peso"]) / 1000;
                                dr[6] = Convert.ToDecimal(dr[6]) / Convert.ToDecimal(drConv["Peso"]) / 1000;
                                dr[7] = Convert.ToDecimal(dr[7]) / Convert.ToDecimal(drConv["Peso"]) / 1000;
                                dr[8] = Convert.ToDecimal(dr[8]) / Convert.ToDecimal(drConv["Peso"]) / 1000;
                                dr[9] = Convert.ToDecimal(dr[9]) / Convert.ToDecimal(drConv["Peso"]) / 1000;
                                dr[10] = Convert.ToDecimal(dr[10]) / Convert.ToDecimal(drConv["Peso"]) / 1000;
                                dr[11] = Convert.ToDecimal(dr[11]) / Convert.ToDecimal(drConv["Peso"]) / 1000;
                                dr[12] = Convert.ToDecimal(dr[12]) / Convert.ToDecimal(drConv["Peso"]) / 1000;
                                dr[13] = Convert.ToDecimal(dr[13]) / Convert.ToDecimal(drConv["Peso"]) / 1000;
                                dr[14] = Convert.ToDecimal(dr[14]) / Convert.ToDecimal(drConv["Peso"]) / 1000;
                                dr[15] = Convert.ToDecimal(dr[15]) / Convert.ToDecimal(drConv["Peso"]) / 1000;
                            }
                            else
                            {
                                if (dr[0].ToString() != "160")
                                {
                                    dr[4] = Convert.ToDecimal(dr[4]) * (Convert.ToDecimal(drConv["Peso"]) / 10);
                                    dr[5] = Convert.ToDecimal(dr[5]) * (Convert.ToDecimal(drConv["Peso"]) / 10);
                                    dr[6] = Convert.ToDecimal(dr[6]) * (Convert.ToDecimal(drConv["Peso"]) / 10);
                                    dr[7] = Convert.ToDecimal(dr[7]) * (Convert.ToDecimal(drConv["Peso"]) / 10);
                                    dr[8] = Convert.ToDecimal(dr[8]) * (Convert.ToDecimal(drConv["Peso"]) / 10);
                                    dr[9] = Convert.ToDecimal(dr[9]) * (Convert.ToDecimal(drConv["Peso"]) / 10);
                                    dr[10] = Convert.ToDecimal(dr[10]) * (Convert.ToDecimal(drConv["Peso"]) / 10);
                                    dr[11] = Convert.ToDecimal(dr[11]) * (Convert.ToDecimal(drConv["Peso"]) / 10);
                                    dr[12] = Convert.ToDecimal(dr[12]) * (Convert.ToDecimal(drConv["Peso"]) / 10);
                                    dr[13] = Convert.ToDecimal(dr[13]) * (Convert.ToDecimal(drConv["Peso"]) / 10);
                                    dr[14] = Convert.ToDecimal(dr[14]) * (Convert.ToDecimal(drConv["Peso"]) / 10);
                                    dr[15] = Convert.ToDecimal(dr[15]) * (Convert.ToDecimal(drConv["Peso"]) / 10);
                                }
                            }
                            break;
                        }
                    }
                }
                qtTotalJaneiroLinha = 0;
                qtTotalFevereiroLinha = 0;
                qtTotalMarcoLinha = 0;
                qtTotalAbrilLinha = 0;
                qtTotalMaioLinha = 0;
                qtTotalJunhoLinha = 0;
                qtTotalJulhoLinha = 0;
                qtTotalAgostoLinha = 0;
                qtTotalSetembroLinha = 0;
                qtTotalOutubroLinha = 0;
                qtTotalNovembroLinha = 0;
                qtTotalDezembroLinha = 0;
                if (dr[4].ToString() != "")
                {
                    qtTotalJaneiro = qtTotalJaneiro + Convert.ToDecimal(dr[4]);
                    qtTotalJaneiroLinha = qtTotalJaneiroLinha + Convert.ToDecimal(dr[4]);
                }
                if (dr[5].ToString() != "")
                {
                    qtTotalFevereiro = qtTotalFevereiro + Convert.ToDecimal(dr[5]);
                    qtTotalFevereiroLinha = qtTotalFevereiroLinha + Convert.ToDecimal(dr[5]);
                }
                if (dr[6].ToString() != "")
                {
                    qtTotalMarco = qtTotalMarco + Convert.ToDecimal(dr[6]);
                    qtTotalMarcoLinha = qtTotalMarcoLinha + Convert.ToDecimal(dr[6]);
                }
                if (dr[7].ToString() != "")
                {
                    qtTotalAbril = qtTotalAbril + Convert.ToDecimal(dr[7]);
                    qtTotalAbrilLinha = qtTotalAbrilLinha + Convert.ToDecimal(dr[7]);
                }
                if (dr[8].ToString() != "")
                {
                    qtTotalMaio = qtTotalMaio + Convert.ToDecimal(dr[8]);
                    qtTotalMaioLinha = qtTotalMaioLinha + Convert.ToDecimal(dr[8]);
                }
                if (dr[9].ToString() != "")
                {
                    qtTotalJunho = qtTotalJunho + Convert.ToDecimal(dr[9]);
                    qtTotalJunhoLinha = qtTotalJunhoLinha + Convert.ToDecimal(dr[9]);
                }
                if (dr[10].ToString() != "")
                {
                    qtTotalJulho = qtTotalJulho + Convert.ToDecimal(dr[10]);
                    qtTotalJulhoLinha = qtTotalJulhoLinha + Convert.ToDecimal(dr[10]);
                }
                if (dr[11].ToString() != "")
                {
                    qtTotalAgosto = qtTotalAgosto + Convert.ToDecimal(dr[11]);
                    qtTotalAgostoLinha = qtTotalAgostoLinha + Convert.ToDecimal(dr[11]);
                }
                if (dr[12].ToString() != "")
                {
                    qtTotalSetembro = qtTotalSetembro + Convert.ToDecimal(dr[12]);
                    qtTotalSetembroLinha = qtTotalSetembroLinha + Convert.ToDecimal(dr[12]);
                }
                if (dr[13].ToString() != "")
                {
                    qtTotalOutubro = qtTotalOutubro + Convert.ToDecimal(dr[13]);
                    qtTotalOutubroLinha = qtTotalOutubroLinha + Convert.ToDecimal(dr[13]);
                }
                if (dr[14].ToString() != "")
                {
                    qtTotalNovembro = qtTotalNovembro + Convert.ToDecimal(dr[14]);
                    qtTotalNovembroLinha = qtTotalNovembroLinha + Convert.ToDecimal(dr[14]);
                }
                if (dr[15].ToString() != "")
                {
                    qtTotalDezembro = qtTotalDezembro + Convert.ToDecimal(dr[15]);
                    qtTotalDezembroLinha = qtTotalDezembroLinha + Convert.ToDecimal(dr[15]);
                }
                qtTotalAno = qtTotalJaneiro + qtTotalFevereiro + qtTotalMarco + qtTotalAbril + qtTotalMaio + qtTotalJunho + qtTotalJulho + qtTotalAgosto +
                             qtTotalSetembro + qtTotalOutubro + qtTotalNovembro + qtTotalDezembro;


                qtTotalAnoLinha = qtTotalJaneiroLinha + qtTotalFevereiroLinha + qtTotalMarcoLinha + qtTotalAbrilLinha + qtTotalMaioLinha + qtTotalJunhoLinha +
                                  qtTotalJulhoLinha + qtTotalAgostoLinha + qtTotalSetembroLinha + qtTotalOutubroLinha + qtTotalNovembroLinha + qtTotalDezembroLinha;
                dr["TotalQuantidadeAno"] = qtTotalAnoLinha.ToString("N2");
                dr["MediaAno"] = (qtTotalAnoLinha / (Convert.ToInt16(intMes.Valor))).ToString("N2");

                if (qtTotalAno > 0 && sInCodigosResiduos.Length > 0)
                    sInCodigosResiduos = sInCodigosResiduos + ", " + dr[0].ToString();
            }
            if (sInCodigosResiduos.Length > 0)
                sInCodigosResiduos = sInCodigosResiduos.Substring(1);
            if (bAddLinhaTotal)
            {
                _dt.NewRow();
                _dt.Rows.Add();
                _dt.Rows[_dt.Rows.Count - 1][1] = "Total (Kg)";
                _dt.Rows[_dt.Rows.Count - 1][4] = qtTotalJaneiro.ToString("N2");
                _dt.Rows[_dt.Rows.Count - 1][5] = qtTotalFevereiro.ToString("N2");
                _dt.Rows[_dt.Rows.Count - 1][6] = qtTotalMarco.ToString("N2");
                _dt.Rows[_dt.Rows.Count - 1][7] = qtTotalAbril.ToString("N2");
                _dt.Rows[_dt.Rows.Count - 1][8] = qtTotalMaio.ToString("N2");
                _dt.Rows[_dt.Rows.Count - 1][9] = qtTotalJunho.ToString("N2");
                _dt.Rows[_dt.Rows.Count - 1][10] = qtTotalJulho.ToString("N2");
                _dt.Rows[_dt.Rows.Count - 1][11] = qtTotalAgosto.ToString("N2");
                _dt.Rows[_dt.Rows.Count - 1][12] = qtTotalSetembro.ToString("N2");
                _dt.Rows[_dt.Rows.Count - 1][13] = qtTotalOutubro.ToString("N2");
                _dt.Rows[_dt.Rows.Count - 1][14] = qtTotalNovembro.ToString("N2");
                _dt.Rows[_dt.Rows.Count - 1][15] = qtTotalDezembro.ToString("N2");
                _dt.Rows[_dt.Rows.Count - 1][16] = qtTotalAno.ToString("N2");
                _dt.Rows[_dt.Rows.Count - 1][17] = (qtTotalAno / Convert.ToInt16(intMes.Valor)).ToString("N2");
            }
            qtTotalAno = qtTotalJaneiro + qtTotalFevereiro + qtTotalMarco + qtTotalAbril + qtTotalMaio + qtTotalJunho + qtTotalJulho + qtTotalAgosto +
                         qtTotalSetembro + qtTotalOutubro + qtTotalNovembro + qtTotalDezembro;
            return qtTotalAno;
        }
        private void MontaGradeGrafico(GridView pGrade, string sInCodigosResiduos)
        {
            DataTable dtGrafico = new DataTable();
            if (pGrade.ClientID == "GradeMTR")
            {
                _dtTodos = new DataTable();
                _dt = oLancamentosMTRDados.PreencheDataTableRGR(Convert.ToInt32(txtCodigoCliente.Text), "20" + intAno.Valor, intMes.Valor, sInCodigosResiduos);
                decimal _tt = AddSomaMediaNaGrade(sInCodigosResiduos, _dt, (_dt.Rows.Count > 1));
                pGrade.DataSource = _dt;
                pGrade.DataBind();
                if (_tt == 0)
                {
                    foreach (DataColumn dc in _dt.Columns)
                    {
                        _dtTodos.Columns.Add(dc.ColumnName);
                    }
                    for (int i = 0; i < 1; i++)
                    {
                        _dtTodos.NewRow();
                        _dtTodos.Rows.Add();
                        if (i == 0)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Recicláveis";
                        _dtTodos.Rows[i][2] = "-";
                        _dtTodos.Rows[i]["JanQuantidade"] = "-";
                        _dtTodos.Rows[i]["FevQuantidade"] = "-";
                        _dtTodos.Rows[i]["MarQuantidade"] = "-";
                        _dtTodos.Rows[i]["AbrQuantidade"] = "-";
                        _dtTodos.Rows[i]["MaiQuantidade"] = "-";
                        _dtTodos.Rows[i]["JunQuantidade"] = "-";
                        _dtTodos.Rows[i]["JulQuantidade"] = "-";
                        _dtTodos.Rows[i]["AgoQuantidade"] = "-";
                        _dtTodos.Rows[i]["SetQuantidade"] = "-";
                        _dtTodos.Rows[i]["OutQuantidade"] = "-";
                        _dtTodos.Rows[i]["NovQuantidade"] = "-";
                        _dtTodos.Rows[i]["DezQuantidade"] = "-";
                        _dtTodos.Rows[i][15] = "-";
                        _dtTodos.Rows[i][16] = "-";
                        _dtTodos.Rows[i][17] = "-";
                    }
                    _dt = _dtTodos;
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    pGrade.Columns[0].ItemStyle.Width = Unit.Parse("550px");
                    for (int ii = 1; ii <= 15; ii++)
                        pGrade.Columns[ii].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                else
                {
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    ZeroTrocaPorIfem(pGrade);

                    dtGrafico = new DataTable();
                    dtGrafico = MontaDtGrafico(dtGrafico, _dt);
                    foreach (DataColumn dc in dtGrafico.Columns)
                    {
                        if (dc.ColumnName != "Mes" && dc.ColumnName.IndexOf("Total") == -1)
                        {
                            Chart1.Series.Add(dc.ColumnName);
                            Chart1.Series[Chart1.Series.Count - 1].MarkerStep = 1;
                            Chart1.Series[Chart1.Series.Count - 1].XValueMember = "Mes";
                            Chart1.Series[Chart1.Series.Count - 1].YValueMembers = dc.ColumnName;
                            if (Chart1.Series.Count <= 1)
                                Chart1.Series[Chart1.Series.Count - 1].Color = Color.IndianRed;
                            if (DropDownList1.Text == "1")
                                Chart1.Series[Chart1.Series.Count - 1].ChartType = SeriesChartType.Area;
                            if (DropDownList1.Text == "2")
                                Chart1.Series[Chart1.Series.Count - 1].ChartType = SeriesChartType.Bar;
                            if (DropDownList1.Text == "3")
                                Chart1.Series[Chart1.Series.Count - 1].ChartType = SeriesChartType.BoxPlot;
                            if (DropDownList1.Text == "4")
                                Chart1.Series[Chart1.Series.Count - 1].ChartType = SeriesChartType.Bubble;
                            if (DropDownList1.Text == "5")
                                Chart1.Series[Chart1.Series.Count - 1].ChartType = SeriesChartType.Candlestick;
                            if (DropDownList1.Text == "6")
                                Chart1.Series[Chart1.Series.Count - 1].ChartType = SeriesChartType.Column;
                            if (DropDownList1.Text == "7")
                                Chart1.Series[Chart1.Series.Count - 1].ChartType = SeriesChartType.Doughnut;
                            if (DropDownList1.Text == "8")
                                Chart1.Series[Chart1.Series.Count - 1].ChartType = SeriesChartType.ErrorBar;
                            if (DropDownList1.Text == "9")
                                Chart1.Series[Chart1.Series.Count - 1].ChartType = SeriesChartType.FastLine;
                            if (DropDownList1.Text == "10")
                                Chart1.Series[Chart1.Series.Count - 1].ChartType = SeriesChartType.FastPoint;
                            if (DropDownList1.Text == "11")
                                Chart1.Series[Chart1.Series.Count - 1].ChartType = SeriesChartType.Funnel;
                            if (DropDownList1.Text == "12")
                                Chart1.Series[Chart1.Series.Count - 1].ChartType = SeriesChartType.Line;
                            if (DropDownList1.Text == "13")
                                Chart1.Series[Chart1.Series.Count - 1].ChartType = SeriesChartType.Pie;
                            if (DropDownList1.Text == "14")
                                Chart1.Series[Chart1.Series.Count - 1].ChartType = SeriesChartType.Point;
                            if (DropDownList1.Text == "15")
                                Chart1.Series[Chart1.Series.Count - 1].ChartType = SeriesChartType.Polar;
                            if (DropDownList1.Text == "16")
                                Chart1.Series[Chart1.Series.Count - 1].ChartType = SeriesChartType.Radar;
                            if (DropDownList1.Text == "17")
                                Chart1.Series[Chart1.Series.Count - 1].ChartType = SeriesChartType.Range;
                            if (DropDownList1.Text == "18")
                                Chart1.Series[Chart1.Series.Count - 1].ChartType = SeriesChartType.RangeBar;
                            if (DropDownList1.Text == "19")
                                Chart1.Series[Chart1.Series.Count - 1].ChartType = SeriesChartType.RangeColumn;
                            if (DropDownList1.Text == "20")
                                Chart1.Series[Chart1.Series.Count - 1].ChartType = SeriesChartType.Stock;
                            if (DropDownList1.Text == "21")
                                Chart1.Series[Chart1.Series.Count - 1].ChartType = SeriesChartType.Spline;
                            if (DropDownList1.Text == "22")
                                Chart1.Series[Chart1.Series.Count - 1].ChartType = SeriesChartType.SplineArea;
                            if (DropDownList1.Text == "23")
                                Chart1.Series[Chart1.Series.Count - 1].ChartType = SeriesChartType.SplineRange;
                            if (DropDownList1.Text == "24")
                                Chart1.Series[Chart1.Series.Count - 1].ChartType = SeriesChartType.StackedArea;
                            if (DropDownList1.Text == "25")
                                Chart1.Series[Chart1.Series.Count - 1].ChartType = SeriesChartType.StackedBar;
                            Legend li = new Legend();
                            li.Name = dc.ColumnName;
                            Font f = new Font("Arial", 7);
                            if (Chart1.Series.Count == 1)
                            {
                                Chart1.Legends.Add(li);
                                Chart1.Legends[Chart1.Legends.Count - 1].Font = f;
                            }
                        }
                    }
                    if (dtGrafico.Columns.Count > 1)
                    {
                        Chart1.Height = 400;
                        Chart1.DataSource = dtGrafico;
                        Chart1.DataBind();
                    }
                    else
                    {
                        Chart1.Height = 1;
                    }
                }
            }
            if (pGrade.ClientID == "GradeReciclagemDesvioAterro")
            {
                // A - Total Gerado (ton)    - com totais de todos os meses 
                // B - Enviado Aterro (ton)  - com totais de todos os meses 
                // C - Total Reciclado (ton) - com totais de todos os meses 
                // % Desvio de Aterro  (C / A)
                sInCodigosResiduos = "94, 41, 234, 92, 34, 61, 144, 21, 228, 62, 9, 116, 117";
                sInCodigosResiduos = sInCodigosResiduos + ", 240, 85, 3, 98";
                sInCodigosResiduos = sInCodigosResiduos + ", 160";
                sInCodigosResiduos = sInCodigosResiduos + ", 170, 171, 101, 102, 105, 164, 165, 137, 141, 32, 122";
                sInCodigosResiduos = sInCodigosResiduos + ", 88, 19, 83, 189, 190";
                sInCodigosResiduos = sInCodigosResiduos + ", 204, 131";
                sInCodigosResiduos = sInCodigosResiduos + ", 177, 81";
                sInCodigosResiduos = sInCodigosResiduos + ", 91, 155, 44, 148, 24, 77";
                _dt = oLancamentosMTRDados.PreencheDataTableRGR(Convert.ToInt32(txtCodigoCliente.Text), "20" + intAno.Valor, intMes.Valor, sInCodigosResiduos);
                _dtTodos = new DataTable();
                foreach (DataColumn dc in _dt.Columns)
                {
                    _dtTodos.Columns.Add(dc.ColumnName);
                    if (dc.ColumnName.IndexOf("Quantidade") > -1)
                        _dtTodos.Columns[dc.ColumnName].DataType = Type.GetType("System.Decimal");
                }
                _dtTodos.NewRow();
                _dtTodos.Rows.Add();
                _dtTodos.Rows[0]["DescricaoReduzida"] = "A-Total Gerado-(ton)";
                _dtTodos.Rows[0]["JanQuantidade"] = 0;
                _dtTodos.Rows[0]["FevQuantidade"] = 0;
                _dtTodos.Rows[0]["MarQuantidade"] = 0;
                _dtTodos.Rows[0]["AbrQuantidade"] = 0;
                _dtTodos.Rows[0]["MaiQuantidade"] = 0;
                _dtTodos.Rows[0]["JunQuantidade"] = 0;
                _dtTodos.Rows[0]["JulQuantidade"] = 0;
                _dtTodos.Rows[0]["AgoQuantidade"] = 0;
                _dtTodos.Rows[0]["SetQuantidade"] = 0;
                _dtTodos.Rows[0]["OutQuantidade"] = 0;
                _dtTodos.Rows[0]["NovQuantidade"] = 0;
                _dtTodos.Rows[0]["DezQuantidade"] = 0;
                foreach (DataRow dr in _dt.Rows)
                {
                    foreach (DataRow drConv in _dtConversao.Rows)
                    {
                        if (drConv["Codigo"].ToString() == dr["CodigoResiduo"].ToString())
                        {
                            if (Convert.ToDecimal(drConv["Peso"]) != 1)
                            {
                                dr[2] = "KG";
                                dr[4] = Convert.ToDecimal(dr[4]) * (Convert.ToDecimal(drConv["Peso"]));
                                dr[5] = Convert.ToDecimal(dr[5]) * (Convert.ToDecimal(drConv["Peso"]));
                                dr[6] = Convert.ToDecimal(dr[6]) * (Convert.ToDecimal(drConv["Peso"]));
                                dr[7] = Convert.ToDecimal(dr[7]) * (Convert.ToDecimal(drConv["Peso"]));
                                dr[8] = Convert.ToDecimal(dr[8]) * (Convert.ToDecimal(drConv["Peso"]));
                                dr[9] = Convert.ToDecimal(dr[9]) * (Convert.ToDecimal(drConv["Peso"]));
                                dr[10] = Convert.ToDecimal(dr[10]) * (Convert.ToDecimal(drConv["Peso"]));
                                dr[11] = Convert.ToDecimal(dr[11]) * (Convert.ToDecimal(drConv["Peso"]));
                                dr[12] = Convert.ToDecimal(dr[12]) * (Convert.ToDecimal(drConv["Peso"]));
                                dr[13] = Convert.ToDecimal(dr[13]) * (Convert.ToDecimal(drConv["Peso"]));
                                dr[14] = Convert.ToDecimal(dr[14]) * (Convert.ToDecimal(drConv["Peso"]));
                                dr[15] = Convert.ToDecimal(dr[15]) * (Convert.ToDecimal(drConv["Peso"]));
                                break;
                            }
                        }
                    }
                    _dtTodos.Rows[0]["JanQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[0]["JanQuantidade"]) + Convert.ToDecimal(dr[4]) / 1000;
                    _dtTodos.Rows[0]["FevQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[0]["FevQuantidade"]) + Convert.ToDecimal(dr[5]) / 1000;
                    _dtTodos.Rows[0]["MarQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[0]["MarQuantidade"]) + Convert.ToDecimal(dr[6]) / 1000;
                    _dtTodos.Rows[0]["AbrQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[0]["AbrQuantidade"]) + Convert.ToDecimal(dr[7]) / 1000;
                    _dtTodos.Rows[0]["MaiQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[0]["MaiQuantidade"]) + Convert.ToDecimal(dr[8]) / 1000;
                    _dtTodos.Rows[0]["JunQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[0]["JunQuantidade"]) + Convert.ToDecimal(dr[9]) / 1000;
                    _dtTodos.Rows[0]["JulQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[0]["JulQuantidade"]) + Convert.ToDecimal(dr[10]) / 1000;
                    _dtTodos.Rows[0]["AgoQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[0]["AgoQuantidade"]) + Convert.ToDecimal(dr[11]) / 1000;
                    _dtTodos.Rows[0]["SetQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[0]["SetQuantidade"]) + Convert.ToDecimal(dr[12]) / 1000;
                    _dtTodos.Rows[0]["OutQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[0]["OutQuantidade"]) + Convert.ToDecimal(dr[13]) / 1000;
                    _dtTodos.Rows[0]["NovQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[0]["NovQuantidade"]) + Convert.ToDecimal(dr[14]) / 1000;
                    _dtTodos.Rows[0]["DezQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[0]["DezQuantidade"]) + Convert.ToDecimal(dr[15]) / 1000;
                }
                _dtTodos.NewRow();
                _dtTodos.Rows.Add();
                _dtTodos.Rows[1]["JanQuantidade"] = 0;
                _dtTodos.Rows[1]["FevQuantidade"] = 0;
                _dtTodos.Rows[1]["MarQuantidade"] = 0;
                _dtTodos.Rows[1]["AbrQuantidade"] = 0;
                _dtTodos.Rows[1]["MaiQuantidade"] = 0;
                _dtTodos.Rows[1]["JunQuantidade"] = 0;
                _dtTodos.Rows[1]["JulQuantidade"] = 0;
                _dtTodos.Rows[1]["AgoQuantidade"] = 0;
                _dtTodos.Rows[1]["SetQuantidade"] = 0;
                _dtTodos.Rows[1]["OutQuantidade"] = 0;
                _dtTodos.Rows[1]["NovQuantidade"] = 0;
                _dtTodos.Rows[1]["DezQuantidade"] = 0;
                _dtTodos.Rows[1]["DescricaoReduzida"] = "B-Enviado Aterro-(ton)";
                foreach (DataRow dr in _dt.Rows)
                {
                    if (dr[0].ToString() == "94" ||
                        dr[0].ToString() == "41" ||
                        dr[0].ToString() == "234" ||
                        dr[0].ToString() == "92" ||
                        dr[0].ToString() == "34" ||
                        dr[0].ToString() == "61" ||
                        dr[0].ToString() == "144" ||
                        dr[0].ToString() == "21" ||
                        dr[0].ToString() == "228" ||
                        dr[0].ToString() == "62" ||
                        dr[0].ToString() == "9" ||
                        dr[0].ToString() == "116" ||
                        dr[0].ToString() == "117")
                    {
                        _dtTodos.Rows[1]["JanQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[1]["JanQuantidade"]) + Convert.ToDecimal(dr[4]) / 1000;
                        _dtTodos.Rows[1]["FevQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[1]["FevQuantidade"]) + Convert.ToDecimal(dr[5]) / 1000;
                        _dtTodos.Rows[1]["MarQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[1]["MarQuantidade"]) + Convert.ToDecimal(dr[6]) / 1000;
                        _dtTodos.Rows[1]["AbrQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[1]["AbrQuantidade"]) + Convert.ToDecimal(dr[7]) / 1000;
                        _dtTodos.Rows[1]["MaiQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[1]["MaiQuantidade"]) + Convert.ToDecimal(dr[8]) / 1000;
                        _dtTodos.Rows[1]["JunQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[1]["JunQuantidade"]) + Convert.ToDecimal(dr[9]) / 1000;
                        _dtTodos.Rows[1]["JulQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[1]["JulQuantidade"]) + Convert.ToDecimal(dr[10]) / 1000;
                        _dtTodos.Rows[1]["AgoQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[1]["AgoQuantidade"]) + Convert.ToDecimal(dr[11]) / 1000;
                        _dtTodos.Rows[1]["SetQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[1]["SetQuantidade"]) + Convert.ToDecimal(dr[12]) / 1000;
                        _dtTodos.Rows[1]["OutQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[1]["OutQuantidade"]) + Convert.ToDecimal(dr[13]) / 1000;
                        _dtTodos.Rows[1]["NovQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[1]["NovQuantidade"]) + Convert.ToDecimal(dr[14]) / 1000;
                        _dtTodos.Rows[1]["DezQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[1]["DezQuantidade"]) + Convert.ToDecimal(dr[15]) / 1000;
                    }
                }
                _dtTodos.NewRow();
                _dtTodos.Rows.Add();
                _dtTodos.Rows[2]["JanQuantidade"] = 0;
                _dtTodos.Rows[2]["FevQuantidade"] = 0;
                _dtTodos.Rows[2]["MarQuantidade"] = 0;
                _dtTodos.Rows[2]["AbrQuantidade"] = 0;
                _dtTodos.Rows[2]["MaiQuantidade"] = 0;
                _dtTodos.Rows[2]["JunQuantidade"] = 0;
                _dtTodos.Rows[2]["JulQuantidade"] = 0;
                _dtTodos.Rows[2]["AgoQuantidade"] = 0;
                _dtTodos.Rows[2]["SetQuantidade"] = 0;
                _dtTodos.Rows[2]["OutQuantidade"] = 0;
                _dtTodos.Rows[2]["NovQuantidade"] = 0;
                _dtTodos.Rows[2]["DezQuantidade"] = 0;
                _dtTodos.Rows[2]["DescricaoReduzida"] = "C-Total Reciclado-(ton)";

                pGrade.DataSource = _dtTodos;
                pGrade.DataBind();

                GridViewRow gvr1 = pGrade.Rows[0];
                _dtTodos.Rows[2]["JanQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[2]["JanQuantidade"]) + Convert.ToDecimal(gvr1.Cells[2].Text);
                _dtTodos.Rows[2]["FevQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[2]["FevQuantidade"]) + Convert.ToDecimal(gvr1.Cells[3].Text);
                _dtTodos.Rows[2]["MarQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[2]["MarQuantidade"]) + Convert.ToDecimal(gvr1.Cells[4].Text);
                _dtTodos.Rows[2]["AbrQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[2]["AbrQuantidade"]) + Convert.ToDecimal(gvr1.Cells[5].Text);
                _dtTodos.Rows[2]["MaiQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[2]["MaiQuantidade"]) + Convert.ToDecimal(gvr1.Cells[6].Text);
                _dtTodos.Rows[2]["JunQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[2]["JunQuantidade"]) + Convert.ToDecimal(gvr1.Cells[7].Text);
                _dtTodos.Rows[2]["JulQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[2]["JulQuantidade"]) + Convert.ToDecimal(gvr1.Cells[8].Text);
                _dtTodos.Rows[2]["AgoQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[2]["AgoQuantidade"]) + Convert.ToDecimal(gvr1.Cells[9].Text);
                _dtTodos.Rows[2]["SetQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[2]["SetQuantidade"]) + Convert.ToDecimal(gvr1.Cells[10].Text);
                _dtTodos.Rows[2]["OutQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[2]["OutQuantidade"]) + Convert.ToDecimal(gvr1.Cells[11].Text);
                _dtTodos.Rows[2]["NovQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[2]["NovQuantidade"]) + Convert.ToDecimal(gvr1.Cells[12].Text);
                _dtTodos.Rows[2]["DezQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[2]["DezQuantidade"]) + Convert.ToDecimal(gvr1.Cells[13].Text);

                GridViewRow gvr2 = pGrade.Rows[1];
                _dtTodos.Rows[2]["JanQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[2]["JanQuantidade"]) - Convert.ToDecimal(gvr2.Cells[2].Text);
                _dtTodos.Rows[2]["FevQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[2]["FevQuantidade"]) - Convert.ToDecimal(gvr2.Cells[3].Text);
                _dtTodos.Rows[2]["MarQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[2]["MarQuantidade"]) - Convert.ToDecimal(gvr2.Cells[4].Text);
                _dtTodos.Rows[2]["AbrQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[2]["AbrQuantidade"]) - Convert.ToDecimal(gvr2.Cells[5].Text);
                _dtTodos.Rows[2]["MaiQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[2]["MaiQuantidade"]) - Convert.ToDecimal(gvr2.Cells[6].Text);
                _dtTodos.Rows[2]["JunQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[2]["JunQuantidade"]) - Convert.ToDecimal(gvr2.Cells[7].Text);
                _dtTodos.Rows[2]["JulQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[2]["JulQuantidade"]) - Convert.ToDecimal(gvr2.Cells[8].Text);
                _dtTodos.Rows[2]["AgoQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[2]["AgoQuantidade"]) - Convert.ToDecimal(gvr2.Cells[9].Text);
                _dtTodos.Rows[2]["SetQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[2]["SetQuantidade"]) - Convert.ToDecimal(gvr2.Cells[10].Text);
                _dtTodos.Rows[2]["OutQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[2]["OutQuantidade"]) - Convert.ToDecimal(gvr2.Cells[11].Text);
                _dtTodos.Rows[2]["NovQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[2]["NovQuantidade"]) - Convert.ToDecimal(gvr2.Cells[12].Text);
                _dtTodos.Rows[2]["DezQuantidade"] = Convert.ToDecimal(_dtTodos.Rows[2]["DezQuantidade"]) - Convert.ToDecimal(gvr2.Cells[13].Text);

                _dtTodos.NewRow();
                _dtTodos.Rows.Add();
                if (gvr1.Cells[2].Text != "0" && gvr1.Cells[2].Text != "0,00")
                    _dtTodos.Rows[3]["JanQuantidade"] = ((Convert.ToDecimal(gvr1.Cells[2].Text) - Convert.ToDecimal(gvr2.Cells[2].Text)) / Convert.ToDecimal(gvr1.Cells[2].Text) * 100).ToString("N0");
                if (gvr1.Cells[3].Text != "0" && gvr1.Cells[3].Text != "0,00")
                    _dtTodos.Rows[3]["FevQuantidade"] = ((Convert.ToDecimal(gvr1.Cells[3].Text) - Convert.ToDecimal(gvr2.Cells[3].Text)) / Convert.ToDecimal(gvr1.Cells[3].Text) * 100).ToString("N0");
                if (gvr1.Cells[4].Text != "0" && gvr1.Cells[4].Text != "0,00")
                    _dtTodos.Rows[3]["MarQuantidade"] = ((Convert.ToDecimal(gvr1.Cells[4].Text) - Convert.ToDecimal(gvr2.Cells[4].Text)) / Convert.ToDecimal(gvr1.Cells[4].Text) * 100).ToString("N0");
                if (gvr1.Cells[5].Text != "0" && gvr1.Cells[5].Text != "0,00")
                    _dtTodos.Rows[3]["AbrQuantidade"] = ((Convert.ToDecimal(gvr1.Cells[5].Text) - Convert.ToDecimal(gvr2.Cells[5].Text)) / Convert.ToDecimal(gvr1.Cells[5].Text) * 100).ToString("N0");
                if (gvr1.Cells[6].Text != "0" && gvr1.Cells[6].Text != "0,00")
                    _dtTodos.Rows[3]["MaiQuantidade"] = ((Convert.ToDecimal(gvr1.Cells[6].Text) - Convert.ToDecimal(gvr2.Cells[6].Text)) / Convert.ToDecimal(gvr1.Cells[6].Text) * 100).ToString("N0");
                if (gvr1.Cells[7].Text != "0" && gvr1.Cells[7].Text != "0,00")
                    _dtTodos.Rows[3]["JunQuantidade"] = ((Convert.ToDecimal(gvr1.Cells[7].Text) - Convert.ToDecimal(gvr2.Cells[7].Text)) / Convert.ToDecimal(gvr1.Cells[7].Text) * 100).ToString("N0");
                if (gvr1.Cells[8].Text != "0" && gvr1.Cells[8].Text != "0,00")
                    _dtTodos.Rows[3]["JulQuantidade"] = ((Convert.ToDecimal(gvr1.Cells[8].Text) - Convert.ToDecimal(gvr2.Cells[8].Text)) / Convert.ToDecimal(gvr1.Cells[8].Text) * 100).ToString("N0");
                if (gvr1.Cells[9].Text != "0" && gvr1.Cells[9].Text != "0,00")
                    _dtTodos.Rows[3]["AgoQuantidade"] = ((Convert.ToDecimal(gvr1.Cells[9].Text) - Convert.ToDecimal(gvr2.Cells[9].Text)) / Convert.ToDecimal(gvr1.Cells[9].Text) * 100).ToString("N0");
                if (gvr1.Cells[10].Text != "0" && gvr1.Cells[10].Text != "0,00")
                    _dtTodos.Rows[3]["SetQuantidade"] = ((Convert.ToDecimal(gvr1.Cells[10].Text) - Convert.ToDecimal(gvr2.Cells[10].Text)) / Convert.ToDecimal(gvr1.Cells[10].Text) * 100).ToString("N0");
                if (gvr1.Cells[11].Text != "0" && gvr1.Cells[11].Text != "0,00")
                    _dtTodos.Rows[3]["OutQuantidade"] = ((Convert.ToDecimal(gvr1.Cells[11].Text) - Convert.ToDecimal(gvr2.Cells[11].Text)) / Convert.ToDecimal(gvr1.Cells[11].Text) * 100).ToString("N0");
                if (gvr1.Cells[12].Text != "0" && gvr1.Cells[12].Text != "0,00")
                    _dtTodos.Rows[3]["NovQuantidade"] = ((Convert.ToDecimal(gvr1.Cells[12].Text) - Convert.ToDecimal(gvr2.Cells[12].Text)) / Convert.ToDecimal(gvr1.Cells[12].Text) * 100).ToString("N0");
                if (gvr1.Cells[13].Text != "0" && gvr1.Cells[13].Text != "0,00")
                    _dtTodos.Rows[3]["DezQuantidade"] = ((Convert.ToDecimal(gvr1.Cells[13].Text) - Convert.ToDecimal(gvr2.Cells[13].Text)) / Convert.ToDecimal(gvr1.Cells[13].Text) * 100).ToString("N0");
                _dtTodos.Rows[3]["DescricaoReduzida"] = "Percentual Desvio Aterro (C/A)";

                AddSomaMediaNaGrade("", _dtTodos, false);
                pGrade.DataSource = _dtTodos;
                pGrade.DataBind();
                ZeroTrocaPorIfem(pGrade);

                dtGrafico = new DataTable();
                dtGrafico = MontaDtGrafico(dtGrafico, _dtTodos);
                foreach (DataColumn dc in dtGrafico.Columns)
                {
                    if (dc.ColumnName != "Mes" && dc.ColumnName.IndexOf("Total_Reciclado") > -1)
                    {
                        ChartReciclagemDesvioAterro.Series.Add(dc.ColumnName);
                        ChartReciclagemDesvioAterro.Series[ChartReciclagemDesvioAterro.Series.Count - 1].MarkerStep = 1;
                        ChartReciclagemDesvioAterro.Series[ChartReciclagemDesvioAterro.Series.Count - 1].XValueMember = "Mes";
                        ChartReciclagemDesvioAterro.Series[ChartReciclagemDesvioAterro.Series.Count - 1].YValueMembers = dc.ColumnName;
                        ChartReciclagemDesvioAterro.Series[ChartReciclagemDesvioAterro.Series.Count - 1].Color = Color.IndianRed;
                        Legend li = new Legend();
                        li.Name = dc.ColumnName;
                        Font f = new Font("Arial", 7);

                        ChartReciclagemDesvioAterro.Legends.Add(li);
                        ChartReciclagemDesvioAterro.Legends[ChartReciclagemDesvioAterro.Legends.Count - 1].Font = f;
                    }
                }
                ChartReciclagemDesvioAterro.Height = 400;
                ChartReciclagemDesvioAterro.DataSource = dtGrafico;
                ChartReciclagemDesvioAterro.DataBind();
            }
            if (pGrade.ClientID == "GradeCompostagem")
            {
                _dtTodos = new DataTable();
                _dt = oLancamentosMTRDados.PreencheDataTableRGR(Convert.ToInt32(txtCodigoCliente.Text), "20" + intAno.Valor, intMes.Valor, sInCodigosResiduos);
                decimal _tt = AddSomaMediaNaGrade(sInCodigosResiduos, _dt, (_dt.Rows.Count > 1));
                pGrade.DataSource = _dt;
                pGrade.DataBind();
                if (_tt == 0)
                {
                    foreach (DataColumn dc in _dt.Columns)
                    {
                        _dtTodos.Columns.Add(dc.ColumnName);
                    }
                    for (int i = 0; i <= 1; i++)
                    {
                        _dtTodos.NewRow();
                        _dtTodos.Rows.Add();
                        if (i == 0)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Compostagem";
                        if (i == 1)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Poda e Jardim";
                        _dtTodos.Rows[i][2] = "-";
                        _dtTodos.Rows[i]["JanQuantidade"] = "-";
                        _dtTodos.Rows[i]["FevQuantidade"] = "-";
                        _dtTodos.Rows[i]["MarQuantidade"] = "-";
                        _dtTodos.Rows[i]["AbrQuantidade"] = "-";
                        _dtTodos.Rows[i]["MaiQuantidade"] = "-";
                        _dtTodos.Rows[i]["JunQuantidade"] = "-";
                        _dtTodos.Rows[i]["JulQuantidade"] = "-";
                        _dtTodos.Rows[i]["AgoQuantidade"] = "-";
                        _dtTodos.Rows[i]["SetQuantidade"] = "-";
                        _dtTodos.Rows[i]["OutQuantidade"] = "-";
                        _dtTodos.Rows[i]["NovQuantidade"] = "-";
                        _dtTodos.Rows[i]["DezQuantidade"] = "-";
                        _dtTodos.Rows[i][15] = "-";
                        _dtTodos.Rows[i][16] = "-";
                        _dtTodos.Rows[i][17] = "-";
                    }
                    _dt = _dtTodos;
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    pGrade.Columns[0].ItemStyle.Width = Unit.Parse("550px");
                    for (int ii = 1; ii <= 15; ii++)
                        pGrade.Columns[ii].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                else
                {
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    ZeroTrocaPorIfem(pGrade);

                    dtGrafico = new DataTable();
                    dtGrafico = MontaDtGrafico(dtGrafico, _dt);
                    foreach (DataColumn dc in dtGrafico.Columns)
                    {
                        if (dc.ColumnName != "Mes" && dc.ColumnName.IndexOf("Total") == -1)
                        {
                            Chart2.Series.Add(dc.ColumnName);
                            Chart2.Series[Chart2.Series.Count - 1].MarkerStep = 1;
                            Chart2.Series[Chart2.Series.Count - 1].XValueMember = "Mes";
                            Chart2.Series[Chart2.Series.Count - 1].YValueMembers = dc.ColumnName;
                            Chart2.Series[Chart2.Series.Count - 1].Color = Color.IndianRed;
                            Legend li = new Legend();
                            li.Name = dc.ColumnName;
                            Font f = new Font("Arial", 7);

                            Chart2.Legends.Add(li);
                            Chart2.Legends[Chart2.Legends.Count - 1].Font = f;
                        }
                    }
                    if (dtGrafico.Columns.Count > 1)
                    {
                        Chart2.Height = 400;
                        Chart2.DataSource = dtGrafico;
                        Chart2.DataBind();
                    }
                    else
                    {
                        Chart2.Height = 1;
                    }
                }
            }
            if (pGrade.ClientID == "GradeResiduoComum")
            {
                _dtTodos = new DataTable();
                _dt = oLancamentosMTRDados.PreencheDataTableRGR(Convert.ToInt32(txtCodigoCliente.Text), "20" + intAno.Valor, intMes.Valor, sInCodigosResiduos);
                decimal _tt = AddSomaMediaNaGrade(sInCodigosResiduos, _dt, (_dt.Rows.Count > 1));
                pGrade.DataSource = _dt;
                pGrade.DataBind();
                if (_tt == 0)
                {
                    foreach (DataColumn dc in _dt.Columns)
                    {
                        _dtTodos.Columns.Add(dc.ColumnName);
                    }
                    for (int i = 0; i <= 1; i++)
                    {
                        _dtTodos.NewRow();
                        _dtTodos.Rows.Add();
                        if (i == 0)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Resíduo Comum (kg)";
                        if (i == 1)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Resíduo Comum (m³)";
                        _dtTodos.Rows[i][2] = "-";
                        _dtTodos.Rows[i]["JanQuantidade"] = "-";
                        _dtTodos.Rows[i]["FevQuantidade"] = "-";
                        _dtTodos.Rows[i]["MarQuantidade"] = "-";
                        _dtTodos.Rows[i]["AbrQuantidade"] = "-";
                        _dtTodos.Rows[i]["MaiQuantidade"] = "-";
                        _dtTodos.Rows[i]["JunQuantidade"] = "-";
                        _dtTodos.Rows[i]["JulQuantidade"] = "-";
                        _dtTodos.Rows[i]["AgoQuantidade"] = "-";
                        _dtTodos.Rows[i]["SetQuantidade"] = "-";
                        _dtTodos.Rows[i]["OutQuantidade"] = "-";
                        _dtTodos.Rows[i]["NovQuantidade"] = "-";
                        _dtTodos.Rows[i]["DezQuantidade"] = "-";
                        _dtTodos.Rows[i][15] = "-";
                        _dtTodos.Rows[i][16] = "-";
                        _dtTodos.Rows[i][17] = "-";
                    }
                    _dt = _dtTodos;
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    pGrade.Columns[0].ItemStyle.Width = Unit.Parse("550px");
                    for (int ii = 1; ii <= 15; ii++)
                        pGrade.Columns[ii].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                else
                {
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    ZeroTrocaPorIfem(pGrade);

                    dtGrafico = new DataTable();
                    dtGrafico = MontaDtGrafico(dtGrafico, _dt);
                    foreach (DataColumn dc in dtGrafico.Columns)
                    {
                        if (dc.ColumnName != "Mes" && dc.ColumnName.IndexOf("Total") == -1)
                        {
                            Chart3.Series.Add(dc.ColumnName);
                            Chart3.Series[Chart3.Series.Count - 1].MarkerStep = 1;
                            Chart3.Series[Chart3.Series.Count - 1].XValueMember = "Mes";
                            Chart3.Series[Chart3.Series.Count - 1].YValueMembers = dc.ColumnName;
                            Chart3.Series[Chart3.Series.Count - 1].Color = Color.IndianRed;
                            Legend li = new Legend();
                            li.Name = dc.ColumnName;
                            Font f = new Font("Arial", 7);

                            Chart3.Legends.Add(li);
                            Chart3.Legends[Chart3.Legends.Count - 1].Font = f;
                        }
                    }
                    if (dtGrafico.Columns.Count > 1)
                    {
                        Chart3.Height = 400;
                        Chart3.DataSource = dtGrafico;
                        Chart3.DataBind();
                    }
                    else
                    {
                        Chart3.Height = 1;
                    }
                }
            }

            if (pGrade.ClientID == "GradeRCD")
            {
                _dtTodos = new DataTable();
                _dt = oLancamentosMTRDados.PreencheDataTableRGR(Convert.ToInt32(txtCodigoCliente.Text), "20" + intAno.Valor, intMes.Valor, sInCodigosResiduos);
                decimal _tt = AddSomaMediaNaGrade(sInCodigosResiduos, _dt, (_dt.Rows.Count > 1));
                pGrade.DataSource = _dt;
                pGrade.DataBind();
                if (_tt == 0)
                {
                    foreach (DataColumn dc in _dt.Columns)
                    {
                        _dtTodos.Columns.Add(dc.ColumnName);
                    }
                    for (int i = 0; i <= 4; i++)
                    {
                        _dtTodos.NewRow();
                        _dtTodos.Rows.Add();
                        if (i == 0)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "RCD-Resíduo Construção e Demolição m³";
                        if (i == 1)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Madeira m³";
                        if (i == 2)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Gesso Limpo (Kg)";
                        if (i == 3)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Gesso Acartonado (Kg)";
                        if (i == 4)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Resíduo de Amianto (Telhas, Cx Dágua, etc) (Kg)";
                        _dtTodos.Rows[i][2] = "-";
                        _dtTodos.Rows[i]["JanQuantidade"] = "-";
                        _dtTodos.Rows[i]["FevQuantidade"] = "-";
                        _dtTodos.Rows[i]["MarQuantidade"] = "-";
                        _dtTodos.Rows[i]["AbrQuantidade"] = "-";
                        _dtTodos.Rows[i]["MaiQuantidade"] = "-";
                        _dtTodos.Rows[i]["JunQuantidade"] = "-";
                        _dtTodos.Rows[i]["JulQuantidade"] = "-";
                        _dtTodos.Rows[i]["AgoQuantidade"] = "-";
                        _dtTodos.Rows[i]["SetQuantidade"] = "-";
                        _dtTodos.Rows[i]["OutQuantidade"] = "-";
                        _dtTodos.Rows[i]["NovQuantidade"] = "-";
                        _dtTodos.Rows[i]["DezQuantidade"] = "-";
                        _dtTodos.Rows[i][15] = "-";
                        _dtTodos.Rows[i][16] = "-";
                        _dtTodos.Rows[i][17] = "-";
                    }
                    _dt = _dtTodos;
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    pGrade.Columns[0].ItemStyle.Width = Unit.Parse("550px");
                    for (int ii = 1; ii <= 15; ii++)
                        pGrade.Columns[ii].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                else
                {
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    ZeroTrocaPorIfem(pGrade);

                    dtGrafico = new DataTable();
                    dtGrafico = MontaDtGrafico(dtGrafico, _dt);
                    foreach (DataColumn dc in dtGrafico.Columns)
                    {
                        if (dc.ColumnName != "Mes" && dc.ColumnName.IndexOf("Total") == -1)
                        {
                            Chart4.Series.Add(dc.ColumnName);
                            Chart4.Series[Chart4.Series.Count - 1].MarkerStep = 1;
                            Chart4.Series[Chart4.Series.Count - 1].XValueMember = "Mes";
                            Chart4.Series[Chart4.Series.Count - 1].YValueMembers = dc.ColumnName;
                            Chart4.Series[Chart4.Series.Count - 1].Color = Color.IndianRed;
                            Legend li = new Legend();
                            li.Name = dc.ColumnName;
                            Font f = new Font("Arial", 7);

                            Chart4.Legends.Add(li);
                            Chart4.Legends[Chart4.Legends.Count - 1].Font = f;
                        }
                    }
                    if (dtGrafico.Columns.Count > 1)
                    {
                        Chart4.Height = 400;
                        Chart4.DataSource = dtGrafico;
                        Chart4.DataBind();
                    }
                    else
                    {
                        Chart4.Height = 1;
                    }
                }
            }

            if (pGrade.ClientID == "GradeSolidosContaminados")
            {
                _dtTodos = new DataTable();
                _dt = oLancamentosMTRDados.PreencheDataTableRGR(Convert.ToInt32(txtCodigoCliente.Text), "20" + intAno.Valor, intMes.Valor, sInCodigosResiduos);
                decimal _tt = AddSomaMediaNaGrade(sInCodigosResiduos, _dt, (_dt.Rows.Count > 1));
                pGrade.DataSource = _dt;
                pGrade.DataBind();
                if (_tt == 0)
                {
                    foreach (DataColumn dc in _dt.Columns)
                    {
                        _dtTodos.Columns.Add(dc.ColumnName);
                    }
                    for (int i = 0; i <= 3; i++)
                    {
                        _dtTodos.NewRow();
                        _dtTodos.Rows.Add();
                        if (i == 0)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Contaminados com Óleos e Tintas";
                        if (i == 1)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Contaminados com Produtos Químicos";
                        if (i == 2)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Embalagens de Saneantes";
                        if (i == 3)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "EPIs usados";
                        _dtTodos.Rows[i][2] = "-";
                        _dtTodos.Rows[i]["JanQuantidade"] = "-";
                        _dtTodos.Rows[i]["FevQuantidade"] = "-";
                        _dtTodos.Rows[i]["MarQuantidade"] = "-";
                        _dtTodos.Rows[i]["AbrQuantidade"] = "-";
                        _dtTodos.Rows[i]["MaiQuantidade"] = "-";
                        _dtTodos.Rows[i]["JunQuantidade"] = "-";
                        _dtTodos.Rows[i]["JulQuantidade"] = "-";
                        _dtTodos.Rows[i]["AgoQuantidade"] = "-";
                        _dtTodos.Rows[i]["SetQuantidade"] = "-";
                        _dtTodos.Rows[i]["OutQuantidade"] = "-";
                        _dtTodos.Rows[i]["NovQuantidade"] = "-";
                        _dtTodos.Rows[i]["DezQuantidade"] = "-";
                        _dtTodos.Rows[i][15] = "-";
                        _dtTodos.Rows[i][16] = "-";
                        _dtTodos.Rows[i][17] = "-";
                    }
                    _dt = _dtTodos;
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    pGrade.Columns[0].ItemStyle.Width = Unit.Parse("550px");
                    for (int ii = 1; ii <= 15; ii++)
                        pGrade.Columns[ii].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                else
                {
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    ZeroTrocaPorIfem(pGrade);

                    dtGrafico = new DataTable();
                    dtGrafico = MontaDtGrafico(dtGrafico, _dt);
                    foreach (DataColumn dc in dtGrafico.Columns)
                    {
                        if (dc.ColumnName != "Mes" && dc.ColumnName.IndexOf("Total") == -1)
                        {
                            Chart5.Series.Add(dc.ColumnName);
                            Chart5.Series[Chart5.Series.Count - 1].MarkerStep = 1;
                            Chart5.Series[Chart5.Series.Count - 1].XValueMember = "Mes";
                            Chart5.Series[Chart5.Series.Count - 1].YValueMembers = dc.ColumnName;
                            Chart5.Series[Chart5.Series.Count - 1].Color = Color.IndianRed;
                            Legend li = new Legend();
                            li.Name = dc.ColumnName;
                            Font f = new Font("Arial", 7);

                            Chart5.Legends.Add(li);
                            Chart5.Legends[Chart5.Legends.Count - 1].Font = f;
                        }
                    }
                    if (dtGrafico.Columns.Count > 1)
                    {
                        Chart5.Height = 400;
                        Chart5.DataSource = dtGrafico;
                        Chart5.DataBind();
                    }
                    else
                    {
                        Chart5.Height = 1;
                    }
                }
            }
            if (pGrade.ClientID == "GradeEletroeletronicosReciclaveis")
            {
                _dtTodos = new DataTable();
                _dt = oLancamentosMTRDados.PreencheDataTableRGR(Convert.ToInt32(txtCodigoCliente.Text), "20" + intAno.Valor, intMes.Valor, sInCodigosResiduos);
                decimal _tt = AddSomaMediaNaGrade(sInCodigosResiduos, _dt, (_dt.Rows.Count > 1));
                pGrade.DataSource = _dt;
                pGrade.DataBind();
                if (_tt == 0)
                {
                    foreach (DataColumn dc in _dt.Columns)
                    {
                        _dtTodos.Columns.Add(dc.ColumnName);
                    }
                    for (int i = 0; i <= 4; i++)
                    {
                        _dtTodos.NewRow();
                        _dtTodos.Rows.Add();
                        if (i == 0)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Cabos e Fontes";
                        if (i == 1)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Fios Elétricos";
                        if (i == 2)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Placas com Componentes";
                        if (i == 3)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Reatores";
                        if (i == 4)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Resíduos Eletroeletrônicos Recicláveis";
                        _dtTodos.Rows[i][2] = "-";
                        _dtTodos.Rows[i]["JanQuantidade"] = "-";
                        _dtTodos.Rows[i]["FevQuantidade"] = "-";
                        _dtTodos.Rows[i]["MarQuantidade"] = "-";
                        _dtTodos.Rows[i]["AbrQuantidade"] = "-";
                        _dtTodos.Rows[i]["MaiQuantidade"] = "-";
                        _dtTodos.Rows[i]["JunQuantidade"] = "-";
                        _dtTodos.Rows[i]["JulQuantidade"] = "-";
                        _dtTodos.Rows[i]["AgoQuantidade"] = "-";
                        _dtTodos.Rows[i]["SetQuantidade"] = "-";
                        _dtTodos.Rows[i]["OutQuantidade"] = "-";
                        _dtTodos.Rows[i]["NovQuantidade"] = "-";
                        _dtTodos.Rows[i]["DezQuantidade"] = "-";
                        _dtTodos.Rows[i][15] = "-";
                        _dtTodos.Rows[i][16] = "-";
                        _dtTodos.Rows[i][17] = "-";
                    }
                    _dt = _dtTodos;
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    pGrade.Columns[0].ItemStyle.Width = Unit.Parse("550px");
                    for (int ii = 1; ii <= 15; ii++)
                        pGrade.Columns[ii].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                else
                {
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    ZeroTrocaPorIfem(pGrade);

                    dtGrafico = new DataTable();
                    dtGrafico = MontaDtGrafico(dtGrafico, _dt);

                    foreach (DataColumn dc in dtGrafico.Columns)
                    {
                        if (dc.ColumnName != "Mes" && dc.ColumnName.IndexOf("Total") == -1)
                        {
                            Chart6.Series.Add(dc.ColumnName);
                            Chart6.Series[Chart6.Series.Count - 1].MarkerStep = 1;
                            Chart6.Series[Chart6.Series.Count - 1].XValueMember = "Mes";
                            Chart6.Series[Chart6.Series.Count - 1].YValueMembers = dc.ColumnName;
                            Chart6.Series[Chart6.Series.Count - 1].Color = Color.IndianRed;
                            Legend li = new Legend();
                            li.Name = dc.ColumnName;
                            Font f = new Font("Arial", 7);

                            Chart6.Legends.Add(li);
                            Chart6.Legends[Chart6.Legends.Count - 1].Font = f;
                        }
                    }
                    if (dtGrafico.Columns.Count > 1)
                    {
                        Chart6.Height = 400;
                        Chart6.DataSource = dtGrafico;
                        Chart6.DataBind();
                    }
                    else
                    {
                        Chart6.Height = 1;
                    }
                }
            }
            if (pGrade.ClientID == "GradeNaoReciclaveis")
            {
                _dtTodos = new DataTable();
                _dt = oLancamentosMTRDados.PreencheDataTableRGR(Convert.ToInt32(txtCodigoCliente.Text), "20" + intAno.Valor, intMes.Valor, sInCodigosResiduos);
                decimal _tt = AddSomaMediaNaGrade(sInCodigosResiduos, _dt, (_dt.Rows.Count > 1));
                pGrade.DataSource = _dt;
                pGrade.DataBind();
                if (_tt == 0)
                {
                    foreach (DataColumn dc in _dt.Columns)
                    {
                        _dtTodos.Columns.Add(dc.ColumnName);
                    }
                    for (int i = 0; i <= 3; i++)
                    {
                        _dtTodos.NewRow();
                        _dtTodos.Rows.Add();
                        if (i == 0)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Componentes Eletrônicos";
                        if (i == 1)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Eletroeletrônicos Não Reciclaveis";
                        if (i == 2)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Equipamentos Eletrodomésticos";
                        if (i == 3)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Placas Sem Componentes";
                        _dtTodos.Rows[i][2] = "-";
                        _dtTodos.Rows[i]["JanQuantidade"] = "-";
                        _dtTodos.Rows[i]["FevQuantidade"] = "-";
                        _dtTodos.Rows[i]["MarQuantidade"] = "-";
                        _dtTodos.Rows[i]["AbrQuantidade"] = "-";
                        _dtTodos.Rows[i]["MaiQuantidade"] = "-";
                        _dtTodos.Rows[i]["JunQuantidade"] = "-";
                        _dtTodos.Rows[i]["JulQuantidade"] = "-";
                        _dtTodos.Rows[i]["AgoQuantidade"] = "-";
                        _dtTodos.Rows[i]["SetQuantidade"] = "-";
                        _dtTodos.Rows[i]["OutQuantidade"] = "-";
                        _dtTodos.Rows[i]["NovQuantidade"] = "-";
                        _dtTodos.Rows[i]["DezQuantidade"] = "-";
                        _dtTodos.Rows[i][15] = "-";
                        _dtTodos.Rows[i][16] = "-";
                        _dtTodos.Rows[i][17] = "-";
                    }
                    _dt = _dtTodos;
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    pGrade.Columns[0].ItemStyle.Width = Unit.Parse("550px");
                    for (int ii = 1; ii <= 15; ii++)
                        pGrade.Columns[ii].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                else
                {
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    ZeroTrocaPorIfem(pGrade);

                    dtGrafico = new DataTable();
                    dtGrafico = MontaDtGrafico(dtGrafico, _dt);

                    foreach (DataColumn dc in dtGrafico.Columns)
                    {
                        if (dc.ColumnName != "Mes" && dc.ColumnName.IndexOf("Total") == -1)
                        {
                            Chart7.Series.Add(dc.ColumnName);
                            Chart7.Series[Chart7.Series.Count - 1].MarkerStep = 1;
                            Chart7.Series[Chart7.Series.Count - 1].XValueMember = "Mes";
                            Chart7.Series[Chart7.Series.Count - 1].YValueMembers = dc.ColumnName;
                            Chart7.Series[Chart7.Series.Count - 1].Color = Color.IndianRed;
                            Legend li = new Legend();
                            li.Name = dc.ColumnName;
                            Font f = new Font("Arial", 7);

                            Chart7.Legends.Add(li);
                            Chart7.Legends[Chart7.Legends.Count - 1].Font = f;
                        }
                    }
                    if (dtGrafico.Columns.Count > 1)
                    {
                        Chart7.Height = 400;
                        Chart7.DataSource = dtGrafico;
                        Chart7.DataBind();
                    }
                    else
                    {
                        Chart7.Height = 1;
                    }
                }
            }
            if (pGrade.ClientID == "GradeLampadasFluorescentes")
            {
                _dtTodos = new DataTable();
                _dt = oLancamentosMTRDados.PreencheDataTableRGR(Convert.ToInt32(txtCodigoCliente.Text), "20" + intAno.Valor, intMes.Valor, sInCodigosResiduos);
                decimal _tt = AddSomaMediaNaGrade(sInCodigosResiduos, _dt, (_dt.Rows.Count > 1));
                pGrade.DataSource = _dt;
                pGrade.DataBind();
                if (_tt == 0)
                {
                    foreach (DataColumn dc in _dt.Columns)
                    {
                        _dtTodos.Columns.Add(dc.ColumnName);
                    }
                    for (int i = 0; i <= 2; i++)
                    {
                        _dtTodos.NewRow();
                        _dtTodos.Rows.Add();
                        if (i == 0)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Lâmpadas Fluorescentes acima de 1,20m";
                        if (i == 1)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Lâmpadas Fluorescentes até 1,20m";
                        if (i == 2)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Lâmpadas Quebradas";
                        _dtTodos.Rows[i][2] = "-";
                        _dtTodos.Rows[i]["JanQuantidade"] = "-";
                        _dtTodos.Rows[i]["FevQuantidade"] = "-";
                        _dtTodos.Rows[i]["MarQuantidade"] = "-";
                        _dtTodos.Rows[i]["AbrQuantidade"] = "-";
                        _dtTodos.Rows[i]["MaiQuantidade"] = "-";
                        _dtTodos.Rows[i]["JunQuantidade"] = "-";
                        _dtTodos.Rows[i]["JulQuantidade"] = "-";
                        _dtTodos.Rows[i]["AgoQuantidade"] = "-";
                        _dtTodos.Rows[i]["SetQuantidade"] = "-";
                        _dtTodos.Rows[i]["OutQuantidade"] = "-";
                        _dtTodos.Rows[i]["NovQuantidade"] = "-";
                        _dtTodos.Rows[i]["DezQuantidade"] = "-";
                        _dtTodos.Rows[i][15] = "-";
                        _dtTodos.Rows[i][16] = "-";
                        _dtTodos.Rows[i][17] = "-";
                    }
                    _dt = _dtTodos;
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    pGrade.Columns[0].ItemStyle.Width = Unit.Parse("550px");
                    for (int ii = 1; ii <= 15; ii++)
                        pGrade.Columns[ii].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                else
                {
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    ZeroTrocaPorIfem(pGrade);

                    dtGrafico = new DataTable();
                    dtGrafico = MontaDtGrafico(dtGrafico, _dt);

                    foreach (DataColumn dc in dtGrafico.Columns)
                    {
                        if (dc.ColumnName != "Mes" && dc.ColumnName.IndexOf("Total") == -1)
                        {
                            Chart8.Series.Add(dc.ColumnName);
                            Chart8.Series[Chart8.Series.Count - 1].MarkerStep = 1;
                            Chart8.Series[Chart8.Series.Count - 1].XValueMember = "Mes";
                            Chart8.Series[Chart8.Series.Count - 1].YValueMembers = dc.ColumnName;
                            if (Chart8.Series.Count <= 1)
                                Chart8.Series[Chart8.Series.Count - 1].Color = Color.IndianRed;
                            Legend li = new Legend();
                            li.Name = dc.ColumnName;
                            Font f = new Font("Arial", 7);
                            if (Chart8.Series.Count <= 1)
                            {
                                Chart8.Legends.Add(li);
                                Chart8.Legends[Chart8.Legends.Count - 1].Font = f;
                            }
                        }
                    }
                    if (dtGrafico.Columns.Count > 1)
                    {
                        Chart8.Height = 400;
                        Chart8.DataSource = dtGrafico;
                        Chart8.DataBind();
                    }
                    else
                    {
                        Chart8.Height = 1;
                    }
                }
            }
            if (pGrade.ClientID == "GradePilhasBaterias")
            {
                _dtTodos = new DataTable();
                _dt = oLancamentosMTRDados.PreencheDataTableRGR(Convert.ToInt32(txtCodigoCliente.Text), "20" + intAno.Valor, intMes.Valor, sInCodigosResiduos);
                decimal _tt = AddSomaMediaNaGrade(sInCodigosResiduos, _dt, (_dt.Rows.Count > 1));
                if (_tt == 0)
                {
                    foreach (DataColumn dc in _dt.Columns)
                    {
                        _dtTodos.Columns.Add(dc.ColumnName);
                    }
                    for (int i = 0; i <= 1; i++)
                    {
                        _dtTodos.NewRow();
                        _dtTodos.Rows.Add();
                        if (i == 0)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Pilhas e Baterias Diversas";
                        if (i == 1)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Baterias Automotivas";
                        _dtTodos.Rows[i][2] = "-";
                        _dtTodos.Rows[i]["JanQuantidade"] = "-";
                        _dtTodos.Rows[i]["FevQuantidade"] = "-";
                        _dtTodos.Rows[i]["MarQuantidade"] = "-";
                        _dtTodos.Rows[i]["AbrQuantidade"] = "-";
                        _dtTodos.Rows[i]["MaiQuantidade"] = "-";
                        _dtTodos.Rows[i]["JunQuantidade"] = "-";
                        _dtTodos.Rows[i]["JulQuantidade"] = "-";
                        _dtTodos.Rows[i]["AgoQuantidade"] = "-";
                        _dtTodos.Rows[i]["SetQuantidade"] = "-";
                        _dtTodos.Rows[i]["OutQuantidade"] = "-";
                        _dtTodos.Rows[i]["NovQuantidade"] = "-";
                        _dtTodos.Rows[i]["DezQuantidade"] = "-";
                        _dtTodos.Rows[i][15] = "-";
                        _dtTodos.Rows[i][16] = "-";
                        _dtTodos.Rows[i][17] = "-";
                    }
                    _dt = _dtTodos;
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    pGrade.Columns[0].ItemStyle.Width = Unit.Parse("550px");
                    for (int ii = 1; ii <= 15; ii++)
                        pGrade.Columns[ii].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                else
                {
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    ZeroTrocaPorIfem(pGrade);
                    dtGrafico = new DataTable();
                    dtGrafico = MontaDtGrafico(dtGrafico, _dt);

                    foreach (DataColumn dc in dtGrafico.Columns)
                    {
                        if (dc.ColumnName != "Mes" && dc.ColumnName.IndexOf("Total") == -1)
                        {
                            Chart9.Series.Add(dc.ColumnName);
                            Chart9.Series[Chart9.Series.Count - 1].MarkerStep = 1;
                            Chart9.Series[Chart9.Series.Count - 1].XValueMember = "Mes";
                            Chart9.Series[Chart9.Series.Count - 1].YValueMembers = dc.ColumnName;
                            if (Chart9.Series.Count <= 1)
                                Chart9.Series[Chart9.Series.Count - 1].Color = Color.IndianRed;
                            Legend li = new Legend();
                            li.Name = dc.ColumnName;
                            Font f = new Font("Arial", 7);
                            if (Chart9.Series.Count <= 1)
                            {
                                Chart9.Legends.Add(li);
                                Chart9.Legends[Chart9.Legends.Count - 1].Font = f;
                            }
                        }
                    }
                    if (dtGrafico.Columns.Count > 1)
                    {
                        Chart9.Height = 400;
                        Chart9.DataSource = dtGrafico;
                        Chart9.DataBind();
                    }
                    else
                    {
                        Chart9.Height = 1;
                    }
                }
            }
            if (pGrade.ClientID == "GradeServicoSaude")
            {
                _dtTodos = new DataTable();
                _dt = oLancamentosMTRDados.PreencheDataTableRGR(Convert.ToInt32(txtCodigoCliente.Text), "20" + intAno.Valor, intMes.Valor, sInCodigosResiduos);
                decimal _tt = AddSomaMediaNaGrade(sInCodigosResiduos, _dt, (_dt.Rows.Count > 1));
                if (_tt == 0)
                {
                    foreach (DataColumn dc in _dt.Columns)
                    {
                        _dtTodos.Columns.Add(dc.ColumnName);
                    }
                    for (int i = 0; i <= 3; i++)
                    {
                        _dtTodos.NewRow();
                        _dtTodos.Rows.Add();
                        if (i == 0)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Carcaças de Animais - Grupo A2";
                        if (i == 1)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Infectocontagiante - Grupo A4 (sc)";
                        if (i == 2)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Perfuro Cortante - Grupo E (cx)";
                        if (i == 3)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Resto de Medicamento Vencido";
                        _dtTodos.Rows[i][2] = "-";
                        _dtTodos.Rows[i]["JanQuantidade"] = "-";
                        _dtTodos.Rows[i]["FevQuantidade"] = "-";
                        _dtTodos.Rows[i]["MarQuantidade"] = "-";
                        _dtTodos.Rows[i]["AbrQuantidade"] = "-";
                        _dtTodos.Rows[i]["MaiQuantidade"] = "-";
                        _dtTodos.Rows[i]["JunQuantidade"] = "-";
                        _dtTodos.Rows[i]["JulQuantidade"] = "-";
                        _dtTodos.Rows[i]["AgoQuantidade"] = "-";
                        _dtTodos.Rows[i]["SetQuantidade"] = "-";
                        _dtTodos.Rows[i]["OutQuantidade"] = "-";
                        _dtTodos.Rows[i]["NovQuantidade"] = "-";
                        _dtTodos.Rows[i]["DezQuantidade"] = "-";
                        _dtTodos.Rows[i][15] = "-";
                        _dtTodos.Rows[i][16] = "-";
                        _dtTodos.Rows[i][17] = "-";

                    }
                    _dt = _dtTodos;
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    pGrade.Columns[0].ItemStyle.Width = Unit.Parse("550px");
                    for (int ii = 1; ii <= 15; ii++)
                        pGrade.Columns[ii].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                else
                {
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    ZeroTrocaPorIfem(pGrade);

                    dtGrafico = new DataTable();
                    dtGrafico = MontaDtGrafico(dtGrafico, _dt);

                    foreach (DataColumn dc in dtGrafico.Columns)
                    {
                        if (dc.ColumnName != "Mes" && dc.ColumnName.IndexOf("Total") == -1)
                        {
                            Chart10.Series.Add(dc.ColumnName);
                            Chart10.Series[Chart10.Series.Count - 1].MarkerStep = 1;
                            Chart10.Series[Chart10.Series.Count - 1].XValueMember = "Mes";
                            Chart10.Series[Chart10.Series.Count - 1].YValueMembers = dc.ColumnName;
                            if (Chart10.Series.Count <= 1)
                                Chart10.Series[Chart10.Series.Count - 1].Color = Color.IndianRed;
                            Legend li = new Legend();
                            li.Name = dc.ColumnName;
                            Font f = new Font("Arial", 7);
                            if (Chart10.Series.Count <= 1)
                            {
                                Chart10.Legends.Add(li);
                                Chart10.Legends[Chart10.Legends.Count - 1].Font = f;
                            }
                        }
                    }
                    if (dtGrafico.Columns.Count > 1)
                    {
                        Chart10.Height = 400;
                        Chart10.DataSource = dtGrafico;
                        Chart10.DataBind();
                    }
                    else
                    {
                        Chart10.Height = 1;
                    }
                }
            }

            if (pGrade.ClientID == "GradeTintasAfins")
            {
                _dtTodos = new DataTable();
                _dt = oLancamentosMTRDados.PreencheDataTableRGR(Convert.ToInt32(txtCodigoCliente.Text), "20" + intAno.Valor, intMes.Valor, sInCodigosResiduos);
                decimal _tt = AddSomaMediaNaGrade(sInCodigosResiduos, _dt, (_dt.Rows.Count > 1));
                if (_tt == 0)
                {
                    foreach (DataColumn dc in _dt.Columns)
                    {
                        _dtTodos.Columns.Add(dc.ColumnName);
                    }
                    for (int i = 0; i <= 3; i++)
                    {
                        _dtTodos.NewRow();
                        _dtTodos.Rows.Add();
                        if (i == 0)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Borra de Cola";
                        if (i == 1)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Borra de Tinta";
                        if (i == 2)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Rezinas e Vernizes";
                        if (i == 3)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Solventes";
                        _dtTodos.Rows[i][2] = "-";
                        _dtTodos.Rows[i]["JanQuantidade"] = "-";
                        _dtTodos.Rows[i]["FevQuantidade"] = "-";
                        _dtTodos.Rows[i]["MarQuantidade"] = "-";
                        _dtTodos.Rows[i]["AbrQuantidade"] = "-";
                        _dtTodos.Rows[i]["MaiQuantidade"] = "-";
                        _dtTodos.Rows[i]["JunQuantidade"] = "-";
                        _dtTodos.Rows[i]["JulQuantidade"] = "-";
                        _dtTodos.Rows[i]["AgoQuantidade"] = "-";
                        _dtTodos.Rows[i]["SetQuantidade"] = "-";
                        _dtTodos.Rows[i]["OutQuantidade"] = "-";
                        _dtTodos.Rows[i]["NovQuantidade"] = "-";
                        _dtTodos.Rows[i]["DezQuantidade"] = "-";
                        _dtTodos.Rows[i][15] = "-";
                        _dtTodos.Rows[i][16] = "-";
                        _dtTodos.Rows[i][17] = "-";

                    }
                    _dt = _dtTodos;
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    pGrade.Columns[0].ItemStyle.Width = Unit.Parse("550px");
                    for (int ii = 1; ii <= 15; ii++)
                        pGrade.Columns[ii].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                else
                {
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    ZeroTrocaPorIfem(pGrade);

                    dtGrafico = new DataTable();
                    dtGrafico = MontaDtGrafico(dtGrafico, _dt);

                    foreach (DataColumn dc in dtGrafico.Columns)
                    {
                        if (dc.ColumnName != "Mes" && dc.ColumnName.IndexOf("Total") == -1)
                        {
                            Chart11.Series.Add(dc.ColumnName);
                            Chart11.Series[Chart11.Series.Count - 1].MarkerStep = 1;
                            Chart11.Series[Chart11.Series.Count - 1].XValueMember = "Mes";
                            Chart11.Series[Chart11.Series.Count - 1].YValueMembers = dc.ColumnName;
                            if (Chart11.Series.Count <= 1)
                                Chart11.Series[Chart11.Series.Count - 1].Color = Color.IndianRed;
                            Legend li = new Legend();
                            li.Name = dc.ColumnName;
                            Font f = new Font("Arial", 7);
                            if (Chart11.Series.Count <= 1)
                            {
                                Chart11.Legends.Add(li);
                                Chart11.Legends[Chart11.Legends.Count - 1].Font = f;
                            }
                        }
                    }
                    if (dtGrafico.Columns.Count > 1 && _dtTodos.Rows.Count == 0)
                    {
                        Chart11.Height = 400;
                        Chart11.DataSource = dtGrafico;
                        Chart11.DataBind();
                    }
                    else
                    {
                        Chart11.Height = 1;
                    }
                }
            }
            if (pGrade.ClientID == "GradeRestoProdutoQuimico")
            {
                _dtTodos = new DataTable();
                _dt = oLancamentosMTRDados.PreencheDataTableRGR(Convert.ToInt32(txtCodigoCliente.Text), "20" + intAno.Valor, intMes.Valor, sInCodigosResiduos);
                decimal _tt = AddSomaMediaNaGrade(sInCodigosResiduos, _dt, (_dt.Rows.Count > 1));
                if (_tt == 0)
                {
                    foreach (DataColumn dc in _dt.Columns)
                    {
                        _dtTodos.Columns.Add(dc.ColumnName);
                    }
                    for (int i = 0; i <= 1; i++)
                    {
                        _dtTodos.NewRow();
                        _dtTodos.Rows.Add();
                        if (i == 0)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Aerosol";
                        if (i == 1)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Produto Higiene e Beleza";
                        _dtTodos.Rows[i][2] = "-";
                        _dtTodos.Rows[i]["JanQuantidade"] = "-";
                        _dtTodos.Rows[i]["FevQuantidade"] = "-";
                        _dtTodos.Rows[i]["MarQuantidade"] = "-";
                        _dtTodos.Rows[i]["AbrQuantidade"] = "-";
                        _dtTodos.Rows[i]["MaiQuantidade"] = "-";
                        _dtTodos.Rows[i]["JunQuantidade"] = "-";
                        _dtTodos.Rows[i]["JulQuantidade"] = "-";
                        _dtTodos.Rows[i]["AgoQuantidade"] = "-";
                        _dtTodos.Rows[i]["SetQuantidade"] = "-";
                        _dtTodos.Rows[i]["OutQuantidade"] = "-";
                        _dtTodos.Rows[i]["NovQuantidade"] = "-";
                        _dtTodos.Rows[i]["DezQuantidade"] = "-";
                        _dtTodos.Rows[i][15] = "-";
                        _dtTodos.Rows[i][16] = "-";
                        _dtTodos.Rows[i][17] = "-";
                    }
                    _dt = _dtTodos;
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    pGrade.Columns[0].ItemStyle.Width = Unit.Parse("550px");
                    for (int ii = 1; ii <= 15; ii++)
                        pGrade.Columns[ii].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                else
                {
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    ZeroTrocaPorIfem(pGrade);

                    dtGrafico = new DataTable();
                    dtGrafico = MontaDtGrafico(dtGrafico, _dt);

                    foreach (DataColumn dc in dtGrafico.Columns)
                    {
                        if (dc.ColumnName != "Mes" && dc.ColumnName.IndexOf("Total") == -1)
                        {
                            Chart12.Series.Add(dc.ColumnName);
                            Chart12.Series[Chart12.Series.Count - 1].MarkerStep = 1;
                            Chart12.Series[Chart12.Series.Count - 1].XValueMember = "Mes";
                            Chart12.Series[Chart12.Series.Count - 1].YValueMembers = dc.ColumnName;
                            if (Chart12.Series.Count <= 1)
                                Chart12.Series[Chart12.Series.Count - 1].Color = Color.IndianRed;
                            Legend li = new Legend();
                            li.Name = dc.ColumnName;
                            Font f = new Font("Arial", 7);
                            if (Chart12.Series.Count <= 1)
                            {
                                Chart12.Legends.Add(li);
                                Chart12.Legends[Chart12.Legends.Count - 1].Font = f;
                            }
                        }
                    }
                    if (dtGrafico.Columns.Count > 1 && _dtTodos.Rows.Count == 0)
                    {
                        Chart12.Height = 400;
                        Chart12.DataSource = dtGrafico;
                        Chart12.DataBind();
                    }
                    else
                    {
                        Chart12.Height = 1;
                    }
                }
            }
            if (pGrade.ClientID == "GradeOleoLubrificanteUsado")
            {
                _dtTodos = new DataTable();
                _dt = oLancamentosMTRDados.PreencheDataTableRGR(Convert.ToInt32(txtCodigoCliente.Text), "20" + intAno.Valor, intMes.Valor, sInCodigosResiduos);
                decimal _tt = AddSomaMediaNaGrade(sInCodigosResiduos, _dt, (_dt.Rows.Count > 1));
                if (_tt == 0)
                {
                    foreach (DataColumn dc in _dt.Columns)
                    {
                        _dtTodos.Columns.Add(dc.ColumnName);
                    }
                    for (int i = 0; i <= 1; i++)
                    {
                        _dtTodos.NewRow();
                        _dtTodos.Rows.Add();
                        if (i == 0)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Óleo Lubrificante Usado (l)";
                        if (i == 1)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Borra de Óleo (Kg)";
                        _dtTodos.Rows[i][2] = "-";
                        _dtTodos.Rows[i]["JanQuantidade"] = "-";
                        _dtTodos.Rows[i]["FevQuantidade"] = "-";
                        _dtTodos.Rows[i]["MarQuantidade"] = "-";
                        _dtTodos.Rows[i]["AbrQuantidade"] = "-";
                        _dtTodos.Rows[i]["MaiQuantidade"] = "-";
                        _dtTodos.Rows[i]["JunQuantidade"] = "-";
                        _dtTodos.Rows[i]["JulQuantidade"] = "-";
                        _dtTodos.Rows[i]["AgoQuantidade"] = "-";
                        _dtTodos.Rows[i]["SetQuantidade"] = "-";
                        _dtTodos.Rows[i]["OutQuantidade"] = "-";
                        _dtTodos.Rows[i]["NovQuantidade"] = "-";
                        _dtTodos.Rows[i]["DezQuantidade"] = "-";
                        _dtTodos.Rows[i][15] = "-";
                        _dtTodos.Rows[i][16] = "-";
                        _dtTodos.Rows[i][17] = "-";
                    }
                    _dt = _dtTodos;
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    pGrade.Columns[0].ItemStyle.Width = Unit.Parse("550px");
                    for (int ii = 1; ii <= 15; ii++)
                        pGrade.Columns[ii].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                else
                {
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    ZeroTrocaPorIfem(pGrade);

                    dtGrafico = new DataTable();
                    dtGrafico = MontaDtGrafico(dtGrafico, _dt);

                    foreach (DataColumn dc in dtGrafico.Columns)
                    {
                        if (dc.ColumnName != "Mes" && dc.ColumnName.IndexOf("Total") == -1)
                        {
                            Chart13.Series.Add(dc.ColumnName);
                            Chart13.Series[Chart13.Series.Count - 1].MarkerStep = 1;
                            Chart13.Series[Chart13.Series.Count - 1].XValueMember = "Mes";
                            Chart13.Series[Chart13.Series.Count - 1].YValueMembers = dc.ColumnName;
                            if (Chart13.Series.Count <= 1)
                                Chart13.Series[Chart13.Series.Count - 1].Color = Color.IndianRed;
                            Legend li = new Legend();
                            li.Name = dc.ColumnName;
                            Font f = new Font("Arial", 7);
                            if (Chart13.Series.Count <= 1)
                            {
                                Chart13.Legends.Add(li);
                                Chart13.Legends[Chart13.Legends.Count - 1].Font = f;
                            }
                        }
                    }
                    if (dtGrafico.Columns.Count > 1 && _dtTodos.Rows.Count == 0)
                    {
                        Chart13.Height = 400;
                        Chart13.DataSource = dtGrafico;
                        Chart13.DataBind();
                    }
                    else
                    {
                        Chart13.Height = 1;
                    }
                }
            }
            if (pGrade.ClientID == "GradeEfluentes")
            {
                _dtTodos = new DataTable();
                _dt = oLancamentosMTRDados.PreencheDataTableRGR(Convert.ToInt32(txtCodigoCliente.Text), "20" + intAno.Valor, intMes.Valor, sInCodigosResiduos);
                decimal _tt = AddSomaMediaNaGrade(sInCodigosResiduos, _dt, (_dt.Rows.Count > 1));
                if (_tt == 0)
                {
                    foreach (DataColumn dc in _dt.Columns)
                    {
                        _dtTodos.Columns.Add(dc.ColumnName);
                    }
                    for (int i = 0; i <= 4; i++)
                    {
                        _dtTodos.NewRow();
                        _dtTodos.Rows.Add();
                        if (i == 0)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Efluente de Fossa Séptica";
                        if (i == 1)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Efluente de Cx de Gordura";
                        if (i == 2)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Efluente de Cx Separadora Água/Óleo";
                        if (i == 3)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Água Contaminada com Óleo";
                        if (i == 4)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Água Contaminada com Tinta";
                        _dtTodos.Rows[i][2] = "-";
                        _dtTodos.Rows[i]["JanQuantidade"] = "-";
                        _dtTodos.Rows[i]["FevQuantidade"] = "-";
                        _dtTodos.Rows[i]["MarQuantidade"] = "-";
                        _dtTodos.Rows[i]["AbrQuantidade"] = "-";
                        _dtTodos.Rows[i]["MaiQuantidade"] = "-";
                        _dtTodos.Rows[i]["JunQuantidade"] = "-";
                        _dtTodos.Rows[i]["JulQuantidade"] = "-";
                        _dtTodos.Rows[i]["AgoQuantidade"] = "-";
                        _dtTodos.Rows[i]["SetQuantidade"] = "-";
                        _dtTodos.Rows[i]["OutQuantidade"] = "-";
                        _dtTodos.Rows[i]["NovQuantidade"] = "-";
                        _dtTodos.Rows[i]["DezQuantidade"] = "-";
                        _dtTodos.Rows[i][15] = "-";
                        _dtTodos.Rows[i][16] = "-";
                        _dtTodos.Rows[i][17] = "-";
                    }
                    _dt = _dtTodos;
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    pGrade.Columns[0].ItemStyle.Width = Unit.Parse("550px");
                    for (int ii = 1; ii <= 15; ii++)
                        pGrade.Columns[ii].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                else
                {
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    ZeroTrocaPorIfem(pGrade);

                    dtGrafico = new DataTable();
                    dtGrafico = MontaDtGrafico(dtGrafico, _dt);

                    foreach (DataColumn dc in dtGrafico.Columns)
                    {
                        if (dc.ColumnName != "Mes" && dc.ColumnName.IndexOf("Total") == -1)
                        {
                            Chart14.Series.Add(dc.ColumnName);
                            Chart14.Series[Chart14.Series.Count - 1].MarkerStep = 1;
                            Chart14.Series[Chart14.Series.Count - 1].XValueMember = "Mes";
                            Chart14.Series[Chart14.Series.Count - 1].YValueMembers = dc.ColumnName;
                            if (Chart14.Series.Count <= 1)
                                Chart14.Series[Chart14.Series.Count - 1].Color = Color.IndianRed;
                            Legend li = new Legend();
                            li.Name = dc.ColumnName;
                            Font f = new Font("Arial", 7);
                            if (Chart14.Series.Count <= 1)
                            {
                                Chart14.Legends.Add(li);
                                Chart14.Legends[Chart14.Legends.Count - 1].Font = f;
                            }
                        }
                    }
                    if (dtGrafico.Columns.Count > 1 && _dtTodos.Rows.Count == 0)
                    {
                        Chart14.Height = 400;
                        Chart14.DataSource = dtGrafico;
                        Chart14.DataBind();
                    }
                    else
                    {
                        Chart14.Height = 1;
                    }
                }
            }
            if (pGrade.ClientID == "GradeLodos")
            {
                _dtTodos = new DataTable();
                _dt = oLancamentosMTRDados.PreencheDataTableRGR(Convert.ToInt32(txtCodigoCliente.Text), "20" + intAno.Valor, intMes.Valor, sInCodigosResiduos);
                decimal _tt = AddSomaMediaNaGrade(sInCodigosResiduos, _dt, (_dt.Rows.Count > 1));
                if (_tt == 0)
                {
                    foreach (DataColumn dc in _dt.Columns)
                    {
                        _dtTodos.Columns.Add(dc.ColumnName);
                    }
                    for (int i = 0; i <= 1; i++)
                    {
                        _dtTodos.NewRow();
                        _dtTodos.Rows.Add();
                        if (i == 0)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Lodo ETE  Classe I";
                        if (i == 1)
                            _dtTodos.Rows[i]["DescricaoReduzida"] = "Lodo ETE  Classe II";
                        _dtTodos.Rows[i][2] = "-";
                        _dtTodos.Rows[i]["JanQuantidade"] = "-";
                        _dtTodos.Rows[i]["FevQuantidade"] = "-";
                        _dtTodos.Rows[i]["MarQuantidade"] = "-";
                        _dtTodos.Rows[i]["AbrQuantidade"] = "-";
                        _dtTodos.Rows[i]["MaiQuantidade"] = "-";
                        _dtTodos.Rows[i]["JunQuantidade"] = "-";
                        _dtTodos.Rows[i]["JulQuantidade"] = "-";
                        _dtTodos.Rows[i]["AgoQuantidade"] = "-";
                        _dtTodos.Rows[i]["SetQuantidade"] = "-";
                        _dtTodos.Rows[i]["OutQuantidade"] = "-";
                        _dtTodos.Rows[i]["NovQuantidade"] = "-";
                        _dtTodos.Rows[i]["DezQuantidade"] = "-";
                        _dtTodos.Rows[i][15] = "-";
                        _dtTodos.Rows[i][16] = "-";
                        _dtTodos.Rows[i][17] = "-";
                    }
                    _dt = _dtTodos;
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    pGrade.Columns[0].ItemStyle.Width = Unit.Parse("550px");
                    for (int ii = 1; ii <= 15; ii++)
                        pGrade.Columns[ii].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }
                else
                {
                    pGrade.DataSource = _dt;
                    pGrade.DataBind();
                    ZeroTrocaPorIfem(pGrade);

                    dtGrafico = new DataTable();
                    dtGrafico = MontaDtGrafico(dtGrafico, _dt);

                    foreach (DataColumn dc in dtGrafico.Columns)
                    {
                        if (dc.ColumnName != "Mes" && dc.ColumnName.IndexOf("Total") == -1)
                        {
                            Chart15.Series.Add(dc.ColumnName);
                            Chart15.Series[Chart15.Series.Count - 1].MarkerStep = 1;
                            Chart15.Series[Chart15.Series.Count - 1].XValueMember = "Mes";
                            Chart15.Series[Chart15.Series.Count - 1].YValueMembers = dc.ColumnName;
                            if (Chart15.Series.Count <= 1)
                                Chart15.Series[Chart15.Series.Count - 1].Color = Color.IndianRed;
                            Legend li = new Legend();
                            li.Name = dc.ColumnName;
                            Font f = new Font("Arial", 7);
                            if (Chart15.Series.Count <= 1)
                            {
                                Chart15.Legends.Add(li);
                                Chart15.Legends[Chart15.Legends.Count - 1].Font = f;
                            }
                        }
                    }
                    if (dtGrafico.Columns.Count > 1 && _dtTodos.Rows.Count == 0)
                    {
                        Chart15.Height = 400;
                        Chart15.DataSource = dtGrafico;
                        Chart15.DataBind();
                    }
                    else
                    {
                        Chart15.Height = 1;
                    }
                }
            }
        }
        private void MontaRGR()
        {
            string sInCodigosResiduos = "";
            DataTable _dtResiduos = new DataTable();
            GradeMTR.Visible = true;
            lblCidadeEmpresa.Visible = true;
            lblDataEmissao.Visible = true;
            lblDadosLAO.Font.Size = 9;
            if (txtCodigoCliente.Text != "" && txtCodigoCliente.Text != "0")
            {
                MostraDadosCliente(Convert.ToInt32(txtCodigoCliente.Text));
                string mesExtenso = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(Convert.ToInt16(DateTime.Now.ToString("MM"))).ToLower();
                lblDataEmissao.Text = DateTime.Now.ToString("dd") + " de " + mesExtenso[0].ToString().ToUpper() + mesExtenso.Substring(1) + " de " + DateTime.Now.ToString("yyyy") + ".";

                clsLicencaAmbientalDados oLAO = new clsLicencaAmbientalDados();
                _dtDestinadorFinal = oLAO.PreencheDataTableLicencaAmbiental("Nome");
                string rtBROOKS = Request.QueryString["BROOKS"];

                // Preenche a tabela de conversão de und / m3 / cx / sc            
                _dtConversao = oResiduoDados.PegaDtComDadosConversao();
                foreach (DataRow dr in _dtConversao.Rows)
                {
                    dr["Unidade"] = "kg/" + dr["Unidade"];
                }
                //GradeConversao.DataSource = _dtConversao;
                //GradeConversao.DataBind();

                /*
                _dtResiduos = oResiduoDados.PreencheDTCodigosResiduos(true);
                sInCodigosResiduos = "";
                foreach (DataRow drResiduos in _dtResiduos.Rows)
                {
                    sInCodigosResiduos = sInCodigosResiduos + ", " + drResiduos[0].ToString();
                }
                sInCodigosResiduos = sInCodigosResiduos.Substring(1);
                */
                sInCodigosResiduos = "";
                sInCodigosResiduos = sInCodigosResiduos + "  92";  // plastico misto 
                sInCodigosResiduos = sInCodigosResiduos + ", 62";  // plastico transparente
                sInCodigosResiduos = sInCodigosResiduos + ", 144"; // plastico pet
                sInCodigosResiduos = sInCodigosResiduos + ", 61";  // plastico
                sInCodigosResiduos = sInCodigosResiduos + ", 34";  // papelão
                sInCodigosResiduos = sInCodigosResiduos + ", 94";  // Alumínio
                sInCodigosResiduos = sInCodigosResiduos + ", 21";  // Sucata de Ferro
                sInCodigosResiduos = sInCodigosResiduos + ", 228"; // Tetra pack 
                sInCodigosResiduos = sInCodigosResiduos + ", 9";   // Vidro
                sInCodigosResiduos = sInCodigosResiduos + ", 117"; // Vidro Conserva Pq
                sInCodigosResiduos = sInCodigosResiduos + ", 116"; // Vidro Conserva Gr
                sInCodigosResiduos = sInCodigosResiduos + ", 234"; // Óleo Vegetal

                sInCodigosResiduos = sInCodigosResiduos + ", 143"; // Plástico Mole
                sInCodigosResiduos = sInCodigosResiduos + ", 147"; // Plástico Duro
                sInCodigosResiduos = sInCodigosResiduos + ", 239"; // Papel Toalha
                sInCodigosResiduos = sInCodigosResiduos + ", 106"; // Papel Branco
                sInCodigosResiduos = sInCodigosResiduos + ", 237"; // Isopor
                sInCodigosResiduos = sInCodigosResiduos + ", 127"; // Garrafoes de Vidro

                MontaGradeGrafico(GradeMTR, sInCodigosResiduos);

                MontaGradeGrafico(GradeCompostagem, "240");
                MontaGradeGrafico(GradeResiduoComum, "85");
                MontaGradeGrafico(GradeRCD, "160");
                MontaGradeGrafico(GradeSolidosContaminados, "3, 98, 80, 139, 76");
                MontaGradeGrafico(GradeEletroeletronicosReciclaveis, "67, 60, 65, 38, 68, 170"); // eletroeletronicos reciclaveis
                MontaGradeGrafico(GradeNaoReciclaveis, "171");                                   // eletroeletronicos não reciclaveis
                MontaGradeGrafico(GradeLampadasFluorescentes, "4, 101, 102, 164, 105");
                MontaGradeGrafico(GradePilhasBaterias, "5, 165");
                MontaGradeGrafico(GradeServicoSaude, "25, 137, 141, 32, 122");
                MontaGradeGrafico(GradeTintasAfins, "19, 83, 88, 189, 190");
                MontaGradeGrafico(GradeRestoProdutoQuimico, "17");
                MontaGradeGrafico(GradeOleoLubrificanteUsado, "177");
                MontaGradeGrafico(GradeEfluentes, "191, 209, 245, 233, 28, 91");
                MontaGradeGrafico(GradeLodos, "24, 77");
                MontaGradeGrafico(GradeReciclagemDesvioAterro, "");

            }
        }
    }
}