using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using MySql.Data.MySqlClient;
using System.Data.OleDb;
using SILCNegocios;

namespace LibSILC
{
    public partial class clsDiasDeColetaDados
    {
        private clsDB oDB = new clsDB();
        private MySqlDataAdapter l_myData;
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

        public string DadoExiste(int pCodigoContrato, int pCodigoResiduo, string pDataReajuste, int pCodigoCliente, int pDia)
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select * from DiasDeColeta \n";
                s = s + "where CodigoContrato = " + pCodigoContrato + " \n";
                s = s + "and   CodigoResiduo  = " + pCodigoResiduo + " \n";
                s = s + "and   CodigoCliente  = " + pCodigoCliente + " \n";
                s = s + "and   Dia            = " + pDia + " \n";
                s = s + "and   DataReajuste = '" + Convert.ToDateTime(pDataReajuste).ToString("yyyy-MM-dd") + "' \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pCodigoContrato > 0 && pCodigoResiduo > 0 && pDataReajuste != "")
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

        public void Inserir(clsDiasDeColeta  pDiasDeColeta)
        {
            try
            {
                s = "";
                s = s + "insert into DiasDeColeta \n";
                s = s + "( \n";
                s = s + "   CodigoContrato, CodigoCliente, CodigoResiduo, DataReajuste, Dia \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                s = s + pDiasDeColeta.CodigoContrato.ToString() + ", \n";
                s = s + pDiasDeColeta.CodigoCliente.ToString()  + ", \n";
                s = s + pDiasDeColeta.CodigoResiduo.ToString()  + ", \n";
                if (pDiasDeColeta.DataReajuste == "" || pDiasDeColeta.DataReajuste == "0001-01-01" || pDiasDeColeta.DataReajuste == "0100-01-01")
                    s = s + "'0001-01-01', \n";
                else
                    s = s + "'" + Convert.ToDateTime(pDiasDeColeta.DataReajuste).ToString("yyyy-MM-dd") + "', \n";
                s = s + " " + pDiasDeColeta.Dia + " \n";
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
                if (pSequencial > 0)
                {
                    s = "";
                    s = s + "delete from DiasDeColeta ";
                    s = s + "where  Sequencial = " + pSequencial.ToString();
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
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