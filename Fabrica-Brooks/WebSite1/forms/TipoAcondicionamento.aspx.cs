using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class forms_TipoAcondicionamento : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsTipoAcondicionamento oTipoAcondicionamento = new clsTipoAcondicionamento();
    clsTipoAcondicionamentoDados oTipoAcondicionamentoDados = new clsTipoAcondicionamentoDados();
    clsUsuarios oUsuario = new clsUsuarios();
    clsUsuarioDados oUsuarioDados = new clsUsuarioDados();
    DataTable _dt = new DataTable();    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["oUsuario"] == null)
        {
            menu _menu = (menu)FindControl("menu1");
            oUsuario.Codigo = Convert.ToInt16(((HiddenField)_menu.FindControl("hifCodigo")).Value);
            oUsuario.CodigoEmpresa = Convert.ToInt16(((HiddenField)_menu.FindControl("hifCodigoEmpresa")).Value);
            oUsuario.Nome = ((HiddenField)_menu.FindControl("hifNome")).Value;
            oUsuario.Aplicativo = false;
            Session["oUsuario"] = oUsuario;
        }
        else if (Session["oUsuario"] != null)
        {
            oUsuario = (clsUsuarios)Session["oUsuario"];
        }
        clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
        if (oUsuario != null)
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "66");

        if (!IsPostBack)
        {
            if (geral.Demonstracao)
            {
                Salvar.Enabled = false;
            }
            if (oUsuario == null)
            {
                Response.Redirect("brooks/loginaplicativo.aspx", true);
            }
            else
            {
                if (oItensMenuPermissoes.Consultar == 0 && oItensMenuPermissoes.Incluir == 0 && oItensMenuPermissoes.Alterar == 0 && oItensMenuPermissoes.Excluir == 0)
                    Response.Redirect("sempermissao.aspx");

                try
                {
                    if (oUsuario.Aplicativo == true)
                        menu1.Visible = false;
                    else
                        menu1.Visible = true;
                    Grade.DataSource = oTipoAcondicionamentoDados.PegaDados(oTipoAcondicionamento, 0, false);
                    Grade.DataBind();
                    txtDescricao.MaxLength = oTipoAcondicionamentoDados.PegaTamanhoCampoVarChar("Descricao");
                    PermissaoIncluir();
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = ex.Message;
                }
            }
        }
        txtDescricao.Focus();
    }
    private void PermissaoIncluir()
    {
        Salvar.Enabled = true;
        if (oItensMenuPermissoes.Incluir == 0)
            Salvar.Enabled = false;
    }

    private void PermissaoAlterar()
    {
        Salvar.Enabled = true;
        if (oItensMenuPermissoes.Alterar == 0)
            Salvar.Enabled = false;
    }
	private void SalvarLog(string pOperacao)
    {
        clsLog oLog = new clsLog();
        clsLogDados oLogDados = new clsLogDados();
        oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
        oLog.CodigoUsuario = oUsuario.Codigo;
        oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
        oLog.LocalOperacao = "Cadastro Tipo Acondicionamento";
        oLog.Operacao = pOperacao;
        oLog.Log = oLog.Log + "Código: " + hifCodigo.Value + " \n";
		oLog.Log = oLog.Log + "Descrição: " + txtDescricao.Text + " \n";
        oLogDados.Inserir(oLog);

    }
    protected void Excluir_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = oTipoAcondicionamentoDados.DadoExiste(oTipoAcondicionamento.Codigo);
        if (txtDescricao.Text.Equals(""))
        {
            lblMensagem.Text = "Tipo Acondicionamento inválido!";
        }
        if (lblMensagem.Text.Equals("Alterar"))
        {
            oTipoAcondicionamentoDados.Excluir(oTipoAcondicionamento.Codigo, "");
            lblMensagem.Text = "Tipo Acondicionamento excluído com sucesso!";

            Grade.DataSource = oTipoAcondicionamentoDados.PegaDados(oTipoAcondicionamento, 0, false);
            Grade.DataBind();
        }
    }
    protected void Salvar_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = "";        
        if (Salvar.Text == "Ok")
        {
            if (txtDescricao.Text.Equals(""))
            {
                lblMensagem.Text = "Tipo Acondicionamento inválido!";
            }
 
            // não ocorreu erro salvar
            if (lblMensagem.Text.Equals(""))
            {
                // salvar
                oTipoAcondicionamento = AtribuiDadosDoForm(oTipoAcondicionamento);
                if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                    hifCodigo.Value = "-1";
                lblMensagem.Text = oTipoAcondicionamentoDados.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                if (lblMensagem.Text == "Incluir")
                {
                    SalvarLog("Inclusão");
					oTipoAcondicionamentoDados.Inserir(oTipoAcondicionamento);

                    Grade.DataSource = oTipoAcondicionamentoDados.PreencheDataTable("Codigo desc", "");
                    Grade.DataBind();
                    lblMensagem.Text = "Tipo Acondicionamento incluído com sucesso!";
                    LimpaCampos();
                }
                else if (lblMensagem.Text == "Alterar")
                {
                    string msgErr = oTipoAcondicionamentoDados.Alterar(oTipoAcondicionamento, Convert.ToInt32(hifCodigo.Value));
                    if (msgErr.Length > 0)
                        lblMensagem.Text = msgErr;
                    else
                    {
						SalvarLog("Alteração");
                        lblMensagem.Text = "Tipo Acondicionamento alterado com sucesso!";
                        Grade.DataSource = oTipoAcondicionamentoDados.PegaDados(oTipoAcondicionamento, 0, false);
                        Grade.DataBind();
                    }
                }
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;Cadastro de Tipo Acondicionamento";                
            }
        }
        else if (Salvar.Text == "Confirma") // confirma a exclusão
        {
            lblMensagem.Text = "";
            if (txtDescricao.Text.Equals(""))
            {
                lblMensagem.Text = "Tipo Acondicionamento inválido!";
            }
            if (lblMensagem.Text.Equals(""))
            {
				SalvarLog("Exclusão");
                oTipoAcondicionamentoDados.Excluir(Convert.ToInt32(hifCodigo.Value), "");
                lblMensagem.Text = "Tipo Acondicionamento excluído com sucesso!";
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;Cadastro de Tipo Acondicionamento";
                Grade.DataSource = oTipoAcondicionamentoDados.PegaDados(oTipoAcondicionamento, 0, false);
                Grade.DataBind();
            }
        }
        LimpaCampos();
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

    protected void ibnExcluir_Click(object sender, ImageClickEventArgs e)
    {
        lblTitulo.Text = "&nbsp;Exclusão de Tipo Acondicionamento";
        Salvar.Text = "Confirma";
        lblMensagem.Text = "";
        Salvar.Enabled = true;
    }
    protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
    {        
        Salvar.Text = "Ok";
        lblTitulo.Text = "&nbsp;Alteração de Tipo Acondicionamento";
        lblMensagem.Text = "";
    }
    protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
    {        
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "Inativo" && e.CommandArgument.ToString() != "DataCadastro" &&
            e.CommandArgument.ToString() != "Descricao")
        {
            txtDescricao.Text = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
            hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
            lblCodigoMostrado.Text = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
            oTipoAcondicionamento = oTipoAcondicionamentoDados.PegaDados(oTipoAcondicionamento, Convert.ToInt32(hifCodigo.Value));
            AtribuiDadosDaClasse(oTipoAcondicionamento);
            if (Salvar.Text != "Confirma")
                PermissaoAlterar();
        }
    }

    protected void AtribuiDadosDaClasse(clsTipoAcondicionamento pTipoAcondicionamento)
    {
        txtDescricao.Text = pTipoAcondicionamento.Descricao;
    }

    protected void LimpaCampos()
    {
        hifCodigo.Value = "";
        txtDescricao.Text = "";
        lblCodigoMostrado.Text = "";
        PermissaoIncluir();
    }
    protected clsTipoAcondicionamento AtribuiDadosDoForm(clsTipoAcondicionamento pTipoAcondicionamento)
    {
        pTipoAcondicionamento.Descricao = txtDescricao.Text;
        return pTipoAcondicionamento;
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        lblTitulo.Text = "&nbsp;Cadastro de Tipo Acondicionamento";
        LimpaCampos();
        hifCodigo.Value = "";
        lblMensagem.Text = "";
        Salvar.Text = "Ok";
    }
    protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex >= 0)
        {
            ImageButton _ibnExcluir = new ImageButton();
            _ibnExcluir.Enabled = true;
            if (oItensMenuPermissoes.Excluir == 0)
            {
                _ibnExcluir = (ImageButton)e.Row.Cells[1].FindControl("ibnExcluir");
                _ibnExcluir.Enabled = false;
            }
        }
    }
    protected void datDataCadastro_TextChanged(object sender, EventArgs e)
    {
       //datDataCadastro.DataMode = TextBoxMode.Date;
    }
    protected void datDataCadastro_DataBinding(object sender, EventArgs e)
    {

    }
    protected void btnProcurar_Click(object sender, EventArgs e)
    {
        
    }
    protected void lblErro_TextChanged(object sender, EventArgs e)
    {

    }

    protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
            geral.Ordem = e.SortExpression + " desc";
        else
            geral.Ordem = e.SortExpression + " asc";

        Grade.DataSource = oTipoAcondicionamentoDados.PreencheDataTable(geral.Ordem, "");
        Grade.DataBind();
    }

    protected void imbEspande_Click(object sender, ImageClickEventArgs e)
    {
        if (hifEspande.Value == "200px")
            hifEspande.Value = "400px";
        else
            hifEspande.Value = "200px";
       
        this.Page.ClientScript.RegisterStartupScript(
            this.Page.GetType(),
            "MessageBox",
            "<script>ExpandeDiv('idX');</script>"
        );

    }
}