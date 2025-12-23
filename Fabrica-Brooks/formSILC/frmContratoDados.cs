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
    public partial class frmContratoDados : Form
    {

        clsClienteDados oClienteDados = new clsClienteDados();
        clsClientes oCliente = new clsClientes();
        clsContratoResiduos oContratoResiduos = new clsContratoResiduos();
        clsContratoResiduosDados oContratoResiduosDados = new clsContratoResiduosDados();
        clsContratos oContrato = new clsContratos();
        clsContratosDados oContratoDados = new clsContratosDados();
        clsContratosReajustes oContratoReajustes = new clsContratosReajustes();
        clsContratosReajustesDados oContratoReajustesDados = new clsContratosReajustesDados();

        DataTable _dt = new DataTable();
        private BindingSource bindingSource = new BindingSource();

        int CodigoClienteSelecionado = 0;

        public frmContratoDados()
        {
            InitializeComponent();
        }

        private void frmContratoDados_Load(object sender, EventArgs e)
        {
            CodigoClienteSelecionado = Convert.ToInt32(lblNrCodigo.Text);
            PopularComboLimiteFranquia();
            MostraDados();
        }

        private void EstiloGrades(DataGridView oGrade)
        {
            oGrade.Columns["CodigoContrato"].Visible = false;

            oGrade.Columns["Data"].Width = 70;
            oGrade.Columns["Data"].HeaderText = "Data";
            oGrade.Columns["Data"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            oGrade.Columns["NumeroContrato"].Width = 70;
            oGrade.Columns["NumeroContrato"].HeaderText = "Nº Contrato";
            oGrade.Columns["NumeroContrato"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
 
            oGrade.Columns["Valor"].Width = 100;
            oGrade.Columns["Valor"].HeaderText = "Valor";
            oGrade.Columns["Valor"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            oGrade.Columns["Situacao"].Width = 180;
            oGrade.Columns["TipoNegociacao"].Width = 260;

            for (int i = 0; i < oGrade.Rows.Count; i++)
            {
                oGrade.Rows[i].Cells["Data"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                oGrade.Rows[i].Cells["NumeroContrato"].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                oGrade.Rows[i].Cells["Valor"].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void MostraDados()
        {
            oCliente = oClienteDados.PegaDados(oCliente, CodigoClienteSelecionado);
            lblNrCodigo.Text = oCliente.Codigo.ToString("000000");
            txtNomeFantasia.Text = oCliente.NomeFantasia;

            oContrato = oContratoDados.PegaDados(oContrato, 0, oCliente.Codigo);
            if (oContrato.DataRecisao == null)
                oContrato.DataRecisao = "";
            oContrato.DataRecisao = geral.DataFormatada(oContrato.DataRecisao);
            if (oContrato.DataRecisao == "")
            {
                txtCodigoContrato.Text = oContrato.Codigo.ToString();
                txtDescricao.Text = oContrato.DescricaoHistorico;
                txtObservacao.Text = oContrato.Observacao;
                DataRescisao.Text = oContrato.DataRecisao;
                cboDocumentoRescisao.Text = oContrato.SituacaoRecisao;
                DataRegistro.Text = oContrato.DataRegistro;
                DataInicio.Text = oContrato.DataInicio;
                DataTermino.Text = oContrato.DataTermino;
                txtMotivoRescisao.Text = oContrato.MotivoRescisao;
                txtContratoInicial.Text = oContrato.NumeroContrato.ToString();
                txtDiaMesAniver.Text = oContrato.AniversarioReajuste;
                DataReajuste.Text = oContrato.DataReajuste;
                txtIndiceReajuste.Text = oContrato.IndiceReajuste;
                txtValorContratoAtual.Text = oContrato.ValorContrato.ToString();
                txtDiaVencimento.Text = oContrato.DiaVencimento.ToString();

                 if (cboLimitar.Items.Count > 0)
                {
                    cboLimitar.SelectedValue = oContrato.LimitarFranquia;
                }

                if (oContrato.Codigo > 0)
                {
                    _dt = oContratoReajustesDados.PreencheDataTable("Data desc", oContrato.Codigo);
                    foreach (DataRow _dr0 in _dt.Rows)
                    {
                        _dr0["Valor"] = Convert.ToDecimal(Convert.ToDecimal(_dr0["Valor"]).ToString("n2"));
                    }
                    bindingSource.DataSource = _dt;
                    Grade1.DataSource = bindingSource.DataSource;

                    EstiloGrades(Grade1);

                    GradeResiduos.AutoGenerateColumns = false;
                    if (_dt.Rows.Count > 0)
                    {
                        string DataUltimoReajuste = _dt.Rows[0]["Data"].ToString();
                        _dt = oContratoResiduosDados.PreencheDataTableContratoResiduos("CodigoResiduo", oContrato.Codigo, DataUltimoReajuste, oContrato.CodigoCliente);
                        foreach (DataRow _dr in _dt.Rows)
                        {
                            _dr["ValorUnitario"] = Convert.ToDecimal(_dr["ValorUnitario"]) / 100;
                        }
                        bindingSource.DataSource = _dt;
                        GradeResiduos.DataSource = bindingSource.DataSource;
                    }
                }
            }
           
        }

        private void PopularComboLimiteFranquia()
        {
            var listaOpcoes = new List<clsLimitarFranquia>
            {
                new clsLimitarFranquia { Texto = "Não Limitar", Valor = 0 },
                new clsLimitarFranquia { Texto = "Limitar", Valor = 1 }
            };

            cboLimitar.DataSource = listaOpcoes;
            cboLimitar.DisplayMember = "Texto";
            cboLimitar.ValueMember = "Valor";
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }
    }
}
