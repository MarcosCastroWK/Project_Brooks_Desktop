using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using LibSILC;
using SILCNegocios;
using Newtonsoft.Json;
using System.IO;

namespace formSILC
{
    public partial class frmMTReLancar : Form
    {
        public string pLoginClienteIMA;
        public string pSenhaClienteIMA;
        public string pCNPJArmazenador;
        public string pCNPJDestinador;
        public string pCNPJ_CPFGerador;
        public string pCNPJTransportador;
        public string pDataProgramada;
        public int pCodigoMotorista;
        public int pCodigoCaminhao;
        public int pCodigoResiduo;
        public int pCodigoCliente;

        public string pCodigoModeloMTRe;

        public DataGridView pGrade;
        public int pRowIndex = 0;
        public bool pChamouPelaProgramacao = true;
        public int pNumeroLancamento = 0;
        public int pCodigoDestinoFinal = 0;

        clsModeloMTRe oModelo = new clsModeloMTRe();
        clsModeloMTReDados oModeloDados = new clsModeloMTReDados();
        clsModeloMTReResiduos oModeloResiduos = new clsModeloMTReResiduos();
        clsModeloMTReResiduosDados oModeloResiduosDados = new clsModeloMTReResiduosDados();
        clsIBAMA oIbama = new clsIBAMA();
        clsIBAMADados oIbamaDados = new clsIBAMADados();
        clsResiduos oResiduo = new clsResiduos();
        clsResiduoDados oResiduoDados = new clsResiduoDados();
        clsFuncionarios oMotorista = new clsFuncionarios();
        clsFuncionarioDados oMotoristaDados = new clsFuncionarioDados();
        clsCaminhoes oCaminhao = new clsCaminhoes();
        clsCaminhoesDados oCaminhaoDados = new clsCaminhoesDados();
        clsClientes oCliente = new clsClientes();
        clsClienteDados oClienteDados = new clsClienteDados();
        clsEnderecos oEndereco = new clsEnderecos();
        clsEnderecosDados oEnderecoDados = new clsEnderecosDados();

