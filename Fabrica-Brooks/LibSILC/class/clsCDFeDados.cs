using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.OleDb;
using SILCNegocios;

namespace LibSILC
{
    public class clsCDFeDados
	{
	    private clsDB oDB = new clsDB();
        private MySqlDataAdapter l_myData = new MySqlDataAdapter();
        private DataSet l_ds = new DataSet();
        DataTable l_dt = new DataTable();
        private string s;

        // funções locais para multi banco de dados
        public void ConectaBanco()
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
        public void DesconectaBanco()
        {
            oDB.DesconectaMySql();
        }
        public clsCDFe PegaDados(clsCDFe pCDFe, int pSequencial)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial, NumeroMTRe, Data, Quantidade, NumeroCDFe, Placas \n"; 
                s = s + "from   CDFe \n";
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
                    pCDFe = ReturnCDFe(l_dt);        

                }
                return pCDFe;
            }
            catch (Exception ex)
            {
                clsCDFe oCDFe = new clsCDFe();
                oCDFe.MensagemErro = ex.Message;
                return oCDFe;
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public bool ExisteMTReComCodigoIbamaDiferente(string pNumeroMTRe)
        {
            clsCDFe pCDFe = new clsCDFe();
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select cd.CodigoIBAMA, i.CodigoIBAMA \n";
                s = s + "from   CDFe cd \n";
                s = s + "inner  join LancamentoMTR lmtr on lmtr.NumeroMTRFatima = cd.NumeroMTRe \n";
                s = s + "inner  join Residuos r on r.Codigo = lmtr.CodigoResiduo  \n";
                s = s + "inner  join IBAMA i on i.Codigo = r.CodigoIBAMA  \n";                
                s = s + "where  cd.NumeroMTRe = '" + pNumeroMTRe + "' \n";
                s = s + "and    cd.CodigoIBAMA <> i.CodigoIBAMA \n";
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows.Count > 0)
                        return true;
                    if (l_dt.Rows.Count == 0)
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                pCDFe.MensagemErro = ex.Message;
                return false;
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public string ExisteMTReComMesmoCodigoIbama(string pNumeroMTRe, string pCodigoIBAMA)
        {
            string sRet = "";
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select cd.CodigoIBAMA, i.CodigoIBAMA \n";
                s = s + "from   CDFe cd \n";
                s = s + "inner  join LancamentoMTR lmtr on lmtr.NumeroMTRFatima = cd.NumeroMTRe \n";
                s = s + "inner  join Residuos r on r.Codigo = lmtr.CodigoResiduo  \n";
                s = s + "inner  join IBAMA i on i.Codigo = r.CodigoIBAMA  \n";
                s = s + "where  cd.NumeroMTRe  = '" + pNumeroMTRe + "' \n";
                s = s + "and    cd.CodigoIBAMA = '" + pCodigoIBAMA + "' \n";
                //s = s + "and    cd.CodigoIBAMA <> i.CodigoIBAMA \n";
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows.Count > 0)
                        sRet = l_dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                DesconectaBanco();
            }
            return sRet;
        }
        public bool ExisteMTReCodigoIbamaDiferente(string pNumeroMTRe, string pCodigoIbama)
        {
            bool bRet = false;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select cd.CodigoIBAMA, i.CodigoIBAMA, (select count(*) from cdfe where (data >= '20230801' and data <= '20230830')) as LnsRelatorio \n";
                s = s + "from   CDFe cd \n";
                s = s + "inner  join LancamentoMTR lmtr on lmtr.NumeroMTRFatima = cd.NumeroMTRe \n";
                s = s + "inner  join Residuos r on r.Codigo = lmtr.CodigoResiduo  \n";
                s = s + "inner  join IBAMA i on i.Codigo = r.CodigoIBAMA  \n";
                s = s + "where  cd.NumeroMTRe  = '" + pNumeroMTRe + "' \n";
                s = s + "and    cd.CodigoIbama = '" + pCodigoIbama + "' \n";
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows.Count > 0)
                    {
                        if (l_dt.Rows[0]["LnsRelatorio"].ToString() != "")
                            if (Convert.ToInt32(l_dt.Rows[0]["LnsRelatorio"].ToString()) > 1)
                                bRet = true;
                    }
                }
            }
            finally
            {
                DesconectaBanco();
            }
            return bRet;
        }
        public string PegaNumeroCDFe(string pNumeroMTRe, string pCodigoIBAMA)
        {
            string sRet = "";
            try
            {
                if (pNumeroMTRe != "")
                {
                    ConectaBanco();
                    s = "";
                    s = s + "select cd.NumeroCDFe \n";
                    s = s + "from   CDFe cd \n";
                    s = s + "where  cd.NumeroMTRe = '" + pNumeroMTRe + "' \n";
                    if (pCodigoIBAMA != "")
                        s = s + "and    cd.CodigoIBAMA = '" + pCodigoIBAMA + "' \n";
                    FillDataSet();
                    if (l_ds.Tables.Count > 0)
                    {
                        l_dt = l_ds.Tables[0];
                        if (l_dt.Rows.Count > 0)
                            sRet = l_dt.Rows[0][0].ToString();
                        else
                        {
                            s = "";
                            s = s + "select cd.NumeroCDFe \n";
                            s = s + "from   CDFe cd \n";
                            s = s + "where  cd.NumeroMTRe = '" + pNumeroMTRe + "' \n";
                            // A diferença é o código do IBAMA
                            FillDataSet();
                            if (l_ds.Tables.Count > 0)
                            {
                                l_dt = l_ds.Tables[0];
                                if (l_dt.Rows.Count > 0)
                                    sRet = l_dt.Rows[0][0].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                DesconectaBanco();
            }
            return sRet;
        }
        public decimal RetornaTotalCDFe(string pNumeroMTRe, string pCodigoIBAMA)
        {
            decimal vRet = 0;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select sum(Quantidade) as QuantidadeCDFe \n";
                s = s + "from   CDFe \n";
                s = s + "where  NumeroMTRe = '" + pNumeroMTRe + "' \n";
                if (pCodigoIBAMA != "")
                    s = s + "and    CodigoIBAMA = '" + pCodigoIBAMA + "' \n";
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows.Count > 0)
                    {
                        if (l_dt.Rows[0][0].ToString() != "")
                            vRet = Convert.ToDecimal(l_dt.Rows[0][0].ToString());
                    }
                }
            }
            finally
            {
                DesconectaBanco();
            }
            return vRet;
        }
        private clsCDFe ReturnCDFe(DataTable l_dt)
        {
            clsCDFe pCDFe = new clsCDFe();

            pCDFe.Sequencial = Convert.ToInt32(l_dt.Rows[0]["Sequencial"]);

            if (l_dt.Rows[0]["NumeroMTRe"].ToString() != "")
                pCDFe.NumeroMTRe = Convert.ToInt64(l_dt.Rows[0]["NumeroMTRe"]);

            if (l_dt.Rows[0]["Data"].ToString() != "")
                pCDFe.Data = Convert.ToDateTime(l_dt.Rows[0]["Data"]);

            if (l_dt.Rows[0]["Quantidade"].ToString() != "")
                pCDFe.Quantidade = Convert.ToDecimal(l_dt.Rows[0]["Quantidade"]);

            if (l_dt.Rows[0]["NumeroCDFe"].ToString() != "")
                pCDFe.NumeroCDFe = Convert.ToInt32(l_dt.Rows[0]["NumeroCDFe"]);

            pCDFe.CodigoIBAMA = l_dt.Rows[0]["CodigoIBAMA"].ToString();
            pCDFe.Placas = l_dt.Rows[0]["Placas"].ToString();

            return pCDFe;
        }        
        public DataTable PegaDados(clsCDFe pCDFe, int pNumeroMTRe, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial, NumeroMTRe, Data, Quantidade, NumeroCDFe, CodigoIbama, Placas \n"; 
                s = s + "from   CDFe \n";
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
                    pCDFe = ReturnCDFe(l_dt);
              
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
        public int PegaTamanhoNumeroMTReVarChar(string pNumeroMTRe)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "show columns ";
                s = s + "from CDFe ";
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
        public string RetornaMTReExisteEmClienteDiferente(string pNumeroMTRe, string pCNPJCPF)
        {
            string sRet = "";
            try
            {
                if (pNumeroMTRe == "2001030169")
                    s = "";
                oDB.ConectaMySql();
                s = "";
                s = s + "select CNPJ_CPF_Cliente \n";
                s = s + "from   CDFe \n";
                s = s + "where  NumeroMTRe = '" + pNumeroMTRe + "' \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                {
                    if (geral.RetiraLetras(l_dt.Rows[0]["CNPJ_CPF_Cliente"].ToString()) != geral.RetiraLetras(pCNPJCPF))
                        sRet = l_dt.Rows[0]["CNPJ_CPF_Cliente"].ToString();
                }
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }
        public string DadoExiste(Int64 pNumeroMTRe, string pCodigoIbama = "")
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select NumeroMTRe ";
                s = s + "from   CDFe ";
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
        public void Inserir(clsCDFe pCDFe)
        {
            try
            {
                s = "";
                s = s + "insert into CDFe \n";
                s = s + "( \n";
                s = s + "  NumeroMTRe, Data, Quantidade, NumeroCDFe, CodigoIBAMA, QtdeUnidade, CNPJ_CPF_Cliente, Situacao, Placas \n"; 
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                
                s = s + " " + pCDFe.NumeroMTRe + ", \n";
                
                if (pCDFe.Data.ToShortDateString() != "")
                    s = s + "'" + pCDFe.Data.ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";

                if (pCDFe.Quantidade > 0)
                    s = s + " " + pCDFe.Quantidade.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " " + (pCDFe.QtdeUnidade * 1000).ToString().Replace(",", ".") + ", \n";

                s = s + " " + pCDFe.NumeroCDFe + ", \n";

                s = s + "'" + pCDFe.CodigoIBAMA + "', \n";

                s = s + " " + (pCDFe.QtdeUnidade * 1000).ToString().Replace(",", ".") + ", \n";

                s = s + "'" + pCDFe.CNPJ_CPF_Cliente + "', \n";

                s = s + "'" + pCDFe.Situacao + "', \n";

                s = s + "'" + pCDFe.Placas + "' \n";

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
        public string Alterar(clsCDFe pCDFe, long pNumeroMTRe)
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
                s = s + "update CDFe \n";                
                if (pCDFe.Data.ToShortDateString() != "")
                    s = s + " set Data = '" + pCDFe.Data.ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + " set Data = '0001-01-01', \n";
                if (pCDFe.Quantidade > 0)
                    s = s + " Quantidade = " + pCDFe.Quantidade.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " Quantidade = " + (pCDFe.QtdeUnidade * 1000).ToString().Replace(",", ".") + ", \n";
                s = s + " NumeroCDFe = " + pCDFe.NumeroCDFe + ", \n";
                s = s + " CodigoIBAMA = '" + pCDFe.CodigoIBAMA + "', \n";
                s = s + " QtdeUnidade = " + (pCDFe.QtdeUnidade * 1000).ToString().Replace(",", ".") + ", \n";
                s = s + " CNPJ_CPF_Cliente = '" + pCDFe.CNPJ_CPF_Cliente + "', \n";
                s = s + " Situacao = '" + pCDFe.Situacao + "', \n";
                s = s + " Placas = '" + pCDFe.Placas + "' \n";

                s = s + "where NumeroMTRe = '" + pNumeroMTRe + "' \n";
                s = s + "and   CodigoIBAMA = '" + pCDFe.CodigoIBAMA + "' \n"; 
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
                s = s + "select Sequencial, NumeroMTRe, Data, Quantidade, NumeroCDFe, Placas \n"; 
                s = s + "from   CDFe \n";
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
                s = s + "select Sequencial, NumeroMTRe, Data, Quantidade, NumeroCDFe, Placas \n"; 
                s = s + "from   CDFe \n";
                s = s + "where " + pNumeroMTRe + " like '%" + pFiltro + "%' \n";
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

        public string Excluir(int pSequencial, string pNumeroMTRe = "", bool bConectarDB = true, bool bDesconectarDB = true)
        {
            if (pSequencial > 0 || pNumeroMTRe != "")
            {
                if (bConectarDB)
                    ConectaBanco();
                MySqlCommand command = oDB.MySqlConnect.CreateCommand();
                MySqlTransaction transaction;
                transaction = oDB.MySqlConnect.BeginTransaction();
                try
                {
                    command.Connection = oDB.MySqlConnect;
                    command.Transaction = transaction;
                    s = "";
                    s = s + "delete from CDFe ";
                    if (pSequencial > 0)
                        s = s + "where  Sequencial = " + pSequencial.ToString();
                    else
                        s = s + "where  NumeroMTRe = '" + pNumeroMTRe + "' \n";
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                    return string.Empty;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    bDesconectarDB = true;
                    return ex.Message.ToString();
                }
                finally
                {
                    if (bDesconectarDB)
                        oDB.DesconectaMySql();
                }
            }
            return string.Empty;
        }
    }
}