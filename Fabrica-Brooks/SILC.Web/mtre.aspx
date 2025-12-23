<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mtre.aspx.cs" Inherits="SILC.Web.mtre" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

</head>
<script lang="JavaScript" type="text/javascript">
    function verHTML()
    {
        var iframe = document.getElementById('iframemtre');
        var conteudoIframe = iframe.parentElement. //.getAttribute('txtCnpj'.value);
        alert(conteudoIframe);
    }
</script>
<body>
    <form id="form1" runat="server">
        <div>
            <input id="btnMTRe" type="button" value="MTRe" onclick="verHTML();" />
        </div>
        <div>
            <iframe id="iframemtre" src="http://mtr.ima.sc.gov.br" style="width:100%;height:400px;"></iframe>
        </div>
    </form>
</body>
</html>
