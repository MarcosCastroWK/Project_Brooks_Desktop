<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testes.aspx.cs" Inherits="forms_testes" %>

<%@ Register src="CLIENTE.ascx" tagname="CLIENTE" tagprefix="uc1" %>

<%@ Register src="INTEIRO2.ascx" tagname="INTEIRO2" tagprefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>
        function _linkMTRe(pX) {
            window.open('http://mtr.ima.sc.gov.br/ControllerServlet?acao=relatorio&nomeRelatorio=manifesto&manifesto=' + pX + '&condicao=N');
        }
    </script>
</head>
<body authorization="">
    <form id="form1" runat="server">
    <uc1:CLIENTE ID="CLIENTE1" runat="server" EnableTheming="False" EnableViewState="False" />
    <span style="cursor: pointer; color: blue; text-decoration: underline;" onclick="_linkMTRe('2211004405');">2211004405</span>
        <uc2:INTEIRO2 ID="INTEIRO21" runat="server" />
    </form>
</body>
</html>

