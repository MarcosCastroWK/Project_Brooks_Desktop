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
    public class clsCodigoServicosPrefeituraDados
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
        public clsCodigoServicosPrefeitura PegaDados(clsCodigoServicosPrefeitura pCodigoServicosPrefeitura, int pSequencial)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial, Codigo, DescricaoServico, NomePrefeitura \n"; 
                s = s + "from   CodigoServicosPrefeitura \n";
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
                        pCodigoServicosPrefeitura.Sequencial = Convert.ToInt32(l_dt.Rows[0]["Sequencial"]);
                    if (l_dt.Rows[0]["Codigo"].ToString() != "")
                        pCodigoServicosPrefeitura.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                    pCodigoServicosPrefeitura.DescricaoServico = l_dt.Rows[0]["DescricaoServico"].ToString();
                    pCodigoServicosPrefeitura.NomePrefeitura = l_dt.Rows[0]["NomePrefeitura"].ToString();
                }
            }
            catch (Exception ex)
            {
                pCodigoServicosPrefeitura = new clsCodigoServicosPrefeitura();
            }
            finally
            {
                DesconectaBanco();
            }
            return pCodigoServicosPrefeitura;
        }
        
        public DataTable PegaDados(clsCodigoServicosPrefeitura pCodigoServicosPrefeitura, int pSequencial, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial, Codigo, DescricaoServico, NomePrefeitura \n"; 
                s = s + "from   CodigoServicosPrefeitura \n";
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
                    s = s + " order by DescricaoServico ";
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
                s = s + "from CodigoServicosPrefeitura ";
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
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Sequencial ";
                s = s + "from   CodigoServicosPrefeitura ";
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
        public void Inserir(clsCodigoServicosPrefeitura pCodigoServicosPrefeitura, int pSequencial = 0)
        {
            try
            {
                s = "";
                s = s + "insert into CodigoServicosPrefeitura \n";
                s = s + "( \n";
                if (pSequencial > 0)
                    s = s + " Sequencial, \n";
                s = s + "    Codigo, DescricaoServico, NomePrefeitura \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pSequencial > 0)
                    s = s + pSequencial.ToString() + ", \n";
                if (pCodigoServicosPrefeitura.Codigo > 0)
                    s = s + " " + pCodigoServicosPrefeitura.Codigo.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                s = s + " '" + pCodigoServicosPrefeitura.DescricaoServico + "', \n";
                s = s + " '" + pCodigoServicosPrefeitura.NomePrefeitura + "' \n";
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
        public string Alterar(clsCodigoServicosPrefeitura pCodigoServicosPrefeitura, int pSequencial)
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
                s = s + "update CodigoServicosPrefeitura \n";
                if (pCodigoServicosPrefeitura.Codigo > 0)
                    s = s + " set Codigo = " + pCodigoServicosPrefeitura.Codigo.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " set Codigo = 0, \n";
                s = s + " DescricaoServico = '" + pCodigoServicosPrefeitura.DescricaoServico + "', \n";
                s = s + " NomePrefeitura = '" + pCodigoServicosPrefeitura.NomePrefeitura + "' \n";
                
                s = s + " where Sequencial = " + pSequencial + " \n";

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
                s = s + "select Sequencial, Codigo, DescricaoServico, NomePrefeitura \n"; 
                s = s + "from   CodigoServicosPrefeitura \n";
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
                s = s + "select Sequencial, Codigo, DescricaoServico, NomePrefeitura \n";
                s = s + "from   CodigoServicosPrefeitura \n";
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
                if (pSequencial > 0)
                {
                    s = "";
                    s = s + "delete from CodigoServicosPrefeitura ";
                    s = s + "where  Sequencial = " + pSequencial.ToString();
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