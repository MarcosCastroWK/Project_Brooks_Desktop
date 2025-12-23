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
    public class clsAnaliseCDFDados
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
        public clsAnaliseCDF PegaDados(clsAnaliseCDF pAnaliseCDF, int pSequencial)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + " select Sequencial, CodigoCliente, CodigoUsuario, PeriodoInicial, PeriodoFinal, \n";
                s = s + "        Status, R, DestinoFinal, Acesso, NomeArquivo \n"; 
                s = s + "from   AnaliseCDF \n";
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
                        pAnaliseCDF.Sequencial = Convert.ToInt32(l_dt.Rows[0]["Sequencial"]);
                    if (l_dt.Rows[0]["CodigoCliente"].ToString() != "")
                        pAnaliseCDF.CodigoCliente = Convert.ToInt32(l_dt.Rows[0]["CodigoCliente"]);
                    if (l_dt.Rows[0]["CodigoUsuario"].ToString() != "")
                        pAnaliseCDF.CodigoUsuario = Convert.ToInt32(l_dt.Rows[0]["CodigoUsuario"]);
                    if (l_dt.Rows[0]["PeriodoInicial"].ToString() != "")
                        pAnaliseCDF.PeriodoInicial = Convert.ToDateTime(l_dt.Rows[0]["PeriodoInicial"].ToString()).Date.ToShortDateString();
                    if (l_dt.Rows[0]["PeriodoFinal"].ToString() != "")
                        pAnaliseCDF.PeriodoFinal = Convert.ToDateTime(l_dt.Rows[0]["PeriodoFinal"].ToString()).Date.ToShortDateString();
                    if (l_dt.Rows[0]["Status"].ToString() != "")
                        pAnaliseCDF.Status = Convert.ToInt32(l_dt.Rows[0]["Status"]);
                    if (l_dt.Rows[0]["R"].ToString() != "")
                        pAnaliseCDF.R = Convert.ToInt32(l_dt.Rows[0]["R"]);
                    pAnaliseCDF.DestinoFinal = l_dt.Rows[0]["DestinoFinal"].ToString();
                    if (l_dt.Rows[0]["Acesso"].ToString() != "")
                        pAnaliseCDF.Acesso = Convert.ToInt32(l_dt.Rows[0]["Acesso"]);
                    pAnaliseCDF.NomeArquivo = l_dt.Rows[0]["NomeArquivo"].ToString();
                }
                return pAnaliseCDF;
            }
            catch (Exception ex)
            {
                return new clsAnaliseCDF();
            }
            finally
            {
                DesconectaBanco();
            }
        }
        
        public DataTable PegaDados(clsAnaliseCDF pAnaliseCDF, int pSequencial, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + " select Sequencial, CodigoCliente, CodigoUsuario, PeriodoInicial, PeriodoFinal, \n";
                s = s + "        Status, R, DestinoFinal, Acesso, NomeArquivo \n";
                s = s + "from   AnaliseCDF \n";
                if (pSequencial > 0)
                {
                    s = s + "where  Sequencial = " + pSequencial + " ";
                }
                if (pUltimoRegistro)
                {
                    s = s + "order by Sequencial desc ";
                }
                else if (pSequencial == 0)
                {
                    s = s + " order by DestinoFinal ";
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
                s = s + "from AnaliseCDF ";
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
                s = s + "from   AnaliseCDF  ";
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
        public void Inserir(clsAnaliseCDF pAnaliseCDF)
        {
            try
            {
                s = "";
                s = s + "insert into AnaliseCDF \n";
                s = s + "( \n";
                s = s + "  CodigoCliente, CodigoUsuario, PeriodoInicial, PeriodoFinal, \n";
                s = s + "  R, DestinoFinal, Acesso, NomeArquivo \n"; 
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pAnaliseCDF.CodigoCliente > 0)
                    s = s + " " + pAnaliseCDF.CodigoCliente.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pAnaliseCDF.CodigoUsuario > 0)
                    s = s + " " + pAnaliseCDF.CodigoUsuario.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pAnaliseCDF.PeriodoInicial != "")
                    s = s + "'" + Convert.ToDateTime(pAnaliseCDF.PeriodoInicial).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                if (pAnaliseCDF.PeriodoFinal != "")
                    s = s + "'" + Convert.ToDateTime(pAnaliseCDF.PeriodoFinal).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                if (pAnaliseCDF.R > 0)
                    s = s + " " + pAnaliseCDF.R.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                s = s + " '" + pAnaliseCDF.DestinoFinal + "', \n";
                if (pAnaliseCDF.Acesso > 0)
                    s = s + " " + pAnaliseCDF.Acesso.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                s = s + " '" + pAnaliseCDF.NomeArquivo + "' \n";
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
        public string Alterar(clsAnaliseCDF pAnaliseCDF, int pSequencial)
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
                s = s + "update AnaliseCDF \n";
                if (pAnaliseCDF.CodigoCliente > 0)
                    s = s + " set CodigoCliente = " + pAnaliseCDF.CodigoCliente.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " set CodigoCliente = 0, \n";
                if (pAnaliseCDF.CodigoUsuario > 0)
                    s = s + " CodigoUsuario =  " + pAnaliseCDF.CodigoUsuario.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " CodigoUsuario = 0, \n";
                if (pAnaliseCDF.PeriodoInicial != "")
                    s = s + " PeriodoInicial = '" + Convert.ToDateTime(pAnaliseCDF.PeriodoInicial).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + " PeriodoInicial = '0001-01-01', \n";
                if (pAnaliseCDF.PeriodoFinal != "")
                    s = s + " PeriodoFinal = '" + Convert.ToDateTime(pAnaliseCDF.PeriodoFinal).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + " PeriodoFinal = '0001-01-01', \n";
                if (pAnaliseCDF.R > 0)
                    s = s + " R = " + pAnaliseCDF.R.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " R = 0, \n";
                s = s + " DestinoFinal = '" + pAnaliseCDF.DestinoFinal + "', \n";
                if (pAnaliseCDF.Acesso > 0)
                    s = s + " Acesso = " + pAnaliseCDF.Acesso.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " Acesso = 0, \n";
                s = s + "  NomeArquivo = '" + pAnaliseCDF.NomeArquivo + "' \n"; 
                
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
                s = s + "       Status, R, DestinoFinal, Acesso, NomeArquivo \n";
                s = s + "from   AnaliseCDF \n";
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
                s = s + "       Status, R, DestinoFinal, Acesso, NomeArquivo \n";
                s = s + "from   AnaliseCDF \n";
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
                    s = s + "delete from AnaliseCDF ";
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