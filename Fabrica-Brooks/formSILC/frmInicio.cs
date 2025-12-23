using System;
using System.Net;
using System.Windows.Forms;
using LibSILC;
using SILCNegocios;

namespace formSILC
{
    public partial class frmInicio : Form
    {
        public frmInicio()
        {
            InitializeComponent();
        }

        private void frmInicio_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
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
        }
    }
}