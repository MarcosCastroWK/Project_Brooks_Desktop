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
	public class clsAbastecimentoDados
	{
	    private clsDB oDB = new clsDB();
        private MySqlDataAdapter l_myData;
        private DataSet l_ds = new DataSet();
        private DataTable l_dt = new DataTable();
        private string s;
        
        public clsAbastecimento PegaDados(clsAbastecimento pAbastecimentos, int pSequencial)
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Sequencial, Data, Hora, CodigoCaminhao, CodigoMotorista, Km, AcumuladoAnterior, AcumuladoAtual, Litros, \n";
                s = s + "       DiferencaLitros, DiferencaAcumuladaLitros, CompraDiesel, CompraAculumadaDiesel, Estoque, \n";
                s = s + "       PrecoLitro, flagAbastExterno, ValorCompraDiesel \n";
                s = s + "from   Abastecimento ";
                if (pSequencial > 0)
                {
                    s = s + "where  Sequencial = " + pSequencial.ToString() + " limit 1 ";
                    l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                    l_ds = new DataSet();
                    l_myData.Fill(l_ds);                   
                    if (l_ds.Tables[0].Rows.Count > 0)
                    {
                        l_dt = l_ds.Tables[0];                       
                        pAbastecimentos.Sequencial = Convert.ToInt32(l_dt.Rows[0]["Sequencial"]);
                        if (l_dt.Rows[0]["Data"].ToString() != "")
                            pAbastecimentos.Data = Convert.ToDateTime(l_dt.Rows[0]["Data"].ToString()).Date.ToShortDateString();
                        pAbastecimentos.Hora = l_dt.Rows[0]["Hora"].ToString();
                        if (l_dt.Rows[0]["CodigoCaminhao"].ToString() != "")
                            pAbastecimentos.CodigoCaminhao = Convert.ToInt32(l_dt.Rows[0]["CodigoCaminhao"]);
                        if (l_dt.Rows[0]["CodigoMotorista"].ToString() != "")
                            pAbastecimentos.CodigoMotorista = Convert.ToInt32(l_dt.Rows[0]["CodigoMotorista"]);
                        if (l_dt.Rows[0]["Km"].ToString() != "")
                            pAbastecimentos.Km = Convert.ToInt32(l_dt.Rows[0]["Km"]);
                        if (l_dt.Rows[0]["AcumuladoAnterior"].ToString() != "")
                            pAbastecimentos.AcumuladoAnterior = Convert.ToInt32(l_dt.Rows[0]["AcumuladoAnterior"]);
                        if (l_dt.Rows[0]["AcumuladoAtual"].ToString() != "")
                            pAbastecimentos.AcumuladoAtual = Convert.ToInt32(l_dt.Rows[0]["AcumuladoAtual"]);
                        if (l_dt.Rows[0]["Litros"].ToString() != "")
                            pAbastecimentos.Litros = Convert.ToDecimal(l_dt.Rows[0]["Litros"]);
                        if (l_dt.Rows[0]["DiferencaLitros"].ToString() != "")
                            pAbastecimentos.DiferencaLitros = Convert.ToDecimal(l_dt.Rows[0]["DiferencaLitros"]);
                        if (l_dt.Rows[0]["DiferencaAcumuladaLitros"].ToString() != "")
                            pAbastecimentos.DiferencaAcumuladaLitros = Convert.ToDecimal(l_dt.Rows[0]["DiferencaAcumuladaLitros"]);
                        if (l_dt.Rows[0]["CompraDiesel"].ToString() != "")
                            pAbastecimentos.CompraDiesel = Convert.ToDecimal(l_dt.Rows[0]["CompraDiesel"]);
                        if (l_dt.Rows[0]["CompraAculumadaDiesel"].ToString() != "")
                            pAbastecimentos.CompraAculumadaDiesel = Convert.ToDecimal(l_dt.Rows[0]["CompraAculumadaDiesel"]);
                        if (l_dt.Rows[0]["Estoque"].ToString() != "")
                            pAbastecimentos.Estoque = Convert.ToDecimal(l_dt.Rows[0]["Estoque"]);
                        if (l_dt.Rows[0]["PrecoLitro"].ToString() != "")
                            pAbastecimentos.PrecoLitro = Convert.ToDecimal(l_dt.Rows[0]["PrecoLitro"]);
                        if (l_dt.Rows[0]["flagAbastExterno"].ToString() != "")
                            pAbastecimentos.flagAbastExterno = Convert.ToInt16(l_dt.Rows[0]["flagAbastExterno"]);
                        if (l_dt.Rows[0]["ValorCompraDiesel"].ToString() != "")
                            pAbastecimentos.ValorCompraDiesel = Convert.ToDecimal(l_dt.Rows[0]["ValorCompraDiesel"]);
                    }
                }
                oDB.DesconectaMySql();
                return pAbastecimentos;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
                return pAbastecimentos;
            }
        }
        private void FillDataSet()
        {
            l_ds = new DataSet();
            l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
            l_myData.Fill(l_ds);
            l_dt = l_ds.Tables[0];
        }
        public string DadoExiste(int pSequencial)
        {
            string sRet = "";
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Sequencial ";
                s = s + "from   Abastecimento  ";
                if (pSequencial != 0)
                {
                    s = s + "where  Sequencial = " + pSequencial + " ";
                }
                FillDataSet();
                if (l_dt.Rows.Count > 0 && pSequencial != 0)
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
        public DataTable PegaDados(clsAbastecimento pAbastecimento, int pSequencial, bool pUltimoRegistro)
        {
            try
            {
                oDB.ConectaMySql();
                s = "";
                s = s + "select Data, Hora, CodigoCaminhao, CodigoMotorista, Km, AcumuladoAnterior, AcumuladoAtual, Litros, \n";
                s = s + "       DiferencaLitros, DiferencaAcumuladaLitros, CompraDiesel, CompraAculumadaDiesel, Estoque, \n";
                s = s + "       PrecoLitro, flagAbastExterno, ValorCompraDiesel \n";
                s = s + "from   Abastecimento ";
                if (pSequencial > 0)
                {
                    s = s + "where  Sequencial = " + pSequencial + " limit 1 ";
                }
                if (pUltimoRegistro)
                {
                    s = s + "order by Sequencial desc limit 1 ";
                }
                else if (pSequencial == 0)
                {
                    s = s + " order by Data ";
                }
                l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
                l_ds = new DataSet();
                l_myData.Fill(l_ds);
                l_dt = l_ds.Tables[0];
                oDB.DesconectaMySql();
                return l_dt;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
                oDB.DesconectaMySql();
                return new DataTable();
            }
        }

        public void Inserir(clsAbastecimento pAbastecimento, int pSequencial = 0)
        {
            oDB.MySqlConnect.Open();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "insert into Abastecimento ";
                s = s + "(";
                if (pSequencial > 0)
                    s = s + "Sequencial, \n";
                s = s + "  Data, Hora, CodigoCaminhao, CodigoMotorista, Km, AcumuladoAnterior, AcumuladoAtual, Litros, \n";
                s = s + "  DiferencaLitros, DiferencaAcumuladaLitros, CompraDiesel, CompraAculumadaDiesel, Estoque, \n";
                s = s + "  PrecoLitro, flagAbastExterno, ValorCompraDiesel \n";
                s = s + ")";
                s = s + "values ";
                s = s + "(";
                if (pSequencial > 0)
                    s = s + pSequencial + ", \n";
                if (pAbastecimento.Data != "")
                    s = s + "'" + Convert.ToDateTime(pAbastecimento.Data).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "'0001-01-01', \n"; 
                s = s + "'" + pAbastecimento.Hora + "', ";
                s = s + " " + pAbastecimento.CodigoCaminhao + ", ";
                s = s + " " + pAbastecimento.CodigoMotorista + ", ";
                s = s + " " + pAbastecimento.Km + ", ";
                s = s + " " + pAbastecimento.AcumuladoAnterior.ToString().Replace(",", ".") + ", \n";
                s = s + " " + pAbastecimento.AcumuladoAtual.ToString().Replace(",", ".") + ", \n";
                s = s + " " + pAbastecimento.Litros.ToString().Replace(",", ".") + ", \n";
                s = s + " " + pAbastecimento.DiferencaLitros.ToString().Replace(",", ".") + ", \n";
                s = s + " " + pAbastecimento.DiferencaAcumuladaLitros.ToString().Replace(",", ".") + ", \n";
                s = s + " " + pAbastecimento.CompraDiesel.ToString().Replace(",", ".") + ", \n";
                s = s + " " + pAbastecimento.CompraAculumadaDiesel.ToString().Replace(",", ".") + ", \n";
                s = s + " " + pAbastecimento.Estoque.ToString().Replace(",", ".") + ", \n";
                s = s + " " + pAbastecimento.PrecoLitro.ToString().Replace(",", ".") + ", \n";
                s = s + " " + pAbastecimento.flagAbastExterno + ", ";
                s = s + " " + pAbastecimento.ValorCompraDiesel.ToString().Replace(",", ".") + " \n";
                s = s + ")";
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                //MessageBox.Show(ex.Message);
            }
            finally
            {                
                oDB.DesconectaMySql();
            }
        }
        public void Alterar(clsAbastecimento pAbastecimento, int pSequencial)
        {
            oDB.MySqlConnect.Open();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "update Abastecimento ";
                
                if (pAbastecimento.Data != "")
                    s = s + "set Data = '" + Convert.ToDateTime(pAbastecimento.Data).ToString("yyyy-MM-dd") + "', \n";
                else
                    s = s + "set Data = '0001-01-01', \n";
                s = s + " Hora = '" + pAbastecimento.Hora + "', \n";
                s = s + " CodigoCaminhao = " + pAbastecimento.CodigoCaminhao + ", \n";
                s = s + " CodigoMotorista = " + pAbastecimento.CodigoMotorista + ", \n";
                s = s + " Km = " + pAbastecimento.Km + ", \n";
                s = s + " AcumuladoAnterior = " + pAbastecimento.AcumuladoAnterior.ToString().Replace(",", ".") + ", \n";
                s = s + " AcumuladoAtual = " + pAbastecimento.AcumuladoAtual.ToString().Replace(",", ".") + ", \n";
                s = s + " Litros = " + pAbastecimento.Litros.ToString().Replace(",", ".") + ", \n";
                s = s + " DiferencaLitros = " + pAbastecimento.DiferencaLitros.ToString().Replace(",", ".") + ", \n";
                s = s + " DiferencaAcumuladaLitros = " + pAbastecimento.DiferencaAcumuladaLitros.ToString().Replace(",", ".") + ", \n";
                s = s + " CompraDiesel = " + pAbastecimento.CompraDiesel.ToString().Replace(",", ".") + ", \n";
                s = s + " CompraAculumadaDiesel = " + pAbastecimento.CompraAculumadaDiesel.ToString().Replace(",", ".") + ", \n";
                s = s + " Estoque = " + pAbastecimento.Estoque.ToString().Replace(",", ".") + ", \n";
                s = s + " PrecoLitro = " + pAbastecimento.PrecoLitro.ToString().Replace(",", ".") + ", \n";
                s = s + " flagAbastExterno = " + pAbastecimento.flagAbastExterno + ", \n";
                s = s + " ValorCompraDiesel = " + pAbastecimento.ValorCompraDiesel.ToString().Replace(",", ".") + " \n";

                s = s + "where Sequencial = " + pSequencial.ToString();
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
        public DataTable PreencheDataTable()
        {
            oDB.ConectaMySql();
            s = "";
            s = s + "select Sequencial, Data, Hora, CodigoCaminhao, CodigoMotorista, Km, AcumuladoAnterior, AcumuladoAtual, Litros, \n";
            s = s + "       DiferencaLitros, DiferencaAcumuladaLitros, CompraDiesel, CompraAculumadaDiesel, Estoque, \n";
            s = s + "       PrecoLitro, flagAbastExterno, ValorCompraDiesel \n";
            s = s + "from   Abastecimento order by Data";
            l_ds = new DataSet();
            l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
            l_ds = new DataSet();
            l_myData.Fill(l_ds);
            oDB.DesconectaMySql();
            return l_ds.Tables[0];
        }
        public void Excluir(int pSequencial,  string pNome)
        {
            oDB.MySqlConnect.Open();
            MySqlCommand command = oDB.MySqlConnect.CreateCommand();
            MySqlTransaction transaction;
            transaction = oDB.MySqlConnect.BeginTransaction();
            command.Connection = oDB.MySqlConnect;
            command.Transaction = transaction;
            try
            {
                s = "";
                s = s + "delete from Abastecimento ";
                s = s + "where  Sequencial = " + pSequencial.ToString();
                command.CommandText = s;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            finally
            {
                oDB.DesconectaMySql();
            }
        }
    }
}