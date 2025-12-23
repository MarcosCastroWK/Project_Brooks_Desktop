using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using LibSILC;
using SILCNegocios;

namespace formSILC
{
    public partial class frmVisualPrintDTR : Form
    {
        public DataTable _dt = new DataTable();
        private clsDestinoFinalDados oDestinoFinalDados = new clsDestinoFinalDados();
        private int regs = 0;
        private decimal pagina = 0;

        public frmVisualPrintDTR()
        {
            InitializeComponent();
        }

        private void frmVisualPrintDTR_Load(object sender, EventArgs e)
        {
            intCopias.VALOR.Text = "2";
            pagina = 0;
            regs = 0;
            printPreviewControl.Document = ppd;
            printPreviewControl.Zoom = 1.2;
            printPreviewControl.Show();
        }

        private void ppd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile("logobrooks.png");
            Point loc = new Point(10, 10);
            e.Graphics.DrawImage(img, loc);

            float cl = 10.0F;
            float ln = 20.0F;

            Font ft = new Font("Arial", 8);
            e.Graphics.DrawString(geral.NomeEmpresa(geral.CodigoEmpresa), ft, Brushes.Black, 230, ln);

            ln = ln + 16.0F;
            e.Graphics.DrawString("RELAÇÃO DE RESÍDUOS ENCAMINHADOS A", ft, Brushes.Black, 230, ln);

            DateTime _dataEnvio = DateTime.Now;
            string _localentrega = "";
            int _codigoresiduo = 0;
            if (_dt.Rows.Count > 0)
            {
                _dataEnvio = Convert.ToDateTime(_dt.Rows[0][8]);
                _localentrega = _dt.Rows[0][9].ToString();
                _codigoresiduo = Convert.ToInt32(_dt.Rows[0][10]);
            }
            ln = ln + 16.0F;
            e.Graphics.DrawString(oDestinoFinalDados.PegaRazaoSocial(_localentrega), ft, Brushes.Black, 230, ln);
            e.Graphics.DrawString("ENVIADO EM: " +
                "" + _dataEnvio.ToString("dd/MM/yy"), ft, Brushes.Black, 620, ln);

            clsResiduos oResiduo = new clsResiduos();
            clsResiduoDados oResiduoDados = new clsResiduoDados();
            oResiduo = oResiduoDados.PegaDados(oResiduo, _codigoresiduo);
            ln = ln + 16.0F;
            e.Graphics.DrawString("CÓDIGO RESÍDUO: " + oResiduo.CodigoResiduoManifesto, ft, Brushes.Black, 230, ln);
            ln = ln + 16.0F;
            e.Graphics.DrawString("TIPO RESÍDUO: " + oResiduo.DescricaoGrupo + "/" + oResiduo.DescricaoReduzida, ft, Brushes.Black, 230, ln);

            decimal _PesoKgTotal = 0;
            int _itens = 0;
            foreach (DataRow dr in _dt.Rows)
            {
                _itens++;
                _PesoKgTotal = _PesoKgTotal + Convert.ToDecimal(dr["PesoKg"]);
            }

            ft = new Font("Arial", 7);
            ln = ln + 40.0F;
            e.Graphics.DrawString("Data Coleta", ft, Brushes.Black, cl, ln);
            cl = cl + 68;
            e.Graphics.DrawString("Código", ft, Brushes.Black, cl, ln);
            cl = cl + 68;
            e.Graphics.DrawString("CNPJ Cliente", ft, Brushes.Black, cl, ln);
            cl = cl + 110;
            e.Graphics.DrawString("Nome Fantasia Cliente", ft, Brushes.Black, cl, ln);
            cl = cl + 230;
            e.Graphics.DrawString("Peso KG", ft, Brushes.Black, cl, ln);
            cl = cl + 100;
            e.Graphics.DrawString("Percentual", ft, Brushes.Black, cl, ln);
            cl = cl + 100;
            e.Graphics.DrawString("Nº MTR-e", ft, Brushes.Black, cl, ln);

            ln = ln + 12.0F;
            string str = "";
            char pad = '─';
            cl = 10.0F;
            e.Graphics.DrawString(str.PadLeft(110, pad), ft, Brushes.Black, cl, ln);
            StringFormat alinhaDireita = new StringFormat();
            alinhaDireita.Alignment = StringAlignment.Far;
            int _NrLinhas = 600;
            if (e.PageBounds.Height > 600)
                _NrLinhas = e.PageBounds.Height - 100;
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
                        e.Graphics.DrawString(Convert.ToDateTime(dr["DataColeta"]).ToString("dd/MM/yyyy"), ft, Brushes.Black, cl, ln);
                        cl = cl + 108;
                        e.Graphics.DrawString(dr["CodigoCliente"].ToString(), ft, Brushes.Black, cl, ln, alinhaDireita);
                        cl = cl + 28;
                        e.Graphics.DrawString(dr["CNPJ_CPF"].ToString(), ft, Brushes.Black, cl, ln);
                        cl = cl + 110;
                        if (dr["Gerador"].ToString().Length > 30)
                            e.Graphics.DrawString(dr["Gerador"].ToString().Substring(0, 30), ft, Brushes.Black, cl, ln);
                        else
                            e.Graphics.DrawString(dr["Gerador"].ToString(), ft, Brushes.Black, cl, ln);
                        cl = cl + 280;
                        e.Graphics.DrawString(Convert.ToDecimal(dr["PesoKg"]).ToString("N2"), ft, Brushes.Black, cl, ln, alinhaDireita);
                        cl = cl + 100;
                        decimal _PercentualPeso = Convert.ToDecimal(dr["PesoKg"]) * 100 / _PesoKgTotal;
                        e.Graphics.DrawString(_PercentualPeso.ToString("N2"), ft, Brushes.Black, cl, ln, alinhaDireita);
                        cl = cl + 100;
                        e.Graphics.DrawString(dr[6].ToString(), ft, Brushes.Black, cl, ln, alinhaDireita);
                    }
                    else
                    {
                        ln = ln + 20F;
                        cl = 385;
                        pagina++;
                        cboPagina.Items.Add(pagina);
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
                ln = ln + 12F;
                cl = 10.0F;
                e.Graphics.DrawString(str.PadLeft(110, pad), ft, Brushes.Black, cl, ln);
                ln = ln + 12F;
                cl = cl + 10;
                e.Graphics.DrawString("TOTAL ITENS: " + _itens.ToString(), ft, Brushes.Black, cl, ln);
                e.Graphics.DrawString("TOTAL:", ft, Brushes.Black, 400, ln);
                e.Graphics.DrawString(_PesoKgTotal.ToString("N2"), ft, Brushes.Black, 536, ln, alinhaDireita);
                e.Graphics.DrawString(100.ToString("N2"), ft, Brushes.Black, 636, ln, alinhaDireita);

                ln = ln + 20F;
                cl = 385;
                pagina++;
                cboPagina.Items.Add(pagina);
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
            for (int i = 0; i < Convert.ToInt32(intCopias.VALOR.Text); i++)
            {
                pagina = 0;
                regs = 0;
                ppd.Print();
            }
            this.Close();
        }

        private void intCopias_Leave(object sender, EventArgs e)
        {
            if (intCopias.VALOR.Text == "")
                intCopias.VALOR.Text = "1";
            if (Convert.ToInt32(intCopias.VALOR.Text) > 6)
                intCopias.VALOR.Text = "3";
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
