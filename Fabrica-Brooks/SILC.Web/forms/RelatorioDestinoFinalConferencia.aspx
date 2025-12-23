<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioDestinoFinalConferencia.aspx.cs" Inherits="SILC.Web.forms.RelatorioDestinoFinalConferencia" %>
<%@ Register src="INTEIRO.ascx" tagname="INTEIRO" tagprefix="uc3" %>
<%@ Register src="INTEIRO7.ascx" tagname="INTEIRO7" tagprefix="uc4" %>
<%@ Register src="MOEDA.ascx" tagname="MOEDA" tagprefix="uc5" %>

<%@ Register src="DATA.ascx" tagname="DATA" tagprefix="uc6" %>

<%@ Register src="MOTORISTA.ascx" tagname="MOTORISTA" tagprefix="uc7" %>

<%@ Register src="CAMINHAO.ascx" tagname="CAMINHAO" tagprefix="uc8" %>

<%@ Register src="DESTINOFINAL.ascx" tagname="DESTINOFINAL" tagprefix="uc9" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="table.css" type="text/css" rel="stylesheet" />
    <link href="aspx.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 50%;
            height: 30px;
        }
    </style>
    </head>
<script src="geral.js" lang="javascript" type="text/javascript"></script>
<script lang="javascript" type="text/javascript">
    function Imprimir()
    {
        try
        {
            document.getElementById("btnMontaDDR").style.visibility = "hidden";
            document.getElementById("btnImprimir").style.visibility = "hidden";
            //document.getElementById("chkTotalResiduo").style.visibility = "hidden";
            //document.getElementById("lblTotalResiduo").style.visibility = "hidden";
            window.print();
        }
        finally
        {
            document.getElementById("btnMontaDDR").style.visibility = "visible";
            document.getElementById("btnImprimir").style.visibility = "visible";
            //document.getElementById("chkTotalResiduo").style.visibility = "visible";
            //document.getElementById("lblTotalResiduo").style.visibility = "visible";
        }
    }
    function FazTotalPorResiduo()
    {
        document.getElementById("<%=btnMontaDDR.ClientID%>").click();
        //window.open('PesquisaMotoristas.aspx', '_blank', 'modalDialog');
    }
</script>
<body>
     <form id="form1" runat="server">
        <div>
            <table style="width:1190px">
                <tr>
                    <td id="dadosCab" style="vertical-align:middle; height: 100px; align-content:center; width:1190px;word-spacing:0;">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/logotipobrooks.jpg" />
                                </td>
                                <td>
                                    <asp:Label ID="lblTitulo" runat="server" Text="Relatório por Destino Final para Conferência" Font-Bold="True" CssClass="tituloFundoBranco" Font-Size="12pt"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height:50px;width:1190px; border:1px solid black; font-family:Arial; font-size:9pt;">

                                    &nbsp;<asp:Label ID="lblPeriodoDesejado" runat="server" ForeColor="Maroon" Text="Selecione o periodo desejado:"></asp:Label>
                                    <asp:TextBox ID="txtDataInicial" runat="server" TabIndex="1" Width="125px" TextMode="Date" EnableTheming="False"></asp:TextBox> <asp:TextBox ID="txtDataFinal" runat="server" TabIndex="1" Width="129px" TextMode="Date"></asp:TextBox>
                                            &nbsp;<asp:CheckBox ID="chkTotalResiduo" runat="server" onclick="FazTotalPorResiduo();"/><asp:Label ID="lblTotalResiduo" runat="server" Text="Total p/Resíduo"></asp:Label>
                                    &nbsp;<asp:Button ID="btnMontaDDR" runat="server" OnClick="btnMontaDDR_Click" Text="Ok" />
