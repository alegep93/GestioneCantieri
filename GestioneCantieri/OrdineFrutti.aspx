<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="OrdineFrutti.aspx.cs" Inherits="GestioneCantieri.OrdineFrutti" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    <title>Ordine Frutti</title>
    <style>
        #body_lblIsGruppoInserito {
            margin-bottom: 30px;
            text-align: right;
        }

        span.pull-right {
            position: relative;
            top: 10px;
            right: 10px;
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
                <!-- Scegli Locale -->
                <div class="col-md-offset-3 col-md-6">
                    <asp:Label ID="lblScegliLocale" runat="server" Text="Scegli Locale"></asp:Label>
                    <asp:DropDownList ID="ddlScegliLocale" CssClass="form-control" OnTextChanged="ddlScegliLocale_TextChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <asp:Panel ID="pnlScegliGruppo" runat="server">
                <div class="row">
                    <div class="col-md-6">
                        <!-- Filtri sui nomi dei gruppi presenti su DB -->
                        <div class="col-md-4">
                            <asp:Label ID="lblFiltro1" runat="server" Text="Filtro 1"></asp:Label>
                            <asp:TextBox ID="txtFiltroGruppo1" placeholder="Filtro 1" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblFiltro2" runat="server" Text="Filtro 2"></asp:Label>
                            <asp:TextBox ID="txtFiltroGruppo2" placeholder="Filtro 2" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblFiltro3" runat="server" Text="Filtro 3"></asp:Label>
                            <asp:TextBox ID="txtFiltroGruppo3" placeholder="Filtro 3" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <asp:Button ID="btnFiltroGruppi" CssClass="btn btn-primary btn-lg pull-right" OnClick="btnFiltroGruppi_Click" runat="server" Text="Filtra Gruppi" />
                        </div>
                    </div>

                    <div class="col-md-6">
                        <!-- Filtri sui nomi dei gruppi presenti su DB -->
                        <div class="col-md-4">
                            <asp:Label ID="lblFiltroFrutto1" runat="server" Text="Filtro Frutto 1"></asp:Label>
                            <asp:TextBox ID="txtFiltroFrutto1" placeholder="Filtro Frutto 1" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblFiltroFrutto2" runat="server" Text="Filtro Frutto 2"></asp:Label>
                            <asp:TextBox ID="txtFiltroFrutto2" placeholder="Filtro Frutto 2" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblFiltroFrutto3" runat="server" Text="Filtro Frutto 3"></asp:Label>
                            <asp:TextBox ID="txtFiltroFrutto3" placeholder="Filtro Frutto 3" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <asp:Button ID="btnFiltraFrutti" CssClass="btn btn-primary btn-lg pull-right" OnClick="btnFiltraFrutti_Click" runat="server" Text="Filtra Frutti" />
                        </div>
                    </div>
                </div>

                <!-- Lista dei gruppi (filtrati o non) -->
                <div class="col-md-6">
                    <asp:Label ID="lblScegliGruppo" runat="server" Text="Scegli Gruppo"></asp:Label>
                    <asp:DropDownList ID="ddlScegliGruppo" CssClass="form-control" AutoPostBack="true" OnTextChanged="ddlScegliGruppo_TextChanged" runat="server"></asp:DropDownList>
                    <asp:Button ID="btnInserisciGruppo" CssClass="btn btn-primary btn-lg pull-right" OnClick="btnInserisciGruppo_Click" runat="server" Text="Inserisci Gruppo" />
                    <asp:Label ID="lblIsGruppoInserito" CssClass="pull-right" runat="server" Text=""></asp:Label>
                </div>

                <!-- Lista e Qta dei frutti da inserire -->
                <div class="col-md-6">
                    <asp:Label ID="lblScegliFrutto" runat="server" Text="Scegli Frutto"></asp:Label>
                    <asp:DropDownList ID="ddlScegliFrutto" CssClass="form-control" AutoPostBack="true" OnTextChanged="ddlScegliFrutto_TextChanged" runat="server"></asp:DropDownList>
                    <asp:Label ID="lblQtaFrutto" runat="server" Text="Quantità Frutto"></asp:Label>
                    <asp:TextBox ID="txtQtaFrutto" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:Button ID="btnInserisciFrutto" CssClass="btn btn-primary btn-lg pull-right" OnClick="btnInserisciFrutto_Click" runat="server" Text="Inserisci Frutto" />
                    <asp:Label ID="lblIsFruttoInserito" CssClass="pull-right" runat="server" Text=""></asp:Label>
                </div>
            </asp:Panel>
        </div>

        <%--<div class="row">
            <asp:Panel ID="pnlMostraGruppiInseriti" runat="server">
                <div class="col-md-6">
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
                <div class="col-md-6">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Frutti aggiunti all'ordine</h3>
                        </div>
                        <div class="panel-body">
                            <asp:Label ID="lblQtaDescrFrutti" runat="server" Text="Qta - Descrizione"></asp:Label>
                            <ul class="list-group">
                                <% foreach (var item in fruttiList)
                                    {%>
                                <li class="list-group-item"><%= item.QtaFrutti + " - " + item.Descrizione %></li>
                                <%} %>
                            </ul>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>--%>

        <div class="row">
            <div class="tableContainer col-md-12 table-responsive">
                <asp:GridView ID="grdOrdini" runat="server" ItemType="GestioneCantieri.Data.MatOrdFrut" OnRowCommand="grdOrdini_RowCommand" AutoGenerateColumns="False" CssClass="table table-striped text-center">
                    <Columns>
                        <asp:BoundField DataField="DescrCant" HeaderText="Cantiere" />
                        <asp:BoundField DataField="Appartamento" HeaderText="Locale" />
                        <asp:BoundField DataField="NomeGruppo" HeaderText="Nome Gruppo" />
                        <asp:BoundField DataField="NomeFrutto" HeaderText="Nome Frutto" />
                        <asp:BoundField DataField="QtaFrutti" HeaderText="Quantità Frutti" />
                        <%--<asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnVisualMatCant" CommandName="VisualMatCant" CommandArgument="<%# BindItem.IdMaterialiCantieri %>" CssClass="btn btn-lg btn-default" runat="server" Text="Visualizza" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <%--<asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnModificaOrdine" CommandName="ModificaOrdine" CommandArgument="<%# BindItem.Id %>" CssClass="btn btn-lg btn-default" runat="server" Text="Modifica" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnElimiminaOrdine" CommandName="EliminaOrdine" CommandArgument="<%# BindItem.Id %>"
                                    CssClass="btn btn-lg btn-default" runat="server" Text="Elimina" OnClientClick="return confirm('Vuoi veramente eliminare questo ordine?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
