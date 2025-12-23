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
    public class clsFaturaDados
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
        public clsFatura PegaDados(clsFatura pFatura, int pNumeroFatura)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select NumeroFatura, NumeroLancamento, DataEmissao, CondicaoPagamento \n"; 
                s = s + "from   Fatura \n";
                if (pNumeroFatura > 0)
                {
                    s = s + "where  NumeroFatura = " + pNumeroFatura + " \n";
                    s = s + "limit 1";
                }
                else if (pNumeroFatura == 0)
                {
                    s = s + " order by NumeroFatura limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows[0]["NumeroFatura"].ToString() != "")
                        pFatura.NumeroFatura = Convert.ToInt32(l_dt.Rows[0]["NumeroFatura"]);
                    if (l_dt.Rows[0]["NumeroLancamento"].ToString() != "")
                        pFatura.NumeroLancamento = Convert.ToInt32(l_dt.Rows[0]["NumeroLancamento"]);
                    if (l_dt.Rows[0]["DataEmissao"].ToString() != "")
                        pFatura.DataEmissao = Convert.ToDateTime(l_dt.Rows[0]["DataEmissao"].ToString()).Date.ToShortDateString();
                    pFatura.CondicaoPagamento = l_dt.Rows[0]["CondicaoPagamento"].ToString();
                }                
            }
            catch (Exception ex)
            {
                pFatura = new clsFatura();
            }
            finally
            {
                DesconectaBanco();
            }
            return pFatura;
        }
        
        public DataTable PegaDados(clsFatura pFatura, int pNumeroFatura, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select NumeroFatura, NumeroLancamento, DataEmissao, CondicaoPagamento \n"; 
                s = s + "from   Fatura \n";
                if (pNumeroFatura > 0)
                {
                    s = s + "where  NumeroFatura = " + pNumeroFatura + " ";
                }
                if (pUltimoRegistro)
                {
                    s = s + "order by NumeroFatura desc ";
                }
                else if (pNumeroFatura == 0)
                {
                    s = s + " order by NumeroLancamento ";
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
        public int PegaTamanhoCampoVarChar(string pCampo)
        {
            int iRet = 0;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "show columns ";
                s = s + "from Fatura ";
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
        public string DadoExiste(int pNumeroFatura)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select NumeroFatura ";
                s = s + "from   Fatura ";
                if (pNumeroFatura != 0)
                {
                    s = s + "where  NumeroFatura = " + pNumeroFatura + " ";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pNumeroFatura != 0)
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
        public void Inserir(clsFatura pFatura)
        {
            try
            {
                s = "";
                s = s + "insert into Fatura \n";
                s = s + "( \n";
                s = s + "  NumeroLancamento, DataEmissao, CondicaoPagamento \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pFatura.NumeroLancamento > 0)
                    s = s + " " + pFatura.NumeroLancamento.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pFatura.DataEmissao == "")
                    s = s + "'0001-01-01', ";
                else
                    s = s + "'" + pFatura.DataEmissao + "', ";
                s = s + " '" + pFatura.CondicaoPagamento + "' \n";

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
        public string Alterar(clsFatura pFatura, int pNumeroFatura)
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
                s = s + "update Fatura \n";

                if (pFatura.NumeroLancamento > 0)
                    s = s + " set NumeroLancamento = " + pFatura.NumeroLancamento.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " set NumeroLancamento = 0, \n";
                if (pFatura.DataEmissao == "")
                    s = s + " DataEmissao = '0001-01-01', ";
                else
                    s = s + " DataEmissao = '" + pFatura.DataEmissao + "', ";
                s = s + " ClienteColetado = '" + pFatura.CondicaoPagamento + "' \n";
                
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
        public DataTable PreencheDataTableCacambas(string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select NumeroFatura, NumeroLancamento, DataEmissao, CondicaoPagamento \n"; 
                s = s + "from   Fatura \n";
                s = s + "order by " + pOrdem;
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

        public DataTable PreencheDataTableCacambas(string pOrdem, string pFiltro, string pCampo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select NumeroFatura, NumeroLancamento, DataEmissao, CondicaoPagamento \n";
                s = s + "from   Fatura \n";
                s = s + "where " + pCampo + " like '%" + pFiltro + "%' \n";
                s = s + "order by " + pOrdem +" \n";
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

        public string Excluir(int pNumeroFatura)
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
                if (pNumeroFatura > 0)
                {
                    s = s + "delete from Fatura ";
                    s = s + "where  NumeroFatura = " + pNumeroFatura.ToString();
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