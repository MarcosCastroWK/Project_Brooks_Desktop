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
    public class clsBloqFinanceiroDados
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
        public clsBloqueioFinanceiro PegaDados(clsBloqueioFinanceiro pBloqueioFinanceiro, int pSequencial)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select b.Sequencial, b.CodigoCliente, b.CodigoUsuario, b.DataBloqueio, b.DataDesbloqueio, b.Observacao, \n ";
                s = s + "       c.NomeFantasia as NomeFantasiaCliente,  u.Nome as NomeUsuario \n";
                s = s + "from   BloqueioFinanceiro b \n ";
                s = s + "inner  join Clientes c on c.Codigo = b.CodigoCliente  \n";
                s = s + "left   join Usuarios u on u.Codigo = b.CodigoUsuario \n";
                if (pSequencial > 0)
                {
                    s = s + "where  b.Sequencial = " + pSequencial + " ";
                    s = s + "limit 1";
                }
                else if (pSequencial == 0)
                {
                    s = s + " order by b.Sequencial limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    pBloqueioFinanceiro.CodigoCliente = Convert.ToInt32(l_dt.Rows[0]["CodigoCliente"]);
                    pBloqueioFinanceiro.NomeFantasiaCliente = l_dt.Rows[0]["NomeFantasiaCliente"].ToString();
                    pBloqueioFinanceiro.NomeUsuario = l_dt.Rows[0]["NomeUsuario"].ToString();
                    pBloqueioFinanceiro.CodigoUsuario = Convert.ToInt32(l_dt.Rows[0]["CodigoUsuario"]);
                    if (l_dt.Rows[0]["DataBloqueio"].ToString() != "")
                        pBloqueioFinanceiro.DataBloqueio = Convert.ToDateTime(l_dt.Rows[0]["DataBloqueio"].ToString()).Date.ToShortDateString();
                    if (l_dt.Rows[0]["DataDesbloqueio"].ToString() != "")
                        pBloqueioFinanceiro.DataDesbloqueio = Convert.ToDateTime(l_dt.Rows[0]["DataDesbloqueio"].ToString()).Date.ToShortDateString();
                    pBloqueioFinanceiro.Observacao = l_dt.Rows[0]["Observacao"].ToString();
                }
                return pBloqueioFinanceiro;
            }
            catch (Exception ex)
            {
                return new clsBloqueioFinanceiro();
            }
            finally
            {
                DesconectaBanco();
            }
        }

        public DataTable PegaDados(clsBloqueioFinanceiro pBloqueioFinanceiro, int pSequencial, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select b.Sequencial, b.CodigoCliente, b.CodigoUsuario, b.DataBloqueio, b.DataDesbloqueio, b.Observacao, \n ";
                s = s + "       c.NomeFantasia as NomeFantasiaCliente,  u.Nome as NomeUsuario \n";
                s = s + "from   BloqueioFinanceiro b \n ";
                s = s + "inner  join Clientes c on c.Codigo = b.CodigoCliente  \n";
                s = s + "left   join Usuarios u on u.Codigo = b.CodigoUsuario \n";
                if (pSequencial > 0)
                {
                    s = s + "where  b.Sequencial = " + pSequencial + " ";
                }
                if (pUltimoRegistro)
                {
                    s = s + "order by b.Sequencial desc ";
                }
                else if (pSequencial == 0)
                {
                    s = s + " order by b.CodigoCliente ";
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
        public int PegaTamanhoCampoVarChar(string pCampo)
        {
            int iRet = 0;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "show columns ";
                s = s + "from BloqueioFinanceiro ";
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
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial ";
                s = s + "from   BloqueioFinanceiro  ";
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
                DesconectaBanco();
            }
        }
        public bool ExisteBloqueio(int pCodigoCliente)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select DataBloqueio \n";
                s = s + "from   BloqueioFinanceiro \n ";
                if (pCodigoCliente != 0)
                {
                    s = s + "where CodigoCliente = " + pCodigoCliente + " \n";
                    s = s + "and   (DataDesbloqueio is null or DataDesbloqueio <= '0100-01-01') \n";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch 
            {
                return false;
            }
            finally
            {
                DesconectaBanco();
            }
        }

        public string PegaDataUltimoBloqueioFinanceiro(int pCodigoCliente)
        {
            string sRet = "";
            try
            {
                ConectaBanco();
                if (pCodigoCliente > 0)
                {
                    s = "";
                    s = s + "select DataBloqueio \n";
                    s = s + "from   BloqueioFinanceiro \n ";
                    s = s + "where CodigoCliente = " + pCodigoCliente + " \n";
                    s = s + "and   (DataDesbloqueio is null or DataDesbloqueio <= '0100-01-01') \n";
                    s = s + "limit 1 \n";
                    FillDataSet();
                    if (l_dt.Rows.Count > 0)
                        sRet = l_dt.Rows[0][0].ToString();
                }
            }
            catch
            {
                sRet = "";
            }
            finally
            {
                DesconectaBanco();
            }
            return sRet;
        }


        public void Inserir(clsBloqueioFinanceiro pBloqueioFinanceiro, int pSequencial = 0)
        {
            try
            {
                s = "";
                s = s + "insert into BloqueioFinanceiro \n";
                s = s + "( \n";
                if (pSequencial > 0)
                    s = s + " Sequencial, \n";
                s = s + "   CodigoCliente, CodigoUsuario, DataBloqueio, DataDesbloqueio, Observacao  \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pSequencial > 0)
                    s = s + pSequencial.ToString() + ", \n";
                if (pBloqueioFinanceiro.CodigoCliente > 0)
                    s = s + " " + pBloqueioFinanceiro.CodigoCliente.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pBloqueioFinanceiro.CodigoUsuario > 0)
                    s = s + " " + pBloqueioFinanceiro.CodigoUsuario.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pBloqueioFinanceiro.DataBloqueio != "")
                    s = s + "'" + Convert.ToDateTime(pBloqueioFinanceiro.DataBloqueio).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                if (pBloqueioFinanceiro.DataDesbloqueio != "")
                    s = s + "'" + Convert.ToDateTime(pBloqueioFinanceiro.DataDesbloqueio).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                s = s + "'" + pBloqueioFinanceiro.Observacao + "' \n";
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
        public string Alterar(clsBloqueioFinanceiro pBloqueioFinanceiro, int pSequencial)
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
                s = s + "update BloqueioFinanceiro \n";
                if (pBloqueioFinanceiro.CodigoCliente > 0)
                    s = s + "set CodigoCliente = " + pBloqueioFinanceiro.CodigoCliente.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "    CodigoCliente = 0, \n";
                if (pBloqueioFinanceiro.CodigoUsuario > 0)
                    s = s + "    CodigoUsuario = " + pBloqueioFinanceiro.CodigoUsuario.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "    CodigoUsuario = 0, \n";
                if (pBloqueioFinanceiro.DataBloqueio != "")
                    s = s + "    DataBloqueio  = '" + Convert.ToDateTime(pBloqueioFinanceiro.DataBloqueio).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "    DataBloqueio  = '0001-01-01', \n";
                if (pBloqueioFinanceiro.DataDesbloqueio != "")
                    s = s + "    DataDesbloqueio = '" + Convert.ToDateTime(pBloqueioFinanceiro.DataDesbloqueio).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "    DataDesbloqueio = '0001-01-01', \n";
                s = s + "    Observacao = '" + pBloqueioFinanceiro.Observacao + "' \n";                
                s = s + "where Sequencial = " + pSequencial.ToString();

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
        public DataTable PreencheDataTableBloqueioFinanceiro(string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select b.Sequencial, b.CodigoCliente, b.CodigoUsuario, b.DataBloqueio, b.DataDesbloqueio, b.Observacao, \n ";
                s = s + "       c.NomeFantasia as NomeFantasiaCliente, u.Nome as NomeUsuario \n";
                s = s + "from   BloqueioFinanceiro b \n ";
                s = s + "left   join Clientes c on c.Codigo = b.CodigoCliente  \n";
                s = s + "left   join Usuarios u on u.Codigo = b.CodigoUsuario \n";
                if (pOrdem.Length > 0)
                    s = s + "order  by " + pOrdem + " \n";
                else
                    s = s + "order  by b.CodigoCliente asc \n";
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
        public DataTable PreencheDataTableBloqueioFinanceiroInClientes(string pCodigosInClientes)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select b.Sequencial, b.CodigoCliente, b.CodigoUsuario, b.DataBloqueio, b.DataDesbloqueio, b.Observacao, \n ";
                s = s + "       c.NomeFantasia as NomeFantasiaCliente,  u.Nome as NomeUsuario \n";
                s = s + "from   BloqueioFinanceiro b \n ";
                s = s + "left   join Clientes c on c.Codigo = b.CodigoCliente  \n";
                s = s + "left   join Usuarios u on u.Codigo = b.CodigoUsuario \n";
                s = s + "where  b.CodigoCliente in (" + pCodigosInClientes + ") \n";
                s = s + "and    (b.DataDesbloqueio is null or year(b.DataDesbloqueio) < 1901) \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                DesconectaBanco();
                return l_dt;
            }
        }
        public DataTable PreencheDataTableBloqueioFinanceiroClientes(string pOrdem, int pCodigoCliente)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select b.DataBloqueio as 'Data Bloqueio', b.DataDesbloqueio as 'Data Desbloqueio', b.Observacao as 'Observação', \n ";
                s = s + "       c.NomeFantasia as 'Nome Fantasia', u.Nome as 'Nome Usuário' \n";
                s = s + "from   BloqueioFinanceiro b \n ";
                s = s + "left   join Clientes c on c.Codigo = b.CodigoCliente  \n";
                s = s + "left   join Usuarios u on u.Codigo = b.CodigoUsuario \n";
                s = s + "where  c.Codigo = " + pCodigoCliente.ToString() + " \n";
                s = s + "order  by b.DataBloqueio desc \n";
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
        public DataTable PreencheDataTableParaRelatorio(string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select c.Codigo as 'Código', c.NomeFantasia as 'Nome Fantasia', c.Nome as 'Nome/Razão Social', b.DataBloqueio as 'Data Bloqueio' \n ";
                s = s + "from   BloqueioFinanceiro b \n ";
                s = s + "inner  join Clientes c on c.Codigo = b.CodigoCliente  \n";
                s = s + "where  b.DataBloqueio <> '0100-01-01' \n";
                s = s + "and    (b.DataDesbloqueio = '0100-01-01' or b.DataDesbloqueio is null) \n"; 
                if (pOrdem.Length > 0)
                    s = s + "order  by " + pOrdem + " \n";
                else
                    s = s + "order  by b.CodigoCliente asc \n";
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
        public DataTable PreencheDataTableBloqueioFinanceiro(string pOrdem, string pFiltro, string pCampo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select b.Sequencial, b.CodigoCliente, b.CodigoUsuario, b.DataBloqueio, b.DataDesbloqueio, b.Observacao, \n ";
                s = s + "       c.NomeFantasia as NomeFantasiaCliente,  u.Nome as NomeUsuario \n";
                s = s + "from   BloqueioFinanceiro b \n ";
                s = s + "left   join Clientes c on c.Codigo = b.CodigoCliente  \n";
                s = s + "left   join Usuarios u on u.Codigo = b.CodigoUsuario \n";
                string _ascdesc = " asc ";
                if (pOrdem.PadRight(3).ToUpper() == "DESC")
                    _ascdesc = " desc ";
                if (pOrdem.ToUpper().Substring(0, 10) == "BLOQUEADOS")
                    pOrdem = "b.DataBloqueio " + _ascdesc;
                if (pOrdem.Length >= 21)
                    if (pOrdem.ToUpper().Substring(0, 21) == "Nome Fantasia Cliente".ToUpper())
                        pOrdem = "c.NomeFantasia" + _ascdesc;
                if (pCampo.ToUpper().Substring(0, 10) == "BLOQUEADOS")
                    pCampo = "b.DataBloqueio";
                if (pCampo.Length >= 21)
                    if (pCampo.ToUpper().Substring(0, 21) == "Nome Fantasia Cliente".ToUpper())
                        pCampo = "c.NomeFantasia";
                s = s + "where " + pCampo + " like '%" + pFiltro + "%' \n";
                if (pCampo.ToUpper() == "b.DataBloqueio".ToUpper())
                    s = s + "and  (b.DataDesbloqueio = '0001-01-01' or b.DataDesbloqueio = '0100-01-01' or b.DataDesbloqueio is null)";
                s = s + "order by " + pOrdem + " \n";
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

        public string Excluir(int pSequencial, string pTipo)
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
                    s = s + "delete from BloqueioFinanceiro ";
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
        public string Excluir(int pCodigoCliente)
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
                s = s + "delete from BloqueioFinanceiro ";
                s = s + "where  CodigoCliente = " + pCodigoCliente.ToString();
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
