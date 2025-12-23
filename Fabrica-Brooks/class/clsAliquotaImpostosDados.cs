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
    public class clsAliquotaImpostosDados
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
        public string PegaDescricao(string pCodigoBrooks)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Descricao \n";
                s = s + "from   AliquotaImpostos \n";
                if (pCodigoBrooks != "")
                {
                    s = s + "where  CodigoBrooks = '" + pCodigoBrooks + "' \n";
                    s = s + "limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    return l_dt.Rows[0]["Descricao"].ToString();
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
            finally
            {
                DesconectaBanco();
            }
        }
        
        public clsAliquotaImpostos PegaDados(clsAliquotaImpostos pAliquotaImpostos, int pSequencial)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial, CodigoSituacaoTributaria, CodigoBROOKS, Descricao, ValorLimiteIR, AliquotaIR_Retido, \n";
                s = s + "       AliquotaPIS_Retido, AliquotaCOFINS_Retido, AliquotaContribSocial, ValorLimiteCRF, \n";
                s = s + "       (AliquotaPIS_Retido + AliquotaCOFINS_Retido + AliquotaContribSocial) as TotalCRF \n";
                s = s + "from   AliquotaImpostos \n";
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
                    if (l_dt.Rows[0]["Sequencial"].ToString() != "")
                        pAliquotaImpostos.Sequencial = Convert.ToInt32(l_dt.Rows[0]["Sequencial"]);
                    if (l_dt.Rows[0]["CodigoSituacaoTributaria"].ToString() != "")
                        pAliquotaImpostos.CodigoSituacaoTributaria = Convert.ToInt32(l_dt.Rows[0]["CodigoSituacaoTributaria"]);
                    pAliquotaImpostos.CodigoBROOKS = l_dt.Rows[0]["CodigoBROOKS"].ToString();
                    pAliquotaImpostos.Descricao = l_dt.Rows[0]["Descricao"].ToString();
                    if (l_dt.Rows[0]["ValorLimiteIR"].ToString() != "")
                        pAliquotaImpostos.ValorLimiteIR = Convert.ToDecimal(l_dt.Rows[0]["ValorLimiteIR"].ToString());
                    if (l_dt.Rows[0]["AliquotaIR_Retido"].ToString() != "")
                        pAliquotaImpostos.AliquotaIR_Retido = Convert.ToDecimal(l_dt.Rows[0]["AliquotaIR_Retido"].ToString());
                    if (l_dt.Rows[0]["AliquotaPIS_Retido"].ToString() != "")
                        pAliquotaImpostos.AliquotaPIS_Retido = Convert.ToDecimal(l_dt.Rows[0]["AliquotaPIS_Retido"].ToString());
                    if (l_dt.Rows[0]["AliquotaCOFINS_Retido"].ToString() != "")
                        pAliquotaImpostos.AliquotaCOFINS_Retido = Convert.ToDecimal(l_dt.Rows[0]["AliquotaCOFINS_Retido"].ToString());
                    if (l_dt.Rows[0]["AliquotaContribSocial"].ToString() != "")
                        pAliquotaImpostos.AliquotaContribSocial = Convert.ToDecimal(l_dt.Rows[0]["AliquotaContribSocial"].ToString());
                    if (l_dt.Rows[0]["ValorLimiteCRF"].ToString() != "")
                        pAliquotaImpostos.ValorLimiteCRF = Convert.ToDecimal(l_dt.Rows[0]["ValorLimiteCRF"].ToString());
                }
                return pAliquotaImpostos;
            }
            catch (Exception ex)
            {
                return new clsAliquotaImpostos();
            }
            finally
            {
                DesconectaBanco();
            }
        }

        public clsAliquotaImpostos PegaAliquotas(clsAliquotaImpostos pAliquotaImpostos, string pCodigoBROOKS)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial, CodigoSituacaoTributaria, CodigoBROOKS, Descricao, ValorLimiteIR, AliquotaIR_Retido, \n";
                s = s + "       AliquotaPIS_Retido, AliquotaCOFINS_Retido, AliquotaContribSocial, ValorLimiteCRF, \n";
                s = s + "       (AliquotaPIS_Retido + AliquotaCOFINS_Retido + AliquotaContribSocial) as TotalCRF \n";
                s = s + "from   AliquotaImpostos \n";
                if (pCodigoBROOKS != "")
                {
                    s = s + "where  CodigoBROOKS = '" + pCodigoBROOKS + "' \n";
                    s = s + "limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows[0]["Sequencial"].ToString() != "")
                        pAliquotaImpostos.Sequencial = Convert.ToInt32(l_dt.Rows[0]["Sequencial"]);
                    if (l_dt.Rows[0]["CodigoSituacaoTributaria"].ToString() != "")
                        pAliquotaImpostos.CodigoSituacaoTributaria = Convert.ToInt32(l_dt.Rows[0]["CodigoSituacaoTributaria"]);
                    pAliquotaImpostos.CodigoBROOKS = l_dt.Rows[0]["CodigoBROOKS"].ToString();
                    pAliquotaImpostos.Descricao = l_dt.Rows[0]["Descricao"].ToString();
                    if (l_dt.Rows[0]["ValorLimiteIR"].ToString() != "")
                        pAliquotaImpostos.ValorLimiteIR = Convert.ToDecimal(l_dt.Rows[0]["ValorLimiteIR"].ToString());
                    if (l_dt.Rows[0]["AliquotaIR_Retido"].ToString() != "")
                        pAliquotaImpostos.AliquotaIR_Retido = Convert.ToDecimal(l_dt.Rows[0]["AliquotaIR_Retido"].ToString());
                    if (l_dt.Rows[0]["AliquotaPIS_Retido"].ToString() != "")
                        pAliquotaImpostos.AliquotaPIS_Retido = Convert.ToDecimal(l_dt.Rows[0]["AliquotaPIS_Retido"].ToString());
                    if (l_dt.Rows[0]["AliquotaCOFINS_Retido"].ToString() != "")
                        pAliquotaImpostos.AliquotaCOFINS_Retido = Convert.ToDecimal(l_dt.Rows[0]["AliquotaCOFINS_Retido"].ToString());
                    if (l_dt.Rows[0]["AliquotaContribSocial"].ToString() != "")
                        pAliquotaImpostos.AliquotaContribSocial = Convert.ToDecimal(l_dt.Rows[0]["AliquotaContribSocial"].ToString());
                    if (l_dt.Rows[0]["ValorLimiteCRF"].ToString() != "")
                        pAliquotaImpostos.ValorLimiteCRF = Convert.ToDecimal(l_dt.Rows[0]["ValorLimiteCRF"].ToString());
                }
                return pAliquotaImpostos;
            }
            catch (Exception ex)
            {
                return new clsAliquotaImpostos();
            }
            finally
            {
                DesconectaBanco();
            }
        }

        public DataTable PegaDados(clsAliquotaImpostos pAliquotaImpostos, int pSequencial, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial, CodigoSituacaoTributaria, CodigoBROOKS, Descricao, ValorLimiteIR, AliquotaIR_Retido, \n";
                s = s + "       AliquotaPIS_Retido, AliquotaCOFINS_Retido, AliquotaContribSocial, ValorLimiteCRF, \n";
                s = s + "       (AliquotaPIS_Retido + AliquotaCOFINS_Retido + AliquotaContribSocial) as TotalCRF \n";
                s = s + "from   AliquotaImpostos \n";
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
                    s = s + " order by CodigoBROOKS ";
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
                s = s + "from AliquotaImpostos ";
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
                s = s + "from   AliquotaImpostos  ";
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
        public void Inserir(clsAliquotaImpostos pAliquotaImpostos, int pSequencial = 0)
        {
            try
            {
                s = "";
                s = s + "insert into AliquotaImpostos \n";
                s = s + "( \n";
                if (pSequencial > 0)
                    s = s + "Sequencial, \n ";
                s = s + "  CodigoSituacaoTributaria, CodigoBROOKS, Descricao, ValorLimiteIR, AliquotaIR_Retido, \n";
                s = s + "  AliquotaPIS_Retido, AliquotaCOFINS_Retido, AliquotaContribSocial, ValorLimiteCRF \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";

                if (pSequencial > 0)
                    s = s + pSequencial.ToString() + ", \n ";

                if (pAliquotaImpostos.CodigoSituacaoTributaria > 0)
                    s = s + " " + pAliquotaImpostos.CodigoSituacaoTributaria.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                s = s + " '" + pAliquotaImpostos.CodigoBROOKS + "', \n";
                s = s + " '" + pAliquotaImpostos.Descricao + "', \n";
                if (pAliquotaImpostos.ValorLimiteIR > 0)
                    s = s + " " + pAliquotaImpostos.ValorLimiteIR.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pAliquotaImpostos.AliquotaIR_Retido > 0)
                    s = s + " " + pAliquotaImpostos.AliquotaIR_Retido.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pAliquotaImpostos.AliquotaPIS_Retido > 0)
                    s = s + " " + pAliquotaImpostos.AliquotaPIS_Retido.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pAliquotaImpostos.AliquotaCOFINS_Retido > 0)
                    s = s + " " + pAliquotaImpostos.AliquotaCOFINS_Retido.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pAliquotaImpostos.AliquotaContribSocial > 0)
                    s = s + " " + pAliquotaImpostos.AliquotaContribSocial.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pAliquotaImpostos.ValorLimiteCRF > 0)
                    s = s + " " + pAliquotaImpostos.ValorLimiteCRF.ToString().Replace(",", ".") + " \n";
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
        public string Alterar(clsAliquotaImpostos pAliquotaImpostos, int pSequencial)
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
                s = s + "update AliquotaImpostos \n";
                if (pAliquotaImpostos.CodigoSituacaoTributaria > 0)
                    s = s + " set CodigoSituacaoTributaria = " + pAliquotaImpostos.CodigoSituacaoTributaria.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " set CodigoSituacaoTributaria = 0, \n";
                s = s + " CodigoBROOKS = '" + pAliquotaImpostos.CodigoBROOKS + "', \n";
                s = s + " Descricao = '" + pAliquotaImpostos.Descricao + "', \n";
                if (pAliquotaImpostos.ValorLimiteIR > 0)
                    s = s + " ValorLimiteIR = " + pAliquotaImpostos.ValorLimiteIR.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " ValorLimiteIR = 0, \n";
                if (pAliquotaImpostos.AliquotaIR_Retido > 0)
                    s = s + " AliquotaIR_Retido = " + pAliquotaImpostos.AliquotaIR_Retido.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " AliquotaIR_Retido = 0, \n";
                if (pAliquotaImpostos.AliquotaPIS_Retido > 0)
                    s = s + " AliquotaPIS_Retido = " + pAliquotaImpostos.AliquotaPIS_Retido.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " AliquotaPIS_Retido = 0, \n";
                if (pAliquotaImpostos.AliquotaCOFINS_Retido > 0)
                    s = s + " AliquotaCOFINS_Retido = " + pAliquotaImpostos.AliquotaCOFINS_Retido.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " AliquotaCOFINS_Retido = 0, \n";
                if (pAliquotaImpostos.AliquotaContribSocial > 0)
                    s = s + " AliquotaContribSocial = " + pAliquotaImpostos.AliquotaContribSocial.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " AliquotaContribSocial = 0, \n";
                if (pAliquotaImpostos.ValorLimiteCRF > 0)
                    s = s + " ValorLimiteCRF = " + pAliquotaImpostos.ValorLimiteCRF.ToString().Replace(",", ".") + " \n";
                else
                    s = s + " ValorLimiteCRF = 0 \n"; s = s + "where Sequencial = " + pSequencial.ToString();
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
                s = s + "select Sequencial, CodigoSituacaoTributaria, CodigoBROOKS, Descricao, ValorLimiteIR, AliquotaIR_Retido, \n";
                s = s + "       AliquotaPIS_Retido, AliquotaCOFINS_Retido, AliquotaContribSocial, ValorLimiteCRF, \n";
                s = s + "       (AliquotaPIS_Retido + AliquotaCOFINS_Retido + AliquotaContribSocial) as TotalCRF \n";
                s = s + "from   AliquotaImpostos \n";
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

        public DataTable PreencheDataTableRetencoes(string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Concat(CodigoBROOKS, '-', Descricao) as CdDesc \n";
                s = s + "from   AliquotaImpostos \n";
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
        public DataTable PreencheDataTable(string pOrdem, string pFiltro, string pCampo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial, CodigoSituacaoTributaria, CodigoBROOKS, Descricao, ValorLimiteIR, AliquotaIR_Retido, \n";
                s = s + "       AliquotaPIS_Retido, AliquotaCOFINS_Retido, AliquotaContribSocial, ValorLimiteCRF, \n";
                s = s + "       (AliquotaPIS_Retido + AliquotaCOFINS_Retido + AliquotaContribSocial) as TotalCRF \n";
                s = s + "from   AliquotaImpostos \n";
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
                if (pSequencial > 0)
                {
                    s = "";
                    s = s + "delete from AliquotaImpostos ";
                    s = s + "where  Sequencial = " + pSequencial.ToString();
                }
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