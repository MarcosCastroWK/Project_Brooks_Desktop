using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

namespace SILC.Web.forms
{
    public partial class LiberarProgramacaoServicos : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsUsuarios oUsuario = new clsUsuarios();
        DataTable _dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
            clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
            if (oUsuario != null)
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "58");
            if (oItensMenuPermissoes.Consultar == 0 && oItensMenuPermissoes.Alterar == 0)
                Response.Redirect("sempermissao.aspx");

            if (!IsPostBack)
            {
                if (geral.Demonstracao)
                {
                    Salvar.Enabled = false;
                }
                oUsuario = (clsUsuarios)Session["oUsuario"];
                if (oUsuario == null)
                {
                    Response.Redirect("brooks/loginaplicativo.aspx", true);
                }
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
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Menu.aspx", true);
        }
        private void SalvarLog(string pOperacao)
        {
            clsLog oLog = new clsLog();
            clsLogDados oLogDados = new clsLogDados();
            oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
            oLog.CodigoUsuario = oUsuario.Codigo;
            oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
            oLog.LocalOperacao = "Liberação Acesso Programação";
            oLog.Operacao = pOperacao;
            oLog.Log = oLog.Log + "Usuário: " + oUsuario.Nome + " (" + oUsuario.Codigo + ") \n";
            oLog.Log = oLog.Log + "Hora: " + DateTime.Now.ToString("hh:mm:ss") + " \n";
            oLogDados.Inserir(oLog);
        }
        protected void Salvar_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = "";
            if (Salvar.Text == "Liberar")
            {
                clsProgramacaoFechadaDados oDados = new clsProgramacaoFechadaDados();
                lblMensagem.Text = oDados.LiberarProgramacaoBloqueada();
                if (lblMensagem.Text == "Liberou")
                {
                    lblMensagem.Text = "Programação liberada com sucesso!";
                    SalvarLog("Liberou");
                }
                else
                    SalvarLog("Já liberada");
            }
        }
    }
}