using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LibSILC;
using SILCNegocios;

namespace SILC.Web.forms
{
    public partial class forms_CDFe_Importacao : System.Web.UI.Page
    {
        clsLancamentoMTR oLancMTR = new clsLancamentoMTR();
        clsLancamentoMTRDados oLancMTRDados = new clsLancamentoMTRDados();
        clsCDFe oCDFe = new clsCDFe();
        clsCDFeDados oCDFeDados = new clsCDFeDados();
        clsUsuarios oUsuario = new clsUsuarios();

        DataTable _dtCDFe = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                oUsuario = (clsUsuarios)Session["oUsuario"];
                if (oUsuario == null)
                {
                    Response.Redirect("brooks/loginaplicativo.aspx", true);
                }
                else
                {
                    lblMensagem.Text = "";
                    lblMensagem.Font.Size = 12;
                    lblCaminho.Text = "@c:\\temp\\";
                }
            }
        }
        private void LerArquivoTxt(string pNomeArquivo)
        {
            try
            {
                if (System.IO.File.Exists("c:\\Temp\\" + pNomeArquivo))
                {
                    string[] lines = System.IO.File.ReadAllLines("c:\\Temp\\" + pNomeArquivo);
                    _dtCDFe = new DataTable();
                    _dtCDFe.Columns.Add("NumeroMTRe");
                    _dtCDFe.Columns.Add("Data");
                    _dtCDFe.Columns.Add("CodigoIBAMA");
                    _dtCDFe.Columns.Add("QtdeUnidade");
                    _dtCDFe.Columns.Add("Quantidade");
                    _dtCDFe.Columns.Add("NumeroCDFe");
                    DataRow _dr;

                    foreach (string line in lines)
                    {
                        if (line.Split("\t"[0]).Length >= 4)
                        {
                            if (line.Split("\t"[0])[6].ToString() != "")
                            {
                                _dr = _dtCDFe.NewRow();
                                _dr[0] = line.Split("\t"[0])[0];
                                _dr[1] = line.Split("\t"[0])[2].ToString().Replace("(*)", " ");
                                _dr[2] = line.Split("\t"[0])[3];
                                _dr[3] = line.Split("\t"[0])[4];
                                _dr[4] = line.Split("\t"[0])[5];
                                _dr[5] = line.Split("\t"[0])[6].ToString().Replace("CDF emitido Nº", "");
                                _dtCDFe.Rows.Add(_dr);
                            }
                        }
                    }
                    foreach (DataRow dr in _dtCDFe.Rows)
                    {
                        dr[2] = dr[2].ToString().Replace("(", " ");
                        dr[2] = dr[2].ToString().Replace(")", " ");
                        dr[2] = dr[2].ToString().Replace("*", " ");
                        if (dr[2].ToString().Split("-"[0]).Count() > 0)
                        {
                            dr[2] = dr[2].ToString().Split("-"[0])[0].ToString();
                        }
                    }

                    foreach (DataRow dr in _dtCDFe.Rows)
                    {
                        if (dr["NumeroMTRe"].ToString() != "" && dr["CodigoIBAMA"].ToString() != "")
                        {
                            oLancMTRDados = new clsLancamentoMTRDados();
                            if (oLancMTRDados.MTReExiste(Convert.ToInt64(dr["NumeroMTRe"]), dr["CodigoIBAMA"].ToString(), Convert.ToDateTime(dr["Data"])))
                            {
                                oCDFeDados = new clsCDFeDados();
                                if (oCDFeDados.DadoExiste(Convert.ToInt64(dr["NumeroMTRe"])) == "Incluir")
                                {
                                    oCDFe.NumeroMTRe = 0;
                                    if (dr["NumeroMTRe"].ToString() != "")
                                        oCDFe.NumeroMTRe = Convert.ToInt64(dr["NumeroMTRe"]);
                                    oCDFe.Quantidade = 0;
                                    if (dr["Quantidade"].ToString() != "")
                                        oCDFe.Quantidade = Convert.ToDecimal(dr["Quantidade"]);
                                    oCDFe.NumeroCDFe = 0;
                                    if (dr["NumeroCDFe"].ToString() != "")
                                        oCDFe.NumeroCDFe = Convert.ToInt64(dr["NumeroCDFe"]);
                                    if (dr["Data"].ToString() != "")
                                        oCDFe.Data = Convert.ToDateTime(dr["Data"]);
                                    oCDFe.CodigoIBAMA = "";
                                    if (dr["CodigoIBAMA"].ToString() != "")
                                        oCDFe.CodigoIBAMA = dr["CodigoIBAMA"].ToString().Substring(0, 2) + " " + dr["CodigoIBAMA"].ToString().Substring(2, 2) + " " + dr["CodigoIBAMA"].ToString().Substring(4, 2);
                                    oCDFe.QtdeUnidade = 0;
                                    if (dr["QtdeUnidade"].ToString() != "")
                                        oCDFe.QtdeUnidade = Convert.ToDecimal(dr["QtdeUnidade"]);

                                    oCDFeDados.Inserir(oCDFe);
                                }
                            }
                        }
                    }
                    lblMensagem.Text = "Importação realizada com sucesso! " + _dtCDFe.Rows.Count.ToString("000000") + " registros importados";
                }
                else if (pNomeArquivo.Length == 0)
                    lblMensagem.Text = "Arquivo de importação inválido!";
                else
                    lblMensagem.Text = "Não foi possível fazer importação. Arquivo do caminho: " + "/" + pNomeArquivo + " inexistente ";
            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }
        protected void btnImportar_Click(object sender, EventArgs e)
        {
            LerArquivoTxt(FileUpload1.FileName);
            GradeCDFe.DataSource = _dtCDFe;
            GradeCDFe.DataBind();
        }
    }
}