<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ima.aspx.cs" Inherits="forms_brooks_ima" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>IMA</title>
    
</head>
<script> 
    function setControl() { 
        document.getElementById('txtCnpj').value = '03.938.048/0001-33';
        document.getElementById('txtSenha').value = 'b10757';
    } 
</script>
<body onload="javascript:setControl();return;">
    <form id="form1" runat="server">
        <asp:Button ID="butIMA" runat="server" Text="IMA" UseSubmitBehavior="false" OnClientClick="javascript:setControl();return;" />
    </form>
</body>
</html>
