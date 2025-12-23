using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using MySql.Data;
using MySql.Data.MySqlClient;
using SILCNegocios;

namespace LibSILC
{
	public class clsClienteDados
	{
	    private clsDB oDB = new clsDB();
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
            MySqlDataAdapter l_myData = new MySqlDataAdapter();
            l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
            l_myData.Fill(l_ds);
            l_dt = l_ds.Tables[0];
        }
        private void DesconectaBanco()
        {
            oDB.DesconectaMySql();
        }

        public clsClientes PegaDadosPeloCNPJFaturamento(clsClientes pClientes, string pCNPJ_Faturamento)
        {
            clsClientes _cliente = new clsClientes();             
            int _codigoCliente = PegaCodigoPeloCNPJ_Faturamento(pCNPJ_Faturamento);
            PegaDados(_cliente, _codigoCliente);
            return _cliente;
        }
        public clsClientes PegaDados(clsClientes pClientes, int pCodigo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Nome, Pessoa, Nome2, NomeFantasia, NomeDaEmpresa, DataNascimento, CEP, CNPJ_CPF, RG_IE, RGEmit, dtRG,  \n";
                s = s + "       email, site, Naturalidade, Nacionalidade, OBS, KmMedia, TiposDeContrato, Percentual, Classificacao, Filial, ContaGerencial, CodigoMunicipioObra, CodigoMunicipioNF, \n";
                s = s + "       CodigoClienteExportacao, CodigoNoAterro, DataCadastro, Inativo, NovaSenha, DataSenha, SolicitadoSenhaPor, NaoAceitaDiferencaPeso, PontoReferencia, CodigoSituacaoTributaria, \n";
                s = s + "       CodigoBROOKS_Retencoes, EnviarDDR, EnviarCDF, SenhaMasterFatima, SenhaAcessoFatima, ObsFatima, ContatoFatima, TelefoneFatima, emailFatma, CNPJ_Faturamento,  \n";
                s = s + "       ClienteEmissaoMTReRCD, CodigoTipoCobranca, CodigoFuncionarioComercial, CodigoUnidadeDoIMA \n";
                s = s + "from   Clientes \n";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " ";
                    s = s + "limit 1";
                }
                else if (pCodigo == 0)
                {
                    s = s + " order by Nome limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    pClientes.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                    pClientes.Nome = l_dt.Rows[0]["Nome"].ToString();
                    if (l_dt.Rows[0]["Pessoa"].ToString() != "")
                        pClientes.Pessoa = Convert.ToInt16(l_dt.Rows[0]["Pessoa"]);
                    pClientes.Nome2 = l_dt.Rows[0]["Nome2"].ToString();
                    pClientes.NomeFantasia = l_dt.Rows[0]["NomeFantasia"].ToString();
                    pClientes.NomeDaEmpresa = l_dt.Rows[0]["NomeDaEmpresa"].ToString();
                    if (l_dt.Rows[0]["DataNascimento"].ToString() != "")
                        pClientes.DataNascimento = Convert.ToDateTime(l_dt.Rows[0]["DataNascimento"].ToString()).Date.ToShortDateString();
                    pClientes.CEP = l_dt.Rows[0]["CEP"].ToString();
                    pClientes.CNPJ_CPF = l_dt.Rows[0]["CNPJ_CPF"].ToString();
                    pClientes.RG_IE = l_dt.Rows[0]["RG_IE"].ToString();
                    pClientes.RGEmit = l_dt.Rows[0]["RGEmit"].ToString();
                    if (l_dt.Rows[0]["dtRG"].ToString() != "")
                        pClientes.dtRG = Convert.ToDateTime(l_dt.Rows[0]["dtRG"].ToString()).Date.ToShortDateString();
                    pClientes.email = l_dt.Rows[0]["email"].ToString();
                    pClientes.site = l_dt.Rows[0]["site"].ToString();
                    pClientes.Naturalidade = l_dt.Rows[0]["Naturalidade"].ToString();
                    pClientes.OBS = l_dt.Rows[0]["OBS"].ToString();
                    if (l_dt.Rows[0]["KmMedia"].ToString() != "")
                        pClientes.KmMedia = Convert.ToInt32(l_dt.Rows[0]["KmMedia"]);
                    if (l_dt.Rows[0]["TiposDeContrato"].ToString() != "")
                        pClientes.TiposDeContrato = Convert.ToInt16(l_dt.Rows[0]["TiposDeContrato"]);
                    if (l_dt.Rows[0]["Percentual"].ToString() != "")
                        pClientes.Percentual = Convert.ToDecimal(l_dt.Rows[0]["Percentual"]);
                    if (l_dt.Rows[0]["Classificacao"].ToString() != "")
                        pClientes.Classificacao = Convert.ToInt32(l_dt.Rows[0]["Classificacao"]);
                    if (l_dt.Rows[0]["Filial"].ToString() != "")
                        pClientes.Filial = Convert.ToInt16(l_dt.Rows[0]["Filial"]);
                    if (l_dt.Rows[0]["ContaGerencial"].ToString() != "")
                        pClientes.ContaGerencial = Convert.ToInt32(l_dt.Rows[0]["ContaGerencial"]);
                    if (l_dt.Rows[0]["CodigoMunicipioObra"].ToString() != "")
                        pClientes.CodigoMunicipioObra = Convert.ToInt32(l_dt.Rows[0]["CodigoMunicipioObra"]);
                    if (l_dt.Rows[0]["CodigoMunicipioNF"].ToString() != "")
                        pClientes.CodigoMunicipioNF = Convert.ToInt32(l_dt.Rows[0]["CodigoMunicipioNF"]);
                    if (l_dt.Rows[0]["CodigoClienteExportacao"].ToString() != "")
                        pClientes.CodigoClienteExportacao = Convert.ToInt32(l_dt.Rows[0]["CodigoClienteExportacao"]);
                    if (l_dt.Rows[0]["CodigoNoAterro"].ToString() != "")
                        pClientes.CodigoNoAterro = Convert.ToInt32(l_dt.Rows[0]["CodigoNoAterro"]);
                    if (l_dt.Rows[0]["DataCadastro"].ToString() != "")
                    {
                        pClientes.DataCadastro = Convert.ToDateTime(l_dt.Rows[0]["DataCadastro"].ToString()).Date.ToShortDateString();
                        if (pClientes.DataCadastro == "01/01/0100" || pClientes.DataCadastro == "1/1/100" || pClientes.DataCadastro == "01/01/0001")
                            pClientes.DataCadastro = "";
                    }
                    if (l_dt.Rows[0]["Inativo"].ToString() != "")
                        pClientes.Inativo = Convert.ToInt16(l_dt.Rows[0]["Inativo"]);
                    pClientes.NovaSenha = l_dt.Rows[0]["NovaSenha"].ToString();
                    if (l_dt.Rows[0]["DataSenha"].ToString() != "")
                        pClientes.DataSenha = Convert.ToDateTime(l_dt.Rows[0]["DataSenha"].ToString()).Date.ToShortDateString();
                    pClientes.SolicitadoSenhaPor = l_dt.Rows[0]["SolicitadoSenhaPor"].ToString();
                    if (l_dt.Rows[0]["NaoAceitaDiferencaPeso"].ToString() != "")
                        pClientes.NaoAceitaDiferencaPeso = Convert.ToInt16(l_dt.Rows[0]["NaoAceitaDiferencaPeso"]);
                    pClientes.PontoReferencia = l_dt.Rows[0]["PontoReferencia"].ToString();
                    if (l_dt.Rows[0]["CodigoSituacaoTributaria"].ToString() != "")
                        pClientes.CodigoSituacaoTributaria = Convert.ToInt16(l_dt.Rows[0]["CodigoSituacaoTributaria"]);
                    pClientes.CodigoBROOKS_Retencoes = l_dt.Rows[0]["CodigoBROOKS_Retencoes"].ToString();
                    if (l_dt.Rows[0]["EnviarDDR"].ToString() != "")
                        pClientes.EnviarDDR = Convert.ToInt16(l_dt.Rows[0]["EnviarDDR"]);
                    if (l_dt.Rows[0]["EnviarCDF"].ToString() != "")
                        pClientes.EnviarCDF = Convert.ToInt16(l_dt.Rows[0]["EnviarCDF"]);
                    pClientes.SenhaMasterFatima = l_dt.Rows[0]["SenhaMasterFatima"].ToString();
                    pClientes.SenhaAcessoFatima = l_dt.Rows[0]["SenhaAcessoFatima"].ToString();
                    pClientes.ObsFatima = l_dt.Rows[0]["ObsFatima"].ToString();
                    pClientes.ContatoFatima = l_dt.Rows[0]["ContatoFatima"].ToString();
                    pClientes.TelefoneFatima = l_dt.Rows[0]["TelefoneFatima"].ToString();
                    pClientes.emailFatma = l_dt.Rows[0]["emailFatma"].ToString();
                    pClientes.CNPJ_Faturamento = l_dt.Rows[0]["CNPJ_Faturamento"].ToString();
                    if (l_dt.Rows[0]["ClienteEmissaoMTReRCD"].ToString() != "")
                        pClientes.ClienteEmissaoMTReRCD = Convert.ToInt16(l_dt.Rows[0]["ClienteEmissaoMTReRCD"]);
                    if (l_dt.Rows[0]["CodigoTipoCobranca"].ToString() != "")
                        pClientes.CodigoTipoCobranca = Convert.ToInt16(l_dt.Rows[0]["CodigoTipoCobranca"]);
                    if (l_dt.Rows[0]["CodigoFuncionarioComercial"].ToString() != "")
                        pClientes.CodigoFuncionarioComercial = Convert.ToInt16(l_dt.Rows[0]["CodigoFuncionarioComercial"]);
                    if (l_dt.Rows[0]["CodigoUnidadeDoIMA"].ToString() != "")
                        pClientes.CodigoUnidadeDoIMA = Convert.ToInt32(l_dt.Rows[0]["CodigoUnidadeDoIMA"]);
                }
            }
            catch (Exception ex)
            {
                return new clsClientes();
            }
            finally
            {
                DesconectaBanco();
            }
            return pClientes;
        }       
        public int PegaTamanhoCampoVarChar(string pCampo)
        {
            int iRet = 0;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "show columns ";
                s = s + "from Clientes ";
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
                s = s + "from   Clientes  ";
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
        public bool ExisteCNPJ_CPF(string pCNPJ_CPF)
        {
            bool bRet = false;
            try
            {
                if (pCNPJ_CPF.Length >= 11)
                {
                    oDB.ConectaMySql();
                    s = "";
                    s = s + "select CNPJ_CPF ";
                    s = s + "from   Clientes  ";
                    s = s + "where  CNPJ_CPF like '" + pCNPJ_CPF + "'";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pCNPJ_CPF != "")
                    bRet = true;
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
        public string PegaUltimoCNPJComLetra(string pCNPJ_CPF)
        {
            string sRet = "";
            try
            {
                if (pCNPJ_CPF != "")
                {
                    oDB.ConectaMySql();
                    s = "";
                    s = s + "select CNPJ_CPF \n";
                    s = s + "from   Clientes  \n";
                    s = s + "where  CNPJ_CPF like '" + pCNPJ_CPF + "%' \n";
                    s = s + "order  by CNPJ_CPF desc \n";
                    FillDataSet();
                    if (l_dt.Rows.Count > 0)
                    {
                        sRet = l_dt.Rows[0][0].ToString();
                    }
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
        public void Inserir(clsClientes pClientes, int pCodigo = 0)
        {
            try
            {
                s = "";
                s = s + "insert into Clientes \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + "  Codigo, \n";
                s = s + "  Nome, Pessoa, Nome2, NomeFantasia, NomeDaEmpresa, DataNascimento, CEP, CNPJ_CPF, RG_IE, RGEmit, dtRG,  \n";
                s = s + "  email, site, Naturalidade, Nacionalidade, OBS, KmMedia, TiposDeContrato, Percentual, Classificacao, Filial, ContaGerencial, CodigoMunicipioObra, CodigoMunicipioNF, \n";
                s = s + "  CodigoClienteExportacao, CodigoNoAterro, DataCadastro, Inativo, NovaSenha, DataSenha, SolicitadoSenhaPor, NaoAceitaDiferencaPeso, PontoReferencia, CodigoSituacaoTributaria, \n";
                s = s + "  EnviarDDR, EnviarCDF, SenhaMasterFatima, SenhaAcessoFatima, ObsFatima, ContatoFatima, TelefoneFatima, emailFatma, ClienteEmissaoMTReRCD, CodigoTipoCobranca, CodigoFuncionarioComercial, \n";
                s = s + "  CodigoBROOKS_Retencoes, CodigoUnidadeDoIMA \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + pCodigo + ", \n";
                s = s + "'" + geral.Left(pClientes.Nome, 50) + "', \n";
                if (pClientes.Pessoa > 0)
                    s = s + " " + pClientes.Pessoa + ", \n";
                else
                    s = s + "0, \n";
                s = s + "'" + pClientes.Nome2 + "', \n";
                s = s + "'" + pClientes.NomeFantasia + "', \n";
                s = s + "'" + pClientes.NomeDaEmpresa + "', \n";
                if (pClientes.DataNascimento != "")
                    s = s + "'" + Convert.ToDateTime(pClientes.DataNascimento).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                s = s + "'" + pClientes.CEP + "', \n";
                s = s + "'" + pClientes.CNPJ_CPF + "', \n";
                s = s + "'" + pClientes.RG_IE + "', \n";
                s = s + "'" + pClientes.RGEmit + "', \n";
                if (pClientes.dtRG != "")
                    s = s + "'" + Convert.ToDateTime(pClientes.dtRG).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                s = s + "'" + pClientes.email + "', \n";
                s = s + "'" + pClientes.site  + "', \n";
                s = s + "'" + pClientes.Naturalidade + "', \n";
                s = s + "'" + pClientes.Nacionalidade + "', \n";
                s = s + "'" + pClientes.OBS + "', \n";
                if (pClientes.KmMedia > 0)
                    s = s + " " + pClientes.KmMedia + ", \n";
                else
                    s = s + "0, \n";
                if (pClientes.TiposDeContrato > 0)
                    s = s + " " + pClientes.TiposDeContrato + ", \n";
                else
                    s = s + "0, \n";
                if (pClientes.Percentual > 0)
                    s = s + " " + pClientes.Percentual + ", \n";
                else
                    s = s + "0, \n";
                if (pClientes.Classificacao > 0)
                    s = s + " " + pClientes.Classificacao + ", \n";
                else
                    s = s + "0, \n";
                if (pClientes.Filial > 0)
                    s = s + " " + pClientes.Filial + ", \n";
                else
                    s = s + "0, \n";
                if (pClientes.ContaGerencial > 0)
                    s = s + " " + pClientes.ContaGerencial + ", \n";
                else
                    s = s + "0, \n";
                if (pClientes.CodigoMunicipioObra > 0)
                    s = s + " " + pClientes.CodigoMunicipioObra + ", \n";
                else
                    s = s + "0, \n";
                if (pClientes.CodigoMunicipioNF > 0)
                    s = s + " " + pClientes.CodigoMunicipioNF + ", \n";
                else
                    s = s + "0, \n";
                if (pClientes.CodigoClienteExportacao > 0)
                    s = s + " " + pClientes.CodigoClienteExportacao + ", \n";
                else
                    s = s + "0, \n";
                if (pClientes.CodigoNoAterro > 0)
                    s = s + " " + pClientes.CodigoNoAterro + ", \n";
                else
                    s = s + "0, \n";
                if (pClientes.DataCadastro != "")
                    s = s + "'" + Convert.ToDateTime(pClientes.DataCadastro).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                if (pClientes.Inativo > 0)
                    s = s + " " + pClientes.Inativo + ", \n";
                else
                    s = s + "0, \n";
                s = s + "'" + pClientes.NovaSenha + "', \n";
                if (pClientes.DataSenha != "")
                    s = s + "'" + Convert.ToDateTime(pClientes.DataSenha).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                s = s + "'" + pClientes.SolicitadoSenhaPor + "', \n";
                if (pClientes.NaoAceitaDiferencaPeso > 0)
                    s = s + " " + pClientes.NaoAceitaDiferencaPeso + ", \n";
                else
                    s = s + "0, \n";
                s = s + "'" + pClientes.PontoReferencia + "', \n";
                if (pClientes.CodigoSituacaoTributaria > 0)
                    s = s + " " + pClientes.CodigoSituacaoTributaria + ", \n";
                else
                    s = s + "0, \n";
                s = s + " " + pClientes.EnviarCDF + ", \n";
                s = s + " " + pClientes.EnviarDDR + ", \n";
                s = s + "'" + pClientes.SenhaMasterFatima + "', \n";
                s = s + "'" + pClientes.SenhaAcessoFatima + "', \n";
                s = s + "'" + pClientes.ObsFatima + "', \n";
                s = s + "'" + pClientes.ContatoFatima + "', \n";
                s = s + "'" + pClientes.TelefoneFatima + "', \n";
                s = s + "'" + pClientes.emailFatma + "', \n";
                s = s + " " + pClientes.ClienteEmissaoMTReRCD.ToString() + ", \n";
                s = s + " " + pClientes.CodigoTipoCobranca + ", \n";
                s = s + " " + pClientes.CodigoFuncionarioComercial + ", \n";
                s = s + "'" + pClientes.CodigoBROOKS_Retencoes + "', \n";
                if (pClientes.CodigoUnidadeDoIMA > 0)
                    s = s + " " + pClientes.CodigoUnidadeDoIMA + " \n";
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
        public string Alterar(clsClientes pClientes, int pCodigo)
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
                s = s + "update Clientes \n";
                s = s + "set Nome          = '" + pClientes.Nome + "', \n";
                s = s + "    Pessoa        =  " + pClientes.Pessoa + ", \n";
                s = s + "    Nome2         = '" + pClientes.Nome2 + "', \n";
                s = s + "    NomeFantasia  = '" + pClientes.NomeFantasia + "', \n";
                s = s + "    NomeDaEmpresa = '" + pClientes.NomeDaEmpresa + "', \n";
                s = s + "    DataNascimento= '" + Convert.ToDateTime(pClientes.DataNascimento).ToString("yyyy-MM-dd") + "', \n";
                s = s + "    CNPJ_CPF      = '" + pClientes.CNPJ_CPF + "', \n";
                s = s + "    RG_IE         = '" + pClientes.RG_IE + "', \n";
                s = s + "    RGEmit        = '" + pClientes.RGEmit + "', \n";
                s = s + "    dtRG          = '" + Convert.ToDateTime(pClientes.dtRG).ToString("yyyy-MM-dd") + "', \n";
                s = s + "    email         = '" + pClientes.email + "', \n";
                s = s + "    site          = '" + pClientes.site + "', \n";
                s = s + "    Naturalidade  = '" + pClientes.Naturalidade + "', \n";
                s = s + "    Nacionalidade = '" + pClientes.Nacionalidade + "', \n";
                s = s + "    OBS           = '" + pClientes.OBS + "', \n";
                s = s + "    KmMedia       =  " + pClientes.KmMedia + ", \n";
                s = s + "    TiposDeContrato= " + pClientes.TiposDeContrato + ", \n";
                s = s + "    Percentual    =  " + pClientes.Percentual.ToString().Replace(",", ".") + ", \n";
                s = s + "    Classificacao =  " + pClientes.Classificacao + ", \n";
                s = s + "    Filial        =  " + pClientes.Filial + ", \n";
                s = s + "    ContaGerencial           = " + pClientes.ContaGerencial + ", \n";
                s = s + "    CodigoMunicipioObra      = " + pClientes.CodigoMunicipioObra + ", \n";
                s = s + "    CodigoMunicipioNF        = " + pClientes.CodigoMunicipioNF + ", \n";
                s = s + "    CodigoClienteExportacao  = " + pClientes.CodigoClienteExportacao + ", \n";
                s = s + "    CodigoNoAterro           = " + pClientes.CodigoNoAterro + ", \n";
                s = s + "    DataCadastro             = '" + Convert.ToDateTime(pClientes.DataCadastro).ToString("yyyy-MM-dd") + "', \n";
                s = s + "    Inativo                  =  " + pClientes.Inativo + ", \n";
                s = s + "    NovaSenha                = '" + pClientes.NovaSenha + "', \n";
                s = s + "    DataSenha                = '" + Convert.ToDateTime(pClientes.DataSenha).ToString("yyyy-MM-dd") + "', \n";
                s = s + "    SolicitadoSenhaPor       = '" + pClientes.SolicitadoSenhaPor + "', \n";
                s = s + "    NaoAceitaDiferencaPeso   = "  + pClientes.NaoAceitaDiferencaPeso + ", \n";
                s = s + "    PontoReferencia          = '" + pClientes.PontoReferencia + "', \n";
                s = s + "    CodigoSituacaoTributaria = " + pClientes.CodigoSituacaoTributaria + ", \n";
                s = s + "    SenhaMasterFatima        = '" + pClientes.SenhaMasterFatima + "', \n";
                s = s + "    SenhaAcessoFatima        = '" + pClientes.SenhaAcessoFatima + "', \n";
                s = s + "    ObsFatima                = '" + pClientes.ObsFatima + "', \n";
                s = s + "    ContatoFatima            = '" + pClientes.ContatoFatima + "', \n";
                s = s + "    TelefoneFatima           = '" + pClientes.TelefoneFatima + "', \n";
                s = s + "    emailFatma               = '" + pClientes.emailFatma + "', \n";
                s = s + "    ClienteEmissaoMTReRCD    =  " + pClientes.ClienteEmissaoMTReRCD.ToString() + ", \n";
                s = s + "    CodigoTipoCobranca       =  " + pClientes.CodigoTipoCobranca.ToString() + ", \n";
                s = s + "    CodigoFuncionarioComercial = " + pClientes.CodigoFuncionarioComercial.ToString() + ", \n";
                s = s + "    CodigoBROOKS_Retencoes     = '" + pClientes.CodigoBROOKS_Retencoes + "', \n";
                s = s + "    CodigoUnidadeDoIMA         =  " + pClientes.CodigoUnidadeDoIMA + " \n";
                s = s + "where Codigo = " + pCodigo.ToString();
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
        public string AlterarCNPJ_Faturamento(string pCNPJ, int pCodigo)
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
                s = s + "update Clientes \n";
                s = s + "set    CNPJ_Faturamento = '" + pCNPJ + "' \n";
                s = s + "where  Codigo = " + pCodigo.ToString();
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
        public string AlterarParametroAliquotaFederal(string pParametro, string pCodigoCliente)
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
                s = s + "update Clientes \n";
                s = s + "set    CodigoBROOKS_Retencoes = '" + pParametro + "' \n";
                s = s + "where  Codigo = " + pCodigoCliente + " \n";
                s = s + "and    CodigoBROOKS_Retencoes is null" + " \n";
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
        public DataTable PreencheDataTableClientes(string pOrdem, string pFiltro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Nome, Pessoa, Nome2, NomeFantasia, NomeDaEmpresa, DataNascimento, CEP, CNPJ_CPF, RG_IE, RGEmit, dtRG,  \n";
                s = s + "       email, site, Naturalidade, Nacionalidade, OBS, KmMedia, TiposDeContrato, Percentual, Classificacao, Filial, ContaGerencial, CodigoMunicipioObra, CodigoMunicipioNF, \n";
                s = s + "       CodigoClienteExportacao, CodigoNoAterro, DataCadastro, Inativo, NovaSenha, DataSenha, SolicitadoSenhaPor, NaoAceitaDiferencaPeso, PontoReferencia, CodigoSituacaoTributaria, \n";
                s = s + "       CodigoBROOKS_Retencoes, EnviarDDR, EnviarCDF, SenhaMasterFatima, SenhaAcessoFatima, ObsFatima, ContatoFatima, TelefoneFatima, emailFatma, CNPJ_Faturamento, ClienteEmissaoMTReRCD, \n";
                s = s + "       CodigoTipoCobranca, CodigoFuncionarioComercial, CodigoUnidadeDoIMA \n";
                s = s + "from   Clientes \n";
                if (pFiltro == "Ativos" || pFiltro == "Ativo")
                    s = s + "where (Inativo = 0 or Inativo is null) \n ";
                else if (pFiltro == "Inativos" || pFiltro == "Inativo")
                    s = s + "where Inativo = 1  \n ";
                s = s + "order by " + pOrdem;
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
        public DataTable PreencheDataTableClientes(string pInCodigos)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Nome, Pessoa, Nome2, NomeFantasia, NomeDaEmpresa, DataNascimento, CEP, CNPJ_CPF, RG_IE, RGEmit, dtRG,  \n";
                s = s + "       email, site, Naturalidade, Nacionalidade, OBS, KmMedia, TiposDeContrato, Percentual, Classificacao, Filial, ContaGerencial, CodigoMunicipioObra, CodigoMunicipioNF, \n";
                s = s + "       CodigoClienteExportacao, CodigoNoAterro, DataCadastro, Inativo, NovaSenha, DataSenha, SolicitadoSenhaPor, NaoAceitaDiferencaPeso, PontoReferencia, CodigoSituacaoTributaria, \n";
                s = s + "       CodigoBROOKS_Retencoes, EnviarDDR, EnviarCDF, SenhaMasterFatima, SenhaAcessoFatima, ObsFatima, ContatoFatima, TelefoneFatima, emailFatma, CNPJ_Faturamento, \n";
                s = s + "       ClienteEmissaoMTReRCD, CodigoTipoCobranca, CodigoFuncionarioComercial, CodigoUnidadeDoIMA \n";
                s = s + "from   Clientes \n";
                s = s + "where  Codigo in (" + pInCodigos + ") \n";
                s = s + "order by Codigo asc \n";
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
        public DataTable PreencheDataTable(string pOrdem, string pFiltro, string pCampo, bool pSoAtivos = false)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Nome, Pessoa, Nome2, NomeFantasia, NomeDaEmpresa, DataNascimento, CEP, CNPJ_CPF, RG_IE, RGEmit, dtRG,  \n";
                s = s + "       email, site, Naturalidade, Nacionalidade, OBS, KmMedia, TiposDeContrato, Percentual, Classificacao, Filial, ContaGerencial, CodigoMunicipioObra, CodigoMunicipioNF, \n";
                s = s + "       CodigoClienteExportacao, CodigoNoAterro, DataCadastro, Inativo, NovaSenha, DataSenha, SolicitadoSenhaPor, NaoAceitaDiferencaPeso, PontoReferencia, CodigoSituacaoTributaria, \n";
                s = s + "       CodigoBROOKS_Retencoes, EnviarDDR, EnviarCDF, SenhaMasterFatima, SenhaAcessoFatima, ObsFatima, ContatoFatima, TelefoneFatima, emailFatma, CNPJ_Faturamento, \n";
                s = s + "       ClienteEmissaoMTReRCD, CodigoTipoCobranca, CodigoFuncionarioComercial, CodigoUnidadeDoIMA \n";
                if (pSoAtivos)
                    s = s + "   , (select Codigo from Contratos where CodigoCliente = c.Codigo and (DataRecisao is null or DataRecisao = '0100-01-01' or DataRecisao = '0001-01-01') limit 1) as Existe \n";
                s = s + "from   Clientes as c\n";
                if (pCampo == "Ativo" || pCampo == "Ativos" || pCampo == "Inativos")
                {
                    if (pCampo == "Ativo" || pCampo == "Ativos")
                        s = s + "where  (Inativo = 0 or Inativo is null) \n";
                    else if (pCampo == "Inativos")
                        s = s + "where  Inativo = 1 \n";
                }
                else
                {
                    s = s + "where  " + pCampo + " like '%" + pFiltro + "%' \n";
                    if (pSoAtivos)
                        s = s + "and   (Inativo = 0 or Inativo is null) \n";
                }
                s = s + "and   (CNPJ_CPF > '') \n ";
                s = s + "order  by " + pOrdem +" \n";
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

        public DataTable PreencheDataTable(string pOrdem, bool pAtivos, bool pTodos = false)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, DataCadastro, Inativo, Nome, NomeFantasia, CNPJ_CPF \n";
                s = s + "from   Clientes \n";
                if (pAtivos && !pTodos)
                    s = s + "where  (Inativo = 0 or Inativo is null) \n";
                else if (!pAtivos && !pTodos)
                    s = s + "where  Invativo = 1 \n";
                s = s + "order  by " + pOrdem + "  \n";
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
        public DataTable PreencheDTNomeFantasia(string pNomeFantasia)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, DataCadastro, Inativo, Nome, NomeFantasia \n";
                s = s + "from   Clientes \n";
                s = s + "where  NomeFantasia like '%"+  pNomeFantasia + "%' \n";
                s = s + "and    (Inativo = 0 or Inativo is null) \n";
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
        public DataTable PreencheDTNomeFantasia(int pCodigoCliente)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, DataCadastro, Inativo, Nome, NomeFantasia \n";
                s = s + "from   Clientes \n";
                s = s + "where  Codigo = " + pCodigoCliente.ToString() + " \n";
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
        public DataTable PreencheSoNomeFantasia(string pNomeFantasia, bool pAtivo = true)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select NomeFantasia, Codigo \n";
                s = s + "from   Clientes \n";
                s = s + "where  NomeFantasia like '" + pNomeFantasia + "%' \n";
                if (pAtivo)
                    s = s + "and    (Inativo = 0 or Inativo is null) \n";
                s = s + "order  by NomeFantasia \n";
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
        public DataTable PreencheNomeFantasiaComCodigo(int pCodigoCliente, bool pAtivo = true)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select NomeFantasia, Codigo \n";
                s = s + "from   Clientes \n";
                s = s + "where  Codigo = " + pCodigoCliente + " \n";
                if (pAtivo)
                    s = s + "and    (Inativo = 0 or Inativo is null) \n";
                s = s + "order  by NomeFantasia \n";
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
        public DataTable PreencheComNomeFantasia(string pNomeOuRazaoSocial)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select NomeFantasia, Codigo \n";
                s = s + "from   Clientes \n";
                s = s + "where  Nome like '" + pNomeOuRazaoSocial + "%' \n";
                s = s + "and    (Inativo = 0 or Inativo is null) \n";
                s = s + "order  by NomeFantasia \n";
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
        public DataTable PegaNomeFantasiaCodigo(int pCodigo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select NomeFantasia, Codigo \n";
                s = s + "from   Clientes \n";
                s = s + "where  Codigo = " + pCodigo + " \n";
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

        public string PegaNomeFantasia(int pCodigo)
        {
            string sRet = "";
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select NomeFantasia \n";
                s = s + "from   Clientes     \n";
                s = s + "where  Codigo = " + pCodigo + " \n";
                FillDataSet();
                DesconectaBanco();
                if (l_ds.Tables[0].Rows.Count > 0)
                    sRet = l_ds.Tables[0].Rows[0][0].ToString();
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
        public string PegaUltimoCodigoCadastrado(bool pConsiderarDataCadastro = true)
        {
            string sRet = "";
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo \n";
                s = s + "from   Clientes     \n";
                if (pConsiderarDataCadastro)
                    s = s + "where  DataCadastro = " + DateTime.Now.ToString("yyyyMMdd") + " \n";
                s = s + "order  by Codigo Desc \n";
                s = s + "limit  1 \n";
                FillDataSet();
                DesconectaBanco();
                if (l_ds.Tables[0].Rows.Count > 0)
                    sRet = l_ds.Tables[0].Rows[0][0].ToString();
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
        public int PegaCodigoPeloCNPJ_Faturamento(string pCNPJ_Faturamento = "")
        {
            int sRet = 0;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo \n";
                s = s + "from   Clientes     \n";
                s = s + "where  CNPJ_CPF = '" + pCNPJ_Faturamento + "' \n";
                s = s + "order  by Codigo Desc \n";
                s = s + "limit  1 \n";
                FillDataSet();
                DesconectaBanco();
                if (l_ds.Tables[0].Rows.Count > 0)
                {
                    if (l_ds.Tables[0].Rows[0][0].ToString() != "")
                        sRet = Convert.ToInt32(l_ds.Tables[0].Rows[0][0].ToString());
                }
            }
            catch (Exception ex)
            {
                DesconectaBanco();
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                sRet = 0;
            }
            return sRet;
        }

        public string PegaNovaSenhaNoSite(int pCodigo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select NovaSenha \n";
                s = s + "from   Clientes \n";
                s = s + "where  Codigo = " + pCodigo + " \n";
                FillDataSet();
                DesconectaBanco();
                if (l_ds.Tables[0].Rows.Count > 0)
                    return l_ds.Tables[0].Rows[0]["NovaSenha"].ToString();
                else
                    return "";
            }
            catch (Exception ex)
            {
                DesconectaBanco();
                return ex.Message;
            }
        }

        public string Excluir(int pCodigo, string pNome)
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
                s = s + "delete from Clientes ";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo.ToString();
                }
                else
                {
                    s = s + "where  Nome = '" + pNome + "'";
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
        public string[] PegaNomeCNPJPeloCNPJ(string pCNPJ_CPF, bool pBuscaExata = false)
        {
            string[] sRetNomeCNPJ = new string[3];
            sRetNomeCNPJ[0] = "";
            sRetNomeCNPJ[1] = "";
            sRetNomeCNPJ[2] = "";
            if (pCNPJ_CPF != "")
            {
                try
                {
                    oDB.ConectaMySql();
                    s = "";
                    s = s + "select Nome, CNPJ_CPF \n";
                    s = s + "from   Clientes \n";
                    if (pBuscaExata)
                        s = s + "where  CNPJ_CPF = '" + pCNPJ_CPF + "' \n";
                    else
                        s = s + "where  CNPJ_CPF like '" + pCNPJ_CPF + "%' \n";
                    FillDataSet();
                    if (l_dt.Rows.Count > 0)
                    {
                        sRetNomeCNPJ[0] = l_dt.Rows[0][0].ToString();
                        sRetNomeCNPJ[1] = l_dt.Rows[0][1].ToString();
                    }
                }
                catch (Exception ex)
                {
                    sRetNomeCNPJ[0] = "Erro: " + ex.Message;
                }
                finally
                {
                    oDB.DesconectaMySql();
                }
            }
            return sRetNomeCNPJ;
        }
    }
}