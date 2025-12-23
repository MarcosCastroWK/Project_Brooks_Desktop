using System;
using System.Data;
using MySql.Data.MySqlClient;
using MySql.Data;
using SILCNegocios;

namespace LibSILC
{
    public class clsDTRDados
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
        public clsDTR PegaDados(clsDTR pDTR, int pSequencial, int pNumeroLancamento, int pNumeroMTR, int pCodigoResiduo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select d.Sequencial, d.NumeroLancamento, d.DataColeta, d.CodigoClienteColetado, d.ClienteColetado as NomeCliente, \n";
                s = s + "       d.CodigoTipoResiduo, d.TipoResiduo as Residuo, d.TotalKg, d.LocalDTR, d.DataSaida, d.CodigoLocalEntrega, \n";
                s = s + "       d.LocalEntrega as DestinoFinal, d.Fechado, d.Imprimido, d.NumeroMTR, d.HoraSaida, d.CodigoMotorista, \n";
                s = s + "       d.CodigoCaminhao, d.NumeroImpressao as Impressao, d.CodigoClienteEmitiuMTR, d.QtNova, d.TotalQtNova \n";
                s = s + "from   DTR d \n";
                if (pSequencial > 0)
                {
                    s = s + "where  d.Sequencial = " + pSequencial + " \n";
                    s = s + "and    d.NumeroLancamento = " + pNumeroLancamento + " \n";
                    s = s + "and    d.NumeroMTR = " + pNumeroMTR + " \n";
                    s = s + "and    d.CodigoTipoResiduo = " + pCodigoResiduo + " \n";
                    s = s + "limit 1";
                }
                else if (pSequencial == 0)
                {
                    if (pNumeroLancamento > 0)
                    {
                        s = s + "where  d.NumeroLancamento = " + pNumeroLancamento + " \n";
                        s = s + "and    d.NumeroMTR = " + pNumeroMTR + " \n";
                        s = s + "and    d.CodigoTipoResiduo = " + pCodigoResiduo + " \n";
                    }
                    s = s + " order by Sequencial limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows[0]["Sequencial"].ToString() != "")
                        pDTR.Sequencial = Convert.ToInt32(l_dt.Rows[0]["Sequencial"]);
                    if (l_dt.Rows[0]["NumeroLancamento"].ToString() != "")
                        pDTR.NumeroLancamento = Convert.ToInt32(l_dt.Rows[0]["NumeroLancamento"]);
                    if (l_dt.Rows[0]["DataColeta"].ToString() != "")
                        pDTR.DataColeta = Convert.ToDateTime(l_dt.Rows[0]["DataColeta"].ToString()).Date.ToShortDateString();
                    if (l_dt.Rows[0]["CodigoClienteColetado"].ToString() != "")
                        pDTR.CodigoClienteColetado = Convert.ToInt32(l_dt.Rows[0]["CodigoClienteColetado"]);
                    pDTR.ClienteColetado = l_dt.Rows[0]["NomeCliente"].ToString();
                    if (l_dt.Rows[0]["CodigoTipoResiduo"].ToString() != "")
                        pDTR.CodigoTipoResiduo = Convert.ToInt32(l_dt.Rows[0]["CodigoTipoResiduo"]);
                    pDTR.TipoResiduo = l_dt.Rows[0]["Residuo"].ToString();
                    if (l_dt.Rows[0]["TotalKg"].ToString() != "")
                        pDTR.TotalKg = Convert.ToDecimal(l_dt.Rows[0]["TotalKg"].ToString().Replace(",", "."));
                    pDTR.LocalDTR = l_dt.Rows[0]["LocalDTR"].ToString();
                    if (l_dt.Rows[0]["DataSaida"].ToString() != "")
                        pDTR.DataSaida = Convert.ToDateTime(l_dt.Rows[0]["DataSaida"].ToString()).Date.ToShortDateString();
                    if (l_dt.Rows[0]["CodigoLocalEntrega"].ToString() != "")
                        pDTR.CodigoLocalEntrega = Convert.ToInt32(l_dt.Rows[0]["CodigoLocalEntrega"]);
                    pDTR.LocalEntrega = l_dt.Rows[0]["DestinoFinal"].ToString();
                    if (l_dt.Rows[0]["Fechado"].ToString() != "")
                        pDTR.Fechado = Convert.ToInt32(l_dt.Rows[0]["Fechado"]);
                    if (l_dt.Rows[0]["Imprimido"].ToString() != "")
                        pDTR.Imprimido = Convert.ToInt32(l_dt.Rows[0]["Imprimido"]);
                    if (l_dt.Rows[0]["NumeroMTR"].ToString() != "")
                        pDTR.NumeroMTR = Convert.ToInt32(l_dt.Rows[0]["NumeroMTR"]);
                    pDTR.HoraSaida = l_dt.Rows[0]["HoraSaida"].ToString();
                    if (l_dt.Rows[0]["CodigoMotorista"].ToString() != "")
                        pDTR.CodigoMotorista = Convert.ToInt32(l_dt.Rows[0]["CodigoMotorista"]);
                    if (l_dt.Rows[0]["CodigoCaminhao"].ToString() != "")
                        pDTR.CodigoCaminhao = Convert.ToInt32(l_dt.Rows[0]["CodigoCaminhao"]);
                    if (l_dt.Rows[0]["Impressao"].ToString() != "")
                        pDTR.NumeroImpressao = Convert.ToInt32(l_dt.Rows[0]["Impressao"]);
                    if (l_dt.Rows[0]["CodigoClienteEmitiuMTR"].ToString() != "")
                        pDTR.CodigoClienteEmitiuMTR = Convert.ToInt32(l_dt.Rows[0]["CodigoClienteEmitiuMTR"]);
                    if (l_dt.Rows[0]["QtNova"].ToString() != "")
                        pDTR.QtNova = Convert.ToDecimal(l_dt.Rows[0]["QtNova"].ToString().Replace(",", "."));
                    if (l_dt.Rows[0]["TotalQtNova"].ToString() != "")
                        pDTR.TotalQtNova = Convert.ToDecimal(l_dt.Rows[0]["TotalQtNova"].ToString().Replace(",", "."));
                }                
            }
            catch (Exception ex)
            {
                pDTR = new clsDTR();
            }
            finally
            {
                DesconectaBanco();
            }
            return pDTR;
        }
        public clsDTR PegaDadosArmazenados(clsDTR pDTR, int pNumeroLancamento, int pNumeroMTR, int pCodigoResiduo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "  select lmtr.NumeroMTR, l.DataRetirada as DataColeta, '01-01-0100 00:00:00' as DataSaida, l.CodigoCliente, c.Nome as NomeCliente, \n ";
                s = s + "         r.DescricaoReduzida as Residuo, lmtr.Quantidade, r.Unidade, '' as DestinoFinal \n ";
                s = s + "  from LancamentoMTR lmtr \n";
                s = s + "  left join Lancamentos l on lmtr.NumeroLancamento = l.NumeroLancamento \n ";
                s = s + "  left join Clientes c on l.CodigoCliente = c.Codigo \n ";
                s = s + "  left join Residuos r on lmtr.CodigoResiduo = r.Codigo \n ";
                s = s + "  where left(lmtr.Deposito, 3) = 'DTR'  \n";
                s = s + "  and   not exists(select * from DTR \n ";
                s = s + "                   where CodigoTipoResiduo = lmtr.CodigoResiduo \n ";
                s = s + "                   and   NumeroLancamento = lmtr.NumeroLancamento \n ";
                s = s + "                   and   NumeroMTR = lmtr.NumeroMTR) \n ";
                s = s + "  and   lmtr.NumeroLancamento = " + pNumeroLancamento + " \n";
                s = s + "  and   lmtr.NumeroMTR = " + pNumeroMTR + " \n";
                s = s + "  and   lmtr.CodigoResiduo = " + pCodigoResiduo + " \n";
                s = s + "  limit 1";
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    pDTR.NumeroLancamento = pNumeroLancamento;
                    if (l_dt.Rows[0]["DataColeta"].ToString() != "")
                        pDTR.DataColeta = Convert.ToDateTime(l_dt.Rows[0]["DataColeta"].ToString()).Date.ToShortDateString();
                    if (l_dt.Rows[0]["CodigoCliente"].ToString() != "")
                        pDTR.CodigoClienteColetado = Convert.ToInt32(l_dt.Rows[0]["CodigoCliente"]);
                    pDTR.ClienteColetado = l_dt.Rows[0]["NomeCliente"].ToString();
                    pDTR.CodigoTipoResiduo = pCodigoResiduo;
                    pDTR.TipoResiduo = l_dt.Rows[0]["Residuo"].ToString();
                    if (l_dt.Rows[0]["Quantidade"].ToString() != "")
                        pDTR.TotalKg = Convert.ToDecimal(l_dt.Rows[0]["Quantidade"].ToString().Replace(",", "."));
                    if (l_dt.Rows[0]["DataSaida"].ToString() != "")
                        pDTR.DataSaida = Convert.ToDateTime(l_dt.Rows[0]["DataSaida"].ToString()).Date.ToShortDateString();
                    pDTR.NumeroMTR = pNumeroMTR;
                }
            }
            catch (Exception ex)
            {
                pDTR = new clsDTR();
            }
            finally
            {
                DesconectaBanco();
            }
            return pDTR;
        }
        public DataTable PegaDados(clsDTR pDTR, int pSequencial, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select d.Sequencial, d.NumeroLancamento, d.DataColeta, d.CodigoClienteColetado, d.ClienteColetado as NomeCliente, \n";
                s = s + "       d.CodigoTipoResiduo, d.TipoResiduo as Residuo, d.TotalKg as Quantidade, d.LocalDTR, d.DataSaida, d.CodigoLocalEntrega, \n";
                s = s + "       d.LocalEntrega as DestinoFinal, d.Fechado, d.Imprimido, d.NumeroMTR, d.HoraSaida, d.CodigoMotorista, \n";
                s = s + "       concat(d.NumeroLancamento,'-',d.NumeroMTR,'-',d.CodigoTipoResiduo) as Lote, d.CodigoCaminhao, d.NumeroImpressao, d.CodigoClienteEmitiuMTR, d.QtNova, d.TotalQtNova, \n";
                s = s + "       r.Unidade \n";
                s = s + "from   DTR d \n";
                s = s + "inner  join Residuos r on r.Codigo = d.CodigoTipoResiduo \n ";
                s = s + "where  d.DataSaida <> '0100-01-01' and not d.DataSaida is null \n ";
                if (pSequencial > 0)
                {
                    s = s + "and  d.Sequencial = " + pSequencial + " and Fechado = 1 AND Imprimido = 1 \n";
                }
                if (pUltimoRegistro)
                {
                    s = s + "order by d.Sequencial desc \n";
                }
                else if (pSequencial == 0)
                {
                    s = s + "and      d.Sequencial = (SELECT Sequencial FROM DTR WHERE Fechado = 1 AND Imprimido = 1 ORDER BY Sequencial DESC limit 1) \n";
                    s = s + "order by d.DataSaida desc \n";
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
        public DataTable PreencheDTExcel(clsDTR pDTR, int pSequencial)
        {
            try
            {
                if (pSequencial > 0)
                {
                    ConectaBanco();
                    s = "";
                    s = s + "select c.CNPJ_CPF as 'CNPJ/CPF Gerador', d.ClienteColetado as 'Razao Social', '' as 'Nr NF', l.NumeroCaixa as 'Nr CX', \n";
                    s = s + "       i.CodigoIBAMA as 'Codigo IBAMA',  d.TipoResiduo as Residuo, r.TecnologiaAplicada as Tecnologia, d.LocalEntrega as 'Destino Final', \n";
                    s = s + "       '03.938.048/0001-33' as 'CNPJ DTR', '' as 'Nr Manifesto (qdo houver)',  d.TotalKg as Quantidade, r.Unidade, '' as '% Participacao' \n";
                    s = s + "from   DTR d \n";
                    s = s + "inner  join Residuos r on r.Codigo = d.CodigoTipoResiduo \n ";
                    s = s + "inner  join Clientes c on c.Codigo = d.CodigoClienteColetado \n ";
                    s = s + "inner  join IBAMA    i on i.Codigo = r.CodigoIbama \n ";
                    s = s + "inner  join Lancamentos l on l.NumeroLancamento = d.NumeroLancamento \n ";
                    s = s + "where  d.DataSaida <> '0100-01-01' and not d.DataSaida is null \n ";
                    s = s + "and  d.Sequencial = " + pSequencial + " and Fechado = 1 AND Imprimido = 1 \n";
                    s = s + "order by d.TipoResiduo \n";
                    FillDataSet();
                }
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

        public decimal PegaTotalResiduo(int pSequencial, string pResiduo)
        {
            decimal dRet = 0;
            try
            {
                if (pSequencial > 0)
                {
                    ConectaBanco();
                    s = "";
                    s = s + "select sum(TotalKg) as PesoTotalResiduo \n";
                    s = s + "from   DTR d \n";
                    s = s + "where  d.DataSaida <> '0100-01-01' and not d.DataSaida is null \n ";
                    s = s + "and    d.Sequencial = " + pSequencial + " and Fechado = 1 AND Imprimido = 1 \n";
                    s = s + "and    d.TipoResiduo = '" + pResiduo + "' \n";
                    FillDataSet();
                }
                dRet = Convert.ToDecimal(l_dt.Rows[0][0]);
            }
            catch (Exception ex)
            {
                dRet = 0;
            }
            finally
            {
                DesconectaBanco();
            }
            return dRet;
        }
        public int PegaUltimoNumeroImpressao()
        {
            int iRet = 0;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select NumeroImpressao \n";
                s = s + "from   DTR \n";
                s = s + "order  by NumeroImpressao desc \n ";
                s = s + "limit  1 \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    iRet = Convert.ToInt32(l_dt.Rows[0][0]);
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
        public int PegaTamanhoCampoVarChar(string pCampo)
        {
            int iRet = 0;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "show columns ";
                s = s + "from DTR ";
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
        public string DadoExiste(int pSequencial, int pNumeroLancamento, int pCodigoResiduo, int pNumeroMTR)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Sequencial ";
                s = s + "from   DTR  ";
                if (pSequencial != 0)
                {
                    s = s + "where  Sequencial = " + pSequencial + " ";
                    s = s + "and    NumeroLancamento = " + pNumeroLancamento.ToString() + " \n";
                    s = s + "and    NumeroMTR = " + pNumeroMTR.ToString() + " \n";
                    s = s + "and    CodigoTipoResiduo = " + pCodigoResiduo.ToString() + " \n";
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
        public string DadoExiste(int pSequencial)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                if (pSequencial != 0)
                {
                    s = s + "select Sequencial ";
                    s = s + "from   DTR  ";
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
        public void Inserir(clsDTR pDTR)
        {
            try
            {
                s = "";
                s = s + "insert into DTR \n";
                s = s + "( \n";
                s = s + "  Sequencial, NumeroLancamento, DataColeta, CodigoClienteColetado, ClienteColetado, \n";
                s = s + "  CodigoTipoResiduo, TipoResiduo, TotalKg, LocalDTR, DataSaida, CodigoLocalEntrega, \n";
                s = s + "  LocalEntrega, Fechado, Imprimido, NumeroMTR, HoraSaida, CodigoMotorista, \n";
                s = s + "  CodigoCaminhao, NumeroImpressao, CodigoClienteEmitiuMTR, QtNova, TotalQtNova \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pDTR.Sequencial > 0)
                    s = s + " " + pDTR.Sequencial.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pDTR.NumeroLancamento > 0)
                    s = s + " " + pDTR.NumeroLancamento.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pDTR.DataColeta == "")
                    s = s + "'0001-01-01', ";
                else
                    s = s + "'" + Convert.ToDateTime(pDTR.DataColeta).ToString("yyyy-MM-dd") + "', ";
                if (pDTR.CodigoClienteColetado > 0)
                    s = s + " " + pDTR.CodigoClienteColetado.ToString() + ", \n";
                else
                    s = s + "0, \n";
                s = s + " '" + pDTR.ClienteColetado + "', \n";
                if (pDTR.CodigoTipoResiduo > 0)
                    s = s + " " + pDTR.CodigoTipoResiduo.ToString() + ", \n";
                else
                    s = s + "0, \n";
                s = s + " '" + pDTR.TipoResiduo + "', \n";
                if (pDTR.TotalKg > 0)
                    s = s + " " + pDTR.TotalKg.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                s = s + " '" + pDTR.LocalDTR + "', \n";
                if (pDTR.DataSaida == "")
                    s = s + "'0001-01-01', ";
                else
                    s = s + "'" + Convert.ToDateTime(pDTR.DataSaida).ToString("yyyy-MM-dd") + "', ";
                if (pDTR.CodigoLocalEntrega > 0)
                    s = s + " " + pDTR.CodigoLocalEntrega.ToString() + ", \n";
                else
                    s = s + "0, \n";
                s = s + " '" + pDTR.LocalEntrega + "', \n";
                if (pDTR.Fechado > 0)
                    s = s + " " + pDTR.Fechado.ToString() + ", \n";
                else
                    s = s + "0, \n";
                if (pDTR.Imprimido > 0)
                    s = s + " " + pDTR.Imprimido.ToString() + ", \n";
                else
                    s = s + "0, \n";
                if (pDTR.NumeroMTR > 0)
                    s = s + " " + pDTR.NumeroMTR.ToString() + ", \n";
                else
                    s = s + "0, \n";
                s = s + " '" + pDTR.HoraSaida + "', \n";
                if (pDTR.CodigoMotorista > 0)
                    s = s + " " + pDTR.CodigoMotorista.ToString() + ", \n";
                else
                    s = s + "0, \n";
                if (pDTR.CodigoCaminhao > 0)
                    s = s + " " + pDTR.CodigoCaminhao.ToString() + ", \n";
                else
                    s = s + "0, \n";
                if (pDTR.NumeroImpressao > 0)
                    s = s + " " + pDTR.NumeroImpressao.ToString() + ", \n";
                else
                    s = s + "0, \n"; 
                if (pDTR.CodigoClienteEmitiuMTR > 0)
                    s = s + " " + pDTR.CodigoClienteEmitiuMTR.ToString() + ", \n";
                else
                    s = s + "0, \n"; 
                if (pDTR.QtNova > 0)
                    s = s + " " + pDTR.QtNova.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pDTR.TotalQtNova > 0)
                    s = s + " " + pDTR.TotalQtNova.ToString().Replace(",", ".") + " \n";
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
        public string Alterar(clsDTR pDTR, int pSequencial, int pNumeroLancamento, int pCodigoResiduo, int pNumeroMTR)
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
                s = s + "update DTR \n";

                if (pDTR.NumeroLancamento > 0)
                    s = s + " set NumeroLancamento = " + pDTR.NumeroLancamento.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " set NumeroLancamento = 0, \n";
                if (pDTR.DataColeta == "")
                    s = s + " DataColeta = '0001-01-01', ";
                else
                    s = s + " DataColeta = '" + Convert.ToDateTime(pDTR.DataColeta).ToString("yyyy-MM-dd") + "', ";
                if (pDTR.CodigoClienteColetado > 0)
                    s = s + " CodigoClienteColetado = " + pDTR.CodigoClienteColetado.ToString() + ", \n";
                else
                    s = s + " CodigoClienteColetado = 0, \n";
                s = s + " ClienteColetado = '" + pDTR.ClienteColetado + "', \n";
                if (pDTR.CodigoTipoResiduo > 0)
                    s = s + " CodigoTipoResiduo = " + pDTR.CodigoTipoResiduo.ToString() + ", \n";
                else
                    s = s + " CodigoTipoResiduo = 0, \n";
                s = s + " TipoResiduo = '" + pDTR.TipoResiduo + "', \n";
                if (pDTR.TotalKg > 0)
                    s = s + " TotalKg = " + pDTR.TotalKg.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " TotalKg = 0, \n";
                s = s + " LocalDTR = '" + pDTR.LocalDTR + "', \n";
                if (pDTR.DataSaida == "")
                    s = s + " DataSaida = '0001-01-01', ";
                else
                    s = s + " DataSaida = '" + Convert.ToDateTime(pDTR.DataSaida).ToString("yyyy-MM-dd") + "', ";
                if (pDTR.CodigoLocalEntrega > 0)
                    s = s + " CodigoLocalEntrega = " + pDTR.CodigoLocalEntrega.ToString() + ", \n";
                else
                    s = s + " CodigoLocalEntrega = 0, \n";
                s = s + " LocalEntrega = '" + pDTR.LocalEntrega + "', \n";
                if (pDTR.Fechado > 0)
                    s = s + " Fechado = " + pDTR.Fechado.ToString() + ", \n";
                else
                    s = s + " Fechado = 0, \n";
                if (pDTR.Imprimido > 0)
                    s = s + " Imprimido = " + pDTR.Imprimido.ToString() + ", \n";
                else
                    s = s + " Imprimido = 0, \n";
                if (pDTR.NumeroMTR > 0)
                    s = s + " NumeroMTR = " + pDTR.NumeroMTR.ToString() + ", \n";
                else
                    s = s + " NumeroMTR = 0, \n";
                s = s + " HoraSaida = '" + pDTR.HoraSaida + "', \n";
                if (pDTR.CodigoMotorista > 0)
                    s = s + " CodigoMotorista = " + pDTR.CodigoMotorista.ToString() + ", \n";
                else
                    s = s + " CodigoMotorista = 0, \n";
                if (pDTR.CodigoCaminhao > 0)
                    s = s + " CodigoCaminhao = " + pDTR.CodigoCaminhao.ToString() + ", \n";
                else
                    s = s + " CodigoCaminhao = 0, \n";
                if (pDTR.NumeroImpressao > 0)
                    s = s + " NumeroImpressao = " + pDTR.NumeroImpressao.ToString() + ", \n";
                else
                    s = s + " NumeroImpressao = 0, \n"; 
                if (pDTR.CodigoClienteEmitiuMTR > 0)
                    s = s + " CodigoClienteEmitiuMTR = " + pDTR.CodigoClienteEmitiuMTR.ToString() + ", \n";
                else
                    s = s + " CodigoClienteEmitiuMTR = 0, \n";
                if (pDTR.QtNova > 0)
                    s = s + " QtNova = " + pDTR.QtNova.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " QtNova = 0, \n";
                if (pDTR.TotalQtNova > 0)
                    s = s + " TotalQtNova = " + pDTR.TotalQtNova.ToString().Replace(",", ".") + " \n";
                else
                    s = s + " TotalQtNova = 0 \n";

                s = s + "where Sequencial = " + pSequencial.ToString() + " \n";
                s = s + "and   NumeroLancamento = " + pNumeroLancamento.ToString() + " \n";
                s = s + "and   NumeroMTR = " + pNumeroMTR.ToString() + " \n";
                s = s + "and   CodigoTipoResiduo = " + pCodigoResiduo.ToString() + " \n";
                
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

        public string AlterarArmazenados(clsDTR pDTR, int pSequencial, int pNumeroLancamento, int pNumeroMTR, int pCodigoResiduo)
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
                s = s + "update DTR \n";
                if (pDTR.DataSaida == "")
                    s = s + " set DataSaida = '0001-01-01', ";
                else
                    s = s + " set DataSaida = '" + Convert.ToDateTime(pDTR.DataSaida).ToString("yyyy-MM-dd") + "', ";
                
                if (pDTR.CodigoLocalEntrega > 0)
                    s = s + " CodigoLocalEntrega = " + pDTR.CodigoLocalEntrega + ", \n";
                else
                    s = s + " CodigoLocalEntrega = 0, \n";
                s = s + " Imprimido = " + pDTR.Imprimido + ", \n";
                s = s + " LocalEntrega = '" + pDTR.LocalEntrega + "' \n";

                s = s + "where Sequencial = " + pSequencial.ToString() + " \n";
                s = s + "and   NumeroLancamento = " + pNumeroLancamento.ToString() + " \n";
                s = s + "and   NumeroMTR = " + pNumeroMTR.ToString() + " \n";
                s = s + "and   CodigoTipoResiduo = " + pCodigoResiduo.ToString() + " \n";

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
        public string SalvarComoImprimido(int pSequencial, int pNumeroLancamento, int pCodigoResiduo, int pCodigoCaminhao, int pCodigoMotorista, int pNumeroImpressao)
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
                    s = s + "update DTR \n";
                    s = s + "set    Imprimido  = 1, \n";
                    s = s + "       CodigoCaminhao = " + pCodigoCaminhao + ", \n";
                    s = s + "       CodigoMotorista = " + pCodigoMotorista + ", \n";
                    s = s + "       NumeroImpressao = " + pNumeroImpressao + " \n";
                    s = s + "where  Sequencial = " + pSequencial.ToString() + " \n";
                    s = s + "and    NumeroLancamento = " + pNumeroLancamento.ToString() + " \n";
                    s = s + "and    CodigoTipoResiduo = " + pCodigoResiduo.ToString() + " \n";
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
        public string SalvarComoFechado(int pSequencial, int pNumeroLancamento, int pCodigoResiduo, string pLocalEntrega, string pCodigoDestinoFinal, string pNumeroMTR)
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
                    s = s + "update DTR \n";
                    s = s + "set    Fechado  = 1, \n";
                    s = s + "       Sequencial = " + pSequencial.ToString() + " \n";
                    s = s + "where  NumeroLancamento = " + pNumeroLancamento.ToString() + " \n";
                    s = s + "and    NumeroMTR = " + pNumeroMTR.ToString() + " \n";
                    s = s + "and    CodigoTipoResiduo = " + pCodigoResiduo.ToString() + " \n";
                    s = s + "and    Fechado = 0 \n";
                    command.CommandText = s;
                    command.ExecuteNonQuery();

                    string s2 = "";
                    s2 = s2 + "update LancamentoMTR \n";
                    s2 = s2 + "set    Deposito = '" + geral.Left(pCodigoDestinoFinal + " - " + pLocalEntrega, 15) + "' \n";
                    s2 = s2 + "where  NumeroLancamento = " + pNumeroLancamento + " \n";
                    s2 = s2 + "and    CodigoResiduo = " + pCodigoResiduo + " \n";
                    s2 = s2 + "and    NumeroMTR = " + pNumeroMTR + " \n";
                    s2 = s2 + "and    left(Deposito, 3) = 'DTR' \n";
                    command.CommandText = s2;
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
        public string SalvarComoAberto(string pSequencial, string pNumeroLancamento, string pCodigoResiduo)
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
                if (pSequencial != "")
                {
                    s = s + "update DTR \n";
                    s = s + "set    Fechado = 0 \n";
                    s = s + "where  Sequencial = " + pSequencial + " \n";
                    s = s + "and    NumeroLancamento = " + pNumeroLancamento + " \n";
                    s = s + "and    CodigoTipoResiduo = " + pCodigoResiduo + " \n";
                    s = s + "and    Fechado = 1 \n";
                    command.CommandText = s;
                    command.ExecuteNonQuery();

                    string s2 = "";
                    s2 = s2 + "update LancamentoMTR \n";
                    s2 = s2 + "set    Deposito = 'DTR-' \n";
                    s2 = s2 + "where  NumeroLancamento = " + pNumeroLancamento + " \n";
                    s2 = s2 + "and    CodigoResiduo    = " + pCodigoResiduo + " \n";
                    s2 = s2 + "and    left(Deposito, 3) <> 'DTR-' \n";
                    command.CommandText = s2;
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
        public DataTable PreencheDataTableEnviados(string pOrdem, int pSequencial, string DataInicial, string DataFinal)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select d.Sequencial, d.NumeroLancamento, d.DataColeta, d.CodigoClienteColetado, d.ClienteColetado as NomeCliente, \n";
                s = s + "       d.CodigoTipoResiduo, d.TipoResiduo as Residuo, d.TotalKg as Quantidade, d.LocalDTR, d.DataSaida, d.CodigoLocalEntrega, \n";
                s = s + "       d.LocalEntrega as DestinoFinal, d.Fechado, d.Imprimido, d.NumeroMTR, d.HoraSaida, d.CodigoMotorista, \n";
                s = s + "       concat(d.NumeroLancamento, '-', d.NumeroMTR,'-', CodigoTipoResiduo, '-', CodigoClienteColetado, '-', 0) as Lote, d.CodigoCaminhao, d.NumeroImpressao, d.CodigoClienteEmitiuMTR, d.QtNova, d.TotalQtNova, \n";
                s = s + "       r.Unidade, m.Nome as NomeMotorista, ca.Placas \n";
                s = s + "from   DTR d \n";
                s = s + "left   join Residuos r on r.Codigo = d.CodigoTipoResiduo \n ";
                s = s + "left   join Funcionarios m on  m.Codigo = d.CodigoMotorista \n ";
                s = s + "left   join Caminhoes   ca on ca.Codigo = d.CodigoCaminhao \n ";
                s = s + "where  d.DataSaida <> '0100-01-01' and not d.DataSaida is null \n ";
                s = s + "and    Fechado = 1 AND Imprimido = 1 \n";
                if (DataInicial.Length > 0)
                {
                    s = s + "and   d.DataSaida >= '" + Convert.ToDateTime(DataInicial).ToString("yyyy-MM-dd") + "' \n";
                    s = s + "and   d.DataSaida <= '" + Convert.ToDateTime(DataFinal).ToString("yyyy-MM-dd") + "' \n";
                    s = s + "order by " + pOrdem;
                }
                else if (pSequencial > 0)
                {
                    s = s + "and  d.Sequencial = " + pSequencial + " and Fechado = 1 AND Imprimido = 1 \n";
                    s = s + "order by " + pOrdem;
                }
                else if (pSequencial == 0)
                {
                    s = s + "and    d.Sequencial = (SELECT Sequencial FROM DTR WHERE Fechado = 1 AND Imprimido = 1 ORDER BY Sequencial DESC limit 1) \n";
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

        public DataTable PreencheDataTableEnviados(string pOrdem, string pFiltro, string pCampo, int pSequencial)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select d.Sequencial, d.NumeroLancamento, d.DataColeta, d.CodigoClienteColetado, d.ClienteColetado as NomeCliente, \n";
                s = s + "       d.CodigoTipoResiduo, d.TipoResiduo as Residuo, d.TotalKg as Quantidade, d.LocalDTR, d.DataSaida, d.CodigoLocalEntrega, \n";
                s = s + "       d.LocalEntrega as DestinoFinal, d.Fechado, d.Imprimido, d.NumeroMTR, d.HoraSaida, d.CodigoMotorista, \n";
                s = s + "       concat(d.NumeroLancamento, '-', d.NumeroMTR,'-', CodigoTipoResiduo, '-', CodigoClienteColetado, '-', 0) as Lote, d.CodigoCaminhao, d.NumeroImpressao, d.CodigoClienteEmitiuMTR, d.QtNova, d.TotalQtNova, \n";
                s = s + "       r.Unidade, m.Nome as NomeMotorista, ca.Placas \n";
                s = s + "from   DTR d \n";
                s = s + "inner  join Residuos r on r.Codigo = d.CodigoTipoResiduo \n ";
                s = s + "inner  join Funcionarios m on  m.Codigo = d.CodigoMotorista \n ";
                s = s + "inner  join Caminhoes   ca on ca.Codigo = d.CodigoCaminhao \n ";
                s = s + "where  d.DataSaida <> '0100-01-01' and not d.DataSaida is null \n ";
                s = s + "and    Fechado = 1 AND Imprimido = 1 \n";
                if (pSequencial > 0)
                {
                    s = s + "and  d.Sequencial = " + pSequencial + " and Fechado = 1 AND Imprimido = 1 \n";
                }
                else if (pSequencial == 0)
                {
                    s = s + "and    d.Sequencial = (SELECT Sequencial FROM DTR WHERE Fechado = 1 AND Imprimido = 1 ORDER BY Sequencial DESC limit 1) \n";
                }
                s = s + "and    " + pCampo + " like '%" + pFiltro + "%' \n";
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

        public int UltimoRegistro()
        {
            int iRet = 0;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial \n";
                s = s + "from   DTR  \n";
                s = s + "where  Fechado = 1 and Imprimido = 1 order by Sequencial Desc limit 1 \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                DesconectaBanco();
                iRet = Convert.ToInt32(l_ds.Tables[0].Rows[0]["Sequencial"]);
            }
            catch (Exception ex)
            {
                DesconectaBanco();
                iRet = 0;
            }
            return iRet;
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
                    s = s + "delete from DTR ";
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

        public string ExcluirAno(string pAno)
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
                if (pAno != "")
                {
                    s = "";
                    s = s + "delete from DTR ";
                    s = s + "where  year(DataColeta) = 20" + pAno;
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

        private string UniaoMovimentacaoDTR(string DataInicial, bool pSemMovimentacao, string pCodigoResiduo)
        {
            string sRet = "";
            sRet = sRet + "  select 0 as Sequencial, concat(l.NumeroLancamento, '-', lmtr.NumeroMTR,'-', lmtr.CodigoResiduo, '-', l.CodigoCliente, '-', 0) as Lote, lmtr.NumeroMTR, l.DataRetirada as DataColeta, '01-01-0100 00:00:00' as DataSaida, l.CodigoCliente, c.NomeFantasia as NomeCliente, \n ";
            sRet = sRet + "         concat((select substr(DescricaoReduzida, 1, 40) from Residuos where Codigo = r.CodigoGrupoResiduo limit 1), '/',  r.DescricaoReduzida) as Residuo, ";
            sRet = sRet + "         lmtr.Quantidade, r.Unidade, '' as DestinoFinal, l.NumeroLancamento, \n";
            if (pSemMovimentacao)
            {
                sRet = sRet + "         replace(Deposito, 'DTR-', '') as LocalDTR, \n ";
            }
            else
            {
                sRet = sRet + "  (select   MoviCxPara ";
                sRet = sRet + "   from     MovimentacaoDTR \n";
                sRet = sRet + "   where    NumeroLancamento  = lmtr.NumeroLancamento " + " \n";
                sRet = sRet + "   and      CodigoResiduo     = lmtr.CodigoResiduo" + " \n";
                sRet = sRet + "   and      CodigoCliente     = l.CodigoCliente" + " \n";
                sRet = sRet + "   and      year(Data)       >= year(l.DataRetirada)" + " \n";
                sRet = sRet + "   order by Codigo desc limit 1) as LocalDTR, \n ";
            }
            sRet = sRet + "  '0' as Imprimido, r.Codigo as CodigoResiduo, lmtr.DescargaMTRe as DestinoMTRe \n ";
            sRet = sRet + "  from LancamentoMTR lmtr \n";
            sRet = sRet + "  left join Lancamentos l on lmtr.NumeroLancamento = l.NumeroLancamento \n ";
            sRet = sRet + "  left join Clientes c on l.CodigoCliente = c.Codigo \n ";
            sRet = sRet + "  left join Residuos r on lmtr.CodigoResiduo = r.Codigo \n ";
            sRet = sRet + "  where left(lmtr.Deposito, 3) = 'DTR'  \n";
            sRet = sRet + "  and   not exists(select * from DTR \n ";
            sRet = sRet + "                   where CodigoTipoResiduo = lmtr.CodigoResiduo \n ";
            sRet = sRet + "                   and   NumeroLancamento  = lmtr.NumeroLancamento \n ";
            sRet = sRet + "                   and   NumeroMTR = lmtr.NumeroMTR) \n ";
            if (pSemMovimentacao)
            {
                sRet = sRet + "  and   left(deposito, 4) = 'DTR-' \n";
                sRet = sRet + "  and   not exists (select   * ";
                sRet = sRet + "                    from     MovimentacaoDTR \n";
                sRet = sRet + "                    where    NumeroLancamento  = lmtr.NumeroLancamento " + " \n";
                sRet = sRet + "                    and      CodigoResiduo     = lmtr.CodigoResiduo" + " \n";
                sRet = sRet + "                    and      CodigoCliente     = l.CodigoCliente" + " \n";
                sRet = sRet + "                    and      year(Data)       >= year(l.DataRetirada)" + " \n";
                sRet = sRet + "                    order by Codigo desc) \n";
            }
            else
            {
                sRet = sRet + "  and   left(deposito, 4) = 'DTR-' \n";
                sRet = sRet + "  and   exists (select   * ";
                sRet = sRet + "                from     MovimentacaoDTR \n";
                sRet = sRet + "                where    NumeroLancamento  = lmtr.NumeroLancamento " + " \n";
                sRet = sRet + "                and      CodigoResiduo     = lmtr.CodigoResiduo" + " \n";
                sRet = sRet + "                and      CodigoCliente     = l.CodigoCliente" + " \n";
                sRet = sRet + "                and      year(Data)       >= year(l.DataRetirada)" + " \n";
                sRet = sRet + "                order by Codigo desc) \n";
            }
            if (DataInicial.Length > 0)
            {
                sRet = sRet + "  and   l.DataRetirada >= '" + DataInicial + "' \n";
            }
            if (pCodigoResiduo != "")
                sRet = sRet + "and   r.Codigo = " + pCodigoResiduo + " \n";
            return sRet;
        }

        private string UniaoMovimentacaoDTR2(string DataInicial, bool pSemMovimentacao, int ur, string pCodigoResiduo)
        {
            string sRet2 = "";
            sRet2 = sRet2 + "  select d.Sequencial, Concat(d.NumeroLancamento,'-', d.NumeroMTR,'-', CodigoTipoResiduo, '-', d.CodigoClienteColetado, '-', d.Sequencial) as Lote, d.NumeroMTR, d.DataColeta, d.DataSaida, d.CodigoClienteColetado, d.ClienteColetado as NomeCliente, \n";
            sRet2 = sRet2 + "         concat((select substr(DescricaoReduzida, 1, 40) from Residuos where Codigo = r.CodigoGrupoResiduo limit 1), '/',  r.DescricaoReduzida) as Residuo, ";
            sRet2 = sRet2 + "         d.TotalKg as Quantidade, r.Unidade, d.LocalEntrega as DestinoFinal, d.NumeroLancamento,  \n";
            if (pSemMovimentacao)
            {
                sRet2 = sRet2 + " LocalDTR, \n ";
            }
            else
            {
                sRet2 = sRet2 + "  (select   MoviCxPara ";
                sRet2 = sRet2 + "   from     MovimentacaoDTR \n";
                sRet2 = sRet2 + "   where    NumeroLancamento  = d.NumeroLancamento " + " \n";
                sRet2 = sRet2 + "   and      CodigoResiduo     = d.CodigoTipoResiduo" + " \n";
                sRet2 = sRet2 + "   and      CodigoCliente     = d.CodigoClienteColetado" + " \n";
                sRet2 = sRet2 + "   and      year(Data)       >= year(d.DataColeta)" + " \n";
                sRet2 = sRet2 + "   order by Codigo desc limit 1) as LocalDTR, \n ";
            }
            sRet2 = sRet2 + "  Imprimido, r.Codigo as CodigoResiduo, '' as DestinoMTRe \n ";

            sRet2 = sRet2 + "  from   DTR d \n";
            sRet2 = sRet2 + "  inner  join Residuos r on r.Codigo = d.CodigoTipoResiduo \n ";
            //sRet2 = sRet2 + "  where  d.Sequencial = " + (ur + 1) + " \n ";
            sRet2 = sRet2 + "  where    d.Fechado = 0 \n ";
            if (DataInicial.Length > 0)
            {
                sRet2 = sRet2 + "  and   d.DataColeta >= '" + DataInicial + "' \n";
            }
            sRet2 = sRet2 + "  and    exists(select * from lancamentoMTR \n ";
            sRet2 = sRet2 + "                where left(Deposito, 3) = 'DTR' \n ";
            sRet2 = sRet2 + "                and   CodigoResiduo = d.CodigoTipoResiduo \n ";
            sRet2 = sRet2 + "                and   NumeroLancamento = d.NumeroLancamento  \n ";
            sRet2 = sRet2 + "                and   NumeroMTR = d.NumeroMTR)  \n ";
            if (pSemMovimentacao)
            {
                sRet2 = sRet2 + "  and   not exists (select   * ";
                sRet2 = sRet2 + "                    from     MovimentacaoDTR \n";
                sRet2 = sRet2 + "                    where    NumeroLancamento  = d.NumeroLancamento " + " \n";
                sRet2 = sRet2 + "                    and      CodigoResiduo     = d.CodigoTipoResiduo" + " \n";
                sRet2 = sRet2 + "                    and      CodigoCliente     = d.CodigoClienteColetado" + " \n";
                sRet2 = sRet2 + "                    and      year(Data)       >= year(d.DataColeta)" + " \n";
                sRet2 = sRet2 + "                    order by Codigo desc) \n";
            }
            else
            {
                sRet2 = sRet2 + "  and   exists (select   * ";
                sRet2 = sRet2 + "                from     MovimentacaoDTR \n";
                sRet2 = sRet2 + "                where    NumeroLancamento  = d.NumeroLancamento " + " \n";
                sRet2 = sRet2 + "                and      CodigoResiduo     = d.CodigoTipoResiduo" + " \n";
                sRet2 = sRet2 + "                and      CodigoCliente     = d.CodigoClienteColetado" + " \n";
                sRet2 = sRet2 + "                and      year(Data)       >= year(d.DataColeta)" + " \n";
                sRet2 = sRet2 + "                order by Codigo desc) \n";
            }
            if (DataInicial.Length > 0)
            {
                sRet2 = sRet2 + "  and   d.DataColeta >= '" + DataInicial + "' \n";
            }
            if (pCodigoResiduo != "")
                sRet2 = sRet2 + "and   r.Codigo = " + pCodigoResiduo + " \n";

            return sRet2;
        }

        public DataTable PegaDadosArmazenados(clsDTR pDTR, int pSequencial, bool pUltimoRegistro, string DataInicial, string pOrdem, string pCodigoResiduo = "")
        {
            int ur = UltimoRegistro();

            ConectaBanco();

            s = "";
            s = s + "select * from ( \n ";

            s = s + UniaoMovimentacaoDTR(DataInicial, true, pCodigoResiduo);

            s = s + "union all \n ";
            s = s + UniaoMovimentacaoDTR(DataInicial, false, pCodigoResiduo);

            s = s + "union all \n ";
            s = s + UniaoMovimentacaoDTR2(DataInicial, true, ur, pCodigoResiduo);

            s = s + "union all \n ";
            s = s + UniaoMovimentacaoDTR2(DataInicial, false, ur, pCodigoResiduo);

            s = s + ")  x \n ";
            s = s + "where not DataColeta is null \n";
            if (pCodigoResiduo != "")
                s = s + "and   CodigoResiduo = " + pCodigoResiduo + " \n";
            if (pOrdem == "" || pOrdem.IndexOf("DataColeta") > -1)
                s = s + "order by  " + pOrdem + ", NumeroLancamento \n";
            else
                s = s + "order by  " + pOrdem;
            try
            {
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
        public DataTable PegaDadosArmazenados(string pSequencial, string pCodigoResiduo, string pOrder)
        {
            int ur = UltimoRegistro();

            ConectaBanco();
            s = "";
            s = s + "select d.Sequencial, Concat(d.NumeroLancamento,'-',d.NumeroMTR,'-',CodigoTipoResiduo) as Lote, d.NumeroMTR, d.DataColeta, d.DataSaida, d.CodigoClienteColetado, d.ClienteColetado as NomeCliente, \n";
            s = s + "       d.TipoResiduo as Residuo, d.TotalKg as Quantidade, r.Unidade, d.LocalEntrega as DestinoFinal \n";
            s = s + "from   DTR d \n";
            s = s + "inner  join Residuos r on r.Codigo = d.CodigoTipoResiduo \n ";
            s = s + "where  not DataColeta is null\n";
            s = s + "and    d.Sequencial = " + pSequencial + " \n";
            s = s + "and    d.CodigoTipoResiduo = " + pCodigoResiduo + " \n";
            s = s + "and    d.Fechado = 1 \n ";
            s = s + "order by  " + pOrder + " \n";
            try
            {
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
        public DataTable PreencheDataTableArmazenados(string pOrdem, int pSequencial, string DataInicial, string DataFinal = "")
        {
            try
            {
                int ur = UltimoRegistro();

                ConectaBanco();
                s = "";
                s = s + "select * from ( \n ";
                s = s + "  select " + (ur + 1) + " as Sequencial, concat(l.NumeroLancamento,'-',lmtr.NumeroMTR,'-',lmtr.CodigoResiduo) as Lote, \n ";
                s = s + "         lmtr.NumeroMTR, l.DataRetirada as DataColeta, '01-01-0100 00:00:00' as DataSaida, l.CodigoCliente, c.Nome as NomeCliente, \n ";
                s = s + "         r.DescricaoReduzida as Residuo, lmtr.Quantidade, r.Unidade, '' as DestinoFinal, l.NumeroLancamento \n ";
                s = s + "  from LancamentoMTR lmtr \n";
                s = s + "  left join Lancamentos l on lmtr.NumeroLancamento = l.NumeroLancamento \n ";
                s = s + "  left join Clientes c on l.CodigoCliente = c.Codigo \n ";
                s = s + "  left join Residuos r on lmtr.CodigoResiduo = r.Codigo \n ";
                s = s + "  where left(lmtr.Deposito, 3) = 'DTR'  \n";
                s = s + "  and   not exists(select * from DTR \n ";
                s = s + "                   where CodigoTipoResiduo = lmtr.CodigoResiduo \n ";
                s = s + "                   and   NumeroLancamento = lmtr.NumeroLancamento \n ";
                s = s + "                   and   NumeroMTR = lmtr.NumeroMTR) \n ";
                if (DataInicial.Length > 0)
                {
                    s = s + "and   l.DataRetirada >= '" + Convert.ToDateTime(DataInicial).ToString("yyyy-MM-dd") + "' \n";
                    if (DataFinal.Length > 0)
                        s = s + "and   l.DataRetirada <= '" + Convert.ToDateTime(DataFinal).ToString("yyyy-MM-dd") + "' \n";
                }

                s = s + "union all \n ";

                s = s + "  select d.Sequencial, Concat(d.NumeroLancamento,'-',d.NumeroMTR,'-',CodigoTipoResiduo) as Lote, d.NumeroMTR, d.DataColeta, d.DataSaida, d.CodigoClienteColetado, d.ClienteColetado as NomeCliente, \n";
                s = s + "         d.TipoResiduo as Residuo, d.TotalKg as Quantidade, r.Unidade, d.LocalEntrega, d.NumeroLancamento \n";
                s = s + "  from   DTR d \n";
                s = s + "  inner  join Residuos r on r.Codigo = d.CodigoTipoResiduo \n ";
                s = s + "  where  d.Sequencial = " + (ur + 1) + " \n ";
                s = s + "  and    (d.DataSaida = '0100-01-01' or d.DataSaida is null) \n ";
                s = s + "  and    exists(select * from lancamentoMTR \n ";
                s = s + "                where left(Deposito, 3) = 'DTR' \n ";
                s = s + "                and   CodigoResiduo = d.CodigoTipoResiduo \n ";
                s = s + "                and   NumeroLancamento = d.NumeroLancamento  \n ";
                s = s + "                and   NumeroMTR = d.NumeroMTR)  \n ";
                if (DataInicial.Length > 0)
                {
                    s = s + "and   d.DataColeta >= '" + Convert.ToDateTime(DataInicial).ToString("yyyy-MM-dd") + "' \n";
                    if (DataFinal.Length > 0)
                        s = s + "and   d.DataColeta <= '" + Convert.ToDateTime(DataFinal).ToString("yyyy-MM-dd") + "' \n";
                }
                else if (pSequencial > 0)
                {
                    s = s + "and  d.Sequencial = " + pSequencial + " and Fechado = 0 \n";
                }
                s = s + ")  x \n ";
                s = s + "where not DataColeta is null \n";
                if (pOrdem == "DataColeta")
                    s = s + "order by  " + pOrdem + ", NumeroLancamento \n";
                else
                    s = s + "order by " + pOrdem + " \n";
                
                //s = s + "limit 300 \n ";
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
        public string Excluir(int pSequencial, int pNumeroLancamento, int pNumeroMTR, int pCodigoResiduo)
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
                    s = s + "delete from DTR ";
                    s = s + "where  Sequencial = " + pSequencial.ToString() + " \n";
                    s = s + "and    NumeroLancamento = " + pNumeroLancamento.ToString() + " \n";
                    s = s + "and    NumeroMTR = " + pNumeroMTR.ToString() + " \n";
                    s = s + "and    CodigoTipoResiduo = " + pCodigoResiduo.ToString() + " \n";
                }
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
                DevolverNoLancamentoMTRParaDTR(pNumeroLancamento, pNumeroMTR, pCodigoResiduo);
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
        public string DevolverNoLancamentoMTRParaDTR(int pNumeroLancamento, int pCodigoResiduo, int pNumeroMTR)
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
                s = s + "update LancamentoMTR \n";
                s = s + "set Deposito = 'DTR' \n";
                s = s + "where NumeroLancamento = " + pNumeroLancamento.ToString() + " \n";
                s = s + "and   NumeroMTR = " + pNumeroMTR.ToString() + " \n";
                s = s + "and   CodigoResiduo = " + pCodigoResiduo.ToString() + " \n";
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