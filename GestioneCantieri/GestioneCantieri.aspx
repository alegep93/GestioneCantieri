<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="GestioneCantieri.aspx.cs" Inherits="GestioneCantieri.GestioneCantieri" %>

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

        span.pull-right {
            position: relative;
            top: 13px;
            right: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>Matieriali Di Cantiere</h1>
    <div class="container-fluid">
        <div class="row">
            <!-- Intestazione -->
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
                            <asp:TextBox ID="txtTipDatCant" CssClass="form-control" Enabled="false" Text="MATERIALE" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-4">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <asp:Label ID="lblFiltroAnnoDDT" Text="Filtro Anno DDT" runat="server" />
                                    <asp:TextBox ID="txtFiltroAnnoDDT" AutoPostBack="true" OnTextChanged="txtFiltroAnnoDDT_TextChanged" placeholder="Filtro Anno DDT" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblFiltroN_DDT" Text="Filtro N_DDT" runat="server" />
                                    <asp:TextBox ID="txtFiltroN_DDT" AutoPostBack="true" OnTextChanged="txtFiltroN_DDT_TextChanged" placeholder="Filtro N_DDT" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <asp:Label ID="lblScegliDDTMef" Text="Scegli DDT" runat="server" />
                                <asp:DropDownList ID="ddlScegliDDTMef" AutoPostBack="true" OnTextChanged="ddlScegliDDTMef_TextChanged" CssClass="form-control" runat="server" />
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

                    <!-- Pulsantiera per scelta maschera -->
                    <asp:Panel ID="pnlScegliMaschera" runat="server">
                        <div class="col-md-offset-2 col-md-8 text-center" style="padding: 20px 0;">
                            <asp:Button ID="btnMatCant" runat="server" OnClick="btnMatCant_Click" CssClass="btn btn-lg btn-default" Text="Matieriali Cantieri" />
                            <asp:Button ID="btnRientro" runat="server" OnClick="btnRientro_Click" CssClass="btn btn-lg btn-default" Text="Rientro Matieriali" />
                            <asp:Button ID="btnManodop" runat="server" OnClick="btnManodop_Click" CssClass="btn btn-lg btn-default" Text="Manodopera" />
                            <asp:Button ID="btnGestOper" runat="server" OnClick="btnGestOper_Click" CssClass="btn btn-lg btn-default" Text="Gest. Operaio" />
                            <asp:Button ID="btnGestArrot" runat="server" OnClick="btnGestArrot_Click" CssClass="btn btn-lg btn-default" Text="Gest. Arrotond." />
                            <asp:Button ID="btnGestPagam" runat="server" OnClick="btnGestPagam_Click" CssClass="btn btn-lg btn-default" Text="Gest. Pagamenti" />
                        </div>
                    </asp:Panel>
                </asp:Panel>
            </asp:Panel>

            <div class="col-md-12 text-center">
                <h2>
                    <asp:Label ID="lblTitoloMaschera" runat="server" Text=""></asp:Label>
                </h2>
            </div>

            <!-- Maschera gestione materiali cantieri e Rientro -->
            <asp:Panel ID="pnlMascheraGestCant" CssClass="col-md-12" runat="server">
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
                    <div class="col-md-offset-2 col-md-8 matCantDdl">
                        <asp:Label ID="lblScegliListino" Text="Scegli Listino" runat="server" />
                        <asp:DropDownList ID="ddlScegliListino" AutoPostBack="true" OnTextChanged="ddlScegliListino_TextChanged" CssClass="form-control" runat="server" />
                    </div>

                    <div class="col-md-offset-2 col-md-8 rientroDdl">
                        <asp:Label ID="lblScegliMatCant" Text="Scegli Materiale Cantiere" runat="server" />
                        <asp:DropDownList ID="ddlScegliMatCant" AutoPostBack="true" OnTextChanged="ddlScegliMatCant_TextChanged" CssClass="form-control" runat="server" />
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
                        <asp:TextBox ID="txtQta" Text="0" CssClass="form-control" runat="server"></asp:TextBox>
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
                        <asp:Button ID="btnInserisciMatCant" OnClick="btnInserisciMatCant_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Mat Cant" />
                        <asp:Button ID="btnInserisciRientro" OnClick="btnInserisciRientro_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Rientro" />
                        <asp:Label ID="lblIsRecordInserito" Text="" CssClass="pull-right" runat="server" />
                    </div>
                </div>
            </asp:Panel>

            
            <!-- Maschera manodopera -->
            <asp:Panel ID="pnlManodopera" CssClass="col-md-12" runat="server">
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblManodopQta" Text="Quantità" runat="server" />
                        <asp:TextBox ID="txtManodopQta" CssClass="form-control" Text="0" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblPzzoManodop" Text="Prezzo Manodopera" runat="server" />
                        <asp:TextBox ID="txtPzzoManodop" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblDescrManodop" Text="Descrizione Manodopera" runat="server" />
                        <asp:TextBox ID="txtDescrManodop" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <asp:Label ID="lblNote1" Text="Note 1" runat="server" />
                        <asp:TextBox ID="txtNote1" TextMode="MultiLine" Rows="5" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="lblNote2" Text="Note 2" runat="server" />
                        <asp:TextBox ID="txtNote2" TextMode="MultiLine" Rows="5" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblManodopVisibile" Text="Visibile" runat="server" />
                        <asp:CheckBox ID="chkManodopVisibile" CssClass="form-control" Checked="true" runat="server" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblManodopRicalcolo" Text="Ricalcolo" runat="server" />
                        <asp:CheckBox ID="chkManodopRicalcolo" CssClass="form-control" Enabled="false" runat="server" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblManodopRicaricoSiNo" Text="Ricarico Si/No" runat="server" />
                        <asp:CheckBox ID="chkManodopRicaricoSiNo" CssClass="form-control" Enabled="false" runat="server" />
                    </div>
                    <div class="col-md-6">
                        <asp:Button ID="btnInsManodop" OnClick="btnInsManodop_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Manodopera" />
                        <asp:Label ID="lblIsManodopInserita" Text="" CssClass="pull-right" runat="server" />
                    </div>
                </div>
            </asp:Panel>

            <!-- Maschera Gestione Operaio -->
            <asp:Panel ID="pnlGestioneOperaio" CssClass="col-md-12" runat="server">
                <div class="row">
                    <div class="col-md-offset-3 col-md-6">
                        <asp:Label ID="lblScegliOperaio" Text="Scegli Operaio" runat="server" />
                        <asp:DropDownList ID="ddlScegliOperaio" CssClass="form-control" AutoPostBack="true" OnTextChanged="ddlScegliOperaio_TextChanged" runat="server"></asp:DropDownList>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblOperQta" Text="Quantità" runat="server" />
                        <asp:TextBox ID="txtOperQta" CssClass="form-control" Text="0" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblPzzoOper" Text="Prezzo Operaio" runat="server" />
                        <asp:TextBox ID="txtPzzoOper" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblDescrOper" Text="Descrizione Operaio" runat="server" />
                        <asp:TextBox ID="txtDescrOper" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <asp:Label ID="lblOperNote1" Text="Note 1" runat="server" />
                        <asp:TextBox ID="txtOperNote1" TextMode="MultiLine" Rows="5" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="lblOperNote2" Text="Note 2" runat="server" />
                        <asp:TextBox ID="txtOperNote2" TextMode="MultiLine" Rows="5" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblOperVisibile" Text="Visibile" runat="server" />
                        <asp:CheckBox ID="chkOperVisibile" CssClass="form-control" Checked="true" runat="server" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblOperRicalcolo" Text="Ricalcolo" runat="server" />
                        <asp:CheckBox ID="chkOperRicalcolo" CssClass="form-control" Enabled="false" Checked="false" runat="server" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblOperRicaricoSiNo" Text="Ricarico Si/No" runat="server" />
                        <asp:CheckBox ID="chkOperRicaricoSiNo" CssClass="form-control" Checked="true" runat="server" />
                    </div>
                    <div class="col-md-6">
                        <asp:Button ID="btnInsOper" OnClick="btnInsOper_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Operaio" />
                        <asp:Label ID="lblIsOperInserita" Text="" CssClass="pull-right" runat="server" />
                    </div>
                </div>
            </asp:Panel>

            <!-- Maschera Gestione Arrotondamenti -->
            <asp:Panel ID="pnlGestArrotond" CssClass="col-md-12" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <asp:Label ID="lblArrotCodArt" Text="Codice Articolo" runat="server" />
                            <asp:TextBox ID="txtArrotCodArt" CssClass="form-control" runat="server" Text="Arrotondamento"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblArrotDescriCodArt" Text="Descrizione Codice Articolo" runat="server" />
                            <asp:TextBox ID="txtArrotDescriCodArt" CssClass="form-control" runat="server" Text="Arrotondamento"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <asp:Label ID="lblArrotQta" Text="Quantità" runat="server" />
                            <asp:TextBox ID="txtArrotQta" CssClass="form-control" Text="0" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblArrotPzzoUnit" Text="Prezzo Unitario" runat="server" />
                            <asp:TextBox ID="txtArrotPzzoUnit" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="lblArrotVisibile" Text="Visibile" runat="server" />
                            <asp:CheckBox ID="chkArrotVisibile" CssClass="form-control" Enabled="false" runat="server" />
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblArrotRicalcolo" Text="Ricalcolo" runat="server" />
                            <asp:CheckBox ID="chkArrotRicalcolo" CssClass="form-control" Enabled="false" runat="server" />
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblArrotRicaricoSiNo" Text="Ricarico Si/No" runat="server" />
                            <asp:CheckBox ID="chkArrotRicaricoSiNo" CssClass="form-control" Enabled="false" runat="server" />
                        </div>
                        <div class="col-md-6">
                            <asp:Button ID="btnInsArrot" OnClick="btnInsArrot_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Arrotondamento" />
                            <asp:Label ID="lblIsArrotondInserito" Text="" CssClass="pull-right" runat="server" />
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <!-- Maschera Gestione Pagamenti-->
            <asp:Panel ID="pnlGestPagam" CssClass="col-md-12" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <asp:Label ID="lblImportoPagam" Text="Importo" runat="server" />
                            <asp:TextBox ID="txtImportoPagam" CssClass="form-control" runat="server" Text=""></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblDescrPagam" Text="Descrizione" runat="server" />
                            <asp:TextBox ID="txtDescrPagam" CssClass="form-control" runat="server" Text=""></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="lblAcconto" Text="Acconto" runat="server" />
                            <asp:CheckBox ID="chkAcconto" CssClass="form-control" Enabled="false" runat="server" />
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblSaldo" Text="Saldo" runat="server" />
                            <asp:CheckBox ID="chkSaldo" CssClass="form-control" Enabled="false" runat="server" />
                        </div>
                        <div class="col-md-8">
                            <asp:Button ID="btnInsPagam" OnClick="btnInsPagam_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Pagamento" />
                            <asp:Label ID="lblIsPagamInserito" Text="" CssClass="pull-right" runat="server" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
