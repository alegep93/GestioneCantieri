<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="RientroMateriale.aspx.cs" Inherits="GestioneCantieri.RientroMateriale" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    <title>Gestione Cantieri</title>
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
    <h1>Rientro Materiale</h1>
    <div class="container-fluid">
        <div class="row">
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
                <asp:Panel ID="pnlSubIntestazione" runat="server">
                    <div class="col-md-12">
                        <div class="col-md-4">
                            <asp:Label ID="lblAcquirente" Text="Acquirente" runat="server" />
                            <asp:DropDownList ID="ddlScegliAcquirente" CssClass="form-control" runat="server" />
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblScegliFornit" Text="Fornitore" runat="server" />
                            <asp:DropDownList ID="ddlScegliFornit" CssClass="form-control" runat="server" />
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblTipDatCant" Text="Tipologia" runat="server" />
                            <asp:TextBox ID="txtTipologia" CssClass="form-control" Enabled="false" Text="RIENTRO" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-4">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <asp:Label ID="lblFiltroCodArt" Text="Filtro CodArt" runat="server" />
                                    <asp:TextBox ID="txtFiltroCodArt" AutoPostBack="true" OnTextChanged="txtFiltroCodArt_TextChanged" placeholder="Filtro Cod Art" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblFiltroDescriCodArt" Text="Filtro DescriCodArt" runat="server" />
                                    <asp:TextBox ID="txtFiltroDescriCodArt" AutoPostBack="true" OnTextChanged="txtFiltroDescriCodArt_TextChanged" placeholder="Filtro DescriCodArt" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <asp:Label ID="lblScegliMatCant" Text="Scegli Materiale Cantiere" runat="server" />
                                <asp:DropDownList ID="ddlScegliMatCant" AppendDataBoundItems="true" AutoPostBack="true" OnTextChanged="ddlScegliMatCant_TextChanged" CssClass="form-control" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="col-md-12">
                                <asp:Label ID="lblDataDDT" Text="Data DDT" runat="server" />
                                <asp:TextBox ID="txtDataDDT" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-12">
                                <asp:Label ID="lblNumBolla" Text="Numero Bolla" runat="server" />
                                <asp:TextBox ID="txtNumBolla" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="col-md-12">
                                <asp:Label ID="lblFascia" Text="Fascia" runat="server" />
                                <asp:TextBox ID="txtFascia" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-12">
                                <asp:Label ID="lblProtocollo" Text="Protocollo" runat="server" />
                                <asp:TextBox ID="txtProtocollo" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                                
                <asp:Panel ID="pnlRientroMatCant" runat="server">
                    <div class="row">
                        <div class="col-md-offset-3 col-md-8">
                            <div class="col-md-4">
                                <asp:Label ID="lblFiltroCod_FSS" Text="Filtro Cod_FSS" runat="server" />
                                <asp:TextBox ID="txtFiltroCodFSS" placeholder="Filtro Cod_FSS" AutoPostBack="true" OnTextChanged="txtFiltroCodFSS_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:Label ID="lblFiltroAA_Des" Text="Filtro AA_DES" runat="server" />
                                <asp:TextBox ID="txtFiltroAA_Des" placeholder="Filtro AA_DES" AutoPostBack="true" OnTextChanged="txtFiltroAA_Des_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-offset-2 col-md-8">
                            <asp:Label ID="lblScegliListino" Text="Scegli Listino" runat="server" />
                            <asp:DropDownList ID="ddlScegliListino" AutoPostBack="true" OnTextChanged="ddlScegliListino_TextChanged" CssClass="form-control" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <asp:Label ID="lblCodArt" Text="Codice Articolo" runat="server" />
                            <asp:TextBox ID="txtCodArt" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblDescriCodArt" Text="Descrizione Codice Articolo" runat="server" />
                            <asp:TextBox ID="txtDescriCodArt" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblDescrMat" Text="Descrizione Materiale" runat="server" />
                            <asp:TextBox ID="txtDescrMat" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblNote" Text="Note" runat="server" />
                            <asp:TextBox ID="txtNote" TextMode="MultiLine" Rows="5" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblQta" Text="Quantità" runat="server" />
                            <asp:TextBox ID="txtQta" CssClass="form-control" Text="0" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblPzzoNettoMef" Text="Prezzo Netto Mef" runat="server" />
                            <asp:TextBox ID="txtPzzoNettoMef" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblPzzoUnit" Text="Prezzo Unitario" runat="server" />
                            <asp:TextBox ID="txtPzzoUnit" Text="0.00" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblPzzoFinCli" Text="Prezzo Finale Cliente" runat="server" />
                            <asp:TextBox ID="txtPzzoFinCli" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div class="col-md-1">
                            <asp:Label ID="lblVisibile" Text="Visibile" runat="server" />
                            <asp:CheckBox ID="chkVisibile" CssClass="form-control" Checked="true" runat="server" />
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblRicalcolo" Text="Ricalcolo" runat="server" />
                            <asp:CheckBox ID="chkRicalcolo" CssClass="form-control" Checked="true" runat="server" />
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblRicarico" Text="Ricarico Si/No" runat="server" />
                            <asp:CheckBox ID="chkRicarico" CssClass="form-control" Checked="true" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Button ID="btnCalcolaPrezzoUnit" OnClick="btnCalcolaPrezzoUnit_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Calcola Prezzo Unitario" />
                        </div>
                        <div class="col-md-6">
                            <asp:Button ID="btnInserisci" OnClick="btnInserisci_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Record" />
                            <asp:Label ID="lblIsRecordInserito" Text="" CssClass="pull-right" runat="server" />
                        </div>
                    </div>
                </asp:Panel>
            </asp:Panel>
        </div>
    </div>
</asp:Content>

