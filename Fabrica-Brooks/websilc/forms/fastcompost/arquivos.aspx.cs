using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class arquivos : System.Web.UI.Page
{
    clsClientes oCliente = new clsClientes();
    clsClienteDados oClienteDados = new clsClienteDados();
    clsUsuarios oUsuario = new clsUsuarios();
    wp_FilesClientesDados owpFilesClientesDados = new wp_FilesClientesDados();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                lblTitulo.Text = "Hoje: " + DateTime.Now.ToShortDateString();
                if (Session["oUsuario"].ToString() != "")
                    oUsuario.Nome = ((clsUsuarios)Session["oUsuario"]).Nome;
                lblCliente.Text = "&nbsp;" + oUsuario.Nome;
                if (oUsuario.Nome.Length >= 9)
                {
                    oUsuario.Nome = oUsuario.Nome.Substring(3, 6);
                    oCliente = oClienteDados.PegaDados(oCliente, Convert.ToInt32(oUsuario.Nome));
                    lblCliente.Text = oCliente.NomeFantasia;
                    clsBloqFinanceiroDados oBloqueioFinanceiroDados = new clsBloqFinanceiroDados();
                    lblBloqueio.Visible = false;
                    bool bBloqFinanceiro;
                    bBloqFinanceiro = oBloqueioFinanceiroDados.ExisteBloqueio(oCliente.Codigo);
                    if (oCliente.Inativo == 1 || oCliente.TiposDeContrato != 0 || bBloqFinanceiro)
                    {
                        if (bBloqFinanceiro)
                        {
                            lblBloqueio.Visible = true;
                            lblBloqueio.Text = "Documentos indisponíveis.<br /> Favor entrar em Contato com: <br />financeiro@brooksambiental.com.br ou telefone: (48)3344-1515 ";
                            lblBloqueio.Font.Size = 14;
                        }
                        rdbRGR.Enabled = false;
                        rdbDDR.Enabled = false;
                        rdbCERTISO9001.Enabled = false;
                        rdbLAO.Enabled = false;
                        rdbAlvara.Enabled = false;
                        rdbCTFIBAMA.Enabled = false;
                        Grade.Enabled = false;
                    }
                    else
                    {
                        Grade.DataSource = owpFilesClientesDados.PegaDados(geral.RetiraLetras(oUsuario.Nome), geral.DOCUMENTO.ALVARA, geral.Left(oUsuario.Nome, 3));
                        Grade.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                //lblMensagem.Text = ex.Message;
            }
        }
    }
    protected void btnProcurar_Click(object sender, EventArgs e)
    {
        lblTitulo.Text = "Hoje: " + DateTime.Now.ToShortDateString();
        if (Session["oUsuario"].ToString() != "")
        {
            oUsuario.Nome = ((clsUsuarios)Session["oUsuario"]).Nome;
            if (rdbRGR.Checked)
                Grade.DataSource = owpFilesClientesDados.PegaDados(oUsuario.Nome.Substring(3, 6), geral.DOCUMENTO.RGR, geral.Left(oUsuario.Nome, 3));
            if (rdbDDR.Checked)
            {
                Response.Redirect("../DDR.aspx");
                //Response.Write("<script>window.open('../DDR.aspx', '_blank');</script>");
            }
            //if (rdbDDR_Anterior.Checked)
            //    Grade.DataSource = owpFilesClientesDados.PegaDados(oUsuario.Nome.Substring(3, 6), geral.DOCUMENTO.DDR, geral.Left(oUsuario.Nome, 3));
            if (rdbCERTISO9001.Checked)
                Grade.DataSource = owpFilesClientesDados.PegaDados(oUsuario.Nome.Substring(3, 6), geral.DOCUMENTO.CERTISO, geral.Left(oUsuario.Nome, 3));
            if (rdbLAO.Checked)
                Grade.DataSource = owpFilesClientesDados.PegaDados(oUsuario.Nome.Substring(3, 6), geral.DOCUMENTO.LAO, geral.Left(oUsuario.Nome, 3));
            if (rdbAlvara.Checked)
                Grade.DataSource = owpFilesClientesDados.PegaDados(oUsuario.Nome.Substring(3, 6), geral.DOCUMENTO.ALVARA, geral.Left(oUsuario.Nome, 3));
            if (rdbCTFIBAMA.Checked)
                Grade.DataSource = owpFilesClientesDados.PegaDados(oUsuario.Nome.Substring(3, 6), geral.DOCUMENTO.CTFIBAMA, geral.Left(oUsuario.Nome, 3));
            Grade.DataBind();
            lblCliente.Text = "&nbsp;" + oClienteDados.PegaNomeFantasiaCodigo(Convert.ToInt32(oUsuario.Nome.Substring(3, 6))).Rows[0]["NomeFantasia"].ToString();
        }
    }
    protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            if (e.Row.Cells[1].Text != "" && e.Row.Cells[1].Text != "&nbsp;" && e.Row.RowIndex >= 2)
            {
                if (rdbAlvara.Checked || rdbCERTISO9001.Checked || rdbLAO.Checked || rdbCTFIBAMA.Checked)
                    e.Row.Cells[0].Text = "<a style='text-decoration:none;color:black;' target='_blank' href='../../dados/" + e.Row.Cells[0].Text + "'>" + e.Row.Cells[0].Text.Replace(".pdf", "") + "</a>";
                else
                    e.Row.Cells[0].Text = "<a style='text-decoration:none;color:black;' target='_blank' href='../../dados/" + geral.Left(e.Row.Cells[0].Text, 14).Substring(8, 6) + "/" + e.Row.Cells[0].Text + "'>" + e.Row.Cells[0].Text.Replace(".pdf", "") + "</a>";
            }
            else
            {
                if (rdbAlvara.Checked || rdbCERTISO9001.Checked || rdbLAO.Checked || rdbCTFIBAMA.Checked)
                    e.Row.Cells[0].Text = "<a style='text-decoration:none;color:black;' target='_blank' href='../../dados/" + e.Row.Cells[0].Text + "'>" + e.Row.Cells[0].Text.Replace(".pdf", "") + "</a>";
                else
                    e.Row.Cells[0].Text = "<a style='text-decoration:none;color:black;' target='_blank' href='../../dados/" + geral.Left(e.Row.Cells[0].Text, 14).Substring(8, 6) + "/" + e.Row.Cells[0].Text + "'>" + e.Row.Cells[0].Text.Replace(".pdf", "") + "</a>";
            }
        }
    }
}