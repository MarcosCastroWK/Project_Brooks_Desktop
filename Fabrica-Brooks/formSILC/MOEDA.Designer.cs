namespace formSILC
{
    partial class MOEDA
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.VALOR = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // VALOR
            // 
            this.VALOR.BackColor = System.Drawing.SystemColors.Info;
            this.VALOR.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VALOR.Location = new System.Drawing.Point(0, 0);
            this.VALOR.MaxLength = 12;
            this.VALOR.Name = "VALOR";
            this.VALOR.Size = new System.Drawing.Size(87, 21);
            this.VALOR.TabIndex = 174;
            this.VALOR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.VALOR.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoBarras_KeyPress);
            this.VALOR.Leave += new System.EventHandler(this.VALOR_Leave);
            // 
            // MOEDA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.VALOR);
            this.Name = "MOEDA";
            this.Size = new System.Drawing.Size(89, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox VALOR;
    }
}
