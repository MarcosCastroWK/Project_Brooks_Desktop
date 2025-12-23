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

namespace formSILC
{
    public partial class frmListaReprogramacao : Form
    {
        public int pAnoMesDia;
        public int Sequencial;
        public int pCodigoCliente;
        public int pCodigoResiduo;
        public string pNomeCliente;
        public string pStatusCor;
        public DateTime pDataProgramada;
        public string pHora;
        public string pExecutarServico;
        public string pTipoContrato;
        public string pDataProgramaAbertaAtual;
        public bool bSalvarRefresh = false;

        clsReprogramacaoDados oReprogramacaoDados = new clsReprogramacaoDados();
        private BindingSource bindingSource = new BindingSource();
        DataTable _dt = new DataTable();

        public frmListaReprogramacao()
        {
            InitializeComponent();
        }

        private void PreencheGrade()
        {
            _dt = oReprogramacaoDados.PegaDadosLista(pAnoMesDia, 0, pCodigoCliente, pNomeCliente, pCodigoResiduo, 
                                                     "", pDataProgramada, pHora, pExecutarServico, pTipoContrato);

            foreach (DataRow _dr in _dt.Rows)
            {
                if (_dr["TipoContrato"].ToString() == "1" || _dr["TipoContrato"].ToString() == "0") // Programação Automática
                    _dr["TipoContrato"] = "A";
                if (_dr["TipoContrato"].ToString() == "2") // Programação Manual
                    _dr["TipoContrato"] = "M";
                if (_dr["Data"].ToString() != "")
                    _dr["Data"] = Convert.ToDateTime(_dr["Data"]).ToString("dd/MM/yyyy");
            }
            bindingSource.DataSource = _dt;
            Grade.DataSource = bindingSource.DataSource;
            EstiloGrades(Grade);
            Pinta();
        }
        private void Pinta()
        {
            foreach (DataGridViewRow dgvr in Grade.Rows)
            {
                if (dgvr.Cells["StatusCor"].Value != null)
                {
                    if (dgvr.Cells["StatusCor"].Value.ToString() == geral.RetornaCodigoCor("Vermelho"))
                        dgvr.DefaultCellStyle.BackColor = Color.IndianRed;
                    else if (dgvr.Cells["StatusCor"].Value.ToString() == geral.RetornaCodigoCor("AzulClaro"))
                        dgvr.DefaultCellStyle.BackColor = Color.MediumAquamarine;
                    else if (dgvr.Cells["StatusCor"].Value.ToString() == geral.RetornaCodigoCor("Cinza"))
                        dgvr.DefaultCellStyle.BackColor = Color.Gray;
                    else if (dgvr.Cells["StatusCor"].Value.ToString() == geral.RetornaCodigoCor("Magenta"))
                    {
                        dgvr.DefaultCellStyle.BackColor = Color.Magenta;
                        dgvr.DefaultCellStyle.ForeColor = Color.White;
                    }
                    else
                        dgvr.DefaultCellStyle.BackColor = Color.White;
                }
            }
            Grade.Refresh();
        }
        private void frmListaReprogramacao_Load(object sender, EventArgs e)
        {
            PreencheGrade();
        }

