<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="GestionePagamenti.aspx.cs" Inherits="GestioneCantieri.GestionePagamenti" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    <title>Gestione Pagamenti</title>
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

        span.pull-right{
            position: relative;
            top: 13px;
            right: 10px;
        }
    </style>
    </asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>Gestione Pagamenti</h1>
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

    <asp:Panel ID="pnlGestPagam" CssClass="col-md-12" runat="server">
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-6">
                    <asp:Label ID="lblImporto" Text="Importo" runat="server" />
                    <asp:TextBox ID="txtImporto" CssClass="form-control" runat="server" Text=""></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblDescr" Text="Descrizione" runat="server" />
                    <asp:TextBox ID="txtDescr" CssClass="form-control" runat="server" Text=""></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="col-md-2">
                    <asp:Label ID="lblAcconto" Text="Acconto" runat="server" />
                    <asp:CheckBox ID="chkACconto" CssClass="form-control" Enabled="false" runat="server" />
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblSaldo" Text="Saldo" runat="server" />
                    <asp:CheckBox ID="chkSaldo" CssClass="form-control" Enabled="false" runat="server" />
                </div>
                <div class="col-md-8">
                    <asp:Button ID="btnInserisci" OnClick="btnInserisci_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Record" />
                    <asp:Label ID="lblIsPagamInserito" Text="" CssClass="pull-right" runat="server" />
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>