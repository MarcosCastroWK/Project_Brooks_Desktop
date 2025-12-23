using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibSILC;
using SILCNegocios;

namespace formSILC
{
    public partial class frmRelatorioParaFaturamentoComResiduos : Form
    {
        private int regs = 0;
        private int pagina = 0;
        public int pAnoMesDia;
        public string pDataProgramacaoAberta;
        clsContratos oContrato = new clsContratos();
        clsContratosDados oContratoDados = new clsContratosDados();

        clsCaminhoes oCaminhao = new clsCaminhoes();
        clsCaminhoesDados oCaminhaoDados = new clsCaminhoesDados();
        clsFuncionarios oMotorista = new clsFuncionarios();
        clsFuncionarioDados oMotoristaDados = new clsFuncionarioDados();

        clsProgramacaoDados oProgramacaoDados = new clsProgramacaoDados();
        
        private BindingSource bindingSource = new BindingSource();
        public DataTable _dt = new DataTable();
        private DataTable _dtProgramadosNaRota = new DataTable();

        public frmRelatorioParaFaturamentoComResiduos()
        {
            InitializeComponent();            
        }
        private void PreencheGrade()
        {
            bindingSource.DataSource = oContratoDados.PreencheDataTableContratos("");
            GradeContrato.DataSource = bindingSource.DataSource;

        }

        private void frmRelatorioParaFaturamentoComResiduos_Load(object sender, EventArgs e)
        {
            PreencheGrade();
            EstiloGrades(Grade);
            GradeContrato.Height = this.Height - 560;
            Grade.Height = this.Height - 300;
        }

        private void EstiloGrades(DataGridView oGrade)
        {

            //oGrade.Columns["Map"].Width = 32;
            //oGrade.Columns["Map"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //oGrade.Columns["Map"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //oGrade.Columns["Rp"].Visible = false;
            //oGrade.Columns["Sequencial"].Visible = false;
            //oGrade.Columns["Linha"].Visible = false;
            //oGrade.Columns["AnoMesDia"].Visible = false;
            //oGrade.Columns["SequencialParaQuadro1"].Visible = false;

        }

