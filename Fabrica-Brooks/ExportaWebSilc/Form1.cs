using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using LibSILC;
using SILCNegocios;
using System.Data.OleDb;
using System.Threading;

namespace ExportaWebSILC
{
    public partial class ExportaWebSILC : Form
    {
        private string Caminho;
        private OleDbConnection aConnection;
        private clsExportacaoRadarDados oExportacaoRadarDados = new clsExportacaoRadarDados();
        clsClientes oCliente = new clsClientes();
        clsClienteDados oClienteDados = new clsClienteDados();
        clsNotasFiscais oNotasFiscais = new clsNotasFiscais();
        clsNotasFiscaisDados oNotasFiscaisDados = new clsNotasFiscaisDados();

        private decimal ValorIRRF = 0;
        private decimal ValorPIS = 0;
        private decimal ValorCOFINS = 0;
        private decimal ValorContrSocial = 0;
        private decimal ValorCRF = 0;
        private decimal ValorINSS = 0;
        private decimal ValorTotal = 0;
        private decimal ValorISS = 0;
        private decimal ValorLiquido = 0;

        clsAliquotaImpostos oRetencao = new clsAliquotaImpostos();

        public ExportaWebSILC()
        {
            geral.BancoUsado = 2; // só pra teste
            geral.BancoUsado = 1;
            if (geral.BancoUsado == 2)
                MessageBox.Show("Cuidado banco test");
            InitializeComponent();
            clsDB oDBMySQL = new clsDB();
            oDBMySQL.ConectaMySql();
            lblServidor.Text = " Servidor atual: " + oDBMySQL.NomeServidor + "       DB: " + oDBMySQL.NomeDB;
            oDBMySQL.DesconectaMySql();
        }

