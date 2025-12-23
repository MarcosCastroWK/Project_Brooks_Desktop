using System;
using System.Data;
using MySql.Data.MySqlClient;
using SILCNegocios;

namespace LibSILC
{
    public class clsAvisoInsercaoProgFechadaDados
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
        public clsAvisoInsercaoProgramacaoFechada PegaDados(clsAvisoInsercaoProgramacaoFechada pAvisoInsercaoProgramadaFechada, int pCodigo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Data, DataExecutado, Usuario, Mensagem, Visto \n"; 
                s = s + "from   AvisoInsercaoProgramadaFechada \n";
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
                        pAvisoInsercaoProgramadaFechada.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                    if (l_dt.Rows[0]["Data"].ToString() != "")
                        pAvisoInsercaoProgramadaFechada.Data = l_dt.Rows[0]["Data"].ToString();
                    if (l_dt.Rows[0]["DataExecutado"].ToString() != "")
                        pAvisoInsercaoProgramadaFechada.DataExecutado = l_dt.Rows[0]["DataExecutado"].ToString();
                    pAvisoInsercaoProgramadaFechada.Usuario = l_dt.Rows[0]["Usuario"].ToString();
                    pAvisoInsercaoProgramadaFechada.Mensagem = l_dt.Rows[0]["Mensagem"].ToString();
                    if (l_dt.Rows[0]["Visto"].ToString() != "")
                        pAvisoInsercaoProgramadaFechada.Visto = Convert.ToInt32(l_dt.Rows[0]["Visto"]);
                }
                return pAvisoInsercaoProgramadaFechada;
            }
            catch (Exception ex)
            {
                return new clsAvisoInsercaoProgramacaoFechada();
            }
            finally
            {
                DesconectaBanco();
            }
        }
        
        public DataTable PegaDados(clsAvisoInsercaoProgramacaoFechada pAvisoInsercaoProgramadaFechada, int pCodigo, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Data, DataExecutado, Usuario, Mensagem, Visto \n"; 
                s = s + "from   AvisoInsercaoProgramadaFechada \n";
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
                    s = s + " order by DataExecutado ";
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
                s = s + "from AvisoInsercaoProgramadaFechada ";
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
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Codigo ";
                s = s + "from   AvisoInsercaoProgramadaFechada ";
                if (pCodigo != 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " ";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pCodigo != 0)
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
        public void Inserir(clsAvisoInsercaoProgramacaoFechada pAvisoInsercaoProgramadaFechada, int pCodigo = 0)
        {
            try
            {
                s = "";
                s = s + "insert into AvisoInsercaoProgramadaFechada \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + "  Codigo, \n";
                s = s + "  Data, DataExecutado, Usuario, Mensagem, Visto \n"; 
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + pCodigo.ToString() + ", \n";
                if (pAvisoInsercaoProgramadaFechada.Data != "")
                    s = s + " '" + Convert.ToDateTime(pAvisoInsercaoProgramadaFechada.Data).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + " '0100-01-01', \n";
                if (pAvisoInsercaoProgramadaFechada.DataExecutado != "")
                    s = s + " '" + Convert.ToDateTime(pAvisoInsercaoProgramadaFechada.DataExecutado).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + " '0100-01-01', \n";
                s = s + "'" + pAvisoInsercaoProgramadaFechada.Usuario +  "', \n";
                s = s + "'" + pAvisoInsercaoProgramadaFechada.Mensagem + "', \n";
                if (pAvisoInsercaoProgramadaFechada.Visto > 0)
                    s = s + " " + pAvisoInsercaoProgramadaFechada.Visto.ToString().Replace(",", ".") + " \n";
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
        public string Alterar(clsAvisoInsercaoProgramacaoFechada pAvisoInsercaoProgramadaFechada, int pCodigo)
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
                s = s + "update AvisoInsercaoProgramadaFechada \n";
                if (pAvisoInsercaoProgramadaFechada.Data != "")
                    s = s + "set Data     = '" + Convert.ToDateTime(pAvisoInsercaoProgramadaFechada.Data).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "set Data     = '" + DateTime.Now.ToString("yyyy-MM-dd") + "', \n";
                if (pAvisoInsercaoProgramadaFechada.Data != "")
                    s = s + "set DataExecutado = '" + Convert.ToDateTime(pAvisoInsercaoProgramadaFechada.DataExecutado).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "set DataExecutado = '0100-01-01', \n";
                s = s + " Usuario = " + pAvisoInsercaoProgramadaFechada.Usuario + ", \n";
                s = s + " Mensagem = " + pAvisoInsercaoProgramadaFechada.Mensagem +  ", \n";
                s = s + " Visto = " +  pAvisoInsercaoProgramadaFechada.Visto.ToString() + "\n";
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
        public string SalvarComoVisto(int pCodigo)
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
                s = s + "update AvisoInsercaoProgramadaFechada \n";
                s = s + "set    Visto = 1 \n";
                s = s + "where  Codigo = " + pCodigo + " \n";
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
                s = s + "select Codigo, Data, DataExecutado, Usuario, Mensagem, Visto \n";
                s = s + "from   AvisoInsercaoProgramadaFechada \n";
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
        public DataTable PreencheDataTable(string pOrdem, string pFiltro, string pCampo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Data, DataExecutado, Usuario, Mensagem, Visto \n";
                s = s + "from   AvisoInsercaoProgramadaFechada \n";
                s = s + "where " + pCampo + " like '%" + pFiltro + "%' \n";
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
                if (pCodigo > 0)
                {
                    s = "";
                    s = s + "delete from AvisoInsercaoProgramadaFechada ";
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