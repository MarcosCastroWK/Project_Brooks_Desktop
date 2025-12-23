using System;
using System.Configuration;
using System.Windows.Forms;
using System.Xml.Linq;
using ConfigurationSilc;
using ConfigurationSilc.Dtos;
using formSILC.Properties;
using LibSILC;

namespace formSILC
{
    public partial class frmMenu : Form
    {
        clsUsuarioDados oUsuarioDados = new clsUsuarioDados();
        clsProgramacaoFechadaDados oProgramacaoFechadaDados = new clsProgramacaoFechadaDados();
        private string _bloqPor;
        private SilcConfigurationManager _silcConfig;
        private UrlConfigs _urlConfigs;

        public frmMenu()
        {
            InitializeComponent();

            _silcConfig = new SilcConfigurationManager();
            _urlConfigs = _silcConfig.GetConfiguration<UrlConfigs>();

        }
        private void frmMenu_Load(object sender, EventArgs e)
        {


            toolStripMenuItem2.Enabled = false;
            municípiosToolStripMenuItem.Enabled = false;
            relatorioDaMTReFATMAParaConferencia.Enabled = false;
            //frmInicio frm = new frmInicio();
            //frm.ShowDialog();

            //geral.AcessoPrincipal = true;
            //geral.AcessoPermitido = true;
            //geral.CodigoEmpresa = 1;

            // geral.BancoUsado = 1;

            //Banco de dados = 4; // fastcompost
            //Banco de dados = 3; // banco localhost home office
            //Banco de dados = 2; // banco de dados 2 produção  teste
            //Banco de dados = 1; // banco de dados 1 produção
            if (geral.AcessoPermitido)
            {
                //if (geral.BancoUsado == 3) 
                //    Program.uri = new System.Uri("http://localhost:52274/forms/brooks/loginaplicativo.aspx");

                //if (geral.BancoUsado == 1 || geral.BancoUsado == 2)
                //    Program.uri = new System.Uri("http://servidor/forms/brooks/loginaplicativo.aspx");
                //if (geral.BancoUsado == 4)
                //    Program.uri = new System.Uri("http://servidor/forms/fastcompost/login.aspx");
                //webBrowser1.Url = Program.uri;

                
                Program.uri = new System.Uri(_urlConfigs.LoginAplicativo);
                webBrowser1.Url = Program.uri;
                webBrowser1.Refresh();
            }
            else
                Application.Exit();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // excluir usuário logado.
            Close();
        }
        private void ControleMenuItem(bool pHabilitar)
        {
            if (geral.UsuarioAtual.ToUpper() == "TEIXEIRA")
            {
                toolStripMenuItem2.Enabled = true;
                municípiosToolStripMenuItem.Enabled = true;
                relatorioDaMTReFATMAParaConferencia.Enabled = true;
            }
        }
        private void programaçãoDiáriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (geral.UsuarioAtual == "" || geral.UsuarioAtual == null)
            {
                MessageBox.Show("Usuário inválido!", "SILC");
            }
            else
            {
                bool _existeUsuario = false;
                using (clsUsuarioDados oUsDados = new clsUsuarioDados())
                {
                    _existeUsuario = oUsDados.ExisteUsuario(geral.UsuarioAtual, geral.CodigoEmpresa);
                }
                if (!_existeUsuario)
                {
                    MessageBox.Show("Usuário inválido!", "SILC");
                }
                else
                {
                    ControleMenuItem(true);
                    _bloqPor = oProgramacaoFechadaDados.UltimoRegistroNomeBloqueado();
                    if (_bloqPor.Length > 0)
                    {
                        if (_bloqPor == geral.UsuarioAtual)
                        {
                            MessageBox.Show("Você abriu a tela de programação diária em outra instância!", "SILC");
                        }
                        else if (_bloqPor == "0")
                        {
                            MessageBox.Show("Usuário inválido! Libere a programação.", "SILC");
                        }
                        else
                        {
                            frmProgramacaoDiaria frm = new frmProgramacaoDiaria();
                            frm.ShowDialog();
                            frm.Close();
                            frm.Dispose();
                        }
                    }
                    else
                    {
                        frmProgramacaoDiaria frm = new frmProgramacaoDiaria();
                        frm.ShowDialog();
                        frm.Close();
                        frm.Dispose();
                    }
                }
            }
        }

