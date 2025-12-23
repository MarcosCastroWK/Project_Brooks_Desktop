using System;
using System.Windows.Forms;
using LibSILC;
using SILCNegocios;
using System.Drawing;
using System.Threading;

namespace formSILC
{
    public partial class frmRecibo : Form
    {
        public int pCodigoCliente = 0;
        public decimal pValor = 0;
        public string pHistorico = "";
        clsParametros oEmpresa = new clsParametros();
        clsParametrosDados oEmpresaDados = new clsParametrosDados();
        clsClientes oCliente = new clsClientes();
        clsClienteDados oClienteDados = new clsClienteDados();
        Bitmap memoryImage;
        private Button printButton = new Button();

        public frmRecibo()
        {
            InitializeComponent();
        }

        private void frmRecibo_Load(object sender, EventArgs e)
        {
            oEmpresa = oEmpresaDados.PegaDados(oEmpresa, 1);
            lblRazaoSocialEmpresa.Text = oEmpresa.Nome;
            lblEnderecoEmpresa.Text = oEmpresa.Endereco;
            lblFonesEmpresa.Text = oEmpresa.Telefones;
            lblCNPJempresa.Text = oEmpresa.CNPJ_CPF;
            oCliente = oClienteDados.PegaDados(oCliente, pCodigoCliente);
            lblCliente.Text = oCliente.Nome;
            lblValor.Text = pValor.ToString("N2");
            lblExtenso.Text = "(" + geral.Extenso_Valor(pValor).ToUpper() + ")";
            
            lblReferente.Text = pHistorico;
            lblDia.Text = DateTime.Now.Day.ToString("00");
            lblMesExtenso.Text = geral.PegaMesExtenso(DateTime.Now.Month);
            lblAno.Text = DateTime.Now.Year.ToString();

            lblRazaoSocialEmpresa2.Text = oEmpresa.Nome;
            lblEnderecoEmpresa2.Text = oEmpresa.Endereco;
            lblFonesEmpresa2.Text = oEmpresa.Telefones;
            lblCNPJempresa2.Text = oEmpresa.CNPJ_CPF;
            lblCliente2.Text = oCliente.Nome;
            lblValor2.Text = pValor.ToString("N2");
            lblExtenso2.Text = lblExtenso.Text;
            lblReferente2.Text = pHistorico;
            lblDia2.Text = DateTime.Now.Day.ToString("00");
            lblMesExtenso2.Text = geral.PegaMesExtenso(DateTime.Now.Month);
            lblAno2.Text = DateTime.Now.Year.ToString();

            timer1.Enabled = false;
        }
        
        private void CaptureScreen()
        {
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            

            btnImprimir.Visible = false;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            timer1.Enabled = true;  
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CaptureScreen();
            printDocument1.Print();
            timer1.Enabled = false;
            this.Close();
        }
    }
}
