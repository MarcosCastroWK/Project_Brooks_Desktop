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
    public partial class frmCDFeImportacao : Form
    {
        clsClientes oCliente = new clsClientes();
        clsClienteDados oClienteDados = new clsClienteDados();
        clsLancamentoMTR oLancMTR = new clsLancamentoMTR();
        clsLancamentoMTRDados oLancMTRDados = new clsLancamentoMTRDados();
        clsCDFe oCDFe = new clsCDFe();
        clsCDFeDados oCDFeDados = new clsCDFeDados();
        clsUsuarios oUsuario = new clsUsuarios();
        int regs = 0;
        int pagina = 0;
        DataTable _dtCDFe = new DataTable();
        DataTable _dtInconsistencias = new DataTable();

        private BindingSource bindingSource = new BindingSource();
        private BindingSource bindingSourceInconsistencias = new BindingSource();

        public frmCDFeImportacao()
        {            
            InitializeComponent();
        }

        private void btnSelecionarArquivos_Click(object sender, EventArgs e)
        {
            if (ctlCliente.txtCodigo.Text == "" && destinofinal1.txtCodigo.Text == "")
                MessageBox.Show("Código ou Cliente e Destino Final inválidos!");
            else if (intMes.VALOR.Text == "")
                MessageBox.Show("Mês inválido!");
            else if (intAno.VALOR.Text == "")
                MessageBox.Show("Ano inválido!");
            else
                //try
                //{
                    if (ctlCliente.txtCodigo.Text != "")
                    {
                        oCliente = oClienteDados.PegaDados(oCliente, Convert.ToInt32(ctlCliente.txtCodigo.Text));
                        oCliente.CNPJ_CPF = geral.RetiraLetras(oCliente.CNPJ_CPF);
                        oCliente.CNPJ_CPF = geral.RetiraCharsCNPJCPF(oCliente.CNPJ_CPF);
                    }
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

                        _dtCDFe = new DataTable();
                        _dtCDFe.Columns.Add("NumeroMTRe");
                        _dtCDFe.Columns.Add("Data");
                        _dtCDFe.Columns.Add("CodigoIBAMA", Type.GetType("System.String"));
                        _dtCDFe.Columns.Add("QtdeUnidade");
                        _dtCDFe.Columns.Add("Quantidade");
                        _dtCDFe.Columns.Add("NumeroCDFe");
                        _dtCDFe.Columns.Add("CNPJ_CPF_Cliente");
                        _dtCDFe.Columns.Add("Situacao");
                        _dtCDFe.Columns.Add("Placas");

                        _dtInconsistencias = new DataTable();
                        _dtInconsistencias.Columns.Add("NumeroMTRe");
                        _dtInconsistencias.Columns.Add("CNPJ_CPF_Cliente");
                        _dtInconsistencias.Columns.Add("Obs");

                        DataRow _dr;
                        string[] lines = System.IO.File.ReadAllLines(ofd1.FileNames[0]);

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
                                    double _qt = 0;
                                    if (dr[15].ToString() != "")
                                        _qt = Convert.ToDouble(dr[15].ToString());
                                    if (dr[16].ToString() != "")
                                        _qt = Convert.ToDouble(dr[16].ToString());
                                    if (_qt > 0)
                                    {
                                        _dr = _dtCDFe.NewRow();
                                        _dr[0] = dr[0].ToString();                      // numero mtre
                                        _dr[1] = dr[10].ToString().Replace("(*)", " "); // data de emissão
                                        string _cdIbamaExcel = dr[13].ToString();
                                        if (geral.Left(_cdIbamaExcel.ToUpper(), 5) == "GRUPO")
                                        {
                                            if ("ABCDEFGHIJKLMNOPQRSTUWVXYZ".IndexOf(geral.Left(_cdIbamaExcel.ToUpper(), 7).Substring(6, 1)) > -1)
                                            {
                                                clsIBAMADados oIbamaDados = new clsIBAMADados();
                                                string cdIbama = oIbamaDados.PegaCodigoIbama(geral.Left(_cdIbamaExcel.ToUpper(), 8).Substring(6, 2).Replace(" ", ""));
                                                if (cdIbama != "")
                                                    _dr[2] = cdIbama.Replace(" ", "");
                                                else
                                                    _dr[2] = "180101";
                                            }
                                            else
                                                _dr[2] = "180101";
                                        }
                                        else
                                            _dr[2] = geral.Left(_cdIbamaExcel, 10);
                                        _dr[3] = dr[15].ToString(); // quantidade por tonelada
                                        _dr[4] = dr[16].ToString(); // quantidade
                                        if (dr[22].ToString() != "")
                                            _dr[5] = dr[22].ToString().Replace("CDF emitido Nº", "");
                                        _dr[6] = dr[6];
                                        _dr[7] = dr[9]; // situacao
                                        _dr[8] = dr[8].ToString().Replace(" ", ""); // Placas
                                        _dtCDFe.Rows.Add(_dr);
                                    }
                                }
                            }
                        }

                        i = 0;
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
                        string sMes = "00";
                        bool bFaz = true;
                        if (_dtCDFe.Rows.Count > 0)
                        {
                            sMes = Convert.ToDateTime(_dtCDFe.Rows[0]["Data"]).ToString("MM");
                            if (_dtCDFe.Rows[0]["CNPJ_CPF_Cliente"].ToString() != oCliente.CNPJ_CPF)
                            {
                                // conferir se é o mesmo cliente
                                if (ctlCliente.txtCodigo.Text != "")
                                {
                                    bFaz = false;
                                    MessageBox.Show("CNPJ do arquivo não confere com o do cliente!");
                                }
                            }
                            if (sMes.Replace("0", "") != intMes.VALOR.Text.Replace("0", ""))
                            {
                                bFaz = false;
                                MessageBox.Show("Mês do arquivo inválido!");
                            }
                            else if (Convert.ToInt16(Convert.ToDateTime(_dtCDFe.Rows[0]["Data"]).ToString("yy")) != Convert.ToInt32(intAno.VALOR.Text))
                            {
                                bFaz = false;
                                MessageBox.Show("Ano do arquivo inválido!");
                            }
                        }
                        else if (_dtCDFe.Rows.Count == 0)
                        {
                            bFaz = false;
                        }

                        if (bFaz)
                        {
                            // antes de incluir apagar todas as CDFe - quando do código do IBAMA não confere com o da B
                            oCDFeDados.ConectaBanco(); // conecta banco   
                            int _iCount = 0;
                            foreach (DataRow dr in _dtCDFe.Rows)
                            {
                                if (dr["NumeroMTRe"].ToString() != "")
                                {
                                    try
                                    {
                                        oCDFeDados.Excluir(0, dr["NumeroMTRe"].ToString(), false, false);
                                    }
                                    finally
                                    {
                                        lblMensagem.Text = "Excluído: " + _iCount.ToString() + "/" + _dtCDFe.Rows.Count.ToString();
                                        lblMensagem.Refresh();
                                        _iCount++;
                                    }
                                }
                            }
                            oCDFeDados.DesconectaBanco();

                            foreach (DataRow dr in _dtCDFe.Rows)
                            {
                                if (dr["NumeroMTRe"].ToString() != "" && dr["CodigoIBAMA"].ToString() != "")
                                {
                                    try
                                    {
                                        oCDFeDados = new clsCDFeDados();
                                        oCDFe.NumeroMTRe = 0;
                                        if (dr["NumeroMTRe"].ToString() != "")
                                            oCDFe.NumeroMTRe = Convert.ToInt64(dr["NumeroMTRe"]);
                                        oCDFe.Quantidade = 0;
                                        if (dr["Quantidade"].ToString() != "")
                                            oCDFe.Quantidade = Convert.ToDecimal(dr["Quantidade"]);
                                        oCDFe.NumeroCDFe = 0;
                                        if (dr["NumeroCDFe"].ToString().Trim() != "" && geral.IsNumeric(dr["NumeroCDFe"].ToString().Trim()))
                                            oCDFe.NumeroCDFe = Convert.ToInt64(dr["NumeroCDFe"]);
                                        if (dr["Data"].ToString() != "")
                                            oCDFe.Data = Convert.ToDateTime(dr["Data"]);
                                        oCDFe.CodigoIBAMA = "";
                                        if (dr["CodigoIBAMA"].ToString() != "")
                                            oCDFe.CodigoIBAMA = dr["CodigoIBAMA"].ToString().Substring(0, 2) + " " + dr["CodigoIBAMA"].ToString().Substring(2, 2) + " " + dr["CodigoIBAMA"].ToString().Substring(4, 2);
                                        oCDFe.QtdeUnidade = 0;
                                        if (dr["QtdeUnidade"].ToString() != "")
                                            oCDFe.QtdeUnidade = Convert.ToDecimal(dr["QtdeUnidade"]);
                                        oCDFe.CNPJ_CPF_Cliente = dr["CNPJ_CPF_Cliente"].ToString();
                                        if (oCDFe.CNPJ_CPF_Cliente.Length > 11)
                                        {
                                            oCDFe.CNPJ_CPF_Cliente = geral.Left(oCDFe.CNPJ_CPF_Cliente, 2) + "." +
                                                                        oCDFe.CNPJ_CPF_Cliente.Substring(2, 3) + "." +
                                                                        oCDFe.CNPJ_CPF_Cliente.Substring(5, 3) + "/" +
                                                                        oCDFe.CNPJ_CPF_Cliente.Substring(8, 4) + "-" +
                                                                        oCDFe.CNPJ_CPF_Cliente.Substring(12, 2);
                                        }
                                        else if(oCDFe.CNPJ_CPF_Cliente.Length == 11)
                                        {
                                            oCDFe.CNPJ_CPF_Cliente = geral.Left(oCDFe.CNPJ_CPF_Cliente, 3) + "." +
                                                                        oCDFe.CNPJ_CPF_Cliente.Substring(3, 3) + "." +
                                                                        oCDFe.CNPJ_CPF_Cliente.Substring(6, 3) + "-" +
                                                                        oCDFe.CNPJ_CPF_Cliente.Substring(9, 2);

                                        }
                                        oCDFe.Situacao = dr["Situacao"].ToString();
                                        oCDFe.Placas = dr["Placas"].ToString();

                                        oCDFeDados.Inserir(oCDFe);

                                        //// verificar se a MTR-e pertence é igual a do cliente atual, caso contrário informar inconsistÊncia
                                        //oLancMTRDados = new clsLancamentoMTRDados();
                                        //if (!oLancMTRDados.MTReExisteEmClienteDiferente(oCDFe.NumeroMTRe, oCDFe.CNPJ_CPF_Cliente))
                                        //{
                                        //    oCDFeDados.Inserir(oCDFe);
                                        //}
                                        //else
                                        //{
                                        //    DataRow _drInc = _dtInconsistencias.NewRow();
                                        //    _drInc[0] = oCDFe.NumeroMTRe.ToString();
                                        //    _drInc[1] = oCDFe.CNPJ_CPF_Cliente;
                                        //    _drInc[2] = "MTR-e não é deste cliente - Rejeitado";
                                        //    _dtInconsistencias.Rows.Add(_drInc);
                                        //}
                                    }
                                    finally
                                    {
                                        lblMensagem.Text = "Importado/lido: " + i.ToString();
                                    }
                                    i++;
                                    lblMensagem.Text = "Importado/lido: " + i.ToString() + " registros importados";
                                    lblMensagem.Refresh();
                                }
                            }
                            lblMensagem.Text = "Importação realizada com sucesso! " + _dtCDFe.Rows.Count.ToString("000000") + " registros importados/rejeitados";
                            bindingSource.DataSource = _dtCDFe;
                            GradeCDFe.DataSource = bindingSource;

                            GradeInconsistecias.AutoGenerateColumns = false;
                            bindingSourceInconsistencias.DataSource = _dtInconsistencias;
                            GradeInconsistecias.DataSource = bindingSourceInconsistencias;
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
            for (int i = regs; i <= GradeInconsistecias.Rows.Count - 1; i++)
            {
                regs++;
                if (ln < 1000 && GradeInconsistecias.Rows[i].Cells[0].Value != null)
                {                    
                    ln = ln + 12F;
                    cl = 10.0F;
                    e.Graphics.DrawString(GradeInconsistecias.Rows[i].Cells[0].Value.ToString(), ft, Brushes.Black, cl, ln);
                    cl = cl + 100;
                    e.Graphics.DrawString(geral.Left(GradeInconsistecias.Rows[i].Cells[1].Value.ToString(), 20), ft, Brushes.Black, cl, ln);
                    cl = cl + 240;
                    e.Graphics.DrawString(GradeInconsistecias.Rows[i].Cells[2].Value.ToString(), ft, Brushes.Black, cl, ln);
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
            if (regs >= (GradeInconsistecias.Rows.Count))
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (GradeInconsistecias.Rows.Count > 1)
                Imprimir();
            else
                MessageBox.Show("Não há dados!");
        }

        private void GradeCDFe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ctlCliente_Enter(object sender, EventArgs e)
        {
            geral.VoltaForm = this.Name;
            geral.CodigoCliente = 0;
        }

        private void destinofinal1_Leave(object sender, EventArgs e)
        {
            if (destinofinal1.txtCodigo.Text != "")
                ctlCliente.txtCodigo.Enabled = false;
            else
                ctlCliente.txtCodigo.Enabled = true;
        }

        private void ctlCliente_Leave(object sender, EventArgs e)
        {
            if (ctlCliente.txtCodigo.Text != "")
                destinofinal1.txtCodigo.Enabled = false;
            else
                destinofinal1.txtCodigo.Enabled = true;
        }

        private void destinofinal1_Enter(object sender, EventArgs e)
        {
            geral.CodigoDESTINOFINAL = 0;
            geral.VoltaForm = "DESTINOFINAL";
        }

        private void frmCDFeImportacao_Load(object sender, EventArgs e)
        {
            lblDB.Text = "db: " + geral.BancoUsado.ToString();
        }
    }
}
