using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;


public partial class EscolherDB : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsUsuarios oUsuario = new clsUsuarios();
    DataTable _dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        oUsuario = (clsUsuarios)Session["oUsuario"];
        clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
        if (oUsuario != null)
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "62");
        if (oItensMenuPermissoes.Consultar == 0 && oItensMenuPermissoes.Alterar == 0)
            Response.Redirect("sempermissao.aspx");

        if (!IsPostBack)
        {
            if (oUsuario == null)
            {
                Response.Redirect("brooks/loginaplicativo.aspx", true);
            }
            if (oUsuario.Aplicativo == true)
                menu.Visible = false;
            else
                menu.Visible = true;
            ddlDB.SelectedIndex = geral.BancoUsado - 1;
        }
    }
    private void SalvarLog(string pOperacao)
    {
        clsLog oLog = new clsLog();
        clsLogDados oLogDados = new clsLogDados();
        oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
        oLog.CodigoUsuario = oUsuario.Codigo;
        oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
        oLog.LocalOperacao = "Escolheu outro DB";
        oLog.Operacao = pOperacao;
        oLog.Log = oLog.Log + "Usuário DB: " + oUsuario.Nome + " (" + oUsuario.Codigo + ") \n";
        oLog.Log = oLog.Log + "Mudou para DB: " + ddlDB.Text + " \n";
        oLog.Log = oLog.Log + "Hora: " + DateTime.Now.ToString("hh:mm:ss") + " \n";
        oLogDados.Inserir(oLog);
    }

    protected void ddlDB_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDB.SelectedIndex >= 0)
        {
            SalvarLog("Escolheu DB");
            geral.BancoUsado = ddlDB.SelectedIndex + 1;
            this.ClientScript.RegisterStartupScript(GetType(), "refresh", "<script>window.location='EscolherDB.aspx'</script>");
        }
    }
}