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
    public class clsServicosDesenvolvedorDados
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
        public clsServicosDesenvolvedor PegaDados(clsServicosDesenvolvedor pServicosDesenvolvedor, int pCodigo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Servico, Detalhamento, Data, Prioridade, Feito, Solicitante, Executando \n"; 
                s = s + "from   ServicosDesenvolvedor \n";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " \n";
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
                        pServicosDesenvolvedor.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                    pServicosDesenvolvedor.Servico = l_dt.Rows[0]["Servico"].ToString();
                    pServicosDesenvolvedor.Detalhamento = l_dt.Rows[0]["Detalhamento"].ToString();
                    if (l_dt.Rows[0]["Data"].ToString() != "")
                        pServicosDesenvolvedor.Data = Convert.ToDateTime(l_dt.Rows[0]["Data"]).ToString("yyyy-MM-dd");
                    if (l_dt.Rows[0]["Prioridade"].ToString() != "")
                        pServicosDesenvolvedor.Prioridade = Convert.ToInt32(l_dt.Rows[0]["Prioridade"]);
                    if (l_dt.Rows[0]["Feito"].ToString() != "")
                        pServicosDesenvolvedor.Feito = Convert.ToInt32(l_dt.Rows[0]["Feito"]);
                    if (l_dt.Rows[0]["Executando"].ToString() != "")
                        pServicosDesenvolvedor.Executando = Convert.ToInt32(l_dt.Rows[0]["Executando"]);
                    pServicosDesenvolvedor.Solicitante = l_dt.Rows[0]["Solicitante"].ToString();
                }
            }
            catch (Exception ex)
            {
                pServicosDesenvolvedor = new clsServicosDesenvolvedor();
            }
            finally
            {
                DesconectaBanco();
            }
            return pServicosDesenvolvedor;
        }
        
        public DataTable PegaDados(clsServicosDesenvolvedor pServicosDesenvolvedor, int pCodigo, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Servico, Detalhamento, Data, Prioridade, Feito, Solicitante, Executando \n"; 
                s = s + "from   ServicosDesenvolvedor \n";
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
                    s = s + " order by Servico ";
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
                s = s + "from ServicosDesenvolvedor ";
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
                s = s + "from   ServicosDesenvolvedor ";
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
        public void Inserir(clsServicosDesenvolvedor pServicosDesenvolvedor, int pCodigo = 0)
        {
            try
            {
                s = "";
                s = s + "insert into ServicosDesenvolvedor \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + " Codigo, \n";
                s = s + "    Servico, Detalhamento, Data, Prioridade, Feito, Solicitante, Executando \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + pCodigo.ToString() + ", \n";
                s = s + " '" + pServicosDesenvolvedor.Servico + "', \n";
                s = s + " '" + pServicosDesenvolvedor.Detalhamento + "', \n";
                if (pServicosDesenvolvedor.Data != "")
                    s = s + " '" + Convert.ToDateTime(pServicosDesenvolvedor.Data).ToString("yyyy-MM-dd") +"', \n";
                else
                    s = s + " '0001-01-01', \n";
                s = s + "  " + pServicosDesenvolvedor.Prioridade + ", \n";
                s = s + "  " + pServicosDesenvolvedor.Feito + ", \n";
                s = s + " '" + pServicosDesenvolvedor.Solicitante + "', \n";
                s = s + "  " + pServicosDesenvolvedor.Executando.ToString() + " \n";
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
        public string Alterar(clsServicosDesenvolvedor pServicosDesenvolvedor, int pCodigo)
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
                s = s + "update ServicosDesenvolvedor \n";
                s = s + " set Servico = '" + pServicosDesenvolvedor.Servico + "', \n";
                s = s + "     Detalhamento = '" + pServicosDesenvolvedor.Detalhamento + "', \n";
                if (pServicosDesenvolvedor.Data != "")
                    s = s + "     Data = '" + Convert.ToDateTime(pServicosDesenvolvedor.Data).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "     Data = '0001-01-01', \n";
                s = s + "     Prioridade  =  " + pServicosDesenvolvedor.Prioridade + ", \n";
                s = s + "     Feito       =  " + pServicosDesenvolvedor.Feito + ", \n";
                s = s + "     Solicitante = '" + pServicosDesenvolvedor.Solicitante + "', \n";
                s = s + "     Executando  =  " + pServicosDesenvolvedor.Executando.ToString() + " \n";
                s = s + " where Codigo = " + pCodigo + " \n";
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
        public DataTable PreencheDT(string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Servico, Detalhamento, Data, Prioridade, Feito, Solicitante, Executando \n"; 
                s = s + "from   ServicosDesenvolvedor \n";
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

        public DataTable PreencheDT(string pOrdem, string pFiltro, string pCampo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Servico, Detalhamento, Data, Prioridade, Feito, Solicitante, Executando \n";
                s = s + "from   ServicosDesenvolvedor \n";
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

        public string Excluir(int pCodigo)
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
                if (pCodigo > 0)
                {
                    s = s + "delete from ServicosDesenvolvedor ";
                    s = s + "where  Codigo = " + pCodigo.ToString();
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