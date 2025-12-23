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
    public class clsItensMenuDados
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

        public void InserirItensMenu()
        {
            // criar tabela primeiro
        }
        public clsItensMenu PegaDados(clsItensMenu pItensMenu, int pCodigo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Item, Ativo \n"; 
                s = s + "from   ItensMenu \n";
                if (pCodigo > 0)
                {
                    s = s + "where Codigo = " + pCodigo + " \n";
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
                        pItensMenu.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                    pItensMenu.Item = l_dt.Rows[0]["Item"].ToString();
                    if (l_dt.Rows[0]["Ativo"].ToString() != "")
                        pItensMenu.Ativo = Convert.ToInt32(l_dt.Rows[0]["Ativo"]);
                }
                return pItensMenu;
            }
            catch (Exception ex)
            {
                return new clsItensMenu();
            }
            finally
            {
                DesconectaBanco();
            }
        }
        
        public DataTable PegaDados(clsItensMenu pItensMenu, int pCodigo, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Item, Ativo \n"; 
                s = s + "from   ItensMenu \n";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " \n";
                }
                if (pUltimoRegistro)
                {
                    s = s + "order by Codigo desc \n";
                }
                else if (pCodigo == 0)
                {
                    s = s + " order by Item \n";
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
        public int PegaTamanhoItemVarChar(string pItem)
        {
            int iRet = 0;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "show  columns ";
                s = s + "from  ItensMenu ";
                s = s + "where type like 'varchar%' and field = '" + pItem + "' ";
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
                s = s + "select Codigo \n";
                s = s + "from   ItensMenu \n";
                if (pCodigo != 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " \n";
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
        public void Inserir(clsItensMenu pItensMenu, int pCodigo = 0)
        {
            try
            {
                s = "";
                s = s + "insert into ItensMenu \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + "  Codigo, \n";
                s = s + "  Item, Ativo \n"; 
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + pCodigo.ToString() + ", \n";
                s = s + " '" + pItensMenu.Item + "', \n";
                if (pItensMenu.Ativo > 0)
                    s = s + " " + pItensMenu.Ativo.ToString().Replace(",", ".") + " \n";
                else
                    s = s + "0 \n";
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
        public string Alterar(clsItensMenu pItensMenu, int pCodigo)
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
                s = s + "update ItensMenu \n";
                
                s = s + "set Item = '" + pItensMenu.Item + "', \n";

                if (pItensMenu.Ativo > 0)
                    s = s + " Ativo = " + pItensMenu.Ativo.ToString().Replace(",", ".") + " \n";
                else
                    s = s + " Ativo = 0 \n";
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
        public DataTable PreencheDataTable(string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Item, Ativo \n";
                s = s + "from   ItensMenu \n";
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

        public DataTable PreencheDataTable(string pOrdem, string pFiltro, string pCampoWhere)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Item, Ativo \n";
                s = s + "from   ItensMenu \n";
                s = s + "where " + pCampoWhere + " like '%" + pFiltro + "%' \n";
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
                s = "";
                if (pCodigo > 0)
                {
                    s = s + "delete from ItensMenu ";
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