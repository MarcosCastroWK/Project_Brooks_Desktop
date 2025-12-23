using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using MySql.Data.MySqlClient;
using SILCNegocios;

namespace LibSILC
{
	public class clsCaminhoesDados
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
        public clsCaminhoes PegaDados(clsCaminhoes pCaminhoes, int pCodigo)
        {
            try
            {

                ConectaBanco();
                s = "";
                s = s + "select Codigo, DataCadastro, Inativo, Modelo, Marca, AnoFabricacao, AnoModelo, Chassi, Cidade, Cor, TipoVeiculo, DataAquisicao, ValorAquisicao, EhProprio, VencimentoSeguroFrota, ";
                s = s + "       dtVctoSegr, Kilometragem, Placas, Renavam,  ValorFranquia, VctoIPVA, VctoLicenciamento ";
                s = s + "from   Caminhoes ";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " ";
                    s = s + "limit 1";
                }
                else if (pCodigo == 0)
                {
                    s = s + " order by Modelo limit 1";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    pCaminhoes.Codigo = Convert.ToInt32(l_dt.Rows[0]["Codigo"]);
                    if (l_dt.Rows[0]["DataCadastro"].ToString() != "")
                        pCaminhoes.DataCadastro = Convert.ToDateTime(l_dt.Rows[0]["DataCadastro"].ToString()).Date.ToShortDateString();
                    if (l_dt.Rows[0]["Inativo"].ToString() != "")
                        pCaminhoes.Inativo = Convert.ToInt32(l_dt.Rows[0]["Inativo"]);
                    if (l_dt.Rows[0]["AnoFabricacao"].ToString() != "")
                        pCaminhoes.AnoFabricacao = Convert.ToInt32(l_dt.Rows[0]["AnoFabricacao"]);
                    if (l_dt.Rows[0]["AnoModelo"].ToString() != "")
                        pCaminhoes.AnoModelo = Convert.ToInt32(l_dt.Rows[0]["AnoModelo"]);
                    pCaminhoes.Chassi = l_dt.Rows[0]["Chassi"].ToString();
                    pCaminhoes.Cidade = l_dt.Rows[0]["Cidade"].ToString();
                    pCaminhoes.Cor = l_dt.Rows[0]["Cor"].ToString();
                    if (l_dt.Rows[0]["DataAquisicao"].ToString() != "")
                        pCaminhoes.DataAquisicao = Convert.ToDateTime(l_dt.Rows[0]["DataAquisicao"].ToString()).Date.ToShortDateString();
                    if (l_dt.Rows[0]["dtVctoSegr"].ToString() != "")
                        pCaminhoes.dtVctoSegr = Convert.ToDateTime(l_dt.Rows[0]["dtVctoSegr"].ToString()).Date.ToShortDateString();
                    if (l_dt.Rows[0]["EhProprio"].ToString() != "")
                        pCaminhoes.EhProprio = Convert.ToInt16(l_dt.Rows[0]["EhProprio"]);
                    if (l_dt.Rows[0]["Kilometragem"].ToString() != "")
                        pCaminhoes.Kilometragem = Convert.ToInt32(l_dt.Rows[0]["Kilometragem"]);
                    pCaminhoes.Marca = l_dt.Rows[0]["Marca"].ToString();
                    pCaminhoes.Modelo = l_dt.Rows[0]["Modelo"].ToString();
                    pCaminhoes.Placas = l_dt.Rows[0]["Placas"].ToString();
                    pCaminhoes.Renavam = l_dt.Rows[0]["Renavam"].ToString();
                    pCaminhoes.TipoVeiculo = l_dt.Rows[0]["TipoVeiculo"].ToString();
                    if (l_dt.Rows[0]["ValorAquisicao"].ToString() != "")
                        pCaminhoes.ValorAquisicao = Convert.ToDecimal(l_dt.Rows[0]["ValorAquisicao"]);
                    if (l_dt.Rows[0]["ValorFranquia"].ToString() != "")
                        pCaminhoes.ValorFranquia = Convert.ToDecimal(l_dt.Rows[0]["ValorFranquia"]);
                    if (l_dt.Rows[0]["VctoIPVA"].ToString() != "")
                        pCaminhoes.VctoIPVA = Convert.ToDateTime(l_dt.Rows[0]["VctoIPVA"].ToString()).Date.ToShortDateString();
                    if (l_dt.Rows[0]["VctoLicenciamento"].ToString() != "")
                        pCaminhoes.VctoLicenciamento =     Convert.ToDateTime(l_dt.Rows[0]["VctoLicenciamento"].ToString()).Date.ToShortDateString();
                    if (l_dt.Rows[0]["VencimentoSeguroFrota"].ToString() != "")
                        pCaminhoes.VencimentoSeguroFrota = Convert.ToDateTime(l_dt.Rows[0]["VencimentoSeguroFrota"].ToString()).Date.ToShortDateString();
                }                
            }
            catch (Exception ex)
            {
                pCaminhoes = new clsCaminhoes();
            }
            finally
            {
                DesconectaBanco();
            }
            return pCaminhoes;

        }
        public DataTable PegaDadosLista(int pCodigo, string pModelo)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, Modelo \n";
                s = s + "from   Caminhoes ";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " ";
                }
                else if (pCodigo == 0)
                {
                    s = s + "where    Modelo like '%" + pModelo + "%' \n";
                    s = s + "order by Modelo \n";
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
        public DataTable PegaDados(clsCaminhoes pCaminhoes, int pCodigo, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, DataCadastro, Inativo, Modelo, Marca, AnoFabricacao, AnoModelo, Chassi, Cidade, Cor, TipoVeiculo, DataAquisicao, ";
                s = s + "       ValorAquisicao, EhProprio, VencimentoSeguroFrota, dtVctoSegr, "; 
                s = s + "       Kilometragem, Placas, Renavam, TitlVeic, ValorFranquia, VctoIPVA, VctoLicenciamento ";
                s = s + "from   Caminhoes ";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo + " ";
                }
                if (pUltimoRegistro)
                {
                    s = s + "order by Codigo desc ";
                }
                else if (pCodigo == 0)
                {
                    s = s + " order by Modelo ";
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
                s = s + "from Caminhoes ";
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
                s = s + "from   Caminhoes ";
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
        public int PegaCodigoCaminhao(string pPlacas)
        {
            int  iRet = 0;
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Codigo ";
                s = s + "from   Caminhoes ";
                s = s + "where  Placas = '" + pPlacas + "' \n";
                FillDataSet();
                if (l_dt.Rows.Count > 0)
                    iRet = Convert.ToInt16(l_dt.Rows[0][0]);
            }
            catch (Exception ex)
            {
                iRet = -1;
            }
            finally
            {
                oDB.DesconectaMySql();
            }
            return iRet;
        }

        public void Inserir(clsCaminhoes pCaminhoes, int pCodigo = 0)
        {
            try
            {
                s = "";
                s = s + "insert into Caminhoes ";
                s = s + "(";
                if (pCodigo > 0)
                    s = s + " Codigo, ";
                s = s + "   DataCadastro, Inativo, Modelo, Marca, AnoFabricacao, AnoModelo, Chassi, Cidade, Cor, TipoVeiculo, DataAquisicao, ValorAquisicao, EhProprio, VencimentoSeguroFrota, ";
                s = s + "   dtVctoSegr, Kilometragem, Placas, Renavam, ValorFranquia, VctoIPVA, VctoLicenciamento";  
                s = s + ") ";
                s = s + "values ";
                s = s + "(";
                if (pCodigo > 0)
                    s = s + pCodigo + ", ";

                if (pCaminhoes.DataCadastro != "" && pCaminhoes.DataCadastro != null)
                    s = s + "'" + Convert.ToDateTime(pCaminhoes.DataCadastro).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";

                s = s + " " + pCaminhoes.Inativo + ", \n";
                s = s + "'" + pCaminhoes.Modelo + "', \n";
                s = s + "'" + pCaminhoes.Marca + "', \n";
                s = s + " " + pCaminhoes.AnoFabricacao + ", \n";
                s = s + " " + pCaminhoes.AnoModelo + ", \n";
                s = s + "'" + pCaminhoes.Chassi + "', \n";
                s = s + "'" + pCaminhoes.Cidade + "', \n";
                s = s + "'" + pCaminhoes.Cor + "', \n";
                s = s + "'" + pCaminhoes.TipoVeiculo + "', \n";
                
                if (pCaminhoes.DataAquisicao != "")
                    s = s + "'" + Convert.ToDateTime(pCaminhoes.DataAquisicao).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";

                if (pCaminhoes.ValorAquisicao.ToString() != "")
                    s = s + " " + pCaminhoes.ValorAquisicao.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0.00, \n";

                s = s + " " + pCaminhoes.EhProprio + ", \n";

                if (pCaminhoes.VencimentoSeguroFrota != "")
                    s = s + "'" + Convert.ToDateTime(pCaminhoes.VencimentoSeguroFrota).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                if (pCaminhoes.dtVctoSegr != "")
                    s = s + "'" + Convert.ToDateTime(pCaminhoes.dtVctoSegr).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";

                s = s + " " + pCaminhoes.Kilometragem + ", \n";
                s = s + "'" + pCaminhoes.Placas + "', \n";
                s = s + "'" + pCaminhoes.Renavam + "', \n";
                if (pCaminhoes.ValorFranquia.ToString() != "")
                    s = s + " " + pCaminhoes.ValorFranquia.ToString().Replace(",", ".") + ", \n";
                else
                    s = s + "0.00, \n";
                if (pCaminhoes.VctoIPVA != "")
                    s = s + "'" + Convert.ToDateTime(pCaminhoes.VctoIPVA).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n";
                if (pCaminhoes.VctoLicenciamento != "")
                    s = s + "'" + Convert.ToDateTime(pCaminhoes.VctoLicenciamento).ToString("yyyy-MM-dd") + "' \n";
                else
                    s = s + "'0001-01-01', \n";

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
        public string Alterar(clsCaminhoes pCaminhoes, int pCodigo)
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
                s = s + "update Caminhoes \n";
                s = s + "set   Modelo        = '" + pCaminhoes.Modelo + "', \n";
                s = s + "      DataCadastro  = '" + Convert.ToDateTime(pCaminhoes.DataCadastro).ToString("yyyy-MM-dd") + "', \n";
                s = s + "      Inativo       = " + pCaminhoes.Inativo.ToString().Replace(",", ".") + ", \n";
                s = s + "      AnoFabricacao = "  + pCaminhoes.AnoFabricacao.ToString() + ", \n";
                s = s + "      AnoModelo     = "  + pCaminhoes.AnoModelo.ToString() + ", \n";
                s = s + "      Chassi        = '" + pCaminhoes.Chassi + "', \n";
                s = s + "      Marca         = '" + pCaminhoes.Marca + "', \n";
                s = s + "      Cidade        = '" + pCaminhoes.Cidade + "', \n";
                s = s + "      Cor           = '" + pCaminhoes.Cor + "', \n";
                s = s + "      dtVctoSegr    = '" + Convert.ToDateTime(pCaminhoes.dtVctoSegr).ToString("yyyy-MM-dd") + "', \n";
                s = s + "      Kilometragem  = "  + pCaminhoes.Kilometragem.ToString() + ", \n";
                s = s + "      Placas        = '" + pCaminhoes.Placas + "', \n";
                s = s + "      TipoVeiculo   = '" + pCaminhoes.TipoVeiculo + "', \n";
                s = s + "      DataAquisicao = '" + Convert.ToDateTime(pCaminhoes.DataAquisicao).ToString("yyyy-MM-dd") + "', \n";
                s = s + "      ValorAquisicao =  " + pCaminhoes.ValorAquisicao.ToString().Replace(",", ".") + ", \n";
                s = s + "      EhProprio      =  " + pCaminhoes.EhProprio + ", \n";
                s = s + "      VencimentoSeguroFrota  = '" + Convert.ToDateTime(pCaminhoes.VencimentoSeguroFrota).ToString("yyyy-MM-dd") + "', \n";
                s = s + "      Renavam       = '" + pCaminhoes.Renavam + "', \n";
                s = s + "      ValorFranquia = "  + pCaminhoes.ValorFranquia.ToString().Replace(",", ".") + ", \n";
                s = s + "      VctoIPVA      = '" + Convert.ToDateTime(pCaminhoes.VctoIPVA).ToString("yyyy-MM-dd") + "', \n";
                s = s + "      VctoLicenciamento = '" + Convert.ToDateTime(pCaminhoes.VctoLicenciamento).ToString("yyyy-MM-dd") + "' \n";
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
        public DataTable PreencheDataTableCaminhoes(string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, ";
                s = s + "   DataCadastro, Inativo, Modelo, Marca, AnoFabricacao, AnoModelo, Chassi, Cidade, Cor, TipoVeiculo, DataAquisicao, ValorAquisicao, EhProprio, VencimentoSeguroFrota, ";
                s = s + "   dtVctoSegr, Kilometragem, Placas, Renavam, ValorFranquia, VctoIPVA, VctoLicenciamento ";
                s = s + "from   Caminhoes \n";
                s = s + "where  (Inativo = 0 or Inativo is null) \n";
                s = s + "order by " + pOrdem + " \n";
                l_ds = new DataSet();
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                FillDataSet();
                DesconectaBanco();
                return l_ds.Tables[0];
            }
            catch (Exception ex)
            {
                DesconectaBanco();
                return l_dt;
            }
        }

        public string Excluir(int pCodigo, string pModelo)
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
                s = s + "delete from Caminhoes ";
                if (pCodigo > 0)
                {
                    s = s + "where  Codigo = " + pCodigo.ToString();
                }
                else
                {
                    s = s + "where  Modelo = '" + pModelo + "'";
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