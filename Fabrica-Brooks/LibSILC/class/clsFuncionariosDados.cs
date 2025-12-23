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
	public class clsFuncionarioDados
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
        public clsFuncionarios PegaDados(clsFuncionarios pFuncionarios, int pCodigo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Bairro, Cidade, Endereco, UF, DataAdmissao, DataDemissao, NomeConta, Nome, CEP, CPF, NumeroCTPS, RG, Serie, Fone, PercentualComissao \n";
                s = s + "from   Funcionarios ";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " ";
                    s = s + "limit 1";
                }
                else if (pCodigo == 0)
                {
                    s = s + " order by Nome limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows[0]["Codigo"].ToString() != "")
                        pFuncionarios.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"].ToString());
                    pFuncionarios.Bairro = l_dt.Rows[0]["Bairro"].ToString();
                    pFuncionarios.Cidade = l_dt.Rows[0]["Cidade"].ToString();
                    pFuncionarios.Endereco = l_dt.Rows[0]["Endereco"].ToString();
                    pFuncionarios.UF = l_dt.Rows[0]["UF"].ToString();
                    pFuncionarios.NomeConta = l_dt.Rows[0]["NomeConta"].ToString();
                    if (l_dt.Rows[0]["DataAdmissao"].ToString() != "")
                        pFuncionarios.DataAdmissao = Convert.ToDateTime(l_dt.Rows[0]["DataAdmissao"]).ToShortDateString();
                    if (l_dt.Rows[0]["DataDemissao"].ToString() != "")
                        pFuncionarios.DataDemissao = Convert.ToDateTime(l_dt.Rows[0]["DataDemissao"]).ToShortDateString();
                    pFuncionarios.Nome = l_dt.Rows[0]["Nome"].ToString();
                    pFuncionarios.CEP = l_dt.Rows[0]["CEP"].ToString();
                    pFuncionarios.CPF = l_dt.Rows[0]["CPF"].ToString();
                    pFuncionarios.NumeroCTPS = l_dt.Rows[0]["NumeroCTPS"].ToString();
                    pFuncionarios.RG = l_dt.Rows[0]["RG"].ToString();
                    pFuncionarios.Serie = l_dt.Rows[0]["Serie"].ToString();
                    pFuncionarios.Fone = l_dt.Rows[0]["Fone"].ToString();
                    if (l_dt.Rows[0]["PercentualComissao"].ToString() != "")
                        pFuncionarios.PercentualComissao = Convert.ToInt16(l_dt.Rows[0]["PercentualComissao"]);
                }            
            }
            catch (Exception ex)
            {
                pFuncionarios = new clsFuncionarios();
            }
            finally
            {
                DesconectaBanco();
            }
            return pFuncionarios;
        }
        
        public DataTable PegaDados(clsFuncionarios pFuncionarios, int pCodigo, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Bairro, Cidade, Endereco, UF, DataAdmissao, DataDemissao, NomeConta, Nome, CEP, CPF, NumeroCTPS, RG, Serie, Fone, PercentualComissao \n"; 
                s = s + "from   Funcionarios ";
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
                    s = s + " order by Nome ";
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
        public DataTable PegaDadosLista(int pCodigo, string pNome)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Nome \n";
                s = s + "from   Funcionarios \n";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " ";
                }
                else if (pCodigo == 0)
                {
                    s = s + "where    Nome like '%" + pNome + "%' \n";
                    s = s + "and      (DataDemissao is null or DataDemissao < '2000-01-01') \n";
                    s = s + "order by Nome \n";
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
                s = s + "from Funcionarios ";
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
                s = s + "select Codigo ";
                s = s + "from   Funcionarios ";
                if (pCodigo != 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " ";
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
        public int PegaCodigoMotorista(string pNome)
        {
            int iRet = 0;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Codigo ";
                s = s + "from   Funcionarios ";
                s = s + "where  Nome = '" + pNome + "' \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    iRet = Convert.ToInt16(l_dt.Rows[0][0]);
            }
            catch (Exception ex)
            {
                iRet = -1;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return iRet;
        }
        public void Inserir(clsFuncionarios pFuncionarios, int pCodigo = 0)
        {
            try
            {
                s = "";
                s = s + "insert into Funcionarios \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + "Codigo,";
                s = s + "  Bairro, Cidade, Endereco, UF, DataAdmissao, DataDemissao, NomeConta, Nome, CEP, CPF, NumeroCTPS, RG, Serie, Fone, PercentualComissao \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + pCodigo.ToString() + ", \n";
                s = s + "'" + pFuncionarios.Bairro + "', \n";
                s = s + "'" + pFuncionarios.Cidade  + "', \n";
                s = s + "'" + pFuncionarios.Endereco  + "', \n";
                s = s + "'" + pFuncionarios.UF    + "', \n";

                if (pFuncionarios.DataAdmissao != "")
                    s = s + "'" + Convert.ToDateTime(pFuncionarios.DataAdmissao).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";

                if (pFuncionarios.DataDemissao != "")
                    s = s + "'" + Convert.ToDateTime(pFuncionarios.DataDemissao).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";

                s = s + "'" + pFuncionarios.NomeConta + "', \n";
                s = s + "'" + pFuncionarios.Nome + "', \n";
                s = s + "'" + pFuncionarios.CEP  + "', \n";
                s = s + "'" + pFuncionarios.CPF  + "', \n";
                s = s + "'" + pFuncionarios.NumeroCTPS + "', \n";
                s = s + "'" + pFuncionarios.RG   + "', \n";
                s = s + "'" + pFuncionarios.Serie + "', \n";
                s = s + "'" + pFuncionarios.Fone + "', \n";
                if (pFuncionarios.PercentualComissao > 0)
                    s = s + " " + pFuncionarios.PercentualComissao + " \n";
                else
                    s = s + "0.00";
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
        public string Alterar(clsFuncionarios pFuncionarios, int pCodigo)
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
                s = s + "update Funcionarios \n";
                s = s + "set   Bairro       = '" + pFuncionarios.Bairro + "', \n";
                s = s + "      Cidade       = '" + pFuncionarios.Cidade + "', \n";
                s = s + "      Endereco     = '" + pFuncionarios.Endereco + "', \n";
                s = s + "      UF           = '" + pFuncionarios.UF + "', \n";
                if (pFuncionarios.DataAdmissao != "")
                    s = s + "      DataAdmissao = '" + Convert.ToDateTime(pFuncionarios.DataAdmissao).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "      DataAdmissao = '0001-01-01', \n";
                if (pFuncionarios.DataDemissao != "")
                    s = s + "      DataDemissao = '" + Convert.ToDateTime(pFuncionarios.DataDemissao).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "      DataDemissao = '0001-01-01', \n";
                s = s + "      NomeConta    = '" + pFuncionarios.NomeConta + "', \n";
                s = s + "      Nome         = '" + pFuncionarios.Nome + "', \n";
                s = s + "      CEP          = '" + pFuncionarios.CEP + "', \n";
                s = s + "      CPF          = '" + pFuncionarios.CPF + "', \n";
                s = s + "      NumeroCTPS   = '" + pFuncionarios.NumeroCTPS + "', \n";
                s = s + "      RG           = '" + pFuncionarios.RG + "', \n";
                s = s + "      Serie        = '" + pFuncionarios.Serie + "', \n";
                s = s + "      Fone         = '" + pFuncionarios.Fone + "', \n";
                if (pFuncionarios.PercentualComissao == 0)
                    s = s + "      PercentualComissao = 0 \n";
                else
                    s = s + "      PercentualComissao = " + pFuncionarios.PercentualComissao + " \n";
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
        public DataTable PreencheDataTableFuncionarios(string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Nome ";
                s = s + "from   Funcionarios order by " + pOrdem;
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

        public DataTable PreencheDataTableFuncionarios(string pOrdem, string pFiltro, string pCampo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Nome, Cidade \n";
                s = s + "from   Funcionarios \n";
                if (pCampo == "NaoDemitidos")
                    s = s + "where (DataDemissao = '0100-01-01' or DataDemissao is null) \n";
                else 
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

        public string Excluir(int pCodigo, string pNome)
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
                s = s + "delete from Funcionarios ";
                if (pCodigo > 0)
                {
                    s = s + "where Codigo = " + pCodigo.ToString();
                }
                else
                {
                    s = s + "where Nome = '" + pNome + "'";
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