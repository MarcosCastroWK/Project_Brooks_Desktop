<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Parametros.aspx.cs" Inherits="Parametros" %>
<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>
<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc2" %>
<%@ Register src="INTEIRO.ascx" tagname="INTEIRO" tagprefix="uc3" %>
<%@ Register src="INTEIRO7.ascx" tagname="INTEIRO7" tagprefix="uc4" %>
<%@ Register src="MOEDA.ascx" tagname="MOEDA" tagprefix="uc5" %>

<%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc6" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="table.css" type="text/css" rel="stylesheet" />
    <link href="aspx.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .auto-style6
        {
            width: 90px;
            font-size: 10px;
            height: 26px;
        }
        .auto-style8
        {
            width: 10px;
            font-size: 9px;
            height: 24px;
        }
        .auto-style9
        {
            width: 120px;
            font-size: 9px;
            height: 24px;
        }
        .auto-style10
        {
            width: 10px;
            font-size: 9px;
            height: 26px;
        }
        .auto-style11
        {
            width: 10px;
            font-size: 9px;
            height: 28px;
        }
    </style>
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
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
                        <uc2:menu ID="menu" runat="server" Visible="false" />
                        
                    </td>
                </tr>
                <tr>
                    <td id="dadosCab" valign="top" style="height:1%; font-size:9pt;">
                        <asp:Label ID="lblTitulo" runat="server" Text="&nbsp;Cadastro&amp;nbsp;de&amp;nbsp;Parâmetros" Font-Bold="True" CssClass="titulo2"></asp:Label>
                     </td>
                </tr>
                <tr>
                    <td valign="top" style="height:1%;">
                        <div id="contentTopRightDiv" style="width:98%;height:200px; overflow-y:scroll;border:ridge 5px;font-size:10pt;" >
                            <asp:GridView ID="Grade" runat="server" CellPadding="0" Width="50%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" OnRowCommand="Grade_RowCommand" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound" ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnSorting="Grade_Sorting" Font-Bold="False" >
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnMudarSernha" runat="server" ImageUrl="~/Images/mudarmostrar.png" ToolTip="Ver/Alterar" OnClick="ibnMudar_Click" CommandArgument='<%# Container.DataItemIndex %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibnExcluir" runat="server" ImageUrl="~/Images/excluir.png" ToolTip="Excluir" OnClick="ibnExcluir_Click" CommandArgument='<%# Container.DataItemIndex %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="1%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Numero" HeaderText="Número" SortExpression="Numero">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Nome" HeaderText="Nome" SortExpression="Nome">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="300px" />
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
                    <td id="dadosForm" valign="top" style="height:80%">
                        <table style="width:300px">
                            <tr>
                                <td class="auto-style10">
                                    <asp:Label ID="lblNovaSenha" CssClass="LetrasLabel" runat="server" Font-Bold="False" >&nbsp;Nome</asp:Label>
                                </td>
                                <td class="auto-style10">
                                    <asp:TextBox ID="txtNome" runat="server" TabIndex="1" Width="300px"></asp:TextBox>
                                </td>
                                <td class="auto-style10">
                                    &nbsp;</td>
                                <td class="auto-style10">
                                    &nbsp;</td>
                                <td class="auto-style10">
                                    <asp:Label ID="Label4" runat="server" Text="CNPJ/CPF" Font-Bold="False" CssClass="LetrasLabel"></asp:Label>
                                </td>
                                <td class="auto-style10">
                                    <asp:TextBox ID="txtCNPJ_CPF" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" Width="200px" ></asp:TextBox>
                                        </td>
                            </tr>
                            <tr>
                                <td class="auto-style11">
                                    <asp:Label CssClass="LetrasLabel" ID="Label1" runat="server" Font-Bold="False">&nbsp;Endereço</asp:Label>
                                </td>
                                <td class="auto-style11">
                                    <asp:TextBox ID="txtEndereco" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" Width="300px" ></asp:TextBox>
                                </td>
                                <td class="auto-style11">
                                </td>
                                <td class="auto-style11">
                                </td>
                                <td class="LetrasLabel">
                                    
                                    <asp:Label ID="Label6" runat="server" Text="Inscrição/RG" Font-Bold="False" CssClass="LetrasLabel"></asp:Label>
                                    
                                </td>
                                <td class="auto-style11">
                                    <asp:TextBox ID="txtIE_RG" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" Width="119px" ></asp:TextBox>
                                        </td>
                            </tr>
                            <tr>
                                <td class="LetrasLabel">
                                    <asp:Label ID="Label3" CssClass="LetrasLabel" runat="server" Font-Bold="False">&nbsp;Cidade</asp:Label>
                                </td>
                                <td class="auto-style6">
                                    <asp:TextBox ID="txtCidade" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="250px"></asp:TextBox>
                                </td>
                                <td class="auto-style6">
                                    <asp:Label ID="Label5" runat="server" Font-Bold="False" CssClass="LetrasLabel">&nbsp;CEP</asp:Label>
                                </td>
                                <td class="auto-style6">
                                    <asp:TextBox ID="txtCEP" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="80px"></asp:TextBox>
                                </td>
                                <td class="auto-style6">
                                    
                                    <asp:Label ID="Label9" runat="server" Text="Código muncpio" Font-Bold="False" CssClass="LetrasLabel" Width="80px"></asp:Label>
                                    
                                </td>
                                <td class="auto-style6">
                                    <asp:TextBox ID="txtCodigoMunicipio" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" Width="84px" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">
                                    <asp:Label ID="Label8" CssClass="LetrasLabel" runat="server" Width="100px" Font-Bold="False">&nbsp;Telefones</asp:Label>
                                </td>
                                <td class="LetrasTD">
                                    <asp:TextBox ID="txtFone" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="100px"></asp:TextBox>
                                </td>
                                <td class="LetrasTD">
                                    <asp:Label ID="Label7" runat="server" Font-Bold="False" CssClass="LetrasLabel">&nbsp;UF</asp:Label>
                                </td>
                                <td class="LetrasTD">
                                    <asp:TextBox ID="txtUF" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="30px"></asp:TextBox>
                                </td>
                                <td class="LetrasTD">
                                    &nbsp;</td>
                                <td class="LetrasTD">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style8">
                                    
                                    <asp:Label ID="Label10" runat="server" Text="&amp;nbsp;e-Mail" Font-Bold="False" CssClass="LetrasLabel" Width="80px"></asp:Label>
                                    
                                    </td>
                                <td class="auto-style8">
                                    <asp:TextBox ID="txtEmail" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" Width="334px" ></asp:TextBox>
                                </td>
                                <td class="auto-style8">
                                    &nbsp;</td>
                                <td class="auto-style8">
                                    &nbsp;</td>
                                <td class="auto-style8">
                                    &nbsp;</td>
                                <td class="auto-style8">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style8">
                                    <asp:Label ID="Label2" runat="server" Font-Bold="False">&nbsp;</asp:Label>
                                    
                                    <asp:Label ID="Label11" runat="server" Text="&amp;nbsp;Site" Font-Bold="False" CssClass="LetrasLabel" Width="80px"></asp:Label>
                                    
                                </td>
                                <td class="auto-style9">
                                    <asp:TextBox ID="txtSite" onfocus="LimpaErro()" runat="server" TextMode="SingleLine" TabIndex="1" Width="334px" ></asp:TextBox>
                                </td>
                                <td class="auto-style9">
                                    &nbsp;</td>
                                <td class="auto-style9">
                                    &nbsp;</td>
                                <td class="auto-style9">
                                    &nbsp;</td>
                                <td class="auto-style9">
                                    <div style="width:160px">
                                        <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Ok" ID="Salvar" OnClick="Salvar_Click" TabIndex="2" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" TabIndex="2" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">&nbsp;</td>
                                <td class="LetrasTD">
                                    &nbsp;</td>
                                <td class="LetrasTD">
                                    &nbsp;</td>
                                <td class="LetrasTD">
                                    &nbsp;</td>
                                <td class="LetrasTD">
                                    &nbsp;</td>
                                <td class="LetrasTD">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width:1%">
                                    <asp:HiddenField ID="hifCodigo" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="lblMensagem" CssClass="LetrasLabel" runat="server"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
