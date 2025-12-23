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
	public class clsResiduoDados
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
        public clsResiduos PegaDados(clsResiduos pResiduos, int pCodigo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select r.Codigo, r.Descricao, r.Unidade, r.Classe, r.EstadoFisico, r.CodigoResiduoManifesto, r.Ativo, r.DescricaoReduzida, r.CodigoDestinoFinal, \n";
                s = s + "       r.CodigoGrupoResiduo, r.TecnologiaAplicada, r.EhReciclavel, r.EhServico, r.CodigoIBAMA, i.CodigoIBAMA as CodigoIBAMA_Analitico, \n ";
                s = s + "       (select DescricaoReduzida from Residuos where  Codigo = r.CodigoGrupoResiduo limit 1) as Grupo, r.DataCadastro, \n";
                s = s + "       a.NomeFantasia as DestinoFinal, M3PorTon \n";
                s = s + "from   Residuos r \n";
                s = s + "left  join IBAMA i  on i.Codigo = r.CodigoIBAMA \n ";
                s = s + "left  join Aterro a on a.Codigo = r.CodigoDestinoFinal \n ";
                if (pCodigo > 0)
                {
                    s = s + "where  r.Codigo = " + pCodigo + " ";
                    s = s + "limit 1";
                }
                else if (pCodigo == 0)
                {
                    s = s + " order by r.DescricaoReduzida limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    pResiduos.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                    pResiduos.Descricao = l_dt.Rows[0]["Descricao"].ToString();
                    pResiduos.Unidade = l_dt.Rows[0]["Unidade"].ToString();
                    pResiduos.Classe = l_dt.Rows[0]["Classe"].ToString();
                    if (l_dt.Rows[0]["DataCadastro"].ToString() != "")
                        pResiduos.DataCadastro = Convert.ToDateTime(l_dt.Rows[0]["DataCadastro"].ToString()).Date.ToShortDateString();
                    else
                        pResiduos.DataCadastro = "";
                    pResiduos.EstadoFisico = l_dt.Rows[0]["EstadoFisico"].ToString();
                    pResiduos.CodigoResiduoManifesto = l_dt.Rows[0]["CodigoResiduoManifesto"].ToString();
                    if (l_dt.Rows[0]["Ativo"].ToString() != "")
                        pResiduos.Ativo = Convert.ToInt16(l_dt.Rows[0]["Ativo"]);
                    pResiduos.DescricaoReduzida = l_dt.Rows[0]["DescricaoReduzida"].ToString();
                    if (l_dt.Rows[0]["CodigoDestinoFinal"].ToString() != "")
                        pResiduos.CodigoDestinoFinal = Convert.ToInt16(l_dt.Rows[0]["CodigoDestinoFinal"]);
                    if (l_dt.Rows[0]["CodigoGrupoResiduo"].ToString() != "")
                        pResiduos.CodigoGrupoResiduo = Convert.ToInt16(l_dt.Rows[0]["CodigoGrupoResiduo"]);
                    pResiduos.DescricaoGrupo = l_dt.Rows[0]["Grupo"].ToString();
                    pResiduos.TecnologiaAplicada = l_dt.Rows[0]["TecnologiaAplicada"].ToString();
                    if (l_dt.Rows[0]["EhReciclavel"].ToString() != "")
                        pResiduos.EhReciclavel = Convert.ToInt16(l_dt.Rows[0]["EhReciclavel"]);
                    if (l_dt.Rows[0]["EhServico"].ToString() != "")
                        pResiduos.EhServico = Convert.ToInt16(l_dt.Rows[0]["EhServico"]);
                    if (l_dt.Rows[0]["CodigoIBAMA"].ToString() != "")
                        pResiduos.CodigoIBAMA = Convert.ToInt16(l_dt.Rows[0]["CodigoIBAMA"]);
                    pResiduos.oIbama.CodigoIBAMA = l_dt.Rows[0]["CodigoIBAMA_Analitico"].ToString();
                    pResiduos.DescricaoDestinoFinal = l_dt.Rows[0]["DestinoFinal"].ToString();
                    if (l_dt.Rows[0]["M3PorTon"].ToString() != "")
                        pResiduos.M3PorTon = Convert.ToDecimal(l_dt.Rows[0]["M3PorTon"]);
                }
            }
            catch (Exception ex)
            {
                pResiduos = new clsResiduos();
            }
            finally
            {
                DesconectaBanco();
            }
            return pResiduos;
        }

        public int PegaCodigoResiduoDescricaoSemAcento(string pDescricaoResiduo)
        {

            int _CodigoRetorno = 0;
            string _DescDB = "";
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select DescricaoReduzida, Codigo from Residuos where CodigoIBAMA > 0 and CodigoGrupoResiduo > 0 \n";
                FillDataSet();
                foreach (DataRow _dr in l_dt.Rows)
                {
                    _DescDB = geral.RemoverAcentos(_dr["DescricaoReduzida"].ToString());
                    if (pDescricaoResiduo == _DescDB)
                    {
                        _CodigoRetorno = (int)_dr["Codigo"];
                        break;
                    }
                }                
            }
            catch (Exception ex)
            {
                _CodigoRetorno = 0; 
            }
            finally
            {
                DesconectaBanco();
            }
            return _CodigoRetorno;
        }
        public int PegaCodigoResiduo(string pDescricaoResiduoOuParte, bool pAtivo = false)
        {
            int _CodigoRetorno = 0;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo from Residuos  \n";
                s = s + "where  CodigoGrupoResiduo > 0 and descricaoreduzida like '" + pDescricaoResiduoOuParte + "%' \n";
                if (pAtivo)
                    s = s + "and    Ativo = 1 \n";
                s = s + "limit 1 \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                {
                    _CodigoRetorno = Convert.ToInt32( l_dt.Rows[0]["Codigo"]);
                }
                else if(l_dt.Rows.Count == 0)
                {
                    s = "";
                    s = s + "select Codigo from Residuos where descricaoreduzida like '" + pDescricaoResiduoOuParte + "%' limit 1 \n";
                    FillDataSet();
                    if (l_dt.Rows.Count > 0)
                        _CodigoRetorno = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                }

            }
            catch (Exception ex)
            {
                _CodigoRetorno = 0;
            }
            finally
            {
                DesconectaBanco();
            }
            return _CodigoRetorno;
        }
        public string PegaDescricao(int pCodigoResiduo)
        {
            string _Ret = "";
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select DescricaoReduzida from Residuos \n";
                s = s + "where Codigo = " + pCodigoResiduo + " \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                {
                    _Ret = l_dt.Rows[0]["DescricaoReduzida"].ToString();
                }
            }
            catch (Exception ex)
            {
                _Ret = "";
            }
            finally
            {
                DesconectaBanco();
            }
            return _Ret;
        }
        public DataTable PegaDados(clsResiduos pResiduos, int pCodigo, bool pUltimoRegistro, bool SoGrupo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select r.Codigo, r.Descricao, r.Unidade, r.Classe, r.EstadoFisico, r.CodigoResiduoManifesto, r.Ativo, r.DataCadastro, r.DescricaoReduzida, \n";
                s = s + "       r.CodigoDestinoFinal, r.CodigoGrupoResiduo, r.TecnologiaAplicada, r.EhReciclavel, r.EhServico, i.CodigoIBAMA as CodigoIBAMA_Analitico, a.NomeFantasia as DestinoFinal,";
                s = s + "       (select DescricaoReduzida from Residuos where  Codigo = r.CodigoGrupoResiduo limit 1) as Grupo \n";
                s = s + "from   Residuos r ";
                s = s + "left  join IBAMA i on i.Codigo =  r.CodigoIBAMA ";
                s = s + "left  join Aterro a on a.Codigo = r.CodigoDestinoFinal ";
                if (pCodigo > 0)
                {
                    s = s + "where  r.Codigo = " + pCodigo + " \n ";
                }
                if (SoGrupo)
                {
                    if (pCodigo == 0)
                        s = s + "where ";
                    else
                        s = s + "and   ";
                    s = s + "CodigoGrupoResiduo = 0 \n";
                }

                if (pUltimoRegistro)
                {
                    s = s + "order by r.Codigo desc ";
                }
                else if (pCodigo == 0)
                {
                    s = s + " order by r.DescricaoReduzida ";
                }
                FillDataSet();                
            }
            catch (Exception ex)
            {
                l_dt = new DataTable();
            }
            finally
            {
                DesconectaBanco();
            }
            return l_dt;
        }
        public DataTable PegaDadosLista(string pDescricao, int pCodigo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select r.Codigo, r.DescricaoReduzida as Descricao, \n";
                s = s + "       (select DescricaoReduzida from Residuos where  Codigo = r.CodigoGrupoResiduo limit 1) as Grupo, Unidade, \n";
                s = s + "       r.Classe, a.Nome as DestinoFinal, i.CodigoIbama\n";
                s = s + "from   Residuos r ";
                s = s + "left  join IBAMA i  on i.Codigo =  r.CodigoIBAMA ";
                s = s + "left  join Aterro a on a.Codigo = r.CodigoDestinoFinal ";
                s = s + "where r.CodigoGrupoResiduo > 0 \n";
                s = s + "and   r.Ativo = 1 \n";
                if (pDescricao != "")
                {
                    s = s + "and  r.DescricaoReduzida LIKE '%" + pDescricao + "%' \n ";
                }
                if (pCodigo > 0)
                {
                    s = s + "and  r.Codigo = " + pCodigo + " \n ";
                }
                else if (pCodigo == 0)
                {
                    s = s + " order by r.DescricaoReduzida ";
                }
                FillDataSet();
            }
            catch (Exception ex)
            {
                l_dt = new DataTable();
            }
            finally
            {
                DesconectaBanco();
            }
            return l_dt;
        }
        public DataTable PegaListaComResiduosContratados(string pDescricao, int pCodigo, int pCodigoCliente)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select r.Codigo, r.DescricaoReduzida as Descricao, \n";
                s = s + "       (select DescricaoReduzida from Residuos where  Codigo = r.CodigoGrupoResiduo limit 1) as Grupo, r.Unidade, 1 as Contratado, \n";
                s = s + "       r.Classe, a.Nome as DestinoFinal, i.CodigoIbama\n";
                s = s + "from   ContratoResiduos cr \n";
                s = s + "inner  join Residuos  r on r.Codigo = cr.CodigoResiduo \n";
                s = s + "inner  join Contratos o on o.Codigo = cr.CodigoContrato \n";
                s = s + "left   join Ibama     i on i.Codigo = r.CodigoIbama \n";
                s = s + "left   join Aterro    a on a.Codigo = r.CodigoDestinoFinal \n";
                s = s + "where  o.CodigoCliente = " + pCodigoCliente.ToString() + "\n";
                s = s + "and    (o.DataRecisao is null or o.DataRecisao = '0100-01-01' or o.DataRecisao = '1900-01-01' or o.DataRecisao = '0001-01-01')  \n";
                s = s + "and    (select Data from Reajustes where CodigoContrato = o.Codigo order by Data desc limit 1) =  cr.DataReajuste \n";
                
                s = s + "union  all \n";
                
                s = s + "select r.Codigo, r.DescricaoReduzida as Descricao, \n";
                s = s + "       (select DescricaoReduzida from Residuos where  Codigo = r.CodigoGrupoResiduo limit 1) as Grupo, r.Unidade, 0 as Contratado, \n";
                s = s + "       r.Classe, a.Nome as DestinoFinal, i.CodigoIbama \n";
                s = s + "from   Residuos r ";
                s = s + "left  join IBAMA  i on i.Codigo = r.CodigoIBAMA ";
                s = s + "left  join Aterro a on a.Codigo = r.CodigoDestinoFinal ";
                s = s + "where r.CodigoGrupoResiduo > 0 \n";
                s = s + "and   r.Ativo = 1 \n";
                if (pDescricao != "")
                {
                    s = s + "and  r.DescricaoReduzida LIKE '%" + pDescricao + "%' \n ";
                }
                if (pCodigo > 0)
                {
                    s = s + "and  r.Codigo = " + pCodigo + " \n ";
                }
                else if (pCodigo == 0)
                {
                    s = s + " order by Contratado desc, Descricao ";
                }
                FillDataSet();
            }
            catch (Exception ex)
            {
                l_dt = new DataTable();
            }
            finally
            {
                DesconectaBanco();
            }
            return l_dt;
        }
        public int PegaTamanhoCampoVarChar(string pCampo)
        {
            int iRet = 0;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "show columns ";
                s = s + "from Residuos ";
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
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Codigo ";
                s = s + "from   Residuos ";
                if (pCodigo != 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " ";
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
        public bool CodigoExiste(int pCodigo)
        {
            bool bRet = false;
            try
            {
                oDB.ConectaMySql();
                s = "";
                if (pCodigo != 0)
                {
                    s = s + "select Codigo ";
                    s = s + "from   Residuos ";
                    s = s + "where  Codigo = " + pCodigo + " ";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pCodigo != 0)
                    bRet = true;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return bRet;
        }
        public void Inserir(clsResiduos pResiduos, int pCodigo = 0)
        {
            try
            {
                s = "";
                s = s + "insert into Residuos \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + "  Codigo, \n";
                s = s + "  DataCadastro, Descricao, Unidade, Classe, EstadoFisico, CodigoResiduoManifesto, Ativo, DescricaoReduzida, CodigoDestinoFinal, CodigoGrupoResiduo, TecnologiaAplicada, EhReciclavel, EhServico, CodigoIBAMA, M3PorTon \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                if (pCodigo > 0)
                    s = s + pCodigo + ", \n";
                if (pResiduos.DataCadastro != "")
                    s = s + "'" + Convert.ToDateTime(pResiduos.DataCadastro).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                s = s + "'" + pResiduos.Descricao + "', \n";
                s = s + "'" + pResiduos.Unidade + "', \n";
                s = s + "'" + pResiduos.Classe + "', \n";
                s = s + "'" + pResiduos.EstadoFisico + "', \n";
                s = s + "'" + pResiduos.CodigoResiduoManifesto + "', \n";

                if (pResiduos.Ativo > 0)
                    s = s + " " + pResiduos.Ativo + ", \n";
                else
                    s = s + "0, \n";
                
                s = s + "'" + pResiduos.DescricaoReduzida + "', \n";

                if (pResiduos.CodigoDestinoFinal > 0 )
                    s = s + " " + pResiduos.CodigoDestinoFinal + ", \n";
                else
                    s = s + "0, \n";
                
                if (pResiduos.CodigoGrupoResiduo > 0)
                    s = s + " " + pResiduos.CodigoGrupoResiduo + ", \n";
                else
                    s = s + "0, \n";

                s = s + "'" + pResiduos.TecnologiaAplicada + "', \n";

                if (pResiduos.EhReciclavel > 0)
                    s = s + " " + pResiduos.EhReciclavel + ", \n";
                else
                    s = s + "0, \n";

                if (pResiduos.EhServico > 0)
                    s = s + " " + pResiduos.EhServico + ", \n";
                else
                    s = s + "0, \n";

                if (pResiduos.CodigoIBAMA > 0)
                    s = s + " " + pResiduos.CodigoIBAMA + ", \n";
                else
                    s = s + "0, \n";

                if (pResiduos.M3PorTon > 0)
                    s = s + " " + pResiduos.M3PorTon.ToString().Replace(",", ".") + " \n";
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
        public string Alterar(clsResiduos pResiduos, int pCodigo)
        {
            string sRet = "";
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update Residuos \n";
                s = s + "set Descricao          = '" + pResiduos.Descricao + "', \n";
                s = s + "    Unidade            = '" + pResiduos.Unidade + "', \n";
                s = s + "    Classe             = '" + pResiduos.Classe + "', \n";
                s = s + "    EstadoFisico       = '" + pResiduos.EstadoFisico + "', \n";
                s = s + "CodigoResiduoManifesto = '" + pResiduos.CodigoResiduoManifesto + "', \n";
                s = s + "    DataCadastro       = '" + Convert.ToDateTime(pResiduos.DataCadastro).ToString("yyyy-MM-dd") + "', \n";
                s = s + "Ativo                  = " + pResiduos.Ativo + ", \n";
                s = s + "DescricaoReduzida      = '" + pResiduos.DescricaoReduzida + "', \n";
                if (pResiduos.CodigoDestinoFinal > 0 )
                    s = s + "CodigoDestinoFinal = " + pResiduos.CodigoDestinoFinal + ", \n";
                else
                    s = s + "CodigoDestinoFinal = 0, \n";
                
                if (pResiduos.CodigoGrupoResiduo > 0)
                    s = s + " CodigoGrupoResiduo = " + pResiduos.CodigoGrupoResiduo + ", \n";
                else
                    s = s + "CodigoGrupoResiduo = 0, \n";

                s = s + "  TecnologiaAplicada = '" + pResiduos.TecnologiaAplicada + "', \n";

                if (pResiduos.EhReciclavel > 0)
                    s = s + " EhReciclavel = " + pResiduos.EhReciclavel + ", \n";
                else
                    s = s + " EhReciclavel = 0, \n";

                if (pResiduos.EhServico > 0)
                    s = s + " EhServico = " + pResiduos.EhServico + ", \n";
                else
                    s = s + " EhServico = 0, \n";

                if (pResiduos.CodigoIBAMA > 0)
                    s = s + " CodigoIBAMA = " + pResiduos.CodigoIBAMA + ", \n";
                else
                    s = s + "CodigoIBAMA = 0, \n";
                
                // retirado só até arrumar no SILC velho
                if (pResiduos.M3PorTon > 0)
                    s = s + " M3PorTon = " + pResiduos.M3PorTon.ToString().Replace(",", ".") + " \n";
                else
                    s = s + "M3PorTon = 0 \n";                
                s = s + "where Codigo = " + pCodigo.ToString();

                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
                sRet = string.Empty;
            }
            catch (Exception ex)
            {
               transaction.Rollback();
               sRet = ex.ToString();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return sRet;
        }
        public DataTable PreencheDataTableResiduos(string pOrdem, string pFiltro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select * from ";
                s = s + "(";

                s = s + "select r.Codigo, r.Descricao, r.Unidade, r.Classe, r.EstadoFisico, r.CodigoResiduoManifesto, r.Ativo, '' as DescricaoReduzida, r.CodigoDestinoFinal,  \n";
   	            s = s + "       r.Codigo as CodigoGrupoResiduo, r.TecnologiaAplicada, r.EhReciclavel, r.CodigoIBAMA, i.CodigoIBAMA as CodigoIBAMA_Analitico,  \n";
                s = s + "       a.NomeFantasia as DestinoFinal, r.DescricaoReduzida as Grupo, r.DataCadastro, r.DescricaoReduzida as OrdemGrupo \n";
	            s = s + "from    Residuos r  \n";
	            s = s + "left   join IBAMA i on i.Codigo = r.CodigoIBAMA  \n";
	            s = s + "left   join Aterro a on a.Codigo = r.CodigoDestinoFinal  \n";
	            s = s + "where  r.CodigoGrupoResiduo = 0 \n";
                s = s + "union  all \n";
 	            s = s + "select r.Codigo, r.Descricao, r.Unidade, r.Classe, r.EstadoFisico, r.CodigoResiduoManifesto, r.Ativo, r.DescricaoReduzida, r.CodigoDestinoFinal,  \n";
		        s = s + "       r.CodigoGrupoResiduo, r.TecnologiaAplicada, r.EhReciclavel, r.CodigoIBAMA, i.CodigoIBAMA as CodigoIBAMA_Analitico,  \n";
		        s = s + "       a.NomeFantasia as DestinoFinal, '' as Grupo, r.DataCadastro,  \n";
                s = s + "       (select DescricaoReduzida from Residuos where Codigo = r.CodigoGrupoResiduo limit 1) as OrdemGrupo \n";
	            s = s + "from   Residuos r  \n";
	            s = s + "left   join IBAMA i on i.Codigo = r.CodigoIBAMA  \n";
	            s = s + "left   join Aterro a on a.Codigo = r.CodigoDestinoFinal  \n";
	            s = s + "where  CodigoGrupoResiduo > 0 \n";
                s = s + ") x \n ";
                if (pFiltro == "Ativos")
                    s = s + "where Ativo = 1 \n ";
                if (pOrdem == "Grupo asc" || pOrdem == "Grupo desc")
                    s = s + "order by OrdemGrupo, DescricaoReduzida, Grupo desc";
                else
                    s = s + "order by " + pOrdem;
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                DesconectaBanco();
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
        public DataTable PreencheDataTableResiduos(string pOrdem, string pFiltro, string pCampo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select * from ";
                s = s + "(";
                s = s + "select r.Codigo, r.DataCadastro ,r.Descricao, r.Unidade, r.Classe, r.EstadoFisico, r.CodigoResiduoManifesto, r.Ativo, r.DescricaoReduzida, \n";
                s = s + "       r.CodigoDestinoFinal, r.CodigoGrupoResiduo, r.TecnologiaAplicada, r.EhReciclavel, r.CodigoIBAMA, i.CodigoIBAMA as CodigoIBAMA_Analitico, \n ";
                s = s + "       (select DescricaoReduzida from Residuos where Codigo = r.CodigoGrupoResiduo limit 1) as Grupo, a.NomeFantasia as DestinoFinal \n";
                s = s + "from   Residuos r ";
                s = s + "left  join IBAMA i on i.Codigo = r.CodigoIBAMA ";
                s = s + "left  join Aterro a on a.Codigo = r.CodigoDestinoFinal ";
                s = s + ") x \n ";
                if (pCampo == "Ativos")
                    s = s + "where (Ativo is null or ativo = 1) \n";
                else
                    s = s + "where  " + pCampo + " like '%" + pFiltro + "%' \n";
                s = s + "order  by " + pOrdem +" \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                DesconectaBanco();
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
        public DataTable PreencheDataTableSoComResiduos(string pOrdem, string pFiltro, string pCampo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select * from \n";
                s = s + "( \n";
                s = s + "select r.Codigo, r.DataCadastro ,r.Descricao, r.Unidade, r.Classe, r.EstadoFisico, r.CodigoResiduoManifesto, r.Ativo, r.DescricaoReduzida, \n";
                s = s + "       r.CodigoDestinoFinal, r.CodigoGrupoResiduo, r.TecnologiaAplicada, r.EhReciclavel, r.CodigoIBAMA, i.CodigoIBAMA as CodigoIBAMA_Analitico, \n ";
                s = s + "       (select DescricaoReduzida from Residuos where Codigo = r.CodigoGrupoResiduo limit 1) as Grupo, a.NomeFantasia as DestinoFinal \n";
                s = s + "from   Residuos r \n";
                s = s + "left  join IBAMA i on i.Codigo = r.CodigoIBAMA \n";
                s = s + "left  join Aterro a on a.Codigo = r.CodigoDestinoFinal \n";
                s = s + "where r.CodigoGrupoResiduo > 0 \n";
                s = s + ") x \n ";
                if (pCampo == "Ativos")
                    s = s + "where (Ativo is null or ativo = 1) \n";
                else
                    s = s + "where  " + pCampo + " like '%" + pFiltro + "%' \n";
                s = s + "order  by " + pOrdem + " \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                DesconectaBanco();
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

        public DataTable PegaDtComDadosConversao()
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select r.Codigo, r.DescricaoReduzida as Residuo, r.m3PorTon as Peso, r.Unidade  \n";
                s = s + "from   Residuos r \n";
                s = s + "where  r.CodigoGrupoResiduo > 0 \n";
                s = s + "and    (Ativo is null or Ativo = 1) \n";
                s = s + "and    UCase(r.Unidade) != 'KG' \n";
                s = s + "and    r.m3PorTon > 0 \n";
                s = s + "union all \n";
                s = s + "select r.Codigo, r.DescricaoReduzida as Residuo, r.KgPorUnd as Peso, r.Unidade  \n";
                s = s + "from   Residuos r \n";
                s = s + "where  r.CodigoGrupoResiduo > 0 \n";
                s = s + "and    (Ativo is null or Ativo = 1) \n";
                s = s + "and    UCase(r.Unidade) != 'KG' \n";
                s = s + "and    r.KgPorUnd > 0 \n";
                s = s + "order  by Residuo \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                DesconectaBanco();
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
        public DataTable PreencheDataTableGrupo(string pOrdem, bool pAtivos)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, DataCadastro, Ativo, DescricaoReduzida as Grupo \n";
                s = s + "from   Residuos \n";
                if (pAtivos)
                    s = s + "where  Ativo = 1 and CodigoGrupoResiduo = 0 \n";
                if (!pAtivos)
                    s = s + "where  CodigoGrupoResiduo = 0 \n";
                s = s + "order  by " + pOrdem + " \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                DesconectaBanco();
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
        public DataTable PreencheDTCodigosResiduos(bool pReciclaveis)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Ativo, EhReciclavel \n";
                s = s + "from   Residuos \n";
                s = s + "where  (EhServico = 0 or EhServico is null) \n";
                if (pReciclaveis)
                    s = s + "and   EhReciclavel = 1 \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                DesconectaBanco();
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
        public string Excluir(int pCodigo, string pDescricaoReduzida)
        {
            string sRet = "";
            ConectaBanco();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "delete from Residuos ";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo.ToString();
                }
                else
                {
                    s = s + "where  DescricaoReduzida = '" + pDescricaoReduzida + "'";
                }
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
                sRet = string.Empty;
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
            return sRet;
        }
    }
}