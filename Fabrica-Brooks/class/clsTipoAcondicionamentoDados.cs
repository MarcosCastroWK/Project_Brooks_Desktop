using System;
using System.Data;
using MySql.Data.MySqlClient;
using SILCNegocios;

namespace LibSILC
{
	public class clsTipoAcondicionamentoDados
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
        public clsTipoAcondicionamento PegaDados(clsTipoAcondicionamento pTipoAcondicionamento, int pCodigo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select ta.Codigo, ta.Descricao \n";
                s = s + "from   TipoAcondicionamento ta \n";
                if (pCodigo > 0)
                {
                    s = s + "where  ta.Codigo = " + pCodigo + " ";
                    s = s + "limit 1";
                }
                else if (pCodigo == 0)
                {
                    s = s + " order by ta.DescricaoReduzida limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    pTipoAcondicionamento.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                    pTipoAcondicionamento.Descricao = l_dt.Rows[0]["Descricao"].ToString();
                }
            }
            catch (Exception ex)
            {
                pTipoAcondicionamento = new clsTipoAcondicionamento();
            }
            finally
            {
                DesconectaBanco();
            }
            return pTipoAcondicionamento;
        }

        public string PegaDescricao(int pCodigo)
        {
            string _Ret = "";
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Descricao from TipoAcondicionamento \n";
                s = s + "where Codigo = " + pCodigo + " \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                {
                    _Ret = l_dt.Rows[0]["Descricao"].ToString();
                }
            }
            catch (Exception ex)
            {
                _Ret = "";
            }
            finally
            {
                DesconectaBanco();
            }
            return _Ret;
        }
        public DataTable PegaDados(clsTipoAcondicionamento pTipoAcondicionamento, int pCodigo, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select ta.Codigo, ta.Descricao \n";
                s = s + "from   TipoAcondicionamento ta \n";
                if (pCodigo > 0)
                {
                    s = s + "where  ta.Codigo = " + pCodigo + " \n ";
                }
                if (pUltimoRegistro)
                {
                    s = s + "order by ta.Codigo desc ";
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
        public DataTable PegaDadosLista(string pDescricao, int pCodigo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select ta.Codigo, ta.Descricao \n";
                s = s + "from   TipoAcondicionamento ta \n";
                if (pDescricao != "")
                {
                    s = s + "and  ta.Descricao LIKE '%" + pDescricao + "%' \n ";
                }
                if (pCodigo > 0)
                {
                    s = s + "and  ta.Codigo = " + pCodigo + " \n ";
                }
                else if (pCodigo == 0)
                {
                    s = s + " order by ta.Descricao ";
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
                s = s + "from TipoAcondicionamento ";
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
                s = s + "select Codigo \n";
                s = s + "from   TipoAcondicionamento \n";
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
        public bool CodigoExiste(int pCodigo)
        {
            bool bRet = false;
            try
            {
                oDB.ConectaMySql();
                s = "";
                if (pCodigo != 0)
                {
                    s = s + "select Codigo ";
                    s = s + "from   TipoAcondicionamento ";
                    s = s + "where  Codigo = " + pCodigo + " ";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pCodigo != 0)
                    bRet = true;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return bRet;
        }
        public void Inserir(clsTipoAcondicionamento pTipoAcondicionamento, int pCodigo = 0)
        {
            try
            {
                s = "";
                s = s + "insert into TipoAcondicionamento \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + "  Codigo, \n";
                s = s + "  Descricao \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + pCodigo + ", \n";
                s = s + "'" + pTipoAcondicionamento.Descricao + "' \n";               
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
        public string Alterar(clsTipoAcondicionamento pTipoAcondicionamento, int pCodigo)
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
                s = s + "update TipoAcondicionamento \n";
                s = s + "set Descricao          = '" + pTipoAcondicionamento.Descricao + "' \n";
                s = s + "where Codigo = " + pCodigo.ToString();
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
        public DataTable PreencheDataTable(string pOrdem, string pFiltro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select * from TipoAcondicionamento \n";
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
                s = s + "delete from TipoAcondicionamento ";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo.ToString();
                }
                else
                {
                    s = s + "where  Descricao = '" + pDescricao + "'";
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