        private void locaçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void notasFiscaisToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            if (geral.UsuarioAtual == "" || geral.UsuarioAtual == null)
            {
                MessageBox.Show("Usuário inválido!", "SILC");
            }
            else
            {
                if (!oUsuarioDados.ExisteUsuario(geral.UsuarioAtual, geral.CodigoEmpresa))
                {
                    MessageBox.Show("Usuário inválido!", "SILC");
                }
                else
                {
                    ControleMenuItem(true);
                    frmCDFeImportacao frm = new frmCDFeImportacao();
                    frm.Show();
                }
            }
        }

        private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            if (geral.UsuarioAtual == "" || geral.UsuarioAtual == null)
            {
                MessageBox.Show("Usuário inválido!", "SILC");
            }
            else
            {
                if (!oUsuarioDados.ExisteUsuario(geral.UsuarioAtual, geral.CodigoEmpresa))
                {
                    MessageBox.Show("Usuário inválido!", "SILC");
                }
                else
                {
                    ControleMenuItem(true);
                    frmSistemaFatma frm = new frmSistemaFatma();
                    frm.Show();
                }
            }
        }

        private void municípiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (geral.UsuarioAtual == "" || geral.UsuarioAtual == null)
            {
                MessageBox.Show("Usuário inválido!", "SILC");
            }
            else
            {
                if (!oUsuarioDados.ExisteUsuario(geral.UsuarioAtual, geral.CodigoEmpresa))
                {
                    MessageBox.Show("Usuário inválido!", "SILC");
                }
                else
                {
                    ControleMenuItem(true);
                    frmImportacaoMunicipios frm = new frmImportacaoMunicipios();
                    frm.Show();
                }
            }
        }

        private void mnAtualizacaoDados_Click(object sender, EventArgs e)
        {
            if (geral.UsuarioAtual == "" || geral.UsuarioAtual == null)
            {
                MessageBox.Show("Usuário inválido!", "SILC");
            }
            else
            {
                if (!oUsuarioDados.ExisteUsuario(geral.UsuarioAtual, geral.CodigoEmpresa))
                {
                    MessageBox.Show("Usuário inválido!", "SILC");
                }
                else
                {
                    ControleMenuItem(true);
                    frmAtualizacaoDados o_frmAtualizacaoDados = new frmAtualizacaoDados();
                    o_frmAtualizacaoDados.ShowDialog();
                }
            }
        }

        private void mnDocumentacaoAplicavel_Click(object sender, EventArgs e)
        {
            if (geral.UsuarioAtual == "" || geral.UsuarioAtual == null)
            {
                MessageBox.Show("Usuário inválido!", "SILC");
            }
            else
            {
                if (!oUsuarioDados.ExisteUsuario(geral.UsuarioAtual, geral.CodigoEmpresa))
                {
                    MessageBox.Show("Usuário inválido!", "SILC");
                }
                else
                {
                    ControleMenuItem(true);
                    frmDocumentacaoAplicavel frm = new frmDocumentacaoAplicavel();
                    frm.ShowDialog();
                }
            }
        }

        private void relatórioDaMTReFATMAParaConferênciaDiároaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (geral.UsuarioAtual == "" || geral.UsuarioAtual == null)
            {
                MessageBox.Show("Usuário inválido!", "SILC");
            }
            else
            {
                if (!oUsuarioDados.ExisteUsuario(geral.UsuarioAtual, geral.CodigoEmpresa))
                {
                    MessageBox.Show("Usuário inválido!", "SILC");
                }
                else
                {
                    ControleMenuItem(true);
                    frmRelatorioMTReConferenciaDiaria frm = new frmRelatorioMTReConferenciaDiaria();
                    frm.ShowDialog();
                }
            }
        }

        private void AbrirAba(string pAba, string pParamentro = "")
        {
            webBrowser1.Stop();
            if (Program.uri.Host == "localhost")
            {

                Program.uri = new System.Uri("http://localhost/webSilc/forms/" + pAba + ".aspx" + pParamentro);
                //Program.uri = new System.Uri("http://localhost:52274/forms/" + pAba + ".aspx" + pParamentro);
            }
            else
                Program.uri = new System.Uri("http://" + Program.uri.Host + "/forms/" + pAba + ".aspx" + pParamentro);
            webBrowser1.Url = Program.uri;
            webBrowser1.Refresh();

        }
        private void AbrirAbaRelatorio(string pAba)
        {
            if (geral.UsuarioAtual != null)
            {
                webBrowser1.Stop();
                //if (Program.uri.Host == "localhost")
                //    Program.uri = new System.Uri("http://localhost:52274/Relatorios/" + pAba + ".aspx");
                //else
                //    Program.uri = new System.Uri("http://" + Program.uri.Host + "/Relatorios/" + pAba + ".aspx");
                
                Program.uri = new System.Uri(_urlConfigs .Relatorios+ pAba + ".aspx");
                webBrowser1.Url = Program.uri;
                webBrowser1.Refresh();
            }
            else
                AbrirAba("brooks/loginaplicativo");
        }
        private void caminhoesMenuItem1_Click(object sender, EventArgs e)
        {
            AbrirAba("Caminhoes");
        }

        private void cadastroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("Clientes");
        }

        private void bloqueioFinanceiroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("BloqueioFinanceiro");
        }

        private void containeresMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("Containeres");
        }

        private void contratosMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void destinoFinalMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("DestinoFinal");
        }

        private void iBAMAMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("IBAMA");
        }

        private void motoristasMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("Motoristas");
        }

        private void municipiosMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("Municipios");
        }

        private void residuosMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("Residuos");
        }

        private void usuariosMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("Usuarios");
        }

        private void notasFiscaisMenuItem1_Click(object sender, EventArgs e)
        {
            if (geral.UsuarioAtual == "" || geral.UsuarioAtual == null)
            {
                MessageBox.Show("Usuário inválido!", "SILC");
            }
            else
            {
                if (!oUsuarioDados.ExisteUsuario(geral.UsuarioAtual, geral.CodigoEmpresa))
                {
                    MessageBox.Show("Usuário inválido!", "SILC");
                }
                else
                {
                    if (geral.BancoDadosDefinido)
                    {
                        ControleMenuItem(true);
                        frmNotasFiscais frm = new frmNotasFiscais();
                        frm.ShowDialog();
                    }
                    else
                        MessageBox.Show("Banco de dados produção ainda não foi liberado!");
                }
            }
        }

        private void blocosMTRMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("BlocosMTR");
        }

        private void mTRCanceladaTransbordoMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("MTRCanceladaTransbordo");
        }

        private void dDRMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Stop();
            //if (Program.uri.Host == "localhost")
            //    Program.uri = new System.Uri("http://localhost:52274/forms/DDR.aspx?Codigo=000282&BROOKS=BRO000935");
            //else
            //    Program.uri = new System.Uri("http://" + Program.uri.Host + "/forms/DDR.aspx?Codigo=000282&BROOKS=BRO000935");

            Program.uri = new System.Uri(_urlConfigs.DDR);
            webBrowser1.Url = Program.uri;
            webBrowser1.Refresh();
        }

        private void cDFMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.Stop();
            //if (Program.uri.Host == "localhost")
            //    Program.uri = new System.Uri("http://localhost:52274/forms/CDF.aspx?Codigo=000282&BROOKS=BRO000935");
            //else
            //    Program.uri = new System.Uri("http://" + Program.uri.Host + "/forms/CDF.aspx?Codigo=000282&BROOKS=BRO000935");

            Program.uri = new System.Uri(_urlConfigs.DDR);
            webBrowser1.Url = Program.uri;
            webBrowser1.Refresh();
        }

        private void armazenadosItem_Click(object sender, EventArgs e)
        {
            frmDTR oFrmDTR = new frmDTR();
            oFrmDTR.StartPosition = FormStartPosition.CenterScreen;
            oFrmDTR.ShowDialog();
        }

        private void enviadosMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("DTR_Enviados");
        }

        private void controleAterroSanitarioMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("ControleAterroSanitario");
        }

        private void geraisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("ConfigSis");
        }

        private void alterarSenhaIPMtem_Click(object sender, EventArgs e)
        {
            AbrirAba("SenhaIPM");
        }

        private void cadastroToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AbrirAba("Parametros");
        }

        private void codigosServicosPrefeituraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("CodigosServicosPrefeitura");
        }

        private void descricaoServicosNFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("DescricaoServicosNF");
        }

        private void liberarProgramacaoServicosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("LiberarProgramacaoServicos");
        }

        private void emailsPadrãoEnvioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("EmailsPadraoEnvio");
        }

        private void retencaoImpostosNFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("RetencaoImpostosNF");
        }

        private void documentosPaginaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult _dr1 = new DialogResult();
            _dr1 = MessageBox.Show("Upload de arquivos somente na página. Deseja continuar assim mesmo?", "Upload documentos", MessageBoxButtons.YesNo);
            if (_dr1 == DialogResult.Yes)
                AbrirAba("DocumentosPagina");
        }

        private void PegaUsuarioWebBrowser()
        {
            var search = webBrowser1.Document.GetElementsByTagName("table");

            foreach (HtmlElement ele in search)
            {
                //Localiza a tabela na página
                HtmlElement h2 = ele.FirstChild;

                if (h2 != null && h2.InnerText != null)
                {
                    if (h2.InnerText.ToString().IndexOf("Usuário"[0], 0) > 0)
                    {
                        if (h2.InnerText.Length > 8 && h2.InnerText.Length < 42)
                        {
                            int qtchar = 32;
                            if (h2.InnerText.Length <= 25)
                                qtchar = h2.InnerText.Length - 7;
                            string login = h2.InnerText.ToString();
                            login = login.Substring(h2.InnerText.ToString().IndexOf("Usuário"[0]) + 9);
                            
                            //string db = login.Substring(login.IndexOf("db:"[0]) + 3).Trim();
                            //if (geral.IsNumeric(db))
                            //    geral.BancoUsado = Convert.ToInt16(db);
                            
                            login = login.Split("\r"[0])[0].ToString().Trim();
                            geral.UsuarioAtual = login;
                            break;
                        }
                    }
                }
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            PegaUsuarioWebBrowser();
            if (geral.UsuarioAtual != null && geral.UsuarioAtual != "")
                ControleMenuItem(true);
        }

        private void relatorioContaineresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAbaRelatorio("RelatorioContaineres");
        }

        private void relatorioConferenciaDiariaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAbaRelatorio("RelatorioConferenciaDiaria");
        }

        private void relatorioConferenciaDiariaPorPeriodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAbaRelatorio("RelatorioConferenciaDiariaPorPeriodo");
        }

        private void relatorioConferenciaDiariaPorClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAbaRelatorio("RelatorioConferenciaDiariaPorCliente");
        }

        private void desenvolvedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("ServicosDesenvolvedor");
        }

        private void upLoadRGRDDRCDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult _dr1 = new DialogResult();
            _dr1 = MessageBox.Show("Upload de arquivos somente na página. Deseja continuar assim mesmo?", "Upload documentos", MessageBoxButtons.YesNo);
            if (_dr1 == DialogResult.Yes)
                AbrirAba("uploadRGRCDFDDR");
        }

        private void RelatorioParaFaturamentoComResíduoMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAbaRelatorio("RelatorioParaFaturamentoComResiduos");
        }

        private void RelatorioDeIndicadoresMenuItem_Click(object sender, EventArgs e)
        {
            frmRelatorioIndicadores _frm = new frmRelatorioIndicadores();
            _frm.ShowDialog();
        }

        private void RelatóriosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void RelatorioMovimentoDeResiduosParaDestinoFinalMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAbaRelatorio("RelatorioMovimentacaoResiduosPorDestinoFinal");
        }

        private void ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            AbrirAbaRelatorio("RelatorioMovimentacaoResiduosPorDestinoFinalComValores");
        }

        private void RelatorioDeFaturamentoEISSMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void RelatorioMovimentoDeAterroComNrDaMTReMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAbaRelatorio("RelatorioMovimentacaoDestinoFinal");
        }

        private void RelatórioDeClientesBloqueadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAbaRelatorio("RelatorioDeClientesBloqueados");
        }

        private void RelatorioPorDestinoFinalReciclaveisMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAbaRelatorio("RelatorioReciclaveisPorDestinoFinal");
        }

        private void ToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            AbrirAbaRelatorio("RelatorioParaFaturamentoParaCliente");
        }

        private void RelatorioDeReciclaveisMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAbaRelatorio("RelatorioReciclaveis");
        }

        private void relatórioDeCDFEmitidoPorDestinoFinalPorPeríodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAbaRelatorio("RelatorioTotalResiduos");
        }

        private void relatorioGerencialMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAbaRelatorio("RelatorioGerencial");
        }

        private void relatórioTotalDeResíduosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAbaRelatorio("RelatorioTotalResiduos");
        }

        private void locaçõesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (geral.BancoDadosDefinido)
            {
                ControleMenuItem(true);
                using (frmLocacoes frm = new frmLocacoes())
                {
                    frm.ShowDialog();
                    frm.Close();
                    frm.Dispose();
                }
            }
            else
                MessageBox.Show("Banco de dados produção ainda não foi liberado!");
        }

        private void ticketsPendentesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("TicketsPendentes");
        }

        private void mTRePendentesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("MTRsPendentes");
        }

        private void cadastrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("Contratos_Cadastro");
        }

        private void alteraçãoConsultaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("Contratos_Alterar");
        }

        private void contratosresiduostesteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("Contratos_Residuos", "?CodigoGerado=838");
        }

        private void lançamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void relatórioPorDestinoFinalParaConferênciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("RelatorioDestinoFinalConferencia", "?Codigo=000000&BROOKS=BRO000935");
        }

        private void reajustarRepactuarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("Contratos_Reajustes");
        }

        private void rescisõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAba("Contratos_Rescisao");
        }

        private void escolherBancoDeDadosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void escolherBancoDeDadosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (geral.UsuarioAtual.ToUpper() == "TEIXEIRA")
                AbrirAba("EscolherDB");
            else
                MessageBox.Show("Sem permissão!", "Escolher Banco de Dados");
        }

        private void testeWebserviceIMAMTReToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBrowserIMA o_frmBrowserIMA = new frmBrowserIMA();
            o_frmBrowserIMA.ShowDialog();
        }

        private void distribuiçãoCálculoQuantidadesDescarregadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDistribuicaoDescargaPendente o_frmDistribuicao = new frmDistribuicaoDescargaPendente();
            o_frmDistribuicao.ShowDialog();
        }

        private void locaçõesAPartirDaProgramaçãoDeServiçosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (geral.BancoDadosDefinido)
            {
                ControleMenuItem(true);
                using (frmLocacaoProgramacao ofrmLocacaoProgramacao = new frmLocacaoProgramacao())
                {
                    ofrmLocacaoProgramacao.ShowDialog();
                }
            }
            else
                MessageBox.Show("Banco de dados produção ainda não foi liberado!");
        }
        private void relatorioDeNotasFiscaisEImpostosMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAbaRelatorio("RelatorioNotasFiscaisImpostos");
        }

        private void relatórioDeClientesAReajustarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAbaRelatorio("RelatorioDeClientesAReajustar");
        }

        private void relatorioDeLogsMenuItem_Click(object sender, EventArgs e)
        {
            AbrirAbaRelatorio("RelatorioDeLogs");
        }

        private void iMAMTReToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIMA ofrmIMA = new frmIMA();
            ofrmIMA.ShowDialog();
        }

        private void modeloMTReToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmModeloMTRe ofrmModeloMTRe = new frmModeloMTRe();
            ofrmModeloMTRe.ShowDialog();
        }

        private void chavesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChaves form = new frmChaves();
            form.ShowDialog();
        }
    }
}