        private void EstiloGrades(DataGridView oGrade)
        {
            oGrade.Columns["AnoMesDia"].Visible = false;
            oGrade.Columns["StatusCor"].Visible = false;
            oGrade.Columns["SequencialProgramacaoDiaria"].Visible = false;           
            oGrade.Columns["CodigoMotorista"].Visible = false;
            oGrade.Columns["CodigoCaminhao"].Visible = false;
            oGrade.Columns["CodigoCliente"].Visible = false;
            oGrade.Columns["Unidade2"].Visible = false;
            oGrade.Columns["MapaMarcado"].Visible = false;

            oGrade.Columns["Data"].Width = 80;
            oGrade.Columns["Data"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            oGrade.Columns["Hora"].Width = 66;

            oGrade.Columns["NomeCliente"].Width = 200;
            oGrade.Columns["NomeCliente"].HeaderText = "Cliente";

            oGrade.Columns["Solicitante"].Width = 90;

            oGrade.Columns["ExecutarServico"].Width = 200;
            oGrade.Columns["ExecutarServico"].HeaderText = "Resíduo";

            oGrade.Columns["DataProgramada"].HeaderText = "Data Programada";
            oGrade.Columns["DataProgramada"].Width = 80;
            oGrade.Columns["DataProgramada"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            oGrade.Columns["HoraProgramada"].HeaderText = "Serviço Executado";

            oGrade.Columns["Quantidade"].HeaderText = "Qt";
            oGrade.Columns["Quantidade"].Width = 20;

            oGrade.Columns["ModeloCaminhao"].HeaderText = "Cam";
            oGrade.Columns["ModeloCaminhao"].Width = 80;

            oGrade.Columns["NomeMotorista"].HeaderText = "Motorista";
            oGrade.Columns["NomeMotorista"].Width = 120;

            oGrade.Columns["TipoContrato"].HeaderText = "TP";
            oGrade.Columns["TipoContrato"].Width = 25;

            oGrade.Columns["Unidade"].HeaderText = "Un";
            oGrade.Columns["Unidade"].Width = 25;
        }

        private void Grade_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Delete))
            {
                if (Grade.CurrentRow.Index >= 0 && Grade.Rows.Count > 2)
                {
                    if (Grade.CurrentRow.Index == 0 && 
                        Convert.ToDateTime(Grade.Rows[Grade.CurrentRow.Index].Cells["DataProgramada"].Value) > Convert.ToDateTime(pDataProgramaAbertaAtual))
                    {
                        if (Grade.Rows[Grade.CurrentRow.Index].Cells["Sequencial"].Value.ToString() != "")
                        {
                            DialogResult _result = new DialogResult();                            
                            _result = MessageBox.Show("Confirma exclusão?", geral.UsuarioAtual + " cuidado!", MessageBoxButtons.YesNo);
                            if (_result == DialogResult.Yes)
                            {
                                oReprogramacaoDados.Excluir(Convert.ToInt32(Grade.Rows[Grade.CurrentRow.Index].Cells["Sequencial"].Value));
                                clsProgramacaoDados oProgDados = new clsProgramacaoDados();
                                if (Convert.ToDateTime(Grade.Rows[Grade.CurrentRow.Index + 1].Cells["DataProgramada"].Value) >= Convert.ToDateTime(pDataProgramaAbertaAtual))
                                {
                                    oProgDados.SalvarDataProgramadaAnterior(Grade.Rows[Grade.CurrentRow.Index].Cells["Hora"].Value.ToString(),
                                                                            Grade.Rows[Grade.CurrentRow.Index + 1].Cells["DataProgramada"].Value.ToString(),
                                                                            Grade.Rows[Grade.CurrentRow.Index + 1].Cells["CodigoCliente"].Value.ToString());
                                }
                                else
                                {
                                    oReprogramacaoDados.SalvarDataProgramadaAnterior(Grade.Rows[Grade.CurrentRow.Index].Cells["Hora"].Value.ToString(),
                                                                                     pDataProgramaAbertaAtual,
                                                                                     Grade.Rows[Grade.CurrentRow.Index + 1].Cells["CodigoCliente"].Value.ToString());
                                    oProgDados.SalvarDataProgramadaAnterior(Grade.Rows[Grade.CurrentRow.Index].Cells["Hora"].Value.ToString(),
                                                                            pDataProgramaAbertaAtual,
                                                                            Grade.Rows[Grade.CurrentRow.Index + 1].Cells["CodigoCliente"].Value.ToString());
                                }
                                bSalvarRefresh = true;
                                this.Close();
                            }
                        }
                    }
                    else
                        MessageBox.Show("Data programada escolhida inválida!");
                }
            }
        }

        private void Grade_Sorted(object sender, EventArgs e)
        {
            // repinta depois de ordenar - tava fincando tudo vermelho.
            Pinta();
        }
    }
}
