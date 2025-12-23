using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.OleDb;
using SILCNegocios;

namespace LibSILC
{
    public class clsMTReGeradoDados
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
        public clsMTReGerado PegaDados(clsMTReGerado pMTReGerado, int pSequencial)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial, DataEmissao, CNPJ_CPF_Armazenador, CNPJ_CPF_Transportador, CNPJ_CPF_Destinador, CNPJ_CPF_Gerador, CodigoCliente, NumeroMTRe, \n";
                s = s + "       NomeResponsavel, CargoResponsavel, DataTransporte, CodigoMotorista, PlacaVeiculo, Observacao \n";
                s = s + "from   MTReGerado \n";
                if (pSequencial > 0)
                {
                    s = s + "where  Codigo = " + pSequencial + " \n";
                    s = s + "limit 1";
                }
                else if (pSequencial == 0)
                {
                    s = s + " order by Codigo limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows[0]["Sequencial"].ToString() != "")
                        pMTReGerado.Sequencial = Convert.ToInt32(l_dt.Rows[0]["Sequencial"]);
                    pMTReGerado.DataEmissao = l_dt.Rows[0]["DataEmissao"].ToString();
                    pMTReGerado.CNPJ_CPF_Armazenador = l_dt.Rows[0]["CNPJ_CPF_Armazenador"].ToString();
                    pMTReGerado.CNPJ_CPF_Transportador = l_dt.Rows[0]["CNPJ_CPF_Transportador"].ToString();
                    pMTReGerado.CNPJ_CPF_Destinador = l_dt.Rows[0]["CNPJ_CPF_Destinador"].ToString();
                    pMTReGerado.CNPJ_CPF_Gerador = l_dt.Rows[0]["CNPJ_CPF_Gerador"].ToString();
                    if (l_dt.Rows[0]["CodigoCliente"].ToString() != "")
                        pMTReGerado.CodigoCliente = Convert.ToInt32(l_dt.Rows[0]["CodigoCliente"]);
                    pMTReGerado.NumeroMTRe = l_dt.Rows[0]["NumeroMTRe"].ToString();
                    pMTReGerado.NomeResponsavel = l_dt.Rows[0]["NomeResponsavel"].ToString();
                    pMTReGerado.CargoResponsavel = l_dt.Rows[0]["CargoResponsavel"].ToString();
                    pMTReGerado.DataTransporte = l_dt.Rows[0]["DataTransporte"].ToString();
                    if (l_dt.Rows[0]["CodigoMotorista"].ToString() != "")
                        pMTReGerado.CodigoMotorista = Convert.ToInt32(l_dt.Rows[0]["CodigoMotorista"]);
                    pMTReGerado.PlacaVeiculo = l_dt.Rows[0]["PlacaVeiculo"].ToString();
                    pMTReGerado.Observacao = l_dt.Rows[0]["Observacao"].ToString();
                }
                return pMTReGerado;
            }
            catch (Exception ex)
            {
                return new clsMTReGerado();
            }
            finally
            {
                DesconectaBanco();
            }
        }
        
        public DataTable PegaDados(clsMTReGerado pMTReGerado, int pSequencial, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial, DataEmissao, CNPJ_CPF_Armazenador, CNPJ_CPF_Transportador, CNPJ_CPF_Destinador, CNPJ_CPF_Gerador, CodigoCliente, NumeroMTRe, \n";
                s = s + "       NomeResponsavel, CargoResponsavel, DataTransporte, CodigoMotorista, PlacaVeiculo, Observacao \n";
                s = s + "from   MTReGerado \n";
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
                    s = s + " order by NumeroMTRe ";
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
        public int PegaTamanhoCampoVarChar(string pCampo)
        {
            int iRet = 0;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "show columns ";
                s = s + "from MTReGerado ";
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
        public string DadoExiste(int pSequencial)
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Sequencial ";
                s = s + "from   MTReGerado ";
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
        public void Inserir(clsMTReGerado pMTReGerado, int pSequencial = 0)
        {
            try
            {
                s = "";
                s = s + "insert into MTReGerado \n";
                s = s + "( \n";
                if (pSequencial > 0)
                    s = s + "  Sequencial, \n";
                s = s + " DataEmissao, CNPJ_CPF_Armazenador, CNPJ_CPF_Transportador, CNPJ_CPF_Destinador, CNPJ_CPF_Gerador, CodigoCliente, NumeroMTRe, \n";
                s = s + " NomeResponsavel, CargoResponsavel, DataTransporte, CodigoMotorista, PlacaVeiculo, Observacao \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pSequencial > 0)
                    s = s + pSequencial.ToString() + ", \n";
                s = s + " '" + Convert.ToDateTime(pMTReGerado.DataEmissao).ToString("yyyy-MM-dd") + "', \n";
                s = s + " '" + pMTReGerado.CNPJ_CPF_Armazenador + "', \n";
                s = s + " '" + pMTReGerado.CNPJ_CPF_Transportador + "', \n";
                s = s + " '" + pMTReGerado.CNPJ_CPF_Destinador + "', \n";
                s = s + " '" + pMTReGerado.CNPJ_CPF_Gerador + "', \n";
                if (pMTReGerado.CodigoCliente > 0)
                    s = s + " " + pMTReGerado.CodigoCliente.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                s = s + " '" + pMTReGerado.NumeroMTRe + "', \n";
                s = s + " '" + pMTReGerado.NomeResponsavel + "', \n";
                s = s + " '" + pMTReGerado.CargoResponsavel + "', \n";
                s = s + " '" + Convert.ToDateTime(pMTReGerado.DataTransporte).ToString("yyyy-MM-dd") + "', \n";
                if (pMTReGerado.CodigoMotorista > 0)
                    s = s + " " + pMTReGerado.CodigoMotorista.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                s = s + " '" + pMTReGerado.Observacao + "' \n";
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
        public string Alterar(clsMTReGerado pMTReGerado, int pSequencial)
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
                s = s + "update MTReGerado \n";                
                s = s + "set DataEmissao = '" + Convert.ToDateTime(pMTReGerado.DataEmissao) + "', \n";
                if (pMTReGerado.CodigoCliente > 0)
                    s = s + " CodigoCliente = " + pMTReGerado.CodigoCliente.ToString().Replace(",", ".") + " \n";
                else
                    s = s + " CodigoCliente = 0 \n";
                s = s + "where Sequencial = " + pSequencial + " \n";        
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
                s = s + "select Sequencial, DataEmissao, CNPJ_CPF_Armazenador, CNPJ_CPF_Transportador, CNPJ_CPF_Destinador, CNPJ_CPF_Gerador, CodigoCliente, NumeroMTRe, \n";
                s = s + "       NomeResponsavel, CargoResponsavel, DataTransporte, CodigoMotorista, PlacaVeiculo, Observacao \n";
                s = s + "from   MTReGerado \n";
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

        public DataTable PreencheDataTable(string pOrdem, string pFiltro, string pCampo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Sequencial, DataEmissao, CNPJ_CPF_Armazenador, CNPJ_CPF_Transportador, CNPJ_CPF_Destinador, CNPJ_CPF_Gerador, CodigoCliente, NumeroMTRe, \n";
                s = s + "       NomeResponsavel, CargoResponsavel, DataTransporte, CodigoMotorista, PlacaVeiculo, Observacao \n";
                s = s + "from   MTReGerado \n";
                s = s + "where " + pCampo + " like '%" + pFiltro + "%' \n";
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

        public string Excluir(int pSequencial = 0)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                if (pSequencial > 0)
                {
                    s = "";
                    s = s + "delete from MTReGerado ";
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
    }
}