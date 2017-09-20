<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="StampaValoriCantieri.aspx.cs" Inherits="GestioneCantieri.StampaValoriCantieri" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    <title>Stampa Valori Cantieri</title>
    <style>
        span.pull-right {
            position: relative;
            top: 10px;
            right: 10px;
            font-size: 20px;
        }

        input[type="checkbox"] {
            width: 20px;
            height: 20px;
            position: relative;
            left: 0px !important;
        }

        .tableContainer {
            max-height: 500px;
            overflow: hidden;
            overflow-y: scroll;
        }

        span.lblTotali {
            font-size: 30px;
            font-style: italic;
        }

        .btnPagaOperaio {
            position: relative;
            right: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>Valori Cantieri</h1>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-offset-3 col-md-6">
                <div class="col-md-3">
                    <asp:Label ID="lblAnno" runat="server" Text="Anno"></asp:Label>
                    <asp:TextBox ID="txtAnno" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-3">
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
                <div class="col-md-3">
                    <asp:Button ID="btnFiltraCantieri" CssClass="btn btn-lg btn-primary" OnClick="btnFiltraCantieri_Click" runat="server" Text="Filtra Cantieri" />
                </div>
                <div class="col-md-12">
                    <asp:Label ID="lblScegliCantiere" runat="server" Text="Scegli Cantiere"></asp:Label>
                    <asp:DropDownList ID="ddlScegliCant" OnTextChanged="ddlScegliCant_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
                <div class="col-md-12">
                    <asp:Button ID="btnStampaValoriCantieri" CssClass="btn btn-lg btn-primary pull-right" OnClick="btnStampaContoCliente_Click" runat="server" Text="Stampa Valori Cantieri" />
                </div>
            </div>

            <asp:Panel ID="pnlRisultati" runat="server">
                <div class="col-md-offset-3 col-md-6">
                    <div class="col-md-4">
                        <asp:Label ID="lblContoPreventivo" runat="server" Text="Conto/Preventivo"></asp:Label>
                        <asp:TextBox ID="txtContoPreventivo" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblTotPagamenti" runat="server" Text="Totale Acconti"></asp:Label>
                        <asp:TextBox ID="txtTotPagamenti" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblTotFinale" runat="server" Text="Totale Finale"></asp:Label>
                        <asp:TextBox ID="txtTotFinale" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            </asp:Panel>

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
    </div>
</asp:Content>
