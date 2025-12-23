using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibSILC;
using SILCNegocios;
using System.Data.OleDb;

namespace formSILC
{
    public partial class frmRelatorioMTReConferenciaDiaria : Form
    {
        clsLancamentoMTR oLancMTR = new clsLancamentoMTR();
        clsLancamentoMTRDados oLancMTRDados = new clsLancamentoMTRDados();
        clsMTReConferenciaDiaria oMTReConferenciaDiaria = new clsMTReConferenciaDiaria();
        clsMTReConferenciaDiariaDados oMTReConferenciaDiariaDados = new clsMTReConferenciaDiariaDados();
        clsUsuarios oUsuario = new clsUsuarios();
        int regs = 0;
        int pagina = 0;
        DataTable _dtMTReConferenciaDiaria = new DataTable();

        private BindingSource bindingSource = new BindingSource();

        public frmRelatorioMTReConferenciaDiaria()
        {
            InitializeComponent();
        }

        private void btnSelecionarArquivos_Click(object sender, EventArgs e)
        {
            //try
            //{
                //define as propriedades do controle 
                this.ofd1.Multiselect = false;
                this.ofd1.Title = "Selecionar txt";
                ofd1.InitialDirectory = @"C:\Temp";
                ofd1.Filter = "Text files (*.xls)|*.xls";
                ofd1.CheckFileExists = true;
                ofd1.CheckPathExists = true;
                ofd1.FilterIndex = 2;
                ofd1.RestoreDirectory = true;
                ofd1.ReadOnlyChecked = true;
                ofd1.ShowReadOnly = true;
                int i = 0;
                DialogResult ofddr = this.ofd1.ShowDialog();

                if (ofddr == System.Windows.Forms.DialogResult.OK)
                {
                    // Le os arquivos selecionados 
                    textBox1.Text = ofd1.FileName;
                    textBox1.Enabled = false;
                    textBox1.Refresh();

                    _dtMTReConferenciaDiaria = new DataTable();
                    _dtMTReConferenciaDiaria.Columns.Add("NumeroMTRe");
                    _dtMTReConferenciaDiaria.Columns.Add("Data");                    
                    _dtMTReConferenciaDiaria.Columns.Add("CodigoIBAMA");
                    _dtMTReConferenciaDiaria.Columns.Add("Quantidade");
                    _dtMTReConferenciaDiaria.Columns.Add("QtdeUnidade");
                    _dtMTReConferenciaDiaria.Columns.Add("NumeroCDFe");
                    _dtMTReConferenciaDiaria.Columns.Add("CNPJ_CPF_Cliente");
                    _dtMTReConferenciaDiaria.Columns.Add("Obs");
                    _dtMTReConferenciaDiaria.Columns.Add("RazaoSocialCliente");

                    DataRow _dr;
                    string[] lines = System.IO.File.ReadAllLines(ofd1.FileNames[0]);

                    string DataInicio = DateTime.Now.ToShortDateString();

                    i = 0;
                    string con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ofd1.FileName + ";Extended Properties='Excel 8.0;HDR=Yes;'";
                    using (OleDbConnection connection = new OleDbConnection(con))
                    {
                        connection.Open();
                        OleDbCommand command = new OleDbCommand("select * from [rel_mtr_ger_trans_des$]", connection);
                        using (OleDbDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                _dr = _dtMTReConferenciaDiaria.NewRow();
                                _dr[0] = dr[0];
                                _dr[1] = dr[10].ToString().Replace("(*)", " ");
                                _dr[2] = dr[12];
                                _dr[3] = dr[14];
                                _dr[4] = dr[15];
                                _dr[5] = dr[21].ToString().Replace("CDF emitido Nº", "");
                                _dr[8] = dr[5];
                                _dr[6] = dr[6];
                                _dr[7] = dr[9];
                                _dtMTReConferenciaDiaria.Rows.Add(_dr);
                                i++;
                            }
                        }
                    }

                    i = 0;
                    foreach (DataRow dr in _dtMTReConferenciaDiaria.Rows)
                    {
                        dr[2] = dr[2].ToString().Replace("(", " ");
                        dr[2] = dr[2].ToString().Replace(")", " ");
                        dr[2] = dr[2].ToString().Replace("*", " ");
                        if (dr[2].ToString().Split("-"[0]).Count() > 0)
                        {
                            dr[2] = dr[2].ToString().Split("-"[0])[0].ToString();
                        }
                    }

                    foreach (DataRow dr in _dtMTReConferenciaDiaria.Rows)
                    {
                        if (dr["NumeroMTRe"].ToString() != "" && dr["CodigoIBAMA"].ToString() != "")
                        {                            
                            if (dr["NumeroMTRe"].ToString() != "")
                            {
                                oMTReConferenciaDiariaDados = new clsMTReConferenciaDiariaDados();
                                oMTReConferenciaDiaria.NumeroMTRe = Convert.ToInt64(dr["NumeroMTRe"]);
                                oMTReConferenciaDiariaDados.Excluir(oMTReConferenciaDiaria.NumeroMTRe);
                                oMTReConferenciaDiaria.Quantidade = 0;
                                oMTReConferenciaDiaria.QtdeUnidade = 0;
                                if (dr["Quantidade"].ToString() != "")
                                    oMTReConferenciaDiaria.Quantidade = Convert.ToDecimal(dr["Quantidade"]);
                                if (dr["QtdeUnidade"].ToString() != "")
                                    oMTReConferenciaDiaria.QtdeUnidade = Convert.ToDecimal(dr["QtdeUnidade"]);
                                oMTReConferenciaDiaria.NumeroCDFe = 0;
                                if (dr["NumeroCDFe"].ToString() != "")
                                    oMTReConferenciaDiaria.NumeroCDFe = Convert.ToInt64(dr["NumeroCDFe"]);
                                if (dr["Data"].ToString() != "")
                                    oMTReConferenciaDiaria.Data = Convert.ToDateTime(dr["Data"]);
                                oMTReConferenciaDiaria.CodigoIBAMA = "";
                                if (dr["CodigoIBAMA"].ToString() != "")
                                    oMTReConferenciaDiaria.CodigoIBAMA = dr["CodigoIBAMA"].ToString().Substring(0, 2) + " " + dr["CodigoIBAMA"].ToString().Substring(2, 2) + " " + dr["CodigoIBAMA"].ToString().Substring(4, 2);                                                                
                                
                                if (oMTReConferenciaDiaria.Quantidade == 0)
                                    if (oMTReConferenciaDiaria.QtdeUnidade > 0)
                                        oMTReConferenciaDiaria.Quantidade = Math.Round(oMTReConferenciaDiaria.QtdeUnidade / 1000, 3);

                                oMTReConferenciaDiaria.CNPJ_CPF_Cliente = dr["CNPJ_CPF_Cliente"].ToString();
                                if (oMTReConferenciaDiaria.CNPJ_CPF_Cliente.Length >= 14)
                                {
                                    oMTReConferenciaDiaria.CNPJ_CPF_Cliente = geral.Left(oMTReConferenciaDiaria.CNPJ_CPF_Cliente, 2) + "." +
                                                                              oMTReConferenciaDiaria.CNPJ_CPF_Cliente.Substring(2, 3) + "." +
                                                                              oMTReConferenciaDiaria.CNPJ_CPF_Cliente.Substring(5, 3) + "/" +
                                                                              oMTReConferenciaDiaria.CNPJ_CPF_Cliente.Substring(8, 4) + "-" +
                                                                              oMTReConferenciaDiaria.CNPJ_CPF_Cliente.Substring(12, 2);
                                }
                                else
                                {
                                    oMTReConferenciaDiaria.CNPJ_CPF_Cliente = geral.Left(oMTReConferenciaDiaria.CNPJ_CPF_Cliente, 3) + "." +
                                                                              oMTReConferenciaDiaria.CNPJ_CPF_Cliente.Substring(3, 3) + "." +
                                                                              oMTReConferenciaDiaria.CNPJ_CPF_Cliente.Substring(6, 3) + "-" +
                                                                              oMTReConferenciaDiaria.CNPJ_CPF_Cliente.Substring(9, 2);
                                }
                                oMTReConferenciaDiaria.Obs = dr["Obs"].ToString();
                                //oLancMTRDados = new clsLancamentoMTRDados();                                
                                //oMTReConferenciaDiaria.Obs = oLancMTRDados.MTReNaoExisteOuCNPJ_Diferente(oMTReConferenciaDiaria.NumeroMTRe, oMTReConferenciaDiaria.CNPJ_CPF_Cliente);
                                //dr["Obs"] = oMTReConferenciaDiaria.Obs;
                                //if (oMTReConferenciaDiariaDados.DadoExiste(Convert.ToInt64(dr["NumeroMTRe"]), oMTReConferenciaDiaria.CodigoIBAMA) == "Alterar")
                                //{
                                //    oMTReConferenciaDiariaDados.Alterar(oMTReConferenciaDiaria, oMTReConferenciaDiaria.NumeroMTRe);
                                //}
                                //else
                                //{
                                //    oMTReConferenciaDiariaDados.Inserir(oMTReConferenciaDiaria);
                                //}
                                oMTReConferenciaDiariaDados.Inserir(oMTReConferenciaDiaria);
                            }
                            i++;
                            lblMensagem.Text = "lido: " + i.ToString() + " registros";
                            lblMensagem.Refresh();
                        }

                    }
                    lblLadoSILC.ForeColor = Color.IndianRed;
                    lblMensagem.Text = "Importação realizada com sucesso! " + _dtMTReConferenciaDiaria.Rows.Count.ToString("000000") + " registros lidos";
                    GradeCDFe.AutoGenerateColumns = false;

                    string DataFim = "";
                    if (_dtMTReConferenciaDiaria.Rows[1]["Data"].ToString() != "")
                    {
                        oMTReConferenciaDiaria.Data = Convert.ToDateTime(_dtMTReConferenciaDiaria.Rows[1]["Data"]);
                        DataInicio = oMTReConferenciaDiaria.Data.ToShortDateString();
                        DataFim = DataInicio; 
                    }

                    GradeCDFe.AutoGenerateColumns = false;
                    _dtMTReConferenciaDiaria = oMTReConferenciaDiariaDados.PreencheDataTablePeriodo("NumeroMTRe", DataInicio, DataFim);
                    _dtMTReConferenciaDiaria.Columns.Add("CNPJ_CPF_Cliente");
                    _dtMTReConferenciaDiaria.Columns.Add("NumeroMTRe");
                    _dtMTReConferenciaDiaria.Columns.Add("CodigoIbama");
                    _dtMTReConferenciaDiaria.Columns.Add("Qtde");
                    _dtMTReConferenciaDiaria.Columns.Add("Data");

                    bindingSource.DataSource = _dtMTReConferenciaDiaria;
                    GradeCDFe.DataSource = bindingSource;

                    clsLancamentos oLanc = new clsLancamentos();
                    clsLancamentosDados oLancDados = new clsLancamentosDados();
                    clsClientes oCliente = new clsClientes();
                    clsClienteDados oClienteDados = new clsClienteDados();
                    clsResiduos oResiduo = new clsResiduos();
                    clsResiduoDados oResiduoDados = new clsResiduoDados();
                    clsIBAMA oIBAMA = new clsIBAMA();
                    clsIBAMADados oIBAMAdados = new clsIBAMADados();
                    clsCacambaDados oCaixaDados = new clsCacambaDados();
                    clsCacambas oCaixa = new clsCacambas();
                    foreach (DataRow dr in _dtMTReConferenciaDiaria.Rows)
                    {
                        oLanc = new clsLancamentos();
                        oLancDados = new clsLancamentosDados();
                        oCliente = new clsClientes();
                        oClienteDados = new clsClienteDados();
                        oResiduo = new clsResiduos();
                        oResiduoDados = new clsResiduoDados();
                        oIBAMA = new clsIBAMA();
                        oIBAMAdados = new clsIBAMADados();
                        oCaixaDados = new clsCacambaDados();
                        oCaixa = new clsCacambas();
                        dr["NumeroMTRe"] = 0;
                        dr["CNPJ_CPF_Cliente"] = "";
                        dr["CodigoIbama"] = "";
                        dr["Data"] = "";
                        oLancMTR = oLancMTRDados.PegaDados(Convert.ToInt64(dr["NumeroMTRe_IMA"]), dr["CodigoIbamaIMA"].ToString());
                        // procurar no SILC Dados da MTRe
                        if (oLancMTR.NumeroLancamento > 0)
                        {
                            oLanc = oLancDados.PegaDados(oLanc, oLancMTR.NumeroLancamento);
                            if (oLanc.CodigoCliente > 0)
                            {
                                oCliente = oClienteDados.PegaDados(oCliente, oLanc.CodigoCliente);
                                dr["CNPJ_CPF_Cliente"] = oCliente.CNPJ_CPF;
                            }
                            dr["NumeroMTRe"] = oLancMTR.NumeroMTRFatima;
                            oResiduo = oResiduoDados.PegaDados(oResiduo, oLancMTR.CodigoResiduo);
                            oIBAMA = oIBAMAdados.PegaDados(oIBAMA, oResiduo.CodigoIBAMA);
                            dr["CodigoIbama"] = oIBAMA.CodigoIBAMA;
                            if (dr["Qtde"].ToString() == "")
                                dr["Qtde"] = "0";
                            if (oLancMTR.Unidade == "CX")
                            {
                                oCaixa = oCaixaDados.PegaDados(oCaixa, oLanc.NumeroCaixa);
                                if (oCaixa.Capacidade > 0)
                                    dr["Qtde"] = Convert.ToDecimal(dr["Qtde"]) + Math.Round(oLancMTR.Quantidade * oCaixa.Capacidade * oResiduo.M3PorTon, 2);
                            }
                            else if (oLancMTR.Unidade.ToUpper() == "M3")
                            {
                                dr["Qtde"] = Math.Round(Convert.ToDecimal(dr["Qtde"]) * oResiduo.M3PorTon, 2);
                            }
                            else
                            {
                                if (oLancMTR.Quantidade > 0)
                                    oLancMTR.Quantidade = Math.Round(oLancMTR.Quantidade, 2);
                                dr["Qtde"] = Math.Round( oLancMTR.Quantidade / 1000, 2);
                            }
                            dr["Data"] = oLanc.DataRetirada;
                        }
                        string sObs = "";
                        if (dr["Obs"].ToString().ToUpper() == "CANCELADO" || dr["Obs"].ToString().ToUpper() == "CANCELADA")
                            sObs = sObs + "CANCELADO";
                        else
                        {
                            if (dr["NumeroMTRe_IMA"].ToString() != dr["NumeroMTRe"].ToString())
                                sObs = sObs + " (NrMTR!=)";
                            if (dr["CNPJ_Cliente_IMA"].ToString() != geral.RetiraLetras(dr["CNPJ_CPF_Cliente"].ToString()))
                                sObs = sObs + " (CNPJ!=)";
                            if (dr["CodigoIbamaIMA"].ToString() != dr["CodigoIbama"].ToString())
                                sObs = sObs + " (CodigoIbama!=)";
                            if (dr["Qtde_IMA"].ToString() == "")
                                dr["Qtde_IMA"] = "0";
                            if (dr["Qtde"].ToString() == "")
                                dr["Qtde"] = "0";
                            if (Convert.ToDecimal(dr["Qtde_IMA"]) != Convert.ToDecimal(dr["Qtde"]))
                                sObs = sObs + " (Qtde!=)";
                            if (Convert.ToDateTime(dr["Data_IMA"]).ToString("dd/MM/yyyy") != dr["Data"].ToString())
                                sObs = sObs + " (Data!=)";
                        }
                        dr["Obs"] = sObs;
                        GradeCDFe.Refresh();
                    }
                    foreach (DataRow dr in oLancMTRDados.PegaDados(oLancMTR, 0, false, DataInicio).Rows)
                    {
                        oLanc = new clsLancamentos();
                        oLancDados = new clsLancamentosDados();
                        oCliente = new clsClientes();
                        oClienteDados = new clsClienteDados();
                        oResiduo = new clsResiduos();
                        oResiduoDados = new clsResiduoDados();
                        oIBAMA = new clsIBAMA();
                        oIBAMAdados = new clsIBAMADados();

                        if (dr["NumeroMTRFatima"].ToString() == "")
                            dr["NumeroMTRFatima"] = "0";
                        DataRow[] dtr = _dtMTReConferenciaDiaria.Select("NumeroMTRe = " + dr["NumeroMTRFatima"].ToString());
                        if (dr["NumeroMTRFatima"].ToString() == "0" || dr["NumeroMTRFatima"].ToString() == "")
                        {

                            // procurar no SILC Dados da MTRe
                            oLancMTR.NumeroLancamento = Convert.ToInt32(dr["NumeroLancamento"]);
                            oLancMTR.CodigoResiduo = Convert.ToInt32(dr["CodigoResiduo"]);
                            oLancMTR.Quantidade = Convert.ToInt32(dr["Quantidade"]);
                            if (oLancMTR.NumeroLancamento > 0)
                            {
                                oLanc = oLancDados.PegaDados(oLanc, oLancMTR.NumeroLancamento);
                                if (oLanc.CodigoCliente > 0)
                                {
                                    oCliente = oClienteDados.PegaDados(oCliente, oLanc.CodigoCliente);
                                }
                                oResiduo = oResiduoDados.PegaDados(oResiduo, oLancMTR.CodigoResiduo);
                                oIBAMA = oIBAMAdados.PegaDados(oIBAMA, oResiduo.CodigoIBAMA);

                                DataRow _nDr = _dtMTReConferenciaDiaria.NewRow();
                                if (oLancMTR.Quantidade > 0)
                                    _nDr["Qtde"] = Math.Round(oLancMTR.Quantidade / 1000, 2);
                                if (oLancMTR.Unidade == "CX")
                                {
                                    oCaixa = oCaixaDados.PegaDados(oCaixa, oLanc.NumeroCaixa);
                                    if (oCaixa.Capacidade > 0)
                                        _nDr["Qtde"] = Convert.ToDecimal(_nDr["Qtde"]) + Math.Round(oLancMTR.Quantidade * oCaixa.Capacidade * oResiduo.M3PorTon, 2);
                                }
                                else if (oLancMTR.Unidade.ToUpper() == "M3")
                                {
                                    _nDr["Qtde"] = Math.Round(Convert.ToDecimal(_nDr["Qtde"]) * oResiduo.M3PorTon, 2);
                                }
                                else
                                {
                                    if (oLancMTR.Quantidade > 0)
                                        oLancMTR.Quantidade = Math.Round(oLancMTR.Quantidade, 2);
                                    _nDr["Qtde"] = Math.Round(oLancMTR.Quantidade / 1000, 3);
                                }
                                _nDr["CNPJ_CPF_Cliente"] = oCliente.CNPJ_CPF;
                                _nDr["NumeroMTRe"] = dr["NumeroMTRFatima"];
                                _nDr["CodigoIbama"] = oIBAMA.CodigoIBAMA;
                                _nDr["Data"] = oLanc.DataRetirada;
                                _nDr["Obs"] = oCliente.NomeFantasia + " - " + oResiduo.DescricaoReduzida;
                                _dtMTReConferenciaDiaria.Rows.Add(_nDr);

                            }
                            GradeCDFe.Refresh();
                        }
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //    lblMensagem.Text = ex.Message;
            //}
        }
        
        private void Imprimir()
        {
            regs = 0;
            pagina = 0;
            ((Form)ppd).StartPosition = FormStartPosition.CenterScreen;
            ((Form)ppd).Text = "Visualizador";
            ((Form)ppd).WindowState = FormWindowState.Maximized;
            ppd.Document = pd;
            ppd.ShowDialog();
        }

        private void pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float cl = 10.0F;
            float ln = 12.0F;
            Font ft = new Font("Courier New", 9);
            e.Graphics.DrawString(geral.NomeSistema, ft, Brushes.Black, cl, ln);

            ln = ln + 12.0F;
            e.Graphics.DrawString("Relatório de Número de MTRe incorreta", ft, Brushes.Black, cl, ln);
            e.Graphics.DrawString("Emissão: " + DateTime.Now.ToShortDateString(), ft, Brushes.Black, 520, ln);

            ln = ln + 20.0F;
            ft = new Font("Courier New", 8);
            e.Graphics.DrawString("Número MTRe", ft, Brushes.Black, cl, ln);
            cl = cl + 100;
            e.Graphics.DrawString("CNPJ/CPF Cliente", ft, Brushes.Black, cl, ln);
            cl = cl + 240;
            e.Graphics.DrawString("Observação", ft, Brushes.Black, cl, ln);

            ln = ln + 12.0F;
            string str = "";
            char pad = '─';
            cl = 10.0F;
            e.Graphics.DrawString(str.PadLeft(110, pad), ft, Brushes.Black, cl, ln);
            StringFormat alinhaDireita = new StringFormat();
            alinhaDireita.Alignment = StringAlignment.Far;

            ft = new Font("Courier New", 8);
            for (int i = regs; i <= GradeCDFe.Rows.Count - 1; i++)
            {
                regs++;
                if (ln < 1000)
                {                    
                    ln = ln + 12F;
                    cl = 10.0F;
                    e.Graphics.DrawString(GradeCDFe.Rows[i].Cells[0].Value.ToString(), ft, Brushes.Black, cl, ln);
                    cl = cl + 100;
                    e.Graphics.DrawString(geral.Left(GradeCDFe.Rows[i].Cells[1].Value.ToString(), 20), ft, Brushes.Black, cl, ln);
                    cl = cl + 240;
                    e.Graphics.DrawString(GradeCDFe.Rows[i].Cells[2].Value.ToString(), ft, Brushes.Black, cl, ln);
                }
                else
                {
                    ln = 20F;
                    cl = 385;
                    ln = ln + 12;
                    pagina++;
                    e.Graphics.DrawString(str.PadLeft(110, pad), ft, Brushes.Black, 10, 1000);
                    e.Graphics.DrawString("Pagina: " + pagina, ft, Brushes.Black, 695, 1000 + 10);
                    break;
                }
            }
            if (regs >= (GradeCDFe.Rows.Count))
            {
                if (ln > 1000)
                {
                    ln = ln + 20;
                    pagina++;
                    e.Graphics.DrawString(str.PadLeft(110, pad), ft, Brushes.Black, 10, 1000);
                    e.Graphics.DrawString("Pagina: " + pagina, ft, Brushes.Black, 695, 1000 + 10);
                }
                e.HasMorePages = false;
                regs++;
            }
            else
            {
                e.HasMorePages = true;
            }

        }

        /*        
        private void button1_Click(object sender, EventArgs e)
        {
            if (GradeCDFe.Rows.Count > 1)
                Imprimir();
            else
                MessageBox.Show("Não há dados!");
        }
        */

        private void GradeCDFe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ppd_Load(object sender, EventArgs e)
        {
            Imprimir();
        }
    }
}
