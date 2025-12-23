<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocumentosPagina.aspx.cs" Inherits="SILC.Web.forms.DocumentosPagina" %>
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
        .auto-style10
        {
            width: 10px;
            font-size: 9px;
            height: 26px;
        }
        .auto-style11
        {
            height: 1%;
        }
        .auto-style12 {
            width: 1%;
            height: 16px;
        }
        .auto-style13 {
            height: 16px;
        }
        </style>
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<script> 
    function ArquivoEscolhido()
    {
        document.getElementById('txtDescricao').innerText = 'teste';
    }
</script>
<body>
     <form id="form1" runat="server">
        <div>
            <table class="form2">
                <tr>
                    <td class="auto-style11">
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
                        <asp:Label ID="lblTitulo" runat="server" Text="&amp;nbsp;Documentos gerais (LAO/Álvara/ISO 9002)" Font-Bold="True" CssClass="titulo2"></asp:Label>
                     </td>
                </tr>
                <tr>
                    <td valign="top" style="height:1%;">
                        <div id="contentTopRightDiv" style="width:98%; overflow-y:scroll;border:ridge 5px;font-size:10pt;" >
                            <asp:GridView ID="Grade" runat="server" CellPadding="3" Width="50%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" OnRowCommand="Grade_RowCommand" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound" ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnSorting="Grade_Sorting" Font-Bold="False" >
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
                                    <asp:BoundField DataField="Id" HeaderText="Código" SortExpression="Id">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Descricao" HeaderText="Descrição" SortExpression="Descricao" >
                                    <ItemStyle CssClass="padItemGrade" Width="300px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BROOKS" HeaderText="BROOKS" SortExpression="BROOKS">
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Periodo" HeaderText="Período" SortExpression="Periodo" />
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
                                    <asp:Label CssClass="LetrasLabel" ID="lblTipo" runat="server" Font-Bold="False" Width="100px">&nbsp;Tipo</asp:Label>
                                </td>
                                <td class="auto-style10">
                                    <asp:DropDownList ID="ddlTipo" runat="server" Width="310px">
                                        <asp:ListItem>ALVARAFUNC</asp:ListItem>
                                        <asp:ListItem>ALVARASANIT</asp:ListItem>
                                        <asp:ListItem>LAO</asp:ListItem>
                                        <asp:ListItem>CERTISO</asp:ListItem>
                                        <asp:ListItem>CTFIBAMA</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label CssClass="LetrasLabel" ID="lblFazerUpload" runat="server" Font-Bold="False" Width="275px">&nbsp;Escolha o documento para fazer o upload</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style10">
                                    <asp:Label CssClass="LetrasLabel" ID="Label1" runat="server" Font-Bold="False" Width="100px">&nbsp;Descrição</asp:Label>
                                </td>
                                <td class="auto-style10">
                                    <asp:TextBox ID="txtDescricao" runat="server" TabIndex="1" Width="300px" OnInit="txtDescricao_Init" OnTextChanged="txtDescricao_TextChanged"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:FileUpload ID="UploadDocumento" runat="server" Width="359px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">
                                    <asp:Label CssClass="LetrasLabel" ID="Label4" runat="server" Font-Bold="False" Width="100px">&nbsp;Período</asp:Label>
                                </td>
                                <td class="LetrasTD">
                                    <asp:TextBox ID="txtPeriodo" runat="server" TabIndex="1" Width="300px" OnInit="txtDescricao_Init" OnTextChanged="txtDescricao_TextChanged"></asp:TextBox>
                                </td>
                                <td class="LetrasTD">
                                    <asp:Label CssClass="LetrasLabel" ID="Label2" runat="server" Font-Bold="False" Width="355px">* além de cadastrar o documento para mostrar na página para todos os clientes, também envia o documento (pdf)</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">
                                    &nbsp;</td>
                                <td class="LetrasTD">
                                    <asp:CheckBox ID="chkBROOKS" runat="server" Text="BROOKS" Width="100px" onclick="TiraCheckTerceiro();" Font-Bold="False" TabIndex="2" CssClass="LetrasLabel"/>
                                </td>
                                <td class="LetrasTD">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">&nbsp;</td>
                                <td class="LetrasTD">
                                    <div style="width:303px">
                                        <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Ok" ID="Salvar" OnClick="Salvar_Click" TabIndex="1" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" TabIndex="1" />                                        
                                    </div>
                                </td>
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
                            </tr>
                            <tr>
                                <td class="auto-style12">
                                </td>
                                <td class="auto-style13">
                                    <asp:Label ID="lblMensagem2" CssClass="LetrasLabel" runat="server"></asp:Label>
                                </td>
                                <td class="auto-style13">
                                    </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
