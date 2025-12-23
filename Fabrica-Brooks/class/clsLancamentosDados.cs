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
using iTextSharp.text.pdf.codec;

namespace LibSILC
{
    public class clsLancamentosDados
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
        public clsLancamentos PegaDados(clsLancamentos pLancamentos, int pNumeroLancamento)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select NumeroLancamento, CodigoCliente, Data, CodigoEnderecoObra, NumeroCaixa, CodigoCaminhaoColoca, \n";
                s = s + "       DataColocacao, HorasColocacao, DataARetirar, HorasARetirar, DataRetirada, HoraRetirada, \n";
                s = s + "       CodigoCaminhoRetirada, CodigoMotoristaColocou, CodigoMotoristaRetirou, ValorLocacao, \n";
                s = s + "       OBS, NuLancColocacao, TipoOperacao, Horas \n";
                s = s + "from   Lancamentos \n";          
                if (pNumeroLancamento > 0)
                {
                    s = s + "where  NumeroLancamento = " + pNumeroLancamento + " \n";
                    s = s + "limit 1";
                }
                else if (pNumeroLancamento == 0)
                {
                    s = s + " order by NumeroLancamento limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows[0]["NumeroLancamento"].ToString() != "")
                        pLancamentos.NumeroLancamento = Convert.ToInt32(l_dt.Rows[0]["NumeroLancamento"]);
                    if (l_dt.Rows[0]["CodigoCliente"].ToString() != "")
                        pLancamentos.CodigoCliente = Convert.ToInt32(l_dt.Rows[0]["CodigoCliente"]);
                    if (l_dt.Rows[0]["Data"].ToString() != "")
                        pLancamentos.Data = Convert.ToDateTime(l_dt.Rows[0]["Data"].ToString()).Date.ToShortDateString();
                    if (l_dt.Rows[0]["CodigoEnderecoObra"].ToString() != "")
                        pLancamentos.CodigoEnderecoObra = Convert.ToInt32(l_dt.Rows[0]["CodigoEnderecoObra"]);
                    pLancamentos.NumeroCaixa = l_dt.Rows[0]["NumeroCaixa"].ToString();
                    if (l_dt.Rows[0]["CodigoCaminhaoColoca"].ToString() != "")
                        pLancamentos.CodigoCaminhaoColoca = Convert.ToInt32(l_dt.Rows[0]["CodigoCaminhaoColoca"]);
                    if (l_dt.Rows[0]["DataColocacao"].ToString() != "")
                        pLancamentos.DataColocacao = Convert.ToDateTime(l_dt.Rows[0]["DataColocacao"].ToString()).Date.ToShortDateString();
                    pLancamentos.HorasColocacao = l_dt.Rows[0]["HorasColocacao"].ToString();
                    if (l_dt.Rows[0]["DataARetirar"].ToString() != "")
                        pLancamentos.DataARetirar = Convert.ToDateTime(l_dt.Rows[0]["DataARetirar"].ToString()).Date.ToShortDateString();
                    pLancamentos.HorasARetirar = l_dt.Rows[0]["HorasARetirar"].ToString();                   
                    if (l_dt.Rows[0]["DataRetirada"].ToString() != "")
                        pLancamentos.DataRetirada = Convert.ToDateTime(l_dt.Rows[0]["DataRetirada"].ToString()).Date.ToShortDateString();
                    pLancamentos.HoraRetirada = l_dt.Rows[0]["HoraRetirada"].ToString();
                    if (l_dt.Rows[0]["CodigoCaminhoRetirada"].ToString() != "")
                        pLancamentos.CodigoCaminhoRetirada = Convert.ToInt32(l_dt.Rows[0]["CodigoCaminhoRetirada"]);
                    if (l_dt.Rows[0]["CodigoMotoristaColocou"].ToString() != "")
                        pLancamentos.CodigoMotoristaColocou = Convert.ToInt32(l_dt.Rows[0]["CodigoMotoristaColocou"]);
                    if (l_dt.Rows[0]["CodigoMotoristaRetirou"].ToString() != "")
                        pLancamentos.CodigoMotoristaRetirou = Convert.ToInt32(l_dt.Rows[0]["CodigoMotoristaRetirou"]);
                    if (l_dt.Rows[0]["ValorLocacao"].ToString() != "")
                        pLancamentos.ValorLocacao = Convert.ToDecimal(l_dt.Rows[0]["ValorLocacao"]);
                    pLancamentos.OBS = l_dt.Rows[0]["OBS"].ToString();
                    if (l_dt.Rows[0]["NuLancColocacao"].ToString() != "")
                        pLancamentos.NuLancColocacao = Convert.ToInt32(l_dt.Rows[0]["NuLancColocacao"]);
                    if (l_dt.Rows[0]["TipoOperacao"].ToString() != "")
                        pLancamentos.TipoOperacao = Convert.ToInt32(l_dt.Rows[0]["TipoOperacao"]);
                    if (l_dt.Rows[0]["Horas"].ToString() != "")
                        pLancamentos.Horas = Convert.ToInt32(l_dt.Rows[0]["Horas"]);
                }
                
            }
            catch (Exception ex)
            {
                pLancamentos = new clsLancamentos();
            }
            finally
            {
                DesconectaBanco();
            }
            return pLancamentos;
        }
        
        public DataTable PegaDados(clsLancamentos pLancamentos, int pNumeroLancamento, bool pUltimoRegistro,
                                   int pCodigoCliente, string pDataInicial, bool pPegarUltimosDadosCliente)
        {
            try
            {
                int urCliente = 0;
                urCliente = UltimoRegistro(true);
                
                ConectaBanco();
                s = "";
                s = s + "select l.NumeroLancamento, c.NomeFantasia as NomeCliente, l.CodigoCliente, l.Data, l.CodigoEnderecoObra, \n";
                s = s + "       l.NumeroCaixa, l.CodigoCaminhaoColoca,l.DataColocacao, l.HorasColocacao, l.DataARetirar, l.HorasARetirar, \n";
                s = s + "       l.DataRetirada, l.HoraRetirada, l.CodigoCaminhoRetirada, l.CodigoMotoristaColocou, lmtr.NumeroMTR, "; 
                s = s + "       r.DescricaoReduzida as DescricaoResiduo, lmtr.Quantidade, lmtr.Unidade, \n";
                s = s + "       l.CodigoMotoristaRetirou, l.ValorLocacao, l.OBS as Observacao, l.NuLancColocacao, l.TipoOperacao, l.Horas \n"; 
                s = s + "from   Lancamentos l \n";
                s = s + "inner  join LancamentoMTR lmtr on lmtr.NumeroLancamento = l.NumeroLancamento \n";
                s = s + "inner  join Clientes c on c.Codigo = l.CodigoCliente \n ";
                s = s + "inner  join Residuos r on r.Codigo = lmtr.CodigoResiduo \n ";
                if (pNumeroLancamento > 0)
                {
                    s = s + "where  NumeroLancamento = " + pNumeroLancamento + " \n ";
                }
                else if (pCodigoCliente > 0)
                {
                    s = s + "where  CodigoCliente = " + pCodigoCliente + " \n ";
                }
                else if (pPegarUltimosDadosCliente)
                {
                    s = s + "where  CodigoCliente = " + urCliente + " \n";
                } 
                if (pDataInicial != "")
                    s = s + "and    l.Data >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n ";
                if (pUltimoRegistro)
                {
                    s = s + "order by Data desc  \n ";
                }
                else if (pNumeroLancamento == 0)
                {
                    s = s + "order by Data  \n ";
                }
                s = s + " limit 1000 \n";
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
                s = s + "from Lancamentos ";
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
        public string PegaNumeroLancamento(int pSequencialProgramacao)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                if (pSequencialProgramacao != 0)
                {
                    s = s + "select NumeroLancamento ";
                    s = s + "from   ProgramacaoLancamento ";
                    s = s + "where  SequencialProgramacao = " + pSequencialProgramacao + " ";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pSequencialProgramacao != 0)
                    sRet = l_dt.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                sRet = "Erro: " + ex.Message;
            }
            finally
            {
                DesconectaBanco();
            }
            return sRet;
        }

        public DataTable PegaNumerosLancamentoProg(string pDataExecucao)
        {
            DataTable dtRet = new DataTable();
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select SequencialProgramacao, NumeroLancamento, CodigoCliente, Visto \n";
                s = s + "from   ProgramacaoLancamento \n";
                s = s + "where  DataExecucao = '" + Convert.ToDateTime(pDataExecucao).ToString("yyyy-MM-dd") + "'";
                FillDataSet();
                dtRet = l_dt;
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
            finally
            {
                DesconectaBanco();
            }
            return dtRet;
        }

        public int PegaNumeroLancamento(int pCodigoCliente, string pNomeFantasiaCliente)
        {
            int iRet = 0;
            try
            {
                oDB.ConectaMySql();
                if (pCodigoCliente != 0)
                {
                    s = "";
                    s = s + "select NumeroLancamento \n";
                    s = s + "from   Lancamentos \n";
                    s = s + "where  CodigoCliente = " + pCodigoCliente + " \n";
                    s = s + "and    DataRetirada > '1900-01-01' \n";
                    s = s + "order  by NumeroLancamento desc";
                }
                else if (pNomeFantasiaCliente != "")
                {
                    s = "";
                    s = s + "select l.NumeroLancamento \n";
                    s = s + "from   Lancamentos l \n";
                    s = s + "inner  join Clientes c on c.Codigo = l.CodigoCliente \n";
                    s = s + "where  c.NomeFantasia = '" + pNomeFantasiaCliente + "' \n";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && (pCodigoCliente != 0 || pNomeFantasiaCliente != ""))
                    iRet = Convert.ToInt32(l_dt.Rows[0][0]);
            }
            catch (Exception ex)
            {
                iRet = -1;
            }
            finally
            {
                DesconectaBanco();
            }
            return iRet;
        }
        public string DadoExiste(int pNumeroLancamento)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select NumeroLancamento ";
                s = s + "from   Lancamentos ";
                if (pNumeroLancamento != 0)
                {
                    s = s + "where  NumeroLancamento = " + pNumeroLancamento + " ";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pNumeroLancamento != 0)
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
                DesconectaBanco();
            }
            return sRet;
        }
        public int Inserir(clsLancamentos pLancamentos, int pNumeroLancamento = 0)
        {
            int iRet = 0;
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            //try
            //{
                /*if (pNumeroLancamento == 0)
                {
                    s = "";
                    s = s + "select NumeroLancamento from Lancamentos order by NumeroLancamento desc limit 1";
                    FillDataSet();
                    pNumeroLancamento = Convert.ToInt32(l_ds.Tables[0].Rows[0][0]) + 1;
                }*/
                s = "";
                s = s + "insert into Lancamentos \n";
                s = s + "( \n";
                if (pNumeroLancamento > 0)
                    s = s + "NumeroLancamento, \n";
                s = s + "  CodigoCliente, Data, CodigoEnderecoObra, NumeroCaixa, CodigoCaminhaoColoca, \n";
                s = s + "  DataColocacao, HorasColocacao, DataARetirar, HorasARetirar, DataRetirada, HoraRetirada, \n";
                s = s + "  CodigoCaminhoRetirada, CodigoMotoristaColocou, CodigoMotoristaRetirou, ValorLocacao, \n";
                s = s + "  OBS, NuLancColocacao, TipoOperacao, Horas \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pNumeroLancamento > 0)
                    s = s + pNumeroLancamento + ", \n";

                if (pLancamentos.CodigoCliente > 0)
                    s = s + " " + pLancamentos.CodigoCliente.ToString() + ", \n";
                else
                    s = s + "0, \n";
                if (pLancamentos.Data != "")
                    s = s + "'" + Convert.ToDateTime(pLancamentos.Data).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                if (pLancamentos.CodigoEnderecoObra > 0)
                    s = s + " " + pLancamentos.CodigoEnderecoObra.ToString() + ", \n";
                else
                    s = s + "0, \n";
                s = s + " '" + pLancamentos.NumeroCaixa + "', \n";
                if (pLancamentos.CodigoCaminhaoColoca > 0)
                    s = s + " " + pLancamentos.CodigoCaminhaoColoca.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pLancamentos.DataColocacao != "")
                    s = s + "'" + Convert.ToDateTime(pLancamentos.DataColocacao).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                s = s + " '" + pLancamentos.HorasColocacao + "', \n";
                if (pLancamentos.DataARetirar != "")
                    s = s + "'" + Convert.ToDateTime(pLancamentos.DataARetirar).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                s = s + " '" + pLancamentos.HorasARetirar + "', \n";
                if (pLancamentos.DataRetirada != "" &&
                    pLancamentos.DataRetirada != "01/01/0001" &&
                    pLancamentos.DataRetirada != "01/01/0100" &&
                    pLancamentos.DataRetirada != "01/01/1900")
                    s = s + "'" + Convert.ToDateTime(pLancamentos.DataRetirada).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                s = s + " '" + pLancamentos.HoraRetirada + "', \n";
                if (pLancamentos.CodigoCaminhoRetirada > 0)
                    s = s + " " + pLancamentos.CodigoCaminhoRetirada.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pLancamentos.CodigoMotoristaColocou > 0)
                    s = s + " " + pLancamentos.CodigoMotoristaColocou.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pLancamentos.CodigoMotoristaRetirou > 0)
                    s = s + " " + pLancamentos.CodigoMotoristaRetirou.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pLancamentos.ValorLocacao > 0)
                    s = s + " " + pLancamentos.ValorLocacao.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                s = s + " '" + pLancamentos.OBS + "', \n";
                if (pLancamentos.NuLancColocacao > 0)
                    s = s + " " + pLancamentos.NuLancColocacao.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pLancamentos.TipoOperacao > 0)
                    s = s + " " + pLancamentos.TipoOperacao.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                s = s + " '" + pLancamentos.Horas + "' \n";

                s = s + ")";
                
                command.Connection = oDB.MySqlConnect;
                command.Transaction = transaction;
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
                s = "";
                s = s + "select NumeroLancamento from Lancamentos order by NumeroLancamento desc limit 1";
                FillDataSet();
                int r = Convert.ToInt32(l_ds.Tables[0].Rows[0][0]);
                iRet = r;
            //}
            //catch (Exception e)
            //{
            //    transaction.Rollback();
            //    iRet = 0;
            //}
            //finally
            //{
                DesconectaBanco();
            //}
            return iRet;
        }
        public string Alterar(clsLancamentos pLancamentos, int pNumeroLancamento)
        {
            string sRet = "";
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            //try
            //{
                s = "";
                s = s + "update Lancamentos \n";
                if (pLancamentos.CodigoCliente > 0)
                    s = s + " set CodigoCliente = " + pLancamentos.CodigoCliente.ToString() + ", \n";
                else
                    s = s + " set CodigoCliente = 0, \n";
                if (pLancamentos.Data != "")
                    s = s + " Data = '" + Convert.ToDateTime(pLancamentos.Data).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + " Data = '0001-01-01', \n";
                if (pLancamentos.CodigoEnderecoObra > 0)
                    s = s + " CodigoEnderecoObra = " + pLancamentos.CodigoEnderecoObra.ToString() + ", \n";
                else
                    s = s + " CodigoEnderecoObra = 0, \n";
                s = s + " NumeroCaixa = '" + pLancamentos.NumeroCaixa + "', \n";
                if (pLancamentos.CodigoCaminhaoColoca > 0)
                    s = s + " CodigoCaminhaoColoca = " + pLancamentos.CodigoCaminhaoColoca.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " CodigoCaminhaoColoca = 0, \n";
                if (pLancamentos.DataColocacao != "")
                    s = s + " DataColocacao = '" + Convert.ToDateTime(pLancamentos.DataColocacao).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + " DataColocacao = '0001-01-01', \n";
                if (pLancamentos.HorasColocacao != "" && pLancamentos.HorasColocacao != null)
                {
                    if (pLancamentos.HorasColocacao.Length >= 5)
                        s = s + " HorasColocacao = '" + pLancamentos.HorasColocacao.Substring(0, 5) + "', \n";
                }
                if (pLancamentos.DataARetirar != "")
                    s = s + " DataARetirar = '" + Convert.ToDateTime(pLancamentos.DataARetirar).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + " DataARetirar = '0001-01-01', \n";
                //s  = s + " HorasARetirar = '" + pLancamentos.HorasARetirar + "', \n";
                if (pLancamentos.DataRetirada != "")
                    s = s + " DataRetirada = '" + Convert.ToDateTime(pLancamentos.DataRetirada).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + " DataRetirada = '0001-01-01', \n";
                s = s + " HoraRetirada = '" + pLancamentos.HoraRetirada + "', \n";
                if (pLancamentos.CodigoCaminhoRetirada > 0)
                    s = s + " CodigoCaminhoRetirada = " + pLancamentos.CodigoCaminhoRetirada.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "CodigoCaminhoRetirada = 0, \n";
                if (pLancamentos.CodigoMotoristaColocou > 0)
                    s = s + " CodigoMotoristaColocou = " + pLancamentos.CodigoMotoristaColocou.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " CodigoMotoristaColocou = 0, \n";
                if (pLancamentos.CodigoMotoristaRetirou > 0)
                    s = s + " CodigoMotoristaRetirou = " + pLancamentos.CodigoMotoristaRetirou.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " CodigoMotoristaRetirou = 0, \n";
                if (pLancamentos.ValorLocacao > 0)
                    s = s + " ValorLocacao = " + pLancamentos.ValorLocacao.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " ValorLocacao = 0, \n";
                s = s + " OBS = '" + pLancamentos.OBS + "', \n";
                if (pLancamentos.NuLancColocacao > 0)
                    s = s + " NuLancColocacao = " + pLancamentos.NuLancColocacao.ToString() + ", \n";
                if (pLancamentos.TipoOperacao > 0)
                    s = s + " TipoOperacao = " + pLancamentos.TipoOperacao.ToString().Replace(",", ".") + ", \n";
                if (pLancamentos.Horas > 0)
                    s = s + " Horas = " + pLancamentos.Horas + " \n";
                else
                    s = s + " Horas = 0 \n";

                s = s + " where NumeroLancamento = " + pNumeroLancamento + " \n";

                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();

                sRet = string.Empty;
            //}
            //catch (Exception ex)
            //{
            //    sRet = ex.ToString();
            //    transaction.Rollback();
            //}
            //finally
            //{
            //    oDB.DesconectaMySql();
            //}
            return sRet;
        }
        public string SalvarNumeroTroca(int pNumeroLancamento, int pNumeroLancamentoQueColocou)
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
                s = s + "update Lancamentos \n";
                s = s + "set   TipoOperacao     = 3, \n";
                s = s + "      NuLancColocacao  = " + pNumeroLancamentoQueColocou + " \n";
                s = s + "where NumeroLancamento = " + pNumeroLancamento + " \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();

                sRet = string.Empty;
            }
            catch (Exception ex)
            {
                sRet = ex.ToString();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }
        public string InserirLancamentoProgramacao(int pNumeroLancamento, int pCodigoCliente, string pDataExecucao, int pSequencialProgramacao)
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
                s = s + "insert into ProgramacaoLancamento \n";
                s = s + "(CodigoCliente, DataExecucao, NumeroLancamento, SequencialProgramacao) \n";
                s = s + "values \n";
                s = s + "(" + pCodigoCliente + ", \n";
                s = s + "'" + Convert.ToDateTime(pDataExecucao).ToString("yyyy-MM-dd") + "',\n";
                s = s + " " + pNumeroLancamento +  ", \n";
                s = s + " " + pSequencialProgramacao + " \n";
                s = s + ") \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
                sRet = string.Empty;
            }
            catch (Exception ex)
            {
                sRet = ex.ToString();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }
        public string SalvarComoTroca(int pNumeroLancamento)
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
                s = s + "update Lancamentos \n";
                s = s + "set    TipoOperacao  = 3 \n";
                s = s + "where  NumeroLancamento = " + pNumeroLancamento + " \n";
                s = s + "and    TipoOperacao <> 3 \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();

                sRet = string.Empty;
            }
            catch (Exception ex)
            {
                sRet = ex.ToString();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }
        public string SalvarNumeroMTRe(int pNumeroLancamento, Int64 pNumeroMTRe, int pNumeroMTR, int pCodigoResiduo)
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
                s = s + "set    NumeroMTRFatima  = '" + pNumeroMTRe + "' \n";
                s = s + "where  NumeroLancamento = " + pNumeroLancamento + " \n";
                s = s + "and    NumeroMTR = '" + pNumeroMTR + "' \n";
                s = s + "and    CodigoResiduo = " + pCodigoResiduo + " \n";
                s = s + "and    NumeroMTRFatima = -1";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();

                sRet = string.Empty;
            }
            catch (Exception ex)
            {
                sRet = ex.ToString();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }
        public string SalvarNumeroTicketPendente(int pNumeroLancamento, string pTicket, int pNumeroMTR, int pCodigoResiduo, string pDeposito, int pCodigoCaminhao, string pObs)
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
                s = s + "set    Ticket  = '" + pTicket + "', \n";
                s = s + "       Observacao = '" + pObs + "', \n";
                s = s + "       Deposito = '" + geral.Left(pDeposito, 15) + "' \n";
                s = s + "where  NumeroLancamento = " + pNumeroLancamento + " \n";
                s = s + "and    NumeroMTR = '" + pNumeroMTR + "' \n";
                s = s + "and    CodigoResiduo = " + pCodigoResiduo + " \n";                
                s = s + "and    Observacao like 'FALTA TICKET PESO%'";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();

                sRet = string.Empty;
            }
            catch (Exception ex)
            {
                sRet = ex.ToString();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }
        public DataTable PreencheDataTable(string pOrdem, int pCodigoCliente, string pDataInicial, string pDataFinal)
        {
            try
            {
                ConectaBanco();
                s = s + "select l.NumeroLancamento, c.NomeFantasia as NomeCliente, l.CodigoCliente, l.Data, l.CodigoEnderecoObra, \n";
                s = s + "       l.NumeroCaixa, l.CodigoCaminhaoColoca,l.DataColocacao, l.HorasColocacao, l.DataARetirar, l.HorasARetirar, \n";
                s = s + "       l.DataRetirada, l.HoraRetirada, l.CodigoCaminhoRetirada, l.CodigoMotoristaColocou, lmtr.NumeroMTR, ";
                s = s + "       r.DescricaoReduzida as DescricaoResiduo, lmtr.Quantidade, lmtr.Unidade, \n";
                s = s + "       l.CodigoMotoristaRetirou, l.ValorLocacao, l.OBS as Observacao, l.NuLancColocacao, l.TipoOperacao, l.Horas \n";
                s = s + "from   Lancamentos l \n";
                s = s + "inner  join LancamentoMTR lmtr on lmtr.NumeroLancamento = l.NumeroLancamento \n";
                s = s + "inner  join Clientes c on c.Codigo = l.CodigoCliente \n ";
                s = s + "inner  join Residuos r on r.Codigo = lmtr.CodigoResiduo \n ";
                if (pCodigoCliente > 0)
                    s = s + "where  CodigoCliente = " + pCodigoCliente + " \n";
                if (pDataInicial != "")
                    s = s + "and    l.Data >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n ";
                if (pDataFinal != "")
                    s = s + "and    l.Data <= '" + Convert.ToDateTime(pDataFinal).ToString("yyyy-MM-dd") + "' \n ";                

                s = s + "order by " + pOrdem + " \n ";

                s = s + "limit 1000 \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                return l_dt;
            }
            finally 
            {
                DesconectaBanco();
            }
        }
        public DataTable PreencheDataTableTicketsPendentes(string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select distinct lmtr.Deposito, l.DataRetirada, lmtr.NumeroLancamento, \n";
                s = s + "                o.Modelo as Caminhao, f.Codigo as CodigoMotorista, f.Nome as Motorista, \n";
                s = s + "                l.NumeroCaixa, lmtr.Observacao, l.CodigoCliente, c.NomeFantasia, lmtr.CodigoResiduo, l.CodigoCaminhoRetirada, \n";
                s = s + "                lmtr.NumeroMTR, lmtr.Quantidade \n";
                s = s + "from   Lancamentos l \n";
                s = s + "inner  join LancamentoMTR lmtr on lmtr.NumeroLancamento = l.NumeroLancamento \n";
                s = s + "inner  join Caminhoes o on o.Codigo = l.CodigoCaminhoRetirada \n";
                s = s + "inner  join Clientes c on c.Codigo = l.CodigoCliente \n ";
                s = s + "inner  join Funcionarios f on f.Codigo = l.CodigoMotoristaRetirou \n ";
                s = s + "where  lmtr.Observacao like 'falta ticket peso%' \n";
                s = s + "and    l.DataRetirada >= '" + DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd") + "'";  
                s = s + "order  by " + pOrdem + " \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                return l_dt;
            }
            finally
            {
                DesconectaBanco();
            }
        }

        public DataTable PreencheDataTableMTRsPendentes(string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select distinct c.NomeFantasia, l.DataRetirada, lmtr.NumeroLancamento, \n";
                s = s + "                r.DescricaoReduzida as DescricaoResiduo, lmtr.Motivo, lmtr.NumeroMTR, \n";
                s = s + "                lmtr.NumeroMTRFatima, lmtr.CodigoResiduo, l.CodigoCliente, lmtr.Quantidade, \n ";
                s = s + "                (select Nome   from Funcionarios where Codigo = l.CodigoMotoristaRetirou limit 1) as Motorista, \n";
                s = s + "                (select Placas from Caminhoes    where Codigo = l.CodigoCaminhoRetirada limit 1) as Placas \n";
                s = s + "from   Lancamentos l \n";
                s = s + "inner  join LancamentoMTR lmtr on lmtr.NumeroLancamento = l.NumeroLancamento \n";
                s = s + "inner  join Clientes c on c.Codigo = l.CodigoCliente \n ";
                s = s + "inner  join Residuos r on r.Codigo = lmtr.CodigoResiduo \n ";
                s = s + "where  lmtr.NumeroMTRFatima = '-1' \n";
                s = s + "order  by " + pOrdem + " \n"; 
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                return l_dt;
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public DataTable PreencheDataTable(string pOrdem, string pFiltro, string pCampo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select l.NumeroLancamento, c.NomeFantasia as NomeCliente, l.CodigoCliente, l.Data, l.CodigoEnderecoObra, \n";
                s = s + "       l.NumeroCaixa, l.CodigoCaminhaoColoca,l.DataColocacao, l.HorasColocacao, l.DataARetirar, l.HorasARetirar, \n";
                s = s + "       l.DataRetirada, l.HoraRetirada, l.CodigoCaminhoRetirada, l.CodigoMotoristaColocou, lmtr.NumeroMTR, ";
                s = s + "       r.DescricaoReduzida as DescricaoResiduo, lmtr.Quantidade, lmtr.Unidade, \n";
                s = s + "       l.CodigoMotoristaRetirou, l.ValorLocacao, l.OBS as Observacao, l.NuLancColocacao, l.TipoOperacao, l.Horas \n";
                s = s + "from   Lancamentos l \n";
                s = s + "inner  join LancamentoMTR lmtr on lmtr.NumeroLancamento = l.NumeroLancamento \n";
                s = s + "inner  join Clientes c on c.Codigo = l.CodigoCliente \n ";
                s = s + "inner  join Residuos r on r.Codigo = lmtr.CodigoResiduo \n ";
                s = s + "where " + pCampo + " like '%" + pFiltro + "%' \n";
                s = s + "order by " + pOrdem +" \n";

                s = s + "limit 1000 \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                return l_dt;
            }
            finally
            { 
                DesconectaBanco(); 
            }
        }

        public DataTable PreencheDataTableDescargaPendente(string pNumeroTicket, string pDataDescarga)
        {
            l_dt = new DataTable();
            try
            {

                ConectaBanco();
                s = "";
                s = s + "select 0 as Sequencial, l.NumeroLancamento, lmtr.NumeroMTR, lmtr.CodigoResiduo, r.DescricaoReduzida, lmtr.Franquia as QuantidadeColetada, \n";
                s = s + "       lmtr.Quantidade as QuantidadeDescarga,  l.CodigoCliente, c.NomeFantasia, 0.00 as PercentualDiferenca, 0.00 as QuantidadeCalculada ";
                s = s + "from   LancamentoMTR lmtr \n";
                s = s + "inner  join Lancamentos l on l.NumeroLancamento = lmtr.NumeroLancamento \n";
                s = s + "left   join Clientes    c on c.Codigo = l.CodigoCliente \n ";
                s = s + "inner  join Residuos    r on r.Codigo = lmtr.CodigoResiduo \n ";
                s = s + "where  lmtr.Ticket = '" + pNumeroTicket + "' \n";
                s = s + "and    (year(l.DataRetirada) = " + Convert.ToDateTime(pDataDescarga).Year + " or (year(l.DataRetirada) = " + (Convert.ToDateTime(pDataDescarga).Year - 1) + " and month(l.DataRetirada) = 12)) \n";
                s = s + "and    not lmtr.CodigoResiduo = 999 \n";
                s = s + "order  by lmtr.NumeroLancamento \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                return l_dt;
            }
            finally
            {
                DesconectaBanco();
            }

        }
        public DataTable PreencheDTClientesLancamentosContratos(string pData1, string pData2)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select distinct * from ( \n";
                s = s + "  select c.NomeFantasia as NomeCliente, c.Codigo as CodigoCliente, c.Nome \n ";
                s = s + "  from   Clientes c \n";
                s = s + "  where  exists(select * from Contratos where CodigoCliente = c.Codigo and (DataRecisao is null or DataRecisao = '0100-01-01' or DataRecisao = '0001-01-01') limit 1) \n";
                s = s + "  union all \n";
                s = s + "  select c.NomeFantasia as NomeCliente, c.Codigo as CodigoCliente, c.Nome \n";
                s = s + "  from   Clientes c \n";
                s = s + "  where  exists(select * from Lancamentos where DataRetirada >= '" + Convert.ToDateTime(pData1).ToString("yyyy-MM-dd") + "' \n";
                s = s + "                                          and   DataRetirada <= '" + Convert.ToDateTime(pData2).ToString("yyyy-MM-dd") + "' \n"; 
                s = s + "                                          and   CodigoCliente = c.Codigo limit 1) \n";
                s = s + ") x \n";
                s = s + "order  by Nome asc \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                return l_dt;
            }
            finally
            {
                DesconectaBanco();
            }
        }

        public DataTable PreencheDataComContaineresDisponiveis(string pOrdenacao)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select a.Numero as Container, a.Tipo, a.Capacidade \n";
                s = s + "from   Cacambas a \n";
                s = s + "where not exists( select * from Lancamentos \n";
                s = s + "                  where NumeroCaixa = a.Numero \n";
                s = s + "                  and (DataRetirada is null or DataRetirada = '0100-01-01' or DataRetirada = '0001-01-01') ) \n";
                s = s + "and not a.Numero = '' \n";
                s = s + "and not a.Numero is null \n";
                s = s + "and(a.Inativo = 0    or a.Inativo is null) \n";
                s = s + "and(a.EhTerceiro = 0 or a.EhTerceiro is null) \n";
                s = s + "order by " + pOrdenacao;
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                return l_dt;
            }
            finally
            {
                DesconectaBanco();
            }

        }
        public Int32 CountDisponiveis()
        {
            int iRet = 0;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Count(a.Numero) as TotalLinhas \n";
                s = s + "from   Cacambas a \n";
                s = s + "inner  join Lancamentos l on l.NumeroCaixa = a.Numero \n";
                s = s + "where  (l.DataRetirada is null or l.DataRetirada = '0100/01/01' or l.DataRetirada = '100/01/01')";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                iRet = Convert.ToInt32(l_ds.Tables[0].Rows[0][0]);
            }
            catch (Exception ex)
            {
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                iRet = 0;
            }
            finally
            {
                DesconectaBanco();
            }
            return iRet;
        }
        public Int32 CountLocados()
        {
            int iRet = 0;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Count(*) as TotalLocados \n";
                s = s + "from \n";
                s = s + "( select l.NumeroCaixa \n";
                s = s + "  from   Lancamentos l \n";
                s = s + "  inner  join Cacambas a on a.Numero = l.NumeroCaixa \n";
                s = s + "  where  (l.DataRetirada is null or l.DataRetirada = '0100-01-01' or l.DataRetirada = '0001-01-01') \n";
                s = s + "  and    not NumeroCaixa = '' \n";
                s = s + "  and    not NumeroCaixa is null \n";
                s = s + "  and    (EhTerceiro = 0 or EhTerceiro is null) \n";
                s = s + "  and    (a.Inativo = 0  or a.Inativo is null) \n";
                s = s + "  group  by NumeroCaixa \n";
                s = s + ") x \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                iRet = Convert.ToInt32(l_ds.Tables[0].Rows[0][0]);
            }
            catch (Exception ex)
            {
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                iRet = 0;
            }
            finally
            {
                DesconectaBanco();
            }
            return iRet;
        }
        public string ContainerLocadoCliente(string pContainer)
        {
            string sRet = "";
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select c.NomeFantasia \n";
                s = s + "from   Lancamentos l \n";
                s = s + "left   join Clientes c on c.Codigo = l.CodigoCliente \n ";
                s = s + "where  (l.DataRetirada is null or l.DataRetirada = '0100-01-01' or l.DataRetirada = '0001-01-01') \n";
                s = s + "and    l.NumeroCaixa = '" + pContainer + "' \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                if (l_ds.Tables[0].Rows.Count > 0)
                    sRet = l_ds.Tables[0].Rows[0]["NomeFantasia"].ToString();
                else
                    sRet = "";
            }
            catch (Exception ex)
            {
                sRet = "";
            }
            finally
            {
                DesconectaBanco();
            }
            return sRet;
        }
        public clsLancamentos ContainerLocadoCliente(string pContainer, int pColoca)
        {
            clsLancamentos oLancamento = new clsLancamentos();
            try
            {

                ConectaBanco();
                s = "";
                s = s + "select c.NomeFantasia, l.NumeroCaixa, l.CodigoCaminhaoColoca, l.DataColocacao, l.CodigoCliente, l.CodigoMotoristaColocou, l.NumeroLancamento \n";
                s = s + "from   Lancamentos l \n";
                s = s + "left   join Clientes c on c.Codigo = l.CodigoCliente \n ";
                s = s + "where  (l.DataRetirada is null or l.DataRetirada = '0100-01-01' or l.DataRetirada = '0001-01-01' or l.DataRetirada = '1900-01-01') \n";
                s = s + "and    l.NumeroCaixa = '" + pContainer + "' \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                if (l_ds.Tables[0].Rows.Count > 0)
                {
                    oLancamento.NumeroLancamento = Convert.ToInt32(l_ds.Tables[0].Rows[0]["NumeroLancamento"]);
                    oLancamento.CodigoCliente = Convert.ToInt32(l_ds.Tables[0].Rows[0]["CodigoCliente"]);
                    oLancamento.CodigoCaminhaoColoca = Convert.ToInt32(l_ds.Tables[0].Rows[0]["CodigoCaminhaoColoca"]);
                    oLancamento.CodigoMotoristaColocou = Convert.ToInt32(l_ds.Tables[0].Rows[0]["CodigoMotoristaColocou"]);
                    oLancamento.DataColocacao = l_ds.Tables[0].Rows[0]["DataColocacao"].ToString();
                    oLancamento.NumeroCaixa = l_ds.Tables[0].Rows[0]["NumeroCaixa"].ToString();
                }
            }
            finally
            {
                DesconectaBanco();
            }
            return oLancamento;
        }
        public DataTable PreencheDataComContaineresLocados(string pOrdenacao)
        {
            try
            {
                ConectaBanco();
                //s = "";
                //s = s + "select l.NumeroCaixa as 'Container', l.NumeroLancamento as 'LANÇAMENTO', c.NomeFantasia as 'Nome Cliente', l.CodigoCliente as 'Código', l.DataColocacao as 'Data Colocação', e.Bairro \n";
                //s = s + "from   Lancamentos l \n";
                //s = s + "left   join Clientes c on c.Codigo = l.CodigoCliente \n ";
                //s = s + "left   join Enderecos e on e.Codigo = l.CodigoCliente and e.TipoCadastro = 0 and e.TipoEndereco = 2 \n ";
                //s = s + "where  (l.DataRetirada is null or l.DataRetirada = '0100-01-01' or l.DataRetirada = '0001-01-01' )";
                s = "";
                s = s + "select * \n";
                s = s + "from \n";
                s = s + "( select l.NumeroCaixa as Container, l.NumeroLancamento as Lançamento, c.NomeFantasia as Cliente, l.CodigoCliente as Código, l.DataColocacao as Data_Colocação, e.Bairro \n";
                s = s + "  from   Lancamentos l \n";
                s = s + "  inner  join Cacambas  a on a.Numero = l.NumeroCaixa \n";
                s = s + "  left   join Clientes  c on c.Codigo = l.CodigoCliente \n ";
                s = s + "  left   join Enderecos e on e.Codigo = l.CodigoCliente and e.TipoCadastro = 0 and e.TipoEndereco = 2 \n ";
                s = s + "  where  (l.DataRetirada is null or l.DataRetirada = '0100-01-01' or l.DataRetirada = '0001-01-01') \n";
                s = s + "  and    not NumeroCaixa = '' \n";
                s = s + "  and    not NumeroCaixa is null \n";
                s = s + "  and    (EhTerceiro = 0 or EhTerceiro is null) \n";
                s = s + "  and    (a.Inativo  = 0 or a.Inativo  is null) \n";
                s = s + "  group  by l.NumeroCaixa \n";
                s = s + ") x \n";
                s = s + "order by " + pOrdenacao;
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                return l_dt;
            }
            finally
            {
                DesconectaBanco();
            }

        }
        public DataTable PreencheDadosConferenciaDiaria(string pData, int pCodigoCliente, string pOrdenacao, bool pComValores)
        {
            try
            {
                //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //MTR Nº|Código|Cliente      |Caixa|Caixa|Caminhão|Motorista |Resíduo   |Qt.Coleta|Qt.Descarg|%Dif |Un|  Valor  |Valor total|Destino             |Nr.Lanc|Observação  |MTR-e  |
                //      |      |             |Retir|Coloc|        |          |          |         |          |     |  |         |           |                    |       |            |       |
                //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                ConectaBanco();
                s = "";
                s = s + "select distinct \n";
                s = s + " NumeroMTR as 'MTR nº', convert(CodigoCliente, char) as 'Código Cliente', NomeFantasia as 'Nome fantasia', Retirada, Colocada, (select Modelo from Caminhoes where Codigo = cdCaminhao limit 1) as Caminhão, \n";
                s = s + " (select substr(Nome, 1, 14) from Funcionarios where Codigo = cdMotorista limit 1) as Motorista, \n";
                s = s + " (select DescricaoReduzida from Residuos where Codigo = CodigoResiduo limit 1) as 'Resíduo', QtColeta as 'Qt.Coleta', QtDescarga, ((QtColeta - QtDescarga) / QtColeta * 100) as 'Diferença', Unidade as Und, \n";
                if (pComValores)
                    s = s + " ValorUnitario as 'Unitário', ValorTotal as Total, \n";
                s = s + " Deposito as Destino, NumeroLancamento as 'NºLanç', Obs, \n";
                s = s + " NumeroMTRFatima as MTRe, DestinoMTRe, ControleInternoDescarga, \n";
                s = s + " (select Modelo from Caminhoes where Codigo = cdCaminhao) as Modelo, cdCaminhao \n";
                s = s + "from ";
                s = s + "( \n";
                // TO = 1 -> Caixa somente colocada
                s = s + "SELECT DISTINCT 0 as NumeroMTR, '' as Retirada, NumeroCaixa as Colocada, Lancamentos.DataColocacao as Data, CodigoCaminhaoColoca as cdCaminhao, CodigoMotoristaColocou as cdMotorista, \n";
                s = s + "                Clientes.NomeFantasia, Lancamentos.CodigoCliente, Lancamentos.obs, Lancamentos.NumeroLancamento, NuLancColocacao, 1 as 'TO', \n";
                s = s + "                TipoOperacao, Horas, 0 as CodigoResiduo, 0 as QtColeta, 0 as QtDescarga, 0 as Dif, '' as Unidade, 0 as ValorUnitario, 0 as ValorTotal, '' as Deposito, 0 as NumeroMTRFatima, '' AS DestinoMTRe, '' AS ControleInternoDescarga  \n";
                s = s + "From Lancamentos \n ";
                s = s + "LEFT JOIN Clientes ON Lancamentos.CodigoCliente = Clientes.Codigo\n ";
                s = s + "WHERE Lancamentos.DataColocacao = '" + Convert.ToDateTime(pData).ToString("yyyy-MM-dd") + "'\n ";
                //s = s + "and   TipoOperacao <> 3 and TipoOperacao <> 6 \n ";
                s = s + "and   (TipoOperacao <> 2 and TipoOperacao <> 3 and TipoOperacao <> 6 or (TipoOperacao = 2 and Lancamentos.DataRetirada = '" + Convert.ToDateTime(pData).ToString("yyyy-MM-dd") + "')) \n ";

                // TO = 2 -> Caixa somente retirada
                s = s + "Union All\n ";
                s = s + "SELECT DISTINCT lmtr.NumeroMTR, NumeroCaixa as Retirada, null as Colocada, Lancamentos.DataRetirada as Data, CodigoCaminhoRetirada as cdCaminhao, \n";
                s = s + "                CodigoMotoristaRetirou as cdMotorista, Clientes.NomeFantasia, Lancamentos.CodigoCliente, lmtr.observacao as obs, \n";
                s = s + "                Lancamentos.NumeroLancamento, NuLancColocacao, 2 as 'TO', \n ";
                s = s + "                TipoOperacao, Horas, lmtr.CodigoResiduo, lmtr.Franquia, lmtr.Quantidade, 0 as Dif, lmtr.Unidade, lmtr.ValorUnitario, lmtr.ValorTotal, Deposito, NumeroMTRFatima, DescargaMTRe AS DestinoMTRe, ControleInternoDescarga \n";
                s = s + "From Lancamentos\n ";
                s = s + "LEFT JOIN Clientes ON Lancamentos.CodigoCliente = Clientes.Codigo \n ";
                s = s + "LEFT JOIN LancamentoMTR lmtr on Lancamentos.NumeroLancamento = lmtr.NumeroLancamento \n";
                s = s + "WHERE Lancamentos.DataRetirada = '" + Convert.ToDateTime(pData).ToString("yyyy-MM-dd") + "'\n ";
                s = s + "And   TipoOperacao <> 3 and TipoOperacao <> 4 and TipoOperacao <> 6 \n ";
                
                // TO = 3 -> Caixa trocada pelo mesmo motorista, mas na select só traz os dados da retirada, depois é feita outra select no corpo do relatorio
                //           trazendo o número do lançamento que a Colocou novamente, concluindo a troca
                s = s + "Union All\n ";
                s = s + "SELECT DISTINCT lmtr.NumeroMTR, NumeroCaixa as Retirada, \n";
                s = s + "                (select l.NumeroCaixa from Lancamentos l where l.NumeroLancamento = Lancamentos.NuLancColocacao limit 1) as Colocada, \n";
                s = s + "                Lancamentos.DataRetirada as Data, CodigoCaminhoRetirada as cdCaminhao, CodigoMotoristaRetirou as cdMotorista, \n";
                s = s + "                Clientes.NomeFantasia, Lancamentos.CodigoCliente, lmtr.observacao as Obs, Lancamentos.NumeroLancamento, NuLancColocacao, 3 as 'TO', \n ";
                s = s + "                TipoOperacao, Horas, lmtr.CodigoResiduo, lmtr.Franquia, lmtr.Quantidade, 0 as Dif, lmtr.Unidade, lmtr.ValorUnitario, lmtr.ValorTotal, Deposito, NumeroMTRFatima, DescargaMTRe AS DestinoMTRe, ControleInternoDescarga  \n";
                s = s + "From Lancamentos \n ";
                s = s + "LEFT JOIN Clientes ON Lancamentos.CodigoCliente = Clientes.Codigo\n ";
                s = s + "LEFT JOIN LancamentoMTR lmtr on Lancamentos.NumeroLancamento = lmtr.NumeroLancamento \n";
                s = s + "WHERE Lancamentos.DataRetirada = '" + Convert.ToDateTime(pData).ToString("yyyy-MM-dd") + "'\n ";
                s = s + "And   Lancamentos.CodigoCaminhoRetirada = (select CodigoCaminhaoColoca from Lancamentos l where l.NumeroLancamento = Lancamentos.NumeroLancamento) \n ";
                s = s + "And   TipoOperacao = 3  \n ";
    
                // TO = 4
                s = s + "Union All\n ";
                s = s + "SELECT DISTINCT lmtr.NumeroMTR, NumeroCaixa as Retirada, \n";
                s = s + "                (select l.NumeroCaixa from Lancamentos l where l.NumeroLancamento = Lancamentos.NuLancColocacao limit 1) as Colocada, \n";
                s = s + "                Lancamentos.DataRetirada as Data, CodigoCaminhoRetirada as cdCaminhao, CodigoMotoristaRetirou as cdMotorista, \n ";
                s = s + "                Clientes.NomeFantasia, Lancamentos.CodigoCliente, lmtr.Observacao as obs, Lancamentos.NumeroLancamento, NuLancColocacao, 4 as 'TO', \n";
                s = s + "                TipoOperacao, Horas, lmtr.CodigoResiduo, lmtr.Franquia, lmtr.Quantidade, 0 as Dif, lmtr.Unidade, lmtr.ValorUnitario, lmtr.ValorTotal, Deposito, NumeroMTRFatima, DescargaMTRe AS DestinoMTRe, ControleInternoDescarga  \n";
                s = s + "From Lancamentos \n ";
                s = s + "LEFT JOIN Clientes ON Lancamentos.CodigoCliente = Clientes.Codigo \n ";
                s = s + "LEFT JOIN LancamentoMTR lmtr on Lancamentos.NumeroLancamento = lmtr.NumeroLancamento \n";
                s = s + "WHERE Lancamentos.DataRetirada = '" + Convert.ToDateTime(pData).ToString("yyyy-MM-dd") + "'\n ";
                s = s + "And   TipoOperacao in(3, 4) \n ";
        
                // TO = 5
                /*
                s = s + "Union All\n ";
                s = s + "SELECT DISTINCT 0 as NumeroMTR, '' as Retirada, NumeroCaixa as Colocada, Lancamentos.DataColocacao as Data, CodigoCaminhaoColoca as cdCaminhao, CodigoMotoristaColocou as cdMotorista, Clientes.NomeFantasia, Lancamentos.CodigoCliente, Lancamentos.obs, 0 as NumeroLancamento, NuLancColocacao, 5 as 'TO', TipoOperacao, Horas\n ";
                s = s + "From Lancamentos\n ";
                s = s + "LEFT JOIN Clientes ON Lancamentos.CodigoCliente = Clientes.Codigo\n ";
                s = s + "WHERE Lancamentos.DataColocacao = '" + Convert.ToDateTime(pData).ToString("yyyy-MM-dd") + "'\n ";
                s = s + "And   TipoOperacao = 3\n ";
                */
                
                // TO = 6
                s = s + "Union All \n ";
                s = s + "SELECT DISTINCT lmtr.NumeroMTR, NumeroCaixa as Retirada, \n";
                s = s + "                (select l.NumeroCaixa from Lancamentos l where l.NumeroLancamento = Lancamentos.NuLancColocacao limit 1) as Colocada, \n";
                s = s + "                Lancamentos.DataRetirada as Data, CodigoCaminhaoColoca as cdCaminhao, CodigoMotoristaRetirou as cdMotorista, \n";
                s = s + "                Clientes.NomeFantasia, Lancamentos.CodigoCliente, lmtr.observacao as obs, 0 as NumeroLancamento, NuLancColocacao, 6 as 'TO', \n";
                s = s + "                TipoOperacao, Horas, lmtr.CodigoResiduo, lmtr.Franquia, lmtr.Quantidade, 0 as Dif, lmtr.Unidade, lmtr.ValorUnitario, lmtr.ValorTotal, Deposito, NumeroMTRFatima, DescargaMTRe AS DestinoMTRe, ControleInternoDescarga \n";
                s = s + "From Lancamentos\n ";
                s = s + "LEFT JOIN Clientes ON Lancamentos.CodigoCliente = Clientes.Codigo\n ";
                s = s + "LEFT JOIN LancamentoMTR lmtr on Lancamentos.NumeroLancamento = lmtr.NumeroLancamento \n";
                s = s + "WHERE Lancamentos.DataRetirada = '" + Convert.ToDateTime(pData).ToString("yyyy-MM-dd") + "'\n ";
                s = s + "And   TipoOperacao = 6 \n ";

                s = s + ") x \n";
                if (pCodigoCliente > 0)
                    s = s + "where  CodigoCliente = " + pCodigoCliente + " \n";

                if (pOrdenacao.Length == 0)
                    s = s + "order by cdCaminhao, Horas, Retirada desc, NumeroLancamento, NomeFantasia \n ";
                else
                    s = s + "order by " + pOrdenacao + " \n ";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                if (l_ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in l_ds.Tables[0].Rows)
                    {
                        if (dr["Motorista"].ToString() != "")
                        {
                            if (dr["Motorista"].ToString().Split(" "[0]).Length > 1)
                                dr["Motorista"] = dr["Motorista"].ToString().Split(" "[0])[0];
                        }
                    }
                }
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                return l_dt;
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public string Excluir(int pNumeroLancamento)
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
                s = s + "delete from Lancamentos ";
                s = s + "where  NumeroLancamento = " + pNumeroLancamento.ToString();
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
        public int UltimoRegistro(bool pDoCliente)
        {
            int ur = 0;
            try
            {
                ConectaBanco();
                s = "";
                if (pDoCliente)
                    s = s + "select CodigoCliente \n";
                else
                    s = s + "select NumeroLancamento \n";
                s = s + "from   Lancamentos  \n";
                s = s + "order  by NumeroLancamento Desc limit 1 \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                if (pDoCliente)
                    ur = Convert.ToInt32(l_ds.Tables[0].Rows[0]["CodigoCliente"]);
                else
                    ur = Convert.ToInt32(l_ds.Tables[0].Rows[0]["NumeroLancamento"]);
            }
            catch (Exception ex)
            {
                ur = 0;
            }
            finally 
            {
                DesconectaBanco();
            }
            return ur;
        }

        public DataTable PreencheDadosConferenciaDiariaPorPeriodo(string pData1, string pData2, int pCodigoCliente, string pOrdenacao, bool pComValores, bool pScopoParaRelatorioFaturamento = false)
        {
            try
            {

                //MTR Nº|Código|Cliente      |Caixa|Caixa|Caminhão|Motorista |Resíduo   |Qt.Coleta|Un|  Valor  |Valor total|Destino             |Nr.Lanc|Observação  |MTR-e  |Destino MTR-e|
                //      |      |             |Retir|Coloc|        |          |          |         |  |         |           |                    |       |            |       |             |
                ConectaBanco();
                s = "";
                s = s + "select \n";
                if (pScopoParaRelatorioFaturamento)
                {
                    s = s + " NumeroMTR, CodigoCliente, NomeFantasia, NumeroCaixa, DataRetirada, DataColocacao, (select Modelo from Caminhoes where Codigo = cdCaminhao limit 1) as Caminhao, \n";
                    s = s + " (select Nome from Funcionarios where Codigo = cdMotorista limit 1) as Motorista, \n";
                    s = s + " (select DescricaoReduzida from Residuos where Codigo = CodigoResiduo limit 1) as 'Residuo', Quantidade as 'QtColetada', Franquia as QtDescarga, Unidade as Und, \n";
                    s = s + " (select DescricaoReduzida from Residuos where Codigo = (select CodigoGrupoResiduo from Residuos where Codigo = CodigoResiduo limit 1) limit 1) as 'GrupoResiduo', \n";
                    if (pComValores)
                        s = s + " ValorUnitario as Unitario, ValorTotal as Total, \n";
                    s = s + " Deposito as Destino, NumeroLancamento, Obs, NumeroMTRFatima as MTRe, \n";
                    s = s + " qtLancsDoMesmo,, DescargaMTRe as DestinoMTRe, ControleInternoDescarga as 'Controle Interno Descarga' \n";
                }
                else
                {
                    s = s + " NumeroMTR as 'MTR nº', CodigoCliente as 'Código Cliente', NomeFantasia as 'Nome Fantasia', NumeroCaixa as 'NºCaixa', DataRetirada as 'Data Coleta', DataColocacao as 'Data Colocação', (select Modelo from Caminhoes where Codigo = cdCaminhao limit 1) as Caminhão, \n";
                    s = s + " (select Nome from Funcionarios where Codigo = cdMotorista limit 1) as Motorista, \n";
                    s = s + " (select DescricaoReduzida from Residuos where Codigo = CodigoResiduo limit 1) as 'Resíduo', Quantidade as 'Qt.Coletada', Unidade as Und, \n";
                    if (pComValores)
                        s = s + " ValorUnitario as 'Unitário', ValorTotal as Total, \n";
                    s = s + " Deposito as 'Destino Final', NumeroLancamento as 'NºLançamento', Obs, NumeroMTRFatima as 'MTR-e Nº', \n";
                    s = s + " DescargaMTRe as DestinoMTRe, ControleInternoDescarga as 'Controle Interno Descarga' \n";
                }

                s = s + "from ";
                s = s + "( \n";

                s = s + "SELECT lmtr.NumeroMTR, NumeroCaixa, Lancamentos.DataRetirada, DataColocacao, CodigoCaminhoRetirada as cdCaminhao, lmtr.Franquia, \n";
                s = s + "       CodigoMotoristaRetirou as cdMotorista, Clientes.NomeFantasia, Lancamentos.CodigoCliente, lmtr.observacao as obs, \n";
                s = s + "       Lancamentos.NumeroLancamento, NuLancColocacao, 2 as 'TO', \n ";
                s = s + "       TipoOperacao, Horas, lmtr.CodigoResiduo, lmtr.Quantidade, lmtr.Unidade, lmtr.ValorUnitario, lmtr.ValorTotal, Deposito, NumeroMTRFatima, \n";
                s = s + "       (select count(lmtr2.NumeroLancamento) from LancamentoMTR lmtr2 where lmtr2.NumeroLancamento = Lancamentos.NumeroLancamento) as qtLancsDoMesmo, \n";
                s = s + "       lmtr.DescargaMTRe, lmtr.ControleInternoDescarga \n";                
                s = s + "From Lancamentos\n ";
                s = s + "LEFT JOIN Clientes ON Lancamentos.CodigoCliente = Clientes.Codigo\n ";
                s = s + "LEFT JOIN LancamentoMTR lmtr on Lancamentos.NumeroLancamento = lmtr.NumeroLancamento \n";
                s = s + "WHERE Lancamentos.DataRetirada >= '" + Convert.ToDateTime(pData1).ToString("yyyy-MM-dd") + "'\n ";
                s = s + "and   Lancamentos.DataRetirada <= '" + Convert.ToDateTime(pData2).ToString("yyyy-MM-dd") + "'\n ";
                s = s + ") x \n";
                if (pCodigoCliente > 0)
                    s = s + "where  CodigoCliente = " + pCodigoCliente + " \n";

                if (pOrdenacao.Length == 0)
                    s = s + "order by cdCaminhao, Horas, NumeroLancamento, NomeFantasia \n ";
                else
                    s = s + "order by " + pOrdenacao + " \n ";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                return l_dt;
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public DataTable PreencheDadosParaFaturamento(string pData1, string pData2, int pCodigoCliente, string pOrdenacao, bool pComValores, bool pCamposExcel = false)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select \n";
                if (!pCamposExcel)
                {
                    s = s + " NumeroLancamento, (select Modelo from Caminhoes where Codigo = cdCaminhao limit 1) as Caminhao, NumeroMTR, NumeroCaixa, DataColocacao, DataRetirada, \n";
                    s = s + " (select DescricaoReduzida from Residuos where Codigo = (select CodigoGrupoResiduo from Residuos where Codigo = CodigoResiduo limit 1) limit 1) as 'GrupoResiduo', \n";
                    s = s + " (select DescricaoReduzida from Residuos where Codigo = CodigoResiduo limit 1) as 'Residuo', Franquia as 'QtColetada', Quantidade as QtDescarga, Unidade as Und, \n";
                    s = s + " (select Nome from Funcionarios where Codigo = cdMotorista limit 1) as Motorista, \n";
                    if (pComValores)
                        s = s + " ValorUnitario as Unitario, ValorTotal as Total, \n";
                    s = s + " Deposito as Destino, Obs, NumeroMTRFatima as MTRe, \n";
                    s = s + " qtLancsDoMesmo, CodigoCliente, NomeFantasia \n";
                }
                else if (pCamposExcel) // para relatório para abrir no excel
                {
                    s = s + " CodigoCliente, NomeFantasia, NumeroLancamento, (select Modelo from Caminhoes where Codigo = cdCaminhao limit 1) as Caminhao, NumeroMTR, NumeroCaixa, DataColocacao, DataRetirada, \n";
                    s = s + " (select DescricaoReduzida from Residuos where Codigo = (select CodigoGrupoResiduo from Residuos where Codigo = CodigoResiduo limit 1) limit 1) as 'GrupoResiduo', \n";
                    s = s + " (select DescricaoReduzida from Residuos where Codigo = CodigoResiduo limit 1) as 'Residuo', Franquia as 'QtColetada', Quantidade as QtDescarga, Unidade as Und, \n";
                    s = s + " (select Nome from Funcionarios where Codigo = cdMotorista limit 1) as Motorista, \n";
                    if (pComValores)
                        s = s + " ValorUnitario as Unitario, ValorTotal as Total, \n";
                    s = s + " Deposito as Destino, Obs, NumeroMTRFatima as MTRe, \n";
                    s = s + " qtLancsDoMesmo \n";
                }
                s = s + "from ";
                s = s + "( \n";

                s = s + "SELECT lmtr.NumeroMTR, NumeroCaixa, Lancamentos.DataRetirada, DataColocacao, CodigoCaminhoRetirada as cdCaminhao, lmtr.Franquia, \n";
                s = s + "       CodigoMotoristaRetirou as cdMotorista, Clientes.NomeFantasia, Lancamentos.CodigoCliente, lmtr.observacao as obs, \n";
                s = s + "       Lancamentos.NumeroLancamento, NuLancColocacao, 2 as 'TO', \n ";
                s = s + "       TipoOperacao, Horas, lmtr.CodigoResiduo, lmtr.Quantidade, lmtr.Unidade, lmtr.ValorUnitario, lmtr.ValorTotal, Deposito, NumeroMTRFatima, \n";
                s = s + "       (select count(lmtr2.NumeroLancamento) from LancamentoMTR lmtr2 where lmtr2.NumeroLancamento = Lancamentos.NumeroLancamento) as qtLancsDoMesmo \n";
                s = s + "From Lancamentos\n ";
                s = s + "LEFT JOIN Clientes ON Lancamentos.CodigoCliente = Clientes.Codigo\n ";
                s = s + "LEFT JOIN LancamentoMTR lmtr on Lancamentos.NumeroLancamento = lmtr.NumeroLancamento \n";
                s = s + "WHERE Lancamentos.DataRetirada >= '" + Convert.ToDateTime(pData1).ToString("yyyy-MM-dd") + "'\n ";
                s = s + "and   Lancamentos.DataRetirada <= '" + Convert.ToDateTime(pData2).ToString("yyyy-MM-dd") + "'\n ";
                s = s + ") x \n";
                if (pCodigoCliente > 0)
                    s = s + "where  CodigoCliente = " + pCodigoCliente + " \n";

                if (pOrdenacao.Length == 0)
                    s = s + "order by cdCaminhao, Horas, NumeroLancamento, NomeFantasia \n ";
                else
                    s = s + "order by " + pOrdenacao + " \n ";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                return l_dt;
            }
            finally
            {
                DesconectaBanco();
            }
        }

        public int QuantidadePorPeriodo(string pData1, string pData2, int pCodigoCliente)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Count(*) as qtLinhas \n";
                s = s + "From   Lancamentos \n ";
                s = s + "where  Lancamentos.DataRetirada >= '" + Convert.ToDateTime(pData1).ToString("yyyy-MM-dd") + "'\n ";
                s = s + "and    Lancamentos.DataRetirada <= '" + Convert.ToDateTime(pData2).ToString("yyyy-MM-dd") + "'\n ";
                if (pCodigoCliente > 0)
                    s = s + "and    CodigoCliente = " + pCodigoCliente + " \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                return Convert.ToInt32(l_ds.Tables[0].Rows[0][0]);
            }
            catch (Exception ex)
            {
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                return 0;
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public DataTable PreencheDadosConferenciaClient(string pData1, string pData2, int pCodigoCliente, string pOrdenacao)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select NumeroLancamento, \n";
                s = s + "       NomeFantasia, CodigoCliente, \n";
                s = s + "       NumeroMTR, \n";
                s = s + "       NumeroCaixa, \n";
                s = s + "       DataColocacao, \n";
                s = s + "       DataRetirada, \n";
                s = s + "       (select DescricaoReduzida from Residuos where Codigo = CodigoResiduo limit 1) as DescricaoResiduo, \n";
                s = s + "       Quantidade, \n";
                s = s + "       Unidade as Und, \n";
                s = s + "       Deposito as Destino, \n";
                s = s + "       (select Modelo from Caminhoes where Codigo = cdCaminhao limit 1) as Caminhao, \n";
                s = s + "       (select Nome from Funcionarios where Codigo = cdMotorista limit 1) as Motorista, \n";
                s = s + "       Quantidade as QtColetada, \n";
                s = s + "       ValorUnitario as Unitario,  \n";
                s = s + "       ValorTotal as Total, \n";
                s = s + "       Data, \n";
                s = s + "       Obs \n";
                s = s + "from ";
                s = s + "( \n";
                s = s + "  select Lancamentos.NumeroLancamento, lmtr.NumeroMTR, NumeroCaixa, Lancamentos.DataRetirada, DataColocacao, CodigoCaminhoRetirada as cdCaminhao, \n";
                s = s + "         CodigoMotoristaRetirou as cdMotorista, Clientes.NomeFantasia, Lancamentos.CodigoCliente, Lancamentos.obs, \n";
                s = s + "         NuLancColocacao, 2 as 'TO', \n ";
                s = s + "         TipoOperacao, Horas, lmtr.CodigoResiduo, lmtr.Quantidade, lmtr.Unidade, lmtr.ValorUnitario, lmtr.ValorTotal, Deposito, NumeroMTRFatima, Data \n";
                s = s + "  from Lancamentos\n ";
                s = s + "  left join Clientes ON Lancamentos.CodigoCliente = Clientes.Codigo\n ";
                s = s + "  left join LancamentoMTR lmtr on Lancamentos.NumeroLancamento = lmtr.NumeroLancamento \n";
                s = s + "  where (Lancamentos.DataRetirada >= '" + Convert.ToDateTime(pData1).ToString("yyyy-MM-dd") + "'\n ";
                s = s + "         and Lancamentos.DataRetirada <= '" + Convert.ToDateTime(pData2).ToString("yyyy-MM-dd") + "') \n ";
                s = s + "         or (Lancamentos.DataRetirada is null or Lancamentos.DataRetirada = '0100-01-01') \n ";
                s = s + ") x \n";
                if (pCodigoCliente > 0)
                    s = s + "where  CodigoCliente = " + pCodigoCliente + " \n";

                if (pOrdenacao.Length == 0)
                    s = s + "order by cdCaminhao, Horas, NumeroLancamento, NomeFantasia \n ";
                else
                    s = s + "order by " + pOrdenacao + " \n ";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                return l_dt;
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public DataTable PreencheDadosLocacao(string pData1, string pData2, int pCodigoCliente, string pOrdenacao,
                                              bool pComValores, bool pMTR, bool pContainer, bool pDataColocacao,
                                              bool pDataRetirada, bool pCaminhao, bool pMotorista, bool pResiduo,
                                              bool pQuantidade, bool pUnidade, bool pValorUnitario, bool pValorTotal,
                                              bool pDestino, bool pEhColocacao = false, bool pSoRetirada = false, 
                                              int pNumeroLancamento = 0)
        {
            try
            {

                //MTR Nº|Código|Cliente      |Caixa|Caixa|Caminhão|Motorista |Resíduo   |Qt.Coleta|Un|  Valor  |Valor total|Destino             |Nr.Lanc|Observação  |MTR-e  |
                //      |      |             |Retir|Coloc|        |          |          |         |   ´        |  p     |  |         |           |                    |       |            |       |
                ConectaBanco();
                s = "";
                s = s + "select NumeroLancamento, \n";
                s = s + "       NomeFantasia, CodigoCliente, \n";
                if (pMTR)
                    s = s + " NumeroMTR, \n";
                if (pContainer)
                    s = s + " NumeroCaixa as Container, \n";

                if (pDataColocacao)
                    s = s + " DataColocacao, \n";
                if (pDataRetirada)
                    s = s + " DataRetirada, \n";

                s = s + " NuLancColocacao, \n";
                s = s + " NumeroMTR, \n";

                if (pResiduo)
                    s = s + " (select DescricaoReduzida from Residuos where Codigo = CodigoResiduo limit 1) as 'Residuo', \n";
                s = s + " Quantidade, \n";
                if (pUnidade)
                    s = s + " Unidade as Und, \n";
                if (pDestino)
                    s = s + " Deposito as Destino, obsLogistica, \n";
                if (pCaminhao)
                    s = s + " (select Modelo from Caminhoes where Codigo = CodigoCaminhaoColoca limit 1) as Caminhao, \n";
                if (pMotorista)
                    s = s + " (select Nome from Funcionarios where Codigo = CodigoMotoristaColocou limit 1) as Motorista, \n";

                s = s + " (select Modelo from Caminhoes where Codigo = CodigoCaminhoRetirada limit 1) as CaminhaoRetirada, \n";
                s = s + " (select Nome from Funcionarios where Codigo = CodigoMotoristaRetirou limit 1) as MotoristaRetirada, \n";

                if (pQuantidade)
                    s = s + " Quantidade as 'Qt.Coletada', \n";
                if (pValorUnitario)
                    s = s + " ValorUnitario as 'Unitário',  \n";
                if (pValorTotal)
                    s = s + " ValorTotal as Total, \n";
                s = s + " Data, CodigoCaminhaoColoca, CodigoCaminhoRetirada, CodigoMotoristaColocou, CodigoMotoristaRetirou, \n";
                s = s + "obs \n";
                s = s + "from ";
                s = s + "( \n";

                s = s + "SELECT Lancamentos.NumeroLancamento, lmtr.NumeroMTR, NumeroCaixa, Lancamentos.DataRetirada, DataColocacao, CodigoCaminhoRetirada, \n";
                s = s + "       Clientes.NomeFantasia, Lancamentos.CodigoCliente, Lancamentos.obs, lmtr.Observacao as obsLogistica,\n";
                s = s + "       NuLancColocacao, 2 as 'TO', CodigoCaminhaoColoca, CodigoMotoristaColocou, CodigoMotoristaRetirou, \n ";
                s = s + "       TipoOperacao, Horas, lmtr.CodigoResiduo, lmtr.Quantidade, lmtr.Unidade, lmtr.ValorUnitario, lmtr.ValorTotal, Deposito, NumeroMTRFatima, Data \n";
                s = s + "From Lancamentos\n ";
                s = s + "LEFT JOIN Clientes ON Lancamentos.CodigoCliente = Clientes.Codigo\n ";
                s = s + "LEFT JOIN LancamentoMTR lmtr on Lancamentos.NumeroLancamento = lmtr.NumeroLancamento \n";

                if (pEhColocacao)
                {
                    s = s + "where (Lancamentos.DataRetirada is null or Lancamentos.DataRetirada = '0100-01-01' \n ";
                    s = s + "       or Lancamentos.DataRetirada = '0001-01-01' or Lancamentos.DataRetirada = '1900-01-01') \n "; 
                }
                else if (pSoRetirada)
                {
                    s = s + "where (Lancamentos.DataRetirada >= '" + Convert.ToDateTime(pData1).ToString("yyyy-MM-dd") + "'\n ";
                    s = s + "       and Lancamentos.DataRetirada <= '" + Convert.ToDateTime(pData2).ToString("yyyy-MM-dd") + "') \n ";
                }
                else
                {
                    s = s + "where (Lancamentos.DataRetirada >= '" + Convert.ToDateTime(pData1).ToString("yyyy-MM-dd") + "'\n ";
                    s = s + "       and Lancamentos.DataRetirada <= '" + Convert.ToDateTime(pData2).ToString("yyyy-MM-dd") + "') \n ";
                    s = s + "       or (Lancamentos.DataRetirada is null or Lancamentos.DataRetirada = '0100-01-01' or Lancamentos.DataRetirada = '0001-01-01' or Lancamentos.DataRetirada = '1900-01-01') \n ";
                }
                s = s + ") x \n";
                if (pCodigoCliente > 0)
                    s = s + "where  CodigoCliente = " + pCodigoCliente + " \n";
                else if (pNumeroLancamento > 0)
                    s = s + "where  NumeroLancamento = " + pNumeroLancamento + " \n";
                if (pOrdenacao.Length == 0)
                    s = s + "order by cdCaminhao, Horas, NumeroLancamento, NomeFantasia \n ";
                else
                    s = s + "order by " + pOrdenacao + " \n ";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                return l_dt;
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public DataTable PreencheDadosMovimentacaoContainer(string pContainer, string pDataInicial)
        {
            try
            {
                ConectaBanco();
                s = " \n";
                s = s + "select c.NomeFantasia, DataColocacao, DataRetirada, f.Nome as NomeMotorista, ca.Modelo, \n";
                s = s + "       l.NumeroLancamento, r.DescricaoReduzida as Residuo, sum(lmtr.Quantidade) as 'Qt Total', lmtr.Unidade \n";
                s = s + "from   Lancamentos l \n";
                s = s + "inner  join Clientes c ON l.CodigoCliente = c.Codigo \n";
                s = s + "left   join Funcionarios f ON l.CodigoMotoristaRetirou = f.Codigo \n";
                s = s + "left   join Caminhoes ca ON l.CodigoCaminhoRetirada = ca.Codigo \n";
                s = s + "left   join LancamentoMTR lmtr ON lmtr.NumeroLancamento = l.NumeroLancamento \n";
                s = s + "left   join Residuos r ON r.Codigo = lmtr.CodigoResiduo \n"; 
                s = s + "where  l.NumeroCaixa  = '" + pContainer + "' \n";
                s = s + "and    (l.DataColocacao >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "' \n";
                s = s + "        or l.DataRetirada >= '" + Convert.ToDateTime(pDataInicial).ToString("yyyy-MM-dd") + "') \n";
                s = s + "group  by c.NomeFantasia, DataColocacao, DataRetirada, f.Nome, ca.Modelo, l.NumeroLancamento, r.DescricaoReduzida, lmtr.Unidade \n";
                s = s + "order by DataColocacao desc \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                return l_dt;
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public DataTable PegaDadosDeColetasPorPeriodo(string pData1, string pData2, int pCodigoCliente)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select 'BROOKS' as NumeroCaixa, count(NumeroCaixa) as QtCaixas, EhTerceiro \n";
                s = s + "from   Lancamentos l \n ";
                s = s + "left   join LancamentoMTR lmtr on l.NumeroLancamento = lmtr.NumeroLancamento \n";
                s = s + "left   join Cacambas c on c.Numero = l.NumeroCaixa \n ";
                s = s + "where  l.DataRetirada >= '" + Convert.ToDateTime(pData1).ToString("yyyy-MM-dd") + "'\n ";
                s = s + "and    l.DataRetirada <= '" + Convert.ToDateTime(pData2).ToString("yyyy-MM-dd") + "'\n ";
                if (pCodigoCliente != 0)
                    s = s + "and    CodigoCliente = " + pCodigoCliente + " \n";
                s = s + "and    ehTerceiro = 0 \n";

                s = s + "union  all \n";

                s = s + "select NumeroCaixa, count(NumeroCaixa) as QtCaixas, EhTerceiro \n";
                s = s + "from   Lancamentos l \n ";
                s = s + "left   join LancamentoMTR lmtr on l.NumeroLancamento = lmtr.NumeroLancamento \n";
                s = s + "left   join Cacambas c on c.Numero = l.NumeroCaixa \n ";
                s = s + "where  l.DataRetirada >= '" + Convert.ToDateTime(pData1).ToString("yyyy-MM-dd") + "'\n ";
                s = s + "and    l.DataRetirada <= '" + Convert.ToDateTime(pData2).ToString("yyyy-MM-dd") + "'\n ";
                if (pCodigoCliente != 0)
                    s = s + "and    CodigoCliente = " + pCodigoCliente + " \n";
                s = s + "and    ehTerceiro = 1 \n";

                s = s + "group by NumeroCaixa, EhTerceiro \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                return l_dt;
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public DataTable PreencheDadosRelatorioReciclaveis(string pData1, string pData2, int pCodigoCliente, string pOrdenacao)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select \n";
                s = s + " NumeroMTR, CodigoCliente, NomeFantasia, NumeroCaixa, DataRetirada, \n";
                s = s + " (select DescricaoReduzida from Residuos where Codigo = CodigoResiduo limit 1) as 'Residuo', Franquia as 'QtColetada', Quantidade as QtDescarga, Unidade as Und, \n";
                s = s + " (select DescricaoReduzida from Residuos where Codigo = (select CodigoGrupoResiduo from Residuos where Codigo = CodigoResiduo limit 1) limit 1) as 'GrupoResiduo', \n";
                s = s + " ValorUnitario as Unitario, ValorTotal as Total, \n";
                s = s + " NumeroLancamento, Obs \n";
                s = s + "from ";
                s = s + "( \n";
                s = s + "SELECT lmtr.NumeroMTR, NumeroCaixa, Lancamentos.DataRetirada, DataColocacao, CodigoCaminhoRetirada as cdCaminhao, lmtr.Franquia, \n";
                s = s + "       CodigoMotoristaRetirou as cdMotorista, Clientes.NomeFantasia, Lancamentos.CodigoCliente, Lancamentos.obs, \n";
                s = s + "       Lancamentos.NumeroLancamento, NuLancColocacao, 2 as 'TO', \n ";
                s = s + "       TipoOperacao, Horas, lmtr.CodigoResiduo, lmtr.Quantidade, lmtr.Unidade, lmtr.ValorUnitario, lmtr.ValorTotal, Deposito, NumeroMTRFatima \n";
                s = s + "From Lancamentos \n ";
                s = s + "LEFT  JOIN Clientes ON Lancamentos.CodigoCliente = Clientes.Codigo\n ";
                s = s + "LEFT  JOIN LancamentoMTR lmtr on Lancamentos.NumeroLancamento = lmtr.NumeroLancamento \n";
                s = s + "inner join Residuos as r on r.Codigo = lmtr.CodigoResiduo \n";
                s = s + "WHERE Lancamentos.DataRetirada >= '" + Convert.ToDateTime(pData1).ToString("yyyy-MM-dd") + "'\n ";
                s = s + "and   Lancamentos.DataRetirada <= '" + Convert.ToDateTime(pData2).ToString("yyyy-MM-dd") + "'\n ";
                s = s + "and   r.EhReciclavel = 1 \n";
                s = s + ") x \n";
                if (pCodigoCliente > 0)
                    s = s + "where  CodigoCliente = " + pCodigoCliente + " \n";

                if (pOrdenacao.Length == 0)
                    s = s + "order by cdCaminhao, Horas, NumeroLancamento, NomeFantasia \n ";
                else
                    s = s + "order by " + pOrdenacao + " \n ";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                return l_dt;
            }
            finally
            {
                DesconectaBanco();
            }
        }

        public DataTable PreencheDadosRelatorioReciclaveis(string pData1, string pData2, int pDestinoFinal, string pInResiduos, string pOrdenacao)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select \n";
                s = s + " NumeroMTR, CodigoCliente, NomeFantasia, NumeroCaixa, DataRetirada, CodigoResiduo, \n";
                s = s + " (select DescricaoReduzida from Residuos where Codigo = CodigoResiduo limit 1) as 'Residuo', Franquia as 'QtColetada', Quantidade as QtDescarga, Unidade as Und, \n";
                s = s + " (select DescricaoReduzida from Residuos where Codigo = (select CodigoGrupoResiduo from Residuos where Codigo = CodigoResiduo limit 1) limit 1) as 'GrupoResiduo', \n";
                s = s + " ValorUnitario as Unitario, ValorTotal as Total, \n";
                s = s + " NumeroLancamento, Obs \n";
                s = s + "from ";
                s = s + "( \n";
                s = s + "SELECT lmtr.NumeroMTR, NumeroCaixa, Lancamentos.DataRetirada, DataColocacao, CodigoCaminhoRetirada as cdCaminhao, lmtr.Franquia, \n";
                s = s + "       CodigoMotoristaRetirou as cdMotorista, Clientes.NomeFantasia, Lancamentos.CodigoCliente, Lancamentos.obs, \n";
                s = s + "       Lancamentos.NumeroLancamento, NuLancColocacao, 2 as 'TO', \n ";
                s = s + "       TipoOperacao, Horas, lmtr.CodigoResiduo, lmtr.Quantidade, lmtr.Unidade, lmtr.ValorUnitario, lmtr.ValorTotal, Deposito, NumeroMTRFatima \n";
                s = s + "From Lancamentos \n ";
                s = s + "left  join Clientes ON Lancamentos.CodigoCliente = Clientes.Codigo\n ";
                s = s + "left  join LancamentoMTR lmtr on Lancamentos.NumeroLancamento = lmtr.NumeroLancamento \n";
                s = s + "inner join Residuos as r on r.Codigo = lmtr.CodigoResiduo \n";
                s = s + "WHERE Lancamentos.DataRetirada >= '" + Convert.ToDateTime(pData1).ToString("yyyy-MM-dd") + "'\n ";
                s = s + "and   Lancamentos.DataRetirada <= '" + Convert.ToDateTime(pData2).ToString("yyyy-MM-dd") + "'\n ";
                if (pInResiduos.Length > 0)
                    s = s + "and   lmtr.CodigoResiduo in (" + pInResiduos + ") \n";
                if (pDestinoFinal > 0)
                    s = s + "and   cast(lmtr.Deposito as signed) = " + pDestinoFinal + " \n";

                s = s + ") x \n";

                if (pOrdenacao.Length == 0)
                    s = s + "order by cdCaminhao, Horas, NumeroLancamento, NomeFantasia \n ";
                else
                    s = s + "order by " + pOrdenacao + " \n ";
                l_ds = new DataSet();
                FillDataSet();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                if (l_dt.Rows.Count > 0)
                {
                    l_dt.NewRow();
                    l_dt.Rows[0][0] = ex.Message;
                }
                return l_dt;
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public DataTable PegaTotalResiduos(string pTipoRelatorio, string pData1, string pData2, string pInClientes, string pInResiduos, bool pSomenteTotal = false, string pResiduo = "", string pUnidade = "", string pOrdem = "")
        {
            try
            {
                ConectaBanco();
                s = "select ";
                if (!pSomenteTotal)
                {
                    if (pInClientes.Length > 0)
                        s = s + "CodigoCliente, ";
                    s = s + "Unidade, DescricaoReduzida, Codigo, sum(Quantidade) as QtTotal, CodigoGrupoResiduo, GrupoNome";
                    if (pTipoRelatorio == "Diario")
                        s = s + ", DataRetirada  \n ";
                    else if (pTipoRelatorio == "Mensal")
                        s = s + ", Ano, Mes  \n ";
                    else if (pTipoRelatorio == "Anual")
                        s = s + ", Ano  \n ";
                }
                else if (pSomenteTotal)
                {
                    s = s + "Unidade, DescricaoReduzida, sum(Quantidade) as QtTotal";
                    if (pTipoRelatorio == "Diario")
                        s = s + ", DataRetirada  \n ";
                    else if (pTipoRelatorio == "Mensal")
                        s = s + ", Ano, Mes  \n ";
                    else if (pTipoRelatorio == "Anual")
                        s = s + ", Ano  \n ";
                }
                s = s + "from ( \n ";
                s = s + "     select ";
                if (pInClientes.Length > 0)
                    s = s + "        l.CodigoCliente, ";
                s = s + "lmtr.Quantidade as Quantidade, ca.Capacidade as Capacidade, lmtr.Unidade,  \n ";
                s = s + "        r.DescricaoReduzida, r.Codigo, r.CodigoGrupoResiduo,  \n ";
                s = s + "        (select R2.DescricaoReduzida from Residuos R2 where R2.Codigo = r.CodigoGrupoResiduo limit 1) as GrupoNome";
                if (pTipoRelatorio == "Diario")
                    s = s + ", l.DataRetirada \n ";
                else if (pTipoRelatorio == "Mensal")
                    s = s + ", Year(l.DataRetirada) as Ano, Month(l.DataRetirada) as Mes  \n ";
                else if (pTipoRelatorio == "Anual")
                    s = s + ", Year(l.DataRetirada) as Ano \n ";
                s = s + "from  LancamentoMTR as lmtr \n ";
                s = s + "inner join Lancamentos   as l  on l.NumeroLancamento = lmtr.NumeroLancamento \n ";
                s = s + "inner join Residuos      as r  on r.Codigo = lmtr.CodigoResiduo \n ";
                s = s + "left  join Aterro        as a  on r.CodigoDestinoFinal = a.Codigo \n";
                s = s + "inner join Cacambas      as ca on ca.Numero = l.NumeroCaixa \n ";
                s = s + "WHERE l.DataRetirada >= '" + Convert.ToDateTime(pData1).ToString("yyyy-MM-dd") + "' \n ";
                s = s + "AND   l.DataRetirada <= '" + Convert.ToDateTime(pData2).ToString("yyyy-MM-dd") + "' \n ";
                if (pInClientes.Length > 0)
                    s = s + "AND  l.CodigoCliente in (" + pInClientes + ") \n";
                if (pInResiduos.Length > 1)
                    s = s + "and  r.Codigo in (" + pInResiduos + ") \n";
                s = s + "and lmtr.Unidade <> 'CX' \n ";

                s = s + "union all \n";

                s = s + "select ";
                if (pInClientes.Length > 0)
                    s = s + "        l.CodigoCliente, ";
                s = s + "lmtr.Quantidade as Quantidade, ca.Capacidade as Capacidade, lmtr.Unidade,  \n ";
                s = s + "        r.DescricaoReduzida, r.Codigo, r.CodigoGrupoResiduo,  \n ";
                s = s + "        (select R2.DescricaoReduzida from Residuos R2 where R2.Codigo = r.CodigoGrupoResiduo limit 1) as GrupoNome";
                if (pTipoRelatorio == "Diario")
                    s = s + ", l.DataRetirada ";
                else if (pTipoRelatorio == "Mensal")
                    s = s + ", Year(l.DataRetirada) as Ano, Month(l.DataRetirada) as Mes  \n ";
                else if (pTipoRelatorio == "Anual")
                    s = s + ", Year(l.DataRetirada) as Ano  \n ";
                s = s + "FROM  LancamentoMTR as lmtr \n ";
                s = s + "inner join Lancamentos   as l  on l.NumeroLancamento = lmtr.NumeroLancamento \n ";
                s = s + "inner join Residuos      as r  on r.Codigo = lmtr.CodigoResiduo \n ";
                s = s + "left  join Aterro        as a  on r.CodigoDestinoFinal = a.Codigo \n";
                s = s + "inner join Cacambas      as ca on ca.Numero = l.NumeroCaixa \n ";
                s = s + "WHERE l.DataRetirada >= '" + Convert.ToDateTime(pData1).ToString("yyyy-MM-dd") + "' \n ";
                s = s + "AND   l.DataRetirada <= '" + Convert.ToDateTime(pData2).ToString("yyyy-MM-dd") + "' \n ";
                if (pInClientes.Length > 0)
                    s = s + "AND  l.CodigoCliente in (" + pInClientes + ") \n";
                if (pInResiduos.Length > 1)
                    s = s + "and  r.Codigo in (" + pInResiduos + ") \n";
                s = s + "and lmtr.Unidade = 'CX' \n ";
                s = s + ") x  \n ";

                if (pSomenteTotal)
                {
                    s = s + "where    Unidade = '" +  pUnidade + "' \n";
                    s = s + "and      DescricaoReduzida = '" + pResiduo + "' \n";
                    s = s + "group by Unidade, DescricaoReduzida ";
                }
                else if (!pSomenteTotal)
                {
                    if (pInClientes.Length > 0)
                        s = s + "group by CodigoCliente, Unidade, DescricaoReduzida, Codigo, CodigoGrupoResiduo, GrupoNome ";
                    else
                        s = s + "group by Unidade, DescricaoReduzida, Codigo, CodigoGrupoResiduo, GrupoNome ";
                    if (pTipoRelatorio == "Diario")
                        s = s + ", DataRetirada ";
                    else if (pTipoRelatorio == "Mensal")
                        s = s + ", Ano, Mes \n ";
                    else if (pTipoRelatorio == "Anual")
                        s = s + ", Ano  \n ";

                    if (pTipoRelatorio == "Diario")
                        s = s + "order by DescricaoReduzida, DataRetirada, Codigo, Unidade, CodigoGrupoResiduo  \n ";
                    else if (pTipoRelatorio == "Mensal")
                        s = s + ", Ano, Mes \n";
                    else if (pTipoRelatorio == "Anual")
                        s = s + ", Ano \n";
                }
                if (pOrdem.Length > 0 && s.IndexOf("order by") == -1)
                    s = s + "order by " + pOrdem + " \n ";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                return l_dt;
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public void SalvarServicoExecutadoVisto(string pId, string pVisto, string pDataExecucao)
        {
            oDB.MySqlConnect.Open();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update ProgramacaoLancamento \n";
                s = s + "set    Visto = '" + pVisto + "', \n";
                s = s + "       DataExecucao = '" + Convert.ToDateTime(pDataExecucao).ToString("yyyy-MM-dd") + "' \n";
                s = s + "where  Id = " + pId + " \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public string PegaIdProgramacaoLancamento(string pNumeroLancamento, string pCodigoCliente, string pSequencialProgramacao)
        {
            string sRet = "0";
            try
            {
                if (pNumeroLancamento != "")
                {
                    oDB.ConectaMySql();
                    s = "";
                    s = s + "select Id \n";
                    s = s + "from   ProgramacaoLancamento \n";
                    s = s + "where  NumeroLancamento = " + pNumeroLancamento + " \n";
                    s = s + "and    CodigoCliente    = " + pCodigoCliente + " \n";
                    s = s + "and    SequencialProgramacao = " + pSequencialProgramacao + " \n";
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
                if (pNumeroLancamento != "")
                    DesconectaBanco();
            }
            return sRet;
        }

        public string PegaVistoProgramacaoLancamento(string pNumeroLancamento, string pCodigoCliente, string pSequencialProgramacao)
        {
            string sRet = "0";
            try
            {
                if (pNumeroLancamento != "")
                {
                    oDB.ConectaMySql();
                    s = "";
                    s = s + "select Visto \n";
                    s = s + "from   ProgramacaoLancamento \n";
                    s = s + "where  NumeroLancamento = " + pNumeroLancamento + " \n";
                    s = s + "and    CodigoCliente    = " + pCodigoCliente + " \n";
                    s = s + "and    SequencialProgramacao = " + pSequencialProgramacao + " \n";
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
                if (pNumeroLancamento != "")
                    DesconectaBanco();
            }
            return sRet;
        }
    }
}