<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="RicalcoloConti.aspx.cs" Inherits="GestioneCantieri.RicalcoloConti" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    <title>Ricalcolo Conti</title>
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
    <h1>Ricalcolo Conti</h1>

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
                    <asp:Label ID="lblChiuso" runat="server" Text="Chiuso" Style="width: 100%; float: left;"></asp:Label>
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
                    <asp:Label ID="lblScegliTipoNote" runat="server" Text="Scegli Note"></asp:Label>
                    <asp:DropDownList ID="ddlScegliTipoNote" CssClass="form-control" runat="server">
                        <asp:ListItem Value="noNote">Senza Note</asp:ListItem>
                        <asp:ListItem Value="note1">Con Note 1</asp:ListItem>
                        <asp:ListItem Value="note2">Con Note 2</asp:ListItem>
                        <asp:ListItem Value="note1note2">Con Note 1 e Note 2</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-12">
                    <asp:Label ID="lblScegliCantiere" runat="server" Text="Scegli Cantiere"></asp:Label>
                    <asp:DropDownList ID="ddlScegliCant" OnTextChanged="ddlScegliCant_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
                <div class="col-md-12">
                    <asp:Button ID="btnStampaContoCliente" CssClass="btn btn-lg btn-primary pull-right" OnClick="btnStampaContoCliente_Click" runat="server" Text="Stampa Conto Finale Cliente" />
                    <asp:Button ID="btnStampaExcel" CssClass="btn btn-lg btn-primary pull-right" OnClick="btnStampaExcel_Click" runat="server" Text="Stampa Excel" />
                    <asp:Label ID="lblControlloMatVisNasc" runat="server" Text="" Style="font-size: 20px; float: right; margin-right: 10px; position: relative; top: 8px;"></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <asp:GridView ID="grdStampaMateCant" runat="server" ItemType="GestioneCantieri.Data.MaterialiCantieri" AutoGenerateColumns="False" CssClass="table table-striped table-responsive text-center" Visible="true">
        <Columns>
            <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:d}" ApplyFormatInEditMode="True" />
            <asp:BoundField DataField="DescriCodArt" HeaderText="Descr. CodArt" />
            <asp:BoundField DataField="Qta" HeaderText="Qta" />
            <asp:BoundField DataField="PzzoUniCantiere" HeaderText="Pzzo Unit." DataFormatString="{0:0.00}" />
            <asp:BoundField DataField="ValoreRicarico" HeaderText="Valore Ricarico" />
            <asp:BoundField DataField="ValoreRicalcolo" HeaderText="Valore Ricalcolo" />
            <asp:BoundField DataField="PzzoFinCli" HeaderText="Pzzo Fin Cli" />
            <asp:BoundField DataField="Valore" HeaderText="Valore" />
            <asp:BoundField DataField="Visibile" HeaderText="Visibile" />
            <asp:BoundField DataField="Ricalcolo" HeaderText="Ricalcolo" />
            <asp:BoundField DataField="RicaricoSiNo" HeaderText="RicaricoSiNo" />
            <asp:BoundField DataField="Note" HeaderText="Note" />
            <asp:BoundField DataField="Note2" HeaderText="Note2" />
        </Columns>
    </asp:GridView>

    <asp:GridView ID="grdStampaMateCantPDF" runat="server" ItemType="GestioneCantieri.Data.MaterialiCantieri" AutoGenerateColumns="False" CssClass="table table-striped table-responsive text-center" Visible="true">
        <Columns>
            <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:d}" ApplyFormatInEditMode="True" />
            <asp:BoundField DataField="DescriCodArt" HeaderText="Descr. CodArt" />
            <asp:BoundField DataField="Qta" HeaderText="Qta" />
            <asp:BoundField DataField="PzzoUniCantiere" HeaderText="Pzzo Unit." DataFormatString="{0:0.00}" />
            <asp:BoundField DataField="Valore" HeaderText="Valore" />
        </Columns>
    </asp:GridView>

    <asp:GridView ID="grdStampaMateCantExcel" runat="server" ItemType="GestioneCantieri.Data.MaterialiCantieri" AutoGenerateColumns="False" CssClass="table table-striped table-responsive text-center" Visible="true">
        <Columns>
            <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:d}" ApplyFormatInEditMode="True" />
            <asp:BoundField DataField="DescriCodArt" HeaderText="Descr. CodArt" />
            <asp:BoundField DataField="Qta" HeaderText="Qta" />
            <asp:BoundField DataField="PzzoUniCantiere" HeaderText="Pzzo Unit." DataFormatString="{0:0.00}" />
            <asp:BoundField DataField="Valore" HeaderText="Valore" />
            <asp:BoundField DataField="Note" HeaderText="Note" />
            <asp:BoundField DataField="Note2" HeaderText="Note2" />
        </Columns>
    </asp:GridView>
</asp:Content>
