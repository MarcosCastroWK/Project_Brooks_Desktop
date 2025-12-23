using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gecko.WebIDL;
using LibSILC;
using Org.BouncyCastle.Asn1.Cms;
using SILCNegocios;

namespace formSILC
{
    public partial class frmServicoExecutar : Form
    {
        public bool bSalvar = false;
        public clsProgramacaoDiariaServicos oProgDiaria = new clsProgramacaoDiariaServicos();
        public string pDataProgramacaoAberta = "";
        public bool bIncluiuServico = false;
        public string pServicoExecutar = "";

        public frmServicoExecutar()
        {
            InitializeComponent();
        }

        private void frmObservacao_Load(object sender, EventArgs e)
        {
            cboServicoExecutar.Items.Add(pServicoExecutar);
            cboServicoExecutar.Text = pServicoExecutar;
            cboServicoExecutar.Items.Add("COLETAR");
            cboServicoExecutar.Items.Add("COLOCAR CAIXA");
            cboServicoExecutar.Items.Add("TROCAR");
            cboServicoExecutar.Items.Add("TROCAR CAIXA");
            cboServicoExecutar.Items.Add("RETIRAR");
            cboServicoExecutar.Items.Add("RETIRAR CAIXA");
            cboServicoExecutar.Items.Add("COLOCAR ROLL ON");
            cboServicoExecutar.Items.Add("RETIRAR ROLL ON");
            cboServicoExecutar.Items.Add("TROCAR ROLL ON");
            cboServicoExecutar.Items.Add("COLOCAR COMPACTADORA");
            cboServicoExecutar.Items.Add("RETIRAR COMPACTADORA");
            cboServicoExecutar.Items.Add("TROCAR COMPACTADORA");
            cboServicoExecutar.Items.Add("RETIRAR, DESCARREGAR, DEVOLVER");
            btnInserirRetirar.Enabled = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = new DialogResult();
            dlgResult = DialogResult.Yes;
            clsProgramacaoDados oProgramacaoDados = new clsProgramacaoDados();
            oProgDiaria.DataProgramada = dtpDataRetirar.Text;
            if (oProgramacaoDados.ExisteProgramacao(Convert.ToInt32(Convert.ToDateTime(oProgDiaria.Data).ToString("yyyyMMdd")),
                                                    oProgDiaria.CodigoCliente, oProgDiaria.ExecutarServico, "RETIRAR") == "Incluir")
            {
                if (btnInserirRetirar.Enabled)
                {
                    dlgResult = DialogResult.No;
                    dlgResult = MessageBox.Show("Você precisa programar uma Retirada. Continuar assim mesmo?", "Retirar", MessageBoxButtons.YesNo);
                }
            }
            if (dlgResult == DialogResult.Yes)
            {
                bSalvar = true;
                this.Close();
            }
        }

        private void cboServicoExecutar_Leave(object sender, EventArgs e)
        {
            cboServicoExecutar.Text = cboServicoExecutar.Text.ToUpper();
        }

        private void InserirServicosFutura(string pDataProgramada)
        {
            // insert ServicoFutura 
            clsServicosFutura oServicosFutura = new clsServicosFutura();
            clsServicosFuturaDados oServicosFuturaDados = new clsServicosFuturaDados();
            oServicosFutura.CodigoCaminhao = oProgDiaria.CodigoCaminhao;
            oServicosFutura.CodigoCliente = oProgDiaria.CodigoCliente;
            oServicosFutura.CodigoMotorista = oProgDiaria.CodigoMotorista;
            oServicosFutura.CodigoResiduo = oProgDiaria.CodigoResiduo;
            oServicosFutura.DataProgramada = pDataProgramada;
            oServicosFutura.MapaMarcado = 0;
            if (oProgDiaria.RotaMapa == "X")
                oServicosFutura.MapaMarcado = 1;
            oServicosFutura.Observacao = oProgDiaria.Observacao;
            oServicosFutura.DestinoFinal = oProgDiaria.DestinoFinal;
            oServicosFutura.DescricaoResiduo = oProgDiaria.ExecutarServico;
            oServicosFutura.ServicoAExecutar = oProgDiaria.ServicoExecutado;
            oServicosFutura.Hora = oProgDiaria.Hora;
            if (oServicosFuturaDados.DadoExiste(oServicosFutura.CodigoCliente,
                                                oServicosFutura.CodigoResiduo,
                                                oServicosFutura.DataProgramada,
                                                oServicosFutura.DescricaoResiduo, oServicosFutura.Hora) == "Incluir")
            {
                oServicosFuturaDados.Inserir(oServicosFutura);
            }
            else
            {
                oServicosFuturaDados.Alterar(oServicosFutura, oServicosFutura.CodigoCliente, oServicosFutura.DataProgramada, oServicosFutura.Hora, false);
            }
        }

