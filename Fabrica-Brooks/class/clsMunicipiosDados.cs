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
    public class clsMunicipiosDados
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
        public clsMunicipios PegaDados(clsMunicipios pMunicipios, int pCodigo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Nome, AliquotaISS, CodigoIBGE, CodigoIPM, UF \n";
                s = s + "from   Municipios ";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " ";
                    s = s + "limit 1";
                }
                else if (pCodigo == 0)
                {
                    s = s + " order by Descricao limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    pMunicipios.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                    pMunicipios.Nome = l_dt.Rows[0]["Nome"].ToString();
                    pMunicipios.UF = l_dt.Rows[0]["UF"].ToString();
                    if (l_dt.Rows[0]["AliquotaISS"].ToString() != "")
                        pMunicipios.AliquotaISS = Convert.ToDecimal(l_dt.Rows[0]["AliquotaISS"].ToString());
                    pMunicipios.CodigoIBGE = l_dt.Rows[0]["CodigoIBGE"].ToString();
                    if (l_dt.Rows[0]["CodigoIPM"].ToString() != "")
                        pMunicipios.CodigoIPM = Convert.ToInt32(l_dt.Rows[0]["CodigoIPM"].ToString());
                }
            }
            catch (Exception ex)
            {
                return new clsMunicipios();
            }
            finally
            {
                DesconectaBanco();
            }
            return pMunicipios;
        }

        public DataTable PegaDados(clsMunicipios pMunicipios, int pCodigo, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Nome, AliquotaISS, CodigoIBGE, CodigoIPM, UF \n";
                s = s + "from   Municipios ";
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
                return new DataTable();
            }
            finally
            {
                DesconectaBanco();
            }
            return l_dt;
        }
        public string PegaCodigoFederal(string pNome)
        {
            string sRet = "";
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select CodigoIPM \n";
                s = s + "from   Municipios \n";
                s = s + "where  Nome = '" + pNome + "' \n";
                s = s + "and    UF   = 'SC' \n";
                s = s + "limit 1";
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows.Count > 0)
                    {                        
                        if (l_dt.Rows[0]["CodigoIPM"].ToString() != "")
                            sRet = l_dt.Rows[0]["CodigoIPM"].ToString();
                        else
                            sRet = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                sRet =  "0";
            }
            finally
            {
                DesconectaBanco();
            }
            return sRet;
        }

        public DataTable PreencheDataTableOrdem(string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Nome, AliquotaISS, CodigoIBGE, CodigoIPM, UF \n";
                s = s + "from   Municipios \n";
                if (pOrdem == "Descrição")
                    pOrdem = "Descricao";
                s = s + "order by " + pOrdem + " \n";
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
        public int PegaTamanhoCampoVarChar(string pCampo)
        {
            int iRet = 0; 
            try
            {
                ConectaBanco();
                s = "";
                s = s + "show columns ";
                s = s + "from Municipios ";
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
                s = s + "from   Municipios ";
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
        public void Inserir(clsMunicipios pMunicipios)
        {
            try
            {
                s = "";
                s = s + "insert into Municipios \n";
                s = s + "( \n";
                s = s + "  Nome, AliquotaISS, CodigoIBGE, CodigoIPM, UF \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                s = s + "'" + pMunicipios.Nome + "', \n";
                if (pMunicipios.AliquotaISS.ToString() != "")
                    s = s + " " + pMunicipios.AliquotaISS.ToString() + ", \n";
                else
                    s = s + "0, \n";
                if (pMunicipios.CodigoIPM == 0)
                    s = s + " 0, \n";
                else
                    s = s + " '" + pMunicipios.CodigoIPM.ToString() + "', \n";
                s = s + "'" + pMunicipios.CodigoIBGE + "', \n";
                s = s + "'" + pMunicipios.UF + "' \n";
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
        public string Alterar(clsMunicipios pMunicipios, int pCodigo)
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
                s = s + "update Municipios \n";
                s = s + "set   Nome        = '" + pMunicipios.Nome + "', \n";
                if (pMunicipios.AliquotaISS.ToString() == "")
                    s = s + "      AliquotaISS = 0, \n";
                else
                    s = s + "      AliquotaISS = " + pMunicipios.AliquotaISS.ToString().Replace(",", ".") + ", \n";
                if (pMunicipios.CodigoIPM.ToString() == "")
                    s = s + "  CodigoIPM   = 0, \n";
                else
                    s = s + "  CodigoIPM   = '" + pMunicipios.CodigoIPM.ToString() + "', \n";
                s = s + "      CodigoIBGE  = '" + pMunicipios.CodigoIBGE + "', \n";
                s = s + "      UF          = '" + pMunicipios.UF + "' \n";
                s = s + "where Codigo = " + pCodigo.ToString() ;
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
        public string Excluir(int pCodigo, string pDescricao)
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
                s = s + "delete from Municipios ";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo.ToString();
                }
                else
                {
                    s = s + "where  Descricao = '" + pDescricao + "'";
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
        public DataTable PreencheDataTableFiltro(string pOrdem, string pFiltro, string pCampo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Nome, AliquotaISS, CodigoIBGE, CodigoIPM, UF \n";
                s = s + "from   Municipios \n";
                if (pCampo == "Descrição") pCampo = "Descricao";
                if (pOrdem == "Descrição") pOrdem = "Descricao";
                s = s + "where " + pCampo + " like '%" + pFiltro + "%' \n";
                s = s + "order by " + pOrdem + " \n";
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
        public DataTable PreencheDtGrandeFpolis()
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Nome, AliquotaISS, CodigoIBGE, CodigoIPM, UF \n";
                s = s + "from   Municipios \n";
                s = s + "where  Nome = 'Biguaçu' \n";
                s = s + "or     Nome = 'São José' \n";
                s = s + "or     Nome = 'Florianópolis' \n";
                s = s + "or     Nome = 'Palhoça' \n";
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