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
    public class clsUsuarioLogadoDados
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
        public clsUsuarioLogado PegaDados(clsUsuarioLogado pUsuarioLogado, string pMaquina, string pUsuarioWindows)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Maquina, UsuarioWindows, UsuarioNome, CodigoEmpresa, Aplicativo \n"; 
                s = s + "from   UsuarioLogado \n";
                s = s + "where  Maquina = '" + pMaquina + "' \n";
                s = s + "and    UsuarioWindows = '" + pUsuarioWindows + "' \n";
                s = s + "limit 1";
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    pUsuarioLogado.Maquina = pMaquina;
                    pUsuarioLogado.UsuarioWindows = pUsuarioWindows;
                    if (l_dt.Rows[0]["Codigo"].ToString() != "")
                        pUsuarioLogado.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                    pUsuarioLogado.UsuarioNome = l_dt.Rows[0]["UsuarioNome"].ToString();
                    if (l_dt.Rows[0]["CodigoEmpresa"].ToString() != "")
                        pUsuarioLogado.CodigoEmpresa = Convert.ToInt32(l_dt.Rows[0]["CodigoEmpresa"]);
                    if (l_dt.Rows[0]["Aplicativo"].ToString() != "")
                    {
                        if (Convert.ToInt32(l_dt.Rows[0]["Aplicativo"]) == 1)
                            pUsuarioLogado.Aplicativo = true;
                        else
                            pUsuarioLogado.Aplicativo = false;
                    }
                }                
            }
            catch (Exception ex)
            {
                pUsuarioLogado = new clsUsuarioLogado();
            }
            finally
            {
                DesconectaBanco();
            }
            return pUsuarioLogado;
        }
        public int PegaTamanhoCampoVarChar(string pCampo)
        {
            int iRet = 0;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "show columns ";
                s = s + "from UsuarioLogado ";
                s = s + "where  type like 'varchar%' and field = '" + pCampo + "' ";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                {
                    iRet = Convert.ToInt16(l_dt.Rows[0]["Type"].ToString().Replace("varchar(", "").Replace(")", ""));
                }
            }
            catch 
            {
                iRet = 0;
            }
            finally
            {
                DesconectaBanco();
            }
            return iRet;
        }
        public void Inserir(clsUsuarioLogado pUsuarioLogado, int pCodigo = 0)
        {
            try
            {
                s = "";
                s = s + "insert into UsuarioLogado \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + "  Codigo, \n";
                s = s + "  Maquina, UsuarioWindows, UsuarioNome, CodigoEmpresa, Aplicativo \n"; 
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + pCodigo.ToString() + ", \n";
                s = s + " '" + pUsuarioLogado.Maquina + "', \n";
                s = s + " '" + pUsuarioLogado.UsuarioWindows + "', \n";
                s = s + " '" + pUsuarioLogado.UsuarioNome + "', \n";
                if (pUsuarioLogado.CodigoEmpresa > 0)
                    s = s + " " + pUsuarioLogado.CodigoEmpresa.ToString() + ", \n";
                else
                    s = s + "0, \n";
                if (pUsuarioLogado.Aplicativo)
                    s = s + " 1 \n";
                else
                    s = s + " 0 \n";
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
        public string Alterar(clsUsuarioLogado pUsuarioLogado, int pCodigo)
        {
            string sRet = "";
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update UsuarioLogado \n";
                s = s + "set Maquina        = '" + pUsuarioLogado.Maquina + "', \n";
                s = s + "    UsuarioWindows = '" + pUsuarioLogado.UsuarioWindows + "', \n";
                s = s + "    UsuarioNome    = '" + pUsuarioLogado.UsuarioNome + "', \n";
                s = s + "    CodigoEmpresa  = " + pUsuarioLogado.CodigoEmpresa + "', \n";
                if (pUsuarioLogado.Aplicativo)
                    s = s + "    Aplicativo = 1 \n";
                else
                    s = s + "    Aplicativo = 0 \n";
                s = s + "where Codigo = " + pCodigo + " \n";        
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
                sRet = string.Empty;
            }
            catch (Exception ex)
            {
               transaction.Rollback();
                sRet = ex.ToString();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }

        public string Excluir(int pCodigo = 0)
        {
            string sRet = "";
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                if (pCodigo > 0)
                {
                    s = s + "delete from UsuarioLogado ";
                    s = s + "where  Codigo = " + pCodigo.ToString();
                }
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
                sRet = string.Empty;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                sRet = ex.Message.ToString();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }
    }
}