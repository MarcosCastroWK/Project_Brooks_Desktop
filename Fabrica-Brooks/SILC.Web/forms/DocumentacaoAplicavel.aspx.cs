using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibSILC;
using SILCNegocios;
using System.Data;
using System.Drawing;

namespace SILC.Web.forms
{
    public partial class forms_DocumentacaoAplicavel : System.Web.UI.Page
    {
        DataTable _dt = new DataTable();
        clsUsuarios oUsuario = new clsUsuarios();
        clsClientes oCliente = new clsClientes();
        clsClienteDados oClienteDados = new clsClienteDados();
        clsDocumentacaoAplicavel oDocAplic = new clsDocumentacaoAplicavel();
        clsDocumentacaoAplicavelDados oDocAplicDados = new clsDocumentacaoAplicavelDados();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    oUsuario = (clsUsuarios)Session["oUsuario"];
                    if (oUsuario == null)
                    {
                        Response.Redirect("brooks/loginaplicativo.aspx", true);
                    }
                    else
                    {
                        if (oUsuario.Aplicativo == true)
                            menu.Visible = false;
                        else
                            menu.Visible = true;
                        string rtCC = Request.QueryString["CodigoCliente"];
                        if (rtCC != "")
                        {
                            oDocAplic.CodigoCliente = Convert.ToInt32(rtCC);
                            intCodigoCliente.Valor = rtCC;
                            _dt = oDocAplicDados.PreencheDataTable("CodigoCliente", 0, 2000, true, oDocAplic.CodigoCliente);
                            if (_dt.Rows.Count > 0)
                            {
                                if (_dt.Rows[0]["AguardarAprovacaoPlanFat"].ToString() != "")
                                    oDocAplic.AguardarAprovacaoPlanFat = Convert.ToInt16(_dt.Rows[0]["AguardarAprovacaoPlanFat"].ToString());
                                if (_dt.Rows[0]["AguardarOrdemCompra"].ToString() != "")
                                    oDocAplic.AguardarOrdemCompra = Convert.ToInt16(_dt.Rows[0]["AguardarOrdemCompra"].ToString());
                                if (_dt.Rows[0]["ConferirDDRAteDia"].ToString() != "")
                                    oDocAplic.ConferirDDRAteDia = Convert.ToInt16(_dt.Rows[0]["ConferirDDRAteDia"].ToString());
                                if (_dt.Rows[0]["EnviarCDFBrooks"].ToString() != "")
                                    oDocAplic.EnviarCDFBrooks = Convert.ToInt16(_dt.Rows[0]["EnviarCDFBrooks"].ToString());
                                if (_dt.Rows[0]["EnviarPlanFatAteDia"].ToString() != "")
                                    oDocAplic.EnviarPlanFatAteDia = Convert.ToInt16(_dt.Rows[0]["EnviarPlanFatAteDia"].ToString());
                                if (_dt.Rows[0]["EnviarRelGer"].ToString() != "")
                                    oDocAplic.EnviarRelGer = Convert.ToInt16(_dt.Rows[0]["EnviarRelGer"].ToString());
                                if (_dt.Rows[0]["EnviarRGRAteDia"].ToString() != "")
                                    oDocAplic.EnviarRGRAteDia = Convert.ToInt16(_dt.Rows[0]["EnviarRGRAteDia"].ToString());
                                oDocAplic.Mes = 0;
                                oDocAplic.Ano = 2000;
                                oDocAplic.PeriodoApuracao = _dt.Rows[0]["PeriodoApuracao"].ToString();

                                txtCliente.Text = _dt.Rows[0]["NomeFantasia"].ToString();

                                intAguardarAprovacaoPlanFat.Valor = oDocAplic.AguardarAprovacaoPlanFat.ToString();
                                intAguardarOrdemCompra.Valor = oDocAplic.AguardarOrdemCompra.ToString();
                                intConferirDDRAteDia.Valor = oDocAplic.ConferirDDRAteDia.ToString();
                                intEnviarPlanFatAteDia.Valor = oDocAplic.EnviarPlanFatAteDia.ToString();
                                intEnviarRGRAteDia.Valor = oDocAplic.EnviarRGRAteDia.ToString();
                                txtPeriodo.Text = oDocAplic.PeriodoApuracao;
                            }
                            else
                            {
                                lblMensagem.Text = "Não há informações!";
                                txtCliente.Text = oClienteDados.PegaNomeFantasiaCodigo(Convert.ToInt32(rtCC.ToString())).Rows[0][0].ToString();
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    lblMensagem.Text = ex.Message;
                }
            }

        }
        protected void Salvar_Click(object sender, EventArgs e)
        {
            // pegar dados dos controles do form textbox - checkbox

            lblMensagem.Text = "";
            oDocAplic = new clsDocumentacaoAplicavel();

            if (intAguardarAprovacaoPlanFat.Valor != "")
            {
                oDocAplic.AguardarAprovacaoPlanFat = Convert.ToInt16(intAguardarAprovacaoPlanFat.Valor);
                if (oDocAplic.AguardarAprovacaoPlanFat > 31)
                {
                    lblMensagem.Text = "Dia para Aguardar Aprovação PlanFat inválido!";
                    intAguardarAprovacaoPlanFat.Valor = "31";
                    oDocAplic.AguardarAprovacaoPlanFat = 31;
                }
            }
            if (intAguardarOrdemCompra.Valor != "")
            {
                oDocAplic.AguardarOrdemCompra = Convert.ToInt16(intAguardarOrdemCompra.Valor);
                if (oDocAplic.AguardarOrdemCompra > 31)
                {
                    lblMensagem.Text = "Dia para Aguardar Ordem Compra inválida!";
                    oDocAplic.AguardarOrdemCompra = 31;
                    intAguardarOrdemCompra.Valor = "31";
                }
            }

            if (intConferirDDRAteDia.Valor != "")
            {
                oDocAplic.ConferirDDRAteDia = Convert.ToInt16(intConferirDDRAteDia.Valor);
                if (oDocAplic.ConferirDDRAteDia > 31)
                {
                    lblMensagem.Text = "Dia para Conferir DDR inválida!";
                    oDocAplic.ConferirDDRAteDia = 31;
                    intConferirDDRAteDia.Valor = "31";
                }
            }

            if (intEnviarPlanFatAteDia.Valor != "")
            {
                oDocAplic.EnviarPlanFatAteDia = Convert.ToInt16(intEnviarPlanFatAteDia.Valor);
                if (oDocAplic.EnviarPlanFatAteDia > 31)
                {
                    lblMensagem.Text = "Dia para Enviar PlanFat inválido!";
                    oDocAplic.EnviarPlanFatAteDia = 31;
                    intEnviarPlanFatAteDia.Valor = "31";
                }
            }

            oDocAplic.EnviarRGRAteDia = 0;
            if (intEnviarRGRAteDia.Valor != "")
            {
                oDocAplic.EnviarRGRAteDia = Convert.ToInt16(intEnviarRGRAteDia.Valor);
                if (oDocAplic.EnviarRGRAteDia > 31)
                {
                    lblMensagem.Text = "Dia para Enviar RGR inválido!";
                    oDocAplic.EnviarRGRAteDia = 31;
                    intEnviarRGRAteDia.Valor = "31";
                }
            }
            oDocAplic.PeriodoApuracao = txtPeriodo.Text;

            if (lblMensagem.Text == "")
            {
                oDocAplic.CodigoCliente = Convert.ToInt32(intCodigoCliente.Valor);
                if (oDocAplic.Ano == 0)
                    oDocAplic.Ano = 2000;
                if (oDocAplicDados.DadoExiste(oDocAplic.CodigoCliente, 0, 2000) == "Alterar")
                {
                    oDocAplicDados.Alterar(oDocAplic);
                    lblMensagem.Text = "Documentação alterada com sucesso!";
                }
                else
                {
                    oDocAplicDados.Inserir(oDocAplic);
                    lblMensagem.Text = "Documentação incluída com sucesso!";
                }
            }

        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>window.close();</script>");
        }
        private void LimpaCampos()
        {
            intAguardarAprovacaoPlanFat.Valor = "";
            intAguardarOrdemCompra.Valor = "";
            intConferirDDRAteDia.Valor = "";
            intEnviarPlanFatAteDia.Valor = "";
            intEnviarRGRAteDia.Valor = "";
            txtPeriodo.Text = "01/30";
        }
    }
}