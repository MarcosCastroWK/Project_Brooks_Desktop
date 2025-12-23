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
	public class clsClienteHistoricoDados
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
        public clsClientesHistorico PegaDados(clsClientesHistorico pClientesHistorico, int pCodigo)
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
                s = s + "from   ClientesHistorico \n";
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
                    pClientesHistorico.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                    pClientesHistorico.Nome = l_dt.Rows[0]["Nome"].ToString();
                    if (l_dt.Rows[0]["Pessoa"].ToString() != "")
                        pClientesHistorico.Pessoa = Convert.ToInt16(l_dt.Rows[0]["Pessoa"]);
                    pClientesHistorico.Nome2 = l_dt.Rows[0]["Nome2"].ToString();
                    pClientesHistorico.NomeFantasia = l_dt.Rows[0]["NomeFantasia"].ToString();
                    pClientesHistorico.NomeDaEmpresa = l_dt.Rows[0]["NomeDaEmpresa"].ToString();
                    if (l_dt.Rows[0]["DataNascimento"].ToString() != "")
                        pClientesHistorico.DataNascimento = Convert.ToDateTime(l_dt.Rows[0]["DataNascimento"].ToString()).Date.ToShortDateString();
                    pClientesHistorico.CEP = l_dt.Rows[0]["CEP"].ToString();
                    pClientesHistorico.CNPJ_CPF = l_dt.Rows[0]["CNPJ_CPF"].ToString();
                    pClientesHistorico.RG_IE = l_dt.Rows[0]["RG_IE"].ToString();
                    pClientesHistorico.RGEmit = l_dt.Rows[0]["RGEmit"].ToString();
                    if (l_dt.Rows[0]["dtRG"].ToString() != "")
                        pClientesHistorico.dtRG = Convert.ToDateTime(l_dt.Rows[0]["dtRG"].ToString()).Date.ToShortDateString();
                    pClientesHistorico.email = l_dt.Rows[0]["email"].ToString();
                    pClientesHistorico.site = l_dt.Rows[0]["site"].ToString();
                    pClientesHistorico.Naturalidade = l_dt.Rows[0]["Naturalidade"].ToString();
                    pClientesHistorico.OBS = l_dt.Rows[0]["OBS"].ToString();
                    if (l_dt.Rows[0]["KmMedia"].ToString() != "")
                        pClientesHistorico.KmMedia = Convert.ToInt32(l_dt.Rows[0]["KmMedia"]);
                    if (l_dt.Rows[0]["TiposDeContrato"].ToString() != "")
                        pClientesHistorico.TiposDeContrato = Convert.ToInt16(l_dt.Rows[0]["TiposDeContrato"]);
                    if (l_dt.Rows[0]["Percentual"].ToString() != "")
                        pClientesHistorico.Percentual = Convert.ToDecimal(l_dt.Rows[0]["Percentual"]);
                    if (l_dt.Rows[0]["Classificacao"].ToString() != "")
                        pClientesHistorico.Classificacao = Convert.ToInt32(l_dt.Rows[0]["Classificacao"]);
                    if (l_dt.Rows[0]["Filial"].ToString() != "")
                        pClientesHistorico.Filial = Convert.ToInt16(l_dt.Rows[0]["Filial"]);
                    if (l_dt.Rows[0]["ContaGerencial"].ToString() != "")
                        pClientesHistorico.ContaGerencial = Convert.ToInt32(l_dt.Rows[0]["ContaGerencial"]);
                    if (l_dt.Rows[0]["CodigoMunicipioObra"].ToString() != "")
                        pClientesHistorico.CodigoMunicipioObra = Convert.ToInt32(l_dt.Rows[0]["CodigoMunicipioObra"]);
                    if (l_dt.Rows[0]["CodigoMunicipioNF"].ToString() != "")
                        pClientesHistorico.CodigoMunicipioNF = Convert.ToInt32(l_dt.Rows[0]["CodigoMunicipioNF"]);
                    if (l_dt.Rows[0]["CodigoClienteExportacao"].ToString() != "")
                        pClientesHistorico.CodigoClienteExportacao = Convert.ToInt32(l_dt.Rows[0]["CodigoClienteExportacao"]);
                    if (l_dt.Rows[0]["CodigoNoAterro"].ToString() != "")
                        pClientesHistorico.CodigoNoAterro = Convert.ToInt32(l_dt.Rows[0]["CodigoNoAterro"]);
                    if (l_dt.Rows[0]["DataCadastro"].ToString() != "")
                    {
                        pClientesHistorico.DataCadastro = Convert.ToDateTime(l_dt.Rows[0]["DataCadastro"].ToString()).Date.ToShortDateString();
                        if (pClientesHistorico.DataCadastro == "01/01/0100" || pClientesHistorico.DataCadastro == "1/1/100" || pClientesHistorico.DataCadastro == "01/01/0001")
                            pClientesHistorico.DataCadastro = "";
                    }
                    if (l_dt.Rows[0]["Inativo"].ToString() != "")
                        pClientesHistorico.Inativo = Convert.ToInt16(l_dt.Rows[0]["Inativo"]);
                    pClientesHistorico.NovaSenha = l_dt.Rows[0]["NovaSenha"].ToString();
                    if (l_dt.Rows[0]["DataSenha"].ToString() != "")
                        pClientesHistorico.DataSenha = Convert.ToDateTime(l_dt.Rows[0]["DataSenha"].ToString()).Date.ToShortDateString();
                    pClientesHistorico.SolicitadoSenhaPor = l_dt.Rows[0]["SolicitadoSenhaPor"].ToString();
                    if (l_dt.Rows[0]["NaoAceitaDiferencaPeso"].ToString() != "")
                        pClientesHistorico.NaoAceitaDiferencaPeso = Convert.ToInt16(l_dt.Rows[0]["NaoAceitaDiferencaPeso"]);
                    pClientesHistorico.PontoReferencia = l_dt.Rows[0]["PontoReferencia"].ToString();
                    if (l_dt.Rows[0]["CodigoSituacaoTributaria"].ToString() != "")
                        pClientesHistorico.CodigoSituacaoTributaria = Convert.ToInt16(l_dt.Rows[0]["CodigoSituacaoTributaria"]);
                    pClientesHistorico.CodigoBROOKS_Retencoes = l_dt.Rows[0]["CodigoBROOKS_Retencoes"].ToString();
                    if (l_dt.Rows[0]["EnviarDDR"].ToString() != "")
                        pClientesHistorico.EnviarDDR = Convert.ToInt16(l_dt.Rows[0]["EnviarDDR"]);
                    if (l_dt.Rows[0]["EnviarCDF"].ToString() != "")
                        pClientesHistorico.EnviarCDF = Convert.ToInt16(l_dt.Rows[0]["EnviarCDF"]);
                    pClientesHistorico.SenhaMasterFatima = l_dt.Rows[0]["SenhaMasterFatima"].ToString();
                    pClientesHistorico.SenhaAcessoFatima = l_dt.Rows[0]["SenhaAcessoFatima"].ToString();
                    pClientesHistorico.ObsFatima = l_dt.Rows[0]["ObsFatima"].ToString();
                    pClientesHistorico.ContatoFatima = l_dt.Rows[0]["ContatoFatima"].ToString();
                    pClientesHistorico.TelefoneFatima = l_dt.Rows[0]["TelefoneFatima"].ToString();
                    pClientesHistorico.emailFatma = l_dt.Rows[0]["emailFatma"].ToString();
                    pClientesHistorico.CNPJ_Faturamento = l_dt.Rows[0]["CNPJ_Faturamento"].ToString();
                    if (l_dt.Rows[0]["ClienteEmissaoMTReRCD"].ToString() != "")
                        pClientesHistorico.ClienteEmissaoMTReRCD = Convert.ToInt16(l_dt.Rows[0]["ClienteEmissaoMTReRCD"]);
                    if (l_dt.Rows[0]["CodigoTipoCobranca"].ToString() != "")
                        pClientesHistorico.CodigoTipoCobranca = Convert.ToInt16(l_dt.Rows[0]["CodigoTipoCobranca"]);
                    if (l_dt.Rows[0]["CodigoFuncionarioComercial"].ToString() != "")
                        pClientesHistorico.CodigoFuncionarioComercial = Convert.ToInt16(l_dt.Rows[0]["CodigoFuncionarioComercial"]);
                    if (l_dt.Rows[0]["CodigoUnidadeDoIMA"].ToString() != "")
                        pClientesHistorico.CodigoUnidadeDoIMA = Convert.ToInt32(l_dt.Rows[0]["CodigoUnidadeDoIMA"]);
                }
            }
            catch (Exception ex)
            {
                return new clsClientesHistorico();
            }
            finally
            {
                DesconectaBanco();
            }
            return pClientesHistorico;
        }       
        public int PegaTamanhoCampoVarChar(string pCampo)
        {
            int iRet = 0;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "show columns ";
                s = s + "from ClientesHistorico ";
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
                s = s + "from   ClientesHistorico  ";
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
                    s = s + "from   ClientesHistorico  ";
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
                    s = s + "from   ClientesHistorico  \n";
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
        public void Inserir(clsClientesHistorico pClientesHistorico, int pCodigo = 0)
        {
            try
            {
                s = "";
                s = s + "insert into ClientesHistorico \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + "  Codigo, \n";
                s = s + "  Nome, Pessoa, Nome2, NomeFantasia, NomeDaEmpresa, DataNascimento, CEP, CNPJ_CPF, RG_IE, RGEmit, dtRG,  \n";
                s = s + "  email, site, Naturalidade, Nacionalidade, OBS, KmMedia, TiposDeContrato, Percentual, Classificacao, Filial, ContaGerencial, CodigoMunicipioObra, CodigoMunicipioNF, \n";
                s = s + "  CodigoClienteExportacao, CodigoNoAterro, DataCadastro, Inativo, NovaSenha, DataSenha, SolicitadoSenhaPor, NaoAceitaDiferencaPeso, PontoReferencia, CodigoSituacaoTributaria, \n";
                s = s + "  EnviarDDR, EnviarCDF, SenhaMasterFatima, SenhaAcessoFatima, ObsFatima, ContatoFatima, TelefoneFatima, emailFatma, ClienteEmissaoMTReRCD, CodigoTipoCobranca, CodigoFuncionarioComercial, \n";
                s = s + "  CodigoBROOKS_Retencoes, CodigoUnidadeDoIMA, DataAlteracao, Status, Usuario, CodigoCliente \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + pCodigo + ", \n";
                s = s + "'" + geral.Left(pClientesHistorico.Nome, 50) + "', \n";
                if (pClientesHistorico.Pessoa > 0)
                    s = s + " " + pClientesHistorico.Pessoa + ", \n";
                else
                    s = s + "0, \n";
                s = s + "'" + pClientesHistorico.Nome2 + "', \n";
                s = s + "'" + pClientesHistorico.NomeFantasia + "', \n";
                s = s + "'" + pClientesHistorico.NomeDaEmpresa + "', \n";
                if (pClientesHistorico.DataNascimento != "")
                    s = s + "'" + Convert.ToDateTime(pClientesHistorico.DataNascimento).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                s = s + "'" + pClientesHistorico.CEP + "', \n";
                s = s + "'" + pClientesHistorico.CNPJ_CPF + "', \n";
                s = s + "'" + pClientesHistorico.RG_IE + "', \n";
                s = s + "'" + pClientesHistorico.RGEmit + "', \n";
                if (pClientesHistorico.dtRG != "")
                    s = s + "'" + Convert.ToDateTime(pClientesHistorico.dtRG).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                s = s + "'" + pClientesHistorico.email + "', \n";
                s = s + "'" + pClientesHistorico.site  + "', \n";
                s = s + "'" + pClientesHistorico.Naturalidade + "', \n";
                s = s + "'" + pClientesHistorico.Nacionalidade + "', \n";
                s = s + "'" + pClientesHistorico.OBS + "', \n";
                if (pClientesHistorico.KmMedia > 0)
                    s = s + " " + pClientesHistorico.KmMedia + ", \n";
                else
                    s = s + "0, \n";
                if (pClientesHistorico.TiposDeContrato > 0)
                    s = s + " " + pClientesHistorico.TiposDeContrato + ", \n";
                else
                    s = s + "0, \n";
                if (pClientesHistorico.Percentual > 0)
                    s = s + " " + pClientesHistorico.Percentual + ", \n";
                else
                    s = s + "0, \n";
                if (pClientesHistorico.Classificacao > 0)
                    s = s + " " + pClientesHistorico.Classificacao + ", \n";
                else
                    s = s + "0, \n";
                if (pClientesHistorico.Filial > 0)
                    s = s + " " + pClientesHistorico.Filial + ", \n";
                else
                    s = s + "0, \n";
                if (pClientesHistorico.ContaGerencial > 0)
                    s = s + " " + pClientesHistorico.ContaGerencial + ", \n";
                else
                    s = s + "0, \n";
                if (pClientesHistorico.CodigoMunicipioObra > 0)
                    s = s + " " + pClientesHistorico.CodigoMunicipioObra + ", \n";
                else
                    s = s + "0, \n";
                if (pClientesHistorico.CodigoMunicipioNF > 0)
                    s = s + " " + pClientesHistorico.CodigoMunicipioNF + ", \n";
                else
                    s = s + "0, \n";
                if (pClientesHistorico.CodigoClienteExportacao > 0)
                    s = s + " " + pClientesHistorico.CodigoClienteExportacao + ", \n";
                else
                    s = s + "0, \n";
                if (pClientesHistorico.CodigoNoAterro > 0)
                    s = s + " " + pClientesHistorico.CodigoNoAterro + ", \n";
                else
                    s = s + "0, \n";
                if (pClientesHistorico.DataCadastro != "")
                    s = s + "'" + Convert.ToDateTime(pClientesHistorico.DataCadastro).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                if (pClientesHistorico.Inativo > 0)
                    s = s + " " + pClientesHistorico.Inativo + ", \n";
                else
                    s = s + "0, \n";
                s = s + "'" + pClientesHistorico.NovaSenha + "', \n";
                if (pClientesHistorico.DataSenha != "")
                    s = s + "'" + Convert.ToDateTime(pClientesHistorico.DataSenha).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                s = s + "'" + pClientesHistorico.SolicitadoSenhaPor + "', \n";
                if (pClientesHistorico.NaoAceitaDiferencaPeso > 0)
                    s = s + " " + pClientesHistorico.NaoAceitaDiferencaPeso + ", \n";
                else
                    s = s + "0, \n";
                s = s + "'" + pClientesHistorico.PontoReferencia + "', \n";
                if (pClientesHistorico.CodigoSituacaoTributaria > 0)
                    s = s + " " + pClientesHistorico.CodigoSituacaoTributaria + ", \n";
                else
                    s = s + "0, \n";
                s = s + " " + pClientesHistorico.EnviarCDF + ", \n";
                s = s + " " + pClientesHistorico.EnviarDDR + ", \n";
                s = s + "'" + pClientesHistorico.SenhaMasterFatima + "', \n";
                s = s + "'" + pClientesHistorico.SenhaAcessoFatima + "', \n";
                s = s + "'" + pClientesHistorico.ObsFatima + "', \n";
                s = s + "'" + pClientesHistorico.ContatoFatima + "', \n";
                s = s + "'" + pClientesHistorico.TelefoneFatima + "', \n";
                s = s + "'" + pClientesHistorico.emailFatma + "', \n";
                s = s + " " + pClientesHistorico.ClienteEmissaoMTReRCD.ToString() + ", \n";
                s = s + " " + pClientesHistorico.CodigoTipoCobranca + ", \n";
                s = s + " " + pClientesHistorico.CodigoFuncionarioComercial + ", \n";
                s = s + "'" + pClientesHistorico.CodigoBROOKS_Retencoes + "', \n";
                if (pClientesHistorico.CodigoUnidadeDoIMA > 0)
                    s = s + " " + pClientesHistorico.CodigoUnidadeDoIMA + ", \n";
                else
                    s = s + "0, \n";
                if (pClientesHistorico.DataAlteracao != "")
                    s = s + "'" + Convert.ToDateTime( pClientesHistorico.DataAlteracao).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                s = s + "'" + pClientesHistorico.Status + "', \n";
                s = s + "'" + pClientesHistorico.Usuario + "', \n";
                s = s + pClientesHistorico.CodigoCliente.ToString();
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
        public string Alterar(clsClientesHistorico pClientesHistorico, int pCodigo)
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
                s = s + "update ClientesHistorico \n";
                s = s + "set Nome          = '" + pClientesHistorico.Nome + "', \n";
                s = s + "    Pessoa        =  " + pClientesHistorico.Pessoa + ", \n";
                s = s + "    Nome2         = '" + pClientesHistorico.Nome2 + "', \n";
                s = s + "    NomeFantasia  = '" + pClientesHistorico.NomeFantasia + "', \n";
                s = s + "    NomeDaEmpresa = '" + pClientesHistorico.NomeDaEmpresa + "', \n";
                s = s + "    DataNascimento= '" + Convert.ToDateTime(pClientesHistorico.DataNascimento).ToString("yyyy-MM-dd") + "', \n";
                s = s + "    CNPJ_CPF      = '" + pClientesHistorico.CNPJ_CPF + "', \n";
                s = s + "    RG_IE         = '" + pClientesHistorico.RG_IE + "', \n";
                s = s + "    RGEmit        = '" + pClientesHistorico.RGEmit + "', \n";
                s = s + "    dtRG          = '" + Convert.ToDateTime(pClientesHistorico.dtRG).ToString("yyyy-MM-dd") + "', \n";
                s = s + "    email         = '" + pClientesHistorico.email + "', \n";
                s = s + "    site          = '" + pClientesHistorico.site + "', \n";
                s = s + "    Naturalidade  = '" + pClientesHistorico.Naturalidade + "', \n";
                s = s + "    Nacionalidade = '" + pClientesHistorico.Nacionalidade + "', \n";
                s = s + "    OBS           = '" + pClientesHistorico.OBS + "', \n";
                s = s + "    KmMedia       =  " + pClientesHistorico.KmMedia + ", \n";
                s = s + "    TiposDeContrato= " + pClientesHistorico.TiposDeContrato + ", \n";
                s = s + "    Percentual    =  " + pClientesHistorico.Percentual.ToString().Replace(",", ".") + ", \n";
                s = s + "    Classificacao =  " + pClientesHistorico.Classificacao + ", \n";
                s = s + "    Filial        =  " + pClientesHistorico.Filial + ", \n";
                s = s + "    ContaGerencial           = " + pClientesHistorico.ContaGerencial + ", \n";
                s = s + "    CodigoMunicipioObra      = " + pClientesHistorico.CodigoMunicipioObra + ", \n";
                s = s + "    CodigoMunicipioNF        = " + pClientesHistorico.CodigoMunicipioNF + ", \n";
                s = s + "    CodigoClienteExportacao  = " + pClientesHistorico.CodigoClienteExportacao + ", \n";
                s = s + "    CodigoNoAterro           = " + pClientesHistorico.CodigoNoAterro + ", \n";
                s = s + "    DataCadastro             = '" + Convert.ToDateTime(pClientesHistorico.DataCadastro).ToString("yyyy-MM-dd") + "', \n";
                s = s + "    Inativo                  =  " + pClientesHistorico.Inativo + ", \n";
                s = s + "    NovaSenha                = '" + pClientesHistorico.NovaSenha + "', \n";
                s = s + "    DataSenha                = '" + Convert.ToDateTime(pClientesHistorico.DataSenha).ToString("yyyy-MM-dd") + "', \n";
                s = s + "    SolicitadoSenhaPor       = '" + pClientesHistorico.SolicitadoSenhaPor + "', \n";
                s = s + "    NaoAceitaDiferencaPeso   = "  + pClientesHistorico.NaoAceitaDiferencaPeso + ", \n";
                s = s + "    PontoReferencia          = '" + pClientesHistorico.PontoReferencia + "', \n";
                s = s + "    CodigoSituacaoTributaria = " + pClientesHistorico.CodigoSituacaoTributaria + ", \n";
                s = s + "    SenhaMasterFatima        = '" + pClientesHistorico.SenhaMasterFatima + "', \n";
                s = s + "    SenhaAcessoFatima        = '" + pClientesHistorico.SenhaAcessoFatima + "', \n";
                s = s + "    ObsFatima                = '" + pClientesHistorico.ObsFatima + "', \n";
                s = s + "    ContatoFatima            = '" + pClientesHistorico.ContatoFatima + "', \n";
                s = s + "    TelefoneFatima           = '" + pClientesHistorico.TelefoneFatima + "', \n";
                s = s + "    emailFatma               = '" + pClientesHistorico.emailFatma + "', \n";
                s = s + "    ClienteEmissaoMTReRCD    =  " + pClientesHistorico.ClienteEmissaoMTReRCD.ToString() + ", \n";
                s = s + "    CodigoTipoCobranca       =  " + pClientesHistorico.CodigoTipoCobranca.ToString() + ", \n";
                s = s + "    CodigoFuncionarioComercial = " + pClientesHistorico.CodigoFuncionarioComercial.ToString() + ", \n";
                s = s + "    CodigoBROOKS_Retencoes     = '" + pClientesHistorico.CodigoBROOKS_Retencoes + "', \n";
                s = s + "    CodigoUnidadeDoIMA         =  " + pClientesHistorico.CodigoUnidadeDoIMA + " \n";
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
                s = s + "update ClientesHistorico \n";
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
                s = s + "update ClientesHistorico \n";
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
                s = s + "from   ClientesHistorico \n";
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
                s = s + "from   ClientesHistorico \n";
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
                s = s + "from   ClientesHistorico as c\n";
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
                s = s + "from   ClientesHistorico \n";
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
                s = s + "from   ClientesHistorico \n";
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
                s = s + "from   ClientesHistorico \n";
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
                s = s + "from   ClientesHistorico \n";
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
                s = s + "from   ClientesHistorico \n";
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
                s = s + "from   ClientesHistorico \n";
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
                s = s + "from   ClientesHistorico \n";
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
                s = s + "from   ClientesHistorico     \n";
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

        public string PegaNovaSenhaNoSite(int pCodigo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select NovaSenha \n";
                s = s + "from   ClientesHistorico \n";
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
                s = s + "delete from ClientesHistorico ";
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
            string[] sRetNomeCNPJ = new string[2];
            sRetNomeCNPJ[0] = "";
            sRetNomeCNPJ[1] = "";
            if (pCNPJ_CPF != "")
            {
                try
                {
                    oDB.ConectaMySql();
                    s = "";
                    s = s + "select Nome, CNPJ_CPF \n";
                    s = s + "from   ClientesHistorico \n";
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
        public int RetornaCodigoHistorico(int pCodigoHistoricoCliente, string pDataAlteracao, string pUsuario)
        {
            int _retCodigo = 0;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo \n";
                s = s + "from   ClientesHistorico \n";
                if (pCodigoHistoricoCliente > 0)
                {
                    s = s + "where  CodigoCliente = " + pCodigoHistoricoCliente + " \n";
                    if (pDataAlteracao != "")
                        s = s + "and    DataAlteracao <= '" + Convert.ToDateTime(pDataAlteracao).ToString("yyyy-MM-dd") + "' \n";
                    if (pUsuario != "")
                        s = s + "and    Usuario = '" + pUsuario + "' \n";
                    s = s + "order  by Codigo Desc limit 1 \n";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows.Count > 0)
                    {
                        if (l_dt.Rows[0][0].ToString() != "")
                            _retCodigo = Convert.ToInt32(l_dt.Rows[0][0]);
                    }
                }
            }
            catch (Exception ex)
            {
                return _retCodigo;
            }
            finally
            {
                DesconectaBanco();
            }
            return _retCodigo;
        }

    }
}