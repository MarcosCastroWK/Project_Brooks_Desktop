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
    public partial class frmLocalDTR : Form
    {
        public bool bSalvar = false;
        private clsCacambaDados oContaineres = new clsCacambaDados();
        private BindingSource bindingSource = new BindingSource();

        public frmLocalDTR()
        {
            InitializeComponent();
        }

        private void frmLocalDTR_Load(object sender, EventArgs e)
        {
            clsCacambaDados oContaineres = new clsCacambaDados();
            cboDTRLocal.Items.Add(cboDTRLocal.Text);
            foreach (DataRow _dr in oContaineres.PreencheDataTableCacambas("Numero").Rows)
                cboDTRLocal.Items.Add(_dr["Numero"]);            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //salvar local dtr
            bSalvar = true;
            this.Close();               
        }

        private void cboDTRLocal_Leave(object sender, EventArgs e)
        {
            // procura numero do container - se não existir - não deixar continuar
            cboDTRLocal.Text = cboDTRLocal.Text.ToUpper();
            bool bLocalValido = false;
            foreach(string strItem in cboDTRLocal.Items)
            {
                if (strItem == cboDTRLocal.Text)
                {                    
                    bLocalValido = true;
                    break;
                }
            }
            if (!bLocalValido)
            {
                MessageBox.Show("Local de armazenamento inválido!");
                cboDTRLocal.Text = "";
                cboDTRLocal.Focus();
            }
        }
    }
}
