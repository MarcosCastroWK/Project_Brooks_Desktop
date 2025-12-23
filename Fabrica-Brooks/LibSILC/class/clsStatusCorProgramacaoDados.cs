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
    public class clsStatusCorProgramacaoDados
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
        public clsStatusCorProgramacao PegaDados(clsStatusCorProgramacao pStatusCorProgramacao, int pSequencialProgramacao)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, SequencialProgramacao, StatusCor \n"; 
                s = s + "from   StatusCorProgramacao \n";
                if (pSequencialProgramacao > 0)
                {
                    s = s + "where  SequencialProgramacao = " + pSequencialProgramacao + " \n";
                    s = s + "order  by Codigo desc  limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows[0]["Codigo"].ToString() != "")
                        pStatusCorProgramacao.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                    if (l_dt.Rows[0]["SequencialProgramacao"].ToString() != "")
                        pStatusCorProgramacao.SequencialProgramacao = Convert.ToInt32(l_dt.Rows[0]["SequencialProgramacao"]);
                    pStatusCorProgramacao.StatusCor = l_dt.Rows[0]["StatusCor"].ToString();
                }
            }
            catch (Exception ex)
            {
                pStatusCorProgramacao = new clsStatusCorProgramacao();
            }
            finally
            {
                DesconectaBanco();
            }
            return pStatusCorProgramacao;
        }
        
        public DataTable PegaDados(clsStatusCorProgramacao pStatusCorProgramacao, int pCodigo, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, SequencialProgramacao, StatusCor \n";
                s = s + "from   StatusCorProgramacao \n";
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
                s = s + "from StatusCorProgramacao ";
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
                s = s + "from   StatusCorProgramacao ";
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
        public void Inserir(clsStatusCorProgramacao pStatusCorProgramacao)
        {
            try
            {
                s = "";
                s = s + "insert into StatusCorProgramacao \n";
                s = s + "( \n";
                s = s + "  SequencialProgramacao, StatusCor \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pStatusCorProgramacao.SequencialProgramacao > 0)
                    s = s + " " + pStatusCorProgramacao.SequencialProgramacao.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0 \n";
                s = s + " '" + pStatusCorProgramacao.StatusCor + "' \n";
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
        public string Alterar(clsStatusCorProgramacao pStatusCorProgramacao, int pSequencialProgramacao)
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
                s = s + "update StatusCorProgramacao \n";
                s = s + "set    StatusCor = '" + pStatusCorProgramacao.StatusCor + "' \n";
                s = s + "where  SequencialProgramacao = " + pSequencialProgramacao + " \n";
                s = s + "order  by Codigo desc limit 1 \n";
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
        public DataTable PreencheDataTable(string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select SequencialProgramacao, StatusCor \n";
                s = s + "from   StatusCorProgramacao \n";
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
                s = s + "select SequencialProgramacao, StatusCor \n";
                s = s + "from   StatusCorProgramacao \n";
                s = s + "where " + pCampo + " like '" + pFiltro + "%' \n";
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

        public string Excluir(int pCodigo)
        {
            string sRet = "";
            if (pCodigo == 0)
                return "Código inválido!";
            else
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
                    s = s + "delete from StatusCorProgramacao ";
                    s = s + "where  Codigo = " + pCodigo.ToString();
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
}