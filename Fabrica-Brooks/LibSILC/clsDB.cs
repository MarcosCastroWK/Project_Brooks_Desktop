using System;
using System.Data;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Configuration;
using ConfigurationSilc;

namespace LibSILC
{
    public class clsDB : IDisposable
    {
        public clsDB()
        {
            _silcConfig = new SilcConfigurationManager();
            //connectionString = "Persist Security Info=false;server=SERVIDOR;password=yes;uid=root;database=ewvs;pwd=BR**ks729;Connect Timeout=360;pooling=false;";
            connectionString = "Persist Security Info=false;server=SERVIDOR;password=yes;uid=root;database=brooks;pwd=admin;Connect Timeout=360;pooling=false;";
        }

        void IDisposable.Dispose() { }

        private MySqlConnection l_mySqlConnect;
        private MySqlTransaction l_mySQLTransaction;
        public string NomeDB = "";
        public string NomeServidor = "";
        public MySqlTransaction MySQLTransaction
        {
            get { return l_mySQLTransaction; }
            set { l_mySQLTransaction = value; }
        }
        public MySqlConnection MySqlConnect
        {
            get { return l_mySqlConnect; }
            set { l_mySqlConnect = value; }
        }
        private MySqlCommand l_mySqlCommad;
        public MySqlCommand MySqlCommad
        {
            get { return l_mySqlCommad; }
            set { l_mySqlCommad = value; }
        }
        private MySqlDataAdapter l_myData = new MySqlDataAdapter();
        private DataSet l_ds = new DataSet();
        private string s;
        private SilcConfigurationManager _silcConfig;
        private string connectionString;

        public void ConectaMySql()
        {
            l_mySqlConnect = new MySqlConnection(connectionString);

            if (l_mySqlConnect.State == ConnectionState.Closed)
                l_mySqlConnect.Open();


            //if (geral.BancoUsado == 3) // 1 teste local // 3 servidor/home brooks
            //{

            //    s = "Persist Security Info=false;server=localhost;uid=root;database=ewvs;pwd=123456;Connect Timeout=360;pooling=false;";
            //    NomeDB = "ewvs";
            //    NomeServidor = "localhost";
            //    //página uol host
            //    //s = "Persist Security Info=false;server=ewvs.mysql.uhserver.com;password=yes;uid=ewvs;database=ewvs;pwd=SA12vi01*";
            //    //NomeDB = "ewvs";

            //    //brooks
            //    //NomeServidor = "localhost";
            //    //s = "Persist Security Info=false;server=localhost;port=3307;uid=root;database=ewvs;pwd=BR**ks729"; // local
            //    //NomeDB = "ewvs";

            //    l_mySqlConnect = new MySqlConnection(s);
            //    try
            //    {
            //        if (l_mySqlConnect.State == ConnectionState.Closed)
            //            l_mySqlConnect.Open();
            //    }
            //    catch (Exception ex)
            //    {
            //        s = "Erro db";
            //    }
            //}
            //else if (geral.BancoUsado == 1 || geral.BancoUsado == 2)
            //{
            //    NomeServidor = "SERVIDOR"; //local: "SERVIDOR"; VIVO: 186.215.191.140; NET: "189.4.81.28";  uol host: "200.98.129.47"; INFORMAC: "132.255.28.192"
            //    if (geral.BancoUsado == 1)
            //        NomeDB = "ewvs";
            //    else if (geral.BancoUsado == 2)
            //        NomeDB = "test";
            //    s = "Persist Security Info=false;server=" + NomeServidor + ";password=yes;uid=root;database=" + NomeDB + ";pwd=BR**ks729;Connect Timeout=360;pooling=false;";
            //    l_mySqlConnect = new MySqlConnection(s);
            //    try
            //    {
            //        if (l_mySqlConnect.State == ConnectionState.Closed)
            //        {
            //            l_mySqlConnect.Open();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        NomeServidor = "132.255.28.192"; 
            //        s = "Persist Security Info=false;server=" + NomeServidor + ";password=yes;uid=root;database=" + NomeDB + ";pwd=BR**ks729;Connect Timeout=360;pooling=false;";
            //        l_mySqlConnect = new MySqlConnection(s);
            //        l_mySqlConnect.Open();
            //    }
            //}
            //else if (geral.BancoUsado == 4) // fastcompost
            //{
            //    NomeDB = "fastcompost";
            //    //NomeServidor = "localhost";
            //    NomeServidor = "SERVIDOR"; //local: "SERVIDOR"; VIVO: 186.215.191.140; NET: "189.4.81.28";  uol host: "200.98.129.47"; INFORMAC: "132.255.28.192"
            //    //s = "Persist Security Info=false;server=localhost;uid=root;database=fastcompost;pwd=654321;Connect Timeout=280;pooling=false;";
            //    s = "Persist Security Info=false;server=" + NomeServidor + ";password=yes;uid=root;database=" + NomeDB + ";pwd=BR**ks729;Connect Timeout=180;pooling=false;";
            //    l_mySqlConnect = new MySqlConnection(s);
            //    try
            //    {
            //        if (l_mySqlConnect.State == ConnectionState.Closed)
            //            l_mySqlConnect.Open();
            //    }
            //    catch (Exception ex)
            //    {
            //        s = "Erro db";
            //    }
            //}

        }
        public void DesconectaMySql()
        {
            if (l_mySqlConnect.State == ConnectionState.Open)
            {
                l_mySqlConnect.Close();
                l_mySqlConnect.Dispose();
            }
        }

