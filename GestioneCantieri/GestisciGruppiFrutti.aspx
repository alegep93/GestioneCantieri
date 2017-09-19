<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="GestisciGruppiFrutti.aspx.cs" Inherits="GestioneCantieri.DistintaBase" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    <title>Gestisci Gruppi Frutti</title>
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
                <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></h1>
            <!-- Fine Titolo Pagina -->

            <!-- Filtri sulla lista di gruppi -->
            <div class="col-md-3"></div>
            <div class="col-md-3">
                <div class="col-md-4">
                    <asp:TextBox ID="txtFiltroGruppi1" placeholder="Filtro 1" OnTextChanged="txtFiltroGruppi1_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtFiltroGruppi2" placeholder="Filtro 2" OnTextChanged="txtFiltroGruppi2_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtFiltroGruppi3" placeholder="Filtro 3" OnTextChanged="txtFiltroGruppi3_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-6"></div>
            <!-- Fine Filtri sulla lista di gruppi -->
        </div>

        <asp:Panel ID="pnlInserisci" runat="server">
            <div class="row">
                <!-- Creazione Gruppo -->
                <div class="col-md-3">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Crea Gruppo</h3>
                        </div>
                        <div class="panel-body">
                            <asp:Label ID="lblNomeGruppo" runat="server" Text="Nome Gruppo"></asp:Label>
                            <asp:TextBox ID="txtNomeGruppo" CssClass="form-control" runat="server"></asp:TextBox>

                            <asp:Label ID="lblInsDescrGruppo" runat="server" Text="Descrizione Gruppo"></asp:Label>
                            <asp:TextBox ID="txaDescr" TextMode="MultiLine" CssClass="form-control" runat="server" Rows="10"></asp:TextBox>

                            <asp:Button ID="btnCreaGruppo" OnClick="btnCreaGruppo_Click" CssClass="btn btn-primary pull-left" runat="server" Text="Crea Gruppo" />
                            <asp:Label ID="lblInserimento" runat="server" Text="" CssClass="pull-right"></asp:Label>
                        </div>
                    </div>
                    <!-- Fine Creazione Gruppo -->
                </div>

                <!-- Mostra Gruppi Inseriti -->
                <div class="col-md-3">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Mostra gruppi inseriti</h3>
                        </div>
                        <div class="panel-body">
                            <asp:Label ID="lblInsGruppiInseritiList" runat="server" Text="Nome Gruppo"></asp:Label>
                            <ul class="list-group">
                                <% foreach (var item in gruppiList)
                                    {%>
                                <li class="list-group-item"><%= item.NomeGruppo %></li>
                                <%} %>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- Fine Mostra Gruppi Inseriti -->

                <!-- Inserimento frutti in Gruppo -->
                <div class="col-md-3">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Aggiungi Frutto a Gruppo</h3>
                        </div>
                        <div class="panel-body">
                            <asp:Label ID="lblElencoGruppi" runat="server" Text="Gruppi"></asp:Label>
                            <asp:DropDownList ID="ddlGruppi" CssClass="form-control" runat="server" OnTextChanged="ddlGruppi_TextChanged" AutoPostBack="true"></asp:DropDownList>

                            <asp:Label ID="lblDescrGruppo" runat="server" Text="Descrizione Gruppo"></asp:Label>
                            <asp:TextBox ID="txaShowDescrGruppo" TextMode="MultiLine" ReadOnly="true" CssClass="form-control" runat="server" Rows="5"></asp:TextBox>

                            <asp:Panel ID="nuovoFruttoPanel" runat="server">
                                <asp:Label ID="lblElencoFrutti" runat="server" Text="Frutti"></asp:Label>
                                <asp:DropDownList ID="ddlFrutti" AutoPostBack="true" OnTextChanged="ddlFrutti_TextChanged" CssClass="form-control" runat="server"></asp:DropDownList>

                                <asp:Label ID="lblQuantita" runat="server" Text="Quantità"></asp:Label>
                                <asp:TextBox ID="txtQta" CssClass="form-control" runat="server"></asp:TextBox>

                                <asp:Button ID="btnInsCompgruppo" OnClick="btnInsCompgruppo_Click" CssClass="btn btn-primary pull-left" runat="server" Text="Aggiungi Frutto" />
                                <asp:Button ID="btnCompletaGruppo" OnClick="btnCompletaGruppo_Click" CssClass="btn btn-primary pull-right" runat="server" Text="Completa Gruppo" />
                                <asp:Label ID="lblFruttoAggiungo" runat="server" Text="" CssClass="pull-right"></asp:Label>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <!-- Fine Inserimento frutti in Gruppo -->

                <!-- Mostra contenuto Gruppo -->
                <div class="col-md-3">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Mostra contenuto gruppo selezionato</h3>
                        </div>
                        <div class="panel-body">
                            <asp:Label ID="lblQtaDescr" runat="server" Text="Qta - Descrizione"></asp:Label>
                            <ul class="list-group">
                                <% foreach (var item in compList)
                                    {%>
                                <li class="list-group-item"><%= item.Qta + " - " + item.NomeFrutto %></li>
                                <%} %>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- Fine Mostra contenuto Gruppo -->
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlModifica" runat="server">
            <div class="row">
                <!-- Modifica Gruppo -->
                <div class="col-md-3">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Modifica Gruppo</h3>
                        </div>
                        <div class="panel-body">
                            <asp:Label ID="lblScegliGruppo" runat="server" Text="Nome Gruppo"></asp:Label>
                            <asp:DropDownList ID="ddlModScegliGruppo" CssClass="form-control" runat="server" OnTextChanged="ddlModMostraGruppi_TextChanged" AutoPostBack="true"></asp:DropDownList>

                            <asp:Panel ID="pnlModGruppo" runat="server">
                                <asp:Label ID="lblModNomeGruppo" runat="server" Text="Nome Gruppo"></asp:Label>
                                <asp:TextBox ID="txtModNomeGruppo" CssClass="form-control" runat="server"></asp:TextBox>

                                <asp:Label ID="lblModDescrGruppo" runat="server" Text="Descrizione Gruppo"></asp:Label>
                                <asp:TextBox ID="txtModDescrGruppo" TextMode="MultiLine" Rows="10" CssClass="form-control" runat="server"></asp:TextBox>

                                <asp:Button ID="btnSaveModGruppo" OnClick="btnSaveModGruppo_Click" CssClass="btn btn-primary pull-left" runat="server" Text="Modifica Gruppo" />
                                <asp:Button ID="btnRiapriGruppo" OnClick="btnRiapriGruppo_Click" CssClass="btn btn-primary pull-right" runat="server" Text="Riapri Gruppo" />
                                <asp:Label ID="lblSaveModGruppo" runat="server" Text="" CssClass="pull-right"></asp:Label>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <!-- Fine Modifica Gruppo -->

                <!-- Mostra Gruppi Inseriti -->
                <div class="col-md-3">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Mostra gruppi inseriti</h3>
                        </div>
                        <div class="panel-body">
                            <asp:Label ID="Label1" runat="server" Text="Nome Gruppo"></asp:Label>
                            <ul class="list-group">
                                <% foreach (var item in gruppiList)
                                    {%>
                                <li class="list-group-item"><%= item.NomeGruppo %></li>
                                <%} %>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- Fine Mostra Gruppi Inseriti -->

                <!-- Mostra contenuto Gruppo -->
                <div class="col-md-3">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Mostra contenuto gruppo selezionato</h3>
                        </div>
                        <div class="panel-body">
                            <asp:Label ID="Label2" runat="server" Text="Qta - Descrizione"></asp:Label>
                            <ul class="list-group">
                                <% foreach (var item in compList)
                                    {%>
                                <li class="list-group-item"><%= item.Qta + " - " + item.NomeFrutto %></li>
                                <%} %>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- Fine Mostra contenuto Gruppo -->
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlElimina" runat="server">
            <div class="row">
                <div class="col-md-3">
                    <!-- Elimina Gruppo -->
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Elimina Gruppo</h3>
                        </div>
                        <div class="panel-body">
                            <asp:Label ID="lblDelGruppo" runat="server" Text="Nome Gruppo"></asp:Label>
                            <asp:DropDownList ID="ddlDelGruppo" OnTextChanged="ddlDelGruppo_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>

                            <asp:Button ID="btnDelGruppo" OnClick="btnDelGruppo_Click" CssClass="btn btn-primary pull-left" runat="server" Text="Elimina Gruppo" OnClientClick="return confirm('Vuoi veramente eliminare questo gruppo?');" />
                            <asp:Label ID="lblIsDelGruppo" runat="server" Text="" CssClass="pull-right"></asp:Label>
                        </div>
                    </div>
                    <!-- Fine Elimina Gruppo -->
                </div>

                <!-- Mostra Gruppi Inseriti -->
                <div class="col-md-3">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Mostra gruppi inseriti</h3>
                        </div>
                        <div class="panel-body">
                            <asp:Label ID="Label3" runat="server" Text="Nome Gruppo"></asp:Label>
                            <ul class="list-group">
                                <% foreach (var item in gruppiList)
                                    {%>
                                <li class="list-group-item"><%= item.NomeGruppo %></li>
                                <%} %>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- Fine Mostra Gruppi Inseriti -->

                <!-- Elimina componenti Gruppo -->
                <div class="col-md-3">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Elimina compotente Gruppo</h3>
                        </div>
                        <div class="panel-body">
                            <asp:Label ID="lblDelNomeGruppo" runat="server" Text="Gruppi"></asp:Label>
                            <asp:DropDownList ID="ddlDelNomeGruppo" CssClass="form-control" runat="server" OnTextChanged="ddlDelNomeGruppo_TextChanged" AutoPostBack="true"></asp:DropDownList>

                            <asp:Label ID="lblDelDescrGruppo" runat="server" Text="Descrizione Gruppo"></asp:Label>
                            <asp:TextBox ID="txtDelDescrGruppo" TextMode="MultiLine" ReadOnly="true" CssClass="form-control" runat="server" Rows="5"></asp:TextBox>

                            <asp:Panel ID="pnlDelCompGrup" runat="server">
                                <asp:Label ID="lblDelCompGrup" runat="server" Text="Componenti Gruppo"></asp:Label>
                                <asp:DropDownList ID="ddlDelCompGrup" OnTextChanged="ddlDelCompGrup_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>

                                <asp:Button ID="btnDelCompGruppo" OnClick="btnDelCompGruppo_Click" CssClass="btn btn-primary pull-left" runat="server" Text="Elimina Componente Gruppo" OnClientClick="return confirm('Vuoi veramente eliminare questo componente?');" />
                                <asp:Label ID="lblIsDelCompGruppo" runat="server" Text="" CssClass="pull-right"></asp:Label>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <!-- Fine Elimina componenti Gruppo -->

                <!-- Mostra contenuto Gruppo -->
                <div class="col-md-3">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Mostra contenuto gruppo selezionato</h3>
                        </div>
                        <div class="panel-body">
                            <asp:Label ID="lblDelMostraCompGruppo" runat="server" Text="Qta - Descrizione"></asp:Label>
                            <ul class="list-group">
                                <% foreach (var item in compList)
                                    { %>
                                <li class="list-group-item"><%= item.Qta + " - " + item.NomeFrutto %></li>
                                <% } %>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- Fine Mostra contenuto Gruppo -->
            </div>
        </asp:Panel>
    </div>
</asp:Content>