        private BindingSource bindingSource = new BindingSource();
        public frmMTReLancar()
        {
            InitializeComponent();
        }
        private void frmMTReLancar_Load(object sender, EventArgs e)
        {
            if (pCodigoCliente > 0)
            {
                txtCNPJCPFDestinador.Text = pCNPJDestinador;

                oCliente = new clsClientes();
                oClienteDados = new clsClienteDados();
                oClienteDados.PegaDados(oCliente, pCodigoCliente);
                txtNomeRazaoSocialGerador.Text = oCliente.Nome;
                txtCodigoUnidadeGerador.Text = oCliente.CodigoUnidadeDoIMA.ToString();

                txtCNPJCPFGerador.Text = pCNPJ_CPFGerador;

                oEndereco = new clsEnderecos();
                oEnderecoDados = new clsEnderecosDados();
                oEnderecoDados.PegaDados(oEndereco, pCodigoCliente, 2, 0);
                txtRespEmissao.Text = oEndereco.Contato;

                if (oEndereco.CargoContato == "")
                    txtCargo.Text = "SUPERVISOR(A)";
                else
                    txtCargo.Text = oEndereco.CargoContato;

                DataTable _dtResiduos = new DataTable();
                _dtResiduos = oModeloResiduosDados.PreencheDataTable("mr.Codigo", pCodigoModeloMTRe);

                bindingSource.DataSource = _dtResiduos;
                grvResiduos.DataSource = bindingSource;

                if (pCNPJTransportador == "" || pCNPJTransportador == null)
                    txtCNPJCPF_Transportador.Text = _dtResiduos.Rows[0]["CNPJTransportador"].ToString();
                else
                    txtCNPJCPF_Transportador.Text = pCNPJTransportador;

                clsDestinoFinal oTransportador = new clsDestinoFinal();
                clsDestinoFinalDados oTransportadorDados = new clsDestinoFinalDados();
                if (txtCNPJCPF_Transportador.Text != "")
                {
                    DataTable _dtTransp = new DataTable();
                    _dtTransp = oTransportadorDados.PegaQQDados("where CNPJ = '" + txtCNPJCPF_Transportador.Text + "'");
                    
                    txtNomeRazaoSocialTransportador.Text = _dtTransp.Rows[0]["Nome"].ToString();
                    cboCodigoUnidadeTransportador.Items.Clear();
                    foreach (DataRow _drUT in _dtTransp.Rows)
                    {
                        cboCodigoUnidadeTransportador.Items.Add(_drUT["CodigoUnidadeDoIMA"].ToString());
                    }
                    //["ExecutarServico"].ToString().Split("%"[0])[0] + "%'");
                    cboCodigoUnidadeTransportador.Text = _dtTransp.Rows[0]["CodigoUnidadeDoIMA"].ToString().Split("-"[0])[0];
                }
                if (pCodigoMotorista > 0)
                {
                    oMotorista = oMotoristaDados.PegaDados(oMotorista, pCodigoMotorista);
                    txtNomeMotorista.Text = oMotorista.Nome;
                }
                if (pCodigoCaminhao > 0)
                {
                    oCaminhao = oCaminhaoDados.PegaDados(oCaminhao, pCodigoCaminhao);
                    txtPlacaVeiculo.Text = oCaminhao.Placas;
                }
                if (pCNPJDestinador != "")
                {
                    DataTable _dtDestinador = new DataTable();
                    clsDestinoFinalDados oDestinadorDados = new clsDestinoFinalDados();

                    if (pCodigoDestinoFinal == 0)
                    {
                        if (pCodigoResiduo == 32 || pCodigoResiduo == 141)
                            _dtDestinador = oDestinadorDados.PegaQQDados("where CNPJ = '" + pCNPJDestinador + "'" + " group by CodigoUnidadeDoIMA desc"); // orderm inversa, pra mostrar destina 1º
                        else
                            _dtDestinador = oDestinadorDados.PegaQQDados("where CNPJ = '" + pCNPJDestinador + "'" + " group by CodigoUnidadeDoIMA ");
                    }
                    else
                        _dtDestinador = oDestinadorDados.PegaQQDados("where Codigo = " + pCodigoDestinoFinal + " \n ");

                    txtNomeRazaoSocialDestinador.Text = _dtDestinador.Rows[0]["Nome"].ToString();
                    cboCodigoUnidadeDestinador.Items.Clear();
                    foreach (DataRow _dtUD in _dtDestinador.Rows)
                    {
                        cboCodigoUnidadeDestinador.Items.Add(_dtUD["CodigoUnidadeDoIMA"].ToString() + "-" + _dtUD["NomeFantasia"].ToString() + "-Nosso Código: " + _dtUD["CodigoDestinoFinal"].ToString());
                    }
                    if (_dtDestinador.Rows.Count > 0)
                        cboCodigoUnidadeDestinador.Text = _dtDestinador.Rows[0]["CodigoUnidadeDoIMA"].ToString() + "-" + _dtDestinador.Rows[0]["NomeFantasia"].ToString() + "-Nosso Código: " + _dtDestinador.Rows[0]["CodigoDestinoFinal"].ToString();
                }
            }
        }

