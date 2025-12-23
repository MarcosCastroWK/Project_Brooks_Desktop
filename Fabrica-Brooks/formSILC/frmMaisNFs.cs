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
    public partial class frmMaisNFs : Form
    {
        private BindingSource bindingSource = new BindingSource();
        clsNotasFiscais oNFs = new clsNotasFiscais();
        clsNotasFiscaisDados oNFsDados = new clsNotasFiscaisDados();
        DataTable _dt = new DataTable();

        public frmMaisNFs()
        {
            InitializeComponent();
        }

        private void frmMaisNFs_Load(object sender, EventArgs e)
        {
            GradeNFs.AutoGenerateColumns = false;
            _dt = oNFsDados.PreencheDataTableOrdem("DataEmissao desc", Convert.ToInt32(txtCodigo.Text));
            bindingSource.DataSource = _dt;
            foreach (DataRow _dr in _dt.Rows)
                if (_dr["Cancelada"].ToString() == "1")
                    _dr["Situacao"] = "Cancelada";
            GradeNFs.DataSource = bindingSource.DataSource;
        }

        private void GradeNFs_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                geral.NumeroNF = Convert.ToInt32(GradeNFs.Rows[e.RowIndex].Cells["NumeroNF"].Value);
                this.Close();
            }
        }
    }
}
