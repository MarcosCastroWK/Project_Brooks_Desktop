using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.OleDb;
using SILCNegocios;

namespace LibSILC
{
    public class clsModeloMTReResiduosDados
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
        public clsModeloMTReResiduos PegaDados(clsModeloMTReResiduos pModeloMTReResiduos, int pCodigo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, CodigoModeloMTRe, CodigoResiduo, CodigoEstadoFisico, CodigoClasse, \n"; 
                s = s + "       CodigoAcondicionamento, CodigoTecnologia, NumeroONU, ClasseRisco, NomeEmbarque, GrupoEmbalagem, CodigoUnidade \n"; 
                s = s + "from   ModeloMTReResiduos \n";
                if (pCodigo > 0)
                {
                    s = s + "where  CodigoModeloMTRe = " + pCodigo + " \n";
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
                        pModeloMTReResiduos.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                    if (l_dt.Rows[0]["CodigoModeloMTRe"].ToString() != "")
                        pModeloMTReResiduos.CodigoModeloMTRe = Convert.ToInt32(l_dt.Rows[0]["CodigoModeloMTRe"]);
                    if (l_dt.Rows[0]["CodigoResiduo"].ToString() != "")
                        pModeloMTReResiduos.CodigoResiduo = Convert.ToInt32(l_dt.Rows[0]["CodigoResiduo"]);
                    if (l_dt.Rows[0]["CodigoEstadoFisico"].ToString() != "")
                        pModeloMTReResiduos.CodigoEstadoFisico = Convert.ToInt32(l_dt.Rows[0]["CodigoEstadoFisico"]);
                    if (l_dt.Rows[0]["CodigoClasse"].ToString() != "")
                        pModeloMTReResiduos.CodigoClasse = Convert.ToInt32(l_dt.Rows[0]["CodigoClasse"]);
                    if (l_dt.Rows[0]["CodigoAcondicionamento"].ToString() != "")
                        pModeloMTReResiduos.CodigoAcondicionamento = Convert.ToInt32(l_dt.Rows[0]["CodigoAcondicionamento"]);
                    if (l_dt.Rows[0]["CodigoTecnologia"].ToString() != "")
                        pModeloMTReResiduos.CodigoTecnologia = Convert.ToInt32(l_dt.Rows[0]["CodigoTecnologia"]);
                    pModeloMTReResiduos.NumeroONU = l_dt.Rows[0]["NumeroONU"].ToString();
                    pModeloMTReResiduos.ClasseRisco = l_dt.Rows[0]["ClasseRisco"].ToString();
                    pModeloMTReResiduos.NomeEmbarque = l_dt.Rows[0]["NomeEmbarque"].ToString();
                    pModeloMTReResiduos.GrupoEmbalagem = l_dt.Rows[0]["GrupoEmbalagem"].ToString();
                    if (l_dt.Rows[0]["CodigoUnidade"].ToString() != "")
                        pModeloMTReResiduos.CodigoUnidade = Convert.ToInt32(l_dt.Rows[0]["CodigoUnidade"]);
                }
                return pModeloMTReResiduos;
            }
            catch (Exception ex)
            {
                return new clsModeloMTReResiduos();
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
                s = s + "select Codigo, CodigoModeloMTRe, CodigoResiduo, CodigoEstadoFisico, CodigoClasse, \n";
                s = s + "       CodigoAcondicionamento, CodigoTecnologia, NumeroONU, ClasseRisco, NomeEmbarque, GrupoEmbalagem, CodigoUnidade \n";
                s = s + "from   ModeloMTReResiduos \n";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " \n";
                }
                if (pUltimoRegistro)
                {
                    s = s + "order by Codigo desc limit 1 \n ";
                }
                else if (pCodigo == 0)
                {
                    s = s + " order by CodigoModeloMTRe \n";
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
                s = s + "from ModeloMTReResiduos ";
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
                s = s + "from   ModeloMTReResiduos ";
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

        public string DadoExiste(int pCodigo, int pCodigoResiduo)
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Codigo ";
                s = s + "from   ModeloMTReResiduos ";
                if (pCodigo != 0 && pCodigoResiduo != 0)
                {
                    s = s + "where  CodigoModeloMTRe = " + pCodigo + " \n";
                    s = s + "and    CodigoResiduo = " + pCodigoResiduo + " \n";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pCodigo != 0 && pCodigoResiduo != 0)
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


        public void Inserir(clsModeloMTReResiduos pModeloMTReResiduos, int pCodigo = 0)
        {
            try
            {
                s = "";
                s = s + "insert into ModeloMTReResiduos \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + "  Codigo, \n";
                s = s + "CodigoModeloMTRe, CodigoResiduo, CodigoEstadoFisico, CodigoClasse, \n";
                s = s + "CodigoAcondicionamento, CodigoTecnologia, NumeroONU, ClasseRisco, NomeEmbarque, GrupoEmbalagem, CodigoUnidade \n"; 
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + pCodigo.ToString() + ", \n";
                if (pModeloMTReResiduos.CodigoModeloMTRe > 0)
                    s = s + " " + pModeloMTReResiduos.CodigoModeloMTRe.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pModeloMTReResiduos.CodigoResiduo > 0)
                    s = s + " " + pModeloMTReResiduos.CodigoResiduo.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pModeloMTReResiduos.CodigoEstadoFisico > 0)
                    s = s + " " + pModeloMTReResiduos.CodigoEstadoFisico.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pModeloMTReResiduos.CodigoClasse > 0)
                    s = s + " " + pModeloMTReResiduos.CodigoClasse.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pModeloMTReResiduos.CodigoAcondicionamento > 0)
                    s = s + " " + pModeloMTReResiduos.CodigoAcondicionamento.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                if (pModeloMTReResiduos.CodigoTecnologia > 0)
                    s = s + " " + pModeloMTReResiduos.CodigoTecnologia.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0, \n";
                s = s + " '" + pModeloMTReResiduos.NumeroONU + "', \n";
                s = s + " '" + pModeloMTReResiduos.ClasseRisco + "', \n";
                s = s + " '" + pModeloMTReResiduos.NomeEmbarque + "', \n";
                s = s + " '" + pModeloMTReResiduos.GrupoEmbalagem + "', \n";
                if (pModeloMTReResiduos.CodigoUnidade > 0)
                    s = s + " " + pModeloMTReResiduos.CodigoUnidade.ToString().Replace(",", ".") + " \n";
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
        public string Alterar(clsModeloMTReResiduos pModeloMTReResiduos, int pCodigo, string pCodigoResiduo)
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
                s = s + "update ModeloMTReResiduos \n";
                if (pModeloMTReResiduos.CodigoModeloMTRe > 0)
                    s = s + " set CodigoModeloMTRe = " + pModeloMTReResiduos.CodigoModeloMTRe.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " set CodigoModeloMTRe = 0, \n";
                if (pModeloMTReResiduos.CodigoResiduo > 0)
                    s = s + " CodigoResiduo = " + pModeloMTReResiduos.CodigoResiduo.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " CodigoResiduo = 0, \n";
                if (pModeloMTReResiduos.CodigoEstadoFisico > 0)
                    s = s + " CodigoEstadoFisico = " + pModeloMTReResiduos.CodigoEstadoFisico.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " CodigoEstadoFisico = 0, \n";
                if (pModeloMTReResiduos.CodigoClasse > 0)
                    s = s + " CodigoClasse = " + pModeloMTReResiduos.CodigoClasse.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " CodigoClasse = 0, \n";
                if (pModeloMTReResiduos.CodigoAcondicionamento > 0)
                    s = s + " CodigoAcondicionamento = " + pModeloMTReResiduos.CodigoAcondicionamento.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " CodigoAcondicionamento = 0, \n";
                if (pModeloMTReResiduos.CodigoTecnologia > 0)
                    s = s + " CodigoTecnologia = " + pModeloMTReResiduos.CodigoTecnologia.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + " CodigoTecnologia = 0, \n";
                s = s + "  NumeroONU = '" + pModeloMTReResiduos.NumeroONU + "', \n";
                s = s + "  ClasseRisco = '" + pModeloMTReResiduos.ClasseRisco + "', \n";
                s = s + "  NomeEmbarque = '" + pModeloMTReResiduos.NomeEmbarque + "', \n";
                s = s + "  GrupoEmbalagem = '" + pModeloMTReResiduos.GrupoEmbalagem + "', \n";
                if (pModeloMTReResiduos.CodigoUnidade > 0)
                    s = s + " CodigoUnidade = " + pModeloMTReResiduos.CodigoUnidade.ToString().Replace(",", ".") + " \n";
                else
                    s = s + " CodigoUnidade = 0 \n";
                s = s + "where CodigoModeloMTRe = " + pCodigo + " \n";    
                s = s + "and   CodigoResiduo = " + pCodigoResiduo + " \n";
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
        public DataTable PreencheDataTable(string pOrdem, string pCodigoModeloMTRe = "")
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select mr.CodigoResiduo, i.CodigoIBAMA, replace(i.Descricao, '–', '') as DescricaoIBAMA, mr.CodigoEstadoFisico, ef.Descricao as DescricaoEstadoFisico, \n";
                s = s + "       mr.CodigoClasse, cls.Descricao as DescricaoClasse, \n";
                s = s + "       mr.CodigoAcondicionamento, ta.Descricao as DescricaoAcondicionamento, mr.CodigoTecnologia, t.Descricao as DescricaoTecnologia, \n";
                s = s + "       u.Descricao as DescricaoUnidade, mr.NumeroONU, mr.ClasseRisco, mr.NomeEmbarque, mr.GrupoEmbalagem, mr.CodigoUnidade \n";
                s = s + "from   ModeloMTReResiduos mr \n";
                s = s + "left   join Residuos r on r.Codigo = mr.CodigoResiduo \n";
                s = s + "left   join IBAMA i on i.Codigo = r.CodigoIbama \n";
                s = s + "left   join EstadoFisico ef on ef.Codigo = mr.CodigoEstadoFisico \n";
                s = s + "left   join Classe cls on cls.Codigo = mr.CodigoClasse \n";
                s = s + "left   join TipoAcondicionamento ta on ta.Codigo = mr.CodigoAcondicionamento \n";
                s = s + "left   join Tecnologia t on t.Codigo = mr.CodigoTecnologia \n";
                s = s + "left   join Unidades u on u.Codigo = mr.CodigoUnidade \n";
                if (pCodigoModeloMTRe != "")
                    s = s + "where  mr.CodigoModeloMTRe = " + pCodigoModeloMTRe + "\n";
                if (pOrdem != "")
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
                s = s + "select mr.CodigoModeloMTRe, m.Nome, mr.CodigoResiduo, mr.CodigoEstadoFisico, mr.CodigoClasse, \n";
                s = s + "       mr.CodigoAcondicionamento, mr.CodigoTecnologia, mr.NumeroONU, mr.ClasseRisco, mr.NomeEmbarque, mr.GrupoEmbalagem \n";
                s = s + "from   ModeloMTReResiduos mr \n";
                s = s + "inner  join ModeloMTRe m on m.Codigo = mr.CodigoModeloMTRe \n";
                s = s + "where  " + pCampo + " like '%" + pFiltro + "%' \n";
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

        public string Excluir(int pCodigoModeloMTRe, int pCodigoResiduo)
        {
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                if (pCodigoResiduo > 0)
                {
                    s = "";
                    s = s + "delete from ModeloMTReResiduos " + " \n";
                    s = s + "where  CodigoResiduo = " + pCodigoResiduo.ToString() + " \n";
                    s = s + "and    CodigoModeloMTRe = " + pCodigoModeloMTRe.ToString() + " \n";
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