<%@ Page Title="" Language="C#" MasterPageFile="~/layout.Master" AutoEventWireup="true" CodeBehind="GestioneAppunti.aspx.cs" Inherits="GestioneCantieri.GestioneAppunti" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    <title>Gestione Appunti</title>
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>Gestione Appunti</h1>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-offset-3 col-md-6">
                <asp:Label ID="lblScegliTipologia" Text="Tipologia" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlTipologia" CssClass="form-control" runat="server">
                    <asp:ListItem Value="cantiere">Da Segnare in Cantiere</asp:ListItem>
                    <asp:ListItem Value="cassa">Da Segnare in Cassa</asp:ListItem>
                </asp:DropDownList>

                <asp:Label ID="lblData" runat="server" Text="Data:"></asp:Label>
                <asp:TextBox ID="txtData" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>

                <asp:Panel ID="pnlCantiere" runat="server">

                </asp:Panel>

                <asp:Panel ID="pnlCassa" runat="server">
                    <asp:Label ID="lblAppuntiCassa" Text="Appunti" runat="server"></asp:Label>
                    <asp:TextBox ID="txtAppuntiCassa" Text="" CssClass="form-control" runat="server"></asp:TextBox>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
