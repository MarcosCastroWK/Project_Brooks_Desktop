using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibSILC;
using SILCNegocios;

namespace formSILC
{
    public partial class frmRelatorioIndicadores : Form
    {
        public string DataInicial, DataFinal;
        public int Programados;
        public DataTable dtExecutados = new DataTable();
        public DataTable dtProgramados = new DataTable();

        private clsProgramacaoDados oProgramacaoDados = new clsProgramacaoDados();
        private DataTable dt = new DataTable();        

        public frmRelatorioIndicadores()
        {
            InitializeComponent();
        }

        private void pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {            
            float cl = 10.0F;
            float ln = 12.0F;
            Font ft = new Font("Arial", 9);            

            e.Graphics.DrawString(geral.NomeEmpresa(geral.CodigoEmpresa), ft, Brushes.Black, cl, ln);

            ln = ln + 12.0F;
            string sTipo = "Mensal";
            e.Graphics.DrawString("Relatório de Indicadores - " + sTipo + " - Período de: " + DataInicial + " a " + DataFinal, ft, Brushes.Black, cl, ln);
            e.Graphics.DrawString("Emissão: " + DateTime.Now.ToShortDateString(), ft, Brushes.Black, 520, ln);
            string str = "";
            char pad = '─';
            cl = 10.0F;
            ln = ln + 12.0F;
            e.Graphics.DrawString(str.PadLeft(71, pad), ft, Brushes.Black, cl, ln);

            ImprimeQuantidades(cl, ln, ft, e);

        }

