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
    public class clsDocumentacaoAplicavelDados
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
        public clsDocumentacaoAplicavel PegaDados(int pMes, int pAno, int pCodigoCliente)
        {
            clsDocumentacaoAplicavel oDocumentacaoAplicavel = new clsDocumentacaoAplicavel();
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select \n";
                s = s + "       Sequencial, CodigoCliente, Mes, Ano, PeriodoApuracao, EnviarPlanFatAteDia, AguardarAprovacaoPlanFat, \n";
                s = s + "       AguardarOrdemCompra, EnviarCDFBrooks, ConferirDDRAteDia, ConferindoDDR, EnviarRGRAteDia, EnviarRelGer, \n";
                s = s + "       PlanFatEnviada, DDRConferida, RelGerEnviado \n";
                s = s + "from   DocumentacaoAplicavel \n";
                s = s + "where  CodigoCliente = " + pCodigoCliente + " \n";
                s = s + "and    Mes = " + pMes + " \n";
                s = s + "and    Ano = " + pAno + " \n";
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows.Count > 0)
                    {
                        if (l_dt.Rows[0]["Sequencial"].ToString() != "")
                            oDocumentacaoAplicavel.Sequencial = Convert.ToInt32(l_dt.Rows[0]["Sequencial"].ToString());
                        if (l_dt.Rows[0]["AguardarAprovacaoPlanFat"].ToString() != "")
                            oDocumentacaoAplicavel.AguardarAprovacaoPlanFat = Convert.ToInt32(l_dt.Rows[0]["AguardarAprovacaoPlanFat"].ToString());
                        if (l_dt.Rows[0]["AguardarOrdemCompra"].ToString() != "")
                            oDocumentacaoAplicavel.AguardarOrdemCompra = Convert.ToInt32(l_dt.Rows[0]["AguardarOrdemCompra"].ToString());
                        if (l_dt.Rows[0]["Ano"].ToString() != "")
                            oDocumentacaoAplicavel.Ano = Convert.ToInt32(l_dt.Rows[0]["Ano"].ToString());
                        if (l_dt.Rows[0]["CodigoCliente"].ToString() != "")
                            oDocumentacaoAplicavel.CodigoCliente = Convert.ToInt32(l_dt.Rows[0]["CodigoCliente"].ToString());
                        if (l_dt.Rows[0]["ConferindoDDR"].ToString() != "")
                            oDocumentacaoAplicavel.ConferindoDDR = Convert.ToInt32(l_dt.Rows[0]["ConferindoDDR"].ToString());
                        if (l_dt.Rows[0]["ConferirDDRAteDia"].ToString() != "")
                            oDocumentacaoAplicavel.ConferirDDRAteDia = Convert.ToInt32(l_dt.Rows[0]["ConferirDDRAteDia"].ToString());
                        if (l_dt.Rows[0]["EnviarCDFBrooks"].ToString() != "")
                            oDocumentacaoAplicavel.EnviarCDFBrooks = Convert.ToInt32(l_dt.Rows[0]["EnviarCDFBrooks"].ToString());
                        if (l_dt.Rows[0]["EnviarPlanFatAteDia"].ToString() != "")
                            oDocumentacaoAplicavel.EnviarPlanFatAteDia = Convert.ToInt32(l_dt.Rows[0]["EnviarPlanFatAteDia"].ToString());
                        if (l_dt.Rows[0]["EnviarRGRAteDia"].ToString() != "")
                            oDocumentacaoAplicavel.EnviarRGRAteDia = Convert.ToInt32(l_dt.Rows[0]["EnviarRGRAteDia"].ToString());
                        if (l_dt.Rows[0]["Mes"].ToString() != "")
                            oDocumentacaoAplicavel.Mes = Convert.ToInt32(l_dt.Rows[0]["Mes"].ToString());
                        if (l_dt.Rows[0]["EnviarRelGer"].ToString() != "")
                            oDocumentacaoAplicavel.EnviarRelGer = Convert.ToInt32(l_dt.Rows[0]["EnviarRelGer"].ToString());
                        oDocumentacaoAplicavel.PeriodoApuracao = l_dt.Rows[0]["PeriodoApuracao"].ToString();
                        if (l_dt.Rows[0]["PlanFatEnviada"].ToString() != "")
                            oDocumentacaoAplicavel.PlanFatEnviada = Convert.ToInt32(l_dt.Rows[0]["PlanFatEnviada"].ToString());
                        if (l_dt.Rows[0]["DDRConferida"].ToString() != "")
                            oDocumentacaoAplicavel.DDRConferida = Convert.ToInt32(l_dt.Rows[0]["DDRConferida"].ToString());
                        if (l_dt.Rows[0]["RelGerEnviado"].ToString() != "")
                            oDocumentacaoAplicavel.RelGerEnviado = Convert.ToInt32(l_dt.Rows[0]["RelGerEnviado"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                oDocumentacaoAplicavel = new clsDocumentacaoAplicavel();
            }
            finally
            {
                DesconectaBanco();
            }
            return oDocumentacaoAplicavel;
        }
        
        public DataTable PegaDados(clsDocumentacaoAplicavel pDocumentacaoAplicavel, int pSequencial, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select \n";
                s = s + "       CodigoCliente, Mes, Ano, PeriodoApuracao, EnviarPlanFatAteDia, AguardarAprovacaoPlanFat, \n";
                s = s + "       AguardarOrdemCompra, EnviarCDFBrooks, ConferirDDRAteDia, ConferindoDDR, EnviarRGRAteDia, EnviarRelGer, \n";
                s = s + "       PlanFatEnviada, DDRConferida, RelGerEnviado \n";
                s = s + "from   DocumentacaoAplicavel \n";
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
                    s = s + " order by Campo ";
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
                s = s + "from DocumentacaoAplicavel ";
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
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Sequencial ";
                s = s + "from   DocumentacaoAplicavel ";
                if (pSequencial != 0)
                {
                    s = s + "where  Sequencial = " + pSequencial + " ";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pSequencial != 0)
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
        public string DadoExiste(int pCodigoCliente, int pMes, int pAno)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Sequencial \n";
                s = s + "from   DocumentacaoAplicavel \n";
                s = s + "where  CodigoCliente = " + pCodigoCliente + " \n";
                s = s + "and    Mes = " + pMes + " \n";
                s = s + "and    Ano = " + pAno + " \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pCodigoCliente > 0)
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
        public int Registros(int pMes, int pAno, int pCodigoCliente)
        {
            int iRet = 0;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Count(Sequencial) as Registros \n";
                s = s + "from   DocumentacaoAplicavel \n";
                s = s + "where  Mes = " + pMes + " \n";
                s = s + "and    Ano = " + pAno + " \n";
                s = s + "and    CodigoCliente = " + pCodigoCliente + " \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    iRet = Convert.ToInt32(l_dt.Rows[0]["Registros"]);
            }
            catch (Exception ex)
            {
                iRet = 0;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return iRet;
        }
        public void Inserir(clsDocumentacaoAplicavel pDocumentacaoAplicavel, int pSeq = 0)
        {
            if (pDocumentacaoAplicavel.CodigoCliente > 0)
            {
                s = "";
                s = s + "insert into DocumentacaoAplicavel \n";
                s = s + "( \n";
                if (pSeq > 0)
                    s = s + "   Sequencial, \n";    
                s = s + "       CodigoCliente, Mes, Ano, PeriodoApuracao, EnviarPlanFatAteDia, AguardarAprovacaoPlanFat, \n";
                s = s + "       AguardarOrdemCompra, EnviarCDFBrooks, ConferirDDRAteDia, ConferindoDDR, EnviarRGRAteDia, EnviarRelGer, \n";
                s = s + "       PlanFatEnviada, DDRConferida, RelGerEnviado \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pSeq > 0)
                    s = s + pSeq + ", \n";
                s = s + " " + pDocumentacaoAplicavel.CodigoCliente.ToString() + ", \n";
                s = s + " " + pDocumentacaoAplicavel.Mes.ToString() + ", \n";
                s = s + " " + pDocumentacaoAplicavel.Ano.ToString() + ", \n";
                s = s + "'" + pDocumentacaoAplicavel.PeriodoApuracao + "', \n";
                s = s + " " + pDocumentacaoAplicavel.EnviarPlanFatAteDia + ", \n";
                s = s + " " + pDocumentacaoAplicavel.AguardarAprovacaoPlanFat + ", \n";
                s = s + " " + pDocumentacaoAplicavel.AguardarOrdemCompra + ", \n";
                s = s + " " + pDocumentacaoAplicavel.EnviarCDFBrooks + ", \n";
                s = s + " " + pDocumentacaoAplicavel.ConferirDDRAteDia + ", \n";
                s = s + " " + pDocumentacaoAplicavel.ConferindoDDR + ", \n";
                s = s + " " + pDocumentacaoAplicavel.EnviarRGRAteDia + ", \n";
                s = s + " " + pDocumentacaoAplicavel.EnviarRelGer + ", \n";
                s = s + " " + pDocumentacaoAplicavel.PlanFatEnviada + ", \n";
                s = s + " " + pDocumentacaoAplicavel.DDRConferida + ", \n";
                s = s + " " + pDocumentacaoAplicavel.RelGerEnviado + " \n";

                s = s + ")";
                try
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
                }
                finally
                {
                    DesconectaBanco();
                }                    
            }
        }
        public string Alterar(clsDocumentacaoAplicavel pDocumentacaoAplicavel)
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
                if (pDocumentacaoAplicavel.CodigoCliente > 0)
                {

                    s = "";
                    s = s + "update DocumentacaoAplicavel \n";
                    s = s + "set  AguardarAprovacaoPlanFat = " + pDocumentacaoAplicavel.AguardarAprovacaoPlanFat + ", \n";
                    s = s + "     AguardarOrdemCompra      = " + pDocumentacaoAplicavel.AguardarOrdemCompra + ", \n";
                    s = s + "     ConferindoDDR            = " + pDocumentacaoAplicavel.ConferindoDDR + ", \n";
                    s = s + "     ConferirDDRAteDia        = " + pDocumentacaoAplicavel.ConferirDDRAteDia + ", \n";
                    s = s + "     EnviarCDFBrooks          = " + pDocumentacaoAplicavel.EnviarCDFBrooks + ", \n";
                    s = s + "     EnviarPlanFatAteDia      = " + pDocumentacaoAplicavel.EnviarPlanFatAteDia + ", \n";
                    s = s + "     EnviarRGRAteDia          = " + pDocumentacaoAplicavel.EnviarRGRAteDia + ", \n";
                    s = s + "     EnviarRelGer             = " + pDocumentacaoAplicavel.EnviarRelGer + ", \n";
                    s = s + "     PeriodoApuracao          ='" + pDocumentacaoAplicavel.PeriodoApuracao + "', \n";
                    s = s + "     PlanFatEnviada           = " + pDocumentacaoAplicavel.PlanFatEnviada + ", \n";
                    s = s + "     DDRConferida             = " + pDocumentacaoAplicavel.DDRConferida + ", \n";
                    s = s + "     RelGerEnviado            = " + pDocumentacaoAplicavel.RelGerEnviado + " \n";
                    if (pDocumentacaoAplicavel.Sequencial > 0)
                    {
                        s = s + "where Sequencial = " + pDocumentacaoAplicavel.Sequencial + " \n";
                    }
                    else
                    {
                        s = s + "where CodigoCliente = " + pDocumentacaoAplicavel.CodigoCliente + " \n";
                        s = s + "and   Mes = " + pDocumentacaoAplicavel.Mes + " \n";
                        s = s + "and   Ano = " + pDocumentacaoAplicavel.Ano + " \n";
                    }
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
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
        public DataTable PreencheDataTable(string pOrdem, int pMes, int pAno, bool pDadosExistentes = false, int pCodigoCliente = 0, string pFiltroNome = "")
        {
            try
            {
                ConectaBanco();
                if (pAno == 0)
                    pAno = 2000;
                if (pDadosExistentes)
                {
                    s = "";
                    s = s + "select distinct d.Sequencial, d.CodigoCliente, c.NomeFantasia, d.Mes, d.Ano, \n";
                    s = s + "       d.PeriodoApuracao, d.EnviarPlanFatAteDia, d.AguardarAprovacaoPlanFat, \n";
                    s = s + "       d.AguardarOrdemCompra, d.EnviarCDFBrooks, d.ConferirDDRAteDia, \n";
                    if (pMes > 0 && pAno > 0)
                        s = s + "       dc.DDR_Conferindo, ";
                    else
                        s = s + "       0 as DDR_Conferindo, ";
                    s = s + "       d.EnviarRGRAteDia, 'Ok' as Ok, 'Reverte' as Reverte, d.EnviarRelGer, \n";
                    s = s + "       d.PlanFatEnviada, d.DDRConferida, d.RelGerEnviado \n";
                    s = s + "from   Clientes c \n";
                    s = s + "left   join DocumentacaoAplicavel d  on c.Codigo = d.CodigoCliente \n ";
                    s = s + "left   join DDR_Conferencia dc on dc.CodigoCliente = c.Codigo and dc.Mes = " + pMes + " and dc.Ano = " + pAno + " \n";
                    s = s + "left   join Contratos ct on ct.CodigoCliente = c.Codigo \n";
                    if (pMes > 0 && pAno > 0)
                        s = s + "and    (ct.DataRecisao = '0100-01-01' or ct.DataRecisao = '0001-01-01' or ct.DataRecisao is null) \n";
                    s = s + "where    c.Codigo > 0 \n";
                    if (pCodigoCliente > 0)
                        s = s + "and    c.Codigo = " + pCodigoCliente +  " \n";
                    if (pFiltroNome == "")
                    {
                        s = s + "and    (d.EnviarPlanFatAteDia > 0 or d.AguardarAprovacaoPlanFat > 0 or \n";
                        s = s + "        d.AguardarOrdemCompra > 0 or d.ConferirDDRAteDia > 0 or \n";
                        s = s + "        d.PlanFatEnviada > 0 or d.DDRConferida > 0 or d.RelGerEnviado > 0 or d.EnviarRGRAteDia > 0) \n";
                    }
                    else if (pFiltroNome != "")
                        s = s + "and    c.NomeFantasia like '%" + pFiltroNome + "%' \n";
                    if (pAno > 2000)
                    {
                        s = s + "and    (d.Mes = 0 and d.Ano = 2000 or d.Mes = " + pMes + " and d.Ano = " + pAno + ") \n";
                        s = s + "and    not exists(select * from DocumentacaoAplicavel  \n";
                        s = s + "                   where CodigoCliente = d.CodigoCliente  \n ";
                        s = s + "                   and   (d.Mes = " + pMes + " and d.Ano = " + pAno + ")) \n";
                    }
                    else
                        s = s + "and   d.Mes = 0 and d.Ano = 2000 \n";
                    
                    s = s + "order by " + pOrdem;
                }
                else if (!pDadosExistentes)
                {
                    s = "";
                    s = s + "select distinct d.Sequencial, \n";
                    s = s + "       max(c.Codigo) as CodigoCliente, c.NomeFantasia, " + pMes + " as Mes, " + pAno + " as Ano, \n";
                    s = s + "       max(Concat(ct.ApuracaoDe, '/', ct.ApuracaoA)) as PeriodoApuracao, ct.EnviarPLANFAT as EnviarPlanFatAteDia, \n";
                    s = s + "       ct.ClienteAprovaPLANFAT as AguardarAprovacaoPlanFat, \n";
                    s = s + "       ct.ClienteEnviaOC as AguardarOrdemCompra, ct.EnviarCDF as EnviarCDFBrooks, d.ConferirDDRAteDia, \n";
                    s = s + "       dc.DDR_Conferindo, ct.EnviarRELGER as EnviarRGRAteDia, 'Ok' as Ok, 'Reverte' as Reverte, d.EnviarRelGer, \n";
                    s = s + "       d.PlanFatEnviada, d.DDRConferida, d.RelGerEnviado \n";
                    s = s + "from   Clientes c \n";
                    s = s + "left   join DocumentacaoAplicavel d  on c.Codigo = d.CodigoCliente \n";
                    s = s + "left   join DDR_Conferencia dc on dc.CodigoCliente = c.Codigo and dc.Mes = " + pMes + " and dc.Ano = " + pAno + " \n";
                    s = s + "left   join Contratos ct on ct.CodigoCliente = c.Codigo \n";
                    s = s + "where  (c.Inativo = 0 or c.inativo is null) \n";
                    s = s + "and    (ct.DataRecisao = '0100-01-01' or ct.DataRecisao = '0001-01-01' or ct.DataRecisao is null) \n";
                    if (pCodigoCliente > 0)
                        s = s + "and    c.Codigo = " + pCodigoCliente + " \n";
                    s = s + "group  by c.NomeFantasia, Mes, Ano, \n";
                    s = s + "          ct.EnviarPLANFAT, ct.ClienteAprovaPLANFAT, \n";
                    s = s + "          ct.ClienteEnviaOC, d.ConferirDDRAteDia, dc.DDR_Conferindo, d.EnviarRELGER \n";
                    s = s + "order by " + pOrdem;
                }
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

        public DataTable PreencheDataTable(string pOrdem, string pFiltro, string pCampo, int pMes = 0, int pAno = 0, bool pMostrarTodosPeriodos = false)
        {
            try
            {
                ConectaBanco();
                s = "";                
                s = s + "select distinct d.Sequencial, \n";
                s = s + "       c.Codigo as CodigoCliente, c.NomeFantasia, " + pMes.ToString("00") + " as Mes, " + pAno.ToString("00") + " Ano, \n ";
                s = s + "       d.PeriodoApuracao, d.EnviarPlanFatAteDia, d.AguardarAprovacaoPlanFat, \n";
                s = s + "       d.AguardarOrdemCompra, d.EnviarCDFBrooks, d.ConferirDDRAteDia, d.ConferindoDDR, d.EnviarRGRAteDia, d.EnviarRelGer, \n";
                s = s + "       d.PlanFatEnviada, d.DDRConferida, d.RelGerEnviado, 'Ok' as Ok, d.EnviarRelGer, dc.DDR_Conferindo \n";
                s = s + "from   Clientes c \n";
                if (pMostrarTodosPeriodos)
                {
                    s = s + "right   join DocumentacaoAplicavel d on c.Codigo = d.CodigoCliente \n";
                    s = s + "left   join DDR_Conferencia dc on dc.CodigoCliente = c.Codigo \n";
                }
                else
                {
                    s = s + "left   join DocumentacaoAplicavel d on c.Codigo = d.CodigoCliente and d.Mes = " + pMes + " and d.Ano = " + pAno.ToString() + "\n";
                    s = s + "left   join DDR_Conferencia dc on dc.CodigoCliente = c.Codigo and dc.Mes = " + pMes + " and dc.Ano = " + pAno.ToString() + " \n";
                }
                if (pCampo == "Codigo" || pCampo == "Código")
                {
                    s = s + "where c." + geral.RemoverAcentos(pCampo) + " = " + pFiltro + " \n";
                    s = s + "order by c." + geral.RemoverAcentos(pOrdem) + " \n";
                }
                else
                {
                    s = s + "where c." + pCampo + " like '%" + pFiltro + "%' \n";
                    s = s + "order by c." + pOrdem + " \n";
                }
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

        public string Excluir(int pSequencial)
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
                if (pSequencial > 0)
                {
                    s = s + "delete from DocumentacaoAplicavel ";
                    s = s + "where  Sequencial = " + pSequencial.ToString();
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
        public Int16 ConferirDDRAteDia(int pCodigoCliente)
        {
            Int16 iRet = 0;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select ConferirDDRAteDia \n";
                s = s + "from   DocumentacaoAplicavel \n";
                s = s + "where  CodigoCliente = " + pCodigoCliente + " \n";
                s = s + "and    Mes = 0 \n";
                s = s + "and    Ano = 2000 \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pCodigoCliente > 0)
                    iRet = Convert.ToInt16(l_dt.Rows[0][0]);
                else
                    iRet = 0;
            }
            catch
            {
                iRet = 0;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return iRet;
        }
        public Int16 TemPLANFAT(int pCodigoCliente)
        {
            Int16 iRet = 0;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select EnviarPlanFatAteDia \n";
                s = s + "from   DocumentacaoAplicavel \n";
                s = s + "where  CodigoCliente = " + pCodigoCliente + " \n";
                s = s + "and    Mes = 0 \n";
                s = s + "and    Ano = 2000 \n";
                s = s + "limit  1 \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pCodigoCliente > 0)
                    iRet = Convert.ToInt16(l_dt.Rows[0][0]);
                else
                    iRet = 0;
            }
            catch
            {
                iRet = 0;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return iRet;
        }
    }
}