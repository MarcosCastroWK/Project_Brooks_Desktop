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
	public class clsFornecedoresDados
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
        public clsFornecedores PegaDados(clsFornecedores pFornecedores, int pCodigo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Nome, NomeFantasia, Contato, Endereco, Bairro, CEP, Cidade, \n";
                s = s + "       UF, Fone1, Fone2, Celular, CNPJ, IE, email, site \n"; 
                s = s + "from   Fornecedores ";
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
                    pFornecedores.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                    pFornecedores.Nome = l_dt.Rows[0]["Nome"].ToString();
                    pFornecedores.NomeFantasia = l_dt.Rows[0]["NomeFantasia"].ToString();
                    pFornecedores.Contato = l_dt.Rows[0]["Contato"].ToString();
                    pFornecedores.Endereco = l_dt.Rows[0]["Endereco"].ToString();
                    pFornecedores.Bairro = l_dt.Rows[0]["Bairro"].ToString();
                    pFornecedores.CEP = l_dt.Rows[0]["CEP"].ToString();                    
                    pFornecedores.Cidade = l_dt.Rows[0]["Cidade"].ToString();
                    pFornecedores.UF = l_dt.Rows[0]["UF"].ToString();
                    pFornecedores.Fone1 = l_dt.Rows[0]["Fone1"].ToString();
                    pFornecedores.Fone2 = l_dt.Rows[0]["Fone2"].ToString();
                    pFornecedores.Celular = l_dt.Rows[0]["Celular"].ToString();
                    pFornecedores.CNPJ = l_dt.Rows[0]["CNPJ"].ToString();
                    pFornecedores.IE = l_dt.Rows[0]["IE"].ToString();
                    pFornecedores.email = l_dt.Rows[0]["email"].ToString();
                    pFornecedores.site = l_dt.Rows[0]["site"].ToString();
                }
            }
            catch (Exception ex)
            {
                pFornecedores = new clsFornecedores();
            }
            finally
            {
                DesconectaBanco();
            }
            return pFornecedores;
        }
        
        public DataTable PegaDados(clsFornecedores pFornecedores, int pCodigo, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Nome, NomeFantasia, Contato, Endereco, Bairro, CEP, Cidade, \n";
                s = s + "       UF, Fone1, Fone2, Celular, CNPJ, IE, email, site \n";
                s = s + "from   Fornecedores ";
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
        public int PegaTamanhoCampoVarChar(string pCampo)
        {
            int iRet = 0;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "show columns ";
                s = s + "from Fornecedores ";
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
                s = s + "from   Fornecedores ";
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
        public void Inserir(clsFornecedores pFornecedores)
        {
            try
            {
                s = "";
                s = s + "insert into Fornecedores \n";
                s = s + "( \n";
                s = s + "  Nome, NomeFantasia, Contato, Endereco, Bairro, CEP, Cidade, \n";
                s = s + "  UF, Fone1, Fone2, Celular, CNPJ, IE, email, site \n"; 
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                s = s + "'" + pFornecedores.Nome + "', \n";
                s = s + "'" + pFornecedores.NomeFantasia + "', \n";
                s = s + "'" + pFornecedores.Contato + "', \n";
                s = s + "'" + pFornecedores.Endereco + "', \n";
                s = s + "'" + pFornecedores.Bairro + "', \n";
                s = s + "'" + pFornecedores.CEP + "', \n";
                s = s + "'" + pFornecedores.Cidade  + "', \n";
                s = s + "'" + pFornecedores.UF + "', \n";
                s = s + "'" + pFornecedores.Fone1 + "', \n";
                s = s + "'" + pFornecedores.Fone2 + "', \n";
                s = s + "'" + pFornecedores.Celular + "', \n";
                s = s + "'" + pFornecedores.CNPJ + "', \n";
                s = s + "'" + pFornecedores.IE + "', \n";
                s = s + "'" + pFornecedores.email + "', \n";
                s = s + "'" + pFornecedores.site + "' \n";
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
        public string Alterar(clsFornecedores pFornecedores, int pCodigo)
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
                s = s + "update Fornecedores \n";
                s = s + "set Nome          = '" + pFornecedores.Nome + "', \n";
                s = s + "    NomeFantasia  = '" + pFornecedores.NomeFantasia + "', \n";
                s = s + "    Contato       = '" + pFornecedores.Contato + "', \n";
                s = s + "    Endereco      = '" + pFornecedores.Endereco + "', \n";
                s = s + "    Bairro        = '" + pFornecedores.Bairro + "', \n";
                s = s + "    CEP           = '" + pFornecedores.CEP + "', \n";
                s = s + "    Cidade        = '" + pFornecedores.Cidade + "', \n";
                s = s + "    Endereco      = '" + pFornecedores.Endereco + "', \n";
                s = s + "    UF            = '" + pFornecedores.UF + "', \n";
                s = s + "    Fone1         = '" + pFornecedores.Fone1 + "', \n";
                s = s + "    Fone2         = '" + pFornecedores.Fone2 + "', \n";
                s = s + "    Celular       = '" + pFornecedores.Celular + "', \n";
                s = s + "    CNPJ           = '" + pFornecedores.CNPJ + "', \n";
                s = s + "    IE            = '" + pFornecedores.IE + "', \n";
                s = s + "    email         = '" + pFornecedores.email + "', \n";
                s = s + "    site          = '" + pFornecedores.site + "', \n";

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
        public DataTable PreencheDataTableFornecedores(string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Nome ";
                s = s + "from   Fornecedores order by " + pOrdem;
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

        public DataTable PreencheDataTableFornecedores(string pOrdem, string pFiltro, string pCampo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Nome, Cidade \n";
                s = s + "from   Fornecedores \n";
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
                s = s + "delete from Fornecedores ";
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