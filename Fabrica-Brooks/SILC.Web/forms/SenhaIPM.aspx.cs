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
    public partial class SenhaIPM : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsUsuarios oUsuario = new clsUsuarios();
        DataTable _dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
            clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
            if (oUsuario != null)
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "54");
            if (oItensMenuPermissoes.Consultar == 0 && oItensMenuPermissoes.Alterar == 0)
                Response.Redirect("sempermissao.aspx"); if (!IsPostBack)
            {
                if (geral.Demonstracao)
                {
                    Salvar.Enabled = false;
                }
                if (oUsuario == null)
                {
                    Response.Redirect("brooks/loginaplicativo.aspx", true);
                }
                if (oUsuario.Aplicativo == true)
                    menu.Visible = false;
                else
                    menu.Visible = true;
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
        protected void Salvar_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = "";
            clsSenhaIPMDados oDados = new clsSenhaIPMDados();
            if (txtSenhaNova.Text.Length == 0)
                lblMensagem.Text = "Senha nova inválida!";
            else if (txtSenhaNova.Text != txtConfirmarNovaSenha.Text)
                lblMensagem.Text = "Senha nova não foi confirmada!";
            else
            {
                string _ultimasenha = oDados.PegaUltimaSenha();
                if (txtSenhaAtual.Text == _ultimasenha || _ultimasenha.Length == 0)
                {
                    clsSenhaIPM oSenhaIPM = new clsSenhaIPM();
                    oSenhaIPM.Senha = txtSenhaNova.Text;
                    lblMensagem.Text = oDados.Inserir(oSenhaIPM);
                }
                else if (txtSenhaAtual.Text != _ultimasenha)
                    lblMensagem.Text = "Senha atual, diferente da senha usada atualmente!";
            }
        }
    }
}