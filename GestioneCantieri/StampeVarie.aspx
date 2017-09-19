<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="StampeVarie.aspx.cs" Inherits="GestioneCantieri.StampeVarie" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    <title>Stampe Varie</title>
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
    </style>
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

        <asp:Panel ID="pnlCampiStampaDDT_MatCant" runat="server">
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
                <asp:Button ID="btnStampaDDT" CssClass="btn btn-lg btn-primary pull-right" OnClick="btnStampaDDT_Click" runat="server" Text="Stampa DDT" />
                <asp:Button ID="btnStampaMatCant" CssClass="btn btn-lg btn-primary pull-right" OnClick="btnStampaMatCant_Click" runat="server" Text="Stampa Mat Cant" />
                <asp:Button ID="btnAggiungiNumPagine" CssClass="btn btn-lg btn-info pull-right" OnClick="btnAggiungiNumPagine_Click" runat="server" Text="Aggiungi Num. Pagine" />
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlCampiStampaCliente" runat="server">
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
                <div class="col-md-3">
                    <asp:Button ID="btnFiltraCantieri" CssClass="btn btn-lg btn-primary" OnClick="btnFiltraCantieri_Click" runat="server" Text="Filtra Cantieri" />
                </div>
                <div class="col-md-12">
                    <asp:Label ID="lblScegliCantiere" runat="server" Text="Scegli Cantiere"></asp:Label>
                    <asp:DropDownList ID="ddlScegliCant" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
            </div>
            <div class="col-md-offset-3 col-md-6">
                <asp:Label ID="lblNomeFileStampaCliente" runat="server" Text="Nome File"></asp:Label>
                <asp:TextBox ID="txtNomeFileStampaCliente" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:Button ID="btnStampaCliente" CssClass="btn btn-lg btn-primary pull-right" OnClick="btnStampaCliente_Click" runat="server" Text="Stampa Materiale Cliente" />
            </div>
        </asp:Panel>

        <asp:Label ID="lblIsNomeFileInserito" runat="server" CssClass="pull-right" Text=""></asp:Label>

        <asp:GridView ID="grdStampaDDT" runat="server" ItemType="GestioneCantieri.Data.DDTMef" AutoGenerateColumns="False" CssClass="table table-striped table-responsive text-center" Visible="true">
            <Columns>
                <asp:BoundField DataField="N_ddt" HeaderText="N_DDT" />
                <asp:BoundField DataField="CodArt" HeaderText="Codice Articolo" />
                <asp:BoundField DataField="DescriCodArt" HeaderText="Descrizione Cod. Art." />
                <asp:BoundField DataField="Qta" HeaderText="Quantità" />
                <asp:BoundField DataField="PrezzoUnitario" HeaderText="Prezzo Unit." />
                <asp:BoundField DataField="Valore" HeaderText="Valore" />
            </Columns>
        </asp:GridView>

        <asp:GridView ID="grdStampaMateCant" runat="server" ItemType="GestioneCantieri.Data.MaterialiCantieri" AutoGenerateColumns="False" CssClass="table table-striped table-responsive text-center" Visible="false">
            <Columns>
                <asp:BoundField DataField="NumeroBolla" HeaderText="Num. Bolla" />
                <asp:BoundField DataField="Fornitore" HeaderText="Fornit." />
                <asp:BoundField DataField="CodCant" HeaderText="CodCant" />
                <asp:BoundField DataField="Acquirente" HeaderText="Acquirente" />
                <asp:BoundField DataField="CodArt" HeaderText="CodArt" />
                <asp:BoundField DataField="DescriCodArt" HeaderText="Descr. CodArt" />
                <asp:BoundField DataField="Qta" HeaderText="Qta" />
                <asp:BoundField DataField="PzzoUniCantiere" HeaderText="Pzzo Unit." />
                <asp:BoundField DataField="Valore" HeaderText="Valore" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
