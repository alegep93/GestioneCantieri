<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="StampaPerTipologia.aspx.cs" Inherits="GestioneCantieri.StampaPerTipologia" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    <title>Stampa Verifica Cantiere</title>
    <style>
        span.pull-right {
            position: relative;
            top: 10px;
            right: 10px;
            font-size: 20px;
        }

        input[type="radio"] {
            width: 20px;
            height: 20px;
            position: relative;
            left: 0px !important;
        }

        span.lblIntestazione {
            font-size: 30px;
            font-style: italic;
            color: darkblue;
            margin-right: 18px;
        }

        .tableContainer {
            max-height: 500px;
            overflow: hidden;
            overflow-y: scroll;
            margin-top: 20px;
        }

        td[colspan="6"] {
            font-weight: bold;
            text-align: left;
        }
    </style>
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>Stampa Per Tipologia</h1>
    <div class="container-fluid">
        <div class="row">
            <!-- Stampa Per Cantiere -->
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

                <!-- Stampa Per Data -->
                <div class="col-md-offset-3 col-md-3">
                    <asp:Label ID="lblDataDa" runat="server" Text="Data Da:"></asp:Label>
                    <asp:TextBox ID="txtDataDa" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblDataA" runat="server" Text="Data A:"></asp:Label>
                    <asp:TextBox ID="txtDataA" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <!-- Scelta Operaio -->
                <div class="col-md-12">
                    <asp:Label ID="lblScegliOperaio" runat="server" Text="Scegli Operaio"></asp:Label>
                    <asp:DropDownList ID="ddlScegliOperaio" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>

                <!-- Tipologie -->
                <div class="col-md-6 text-center">
                    <asp:Label ID="lblManodop" runat="server" Text="Manodopera"></asp:Label>
                    <asp:RadioButton ID="rdbManodop" GroupName="rdbTipol" Checked="true" runat="server" />
                </div>
                <div class="col-md-6 text-center">
                    <asp:Label ID="lblOperaio" runat="server" Text="Operaio"></asp:Label>
                    <asp:RadioButton ID="rdbOper" GroupName="rdbTipol" runat="server" />
                </div>

                <!-- Bottone di stampa -->
                <div class="col-md-12">
                    <asp:Button ID="btnStampaPerTipologia" CssClass="btn btn-lg btn-primary pull-right" OnClick="btnStampaVerificaCant_Click" runat="server" Text="Stampa Tipologia" />
                </div>
            </div>
        </div>

        <div class="tableContainer col-md-12 table-responsive">
            <asp:GridView ID="grdStampaPerTipologia" runat="server" ItemType="GestioneCantieri.Data.MaterialiCantieri" AutoGenerateColumns="False" CssClass="table table-striped text-center">
                <Columns>
                    <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:d}" ApplyFormatInEditMode="True" />
                    <asp:BoundField DataField="CodCant" HeaderText="Codice Cantiere" />
                    <asp:BoundField DataField="DescriCodCant" HeaderText="Descrizione CodCant" />
                    <asp:BoundField DataField="RagSocCli" HeaderText="Ragione Soc. Cliente" />
                    <asp:BoundField DataField="Qta" HeaderText="Qta" />
                    <asp:BoundField DataField="PzzoUniCantiere" HeaderText="Pzzo Unit. Cantiere" DataFormatString="{0:0.00}" />
                    <asp:BoundField DataField="Acquirente" HeaderText="Operaio" />
                </Columns>
            </asp:GridView>
        </div>

        <div class="col-md-12 text-right">
            <asp:Label ID="lblTotOre" CssClass="lblIntestazione" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblTotale" CssClass="lblIntestazione" runat="server" Text=""></asp:Label>
        </div>
    </div>
</asp:Content>
