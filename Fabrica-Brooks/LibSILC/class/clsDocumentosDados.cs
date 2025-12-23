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
    public class clsDocumentosPaginaDados
	{
	    private clsDB oDB = new clsDB();
        private MySqlDataAdapter l_myData;
        private DataSet l_ds = new DataSet();
        DataTable l_dt = new DataTable();
        private string s;

        // funções locais para multi banco de dados Host Gator
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
        public clsDocumentosPagina PegaDados(clsDocumentosPagina pDocumentosPagina, int pId)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Id, Tipo, Descricao, BROOKS, Periodo \n"; 
                s = s + "from   Documentos \n";
                if (pId > 0)
                {
                    s = s + "where  Id = " + pId + " \n";
                    s = s + "limit 1";
                }
                else if (pId == 0)
                {
                    s = s + " order by Id limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows[0]["Id"].ToString() != "")
                        pDocumentosPagina.Id = Convert.ToInt32(l_dt.Rows[0]["Id"]);
                    pDocumentosPagina.Tipo = l_dt.Rows[0]["Tipo"].ToString();
                    pDocumentosPagina.Descricao = l_dt.Rows[0]["Descricao"].ToString();
                    if (l_dt.Rows[0]["BROOKS"].ToString() == "S")
                        pDocumentosPagina.BROOKS = 1;
                    else
                        pDocumentosPagina.BROOKS = 0;
                    pDocumentosPagina.Periodo = l_dt.Rows[0]["Periodo"].ToString();
                }
                return pDocumentosPagina;
            }
            catch (Exception ex)
            {
                return new clsDocumentosPagina();
            }
            finally
            {
                DesconectaBanco();
            }
        }
        
        public DataTable PegaDados(clsDocumentosPagina pDocumentosPagina, int pId, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Id, Tipo, Descricao, BROOKS, Periodo \n";
                s = s + "from   Documentos \n";
                if (pId > 0)
                {
                    s = s + "where  Id = " + pId + " ";
                }
                if (pUltimoRegistro)
                {
                    s = s + "order by Id desc ";
                }
                else if (pId == 0)
                {
                    s = s + " order by Tipo ";
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
        public int PegaTamanhoTipoVarChar(string pTipo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "show  columns ";
                s = s + "from  Documentos ";
                s = s + "where type like 'varchar%' and field = '" + pTipo + "' ";
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
        public string DadoExiste(int pId)
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Id ";
                s = s + "from   Documentos ";
                if (pId != 0)
                {
                    s = s + "where  Id = " + pId + " ";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pId != 0)
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
        public void Inserir(clsDocumentosPagina pDocumentosPagina)
        {
            try
            {
                s = "";
                s = s + "insert into Documentos \n";
                s = s + "( \n";
                s = s + " Tipo, Descricao, BROOKS, Periodo \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                s = s + " '" + pDocumentosPagina.Tipo + "', \n";
                s = s + " '" + pDocumentosPagina.Descricao + "', \n";
                if (pDocumentosPagina.BROOKS == 1)
                    s = s + " 'S', \n";
                else
                    s = s + " 'N', \n";
                s = s + " '" + pDocumentosPagina.Periodo + "' \n";
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
        public string Alterar(clsDocumentosPagina pDocumentosPagina, int pId)
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
                s = s + "update Documentos \n";
                
                s = s + "set Tipo = '" + pDocumentosPagina.Tipo + "', \n";
                s = s + "    Descricao = '" + pDocumentosPagina.Descricao + "', \n";
                if (pDocumentosPagina.BROOKS == 1)
                    s = s + " BROOKS = 'S', \n";
                else
                    s = s + " BROOKS = 'N', \n";
                s = s + " Periodo = '" + pDocumentosPagina.Periodo + "' \n";
                s = s + "where Id = " + pId + " \n";        
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
                s = s + "select Id, Tipo, Descricao, BROOKS, Periodo \n";
                s = s + "from   Documentos \n";
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

        public DataTable PreencheDataTable(string pOrdem, string pFiltro, string pTipo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Id, Tipo, Descricao, BROOKS, Periodo \n";
                s = s + "from   Documentos \n";
                s = s + "where " + pTipo + " like '%" + pFiltro + "%' \n";
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

        public string Excluir(int pId)
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
                if (pId > 0)
                {
                    s = s + "delete from Documentos ";
                    s = s + "where  Id = " + pId.ToString();
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