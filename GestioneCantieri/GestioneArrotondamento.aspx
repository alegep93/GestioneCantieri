<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="GestioneArrotondamento.aspx.cs" Inherits="GestioneCantieri.Gestione_Arrotondamento" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    <title>Gestione Arrotondamento</title>
    <style type="text/css">
        span.form-control {
            border: none;
            background-color: transparent;
            box-shadow: none;
            -webkit-box-shadow: none;
        }

        input[type="checkbox"] {
            width: 20px;
            height: 20px;
            position: relative;
            left: -10px;
        }

        input.btn.btn-lg.btn-primary.pull-left {
            position: relative;
            top: 6px;
        }
    </style>
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>Gestione Arrotondamento</h1>
    <asp:Panel ID="pnlIntestazione" CssClass="col-md-12" runat="server">
        <asp:Panel ID="pnlFiltriSceltaCant" CssClass="col-md-offset-2 col-md-8" runat="server">
            <div class="col-md-2">
                <asp:Label ID="lblFiltroCantAnno" Text="Anno" runat="server" />
                <asp:TextBox ID="txtFiltroCantAnno" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblFiltroCantCodCant" Text="Cod Cant" runat="server" />
                <asp:TextBox ID="txtFiltroCantCodCant" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblFiltroCantDescrCodCant" Text="Descri Cod Cant" runat="server" />
                <asp:TextBox ID="txtFiltroCantDescrCodCant" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblFiltroCantChiuso" Text="Chiuso" runat="server" />
                <asp:CheckBox ID="chkFiltroCantChiuso" CssClass="form-control" Checked="false" runat="server" />
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblFiltroCantRiscosso" Text="Riscosso" runat="server" />
                <asp:CheckBox ID="chkFiltroCantRiscosso" CssClass="form-control" Checked="false" runat="server" />
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnFiltroCant" CssClass="btn btn-lg btn-primary pull-left" OnClick="btnFiltroCant_Click" runat="server" Text="Filtra" />
            </div>
        </asp:Panel>
        <div class="col-md-offset-2 col-md-8">
            <asp:Label ID="lblScegliCant" Text="Scegli Cantiere" runat="server" />
            <asp:DropDownList ID="ddlScegliCant" CssClass="form-control" AutoPostBack="true" OnTextChanged="ddlScegliCant_TextChanged" runat="server" />
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlGestArrotond" CssClass="col-md-12" runat="server">
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-6">
                    <asp:Label ID="lblCodArt" Text="Codice Articolo" runat="server" />
                    <asp:TextBox ID="txtCodArt" CssClass="form-control" runat="server" Text="Arrotondamento"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblDescriCodArt" Text="Descrizione Codice Articolo" runat="server" />
                    <asp:TextBox ID="txtDescriCodArt" CssClass="form-control" runat="server" Text="Arrotondamento"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="col-md-6">
                    <asp:Label ID="lblQta" Text="Quantità" runat="server" />
                    <asp:TextBox ID="txtQta" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblPzzoUnit" Text="Prezzo Unitario" runat="server" />
                    <asp:TextBox ID="txtPzzoUnit" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="col-md-2">
                    <asp:Label ID="lblVisibile" Text="Visibile" runat="server" />
                    <asp:CheckBox ID="chkVisibile" CssClass="form-control" Enabled="false" runat="server" />
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblRicalcolo" Text="Ricalcolo" runat="server" />
                    <asp:CheckBox ID="chkRicalcolo" CssClass="form-control" Enabled="false" runat="server" />
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblRicarico" Text="Ricarico Si/No" runat="server" />
                    <asp:CheckBox ID="chkRicarico" CssClass="form-control" Enabled="false" runat="server" />
                </div>
                <div class="col-md-6">
                    <asp:Button ID="btnInserisci" OnClick="btnInserisci_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Record" />
                    <asp:Label ID="lblIsArrotondInserito" Text="" runat="server" />
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
