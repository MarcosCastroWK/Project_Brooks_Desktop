using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using MySql.Data.MySqlClient;
using SILCNegocios;

namespace LibSILC
{
	public class clsMovimentacaoDTRDados
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
        public clsMovimentacaoDTR PegaDados(clsMovimentacaoDTR pMovimentacaoDTR, int pCodigo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Data, MoviCxDe, MoviCxPara, NumeroLancamento, CodigoResiduo, CodigoCliente \n"; 
                s = s + "from   MovimentacaoDTR \n";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " ";
                }
                else if (pCodigo == 0)
                {
                    s = s + " order by Data limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    pMovimentacaoDTR.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                    if (l_dt.Rows[0]["Data"].ToString() != "")
                        pMovimentacaoDTR.Data = Convert.ToDateTime(l_dt.Rows[0]["Data"].ToString()).Date.ToShortDateString();
                    else
                        pMovimentacaoDTR.Data = "";
                    pMovimentacaoDTR.MoviCxDe = l_dt.Rows[0]["MoviCxDe"].ToString();
                    pMovimentacaoDTR.MoviCxPara = l_dt.Rows[0]["MoviCxPara"].ToString();
                    if (l_dt.Rows[0]["NumeroLancamento"].ToString() != "")
                        pMovimentacaoDTR.NumeroLancamento = Convert.ToInt32(l_dt.Rows[0]["NumeroLancamento"]);
                    if (l_dt.Rows[0]["CodigoResiduo"].ToString() != "")
                        pMovimentacaoDTR.CodigoResiduo = Convert.ToInt32(l_dt.Rows[0]["CodigoResiduo"]);
                    if (l_dt.Rows[0]["CodigoCliente"].ToString() != "")
                        pMovimentacaoDTR.CodigoCliente = Convert.ToInt32(l_dt.Rows[0]["CodigoCliente"]);
                }
                
            }
            catch (Exception ex)
            {
                pMovimentacaoDTR = new clsMovimentacaoDTR();
            }
            finally
            {
                DesconectaBanco();
            }
            return pMovimentacaoDTR;
        }
        public DataTable PegaDados(int pCodigo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Data, MoviCxDe, MoviCxPara, NumeroLancamento, CodigoResiduo, CodigoCliente \n"; 
                s = s + "from   MovimentacaoDTR \n";
                if (pCodigo > 0)
                    s = s + "where  Codigo = " + pCodigo + " ";
                else if (pCodigo == 0)
                    s = s + " order by Data ";
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
                s = s + "from MovimentacaoDTR ";
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
                if (pCodigo != 0)
                {
                    s = "";
                    s = s + "select Codigo \n";
                    s = s + "from   MovimentacaoDTR \n";
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
        public bool Inserir(clsMovimentacaoDTR pMovimentacaoDTR)
        {
            bool _resultado = false;
            oDB.ConectaMySql();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            MySqlTransaction transaction;
            ConectaBanco();
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "insert into MovimentacaoDTR \n";
                s = s + "( \n";
                s = s + "  Data, MoviCxDe, MoviCxPara, NumeroLancamento, CodigoResiduo, CodigoCliente \n"; 
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
               if (pMovimentacaoDTR.Data == "")
                    s = s + "'0001-01-01', \n";
                else
                    s = s + "'" + Convert.ToDateTime(pMovimentacaoDTR.Data).ToString("yyyy-MM-dd") + "', \n";
                s = s + "'" + pMovimentacaoDTR.MoviCxDe + "', \n";
                s = s + "'" + pMovimentacaoDTR.MoviCxPara + "', \n";
                if (pMovimentacaoDTR.NumeroLancamento.ToString() != "")
                    s = s + " " + pMovimentacaoDTR.NumeroLancamento.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n"; 
                if (pMovimentacaoDTR.CodigoResiduo.ToString() != "")
                    s = s + " " + pMovimentacaoDTR.CodigoResiduo.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pMovimentacaoDTR.CodigoCliente.ToString() != "")
                    s = s + " " + pMovimentacaoDTR.CodigoCliente.ToString().Replace(",", ".") + " \n";
                else
                    s = s + "0 \n";
                s = s + ")"; 
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
                _resultado = true;
            } 
            catch (Exception er)
            {
                transaction.Rollback();
                _resultado = false;
            }
            finally
            {
                DesconectaBanco();
            }
            return _resultado;
        }
        public string Alterar(clsMovimentacaoDTR pMovimentacaoDTR, int pCodigo)
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
                s = s + "update MovimentacaoDTR \n";

                if (pMovimentacaoDTR.Data == "")
                    s = s + " set Data = '0001-01-01', \n";
                else
                    s = s + " set Data = '" + Convert.ToDateTime(pMovimentacaoDTR.Data).ToString("yyyy-MM-dd") + "', \n";
                s = s + " MoviCxDe = '" + pMovimentacaoDTR.MoviCxDe + "', \n";
                s = s + " MoviCxPara = '" + pMovimentacaoDTR.MoviCxPara + "', \n";
                if (pMovimentacaoDTR.NumeroLancamento.ToString() != "")
                    s = s + " NumeroLancamento = " + pMovimentacaoDTR.NumeroLancamento.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " NumeroLancamento = 0, \n";
                if (pMovimentacaoDTR.CodigoResiduo.ToString() != "")
                    s = s + " CodigoResiduo = " + pMovimentacaoDTR.CodigoResiduo.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " CodigoResiduo = 0, \n";
                if (pMovimentacaoDTR.CodigoCliente.ToString() != "")
                    s = s + " CodigoCliente = " + pMovimentacaoDTR.CodigoCliente.ToString().Replace(",", ".") + " \n";
                else
                    s = s + " CodigoCliente = 0 \n";

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
        public string PegaMovimentacaoDTR(string pNumeroLancamento, string pCodigoResiduo, string pCodigoCliente)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();

                s = "";
                s = s + "select MoviCxDe ";
                s = s + "from   MovimentacaoDTR \n";
                s = s + "where  NumeroLancamento = " + pNumeroLancamento + " \n";
                s = s + "and    CodigoResiduo    = " + pCodigoResiduo + " \n";
                s = s + "and    CodigoCliente    = " + pCodigoCliente + " \n";
                s = s + "order by Codigo desc \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                {
                    sRet = l_dt.Rows[0][0].ToString();
                }
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

        public string PegaMovimentacaoDTRPara(string pNumeroLancamento, string pCodigoResiduo, string pCodigoCliente)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();

                s = "";
                s = s + "select MoviCxPara ";
                s = s + "from   MovimentacaoDTR \n";
                s = s + "where  NumeroLancamento = " + pNumeroLancamento + " \n";
                s = s + "and    CodigoResiduo    = " + pCodigoResiduo + " \n";
                s = s + "and    CodigoCliente    = " + pCodigoCliente + " \n";
                s = s + "order by Codigo desc \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                {
                    sRet = l_dt.Rows[0][0].ToString();
                }
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
        public DataTable PreencheDataTableMovimentacaoDTR(string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Data, MoviCxDe, MoviCxPara, NumeroLancamento, CodigoResiduo, CodigoCliente \n"; 
                s = s + "from   MovimentacaoDTR  \n";
                s = s + "order  by " + pOrdem;
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
        public DataTable PreencheDataTableMovimentacaoDTR(string pOrdem, string pNumeroLancamento, string pCodigoResiduo, string pCodigoCliente)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Data, MoviCxDe, MoviCxPara, NumeroLancamento, CodigoResiduo, CodigoCliente \n";
                s = s + "from   MovimentacaoDTR  \n";
                s = s + "where  NumeroLancamento = " + pNumeroLancamento + "\n"; 
                s = s + "and    CodigoResiduo    = " + pCodigoResiduo + "\n";
                s = s + "and    CodigoCliente    = " + pCodigoCliente + "\n";
                s = s + "order  by " + pOrdem;
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
                    s = s + "delete from MovimentacaoDTR ";
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
        public string ExcluirMovimentoIncorreto(string pNumeroLancamento, string pCodigoCliente, string pCodigoResiduo, string pData, string pMoviCxDe, string pMoviCxPara )
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
                s = s + "delete from MovimentacaoDTR ";
                s = s + "where  NumeroLancamento =  " + pNumeroLancamento + " \n";
                s = s + "and    CodigoCliente    =  " + pCodigoCliente + " \n";
                s = s + "and    CodigoResiduo    =  " + pCodigoResiduo + " \n";
                s = s + "and    Data             = '" + Convert.ToDateTime(pData).ToString("yyyy-MM-dd") + "' \n";
                s = s + "and    MoviCxDe         = '" + pMoviCxDe + "' \n";
                s = s + "and    MoviCxPara       = '" + pMoviCxPara + "' \n";
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