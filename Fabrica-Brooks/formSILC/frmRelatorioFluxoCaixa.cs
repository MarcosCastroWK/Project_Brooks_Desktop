using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibSILC;

namespace formSILC
{
    public partial class frmRelatorioExecutado : Form
    {
        //private clsContasPagarDados oContasPagarDados = new clsContasPagarDados();
        //private clsContasReceberDados oContasReceberDados = new clsContasReceberDados();
        private DataTable dt = new DataTable();
        private DataTable dtR = new DataTable();
        private int regs = 0;
        private int pagina = 0;
        private decimal vTotal_aReceber, vTotal_aPagar, vTotalDiversos;

        public frmRelatorioExecutado()
        {
            InitializeComponent();
        }

        private void pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            decimal vSubReceber = 0;
            float cl = 10.0F;
            float ln = 12.0F;
            Font ft = new Font("Arial", 9);
            e.Graphics.DrawString(geral.NomeSistema, ft, Brushes.Black, cl, ln);

            ln = ln + 12.0F;
            e.Graphics.DrawString("Fluxo de Caixa - de: " + DataInicial.Text + " a " + DataFinal.Text, ft, Brushes.Black, cl, ln);
            e.Graphics.DrawString("Emissão: " + DateTime.Now.ToShortDateString(), ft, Brushes.Black, 520, ln);

            ln = ln + 20.0F;
            ft = new Font("Arial", 8);
            e.Graphics.DrawString("Nº", ft, Brushes.Black, cl, ln);
            cl = cl + 40;
            e.Graphics.DrawString("Descrição da Conta", ft, Brushes.Black, cl, ln);
            cl = cl + 180;
            e.Graphics.DrawString("Histórico", ft, Brushes.Black, cl, ln);
            cl = cl + 150;
            e.Graphics.DrawString("Vencimento", ft, Brushes.Black, cl, ln);
            cl = cl + 115;
            e.Graphics.DrawString("Valor", ft, Brushes.Black, cl, ln);

            cl = cl + 115;
            e.Graphics.DrawString("Saldo", ft, Brushes.Black, cl, ln);

            ln = ln + 12.0F;
            string str = "";
            char pad = '─';
            cl = 10.0F;
            e.Graphics.DrawString(str.PadLeft(90, pad), ft, Brushes.Black, cl, ln);
            StringFormat alinhaDireita = new StringFormat();
            alinhaDireita.Alignment = StringAlignment.Far;

            ln = ln + 12;
            ft = new Font("Arial", 8);
            e.Graphics.DrawString("Contas a Receber/Pagar/Diversos", ft, Brushes.Black, 10, ln);
            ln = ln + 5;

