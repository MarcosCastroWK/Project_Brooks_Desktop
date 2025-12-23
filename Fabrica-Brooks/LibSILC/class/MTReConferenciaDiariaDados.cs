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
    public class clsMTReConferenciaDiariaDados
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
        public clsMTReConferenciaDiaria PegaDados(clsMTReConferenciaDiaria pMTReConferenciaDiaria, int pSequencial)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial, NumeroMTRe, Data, Quantidade, NumeroCDFe, Obs \n"; 
                s = s + "from   MTReConferenciaDiaria \n";
                if (pSequencial > 0)
                {
                    s = s + "where  Sequencial = " + pSequencial + " \n";
                    s = s + "limit 1";
                }
                else if (pSequencial == 0)
                {
                    s = s + " order by Sequencial limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    
                    pMTReConferenciaDiaria.Sequencial = Convert.ToInt32(l_dt.Rows[0]["Sequencial"]);
                    
                    if (l_dt.Rows[0]["NumeroMTRe"].ToString() != "")
                        pMTReConferenciaDiaria.NumeroMTRe = Convert.ToInt64(l_dt.Rows[0]["NumeroMTRe"]);
                    
                    if (l_dt.Rows[0]["Data"].ToString() != "")
                        pMTReConferenciaDiaria.Data = Convert.ToDateTime(l_dt.Rows[0]["Data"]);
                    
                    if (l_dt.Rows[0]["Quantidade"].ToString() != "")
                        pMTReConferenciaDiaria.Quantidade = Convert.ToDecimal(l_dt.Rows[0]["Quantidade"]);
                    
                    if (l_dt.Rows[0]["NumeroCDFe"].ToString() != "")
                        pMTReConferenciaDiaria.NumeroCDFe = Convert.ToInt32(l_dt.Rows[0]["NumeroCDFe"]);

                    pMTReConferenciaDiaria.Obs = l_dt.Rows[0]["Obs"].ToString();

                }
                return pMTReConferenciaDiaria;
            }
            catch (Exception ex)
            {
                clsMTReConferenciaDiaria oCDFe = new clsMTReConferenciaDiaria();
                oCDFe.MensagemErro = ex.Message;
                return oCDFe;
            }
            finally
            {
                DesconectaBanco();
            }
        }
        
        public DataTable PegaDados(clsMTReConferenciaDiaria pMTReConferenciaDiaria, int pNumeroMTRe, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial, NumeroMTRe, Data, Quantidade, NumeroCDFe, Obs \n"; 
                s = s + "from   MTReConferenciaDiaria \n";
                if (pNumeroMTRe > 0)
                {
                    s = s + "where  NumeroMTRe = " + pNumeroMTRe + " ";
                }
                if (pUltimoRegistro)
                {
                    s = s + "order by Sequencial desc ";
                }
                else if (pNumeroMTRe == 0)
                {
                    s = s + " order by NumeroMTRe ";
                }
                FillDataSet();
                if (l_ds.Tables.Count == 1)
                {
                    l_dt = l_ds.Tables[0];

                    pMTReConferenciaDiaria.Sequencial = Convert.ToInt32(l_dt.Rows[0]["Sequencial"]);

                    if (l_dt.Rows[0]["NumeroMTRe"].ToString() != "")
                        pMTReConferenciaDiaria.NumeroMTRe = Convert.ToInt64(l_dt.Rows[0]["NumeroMTRe"]);

                    if (l_dt.Rows[0]["Data"].ToString() != "")
                        pMTReConferenciaDiaria.Data = Convert.ToDateTime(l_dt.Rows[0]["Data"]);

                    if (l_dt.Rows[0]["Quantidade"].ToString() != "")
                        pMTReConferenciaDiaria.Quantidade = Convert.ToDecimal(l_dt.Rows[0]["Quantidade"]);

                    if (l_dt.Rows[0]["NumeroCDFe"].ToString() != "")
                        pMTReConferenciaDiaria.NumeroCDFe = Convert.ToInt32(l_dt.Rows[0]["NumeroCDFe"]);
                    
                    pMTReConferenciaDiaria.Obs = l_dt.Rows[0]["Obs"].ToString();

                }
                return l_dt;
            }
            catch (Exception ex)
            {
                DataTable _dt = new DataTable();
                _dt.Columns.Add("MensagemErro");
                DataRow _dr = _dt.NewRow();
                _dr["MensagemErro"] = ex.Message;
                _dt.Rows.Add(_dr);
                return _dt;
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public DataTable PreencheDataTablePeriodo(string pOrdem, string pDataInicial, string pDataFinal)
        {
            try
            {
                ConectaBanco();                
                s = "";
                s = s + "select rmtre.CNPJ_CPF_Cliente as CNPJ_Cliente_IMA, \n";
                s = s + "       rmtre.NumeroMTRe as NumeroMTRe_IMA, rmtre.CodigoIBAMA as CodigoIbamaIMA, \n ";
                s = s + "       rmtre.Quantidade as Qtde_IMA, rmtre.Data as Data_IMA, Obs \n";
                s = s + "from   MTReConferenciaDiaria rmtre \n";
                s = s + "where rmtre.Data >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n ";
                if (pDataFinal != "")
                    s = s + "and   rmtre.Data <= '" + Convert.ToDateTime(pDataFinal).ToString("yyyy-MM-dd") + "' \n ";
                s = s + "order by " + pOrdem + " \n ";

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


        public int PegaTamanhoNumeroMTReVarChar(string pNumeroMTRe)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "show columns ";
                s = s + "from MTReConferenciaDiaria ";
                s = s + "where  type like 'varchar%' and field = '" + pNumeroMTRe + "' ";
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
        public string DadoExiste(Int64 pNumeroMTRe, string pCodigoIbama = "")
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select NumeroMTRe ";
                s = s + "from   MTReConferenciaDiaria ";
                if (pNumeroMTRe != 0)
                {
                    s = s + "where  NumeroMTRe = " + pNumeroMTRe + " \n ";
                    if (pCodigoIbama != "")
                        s = s + "and CodigoIbama = '" + pCodigoIbama + "' \n ";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pNumeroMTRe != 0)
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
        public void Inserir(clsMTReConferenciaDiaria pMTReConferenciaDiaria)
        {
            try
            {
                s = "";
                s = s + "insert into MTReConferenciaDiaria \n";
                s = s + "( \n";
                s = s + "  NumeroMTRe, Data, Quantidade, NumeroCDFe, CodigoIBAMA, QtdeUnidade, CNPJ_CPF_Cliente, Obs  \n"; 
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                
                s = s + " " + pMTReConferenciaDiaria.NumeroMTRe + ", \n";
                
                if (pMTReConferenciaDiaria.Data.ToShortDateString() != "")
                    s = s + "'" + pMTReConferenciaDiaria.Data.ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";

                if (pMTReConferenciaDiaria.Quantidade > 0)
                    s = s + " " + pMTReConferenciaDiaria.Quantidade.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " " + (pMTReConferenciaDiaria.QtdeUnidade * 1000).ToString().Replace(",", ".") + ", \n";

                s = s + " " + pMTReConferenciaDiaria.NumeroCDFe + ", \n";

                s = s + "'" + pMTReConferenciaDiaria.CodigoIBAMA + "', \n";

                s = s + " " + (pMTReConferenciaDiaria.QtdeUnidade * 1000).ToString().Replace(",", ".") + ", \n";

                s = s + "'" + pMTReConferenciaDiaria.CNPJ_CPF_Cliente + "', \n";

                s = s + "'" + pMTReConferenciaDiaria.Obs + "' \n";

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
        public string Alterar(clsMTReConferenciaDiaria pMTReConferenciaDiaria, long pNumeroMTRe)
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
                s = s + "update MTReConferenciaDiaria \n";                
                if (pMTReConferenciaDiaria.Data.ToShortDateString() != "")
                    s = s + " set Data = '" + pMTReConferenciaDiaria.Data.ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + " set Data = '0001-01-01', \n";
                if (pMTReConferenciaDiaria.Quantidade > 0)
                    s = s + " Quantidade = " + pMTReConferenciaDiaria.Quantidade.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " Quantidade = " + (pMTReConferenciaDiaria.QtdeUnidade * 1000).ToString().Replace(",", ".") + ", \n";
                s = s + " NumeroCDFe = " + pMTReConferenciaDiaria.NumeroCDFe + ", \n";
                s = s + " CodigoIBAMA = '" + pMTReConferenciaDiaria.CodigoIBAMA + "', \n";
                s = s + " QtdeUnidade = " + (pMTReConferenciaDiaria.QtdeUnidade * 1000).ToString().Replace(",", ".") + ", \n";
                s = s + " CNPJ_CPF_Cliente = '" + pMTReConferenciaDiaria.CNPJ_CPF_Cliente + "', \n";
                s = s + " Obs = '" + pMTReConferenciaDiaria.Obs + "' \n";

                s = s + "where NumeroMTRe = '" + pNumeroMTRe + "' \n";
                s = s + "and   CodigoIBAMA = '" + pMTReConferenciaDiaria.CodigoIBAMA + "' \n"; 
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
                s = s + "select Sequencial, NumeroMTRe, Data, Quantidade, NumeroCDFe, Obs \n"; 
                s = s + "from   MTReConferenciaDiaria \n";
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

        public DataTable PreencheDataTable(string pOrdem, string pFiltro, string pNumeroMTRe)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial, NumeroMTRe, Data, Quantidade, NumeroCDFe, Obs \n"; 
                s = s + "from   MTReConferenciaDiaria \n";
                s = s + "where " + pNumeroMTRe + " like '" + pFiltro + "%' \n";
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

        public string Excluir(int pSequencial)
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
                    s = s + "delete from MTReConferenciaDiaria ";
                    s = s + "where  Sequencial = " + pSequencial.ToString();
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
            return string.Empty;
        }

        public string Excluir(Int64 pNumeroMTRe)
        {
            if (pNumeroMTRe > 0)
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
                    s = s + "delete from MTReConferenciaDiaria ";
                    s = s + "where  NumeroMTRe = " + pNumeroMTRe.ToString();
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
            return string.Empty;
        }
    }
}