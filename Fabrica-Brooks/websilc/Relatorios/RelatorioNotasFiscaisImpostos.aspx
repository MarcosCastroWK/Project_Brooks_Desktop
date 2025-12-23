<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RelatorioNotasFiscaisImpostos.aspx.cs" Inherits="RelatorioNotasFiscaisImpostos" %>

<%@ Register src="../forms/cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>

<%@ Register src="../forms/CLIENTESCONTROL.ascx" tagname="CLIENTESCONTROL" tagprefix="uc2" %>

<%@ Register src="../forms/DATA.ascx" tagname="DATA" tagprefix="uc3" %>

<%@ Register src="../forms/DESTINOFINAL.ascx" tagname="DESTINOFINAL" tagprefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../forms/table.css" type="text/css" rel="stylesheet" />
    <link href="../forms/aspx.css" type="text/css" rel="stylesheet" />
    <script src="../Scripts/funcaoGeral.js"></script>
    <script>
        function Imprime() {
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
    
            <asp:Label ID="lblTitulo" runat="server" CssClass="titulo2" Text="Relatório de Notas Fiscais e Impostos"></asp:Label>
    
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblData" runat="server" CssClass="LetrasLabel" Text="Período:" Width="30px"></asp:Label>
                    </td>
                    <td>
                        <uc3:DATA ID="Data1" runat="server" />
                    </td>
                    <td style="text-align:center">
                        <asp:Label ID="Label1" runat="server" CssClass="LetrasLabel" Text=" a " Width="20px"></asp:Label>
                    </td>
                    <td>
                        <uc3:DATA ID="Data2" runat="server" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:RadioButtonList  ID="cklAcao" runat="server" CssClass="LetrasLabel" AutoPostBack="True" CellPadding="5" CellSpacing="5" RepeatLayout="Flow" TextAlign="Right" OnSelectedIndexChanged="Check_Clicked">
                            <asp:ListItem>Ordem Inversa</asp:ListItem>
                            <asp:ListItem>Só Fatura</asp:ListItem>
                            <asp:ListItem>Só ISS PF</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnOk_Click" OnClientClick="DesabilitaOperacoes(this.id, 'Data1_txtData', 'Data2_txtData', 'Relatório de Notas Fiscais e Impostos');" />
                                </td>
                                <td>
                                    <asp:Button ID="btnCancelar" runat="server" Text="Voltar" PostBackUrl="~/forms/Menu.aspx" OnClick="btnCancelar_Click" />
                                </td>
                                <td>
                                    <input id="btnImprimir" type="button" value="Imprimir" onclick="Imprime();" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="imbExcel" runat="server" ImageUrl="~/Images/excel2010.png" OnClick="imbExcel_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:Image ID="imgGirando" runat="server" ImageUrl="~/Images/aguarde.gif" style="visibility:hidden; height:1px;" />
        </div>
        <div id="divImprimir">
            <asp:Panel ID="Panel1" runat="server" BorderWidth="1px" Width="1300px">
                &nbsp;&nbsp;
                <asp:Label ID="lblTituloRelatorio" runat="server" style="font-weight: bold; font-family:Tahoma; font-size: 8pt;" Text=""></asp:Label>                
                <table cellspacing="1" cellpadding="1"> 
                    <tr>
                        <td colspan="6" style="width: 790px;"></td>
                        <td colspan="6" style="width: 532px; text-align: center;border: 1px solid; font-weight: bold; font-family:Tahoma; font-size: 8pt;">RETENÇÕES NA FONTE</td>
                        <td style="width: 88px;"></td>
                    </tr>
                    <tr>
                        <td style="width: 66px; text-align: center;border: 1px solid; font-weight: bold; font-family:Tahoma; font-size: 8pt;">NF/Fat</td>
                        <td style="width: 66px; text-align: center;border: 1px solid; font-weight: bold; font-family:Tahoma; font-size: 8pt;">Emissão</td>
                        <td style="width: 70px; text-align: center;border: 1px solid; font-weight: bold; font-family:Tahoma; font-size: 8pt;">Sit.Trib</td>
                        <td style="width: 70px; text-align: center;border: 1px solid; font-weight: bold; font-family:Tahoma; font-size: 8pt;">Sit.Nota</td>
                        <td style="width: 352px;text-align: center;border: 1px solid; font-weight: bold; font-family:Tahoma; font-size: 8pt;">Tomador</td>
                        <td style="width: 96px; text-align: center;border: 1px solid; font-weight: bold; font-family:Tahoma; font-size: 8pt;">Vl.Bruto NF/Fat</td>
                        <td style="width: 80px; text-align: center;border: 1px solid; font-weight: bold; font-family:Tahoma; font-size: 8pt;">PIS</td>
                        <td style="width: 80px; text-align: center;border: 1px solid; font-weight: bold; font-family:Tahoma; font-size: 8pt;">Cofins</td>
                        <td style="width: 80px; text-align: center;border: 1px solid; font-weight: bold; font-family:Tahoma; font-size: 8pt;">INSS</td>
                        <td style="width: 80px; text-align: center;border: 1px solid; font-weight: bold; font-family:Tahoma; font-size: 8pt;">IR</td>
                        <td style="width: 80px; text-align: center;border: 1px solid; font-weight: bold; font-family:Tahoma; font-size: 8pt;">C.Social</td>
                        <td style="width: 80px; text-align: center;border: 1px solid; font-weight: bold; font-family:Tahoma; font-size: 8pt;">ISS RF</td>
                        <td style="width: 80px; text-align: center;border: 1px solid; font-weight: bold; font-family:Tahoma; font-size: 8pt;">ISS PF</td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
