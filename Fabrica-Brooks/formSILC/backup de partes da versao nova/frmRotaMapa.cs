using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gecko;
using iTextSharp.text.pdf.parser;
using LibSILC;
using SILCNegocios;

namespace formSILC
{
    public partial class frmRotaMapa : Form
    {
        private int regs = 0;
        private int pagina = 0;
        public int pAnoMesDia;
        public string pDataProgramacaoAberta;
        clsCaminhoes oCaminhao = new clsCaminhoes();
        clsCaminhoesDados oCaminhaoDados = new clsCaminhoesDados();
        clsFuncionarios oMotorista = new clsFuncionarios();
        clsFuncionarioDados oMotoristaDados = new clsFuncionarioDados();

        clsProgramacaoDados oProgramacaoDados = new clsProgramacaoDados();
        
        private BindingSource bindingSource = new BindingSource();
        public DataTable _dt = new DataTable();
        private DataTable _dtProgramadosNaRota = new DataTable();

        public frmRotaMapa()
        {
            InitializeComponent();            
        }
        private void PreencheGrade()
        {            
            foreach (DataColumn _dc in _dt.Columns)
            {
                _dtProgramadosNaRota.Columns.Add(_dc.Caption, _dc.DataType);
            }            
            foreach (DataRow _dr in _dt.Rows)
            {
                DataRow _dr2 = _dtProgramadosNaRota.NewRow();
                for (int i = 0; i < _dt.Columns.Count; i++)
                {              
                    _dr2[i] = _dr[i];
                    if (i == 33)
                        _dr2[33] = 0;
                }
                if (_dr["Map"].ToString() == "X")
                    _dtProgramadosNaRota.Rows.Add(_dr2);
            }

            bindingSource.DataSource = _dtProgramadosNaRota;
            Grade.DataSource = bindingSource.DataSource;
            Grade.Columns["NovaLinha"].Width = 40;
            Grade.Columns["NovaLinha"].HeaderText = "Ordem";

        }

        private void frmRotaMapa_Load(object sender, EventArgs e)
        {
            if (_dt.Columns.Count == 32)
            {
                _dt.Columns.Add("NovaLinha", Type.GetType("System.Int16"));
                for (int i = 0; i < Grade.Rows.Count - 1;i++)
                    Grade.Rows[i].Cells["NovaLinha"].Value = 0;
            }
            PreencheGrade();
            EstiloGrades(Grade);
        }

