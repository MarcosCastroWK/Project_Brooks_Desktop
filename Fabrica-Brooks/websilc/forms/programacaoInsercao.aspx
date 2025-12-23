<%@ Page Language="C#" AutoEventWireup="true" CodeFile="programacaoInsercao.aspx.cs" Inherits="programacao_insercao" %>
<%@ Register Src="cabecalho.ascx" TagPrefix="uc1" TagName="cabecalho" %>
<%@ Register src="menu.ascx" tagname="menu" tagprefix="uc2" %>
<%@ Register src="INTEIRO7.ascx" tagname="INTEIRO7" tagprefix="uc4" %>
<%@ Register src="MOEDA.ascx" tagname="MOEDA" tagprefix="uc5" %>
<%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc6" %>
<%@ Register src="GRUPORESIDUO.ascx" tagname="GRUPORESIDUO" tagprefix="uc7" %>
<%@ Register src="DESTINOFINAL.ascx" tagname="DESTINOFINAL" tagprefix="uc8" %>
<%@ Register Src="~/forms/INTEIRO7.ascx" TagPrefix="uc1" TagName="INTEIRO7" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <link href="table.css" type="text/css" rel="stylesheet" />
    <link href="aspx.css" type="text/css" rel="stylesheet" />
    <script src="../Scripts/funcaoGeral.js"></script>
