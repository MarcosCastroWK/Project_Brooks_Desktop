using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using MySql.Data.MySqlClient;
using SILCNegocios;
using System.Data.Sql;


namespace LibSILC
{
	public class clsUsuarioDados : IDisposable
	{
        void IDisposable.Dispose() { }

	    private clsDB oDB = new clsDB();
        private MySqlDataAdapter l_myData;
        private DataSet l_ds = new DataSet();
        DataTable l_dt = new DataTable();
        private string s;

        private void FillDataSet()
        {
            l_ds = new DataSet();
            l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
            l_myData.Fill(l_ds);
            l_dt = l_ds.Tables[0];
        }

        public DataTable PegaDadosEmpresa(clsUsuarios pUsuario, int pCodigo, bool pUltimoRegistro, int pCodigoEmpresa)
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select u.Codigo, u.Nome as Nome, p.Nome as 'Nome Empresa' ";
                s = s + "from   Usuarios u ";
                s = s + "inner join Parametros p on p.Numero = u.CodigoEmpresa ";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " ";
                    s = s + "and    CodigoEmpresa = " + pCodigoEmpresa + " ";
                }
                else
                {
                    s = s + "where  CodigoEmpresa = " + pCodigoEmpresa + " ";
                }
                if (pUltimoRegistro)
                {
                    s = s + "order by Codigo desc ";
                }
                else if (pCodigo == 0)
                {
                    s = s + " order by u.Nome ";
                }
                FillDataSet();                
            }
            catch (Exception ex)
            {
                l_dt = new DataTable();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return l_dt;
        }
        public int PegaTamanhoCampoVarChar(string pCampo)
        {
            int iRet = 0;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "show columns ";
                s = s + "from Usuarios ";
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
                oDB.DesconectaMySql();
            }
            return iRet;
        }        
        public string PegaDados(int pCodigo, string pNome, int pCodigoEmpresa)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select ";
                s = s + "        Nome ";
                s = s + "from   Usuarios ";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo.ToString() + " \n";
                    s = s + "and    CodigoEmpresa = " + pCodigoEmpresa + " \n";
                    s = s + "limit 1 ";
                    l_ds = new DataSet();
                    FillDataSet();
                    if (l_ds.Tables[0].Rows.Count > 0)
                    {
                        sRet = l_ds.Tables[0].Rows[0]["Nome"].ToString().ToUpper();
                    }
                }
                else if (pNome.Length > 0)
                {
                    s = s + "where  Nome = '" + pNome + "' \n";
                    s = s + "and    CodigoEmpresa = " + pCodigoEmpresa + " \n";
                    s = s + "limit 1 ";
                    l_ds = new DataSet();
                    FillDataSet();
                    if (l_ds.Tables[0].Rows.Count > 0)
                    {
                        sRet = l_ds.Tables[0].Rows[0]["Nome"].ToString().ToUpper();
                    }
                }
            }
            catch (Exception ex)
            {
                sRet = string.Empty;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }

        public string PegaSenha(string pNome, int pCodigoEmpresa)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select ";
                s = s + "       Senha \n";
                s = s + "from   Usuarios \n";
                s = s + "where  Nome = '" + pNome + "' \n";
                s = s + "and    CodigoEmpresa = " + pCodigoEmpresa + " \n";
                s = s + " limit 1 ";
                FillDataSet();
                if (l_ds.Tables[0].Rows.Count > 0)
                {
                    sRet = l_ds.Tables[0].Rows[0]["Senha"].ToString();
                }
            }
            finally
            {
                oDB.DesconectaMySql();                
            }
            return sRet;
        }
        public int PegaCodigoUsuario(string pNome, string pSenha, int pCodigoEmpresa)
        {
            int iRet = 0;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select ";
                s = s + "       Codigo \n";
                s = s + "from   Usuarios \n";
                s = s + "where  Nome  = '" + pNome  + "' \n";
                s = s + "and    Senha = '" + pSenha + "' \n";
                s = s + "and    CodigoEmpresa = " + pCodigoEmpresa + " \n";
                s = s + "limit 1 \n";
                FillDataSet();
                if (l_ds.Tables[0].Rows.Count > 0)
                {
                    iRet = Convert.ToInt32(l_ds.Tables[0].Rows[0]["Codigo"]);
                }
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return iRet;
        }

        public int PegaCodigoUltimoUsuario()
        {
            int iRet = 0;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Codigo from Usuarios order by Codigo Desc \n";
                s = s + "limit 1 \n";
                FillDataSet();
                if (l_ds.Tables[0].Rows.Count > 0)
                {
                    iRet = Convert.ToInt32(l_ds.Tables[0].Rows[0]["Codigo"]);
                }
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return iRet;
        }

        public int PegaCodigoEmpresa(string pNome)
        {
            int iRet = 0;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select ";
                s = s + "       CodigoEmpresa \n";
                s = s + "from   Usuarios \n";
                s = s + "where  Nome = '" + pNome + "' \n";
                s = s + " limit 1 ";
                FillDataSet();
                if (l_ds.Tables[0].Rows.Count > 0)
                {
                    iRet = Convert.ToInt32(l_ds.Tables[0].Rows[0]["CodigoEmpresa"].ToString());
                }
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return iRet;
        }
        public bool ExisteUsuario(string pNome, int pCodigoEmpresa)
        {
            bool bRet = false;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select * \n";
                s = s + "from   Usuarios \n";
                s = s + "where  Nome = '" + pNome + "' \n";
                s = s + "and    CodigoEmpresa = " + pCodigoEmpresa + " \n";
                s = s + "limit 1 \n";
                FillDataSet();
                if (l_ds.Tables[0].Rows.Count > 0)
                {
                    bRet = true;
                }
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return bRet;
        }
        public void Inserir(clsUsuarios pUsuario)
        {
            try
            {
                s = "";
                s = s + "insert into Usuarios (Nome, Senha, CodigoEmpresa) ";
                s = s + "values ";
                s = s + "(";
                s = s + "'" + pUsuario.Nome + "', ";
                s = s + "'" + pUsuario.Senha + "', ";
                s = s + " " + pUsuario.CodigoEmpresa + " ";
                s = s + ")"; 
                oDB.MySqlConnect.Open();
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
                oDB.DesconectaMySql();
            }
        }
        public void Alterar(clsUsuarios pUsuario, int pCodigo)
        {
            oDB.MySqlConnect.Open();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update Usuarios ";
                s = s + "set   Nome     = '" + pUsuario.Nome + "', ";
                s = s + "      Senha    = '" + pUsuario.Senha + "', ";
                s = s + "      CodigoEmpresa = " + pUsuario.CodigoEmpresa.ToString();
                s = s + "where Codigo = " + pCodigo.ToString();
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
        public string AlteraSenha(string pSenhaNova, string pNome, int pCodigoEmpresa)
        {
            string sRet = "";
            oDB.MySqlConnect.Open();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update Usuarios ";
                s = s + "set    Senha    = '" + pSenhaNova + "' ";
                s = s + "where  Nome = '" + pNome.ToString() + "' ";
                s = s + "and    CodigoEmpresa = " + pCodigoEmpresa + " \n";
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
        public DataTable PreencheDataTableUsuarios(string pOrdem, int pCodigoEmpresa)
        {
            oDB.ConectaMySql();
            s = "";
            s = s + "select Nome, Codigo, CodigoEmpresa \n";
            s = s + "from   Usuarios \n";
            s = s + "where  CodigoEmpresa = " + pCodigoEmpresa + " \n";
            s = s + "order by " + pOrdem;
            l_ds = new DataSet();
            l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
            FillDataSet();
            oDB.DesconectaMySql();
            return l_ds.Tables[0];
        }
        public void Excluir(int pCodigo, string pNome, int pCodigoEmpresa)
        {
            oDB.MySqlConnect.Open();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "delete from Usuarios ";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo.ToString();
                }
                else
                {
                    s = s + "where  Nome = '" + pNome + "'";
                }
                s = s + "and    CodigoEmpresa = " + pCodigoEmpresa + " \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
    }
}