using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.OleDb;
using SILCNegocios;

namespace LibSILC
{
    public class clsGeradorRelatorioDados
    {
	    private clsDB oDB = new clsDB();
        private MySqlDataAdapter l_myData = new MySqlDataAdapter();
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
        public clsGeradorRelatorio PegaDados(clsGeradorRelatorio pGeradorRelatorio, int pCodigo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo,  NomeArquivo, SqlConsulta \n"; 
                s = s + "from   RelatoriosGerador \n";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " \n";
                    s = s + "limit 1";
                }
                else if (pCodigo == 0)
                {
                    s = s + " order by Codigo limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows[0]["Codigo"].ToString() != "")
                        pGeradorRelatorio.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                    pGeradorRelatorio.NomeArquivo = l_dt.Rows[0]["NomeArquivo"].ToString();
                    pGeradorRelatorio.SqlConsulta = l_dt.Rows[0]["SqlConsulta"].ToString();
                }
                return pGeradorRelatorio;
            }
            catch (Exception ex)
            {
                return new clsGeradorRelatorio();
            }
            finally
            {
                DesconectaBanco();
            }
        }
        
        public DataTable PegaDados(clsGeradorRelatorio pGeradorRelatorio, int pCodigo, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo,  NomeArquivo, SqlConsulta \n"; 
                s = s + "from   RelatoriosGerador \n";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " ";
                }
                if (pUltimoRegistro)
                {
                    s = s + "order by Codigo desc ";
                }
                else if (pCodigo == 0)
                {
                    s = s + " order by Campo ";
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
        public clsGeradorRelatorio PegaDados(clsGeradorRelatorio pGeradorRelatorio, string pNomeArquivo)
        {
            try
            {
                ConectaBanco();
                if (pNomeArquivo != "")
                {
                    s = "";
                    s = s + "select Codigo,  NomeArquivo, SqlConsulta \n";
                    s = s + "from   RelatoriosGerador \n";
                    s = s + "where  NomeArquivo = '" + pNomeArquivo + "' \n";
                    s = s + "limit 1";
                    FillDataSet();
                    if (l_ds.Tables.Count > 0)
                    {
                        l_dt = l_ds.Tables[0];
                        if (l_dt.Rows[0]["Codigo"].ToString() != "")
                            pGeradorRelatorio.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                        pGeradorRelatorio.NomeArquivo = l_dt.Rows[0]["NomeArquivo"].ToString();
                        pGeradorRelatorio.SqlConsulta = l_dt.Rows[0]["SqlConsulta"].ToString();
                    }
                }
                return pGeradorRelatorio;
            }
            catch (Exception ex)
            {
                return new clsGeradorRelatorio();
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
                s = s + "from RelatoriosGerador ";
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
        public string DadoExiste(int pCodigo)
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Codigo ";
                s = s + "from   RelatoriosGerador ";
                if (pCodigo != 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " ";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pCodigo != 0)
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
        public string RelatorioExiste(string pNomeArquivo)
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                if (pNomeArquivo != "")
                {
                    s = s + "select Codigo ";
                    s = s + "from   RelatoriosGerador ";
                    s = s + "where  NomeArquivo = '" + pNomeArquivo + "'";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pNomeArquivo != "")
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

        public void Inserir(clsGeradorRelatorio pGeradorRelatorio, int pCodigo = 0)
        {
            try
            {
                s = "";
                s = s + "insert into RelatoriosGerador \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + "  Codigo, \n";
                s = s + "   NomeArquivo, SqlConsulta \n"; 
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + pCodigo.ToString() + ", \n";
                s = s + " '" + pGeradorRelatorio.NomeArquivo + "', \n";
                s = s + " '" + pGeradorRelatorio.SqlConsulta + "' \n";
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
        public string Alterar(clsGeradorRelatorio pGeradorRelatorio, int pCodigo)
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
                s = s + "update RelatoriosGerador \n";
                s = s + "set NomeArquivo = '" + pGeradorRelatorio.NomeArquivo + "', \n";
                s = s + "    SqlConsulta = '" + pGeradorRelatorio.SqlConsulta + "' \n";
                s = s + "where Codigo = " + pCodigo + " \n";        
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
        public string Alterar(clsGeradorRelatorio pGeradorRelatorio, string pNomeArquivo)
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
                s = s + "update RelatoriosGerador \n";
                s = s + "set    SqlConsulta = '" + pGeradorRelatorio.SqlConsulta + "' \n";
                s = s + "where  NomeArquivo = '" + pNomeArquivo + "' \n";
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
        public DataTable PreencheDataTable(string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo,  NomeArquivo, SqlConsulta \n";
                s = s + "from   RelatoriosGerador \n";
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

        public DataTable PreencheDataTable(string pOrdem, string pFiltro, string pCampo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo,  NomeArquivo, SqlConsulta \n";
                s = s + "from   RelatoriosGerador \n";
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

        public string Excluir(int pCodigo = 0)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                if (pCodigo > 0)
                {
                    s = "";
                    s = s + "delete from RelatoriosGerador ";
                    s = s + "where  Codigo = " + pCodigo.ToString();
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