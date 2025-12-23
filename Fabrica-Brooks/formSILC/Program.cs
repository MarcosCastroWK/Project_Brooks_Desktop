using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibSILC;
using System.Threading;

namespace formSILC
{
    static class Program
    {
        /*public static System.Uri uri = new System.Uri("http://localhost:26089/WebSite1/forms/brooks/loginaplicativo.aspx");*/

        public static System.Uri uri = new System.Uri("http://servidor/forms/brooks/loginaplicativo.aspx");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            geral.AcessoPrincipal = true;
            geral.AcessoPermitido = true;
            geral.CodigoEmpresa = 1;
            geral.BancoUsado = 1;
            //Application.Run(new teste());
            if (args.Length != 0)
            {
                Application.Run(new frmMenu());
            }
            else
            {
                Application.Run(new frmInicio());

                //var autoEvent = new AutoResetEvent(false);
                //var statusChecker = new StatusChecker();
                //var stateTimer = new System.Threading.Timer(statusChecker.CheckStatus, autoEvent, 1000, 1000);
                //autoEvent.WaitOne();
                //stateTimer.Change(5000, 5000);
                //autoEvent.WaitOne();
            }
        }
    }

    class StatusChecker
    {
        public StatusChecker()
        {
        }

        public void CheckStatus(Object stateInfo)
        {
            /*
            AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
            //autoEvent.Set(); // para o timer
            string _ip = "";
            IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress[] addr = ipEntry.AddressList;
            _ip = addr[1].ToString();
            string _path = @"\\servidor\Temp\" + _ip.Replace(".", "") + ".txt";
            if (!System.IO.File.Exists(_path))
            {
                _path = @"\\servidor\winSILC\temp\" + _ip.Replace(".", "") + ".txt";
            }
            if (System.IO.File.Exists(_path))
            {
                System.IO.StreamReader _sr = new System.IO.StreamReader(_path);
                string _lido = _sr.ReadLine();
                _sr.Close();
                System.IO.File.Delete(_path);
                string[] _parametro = _lido.Split("-"[0]);
                geral.CodigoUsuarioAtual = Convert.ToInt32(_parametro[0]);
                geral.UsuarioAtual = _parametro[1].ToString();
                Form oteste = new Form();
                try
                {
                    oteste.WindowState = FormWindowState.Minimized;
                    oteste.Show();
                }
                finally
                {
                    oteste.WindowState = FormWindowState.Maximized;
                    oteste.Close();
                }
                if (_parametro[2].ToString() == "Locacao")
                {
                    frmLocacoes ofrmLocacao = new frmLocacoes();
                    ofrmLocacao.WindowState = FormWindowState.Maximized;
                    ofrmLocacao.ShowDialog();
                }
                else if (_parametro[2].ToString() == "LocacaoProgramacao")
                {
                    frmLocacaoProgramacao ofrmLocacaoProgramacao = new frmLocacaoProgramacao();
                    ofrmLocacaoProgramacao.WindowState = FormWindowState.Maximized;
                    ofrmLocacaoProgramacao.ShowDialog();
                }
                else if (_parametro[2].ToString() == "DTR")
                {
                    frmDTR ofrmDTR = new frmDTR();
                    ofrmDTR.StartPosition = FormStartPosition.CenterScreen;
                    ofrmDTR.ShowDialog();
                }
                else if (_parametro[2].ToString() == "Distribuicao")
                {
                    frmDistribuicaoDescargaPendente ofrmDistribuicao = new frmDistribuicaoDescargaPendente();
                    ofrmDistribuicao.StartPosition = FormStartPosition.CenterScreen;
                    ofrmDistribuicao.ShowDialog();
                }
                else if (_parametro[2].ToString() == "Programacao")
                {
                    frmProgramacaoDiaria ofrmProgramacao = new frmProgramacaoDiaria();
                    ofrmProgramacao.WindowState = FormWindowState.Maximized;
                    ofrmProgramacao.ShowDialog();
                }
                else if (_parametro[2].ToString() == "NotasFiscais")
                {
                    frmNotasFiscais ofrmNotasFiscais = new frmNotasFiscais();
                    ofrmNotasFiscais.WindowState = FormWindowState.Maximized;
                    ofrmNotasFiscais.ShowDialog();
                }
                else if (_parametro[2].ToString() == "RelatorioMTReIMA")
                {
                    frmCDFeImportacao ofrmCDFe = new frmCDFeImportacao();
                    ofrmCDFe.ShowDialog();
                }
                else if (_parametro[2].ToString() == "SistemaIMA_AtualizarSenha")
                {
                    frmSistemaFatma ofrmSistemaIMA = new frmSistemaFatma();
                    ofrmSistemaIMA.ShowDialog();
                }
                else if (_parametro[2].ToString() == "ImportarMunicipiosRadar")
                {
                    frmImportacaoMunicipios ofrmImpMunicipios = new frmImportacaoMunicipios();
                    ofrmImpMunicipios.ShowDialog();
                }
                else if (_parametro[2].ToString() == "RelatorioMTReFatimaConferenciaDiaria")
                {
                    frmRelatorioMTReConferenciaDiaria ofrmRelMTReConfDiaria = new frmRelatorioMTReConferenciaDiaria();
                    ofrmRelMTReConfDiaria.ShowDialog();
                }
                else if (_parametro[2].ToString() == "AtualizacaoDados")
                {
                    frmAtualizacaoDados ofrmAtualizacaoDados = new frmAtualizacaoDados();
                    ofrmAtualizacaoDados.ShowDialog();
                }
                else if (_parametro[2].ToString() == "DocumentacaoAplicavel")
                {
                    frmDocumentacaoAplicavel ofrmDocAplicavel = new frmDocumentacaoAplicavel();
                    ofrmDocAplicavel.ShowDialog();
                }
                else if (_parametro[2].ToString() == "RelatorioIndicadores")
                {
                    frmRelatorioIndicadores ofrmRelIndicadores = new frmRelatorioIndicadores();
                    ofrmRelIndicadores.ShowDialog();
                }
            }
            */
        }
    }
}
