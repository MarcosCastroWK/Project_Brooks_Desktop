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
    public class clsLancamentoMTRDados
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
            l_myData.SelectCommand.CommandTimeout = 1200;
            l_myData.Fill(l_ds);
            l_dt = l_ds.Tables[0];
        }
        private void DesconectaBanco()
        {
            oDB.DesconectaMySql();
        }

        public int PegaItemsDescarga(string pNumeroTicket, string pAnoDescarga)
        {
            int dRet = 0;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Count(*) as xItems \n";
                s = s + "from   LancamentoMTR \n";
                s = s + "where  Ticket = '" + pNumeroTicket + "' \n";
                s = s + "and    year(DataDescarga) = " + pAnoDescarga + " \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    dRet = Convert.ToInt32(l_dt.Rows[0][0]);
            }
            catch
            {
                dRet = 0;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return dRet;
        }

        public DataTable PegaListaItemsDescarga(string pNumeroTicket, string pAnoDescarga)
        {
            DataTable dRet = new DataTable();
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select convert(l.CodigoCliente, char) as xCodigoCliente, c.NomeFantasia, lmtr.Quantidade, lmtr.Franquia, l.NumeroCaixa, lmtr.DataDescarga \n";
                s = s + "from   LancamentoMTR lmtr \n";
                s = s + "left   Join Lancamentos l on l.NumeroLancamento = lmtr.NumeroLancamento \n";
                s = s + "left   join Clientes c on c.Codigo = l.CodigoCliente \n";
                s = s + "where  lmtr.Ticket = '" + pNumeroTicket + "' \n";
                s = s + "and    year(lmtr.DataDescarga) = " + pAnoDescarga + " \n";
                s = s + "union all \n";
                s = s + "select '' as CodigoCliente, 'Totais' as NomeFantasia, sum(lmtr.Quantidade), sum(lmtr.Franquia), '' as NumeroCaixa, '' as DataDescarga \n";
                s = s + "from   LancamentoMTR lmtr \n";
                s = s + "left   Join Lancamentos l on l.NumeroLancamento = lmtr.NumeroLancamento \n";
                s = s + "left   join Clientes c on c.Codigo = l.CodigoCliente \n";
                s = s + "where  lmtr.Ticket = '" + pNumeroTicket + "' \n";
                s = s + "and    year(lmtr.DataDescarga) = " + pAnoDescarga + " \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    dRet = l_dt;
            }
            catch
            {
                dRet = new DataTable();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return dRet;
        }

        public clsLancamentoMTR PegaDados(Int64 pNumeroMTRe, string pCodigoIbama)
        {
            clsLancamentoMTR oLancMTR = new clsLancamentoMTR();
            try
            {
                if (pNumeroMTRe > 0)
                {
                    // verifica se existe com o mesmo código ibama
                    ConectaBanco();
                    s = "";
                    s = s + "select lmtr.NumeroLancamento, lmtr.CodigoResiduo, lmtr.NumeroMTR, lmtr.NumeroMTRFatima \n";
                    s = s + "from   LancamentoMTR lmtr \n";
                    s = s + "inner  join Residuos r on r.Codigo = lmtr.CodigoResiduo \n ";
                    s = s + "inner  join IBAMA    i on i.Codigo = r.CodigoIBAMA \n ";
                    s = s + "where  NumeroMTRFatima = " + pNumeroMTRe + " \n";
                    s = s + "and    i.CodigoIBAMA = '" + pCodigoIbama + "' \n ";
                    FillDataSet();
                    DesconectaBanco();
                    decimal qt = 0;
                    if (l_ds.Tables.Count > 0)
                    {
                        l_dt = l_ds.Tables[0];
                        if (l_dt.Rows.Count > 0)
                        {
                            qt = 0;
                            foreach (DataRow dr in l_dt.Rows)
                            {
                                oLancMTR.NumeroLancamento = Convert.ToInt32(dr["NumeroLancamento"]);
                                oLancMTR.CodigoResiduo = Convert.ToInt32(dr["CodigoResiduo"]);
                                oLancMTR.NumeroMTRFatima = Convert.ToInt32(dr["NumeroMTRFatima"]);
                                oLancMTR.NumeroMTR = Convert.ToInt32(dr["NumeroMTR"]);
                                oLancMTR = PegaDados(oLancMTR, oLancMTR.NumeroLancamento, oLancMTR.CodigoResiduo, oLancMTR.NumeroMTRFatima, oLancMTR.NumeroMTR);
                                qt = qt + oLancMTR.Quantidade;
                            }
                            oLancMTR.Quantidade = qt;
                        }
                        else
                        {
                            // caso não exista com o mesmo código do ibama - acumular os diferentes
                            ConectaBanco();
                            s = "";
                            s = s + "select lmtr.NumeroLancamento, lmtr.CodigoResiduo, lmtr.NumeroMTR, lmtr.NumeroMTRFatima \n";
                            s = s + "from   LancamentoMTR lmtr \n";
                            s = s + "inner  join Residuos r on r.Codigo = lmtr.CodigoResiduo \n ";
                            s = s + "inner  join IBAMA    i on i.Codigo = r.CodigoIBAMA \n ";
                            s = s + "where  NumeroMTRFatima = " + pNumeroMTRe + " \n";
                            FillDataSet();
                            DesconectaBanco();
                            l_dt = l_ds.Tables[0];
                            if (l_dt.Rows.Count > 0)
                            {
                                qt = 0;
                                foreach (DataRow dr in l_dt.Rows)
                                {
                                    oLancMTR.NumeroLancamento = Convert.ToInt32(dr["NumeroLancamento"]);
                                    oLancMTR.CodigoResiduo = Convert.ToInt32(dr["CodigoResiduo"]);
                                    oLancMTR.NumeroMTRFatima = Convert.ToInt32(dr["NumeroMTRFatima"]);
                                    oLancMTR.NumeroMTR = Convert.ToInt32(dr["NumeroMTR"]);
                                    oLancMTR = PegaDados(oLancMTR, oLancMTR.NumeroLancamento, oLancMTR.CodigoResiduo, oLancMTR.NumeroMTRFatima, oLancMTR.NumeroMTR);
                                    qt = qt + oLancMTR.Quantidade;
                                }
                                oLancMTR.Quantidade = qt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oLancMTR = new clsLancamentoMTR();
            }
            return oLancMTR;
        }

        public clsLancamentoMTR PegaDados(clsLancamentoMTR pLancamentoMTR, int pNumeroLancamento, int pCodigoResiduo = 0, long pNumeroMTRe = 0,
                                          int pNumeroMTR = 0)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select NumeroLancamento, NumeroMTR, CodigoResiduo, Franquia, Quantidade, ValorUnitario, \n";
                s = s + "       ValorTotal, Deposito, Unidade, Ticket, CodigoAterroSanitario, observacao, \n";
                s = s + "       NumeroMTRFatima, Motivo, DataDescarga, DescargaMTRe, ControleInternoDescarga \n";
                s = s + "from   LancamentoMTR ";
                if (pNumeroLancamento > 0)
                {
                    s = s + "where  NumeroLancamento = " + pNumeroLancamento + " and CodigoResiduo > 0 \n";
                    if (pCodigoResiduo > 0)
                        s = s + "and   CodigoResiduo = " + pCodigoResiduo + " \n";
                    if (pNumeroMTRe > 0)
                        s = s + "and   NumeroMTRFatima = " + pNumeroMTRe + " \n";
                    if (pNumeroMTR > 0)
                        s = s + "and   NumeroMTR = " + pNumeroMTR + " \n";
                    s = s + "limit 1";
                }
                else if (pNumeroLancamento == 0)
                {
                    s = s + " order by Descricao limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows[0]["NumeroLancamento"].ToString() != "")
                        pLancamentoMTR.NumeroLancamento = Convert.ToInt32(l_dt.Rows[0]["NumeroLancamento"]);
                    if (l_dt.Rows[0]["NumeroMTR"].ToString() != "")
                        pLancamentoMTR.NumeroMTR = Convert.ToInt32(l_dt.Rows[0]["NumeroMTR"]);
                    if (l_dt.Rows[0]["CodigoResiduo"].ToString() != "")
                        pLancamentoMTR.CodigoResiduo = Convert.ToInt32(l_dt.Rows[0]["CodigoResiduo"]);
                    if (l_dt.Rows[0]["Franquia"].ToString() != "")
                        pLancamentoMTR.Franquia = Convert.ToDecimal(l_dt.Rows[0]["Franquia"]);
                    if (l_dt.Rows[0]["Quantidade"].ToString() != "")
                        pLancamentoMTR.Quantidade = Convert.ToDecimal(l_dt.Rows[0]["Quantidade"]);
                    if (l_dt.Rows[0]["ValorUnitario"].ToString() != "")
                        pLancamentoMTR.ValorUnitario = Convert.ToDecimal(l_dt.Rows[0]["ValorUnitario"]);
                    if (l_dt.Rows[0]["ValorTotal"].ToString() != "")
                        pLancamentoMTR.ValorTotal = Convert.ToDecimal(l_dt.Rows[0]["ValorTotal"]);
                    pLancamentoMTR.Deposito = l_dt.Rows[0]["Deposito"].ToString();
                    pLancamentoMTR.Unidade = l_dt.Rows[0]["Unidade"].ToString();
                    pLancamentoMTR.Ticket = l_dt.Rows[0]["Ticket"].ToString();
                    if (l_dt.Rows[0]["CodigoAterroSanitario"].ToString() != "")
                        pLancamentoMTR.CodigoAterroSanitario = Convert.ToInt32(l_dt.Rows[0]["CodigoAterroSanitario"]);
                    pLancamentoMTR.observacao = l_dt.Rows[0]["observacao"].ToString();
                    if (l_dt.Rows[0]["NumeroMTRFatima"].ToString() != "")
                        pLancamentoMTR.NumeroMTRFatima = Convert.ToInt32(l_dt.Rows[0]["NumeroMTRFatima"]);
                    pLancamentoMTR.Motivo = l_dt.Rows[0]["Motivo"].ToString();
                    if (l_dt.Rows[0]["DataDescarga"].ToString() != "")
                        pLancamentoMTR.DataDescarga = Convert.ToDateTime(l_dt.Rows[0]["DataDescarga"].ToString()).Date.ToShortDateString();
                    pLancamentoMTR.DescargaMTRe = l_dt.Rows[0]["DescargaMTRe"].ToString();
                    pLancamentoMTR.ControleInternoDescarga = l_dt.Rows[0]["ControleInternoDescarga"].ToString();
                }
            }
            catch (Exception ex)
            {
                pLancamentoMTR = new clsLancamentoMTR();
            }
            finally
            {
                DesconectaBanco();
            }
            return pLancamentoMTR;
        }
        public DataTable PegaDados(clsLancamentoMTR pLancamentoMTR, int pNumeroLancamento, bool pUltimoRegistro, string pDataRetirda = "", string pDataInicial = "", string pDataFinal = "")
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select distinct lmtr.NumeroLancamento, lmtr.NumeroMTR, lmtr.CodigoResiduo, r.DescricaoReduzida, lmtr.Unidade, lmtr.Franquia as QtColetada, lmtr.Quantidade as QtDescarga,  \n";
                s = s + "       lmtr.ValorUnitario, lmtr.ValorTotal, lmtr.Observacao, lmtr.Deposito, lmtr.DataDescarga, lmtr.Ticket, lmtr.CodigoAterroSanitario, ats.Hora as HoraDescarga, \n";
                s = s + "       lmtr.NumeroMTRFatima, lmtr.Motivo, lmtr.DescargaMTRe, lmtr.ControleInternoDescarga \n";
                s = s + "from   LancamentoMTR lmtr \n";
                s = s + "inner  join Lancamentos l on l.NumeroLancamento = lmtr.NumeroLancamento \n ";
                s = s + "left   Join Residuos r on r.Codigo = lmtr.CodigoResiduo \n";
                s = s + "left   join AterroSanitario ats on ats.NumeroLancamento = lmtr.NumeroLancamento and ats.CodigoResiduo = lmtr.CodigoResiduo \n";
                if (pNumeroLancamento > 0)
                {
                    s = s + "where  lmtr.NumeroLancamento = " + pNumeroLancamento + " \n";
                }
                if (pUltimoRegistro)
                {
                    s = s + "order by lmtr.NumeroLancamento desc ";
                }
                else if (pNumeroLancamento == 0 && pDataRetirda != "")
                {
                    s = s + " where l.DataRetirada = '" + Convert.ToDateTime(pDataRetirda).ToString("yyyy-MM-dd") + "' \n ";
                }
                else if (pNumeroLancamento == 0 && pDataInicial != "" & pDataFinal != "")
                {
                    s = s + " where l.DataRetirada >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n ";
                    s = s + " and   l.DataRetirada <= '" + Convert.ToDateTime(pDataFinal).ToString("yyyy-MM-dd") + "' \n ";
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
        public DataTable RetornaTotalMTRe(string pNumeroMTRe, string pCodigoIBAMA, string pDataInicial = "", string pDataFinal = "", string pDestinoFinal = "")
        {
            l_dt = new DataTable();
            try
            {
                ConectaBanco();
                if (pDataInicial != "" && pDataFinal != "" && pNumeroMTRe != "")
                {
                    s = "";
                    s = s + "select sum(TotalQtdeBROOKS) as TotalQtdeBROOKS, sum(TotalQtdeCDFe) as TotalQtdeCDFe from \n";
                    s = s + "( \n";
                    s = s + "  select lmtr.Quantidade as TotalQtdeBROOKS,  0 as TotalQtdeCDFe \n";
                    s = s + "  from   LancamentoMTR         as lmtr \n";
                    s = s + "  inner  join Lancamentos      as l on l.NumeroLancamento = lmtr.NumeroLancamento  \n";
                    s = s + "  inner  join Residuos         as r on r.Codigo = lmtr.CodigoResiduo \n";
                    s = s + "  inner  join IBAMA            as ib on ib.Codigo = r.CodigoIBAMA \n";
                    s = s + "  where  lmtr.DataDescarga >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n ";
                    s = s + "  and    lmtr.DataDescarga <= '" + Convert.ToDateTime(pDataFinal).ToString("yyyy-MM-dd") + "' \n ";
                    s = s + "  and    lmtr.NumeroMTRFatima = '" + pNumeroMTRe + "' \n";
                    if (pCodigoIBAMA != "")
                        s = s + "  and    ib.CodigoIBAMA = '" + pCodigoIBAMA + "' \n";
                    if (pDestinoFinal != "")
                        s = s + "  and    cast(lmtr.deposito as signed) = " + pDestinoFinal + " \n";
                    s = s + "  and    not lmtr.Unidade in('M3', 'm3', 'M³', 'm³', 'CX', 'cx', 'Cx', 'cX') \n";
                    s = s + "  union all \n";
                    s = s + "  select (lmtr.Quantidade * (select M3PorTon   from Residuos    where Codigo = lmtr.CodigoResiduo limit 1) * 1000) as TotalQtdeBROOKS,  0 as TotalQtdeCDFe \n";
                    s = s + "  from   LancamentoMTR         as lmtr \n";
                    s = s + "  inner  join Lancamentos      as l  on l.NumeroLancamento = lmtr.NumeroLancamento  \n";
                    s = s + "  inner  join Cacambas         as ca on ca.Numero = l.NumeroCaixa \n";
                    s = s + "  inner  join Residuos         as r on r.Codigo = lmtr.CodigoResiduo \n";
                    s = s + "  inner  join IBAMA            as ib on ib.Codigo = r.CodigoIBAMA \n";
                    s = s + "  where  lmtr.DataDescarga >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n ";
                    s = s + "  and    lmtr.DataDescarga <= '" + Convert.ToDateTime(pDataFinal).ToString("yyyy-MM-dd") + "' \n ";
                    s = s + "  and    lmtr.NumeroMTRFatima = '" + pNumeroMTRe + "' \n";
                    if (pCodigoIBAMA != "")
                        s = s + "  and    ib.CodigoIBAMA = '" + pCodigoIBAMA + "' \n";
                    if (pDestinoFinal != "")
                        s = s + "  and    cast(lmtr.deposito as signed) = " + pDestinoFinal + " \n";
                    s = s + "  and    lmtr.Unidade in('M3', 'm3', 'M³', 'm³') \n";
                    s = s + "  union all \n";
                    s = s + "  select (lmtr.Quantidade * (select r.M3PorTon from Residuos as r where Codigo = lmtr.CodigoResiduo limit 1) * ca.Capacidade * 1000) as TotalQtdeBROOKS,  0 as TotalQtdeCDFe \n";
                    s = s + "  from   LancamentoMTR         as lmtr  \n";
                    s = s + "  inner  join Lancamentos      as l  on l.NumeroLancamento = lmtr.NumeroLancamento  \n";
                    s = s + "  inner  join Cacambas         as ca on ca.Numero = l.NumeroCaixa \n";
                    s = s + "  inner  join Residuos         as r on r.Codigo = lmtr.CodigoResiduo \n";
                    s = s + "  inner  join IBAMA            as ib on ib.Codigo = r.CodigoIBAMA \n";
                    s = s + "  where  lmtr.DataDescarga >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n ";
                    s = s + "  and    lmtr.DataDescarga <= '" + Convert.ToDateTime(pDataFinal).ToString("yyyy-MM-dd") + "' \n ";
                    s = s + "  and    lmtr.NumeroMTRFatima = '" + pNumeroMTRe + "' \n";
                    if (pCodigoIBAMA != "")
                        s = s + "  and    ib.CodigoIBAMA = '" + pCodigoIBAMA + "' \n";
                    if (pDestinoFinal != "")
                        s = s + "  and    cast(lmtr.deposito as signed) = " + pDestinoFinal + " \n";
                    s = s + "  and    lmtr.Unidade in('CX', 'cx', 'Cx', 'cX') \n";
                    s = s + "  union all \n";
                    s = s + "  select 0 as TotalQtdeBROOKS, sum(Quantidade) as TotalQtdeCDFe \n";
                    s = s + "  from   CDFe \n";
                    s = s + "  where  NumeroMTRe  = '" + pNumeroMTRe + "' \n";
                    if (pCodigoIBAMA != "")
                        s = s + "  and    CodigoIBAMA = '" + pCodigoIBAMA + "' \n";
                    s = s + ") qts \n";
                    FillDataSet();
                }
            }
            catch
            {
                l_dt = new DataTable();
            }
            finally
            {
                DesconectaBanco();
            }
            return l_dt;
        }
        public DataTable PreencheDTparaRelatorioMovimentacao(clsLancamentoMTR pLancamentoMTR, string pDataInicial = "", string pDataFinal = "", int pCodigoDestinoFinal = 0,
                                                             int pCodigoCliente = 0, bool pTotalMov = false, bool pUsarDataRetirada = false, bool pColocarFiltroCodigoIBAMA = false)
        {
            try
            {
                ConectaBanco();
                s = "";
                if (pTotalMov)
                {
                    //Data - Código - Cliente - Und - Qtde Coletada - Qtde Descarga - Residuo - Nº Ticket - Vl.Unitario - Vl.Total - Nº MTR-e - Total(Kg)MTRe
                    s = s + "select lmtr.DataDescarga, l.CodigoCliente, c.Nome, lmtr.Unidade, lmtr.Franquia as Quantidade, lmtr.Quantidade as QtDescarga, r.DescricaoReduzida, lmtr.Ticket, \n";
                    s = s + "       lmtr.ValorUnitario, 0.00 as ValorTotal, cast(lmtr.NumeroMTRFatima as unsigned) as MTRe, 0.00 as PesoTotalMTRe, 0.00 as PesoTotalCliente, h.Placas, i.CodigoIBAMA, i.Descricao, \n";
                    s = s + "       lmtr.CodigoResiduo,cast(lmtr.Deposito as signed) as CodigoAterro, r.M3PorTon, l.NumeroCaixa, c.CNPJ_CPF, f.Nome as NomeMotorista, \n";
                    s = s + "       lmtr.NumeroLancamento, cd.NumeroCDFe, cd.Quantidade as QtdeCDFe, cd.Situacao, cd.Placas as PlacasNaCDFe, c.ClienteEmissaoMTReRCD \n";
                }
                else
                {
                    s = s + "select lmtr.DataDescarga, l.CodigoCliente, c.Nome, c.CNPJ_CPF, lmtr.Quantidade as QtDescarga, r.DescricaoReduzida, lmtr.Ticket, f.Nome as NomeMotorista, \n";
                    s = s + "       h.Placas, i.CodigoIBAMA, i.Descricao, lmtr.NumeroMTRFatima as MTRe, 0.00 as PesoTotalCliente, lmtr.Unidade, lmtr.Franquia as Quantidade, lmtr.ValorUnitario, \n";
                    s = s + "       lmtr.CodigoResiduo,cast(lmtr.Deposito as signed) as CodigoAterro, r.M3PorTon, l.NumeroCaixa, \n";
                    s = s + "       lmtr.NumeroLancamento, cd.NumeroCDFe, cd.Quantidade as QtdeCDFe, cd.Situacao, cd.Placas as PlacasNaCDFe, c.ClienteEmissaoMTReRCD, l.DataRetirada \n";
                }
                s = s + "FROM  LancamentoMTR        as lmtr \n";
                s = s + "inner join Lancamentos     as l  on L.NumeroLancamento = lmtr.NumeroLancamento \n";
                s = s + "inner join Residuos        as r  on r.Codigo = lmtr.CodigoResiduo \n";
                s = s + "inner join Clientes        as c  on l.CodigoCliente = c.Codigo \n";
                s = s + "inner join IBAMA           as i  on i.Codigo = r.CodigoIbama \n";
                s = s + "inner join Funcionarios    as f  on f.Codigo = l.CodigoMotoristaRetirou \n";
                s = s + "inner join Caminhoes       as h  on h.codigo = l.CodigoCaminhoRetirada \n";
                if (pColocarFiltroCodigoIBAMA)
                    s = s + "left  join CDFe            as cd on cd.NumeroMTRe = lmtr.NumeroMTRFatima and cd.CodigoIBAMA = i.CodigoIBAMA \n ";
                else
                    s = s + "left  join CDFe            as cd on cd.NumeroMTRe = lmtr.NumeroMTRFatima \n ";
                if (pDataInicial != "" && pDataFinal != "")
                {
                    if (pUsarDataRetirada)
                    {
                        s = s + " where l.DataRetirada >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n ";
                        s = s + " and   l.DataRetirada <= '" + Convert.ToDateTime(pDataFinal).ToString("yyyy-MM-dd") + "' \n ";
                    }
                    else
                    {
                        s = s + " where lmtr.DataDescarga >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n ";
                        s = s + " and   lmtr.DataDescarga <= '" + Convert.ToDateTime(pDataFinal).ToString("yyyy-MM-dd") + "' \n ";
                    }
                    s = s + " and   (r.EhServico = 0 or r.EhServico is null) \n";
                }
                if (pCodigoDestinoFinal > 0 && pCodigoDestinoFinal != 9999)
                {
                    if (s.IndexOf("where") > -1)
                        s = s + "and   cast(lmtr.Deposito as signed) = " + pCodigoDestinoFinal.ToString() + " \n";
                    else
                        s = s + "where cast(lmtr.Deposito as signed) = " + pCodigoDestinoFinal.ToString() + " \n";
                }
                else if (pCodigoDestinoFinal == 9999) // Só os que estão em DTR
                {
                    if (s.IndexOf("where") > -1)
                        s = s + "and   Left(lmtr.Deposito, 3) = 'DTR' \n";
                    else
                        s = s + "where Left(lmtr.Deposito, 3) = 'DTR' \n";
                }
                if (pCodigoCliente > 0)
                {
                    if (s.IndexOf("where") > -1)
                        s = s + "and   l.CodigoCliente = " + pCodigoCliente.ToString() + " \n";
                    else
                        s = s + "where l.CodigoCliente = " + pCodigoCliente.ToString() + " \n";
                }
                s = s + "group by lmtr.NumeroLancamento, lmtr.DataDescarga, c.Nome, lmtr.Unidade, lmtr.NumeroMTRFatima, lmtr.CodigoResiduo, \n";
                s = s + "         lmtr.ValorUnitario, c.CNPJ_CPF, l.CodigoCliente, r.DescricaoReduzida, f.Nome, i.CodigoIBAMA, i.Descricao, l.DataRetirada,  lmtr.Ticket, lmtr.NumeroMTR \n";
                if (pUsarDataRetirada) // quando relatorio com residuos em DTR a ordem é diferente
                    s = s + "order by  cast(lmtr.NumeroMTRFatima as unsigned), i.CodigoIBAMA, c.Nome, lmtr.DataDescarga \n";
                else
                {
                    //s = s + "order by c.Nome, lmtr.NumeroMTRFatima, i.CodigoIBAMA, lmtr.DataDescarga \n";                    
                    s = s + "order by Convert(lmtr.NumeroMTRFatima, signed), c.Nome, i.CodigoIBAMA, lmtr.DataDescarga \n";
                }
                FillDataSet();
            }
            catch
            {
                l_dt = new DataTable();
            }
            finally
            {
                DesconectaBanco();
            }
            return l_dt;
        }

        public DataTable PreencheDTAterroSanitario(clsLancamentoMTR pLancamentoMTR, string pDataInicial = "", string pDataFinal = "", int pCodigoDestinoFinal = 0, int pCodigoCliente = 0, bool pAgrupadoPorMTRe = true, bool pMostrarMTReSemTicket = false)
        {
            ConectaBanco();
            s = "";
            s = s + "select lmtr.DataDescarga as 'Data', s.Hora, left(lmtr.deposito, 8) as LocalAterro, lmtr.Ticket as 'NumeroTicket', f.Nome as NomeMotorista, h.Placas, ";
            if (pAgrupadoPorMTRe)
                s = s + "max(s.NumeroCaixa) as NumeroCaixa, s.CodigoMotorista, \n";
            else
                s = s + "min(s.NumeroCaixa) as NumeroCaixa, s.CodigoMotorista, \n";

            //(select sum(TotalPeso) from AterroSanitario where NumeroTicket = s.NumeroTicket and CodigoResiduo = 999) as TotalPeso, sum(lmtr.Quantidade) as PesoIndividual, l.CodigoCliente, c.Nome, c.CNPJ_CPF, 0 as TotalGrupo, lmtr.NumeroMTRFatima as MTRe \n";
            //está alteração - foi porque tinha o sum na Quantidade e estava dobrando o valor no Relatório Movimentação com MTR-e 
            s = s + "       (select sum(TotalPeso) from AterroSanitario where NumeroTicket = s.NumeroTicket and CodigoResiduo=999) as TotalPeso, lmtr.Quantidade as PesoIndividual, l.CodigoCliente, c.Nome, c.CNPJ_CPF, 0 as TotalGrupo, lmtr.NumeroMTRFatima as MTRe \n";

            s = s + "FROM  LancamentoMTR   as lmtr \n";
            if (pAgrupadoPorMTRe)
                s = s + "left  join AterroSanitario as s on s.NumeroLancamento = lmtr.NumeroLancamento and s.CodigoResiduo = lmtr.CodigoResiduo and s.numeroticket = lmtr.ticket \n";
            else
                s = s + "left  join AterroSanitario as s on s.NumeroLancamento = lmtr.NumeroLancamento and s.CodigoResiduo = lmtr.CodigoResiduo \n";
            s = s + "left  join Lancamentos     as l    on l.NumeroLancamento = s.NumeroLancamento \n";
            s = s + "left  join Clientes        as c    on l.CodigoCliente = c.Codigo \n";
            s = s + "left  join Funcionarios    as f    on f.Codigo = l.CodigoMotoristaRetirou \n";
            s = s + "left  join Caminhoes       as h    on h.Codigo = l.CodigoCaminhoRetirada \n";
            if (pDataInicial != "" && pDataFinal != "")
            {
                s = s + " where lmtr.DataDescarga >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n ";
                s = s + " and   lmtr.DataDescarga <= '" + Convert.ToDateTime(pDataFinal).ToString("yyyy-MM-dd") + "' \n ";
            }
            if (pCodigoDestinoFinal > 0)
            {
                if (s.IndexOf("where") > -1)
                    s = s + "and   lmtr.Deposito like '" + pCodigoDestinoFinal.ToString() + "-%' \n";
                else
                    s = s + "where lmtr.Deposito like '" + pCodigoDestinoFinal.ToString() + "-%' \n";
            }
            s = s + "and   (s.Excluido = 0 or s.Excluido is null)  \n";
            if (!pMostrarMTReSemTicket)
                s = s + "and lmtr.Ticket <> '' ";
            if (pCodigoCliente > 0)
            {
                if (s.IndexOf("where") > -1)
                    s = s + "and   s.CodigoCliente = " + pCodigoCliente.ToString() + " \n";
                else
                    s = s + "where s.CodigoCliente = " + pCodigoCliente.ToString() + " \n";
                s = s + "union all \n ";
                s = s + "select s.Data, s.Hora, '' as LocalAterro, s.NumeroTicket, '' as NomeMotorista, '' as Placas, max(s.NumeroCaixa) as NumeroCaixa, s.CodigoMotorista, \n";
                s = s + "       sum(s.TotalPeso) as TotalPeso, sum(s.TotalPeso) as PesoIndividual, s.CodigoCliente, '' as Nome, '' as CNPJ_CPF, 0 as TotalGrupo, '' as MTRe \n";
                s = s + "FROM AterroSanitario       as s \n";
                if (pDataInicial != "" && pDataFinal != "")
                {
                    s = s + " where s.Data >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n ";
                    s = s + " and   s.Data <= '" + Convert.ToDateTime(pDataFinal).ToString("yyyy-MM-dd") + "' \n ";
                }
                if (pCodigoDestinoFinal > 0)
                {
                    if (s.IndexOf("where") > -1)
                        s = s + "and   s.CodigoAterro = " + pCodigoDestinoFinal.ToString() + " \n";
                    else
                        s = s + "where s.CodigoAterro = " + pCodigoDestinoFinal.ToString() + " \n";
                    s = s + "and   (s.CodigoCliente = " + pCodigoCliente.ToString() + " or s.CodigoCliente = 0) \n";
                    s = s + "and   s.TotalPeso > 0 \n";
                    s = s + "and   (select Count(*) from AterroSanitario \n ";
                    s = s + "       where Data          = s.Data \n ";
                    s = s + "       and   CodigoAterro  = " + pCodigoDestinoFinal.ToString() + " \n";
                    s = s + "       and   NumeroTicket  = s.NumeroTicket \n ";
                    s = s + "       and   TotalPeso     = 0 \n ";
                    s = s + "       and   CodigoCliente = " + pCodigoCliente.ToString() + ") > 1 \n ";
                }
            }
            if (pAgrupadoPorMTRe)
            {
                //s = s + "group  by lmtr.NumeroMTRFatima \n"; 
                s = s + "group  by lmtr.NumeroLancamento, lmtr.NumeroMTRFatima \n";
                s = s + "order by cast(lmtr.Ticket as char(10)) asc, Data asc, lmtr.NumeroMTRFatima asc, TotalPeso desc \n";
            }
            else
            {
                s = s + "group by s.NumeroTicket \n";
                s = s + "order by Data asc, cast(lmtr.Ticket as char(10)) asc, lmtr.NumeroMTRFatima asc, TotalPeso desc \n";
            }
            FillDataSet();
            DesconectaBanco();
            return l_dt;
        }

        public string PegaContaineres(string pCodigoCliente, string pNumeroMTRe)
        {
            string sRet = "";
            try
            {
                if (pCodigoCliente != "" && pCodigoCliente != "0" && pNumeroMTRe != "" && pNumeroMTRe != "0" && pNumeroMTRe != "-1")
                {
                    ConectaBanco();
                    s = "";
                    s = s + "select s.NumeroCaixa \n";
                    s = s + "FROM   AterroSanitario      as s \n";
                    s = s + "inner  join Lancamentos     as l    on l.NumeroLancamento = s.NumeroLancamento \n";
                    s = s + "inner  join LancamentoMTR   as lmtr on s.NumeroLancamento = lmtr.NumeroLancamento \n";
                    s = s + "where  (s.Excluido = 0 or s.Excluido is null) \n";
                    s = s + "and    s.CodigoCliente = " + pCodigoCliente + " \n";
                    s = s + "and    lmtr.NumeroMTRFatima = " + pNumeroMTRe + " \n";
                    FillDataSet();
                    if (l_dt.Rows.Count > 0)
                    {
                        foreach (DataRow _dr in l_dt.Rows)
                        {
                            if (_dr["NumeroCaixa"].ToString() != "")
                                sRet = sRet + "-" + _dr["NumeroCaixa"].ToString();
                        }
                        sRet = sRet.Substring(1);
                    }
                }
            }
            catch
            {
                sRet = "";
            }
            finally
            {
                DesconectaBanco();
            }
            return sRet;
        }

        public DataTable PreencheDataTableOrdem(string pOrdem, int pNumeroLancamento, int pCodigoCliente, string pDataInicial, string pDataFinal)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select l.NumeroLancamento, lmtr.NumeroMTR, lmtr.CodigoResiduo, Franquia, Quantidade, ValorUnitario, \n";
                s = s + "       ValorTotal, Deposito, lmtr.Unidade, Ticket, CodigoAterroSanitario, observacao, c.NomeFantasia as NomeCliente, \n";
                s = s + "       NumeroMTRFatima, Motivo, DescargaMTRe, ControleInternoDescarga, DataDescarga, r.DescricaoReduzida as DescricaoResiduo, l.CodigoCliente \n";
                s = s + "from   LancamentoMTR lmtr \n";
                s = s + "inner  join Lancamentos l on l.NumeroLancamento = lmtr.NumeroLancamento \n ";
                s = s + "inner  join Clientes c on c.Codigo = l.CodigoCliente \n ";
                s = s + "inner  join Residuos r on r.Codigo = lmtr.CodigoResiduo \n ";
                if (pNumeroLancamento > 0)
                {
                    s = s + "where  l.NumeroLancamento = " + pNumeroLancamento + " \n ";
                    if (pCodigoCliente > 0)
                        s = s + "and   l.CodigoCliente = " + pCodigoCliente + " \n ";
                }
                else
                {
                    if (pCodigoCliente > 0)
                        s = s + "where l.CodigoCliente = " + pCodigoCliente + " \n ";
                }
                if (pDataInicial != "")
                    s = s + "and    lmtr.DataDescarga >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n ";
                if (pDataFinal != "")
                    s = s + "and    lmtr.DataDescarga <= '" + Convert.ToDateTime(pDataFinal).ToString("yyyy-MM-dd") + "' \n ";
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
        public DataTable PreencheDataTableRGR(int pCodigoCliente, string pAno, string pMes, string sInCodigosResiduos)
        {
            try
            {
                pMes = Convert.ToInt16(pMes).ToString();
                if (pCodigoCliente > 0)
                {
                    ConectaBanco();
                    s = "";
                    s = s + "select CodigoResiduo, DescricaoReduzida, Unidade, Mes, sum(JanQuantidade) as JanQuantidade, sum(FevQuantidade) as FevQuantidade, sum(MarQuantidade) as MarQuantidade,";
                    s = s + " sum(AbrQuantidade) as AbrQuantidade, sum(MaiQuantidade) as MaiQuantidade, sum(JunQuantidade) as JunQuantidade, sum(JulQuantidade) as JulQuantidade,";
                    s = s + " sum(AgoQuantidade) as AgoQuantidade, sum(SetQuantidade) as SetQuantidade, sum(OutQuantidade) as OutQuantidade, sum(NovQuantidade) as NovQuantidade,";
                    s = s + " sum(DezQuantidade) as DezQuantidade, 0.00 as TotalQuantidadeAno, 0.00 as MediaAno from ( \n";
                    if (pMes == "1")
                    {
                        s = s + MesRGR(1, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                    }
                    else if (pMes == "2")
                    {
                        s = s + MesRGR(1, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(2, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                    }
                    else if (pMes == "3")
                    {
                        s = s + MesRGR(1, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(2, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(3, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                    }
                    else if (pMes == "4")
                    {
                        s = s + MesRGR(1, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(2, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(3, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(4, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                    }
                    else if (pMes == "5")
                    {
                        s = s + MesRGR(1, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(2, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(3, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(4, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(5, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                    }
                    else if (pMes == "6")
                    {
                        s = s + MesRGR(1, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(2, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(3, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(4, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(5, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(6, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                    }
                    else if (pMes == "7")
                    {
                        s = s + MesRGR(1, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(2, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(3, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(4, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(5, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(6, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(7, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                    }
                    else if (pMes == "8")
                    {
                        s = s + MesRGR(1, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(2, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(3, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(4, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(5, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(6, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(7, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(8, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                    }
                    else if (pMes == "9")
                    {
                        s = s + MesRGR(1, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(2, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(3, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(4, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(5, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(6, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(7, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(8, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(9, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                    }
                    else if (pMes == "10")
                    {
                        s = s + MesRGR(1, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(2, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(3, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(4, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(5, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(6, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(7, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(8, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(9, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(10, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                    }
                    else if (pMes == "11")
                    {
                        s = s + MesRGR(1, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(2, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(3, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(4, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(5, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(6, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(7, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(8, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(9, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(10, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(11, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                    }
                    else if (pMes == "12")
                    {
                        s = s + MesRGR(1, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(2, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(3, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(4, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(5, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(6, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(7, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(8, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(9, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(10, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(11, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                        s = s + MesRGR(12, pCodigoCliente.ToString(), pAno, sInCodigosResiduos);
                    }
                    s = s + ") x \n";
                    s = s + "group  by DescricaoReduzida \n";
                    l_ds = new DataSet();
                    l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                    FillDataSet();
                    DesconectaBanco();
                }
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
        private string MesRGR(int pMes, string pCodigoCliente, string pAno, string pInCodigosResiduos)
        {
            string x = "";
            if (pMes != 1)
                x = x + "union all \n";
            if (pMes == 1)
                x = x + "    select lmtr.CodigoResiduo, lmtr.Unidade, r.DescricaoReduzida, 'Jan' as Mes, sum(lmtr.quantidade) as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 2)
                x = x + "    select lmtr.CodigoResiduo, lmtr.Unidade, r.DescricaoReduzida, 'Fev' as Mes, 0 as JanQuantidade, sum(lmtr.quantidade) as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 3)
                x = x + "    select lmtr.CodigoResiduo, lmtr.Unidade, r.DescricaoReduzida, 'Mar' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, sum(lmtr.quantidade) as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 4)
                x = x + "    select lmtr.CodigoResiduo, lmtr.Unidade, r.DescricaoReduzida, 'Abr' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, sum(lmtr.quantidade) as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 5)
                x = x + "    select lmtr.CodigoResiduo, lmtr.Unidade, r.DescricaoReduzida, 'Mai' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, sum(lmtr.quantidade) as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 6)
                x = x + "    select lmtr.CodigoResiduo, lmtr.Unidade, r.DescricaoReduzida, 'Jun' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, sum(lmtr.quantidade) as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 7)
                x = x + "    select lmtr.CodigoResiduo, lmtr.Unidade, r.DescricaoReduzida, 'Jul' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, sum(lmtr.quantidade) as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 8)
                x = x + "    select lmtr.CodigoResiduo, lmtr.Unidade, r.DescricaoReduzida, 'Agosto' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, sum(lmtr.quantidade) as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 9)
                x = x + "    select lmtr.CodigoResiduo, lmtr.Unidade, r.DescricaoReduzida, 'Set' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, sum(lmtr.quantidade) as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 10)
                x = x + "    select lmtr.CodigoResiduo, lmtr.Unidade, r.DescricaoReduzida, 'Out' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, sum(lmtr.quantidade) as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 11)
                x = x + "    select lmtr.CodigoResiduo, lmtr.Unidade, r.DescricaoReduzida, 'Nov' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, sum(lmtr.quantidade) as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 12)
                x = x + "    select lmtr.CodigoResiduo, lmtr.Unidade, r.DescricaoReduzida, 'Dez' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, sum(lmtr.quantidade) as DezQuantidade \n";
            x = x + "    from   LancamentoMTR as lmtr \n";
            x = x + "    inner  join Lancamentos l on l.NumeroLancamento = lmtr.NumeroLancamento \n ";
            x = x + "    inner  join Residuos r on r.Codigo = lmtr.CodigoResiduo \n ";
            x = x + "    where  l.CodigoCliente = " + pCodigoCliente + " \n ";
            x = x + "    and    year(l.DataRetirada) = " + pAno + " \n ";
            x = x + "    and    month(l.DataRetirada) = " + pMes + " \n";
            x = x + "    and    upper(lmtr.Unidade) != 'CX' \n";
            x = x + "    and    upper(lmtr.Unidade) != 'M3' \n";
            //x = x + "    and    EhServico = 0 \n ";
            if (pInCodigosResiduos != "")
                x = x + "    and    r.Codigo in (" + pInCodigosResiduos + ") \n ";

            x = x + "    group  by lmtr.CodigoResiduo, lmtr.Unidade, r.DescricaoReduzida, month(l.DataRetirada) \n";

            // essa parte é pra converter CX em Kg ou M3, conforme a capacidade do container que está na tabela de cacambas
            // sum(quantidade = lmtr.quantidade * ca.Capacidade * r.M3PorTon * 1000);            
            x = x + "union all \n";
            if (pMes == 1)
                x = x + "    select lmtr.CodigoResiduo, 'KG' as Unidade, r.DescricaoReduzida, 'Jan' as Mes, sum(lmtr.quantidade * ca.Capacidade * r.M3PorTon * 1000) as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 2)
                x = x + "    select lmtr.CodigoResiduo, 'KG' as Unidade, r.DescricaoReduzida, 'Fev' as Mes, 0 as JanQuantidade, sum(lmtr.quantidade * ca.Capacidade * r.M3PorTon * 1000) as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 3)
                x = x + "    select lmtr.CodigoResiduo, 'KG' as Unidade, r.DescricaoReduzida, 'Mar' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, sum(lmtr.quantidade * ca.Capacidade * r.M3PorTon * 1000) as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 4)
                x = x + "    select lmtr.CodigoResiduo, 'KG' as Unidade, r.DescricaoReduzida, 'Abr' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, sum(lmtr.quantidade * ca.Capacidade * r.M3PorTon * 1000) as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 5)
                x = x + "    select lmtr.CodigoResiduo, 'KG' as Unidade, r.DescricaoReduzida, 'Mai' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, sum(lmtr.quantidade * ca.Capacidade * r.M3PorTon * 1000) as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 6)
                x = x + "    select lmtr.CodigoResiduo, 'KG' as Unidade, r.DescricaoReduzida, 'Jun' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, sum(lmtr.quantidade * ca.Capacidade * r.M3PorTon * 1000) as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 7)
                x = x + "    select lmtr.CodigoResiduo, 'KG' as Unidade, r.DescricaoReduzida, 'Jul' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, sum(lmtr.quantidade * ca.Capacidade * r.M3PorTon * 1000) as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 8)
                x = x + "    select lmtr.CodigoResiduo, 'KG' as Unidade, r.DescricaoReduzida, 'Ago' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, sum(lmtr.quantidade * ca.Capacidade * r.M3PorTon * 1000) as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 9)
                x = x + "    select lmtr.CodigoResiduo, 'KG' as Unidade, r.DescricaoReduzida, 'Set' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, sum(lmtr.quantidade * ca.Capacidade * r.M3PorTon * 1000) as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 10)
                x = x + "    select lmtr.CodigoResiduo, 'KG' as Unidade, r.DescricaoReduzida, 'Out' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, sum(lmtr.quantidade * ca.Capacidade * r.M3PorTon * 1000) as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 11)
                x = x + "    select lmtr.CodigoResiduo, 'KG' as Unidade, r.DescricaoReduzida, 'Nov' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, sum(lmtr.quantidade * ca.Capacidade * r.M3PorTon * 1000) as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 12)
                x = x + "    select lmtr.CodigoResiduo, 'KG' as Unidade, r.DescricaoReduzida, 'Dez' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, sum(lmtr.quantidade * ca.Capacidade * r.M3PorTon * 1000) as DezQuantidade \n";
            x = x + "    from   LancamentoMTR as lmtr \n";
            x = x + "    inner  join Lancamentos l on l.NumeroLancamento = lmtr.NumeroLancamento \n ";
            x = x + "    inner  join Residuos r on r.Codigo = lmtr.CodigoResiduo \n ";
            x = x + "    inner  join Cacambas ca on ca.Numero = l.NumeroCaixa \n ";
            x = x + "    where  l.CodigoCliente = " + pCodigoCliente + " \n ";
            x = x + "    and    year(l.DataRetirada) = " + pAno + " \n ";
            x = x + "    and    month(l.DataRetirada) = " + pMes + " \n";
            x = x + "    and    upper(lmtr.Unidade) = 'CX' \n";
            //x = x + "    and    EhServico = 0 \n ";
            if (pInCodigosResiduos != "")
                x = x + "    and    r.Codigo in (" + pInCodigosResiduos + ") \n ";
            x = x + "    group  by lmtr.CodigoResiduo, lmtr.Unidade, r.DescricaoReduzida, month(l.DataRetirada) \n";

            x = x + "union all \n";
            if (pMes == 1)
                x = x + "    select lmtr.CodigoResiduo, 'KG' as Unidade, r.DescricaoReduzida, 'Jan' as Mes, sum(lmtr.quantidade* r.M3PorTon * 1000) as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 2)
                x = x + "    select lmtr.CodigoResiduo, 'KG' as Unidade, r.DescricaoReduzida, 'Fev' as Mes, 0 as JanQuantidade, sum(lmtr.quantidade* r.M3PorTon * 1000) as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 3)
                x = x + "    select lmtr.CodigoResiduo, 'KG' as Unidade, r.DescricaoReduzida, 'Mar' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, sum(lmtr.quantidade* r.M3PorTon * 1000) as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 4)
                x = x + "    select lmtr.CodigoResiduo, 'KG' as Unidade, r.DescricaoReduzida, 'Abr' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, sum(lmtr.quantidade* r.M3PorTon * 1000) as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 5)
                x = x + "    select lmtr.CodigoResiduo, 'KG' as Unidade, r.DescricaoReduzida, 'Mai' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, sum(lmtr.quantidade* r.M3PorTon * 1000) as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 6)
                x = x + "    select lmtr.CodigoResiduo, 'KG' as Unidade, r.DescricaoReduzida, 'Jun' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, sum(lmtr.quantidade* r.M3PorTon * 1000) as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 7)
                x = x + "    select lmtr.CodigoResiduo, 'KG' as Unidade, r.DescricaoReduzida, 'Jul' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, sum(lmtr.quantidade* r.M3PorTon * 1000) as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 8)
                x = x + "    select lmtr.CodigoResiduo, 'KG' as Unidade, r.DescricaoReduzida, 'Ago' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, sum(lmtr.quantidade* r.M3PorTon * 1000) as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 9)
                x = x + "    select lmtr.CodigoResiduo, 'KG' as Unidade, r.DescricaoReduzida, 'Set' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, sum(lmtr.quantidade* r.M3PorTon * 1000) as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 10)
                x = x + "    select lmtr.CodigoResiduo, 'KG' as Unidade, r.DescricaoReduzida, 'Out' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, sum(lmtr.quantidade* r.M3PorTon * 1000) as OutQuantidade, 0 as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 11)
                x = x + "    select lmtr.CodigoResiduo, 'KG' as Unidade, r.DescricaoReduzida, 'Nov' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, sum(lmtr.quantidade* r.M3PorTon * 1000) as NovQuantidade, 0 as DezQuantidade \n";
            else if (pMes == 12)
                x = x + "    select lmtr.CodigoResiduo, 'KG' as Unidade, r.DescricaoReduzida, 'Dez' as Mes, 0 as JanQuantidade, 0 as FevQuantidade, 0 as MarQuantidade, 0 as AbrQuantidade, 0 as MaiQuantidade, 0 as JunQuantidade, 0 as JulQuantidade, 0 as AgoQuantidade, 0 as SetQuantidade, 0 as OutQuantidade, 0 as NovQuantidade, sum(lmtr.quantidade* r.M3PorTon * 1000) as DezQuantidade \n";
            x = x + "    from   LancamentoMTR as lmtr \n";
            x = x + "    inner  join Lancamentos l on l.NumeroLancamento = lmtr.NumeroLancamento \n ";
            x = x + "    inner  join Residuos r on r.Codigo = lmtr.CodigoResiduo \n ";
            x = x + "    inner  join Cacambas ca on ca.Numero = l.NumeroCaixa \n ";
            x = x + "    where  l.CodigoCliente = " + pCodigoCliente + " \n ";
            x = x + "    and    year(l.DataRetirada) = " + pAno + " \n ";
            x = x + "    and    month(l.DataRetirada) = " + pMes + " \n";
            x = x + "    and    upper(lmtr.Unidade) = 'M3' \n";
            //x = x + "    and    EhServico = 0 \n ";
            if (pInCodigosResiduos != "")
                x = x + "    and    r.Codigo in (" + pInCodigosResiduos + ") \n ";
            x = x + "    group  by lmtr.CodigoResiduo, lmtr.Unidade, r.DescricaoReduzida, month(l.DataRetirada) \n";
            return x;
        }

        private void ParteFromWhere(int pNumeroLancamento, int pCodigoCliente, string pDataInicial, string pDataFinal, int pCodigoDestinoFinal = 0)
        {
            s = s + "from   LancamentoMTR lmtr \n";
            s = s + "inner  join Lancamentos l on l.NumeroLancamento = lmtr.NumeroLancamento \n ";
            s = s + "inner  join Clientes c  on c.Codigo = l.CodigoCliente \n ";
            s = s + "inner  join Residuos r  on r.Codigo = lmtr.CodigoResiduo \n ";
            s = s + "inner  join IBAMA i     on i.Codigo = r.CodigoIBAMA \n ";
            s = s + "inner  join Cacambas ca on ca.Numero = l.NumeroCaixa \n";

            if (pCodigoDestinoFinal == 7)
                s = s + "LEFT   join Aterro a    on a.Codigo = 7 \n";
            else
                s = s + "LEFT   JOIN Aterro a    on r.CodigoDestinoFinal = a.Codigo \n";

            s = s + "LEFT   JOIN CDFe cd     on cd.NumeroMTRe = lmtr.NumeroMTRFatima and cd.CodigoIbama = i.CodigoIBAMA \n";

            if (pNumeroLancamento > 0)
            {
                s = s + "where  l.NumeroLancamento = " + pNumeroLancamento + " \n ";
                if (pCodigoCliente > 0)
                    s = s + "and   l.CodigoCliente = " + pCodigoCliente + " \n ";
                if (pCodigoDestinoFinal > 0)
                    s = s + "and   r.CodigoDestinoFinal = " + pCodigoDestinoFinal + " \n ";
            }
            else
            {
                if (pDataInicial != "")
                    s = s + "where    l.DataRetirada >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n ";
                if (pDataFinal != "")
                    s = s + "and      l.DataRetirada <= '" + Convert.ToDateTime(pDataFinal).ToString("yyyy-MM-dd") + "' \n ";

                s = s + "and      (lmtr.Franquia > 0 or lmtr.Quantidade > 0) \n ";
                s = s + "and      (r.EhServico = 0 or r.EhServico is null) \n ";
                if (pCodigoCliente > 0)
                    s = s + "and l.CodigoCliente = " + pCodigoCliente + " \n ";
                if (pCodigoDestinoFinal > 0)
                    s = s + "and   r.CodigoDestinoFinal = " + pCodigoDestinoFinal + " \n ";
            }
            if (pCodigoDestinoFinal == 7)
            {
                s = s + "and   cast(lmtr.deposito as signed) = " + pCodigoDestinoFinal + " \n";
                s = s + "and   r.Codigo in (160, 50, 121) \n"; // RCD, Resíduo de Concreto e LODO
            }
        }

        public DataTable PreencheDataTableDDROrdem(string pOrdem, int pNumeroLancamento, int pCodigoCliente, string pDataInicial, string pDataFinal, bool pTestar, int pCodigoDestinoFinal = 0)
        {
            try
            {
                ConectaBanco();
                s = "select distinct * from \n";
                s = s + "( \n";
                s = s + "select r.DescricaoReduzida as DescricaoResiduo, r.Classe, i.CodigoIBAMA, lmtr.NumeroMTRFatima, \n";
                if (pCodigoDestinoFinal == 7)
                    s = s + "       l.DataRetirada as DataColeta, sum(lmtr.Franquia) as QtdeColetada, sum(lmtr.Franquia) as QtdeDestinada, lmtr.DataDescarga as DataDestinada, \n";
                else
                    s = s + "       l.DataRetirada as DataColeta, lmtr.Franquia as QtdeColetada, lmtr.Quantidade as QtdeDestinada, lmtr.DataDescarga as DataDestinada, \n";
                s = s + "       lmtr.Unidade, r.TecnologiaAplicada, lmtr.Deposito, '-' as CDFe, l.NumeroLancamento, \n";
                s = s + "       r.CodigoGrupoResiduo, r.Codigo, l.DataColocacao, 0 as CodigoDestinoFinal, ' ' as DescricaoGrupo, \n ";
                s = s + "       ca.Capacidade, lmtr.CodigoResiduo, (select NumeroCDFe from CDFe where NumeroMTRe = lmtr.NumeroMTRFatima limit 1) as NumeroCDFe, \n";
                s = s + "       cd.Quantidade as QtdeCDF, \n";
                s = s + "       lmtr.Quantidade as QtdeDTR, r.M3PorTon, cd.Situacao, lmtr.NumeroMTR \n";
                ParteFromWhere(pNumeroLancamento, pCodigoCliente, pDataInicial, pDataFinal, pCodigoDestinoFinal);

                if (pCodigoDestinoFinal == 0)
                {
                    s = s + "union all \n";
                    if (pTestar)
                        s = s + "select ' ' as DescricaoResiduo, '' as Classe, concat(cd.CodigoIBAMA, 'Z') as CodigoIBAMA, concat(lmtr.NumeroMTRFatima,  'ZZTOTAL') as NumeroMTRFatima, \n";
                    else
                        s = s + "select 'Total Residuo' as DescricaoResiduo, '' as Classe, '' as CodigoIBAMA, 0 as NumeroMTRFatima, \n";
                    s = s + "       str_to_date('0001-01-01', '%d/%m/%Y') as DataColeta, 0 as QtdeColetada, \n";
                    s = s + "       0 as QtdeDestinada, str_to_date('0001-01-01', '%d/%m/%Y') as DataDestinada, \n";
                    s = s + "       '' as Unidade, '' as TecnologiaAplicada, '' as Deposito, '' as CDFe, 0 as NumeroLancamento, \n";
                    s = s + "       r.CodigoGrupoResiduo, 0 as Codigo, str_to_date('0001-01-01', '%d/%m/%Y') as DataColocacao, 0 as CodigoDestinoFinal, ' ' as DescricaoGrupo, \n ";
                    s = s + "       0 as Capacidade, concat(lmtr.CodigoResiduo,  ' ZZTOTAL ') as CodigoResiduo, 0 as NumeroCDFe, \n";
                    s = s + "       (select sum(Quantidade) from CDFe where NumeroMTRe = lmtr.NumeroMTRFatima) as QtdeCDF, 0 as QtdeDTR, 0 as M3PorTon, '' as Situacao, 0 as NumeroMTR \n";
                    ParteFromWhere(pNumeroLancamento, pCodigoCliente, pDataInicial, pDataFinal);
                }
                if (pTestar)
                    s = s + "group by lmtr.NumeroMTRFatima \n";
                else
                {
                    if (pCodigoDestinoFinal != 7)
                        s = s + "group by r.DescricaoReduzida + 'T ', r.Codigo \n";
                    else if (pCodigoDestinoFinal == 7)
                        s = s + "group by r.DescricaoReduzida \n";
                }
                s = s + ") x \n";

                if (pOrdem == "Descrição")
                    pOrdem = "Descricao";

                if (pTestar)
                    s = s + "order by NumeroMTRFatima, CodigoIBAMA, NumeroMTR \n";
                else
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
        public DataTable PreencheDTRelatorioDestinoFinalConferencia(string pOrdem, int pNumeroLancamento, int pCodigoCliente, string pDataInicial, string pDataFinal, bool pTestar, int pCodigoDestinoFinal = 0)
        {
            try
            {
                ConectaBanco();
                s = "select distinct * from \n";
                s = s + "( \n";
                s = s + "select r.DescricaoReduzida as DescricaoResiduo, r.Classe, i.CodigoIBAMA, lmtr.NumeroMTRFatima, \n";
                s = s + "       l.CodigoCliente, c.Nome, ";
                s = s + "       l.DataRetirada as DataColeta, lmtr.Franquia as QtdeColetada, lmtr.Quantidade as QtdeDestinada, lmtr.DataDescarga as DataDestinada, \n";
                s = s + "       lmtr.Unidade, r.TecnologiaAplicada, lmtr.Deposito, '-' as CDFe, l.NumeroLancamento, \n";
                s = s + "       r.CodigoGrupoResiduo, r.Codigo, l.DataColocacao, 0 as CodigoDestinoFinal, ' ' as DescricaoGrupo, \n ";
                s = s + "       ca.Capacidade, lmtr.CodigoResiduo, cd.NumeroCDFe, \n";
                s = s + "       cd.Quantidade as QtdeCDF, \n";
                s = s + "       lmtr.Quantidade as QtdeDTR, r.M3PorTon, cd.Situacao \n";
                ParteFromWhere(pNumeroLancamento, pCodigoCliente, pDataInicial, pDataFinal, pCodigoDestinoFinal);

                s = s + "union all \n";
                if (pTestar)
                    s = s + "select ' ' as DescricaoResiduo, '' as Classe, concat(cd.CodigoIBAMA, 'Z') as CodigoIBAMA, concat(lmtr.NumeroMTRFatima,  'ZZTOTAL') as NumeroMTRFatima, \n";
                else
                    s = s + "select 'Total Residuo' as DescricaoResiduo, '' as Classe, '' as CodigoIBAMA, 0 as NumeroMTRFatima, \n";
                s = s + "       l.CodigoCliente, c.Nome, ";
                s = s + "       str_to_date('0001-01-01', '%d/%m/%Y') as DataColeta, 0 as QtdeColetada, \n";
                s = s + "       0 as QtdeDestinada, str_to_date('0001-01-01', '%d/%m/%Y') as DataDestinada, \n";
                s = s + "       '' as Unidade, '' as TecnologiaAplicada, '' as Deposito, '' as CDFe, 0 as NumeroLancamento, \n";
                s = s + "       r.CodigoGrupoResiduo, 0 as Codigo, str_to_date('0001-01-01', '%d/%m/%Y') as DataColocacao, 0 as CodigoDestinoFinal, ' ' as DescricaoGrupo, \n ";
                s = s + "       0 as Capacidade, concat(lmtr.CodigoResiduo,  ' ZZTOTAL ') as CodigoResiduo, 0 as NumeroCDFe, \n";
                s = s + "       (select sum(Quantidade) from CDFe where NumeroMTRe = lmtr.NumeroMTRFatima) as QtdeCDF, 0 as QtdeDTR, 0 as M3PorTon, '' as Situacao \n";
                ParteFromWhere(pNumeroLancamento, pCodigoCliente, pDataInicial, pDataFinal);
                if (pTestar)
                    s = s + "group by lmtr.NumeroMTRFatima \n";
                else
                    s = s + "group by r.DescricaoReduzida + 'T ', r.Codigo \n";

                s = s + ") x \n";

                if (pOrdem == "Descrição")
                    pOrdem = "Descricao";

                if (pTestar)
                    s = s + "order by Nome, NumeroMTRFatima, CodigoIBAMA \n";
                else
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
                s = s + "from LancamentoMTR ";
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
        public string DadoExiste(int pNumeroLancamento, int pNumeroMTR, int pCodigoResiduo)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select NumeroLancamento \n";
                s = s + "from   LancamentoMTR \n";
                if (pNumeroLancamento > 0 && pNumeroMTR > 0)
                {
                    s = s + "where  NumeroLancamento = " + pNumeroLancamento + " \n";
                    s = s + "and    NumeroMTR = " + pNumeroMTR + " \n";
                    s = s + "and    CodigoResiduo = " + pCodigoResiduo + " \n";
                }
                else
                    s = s + "where  NumeroLancamento = " + pNumeroLancamento + " \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pNumeroLancamento > 0)
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
        public bool MTReExiste(Int64 pNumeroMTRe, string pCodigoIBAMA, DateTime pDataDescarga)
        {
            bool bRet = false;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select lmtr.NumeroLancamento \n";
                s = s + "from   LancamentoMTR lmtr \n";
                s = s + "inner  join Residuos r on r.Codigo = lmtr.CodigoResiduo \n";
                s = s + "inner  join IBAMA i on i.Codigo = r.CodigoIBAMA \n";
                s = s + "where  NumeroMTRFatima = " + pNumeroMTRe + " \n";
                s = s + "and    i.CodigoIBAMA = '" + pCodigoIBAMA.Substring(0, 2) + " " + pCodigoIBAMA.Substring(2, 2) + " " + pCodigoIBAMA.Substring(4, 2) + "' \n";
                s = s + "and    year(lmtr.DataDescarga) = " + pDataDescarga.Year + " \n";
                s = s + "and    month(lmtr.DataDescarga) = " + pDataDescarga.Month + " \n";
                s = s + "and    day(lmtr.DataDescarga) = " + pDataDescarga.Day + " \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    bRet = true;
                else
                    bRet = false;
            }
            catch (Exception ex)
            {
                bRet = false;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return bRet;
        }

        public int PegaNumeroLancamentoPelaMTRe(Int64 pNumeroMTRe)
        {
            int nRet = 0;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select lmtr.NumeroLancamento \n";
                s = s + "from   LancamentoMTR lmtr \n";
                s = s + "where  lmtr.NumeroMTRFatima = " + pNumeroMTRe + " \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                {
                    if (l_dt.Rows[0][0] != null && l_dt.Rows[0][0].ToString() != "")
                        nRet = Convert.ToInt32(l_dt.Rows[0][0]);
                }
            }
            catch (Exception ex)
            {
                nRet = 0;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return nRet;
        }
        public Int64 PegaNumeroMTRe(Int64 pNumeroLancamento, int pCodigoResiduo)
        {
            Int64 nRet = 0;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select lmtr.NumeroMTRFatima as NumeroMTRe \n";
                s = s + "from   LancamentoMTR lmtr \n";
                s = s + "where  lmtr.NumeroLancamento = " + pNumeroLancamento + " \n";
                s = s + "and    lmtr.CodigoResiduo = " + pCodigoResiduo + " \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                {
                    if (l_dt.Rows[0][0] != null && l_dt.Rows[0][0].ToString() != "")
                        nRet = Convert.ToInt64(l_dt.Rows[0][0]);
                }
            }
            catch (Exception ex)
            {
                nRet = 0;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return nRet;
        }
        public int PegaCodigoDestinoFinal(string pNumeroLancamento, string pCodigoResiduo)
        {
            int nRet = 0;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Convert(lmtr.Deposito, unsigned) as CodigoDestinoFinal\n";
                s = s + "from   LancamentoMTR lmtr \n";
                s = s + "where  lmtr.NumeroLancamento = " + pNumeroLancamento + " \n";
                s = s + "and    lmtr.CodigoResiduo = " + pCodigoResiduo + " \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                {
                    if (l_dt.Rows[0][0] != null && l_dt.Rows[0][0].ToString() != "")
                        nRet = Convert.ToInt32(l_dt.Rows[0][0]);
                }
            }
            catch (Exception ex)
            {
                nRet = 0;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return nRet;
        }

        public int PegaNumeroLancamentoPelaMTR(Int64 pNumeroMTR)
        {
            int nRet = 0;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select lmtr.NumeroLancamento \n";
                s = s + "from   LancamentoMTR lmtr \n";
                s = s + "where  lmtr.NumeroMTR = " + pNumeroMTR + " \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                {
                    if (l_dt.Rows[0][0] != null && l_dt.Rows[0][0].ToString() != "")
                        nRet = Convert.ToInt32(l_dt.Rows[0][0]);
                }
            }
            catch (Exception ex)
            {
                nRet = 0;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return nRet;
        }
        public bool MTReExisteEmClienteDiferente(Int64 pNumeroMTRe, string pCNPJCPF)
        {
            bool bRet = false;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select lmtr.NumeroLancamento, c.CNPJ_CPF \n";
                s = s + "from   LancamentoMTR lmtr \n";
                s = s + "inner  join Lancamentos l on l.NumeroLancamento = lmtr.NumeroLancamento \n";
                s = s + "inner  join Clientes c on c.Codigo = l.CodigoCliente \n";
                s = s + "where  lmtr.NumeroMTRFatima = " + pNumeroMTRe + " \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                {
                    if (geral.RetiraLetras(l_dt.Rows[0]["CNPJ_CPF"].ToString()) != pCNPJCPF)
                        bRet = true;
                    else
                        bRet = false;
                }
                else
                    bRet = false;
            }
            catch
            {
                bRet = false;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return bRet;
        }
        public bool MTReExisteEmClienteDiferente(string pNumeroMTRe, int pCodigoCliente)
        {
            bool bRet = false;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select lmtr.NumeroLancamento, l.CodigoCliente \n";
                s = s + "from   LancamentoMTR lmtr \n";
                s = s + "inner  join Lancamentos l on l.NumeroLancamento = lmtr.NumeroLancamento \n";
                s = s + "where  lmtr.NumeroMTRFatima = " + pNumeroMTRe + " \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                {
                    if (geral.RetiraLetras(l_dt.Rows[0]["CodigoCliente"].ToString()) != pCodigoCliente.ToString())
                        bRet = true;
                    else
                        bRet = false;
                }
                else
                    bRet = false;
            }
            catch
            {
                bRet = false;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return bRet;
        }
        public string MTReNaoExisteOuCNPJ_Diferente(Int64 pNumeroMTRe, string pCNPJCPF)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select lmtr.NumeroLancamento, c.CNPJ_CPF \n";
                s = s + "from   LancamentoMTR lmtr \n";
                s = s + "inner  join Lancamentos l on l.NumeroLancamento = lmtr.NumeroLancamento \n";
                s = s + "inner  join Clientes c on c.Codigo = l.CodigoCliente \n";
                s = s + "where  lmtr.NumeroMTRFatima = " + pNumeroMTRe + " \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                {
                    if (geral.RetiraLetras(l_dt.Rows[0]["CNPJ_CPF"].ToString()) != pCNPJCPF)
                        sRet = "Existe em cliente diferente - CNPJ SILC: " + l_dt.Rows[0]["CNPJ_CPF"].ToString();
                    else
                        sRet = "Ok";
                }
                else
                    sRet = "Número MTR-e inexistente no SILC";
            }
            catch (Exception ex)
            {
                sRet = ex.Message;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }

        public void Inserir(clsLancamentoMTR pLancamentoMTR)
        {
            try
            {
                s = "";
                s = s + "insert into LancamentoMTR \n";
                s = s + "( \n";
                s = s + "  NumeroLancamento, NumeroMTR, CodigoResiduo, Franquia, Quantidade, ValorUnitario, \n";
                s = s + "  ValorTotal, Deposito, Unidade, Ticket, CodigoAterroSanitario, observacao, \n";
                s = s + "  NumeroMTRFatima, Motivo, DataDescarga, DescargaMTRe, ControleInternoDescarga \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pLancamentoMTR.NumeroLancamento.ToString() != "")
                    s = s + " " + pLancamentoMTR.NumeroLancamento.ToString() + ", \n";
                else
                    s = s + "0, \n";
                if (pLancamentoMTR.NumeroMTR.ToString() != "")
                    s = s + " " + pLancamentoMTR.NumeroMTR.ToString() + ", \n";
                else
                    s = s + "0, \n";
                if (pLancamentoMTR.CodigoResiduo.ToString() != "")
                    s = s + " " + pLancamentoMTR.CodigoResiduo.ToString() + ", \n";
                else
                    s = s + "0, \n";
                if (pLancamentoMTR.Franquia.ToString() != "")
                    s = s + " " + pLancamentoMTR.Franquia.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pLancamentoMTR.Quantidade.ToString() != "")
                    s = s + " " + pLancamentoMTR.Quantidade.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pLancamentoMTR.ValorUnitario.ToString() != "")
                    s = s + " " + pLancamentoMTR.ValorUnitario.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pLancamentoMTR.ValorTotal.ToString() != "")
                    s = s + " " + pLancamentoMTR.ValorTotal.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                s = s + " '" + geral.Left(pLancamentoMTR.Deposito, 20) + "', \n";
                s = s + " '" + pLancamentoMTR.Unidade + "', \n";
                s = s + " '" + pLancamentoMTR.Ticket + "', \n";
                if (pLancamentoMTR.CodigoAterroSanitario.ToString() != "")
                    s = s + " " + pLancamentoMTR.CodigoAterroSanitario.ToString() + ", \n";
                else
                    s = s + "0, \n";
                s = s + " '" + pLancamentoMTR.observacao.Replace("'", "") + "', \n";
                if (pLancamentoMTR.NumeroMTRFatima.ToString() != "")
                    s = s + " " + pLancamentoMTR.NumeroMTRFatima.ToString() + ", \n";
                else
                    s = s + "0, \n";
                s = s + " '" + pLancamentoMTR.Motivo.Replace("'", "") + "', \n";
                if (pLancamentoMTR.DataDescarga != "")
                    s = s + "'" + Convert.ToDateTime(pLancamentoMTR.DataDescarga).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                s = s + "'" + pLancamentoMTR.DescargaMTRe + "', \n";
                s = s + "'" + pLancamentoMTR.ControleInternoDescarga + "' \n";

                s = s + ")";
                ConectaBanco();
                MySqlCommand command = oDB.MySqlConnect.CreateCommand();
                command.CommandTimeout = 360;
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
        public string Alterar(clsLancamentoMTR pLancamentoMTR, int pNumeroLancamento, int pNumeroMTR, int pCodigoResiduo)
        {
            string sRet = "";
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update LancamentoMTR \n";

                if (pLancamentoMTR.NumeroLancamento.ToString() != "")
                    s = s + " set NumeroLancamento = " + pLancamentoMTR.NumeroLancamento.ToString() + ", \n";
                else
                    s = s + " set NumeroLancamento = 0, \n";
                if (pLancamentoMTR.NumeroMTR.ToString() != "")
                    s = s + " NumeroMTR = " + pLancamentoMTR.NumeroMTR.ToString() + ", \n";
                else
                    s = s + " NumeroMTR = 0, \n";
                if (pLancamentoMTR.CodigoResiduo.ToString() != "")
                    s = s + " CodigoResiduo = " + pLancamentoMTR.CodigoResiduo.ToString() + ", \n";
                else
                    s = s + " CodigoResiduo = 0, \n";
                if (pLancamentoMTR.Franquia.ToString() != "")
                    s = s + " Franquia = " + pLancamentoMTR.Franquia.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " Franquia = 0, \n";
                if (pLancamentoMTR.Quantidade.ToString() != "")
                    s = s + " Quantidade = " + pLancamentoMTR.Quantidade.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " Quantidade = 0, \n";
                if (pLancamentoMTR.ValorUnitario.ToString() != "")
                    s = s + " ValorUnitario = " + pLancamentoMTR.ValorUnitario.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " ValorUnitario = 0, \n";
                if (pLancamentoMTR.ValorTotal.ToString() != "")
                    s = s + " ValorTotal = " + pLancamentoMTR.ValorTotal.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " ValorTotal = 0, \n";
                s = s + " Deposito = '" + geral.Left(pLancamentoMTR.Deposito, 20) + "', \n";
                s = s + " Unidade = '" + pLancamentoMTR.Unidade + "', \n";
                s = s + " Ticket = '" + pLancamentoMTR.Ticket + "', \n";
                if (pLancamentoMTR.CodigoAterroSanitario.ToString() != "")
                    s = s + " CodigoAterroSanitario = " + pLancamentoMTR.CodigoAterroSanitario.ToString() + ", \n";
                else
                    s = s + " CodigoAterroSanitario = 0, \n";
                s = s + " observacao = '" + pLancamentoMTR.observacao + "', \n";
                if (pLancamentoMTR.NumeroMTRFatima.ToString() != "")
                    s = s + " NumeroMTRFatima = " + pLancamentoMTR.NumeroMTRFatima.ToString() + ", \n";
                else
                    s = s + " NumeroMTRFatima = 0, \n";
                s = s + " Motivo = '" + pLancamentoMTR.Motivo + "', \n";
                if (pLancamentoMTR.DataDescarga != "")
                    s = s + " DataDescarga = '" + Convert.ToDateTime(pLancamentoMTR.DataDescarga).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + " DataDescarga = '0001-01-01', \n";
                s = s + " DescargaMTRe = '" + pLancamentoMTR.DescargaMTRe + "', \n";
                s = s + " ControleInternoDescarga = '" + pLancamentoMTR.ControleInternoDescarga + "' \n";

                s = s + "where  NumeroLancamento = " + pNumeroLancamento + " \n";
                s = s + "and    NumeroMTR = " + pNumeroMTR + " \n";
                s = s + "and    CodigoResiduo = " + pCodigoResiduo + " \n";

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

        public string AlterarQuantidadeDescarrega(int pNumeroLancamento, int pNumeroMTR, int pCodigoResiduo, decimal pQuantidade, string pDataDescarga)
        {
            string sRet = "";
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update LancamentoMTR \n";
                if (pQuantidade.ToString() != "")
                    s = s + "set Quantidade = " + pQuantidade.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "set Quantidade = 0, \n";
                s = s + "    DataDescarga = '" + Convert.ToDateTime(pDataDescarga).ToString("yyyy-MM-dd") + "' \n";
                s = s + "where  NumeroLancamento = " + pNumeroLancamento + " \n";
                s = s + "and    NumeroMTR        = " + pNumeroMTR + " \n";
                s = s + "and    CodigoResiduo    = " + pCodigoResiduo + " \n";
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

        public string SalvarDestinoDataDescarga(int pNumeroLancamento, int pNumeroMTR, int pCodigoResiduo, string pDataDescarga, int pCodigoDestino, string pNomeDestino)
        {
            string sRet = "";
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update LancamentoMTR \n";
                s = s + "set    Deposito     = '" + geral.Left(pCodigoDestino.ToString() + "-" + pNomeDestino, 15) + "', \n";
                s = s + "       DataDescarga = '" + Convert.ToDateTime(pDataDescarga).ToString("yyyy-MM-dd") + "' \n";
                s = s + "where  NumeroLancamento = " + pNumeroLancamento + " \n";
                s = s + "and    NumeroMTR        = " + pNumeroMTR + " \n";
                s = s + "and    CodigoResiduo    = " + pCodigoResiduo + " \n";
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
        public string Excluir(int pNumeroLancamento, string pDescricao)
        {
            string sRet = "";
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "delete from LancamentoMTR ";
                if (pNumeroLancamento > 0)
                {
                    s = s + "where  NumeroLancamento = " + pNumeroLancamento.ToString();
                }
                else if (pDescricao != "" && pNumeroLancamento == 0)
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
        public string Excluir(int pNumeroLancamento, int pNumeroMTR, int pCodigoResiduo)
        {
            string sRet = "";
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                if (pNumeroLancamento > 0)
                {
                    s = s + "delete from LancamentoMTR ";
                    s = s + "where  NumeroLancamento = " + pNumeroLancamento.ToString() + " \n";
                    s = s + "and    NumeroMTR        = " + pNumeroMTR.ToString() + " \n";
                    s = s + "and    CodigoResiduo    = " + pCodigoResiduo.ToString() + " \n";
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
        public string ExcluirMesAno(int pCodigoCliente, int pMes, int pAno)
        {
            string sRet = "";
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                if (pCodigoCliente > 0)
                {
                    s = s + "DELETE FROM LancamentoMTR \n";
                    s = s + "WHERE EXISTS(SELECT * FROM Lancamentos l \n";
                    s = s + "             WHERE Month(l.DataRetirada) = " + pMes.ToString() + " \n";
                    s = s + "             AND   Year(l.DataRetirada)  = " + pAno.ToString() + " \n";
                    s = s + "             AND   l.CodigoCliente = " + pCodigoCliente.ToString() + " \n";
                    s = s + "             AND   NumeroLancamento = LancamentoMTR.NumeroLancamento) \n";
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
                s = s + "select NumeroLancamento, NumeroMTR, CodigoResiduo, Franquia, Quantidade, ValorUnitario, \n";
                s = s + "       ValorTotal, Deposito, Unidade, Ticket, CodigoAterroSanitario, observacao, \n";
                s = s + "       NumeroMTRFatima, Motivo, DataDescarga, DescargaMTRe, ControleInternoDescarga \n";
                s = s + "from   LancamentoMTR \n";
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
        public DataTable PreencheDT_Replicar(string pOrdem, string pNumeroLancamento, string pCodigoResiduo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select l.NumeroCaixa, l.CodigoCliente, l.CodigoCaminhaoColoca, l.CodigoMotoristaColocou, \n";
                s = s + "       l.DataColocacao, l.DataRetirada, l.CodigoMotoristaRetirou, l.CodigoCaminhoRetirada, \n";
                s = s + "       lmtr.NumeroLancamento, '' as NumeroMTR, lmtr.CodigoResiduo, ";
                s = s + "       lmtr.Franquia, lmtr.observacao, lmtr.CodigoAterroSanitario, \n ";
                s = s + "       lmtr.Quantidade, lmtr.ValorUnitario, \n";
                s = s + "       lmtr.ValorTotal, lmtr.Deposito, lmtr.Unidade, lmtr.Ticket, lmtr.observacao, \n";
                s = s + "       lmtr.NumeroMTRFatima, lmtr.Motivo, lmtr.DescargaMTRe, lmtr.ControleInternoDescarga, lmtr.DataDescarga, l.CodigoCliente, c.NomeFantasia, \n";
                s = s + "       r.DescricaoReduzida as Residuo \n";
                s = s + "from   LancamentoMTR lmtr \n";
                s = s + "inner  join Lancamentos l on lmtr.NumeroLancamento = l.NumeroLancamento \n";
                s = s + "inner  join Clientes c on c.Codigo = l.CodigoCliente \n";
                s = s + "inner  join Residuos r on r.Codigo = lmtr.CodigoResiduo \n";
                s = s + "where  lmtr.NumeroLancamento = " + pNumeroLancamento + " \n";
                if (pCodigoResiduo != "" && pCodigoResiduo != null)
                    s = s + "and    lmtr.CodigoResiduo = " + pCodigoResiduo + " \n";
                s = s + "limit 1 \n";
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
        public DataTable PreencheDataTableServicosRealizados(string pOrdem, int pCodigoCliente, string pDataInicial, string pDataFinal, bool pLocadosRetirados)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select l.NumeroLancamento, c.NomeFantasia as NomeCliente, l.CodigoCliente, l.NumeroCaixa, l.DataColocacao, l.DataRetirada, \n";
                s = s + "       lmtr.NumeroMTR, r.DescricaoReduzida as DescricaoResiduo, lmtr.Quantidade, lmtr.Unidade, lmtr.Deposito as 'Destino Final', \n";
                s = s + "       lmtr.Observacao, lmtr.NumeroMTRFatima as 'Numero MTRe' \n";
                if (pLocadosRetirados)
                {
                    s = s + ", (select Modelo from Caminhoes where Codigo = l.CodigoCaminhaoColoca)  as Caminhao \n";
                }
                else
                {
                    s = s + ", (select Modelo from Caminhoes where Codigo = l.CodigoCaminhoRetirada) as Caminhao \n";
                }
                s = s + "from   Lancamentos l \n";
                s = s + "left   join LancamentoMTR lmtr on lmtr.NumeroLancamento = l.NumeroLancamento \n";
                s = s + "left   join Clientes c on c.Codigo = l.CodigoCliente \n ";
                s = s + "left   join Residuos r on r.Codigo = lmtr.CodigoResiduo \n ";
                if (pCodigoCliente > 0)
                    s = s + "where  CodigoCliente = " + pCodigoCliente + " \n";
                if (pLocadosRetirados)
                {
                    s = s + "and   (l.DataRetirada is null or l.DataRetirada = '0100-01-01' or l.DataRetirada = '0001-01-01') \n ";
                }
                else
                {
                    if (pDataInicial != "")
                    {
                        s = s + "and    l.DataRetirada >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n ";
                    }
                    if (pDataFinal != "")
                    {
                        s = s + "and    l.DataRetirada <= '" + Convert.ToDateTime(pDataFinal).ToString("yyyy-MM-dd") + "' \n ";
                    }
                    s = s + "and   (not l.DataRetirada is null or l.DataRetirada != '0100-01-01' or l.DataRetirada != '0001-01-01') \n ";
                }
                s = s + "order by " + pOrdem + " \n ";

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
        public DataTable PreencheDataTablePeriodo(string pOrdem, string pDataInicial, string pDataFinal)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select distinct l.CodigoCliente, c.CNPJ_CPF as CNPJ_CPF_Cliente, c.NomeFantasia as RazaoSocialCliente, rmtre.Data, \n";
                s = s + "       lmtr.NumeroMTRFatima as 'NumeroMTRe', rmtre.Obs, lmtr.CodigoResiduo, r.DescricaoReduzida \n";
                s = s + "from   Lancamentos l \n";
                s = s + "left   join LancamentoMTR lmtr on lmtr.NumeroLancamento = l.NumeroLancamento \n";
                s = s + "left   join Residuos r on r.Codigo = lmtr.CodigoResiduo \n";
                s = s + "left   join Clientes c on c.Codigo = l.CodigoCliente \n ";
                s = s + "left   join MTReConferenciaDiaria rmtre  on rmtre.NumeroMTRe = lmtr.NumeroMTRFatima \n";
                s = s + "where  l.DataRetirada >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n ";
                if (pDataFinal != "")
                    s = s + "and    l.DataRetirada <= '" + Convert.ToDateTime(pDataFinal).ToString("yyyy-MM-dd") + "' \n ";
                s = s + "order by " + pOrdem + " \n ";

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
        public string RetornaNumeroMTRe(int pNumeroLancamento, int pCodigoResiduo, int pNumeroMTR = 0)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                if (pNumeroLancamento > 0)
                {
                    s = s + "select NumeroMTRFatima \n";
                    s = s + "from   LancamentoMTR \n";
                    s = s + "where  NumeroLancamento = " + pNumeroLancamento + " \n";
                    s = s + "and    CodigoResiduo = " + pCodigoResiduo + " \n";
                    if (pNumeroMTR > 0)
                        s = s + "and    NumeroMTR = " + pNumeroMTR + " \n";
                    FillDataSet();
                    if (l_dt.Rows.Count > 0)
                        sRet = l_dt.Rows[0][0].ToString();
                }
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
        public DataTable PegaDadosRelatorioControleAterro(clsLancamentoMTR pLancamentoMTR, string pDataInicial = "", string pDataFinal = "", string pCodigoCliente = "",
                                                          string pCodigoAterro = "")
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select * from ( \n";
                s = s + "select 0 as NumeroLancamento, 0 as NumeroMTR, 0 as CodigoResiduo, a.Codigo, a.Data, a.Hora, at.LocalAterro, a.NumeroTicket, SUBSTRING_INDEX(m.Nome, '-', 2) as NomeMotorista, \n";
                s = s + "       ca.Modelo, a.NumeroCaixa, a.TotalPeso, ca.Placas, \n";
                s = s + "       '' as Nome, a.TotalPeso as PesoIndividual, 0 as CodigoCliente, 0 as TotalGrupo, '' as NomeFantasia \n";
                s = s + "from   AterroSanitario a \n";
                s = s + "left   join Funcionarios m on m.Codigo = a.CodigoMotorista \n";
                s = s + "left   join Caminhoes ca on ca.Codigo = a.CodigoCaminhao \n";
                s = s + "left   join Aterro at on at.codigo = a.CodigoAterro \n";
                if (pCodigoAterro != "")
                    s = s + "where  a.CodigoAterro = " + pCodigoAterro + " \n ";
                else
                    s = s + "where  (a.CodigoAterro = 2 or a.CodigoAterro = 13) \n ";
                s = s + "and    a.CodigoResiduo = 999 and (excluido = 0 or excluido is null) \n";
                if (pDataInicial.Length > 0 && pDataFinal.Length > 0)
                {
                    s = s + "and  a.Data >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n";
                    s = s + "and  a.Data <= '" + Convert.ToDateTime(pDataFinal).ToString("yyyy-MM-dd") + "' \n";
                }
                if (pCodigoCliente != "")
                    s = s + "and  a.CodigoCliente = " + pCodigoCliente + " \n";
                s = s + "union all \n";
                s = s + "select lmtr.NumeroLancamento, lmtr.NumeroMTR, lmtr.CodigoResiduo, 0 as Codigo, lmtr.DataDescarga as Data, '00:00:00' as Hora, lmtr.Deposito as LocalAterro, lmtr.Ticket, SUBSTRING_INDEX(m.Nome, '-', 2) as NomeMotorista, \n";
                s = s + "       ca.Modelo, l.NumeroCaixa , lmtr.Quantidade as TotalPeso, ca.Placas, c.Nome, lmtr.Quantidade as PesoIndividual, l.CodigoCliente, 0 as TotalGrupo, c.NomeFantasia \n";
                s = s + "from   LancamentoMTR lmtr \n";
                s = s + "inner  join Lancamentos l on l.NumeroLancamento = lmtr.NumeroLancamento \n";
                s = s + "left   join Funcionarios m on m.Codigo = l.CodigoMotoristaRetirou \n";
                s = s + "left   join Caminhoes ca on ca.Codigo  = l.CodigoCaminhoRetirada \n";
                s = s + "left   join Clientes c on c.Codigo = l.CodigoCliente \n";
                s = s + "left   join Aterro at on at.codigo = lmtr.CodigoAterroSanitario \n";
                if (pCodigoAterro == "2")
                    s = s + "where  (substring(lmtr.Deposito,1,2) = '2-' or substring(lmtr.Deposito,1,2) = '02') \n ";
                else if (pCodigoAterro == "13")
                    s = s + "where  (substring(lmtr.Deposito,1,2) = '13') \n ";
                else
                    s = s + "where  (substring(lmtr.Deposito,1,2) = '2-' or substring(lmtr.Deposito,1,2) = '02' or substring(lmtr.Deposito,1,2) = '13') \n ";
                // feito na sexta feira 14/07/2023
                if (pCodigoCliente == "")
                {
                    s = s + "and    not exists(select *  \n";
                    s = s + "                  from   AterroSanitario a \n";
                    s = s + "                  inner  join Funcionarios m on m.Codigo = a.CodigoMotorista \n";
                    s = s + "                  inner  join Caminhoes ca on ca.Codigo = a.CodigoCaminhao \n";
                    s = s + "                  where  (a.CodigoAterro = 2 or a.CodigoAterro = 13) \n ";
                    s = s + "                  and    a.CodigoResiduo = 999 and (excluido = 0 or excluido is null) \n";
                    s = s + "                  and    a.NumeroTicket = lmtr.Ticket \n";
                    if (pDataInicial.Length > 0 && pDataFinal.Length > 0)
                    {
                        s = s + "                  and   a.Data >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n";
                        s = s + "                  and   a.Data <= '" + Convert.ToDateTime(pDataFinal).ToString("yyyy-MM-dd") + "' \n";
                    }
                    s = s + "                 ) \n";
                } // se preciso, tirar só colchetes
                if (pDataInicial.Length > 0 && pDataFinal.Length > 0)
                {
                    s = s + "and  lmtr.DataDescarga >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n";
                    s = s + "and  lmtr.DataDescarga <= '" + Convert.ToDateTime(pDataFinal).ToString("yyyy-MM-dd") + "' \n";
                }
                if (pCodigoCliente != "")
                    s = s + "and  l.CodigoCliente = " + pCodigoCliente + " \n";
                s = s + ") x \n";
                s = s + "order by Data, NumeroTicket \n";
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

        public string SalvarNumeroMTRe(int pNumeroLancamento, int pCodigoResiduo, string pNumeroMTRe)
        {
            string sRet = "";
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update LancamentoMTR \n";
                s = s + "set    NumeroMTRFatima  = '" + pNumeroMTRe.ToString() + "' \n";
                s = s + "where  NumeroLancamento = " + pNumeroLancamento + " \n";
                s = s + "and    CodigoResiduo    = " + pCodigoResiduo + " \n";
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