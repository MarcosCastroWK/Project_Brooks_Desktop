using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using MySql.Data.MySqlClient;
using System.Data.OleDb;
using SILCNegocios;

namespace LibSILC
{
    public class clsAnaliseDDRDados
	{
	    private clsDB oDB = new clsDB();
        private MySqlDataAdapter l_myData;
        private DataSet l_ds = new DataSet();
        DataTable l_dt = new DataTable();
        private string s;

        // funções locais para multi banco de dados
        private void ConectaBanco()
        {
            oDB.ConectaMySql();
        }
        private void FillDataSet()
        {
            l_ds = new DataSet();
            l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
            l_myData.Fill(l_ds);
            l_dt = l_ds.Tables[0];
        }
        private void DesconectaBanco()
        {
            oDB.DesconectaMySql();
        }
        public clsAnaliseDDR PegaDados(clsAnaliseDDR pAnaliseDDR, int pSequencial)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + " select Sequencial, CodigoCliente, CodigoUsuario, PeriodoInicial, PeriodoFinal, \n";
                s = s + "        Status, R, Acesso, NomeArquivo \n"; 
                s = s + "from   AnaliseDDR \n";
                if (pSequencial > 0)
                {
                    s = s + "where  Sequencial = " + pSequencial + " \n";
                    s = s + "limit 1";
                }
                else if (pSequencial == 0)
                {
                    s = s + " order by Sequencial limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows[0]["Sequencial"].ToString() != "")
                        pAnaliseDDR.Sequencial = Convert.ToInt32(l_dt.Rows[0]["Sequencial"]);
                    if (l_dt.Rows[0]["CodigoCliente"].ToString() != "")
                        pAnaliseDDR.CodigoCliente = Convert.ToInt32(l_dt.Rows[0]["CodigoCliente"]);
                    if (l_dt.Rows[0]["CodigoUsuario"].ToString() != "")
                        pAnaliseDDR.CodigoUsuario = Convert.ToInt32(l_dt.Rows[0]["CodigoUsuario"]);
                    if (l_dt.Rows[0]["PeriodoInicial"].ToString() != "")
                        pAnaliseDDR.PeriodoInicial = Convert.ToDateTime(l_dt.Rows[0]["PeriodoInicial"].ToString()).Date.ToShortDateString();
                    if (l_dt.Rows[0]["PeriodoFinal"].ToString() != "")
                        pAnaliseDDR.PeriodoFinal = Convert.ToDateTime(l_dt.Rows[0]["PeriodoFinal"].ToString()).Date.ToShortDateString();
                    if (l_dt.Rows[0]["Status"].ToString() != "")
                        pAnaliseDDR.Status = Convert.ToInt32(l_dt.Rows[0]["Status"]);
                    if (l_dt.Rows[0]["R"].ToString() != "")
                        pAnaliseDDR.R = Convert.ToInt32(l_dt.Rows[0]["R"]);
                    if (l_dt.Rows[0]["Acesso"].ToString() != "")
                        pAnaliseDDR.Acesso = Convert.ToInt32(l_dt.Rows[0]["Acesso"]);
                    pAnaliseDDR.NomeArquivo = l_dt.Rows[0]["NomeArquivo"].ToString();
                }
                return pAnaliseDDR;
            }
            catch (Exception ex)
            {
                return new clsAnaliseDDR();
            }
            finally
            {
                DesconectaBanco();
            }
        }
        
        public DataTable PegaDados(clsAnaliseDDR pAnaliseDDR, int pSequencial, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + " select Sequencial, CodigoCliente, CodigoUsuario, PeriodoInicial, PeriodoFinal, \n";
                s = s + "        Status, R, Acesso, NomeArquivo \n";
                s = s + "from   AnaliseDDR \n";
                if (pSequencial > 0)
                {
                    s = s + "where  Sequencial = " + pSequencial + " ";
                }
                if (pUltimoRegistro)
                {
                    s = s + "order by Sequencial desc ";
                }
                FillDataSet();
                return l_dt;
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public int PegaTamanhoCampoVarChar(string pCampo)
        {
            int iRet = 0;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "show columns ";
                s = s + "from AnaliseDDR ";
                s = s + "where  type like 'varchar%' and field = '" + pCampo + "' ";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                {
                    iRet = Convert.ToInt16(l_dt.Rows[0]["Type"].ToString().Replace("varchar(", "").Replace(")", ""));
                }
            }
            catch (Exception ex)
            {
                iRet = 0;
            }
            finally
            {
                DesconectaBanco();
            }
            return iRet;
        }
        public string DadoExiste(int pSequencial)
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Sequencial ";
                s = s + "from   AnaliseDDR  ";
                if (pSequencial != 0)
                {
                    s = s + "where  Sequencial = " + pSequencial + " ";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pSequencial != 0)
                    return "Alterar";
                else
                    return "Incluir";
            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
        public void Inserir(clsAnaliseDDR pAnaliseDDR)
        {
            try
            {
                s = "";
                s = s + "insert into AnaliseDDR \n";
                s = s + "( \n";
                s = s + "  CodigoCliente, CodigoUsuario, PeriodoInicial, PeriodoFinal, \n";
                s = s + "  R, Acesso, NomeArquivo \n"; 
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pAnaliseDDR.CodigoCliente > 0)
                    s = s + " " + pAnaliseDDR.CodigoCliente.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pAnaliseDDR.CodigoUsuario > 0)
                    s = s + " " + pAnaliseDDR.CodigoUsuario.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pAnaliseDDR.PeriodoInicial != "")
                    s = s + "'" + Convert.ToDateTime(pAnaliseDDR.PeriodoInicial).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                if (pAnaliseDDR.PeriodoFinal != "")
                    s = s + "'" + Convert.ToDateTime(pAnaliseDDR.PeriodoFinal).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                if (pAnaliseDDR.R > 0)
                    s = s + " " + pAnaliseDDR.R.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pAnaliseDDR.Acesso > 0)
                    s = s + " " + pAnaliseDDR.Acesso.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                s = s + " '" + pAnaliseDDR.NomeArquivo + "' \n";
                s = s + ")"; 
                ConectaBanco();
                MySqlCommand command = oDB.MySqlConnect.CreateCommand();
                MySqlTransaction transaction;
                transaction = oDB.MySqlConnect.BeginTransaction();
                command.Connection = oDB.MySqlConnect;
                command.Transaction = transaction;
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public string Alterar(clsAnaliseDDR pAnaliseDDR, int pSequencial)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update AnaliseDDR \n";
                if (pAnaliseDDR.CodigoCliente > 0)
                    s = s + " set CodigoCliente = " + pAnaliseDDR.CodigoCliente.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " set CodigoCliente = 0, \n";
                if (pAnaliseDDR.CodigoUsuario > 0)
                    s = s + " CodigoUsuario =  " + pAnaliseDDR.CodigoUsuario.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " CodigoUsuario = 0, \n";
                if (pAnaliseDDR.PeriodoInicial != "")
                    s = s + " PeriodoInicial = '" + Convert.ToDateTime(pAnaliseDDR.PeriodoInicial).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + " PeriodoInicial = '0001-01-01', \n";
                if (pAnaliseDDR.PeriodoFinal != "")
                    s = s + " PeriodoFinal = '" + Convert.ToDateTime(pAnaliseDDR.PeriodoFinal).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + " PeriodoFinal = '0001-01-01', \n";
                if (pAnaliseDDR.R > 0)
                    s = s + " R = " + pAnaliseDDR.R.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " R = 0, \n";
                if (pAnaliseDDR.Acesso > 0)
                    s = s + " Acesso = " + pAnaliseDDR.Acesso.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " Acesso = 0, \n";
                s = s + "  NomeArquivo = '" + pAnaliseDDR.NomeArquivo + "' \n"; 
                
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
                return string.Empty;
            }
            catch (Exception ex)
            {
               transaction.Rollback();
               return ex.ToString();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
        public DataTable PreencheDataTableCacambas(string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial, CodigoCliente, CodigoUsuario, PeriodoInicial, PeriodoFinal, \n";
                s = s + "       Status, R, Acesso, NomeArquivo \n";
                s = s + "from   AnaliseDDR \n";
                s = s + "order by " + pOrdem;
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                DesconectaBanco();
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                return l_dt;
            }
        }

        public DataTable PreencheDataTableCacambas(string pOrdem, string pFiltro, string pCampo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial, CodigoCliente, CodigoUsuario, PeriodoInicial, PeriodoFinal, \n";
                s = s + "       Status, R, Acesso, NomeArquivo \n";
                s = s + "from   AnaliseDDR \n";
                s = s + "where " + pCampo + " like '%" + pFiltro + "%' \n";
                s = s + "order by " + pOrdem +" \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                DesconectaBanco();
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                return l_dt;
            }
        }

        public string Excluir(int pSequencial)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                if (pSequencial > 0)
                {
                    s = "";
                    s = s + "delete from AnaliseDDR ";
                    s = s + "where  Sequencial = " + pSequencial.ToString();
                }
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
                return string.Empty;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return ex.Message.ToString();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
    }
}