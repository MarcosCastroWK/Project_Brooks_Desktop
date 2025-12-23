<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServicosDesenvolvedor.aspx.cs" Inherits="SILC.Web.forms.ServicosDesenvolvedor" %>
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
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<script>    
    function InabilitarBotao(pBotao) {
        document.getElementById('Grade').style.visibility = 'hidden';
        document.getElementById(pBotao).style.visibility = 'hidden';
        document.getElementById('btnCancelar').style.visibility = 'hidden';
        document.getElementById('lblMensagem').innerText = 'Salvando/enviando e-mail...';
    }
</script>
<body>
     <form id="form1" runat="server">
        <div id="Operacoes">
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
                        <asp:Label ID="lblTitulo" runat="server" Text="Servicos Desenvolvedor" Font-Bold="True" CssClass="titulo2"></asp:Label>
                     </td>
                </tr>
                <tr>
                    <td valign="top" style="height:1%;">
                        <div id="contentTopRightDiv" style="width:99%;height:200px; overflow-y:scroll;border:ridge 5px;font-size:10pt;" >
                            <asp:GridView ID="Grade" runat="server" CellPadding="3" Width="97%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" OnRowCommand="Grade_RowCommand" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound" ForeColor="Black" GridLines="Vertical" AllowSorting="True" OnSorting="Grade_Sorting" Font-Bold="False" >
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
                                    <asp:BoundField DataField="Codigo" HeaderText="Código" SortExpression="Codigo" >
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Servico" HeaderText="Descrição" SortExpression="Servico">
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left" Width="600px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Solicitante" HeaderText="Solicitante" SortExpression="Solicitante">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Executando" HeaderText="Executando" SortExpression="Executando">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Data" HeaderText="Data" SortExpression="Data"  DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" Width="90px"/>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Prioridade" HeaderText="Prioridade" SortExpression="Prioridade">
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Feito" HeaderText="Feito" SortExpression="Feito">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
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
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDescricao" CssClass="LetrasLabel" runat="server" Font-Bold="False" >&nbsp;Serviço</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescricao" runat="server" Width="800px" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDetalhamento" CssClass="LetrasLabel" runat="server" Font-Bold="False" >&nbsp;Detalhamento</asp:Label>
                                </td>
                                <td class="auto-style16">
                                    <asp:TextBox ID="txtDetalhamento" runat="server" Width="800px" Height="77px" TextMode="MultiLine" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblData" CssClass="LetrasLabel" runat="server" Font-Bold="False" >&nbsp;Data</asp:Label>
                                </td>
                                <td>
                                    <uc6:DATA ID="txtData" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" CssClass="LetrasLabel" runat="server" Font-Bold="False" >&nbsp;Solicitante</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSolicitante" runat="server" Width="100px" ></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="lblPrioridade" CssClass="LetrasLabel" runat="server" Font-Bold="False" >&nbsp;Prioridade</asp:Label>
                                </td>
                                <td>
                                    <uc7:INTEIRO2 ID="intPrioridade" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblFeito" CssClass="LetrasLabel" runat="server" Font-Bold="False" >&nbsp;Feito</asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkFeito" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" CssClass="LetrasLabel" runat="server" Font-Bold="False" >&nbsp;Executando</asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkExecutando" runat="server" />
                                </td>
                            </tr>                            
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Label ID="lblConfirmacao" CssClass="LetrasLabel" runat="server" Font-Bold="False" >Confirma inclusão</asp:Label>
                                </td>
                                <td class="auto-style16">
                                    <div style="width:160px">
                                        <asp:Button runat="server" Font-Size="10pt" CommandName="Salvar" Text="Ok" ID="Salvar" OnClick="Salvar_Click" OnClientClick="InabilitarBotao(this.id);" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Novo" OnClick="btnCancelar_Click" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hifCodigo" runat="server" />
                                </td>
                                <td>
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
