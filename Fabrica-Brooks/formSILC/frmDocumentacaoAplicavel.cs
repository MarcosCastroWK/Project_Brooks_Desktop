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
    public partial class frmDocumentacaoAplicavel : Form
    {
        private clsDocumentacaoAplicavel oDocAplic = new clsDocumentacaoAplicavel();
        private clsDocumentacaoAplicavelDados oDocAplicDados = new clsDocumentacaoAplicavelDados();
        private clsDDR_Conferencia oDDRConferenciaDados = new clsDDR_Conferencia();
        private BindingSource bindingSource = new BindingSource();
        private DataTable _dt = new DataTable();
        private int regs = 0;
        private int pagina = 0;
        private int LimiteLnImpressora = 1040;
        private enum _TitulopRelatorio { RGR, DDR, PLANFAT, NaoSelecionado };

        _TitulopRelatorio TituloRelatorio = _TitulopRelatorio.NaoSelecionado;
        public frmDocumentacaoAplicavel()
        {
            InitializeComponent();
        }

        private void frmDocumentacaoAplicavel_Load(object sender, EventArgs e)
        {
            TituloRelatorio = _TitulopRelatorio.NaoSelecionado;

            this.Top = 0;
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - 50;
            Grade.Height = this.Height - 150;
            Grade.AutoGenerateColumns = false;

            //this.Grade.RowsDefaultCellStyle.BackColor = Color.Bisque;
            this.Grade.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige;
            PreencheGrade("EnviarPlanFatAteDia Desc, NomeFantasia");

            cboFiltro.Text = "Código";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            TituloRelatorio = _TitulopRelatorio.NaoSelecionado;
            PreencheGrade("EnviarPlanFatAteDia Desc, NomeFantasia");
        }
        private void PreencheGrade(string pOrdem)
        {
            oDocAplicDados = new clsDocumentacaoAplicavelDados();
            if (int2Mes.VALOR.Text != "" && int2Ano.VALOR.Text != "" && int2Mes.VALOR.Text != "0" && int2Ano.VALOR.Text != "0")
            {
                 _dt = oDocAplicDados.PreencheDataTable(pOrdem,
                                                        Convert.ToInt32(int2Mes.VALOR.Text),
                                                        Convert.ToInt32("20" + int2Ano.VALOR.Text), true);
                 foreach (DataRow dr in _dt.Rows)
                 {
                    if (dr["DDR_Conferindo"].ToString() == "1")
                        dr["DDR_Conferindo"] = true;
                    if (dr["PlanFatEnviada"].ToString() == "1")
                        dr["PlanFatEnviada"] = true;
                    if (dr["DDRConferida"].ToString() == "1")
                        dr["DDRConferida"] = true;
                    if (dr["RelGerEnviado"].ToString() == "1")
                        dr["RelGerEnviado"] = true;
                    if (dr["CodigoCliente"].ToString() == "")
                        dr.Delete();
                    if ((dr["Mes"].ToString() == "00" || dr["Mes"].ToString() == "0" || dr["Mes"].ToString() == "") && dr["Ano"].ToString() == "2000")
                    {
                        dr["Mes"] = Convert.ToInt32(int2Mes.VALOR.Text);
                        dr["Ano"] = Convert.ToInt32(int2Ano.VALOR.Text);
                        oDocAplic = oDocAplicDados.PegaDados(Convert.ToInt32(int2Mes.VALOR.Text), Convert.ToInt32("20" + int2Ano.VALOR.Text), 
                                                             Convert.ToInt32(dr["CodigoCliente"]));
                        if (oDocAplic.Sequencial > 0)
                        {
                            dr["AguardarAprovacaoPlanFat"] = oDocAplic.AguardarAprovacaoPlanFat;
                            dr["AguardarOrdemCompra"] = oDocAplic.AguardarOrdemCompra;
                            dr["ConferirDDRAteDia"] = oDocAplic.ConferirDDRAteDia;
                            dr["DDRConferida"] = oDocAplic.DDRConferida;
                            dr["EnviarCDFBrooks"] = oDocAplic.EnviarCDFBrooks;
                            dr["EnviarPlanFatAteDia"] = oDocAplic.EnviarPlanFatAteDia;
                            dr["EnviarRelGer"] = oDocAplic.EnviarRelGer;
                            dr["EnviarRGRAteDia"] = oDocAplic.EnviarRGRAteDia;
                            dr["PeriodoApuracao"] = oDocAplic.PeriodoApuracao;
                            dr["PlanFatEnviada"] = oDocAplic.PlanFatEnviada;
                            dr["RelGerEnviado"] = oDocAplic.RelGerEnviado;
                            dr["Sequencial"] = oDocAplic.Sequencial;
                        }
                    }
                 }
                 bindingSource.DataSource = _dt;
                 Grade.DataSource = bindingSource;
            }
            else if ((int2Mes.VALOR.Text == "" && int2Ano.VALOR.Text == "") || (int2Mes.VALOR.Text == "0" && int2Ano.VALOR.Text == "0"))
            {
                if (int2Mes.VALOR.Text == "")
                    int2Mes.VALOR.Text = "0";
                if (int2Ano.VALOR.Text == "")
                    int2Ano.VALOR.Text = "0";
                int i = 0;
                _dt = new DataTable();
                _dt = oDocAplicDados.PreencheDataTable(pOrdem, 0, 2000, true);
                foreach (DataRow dr in _dt.Rows)
                {
                    oDocAplic = new clsDocumentacaoAplicavel();
                    if (dr["CodigoCliente"].ToString() != "")
                        oDocAplic.CodigoCliente = Convert.ToInt32(dr["CodigoCliente"]);

                    if (dr["AguardarAprovacaoPlanFat"].ToString() != "")
                        oDocAplic.AguardarAprovacaoPlanFat = Convert.ToInt32(dr["AguardarAprovacaoPlanFat"]);
                    if (dr["AguardarOrdemCompra"].ToString() != "")
                        oDocAplic.AguardarOrdemCompra = Convert.ToInt32(dr["AguardarOrdemCompra"]);
                    oDocAplic.Ano = 2000;
                    oDocAplic.ConferindoDDR = 0;
                    if (dr["ConferirDDRAteDia"].ToString() != "")
                        oDocAplic.ConferirDDRAteDia = Convert.ToInt32(dr["ConferirDDRAteDia"]);
                    if (dr["EnviarPlanFatAteDia"].ToString() != "")
                        oDocAplic.EnviarPlanFatAteDia = Convert.ToInt32(dr["EnviarPlanFatAteDia"]);
                    if (dr["EnviarRGRAteDia"].ToString() != "")
                        oDocAplic.EnviarRGRAteDia = Convert.ToInt32(dr["EnviarRGRAteDia"]);
                    oDocAplic.Mes = 0;
                    oDocAplic.PeriodoApuracao = dr["PeriodoApuracao"].ToString();
                    if (dr["EnviarRelGer"].ToString() != "")
                        oDocAplic.EnviarRelGer = Convert.ToInt32(dr["EnviarRelGer"]);
                    if (dr["PlanFatEnviada"].ToString() != "")
                        oDocAplic.PlanFatEnviada = Convert.ToInt32(dr["PlanFatEnviada"]);
                    if (dr["DDRConferida"].ToString() != "")
                        oDocAplic.DDRConferida = Convert.ToInt32(dr["DDRConferida"]);
                    if (dr["RelGerEnviado"].ToString() != "")
                        oDocAplic.RelGerEnviado = Convert.ToInt32(dr["RelGerEnviado"]);
                    if (oDocAplicDados.DadoExiste(oDocAplic.CodigoCliente, 0, 2000) == "Incluir")
                    {
                        i++;
                        oDocAplicDados.Inserir(oDocAplic, 0);
                    }
                }
                _dt = new DataTable();
                _dt = oDocAplicDados.PreencheDataTable(pOrdem, 0, 2000, true);
                foreach (DataRow dr in _dt.Rows)
                {
                    if (dr["Ano"].ToString() == "2000")
                        dr["Ano"] = 0;
                    if (dr["CodigoCliente"].ToString() == "")
                        dr.Delete();
                }
                bindingSource.DataSource = _dt;
                Grade.DataSource = bindingSource;
            }
            lblLinhas.Text = "Linhas grade: " + _dt.Rows.Count.ToString();
        }

        private void Grade_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //DDR_Conferindo
            if (e.RowIndex >= 0)
            {
                if (int2Mes.VALOR.Text == "")
                    int2Mes.VALOR.Text = "0";
                if (int2Ano.VALOR.Text == "")
                    int2Ano.VALOR.Text = "0";

                if (e.ColumnIndex == 12)
                {
                    oDDRConferenciaDados = new clsDDR_Conferencia();
                    var chkCell = (DataGridViewCheckBoxCell)Grade.Rows[e.RowIndex].Cells["DDR_Conferindo"];
                    // Colocar bloqueio na página
                    if (!oDDRConferenciaDados.MesEmConferencia(Convert.ToInt32(Grade.Rows[e.RowIndex].Cells["CodigoCliente"].Value),
                                                               Convert.ToInt32(int2Mes.VALOR.Text),
                                                               2000 + Convert.ToInt32(int2Ano.VALOR.Text)))
                    {
                        oDDRConferenciaDados.Inserir(Convert.ToInt32(Grade.Rows[e.RowIndex].Cells["CodigoCliente"].Value),
                                                     Convert.ToInt32(int2Mes.VALOR.Text),
                                                     2000 + Convert.ToInt32(int2Ano.VALOR.Text));
                    }
                    else
                    {
                        // retirar bloqueio na página
                        oDDRConferenciaDados.Excluir(Convert.ToInt32(Grade.Rows[e.RowIndex].Cells["CodigoCliente"].Value),
                                                     Convert.ToInt32(int2Mes.VALOR.Text),
                                                     2000 + Convert.ToInt32(int2Ano.VALOR.Text));
                    }
                }
                if (e.ColumnIndex == 15)
                { 
                    //botão Ok
                    var butCell = (DataGridViewButtonCell)Grade.Rows[e.RowIndex].Cells["Ok"];
                    if (butCell.Value.ToString() == "Ok")
                    {
                        // salvar
                        butCell.ReadOnly = true;
                        try
                        {
                            oDocAplic = new clsDocumentacaoAplicavel();
                            if (Grade.Rows[e.RowIndex].Cells["AguardarAprovacaoPlanFat"].Value.ToString() != "")
                                oDocAplic.AguardarAprovacaoPlanFat = Convert.ToInt32(Grade.Rows[e.RowIndex].Cells["AguardarAprovacaoPlanFat"].Value);
                            if (Grade.Rows[e.RowIndex].Cells["AguardarOrdemCompra"].Value.ToString() != "")
                                oDocAplic.AguardarOrdemCompra = Convert.ToInt32(Grade.Rows[e.RowIndex].Cells["AguardarOrdemCompra"].Value);
                            oDocAplic.Ano = 2000;
                            if (Grade.Rows[e.RowIndex].Cells["Ano"].Value.ToString() != "" && Grade.Rows[e.RowIndex].Cells["Ano"].Value.ToString() != "0")
                            {
                                oDocAplic.Ano = Convert.ToInt32(Grade.Rows[e.RowIndex].Cells["Ano"].Value);
                                if (oDocAplic.Ano < 2000)
                                    oDocAplic.Ano = oDocAplic.Ano + 2000;
                            }
                            oDocAplic.CodigoCliente = Convert.ToInt32(Grade.Rows[e.RowIndex].Cells["CodigoCliente"].Value);
                            if (Grade.Rows[e.RowIndex].Cells["DDR_Conferindo"].Value.ToString() != "")
                                oDocAplic.ConferindoDDR = Convert.ToInt32(Grade.Rows[e.RowIndex].Cells["DDR_Conferindo"].Value);
                            if (Grade.Rows[e.RowIndex].Cells["ConferirDDRAteDia"].Value.ToString() != "")
                                oDocAplic.ConferirDDRAteDia = Convert.ToInt32(Grade.Rows[e.RowIndex].Cells["ConferirDDRAteDia"].Value);
                            if (Grade.Rows[e.RowIndex].Cells["EnviarPlanFatAteDia"].Value.ToString() != "")
                                oDocAplic.EnviarPlanFatAteDia = Convert.ToInt32(Grade.Rows[e.RowIndex].Cells["EnviarPlanFatAteDia"].Value);
                            if (Grade.Rows[e.RowIndex].Cells["EnviarRGRAteDia"].Value.ToString() != "")
                                oDocAplic.EnviarRGRAteDia = Convert.ToInt32(Grade.Rows[e.RowIndex].Cells["EnviarRGRAteDia"].Value);
                            if (Grade.Rows[e.RowIndex].Cells["PlanFatEnviada"].Value.ToString() != "")
                                oDocAplic.PlanFatEnviada = Convert.ToInt32(Grade.Rows[e.RowIndex].Cells["PlanFatEnviada"].Value);
                            if (Grade.Rows[e.RowIndex].Cells["DDRConferida"].Value.ToString() != "")
                                oDocAplic.DDRConferida = Convert.ToInt32(Grade.Rows[e.RowIndex].Cells["DDRConferida"].Value);
                            if (Grade.Rows[e.RowIndex].Cells["RelGerEnviado"].Value.ToString() != "")
                                oDocAplic.RelGerEnviado = Convert.ToInt32(Grade.Rows[e.RowIndex].Cells["RelGerEnviado"].Value);
                            if (Grade.Rows[e.RowIndex].Cells["Mes"].Value.ToString() != "")
                                oDocAplic.Mes = Convert.ToInt32(Grade.Rows[e.RowIndex].Cells["Mes"].Value);
                            oDocAplic.PeriodoApuracao = Grade.Rows[e.RowIndex].Cells["PeriodoApuracao"].Value.ToString();

                            // verificar se existe mês e ano 0, 0 deste cliente
                            clsDocumentacaoAplicavel oDocAplicInsere = new clsDocumentacaoAplicavel();
                            oDocAplicInsere = oDocAplicDados.PegaDados(Convert.ToInt16(int2Mes.VALOR.Text), 2000 + Convert.ToInt16(int2Ano.VALOR.Text), oDocAplic.CodigoCliente);
                            if (oDocAplicInsere.Sequencial == 0)
                            {
                                if (oDocAplic.Ano == 0)
                                    oDocAplic.Ano = 2000;
                                oDocAplicDados.Inserir(oDocAplic);
                            }
                            else if (oDocAplicInsere.Sequencial > 0)
                            {
                                oDocAplic.Sequencial = oDocAplicInsere.Sequencial;
                                oDocAplicDados.Alterar(oDocAplic);
                            }
                            if (Convert.ToInt16(int2Mes.VALOR.Text) > 0 && Convert.ToInt16(int2Ano.VALOR.Text) > 0)
                            {
                                oDocAplicInsere = oDocAplicDados.PegaDados(0, 2000, oDocAplic.CodigoCliente);
                                if (oDocAplicInsere.Sequencial == 0)
                                    oDocAplicDados.Inserir(oDocAplic);
                            }
                        }
                        finally
                        {
                            butCell.ReadOnly = false;
                            butCell.Style.BackColor = Color.Gainsboro;
                        }
                    }
                }
            }
        }

        private void Grade_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (Grade.CurrentCell is DataGridViewCheckBoxCell)
            {
                Grade.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void chkTodosClientes_CheckedChanged(object sender, EventArgs e)
        {
            /*
            _dt = new DataTable();
            _dt = oDocAplicDados.PreencheDataTable("EnviarPlanFatAteDia, NomeFantasia", 0, 0, !chkTodosClientes.Checked);
            bindingSource.DataSource = _dt;
            Grade.DataSource = bindingSource;
            */
        }

        private void btnImprimirSelecionados_Click(object sender, EventArgs e)
        {
            // imprimir selecionados
            ImprimirSelecionados();
        }

        private void ImprimirSelecionados()
        {
            regs = 0;
            pagina = 0;            
            ((Form)ppd).StartPosition = FormStartPosition.CenterScreen;
            ((Form)ppd).Text = "Visualizador";
            ((Form)ppd).WindowState = FormWindowState.Maximized;
            ppd.Document = pd;
            ppd.ShowDialog();        
        }

        private void pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            pagina = 0;
            regs = 0;
            float cl = 10.0F;
            float ln = 12.0F;
            string str = "";
            char pad = '─';
            Font ft = new Font("Courier New", 9);
            e.Graphics.DrawString(geral.NomeSistema, ft, Brushes.Black, cl, ln);

            ln = ln + 12.0F;
            e.Graphics.DrawString("Relatório Documentação Aplicável", ft, Brushes.Black, cl, ln);

            string sTituloRel = "";
            if (TituloRelatorio == _TitulopRelatorio.RGR)
            {
                sTituloRel = "CLIENTES EMITIR RGR";
                cl = 300;
            }
            else if (TituloRelatorio == _TitulopRelatorio.DDR)
            {
                sTituloRel = "CLIENTES CONFERIR DDR";
                cl = 290;
            }
            else if (TituloRelatorio == _TitulopRelatorio.PLANFAT)
            {
                sTituloRel = "CLIENTES EMITIR PLANFAT";
                cl = 280;
            }
            if (int2Mes.VALOR.Text != "" && int2Mes.VALOR.Text != "0" && int2Ano.VALOR.Text != "" && int2Ano.VALOR.Text != "0")
            {
                sTituloRel = sTituloRel + " " + int2Mes.VALOR.Text + "/" + int2Ano.VALOR.Text;
                cl = cl - 30;
            }
            ft = new Font("Courier New", 14);
            ln = ln + 16;
            e.Graphics.DrawString(sTituloRel, ft, Brushes.Black, cl, ln);
            ft = new Font("Courier New", 9);
            e.Graphics.DrawString("Emissão: " + DateTime.Now.ToShortDateString(), ft, Brushes.Black, 656, ln);

            cl = 10.0F;
            ln = ln + 20.0F;
            ft = new Font("Courier New", 7);
            e.Graphics.DrawString(str.PadLeft(132, pad), ft, Brushes.Black, 10, ln);
            
            ln = ln + 12.0F;
            e.Graphics.DrawString("Código", ft, Brushes.Black, cl, ln);          // Código do Cliente
            cl = cl + 40;
            e.Graphics.DrawString("Nome fantasia", ft, Brushes.Black, cl, ln);   // Nome fantasia
            cl = cl + 235;
            e.Graphics.DrawString("Período", ft, Brushes.Black, cl, ln);         // Período de apuraçao
            cl = cl + 55;
            e.Graphics.DrawString("Enviar", ft, Brushes.Black, cl, ln);          // Enviar PlanFat até dia
            cl = cl + 50;
            e.Graphics.DrawString("Aguardar", ft, Brushes.Black, cl, ln);        // Aguardar aprovação PlanFat
            cl = cl + 60;
            e.Graphics.DrawString("PlanFat", ft, Brushes.Black, cl, ln);         // PlanFat enviada
            cl = cl + 49;
            e.Graphics.DrawString("Aguardar", ft, Brushes.Black, cl, ln);        // Aguardar Ordem de Compra
            cl = cl + 55;
            e.Graphics.DrawString("Conferir", ft, Brushes.Black, cl, ln);        // Conferir DDR até dia
            cl = cl + 52;
            e.Graphics.DrawString("DDR", ft, Brushes.Black, cl, ln);             // DDR Conferida
            cl = cl + 60;
            e.Graphics.DrawString("DDR", ft, Brushes.Black, cl, ln);             // DDR Liberada
            cl = cl + 53;
            e.Graphics.DrawString("Enviar", ft, Brushes.Black, cl, ln);          // Enviar RGR Até dia
            cl = cl + 40;
            e.Graphics.DrawString("RGR", ft, Brushes.Black, cl, ln);             // RGR enviada

            ln = ln + 12;
            cl = 285;
            e.Graphics.DrawString("Apuração", ft, Brushes.Black, cl, ln);
            cl = cl + 55;
            e.Graphics.DrawString("PlanFat", ft, Brushes.Black, cl, ln);
            cl = cl + 50;
            e.Graphics.DrawString("Aprovação", ft, Brushes.Black, cl, ln);
            cl = cl + 60;
            e.Graphics.DrawString("Enviada", ft, Brushes.Black, cl, ln);
            cl = cl + 49;
            e.Graphics.DrawString("Ordem de", ft, Brushes.Black, cl, ln);
            cl = cl + 55;
            e.Graphics.DrawString("DDR Até", ft, Brushes.Black, cl, ln);
            cl = cl + 52;
            e.Graphics.DrawString("Conferida", ft, Brushes.Black, cl, ln);
            cl = cl + 60;
            e.Graphics.DrawString("Liberada", ft, Brushes.Black, cl, ln);
            cl = cl + 53;
            e.Graphics.DrawString("RGR", ft, Brushes.Black, cl, ln);
            cl = cl + 40;
            e.Graphics.DrawString("Enviada", ft, Brushes.Black, cl, ln);

            ln = ln + 12;
            cl = 340;
            e.Graphics.DrawString("Até Dia", ft, Brushes.Black, cl, ln);
            cl = cl + 50;
            e.Graphics.DrawString("PlanFat", ft, Brushes.Black, cl, ln);
            cl = cl + 110;
            e.Graphics.DrawString("Compra", ft, Brushes.Black, cl, ln);
            cl = cl + 54;
            e.Graphics.DrawString("Dia", ft, Brushes.Black, cl, ln);
            cl = cl + 55;
            e.Graphics.DrawString("", ft, Brushes.Black, cl, ln);
            cl = cl + 58;
            e.Graphics.DrawString("", ft, Brushes.Black, cl, ln);
            cl = cl + 50;
            e.Graphics.DrawString("", ft, Brushes.Black, cl, ln);

            ln = ln + 12.0F;
            cl = 10.0F;
            e.Graphics.DrawString(str.PadLeft(132, pad), ft, Brushes.Black, cl, ln);
            StringFormat alinhaDireita = new StringFormat();
            alinhaDireita.Alignment = StringAlignment.Far;

            ft = new Font("Courier New", 7);
            for (int i = regs; i <= Grade.Rows.Count - 1; i++)
            {
                regs++;
                if (Grade.Rows[i].Cells[0].Selected)
                {

                    if (ln < LimiteLnImpressora && Grade.Rows[i].Cells["NomeFantasia"].Value != null)
                    {
                        ln = ln + 12F;
                        cl = 10.0F;
                        e.Graphics.DrawString(Convert.ToInt32(Grade.Rows[i].Cells["CodigoCliente"].Value).ToString("000000"), ft, Brushes.Black, cl, ln);
                        cl = cl + 45;
                        e.Graphics.DrawString(geral.Left(Grade.Rows[i].Cells["NomeFantasia"].Value.ToString(), 35), ft, Brushes.Black, cl, ln);
                        cl = cl + 245;
                        e.Graphics.DrawString(Grade.Rows[i].Cells["PeriodoApuracao"].Value.ToString(), ft, Brushes.Black, cl, ln);
                        cl = cl + 55;
                        e.Graphics.DrawString(Grade.Rows[i].Cells["EnviarPlanFatAteDia"].Value.ToString(), ft, Brushes.Black, cl, ln);
                        cl = cl + 50;
                        e.Graphics.DrawString(Grade.Rows[i].Cells["AguardarAprovacaoPlanFat"].Value.ToString(), ft, Brushes.Black, cl, ln);
                        cl = cl + 55;
                        if (Grade.Rows[i].Cells["PlanFatEnviada"].Value.ToString() == "1")
                            e.Graphics.DrawString("Ok", ft, Brushes.Black, cl, ln);
                        cl = cl + 55;
                        e.Graphics.DrawString(Grade.Rows[i].Cells["AguardarOrdemCompra"].Value.ToString(), ft, Brushes.Black, cl, ln);
                        cl = cl + 55;
                        e.Graphics.DrawString(Grade.Rows[i].Cells["ConferirDDRAteDia"].Value.ToString(), ft, Brushes.Black, cl, ln);
                        cl = cl + 55;
                        if (Grade.Rows[i].Cells["DDRConferida"].Value.ToString() == "1")
                            e.Graphics.DrawString("Ok", ft, Brushes.Black, cl, ln);
                        cl = cl + 55;
                        if (Grade.Rows[i].Cells["DDR_Conferindo"].Value.ToString() == "1")
                            e.Graphics.DrawString("Ok", ft, Brushes.Black, cl, ln);                        
                        cl = cl + 55;
                        e.Graphics.DrawString(Grade.Rows[i].Cells["EnviarRGRAteDia"].Value.ToString(), ft, Brushes.Black, cl, ln);
                        cl = cl + 30;
                        if (Grade.Rows[i].Cells["RelGerEnviado"].Value.ToString() == "1")
                            e.Graphics.DrawString("Ok", ft, Brushes.Black, cl, ln);
                        ln = ln + 8F;
                        e.Graphics.DrawString(str.PadLeft(132, pad), ft, Brushes.Black, 10, ln);
                    }
                    else
                    {
                        ln = 20F;
                        cl = 385;
                        ln = ln + 12;
                        pagina++;
                        e.Graphics.DrawString(str.PadLeft(132, pad), ft, Brushes.Black, 10, LimiteLnImpressora + 30);
                        e.Graphics.DrawString("Pagina: " + pagina, ft, Brushes.Black, 740, LimiteLnImpressora + 40);
                        break;
                    }
                }
            }
            if (regs >= (Grade.Rows.Count - 2))
            {
                ln = ln + 20;
                pagina++;
                e.Graphics.DrawString(str.PadLeft(132, pad), ft, Brushes.Black, 10, LimiteLnImpressora + 30);
                e.Graphics.DrawString("Pagina: " + pagina, ft, Brushes.Black, 740, LimiteLnImpressora + 40);
                e.HasMorePages = false;
                regs++;
            }
            else
            {
                e.HasMorePages = false; // mas tem que ser true
            }
            
        }

        private void btnOkNome_Click(object sender, EventArgs e)
        {
            TituloRelatorio = _TitulopRelatorio.NaoSelecionado;
            if (cboFiltro.Text == "Código")
                txtNomeCliente.Text = geral.RetiraLetras(txtNomeCliente.Text.ToUpper());
            if (txtNomeCliente.Text != "")
            {
                if (int2Mes.VALOR.Text == "")
                    int2Mes.VALOR.Text = "0";
                if (int2Ano.VALOR.Text == "")
                    int2Ano.VALOR.Text = "0";
                _dt = new DataTable();
                _dt = oDocAplicDados.PreencheDataTable(cboFiltro.Text, txtNomeCliente.Text, cboFiltro.Text, 
                                                       Convert.ToInt16(int2Mes.VALOR.Text), 2000 + Convert.ToInt16(int2Ano.VALOR.Text), chkMostrarPeriodos.Checked);
                foreach (DataRow dr in _dt.Rows)
                {
                    if (Convert.ToInt16(int2Ano.VALOR.Text) == 0)
                        dr["Ano"] = 0;
                    if (dr["CodigoCliente"].ToString() == "")
                        dr.Delete();
                }
                bindingSource.DataSource = _dt;
                Grade.DataSource = bindingSource;
            }
        }

        private void btnSelecionarOrdemPlanFat_Click(object sender, EventArgs e)
        {
            TituloRelatorio = _TitulopRelatorio.PLANFAT;
            PreencheGrade("EnviarPlanFatAteDia Desc, NomeFantasia");
            for (int i = 0; i <= Grade.Rows.Count - 2; i++)
            {
                if (Grade.Rows[i].Cells["EnviarPlanFatAteDia"].Value.ToString() != "" && Grade.Rows[i].Cells["EnviarPlanFatAteDia"].Value.ToString() != "0")
                    Grade.Rows[i].Selected = true;
                else
                    Grade.Rows[i].Selected = false;
            }
        }

        private void btnSelecionarOrdemDDR_Click(object sender, EventArgs e)
        {
            TituloRelatorio = _TitulopRelatorio.DDR;
            PreencheGrade("ConferirDDRAteDia Desc, NomeFantasia");
            for (int i = 0; i <= Grade.Rows.Count - 2; i++)
            {
                if (Grade.Rows[i].Cells["ConferirDDRAteDia"].Value.ToString() != "" && Grade.Rows[i].Cells["ConferirDDRAteDia"].Value.ToString() != "0")
                    Grade.Rows[i].Selected = true;
                else
                    Grade.Rows[i].Selected = false;
            }
        }

        private void btnSelecionarRGR_Click(object sender, EventArgs e)
        {
            TituloRelatorio = _TitulopRelatorio.RGR;
            PreencheGrade("EnviarRGRAteDia Desc, NomeFantasia");
            for (int i = 0; i <= Grade.Rows.Count - 2; i++)
            {
                if (Grade.Rows[i].Cells["EnviarRGRAteDia"].Value.ToString() != "" && Grade.Rows[i].Cells["EnviarRGRAteDia"].Value.ToString() != "0")
                    Grade.Rows[i].Selected = true;
                else
                    Grade.Rows[i].Selected = false;
            }
        }

        private void cboFiltro_KeyUp(object sender, KeyEventArgs e)
        {
            cboFiltro.Text = "Código";
        }

        private void ppd_Load(object sender, EventArgs e)
        {
            pagina = 0;
            regs = 0;
        }

        private void Grade_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Grade.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;
                Grade.Columns[7].SortMode = DataGridViewColumnSortMode.NotSortable;
                //botão Ok
                var butCell = (DataGridViewButtonCell)Grade.Rows[e.RowIndex].Cells["Ok"];
                butCell.UseColumnTextForButtonValue = true;
                butCell.Value = "OK";
                butCell.Style.BackColor = Color.Yellow;
            }
        }
    }
}
