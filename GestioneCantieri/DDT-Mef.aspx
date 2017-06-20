<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="DDT-Mef.aspx.cs" Inherits="GestioneCantieri.Default" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    <title>Gestione DDT Mef</title>
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>DDT Mef</h1>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12 text-center">
                <div class="col-md-offset-3 col-md-6">
                    <h1 id="filterToggle" class="btn btn-default btn-lg"></h1>
                </div>
            </div>
            <div id="filterContainer">
                <!-- Ricerca Per Anno -->
                <div class="searchFilterContainer col-md-3">
                    <asp:Label ID="lblCercaAnno" runat="server" Text="Cerca per anno"></asp:Label>
                    <div class="col-md-12">
                        <asp:Label ID="lblAnnoInizio" runat="server" Text="Anno Iniziale"></asp:Label>
                        <asp:TextBox ID="txtAnnoInizio" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-12">
                        <asp:Label ID="lblAnnoFine" runat="server" Text="Anno Finale"></asp:Label>
                        <asp:TextBox ID="txtAnnoFine" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <!-- Ricerca Per Data -->
                <div class="searchFilterContainer col-md-3">
                    <asp:Label ID="lblCercaData" runat="server" Text="Cerca per data"></asp:Label>
                    <div class="col-md-12">
                        <asp:Label ID="lblDataInizio" runat="server" Text="Data Inizio"></asp:Label>
                        <asp:TextBox ID="txtDataInizio" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-12">
                        <asp:Label ID="lblDataFine" runat="server" Text="Data Fine"></asp:Label>
                        <asp:TextBox ID="txtDataFine" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <!-- Ricerca Per CodArt e DescriCodArt -->
                <div class="searchFilterContainer col-md-4">
                    <asp:Label ID="Label1" runat="server" Text="CodArt & DescriCodArt"></asp:Label>
                    <div class="col-md-12">
                        <asp:Label ID="lblCercaCodArt" runat="server" Text="Cerca per codice articolo"></asp:Label>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtCodArt1" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtCodArt2" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtCodArt3" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <asp:Label ID="lblCercaDescriCodArt" runat="server" Text="Cerca per Descrizione Cod. Art."></asp:Label>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtDescriCodArt1" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtDescriCodArt2" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtDescriCodArt3" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <!-- Svuota e Media -->
                <div class="searchFilterContainer col-md-2 text-center">
                    <asp:Label ID="lblMedia" runat="server" Text="Media Prezzo Unitario"></asp:Label>
                    <asp:TextBox ID="txtMedia" Enabled="false" runat="server" Style="width: 70%;"></asp:TextBox>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" CssClass="btn btn-primary btn-lg" Text="Cerca" />
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSvuotaTxt" runat="server" OnClick="btnSvuotaTxt_Click" Text="Svuota Caselle di Testo" CssClass="btn btn-default btn-lg" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:GridView ID="grdListaDDTMef" runat="server" OnRowDataBound="grdListaDDTMef_RowDataBound" ItemType="GestioneCantieri.Data.DDTMef"
                    AutoGenerateColumns="False" OnRowCommand="grdListaDDTMef_RowCommand"
                    PageSize="20" OnPageIndexChanging="grdListaDDTMef_PageIndexChanging" CssClass="table table-striped table-responsive text-center">
                    <Columns>
                        <%--<asp:BoundField DataField="Id" HeaderText="ID DDT Mef" />--%>
                        <asp:BoundField DataField="Anno" HeaderText="Anno" />
                        <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:d}" ApplyFormatInEditMode="True" />
                        <asp:BoundField DataField="N_ddt" HeaderText="N_DDT" />
                        <asp:BoundField DataField="CodArt" HeaderText="Codice Articolo" />
                        <asp:BoundField DataField="DescriCodArt" HeaderText="Descrizione Codice Articolo" />
                        <asp:BoundField DataField="Qta" HeaderText="Quantità" />
                        <asp:BoundField DataField="Importo" HeaderText="Importo" DataFormatString="{0:0.00}" />
                        <asp:BoundField DataField="Acquirente" HeaderText="Acquirente" />
                        <asp:BoundField DataField="PrezzoUnitario" HeaderText="Prezzo Unitario" DataFormatString="{0:0.00}" />
                        <asp:BoundField DataField="AnnoN_ddt" HeaderText="Anno N_DDT" />
                    </Columns>
                    <PagerSettings Mode="Numeric" Position="Bottom" PageButtonCount="20" />
                    <PagerStyle ForeColor="#333" BorderWidth="0" BorderColor="Transparent" BorderStyle="None" CssClass="text-center pagination-container" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
