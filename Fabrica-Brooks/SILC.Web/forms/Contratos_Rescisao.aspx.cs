using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

namespace SILC.Web.forms
{
    public partial class Contratos_Rescisao : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsContratos oContratos = new clsContratos();
        clsContratosDados oContratosDados = new clsContratosDados();
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
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "8");
            if (oItensMenuPermissoes.Alterar == 0)
                Response.Redirect("sempermissao.aspx");

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
                    try
                    {
                        if (oUsuario.Aplicativo == true)
                            menu1.Visible = false;
                        else
                            menu1.Visible = true;
                        try
                        {
                            _dt = oContratosDados.PegaDados(false);
                            Grade.DataSource = _dt;
                            Grade.DataBind();
                        }
                        finally
                        {
                            txtMotivoRecisao.MaxLength = oContratosDados.PegaTamanhoCampoVarChar("MotivoRecisao");
                            TotalContratos();
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMensagem.Text = ex.Message;
                    }
                }
                string r = Request.QueryString["view"];
                if (r != null)
                    ViewStateSetForm();
            }
            datDataRescisao.Focus();
        }

        protected void Excluir_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = oContratosDados.DadoExiste(oContratos.Codigo);
            if (txtNomeFantasia.Text.Equals(""))
            {
                lblMensagem.Text = "Contratos inválido!";
            }
            if (lblMensagem.Text.Equals("Alterar"))
            {
                oContratosDados.Excluir(oContratos.Codigo);
                lblMensagem.Text = "Contratos excluído com sucesso!";
                Grade.DataSource = oContratosDados.PreencheDataTableContratos("Codigo");
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
            oLog.LocalOperacao = "Rescisão-Contratos";
            oLog.Operacao = pOperacao;
            oLog.Log = oLog.Log + "Sequencial contrato nº: " + hifCodigo.Value + " \n";
            oLog.Log = oLog.Log + "Nome fantasia: " + txtNomeFantasia.Text + "(" + intCodigoCliente.Valor + ") \n";
            oLog.Log = oLog.Log + "Nome: " + txtNome.Text + ") \n";
            oLog.Log = oLog.Log + "CNPJ/CPF: " + txtCNPJ_CPF.Text + ") \n";
            oLog.Log = oLog.Log + "Data Registro: " + datDataRegistro.Data + " \n";
            oLog.Log = oLog.Log + "Data Rescisão: " + datDataRescisao.Data + " \n";
            oLog.Log = oLog.Log + "Documento Rescisão: " + ddlDocumentoRecisao.Text + " \n";
            oLog.Log = oLog.Log + "Motivo Rescisão: " + txtMotivoRecisao.Text + " \n";
            oLogDados.Inserir(oLog);
        }
        protected void Salvar_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = "";
            if (Salvar.Text == "Salvar")
            {
                if (txtNomeFantasia.Text.Equals("") || txtNome.Text.Equals("") || intCodigoCliente.Valor.Equals("") || !geral.IsNumeric(intCodigoCliente.Valor))
                {
                    lblMensagem.Text = "Contrato inválido!";
                }
                // não ocorreu erro salvar
                if (lblMensagem.Text.Equals(""))
                {
                    // salvar
                    oContratos = AtribuiDadosDoForm(oContratos);
                    if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                        hifCodigo.Value = "-1";
                    lblMensagem.Text = oContratosDados.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                    if (lblMensagem.Text == "Alterar")
                    {
                        // verificar se a senha informada é igual a gravada, caso contrário NÃO salvar senha nova
                        SalvarLog("Alteração");
                        string msgErr = oContratosDados.AlterarCamposRescisao(oContratos, Convert.ToInt32(hifCodigo.Value), Convert.ToInt32(intCodigoCliente.Valor));
                        if (msgErr.Length > 0)
                            lblMensagem.Text = msgErr;
                        else
                        {
                            ddlFiltro.Text = "Código Cliente";
                            txtFiltro.Text = oContratos.CodigoCliente.ToString();
                            CancelarOperacao();
                            lblMensagem.Text = "Alteração de dados da rescisão realizada com sucesso!";
                            btnOk_Click(sender, e);
                        }
                    }
                    Salvar.Text = "Salvar";
                    lblTitulo.Text = "&nbsp;Rescisão de Contrato(s)";
                }
            }
            else if (Salvar.Text == "Confirma") // confirma a exclusão
            {
                lblMensagem.Text = "";
                if (txtNomeFantasia.Text.Equals(""))
                {
                    lblMensagem.Text = "Contratos inválido!";
                }
                if (lblMensagem.Text.Equals(""))
                {
                    SalvarLog("Exclusão");
                    oContratosDados.Excluir(Convert.ToInt32(hifCodigo.Value));
                    lblMensagem.Text = "Contratos excluído com sucesso!";
                    Salvar.Text = "Salvar";
                    lblTitulo.Text = "&nbsp;Rescisão de Contrato(s)";
                    Grade.DataSource = oContratosDados.PreencheDataTableContratos("Codigo asc");
                    Grade.DataBind();
                }
            }
            LimpaCampos();
        }

        protected void ibnExcluir_Click(object sender, ImageClickEventArgs e)
        {
            lblTitulo.Text = "&nbsp;Exclusão de Contratos";
            Salvar.Text = "Confirma";
            lblMensagem.Text = "";
        }
        protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
        {
            Salvar.Text = "Salvar";
            lblTitulo.Text = "&nbsp;Alteração de Rescisão";
            lblMensagem.Text = "";
        }
        protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView oGrade = (GridView)sender;
            if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "Nome" && e.CommandArgument.ToString() != "NomeFantasia" &&
                e.CommandArgument.ToString() != "CodigoCliente" && e.CommandArgument.ToString() != "ValorContrato" && e.CommandArgument.ToString() != "DiaVencimento" &&
                e.CommandArgument.ToString() != "DataReajuste" && e.CommandArgument.ToString() != "IndiceReajuste" && e.CommandArgument.ToString() != "DataTermino" &&
                e.CommandArgument.ToString() != "DataRecisao")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    if (Grade.Rows.Count - 1 >= Convert.ToInt32(e.CommandArgument))
                    {
                        //txtNomeFantasia.Text = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Text;
                        hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
                        intCodigoCliente.Valor = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
                        oContratos = oContratosDados.PegaDados(oContratos, Convert.ToInt32(hifCodigo.Value), Convert.ToInt32(intCodigoCliente.Valor));
                        AtribuiDadosDaClasse(oContratos);

                        TiraSelecionado();
                        MarcaContratoGrade(Convert.ToInt32(e.CommandArgument));

                    }
                }
            }
        }
        protected void AtribuiDadosDaClasse(clsContratos pContratos)
        {
            txtNome.Text = pContratos.Nome;
            txtCNPJ_CPF.Text = pContratos.CNPJ_CPF;
            txtNomeFantasia.Text = pContratos.NomeFantasia;
            if (pContratos.DataRecisao == "01/01/0001" || pContratos.DataRecisao == "01/01/0100" || pContratos.DataRecisao == null || pContratos.DataRecisao == "")
                datDataRescisao.Data = "";
            else
                datDataRescisao.Data = pContratos.DataRecisao;
            ddlDocumentoRecisao.Text = pContratos.SituacaoRecisao;
            if (pContratos.DataRegistro == "01/01/0001" || pContratos.DataRegistro == "01/01/0100" || pContratos.DataRegistro == null || pContratos.DataRegistro == "")
                datDataRegistro.Data = "";
            else
                datDataRegistro.Data = pContratos.DataRegistro;
            txtMotivoRecisao.Text = pContratos.MotivoRescisao;
        }

        protected void LimpaCampos()
        {
            intCodigoCliente.Valor = "";
            txtNome.Text = "";
            txtNomeFantasia.Text = "";
            txtCNPJ_CPF.Text = "";
            datDataRescisao.Data = "";
            ddlDocumentoRecisao.Text = "";
            datDataRegistro.Data = "";
            txtMotivoRecisao.Text = "";
            Grade.Visible = true;
        }
        protected clsContratos AtribuiDadosDoForm(clsContratos pContratos)
        {
            if (geral.IsNumeric(intCodigoCliente.Valor))
            {
                pContratos.CodigoCliente = Convert.ToInt32(intCodigoCliente.Valor);
                pContratos.DataRecisao = datDataRescisao.Data;
                pContratos.SituacaoRecisao = ddlDocumentoRecisao.Text;
                pContratos.DataRegistro = datDataRegistro.Data;
                pContratos.MotivoRescisao = txtMotivoRecisao.Text;
            }
            return pContratos;
        }
        protected void ViewStateGetForm()
        {
            ViewState["Nome"] = txtNomeFantasia.Text;
        }
        protected void ViewStateSetForm()
        {
            txtNomeFantasia.Text = ViewState["Nome"].ToString();
        }
        private void CancelarOperacao()
        {
            lblTitulo.Text = "&nbsp;Rescisão de Contrato(s)";
            LimpaCampos();
            hifCodigo.Value = "";
            lblMensagem.Text = "";
            Salvar.Text = "Salvar";
        }
        protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                e.Row.Cells[1].Text = "";
                if (e.Row.Cells[6].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[6].Text.Replace(" ", "") == "01/01/0100")
                    e.Row.Cells[6].Text = "";
                for (int i = 0; i <= Grade.Columns.Count - 1; i++)
                {
                    if (e.Row.Cells[6].Text == "")
                        e.Row.Cells[i].ForeColor = System.Drawing.Color.Black;
                    else
                        e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
                geral.Ordem = e.SortExpression + " desc";
            else
                geral.Ordem = e.SortExpression + " asc";
            string _filtro = ddlFiltro.Text;
            if (ddlFiltro.Text.ToUpper() == "CÓDIGO CLIENTE")
                _filtro = "CodigoCliente";
            else if (ddlFiltro.Text == "Cancelados")
            {
                _filtro = "DataRecisao";
                txtFiltro.Text = ">0100-01-01";
            }
            else if (ddlFiltro.Text == "Não Cancelados")
            {
                _filtro = "DataRecisao";
                txtFiltro.Text = "";
            }
            else if (ddlFiltro.Text == "Código Contrato")
            {
                _filtro = "Codigo";
                if (!geral.IsNumeric(txtFiltro.Text))
                    txtFiltro.Text = "";
            }
            _dt = oContratosDados.PreencheDataTableContratos(geral.Ordem, txtFiltro.Text, _filtro);
            Grade.DataSource = _dt;
            Grade.DataBind();
            TiraSelecionado();
            TotalContratos();
            LimpaCampos();
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            Session["Clientes"] = null;
            string _Campo = "";
            string _Ordem = ddlFiltro.Text;
            _Campo = ddlFiltro.Text;
            if (ddlFiltro.Text.ToUpper() == "CÓDIGO CLIENTE")
            {
                _Campo = "CodigoCliente";
                _Ordem = "Codigo desc";
                if (!geral.IsNumeric(txtFiltro.Text))
                    txtFiltro.Text = "";
            }
            else if (ddlFiltro.Text == "Cancelados")
            {
                _Campo = "DataRecisao";
                _Ordem = _Campo;
                txtFiltro.Text = ">0100-01-01";
            }
            else if (ddlFiltro.Text == "Não Cancelados")
            {
                _Campo = "DataRecisao";
                _Ordem = _Campo;
                txtFiltro.Text = "";
            }
            else if (ddlFiltro.Text == "Código Contrato")
            {
                _Campo = "Codigo";
                _Ordem = _Campo;
                if (!geral.IsNumeric(txtFiltro.Text))
                    txtFiltro.Text = "0";
            }

            _dt = oContratosDados.PreencheDataTableContratos(_Ordem, txtFiltro.Text, _Campo);
            Grade.DataSource = _dt;
            Grade.DataBind();
            TotalContratos();
            if (_dt.Rows.Count > 0)
            {
                hifCodigo.Value = _dt.Rows[0]["Codigo"].ToString();
                intCodigoCliente.Valor = _dt.Rows[0]["CodigoCliente"].ToString();
                oContratos = oContratosDados.PegaDados(oContratos, Convert.ToInt32(hifCodigo.Value), Convert.ToInt32(intCodigoCliente.Valor));
                AtribuiDadosDaClasse(oContratos);
                TiraSelecionado();
                MarcaContratoGrade(0);
            }
        }
        protected void btnProcurar_Click(object sender, EventArgs e)
        {
            ViewStateGetForm();
        }
        private void TotalContratos()
        {
            lblTotal.Text = "";
            btnContratos.Text = "";
            if (ddlFiltro.SelectedValue.ToString().IndexOf("Cancelados") > -1)
            {
                decimal _valorTotalContratos = 0;
                foreach (DataRow _dr in _dt.Rows)
                {
                    if (_dr["ValorContrato"].ToString() != "")
                        _valorTotalContratos = _valorTotalContratos + Convert.ToDecimal(_dr["ValorContrato"]);
                }
                btnContratos.Text = "Total Contratos";
                lblTotal.Text = _valorTotalContratos.ToString("N2");
            }
        }
        private void TiraSelecionado()
        {
            bool bInterCor = false;
            for (int i = 0; i < Grade.Rows.Count; i++)
            {
                ImageButton ibnConsultar = (ImageButton)Grade.Rows[i].FindControl("ibnMudar");
                if (ibnConsultar != null)
                    ibnConsultar.ImageUrl = "~/Images/selecionar.png";
                if (bInterCor)
                    bInterCor = false;
                else
                    bInterCor = true;
                for (int j = 0; j < Grade.Columns.Count; j++)
                {
                    if (bInterCor)
                        Grade.Rows[i].Cells[j].BackColor = System.Drawing.Color.White;
                    else
                        Grade.Rows[i].Cells[j].BackColor = System.Drawing.Color.AliceBlue;
                }
            }
        }
        private void MarcaContratoGrade(int pLinha)
        {
            if (Grade.Rows.Count > 0)
            {
                for (int i = 0; i < Grade.Columns.Count; i++)
                {
                    Grade.Rows[pLinha].Cells[i].BackColor = System.Drawing.Color.CadetBlue;
                }
                ImageButton ibnConsultar = (ImageButton)Grade.Rows[pLinha].FindControl("ibnMudar");
                if (ibnConsultar != null)
                    ibnConsultar.ImageUrl = "~/Images/selecionado.png";
            }
        }
        protected void Grade_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (geral.Ordem == "")
                    geral.Ordem = "NomeFantasia";
                string _filtro = ddlFiltro.Text;
                if (ddlFiltro.Text.ToUpper() == "CÓDIGO CLIENTE")
                    _filtro = "CodigoCliente";
                else if (ddlFiltro.Text == "Cancelados")
                {
                    //geral.Ordem = "DataRecisao";
                    _filtro = "DataRecisao";
                    txtFiltro.Text = ">0100-01-01";
                }
                else if (ddlFiltro.Text == "Não Cancelados")
                {
                    //geral.Ordem = "DataRecisao";
                    txtFiltro.Text = "";
                    _filtro = "DataRecisao";
                }
                else if (ddlFiltro.Text == "Código Contrato")
                {
                    _filtro = "Codigo";
                    if (!geral.IsNumeric(txtFiltro.Text))
                        txtFiltro.Text = "";
                }
                _dt = oContratosDados.PreencheDataTableContratos(geral.Ordem, txtFiltro.Text, _filtro);
                Grade.DataSource = _dt;
            }
            finally
            {
                Grade.PageIndex = e.NewPageIndex;
                Grade.DataBind();
                TiraSelecionado();
                LimpaCampos();
                TotalContratos();
            }
        }
        protected void ddlFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFiltro.Text = "";
            btnOk_Click(sender, e);
        }
        protected void btnCancelaAlteracaoReajuste_Click(object sender, EventArgs e)
        {
            CancelarOperacao();
        }
        protected void btnProcurar_Click1(object sender, EventArgs e)
        {
            if (Session["Clientes"] != null)
            {
                CancelarOperacao();
                clsClientes oCl = new clsClientes();
                oCl = (clsClientes)Session["Clientes"];
                intCodigoCliente.Valor = oCl.Codigo.ToString();
                txtNome.Text = oCl.Nome;
                txtNomeFantasia.Text = oCl.NomeFantasia;
                txtCNPJ_CPF.Text = oCl.CNPJ_CPF;
            }
        }
    }
}