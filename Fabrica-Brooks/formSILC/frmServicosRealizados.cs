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
    public partial class frmServicosRealizados : Form
    {
        clsLancamentoMTRDados oLancMTRDados = new clsLancamentoMTRDados();
        DataTable _dt = new DataTable();

        private BindingSource bindingSource = new BindingSource();

        public frmServicosRealizados()
        {
            InitializeComponent();
        }

        private void frmServicosRealizados_Load(object sender, EventArgs e)
        {
            dtpDataInicial.Text = DateTime.Now.AddDays(-30).ToShortDateString();
            dtpFinal.Text = DateTime.Now.ToShortDateString();
            if (lblNrCodigo.Text != "000000" && lblNrCodigo.Text != "" && lblNrCodigo.Text != "0")
            {
                _dt = oLancMTRDados.PreencheDataTableServicosRealizados("DataColocacao desc", Convert.ToInt32(lblNrCodigo.Text), dtpDataInicial.Text, dtpFinal.Text, true);
                _dt.Merge(oLancMTRDados.PreencheDataTableServicosRealizados("DataRetirada desc", Convert.ToInt32(lblNrCodigo.Text), dtpDataInicial.Text, dtpFinal.Text, false));
                bindingSource.DataSource = _dt;
                Grade1.DataSource = bindingSource.DataSource;
                EstiloGrades(Grade1);
            }
        }

        private void EstiloGrades(DataGridView oGrade)
        {

            oGrade.Columns["NumeroLancamento"].Width = 70;
            oGrade.Columns["NumeroLancamento"].HeaderText = "Nr.Lançto";
            oGrade.Columns["NumeroLancamento"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            oGrade.Columns["NomeCliente"].Width = 200;
            oGrade.Columns["NomeCliente"].HeaderText = "Nome fantasia";

            oGrade.Columns["CodigoCliente"].Width = 60;
            oGrade.Columns["CodigoCliente"].HeaderText = "Código Cliente";
            //for (int i = 0; i < oGrade.Rows.Count; i++)
            //    oGrade.Rows[i].Cells["CodigoCliente"].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            oGrade.Columns["NumeroCaixa"].Width = 60;
            oGrade.Columns["NumeroCaixa"].HeaderText = "Container";

            oGrade.Columns["DataColocacao"].Width = 70;
            oGrade.Columns["DataColocacao"].HeaderText = "Data Colocação";
            oGrade.Columns["DataColocacao"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //for (int i = 0; i < oGrade.Rows.Count; i++)
            //    oGrade.Rows[i].Cells["DataColocacao"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            oGrade.Columns["DataRetirada"].Width = 70;
            oGrade.Columns["DataRetirada"].HeaderText = "Data Retirada";
            oGrade.Columns["DataRetirada"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            oGrade.Columns["NumeroMTR"].Width = 70;
            oGrade.Columns["NumeroMTR"].HeaderText = "Número MTR";

            oGrade.Columns["DescricaoResiduo"].Width = 250;
            oGrade.Columns["DescricaoResiduo"].HeaderText = "Descrição Resíduo";

            oGrade.Columns["Quantidade"].Width = 70;

            oGrade.Columns["Unidade"].Width = 70;

            oGrade.Columns["Destino Final"].Width = 120;

            oGrade.Columns["Observacao"].Width = 232;
            oGrade.Columns["Observacao"].HeaderText = "Observação";

            for (int i = 0; i < oGrade.Rows.Count; i++)
            {
                oGrade.Rows[i].Cells["NumeroLancamento"].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                oGrade.Rows[i].Cells["CodigoCliente"].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                oGrade.Rows[i].Cells["DataColocacao"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                oGrade.Rows[i].Cells["NumeroMTR"].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                oGrade.Rows[i].Cells["Quantidade"].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

        }

        private void frmServicosRealizados_Resize(object sender, EventArgs e)
        {
            Grade1.Height = this.Height - 120;
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            if (lblNrCodigo.Text != "000000" && lblNrCodigo.Text != "" && lblNrCodigo.Text != "0")
            {
                _dt = oLancMTRDados.PreencheDataTableServicosRealizados("DataColocacao desc", Convert.ToInt32(lblNrCodigo.Text), dtpDataInicial.Text, dtpFinal.Text, true);
                _dt.Merge(oLancMTRDados.PreencheDataTableServicosRealizados("DataRetirada desc", Convert.ToInt32(lblNrCodigo.Text), dtpDataInicial.Text, dtpFinal.Text, false));
                bindingSource.DataSource = _dt;
                Grade1.DataSource = bindingSource.DataSource;
                EstiloGrades(Grade1);
            }
        }
    }
}
