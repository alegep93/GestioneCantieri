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

            <!-- Maschera gestione materiali cantieri -->
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
                        <asp:Button ID="btnInserisci" OnClick="btnInserisci_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Record" />
                        <asp:Label ID="lblIsRecordInserito" Text="" CssClass="pull-right" runat="server" />
                    </div>
                </div>
            </asp:Panel>

            <!-- Maschera di rientro materiali -->
            <asp:Panel ID="pnlRientroMatCant" CssClass="col-md-12" runat="server">
                <div class="row">
                    <div class="col-md-offset-3 col-md-8">
                        <div class="col-md-4">
                            <asp:Label ID="Label1" Text="Filtro Cod_FSS" runat="server" />
                            <asp:TextBox ID="TextBox1" placeholder="Filtro Cod_FSS" AutoPostBack="true" OnTextChanged="txtFiltroCodFSS_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="Label2" Text="Filtro AA_DES" runat="server" />
                            <asp:TextBox ID="TextBox2" placeholder="Filtro AA_DES" AutoPostBack="true" OnTextChanged="txtFiltroAA_Des_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-offset-2 col-md-8">
                        <asp:Label ID="Label3" Text="Scegli Listino" runat="server" />
                        <asp:DropDownList ID="DropDownList1" AutoPostBack="true" OnTextChanged="ddlScegliListino_TextChanged" CssClass="form-control" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <asp:Label ID="Label4" Text="Codice Articolo" runat="server" />
                        <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label5" Text="Descrizione Codice Articolo" runat="server" />
                        <asp:TextBox ID="TextBox4" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label6" Text="Descrizione Materiale" runat="server" />
                        <asp:TextBox ID="TextBox5" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label7" Text="Note" runat="server" />
                        <asp:TextBox ID="TextBox6" TextMode="MultiLine" Rows="5" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="Label8" Text="Quantità" runat="server" />
                        <asp:TextBox ID="TextBox7" CssClass="form-control" Text="0" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label9" Text="Prezzo Netto Mef" runat="server" />
                        <asp:TextBox ID="TextBox8" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label10" Text="Prezzo Unitario" runat="server" />
                        <asp:TextBox ID="TextBox9" Text="0.00" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label11" Text="Prezzo Finale Cliente" runat="server" />
                        <asp:TextBox ID="TextBox10" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-md-1">
                        <asp:Label ID="Label12" Text="Visibile" runat="server" />
                        <asp:CheckBox ID="CheckBox1" CssClass="form-control" Checked="true" runat="server" />
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="Label13" Text="Ricalcolo" runat="server" />
                        <asp:CheckBox ID="CheckBox2" CssClass="form-control" Checked="true" runat="server" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label14" Text="Ricarico Si/No" runat="server" />
                        <asp:CheckBox ID="CheckBox3" CssClass="form-control" Checked="true" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <asp:Button ID="Button1" OnClick="btnCalcolaPrezzoUnit_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Calcola Prezzo Unitario" />
                    </div>
                    <div class="col-md-6">
                        <asp:Button ID="Button2" OnClick="btnInserisci_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Record" />
                        <asp:Label ID="Label15" Text="" CssClass="pull-right" runat="server" />
                    </div>
                </div>
            </asp:Panel>

            <!-- Maschera manodopera -->
            <asp:Panel ID="pnlManodopera" CssClass="col-md-12" runat="server">
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="Label16" Text="Quantità" runat="server" />
                        <asp:TextBox ID="TextBox11" CssClass="form-control" Text="0" runat="server"></asp:TextBox>
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
                        <asp:Label ID="Label17" Text="Visibile" runat="server" />
                        <asp:CheckBox ID="CheckBox4" CssClass="form-control" Checked="true" runat="server" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label18" Text="Ricalcolo" runat="server" />
                        <asp:CheckBox ID="CheckBox5" CssClass="form-control" Enabled="false" runat="server" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label19" Text="Ricarico Si/No" runat="server" />
                        <asp:CheckBox ID="CheckBox6" CssClass="form-control" Enabled="false" runat="server" />
                    </div>
                    <div class="col-md-6">
                        <asp:Button ID="Button3" OnClick="btnInserisci_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Record" />
                        <asp:Label ID="lblIsManodopInserita" Text="" CssClass="pull-right" runat="server" />
                    </div>
                </div>
            </asp:Panel>

            <!-- Maschera Gestione Operaio -->
            <asp:Panel ID="pnlGestioneOperaio" CssClass="col-md-12" runat="server">
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="Label20" Text="Quantità" runat="server" />
                        <asp:TextBox ID="TextBox12" CssClass="form-control" Text="0" runat="server"></asp:TextBox>
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
                        <asp:Label ID="Label21" Text="Note 1" runat="server" />
                        <asp:TextBox ID="TextBox13" TextMode="MultiLine" Rows="5" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="Label22" Text="Note 2" runat="server" />
                        <asp:TextBox ID="TextBox14" TextMode="MultiLine" Rows="5" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="Label23" Text="Visibile" runat="server" />
                        <asp:CheckBox ID="CheckBox7" CssClass="form-control" Checked="true" runat="server" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label24" Text="Ricalcolo" runat="server" />
                        <asp:CheckBox ID="CheckBox8" CssClass="form-control" Enabled="false" runat="server" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label25" Text="Ricarico Si/No" runat="server" />
                        <asp:CheckBox ID="CheckBox9" CssClass="form-control" Checked="true" runat="server" />
                    </div>
                    <div class="col-md-6">
                        <asp:Button ID="Button4" OnClick="btnInserisci_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Record" />
                        <asp:Label ID="lblIsOperInserita" Text="" CssClass="pull-right" runat="server" />
                    </div>
                </div>
            </asp:Panel>

            <!-- Maschera Gestione Arrotondamenti -->
            <asp:Panel ID="pnlGestArrotond" CssClass="col-md-12" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <asp:Label ID="Label26" Text="Codice Articolo" runat="server" />
                            <asp:TextBox ID="TextBox15" CssClass="form-control" runat="server" Text="Arrotondamento"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="Label27" Text="Descrizione Codice Articolo" runat="server" />
                            <asp:TextBox ID="TextBox16" CssClass="form-control" runat="server" Text="Arrotondamento"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <asp:Label ID="Label28" Text="Quantità" runat="server" />
                            <asp:TextBox ID="TextBox17" CssClass="form-control" Text="0" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="Label29" Text="Prezzo Unitario" runat="server" />
                            <asp:TextBox ID="TextBox18" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="Label30" Text="Visibile" runat="server" />
                            <asp:CheckBox ID="CheckBox10" CssClass="form-control" Enabled="false" runat="server" />
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label31" Text="Ricalcolo" runat="server" />
                            <asp:CheckBox ID="CheckBox11" CssClass="form-control" Enabled="false" runat="server" />
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label32" Text="Ricarico Si/No" runat="server" />
                            <asp:CheckBox ID="CheckBox12" CssClass="form-control" Enabled="false" runat="server" />
                        </div>
                        <div class="col-md-6">
                            <asp:Button ID="Button5" OnClick="btnInserisci_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Record" />
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
                            <asp:Button ID="Button6" OnClick="btnInserisci_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Record" />
                            <asp:Label ID="lblIsPagamInserito" Text="" CssClass="pull-right" runat="server" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
