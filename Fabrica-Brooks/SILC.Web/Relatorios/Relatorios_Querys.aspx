<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Relatorios_Querys.aspx.cs" Inherits="SILC.Web.Relatorios.Relatorios_Querys" %>

<%@ Register src="../forms/cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>

<%@ Register src="../forms/CLIENTESCONTROL.ascx" tagname="CLIENTESCONTROL" tagprefix="uc2" %>

<%@ Register src="../forms/DATA.ascx" tagname="DATA" tagprefix="uc3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../forms/table.css" type="text/css" rel="stylesheet" />
    <link href="../forms/aspx.css" type="text/css" rel="stylesheet" />
    <script src="../Scripts/funcaoGeral.js"></script>
    <script>
        function Imprime()
        {
            try {
                document.getElementById('Operacoes').style.visibility = "hidden";   
                document.getElementById('Panel1').style.position = "absolute";
                document.getElementById('Panel1').style.top = 0;
                window.print();
            }
            finally {
                document.getElementById('Operacoes').style.visibility = "visible";
                document.getElementById('Panel1').style.position = "";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div id="Operacoes">
            <uc1:cabecalho ID="cabecalho1" runat="server" />
    
            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo2" Text="Relatórios em geral"></asp:Label>
    
            <br />
            <br />
            <table>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnImprimir_Click" OnClientClick="DesabilitaOperacoes(this.id, '', '', 'Relatórios em geral');" />
                                </td>
                                <td>
                                    <asp:Button ID="btnCancelar" runat="server" Text="Voltar" PostBackUrl="~/forms/Menu.aspx" />    
                                </td>
                                <td>
                                    <input id="btnImprimir" type="button" value="Imprimir" onclick="Imprime();" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="imbExcel" runat="server" ImageUrl="~/Images/excel2010.png" OnClick="imbExcel_Click" />
                                </td>
                                <td>
                                    <asp:Label ID="lblNomeRelatorio" runat="server" Text="Relatórios"></asp:Label>
                                </td>
                                <td>

                                    <asp:DropDownList ID="ddlRelatorios" runat="server" Height="16px" Width="573px" OnSelectedIndexChanged="ddlRelatorios_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;Linhas:</td>
                                <td>
                                    <asp:Label ID="lblLinhas" runat="server" Text="0"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    Salvar como</td>
                                <td>

                                    <asp:TextBox ID="txtSalvarComo" runat="server" Width="563px"></asp:TextBox>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtQuery" runat="server" Height="247px" TextMode="MultiLine" Width="973px">select distinct c.Codigo as `Código Cliente`, c.NomeFantasia as `Nome Fantasia`, c.Nome as `Razão Social`,
       c.CNPJ_CPF, c.SenhaAcessoFatima `Senha Acesso MTR`,  
       (select Contato from enderecos where Codigo = c.Codigo and tipoendereco = 0 limit 1) as ContatoPadrao,
       (select Contato from enderecos where Codigo = c.Codigo and tipoendereco = 2 limit 1) as ContatoColeta
from   Clientes c
left   join Lancamentos l on l.CodigoCliente = c.Codigo 
where  l.DataRetirada > 20220101 and l.DataRetirada < 20220630
limit 2000
                        </asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <asp:Image ID="imgGirando" runat="server" ImageUrl="~/Images/aguarde.gif" style="visibility:hidden; height:1px;" />
        </div>
        <asp:Panel ID="Panel1" runat="server">
            <asp:Label ID="lblTitulo0" runat="server" BackColor="White" CssClass="titulo2" Text="Relatório de "></asp:Label>
            <br />
            <br />
            <asp:GridView ID="Grade" runat="server" PageSize="45" Width="1200px" CssClass="LetrasLabel" OnRowDataBound="Grade_RowDataBound" CellPadding="4" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#242121" />
            </asp:GridView>
            <br />
            <br />
        </asp:Panel>
        
    </form>
</body>
</html>
