<%@ Page Language="C#" AutoEventWireup="true" CodeFile="arquivos.aspx.cs" Inherits="arquivos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
     <style>
        .body 
        {              
            background-image:url('fundobrooks.jpg');
        }
        .cabecbrooks
        {
            background-image: url('../../Images/cabecbrooks.jpg'); width: 1000px; height:100px;margin:auto; 
            color: brown;
            font-size:8pt;
        }
        .cliente
        {
            background-image: url('cliente.jpg'); width: 1000px; height:60px;margin:auto; 
        }
        .pastas
        {
            background-image: url('pastas.jpg'); width: 1000px; height:280px;margin:auto;             
        }
        .fundopastas
        {
            background-image: url('fundopastas.jpg'); width: 1000px; margin:auto; text-align:top; 
        }
        .rodapebrooks
        {
            background: transparent center center repeat-y; width: 1000px;margin: auto;
            color:black;            
            font-size:11pt;            
        }
        .tabletitulo 
        {
            position:relative;
            left: 880px;
            top: 60px;
        }
         .auto-style1 {
             height: 24px;
         }
    </style>
    <script type="text/javascript" lang="javascript">
        function BotaoDocumentos(pEscolha) {
            if (pEscolha == 'rdbRGR') {
                document.getElementById("rdbLAO").checked = false;
                document.getElementById('rdbDDR').checked = false;
                document.getElementById('rdbCERTISO9001').checked = false;
                document.getElementById('rdbAlvara').checked = false;
                document.getElementById('rdbCTFIBAMA').checked = false;
            }
            else if (pEscolha == 'rdbLAO') {
                document.getElementById('rdbRGR').checked = false;
                document.getElementById('rdbDDR').checked = false;
                document.getElementById('rdbCERTISO9001').checked = false;
                document.getElementById('rdbAlvara').checked = false;
                document.getElementById('rdbCTFIBAMA').checked = false;
            }
            else if (pEscolha == 'rdbDDR') {
                document.getElementById('rdbRGR').checked = false;
                document.getElementById('rdbLAO').checked = false;
                document.getElementById('rdbCERTISO9001').checked = false;
                document.getElementById('rdbAlvara').checked = false;
                document.getElementById('rdbCTFIBAMA').checked = false;
            }
            else if (pEscolha == 'rdbCERTISO9001') {
                document.getElementById('rdbRGR').checked = false;
                document.getElementById('rdbLAO').checked = false;
                document.getElementById('rdbDDR').checked = false;
                document.getElementById('rdbAlvara').checked = false;
                document.getElementById('rdbCTFIBAMA').checked = false;
            }
            else if (pEscolha == 'rdbAlvara') {
                document.getElementById('rdbRGR').checked = false;
                document.getElementById('rdbLAO').checked = false;
                document.getElementById('rdbDDR').checked = false;
                document.getElementById('rdbCERTISO9001').checked = false;
                document.getElementById('rdbCTFIBAMA').checked = false;
            }
            else if (pEscolha == 'rdbCTFIBAMA') {
                document.getElementById('rdbRGR').checked = false;
                document.getElementById('rdbLAO').checked = false;
                document.getElementById('rdbDDR').checked = false;
                document.getElementById('rdbCERTISO9001').checked = false;
                document.getElementById('rdbAlvara').checked = false;
            }
            document.getElementById("<%=btnProcurar.ClientID%>").click();
        }
    </script>
</head>
<body class="body">
    <form id="form1" runat="server">
        <div class="cabecbrooks">
            <table class="tabletitulo">
                <tr>
                    <td colspan="2"><asp:Label ID="lblTitulo" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td><a href="http://www.brooksambiental.com.br">home</a></td>
                    <td><a href="arquivos.aspx">documentos</a></td>
                    <td><a href="default.aspx">sair</a></td>
                </tr>
            </table>
        </div>
        <div class="pastas"></div>
        <div class="cliente">
            <br />        
            <asp:Label ID="lblCliente" runat="server" Text="Label"></asp:Label>
        </div>
        <div class="fundopastas">
            <table style="width:1000px;">
                <tr>
                    <td style="vertical-align:top;width:600px">
                         <asp:GridView ID="Grade" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" GridLines="Vertical" style="margin-bottom: 0px" AutoGenerateColumns="False" OnRowDataBound="Grade_RowDataBound">
                             <AlternatingRowStyle BackColor="#CCCCCC" />
                             <Columns>
                                 <asp:BoundField DataField="file" HeaderText="Documento" SortExpression="file">
                                 <HeaderStyle Width="400px" HorizontalAlign="Left" />
                                 </asp:BoundField>
                                 <asp:BoundField DataField="periodo" HeaderText="Período de apuração">
                                 <HeaderStyle Width="60px" />
                                 <ItemStyle HorizontalAlign="Center" />
                                 </asp:BoundField>
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
                         <asp:Label ID="lblBloqueio" runat="server" Text="se bloq fin" Visible="False" Font-Names="Tahoma"></asp:Label>
                    </td>
                    <td style="vertical-align:top;">
                        <table style="font-family:Arial;font-size:10px;">
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Faça sua busca:"></asp:Label> 
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Encontre os documentos filtrados por:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">
                                    <asp:RadioButton ID="rdbRGR" runat="server" Text="[RGR] Relatório de Gerenciamento de Resíduos" onclick="BotaoDocumentos('rdbRGR');" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="rdbLAO" runat="server" Text="[LAO] Licença AmbientaL de Operação" onclick="BotaoDocumentos('rdbLAO');" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="rdbDDR" runat="server" Text="[DDR] Declaração de Destinação de Resíduos" onclick="BotaoDocumentos('rdbDDR');"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="rdbCERTISO9001" runat="server" Text="[CERTISO9001] Certificado ISO 9001:2008" onclick="BotaoDocumentos('rdbCERTISO9001');" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="rdbAlvara" runat="server" Text="[ALVARÁ] Alvará Sanitário/Funcionamento"  onclick="BotaoDocumentos('rdbAlvara');" Checked="True"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="rdbCTFIBAMA" runat="server" Text="[CTFIBAMA] Cadastro Técnico Federal do IBAMA"  onclick="BotaoDocumentos('rdbCTFIBAMA');"/>
                                </td>
                            </tr>
                        </table>
                        <asp:Button ID="btnProcurar" runat="server" OnClick="btnProcurar_Click" ForeColor="White" BackColor="#efefef" BorderWidth="0" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
    <div class="rodapebrooks">
        <asp:Image ID="Image2" runat="server" ImageAlign="Middle" ImageUrl="~/Images/rodapebrooks.jpg" />
    </div>
</body>
</html>