        private void GerarMTRe()
        {
            if (pCodigoCliente > 0)
            {
                //var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://miramichi.procergs.com.br/mtrservice/salvarManifestoLote");
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://mtr.ima.sc.gov.br/mtrservice/salvarManifestoLote");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                clsParametros oPar = new clsParametros();
                clsParametrosDados oParDados = new clsParametrosDados();
                oPar = oParDados.PegaDados(oPar, 1);

                clsMTReLogin oLogin = new clsMTReLogin();
                oLogin.login = geral.RetiraLetras(pLoginClienteIMA);
                oLogin.senha = pSenhaClienteIMA;

                string json = "";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    clsJsonCriar _oJsonCriar = new clsJsonCriar();
                    ManifestoJSONDto _manifesto = new ManifestoJSONDto();
                    if (pCNPJArmazenador != "" && pCNPJArmazenador != null)
                        _manifesto.cnpArmazenador = geral.RetiraCharsCNPJCPF(pCNPJArmazenador);
                    else
                        _manifesto.cnpArmazenador = null;
                    _manifesto.cnpDestinador = geral.RetiraCharsCNPJCPF(pCNPJDestinador);
                    _manifesto.cnpGerador = geral.RetiraCharsCNPJCPF(pCNPJ_CPFGerador);
                    _manifesto.cnpTransportador = geral.RetiraCharsCNPJCPF(pCNPJTransportador);

                    //_manifesto.cnpArmazenador = pCNPJArmazenador;
                    //_manifesto.cnpDestinador = pCNPJDestinador;
                    //_manifesto.cnpGerador = pCNPJ_CPFGerador;
                    //_manifesto.cnpTransportador = pCNPJTransportador;

                    if (_manifesto.cnpArmazenador != "")
                    {
                        if (_manifesto.cnpArmazenador == "50.668.722/0019-16" || _manifesto.cnpArmazenador == "50668722001916")
                            _manifesto.codUnidadeArmazenador = 36820;
                        else
                        {
                            clsDestinoFinal oArmazenador = new clsDestinoFinal();
                            clsDestinoFinalDados oArmazenadorDados = new clsDestinoFinalDados();
                            string _CodigoArmazenador = "";
                            if (pCNPJArmazenador != "")
                                _CodigoArmazenador = oArmazenadorDados.PegaQQDados("where CNPJ = '" + pCNPJArmazenador + "'").Rows[0]["Codigo"].ToString();
                            if (_CodigoArmazenador != "")
                            {
                                oArmazenadorDados.PegaDados(oArmazenador, Convert.ToInt32(_CodigoArmazenador));
                                if (oArmazenador.CodigoUnidadeDoIMA != 0)
                                    _manifesto.codUnidadeArmazenador = oArmazenador.CodigoUnidadeDoIMA;
                            }
                        }
                    }
                    else
                        _manifesto.codUnidadeArmazenador = 0;
                    _manifesto.codUnidadeDestinador = cboCodigoUnidadeDestinador.Text.Split("-"[0])[0];   // isso é do IMA
                    _manifesto.codUnidadeGerador = Convert.ToInt32(txtCodigoUnidadeGerador.Text);         // isso é do IMA
                    _manifesto.codUnidadeTransportador = Convert.ToInt32(cboCodigoUnidadeTransportador.Text.Split("-"[0])[0]);   // isso é do IMA
                    _manifesto.manifData = DateTime.Now.ToString();
                    //_manifesto.manifDataExpedicao = dtpDataEmissao.Text + " 00:00:00";
                    _manifesto.manifTransportadorDataExpedicao = dtpDataTransporte.Value.ToString("yyyyMMdd") ; // + " 00:00:00";
                    //_manifesto.manifestoCodigo = 9999999;
                    _manifesto.manifGeradorCargoResponsavel = txtCargo.Text;
                    _manifesto.manifGeradorNomeResponsavel = txtRespEmissao.Text;
                    _manifesto.manifObservacao = txtObservacoes.Text;
                    _manifesto.manifTransportadorNomeMotorista = txtNomeMotorista.Text;
                    _manifesto.manifTransportadorPlacaVeiculo = txtPlacaVeiculo.Text;
                    _manifesto.situacaoManifestoCodigo = 0;

                    foreach (DataGridViewRow _dgvr in grvResiduos.Rows)
                    {
                        if (_dgvr.Cells["CodigoEstadoFisico"].Value != null)
                        {
                            ItemManifestoJSONDto oitem = new ItemManifestoJSONDto();
                            oitem.codigoTipoEstado = 1;
                            if (_dgvr.Cells["CodigoEstadoFisico"].Value.ToString() != "")
                                oitem.codigoTipoEstado = Convert.ToInt32(_dgvr.Cells["CodigoEstadoFisico"].Value.ToString());

                            if (_dgvr.Cells["CodigoClasse"].Value.ToString() != "")
                                oitem.codigoClasse = Convert.ToInt32(_dgvr.Cells["CodigoClasse"].Value.ToString());

                            if (_dgvr.Cells["CodigoAcondicionamento"].Value.ToString() != "")
                                oitem.codigoAcondicionamento = Convert.ToInt32(_dgvr.Cells["CodigoAcondicionamento"].Value.ToString());

                            oitem.codigoInterno = "999";
                            oitem.codigoSequencial = 1;

                            if (_dgvr.Cells["codigoTecnologia"].Value.ToString() != "")
                                oitem.codigoTecnologia = Convert.ToInt32(_dgvr.Cells["codigoTecnologia"].Value.ToString());

                            oitem.codigoUnidade = 4;
                            if (_dgvr.Cells["codigoUnidade"].Value.ToString() != "")
                                oitem.codigoUnidade = Convert.ToInt32(_dgvr.Cells["codigoUnidade"].Value.ToString());
                            //oitem.codigoUnidade = 1; // essa linha é só pra teste - 30/11/23

                            oitem.justificativa = null;
                            oitem.manifestoItemCodInterno = "";
                            oitem.manifestoItemCodInternoDestinador = "";

                            //oitem.manifestoItemObservacao = txtObservacoes.Text;

                            oitem.quantidade = 0;
                            if (_dgvr.Cells["Quantidade"].Value.ToString() != "")
                                oitem.quantidade = Convert.ToDouble(_dgvr.Cells["Quantidade"].Value.ToString());

                            clsResiduos oResiduo = new clsResiduos();
                            clsResiduoDados oResiduoDados = new clsResiduoDados();
                            oResiduoDados.PegaDados(oResiduo, 0);

                            //clsIBAMA oIbama = new clsIBAMA();
                            //clsIBAMADados oIbamaDados = new clsIBAMADados();
                            //oIbamaDados.PegaDados(oIbama, oResiduo.CodigoIBAMA);
                            if (_dgvr.Cells["DescricaoIBAMA"].Value.ToString().IndexOf("(*)") > -1)
                                oitem.residuo = _dgvr.Cells["CodigoIBAMA"].Value.ToString() + "(*)";
                            else
                                oitem.residuo = _dgvr.Cells["CodigoIBAMA"].Value.ToString();
                            oitem.tipoDensidadeUnidade = "1";
                            oitem.tipoDensidadeValor = "1";
                            oitem.NumeroONU = _dgvr.Cells["NumeroONU"].Value.ToString();
                            oitem.ClasseDeRisco = _dgvr.Cells["ClasseRisco"].Value.ToString();
                            oitem.NomeEmbarque = _dgvr.Cells["NomeEmbarque"].Value.ToString();
                            oitem.GrupoEmbalagem = _dgvr.Cells["GrupoEmbalagem"].Value.ToString();
                            _manifesto.itemManifestoJSONs.Add(oitem);
                        }
                    }
                    json = _oJsonCriar.CriarJsonGeraLote(_manifesto, oLogin.login, oLogin.senha, "0");

                    if (geral.UsuarioAtual.ToLower() == "teixeira")
                    {
                        teste oTeste = new teste();
                        oTeste.textBox1.Text = json.Replace(",", ",\n");
                        oTeste.ShowDialog();

                        DialogResult _DResult = new DialogResult();
                        _DResult = MessageBox.Show("Confirma envio [Cancel:Fecha tudo]?", "SILC", MessageBoxButtons.YesNoCancel);
                        if (_DResult == DialogResult.Yes)
                        {
                            streamWriter.Write(json);
                        }
                        else if (_DResult == DialogResult.No)
                        {
                            return;
                        }
                        else if (_DResult == DialogResult.Cancel)
                        {
                            Application.Exit();
                            return;
                        }
                    }
                    else
                    {
                        streamWriter.Write(json);
                    }

                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                string xjson;
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    xjson = result.ToString();
                }
                xjson = xjson.Replace("null", "-1");
                var post = JsonConvert.DeserializeObject<LoteManifestoJSON>(xjson);

                ManifestoJSONDto omanif = new ManifestoJSONDto();
                omanif = post.manifestoJSONDtos[0];
                if (omanif.manifestoCodigo <= 0)
                {
                    MessageBox.Show("retorno: " + omanif.retorno +
                                    " \n" + "manifesto codigo: " + omanif.manifestoCodigo +
                                    " \n" + "retorno  codigo: " + omanif.retornoCodigo +
                                    " \n" + "situacao manifesto : " + omanif.situacaoManifestoCodigo);
                }
                string _Url = "";
                //omanif.manifestoCodigo = 9876543210; // teste de gravação // Edson

                if (omanif.manifestoCodigo >= 0)
                {
                    if (pChamouPelaProgramacao)
                    {
                        // salvar manifesto no ServicosFutura
                        clsServicosFutura oServicosFutura = new clsServicosFutura();
                        clsServicosFuturaDados oServicosFuturaDados = new clsServicosFuturaDados();
                        if (pCodigoCliente != 0)
                        {
                            if (pCodigoCaminhao != 0)
                                oServicosFutura.CodigoCaminhao = pCodigoCaminhao;
                            oServicosFutura.CodigoCliente = pCodigoCliente;
                            if (pCodigoMotorista != 0)
                                oServicosFutura.CodigoMotorista = pCodigoMotorista;

                            oServicosFutura.CodigoResiduo = pCodigoResiduo;
                            clsResiduos _oRes = new clsResiduos();
                            clsResiduoDados _oResDados = new clsResiduoDados();
                            if (oServicosFutura.CodigoResiduo == 0)
                            {
                                oServicosFutura.CodigoResiduo = _oResDados.PegaCodigoResiduo(oServicosFutura.DescricaoResiduo);
                            }
                            oServicosFutura.Observacao = pGrade.Rows[pRowIndex].Cells["Observacao"].Value.ToString();
                            oServicosFutura.DestinoFinal = pGrade.Rows[pRowIndex].Cells["DestinoFinal"].Value.ToString();
                            oServicosFutura.DataProgramada = pGrade.Rows[pRowIndex].Cells["DataProgramada"].Value.ToString();
                            oServicosFutura.ServicoAExecutar = pGrade.Rows[pRowIndex].Cells["HoraProgramada"].Value.ToString();
                            oServicosFutura.Hora = pGrade.Rows[pRowIndex].Cells["Hora"].Value.ToString();
                            oServicosFutura.Solicitante = pGrade.Rows[pRowIndex].Cells["Solicitante"].Value.ToString();
                            oServicosFutura.NumeroMTRe = omanif.manifestoCodigo.ToString();
                            oServicosFutura.DescricaoResiduo = pGrade.Rows[pRowIndex].Cells["ExecutarServico"].Value.ToString();
                            if (oServicosFuturaDados.DadoExiste(pCodigoCliente,
                                                                oServicosFutura.CodigoResiduo,
                                                                oServicosFutura.DataProgramada,
                                                                oServicosFutura.DescricaoResiduo, oServicosFutura.Hora) == "Incluir")
                            {
                                oServicosFuturaDados.Inserir(oServicosFutura);
                            }
                            else
                            {
                                bool bFiltrarCampoHora = false;
                                if (pGrade.Rows[pRowIndex].Cells["StatusCor"].Value.ToString() == "15000000")
                                    bFiltrarCampoHora = true;
                                oServicosFuturaDados.Alterar(oServicosFutura, oServicosFutura.CodigoCliente, oServicosFutura.DataProgramada, oServicosFutura.Hora, bFiltrarCampoHora);
                            }
                            if (!System.IO.File.Exists("\\\\servidor\\WinSILC\\temp\\" + omanif.manifestoCodigo.ToString() + ".pdf"))
                            {
                                _Url = "http://mtr.ima.sc.gov.br/ControllerServlet?acao=relatorio&nomeRelatorio=manifesto&manifesto=" + omanif.manifestoCodigo.ToString() + "&condicao=N";
                                using (var client = new WebClient())
                                {
                                    client.DownloadFile(_Url, "\\\\servidor\\WinSILC\\temp\\" + omanif.manifestoCodigo.ToString() + ".pdf");
                                }
                            }
                            if (System.IO.File.Exists("\\\\servidor\\WinSILC\\temp\\" + omanif.manifestoCodigo.ToString() + ".pdf"))
                            {
                                try
                                {
                                    System.Diagnostics.Process.Start("\\\\servidor\\WinSILC\\temp\\" + omanif.manifestoCodigo.ToString() + ".pdf");
                                }
                                finally
                                {
                                    this.Close();
                                }
                            }
                        }
                    }
                    else if (!pChamouPelaProgramacao) // chamou pela Locação
                    {
                        if (!System.IO.File.Exists("\\\\servidor\\WinSILC\\temp\\" + omanif.manifestoCodigo.ToString() + ".pdf"))
                        {
                            _Url = "http://mtr.ima.sc.gov.br/ControllerServlet?acao=relatorio&nomeRelatorio=manifesto&manifesto=" + omanif.manifestoCodigo.ToString() + "&condicao=N";
                            using (var client = new WebClient())
                            {
                                client.DownloadFile(_Url, "\\\\servidor\\WinSILC\\temp\\" + omanif.manifestoCodigo.ToString() + ".pdf");
                            }
                        }
                        if (System.IO.File.Exists("\\\\servidor\\WinSILC\\temp\\" + omanif.manifestoCodigo.ToString() + ".pdf"))
                        {
                            try
                            {
                                clsLancamentoMTR oLancamentoMTR = new clsLancamentoMTR();
                                oLancamentoMTR.NumeroLancamento = pNumeroLancamento;
                                oLancamentoMTR.CodigoResiduo = pCodigoResiduo;
                                oLancamentoMTR.NumeroMTRFatima = omanif.manifestoCodigo;

                                clsLancamentoMTRDados oLancamentoMTRDados = new clsLancamentoMTRDados();
                                oLancamentoMTRDados.SalvarNumeroMTRe(pNumeroLancamento, pCodigoResiduo, omanif.manifestoCodigo.ToString());

                                System.Diagnostics.Process.Start("\\\\servidor\\WinSILC\\temp\\" + omanif.manifestoCodigo.ToString() + ".pdf");
                            }
                            finally
                            {
                                this.Close();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Número manifesto inválido!");
                }
            }
        }
        private void btnGerar_Click(object sender, EventArgs e)
        {
            for (int _ln = 0; _ln <= grvResiduos.Rows.Count - 2; _ln++)
            {
                if (grvResiduos.Rows[_ln].Cells["Quantidade"].Value == null)
                {
                    MessageBox.Show("Quantidade inválida! Linha: " + (_ln + 1).ToString());
                    return;
                }
            }
            if (txtCodigoUnidadeGerador.Text == "" || txtCodigoUnidadeGerador.Text == "0")
                MessageBox.Show("Codigo Unidade do Gerador inválido!");
            else
                GerarMTRe();
        }
        private void grvResiduos_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (grvResiduos.CurrentCell is DataGridViewCheckBoxCell)
            {
                grvResiduos.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }

        }
        private void grvResiduos_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show("Teclou -> " + e.KeyCode.ToString());
        }
    }
}
