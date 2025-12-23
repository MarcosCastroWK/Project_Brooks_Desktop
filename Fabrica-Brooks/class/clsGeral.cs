using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace LibSILC
{
    public class clsGeral
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
        public DataTable ConsultaQQ(string pSql)
        {
            try
            {
                oDB.ConectaMySql();
                s = pSql;
                FillDataSet();
            }
            catch (Exception ex)
            {
                return l_dt;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return l_dt;
        }
    }
}