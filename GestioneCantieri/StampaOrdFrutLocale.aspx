<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="StampaOrdFrutLocale.aspx.cs" Inherits="GestioneCantieri.StampaOrdFrutLocale" EnableEventValidation="false" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    <title>Stampa Ord Frut Locale</title>
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $(".groupHeader").css("font-weight", "bold")
            $("#body_grdGruppiInLocale td:not(.groupHeader)").css("padding-left", 40)
        });
    </script>
    <style>
        .table-container > div {
            max-height: 500px;
            overflow: hidden;
            overflow-y: auto;
        }
    </style>
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>Stampa Ord Frut Loc</h1>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-offset-3 col-md-6">
                <asp:Label ID="lblScegliCantiere" runat="server" Text="Scegli Cantiere"></asp:Label>
                <asp:DropDownList ID="ddlScegliCantiere" CssClass="form-control" OnTextChanged="ddlScegliCantiere_TextChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="col-md-6 table-container">
                <asp:GridView ID="grdGruppiInLocale" AllowSorting="true" OnSorting="grdGruppiInLocale_Sorting" AutoGenerateColumns="false" ItemType="GestioneCantieri.Data.StampaCantiere" runat="server" CssClass="table table-striped table-responsive">
                    <Columns>
                        <asp:BoundField HeaderText="Nome Locale" DataField="NomeLocale" />
                        <asp:BoundField HeaderText="Nome Locale / Gruppi Contenuti" DataField="NomeGruppo" />
                        <asp:BoundField HeaderText="Quantità" DataField="Qta" />
                    </Columns>
                </asp:GridView>
            </div>

            <div class="col-md-6 table-container">
                <asp:GridView ID="grdFruttiInLocale" AutoGenerateColumns="false" ItemType="GestioneCantieri.Data.StampaCantiere" runat="server" CssClass="table table-striped table-responsive">
                    <Columns>
                        <asp:BoundField HeaderText="Descrizione Frutto" DataField="Descr001" />
                        <asp:BoundField HeaderText="Quantità (tot.)" DataField="Qta" />
                    </Columns>
                </asp:GridView>
            </div>

            <asp:GridView ID="grdFruttiNonInGruppo" AutoGenerateColumns="false" ItemType="GestioneCantieri.Data.StampaCantiere" runat="server" CssClass="table table-striped table-responsive" Visible="false">
                <Columns>
                    <asp:BoundField HeaderText="Descrizione Frutto" DataField="Descr001" />
                    <asp:BoundField HeaderText="Quantità (tot.)" DataField="Qta" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