            // Contas a Receber/Pagar/Diversos
            for (int i = regs; i <= dtR.Rows.Count - 1; i++)
            {
                regs++;
                DataRow dr = dtR.Rows[i];
                if (ln < 1050)
                {
                    ln = ln + 12F;
                    cl = 40.0F;
                    e.Graphics.DrawString(dr["Numero"].ToString(), ft, Brushes.Black, cl, ln, alinhaDireita);
                    cl = cl + 10;
                    e.Graphics.DrawString(dr["NomeConta"].ToString(), ft, Brushes.Black, cl, ln);
                    cl = cl + 180;
                    e.Graphics.DrawString(dr["Historico"].ToString(), ft, Brushes.Black, cl, ln);
                    cl = cl + 155;
                    e.Graphics.DrawString(Convert.ToDateTime(dr["DataVencimento"]).ToShortDateString(), ft, Brushes.Black, cl, ln);
                    cl = cl + 160;
                    if (Convert.ToDecimal(dr["Valor"]) < 0)
                        e.Graphics.DrawString(Convert.ToDecimal(dr["Valor"]).ToString("#0.00"), ft, Brushes.Red, cl, ln, alinhaDireita);
                    else
                        e.Graphics.DrawString(Convert.ToDecimal(dr["Valor"]).ToString("#0.00"), ft, Brushes.Black, cl, ln, alinhaDireita);

                    vSubReceber = vSubReceber + Convert.ToDecimal(dr["Valor"]);
                    vTotal_aReceber = vTotal_aReceber + Convert.ToDecimal(dr["Valor"]);
                    
                    cl = cl + 130;
                    if (vTotal_aReceber < 0 && vTotal_aReceber > -300)
                        e.Graphics.DrawString(vTotal_aReceber.ToString("#0.00"), ft, Brushes.Coral, cl, ln, alinhaDireita);
                    else if (vTotal_aReceber < -300)
                        e.Graphics.DrawString(vTotal_aReceber.ToString("#0.00"), ft, Brushes.Red, cl, ln, alinhaDireita);
                    else 
                        e.Graphics.DrawString(vTotal_aReceber.ToString("#0.00"), ft, Brushes.Black, cl, ln, alinhaDireita);
                }
                else
                {
                    ln = ln + 20F;
                    cl = 385;
                    e.Graphics.DrawString("Subtotal", ft, Brushes.Black, cl, ln);
                    cl = cl + 160;
                    e.Graphics.DrawString((vSubReceber).ToString("#0.00"), ft, Brushes.Black, cl, ln, alinhaDireita);
                    ln = ln + 12;
                    pagina++;
                    e.Graphics.DrawString(str.PadLeft(76, pad), ft, Brushes.Black, 10, 1080);
                    e.Graphics.DrawString(geral.oLicencaDe.NomeRazaoSocial, ft, Brushes.Black, 12, 1090);
                    e.Graphics.DrawString("Pagina: " + pagina, ft, Brushes.Black, 570, 1090);
                    break;
                }
            } 
            if (regs >= (dtR.Rows.Count + dt.Rows.Count - 2))
            {
                ln = ln + 20;
                cl = 355;
                e.Graphics.DrawString("Saldo total final", ft, Brushes.Black, cl, ln);
                cl = cl + 190;
                e.Graphics.DrawString((vTotal_aReceber - vTotal_aPagar - (vTotalDiversos*-1)).ToString("#0.00"), ft, Brushes.Black, cl, ln, alinhaDireita);

                pagina++;
                e.Graphics.DrawString(str.PadLeft(76, pad), ft, Brushes.Black, 10, 1080);
//                e.Graphics.DrawString(geral.oLicencaDe.NomeRazaoSocial, ft, Brushes.Black, 12, 1090);
                e.Graphics.DrawString("Pagina: " + pagina, ft, Brushes.Black, 570, 1090);
                e.HasMorePages = false;
            }
            else
            {
                e.HasMorePages = true;
            }

        }

        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            vTotal_aReceber = 0;
            vTotal_aPagar = 0;
            vTotalDiversos = 0;
            regs = 0;
            pagina = 0;
            dtR = oContasReceberDados.PegaDadosParaFluxoCaixa(Convert.ToDateTime(DataInicial.Text),
                                                              Convert.ToDateTime(DataFinal.Text), "EMABERTO",
                                                                     0, 0);
            //dt = oContasPagarDados.PreencheDataTableContasPagar(Convert.ToDateTime(DataInicial.Text),
            //                                                    Convert.ToDateTime(DataFinal.Text), "EMABERTO",
            //                                                    0, 0);
            ((Form)ppd).StartPosition = FormStartPosition.CenterScreen;
            ((Form)ppd).Text = "Visualizador";
            ((Form)ppd).WindowState = FormWindowState.Maximized;
            ppd.Document = pd;
            ppd.ShowDialog();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            regs = 0;
            pagina = 0;
            vTotal_aReceber = 0;
            vTotal_aPagar = 0;
            vTotalDiversos = 0;
  //          dtR = oContasReceberDados.PreencheDataTableContasReceber(Convert.ToDateTime(DataInicial.Text),
  //                                                                   Convert.ToDateTime(DataFinal.Text), "EMABERTO",
                                                                     0, 0);
  //          dt = oContasPagarDados.PreencheDataTableContasPagar(Convert.ToDateTime(DataInicial.Text),
  //                                                              Convert.ToDateTime(DataFinal.Text), "EMABERTO",
                                                                0, 0);
            pdialog.Document = pd;
            if (pdialog.ShowDialog() == DialogResult.OK)
            {
                pd.Print();
            }
        }

        private void frmRelatorioFluxoCaixa_Load(object sender, EventArgs e)
        {
            DataInicial.Text = "01/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
            DataFinal.Text = Convert.ToDateTime(DataInicial.Text).AddMonths(1).AddDays(5).ToShortDateString();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // frmRelatorioExecutado
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "frmRelatorioExecutado";
            this.ResumeLayout(false);

        }
    }
}