        private Boolean ExistsTable(string pTabelaNome)
        {
            ConectaMySql();
            Boolean l_vf;
            s = "";
            s = s + "SELECT table_name ";
            s = s + "FROM   information_schema.Tables ";
            s = s + "WHERE  table_schema = 'entrega' ";
            s = s + "AND    table_name = '" + pTabelaNome + "' ;";
            DataTable l_dt = new DataTable();
            l_myData = new MySqlDataAdapter(s, l_mySqlConnect);
            l_ds = new DataSet();
            l_myData.Fill(l_dt);
            DesconectaMySql();
            if (l_dt.Rows.Count == 0)
            {
                l_vf = false;
            }
            else
            {
                l_vf = true;
            }
            return l_vf;
        }

        public DataTable ConsultaSQL(string pSQL)
        {
            ConectaMySql();
            DataTable l_dt = new DataTable();
            l_myData = new MySqlDataAdapter(pSQL, l_mySqlConnect);
            l_ds = new DataSet();
            l_myData.Fill(l_dt);
            DesconectaMySql();
            return l_dt;
        }

        public int RegistrosTabela(string pTabelaNome, string pCampo)
        {
            ConectaMySql();
            int l_regs = 0;
            s = "";
            s = s + "SELECT " + pCampo + " \n";
            s = s + "FROM   " + pTabelaNome + "   \n";
            s = s + "ORDER BY Codigo DESC limit 1 \n";
            DataTable l_dt = new DataTable();
            l_myData = new MySqlDataAdapter(s, l_mySqlConnect);
            l_ds = new DataSet();
            l_myData.Fill(l_dt);
            DesconectaMySql();
            if (l_dt.Rows.Count > 0)
            {
                l_regs = Convert.ToInt32(l_dt.Rows[0][0]);
            }
            else
            {
                l_regs = 0;
            }
            return l_regs;
        }

        private Boolean ExisteCampoTabela(string pTabelaNome, string pCampoNome)
        {
            ConectaMySql();
            Boolean l_vf;
            s = "";
            s = s + "SHOW COLUMNS ";
            s = s + "FROM " + pTabelaNome + " ";
            s = s + "WHERE  field = '" + pCampoNome + "' ";
            DataTable l_dt = new DataTable();
            l_myData = new MySqlDataAdapter(s, l_mySqlConnect);
            l_ds = new DataSet();
            l_myData.Fill(l_dt);
            DesconectaMySql();
            if (l_dt.Rows.Count == 0)
            {
                l_vf = false;
            }
            else
            {
                l_vf = true;
            }
            return l_vf;
        }

