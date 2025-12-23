using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Drawing;
using System.Data.Sql;
using MySql.Data.MySqlClient;
using LibSILC;

public partial class forms_ewvs_Barras : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {              
        clsContaDados oContasDados = new clsContaDados();
               
        Chart1.DataMember = "ValorGrupo";
        DateTime _dti = Convert.ToDateTime(DateTime.Now.ToString("01/MM/yyyy"));
        DateTime _dtf = Convert.ToDateTime(DateTime.Now.AddMonths(2).ToString("06/MM/yyyy"));
        Chart1.DataSource = oContasDados.PegaDadosParaFluxoCaixa(_dti, _dtf, "EMABERTO", 0, 0);
        Chart1.DataBind();
    }
}

class clsDB
{
    private MySqlConnection l_mySqlConnect;
    private MySqlTransaction l_mySQLTransaction;
    public string NomeDB = "";

    public MySqlTransaction MySQLTransaction
    {
        get { return l_mySQLTransaction; }
        set { l_mySQLTransaction = value; }
    }

    public MySqlConnection MySqlConnect
    {
        get { return l_mySqlConnect; }
        set { l_mySqlConnect = value; }
    }
    private MySqlCommand l_mySqlCommad;

    public MySqlCommand MySqlCommad
    {
        get { return l_mySqlCommad; }
        set { l_mySqlCommad = value; }
    }
    private MySqlDataAdapter l_myData;
    private DataSet l_ds = new DataSet();
    private string s;
    public void ConectaMySql()
    {
        s = "";
        //página uol host
        s = "Persist Security Info=false;server=finentrega.mysql.uhserver.com;password=yes;uid=finentrega;database=finentrega;pwd=SA12vi01*";
        NomeDB = "ewvs";

        l_mySqlConnect = new MySqlConnection(s);
        try
        {
            l_mySqlConnect.Open();
        }
        catch (Exception ex)
        {
            s = "Erro db";
        }
    }

    public void DesconectaMySql()
    {
        l_mySqlConnect.Close();
    }
}
class clsConta
{
    private int l_Codigo;
    private string l_NomeConta;
    private string l_Db_Cr;
    private string l_CodigoContabil;
    private int l_EhContaMensal;
    private int l_DiaVencimento;

    public int DiaVencimento
    {
        get { return l_DiaVencimento; }
        set { l_DiaVencimento = value; }
    }

    public int EhContaMensal
    {
        get { return l_EhContaMensal; }
        set { l_EhContaMensal = value; }
    }
    private DateTime l_DataCadastro;

    public DateTime DataCadastro
    {
        get { return l_DataCadastro; }
        set { l_DataCadastro = value; }
    }


    public clsConta()
    {
        LimpaCampos();
    }

    public int Codigo
    {
        get { return l_Codigo; }
        set { l_Codigo = value; }
    }

    public string NomeConta
    {
        get { return l_NomeConta; }
        set { l_NomeConta = value; }
    }

    public string Db_Cr
    {
        // D - Débito
        // C - Crédito
        get { return l_Db_Cr; }
        set { l_Db_Cr = value; }
    }

    public string CodigoContabil
    {
        get { return l_CodigoContabil; }
        set { l_CodigoContabil = value; }
    }

    private void LimpaCampos()
    {
        l_NomeConta = "";
        l_Db_Cr = "";
        l_CodigoContabil = "";
        l_EhContaMensal = 0;
        l_DiaVencimento = 0;
    }
}

class clsContaDados
{
    private clsDB oDB = new clsDB();
    private MySqlDataAdapter l_myData;
    private DataSet l_ds = new DataSet();
    private string s;