        private void ImprimeQuantidades(float cl, float ln, Font ft, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string str = "";
            char pad = '─';
            ft = new Font("Tahoma", 6);
            StringFormat alinhaDireita = new StringFormat();
            alinhaDireita.Alignment = StringAlignment.Far;

            // colocar aqui os indicadores
            clsIndicadoresDados oIndicadoresDados = new clsIndicadoresDados();
            clsIndicadores oIndicadores = new clsIndicadores();

            cl = 5;
            ln = ln + 30;

            //e.Graphics.DrawString(str.PadLeft(189, pad), ft, Brushes.Black, cl, ln);
            ln = ln + 12;
            e.Graphics.DrawString("QUANTIDADE", ft, Brushes.Black, cl, ln);
            ln = ln + 12;
            e.Graphics.DrawString(str.PadLeft(189, pad), ft, Brushes.Black, cl, ln);

            int iUltimoSolicitado = Convert.ToDateTime(DataMostragemFinal.Text).Day - 1;
            if (rdpAnual.Checked)
                iUltimoSolicitado = Convert.ToDateTime(DataMostragemInicial.Text).Year;

            ln = ln + 12;
            e.Graphics.DrawString("| Dias", ft, Brushes.Black, cl, ln);
            for (int i = 0; i <= iUltimoSolicitado; i++)
                e.Graphics.DrawString((i + 1).ToString("00") + "|", ft, Brushes.Black, 162 + ((i * 31)), ln, alinhaDireita);
            e.Graphics.DrawString("Média |", ft, Brushes.Black, 160 + ((iUltimoSolicitado * 31)), ln);
            e.Graphics.DrawString("Total  |", ft, Brushes.Black, 191 + ((iUltimoSolicitado * 31) - 2), ln);

            ln = ln + 12;
            e.Graphics.DrawString(str.PadLeft(189, pad), ft, Brushes.Black, cl, ln);

            DataTable[] _arraydtExecutados = new DataTable[31];
            DataTable[] _arraydtProgramados = new DataTable[31];
            for (int i = 0; i <= iUltimoSolicitado; i++)
            {
                if (i == 4)
                    i = 4;
                _arraydtExecutados[i] = oProgramacaoDados.PegaDados(Convert.ToInt32(Convert.ToDateTime(DataMostragemInicial.Value.AddDays(i).ToString()).ToString("yyyyMMdd")), 1, false);
                foreach (DataRow _dr in _arraydtExecutados[i].Rows)
                {
                    if (_dr["TipoProgramacao"].ToString() == "1") // Programação Automática 
                        _dr["TipoProgramacao"] = "A";
                    if (_dr["TipoProgramacao"].ToString() == "2") // Programação Manual 
                        _dr["TipoProgramacao"] = "M";
                    if (_dr["NomeMotoristaOuDescricao"].ToString() != "")
                        _dr["NomeMotorista"] = _dr["NomeMotoristaOuDescricao"];
                }
                _arraydtProgramados[i] = oProgramacaoDados.PegaDados(Convert.ToInt32(Convert.ToDateTime(DataMostragemInicial.Value.AddDays(i).ToString()).ToString("yyyyMMdd")), 2, true, "Convert(StatusCor, unsigned), Sequencial");
                foreach (DataRow _dr in _arraydtProgramados[i].Rows)
                {
                    // Programação Automática
                    if (_dr["TipoProgramacao"].ToString() == "1" || _dr["TipoProgramacao"].ToString() == "0" || _dr["TipoProgramacao"].ToString() == "")
                        _dr["TipoProgramacao"] = "A";
                    // Programação Manual
                    if (_dr["TipoProgramacao"].ToString() == "2" || _dr["StatusCor"].ToString() == geral.RetornaCodigoCor("Cinza"))
                        _dr["TipoProgramacao"] = "M";
                }
            }
            decimal _totalx = 0;
            for (int i = 0; i <= iUltimoSolicitado; i++)
            {
                if (i == 4)
                    i = 4;
                oIndicadoresDados = new clsIndicadoresDados();
                oIndicadores = new clsIndicadores();
                dtExecutados = _arraydtExecutados[i];
                dtProgramados = _arraydtProgramados[i];                
                oIndicadoresDados.CalculaIndicadores(oIndicadores, dtExecutados, dtProgramados, Convert.ToDateTime(DataMostragemInicial.Value.AddDays(i).ToString()).ToString("dd/MM/yyyy"));
                _totalx = _totalx + oIndicadores.TotalProgramado;
                if (i == 0)
                {
                    ln = ln + 12;
                    e.Graphics.DrawString("TOTAL GERAL PROGRAMADO: ", ft, Brushes.Black, 5, ln);
                    e.Graphics.DrawString(oIndicadores.TotalProgramado.ToString() + "|", ft, Brushes.Black, 162, ln, alinhaDireita);
                }
                else
                    e.Graphics.DrawString(oIndicadores.TotalProgramado.ToString() + "|", ft, Brushes.Black, 162 + ((i * 31)), ln, alinhaDireita);
            }
            e.Graphics.DrawString((_totalx / (iUltimoSolicitado + 1)).ToString("N2") + "|", ft, Brushes.Black, 160 + (((iUltimoSolicitado + 1) * 31) - 4), ln, alinhaDireita);
            e.Graphics.DrawString(_totalx.ToString() + "|", ft, Brushes.Black, 191 + (((iUltimoSolicitado + 1) * 31) - 2), ln, alinhaDireita);

            _totalx = 0;
            for (int i = 0; i <= iUltimoSolicitado; i++)
            {
                oIndicadoresDados = new clsIndicadoresDados();
                oIndicadores = new clsIndicadores();
                dtExecutados = _arraydtExecutados[i];
                dtProgramados = _arraydtProgramados[i];
                oIndicadoresDados.CalculaIndicadores(oIndicadores, dtExecutados, dtProgramados, Convert.ToDateTime(DataMostragemInicial.Value.AddDays(i).ToString()).ToString("dd/MM/yyyy"));
                _totalx = _totalx + oIndicadores.TotalProgramadoAutomatico;
                if (i == 0)
                {
                    ln = ln + 12;
                    e.Graphics.DrawString("  AUTOMÁTICO: ", ft, Brushes.Black, 5, ln);
                    e.Graphics.DrawString(oIndicadores.TotalProgramadoAutomatico.ToString() + "|", ft, Brushes.Black, 162, ln, alinhaDireita);
                }
                else
                    e.Graphics.DrawString(oIndicadores.TotalProgramadoAutomatico.ToString() + "|", ft, Brushes.Black, 162 + (i * 31), ln, alinhaDireita);
            }
            e.Graphics.DrawString((_totalx / (iUltimoSolicitado + 1)).ToString("N2") + "|", ft, Brushes.Black, 160 + (((iUltimoSolicitado + 1) * 31) - 4), ln, alinhaDireita);
            e.Graphics.DrawString(_totalx.ToString() + "|", ft, Brushes.Black, 191 + (((iUltimoSolicitado + 1) * 31) - 2), ln, alinhaDireita);

            _totalx = 0;
            for (int i = 0; i <= iUltimoSolicitado; i++)
            {
                oIndicadoresDados = new clsIndicadoresDados();
                oIndicadores = new clsIndicadores();
                dtExecutados = _arraydtExecutados[i];
                dtProgramados = _arraydtProgramados[i];
                oIndicadoresDados.CalculaIndicadores(oIndicadores, dtExecutados, dtProgramados, Convert.ToDateTime(DataMostragemInicial.Value.AddDays(i).ToString()).ToString("dd/MM/yyyy"));
                _totalx = _totalx + oIndicadores.TotalProgramadoManual;
                if (i == 0)
                {
                    ln = ln + 12;
                    e.Graphics.DrawString("  MANUAL:", ft, Brushes.Black, 5, ln);
                    e.Graphics.DrawString(oIndicadores.TotalProgramadoManual.ToString() + "|", ft, Brushes.Black, 162, ln, alinhaDireita);
                }
                else
                    e.Graphics.DrawString(oIndicadores.TotalProgramadoManual.ToString() + "|", ft, Brushes.Black, 162 + (i * 31), ln, alinhaDireita);
            }
            e.Graphics.DrawString((_totalx / (iUltimoSolicitado + 1)).ToString("N2") + "|", ft, Brushes.Black, 160 + (((iUltimoSolicitado + 1) * 31) - 4), ln, alinhaDireita);
            e.Graphics.DrawString(_totalx.ToString() + "|", ft, Brushes.Black, 191 + (((iUltimoSolicitado + 1) * 31) - 2), ln, alinhaDireita);

            _totalx = 0;
            ln = ln + 12;
            e.Graphics.DrawString(str.PadLeft(189, pad), ft, Brushes.Black, cl, ln);
            for (int i = 0; i <= iUltimoSolicitado; i++)
            {
                oIndicadoresDados = new clsIndicadoresDados();
                oIndicadores = new clsIndicadores();
                dtExecutados = _arraydtExecutados[i];
                dtProgramados = _arraydtProgramados[i];
                oIndicadoresDados.CalculaIndicadores(oIndicadores, dtExecutados, dtProgramados, Convert.ToDateTime(DataMostragemInicial.Value.AddDays(i).ToString()).ToString("dd/MM/yyyy"));

                _totalx = _totalx + oIndicadores.TotalExecutado;
                if (i == 0)
                {
                    ln = ln + 12;
                    e.Graphics.DrawString("TOTAL GERAL EXECUTADO: ", ft, Brushes.Black, 5, ln);
                    e.Graphics.DrawString(oIndicadores.TotalExecutado.ToString() + "|", ft, Brushes.Black, 162, ln, alinhaDireita);
                }
                else
                    e.Graphics.DrawString(oIndicadores.TotalExecutado.ToString() + "|", ft, Brushes.Black, 162 + (i * 31), ln, alinhaDireita);
            }
            e.Graphics.DrawString((_totalx / (iUltimoSolicitado + 1)).ToString("N2") + "|", ft, Brushes.Black, 160 + (((iUltimoSolicitado + 1) * 31) - 4), ln, alinhaDireita);
            e.Graphics.DrawString(_totalx.ToString() + "|", ft, Brushes.Black, 191 + (((iUltimoSolicitado + 1) * 31) - 2), ln, alinhaDireita);

            _totalx = 0;
            for (int i = 0; i <= iUltimoSolicitado; i++)
            {
                oIndicadoresDados = new clsIndicadoresDados();
                oIndicadores = new clsIndicadores();
                dtExecutados = _arraydtExecutados[i];
                dtProgramados = _arraydtProgramados[i];
                oIndicadoresDados.CalculaIndicadores(oIndicadores, dtExecutados, dtProgramados, Convert.ToDateTime(DataMostragemInicial.Value.AddDays(i).ToString()).ToString("dd/MM/yyyy"));
                _totalx = _totalx + oIndicadores.TotalExecutadoAutomatico;
                if (i == 0)
                {
                    ln = ln + 12;
                    e.Graphics.DrawString("  AUTOMÁTICO: ", ft, Brushes.Black, 5, ln);
                    e.Graphics.DrawString(oIndicadores.TotalExecutadoAutomatico.ToString() + "|", ft, Brushes.Black, 162, ln, alinhaDireita);
                }
                else
                    e.Graphics.DrawString(oIndicadores.TotalExecutadoAutomatico.ToString() + "|", ft, Brushes.Black, 162 + (i * 31), ln, alinhaDireita);
            }
            e.Graphics.DrawString((_totalx / (iUltimoSolicitado + 1)).ToString("N2") + "|", ft, Brushes.Black, 160 + (((iUltimoSolicitado + 1) * 31) - 4), ln, alinhaDireita);
            e.Graphics.DrawString(_totalx.ToString() + "|", ft, Brushes.Black, 191 + (((iUltimoSolicitado + 1) * 31) - 2), ln, alinhaDireita);

            _totalx = 0;
            for (int i = 0; i <= iUltimoSolicitado; i++)
            {
                oIndicadoresDados = new clsIndicadoresDados();
                oIndicadores = new clsIndicadores();
                dtExecutados = _arraydtExecutados[i];
                dtProgramados = _arraydtProgramados[i];
                oIndicadoresDados.CalculaIndicadores(oIndicadores, dtExecutados, dtProgramados, Convert.ToDateTime(DataMostragemInicial.Value.AddDays(i).ToString()).ToString("dd/MM/yyyy"));
                _totalx = _totalx + oIndicadores.TotalExecutadoManual;
                if (i == 0)
                {
                    ln = ln + 12;
                    e.Graphics.DrawString("  MANUAL: ", ft, Brushes.Black, 5, ln);
                    e.Graphics.DrawString(oIndicadores.TotalExecutadoManual.ToString() + "|", ft, Brushes.Black, 162, ln, alinhaDireita);
                }
                else
                    e.Graphics.DrawString(oIndicadores.TotalExecutadoManual.ToString() + "|", ft, Brushes.Black, 162 + (i * 31), ln, alinhaDireita);
            }
            e.Graphics.DrawString((_totalx / (iUltimoSolicitado + 1)).ToString("N2") + "|", ft, Brushes.Black, 160 + (((iUltimoSolicitado + 1) * 31) - 4), ln, alinhaDireita);
            e.Graphics.DrawString(_totalx.ToString() + "|", ft, Brushes.Black, 191 + (((iUltimoSolicitado + 1) * 31) - 2), ln, alinhaDireita);

            _totalx = 0;
            ln = ln + 12;
            e.Graphics.DrawString(str.PadLeft(189, pad), ft, Brushes.Black, cl, ln);
            for (int i = 0; i <= iUltimoSolicitado; i++)
            {
                oIndicadoresDados = new clsIndicadoresDados();
                oIndicadores = new clsIndicadores();
                dtExecutados = _arraydtExecutados[i];
                dtProgramados = _arraydtProgramados[i];
                oIndicadoresDados.CalculaIndicadores(oIndicadores, dtExecutados, dtProgramados, Convert.ToDateTime(DataMostragemInicial.Value.AddDays(i).ToString()).ToString("dd/MM/yyyy"));
                _totalx = _totalx + oIndicadores.TotalCancelado;
                if (i == 0)
                {
                    ln = ln + 12;
                    e.Graphics.DrawString("TOTAL GERAL CANCELADO: ", ft, Brushes.Black, 5, ln);
                    e.Graphics.DrawString(oIndicadores.TotalCancelado.ToString() + "|", ft, Brushes.Black, 162, ln, alinhaDireita);
                }
                else
                    e.Graphics.DrawString(oIndicadores.TotalCancelado.ToString() + "|", ft, Brushes.Black, 162 + (i * 31), ln, alinhaDireita);
            }
            e.Graphics.DrawString((_totalx / (iUltimoSolicitado + 1)).ToString("N2") + "|", ft, Brushes.Black, 160 + (((iUltimoSolicitado + 1) * 31) - 4), ln, alinhaDireita);
            e.Graphics.DrawString(_totalx.ToString() + "|", ft, Brushes.Black, 191 + (((iUltimoSolicitado + 1) * 31) - 2), ln, alinhaDireita);

            _totalx = 0;
            for (int i = 0; i <= iUltimoSolicitado; i++)
            {
                oIndicadoresDados = new clsIndicadoresDados();
                oIndicadores = new clsIndicadores();
                dtExecutados = _arraydtExecutados[i];
                dtProgramados = _arraydtProgramados[i];
                oIndicadoresDados.CalculaIndicadores(oIndicadores, dtExecutados, dtProgramados, Convert.ToDateTime(DataMostragemInicial.Value.AddDays(i).ToString()).ToString("dd/MM/yyyy"));
                _totalx = _totalx + oIndicadores.TotalCanceladoCliente;
                if (i == 0)
                {
                    ln = ln + 12;
                    e.Graphics.DrawString("  PELO CLIENTE: ", ft, Brushes.Black, 5, ln);
                    e.Graphics.DrawString(oIndicadores.TotalCanceladoCliente.ToString() + "|", ft, Brushes.Black, 162, ln, alinhaDireita);
                }
                else
                    e.Graphics.DrawString(oIndicadores.TotalCanceladoCliente.ToString() + "|", ft, Brushes.Black, 162 + (i * 31), ln, alinhaDireita);
            }
            e.Graphics.DrawString((_totalx / (iUltimoSolicitado + 1)).ToString("N2") + "|", ft, Brushes.Black, 160 + (((iUltimoSolicitado + 1) * 31) - 4), ln, alinhaDireita);
            e.Graphics.DrawString(_totalx.ToString() + "|", ft, Brushes.Black, 191 + (((iUltimoSolicitado + 1) * 31) - 2), ln, alinhaDireita);

            _totalx = 0;
            ln = ln + 12;
            e.Graphics.DrawString(str.PadLeft(189, pad), ft, Brushes.Black, cl, ln);
            for (int i = 0; i <= iUltimoSolicitado; i++)
            {
                oIndicadoresDados = new clsIndicadoresDados();
                oIndicadores = new clsIndicadores();
                dtExecutados = _arraydtExecutados[i];
                dtProgramados = _arraydtProgramados[i];
                oIndicadoresDados.CalculaIndicadores(oIndicadores, dtExecutados, dtProgramados, Convert.ToDateTime(DataMostragemInicial.Value.AddDays(i).ToString()).ToString("dd/MM/yyyy"));
                _totalx = _totalx + oIndicadores.TotalReprog;
                if (i == 0)
                {
                    ln = ln + 12;
                    e.Graphics.DrawString("TOTAL REPROGRAMADO: ", ft, Brushes.Black, 5, ln);
                    e.Graphics.DrawString(oIndicadores.TotalReprog.ToString() + "|", ft, Brushes.Black, 162, ln, alinhaDireita);
                }
                else
                    e.Graphics.DrawString(oIndicadores.TotalReprog.ToString() + "|", ft, Brushes.Black, 162 + (i * 31), ln, alinhaDireita);
            }
            e.Graphics.DrawString((_totalx / (iUltimoSolicitado + 1)).ToString("N2") + "|", ft, Brushes.Black, 160 + (((iUltimoSolicitado + 1) * 31) - 4), ln, alinhaDireita);
            e.Graphics.DrawString(_totalx.ToString() + "|", ft, Brushes.Black, 191 + (((iUltimoSolicitado + 1) * 31) - 2), ln, alinhaDireita);

            _totalx = 0;
            for (int i = 0; i <= iUltimoSolicitado; i++)
            {
                oIndicadoresDados = new clsIndicadoresDados();
                oIndicadores = new clsIndicadores();
                dtExecutados = _arraydtExecutados[i];
                dtProgramados = _arraydtProgramados[i];
                oIndicadoresDados.CalculaIndicadores(oIndicadores, dtExecutados, dtProgramados, Convert.ToDateTime(DataMostragemInicial.Value.AddDays(i).ToString()).ToString("dd/MM/yyyy"));
                _totalx = _totalx + oIndicadores.TotalReprogImpossivelAtender;
                if (i == 0)
                {
                    ln = ln + 12;
                    e.Graphics.DrawString("  IMPOSS.DE ATENDIMENTO: ", ft, Brushes.Black, 5, ln);
                    e.Graphics.DrawString(oIndicadores.TotalReprogImpossivelAtender.ToString() + "|", ft, Brushes.Black, 162, ln, alinhaDireita);
                }
                else
                    e.Graphics.DrawString(oIndicadores.TotalReprogImpossivelAtender.ToString() + "|", ft, Brushes.Black, 162 + (i * 31), ln, alinhaDireita);
            }
            e.Graphics.DrawString((_totalx / (iUltimoSolicitado + 1)).ToString("N2") + "|", ft, Brushes.Black, 160 + (((iUltimoSolicitado + 1) * 31) - 4), ln, alinhaDireita);
            e.Graphics.DrawString(_totalx.ToString() + "|", ft, Brushes.Black, 191 + (((iUltimoSolicitado + 1) * 31) - 2), ln, alinhaDireita);

            _totalx = 0;
            for (int i = 0; i <= iUltimoSolicitado; i++)
            {
                oIndicadoresDados = new clsIndicadoresDados();
                oIndicadores = new clsIndicadores();
                dtExecutados = _arraydtExecutados[i];
                dtProgramados = _arraydtProgramados[i];
                oIndicadoresDados.CalculaIndicadores(oIndicadores, dtExecutados, dtProgramados, Convert.ToDateTime(DataMostragemInicial.Value.AddDays(i).ToString()).ToString("dd/MM/yyyy"));
                _totalx = _totalx + oIndicadores.TotalReprogFlexibilidade;
                if (i == 0)
                {
                    ln = ln + 12;
                    e.Graphics.DrawString("  FLEXIBILIDAD CONTRATO: ", ft, Brushes.Black, 5, ln);
                    e.Graphics.DrawString(oIndicadores.TotalReprogFlexibilidade.ToString() + "|", ft, Brushes.Black, 162, ln, alinhaDireita);
                }
                else
                    e.Graphics.DrawString(oIndicadores.TotalReprogFlexibilidade.ToString() + "|", ft, Brushes.Black, 162 + (i * 31), ln, alinhaDireita);
            }
            e.Graphics.DrawString((_totalx / (iUltimoSolicitado + 1)).ToString("N2") + "|", ft, Brushes.Black, 160 + (((iUltimoSolicitado + 1) * 31) - 4), ln, alinhaDireita);
            e.Graphics.DrawString(_totalx.ToString() + "|", ft, Brushes.Black, 191 + (((iUltimoSolicitado + 1) * 31) - 2), ln, alinhaDireita);

            _totalx = 0;
            for (int i = 0; i <= iUltimoSolicitado; i++)
            {
                oIndicadoresDados = new clsIndicadoresDados();
                oIndicadores = new clsIndicadores();
                dtExecutados = _arraydtExecutados[i];
                dtProgramados = _arraydtProgramados[i];
                oIndicadoresDados.CalculaIndicadores(oIndicadores, dtExecutados, dtProgramados, Convert.ToDateTime(DataMostragemInicial.Value.AddDays(i).ToString()).ToString("dd/MM/yyyy"));
                _totalx = _totalx + oIndicadores.TotalReprogSolicitacaoCliente;
                if (i == 0)
                {
                    ln = ln + 12;
                    e.Graphics.DrawString("  SOLICITAÇÃO DO CLIENTE: ", ft, Brushes.Black, 5, ln);
                    e.Graphics.DrawString(oIndicadores.TotalReprogSolicitacaoCliente.ToString() + "|", ft, Brushes.Black, 162, ln, alinhaDireita);
                }
                else
                    e.Graphics.DrawString(oIndicadores.TotalReprogSolicitacaoCliente.ToString() + "|", ft, Brushes.Black, 162 + (i * 31), ln, alinhaDireita);
            }
            e.Graphics.DrawString((_totalx / (iUltimoSolicitado + 1)).ToString("N2") + "|", ft, Brushes.Black, 160 + (((iUltimoSolicitado + 1) * 31) - 4), ln, alinhaDireita);
            e.Graphics.DrawString(_totalx.ToString() + "|", ft, Brushes.Black, 191 + (((iUltimoSolicitado + 1) * 31) - 2), ln, alinhaDireita);

            _totalx = 0;
            for (int i = 0; i <= iUltimoSolicitado; i++)
            {
                oIndicadoresDados = new clsIndicadoresDados();
                oIndicadores = new clsIndicadores();
                dtExecutados = _arraydtExecutados[i];
                dtProgramados = _arraydtProgramados[i];
                oIndicadoresDados.CalculaIndicadores(oIndicadores, dtExecutados, dtProgramados, Convert.ToDateTime(DataMostragemInicial.Value.AddDays(i).ToString()).ToString("dd/MM/yyyy"));
                _totalx = _totalx + oIndicadores.TotalReprogOutros;
                if (i == 0)
                {
                    ln = ln + 12;
                    e.Graphics.DrawString("  OUTROS: ", ft, Brushes.Black, 5, ln);
                    e.Graphics.DrawString(oIndicadores.TotalReprogOutros.ToString() + "|", ft, Brushes.Black, 162, ln, alinhaDireita);
                }
                else
                    e.Graphics.DrawString(oIndicadores.TotalReprogOutros.ToString() + "|", ft, Brushes.Black, 162 + (i * 31), ln, alinhaDireita);
            }
            e.Graphics.DrawString((_totalx / (iUltimoSolicitado + 1)).ToString("N2") + "|", ft, Brushes.Black, 160 + (((iUltimoSolicitado + 1) * 31) - 4), ln, alinhaDireita);
            e.Graphics.DrawString(_totalx.ToString() + "|", ft, Brushes.Black, 191 + (((iUltimoSolicitado + 1) * 31) - 2), ln, alinhaDireita);

            _totalx = 0;
            ln = ln + 12;
            e.Graphics.DrawString(str.PadLeft(189, pad), ft, Brushes.Black, cl, ln);
            for (int i = 0; i <= iUltimoSolicitado; i++)
            {
                oIndicadoresDados = new clsIndicadoresDados();
                oIndicadores = new clsIndicadores();
                dtExecutados = _arraydtExecutados[i];
                dtProgramados = _arraydtProgramados[i];
                oIndicadoresDados.CalculaIndicadores(oIndicadores, dtExecutados, dtProgramados, Convert.ToDateTime(DataMostragemInicial.Value.AddDays(i).ToString()).ToString("dd/MM/yyyy"));
                _totalx = _totalx + oIndicadores.TotalBloqueioFinanc;
                if (i == 0)
                {
                    ln = ln + 12;
                    e.Graphics.DrawString("TOTAL BLOQUEIO FINANCEIRO: ", ft, Brushes.Black, 5, ln);
                    e.Graphics.DrawString(oIndicadores.TotalBloqueioFinanc.ToString() + "|", ft, Brushes.Black, 162, ln, alinhaDireita);
                }
                else
                    e.Graphics.DrawString(oIndicadores.TotalBloqueioFinanc.ToString() + "|", ft, Brushes.Black, 162 + (i * 31), ln, alinhaDireita);
            }
            e.Graphics.DrawString((_totalx / (iUltimoSolicitado + 1)).ToString("N2") + "|", ft, Brushes.Black, 160 + (((iUltimoSolicitado + 1) * 31) - 4), ln, alinhaDireita);
            e.Graphics.DrawString(_totalx.ToString() + "|", ft, Brushes.Black, 191 + (((iUltimoSolicitado + 1) * 31) - 2), ln, alinhaDireita);

            _totalx = 0;
            ln = ln + 12;
            e.Graphics.DrawString(str.PadLeft(189, pad), ft, Brushes.Black, cl, ln);
            ln = ln + 18;
            e.Graphics.DrawString("INDICADORES", ft, Brushes.Black, cl, ln);
            ln = ln + 18;
            e.Graphics.DrawString(str.PadLeft(189, pad), ft, Brushes.Black, cl, ln);
            for (int i = 0; i <= iUltimoSolicitado; i++)
            {
                oIndicadoresDados = new clsIndicadoresDados();
                oIndicadores = new clsIndicadores();
                dtExecutados = _arraydtExecutados[i];
                dtProgramados = _arraydtProgramados[i];
                oIndicadoresDados.CalculaIndicadores(oIndicadores, dtExecutados, dtProgramados, Convert.ToDateTime(DataMostragemInicial.Value.AddDays(i).ToString()).ToString("dd/MM/yyyy"));
                _totalx = _totalx + oIndicadores.Cumprimento;
                if (i == 0)
                {
                    ln = ln + 18;
                    e.Graphics.DrawString("INDIC EXECUÇÃO PROGR: ", ft, Brushes.Black, 5, ln);
                    e.Graphics.DrawString(oIndicadores.Cumprimento.ToString("n2") + "|", ft, Brushes.Black, 162, ln, alinhaDireita);
                }
                else
                    e.Graphics.DrawString(oIndicadores.Cumprimento.ToString("n2") + "|", ft, Brushes.Black, 162 + (i * 31), ln, alinhaDireita);
            }
            e.Graphics.DrawString((_totalx / (iUltimoSolicitado + 1)).ToString("N2") + "|", ft, Brushes.Black, 160 + (((iUltimoSolicitado + 1) * 31) - 4), ln, alinhaDireita);

            _totalx = 0;
            ln = ln + 18;
            e.Graphics.DrawString(str.PadLeft(189, pad), ft, Brushes.Black, cl, ln);
            for (int i = 0; i <= iUltimoSolicitado; i++)
            {
                oIndicadoresDados = new clsIndicadoresDados();
                oIndicadores = new clsIndicadores();
                dtExecutados = _arraydtExecutados[i];
                dtProgramados = _arraydtProgramados[i];
                oIndicadoresDados.CalculaIndicadores(oIndicadores, dtExecutados, dtProgramados, Convert.ToDateTime(DataMostragemInicial.Value.AddDays(i).ToString()).ToString("dd/MM/yyyy"));
                _totalx = _totalx + oIndicadores.Cancelamento;
                if (i == 0)
                {
                    ln = ln + 18;
                    e.Graphics.DrawString("INDICE DE CANCELAMENTO: ", ft, Brushes.Black, 5, ln);
                    e.Graphics.DrawString(oIndicadores.Cancelamento.ToString("n2") + "|", ft, Brushes.Black, 162, ln, alinhaDireita);
                }
                else
                    e.Graphics.DrawString(oIndicadores.Cancelamento.ToString("n2") + "|", ft, Brushes.Black, 162 + (i * 31), ln, alinhaDireita);
            }
            e.Graphics.DrawString((_totalx / (iUltimoSolicitado + 1)).ToString("N2") + "|", ft, Brushes.Black, 160 + (((iUltimoSolicitado + 1) * 31) - 4), ln, alinhaDireita);

            _totalx = 0;
            ln = ln + 18;
            e.Graphics.DrawString(str.PadLeft(189, pad), ft, Brushes.Black, cl, ln);
            decimal indReprog = 0;
            for (int i = 0; i <= iUltimoSolicitado; i++)
            {
                oIndicadoresDados = new clsIndicadoresDados();
                oIndicadores = new clsIndicadores();
                dtExecutados = _arraydtExecutados[i];
                dtProgramados = _arraydtProgramados[i];
                oIndicadoresDados.CalculaIndicadores(oIndicadores, dtExecutados, dtProgramados, Convert.ToDateTime(DataMostragemInicial.Value.AddDays(i).ToString()).ToString("dd/MM/yyyy"));
                indReprog = 0;
                if (oIndicadores.TotalProgramado > 0)
                    indReprog = oIndicadores.TotalReprog / oIndicadores.TotalProgramado * 100;

                if (oIndicadores.TotalProgramado > 0)
                    oIndicadores.IndiceReprogOutros = oIndicadores.TotalReprogOutros / oIndicadores.TotalProgramado * 100;

                _totalx = _totalx + indReprog;
                if (i == 0)
                {
                    ln = ln + 18;
                    e.Graphics.DrawString("INDICE DE REPROGRAMAÇÃO : ", ft, Brushes.Black, 5, ln);
                    e.Graphics.DrawString(indReprog.ToString("n2") + "|", ft, Brushes.Black, 162, ln, alinhaDireita);
                }
                else
                {
                    e.Graphics.DrawString(indReprog.ToString("n2") + "|", ft, Brushes.Black, 162 + (i * 31), ln, alinhaDireita);
                }
            }
            e.Graphics.DrawString((_totalx / (iUltimoSolicitado + 1)).ToString("N2") + "|", ft, Brushes.Black, 160 + (((iUltimoSolicitado + 1) * 31) - 4), ln, alinhaDireita);

            _totalx = 0;
            for (int i = 0; i <= iUltimoSolicitado; i++)
            {
                oIndicadoresDados = new clsIndicadoresDados();
                oIndicadores = new clsIndicadores();
                dtExecutados = _arraydtExecutados[i];
                dtProgramados = _arraydtProgramados[i];
                oIndicadoresDados.CalculaIndicadores(oIndicadores, dtExecutados, dtProgramados, Convert.ToDateTime(DataMostragemInicial.Value.AddDays(i).ToString()).ToString("dd/MM/yyyy"));
                _totalx = _totalx + oIndicadores.ImpossivelAtender;
                if (i == 0)
                {
                    ln = ln + 12;
                    e.Graphics.DrawString("  IMPOSSIBILIDAD ATENDIMENTO: ", ft, Brushes.Black, 5, ln);
                    e.Graphics.DrawString(oIndicadores.ImpossivelAtender.ToString("n2") + "|", ft, Brushes.Black, 162, ln, alinhaDireita);
                }
                else
                    e.Graphics.DrawString(oIndicadores.ImpossivelAtender.ToString("n2") + "|", ft, Brushes.Black, 162 + (i * 31), ln, alinhaDireita);
            }
            e.Graphics.DrawString((_totalx / (iUltimoSolicitado + 1)).ToString("N2") + "|", ft, Brushes.Black, 160 + (((iUltimoSolicitado + 1) * 31) - 4), ln, alinhaDireita);

            _totalx = 0;
            for (int i = 0; i <= iUltimoSolicitado; i++)
            {
                oIndicadoresDados = new clsIndicadoresDados();
                oIndicadores = new clsIndicadores();
                dtExecutados = _arraydtExecutados[i];
                dtProgramados = _arraydtProgramados[i];
                oIndicadoresDados.CalculaIndicadores(oIndicadores, dtExecutados, dtProgramados, Convert.ToDateTime(DataMostragemInicial.Value.AddDays(i).ToString()).ToString("dd/MM/yyyy"));
                _totalx = _totalx + oIndicadores.FlexibilidadeContrato;
                if (i == 0)
                {
                    ln = ln + 12;
                    e.Graphics.DrawString("  FLEXIBILIDADE CONTRATO: ", ft, Brushes.Black, 5, ln);
                    e.Graphics.DrawString(oIndicadores.FlexibilidadeContrato.ToString("n2") + "|", ft, Brushes.Black, 162, ln, alinhaDireita);
                }
                else
                    e.Graphics.DrawString(oIndicadores.FlexibilidadeContrato.ToString("n2") + "|", ft, Brushes.Black, 162 + (i * 31), ln, alinhaDireita);
            }
            e.Graphics.DrawString((_totalx / (iUltimoSolicitado + 1)).ToString("N2") + "|", ft, Brushes.Black, 160 + (((iUltimoSolicitado + 1) * 31) - 4), ln, alinhaDireita);

            _totalx = 0;
            for (int i = 0; i <= iUltimoSolicitado; i++)
            {
                oIndicadoresDados = new clsIndicadoresDados();
                oIndicadores = new clsIndicadores();
                dtExecutados = _arraydtExecutados[i];
                dtProgramados = _arraydtProgramados[i];
                oIndicadoresDados.CalculaIndicadores(oIndicadores, dtExecutados, dtProgramados, Convert.ToDateTime(DataMostragemInicial.Value.AddDays(i).ToString()).ToString("dd/MM/yyyy"));
                _totalx = _totalx + oIndicadores.SolicitacaoCliente;
                if (i == 0)
                {
                    ln = ln + 12;
                    e.Graphics.DrawString("  SOLICITAÇÃO DO CLIENTE: ", ft, Brushes.Black, 5, ln);
                    e.Graphics.DrawString(oIndicadores.SolicitacaoCliente.ToString("n2") + "|", ft, Brushes.Black, 162, ln, alinhaDireita);
                }
                else
                    e.Graphics.DrawString(oIndicadores.SolicitacaoCliente.ToString("n2") + "|", ft, Brushes.Black, 162 + (i * 31), ln, alinhaDireita);
            }
            e.Graphics.DrawString((_totalx / (iUltimoSolicitado + 1)).ToString("N2") + "|", ft, Brushes.Black, 160 + (((iUltimoSolicitado + 1) * 31) - 4), ln, alinhaDireita);

            //_totalx = 0;
            //for (int i = 0; i <= iUltimoSolicitado; i++)
            //{
            //    oIndicadoresDados = new clsIndicadoresDados();
            //    oIndicadores = new clsIndicadores();
            //    dtExecutados = _arraydtExecutados[i];
            //    dtProgramados = _arraydtProgramados[i];
            //    oIndicadoresDados.CalculaIndicadores(oIndicadores, dtExecutados, dtProgramados, Convert.ToDateTime(DataMostragemInicial.Value.AddDays(i).ToString()).ToString("dd/MM/yyyy"));
            //    _totalx = _totalx + oIndicadores.BloqueioFinanceiro;
            //    if (i == 0)
            //    {
            //        ln = ln + 12;
            //        e.Graphics.DrawString("  REPR.C/BLOQUEIO FINANCEIRO: ", ft, Brushes.Black, 5, ln);
            //        e.Graphics.DrawString(oIndicadores.BloqueioFinanceiro.ToString("n2") + "|", ft, Brushes.Black, 162, ln, alinhaDireita);
            //    }
            //    else
            //        e.Graphics.DrawString(oIndicadores.BloqueioFinanceiro.ToString("n2") + "|", ft, Brushes.Black, 162 + (i * 31), ln, alinhaDireita);
            //}
            //e.Graphics.DrawString((_totalx / (iUltimoSolicitado + 1)).ToString("N2") + "|", ft, Brushes.Black, 160 + (((iUltimoSolicitado + 1) * 31) - 4), ln, alinhaDireita);

            _totalx = 0;
            for (int i = 0; i <= iUltimoSolicitado; i++)
            {
                oIndicadoresDados = new clsIndicadoresDados();
                oIndicadores = new clsIndicadores();
                dtExecutados = _arraydtExecutados[i];
                dtProgramados = _arraydtProgramados[i];
                oIndicadoresDados.CalculaIndicadores(oIndicadores, dtExecutados, dtProgramados, Convert.ToDateTime(DataMostragemInicial.Value.AddDays(i).ToString()).ToString("dd/MM/yyyy"));
                _totalx = _totalx + oIndicadores.IndiceReprogOutros;
                if (i == 0)
                {
                    ln = ln + 12;
                    e.Graphics.DrawString("  REPROG OUTROS: ", ft, Brushes.Black, 5, ln);
                    e.Graphics.DrawString(oIndicadores.IndiceReprogOutros.ToString("n2") + "|", ft, Brushes.Black, 162, ln, alinhaDireita);
                }
                else
                    e.Graphics.DrawString(oIndicadores.IndiceReprogOutros.ToString("n2") + "|", ft, Brushes.Black, 162 + (i * 31), ln, alinhaDireita);
            }
            e.Graphics.DrawString((_totalx / (iUltimoSolicitado + 1)).ToString("N2") + "|", ft, Brushes.Black, 160 + (((iUltimoSolicitado + 1) * 31) - 4), ln, alinhaDireita);

            _totalx = 0;
            ln = ln + 18;
            e.Graphics.DrawString(str.PadLeft(189, pad), ft, Brushes.Black, cl, ln);
            for (int i = 0; i <= iUltimoSolicitado; i++)
            {
                oIndicadoresDados = new clsIndicadoresDados();
                oIndicadores = new clsIndicadores();
                dtExecutados = _arraydtExecutados[i];
                dtProgramados = _arraydtProgramados[i];
                oIndicadoresDados.CalculaIndicadores(oIndicadores, dtExecutados, dtProgramados, Convert.ToDateTime(DataMostragemInicial.Value.AddDays(i).ToString()).ToString("dd/MM/yyyy"));
                _totalx = _totalx + oIndicadores.ProgramacaoAutomatica;
                if (i == 0)
                {
                    ln = ln + 18;
                    e.Graphics.DrawString("INDICE PROGR. AUTOMÁTICA: ", ft, Brushes.Black, 5, ln);
                    e.Graphics.DrawString(oIndicadores.ProgramacaoAutomatica.ToString("n2") + "|", ft, Brushes.Black, 162, ln, alinhaDireita);
                }
                else
                    e.Graphics.DrawString(oIndicadores.ProgramacaoAutomatica.ToString("n2") + "|", ft, Brushes.Black, 162 + (i * 31), ln, alinhaDireita);
            }
            e.Graphics.DrawString((_totalx / (iUltimoSolicitado + 1)).ToString("N2") + "|", ft, Brushes.Black, 160 + (((iUltimoSolicitado + 1) * 31) - 4), ln, alinhaDireita);

            _totalx = 0;
            for (int i = 0; i <= iUltimoSolicitado; i++)
            {
                oIndicadoresDados = new clsIndicadoresDados();
                oIndicadores = new clsIndicadores();
                dtExecutados = _arraydtExecutados[i];
                dtProgramados = _arraydtProgramados[i];
                oIndicadoresDados.CalculaIndicadores(oIndicadores, dtExecutados, dtProgramados, Convert.ToDateTime(DataMostragemInicial.Value.AddDays(i).ToString()).ToString("dd/MM/yyyy"));
                _totalx = _totalx + oIndicadores.CumprimentoManual;
                if (i == 0)
                {
                    ln = ln + 12;
                    e.Graphics.DrawString("INDICE EXECUÇÃO MANUAL: ", ft, Brushes.Black, 5, ln);
                    e.Graphics.DrawString(oIndicadores.CumprimentoManual.ToString("n2") + "|", ft, Brushes.Black, 162, ln, alinhaDireita);
                }
                else
                    e.Graphics.DrawString(oIndicadores.CumprimentoManual.ToString("n2") + "|", ft, Brushes.Black, 162 + (i * 31), ln, alinhaDireita);
            }
            e.Graphics.DrawString((_totalx / (iUltimoSolicitado + 1)).ToString("N2") + "|", ft, Brushes.Black, 160 + (((iUltimoSolicitado + 1) * 31) - 4), ln, alinhaDireita);
            ln = ln + 18;
            e.Graphics.DrawString(str.PadLeft(189, pad), ft, Brushes.Black, cl, ln);
        }