<input id="btnImprimir" type="button" value="Imprimir" onclick="Imprimir();" /></td>

                            </tr>
                        </table>
                     </td>
                </tr>
                <tr>
                    <td style="height:20px;width:1190px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp;&nbsp;Informe o Destino Final</td>
                </tr>
                <tr>
                    <td style="border:1px solid black; font-family:Arial; font-size:9pt;" class="auto-style1">
                        Código:<asp:TextBox ID="txtCodigoDestinoFinal" runat="server" MaxLength="6" Width="54px" BorderWidth="0px"></asp:TextBox>
                        <asp:Label ID="lblNomeDestinoFinal" runat="server" CssClass="tituloFundoBranco" Text="Destino Final:"></asp:Label>
                        <asp:Label ID="lblCNPJ" runat="server" CssClass="tituloFundoBranco" Text="CNPJ: 00.000.000/0001-00"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height:20px;width:1190px; border:1px solid black; font-family:Arial; font-size:9pt; background-color:ActiveCaption">
                        &nbsp;&nbsp;<asp:Label ID="lblDadosLAO" runat="server" Text=" Informações do(s) Resíduo(s)"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width:1490px" cellpadding="0" cellspacing="0">
                                    <asp:GridView ID="GradeMTR" runat="server" CellPadding="4" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Font-Names="verdana" Font-Size="9px" ClientIDMode="AutoID" AutoGenerateColumns="False" ForeColor="Black" AllowSorting="True" Font-Bold="False" OnRowDataBound="GradeMTR_RowDataBound" Width="1490px" GridLines="Vertical">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="DescricaoGrupo" HeaderText="Descrição grupo">
                                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DescricaoResiduo" HeaderText="Denominação">
                                            <HeaderStyle CssClass="padItemGrade" Width="150px" HorizontalAlign="Left" />
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Left"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Classe" HeaderText="Classe">
                                            <HeaderStyle CssClass="padItemGrade"  HorizontalAlign="Center" Width="35"/>
                                            <ItemStyle CssClass="padItemGrade"  HorizontalAlign="Center"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CodigoIBAMA" HeaderText="Código IBAMA">
                                            <HeaderStyle CssClass="padItemGrade"  HorizontalAlign="Center" Width="60"/>
                                            <ItemStyle CssClass="padItemGrade"  HorizontalAlign="Center"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NumeroMTRFatima" HeaderText="Nº MTR-e" >
                                            <HeaderStyle CssClass="padItemGrade"  HorizontalAlign="Center" Width="80"/>
                                            <ItemStyle CssClass="padItemGrade"  HorizontalAlign="Center"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DataColeta" HeaderText="Data Coleta" DataFormatString="{0: dd/MM/y}">
                                            <HeaderStyle CssClass="padItemGrade"  HorizontalAlign="Center" Width="60" />
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="QtdeColetada" HeaderText="Quantidade Gerada" DataFormatString="{0:n2}" >
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="QtdeDTR" DataFormatString="{0:n2}" HeaderText="Quantidade Armazenada">
                                            <ItemStyle CssClass="padItemGrade"  HorizontalAlign="Right" Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="QtdeDestinada" HeaderText="Quantidade Destinada" DataFormatString="{0:n2}">
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" Width="60px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DataDestinada" HeaderText="Data Destinada" DataFormatString=" {0: dd/MM/y}">
                                            <HeaderStyle CssClass="padItemGrade"  HorizontalAlign="Center" Width="60" />
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Unidade" HeaderText="Un">
                                            <HeaderStyle CssClass="padItemGrade"  HorizontalAlign="Center" Width="20" />
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Tecnologia Aplicada" DataField="TecnologiaAplicada" />
                                            <asp:BoundField DataField="Deposito" HeaderText="Destinador Final" />
                                            <asp:BoundField DataField="NumeroCDFe" HeaderText="CDF-e Nº" />
                                            <asp:BoundField DataField="NumeroLancamento" HeaderText="Nº Lançamento" Visible="False">
                                            <HeaderStyle CssClass="padItemGrade" />
                                            <ItemStyle CssClass="padItemGrade" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CodigoDestinoFinal" HeaderText="CodigoDestinoFinal" Visible="false" />
                                            <asp:BoundField DataField="DataDestinada" DataFormatString=" {0: dd/MM/y}" HeaderText="DataDescarga" Visible="False" />
                                            <asp:BoundField DataField="QtdeCDF" HeaderText="Qtde CDF" />
                                            <asp:BoundField DataField="Situacao" HeaderText="Situacao" />
                                            <asp:BoundField DataField="CodigoCliente" HeaderText="Cliente" />
                                            <asp:BoundField DataField="Nome" HeaderText="Nome/Razão Social Cliente" />
                                        </Columns>
                                        <EditRowStyle Wrap="False" />
                                        <FooterStyle BackColor="#CCCC99" />
                                        <HeaderStyle BackColor="White" Font-Bold="True" ForeColor="Black" BorderColor="DimGray" BorderWidth="1"/>
                                        <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="#F7F7DE" />
                                        <RowStyle BackColor="#F7F7DE" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                        <SortedAscendingHeaderStyle BackColor="#848384" />
                                        <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                        <SortedDescendingHeaderStyle BackColor="#575357" />
                                    </asp:GridView>
                                </td>
                                <td></td>
                            </tr>
                            </table>                       
                        <asp:HiddenField ID="hifCodigo" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