    public DataTable PreencheDataTableContas()
    {
        oDB.ConectaMySql();
        s = "";
        s = s + "select NomeConta, Codigo ";
        s = s + "from   Contas order by NomeConta";
        l_ds = new DataSet();
        l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
        l_ds = new DataSet();
        l_myData.Fill(l_ds);
        oDB.DesconectaMySql();
        return l_ds.Tables[0];
    }
    public DataTable PreencheDataTableContas(string pProcura)
    {
        oDB.ConectaMySql();
        s = "";
        s = s + "select NomeConta, Codigo ";
        s = s + "from   Contas ";
        s = s + "where  NomeConta like '" + pProcura + "%' ";
        s = s + "order  by NomeConta ";
        l_ds = new DataSet();
        l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
        l_ds = new DataSet();
        l_myData.Fill(l_ds);
        oDB.DesconectaMySql();
        return l_ds.Tables[0];
    }

    public DataTable PegaDadosParaFluxoCaixa(DateTime pDataInicial, DateTime pDataFinal, string pTipo, int pConta, int pCliente)
    {
        oDB.ConectaMySql();
        s = "";
        s = s + "select * from \n ";
        s = s + "( \n";
        s = s + "  select c.NomeConta, sum(cr.Valor) as ValorGrupo, cr.DataVencimento as Vencimento \n";
        s = s + "  from   ContasReceber cr  \n";
        s = s + "  left  join Contas c on c.Codigo = cr.CodigoConta  \n";
        s = s + "  where cr.DataVencimento >= '" + pDataInicial.ToString("yyyy/MM/dd") + "'  \n";
        s = s + "  and   cr.DataVencimento <= '" + pDataFinal.ToString("yyyy/MM/dd") + "'  \n";
        if (pConta > 0)
            s = s + "  and  cr.CodigoConta = " + pConta.ToString() + "  \n";
        if (pTipo == "EMABERTO")
        {
            s = s + "  and   (cr.DataPagamento is null or cr.DataPagamento < '2000/01/01')  \n";
        }
        else if (pTipo == "PAGO")
        {
            s = s + "  and    (cr.DataPagamento > '1901/01/01')  \n";
        }        
        s = s + "  group by c.NomeConta \n";
        s = s + "  union all  \n";
        s = s + "  select c.NomeConta, sum(cp.Valor) as ValorGrupo, cp.DataVencimento as Vencimento  \n";
        s = s + "  from   ContasPagar cp  \n";
        s = s + "  left  join Contas c on c.Codigo = cp.CodigoConta  \n";
        s = s + "  where cp.DataVencimento >= '" + pDataInicial.ToString("yyyy/MM/dd") + "'  \n";
        s = s + "  and   cp.DataVencimento <= '" + pDataFinal.ToString("yyyy/MM/dd") + "'  \n";
        if (pConta > 0)
            s = s + "  and  cp.CodigoConta = " + pConta.ToString() + "  \n";
        if (pTipo == "EMABERTO")
        {
            s = s + "  and   (cp.DataPagamento is null or cp.DataPagamento < '2000/01/01')  \n";
        }
        else if (pTipo == "PAGO")
        {
            s = s + "  and    (cp.DataPagamento > '1901/01/01')  \n";
        }
        s = s + "  group by c.NomeConta \n";

        s = s + "  union all  \n";
        s = s + "  select c.NomeConta, sum(cd.Valor) as ValorGrupo, cd.Data as Vencimento \n";
        s = s + "  from   ContasDiversos cd  \n";
        s = s + "  left  join Contas c on c.Codigo = cd.CodigoConta  \n";
        s = s + "  where cd.Data >= '" + pDataInicial.ToString("yyyy/MM/dd") + "'  \n";
        s = s + "  and   cd.Data <= '" + pDataFinal.ToString("yyyy/MM/dd") + "'  \n";
        s = s + "  group by c.NomeConta \n";

        s = s + ") x \n";

        s = s + "  order by Vencimento \n ";
        l_ds = new DataSet();
        l_myData = new MySqlDataAdapter(s, oDB.MySqlConnect);
        l_ds = new DataSet();
        l_myData.Fill(l_ds);
        oDB.DesconectaMySql();
        return l_ds.Tables[0];
    }
}