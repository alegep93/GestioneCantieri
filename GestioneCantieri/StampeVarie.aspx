﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="StampeVarie.aspx.cs" Inherits="GestioneCantieri.StampeVarie" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    <title>Stampe Varie</title>
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>Stampe Varie</h1>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-offset-3 col-md-6">
                <asp:Label ID="lblScegliStampa" runat="server" Text="Seleziona Stampa"></asp:Label>
                <asp:DropDownList ID="ddlScegliStampa" CssClass="form-control" AutoPostBack="true" OnTextChanged="ddlScegliStampa_TextChanged" runat="server"></asp:DropDownList>
            </div>
        </div>

        <asp:Panel ID="pnlCampiStampaDDT" runat="server">
            <div class="col-md-4">
                <asp:Label ID="lblDataDa" runat="server" Text="Data Da:"></asp:Label>
                <asp:TextBox ID="txtDataDa" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblDataA" runat="server" Text="Data A:"></asp:Label>
                <asp:TextBox ID="txtDataA" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblScegliFornitore" runat="server" Text="Scegli Fornitore"></asp:Label>
                <asp:DropDownList ID="ddlScegliFornitore" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblScegliAcquirente" runat="server" Text="Scegli Acquirente"></asp:Label>
                <asp:DropDownList ID="ddlScegliAcquirente" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblNumDDT" runat="server" Text="Numero DDT"></asp:Label>
                <asp:TextBox ID="txtNumDDT" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <asp:Label ID="lblNomeFile" runat="server" Text="Nome File"></asp:Label>
                <asp:TextBox ID="txtNomeFile" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-12">
                <asp:Button ID="btnShowGridView" CssClass="btn btn-lg btn-primary pull-right" OnClick="btnShowGridView_Click" runat="server" Text="Mostra Risultati" />
            </div>
        </asp:Panel>

        <asp:GridView ID="grdStampaDDT" runat="server" ItemType="GestioneCantieri.Data.DDTMef" AutoGenerateColumns="False" CssClass="table table-striped table-responsive text-center">
            <Columns>
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
        </asp:GridView>

        <div class="col-md-12">
            <asp:Button ID="btnStampaDDT" CssClass="btn btn-lg btn-primary pull-right" OnClick="btnStampaDDT_Click" runat="server" Text="Stampa DDT" />
        </div>
    </div>
</asp:Content>
