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
    public class wp_FilesClientesDados
	{
	    private clsDB oDB = new clsDB();
        private MySqlDataAdapter l_myData;
        private DataSet l_ds = new DataSet();
        DataTable l_dt = new DataTable();
        private string s;

        private void FillDataSet()
        {
            l_ds = new DataSet();
            l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
            l_myData.Fill(l_ds);
            l_dt = l_ds.Tables[0];
        }
        
        public DataTable PegaDados(string pCliCode, geral.DOCUMENTO pDocumento = geral.DOCUMENTO.Nenhum, string pInicialCliente = "")
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select distinct file, apuracao as periodo, type from \n";
                s = s + "( \n";
                if (pDocumento == geral.DOCUMENTO.CDF)
                {
                    s = s + "  select ' CDF Click aqui para ir na página do IMA - a partir de 12/2016' as file, '' as apuracao, 'CDF' as type \n";
                    s = s + "  union all \n";
                    s = s + "  select ' CDF Click aqui para emitir CDF do RCD a partir de 07/20' as file, '07/20' as apuracao, 'CDF' as type \n";
                    s = s + "  union all \n";
                }
                s = s + "  select file, periodo as apuracao, type \n";
                s = s + "  from   wp_brooks_files \n";
                s = s + "  where  cli_code = " + Convert.ToInt32(pCliCode) + " and type in ('alvarafunc','alvarasanit','certiso', 'LAO', 'ctfibama') \n";
                s = s + "  union all \n";
                s = s + "  select file, concat(right(left(file, 19), 2), '/', right(left(file, 21), 2)) as apuracao, type \n";
                s = s + "  from   wp_brooks_files \n";
                s = s + "  where  cli_code = " + Convert.ToInt32(pCliCode) + " and type in ('CDF','DDR','RGR') \n";
                s = s + ") x \n";
                if (pDocumento == geral.DOCUMENTO.RGR)
                {                    
                    s = s + "where  type = '" + geral.DOCUMENTO.RGR.ToString() + "' \n";
                    //s = s + "and    right(left(file, 7), 3) = '" + pInicialCliente + "' \n";
                }
                if (pDocumento == geral.DOCUMENTO.ALVARA)
                    s = s + "where  type in ('alvarasanit', 'alvarafunc') \n";
                if (pDocumento == geral.DOCUMENTO.CDF)
                {
                    s = s + "where  (right(left(file, 7), 3) = '" + pInicialCliente + "' or left(file, 4) = ' CDF') \n";
                    s = s + "and    type = '" + geral.DOCUMENTO.CDF.ToString() + "' \n";
                }
                if (pDocumento == geral.DOCUMENTO.CERTISO)
                    s = s + "where  type = '" + geral.DOCUMENTO.CERTISO.ToString() + "' \n";
                if (pDocumento == geral.DOCUMENTO.DDR)
                {
                    s = s + "where  right(left(file, 7), 3) = '" + pInicialCliente + "' \n";
                    s = s + "and    type = '" + geral.DOCUMENTO.DDR.ToString() + "' \n";
                }
                if (pDocumento == geral.DOCUMENTO.LAO)
                    s = s + "where  type = '" + geral.DOCUMENTO.LAO.ToString() + "' \n";
                if (pDocumento == geral.DOCUMENTO.CTFIBAMA)
                    s = s + "where  type = '" + geral.DOCUMENTO.CTFIBAMA.ToString() + "' \n";
                s = s + "order  by type, right(left(file, 21), 2) desc, right(left(file, 19), 2) desc \n";
                FillDataSet();
            }
            catch (Exception ex)
            {
                s = ex.Message;
                l_dt = new DataTable();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return l_dt;
        }
        public int PegaTamanhoCampoVarChar(string pCampo)
        {
            int iRet = 0;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "show  columns ";
                s = s + "from  wp_brooks_files ";
                s = s + "where type like 'varchar%' and field = '" + pCampo + "' ";
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
                oDB.DesconectaMySql();
            }
            return iRet;
        }
        
        public string Excluir(string pDescricaoDocumento)
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
                s = s + "delete from wp_brooks_files ";
                s = s + "where  file = '" + pDescricaoDocumento + "' \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return "";
        }
        public string Excluir(int pId)
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
                s = s + "delete from wp_brooks_files ";
                s = s + "where  Id = " + pId + " \n";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return "";
        }
        public string Incluir(string pDescricaoDocumento, string pTipo, int pCodigoCliente, string pTitle, string pPeriodo)
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
                s = s + "insert into wp_brooks_files \n";
                s = s + "(cli_code, file, type, title, periodo, description) \n";
                s = s + "values \n";
                s = s + "(";
                s = s + pCodigoCliente.ToString("000000") + ", ";
                s = s + " '" + pDescricaoDocumento + "', '" + pTipo + "', '" + pTitle + "', '" + pPeriodo + "', ''" ;
                s = s + ")";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return "";
        }
        public string DadoExiste(int pId)
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Id ";
                s = s + "from   wp_brooks_files ";
                if (pId != 0)
                {
                    s = s + "where  Id = " + pId + " ";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pId != 0)
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
        public bool DadoExiste(int pCodigoCliente, string pDescricao)
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Id ";
                s = s + "from   wp_brooks_files ";
                s = s + "where  cli_code = " + pCodigoCliente + " ";
                s = s + "and    file = '" + pDescricao + "'";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
        public DataTable PreencheDt(string pCliCode, string pTipo, string pOrdem = "")
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Id, cli_code as CodigoCliente, type as Tipo, file as Descricao, periodo, NomeFantasia \n";
                s = s + "from   wp_brooks_files f \n";
                s = s + "inner  join clientes c on c.Codigo = f.cli_code \n";
                s = s + "where  cli_code = " + Convert.ToInt32(pCliCode) + " \n";
                s = s + "and    type = '" + pTipo + "' \n";
                if (pOrdem == "")
                    s = s + "order  by type, right(left(file, 21), 2) desc, right(left(file, 19), 2) desc \n";
                else
                    s = s + "order  by " + pOrdem + " \n";
                FillDataSet();
                return l_dt;
            }
            catch (Exception ex)
            {
                s = ex.Message;
                return new DataTable();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
    }
}