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
    public class clsObjetosDados
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
        public clsObjetos PegaDados(clsObjetos pObjetos, int pSequencial)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial, Objetos, NomeObjeto  \n"; 
                s = s + "from   Objetos \n";
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
                        pObjetos.Sequencial = Convert.ToInt32(l_dt.Rows[0]["Sequencial"]);
                    pObjetos.Objetos = l_dt.Rows[0]["Objetos"].ToString();
                    pObjetos.NomeObjeto = l_dt.Rows[0]["NomeObjeto"].ToString();

                }
                return pObjetos;
            }
            catch (Exception ex)
            {
                return new clsObjetos();
            }
            finally
            {
                DesconectaBanco();
            }
        }
        
        public DataTable PegaDados(clsObjetos pObjetos, int pSequencial, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial, Objetos, NomeObjeto  \n"; 
                s = s + "from   Objetos \n";
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
                    s = s + " order by Objetos ";
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
        public int PegaTamanhoObjetosVarChar(string pObjetos)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "show columns ";
                s = s + "from Objetos ";
                s = s + "where  type like 'varchar%' and field = '" + pObjetos + "' ";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                {
                    return Convert.ToInt16(l_dt.Rows[0]["Type"].ToString().Replace("varchar(", "").Replace(")", ""));
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public string DadoExiste(int pSequencial)
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Sequencial ";
                s = s + "from   Objetos ";
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
        public void Inserir(clsObjetos pObjetos)
        {
            try
            {
                s = "";
                s = s + "insert into Objetos \n";
                s = s + "( \n";
                s = s + " Objetos, NomeObjeto  \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                s = s + " '" + pObjetos.Objetos + "', \n";
                s = s + " '" + pObjetos.NomeObjeto + "' \n";
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
        public string Alterar(clsObjetos pObjetos, int pSequencial)
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
                s = s + "update Objetos \n";
                
                s = s + "set Objetos = '" + pObjetos.Objetos + "', \n";
                s = s + "  NomeObjeto = '" + pObjetos.NomeObjeto + "' \n";
                
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
                s = s + "select Sequencial, Objetos, NomeObjeto  \n";
                s = s + "from   Objetos \n";
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

        public DataTable PreencheDataTable(string pOrdem, string pFiltro, string pObjetos)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial, Objetos, NomeObjeto  \n";
                s = s + "from   Objetos \n";
                s = s + "where " + pObjetos + " like '" + pFiltro + "%' \n";
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
                s = "";
                if (pSequencial > 0)
                {
                    s = s + "delete from Objetos ";
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