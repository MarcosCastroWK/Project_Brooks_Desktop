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
	public class clsParametrosDados
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
        public clsParametros PegaDados(clsParametros pParametros, int pNumero)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Numero, Nome, Endereco, CEP, Cidade, UF, CNPJ_CPF, IE_RG, Site, Email, Telefones, \n";
                s = s + "       CodigoMunicipio, FormularioContinuo, AtualizaRoteiroSemanal, AtualizaRoteiroMensal,  \n";
                s = s + "       PercentualISS  \n"; 
                s = s + "from   Parametros ";
                if (pNumero > 0)
                {
                    s = s + "where  Numero = " + pNumero + " ";
                    s = s + "limit 1";
                }
                else if (pNumero == 0)
                {
                    s = s + " order by Nome limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows[0]["Numero"].ToString() != "")
                        pParametros.Numero = Convert.ToInt32(l_dt.Rows[0]["Numero"]);
                    pParametros.Nome = l_dt.Rows[0]["Nome"].ToString();
                    pParametros.Endereco = l_dt.Rows[0]["Endereco"].ToString();
                    pParametros.CEP = l_dt.Rows[0]["CEP"].ToString();
                    pParametros.Cidade = l_dt.Rows[0]["Cidade"].ToString();
                    pParametros.UF = l_dt.Rows[0]["UF"].ToString();
                    pParametros.CNPJ_CPF = l_dt.Rows[0]["CNPJ_CPF"].ToString();
                    pParametros.IE_RG = l_dt.Rows[0]["IE_RG"].ToString();
                    pParametros.Site = l_dt.Rows[0]["Site"].ToString();
                    pParametros.Email = l_dt.Rows[0]["Email"].ToString();
                    pParametros.Telefones = l_dt.Rows[0]["Telefones"].ToString();
                    pParametros.CodigoMunicipio = l_dt.Rows[0]["CodigoMunicipio"].ToString();
                    if (l_dt.Rows[0]["FormularioContinuo"].ToString() != "")
                        pParametros.FormularioContinuo = Convert.ToInt16(l_dt.Rows[0]["FormularioContinuo"]);
                    if (l_dt.Rows[0]["AtualizaRoteiroMensal"].ToString() != "")
                        pParametros.AtualizaRoteiroMensal = Convert.ToInt16(l_dt.Rows[0]["AtualizaRoteiroMensal"]);
                    if (l_dt.Rows[0]["PercentualISS"].ToString() != "")
                        pParametros.PercentualISS = Convert.ToInt16(l_dt.Rows[0]["PercentualISS"]);
                }
                
            }
            catch (Exception ex)
            {
                pParametros = new clsParametros();
            }
            finally
            {
                DesconectaBanco();
            }
            return pParametros;
        }
        
        public DataTable PegaDados(clsParametros pParametros, int pNumero, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Numero, Nome, Endereco, CEP, Cidade, UF, CNPJ_CPF, IE_RG, Site, Email, Telefones, \n";
                s = s + "       CodigoMunicipio, FormularioContinuo, AtualizaRoteiroSemanal, AtualizaRoteiroMensal,  \n";
                s = s + "       PercentualISS  \n"; 
                s = s + "from   Parametros \n";
                if (pNumero > 0)
                {
                    s = s + "where  Numero = " + pNumero + " \n";
                }
                if (pUltimoRegistro)
                {
                    s = s + "order by Numero desc \n";
                }
                else if (pNumero == 0)
                {
                    s = s + " order by Nome \n";
                }
                s = s + "limit  1 \n";
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
                s = s + "from Parametros ";
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
        public string DadoExiste(int pNumero)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Numero ";
                s = s + "from   Parametros ";
                if (pNumero != 0)
                {
                    s = s + "where  Numero = " + pNumero + " ";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pNumero != 0)
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
        public void Inserir(clsParametros pParametros)
        {
            try
            {
                s = "";
                s = s + "insert into Parametros \n";
                s = s + "( \n";
                s = s + "  Nome, Endereco, CEP, Cidade, UF, CNPJ_CPF, IE_RG, Site, Email, Telefones, \n";
                s = s + "  CodigoMunicipio, FormularioContinuo, AtualizaRoteiroSemanal, AtualizaRoteiroMensal,  \n";
                s = s + "  PercentualISS  \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                s = s + "'" + pParametros.Nome + "', \n";
                s = s + "'" + pParametros.Endereco + "', \n";
                s = s + "'" + pParametros.CEP + "', \n";
                s = s + "'" + pParametros.Cidade  + "', \n";
                s = s + "'" + pParametros.UF    + "', \n";
                s = s + "'" + pParametros.CNPJ_CPF + "', \n";
                s = s + "'" + pParametros.IE_RG + "', \n";
                s = s + "'" + pParametros.Site + "', \n";
                s = s + "'" + pParametros.Email + "', \n";
                s = s + "'" + pParametros.Telefones + "', \n";
                s = s + "'" + pParametros.CodigoMunicipio + "', \n";
                if (pParametros.FormularioContinuo > 0)
                    s = s + " " + pParametros.FormularioContinuo + ", \n";
                else
                    s = s + "0, ";
                if (pParametros.AtualizaRoteiroSemanal > 0)
                    s = s + " " + pParametros.AtualizaRoteiroSemanal + ", \n";
                else
                    s = s + "0, ";
                if (pParametros.AtualizaRoteiroMensal > 0)
                    s = s + " " + pParametros.AtualizaRoteiroMensal + ", \n";
                else
                    s = s + "0, ";
                if (pParametros.PercentualISS > 0)
                    s = s + " " + pParametros.PercentualISS.ToString().Replace(",", ".") + " \n";
                else
                    s = s + "0 ";
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
        public string Alterar(clsParametros pParametros, int pNumero)
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
                s = s + "update Parametros \n";
                s = s + "set Nome = '" + pParametros.Nome + "', \n";
                s = s + "    Endereco = '" + pParametros.Endereco + "', \n";
                s = s + "    CEP = '" + pParametros.CEP + "', \n";
                s = s + "    Cidade = '" + pParametros.Cidade + "', \n";
                s = s + "    UF = '" + pParametros.UF + "', \n";
                s = s + "    CNPJ_CPF = '" + pParametros.CNPJ_CPF + "', \n";
                s = s + "    IE_RG = '" + pParametros.IE_RG + "', \n";
                s = s + "    Site = '" + pParametros.Site + "', \n";
                s = s + "    Email = '" + pParametros.Email + "', \n";
                s = s + "    Telefones = '" + pParametros.Telefones + "', \n";
                s = s + "    CodigoMunicipio = '" + pParametros.CodigoMunicipio + "', \n";
                if (pParametros.FormularioContinuo > 0)
                    s = s + " FormularioContinuo = " + pParametros.FormularioContinuo + ", \n";
                else
                    s = s + " FormularioContinuo = 0, ";
                if (pParametros.AtualizaRoteiroSemanal > 0)
                    s = s + " AtualizaRoteiroSemanal = " + pParametros.AtualizaRoteiroSemanal + ", \n";
                else
                    s = s + " AtualizaRoteiroSemanal = 0, ";
                if (pParametros.AtualizaRoteiroMensal > 0)
                    s = s + " AtualizaRoteiroMensal = " + pParametros.AtualizaRoteiroMensal + ", \n";
                else
                    s = s + " AtualizaRoteiroMensal = 0, ";
                if (pParametros.PercentualISS > 0)
                    s = s + " PercentualISS = " + pParametros.PercentualISS.ToString().Replace(",", ".") + " \n";
                else
                    s = s + " PercentualISS = 0 \n";

                s = s + "where Numero = " + pNumero.ToString();
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
        public DataTable PreencheDataTableParametros(string pOrdem, int pCodigoEmpresa)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Numero, Nome, Endereco, CEP, Cidade, UF, CNPJ_CPF, IE_RG, Site, Email, Telefones, \n";
                s = s + "       CodigoMunicipio, FormularioContinuo, AtualizaRoteiroSemanal, AtualizaRoteiroMensal,  \n";
                s = s + "       PercentualISS  \n";
                s = s + "from   Parametros \n";
                s = s + "where  Numero = " + pCodigoEmpresa + " \n";
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

        public DataTable PreencheDataTableParametros(string pOrdem, string pFiltro, string pCampo, int pCodigoEmpresa)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Numero, Nome, Endereco, CEP, Cidade, UF, CNPJ_CPF, IE_RG, Site, Email, Telefones, \n";
                s = s + "       CodigoMunicipio, FormularioContinuo, AtualizaRoteiroSemanal, AtualizaRoteiroMensal,  \n";
                s = s + "       PercentualISS  \n"; 
                s = s + "from   Parametros \n";
                s = s + "where " + pCampo + " like '%" + pFiltro + "%' \n";
                s = s + "and    Numero = " + pCodigoEmpresa + " \n";
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

        public string Excluir(int pNumero, string pNome)
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
                s = s + "delete from Parametros ";
                if (pNumero > 0)
                {
                    s = s + "where  Numero = " + pNumero.ToString();
                }
                else
                {
                    s = s + "where  Nome = '" + pNome + "'";
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