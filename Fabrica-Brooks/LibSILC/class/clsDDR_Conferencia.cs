using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using SILCNegocios;

namespace LibSILC
{
    public class clsDDR_Conferencia
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

        public bool MesEmConferencia(int pCodigoCliente, int pMes, int pAno)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select * \n";
                s = s + "from   DDR_Conferencia \n";
                if (pCodigoCliente > 0)
                {
                    s = s + "where CodigoCliente = " + pCodigoCliente + " \n";
                    s = s + "and   Mes = " + pMes + " \n";
                    s = s + "and   Ano = " + pAno.ToString() + " \n";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows.Count > 0)
                    {
                        if (l_dt.Rows[0]["CodigoCliente"].ToString() != "")
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                DesconectaBanco();
            }
        }
        public bool MesLiberado(int pCodigoCliente, int pMes, int pAno)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select * \n";
                s = s + "from   DDR_Conferencia \n";
                if (pCodigoCliente > 0)
                {
                    s = s + "where CodigoCliente = " + pCodigoCliente + " \n";
                    s = s + "and   Mes = " + pMes + " \n";
                    s = s + "and   Ano = " + pAno.ToString() + " \n";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows.Count > 0)
                    {
                        if (l_dt.Rows[0]["CodigoCliente"].ToString() != "")
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                DesconectaBanco();
            }
        }

        public void Inserir(int pCodigoCliente, int pMes, int pAno)
        {
            try
            {
                s = "";
                s = s + "insert into DDR_Conferencia \n";
                s = s + "( \n";
                s = s + "  DDR_Conferindo, CodigoCliente, Mes, Ano \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                s = s + " 1, " + pCodigoCliente + ", \n";
                s = s + " " + pMes + ", \n";
                s = s + " " + pAno + " \n";
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

        public string Excluir(int pCodigoCliente, int pMes, int pAno)
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
                s = s + "delete from DDR_Conferencia ";
                s = s + "where  CodigoCliente = " + pCodigoCliente + " \n";
                s = s + "and    Mes = " + pMes + " \n";
                s = s + "and    Ano = " + pAno.ToString() + " \n";
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
                oDB.MySqlConnect.Close();
            }
        }
    }

}
