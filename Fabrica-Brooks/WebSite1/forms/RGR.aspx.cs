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

public partial class RGR : System.Web.UI.Page
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

    DataTable _dt = new DataTable();

    string rt = "";
    decimal SubTotalGerado = 0;
    decimal SubTotalAmazendado = 0;
    decimal SubTotalDestinado = 0;
    decimal SubTotalCDFe = 0;

    string _datainicial = "";
    string _datafinal = "";
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
    private void MontaRGR()
    {        
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
            DataTable _dtResiduos = new DataTable();
            _dtResiduos = oResiduoDados.PreencheDTCodigosResiduos(true);
            string sInCodigosResiduos = "";
            foreach (DataRow drResiduos in _dtResiduos.Rows)
            {
                sInCodigosResiduos = sInCodigosResiduos + ", " + drResiduos[0].ToString();
            }
            sInCodigosResiduos = sInCodigosResiduos.Substring(1);

            _dt = oLancamentosMTRDados.PreencheDataTableRGR(Convert.ToInt32(txtCodigoCliente.Text), "20" + intAno.Valor, intMes.Valor, sInCodigosResiduos);

            decimal qtTotalAno = 0;
            foreach (DataRow dr in _dt.Rows)
            {
                qtTotalAno = Convert.ToDecimal(dr[4]) + Convert.ToDecimal(dr[5]) + Convert.ToDecimal(dr[6])  + Convert.ToDecimal(dr[7])  + Convert.ToDecimal(dr[8])  + Convert.ToDecimal(dr[9]) + 
                             Convert.ToDecimal(dr[10]) + Convert.ToDecimal(dr[11]) + Convert.ToDecimal(dr[12]) + Convert.ToDecimal(dr[13]) + Convert.ToDecimal(dr[14]);
                dr["TotalQuantidadeAno"] = qtTotalAno;
             
                if (intMes.Valor != "")
                    dr["MediaAno"] = qtTotalAno / (Convert.ToInt16(intMes.Valor));
            }
            GradeMTR.DataSource = _dt;
            GradeMTR.DataBind();

            Chart1.DataMember = "DescricaoReduzida";
            Chart1.DataSource = _dt;
            Chart1.DataBind();
        }
    }
    protected void btnMontaDDR_Click(object sender, EventArgs e)
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
            if (btnMontaDDR.Text == "Ok")
                MontaRGR();
            if (intAno.Visible)
            {
                lblPeriodoDesejado.Text = Convert.ToDateTime(_datainicial).Month.ToString("00") +  "/" + 
                                          Convert.ToDateTime(_datainicial).Year.ToString();
                lblPeriodoDesejado.Visible = false;
                intAno.Visible = false;
                intMes.Visible = false;
                lblBarra.Visible = false;
                lblMesAnoReferente.Visible = true;
                lblPeriodoApuracao.Visible = true;
                imgLogoCliente.Visible = true;
                GradeMTR.Visible = true;
                lblPeriodoDesejado.ForeColor = Color.Black;
                btnMontaDDR.Text = "Novo Ano";
            }
            else
            {
                lblPeriodoDesejado.Text = "Selecione Mês/Ano desejado: ";
                lblPeriodoDesejado.Visible = true;
                intAno.Visible = true;
                intMes.Visible = true;
                lblBarra.Visible = true;
                lblMesAnoReferente.Visible = false;
                lblPeriodoApuracao.Visible = false;
                imgLogoCliente.Visible = false;
                GradeMTR.Visible = false;
                lblPeriodoDesejado.ForeColor = Color.IndianRed;
                btnMontaDDR.Text = "Ok";
            }
        }
    }

    protected void GradeMTR_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            if (e.Row.Cells[2].Text == "0,00")
                e.Row.Cells[2].Text = "-";

        }
    }
}