using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using LibSILC;
using SILCNegocios;

namespace formSILC
{
    public partial class frmVisualPrintEstoque : Form
    {
        public DataTable _dt = new DataTable();
        private clsDestinoFinalDados oDestinoFinalDados = new clsDestinoFinalDados();
        private int regs = 0;
        private decimal pagina = 0;

        public frmVisualPrintEstoque()
        {
            InitializeComponent();
        }

        private void frmVisualPrintEstoque_Load(object sender, EventArgs e)
        {
            pagina = 0;
            regs = 0;
            ppd.DefaultPageSettings.Landscape = true;
            printPreviewControl.Document = ppd;            
            printPreviewControl.Zoom = 1;
            printPreviewControl.Show();
        }

        private void ppd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile("logobrooks.png");
            Point loc = new Point(10, 10);
            e.Graphics.DrawImage(img, loc);

            float cl = 10.0F;
            float ln = 20.0F;

            Font ft = new Font("Tahoma", 10);
            e.Graphics.DrawString(geral.NomeEmpresa(geral.CodigoEmpresa), ft, Brushes.Black, 230, ln);

            ln = ln + 16.0F;
            e.Graphics.DrawString("DEPÓSITO TEMPORÁRIO DE RESÍDUOS", ft, Brushes.Black, 230, ln);
            
            ft = new Font("Tahoma", 8);
            ln = ln + 60.0F;
            e.Graphics.DrawString("Data Coleta", ft, Brushes.Black, cl, ln);
            cl = cl + 70;
            e.Graphics.DrawString("Nome Fantasia Cliente", ft, Brushes.Black, cl, ln);
            cl = cl + 300;
            e.Graphics.DrawString("Grupo/Resíduo", ft, Brushes.Black, cl, ln);
            cl = cl + 390;
            e.Graphics.DrawString("Quantidade", ft, Brushes.Black, cl, ln);
            cl = cl + 70;
            e.Graphics.DrawString("Und", ft, Brushes.Black, cl, ln);
            cl = cl + 30;
            e.Graphics.DrawString("DTR", ft, Brushes.Black, cl, ln);
            cl = cl + 54;
            e.Graphics.DrawString("Data saída", ft, Brushes.Black, cl, ln);
            cl = cl + 60;
            e.Graphics.DrawString("Destino final", ft, Brushes.Black, cl, ln);
            cl = cl + 158;
            e.Graphics.DrawString("Imp", ft, Brushes.Black, cl, ln);

            ln = ln + 12.0F;
            string str = "";
            char pad = '─';
            cl = 10.0F;
            e.Graphics.DrawString(str.PadLeft(142, pad), ft, Brushes.Black, cl, ln);
            StringFormat alinhaDireita = new StringFormat();
            alinhaDireita.Alignment = StringAlignment.Far;
            int _NrLinhas = 640;
            if (e.PageBounds.Height > 640)
                _NrLinhas = e.PageBounds.Height - 140;
            if (_dt.Rows.Count > 0)
            {
                DataRow dr = _dt.Rows[0];
                for (int i = regs; i <= _dt.Rows.Count - 1; i++)
                {
                    regs++;
                    dr = _dt.Rows[i];
                    if (ln < _NrLinhas)
                    {
                        ln = ln + 12F;
                        cl = 10.0F;
                        if (dr["DataColeta"].ToString() != "")
                            e.Graphics.DrawString(Convert.ToDateTime(dr["DataColeta"]).ToString("dd/MM/yyyy"), ft, Brushes.Black, cl, ln);
                        else
                            e.Graphics.DrawString("", ft, Brushes.Black, cl, ln);
                        cl = cl + 70;
                        e.Graphics.DrawString(dr["NomeCliente"].ToString(), ft, Brushes.Black, cl, ln);
                        cl = cl + 300;
                        e.Graphics.DrawString(geral.Left(dr["Residuo"].ToString(), 60), ft, Brushes.Black, cl, ln);
                        cl = cl + 454;
                        if (dr["Quantidade"].ToString() != "")
                            e.Graphics.DrawString(Convert.ToDecimal(dr["Quantidade"]).ToString("N2"), ft, Brushes.Black, cl, ln, alinhaDireita);
                        else
                            e.Graphics.DrawString("", ft, Brushes.Black, cl, ln, alinhaDireita);
                        cl = cl + 6;
                        e.Graphics.DrawString(dr["Unidade"].ToString(), ft, Brushes.Black, cl, ln );
                        cl = cl + 30;
                        e.Graphics.DrawString(dr["LocalDTR"].ToString(), ft, Brushes.Black, cl, ln);
                        cl = cl + 54;
                        e.Graphics.DrawString(dr["DataSaida"].ToString(), ft, Brushes.Black, cl, ln);
                        cl = cl + 60;
                        e.Graphics.DrawString(dr["DestinoFinal"].ToString(), ft, Brushes.Black, cl, ln);
                        cl = cl + 160;
                        e.Graphics.DrawString(dr["Imprimido"].ToString(), ft, Brushes.Black, cl, ln);
                    }
                    else
                    {
                        ln = ln + 12F;
                        cl = 0;
                        pagina++;
                        cboPagina.Items.Add(pagina.ToString());
                        e.Graphics.DrawString(str.PadLeft(88, pad), ft, Brushes.Black, 10, _NrLinhas + 30);
                        e.Graphics.DrawString(geral.NomeSistema, ft, Brushes.Black, 12, _NrLinhas + 40);
                        e.Graphics.DrawString("Pagina: " + pagina, ft, Brushes.Black, 570, _NrLinhas + 40);
                        e.HasMorePages = false;
                        break;
                    }
                }
            }
            if (regs >= _dt.Rows.Count - 1)
            {
                ln = ln + 20F;
                cl = 385;
                pagina++;
                cboPagina.Items.Add(pagina.ToString());
                e.Graphics.DrawString(str.PadLeft(88, pad), ft, Brushes.Black, 10, _NrLinhas + 30);
                e.Graphics.DrawString(geral.NomeSistema, ft, Brushes.Black, 12, _NrLinhas + 40);
                e.Graphics.DrawString("Pagina: " + pagina, ft, Brushes.Black, 570, _NrLinhas + 40);
                e.HasMorePages = false;
            }
            else
            {
                e.HasMorePages = true;
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ppd.PrinterSettings.DefaultPageSettings.Landscape = true;
            pagina = 0;
            regs = 0;
            ppd.Print();

        }

        private void btnIr_Click(object sender, EventArgs e)
        {
            if (cboPagina.Text != "")
            {
                printPreviewControl.StartPage = Convert.ToInt32(cboPagina.Text) - 1;
                printPreviewControl.Show();
            }
        }
    }
}
