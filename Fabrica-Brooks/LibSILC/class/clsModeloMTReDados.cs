using System;
using System.Data;
using MySql.Data.MySqlClient;
using SILCNegocios;

namespace LibSILC
{
    public class clsModeloMTReDados
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
        public clsModeloMTRe PegaDados(clsModeloMTRe pModeloMTRe, int pCodigo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Nome, CNPJ_CPF_Armazenador, CNPJ_CPF_Transportador, CNPJ_CPF_Destinador, CNPJ_CPF_Gerador, CodigoDestinoFinal \n"; 
                s = s + "from   ModeloMTRe \n";
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
                        pModeloMTRe.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                    pModeloMTRe.CNPJ_CPF_Armazenador = l_dt.Rows[0]["CNPJ_CPF_Armazenador"].ToString();
                    pModeloMTRe.CNPJ_CPF_Destinador = l_dt.Rows[0]["CNPJ_CPF_Destinador"].ToString();
                    pModeloMTRe.CNPJ_CPF_Gerador = l_dt.Rows[0]["CNPJ_CPF_Gerador"].ToString();
                    pModeloMTRe.CNPJ_CPF_Transportador = l_dt.Rows[0]["CNPJ_CPF_Transportador"].ToString();
                    pModeloMTRe.Nome = l_dt.Rows[0]["Nome"].ToString();
                    if (l_dt.Rows[0]["CodigoDestinoFinal"].ToString() != "")
                        pModeloMTRe.CodigoDestinoFinal = Convert.ToInt32(l_dt.Rows[0]["CodigoDestinoFinal"].ToString());
                }
                return pModeloMTRe;
            }
            catch (Exception ex)
            {
                return new clsModeloMTRe();
            }
            finally
            {
                DesconectaBanco();
            }
        }
        
        public DataTable PegaDados(int pCodigo, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Nome, CNPJ_CPF_Armazenador, CNPJ_CPF_Transportador, CNPJ_CPF_Destinador, CNPJ_CPF_Gerador, CodigoDestinoFinal \n"; 
                s = s + "from   ModeloMTRe \n";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " ";
                }
                if (pUltimoRegistro)
                {
                    s = s + "order by Codigo desc limit 1 ";
                }
                else if (pCodigo == 0)
                {
                    s = s + " order by Nome ";
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
                s = s + "from ModeloMTRe ";
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
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Codigo ";
                s = s + "from   ModeloMTRe ";
                if (pCodigo != 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " ";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pCodigo != 0)
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
        public void Inserir(clsModeloMTRe pModeloMTRe, int pCodigo = 0)
        {
            try
            {
                s = "";
                s = s + "insert into ModeloMTRe \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + "  Codigo, \n";
                s = s + "  Nome, CNPJ_CPF_Armazenador, CNPJ_CPF_Transportador, CNPJ_CPF_Destinador, CNPJ_CPF_Gerador, CodigoDestinoFinal \n"; 
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + pCodigo.ToString() + ", \n";
                s = s + " '" + pModeloMTRe.Nome + "', \n";
                s = s + " '" + pModeloMTRe.CNPJ_CPF_Armazenador + "', \n";
                s = s + " '" + pModeloMTRe.CNPJ_CPF_Transportador + "', \n";
                s = s + " '" + pModeloMTRe.CNPJ_CPF_Destinador + "', \n";
                s = s + " '" + pModeloMTRe.CNPJ_CPF_Gerador + "', \n";
                s = s + "  " + pModeloMTRe.CodigoDestinoFinal + " \n";
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
        public string Alterar(clsModeloMTRe pModeloMTRe, int pCodigo)
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
                s = s + "update ModeloMTRe \n";
                s = s + "set Nome = '" + pModeloMTRe.Nome + "', \n";
                s = s + "    CNPJ_CPF_Armazenador = '" + pModeloMTRe.CNPJ_CPF_Armazenador + "', \n";
                s = s + "    CNPJ_CPF_Transportador = '" + pModeloMTRe.CNPJ_CPF_Transportador + "', \n";
                s = s + "    CNPJ_CPF_Destinador = '" + pModeloMTRe.CNPJ_CPF_Destinador + "', \n";
                s = s + "    CNPJ_CPF_Gerador = '" + pModeloMTRe.CNPJ_CPF_Gerador + "', \n";
                s = s + "    CodigoDestinoFinal = " + pModeloMTRe.CodigoDestinoFinal + " \n";
                s = s + "where Codigo = " + pCodigo + " \n";        
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
                s = s + "select * from ( \n";
                s = s + "  select mm.Codigo, mm.Nome, mm.CNPJ_CPF_Armazenador, mm.CNPJ_CPF_Transportador, a.Nome as NomeTransportador, mm.CNPJ_CPF_Destinador, mm.CNPJ_CPF_Gerador \n";
                s = s + "  from   ModeloMTRe mm \n";
                s = s + "  inner  join Aterro a on mm.CNPJ_CPF_Transportador = a.CNPJ \n";
                s = s + "  union all \n";
                s = s + "  select mm.Codigo, mm.Nome, mm.CNPJ_CPF_Armazenador, mm.CNPJ_CPF_Transportador, c.Nome as NomeTransportador, mm.CNPJ_CPF_Destinador, mm.CNPJ_CPF_Gerador \n";
                s = s + "  from   ModeloMTRe mm \n";
                s = s + "  inner  join Clientes c on mm.CNPJ_CPF_Transportador = c.CNPJ_CPF \n";
                s = s + ") x \n";
                s = s + "group by Codigo \n";
                s = s + "order by Nome \n";
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
                s = s + "select mm.Codigo, mm.Nome, mm.CNPJ_CPF_Armazenador, mm.CNPJ_CPF_Transportador, mm.CNPJ_CPF_Destinador, \n";
                s = s + "       mm.CNPJ_CPF_Gerador, mr.CodigoModeloMTRe, mm.CodigoDestinoFinal \n";
                s = s + "from   ModeloMTRe mm \n";
                s = s + "inner  join ModeloMTReResiduos mr on mr.CodigoModeloMTRe = mm.Codigo \n";
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
        public DataTable PreencheDataTable(string pCodigoResiduo, string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select mm.Codigo, mm.Nome, mm.CNPJ_CPF_Armazenador, mm.CNPJ_CPF_Transportador, mm.CNPJ_CPF_Destinador, mm.CNPJ_CPF_Gerador, CodigoModeloMTRe, mm.CodigoDestinoFinal  \n";
                s = s + "from   ModeloMTRe mm\n";
                s = s + "inner  join ModeloMTReResiduos mr on mr.CodigoModeloMTRe = mm.Codigo \n";
                s = s + "where  CodigoResiduo = " + pCodigoResiduo + " \n";
                s = s + "order by " + pOrdem + " \n";
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

        public int PegaUltimoCodigo()
        {
            int iRet = 0;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Codigo \n";
                s = s + "from   ModeloMTRe \n";
                s = s + "order  by Codigo desc limit 1 \n ";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    return Convert.ToInt32(l_dt.Rows[0][0]);
            }
            catch 
            {
                return 0;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return iRet;
        }
        public string Excluir(int pCodigo = 0)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                if (pCodigo > 0)
                {
                    s = "";
                    s = s + "delete from ModeloMTRe ";
                    s = s + "where  Codigo = " + pCodigo.ToString();
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