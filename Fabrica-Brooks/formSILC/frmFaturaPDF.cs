using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SILCNegocios;
using LibSILC;

namespace formSILC
{
    public partial class frmFaturaPDF : Form
    {
        public int CodigoCliente;
        public int Parcelas;
        public DateTime Vencimento;
        public int DiasEntreVencimentos;
        public bool pApenasGerarFatura;

        protected Label[] _numerofatura = new Label[3];
        protected Label[] _numeroparcela = new Label[3];
        protected Label[] _vencimentoparcela = new Label[3];
        protected Label[] _valorparcela = new Label[3];

        private clsClientes oCliente = new clsClientes();
        private clsClienteDados oClienteDados = new clsClienteDados();
        private clsEnderecos oEndereco = new clsEnderecos();
        private clsEnderecosDados oEnderecoDados = new clsEnderecosDados();
        private clsMunicipios oMunicipio = new clsMunicipios();
        private clsMunicipiosDados oMunicipioDados = new clsMunicipiosDados();

        public frmFaturaPDF()
        {
            InitializeComponent();
        }
        private void frmFaturaPDF_Load(object sender, EventArgs e)
        {
            LeituraInicial();
        }
     
        public void LeituraInicial()
        {
            if (geral.BancoUsado == 4) // fastcompost
            {
                geral.CodigoEmpresa = 6;
                picLogo.Image = Image.FromFile("logo.jpg");
            }
            // dados da empresa
            geral.oEmpresa = geral.oEmpresaDados.PegaDados(geral.oEmpresa, geral.CodigoEmpresa);
            lblRazaoSocial.Text = geral.oEmpresa.Nome;
            lblCNPJ.Text = geral.oEmpresa.CNPJ_CPF;
            lblEndereco.Text = geral.oEmpresa.Endereco;
            lblCidadeUFCEP.Text = geral.oEmpresa.Cidade + "/" + geral.oEmpresa.UF + " - CEP " + geral.oEmpresa.CEP;
            lblFones.Text = "Fones: " + geral.oEmpresa.Telefones;
            if (geral.oEmpresa.Email != "")
                lblEmail.Text = "e-mail: " + geral.oEmpresa.Email;

            if (geral.BancoUsado == 4) // fastcompost
            {
                lblInscricaoMunicipalEstadual.Text = "";
            }
            else
            {
                if (geral.oEmpresa.IE_RG == "")
                    lblInscricaoMunicipalEstadual.Text = "Insc.Municipal: 1609  Insc.Estadual: 255.141.351";
                else
                    lblInscricaoMunicipalEstadual.Text = "Insc.Municipal: 1609 - Insc. Estadual: " + geral.oEmpresa.IE_RG;
            }

            // dados do cliente
            oCliente = new clsClientes();
            oClienteDados = new clsClienteDados();
            oEndereco = new clsEnderecos();
            oEnderecoDados = new clsEnderecosDados();

            oCliente = oClienteDados.PegaDados(oCliente, CodigoCliente);

            lblNomeCliente.Text = oCliente.Nome2;
            if (lblNomeCliente.Text == "")
                lblNomeCliente.Text = oCliente.Nome;

            lblCNPJCPFCliente.Text = oCliente.CNPJ_CPF;

            oEndereco = oEnderecoDados.PegaDados(oEndereco, CodigoCliente, 1, 0);
            if (oEndereco.Numero != "0" && oEndereco.Numero != "")
                lblEnderecoCliente.Text = oEndereco.endereco + ", " + oEndereco.Numero;
            else
                lblEnderecoCliente.Text = oEndereco.endereco;
            lblBairroCliente.Text = oEndereco.Bairro;
            lblCEPCliente.Text = oEndereco.CEP;

            oMunicipio = new clsMunicipios();
            oMunicipioDados = new clsMunicipiosDados();
            oMunicipio = oMunicipioDados.PegaDados(oMunicipio, oEndereco.CodigoMunicipio);

            lblUFCliente.Text = oMunicipio.UF;
            lblMunicipioCliente.Text = oMunicipio.Nome;

            lblFonesCliente.Text = "";
            if (oEndereco.Fone1 != "")
                if (oEndereco.DDD1 > 0)
                    lblFonesCliente.Text = oEndereco.DDD1.ToString() + "  " + oEndereco.Fone1;
                else
                    lblFonesCliente.Text = oEndereco.Fone1;
            if (oEndereco.Fone2 != "")
                if (oEndereco.DDD2 > 0)
                    lblFonesCliente.Text = lblFonesCliente.Text + "  " + oEndereco.DDD2.ToString() + "-" + oEndereco.Fone2;
                else
                    lblFonesCliente.Text = lblFonesCliente.Text + "  " + oEndereco.Fone2;
            if (oEndereco.Fone3 != "")
                if (oEndereco.DDD3 > 0)
                    lblFonesCliente.Text = lblFonesCliente.Text + "  " + oEndereco.DDD3.ToString() + "-" + oEndereco.Fone3;
                else
                    lblFonesCliente.Text = lblFonesCliente.Text + "  " + oEndereco.Fone3;

            lblFonesCliente.Text = lblFonesCliente.Text.Trim();

            // parcelas
            CriaControlesParcela();

            if (pApenasGerarFatura)
            {
                
                btnImprimir.Visible = false;
                btnFechar.Visible = false;
                timer1.Enabled = true;
            }
        }