        private int PegaNumeroMaior()
        {
            int iRet = 0;
            for (int iCount = 400; iCount > 0; iCount--)
            {
                for (int i = 0; i < Grade.Rows.Count - 1; i++)
                {
                    if (Grade.Rows[i].Cells["NovaLinha"].Value.ToString() != "")
                    {
                        if (Convert.ToInt16(Grade.Rows[i].Cells["NovaLinha"].Value) == iCount)
                        {
                            iRet = iCount;
                            break;
                        }
                    }
                }
                if (iRet > 0)
                    break;
            }
            return iRet;
        }
        private void Grade_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 34 && e.RowIndex >= 0)
            {
                int iDesaf = 0;
                iDesaf = PegaNumeroMaior();
                if (iDesaf == 0)
                {
                    iDesaf = 1;
                    Grade.Rows[Grade.CurrentRow.Index].Cells["NovaLinha"].Value = 1;
                }
                else
                {
                    iDesaf++;
                    Grade.Rows[Grade.CurrentRow.Index].Cells["NovaLinha"].Value = iDesaf;
                }
                //oProgramacaoDados.SalvarNovaLinha(Grade.Rows[Grade.CurrentRow.Index].Cells["Sequencial"].Value.ToString(), iDesaf.ToString());
            }
        }

        private void NumeraQuandoOrdena()
        {
            int iDesaf = 1;
            for (int i = 0; i < Grade.Rows.Count - 1; i++)
            {
                DataGridViewRow _dgr = Grade.Rows[i];
                //DataRow _dr = _dtProgramadosNaRota.Rows[i];
                _dgr.Cells["NovaLinha"].Value = iDesaf;
                //_dr["NovaLinha"] = iDesaf;
                iDesaf++;
            }
            Grade.Refresh();
        }

        private void Grade_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            bindingSource.DataSource = _dtProgramadosNaRota;
            Grade.DataSource = bindingSource.DataSource;

            if (e.ColumnIndex  != 32) // coluna ordem não deve ser renumerada
            {
                DialogResult dResult = DialogResult.Abort;
                dResult = MessageBox.Show("Renumerar conforme ordenação?", "SILC", MessageBoxButtons.YesNo);
                if (dResult == DialogResult.Yes)
                    NumeraQuandoOrdena();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (VerificaCampoOrdem() == DialogResult.Yes)
            {
                pagina = 0;
                regs = 0;

                pd.DefaultPageSettings.Landscape = true;
                pdialog.Document = pd;
                if (pdialog.ShowDialog() == DialogResult.OK)
                {
                    pd.Print();
                }
            }
        }

        private void pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float cl = 10.0F;
            float ln = 12.0F;

            Font ft = new Font("Arial", 9);
            e.Graphics.DrawString(geral.NomeEmpresa(geral.CodigoEmpresa), ft, Brushes.Black, cl, ln);

            ln = ln + 12.0F;
            e.Graphics.DrawString("Roteiro de Serviços do Dia: " + pDataProgramacaoAberta, ft, Brushes.Black, 460, ln);

            Font ftBold = new Font("Arial", 12, FontStyle.Bold);

            ln = ln + 20.0F;
            e.Graphics.DrawString("Motorista: " + Grade.Rows[0].Cells["NomeMotorista"].Value, ftBold, Brushes.Black, 9, ln);
            ln = ln + 20.0F;
            e.Graphics.DrawString("Caminhão: " + Grade.Rows[0].Cells["ModeloCaminhao"].Value, ftBold, Brushes.Black, 9, ln);

            ln = ln + 30.0F;
            ft = new Font("Arial", 7);
            cl = 26F;
            e.Graphics.DrawString("Serviço a Executar", ft, Brushes.Black, cl, ln);
            cl = cl + 176;
            e.Graphics.DrawString("Tipo de Resíduo", ft, Brushes.Black, cl, ln);
            cl = cl + 130;
            e.Graphics.DrawString("Cliente/Destino", ft, Brushes.Black, cl, ln);
            cl = cl + 200;
            e.Graphics.DrawString("Franquia", ft, Brushes.Black, cl, ln);
            cl = cl + 50;
            e.Graphics.DrawString("Un", ft, Brushes.Black, cl, ln);
            cl = cl + 20;
            e.Graphics.DrawString("Observação", ft, Brushes.Black, cl, ln);
            cl = cl + 200;
            e.Graphics.DrawString("Destino Final", ft, Brushes.Black, cl, ln);
            cl = cl + 100;
            e.Graphics.DrawString("Endereço", ft, Brushes.Black, cl, ln);

            ln = ln + 12.0F;
            string str = "";
            char pad = '─';
            cl = 10.0F;
            e.Graphics.DrawString(str.PadLeft(162, pad), ft, Brushes.Black, cl, ln);
            StringFormat alinhaDireita = new StringFormat();
            alinhaDireita.Alignment = StringAlignment.Far;
            int _NrLinhas = 620;
            //DataRow[] _drArray = _dtProgramadosNaRota.Select("NovaLinha >= 0", "NovaLinha asc");
            bool bJaImprimiuRodape = false;
            Grade.Sort(Grade.Columns[34], ListSortDirection.Ascending);

            for (int i = regs; i <= _dtProgramadosNaRota.Rows.Count - 1; i++)
            {
                DataGridViewRow dgr = Grade.Rows[i];
                if (ln < _NrLinhas)
                {
                    ln = ln + 12F;
                    cl = 10.0F;
                    ftBold = new Font("Arial", 8, FontStyle.Bold);
                    e.Graphics.DrawString((i + 1).ToString(), ftBold, Brushes.Black, cl, ln);

                    cl = 26.0F;
                    string[] sServico = dgr.Cells["HoraProgramada"].Value.ToString().Split(" "[0]);
                    int _iprimeiro = 0;
                    string _servrest = "";
                    foreach (string _servico in sServico)
                    {
                        _iprimeiro++;
                        if (_iprimeiro == 1)
                        {
                            ftBold = new Font("Arial", 7, FontStyle.Bold);
                            e.Graphics.DrawString(_servico, ftBold, Brushes.Black, cl, ln);
                        }
                        else
                            _servrest = _servrest + " " + _servico;
                    }

                    ftBold = new Font("Arial", 7);
                    if (_servrest != "")
                    {
                        float cl2 = 10;
                        cl2 = (cl - 8) + (sServico[0].Length * 8);
                        e.Graphics.DrawString(_servrest, ftBold, Brushes.Black, cl2, ln);
                    }
                    cl = cl + 176;
                    if (dgr.Cells["ExecutarServico"].Value.ToString().Length > 20)
                        e.Graphics.DrawString(dgr.Cells["ExecutarServico"].Value.ToString().Substring(0, 20), ft, Brushes.Black, cl, ln);
                    else
                        e.Graphics.DrawString(dgr.Cells["ExecutarServico"].Value.ToString(), ft, Brushes.Black, cl, ln);
                    cl = cl + 130;
                    if (dgr.Cells["NomeFantasiaCliente"].Value.ToString().Length > 30)
                        e.Graphics.DrawString(dgr.Cells["NomeFantasiaCliente"].Value.ToString().Substring(0, 30), ft, Brushes.Black, cl, ln);
                    else
                        e.Graphics.DrawString(dgr.Cells["NomeFantasiaCliente"].Value.ToString(), ft, Brushes.Black, cl, ln);
                    cl = cl + 200;
                    e.Graphics.DrawString(dgr.Cells["Quantidade"].Value.ToString(), ft, Brushes.Black, cl, ln);
                    cl = cl + 50;
                    e.Graphics.DrawString(dgr.Cells["Unidade"].Value.ToString(), ft, Brushes.Black, cl, ln);
                    cl = cl + 20;
                    e.Graphics.DrawString(dgr.Cells["Observacao"].Value.ToString(), ft, Brushes.Black, cl, ln);
                    cl = cl + 200;
                    if (dgr.Cells["DestinoFinal"].Value.ToString().Length > 14)
                        e.Graphics.DrawString(dgr.Cells["DestinoFinal"].Value.ToString().Substring(0, 14), ft, Brushes.Black, cl, ln);
                    else
                        e.Graphics.DrawString(dgr.Cells["DestinoFinal"].Value.ToString(), ft, Brushes.Black, cl, ln);
                    cl = cl + 100;
                    int _palavarasnalinha = 6;
                    if (i == 0)
                        _palavarasnalinha = 5;
                    ln = ImprimeTextoQuebrado(dgr.Cells["CidadeBairroEndereco"].Value.ToString(), cl, _palavarasnalinha, ln, e, _NrLinhas);

                    if (ln <= 730)
                    {
                        ln = ln - 8;
                        e.Graphics.DrawString(str.PadLeft(162, pad), ft, Brushes.Black, 10, ln);
                    }
                    regs++;
                }
                else
                {
                    ln = ln + 20F;
                    cl = 385;
                    pagina++;
                    e.Graphics.DrawString(str.PadLeft(86, pad), ft, Brushes.Black, 10, 710);
                    e.Graphics.DrawString(geral.NomeSistema, ft, Brushes.Black, 12, 720);
                    e.Graphics.DrawString("Emissão: " + DateTime.Now.ToShortDateString(), ft, Brushes.Black, 420, 720);
                    e.Graphics.DrawString("Pagina: " + pagina, ft, Brushes.Black, 570, 720);
                    e.HasMorePages = true;
                    ln = 20F;
                    bJaImprimiuRodape = true;
                    break;
                }
            }
            if (regs >= _dtProgramadosNaRota.Rows.Count -1 && !bJaImprimiuRodape)
            {
                ln = ln + 20F;
                cl = 385;
                pagina++;
                e.Graphics.DrawString(str.PadLeft(86, pad), ft, Brushes.Black, 10, 710);
                e.Graphics.DrawString(geral.NomeSistema, ft, Brushes.Black, 12, 720);
                e.Graphics.DrawString("Emissão: " + DateTime.Now.ToShortDateString(), ft, Brushes.Black, 420, 720);
                e.Graphics.DrawString("Pagina: " + pagina, ft, Brushes.Black, 570, 720);
                e.HasMorePages = false;
            }
            else
            {
                e.HasMorePages = true;
            }
        }

        private void Grade_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2 || e.KeyCode == Keys.Enter)
            {
                int _currentrowindex = Grade.CurrentRow.Index;

                if (e.KeyCode == Keys.Enter)
                    _currentrowindex = Grade.CurrentRow.Index - 1;

                if (Grade.Columns[Grade.CurrentCell.ColumnIndex].HeaderText == "Cam") // caminhão
                {
                    frmProcura frmLista = new frmProcura("CAMINHAOPROGRAMACAO");
                    frmLista.ShowDialog();
                    if (geral.CodigoCaminhao > 0)
                    {
                        Grade.Rows[_currentrowindex].Cells["CodigoCaminhao"].Value = geral.CodigoCaminhao;
                        oCaminhao = oCaminhaoDados.PegaDados(oCaminhao, geral.CodigoCaminhao);
                        if (oCaminhao.Modelo != "")
                        {
                            Grade.Rows[_currentrowindex].Cells["ModeloCaminhao"].Value = oCaminhao.Modelo;
                            oProgramacaoDados.SalvarCaminhao(Grade.Rows[_currentrowindex].Cells["Sequencial"].Value.ToString(), geral.CodigoCaminhao);
                        }
                    }
                }
                if (Grade.Columns[Grade.CurrentCell.ColumnIndex].HeaderText == "Motorista") // Motorista
                {
                    frmProcura frmLista = new frmProcura("MOTORISTAPROGRAMACAO");
                    frmLista.ShowDialog();
                    if (geral.CodigoMotorista > 0)
                    {
                        Grade.Rows[_currentrowindex].Cells["CodigoMotorista"].Value = geral.CodigoMotorista;

                        oMotorista = oMotoristaDados.PegaDados(oMotorista, geral.CodigoMotorista);
                        if (oMotorista.Nome != "")
                        {
                            Grade.Rows[_currentrowindex].Cells["NomeMotorista"].Value = oMotorista.Nome;
                            oProgramacaoDados.SalvarMotorista(Grade.Rows[_currentrowindex].Cells["Sequencial"].Value.ToString(), geral.CodigoMotorista, oMotorista.Nome);
                        }
                    }
                }
            }
        }
        private DialogResult VerificaCampoOrdem()
        {
            bool bFaltaNumeracao = false;
            for (int i = 0; i < Grade.Rows.Count - 1; i++)
            {
                if (Grade.Rows[i].Cells["NovaLinha"].Value.ToString() == "" ||
                    Grade.Rows[i].Cells["NovaLinha"].Value.ToString() == "0")
                    bFaltaNumeracao = true;
            }
            DialogResult dResult = DialogResult.Yes;
            if (bFaltaNumeracao)
            {
                dResult = MessageBox.Show("É necessário numerar campo ordem. Numerar assim mesmo?", "SILC", MessageBoxButtons.YesNo);
                if (dResult == DialogResult.Yes)
                {
                    NumeraQuandoOrdena();
                }
            }
            return dResult;
        }
        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            if (VerificaCampoOrdem() == DialogResult.Yes)
            {
                pagina = 0;
                regs = 0;
                ((Form)ppd).StartPosition = FormStartPosition.CenterScreen;
                ((Form)ppd).Text = "Visualizador";
                ((Form)ppd).WindowState = FormWindowState.Maximized;
                pd.DefaultPageSettings.Landscape = true;
                
                ppd.Document = pd;
                ppd.ShowDialog();
            }
        }

        private float ImprimeTextoQuebrado(string texto, float pColuna, int pPalavrasNaLinha, float pLinha, System.Drawing.Printing.PrintPageEventArgs pe, int pNrLinhas)
        {
            Font ft = new Font("Arial", 7); 
            int j = 0;
            string mArray = "";
            for (int i = 1; i <= texto.Length; i++)
            {
                mArray = mArray + texto.Substring(i - 1, 1);
                if (texto.Substring(i - 1, 1) == " " || texto.Substring(i - 1, 1) == "-" || texto.Substring(i - 1, 1) == ",")
                    j = j + 1;
                if (j == pPalavrasNaLinha)
                {
                    if (mArray.Substring(0, mArray.Length - 1) != " " && mArray.Substring(0, mArray.Length - 1) != "" && 
                        mArray.Substring(0, mArray.Length - 1) != System.Environment.NewLine)
                    {
                        mArray.Replace(System.Environment.CommandLine, "");
                        mArray.Replace(System.Environment.NewLine, "");
                        pe.Graphics.DrawString(mArray.Substring(0, mArray.Length).Trim(), ft, Brushes.Black, pColuna, pLinha);
                        pLinha = pLinha + 20;
                        if (pLinha > pNrLinhas)
                        {
                            pe.HasMorePages = true;
                        }
                    }
                    j = 0;
                    mArray = "";
                }
            }
            if (mArray.Length > 0)
            {
                if (mArray.Substring(0, mArray.Length - 1) != " " && mArray.Substring(0, mArray.Length - 1) != "")
                {
                    pe.Graphics.DrawString(mArray.Substring(0, mArray.Length).Trim(), ft, Brushes.Black, pColuna, pLinha);
                    pLinha = pLinha + 20;
                }
            }
            return pLinha;
        }

        private void btnLimpaOrdem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Grade.Rows.Count; i++)
            {
                Grade.Rows[i].Cells["NovaLinha"].Value = 0;
            }
        }

        private void Grade_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
