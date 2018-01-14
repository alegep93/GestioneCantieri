<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="Listino.aspx.cs" Inherits="GestioneCantieri.Listino" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    <title>Listino Mef</title>
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>Listino Mef</h1>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12 text-center">
                <div class="col-md-offset-3 col-md-6">
                    <h1 id="filterToggle" class="btn btn-default btn-lg"></h1>
                </div>
            </div>
            <div id="filterContainer">
                <!-- Ricerca Per CodArt e DescriCodArt -->
                <div class="searchFilterContainer col-md-4 col-md-offset-3">
                    <asp:Label ID="Label1" runat="server" Text="CodArt & DescriCodArt"></asp:Label>
                    <div class="col-md-12">
                        <asp:Label ID="lblCercaCodArt" runat="server" Text="Cerca per codice articolo"></asp:Label>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtCodArt1" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtCodArt2" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtCodArt3" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <asp:Label ID="lblCercaDescriCodArt" runat="server" Text="Cerca per Descrizione Cod. Art."></asp:Label>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtDescriCodArt1" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtDescriCodArt2" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtDescriCodArt3" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <!-- Cerca e Svuota -->
                <div class="searchFilterContainer col-md-2 text-center">
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" CssClass="btn btn-primary btn-lg" Text="Cerca" />
                    </div>
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
                <asp:GridView ID="grdListino" runat="server" ItemType="GestioneCantieri.Data.Mamg0" AutoGenerateColumns="False" CssClass="table table-striped table-responsive text-center">
                    <Columns>
                        <asp:BoundField DataField="CodArt" HeaderText="Codice Articolo" />
                        <asp:BoundField DataField="Desc" HeaderText="Descrizione" />
                        <asp:BoundField DataField="UnitMis" HeaderText="Unità di Misura" />
                        <asp:BoundField DataField="Pezzo" HeaderText="Pezzo" />
                        <asp:BoundField DataField="PrezzoListino" HeaderText="Prezzo Listino" />
                        <asp:BoundField DataField="Sconto1" HeaderText="Sconto 1" />
                        <asp:BoundField DataField="Sconto2" HeaderText="Sconto2" />
                        <asp:BoundField DataField="Sconto3" HeaderText="Sconto3" />
                        <asp:BoundField DataField="PrezzoNetto" HeaderText="Prezzo Netto" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
