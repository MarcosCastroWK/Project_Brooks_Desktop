using System;
using System.Data;
using System.Net.Sockets;
using System.IO;
using LibSILC;
using SILCNegocios;

public partial class Balanca : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsUsuarios oUsuario = new clsUsuarios();
    DataTable _dt = new DataTable();

    private NetworkStream sockStream;
    private BinaryWriter escreve;
    private BinaryReader le;
    private string message = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        oUsuario = (clsUsuarios)Session["oUsuario"];
        clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
        if (oUsuario != null)
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "63");
        if (oItensMenuPermissoes.Consultar == 0 && oItensMenuPermissoes.Alterar == 0)
            Response.Redirect("sempermissao.aspx"); 
        if (!IsPostBack)
        {
        }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Menu.aspx", true);
    }

    protected void LerDadosBalanca_Click(object sender, EventArgs e)
    {
        TcpClient cliente = new TcpClient();
        txtPesoTotal.Text = "";

        try
        {

            cliente.Connect("192.168.1.150", 23);

            //Se preferir altere localhost pelo IP do server

            sockStream = cliente.GetStream();

            le = new BinaryReader(sockStream);

            try
            {

                message = le.ReadString();

                txtPesoTotal.Text += System.Environment.NewLine;

                txtPesoTotal.Text += message; //message.Replace("T,GS,+", "Peso total: ");

            }
            catch (Exception)
            {

                System.Environment.Exit(System.Environment.ExitCode);

            }

            //escreve.Close();

            le.Close();

            sockStream.Close();

            cliente.Close();

        }

        catch (Exception error)

        {

            lblMensagem.Text = error.Message;

        }
    }
}