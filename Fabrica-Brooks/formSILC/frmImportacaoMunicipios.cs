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
    public partial class frmImportacaoMunicipios : Form
    {

        DataTable _dtImportacao = new DataTable();
        private BindingSource bindingSource = new BindingSource();

        public frmImportacaoMunicipios()
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

                    _dtImportacao = new DataTable();
                    _dtImportacao.Columns.Add("Codigo");
                    _dtImportacao.Columns.Add("CodigoFederal");
                    DataRow _dr;
                    string[] lines = System.IO.File.ReadAllLines(ofd1.FileNames[0]);

                    i = 0;
                    foreach (string line in lines)
                    {
                        if (line != "" && i > 0)
                        {
                            if (!geral.ContemLetras(line.Substring(0, 12)) && i > 0)
                            {
                                if (!geral.ContemLetras(line.Substring(32, 8)))
                                {
                                    if (line.Substring(32, 8).Trim() != "0000")
                                    {
                                        _dr = _dtImportacao.NewRow();
                                        _dr[0] = line.Substring(0, 12).Trim();
                                        _dr[1] = line.Substring(32, 8).Trim();
                                        _dtImportacao.Rows.Add(_dr);
                                    }
                                }
                            }
                        }
                        i++;
                    }
                    i = 0;

                    //cria a conexão com o banco de dados
                    OleDbConnection aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtCaminhoDBSILC.Text);
                    aConnection.Open();

                    foreach (DataRow dr in _dtImportacao.Rows)
                    {
                        if (dr["Codigo"].ToString() != "" && dr["Codigo"].ToString() != "0000")
                        {
                            string s = "";
                            s = s + " update Municipios \n";
                            if (dr["CodigoFederal"].ToString() != "")
                            {
                                s = s + " set   CodigoIPM = " + dr["CodigoFederal"].ToString() + " \n";
                                s = s + " where Codigo    = " + dr["Codigo"].ToString() + " \n";
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
                    //fecha a conexao 
                    aConnection.Close();
                    aConnection.Dispose();
                    lblMensagem.Text = "Importação realizada com sucesso! " + _dtImportacao.Rows.Count.ToString("000000") + " registros importados";
                    bindingSource.DataSource = _dtImportacao;
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
