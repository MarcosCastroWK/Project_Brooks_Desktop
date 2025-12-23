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
using System.Data.Odbc;

namespace formSILC
{
    public partial class frmSistemaFatma : Form
    {
        DataTable _dtSistFatma = new DataTable();
        private BindingSource bindingSource = new BindingSource();

        public frmSistemaFatma()
        {
            InitializeComponent();
        }

        private void btnSelecionarArquivos_Click(object sender, EventArgs e)
        {
            try
            {
                //define as propriedades do controle 
                this.ofd1.Multiselect = false;
                this.ofd1.Title = "Selecionar txt";
                ofd1.InitialDirectory = @"C:\Temp";
                ofd1.Filter = "Text files (*.txt)|*.txt";
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

                    _dtSistFatma = new DataTable();
                    _dtSistFatma.Columns.Add("RazaoSocial");
                    _dtSistFatma.Columns.Add("OBS");
                    _dtSistFatma.Columns.Add("CNPJ_CPF");
                    _dtSistFatma.Columns.Add("SenhaMaster");
                    _dtSistFatma.Columns.Add("SenhaAcesso");
                    _dtSistFatma.Columns.Add("Contato");
                    DataRow _dr;
                    string[] lines = System.IO.File.ReadAllLines(ofd1.FileNames[0]);

                    i = 0;
                    foreach (string line in lines)
                    {
                        if (line.Split("\t"[0]).Length >= 4 && i > 0)
                        {
                            if (line.Split("\t"[0])[6].ToString() != "")
                            {
                                _dr = _dtSistFatma.NewRow();
                                _dr[0] = line.Split("\t"[0])[0];
                                _dr[1] = line.Split("\t"[0])[1];
                                _dr[2] = line.Split("\t"[0])[2];
                                _dr[3] = line.Split("\t"[0])[3];
                                _dr[4] = line.Split("\t"[0])[4];
                                _dr[5] = line.Split("\t"[0])[5];
                                _dtSistFatma.Rows.Add(_dr);
                            }
                        }
                        i++;
                    }
                    i = 0;

                    //cria a conexão com o banco de dados
                    OleDbConnection aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                    aConnection.Open();

                    foreach (DataRow dr in _dtSistFatma.Rows)
                    {
                        if (dr["RazaoSocial"].ToString() != "")
                        {
                            string sAlf = " ABCDEFGHIJKLMNOPQRS";
                            foreach (char sChar in sAlf)
                            {
                                string s = "";
                                s = s + " update Clientes\n";
                                if (dr["SenhaMaster"].ToString() != "")
                                    s = s + " set SenhaMasterFatima = '" + geral.Left(dr["SenhaMaster"].ToString(), 20) + "', \n";
                                else
                                    s = s + " set SenhaMasterFatima = '', \n";
                                if (dr["SenhaAcesso"].ToString() != "")
                                    s = s + "     SenhaAcessoFatima = '" + geral.Left(dr["SenhaAcesso"].ToString(), 20) + "', \n";
                                else
                                    s = s + "     SenhaAcessoFatima = '', \n";
                                if (dr["OBS"].ToString() != "")
                                    s = s + "     ObsFatima         = '" + geral.Left(dr["OBS"].ToString(), 20) + "', \n";
                                else
                                    s = s + "     ObsFatima         = '', \n";
                                if (dr["Contato"].ToString() != "")
                                    s = s + "     ContatoFatima     = '" + geral.Left(dr["Contato"].ToString(), 20) + "' \n";
                                else
                                    s = s + "     ContatoFatima     = '' \n";

                                if (dr["CNPJ_CPF"].ToString() != "")
                                {
                                    s = s + " where CGC_CPF         = '" + dr["CNPJ_CPF"].ToString() + sChar.ToString().Trim() + "' \n";
                                    //cria o objeto command and armazena a consulta SQL                            
                                    OleDbCommand aCommand = new OleDbCommand(s, aConnection);
                                    aCommand.Transaction = aConnection.BeginTransaction();
                                    aCommand.CommandText = s;
                                    aCommand.ExecuteNonQuery();
                                    aCommand.Transaction.Commit();
                                }
                                i++;
                                lblMensagem.Text = "Importado/lido: " + i.ToString() + " registros importados";
                                lblMensagem.Refresh();
                            }
                        }
                    }
                    //fecha a conexao 
                    aConnection.Close();
                    aConnection.Dispose();
                    lblMensagem.Text = "Importação realizada com sucesso! " + _dtSistFatma.Rows.Count.ToString("000000") + " registros importados";
                    bindingSource.DataSource = _dtSistFatma;
                    Grade.DataSource = bindingSource;
                }


            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
            }


        }
    }


}
