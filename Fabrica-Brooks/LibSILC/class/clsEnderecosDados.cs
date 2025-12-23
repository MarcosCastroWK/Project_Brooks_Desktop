using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using MySql.Data.MySqlClient;
using MySql.Data;
using SILCNegocios;

namespace LibSILC
{
    public class clsEnderecosDados
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
        public clsEnderecos PegaDados(clsEnderecos pEnderecos, int pCodigoCliente, int pTipoEndereco, int pTipoCadastro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + "select Codigo, TipoCadastro, TipoEndereco, endereco, Numero, Bairro, \n";
                s = s + "       CEP, CodigoMunicipio, DDD1, Fone1, DDD2, Fone2, DDD3, Fone3, \n";
                s = s + "       DDDF, Fax, email, Contato, InstrucoesFat, Complemento, CargoContato \n";
                s = s + "from   Enderecos \n";
                if (pCodigoCliente > 0)
                {
                    s = s + "where  Codigo = " + pCodigoCliente +  " \n";
                    s = s + "and    TipoEndereco = " + pTipoEndereco + " \n";
                    s = s + "and    TipoCadastro = " + pTipoCadastro + " \n";
                }
                else if (pCodigoCliente == 0)
                {
                    s = s + " order by Descricao limit 1 \n";
                }
                FillDataSet();
                if (l_ds.Tables.Count > 0)
                {
                    l_dt = l_ds.Tables[0];
                    if (l_dt.Rows.Count > 0)
                    {
                        if (l_dt.Rows[0]["TipoCadastro"].ToString() != "")
                            pEnderecos.TipoCadastro = Convert.ToInt16(l_dt.Rows[0]["TipoCadastro"]);
                        if (l_dt.Rows[0]["TipoEndereco"].ToString() != "")
                            pEnderecos.TipoCadastro = Convert.ToInt16(l_dt.Rows[0]["TipoEndereco"]);
                        pEnderecos.endereco = l_dt.Rows[0]["endereco"].ToString();
                        pEnderecos.Numero = l_dt.Rows[0]["Numero"].ToString();
                        pEnderecos.Bairro = l_dt.Rows[0]["Bairro"].ToString();
                        pEnderecos.CEP = l_dt.Rows[0]["CEP"].ToString();
                        if (l_dt.Rows[0]["CodigoMunicipio"].ToString() != "")
                            pEnderecos.CodigoMunicipio = Convert.ToInt32(l_dt.Rows[0]["CodigoMunicipio"].ToString());
                        if (l_dt.Rows[0]["DDD1"].ToString() != "")
                            pEnderecos.DDD1 = Convert.ToInt32(l_dt.Rows[0]["DDD1"].ToString());
                        pEnderecos.Fone1 = l_dt.Rows[0]["Fone1"].ToString();
                        if (l_dt.Rows[0]["DDD2"].ToString() != "")
                            pEnderecos.DDD2 = Convert.ToInt32(l_dt.Rows[0]["DDD2"].ToString());
                        pEnderecos.Fone2 = l_dt.Rows[0]["Fone2"].ToString();
                        if (l_dt.Rows[0]["DDD3"].ToString() != "")
                            pEnderecos.DDD3 = Convert.ToInt32(l_dt.Rows[0]["DDD3"].ToString());
                        pEnderecos.Fone3 = l_dt.Rows[0]["Fone3"].ToString();
                        if (l_dt.Rows[0]["DDDF"].ToString() != "")
                            pEnderecos.DDDF = Convert.ToInt32(l_dt.Rows[0]["DDDF"].ToString());
                        pEnderecos.Fax = l_dt.Rows[0]["Fax"].ToString();
                        pEnderecos.email = l_dt.Rows[0]["email"].ToString();
                        pEnderecos.Contato = l_dt.Rows[0]["Contato"].ToString();
                        pEnderecos.InstrucoesFat = l_dt.Rows[0]["InstrucoesFat"].ToString();
                        pEnderecos.Complemento = l_dt.Rows[0]["Complemento"].ToString();
                        pEnderecos.CargoContato = l_dt.Rows[0]["CargoContato"].ToString();
                    }
                }                
            }
            catch (Exception ex)
            {
                return new clsEnderecos();
            }
            finally
            {
                DesconectaBanco();
            }
            return pEnderecos;
        }

        public DataTable PegaDados(clsEnderecos pEnderecos, int pCodigo, bool pUltimoRegistro)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + " select Codigo, TipoCadastro, TipoEndereco, endereco, Numero, Bairro, \n";
                s = s + "        CEP, CodigoMunicipio, DDD1, Fone1, DDD2, Fone2, DDD3, Fone3, \n";
                s = s + "        DDDF, Fax, email, Contato, InstrucoesFat, Complemento, CargoContato \n"; 
                s = s + "from    Enderecos ";
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
                    s = s + " order by endereco ";
                }
                FillDataSet();                
            }
            catch (Exception ex)
            {
                return new DataTable();
            }
            finally
            {
                DesconectaBanco();
            }
            return l_dt;
        }
        public DataTable PreencheDataTableOrdem(string pOrdem)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + " select Codigo, TipoCadastro, TipoEndereco, endereco, Numero, Bairro, \n";
                s = s + "        CEP, CodigoMunicipio, DDD1, Fone1, DDD2, Fone2, DDD3, Fone3, \n";
                s = s + "        DDDF, Fax, email, Contato, InstrucoesFat, Complemento, CargoContato \n"; 
                s = s + " from   Enderecos \n";
                if (pOrdem == "Descrição") pOrdem = "Descricao";
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
                l_dt.NewRow();
                l_dt.Rows[0][0] = ex.Message;
                return l_dt;
            }
        }
        public DataTable PreencheDataTableEnderecosClientes(string pInCodigos, int pTipoEndereco)
        {
            try
            {
                ConectaBanco();
                s = "";
                s = s + " select e.Codigo, e.TipoCadastro, e.TipoEndereco, e.endereco, e.Numero, e.Bairro, \n";
                s = s + "        e.CEP, e.CodigoMunicipio, e.DDD1, e.Fone1, e.DDD2, e.Fone2, e.DDD3, e.Fone3, \n";
                s = s + "        e.DDDF, e.Fax, e.email, e.Contato, e.InstrucoesFat, e.Complemento, "; 
                s = s + "        Concat(m.Nome, ', ', e.Bairro, ', ', e.endereco, ', ', e.numero, ' ', e.complemento, ' |', c.PontoReferencia) as CidadeBairroEndereco \n";
                //"concat(m.Nome, '-', Bairro, '-', Endereco) as CidadeBairroEndereco \n";
                s = s + " from   Enderecos e \n";
                s = s + " inner  join Municipios m on m.Codigo = e.CodigoMunicipio \n";
                s = s + " left   join Clientes c on e.Codigo = c.Codigo \n";
                s = s + " where  e.Codigo in (" + pInCodigos + ") \n";
                s = s + " and    e.TipoEndereco = " + pTipoEndereco.ToString() + " \n";
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
        public int PegaTamanhoCampoVarChar(string pCampo)
        {
            int iRet = 0;
            try
            {
                ConectaBanco();
                s = "";
                s = s + "show   columns ";
                s = s + "from   Enderecos ";
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
        public string DadoExiste(int pCodigoCliente, int pTipoCadastro, int pTipoEndereco)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Codigo ";
                s = s + "from   Enderecos  ";
                s = s + "where  Codigo = " + pCodigoCliente + " ";
                s = s + "and    TipoCadastro = " + pTipoCadastro + " ";
                s = s + "and    TipoEndereco = " + pTipoEndereco + " ";
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pCodigoCliente != 0)
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
        public void Inserir(clsEnderecos pEnderecos, int pCodigoCliente, int pTipoCadastro, int pTipoEndereco)
        {
            try
            {
                s = "";
                s = s + "insert into Enderecos \n";
                s = s + "( \n";
                s = s + "        Codigo, TipoCadastro, TipoEndereco, endereco, Numero, Bairro, \n";
                s = s + "        CEP, CodigoMunicipio, DDD1, Fone1, DDD2, Fone2, DDD3, Fone3, \n";
                s = s + "        DDDF, Fax, email, Contato, InstrucoesFat, Complemento, CargoContato \n";
                s = s + ") \n";
                s = s + "values \n";
                s = s + "( \n";
                s = s + " " + pCodigoCliente.ToString() + ", \n";
                s = s + " " + pTipoCadastro.ToString() + ", \n";
                s = s + " " + pTipoEndereco.ToString() + ", \n";
                s = s + "'" + pEnderecos.endereco + "', \n";
                s = s + "'" + pEnderecos.Numero + "', \n";
                s = s + "'" + pEnderecos.Bairro + "', \n";
                s = s + "'" + pEnderecos.CEP + "', \n";
                if (pEnderecos.CodigoMunicipio.ToString() == "")
                    s = s + " 0, \n";
                else
                    s = s + " " + pEnderecos.CodigoMunicipio.ToString() + ", \n";
                if (pEnderecos.DDD1.ToString() != "")
                    s = s + " " + pEnderecos.DDD1.ToString() + ", \n";
                else
                    s = s + "0, \n";
                s = s + "'" + pEnderecos.Fone1 + "', \n";
                if (pEnderecos.DDD2.ToString() != "")
                    s = s + " " + pEnderecos.DDD2.ToString() + ", \n";
                else
                    s = s + "0, \n";
                s = s + "'" + pEnderecos.Fone2 + "', \n";
                if (pEnderecos.DDD3.ToString() != "")
                    s = s + " " + pEnderecos.DDD3.ToString() + ", \n";
                else
                    s = s + "0, \n";
                s = s + "'" + pEnderecos.Fone3 + "', \n";
                if (pEnderecos.DDDF.ToString() != "")
                    s = s + " " + pEnderecos.DDDF.ToString() + ", \n";
                else
                    s = s + "0, \n";
                s = s + "'" + pEnderecos.Fax + "', \n";
                s = s + "'" + pEnderecos.email + "', \n";
                s = s + "'" + pEnderecos.Contato + "', \n";
                s = s + "'" + pEnderecos.InstrucoesFat + "', \n";
                s = s + "'" + pEnderecos.Complemento + "', \n";
                s = s + "'" + pEnderecos.CargoContato + "' \n";
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
        public string Alterar(clsEnderecos pEnderecos, int pCodigoCliente, int pTipoCadastro, int pTipoEndereco)
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
                s = s + "update Enderecos \n";
                s = s + "set   endereco        = '" + pEnderecos.endereco + "', \n";
                s = s + "      Numero          = '" + pEnderecos.Numero + "', \n";
                s = s + "      Bairro          = '" + pEnderecos.Bairro + "', \n";
                s = s + "      CEP             = '" + pEnderecos.CEP + "', \n";
                if (pEnderecos.CodigoMunicipio.ToString() == "")
                    s = s + "  CodigoMunicipio = 0, \n";
                else
                    s = s + "  CodigoMunicipio = " + pEnderecos.CodigoMunicipio.ToString() + ", \n";
                if (pEnderecos.DDD1.ToString() != "")
                    s = s + "  DDD1            = " + pEnderecos.DDD1.ToString() + ", \n";
                else
                    s = s + "  DDD1            = 0, \n";
                s = s + "      Fone1           = '" + pEnderecos.Fone1 + "', \n";
                if (pEnderecos.DDD2.ToString() != "")
                    s = s + "  DDD2           = " + pEnderecos.DDD2.ToString() + ", \n";
                else
                    s = s + "  DDD2           = 0, \n";
                s = s + "      Fone2          = '" + pEnderecos.Fone2 + "', \n";
                if (pEnderecos.DDD3.ToString() != "")
                    s = s + "  DDD3            = " + pEnderecos.DDD3.ToString() + ", \n";
                else
                    s = s + "  DDD3            = 0, \n";
                s = s + "      Fone3           = '" + pEnderecos.Fone3 + "', \n";
                if (pEnderecos.DDDF.ToString() != "")
                    s = s + "  DDDF            = " + pEnderecos.DDDF.ToString() + ", \n";
                else
                    s = s + "  DDDF            = 0, \n";
                s = s + "      Fax             = '" + pEnderecos.Fax + "', \n";
                s = s + "      email           = '" + pEnderecos.email + "', \n";
                s = s + "      Contato         = '" + pEnderecos.Contato + "', \n";
                s = s + "      InstrucoesFat   = '" + pEnderecos.InstrucoesFat + "', \n";
                s = s + "      Complemento     = '" + pEnderecos.Complemento + "', \n";
                s = s + "      CargoContato    = '" + pEnderecos.CargoContato + "' \n";

                s = s + "where Codigo       = " + pCodigoCliente.ToString() + " \n";
                s = s + "and   TipoCadastro = " + pTipoCadastro.ToString() + " \n";
                s = s + "and   TipoEndereco = " + pTipoEndereco.ToString() + " \n";

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
        public string Excluir(int pCodigo, string pTipoCadastro, string pTipoEndereco)
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
                if (pCodigo > 0)
                {
                    s = s + "delete from Enderecos \n";
                    s = s + "where  Codigo = " + pCodigo.ToString() + " \n";
                    s = s + "and    TipoCadastro = " + pTipoCadastro.ToString() + " \n";
                    s = s + "and    TipoEndereco = " + pTipoEndereco.ToString() + " \n";
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