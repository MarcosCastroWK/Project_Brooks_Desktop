using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using LibSILC;
using SILCNegocios;

namespace formSILC
{
    public partial class frmVisualPrintMTR : Form
    {
        public bool bImprimiu = false;
        public int iCodigoCaminhao = 0;
        public int iCodigoMotorista = 0;
        public DataTable _dt = new DataTable();
        private clsDestinoFinalDados oDestinoFinalDados = new clsDestinoFinalDados();
        private clsCaminhoesDados oCaminhaoDados = new clsCaminhoesDados();
        private clsFuncionarioDados oMotoristaDados = new clsFuncionarioDados();

        public frmVisualPrintMTR()
        {
            InitializeComponent();
        }

        private void frmVisualPrintMTR_Load(object sender, EventArgs e)
        {
            intCopias.VALOR.Text = "3";
            DataTable _dtCaminhoes = new DataTable();
            _dtCaminhoes = oCaminhaoDados.PreencheDataTableCaminhoes("Modelo");
            foreach (DataRow _dr in _dtCaminhoes.Rows)
            {
                if (_dr["Placas"].ToString() != "")
                    cboPlacas.Items.Add(_dr["Placas"].ToString());
            }
            DataTable _dtMotoristas = new DataTable();
            _dtMotoristas = oMotoristaDados.PreencheDataTableFuncionarios("Nome", "", "NaoDemitidos");
            foreach (DataRow _dr in _dtMotoristas.Rows)
            {
                cboMotorista.Items.Add(_dr["Nome"].ToString());
            }

        }

        private void ppd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile("espelhomtr.jpg");
            Point loc = new Point(10, 10);
            e.Graphics.DrawImage(img, loc);

            Font ft = new Font("Tahoma", 8, FontStyle.Bold);

            DateTime _dataEnvio = DateTime.Now;
            string _localentrega = "";
            int _codigoresiduo = 0;
            if (_dt.Rows.Count > 0)
            {
                _dataEnvio = Convert.ToDateTime(_dt.Rows[0][8]);
                _localentrega = _dt.Rows[0][9].ToString();
                _codigoresiduo = Convert.ToInt32(_dt.Rows[0][10]);
            }

            clsResiduos oResiduo = new clsResiduos();
            clsResiduoDados oResiduoDados = new clsResiduoDados();
            oResiduo = oResiduoDados.PegaDados(oResiduo, _codigoresiduo);
            e.Graphics.DrawString(oResiduo.DescricaoGrupo + "/" + oResiduo.DescricaoReduzida, ft, Brushes.Black, 114, 150);
            e.Graphics.DrawString(oResiduo.CodigoResiduoManifesto, ft, Brushes.Black, 540, 150);            
            e.Graphics.DrawString(oResiduo.Classe, ft, Brushes.Black, 670, 150);
            e.Graphics.DrawString(oResiduo.EstadoFisico, ft, Brushes.Black, 114, 180);
            decimal _PesoKgTotal = 0;
            int _itens = 0;
            foreach (DataRow dr in _dt.Rows)
            {
                _itens++;
                _PesoKgTotal = _PesoKgTotal + Convert.ToDecimal(dr["PesoKg"]);
            }
            e.Graphics.DrawString(_PesoKgTotal.ToString("N2") + " KG", ft, Brushes.Black, 550, 180);

            e.Graphics.DrawString(cboMotorista.Text, ft, Brushes.Black, 124, 580);
            e.Graphics.DrawString(cboPlacas.Text, ft, Brushes.Black, 124, 620);

            e.Graphics.DrawString("SC / PALHOÇA", ft, Brushes.Black, 338, 620);

            clsDestinoFinal oDestinoFinal = new clsDestinoFinal();
            string _codigoDestino = oDestinoFinalDados.PegaCodigo(_localentrega);
            if (_codigoDestino != "")
            {
                // pegar dados do destino e mudar no campos do [8 destino]
                oDestinoFinal = oDestinoFinalDados.PegaDados(oDestinoFinal, Convert.ToInt32(_codigoDestino));
                e.Graphics.DrawString(oDestinoFinal.Nome, ft, Brushes.Black, 124, 670);
                e.Graphics.DrawString(oDestinoFinal.CNPJ, ft, Brushes.Black, 124, 710);
                e.Graphics.DrawString(oDestinoFinal.Endereco, ft, Brushes.Black, 124, 746);
                e.Graphics.DrawString(oDestinoFinal.Cidade, ft, Brushes.Black, 124, 780);
                e.Graphics.DrawString(oDestinoFinal.UF, ft, Brushes.Black, 434, 780);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (cboPlacas.Text == "")
            {
                MessageBox.Show("Placa inválida!");
                cboPlacas.Focus();
                return;
            }
            if (cboMotorista.Text == "")
            {
                MessageBox.Show("Motorista inválido!");
                cboMotorista.Focus();
                return;
            }
            int i = 0;
            for (i = 0; i < Convert.ToInt32(intCopias.VALOR.Text); i++)
                ppd.Print();
            if (i > 0)
            {
                bImprimiu = true;
                oCaminhaoDados = new clsCaminhoesDados();
                oMotoristaDados = new clsFuncionarioDados();
                iCodigoCaminhao = oCaminhaoDados.PegaCodigoCaminhao(cboPlacas.Text);
                iCodigoMotorista = oMotoristaDados.PegaCodigoMotorista(cboMotorista.Text);
                this.Close();
            }
        }
        private void intCopias_Leave(object sender, EventArgs e)
        {
            if (intCopias.VALOR.Text == "")
                intCopias.VALOR.Text = "1";
            if (Convert.ToInt32(intCopias.VALOR.Text) > 6)
                intCopias.VALOR.Text = "3";
        }

        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            printPreviewControl.Document = ppd;
            printPreviewControl.Zoom = 1.2;
            printPreviewControl.Show();
        }
    }
}
