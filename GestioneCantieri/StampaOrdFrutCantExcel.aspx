<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="StampaOrdFrutCantExcel.aspx.cs" Inherits="GestioneCantieri.StampaExcell" EnableEventValidation="false" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    <title>Stampa Ord Frut Cant Excel</title>
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script src="Scripts/DynamicStampaOrdFrutStyle.js"></script>
    <style>
        .table-container {
            max-height: 500px;
            overflow: hidden;
            overflow-y: scroll;
        }
    </style>
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>Stampa Ord Frut Cant Excel</h1>
    <div class="container-fluid">
        <div class="row">
            <div class="ddlContainer col-md-6">
                <asp:Label ID="lblScegliCantiere" runat="server" Text="Scegli Cantiere"></asp:Label>
                <asp:DropDownList ID="ddlScegliCantiere" CssClass="form-control" OnTextChanged="ddlScegliCantiere_TextChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                <asp:Button ID="btnPrint" CssClass="btn btn-primary btn-lg pull-right" OnClick="btnPrint_Click" runat="server" Text="Stampa" />
            </div>
            <div class="col-md-6 table-container" style="position: relative; top: 10px;">
                <asp:GridView ID="grdGruppi" AutoGenerateColumns="false" ItemType="GestioneCantieri.Data.StampaCantiere" runat="server" CssClass="table table-striped table-responsive" Visible="false">
                    <Columns>
                        <asp:BoundField HeaderText="Descrizione Frutto" DataField="DescrFrutto" />
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
    </div>
</asp:Content>
