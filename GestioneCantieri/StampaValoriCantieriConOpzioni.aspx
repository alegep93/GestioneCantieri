<%@ Page Language="C#" MasterPageFile="~/layout.Master" AutoEventWireup="true" CodeBehind="StampaValoriCantieriConOpzioni.aspx.cs" Inherits="GestioneCantieri.StampaValoriCantieriConOpzioni" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    <title>Stampa valori cantieri con opzioni</title>
    <style>
        h1 {
            margin-bottom: 20px;
        }

        .btn.btn-lg {
            min-width: 100px;
        }

        .panel-container {
            margin-top: 20px;
        }

        span.lblIntestazione {
            font-size: 20px;
            font-style: italic;
            color: darkblue;
            float: right;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="container-fluid">

        <!-- Filtri per la stampa -->
        <div class="row">
            <div class="col-md-offset-3 col-md-6">
                <!-- Filtri per scelta cliente -->
                <div class="col-md-2">
                    <asp:Label ID="lblFiltraCliente" runat="server" Text="Cliente"></asp:Label>
                    <asp:TextBox ID="txtFiltraCliente" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnFiltraCantieri" CssClass="btn btn-lg btn-primary" OnClick="btnFiltraCantieri_Click" runat="server" Text="Filtra Cantieri" />
                </div>
                <div class="col-md-12">
                    <asp:Label ID="lblScegliCliente" runat="server" Text="Scegli Cantiere"></asp:Label>
                    <asp:DropDownList ID="ddlScegliCliente" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>

                <!-- Altri filtri -->
                <div class="col-md-2">
                    <asp:Label ID="lblAnno" runat="server" Text="Anno"></asp:Label>
                    <asp:TextBox ID="txtAnno" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblCodCant" runat="server" Text="Codice Cantiere"></asp:Label>
                    <asp:TextBox ID="txtCodCant" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:Label ID="lblChiuso" runat="server" Text="Chiuso"></asp:Label>
                    <asp:CheckBox ID="chkChiuso" runat="server" />
                </div>
                <div class="col-md-1">
                    <asp:Label ID="lblRiscosso" runat="server" Text="Riscosso"></asp:Label>
                    <asp:CheckBox ID="chkRiscosso" runat="server" />
                </div>
                <div class="col-md-1">
                    <asp:Label ID="lblFatturato" runat="server" Text="Fatturato"></asp:Label>
                    <asp:CheckBox ID="chkFatturato" runat="server" />
                </div>

                <!-- Bottone visualizzazione stampa -->
                <div class="col-md-12">
                    <asp:Button ID="btnStampaValoriCantieri" CssClass="btn btn-lg btn-primary pull-right" OnClick="btnStampaContoCliente_Click" runat="server" Text="Stampa Valori Cantieri" />
                </div>
            </div>
        </div>

        <!-- Griglia di visualizzazione -->
        <div class="row">
            <div class="col-md-12 table-container">
                <asp:GridView ID="grdStampaConOpzioni" AutoGenerateColumns="false" ItemType="GestioneCantieri.Data.StampaValoriCantieriConOpzioni" CssClass="table table-striped table-responsive text-center" runat="server">
                    <Columns>
                        <asp:BoundField HeaderText="Codice Cantiere" DataField="CodCant" />
                        <asp:BoundField HeaderText="Descrizione Cantiere" DataField="DescriCodCAnt" />
                        <asp:BoundField HeaderText="Cliente" DataField="RagSocCli" />
                        <%--<asp:BoundField HeaderText="Data Inserimento" DataField="Data" DataFormatString="{0:d}" />--%>
                        <asp:BoundField HeaderText="Totale Conto" DataField="TotaleConto" />
                        <asp:BoundField HeaderText="Totale Acconti" DataField="TotaleAcconti" />
                        <asp:BoundField HeaderText="Totale Finale" DataField="TotaleFinale" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <asp:Label ID="lblTotaleGeneraleStampa" CssClass="lblIntestazione" runat="server"></asp:Label>

        <!-- GridView di appoggio -->
            <asp:GridView ID="grdStampaMateCant" runat="server" ItemType="GestioneCantieri.Data.MaterialiCantieri" AutoGenerateColumns="False" CssClass="table table-striped table-responsive text-center" Visible="false">
                <Columns>
                    <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:d}" ApplyFormatInEditMode="True" />
                    <asp:BoundField DataField="DescriCodArt" HeaderText="Descr. CodArt" />
                    <asp:BoundField DataField="Qta" HeaderText="Qta" />
                    <asp:BoundField DataField="PzzoUniCantiere" HeaderText="Pzzo Unit." DataFormatString="{0:0.00}" />
                    <asp:BoundField DataField="ValoreRicarico" HeaderText="Valore Ricarico" />
                    <asp:BoundField DataField="ValoreRicalcolo" HeaderText="Valore Ricalcolo" />
                    <asp:BoundField DataField="PzzoFinCli" HeaderText="Pzzo Unit Fin Cli" />
                    <asp:BoundField DataField="Valore" HeaderText="Valore" />
                    <asp:BoundField DataField="Visibile" HeaderText="Visibile" />
                    <asp:BoundField DataField="Ricalcolo" HeaderText="Ricalcolo" />
                    <asp:BoundField DataField="RicaricoSiNo" HeaderText="RicaricoSiNo" />
                </Columns>
            </asp:GridView>

            <asp:GridView ID="grdStampaMateCantPDF" runat="server" ItemType="GestioneCantieri.Data.MaterialiCantieri" AutoGenerateColumns="False" CssClass="table table-striped table-responsive text-center" Visible="false">
                <Columns>
                    <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:d}" ApplyFormatInEditMode="True" />
                    <asp:BoundField DataField="DescriCodArt" HeaderText="Descr. CodArt" />
                    <asp:BoundField DataField="Qta" HeaderText="Qta" />
                    <asp:BoundField DataField="PzzoUniCantiere" HeaderText="Pzzo Unit." DataFormatString="{0:0.00}" />
                    <asp:BoundField DataField="Valore" HeaderText="Valore" />
                </Columns>
            </asp:GridView>
    </div>
</asp:Content>
