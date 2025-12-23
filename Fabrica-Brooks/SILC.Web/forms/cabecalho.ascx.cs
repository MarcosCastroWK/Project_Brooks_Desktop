using System;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;

namespace SILC.Web.forms
{
    public partial class cabecalho : System.Web.UI.UserControl
    {
        public string Label
        {
            set { lblUsuario.Text = value; }
            get { return lblUsuario.Text; }
        }
        clsUsuarios oUsuario = new clsUsuarios();
        clsUsuarioDados oUsuarioDados = new clsUsuarioDados();
        protected void Page_Load(object sender, EventArgs e)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
            if (geral.BancoUsado == 4)
            {
                geral.CodigoEmpresa = 6;
                oUsuario.CodigoEmpresa = 6;
            }
            System.Web.UI.HtmlControls.HtmlImage _img = (System.Web.UI.HtmlControls.HtmlImage)FindControl("imglogo");
            if (oUsuario.CodigoEmpresa == 2) // é EWVS
                _img.Src = "../Images/ewvs.png";
            if (oUsuario.CodigoEmpresa == 3) // é FIRST Rent a Car - São Paulo/SP
                _img.Src = "../Images/first.jpg";
            if (oUsuario.CodigoEmpresa == 4) // é VIAMAR Rent a Car - Florianópolis/SC
                _img.Src = "../Images/viamar.jpg";
            if (oUsuario.CodigoEmpresa == 6) // FastCompost - Palhoça - Galpão 02
                _img.Src = "../forms/fastcompost/logo.jpg";

            if (!IsPostBack)
            {
                if (oUsuario.CodigoEmpresa == 1 || geral.CodigoEmpresa == 1) // é BROOKS
                    MostraUsuarioLogado();
                else
                    MostraUsuarioLogado();
            }
        }
        private void MostraUsuarioLogado()
        {
            Label _lb1 = (Label)FindControl("lblUsuario");
            if (oUsuario.Nome != "" && oUsuario.Nome != "&nbsp;")
                _lb1.Text = "Usuário: " + oUsuario.Nome + "&nbsp;&nbsp;";
            else
                _lb1.Text = "Usuário: não logado";

            Label _lblDB = (Label)FindControl("lblDB");
            if (lblDB != null)
            {
                if (LibSILC.geral.BancoUsado == 4)
                    _lblDB.Text = "db: " + LibSILC.geral.BancoUsado.ToString() + "&nbsp;FastCompost";
                else
                    _lblDB.Text = "db: " + LibSILC.geral.BancoUsado.ToString() + "&nbsp;&nbsp;";
                _lblDB.Visible = true;
            }
        }
    }
}