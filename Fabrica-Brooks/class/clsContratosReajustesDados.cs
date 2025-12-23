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
	public class clsContratosReajustesDados
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
        public clsContratosReajustes PegaDados(clsContratosReajustes pReajustes, int pCodigoContrato, int pCodigoCliente, int pSequencial)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select CodigoContrato, Data, Valor, NumeroContrato, Situacao, TipoNegociacao, Sequencial, Observacao \n";
                s = s + "from   Reajustes \n";
                if (pCodigoContrato > 0)
                {
                    s = s + "where  CodigoContrato = " + pCodigoContrato + " \n";
                    s = s + "and    CodigoCliente = " + pCodigoCliente + " \n";
                    s = s + "and    Sequencial    = " + pSequencial + " \n";
                    s = s + "limit 1";
                }
                else if (pCodigoContrato == 0)
                {
                    s = s + " order by CodigoCliente limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows[0]["CodigoContrato"].ToString() != "")
                        pReajustes.CodigoContrato = Convert.ToInt32(l_dt.Rows[0]["CodigoContrato"]);
                    if (l_dt.Rows[0]["Data"].ToString() != "")
                        pReajustes.Data = Convert.ToDateTime(l_dt.Rows[0]["Data"].ToString()).Date.ToShortDateString();
                    pReajustes.NumeroContrato = l_dt.Rows[0]["NumeroContrato"].ToString();
                    if (l_dt.Rows[0]["Valor"].ToString() != "")
                        pReajustes.Valor = Convert.ToDecimal(l_dt.Rows[0]["Valor"].ToString());
                    pReajustes.Situacao = l_dt.Rows[0]["Situacao"].ToString();
                    pReajustes.TipoNegociacao = l_dt.Rows[0]["TipoNegociacao"].ToString();
                    pReajustes.Observacao = l_dt.Rows[0]["Reajustes"].ToString();
                }
            }
            catch (Exception ex)
            {
                pReajustes = new clsContratosReajustes();
            }
            finally
            {
                DesconectaBanco();
            }
            return pReajustes;
        }
        public clsContratosReajustes PegaDados(clsContratosReajustes pReajustes, int pCodigoContrato, string pDataReajuste, int pSequencial)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select CodigoContrato, Data, Valor, NumeroContrato, Situacao, TipoNegociacao, Sequencial \n";
                s = s + "from   Reajustes \n";
                s = s + "where  CodigoContrato = " + pCodigoContrato + " \n";
                s = s + "and    Data = '" + Convert.ToDateTime(pDataReajuste).ToString("yyyy-MM-dd") + "' \n";
                if (pSequencial > 0)
                    s = s + "and    Sequencial    = " + pSequencial + " \n";
                else if (pSequencial == 0) // quando for igual a zero pegar o primeiro que o inicio contrato
                    s = s + "order by Sequencial ";
                s = s + "limit 1";
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows[0]["CodigoContrato"].ToString() != "")
                        pReajustes.CodigoContrato = Convert.ToInt32(l_dt.Rows[0]["CodigoContrato"]);
                    if (l_dt.Rows[0]["Data"].ToString() != "")
                        pReajustes.Data = Convert.ToDateTime(l_dt.Rows[0]["Data"].ToString()).Date.ToShortDateString();
                    pReajustes.NumeroContrato = l_dt.Rows[0]["NumeroContrato"].ToString();
                    if (l_dt.Rows[0]["Valor"].ToString() != "")
                        pReajustes.Valor = Convert.ToDecimal(l_dt.Rows[0]["Valor"].ToString());
                    pReajustes.Situacao = l_dt.Rows[0]["Situacao"].ToString();
                    pReajustes.TipoNegociacao = l_dt.Rows[0]["TipoNegociacao"].ToString();
                    pReajustes.Observacao = l_dt.Rows[0]["Reajustes"].ToString();
                }
            }
            catch (Exception ex)
            {
                pReajustes = new clsContratosReajustes();
            }
            finally
            {
                DesconectaBanco();
            }
            return pReajustes;
        }
        public DataTable PegaDados(clsContratosReajustes pReajustes, int pCodigoContrato, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select CodigoContrato, Data, Valor, NumeroContrato, Situacao, TipoNegociacao, Sequencial, Observacao \n";
                s = s + "from   Reajustes \n";
                if (pCodigoContrato > 0)
                {
                    s = s + "where  CodigoContrato = " + pCodigoContrato + " ";
                }
                if (pUltimoRegistro)
                {
                    s = s + "order by CodigoContrato desc ";
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
                s = s + "from Reajustes ";
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
        public string DadoExiste(int pCodigoContrato, string pData, string pNumeroContrato)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                if (pCodigoContrato != 0)
                {
                    s = s + "select CodigoContrato ";
                    s = s + "from   Reajustes ";
                    s = s + "where  CodigoContrato = " + pCodigoContrato + " ";
                    s = s + "and    Data = '" + Convert.ToDateTime(pData).ToString("yyyy-MM-dd") + "' \n";
                    s = s + "and    NumeroContrato = '" + pNumeroContrato + "' \n";
                    FillDataSet();
                }
                if (l_dt.Rows.Count > 0 && pCodigoContrato != 0)
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
        public string DadoExiste(int pSequencial)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                if (pSequencial > 0)
                {
                    s = s + "select Sequencial ";
                    s = s + "from   Reajustes ";
                    s = s + "where  Sequencial = " + pSequencial + " ";
                    FillDataSet();
                    if (l_dt.Rows.Count > 0)
                        sRet = "Alterar";
                    else
                        sRet = "Incluir";
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
        public string RegistrosDeReajuste(int pCodigoContrato)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                if (pCodigoContrato != 0)
                {
                    s = s + "select count(*) as QtReajustes \n";
                    s = s + "from   Reajustes \n";
                    s = s + "where  CodigoContrato = " + pCodigoContrato + " \n";
                    FillDataSet();
                }
                if (l_dt.Rows.Count > 0 && pCodigoContrato != 0)
                    sRet = l_dt.Rows[0][0].ToString();
                else
                    sRet = "0";
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
        public bool Inserir(clsContratosReajustes pReajustes)
        {
            bool bRet = false;
            try
            {
                s = "";
                s = s + "insert into Reajustes \n";
                s = s + "( \n";
                s = s + "  CodigoContrato, Data, Valor, NumeroContrato, Situacao, TipoNegociacao, Observacao \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pReajustes.CodigoContrato == 0)
                    s = s + "0, ";
                else
                    s = s + pReajustes.CodigoContrato.ToString() + ", ";

                if (pReajustes.Data == "")
                    s = s + "'0001-01-01', ";
                else
                    s = s + "'" + Convert.ToDateTime(pReajustes.Data).ToString("yyyy-MM-dd") + "', ";

                if (pReajustes.Valor == 0)
                    s = s + "0, ";
                else
                    s = s + " " + pReajustes.Valor.ToString().Replace(",", ".") + ", ";

                if (pReajustes.NumeroContrato == "0")
                    s = s + "0, ";
                else
                    s = s + "'" + pReajustes.NumeroContrato.ToString() + "', ";

                s = s + "'" + pReajustes.Situacao + "', ";
                s = s + "'" + pReajustes.TipoNegociacao + "', ";
                s = s + "'" + pReajustes.Observacao + "' ";
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
                bRet = true;
            }
            catch
            {
                bRet = false;
            }
            finally
            {
                DesconectaBanco();
            }
            return bRet;

        }
        public string Alterar(clsContratosReajustes pReajustes, int pCodigoContrato, int pCodigoCliente, int pSequencial = 0)
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
                s = s + "update Reajustes \n";
                if (pReajustes.Data == "")
                    s = s + " set Data = '0001-01-01', ";
                else
                    s = s + " set Data = '" + Convert.ToDateTime(pReajustes.Data).ToString("yyyy-MM-dd") + "', ";

                if (pReajustes.Valor == 0)
                    s = s + " Valor = 0, ";
                else
                    s = s + " Valor = " + pReajustes.Valor.ToString().Replace(",", ".") + ", ";

                if (pReajustes.NumeroContrato.ToString() == "0")
                    s = s + " NumeroContrato = '0', ";
                else
                    s = s + " NumeroContrato = '" + pReajustes.NumeroContrato.ToString() + "', ";

                s = s + "  Situacao = '" + pReajustes.Situacao + "', ";
                s = s + "  TipoNegociacao = '" + pReajustes.TipoNegociacao + "', ";
                s = s + "  Observacao = '" + pReajustes.Observacao + "' ";

                if (pSequencial == 0)
                {
                    s = s + "where CodigoContrato = " + pCodigoContrato.ToString() + " \n";
                    s = s + "and   Data = '" + Convert.ToDateTime(pReajustes.Data).ToString("yyyy-MM-dd") + "' \n";
                    s = s + "and   NumeroContrato = '" + pReajustes.NumeroContrato + "' \n";
                }
                else if (pSequencial > 0)
                {
                    s = s + "where   Sequencial = '" + pSequencial + "' \n";
                }
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
        public string AlterarValorContratoUltimoReajuste(string pValorContrato, int pSequencial)
        {
            string sRet = "";
            if (pSequencial > 0)
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
                    s = s + "update Reajustes \n";
                    s = s + "set    Valor = " + pValorContrato.Replace(",", ".") + " \n";
                    s = s + "where  Sequencial = '" + pSequencial + "' \n";
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
            }
            return sRet;
        }
        public DataTable PreencheDT_Situacao(int pLimite = 1000)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select distinct Situacao \n";
                s = s + "from   Reajustes \n";
                s = s + "order  by Situacao  \n";
                s = s + "limit  " + pLimite.ToString() + " \n";
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
        public DataTable PreencheDT_TipoNegociacao(int pLimite = 1000)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select distinct TipoNegociacao \n";
                s = s + "from   Reajustes \n";
                s = s + "order  by TipoNegociacao \n";
                s = s + "limit  " + pLimite.ToString() + " \n";
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
        public DataTable PreencheDataTable(string pOrdem, int pCodigoContrato, int pLimite = 1000)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select CodigoContrato, Data, Valor, NumeroContrato, Situacao, TipoNegociacao, Sequencial, date('0001-01-01') as ProximoReajuste, 0.00 as PercentualReajuste, Observacao \n";
                s = s + "from   Reajustes \n";
                if (pCodigoContrato > 0)
                    s = s + "where  CodigoContrato = " + pCodigoContrato + " \n";
                s = s + "order by " + pOrdem + " \n";
                s = s + "limit " + pLimite.ToString() + " \n";
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
        public DataTable PreencheDataTableContratos(string pOrdem, string pFiltro, string pCampo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select CodigoContrato, Data, Valor, NumeroContrato, Situacao, TipoNegociacao, Sequencial, Observacao \n";
                s = s + "from   Reajustes \n";
                if (pOrdem.ToUpper() == "CodigoContrato asc".ToUpper() || pOrdem.ToUpper() == "CodigoContrato desc".ToUpper() || pOrdem.ToUpper() == "Codigo".ToUpper())
                {
                    s = s + "order by " + pOrdem + " \n";
                }
                else
                {
                    s = s + "where " + pCampo + " like '%" + pFiltro + "%' \n";
                    s = s + "order by " + pOrdem + " \n";
                }
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
        public string Excluir(int pCodigoContrato, string pDataReajuste)
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
                if (pCodigoContrato > 0)
                {
                    s = s + "delete from Reajustes \n";
                    s = s + "where  CodigoContrato = " + pCodigoContrato.ToString() + " \n";
                    s = s + "and    Data           = '" + Convert.ToDateTime(pDataReajuste).ToString("yyyy-MM-dd") + "' \n"; 
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
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
        public bool ExcluirPeloSequencial(int pSequencial)
        {
            bool sRet = false;
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "delete from Reajustes \n";
                s = s + "where  Sequencial = " + pSequencial.ToString() + " \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
                sRet = true;
            }
            catch
            {
                transaction.Rollback();
                sRet = false;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }

        public string Excluir(int pCodigoContrato)
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
                if (pCodigoContrato > 0)
                {
                    s = s + "delete from Reajustes \n";
                    s = s + "where  CodigoContrato = " + pCodigoContrato.ToString();
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
        public DataTable PreencheDataTableReajustes(string pOrdem, string pData1, string pData2)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + " SELECT c.Codigo, c.NomeFantasia as 'Nome Fantasia', c.Nome, cr.DescricaoHistorico as Histórico, \n ";
                s = s + "        max(cr.DataReajuste) as 'Data Reajuste',  cr.ValorContrato, \n ";
                s = s + "        cr.DiaVencimento \n ";
                s = s + " FROM   Contratos cr \n ";
                s = s + " left   join Clientes c ON (cr.CodigoCliente = c.Codigo) \n ";
                s = s + " left   join ContratoResiduos cres ON (cres.CodigoCliente = c.Codigo) \n ";
                s = s + " left   join Reajustes r  ON (r.CodigoContrato = cr.CodigoContrato) \n ";
                s = s + " where ";
                if (pData2 == "") 
                    s = s + " (DataRecisao = '0100-01-01' or DataRecisao = '0001-01-01' or DataRecisao = '01900-01-01' or DataRecisao is null) \n ";
                else if ((pData1 == "") && (pData2 != ""))
                {
                    s = s + " (DataRecisao = '0100-01-01' or DataRecisao = '0001-01-01' or DataRecisao = '01900-01-01' or DataRecisao is null) \n ";
                    s = s + " and cres.DataReajuste <= '" + Convert.ToDateTime(pData2).ToString("yyyy-MM-dd") + "' \n ";
                }
                else if (pData1 != "" && pData2 != "")
                {
                    s = s + " (DataRecisao = '0100-01-01' or DataRecisao = '0001-01-01' or DataRecisao = '01900-01-01' or DataRecisao is null) \n ";
                    s = s + " and cr.DataReajuste >= '" + Convert.ToDateTime(pData1).ToString("yyyy-MM-dd") + "' \n ";
                    s = s + " and cr.DataReajuste <= '" + Convert.ToDateTime(pData2).ToString("yyyy-MM-dd") + "' \n ";
                }
                s = s + "and (c.Inativo = 0 or c.Inativo is null) \n";
                s = s + "group by c.Codigo, c.NomeFantasia, c.Nome, cr.DescricaoHistorico, cr.DiaVencimento, cr.ValorContrato \n ";
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
    }
}