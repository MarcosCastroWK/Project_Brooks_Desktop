using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Data.OleDb;
using MySql.Data.MySqlClient;
using MySql.Data;
using SILCNegocios;

namespace LibSILC
{
	public class clsProgramacaoDados
	{
	    private clsDB oDB = new clsDB();
        private DataSet l_ds = new DataSet();
        DataTable l_dt = new DataTable(); 
        private string s;

        // funções locais para multi banco de dados
        private void ConectaBanco()
        {
            oDB.ConectaMySql();
        }        
        private void FillDataSet()
        {
            l_ds = new DataSet();
            MySqlDataAdapter l_myData = new MySqlDataAdapter();
            l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
            l_myData.Fill(l_ds);
            l_dt = l_ds.Tables[0];
        }
        private void DesconectaBanco()
        {
            oDB.DesconectaMySql();
        }
        public DataTable PegaDados(clsProgramacaoDiariaServicos pProgramacao, int pSequencial, int pAnoMesDia, int pQuadro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select p.AnoMesDia, p.Quadro, p.Linha, p.StatusCor, p.Data, p.Hora, p.Solicitante, p.CodigoCliente, \n";
                s = s + "       c.Nome as NomeCliente, p.CodigoResiduo, p.ExecutarServico, p.DataProgramada, p.HoraProgramada, p.Quantidade, \n";
                s = s + "       p.CodigoCaminhao, o.Modelo as ModeloCaminhao, p.CodigoMotorista, m.Nome as NomeMotorista, \n";
                s = s + "       p.Observacao, p.Unidade, p.SequencialParaQuadro1, p.TipoProgramacao, p.Quantidade2, \n";
                s = s + "       p.Unidade2, p.RotaMapa, p.CorObservacao, p.Origem, p.DestinoFinal \n";
                s = s + "from   ProgramacaoDiariaServicos p \n";
                s = s + "inner  join Clientes c on c.Codigo = p.CodigoCliente \n";
                s = s + "inner  join Caminhoes o on o.Codigo = p.CodigoCaminhao \n";
                s = s + "inner  join Funcionarios m on m.Codigo = p.CodigoMotorista \n";
                if (pSequencial > 0)
                {
                    s = s + "where  Sequencial = " + pSequencial + " ";
                }
                else
                {
                    s = s + "where p.AnoMesDia = " + pAnoMesDia + " ";
                    s = s + "and   p.Quadro    = " + pQuadro + " ";
                }
                s = s + " order by p.AnoMesDia ";
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows[0]["AnoMesDia"].ToString() != "")
                        pProgramacao.AnoMesDia = Convert.ToInt32(l_dt.Rows[0]["AnoMesDia"]);
                    if (l_dt.Rows[0]["Quadro"].ToString() != "")
                        pProgramacao.Quadro = Convert.ToInt32(l_dt.Rows[0]["Quadro"]);
                    pProgramacao.StatusCor = l_dt.Rows[0]["StatusCor"].ToString();
                    pProgramacao.Data = l_dt.Rows[0]["Data"].ToString();
                    pProgramacao.Hora = l_dt.Rows[0]["Hora"].ToString();
                    pProgramacao.Solicitante = l_dt.Rows[0]["Solicitante"].ToString();
                    if (l_dt.Rows[0]["CodigoCliente"].ToString() != "")
                        pProgramacao.CodigoCliente = Convert.ToInt32(l_dt.Rows[0]["CodigoCliente"]);
                    pProgramacao.NomeCliente = l_dt.Rows[0]["NomeCliente"].ToString();
                    pProgramacao.ExecutarServico = l_dt.Rows[0]["ExecutarServico"].ToString();
                    if (l_dt.Rows[0]["CodigoResiduo"].ToString() != "")
                        pProgramacao.CodigoResiduo = Convert.ToInt32(l_dt.Rows[0]["CodigoResiduo"]);
                    else if (l_dt.Rows[0]["CodigoResiduo"].ToString() == "")
                    {
                        string _descricao;
                        int _codigo;
                        clsResiduoDados oResiduo = new clsResiduoDados();
                        _descricao = oResiduo.PegaDadosLista(pProgramacao.ExecutarServico.Substring(0, 3), 0).Rows[0]["DescricaoReduzida"].ToString();
                        if (_descricao != "")
                        {
                            pProgramacao.ExecutarServico = _descricao;
                            _codigo = Convert.ToInt32(oResiduo.PegaDadosLista(pProgramacao.ExecutarServico.Substring(0, 3), 0).Rows[0]["Codigo"]);
                            pProgramacao.CodigoResiduo = _codigo;
                        }
                    }
                    pProgramacao.ServicoExecutado = l_dt.Rows[0]["ServicoExecutado"].ToString();
                    if (l_dt.Rows[0]["Quantidade"].ToString() != "")
                        pProgramacao.Quantidade = Convert.ToInt32(l_dt.Rows[0]["Quantidade"]);
                    if (l_dt.Rows[0]["CodigoCaminhao"].ToString() != "")
                        pProgramacao.CodigoCaminhao = Convert.ToInt32(l_dt.Rows[0]["CodigoCaminhao"]);
                    pProgramacao.ModeloCaminhao = l_dt.Rows[0]["ModeloCaminhao"].ToString();
                    if (l_dt.Rows[0]["CodigoMotorista"].ToString() != "")
                        pProgramacao.CodigoMotorista = Convert.ToInt32(l_dt.Rows[0]["CodigoMotorista"]);
                    pProgramacao.NomeMotorista = l_dt.Rows[0]["NomeMotorista"].ToString();
                    pProgramacao.Observacao = l_dt.Rows[0]["Observacao"].ToString();
                    pProgramacao.Unidade = l_dt.Rows[0]["Unidade"].ToString();
                    if (l_dt.Rows[0]["SequencialParaQuadro1"].ToString() != "")
                        pProgramacao.SequencialParaQuadro1 = Convert.ToInt32(l_dt.Rows[0]["SequencialParaQuadro1"]);
                    if (l_dt.Rows[0]["TipoProgramacao"].ToString() != "")
                        pProgramacao.TipoProgramacao = Convert.ToInt32(l_dt.Rows[0]["TipoProgramacao"]);
                    if (l_dt.Rows[0]["Quantidade2"].ToString() != "")
                        pProgramacao.Quantidade2 = Convert.ToDecimal(l_dt.Rows[0]["Quantidade2"]);
                    pProgramacao.Unidade2 = l_dt.Rows[0]["Unidade2"].ToString();
                    pProgramacao.RotaMapa = l_dt.Rows[0]["RotaMapa"].ToString();
                    pProgramacao.CorObservacao = l_dt.Rows[0]["CorObservacao"].ToString();
                    pProgramacao.Origem = l_dt.Rows[0]["Origem"].ToString();
                    pProgramacao.DestinoFinal = l_dt.Rows[0]["DestinoFinal"].ToString();
                }

                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public DataTable PegaDados(int pAnoMesDia, int pQuadro, bool pNaoMostrarFechados, string pOrdem = "")
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select 0 as Rp, \n ";
                s = s + "       p.Sequencial, p.CodigoResiduo, p.AnoMesDia, p.Quadro, p.Linha, p.StatusCor, p.Data, p.Hora, p.Solicitante, p.CodigoCliente, \n";
                s = s + "       c.NomeFantasia as NomeFantasiaCliente, p.ExecutarServico, p.DataProgramada, p.HoraProgramada, p.Quantidade, \n";
                s = s + "       p.CodigoCaminhao, o.Modelo as ModeloCaminhao, p.CodigoMotorista, p.NomeMotoristaOuDescricao, m.Nome as NomeMotorista, \n";
                s = s + "       p.RotaMapa as Map, '' as Particularidade, p.DestinoFinal, p.Observacao, p.Unidade, p.SequencialParaQuadro1, Convert(p.TipoProgramacao, char) as TipoProgramacao, p.Quantidade2, \n";
                s = s + "       p.Unidade2, p.CorObservacao, p.Origem, \n";
                s = s + "       Concat(mu.Nome, ', ', e.Bairro, ', ', e.endereco, ', ', e.numero, ' ', e.complemento, ' |', c.PontoReferencia) as CidadeBairroEndereco, p.RotaMapa, NovaLinha \n";
                s = s + "from   ProgramacaoDiariaServicos p \n";
                s = s + "left   join Clientes     c on c.Codigo = p.CodigoCliente \n";
                s = s + "left   join Enderecos    e on e.Codigo = c.Codigo and e.TipoEndereco = 2 and e.TipoCadastro = 0 \n";
                s = s + "left   join Municipios   mu on e.CodigoMunicipio = mu.Codigo \n";
                s = s + "left   join Caminhoes    o on o.Codigo = p.CodigoCaminhao \n";
                s = s + "left   join Funcionarios m on m.Codigo = p.CodigoMotorista \n";
                if (pAnoMesDia > 0)
                {
                    //s = s + "where (p.AnoMesDia = " + pAnoMesDia.ToString() + " or ";
                    //s = s + "       p.DataProgramada = '" + geral.Left(pAnoMesDia.ToString(), 4) + "-" + pAnoMesDia.ToString().Substring(4, 2) + "-" + pAnoMesDia.ToString().Substring(6, 2) + "') \n";
                    if (pQuadro == 1)
                    {
                        s = s + "where p.DataProgramada = '" + geral.Left(pAnoMesDia.ToString(), 4) + "-" + pAnoMesDia.ToString().Substring(4, 2) + "-" + pAnoMesDia.ToString().Substring(6, 2) + "' \n";
                        s = s + "and   p.StatusCor = '16761024' and quadro = 2 \n";
                    }
                    else if (pQuadro == 2)
                    {
                        s = s + "where p.AnoMesDia = " + pAnoMesDia.ToString() + " \n ";
                        s = s + "and   p.Quadro = " + pQuadro.ToString() + " \n";
                    }
                    if (pNaoMostrarFechados)
                        s = s + "and  p.StatusCor <> '16761024' \n";
                    if (pOrdem.Length > 0)
                        s = s + " order by " + pOrdem + " \n";
                    else
                        s = s + " order by Convert(StatusCor, unsigned), Sequencial \n";
                    FillDataSet();

                    if (l_ds.Tables[0].Rows.Count > 0)
                    {
                        return l_ds.Tables[0];
                    }
                }
                else
                    FillDataSet();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                return l_ds.Tables[0];
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public DataTable PegaCodigoResiduo()
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select CodigoResiduo, ExecutarServico, Sequencial \n";
                s = s + "from   ProgramacaoDiariaServicos \n";
                s = s + "where  (CodigoResiduo = 0 or CodigoResiduo is null) \n";
                FillDataSet();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                return l_ds.Tables[0];
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public string DadoExiste(int pSequencial, int pQuadro, int pAnoMesDia, int pCodigoCliente)
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Sequencial \n";
                s = s + "from   ProgramacaoDiariaServicos \n";
                if (pSequencial != 0)
                {
                    s = s + "where  Sequencial    = " + pSequencial + " \n";
                    s = s + "and    Quadro        = " + pQuadro + " \n";
                    s = s + "and    AnoMesDia     = " + pAnoMesDia + " \n";
                    s = s + "and    CodigoCliente = " + pCodigoCliente + " \n";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pSequencial != 0)
                    return "Alterar";
                else
                    return "Incluir";
            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
        public string ExisteProgramacao(int pAnoMesDia, string pHora, int pCodigoCliente, string pDataProgramada)
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Sequencial ";
                s = s + "from   ProgramacaoDiariaServicos ";
                s = s + "where  AnoMesDia      =  " + pAnoMesDia.ToString() + " \n";
                s = s + "and    Hora           = '" + pHora + "' \n";
                s = s + "and    CodigoCliente  =  " + pCodigoCliente + " \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    return "Alterar";
                else
                    return "Incluir";
            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
        public bool ExisteProgramacaoFechada(string pHora, string pCodigoCliente)
        {
            bool bRet = false;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Sequencial \n";
                s = s + "from   ProgramacaoDiariaServicos \n";
                s = s + "where  Hora           = '" + pHora + "' \n";
                s = s + "and    CodigoCliente  =  " + pCodigoCliente + " \n";
                s = s + "and    StatusCor      = '16761024' \n";
                s = s + "limit  1 \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    bRet = true;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return bRet;
        }
        public string ExisteProgramacao(int pAnoMesDia, int pCodigoCliente, string pTipoResiduo, string pExecutarServico)
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Sequencial ";
                s = s + "from   ProgramacaoDiariaServicos ";
                s = s + "where  AnoMesDia      =  " + pAnoMesDia.ToString() + " \n";
                s = s + "and    CodigoCliente  =  " + pCodigoCliente + " \n";
                s = s + "and    ExecutarServico like '%" + pTipoResiduo + "%' \n";
                if (pExecutarServico != "")
                    s = s + "and    HoraProgramada  like '%" + pExecutarServico + "%' \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    return "Alterar";
                else
                    return "Incluir";
            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
        public string SequencialAdd(int pAnoMesDia, int pCodigoCliente, string pHora)
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Sequencial \n";
                s = s + "from   ProgramacaoDiariaServicos \n";
                s = s + "where  Hora          = '" + pHora + "' \n";
                s = s + "and    AnoMesDia     = " + pAnoMesDia + " \n";
                s = s + "and    CodigoCliente = " + pCodigoCliente + " \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)                    
                    return l_dt.Rows[0][0].ToString();
                else
                    return "0";
            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }

        public bool ExisteReprogramacaoFechada(string pHora, int pCodigoCliente, int pCodigoResiduo, string pExecutarServico)
        {
            bool bRet = false;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Sequencial \n";
                s = s + "from   ProgramacaoDiariaServicos \n";
                s = s + "where  Hora          = '" + pHora + "' \n";
                s = s + "and    StatusCor     = '" + geral.RetornaCodigoCor("Azul") + "' \n";
                s = s + "and    CodigoCliente = " + pCodigoCliente + " \n";
                s = s + "and    CodigoResiduo = " + pCodigoResiduo + " \n ";
                s = s + "and    ExecutarServico = '" + pExecutarServico + "' \n";
                s = s + "and    DataProgramada  >= '" + DateTime.Now.AddDays(-61).ToString("yyyy-MM-yy") + "' \n";
                s = s + "limit 1 \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    bRet = true;
                else
                    bRet = false;
            }
            catch
            {
                return false;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return bRet;
        }
        public bool ExisteReprogramacaoFutura(int pCodigoCliente, string pExecutarServico, string pDataProgramada, string pAnoMesDia, string pCodigoResiduo = "")
        {
            bool bRet = false;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Sequencial \n";
                s = s + "from   ReprogramacaoServicos \n";
                s = s + "where  StatusCor       = '8421631' \n";
                s = s + "and    CodigoCliente   = "  + pCodigoCliente + " \n";
                s = s + "and    ExecutarServico = '" + pExecutarServico.Replace("'", "") + "' \n";
                if (pDataProgramada != "" && pDataProgramada != null)
                    s = s + "and    DataProgramada >= '" + Convert.ToDateTime(pDataProgramada).ToString("yyyy-MM-dd") + "' \n";
                s = s + "and    AnoMesDia       = "  + pAnoMesDia + "\n";
                s = s + "and    TipoContrato = 0 \n ";
                if (pCodigoResiduo != "")
                    s = s + "and    CodigoResiduo = " + pCodigoResiduo + " \n";
                s = s + "limit 1 \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    bRet = true;
                else
                    bRet = false;
            }
            catch
            {
                return false;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return bRet;
        }

        public void ArrumaCodigoResiduo()
        {
            try
            {
                SILCNegocios.clsResiduos oResiduo = new clsResiduos();
                clsResiduoDados oResiduoDados = new clsResiduoDados();

                string _ServicoSemAcento = "";
                l_dt = PegaCodigoResiduo();
                foreach (DataRow _dr in l_dt.Rows)
                {
                    if (_dr["CodigoResiduo"].ToString() == "" || _dr["CodigoResiduo"].ToString() == "0")
                    {
                        _ServicoSemAcento = geral.RemoverAcentos(_dr["ExecutarServico"].ToString());
                        SalvarCodigoResiduo(oResiduoDados.PegaCodigoResiduoDescricaoSemAcento(_ServicoSemAcento), Convert.ToInt32(_dr["Sequencial"]));      
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }

        public string Inserir(clsProgramacaoDiariaServicos pProgramacao, bool pSoQuey = false, bool pPegarUltimoSequencial = false)
        {
            s = "";
            s = s + "insert into ProgramacaoDiariaServicos ";
            s = s + "(";
            if (pProgramacao.Sequencial > 0)
                s = s + "   Sequencial, ";
            s = s + "   AnoMesDia, Quadro, Linha, StatusCor, Data, Hora, Solicitante, CodigoCliente, \n";
            s = s + "   ExecutarServico, DataProgramada, HoraProgramada, Quantidade, \n";
            s = s + "   CodigoResiduo, CodigoCaminhao, CodigoMotorista, \n";
            s = s + "   Observacao, Unidade, SequencialParaQuadro1, TipoProgramacao, Quantidade2, \n";
            s = s + "   Unidade2, RotaMapa, CorObservacao, Origem, NomeMotoristaOuDescricao, DestinoFinal \n";
            s = s + ") \n";
            s = s + "values ";
            s = s + "(";
            if (pProgramacao.Sequencial > 0)
                s = s + " " + pProgramacao.Sequencial + ", ";
            s = s + " " + pProgramacao.AnoMesDia + ", ";
            s = s + " " + pProgramacao.Quadro + ", ";
            s = s + " " + pProgramacao.Linha + ", ";
            s = s + " '" + pProgramacao.StatusCor + "', ";
            if (pProgramacao.Data != "")
                s = s + "'" + Convert.ToDateTime(pProgramacao.Data).ToString("yyyy-MM-dd") + "', \n";
            else
                s = s + "'0001-01-01', \n";
            s = s + "'" + pProgramacao.Hora + "', ";
            s = s + "'" + pProgramacao.Solicitante + "', ";
            s = s + " " + pProgramacao.CodigoCliente + ", ";
            s = s + "'" + pProgramacao.ExecutarServico + "', ";
            if (pProgramacao.DataProgramada != "")
                s = s + "'" + Convert.ToDateTime(pProgramacao.DataProgramada).ToString("yyyy-MM-dd") + "', \n";
            else
                s = s + "'0001-01-01', \n";
            s = s + "'" + pProgramacao.ServicoExecutado + "', ";
            s = s + " " + pProgramacao.Quantidade.ToString().Replace(",", ".") + ", ";
            s = s + " " + pProgramacao.CodigoResiduo + ", ";
            s = s + " " + pProgramacao.CodigoCaminhao + ", ";
            s = s + " " + pProgramacao.CodigoMotorista + ", ";
            s = s + "'" + pProgramacao.Observacao + "', ";
            s = s + "'" + pProgramacao.Unidade + "', ";
            s = s + " " + pProgramacao.SequencialParaQuadro1 + ", ";
            s = s + " " + pProgramacao.TipoProgramacao + ", ";
            s = s + " " + pProgramacao.Quantidade2.ToString().Replace(",", ".") + ", ";
            s = s + "'" + pProgramacao.Unidade2 + "', ";
            s = s + "'" + pProgramacao.RotaMapa + "', ";
            s = s + "'" + pProgramacao.CorObservacao + "', ";
            s = s + "'" + pProgramacao.Origem + "', \n ";
            s = s + "'" + pProgramacao.NomeMotorista + "', \n";
            s = s + "'" + pProgramacao.DestinoFinal + "'";
            s = s + "); \n";
            if (!pSoQuey)
            {
                ConectaBanco();
                MySqlCommand command = oDB.MySqlConnect.CreateCommand();
                command.CommandTimeout = 360;
                MySqlTransaction transaction;
                transaction = oDB.MySqlConnect.BeginTransaction();
                command.Connection = oDB.MySqlConnect;
                command.Transaction = transaction;
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
                if (pPegarUltimoSequencial)
                { 
                    s = "";
                    s = s + "select Sequencial \n";
                    s = s + "from   ProgramacaoDiariaServicos \n";
                    s = s + "where  Hora          = '" + pProgramacao.Hora + "' \n";
                    s = s + "and    AnoMesDia     =  " + pProgramacao.AnoMesDia + " \n";
                    s = s + "and    CodigoCliente =  " + pProgramacao.CodigoCliente + " \n";
                    FillDataSet();
                    if (l_dt.Rows.Count > 0)
                        pProgramacao.Sequencial = Convert.ToInt32(l_dt.Rows[0][0].ToString());
                }
                DesconectaBanco();
            }
            return s;
        }
        public void SalvarCodigoResiduo(int pCodigoResiduo, int pSequencial)
        {
            if (pCodigoResiduo > 0 && pSequencial > 0)
            {
                ConectaBanco();
                MySqlCommand command = oDB.MySqlConnect.CreateCommand();
                command.CommandTimeout = 360;
                MySqlTransaction transaction;
                transaction = oDB.MySqlConnect.BeginTransaction();
                command.Connection = oDB.MySqlConnect;
                command.Transaction = transaction;
                try
                {
                    s = "";
                    s = s + "update ProgramacaoDiariaServicos \n";
                    s = s + "set    CodigoResiduo = " + pCodigoResiduo + " \n";
                    s = s + "where  Sequencial = " + pSequencial.ToString() + " \n";
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
                finally
                {
                    DesconectaBanco();
                }
            }
        }
        public string Alterar(clsProgramacaoDiariaServicos pProgramacao, int pSequencial, bool pSoQuery = false)
        {
            try
            {
                s = "";
                s = s + "update ProgramacaoDiariaServicos \n";
                s = s + "set    AnoMesDia  = " + pProgramacao.AnoMesDia + ", \n";
                s = s + "       Quadro     = " + pProgramacao.Quadro + ", \n";
                s = s + "       Linha      = " + pProgramacao.Linha + ", \n";
                if (pProgramacao.Data != "")
                    s = s + "       Data       = '" + Convert.ToDateTime(pProgramacao.Data).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "       Data       = '0001-01-01', \n";
                s = s + "       Hora       = '" + pProgramacao.Hora + "', \n";
                s = s + "       Solicitante = '" + pProgramacao.Solicitante + "', \n";
                s = s + "       CodigoCliente = " + pProgramacao.CodigoCliente + ", \n";
                s = s + "       ExecutarServico = '" + pProgramacao.ExecutarServico + "', \n";
                if (pProgramacao.DataProgramada != "")
                    s = s + "       DataProgramada = '" + Convert.ToDateTime(pProgramacao.DataProgramada).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "       DataProgramada = '0001-01-01', \n";
                s = s + "       HoraProgramada = '" + pProgramacao.ServicoExecutado + "', \n";
                s = s + "       Quantidade = " + pProgramacao.Quantidade.ToString().Replace(",", ".") + ", \n";
                s = s + "       CodigoResiduo = " + pProgramacao.CodigoResiduo + ", \n";
                s = s + "       CodigoCaminhao = " + pProgramacao.CodigoCaminhao + ", \n";
                s = s + "       CodigoMotorista = " + pProgramacao.CodigoMotorista + ", \n";
                s = s + "       Observacao = '" + pProgramacao.Observacao + "', \n";
                s = s + "       Unidade = '" + pProgramacao.Unidade + "', \n";
                s = s + "       SequencialParaQuadro1 = " + pProgramacao.SequencialParaQuadro1 + ", \n";
                s = s + "       TipoProgramacao = " + pProgramacao.TipoProgramacao + ", \n";
                s = s + "       Quantidade2   = " + pProgramacao.Quantidade2.ToString().Replace(",", ".") + ", \n";
                s = s + "       Unidade2      = '" + pProgramacao.Unidade2 + "', \n";
                s = s + "       RotaMapa      = '" + pProgramacao.RotaMapa + "', \n";
                s = s + "       CorObservacao = '" + pProgramacao.CorObservacao + "', \n";
                s = s + "       StatusCor     = '" + pProgramacao.StatusCor + "', \n";
                s = s + "       Origem        = '" + pProgramacao.Origem + "', \n";
                s = s + "       DestinoFinal  = '" + pProgramacao.DestinoFinal + "', \n";
                s = s + "       NomeMotoristaOuDescricao = '" + pProgramacao.NomeMotorista + "' \n";
                s = s + "where  Sequencial = " + pSequencial.ToString() + "; \n";
                if (!pSoQuery)
                {
                    ConectaBanco();
                    MySqlCommand command = oDB.MySqlConnect.CreateCommand();
                    command.CommandTimeout = 360;
                    MySqlTransaction transaction;
                    transaction = oDB.MySqlConnect.BeginTransaction();
                    command.Connection = oDB.MySqlConnect;
                    command.Transaction = transaction;
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                return s;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public DataTable PreencheDataTableProgramacao(string pCampoFiltro, string pTextoFiltro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select 0 as Rp, \n ";
                s = s + "       p.Sequencial, p.CodigoResiduo, p.AnoMesDia, p.Quadro, p.Linha, p.StatusCor, p.Data, p.Hora, p.Solicitante, p.CodigoCliente, \n";
                s = s + "       c.NomeFantasia as NomeFantasiaCliente, p.ExecutarServico, p.DataProgramada, p.HoraProgramada, p.Quantidade, \n";
                s = s + "       p.CodigoCaminhao, o.Modelo as ModeloCaminhao, p.CodigoMotorista, p.NomeMotoristaOuDescricao, m.Nome as NomeMotorista, \n";
                s = s + "       p.RotaMapa as Map, '' as Particularidade, p.DestinoFinal, p.Observacao, p.Unidade, p.SequencialParaQuadro1, Convert(p.TipoProgramacao, char) as TipoProgramacao, p.Quantidade2, \n";
                s = s + "       p.Unidade2, p.CorObservacao, p.Origem, \n";
                s = s + "       Concat(mu.Nome, ', ', e.Bairro, ', ', e.endereco, ', ', e.numero, ' ', e.complemento, ' |', c.PontoReferencia) as CidadeBairroEndereco, p.RotaMapa, NovaLinha \n";
                s = s + "from   ProgramacaoDiariaServicos p \n";
                s = s + "left   join Clientes     c on c.Codigo = p.CodigoCliente \n";
                s = s + "left   join Enderecos    e on e.Codigo = c.Codigo and e.TipoEndereco = 2 and e.TipoCadastro = 0 \n";
                s = s + "left   join Municipios   mu on e.CodigoMunicipio = mu.Codigo \n";
                s = s + "left   join Caminhoes    o on o.Codigo = p.CodigoCaminhao \n";
                s = s + "left   join Funcionarios m on m.Codigo = p.CodigoMotorista \n";
                if (pCampoFiltro.IndexOf("NomeFantasia") > -1)
                    s = s + "where NomeFantasia like '%" + pTextoFiltro + "%' \n";
                else if (pCampoFiltro.IndexOf("Nome") > -1)
                    s = s + "where Nome like '%" + pTextoFiltro + "%' \n";
                else if (pCampoFiltro.IndexOf("CodigoCliente") > -1)
                {
                    if (geral.IsNumeric(pTextoFiltro))
                        s = s + "where p.CodigoCliente = " + pTextoFiltro + " \n";
                    else
                        s = s + "where p.CodigoCliente = -1 \n";
                }
                s = s + "order  by p.Sequencial desc\n";
                s = s + "limit  1000 \n";
                FillDataSet();
                if (l_ds.Tables[0].Rows.Count > 0)
                {
                    return l_ds.Tables[0];
                }
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public void Excluir(int pSequencial)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "delete from ProgramacaoDiariaServicos ";
                s = s + "where  Sequencial = " + pSequencial.ToString();
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public void Excluir(string pHora, int pCodigoCliente)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "delete from ProgramacaoDiariaServicos \n";
                s = s + "where  Hora = '" + pHora.ToString() + "' \n";
                s = s + "and    CodigoCliente = " + pCodigoCliente + " \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                s = "";
                s = s + "delete from ReprogramacaoServicos \n";
                s = s + "where  Hora = '" + pHora.ToString() + "' \n";
                s = s + "and    CodigoCliente = " + pCodigoCliente + " \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public void ExcluirProgramacaoDia(string pAnoMesDia, bool pInclusiveFutura = false)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "delete from ProgramacaoDiariaServicos ";
                if (pInclusiveFutura)
                    s = s + "where  AnoMesDia >= " + pAnoMesDia;
                else
                    s = s + "where  AnoMesDia = " + pAnoMesDia;
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public bool SalvarNoQuadro1(string pSequencialQuadro, int pLinha, string pDataProgramacaoAberta)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            bool bReturn = false;
            try
            {
                if (pSequencialQuadro != "" || pSequencialQuadro != "0")
                {
                    s = "";
                    s = s + "update ProgramacaoDiariaServicos \n";
                    s = s + "set    StatusCor = '16761024', \n"; // não é pra mudar para quadro 1 está pegando pela cor
                    s = s + "       Linha = " + pLinha.ToString() + ", \n";
                    s = s + "       DataProgramada = '"  + Convert.ToDateTime(pDataProgramacaoAberta).ToString("yyyy-MM-dd") + "' \n";
                    s = s + "where  Sequencial = " + pSequencialQuadro + " \n";
                    s = s + "and    Quadro = 2 \n";
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                    bReturn = true;
                }
            }
            catch
            {
                bReturn = false;
                transaction.Rollback();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return bReturn;
        }
        public void SalvarNovaLinha(string pSequencial, string pNovaLinha)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update ProgramacaoDiariaServicos \n";
                s = s + "set    NovaLinha = "+ pNovaLinha + " \n";
                s = s + "where  Sequencial = " + pSequencial + " \n";
                s = s + "and    Quadro = 2 \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
        public void SalvarExecutando(string pSequencialQuadro)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update ProgramacaoDiariaServicos \n";
                s = s + "set    StatusCor = '65280' \n";
                s = s + "where  Sequencial = " + pSequencialQuadro + " \n";
                s = s + "and    Quadro = 2 \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
        public void SalvarCliente(string pSequencial, int pCodigoCliente)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                if (pSequencial != "" && pCodigoCliente > 0)
                {
                    s = "";
                    s = s + "update ProgramacaoDiariaServicos \n";
                    s = s + "set    CodigoCliente = " + pCodigoCliente + " \n";
                    s = s + "where  Sequencial = " + pSequencial + " \n";
                    s = s + "and    Quadro = 2 \n";
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
        public void SalvarSolicitante(string pSequencial, string pSolicitante)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                if (pSequencial != "")
                {
                    s = "";
                    s = s + "update ProgramacaoDiariaServicos \n";
                    s = s + "set    Solicitante = '" + pSolicitante + "' \n";
                    s = s + "where  Sequencial = " + pSequencial + " \n";
                    s = s + "and    Quadro = 2 \n";
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
        public void SalvarCaminhao(string pSequencial, int pCodigoCaminhao)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update ProgramacaoDiariaServicos \n";
                s = s + "set    CodigoCaminhao = " + pCodigoCaminhao + " \n";
                s = s + "where  Sequencial = " + pSequencial + " \n";
                s = s + "and    Quadro = 2 \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
        public void SalvarMotorista(string pSequencial, int pCodigoMotorista, string pNomeMotorista)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update ProgramacaoDiariaServicos \n";
                s = s + "set    CodigoMotorista = " + pCodigoMotorista + ", \n";
                s = s + "       NomeMotoristaOuDescricao = '" + pNomeMotorista + "' \n"; 
                s = s + "where  Sequencial = " + pSequencial + " \n";
                s = s + "and    Quadro = 2 \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
        public void SalvarServicoAExecutar(string pSequencial, string pServicoAExecutar)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update ProgramacaoDiariaServicos \n";
                s = s + "set    HoraProgramada = '" + geral.Left(pServicoAExecutar, 50) + "' \n";
                s = s + "where  Sequencial = " + pSequencial + " \n";
                s = s + "and    Quadro = 2 \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
        public void SalvarObservacao(string pSequencial, string pObservacao)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update ProgramacaoDiariaServicos \n";
                s = s + "set    Observacao = '" + pObservacao + "' \n";
                s = s + "where  Sequencial = " + pSequencial + " \n";
                s = s + "and    Quadro = 2 \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
        public void SalvarDestinoFinal(string pSequencial, string pDestinoFinal)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update ProgramacaoDiariaServicos \n";
                s = s + "set    DestinoFinal = '" + pDestinoFinal + "' \n";
                s = s + "where  Sequencial = " + pSequencial + " \n";
                s = s + "and    Quadro = 2 \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
        public void SalvarCancelar(string pSequencialQuadro, string pObservacao)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update ProgramacaoDiariaServicos \n";
                s = s + "set    StatusCor = '65535', \n";
                s = s + "       Observacao = '" + pObservacao + "' \n";
                s = s + "where  Sequencial = " + pSequencialQuadro + " \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
        public void SalvarCorManual(string pSequencialQuadro)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update ProgramacaoDiariaServicos \n";
                s = s + "set    StatusCor = '8427929' \n";
                s = s + "where  Sequencial = " + pSequencialQuadro + " \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
        public void SalvarStatusCor(string pSequencialQuadro, string pStatusCor)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update ProgramacaoDiariaServicos \n";
                s = s + "set    StatusCor = '" + pStatusCor + "' \n";
                s = s + "where  Sequencial = " + pSequencialQuadro + " \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
        public void SalvarDataReprogramada(string pSequencial, string pDataReprogramada)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update ProgramacaoDiariaServicos \n";
                s = s + "set    DataProgramada = '" + Convert.ToDateTime(pDataReprogramada).ToString("yyyy-MM-dd") + "' \n";
                s = s + "where  Sequencial = " + pSequencial + " \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
        public void SalvarDataProgramadaAnterior(string pHora, string pDataReprogramada, string pCodigoCliente)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update ProgramacaoDiariaServicos \n";
                s = s + "set    DataProgramada = '" + Convert.ToDateTime(pDataReprogramada).ToString("yyyy-MM-dd") + "' \n";
                s = s + "where  Hora = '" + pHora + "' \n";
                s = s + "and    CodigoCliente = " + pCodigoCliente + " \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
        public void SalvarRotaMapa(string pSequencialQuadro, string pMap)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update ProgramacaoDiariaServicos \n";
                s = s + "set    RotaMapa = '" + pMap + "' \n";
                s = s + "where  Sequencial = " + pSequencialQuadro + " \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
        public void LimparMapa(string pAnoMesDia)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update ProgramacaoDiariaServicos \n";
                s = s + "set    RotaMapa = '' \n ";
                s = s + "where  AnoMesDia = " + pAnoMesDia + " \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }

        public DataTable PegaDadosListaRota(int pAnoMesDia, int pQuadro, bool pOrdemNovaLinha)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select p.HoraProgramada, p.ExecutarServico, c.NomeFantasia as NomeFantasiaCliente, p.Unidade, p.Observacao, \n";
                s = s + "       Concat(mu.Nome, '-', e.Bairro, '-', e.endereco ) as CidadeBairroEndereco, m.Nome as NomeMotorista, o.Modelo as ModeloCaminhao, \n";
                s = s + "       Concat(mu.Nome, '-', e.Bairro, '-', e.endereco, '-', c.PontoReferencia, '-', e.Fone1, '-', e.Fone2, '-', e.Fone3, '-', e.Contato) as EnderecoCompleto, \n";
                s = s + "       p.CodigoCaminhao, p.CodigoMotorista, p.NomeMotoristaOuDescricao, \n";
                s = s + "       p.Sequencial, p.CodigoResiduo, p.AnoMesDia, p.Quadro, p.StatusCor, p.Data, p.Hora, p.Solicitante, p.CodigoCliente, \n";
                s = s + "       p.Quantidade2, \n";
                s = s + "       p.CorObservacao, p.Origem, p.NovaLinha \n";
                s = s + "from   ProgramacaoDiariaServicos p \n";
                s = s + "left   join Clientes c on c.Codigo = p.CodigoCliente \n";
                s = s + "left   join Enderecos e on e.Codigo = c.Codigo and e.TipoEndereco= 2 and e.TipoCadastro = 0 \n";
                s = s + "left   join Municipios mu on e.CodigoMunicipio = mu.Codigo \n";
                s = s + "left   join Caminhoes o on o.Codigo = p.CodigoCaminhao \n";
                s = s + "left   join Funcionarios m on m.Codigo = p.CodigoMotorista \n";
                if (pAnoMesDia > 0)
                {
                    s = s + "where p.AnoMesDia = " + pAnoMesDia.ToString() + " \n";
                    s = s + "and   p.Quadro = " + pQuadro.ToString() + " \n";
                    s = s + "and   p.StatusCor <> '16761024' \n";
                    s = s + "and   p.RotaMapa = 'X' \n";
                    if (pOrdemNovaLinha)
                        s = s + "order by NovaLinha \n";
                    else
                        s = s + "order by StatusCor desc, Sequencial, Linha \n";
                    FillDataSet();
                    if (l_ds.Tables[0].Rows.Count > 0)
                    {
                        return l_ds.Tables[0];
                    }
                }
                else
                    FillDataSet();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                return l_ds.Tables[0];
            }
            finally
            {
                DesconectaBanco();
            }
        }

        public string SqlAlter(string pSql)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                command.CommandText = pSql;
                command.ExecuteNonQuery();
                transaction.Commit();
                return string.Empty;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return ex.ToString();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
        
        public void SqlInsert(string pSql)
        {
            try
            {
                ConectaBanco();
                MySqlCommand command = oDB.MySqlConnect.CreateCommand();
                command.CommandTimeout = 360;
                MySqlTransaction transaction;
                transaction = oDB.MySqlConnect.BeginTransaction();
                command.Connection = oDB.MySqlConnect;
                command.Transaction = transaction;
                command.CommandText = pSql;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public bool RetornaParaQuadro2(string pSequencialQuadro, string pHora, string pCodigoCor)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            bool bReturn = false;
            try
            {
                s = "";
                s = s + "update ProgramacaoDiariaServicos \n";
                s = s + "set    StatusCor =  '" + pCodigoCor + "' \n"; // branco ou cinza
                s = s + "where  Sequencial =  " + pSequencialQuadro + " \n";
                s = s + "and    Hora       = '" + pHora + "' \n";
                s = s + "and    Quadro = 2 \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
                bReturn = true;
            }
            catch
            {
                transaction.Rollback();
                bReturn = false;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return bReturn;
        }

    }
}