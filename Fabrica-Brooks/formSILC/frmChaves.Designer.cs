namespace formSILC
{
    partial class frmChaves
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gridChaves = new System.Windows.Forms.DataGridView();
            this.btnAddRow = new System.Windows.Forms.Button();
            this.chave = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridChaves)).BeginInit();
            this.SuspendLayout();
            // 
            // gridChaves
            // 
            this.gridChaves.AllowUserToAddRows = false;
            this.gridChaves.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridChaves.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chave,
            this.valor});
            this.gridChaves.Location = new System.Drawing.Point(12, 54);
            this.gridChaves.Name = "gridChaves";
            this.gridChaves.Size = new System.Drawing.Size(763, 384);
            this.gridChaves.TabIndex = 0;
            // 
            // btnAddRow
            // 
            this.btnAddRow.Location = new System.Drawing.Point(700, 22);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(75, 23);
            this.btnAddRow.TabIndex = 1;
            this.btnAddRow.Text = "Adicionar";
            this.btnAddRow.UseVisualStyleBackColor = true;
            this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
            // 
            // chave
            // 
            this.chave.HeaderText = "Chave";
            this.chave.Name = "chave";
            // 
            // valor
            // 
            this.valor.HeaderText = "Valor";
            this.valor.Name = "valor";
            // 
            // frmChaves
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnAddRow);
            this.Controls.Add(this.gridChaves);
            this.Name = "frmChaves";
            this.Text = "Chaves";
            ((System.ComponentModel.ISupportInitialize)(this.gridChaves)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridChaves;
        private System.Windows.Forms.Button btnAddRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn chave;
        private System.Windows.Forms.DataGridViewTextBoxColumn valor;
    }
}