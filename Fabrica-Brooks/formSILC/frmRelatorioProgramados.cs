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
    public partial class frmRelatorioProgramados : Form
    {
        public string DataEmissao;
        public DataTable dtProgramados = new DataTable();

        private clsProgramacaoDados oProgramacaoDados = new clsProgramacaoDados();
        private DataTable dt = new DataTable();
        private int regs = 0;
        private int pagina = 0;

        public frmRelatorioProgramados()
        {
            InitializeComponent();
            dt = dtProgramados;
        }

        private void pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float cl = 10.0F;
            float ln = 12.0F;
            if (regs != 0)
                regs--;
            Font ft = new Font("Arial", 9);
            e.Graphics.DrawString(geral.NomeEmpresa(geral.CodigoEmpresa), ft, Brushes.Black, cl, ln);

            ln = ln + 12.0F;
            string sTipo = "Programado";
            e.Graphics.DrawString("Programação de Serviços - " + sTipo + " - Data serviços: " + DataEmissao, ft, Brushes.Black, cl, ln);
            e.Graphics.DrawString("Emissão: " + DateTime.Now.ToShortDateString(), ft, Brushes.Black, 520, ln);

            ft = new Font("Arial", 7);
            ln = ln + 20.0F;
            e.Graphics.DrawString("Rp Data", ft, Brushes.Black, cl, ln);
            cl = cl + 75;
            e.Graphics.DrawString("Hora", ft, Brushes.Black, cl, ln);
            cl = cl + 50;
            e.Graphics.DrawString("Nome Fantasia Cliente", ft, Brushes.Black, cl, ln);
            cl = cl + 230;
            e.Graphics.DrawString("Solicitante", ft, Brushes.Black, cl, ln);
            cl = cl + 100;
            e.Graphics.DrawString("Resíduo", ft, Brushes.Black, cl, ln);
            cl = cl + 195;
            e.Graphics.DrawString("Data Prog", ft, Brushes.Black, cl, ln);
            cl = cl + 70;
            e.Graphics.DrawString("Serviço programado", ft, Brushes.Black, cl, ln);
            cl = cl + 200;
            e.Graphics.DrawString("Qt", ft, Brushes.Black, cl, ln);
            cl = cl + 20;
            e.Graphics.DrawString("Cam", ft, Brushes.Black, cl, ln);
            cl = cl + 36;
            e.Graphics.DrawString("Motorista", ft, Brushes.Black, cl, ln);
            cl = cl + 106;
            e.Graphics.DrawString("TP", ft, Brushes.Black, cl, ln);
            cl = cl + 20;
            e.Graphics.DrawString("Un", ft, Brushes.Black, cl, ln);

            ln = ln + 12.0F;
            string str = "";
            char pad = '─';
            cl = 10.0F;
            e.Graphics.DrawString(str.PadLeft(162, pad), ft, Brushes.Black, cl, ln);
            StringFormat alinhaDireita = new StringFormat();
            alinhaDireita.Alignment = StringAlignment.Far;
            int _NrLinhas = 640;
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                string TipoProgramacaoAnterior = dr["TipoProgramacao"].ToString()[1].ToString();
                for (int i = regs; i <= dt.Rows.Count - 1; i++)
                {
                    dr = dt.Rows[i];
                    regs++;
                    if (dt.Rows[i].RowState.ToString() != "Deleted")
                    {
                        if (ln < _NrLinhas)
                        {
                            if (TipoProgramacaoAnterior != dr["TipoProgramacao"].ToString()[1].ToString())
                            {
                                if (ln != 56)
                                {
                                    ln = ln + 12F;
                                    cl = 10.0F;
                                    e.Graphics.DrawString(str.PadLeft(162, pad), ft, Brushes.Black, cl, ln);
                                }
                            }
                            TipoProgramacaoAnterior = dr["TipoProgramacao"].ToString()[1].ToString();
                            ln = ln + 12F;
                            cl = 10.0F;
                            e.Graphics.DrawString(dr["Rp"].ToString() + "*", ft, Brushes.Black, cl, ln);
                            cl = cl + 15;
                            e.Graphics.DrawString(Convert.ToDateTime(dr["Data"]).ToString("dd/MM/yyyy"), ft, Brushes.Black, cl, ln);
                            cl = cl + 60;
                            e.Graphics.DrawString(dr["Hora"].ToString(), ft, Brushes.Black, cl, ln);
                            cl = cl + 50;
                            if (dr["NomeFantasiaCliente"].ToString().Length > 30)
                                e.Graphics.DrawString(dr["NomeFantasiaCliente"].ToString().Substring(0, 30), ft, Brushes.Black, cl, ln);
                            else
                                e.Graphics.DrawString(dr["NomeFantasiaCliente"].ToString(), ft, Brushes.Black, cl, ln);
                            cl = cl + 230;
                            e.Graphics.DrawString(dr["Solicitante"].ToString(), ft, Brushes.Black, cl, ln);
                            cl = cl + 100;
                            if (dr["ExecutarServico"].ToString().Length > 25)
                                e.Graphics.DrawString(dr["ExecutarServico"].ToString().Substring(0, 25), ft, Brushes.Black, cl, ln);
                            else
                                e.Graphics.DrawString(dr["ExecutarServico"].ToString(), ft, Brushes.Black, cl, ln);
                            cl = cl + 195;
                            e.Graphics.DrawString(Convert.ToDateTime(dr["DataProgramada"]).ToString("dd/MM/yyyy"), ft, Brushes.Black, cl, ln);
                            cl = cl + 70;
                            if (dr["HoraProgramada"].ToString().Length > 20)
                                e.Graphics.DrawString(dr["HoraProgramada"].ToString().Substring(0, 20), ft, Brushes.Black, cl, ln);
                            else
                                e.Graphics.DrawString(dr["HoraProgramada"].ToString(), ft, Brushes.Black, cl, ln);
                            cl = cl + 220;
                            e.Graphics.DrawString(dr["Quantidade"].ToString(), ft, Brushes.Black, cl, ln, alinhaDireita);
                            cl = cl + 2;
                            if (dr["ModeloCaminhao"].ToString().Length > 4)
                                e.Graphics.DrawString(dr["ModeloCaminhao"].ToString().Substring(0, 4), ft, Brushes.Black, cl, ln);
                            else
                                e.Graphics.DrawString(dr["ModeloCaminhao"].ToString(), ft, Brushes.Black, cl, ln);
                            cl = cl + 36;
                            string primeiroNome = "";
                            if (dr["NomeMotoristaOuDescricao"].ToString().IndexOf("F2 - Procura") >= 0)
                                dr["NomeMotoristaOuDescricao"] = "";
                            if (dr["NomeMotorista"].ToString().IndexOf("F2 - Procura") >= 0)
                                dr["NomeMotorista"] = "";
                            if (dr["NomeMotoristaOuDescricao"].ToString().IndexOf(" ") >= 0)
                                primeiroNome = dr["NomeMotoristaOuDescricao"].ToString().Split(" "[0])[0];
                            else
                                primeiroNome = dr["NomeMotoristaOuDescricao"].ToString();
                            if (primeiroNome == "" && dr["NomeMotorista"].ToString().IndexOf(" ") >= 0)
                                primeiroNome = dr["NomeMotorista"].ToString().Split(" "[0])[0];
                            else if (primeiroNome == "")
                                primeiroNome = dr["NomeMotorista"].ToString();
                            if (primeiroNome.Length > 17)
                                e.Graphics.DrawString(primeiroNome.Substring(0, 17), ft, Brushes.Black, cl, ln);
                            else
                                e.Graphics.DrawString(primeiroNome, ft, Brushes.Black, cl, ln);
                            cl = cl + 106;
                            e.Graphics.DrawString(dr["TipoProgramacao"].ToString(), ft, Brushes.Black, cl, ln);
                            cl = cl + 20;
                            e.Graphics.DrawString(dr["Unidade"].ToString(), ft, Brushes.Black, cl, ln);
                        }
                        else
                        {
                            ln = ln + 20F;
                            cl = 385;
                            pagina++;
                            e.Graphics.DrawString(str.PadLeft(88, pad), ft, Brushes.Black, 10, _NrLinhas + 30);
                            e.Graphics.DrawString(geral.NomeSistema, ft, Brushes.Black, 12, _NrLinhas + 40);
                            e.Graphics.DrawString("Pagina: " + pagina, ft, Brushes.Black, 570, _NrLinhas + 40);
                            e.HasMorePages = true;
                            break;
                        }
                    }
                }
            }
            if (regs >= dt.Rows.Count - 1)
            {
                ln = ln + 20F;
                cl = 385;
                pagina++;
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
        private void PreparaDados()
        {
            pagina = 0;
            regs = 0;
            dt = dtProgramados;
            foreach (DataRow _dr in dt.Rows)
            {
                if (_dr.RowState.ToString() != "Deleted")
                {
                    if (_dr["TipoProgramacao"].ToString().Length < 2)
                    {
                        if (_dr["TipoProgramacao"].ToString() == "1" || _dr["TipoProgramacao"].ToString() == "A") // Programação Automática 
                            _dr["TipoProgramacao"] = "A";
                        if (_dr["TipoProgramacao"].ToString() == "2" || _dr["TipoProgramacao"].ToString() == "M") // Programação Manual 
                            _dr["TipoProgramacao"] = "M";

                        if (_dr["StatusCor"].ToString() == "8421631")       // reprogramação vermelho
                            _dr["TipoProgramacao"] = _dr["TipoProgramacao"] + "R";
                        else if (_dr["StatusCor"].ToString() == "65535")    // cancelado
                            _dr["TipoProgramacao"] = _dr["TipoProgramacao"] + "C";
                        else if (_dr["StatusCor"].ToString() == "15000000") // antecipado
                            _dr["TipoProgramacao"] = _dr["TipoProgramacao"] + "A";
                        else if (_dr["StatusCor"].ToString() == "65280")    // antecipado
                            _dr["TipoProgramacao"] = _dr["TipoProgramacao"] + "E";
                        else
                            _dr["TipoProgramacao"] = _dr["TipoProgramacao"] + " ";

                        if (_dr["NomeMotoristaOuDescricao"].ToString() != "")
                            _dr["NomeMotorista"] = _dr["NomeMotoristaOuDescricao"];
                    }
                }
            }
        }
        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            PreparaDados();
            ((Form)ppd).StartPosition = FormStartPosition.CenterScreen;
            ((Form)ppd).Text = "Visualizador";
            ((Form)ppd).WindowState = FormWindowState.Maximized;
            pd.DefaultPageSettings.Landscape = true;
            
            ppd.Document = pd;
            ppd.ShowDialog();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            PreparaDados();
            pd.DefaultPageSettings.Landscape = true;
            pdialog.Document = pd;
            if (pdialog.ShowDialog() == DialogResult.OK)
            {
                pd.Print();
            }
        }
    }
}