        private void PreparaDados()
        {
            DataInicial = DataMostragemInicial.Text;
            DataFinal = DataMostragemFinal.Text;
        }

        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            PreparaDados();
            ((Form)ppd).StartPosition = FormStartPosition.CenterScreen;
            ((Form)ppd).Text = "Visualizador";
            ((Form)ppd).WindowState = FormWindowState.Maximized;
            pd.DefaultPageSettings.Landscape = true;

            // propriedade kind tem que ser custom
            //pd.DefaultPageSettings.PaperSize.Width = 1200;
            ppd.Document = pd;
            ppd.ShowDialog();
        }

        private void FrmRelatorioIndicadores_Load(object sender, EventArgs e)
        {
            string _ultimodiamesanterior = Convert.ToDateTime(("01/" + DateTime.Now.Month + "/" + DateTime.Now.Year)).AddDays(-1).ToString("dd/MM/yyyy");

            DataMostragemInicial.Text =  Convert.ToDateTime(("01/" +
                                         Convert.ToDateTime(_ultimodiamesanterior).Month.ToString() + "/" +
                                         Convert.ToDateTime(_ultimodiamesanterior).Year.ToString())).ToString("yyyy-MM-dd");

            DataMostragemFinal.Text = Convert.ToDateTime(_ultimodiamesanterior).ToString("yyyy-MM-dd");
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