using System; 
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;

namespace SILC.Web.forms
{
    public partial class Caminhoes : System.Web.UI.Page
    {
        clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
        clsCaminhoes oCaminhoes = new clsCaminhoes();
        clsCaminhoesDados oCaminhoesDados = new clsCaminhoesDados();
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
                oItensMenuPermissoes = oPermissaoDados.PegaPermissoesMenu(oUsuario.Codigo.ToString(), "1");
            if (oItensMenuPermissoes.Consultar == 0 && oItensMenuPermissoes.Incluir == 0 && oItensMenuPermissoes.Alterar == 0 && oItensMenuPermissoes.Excluir == 0)
                Response.Redirect("sempermissao.aspx");

            if (!IsPostBack)
            {
                datDataCadastro.Data = DateTime.Now.Date.ToString("dd/MM/yyyy");
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
                            Grade.DataSource = oCaminhoesDados.PegaDados(oCaminhoes, 0, false);
                            Grade.DataBind();
                        }
                        finally
                        {
                            txtChassi.MaxLength = oCaminhoesDados.PegaTamanhoCampoVarChar("Chassi");
                            txtCidade.MaxLength = oCaminhoesDados.PegaTamanhoCampoVarChar("Cidade");
                            txtCor.MaxLength = oCaminhoesDados.PegaTamanhoCampoVarChar("Cor");
                            txtMarca.MaxLength = oCaminhoesDados.PegaTamanhoCampoVarChar("Marca");
                            txtModelo.MaxLength = oCaminhoesDados.PegaTamanhoCampoVarChar("Modelo");
                            txtPlacas.MaxLength = oCaminhoesDados.PegaTamanhoCampoVarChar("Placas");
                            txtRenavam.MaxLength = oCaminhoesDados.PegaTamanhoCampoVarChar("Renavam");
                            txtTipoVeiculo.MaxLength = oCaminhoesDados.PegaTamanhoCampoVarChar("TipoVeiculo");
                            PermissaoIncluir();
                            PermissaoAlterar();
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMensagem.Text = ex.Message;
                    }
                }
            }
            txtModelo.Focus();
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
        private void MarcaGrade(int pLinha)
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
                Grade.Rows[pLinha].Focus();
            }
        }
        protected void Excluir_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = oCaminhoesDados.DadoExiste(oCaminhoes.Codigo);
            if (txtModelo.Text.Equals(""))
            {
                lblMensagem.Text = "Modelo do caminhão inválido!";
            }
            if (lblMensagem.Text.Equals("Alterar"))
            {
                oCaminhoesDados.Excluir(oCaminhoes.Codigo, "");
                lblMensagem.Text = "Caminhão excluído com sucesso!";

                Grade.DataSource = oCaminhoesDados.PegaDados(oCaminhoes, 0, false);
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
            oLog.LocalOperacao = "Cadastro de Caminhões";
            oLog.Operacao = pOperacao;
            oLog.Log = oLog.Log + "Código caminhão: " + hifCodigo.Value + " \n";
            oLog.Log = oLog.Log + "Data Cadastro: " + datDataCadastro.Data + " \n";
            oLog.Log = oLog.Log + "Descrição/Modelo: " + txtModelo.Text + " \n";
            if (chkInativo.Checked)
                oLog.Log = oLog.Log + "Ativo: Não \n";
            else
                oLog.Log = oLog.Log + "Ativo: Sim \n";
            oLog.Log = oLog.Log + "Ano Fabricação/Modelo: " + intAnoFabricacao.Valor + "/" + intAnoModelo.Valor + " \n";
            oLog.Log = oLog.Log + "Marca: " + txtMarca.Text + " \n";
            oLog.Log = oLog.Log + "Tipo veículo: " + txtTipoVeiculo.Text + " \n";
            oLog.Log = oLog.Log + "Placas: " + txtPlacas.Text + " \n";
            oLog.Log = oLog.Log + "Cor: " + txtCor.Text + " \n";
            oLog.Log = oLog.Log + "Chassi: " + txtChassi.Text + " \n";
            oLog.Log = oLog.Log + "RENAVAM: " + txtRenavam.Text + " \n";
            oLog.Log = oLog.Log + "Cidade emplacamento: " + txtCidade.Text + "\n";
            oLog.Log = oLog.Log + "Km Inicial: " + intKmInicial.Valor + "\n";
            oLog.Log = oLog.Log + "Data Aquisição: " + datDataAquisicao.Data + " \n";
            oLog.Log = oLog.Log + "Valor Aquisição: " + valValorAquisicao.Valor + " \n";
            oLog.Log = oLog.Log + "Vencimento IPVA: " + datVencimentoIPVA.Data + " \n";
            oLog.Log = oLog.Log + "Vcto Licenciamento: " + datVencimentoLicenciamento.Data + " \n";
            oLog.Log = oLog.Log + "Vcto Seguro Obrigatório:" + datVencimentoSeguroObrigatorio.Data + " \n";
            oLog.Log = oLog.Log + "Seguro Frota: " + datSeguroFrota.Data + " \n";
            if (chkProprio.Checked)
                oLog.Log = oLog.Log + "É Próprio: Sim \n";
            else if (chkTerceiro.Checked)
                oLog.Log = oLog.Log + "É de Terceiro: Sim  \n";
            oLogDados.Inserir(oLog);

        }
        protected void Salvar_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = "";
            if (Salvar.Text == "Ok")
            {
                if (txtModelo.Text.Equals(""))
                {
                    lblMensagem.Text = "Modelo do caminhão inválido!";
                }

                // não ocorreu erro salvar
                if (lblMensagem.Text.Equals(""))
                {
                    // salvar
                    oCaminhoes = AtribuiDadosDoForm(oCaminhoes);
                    if (hifCodigo.Value == "" || hifCodigo.Value == "0")
                        hifCodigo.Value = "-1";
                    lblMensagem.Text = oCaminhoesDados.DadoExiste(Convert.ToInt32(hifCodigo.Value));
                    if (lblMensagem.Text == "Incluir")
                    {
                        oCaminhoesDados.Inserir(oCaminhoes);
                        SalvarLog("Inclusão");
                        Grade.DataSource = oCaminhoesDados.PegaDados(oCaminhoes, 0, false);
                        Grade.DataBind();
                        lblMensagem.Text = "Caminhão incluído com sucesso!";
                    }
                    else if (lblMensagem.Text == "Alterar")
                    {
                        // verificar se a senha informada é igual a gravada, caso contrário NÃO salvar senha nova
                        string msgErr = oCaminhoesDados.Alterar(oCaminhoes, Convert.ToInt32(hifCodigo.Value));
                        if (msgErr.Length > 0)
                            lblMensagem.Text = msgErr;
                        else
                        {
                            SalvarLog("Alteração");
                            lblMensagem.Text = "Caminhão alterado com sucesso!";
                            Grade.DataSource = oCaminhoesDados.PegaDados(oCaminhoes, 0, false);
                            Grade.DataBind();
                        }
                    }
                    Salvar.Text = "Ok";
                    lblTitulo.Text = "&nbsp;Cadastro de Caminhões";
                    LimpaCampos();
                    TiraSelecionado();
                }
            }
            else if (Salvar.Text == "Confirma") // confirma a exclusão
            {
                lblMensagem.Text = "";
                if (txtModelo.Text.Equals(""))
                {
                    lblMensagem.Text = "Modelo do caminhão inválido!";
                }
                if (lblMensagem.Text.Equals(""))
                {
                    oCaminhoesDados.Excluir(Convert.ToInt32(hifCodigo.Value), "");
                    lblMensagem.Text = "Caminhão excluído com sucesso!";
                    SalvarLog("Exclusão");
                    Salvar.Text = "Ok";
                    lblTitulo.Text = "&nbsp;Cadastro de Caminhões";
                    Grade.DataSource = oCaminhoesDados.PegaDados(oCaminhoes, 0, false);
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
            lblTitulo.Text = "&nbsp;Exclusão de Caminhão";
            Salvar.Text = "Confirma";
            lblMensagem.Text = "";
            Salvar.Enabled = true;
        }
        protected void ibnMudar_Click(object sender, ImageClickEventArgs e)
        {
            Salvar.Text = "Ok";
            lblTitulo.Text = "&nbsp;Alteração de Caminhão";
            lblMensagem.Text = "";
        }
        protected void Grade_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView oGrade = (GridView)sender;
            if (e.CommandArgument.ToString() != "Modelo" && e.CommandArgument.ToString() != "AnoFabricacao" && e.CommandArgument.ToString() != "EhProprio"
                && e.CommandArgument.ToString() != "Marca" && e.CommandArgument.ToString() != "TipoVeiculo" && e.CommandArgument.ToString() != "Placas"
                && e.CommandArgument.ToString() != "Cor" && e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "DataAquisicao"
                && e.CommandArgument.ToString() != "DataCadastro" && e.CommandArgument.ToString() != "Inativo")
            {
                btnCancelar.Enabled = true;
                txtModelo.Text = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
                hifCodigo.Value = oGrade.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
                oCaminhoes = oCaminhoesDados.PegaDados(oCaminhoes, Convert.ToInt32(hifCodigo.Value));
                AtribuiDadosDaClasse(oCaminhoes);
                TiraSelecionado();
                MarcaGrade(Convert.ToInt32(e.CommandArgument));

                if (Salvar.Text != "Confirma")
                    PermissaoAlterar();
            }
        }

        protected void AtribuiDadosDaClasse(clsCaminhoes pCaminhao)
        {
            if (pCaminhao.Inativo == 1)
                chkInativo.Checked = true;
            else
                chkInativo.Checked = false;

            if (pCaminhao.DataCadastro == "01/01/0001" || pCaminhao.DataCadastro == "01/01/0100" || pCaminhao.DataCadastro == null)
                datDataCadastro.Data = "";
            else
                datDataCadastro.Data = Convert.ToDateTime(pCaminhao.DataCadastro).ToString("dd/MM/yyyy");

            if (pCaminhao.AnoFabricacao > 0)
                intAnoFabricacao.Valor = pCaminhao.AnoFabricacao.ToString();
            if (pCaminhao.AnoModelo > 0)
                intAnoModelo.Valor = pCaminhao.AnoModelo.ToString();
            txtChassi.Text = pCaminhao.Chassi;
            txtCidade.Text = pCaminhao.Cidade;
            txtCor.Text = pCaminhao.Cor;
            if (pCaminhao.dtVctoSegr == "01/01/0001" || pCaminhao.dtVctoSegr == "01/01/0100" || pCaminhao.dtVctoSegr == null)
                datVencimentoSeguroObrigatorio.Data = "";
            else
                datVencimentoSeguroObrigatorio.Data = Convert.ToDateTime(pCaminhao.dtVctoSegr).ToString("dd/MM/yyyy");
            intKmInicial.Valor = pCaminhao.Kilometragem.ToString();
            txtMarca.Text = pCaminhao.Marca;
            txtTipoVeiculo.Text = pCaminhao.TipoVeiculo;
            txtModelo.Text = pCaminhao.Modelo;
            txtPlacas.Text = pCaminhao.Placas;
            txtRenavam.Text = pCaminhao.Renavam;
            valValorAquisicao.Valor = pCaminhao.ValorFranquia.ToString();
            if (pCaminhao.VctoIPVA == "01/01/0001" || pCaminhao.VctoIPVA == "01/01/0100" || pCaminhao.VctoIPVA == null)
                datVencimentoIPVA.Data = "";
            else
                datVencimentoIPVA.Data = Convert.ToDateTime(pCaminhao.VctoIPVA).ToString("dd/MM/yyyy");
            if (pCaminhao.VctoLicenciamento == "01/01/0001" || pCaminhao.VctoLicenciamento == "01/01/0100" || pCaminhao.VctoLicenciamento == null)
                datVencimentoLicenciamento.Data = "";
            else
                datVencimentoLicenciamento.Data = Convert.ToDateTime(pCaminhao.VctoLicenciamento).ToString("dd/MM/yyyy");
            valValorAquisicao.Valor = pCaminhao.ValorAquisicao.ToString();
            if (pCaminhao.DataAquisicao == "01/01/0001" || pCaminhao.DataAquisicao == "01/01/0100" || pCaminhao.DataAquisicao == null)
                datDataAquisicao.Data = "";
            else
                datDataAquisicao.Data = Convert.ToDateTime(pCaminhao.DataAquisicao).ToString("dd/MM/yyyy");
            if (pCaminhao.VencimentoSeguroFrota == "01/01/0001" || pCaminhao.VencimentoSeguroFrota == "01/01/0100" || pCaminhao.VencimentoSeguroFrota == null)
                datSeguroFrota.Data = "";
            else
                datSeguroFrota.Data = Convert.ToDateTime(pCaminhao.VencimentoSeguroFrota).ToString("dd/MM/yyyy");
            if (pCaminhao.EhProprio == 1)
            {
                chkProprio.Checked = true;
                chkTerceiro.Checked = false;
            }
            else if (pCaminhao.EhProprio == 0)
            {
                chkProprio.Checked = false;
                chkTerceiro.Checked = true;
            }
        }

        protected void LimpaCampos()
        {
            datDataCadastro.Data = DateTime.Now.Date.ToString("dd/MM/yyyy");
            chkInativo.Checked = false;
            intAnoModelo.Valor = "";
            intAnoFabricacao.Valor = "";
            txtChassi.Text = "";
            txtCidade.Text = "";
            txtCor.Text = "";
            datVencimentoSeguroObrigatorio.Data = "";
            intKmInicial.Valor = "";
            txtMarca.Text = "";
            txtTipoVeiculo.Text = "";
            txtModelo.Text = "";
            txtPlacas.Text = "";
            txtRenavam.Text = "";
            valValorAquisicao.Valor = "";
            datVencimentoIPVA.Data = "";
            datVencimentoLicenciamento.Data = "";
            datDataAquisicao.Data = "";
            chkProprio.Checked = true;
            chkTerceiro.Checked = false;
            hifCodigo.Value = "";
            datSeguroFrota.Data = "";
            PermissaoIncluir();
        }

        protected clsCaminhoes AtribuiDadosDoForm(clsCaminhoes pCaminhao)
        {
            if (datDataCadastro.Data != "")
                pCaminhao.DataCadastro = datDataCadastro.Data;

            if (chkInativo.Checked)
                pCaminhao.Inativo = 1;
            else if (!chkInativo.Checked)
                pCaminhao.Inativo = 0;

            if (intAnoModelo.Valor != "")
            {
                pCaminhao.AnoModelo = Convert.ToInt32(intAnoModelo.Valor);
                pCaminhao.AnoFabricacao = Convert.ToInt32(intAnoFabricacao.Valor);
            }
            pCaminhao.Chassi = txtChassi.Text;
            pCaminhao.Cidade = txtCidade.Text;
            pCaminhao.Cor = txtCor.Text;
            if (datVencimentoSeguroObrigatorio.Data != "")
                pCaminhao.dtVctoSegr = datVencimentoSeguroObrigatorio.Data;
            if (intKmInicial.Valor != "")
                pCaminhao.Kilometragem = Convert.ToInt32(intKmInicial.Valor);
            pCaminhao.Marca = txtMarca.Text;
            pCaminhao.TipoVeiculo = txtTipoVeiculo.Text;
            pCaminhao.Modelo = txtModelo.Text;
            pCaminhao.Placas = txtPlacas.Text;
            pCaminhao.Renavam = txtRenavam.Text;
            if (valValorAquisicao.Valor != "")
                pCaminhao.ValorFranquia = Convert.ToDecimal(valValorAquisicao.Valor);
            if (datVencimentoIPVA.Data != "")
                pCaminhao.VctoIPVA = datVencimentoIPVA.Data;
            if (datVencimentoLicenciamento.Data != "")
                pCaminhao.VctoLicenciamento = datVencimentoLicenciamento.Data;
            if (datSeguroFrota.Data != "")
                pCaminhao.VencimentoSeguroFrota = datSeguroFrota.Data;
            if (valValorAquisicao.Valor != "")
                pCaminhao.ValorAquisicao = Convert.ToDecimal(valValorAquisicao.Valor);
            if (datDataAquisicao.Data != "")
                pCaminhao.DataAquisicao = datDataAquisicao.Data;
            if (chkProprio.Checked)
                pCaminhao.EhProprio = 1;
            if (chkTerceiro.Checked)
                pCaminhao.EhProprio = 0;
            return pCaminhao;
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "&nbsp;Cadastro de Caminhões";
            LimpaCampos();
            TiraSelecionado();
            hifCodigo.Value = "";
            lblMensagem.Text = "";
            chkProprio.Checked = true;
            chkTerceiro.Checked = false;
            Salvar.Text = "Ok";
            btnCancelar.Enabled = false;
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
                if (e.Row.Cells[4].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[4].Text.Replace(" ", "") == "01/01/0100")
                    e.Row.Cells[4].Text = "";

                if (e.Row.Cells[5].Text.Replace(" ", "") == "1")
                    e.Row.Cells[5].Text = "Sim";
                else if (e.Row.Cells[5].Text.Replace(" ", "") == "0" || e.Row.Cells[5].Text.Replace(" ", "") == "" || e.Row.Cells[4].Text.Replace(" ", "") == "&nbsp;")
                    e.Row.Cells[5].Text = "Não";
                if (e.Row.Cells[10].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[10].Text.Replace(" ", "") == "01/01/0100")
                    e.Row.Cells[10].Text = "";
                if (e.Row.Cells[11].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[11].Text.Replace(" ", "") == "01/01/0100")
                    e.Row.Cells[11].Text = "";
                if (e.Row.Cells[12].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[12].Text.Replace(" ", "") == "01/01/0100")
                    e.Row.Cells[12].Text = "";
                if (e.Row.Cells[13].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[13].Text.Replace(" ", "") == "01/01/0100")
                    e.Row.Cells[13].Text = "";
                if (e.Row.Cells[14].Text.Replace(" ", "") == "01/01/0001" || e.Row.Cells[14].Text.Replace(" ", "") == "01/01/0100")
                    e.Row.Cells[14].Text = "";
                if (e.Row.Cells[15].Text.Replace(" ", "") == "1")
                    e.Row.Cells[15].Text = "Sim";
                else if (e.Row.Cells[15].Text.Replace(" ", "") == "0" || e.Row.Cells[15].Text.Replace(" ", "") == "" || e.Row.Cells[15].Text.Replace(" ", "") == "&nbsp;")
                    e.Row.Cells[15].Text = "Não";
            }
        }
        protected void Grade_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (geral.Ordem == e.SortExpression + " asc" || geral.Ordem == string.Empty)
                geral.Ordem = e.SortExpression + " desc";
            else
                geral.Ordem = e.SortExpression + " asc";

            Grade.DataSource = oCaminhoesDados.PreencheDataTableCaminhoes(geral.Ordem);
            Grade.DataBind();
        }
    }
}