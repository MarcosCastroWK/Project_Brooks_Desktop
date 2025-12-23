using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using MySql.Data.MySqlClient;
using MySql.Data;
using SILCNegocios;

namespace LibSILC
{
	public class clsLicencaAmbientalDados
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
        public clsLicencaAmbiental PegaDados(clsLicencaAmbiental pLicencaAmbiental, int pCodigo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select l.Codigo, l.CodigoAterro, l.CodigoAtividade, l.NumeroLicenca, l.Obs, l.PrazoValidade, \n";
                s = s + "       a.Nome as NomeDestinoFinal, l.Arquivo \n";
                s = s + "from   LicencaAmbiental l \n";
                s = s + "inner  join Aterro a on a.Codigo = l.CodigoAterro \n";
                if (pCodigo > 0)
                {
                    s = s + "where  l.Codigo = " + pCodigo + " ";
                }
                else if (pCodigo == 0)
                {
                    s = s + " order by PrazoValidade limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    pLicencaAmbiental.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                    pLicencaAmbiental.CodigoAterro = Convert.ToInt16(l_dt.Rows[0]["CodigoAterro"].ToString());
                    pLicencaAmbiental.NomeDestinoFinal = l_dt.Rows[0]["NomeDestinoFinal"].ToString();
                    pLicencaAmbiental.CodigoAtividade = l_dt.Rows[0]["CodigoAtividade"].ToString();
                    pLicencaAmbiental.NumeroLicenca = l_dt.Rows[0]["NumeroLicenca"].ToString();
                    pLicencaAmbiental.Obs = l_dt.Rows[0]["Obs"].ToString();
                    pLicencaAmbiental.Arquivo = l_dt.Rows[0]["Arquivo"].ToString();
                    pLicencaAmbiental.PrazoValidade = Convert.ToDateTime(l_dt.Rows[0]["PrazoValidade"]).ToShortDateString();
                }                
            }
            catch (Exception ex)
            {
                pLicencaAmbiental = new clsLicencaAmbiental();
            }
            finally
            {
                DesconectaBanco();
            }
            return pLicencaAmbiental;
        }
        
        public DataTable PegaDados(int pCodigoAterro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select l.Codigo, l.CodigoAterro, l.CodigoAtividade, l.NumeroLicenca, l.Obs, l.PrazoValidade, \n";
                s = s + "       a.Nome as NomeDestinoFinal, Arquivo \n";
                s = s + "from   LicencaAmbiental l \n";
                s = s + "inner  join Aterro a on a.Codigo = l.CodigoAterro \n";
                if (pCodigoAterro > 0)
                    s = s + "where  l.CodigoAterro = " + pCodigoAterro + " ";
                else if (pCodigoAterro == 0)
                    s = s + " order by PrazoValidade ";
                FillDataSet();                
            }
            catch (Exception ex)
            {
                l_dt = new DataTable();
            }
            finally
            {
                DesconectaBanco();
            }
            return l_dt;
        }
        public int PegaTamanhoCampoVarChar(string pCampo)
        {
            int iRet = 0;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "show columns ";
                s = s + "from LicencaAmbiental ";
                s = s + "where  type like 'varchar%' and field = '" + pCampo + "' ";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                {
                    iRet = Convert.ToInt16(l_dt.Rows[0]["Type"].ToString().Replace("varchar(", "").Replace(")", ""));
                }
                else
                    iRet = 0;
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
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Codigo ";
                s = s + "from   LicencaAmbiental ";
                if (pCodigo != 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " ";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pCodigo != 0)
                    sRet = "Alterar";
                else
                    sRet = "Incluir";
            }
            catch (Exception ex)
            {
                sRet = "Erro: " + ex.Message;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }
        public string DadoExiste(int pCodigoAterro, string pNumeroLicenca)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select CodigoAterro ";
                s = s + "from   LicencaAmbiental ";
                s = s + "where  CodigoAterro = " + pCodigoAterro + " \n";
                s = s + "and    NumeroLicenca = '" + pNumeroLicenca + "' \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pCodigoAterro != 0 && pNumeroLicenca != "")
                    sRet = "Alterar";
                else
                    sRet = "Incluir";
            }
            catch (Exception ex)
            {
                sRet = "Erro: " + ex.Message;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }
        public bool Inserir(clsLicencaAmbiental pLicencaAmbiental, int pCodigo = 0)
        {
            bool _resultado = false;
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            MySqlTransaction transaction;
            ConectaBanco();
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "insert into LicencaAmbiental \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + "Codigo, \n";
                s = s + "  CodigoAterro, CodigoAtividade, NumeroLicenca, Obs, Arquivo, PrazoValidade \n"; 
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + pCodigo.ToString() + ", \n";
                if (pLicencaAmbiental.CodigoAterro.ToString() == "")
                    s = s + " 0, \n";
                else
                    s = s + " " + pLicencaAmbiental.CodigoAterro.ToString().Replace(",", ".") + ", \n";
                s = s + "'" + pLicencaAmbiental.CodigoAtividade + "', \n";
                s = s + "'" + pLicencaAmbiental.NumeroLicenca + "', \n";
                s = s + "'" + pLicencaAmbiental.Obs + "', \n";
                s = s + "'" + pLicencaAmbiental.Arquivo + "', \n";
                if (pLicencaAmbiental.PrazoValidade == "")
                    s = s + "'0001-01-01' \n";
                else
                    s = s + "'" + Convert.ToDateTime(pLicencaAmbiental.PrazoValidade).ToString("yyyy-MM-dd") + "' \n";
                s = s + ")"; 
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
                _resultado = true;
            } 
            catch (Exception er)
            {
                transaction.Rollback();
                _resultado = false;
            }
            finally
            {
                DesconectaBanco();
            }
            return _resultado;
        }
        public string Alterar(clsLicencaAmbiental pLicencaAmbiental, int pCodigo)
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
                s = s + "update LicencaAmbiental \n";
                s = s + "  set CodigoAterro = " + pLicencaAmbiental.CodigoAterro.ToString().Replace(",", ".") + ", \n";
                s = s + "      CodigoAtividade  = '" + pLicencaAmbiental.CodigoAtividade + "', \n";
                s = s + "      NumeroLicenca    = '" + pLicencaAmbiental.NumeroLicenca + "', \n";
                s = s + "      Obs              = '" + pLicencaAmbiental.Obs + "', \n";
                if (pLicencaAmbiental.Arquivo != "" && pLicencaAmbiental.Arquivo != null)
                    s = s + "      Arquivo          = '" + pLicencaAmbiental.Arquivo + "', \n";
                if (pLicencaAmbiental.PrazoValidade != "")
                    s = s + "      PrazoValidade    = '" + Convert.ToDateTime(pLicencaAmbiental.PrazoValidade).ToString("yyyyMMdd") + "' \n";
                s = s + "where Codigo = " + pCodigo.ToString();
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
        public DataTable PreencheDataTableLicencaAmbiental(string pOrdem, int pCodigoDestinoFinal = 0, string pNomeDestinoFinal = "")
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select l.Codigo, l.CodigoAterro, l.CodigoAtividade, l.NumeroLicenca, l.Obs, DATE_FORMAT(PrazoValidade, '%d %m %Y') AS PrazoValidade, \n";
                s = s + "       a.Nome as NomeDestinoFinal, a.LocalAterro, l.Obs as Observacao, Arquivo \n";
                s = s + "from   Aterro a \n";
                s = s + "inner  join LicencaAmbiental l on a.Codigo = l.CodigoAterro \n";
                if (pCodigoDestinoFinal > 0)
                    s = s + "where l.CodigoAterro = " + pCodigoDestinoFinal.ToString() + " \n";
                else if (pNomeDestinoFinal != "")
                    s = s + "where a.Nome = '" + pNomeDestinoFinal + "' \n";
                s = s + "group  by a.Nome, l.NumeroLicenca, a.Codigo \n";
                if (pOrdem == "Codigo asc" || pOrdem == "Codigo")
                    pOrdem = "l.Codigo asc";
                if (pOrdem == "Codigo desc")
                    pOrdem = "l.Codigo desc";
                if (pOrdem != "")
                    s = s + "order  by " + pOrdem;
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                DesconectaBanco();
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

        public string Excluir(int pCodigoAterro, string pCodigoAtividade = "", string pNumeroLicenca = "")
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
                if (pCodigoAterro > 0)
                {
                    s = "";
                    s = s + "delete from LicencaAmbiental \n";
                    s = s + "where  CodigoAterro = " + pCodigoAterro.ToString() + " \n";
                    if (pCodigoAtividade != "")
                        s = s + "and  CodigoAtividade =  '" + pCodigoAtividade + "' \n";
                    if (pNumeroLicenca != "")
                        s = s + "and  NumeroLicenca =  '" + pNumeroLicenca + "' \n";
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
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