        private void CriaControlesParcela()
        {
            int k = 22;
            for (int i = 0; i < Parcelas; i++)
            {
                _numerofatura[i] = new Label();
                _numerofatura[i].Name = "NumeroFatura" + i.ToString();
                _numerofatura[i].Location = new System.Drawing.Point(4, (k * 20));
                _numerofatura[i].Size = new System.Drawing.Size(70, 22);
                if (i == 0)
                    _numerofatura[i].Text = lblNumeroFatura.Text;
                panDados.Controls.Add(_numerofatura[i]);

                _numeroparcela[i] = new Label();
                _numeroparcela[i].Name = "NumeroParcela" + i.ToString();
                _numeroparcela[i].Location = new System.Drawing.Point(80, (k * 20));
                _numeroparcela[i].Size = new System.Drawing.Size(40, 22);
                _numeroparcela[i].Text = (i + 1).ToString();
                panDados.Controls.Add(_numeroparcela[i]);

                _vencimentoparcela[i] = new Label();
                _vencimentoparcela[i].Name = "Vencimento" + i.ToString();
                _vencimentoparcela[i].Location = new System.Drawing.Point(150, (k * 20));
                _vencimentoparcela[i].Size = new System.Drawing.Size(70, 22);
                if (i == 0)
                    _vencimentoparcela[i].Text = Vencimento.ToShortDateString();
                else
                    _vencimentoparcela[i].Text = Vencimento.AddDays(DiasEntreVencimentos * i).ToShortDateString();
                panDados.Controls.Add(_vencimentoparcela[i]);
                _valorparcela[i] = new Label();
                _valorparcela[i].Name = "Valor" + i.ToString();
                _valorparcela[i].Location = new System.Drawing.Point(250, (k * 20));
                _valorparcela[i].Size = new System.Drawing.Size(70, 22);
                _valorparcela[i].Text = (Convert.ToDecimal(lblValorTotalBaixo.Text) / Parcelas).ToString("N2");
                panDados.Controls.Add(_valorparcela[i]);                        
                k++;
            }
        }
        private void GeraPDF()
        {
            var doc = new PdfSharp.Pdf.PdfDocument();
            var page = doc.AddPage();

            var graphics = PdfSharp.Drawing.XGraphics.FromPdfPage(page);
            var textFormatter = new PdfSharp.Drawing.Layout.XTextFormatter(graphics);
            var font = new PdfSharp.Drawing.XFont("Arial", 14);

            // escrever texto
            //textFormatter.DrawString("Que belo texto!", font, PdfSharp.Drawing.XBrushes.Red, new PdfSharp.Drawing.XRect(0, 0, page.Width, page.Height));

            // desenhar linha
            //graphics.DrawLine(PdfSharp.Drawing.XPens.Blue, 150, 150, 250, 200);

            // desenhar retangulo
            //graphics.DrawRoundedRectangle(PdfSharp.Drawing.XPens.Green, PdfSharp.Drawing.XBrushes.LightGreen, 100, 300, 100, 50, 10, 10);

            CaptureScreen();

            // adicionando imagens
            PdfSharp.Drawing.XPoint x = new PdfSharp.Drawing.XPoint();
            graphics.DrawImage(PdfSharp.Drawing.XImage.FromFile("arquivo" + lblNumeroFatura.Text + ".bmp"),  x);
            graphics.Save();

            // Salvando arquivo PDF
            for (int tt = 1; tt < 3; tt++)
            {
                try
                {
                    Thread.Sleep(1000);
                    doc.Save("arquivo" + lblNumeroFatura.Text + ".pdf");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            doc.Dispose();

            if (!pApenasGerarFatura)
            {
                // abrindo PDF no reader
                System.Diagnostics.Process.Start("arquivo" + lblNumeroFatura.Text + ".pdf");
            }

        }

        private void CaptureScreen()
        {
            Bitmap memoryImage;
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width - 50, s.Height - 20, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X + 40, this.Location.Y + 10, 0, 0, s);
            memoryImage.Save("arquivo" + lblNumeroFatura.Text + ".bmp");
            memoryImage = null;
        }

        private void frmFaturaPDF_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                GeraPDF();
            }
            finally
            {
                this.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;
                GeraPDF();
            }
            finally
            {
                this.Close();
            }

        }
    }
}
