using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LibSILC;
using SILCNegocios;

namespace formSILC
{
    public partial class frmRelatorioExecutados : Form
    {
        public string DataEmissao;
        public int Programados;
        public DataTable dtExecutados = new DataTable();
        public DataTable dtProgramados = new DataTable();

        private clsProgramacaoDados oProgramacaoDados = new clsProgramacaoDados();
        private DataTable dt = new DataTable();        

        private int regs = 0;
        private int pagina = 0;
        private bool bImprimeIndicadores = false;

        public frmRelatorioExecutados()
        {
            InitializeComponent();
        }

        private void pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {            
            float cl = 8.0F;
            float ln = 12.0F;
            Font ft = new Font("Arial", 9);
            e.Graphics.DrawString(geral.NomeEmpresa(geral.CodigoEmpresa), ft, Brushes.Black, cl, ln);

            ln = ln + 12.0F;
            string sTipo = "Executado";
            e.Graphics.DrawString("Programação de Serviços - " + sTipo + " - Data serviços: " + DataEmissao, ft, Brushes.Black, cl, ln);
            e.Graphics.DrawString("Emissão: " + DateTime.Now.ToShortDateString(), ft, Brushes.Black, 520, ln);

            if (!bImprimeIndicadores)
            {
                ft = new Font("Arial", 7);
                ln = ln + 18.0F;
                e.Graphics.DrawString("Caminhão", ft, Brushes.Black, cl, ln);
                cl = cl + 60;
                e.Graphics.DrawString("Motorista", ft, Brushes.Black, cl, ln);
                cl = cl + 70;
                e.Graphics.DrawString("Código", ft, Brushes.Black, cl, ln);
                cl = cl + 36;
                e.Graphics.DrawString("Nome Fantasia Cliente", ft, Brushes.Black, cl, ln);
                cl = cl + 184;
                e.Graphics.DrawString("Código", ft, Brushes.Black, cl, ln);
                cl = cl + 36;
                e.Graphics.DrawString("Resíduo", ft, Brushes.Black, cl, ln);
                cl = cl + 170;
                e.Graphics.DrawString("Serviço executado", ft, Brushes.Black, cl, ln);
                cl = cl + 150;
                e.Graphics.DrawString("Observações", ft, Brushes.Black, cl, ln);
                cl = cl + 150;
                e.Graphics.DrawString("Qt", ft, Brushes.Black, cl, ln);
                cl = cl + 20;
                e.Graphics.DrawString("Un", ft, Brushes.Black, cl, ln);
                cl = cl + 20;
                e.Graphics.DrawString("Dt.Inicial", ft, Brushes.Black, cl, ln);
                cl = cl + 70;
                e.Graphics.DrawString("Hora", ft, Brushes.Black, cl, ln);
                cl = cl + 50;
                e.Graphics.DrawString("Dt.Executa", ft, Brushes.Black, cl, ln);
                cl = cl + 56;
                e.Graphics.DrawString("Solicitante", ft, Brushes.Black, cl, ln);
                cl = cl + 66;
                e.Graphics.DrawString("TP", ft, Brushes.Black, cl, ln);
            }
            ln = ln + 12.0F;
            string str = "";
            char pad = '─';
            cl = 8.0F;
            e.Graphics.DrawString(str.PadLeft(172, pad), ft, Brushes.Black, cl, ln);
            StringFormat alinhaDireita = new StringFormat();
            alinhaDireita.Alignment = StringAlignment.Far;
            int _NrLinhas = 560;
            string ModeloCaminhaoAnterior = "";
            DataRow dr;
            if (dtExecutados.Rows.Count > 0)
            {
                dr = dtExecutados.Rows[0];
                ModeloCaminhaoAnterior = dr["ModeloCaminhao"].ToString();
            }
            DataRow[] drArrayExecutados = dtExecutados.Select("", "CodigoCaminhao asc, Linha asc");

            for (int i = regs; i <= dtExecutados.Rows.Count - 1; i++)
            {
                dr = drArrayExecutados[i];
                string sCl = dr["NomeFantasiaCliente"].ToString() + " " + dr["Sequencial"].ToString();
                if (ln <= _NrLinhas)
                {
                    if (ModeloCaminhaoAnterior != dr["ModeloCaminhao"].ToString())
                    {                        
                        if (ln != 56)
                        {
                            ln = ln + 10F;
                            e.Graphics.DrawString(str.PadLeft(172, pad), ft, Brushes.Black, 8, ln);
                        }
                    }
                    ln = ln + 12F;
                    cl = 8.0F;
                    if (dr["ModeloCaminhao"].ToString().Length > 8)
                        e.Graphics.DrawString(dr["ModeloCaminhao"].ToString().Substring(0, 4), ft, Brushes.Black, cl, ln);
                    else
                        e.Graphics.DrawString(dr["ModeloCaminhao"].ToString(), ft, Brushes.Black, cl, ln);

                    cl = cl + 60;
                    string primeiroNome = "";
                    if (dr["NomeMotoristaOuDescricao"].ToString().IndexOf(" ") >= 0)
                        primeiroNome = dr["NomeMotoristaOuDescricao"].ToString().Split(" "[0])[0];
                    else
                        primeiroNome = dr["NomeMotoristaOuDescricao"].ToString();

                    if (primeiroNome == "" && dr["NomeMotorista"].ToString().IndexOf(" ") >= 0)
                        primeiroNome = dr["NomeMotorista"].ToString().Split(" "[0])[0];
                    else if (primeiroNome == "")
                        primeiroNome = dr["NomeMotorista"].ToString();

                    if (primeiroNome.Length > 10)
                        e.Graphics.DrawString(primeiroNome.Substring(0, 10), ft, Brushes.Black, cl, ln);
                    else
                        e.Graphics.DrawString(primeiroNome, ft, Brushes.Black, cl, ln);

                    cl = cl + 70;
                    e.Graphics.DrawString(Convert.ToInt32(dr["CodigoCliente"]).ToString("000000"), ft, Brushes.Black, cl, ln);

                    cl = cl + 36;
                    if (dr["NomeFantasiaCliente"].ToString().Length > 25)
                        e.Graphics.DrawString(dr["NomeFantasiaCliente"].ToString().Substring(0, 25), ft, Brushes.Black, cl, ln);
                    else
                        e.Graphics.DrawString(dr["NomeFantasiaCliente"].ToString(), ft, Brushes.Black, cl, ln);
                    
                    cl = cl + 184;
                    if (Convert.ToInt32(dr["CodigoResiduo"]) == 0)
                    {
                        clsResiduoDados oRes = new clsResiduoDados();
                        int _cdRes = 0;
                        _cdRes = oRes.PegaCodigoResiduo(dr["ExecutarServico"].ToString());
                        e.Graphics.DrawString(_cdRes.ToString("000000"), ft, Brushes.Black, cl, ln);
                    }
                    else
                        e.Graphics.DrawString(Convert.ToInt32(dr["CodigoResiduo"]).ToString("000000"), ft, Brushes.Black, cl, ln);

                    cl = cl + 36;
                    if (dr["ExecutarServico"].ToString().Length > 20)
                        e.Graphics.DrawString(dr["ExecutarServico"].ToString().Substring(0, 20), ft, Brushes.Black, cl, ln);
                    else
                        e.Graphics.DrawString(dr["ExecutarServico"].ToString(), ft, Brushes.Black, cl, ln);
                    cl = cl + 170;
                    if (dr["HoraProgramada"].ToString().Length > 20)
                        e.Graphics.DrawString(dr["HoraProgramada"].ToString().Substring(0, 20), ft, Brushes.Black, cl, ln);
                    else
                        e.Graphics.DrawString(dr["HoraProgramada"].ToString(), ft, Brushes.Black, cl, ln);
                    cl = cl + 150;
                    if (dr["Observacao"].ToString().Length > 23)
                        e.Graphics.DrawString(geral.Left(dr["Observacao"].ToString(), 23), ft, Brushes.Black, cl, ln);
                    else
                        e.Graphics.DrawString(dr["Observacao"].ToString(), ft, Brushes.Black, cl, ln);

                    cl = cl + 163;
                    string _qt = dr["Quantidade"].ToString();
                    if (dr["Quantidade"].ToString().IndexOf(",") == -1 && dr["Quantidade"].ToString().IndexOf(".") == -1)
                        _qt = dr["Quantidade"].ToString();
                    else if (dr["Quantidade"].ToString().IndexOf(",") >= 0)
                        _qt = dr["Quantidade"].ToString().Split(","[0])[0];
                    else if (dr["Quantidade"].ToString().IndexOf(".") >= 0)
                        _qt = dr["Quantidade"].ToString().Split("."[0])[0];
                    e.Graphics.DrawString(_qt, ft, Brushes.Black, cl, ln, alinhaDireita);

                    cl = cl + 7;
                    e.Graphics.DrawString(dr["Unidade"].ToString(), ft, Brushes.Black, cl, ln);
                    cl = cl + 20;
                    e.Graphics.DrawString(Convert.ToDateTime(dr["Data"]).ToString("dd/MM/yyyy"), ft, Brushes.Black, cl, ln);
                    cl = cl + 70;
                    e.Graphics.DrawString(dr["Hora"].ToString(), ft, Brushes.Black, cl, ln);
                    cl = cl + 50;
                    e.Graphics.DrawString(Convert.ToDateTime(dr["DataProgramada"]).ToString("dd/MM/yyyy"), ft, Brushes.Black, cl, ln);
                    cl = cl + 56;
                    e.Graphics.DrawString(dr["Solicitante"].ToString(), ft, Brushes.Black, cl, ln);
                    cl = cl + 66;
                    e.Graphics.DrawString(dr["TipoProgramacao"].ToString(), ft, Brushes.Black, cl, ln);
                    ModeloCaminhaoAnterior = dr["ModeloCaminhao"].ToString();
                }
                else
                {
                    ln = ln + 20F;
                    cl = 385;
                    pagina++;
                    e.Graphics.DrawString(str.PadLeft(88, pad), ft, Brushes.Black, 10, _NrLinhas + 64);
                    e.Graphics.DrawString(geral.NomeSistema, ft, Brushes.Black, 12, _NrLinhas + 84);
                    e.Graphics.DrawString("Pagina: " + pagina, ft, Brushes.Black, 570, _NrLinhas + 84);
                    e.HasMorePages = true;
                    break;
                }
                regs++;
            }
            if (regs >= dtExecutados.Rows.Count - 1 && !bImprimeIndicadores)
            {
                if (ln < 160)
                {
                    ImprimeIndicadores(cl, ln, ft, e);
                    bImprimeIndicadores = false;
                }
                else
                {
                    ln = ln + 20F;
                    cl = 385;
                    pagina++;
                    e.Graphics.DrawString(str.PadLeft(88, pad), ft, Brushes.Black, 10, _NrLinhas + 64);
                    e.Graphics.DrawString(geral.NomeSistema, ft, Brushes.Black, 12, _NrLinhas + 84);
                    e.Graphics.DrawString("Pagina: " + pagina, ft, Brushes.Black, 570, _NrLinhas + 84);
                    e.HasMorePages = true;
                    bImprimeIndicadores = true;
                }
            }
            else if (bImprimeIndicadores)
            {
                e.HasMorePages = false;
                ImprimeIndicadores(cl, ln, ft, e);
                bImprimeIndicadores = false;
            }
        }
        private void ImprimeIndicadores(float cl, float ln, Font ft, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string str = "";
            char pad = '─';
            ft = new Font("Courier New", 9);
            StringFormat alinhaDireita = new StringFormat();
            alinhaDireita.Alignment = StringAlignment.Far;

            // colocar aqui os indicadores
            clsIndicadoresDados oIndicadoresDados = new clsIndicadoresDados();
            clsIndicadores oIndicadores = new clsIndicadores();

            oIndicadoresDados.CalculaIndicadores(oIndicadores, dtExecutados, dtProgramados, DataEmissao);
            cl = 5;
            ln = ln + 30;
            
            e.Graphics.DrawString(str.PadLeft(150, pad), ft, Brushes.Black, cl, ln);
            ln = ln + 12;
            e.Graphics.DrawString("QUANTIDADE", ft, Brushes.Black, 135, ln);
            ln = ln + 12;
            e.Graphics.DrawString(str.PadLeft(150, pad), ft, Brushes.Black, cl, ln);
            ln = ln + 12;
            e.Graphics.DrawString("TOTAL GERAL PROGRAMADO .............: ", ft, Brushes.Black, 5, ln);
            e.Graphics.DrawString(oIndicadores.TotalProgramado.ToString(), ft, Brushes.Black, 365, ln, alinhaDireita);
            ln = ln + 12;
            e.Graphics.DrawString("      AUTOMÁTICO ...................: ", ft, Brushes.Black, 5, ln);
            e.Graphics.DrawString(oIndicadores.TotalProgramadoAutomatico.ToString(), ft, Brushes.Black, 365, ln, alinhaDireita);
            ln = ln + 12;
            e.Graphics.DrawString("      MANUAL .......................:" , ft, Brushes.Black, 5, ln);
            e.Graphics.DrawString(oIndicadores.TotalProgramadoManual.ToString(), ft, Brushes.Black, 365, ln, alinhaDireita);
            ln = ln + 12;
            e.Graphics.DrawString(str.PadLeft(150, pad), ft, Brushes.Black, cl, ln);
            ln = ln + 12;
            e.Graphics.DrawString("TOTAL GERAL EXECUTADO ..............: ", ft, Brushes.Black, 5, ln);
            e.Graphics.DrawString(oIndicadores.TotalExecutado.ToString(), ft, Brushes.Black, 365, ln, alinhaDireita);
            ln = ln + 12;
            e.Graphics.DrawString("      AUTOMÁTICO ...................: ", ft, Brushes.Black, 5, ln);
            e.Graphics.DrawString(oIndicadores.TotalExecutadoAutomatico.ToString(), ft, Brushes.Black, 365, ln, alinhaDireita);
            ln = ln + 12;
            e.Graphics.DrawString("      MANUAL .......................: ", ft, Brushes.Black, 5, ln);
            e.Graphics.DrawString(oIndicadores.TotalExecutadoManual.ToString(), ft, Brushes.Black, 365, ln, alinhaDireita);
            ln = ln + 12;
            e.Graphics.DrawString(str.PadLeft(150, pad), ft, Brushes.Black, cl, ln);
            ln = ln + 12;
            e.Graphics.DrawString("TOTAL GERAL CANCELADO ..............: ", ft, Brushes.Black, 5, ln);
            e.Graphics.DrawString(oIndicadores.TotalCancelado.ToString(), ft, Brushes.Black, 365, ln, alinhaDireita);
            ln = ln + 12;
            e.Graphics.DrawString("      PELO CLIENTE .................: ", ft, Brushes.Black, 5, ln);
            e.Graphics.DrawString(oIndicadores.TotalCanceladoCliente.ToString(), ft, Brushes.Black, 365, ln, alinhaDireita);
            ln = ln + 12;
            e.Graphics.DrawString(str.PadLeft(150, pad), ft, Brushes.Black, cl, ln);
            ln = ln + 12;
            e.Graphics.DrawString("TOTAL REPROGRAMADO .................: ", ft, Brushes.Black, 5, ln);
            e.Graphics.DrawString(oIndicadores.TotalReprog.ToString(), ft, Brushes.Black, 365, ln, alinhaDireita);
            ln = ln + 12;
            e.Graphics.DrawString("      IMPOSSIBILIDADE DE ATENDIMENTO: ", ft, Brushes.Black, 5, ln);
            e.Graphics.DrawString(oIndicadores.TotalReprogImpossivelAtender.ToString(), ft, Brushes.Black, 365, ln, alinhaDireita);
            ln = ln + 12;
            e.Graphics.DrawString("      FLEXIBILIDADE DO CONTRATO ....: ", ft, Brushes.Black, 5, ln);
            e.Graphics.DrawString(oIndicadores.TotalReprogFlexibilidade.ToString(), ft, Brushes.Black, 365, ln, alinhaDireita);
            ln = ln + 12;
            e.Graphics.DrawString("      SOLICITAÇÃO DO CLIENTE .......: ", ft, Brushes.Black, 5, ln);
            e.Graphics.DrawString(oIndicadores.TotalReprogSolicitacaoCliente.ToString(), ft, Brushes.Black, 365, ln, alinhaDireita);
            ln = ln + 12;
            e.Graphics.DrawString("      OUTROS .......................: ", ft, Brushes.Black, 5, ln);
            e.Graphics.DrawString(oIndicadores.TotalReprogOutros.ToString(), ft, Brushes.Black, 365, ln, alinhaDireita);
            ln = ln + 12;
            e.Graphics.DrawString(str.PadLeft(150, pad), ft, Brushes.Black, cl, ln);
            ln = ln + 12;
            e.Graphics.DrawString("TOTAL BLOQUEIO FINANCEIRO ..........: ", ft, Brushes.Black, 5, ln);
            e.Graphics.DrawString(oIndicadores.TotalBloqueioFinanc.ToString(), ft, Brushes.Black, 365, ln, alinhaDireita);
            ln = ln + 30;
            e.Graphics.DrawString("INDICADORES", ft, Brushes.Black, cl, ln);
            ln = ln + 18;
            e.Graphics.DrawString(str.PadLeft(150, pad), ft, Brushes.Black, cl, ln);
            ln = ln + 18;
            e.Graphics.DrawString("INDICE DE EXECUÇÃO DA PROGRAMAÇÃO: ", ft, Brushes.Black, 5, ln);
            e.Graphics.DrawString(oIndicadores.Cumprimento.ToString("n2"), ft, Brushes.Black, 365, ln, alinhaDireita);
            e.Graphics.DrawString(" %  EXECUTADO / PROGRAMADO * 100", ft, Brushes.Black, 360, ln);
            ln = ln + 18;
            e.Graphics.DrawString(str.PadLeft(150, pad), ft, Brushes.Black, cl, ln);
            ln = ln + 18;
            e.Graphics.DrawString("INDICE DE CANCELAMENTO .............: ", ft, Brushes.Black, 5, ln);
            e.Graphics.DrawString(oIndicadores.Cancelamento.ToString("n2"), ft, Brushes.Black, 365, ln, alinhaDireita);
            e.Graphics.DrawString(" %  TOTAL CANCELADOS / PROGRAMADO * 100 ", ft, Brushes.Black, 360, ln);
            ln = ln + 18;
            e.Graphics.DrawString(str.PadLeft(150, pad), ft, Brushes.Black, cl, ln);
            ln = ln + 18;

            decimal indReprog = 0;

            if (oIndicadores.TotalProgramado > 0)
                indReprog = oIndicadores.TotalReprog / oIndicadores.TotalProgramado * 100;

            if (oIndicadores.TotalProgramado > 0)
                oIndicadores.IndiceReprogOutros = oIndicadores.TotalReprogOutros / oIndicadores.TotalProgramado * 100;

            e.Graphics.DrawString("INDICE DE REPROGRAMAÇÃO ............: ", ft, Brushes.Black, 5, ln);
            e.Graphics.DrawString(indReprog.ToString("n2"), ft, Brushes.Black, 365, ln, alinhaDireita);
            e.Graphics.DrawString(" %  TOTAL REPROGRAMADO / PROGRAMADO * 100", ft, Brushes.Black, 360, ln);
            ln = ln + 22;
            e.Graphics.DrawString("     IMPOSSIBILIDADE DE ATENDIMENTO : ", ft, Brushes.Black, 5, ln);
            e.Graphics.DrawString(oIndicadores.ImpossivelAtender.ToString("n2"), ft, Brushes.Black, 365, ln, alinhaDireita);
            e.Graphics.DrawString(" %  TOTAL IMPOSSIBILIDADE ATENDIMENTO / PROGRAMADO * 100", ft, Brushes.Black, 360, ln);
            ln = ln + 12;
            e.Graphics.DrawString("     FLEXIBILIDADE DO CONTRATO .....: ", ft, Brushes.Black, 5, ln);
            e.Graphics.DrawString(oIndicadores.FlexibilidadeContrato.ToString("n2"), ft, Brushes.Black, 365, ln, alinhaDireita);
            e.Graphics.DrawString(" %  TOTAL FLEXIBILIDADE CONTRATO / PROGRAMADO * 100", ft, Brushes.Black, 360, ln);
            ln = ln + 12;
            e.Graphics.DrawString("     SOLICITAÇÃO DO CLIENTE ........: ", ft, Brushes.Black, 5, ln);
            e.Graphics.DrawString(oIndicadores.SolicitacaoCliente.ToString("n2"), ft, Brushes.Black, 365, ln, alinhaDireita);
            e.Graphics.DrawString(" %  TOTAL SOLICITADO CLIENTE / PROGRAMADO * 100", ft, Brushes.Black, 360, ln);
            //ln = ln + 12;
            //e.Graphics.DrawString("     REPROG C/BLOQUEIO FINANCEIRO ..: ", ft, Brushes.Black, 5, ln);
            //e.Graphics.DrawString(oIndicadores.BloqueioFinanceiro.ToString("n2"), ft, Brushes.Black, 365, ln, alinhaDireita);
            //e.Graphics.DrawString(" %  TOTAL REPROG BLOQUEIO FINANCEIRO / PROGRAMADO * 100", ft, Brushes.Black, 360, ln);
            ln = ln + 12;
            e.Graphics.DrawString("     REPROG OUTROS .................: ", ft, Brushes.Black, 5, ln);
            e.Graphics.DrawString(oIndicadores.IndiceReprogOutros.ToString("n2"), ft, Brushes.Black, 365, ln, alinhaDireita);
            e.Graphics.DrawString(" %  TOTAL REPROG OUTROS / PROGRAMADO * 100", ft, Brushes.Black, 360, ln);
            ln = ln + 18;
            e.Graphics.DrawString(str.PadLeft(150, pad), ft, Brushes.Black, cl, ln);
            ln = ln + 18;
            e.Graphics.DrawString("INDICE DE PROGRAMAÇÃO AUTOMÁTICA ...: ", ft, Brushes.Black, 5, ln);
            e.Graphics.DrawString(oIndicadores.ProgramacaoAutomatica.ToString("n2"), ft, Brushes.Black, 365, ln, alinhaDireita);
            e.Graphics.DrawString(" %  TOTAL PROGRAMADO AUTOMÁTICO / TOTAL PROGRAMAÇÃO * 100", ft, Brushes.Black, 360, ln);
            ln = ln + 12;
            e.Graphics.DrawString("INDICE DE EXECUÇÃO MANUAL .......: ", ft, Brushes.Black, 5, ln);
            e.Graphics.DrawString(oIndicadores.CumprimentoManual.ToString("n2"), ft, Brushes.Black, 365, ln, alinhaDireita);
            e.Graphics.DrawString(" %  TOTAL EXECUÇÃO MANUAL / TOTAL PROGRAMAÇÃO MANUAL * 100", ft, Brushes.Black, 360, ln);
            //ln = ln + 18;
            //e.Graphics.DrawString(str.PadLeft(150, pad), ft, Brushes.Black, cl, ln);
        }
        private void PreparaDados()
        {
            pagina = 0;
            regs = 0;
            bImprimeIndicadores = false;
            foreach (DataRow _dr in dtExecutados.Rows)
            {
                if (_dr["TipoProgramacao"].ToString() == "1") // Programação Automática 
                    _dr["TipoProgramacao"] = "A";
                if (_dr["TipoProgramacao"].ToString() == "2") // Programação Manual 
                    _dr["TipoProgramacao"] = "M";
                if (_dr["NomeMotoristaOuDescricao"].ToString() != "")
                    _dr["NomeMotorista"] = _dr["NomeMotoristaOuDescricao"];
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