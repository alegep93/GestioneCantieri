<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="OrdineFrutti.aspx.cs" Inherits="GestioneCantieri.OrdineFrutti" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    <title>Ordine Frutti</title>
    <style>
        #body_lblIsGruppoInserito{
            margin-bottom: 30px;
            text-align: right;
        }
    </style>
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>Ordine Frutti</h1>
    <div class="container-fluid">
        <div class="row">
            <!-- Scegli Cantiere -->
            <div class="col-md-offset-3 col-md-6">
                <asp:Label ID="lblScegliCantiere" runat="server" Text="Scegli Cantiere"></asp:Label>
                <asp:DropDownList ID="ddlScegliCantiere" CssClass="form-control" OnTextChanged="ddlScegliCantiere_TextChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <asp:Panel ID="pnlInserisciDati" runat="server">
                <!-- Scegli Data Ordine -->
                <!--<div class="col-md-4">
                    <asp:Label ID="lblDataOrdine" runat="server" Text="Data Ordine"></asp:Label>
                    <asp:TextBox ID="txtDataOrdine" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                </div> -->

                <!-- Inserisci Appartamento -->
                <!--<div class="col-md-4">
                    <asp:Label ID="lblAppartamento" runat="server" Text="Appartamento"></asp:Label>
                    <asp:TextBox ID="txtAppartamento" CssClass="form-control" runat="server"></asp:TextBox>
                </div>-->

                <!-- Scegli Locale -->
                <div class="col-md-offset-3 col-md-6">
                    <asp:Label ID="lblScegliLocale" runat="server" Text="Scegli Locale"></asp:Label>
                    <asp:DropDownList ID="ddlScegliLocale" CssClass="form-control" OnTextChanged="ddlScegliLocale_TextChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <asp:Panel ID="pnlScegliGruppo" runat="server">
                <div class="col-md-12">
                    <!-- Filtri sui nomi dei gruppi presenti su DB -->
                    <div class="col-md-4">
                        <asp:Label ID="lblFiltro1" runat="server" Text="Filtro 1"></asp:Label>
                        <asp:TextBox ID="txtFiltroGruppo1" placeholder="Filtro 1" OnTextChanged="txtFiltroGruppo1_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblFiltro2" runat="server" Text="Filtro 2"></asp:Label>
                        <asp:TextBox ID="txtFiltroGruppo2" placeholder="Filtro 2" OnTextChanged="txtFiltroGruppo2_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblFiltro3" runat="server" Text="Filtro 3"></asp:Label>
                        <asp:TextBox ID="txtFiltroGruppo3" placeholder="Filtro 3" OnTextChanged="txtFiltroGruppo3_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <!-- Lista dei gruppi (filtrati o non) -->
                <div class="col-md-offset-3 col-md-6">
                    <asp:Label ID="lblScegliGruppo" runat="server" Text="Scegli Gruppo"></asp:Label>
                    <asp:DropDownList ID="ddlScegliGruppo" CssClass="form-control" AutoPostBack="true" OnTextChanged="ddlScegliGruppo_TextChanged" runat="server"></asp:DropDownList>
                    <asp:Button ID="btnInserisciGruppo" CssClass="btn btn-primary btn-lg pull-right" OnClick="btnInserisciGruppo_Click" runat="server" Text="Inserisci Gruppo" />
                    <asp:Label ID="lblIsGruppoInserito" CssClass="pull-right" runat="server" Text=""></asp:Label>
                </div>
            </asp:Panel>
        </div>

        <div class="row">
            <asp:Panel ID="pnlMostraGruppiInseriti" runat="server">
                <div class="col-md-offset-3 col-md-6">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Mostra contenuto gruppo selezionato</h3>
                        </div>
                        <div class="panel-body">
                            <asp:Label ID="lblQtaDescr" runat="server" Text="Qta - Descrizione"></asp:Label>
                            <ul class="list-group">
                                <% foreach (var item in compList)
                                    {%>
                                <li class="list-group-item"><%= item.NomeGruppo + " - " + item.Descrizione %></li>
                                <%} %>
                            </ul>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>