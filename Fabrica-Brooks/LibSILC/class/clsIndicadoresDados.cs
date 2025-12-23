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
    public class clsIndicadoresDados
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
        public clsIndicadores PegaDados(clsIndicadores pIndicadores, int pSequencial)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial, Data, Cumprimento, ImpossivelAtender, FlexibilidadeContrato, SolicitacaoCliente, \n";
                s = s + "       ProgramacaoAutomatica, CumprimentoManual, TotalProgramadoAutomatico, TotalProgramadoManual, \n";
                s = s + "       TotalProgramado, TotalExecutado, TotalExecutadoAutomatico, TotalExecutadoManual, TotalCancelado, \n";
                s = s + "       TotalCanceladoCliente, TotalReprog, TotalBloqueioFinanc, Cancelamento, BloqueioFinanceiro, \n";
                s = s + "       TotalReprogImpossivelAtender, TotalReprogFlexibilidade, TotalReprogSolicitacaoCliente, \n";
                s = s + "       IndiceReprog, TotalReprogOutros, IndiceReprogOutros \n";
                s = s + "from   Indicadores \n";
                if (pSequencial > 0)
                {
                    s = s + "where  Sequencial = " + pSequencial + " \n";
                    s = s + "limit 1";
                }
                else if (pSequencial == 0)
                {
                    s = s + " order by Sequencial limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows[0]["Sequencial"].ToString() != "")
                        pIndicadores.Sequencial = Convert.ToInt32(l_dt.Rows[0]["Sequencial"]);
                    if (l_dt.Rows[0]["Data"].ToString() != "")
                        pIndicadores.Data = Convert.ToDateTime(l_dt.Rows[0]["Data"].ToString()).Date.ToShortDateString();
                    if (l_dt.Rows[0]["Cumprimento"].ToString() != "")
                        pIndicadores.Cumprimento = Convert.ToDecimal(l_dt.Rows[0]["Cumprimento"]);
                    if (l_dt.Rows[0]["ImpossivelAtender"].ToString() != "")
                        pIndicadores.ImpossivelAtender = Convert.ToDecimal(l_dt.Rows[0]["ImpossivelAtender"]);
                    if (l_dt.Rows[0]["FlexibilidadeContrato"].ToString() != "")
                        pIndicadores.FlexibilidadeContrato = Convert.ToDecimal(l_dt.Rows[0]["FlexibilidadeContrato"]);
                    if (l_dt.Rows[0]["SolicitacaoCliente"].ToString() != "")
                        pIndicadores.SolicitacaoCliente = Convert.ToDecimal(l_dt.Rows[0]["SolicitacaoCliente"]);
                    if (l_dt.Rows[0]["ProgramacaoAutomatica"].ToString() != "")
                        pIndicadores.ProgramacaoAutomatica = Convert.ToDecimal(l_dt.Rows[0]["ProgramacaoAutomatica"]);
                    if (l_dt.Rows[0]["CumprimentoManual"].ToString() != "")
                        pIndicadores.CumprimentoManual = Convert.ToDecimal(l_dt.Rows[0]["CumprimentoManual"]);
                    if (l_dt.Rows[0]["TotalProgramadoAutomatico"].ToString() != "")
                        pIndicadores.TotalProgramadoAutomatico = Convert.ToDecimal(l_dt.Rows[0]["TotalProgramadoAutomatico"]);
                    if (l_dt.Rows[0]["TotalProgramadoManual"].ToString() != "")
                        pIndicadores.TotalProgramadoManual = Convert.ToDecimal(l_dt.Rows[0]["TotalProgramadoManual"]);
                    if (l_dt.Rows[0]["TotalProgramado"].ToString() != "")
                        pIndicadores.TotalProgramado = Convert.ToDecimal(l_dt.Rows[0]["TotalProgramado"]);
                    if (l_dt.Rows[0]["TotalExecutado"].ToString() != "")
                        pIndicadores.TotalExecutado = Convert.ToDecimal(l_dt.Rows[0]["TotalExecutado"]);
                    if (l_dt.Rows[0]["TotalExecutadoAutomatico"].ToString() != "")
                        pIndicadores.TotalExecutadoAutomatico = Convert.ToDecimal(l_dt.Rows[0]["TotalExecutadoAutomatico"]);
                    if (l_dt.Rows[0]["TotalExecutadoManual"].ToString() != "")
                        pIndicadores.TotalExecutadoManual = Convert.ToDecimal(l_dt.Rows[0]["TotalExecutadoManual"]);
                    if (l_dt.Rows[0]["TotalCancelado"].ToString() != "")
                        pIndicadores.TotalCancelado = Convert.ToDecimal(l_dt.Rows[0]["TotalCancelado"]);
                    if (l_dt.Rows[0]["TotalCanceladoCliente"].ToString() != "")
                        pIndicadores.TotalCanceladoCliente = Convert.ToDecimal(l_dt.Rows[0]["TotalCanceladoCliente"]);
                    if (l_dt.Rows[0]["TotalReprog"].ToString() != "")
                        pIndicadores.TotalReprog = Convert.ToDecimal(l_dt.Rows[0]["TotalReprog"]);
                    if (l_dt.Rows[0]["TotalBloqueioFinanc"].ToString() != "")
                        pIndicadores.TotalBloqueioFinanc = Convert.ToDecimal(l_dt.Rows[0]["TotalBloqueioFinanc"]);
                    if (l_dt.Rows[0]["Cancelamento"].ToString() != "")
                        pIndicadores.Cancelamento = Convert.ToDecimal(l_dt.Rows[0]["Cancelamento"]);
                    if (l_dt.Rows[0]["BloqueioFinanceiro"].ToString() != "")
                        pIndicadores.BloqueioFinanceiro = Convert.ToDecimal(l_dt.Rows[0]["BloqueioFinanceiro"]);
                    if (l_dt.Rows[0]["TotalReprogImpossivelAtender"].ToString() != "")
                        pIndicadores.TotalReprogImpossivelAtender = Convert.ToDecimal(l_dt.Rows[0]["TotalReprogImpossivelAtender"]);
                    if (l_dt.Rows[0]["TotalReprogFlexibilidade"].ToString() != "")
                        pIndicadores.TotalReprogFlexibilidade = Convert.ToDecimal(l_dt.Rows[0]["TotalReprogFlexibilidade"]);
                    if (l_dt.Rows[0]["TotalReprogSolicitacaoCliente"].ToString() != "")
                        pIndicadores.TotalReprogSolicitacaoCliente = Convert.ToDecimal(l_dt.Rows[0]["TotalReprogSolicitacaoCliente"]);
                    if (l_dt.Rows[0]["IndiceReprog"].ToString() != "")
                        pIndicadores.IndiceReprog = Convert.ToDecimal(l_dt.Rows[0]["IndiceReprog"]);
                    if (l_dt.Rows[0]["TotalReprogOutros"].ToString() != "")
                        pIndicadores.TotalReprogOutros = Convert.ToDecimal(l_dt.Rows[0]["TotalReprogOutros"]);
                    if (l_dt.Rows[0]["IndiceReprogOutros"].ToString() != "")
                        pIndicadores.IndiceReprogOutros = Convert.ToDecimal(l_dt.Rows[0]["IndiceReprogOutros"]);
                }
                return pIndicadores;
            }
            catch (Exception ex)
            {
                return new clsIndicadores();
            }
            finally
            {
                DesconectaBanco();
            }
        }
        
        public DataTable PegaDados(clsIndicadores pIndicadores, int pSequencial, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial, Data, Cumprimento, ImpossivelAtender, FlexibilidadeContrato, SolicitacaoCliente, \n";
                s = s + "       ProgramacaoAutomatica, CumprimentoManual, TotalProgramadoAutomatico, TotalProgramadoManual, \n";
                s = s + "       TotalProgramado, TotalExecutado, TotalExecutadoAutomatico, TotalExecutadoManual, TotalCancelado, \n";
                s = s + "       TotalCanceladoCliente, TotalReprog, TotalBloqueioFinanc, Cancelamento, BloqueioFinanceiro, \n";
                s = s + "       TotalReprogImpossivelAtender, TotalReprogFlexibilidade, TotalReprogSolicitacaoCliente, \n";
                s = s + "       IndiceReprog, TotalReprogOutros, IndiceReprogOutros \n";
                s = s + "from   Indicadores \n";
                if (pSequencial > 0)
                {
                    s = s + "where  Sequencial = " + pSequencial + " ";
                }
                if (pUltimoRegistro)
                {
                    s = s + "order by Sequencial desc ";
                }
                else if (pSequencial == 0)
                {
                    s = s + " order by Data ";
                }
                FillDataSet();
                return l_dt;
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
        public int PegaTamanhoDataVarChar(string pData)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "show columns ";
                s = s + "from Indicadores ";
                s = s + "where  type like 'varchar%' and field = '" + pData + "' ";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                {
                    return Convert.ToInt16(l_dt.Rows[0]["Type"].ToString().Replace("varchar(", "").Replace(")", ""));
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public string DadoExiste(int pSequencial)
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Sequencial ";
                s = s + "from   Indicadores  ";
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
        public void Inserir(clsIndicadores pIndicadores)
        {
            try
            {
                s = "";
                s = s + "insert into Indicadores \n";
                s = s + "( \n";
                s = s + "  Data, Cumprimento, ImpossivelAtender, FlexibilidadeContrato, SolicitacaoCliente, \n";
                s = s + "  ProgramacaoAutomatica, CumprimentoManual, TotalProgramadoAutomatico, TotalProgramadoManual, \n";
                s = s + "  TotalProgramado, TotalExecutado, TotalExecutadoAutomatico, TotalExecutadoManual, TotalCancelado, \n";
                s = s + "  TotalCanceladoCliente, TotalReprog, TotalBloqueioFinanc, Cancelamento, BloqueioFinanceiro, \n";
                s = s + "  TotalReprogImpossivelAtender, TotalReprogFlexibilidade, TotalReprogSolicitacaoCliente, \n";
                s = s + "  IndiceReprog, TotalReprogOutros, IndiceReprogOutros \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (l_dt.Rows[0]["Data"].ToString() != "")
                    s = s + "'" + Convert.ToDateTime(pIndicadores.Data).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                if (pIndicadores.Cumprimento.ToString() != "")
                    s = s + " " + pIndicadores.Cumprimento.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pIndicadores.ImpossivelAtender.ToString() != "")
                    s = s + " " + pIndicadores.ImpossivelAtender.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pIndicadores.FlexibilidadeContrato.ToString() != "")
                    s = s + " " + pIndicadores.FlexibilidadeContrato.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pIndicadores.SolicitacaoCliente.ToString() != "")
                    s = s + " " + pIndicadores.SolicitacaoCliente.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pIndicadores.ProgramacaoAutomatica.ToString() != "")
                    s = s + " " + pIndicadores.ProgramacaoAutomatica.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pIndicadores.CumprimentoManual.ToString() != "")
                    s = s + " " + pIndicadores.CumprimentoManual.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pIndicadores.TotalProgramadoAutomatico.ToString() != "")
                    s = s + " " + pIndicadores.TotalProgramadoAutomatico.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pIndicadores.TotalProgramadoManual.ToString() != "")
                    s = s + " " + pIndicadores.TotalProgramadoManual.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pIndicadores.TotalProgramado.ToString() != "")
                    s = s + " " + pIndicadores.TotalProgramado.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pIndicadores.TotalExecutado.ToString() != "")
                    s = s + " " + pIndicadores.TotalExecutado.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pIndicadores.TotalExecutadoAutomatico.ToString() != "")
                    s = s + " " + pIndicadores.TotalExecutadoAutomatico.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pIndicadores.TotalExecutadoManual.ToString() != "")
                    s = s + " " + pIndicadores.TotalExecutadoManual.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pIndicadores.TotalCancelado.ToString() != "")
                    s = s + " " + pIndicadores.TotalCancelado.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pIndicadores.TotalCanceladoCliente.ToString() != "")
                    s = s + " " + pIndicadores.TotalCanceladoCliente.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pIndicadores.TotalReprog.ToString() != "")
                    s = s + " " + pIndicadores.TotalReprog.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pIndicadores.TotalBloqueioFinanc.ToString() != "")
                    s = s + " " + pIndicadores.TotalBloqueioFinanc.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pIndicadores.Cancelamento.ToString() != "")
                    s = s + " " + pIndicadores.Cancelamento.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pIndicadores.BloqueioFinanceiro.ToString() != "")
                    s = s + " " + pIndicadores.BloqueioFinanceiro.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pIndicadores.TotalReprogImpossivelAtender.ToString() != "")
                    s = s + " " + pIndicadores.TotalReprogImpossivelAtender.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pIndicadores.TotalReprogFlexibilidade.ToString() != "")
                    s = s + " " + pIndicadores.TotalReprogFlexibilidade.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pIndicadores.TotalReprogSolicitacaoCliente.ToString() != "")
                    s = s + " " + pIndicadores.TotalReprogSolicitacaoCliente.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pIndicadores.IndiceReprog.ToString() != "")
                    s = s + " " + pIndicadores.IndiceReprog.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pIndicadores.TotalReprogOutros.ToString() != "")
                    s = s + " " + pIndicadores.TotalReprogOutros.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pIndicadores.IndiceReprogOutros.ToString() != "")
                    s = s + " " + pIndicadores.IndiceReprogOutros.ToString().Replace(",", ".") + " \n";
                else
                    s = s + "0 \n"; 
               
                s = s + ")"; 
                ConectaBanco();
                MySqlCommand command = oDB.MySqlConnect.CreateCommand();
                MySqlTransaction transaction;
                transaction = oDB.MySqlConnect.BeginTransaction();
                command.Connection = oDB.MySqlConnect;
                command.Transaction = transaction;
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public string Alterar(clsIndicadores pIndicadores, int pSequencial)
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
                s = s + "update Indicadores \n";
                if (l_dt.Rows[0]["Data"].ToString() != "")
                    s = s + " set Data = '" + Convert.ToDateTime(pIndicadores.Data).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + " set Data = '0001-01-01', \n";
                if (pIndicadores.Cumprimento.ToString() != "")
                    s = s + " Cumprimento = " + pIndicadores.Cumprimento.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " Cumprimento = 0, \n";
                if (pIndicadores.ImpossivelAtender.ToString() != "")
                    s = s + " ImpossivelAtender = " + pIndicadores.ImpossivelAtender.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " ImpossivelAtender = 0, \n";
                if (pIndicadores.FlexibilidadeContrato.ToString() != "")
                    s = s + " FlexibilidadeContrato = " + pIndicadores.FlexibilidadeContrato.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " FlexibilidadeContrato = 0, \n";
                if (pIndicadores.SolicitacaoCliente.ToString() != "")
                    s = s + " SolicitacaoCliente = " + pIndicadores.SolicitacaoCliente.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " SolicitacaoCliente = 0, \n";
                if (pIndicadores.ProgramacaoAutomatica.ToString() != "")
                    s = s + " ProgramacaoAutomatica = " + pIndicadores.ProgramacaoAutomatica.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " ProgramacaoAutomatica = 0, \n";
                if (pIndicadores.CumprimentoManual.ToString() != "")
                    s = s + " CumprimentoManual = " + pIndicadores.CumprimentoManual.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "CumprimentoManual = 0, \n";
                if (pIndicadores.TotalProgramadoAutomatico.ToString() != "")
                    s = s + " TotalProgramadoAutomatico = " + pIndicadores.TotalProgramadoAutomatico.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " TotalProgramadoAutomatico = 0, \n";
                if (pIndicadores.TotalProgramadoManual.ToString() != "")
                    s = s + " TotalProgramadoManual = " + pIndicadores.TotalProgramadoManual.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " TotalProgramadoManual = 0, \n";
                if (pIndicadores.TotalProgramado.ToString() != "")
                    s = s + " TotalProgramado = " + pIndicadores.TotalProgramado.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " TotalProgramado = 0, \n";
                if (pIndicadores.TotalExecutado.ToString() != "")
                    s = s + " TotalExecutado = " + pIndicadores.TotalExecutado.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " TotalExecutado = 0, \n";
                if (pIndicadores.TotalExecutadoAutomatico.ToString() != "")
                    s = s + " TotalExecutadoAutomatico = " + pIndicadores.TotalExecutadoAutomatico.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " TotalExecutadoAutomatico = 0, \n";
                if (pIndicadores.TotalExecutadoManual.ToString() != "")
                    s = s + " TotalExecutadoManual = " + pIndicadores.TotalExecutadoManual.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " TotalExecutadoManual = 0, \n";
                if (pIndicadores.TotalCancelado.ToString() != "")
                    s = s + " TotalCancelado = " + pIndicadores.TotalCancelado.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " TotalCancelado = 0, \n";
                if (pIndicadores.TotalCanceladoCliente.ToString() != "")
                    s = s + " TotalCanceladoCliente = " + pIndicadores.TotalCanceladoCliente.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " TotalCanceladoCliente = 0, \n";
                if (pIndicadores.TotalReprog.ToString() != "")
                    s = s + " TotalReprog = " + pIndicadores.TotalReprog.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " TotalReprog = 0, \n";
                if (pIndicadores.TotalBloqueioFinanc.ToString() != "")
                    s = s + " TotalBloqueioFinanc = " + pIndicadores.TotalBloqueioFinanc.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " TotalBloqueioFinanc = 0, \n";
                if (pIndicadores.Cancelamento.ToString() != "")
                    s = s + " Cancelamento = " + pIndicadores.Cancelamento.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " Cancelamento = 0, \n";
                if (pIndicadores.BloqueioFinanceiro.ToString() != "")
                    s = s + " BloqueioFinanceiro = " + pIndicadores.BloqueioFinanceiro.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " BloqueioFinanceiro = 0, \n";
                if (pIndicadores.TotalReprogImpossivelAtender.ToString() != "")
                    s = s + " TotalReprogImpossivelAtender = " + pIndicadores.TotalReprogImpossivelAtender.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " TotalReprogImpossivelAtender = 0, \n";
                if (pIndicadores.TotalReprogFlexibilidade.ToString() != "")
                    s = s + " TotalReprogFlexibilidade = " + pIndicadores.TotalReprogFlexibilidade.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " TotalReprogFlexibilidade = 0, \n";
                if (pIndicadores.TotalReprogSolicitacaoCliente.ToString() != "")
                    s = s + " TotalReprogSolicitacaoCliente = " + pIndicadores.TotalReprogSolicitacaoCliente.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " TotalReprogSolicitacaoCliente = 0, \n";
                if (pIndicadores.IndiceReprog.ToString() != "")
                    s = s + " IndiceReprog = " + pIndicadores.IndiceReprog.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " IndiceReprog = 0, \n";
                if (pIndicadores.TotalReprogOutros.ToString() != "")
                    s = s + " TotalReprogOutros = " + pIndicadores.TotalReprogOutros.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " TotalReprogOutros = 0, \n";
                if (pIndicadores.IndiceReprogOutros.ToString() != "")
                    s = s + " IndiceReprogOutros = " + pIndicadores.IndiceReprogOutros.ToString().Replace(",", ".") + " \n";
                else
                    s = s + " IndiceReprogOutros = 0 \n";

                s = s + ")";
                command.CommandText = s;
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
        public DataTable PreencheDataTable(string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial, Data, Cumprimento, ImpossivelAtender, FlexibilidadeContrato, SolicitacaoCliente, \n";
                s = s + "       ProgramacaoAutomatica, CumprimentoManual, TotalProgramadoAutomatico, TotalProgramadoManual, \n";
                s = s + "       TotalProgramado, TotalExecutado, TotalExecutadoAutomatico, TotalExecutadoManual, TotalCancelado, \n";
                s = s + "       TotalCanceladoCliente, TotalReprog, TotalBloqueioFinanc, Cancelamento, BloqueioFinanceiro, \n";
                s = s + "       TotalReprogImpossivelAtender, TotalReprogFlexibilidade, TotalReprogSolicitacaoCliente, \n";
                s = s + "       IndiceReprog, TotalReprogOutros, IndiceReprogOutros \n";
                s = s + "from   Indicadores \n";
                s = s + "order by " + pOrdem;
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
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

        public DataTable PreencheDataTable(string pOrdem, string pFiltro, string pData)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial, Data, Cumprimento, ImpossivelAtender, FlexibilidadeContrato, SolicitacaoCliente, \n";
                s = s + "       ProgramacaoAutomatica, CumprimentoManual, TotalProgramadoAutomatico, TotalProgramadoManual, \n";
                s = s + "       TotalProgramado, TotalExecutado, TotalExecutadoAutomatico, TotalExecutadoManual, TotalCancelado, \n";
                s = s + "       TotalCanceladoCliente, TotalReprog, TotalBloqueioFinanc, Cancelamento, BloqueioFinanceiro, \n";
                s = s + "       TotalReprogImpossivelAtender, TotalReprogFlexibilidade, TotalReprogSolicitacaoCliente, \n";
                s = s + "       IndiceReprog, TotalReprogOutros, IndiceReprogOutros \n";
                s = s + "from   Indicadores \n";
                s = s + "where " + pData + " like '%" + pFiltro + "%' \n";
                s = s + "order by " + pOrdem +" \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
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

        public string Excluir(int pSequencial)
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
                if (pSequencial > 0)
                {
                    s = s + "delete from Indicadores ";
                    s = s + "where  Sequencial = " + pSequencial.ToString();
                }
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
                return string.Empty;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return ex.Message.ToString();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }

        public void CalculaIndicadores(clsIndicadores oIndicadores, DataTable pdtExecutados, DataTable pdtProgramados, string pData)
        {
            decimal executados, programacoes, impossib_atender, reprogs, reprogsflex, reprogsolicitcliente, progautom, progmanual,
                    execmanual, execautom, totalcancelados, totalcanceladosclientes, bloqfinanc, reprogoutros;

            executados = 0;
            programacoes = 0;
            impossib_atender = 0;
            reprogs = 0;
            reprogsflex = 0;
            reprogsolicitcliente = 0;
            reprogoutros = 0;
            progautom = 0;
            progmanual = 0;
            execmanual = 0;
            execautom = 0;
            totalcancelados = 0;
            totalcanceladosclientes = 0;
            bloqfinanc = 0;

            executados = pdtExecutados.Rows.Count;
            programacoes = executados + pdtProgramados.Rows.Count;

            // programados
            foreach (DataRow _dr in pdtProgramados.Rows)
            {
                if (_dr.RowState.ToString() != "Deleted")
                {
                    if (_dr["TipoProgramacao"].ToString()[0] == "A"[0])   // Programação Automática 
                        _dr["TipoProgramacao"] = "A";
                    else if (_dr["TipoProgramacao"].ToString()[0] == "1"[0])   // Programação Automática 
                        _dr["TipoProgramacao"] = "A";
                    else if (_dr["TipoProgramacao"].ToString()[0] == "M"[0])   // Programação Manual
                        _dr["TipoProgramacao"] = "M";
                    else if (_dr["TipoProgramacao"].ToString()[0] == "0"[0])
                        _dr["TipoProgramacao"] = "M";
                }
            }

            for (int i = 0; i < pdtProgramados.Rows.Count; i++)
            {
                if (pdtProgramados.Rows[i].RowState.ToString() != "Deleted")
                {
                    if ((pdtProgramados.Rows[i]["StatusCor"].ToString() == geral.RetornaCodigoCor("Amarelo") && pdtProgramados.Rows[i]["Observacao"].ToString().ToUpper().IndexOf("SOLICITAÇÃO CLIENTE") >= 0) ||
                    (pdtProgramados.Rows[i]["StatusCor"].ToString() == geral.RetornaCodigoCor("Vermelho") && pdtProgramados.Rows[i]["Observacao"].ToString().ToUpper().IndexOf("SOLICITAÇÃO CLIENTE") >= 0) ||
                     pdtProgramados.Rows[i]["Observacao"].ToString().ToUpper().IndexOf("BLOQUEIO FINANCEIRO") >= 0)
                    {
                        programacoes = programacoes - 1;
                    }

                    if (pdtProgramados.Rows[i]["Observacao"].ToString().IndexOf("BLOQUEIO FINANCEIRO") >= 0)
                        bloqfinanc = bloqfinanc + 1;

                    if (pdtProgramados.Rows[i]["StatusCor"].ToString() == geral.RetornaCodigoCor("Vermelho"))
                        reprogs = reprogs + 1;

                    if (pdtProgramados.Rows[i]["StatusCor"].ToString() == geral.RetornaCodigoCor("Vermelho") && pdtProgramados.Rows[i]["Observacao"].ToString().IndexOf("IMPOSSIBILIDADE DE ATENDIMENTO") >= 0)
                        impossib_atender = impossib_atender + 1;
                    else if (pdtProgramados.Rows[i]["StatusCor"].ToString() == geral.RetornaCodigoCor("Vermelho") && pdtProgramados.Rows[i]["Observacao"].ToString().IndexOf("FLEXIBILIDADE DO CONTRATO") >= 0)
                        reprogsflex = reprogsflex + 1;
                    else if (pdtProgramados.Rows[i]["StatusCor"].ToString() == geral.RetornaCodigoCor("Vermelho") && pdtProgramados.Rows[i]["Observacao"].ToString().IndexOf("SOLICITAÇÃO CLIENTE") >= 0)
                        reprogsolicitcliente = reprogsolicitcliente + 1;
                    else if (pdtProgramados.Rows[i]["StatusCor"].ToString() == geral.RetornaCodigoCor("Vermelho"))
                        reprogoutros = reprogoutros + 1;

                    if (pdtProgramados.Rows[i]["TipoProgramacao"].ToString() == "A" && pdtProgramados.Rows[i]["StatusCor"].ToString() != geral.RetornaCodigoCor("AzulClaro"))
                        progautom = progautom + 1;

                    if (pdtProgramados.Rows[i]["StatusCor"].ToString() == geral.RetornaCodigoCor("Cinza"))
                    {
                        if (pdtProgramados.Rows[i]["TipoProgramacao"].ToString() == "M" && pData == pdtProgramados.Rows[i]["DataProgramada"].ToString())
                        {
                            progmanual = progmanual + 1;
                        }
                    }
                    if (pdtProgramados.Rows[i]["TipoProgramacao"].ToString() == "M" && pdtProgramados.Rows[i]["StatusCor"].ToString() == geral.RetornaCodigoCor("Vermelho"))
                    {
                        progmanual = progmanual + 1;
                    }
                    if (pdtProgramados.Rows[i]["TipoProgramacao"].ToString() == "M" && pdtProgramados.Rows[i]["StatusCor"].ToString() == geral.RetornaCodigoCor("Amarelo"))
                    {
                        progmanual = progmanual + 1;
                    }

                    if (pdtProgramados.Rows[i]["StatusCor"].ToString() == geral.RetornaCodigoCor("Amarelo") ||
                        pdtProgramados.Rows[i]["StatusCor"].ToString() == "65280")
                        totalcancelados = totalcancelados + 1;

                    if (pdtProgramados.Rows[i]["StatusCor"].ToString() == geral.RetornaCodigoCor("Amarelo") && pdtProgramados.Rows[i]["Observacao"].ToString().IndexOf("SOLICITAÇÃO CLIENTE") >= 0)
                        totalcanceladosclientes = totalcanceladosclientes + 1;
                }
            }

            // executados 
            foreach (DataRow _dr in pdtExecutados.Rows)
            {
                if (_dr.RowState.ToString() != "Deleted")
                {
                    if (_dr["TipoProgramacao"].ToString() == "1") // Programação Automática 
                        _dr["TipoProgramacao"] = "A";
                    else if (_dr["TipoProgramacao"].ToString() == "2") // Programação Manual 
                        _dr["TipoProgramacao"] = "M";
                    else if (_dr["TipoProgramacao"].ToString()[0] == "A"[0]) // Programação Automática 
                        _dr["TipoProgramacao"] = "A";
                    else if (_dr["TipoProgramacao"].ToString()[0] == "1"[0]) // Programação Automática 
                        _dr["TipoProgramacao"] = "A";
                    else if (_dr["TipoProgramacao"].ToString()[0] == "M"[0]) // Programação Manual 
                        _dr["TipoProgramacao"] = "M";
                    else if (_dr["TipoProgramacao"].ToString()[0] == "0"[0]) // Programação Manual 
                        _dr["TipoProgramacao"] = "M";

                    if (_dr["NomeMotoristaOuDescricao"].ToString() != "")
                        _dr["NomeMotorista"] = _dr["NomeMotoristaOuDescricao"];
                }
            }

            for (int i = 0; i < pdtExecutados.Rows.Count; i++)
            {
                if (pdtExecutados.Rows[i]["TipoProgramacao"].ToString() == "A")
                {
                    progautom = progautom + 1;
                    execautom = execautom + 1;
                }
                if (pdtExecutados.Rows[i]["TipoProgramacao"].ToString() == "M")
                {
                    progmanual = progmanual + 1;
                    execmanual = execmanual + 1;
                }
            }

            oIndicadores.TotalProgramado = progmanual + progautom;
            oIndicadores.TotalProgramadoAutomatico = progautom;
            oIndicadores.TotalProgramadoManual = progmanual;
            oIndicadores.TotalExecutado = execautom + execmanual;
            oIndicadores.TotalExecutadoAutomatico = execautom;
            oIndicadores.TotalExecutadoManual = execmanual;
            oIndicadores.TotalReprog = reprogs;
            oIndicadores.TotalBloqueioFinanc = bloqfinanc;


            // Indice de Cumprimento
            if (oIndicadores.TotalProgramado > 0 )
                oIndicadores.Cumprimento = Math.Round(executados / oIndicadores.TotalProgramado * 100, 2);

            if (reprogs > 0)
            {
                oIndicadores.ImpossivelAtender = impossib_atender / oIndicadores.TotalProgramado * 100;
                oIndicadores.FlexibilidadeContrato = reprogsflex / oIndicadores.TotalProgramado * 100;
                oIndicadores.SolicitacaoCliente = reprogsolicitcliente / oIndicadores.TotalProgramado * 100;
                oIndicadores.BloqueioFinanceiro = bloqfinanc / oIndicadores.TotalProgramado * 100;
            }

            if ((progautom + progmanual) > 0)
            {
                oIndicadores.ProgramacaoAutomatica = progautom / (progautom + progmanual) * 100;
                oIndicadores.Cancelamento = totalcancelados / (progautom + progmanual) * 100;
            }
            oIndicadores.TotalCanceladoCliente = totalcanceladosclientes;
            oIndicadores.TotalCancelado = totalcancelados;

            oIndicadores.TotalReprogImpossivelAtender = impossib_atender;
            oIndicadores.TotalReprogFlexibilidade = reprogsflex;
            oIndicadores.TotalReprogSolicitacaoCliente = reprogsolicitcliente;
            oIndicadores.TotalReprogOutros = reprogoutros;

            if ((progautom + progmanual) > 0)
                oIndicadores.IndiceReprog = reprogs / (progautom + progmanual) * 100;

            if ((progautom + progmanual) > 0)
                oIndicadores.IndiceReprogOutros = oIndicadores.TotalReprogOutros / (progautom + progmanual) * 100;

            if ((execmanual + progmanual) > 0)
                oIndicadores.CumprimentoManual = execmanual / progmanual * 100;
        }
    }
}