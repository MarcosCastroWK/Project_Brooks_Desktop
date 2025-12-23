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
    public class clsNotasFiscaisDados
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
        public clsNotasFiscais PegaDados(clsNotasFiscais pNotasFiscais, int pCodigoCliente, int pNumeroNF, bool pUltimaNotaCliente)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select n.NumeroNotaFiscal, n.DataEmissao, n.ValorTotal, n.ValorISS, c.Nome2 as NomeCliente, c.Codigo as CodigoCliente, \n";
                s = s + "       c.CNPJ_CPF, n.Vencimento, n.NumeroNF, n.PercentualISS, n.ValorPorExtenso, n.Cancelada, n.NFImpressa,  \n";
                s = s + "       n.PercentualIRRF, n.PercentualCRF, n.PercentualISS, n.DiasEntreVctos, n.NumeroParcelas, n.Classificacao,  \n";
                s = s + "       n.ContaGerencial, n.TipoDocumento, n.Historico, n.PercentualINSS, n.ValorBaseINSS,  \n";
                s = s + "       n.ArquivoParaImportacaoGerado, n.DataReferencia, n.ValorDescontoIncondicionado,  \n";
                s = s + "       n.CodigoBROOKS_Impostos, n.ValorPIS, n.ValorCOFINS, n.ValorContrSocial, n.NumeroRecibo \n";
                s = s + "from   NotasFiscais n  \n";
                s = s + "inner  join Clientes c on c.Codigo = n.CodigoCliente \n ";
                if (!pUltimaNotaCliente)
                {
                    if (pCodigoCliente > 0)
                    {
                        s = s + "where  n.CodigoCliente = " + pCodigoCliente + " \n ";
                        if (pNumeroNF > 0)
                            s = s + "and NumeroNF = " + pNumeroNF + " \n ";
                        s = s + "order by n.NumeroNotaFiscal desc \n ";
                    }
                    else if (pCodigoCliente == 0)
                    {
                        s = s + " order by c.Nome2 \n ";
                    }
                }
                else if (pUltimaNotaCliente)
                {
                    if (pCodigoCliente > 0)
                    {
                        s = s + "where n.CodigoCliente = " + pCodigoCliente + " \n ";
                        s = s + "order by n.NumeroNotaFiscal desc \n ";
                    }
                    else if (pNumeroNF > 0)
                    {
                        s = s + "where n.NumeroNF = " + pNumeroNF + " \n ";
                        s = s + "order by n.NumeroNotaFiscal desc \n ";
                    }
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];

                    if (l_dt.Rows[0]["NumeroNotaFiscal"].ToString() != "")
                        pNotasFiscais.NumeroNotaFiscal = Convert.ToInt32(l_dt.Rows[0]["NumeroNotaFiscal"]);

                    if (l_dt.Rows[0]["CodigoCliente"].ToString() != "")
                        pNotasFiscais.CodigoCliente = Convert.ToInt32(l_dt.Rows[0]["CodigoCliente"]);
                    pNotasFiscais.NomeCliente = l_dt.Rows[0]["NomeCliente"].ToString();
                    if (l_dt.Rows[0]["DataEmissao"].ToString() != "")
                        pNotasFiscais.DataEmissao = Convert.ToDateTime(l_dt.Rows[0]["DataEmissao"].ToString()).Date.ToShortDateString();
                    else
                        pNotasFiscais.DataEmissao = "";
                    if (l_dt.Rows[0]["ValorTotal"].ToString() != "")
                        pNotasFiscais.ValorTotal = Convert.ToDecimal(l_dt.Rows[0]["ValorTotal"].ToString());
                    if (l_dt.Rows[0]["ValorISS"].ToString() != "")
                        pNotasFiscais.ValorISS = Convert.ToDecimal(l_dt.Rows[0]["ValorISS"].ToString());
                    if (l_dt.Rows[0]["Vencimento"].ToString() != "")
                        pNotasFiscais.Vencimento = Convert.ToDateTime(l_dt.Rows[0]["Vencimento"].ToString()).Date.ToShortDateString();
                    else
                        pNotasFiscais.Vencimento = "";
                    if (l_dt.Rows[0]["NumeroNF"].ToString() != "")
                        pNotasFiscais.NumeroNF = Convert.ToInt32(l_dt.Rows[0]["NumeroNF"].ToString());
                    if (l_dt.Rows[0]["PercentualISS"].ToString() != "")
                        pNotasFiscais.PercentualISS = Convert.ToDecimal(l_dt.Rows[0]["PercentualISS"].ToString());
                    pNotasFiscais.ValorPorExtenso = l_dt.Rows[0]["ValorPorExtenso"].ToString();
                    pNotasFiscais.Cancelada = 0;
                    if (l_dt.Rows[0]["Cancelada"].ToString() != "")
                        pNotasFiscais.Cancelada = Convert.ToInt32(l_dt.Rows[0]["Cancelada"].ToString());
                    if (l_dt.Rows[0]["NFImpressa"].ToString() != "")
                        pNotasFiscais.NFImpressa = Convert.ToInt32(l_dt.Rows[0]["NFImpressa"].ToString());
                    if (l_dt.Rows[0]["PercentualIRRF"].ToString() != "")
                        pNotasFiscais.PercentualIRRF = Convert.ToDecimal(l_dt.Rows[0]["PercentualIRRF"].ToString());
                    if (l_dt.Rows[0]["PercentualCRF"].ToString() != "")
                        pNotasFiscais.PercentualCRF = Convert.ToDecimal(l_dt.Rows[0]["PercentualCRF"].ToString());
                    if (l_dt.Rows[0]["DiasEntreVctos"].ToString() != "")
                        pNotasFiscais.DiasEntreVctos = Convert.ToInt32(l_dt.Rows[0]["DiasEntreVctos"].ToString());
                    if (l_dt.Rows[0]["NumeroParcelas"].ToString() != "")
                        pNotasFiscais.NumeroParcelas = Convert.ToInt32(l_dt.Rows[0]["NumeroParcelas"].ToString());
                    if (l_dt.Rows[0]["Classificacao"].ToString() != "")
                        pNotasFiscais.Classificacao = Convert.ToInt32(l_dt.Rows[0]["Classificacao"].ToString());
                    if (l_dt.Rows[0]["ContaGerencial"].ToString() != "")
                        pNotasFiscais.ContaGerencial = Convert.ToInt32(l_dt.Rows[0]["ContaGerencial"].ToString());
                    if (l_dt.Rows[0]["TipoDocumento"].ToString() != "")
                        pNotasFiscais.TipoDocumento = Convert.ToInt32(l_dt.Rows[0]["TipoDocumento"].ToString());
                    pNotasFiscais.Historico = l_dt.Rows[0]["Historico"].ToString();
                    if (l_dt.Rows[0]["PercentualINSS"].ToString() != "")
                        pNotasFiscais.PercentualINSS = Convert.ToDecimal(l_dt.Rows[0]["PercentualINSS"].ToString());
                    if (l_dt.Rows[0]["ValorBaseINSS"].ToString() != "")
                        pNotasFiscais.ValorBaseINSS = Convert.ToDecimal(l_dt.Rows[0]["ValorBaseINSS"].ToString());
                    if (l_dt.Rows[0]["ArquivoParaImportacaoGerado"].ToString() != "")
                        pNotasFiscais.ArquivoParaImportacaoGerado = Convert.ToInt32(l_dt.Rows[0]["ArquivoParaImportacaoGerado"]);
                    if (l_dt.Rows[0]["DataReferencia"].ToString() != "")
                        pNotasFiscais.DataReferencia = Convert.ToDateTime(l_dt.Rows[0]["DataReferencia"].ToString()).Date.ToShortDateString();
                    else
                        pNotasFiscais.DataReferencia = "";
                    if (l_dt.Rows[0]["ValorDescontoIncondicionado"].ToString() != "")
                        pNotasFiscais.ValorDescontoIncondicionado = Convert.ToDecimal(l_dt.Rows[0]["ValorDescontoIncondicionado"].ToString());
                    pNotasFiscais.CodigoBROOKS_Impostos = l_dt.Rows[0]["CodigoBROOKS_Impostos"].ToString();
                    if (l_dt.Rows[0]["ValorPIS"].ToString() != "")
                        pNotasFiscais.ValorPIS = Convert.ToDecimal(l_dt.Rows[0]["ValorPIS"].ToString());
                    if (l_dt.Rows[0]["ValorCOFINS"].ToString() != "")
                        pNotasFiscais.ValorCOFINS = Convert.ToDecimal(l_dt.Rows[0]["ValorCOFINS"].ToString());
                    if (l_dt.Rows[0]["ValorContrSocial"].ToString() != "")
                        pNotasFiscais.ValorContrSocial = Convert.ToDecimal(l_dt.Rows[0]["ValorContrSocial"].ToString());
                    if (l_dt.Rows[0]["NumeroRecibo"].ToString() != "")
                        pNotasFiscais.NumeroRecibo = Convert.ToInt32(l_dt.Rows[0]["NumeroRecibo"].ToString());
                }
                
            }
            catch (Exception ex)
            {
                pNotasFiscais = new clsNotasFiscais();
            }
            finally
            {
                DesconectaBanco();
            }
            return pNotasFiscais;
        }

        public clsNotasFiscais PegaDados(clsNotasFiscais pNotasFiscais, int pNumeroNotaFiscal)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select n.NumeroNotaFiscal, n.DataEmissao, n.ValorTotal, n.ValorISS, c.Nome2 as NomeCliente, c.Codigo as CodigoCliente, \n";
                s = s + "       c.CNPJ_CPF, n.Vencimento, n.NumeroNF, n.PercentualISS, n.ValorPorExtenso, n.Cancelada, n.NFImpressa,  \n";
                s = s + "       n.PercentualIRRF, n.PercentualCRF, n.PercentualISS, n.DiasEntreVctos, n.NumeroParcelas, n.Classificacao,  \n";
                s = s + "       n.ContaGerencial, n.TipoDocumento, n.Historico, n.PercentualINSS, n.ValorBaseINSS,  \n";
                s = s + "       n.ArquivoParaImportacaoGerado, n.DataReferencia, n.ValorDescontoIncondicionado,  \n";
                s = s + "       n.CodigoBROOKS_Impostos, n.ValorPIS, n.ValorCOFINS, n.ValorContrSocial, n.NumeroRecibo \n";
                s = s + "from   NotasFiscais n  \n";
                s = s + "inner  join Clientes c on c.Codigo = n.CodigoCliente \n ";
                s = s + "where  n.NumeroNotaFiscal = " + pNumeroNotaFiscal + " \n ";
                s = s + "order  by n.NumeroNotaFiscal desc \n ";
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];

                    if (l_dt.Rows[0]["NumeroNotaFiscal"].ToString() != "")
                        pNotasFiscais.NumeroNotaFiscal = Convert.ToInt32(l_dt.Rows[0]["NumeroNotaFiscal"]);

                    if (l_dt.Rows[0]["CodigoCliente"].ToString() != "")
                    {
                        pNotasFiscais.CodigoCliente = Convert.ToInt32(l_dt.Rows[0]["CodigoCliente"]);
                        pNotasFiscais.NomeCliente = l_dt.Rows[0]["NomeCliente"].ToString();
                    }
                    if (l_dt.Rows[0]["DataEmissao"].ToString() != "")
                        pNotasFiscais.DataEmissao = Convert.ToDateTime(l_dt.Rows[0]["DataEmissao"].ToString()).Date.ToShortDateString();
                    else
                        pNotasFiscais.DataEmissao = "";
                    if (l_dt.Rows[0]["ValorTotal"].ToString() != "")
                        pNotasFiscais.ValorTotal = Convert.ToDecimal(l_dt.Rows[0]["ValorTotal"].ToString());
                    if (l_dt.Rows[0]["ValorISS"].ToString() != "")
                        pNotasFiscais.ValorISS = Convert.ToDecimal(l_dt.Rows[0]["ValorISS"].ToString());
                    if (l_dt.Rows[0]["Vencimento"].ToString() != "")
                        pNotasFiscais.Vencimento = Convert.ToDateTime(l_dt.Rows[0]["Vencimento"].ToString()).Date.ToShortDateString();
                    else
                        pNotasFiscais.Vencimento = "";
                    if (l_dt.Rows[0]["NumeroNF"].ToString() != "")
                        pNotasFiscais.NumeroNF = Convert.ToInt32(l_dt.Rows[0]["NumeroNF"].ToString());
                    if (l_dt.Rows[0]["PercentualISS"].ToString() != "")
                        pNotasFiscais.PercentualISS = Convert.ToDecimal(l_dt.Rows[0]["PercentualISS"].ToString());
                    pNotasFiscais.ValorPorExtenso = l_dt.Rows[0]["ValorPorExtenso"].ToString();
                    pNotasFiscais.Cancelada = 0;
                    if (l_dt.Rows[0]["Cancelada"].ToString() != "")
                        pNotasFiscais.Cancelada = Convert.ToInt32(l_dt.Rows[0]["Cancelada"].ToString());
                    if (l_dt.Rows[0]["NFImpressa"].ToString() != "")
                        pNotasFiscais.NFImpressa = Convert.ToInt32(l_dt.Rows[0]["NFImpressa"].ToString());
                    if (l_dt.Rows[0]["PercentualIRRF"].ToString() != "")
                        pNotasFiscais.PercentualIRRF = Convert.ToDecimal(l_dt.Rows[0]["PercentualIRRF"].ToString());
                    if (l_dt.Rows[0]["PercentualCRF"].ToString() != "")
                        pNotasFiscais.PercentualCRF = Convert.ToDecimal(l_dt.Rows[0]["PercentualCRF"].ToString());
                    if (l_dt.Rows[0]["DiasEntreVctos"].ToString() != "")
                        pNotasFiscais.DiasEntreVctos = Convert.ToInt32(l_dt.Rows[0]["DiasEntreVctos"].ToString());
                    if (l_dt.Rows[0]["NumeroParcelas"].ToString() != "")
                        pNotasFiscais.NumeroParcelas = Convert.ToInt32(l_dt.Rows[0]["NumeroParcelas"].ToString());
                    if (l_dt.Rows[0]["Classificacao"].ToString() != "")
                        pNotasFiscais.Classificacao = Convert.ToInt32(l_dt.Rows[0]["Classificacao"].ToString());
                    if (l_dt.Rows[0]["ContaGerencial"].ToString() != "")
                        pNotasFiscais.ContaGerencial = Convert.ToInt32(l_dt.Rows[0]["ContaGerencial"].ToString());
                    if (l_dt.Rows[0]["TipoDocumento"].ToString() != "")
                        pNotasFiscais.TipoDocumento = Convert.ToInt32(l_dt.Rows[0]["TipoDocumento"].ToString());
                    pNotasFiscais.Historico = l_dt.Rows[0]["Historico"].ToString();
                    if (l_dt.Rows[0]["PercentualINSS"].ToString() != "")
                        pNotasFiscais.PercentualINSS = Convert.ToDecimal(l_dt.Rows[0]["PercentualINSS"].ToString());
                    if (l_dt.Rows[0]["ValorBaseINSS"].ToString() != "")
                        pNotasFiscais.ValorBaseINSS = Convert.ToDecimal(l_dt.Rows[0]["ValorBaseINSS"].ToString());
                    if (l_dt.Rows[0]["ArquivoParaImportacaoGerado"].ToString() != "")
                        pNotasFiscais.ArquivoParaImportacaoGerado = Convert.ToInt32(l_dt.Rows[0]["ArquivoParaImportacaoGerado"]);
                    if (l_dt.Rows[0]["DataReferencia"].ToString() != "")
                        pNotasFiscais.DataReferencia = Convert.ToDateTime(l_dt.Rows[0]["DataReferencia"].ToString()).Date.ToShortDateString();
                    else
                        pNotasFiscais.DataReferencia = "";
                    if (l_dt.Rows[0]["ValorDescontoIncondicionado"].ToString() != "")
                        pNotasFiscais.ValorDescontoIncondicionado = Convert.ToDecimal(l_dt.Rows[0]["ValorDescontoIncondicionado"].ToString());
                    pNotasFiscais.CodigoBROOKS_Impostos = l_dt.Rows[0]["CodigoBROOKS_Impostos"].ToString();
                    if (l_dt.Rows[0]["ValorPIS"].ToString() != "")
                        pNotasFiscais.ValorPIS = Convert.ToDecimal(l_dt.Rows[0]["ValorPIS"].ToString());
                    if (l_dt.Rows[0]["ValorCOFINS"].ToString() != "")
                        pNotasFiscais.ValorCOFINS = Convert.ToDecimal(l_dt.Rows[0]["ValorCOFINS"].ToString());
                    if (l_dt.Rows[0]["ValorContrSocial"].ToString() != "")
                        pNotasFiscais.ValorContrSocial = Convert.ToDecimal(l_dt.Rows[0]["ValorContrSocial"].ToString());
                    if (l_dt.Rows[0]["NumeroRecibo"].ToString() != "")
                        pNotasFiscais.NumeroRecibo = Convert.ToInt32(l_dt.Rows[0]["NumeroRecibo"].ToString());
                }

            }
            catch (Exception ex)
            {
                pNotasFiscais = new clsNotasFiscais();
            }
            finally
            {
                DesconectaBanco();
            }
            return pNotasFiscais;
        }

        public DataTable PegaDados(clsNotasFiscais pNotasFiscais, int pCodigoCliente, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select n.NumeroNotaFiscal, n.DataEmissao, n.ValorTotal, n.ValorISS, c.Nome2 as NomeCliente, c.Codigo as CodigoCliente, \n";
                s = s + "       c.CNPJ_CPF, n.Vencimento, n.NumeroNF, n.PercentualISS, n.ValorPorExtenso, n.Cancelada, n.NFImpressa,  \n";
                s = s + "       n.PercentualIRRF, n.PercentualCRF, n.DiasEntreVctos, n.NumeroParcelas, n.Classificacao,  \n";
                s = s + "       n.ContaGerencial, n.TipoDocumento, n.Historico, n.PercentualINSS, n.ValorBaseINSS,  \n";
                s = s + "       n.ArquivoParaImportacaoGerado, n.DataReferencia, n.ValorDescontoIncondicionado,  \n";
                s = s + "       n.CodigoBROOKS_Impostos, n.ValorPIS, n.ValorCOFINS, n.ValorContrSocial, n.NumeroRecibo \n";
                s = s + "from   NotasFiscais n  \n";
                s = s + "inner  join Clientes c on c.Codigo = n.CodigoCliente \n ";
                if (pCodigoCliente > 0)
                {
                    s = s + "where  n.CodigoCliente = " + pCodigoCliente + " \n ";
                }
                if (pUltimoRegistro)
                {
                    s = s + "order by NumeroNotaFiscal desc \n ";
                }
                else if (pCodigoCliente == 0)
                {
                    s = s + " order by c.Nome2 \n ";
                }
                s = s + " limit 100 \n ";
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

        public DataTable PegaDados(int pCodigoCliente, string pDataInicial, string pDataFinal)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select NumeroNF, ValorTotal, TipoDocumento, DataEmissao \n ";
                s = s + "from   NotasFiscais \n ";
                if (pCodigoCliente == 0)
                {
                    s = s + "where  (Cancelada = 0 or Cancelada is null) \n ";
                }
                else
                {
                    s = s + "where  CodigoCliente = " + pCodigoCliente + " \n ";
                    s = s + "and    (Cancelada = 0 or Cancelada is null) \n ";
                }
                s = s + "and    DataReferencia >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n ";
                s = s + "and    DataReferencia <= '" + Convert.ToDateTime(pDataFinal).ToString("yyyy-MM-dd") + "' \n ";
                s = s + "order by DataEmissao Desc \n ";
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

        public DataTable RetornaNotasFiscais(string pDataInicial, string pDataFinal, string pOrdem, bool pSoFatura, bool pSoISSPF)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select nf.NumeroNF, nf.DataEmissao, Convert(nf.Cancelada, char(1)) as 'Cancelada', c.Nome as Cliente, nf.ValorTotal, Convert(nf.TipoDocumento, char(2)) as 'TipoDocumento', \n";
                s = s + "       nf.ValorPIS, nf.ValorCofins, (ValorBaseINSS * PercentualINSS / 100) as ValorINSS, (ValorTotal * PercentualIRRF / 100) as ValorIR, \n";
                s = s + "       nf.ValorContrSocial, (ValorTotal * PercentualISS / 100) as ValorISSRF, 0.00 as ValorISSPF, c.CNPJ_CPF \n ";
                s = s + "from   NotasFiscais nf \n ";
                s = s + "left join Clientes c on c.Codigo = nf.CodigoCliente \n";
                s = s + "where    nf.DataEmissao >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n ";
                s = s + "and      nf.DataEmissao <= '" + Convert.ToDateTime(pDataFinal).ToString("yyyy-MM-dd") + "' \n ";
                if (pSoFatura)
                    s = s + "and      TipoDocumento = 5 \n";
                else if (pSoISSPF)
                    s = s + "and      Length(CNPJ_CPF) <= 15 \n";
                if (pOrdem.Length > 0)
                    s = s + "order by " + pOrdem + " \n ";
                else
                    s = s + "order by nf.DataEmissao \n ";
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

        public DataTable PegaVendasPorRepresentante(string pDataInicial, string pDataFinal, string pCodigoFuncionarioComercial)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select nf.DataEmissao, nf.Vencimento, c.Codigo, c.Nome as Cliente, nf.ValorTotal, \n";
                s = s + "       c.CodigoFuncionarioComercial, f.Nome as NomeFuncionario, Convert(nf.Cancelada, char(1)) as 'Cancelada' \n ";
                s = s + "from   NotasFiscais nf \n ";
                s = s + "left   join Clientes c     on c.Codigo = nf.CodigoCliente \n";
                s = s + "left   join Funcionarios f on f.Codigo = c.CodigoFuncionarioComercial \n";
                s = s + "where  nf.Vencimento >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n ";
                s = s + "and    nf.Vencimento <= '" + Convert.ToDateTime(pDataFinal).ToString("yyyy-MM-dd") + "' \n ";
                s = s + "and   (nf.Cancelada = 0 or nf.cancelada is null) \n ";
                if (pCodigoFuncionarioComercial != "" && pCodigoFuncionarioComercial != "0" && pCodigoFuncionarioComercial != "&nbsp;")
                    s = s + "and   c.CodigoFuncionarioComercial = " + pCodigoFuncionarioComercial + "\n";
                s = s + "order by f.Nome, nf.Vencimento\n ";
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

        public decimal PegaValorTotalMesParaCalcularCRF(int pMes, int pAno, string pCNPJ)
        {
            decimal dRet = 0;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select sum(ValorTotal) as nValorTotal \n";
                s = s + "from   NotasFiscais \n";
                s = s + "       inner join Clientes on NotasFiscais.CodigoCliente = Clientes.Codigo \n";
                s = s + "where  Clientes.CNPJ_CPF = '" + pCNPJ + "'";
                s = s + "and    month(DataEmissao) = " + pMes + "  \n";
                s = s + "and    year(DataEmissao) = " + pAno + " \n";
                s = s + "and    (Cancelada = 0 or Cancelada is null) \n";
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows[0]["nValorTotal"].ToString() != "")
                        dRet = Convert.ToDecimal(l_dt.Rows[0]["nValorTotal"]);
                }
                dRet = Convert.ToDecimal(0);
            }
            catch (Exception ex)
            {
                dRet = Convert.ToDecimal(0);
            }
            finally
            {
                DesconectaBanco();
            }
            return dRet;
        }

        public DataTable PreencheDataTableOrdem(string pOrdem, int pCodigoCliente)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = "";
                s = s + "select n.NumeroNotaFiscal, n.DataEmissao, n.ValorTotal, n.ValorISS, c.Nome2 as NomeCliente, c.Codigo as CodigoCliente, \n";
                s = s + "       c.CNPJ_CPF, n.Vencimento, n.NumeroNF, n.PercentualISS, n.ValorPorExtenso, n.Cancelada, n.NFImpressa,  \n";
                s = s + "       n.PercentualIRRF, n.PercentualCRF, n.DiasEntreVctos, n.NumeroParcelas, n.Classificacao,  \n";
                s = s + "       n.ContaGerencial, n.TipoDocumento, n.Historico, n.PercentualINSS, n.ValorBaseINSS,  \n";
                s = s + "       n.ArquivoParaImportacaoGerado, n.DataReferencia, n.ValorDescontoIncondicionado,  \n";
                s = s + "       n.CodigoBROOKS_Impostos, n.ValorPIS, n.ValorCOFINS, n.ValorContrSocial, n.NumeroRecibo, '' as Situacao \n";
                s = s + "from   NotasFiscais n  \n";
                s = s + "inner  join Clientes c on c.Codigo = n.CodigoCliente \n ";
                if (pCodigoCliente > 0)
                {
                    s = s + "where  n.CodigoCliente = " + pCodigoCliente + " \n ";
                }
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
                s = s + "from NotasFiscais ";
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
        public string DadoExiste(int pNumeroNotaFiscal)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select NumeroNotaFiscal ";
                s = s + "from   NotasFiscais ";
                if (pNumeroNotaFiscal != 0)
                {
                    s = s + "where  NumeroNotaFiscal = " + pNumeroNotaFiscal + " ";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pNumeroNotaFiscal != 0)
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
        
        public int PegaUltimoNumeroNF()
        {
            int iRet = 0;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select NumeroNF \n";
                s = s + "from   NotasFiscais \n ";
                s = s + "where  TipoDocumento = 8 \n"; 
                s = s + "order  by NumeroNF desc limit 1";
                FillDataSet();
                if (l_ds.Tables[0].Rows.Count > 0)
                {
                    iRet = Convert.ToInt32(l_ds.Tables[0].Rows[0][0]);
                }
                else
                    iRet = 0;
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
        public int PegaUltimoNumeroRecibo()
        {
            int iRet = 0;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select NumeroNF \n";
                s = s + "from   NotasFiscais \n ";
                s = s + "where  TipoDocumento = 4 \n";
                s = s + "order  by NumeroNF desc limit 1";
                FillDataSet();
                if (l_ds.Tables[0].Rows.Count > 0)
                {
                    iRet = Convert.ToInt32(l_ds.Tables[0].Rows[0][0]);
                }
                else
                    iRet = 0;
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
        public int PegaUltimoNumeroFatura()
        {
            int iRet = 0;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select NumeroNF \n";
                s = s + "from   NotasFiscais \n ";
                s = s + "where  TipoDocumento = 5 \n";
                s = s + "order  by NumeroNF desc limit 1";
                FillDataSet();
                if (l_ds.Tables[0].Rows.Count > 0)
                {
                    iRet = Convert.ToInt32(l_ds.Tables[0].Rows[0][0]);
                }
                else
                    iRet = 0;
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

        public int PegaUltimoNumeroNotaFiscal(bool pComConexao = false)
        {
            int iRet = 0;
            try
            {
                if (pComConexao)
                    oDB.ConectaMySql();
                s = "";
                s = s + "select NumeroNotaFiscal ";
                s = s + "from   NotasFiscais where NumeroNotaFiscal > 0 order by NumeroNotaFiscal desc limit 1";
                FillDataSet();
                if (pComConexao)
                    oDB.DesconectaMySql();
                if (l_ds.Tables[0].Rows.Count > 0)
                {
                    iRet = Convert.ToInt32(l_ds.Tables[0].Rows[0][0]);
                }
                else
                    iRet = 0;
            }
            catch (Exception ex)
            {
                iRet = 0;
                if (pComConexao)
                    oDB.DesconectaMySql();
            }
            return iRet;
        }

        public int Inserir(clsNotasFiscais pNotasFiscais)
        {
            int iRet = 0;
            try
            {
                s = "";
                s = s + "insert into NotasFiscais \n";
                s = s + "( \n";
                if (pNotasFiscais.NumeroNotaFiscal > 0)
                    s = s + " NumeroNotaFiscal, \n ";
                s = s + "   DataEmissao, ValorTotal, ValorISS,  \n";
                s = s + "   Vencimento, NumeroNF, PercentualISS, ValorPorExtenso, Cancelada, NFImpressa,  \n";
                s = s + "   PercentualIRRF, PercentualCRF, DiasEntreVctos, NumeroParcelas, Classificacao,  \n";
                s = s + "   ContaGerencial, TipoDocumento, Historico, PercentualINSS, ValorBaseINSS,  \n";
                s = s + "   ArquivoParaImportacaoGerado, DataReferencia, ValorDescontoIncondicionado,  \n";
                s = s + "   CodigoBROOKS_Impostos, ValorPIS, ValorCOFINS, ValorContrSocial, NumeroRecibo, CodigoCliente \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pNotasFiscais.NumeroNotaFiscal > 0)
                    s = s + pNotasFiscais.NumeroNotaFiscal.ToString() + ", \n ";
                if (pNotasFiscais.DataEmissao == "")
                    s = s + "'0001-01-01', \n";
                else
                    s = s + "'" + Convert.ToDateTime(pNotasFiscais.DataEmissao).ToString("yyyy-MM-dd") + "', \n";
                if (pNotasFiscais.ValorTotal.ToString() != "")
                    s = s + " " + pNotasFiscais.ValorTotal.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pNotasFiscais.ValorISS.ToString() != "")
                    s = s + " " + pNotasFiscais.ValorISS.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pNotasFiscais.Vencimento == "")
                    s = s + "'0001-01-01', \n";
                else
                    s = s + "'" + Convert.ToDateTime(pNotasFiscais.Vencimento).ToString("yyyy-MM-dd") + "', \n";
                if (pNotasFiscais.NumeroNF.ToString() != "")
                    s = s + " " + pNotasFiscais.NumeroNF.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pNotasFiscais.PercentualISS.ToString() != "")
                    s = s + " " + pNotasFiscais.PercentualISS.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                s = s + "'" + pNotasFiscais.ValorPorExtenso + "', \n";
                if (pNotasFiscais.Cancelada.ToString() != "")
                    s = s + " " + pNotasFiscais.Cancelada.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pNotasFiscais.NFImpressa.ToString() != "")
                    s = s + " " + pNotasFiscais.NFImpressa.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pNotasFiscais.PercentualIRRF.ToString() != "")
                    s = s + " " + pNotasFiscais.PercentualIRRF.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pNotasFiscais.PercentualCRF.ToString() != "")
                    s = s + " " + pNotasFiscais.PercentualCRF.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pNotasFiscais.DiasEntreVctos.ToString() != "")
                    s = s + " " + pNotasFiscais.DiasEntreVctos.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pNotasFiscais.NumeroParcelas.ToString() != "")
                    s = s + " " + pNotasFiscais.NumeroParcelas.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pNotasFiscais.Classificacao.ToString() != "")
                    s = s + " " + pNotasFiscais.Classificacao.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pNotasFiscais.ContaGerencial.ToString() != "")
                    s = s + " " + pNotasFiscais.ContaGerencial.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pNotasFiscais.TipoDocumento.ToString() != "")
                    s = s + " " + pNotasFiscais.TipoDocumento.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                s = s + "'" + pNotasFiscais.Historico + "', \n";
                if (pNotasFiscais.PercentualINSS.ToString() != "")
                    s = s + " " + pNotasFiscais.PercentualINSS.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pNotasFiscais.ValorBaseINSS.ToString() != "")
                    s = s + " " + pNotasFiscais.ValorBaseINSS.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                s = s + "'" + pNotasFiscais.ArquivoParaImportacaoGerado + "', \n";
                if (pNotasFiscais.DataReferencia == "")
                    s = s + "'0001-01-01', \n";
                else
                    s = s + "'" + Convert.ToDateTime(pNotasFiscais.DataReferencia).ToString("yyyy-MM-dd") + "', \n";
                if (pNotasFiscais.ValorDescontoIncondicionado.ToString() != "")
                    s = s + " " + pNotasFiscais.ValorDescontoIncondicionado.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                s = s + "'" + pNotasFiscais.CodigoBROOKS_Impostos + "', \n";
                if (pNotasFiscais.ValorPIS.ToString() != "")
                    s = s + " " + pNotasFiscais.ValorPIS.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pNotasFiscais.ValorCOFINS.ToString() != "")
                    s = s + " " + pNotasFiscais.ValorCOFINS.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pNotasFiscais.ValorContrSocial.ToString() != "")
                    s = s + " " + pNotasFiscais.ValorContrSocial.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pNotasFiscais.NumeroRecibo.ToString() != "")
                    s = s + " " + pNotasFiscais.NumeroRecibo.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pNotasFiscais.CodigoCliente.ToString() != "")
                    s = s + " " + pNotasFiscais.CodigoCliente.ToString() + " \n";
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
                // pegar NumeroNotaFiscal gerado em autoincrement
                int UltimoNumeroNotaFiscal = PegaUltimoNumeroNotaFiscal(); 
                transaction.Commit();
                iRet = UltimoNumeroNotaFiscal;
            }
            catch
            {
                iRet = 0;
            }
            finally
            {
                DesconectaBanco();
            }
            return iRet;
        }
        public string Alterar(clsNotasFiscais pNotasFiscais, int pNumeroNotaFiscal)
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
                s = s + "update NotasFiscais \n";
                if (pNotasFiscais.DataEmissao == "")
                    s = s + " set DataEmissao= '0001-01-01', \n";
                else
                    s = s + " set DataEmissao = '" + Convert.ToDateTime(pNotasFiscais.DataEmissao).ToString("yyyy-MM-dd") + "', \n";
                if (pNotasFiscais.ValorTotal.ToString() != "")
                    s = s + " ValorTotal = " + pNotasFiscais.ValorTotal.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " ValorTotal = 0, \n";
                if (pNotasFiscais.ValorISS.ToString() != "")
                    s = s + " ValorISS = " + pNotasFiscais.ValorISS.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " ValorISS = 0, \n";
                if (pNotasFiscais.Vencimento == "")
                    s = s + " Vencimento = '0001-01-01', \n";
                else
                    s = s + " Vencimento = '" + Convert.ToDateTime(pNotasFiscais.Vencimento).ToString("yyyy-MM-dd") + "', \n";
                if (pNotasFiscais.NumeroNF.ToString() != "")
                    s = s + " NumeroNF = " + pNotasFiscais.NumeroNF.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " NumeroNF = 0, \n";
                if (pNotasFiscais.PercentualISS.ToString() != "")
                    s = s + " PercentualISS = " + pNotasFiscais.PercentualISS.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " PercentualISS = 0, \n";
                s = s + " ValorPorExtenso = '" + pNotasFiscais.ValorPorExtenso + "', \n";
                if (pNotasFiscais.Cancelada.ToString() != "")
                    s = s + " Cancelada = " + pNotasFiscais.Cancelada.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " Cancelada = 0, \n";
                if (pNotasFiscais.NFImpressa.ToString() != "")
                    s = s + " NFImpressa = " + pNotasFiscais.NFImpressa.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " NFImpressa = 0, \n";
                if (pNotasFiscais.PercentualIRRF.ToString() != "")
                    s = s + " PercentualIRRF = " + pNotasFiscais.PercentualIRRF.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " PercentualIRRF = 0, \n";
                if (pNotasFiscais.PercentualCRF.ToString() != "")
                    s = s + " PercentualCRF = " + pNotasFiscais.PercentualCRF.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " PercentualCRF = 0, \n";
                if (pNotasFiscais.DiasEntreVctos.ToString() != "")
                    s = s + " DiasEntreVctos = " + pNotasFiscais.DiasEntreVctos.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "DiasEntreVctos = 0, \n";
                if (pNotasFiscais.NumeroParcelas.ToString() != "")
                    s = s + " NumeroParcelas = " + pNotasFiscais.NumeroParcelas.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " NumeroParcelas = 0, \n";
                if (pNotasFiscais.Classificacao.ToString() != "")
                    s = s + " Classificacao = " + pNotasFiscais.Classificacao.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " Classificacao = 0, \n";
                if (pNotasFiscais.ContaGerencial.ToString() != "")
                    s = s + " ContaGerencial = " + pNotasFiscais.ContaGerencial.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " ContaGerencial = 0, \n";
                if (pNotasFiscais.TipoDocumento.ToString() != "")
                    s = s + " TipoDocumento = " + pNotasFiscais.TipoDocumento.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " TipoDocumento = 0, \n";
                s = s + " Historico = '" + pNotasFiscais.Historico + "', \n";
                if (pNotasFiscais.PercentualINSS.ToString() != "")
                    s = s + " PercentualINSS = " + pNotasFiscais.PercentualINSS.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " PercentualINSS = 0, \n";
                if (pNotasFiscais.ValorBaseINSS.ToString() != "")
                    s = s + " ValorBaseINSS = " + pNotasFiscais.ValorBaseINSS.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " ValorBaseINSS = 0, \n";
                s = s + " ArquivoParaImportacaoGerado = '" + pNotasFiscais.ArquivoParaImportacaoGerado + "', \n";
                if (pNotasFiscais.DataReferencia == "")
                    s = s + " DataReferencia = '0001-01-01', \n";
                else
                    s = s + " DataReferencia = '" + Convert.ToDateTime(pNotasFiscais.DataReferencia).ToString("yyyy-MM-dd") + "', \n";
                if (pNotasFiscais.ValorDescontoIncondicionado.ToString() != "")
                    s = s + " ValorDescontoIncondicionado = " + pNotasFiscais.ValorDescontoIncondicionado.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " ValorDescontoIncondicionado = 0, \n";
                s = s + " CodigoBROOKS_Impostos = '" + pNotasFiscais.CodigoBROOKS_Impostos + "', \n";
                if (pNotasFiscais.ValorPIS.ToString() != "")
                    s = s + " ValorPIS = " + pNotasFiscais.ValorPIS.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " ValorPIS = 0, \n";
                if (pNotasFiscais.ValorCOFINS.ToString() != "")
                    s = s + " ValorCOFINS = " + pNotasFiscais.ValorCOFINS.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " ValorCOFINS = 0, \n";
                if (pNotasFiscais.ValorContrSocial.ToString() != "")
                    s = s + " ValorContrSocial = " + pNotasFiscais.ValorContrSocial.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " ValorContrSocial = 0, \n";
                if (pNotasFiscais.NumeroRecibo.ToString() != "")
                    s = s + " NumeroRecibo = " + pNotasFiscais.NumeroRecibo.ToString().Replace(",", ".") + " \n";
                else
                    s = s + " NumeroRecibo = 0 \n";
                s = s + "where NumeroNotaFiscal = " + pNumeroNotaFiscal.ToString() ;
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
        public string Excluir(int pNumeroNotaFiscal)
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
                if (pNumeroNotaFiscal > 0)
                {
                    s = s + "delete from NotasFiscais ";
                    s = s + "where  NumeroNotaFiscal = " + pNumeroNotaFiscal.ToString();
                }
                command.CommandText = s;
                command.ExecuteNonQuery();
                if (pNumeroNotaFiscal > 0)
                {
                    s = "";
                    s = s + "delete from CorpoNotasFiscais ";
                    s = s + "where  NumeroNotaFiscal = " + pNumeroNotaFiscal.ToString();
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                }
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
                s = s + "select NumeroNotaFiscal, DataEmissao, ValorTotal, ValorISS,  \n";
                s = s + "       Vencimento, NumeroNF, PercentualISS, ValorPorExtenso, Cancelada, NFImpressa,  \n";
                s = s + "       PercentualIRRF, PercentualCRF, DiasEntreVctos, NumeroParcelas, Classificacao,  \n";
                s = s + "       ContaGerencial, TipoDocumento, Historico, PercentualINSS, ValorBaseINSS,  \n";
                s = s + "       ArquivoParaImportacaoGerado, DataReferencia, ValorDescontoIncondicionado,  \n";
                s = s + "       CodigoBROOKS_Impostos, ValorPIS, ValorCOFINS, ValorContrSocial, NumeroRecibo \n";
                s = s + "from   NotasFiscais \n";
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
        public string SalvarCancelamento(clsNotasFiscais pNotasFiscais, int pNumeroNotaFiscal)
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
                s = s + "update NotasFiscais \n";

                if (pNotasFiscais.Cancelada.ToString() != "")
                    s = s + " set Cancelada = " + pNotasFiscais.Cancelada.ToString().Replace(",", ".") + " \n";
                else
                    s = s + " set Cancelada = 0 \n";

                s = s + "where NumeroNotaFiscal = " + pNumeroNotaFiscal.ToString();
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
    }
}