        private void btnInserirRetirar_Click(object sender, EventArgs e)
        {
            bIncluiuServico = false;
            clsProgramacaoDados oProgramacaoDados = new clsProgramacaoDados();            
            oProgDiaria.DataProgramada = dtpDataRetirar.Text;
            if (oProgramacaoDados.ExisteProgramacao(Convert.ToInt32(Convert.ToDateTime(oProgDiaria.Data).ToString("yyyyMMdd")), 
                                                    oProgDiaria.CodigoCliente, oProgDiaria.ExecutarServico, "RETIRAR") == "Incluir")
            {                
                oProgDiaria.AnoMesDia = Convert.ToInt32(Convert.ToDateTime(oProgDiaria.Data).ToString("yyyyMMdd"));
                oProgDiaria.ServicoExecutado = cboServicoExecutar.Text.Replace("COLOCAR", "RETIRAR");
                oProgDiaria.TipoProgramacao = 2;
                oProgDiaria.Quadro = 2;
                oProgDiaria.Hora = DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00");
                oProgramacaoDados.Inserir(oProgDiaria, false, true);

                if (Convert.ToDateTime(dtpDataRetirar.Text) >= Convert.ToDateTime(oProgDiaria.Data))
                {
                    clsReprogramacaoServicos oReprogramacao = new clsReprogramacaoServicos();
                    oReprogramacao.AnoMesDia = oProgDiaria.AnoMesDia;
                    oReprogramacao.DescricaoResiduo = oProgDiaria.ExecutarServico;
                    oReprogramacao.CodigoCaminhao = oProgDiaria.CodigoCaminhao;
                    oReprogramacao.CodigoCliente = oProgDiaria.CodigoCliente;
                    oReprogramacao.CodigoMotorista = oProgDiaria.CodigoMotorista;
                    oReprogramacao.Data = oProgDiaria.Data;
                    oReprogramacao.ExecutarServico = oProgDiaria.ExecutarServico;
                    oReprogramacao.DataProgramada = oProgDiaria.DataProgramada;
                    oReprogramacao.Hora = oProgDiaria.Hora;
                    oReprogramacao.HoraProgramada = cboServicoExecutar.Text.Replace("COLOCAR", "RETIRAR");
                    oReprogramacao.Observacao = oProgDiaria.Observacao;
                    oReprogramacao.Quantidade = oProgDiaria.Quantidade;
                    oReprogramacao.SequencialProgramacaoDiaria = oProgDiaria.Sequencial;
                    oReprogramacao.Solicitante = oProgDiaria.Solicitante;
                    oReprogramacao.StatusCor = geral.RetornaCodigoCor("Cinza");
                    oReprogramacao.TipoProgramacao = oProgDiaria.TipoProgramacao;
                    oReprogramacao.Unidade = oProgDiaria.Unidade;
                    clsReprogramacaoDados oReprogramacaoDados = new clsReprogramacaoDados();
                    oReprogramacaoDados.Inserir(oReprogramacao);
                    InserirServicosFutura(oProgDiaria.DataProgramada);
                    if (Convert.ToDateTime(oProgDiaria.DataProgramada) > Convert.ToDateTime(oProgDiaria.Data))
                    {                        
                        InserirServicosFutura(oProgDiaria.Data);
                    }
                    bIncluiuServico = true;
                }
                btnOk_Click(sender, EventArgs.Empty);
            }
            else
                MessageBox.Show("Já existe um programação dessa colocação!");
        }

        private void cboServicoExecutar_SelectedIndexChanged(object sender, EventArgs e)
        {
            OperacaoDeixarInserirRetirada();
        }

        private void OperacaoDeixarInserirRetirada()
        {
            if (cboServicoExecutar.Text.ToUpper().IndexOf("COLOCAR") > -1 && dtpDataRetirar.Value >= Convert.ToDateTime(pDataProgramacaoAberta))
            {
                btnInserirRetirar.Enabled = true;
            }
            else
            {
                btnInserirRetirar.Enabled = false;
            }
        }

        private void dtpDataRetirar_ValueChanged(object sender, EventArgs e)
        {
            OperacaoDeixarInserirRetirada();
        }
    }
}
