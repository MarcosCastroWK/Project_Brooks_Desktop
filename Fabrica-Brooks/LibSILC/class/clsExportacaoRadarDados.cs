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
    public class clsExportacaoRadarDados
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
        public void PegaDados(clsExportacaoRadar pExportacaoRadar, int pSequencial)
        {
            if (pSequencial > 0)
            {
                try
                {
                    ConectaBanco();
                    s = "";
                    s = s + "select Sequencial, Tabela, CodigoNumero, DataSolicitacao, Gerado \n";
                    s = s + "from   ExportacaoRadar \n";
                    s = s + "where  Sequencial = " + pSequencial + " \n";
                    s = s + "limit 1";
                    FillDataSet();
                    if (l_ds.Tables.Count > 0)
                    {
                        l_dt = l_ds.Tables[0];
                        pExportacaoRadar.Sequencial = pSequencial;
                        pExportacaoRadar.Tabela = l_dt.Rows[0]["Tabela"].ToString();
                        if (l_dt.Rows[0]["CodigoNumero"].ToString() != "")
                            pExportacaoRadar.CodigoNumero = Convert.ToInt32(l_dt.Rows[0]["CodigoNumero"]);
                        if (l_dt.Rows[0]["DataSolicitacao"].ToString() != "")
                            pExportacaoRadar.DataSolicitacao = l_dt.Rows[0]["DataSolicitacao"].ToString();
                        if (l_dt.Rows[0]["Gerado"].ToString() != "")
                            pExportacaoRadar.Gerado = Convert.ToInt16(l_dt.Rows[0]["Gerado"]);
                    }
                }
                finally
                {
                    DesconectaBanco();
                }
            }
        }
        public void PegaArquivoNaoGerado(clsExportacaoRadar pExportacaoRadar)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial, Tabela, CodigoNumero, DataSolicitacao \n";
                s = s + "from   ExportacaoRadar \n";
                s = s + "where  (Gerado = 0 or Gerado is null) \n";
                s = s + "limit  1 \n";
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows[0]["Sequencial"].ToString() != "")
                        pExportacaoRadar.Sequencial = Convert.ToInt32(l_dt.Rows[0]["Sequencial"]);
                    pExportacaoRadar.Tabela = l_dt.Rows[0]["Tabela"].ToString();
                    if (l_dt.Rows[0]["CodigoNumero"].ToString() != "")
                        pExportacaoRadar.CodigoNumero = Convert.ToInt32(l_dt.Rows[0]["CodigoNumero"]);
                    if (l_dt.Rows[0]["DataSolicitacao"].ToString() != "")
                        pExportacaoRadar.DataSolicitacao = l_dt.Rows[0]["DataSolicitacao"].ToString();
                    pExportacaoRadar.Gerado = 0;
                }
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public bool ExisteArquivoNaoGerado()
        {
            bool bRet = false;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select CodigoNumero \n";
                s = s + "from   ExportacaoRadar \n";
                s = s + "where  (Gerado = 0 or Gerado is null) \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    bRet = true;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return bRet;
        }
        public void Inserir(clsExportacaoRadar pExportacaoRadar, int pSequencial = 0)
        {
            try
            {
                s = "";
                s = s + "insert into ExportacaoRadar \n";
                s = s + "( \n";
                if (pSequencial > 0)
                    s = s + "  Sequencial, \n";
                s = s + "  Tabela, CodigoNumero, DataSolicitacao, Gerado \n"; 
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pSequencial > 0)
                    s = s + pSequencial.ToString() + ", \n";
                s = s + " '" + pExportacaoRadar.Tabela + "', \n";
                s = s + " " + pExportacaoRadar.CodigoNumero + ", \n";
                if (pExportacaoRadar.DataSolicitacao != "")
                    s = s + " '" + Convert.ToDateTime(pExportacaoRadar.DataSolicitacao).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + " '0100-01-01', \n";
                s = s + pExportacaoRadar.Gerado + " \n";
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
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public void SalvarComoGerado(int pCodigoNumero)
        {
            if (pCodigoNumero > 0)
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
                    s = s + "update ExportacaoRadar \n";
                    s = s + "set    Gerado = 1 \n";
                    s = s + "where  Sequencial = " + pCodigoNumero + " \n";
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                finally
                {
                    oDB.DesconectaMySql();
                }
            }
        }
        public DataTable PreencheDataTable(string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial, Tabela, CodigoNumero \n";
                s = s + "from   ExportacaoRadar \n";
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

        public DataTable PreencheDataTable(string pOrdem, string pFiltro, string pTabela)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial, Tabela, CodigoNumero \n";
                s = s + "from   ExportacaoRadar \n";
                s = s + "where " + pTabela + " like '%" + pFiltro + "%' \n";
                if (pOrdem != "")
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

        public void Excluir(int pSequencial)
        {
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
                    s = s + "delete from ExportacaoRadar \n";
                    s = s + "where  Sequencial = " + pSequencial.ToString();
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                finally
                {
                    oDB.DesconectaMySql();
                }
            }
        }
    }
}