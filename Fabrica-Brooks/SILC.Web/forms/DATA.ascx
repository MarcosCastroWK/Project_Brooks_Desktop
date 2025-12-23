<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DATA.ascx.cs" Inherits="SILC.Web.forms.DATA" %>
<script>
    function isNumberKeyData(evt, txt)
    {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode > 31 && (charCode < 47 || charCode > 57) || charCode == 13)
            return false;
        else {
            if (document.getElementById(txt).value.length == 0) {
                if (charCode >= 48 && charCode <= 51) {
                    return true;
                }
                else
                    return false;
            }
            else if (document.getElementById(txt).value.length == 1) {
                var txt2 = document.getElementById(txt).value.substring(0, 1);
                if (txt2 == '0' || txt2 == '1' || txt2 == '2') {
                    if (charCode >= 48 && charCode <= 58) {
                        document.getElementById(txt).value = document.getElementById(txt).value + String.fromCharCode(charCode) + '/';
                    }
                }
                else if (txt2 == '3') {
                    if (charCode >= 48 && charCode <= 49) {
                        document.getElementById(txt).value = document.getElementById(txt).value + String.fromCharCode(charCode) + '/';
                    }
                }
                return false;
            }
            else if (document.getElementById(txt).value.length == 2) {
                if (charCode == 47)
                    return true;
                else
                    return false;
            }
            else if (document.getElementById(txt).value.length == 3) {
                if (charCode >= 48 && charCode <= 49) {
                    return true;
                }
            }
            else if (document.getElementById(txt).value.length == 4) {
                var txt3 = document.getElementById(txt).value.substring(3, 4);
                if ((document.getElementById(txt).value.substring(0, 4) == '30/0' || document.getElementById(txt).value.substring(0, 4) == '31/0') && charCode == 50)
                    return false;
                else if (document.getElementById(txt).value.substring(0, 4) == '31/0' && (charCode == 52 || charCode == 54 || charCode == 57))
                    return false;
                else if (document.getElementById(txt).value.substring(0, 4) == '31/1' && charCode == 49)
                    return false;
                else if (txt3 == '0') {
                    if (charCode >= 49 && charCode <= 58) {
                        document.getElementById(txt).value = document.getElementById(txt).value + String.fromCharCode(charCode) + '/20';
                    }
                }
                else if (txt3 == '1') {

                    if (charCode >= 48 && charCode <= 50) {
                        document.getElementById(txt).value = document.getElementById(txt).value + String.fromCharCode(charCode) + '/20';
                    }
                }
                return false;
            }
            else if (document.getElementById(txt).value.length == 5 && charCode == 47)
                return true;
            else if (document.getElementById(txt).value.length > 5 && charCode != 47)
                return true;
            return false;
        }
    }
    function CalcularDataTerminoReajuste(evt, txt) {
        ConfirmaData(txt);
        if (document.getElementById('datDataInicio_txtData') != null) {
            if (document.getElementById('datDataInicio_txtData').value != '') {
                var _dia = document.getElementById('datDataInicio_txtData').value.substring(0, 2);
                var _mes = document.getElementById('datDataInicio_txtData').value.substring(3, 5);
                var _ano4 = parseInt(document.getElementById('datDataInicio_txtData').value.substring(6, 10)) + 1;
                var _str_data = _dia + '/' + _mes + '/' + _ano4;
                if (document.getElementById('datDataTermino_txtData') != null) {
                    if (document.getElementById('datDataTermino_txtData').value == '' && document.getElementById('datDataInicio_txtData').value != '')
                        document.getElementById('datDataTermino_txtData').value = _str_data;
                }
                if (document.getElementById('datDataReajuste_txtData') != null) {
                    if (document.getElementById('datDataReajuste_txtData').value == '' && document.getElementById('datDataInicio_txtData').value != '')
                        document.getElementById('datDataReajuste_txtData').value = _str_data;
                }
                if (document.getElementById('txtAniversarioReajuste') != null) {
                    if (document.getElementById('txtAniversarioReajuste').value == '' && document.getElementById('datDataInicio_txtData.value') != '')
                        document.getElementById('txtAniversarioReajuste').value = _dia + "/" + _mes
                }
            }
        }
    }
    function ConfirmaData(txt) {
        if (document.getElementById(txt).value.length >= 3) {
            if (document.getElementById(txt).value.substring(2, 3) != '/') {
                document.getElementById(txt).value = '';
                return false;
            }
            if (document.getElementById(txt).value.length > 4) {
                if (document.getElementById(txt).value.substring(5, 6) != '/') {
                    document.getElementById(txt).value = '';
                    return false;
                }
            }
        }
        return true; 
    }
</script>
<style>
    .alinha
    {
        text-align:center;
        width: 70px;
    }
</style>
<asp:TextBox ID="txtData" runat="server" Width="80px" MaxLength="10" onkeypress="return isNumberKeyData(event, this.id);" onfocusout="return CalcularDataTerminoReajuste(event, this.id);"></asp:TextBox>