        public DataTable TabelasDB()
        {
            ConectaMySql();
            s = "SHOW TABLES";
            DataTable l_dt = new DataTable();
            l_myData = new MySqlDataAdapter(s, l_mySqlConnect);
            l_ds = new DataSet();
            l_myData.Fill(l_dt);
            DesconectaMySql();
            return l_dt;
        }

        public void CriarTabelaCliente()
        {
            if (!ExistsTable("Clientes"))
            {
                ConectaMySql();
                s = "";
                s = s + "CREATE TABLE Clientes ";
                s = s + "(";
                s = s + " Codigo          INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY, ";
                s = s + " Bairro          VARCHAR(40), ";
                s = s + " Celular1        VARCHAR(40), ";
                s = s + " Celular2        VARCHAR(40), ";
                s = s + " CEP             VARCHAR(9), ";
                s = s + " Cidade          VARCHAR(50), ";
                s = s + " CNPJ            VARCHAR(20), ";
                s = s + " Contato         VARCHAR(20), ";
                s = s + " CPF             VARCHAR(15), ";
                s = s + " Email           VARCHAR(200), ";
                s = s + " Logradouro      VARCHAR(60), ";
                s = s + " NomeRazaoSocial VARCHAR(60), ";
                s = s + " Telefone1       VARCHAR(20), ";
                s = s + " Telefone2       VARCHAR(20), ";
                s = s + " DataCadastro    DATETIME, ";
                s = s + " UF              VARCHAR(2)";
                s = s + ")";
                l_mySqlCommad = new MySqlCommand(s, l_mySqlConnect);
                l_mySqlCommad.ExecuteNonQuery();
                DesconectaMySql();
            }
        }
        public void CriarTabelaEmpresas()
        {
            if (!ExistsTable("Empresas"))
            {
                ConectaMySql();
                s = "";
                s = s + "CREATE TABLE Empresas ";
                s = s + "(";
                s = s + " Codigo          INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY, ";
                s = s + " Bairro          VARCHAR(40), ";
                s = s + " Celular1        VARCHAR(40), ";
                s = s + " Celular2        VARCHAR(40), ";
                s = s + " CEP             VARCHAR(9), ";
                s = s + " Cidade          VARCHAR(50), ";
                s = s + " CNPJ            VARCHAR(20), ";
                s = s + " Contato         VARCHAR(20), ";
                s = s + " CPF             VARCHAR(15), ";
                s = s + " Email           VARCHAR(200), ";
                s = s + " Logradouro      VARCHAR(60), ";
                s = s + " NomeRazaoSocial VARCHAR(60), ";
                s = s + " Telefone1       VARCHAR(20), ";
                s = s + " Telefone2       VARCHAR(20), ";
                s = s + " DataCadastro    DATETIME, ";
                s = s + " UF              VARCHAR(2)";
                s = s + ")";
                l_mySqlCommad = new MySqlCommand(s, l_mySqlConnect);
                l_mySqlCommad.ExecuteNonQuery();
                DesconectaMySql();
            }
        }
        public void CriarTabelaUsuarios()
        {
            if (!ExistsTable("Usuarios"))
            {
                ConectaMySql();
                s = "";
                s = s + "CREATE TABLE Usuarios ";
                s = s + "(";
                s = s + " Codigo     INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY, ";
                s = s + " Nome       VARCHAR(60), ";
                s = s + " Senha      VARCHAR(20), ";
                s = s + " Logradouro VARCHAR(60), ";
                s = s + " Bairro     VARCHAR(40), ";
                s = s + " CEP        VARCHAR(9), ";
                s = s + " Cidade     VARCHAR(50), ";
                s = s + " Telefone   VARCHAR(20), ";
                s = s + " Celular    VARCHAR(40), ";
                s = s + " CPF        VARCHAR(15), ";
                s = s + " Contato    VARCHAR(20), ";
                s = s + " Email      VARCHAR(200), ";
                s = s + " DataCadastro    DATETIME, ";
                s = s + " UF         VARCHAR(2)";
                s = s + ")";
                l_mySqlCommad = new MySqlCommand(s, l_mySqlConnect);
                l_mySqlCommad.ExecuteNonQuery();
                DesconectaMySql();
            }
        }
        public void CriarTabelaColaboradores()
        {
            if (!ExistsTable("Colaboradores"))
            {
                ConectaMySql();
                s = "";
                s = s + "CREATE TABLE Colaboradores ";
                s = s + "(";
                s = s + " Codigo       INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY, ";
                s = s + " Nome         VARCHAR(60), ";
                s = s + " Senha        VARCHAR(20), ";
                s = s + " Logradouro   VARCHAR(60), ";
                s = s + " Bairro       VARCHAR(40), ";
                s = s + " CEP          VARCHAR(9), ";
                s = s + " Cidade       VARCHAR(50), ";
                s = s + " Telefone     VARCHAR(20), ";
                s = s + " Celular      VARCHAR(40), ";
                s = s + " CPF          VARCHAR(15), ";
                s = s + " Contato      VARCHAR(20), ";
                s = s + " Email        VARCHAR(200), ";
                s = s + " DataCadastro DATETIME, ";
                s = s + " UF           VARCHAR(2)";
                s = s + ")";
                l_mySqlCommad = new MySqlCommand(s, l_mySqlConnect);
                l_mySqlCommad.ExecuteNonQuery();
                DesconectaMySql();
            }
        }
        public void CriarTabelaFornecedores()
        {
            if (!ExistsTable("Fornecedores"))
            {
                ConectaMySql();
                s = "";
                s = s + "CREATE TABLE Fornecedores ";
                s = s + "(";
                s = s + " Codigo          INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY, ";
                s = s + " Bairro          VARCHAR(40), ";
                s = s + " Celular1        VARCHAR(40), ";
                s = s + " Celular2        VARCHAR(40), ";
                s = s + " CEP             VARCHAR(9), ";
                s = s + " Cidade          VARCHAR(50), ";
                s = s + " CNPJ            VARCHAR(20), ";
                s = s + " Contato         VARCHAR(20), ";
                s = s + " CPF             VARCHAR(15), ";
                s = s + " Email           VARCHAR(200), ";
                s = s + " Logradouro      VARCHAR(60), ";
                s = s + " NomeRazaoSocial VARCHAR(60), ";
                s = s + " Telefone1       VARCHAR(20), ";
                s = s + " Telefone2       VARCHAR(20), ";
                s = s + " DataCadastro    DATETIME, ";
                s = s + " UF              VARCHAR(2)";
                s = s + ")";
                l_mySqlCommad = new MySqlCommand(s, l_mySqlConnect);
                l_mySqlCommad.ExecuteNonQuery();
                DesconectaMySql();
            }
        }
        public void CriarTabelaContas()
        {
            if (!ExistsTable("Contas"))
            {
                ConectaMySql();
                s = "";
                s = s + "CREATE TABLE Contas ";
                s = s + "(";
                s = s + " Codigo          INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY, ";
                s = s + " NomeConta       VARCHAR(40), ";
                s = s + " Db_Cr           VARCHAR(1), ";
                s = s + " DataCadastro    DATETIME, ";
                s = s + " CodigoContabil  VARCHAR(20) ";
                s = s + ")";
                l_mySqlCommad = new MySqlCommand(s, l_mySqlConnect);
                l_mySqlCommad.ExecuteNonQuery();
                DesconectaMySql();
            }
        }
        public void CriarTabelaProdutos()
        {
            if (!ExistsTable("Produtos"))
            {
                ConectaMySql();
                s = "";
                s = s + "CREATE TABLE Produtos ";
                s = s + "(";
                s = s + " Codigo              INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY, ";
                s = s + " Descricao           VARCHAR(50), ";
                s = s + " CodigoBarrasProduto VARCHAR(150), ";
                s = s + " Quantidade          DECIMAL(13, 2), ";
                s = s + " ValorCompra         DECIMAL(13, 2), ";
                s = s + " ValorVenda          DECIMAL(13, 2), ";
                s = s + " CodigoGrupoProduto  INTEGER, ";
                s = s + " Numero              VARCHAR(20), ";
                s = s + " DataCompra          DATE, ";
                s = s + " DataCadastro        DATE, ";
                s = s + " Unidade             VARCHAR(10), ";
                s = s + " DescricaoItens      VARCHAR(100) ";
                s = s + ")";
                l_mySqlCommad = new MySqlCommand(s, l_mySqlConnect);
                l_mySqlCommad.ExecuteNonQuery();
                DesconectaMySql();
            }
        }
        public void CriarTabelaFormaPagamento()
        {
            if (!ExistsTable("FormaPagamento"))
            {
                ConectaMySql();
                s = "";
                s = s + "CREATE TABLE FormaPagamento ";
                s = s + "(";
                s = s + " Numero              INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY, ";
                s = s + " Descricao           VARCHAR(50), ";
                s = s + " EhDinheiro          INTEGER, ";
                s = s + " EhCheque            INTEGER, ";
                s = s + " EhCartao            INTEGER, ";
                s = s + " EhContaPagarReceber INTEGER, ";
                s = s + " DataCadastro        DATETIME, ";
                s = s + " CreditaDebitaCaixa  INTEGER ";
                s = s + ")";
                l_mySqlCommad = new MySqlCommand(s, l_mySqlConnect);
                l_mySqlCommad.ExecuteNonQuery();
                DesconectaMySql();
            }
        }
        public void CriarTabelaGrupoValor()
        {
            if (!ExistsTable("GrupoValor"))
            {
                ConectaMySql();
                s = "";
                s = s + "CREATE TABLE GrupoValor ";
                s = s + "(";
                s = s + " Codigo               INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY, ";
                s = s + " CodigoGrupoProduto   INTEGER, ";
                s = s + " CodigoTamanho        INTEGER, ";
                s = s + " Valor                DECIMAL(13, 2) ";
                s = s + ")";
                l_mySqlCommad = new MySqlCommand(s, l_mySqlConnect);
                l_mySqlCommad.ExecuteNonQuery();
                DesconectaMySql();
            }
        }
        public void CriarTabelaItensProduto()
        {
            if (!ExistsTable("ItensProdutos"))
            {
                ConectaMySql();
                s = "";
                s = s + "CREATE TABLE ItensProdutos ";
                s = s + "(";
                s = s + " Codigo     INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY, ";
                s = s + " Descricao  VARCHAR(60) ";
                s = s + ")";
                l_mySqlCommad = new MySqlCommand(s, l_mySqlConnect);
                l_mySqlCommad.ExecuteNonQuery();
                DesconectaMySql();
            }
        }
        public void CriarTabelaTamanho()
        {
            if (!ExistsTable("Tamanho"))
            {
                ConectaMySql();
                s = "";
                s = s + "CREATE TABLE Tamanho ";
                s = s + "(";
                s = s + " Codigo     INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY, ";
                s = s + " Descricao  VARCHAR(20) ";
                s = s + ")";
                l_mySqlCommad = new MySqlCommand(s, l_mySqlConnect);
                l_mySqlCommad.ExecuteNonQuery();
                DesconectaMySql();
            }
        }
        public void CriarTabelaGrupoProduto()
        {
            if (!ExistsTable("GrupoProduto"))
            {
                ConectaMySql();
                s = "";
                s = s + "CREATE TABLE GrupoProduto ";
                s = s + "(";
                s = s + " Codigo     INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY, ";
                s = s + " Descricao  VARCHAR(60) ";
                s = s + ")";
                l_mySqlCommad = new MySqlCommand(s, l_mySqlConnect);
                l_mySqlCommad.ExecuteNonQuery();
                DesconectaMySql();
            }
        }
        public void CriarTabelaContasPagar()
        {
            if (!ExistsTable("ContasPagar"))
            {
                ConectaMySql();
                s = "";
                s = s + "CREATE TABLE ContasPagar ";
                s = s + "(";
                s = s + " Numero           INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY, ";
                s = s + " CodigoConta      INTEGER, ";
                s = s + " NumeroDocumento  VARCHAR(10), ";
                s = s + " Historico        VARCHAR(60), ";
                s = s + " DataVencimento   DATETIME, ";
                s = s + " Valor            DECIMAL(14, 2), ";
                s = s + " DataPagamento    DATETIME, ";
                s = s + " ValorPago        DECIMAL(14, 2), ";
                s = s + " CodigoFornecedor INTEGER, ";
                s = s + " DataCadastro     DATETIME ";
                s = s + ")";
                l_mySqlCommad = new MySqlCommand(s, l_mySqlConnect);
                l_mySqlCommad.ExecuteNonQuery();
                DesconectaMySql();
            }
            if (!ExisteCampoTabela("ContasPagar", "ParcelaAtual"))
            {
                ConectaMySql();
                s = "";
                s = "alter table ContasPagar add column ParcelaAtual integer; ";
                l_mySqlCommad = new MySqlCommand(s, l_mySqlConnect);
                l_mySqlCommad.ExecuteNonQuery();
                DesconectaMySql();
            }
            if (!ExisteCampoTabela("ContasPagar", "QtdeParcelas"))
            {
                ConectaMySql();
                s = "";
                s = "alter table ContasPagar add column QtdeParcelas integer; ";
                l_mySqlCommad = new MySqlCommand(s, l_mySqlConnect);
                l_mySqlCommad.ExecuteNonQuery();
                DesconectaMySql();
            }
        }
        public void CriarTabelaContasReceber()
        {
            if (!ExistsTable("ContasReceber"))
            {
                ConectaMySql();
                s = "";
                s = s + "CREATE TABLE ContasReceber ";
                s = s + "(";
                s = s + " Numero           INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY, ";
                s = s + " CodigoConta      INTEGER, ";
                s = s + " NumeroDocumento  VARCHAR(10), ";
                s = s + " ParcelaAtual     INTEGER, ";
                s = s + " QtdeParcelas     INTEGER, ";
                s = s + " Historico        VARCHAR(60), ";
                s = s + " DataVencimento   DATETIME, ";
                s = s + " Valor            DECIMAL(14, 2), ";
                s = s + " DataPagamento    DATETIME, ";
                s = s + " ValorPago        DECIMAL(14, 2), ";
                s = s + " CodigoCliente    INTEGER, ";
                s = s + " DataCadastro     DATETIME ";
                s = s + ")";
                l_mySqlCommad = new MySqlCommand(s, l_mySqlConnect);
                l_mySqlCommad.ExecuteNonQuery();
                DesconectaMySql();
            }
        }
        public void CriarTabelaContasDiversos()
        {
            if (!ExistsTable("ContasDiversos"))
            {
                ConectaMySql();
                s = "";
                s = s + "CREATE TABLE ContasDiversos ";
                s = s + "(";
                s = s + " Numero       INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY, ";
                s = s + " CodigoConta  INTEGER, ";
                s = s + " Historico    VARCHAR(60), ";
                s = s + " Data         DATETIME, ";
                s = s + " Valor        DECIMAL(14, 2), ";
                s = s + " DataCadastro DATETIME ";
                s = s + ")";
                l_mySqlCommad = new MySqlCommand(s, l_mySqlConnect);
                l_mySqlCommad.ExecuteNonQuery();
                DesconectaMySql();
            }
        }
    }
}