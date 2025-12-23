using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Resources;
using System.Text;
using System.Windows.Forms;

namespace ExportaWebSILC
{
    partial class ExportaWebSILC
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
            this.components = new System.ComponentModel.Container();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblImportacaoAutomatica = new System.Windows.Forms.Label();
            this.lblServidor = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(4, 38);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(824, 430);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 18000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblImportacaoAutomatica
            // 
            this.lblImportacaoAutomatica.AutoSize = true;
            this.lblImportacaoAutomatica.Location = new System.Drawing.Point(12, 12);
            this.lblImportacaoAutomatica.Name = "lblImportacaoAutomatica";
            this.lblImportacaoAutomatica.Size = new System.Drawing.Size(384, 13);
            this.lblImportacaoAutomatica.TabIndex = 1;
            this.lblImportacaoAutomatica.Text = "Também importação automática de Lançamentos para o sistema novo webSILC";
            // 
            // lblServidor
            // 
            this.lblServidor.AutoSize = true;
            this.lblServidor.Location = new System.Drawing.Point(502, 11);
            this.lblServidor.Name = "lblServidor";
            this.lblServidor.Size = new System.Drawing.Size(44, 13);
            this.lblServidor.TabIndex = 2;
            this.lblServidor.Text = "servidor";
            // 
            // ExportaWebSILC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 373);
            this.Controls.Add(this.lblServidor);
            this.Controls.Add(this.lblImportacaoAutomatica);
            this.Controls.Add(this.webBrowser1);
            this.Name = "ExportaWebSILC";
            this.Text = "Aplicativo que exporta clientes para página da BROOKS    versão de 05/10/2023-r0";
            this.Load += new System.EventHandler(this.ExportaWebSILC_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Timer timer1;
        private Label lblImportacaoAutomatica;
        private Label lblServidor;
    }
}

