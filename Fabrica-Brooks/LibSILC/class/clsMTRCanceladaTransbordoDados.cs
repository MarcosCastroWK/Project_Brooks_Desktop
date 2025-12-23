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
    public class clsMTRCanceladaTransbordoDados
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
        public clsMTRCanceladaTransbordo PegaDados(clsMTRCanceladaTransbordo pMTRCanceladaTransbordo, int pSequencial)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select b.Sequencial, b.Data, b.CodigoMotorista, f.Nome as NomeMotorista, b.NumeroMTR, b.EhTransbordo \n";
                s = s + "from   MTRCancelada b \n";
                s = s + "inner  join Funcionarios f on f.Codigo = b.CodigoMotorista \n ";
                if (pSequencial > 0)
                {
                    s = s + "where  Sequencial = " + pSequencial + " ";
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
                    pMTRCanceladaTransbordo.Sequencial = Convert.ToInt32(l_dt.Rows[0]["Sequencial"]);
                    if (l_dt.Rows[0]["Data"].ToString() != "")
                        pMTRCanceladaTransbordo.Data = Convert.ToDateTime(l_dt.Rows[0]["Data"].ToString()).Date.ToShortDateString();
                    if (l_dt.Rows[0]["CodigoMotorista"].ToString() != "")
                        pMTRCanceladaTransbordo.CodigoMotorista = Convert.ToInt32(l_dt.Rows[0]["CodigoMotorista"]);
                    pMTRCanceladaTransbordo.NomeMotorista = l_dt.Rows[0]["NomeMotorista"].ToString();
                    if (l_dt.Rows[0]["NumeroMTR"].ToString() != "")
                        pMTRCanceladaTransbordo.NumeroMTR = Convert.ToInt32(l_dt.Rows[0]["NumeroMTR"]);
                    if (l_dt.Rows[0]["EhTransbordo"].ToString() != "")
                        pMTRCanceladaTransbordo.EhTransbordo = Convert.ToInt32(l_dt.Rows[0]["EhTransbordo"]);
                }
                
            }
            catch (Exception ex)
            {
                pMTRCanceladaTransbordo = new clsMTRCanceladaTransbordo();
            }
            finally
            {
                DesconectaBanco();
            }
            return pMTRCanceladaTransbordo;
        }
        
        public DataTable PegaDados(clsMTRCanceladaTransbordo pMTRCanceladaTransbordo, int pSequencial, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select b.Sequencial, b.Data, b.CodigoMotorista, f.Nome as NomeMotorista, b.NumeroMTR, b.EhTransbordo \n";
                s = s + "from   MTRCancelada b \n";
                s = s + "inner  join Funcionarios f on f.Codigo = b.CodigoMotorista \n ";
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
                    s = s + " order by NomeMotorista ";
                }
                s = s + " limit 1000";
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
                s = s + "from MTRCancelada ";
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
        public string DadoExiste(int pSequencial)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Sequencial ";
                s = s + "from   MTRCancelada ";
                if (pSequencial != 0)
                {
                    s = s + "where  Sequencial = " + pSequencial + " ";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pSequencial != 0)
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
        public void Inserir(clsMTRCanceladaTransbordo pMTRCanceladaTransbordo)
        {
            try
            {
                s = "";
                s = s + "insert into MTRCancelada \n";
                s = s + "( \n";
                s = s + "   Data, CodigoMotorista, NumeroMTR, EhTransbordo \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";

                if (pMTRCanceladaTransbordo.Data != "")
                    s = s + "'" + Convert.ToDateTime(pMTRCanceladaTransbordo.Data).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";

                if (pMTRCanceladaTransbordo.CodigoMotorista > 0)
                    s = s + " " + pMTRCanceladaTransbordo.CodigoMotorista.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";

                if (pMTRCanceladaTransbordo.NumeroMTR == 0)
                    s = s + " 0, \n";
                else
                    s = s + " " + pMTRCanceladaTransbordo.NumeroMTR.ToString().Replace(",", ".") + ", \n";
                
                if (pMTRCanceladaTransbordo.EhTransbordo == 0)
                    s = s + " 0 \n";
                else
                    s = s + " " + pMTRCanceladaTransbordo.EhTransbordo + " \n";

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
        public string Alterar(clsMTRCanceladaTransbordo pMTRCanceladaTransbordo, int pSequencial)
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
                s = s + "update MTRCancelada \n";
                s = s + "set   Data  = '" + Convert.ToDateTime(pMTRCanceladaTransbordo.Data).ToString("yyyy-MM-dd") + "', \n";
                if (pMTRCanceladaTransbordo.CodigoMotorista == 0)
                    s = s + "  CodigoMotorista = 0, \n";
                else             
                    s = s + "  CodigoMotorista       = " + pMTRCanceladaTransbordo.CodigoMotorista.ToString().Replace(",", ".") + ", \n";
                if (pMTRCanceladaTransbordo.NumeroMTR == 0)
                    s = s + "  NumeroMTR = 0, \n";
                else
                    s = s + "  NumeroMTR = " + pMTRCanceladaTransbordo.NumeroMTR.ToString().Replace(",", ".") + ", \n";
                if (pMTRCanceladaTransbordo.EhTransbordo == 0)
                    s = s + "      EhTransbordo = 0 \n";
                else
                    s = s + "      EhTransbordo = " + pMTRCanceladaTransbordo.EhTransbordo + " \n";
                s = s + "where Sequencial = " + pSequencial.ToString();
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
        public DataTable PreencheDataTableMTRCanceladaTransbordo(string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select b.Sequencial, b.Data, b.CodigoMotorista, f.Nome as NomeMotorista, b.NumeroMTR, b.EhTransbordo \n";
                s = s + "from   MTRCancelada b \n";
                s = s + "inner  join Funcionarios f on f.Codigo = b.CodigoMotorista \n ";
                s = s + "order by " + pOrdem;
                s = s + " limit 1000";
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

        public DataTable PreencheDataTableMTRCanceladaTransbordo(string pOrdem, string pFiltro, string pCampo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select b.Sequencial, b.Data, b.CodigoMotorista, f.Nome as NomeMotorista, b.NumeroMTR, b.EhTransbordo \n";
                s = s + "from   MTRCancelada b \n";
                s = s + "inner  join Funcionarios f on f.Codigo = b.CodigoMotorista \n "; 
                s = s + "where " + pCampo + " like '%" + pFiltro + "%' \n";
                s = s + "order by " + pOrdem +" \n";
                s = s + " limit 1000";
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

        public string Excluir(int pSequencial)
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
                s = s + "delete from MTRCancelada ";
                s = s + "where  Sequencial = " + pSequencial.ToString();
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