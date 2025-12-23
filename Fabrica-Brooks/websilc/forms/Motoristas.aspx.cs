using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

public partial class webMotoristas : System.Web.UI.Page
{
    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
    clsFuncionarios oMotoristas = new clsFuncionarios();
    clsFuncionarioDados oMotoristasDados = new clsFuncionarioDados();
    clsUsuarios oUsuario = new clsUsuarios();
    DataTable _dt = new DataTable();    
    protected void Page_Load(object sender, EventArgs e)
    {
        oUsuario = (clsUsuarios)Session["oUsuario"];
        clsItensMenuPermissoesDados oPermissaoDados = new clsItensMenuPermissoesDados();
        if (oUsuario != null)
            oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "11");
        if (oItensMenuPermissoes.Consultar == 0 && oItensMenuPermissoes.Incluir == 0 && oItensMenuPermissoes.Alterar == 0 && oItensMenuPermissoes.Excluir == 0)
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
            else
            {
                try
                {
                    if (oUsuario.Aplicativo == true)
                        menu.Visible = false;
                    else
                        menu.Visible = true;
                    Grade.DataSource = oMotoristasDados.PegaDados(oMotoristas, 0, false);
                    Grade.DataBind();
                    //Bairro, Cidade, Endereco, UF, DataAdmissao, DataDemissao, NomeConta, Nome, CEP, CPF, NumeroCTPS, RG, Serie, Fone, PercentualComissao
                    txtNome.MaxLength = oMotoristasDados.PegaTamanhoCampoVarChar("Nome");
                    txtBairro.MaxLength = oMotoristasDados.PegaTamanhoCampoVarChar("Bairro");
                    txtCEP.MaxLength = oMotoristasDados.PegaTamanhoCampoVarChar("CEP");
                    txtCidade.MaxLength = oMotoristasDados.PegaTamanhoCampoVarChar("Cidade");
                    txtEndereco.MaxLength = oMotoristasDados.PegaTamanhoCampoVarChar("Endereco");
                    txtFone.MaxLength = oMotoristasDados.PegaTamanhoCampoVarChar("Fone");
                    txtUF.MaxLength = oMotoristasDados.PegaTamanhoCampoVarChar("UF");
                    PermissaoIncluir();
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = ex.Message;
                }
            }
        }
        txtNome.Focus();
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
    protected void Excluir_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = oMotoristasDados.DadoExiste(oMotoristas.Codigo);
        if (txtNome.Text.Equals(""))
        {
            lblMensagem.Text = "Motorista inválido!";
        }
        if (lblMensagem.Text.Equals("Alterar"))
        {
            oMotoristasDados.Excluir(oMotoristas.Codigo, "");
            lblMensagem.Text = "Motorista excluído com sucesso!";

            Grade.DataSource = oMotoristasDados.PegaDados(oMotoristas, 0, false);
            Grade.DataBind();
        }
    }
    private void SalvarLog(string pOperacao)
    {
        clsLog oLog = new clsLog();
        clsLogDados oLogDados = new clsLogDados();
        oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
        oLog.CodigoUsuario = oUsuario.Codigo;
        oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
        oLog.LocalOperacao = "Cadastro de Motoristas";
        oLog.Operacao = pOperacao;
        oLog.Log = oLog.Log + "Código Motorista: " + hifCodigo.Value + " \n";
        oLog.Log = oLog.Log + "Nome: " + txtNome.Text + " \n";
        oLog.Log = oLog.Log + "Cidade: " + txtCidade.Text + " \n";
        oLog.Log = oLog.Log + "Endereço: " + txtBairro.Text + " \n";
        oLog.Log = oLog.Log + "CEP: " + txtCEP.Text + " \n";
        oLog.Log = oLog.Log + "Fone: " + txtFone.Text + " \n";
        oLog.Log = oLog.Log + "UF: " + txtUF.Text + " \n";
        oLog.Log = oLog.Log + "Data Admissão: " + datDataAdmissao.Data + " \n";
        oLog.Log = oLog.Log + "Data Demissão: " + datDataDemissao.Data + " \n";
        oLogDados.Inserir(oLog);
    }
    protected void Salvar_Click(object sender, EventArgs e)
    {
        lblMensagem.Text = "";        
        if (Salvar.Text == "Ok")
        {
            if (txtNome.Text.Equals(""))
            {
                lblMensagem.Text = "Motorista inválido!";
            }
 
            // não ocorreu erro salvar
            if (lblMensagem.Text.Equals(""))
            {
                // salvar
                oMotoristas = AtribuiDadosDoForm(oMotoristas);
                if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                    hifCodigo.Value = "-1";
                lblMensagem.Text =oMotoristasDados.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                if (lblMensagem.Text == "Incluir")
                {
                    oMotoristasDados.Inserir(oMotoristas);
                    SalvarLog("Inclusão");
                    Grade.DataSource = oMotoristasDados.PegaDados(oMotoristas, 0, false);
                    Grade.DataBind();
                    lblMensagem.Text = "Motorista incluído com sucesso!";
                }
                else if (lblMensagem.Text == "Alterar")
                {
                    string msgErr = oMotoristasDados.Alterar(oMotoristas, Convert.ToInt32(hifCodigo.Value));
                    if (msgErr.Length > 0)
                        lblMensagem.Text = msgErr;
                    else
                    {
                        lblMensagem.Text = "Motorista alterada com sucesso!";
                        SalvarLog("Alteração");
                        Grade.DataSource = oMotoristasDados.PegaDados(oMotoristas, 0, false);
                        Grade.DataBind();
                    }
                }
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;Cadastro de Motoristas";
                LimpaCampos();
            }
        }
        else if (Salvar.Text == "Confirma") // confirma a exclusão
        {
            lblMensagem.Text = "";
            if (txtNome.Text.Equals(""))
            {
                lblMensagem.Text = "Motorista inválido!";
            }
            if (lblMensagem.Text.Equals(""))
            {
                SalvarLog("Exclusão");
                oMotoristasDados.Excluir(Convert.ToInt32(hifCodigo.Value), "");
                lblMensagem.Text = "Motorista excluído com sucesso!";
                Salvar.Text = "Ok";
                lblTitulo.Text = "&nbsp;Cadastro de Motoristas";
                Grade.DataSource = oMotoristasDados.PegaDados(oMotoristas, 0, false);
                Grade.DataBind();
            }
        }
        LimpaCampos();
    }
    protected void ibnExcluir_Click(object sender, ImageClickEventArgs e)
    {
        lblTitulo.Text = "&nbsp;Exclusão de Motorista";
        Salvar.Text = "Confirma";
        lblMensagem.Text = "";
        Salvar.Enabled = true;
    }
    protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
    {        
        Salvar.Text = "Ok";
        lblTitulo.Text = "&nbsp;Alteração de Motorista";
        lblMensagem.Text = "";
    }
    protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView oGrade = (GridView)sender;
        if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "Nome")
        {
            txtNome.Text = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
            hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
            oMotoristas = oMotoristasDados.PegaDados(oMotoristas, Convert.ToInt32(hifCodigo.Value));
            AtribuiDadosDaClasse(oMotoristas);
            if (Salvar.Text != "Confirma")
                PermissaoAlterar();
        }
    }

    protected void AtribuiDadosDaClasse(clsFuncionarios pMotorista)
    {
        txtNome.Text = pMotorista.Nome;
        txtCidade.Text = pMotorista.Cidade;
        txtEndereco.Text = pMotorista.Endereco;
        txtBairro.Text = pMotorista.Bairro;
        txtCEP.Text = pMotorista.CEP;
        txtFone.Text = pMotorista.Fone;
        txtUF.Text = pMotorista.UF;
        
        if (pMotorista.DataAdmissao == "01/01/0100" || pMotorista.DataAdmissao == "01/01/0001" || pMotorista.DataAdmissao == null)
            datDataAdmissao.Data = "";
        else
            datDataAdmissao.Data = Convert.ToDateTime(pMotorista.DataAdmissao).ToString("dd/MM/yyyy");

        if (pMotorista.DataDemissao == "01/01/0100" || pMotorista.DataDemissao == "01/01/0001" || pMotorista.DataDemissao == null)
            datDataDemissao.Data = "";
        else
            datDataDemissao.Data = Convert.ToDateTime(pMotorista.DataDemissao).ToString("dd/MM/yyyy");
    }

    protected void LimpaCampos()
    {
        txtNome.Text = "";
        txtCidade.Text = "";
        txtEndereco.Text = "";
        txtBairro.Text = "";
        txtCEP.Text = "";
        txtFone.Text = "";
        txtUF.Text = "";
        datDataAdmissao.Data = "";
        datDataDemissao.Data = "";
        PermissaoIncluir();
    }
    protected clsFuncionarios AtribuiDadosDoForm(clsFuncionarios pMotorista)
    {
        pMotorista.Nome = txtNome.Text;
        pMotorista.Cidade = txtCidade.Text;
        pMotorista.Endereco = txtEndereco.Text;
        pMotorista.Bairro = txtBairro.Text;
        pMotorista.CEP = txtCEP.Text;
        txtFone.Text = pMotorista.Fone;
        pMotorista.UF = txtUF.Text;
        pMotorista.DataAdmissao = datDataAdmissao.Data;
        pMotorista.DataDemissao = datDataDemissao.Data;
        return pMotorista;
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        lblTitulo.Text = "&nbsp;Cadastro de Motoristas";
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
    protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
            geral.Ordem = e.SortExpression + " desc";
        else
            geral.Ordem = e.SortExpression + " asc";

        Grade.DataSource = oMotoristasDados.PreencheDataTableFuncionarios(geral.Ordem);
        Grade.DataBind();
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        Grade.DataSource = oMotoristasDados.PreencheDataTableFuncionarios(ddlFiltro.Text, txtFiltro.Text, ddlFiltro.Text);
        Grade.DataBind();
    }
}