        private void SalvaAterroSanitario(int pNumeroLancamento, int pCodigo = 0)
        {
            DataTable _dtControleAterro = new DataTable();
            string s = "";
            s = s + "select * from AterroSanitario \n";
            if (pCodigo == 0)
                s = s + "where NumeroLancamento = " + pNumeroLancamento + "\n ";
            else
                s = s + "where Codigo = " + pCodigo.ToString() + "\n ";

            OleDbDataAdapter oDA_ControleAterro = new OleDbDataAdapter(s, aConnection);
            oDA_ControleAterro.Fill(_dtControleAterro);
            oDA_ControleAterro.Dispose();
            clsAterroSanitarioDados oAterroSanitarioDados = new clsAterroSanitarioDados();
            clsAterroSanitario oAterroSanitario = new clsAterroSanitario();

            // excluir anterior quando não for descarga
            if (pCodigo == 0 && pNumeroLancamento > 0)
                oAterroSanitarioDados.ExcluirNumeroLancamento(pNumeroLancamento);

            foreach (DataRow _drControleAterro in _dtControleAterro.Rows)
            {
                oAterroSanitario = new clsAterroSanitario();
                if (_drControleAterro["Codigo"].ToString() != "")
                    oAterroSanitario.Codigo = Convert.ToInt32(_drControleAterro["Codigo"]);
                if (_drControleAterro["NumeroLancamento"].ToString() != "")
                    oAterroSanitario.NumeroLancamento = Convert.ToInt32(_drControleAterro["NumeroLancamento"]);
                if (_drControleAterro["CodigoAterro"].ToString() != "")
                    oAterroSanitario.CodigoAterro = Convert.ToInt32(_drControleAterro["CodigoAterro"]);

                if (_drControleAterro["CodigoCaminhao"].ToString() != "")
                    oAterroSanitario.CodigoCaminhao = Convert.ToInt32(_drControleAterro["CodigoCaminhao"]);

                if (_drControleAterro["CodigoCliente"].ToString() != "")
                    oAterroSanitario.CodigoCliente = Convert.ToInt32(_drControleAterro["CodigoCliente"]);

                if (_drControleAterro["CodigoMotorista"].ToString() != "")
                    oAterroSanitario.CodigoMotorista = Convert.ToInt32(_drControleAterro["CodigoMotorista"]);

                if (_drControleAterro["CodigoResiduo"].ToString() != "")
                    oAterroSanitario.CodigoResiduo = Convert.ToInt32(_drControleAterro["CodigoResiduo"]);
                oAterroSanitario.ContainerDescricao = _drControleAterro["ContainerDescricao"].ToString();
                oAterroSanitario.Data = _drControleAterro["Data"].ToString();
                if (_drControleAterro["Excluido"].ToString() != "")
                    oAterroSanitario.Excluido = Convert.ToInt16(_drControleAterro["Excluido"]);
                oAterroSanitario.Hora = _drControleAterro["Hora"].ToString();
                oAterroSanitario.LocalAterro = _drControleAterro["LocalAterro"].ToString();
                oAterroSanitario.NumeroCaixa = _drControleAterro["NumeroCaixa"].ToString();
                if (_drControleAterro["NumeroMTR"].ToString() != "")
                    oAterroSanitario.NumeroMTR = Convert.ToInt32(_drControleAterro["NumeroMTR"]);
                oAterroSanitario.NumeroTicket = _drControleAterro["NumeroTicket"].ToString();
                if (_drControleAterro["Status"].ToString() != "")
                    oAterroSanitario.Status = Convert.ToInt16(_drControleAterro["Status"]);
                if (_drControleAterro["TotalPeso"].ToString() != "")
                    oAterroSanitario.TotalPeso = Convert.ToDecimal(_drControleAterro["TotalPeso"]);

                if (pCodigo == 0)
                    oAterroSanitarioDados.Inserir(oAterroSanitario, oAterroSanitario.Codigo);
                else
                {
                    if (oAterroSanitarioDados.DadoExiste(pCodigo) != "Alterar")
                        oAterroSanitarioDados.Inserir(oAterroSanitario, oAterroSanitario.Codigo);
                    else
                        oAterroSanitarioDados.Alterar(oAterroSanitario, oAterroSanitario.Codigo);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Caminho = string.Concat(Application.StartupPath, "\\ExportaSite\\");
            if (ExisteArquivo("Lancamento_EM_TESTE"))
            {
                try
                {
                    timer1.Enabled = false;
                    Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                    DataTable _dtAtualizacao = new DataTable();
                    string LancLinha;
                    clsLancamentoMTRDados oLancamentoMTRDados = new clsLancamentoMTRDados();
                    clsLancamentoMTR oLancamentoMTR = new clsLancamentoMTR();
                    clsLancamentos oLancamentos = new clsLancamentos();
                    clsLancamentosDados oLancamentosDados = new clsLancamentosDados();

                    //cria a conexão com o banco de dados
                    aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\WinSilc\\SILC.MDB");
                    aConnection.Open();

                    string s = "";
                    for (int i = 0; i < 1000; i++)
                    {
                        try
                        {
                            //string CaminhoLacamentos = @"ImportacaoWebSILC\";
                            Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                            if (i == 0)
                                Caminho = Caminho + "Lancamento.txt";
                            else
                                Caminho = Caminho + "Lancamento" + i.ToString() + ".txt";

                            if (new FileInfo(Caminho).Exists)
                            {
                                using (StreamReader reader = new StreamReader(Caminho))
                                {
                                    LancLinha = reader.ReadLine();
                                    if (LancLinha != "")
                                    {
                                        s = "";
                                        s = s + "select * from Lancamentos \n";
                                        s = s + "where  nuLanc = " + Convert.ToInt32(LancLinha) + " \n";
                                        _dtAtualizacao = new DataTable();
                                        OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                                        oDataAdapter.Fill(_dtAtualizacao);
                                        oDataAdapter.Dispose();
                                        foreach (DataRow dr in _dtAtualizacao.Rows)
                                        {
                                            if (dr["nuLanc"].ToString() != "" && dr["nuLanc"].ToString() != "0")
                                            {
                                                oLancamentos = new clsLancamentos();
                                                oLancamentos.NumeroLancamento = Convert.ToInt32(LancLinha);
                                                if (dr["cdCamhColc"].ToString() != "")
                                                    oLancamentos.CodigoCaminhaoColoca = Convert.ToInt32(dr["cdCamhColc"]);
                                                if (dr["cdCamhRetr"].ToString() != "")
                                                    oLancamentos.CodigoCaminhoRetirada = Convert.ToInt32(dr["cdCamhRetr"]);
                                                if (dr["cdClnt"].ToString() != "")
                                                    oLancamentos.CodigoCliente = Convert.ToInt32(dr["cdClnt"]);
                                                if (dr["cdEndrObra"].ToString() != "")
                                                    oLancamentos.CodigoEnderecoObra = Convert.ToInt32(dr["cdEndrObra"]);
                                                if (dr["cdMotr1"].ToString() != "")
                                                    oLancamentos.CodigoMotoristaColocou = Convert.ToInt32(dr["cdMotr1"]);
                                                if (dr["cdMotr2"].ToString() != "")
                                                    oLancamentos.CodigoMotoristaRetirou = Convert.ToInt32(dr["cdMotr2"]);
                                                if (dr["dtLanc"].ToString() != "")
                                                    oLancamentos.Data = Convert.ToDateTime(dr["dtLanc"]).ToString("yyyy-MM-dd");
                                                if (dr["dtARetr"].ToString() != "")
                                                    oLancamentos.DataARetirar = Convert.ToDateTime(dr["dtARetr"]).ToString("yyyy-MM-dd");
                                                if (dr["dtColc"].ToString() != "")
                                                    oLancamentos.DataColocacao = Convert.ToDateTime(dr["dtColc"]).ToString("yyyy-MM-dd");
                                                if (dr["dtRetr"].ToString() != "")
                                                    oLancamentos.DataRetirada = Convert.ToDateTime(dr["dtRetr"]).ToString("yyyy-MM-dd");
                                                oLancamentos.HoraRetirada = dr["hsRetr"].ToString();
                                                if (dr["Horas"].ToString() != "")
                                                    oLancamentos.Horas = Convert.ToInt32(dr["Horas"]);
                                                oLancamentos.HorasARetirar = dr["hsARetr"].ToString();
                                                oLancamentos.HorasColocacao = dr["hsColc"].ToString();
                                                if (dr["NuLancColocacao"].ToString() != "")
                                                    oLancamentos.NuLancColocacao = Convert.ToInt32(dr["NuLancColocacao"]);
                                                oLancamentos.NumeroCaixa = dr["nuCaxa"].ToString();
                                                oLancamentos.OBS = dr["OBS"].ToString();
                                                if (dr["TipoOperacao"].ToString() != "")
                                                    oLancamentos.TipoOperacao = Convert.ToInt32(dr["TipoOperacao"]);
                                                if (dr["vlLocc"].ToString() != "")
                                                    oLancamentos.ValorLocacao = Convert.ToDecimal(dr["vlLocc"]);
                                                if (oLancamentosDados.DadoExiste(oLancamentos.NumeroLancamento) == "Alterar")
                                                    oLancamentosDados.Alterar(oLancamentos, oLancamentos.NumeroLancamento);
                                                else
                                                    oLancamentosDados.Inserir(oLancamentos, oLancamentos.NumeroLancamento);
                                                oLancamentoMTRDados = new clsLancamentoMTRDados();
                                                oLancamentoMTRDados.Excluir(Convert.ToInt32(LancLinha), "");
                                                oLancamentoMTR.NumeroLancamento = Convert.ToInt32(LancLinha);

                                                DataTable _dtLancamentoMTR = new DataTable();
                                                s = "";
                                                s = "select * from LancamentoMTR where NumeroLancamento = " + oLancamentoMTR.NumeroLancamento;
                                                OleDbDataAdapter oDAMTR = new OleDbDataAdapter(s, aConnection);
                                                oDAMTR.Fill(_dtLancamentoMTR);
                                                oDAMTR.Dispose();
                                                foreach (DataRow _drMTR in _dtLancamentoMTR.Rows)
                                                {
                                                    oLancamentoMTR = new clsLancamentoMTR();
                                                    oLancamentoMTR.NumeroLancamento = Convert.ToInt32(LancLinha);
                                                    if (_drMTR["CodigoAterroSanitario"].ToString() != "")
                                                        oLancamentoMTR.CodigoAterroSanitario = Convert.ToInt32(_drMTR["CodigoAterroSanitario"]);
                                                    if (_drMTR["CodigoResiduo"].ToString() != "")
                                                        oLancamentoMTR.CodigoResiduo = Convert.ToInt32(_drMTR["CodigoResiduo"]);
                                                    DataTable _dtAT = new DataTable();
                                                    s = "";
                                                    if (Convert.ToInt32(_drMTR["CodigoResiduo"]) > 0 && oLancamentos.NumeroLancamento > 0)
                                                    {
                                                        s = s + "select Data from AterroSanitario \n";
                                                        s = s + "where  NumeroLancamento = " + oLancamentos.NumeroLancamento + " \n";
                                                        s = s + "and    CodigoResiduo    = " + _drMTR["CodigoResiduo"].ToString();
                                                        OleDbDataAdapter oDAT = new OleDbDataAdapter(s, aConnection);
                                                        oDAT.Fill(_dtAT);
                                                        if (_dtAT.Rows.Count > 0)
                                                            oLancamentoMTR.DataDescarga = Convert.ToDateTime(_dtAT.Rows[0]["Data"]).ToShortDateString();
                                                        else if (_drMTR["DataDescarga"].ToString() != "" && geral.Left(_drMTR["DataDescarga"].ToString(), 10) != "01/01/0100")
                                                            oLancamentoMTR.DataDescarga = Convert.ToDateTime(_drMTR["DataDescarga"]).ToShortDateString();
                                                        oDAT.Dispose();
                                                    }
                                                    oLancamentoMTR.Deposito = _drMTR["Deposito"].ToString();
                                                    if (_drMTR["Franquia"].ToString() != "")
                                                        oLancamentoMTR.Franquia = Convert.ToDecimal(_drMTR["Franquia"]);
                                                    oLancamentoMTR.Motivo = _drMTR["Motivo"].ToString();
                                                    if (_drMTR["NumeroMTR"].ToString() != "")
                                                        oLancamentoMTR.NumeroMTR = Convert.ToInt32(_drMTR["NumeroMTR"]);
                                                    if (_drMTR["NumeroMTRFatima"].ToString() != "")
                                                        oLancamentoMTR.NumeroMTRFatima = Convert.ToInt64(_drMTR["NumeroMTRFatima"]);
                                                    oLancamentoMTR.observacao = _drMTR["observacao"].ToString();
                                                    if (_drMTR["Quantidade"].ToString() != "")
                                                        oLancamentoMTR.Quantidade = Convert.ToDecimal(_drMTR["Quantidade"]);
                                                    oLancamentoMTR.Ticket = _drMTR["Ticket"].ToString();
                                                    oLancamentoMTR.Unidade = _drMTR["Unidade"].ToString();
                                                    if (_drMTR["ValorTotal"].ToString() != "")
                                                        oLancamentoMTR.ValorTotal = Convert.ToDecimal(_drMTR["ValorTotal"]);
                                                    if (_drMTR["ValorUnitario"].ToString() != "")
                                                        oLancamentoMTR.ValorUnitario = Convert.ToDecimal(_drMTR["ValorUnitario"]);
                                                    if (oLancamentoMTRDados.DadoExiste(oLancamentoMTR.NumeroLancamento, oLancamentoMTR.NumeroMTR, oLancamentoMTR.CodigoResiduo) == "Alterar")
                                                        oLancamentoMTRDados.Alterar(oLancamentoMTR, oLancamentoMTR.NumeroLancamento, oLancamentoMTR.NumeroMTR, oLancamentoMTR.CodigoResiduo);
                                                    else
                                                        oLancamentoMTRDados.Inserir(oLancamentoMTR);
                                                }
                                                // exclui e insere novamente quando codigo = 0 ou não informado
                                                SalvaAterroSanitario(oLancamentos.NumeroLancamento);

                                            }
                                        }
                                    }
                                    reader.Close();
                                    reader.Dispose();
                                }

                                // apagar arquivo lancamento[].txt
                                System.IO.File.Delete(Caminho);
                            }
                        }
                        catch (Exception ex)
                        {
                            StreamWriter sw = new StreamWriter(Caminho.Replace(".txt", "err") + oLancamentos.NumeroLancamento + ".txt");
                            sw.WriteLine("Erro: Lancamento: " + oLancamentos.NumeroLancamento.ToString());
                            sw.WriteLine(ex.Message);
                            sw.Close();
                            sw.Dispose();
                        }
                    }
                }
                catch (Exception er)
                {
                    Caminho = er.Message;
                    StreamWriter sw = new StreamWriter(Caminho.Replace(".txt", "err") + DateTime.Now.Second + ".txt");
                    sw.WriteLine("Erro: Lancamento: " + DateTime.Now.Second.ToString());
                    sw.WriteLine(er.Message);
                    sw.Close();
                    sw.Dispose();
                }
                finally
                {
                    aConnection.Close();
                    aConnection.Dispose();
                    timer1.Enabled = true;
                }
            }
            else if (ExisteArquivo("Clientes"))
            {
                try
                {
                    timer1.Enabled = false;
                    Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                    DataTable _dtAtualizacao = new DataTable();
                    string LancLinha;

                    clsClientes oCliente = new clsClientes();
                    clsClienteDados oClienteDados = new clsClienteDados();
                    clsEnderecos oEndereco = new clsEnderecos();
                    clsEnderecosDados oEnderecoDados = new clsEnderecosDados();

                    //cria a conexão com o banco de dados
                    aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\WinSilc\\SILC.MDB");
                    aConnection.Open();

                    string s = "";
                    for (int i = 0; i < 1000; i++)
                    {
                        try
                        {
                            Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                            if (i == 0)
                                Caminho = Caminho + "Clientes.txt";
                            else
                                Caminho = Caminho + "Clientes" + i.ToString() + ".txt";

                            if (new FileInfo(Caminho).Exists)
                            {
                                using (StreamReader reader = new StreamReader(Caminho))
                                {
                                    LancLinha = reader.ReadLine();
                                    if (LancLinha != "")
                                    {
                                        s = "";
                                        s = s + "select * from Clientes \n";
                                        s = s + "where  Codigo = " + Convert.ToInt32(LancLinha) + " \n"; // Código do cliente
                                        _dtAtualizacao = new DataTable();
                                        OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                                        oDataAdapter.Fill(_dtAtualizacao);
                                        oDataAdapter.Dispose();
                                        foreach (DataRow dr in _dtAtualizacao.Rows)
                                        {
                                            if (dr["Codigo"].ToString() != "" && dr["Codigo"].ToString() != "0")
                                            {
                                                try
                                                {
                                                    oCliente = new clsClientes();
                                                    oCliente.Codigo = Convert.ToInt32(dr["Codigo"]);
                                                    if (oCliente.Codigo == 2161)
                                                    {
                                                        oCliente.Codigo = 2161;
                                                    }
                                                    if (dr["DataCadastro"].ToString() != "")
                                                        oCliente.DataCadastro = Convert.ToDateTime(dr["DataCadastro"]).ToShortDateString();
                                                    oCliente.CEP = dr["CEP"].ToString();
                                                    if (dr["Classificacao"].ToString() != "")
                                                        oCliente.Classificacao = Convert.ToInt32(dr["Classificacao"]);
                                                    oCliente.CNPJ_CPF = dr["CGC_CPF"].ToString();
                                                    oCliente.CNPJ_Faturamento = dr["CNPJ_Faturamento"].ToString();
                                                    oCliente.CodigoBROOKS_Retencoes = dr["CodigoBROOKS_Retencoes"].ToString();
                                                    if (dr["CodigoClienteExportacao"].ToString() != "")
                                                        oCliente.CodigoClienteExportacao = Convert.ToInt32(dr["CodigoClienteExportacao"]);
                                                    if (dr["CodigoMunicipioNF"].ToString() != "")
                                                        oCliente.CodigoMunicipioNF = Convert.ToInt32(dr["CodigoMunicipioNF"]);
                                                    if (dr["CodigoMunicipioObra"].ToString() != "")
                                                        oCliente.CodigoMunicipioObra = Convert.ToInt32(dr["CodigoMunicipioObra"]);
                                                    if (dr["CodigoNoAterro"].ToString() != "")
                                                        oCliente.CodigoNoAterro = Convert.ToInt32(dr["CodigoNoAterro"]);
                                                    if (dr["CodigoSituacaoTributaria"].ToString() != "")
                                                        oCliente.CodigoSituacaoTributaria = Convert.ToInt32(dr["CodigoSituacaoTributaria"]);
                                                    if (dr["ContaGerencial"].ToString() != "")
                                                        oCliente.ContaGerencial = Convert.ToInt32(dr["ContaGerencial"]);
                                                    oCliente.ContatoFatima = dr["ContatoFatima"].ToString();
                                                    if (dr["dtNasc"].ToString() != "")
                                                        oCliente.DataNascimento = Convert.ToDateTime(dr["dtNasc"]).ToShortDateString();
                                                    if (dr["DataSenha"].ToString() != "")
                                                        oCliente.DataSenha = Convert.ToDateTime(dr["DataSenha"]).ToShortDateString();
                                                    if (dr["dtRG"].ToString() != "")
                                                        oCliente.dtRG = Convert.ToDateTime(dr["dtRG"]).ToShortDateString();
                                                    oCliente.email = dr["email"].ToString();
                                                    oCliente.emailFatma = dr["emailFatma"].ToString();
                                                    if (dr["EnviarCDF"].ToString() != "")
                                                        oCliente.EnviarCDF = Convert.ToInt32(dr["EnviarCDF"]);
                                                    if (dr["EnviarDDR"].ToString() != "")
                                                        oCliente.EnviarDDR = Convert.ToInt32(dr["EnviarDDR"]);
                                                    if (dr["Filial"].ToString() != "")
                                                        oCliente.Filial = Convert.ToInt16(dr["Filial"]);
                                                    if (dr["Inativo"].ToString() != "")
                                                        oCliente.Inativo = Convert.ToInt16(dr["Inativo"]);
                                                    if (dr["KmMedia"].ToString() != "")
                                                        oCliente.KmMedia = Convert.ToInt32(dr["KmMedia"]);
                                                    oCliente.Nacionalidade = dr["Nacionalidade"].ToString();
                                                    if (dr["NaoAceitaDiferencaPeso"].ToString() != "")
                                                        oCliente.NaoAceitaDiferencaPeso = Convert.ToInt16(dr["NaoAceitaDiferencaPeso"]);
                                                    oCliente.Naturalidade = dr["Naturalidade"].ToString();
                                                    oCliente.Nome = dr["Nome2"].ToString();
                                                    oCliente.Nome2 = dr["Nome2"].ToString();
                                                    oCliente.NomeDaEmpresa = dr["NomeDaEmpresa"].ToString();
                                                    oCliente.NomeFantasia = dr["nmFant"].ToString();
                                                    oCliente.NovaSenha = dr["NovaSenha"].ToString();
                                                    oCliente.OBS = dr["OBS"].ToString();
                                                    oCliente.ObsFatima = dr["ObsFatima"].ToString();
                                                    if (dr["Percentual"].ToString() != "")
                                                        oCliente.Percentual = Convert.ToDecimal(dr["Percentual"]);
                                                    if (dr["Pessoa"].ToString() != "")
                                                        oCliente.Pessoa = Convert.ToInt16(dr["Pessoa"]);
                                                    oCliente.PontoReferencia = dr["PontoReferencia"].ToString();
                                                    oCliente.RG_IE = dr["RG_IE"].ToString();
                                                    oCliente.RGEmit = dr["RGEmit"].ToString();
                                                    oCliente.SenhaAcessoFatima = dr["SenhaAcessoFatima"].ToString();
                                                    oCliente.SenhaMasterFatima = dr["SenhaMasterFatima"].ToString();
                                                    oCliente.site = dr["site"].ToString();
                                                    oCliente.SolicitadoSenhaPor = dr["SolicitadoSenhaPor"].ToString();
                                                    oCliente.TelefoneFatima = dr["TelefoneFatima"].ToString();
                                                    if (dr["TiposDeContrato"].ToString() != "")
                                                        oCliente.TiposDeContrato = Convert.ToInt16(dr["TiposDeContrato"]);
                                                    if (dr["CodigoTipoCobranca"].ToString() != "")
                                                        oCliente.CodigoTipoCobranca = Convert.ToInt16(dr["CodigoTipoCobranca"]);

                                                    if (oClienteDados.DadoExiste(oCliente.Codigo) == "Alterar")
                                                        oClienteDados.Alterar(oCliente, oCliente.Codigo);
                                                    else
                                                    {
                                                        oClienteDados.Inserir(oCliente, oCliente.Codigo);
                                                    }
                                                    wp_FilesClientesDados wpFiles = new wp_FilesClientesDados();
                                                    clsDocumentosPagina oDocumentos = new clsDocumentosPagina();
                                                    clsDocumentosPaginaDados oDocumentosDados = new clsDocumentosPaginaDados();
                                                    foreach (DataRow drDoc in oDocumentosDados.PegaDados(oDocumentos, 0, false).Rows)
                                                    {
                                                        if (drDoc["BROOKS"].ToString() == "S")
                                                        {
                                                            if (drDoc["Tipo"].ToString() != "" && drDoc["Descricao"].ToString() != "")
                                                            {
                                                                if (!wpFiles.DadoExiste(oCliente.Codigo, drDoc["Descricao"].ToString()))
                                                                {
                                                                    string sPer = DateTime.Now.Year.ToString();
                                                                    if (drDoc["Periodo"].ToString() != "")
                                                                        sPer = drDoc["Periodo"].ToString();
                                                                    wpFiles.Incluir(drDoc["descricao"].ToString(), drDoc["Tipo"].ToString(), oCliente.Codigo,
                                                                                    drDoc["Tipo"].ToString(), sPer);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                finally
                                                {
                                                    DataTable _dtEndereco = new DataTable();
                                                    s = "";
                                                    s = s + "select * from Enderecos \n";
                                                    s = s + "where Codigo = " + oCliente.Codigo;
                                                    s = s + "and   TipoCadastro = 0 \n";
                                                    OleDbDataAdapter oDAEndereco = new OleDbDataAdapter(s, aConnection);
                                                    oDAEndereco.Fill(_dtEndereco);
                                                    oDAEndereco.Dispose();
                                                    foreach (DataRow _drEndereco in _dtEndereco.Rows)
                                                    {
                                                        oEndereco = new clsEnderecos();
                                                        oEndereco.Codigo = Convert.ToInt32(_drEndereco["Codigo"]);
                                                        oEndereco.Bairro = _drEndereco["Bairro"].ToString();
                                                        oEndereco.CEP = _drEndereco["CEP"].ToString();
                                                        if (_drEndereco["CodigoMunicipio"].ToString() != "")
                                                            oEndereco.CodigoMunicipio = Convert.ToInt32(_drEndereco["CodigoMunicipio"]);
                                                        oEndereco.Complemento = _drEndereco["Complemento"].ToString();
                                                        oEndereco.Contato = _drEndereco["Contato"].ToString();
                                                        if (_drEndereco["DDD1"].ToString() != "")
                                                            oEndereco.DDD1 = Convert.ToInt32(_drEndereco["DDD1"]);
                                                        if (_drEndereco["DDD2"].ToString() != "")
                                                            oEndereco.DDD2 = Convert.ToInt32(_drEndereco["DDD2"]);
                                                        if (_drEndereco["DDD3"].ToString() != "")
                                                            oEndereco.DDD3 = Convert.ToInt32(_drEndereco["DDD3"]);
                                                        if (_drEndereco["DDDF"].ToString() != "")
                                                            oEndereco.DDDF = Convert.ToInt32(_drEndereco["DDDF"]);
                                                        oEndereco.email = _drEndereco["email"].ToString();
                                                        oEndereco.endereco = _drEndereco["endereco"].ToString();
                                                        oEndereco.Fax = _drEndereco["Fax"].ToString();
                                                        oEndereco.Fone1 = _drEndereco["Fone1"].ToString();
                                                        oEndereco.Fone2 = _drEndereco["Fone2"].ToString();
                                                        oEndereco.Fone3 = _drEndereco["Fone3"].ToString();
                                                        oEndereco.InstrucoesFat = _drEndereco["InstrucoesFat"].ToString();
                                                        oEndereco.Numero = _drEndereco["Numero"].ToString();
                                                        oEndereco.TipoCadastro = 0;
                                                        if (_drEndereco["TipoEndereco"].ToString() != "")
                                                            oEndereco.TipoEndereco = Convert.ToInt16(_drEndereco["TipoEndereco"]);

                                                        if (oEnderecoDados.DadoExiste(oEndereco.Codigo, 0, oEndereco.TipoEndereco) == "Alterar")
                                                            oEnderecoDados.Alterar(oEndereco, oEndereco.Codigo, 0, oEndereco.TipoEndereco);
                                                        else
                                                            oEnderecoDados.Inserir(oEndereco, oEndereco.Codigo, 0, oEndereco.TipoEndereco);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                // apagar arquivo lancamento[].txt
                                System.IO.File.Delete(Caminho);
                            }
                        }
                        catch (Exception ex)
                        {
                            StreamWriter sw = new StreamWriter(Caminho.Replace(".txt", "Err") + oCliente.Codigo + ".txt");
                            sw.WriteLine("Erro: Cliente: " + oCliente.Codigo.ToString());
                            sw.WriteLine(ex.Message);
                            sw.Close();
                            sw.Dispose();
                        }
                    }
                }
                catch (Exception er)
                {
                    Caminho = er.Message;
                }
                finally
                {
                    timer1.Enabled = true;
                    aConnection.Close();
                    aConnection.Dispose();
                }
            }
            else if (ExisteArquivo("AterroSanitario_EM_TESTE"))
            {
                try
                {
                    timer1.Enabled = false;
                    Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                    DataTable _dtAtualizacao = new DataTable();
                    string LancLinha;

                    clsAterroSanitario oAterroSanitario = new clsAterroSanitario();
                    clsAterroSanitarioDados oAterroSanitarioDados = new clsAterroSanitarioDados();

                    //cria a conexão com o banco de dados
                    aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\WinSilc\\SILC.MDB");
                    aConnection.Open();

                    string s = "";
                    for (int i = 0; i < 1000; i++)
                    {
                        try
                        {
                            Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                            if (i == 0)
                                Caminho = Caminho + "AterroSanitario.txt";
                            else
                                Caminho = Caminho + "AterroSanitario" + i.ToString() + ".txt";

                            if (new FileInfo(Caminho).Exists)
                            {
                                using (StreamReader reader = new StreamReader(Caminho))
                                {
                                    LancLinha = reader.ReadLine();
                                    if (LancLinha != "")
                                    {
                                        SalvaAterroSanitario(0, Convert.ToInt32(LancLinha));
                                    }
                                }
                                // apagar arquivo AterroSanitario.txt
                                System.IO.File.Delete(Caminho);
                            }
                        }
                        catch (Exception ex)
                        {
                            StreamWriter sw = new StreamWriter("Erro_" + Caminho.Replace(".txt", "") + oAterroSanitario.Codigo + ".txt");
                            sw.WriteLine("Erro: AterroSanitario: " + oAterroSanitario.CodigoCliente.ToString());
                            sw.WriteLine(ex.Message);
                            sw.Close();
                            sw.Dispose();
                        }
                    }
                }
                catch (Exception er)
                {
                    Caminho = er.Message;
                }
                finally
                {
                    timer1.Enabled = true;
                    aConnection.Close();
                    aConnection.Dispose();
                }
            }
            else if (ExisteArquivo("BloqueioFinanceiro"))
            {
                try
                {
                    timer1.Enabled = false;
                    Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                    DataTable _dtAtualizacao = new DataTable();
                    string LancLinha;

                    clsBloqueioFinanceiro oBloqueioFinanceiro = new clsBloqueioFinanceiro();
                    clsBloqFinanceiroDados oBloqueioFinanceiroDados = new clsBloqFinanceiroDados();

                    //cria a conexão com o banco de dados
                    aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\WinSilc\\SILC.MDB");
                    aConnection.Open();

                    string s = "";
                    for (int i = 0; i < 1000; i++)
                    {
                        try
                        {
                            Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                            if (i == 0)
                                Caminho = Caminho + "BloqueioFinanceiro.txt";
                            else
                                Caminho = Caminho + "BloqueioFinanceiro" + i.ToString() + ".txt";

                            if (new FileInfo(Caminho).Exists)
                            {
                                using (StreamReader reader = new StreamReader(Caminho))
                                {
                                    LancLinha = reader.ReadLine();
                                    if (LancLinha != "")
                                    {
                                        s = "";
                                        s = s + "select * from BloqueioFinanceiro \n";
                                        s = s + "where  CodigoCliente = " + Convert.ToInt32(LancLinha) + " \n"; // BloqueioFinanceiro
                                        _dtAtualizacao = new DataTable();
                                        OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                                        oDataAdapter.Fill(_dtAtualizacao);
                                        oDataAdapter.Dispose();
                                        //Exclui todos os bloqueios e inclui novamente
                                        oBloqueioFinanceiroDados.Excluir(Convert.ToInt32(LancLinha)); //CodigoCliente
                                        foreach (DataRow dr in _dtAtualizacao.Rows)
                                        {
                                            if (dr["CodigoCliente"].ToString() != "" && dr["CodigoCliente"].ToString() != "0")
                                            {
                                                oBloqueioFinanceiro = new clsBloqueioFinanceiro();
                                                oBloqueioFinanceiro.CodigoCliente = Convert.ToInt32(dr["CodigoCliente"]);
                                                if (dr["CodigoUsuario"].ToString() != "")
                                                    oBloqueioFinanceiro.CodigoUsuario = Convert.ToInt32(dr["CodigoUsuario"]);
                                                if (dr["DataBloqueio"].ToString() != "" && dr["DataBloqueio"].ToString() != "0001-01-01" && dr["DataBloqueio"].ToString() != "0100-01-01")
                                                    oBloqueioFinanceiro.DataBloqueio = Convert.ToDateTime(dr["DataBloqueio"]).ToShortDateString();
                                                if (dr["DataDesbloqueio"].ToString() != "" && dr["DataDesbloqueio"].ToString() != "0001-01-01" && dr["DataDesbloqueio"].ToString() != "0100-01-01")
                                                    oBloqueioFinanceiro.DataDesbloqueio = Convert.ToDateTime(dr["DataDesbloqueio"]).ToShortDateString();
                                                oBloqueioFinanceiro.Observacao = dr["Observacao"].ToString();
                                                oBloqueioFinanceiroDados.Inserir(oBloqueioFinanceiro);
                                            }
                                        }
                                    }
                                }
                                // apagar arquivo BloqueiroFinanceiro.txt
                                System.IO.File.Delete(Caminho);
                            }
                        }
                        catch (Exception ex)
                        {
                            StreamWriter sw = new StreamWriter("Erro_" + Caminho.Replace(".txt", "") + oBloqueioFinanceiro.Sequencial + ".txt");
                            sw.WriteLine("Erro: BloqueioFinanceiro: " + oBloqueioFinanceiro.CodigoCliente.ToString());
                            sw.WriteLine(ex.Message);
                            sw.Close();
                            sw.Dispose();
                        }
                    }
                }
                catch (Exception er)
                {
                    Caminho = er.Message;
                }
                finally
                {
                    timer1.Enabled = true;
                    aConnection.Close();
                    aConnection.Dispose();
                }
            }
            else if (ExisteArquivo("LicencaAmbiental"))
            {
                try
                {
                    timer1.Enabled = false;
                    Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                    DataTable _dtAtualizacao = new DataTable();
                    clsLicencaAmbiental oLicencaAmbiental = new clsLicencaAmbiental();
                    clsLicencaAmbientalDados oLicencaAmbientalDados = new clsLicencaAmbientalDados();
                    string LancLinha;

                    //cria a conexão com o banco de dados
                    aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\WinSilc\\SILC.MDB");
                    aConnection.Open();

                    string s = "";
                    for (int i = 0; i < 1000; i++)
                    {
                        try
                        {
                            Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                            if (i == 0)
                                Caminho = Caminho + "LicencaAmbiental.txt";
                            else
                                Caminho = Caminho + "LicencaAmbiental" + i.ToString() + ".txt";

                            if (new FileInfo(Caminho).Exists)
                            {
                                using (StreamReader reader = new StreamReader(Caminho))
                                {
                                    LancLinha = reader.ReadLine();
                                    if (LancLinha != "")
                                    {
                                        //cria a conexão com o banco de dados
                                        aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\WinSilc\\SILC.MDB");
                                        aConnection.Open();
                                        s = "";
                                        s = s + "select * from LicencaAmbiental \n";
                                        s = s + "where  CodigoAterro = " + Convert.ToInt32(LancLinha) + " \n";
                                        OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                                        _dtAtualizacao = new DataTable();
                                        oDataAdapter.Fill(_dtAtualizacao);
                                        oDataAdapter.Dispose();
                                        foreach (DataRow dr in _dtAtualizacao.Rows)
                                        {
                                            if (dr["CodigoAterro"].ToString() != "" && dr["CodigoAterro"].ToString() != "0" &&
                                                dr["Codigo"].ToString() != "" && dr["Codigo"].ToString() != "0")
                                            {
                                                oLicencaAmbiental = new clsLicencaAmbiental();
                                                if (dr["CodigoAterro"].ToString() != "")
                                                    oLicencaAmbiental.CodigoAterro = Convert.ToInt32(dr["CodigoAterro"]);
                                                if (dr["Codigo"].ToString() != "")
                                                    oLicencaAmbiental.Codigo = Convert.ToInt32(dr["Codigo"]);
                                                oLicencaAmbiental.CodigoAtividade = dr["CodigoAtividade"].ToString();
                                                oLicencaAmbiental.NumeroLicenca = dr["NumeroLicenca"].ToString();
                                                oLicencaAmbiental.Obs = dr["Obs"].ToString();
                                                if (dr["PrazoValidade"].ToString() != "")
                                                    oLicencaAmbiental.PrazoValidade = Convert.ToDateTime(dr["PrazoValidade"]).ToString("yyyy-MM-dd");
                                                if (oLicencaAmbientalDados.DadoExiste(oLicencaAmbiental.CodigoAterro, oLicencaAmbiental.NumeroLicenca) == "Alterar")
                                                    oLicencaAmbientalDados.Alterar(oLicencaAmbiental, oLicencaAmbiental.Codigo);
                                                else
                                                    oLicencaAmbientalDados.Inserir(oLicencaAmbiental, oLicencaAmbiental.Codigo);
                                                i++;
                                            }
                                        }
                                    }
                                }
                                // apagar arquivo LicencaAmbiental
                                System.IO.File.Delete(Caminho);
                            }
                        }
                        catch (Exception ex)
                        {
                            StreamWriter sw = new StreamWriter("Erro_" + Caminho.Replace(".txt", "") + oLicencaAmbiental.CodigoAterro + ".txt");
                            sw.WriteLine("Erro: LAO: " + oLicencaAmbiental.CodigoAterro.ToString());
                            sw.WriteLine(ex.Message);
                            sw.Close();
                            sw.Dispose();
                        }
                    }
                }
                catch (Exception er)
                {
                    Caminho = er.Message;
                }
                finally
                {
                    timer1.Enabled = true;
                    aConnection.Close();
                    aConnection.Dispose();
                }
            }
            else if (ExisteArquivo("DTR"))
            {
                try
                {
                    timer1.Enabled = false;
                    Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                    DataTable _dtAtualizacao = new DataTable();
                    clsDTR oDTR = new clsDTR();
                    clsDTRDados oDTRDados = new clsDTRDados();
                    string LancLinha;

                    //cria a conexão com o banco de dados
                    aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\WinSilc\\SILC.MDB");
                    aConnection.Open();

                    string s = "";
                    for (int i = 0; i < 1000; i++)
                    {
                        try
                        {
                            Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                            if (i == 0)
                                Caminho = Caminho + "DTR.txt";
                            else
                                Caminho = Caminho + "DTR" + i.ToString() + ".txt";

                            if (new FileInfo(Caminho).Exists)
                            {
                                using (StreamReader reader = new StreamReader(Caminho))
                                {
                                    LancLinha = reader.ReadLine();
                                    if (LancLinha != "")
                                    {
                                        //cria a conexão com o banco de dados
                                        aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\WinSilc\\SILC.MDB");
                                        aConnection.Open();
                                        //seq        nunlanc  cdres nrmtr    impresso
                                        //00001114 - 223394 - 126 - 130815 - 0
                                        int _nuSeq = Convert.ToInt32(LancLinha.Split("-"[0])[0].ToString());
                                        int _nuLanc = Convert.ToInt32(LancLinha.Split("-"[0])[1].ToString());
                                        int _cdResiduo = Convert.ToInt32(LancLinha.Split("-"[0])[2].ToString());
                                        int _nuMTR = Convert.ToInt32(LancLinha.Split("-"[0])[3].ToString());
                                        s = "";
                                        s = s + "select * from DTR \n";
                                        s = s + "where  Sequencial = " + _nuSeq.ToString() + " \n";
                                        s = s + "and    NumeroLancamento = " + _nuLanc.ToString() + " \n";
                                        s = s + "and    CodigoTipoResiduo = " + _cdResiduo.ToString() + " \n";
                                        s = s + "and    NumeroMTR = " + _nuMTR.ToString() + " \n";
                                        OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                                        _dtAtualizacao = new DataTable();
                                        oDataAdapter.Fill(_dtAtualizacao);
                                        oDataAdapter.Dispose();
                                        foreach (DataRow dr in _dtAtualizacao.Rows)
                                        {
                                            if (dr["Sequencial"].ToString() != "" && dr["Sequencial"].ToString() != "0")
                                            {
                                                oDTR = new clsDTR();
                                                if (dr["Sequencial"].ToString() != "")
                                                    oDTR.Sequencial = Convert.ToInt32(dr["Sequencial"]);
                                                oDTR.ClienteColetado = dr["ClienteColetado"].ToString();
                                                if (dr["CodigoCaminhao"].ToString() != "")
                                                    oDTR.CodigoCaminhao = Convert.ToInt32(dr["CodigoCaminhao"]);
                                                if (dr["CodigoClienteColetado"].ToString() != "")
                                                    oDTR.CodigoClienteColetado = Convert.ToInt32(dr["CodigoClienteColetado"]);
                                                if (dr["CodigoClienteEmitiuMTR"].ToString() != "")
                                                    oDTR.CodigoClienteEmitiuMTR = Convert.ToInt32(dr["CodigoClienteEmitiuMTR"]);
                                                if (dr["CodigoLocalEntrega"].ToString() != "")
                                                    oDTR.CodigoLocalEntrega = Convert.ToInt32(dr["CodigoLocalEntrega"]);
                                                if (dr["CodigoMotorista"].ToString() != "")
                                                    oDTR.CodigoMotorista = Convert.ToInt32(dr["CodigoMotorista"]);
                                                if (dr["CodigoTipoResiduo"].ToString() != "")
                                                    oDTR.CodigoTipoResiduo = Convert.ToInt32(dr["CodigoTipoResiduo"]);
                                                if (dr["DataColeta"].ToString() != "")
                                                    oDTR.DataColeta = Convert.ToDateTime(dr["DataColeta"]).ToShortDateString();
                                                if (dr["DataSaida"].ToString() != "")
                                                    oDTR.DataSaida = Convert.ToDateTime(dr["DataSaida"]).ToShortDateString();
                                                if (dr["Fechado"].ToString() != "")
                                                    oDTR.Fechado = Convert.ToInt16(dr["Fechado"]);
                                                oDTR.HoraSaida = dr["HoraSaida"].ToString();
                                                if (dr["Imprimido"].ToString() != "")
                                                    oDTR.Imprimido = Convert.ToInt16(dr["Imprimido"]);
                                                oDTR.LocalDTR = dr["LocalDTR"].ToString();
                                                oDTR.LocalEntrega = dr["LocalEntrega"].ToString();
                                                if (dr["NumeroImpressao"].ToString() != "")
                                                    oDTR.NumeroImpressao = Convert.ToInt32(dr["NumeroImpressao"]);
                                                if (dr["NumeroLancamento"].ToString() != "")
                                                    oDTR.NumeroLancamento = Convert.ToInt32(dr["NumeroLancamento"]);
                                                if (dr["NumeroMTR"].ToString() != "")
                                                    oDTR.NumeroMTR = Convert.ToInt32(dr["NumeroMTR"]);
                                                if (dr["QtNova"].ToString() != "")
                                                    oDTR.QtNova = Convert.ToDecimal(dr["QtNova"]);
                                                oDTR.TipoResiduo = dr["TipoResiduo"].ToString();
                                                if (dr["TotalKg"].ToString() != "")
                                                    oDTR.TotalKg = Convert.ToDecimal(dr["TotalKg"]);
                                                if (dr["TotalQtNova"].ToString() != "")
                                                    oDTR.TotalQtNova = Convert.ToDecimal(dr["TotalQtNova"]);

                                                if (oDTRDados.DadoExiste(oDTR.Sequencial, oDTR.NumeroLancamento, oDTR.CodigoTipoResiduo, oDTR.NumeroMTR) == "Alterar")
                                                {
                                                    oDTRDados.Alterar(oDTR, oDTR.Sequencial, oDTR.NumeroLancamento, oDTR.CodigoTipoResiduo, oDTR.NumeroMTR);
                                                }
                                                else
                                                {
                                                    oDTRDados.Inserir(oDTR);
                                                }
                                                i++;
                                            }
                                        }
                                    }
                                }
                                // apagar arquivo DTR
                                System.IO.File.Delete(Caminho);
                            }
                        }
                        catch (Exception ex)
                        {
                            StreamWriter sw = new StreamWriter("Erro_" + Caminho.Replace(".txt", "") + oDTR.Sequencial + ".txt");
                            sw.WriteLine("Erro: DTR: " + oDTR.Sequencial.ToString());
                            sw.WriteLine(ex.Message);
                            sw.Close();
                            sw.Dispose();
                        }

                    }

                }
                catch (Exception er)
                {
                    Caminho = er.Message;
                }
                finally
                {
                    timer1.Enabled = true;
                    aConnection.Close();
                    aConnection.Dispose();
                }
            }
            else if (ExisteArquivo("DestinoFinal"))
            {
                try
                {
                    timer1.Enabled = false;
                    Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                    DataTable _dtAtualizacao = new DataTable();
                    clsDestinoFinal oDestino = new clsDestinoFinal();
                    clsDestinoFinalDados oDestinoDados = new clsDestinoFinalDados();
                    string LancLinha;

                    //cria a conexão com o banco de dados
                    aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\WinSilc\\SILC.MDB");
                    aConnection.Open();

                    string s = "";
                    for (int i = 0; i < 1000; i++)
                    {
                        try
                        {
                            Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                            if (i == 0)
                                Caminho = Caminho + "DestinoFinal.txt";
                            else
                                Caminho = Caminho + "DestinoFinal" + i.ToString() + ".txt";

                            if (new FileInfo(Caminho).Exists)
                            {
                                using (StreamReader reader = new StreamReader(Caminho))
                                {
                                    LancLinha = reader.ReadLine();
                                    if (LancLinha != "")
                                    {
                                        //cria a conexão com o banco de dados
                                        aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\WinSilc\\SILC.MDB");
                                        aConnection.Open();
                                        s = "";
                                        s = s + "select * from Aterro \n";
                                        s = s + "where  Codigo = " + Convert.ToInt32(LancLinha) + " \n";
                                        OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                                        _dtAtualizacao = new DataTable();
                                        oDataAdapter.Fill(_dtAtualizacao);
                                        oDataAdapter.Dispose();
                                        foreach (DataRow dr in _dtAtualizacao.Rows)
                                        {
                                            if (dr["Codigo"].ToString() != "" && dr["Codigo"].ToString() != "0")
                                            {
                                                oDestino = new clsDestinoFinal();
                                                if (dr["Codigo"].ToString() != "")
                                                    oDestino.Codigo = Convert.ToInt32(dr["Codigo"]);
                                                if (dr["Ativo"].ToString() != "")
                                                    oDestino.Ativo = Convert.ToInt16(dr["Ativo"]);
                                                oDestino.Bairro = dr["Bairro"].ToString();
                                                oDestino.Celular = dr["Celular"].ToString();
                                                oDestino.CEP = dr["CEP"].ToString();
                                                oDestino.Cidade = dr["Cidade"].ToString();
                                                oDestino.CNPJ = dr["CNPJ"].ToString();
                                                oDestino.DataCadastro = DateTime.Now.ToShortDateString();
                                                oDestino.eMail = dr["eMail"].ToString();
                                                if (dr["EmiteCDF"].ToString() != "")
                                                    oDestino.EmiteCDF = Convert.ToInt32(dr["EmiteCDF"]);
                                                oDestino.Endereco = dr["Endereco"].ToString();
                                                if (dr["Enviar_emailCDF"].ToString() != "")
                                                    oDestino.Enviar_emailCDF = Convert.ToInt32(dr["Enviar_emailCDF"]);
                                                if (dr["Enviar_emailMovResiduos"].ToString() != "")
                                                    oDestino.Enviar_emailMovResiduos = Convert.ToInt32(dr["Enviar_emailMovResiduos"]);
                                                oDestino.Fone = dr["Fone"].ToString();
                                                oDestino.LocalAterro = dr["LocalAterro"].ToString();
                                                oDestino.Nome = dr["Nome"].ToString();
                                                oDestino.NomeArqAss = dr["NomeArqAss"].ToString();
                                                oDestino.NomeArqLogo = dr["NomeArqLogo"].ToString();
                                                oDestino.NomeFantasia = dr["NomeFantasia"].ToString();
                                                oDestino.UF = dr["UF"].ToString();
                                                if (oDestinoDados.DadoExiste(oDestino.Codigo) == "Alterar")
                                                    oDestinoDados.Alterar(oDestino, oDestino.Codigo);
                                                else
                                                    oDestinoDados.Inserir(oDestino, oDestino.Codigo);
                                                i++;
                                            }
                                        }
                                    }
                                }
                                // apagar arquivo DestinoFinal
                                System.IO.File.Delete(Caminho);
                            }
                        }
                        catch (Exception ex)
                        {
                            StreamWriter sw = new StreamWriter("Erro_" + Caminho.Replace(".txt", "") + oDestino.Codigo + ".txt");
                            sw.WriteLine("Erro: Destino Final: " + oDestino.Codigo.ToString());
                            sw.WriteLine(ex.Message);
                            sw.Close();
                            sw.Dispose();
                        }
                    }
                }
                catch (Exception er)
                {
                    Caminho = er.Message;
                }
                finally
                {
                    timer1.Enabled = true;
                    aConnection.Close();
                    aConnection.Dispose();
                }
            }
            else if (ExisteArquivo("Containeres"))
            {
                try
                {
                    // cacambas
                    timer1.Enabled = false;
                    Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                    DataTable _dtAtualizacao = new DataTable();
                    clsCacambas oContaineres = new clsCacambas();
                    clsCacambaDados oContaineresDados = new clsCacambaDados();

                    string LancLinha;

                    //cria a conexão com o banco de dados
                    aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\WinSilc\\SILC.MDB");
                    aConnection.Open();

                    string s = "";
                    for (int i = 0; i < 1000; i++)
                    {
                        try
                        {
                            Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                            if (i == 0)
                                Caminho = Caminho + "Containeres.txt";
                            else
                                Caminho = Caminho + "Containeres" + i.ToString() + ".txt";

                            if (new FileInfo(Caminho).Exists)
                            {
                                using (StreamReader reader = new StreamReader(Caminho))
                                {
                                    LancLinha = reader.ReadLine();
                                    if (LancLinha != "")
                                    {
                                        //cria a conexão com o banco de dados
                                        aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\WinSilc\\SILC.MDB");
                                        aConnection.Open();
                                        s = "";
                                        s = s + "select * from Cacambas \n";
                                        s = s + "where  Codigo = " + Convert.ToInt32(LancLinha) + " \n";
                                        OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                                        _dtAtualizacao = new DataTable();
                                        oDataAdapter.Fill(_dtAtualizacao);
                                        oDataAdapter.Dispose();
                                        foreach (DataRow dr in _dtAtualizacao.Rows)
                                        {
                                            if (dr["Codigo"].ToString() != "" && dr["Codigo"].ToString() != "0")
                                            {
                                                oContaineres = new clsCacambas();
                                                if (dr["Codigo"].ToString() != "")
                                                    oContaineres.Codigo = Convert.ToInt32(dr["Codigo"]);
                                                if (dr["Capacidade"].ToString() != "")
                                                    oContaineres.Capacidade = Convert.ToDecimal(dr["Capacidade"]);
                                                oContaineres.Cor = dr["Cor"].ToString();
                                                oContaineres.DataCadastro = DateTime.Now.ToShortDateString();
                                                if (dr["EhLocal"].ToString() != "")
                                                    oContaineres.EhLocal = Convert.ToInt16(dr["EhLocal"]);
                                                if (dr["EhTerceiro"].ToString() != "")
                                                    oContaineres.EhTerceiro = Convert.ToInt16(dr["EhTerceiro"]);
                                                oContaineres.Inativo = 0;
                                                oContaineres.Numero = dr["Numero"].ToString();
                                                oContaineres.Tipo = dr["Tipo"].ToString();
                                                if (oContaineresDados.DadoExiste(oContaineres.Codigo) == "Alterar")
                                                    oContaineresDados.Alterar(oContaineres, oContaineres.Codigo);
                                                else
                                                    oContaineresDados.Inserir(oContaineres, oContaineres.Codigo);
                                                i++;
                                            }
                                        }
                                    }
                                }
                                // apagar arquivo Containeres
                                System.IO.File.Delete(Caminho);
                            }
                        }
                        catch (Exception ex)
                        {
                            StreamWriter sw = new StreamWriter("Erro_" + Caminho.Replace(".txt", "") + oContaineres.Codigo + ".txt");
                            sw.WriteLine("Erro: Destino Final: " + oContaineres.Codigo.ToString());
                            sw.WriteLine(ex.Message);
                            sw.Close();
                            sw.Dispose();
                        }
                    }
                }
                catch (Exception er)
                {
                    Caminho = er.Message;
                }
                finally
                {
                    timer1.Enabled = true;
                    aConnection.Close();
                    aConnection.Dispose();
                }
            }
            else if (ExisteArquivo("Funcionarios"))
            {
                try
                {
                    // Funcionarios
                    timer1.Enabled = false;
                    Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                    DataTable _dtAtualizacao = new DataTable();
                    clsFuncionarios oFuncionarios = new clsFuncionarios();
                    clsFuncionarioDados oFuncionarioDados = new clsFuncionarioDados();

                    string LancLinha;

                    //cria a conexão com o banco de dados
                    aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\WinSilc\\SILC.MDB");
                    aConnection.Open();

                    string s = "";
                    for (int i = 0; i < 1000; i++)
                    {
                        try
                        {
                            Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                            if (i == 0)
                                Caminho = Caminho + "Funcionarios.txt";
                            else
                                Caminho = Caminho + "Funcionarios" + i.ToString() + ".txt";

                            if (new FileInfo(Caminho).Exists)
                            {
                                using (StreamReader reader = new StreamReader(Caminho))
                                {
                                    LancLinha = reader.ReadLine();
                                    if (LancLinha != "")
                                    {
                                        //cria a conexão com o banco de dados
                                        aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\WinSilc\\SILC.MDB");
                                        aConnection.Open();
                                        s = "";
                                        s = s + "select * from Funcionarios \n";
                                        s = s + "where  Codigo = " + Convert.ToInt32(LancLinha) + " \n";
                                        OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                                        _dtAtualizacao = new DataTable();
                                        oDataAdapter.Fill(_dtAtualizacao);
                                        oDataAdapter.Dispose();
                                        foreach (DataRow dr in _dtAtualizacao.Rows)
                                        {
                                            if (dr["Codigo"].ToString() != "" && dr["Codigo"].ToString() != "0")
                                            {
                                                oFuncionarios = new clsFuncionarios();
                                                if (dr["Codigo"].ToString() != "")
                                                    oFuncionarios.Codigo = Convert.ToInt32(dr["Codigo"]);
                                                oFuncionarios.Bairro = dr["deBairr"].ToString();
                                                oFuncionarios.CEP = dr["nuCEP"].ToString();
                                                oFuncionarios.Cidade = dr["deCidd"].ToString();
                                                oFuncionarios.CPF = dr["nuCPF"].ToString();
                                                if (dr["dtAdms"].ToString() != "")
                                                    oFuncionarios.DataAdmissao = Convert.ToDateTime(dr["dtAdms"]).ToString("yyyy-MM-dd");
                                                if (dr["dtDems"].ToString() != "")
                                                    oFuncionarios.DataDemissao = Convert.ToDateTime(dr["dtDems"]).ToString("yyyy-MM-dd");
                                                oFuncionarios.Endereco = dr["deEndr"].ToString();
                                                oFuncionarios.Fone = dr["nuTelf"].ToString();
                                                oFuncionarios.Nome = dr["nmFunc"].ToString();
                                                oFuncionarios.NumeroCTPS = dr["nuCTPS"].ToString();
                                                oFuncionarios.RG = dr["nuRG"].ToString();
                                                oFuncionarios.Serie = dr["nuSere"].ToString();
                                                oFuncionarios.UF = dr["deUF"].ToString();

                                                if (oFuncionarioDados.DadoExiste(oFuncionarios.Codigo) == "Alterar")
                                                    oFuncionarioDados.Alterar(oFuncionarios, oFuncionarios.Codigo);
                                                else
                                                    oFuncionarioDados.Inserir(oFuncionarios, oFuncionarios.Codigo);
                                                i++;
                                            }
                                        }
                                    }
                                }
                                // apagar arquivo Funcionarios
                                System.IO.File.Delete(Caminho);
                            }
                        }
                        catch (Exception ex)
                        {
                            StreamWriter sw = new StreamWriter("Erro_" + Caminho.Replace(".txt", "") + oFuncionarios.Codigo + ".txt");
                            sw.WriteLine("Erro: Destino Final: " + oFuncionarios.Codigo.ToString());
                            sw.WriteLine(ex.Message);
                            sw.Close();
                            sw.Dispose();
                        }
                    }
                }
                catch (Exception er)
                {
                    Caminho = er.Message;
                }
                finally
                {
                    timer1.Enabled = true;
                    aConnection.Close();
                    aConnection.Dispose();
                }
            }
            else if (ExisteArquivo("Caminhoes"))
            {
                try
                {
                    // Caminhoes
                    timer1.Enabled = false;
                    Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                    DataTable _dtAtualizacao = new DataTable();
                    clsCaminhoes oCaminhoes = new clsCaminhoes();
                    clsCaminhoesDados oCaminhoesDados = new clsCaminhoesDados();

                    string LancLinha;

                    //cria a conexão com o banco de dados
                    aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\WinSilc\\SILC.MDB");
                    aConnection.Open();

                    string s = "";
                    for (int i = 0; i < 1000; i++)
                    {
                        try
                        {
                            Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                            if (i == 0)
                                Caminho = Caminho + "Caminhoes.txt";
                            else
                                Caminho = Caminho + "Caminhoes" + i.ToString() + ".txt";

                            if (new FileInfo(Caminho).Exists)
                            {
                                using (StreamReader reader = new StreamReader(Caminho))
                                {
                                    LancLinha = reader.ReadLine();
                                    if (LancLinha != "")
                                    {
                                        //cria a conexão com o banco de dados
                                        aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\WinSilc\\SILC.MDB");
                                        aConnection.Open();
                                        s = "";
                                        s = s + "select * from Caminhoes \n";
                                        s = s + "where  Codigo = " + Convert.ToInt32(LancLinha) + " \n";
                                        OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                                        _dtAtualizacao = new DataTable();
                                        oDataAdapter.Fill(_dtAtualizacao);
                                        oDataAdapter.Dispose();
                                        foreach (DataRow dr in _dtAtualizacao.Rows)
                                        {
                                            if (dr["Codigo"].ToString() != "" && dr["Codigo"].ToString() != "0")
                                            {
                                                oCaminhoes = new clsCaminhoes();
                                                if (dr["Codigo"].ToString() != "")
                                                    oCaminhoes.Codigo = Convert.ToInt32(dr["Codigo"]);
                                                if (dr["AnoFabricacao"].ToString() != "")
                                                    oCaminhoes.AnoFabricacao = Convert.ToInt32(dr["AnoFabricacao"]);
                                                if (dr["AnoModelo"].ToString() != "")
                                                    oCaminhoes.AnoModelo = Convert.ToInt32(dr["AnoModelo"]);
                                                oCaminhoes.Chassi = dr["Chassi"].ToString();
                                                oCaminhoes.Cidade = dr["Cidade"].ToString();
                                                oCaminhoes.Cor = dr["Cor"].ToString();
                                                if (dr["dtVctoSegr"].ToString() != "")
                                                    oCaminhoes.dtVctoSegr = Convert.ToDateTime(dr["dtVctoSegr"]).ToShortDateString();
                                                if (dr["Kilometragem"].ToString() != "")
                                                    oCaminhoes.Kilometragem = Convert.ToInt32(dr["Kilometragem"]);
                                                oCaminhoes.Marca = dr["Marca"].ToString();
                                                oCaminhoes.Modelo = dr["Modelo"].ToString();
                                                oCaminhoes.Placas = dr["Placas"].ToString();
                                                oCaminhoes.Renavam = dr["Renavam"].ToString();
                                                if (dr["ValorFranquia"].ToString() != "")
                                                    oCaminhoes.ValorFranquia = Convert.ToDecimal(dr["ValorFranquia"].ToString());
                                                if (dr["VctoIPVA"].ToString() != "")
                                                    oCaminhoes.VctoIPVA = Convert.ToDateTime(dr["VctoIPVA"]).ToShortDateString();
                                                if (dr["VctoLicenciamento"].ToString() != "")
                                                    oCaminhoes.VctoLicenciamento = Convert.ToDateTime(dr["VctoLicenciamento"]).ToShortDateString();
                                                if (dr["dtVctoSegr"].ToString() != "")
                                                    oCaminhoes.VencimentoSeguroFrota = Convert.ToDateTime(dr["dtVctoSegr"]).ToShortDateString();

                                                if (oCaminhoesDados.DadoExiste(oCaminhoes.Codigo) == "Alterar")
                                                    oCaminhoesDados.Alterar(oCaminhoes, oCaminhoes.Codigo);
                                                else
                                                    oCaminhoesDados.Inserir(oCaminhoes, oCaminhoes.Codigo);
                                                i++;
                                            }
                                        }
                                    }
                                }
                                // apagar arquivo Caminhoes
                                System.IO.File.Delete(Caminho);
                            }
                        }
                        catch (Exception ex)
                        {
                            StreamWriter sw = new StreamWriter("Erro_" + Caminho.Replace(".txt", "") + oCaminhoes.Codigo + ".txt");
                            sw.WriteLine("Erro: Destino Final: " + oCaminhoes.Codigo.ToString());
                            sw.WriteLine(ex.Message);
                            sw.Close();
                            sw.Dispose();
                        }
                    }
                }
                catch (Exception er)
                {
                    Caminho = er.Message;
                }
                finally
                {
                    timer1.Enabled = true;
                    aConnection.Close();
                    aConnection.Dispose();
                }
            }
            else if (ExisteArquivo("Residuos"))
            {
                try
                {
                    // Residuos
                    timer1.Enabled = false;
                    Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                    DataTable _dtAtualizacao = new DataTable();
                    clsResiduos oResiduos = new clsResiduos();
                    clsResiduoDados oResiduoDados = new clsResiduoDados();

                    string LancLinha;

                    //cria a conexão com o banco de dados
                    aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\WinSilc\\SILC.MDB");
                    aConnection.Open();

                    string s = "";
                    for (int i = 0; i < 1000; i++)
                    {
                        try
                        {
                            Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                            if (i == 0)
                                Caminho = Caminho + "Residuos.txt";
                            else
                                Caminho = Caminho + "Residuos" + i.ToString() + ".txt";

                            if (new FileInfo(Caminho).Exists)
                            {
                                using (StreamReader reader = new StreamReader(Caminho))
                                {
                                    LancLinha = reader.ReadLine();
                                    if (LancLinha != "")
                                    {
                                        //cria a conexão com o banco de dados
                                        aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\WinSilc\\SILC.MDB");
                                        aConnection.Open();
                                        s = "";
                                        s = s + "select * from Residuos \n";
                                        s = s + "where  Codigo = " + Convert.ToInt32(LancLinha) + " \n";
                                        OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                                        _dtAtualizacao = new DataTable();
                                        oDataAdapter.Fill(_dtAtualizacao);
                                        oDataAdapter.Dispose();
                                        foreach (DataRow dr in _dtAtualizacao.Rows)
                                        {
                                            if (dr["Codigo"].ToString() != "" && dr["Codigo"].ToString() != "0")
                                            {
                                                oResiduos = new clsResiduos();
                                                if (dr["Codigo"].ToString() != "")
                                                    oResiduos.Codigo = Convert.ToInt32(dr["Codigo"]);
                                                if (dr["Ativo"].ToString() != "")
                                                    oResiduos.Ativo = Convert.ToInt16(dr["Ativo"]);
                                                oResiduos.Classe = dr["Classe"].ToString();
                                                if (dr["CodigoDestinoFinal"].ToString() != "")
                                                    oResiduos.CodigoDestinoFinal = Convert.ToInt32(dr["CodigoDestinoFinal"]);
                                                if (dr["CodigoGrupoResiduo"].ToString() != "")
                                                    oResiduos.CodigoGrupoResiduo = Convert.ToInt32(dr["CodigoGrupoResiduo"]);
                                                if (dr["CodigoIBAMA"].ToString() != "")
                                                    oResiduos.CodigoIBAMA = Convert.ToInt32(dr["CodigoIBAMA"]);
                                                oResiduos.CodigoResiduoManifesto = dr["CodigoResiduoManifesto"].ToString();
                                                oResiduos.DataCadastro = DateTime.Now.ToShortDateString();
                                                oResiduos.Descricao = dr["Descricao"].ToString();
                                                oResiduos.DescricaoReduzida = dr["DescricaoReduzida"].ToString();
                                                if (dr["EhReciclavel"].ToString() != "")
                                                    oResiduos.EhReciclavel = Convert.ToInt16(dr["EhReciclavel"]);
                                                if (dr["EhServico"].ToString() != "")
                                                    oResiduos.EhServico = Convert.ToInt16(dr["EhServico"]);
                                                oResiduos.EstadoFisico = dr["EstadoFisico"].ToString();
                                                oResiduos.TecnologiaAplicada = dr["TecnologiaAplicada"].ToString();
                                                if (dr["M3PorTon"].ToString() != "")
                                                    oResiduos.M3PorTon = Convert.ToDecimal(dr["M3PorTon"]);
                                                oResiduos.Unidade = dr["Unidade"].ToString();
                                                if (oResiduoDados.DadoExiste(oResiduos.Codigo) == "Alterar")
                                                    oResiduoDados.Alterar(oResiduos, oResiduos.Codigo);
                                                else
                                                    oResiduoDados.Inserir(oResiduos, oResiduos.Codigo);
                                                i++;
                                            }
                                        }
                                    }
                                }
                                // apagar arquivo Residuos
                                System.IO.File.Delete(Caminho);
                            }
                        }
                        catch (Exception ex)
                        {
                            StreamWriter sw = new StreamWriter("Erro_" + Caminho.Replace(".txt", "") + oResiduos.Codigo + ".txt");
                            sw.WriteLine(oResiduos.Codigo.ToString());
                            sw.WriteLine(ex.Message);
                            sw.Close();
                            sw.Dispose();
                        }
                    }
                }
                catch (Exception er)
                {
                    Caminho = er.Message;
                }
                finally
                {
                    timer1.Enabled = true;
                    aConnection.Close();
                    aConnection.Dispose();
                }
            }
            else if (ExisteArquivo("NotasFiscais"))
            {
                try
                {
                    // Residuos
                    timer1.Enabled = false;
                    Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                    DataTable _dtAtualizacao = new DataTable();
                    clsNotasFiscais oNF = new clsNotasFiscais();
                    clsNotasFiscaisDados oNFdados = new clsNotasFiscaisDados();

                    string LancLinha;

                    //cria a conexão com o banco de dados
                    aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\WinSilc\\SILC.MDB");
                    aConnection.Open();

                    string s = "";
                    for (int i = 0; i < 1000; i++)
                    {
                        try
                        {
                            Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                            if (i == 0)
                                Caminho = Caminho + "NotasFiscais.txt";
                            else
                                Caminho = Caminho + "NotasFiscais" + i.ToString() + ".txt";

                            if (new FileInfo(Caminho).Exists)
                            {
                                using (StreamReader reader = new StreamReader(Caminho))
                                {
                                    LancLinha = reader.ReadLine();
                                    if (LancLinha != "")
                                    {
                                        //cria a conexão com o banco de dados
                                        aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\WinSilc\\SILC.MDB");
                                        aConnection.Open();
                                        s = "";
                                        s = s + "select top 1 * from NotasFiscais \n";
                                        s = s + "where  NumeroNF = " + LancLinha + " \n";
                                        s = s + "order  by NumeroNotaFiscal desc \n";
                                        OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                                        _dtAtualizacao = new DataTable();
                                        oDataAdapter.Fill(_dtAtualizacao);
                                        oDataAdapter.Dispose();
                                        foreach (DataRow dr in _dtAtualizacao.Rows)
                                        {
                                            if (dr["NumeroNF"].ToString() != "")
                                            {
                                                oNF = new clsNotasFiscais();
                                                if (dr["ArquivoParaImportacaoGerado"].ToString() != "")
                                                    oNF.ArquivoParaImportacaoGerado = Convert.ToInt32(dr["ArquivoParaImportacaoGerado"]);
                                                if (dr["Cancelada"].ToString() != "")
                                                    oNF.Cancelada = Convert.ToInt32(dr["Cancelada"]);
                                                if (dr["Classificacao"].ToString() != "")
                                                    oNF.Classificacao = Convert.ToInt32(dr["Classificacao"]);
                                                oNF.CodigoBROOKS_Impostos = dr["CodigoBROOKS_Impostos"].ToString();
                                                if (dr["CodigoCliente"].ToString() != "")
                                                    oNF.CodigoCliente = Convert.ToInt32(dr["CodigoCliente"]);
                                                if (dr["ContaGerencial"].ToString() != "")
                                                    oNF.ContaGerencial = Convert.ToInt32(dr["ContaGerencial"]);
                                                if (dr["DataEmissao"].ToString() != "")
                                                    oNF.DataEmissao = Convert.ToDateTime(dr["DataEmissao"]).ToString("yyyy-MM-dd");
                                                if (dr["DataReferencia"].ToString() != "")
                                                    oNF.DataReferencia = Convert.ToDateTime(dr["DataReferencia"]).ToString("yyyy-MM-dd");
                                                if (dr["DiasEntreVctos"].ToString() != "")
                                                    oNF.DiasEntreVctos = Convert.ToInt32(dr["DiasEntreVctos"]);
                                                oNF.Historico = dr["Historico"].ToString();
                                                if (dr["NFImpressa"].ToString() != "")
                                                    oNF.NFImpressa = Convert.ToInt32(dr["NFImpressa"]);
                                                if (dr["NumeroNF"].ToString() != "")
                                                    oNF.NumeroNF = Convert.ToInt32(dr["NumeroNF"]);
                                                if (dr["NumeroNotaFiscal"].ToString() != "")
                                                    oNF.NumeroNotaFiscal = Convert.ToInt32(dr["NumeroNotaFiscal"]);
                                                if (dr["NumeroParcelas"].ToString() != "")
                                                    oNF.NumeroParcelas = Convert.ToInt32(dr["NumeroParcelas"]);
                                                if (dr["NumeroRecibo"].ToString() != "")
                                                    oNF.NumeroRecibo = Convert.ToInt32(dr["NumeroRecibo"]);
                                                if (dr["PercentualCRF"].ToString() != "")
                                                    oNF.PercentualCRF = Convert.ToDecimal(dr["PercentualCRF"]);
                                                if (dr["PercentualINSS"].ToString() != "")
                                                    oNF.PercentualINSS = Convert.ToDecimal(dr["PercentualINSS"]);
                                                if (dr["PercentualIRRF"].ToString() != "")
                                                    oNF.PercentualIRRF = Convert.ToDecimal(dr["PercentualIRRF"]);
                                                if (dr["PercentualISS"].ToString() != "")
                                                    oNF.PercentualISS = Convert.ToDecimal(dr["PercentualISS"]);
                                                if (dr["TipoDocumento"].ToString() != "")
                                                    oNF.TipoDocumento = Convert.ToInt32(dr["TipoDocumento"]);
                                                if (dr["ValorBaseINSS"].ToString() != "")
                                                    oNF.ValorBaseINSS = Convert.ToDecimal(dr["ValorBaseINSS"]);
                                                if (dr["ValorCOFINS"].ToString() != "")
                                                    oNF.ValorCOFINS = Convert.ToDecimal(dr["ValorCOFINS"]);
                                                if (dr["ValorContrSocial"].ToString() != "")
                                                    oNF.ValorContrSocial = Convert.ToDecimal(dr["ValorContrSocial"]);
                                                if (dr["ValorDescontoIncondicionado"].ToString() != "")
                                                    oNF.ValorDescontoIncondicionado = Convert.ToDecimal(dr["ValorDescontoIncondicionado"]);
                                                if (dr["ValorISS"].ToString() != "")
                                                    oNF.ValorISS = Convert.ToDecimal(dr["ValorISS"]);
                                                if (dr["ValorPIS"].ToString() != "")
                                                    oNF.ValorPIS = Convert.ToDecimal(dr["ValorPIS"]);
                                                if (dr["ValorTotal"].ToString() != "")
                                                    oNF.ValorTotal = Convert.ToDecimal(dr["ValorTotal"]);
                                                if (dr["Vencimento"].ToString() != "")
                                                    oNF.Vencimento = Convert.ToDateTime(dr["Vencimento"]).ToString("yyyy-MM-dd");

                                                if (oNFdados.DadoExiste(oNF.NumeroNotaFiscal) == "Alterar")
                                                    oNFdados.Alterar(oNF, oNF.NumeroNotaFiscal);
                                                else
                                                    oNFdados.Inserir(oNF);

                                                clsCorpoNotasFiscais oCorpoNotasFiscais = new clsCorpoNotasFiscais();
                                                clsCorpoNotasFiscaisDados oCorpoNotasFiscaisDados = new clsCorpoNotasFiscaisDados();

                                                s = "";
                                                s = s + "select cnf.* from CorpoNotasFiscais cnf \n";
                                                s = s + "inner join NotasFiscais nf on nf.NumeroNotaFiscal = cnf.NumeroNotaFiscal \n";
                                                s = s + "where nf.NumeroNotaFiscal = " + oNF.NumeroNotaFiscal + " \n";
                                                OleDbDataAdapter oDataAdapter2 = new OleDbDataAdapter(s, aConnection);
                                                DataTable _dtAtualizacao2 = new DataTable();
                                                oDataAdapter2.Fill(_dtAtualizacao2);
                                                oDataAdapter2.Dispose();

                                                foreach (DataRow dr2 in _dtAtualizacao2.Rows)
                                                {
                                                    if (dr2["NumeroNotaFiscal"].ToString() != "" && dr2["NumeroNotaFiscal"].ToString() != "0")
                                                    {
                                                        oCorpoNotasFiscais = new clsCorpoNotasFiscais();
                                                        if (dr2["NumeroNotaFiscal"].ToString() != "")
                                                            oCorpoNotasFiscais.SequencialNotaFiscal = Convert.ToInt32(dr2["NumeroNotaFiscal"]);
                                                        oCorpoNotasFiscais.Descricao = dr2["Descricao"].ToString();
                                                        if (dr2["Linha"].ToString() != "")
                                                            oCorpoNotasFiscais.Linha = Convert.ToInt32(dr2["Linha"]);
                                                        if (dr2["PrecoUnitario"].ToString() != "")
                                                            oCorpoNotasFiscais.PrecoUnitario = Convert.ToDecimal(dr2["PrecoUnitario"]);
                                                        if (dr2["Quantidade"].ToString() != "")
                                                            oCorpoNotasFiscais.Quantidade = Convert.ToDecimal(dr2["Quantidade"]);
                                                        oCorpoNotasFiscais.Unidade = dr2["Unidade"].ToString();
                                                        if (dr2["Valor"].ToString() != "")
                                                            oCorpoNotasFiscais.Valor = Convert.ToDecimal(dr2["Valor"]);
                                                        if (oCorpoNotasFiscaisDados.DadoExiste(oCorpoNotasFiscais.SequencialNotaFiscal, oCorpoNotasFiscais.Linha) == "Alterar")
                                                        {
                                                            oCorpoNotasFiscaisDados.Alterar(oCorpoNotasFiscais, oCorpoNotasFiscais.SequencialNotaFiscal);
                                                        }
                                                        else
                                                        {
                                                            oCorpoNotasFiscaisDados.Inserir(oCorpoNotasFiscais, oCorpoNotasFiscais.SequencialNotaFiscal);
                                                        }
                                                    }
                                                }
                                            }
                                            //fecha a conexao 
                                            aConnection.Close();
                                            aConnection.Dispose();
                                        }
                                    }
                                }
                                // apagar arquivo Residuos
                                System.IO.File.Delete(Caminho);
                            }
                        }
                        catch (Exception ex)
                        {
                            StreamWriter sw = new StreamWriter("Erro_" + Caminho.Replace(".txt", "") + oNF.NumeroNF + ".txt");
                            sw.WriteLine(oNF.NumeroNF.ToString());
                            sw.WriteLine(ex.Message);
                            sw.Close();
                            sw.Dispose();
                        }
                    }
                }
                catch (Exception er)
                {
                    Caminho = er.Message;
                }
                finally
                {
                    timer1.Enabled = true;
                    aConnection.Close();
                    aConnection.Dispose();
                }
            }
            else if (ExisteArquivo("Contrato_DEFINITIVO_NÃO_RETORNAR"))
            {
                try
                {
                    // Residuos
                    timer1.Enabled = false;
                    Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";

                    DataTable _dtAtualizacao = new DataTable();
                    clsContratos oContratos = new clsContratos();
                    clsContratosDados oContratosDados = new clsContratosDados();
                    string LancLinha;
                    string s = "";
                    for (int i = 0; i < 1000; i++)
                    {
                        try
                        {
                            Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                            if (i == 0)
                                Caminho = Caminho + "Contrato.txt";
                            else
                                Caminho = Caminho + "Contrato" + i.ToString() + ".txt";

                            if (new FileInfo(Caminho).Exists)
                            {
                                using (StreamReader reader = new StreamReader(Caminho))
                                {
                                    LancLinha = reader.ReadLine();
                                    if (LancLinha != "")
                                    {
                                        //cria a conexão com o banco de dados
                                        try
                                        {
                                            aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\WinSilc\\SILC.MDB");
                                            aConnection.Open();
                                            s = "";
                                            s = s + "select * from Precos \n";
                                            s = s + "where  Codigo = " + Convert.ToInt32(LancLinha) + " \n";
                                            OleDbDataAdapter oDataAdapter = new OleDbDataAdapter(s, aConnection);
                                            _dtAtualizacao = new DataTable();
                                            oDataAdapter.Fill(_dtAtualizacao);
                                            oDataAdapter.Dispose();
                                        }
                                        finally
                                        {
                                            aConnection.Close();
                                            aConnection.Dispose();
                                        }
                                        foreach (DataRow dr in _dtAtualizacao.Rows)
                                        {
                                            if (dr["cdClnt"].ToString() != "" && dr["cdClnt"].ToString() != "0" &&
                                                dr["Codigo"].ToString() != "" && dr["Codigo"].ToString() != "0")
                                            {
                                                oContratos = new clsContratos();
                                                oContratos.AniversarioReajuste = dr["AniversarioReajuste"].ToString();
                                                if (dr["ApuracaoA"].ToString() != "")
                                                    oContratos.ApuracaoA = Convert.ToInt32(dr["ApuracaoA"]);
                                                if (dr["ApuracaoDe"].ToString() != "")
                                                    oContratos.ApuracaoDe = Convert.ToInt32(dr["ApuracaoDe"]);
                                                if (dr["AvisarDiasAntes"].ToString() != "")
                                                    oContratos.AvisarDiasAntes = Convert.ToInt16(dr["AvisarDiasAntes"]);
                                                if (dr["BrooksEnviaPLANFAT"].ToString() != "")
                                                    oContratos.BrooksEnviaPLANFAT = Convert.ToInt16(dr["BrooksEnviaPLANFAT"]);
                                                if (dr["ClienteAprovaPLANFAT"].ToString() != "")
                                                    oContratos.ClienteAprovaPLANFAT = Convert.ToInt16(dr["ClienteAprovaPLANFAT"]);
                                                if (dr["ClienteEnviaOC"].ToString() != "")
                                                    oContratos.ClienteEnviaOC = Convert.ToInt16(dr["ClienteEnviaOC"]);
                                                if (dr["ClienteRecebeNFDocs"].ToString() != "")
                                                    oContratos.ClienteRecebeNFDocs = Convert.ToInt16(dr["ClienteRecebeNFDocs"]);
                                                if (dr["Codigo"].ToString() != "")
                                                    oContratos.Codigo = Convert.ToInt32(dr["Codigo"]);
                                                if (dr["cdClnt"].ToString() != "")
                                                    oContratos.CodigoCliente = Convert.ToInt32(dr["cdClnt"]);
                                                if (dr["DataInicio"].ToString() != "" && dr["DataInicio"].ToString() != "0100-01-01")
                                                    oContratos.DataInicio = Convert.ToDateTime(dr["DataInicio"]).ToShortDateString();
                                                if (dr["DataReajuste"].ToString() != "" && dr["DataReajuste"].ToString() != "0100-01-01")
                                                    oContratos.DataReajuste = Convert.ToDateTime(dr["DataReajuste"]).ToShortDateString();
                                                if (dr["DataRecisao"].ToString() != "" && dr["DataRecisao"].ToString() != "0100-01-01")
                                                    oContratos.DataRecisao = Convert.ToDateTime(dr["DataRecisao"]).ToShortDateString();
                                                if (dr["DataRegistro"].ToString() != "" && dr["DataRegistro"].ToString() != "0100-01-01")
                                                    oContratos.DataRegistro = Convert.ToDateTime(dr["DataRegistro"]).ToShortDateString();
                                                if (dr["dtTerm"].ToString() != "" && dr["dtTerm"].ToString() != "0100-01-01")
                                                    oContratos.DataTermino = Convert.ToDateTime(dr["dtTerm"]).ToShortDateString();
                                                oContratos.DescricaoHistorico = dr["deHist"].ToString();
                                                if (dr["nuDiasMes"].ToString() != "")
                                                    oContratos.DiaVencimento = Convert.ToInt16(dr["nuDiasMes"]);
                                                if (dr["EmiteMTR"].ToString() != "")
                                                    oContratos.EmiteMTR = Convert.ToInt16(dr["EmiteMTR"]);
                                                if (dr["EnviarCDF"].ToString() != "")
                                                    oContratos.EnviarCDF = Convert.ToInt16(dr["EnviarCDF"]);
                                                if (dr["EnviarDDR"].ToString() != "")
                                                    oContratos.EnviarDDR = Convert.ToInt16(dr["EnviarDDR"]);
                                                if (dr["EnviarPLANFAT"].ToString() != "")
                                                    oContratos.EnviarPLANFAT = Convert.ToInt16(dr["EnviarPLANFAT"]);
                                                if (dr["EnviarRELGER"].ToString() != "")
                                                    oContratos.EnviarRELGER = Convert.ToInt16(dr["EnviarRELGER"]);
                                                oContratos.IndiceReajuste = dr["IndiceReajuste"].ToString();
                                                oContratos.MotivoRescisao = dr["MotivoRescisao"].ToString();
                                                if (dr["nuCaxaLocd"].ToString() != "")
                                                    oContratos.NumeroCaixasLocadas = Convert.ToInt16(dr["nuCaxaLocd"]);
                                                if (dr["NuCont"].ToString() != "")
                                                    oContratos.NumeroContrato = Convert.ToInt32(dr["NuCont"]);
                                                oContratos.Observacao = dr["Observacao"].ToString();
                                                oContratos.SituacaoRecisao = dr["SituacaoRecisao"].ToString();
                                                if (dr["vlCont"].ToString() != "")
                                                    oContratos.ValorContrato = Convert.ToDecimal(dr["vlCont"]);
                                                bool bEnviar_email = false;
                                                if (oContratosDados.DadoExiste(oContratos.Codigo) == "Alterar")
                                                {
                                                    // teste no alterar também
                                                    //bEnviar_email = true;
                                                    oContratosDados.Alterar(oContratos, oContratos.Codigo, oContratos.CodigoCliente);
                                                }
                                                else
                                                {
                                                    //bEnviar_email = true;
                                                    oContratosDados.Inserir(oContratos, oContratos.Codigo);
                                                }
                                                if (bEnviar_email)
                                                {
                                                    // Quando incluir um contrato - enviar e-mail avisando que está disponível seus documentos
                                                    string eMailQuemEnvia = "";
                                                    string eMailQuemEnvia2 = "";
                                                    string eMailQueLoga = "comercial3@brooksambiental.com.br";
                                                    string SenhaQueLoga = "gef*8855";
                                                    eMailQuemEnvia = "comercial3@brooksambiental.com.br";
                                                    eMailQuemEnvia2 = "comercial3@brooksambiental.com.br";
                                                    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();

                                                    client.Host = "smtp.gmail.com";
                                                    client.EnableSsl = true;
                                                    client.Port = 587;
                                                    client.Credentials = new System.Net.NetworkCredential(eMailQueLoga, SenhaQueLoga);

                                                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

                                                    mail.Sender = new System.Net.Mail.MailAddress(eMailQueLoga, "Contato BROOKS");
                                                    mail.From = new System.Net.Mail.MailAddress(eMailQuemEnvia, "Contato BROOKS");

                                                    clsEnderecos oEndereco = new clsEnderecos();
                                                    clsEnderecosDados oEnderecoDados = new clsEnderecosDados();
                                                    oEndereco = oEnderecoDados.PegaDados(oEndereco, oContratos.CodigoCliente, 1, 0);
                                                    if (oEndereco.email != "")
                                                    {
                                                        // teste
                                                        mail.To.Add(new System.Net.Mail.MailAddress("megasis.edson@gmail.com", "Contato BROOKS"));
                                                        // produção
                                                        //mail.To.Add(new System.Net.Mail.MailAddress(oEndereco.email, "Contato BROOKS"));
                                                    }

                                                    if (eMailQuemEnvia2 != "")
                                                        mail.To.Add(new System.Net.Mail.MailAddress("comercial@brooksambiental.com.br", "Contato BROOKS"));

                                                    mail.Subject = "Documentos disponíveis home BROOKS";
                                                    string sMensagem = "";
                                                    sMensagem = sMensagem + "Prezado Cliente";
                                                    sMensagem = sMensagem + Environment.NewLine;
                                                    sMensagem = sMensagem + Environment.NewLine;
                                                    sMensagem = sMensagem + "Informamos que disponibilizamos, em nosso site, documentos para fazer";
                                                    sMensagem = sMensagem + Environment.NewLine;
                                                    sMensagem = sMensagem + "consulta e downloads sempre que necessitar.";
                                                    sMensagem = sMensagem + Environment.NewLine;
                                                    sMensagem = sMensagem + "A página e seu acesso à Área do cliente é:";
                                                    sMensagem = sMensagem + Environment.NewLine;
                                                    sMensagem = sMensagem + Environment.NewLine;
                                                    sMensagem = sMensagem + "http://www.brooksambiental.com.br/forms/brooks/login.aspx";
                                                    sMensagem = sMensagem + Environment.NewLine;
                                                    sMensagem = sMensagem + Environment.NewLine;
                                                    clsClientes oCliente = new clsClientes();
                                                    clsClienteDados oClienteDados = new clsClienteDados();
                                                    oCliente = oClienteDados.PegaDados(oCliente, oContratos.CodigoCliente);
                                                    sMensagem = sMensagem + "Seu Login é: " + geral.Left(oCliente.NomeFantasia, 3) + oCliente.Codigo.ToString("000000") + "";
                                                    sMensagem = sMensagem + Environment.NewLine;
                                                    sMensagem = sMensagem + "     e Senha: " + geral.Left(oCliente.NomeFantasia, 3) + oCliente.Codigo.ToString("000000") + " - que são iguais.";
                                                    sMensagem = sMensagem + Environment.NewLine;
                                                    sMensagem = sMensagem + Environment.NewLine;
                                                    sMensagem = sMensagem + "Documentos disponíveis: Álvaras, ISO 9001 e Licenças Ambientais de Operação (LAO) da Brooks.";
                                                    sMensagem = sMensagem + Environment.NewLine;
                                                    sMensagem = sMensagem + "Você também pode ver/baixar a Declaração de Destinação de Resíduos (DDR).";
                                                    sMensagem = sMensagem + Environment.NewLine;
                                                    sMensagem = sMensagem + "As LAO's dos destinadores finais estarão disponíveis para ver/baixar na DDR.";
                                                    sMensagem = sMensagem + Environment.NewLine;
                                                    sMensagem = sMensagem + Environment.NewLine;
                                                    sMensagem = sMensagem + "Estamos à disposição para quaisquer dúvidas e esclarecimentos.";
                                                    sMensagem = sMensagem + Environment.NewLine;
                                                    sMensagem = sMensagem + Environment.NewLine;
                                                    sMensagem = sMensagem + "Atenciosamente ";
                                                    sMensagem = sMensagem + Environment.NewLine;
                                                    sMensagem = sMensagem + Environment.NewLine;
                                                    sMensagem = sMensagem + "Brooks Ambiental ";
                                                    sMensagem = sMensagem + Environment.NewLine;
                                                    mail.Body = sMensagem;
                                                    try
                                                    {
                                                        client.Send(mail);
                                                    }
                                                    catch (System.Exception erro)
                                                    {
                                                        StreamWriter sw = new StreamWriter("Erro_envio_email");
                                                        sw.WriteLine(erro.Message);
                                                        sw.Close();
                                                        sw.Dispose();
                                                    }
                                                    finally
                                                    {
                                                        mail = null;
                                                    }

                                                }
                                                // Reajustes
                                                //cria a conexão com o banco de dados
                                                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\WinSilc\\SILC.MDB");
                                                aConnection.Open();
                                                s = "";
                                                s = s + "select * from Reajustes \n";
                                                s = s + "where  CodigoPreco = " + Convert.ToInt32(LancLinha) + " \n";
                                                OleDbCommand oCommand = new OleDbCommand(s, aConnection);
                                                oCommand.CommandText = s;
                                                OleDbDataAdapter oDataAdapter2 = new OleDbDataAdapter(oCommand);
                                                _dtAtualizacao = new DataTable();
                                                oDataAdapter2.Fill(_dtAtualizacao);
                                                oDataAdapter2.Dispose();
                                                aConnection.Close();
                                                foreach (DataRow dr2 in _dtAtualizacao.Rows)
                                                {
                                                    clsContratosReajustes oContratoReajuste = new clsContratosReajustes();
                                                    clsContratosReajustesDados oContratoReajusteDados = new clsContratosReajustesDados();
                                                    if (dr2["CodigoPreco"].ToString() != "" && dr2["CodigoPreco"].ToString() != "0")
                                                    {
                                                        oContratoReajuste = new clsContratosReajustes();
                                                        if (dr2["CodigoPreco"].ToString() != "")
                                                            oContratoReajuste.CodigoContrato = Convert.ToInt32(dr2["CodigoPreco"]);
                                                        if (dr2["Data"].ToString() != "")
                                                            oContratoReajuste.Data = Convert.ToDateTime(dr2["Data"]).ToShortDateString();
                                                        oContratoReajuste.NumeroContrato = dr2["NumeroContrato"].ToString();
                                                        oContratoReajuste.Situacao = dr2["Situacao"].ToString();
                                                        oContratoReajuste.TipoNegociacao = dr2["TipoNegociacao"].ToString();
                                                        if (dr2["Valor"].ToString() != "")
                                                            oContratoReajuste.Valor = Convert.ToDecimal(dr2["Valor"]);

                                                        if (oContratoReajusteDados.DadoExiste(oContratoReajuste.CodigoContrato, oContratoReajuste.Data,
                                                                                              oContratoReajuste.NumeroContrato) == "Alterar")
                                                            oContratoReajusteDados.Alterar(oContratoReajuste, oContratoReajuste.CodigoContrato,
                                                                                           oContratos.CodigoCliente);
                                                        else
                                                            oContratoReajusteDados.Inserir(oContratoReajuste);
                                                    }
                                                }

                                                // ContratoResiduos 
                                                clsContratoResiduos oContratoResiduos = new clsContratoResiduos();
                                                clsContratoResiduosDados oContratoResiduosDados = new clsContratoResiduosDados();
                                                //cria a conexão com o banco de dados
                                                aConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\WinSilc\\SILC.MDB");
                                                aConnection.Open();
                                                s = "";
                                                s = s + "select * from ContratoResiduos \n";
                                                s = s + "where  CodigoContrato = " + Convert.ToInt32(LancLinha) + " \n";
                                                OleDbDataAdapter oDataAdapter3 = new OleDbDataAdapter(s, aConnection);
                                                _dtAtualizacao = new DataTable();
                                                oDataAdapter3.Fill(_dtAtualizacao);
                                                oDataAdapter3.Dispose();
                                                aConnection.Close();
                                                aConnection.Dispose();

                                                oContratoResiduosDados.Excluir(Convert.ToInt32(LancLinha));

                                                foreach (DataRow dr2 in _dtAtualizacao.Rows)
                                                {
                                                    if (dr2["CodigoCliente"].ToString() != "" && dr2["CodigoCliente"].ToString() != "0" &&
                                                        dr2["CodigoResiduo"].ToString() != "" && dr2["CodigoResiduo"].ToString() != "0" &&
                                                        dr2["CodigoContrato"].ToString() != "" && dr2["CodigoContrato"].ToString() != "0" &&
                                                        dr2["DataReajuste"].ToString() != "")
                                                    {
                                                        oContratoResiduos = new clsContratoResiduos();
                                                        if (dr2["CaixaDisponivel"].ToString() != "")
                                                            oContratoResiduos.CaixaDisponivel = Convert.ToInt32(dr2["CaixaDisponivel"]);
                                                        if (dr2["CodigoCaminhao"].ToString() != "")
                                                            oContratoResiduos.CodigoCaminhao = Convert.ToInt32(dr2["CodigoCaminhao"]);
                                                        if (dr2["CodigoCliente"].ToString() != "")
                                                            oContratoResiduos.CodigoCliente = Convert.ToInt32(dr2["CodigoCliente"]);
                                                        if (dr2["CodigoContrato"].ToString() != "")
                                                            oContratoResiduos.CodigoContrato = Convert.ToInt32(dr2["CodigoContrato"]);
                                                        if (dr2["CodigoResiduo"].ToString() != "")
                                                            oContratoResiduos.CodigoResiduo = Convert.ToInt32(dr2["CodigoResiduo"]);
                                                        oContratoResiduos.DataReajuste = Convert.ToDateTime(dr2["DataReajuste"]).ToShortDateString();
                                                        oContratoResiduos.DiasColeta = dr2["DiasColeta"].ToString();
                                                        oContratoResiduos.Franquia = dr2["Franquia"].ToString();
                                                        oContratoResiduos.FrequenciaColeta = dr2["FrequenciaColeta"].ToString();
                                                        oContratoResiduos.MesAnoBase = dr2["MesAnoBase"].ToString();
                                                        oContratoResiduos.Particularidade = dr2["Particularidade"].ToString();
                                                        oContratoResiduos.OBS = dr2["OBS"].ToString();
                                                        if (dr2["QuantidadeFranquia"].ToString() != "")
                                                            oContratoResiduos.QuantidadeFranquia = Convert.ToDecimal(dr2["QuantidadeFranquia"]);
                                                        oContratoResiduos.Roteiro = dr2["Roteiro"].ToString();
                                                        oContratoResiduos.TipoCaixa = dr2["TipoCaixa"].ToString();
                                                        oContratoResiduos.Unidade = dr2["Unidade"].ToString();
                                                        string _s_vu = dr2["ValorUnitario"].ToString();
                                                        oContratoResiduos.ValorUnitario = Convert.ToDecimal(_s_vu) * 100;
                                                        oContratoResiduosDados.Inserir(oContratoResiduos);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                // apagar arquivo Residuos
                                System.IO.File.Delete(Caminho);
                            }
                        }
                        catch (Exception ex)
                        {
                            StreamWriter sw = new StreamWriter("Erro_" + Caminho.Replace(".txt", "") + oContratos.Codigo + ".txt");
                            sw.WriteLine(oContratos.Codigo.ToString());
                            sw.WriteLine(ex.Message);
                            sw.Close();
                            sw.Dispose();
                        }
                    }
                }
                catch (Exception er)
                {
                    Caminho = er.Message;
                }
                finally
                {
                    timer1.Enabled = true;
                    aConnection.Close();
                    aConnection.Dispose();
                }
            }
            else if (ExisteArquivo("ExcluirReajuste"))
            {
                clsContratosReajustesDados oExcluirReajusteDados = new clsContratosReajustesDados();
                string LancLinha = "";

                try
                {
                    // ExcluirReajuste
                    timer1.Enabled = false;
                    Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";


                    for (int i = 0; i < 1000; i++)
                    {
                        try
                        {
                            Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                            if (i == 0)
                                Caminho = Caminho + "ExcluirReajuste.txt";
                            else
                                Caminho = Caminho + "ExcluirReajuste" + i.ToString() + ".txt";

                            if (new FileInfo(Caminho).Exists)
                            {
                                using (StreamReader reader = new StreamReader(Caminho))
                                {
                                    LancLinha = reader.ReadLine();
                                    if (LancLinha != "")
                                    {
                                        int _seq = Convert.ToInt32(LancLinha.Split("-"[0])[0].ToString());
                                        string _dt = LancLinha.Split("-"[0])[1];
                                        oExcluirReajusteDados.Excluir(_seq, _dt);
                                    }
                                }
                                // apagar arquivo ExcluirReajuste
                                System.IO.File.Delete(Caminho);
                            }
                        }
                        catch (Exception ex)
                        {
                            StreamWriter sw = new StreamWriter("Erro_" + Caminho.Replace(".txt", "") + LancLinha + ".txt");
                            sw.WriteLine("Erro: Excluir Reajuste: " + LancLinha);
                            sw.WriteLine(ex.Message);
                            sw.Close();
                            sw.Dispose();
                        }
                    }
                }
                catch (Exception er)
                {
                    Caminho = er.Message;
                }
                finally
                {
                    timer1.Enabled = true;
                }
            }
            else if (ExisteArquivo("ExcluirResiduoContrato"))
            {
                clsContratoResiduosDados oExcluirContratoResiduoDados = new clsContratoResiduosDados();
                string LancLinha = "";

                try
                {
                    // ExcluirReajuste
                    timer1.Enabled = false;
                    Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                    for (int i = 0; i < 1000; i++)
                    {
                        try
                        {
                            Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                            if (i == 0)
                                Caminho = Caminho + "ExcluirResiduoContrato.txt";
                            else
                                Caminho = Caminho + "ExcluirResiduoContrato" + i.ToString() + ".txt";

                            if (new FileInfo(Caminho).Exists)
                            {
                                using (StreamReader reader = new StreamReader(Caminho))
                                {
                                    LancLinha = reader.ReadLine();
                                    if (LancLinha != "")
                                    {
                                        int _seq = Convert.ToInt32(LancLinha.Split("-"[0])[0].ToString());
                                        int _cdResiduo = Convert.ToInt32(LancLinha.Split("-"[0])[1].ToString());
                                        string _dt = LancLinha.Split("-"[0])[2];
                                        int _cdCliente = Convert.ToInt32(LancLinha.Split("-"[0])[3].ToString());
                                        oExcluirContratoResiduoDados.Excluir(_seq, _cdResiduo, _cdCliente, _dt);
                                    }
                                }
                                // apagar arquivo ExcluirReajuste
                                System.IO.File.Delete(Caminho);
                            }
                        }
                        catch (Exception ex)
                        {
                            StreamWriter sw = new StreamWriter("Erro_" + Caminho.Replace(".txt", "") + LancLinha + ".txt");
                            sw.WriteLine("Erro: Excluir Resíduo do Contrato: " + LancLinha);
                            sw.WriteLine(ex.Message);
                            sw.Close();
                            sw.Dispose();
                        }
                    }
                }
                catch (Exception er)
                {
                    Caminho = er.Message;
                }
                finally
                {
                    timer1.Enabled = true;
                }
            }
            else if (ExisteArquivo("ExcluirLancamento"))
            {
                string LancLinha = "";
                try
                {
                    // ExcluirLancamento
                    timer1.Enabled = false;
                    Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                    for (int i = 0; i < 1000; i++)
                    {
                        try
                        {
                            Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                            if (i == 0)
                                Caminho = Caminho + "ExcluirLancamento.txt";
                            else
                                Caminho = Caminho + "ExcluirLancamento" + i.ToString() + ".txt";

                            if (new FileInfo(Caminho).Exists)
                            {
                                using (StreamReader reader = new StreamReader(Caminho))
                                {
                                    LancLinha = reader.ReadLine();
                                    if (LancLinha != "")
                                    {
                                        int _nuLanc = Convert.ToInt32(LancLinha.Split("-"[0])[0].ToString());
                                        clsLancamentosDados oExcluirLancamentoDados = new clsLancamentosDados();
                                        clsLancamentoMTRDados oExcluirLancamntosMTRDados = new clsLancamentoMTRDados();
                                        clsAterroSanitarioDados oExcluirAterroSanitarioDados = new clsAterroSanitarioDados();
                                        try
                                        {
                                            oExcluirLancamentoDados.Excluir(_nuLanc);
                                        }
                                        finally
                                        {
                                            oExcluirLancamntosMTRDados.Excluir(_nuLanc, "");
                                            oExcluirAterroSanitarioDados.ExcluirNumeroLancamento(_nuLanc);
                                        }
                                    }
                                }
                                // apagar arquivo ExcluirLancamento
                                System.IO.File.Delete(Caminho);
                            }
                        }
                        catch (Exception ex)
                        {
                            StreamWriter sw = new StreamWriter("Erro_" + Caminho.Replace(".txt", "") + LancLinha + ".txt");
                            sw.WriteLine("Erro: Excluir Lançamento: " + LancLinha);
                            sw.WriteLine(ex.Message);
                            sw.Close();
                            sw.Dispose();
                        }
                    }
                }
                catch (Exception er)
                {
                    Caminho = er.Message;
                }
                finally
                {
                    timer1.Enabled = true;
                }
            }
            else if (ExisteArquivo("ExcluirLancamentoMTR"))
            {
                string LancLinha = "";
                try
                {
                    // ExcluirLancamento
                    timer1.Enabled = false;
                    Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                    for (int i = 0; i < 1000; i++)
                    {
                        try
                        {
                            Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
                            if (i == 0)
                                Caminho = Caminho + "ExcluirLancamentoMTR.txt";
                            else
                                Caminho = Caminho + "ExcluirLancamentoMTR" + i.ToString() + ".txt";

                            if (new FileInfo(Caminho).Exists)
                            {
                                using (StreamReader reader = new StreamReader(Caminho))
                                {
                                    LancLinha = reader.ReadLine();
                                    if (LancLinha != "")
                                    {
                                        int _nuLanc = Convert.ToInt32(LancLinha.Split("-"[0])[0].ToString());
                                        int _nuMTR = Convert.ToInt32(LancLinha.Split("-"[0])[1].ToString());
                                        int _cdResiduo = Convert.ToInt32(LancLinha.Split("-"[0])[2].ToString());
                                        clsLancamentoMTRDados oExcluirLancamntosMTRDados = new clsLancamentoMTRDados();
                                        oExcluirLancamntosMTRDados.Excluir(_nuLanc, _nuMTR, _cdResiduo);
                                        clsAterroSanitarioDados oExcluirAterroSanitarioDados = new clsAterroSanitarioDados();
                                        oExcluirAterroSanitarioDados.Excluir(_nuLanc, _nuMTR, _cdResiduo);
                                    }
                                }
                                // apagar arquivo ExcluirLancamento
                                System.IO.File.Delete(Caminho);
                            }
                        }
                        catch (Exception ex)
                        {
                            StreamWriter sw = new StreamWriter("Erro_" + Caminho.Replace(".txt", "") + LancLinha + ".txt");
                            sw.WriteLine("Erro: Excluir Lançamento: " + LancLinha);
                            sw.WriteLine(ex.Message);
                            sw.Close();
                            sw.Dispose();
                        }
                    }
                }
                catch (Exception er)
                {
                    Caminho = er.Message;
                }
                finally
                {
                    timer1.Enabled = true;
                }
            }
            else if (oExportacaoRadarDados.ExisteArquivoNaoGerado())
            {
                clsExportacaoRadar oExpRadar = new clsExportacaoRadar();
                oExportacaoRadarDados.PegaArquivoNaoGerado(oExpRadar);
                if (oExpRadar.Tabela == "Clientes")
                {
                    SalvaArquivoClienteParaImportacaoRadar(oExpRadar.CodigoNumero, oExpRadar.Sequencial);
                    Thread.Sleep(200);
                }
                else if (oExpRadar.Tabela == "NotasFiscais")
                {
                    SalvaArquivoNotasFiscaisParaImportacaoRadar(oExpRadar.CodigoNumero, oExpRadar.Sequencial);
                    Thread.Sleep(200);
                }
                else if (oExpRadar.Tabela == "Recibo" || oExpRadar.Tabela == "Fatura")
                {
                    SalvaArquivoReciboFaturaImportacaoRadar(oExpRadar.CodigoNumero, oExpRadar.Sequencial);
                    Thread.Sleep(2000);
                }
            }
            else if (!File.Exists("\\\\SERVIDOR\\WinSILC\\ImportacaoWebSILC\\Radar\\ClienteSILC.txt"))
            {
                if (Directory.GetFiles("\\\\SERVIDOR\\WinSILC\\ImportacaoWebSILC\\Radar").Length > 0)
                {
                    bool bExisteArquivo = false;
                    foreach (string _nomeArquivo in Directory.GetFiles("\\\\SERVIDOR\\WinSILC\\ImportacaoWebSILC\\Radar"))
                    {
                        if (_nomeArquivo.IndexOf("ClienteSILC") > -1)
                        {
                            bExisteArquivo = true;
                            break;
                        }
                    }
                    if (bExisteArquivo)
                    {
                        int i = 0;
                        while (i < 10000)
                        {
                            i++;
                            if (File.Exists("\\\\SERVIDOR\\WinSILC\\ImportacaoWebSILC\\Radar\\ClienteSILC" + i + ".txt"))
                            {
                                File.Move("\\\\SERVIDOR\\WinSILC\\ImportacaoWebSILC\\Radar\\ClienteSILC" + i + ".txt", "\\\\SERVIDOR\\WinSILC\\ImportacaoWebSILC\\Radar\\ClienteSILC.txt");
                                break;
                            }
                        }
                    }
                }
            }
            if (!File.Exists("\\\\SERVIDOR\\WinSILC\\ImportacaoWebSILC\\Radar\\MovimentoServicosInclusao.txt"))
            {
                if (Directory.GetFiles("\\\\SERVIDOR\\WinSILC\\ImportacaoWebSILC\\Radar").Length > 0)
                {
                    bool bExisteArquivo = false;
                    foreach (string _nomeArquivo in Directory.GetFiles("\\\\SERVIDOR\\WinSILC\\ImportacaoWebSILC\\Radar"))
                    {
                        if (_nomeArquivo.IndexOf("MovimentoServicosInclusao") > -1)
                        {                            
                            bExisteArquivo = true;
                            break;
                        }
                    }
                    if (bExisteArquivo)
                    {
                        int i = 0;
                        while (i < 10000)
                        {
                            i++;
                            if (File.Exists("\\\\SERVIDOR\\WinSILC\\ImportacaoWebSILC\\Radar\\MovimentoServicosInclusao" + i + ".txt"))
                            {
                                File.Move("\\\\SERVIDOR\\WinSILC\\ImportacaoWebSILC\\Radar\\MovimentoServicosInclusao" + i + ".txt", "\\\\SERVIDOR\\WinSILC\\ImportacaoWebSILC\\Radar\\MovimentoServicosInclusao.txt");
                                break;
                            }
                        }
                    }
                }
            }
        }

        // outros eventos 
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (base.WindowState == FormWindowState.Minimized)
            {
                base.Hide();
            }
        }

        private bool ExisteArquivo(string pTabela)
        {
            // pTabela é a tabela do DB
            Caminho = "c:\\WinSILC\\ImportacaoWebSILC\\";
            bool bExisteArquivo = false;
            string _arq = Caminho + pTabela + ".txt";
            for (int i = 0; i < 1000; i++)
            {
                if (i > 0)
                    _arq = Caminho + pTabela + i.ToString() + ".txt";
                FileInfo oFInfo = new FileInfo(_arq);
                if ((oFInfo).Exists)
                {
                    bExisteArquivo = true;
                    break;
                }
            }
            return bExisteArquivo;
        }

        private void ExportaWebSILC_Load(object sender, EventArgs e)
        {

        }

        // Sempre que inserido ou alterado cliente no webSILC, salvar na Pasta 
        private void SalvaArquivoClienteParaImportacaoRadar(int pCodigoCliente, int pSequencial)
        {
            string _s = "";
            string pathExportacao = "";
            if (Directory.Exists("\\\\SERVIDOR\\WinSILC\\ImportacaoWebSILC\\Radar"))
            {
                pathExportacao = "\\\\SERVIDOR\\WinSILC\\ImportacaoWebSILC\\Radar";
            }
            if (!Directory.Exists("\\\\SERVIDOR\\WinSILC\\ImportacaoWebSILC\\Radar"))
            {
                _s = _s = "Importação Automática do RADAR não está configurada! \n";
                _s = _s + "Cliente não foi Importado para o Radar! \n";
                _s = _s + "Por falta da Pasta e arquivo em: Radar" + "\n";
                _s = _s + "Informe urgentemente a TI para resolver o problema!";
                StreamWriter sw = new StreamWriter("Erro_Importacao_radar.txt");
                sw.WriteLine(_s);
                sw.Close();
                sw.Dispose();
            }
            if (pathExportacao != "")
            {
                string ArquivoDeExportacaoCliente = pathExportacao + "\\ClienteSILC.txt";
                int i = 1;
                while (true)
                {
                    if (!File.Exists(ArquivoDeExportacaoCliente))
                        break;
                    else
                        ArquivoDeExportacaoCliente = pathExportacao + "\\ClienteSILC" + i + ".txt";
                    i++;
                    if (i > 10000)
                        break;
                }

                if (pSequencial > 0)
                {
                    clsClientes oClientes = new clsClientes();
                    clsClienteDados oClientesDados = new clsClienteDados();
                    oClientesDados.PegaDados(oClientes, Convert.ToInt32(pCodigoCliente));
                    try
                    {
                        if (oClientes.Codigo > 0)
                        {
                            // gera arquivo para importação
                            StreamWriter x = File.CreateText(ArquivoDeExportacaoCliente);

                            string sLn = "";
                            string sDelimitador = "|";
                            sLn = sLn + "C" + sDelimitador;
                            sLn = sLn + oClientes.Nome + sDelimitador;
                            sLn = sLn + oClientes.NomeFantasia + sDelimitador;
                            sLn = sLn + geral.RetiraCharsCNPJCPF(oClientes.CNPJ_CPF) + sDelimitador;
                            if (geral.RetiraCharsCNPJCPF(oClientes.CNPJ_CPF).Length > 11)
                                sLn = sLn + "J" + sDelimitador;
                            else
                                sLn = sLn + "F" + sDelimitador;
                            if (oClientes.RG_IE == "")
                                sLn = sLn + "Isento" + sDelimitador;
                            else
                                sLn = sLn + oClientes.RG_IE + sDelimitador;
                            clsEnderecos oEndereco0 = new clsEnderecos();
                            clsEnderecosDados oEnderecoDados = new clsEnderecosDados();
                            oEnderecoDados.PegaDados(oEndereco0, oClientes.Codigo, 0, 0);
                            sLn = sLn + oEndereco0.endereco + sDelimitador;
                            sLn = sLn + oEndereco0.Numero + sDelimitador;
                            sLn = sLn + oEndereco0.Bairro + sDelimitador;
                            sLn = sLn + " " + oEndereco0.CodigoMunicipio + " " + sDelimitador;
                            sLn = sLn + oEndereco0.CEP + sDelimitador;
                            sLn = sLn + oEndereco0.Fone1 + sDelimitador;
                            sLn = sLn + oEndereco0.Fone2 + sDelimitador;
                            sLn = sLn + " " + oEndereco0.DDD1 + " " + sDelimitador;
                            sLn = sLn + oEndereco0.Fone3 + sDelimitador;
                            sLn = sLn + oEndereco0.email + sDelimitador;
                            sLn = sLn + " " + oEndereco0.DDD2 + " " + sDelimitador;
                            sLn = sLn + oEndereco0.Contato + sDelimitador;
                            string[] Complemento = new string[3];
                            Complemento[0] = oEndereco0.Complemento;

                            clsEnderecos oEndereco1 = new clsEnderecos();
                            oEnderecoDados.PegaDados(oEndereco1, oClientes.Codigo, 1, 0);
                            sLn = sLn + oEndereco1.endereco + sDelimitador;
                            sLn = sLn + oEndereco1.Numero + sDelimitador;
                            sLn = sLn + oEndereco1.Bairro + sDelimitador;
                            sLn = sLn + " " + oEndereco1.CodigoMunicipio + " " + sDelimitador;
                            sLn = sLn + oEndereco1.CEP + sDelimitador;
                            sLn = sLn + oEndereco1.Fone1 + sDelimitador;
                            sLn = sLn + " " + oEndereco1.DDD1 + " " + sDelimitador;
                            sLn = sLn + oEndereco1.email + sDelimitador;
                            sLn = sLn + oEndereco1.Contato + sDelimitador;
                            Complemento[1] = oEndereco1.Complemento;

                            clsEnderecos oEndereco2 = new clsEnderecos();
                            oEnderecoDados.PegaDados(oEndereco2, oClientes.Codigo, 2, 0);
                            sLn = sLn + oEndereco2.endereco + sDelimitador;
                            sLn = sLn + oEndereco2.Numero + sDelimitador;
                            sLn = sLn + oEndereco2.Bairro + sDelimitador;
                            sLn = sLn + " " + oEndereco2.CodigoMunicipio + " " + sDelimitador;
                            sLn = sLn + oEndereco2.CEP + sDelimitador;
                            sLn = sLn + oEndereco2.Fone1 + sDelimitador;
                            sLn = sLn + " " + oEndereco2.DDD1 + " " + sDelimitador;
                            sLn = sLn + oEndereco2.email + sDelimitador;
                            sLn = sLn + oEndereco2.Contato + sDelimitador;
                            Complemento[2] = oEndereco2.Complemento;

                            sLn = sLn + Complemento[0] + sDelimitador;
                            sLn = sLn + Complemento[1] + sDelimitador;
                            sLn = sLn + Complemento[2] + sDelimitador;

                            // Cobrança = Faturamento = 1
                            sLn = sLn + oEndereco1.endereco + sDelimitador;
                            sLn = sLn + oEndereco1.Numero + sDelimitador;
                            sLn = sLn + oEndereco1.Bairro + sDelimitador;
                            sLn = sLn + " " + oEndereco1.CodigoMunicipio + " " + sDelimitador;
                            sLn = sLn + oEndereco1.CEP + sDelimitador;
                            sLn = sLn + oEndereco1.Fone1 + sDelimitador;
                            sLn = sLn + " " + oEndereco1.DDD1 + " " + sDelimitador;
                            sLn = sLn + oEndereco1.email + sDelimitador;
                            sLn = sLn + oEndereco1.Contato + sDelimitador;

                            sLn = sLn + oClientes.CodigoTipoCobranca.ToString("00");

                            x.WriteLine(sLn);

                            //fechando o arquivo texto 
                            x.Close();
                        }
                    }
                    finally
                    {
                        oExportacaoRadarDados.SalvarComoGerado(pSequencial);
                    }
                }
            }
        }

        // Sempre que inserida NotaFiscal no webSILC, salvar na Pasta 
        private void SalvaArquivoNotasFiscaisParaImportacaoRadar(int pNumeroNotaFiscal, int pSequencial)
        {
            string _s = "";
            string pathExportacao = "";
            if (Directory.Exists("\\\\SERVIDOR\\WinSILC\\ImportacaoWebSILC\\Radar"))
            {
                pathExportacao = "\\\\SERVIDOR\\WinSILC\\ImportacaoWebSILC\\Radar";
            }
            if (!Directory.Exists("\\\\SERVIDOR\\WinSILC\\ImportacaoWebSILC\\Radar"))
            {
                _s = _s = "Importação Automática do RADAR não está configurada! \n";
                _s = _s + "NotaFiscal/Fatura não foi Importada para o Radar! \n";
                _s = _s + "Por falta da Pasta e arquivo em: Radar" + "\n";
                _s = _s + "Informe urgentemente a TI para resolver o problema!";
                StreamWriter sw = new StreamWriter("Erro_Importacao_radar_NotaFiscal.txt");
                sw.WriteLine(_s);
                sw.Close();
                sw.Dispose();
            }
            if (pathExportacao != "")
            {
                // procura próximo arquivo a ser criado
                // criar um número a mais do último que foi criado
                string ArquivoExportacao = "";
                int iArq = 1000;
                while (true)
                {
                    //procurar arquivos criados e parar no último, ainda não criado
                    ArquivoExportacao = pathExportacao + "\\MovimentoServicosInclusao" + iArq + ".txt";
                    if (File.Exists(ArquivoExportacao))
                    {
                        iArq++;
                        ArquivoExportacao = pathExportacao + "\\MovimentoServicosInclusao" + iArq + ".txt";
                        break;
                    }
                    iArq--;
                    if (iArq <= 0)
                        break;
                }

                int i = 0;
                if (pNumeroNotaFiscal > 0)
                {
                    clsNotasFiscais oNotasFiscais = new clsNotasFiscais();
                    clsNotasFiscaisDados oNotasFiscaisDados = new clsNotasFiscaisDados();
                    oNotasFiscaisDados.PegaDados(oNotasFiscais, pNumeroNotaFiscal);
                    if (oNotasFiscais.NumeroNotaFiscal > 0)
                    {
                        oCliente = new clsClientes();
                        oClienteDados = new clsClienteDados();
                        oClienteDados.PegaDados(oCliente, oNotasFiscais.CodigoCliente);

                        // gera arquivo para importação
                        StreamWriter x = File.CreateText(ArquivoExportacao);

                        string sLn = "";
                        string DataVendimento = "";
                        string sDelimitador = ";";
                        string DataVencimentoParcela2 = "";

                        // Tipo de Linha
                        sLn = sLn + "1" + sDelimitador;

                        // 2 - Filial
                        sLn = sLn + "1" + sDelimitador;

                        clsParametros oBrooks = new clsParametros();
                        clsParametrosDados oBrooksDados = new clsParametrosDados();
                        oBrooksDados.PegaDados(oBrooks, 1);

                        // 3 - CNPJ/CPF da filial - matriz - BROOKS
                        sLn = sLn + oBrooks.CNPJ_CPF + sDelimitador; // não existe - branco

                        // 4 - CNPJ cliente
                        if (oCliente.CNPJ_Faturamento != null && oCliente.CNPJ_Faturamento != "")
                        {
                            sLn = sLn + oCliente.CNPJ_Faturamento + sDelimitador;
                            if (geral.RetiraCharsCNPJCPF(oCliente.CNPJ_Faturamento).Length >= 14)
                                oCliente.Pessoa = 2;
                            else
                                oCliente.Pessoa = 1;
                        }
                        else
                        {
                            sLn = sLn + oCliente.CNPJ_CPF + sDelimitador;
                            if (geral.RetiraCharsCNPJCPF(oCliente.CNPJ_CPF).Length >= 14)
                                oCliente.Pessoa = 2;
                            else
                                oCliente.Pessoa = 1;
                        }

                        // 5 - Natureza de operação (CFOP)
                        clsAliquotaImpostos o_RetencoesNF = new clsAliquotaImpostos();
                        clsAliquotaImpostosDados o_RetencoesDados = new clsAliquotaImpostosDados();
                        o_RetencoesDados.PegaAliquotas(o_RetencoesNF, oNotasFiscais.CodigoBROOKS_Impostos);

                        ValorTotal = oNotasFiscais.ValorTotal;
                        CalcularImpostos(o_RetencoesNF, oCliente.Codigo.ToString(), oNotasFiscais.DataEmissao, oNotasFiscais);

                        clsEnderecos oEndereco = new clsEnderecos();
                        clsEnderecosDados oEnderecoDados = new clsEnderecosDados();
                        // pega dados endereço obra / local serviços
                        oEnderecoDados.PegaDados(oEndereco, oCliente.Codigo, 2, 0);

                        // municipio de são josé não aceita issrf - no caso de condominio
                        if (oEndereco.CodigoMunicipio == 83275 && geral.Left(oNotasFiscais.CodigoBROOKS_Impostos, 1) == "G")
                            ValorLiquido = oNotasFiscais.ValorTotal - ValorCOFINS - ValorContrSocial - ValorINSS - ValorIRRF - ValorPIS;
                        else
                            ValorLiquido = oNotasFiscais.ValorTotal - ValorCOFINS - ValorContrSocial - ValorINSS - ValorIRRF - ValorISS - ValorPIS;
                        if (o_RetencoesNF.Sequencial >= 10)
                            sLn = sLn + "80" + o_RetencoesNF.Sequencial + sDelimitador; // aguardando retorno
                        else
                            sLn = sLn + "800" + o_RetencoesNF.Sequencial + sDelimitador; // aguardando retorno
                        // 6 - Número do documento / NFs
                        sLn = sLn + oNotasFiscais.NumeroNF + sDelimitador;

                        // 7 - Documento Final
                        sLn = sLn + oNotasFiscais.NumeroNF + sDelimitador;

                        // 8 - Modelo de documento
                        sLn = sLn + "3" + sDelimitador;

                        // 9 - Espécie do documento
                        if (oNotasFiscais.TipoDocumento == 5)
                            sLn = sLn + "FAT" + sDelimitador;
                        else
                            sLn = sLn + "NFS" + sDelimitador;

                        //10 - Série do documento
                        sLn = sLn + "U" + sDelimitador;

                        //11 - Data de entrada
                        sLn = sLn + Convert.ToDateTime(oNotasFiscais.DataEmissao).ToString("dd/MM/yy") + sDelimitador;

                        //12 - Data de emissão
                        sLn = sLn + Convert.ToDateTime(oNotasFiscais.DataEmissao).ToString("dd/MM/yy") + sDelimitador;

                        oEndereco = new clsEnderecos();
                        oEnderecoDados = new clsEnderecosDados();

                        //13 - Estado de destino
                        oEnderecoDados.PegaDados(oEndereco, oCliente.Codigo, 1, 0);

                        clsMunicipios oMunicipio = new clsMunicipios();
                        clsMunicipiosDados oMunicipioDados = new clsMunicipiosDados();
                        oMunicipioDados.PegaDados(oMunicipio, oEndereco.CodigoMunicipio);
                        if (oMunicipio.Codigo > 0)
                            sLn = sLn + oMunicipio.UF + sDelimitador;
                        else
                            sLn = sLn + "SC" + sDelimitador;

                        //14 - Estado de origem
                        sLn = sLn + "SC" + sDelimitador;

                        //15 - V - Pgto a vista - P a prazo / S sem pgto
                        sLn = sLn + "P" + sDelimitador;

                        //16 - Observação para a nota
                        sLn = sLn + sDelimitador;

                        //17 - Aliquota de ISS
                        if (oNotasFiscais.TipoDocumento == 8)
                            sLn = sLn + oNotasFiscais.PercentualISS.ToString("N2") + sDelimitador;
                        else
                            sLn = sLn + "0,00" + sDelimitador;

                        // 18 - Valor Contábil do CFOP
                        sLn = sLn + ValorTotal.ToString("N2") + sDelimitador;

                        // 19 - Base de cálculo do iss
                        if (oNotasFiscais.TipoDocumento == 8)
                            sLn = sLn + (ValorTotal - oNotasFiscais.ValorDescontoIncondicionado).ToString("N2") + sDelimitador;
                        else
                            sLn = sLn + 0.ToString("N2") + sDelimitador;

                        // 20 - Valor do iss normal
                        if (oNotasFiscais.TipoDocumento == 8)
                            sLn = sLn + ValorISS.ToString("N2") + sDelimitador;
                        else
                            sLn = sLn + 0.ToString("N2") + sDelimitador;

                        // 21 -Valor de isentas de icms/iss
                        sLn = sLn + 0.ToString("N2") + sDelimitador;

                        // 22 - Valor de outros de icms/iss
                        sLn = sLn + 0.ToString("N2") + sDelimitador;

                        // 23 - Base Calculo IR e 24 Valor do IR
                        if (o_RetencoesNF.ValorLimiteIR <= (ValorTotal - oNotasFiscais.ValorDescontoIncondicionado))
                        {
                            sLn = sLn + (ValorTotal - oNotasFiscais.ValorDescontoIncondicionado).ToString("N2") + sDelimitador;
                            sLn = sLn + ValorIRRF.ToString("N2") + sDelimitador;
                        }
                        else
                        {
                            sLn = sLn + 0.ToString("N2") + sDelimitador;
                            sLn = sLn + 0.ToString("N2") + sDelimitador;
                        }

                        // 25 - Valor do ISSQN
                        if (oNotasFiscais.TipoDocumento == 8)
                            sLn = sLn + ValorISS.ToString("N2") + sDelimitador;
                        else
                            sLn = sLn + 0.ToString("N2") + sDelimitador;

                        // 26 - Valor do ISS retido // NF = 8 e tem que ser pessoa Jurídica
                        if (oNotasFiscais.TipoDocumento == 8 && oCliente.CodigoBROOKS_Retencoes == "G" && oCliente.Pessoa == 2 && oEndereco.CodigoMunicipio == 83275)
                            sLn = sLn + 0.ToString("N2") + sDelimitador;
                        else if (oNotasFiscais.TipoDocumento == 8 && oCliente.Pessoa < 2)
                            sLn = sLn + ValorISS.ToString("N2") + sDelimitador;
                        else
                            sLn = sLn + 0.ToString("N2") + sDelimitador;

                        // 27 - Base Calc ISS Retido // quando NF = 8 e tem que ser pessoa Jurídica
                        if (oNotasFiscais.TipoDocumento == 8 && oCliente.CodigoBROOKS_Retencoes == "G" && oEndereco.CodigoMunicipio == 83275)
                            sLn = sLn + 0.ToString("N2") + sDelimitador;
                        else if (oNotasFiscais.TipoDocumento == 8 && oCliente.Pessoa < 2)
                            sLn = sLn + (ValorTotal - oNotasFiscais.ValorDescontoIncondicionado).ToString("N2") + sDelimitador;
                        else
                            sLn = sLn + 0.ToString("N2") + sDelimitador;

                        // 28 -  Descrição dos dados adicionais
                        if (ValorINSS == 0)
                            sLn = sLn + "Isenção INSS (Art. 118, inciso V, da IN/RFB nº 971/2009)" + sDelimitador;
                        else
                            sLn = sLn + sDelimitador;

                        // 29 - Autenticidade da Nota
                        sLn = sLn + "0000000000" + sDelimitador;

                        // 30 - Base INSS
                        sLn = sLn + oNotasFiscais.ValorBaseINSS.ToString("N2") + sDelimitador;

                        // 31 - Valor INSS
                        sLn = sLn + oNotasFiscais.ValorINSS.ToString("N2") + sDelimitador;

                        // 32 - Valor Desconto
                        sLn = sLn + oNotasFiscais.ValorDescontoIncondicionado.ToString("N2");

                        // nova linha
                        sLn = sLn + Environment.NewLine;

                        // ITENS DA NOTA - linha tipo 3
                        int iOrd = 0;
                        i = 0;
                        decimal acumulaValor = 0;

                        while (i <= 13)
                        {
                            string sDesc = "";
                            string ImprimeLinha = "";

                            clsCorpoNotasFiscais oCorpoNF = new clsCorpoNotasFiscais();
                            clsCorpoNotasFiscaisDados oCorpoNFDados = new clsCorpoNotasFiscaisDados();
                            oCorpoNFDados.PegaDados(oCorpoNF, oNotasFiscais.NumeroNotaFiscal, i + 1);
                            
                            // Tipo de linha
                            ImprimeLinha = ImprimeLinha + "3" + sDelimitador;

                            // 2 - Código/Classificação do produto
                            if (geral.IsNumeric(oCorpoNF.Descricao) && oCorpoNF.Descricao != "")
                                ImprimeLinha = ImprimeLinha + oCorpoNF.Descricao + sDelimitador; // essa descrição é código diferente de 709
                            else
                                ImprimeLinha = ImprimeLinha + "709" + sDelimitador;

                            int j = i;
                            // j - aponta para o primeiro item com quantidade
                            while (j <= 13)
                            {
                                oCorpoNFDados.PegaDados(oCorpoNF, oNotasFiscais.NumeroNotaFiscal, j);
                                if (oCorpoNF.Quantidade > 0)
                                    break;
                                j = j + 1;
                            }
                            // 3 - Quantidade comercializada
                            ImprimeLinha = ImprimeLinha + oCorpoNF.Quantidade.ToString("N3") + sDelimitador;

                            // 4 - Valor total do produto / serviço
                            ImprimeLinha = ImprimeLinha + Math.Round(oCorpoNF.PrecoUnitario * oCorpoNF.Quantidade, 2).ToString("N2") + sDelimitador;

                            // 5 - Situação Tributária CST/CSOSN - ISS
                            ImprimeLinha = ImprimeLinha + o_RetencoesNF.CodigoSituacaoTributaria + sDelimitador; // ou só 0

                            // 6 - Número de ordem do item
                            iOrd = iOrd + 1;
                            ImprimeLinha = ImprimeLinha + iOrd.ToString("00") + sDelimitador;

                            // cálculo para 10 e 11
                            sDesc = "";
                            oCorpoNF = new clsCorpoNotasFiscais();
                            oCorpoNFDados.PegaDados(oCorpoNF, oNotasFiscais.NumeroNotaFiscal, i);
                            if (oCorpoNF.Quantidade == 0)
                            {
                                int k = i;
                                while (k <= 13 && oCorpoNF.Valor == 0)
                                {
                                    oCorpoNF = new clsCorpoNotasFiscais();
                                    oCorpoNFDados.PegaDados(oCorpoNF, oNotasFiscais.NumeroNotaFiscal, k);
                                    if (oCorpoNF.Descricao != null)
                                    {
                                        if (oCorpoNF.Descricao.Length > 0)
                                        {
                                            sDesc = sDesc + oCorpoNF.Descricao + " ";
                                            i++;
                                        }
                                        k++;
                                    }
                                }
                            }
                            else
                                sDesc = sDesc + oCorpoNF.Descricao;

                            sDesc = sDesc.Trim();
                            decimal vlBaseCalculo = Math.Round(oCorpoNF.PrecoUnitario * oCorpoNF.Quantidade, 2);
                            decimal percRatiado = 0;
                            if (sDesc.Length > 0 && oNotasFiscais.ValorDescontoIncondicionado > 0)
                            {
                                percRatiado = (ValorTotal - oNotasFiscais.ValorDescontoIncondicionado) * 100 / ValorTotal;
                                vlBaseCalculo = Math.Round(oCorpoNF.Valor * percRatiado / 100, 2);
                                acumulaValor = acumulaValor + vlBaseCalculo;
                                if (acumulaValor > (oNotasFiscais.ValorTotal - oNotasFiscais.ValorDescontoIncondicionado))
                                    vlBaseCalculo = vlBaseCalculo - 0.00m;
                                else if (acumulaValor < (oNotasFiscais.ValorTotal - -oNotasFiscais.ValorDescontoIncondicionado))
                                    vlBaseCalculo = vlBaseCalculo + 0.00m;
                            }

                            // 7 - Base de Cálculo ISS/ICMS do produto/serviço
                            if (oNotasFiscais.TipoDocumento == 8)
                                ImprimeLinha = ImprimeLinha + vlBaseCalculo.ToString("N2").Replace(".", "") + sDelimitador;
                            else
                                ImprimeLinha = ImprimeLinha + "0" + sDelimitador;

                            // 8 - Alíquota de ICMS/ISS do produto/serviço
                            if (oNotasFiscais.TipoDocumento == 8)
                                ImprimeLinha = ImprimeLinha + oNotasFiscais.PercentualISS.ToString("N2") + sDelimitador;
                            else
                                ImprimeLinha = ImprimeLinha + "0,00" + sDelimitador;

                            // 9 - Valor do desconto
                            ImprimeLinha = ImprimeLinha + "0" + sDelimitador;

                            // 10 - Valor do ISS/ICMS
                            ImprimeLinha = ImprimeLinha + Math.Round(vlBaseCalculo * oNotasFiscais.PercentualISS / 100, 2).ToString("N2") + sDelimitador;

                            // 11 - Descrição Complementar
                            ImprimeLinha = ImprimeLinha + sDesc + sDelimitador;
                            sLn = sLn + ImprimeLinha;

                            // 12 - Situação Tributária Pis
                            sLn = sLn + "1" + sDelimitador;

                            // 13 - Situação Tributária Cofins
                            sLn = sLn + "1" + sDelimitador;

                            // 14 - Alíquota do PIS
                            sLn = sLn + "0,65" + sDelimitador;

                            // 15 - Base de cálculo do PIS
                            sLn = sLn + vlBaseCalculo.ToString("N2") + sDelimitador;

                            // 16 - Valor do PIS
                            sLn = sLn + Math.Round(vlBaseCalculo * 0.65m / 100, 2).ToString("N2") + sDelimitador;

                            // 17 - Alíquota do COFINS
                            sLn = sLn + 3.ToString("N2") + sDelimitador;

                            // 18 - Base de cálculo do COFINS
                            sLn = sLn + vlBaseCalculo.ToString("N2") + sDelimitador;

                            // 19 - Valor do COFINS
                            sLn = sLn + Math.Round(vlBaseCalculo * 3 / 100, 2).ToString("N2");

                            // nova linha
                            sLn = sLn + Environment.NewLine;

                            if (i > 5)
                            {
                                if ((oCorpoNF.Descricao == "" || oCorpoNF.Descricao == null))
                                {
                                    i = 14;
                                    break;
                                }
                            }
                            i++;
                        }

                        // Parcelas Financeiro
                        DataVendimento = "";
                        DataVencimentoParcela2 = "";
                        if (oNotasFiscais.NumeroParcelas == 0)
                            oNotasFiscais.NumeroParcelas = 1;
                        for (i = 1; i <= oNotasFiscais.NumeroParcelas; i++)
                        {

                            // 1 - Tipo de linha
                            sLn = sLn + "4" + sDelimitador;

                            // 2 - Número da parcela
                            sLn = sLn + i.ToString() + sDelimitador;

                            // 3 - Número do título
                            if (oNotasFiscais.NumeroParcelas == 1)
                                sLn = sLn + oNotasFiscais.NumeroNF + "/" + Convert.ToDateTime(oNotasFiscais.DataEmissao).ToString("yy") + sDelimitador;    // número fatura
                            else
                                sLn = sLn + oNotasFiscais.NumeroNF + "/" + i.ToString() + "/" + Convert.ToDateTime(oNotasFiscais.DataEmissao).ToString("yy") + sDelimitador;    // número fatura e parcela

                            // 4 - Data de vencimento
                            DataVencimentoParcela2 = Convert.ToDateTime(oNotasFiscais.Vencimento).AddDays(oNotasFiscais.DiasEntreVctos).ToString("dd/MM/yy");
                            if (i == 2)
                                DataVendimento = Convert.ToDateTime(DataVencimentoParcela2).ToString("dd/MM/yy");
                            else if (i == 3)
                                DataVendimento = Convert.ToDateTime(DataVencimentoParcela2).AddDays(oNotasFiscais.DiasEntreVctos).ToString("dd/MM/yy");
                            else if (oNotasFiscais.NumeroParcelas == 1)
                                DataVendimento = Convert.ToDateTime(oNotasFiscais.Vencimento).ToString("dd/MM/yy");
                            else if (i == 1)
                                DataVendimento = Convert.ToDateTime(oNotasFiscais.Vencimento).ToString("dd/MM/yy");
                            sLn = sLn + DataVendimento + sDelimitador; // data vencimento

                            // 5 - Valor da parcela
                            if (oNotasFiscais.NumeroParcelas == 1)
                                sLn = sLn + ValorLiquido.ToString("N2") + sDelimitador; // valor liquido
                            else
                                sLn = sLn + (ValorLiquido / oNotasFiscais.NumeroParcelas).ToString("N2") + sDelimitador;  // valor da parcela

                            // 6 - Código Histórico
                            sLn = sLn + "1191" + sDelimitador;

                            // 7 - Comp Histórico
                            sLn = sLn + oNotasFiscais.Historico + sDelimitador;

                            // 8 - Competência
                            sLn = sLn + Convert.ToDateTime(oNotasFiscais.DataReferencia).ToString("MM/yyyy") + sDelimitador;

                            // 9 - Forma de pagamento
                            sLn = sLn + oCliente.CodigoTipoCobranca.ToString("00");

                            // nova linha
                            sLn = sLn + Environment.NewLine;

                            // Importação dos Classificação empresarial
                            // 1 - Tipo de linha = 8
                            sLn = sLn + "8" + sDelimitador;

                            // 2 - Conta Contábil
                            sLn = sLn + oCliente.Classificacao.ToString() + sDelimitador;

                            //    // 3 - Valor
                            if (oNotasFiscais.NumeroParcelas == 1)
                                sLn = sLn + ValorLiquido.ToString("N2"); // valor titulo
                            else
                                sLn = sLn + (ValorLiquido / oNotasFiscais.NumeroParcelas).ToString("N2");  // valor da parcela

                            // nova linha
                            sLn = sLn + Environment.NewLine;

                            // Importação das Contas Gerenciais Associadas a Classificação
                            // 1 - Tipo de linha = 9
                            sLn = sLn + "9" + sDelimitador;

                            // 2 - Conta Contábil
                            sLn = sLn + oCliente.Classificacao.ToString() + sDelimitador;

                            // 3 - Conta Gerencial
                            sLn = sLn + oCliente.ContaGerencial.ToString() + sDelimitador;  //não sei

                            // 4 - Valor
                            if (oNotasFiscais.NumeroParcelas == 1)
                                sLn = sLn + ValorLiquido.ToString("N2"); // valor titulo
                            else
                                sLn = sLn + (ValorLiquido / oNotasFiscais.NumeroParcelas).ToString("N2");  // valor da parcela

                            // nova linha
                            sLn = sLn + Environment.NewLine;

                        }

                        //inclui linha 6
                        sLn = sLn + "6" + sDelimitador + "C" + sDelimitador + oCliente.ContaGerencial.ToString() + sDelimitador + ValorTotal.ToString("N2");

                        // nova linha
                        sLn = sLn + Environment.NewLine;


                        // RETENÇÕES (PCC)
                        // 1 - Tipo de linha
                        sLn = sLn + "14" + sDelimitador;

                        // 2 - Cod Retenção
                        sLn = sLn + "1708" + sDelimitador;

                        // 3 - Base Calculo
                        sLn = sLn + (ValorTotal - oNotasFiscais.ValorDescontoIncondicionado).ToString("N2") + sDelimitador;

                        // 4 - Valor Retenção
                        sLn = sLn + ValorIRRF.ToString("N2") + sDelimitador;

                        // 5 - Sigla imposto
                        sLn = sLn + "IRRF";

                        // nova linha
                        sLn = sLn + Environment.NewLine;

                        // RETENÇÕES (PCC)
                        // 1 - Tipo de linha
                        sLn = sLn + "14" + sDelimitador;

                        // 2 - Cod Retenção
                        sLn = sLn + "5960" + sDelimitador;

                        // 3 - Base Calculo
                        sLn = sLn + (ValorTotal - oNotasFiscais.ValorDescontoIncondicionado).ToString("N2") + sDelimitador;

                        // 4 - Valor Retenção
                        sLn = sLn + ValorCOFINS.ToString("N2") + sDelimitador;

                        // 5 - Sigla imposto
                        sLn = sLn + "CFSR";

                        // nova linha
                        sLn = sLn + Environment.NewLine;

                        // RETENÇÕES (PCC)
                        // 1 - Tipo de linha
                        sLn = sLn + "14" + sDelimitador;

                        // 2 - Cod Retenção
                        sLn = sLn + "5979" + sDelimitador;

                        // 3 - Base Calculo
                        sLn = sLn + (ValorTotal - oNotasFiscais.ValorDescontoIncondicionado).ToString("N2") + sDelimitador;

                        // 4 - Valor Retenção
                        sLn = sLn + ValorPIS.ToString("N2") + sDelimitador;

                        // 5 - Sigla imposto
                        sLn = sLn + "PISR";

                        // nova linha
                        sLn = sLn + Environment.NewLine;

                        // RETENÇÕES (PCC)
                        // 1 - Tipo de linha
                        sLn = sLn + "14" + sDelimitador;

                        // 2 - Cod Retenção //5987 Contribuição Social
                        sLn = sLn + "5987" + sDelimitador;

                        // 3 - Base Calculo
                        sLn = sLn + (ValorTotal - oNotasFiscais.ValorDescontoIncondicionado).ToString("N2") + sDelimitador;

                        // 4 - Valor Retenção
                        sLn = sLn + ValorContrSocial.ToString("N2") + sDelimitador;

                        // 5 - Sigla imposto
                        sLn = sLn + "CSRF";

                        x.WriteLine(sLn);

                        //fechando o arquivo texto 
                        x.Close();

                        oExportacaoRadarDados.SalvarComoGerado(pSequencial);
                    }
                }
            }
        }
        private void CalcularImpostos(clsAliquotaImpostos oRetencao, string pCodigoCliente, string pDataEmissao, clsNotasFiscais pNotasFiscais)
        {
            clsCorpoNotasFiscais oCorpoNF = new clsCorpoNotasFiscais();
            clsCorpoNotasFiscaisDados oCorpoNFDados = new clsCorpoNotasFiscaisDados();
            oCorpoNFDados.PegaDados(oCorpoNF, pNotasFiscais.NumeroNotaFiscal, 0);

            ValorIRRF = 0;
            ValorPIS = 0;
            ValorCOFINS = 0;
            ValorContrSocial = 0;
            ValorCRF = 0;
            ValorINSS = 0;
            ValorISS = 0;
            bool Eh709ou710 = false;
            if (oCorpoNF.Descricao == null)
                oCorpoNF.Descricao = " ";
            if (oCorpoNF.Descricao.IndexOf("709") > -1) // encontrou 709 - descontar Impostos federais
            {
                Eh709ou710 = true;
            }
            if (oCorpoNF.Descricao.IndexOf("710") > -1) // encontrou 710 - descontar Impostos federais
            {
                Eh709ou710 = true;
            }
            if (Eh709ou710 == false)
            {
                oRetencao.AliquotaCOFINS_Retido = 0;
                oRetencao.AliquotaContribSocial = 0;
                oRetencao.AliquotaIR_Retido = 0;
                oRetencao.AliquotaPIS_Retido = 0;
                oRetencao.ValorLimiteCRF = 0;
                oRetencao.ValorLimiteIR = 0;
            }
            if (pCodigoCliente != "")
            {
                oCliente = oClienteDados.PegaDados(oCliente, Convert.ToInt32(pCodigoCliente));
                decimal ValorTotalMesCRF = 0;
                if (oCliente.CNPJ_Faturamento != "")
                    ValorTotalMesCRF = oNotasFiscaisDados.PegaValorTotalMesParaCalcularCRF(Convert.ToInt32(Convert.ToDateTime(pDataEmissao).ToString("MM")),
                                                                                           Convert.ToInt32(Convert.ToDateTime(pDataEmissao).ToString("yyyy")),
                                                                                           geral.RetiraLetras(oCliente.CNPJ_Faturamento));
                else
                    ValorTotalMesCRF = oNotasFiscaisDados.PegaValorTotalMesParaCalcularCRF(Convert.ToInt32(Convert.ToDateTime(pDataEmissao).ToString("MM")),
                                                                                           Convert.ToInt32(Convert.ToDateTime(pDataEmissao).ToString("yyyy")),
                                                                                           geral.RetiraLetras(oCliente.CNPJ_CPF));

                if (geral.Left(oRetencao.CodigoBROOKS, 1) != "A" || geral.RetiraLetras(geral.RetiraCharsCNPJCPF(oCliente.CNPJ_CPF)).Length > 11)
                {
                    ValorISS = Convert.ToDecimal(Math.Round(ValorTotal * pNotasFiscais.PercentualISS / 100, 2).ToString("N2"));
                }
                
                if (ValorTotal >= oRetencao.ValorLimiteIR && oRetencao.ValorLimiteIR > 0)
                    ValorIRRF = Math.Round(ValorTotal * oRetencao.AliquotaIR_Retido / 100, 2);

                if ((ValorTotal >= oRetencao.ValorLimiteCRF && oRetencao.ValorLimiteCRF > 0) ||
                    (ValorTotalMesCRF > 0 && (ValorTotal + ValorTotalMesCRF) >= oRetencao.ValorLimiteCRF) || 
                    oCorpoNF.Descricao.IndexOf("709") > -1 || oCorpoNF.Descricao.IndexOf("710") > -1)
                {
                    if (ValorTotalMesCRF > 0 && ValorTotalMesCRF >= oRetencao.ValorLimiteCRF)
                        ValorTotalMesCRF = 0;

                    if (ValorTotal + ValorTotalMesCRF < oRetencao.ValorLimiteCRF)
                        ValorTotalMesCRF = 0;

                    ValorPIS = Math.Round(ValorTotal * oRetencao.AliquotaPIS_Retido / 100, 2);
                    ValorCOFINS = Math.Round(ValorTotal * oRetencao.AliquotaCOFINS_Retido / 100, 2);
                    ValorContrSocial = Math.Round(ValorTotal * oRetencao.AliquotaContribSocial / 100, 2);
                    ValorCRF = ValorPIS + ValorCOFINS + ValorContrSocial;
                }
                else
                {
                    oRetencao.AliquotaCOFINS_Retido = 0;
                    oRetencao.AliquotaContribSocial = 0;
                    oRetencao.AliquotaPIS_Retido = 0;
                }
                ValorINSS = 0;
                if (pNotasFiscais.ValorBaseINSS > 0 && pNotasFiscais.PercentualINSS > 0)
                    ValorINSS = Math.Round(pNotasFiscais.ValorBaseINSS * pNotasFiscais.PercentualINSS / 100, 2);
                decimal PercentualCRF = oRetencao.AliquotaPIS_Retido + oRetencao.AliquotaCOFINS_Retido + oRetencao.AliquotaContribSocial;
            }
        }

        // Sempre que inserido Recibo
        private void SalvaArquivoReciboFaturaImportacaoRadar(int pNumeroReciboNotaFiscal, int pSequencial)
        {
            //C:\\BROOKS\\formSILC\\bin\\SERVIDOR
            string _s = "";
            string pathExportacao = "";
            if (Directory.Exists("\\\\SERVIDOR\\WinSILC\\ImportacaoWebSILC\\Radar"))
            {
                pathExportacao = "\\\\SERVIDOR\\WinSILC\\ImportacaoWebSILC\\Radar";
            }
            if (!Directory.Exists("\\\\SERVIDOR\\WinSILC\\ImportacaoWebSILC\\Radar"))
            {
                _s = _s = "Importação Automática do RADAR não está configurada! \n";
                _s = _s + "Recibo não foi Importada para o Radar! \n";
                _s = _s + "Por falta da Pasta e arquivo em: Radar" + "\n";
                _s = _s + "Informe urgentemente a TI para resolver o problema!";
                StreamWriter sw = new StreamWriter("Erro_Importacao_radar_Recibo.txt");
                sw.WriteLine(_s);
                sw.Close();
                sw.Dispose();
            }
            if (pathExportacao != "")
            {
                string ArquivoExportacao = pathExportacao + "\\ContasReceber.txt";
                int i = 1;
                while (true)
                {
                    if (!File.Exists(ArquivoExportacao))
                        break;
                    else
                        ArquivoExportacao = pathExportacao + "\\ContasReceber" + i + ".txt";
                    i++;
                    if (i > 10000)
                        break;
                }

                if (pNumeroReciboNotaFiscal > 0)
                {
                    clsNotasFiscais oNotasFiscais = new clsNotasFiscais();
                    clsNotasFiscaisDados oNotasFiscaisDados = new clsNotasFiscaisDados();
                    oNotasFiscaisDados.PegaDados(oNotasFiscais, pNumeroReciboNotaFiscal);
                    if (oNotasFiscais.NumeroNotaFiscal > 0)
                    {
                        oCliente = new clsClientes();
                        oClienteDados = new clsClienteDados();
                        oClienteDados.PegaDados(oCliente, oNotasFiscais.CodigoCliente);

                        // gera arquivo para importação
                        StreamWriter x = File.CreateText(ArquivoExportacao);

                        string sLn = "";
                        string sDelimitador = ";";

                        for (int nParcs = 1; nParcs <= Convert.ToInt16(oNotasFiscais.NumeroParcelas); nParcs++)
                        {
                            // Tipo de Linha
                            if (oCliente.CNPJ_Faturamento != "")
                                sLn = sLn + "T" + geral.RetiraCharsCNPJCPF(oCliente.CNPJ_Faturamento) + ";";
                            else
                                sLn = sLn + "T" + geral.RetiraCharsCNPJCPF(oCliente.CNPJ_CPF) + ";";

                            sLn = sLn + Convert.ToDateTime(oNotasFiscais.DataEmissao).ToString("dd/MM/yy") + ";";

                            if (nParcs == 1 || oNotasFiscais.NumeroParcelas == 1)
                                sLn = sLn + Convert.ToDateTime(oNotasFiscais.Vencimento).ToString("dd/MM/yy") + ";";
                            else
                                sLn = sLn + Convert.ToDateTime(oNotasFiscais.Vencimento).AddDays(oNotasFiscais.DiasEntreVctos * (nParcs-1)).ToString("dd/MM/yy") + ";";

                            // 2 - Filial
                            if (oNotasFiscais.TipoDocumento == 4) // recibo
                                sLn = sLn + "2" + sDelimitador;
                            else
                                sLn = sLn + "1" + sDelimitador;

                            if (oNotasFiscais.TipoDocumento == 4)
                                sLn = sLn + "4S" + oNotasFiscais.NumeroNF + ";";  // número documento 4 - recibo
                            else
                            {
                                if (oNotasFiscais.NumeroParcelas == 1)
                                    sLn = sLn + "5S" + oNotasFiscais.NumeroNF + ";";  // número documento 5 - fatura
                                else
                                    sLn = sLn + "5S" + oNotasFiscais.NumeroNF + "/" + nParcs.ToString() + ";";  // número documento 5 - fatura
                            }
                            sLn = sLn + oNotasFiscais.TipoDocumento + ";"; //' tipo documento
                            decimal vParcelaArredondada = 0;
                            vParcelaArredondada = oNotasFiscais.ValorTotal / oNotasFiscais.NumeroParcelas;
                            decimal vTotalCalculado = 0;
                            for (int xv = 1; xv <= oNotasFiscais.NumeroParcelas; xv++)
                            {
                                vTotalCalculado = vTotalCalculado + vParcelaArredondada;
                            }
                            if (vTotalCalculado < oNotasFiscais.ValorTotal && nParcs == 1)
                            {
                                vParcelaArredondada = vParcelaArredondada + Convert.ToDecimal(0.01);
                            }

                            decimal valorLiquido = oNotasFiscais.ValorTotal - oNotasFiscais.ValorDescontoIncondicionado;
                            if (oNotasFiscais.NumeroParcelas == 1)
                                sLn = sLn + valorLiquido.ToString("N2") + ";";
                            else
                                sLn = sLn + vParcelaArredondada.ToString("N2") + ";";

                            sLn = sLn + Convert.ToDateTime(oNotasFiscais.DataEmissao).ToString("dd/MM/yy") + ";"; // data saída
                            sLn = sLn + "\"" + oNotasFiscais.Historico + "\";"; // complemento histórico

                            if (oNotasFiscais.NumeroParcelas == 1)
                                sLn = sLn + oNotasFiscais.NumeroNF + "/" + Convert.ToDateTime(oNotasFiscais.DataEmissao).ToString("yy") + ";"; // número fatura
                            else
                                sLn = sLn + oNotasFiscais.NumeroNF + "/" + nParcs.ToString() + "/" + Convert.ToDateTime(oNotasFiscais.DataEmissao).ToString("yy") + ";"; // número fatura

                            if (oNotasFiscais.NumeroParcelas == 1)
                                sLn = sLn + valorLiquido.ToString("N2") + ";";
                            else
                                sLn = sLn + vParcelaArredondada.ToString("N2") + ";";
                            sLn = sLn + oNotasFiscais.Classificacao + ";";
                            sLn = sLn + "2;";
                            sLn = sLn + oNotasFiscais.ContaGerencial + ";";
                            sLn = sLn + oCliente.CodigoTipoCobranca.ToString("00") + ";" + Environment.NewLine;

                            if (oNotasFiscais.TipoDocumento == 5) // fatura
                            {
                                sLn = sLn + "O";
                                sLn = sLn + oNotasFiscais.NumeroNF + "/" + nParcs.ToString() + "/" + Convert.ToDateTime(oNotasFiscais.DataEmissao).ToString("yy") + ";"; // número fatura
                                sLn = sLn + oNotasFiscais.DataEmissao + ";";
                                sLn = sLn + vParcelaArredondada.ToString("N2") + ";";
                                if (oNotasFiscais.NumeroParcelas == 1)
                                    sLn = sLn + valorLiquido.ToString("N2") + ";";
                                else
                                    sLn = sLn + vParcelaArredondada.ToString("N2") + ";";
                                sLn = sLn + 0.ToString("N2") + ";";
                                sLn = sLn + 0.ToString("N2") + ";";
                                sLn = sLn + 0.ToString("N2") + ";";
                                sLn = sLn + 0.ToString("N2") + ";";
                                sLn = sLn + vParcelaArredondada.ToString("N2") + ";";
                                sLn = sLn + vParcelaArredondada.ToString("N2") + ";";
                                sLn = sLn + 0.ToString("N2") + ";" + Environment.NewLine;
                            }
                        }
                        sLn = sLn + "I" + geral.Left(oCliente.NomeFantasia, 10) + ";";
                        sLn = sLn + "1.001.005" + ";";
                        sLn = sLn + Convert.ToDateTime(oNotasFiscais.DataReferencia).ToString("MM/yyyy");

                        // escreve
                        x.WriteLine(sLn);

                        // salva e fecha o arquivo texto 
                        x.Close();

                        oExportacaoRadarDados.SalvarComoGerado(pSequencial);
                    }
                }
            }
        }
    }
}