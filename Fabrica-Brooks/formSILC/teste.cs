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
    public partial class teste : Form
    {
        private clsCorpoNotasFiscaisDados oCorpoNFDados = new clsCorpoNotasFiscaisDados();
        private BindingSource _dbsource = new BindingSource();
        protected TextBox[] _descricao = new TextBox[22];
        protected MOEDA[] _quantidade = new MOEDA[22];
        protected TextBox[] _unidade = new TextBox[22];
        protected MOEDA[] _precounitario = new MOEDA[22];
        protected MOEDA[] _valor = new MOEDA[22];
        public teste()
        {
            InitializeComponent();
        }

        private void teste_Load(object sender, EventArgs e)
        {
            //int k = 3;
            //for (int i = 0; i <= 21; i++)
            //{
            //    _descricao[i] = new TextBox();
            //    _descricao[i].Name = "txtDescricao" + i.ToString();
            //    _descricao[i].Location = new System.Drawing.Point(198, (k * 18));
            //    _descricao[i].Multiline = false;
            //    _descricao[i].Size = new System.Drawing.Size(400, 18);
            //    _descricao[i].MaxLength = 60;
            //    _descricao[i].TabIndex = 0;
            //    _descricao[i].BackColor = Color.Beige;
            //    this.Controls.Add(_descricao[i]);

            //    _quantidade[i] = new MOEDA();
            //    _quantidade[i].Name = "moeQuantidade" + i.ToString();
            //    _quantidade[i].Location = new System.Drawing.Point(600, (k * 18));
            //    _quantidade[i].Size = new System.Drawing.Size(97, 18);
            //    _quantidade[i].TabIndex = 0;
            //    this.Controls.Add(_quantidade[i]);

            //    _unidade[i] = new TextBox();
            //    _unidade[i].Name = "txtUnidade" + i.ToString();
            //    _unidade[i].Location = new System.Drawing.Point(696, (k * 18));
            //    _unidade[i].Multiline = false;
            //    _unidade[i].Size = new System.Drawing.Size(34, 18);
            //    _unidade[i].MaxLength = 60;
            //    _unidade[i].TabIndex = 0;
            //    _unidade[i].BackColor = Color.Beige;
            //    this.Controls.Add(_unidade[i]);     
           
            //    _precounitario[i] = new MOEDA();
            //    _precounitario[i].Name = "moePrecounitario" + i.ToString();
            //    _precounitario[i].Location = new System.Drawing.Point(731, (k * 18));
            //    _precounitario[i].Size = new System.Drawing.Size(97, 18);
            //    _precounitario[i].TabIndex = 0;
            //    this.Controls.Add(_precounitario[i]);
                
            //    _valor[i] = new MOEDA();
            //    _valor[i].Name = "moeValor" + i.ToString();
            //    _valor[i].Location = new System.Drawing.Point(828, (k * 18));
            //    _valor[i].Size = new System.Drawing.Size(97, 18);
            //    _valor[i].TabIndex = 0;
            //    this.Controls.Add(_valor[i]);

            //    k++;

            //}
            //int j = 0;
            //foreach (DataRow _dr in oCorpoNFDados.PreencheDataTable("Linha", "25107").Rows)
            //{
            //    _descricao[j].Text = _dr["Descricao"].ToString();

            //    if (_dr["Quantidade"].ToString() != "" && _dr["Quantidade"].ToString() != "0")
            //        _quantidade[j].VALOR.Text = _dr["Quantidade"].ToString();

            //    _unidade[j].Text = _dr["Unidade"].ToString();

            //    if (_dr["PrecoUnitario"].ToString() != "" && _dr["PrecoUnitario"].ToString() != "0,0000")
            //        _precounitario[j].VALOR.Text = _dr["PrecoUnitario"].ToString();

            //    if (_dr["Valor"].ToString() != "" && _dr["Valor"].ToString() != "0,0000")
            //        _valor[j].VALOR.Text = _dr["Valor"].ToString();
                
            //    j++;
            //}
            
            //_dbsource.DataSource = oCorpoNFDados.PreencheDataTable("Linha", "48120");
            //dataGridView1.DataSource = _dbsource.DataSource;

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            //
        }
    }
}
