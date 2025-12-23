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
    public class clsBlocosMTRDados
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
        public clsBlocosMTR PegaDados(clsBlocosMTR pBlocosMTR, int pSequencial)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select b.Sequencial, b.Data, b.CodigoMotorista, f.Nome as NomeMotorista, b.NumeroBloco, b.NuMTRInicial, b.NuMTRFinal, b.CoeficienteNumeracao \n";
                s = s + "from   BlocosMTR b \n";
                s = s + "inner  join Funcionarios f on f.Codigo = b.CodigoMotorista \n ";
                if (pSequencial > 0)
                {
                    s = s + "where  Sequencial = " + pSequencial + " ";
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
                    pBlocosMTR.Sequencial = Convert.ToInt32(l_dt.Rows[0]["Sequencial"]);
                    if (l_dt.Rows[0]["Data"].ToString() != "")
                        pBlocosMTR.Data = Convert.ToDateTime(l_dt.Rows[0]["Data"].ToString()).Date.ToShortDateString();
                    if (l_dt.Rows[0]["CodigoMotorista"].ToString() != "")
                        pBlocosMTR.CodigoMotorista = Convert.ToInt32(l_dt.Rows[0]["CodigoMotorista"]);
                    pBlocosMTR.NomeMotorista = l_dt.Rows[0]["NomeMotorista"].ToString();
                    if (l_dt.Rows[0]["NumeroBloco"].ToString() != "")
                        pBlocosMTR.NumeroBloco = Convert.ToInt32(l_dt.Rows[0]["NumeroBloco"]);
                    if (l_dt.Rows[0]["NuMTRInicial"].ToString() != "")
                        pBlocosMTR.NuMTRInicial = Convert.ToInt32(l_dt.Rows[0]["NuMTRInicial"]);
                    if (l_dt.Rows[0]["NuMTRFinal"].ToString() != "")
                        pBlocosMTR.NuMTRFinal = Convert.ToInt32(l_dt.Rows[0]["NuMTRFinal"]);
                    if (l_dt.Rows[0]["CoeficienteNumeracao"].ToString() != "")
                        pBlocosMTR.CoeficienteNumeracao = Convert.ToInt32(l_dt.Rows[0]["CoeficienteNumeracao"]);
                }
                return pBlocosMTR;
            }
            catch (Exception ex)
            {
                return new clsBlocosMTR();
            }
            finally
            {
                DesconectaBanco();
            }
        }
        
        public DataTable PegaDados(clsBlocosMTR pBlocosMTR, int pSequencial, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select b.Sequencial, b.Data, b.CodigoMotorista, f.Nome as NomeMotorista, b.NumeroBloco, b.NuMTRInicial, b.NuMTRFinal, b.CoeficienteNumeracao \n";
                s = s + "from   BlocosMTR b \n";
                s = s + "inner  join Funcionarios f on f.Codigo = b.CodigoMotorista \n ";
                if (pSequencial > 0)
                {
                    s = s + "where  Sequencial = " + pSequencial + " ";
                }
                if (pUltimoRegistro)
                {
                    s = s + "order by Sequencial desc ";
                }
                else if (pSequencial == 0)
                {
                    s = s + " order by NomeMotorista ";
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
                s = s + "from BlocosMTR ";
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
                oDB.ConectaMySql();
                s = "";
                s = s + "select Sequencial ";
                s = s + "from   BlocosMTR ";
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
                oDB.DesconectaMySql();
            }
        }
        public void Inserir(clsBlocosMTR pBlocosMTR, int pSequencial = 0)
        {
            try
            {
                s = "";
                s = s + "insert into BlocosMTR \n";
                s = s + "( \n";
                if (pSequencial > 0)
                    s = s + " Sequencial, \n";
                s = s + "   Data, CodigoMotorista, NumeroBloco, NuMTRInicial, NuMTRFinal, CoeficienteNumeracao \n"; 
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";

                if (pSequencial > 0)
                    s = s + pSequencial.ToString() + ", \n";

                if (pBlocosMTR.Data != "")
                    s = s + "'" + Convert.ToDateTime(pBlocosMTR.Data).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";

                if (pBlocosMTR.CodigoMotorista > 0)
                    s = s + " " + pBlocosMTR.CodigoMotorista.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";

                if (pBlocosMTR.NumeroBloco == 0)
                    s = s + " 0, \n";
                else
                    s = s + " " + pBlocosMTR.NumeroBloco.ToString().Replace(",", ".") + ", \n";
                if (pBlocosMTR.NuMTRInicial == 0)
                    s = s + " 0, \n";
                else 
                    s = s + " " + pBlocosMTR.NuMTRInicial + ", \n";
                if (pBlocosMTR.NuMTRFinal == 0)
                    s = s + " 0, \n";
                else
                    s = s + " " + pBlocosMTR.NuMTRFinal + ", \n";
                if (pBlocosMTR.CoeficienteNumeracao == 0)
                    s = s + " 0 \n";
                else
                    s = s + " " + pBlocosMTR.CoeficienteNumeracao + " \n";

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
        public string Alterar(clsBlocosMTR pBlocosMTR, int pSequencial)
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
                s = s + "update BlocosMTR \n";
                s = s + "set   Data  = '" + Convert.ToDateTime(pBlocosMTR.Data).ToString("yyyy-MM-dd") + "', \n";
                if (pBlocosMTR.CodigoMotorista == 0)
                    s = s + "  CodigoMotorista = 0, \n";
                else             
                    s = s + "  CodigoMotorista       = " + pBlocosMTR.CodigoMotorista.ToString().Replace(",", ".") + ", \n";
                if (pBlocosMTR.NumeroBloco == 0)
                    s = s + "  NumeroBloco = 0, \n";
                else
                    s = s + "  NumeroBloco = "  + pBlocosMTR.NumeroBloco.ToString().Replace(",", ".") + ", \n";
                if (pBlocosMTR.NuMTRInicial == 0)
                    s = s + "  NuMTRInicial = 0, \n";
                else             
                    s = s + "  NuMTRInicial        = '" + pBlocosMTR.NuMTRInicial + "', \n";
                if (pBlocosMTR.NuMTRFinal == 0)
                    s = s + "  NuMTRFinal    = 0, \n";
                else
                    s = s + "  NuMTRFinal    = " + pBlocosMTR.NuMTRFinal + ", \n";
                if (pBlocosMTR.CoeficienteNumeracao == 0)
                    s = s + "      CoeficienteNumeracao = 0 \n";
                else
                    s = s + "      CoeficienteNumeracao = "  + pBlocosMTR.CoeficienteNumeracao + " \n";
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
        public DataTable PreencheDataTableBlocosMTR(string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select b.Sequencial, b.Data, b.CodigoMotorista, f.Nome as NomeMotorista, b.NumeroBloco, b.NuMTRInicial, b.NuMTRFinal, b.CoeficienteNumeracao \n";
                s = s + "from   BlocosMTR b \n";
                s = s + "inner  join Funcionarios f on f.Codigo = b.CodigoMotorista \n ";
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

        public DataTable PreencheDataTableBlocosMTR(string pOrdem, string pFiltro, string pCampo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select b.Sequencial, b.Data, b.CodigoMotorista, f.Nome as NomeMotorista, b.NumeroBloco, b.NuMTRInicial, b.NuMTRFinal, b.CoeficienteNumeracao \n";
                s = s + "from   BlocosMTR b \n";
                s = s + "inner  join Funcionarios f on f.Codigo = b.CodigoMotorista \n "; 
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

        public string Excluir(int pSequencial)
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
                s = s + "delete from BlocosMTR ";
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
    }
}