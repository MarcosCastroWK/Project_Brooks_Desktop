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
    public class clsAterroSanitarioDados
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
        public clsAterroSanitario PegaDados(clsAterroSanitario pAterroSanitario, int pCodigo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Data, CodigoAterro, LocalAterro, NumeroTicket, CodigoMotorista, \n";
                s = s + "       CodigoCaminhao, NumeroLancamento, NumeroMTR, NumeroCaixa, TotalPeso, \n";
                s = s + "       CodigoCliente, Hora, ContainerDescricao, Excluido, Status, CodigoResiduo \n";
                s = s + "from   AterroSanitario \n";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " \n";
                    s = s + "limit 1";
                }
                else if (pCodigo == 0)
                {
                    s = s + " order by Codigo limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows[0]["Codigo"].ToString() != "")
                        pAterroSanitario.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                    if (l_dt.Rows[0]["Data"].ToString() != "")
                        pAterroSanitario.Data = Convert.ToDateTime(l_dt.Rows[0]["Data"].ToString()).Date.ToShortDateString();
                    if (l_dt.Rows[0]["CodigoAterro"].ToString() != "")
                        pAterroSanitario.CodigoAterro = Convert.ToInt32(l_dt.Rows[0]["CodigoAterro"]);
                    pAterroSanitario.LocalAterro = l_dt.Rows[0]["LocalAterro"].ToString();
                    pAterroSanitario.NumeroTicket = l_dt.Rows[0]["NumeroTicket"].ToString();
                    if (l_dt.Rows[0]["CodigoMotorista"].ToString() != "")
                        pAterroSanitario.CodigoMotorista = Convert.ToInt32(l_dt.Rows[0]["CodigoMotorista"]);
                    if (l_dt.Rows[0]["CodigoCaminhao"].ToString() != "")
                        pAterroSanitario.CodigoCaminhao = Convert.ToInt32(l_dt.Rows[0]["CodigoCaminhao"]);
                    if (l_dt.Rows[0]["NumeroLancamento"].ToString() != "")
                        pAterroSanitario.NumeroLancamento = Convert.ToInt32(l_dt.Rows[0]["NumeroLancamento"]);
                    if (l_dt.Rows[0]["NumeroMTR"].ToString() != "")
                        pAterroSanitario.NumeroMTR = Convert.ToInt32(l_dt.Rows[0]["NumeroMTR"]);
                    pAterroSanitario.NumeroCaixa = l_dt.Rows[0]["NumeroCaixa"].ToString();
                    if (l_dt.Rows[0]["TotalPeso"].ToString() != "")
                        pAterroSanitario.TotalPeso = Convert.ToDecimal(l_dt.Rows[0]["TotalPeso"].ToString());
                    if (l_dt.Rows[0]["CodigoCliente"].ToString() != "")
                        pAterroSanitario.CodigoCliente = Convert.ToInt32(l_dt.Rows[0]["CodigoCliente"]);
                    pAterroSanitario.Hora = l_dt.Rows[0]["Hora"].ToString();
                    pAterroSanitario.ContainerDescricao = l_dt.Rows[0]["ContainerDescricao"].ToString();
                    if (l_dt.Rows[0]["Excluido"].ToString() != "")
                        pAterroSanitario.Excluido = Convert.ToInt32(l_dt.Rows[0]["Excluido"]);
                    if (l_dt.Rows[0]["Status"].ToString() != "")
                        pAterroSanitario.Status = Convert.ToInt32(l_dt.Rows[0]["Status"]);
                    if (l_dt.Rows[0]["CodigoResiduo"].ToString() != "")
                        pAterroSanitario.CodigoResiduo = Convert.ToInt32(l_dt.Rows[0]["CodigoResiduo"]);
                }
                return pAterroSanitario;
            }
            catch (Exception ex)
            {
                return new clsAterroSanitario();
            }
            finally
            {
                DesconectaBanco();
            }
        }

        public clsAterroSanitario PegaDados(clsAterroSanitario pAterroSanitario, string pNumeroTicket, bool pComNomeMotorista, int pCodigoMotorista)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select ats.Codigo, ats.Data, ats.CodigoAterro, ats.LocalAterro, ats.NumeroTicket, ats.CodigoMotorista, \n";
                s = s + "       ats.CodigoCaminhao, ats.NumeroLancamento, ats.NumeroMTR, ats.NumeroCaixa, ats.TotalPeso, \n";
                s = s + "       ats.CodigoCliente, ats.Hora, ats.ContainerDescricao, ats.Excluido, ats.Status, ats.CodigoResiduo, \n";
                s = s + "       m.Nome as NomeMotorista, ca.Placas as PlacasCaminhao \n";
                s = s + "from   AterroSanitario as ats \n";
                s = s + "inner  join Funcionarios as m  on m.Codigo = ats.CodigoMotorista \n";
                s = s + "inner  join Caminhoes    as ca on ca.Codigo = ats.CodigoCaminhao \n";
                s = s + "where  ats.NumeroTicket = '" + pNumeroTicket + "' \n";
                if (pCodigoMotorista > 0)
                    s = s + "and    ats.CodigoMotorista = " + pCodigoMotorista + " \n";
                if (pComNomeMotorista)
                    s = s + "and  CodigoMotorista > 0 order by CodigoMotorista limit 1 \n";
                else
                    s = s + "order by Codigo \n";
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows[0]["Codigo"].ToString() != "")
                        pAterroSanitario.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                    if (l_dt.Rows[0]["Data"].ToString() != "")
                        pAterroSanitario.Data = Convert.ToDateTime(l_dt.Rows[0]["Data"].ToString()).Date.ToShortDateString();
                    if (l_dt.Rows[0]["CodigoAterro"].ToString() != "")
                        pAterroSanitario.CodigoAterro = Convert.ToInt32(l_dt.Rows[0]["CodigoAterro"]);
                    pAterroSanitario.LocalAterro = l_dt.Rows[0]["LocalAterro"].ToString();
                    pAterroSanitario.NumeroTicket = l_dt.Rows[0]["NumeroTicket"].ToString();
                    if (l_dt.Rows[0]["CodigoMotorista"].ToString() != "")
                        pAterroSanitario.CodigoMotorista = Convert.ToInt32(l_dt.Rows[0]["CodigoMotorista"]);
                    pAterroSanitario.NomeMotorista = l_dt.Rows[0]["NomeMotorista"].ToString();
                    if (l_dt.Rows[0]["CodigoCaminhao"].ToString() != "")
                        pAterroSanitario.CodigoCaminhao = Convert.ToInt32(l_dt.Rows[0]["CodigoCaminhao"]);
                    pAterroSanitario.PlacasCaminhao = l_dt.Rows[0]["PlacasCaminhao"].ToString();
                    pAterroSanitario.NomeMotorista = l_dt.Rows[0]["NomeMotorista"].ToString();
                    if (l_dt.Rows[0]["NumeroLancamento"].ToString() != "")
                        pAterroSanitario.NumeroLancamento = Convert.ToInt32(l_dt.Rows[0]["NumeroLancamento"]);
                    if (l_dt.Rows[0]["NumeroMTR"].ToString() != "")
                        pAterroSanitario.NumeroMTR = Convert.ToInt32(l_dt.Rows[0]["NumeroMTR"]);
                    pAterroSanitario.NumeroCaixa = l_dt.Rows[0]["NumeroCaixa"].ToString();
                    if (l_dt.Rows[0]["TotalPeso"].ToString() != "")
                        pAterroSanitario.TotalPeso = Convert.ToDecimal(l_dt.Rows[0]["TotalPeso"].ToString());
                    if (l_dt.Rows[0]["CodigoCliente"].ToString() != "")
                        pAterroSanitario.CodigoCliente = Convert.ToInt32(l_dt.Rows[0]["CodigoCliente"]);
                    pAterroSanitario.Hora = l_dt.Rows[0]["Hora"].ToString();
                    pAterroSanitario.ContainerDescricao = l_dt.Rows[0]["ContainerDescricao"].ToString();
                    if (l_dt.Rows[0]["Excluido"].ToString() != "")
                        pAterroSanitario.Excluido = Convert.ToInt32(l_dt.Rows[0]["Excluido"]);
                    if (l_dt.Rows[0]["Status"].ToString() != "")
                        pAterroSanitario.Status = Convert.ToInt32(l_dt.Rows[0]["Status"]);
                    if (l_dt.Rows[0]["CodigoResiduo"].ToString() != "")
                        pAterroSanitario.CodigoResiduo = Convert.ToInt32(l_dt.Rows[0]["CodigoResiduo"]);
                }
                return pAterroSanitario;
            }
            catch (Exception ex)
            {
                return new clsAterroSanitario();
            }
            finally
            {
                DesconectaBanco();
            }
        }

        public DataTable PegaDados(clsAterroSanitario pAterroSanitario, int pCodigo, bool pUltimoRegistro, 
                                   string pDataInicial, string pDataFinal, string pCodigoCliente = "", string pCodigoAterro = "", string pOrdem = "")
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select * from ( \n";
                s = s + "select a.Codigo, a.Data, a.Hora, at.LocalAterro, a.NumeroTicket, SUBSTRING_INDEX(m.Nome, '-', 2) as NomeMotorista, \n";
                s = s + "       ca.Modelo, a.NumeroCaixa, a.TotalPeso, \n";
                s = s + "       '' as NomeCliente, 0.00 as PesoIndividual \n";
                s = s + "from   AterroSanitario a \n";
                s = s + "left   join Funcionarios m on m.Codigo = a.CodigoMotorista \n";
                s = s + "left   join Caminhoes ca on ca.Codigo = a.CodigoCaminhao \n";
                s = s + "left   join Aterro at on at.codigo = a.CodigoAterro \n";
                if (pCodigoAterro == "")
                    s = s + "where  (a.CodigoAterro = 2 or a.CodigoAterro = 13) \n ";
                else
                    s = s + "where  a.CodigoAterro = " + pCodigoAterro +  "\n ";

                s = s + "and    a.CodigoResiduo = 999 and (excluido = 0 or excluido is null) \n";
                if (pCodigo > 0)
                {
                    s = s + "and  a.Codigo = " + pCodigo + " \n";
                }
                else if (pCodigo == 0 && pDataInicial.Length > 0 && pDataFinal.Length > 0)
                {
                    s = s + "and  a.Data >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n";
                    s = s + "and  a.Data <= '" + Convert.ToDateTime(pDataFinal).ToString("yyyy-MM-dd") + "' \n";
                }

                s = s + "union all \n";

                s = s + "select 0 as Codigo, lmtr.DataDescarga as Data, '00:00:00' as Hora, lmtr.Deposito as LocalAterro, lmtr.Ticket, SUBSTRING_INDEX(m.Nome, '-', 2) as NomeMotorista, \n";
                s = s + "       ca.Modelo, l.NumeroCaixa , lmtr.Quantidade as TotalPeso, c.NomeFantasia as NomeCliente, lmtr.Franquia as PesoIndividual \n";
                s = s + "from   LancamentoMTR lmtr \n";
                s = s + "inner  join Lancamentos l on l.NumeroLancamento = lmtr.NumeroLancamento \n";
                s = s + "left   join Funcionarios m on m.Codigo = l.CodigoMotoristaRetirou \n";
                s = s + "left   join Caminhoes ca on ca.Codigo = l.CodigoCaminhoRetirada \n";
                s = s + "left   join Clientes c on c.Codigo = l.CodigoCliente \n";
                s = s + "left   join Aterro at on at.codigo = lmtr.CodigoAterroSanitario \n";
                if (pCodigoAterro == "2")
                    s = s + "where  (substring(lmtr.Deposito,1,2) = '2-' or substring(lmtr.Deposito,1,2) = '02') \n ";
                else if (pCodigoAterro == "13")
                    s = s + "where  (substring(lmtr.Deposito,1,2) = '13') \n ";
                else
                    s = s + "where  (substring(lmtr.Deposito,1,2) = '2-' or substring(lmtr.Deposito,1,2) = '02' or substring(lmtr.Deposito,1,2) = '13') \n ";
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
                if (pDataInicial.Length > 0 && pDataFinal.Length > 0)
                {
                    s = s + "and  lmtr.DataDescarga >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n";
                    s = s + "and  lmtr.DataDescarga <= '" + Convert.ToDateTime(pDataFinal).ToString("yyyy-MM-dd") + "' \n";
                }
                if (pCodigoCliente != "")
                    s = s + "and  l.CodigoCliente = " + pCodigoCliente + " \n";
                s = s + ") x \n";

                if (pUltimoRegistro)
                {
                    s = s + "limit 1 \n";
                    s = s + "order by Codigo desc \n";
                }
                if (pOrdem != "")
                {
                    s = s + "order by " + pOrdem + "\n";
                }
                else
                {
                    s = s + "order by Data, LocalAterro, NumeroTicket \n";
                    //s = s + "order by Data, Convert(NumeroTicket, UNSIGNED) \n";
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
                s = s + "show  columns ";
                s = s + "from  AterroSanitario ";
                s = s + "where type like 'varchar%' and field = '" + pCampo + "' ";
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
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Codigo ";
                s = s + "from   AterroSanitario  ";
                if (pCodigo != 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " ";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pCodigo != 0)
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
        public decimal PegaPesoTotal(string pNumeroTicket, string pAnoDescarga)
        {
            decimal dRet = 0;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select sum(TotalPeso) as xTotalPeso \n";
                s = s + "from   AterroSanitario \n";
                s = s + "where  NumeroTicket = '" + pNumeroTicket + "' and CodigoResiduo = 999 and year(data) = " + pAnoDescarga + " \n";
                s = s + "and    excluido = 0 \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    dRet = Convert.ToDecimal(l_dt.Rows[0][0]);
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

        public int PegaCodigoMotoristaQueDescarregou(string pNumeroTicket, string pAnoDescarga)
        {
            int dRet = 0;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select CodigoMotorista \n";
                s = s + "from   AterroSanitario \n";
                s = s + "where  NumeroTicket = '" + pNumeroTicket + "' and CodigoResiduo = 999 and year(data) = " + pAnoDescarga + " \n";
                s = s + "and    excluido = 0 \n";
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
        public int PegaCodigoCaminhaoQueDescarregou(string pNumeroTicket, string pAnoDescarga)
        {
            int dRet = 0;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select CodigoCaminhao \n";
                s = s + "from   AterroSanitario \n";
                s = s + "where  NumeroTicket = '" + pNumeroTicket + "' and CodigoResiduo = 999 and year(data) = " + pAnoDescarga + " \n";
                s = s + "and    excluido = 0 \n";
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

        //public int ExisteLancamentoNoAterroSanitario(int pNumeroLancamento, int pCodigoCliente, int pCodigoResiduo)
        public int ExisteLancamentoNoAterroSanitario(int pNumeroLancamento, int pCodigoResiduo)
        {
            int iRetCodigoSequencial = 0;
            //if (pNumeroLancamento > 0 && pCodigoCliente > 0 && pCodigoResiduo > 0)
            if (pNumeroLancamento > 0 && pCodigoResiduo > 0)
                {
                try
                {
                    oDB.ConectaMySql();
                    s = "";
                    s = s + "select Codigo ";
                    s = s + "from   AterroSanitario ";
                    s = s + "where  NumeroLancamento = " + pNumeroLancamento + " \n";
                    //s = s + "and    CodigoCliente    = " + pCodigoCliente + " \n";
                    s = s + "and    CodigoResiduo    = " + pCodigoResiduo + " \n";
                    s = s + "limit  1 \n";
                    FillDataSet();
                    if (l_dt.Rows.Count > 0)
                        iRetCodigoSequencial = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                }
                catch (Exception ex)
                {
                    iRetCodigoSequencial = -1;
                }
                finally
                {
                    oDB.DesconectaMySql();
                }
            }
            return iRetCodigoSequencial;
        }

        public void Inserir(clsAterroSanitario pAterroSanitario, int pCodigo = 0)
        {
            try
            {
                s = "";
                s = s + "insert into AterroSanitario \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + " Codigo, \n";
                s = s + "  Data, CodigoAterro, LocalAterro, NumeroTicket, CodigoMotorista, \n";
                s = s + "  CodigoCaminhao, NumeroLancamento, NumeroMTR, NumeroCaixa, TotalPeso, \n";
                s = s + "  CodigoCliente, Hora, ContainerDescricao, Excluido, Status, CodigoResiduo \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + pCodigo + ", \n";

                if (pAterroSanitario.Data != "")
                    s = s + "'" + Convert.ToDateTime(pAterroSanitario.Data).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                if (pAterroSanitario.CodigoAterro > 0)
                    s = s + " " + pAterroSanitario.CodigoAterro.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                s = s + " '" + pAterroSanitario.LocalAterro + "', \n";
                s = s + " '" + pAterroSanitario.NumeroTicket + "', \n";
                if (pAterroSanitario.CodigoMotorista > 0)
                    s = s + " " + pAterroSanitario.CodigoMotorista.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pAterroSanitario.CodigoCaminhao > 0)
                    s = s + " " + pAterroSanitario.CodigoCaminhao.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pAterroSanitario.NumeroLancamento > 0)
                    s = s + " " + pAterroSanitario.NumeroLancamento.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pAterroSanitario.NumeroMTR > 0)
                    s = s + " " + pAterroSanitario.NumeroMTR.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                s = s + " '" + pAterroSanitario.NumeroCaixa + "', \n";
                if (pAterroSanitario.TotalPeso > 0)
                    s = s + " " + pAterroSanitario.TotalPeso.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pAterroSanitario.CodigoCliente > 0)
                    s = s + " " + pAterroSanitario.CodigoCliente.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                s = s + " '" + pAterroSanitario.Hora + "', \n";
                s = s + " '" + pAterroSanitario.ContainerDescricao + "', \n";
                if (pAterroSanitario.Excluido > 0)
                    s = s + " " + pAterroSanitario.Excluido.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pAterroSanitario.Status > 0)
                    s = s + " " + pAterroSanitario.Status.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pAterroSanitario.CodigoResiduo > 0)
                    s = s + " " + pAterroSanitario.CodigoResiduo.ToString().Replace(",", ".") + " \n";
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
        public string Alterar(clsAterroSanitario pAterroSanitario, int pCodigo)
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
                s = s + "update AterroSanitario \n";
                if (pAterroSanitario.Data != "")
                    s = s + " set Data = '" + Convert.ToDateTime(pAterroSanitario.Data).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + " set Data = '0001-01-01', \n";
                if (pAterroSanitario.CodigoAterro > 0)
                    s = s + "CodigoAterro = " + pAterroSanitario.CodigoAterro.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " CodigoAterro = 0, \n";
                s = s + " LocalAterro = '" + pAterroSanitario.LocalAterro + "', \n";
                s = s + " NumeroTicket = '" + pAterroSanitario.NumeroTicket + "', \n";
                if (pAterroSanitario.CodigoMotorista > 0)
                    s = s + " CodigoMotorista = " + pAterroSanitario.CodigoMotorista.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " CodigoMotorista = 0, \n";
                if (pAterroSanitario.CodigoCaminhao > 0)
                    s = s + " CodigoCaminhao = " + pAterroSanitario.CodigoCaminhao.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " CodigoCaminhao = 0, \n";
                if (pAterroSanitario.NumeroLancamento > 0)
                    s = s + " NumeroLancamento = " + pAterroSanitario.NumeroLancamento.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " NumeroLancamento = 0, \n";
                if (pAterroSanitario.NumeroMTR > 0)
                    s = s + " NumeroMTR = " + pAterroSanitario.NumeroMTR.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " NumeroMTR = 0, \n";
                s = s + " NumeroCaixa = '" + pAterroSanitario.NumeroCaixa + "', \n";
                if (pAterroSanitario.TotalPeso > 0)
                    s = s + " TotalPeso = " + pAterroSanitario.TotalPeso.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " TotalPeso = 0, \n";
                if (pAterroSanitario.CodigoCliente > 0)
                    s = s + " CodigoCliente = " + pAterroSanitario.CodigoCliente.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " CodigoCliente = 0, \n";
                s = s + " Hora = '" + pAterroSanitario.Hora + "', \n";
                s = s + " ContainerDescricao = '" + pAterroSanitario.ContainerDescricao + "', \n";
                if (pAterroSanitario.Excluido > 0)
                    s = s + " Excluido = " + pAterroSanitario.Excluido.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " Excluido = 0, \n";
                if (pAterroSanitario.Status > 0)
                    s = s + " Status = " + pAterroSanitario.Status.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " Status = 0, \n";
                if (pAterroSanitario.CodigoResiduo > 0)
                    s = s + " CodigoResiduo = " + pAterroSanitario.CodigoResiduo.ToString().Replace(",", ".") + " \n";
                else
                    s = s + " CodigoResiduo = 0 \n";
                
                s = s + " where Codigo = " + pCodigo + " \n";                

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
        public DataTable PreencheDataTable(string pOrdem, string pDataInicial, string pDataFinal)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select * from ( \n";
                s = s + "select a.Codigo, a.Data, a.Hora, at.LocalAterro, a.NumeroTicket,SUBSTRING_INDEX(m.Nome, '-', 2) as NomeMotorista, \n";
                s = s + "       ca.Modelo, a.NumeroCaixa, a.TotalPeso, \n";
                s = s + "       '' as NomeCliente \n";
                s = s + "from   AterroSanitario a \n";
                s = s + "left   join Funcionarios m on m.Codigo = a.CodigoMotorista \n";
                s = s + "left   join Caminhoes ca on ca.Codigo = a.CodigoCaminhao \n";
                s = s + "left   join Aterro at on at.codigo = a.CodigoAterro \n";
                s = s + "where  (a.CodigoAterro = 2 or a.CodigoAterro = 13) \n ";
                s = s + "and    a.CodigoResiduo = 999 and (excluido = 0 or excluido is null) \n";
                if (pDataInicial.Length > 0 && pDataFinal.Length > 0)
                {
                    s = s + "and  a.Data >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n";
                    s = s + "and  a.Data <= '" + Convert.ToDateTime(pDataFinal).ToString("yyyy-MM-dd") + "' \n";
                }

                s = s + "union all \n";
                s = s + "select 0 as Codigo, lmtr.DataDescarga as Data, '00:00:00' as Hora, at.LocalAterro, lmtr.Ticket, SUBSTRING_INDEX(m.Nome, '-', 2) as NomeMotorista, \n";
                s = s + "       ca.Modelo, l.NumeroCaixa , lmtr.Quantidade as TotalPeso, c.Nome as NomeCliente \n";
                s = s + "from   LancamentoMTR lmtr \n";
                s = s + "inner  join Lancamentos l on l.NumeroLancamento = lmtr.NumeroLancamento \n";
                s = s + "inner  join Funcionarios m on m.Codigo = l.CodigoMotoristaRetirou \n";
                s = s + "inner  join Caminhoes ca on ca.Codigo = l.CodigoCaminhoRetirada \n";
                s = s + "inner  join Clientes c on c.Codigo = l.CodigoCliente \n";
                s = s + "left   join Aterro at on at.codigo = lmtr.CodigoAterroSanitario \n";
                s = s + "where  (substring(lmtr.Deposito,1,1) = 2 or substring(lmtr.Deposito,1,2) = 13) \n ";

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
                if (pDataInicial.Length > 0 && pDataFinal.Length > 0)
                {
                    s = s + "and  lmtr.DataDescarga >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n";
                    s = s + "and  lmtr.DataDescarga <= '" + Convert.ToDateTime(pDataFinal).ToString("yyyy-MM-dd") + "' \n";
                }
                s = s + ") x \n";
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

        public DataTable PreencheDataTable(string pOrdem, string pFiltro, string pCampo,
                                           string pDataInicial, string pDataFinal)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select * from ( \n";
                s = s + "select a.Codigo, a.Data, a.Hora, at.LocalAterro, a.NumeroTicket,SUBSTRING_INDEX(m.Nome, '-', 2) as NomeMotorista, \n";
                s = s + "       ca.Modelo, a.NumeroCaixa, a.TotalPeso, \n";
                s = s + "       '' as NomeCliente \n";
                s = s + "from   AterroSanitario a \n";
                s = s + "left   join Funcionarios m on m.Codigo = a.CodigoMotorista \n";
                s = s + "left   join Caminhoes ca on ca.Codigo = a.CodigoCaminhao \n";
                s = s + "left   join Aterro at on at.codigo = a.CodigoAterro \n";
                s = s + "where  (a.CodigoAterro = 2 or a.CodigoAterro = 13) \n ";
                s = s + "and    a.CodigoResiduo = 999 and (excluido = 0 or excluido is null) \n";
                if (pDataInicial.Length > 0 && pDataFinal.Length > 0)
                {
                    s = s + "and  a.Data >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n";
                    s = s + "and  a.Data <= '" + Convert.ToDateTime(pDataFinal).ToString("yyyy-MM-dd") + "' \n";
                }

                s = s + "union all \n";
                s = s + "select 0 as Codigo, lmtr.DataDescarga as Data, '00:00:00' as Hora, at.LocalAterro, lmtr.Ticket, SUBSTRING_INDEX(m.Nome, '-', 2) as NomeMotorista, \n";
                s = s + "       ca.Modelo, l.NumeroCaixa , lmtr.Quantidade as TotalPeso, c.Nome as NomeCliente \n";
                s = s + "from   LancamentoMTR lmtr \n";
                s = s + "inner  join Lancamentos l on l.NumeroLancamento = lmtr.NumeroLancamento \n";
                s = s + "inner  join Funcionarios m on m.Codigo = l.CodigoMotoristaRetirou \n";
                s = s + "inner  join Caminhoes ca on ca.Codigo = l.CodigoCaminhoRetirada \n";
                s = s + "inner  join Clientes c on c.Codigo = l.CodigoCliente \n";
                s = s + "left   join Aterro at on at.codigo = lmtr.CodigoAterroSanitario \n";
                s = s + "where  (substring(lmtr.Deposito,1,1) = 2 or substring(lmtr.Deposito,1,2) = 13) \n ";

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
                if (pDataInicial.Length > 0 && pDataFinal.Length > 0)
                {
                    s = s + "and  lmtr.DataDescarga >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n";
                    s = s + "and  lmtr.DataDescarga <= '" + Convert.ToDateTime(pDataFinal).ToString("yyyy-MM-dd") + "' \n";
                }
                s = s + ") x \n";
                s = s + "order by " + pOrdem + " \n";
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

        public string Excluir(int pCodigo)
        {
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
                    s = s + "delete from AterroSanitario ";
                    s = s + "where  Codigo = " + pCodigo.ToString();
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
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
        public string ExcluirMesAno(string pMes, string pAno)
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
                if (pAno != "" && pMes != "")
                {
                    s = "";
                    s = s + "delete from AterroSanitario ";
                    s = s + "where  month(Data) = " + pMes + " \n";
                    s = s + "and    year(Data) = 20" + pAno + " \n";
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
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
        public string ExcluirNumeroLancamento(int pNumeroLancamento)
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
                if (pNumeroLancamento > 0)
                {
                    s = "";
                    s = s + "delete from AterroSanitario \n";
                    s = s + "where  NumeroLancamento = " + pNumeroLancamento.ToString() + " \n";
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
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
        public string ExcluirDescarga(string pNumeroTicket, string pAno)
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
                s = s + "delete from AterroSanitario \n";
                s = s + "where  CodigoResiduo = 999 \n";
                s = s + "and    NumeroTicket = '" + pNumeroTicket + "' \n";
                s = s + "and    year(Data) = " + pAno + " \n";
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
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                if (pNumeroLancamento > 0 && pNumeroMTR > 0 && pCodigoResiduo > 0)
                {
                    s = "";
                    s = s + "delete from AterroSanitario \n";
                    s = s + "where  NumeroLancamento = " + pNumeroLancamento.ToString() + " \n";
                    s = s + "and    NumeroMTR        = " + pNumeroMTR.ToString() + " \n";
                    s = s + "and    CodigoResiduo    = " + pCodigoResiduo.ToString() + " \n";
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
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
        public DataTable AdicionaSubTotal(DataTable pDt)
        {
            string _dataAnterior = "";
            string _destinoFinal = "";
            decimal _peso = 0;
            pDt.Columns.Add("DataOrdenada", Type.GetType("System.DateTime"));
            int _lns = pDt.Rows.Count - 1;
            DataRow _dr;
            if (_lns > 0)
            {
                foreach (DataRow _dr1 in pDt.Rows)
                {
                    _dr1["DataOrdenada"] = geral.Left(_dr1["Data"].ToString(), 10) + " 00:00:00";
                    _dr1["LocalAterro"] = _dr1["LocalAterro"].ToString().Replace("2-", "").Replace("13-", "").Replace("02-", "");
                    if (_dr1["LocalAterro"].ToString().IndexOf("PROACTIVA AT") > -1)
                        _dr1["LocalAterro"] = "Tijucas";
                    else if (_dr1["LocalAterro"].ToString().IndexOf("PROACTIVA TB") > -1)
                        _dr1["LocalAterro"] = "TRANSB 282";
                }
                DataRow[] _ard = pDt.Select("", "DataOrdenada, LocalAterro");
                _dr = _ard[0];
                _dataAnterior = _dr["Data"].ToString();
                _destinoFinal = _dr["LocalAterro"].ToString();
                for (int i = 0; i < _lns; i++)
                {
                    _dr = _ard[i];
                    if (_dr["DataOrdenada"].ToString() != _dataAnterior || _dr["LocalAterro"].ToString() != _destinoFinal)
                    {
                        pDt.NewRow();
                        pDt.Rows.Add();
                        pDt.Rows[pDt.Rows.Count - 1]["DataOrdenada"] = Convert.ToDateTime(_dataAnterior).ToString("dd/MM/yyyy") + " 23:59:59";
                        pDt.Rows[pDt.Rows.Count - 1]["LocalAterro"] = _destinoFinal;
                        pDt.Rows[pDt.Rows.Count - 1]["TotalPeso"] = _peso;
                        _peso = 0;
                    }
                    if (_dr["TotalPeso"].ToString() != "")
                        _peso = _peso + Convert.ToDecimal(_dr["TotalPeso"].ToString());
                    _dataAnterior = _dr["DataOrdenada"].ToString();
                    _destinoFinal = _dr["LocalAterro"].ToString();
                }
            }
            if (_peso > 0)
            {
                if (pDt.Rows[_lns]["TotalPeso"].ToString() != "")
                    _peso = _peso + Convert.ToDecimal(pDt.Rows[_lns]["TotalPeso"].ToString());
                pDt.NewRow();
                pDt.Rows.Add();
                pDt.Rows[pDt.Rows.Count - 1]["DataOrdenada"] = Convert.ToDateTime(_dataAnterior).ToString("dd/MM/yyyy") + " 23:59:59";
                pDt.Rows[pDt.Rows.Count - 1]["LocalAterro"] = _destinoFinal;
                pDt.Rows[pDt.Rows.Count - 1]["TotalPeso"] = _peso;
            }
            DataTable _dtOrdenado = new DataTable();
            geral.Ordem = "LocalAterro, DataOrdenada";
            DataRow[] _drr = pDt.Select("", geral.Ordem);
            foreach (DataColumn dc in pDt.Columns)
            {
                _dtOrdenado.Columns.Add(dc.ColumnName, dc.DataType);
            }
            foreach (DataRow dr in _drr)
            {
                _dtOrdenado.NewRow();
                _dtOrdenado.Rows.Add();
                foreach (DataColumn dc in _dtOrdenado.Columns)
                {
                    _dtOrdenado.Rows[_dtOrdenado.Rows.Count - 1][dc.ColumnName] = dr[dc.ColumnName];
                }
            }
            return _dtOrdenado;
        }
    }
}