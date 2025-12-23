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
	public class clsReprogramacaoDados
	{
	    private clsDB oDB = new clsDB();
        private MySqlDataAdapter l_myData = new MySqlDataAdapter();
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
            l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
            l_myData.Fill(l_ds);
            l_dt = l_ds.Tables[0];
        }
        private void DesconectaBanco()
        {
            oDB.DesconectaMySql();
        }

        public DataTable PegaDados(clsReprogramacaoServicos pReprogramacao, int pSequencial, int pAnoMesDia, int pQuadro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select r.SequencialProgramacaoDiaria, r.AnoMesDia, r.StatusCor, r.Data, r.Hora, r.Solicitante, r.CodigoCliente, \n";
                s = s + "       c.Nome as NomeCliente, r.ExecutarServico, r.DataProgramada, r.HoraProgramada, r.Quantidade, \n";
                s = s + "       r.CodigoCaminhao, o.Modelo as ModeloCaminhao, r.CodigoMotorista, m.Nome as NomeMotorista, \n";
                s = s + "       r.Observacao, r.DestinoFinal, r.Unidade, r.TipoProgramacao, \n";
                s = s + "       r.Unidade2, r.MapaMarcado \n";
                s = s + "from   ReprogramacaoServicos r \n";
                s = s + "left   join Clientes c on c.Codigo = r.CodigoCliente \n";
                s = s + "left   join Caminhoes o on o.Codigo = r.CodigoCaminhao \n";
                s = s + "left   join Funcionarios m on m.Codigo = r.CodigoMotorista \n";
                s = s + "left   join Residuos res on res.Codigo = r.CodigoResiduo \n";

                if (pSequencial > 0)
                {
                    s = s + "where  Sequencial = " + pSequencial + " ";
                }
                else
                {
                    s = s + "where r.AnoMesDia = " + pAnoMesDia + " ";
                    s = s + "and   r.Quadro    = " + pQuadro + " ";
                }
                s = s + " order by r.DataProgramada desc ";
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows[0]["SequencialProgramacaoDiaria"].ToString() != "")
                        pReprogramacao.SequencialProgramacaoDiaria = Convert.ToInt32(l_dt.Rows[0]["SequencialProgramacaoDiaria"]);
                    if (l_dt.Rows[0]["AnoMesDia"].ToString() != "")
                        pReprogramacao.AnoMesDia = Convert.ToInt32(l_dt.Rows[0]["AnoMesDia"]);
                    pReprogramacao.StatusCor = l_dt.Rows[0]["StatusCor"].ToString();

                    pReprogramacao.Data = l_dt.Rows[0]["Data"].ToString();
                    pReprogramacao.Hora = l_dt.Rows[0]["Hora"].ToString();
                    pReprogramacao.Solicitante = l_dt.Rows[0]["Solicitante"].ToString();
                    if (l_dt.Rows[0]["CodigoCliente"].ToString() != "")
                        pReprogramacao.CodigoCliente = Convert.ToInt32(l_dt.Rows[0]["CodigoCliente"]);
                    pReprogramacao.NomeCliente = l_dt.Rows[0]["NomeCliente"].ToString();
                    pReprogramacao.ExecutarServico = l_dt.Rows[0]["ExecutarServico"].ToString();
                    if (l_dt.Rows[0]["Quantidade"].ToString() != "")
                        pReprogramacao.Quantidade = Convert.ToDecimal(l_dt.Rows[0]["Quantidade"]);
                    if (l_dt.Rows[0]["CodigoCaminhao"].ToString() != "")
                        pReprogramacao.CodigoCaminhao = Convert.ToInt32(l_dt.Rows[0]["CodigoCaminhao"]);
                    pReprogramacao.ModeloCaminhao = l_dt.Rows[0]["ModeloCaminhao"].ToString();
                    if (l_dt.Rows[0]["CodigoMotorista"].ToString() != "")
                        pReprogramacao.CodigoMotorista = Convert.ToInt32(l_dt.Rows[0]["CodigoMotorista"]);
                    if (l_dt.Rows[0]["CodigoResiduo"].ToString() != "")
                        pReprogramacao.CodigoResiduo = Convert.ToInt32(l_dt.Rows[0]["CodigoResiduo"]);
                    pReprogramacao.NomeMotorista = l_dt.Rows[0]["NomeMotorista"].ToString();
                    pReprogramacao.Observacao = l_dt.Rows[0]["Observacao"].ToString();
                    pReprogramacao.DestinoFinal = l_dt.Rows[0]["DestinoFinal"].ToString();
                    pReprogramacao.Unidade = l_dt.Rows[0]["Unidade"].ToString();
                    if (l_dt.Rows[0]["TipoProgramacao"].ToString() != "")
                        pReprogramacao.TipoProgramacao = Convert.ToInt32(l_dt.Rows[0]["TipoProgramacao"]);
                    if (l_dt.Rows[0]["MapaMarcado"].ToString() != "")
                        pReprogramacao.MapaMarcado = Convert.ToInt32(l_dt.Rows[0]["MapaMarcado"]);

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
        public DataTable PegaDados(int pAnoMesDia, int pQuadro, bool pNaoMostrarFechados)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select r.SequencialProgramacaoDiaria, r.AnoMesDia, r.StatusCor, r.Data, r.Hora, r.Solicitante, r.CodigoCliente, \n";
                s = s + "       c.Nome as NomeCliente, r.ExecutarServico, r.DataProgramada, r.HoraProgramada, r.Quantidade, \n";
                s = s + "       r.CodigoCaminhao, o.Modelo as ModeloCaminhao, r.CodigoMotorista, m.Nome as NomeMotorista, \n";
                s = s + "       r.Observacao, r.DestinoFinal, r.Unidade, r.TipoContrato, \n";
                s = s + "       r.Unidade2, r.MapaMarcado \n";
                s = s + "from   ReprogramacaoServicos r \n";
                s = s + "inner  join Clientes c on c.Codigo = r.CodigoCliente \n";
                s = s + "inner  join Caminhoes o on o.Codigo = r.CodigoCaminhao \n";
                s = s + "inner  join Funcionarios m on m.Codigo = r.CodigoMotorista \n";

                if (pAnoMesDia > 0)
                {
                    s = s + "where r.AnoMesDia = " + pAnoMesDia.ToString() + " \n";              
                    if (pNaoMostrarFechados)
                        s = s + "and  r.StatusCor <> '16761024' \n";
                    s = s +" order by r.StatusCor \n";
                    FillDataSet();
                    if (l_ds.Tables[0].Rows.Count > 0)
                    {
                        return l_ds.Tables[0];
                    }
                }
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
        public DataTable PegaReprogramacoes(string pStrAnoMesDia)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select distinct r.AnoMesDia, r.CodigoCliente, r.Data, r.Hora, r.DataProgramada \n";
                s = s + "from   ReprogramacaoServicos r \n";
                s = s + "where r.AnoMesDia In (" + pStrAnoMesDia + ") \n";
                s = s + "and   r.StatusCor = '8421631' \n";
                s = s + "order by r.StatusCor \n";
                FillDataSet();
                if (l_ds.Tables[0].Rows.Count > 0)
                {
                    return l_ds.Tables[0];
                }
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

        public DataTable PegaUltimaReprogramacaoAntecipacao(string pAnoMesDia, string pHora, string pCodigoCliente)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select r.SequencialProgramacaoDiaria, r.AnoMesDia, r.StatusCor, r.Data, r.Hora, r.Solicitante, r.CodigoCliente, \n";
                s = s + "       c.NomeFantasia as NomeCliente, r.ExecutarServico, r.DataProgramada, r.HoraProgramada, r.Quantidade, \n";
                s = s + "       r.CodigoCaminhao, o.Modelo as ModeloCaminhao, r.CodigoMotorista, m.Nome as NomeMotorista, \n";
                s = s + "       r.Observacao, r.DestinoFinal, r.Unidade, Convert(r.TipoContrato, char) as TipoContrato, \n";
                s = s + "       r.Unidade2, r.MapaMarcado, r.Sequencial \n";
                s = s + "from   ReprogramacaoServicos r \n";
                s = s + "inner  join Clientes c on c.Codigo = r.CodigoCliente \n";
                s = s + "left   join Caminhoes o on o.Codigo = r.CodigoCaminhao \n";
                s = s + "left   join Funcionarios m on m.Codigo = r.CodigoMotorista \n";
                s = s + "where  hora = '" + pHora + "' \n";
                if (pAnoMesDia != "")
                    s = s + "and    AnoMesDia = " + pAnoMesDia + " \n";
                s = s + "and    CodigoCliente = " + pCodigoCliente + " \n";
                s = s + "order  by Sequencial desc limit 1 \n";   
                FillDataSet();
                if (l_ds.Tables[0].Rows.Count > 0)
                {
                    return l_ds.Tables[0];
                }
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
        public DataTable PegaDadosLista(int pAnoMesDia, int pSequencialProgramacaoDiaria, int pCodigoCliente, string pNomeCliente,
                                        int pCodigoResiduo, string pStatusCor, DateTime pDataProgramada, string pHora, string pExecutarServico,
                                        string pTipoContrato)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select r.SequencialProgramacaoDiaria, r.AnoMesDia, r.StatusCor, r.Data, r.Hora, r.Solicitante, r.CodigoCliente, \n";
                s = s + "       c.NomeFantasia as NomeCliente, r.ExecutarServico, r.DataProgramada, r.HoraProgramada, r.Quantidade, \n";
                s = s + "       r.CodigoCaminhao, o.Modelo as ModeloCaminhao, r.CodigoMotorista, m.Nome as NomeMotorista, \n";
                s = s + "       r.Observacao, r.DestinoFinal, r.Unidade, Convert(r.TipoContrato, char) as TipoContrato, \n";
                s = s + "       r.Unidade2, r.MapaMarcado, r.Sequencial \n";
                s = s + "from   ReprogramacaoServicos r \n";
                s = s + "inner  join Clientes c on c.Codigo = r.CodigoCliente \n";
                s = s + "left   join Caminhoes o on o.Codigo = r.CodigoCaminhao \n";
                s = s + "left   join Funcionarios m on m.Codigo = r.CodigoMotorista \n";
                s = s + "where  Hora = '" + pHora + "' \n";
                if (pCodigoCliente > 0)
                    s = s + "and   CodigoCliente = " + pCodigoCliente + " \n";
                s = s + "order  by Sequencial \n";   
                FillDataSet();
                if (l_ds.Tables[0].Rows.Count > 0)
                {
                    return l_ds.Tables[0];
                }                
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
                s = s + "from   ReprogramacaoServicos \n";
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
                    s = s + "update ReprogramacaoServicos \n";
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
        public string DadoExiste(int pSequencial)
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Sequencial ";
                s = s + "from   ReprogramacaoServicos ";
                if (pSequencial != 0)
                {
                    s = s + "where  Sequencial = " + pSequencial + " ";
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
        public string DadoExiste(int pAnoMesDia, string pHora, int pCodigoCliente)
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Sequencial ";
                s = s + "from   ReprogramacaoServicos ";
                s = s + "where  AnoMesDia     = " + pAnoMesDia.ToString() + " \n";
                s = s + "and    Hora          = '" + pHora + "' \n";
                s = s + "and    CodigoCliente = " + pCodigoCliente + " \n";
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
        public int DadoExisteRetornaSequencial(int pAnoMesDia, string pHora, int pCodigoCliente)
        {
            int iRet = 0;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Sequencial ";
                s = s + "from   ReprogramacaoServicos ";
                s = s + "where  AnoMesDia     = " + pAnoMesDia.ToString() + " \n";
                s = s + "and    Hora          = '" + pHora + "' \n";
                s = s + "and    CodigoCliente = " + pCodigoCliente + " \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                {
                    if (l_dt.Rows[0]["Sequencial"].ToString() != "")
                        iRet = Convert.ToInt32(l_dt.Rows[0]["Sequencial"]);
                }
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return iRet;
        }

        public string ExisteReprogramacao(int pAnoMesDia, string pHora, int pCodigoCliente, string pDataProgramada)
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Sequencial ";
                s = s + "from   ReprogramacaoServicos ";
                s = s + "where  AnoMesDia      = "  + pAnoMesDia.ToString() + " \n";
                s = s + "and    Hora           = '" + pHora + "' \n";
                s = s + "and    CodigoCliente  = "  + pCodigoCliente + " \n";
                s = s + "and    DataProgramada = '" + Convert.ToDateTime(pDataProgramada).ToString("yyyy-MM-dd") + "' \n";
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
        public string Inserir(clsReprogramacaoServicos pReprogramacao, int pSequencial = 0, bool pSoSql = false)
        {
            try
            {
                s = "";
                s = s + "insert into ReprogramacaoServicos ";
                s = s + "(";
                if (pSequencial > 0)
                    s = s + "   Sequencial, ";
                s = s + "   SequencialProgramacaoDiaria, AnoMesDia, Data, Hora, Solicitante, CodigoCliente, \n";
                s = s + "   ExecutarServico, DataProgramada, Quantidade, \n";
                s = s + "   CodigoCaminhao, CodigoMotorista, \n";
                s = s + "   Observacao, Unidade, TipoContrato, \n";
                s = s + "   MapaMarcado, StatusCor, DestinoFinal, CodigoResiduo, HoraProgramada \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pSequencial > 0)
                    s = s + pSequencial + ", ";
                s = s + " " + pReprogramacao.SequencialProgramacaoDiaria + ", \n";
                s = s + " " + pReprogramacao.AnoMesDia + ", \n";
                if (pReprogramacao.Data != "")
                    s = s + "'" + Convert.ToDateTime(pReprogramacao.Data).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                s = s + "'" + pReprogramacao.Hora + "', \n";
                s = s + "'" + pReprogramacao.Solicitante + "', \n";
                s = s + " " + pReprogramacao.CodigoCliente + ", \n";
                s = s + "'" + pReprogramacao.ExecutarServico + "', \n";
                if (pReprogramacao.DataProgramada != "")
                    s = s + "'" + Convert.ToDateTime(pReprogramacao.DataProgramada).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                s = s + " " + pReprogramacao.Quantidade.ToString().Replace(",", ".") + ", \n";
                s = s + " " + pReprogramacao.CodigoCaminhao + ", \n";
                s = s + " " + pReprogramacao.CodigoMotorista + ", \n";
                s = s + "'" + pReprogramacao.Observacao + "', \n";
                s = s + "'" + pReprogramacao.Unidade + "', \n";
                s = s + " " + pReprogramacao.TipoProgramacao + ", \n";
                s = s + "'" + pReprogramacao.MapaMarcado + "', \n";
                s = s + "'" + pReprogramacao.StatusCor + "', \n";
                s = s + "'" + pReprogramacao.DestinoFinal + "', \n";
                s = s + "'" + pReprogramacao.CodigoResiduo + "', \n";
                s = s + "'" + pReprogramacao.HoraProgramada + "' \n";
                s = s + "); \n";
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
                    return "";
                }
                else
                    return s;

            }
            finally
            {
                DesconectaBanco();
            }
        }
        public string Alterar(clsReprogramacaoServicos pReprogramacao, int pSequencial, bool pSoSql = false)
        {
            try
            {
                s = "";
                s = s + "update ReprogramacaoServicos ";
                s = s + " set SequencialProgramacaoDiaria = " + pReprogramacao.SequencialProgramacaoDiaria + ", ";
                s = s + "     AnoMesDia  = " + pReprogramacao.AnoMesDia + ", ";
                if (pReprogramacao.Data != "")
                    s = s + "   Data       = '" + Convert.ToDateTime(pReprogramacao.Data).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "   Data       = '0001-01-01', \n";
                s = s + "       Hora       = '" + pReprogramacao.Hora + "', ";
                s = s + "       Solicitante = '" + pReprogramacao.Solicitante + "', ";
                s = s + "       CodigoCliente = " + pReprogramacao.CodigoCliente + ", ";
                s = s + "       ExecutarServico = '" + pReprogramacao.ExecutarServico + "', ";
                if (pReprogramacao.DataProgramada != "")
                    s = s + "   DataProgramada = '" + Convert.ToDateTime(pReprogramacao.DataProgramada).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "   DataProgramada = '0001-01-01', \n";
                s = s + "       Quantidade = " + pReprogramacao.Quantidade.ToString().Replace(",", ".") + ", ";
                s = s + "       CodigoCaminhao = " + pReprogramacao.CodigoCaminhao + ", ";
                s = s + "       CodigoMotorista = " + pReprogramacao.CodigoMotorista + ", ";
                s = s + "       CodigoResiduo = " + pReprogramacao.CodigoResiduo + ", ";
                s = s + "       Observacao = '" + pReprogramacao.Observacao + "', ";
                s = s + "       DestinoFinal = '" + pReprogramacao.DestinoFinal + "', ";
                s = s + "       Unidade = '" + pReprogramacao.Unidade + "', ";
                s = s + "       TipoContrato = " + pReprogramacao.TipoProgramacao + ", ";
                s = s + "       MapaMarcado = '" + pReprogramacao.MapaMarcado + "' ";
                s = s + "where  Sequencial = " + pSequencial.ToString() + "; \n";

                if (!pSoSql)
                {
                    oDB.MySqlConnect.Open();
                    MySqlCommand command = oDB.MySqlConnect.CreateCommand();
                    command.CommandTimeout = 360;
                    MySqlTransaction transaction;
                    transaction = oDB.MySqlConnect.BeginTransaction();
                    command.Connection = oDB.MySqlConnect;
                    command.Transaction = transaction;
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                    return "";
                }
                else
                    return s;
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public DataTable PreencheDataTableProgramacao()
        {
            ConectaBanco();
            s = "";
            s = s + "select * ";
            s = s + "from   ReprogramacaoServicos";
            l_ds = new DataSet();
            l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
            l_ds = new DataSet();
            l_myData.Fill(l_ds);
            DesconectaBanco();
            return l_ds.Tables[0];
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
                s = s + "delete from ReprogramacaoServicos ";
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
        public void Excluir(string pAnoMesDia)
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
                s = s + "delete from ReprogramacaoServicos ";
                s = s + "where  AnoMesDia >= " + pAnoMesDia;
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
        public void Excluir(string pDataProgramada, string pHora, int pCodigoCliente)
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
                s = s + "delete from ReprogramacaoServicos ";
                s = s + "where  DataProgramada = '" + Convert.ToDateTime(pDataProgramada).ToString("yyyy-MM-dd") + "' \n";
                s = s + "and    Hora           = '" + pHora + "' \n";
                s = s + "and    CodigoCliente  =  " + pCodigoCliente + " \n";
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
                s = s + "update ReprogramacaoServicos \n";
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

        public void SalvarDataProgramacaoAbertaCorDeAntecipacao(string pHora, string pCodigoCliente, string pSequencialProgramacao)
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
                s = s + "update ReprogramacaoServicos \n";
                s = s + "set    StatusCor      = '15000000' \n";
                s = s + "where  Hora           = '" + pHora + "' \n";
                s = s + "and    CodigoCliente = " + pCodigoCliente + " \n";
                s = s + "and    SequencialProgramacaoDiaria = " + pSequencialProgramacao + " \n";
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
    }
}