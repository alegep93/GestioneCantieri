<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="Listino.aspx.cs" Inherits="GestioneCantieri.Listino" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    <title>Listino Mef</title>
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>Listino Mef</h1>
    <div class="container-fluid">
        <div ng-controller="listinoController">
            <div class="row">
                <div class="col-md-12 text-center">
                    <div class="col-md-offset-3 col-md-6">
                        <h1 id="filterToggle" class="btn btn-default btn-lg"></h1>
                    </div>
                </div>
                <div id="filterContainer">
                    <!-- Ricerca Per CodArt e DescriCodArt -->
                    <div class="searchFilterContainer col-md-4 col-md-offset-3">
                        <asp:Label ID="Label1" runat="server" Text="Cerca per CodArt e/o DescriCodArt"></asp:Label>
                        <div class="col-md-12">
                            <asp:TextBox ID="txtCodArt1" CssClass="form-control" runat="server" ng-model="searchText"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Svuota ricerca ed elimina listino da DB -->
                    <div class="searchFilterContainer col-md-2 text-center">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSvuotaTxt" runat="server" OnClick="btnSvuotaTxt_Click" Text="Svuota Caselle di Testo" CssClass="btn btn-default btn-lg" />
                        </div>
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnEliminaListino" runat="server" OnClick="btnEliminaListino_Click" Text="ELIMINA LISTINO" CssClass="btn btn-danger btn-lg" OnClientClick="return confirm('Vuoi veramente eliminare TUTTO il listino?');" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <table class="table table-striped table-responsive text-center">
                        <thead>
                            <tr>
                                <th>Codice Articolo</th>
                                <th>Descrizione</th>
                                <th>Unità di Misura</th>
                                <th>Prezzo</th>
                                <th>Prezzo Listino</th>
                                <th>Sconto 1</th>
                                <th>Sconto 2</th>
                                <th>Sconto 3</th>
                                <th>Prezzo Netto</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr ng-repeat="listino in listini | filter:search">
                                <td>{{listino.CodArt}}</td>
                                <td>{{listino.Desc}}</td>
                                <td>{{listino.UnitMis}}</td>
                                <td>{{listino.Pezzo}}</td>
                                <td>{{listino.PrezzoListino}}</td>
                                <td>{{listino.Sconto1}}</td>
                                <td>{{listino.Sconto2}}</td>
                                <td>{{listino.Sconto3}}</td>
                                <td>{{listino.PrezzoNetto}}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
