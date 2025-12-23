<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ewvs_default.aspx.cs" Inherits="SILC.Web._Default" %>
<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h2>Disponibilizamos sistemas para sua empresa melhorar, agilizar e facilitar o controle de seus processos </h2>
            </hgroup>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Melhores clientes:</h3>
    <ol class="round">        
        <li class="one">
            <h5><a href="forms/brooks">BROOKS Ambiental</a></h5>
            Palhoça/SC
        </li>
        <li class="one">
            <h5><a href="forms/Entrar.aspx?Codigo=3">FIRST Rent a Car</a></h5>
            São Paulo/SP
        </li>
        <li class="one">
            <h5><a href="forms/Entrar.aspx?Codigo=4">VIAMAR Rent a Car</a></h5>
            Florianópolis/SC</li>
        <li class="one">
            <h5><a href="forms/wabengenharia/">WAB Engenharia</a></h5>
            Florianópolis/SC</li>
    </ol>
</asp:Content>