</head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<script>
    function AbrePesquisaClientes() {
        document.getElementById("<%=btnProcurar.ClientID%>").click();
        window.open('../forms/PesquisaClientes.aspx');
    }
    function AbrePesquisaResiduos() {
        document.getElementById("<%=btnProcurar.ClientID%>").click();
        window.open('../forms/PesquisaResiduos.aspx');
    }    
    function Invisivel(pIdBotao) {
        document.getElementById(pIdBotao).style.visibility = "hidden";
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
                    <td style="height:1%;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="Menu">
                        <uc2:menu ID="menu" runat="server" Visible="false" />
                    </td>
                    <td class="Menu">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <table style="word-spacing: 0; padding: 0; border: 0;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblTitulo" runat="server" Text="Inserção a partir do comercial" Font-Bold="True" Font-Names="Arial" CssClass="titulo2"></asp:Label>
                                </td>
                                <td><asp:Button ID="btnProcurar" Visible="true" BackColor="ButtonFace" ForeColor="ButtonFace" BorderWidth="0" runat="server" Text="P" /></td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblData" runat="server" CssClass="LetrasLabel" Text="Data"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="lblDataAtual" runat="server" CssClass="LetrasLabel"></asp:Label>
                                </td>                            
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblHora" runat="server" CssClass="LetrasLabel" Text="Hora"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="lblHoraAtual" runat="server" CssClass="LetrasLabel"></asp:Label>
                                </td>                            
                            </tr>                        
                            <tr>
                                <td>
                                    <asp:Label ID="lblSolicitante" runat="server" CssClass="LetrasLabel" Text="Solicitante"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtSolicitante" runat="server" Width="156px" MaxLength="40" style="text-transform:uppercase;"></asp:TextBox>
                                </td>                            
                            </tr>                        
                            <tr>
                                <td>
                                    <asp:Label ID="lblCodigoCliente" runat="server" CssClass="LetrasLabel" Text="Cliente"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCodigoCliente" runat="server" Width="46px" Enabled="false"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNomeFantasiaCliente" runat="server" Enabled="false" Width="381px"></asp:TextBox>
                                </td>
                                <td>
                                    <img id="imagem" alt="x" src="../Images/procura2.png" style="cursor:pointer;" onclick="AbrePesquisaClientes()"/>
                                </td>
                            </tr>                        
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" CssClass="LetrasLabel" Text="Resíduo"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCodigoResiduo" runat="server" Width="46px" Enabled="false"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescricaoResiduo" runat="server" Width="381px" Enabled="false"></asp:TextBox>
                                </td>
                                <td>
                                    <img id="imagem2" alt="x" src="../Images/procura2.png" style="cursor:pointer;" onclick="AbrePesquisaResiduos()"/>
                                </td>
                            </tr>                                                
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" CssClass="LetrasLabel" Text="Data Programada"></asp:Label>
                                </td>
                                <td>
                                    
                                    <uc6:DATA ID="dtDataProgramada" runat="server" />
                                    
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblServicoExecutar" runat="server" CssClass="LetrasLabel" Text="Serviço a Executar"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtServicoExecutar" runat="server" Width="480px" MaxLength="50" style="text-transform:uppercase;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblObservacao" runat="server" CssClass="LetrasLabel" Text="Observação"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtObservacao" runat="server" Width="480px" MaxLength="50" style="text-transform:uppercase;"></asp:TextBox>
                                </td>
                            </tr>       
                            <tr>
                                <td style="text-align:right;">
                                    <asp:Button ID="butOk" runat="server" Text="Ok" OnClick="butOk_Click" OnClientClick="Invisivel(this.id);" />
                                </td>
                                <td>
                                    <asp:Button ID="butCancelar" runat="server" Text="Cancelar" OnClick="butCancelar_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="dadosForm" valign="top" style="height:90%; width:100%;">
                        <table style="height:90%; width:100%;">
                            <tr>
                                <td>
                                    <table style="word-spacing: 0; padding: 0; border: 0;">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDataProgramacaoAberta" runat="server" Enabled="false" Width="88px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblLinhas" runat="server" CssClass="LetrasLabel" Text="Linhas: 000/000"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblTitulo0" runat="server" Text="Serviços Programados" Font-Bold="True" Font-Names="Arial" CssClass="titulo2"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="Div1" style="width:100%;height:380px; overflow-y:scroll;border:ridge 5px;font-size:10pt;">
                                        <asp:GridView ID="Grade2" runat="server" CellPadding="3" Width="2000px" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" GridLines="Vertical" Font-Bold="False" PageSize="5" OnRowCommand="Grade2_RowCommand" OnRowDataBound="Grade2_RowDataBound" >
                                            <AlternatingRowStyle BackColor="#CCCCCC" />
                                            <Columns>
                                                <asp:BoundField DataField="Sequencial" HeaderText="Sequencial" SortExpression="Sequencial">
                                                <HeaderStyle CssClass="padItemGrade" />
                                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CodigoCliente" HeaderText="Código Cliente" SortExpression="CodigoCliente" >
                                                <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NomeFantasiaCliente" HeaderText="Nome Fantasia" SortExpression="NomeFantasiaCliente">
                                                <HeaderStyle CssClass="padItemGrade" HorizontalAlign="Left" />
                                                <ItemStyle CssClass="padItemGrade" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Data" DataFormatString="{0:dd/MM/yy}" HeaderText="Data" SortExpression="Data" />
                                                <asp:BoundField DataField="Hora" HeaderText="Hora" SortExpression="Hora" />
                                                <asp:BoundField DataField="Solicitante" HeaderText="Solicitante" SortExpression="Solicitante" />
                                                <asp:BoundField DataField="ExecutarServico" HeaderText="Executar Serviço" SortExpression="ExecutarServico" />
                                                <asp:BoundField DataField="DataProgramada" DataFormatString="{0:dd/MM/yy}" HeaderText="DataProgramada" SortExpression="DataProgramada" />
                                                <asp:BoundField DataField="HoraProgramada" HeaderText="Serviço Executado" SortExpression="HoraProgramada" />
                                                <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" SortExpression="Quantidade" />
                                                <asp:BoundField DataField="CodigoCaminhao" HeaderText="Código Caminhao" SortExpression="CodigoCaminhao" />
                                                <asp:BoundField DataField="ModeloCaminhao" HeaderText="Caminhão" SortExpression="ModeloCaminhao" />
                                                <asp:BoundField DataField="CodigoMotorista" HeaderText="Código Motorista" SortExpression="CodigoMotorista" />
                                                <asp:BoundField DataField="NomeMotorista" HeaderText="Motorista" SortExpression="NomeMotorista" />
                                                <asp:BoundField DataField="Observacao" HeaderText="Observacao" SortExpression="Observacao" />
                                                <asp:BoundField DataField="Unidade" HeaderText="Unidade" SortExpression="Unidade" />
                                                <asp:BoundField DataField="TipoProgramacao" HeaderText="Tipo Programação" SortExpression="TipoProgramacao" />
                                                <asp:BoundField DataField="RotaMapa" HeaderText="Rota Mapa" SortExpression="RotaMapa" />
                                                <asp:BoundField DataField="Origem" HeaderText="Origem" SortExpression="Origem" />
                                                <asp:BoundField DataField="StatusCor" HeaderText="StatusCor" SortExpression="StatusCor" />
                                            </Columns>
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                        </asp:GridView>
                                    </div>
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
