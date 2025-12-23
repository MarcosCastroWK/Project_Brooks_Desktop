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
using System.Globalization;

namespace LibSILC
{
	public class clsServicosFuturaDados
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

        public DataTable PegaDados(clsServicosFutura pServicosFutura, int pCodigoCliente, string pDataProgramada)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select s.CodigoCliente,  c.Nome as NomeCliente, s.DataProgramada, \n";
                s = s + "       s.CodigoCaminhao, o.Modelo as ModeloCaminhao, s.CodigoMotorista, m.Nome as NomeMotorista, \n";
                s = s + "       s.Observacao,     s.DestinoFinal, s.MapaMarcado, s.ServicoAExecutar, s.Hora, s.NumeroMTRe \n";
                s = s + "from   ServicosFutura s \n";
                s = s + "inner  join Clientes c on c.Codigo = s.CodigoCliente \n";
                s = s + "inner  join Caminhoes o on o.Codigo = s.CodigoCaminhao \n";
                s = s + "inner  join Funcionarios m on m.Codigo = s.CodigoMotorista \n";
                s = s + "where  s.CodigoCliente = " + pCodigoCliente + " \n";
                s = s + "and    s.DataProgramada = " + Convert.ToDateTime(pDataProgramada).ToString("yyyy-MM-dd") + " \n";
                s = s + "order by s.CodigoCliente ";
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows.Count > 0)
                    {
                        if (l_dt.Rows[0]["CodigoCliente"].ToString() != "")
                            pServicosFutura.CodigoCliente = Convert.ToInt32(l_dt.Rows[0]["CodigoCliente"]);
                        pServicosFutura.NomeCliente = l_dt.Rows[0]["NomeCliente"].ToString();
                        if (l_dt.Rows[0]["DataProgramada"].ToString() != "")
                            pServicosFutura.DataProgramada = Convert.ToDateTime(l_dt.Rows[0]["DataProgramada"].ToString()).Date.ToShortDateString();
                        if (l_dt.Rows[0]["CodigoCaminhao"].ToString() != "")
                            pServicosFutura.CodigoCaminhao = Convert.ToInt32(l_dt.Rows[0]["CodigoCaminhao"]);
                        pServicosFutura.ModeloCaminhao = l_dt.Rows[0]["ModeloCaminhao"].ToString();
                        if (l_dt.Rows[0]["CodigoMotorista"].ToString() != "")
                            pServicosFutura.CodigoMotorista = Convert.ToInt32(l_dt.Rows[0]["CodigoMotorista"]);
                        pServicosFutura.NomeMotorista = l_dt.Rows[0]["NomeMotorista"].ToString();
                        pServicosFutura.Observacao = l_dt.Rows[0]["Observacao"].ToString();
                        pServicosFutura.DestinoFinal = l_dt.Rows[0]["DestinoFinal"].ToString();
                        if (l_dt.Rows[0]["MapaMarcado"].ToString() != "")
                            pServicosFutura.MapaMarcado = Convert.ToInt32(l_dt.Rows[0]["MapaMarcado"]);
                        pServicosFutura.ServicoAExecutar = l_dt.Rows[0]["ServicoAExecutar"].ToString();
                        pServicosFutura.Hora = l_dt.Rows[0]["Hora"].ToString();
                        pServicosFutura.NumeroMTRe = l_dt.Rows[0]["NumeroMTRe"].ToString();
                    }
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
        public DataTable GetDados(clsServicosFutura pServicosFutura, int pCodigoCliente, int pCodigoResiduo, string pHora, string pDataProgramada = "")
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select s.CodigoCliente, s.Solicitante, c.Nome as NomeCliente, s.DataProgramada, \n";
                s = s + "       s.CodigoCaminhao, o.Modelo as ModeloCaminhao, s.CodigoMotorista, m.Nome as NomeMotorista, \n";
                s = s + "       s.Observacao,     s.DestinoFinal, s.MapaMarcado, s.ServicoAExecutar, s.Hora, s.NumeroMTRe \n";
                s = s + "from   ServicosFutura s \n";
                s = s + "inner  join Clientes c on c.Codigo = s.CodigoCliente \n";
                s = s + "left   join Caminhoes o on o.Codigo = s.CodigoCaminhao \n";
                s = s + "left   join Funcionarios m on m.Codigo = s.CodigoMotorista \n";
                s = s + "where  s.CodigoCliente = " + pCodigoCliente.ToString() + " \n";
                s = s + "and    s.CodigoResiduo = " + pCodigoResiduo.ToString() + " \n";
                if (pHora != "")
                    s = s + "and    s.Hora = '" + pHora +  "' \n";
                else
                    s = s + "and    not s.Hora like '%:%' \n";
                if (pDataProgramada != "") // here Edson 22/01/24
                    s = s + "and    s.DataProgramada = '" + Convert.ToDateTime(pDataProgramada).ToString("yyyyMMdd") + "' \n";
                //if (pDataProgramada != "") // código anterior - mudou de maior ou igual para 'igual'.
                //    s = s + "and    s.DataProgramada >= '" + Convert.ToDateTime(pDataProgramada).ToString("yyyyMMdd") + "' \n";
                s = s + "order by s.Sequencial desc ";
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows.Count > 0)
                    {
                        if (l_dt.Rows[0]["CodigoCliente"].ToString() != "")
                            pServicosFutura.CodigoCliente = Convert.ToInt32(l_dt.Rows[0]["CodigoCliente"]);
                        pServicosFutura.Solicitante = l_dt.Rows[0]["Solicitante"].ToString();
                        pServicosFutura.NomeCliente = l_dt.Rows[0]["NomeCliente"].ToString();
                        if (l_dt.Rows[0]["DataProgramada"].ToString() != "")
                            pServicosFutura.DataProgramada = Convert.ToDateTime(l_dt.Rows[0]["DataProgramada"].ToString()).Date.ToShortDateString();
                        if (l_dt.Rows[0]["CodigoCaminhao"].ToString() != "")
                            pServicosFutura.CodigoCaminhao = Convert.ToInt32(l_dt.Rows[0]["CodigoCaminhao"]);
                        pServicosFutura.ModeloCaminhao = l_dt.Rows[0]["ModeloCaminhao"].ToString();
                        if (l_dt.Rows[0]["CodigoMotorista"].ToString() != "")
                            pServicosFutura.CodigoMotorista = Convert.ToInt32(l_dt.Rows[0]["CodigoMotorista"]);
                        pServicosFutura.NomeMotorista = l_dt.Rows[0]["NomeMotorista"].ToString();
                        pServicosFutura.Observacao = l_dt.Rows[0]["Observacao"].ToString();
                        pServicosFutura.DestinoFinal = l_dt.Rows[0]["DestinoFinal"].ToString();
                        if (l_dt.Rows[0]["MapaMarcado"].ToString() != "")
                            pServicosFutura.MapaMarcado = Convert.ToInt32(l_dt.Rows[0]["MapaMarcado"]);
                        pServicosFutura.ServicoAExecutar = l_dt.Rows[0]["ServicoAExecutar"].ToString();
                        pServicosFutura.Hora = l_dt.Rows[0]["Hora"].ToString();
                        pServicosFutura.NumeroMTRe = l_dt.Rows[0]["NumeroMTRe"].ToString();
                    }
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
        public DataTable PegaDados(clsServicosFutura pServicosFutura, int pCodigoCliente, string pDataProgramada, bool pMapaMarcado)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select s.CodigoCliente, c.Nome as NomeCliente, s.DataProgramada, \n";
                s = s + "       s.CodigoCaminhao, o.Modelo as ModeloCaminhao, s.CodigoMotorista, m.Nome as NomeMotorista, \n";
                s = s + "       s.Observacao, s.DestinoFinal, s.MapaMarcado, s.ServicoAExecutar, s.Hora, s.NumeroMTRe \n";
                s = s + "from   ServicosFutura s \n";
                s = s + "inner  join Clientes c on c.Codigo = s.CodigoCliente \n";
                s = s + "inner  join Caminhoes o on o.Codigo = s.CodigoCaminhao \n";
                s = s + "inner  join Funcionarios m on m.Codigo = s.CodigoMotorista \n";
                if (pCodigoCliente > 0 && pDataProgramada != "")
                {
                    s = s + "where  s.CodigoCliente = " + pCodigoCliente + " \n";
                    s = s + "and    s.DataProgramada = " + Convert.ToDateTime(pDataProgramada).ToString("yyyy-MM-dd") + " \n";
                    if (pMapaMarcado)
                        s = s + "and s.MapaMarcado > 0 \n";
                }
                else
                    if (pMapaMarcado)
                        s = s + "where s.MapaMarcado > 0 \n";
                s = s +" order by c.Nome \n";
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
        public string DadoExiste(int pCodigoCliente, string pDataProgramada)
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select * ";
                s = s + "from   ServicosFutura ";
                s = s + "where  CodigoCliente = " + pCodigoCliente + " \n";
                s = s + "and    DataProgramada = " + Convert.ToDateTime(pDataProgramada).ToString("yyyy-MM-dd") + " \n";
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
        public string DadoExiste(int pCodigoCliente, int pCodigoResiduo, string pDataProgramada, string pDescricaoResiduo, string pHora)
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select * ";
                s = s + "from   ServicosFutura \n";
                s = s + "where  CodigoCliente = " + pCodigoCliente + " \n";
                if (pCodigoResiduo > 0)
                    s = s + "and    CodigoResiduo = " + pCodigoResiduo + " \n";
                else
                    s = s + "and    DescricaoResiduo = '" + pDescricaoResiduo + "' \n";
                s = s + "and    DataProgramada = '" + Convert.ToDateTime(pDataProgramada).ToString("yyyy-MM-dd") + "' \n";
                if (pHora.IndexOf(":") > -1)
                    s = s + "and    Hora = '" + pHora + "' \n";
                else
                    s = s + "and not Hora like '%:%' \n";
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
        public string PegaSolicitante(int pCodigoCliente, int pCodigoResiduo, string pDataProgramada, string pDescricaoResiduo, string pHora)
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Solicitante ";
                s = s + "from   ServicosFutura \n";
                s = s + "where  CodigoCliente = " + pCodigoCliente + " \n";
                if (pCodigoResiduo > 0)
                    s = s + "and    CodigoResiduo = " + pCodigoResiduo + " \n";
                else
                    s = s + "and    DescricaoResiduo = '" + pDescricaoResiduo + "' \n";
                if (pDataProgramada != "")
                    s = s + "and    DataProgramada = '" + Convert.ToDateTime(pDataProgramada).ToString("yyyy-MM-dd") + "' \n";
                if (pHora.IndexOf(":") > -1)
                    s = s + "and    Hora = '" + pHora + "' \n";
                else if (pHora != "")
                    s = s + "and    Hora = '" + pHora + "' \n";
                else
                    s = s + "and not Hora like '%:%' \n";
                s = s + "order by Sequencial desc \n"; 
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    return l_dt.Rows[0][0].ToString();
                else
                    return "";
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
        public string PegaObservacao(int pCodigoCliente, int pCodigoResiduo, string pDataProgramada, string pDescricaoResiduo, string pHora)
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Observacao ";
                s = s + "from   ServicosFutura \n";
                s = s + "where  CodigoCliente = " + pCodigoCliente + " \n";
                if (pCodigoResiduo > 0)
                    s = s + "and    CodigoResiduo = " + pCodigoResiduo + " \n";
                else
                    s = s + "and    DescricaoResiduo = '" + pDescricaoResiduo + "' \n";
                if (pDataProgramada != "")
                    s = s + "and    DataProgramada = '" + Convert.ToDateTime(pDataProgramada).ToString("yyyy-MM-dd") + "' \n";
                if (pHora.IndexOf(":") > -1)
                    s = s + "and    Hora = '" + pHora + "' \n";
                else if (pHora != "")
                    s = s + "and    Hora = '" + pHora + "' \n";
                else
                    s = s + "and not Hora like '%:%' \n";
                s = s + "order by Sequencial desc \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    return l_dt.Rows[0][0].ToString();
                else
                    return "";
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

        public string PegaDestinoFinal(int pCodigoCliente, int pCodigoResiduo, string pDataProgramada, string pDescricaoResiduo, string pHora)
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select DestinoFinal ";
                s = s + "from   ServicosFutura \n";
                s = s + "where  CodigoCliente = " + pCodigoCliente + " \n";
                if (pCodigoResiduo > 0)
                    s = s + "and    CodigoResiduo = " + pCodigoResiduo + " \n";
                else
                    s = s + "and    DescricaoResiduo = '" + pDescricaoResiduo + "' \n";
                if (pDataProgramada != "")
                    s = s + "and    DataProgramada = '" + Convert.ToDateTime(pDataProgramada).ToString("yyyy-MM-dd") + "' \n";
                //if (pHora.IndexOf(":") > -1)
                //    s = s + "and    Hora = '" + pHora + "' \n";
                //else
                //    s = s + "and not Hora like '%:%' \n";
                s = s + "and    Hora = '" + pHora + "' \n";
                s = s + "order by Sequencial desc \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    return l_dt.Rows[0][0].ToString();
                else
                    return "";
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
        public void Inserir(clsServicosFutura pServicosFutura)
        {
            oDB.ConectaMySql();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "insert into ServicosFutura ";
                s = s + "(";
                s = s + "  CodigoCliente, CodigoResiduo, DataProgramada, CodigoCaminhao, CodigoMotorista, Observacao, MapaMarcado, ServicoAExecutar, DescricaoResiduo, DestinoFinal, Hora, Solicitante, NumeroMTRe \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                s = s + " " + pServicosFutura.CodigoCliente + ", \n";
                s = s + " " + pServicosFutura.CodigoResiduo + ", \n";
                if (pServicosFutura.DataProgramada != "")
                    s = s + "'" + Convert.ToDateTime(pServicosFutura.DataProgramada).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                s = s + " " + pServicosFutura.CodigoCaminhao + ", ";
                s = s + " " + pServicosFutura.CodigoMotorista + ", ";
                s = s + "'" + pServicosFutura.Observacao + "', ";
                s = s + "'" + pServicosFutura.MapaMarcado + "', ";
                s = s + "'" + pServicosFutura.ServicoAExecutar + "', ";
                s = s + "'" + pServicosFutura.DescricaoResiduo + "', ";
                s = s + "'" + pServicosFutura.DestinoFinal + "', ";
                s = s + "'" + pServicosFutura.Hora + "', ";
                s = s + "'" + pServicosFutura.Solicitante + "', ";
                s = s + "'" + pServicosFutura.NumeroMTRe + "'";
                s = s + ") \n";
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
        public void Alterar(clsServicosFutura pServicosFutura, int pCodigoCliente, string pDataProgramada, string pHora, bool pFiltroNaHora)
        {
            oDB.MySqlConnect.Open();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update ServicosFutura \n";
                s = s + " set CodigoCliente = " + pServicosFutura.CodigoCliente + ", \n";
                s = s + "     CodigoResiduo = " + pServicosFutura.CodigoResiduo + ", \n";
                if (pServicosFutura.DataProgramada != "")
                    s = s + " DataProgramada = '" + Convert.ToDateTime(pServicosFutura.DataProgramada).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + " DataProgramada = '0001-01-01', \n";
                s = s + " CodigoCaminhao = " + pServicosFutura.CodigoCaminhao + ", \n";
                s = s + " CodigoMotorista = " + pServicosFutura.CodigoMotorista + ", \n";
                s = s + " Observacao = '" + pServicosFutura.Observacao + "', \n";
                s = s + " DestinoFinal = '" + pServicosFutura.DestinoFinal + "', \n";
                s = s + " MapaMarcado = '" + pServicosFutura.MapaMarcado + "', \n";
                s = s + " ServicoAExecutar = '" + pServicosFutura.ServicoAExecutar + "', \n";
                s = s + " DescricaoResiduo = '" + pServicosFutura.DescricaoResiduo + "', \n";
                s = s + " Hora = '" + pServicosFutura.Hora + "', \n";
                s = s + " Solicitante = '" + pServicosFutura.Solicitante + "', \n";
                s = s + " NumeroMTRe = '" + pServicosFutura.NumeroMTRe + "' \n";
                s = s + "where  CodigoCliente = " + pCodigoCliente + " \n";
                if (pServicosFutura.DescricaoResiduo != "" && pServicosFutura.CodigoResiduo == 0)
                    s = s + "and    DescricaoResiduo = '" + pServicosFutura.DescricaoResiduo + "' \n";
                else if (pServicosFutura.CodigoResiduo > 0)
                    s = s + "and    CodigoResiduo = " + pServicosFutura.CodigoResiduo + " \n";
                s = s + "and    DataProgramada = '" + Convert.ToDateTime(pDataProgramada).ToString("yyyy-MM-dd") + "' \n";
                if (pHora.IndexOf(":") > -1 || pFiltroNaHora)
                {
                    s = s + "and    Hora = '" + pHora + "' \n";
                }
                else
                {
                    s = s + "and not Hora like '%:%' \n";
                }
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

        public void SalvarCaminhao(int pCodigoCliente, string pDataProgramada, string pHora, int pCodigoResiduo, int pCodigoCaminhao)
        {
            oDB.MySqlConnect.Open();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update ServicosFutura \n";
                s = s + "set    CodigoCaminhao = " + pCodigoCaminhao + " \n";
                s = s + "where  CodigoCliente  = " + pCodigoCliente  + " \n";
                s = s + "and    CodigoResiduo  = " + pCodigoResiduo  + " \n";
                s = s + "and    DataProgramada = '" + Convert.ToDateTime(pDataProgramada).ToString("yyyy-MM-dd") + "' \n";
                if (pHora.IndexOf(":") > -1)
                {
                    s = s + "and    Hora = '" + pHora + "' \n";
                }
                else
                {
                    s = s + "and not Hora like '%:%' \n";
                }
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

        public void SalvarMotorista(int pCodigoCliente, string pDataProgramada, string pHora, int pCodigoResiduo, int pCodigoMotorista)
        {
            oDB.MySqlConnect.Open();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update ServicosFutura \n";
                s = s + "set    CodigoMotorista = " + pCodigoMotorista + " \n";
                s = s + "where  CodigoCliente   = " + pCodigoCliente + " \n";
                s = s + "and    CodigoResiduo   = " + pCodigoResiduo + " \n";
                s = s + "and    DataProgramada  = '" + Convert.ToDateTime(pDataProgramada).ToString("yyyy-MM-dd") + "' \n";
                if (pHora.IndexOf(":") > -1)
                {
                    s = s + "and    Hora = '" + pHora + "' \n";
                }
                else
                {
                    s = s + "and not Hora like '%:%' \n";
                }
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

        public void LimparMapa(string pDataProgramada)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update ServicosFutura \n";
                s = s + "set    MapaMarcado = '' \n ";
                s = s + "where  DataProgramada = '" + pDataProgramada + "' \n";
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

        public DataTable PreencheDataTable()
        {
            ConectaBanco();
            s = "";
            s = s + "select * ";
            s = s + "from  ServicosFutura";
            l_ds = new DataSet();
            l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
            l_ds = new DataSet();
            l_myData.Fill(l_ds);
            DesconectaBanco();
            return l_ds.Tables[0];
        }
        public DataTable PreencheDataTable(string pCodigosInClientes, string pDataProgramada)
        {
            ConectaBanco();
            s = "";
            s = s + "select sf.*, m.Nome as NomeMotorista, ca.Modelo as ModeloCaminhao \n";
            s = s + "from  ServicosFutura sf \n";
            s = s + "left  join Funcionarios m on m.Codigo = sf.CodigoMotorista \n";
            s = s + "left  join Caminhoes ca on ca.Codigo = sf.CodigoCaminhao \n";
            s = s + "where CodigoCliente in (" + pCodigosInClientes + ") \n";
            s = s + "and   DataProgramada >= '" + Convert.ToDateTime(pDataProgramada, new CultureInfo("pt-BR")).ToString("yyyy-MM-dd") + "' \n";
            s = s + "order by sf.DataProgramada desc \n";
            l_ds = new DataSet();
            l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
            l_ds = new DataSet();
            l_myData.Fill(l_ds);
            DesconectaBanco();
            return l_ds.Tables[0];
        }
        public void Excluir(int pCodigoCliente, string pDataProgramada)
        {
            oDB.MySqlConnect.Open();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "delete from ServicosFutura ";
                s = s + "where  CodigoCliente = " + pCodigoCliente + " \n";
                s = s + "and    DataProgramada = " + Convert.ToDateTime(pDataProgramada).ToString("yyyy-MM-dd") + " \n";
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
}