        private void EstiloGrades(DataGridView oGrade)
        {

            oGrade.Columns["Map"].Width = 32;
            //oGrade.Columns["Map"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //oGrade.Columns["Map"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            oGrade.Columns["Rp"].Visible = false;
            oGrade.Columns["Sequencial"].Visible = false;
            oGrade.Columns["Linha"].Visible = false;
            oGrade.Columns["AnoMesDia"].Visible = false;
            oGrade.Columns["SequencialParaQuadro1"].Visible = false;
            oGrade.Columns["TipoProgramacao"].Visible = false;
            oGrade.Columns["RotaMapa"].Visible = false;
            oGrade.Columns["Unidade2"].Visible = false;
            oGrade.Columns["Quadro"].Visible = false;
            oGrade.Columns["Data"].Visible = false;
            oGrade.Columns["Hora"].Visible = false;
            oGrade.Columns["StatusCor"].Visible = false;
            oGrade.Columns["Quantidade2"].Visible = false;
            oGrade.Columns["CorObservacao"].Visible = false;
            oGrade.Columns["Origem"].Visible = false;
            oGrade.Columns["CodigoResiduo"].Visible = false;
            oGrade.Columns["CodigoMotorista"].Visible = false;
            oGrade.Columns["CodigoCaminhao"].Visible = false;
            oGrade.Columns["CodigoCliente"].Visible = false;
            oGrade.Columns["NomeMotoristaOuDescricao"].Visible = false;
            oGrade.Columns["Solicitante"].Visible = false;

            oGrade.Columns["Data"].Width = 70;
            oGrade.Columns["Hora"].Width = 50;

            oGrade.Columns["NomeFantasiaCliente"].Width = 200;
            oGrade.Columns["NomeFantasiaCliente"].HeaderText = "Cliente";

            oGrade.Columns["Solicitante"].Width = 90;

            oGrade.Columns["ExecutarServico"].Width = 100;
            oGrade.Columns["ExecutarServico"].HeaderText = "Resíduo";

            oGrade.Columns["HoraProgramada"].Width = 150;
            oGrade.Columns["HoraProgramada"].HeaderText = "Serviço Executado";

            oGrade.Columns["ModeloCaminhao"].HeaderText = "Cam";
            oGrade.Columns["ModeloCaminhao"].Width = 50;

            oGrade.Columns["NomeMotorista"].HeaderText = "Motorista";
            oGrade.Columns["NomeMotorista"].Width = 100;

            oGrade.Columns["Unidade"].HeaderText = "Un";
            oGrade.Columns["Unidade"].Width = 25;

            oGrade.Columns["CidadeBairroEndereco"].HeaderText = "Cidade-Bairro-Endereço";
            oGrade.Columns["CidadeBairroEndereco"].Width = 330;

            //oGrade.Columns["EnderecoCompleto"].Visible = false;

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
            //e.Graphics.DrawString(geral.NomeEmpresa(geral.CodigoEmpresa), ft, Brushes.Black, cl, ln);
            
            System.Drawing.Image img = System.Drawing.Image.FromFile("logobrooks.png");
            Point loc = new Point(10, 10);
            e.Graphics.DrawImage(img, loc);

            ln = ln + 62.0F;
            e.Graphics.DrawString("Roteiro de Serviços do Dia: ".ToUpper() + pDataProgramacaoAberta, ft, Brushes.Black, 460, ln);

            Font ftBold = new Font("Arial", 12, FontStyle.Bold);

            ln = ln + 20.0F;
            e.Graphics.DrawString("Motorista: ".ToUpper() + Grade.Rows[0].Cells["NomeMotorista"].Value, ftBold, Brushes.Black, 20, ln);
            ln = ln + 20.0F;
            e.Graphics.DrawString("Caminhão: ".ToUpper() + Grade.Rows[0].Cells["ModeloCaminhao"].Value, ftBold, Brushes.Black, 20, ln);

            ln = ln + 30.0F;
            ft = new Font("Arial", 7);
            cl = 26F;
            e.Graphics.DrawString("Serviço a Executar".ToUpper(), ft, Brushes.Black, cl, ln);
            cl = cl + 186;
            e.Graphics.DrawString("Tipo de Resíduo".ToUpper(), ft, Brushes.Black, cl, ln);
            cl = cl + 230;
            e.Graphics.DrawString("Cliente".ToUpper(), ft, Brushes.Black, cl, ln);
            cl = cl + 200;
            e.Graphics.DrawString("Destino Final".ToUpper(), ft, Brushes.Black, cl, ln);
            cl = cl + 200;
            e.Graphics.DrawString("Observação".ToUpper(), ft, Brushes.Black, cl, ln);

            ln = ln + 10.0F;
            string str = "";
            char pad = '═';
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
                    //e.Graphics.DrawString((i + 1).ToString(), ftBold, Brushes.Black, cl, ln);

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
                    cl = cl + 186;
                    e.Graphics.DrawString(dgr.Cells["ExecutarServico"].Value.ToString(), ft, Brushes.Black, cl, ln);
                    cl = cl + 230;
                    e.Graphics.DrawString(dgr.Cells["NomeFantasiaCliente"].Value.ToString(), ft, Brushes.Black, cl, ln);
                    cl = cl + 200;
                    e.Graphics.DrawString(dgr.Cells["DestinoFinal"].Value.ToString(), ft, Brushes.Black, cl, ln);
                    cl = cl + 200;
                    e.Graphics.DrawString(dgr.Cells["Observacao"].Value.ToString(), ft, Brushes.Black, cl, ln);

                    ln = ln + 20F;
                    e.Graphics.DrawString("Endereço: ".ToUpper(), ft, Brushes.Black, 26, ln);                    
                    e.Graphics.DrawString(dgr.Cells["CidadeBairroEndereco"].Value.ToString().Split("|"[0])[0], ft, Brushes.Black, 150, ln);
                    ln = ln + 12F;
                    e.Graphics.DrawString("Ponto de Referência: ".ToUpper(), ft, Brushes.Black, 26, ln);
                    string PR_semLineFeed = "";
                    if (dgr.Cells["CidadeBairroEndereco"].Value.ToString() != "")
                        PR_semLineFeed = dgr.Cells["CidadeBairroEndereco"].Value.ToString().Split("|"[0])[1].Replace("\n\r", "").Replace("\n", "").Replace("\r", "").Replace("\t", "");
                    e.Graphics.DrawString(PR_semLineFeed, ft, Brushes.Black, 150, ln);

                    //if (chkImprimirFranquia.Checked && dgr.Cells["Quantidade"].Value.ToString() != "0,0000" && dgr.Cells["Quantidade"].Value.ToString() != "0" && 
                    //                                   dgr.Cells["Quantidade"].Value.ToString() != "")
                    //{
                    //    ln = ln + 20F;
                    //    e.Graphics.DrawString("Franquia: " + dgr.Cells["Quantidade"].Value.ToString(), ft, Brushes.Black, 26, ln);
                    //}
                    if (chkImprimirFranquia.Checked)
                    {
                        string _cdCliente = dgr.Cells["CodigoCliente"].Value.ToString();
                        clsContratoResiduosDados oContratoResiduoDados = new clsContratoResiduosDados();
                        DataTable _dtContDataReajuste = oContratoResiduoDados.PegaDadosUltimoReajuste(_cdCliente);
                        string _cdContrato = "";
                        string _dtReajuste = "";

                        if (_dtContDataReajuste.Rows.Count > 0)
                        {
                            _cdContrato = _dtContDataReajuste.Rows[0]["CodigoContrato"].ToString();
                            _dtReajuste = _dtContDataReajuste.Rows[0]["DataReajuste"].ToString();

                            DataTable _dtDadosResiduos = oContratoResiduoDados.PreencheDataTableContratoResiduos("CodigoResiduo", Convert.ToInt32(_cdContrato),
                                                                                                                 _dtReajuste, Convert.ToInt32(_cdCliente));

                            DataRow[] drr;
                            if (dgr.Cells["CodigoResiduo"].Value.ToString() == "")
                                drr = _dtDadosResiduos.Select("CodigoResiduo = " + dgr.Cells["CodigoResiduo"].Value.ToString());
                            else
                                drr = _dtDadosResiduos.Select("DescricaoReduzidaResiduo = '" + dgr.Cells[12].Value.ToString() + "'");
                            ln = ln + 20F;
                            if (drr.Length > 0)
                                e.Graphics.DrawString("Franquia de peso: " + drr[0]["FranquiaCobrancaPeso"].ToString() + "/" + drr[0]["UnidadeCobrancaPeso"].ToString(), ft, Brushes.Black, 26, ln);
                            else
                                e.Graphics.DrawString("Franquia de peso:", ft, Brushes.Black, 26, ln);
                        }
                        else
                        {
                            ln = ln + 20F;
                            e.Graphics.DrawString("Franquia de peso:", ft, Brushes.Black, 26, ln);
                        }
                    }

                    if (ln <= 730)
                    {
                        ln = ln + 20F;
                        e.Graphics.DrawString(str.PadLeft(162, pad), ft, Brushes.Black, 10, ln);
                    }
                    regs++;
                }
                else
                {
                    ln = ln + 20F;
                    e.Graphics.DrawString(txtMensagemMotorista.Text, ft, Brushes.Black, 26, 700);
                    cl = 385;
                    pagina++;
                    decimal x = (8 + _dtProgramadosNaRota.Rows.Count) / 8;
                    decimal pgns = Math.Round(x, 0);
                    e.Graphics.DrawString(pagina + "/" + pgns, ft, Brushes.Black, 570, 720);
                    e.HasMorePages = true;
                    ln = 20F;
                    bJaImprimiuRodape = true;
                    break;
                    //e.Graphics.DrawString(str.PadLeft(86, pad), ft, Brushes.Black, 10, 710);
                    //e.Graphics.DrawString(geral.NomeSistema, ft, Brushes.Black, 12, 720);
                    //e.Graphics.DrawString("Emissão: " + DateTime.Now.ToShortDateString(), ft, Brushes.Black, 420, 720);
                }
            }
            if (regs >= _dtProgramadosNaRota.Rows.Count -1 && !bJaImprimiuRodape)
            {
                e.Graphics.DrawString(txtMensagemMotorista.Text, ft, Brushes.Black, 26, 700);
                ln = ln + 20F;
                cl = 385;
                pagina++;
                decimal x = (8 + _dtProgramadosNaRota.Rows.Count) / 8;
                decimal pgns = Math.Round(x, 0);
                e.Graphics.DrawString(pagina + "/" + pgns, ft, Brushes.Black, 570, 720);
                e.HasMorePages = false;
                //e.Graphics.DrawString(str.PadLeft(86, pad), ft, Brushes.Black, 10, 710);
                //e.Graphics.DrawString(geral.NomeSistema, ft, Brushes.Black, 12, 720);
                //e.Graphics.DrawString("Emissão: " + DateTime.Now.ToShortDateString(), ft, Brushes.Black, 420, 720);
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
