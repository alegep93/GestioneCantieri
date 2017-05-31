<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="GestisciFrutti.aspx.cs" Inherits="GestioneCantieri.GestisciFrutti" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    <title>Gestisci Frutti</title>
    <style>
        .panel.panel-default div.panel-body {
            max-height: 500px;
            overflow: hidden;
            overflow-y: auto;
        }
    </style>
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12 text-center btnChoosePanelContainer">
                <asp:Button ID="btnApriInserisci" OnClick="btnApriInserisci_Click" CssClass="btn btn-default btn-lg" runat="server" Text="Inserisci" />
                <asp:Button ID="btnApriModifica" OnClick="btnApriModifica_Click" CssClass="btn btn-default btn-lg" runat="server" Text="Modifica" />
                <asp:Button ID="btnApriElimina" OnClick="btnApriElimina_Click" CssClass="btn btn-default btn-lg" runat="server" Text="Elimina" />
            </div>
        </div>

        <div class="row">
            <!-- Titolo Pagina -->
            <h1>
                <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
            </h1>
            <!-- Fine Titolo Pagina -->

            <div class="col-md-6"></div>
            <div class="col-md-6">
                <div class="col-md-4">
                    <asp:TextBox ID="txtFiltroFrutti1" placeholder="Filtro 1" OnTextChanged="txtFiltroFrutti1_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtFiltroFrutti2" placeholder="Filtro 2" OnTextChanged="txtFiltroFrutti2_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtFiltroFrutti3" placeholder="Filtro 3" OnTextChanged="txtFiltroFrutti3_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>

        <asp:Panel ID="pnlInserisci" runat="server">
            <div class="row">
                <div class="col-md-6">
                    <!-- Creazione Frutto -->
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Inserisci Frutto</h3>
                        </div>
                        <div class="panel-body">
                            <asp:Label ID="lblInsFrutto" runat="server" Text="Nome Frutto"></asp:Label>
                            <asp:TextBox ID="txtInsNomeFrutto" CssClass="form-control" runat="server"></asp:TextBox>

                            <asp:Button ID="btnInsFrutto" OnClick="btnInsFrutto_Click" CssClass="btn btn-primary pull-left" runat="server" Text="Inserisci Frutto" />
                            <asp:Label ID="lblIsFruttoInserito" runat="server" Text="" CssClass="pull-right"></asp:Label>
                        </div>
                    </div>
                    <!-- Fine Creazione Frutto -->
                </div>

                <!-- Mostra Lista Frutti -->
                <div class="col-md-6">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Mostra Lista Frutti Inseriti</h3>
                        </div>
                        <div class="panel-body">
                            <asp:Label ID="lblInsMostraListaFrutti" runat="server" Text="Nome Frutto"></asp:Label>
                            <ul class="list-group">
                                <% foreach (var item in fruttiList)
                                    {%>
                                <li class="list-group-item"><%= item.Descr %></li>
                                <% } %>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- Fine Mostra Lista Frutti -->
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlModifica" runat="server">
            <div class="row">
                <!-- Modifica Frutto -->
                <div class="col-md-6">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Modifica Frutto</h3>
                        </div>
                        <div class="panel-body">
                            <asp:Label ID="lblModScegliFrutto" runat="server" Text="Scegli Frutto"></asp:Label>
                            <asp:DropDownList ID="ddlModScegliFrutto" CssClass="form-control" runat="server" OnTextChanged="ddlModScegliFrutto_TextChanged" AutoPostBack="true"></asp:DropDownList>

                            <asp:Panel ID="pnlModFrutto" runat="server">
                                <asp:Label ID="lblModNomeFrutto" runat="server" Text="Nome Frutto"></asp:Label>
                                <asp:TextBox ID="txtModNomeFrutto" CssClass="form-control" runat="server"></asp:TextBox>

                                <asp:Button ID="btnSaveModFrutto" OnClick="btnSaveModFrutto_Click" CssClass="btn btn-primary pull-left" runat="server" Text="Modifica Frutto" />
                                <asp:Label ID="lblSaveModFrutto" runat="server" Text="" CssClass="pull-right"></asp:Label>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <!-- Fine Modifica Frutto -->

                <!-- Mostra Lista Frutti -->
                <div class="col-md-6">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Mostra Lista Frutti Inseriti</h3>
                        </div>
                        <div class="panel-body">
                            <asp:Label ID="lblModListaFrutti" runat="server" Text="Nome Frutto"></asp:Label>
                            <ul class="list-group">
                                <% foreach (var item in fruttiList)
                                    { %>
                                <li class="list-group-item"><%= item.Descr %></li>
                                <%  } %>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- Fine Mostra Lista Frutti -->
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlElimina" runat="server">
            <div class="row">
                <div class="col-md-6">
                    <!-- Elimina Frutto -->
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Elimina Frutto</h3>
                        </div>
                        <div class="panel-body">
                            <asp:Label ID="lblDelFrutto" runat="server" Text="Nome Frutto"></asp:Label>
                            <asp:DropDownList ID="ddlDelFrutto" OnTextChanged="ddlDelFrutto_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>

                            <asp:Button ID="btnDelFrutto" OnClick="btnDelFrutto_Click" CssClass="btn btn-primary pull-left" runat="server" Text="Elimina Frutto" OnClientClick="return confirm('Vuoi veramente eliminare questo frutto?');" />
                            <asp:Label ID="lblIsDelFrutto" runat="server" Text="" CssClass="pull-right"></asp:Label>
                        </div>
                    </div>
                    <!-- Fine Elimina Frutto -->
                </div>

                <!-- Mostra Frutti -->
                <div class="col-md-6">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Mostra Lista Frutti Inseriti</h3>
                        </div>
                        <div class="panel-body">
                            <asp:Label ID="lblDelMostraListaFrutti" runat="server" Text="Nome Frutto"></asp:Label>
                            <ul class="list-group">
                                <% foreach (var item in fruttiList)
                                    { %>
                                <li class="list-group-item"><%= item.Descr %></li>
                                <% } %>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- Fine Mostra Frutti -->
            </div>
        </asp:Panel>
    </div>
</asp:Content>
