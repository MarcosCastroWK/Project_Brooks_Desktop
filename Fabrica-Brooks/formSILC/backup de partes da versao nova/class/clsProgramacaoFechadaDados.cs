using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data.OleDb;
using SILCNegocios;

namespace LibSILC
{
    public class clsProgramacaoFechadaDados
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
            using (MySqlDataAdapter l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect))
            {
                l_myData.Fill(l_ds);
                l_dt = l_ds.Tables[0];
                l_myData.Dispose();
            }
        }
        private void DesconectaBanco()
        {
            oDB.DesconectaMySql();
        }
        public clsProgramacaoFechada PegaDados(clsProgramacaoFechada pProgramacaoFechada, int pCodigo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Data, Fechada, BloqueadaCodigoUsuario \n"; 
                s = s + "from   ProgramacaoFechada \n";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " \n";
                    s = s + "limit 1";
                }
                else if (pCodigo == 0)
                {
                    s = s + " order by Codigo limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows[0]["Codigo"].ToString() != "")
                        pProgramacaoFechada.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                    if (l_dt.Rows[0]["Data"].ToString() != "")
                        pProgramacaoFechada.Data = Convert.ToDateTime(l_dt.Rows[0]["Data"].ToString()).Date.ToShortDateString();
                    else
                        pProgramacaoFechada.Data = "";
                    if (l_dt.Rows[0]["Fechada"].ToString() != "")
                        pProgramacaoFechada.Fechada = Convert.ToInt32(l_dt.Rows[0]["Fechada"]);
                    if (l_dt.Rows[0]["BloqueadaCodigoUsuario"].ToString() != "")
                        pProgramacaoFechada.BloqueadaCodigoUsuario = Convert.ToInt32(l_dt.Rows[0]["BloqueadaCodigoUsuario"]);
                }                
            }
            catch (Exception ex)
            {
                pProgramacaoFechada = new clsProgramacaoFechada();
            }
            finally
            {
                DesconectaBanco();
            }
            return pProgramacaoFechada;
        }
        
        public DataTable PegaDados(clsProgramacaoFechada pProgramacaoFechada, int pCodigo, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Data, Fechada, BloqueadaCodigoUsuario \n"; 
                s = s + "from   ProgramacaoFechada \n";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " ";
                }
                if (pUltimoRegistro)
                {
                    s = s + "order by Codigo desc ";
                }
                else if (pCodigo == 0)
                {
                    s = s + " order by Campo ";
                }
                FillDataSet();
            }
            catch (Exception ex)
            {
                l_dt = new DataTable();
            }
            finally
            {
                DesconectaBanco();
            }
            return l_dt;
        }
        public int PegaTamanhoCampoVarChar(string pCampo)
        {
            int iRet = 0;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "show columns ";
                s = s + "from ProgramacaoFechada ";
                s = s + "where  type like 'varchar%' and field = '" + pCampo + "' ";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                {
                    iRet = Convert.ToInt16(l_dt.Rows[0]["Type"].ToString().Replace("varchar(", "").Replace(")", ""));
                }
            }
            catch (Exception ex)
            {
                iRet = 0;
            }
            finally
            {
                DesconectaBanco();
            }
            return iRet;
        }
        public string DadoExiste(int pCodigo)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Codigo ";
                s = s + "from   ProgramacaoFechada ";
                if (pCodigo != 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " ";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pCodigo != 0)
                    sRet = "Alterar";
                else
                    sRet = "Incluir";
            }
            catch (Exception ex)
            {
                sRet = "Erro: " + ex.Message;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }
        public string UltimoRegistroBloqueado()
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Codigo \n";
                s = s + "from   ProgramacaoFechada \n";
                s = s + "where  Fechada = 0 and BloqueadoPeloUsuario > '' \n";
                s = s + "order  by Codigo desc \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    sRet = l_ds.Tables[0].Rows[0]["Codigo"].ToString();
                else
                    sRet = "";
            }
            catch (Exception ex)
            {
                sRet = "Erro: " + ex.Message;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }
        public string UltimoRegistroNomeBloqueado()
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select BloqueadoPeloUsuario \n";
                s = s + "from   ProgramacaoFechada \n";
                s = s + "where  Fechada = 0 \n";
                s = s + "order  by Codigo desc limit 1 \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    sRet = l_ds.Tables[0].Rows[0]["BloqueadoPeloUsuario"].ToString();
                else
                    sRet = "";
            }
            catch (Exception ex)
            {
                sRet = "Erro: " + ex.Message;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }
        public string DataUltimaProgAberta()
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Data \n";
                s = s + "from   ProgramacaoFechada \n";
                s = s + "order  by Data desc \n";
                s = s + "limit  1 ";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    sRet = l_ds.Tables[0].Rows[0]["Data"].ToString();
                else
                    sRet = "";
            }
            catch (Exception ex)
            {
                sRet = "Erro: " + ex.Message;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }
        public string LiberarProgramacaoBloqueada()
        {
            string sRet = "";
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                string _codigo = UltimoRegistroBloqueado();
                if (_codigo != "")
                {
                    s = "";
                    s = s + "update ProgramacaoFechada \n";
                    s = s + "set    BloqueadoPeloUsuario = ''";
                    s = s + "where  Fechada = 0 and Codigo = " + _codigo;
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                    sRet = "Liberou";
                }
                else
                    sRet = "A programação já está liberada!";
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                sRet = ex.ToString();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }
        public string Inserir(clsProgramacaoFechada pProgramacaoFechada, int pCodigo = 0, bool pSoSql = false)
        {
            string sRet = "";
            try
            {
                s = "";
                s = s + "insert into ProgramacaoFechada \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + " Codigo, ";
                s = s + "  Data, Fechada, BloqueadoPeloUsuario \n";  
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + pCodigo + ", " ;
                if (pProgramacaoFechada.Data != "")
                    s = s + "'" + Convert.ToDateTime(pProgramacaoFechada.Data).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                if (pProgramacaoFechada.Fechada > 0)
                    s = s + " " + pProgramacaoFechada.Fechada.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                s = s + " '" + pProgramacaoFechada.BloqueadaNomeUsuario + "' \n";
                s = s + "); ";
                if (!pSoSql)
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
                    sRet = "";
                }
                else
                    sRet = s;
            }
            finally
            {
                DesconectaBanco();
            }
            return sRet;
        }
        public string Alterar(clsProgramacaoFechada pProgramacaoFechada, int pCodigo, bool pSoSql = false)
        {
            string sRet = "";
            try
            {
                s = "";
                s = s + "update ProgramacaoFechada \n";

                if (pProgramacaoFechada.Data != "")
                    s = s + " set Data = '" + Convert.ToDateTime(pProgramacaoFechada.Data).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + " set Data = '0001-01-01', \n";
                if (pProgramacaoFechada.Fechada > 0)
                    s = s + " Fechada = " + pProgramacaoFechada.Fechada.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " Fechada = 0, \n";
                s = s + " BloqueadoPeloUsuario = '" + pProgramacaoFechada.BloqueadaNomeUsuario + "' \n";
                s = s + "where Codigo = " + pCodigo + "; \n";

                if (!pSoSql)
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
                    sRet = string.Empty;
                }
                else
                    sRet = s;
                
            }
            catch (Exception ex)
            {
                sRet = ex.ToString();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }
        public DataTable PreencheDataTable(string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Data, Fechada, BloqueadaCodigoUsuario \n";
                s = s + "from   ProgramacaoFechada \n";
                s = s + "order by " + pOrdem;
                l_ds = new DataSet();
                FillDataSet();
                DesconectaBanco();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                DesconectaBanco();
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                return l_dt;
            }
        }
        public DataTable PreencheDataTable(string pOrdem, string pFiltro, string pCampo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Data, Fechada, BloqueadaCodigoUsuario \n"; 
                s = s + "from   ProgramacaoFechada \n";
                s = s + "where " + pCampo + " like '" + pFiltro + "%' \n";
                s = s + "order by " + pOrdem +" \n";
                l_ds = new DataSet();
                FillDataSet();
                DesconectaBanco();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                DesconectaBanco();
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                return l_dt;
            }
        }

        public string Excluir(int pCodigo)
        {
            string sRet = "";
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            command.CommandTimeout = 360;
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                if (pCodigo > 0)
                {
                    s = "";
                    s = s + "delete from ProgramacaoFechada ";
                    s = s + "where  Codigo = " + pCodigo.ToString();
                }
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
                sRet = string.Empty;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                sRet = ex.Message.ToString();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }
        public string UsuarioQueBloqueouProgramacaoAberta(string pNomeUsuario)
        {
            string sRet = "";
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
                s = s + "update ProgramacaoFechada \n";
                s = s + "set    BloqueadoPeloUsuario = '" + pNomeUsuario + "' \n";
                s = s + "where  Fechada = 0 \n";
                s = s + "order  by Codigo desc \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
                sRet = string.Empty;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                sRet = ex.ToString();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }
        public string FecharProgramacaoAberta(string pNomeUsuario)
        {
            string sRet = "";
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
                s = s + "update ProgramacaoFechada \n";
                s = s + "set    BloqueadoPeloUsuario = '" + pNomeUsuario + "', \n";
                s = s + "       Fechada = 1 \n";
                s = s + "where  Fechada = 0 \n";
                s = s + "order  by Codigo desc \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
                sRet = string.Empty;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                sRet = ex.ToString();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }
 
        public string AbreProgramacao(string pNomeUsuario, string pData)
        {
            string sRet = "";
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
                s = s + "Insert ProgramacaoFechada \n";
                s = s + "  (BloqueadoPeloUsuario, Fechada, Data) \n";
                s = s + "Values ('" + pNomeUsuario + "', \n";
                s = s + "       0, \n";
                s = s + "'" + Convert.ToDateTime(pData).ToString("yyyy-MM-dd") + "' \n";
                s = s + ") \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
                sRet = string.Empty;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                sRet = ex.ToString();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }
        public string LiberaProgramacaoParaOutroUsuario(string pNomeUsuario)
        {
            string sRet = "";
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
                s = s + "update ProgramacaoFechada \n";
                s = s + "set    BloqueadoPeloUsuario = '' \n";
                s = s + "where  Fechada = 0 \n";
                s = s + "and    BloqueadoPeloUsuario = '" + pNomeUsuario + "' \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
                sRet = string.Empty;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                sRet = ex.ToString();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }

        private void PrencheDTFutura(clsProgramacaoDiariaServicos pProgramacaoDiaria, DataTable dtFutura)
        {
            DataRow drFutura = dtFutura.NewRow();
            drFutura["Sequencial"] = 0;
            drFutura["AnoMesDia"] = pProgramacaoDiaria.AnoMesDia;
            drFutura["CodigoCaminhao"] = pProgramacaoDiaria.CodigoCaminhao;
            drFutura["CodigoCliente"] = pProgramacaoDiaria.CodigoCliente;
            drFutura["CodigoMotorista"] = pProgramacaoDiaria.CodigoMotorista;
            drFutura["CodigoResiduo"] = pProgramacaoDiaria.CodigoResiduo;
            drFutura["CorObservacao"] = pProgramacaoDiaria.CorObservacao;
            drFutura["Data"] = pProgramacaoDiaria.Data;
            drFutura["DataProgramada"] = pProgramacaoDiaria.DataProgramada;
            drFutura["ExecutarServico"] = pProgramacaoDiaria.ExecutarServico;
            drFutura["Hora"] = pProgramacaoDiaria.Hora;
            drFutura["Linha"] = pProgramacaoDiaria.Linha;

            drFutura["ModeloCaminhao"] = pProgramacaoDiaria.ModeloCaminhao;
            if (pProgramacaoDiaria.CodigoCaminhao > 0 && pProgramacaoDiaria.ModeloCaminhao == "")
            {
                clsCaminhoesDados oCaminhao = new clsCaminhoesDados();
                drFutura["ModeloCaminhao"] = oCaminhao.PegaDados(new clsCaminhoes(), pProgramacaoDiaria.CodigoCaminhao).Modelo;
            }
            drFutura["NomeFantasiaCliente"] = pProgramacaoDiaria.NomeCliente;

            drFutura["NomeMotorista"] = pProgramacaoDiaria.NomeMotorista;
            if (pProgramacaoDiaria.CodigoMotorista > 0 && pProgramacaoDiaria.NomeMotorista =="")
            {
                clsFuncionarioDados oMotorista = new clsFuncionarioDados();
                drFutura["NomeMotorista"] = oMotorista.PegaDados(new clsFuncionarios(), pProgramacaoDiaria.CodigoMotorista).Nome;
            }

            drFutura["Observacao"] = pProgramacaoDiaria.Observacao;
            drFutura["DestinoFinal"] = pProgramacaoDiaria.DestinoFinal;
            drFutura["Origem"] = pProgramacaoDiaria.Origem;
            drFutura["Quadro"] = pProgramacaoDiaria.Quadro;
            drFutura["Quantidade"] = pProgramacaoDiaria.Quantidade;
            drFutura["Quantidade2"] = pProgramacaoDiaria.Quantidade2;
            drFutura["RotaMapa"] = pProgramacaoDiaria.RotaMapa;
            drFutura["Particularidade"] = "";
            drFutura["SequencialParaQuadro1"] = pProgramacaoDiaria.SequencialParaQuadro1;
            drFutura["HoraProgramada"] = pProgramacaoDiaria.ServicoExecutado;
            drFutura["Solicitante"] = pProgramacaoDiaria.Solicitante;
            drFutura["StatusCor"] = pProgramacaoDiaria.StatusCor;
            drFutura["TipoProgramacao"] = pProgramacaoDiaria.TipoProgramacao.ToString();
            drFutura["Unidade"] = pProgramacaoDiaria.Unidade;
            drFutura["Unidade2"] = pProgramacaoDiaria.Unidade2;
            dtFutura.Rows.Add(drFutura);

        }

        public bool ExisteAntecipacao(DataRow[] pdrArray_Antecipacao, clsProgramacaoDiariaServicos pProgramacaoFutura)
        {
            bool bRet = false;
            foreach (DataRow drRep in pdrArray_Antecipacao)
            {
                clsProgramacaoDiariaServicos oProgramacaoAntecipada = new clsProgramacaoDiariaServicos();
                if (drRep["AnoMesDia"].ToString() != "")
                    oProgramacaoAntecipada.AnoMesDia = Convert.ToInt32(drRep["AnoMesDia"]);
                oProgramacaoAntecipada.Data = drRep["Data"].ToString();
                oProgramacaoAntecipada.CodigoCliente = Convert.ToInt32(drRep["CodigoCliente"]);
                oProgramacaoAntecipada.NomeCliente = drRep["NomeCliente"].ToString();
                oProgramacaoAntecipada.ExecutarServico = drRep["ExecutarServico"].ToString();
                if (oProgramacaoAntecipada.AnoMesDia == pProgramacaoFutura.AnoMesDia &&
                    Convert.ToDateTime(oProgramacaoAntecipada.Data).ToString("dd/MM/yyyy") == Convert.ToDateTime(pProgramacaoFutura.Data.ToString().Substring(0, 10)).ToString("dd/MM/yyyy") &&
                    oProgramacaoAntecipada.CodigoCliente == pProgramacaoFutura.CodigoCliente &&
                    oProgramacaoAntecipada.NomeCliente == pProgramacaoFutura.NomeCliente.ToString() &&
                    oProgramacaoAntecipada.ExecutarServico == pProgramacaoFutura.ExecutarServico.ToString())
                {
                    bRet = true;
                    break;
                }
            }
            return bRet;
        }

        public DataRow[] PegaAntecipacoes(string pDataFutura)
        {
            ConectaBanco();
            s = "";
            s = s + "select r.*, c.NomeFantasia as NomeCliente \n";
            s = s + "from   ReprogramacaoServicos r \n";
            s = s + "inner  join Clientes c on c.Codigo = r.CodigoCliente \n";
            s = s + "where  (r.DataProgramada = '" + Convert.ToDateTime(pDataFutura).ToString("yyyy-MM-dd") + "' \n";
            s = s + "        or r.AnoMesDia = '" + Convert.ToDateTime(pDataFutura).ToString("yyyyMMdd") + "') \n";
            s = s + "and    (r.StatusCor = '8421631' or r.StatusCor = '8427929' or r.StatusCor = '15000000') \n";
            s = s + "order  by CONVERT(r.StatusCor, UNSIGNED) desc, r.Sequencial desc \n";
            l_ds = new DataSet();
            FillDataSet();
            DesconectaBanco();
            return l_ds.Tables[0].Select("StatusCor = '" + geral.RetornaCodigoCor("AzulClaro") + "'", "");
        }

        private clsProgramacaoDiariaServicos ServicosFutura(clsProgramacaoDiariaServicos pProgDiaria, int pCodigoCliente, 
                                                            string pDataProgramada, bool pEncerramento)
        {
            if (pEncerramento)
            {
                clsServicosFutura oServicosFutura = new clsServicosFutura();
                clsServicosFuturaDados oServicosFuturaDados = new clsServicosFuturaDados();
                DataTable _dtServicos = new DataTable();
                _dtServicos = oServicosFuturaDados.PreencheDataTable(pCodigoCliente.ToString(), pDataProgramada);
                if (_dtServicos.Rows.Count >= 0)
                {
                    DataRow[] _drServicosFutura;
                    if (pProgDiaria.ExecutarServico != "" && pProgDiaria.CodigoResiduo != 0)
                        _drServicosFutura = _dtServicos.Select("CodigoCliente = " + pProgDiaria.CodigoCliente.ToString() +
                                                               "and CodigoResiduo = " + pProgDiaria.CodigoResiduo.ToString());
                    else
                        _drServicosFutura = _dtServicos.Select("CodigoCliente = " + pProgDiaria.CodigoCliente.ToString() +
                                                               "and DescricaoResiduo = '" + pProgDiaria.ExecutarServico + "'");
                    if (_drServicosFutura.Length > 0)
                    {
                        if (_drServicosFutura[0]["CodigoCaminhao"].ToString() != "")
                            oServicosFutura.CodigoCaminhao = Convert.ToInt32(_drServicosFutura[0]["CodigoCaminhao"].ToString());
                        oServicosFutura.ModeloCaminhao = _drServicosFutura[0]["ModeloCaminhao"].ToString();
                        if (_drServicosFutura[0]["CodigoMotorista"].ToString() != "")
                            oServicosFutura.CodigoMotorista = Convert.ToInt32(_drServicosFutura[0]["CodigoMotorista"].ToString());
                        oServicosFutura.NomeMotorista = _drServicosFutura[0]["NomeMotorista"].ToString();
                        oServicosFutura.MapaMarcado = 0;
                        if (_drServicosFutura[0]["MapaMarcado"].ToString() == "1")
                            oServicosFutura.MapaMarcado = 1;
                        oServicosFutura.Observacao = _drServicosFutura[0]["Observacao"].ToString();
                        oServicosFutura.DestinoFinal = _drServicosFutura[0]["DestinoFinal"].ToString();
                        oServicosFutura.ServicoAExecutar = _drServicosFutura[0]["ServicoAExecutar"].ToString();

                        if (oServicosFutura.CodigoCaminhao > 0)
                            pProgDiaria.CodigoCaminhao = oServicosFutura.CodigoCaminhao;
                        if (oServicosFutura.ModeloCaminhao != "")
                            pProgDiaria.ModeloCaminhao = oServicosFutura.ModeloCaminhao;
                        if (oServicosFutura.CodigoMotorista > 0)
                            pProgDiaria.CodigoMotorista = oServicosFutura.CodigoMotorista;
                        if (oServicosFutura.NomeMotorista != "")
                            pProgDiaria.NomeMotorista = oServicosFutura.NomeMotorista;
                        if (oServicosFutura.MapaMarcado == 1)
                            pProgDiaria.RotaMapa = "X";
                        if (oServicosFutura.Observacao != "")
                            pProgDiaria.Observacao = oServicosFutura.Observacao;
                        if (oServicosFutura.DestinoFinal != "")
                            pProgDiaria.DestinoFinal = oServicosFutura.DestinoFinal;
                        if (oServicosFutura.ServicoAExecutar != "")
                            pProgDiaria.ServicoExecutado  = oServicosFutura.ServicoAExecutar;
                    }
                }
            }
            return pProgDiaria;
        }
        public DataTable MontaProgramacao(string pDataFutura, string pUsuario, bool pEncerramento = false)
        {
            // Cria Reprogramação de serviços
            clsReprogramacaoServicos oRepr = new clsReprogramacaoServicos();
            clsReprogramacaoDados oReprServs = new clsReprogramacaoDados();
            clsProgramacaoDiariaServicos oProgramacaoDiaria = new clsProgramacaoDiariaServicos();
            clsProgramacaoDados oProgramacaoDados = new clsProgramacaoDados();
            bool existeReprogramacaoFechada = false;
            bool existeReprogramacaoFutura = false;
            DataRow[] drArrayAntecipacaoReprogr;

            string ultimaDataFev = "";
            ultimaDataFev = Convert.ToDateTime("01/03/" + Convert.ToDateTime(pDataFutura).Year.ToString()).AddDays(-1).ToString("");

            string _anomesdia;
            _anomesdia = Convert.ToDateTime(pDataFutura).ToString("yyyy") + Convert.ToDateTime(pDataFutura).ToString("MM") + Convert.ToDateTime(pDataFutura).ToString("dd");

            DataTable dtFutura = new DataTable();
            oProgramacaoDados.ExcluirProgramacaoDia(Convert.ToDateTime(pDataFutura).ToString("yyyyMMdd"), false);
            dtFutura = oProgramacaoDados.PegaDados(Convert.ToInt32(Convert.ToDateTime("11/11/1111").ToString("yyyyMMdd")), 2, true);

            ConectaBanco();
            s = "";
            s = s + "select (select Sequencial     from ReprogramacaoServicos where Sequencial = mseq limit 1) as Sequencial, \n";
            s = s + "       (select Data           from ReprogramacaoServicos where Sequencial = mseq limit 1) as Data, \n";
            s = s + "       Hora, \n";
            s = s + "       (select AnoMesDia      from ReprogramacaoServicos where Sequencial = mseq limit 1) as AnoMesDia, \n";
            s = s + "       (select Solicitante    from ReprogramacaoServicos where Sequencial = mseq limit 1) as Solicitante, \n";
            s = s + "       CodigoCliente, NomeFantasia as NomeCliente, \n";
            s = s + "       (select NomeClienteOuDescricao from ReprogramacaoServicos where Sequencial = mseq limit 1) as NomeClienteOuDescricao, \n";
            s = s + "       (select ExecutarServico        from ReprogramacaoServicos where Sequencial = mseq limit 1) as ExecutarServico, \n";
            s = s + "       (select DataProgramada         from ReprogramacaoServicos where Sequencial = mseq limit 1) as DataProgramada, \n";
            s = s + "       (select HoraProgramada         from ReprogramacaoServicos where Sequencial = mseq limit 1) as HoraProgramada, \n";
            s = s + "       (select Observacao             from ReprogramacaoServicos where Sequencial = mseq limit 1) as Observacao, \n";
            s = s + "       (select Unidade                from ReprogramacaoServicos where Sequencial = mseq limit 1) as Unidade, \n";
            s = s + "       (select Unidade2               from ReprogramacaoServicos where Sequencial = mseq limit 1) as Unidade2, \n";
            s = s + "       (select StatusCor              from ReprogramacaoServicos where Sequencial = mseq limit 1) as StatusCor, \n";
            s = s + "       (select TipoContrato           from ReprogramacaoServicos where Sequencial = mseq limit 1) as TipoContrato, \n";
            s = s + "       (select MapaMarcado            from ReprogramacaoServicos where Sequencial = mseq limit 1) as MapaMarcado, \n";
            s = s + "       (select CodigoResiduo          from ReprogramacaoServicos where Sequencial = mseq limit 1) as CodigoResiduo, \n";
            s = s + "       (select CodigoCaminhao         from ReprogramacaoServicos where Sequencial = mseq limit 1) as CodigoCaminhao, \n";
            s = s + "       (select ModeloCaminhao         from ReprogramacaoServicos where Sequencial = mseq limit 1) as ModeloCaminhao, \n";
            s = s + "       (select CodigoMotorista        from ReprogramacaoServicos where Sequencial = mseq limit 1) as CodigoMotorista, \n";
            s = s + "       (select NomeMotoristaOuDescricao from ReprogramacaoServicos where Sequencial = mseq limit 1) as NomeMotoristaOuDescricao, \n";
            s = s + "       (select SequencialProgramacaoDiaria from ReprogramacaoServicos where Sequencial = mseq limit 1) as SequencialProgramacaoDiaria, \n";
            s = s + "       (select Quantidade             from ReprogramacaoServicos where Sequencial = mseq limit 1) as Quantidade, \n";
            s = s + "       (select DestinoFinal           from ReprogramacaoServicos where Sequencial = mseq limit 1) as DestinoFinal \n";
            s = s + "from \n";
            s = s + "   (select max(sequencial) as mseq, Hora, CodigoCliente, NomeFantasia \n";
            s = s + "    from   ReprogramacaoServicos r \n";
            s = s + "    inner  join Clientes c on c.Codigo = r.CodigoCliente \n";
            s = s + "    where  (r.DataProgramada = '" + Convert.ToDateTime(pDataFutura).ToString("yyyy-MM-dd") + "' \n";
            s = s + "           or r.AnoMesDia = '" + Convert.ToDateTime(pDataFutura).ToString("yyyyMMdd") + "') \n";
            s = s + "    and    (r.StatusCor = '8421631' or r.StatusCor = '8427929' or r.StatusCor = '15000000') \n"; 
            s = s + "    group  by Hora, CodigoCliente, NomeFantasia  order by Sequencial, CodigoCliente \n";
            s = s + "   ) x \n";

            //s = "";
            //s = s + "select r.*, c.NomeFantasia as NomeCliente \n";
            //s = s + "from   ReprogramacaoServicos r \n";
            //s = s + "inner  join Clientes c on c.Codigo = r.CodigoCliente \n";
            //s = s + "where  (r.DataProgramada = '" + Convert.ToDateTime(pDataFutura).ToString("yyyy-MM-dd") + "' \n";
            //s = s + "        or r.AnoMesDia = '" + Convert.ToDateTime(pDataFutura).ToString("yyyyMMdd") + "') \n";
            //s = s + "and    (r.StatusCor = '8421631' or r.StatusCor = '8427929' or r.StatusCor = '15000000') \n";
            //s = s + "group  by Hora, CodigoCliente, c.NomeFantasia \n";
            //s = s + "and    not exists(select CodigoCliente \n";
            //s = s + "                  from   ReprogramacaoServicos \n";
            //s = s + "                  where  CodigoCliente = r.CodigoCliente \n";
            //s = s + "                 #and    DataProgramada = '" + Convert.ToDateTime(pDataFutura).ToString("yyyy-MM-dd") + "' \n";
            //s = s + "                  and    Sequencial > r.Sequencial \n";
            //s = s + "                  and    Hora = r.Hora) limit 1 \n";
            //s = s + "order  by CONVERT(r.StatusCor, UNSIGNED) desc, r.Sequencial desc \n";
            //s = s + "order  by r.Sequencial desc \n";
            l_ds = new DataSet();
            // abre Reprogramacao de Servicos 1ª parte 
            FillDataSet();
            DesconectaBanco();
            drArrayAntecipacaoReprogr = l_ds.Tables[0].Select("StatusCor = '" + geral.RetornaCodigoCor("AzulClaro") + "' or " +
                                                              "StatusCor = '" + geral.RetornaCodigoCor("Vermelho") + "'", "");

            foreach (DataRow _drReprServs in l_ds.Tables[0].Rows)
            {
                if (Convert.ToInt32(_drReprServs["CodigoCliente"]) == 2951)
                {
                    oProgramacaoDiaria.CodigoCliente = 2951;
                    //oProgramacaoDiaria.ExecutarServico = _drReprServs["ExecutarServico"].ToString();
                    //if (_drReprServs["CodigoResiduo"].ToString() != "")
                    //    oProgramacaoDiaria.CodigoResiduo = Convert.ToInt32(_drReprServs["CodigoResiduo"]);
                }
                // Verifica se já foi feito uma antecipação
                existeReprogramacaoFechada = false;

                // senão for antecipação verifica
                if (_drReprServs["StatusCor"].ToString() != geral.RetornaCodigoCor("AzulClaro"))
                    existeReprogramacaoFechada = oProgramacaoDados.ExisteReprogramacaoFechada(_drReprServs["Hora"].ToString(), Convert.ToInt32(_drReprServs["CodigoCliente"]),
                                                                                                Convert.ToInt32(_drReprServs["CodigoResiduo"]), _drReprServs["ExecutarServico"].ToString());
                if (!existeReprogramacaoFechada)
                {
                    oProgramacaoDiaria.AnoMesDia = Convert.ToInt32(_anomesdia);
                    oProgramacaoDiaria.Data = _drReprServs["Data"].ToString();
                    if (_drReprServs["StatusCor"].ToString() == geral.RetornaCodigoCor("AzulClaro") || _drReprServs["StatusCor"].ToString() == geral.RetornaCodigoCor("Vermelho"))
                        oProgramacaoDiaria.DataProgramada = _drReprServs["DataProgramada"].ToString();
                    else
                        oProgramacaoDiaria.DataProgramada = Convert.ToDateTime(pDataFutura).ToString("yyyy-MM-dd");
                    if (_drReprServs["Hora"] != null)
                        oProgramacaoDiaria.Hora = _drReprServs["Hora"].ToString();
                    if (_drReprServs["CodigoResiduo"].ToString() != "")
                        oProgramacaoDiaria.CodigoResiduo = Convert.ToInt32(_drReprServs["CodigoResiduo"]);
                    oProgramacaoDiaria.Solicitante = _drReprServs["Solicitante"].ToString();
                    oProgramacaoDiaria.CodigoCliente = Convert.ToInt32(_drReprServs["CodigoCliente"]);
                    oProgramacaoDiaria.NomeCliente = _drReprServs["NomeCliente"].ToString();
                    oProgramacaoDiaria.StatusCor = _drReprServs["StatusCor"].ToString();
                    oProgramacaoDiaria.ExecutarServico = _drReprServs["ExecutarServico"].ToString();
                    oProgramacaoDiaria.CodigoCaminhao = Convert.ToInt32(_drReprServs["CodigoCaminhao"]);
                    oProgramacaoDiaria.ModeloCaminhao = _drReprServs["ModeloCaminhao"].ToString();
                    oProgramacaoDiaria.CodigoMotorista = Convert.ToInt32(_drReprServs["CodigoMotorista"]);
                    oProgramacaoDiaria.NomeMotorista = _drReprServs["NomeMotoristaOuDescricao"].ToString();
                    oProgramacaoDiaria.Observacao = _drReprServs["Observacao"].ToString();
                    oProgramacaoDiaria.DestinoFinal = _drReprServs["DestinoFinal"].ToString();
                    oProgramacaoDiaria.RotaMapa = _drReprServs["MapaMarcado"].ToString();

                    oProgramacaoDiaria.ServicoExecutado = _drReprServs["HoraProgramada"].ToString();
                    oProgramacaoDiaria.SequencialParaQuadro1 = Convert.ToInt32(_drReprServs["SequencialProgramacaoDiaria"]);
                    if (_drReprServs["Quantidade"].ToString() != "")
                    {
                        oProgramacaoDiaria.Quantidade = Convert.ToInt32(_drReprServs["Quantidade"]);
                        if (oProgramacaoDiaria.CodigoCliente == 2260)
                            oProgramacaoDiaria.Quantidade = Convert.ToInt32(_drReprServs["Quantidade"]);
                    }
                    oProgramacaoDiaria.Unidade = _drReprServs["Unidade"].ToString();
                    oProgramacaoDiaria.Quadro = 2;
                    oProgramacaoDiaria.TipoProgramacao = Convert.ToInt32(_drReprServs["TipoContrato"]);
                    oProgramacaoDiaria.Quantidade2 = 0;
                    oProgramacaoDiaria.Unidade2 = _drReprServs["Unidade2"].ToString();

                    ServicosFutura(oProgramacaoDiaria, oProgramacaoDiaria.CodigoCliente, oProgramacaoDiaria.DataProgramada, pEncerramento);

                    //sExisteBloqueioFinanceiro = oBloqFinanceiro.ExisteBloqueio(oReprServs.CodigoCliente)
                    //If sExisteBloqueioFinanceiro > "" 
                    //    oProgServsNova.Observacao = "BLOQUEIO FINANCEIRO DESDE " + sExisteBloqueioFinanceiro
                    //End If

                    // adiciona reprogramação 
                    if (pEncerramento)
                        oProgramacaoDados.Inserir(oProgramacaoDiaria);
                    else
                        PrencheDTFutura(oProgramacaoDiaria, dtFutura);
                    //' Grava status da programação atual, caso a última programação
                    //GravaStatusProgramacaoAtual oReprServs.StatusCor, oProgServsNova.sequencial
                }
            }
            // Abrir lista de Contrato Clientes / dias de coleta.
            // SEMANAL
            ConectaBanco();
            s = "";
            s = s + "select * from \n";
            s = s + "( \n";
            s = s + "    select distinct cr.CodigoCliente, res.DescricaoReduzida as DescricaoResiduo, c.NomeFantasia as NomeCliente, res.Unidade, \n";
            s = s + "                    cres.DiasColeta, r.CodigoContrato, r.Data, cres.QuantidadeFranquia, cres.CodigoResiduo \n";
            s = s + "    from   Reajustes r \n";
            s = s + "    inner  join Contratos        cr on cr.Codigo = r.CodigoContrato \n";
            s = s + "    inner  join ContratoResiduos cres on cres.CodigoContrato = cr.Codigo \n";
            s = s + "    inner  join Residuos res on res.Codigo = cres.CodigoResiduo \n";
            s = s + "    inner  join Clientes c on c.Codigo = cr.CodigoCliente \n";
            s = s + "    where  ucase(cres.Roteiro) = 'SEMANAL' \n";
            s = s + "    and    r.CodigoContrato = cr.Codigo \n";
            s = s + "    and    (length(ltrim(cres.DiasColeta)) > 0)  \n";
            s = s + "    and    (c.Inativo <> 1 Or c.Inativo Is Null)  \n";
            s = s + "    and    cres.DataReajuste = r.Data \n";
            s = s + "    and    (cr.DataRecisao = '100-1-1' OR cr.DataRecisao = '0001-01-01' OR cr.DataRecisao = '0100-01-01' OR isnull(cr.DataRecisao)) \n";
            s = s + "union all \n";
            s = s + "    select distinct cr.CodigoCliente, res.DescricaoReduzida as DescricaoResiduo, c.NomeFantasia as NomeCliente, res.Unidade, \n";
            s = s + "                    cres.DiasColeta, 0 as CodigoContrato, '' as Data, cres.QuantidadeFranquia, cres.CodigoResiduo \n";
            s = s + "    from   Contratos cr \n";
            s = s + "    inner  join ContratoResiduos cres on cres.CodigoContrato = cr.Codigo \n";
            s = s + "    inner  join Residuos res on res.Codigo = cres.CodigoResiduo \n";
            s = s + "    inner  join Clientes c on c.Codigo = cr.CodigoCliente \n";
            s = s + "    where  ucase(cres.Roteiro) = 'SEMANAL' \n";
            s = s + "    and    (length(ltrim(cres.DiasColeta)) > 0)  \n";
            s = s + "    and    (c.Inativo <> 1 Or c.Inativo Is Null)  \n";
            s = s + "    and    (cr.DataRecisao = '100-1-1' OR cr.DataRecisao = '0001-01-01' OR cr.DataRecisao = '0100-01-01' OR isnull(cr.DataRecisao)) \n";
            s = s + "    and    not exists (select * from Reajustes where CodigoContrato = cr.Codigo) ";
            s = s + ") x \n";
            s = s + "where  (select Data from Reajustes  \n";
            s = s + "        where CodigoContrato = x.CodigoContrato \n";
            s = s + "        order by Data Desc limit 1) = x.Data \n";
            s = s + "or     x.Data = '' \n";
            s = s + "order by CodigoContrato \n";
            l_ds = new DataSet();
            FillDataSet();
            DesconectaBanco();
            int iCountHora = 0;
            string[] _DiasColetas;

            foreach (DataRow _drSemanal in l_ds.Tables[0].Rows)
            {
                _DiasColetas = _drSemanal["DiasColeta"].ToString().Split(","[0]);
                int _diaDaSemana = (int)Convert.ToDateTime(pDataFutura).DayOfWeek + 1; // por que na contagem do c# começa com 0
                foreach (string strDia in _DiasColetas)
                {
                    if (strDia.Replace(" ", "") == _diaDaSemana.ToString())
                    {
                        oProgramacaoDiaria.AnoMesDia = Convert.ToInt32(_anomesdia);
                        oProgramacaoDiaria.Data = Convert.ToDateTime(pDataFutura).ToString("yyyy-MM-dd");
                        oProgramacaoDiaria.DataProgramada = Convert.ToDateTime(pDataFutura).ToString("yyyy-MM-dd");
                        oProgramacaoDiaria.Hora = DateTime.Now.TimeOfDay.TotalMilliseconds.ToString().Replace(",", "");
                        iCountHora++;
                        oProgramacaoDiaria.Hora = (Convert.ToInt64(oProgramacaoDiaria.Hora) + iCountHora).ToString();
                        oProgramacaoDiaria.Hora = oProgramacaoDiaria.Hora.Substring(oProgramacaoDiaria.Hora.Length - 8);
                        oProgramacaoDiaria.Solicitante = "";
                        oProgramacaoDiaria.CodigoCliente = Convert.ToInt32(_drSemanal["CodigoCliente"]);
                        oProgramacaoDiaria.NomeCliente = _drSemanal["NomeCliente"].ToString();
                        oProgramacaoDiaria.StatusCor = "-2147483647";
                        if (_drSemanal["CodigoResiduo"].ToString() != "")
                            oProgramacaoDiaria.CodigoResiduo = Convert.ToInt32(_drSemanal["CodigoResiduo"]);
                        oProgramacaoDiaria.ExecutarServico = _drSemanal["DescricaoResiduo"].ToString();
                        oProgramacaoDiaria.CodigoCaminhao = 0;
                        oProgramacaoDiaria.ModeloCaminhao = "";
                        oProgramacaoDiaria.CodigoMotorista = 0;
                        oProgramacaoDiaria.NomeMotorista = "";
                        oProgramacaoDiaria.Observacao = "";
                        oProgramacaoDiaria.DestinoFinal = "";
                        oProgramacaoDiaria.RotaMapa = "";

                        oProgramacaoDiaria.ServicoExecutado = "";
                        oProgramacaoDiaria.SequencialParaQuadro1 = 0;
                        oProgramacaoDiaria.Quantidade = 0;
                        if (_drSemanal["QuantidadeFranquia"].ToString() != "")
                            oProgramacaoDiaria.Quantidade = Convert.ToInt32(_drSemanal["QuantidadeFranquia"]);
                        oProgramacaoDiaria.Unidade = _drSemanal["Unidade"].ToString();
                        oProgramacaoDiaria.Quadro = 2;
                        oProgramacaoDiaria.TipoProgramacao = 1;
                        oProgramacaoDiaria.Quantidade2 = 0;
                        oProgramacaoDiaria.Unidade2 = "";

                        ServicosFutura(oProgramacaoDiaria, oProgramacaoDiaria.CodigoCliente, oProgramacaoDiaria.DataProgramada, pEncerramento);

                        //sExisteBloqueioFinanceiro = oBloqFinanceiro.ExisteBloqueio(oReprServs.CodigoCliente)
                        //If sExisteBloqueioFinanceiro > "" Then
                        //    oProgServsNova.Observacao = "BLOQUEIO FINANCEIRO DESDE " + sExisteBloqueioFinanceiro
                        //End If

                        if (!ExisteAntecipacao(drArrayAntecipacaoReprogr, oProgramacaoDiaria))
                        {
                            /*
                            // adiciona programacao semanal
                            if (pEncerramento)
                                oProgramacaoDados.Inserir(oProgramacaoDiaria);
                            else
                                PrencheDTFutura(oProgramacaoDiaria, dtFutura);
                            */
                            if (oProgramacaoDiaria.CodigoCliente == 2447)
                                oProgramacaoDiaria.CodigoCliente = 2447;
                            existeReprogramacaoFutura = oProgramacaoDados.ExisteReprogramacaoFutura(oProgramacaoDiaria.CodigoCliente, oProgramacaoDiaria.ExecutarServico, 
                                                                                                    pDataFutura, oProgramacaoDiaria.AnoMesDia.ToString(), "");
                            if (!existeReprogramacaoFutura)
                            {
                                // adiciona programacao semanal
                                if (pEncerramento)
                                    oProgramacaoDados.Inserir(oProgramacaoDiaria);
                                else
                                    PrencheDTFutura(oProgramacaoDiaria, dtFutura);
                            }
                               
                        }
                        //' Grava status da programação atual, caso a última programação
                        //GravaStatusProgramacaoAtual oReprServs.StatusCor, oProgServsNova.sequencial
                    }
                }

            }

            // Abrir lista de Contrato Clientes / dias de coleta.
            // MENSAL e QUINZENA
            ConectaBanco();
            s = "";
            s = s + "select * from \n";
            s = s + "( \n";
            s = s + "    select distinct cr.CodigoCliente, res.DescricaoReduzida as DescricaoResiduo, c.NomeFantasia as NomeCliente, res.Unidade, \n";
            s = s + "                    cres.DiasColeta, r.CodigoContrato, r.Data, ucase(cres.FrequenciaColeta) as FrequenciaColeta, \n";
            s = s + "                    cres.QuantidadeFranquia, cres.CodigoResiduo \n";
            s = s + "    from   Reajustes r \n";
            s = s + "    inner  join Contratos cr on cr.Codigo = r.CodigoContrato \n";
            s = s + "    inner  join ContratoResiduos cres on cres.CodigoContrato = cr.Codigo \n";
            s = s + "    inner  join Residuos res on res.Codigo = cres.CodigoResiduo \n";
            s = s + "    inner  join Clientes c on c.Codigo = cr.CodigoCliente \n";
            s = s + "    where  ucase(cres.Roteiro) = 'MENSAL' \n";
            s = s + "    and    (locate('MENSAL', ucase(cres.FrequenciaColeta)) or \n";
            s = s + "            locate('DEMANDA', ucase(cres.FrequenciaColeta)) or \n";
            s = s + "            locate('QUINZE', ucase(cres.FrequenciaColeta)))  \n";
            // Se for domingo dia 1º passar para o dia 2 segunda feira
            if (Convert.ToDateTime("01/" + Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                                    Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday" &&
                                    Convert.ToDateTime(pDataFutura).Day == 2)
            {
                s = s + "    and    locate(1, cres.DiasColeta) \n";
            }
            else if (Convert.ToDateTime(pDataFutura).AddDays(1).DayOfWeek.ToString() == "Sunday" &&
                                        Convert.ToDateTime(pDataFutura).Day > 1)
            {
                if (Convert.ToDateTime(pDataFutura).Month == 2 && Convert.ToDateTime(pDataFutura).Day >= 27)
                {
                    s = s + "    and (locate(27, cres.DiasColeta) or locate(28, cres.DiasColeta) or locate(29, cres.DiasColeta) or locate(30, cres.DiasColeta) or locate(31, cres.DiasColeta)) \n";
                }
                else
                {
                    s = s + "    and (locate(" + (Convert.ToDateTime(pDataFutura).Day).ToString() + ", cres.DiasColeta) or locate(" + (Convert.ToDateTime(pDataFutura).Day + 1).ToString() + ", cres.DiasColeta)) \n";
                }
            }
            else if (Convert.ToDateTime(pDataFutura).Month == 2)
            {
                if (Convert.ToDateTime(pDataFutura).Month == 2 && Convert.ToDateTime(pDataFutura).Day >= 27)
                {
                    s = s + "    and (locate(27, cres.DiasColeta) or locate(28, cres.DiasColeta) or locate(29, cres.DiasColeta) or locate(30, cres.DiasColeta) or locate(31, cres.DiasColeta)) \n";
                }
                else
                {
                    s = s + "    and (locate(" + (Convert.ToDateTime(pDataFutura).Day).ToString() + ", cres.DiasColeta) or locate(" + (Convert.ToDateTime(pDataFutura).Day + 1).ToString() + ", cres.DiasColeta)) \n";
                }
            }
            else if ((Convert.ToDateTime(pDataFutura).Month == 4 || Convert.ToDateTime(pDataFutura).Month == 6 || 
                      Convert.ToDateTime(pDataFutura).Month == 9 || Convert.ToDateTime(pDataFutura).Month == 11))
            {

                if (Convert.ToDateTime(pDataFutura).Day + 1 < 31 && Convert.ToDateTime((Convert.ToDateTime(pDataFutura).Day + 1).ToString() + "/" +
                    Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                    Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday" &&
                    Convert.ToDateTime(pDataFutura).Day > 1)
                {
                    s = s + "    and    (locate(" + (Convert.ToDateTime(pDataFutura).Day).ToString() + ", cres.DiasColeta) \n";
                    s = s + "            or locate(" + (Convert.ToDateTime(pDataFutura).Day + 1).ToString() + ", cres.DiasColeta)) \n";
                }
                else
                {
                    s = s + "    and    (locate(" + (Convert.ToDateTime(pDataFutura).Day).ToString() + ", cres.DiasColeta) \n";
                    s = s + "            or locate(" + (Convert.ToDateTime(pDataFutura).Day + 1).ToString() + ", cres.DiasColeta)) \n";
                }
            }
            else if (Convert.ToDateTime(pDataFutura).Day < 31 && Convert.ToDateTime((Convert.ToDateTime(pDataFutura).Day + 1).ToString() + "/" +
                        Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                        Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday" &&
                        Convert.ToDateTime(pDataFutura).Day > 1 )
            {
                s = s + "    and    (locate(" + (Convert.ToDateTime(pDataFutura).Day).ToString() + ", cres.DiasColeta) \n";
                s = s + "            or locate(" + (Convert.ToDateTime(pDataFutura).Day + 1).ToString() + ", cres.DiasColeta)) \n";
            }
            else
            {
                if (geral.Left(pDataFutura, 1) == "0")
                    s = s + "    and    locate(" + geral.Left(pDataFutura.Substring(1), 1) + ", cres.DiasColeta) \n";
                else
                    s = s + "    and    locate(" + geral.Left(pDataFutura, 2) + ", cres.DiasColeta) \n";
            }
            s = s + "    and    r.CodigoContrato = cr.Codigo \n";
            s = s + "    and    (length(ltrim(cres.DiasColeta)) > 0)  \n";
            s = s + "    and    (c.Inativo <> 1 Or c.Inativo Is Null)  \n";
            s = s + "    and    cres.DataReajuste = r.Data \n";
            s = s + "    and    (cr.DataRecisao = '100-1-1' OR cr.DataRecisao = '0001-01-01' OR cr.DataRecisao = '0100-01-01' OR isnull(cr.DataRecisao)) \n";
            s = s + "union all \n";
            s = s + "    select distinct cr.CodigoCliente, res.DescricaoReduzida as DescricaoResiduo, c.NomeFantasia as NomeCliente, res.Unidade, \n";
            s = s + "                    cres.DiasColeta, 0 as CodigoContrato, '' as Data, ucase(cres.FrequenciaColeta) as FrequenciaColeta, ";
            s = s + "                    cres.QuantidadeFranquia, cres.CodigoResiduo \n";
            s = s + "    from   Contratos cr \n";
            s = s + "    inner  join ContratoResiduos cres on cres.CodigoContrato = cr.Codigo \n";
            s = s + "    inner  join Residuos res on res.Codigo = cres.CodigoResiduo \n";
            s = s + "    inner  join Clientes c on c.Codigo = cr.CodigoCliente \n";
            s = s + "    where  ucase(cres.Roteiro) = 'MENSAL' \n";
            s = s + "    and    (locate('MENSAL', ucase(cres.FrequenciaColeta)) or \n";
            s = s + "            locate('DEMANDA', ucase(cres.FrequenciaColeta)) or \n";
            s = s + "            locate('QUINZE', ucase(cres.FrequenciaColeta)))  \n";
            // Se for domingo dia 1º passar para o dia 2 segunda feira
            if (Convert.ToDateTime("01/" + Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                                    Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday" &&
                                    Convert.ToDateTime(pDataFutura).Day == 2)
            {
                s = s + "    and    locate(1, cres.DiasColeta) \n";
            }
            else if (Convert.ToDateTime(pDataFutura).Month == 2)
            {
                if (Convert.ToDateTime(pDataFutura).Month == 2 && Convert.ToDateTime(pDataFutura).Day >= 27)
                {
                    s = s + "    and (locate(27, cres.DiasColeta) or locate(28, cres.DiasColeta) or locate(29, cres.DiasColeta) or locate(30, cres.DiasColeta) or locate(31, cres.DiasColeta)) \n";
                }
                else
                {
                    s = s + "    and (locate(" + (Convert.ToDateTime(pDataFutura).Day).ToString() + ", cres.DiasColeta) or locate(" + (Convert.ToDateTime(pDataFutura).Day + 1).ToString() + ", cres.DiasColeta)) \n";
                }
            }
            else if ((Convert.ToDateTime(pDataFutura).Month == 4 || Convert.ToDateTime(pDataFutura).Month == 6 ||
                      Convert.ToDateTime(pDataFutura).Month == 9 || Convert.ToDateTime(pDataFutura).Month == 11))
            {

                if (Convert.ToDateTime(pDataFutura).Day + 1 < 31 && Convert.ToDateTime((Convert.ToDateTime(pDataFutura).Day + 1).ToString() + "/" +
                    Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                    Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday" &&
                    Convert.ToDateTime(pDataFutura).Day > 1)
                {
                    s = s + "    and    (locate(" + (Convert.ToDateTime(pDataFutura).Day).ToString() + ", cres.DiasColeta) \n";
                    s = s + "            or locate(" + (Convert.ToDateTime(pDataFutura).Day + 1).ToString() + ", cres.DiasColeta)) \n";
                }
                else
                {
                    s = s + "    and    (locate(" + (Convert.ToDateTime(pDataFutura).Day).ToString() + ", cres.DiasColeta) \n";
                    s = s + "            or locate(" + (Convert.ToDateTime(pDataFutura).Day + 1).ToString() + ", cres.DiasColeta)) \n";
                }
            }
            else if (Convert.ToDateTime(pDataFutura).Day < 31 && Convert.ToDateTime((Convert.ToDateTime(pDataFutura).Day + 1).ToString() + "/" +
                        Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                        Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday" &&
                        Convert.ToDateTime(pDataFutura).Day > 1)
            {
                if (Convert.ToDateTime(pDataFutura).Month == 2 && Convert.ToDateTime(pDataFutura).Day >= 27)
                {
                    s = s + "    and (locate(27, cres.DiasColeta) or locate(28, cres.DiasColeta) or locate(29, cres.DiasColeta) or locate(30, cres.DiasColeta) or locate(31, cres.DiasColeta)) \n";
                }
                else
                {
                    s = s + "    and (locate(" + (Convert.ToDateTime(pDataFutura).Day).ToString() + ", cres.DiasColeta) or locate(" + (Convert.ToDateTime(pDataFutura).Day + 1).ToString() + ", cres.DiasColeta)) \n";
                }
            }
            else
            {
                if (geral.Left(pDataFutura, 1) == "0")
                    s = s + "    and    locate(" + geral.Left(pDataFutura.Substring(1), 1) + ", cres.DiasColeta) \n";
                else
                    s = s + "    and    locate(" + geral.Left(pDataFutura, 2) + ", cres.DiasColeta) \n";
            }
            s = s + "    and    (length(ltrim(cres.DiasColeta)) > 0)  \n";
            s = s + "    and    (c.Inativo <> 1 Or c.Inativo Is Null)  \n";
            s = s + "    and    (cr.DataRecisao = '100-1-1' OR cr.DataRecisao = '0001-01-01' OR cr.DataRecisao = '0100-01-01' OR isnull(cr.DataRecisao)) \n";
            s = s + "    and    not exists (select * from Reajustes where CodigoContrato = cr.Codigo) ";
            s = s + ") x \n";
            s = s + "where  (select Data from Reajustes  \n";
            s = s + "        where CodigoContrato = x.CodigoContrato \n";
            s = s + "        order by Data Desc limit 1) = x.Data \n";
            s = s + "or      x.Data = '' \n";
            s = s + "order by CodigoContrato \n";
            l_ds = new DataSet();
            FillDataSet();
            DesconectaBanco();
            iCountHora = 0;

            // MENSAL e QUINZENA
            foreach (DataRow _drMensal in l_ds.Tables[0].Rows)
            {
                if (Convert.ToInt32(_drMensal["CodigoCliente"]) == 1499)
                    oProgramacaoDiaria.CodigoCliente = 1499;

                if (Convert.ToInt32(_drMensal["CodigoCliente"]) == 2568)
                    oProgramacaoDiaria.CodigoCliente = 2568;

                _DiasColetas = _drMensal["DiasColeta"].ToString().Split(","[0]);

                for (int i = 0; i < _DiasColetas.Length;i++)
                {
                    if (Convert.ToDateTime(pDataFutura).Month == 2 && Convert.ToDateTime(pDataFutura).Day >= 27)
                    {
                        if (Convert.ToDateTime(ultimaDataFev).DayOfWeek.ToString() == "Sunday")
                        {
                            _DiasColetas[i] = _DiasColetas[i].Replace("28", (Convert.ToDateTime(ultimaDataFev).Day - 1).ToString());
                            _DiasColetas[i] = _DiasColetas[i].Replace("29", (Convert.ToDateTime(ultimaDataFev).Day - 1).ToString());
                            _DiasColetas[i] = _DiasColetas[i].Replace("30", (Convert.ToDateTime(ultimaDataFev).Day - 1).ToString());
                            _DiasColetas[i] = _DiasColetas[i].Replace("31", (Convert.ToDateTime(ultimaDataFev).Day - 1).ToString());
                        }
                        else
                        {
                            _DiasColetas[i] = _DiasColetas[i].Replace("28", (Convert.ToDateTime(ultimaDataFev).Day).ToString());
                            _DiasColetas[i] = _DiasColetas[i].Replace("29", (Convert.ToDateTime(ultimaDataFev).Day).ToString());
                            _DiasColetas[i] = _DiasColetas[i].Replace("30", (Convert.ToDateTime(ultimaDataFev).Day).ToString());
                            _DiasColetas[i] = _DiasColetas[i].Replace("31", (Convert.ToDateTime(ultimaDataFev).Day).ToString());
                        }
                    }
                }
                int _diaDoMes = Convert.ToInt32(Convert.ToDateTime(pDataFutura).ToString("dd"));
                foreach (string strDia in _DiasColetas)
                {
                    if (Convert.ToInt32(_drMensal["CodigoCliente"]) == 1791)
                        oProgramacaoDiaria.CodigoCliente = Convert.ToInt32(_drMensal["CodigoCliente"]);

                    // Se for domingo dia 1º passar para o dia 2 segunda feira
                    if (Convert.ToDateTime("01/" + Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                                            Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday" &&
                                            Convert.ToDateTime(pDataFutura).Day == 2)
                    {
                        _diaDoMes = 1;
                    }
                    else if (Convert.ToDateTime(pDataFutura).Month == 2 && Convert.ToDateTime(pDataFutura).Day >= 27)
                    {
                        if (strDia.IndexOf("28") > -1 && Convert.ToDateTime(pDataFutura).Day >= 27)
                            _diaDoMes = 28;
                        if (strDia.IndexOf("29") > -1 && Convert.ToDateTime(pDataFutura).Day >= 27)
                            _diaDoMes = 29;
                        if (strDia.IndexOf("30") > -1 && Convert.ToDateTime(pDataFutura).Day >= 27)
                            _diaDoMes = 30;
                        if (strDia.IndexOf("31") > -1 && Convert.ToDateTime(pDataFutura).Day >= 27)
                            _diaDoMes = 31;
                        if (Convert.ToDateTime(pDataFutura).Month == 2 && 
                           (29 == Convert.ToInt16(strDia.Replace(" ", "")) || 30 == Convert.ToInt16(strDia.Replace(" ", "")) || 31 == Convert.ToInt16(strDia.Replace(" ", ""))))
                        {
                            if (Convert.ToDateTime(ultimaDataFev).DayOfWeek.ToString() == "Sunday")
                            {
                                _diaDoMes = 27;
                            }
                        }
                    }
                    else if (Convert.ToDateTime((Convert.ToDateTime(pDataFutura).Day).ToString() + "/" +
                                                 Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                                                 Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday" &&
                                                 Convert.ToDateTime(pDataFutura).Day > 1)
                    {
                        if (Convert.ToDateTime(pDataFutura).Month == 2 && Convert.ToInt16(strDia.Replace(" ", "")) >= Convert.ToDateTime(ultimaDataFev).Day)
                        {
                            _diaDoMes = 99;
                        }
                        else
                        {
                            if (Convert.ToDateTime((Convert.ToInt16(strDia.Replace(" ", ""))).ToString() + "/" +
                                                    Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                                                    Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday")
                            {
                                _diaDoMes = 99;
                            }
                        }
                    }
                    else if ((Convert.ToDateTime(pDataFutura).Month == 1 || Convert.ToDateTime(pDataFutura).Month == 3 ||
                              Convert.ToDateTime(pDataFutura).Month == 5 || Convert.ToDateTime(pDataFutura).Month == 7 ||
                              Convert.ToDateTime(pDataFutura).Month == 8 || Convert.ToDateTime(pDataFutura).Month == 10 || 
                              Convert.ToDateTime(pDataFutura).Month == 12))
                    {
                        if (Convert.ToDateTime(pDataFutura).Day + 1 < 32 && Convert.ToDateTime((Convert.ToDateTime(pDataFutura).Day + 1).ToString() + "/" +
                            Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                            Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday" &&
                            Convert.ToDateTime(pDataFutura).Day > 1)
                        {
                            if (Convert.ToInt32(strDia.Trim()) == Convert.ToDateTime(pDataFutura).Day + 1)
                                _diaDoMes = Convert.ToDateTime(pDataFutura).Day + 1;
                            else
                                _diaDoMes = Convert.ToDateTime(pDataFutura).Day;
                        }
                        else if (Convert.ToDateTime(pDataFutura).Day < 32 && Convert.ToDateTime((Convert.ToDateTime(pDataFutura).Day).ToString() + "/" +
                                 Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                                 Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() != "Sunday" &&
                            _diaDoMes == Convert.ToDateTime(pDataFutura).Day)
                        {
                            _diaDoMes = Convert.ToDateTime(pDataFutura).Day;
                        }
                        else if (Convert.ToDateTime((Convert.ToDateTime(pDataFutura).Day).ToString() + "/" +
                            Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                            Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday" &&
                            Convert.ToDateTime(pDataFutura).Day == 1)
                        {
                            _diaDoMes = Convert.ToDateTime(pDataFutura).Day + 1;
                        }
                    }
                    else if ((Convert.ToDateTime(pDataFutura).Month == 4 || Convert.ToDateTime(pDataFutura).Month == 6 ||
                              Convert.ToDateTime(pDataFutura).Month == 9 || Convert.ToDateTime(pDataFutura).Month == 11))
                    {
                        if (Convert.ToDateTime(pDataFutura).Day + 1 < 31 && Convert.ToDateTime((Convert.ToDateTime(pDataFutura).Day + 1).ToString() + "/" +
                            Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                            Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday" &&
                            Convert.ToDateTime(pDataFutura).Day > 1)
                        {
                            if (Convert.ToInt32(strDia.Trim()) == Convert.ToDateTime(pDataFutura).Day + 1)
                                _diaDoMes = Convert.ToDateTime(pDataFutura).Day + 1;
                            else
                                _diaDoMes = Convert.ToDateTime(pDataFutura).Day;
                        }
                        else if (Convert.ToDateTime(pDataFutura).Day < 31 && Convert.ToDateTime((Convert.ToDateTime(pDataFutura).Day).ToString() + "/" +
                                 Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                                 Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() != "Sunday" &&
                                 _diaDoMes == Convert.ToDateTime(pDataFutura).Day)
                        {
                            _diaDoMes = Convert.ToDateTime(pDataFutura).Day;
                        }
                        else if (Convert.ToDateTime((Convert.ToDateTime(pDataFutura).Day + 1).ToString() + "/" +
                                 Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                                 Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday" &&
                                 Convert.ToDateTime(pDataFutura).Day == 1)
                        {
                            _diaDoMes = Convert.ToDateTime(pDataFutura).Day + 1;
                        }
                    }
                    else if (Convert.ToDateTime((Convert.ToDateTime(pDataFutura).Day + 1).ToString() + "/" +
                                                 Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                                                 Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday" &&
                                                 Convert.ToDateTime(pDataFutura).Day > 1)
                    {
                        if (Convert.ToDateTime(pDataFutura).Month == 2 && Convert.ToInt16(strDia.Replace(" ", "")) <= Convert.ToDateTime(ultimaDataFev).Day)
                        {
                            _diaDoMes = Convert.ToDateTime(pDataFutura).Day + 1;
                        }
                        else if (Convert.ToDateTime(pDataFutura).Month != 2 && Convert.ToDateTime(pDataFutura).Day >= 29)
                            _diaDoMes = Convert.ToDateTime(pDataFutura).Day + 1;
                    }
                    else if (_diaDoMes == 99)
                    {
                        // nao faz nada
                    }
                    else if (Convert.ToDateTime(_diaDoMes.ToString() + "/" + Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                                Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday" &&
                                Convert.ToDateTime(pDataFutura).Day == 1)
                    {
                        _diaDoMes = 99;
                    }

                    if ((Convert.ToInt16(_diaDoMes.ToString().Replace(" ", "")) == Convert.ToInt16(strDia.Replace(" ", "")) ||
                        (Convert.ToDateTime(pDataFutura).Month == 2 && Convert.ToDateTime(pDataFutura).Day >= 27 && 
                            (29 == Convert.ToInt16(strDia.Replace(" ", "")) || 
                            30 == Convert.ToInt16(strDia.Replace(" ", "")) ||
                            31 == Convert.ToInt16(strDia.Replace(" ", "")))))) // mensal
                    {
                        oProgramacaoDiaria.AnoMesDia = Convert.ToInt32(_anomesdia);
                        oProgramacaoDiaria.Data = Convert.ToDateTime(pDataFutura).ToString("yyyy-MM-dd");
                        oProgramacaoDiaria.DataProgramada = Convert.ToDateTime(pDataFutura).ToString("yyyy-MM-dd");
                        oProgramacaoDiaria.Hora = DateTime.Now.TimeOfDay.TotalMilliseconds.ToString().Replace(",", "");
                        iCountHora++;
                        oProgramacaoDiaria.Hora = (Convert.ToInt64(oProgramacaoDiaria.Hora) + iCountHora).ToString();
                        oProgramacaoDiaria.Hora = oProgramacaoDiaria.Hora.Substring(oProgramacaoDiaria.Hora.Length - 8);
                        oProgramacaoDiaria.Solicitante = "";
                        oProgramacaoDiaria.CodigoCliente = Convert.ToInt32(_drMensal["CodigoCliente"]);
                        oProgramacaoDiaria.NomeCliente = _drMensal["NomeCliente"].ToString();
                        oProgramacaoDiaria.StatusCor = "-2147483647";
                        if (_drMensal["CodigoResiduo"].ToString() != "")
                            oProgramacaoDiaria.CodigoResiduo = Convert.ToInt32(_drMensal["CodigoResiduo"]);
                        oProgramacaoDiaria.ExecutarServico = _drMensal["DescricaoResiduo"].ToString();
                        oProgramacaoDiaria.CodigoCaminhao = 0;
                        oProgramacaoDiaria.ModeloCaminhao = "";
                        oProgramacaoDiaria.CodigoMotorista = 0;
                        oProgramacaoDiaria.NomeMotorista = "";
                        oProgramacaoDiaria.Observacao = "";
                        oProgramacaoDiaria.DestinoFinal = "";
                        oProgramacaoDiaria.RotaMapa = "";

                        oProgramacaoDiaria.ServicoExecutado = "";
                        oProgramacaoDiaria.SequencialParaQuadro1 = 0;
                        oProgramacaoDiaria.Quantidade = 0;
                        if (_drMensal["QuantidadeFranquia"].ToString() != "")
                            oProgramacaoDiaria.Quantidade = Convert.ToInt32(_drMensal["QuantidadeFranquia"]);
                        oProgramacaoDiaria.Unidade = _drMensal["Unidade"].ToString();
                        oProgramacaoDiaria.Quadro = 2;
                        oProgramacaoDiaria.TipoProgramacao = 1;
                        oProgramacaoDiaria.Quantidade2 = 0;
                        oProgramacaoDiaria.Unidade2 = "";

                        ServicosFutura(oProgramacaoDiaria, oProgramacaoDiaria.CodigoCliente, oProgramacaoDiaria.DataProgramada, pEncerramento);

                        //sExisteBloqueioFinanceiro = oBloqFinanceiro.ExisteBloqueio(oReprServs.CodigoCliente)
                        //If sExisteBloqueioFinanceiro > "" Then
                        //    oProgServsNova.Observacao = "BLOQUEIO FINANCEIRO DESDE " + sExisteBloqueioFinanceiro
                        //End If

                        if (!ExisteAntecipacao(drArrayAntecipacaoReprogr, oProgramacaoDiaria))
                        {
                            /*
                            // adiciona programação mensal
                            if (pEncerramento)
                                oProgramacaoDados.Inserir(oProgramacaoDiaria);
                            else
                                PrencheDTFutura(oProgramacaoDiaria, dtFutura);
                            */
                            if (oProgramacaoDiaria.CodigoCliente == 2708)
                                oProgramacaoDiaria.CodigoCliente = 2708;
                            existeReprogramacaoFutura = oProgramacaoDados.ExisteReprogramacaoFutura(oProgramacaoDiaria.CodigoCliente, 
                                                                                                    oProgramacaoDiaria.ExecutarServico, pDataFutura, 
                                                                                                    oProgramacaoDiaria.AnoMesDia.ToString());
                            if (!existeReprogramacaoFutura)
                            {
                                // adiciona programação mensal
                                if (pEncerramento)
                                    oProgramacaoDados.Inserir(oProgramacaoDiaria);
                                else
                                    PrencheDTFutura(oProgramacaoDiaria, dtFutura);
                            }                                
                        }
                        //sSqlIns = sSqlIns + oProgramacaoDados.Inserir(oProgramacaoDiaria, true);

                        //' Grava status da programação atual, caso a última programação
                        //GravaStatusProgramacaoAtual oReprServs.StatusCor, oProgServsNova.sequencial
                    }
                }

            }
            // Abrir lista de Contrato Clientes / dias de coleta.
            // BIMESTRAL, TRIMESTRAL, QUADRIMESTRAL, SEMESTRAL, ANUAL
            ConectaBanco();
            s = "";
            s = s + "select * from \n";
            s = s + "( \n";
            s = s + "    select distinct cr.CodigoCliente, res.DescricaoReduzida as DescricaoResiduo, c.NomeFantasia as NomeCliente, res.Unidade, \n";
            s = s + "                    cres.DiasColeta, r.CodigoContrato, r.Data, ucase(cres.FrequenciaColeta) as FrequenciaColeta, \n";
            s = s + "                    cres.MesAnoBase, cres.QuantidadeFranquia, cres.CodigoResiduo \n";
            s = s + "    from   Reajustes r \n";
            s = s + "    inner  join Contratos cr on cr.Codigo = r.CodigoContrato \n";
            s = s + "    inner  join ContratoResiduos cres on cres.CodigoContrato = cr.Codigo \n";
            s = s + "    inner  join Residuos res on res.Codigo = cres.CodigoResiduo \n";
            s = s + "    inner  join Clientes c on c.Codigo = cr.CodigoCliente \n";
            s = s + "    where  ucase(cres.Roteiro) = 'MENSAL' \n";
            s = s + "    and    (locate('BIMESTR', cres.FrequenciaColeta) or locate('TRIMESTR', cres.FrequenciaColeta) or \n";
            s = s + "            locate('QUADRIMESTR', cres.FrequenciaColeta) or locate('SEMESTR', cres.FrequenciaColeta) or \n";
            s = s + "            locate('ANUAL', cres.FrequenciaColeta)) \n";
            // Se for domingo dia 1º passar para o dia 2 segunda feira
            if (Convert.ToDateTime("01/" + Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                                    Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday" &&
                                    Convert.ToDateTime(pDataFutura).Day == 2)
            {
                s = s + "    and    locate(1, cres.DiasColeta) \n";
            }
            else if (Convert.ToDateTime(pDataFutura).Month == 2)
            {
                if (Convert.ToDateTime(pDataFutura).Month == 2 && Convert.ToDateTime(pDataFutura).Day >= 27)
                {
                    s = s + "    and (locate(27, cres.DiasColeta) or locate(28, cres.DiasColeta) or locate(29, cres.DiasColeta) or locate(30, cres.DiasColeta) or locate(31, cres.DiasColeta)) \n";
                }
                else
                {
                    s = s + "    and (locate(" + (Convert.ToDateTime(pDataFutura).Day).ToString() + ", cres.DiasColeta) or locate(" + (Convert.ToDateTime(pDataFutura).Day + 1).ToString() + ", cres.DiasColeta)) \n";
                }
            }
            else if ((Convert.ToDateTime(pDataFutura).Month == 4 || Convert.ToDateTime(pDataFutura).Month == 6 ||
                      Convert.ToDateTime(pDataFutura).Month == 9 || Convert.ToDateTime(pDataFutura).Month == 11))
            {

                if (Convert.ToDateTime(pDataFutura).Day + 1 < 31 && Convert.ToDateTime((Convert.ToDateTime(pDataFutura).Day + 1).ToString() + "/" +
                    Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                    Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday" &&
                    Convert.ToDateTime(pDataFutura).Day > 1)
                {
                    s = s + "    and    (locate(" + (Convert.ToDateTime(pDataFutura).Day).ToString() + ", cres.DiasColeta) \n";
                    s = s + "            or locate(" + (Convert.ToDateTime(pDataFutura).Day + 1).ToString() + ", cres.DiasColeta)) \n";
                }
                else
                {
                    s = s + "    and    (locate(" + (Convert.ToDateTime(pDataFutura).Day).ToString() + ", cres.DiasColeta) \n";
                    s = s + "            or locate(" + (Convert.ToDateTime(pDataFutura).Day + 1).ToString() + ", cres.DiasColeta)) \n";
                }
            }
            else if (Convert.ToDateTime(pDataFutura).Day < 31 && Convert.ToDateTime((Convert.ToDateTime(pDataFutura).Day + 1).ToString() + "/" +
                                                Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                                                Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday" &&
                                                Convert.ToDateTime(pDataFutura).Day > 1)
            {
                s = s + "    and    (locate(" + (Convert.ToDateTime(pDataFutura).Day).ToString() + ", cres.DiasColeta) \n";
                s = s + "           or locate(" + (Convert.ToDateTime(pDataFutura).Day + 1).ToString() + ", cres.DiasColeta)) \n";
            }
            else
            {
                if (geral.Left(pDataFutura, 1) == "0")
                    s = s + "    and    locate(" + geral.Left(pDataFutura.Substring(1), 1) + ", cres.DiasColeta) \n";
                else
                    s = s + "    and    locate(" + geral.Left(pDataFutura, 2) + ", cres.DiasColeta) \n";
            }
            s = s + "    and    (not cres.MesAnoBase is null or cres.MesAnoBase > '') \n";
            s = s + "    and    r.CodigoContrato = cr.Codigo \n";
            s = s + "    and    (length(ltrim(cres.DiasColeta)) > 0)  \n";
            s = s + "    and    (c.Inativo <> 1 Or c.Inativo Is Null)  \n";
            s = s + "    and    cres.DataReajuste = r.Data \n";
            s = s + "    and    (cr.DataRecisao = '100-1-1' OR cr.DataRecisao = '0001-01-01' OR cr.DataRecisao = '0100-01-01' OR isnull(cr.DataRecisao)) \n";
            s = s + "union all \n";
            s = s + "    select distinct cr.CodigoCliente, res.DescricaoReduzida as DescricaoResiduo, c.NomeFantasia as NomeCliente, res.Unidade, \n";
            s = s + "                    cres.DiasColeta, 0 as CodigoContrato, '' as Data, ucase(cres.FrequenciaColeta) as FrequenciaColeta, \n";
            s = s + "                    cres.MesAnoBase, cres.QuantidadeFranquia, cres.CodigoResiduo \n";
            s = s + "    from Contratos cr \n";
            s = s + "    inner  join ContratoResiduos cres on cres.CodigoContrato = cr.Codigo \n";
            s = s + "    inner  join Residuos res on res.Codigo = cres.CodigoResiduo \n";
            s = s + "    inner  join Clientes c on c.Codigo = cr.CodigoCliente \n";
            s = s + "    where  ucase(cres.Roteiro) = 'MENSAL' \n";
            s = s + "    and    (locate('BIMESTR', cres.FrequenciaColeta) or locate('TRIMESTR', cres.FrequenciaColeta) or \n";
            s = s + "            locate('QUADRIMESTR', cres.FrequenciaColeta) or locate('SEMESTR', cres.FrequenciaColeta) or \n";
            s = s + "            locate('ANUAL', cres.FrequenciaColeta)) \n";
            // Se for domingo dia 1º passar para o dia 2 segunda feira
            if (Convert.ToDateTime("01/" + Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                                    Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday" &&
                                    Convert.ToDateTime(pDataFutura).Day == 2)
            {
                s = s + "    and    locate(1, cres.DiasColeta) \n";
            }
            else if (Convert.ToDateTime(pDataFutura).Month == 2)
            {
                if (Convert.ToDateTime(pDataFutura).Month == 2 && Convert.ToDateTime(pDataFutura).Day >= 27)
                {
                    s = s + "    and (locate(27, cres.DiasColeta) or locate(28, cres.DiasColeta) or locate(29, cres.DiasColeta) or locate(30, cres.DiasColeta) or locate(31, cres.DiasColeta)) \n";
                }
                else
                {
                    s = s + "    and (locate(" + (Convert.ToDateTime(pDataFutura).Day).ToString() + ", cres.DiasColeta) or locate(" + (Convert.ToDateTime(pDataFutura).Day + 1).ToString() + ", cres.DiasColeta)) \n";
                }
            }
            else if ((Convert.ToDateTime(pDataFutura).Month == 4 || Convert.ToDateTime(pDataFutura).Month == 6 ||
                      Convert.ToDateTime(pDataFutura).Month == 9 || Convert.ToDateTime(pDataFutura).Month == 11))
            {

                if (Convert.ToDateTime(pDataFutura).Day + 1 < 31 && Convert.ToDateTime((Convert.ToDateTime(pDataFutura).Day + 1).ToString() + "/" +
                    Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                    Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday" &&
                    Convert.ToDateTime(pDataFutura).Day > 1)
                {
                    s = s + "    and    (locate(" + (Convert.ToDateTime(pDataFutura).Day).ToString() + ", cres.DiasColeta) \n";
                    s = s + "            or locate(" + (Convert.ToDateTime(pDataFutura).Day + 1).ToString() + ", cres.DiasColeta)) \n";
                }
                else
                {
                    s = s + "    and    (locate(" + (Convert.ToDateTime(pDataFutura).Day).ToString() + ", cres.DiasColeta) \n";
                    s = s + "            or locate(" + (Convert.ToDateTime(pDataFutura).Day + 1).ToString() + ", cres.DiasColeta)) \n";
                }
            }
            else if (Convert.ToDateTime(pDataFutura).Day < 31 && Convert.ToDateTime((Convert.ToDateTime(pDataFutura).Day + 1).ToString() + "/" +
                                            Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                                            Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday" &&
                                            Convert.ToDateTime(pDataFutura).Day > 1)
            {
                s = s + "    and    (locate(" + (Convert.ToDateTime(pDataFutura).Day).ToString() + ", cres.DiasColeta) \n";
                s = s + "           or locate(" + (Convert.ToDateTime(pDataFutura).Day + 1).ToString() + ", cres.DiasColeta)) \n";
            }
            else
            {
                if (geral.Left(pDataFutura, 1) == "0")
                    s = s + "    and    locate(" + geral.Left(pDataFutura.Substring(1), 1) + ", cres.DiasColeta) \n";
                else
                    s = s + "    and    locate(" + geral.Left(pDataFutura, 2) + ", cres.DiasColeta) \n";
            }
            s = s + "    and    (not cres.MesAnoBase is null or cres.MesAnoBase > '') \n";
            s = s + "    and    (length(ltrim(cres.DiasColeta)) > 0)  \n";
            s = s + "    and    (c.Inativo <> 1 Or c.Inativo Is Null)  \n";
            s = s + "    and    (cr.DataRecisao = '100-1-1' OR cr.DataRecisao = '0001-01-01' OR cr.DataRecisao = '0100-01-01' OR isnull(cr.DataRecisao)) \n";
            s = s + "    and    not exists (select * from Reajustes where CodigoContrato = cr.Codigo) ";
            s = s + ") x \n";
            s = s + "where  (select Data from Reajustes  \n";
            s = s + "        where CodigoContrato = x.CodigoContrato \n";
            s = s + "        order by Data Desc limit 1) = x.Data \n";
            s = s + "or      x.Data = '' \n";
            s = s + "order by CodigoContrato \n";
            l_ds = new DataSet();
            FillDataSet();
            DesconectaBanco();
            iCountHora = 0;
            foreach (DataRow _drMensal in l_ds.Tables[0].Rows)
            {
                if (Convert.ToInt32(_drMensal["CodigoCliente"]) == 1791)
                    oProgramacaoDiaria.CodigoCliente = 1791;

                _DiasColetas = _drMensal["DiasColeta"].ToString().Split(","[0]);
                int _diaDoMes = Convert.ToInt32(Convert.ToDateTime(pDataFutura).ToString("dd"));
                string[] _meses = new string[12];
                int _anoProgAberta = Convert.ToInt32(Convert.ToDateTime(pDataFutura).ToString("yy"));
                int j = 0;
                int _mesBase = 0;
                int _anoBase = 0;
                bool _mesValido = false;

                if (_drMensal["FrequenciaColeta"].ToString().IndexOf("BIMESTR") > 0 || _drMensal["FrequenciaColeta"].ToString().IndexOf("DEMANDA") > 0)
                {
                    _mesBase = Convert.ToInt16(_drMensal["MesAnoBase"].ToString().Substring(0, 2).Trim());
                    _anoBase = Convert.ToInt16(_drMensal["MesAnoBase"].ToString().Substring(3, 2).Trim());
                    if (_anoProgAberta > _anoBase)
                    {
                        if (_mesBase % 2 == 0)
                            _mesBase = 2;
                        else
                            _mesBase = 1;
                    }
                    for (int i = _mesBase; i <= 12; i = i + 2)
                    {
                        _meses[j] = i.ToString("00");
                        j++;
                    }
                    foreach (string _mesvalido in _meses)
                    {
                        if (_mesvalido == pDataFutura.Substring(3, 2))
                        {
                            _mesValido = true;
                            break;
                        }
                    }
                }
                if (_drMensal["FrequenciaColeta"].ToString().IndexOf("TRIMESTR") > 0 || _drMensal["FrequenciaColeta"].ToString().IndexOf("DEMANDA") > 0)
                {
                    _mesBase = Convert.ToInt16(_drMensal["MesAnoBase"].ToString().Substring(0, 2).Trim());
                    _anoBase = Convert.ToInt16(_drMensal["MesAnoBase"].ToString().Substring(3, 2).Trim());
                    if (_anoProgAberta > _anoBase)
                    {
                        if (_mesBase == 1)
                            _mesBase = 4;
                        if (_mesBase == 2)
                            _mesBase = 5;
                        if (_mesBase == 3)
                            _mesBase = 6;
                        if (_mesBase == 4)
                            _mesBase = 7;
                        if (_mesBase == 5)
                            _mesBase = 8;
                        if (_mesBase == 6)
                            _mesBase = 9;
                        if (_mesBase == 7)
                            _mesBase = 10;
                        if (_mesBase == 8)
                            _mesBase = 11;
                        if (_mesBase == 9)
                            _mesBase = 12;
                        if (_mesBase == 10)
                            _mesBase = 1;
                        if (_mesBase == 11)
                            _mesBase = 2;
                        if (_mesBase == 12)
                            _mesBase = 3;
                    }
                    for (int i = _mesBase; i <= 12; i = i + 3)
                    {
                        _meses[j] = i.ToString("00");
                        j++;
                    }
                    foreach (string _mesvalido in _meses)
                    {
                        if (_mesvalido == pDataFutura.Substring(3, 2))
                        {
                            _mesValido = true;
                            break;
                        }
                    }
                }
                if (_drMensal["FrequenciaColeta"].ToString().IndexOf("QUADRIMESTR") > 0 || _drMensal["FrequenciaColeta"].ToString().IndexOf("DEMANDA") > 0)
                {
                    _mesBase = Convert.ToInt16(_drMensal["MesAnoBase"].ToString().Substring(0, 2).Trim());
                    _anoBase = Convert.ToInt16(_drMensal["MesAnoBase"].ToString().Substring(3, 2).Trim());
                    if (_anoProgAberta > _anoBase)
                    {
                        if (_mesBase == 8)
                            _mesBase = 4;
                        if (_mesBase == 9 || _mesBase == 5)
                            _mesBase = 1;
                        if (_mesBase == 10 || _mesBase == 6)
                            _mesBase = 2;
                        if (_mesBase == 11 || _mesBase == 7)
                            _mesBase = 3;
                        if (_mesBase == 12)
                            _mesBase = 4;
                    }
                    for (int i = _mesBase; i <= 12; i = i + 4)
                    {
                        _meses[j] = i.ToString("00");
                        j++;
                    }
                    foreach (string _mesvalido in _meses)
                    {
                        if (_mesvalido == pDataFutura.Substring(3, 2))
                        {
                            _mesValido = true;
                            break;
                        }

                    }
                }
                if (_drMensal["FrequenciaColeta"].ToString().IndexOf("SEMESTR") > 0 || _drMensal["FrequenciaColeta"].ToString().IndexOf("DEMANDA") > 0)
                {
                    _mesBase = Convert.ToInt16(_drMensal["MesAnoBase"].ToString().Substring(0, 2).Trim());
                    _anoBase = Convert.ToInt16(_drMensal["MesAnoBase"].ToString().Substring(3, 2).Trim());
                    if (_anoProgAberta > _anoBase)
                    {
                        if (_mesBase == 7)
                            _mesBase = 1;
                        if (_mesBase == 8)
                            _mesBase = 2;
                        if (_mesBase == 9)
                            _mesBase = 3;
                        if (_mesBase == 10)
                            _mesBase = 4;
                        if (_mesBase == 11)
                            _mesBase = 5;
                        if (_mesBase == 12)
                            _mesBase = 6;
                    }
                    for (int i = _mesBase; i <= 12; i = i + 6)
                    {
                        _meses[j] = i.ToString("00");
                        j++;
                    }
                    foreach (string _mesvalido in _meses)
                    {
                        if (_mesvalido == pDataFutura.Substring(3, 2))
                        {
                            _mesValido = true;
                            break;
                        }

                    }
                }
                if (_drMensal["FrequenciaColeta"].ToString().IndexOf("ANUAL") > 0 || _drMensal["FrequenciaColeta"].ToString().IndexOf("DEMANDA") > 0)
                {
                    _mesBase = Convert.ToInt16(_drMensal["MesAnoBase"].ToString().Substring(0, 2).Trim());
                    _anoBase = Convert.ToInt16(_drMensal["MesAnoBase"].ToString().Substring(3, 2).Trim());

                    if (_mesBase == Convert.ToDateTime(pDataFutura).Month)
                    {
                        _mesValido = true;
                    }
                }

                foreach (string strDia in _DiasColetas)
                {
                    // Se for domingo dia 1º passar para o dia 2 segunda feira
                    if (Convert.ToDateTime("01/" + Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                                            Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday" &&
                                            Convert.ToDateTime(pDataFutura).Day == 2)
                    {
                        _diaDoMes = 1;
                    }
                    else if (Convert.ToDateTime(pDataFutura).Month == 2)
                    {
                        if (strDia.IndexOf("28") > -1 && Convert.ToDateTime(pDataFutura).Day >= 27)
                            _diaDoMes = 28;
                        if (strDia.IndexOf("29") > -1 && Convert.ToDateTime(pDataFutura).Day >= 27)
                            _diaDoMes = 29;
                        if (strDia.IndexOf("30") > -1 && Convert.ToDateTime(pDataFutura).Day >= 27)
                            _diaDoMes = 30;
                        if (strDia.IndexOf("31") > -1 && Convert.ToDateTime(pDataFutura).Day >= 27)
                            _diaDoMes = 31;
                        if (Convert.ToDateTime(pDataFutura).Month == 2 && Convert.ToDateTime(pDataFutura).Day >= 27 &&
                            (29 == Convert.ToInt16(strDia.Replace(" ", "")) ||
                                30 == Convert.ToInt16(strDia.Replace(" ", "")) ||
                                31 == Convert.ToInt16(strDia.Replace(" ", ""))))
                        {
                            ultimaDataFev = Convert.ToDateTime("01/03/" + Convert.ToDateTime(pDataFutura).Year.ToString()).AddDays(-1).ToString("");
                            if (Convert.ToDateTime(ultimaDataFev).DayOfWeek.ToString() == "Sunday")
                            {
                                _diaDoMes = 27;
                            }
                        }
                    }
                    else if (Convert.ToDateTime((Convert.ToDateTime(pDataFutura).Day + 1).ToString() + "/" +
                                                    Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                                                    Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday" &&
                                                    Convert.ToDateTime(pDataFutura).Day > 1)
                    {
                        if (Convert.ToDateTime((Convert.ToInt16(strDia.Replace(" ", ""))).ToString() + "/" +
                                                    Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                                                    Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday")
                        {
                            _diaDoMes = Convert.ToDateTime(pDataFutura).Day + 1;
                        }
                    }
                    else if (Convert.ToDateTime((Convert.ToDateTime(pDataFutura).Day).ToString() + "/" +
                                                 Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                                                 Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday" &&
                                                 Convert.ToDateTime(pDataFutura).Day > 1)
                    {
                        if (Convert.ToDateTime((Convert.ToInt16(strDia.Replace(" ", ""))).ToString() + "/" +
                                                    Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                                                    Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday")
                        {
                            _diaDoMes = 99;

                        }
                    }
                    else if (Convert.ToDateTime(_diaDoMes.ToString() + "/" + Convert.ToDateTime(pDataFutura).Month.ToString() + "/" +
                             Convert.ToDateTime(pDataFutura).Year.ToString()).DayOfWeek.ToString() == "Sunday" &&
                             Convert.ToDateTime(pDataFutura).Day == 1)
                    {
                        _diaDoMes = 99;
                    }

                    if (Convert.ToInt16(_diaDoMes.ToString().Replace(" ", "")) == Convert.ToInt16(strDia.Replace(" ", "")) && _mesValido)
                    {
                        oProgramacaoDiaria.AnoMesDia = Convert.ToInt32(_anomesdia);
                        oProgramacaoDiaria.Data = Convert.ToDateTime(pDataFutura).ToString("yyyy-MM-dd");
                        oProgramacaoDiaria.DataProgramada = Convert.ToDateTime(pDataFutura).ToString("yyyy-MM-dd");
                        oProgramacaoDiaria.Hora = DateTime.Now.TimeOfDay.TotalMilliseconds.ToString().Replace(",", "");
                        iCountHora++;
                        oProgramacaoDiaria.Hora = (Convert.ToInt64(oProgramacaoDiaria.Hora) + iCountHora).ToString();
                        oProgramacaoDiaria.Hora = oProgramacaoDiaria.Hora.Substring(oProgramacaoDiaria.Hora.Length - 8);
                        oProgramacaoDiaria.Solicitante = "";
                        oProgramacaoDiaria.CodigoCliente = Convert.ToInt32(_drMensal["CodigoCliente"]);

                        if (Convert.ToInt32(_drMensal["CodigoCliente"]) == 1826)
                            oProgramacaoDiaria.CodigoCliente = 1826;
                        oProgramacaoDiaria.NomeCliente = _drMensal["NomeCliente"].ToString();
                        oProgramacaoDiaria.StatusCor = "-2147483647";
                        if (_drMensal["CodigoResiduo"].ToString() != "")
                            oProgramacaoDiaria.CodigoResiduo = Convert.ToInt32(_drMensal["CodigoResiduo"]);
                        oProgramacaoDiaria.ExecutarServico = _drMensal["DescricaoResiduo"].ToString();
                        oProgramacaoDiaria.CodigoCaminhao = 0;
                        oProgramacaoDiaria.ModeloCaminhao = "";
                        oProgramacaoDiaria.CodigoMotorista = 0;
                        oProgramacaoDiaria.NomeMotorista = "";
                        oProgramacaoDiaria.Observacao = "";
                        oProgramacaoDiaria.DestinoFinal = "";
                        oProgramacaoDiaria.RotaMapa = "";

                        oProgramacaoDiaria.ServicoExecutado = "";
                        oProgramacaoDiaria.SequencialParaQuadro1 = 0;
                        oProgramacaoDiaria.Quantidade = 0;
                        if (_drMensal["QuantidadeFranquia"].ToString() != "")
                            oProgramacaoDiaria.Quantidade = Convert.ToInt32(_drMensal["QuantidadeFranquia"]);
                        oProgramacaoDiaria.Unidade = _drMensal["Unidade"].ToString();
                        oProgramacaoDiaria.Quadro = 2;
                        oProgramacaoDiaria.TipoProgramacao = 1;
                        oProgramacaoDiaria.Quantidade2 = 0;
                        oProgramacaoDiaria.Unidade2 = "";

                        ServicosFutura(oProgramacaoDiaria, oProgramacaoDiaria.CodigoCliente, oProgramacaoDiaria.DataProgramada, pEncerramento);

                        //sExisteBloqueioFinanceiro = oBloqFinanceiro.ExisteBloqueio(oReprServs.CodigoCliente)
                        //If sExisteBloqueioFinanceiro > "" Then
                        //    oProgServsNova.Observacao = "BLOQUEIO FINANCEIRO DESDE " + sExisteBloqueioFinanceiro
                        //End If

                        if (!ExisteAntecipacao(drArrayAntecipacaoReprogr, oProgramacaoDiaria))
                        {
                            /*
                            // adiciona programação bimestral, trimestral, semestral, anual
                            if (pEncerramento)
                            {
                                //oProgramacaoDiaria.Sequencial = 0;
                                oProgramacaoDados.Inserir(oProgramacaoDiaria);
                            }
                            else
                                PrencheDTFutura(oProgramacaoDiaria, dtFutura);

                            */
                            if (oProgramacaoDiaria.CodigoCliente == 2708)
                                oProgramacaoDiaria.CodigoCliente = 2708;
                            existeReprogramacaoFutura = oProgramacaoDados.ExisteReprogramacaoFutura(oProgramacaoDiaria.CodigoCliente, 
                                                                                                    oProgramacaoDiaria.ExecutarServico, pDataFutura,
                                                                                                    oProgramacaoDiaria.AnoMesDia.ToString());
                            if (!existeReprogramacaoFutura)
                            {
                                // adiciona programação bimestral, trimestral, semestral, anual
                                if (pEncerramento)
                                {
                                    //oProgramacaoDiaria.Sequencial = 0;
                                    oProgramacaoDados.Inserir(oProgramacaoDiaria);
                                }
                                else
                                    PrencheDTFutura(oProgramacaoDiaria, dtFutura);
                            }
                                
                        }
                        //oProgramacaoDados.Inserir(oProgramacaoDiaria);
                        //sSqlIns = sSqlIns + oProgramacaoDados.Inserir(oProgramacaoDiaria, true);
                        //' Grava status da programação atual, caso a última programação
                        //GravaStatusProgramacaoAtual oReprServs.StatusCor, oProgServsNova.sequencial
                    }
                }
            }
            foreach (DataRow dr in dtFutura.Rows)
            {
                if (dr["TipoProgramacao"].ToString() == "2")
                {
                    dr["TipoProgramacao"] = "M";
                }
                else
                {
                    dr["TipoProgramacao"] = "A";
                }
            }
            return dtFutura;
        }

        public string SqlAlter(string pSql)
        {
            string sRet = "";
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
                sRet = string.Empty;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                sRet = ex.ToString();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
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
    }
}