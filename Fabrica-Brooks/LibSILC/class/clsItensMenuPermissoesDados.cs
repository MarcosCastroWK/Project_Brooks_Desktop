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
    public class clsItensMenuPermissoesDados
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

        public void InserirItensMenuPermissoes()
        {
            // criar tabela primeiro
        }
        public clsItensMenuPermissoes PegaDados(clsItensMenuPermissoes pItensMenuPermissoes, int pCodigo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, CodigoUsuario, CodigoItemMenu, Consultar, Incluir, Alterar, Excluir \n"; 
                s = s + "from   ItensMenuPermissoes \n";
                if (pCodigo > 0)
                {
                    s = s + "where Codigo = " + pCodigo + " \n";
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
                        pItensMenuPermissoes.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);

                    if (l_dt.Rows[0]["CodigoUsuario"].ToString() != "")
                        pItensMenuPermissoes.CodigoUsuario = Convert.ToInt32(l_dt.Rows[0]["CodigoUsuario"]);

                    if (l_dt.Rows[0]["CodigoItemMenu"].ToString() != "")
                        pItensMenuPermissoes.CodigoItensMenu = Convert.ToInt32(l_dt.Rows[0]["CodigoItemMenu"]);

                    if (l_dt.Rows[0]["Consultar"].ToString() != "")
                        pItensMenuPermissoes.Consultar = Convert.ToInt32(l_dt.Rows[0]["Consultar"]);

                    if (l_dt.Rows[0]["Incluir"].ToString() != "")
                        pItensMenuPermissoes.Incluir = Convert.ToInt32(l_dt.Rows[0]["Incluir"]);

                    if (l_dt.Rows[0]["Alterar"].ToString() != "")
                        pItensMenuPermissoes.Alterar = Convert.ToInt32(l_dt.Rows[0]["Alterar"]);

                    if (l_dt.Rows[0]["Excluir"].ToString() != "")
                        pItensMenuPermissoes.Excluir = Convert.ToInt32(l_dt.Rows[0]["Excluir"]);

                }
                return pItensMenuPermissoes;
            }
            catch (Exception ex)
            {
                return new clsItensMenuPermissoes();
            }
            finally
            {
                DesconectaBanco();
            }
        }
        
        public DataTable PegaDados(clsItensMenuPermissoes pItensMenuPermissoes, int pCodigo, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, CodigoUsuario, CodigoItemMenu, Consultar, Incluir, Alterar, Excluir \n"; 
                s = s + "from   ItensMenuPermissoes \n";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " \n";
                }
                if (pUltimoRegistro)
                {
                    s = s + "order by Codigo desc limit 1\n";
                }
                else if (pCodigo == 0)
                {
                    s = s + " order by Item \n";
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
        public int PegaTamanhoItemVarChar(string pItem)
        {
            int iRet = 0;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "show  columns ";
                s = s + "from  ItensMenuPermissoes ";
                s = s + "where type like 'varchar%' and field = '" + pItem + "' ";
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
                s = s + "select Codigo \n";
                s = s + "from   ItensMenuPermissoes \n";
                if (pCodigo != 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " \n";
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
        public void Inserir(clsItensMenuPermissoes pItensMenuPermissoes, int pCodigo = 0)
        {
            try
            {
                s = "";
                s = s + "insert into ItensMenuPermissoes \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + "  Codigo, \n";
                s = s + "  CodigoUsuario, CodigoItemMenu, Consultar, Incluir, Alterar, Excluir \n"; 
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + pCodigo.ToString() + ", \n";

                if (pItensMenuPermissoes.CodigoUsuario > 0)
                    s = s + " " + pItensMenuPermissoes.CodigoUsuario.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";

                if (pItensMenuPermissoes.CodigoItensMenu > 0)
                    s = s + " " + pItensMenuPermissoes.CodigoItensMenu.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";

                if (pItensMenuPermissoes.Consultar > 0)
                    s = s + " " + pItensMenuPermissoes.Consultar.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";

                if (pItensMenuPermissoes.Incluir > 0)
                    s = s + " " + pItensMenuPermissoes.Incluir.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";

                if (pItensMenuPermissoes.Alterar > 0)
                    s = s + " " + pItensMenuPermissoes.Alterar.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";

                if (pItensMenuPermissoes.Excluir > 0)
                    s = s + " " + pItensMenuPermissoes.Excluir.ToString().Replace(",", ".") + " \n";
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
        public string Alterar(clsItensMenuPermissoes pItensMenuPermissoes, int pCodigoItemMenu, int pCodigoUsuario)
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
                s = s + "update ItensMenuPermissoes \n";
                
                if (pItensMenuPermissoes.Consultar > 0)
                    s = s + "  set Consultar = " + pItensMenuPermissoes.Consultar.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "  set Consultar = 0, \n";

                if (pItensMenuPermissoes.Incluir > 0)
                    s = s + "  Incluir = " + pItensMenuPermissoes.Incluir.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "  Incluir = 0, \n";

                if (pItensMenuPermissoes.Alterar > 0)
                    s = s + "  Alterar = " + pItensMenuPermissoes.Alterar.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "  Alterar = 0, \n";

                if (pItensMenuPermissoes.Excluir > 0)
                    s = s + "  Excluir = " + pItensMenuPermissoes.Excluir.ToString().Replace(",", ".") + " \n";
                else
                    s = s + "  Excluir = 0 \n";

                s = s + "where CodigoItemMenu = " + pCodigoItemMenu + " \n";
                s = s + "and   CodigoUsuario  = " + pCodigoUsuario  + " \n";
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
        public DataTable PreencheDataTable(string pOrdem, int pCodigoUsuario)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select " + pCodigoUsuario + " as CodigoUsuario, im.Codigo as CodigoItemMenu, im.Item, \n";
                s = s + "       0 as Consultar, \n";
                s = s + "       0 as Incluir, \n";
                s = s + "       0 as Alterar,  \n";
                s = s + "       0 as Excluir  \n";
                s = s + "from ItensMenu im \n";
                s = s + "order  by " + pOrdem;
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

        public clsItensMenuPermissoes PegaPermissoesMenu(string pCodigoUsuario, string pCodigoItemMenu)
        {
            clsItensMenuPermissoes oRetIMP = new clsItensMenuPermissoes();
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Consultar, Incluir, Alterar, Excluir \n";
                s = s + "from   ItensMenuPermissoes \n";
                s = s + "where  CodigoUsuario  = " + pCodigoUsuario  + " \n";
                s = s + "and    CodigoItemMenu = " + pCodigoItemMenu + " \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                DesconectaBanco();
                if (l_ds.Tables[0].Rows.Count > 0)
                {
                    oRetIMP.Consultar = Convert.ToInt16(l_ds.Tables[0].Rows[0][0]);
                    if (l_ds.Tables[0].Rows[0][1].ToString() != "")
                        oRetIMP.Incluir = Convert.ToInt16(l_ds.Tables[0].Rows[0][1]);
                    oRetIMP.Alterar   = Convert.ToInt16(l_ds.Tables[0].Rows[0][2]);
                    oRetIMP.Excluir = Convert.ToInt16(l_ds.Tables[0].Rows[0][3]);
                }
            }
            catch (Exception ex)
            {
                DesconectaBanco();
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
            }
            return oRetIMP;
        }

        public bool ExistePermissaoMenu(string pCodigoUsuario, string pCodigoItemMenu)
        {
            bool bRet = false;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo \n";
                s = s + "from   ItensMenuPermissoes \n";
                s = s + "where  CodigoUsuario  = " + pCodigoUsuario + " \n";
                s = s + "and    CodigoItemMenu = " + pCodigoItemMenu + " \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                DesconectaBanco();
                if (l_ds.Tables[0].Rows.Count > 0)
                    bRet = true;
            }
            catch (Exception ex)
            {
                DesconectaBanco();
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
            }
            return bRet;
        }
        public DataTable PreencheDataTable(string pOrdem, string pFiltro, string pCampoWhere)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, CodigoUsuario, CodigoItemMenu, Consultar, Incluir, Alterar, Excluir \n";
                s = s + "from   ItensMenuPermissoes \n";
                s = s + "where " + pCampoWhere + " like '%" + pFiltro + "%' \n";
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
        public string Excluir(int pCodigo = 0)
        {
            string sRet = "";
            if (pCodigo > 0)
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
                    s = s + "delete from ItensMenuPermissoes ";
                    s = s + "where  Codigo = " + pCodigo.ToString();
                    command.CommandText = s;
                    command.ExecuteNonQuery();
                    transaction.Commit();
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
            }
            return sRet;
        }
    }
}