using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using MySql.Data.MySqlClient;
using MySql.Data;
using SILCNegocios;

namespace LibSILC
{
	public class clsDestinoFinalDados
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
        public clsDestinoFinal PegaDados(clsDestinoFinal pDestinoFinal, int pCodigo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, DataCadastro, Nome, Ativo, Bairro, Celular, CEP, Cidade, CNPJ, eMail, EmiteCDF, Endereco, Enviar_emailCDF, Enviar_emailMovResiduos, Fone, ";
                s = s + "       LocalAterro, NomeArqAss, NomeArqLogo, NomeFantasia, UF, CodigoUnidadeDoIMA ";
                s = s + "from   Aterro ";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " ";
                }
                else if (pCodigo == 0)
                {
                    s = s + " order by Nome limit 1";
                }
                FillDataSet();
                if (l_ds.Tables[0].Rows.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    pDestinoFinal.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                    if (l_dt.Rows[0]["DataCadastro"].ToString() != "")
                        pDestinoFinal.DataCadastro = Convert.ToDateTime(l_dt.Rows[0]["DataCadastro"].ToString()).Date.ToShortDateString();
                    pDestinoFinal.Nome = l_dt.Rows[0]["Nome"].ToString();
                    pDestinoFinal.Ativo = Convert.ToInt32(l_dt.Rows[0]["Ativo"]);
                    pDestinoFinal.Bairro = l_dt.Rows[0]["Bairro"].ToString();
                    pDestinoFinal.Cidade = l_dt.Rows[0]["Cidade"].ToString();
                    pDestinoFinal.Celular = l_dt.Rows[0]["Celular"].ToString();
                    pDestinoFinal.CEP = l_dt.Rows[0]["CEP"].ToString();
                    pDestinoFinal.CNPJ = l_dt.Rows[0]["CNPJ"].ToString();
                    pDestinoFinal.eMail = l_dt.Rows[0]["eMail"].ToString();
                    if (l_dt.Rows[0]["EmiteCDF"].ToString() != "")
                        pDestinoFinal.EmiteCDF = Convert.ToInt32(l_dt.Rows[0]["EmiteCDF"]);
                    pDestinoFinal.Endereco = l_dt.Rows[0]["Endereco"].ToString();
                    if (l_dt.Rows[0]["Enviar_emailCDF"].ToString() != "")
                        pDestinoFinal.Enviar_emailCDF = Convert.ToInt16(l_dt.Rows[0]["Enviar_emailCDF"]);
                    if (l_dt.Rows[0]["Enviar_emailMovResiduos"].ToString() != "")
                        pDestinoFinal.Enviar_emailMovResiduos = Convert.ToInt16(l_dt.Rows[0]["Enviar_emailMovResiduos"]);
                    pDestinoFinal.Fone = l_dt.Rows[0]["Fone"].ToString();
                    pDestinoFinal.LocalAterro = l_dt.Rows[0]["LocalAterro"].ToString();
                    pDestinoFinal.NomeArqAss = l_dt.Rows[0]["NomeArqAss"].ToString();
                    pDestinoFinal.NomeArqLogo = l_dt.Rows[0]["NomeArqLogo"].ToString();
                    pDestinoFinal.NomeFantasia = l_dt.Rows[0]["NomeFantasia"].ToString();
                    pDestinoFinal.UF = l_dt.Rows[0]["UF"].ToString();
                    if (l_dt.Rows[0]["CodigoUnidadeDoIMA"].ToString() != "")
                        pDestinoFinal.CodigoUnidadeDoIMA = Convert.ToInt32(l_dt.Rows[0]["CodigoUnidadeDoIMA"].ToString());
                }                
            }
            catch (Exception ex)
            {
                pDestinoFinal = new clsDestinoFinal();
            }
            finally
            {
                DesconectaBanco();
            }
            return pDestinoFinal;
        }
        
        public DataTable PegaDados(clsDestinoFinal pDestinoFinal, int pCodigo, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, DataCadastro, Nome, Ativo, Bairro, Celular, CEP, Cidade, CNPJ, eMail, EmiteCDF, Endereco, Enviar_emailCDF, Enviar_emailMovResiduos, Fone, ";
                s = s + "       LocalAterro, NomeArqAss, NomeArqLogo, NomeFantasia,  UF, CodigoUnidadeDoIMA ";
                s = s + "from   Aterro ";
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
                s = s + "select Codigo, Nome, NomeFantasia, CodigoUnidadeDoIMA \n";
                s = s + "from   Aterro ";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " ";
                }
                else if (pCodigo == 0)
                {
                    s = s + "where    Nome like '%" + pNome + "%' \n";
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

        public DataTable PegaQQDados(string pWhereOrder)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, DataCadastro, Nome, Ativo, Bairro, Celular, CEP, Cidade, CNPJ, eMail, EmiteCDF, Endereco, Enviar_emailCDF, Enviar_emailMovResiduos, Fone, \n";
                s = s + "       LocalAterro, NomeArqAss, NomeArqLogo, NomeFantasia, UF, CodigoUnidadeDoIMA, Codigo as CodigoDestinoFinal \n";
                s = s + "from   Aterro \n";
                s = s + pWhereOrder;
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
                s = s + "from Aterro ";
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
                s = s + "from   Aterro ";
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
        public string PegaRazaoSocial(string pLocalEntrega)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Nome \n";
                s = s + "from   Aterro \n";
                s = s + "where  NomeFantasia = '" + pLocalEntrega + "' \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    sRet = l_dt.Rows[0][0].ToString();
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
        public string[] PegaNomeCNPJPeloCNPJ(string pCNPJ_CPF)
        {
            string[] sRetNomeCNPJ = new string[4];
            sRetNomeCNPJ[0] = "";
            sRetNomeCNPJ[1] = "";
            sRetNomeCNPJ[2] = "";
            sRetNomeCNPJ[3] = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Nome, CNPJ, Codigo, CodigoUnidadeDoIMA \n";
                s = s + "from   Aterro \n";
                s = s + "where  CNPJ like '" + pCNPJ_CPF + "%' \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                {
                    sRetNomeCNPJ[0] = l_dt.Rows[0][0].ToString();
                    sRetNomeCNPJ[1] = l_dt.Rows[0][1].ToString();
                    sRetNomeCNPJ[2] = l_dt.Rows[0][2].ToString();
                    sRetNomeCNPJ[3] = l_dt.Rows[0][3].ToString();
                }
            }
            catch (Exception ex)
            {
                sRetNomeCNPJ[0] = "Erro: " + ex.Message;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRetNomeCNPJ;
        }
        public string PegaNomeFantasia(string pCodigo)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Concat(Codigo, '-', NomeFantasia) as NomeFantasia \n";
                s = s + "from   Aterro \n";

                if (geral.RetiraCharsCNPJCPF(geral.RetiraLetras(pCodigo)) != "")
                    s = s + "where  Codigo = " + geral.RetiraCharsCNPJCPF(geral.RetiraLetras(pCodigo)) + " \n";
                else
                    s = s + "where  Codigo = -1 \n";

                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    sRet = l_dt.Rows[0][0].ToString();
            }
            catch
            {
                sRet = "";
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }
        public string PegaCodigo(string pLocalEntrega)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Codigo \n";
                s = s + "from   Aterro \n";
                s = s + "where  NomeFantasia = '" + pLocalEntrega + "' \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    sRet = l_dt.Rows[0][0].ToString();
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
        public string PegaCNPJ(string pNomeFantasia)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select CNPJ \n";
                s = s + "from   Aterro \n";
                s = s + "where  NomeFantasia like '" + pNomeFantasia + "%' \n";
                s = s + "and    Ativo = 1 \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    sRet = l_dt.Rows[0][0].ToString();
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
        public int PegaCodigo(string pNomeFantasia, string pLocalAterro = "")
        {
            int sRet = 0;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Codigo \n";
                s = s + "from   Aterro \n";
                if (pNomeFantasia != "")
                    s = s + "where  NomeFantasia = '" + pNomeFantasia + "' \n";
                else
                    s = s + "where  LocalAterro = '" + pLocalAterro + "' \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    sRet = Convert.ToInt32(l_dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                sRet = 0;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }
        public void Inserir(clsDestinoFinal pDestinoFinal, int pCodigo = 0)
        {
            try
            {
                s = "";
                s = s + "insert into Aterro ";
                s = s + "(";
                if (pCodigo > 0)
                    s = s + "Codigo, \n";
                s = s + " Nome, DataCadastro, Ativo, Bairro, Celular, CEP, Cidade, CNPJ, eMail, EmiteCDF, Endereco, Enviar_emailCDF, Enviar_emailMovResiduos, Fone, ";
                s = s + " LocalAterro, NomeArqAss, NomeArqLogo, NomeFantasia, UF, CodigoUnidadeDoIMA ";
                s = s + ") ";
                s = s + "values ";
                s = s + "(";
                if (pCodigo > 0)
                    s = s + pCodigo + ", \n";
                s = s + "'" + pDestinoFinal.Nome + "', \n";
                if (pDestinoFinal.DataCadastro != "")
                    s = s + "'" + Convert.ToDateTime(pDestinoFinal.DataCadastro).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";

                if (pDestinoFinal.Ativo.ToString() != "")
                    s = s + " " + pDestinoFinal.Ativo.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                s = s + "'" + pDestinoFinal.Bairro + "', \n";
                s = s + "'" + pDestinoFinal.Celular + "', \n";
                s = s + "'" + pDestinoFinal.CEP + "', \n";
                s = s + "'" + pDestinoFinal.Cidade + "', \n";
                s = s + "'" + pDestinoFinal.CNPJ + "', \n";
                s = s + "'" + pDestinoFinal.eMail + "', \n";
                if (pDestinoFinal.EmiteCDF.ToString() != "")
                    s = s + " " + pDestinoFinal.EmiteCDF.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                s = s + "'" + pDestinoFinal.Endereco + "', \n";
                if (pDestinoFinal.Enviar_emailCDF.ToString() != "")
                    s = s + " " + pDestinoFinal.Enviar_emailCDF.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pDestinoFinal.Enviar_emailMovResiduos.ToString() != "")
                    s = s + " " + pDestinoFinal.Enviar_emailMovResiduos.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                s = s + "'" + pDestinoFinal.Fone + "', \n";
                s = s + "'" + pDestinoFinal.LocalAterro + "', \n";
                s = s + "'" + pDestinoFinal.NomeArqAss + "', \n";
                s = s + "'" + pDestinoFinal.NomeArqLogo + "', \n";
                s = s + "'" + pDestinoFinal.NomeFantasia + "', \n";
                s = s + "'" + pDestinoFinal.UF + "', \n";
                if (pDestinoFinal.CodigoUnidadeDoIMA.ToString() != "")
                    s = s + " " + pDestinoFinal.CodigoUnidadeDoIMA.ToString().Replace(",", ".") + " \n";
                else
                    s = s + "0 \n";

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
        public string Alterar(clsDestinoFinal pDestinoFinal, int pCodigo)
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
                s = s + "update Aterro \n";
                s = s + "set   Nome          = '" + pDestinoFinal.Nome + "', \n";
                s = s + "      DataCadastro  = '" + Convert.ToDateTime(pDestinoFinal.DataCadastro).ToString("yyyy-MM-dd") + "', \n";
                s = s + "      Ativo         = " + pDestinoFinal.Ativo.ToString() + ", \n";
                s = s + "      Bairro        = '" + pDestinoFinal.Bairro + "', \n";
                s = s + "      Celular       = '" + pDestinoFinal.Celular + "', \n";
                s = s + "      CEP           = '" + pDestinoFinal.CEP + "', \n";
                s = s + "      Cidade        = '" + pDestinoFinal.Cidade + "', \n";
                s = s + "      CNPJ          = '" + pDestinoFinal.CNPJ + "', \n";
                s = s + "      eMail         = '" + pDestinoFinal.eMail + "', \n";
                s = s + "      EmiteCDF      = " + pDestinoFinal.EmiteCDF.ToString() + ", \n";
                s = s + "      Endereco      = '" + pDestinoFinal.Endereco + "', \n";
                s = s + "      Enviar_emailCDF = " + pDestinoFinal.Enviar_emailCDF.ToString() + ", \n";
                s = s + "      Enviar_emailMovResiduos = " + pDestinoFinal.Enviar_emailMovResiduos.ToString() + ", \n";
                s = s + "      Fone          = '" + pDestinoFinal.Fone + "', \n";
                s = s + "      LocalAterro   = '" + pDestinoFinal.LocalAterro + "', \n";
                s = s + "      NomeArqAss    = '" + pDestinoFinal.NomeArqAss + "', \n";
                s = s + "      NomeArqLogo   = '" + pDestinoFinal.NomeArqLogo + "', \n";
                s = s + "      NomeFantasia  = '" + pDestinoFinal.NomeFantasia + "', \n";
                s = s + "      UF  = '" + pDestinoFinal.UF + "', \n";
                s = s + "      CodigoUnidadeDoIMA  = " + pDestinoFinal.CodigoUnidadeDoIMA + " \n";
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
        public DataTable PreencheDataTableAterro(string pOrdem, bool pAtivos = false)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, DataCadastro, Nome, Ativo, Bairro, Celular, CEP, Cidade, CNPJ, eMail, EmiteCDF, Endereco, Enviar_emailCDF, Enviar_emailMovResiduos, Fone, ";
                s = s + "       LocalAterro, NomeArqAss, NomeArqLogo, NomeFantasia,  UF, CodigoUnidadeDoIMA ";
                s = s + "from   Aterro \n ";
                if (pAtivos)
                    s = s + "where Ativo = 1 \n";
                s = s + "order by " + pOrdem + "\n ";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                DesconectaBanco();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                DesconectaBanco();
                return l_dt;
            }
        }
        public DataTable PreencheDTDestinoSoCodigoNome(string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Nome, NomeFantasia \n";
                s = s + "from   Aterro order by " + pOrdem + " \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                DesconectaBanco();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                DesconectaBanco();
                return l_dt;
            }
        }
        public string Excluir(int pCodigo, string pModelo)
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
                s = s + "delete from Aterro ";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo.ToString();
                }
                else
                {
                    s = s + "where  Nome = '" + pModelo + "'";
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