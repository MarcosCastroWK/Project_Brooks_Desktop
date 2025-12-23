using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data.OleDb;
using SILCNegocios;

namespace LibSILC
{
    public class clsCorpoNotasFiscaisDados
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
        public clsCorpoNotasFiscais PegaDados(clsCorpoNotasFiscais pCorpoNotasFiscais, int pSequencialNotaFiscal)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select NumeroNotaFiscal, Linha, Descricao, Unidade, PrecoUnitario, Valor, qt3Aux, Quantidade \n ";
                s = s + "from   CorpoNotasFiscais \n";
                if (pSequencialNotaFiscal > 0)
                {
                    s = s + "where  NumeroNotaFiscal = " + pSequencialNotaFiscal + " \n";
                    s = s + "limit 1";
                }
                else if (pSequencialNotaFiscal == 0)
                {
                    s = s + " order by NumeroNotaFiscal limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows[0]["NumeroNotaFiscal"].ToString() != "")
                        pCorpoNotasFiscais.SequencialNotaFiscal = Convert.ToInt32(l_dt.Rows[0]["NumeroNotaFiscal"]);
                    if (l_dt.Rows[0]["Linha"].ToString() != "")
                        pCorpoNotasFiscais.Linha = Convert.ToInt32(l_dt.Rows[0]["Linha"]);
                    pCorpoNotasFiscais.Descricao = l_dt.Rows[0]["Descricao"].ToString();
                    pCorpoNotasFiscais.Unidade = l_dt.Rows[0]["Unidade"].ToString();
                    if (l_dt.Rows[0]["PrecoUnitario"].ToString() != "")
                        pCorpoNotasFiscais.PrecoUnitario = Convert.ToDecimal(l_dt.Rows[0]["PrecoUnitario"]);
                    if (l_dt.Rows[0]["Valor"].ToString() != "")
                        pCorpoNotasFiscais.Valor = Convert.ToDecimal(l_dt.Rows[0]["Valor"]);
                    if (l_dt.Rows[0]["qt3Aux"].ToString() != "")
                        pCorpoNotasFiscais.qt3Aux = Convert.ToDecimal(l_dt.Rows[0]["qt3Aux"]);
                    if (l_dt.Rows[0]["Quantidade"].ToString() != "")
                        pCorpoNotasFiscais.Quantidade = Convert.ToDecimal(l_dt.Rows[0]["Quantidade"]);
                }
            }
            catch (Exception ex)
            {
                pCorpoNotasFiscais = new clsCorpoNotasFiscais();
            }
            finally
            {
                DesconectaBanco();
            }
            return pCorpoNotasFiscais;
        }
        public clsCorpoNotasFiscais PegaDados(clsCorpoNotasFiscais pCorpoNotasFiscais, int pSequencialNotaFiscal, int pLinha)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select NumeroNotaFiscal, Linha, Descricao, Unidade, PrecoUnitario, Valor, qt3Aux, Quantidade \n ";
                s = s + "from   CorpoNotasFiscais \n";
                if (pSequencialNotaFiscal > 0 && pLinha >= 0)
                {
                    s = s + "where  NumeroNotaFiscal = " + pSequencialNotaFiscal + " \n";
                    s = s + "and    Linha = " + pLinha + " \n";
                    s = s + "limit 1";
                }
                else if (pSequencialNotaFiscal == 0)
                {
                    s = s + " order by NumeroNotaFiscal limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows[0]["NumeroNotaFiscal"].ToString() != "")
                        pCorpoNotasFiscais.SequencialNotaFiscal = Convert.ToInt32(l_dt.Rows[0]["NumeroNotaFiscal"]);
                    if (l_dt.Rows[0]["Linha"].ToString() != "")
                        pCorpoNotasFiscais.Linha = Convert.ToInt32(l_dt.Rows[0]["Linha"]);
                    pCorpoNotasFiscais.Descricao = l_dt.Rows[0]["Descricao"].ToString();
                    pCorpoNotasFiscais.Unidade = l_dt.Rows[0]["Unidade"].ToString();
                    if (l_dt.Rows[0]["PrecoUnitario"].ToString() != "")
                        pCorpoNotasFiscais.PrecoUnitario = Convert.ToDecimal(l_dt.Rows[0]["PrecoUnitario"]);
                    if (l_dt.Rows[0]["Valor"].ToString() != "")
                        pCorpoNotasFiscais.Valor = Convert.ToDecimal(l_dt.Rows[0]["Valor"]);
                    if (l_dt.Rows[0]["qt3Aux"].ToString() != "")
                        pCorpoNotasFiscais.qt3Aux = Convert.ToDecimal(l_dt.Rows[0]["qt3Aux"]);
                    if (l_dt.Rows[0]["Quantidade"].ToString() != "")
                        pCorpoNotasFiscais.Quantidade = Convert.ToDecimal(l_dt.Rows[0]["Quantidade"]);
                }
            }
            catch (Exception ex)
            {
                pCorpoNotasFiscais = new clsCorpoNotasFiscais();
            }
            finally
            {
                DesconectaBanco();
            }
            return pCorpoNotasFiscais;
        }
        public DataTable PegaDados(clsCorpoNotasFiscais pCorpoNotasFiscais, int pSequencialNotaFiscal, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select NumeroNotaFiscal, Linha, Descricao, Unidade, PrecoUnitario, Valor, Quantidade \n ";
                s = s + "from   CorpoNotasFiscais \n";
                if (pSequencialNotaFiscal > 0)
                {
                    s = s + "where  NumeroNotaFiscal = " + pSequencialNotaFiscal + " \n ";
                }
                if (pUltimoRegistro)
                {
                    s = s + "order by NumeroNotaFiscal desc \n ";
                }
                else if (pSequencialNotaFiscal == 0 && pUltimoRegistro)
                {
                    s = s + " order by NumeroNotaFiscal \n ";
                }
                s = s + " limit 100 \n ";
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
                s = s + "from CorpoNotasFiscais ";
                s = s + "where  type like 'varchar%' and field = '" + pCampo + "' ";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                {
                    iRet = Convert.ToInt16(l_dt.Rows[0]["Type"].ToString().Replace("varchar(", "").Replace(")", ""));
                }
                else
                    iRet = 0;
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
        public string DadoExiste(int pSequencialNotaFiscal, int pLinha = 0, bool pSoQuery = false)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select NumeroNotaFiscal ";
                s = s + "from   CorpoNotasFiscais ";
                if (pSequencialNotaFiscal != 0)
                {
                    s = s + "where  NumeroNotaFiscal = " + pSequencialNotaFiscal + " \n";
                    if (pLinha > 0)
                        s = s + "and    Linha = " + pLinha + " \n";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pSequencialNotaFiscal != 0)
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
        public string Inserir(clsCorpoNotasFiscais pCorpoNotasFiscais, int pNumeroNotaFiscal, bool pSoQuery = false)
        {
            string sRet = "";
            try
            {
                s = "";
                s = s + "insert into CorpoNotasFiscais \n";
                s = s + "( \n";
                s = s + " NumeroNotaFiscal, Linha, Descricao, Unidade, PrecoUnitario, Valor, Quantidade \n ";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                s = s + " " + pNumeroNotaFiscal + ", \n";
                if (pCorpoNotasFiscais.Linha > 0)
                    s = s + " " + pCorpoNotasFiscais.Linha.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                s = s + " '" + pCorpoNotasFiscais.Descricao + "', \n";
                s = s + " '" + pCorpoNotasFiscais.Unidade + "', \n";
                if (pCorpoNotasFiscais.PrecoUnitario > 0)
                    s = s + " " + pCorpoNotasFiscais.PrecoUnitario.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pCorpoNotasFiscais.Valor > 0)
                    s = s + " " + pCorpoNotasFiscais.Valor.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pCorpoNotasFiscais.Quantidade > 0)
                    s = s + " " + pCorpoNotasFiscais.Quantidade.ToString().Replace(",", ".") + " \n";
                else
                    s = s + "0 \n";
                s = s + ");";

                if (!pSoQuery)
                {
                    ConectaBanco();
                    MySqlCommand command = oDB.MySqlConnect.CreateCommand();
                    MySqlTransaction transaction;
                    transaction = oDB.MySqlConnect.BeginTransaction();
                    command.Connection = oDB.MySqlConnect;
                    command.Transaction = transaction;
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                    sRet = string.Empty;
                }
                else
                    sRet = s;
            }
            finally
            {
                DesconectaBanco();
            }
            return sRet;
        }
        public void SqlInsert(string pSql)
        {
            try
            {
                ConectaBanco();
                MySqlCommand command = oDB.MySqlConnect.CreateCommand();
                MySqlTransaction transaction;
                transaction = oDB.MySqlConnect.BeginTransaction();
                command.Connection = oDB.MySqlConnect;
                command.Transaction = transaction;
                command.CommandText = pSql;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public string Alterar(clsCorpoNotasFiscais pCorpoNotasFiscais, int pSequencialNotaFiscal, int pLinha = 0, bool pSoQuery = false)
        {
            string sRet = "";
            try
            {
                s = "";
                s = s + "update CorpoNotasFiscais \n";
                if (pCorpoNotasFiscais.Linha > 0)
                    s = s + " set Linha = " + pCorpoNotasFiscais.Linha.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " set Linha = 0, \n";
                s = s + " Descricao = '" + pCorpoNotasFiscais.Descricao + "', \n";
                s = s + " Unidade = '" + pCorpoNotasFiscais.Unidade + "', \n";
                if (pCorpoNotasFiscais.PrecoUnitario > 0)
                    s = s + " PrecoUnitario = " + pCorpoNotasFiscais.PrecoUnitario.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " PrecoUnitario = 0, \n";
                if (pCorpoNotasFiscais.Valor > 0)
                    s = s + " Valor = " + pCorpoNotasFiscais.Valor.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " Valor = 0, \n";
                if (pCorpoNotasFiscais.qt3Aux > 0)
                    s = s + " qt3Aux = " + pCorpoNotasFiscais.qt3Aux.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " qt3Aux = 0, \n";
                if (pCorpoNotasFiscais.Quantidade > 0)
                    s = s + " Quantidade = " + pCorpoNotasFiscais.Quantidade.ToString().Replace(",", ".") + " \n";
                else
                    s = s + " Quantidade = 0 \n";
                s = s + "where NumeroNotaFiscal = " + pSequencialNotaFiscal + " \n";
                if (pLinha > 0)
                    s = s + "and  Linha = " + pLinha + "; \n";
                else
                    s = s + "; \n";
                if (!pSoQuery)
                {
                    ConectaBanco();
                    MySqlCommand command = oDB.MySqlConnect.CreateCommand();
                    MySqlTransaction transaction;
                    transaction = oDB.MySqlConnect.BeginTransaction();
                    command.Connection = oDB.MySqlConnect;
                    command.Transaction = transaction;
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                    s = "";
                }
                sRet = s;
            }
            catch (Exception ex)
            {
                sRet = ex.ToString();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }
        public string SqlAlter(string pSql)
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
                command.CommandText = pSql;
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
        public DataTable PreencheDataTable(string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select NumeroNotaFiscal, Linha, Descricao, Unidade, PrecoUnitario, Valor, qt3Aux, Quantidade \n ";
                s = s + "from   CorpoNotasFiscais \n";
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
        public DataTable PreencheDataTable(string pOrdem, string pNumeroNF)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select NumeroNotaFiscal, Linha, Descricao, Unidade, PrecoUnitario, Valor, qt3Aux, Quantidade \n ";
                s = s + "from   CorpoNotasFiscais \n";
                s = s + "where  NumeroNotaFiscal = " + pNumeroNF + "\n";
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
        public DataTable PreencheDataTable(string pOrdem, string pFiltro, string pCampo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select SequencialNotaFiscal, Linha, Descricao, Unidade, PrecoUnitario, Valor, qt3Aux, Quantidade \n ";
                s = s + "from   CorpoNotasFiscais \n";
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

        public string Excluir(int pSequencialNotaFiscal)
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
                if (pSequencialNotaFiscal > 0)
                {
                    s = s + "delete from CorpoNotasFiscais ";
                    s = s + "where  SequencialNotaFiscal = " + pSequencialNotaFiscal.ToString();
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