<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PreviewRelatorioContaineres.aspx.cs" Inherits="Relatorios_PreviewRelatorio" %>
<%@ Register src="~/forms/cabecalho.ascx" tagname="cabecalho" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../forms/table.css" type="text/css" rel="stylesheet" />
    <link href="../forms/aspx.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <uc1:cabecalho ID="cabecalho1" runat="server" />
        <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" OnClick="btnImprimir_Click" />    
    </form>
</body>
</html>
