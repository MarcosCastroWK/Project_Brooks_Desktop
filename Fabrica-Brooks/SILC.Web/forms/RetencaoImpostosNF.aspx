<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RetencaoImpostosNF.aspx.cs" Inherits="SILC.Web.forms.forms_RetencaoImpostosNF" %>
<%@ Register src="cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>
<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc2" %>
<%@ Register src="INTEIRO.ascx" tagname="INTEIRO" tagprefix="uc3" %>
<%@ Register src="INTEIRO7.ascx" tagname="INTEIRO7" tagprefix="uc4" %>
<%@ Register src="MOEDA.ascx" tagname="MOEDA" tagprefix="uc5" %>

<%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc6" %>

<%@ Register src="INTEIRO2.ascx" tagname="INTEIRO2" tagprefix="uc7" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        .auto-style13
        {
            height: 26px;
        }
        .auto-style14
        {
            height: 26px;
            width: 308px;
        }
        .auto-style15
        {
            width: 308px;
        }
        .auto-style16
        {
            height: 1%;
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
                        <asp:Label ID="lblTitulo" runat="server" Text="&nbsp;Retenções NF" Font-Bold="True" CssClass="titulo2"></asp:Label>
                     </td>
                </tr>
                <tr>
                    <td valign="top" style="height:1%;">
                        <div id="contentTopRightDiv" style="width:98%;height:200px; overflow-y:scroll;border:ridge 5px;font-size:10pt;" >
                            <asp:GridView ID="Grade" runat="server" CellPadding="3" Width="80%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" OnRowCommand="Grade_RowCommand" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound" ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnSorting="Grade_Sorting" Font-Bold="False" >
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
                                    <asp:BoundField DataField="Sequencial" HeaderText="Sequencial" SortExpression="Sequencial">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodigoSituacaoTributaria" HeaderText="Situação Tributária" SortExpression="CodigoSituacaoTributaria">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CodigoBROOKS" HeaderText="Código BROOKS" SortExpression="CodigoBROOKS" >
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Descricao" HeaderText="Descrição" SortExpression="Descricao" >
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="300px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ValorLimiteIR" HeaderText="Limite&gt;= NF IR" DataFormatString="{0:n2}" SortExpression="ValorLimiteIR">
                                    <HeaderStyle CssClass="padItemGrade"></HeaderStyle>
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AliquotaIR_Retido" HeaderText="Alíquota IR" SortExpression="AliquotaIR_Retido" DataFormatString="{0:n2}">
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ValorLimiteCRF" HeaderText="Limite&gt;=NF CRF" SortExpression="ValorLimiteCRF" DataFormatString="{0:n2}">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AliquotaPIS_Retido" HeaderText="Alíquota PIS" SortExpression="AliquotaPIS_Retido" DataFormatString="{0:n2}">
                                    <HeaderStyle CssClass="padItemGrade" />
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AliquotaCOFINS_Retido" HeaderText="Alíquota COFINS" SortExpression="AliquotaCOFINS_Retido" DataFormatString="{0:n2}">
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AliquotaContribSocial" DataFormatString="{0:n2}" HeaderText="Alíquota CSLL" SortExpression="AliquotaContribSocial">
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalCRF" DataFormatString="{0:n2}" HeaderText="Total CRF" SortExpression="TotalCRF">
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
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
                                    <asp:Label CssClass="LetrasLabel" ID="lblSitTrib" runat="server" Font-Bold="False" Width="110px">&nbsp;Situação &nbsp;Tributária</asp:Label>
                                </td>
                                <td class="auto-style11">
                                    <uc7:INTEIRO2 ID="intSituacaoTributaria" runat="server" />
                                </td>
                                <td class="auto-style13">
                                    <asp:Label CssClass="LetrasLabel" ID="lblCodigo" runat="server" Font-Bold="False" Width="90px">&nbsp;Código BROOKS</asp:Label>
                                </td>
                                <td class="auto-style13">
                                    <asp:TextBox ID="txtCodigoBROOKS" onfocus="LimpaErro()" runat="server" TabIndex="1" Width="20px"></asp:TextBox>
                                </td>
                                <td class="auto-style13">
                                    <asp:Label ID="lblDescricao" CssClass="LetrasLabel" runat="server" Font-Bold="False" >&nbsp;Descrição</asp:Label>
                                </td>
                                <td class="auto-style14">
                                    <asp:TextBox ID="txtDescricao" runat="server" TabIndex="1" Width="300px" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style10">
                                    <asp:Label ID="Label2" runat="server" Font-Bold="False" CssClass="LetrasLabel" Width="77px">&nbsp;Limite&gt;=NF IR</asp:Label>
                                </td>
                                <td class="auto-style11">
                                    <uc5:MOEDA ID="valValorLimiteIR" runat="server" IndiceTab="1" />
                                </td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Font-Bold="False" CssClass="LetrasLabel" Width="77px">&nbsp;Alíquota IR</asp:Label>
                                </td>
                                <td>
                                    <uc5:MOEDA ID="valAliquotaIR" runat="server" IndiceTab="1" />
                                </td>
                                <td>
                                    <asp:Label ID="lblLimiteCRF" CssClass="LetrasLabel" runat="server" Font-Bold="False" Width="100px" >&nbsp;Limite CRF</asp:Label>
                                </td>
                                <td class="auto-style15">
                                    <uc5:MOEDA ID="valValorLimiteCRF" runat="server" IndiceTab="1" />
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">
                                    <asp:Label ID="Label4" runat="server" Font-Bold="False" CssClass="LetrasLabel" Width="77px">&nbsp;Alíquota PIS</asp:Label>
                                </td>
                                <td class="auto-style12">
                                    <uc5:MOEDA ID="valAliquotaPIS" runat="server" IndiceTab="1" />
                                </td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Font-Bold="False" CssClass="LetrasLabel" Width="90px">&nbsp;Alíquota COFINS</asp:Label>
                                </td>
                                <td>
                                    <uc5:MOEDA ID="valAliquotaCOFINS" runat="server" IndiceTab="1" />
                                </td>
                                <td colspan="2">
                                    <table style="padding:0">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Font-Bold="False" CssClass="LetrasLabel" Width="97px">&nbsp;Alíquota CSLL</asp:Label>
                                            </td>
                                            <td>
                                                <uc5:MOEDA ID="valAliquotaCSLL" runat="server" IndiceTab="1" />                                   
                                            </td>
                                            <td><asp:Label ID="Label7" runat="server" Font-Bold="False" CssClass="LetrasLabel" Width="77px">&nbsp;Total CRF</asp:Label></td>
                                            <td><uc5:MOEDA ID="valLimiteCRF1" runat="server" IndiceTab="1" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="LetrasTD">&nbsp;</td>
                                <td class="auto-style12">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td class="auto-style15">
                                    <div style="width:160px">
                                        <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Ok" ID="Salvar" OnClick="Salvar_Click" TabIndex="18" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:1%">
                                    <asp:HiddenField ID="hifCodigo" runat="server" />
                                </td>
                                <td colspan="5">
                                    <asp:Label ID="lblMensagem" runat="server" CssClass="LetrasLabel"></asp:Label>
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
