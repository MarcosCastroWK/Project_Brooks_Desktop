<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="SILC.Web.forms.Contact" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Nosssos contatos</h2>
    </hgroup>

    <section class="contact">
        <header>
            <h3>Telefone:</h3>
        </header>
        <p>
            <span class="label">Escritório:</span>
            <span>48-3132-2294</span>
        </p>
        <p>
            <span class="label">Celular:"</span>
            <span>48-98462-0077</span>
            <asp:TextBox ID="TextBox1" runat="server" MaxLength="10" TextMode="Number" Width="60px"></asp:TextBox>
        </p>
    </section>

</asp:Content>