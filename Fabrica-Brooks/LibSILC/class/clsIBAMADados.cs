using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data.OleDb;
using SILCNegocios;

namespace LibSILC
{
    public class clsIBAMADados
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
        public clsIBAMA PegaDados(clsIBAMA pIBAMA, int pCodigo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Descricao, Ativo, CodigoIBAMA ";
                s = s + "from   IBAMA ";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " ";
                    s = s + "limit 1";
                }
                else if (pCodigo == 0)
                {
                    s = s + " order by Descricao limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    pIBAMA.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                    if (l_dt.Rows[0]["Ativo"].ToString() != "")
                        pIBAMA.Ativo = Convert.ToInt16(l_dt.Rows[0]["Ativo"].ToString());
                    pIBAMA.Descricao = l_dt.Rows[0]["Descricao"].ToString();
                    pIBAMA.CodigoIBAMA = l_dt.Rows[0]["CodigoIBAMA"].ToString();
                }
                
            }
            catch (Exception ex)
            {
                pIBAMA = new clsIBAMA();
            }
            finally
            {
                DesconectaBanco();
            }
            return pIBAMA;
        }
        public string PegaDados(string pCodigoIBAMA)
        {
            string sRet = "";
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Descricao, Ativo, CodigoIBAMA \n";
                s = s + "from   IBAMA \n";
                s = s + "where  CodigoIBAMA = '" + pCodigoIBAMA + "' \n";
                s = s + "limit  1 \n";
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    sRet = l_dt.Rows[0]["Descricao"].ToString();
                }
            }
            catch (Exception ex)
            {
                sRet = "";
            }
            finally
            {
                DesconectaBanco();
            }
            return sRet;;
        }
        public string PegaCodigoIbama(string pGrupo)
        {
            string sRet = "";
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select CodigoIBAMA \n";
                s = s + "from   IBAMA \n";
                s = s + "where  Grupo = '" + pGrupo + "' \n";
                s = s + "limit  1 \n";
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    sRet = l_dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                sRet = "";
            }
            finally
            {
                DesconectaBanco();
            }
            return sRet; 
        }
        public DataTable PegaDados(clsIBAMA pIBAMA, int pCodigo, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select  Codigo, Descricao, Ativo, CodigoIBAMA ";
                s = s + "from   IBAMA ";
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
                    s = s + " order by Descricao ";
                }
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
        public DataTable PreencheDataTableOrdem(string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Descricao, Ativo, CodigoIBAMA ";
                s = s + "from   IBAMA \n";
                if (pOrdem == "Descrição")
                    pOrdem = "Descricao";
                s = s + "order by " + pOrdem + " \n";
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
        public int PegaTamanhoCampoVarChar(string pCampo)
        {
            int iRet = 0;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "show columns ";
                s = s + "from IBAMA ";
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
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Codigo ";
                s = s + "from   IBAMA ";
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
        public void Inserir(clsIBAMA pIBAMA)
        {
            try
            {
                s = "";
                s = s + "insert into IBAMA \n";
                s = s + "( \n";
                s = s + "   Descricao, Ativo, CodigoIBAMA \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                s = s + "'" + pIBAMA.Descricao + "', \n";
                if (pIBAMA.Ativo.ToString() != "")
                    s = s + " " + pIBAMA.Ativo.ToString() + ", \n";
                else
                    s = s + "0, \n";
                if (pIBAMA.CodigoIBAMA == null)
                    s = s + " 0 \n";
                else
                    s = s + " '" + pIBAMA.CodigoIBAMA.ToString() + "' \n";
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
        public string Alterar(clsIBAMA pIBAMA, int pCodigo)
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
                s = s + "update IBAMA \n";
                s = s + "set   Descricao   = '" + pIBAMA.Descricao + "', \n";
                s = s + "      Ativo       = " + pIBAMA.Ativo.ToString() + ", \n";
                if (pIBAMA.CodigoIBAMA == null)
                    s = s + "      CodigoIBAMA = '0', \n";
                else
                    s = s + "      CodigoIBAMA = '" + pIBAMA.CodigoIBAMA.ToString() + "' \n";
                s = s + "where Codigo = " + pCodigo.ToString() ;
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
        public string Excluir(int pCodigo, string pDescricao)
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
                s = s + "delete from IBAMA ";
                if (pCodigo > 0)
                {
                    s = s + "where Codigo = " + pCodigo.ToString();
                }
                else
                {
                    s = s + "where Descricao = '" + pDescricao + "'";
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
        public DataTable PreencheDataTableFiltro(string pOrdem, string pFiltro, string pCampo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Ativo, Descricao, CodigoIBAMA \n";
                s = s + "from   IBAMA \n";
                if (pCampo == "Descrição") pCampo = "Descricao";
                if (pOrdem == "Descrição") pOrdem = "Descricao";
                s = s + "where " + pCampo + " like '%" + pFiltro + "%' \n";
                s = s + "order by " + pOrdem + " \n";
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
    }
}