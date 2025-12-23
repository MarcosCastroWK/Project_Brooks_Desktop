<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TipoAcondicionamento.aspx.cs" Inherits="forms_TipoAcondicionamento" %>
<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>
<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc2" %>
<%@ Register src="INTEIRO.ascx" tagname="INTEIRO" tagprefix="uc3" %>
<%@ Register src="INTEIRO7.ascx" tagname="INTEIRO7" tagprefix="uc4" %>
<%@ Register src="MOEDA.ascx" tagname="MOEDA" tagprefix="uc5" %>

<%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc6" %>

<%@ Register src="INTEIRO2.ascx" tagname="INTEIRO2" tagprefix="uc7" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="table.css" type="text/css" rel="stylesheet" />
    <link href="aspx.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .auto-style10
        {
            width: 10px;
            font-size: 9px;
            height: 26px;
        }
        .auto-style11
        {
            width: 59px;
            font-size: 9px;
            height: 26px;
        }
        .auto-style12
        {
            width: 59px;
            font-size: 9px;
        }
        .auto-style13 {
            width: 300px;
        }
        .auto-style14 {
            height: 4%;
        }
        hr {           
            border: groove 0px;
            height: 2px;
            background: bisque;
        }
        </style>
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<script>
    function ExpandeDiv(pId)
    {
        if (document.getElementById('contentTopRightDiv').style.height == '400px')
        {
            document.getElementById('contentTopRightDiv').style.height = '200px';
        }
        else
        {
            document.getElementById('contentTopRightDiv').style.height = '400px'
        }
    }
</script>
<body>
     <form id="form1" runat="server">
        <div>
            <table class="form2">
                <tr>
                    <td style="height:1%;">
                        <uc1:cabecalho ID="cabecalho1" runat="server" ClientIDMode="Inherit" EnableViewState="True" />
                    </td>
                </tr>
                <tr>
                    <td class="Menu">
                        <uc2:menu ID="menu1" runat="server" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td id="dadosCab" valign="top" style="height:1%; font-size:9pt;">
                        <asp:Label ID="lblTitulo" runat="server" Text=" Cadastro de Tipo Acondicionamento" Font-Bold="True" CssClass="titulo2"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="auto-style14">
                        <div id="contentTopRightDiv" style="width:98%; overflow-y:scroll;border:ridge 5px;font-size:10pt; min-height: 10vh; max-height: 66vh;">
                            <asp:GridView ID="Grade" runat="server" CellPadding="3" Width="40%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" OnRowCommand="Grade_RowCommand" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound" ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnSorting="Grade_Sorting" Font-Bold="False">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnMudarSernha" runat="server" ImageUrl="~/Images/mudarmostrar.png" ToolTip="Ver/Alterar" CommandArgument='<%# Container.DisplayIndex%>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnExcluir" runat="server" ImageUrl="~/Images/excluir.png" ToolTip="Excluir" OnClick="ibnExcluir_Click"  CommandArgument='<%# Container.DisplayIndex%>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Codigo" HeaderText="Código" SortExpression="Codigo">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" Width="30px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Descricao" HeaderText="Descrição" SortExpression="Descricao">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                <PagerStyle ForeColor="Black" HorizontalAlign="Center" BackColor="#999999" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#383838" />
                            </asp:GridView>                            
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left; border:0; word-spacing: 0;">
                        <hr id="idHR" style="height: 10px;" onclick="ExpandeDiv(this.id);" title="Click para expandir ou retrair" />
                    </td>
                </tr>
                <tr>
                    <td id="dadosForm" valign="top" style="height:80%">
                        <table class="auto-style13">
                            <tr>
                                <td class="auto-style10">
                                    <asp:Label ID="lblCodigo" CssClass="LetrasLabel" runat="server" Font-Bold="False" >&nbsp;Código</asp:Label>
                                </td>
                                <td class="auto-style11">
                                    <asp:Label ID="lblCodigoMostrado" CssClass="LetrasLabel" runat="server" Font-Bold="False" >00</asp:Label>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style10">
                                    <asp:Label ID="lblDescricao" CssClass="LetrasLabel" runat="server" Font-Bold="False" >&nbsp;Descrição</asp:Label>
                                </td>
                                <td class="auto-style11">
                                    <asp:TextBox ID="txtDescricao" runat="server" TabIndex="1" ></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">&nbsp;</td>
                                <td class="auto-style12">
                                    <div style="width:160px">
                                        <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Ok" ID="Salvar" OnClick="Salvar_Click" TabIndex="18" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Novo" OnClick="btnCancelar_Click" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:1%">
                                    <asp:HiddenField ID="hifCodigo" runat="server" />
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="lblMensagem" runat="server" CssClass="LetrasLabel"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="hifEspande" runat="server" Value="200px" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
