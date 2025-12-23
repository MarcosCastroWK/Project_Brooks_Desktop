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
    public partial class ItensMenuPermissoes : System.Web.UI.Page
    {
        private clsItensMenuPermissoesDados oItensMenuPermissoesDados = new clsItensMenuPermissoesDados();
        private clsItensMenuDados oItensMenuDados = new clsItensMenuDados();

        private DataTable dtItensMenu = new DataTable();
        private DataTable dtPermissoes = new DataTable();
        private int regs = 0;

        clsUsuarios oUsuario = new clsUsuarios();
        clsUsuarioDados oUsuarioDados = new clsUsuarioDados();

        DataTable _dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                geral.AcessoPrincipal = false;
                if (geral.Demonstracao)
                {
                    //Salvar.Enabled = false;
                }
                oUsuario = (clsUsuarios)Session["oUsuario"];
                if (oUsuario == null || oUsuario.Nome.ToLower() != "teixeira")
                {
                    Response.Redirect("../default.aspx", true);
                }
                else
                {
                    geral.CodigoEmpresa = oUsuario.CodigoEmpresa;

                    clsUsuarioDados oUsuariosDados = new clsUsuarioDados();
                    GradeUsuarios.DataSource = oUsuariosDados.PreencheDataTableUsuarios("Codigo desc", geral.CodigoEmpresa);
                    GradeUsuarios.DataBind();

                    clsUsuarioDados oUsuarioDados = new clsUsuarioDados();
                    int iCodigoUsuario = oUsuarioDados.PegaCodigoUltimoUsuario();
                    TiraSelecionado();
                    MarcaGradeUsuarios(0);
                    hifCodigoUsuario.Value = iCodigoUsuario.ToString();
                    refreshGrade(iCodigoUsuario);
                }
            }
        }

        protected void Grade_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                TextBox _txtCodigoItemMenu = (TextBox)e.Row.Cells[0].FindControl("txtCodigoItemMenu");
                if (_txtCodigoItemMenu.Text != "")
                {
                    clsItensMenuPermissoes oIMP = new clsItensMenuPermissoes();
                    oIMP = oItensMenuPermissoesDados.PegaPermissoesMenu(hifCodigoUsuario.Value, _txtCodigoItemMenu.Text);

                    CheckBox chkConsultar = (CheckBox)e.Row.Cells[2].FindControl("chkConsultar");
                    if (oIMP.Consultar == 1)
                        chkConsultar.Checked = true;

                    CheckBox chkIncluir = (CheckBox)e.Row.Cells[3].FindControl("chkIncluir");
                    if (oIMP.Incluir == 1)
                        chkIncluir.Checked = true;

                    CheckBox chkAlterar = (CheckBox)e.Row.Cells[4].FindControl("chkAlterar");
                    if (oIMP.Alterar == 1)
                        chkAlterar.Checked = true;

                    CheckBox chkExcluir = (CheckBox)e.Row.Cells[5].FindControl("chkExcluir");
                    if (oIMP.Excluir == 1)
                        chkExcluir.Checked = true;

                    chkConsultar.Enabled = true;
                    chkAlterar.Enabled = true;
                    chkExcluir.Enabled = true;
                    chkIncluir.Enabled = true;
                    if (_txtCodigoItemMenu.Text == "5")
                    {
                        chkConsultar.Enabled = false;
                        chkAlterar.Enabled = false;
                        chkExcluir.Enabled = false;
                    }
                    if (_txtCodigoItemMenu.Text == "6" || _txtCodigoItemMenu.Text == "7")
                        chkIncluir.Enabled = false;

                    if (_txtCodigoItemMenu.Text == "8")
                    {
                        chkConsultar.Enabled = false;
                        chkIncluir.Enabled = false;
                        chkExcluir.Enabled = false;
                    }

                    if (_txtCodigoItemMenu.Text == "15" || _txtCodigoItemMenu.Text == "16")
                    {
                        chkIncluir.Enabled = false;
                        chkAlterar.Enabled = false;
                        chkExcluir.Enabled = false;
                    }
                    if (_txtCodigoItemMenu.Text == "17" || _txtCodigoItemMenu.Text == "18" || _txtCodigoItemMenu.Text == "20" || _txtCodigoItemMenu.Text == "21" ||
                        _txtCodigoItemMenu.Text == "54" || _txtCodigoItemMenu.Text == "58" || _txtCodigoItemMenu.Text == "62")
                    {
                        chkIncluir.Enabled = false;
                        chkExcluir.Enabled = false;
                    }
                    if (_txtCodigoItemMenu.Text == "19" || _txtCodigoItemMenu.Text == "22" || _txtCodigoItemMenu.Text == "23" || _txtCodigoItemMenu.Text == "24" ||
                        _txtCodigoItemMenu.Text == "27" || _txtCodigoItemMenu.Text == "28" || _txtCodigoItemMenu.Text == "29" || _txtCodigoItemMenu.Text == "64" ||
                        (Convert.ToInt16(_txtCodigoItemMenu.Text) >= 30 && Convert.ToInt16(_txtCodigoItemMenu.Text) <= 51))
                    {
                        chkAlterar.Enabled = false;
                        chkIncluir.Enabled = false;
                        chkExcluir.Enabled = false;
                    }
                    if (_txtCodigoItemMenu.Text == "25")
                    {
                        chkAlterar.Enabled = false;
                    }
                }
            }
        }

        private void refreshGrade(int pCodigoUsuario)
        {
            dtPermissoes = new DataTable();
            dtPermissoes = oItensMenuPermissoesDados.PreencheDataTable("im.Item", pCodigoUsuario);

            Grade.DataSource = dtPermissoes;
            Grade.DataBind();
        }

        protected void GradeUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.ToString() != "Codigo" && e.CommandArgument.ToString() != "Nome")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    if (GradeUsuarios.Rows.Count - 1 >= Convert.ToInt32(e.CommandArgument))
                    {
                        TiraSelecionado();
                        MarcaGradeUsuarios(Convert.ToInt32(e.CommandArgument));
                        hifCodigoUsuario.Value = GradeUsuarios.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
                        if (hifCodigoUsuario.Value != "")
                            refreshGrade(Convert.ToInt32(hifCodigoUsuario.Value));
                    }
                }
            }
        }
        private void TiraSelecionado()
        {
            bool bInterCor = false;
            for (int i = 0; i < GradeUsuarios.Rows.Count; i++)
            {
                ImageButton ibnConsultar = (ImageButton)GradeUsuarios.Rows[i].FindControl("ibnMudar");
                if (ibnConsultar != null)
                    ibnConsultar.ImageUrl = "~/Images/selecionar.png";
                if (bInterCor)
                    bInterCor = false;
                else
                    bInterCor = true;
                for (int j = 0; j < GradeUsuarios.Columns.Count; j++)
                {
                    if (bInterCor)
                        GradeUsuarios.Rows[i].Cells[j].BackColor = System.Drawing.Color.White;
                    else
                        GradeUsuarios.Rows[i].Cells[j].BackColor = System.Drawing.Color.AliceBlue;
                }
            }
        }
        private void MarcaGradeUsuarios(int pLinha)
        {
            if (GradeUsuarios.Rows.Count > 0)
            {
                for (int i = 0; i < GradeUsuarios.Columns.Count; i++)
                {
                    GradeUsuarios.Rows[pLinha].Cells[i].BackColor = System.Drawing.Color.CadetBlue;
                }
                ImageButton ibnConsultar = (ImageButton)GradeUsuarios.Rows[pLinha].FindControl("ibnMudar");
                if (ibnConsultar != null)
                    ibnConsultar.ImageUrl = "~/Images/selecionado.png";
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            SalvarDados();
            SalvarLog("Alteração");
        }
        private void SalvarLog(string pOperacao)
        {
            clsLog oLog = new clsLog();
            clsLogDados oLogDados = new clsLogDados();
            oLog.Data = DateTime.Now.ToString("yyyy-MM-dd");
            oLog.CodigoUsuario = geral.CodigoUsuarioAtual;
            oLog.Hora = DateTime.Now.ToString("hh:mm:ss");
            oLog.LocalOperacao = "Permissões";
            oLog.Operacao = pOperacao;
            oLog.Log = oLog.Log + "Parâmetro Permissões: " + hifCodigoUsuario.Value + " \n";

            oLog.Log = oLog.Log + "Usuário: " + geral.UsuarioAtual + " (" + geral.CodigoUsuarioAtual + ") \n";
            oLog.Log = oLog.Log + "Hora: " + DateTime.Now.ToString("hh:mm:ss") + " \n";
            clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
            foreach (GridViewRow gvr in Grade.Rows)
            {
                TextBox _txtCodigoItemMenu = (TextBox)gvr.Cells[0].FindControl("txtCodigoItemMenu");
                TextBox _txtItem = (TextBox)gvr.Cells[1].FindControl("txtItem");
                CheckBox _chkConsultar = (CheckBox)gvr.Cells[2].FindControl("chkConsultar");
                CheckBox _chkIncluir = (CheckBox)gvr.Cells[3].FindControl("chkIncluir");
                CheckBox _chkAlterar = (CheckBox)gvr.Cells[4].FindControl("chkAlterar");
                CheckBox _chkExcluir = (CheckBox)gvr.Cells[5].FindControl("chkExcluir");
                if (_txtCodigoItemMenu.Text != "" && geral.IsNumeric(_txtCodigoItemMenu.Text) &&
                    hifCodigoUsuario.Value != "" && geral.IsNumeric(hifCodigoUsuario.Value))
                {
                    oItensMenuPermissoes.CodigoUsuario = Convert.ToInt32(hifCodigoUsuario.Value);
                    oItensMenuPermissoes.CodigoItensMenu = Convert.ToInt32(_txtCodigoItemMenu.Text);
                    oLog.Log = oLog.Log + "Item: (" + _txtCodigoItemMenu.Text + ") " + _txtItem.Text + ": ";
                    if (_chkConsultar.Checked)
                        oLog.Log = oLog.Log + "Consultar: Sim, ";
                    else
                        oLog.Log = oLog.Log + "Consultar: Não, ";
                    if (_chkIncluir.Checked)
                        oLog.Log = oLog.Log + "Incluir: Sim, ";
                    else
                        oLog.Log = oLog.Log + "Incluir: Não, ";
                    if (_chkAlterar.Checked)
                        oLog.Log = oLog.Log + "Alterar: Sim, ";
                    else
                        oLog.Log = oLog.Log + "Alterar: Não, ";
                    if (_chkExcluir.Checked)
                        oLog.Log = oLog.Log + "Excluir: Sim \n";
                    else
                        oLog.Log = oLog.Log + "Excluir: Não \n";
                }
            }
            oLogDados.Inserir(oLog);
        }

        private void SalvarDados()
        {
            foreach (GridViewRow gvr in Grade.Rows)
            {
                TextBox _txtCodigoItemMenu = (TextBox)gvr.Cells[0].FindControl("txtCodigoItemMenu");
                CheckBox _chkConsultar = (CheckBox)gvr.Cells[2].FindControl("chkConsultar");
                CheckBox _chkIncluir = (CheckBox)gvr.Cells[3].FindControl("chkIncluir");
                CheckBox _chkAlterar = (CheckBox)gvr.Cells[4].FindControl("chkAlterar");
                CheckBox _chkExcluir = (CheckBox)gvr.Cells[5].FindControl("chkExcluir");
                if (_txtCodigoItemMenu.Text != "" && geral.IsNumeric(_txtCodigoItemMenu.Text) &&
                    hifCodigoUsuario.Value != "" && geral.IsNumeric(hifCodigoUsuario.Value))
                {
                    clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
                    oItensMenuPermissoes.CodigoUsuario = Convert.ToInt32(hifCodigoUsuario.Value);
                    oItensMenuPermissoes.CodigoItensMenu = Convert.ToInt32(_txtCodigoItemMenu.Text);

                    oItensMenuPermissoes.Consultar = 0;
                    if (_chkConsultar.Checked)
                        oItensMenuPermissoes.Consultar = 1;

                    oItensMenuPermissoes.Incluir = 0;
                    if (_chkIncluir.Checked)
                        oItensMenuPermissoes.Incluir = 1;

                    oItensMenuPermissoes.Alterar = 0;
                    if (_chkAlterar.Checked)
                        oItensMenuPermissoes.Alterar = 1;

                    oItensMenuPermissoes.Excluir = 0;
                    if (_chkExcluir.Checked)
                        oItensMenuPermissoes.Excluir = 1;

                    if (!oItensMenuPermissoesDados.ExistePermissaoMenu(hifCodigoUsuario.Value, _txtCodigoItemMenu.Text))
                        oItensMenuPermissoesDados.Inserir(oItensMenuPermissoes);
                    else
                        oItensMenuPermissoesDados.Alterar(oItensMenuPermissoes, oItensMenuPermissoes.CodigoItensMenu, oItensMenuPermissoes.CodigoUsuario);
                }
            }
        }

        protected void btnLiberarTodas_Click(object sender, EventArgs e)
        {
            TirarLiberarPermissoes(1);
        }

        private void TirarLiberarPermissoes(int bLiberarTirar)
        {
            clsItensMenuPermissoes oItensMenuPermissoes = new clsItensMenuPermissoes();
            foreach (GridViewRow gvr in Grade.Rows)
            {
                TextBox _txtCodigoItemMenu = (TextBox)gvr.Cells[0].FindControl("txtCodigoItemMenu");
                CheckBox _chkConsultar = (CheckBox)gvr.Cells[2].FindControl("chkConsultar");
                CheckBox _chkIncluir = (CheckBox)gvr.Cells[3].FindControl("chkIncluir");
                CheckBox _chkAlterar = (CheckBox)gvr.Cells[4].FindControl("chkAlterar");
                CheckBox _chkExcluir = (CheckBox)gvr.Cells[5].FindControl("chkExcluir");
                if (_txtCodigoItemMenu.Text != "" && geral.IsNumeric(_txtCodigoItemMenu.Text) &&
                    hifCodigoUsuario.Value != "" && geral.IsNumeric(hifCodigoUsuario.Value))
                {
                    oItensMenuPermissoes.CodigoUsuario = Convert.ToInt32(hifCodigoUsuario.Value);
                    oItensMenuPermissoes.CodigoItensMenu = Convert.ToInt32(_txtCodigoItemMenu.Text);
                    oItensMenuPermissoes.Consultar = bLiberarTirar;
                    oItensMenuPermissoes.Incluir = bLiberarTirar;
                    oItensMenuPermissoes.Alterar = bLiberarTirar;
                    oItensMenuPermissoes.Excluir = 0;
                    if (!oItensMenuPermissoesDados.ExistePermissaoMenu(hifCodigoUsuario.Value, _txtCodigoItemMenu.Text))
                        oItensMenuPermissoesDados.Inserir(oItensMenuPermissoes);
                    else
                        oItensMenuPermissoesDados.Alterar(oItensMenuPermissoes, oItensMenuPermissoes.CodigoItensMenu, oItensMenuPermissoes.CodigoUsuario);
                }
            }
            refreshGrade(oItensMenuPermissoes.CodigoUsuario);
        }

        protected void btnTirarTodas_Click(object sender, EventArgs e)
        {
            TirarLiberarPermissoes(0);
        }
    }
}