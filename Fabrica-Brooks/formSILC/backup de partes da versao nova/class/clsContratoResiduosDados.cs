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
    public partial class clsContratoResiduosDados
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
        public clsContratoResiduos PegaDados(clsContratoResiduos pContratoResiduos, int pCodigoContrato, int pCodigoResiduo, string pDataReajuste)
        {
            try
            {
                ConectaBanco();
                s = "select \n";
                s = s + "  cr.CodigoContrato, cr.CodigoResiduo, r.DescricaoReduzida as DescricaoReduzidaResiduo, cr.CodigoCliente, cr.CaixaDisponivel, cr.TipoCaixa, \n";
                s = s + "  cr.FrequenciaColeta, cr.Franquia, cr.QuantidadeFranquia, cr.ValorUnitario, cr.OBS, cr.DataReajuste, cr.Unidade, \n";
                s = s + "  cr.Roteiro, cr.CodigoCaminhao, cr.DiasColeta, cr.MesAnoBase, cr.Particularidade,  \n";
                s = s + "  cr.expressao1CobrancaMensal, cr.Franquia1CobrancaMensal, cr.expressao2CobrancaMensal, cr.PeriodicidadeCobrancaMensal, \n";
                s = s + "  cr.ValorUnitarioCobrancaMensal, cr.ValorExcedenteCobrancaMensal, cr.expressao1CobrancaPeso, cr.expressao2CobrancaPeso,  \n";
                s = s + "  cr.expressao3CobrancaPeso, cr.condicaoCobrancaPeso, expressao4CobrancaPeso, FranquiaCobrancaPeso, UnidadeCobrancaPeso, \n";
                s = s + "  cr.ValorUnitarioCobrancaMensal \n";
                s = s + "  from ContratoResiduos cr \n";
                s = s + "  inner join Residuos r on r.Codigo = cr.CodigoResiduo \n";
                if (pCodigoContrato > 0)
                {
                    s = s + "where  cr.CodigoContrato = " + pCodigoContrato + " \n";
                    s = s + "and    cr.CodigoResiduo  = " + pCodigoResiduo + " \n";
                    s = s + "and    cr.DataReajuste = '" + Convert.ToDateTime(pDataReajuste).ToString("yyyy-MM-dd") + "' \n";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows[0]["CodigoContrato"].ToString() != "")
                        pContratoResiduos.CodigoContrato = Convert.ToInt16(l_dt.Rows[0]["CodigoContrato"]);
                    if (l_dt.Rows[0]["CodigoResiduo"].ToString() != "")
                        pContratoResiduos.CodigoResiduo = Convert.ToInt16(l_dt.Rows[0]["CodigoResiduo"]);
                    pContratoResiduos.DescricaoReduzidaResiduo = l_dt.Rows[0]["DescricaoReduzidaResiduo"].ToString();
                    if (l_dt.Rows[0]["CodigoCliente"].ToString() != "")
                        pContratoResiduos.CodigoCliente = Convert.ToInt16(l_dt.Rows[0]["CodigoCliente"]);
                    if (l_dt.Rows[0]["CaixaDisponivel"].ToString() != "")
                        pContratoResiduos.CaixaDisponivel = Convert.ToInt16(l_dt.Rows[0]["CaixaDisponivel"]);
                    pContratoResiduos.TipoCaixa = l_dt.Rows[0]["TipoCaixa"].ToString();
                    pContratoResiduos.FrequenciaColeta = l_dt.Rows[0]["FrequenciaColeta"].ToString();
                    pContratoResiduos.Franquia = l_dt.Rows[0]["QuantidadeFranquia"].ToString();
                    if (l_dt.Rows[0]["QuantidadeFranquia"].ToString() != "")
                        pContratoResiduos.QuantidadeFranquia = Convert.ToDecimal(l_dt.Rows[0]["QuantidadeFranquia"].ToString());
                    if (l_dt.Rows[0]["ValorUnitario"].ToString() != "")
                        pContratoResiduos.ValorUnitario = Convert.ToDecimal(l_dt.Rows[0]["ValorUnitario"].ToString());
                    pContratoResiduos.OBS = l_dt.Rows[0]["OBS"].ToString();
                    pContratoResiduos.Particularidade = l_dt.Rows[0]["Particularidade"].ToString();
                    if (l_dt.Rows[0]["DataReajuste"].ToString() != "")
                        pContratoResiduos.DataReajuste = Convert.ToDateTime(l_dt.Rows[0]["DataReajuste"].ToString()).Date.ToShortDateString();
                    pContratoResiduos.Unidade = l_dt.Rows[0]["Unidade"].ToString();
                    pContratoResiduos.Roteiro = l_dt.Rows[0]["Roteiro"].ToString();
                    if (l_dt.Rows[0]["CodigoCaminhao"].ToString() != "")
                        pContratoResiduos.CodigoCaminhao = Convert.ToInt32(l_dt.Rows[0]["CodigoCaminhao"].ToString());
                    pContratoResiduos.MesAnoBase = l_dt.Rows[0]["MesAnoBase"].ToString();

                    pContratoResiduos.expressao1CobrancaMensal = l_dt.Rows[0]["expressao1CobrancaMensal"].ToString();
                    pContratoResiduos.Franquia1CobrancaMensal = l_dt.Rows[0]["Franquia1CobrancaMensal"].ToString();
                    pContratoResiduos.expressao2CobrancaMensal = l_dt.Rows[0]["expressao2CobrancaMensal"].ToString();
                    pContratoResiduos.PeriodicidadeCobrancaMensal = l_dt.Rows[0]["PeriodicidadeCobrancaMensal"].ToString();
                    if (l_dt.Rows[0]["ValorUnitarioCobrancaMensal"].ToString() != "")
                        pContratoResiduos.ValorUnitarioCobrancaMensal = Convert.ToDecimal(l_dt.Rows[0]["ValorUnitarioCobrancaMensal"].ToString());
                    if (l_dt.Rows[0]["ValorContratoCobrancaMensal"].ToString() != "")
                        pContratoResiduos.ValorContratoCobrancaMensal = Convert.ToDecimal(l_dt.Rows[0]["ValorContratoCobrancaMensal"].ToString());
                    if (l_dt.Rows[0]["ValorExcedenteCobrancaMensal"].ToString() != "")
                        pContratoResiduos.ValorExcedenteCobrancaMensal = Convert.ToDecimal(l_dt.Rows[0]["ValorExcedenteCobrancaMensal"].ToString());
                    pContratoResiduos.expressao1CobrancaPeso = l_dt.Rows[0]["expressao1CobrancaPeso"].ToString();
                    pContratoResiduos.expressao2CobrancaPeso = l_dt.Rows[0]["expressao2CobrancaPeso"].ToString();
                    pContratoResiduos.expressao3CobrancaPeso = l_dt.Rows[0]["expressao3CobrancaPeso"].ToString();
                    pContratoResiduos.expressao4CobrancaPeso = l_dt.Rows[0]["expressao4CobrancaPeso"].ToString();
                    pContratoResiduos.condicaoCobrancaPeso = l_dt.Rows[0]["condicaoCobrancaPeso"].ToString();
                    if (l_dt.Rows[0]["FranquiaCobrancaPeso"].ToString() != "")
                        pContratoResiduos.FranquiaCobrancaPeso = Convert.ToDecimal(l_dt.Rows[0]["FranquiaCobrancaPeso"].ToString());
                    pContratoResiduos.UnidadeCobrancaPeso = l_dt.Rows[0]["UnidadeCobrancaPeso"].ToString();
                }
            }
            catch (Exception ex)
            {
                pContratoResiduos = new clsContratoResiduos();
            }
            finally
            {
                DesconectaBanco();
            }
            return pContratoResiduos;
        }

        public DataTable PegaDados(clsContratoResiduos pContratoResiduos, int pCodigoContrato, string pDataReajuste)
        {
            try
            {
                ConectaBanco();
                if (pCodigoContrato > 0 && pDataReajuste != "")
                {
                    s = "select \n";
                    s = s + "  cr.CodigoContrato, cr.CodigoResiduo, r.DescricaoReduzida as DescricaoReduzidaResiduo, cr.CodigoCliente, cr.CaixaDisponivel, cr.TipoCaixa, \n";
                    s = s + "  cr.FrequenciaColeta, cr.Franquia, cr.QuantidadeFranquia, cr.ValorUnitario, cr.OBS, cr.DataReajuste, cr.Unidade, \n";
                    s = s + "  cr.Roteiro, cr.CodigoCaminhao, cr.DiasColeta, cr.MesAnoBase, cr.Particularidade, \n";
                    s = s + "  cr.expressao1CobrancaMensal, cr.Franquia1CobrancaMensal, cr.expressao2CobrancaMensal, cr.PeriodicidadeCobrancaMensal, \n";
                    s = s + "  cr.ValorUnitarioCobrancaMensal, cr.ValorExcedenteCobrancaMensal, cr.expressao1CobrancaPeso, cr.expressao2CobrancaPeso,  \n";
                    s = s + "  cr.expressao3CobrancaPeso, cr.condicaoCobrancaPeso, expressao4CobrancaPeso, FranquiaCobrancaPeso, UnidadeCobrancaPeso, \n";
                    s = s + "  cr.ValorContratoCobrancaMensal \n";
                    s = s + "  from ContratoResiduos cr \n";
                    s = s + "  inner join Residuos r on r.Codigo = cr.CodigoResiduo \n";
                    s = s + "where  cr.CodigoContrato = " + pCodigoContrato + " \n";
                    s = s + "and    cr.DataReajuste = '" + Convert.ToDateTime(pDataReajuste).ToString("yyyy-MM-dd") + "' \n";
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
        public DataTable RetornaParticularidade()
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select cr.Particularidade, cr.CodigoCliente, cr.CodigoResiduo \n";
                s = s + "from   ContratoResiduos cr \n";
                s = s + "inner  join Contratos c on c.Codigo = cr.CodigoContrato \n";
                s = s + "where  cr.Particularidade <> '' \n";
                s = s + "and    cr.Particularidade <> '-' \n";
                s = s + "and    (c.DataRecisao is null or c.DataRecisao = '1900-01-01' or c.DataRecisao = '0100-01-01'  or c.DataRecisao = '0001-01-01') \n";
                s = s + "and    (select r.Data from Reajustes r where r.CodigoContrato = c.Codigo order by r.Data desc limit 1) = cr.DataReajuste \n";
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

        public DataTable PegaDadosUltimoReajuste(string pCodigoCliente)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select cr.CodigoContrato, cr.DataReajuste \n";
                s = s + "from   ContratoResiduos cr \n";
                s = s + "inner  join Contratos c on c.Codigo = cr.CodigoContrato \n";
                s = s + "where  (c.DataRecisao is null or c.DataRecisao = '1900-01-01' or c.DataRecisao = '0100-01-01'  or c.DataRecisao = '0001-01-01') \n";
                s = s + "and    (select r.Data from Reajustes r where r.CodigoContrato = c.Codigo order by r.Data desc limit 1) = cr.DataReajuste \n";
                s = s + "and    cr.CodigoCliente = " + pCodigoCliente + " \n";
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
                s = s + "from ContratoResiduos ";
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
        public string DadoExiste(int pCodigoContrato, int pCodigoResiduo, string pDataReajuste, int pCodigoCliente)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select * from   ContratoResiduos  ";
                if (pCodigoContrato > 0 && pCodigoResiduo > 0 && pDataReajuste != "")
                {
                    s = s + "where CodigoContrato = " + pCodigoContrato + " \n";
                    s = s + "and   CodigoResiduo  = " + pCodigoResiduo + " \n";
                    s = s + "and   CodigoCliente  = " + pCodigoCliente + " \n";
                    s = s + "and   DataReajuste = '" + Convert.ToDateTime(pDataReajuste).ToString("yyyy-MM-dd") + "' \n";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pCodigoContrato > 0 && pCodigoResiduo > 0 && pDataReajuste != "")
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
        public void Inserir(clsContratoResiduos pContratoResiduos)
        {
            try
            {
                s = "";
                s = s + "insert into ContratoResiduos \n";
                s = s + "( \n";
                s = s + "   CodigoContrato, CodigoCliente, CodigoResiduo, DataReajuste, DiasColeta, \n";
                s = s + "   CaixaDisponivel, TipoCaixa, FrequenciaColeta, Franquia, QuantidadeFranquia, \n";
                s = s + "   ValorUnitario, OBS, Unidade, Roteiro, CodigoCaminhao, MesAnoBase, Particularidade, \n";
                s = s + "   expressao1CobrancaMensal, Franquia1CobrancaMensal, expressao2CobrancaMensal, PeriodicidadeCobrancaMensal, \n";
                s = s + "   ValorUnitarioCobrancaMensal, ValorExcedenteCobrancaMensal, expressao1CobrancaPeso, expressao2CobrancaPeso,  \n";
                s = s + "   expressao3CobrancaPeso, condicaoCobrancaPeso, expressao4CobrancaPeso, FranquiaCobrancaPeso, UnidadeCobrancaPeso, \n";
                s = s + "   ValorContratoCobrancaMensal \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                s = s + pContratoResiduos.CodigoContrato.ToString() + ", \n";
                s = s + pContratoResiduos.CodigoCliente.ToString() + ", \n";
                s = s + pContratoResiduos.CodigoResiduo.ToString() + ", \n";
                if (pContratoResiduos.DataReajuste == "" || pContratoResiduos.DataReajuste == "0001-01-01" || pContratoResiduos.DataReajuste == "0100-01-01")
                    s = s + "'0001-01-01', \n";
                else
                    s = s + "'" + Convert.ToDateTime(pContratoResiduos.DataReajuste).ToString("yyyy-MM-dd") + "', \n";
                s = s + "'" + pContratoResiduos.DiasColeta + "', \n";
                s = s + " " + pContratoResiduos.CaixaDisponivel.ToString() + ", \n";
                s = s + "'" + pContratoResiduos.TipoCaixa + "', \n";
                s = s + "'" + pContratoResiduos.FrequenciaColeta.ToString() + "', \n";
                s = s + "'" + pContratoResiduos.Franquia + "', \n";
                s = s + " " + pContratoResiduos.QuantidadeFranquia.ToString().Replace(",", ".") + ", \n";
                s = s + " " + pContratoResiduos.ValorUnitario.ToString().Replace(",", ".") + ", \n";
                s = s + "'" + pContratoResiduos.OBS + "', \n";
                s = s + "'" + pContratoResiduos.Unidade + "', \n";
                s = s + "'" + pContratoResiduos.Roteiro + "', \n";
                s = s + " " + pContratoResiduos.CodigoCaminhao.ToString() + ", \n";
                s = s + "'" + pContratoResiduos.MesAnoBase + "', \n";
                s = s + "'" + pContratoResiduos.Particularidade + "', \n";
                s = s + "'" + pContratoResiduos.expressao1CobrancaMensal + "', \n";
                s = s + "'" + pContratoResiduos.Franquia1CobrancaMensal + "', \n";
                s = s + "'" + pContratoResiduos.expressao2CobrancaMensal + "', \n";
                s = s + "'" + pContratoResiduos.PeriodicidadeCobrancaMensal + "', \n";
                s = s + " " + pContratoResiduos.ValorUnitarioCobrancaMensal.ToString().Replace(",", ".") + ", \n";
                s = s + " " + pContratoResiduos.ValorExcedenteCobrancaMensal.ToString().Replace(",", ".") + ", \n";
                s = s + "'" + pContratoResiduos.expressao1CobrancaPeso + "', \n";
                s = s + "'" + pContratoResiduos.expressao2CobrancaPeso + "', \n";
                s = s + "'" + pContratoResiduos.expressao3CobrancaPeso + "', \n";
                s = s + "'" + pContratoResiduos.condicaoCobrancaPeso + "', \n";
                s = s + "'" + pContratoResiduos.expressao4CobrancaPeso + "', \n";
                s = s + " " + pContratoResiduos.FranquiaCobrancaPeso.ToString().Replace(",", ".") + ", \n";
                s = s + "'" + pContratoResiduos.UnidadeCobrancaPeso + "', \n";
                s = s + " " + pContratoResiduos.ValorContratoCobrancaMensal.ToString().Replace(",", ".") + " \n";
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
            catch (Exception ex)
            {
                s = ex.Message;
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public string Alterar(clsContratoResiduos pContratoResiduos, int pCodigo, string pDataReajuste)
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
                s = s + "update ContratoResiduos \n";
                s = s + "set CodigoContrato = " + pContratoResiduos.CodigoContrato.ToString() + ", \n";
                s = s + "    CodigoCliente  = " + pContratoResiduos.CodigoCliente.ToString() + ", \n";
                s = s + "    CodigoResiduo  = " + pContratoResiduos.CodigoResiduo.ToString() + ", \n";
                if (pContratoResiduos.DataReajuste == "" || pContratoResiduos.DataReajuste == "0001-01-01" || pContratoResiduos.DataReajuste == "0100-01-01")
                    s = s + " DataReajuste = '0001-01-01', \n";
                else
                    s = s + " DataReajuste = '" + Convert.ToDateTime(pContratoResiduos.DataReajuste).ToString("yyyy-MM-dd") + "', \n";
                s = s + "    DiasColeta = '" + pContratoResiduos.DiasColeta + "', \n";
                s = s + "    CaixaDisponivel = " + pContratoResiduos.CaixaDisponivel.ToString() + ", \n";
                s = s + "    TipoCaixa = '" + pContratoResiduos.TipoCaixa + "', \n";
                s = s + "    FrequenciaColeta = '" + pContratoResiduos.FrequenciaColeta.ToString() + "', \n";
                s = s + "    Franquia = '" + pContratoResiduos.Franquia + "', \n";
                s = s + "    QuantidadeFranquia = " + pContratoResiduos.QuantidadeFranquia.ToString().Replace(",", ".") + ", \n";
                s = s + "    ValorUnitario = " + pContratoResiduos.ValorUnitario.ToString().Replace(",", ".") + ", \n";
                s = s + "    Particularidade = '" + pContratoResiduos.Particularidade + "', \n";
                s = s + "    OBS = '" + pContratoResiduos.OBS + "', \n";
                s = s + "    Unidade = '" + pContratoResiduos.Unidade + "', \n";
                s = s + "    Roteiro = '" + pContratoResiduos.Roteiro + "', \n";
                s = s + "    CodigoCaminhao =  " + pContratoResiduos.CodigoCaminhao.ToString() + ", \n";
                s = s + "    MesAnoBase = '" + pContratoResiduos.MesAnoBase + "', \n";
                s = s + "    expressao1CobrancaMensal     = '" + pContratoResiduos.expressao1CobrancaMensal + "', \n";
                s = s + "    Franquia1CobrancaMensal      = '" + pContratoResiduos.Franquia1CobrancaMensal + "', \n";
                s = s + "    expressao2CobrancaMensal     = '" + pContratoResiduos.expressao2CobrancaMensal + "', \n";
                s = s + "    PeriodicidadeCobrancaMensal  = '" + pContratoResiduos.PeriodicidadeCobrancaMensal + "', \n";
                s = s + "    ValorUnitarioCobrancaMensal  = " + pContratoResiduos.ValorUnitarioCobrancaMensal.ToString().Replace(",", ".") + ", \n";
                s = s + "    ValorExcedenteCobrancaMensal = " + pContratoResiduos.ValorExcedenteCobrancaMensal.ToString().Replace(",", ".") + ", \n";
                s = s + "    expressao1CobrancaPeso       = '" + pContratoResiduos.expressao1CobrancaPeso + "', \n";
                s = s + "    expressao2CobrancaPeso       = '" + pContratoResiduos.expressao2CobrancaPeso + "', \n";
                s = s + "    expressao3CobrancaPeso       = '" + pContratoResiduos.expressao3CobrancaPeso + "', \n";
                s = s + "    condicaoCobrancaPeso         = '" + pContratoResiduos.condicaoCobrancaPeso + "', \n";
                s = s + "    expressao4CobrancaPeso       = '" + pContratoResiduos.expressao4CobrancaPeso + "', \n";
                s = s + "    FranquiaCobrancaPeso         =  " + pContratoResiduos.FranquiaCobrancaPeso.ToString().Replace(",", ".") + ", \n";
                s = s + "    UnidadeCobrancaPeso          = '" + pContratoResiduos.UnidadeCobrancaPeso + "', \n";
                s = s + "    ValorContratoCobrancaMensal  = " + pContratoResiduos.ValorContratoCobrancaMensal.ToString().Replace(",", ".") + " \n";
                s = s + "where CodigoContrato = " + pContratoResiduos.CodigoContrato.ToString() + " \n";
                s = s + "and   CodigoCliente  = " + pContratoResiduos.CodigoCliente.ToString() + " \n";
                s = s + "and   CodigoResiduo  = " + pContratoResiduos.CodigoResiduo.ToString() + " \n";
                s = s + "and   DataReajuste   = '" + Convert.ToDateTime(pContratoResiduos.DataReajuste).ToString("yyyy-MM-dd") + "' \n";

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
        public string AlterarParticularidade(int pCodigoContrato, int pCodigoCliente, int pCodigoResiduo, string pDataReajuste, string pParticularidade)
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
                s = s + "update ContratoResiduos \n";
                s = s + "set   Particularidade = '" + pParticularidade + "' \n";
                s = s + "where CodigoContrato = " + pCodigoContrato.ToString() + " \n";
                s = s + "and   CodigoCliente  = " + pCodigoCliente.ToString() + " \n";
                s = s + "and   CodigoResiduo  = " + pCodigoResiduo.ToString() + " \n";
                s = s + "and   DataReajuste   = '" + Convert.ToDateTime(pDataReajuste).ToString("yyyy-MM-dd") + "' \n";
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
        public DataTable PreencheDataTableContratoResiduos(string pOrdem, int pCodigoContrato, string pDataReajuste, int pCodigoCliente)
        {
            try
            {
                ConectaBanco();
                s = "select \n";
                s = s + "  cr.CodigoContrato, cr.CodigoResiduo, r.DescricaoReduzida as DescricaoReduzidaResiduo, cr.CodigoCliente, cr.CaixaDisponivel, cr.TipoCaixa, \n";
                s = s + "  cr.FrequenciaColeta, cr.Franquia, cr.QuantidadeFranquia, cr.ValorUnitario, cr.OBS, cr.DataReajuste, cr.Unidade, \n";
                s = s + "  cr.Roteiro, cr.CodigoCaminhao, cr.DiasColeta, cr.MesAnoBase, cr.Particularidade, \n";
                s = s + "  cr.expressao1CobrancaMensal, cr.Franquia1CobrancaMensal, cr.expressao2CobrancaMensal, cr.PeriodicidadeCobrancaMensal, \n";
                s = s + "  cr.ValorUnitarioCobrancaMensal, cr.ValorExcedenteCobrancaMensal, cr.expressao1CobrancaPeso, cr.expressao2CobrancaPeso,  \n";
                s = s + "  cr.expressao3CobrancaPeso, cr.condicaoCobrancaPeso, cr.expressao4CobrancaPeso, cr.FranquiaCobrancaPeso, UnidadeCobrancaPeso, \n";
                s = s + "  cr.ValorContratoCobrancaMensal \n";
                s = s + "  from ContratoResiduos cr \n";
                s = s + "  inner join Residuos r on r.Codigo = cr.CodigoResiduo \n";
                if (pCodigoContrato > 0)
                {
                    s = s + "where  cr.CodigoContrato = " + pCodigoContrato + " \n";
                    s = s + "and    cr.CodigoCliente = " + pCodigoCliente + " \n";
                    s = s + "and    cr.DataReajuste = '" + Convert.ToDateTime(pDataReajuste).ToString("yyyy-MM-dd") + "' \n";
                }
                if (pOrdem.ToUpper() == "DescricaoReduzidaResiduo")
                    s = s + "order by r.DescricaoReduzida " + pOrdem.PadRight(3);
                else
                    s = s + "order by cr." + pOrdem;
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

        public DataTable PreencheDTContratoResiduosExcel(string pOrdem, int pCodigoContrato, string pDataReajuste)
        {
            try
            {
                ConectaBanco();
                s = "select \n";
                s = s + "  cr.CodigoResiduo, r.DescricaoReduzida as DescricaoResiduo, cr.CaixaDisponivel, cr.TipoCaixa, \n";
                s = s + "  cr.FrequenciaColeta, cr.Franquia, cr.QuantidadeFranquia, cr.ValorUnitario, cr.OBS \n";
                s = s + "  from ContratoResiduos cr \n";
                s = s + "  inner join Residuos r on r.Codigo = cr.CodigoResiduo \n";
                if (pCodigoContrato > 0)
                {
                    s = s + "where  cr.CodigoContrato = " + pCodigoContrato + " \n";
                    s = s + "and    cr.DataReajuste = '" + Convert.ToDateTime(pDataReajuste).ToString("yyyy-MM-dd") + "' \n";
                }
                if (pOrdem.ToUpper() == "DescricaoReduzidaResiduo")
                    s = s + "order by r.DescricaoReduzida " + pOrdem.PadRight(3);
                else
                    s = s + "order by cr." + pOrdem;
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

        public string PegaDataProxColeta(int pCodigoCliente, int pCodigoResiduo, string pFrequenciaColeta, string pData1)
        {
            string sRet = "";
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select distinct MesAnoBase, DiasColeta, FrequenciaColeta \n ";
                s = s + "from   ContratoResiduos \n ";
                s = s + "where  CodigoCliente = " + pCodigoCliente + " \n ";
                s = s + "and    CodigoResiduo = " + pCodigoResiduo + " \n ";
                s = s + "and    FrequenciaColeta = '" + pFrequenciaColeta + "' \n";
                s = s + "and    DiasColeta <> '' \n";
                s = s + "order by DataReajuste desc \n";
                s = s + "limit 1 \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();

                DesconectaBanco();

                string DataProxColeta = "";
                if (l_dt.Rows.Count > 0)
                {
                    string PrimeiroDiaColeta = "";
                    string mesBase = "";

                    mesBase = geral.Left(l_dt.Rows[0]["MesAnoBase"].ToString(), 2);
                    PrimeiroDiaColeta = l_dt.Rows[0]["DiasColeta"].ToString();

                    string[] spDiasColeta = PrimeiroDiaColeta.Split(","[0]);

                    if (spDiasColeta.Length == 0)
                        PrimeiroDiaColeta = "01";
                    else if (spDiasColeta.Length > 0)
                        PrimeiroDiaColeta = Convert.ToInt32(spDiasColeta[0]).ToString("00");

                    string DATA2 = "";
                    DATA2 = Convert.ToDateTime("01/" + Convert.ToDateTime(pData1).ToString("MM/yyyy")).AddDays(-1).ToString("dd/MM/yyyy");

                    int iPC = 0;
                    if (l_dt.Rows[0]["MesAnoBase"].ToString() != "")
                    {
                        if (l_dt.Rows[0]["FrequenciaColeta"].ToString().IndexOf("BIMESTR") > 0)
                        {
                            DataProxColeta = Convert.ToDateTime(PrimeiroDiaColeta + "/" + mesBase + "/" + Convert.ToDateTime(DATA2).Year).ToString("dd/MM/yyyy");
                            for (iPC = 2; iPC <= 24; iPC = iPC + 2)
                            {
                                if (Convert.ToDateTime("01/" + Convert.ToDateTime(DataProxColeta).ToString("MM/yyyy")) >= Convert.ToDateTime("01/" + Convert.ToDateTime(pData1).ToString("MM/yyyy")))
                                    break;
                                DataProxColeta = Convert.ToDateTime(DataProxColeta).AddMonths(2).ToString("dd/MM/yyyy");
                            }
                        }
                        else if (l_dt.Rows[0]["FrequenciaColeta"].ToString().IndexOf("MENSAL") > 0)
                        {
                            int _proximoMes = Convert.ToDateTime(pData1).Month;
                            if (Convert.ToDateTime(pData1).Month < 12)
                                _proximoMes++;
                            else if (Convert.ToDateTime(pData1).Month == 12)
                                _proximoMes = 1;
                            DataProxColeta = Convert.ToDateTime(PrimeiroDiaColeta + "/" + _proximoMes.ToString("00") + "/" + Convert.ToDateTime(pData1).Year).ToString("dd/MM/yyyy");
                            for (iPC = 1; iPC <= 12; iPC++)
                            {
                                if (Convert.ToDateTime(DataProxColeta) >=  Convert.ToDateTime("01" + "/" + _proximoMes.ToString("00") + "/" + DateTime.Now.Year.ToString()))
                                    break;
                                DataProxColeta = Convert.ToDateTime(DataProxColeta).AddMonths(1).ToString("dd/MM/yyyy");
                            }
                        }
                        else if (l_dt.Rows[0]["FrequenciaColeta"].ToString().IndexOf("TRIMESTR") > 0)
                        {
                            DataProxColeta = Convert.ToDateTime(PrimeiroDiaColeta + "/" + mesBase + "/" + Convert.ToDateTime(DATA2).Year).ToString("dd/MM/yyyy");
                            for (iPC = 1; iPC <= 12; iPC++)
                            {
                                if (Convert.ToDateTime("01/" + Convert.ToDateTime(DataProxColeta).ToString("MM/yyyy")) >= Convert.ToDateTime("01/" + Convert.ToDateTime(pData1).ToString("MM/yyyy")))
                                    break;
                                DataProxColeta = Convert.ToDateTime(DataProxColeta).AddMonths(3).ToString("dd/MM/yyyy");
                            }
                        }
                        else if (l_dt.Rows[0]["FrequenciaColeta"].ToString().IndexOf("QUADRIMESTR") > 0)
                        {
                            DataProxColeta = Convert.ToDateTime(PrimeiroDiaColeta + "/" + mesBase + "/" + Convert.ToDateTime(DATA2).Year).ToString("dd/MM/yyyy");
                            for (iPC = 1; iPC <= 11; iPC++)
                            {
                                if (Convert.ToDateTime("01/" + Convert.ToDateTime(DataProxColeta).ToString("MM/yyyy")) >= Convert.ToDateTime("01/" + Convert.ToDateTime(pData1).ToString("MM/yyyy")))
                                    break;
                                DataProxColeta = Convert.ToDateTime(DataProxColeta).AddMonths(4).ToString("dd/MM/yyyy");
                            }
                        }
                        else if (l_dt.Rows[0]["FrequenciaColeta"].ToString().IndexOf("SEMESTR") > 0)
                        {
                            DataProxColeta = Convert.ToDateTime(PrimeiroDiaColeta + "/" + mesBase + "/" + Convert.ToDateTime(DATA2).Year).ToString("dd/MM/yyyy");
                            for (iPC = 1; iPC <= 10; iPC++)
                            {
                                if (Convert.ToDateTime("01/" + Convert.ToDateTime(DataProxColeta).ToString("MM/yyyy")) >= Convert.ToDateTime("01/" + Convert.ToDateTime(pData1).ToString("MM/yyyy")))
                                    break;
                                DataProxColeta = Convert.ToDateTime(DataProxColeta).AddMonths(6).ToString("dd/MM/yyyy");
                            }
                        }
                        else if (l_dt.Rows[0]["FrequenciaColeta"].ToString().IndexOf("ANUAL") > 0)
                        {
                            DataProxColeta = Convert.ToDateTime(PrimeiroDiaColeta + "/" + mesBase + "/" + Convert.ToDateTime(DATA2).Year).ToString("dd/MM/yyyy");
                            if (Convert.ToDateTime("01/" + Convert.ToDateTime(DataProxColeta).ToString("MM/yyyy")) >= Convert.ToDateTime("01/" + Convert.ToDateTime(pData1).ToString("MM/yyyy")))
                                DataProxColeta = Convert.ToDateTime(DataProxColeta).AddYears(1).ToString("dd/MM/yyyy");
                        }
                        else if (l_dt.Rows[0]["FrequenciaColeta"].ToString().IndexOf("SEMANA") > 0)
                        {
                            DATA2 = Convert.ToDateTime(pData1).AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy");

                            int _diaDaSemana = (int)Convert.ToDateTime(DATA2).DayOfWeek + 1; 

                            string _diasSemanaColeta = l_dt.Rows[0]["DiasColeta"].ToString();
                            string[] spDiasSemanaColeta = _diasSemanaColeta.Split(","[0]);
                            int _diaSemanaProximaColeta = 0;
                            foreach (string _diaSemanaProximaColetaX in spDiasSemanaColeta)
                            {
                                if (spDiasSemanaColeta.Length == 1)
                                {
                                    _diaSemanaProximaColeta = Convert.ToInt32(_diaSemanaProximaColetaX);
                                    if (_diaDaSemana < _diaSemanaProximaColeta)
                                    {
                                        if (8 + _diaDaSemana - _diaSemanaProximaColeta > 7)
                                            DataProxColeta = Convert.ToDateTime(DATA2).AddDays(1 + _diaDaSemana - _diaSemanaProximaColeta).ToString("dd/MM/yyyy");
                                        else
                                            DataProxColeta = Convert.ToDateTime(DATA2).AddDays(0 + _diaDaSemana - _diaSemanaProximaColeta).ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        DataProxColeta = Convert.ToDateTime(DATA2).AddDays(0 + _diaSemanaProximaColeta - _diaDaSemana).ToString("dd/MM/yyyy");
                                    }
                                }
                                else
                                {
                                    for (int _i = 0; _i < spDiasSemanaColeta.Length;_i++)
                                    {
                                        if (spDiasSemanaColeta[_i] == _diaSemanaProximaColetaX)
                                        {
                                            if (_i < spDiasSemanaColeta.Length)
                                                _diaSemanaProximaColeta = Convert.ToInt32(spDiasSemanaColeta[_i]);
                                            else
                                                _diaSemanaProximaColeta = Convert.ToInt32(_diaSemanaProximaColetaX);
                                            DataProxColeta = Convert.ToDateTime(DATA2).AddDays(_diaSemanaProximaColeta - _diaDaSemana).ToString("dd/MM/yyyy");
                                            break;
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
                sRet = DataProxColeta;
            }
            catch (Exception ex)
            {
                DesconectaBanco();
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                sRet = "";
            }
            return sRet;
        }

        public DataTable PreencheDataTableContratoResiduos(string pOrdem, string pFiltro, string pCampo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select t.Codigo, t.CodigoCliente, DescricaoHistorico, DiaVencimento, NumeroContrato, ValorContrato, DataTermino, \n";
                s = s + "       NumeroCaixasLocadas, DataInicio, DataReajuste, IndiceReajuste, DataRecisao, SituacaoRecisao, \n";
                s = s + "       Observacao, c.Nome, c.NomeFantasia, t.Particularidade, \n";
                s = s + "       t.expressao1CobrancaMensal, t.Franquia1CobrancaMensal, t.expressao2CobrancaMensal, t.PeriodicidadeCobrancaMensal, \n";
                s = s + "       t.ValorUnitarioCobrancaMensal, t.ValorExcedenteCobrancaMensal, t.expressao1CobrancaPeso, t.expressao2CobrancaPeso,  \n";
                s = s + "       t.expressao3CobrancaPeso, t.condicaoCobrancaPeso, t.expressao4CobrancaPeso, t.FranquiaCobrancaPeso, UnidadeCobrancaPeso, \n";
                s = s + "       t.ValorContratoCobrancaMensal \n";
                s = s + "from   ContratoResiduos t \n";
                s = s + "inner  join Clientes c on c.Codigo = t.CodigoCliente \n ";
                if (pOrdem.ToUpper() == "Codigo asc".ToUpper() || pOrdem.ToUpper() == "Codigo desc".ToUpper() || pOrdem.ToUpper() == "Codigo".ToUpper())
                {
                    s = s + "order by t." + pOrdem + " \n";
                }
                else
                {
                    s = s + "where " + pCampo + " like '%" + pFiltro + "%' \n";
                    s = s + "order by " + pOrdem + " \n";
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
        public string Excluir(int pCodigo)
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
                if (pCodigo > 0)
                {
                    s = "";
                    s = s + "delete from ContratoResiduos ";
                    s = s + "where  CodigoContrato = " + pCodigo.ToString();
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
        public string Excluir(int pCodigoContrato, int pCodigoResiduo, int pCodigoCliente, string pDataReajuste)
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
                if (pCodigoContrato > 0 && pCodigoResiduo > 0 && pCodigoCliente > 0 && pDataReajuste != "")
                {
                    s = "";
                    s = s + "delete from ContratoResiduos ";
                    s = s + "where  CodigoContrato = " + pCodigoContrato.ToString() + " \n";
                    s = s + "and    CodigoResiduo  = " + pCodigoResiduo.ToString() + " \n";
                    s = s + "and    CodigoCliente  = " + pCodigoCliente.ToString() + " \n";
                    s = s + "and    DataReajuste   = '" + Convert.ToDateTime(pDataReajuste).ToString("yyyy-MM-dd") + "' \n";
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
        public string Excluir(int pCodigoContrato, int pCodigoCliente, string pDataReajuste)
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
                if (pCodigoContrato > 0 && pCodigoCliente > 0 && pDataReajuste != "")
                {
                    s = s + "delete from ContratoResiduos ";
                    s = s + "where  CodigoContrato = " + pCodigoContrato.ToString() + " \n";
                    s = s + "and    CodigoCliente  = " + pCodigoCliente.ToString() + " \n";
                    s = s + "and    DataReajuste   = '" + Convert.ToDateTime(pDataReajuste).ToString("yyyy-MM-dd") + "' \n";
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
        
        public DataTable PegaFrequenciaColetasDistintas()
        {
            try
            {
                ConectaBanco();
                DataTable _dtRet = new DataTable();
                _dtRet.Columns.Add("FrequenciaColeta");
                DataRow _dr = _dtRet.NewRow();
                _dr[0] = "";
                _dtRet.Rows.Add(_dr);
                s = "select distinct FrequenciaColeta from ContratoResiduos \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                _dtRet.Merge(l_ds.Tables[0]);
                DesconectaBanco();
                return _dtRet;
            }
            catch (Exception ex)
            {
                DesconectaBanco();
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                return l_dt;
            }
        }
        public DataTable PegaConteineresDistintos()
        {
            try
            {
                ConectaBanco();
                s = "SELECT distinct replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(numero, '0', ''), '1', ''), '2', ''), '3', ''), '4', ''), '5', ''), '6', ''), '7', ''), '8', ''), '9', '') as TipoCaixa FROM cacambas; \n";
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
        public string PegaDataUltimaColeta(int pCodigoCliente, int pCodigoResiduo, string pData1)
        {
            string sRet = "";
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Lancamentos.DataRetirada from Lancamentos \n";
                s = s + "inner  join LancamentoMTR on Lancamentos.NumeroLancamento = LancamentoMTR.NumeroLancamento \n";
                s = s + "where  Lancamentos.CodigoCliente   = " + pCodigoCliente + " \n";
                s = s + "and    LancamentoMTR.CodigoResiduo = " + pCodigoResiduo + " \n";
                s = s + "and    Lancamentos.DataRetirada  <= '" + Convert.ToDateTime(pData1).AddDays(-1).ToString("yyyy-MM-dd") + "' \n";
                s = s + "order  by Lancamentos.DataRetirada DESC \n";
                s = s + "limit  1 \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                DesconectaBanco();
                if (l_dt.Rows.Count > 0)
                {
                    if (l_dt.Rows[0][0].ToString() != "")
                        sRet = Convert.ToDateTime(l_dt.Rows[0][0]).ToString("dd/MM/yyyy");
                }
            }
            catch (Exception ex)
            {
                DesconectaBanco();
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                sRet = "";
            }
            return sRet;
        }
    }
}