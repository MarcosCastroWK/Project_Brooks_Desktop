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
	public class clsContratosDados
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

        private clsContratos AtribuirDados(clsContratos pContratos, int CodigoCliente)
        {
            if (l_ds.Tables.Count > 0)
            {
                l_dt = l_ds.Tables[0];
                if (l_dt.Rows.Count > 0)
                {
                    if (l_dt.Rows[0]["Codigo"].ToString() != "")
                        pContratos.Codigo = Convert.ToInt16(l_dt.Rows[0]["Codigo"]);
                    pContratos.Nome = l_dt.Rows[0]["Nome"].ToString();
                    pContratos.NomeFantasia = l_dt.Rows[0]["NomeFantasia"].ToString();
                    pContratos.CNPJ_CPF = l_dt.Rows[0]["CNPJ_CPF"].ToString();
                    pContratos.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                    if (l_dt.Rows[0]["CodigoCliente"].ToString() != "")
                        pContratos.CodigoCliente = Convert.ToInt16(l_dt.Rows[0]["CodigoCliente"]);
                    pContratos.DescricaoHistorico = l_dt.Rows[0]["DescricaoHistorico"].ToString();
                    if (l_dt.Rows[0]["DiaVencimento"].ToString() != "")
                        pContratos.DiaVencimento = Convert.ToInt32(l_dt.Rows[0]["DiaVencimento"]);
                    if (l_dt.Rows[0]["NumeroContrato"].ToString() != "")
                        pContratos.NumeroContrato = Convert.ToInt32(l_dt.Rows[0]["NumeroContrato"]);
                    if (l_dt.Rows[0]["ValorContrato"].ToString() != "")
                        pContratos.ValorContrato = Convert.ToDecimal(l_dt.Rows[0]["ValorContrato"].ToString());
                    if (l_dt.Rows[0]["DataTermino"].ToString() != "")
                    {
                        pContratos.DataTermino = Convert.ToDateTime(l_dt.Rows[0]["DataTermino"].ToString()).Date.ToShortDateString();
                        if (pContratos.DataTermino == "01/01/0100" || pContratos.DataTermino == "01/01/0001")
                            pContratos.DataTermino = "";
                    }
                    if (l_dt.Rows[0]["NumeroCaixasLocadas"].ToString() != "")
                        pContratos.NumeroCaixasLocadas = Convert.ToInt16(l_dt.Rows[0]["NumeroCaixasLocadas"]);
                    if (l_dt.Rows[0]["DataInicio"].ToString() != "")
                    {
                        pContratos.DataInicio = Convert.ToDateTime(l_dt.Rows[0]["DataInicio"].ToString()).Date.ToShortDateString();
                        if (pContratos.DataInicio == "01/01/0100" || pContratos.DataInicio == "01/01/0001")
                            pContratos.DataInicio = "";
                    }
                    if (l_dt.Rows[0]["DataReajuste"].ToString() != "")
                    {
                        pContratos.DataReajuste = Convert.ToDateTime(l_dt.Rows[0]["DataReajuste"].ToString()).Date.ToShortDateString();
                        if (pContratos.DataReajuste == "01/01/0100" || pContratos.DataReajuste == "01/01/0001")
                            pContratos.DataReajuste = "";
                    }
                    if (l_dt.Rows[0]["DataRecisao"].ToString() != "")
                    {
                        pContratos.DataRecisao = Convert.ToDateTime(l_dt.Rows[0]["DataRecisao"].ToString()).Date.ToShortDateString();
                        if (pContratos.DataRecisao == "01/01/0100" || pContratos.DataRecisao == "01/01/0001")
                            pContratos.DataRecisao = "";
                    }
                    pContratos.IndiceReajuste = l_dt.Rows[0]["IndiceReajuste"].ToString();
                    pContratos.SituacaoRecisao = l_dt.Rows[0]["SituacaoRecisao"].ToString();
                    if (l_dt.Rows[0]["AniversarioReajuste"].ToString() != "")
                        pContratos.AniversarioReajuste = l_dt.Rows[0]["AniversarioReajuste"].ToString();
                    if (l_dt.Rows[0]["DataRegistro"].ToString() != "")
                    {
                        pContratos.DataRegistro = Convert.ToDateTime(l_dt.Rows[0]["DataRegistro"].ToString()).Date.ToShortDateString();
                        if (pContratos.DataRegistro == "01/01/0100" || pContratos.DataRegistro == "01/01/0001")
                            pContratos.DataRegistro = "";
                    }
                    pContratos.MotivoRescisao = l_dt.Rows[0]["MotivoRescisao"].ToString();
                    if (l_dt.Rows[0]["AvisarDiasAntes"].ToString() != "")
                        pContratos.AvisarDiasAntes = Convert.ToInt16(l_dt.Rows[0]["AvisarDiasAntes"]);
                    pContratos.Observacao = l_dt.Rows[0]["Observacao"].ToString();
                }
            }
            return pContratos;
        }

        public clsContratos PegaDados(clsContratos pContratos, int pCodigo, int pCodigoCliente)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select t.Codigo, t.CodigoCliente, DescricaoHistorico, DiaVencimento, NumeroContrato, ValorContrato, DataTermino, \n";
                s = s + "       NumeroCaixasLocadas, DataInicio, DataReajuste, IndiceReajuste, DataRecisao, SituacaoRecisao, \n";
                s = s + "       Observacao, c.Nome, c.NomeFantasia, AniversarioReajuste, DataRegistro, MotivoRescisao, AvisarDiasAntes, C.CNPJ_CPF \n";
                s = s + "from   Contratos t \n";
                s = s + "inner join Clientes c on c.Codigo = t.CodigoCliente \n ";
                if (pCodigo > 0)
                {
                    s = s + "where  t.Codigo = " + pCodigo + " ";
                    if (pCodigoCliente > 0)
                        s = s + "and    t.CodigoCliente = " + pCodigoCliente + " ";
                    s = s + "limit 1";
                }
                else if (pCodigo == 0)
                {
                    s = s + "and    t.CodigoCliente = " + pCodigoCliente + " \n";
                    s = s + "order by t.Codigo desc limit 1";
                }
                FillDataSet();
                pContratos = AtribuirDados(pContratos, pCodigoCliente );
                
                return pContratos;
            }
            catch (Exception ex)
            {
                return new clsContratos();
            }
            finally
            {
                DesconectaBanco();
            }
        }

        public int PegaUltimoSequencial(int pCodigoCliente)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select t.Codigo \n";
                s = s + "from   Contratos t \n";
                s = s + "where  t.CodigoCliente = " + pCodigoCliente + " \n";
                s = s + "order  by t.Codigo desc limit 1 \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    return Convert.ToInt32(l_dt.Rows[0][0]);
                else
                    return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                DesconectaBanco();
            }
        }
        
        public int PegaDiaDoVencimento(int pCodigoCliente)
        {
            int nRet = 0;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select t.DiaVencimento \n";
                s = s + "from   Contratos t \n";
                s = s + "where  t.CodigoCliente = " + pCodigoCliente + " \n";
                s = s + "and    (t.DataRecisao = '0001-01-01' or t.DataRecisao = '0100-01-01' or t.DataRecisao = '1900-01-01' or t.DataRecisao is null) \n";
                s = s + "order  by t.Codigo desc limit 1 \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    nRet =  Convert.ToInt32(l_dt.Rows[0][0]);
            }
            finally
            {
                DesconectaBanco();
            }
            return nRet;
        }

        public DataTable PegaDados(clsContratos pContratos, int pCodigo, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, CodigoCliente, DescricaoHistorico, DiaVencimento, NumeroContrato, ValorContrato, DataTermino, \n";
                s = s + "       NumeroCaixasLocadas, DataInicio, DataReajuste, DataRecisao, IndiceReajuste, SituacaoRecisao, \n";
                s = s + "       AniversarioReajuste, DataRegistro, MotivoRescisao, AvisarDiasAntes, Observacao \n";
                s = s + "from   Contratos ";
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
                    s = s + " order by Tipo ";
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

        public DataTable PegaDados(bool pCancelados)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select t.Codigo, t.CodigoCliente, DescricaoHistorico, DiaVencimento, NumeroContrato, ValorContrato, DataTermino, \n";
                s = s + "       NumeroCaixasLocadas, DataInicio, DataReajuste, IndiceReajuste, DataRecisao, SituacaoRecisao, \n";
                s = s + "       Observacao, c.Nome, c.NomeFantasia, AniversarioReajuste, DataRegistro, MotivoRescisao, AvisarDiasAntes, C.CNPJ_CPF \n";
                s = s + "from   Contratos t \n";
                s = s + "inner join Clientes c on c.Codigo = t.CodigoCliente \n ";
                if (pCancelados)
                    s = s + "where DataRecisao > '0100-01-01'";
                else
                    s = s + "where (DataRecisao = '0100-01-01' or DataRecisao is null)"; 
                s = s + " order by CodigoCliente ";
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
                s = s + "from Contratos ";
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
        public string DadoExiste(int pCodigo, int pCodigoCliente = 0)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Codigo ";
                s = s + "from   Contratos ";
                s = s + "where  Codigo = " + pCodigo + " \n";
                if (pCodigoCliente > 0)
                    s = s + "and  CodigoCliente = " + pCodigoCliente + " \n";                
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
        public string Inserir(clsContratos pContratos, int pCodigo = 0)
        {
            try
            {
                s = "";
                s = s + "insert into Contratos \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + "Codigo, \n";
                s = s + "   CodigoCliente, DescricaoHistorico, DiaVencimento, NumeroContrato, ValorContrato, DataTermino, \n";
                s = s + "   NumeroCaixasLocadas, DataInicio, DataReajuste, DataRecisao, IndiceReajuste, SituacaoRecisao, \n";
                s = s + "   AniversarioReajuste, DataRegistro, MotivoRescisao, AvisarDiasAntes, Observacao \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pCodigo == 722)
                    pCodigo = 722;
                if (pCodigo > 0)
                    s = s + pCodigo.ToString() + ", \n";
                if (pContratos.CodigoCliente == 0)
                    s = s + "0, ";
                else
                    s = s + pContratos.CodigoCliente.ToString() + ", ";
                s = s + "'" + pContratos.DescricaoHistorico + "', ";
                if (pContratos.DiaVencimento == 0)
                    s = s + "0, ";
                else
                    s = s + pContratos.DiaVencimento.ToString() + ", ";
                if (pContratos.NumeroContrato == 0)
                    s = s + "0, ";
                else
                    s = s + pContratos.NumeroContrato.ToString() + ", ";
                if (pContratos.ValorContrato == 0)
                    s = s + "0, ";
                else
                    s = s + " " + pContratos.ValorContrato.ToString().Replace(",", ".") + ", ";
                if (pContratos.DataTermino == "")
                    s = s + "'0001-01-01', ";
                else
                    s = s + "'" + Convert.ToDateTime(pContratos.DataTermino).ToString("yyyy-MM-dd") + "', ";
                if (pContratos.NumeroCaixasLocadas == 0)
                    s = s + "0, ";
                else
                    s = s + pContratos.NumeroCaixasLocadas + ", ";
                if (pContratos.DataInicio == "")
                    s = s + "'0001-01-01', ";
                else
                    s = s + "'" + Convert.ToDateTime(pContratos.DataInicio).ToString("yyyy-MM-dd") + "', ";
                if (pContratos.DataReajuste == "")
                    s = s + "'0001-01-01', ";
                else
                    s = s + "'" + Convert.ToDateTime(pContratos.DataReajuste).ToString("yyyy-MM-dd") + "', ";
                if (pContratos.DataRecisao == "")
                    s = s + "'0001-01-01', ";
                else
                    s = s + "'" + Convert.ToDateTime(pContratos.DataRecisao).ToString("yyyy-MM-dd") + "', ";
                s = s + "'" + pContratos.IndiceReajuste + "', ";
                s = s + "'" + pContratos.SituacaoRecisao + "', ";
                s = s + "'" + pContratos.AniversarioReajuste + "', ";
                if (pContratos.DataRegistro == "")
                    s = s + "'0001-01-01', ";
                else
                    s = s + "'" + Convert.ToDateTime(pContratos.DataRegistro).ToString("yyyy-MM-dd") + "', ";
                s = s + "'" + pContratos.MotivoRescisao + "', ";
                if (pContratos.AvisarDiasAntes == 0)
                    s = s + "0, ";
                else
                    s = s + pContratos.AvisarDiasAntes.ToString() + ", ";
                s = s + "'" + pContratos.Observacao + "' ";
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
                DesconectaBanco();
                s = "inserido";
            }
            catch (Exception ex)
            {
                s = ex.Message;
            }
            return s;
        }
        public string Alterar(clsContratos pContratos, int pCodigo, int pCodigoCliente = 0)
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
                s = s + "update Contratos \n";
                s = s + "set DescricaoHistorico = '" + pContratos.DescricaoHistorico + "',  \n";
                if (pContratos.DiaVencimento == 0)
                    s = s + "   DiaVencimento = 0,  \n";
                else
                    s = s + "   DiaVencimento = " + pContratos.DiaVencimento.ToString() + ",  \n";
                if (pContratos.NumeroContrato == 0)
                    s = s + "   NumeroContrato = 0,  \n";
                else
                    s = s + "   NumeroContrato = " + pContratos.NumeroContrato.ToString() + ",  \n";
                if (pContratos.ValorContrato == 0)
                    s = s + "     ValorContrato = 0,  \n";
                else
                    s = s + "    ValorContrato = " + pContratos.ValorContrato.ToString().Replace(",", ".") + ",  \n";
                if (pContratos.DataTermino == "")
                    s = s + "    DataTermino = '0001-01-01',  \n";
                else
                    s = s + "    DataTermino = '" + Convert.ToDateTime(pContratos.DataTermino).ToString("yyyy-MM-dd") + "',  \n";
                if (pContratos.NumeroCaixasLocadas == 0)
                    s = s + "    NumeroCaixasLocadas = 0,  \n";
                else
                    s = s + "    NumeroCaixasLocadas = " + pContratos.NumeroCaixasLocadas + ",  \n";
                if (pContratos.DataInicio == "")
                    s = s + "    DataInicio = '0001-01-01',  \n";
                else
                    s = s + "    DataInicio = '" + Convert.ToDateTime(pContratos.DataInicio).ToString("yyyy-MM-dd") + "',  \n";
                s = s + "    CodigoCliente = " + pContratos.CodigoCliente + ", \n ";
                if (pContratos.DataReajuste == "")
                    s = s + "    DataReajuste = '0001-01-01',  \n";
                else
                    s = s + "    DataReajuste = '" + Convert.ToDateTime(pContratos.DataReajuste).ToString("yyyy-MM-dd") + "',  \n";
                if (pContratos.DataRecisao == "")
                    s = s + "    DataRecisao = '0001-01-01',  \n";
                else
                    s = s + "    DataRecisao = '" + Convert.ToDateTime(pContratos.DataRecisao).ToString("yyyy-MM-dd") + "',  \n";
                s = s + "    IndiceReajuste = '" + pContratos.IndiceReajuste + "',  \n";
                s = s + "    SituacaoRecisao = '" + pContratos.SituacaoRecisao + "',  \n";
                s = s + "    AniversarioReajuste = '" + pContratos.AniversarioReajuste + "',  \n";
                if (pContratos.DataRegistro == "")
                    s = s + "    DataRegistro = '0001-01-01',  \n";
                else
                    s = s + "    DataRegistro = '" + Convert.ToDateTime(pContratos.DataRegistro).ToString("yyyy-MM-dd") + "',  \n";
                s = s + "    MotivoRescisao = '" + pContratos.MotivoRescisao + "',  \n";
                if (pContratos.AvisarDiasAntes == 0)
                    s = s + "    AvisarDiasAntes = 0,  \n";
                else
                    s = s + "    AvisarDiasAntes = " + pContratos.AvisarDiasAntes.ToString() + ",  \n";
                s = s + "    Observacao = '" + pContratos.Observacao + "' \n";
                s = s + "where Codigo = " + pCodigo.ToString() + " \n";
                if (pCodigoCliente > 0)
                    s = s + "and    CodigoCliente = " + pCodigoCliente + " \n ";

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

        public string AlterarCamposContrato(clsContratos pContratos, int pCodigo, int pCodigoCliente = 0)
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
                s = s + "update Contratos \n";
                s = s + "set DescricaoHistorico = '" + pContratos.DescricaoHistorico + "',  \n";
                if (pContratos.DiaVencimento == 0)
                    s = s + "   DiaVencimento = 0,  \n";
                else
                    s = s + "   DiaVencimento = " + pContratos.DiaVencimento.ToString() + ",  \n";
                if (pContratos.NumeroContrato == 0)
                    s = s + "   NumeroContrato = 0,  \n";
                else
                    s = s + "   NumeroContrato = " + pContratos.NumeroContrato.ToString() + ",  \n";
                if (pContratos.ValorContrato == 0)
                    s = s + "     ValorContrato = 0,  \n";
                else
                    s = s + "    ValorContrato = " + pContratos.ValorContrato.ToString().Replace(",", ".") + ",  \n";
                if (pContratos.DataTermino == "")
                    s = s + "    DataTermino = '0001-01-01',  \n";
                else
                    s = s + "    DataTermino = '" + Convert.ToDateTime(pContratos.DataTermino).ToString("yyyy-MM-dd") + "',  \n";
                if (pContratos.NumeroCaixasLocadas == 0)
                    s = s + "    NumeroCaixasLocadas = 0,  \n";
                else
                    s = s + "    NumeroCaixasLocadas = " + pContratos.NumeroCaixasLocadas + ",  \n";
                if (pContratos.DataInicio == "")
                    s = s + "    DataInicio = '0001-01-01',  \n";
                else
                    s = s + "    DataInicio = '" + Convert.ToDateTime(pContratos.DataInicio).ToString("yyyy-MM-dd") + "',  \n";
                s = s + "    CodigoCliente = " + pContratos.CodigoCliente + ", \n ";
                if (pContratos.DataReajuste == "")
                    s = s + "    DataReajuste = '0001-01-01',  \n";
                else
                    s = s + "    DataReajuste = '" + Convert.ToDateTime(pContratos.DataReajuste).ToString("yyyy-MM-dd") + "',  \n";
                s = s + "    IndiceReajuste = '" + pContratos.IndiceReajuste + "',  \n";
                s = s + "    AniversarioReajuste = '" + pContratos.AniversarioReajuste + "',  \n";
                if (pContratos.DataRegistro == "")
                    s = s + "    DataRegistro = '0001-01-01',  \n";
                else
                    s = s + "    DataRegistro = '" + Convert.ToDateTime(pContratos.DataRegistro).ToString("yyyy-MM-dd") + "',  \n";
                if (pContratos.AvisarDiasAntes == 0)
                    s = s + "    AvisarDiasAntes = 0,  \n";
                else
                    s = s + "    AvisarDiasAntes = " + pContratos.AvisarDiasAntes.ToString() + ",  \n";
                s = s + "    Observacao = '" + pContratos.Observacao + "' \n";
                s = s + "where Codigo = " + pCodigo.ToString() + " \n";
                if (pCodigoCliente > 0)
                    s = s + "and    CodigoCliente = " + pCodigoCliente + " \n ";

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
        public string AlterarCamposRescisao(clsContratos pContratos, int pCodigo, int pCodigoCliente = 0)
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
                s = s + "update Contratos \n";
                if (pContratos.DataRecisao == "")
                    s = s + "set DataRecisao = '0001-01-01',  \n";
                else
                    s = s + "set DataRecisao = '" + Convert.ToDateTime(pContratos.DataRecisao).ToString("yyyy-MM-dd") + "',  \n";
                if (pContratos.DataRegistro == "")
                    s = s + "    DataRegistro = '0001-01-01',  \n";
                else
                    s = s + "    DataRegistro = '" + Convert.ToDateTime(pContratos.DataRegistro).ToString("yyyy-MM-dd") + "',  \n";
                s = s + "    SituacaoRecisao = '" + pContratos.SituacaoRecisao + "',  \n";
                s = s + "    MotivoRescisao = '" + pContratos.MotivoRescisao + "' \n";
                s = s + "where Codigo = " + pCodigo.ToString() + " \n";
                if (pCodigoCliente > 0)
                    s = s + "and    CodigoCliente = " + pCodigoCliente + " \n ";
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
        public string AlterarValorContrato(decimal pValorContrato, int pCodigo, int pCodigoCliente = 0)
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
                s = s + "update Contratos \n";
                if (pValorContrato == 0)
                    s = s + "set ValorContrato = 0 \n";
                else
                    s = s + "set ValorContrato = " + pValorContrato.ToString().Replace(",", ".") + " \n";
                s = s + "where Codigo = " + pCodigo.ToString() + " \n";
                if (pCodigoCliente > 0)
                    s = s + "and    CodigoCliente = " + pCodigoCliente + " \n ";
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
        public string AlterarDataProximoReajuste(string pDataProximoReajuste, int pCodigo, int pCodigoCliente = 0)
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
                s = s + "update Contratos \n";
                if (pDataProximoReajuste == "")
                    s = s + "set DataReajuste = '01-01-0100' \n";
                else
                    s = s + "set DataReajuste = '" + Convert.ToDateTime(pDataProximoReajuste).ToString("yyyy-MM-dd") + "' \n";
                s = s + "where Codigo = " + pCodigo.ToString() + " \n";
                if (pCodigoCliente > 0)
                    s = s + "and    CodigoCliente = " + pCodigoCliente + " \n ";
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
        public DataTable PreencheDataTableContratos(string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select t.Codigo, t.CodigoCliente, DescricaoHistorico, DiaVencimento, NumeroContrato, ValorContrato, DataTermino, \n";
                s = s + "       NumeroCaixasLocadas, DataInicio, DataReajuste, IndiceReajuste, DataRecisao, SituacaoRecisao, MotivoRescisao, \n";
                s = s + "       Observacao, c.Nome, c.NomeFantasia \n";
                s = s + "from   Contratos t \n";
                s = s + "inner join Clientes c on c.Codigo = t.CodigoCliente \n ";
                s = s + "order by " + pOrdem;
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
        public DataTable PreencheDataTableContratos(string pOrdem, string pFiltro, string pCampo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select t.Codigo, t.CodigoCliente, DescricaoHistorico, DiaVencimento, NumeroContrato, ValorContrato, DataTermino, \n";
                s = s + "       NumeroCaixasLocadas, DataInicio, DataReajuste, IndiceReajuste, DataRecisao, SituacaoRecisao, MotivoRescisao, \n";
                s = s + "       Observacao, c.Nome, c.NomeFantasia, t.Particularidade \n";
                s = s + "from   Contratos t \n";
                s = s + "inner join Clientes c on c.Codigo = t.CodigoCliente \n ";
                //if (pOrdem.ToUpper() == "Codigo asc".ToUpper() || pOrdem.ToUpper() == "Codigo desc".ToUpper() || pOrdem.ToUpper() == "Codigo".ToUpper())
                //{
                //    s = s + "order by t." + pOrdem + " \n";
                //}
                //else
                //{
                if (pCampo == "CodigoCliente" && pFiltro != "")
                    s = s + "where " + pCampo + " = " + pFiltro + " \n";
                else if (pCampo == "DataRecisao" && pFiltro == "") // contratos abertos
                    s = s + "where (DataRecisao = '0100-01-01' or DataRecisao is null) \n";
                else if (pCampo == "DataRecisao" && pFiltro == ">0100-01-01") // contratos cancelados
                    s = s + "where (DataRecisao > '0100-01-01') \n";
                else if (pCampo == "Codigo" || pCampo == "Código Contrato")
                {
                    if (pFiltro == "")
                        s = s + "where t.Codigo = -1 \n";
                    else
                        s = s + "where t.Codigo = " + pFiltro + "\n";
                }
                else
                    s = s + "where " + pCampo + " like '%" + pFiltro + "%' \n";
                s = s + "order by " + pOrdem + " \n";
                //}
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
        public DataTable PreencheDataTableContratos(int pCodigoCliente)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select t.Codigo, t.CodigoCliente, DescricaoHistorico, DiaVencimento, NumeroContrato, ValorContrato, DataTermino, \n";
                s = s + "       NumeroCaixasLocadas, DataInicio, DataReajuste, IndiceReajuste, DataRecisao, SituacaoRecisao, \n";
                s = s + "       Observacao, c.Nome, c.NomeFantasia, t.Particularidade \n";
                s = s + "from   Contratos t \n";
                s = s + "inner join Clientes c on c.Codigo = t.CodigoCliente \n ";
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
                    s = s + "delete from Contratos ";
                    s = s + "where  Codigo = " + pCodigo.ToString();
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

        public bool ExisteContratoEmAberto(int pCodigoCliente)
        {
            bool bRet = false;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Codigo \n";
                s = s + "from   Contratos \n";
                s = s + "where  (DataRecisao = '0100-01-01' or DataRecisao = '0001-01-01' or DataRecisao is null) \n";
                if (pCodigoCliente > 0)
                    s = s + "and  CodigoCliente = " + pCodigoCliente + " \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    bRet = true;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return bRet;
        }
    }
}