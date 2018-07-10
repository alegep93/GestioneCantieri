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

        input.btn.btn-lg.btn-primary.pull-right,
        #body_pnlFiltriMatCant input.btn {
            position: relative;
            top: 10px;
        }

        span.pull-right {
            position: relative;
            top: 13px;
            right: 10px;
        }

        .tableContainer {
            max-height: 500px;
            overflow: hidden;
            overflow-y: scroll;
        }

        select, input {
            font-size: 20px !important;
            min-height: 40px;
        }

        h3 {
            margin-top: 0 !important;
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
                    <div class="col-md-1">
                        <asp:Label ID="lblFiltroCantChiuso" Text="Chiuso" runat="server" />
                        <asp:CheckBox ID="chkFiltroCantChiuso" CssClass="form-control" Checked="false" runat="server" />
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="lblFiltroCantRiscosso" Text="Riscosso" runat="server" />
                        <asp:CheckBox ID="chkFiltroCantRiscosso" CssClass="form-control" Checked="false" runat="server" />
                    </div>
                    <div class="col-md-4">
                        <asp:Button ID="btnFiltroCant" CssClass="btn btn-lg btn-primary pull-left" OnClick="btnFiltroCant_Click" runat="server" Text="Filtra" Style="margin-right: 10px;" />
                        <asp:Button ID="btnSvuotaIntestazione" CssClass="btn btn-lg btn-primary pull-left" OnClick="btnSvuotaIntestazione_Click" runat="server" Text="Svuota Intestazione" />
                    </div>
                </asp:Panel>
                <div class="col-md-offset-2 col-md-8">
                    <asp:Label ID="lblScegliCant" Text="Scegli Cantiere" runat="server" />
                    <asp:DropDownList ID="ddlScegliCant" CssClass="form-control" AutoPostBack="true" OnTextChanged="ddlScegliCant_TextChanged" runat="server" />
                </div>
                <asp:Panel ID="pnlSubIntestazione" runat="server">
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
                            <div class="col-md-6">
                                <asp:Label ID="lblProtocollo" Text="Protocollo" runat="server" />
                                <asp:TextBox ID="txtProtocollo" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <asp:Button ID="btnGenetaNumBolla" CssClass="btn btn-lg btn-primary pull-right" OnClick="btnGenetaNumBolla_Click" runat="server" Text="Genera num. bolla" />
                                <asp:Label ID="lblErroreGeneraNumBolla" Text="" CssClass="pull-right" runat="server" Style="position: relative; top: 10px; right: 10px;" />
                            </div>
                        </div>
                    </div>
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
                            <asp:TextBox ID="txtTipDatCant" CssClass="form-control" Text="MATERIALE" Enabled="false" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Pulsantiera per scelta maschera -->
                    <asp:Panel ID="pnlScegliMaschera" runat="server">
                        <div class="col-md-offset-1 col-md-10 text-center" style="padding: 20px 0;">
                            <asp:Button ID="btnMatCantFromDDT" runat="server" OnClick="btnMatCantFromDDT_Click" CssClass="btn btn-lg btn-default" Text="Materiali da DDT" />
                            <asp:Button ID="btnMatCant" runat="server" OnClick="btnMatCant_Click" CssClass="btn btn-lg btn-default" Text="Materiali Cantieri" />
                            <asp:Button ID="btnRientro" runat="server" OnClick="btnRientro_Click" CssClass="btn btn-lg btn-default" Text="Rientro Materiali" />
                            <asp:Button ID="btnManodop" runat="server" OnClick="btnManodop_Click" CssClass="btn btn-lg btn-default" Text="Manodopera" />
                            <asp:Button ID="btnGestOper" runat="server" OnClick="btnGestOper_Click" CssClass="btn btn-lg btn-default" Text="Gest. Operaio" />
                            <asp:Button ID="btnGestArrot" runat="server" OnClick="btnGestArrot_Click" CssClass="btn btn-lg btn-default" Text="Gest. Arrotond." />
                            <asp:Button ID="btnGestChiam" runat="server" OnClick="btnGestChiam_Click" CssClass="btn btn-lg btn-default" Text="Gest. A Chiamata." />
                            <asp:Button ID="btnGestSpese" runat="server" OnClick="btnGestSpese_Click" CssClass="btn btn-lg btn-default" Text="Gest. Spese" />
                        </div>
                    </asp:Panel>
                </asp:Panel>
            </asp:Panel>

            <div class="col-md-12 text-center">
                <h3>
                    <asp:Label ID="lblTitoloMaschera" runat="server" Text=""></asp:Label>
                </h3>
            </div>

            <!-- Maschera gestione materiali da DDT -->
            <asp:Panel ID="pnlMascheraMaterialiDaDDT" CssClass="col-md-12" runat="server">
                <div class="row">
                    <div class="col-md-offset-2 col-md-8">
                        <asp:Button ID="btnSelezionaTuttoTOP" CssClass="btn btn-lg btn-primary pull-left" OnClick="btnSelezionaTutto_Click" Text="Seleziona Tutto" runat="server" />
                        <asp:Button ID="btnDeselezionaTuttoTOP" CssClass="btn btn-lg btn-primary pull-right" OnClick="btnDeselezionaTutto_Click" Text="Deseleziona Tutto" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <asp:GridView ID="grdMostraDDTDaInserire" ItemType="GestioneCantieri.Data.DDTMef" AutoGenerateColumns="false" CssClass="table table-striped text-center" runat="server">
                        <Columns>
                            <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:d}" ApplyFormatInEditMode="True" />
                            <asp:BoundField DataField="N_ddt" HeaderText="N_DDT" />
                            <asp:BoundField DataField="CodArt" HeaderText="Codice Articolo" />
                            <asp:BoundField DataField="DescriCodArt" HeaderText="Descrizione Codice Articolo" />
                            <asp:BoundField DataField="Qta" HeaderText="Quantità" />
                            <asp:BoundField DataField="PrezzoUnitario" HeaderText="Prezzo Unitario" DataFormatString="{0:0.00}" />
                            <asp:BoundField DataField="Acquirente" HeaderText="Acquirente" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkDDTSelezionato" Checked="<%# BindItem.DaInserire %>" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="row">
                    <asp:Button ID="btnSelezionaTuttoBOTTOM" CssClass="btn btn-lg btn-primary pull-left" OnClick="btnSelezionaTutto_Click" Text="Seleziona Tutto" style="margin-right: 10px;" runat="server" />
                    <asp:Button ID="btnDeselezionaTuttoBOTTOM" CssClass="btn btn-lg btn-primary pull-left" OnClick="btnDeselezionaTutto_Click" Text="Deseleziona Tutto" runat="server" />
                    <asp:Button ID="btnInsMatDaDDT" OnClick="btnInsMatDaDDT_Click" CssClass="btn btn-lg btn-primary pull-right" Text="Inserisci Materiali" runat="server" /><br />
                    <asp:Label ID="lblInsMatDaDDT" CssClass="pull-right" runat="server"></asp:Label>
                </div>
            </asp:Panel>

            <!-- Maschera gestione materiali cantieri e Rientro -->
            <asp:Panel ID="pnlMascheraGestCant" CssClass="col-md-12" runat="server">
                <div class="row">
                    <div class="col-md-offset-1 col-md-10">
                        <div class="col-md-3">
                            <asp:Label ID="lblFiltroCod_FSS" Text="Filtro Codice Articolo" runat="server" />
                            <asp:TextBox ID="txtFiltroCodFSS" placeholder="Filtro Codice Articolo" AutoPostBack="true" OnTextChanged="txtFiltroCodFSS_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblFiltroAA_Des" Text="Filtro Descrizione Articolo" runat="server" />
                            <asp:TextBox ID="txtFiltroAA_Des" placeholder="Filtro Descrizione Articolo" AutoPostBack="true" OnTextChanged="txtFiltroAA_Des_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-6 matCantDdl">
                            <asp:Label ID="lblScegliListino" Text="Scegli Listino" runat="server" />
                            <asp:DropDownList ID="ddlScegliListino" AutoPostBack="true" OnTextChanged="ddlScegliListino_TextChanged" CssClass="form-control" runat="server" />
                        </div>
                    </div>

                    <div class="col-md-offset-1 col-md-10">
                        <div class="col-md-3 rientroDdl">
                            <asp:Label ID="lblFiltroMatCantCodArt" Text="Filtro Cod. Art." runat="server" />
                            <asp:TextBox ID="txtFiltroMatCantCodArt" placeholder="Filtro Cod. Art." AutoPostBack="true" OnTextChanged="txtFiltroMatCantCodArt_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3 rientroDdl">
                            <asp:Label ID="lblFiltroMatCantDescriCodArt" Text="Filtro Descri. Cod. Art." runat="server" />
                            <asp:TextBox ID="txtFiltroMatCantDescriCodArt" placeholder="Filtro Descri. Cod. Art." AutoPostBack="true" OnTextChanged="txtFiltroMatCantDescriCodArt_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-6 rientroDdl">
                            <asp:Label ID="lblScegliMatCant" Text="Scegli Materiale Cantiere" runat="server" />
                            <asp:DropDownList ID="ddlScegliMatCant" AutoPostBack="true" OnTextChanged="ddlScegliMatCant_TextChanged" CssClass="form-control" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblCodArt" Text="Codice Articolo" runat="server" />
                        <asp:TextBox ID="txtCodArt" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCodArt_TextChanged" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblDescriCodArt" Text="Descrizione Codice Articolo" runat="server" />
                        <asp:TextBox ID="txtDescriCodArt" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDescriCodArt_TextChanged" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblDescrMat" Text="Descrizione Materiale" runat="server" />
                        <asp:TextBox ID="txtDescrMat" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblNote" Text="Note" runat="server" />
                        <asp:TextBox ID="txtNote" TextMode="MultiLine" Rows="2" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblNote_2" Text="Note 2" runat="server" />
                        <asp:TextBox ID="txtNote_2" TextMode="MultiLine" Rows="2" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblQta" Text="Quantità" runat="server" />
                        <asp:TextBox ID="txtQta" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblPzzoNettoMef" Text="Prezzo Netto Mef" runat="server" />
                        <asp:TextBox ID="txtPzzoNettoMef" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnCalcolaPrezzoUnit" OnClick="btnCalcolaPrezzoUnit_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Calcola Prezzo Unitario" />
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnInserisciMatCant" OnClick="btnInserisciMatCant_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Mat Cant" />
                        <asp:Button ID="btnModMatCant" OnClick="btnModMatCant_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Modifica Mat Cant" />
                        <asp:Button ID="btnInserisciRientro" OnClick="btnInserisciRientro_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Rientro" />
                        <asp:Button ID="btnModRientro" OnClick="btnModRientro_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Modifica Rientro" />
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblIsRecordInserito" Text="" CssClass="pull-right" runat="server" />
                    </div>
                </div>
                <div class="row">
                </div>
                <div class="row">
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

                <asp:HiddenField ID="hidIdMatCant" runat="server" />

                <asp:Panel ID="pnlFiltriMatCant" CssClass="col-md-12" Style="margin-top: 20px;" runat="server">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="lblFiltroCodArtGrdMatCant" runat="server" Text="Filtro Cod Art"></asp:Label>
                            <asp:TextBox ID="txtFiltroCodArtGrdMatCant" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblFiltroDescriCodArtGrdMatCant" runat="server" Text="Filtro Descri Cod Art"></asp:Label>
                            <asp:TextBox ID="txtFiltroDescriCodArtGrdMatCant" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblFiltroProtocolloGrdMatCant" runat="server" Text="Filtro Protocollo"></asp:Label>
                            <asp:TextBox ID="txtFiltroProtocolloGrdMatCant" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblFiltroFornitoreGrdMatCant" runat="server" Text="Filtro Fornitore"></asp:Label>
                            <asp:TextBox ID="txtFiltroFornitoreGrdMatCant" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="btnFiltraGrdMatCant" OnClick="btnFiltraGrdMatCant_Click" CssClass="btn btn-lg btn-primary pull-left" runat="server" Text="Filtra Record" />
                            <asp:Label ID="lblTotaleValoreMatCant_Rientro" CssClass="pull-right" Style="color: blue; font-size: 30px;" runat="server"></asp:Label>
                        </div>
                    </div>
                </asp:Panel>

                <div class="col-md-12 table-responsive tableContainer">
                    <asp:GridView ID="grdMatCant" ItemType="GestioneCantieri.Data.MaterialiCantieri" AutoGenerateColumns="false" OnRowCommand="grdMatCant_RowCommand" CssClass="table table-striped text-center" runat="server">
                        <Columns>
                            <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:d}" ApplyFormatInEditMode="True" />
                            <asp:BoundField DataField="ProtocolloInterno" HeaderText="Protocollo" />
                            <asp:BoundField DataField="Fornitore" HeaderText="Fornitore" />
                            <asp:BoundField DataField="CodArt" HeaderText="Cod. Art" />
                            <asp:BoundField DataField="DescriCodArt" HeaderText="Descr. Cod. Art." />
                            <asp:BoundField DataField="Qta" HeaderText="Quantità" />
                            <asp:BoundField DataField="PzzoUniCantiere" HeaderText="Prezzo Unitario" DataFormatString="{0:0.00}" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnVisualMatCant" CommandName="VisualMatCant" CommandArgument="<%# BindItem.IdMaterialiCantieri %>" CssClass="btn btn-lg btn-default" runat="server" Text="Visualizza" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnModMatCant" CommandName="ModMatCant" CommandArgument="<%# BindItem.IdMaterialiCantieri %>" CssClass="btn btn-lg btn-default" runat="server" Text="Modifica" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnElimMatCant" CommandName="ElimMatCant" CommandArgument="<%# BindItem.IdMaterialiCantieri %>"
                                        CssClass="btn btn-lg btn-default" runat="server" Text="Elimina" OnClientClick="return confirm('Vuoi veramente eliminare questo record?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <asp:GridView ID="grdRientro" ItemType="GestioneCantieri.Data.MaterialiCantieri" AutoGenerateColumns="false" OnRowCommand="grdRientro_RowCommand" CssClass="table table-striped text-center" runat="server">
                        <Columns>
                            <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:d}" ApplyFormatInEditMode="True" />
                            <asp:BoundField DataField="ProtocolloInterno" HeaderText="Protocollo" />
                            <asp:BoundField DataField="Fornitore" HeaderText="Fornitore" />
                            <asp:BoundField DataField="CodArt" HeaderText="Cod. Art" />
                            <asp:BoundField DataField="DescriCodArt" HeaderText="Descr. Cod. Art." />
                            <asp:BoundField DataField="Qta" HeaderText="Quantità" />
                            <asp:BoundField DataField="PzzoUniCantiere" HeaderText="Prezzo Unitario" DataFormatString="{0:0.00}" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnVisualRientro" CommandName="VisualRientro" CommandArgument="<%# BindItem.IdMaterialiCantieri %>" CssClass="btn btn-lg btn-default" runat="server" Text="Visualizza" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnModRientro" CommandName="ModRientro" CommandArgument="<%# BindItem.IdMaterialiCantieri %>" CssClass="btn btn-lg btn-default" runat="server" Text="Modifica" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnElimRientro" CommandName="ElimRientro" CommandArgument="<%# BindItem.IdMaterialiCantieri %>"
                                        CssClass="btn btn-lg btn-default" runat="server" Text="Elimina" OnClientClick="return confirm('Vuoi veramente eliminare questo record?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>

            <!-- Maschera manodopera -->
            <asp:Panel ID="pnlManodopera" CssClass="col-md-12" runat="server">
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblManodopQta" Text="Quantità" runat="server" />
                        <asp:TextBox ID="txtManodopQta" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtManodopQta_TextChanged" runat="server"></asp:TextBox>
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
                        <asp:TextBox ID="txtNote1" TextMode="MultiLine" Rows="2" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="lblNote2" Text="Note 2" runat="server" />
                        <asp:TextBox ID="txtNote2" TextMode="MultiLine" Rows="2" CssClass="form-control" runat="server"></asp:TextBox>
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
                        <asp:CheckBox ID="chkManodopRicaricoSiNo" CssClass="form-control" runat="server" />
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnInsManodop" OnClick="btnInsManodop_Click" CssClass="btn btn-lg btn-primary pull-left" runat="server" Text="Inserisci Manodopera" />
                        <asp:Button ID="btnModManodop" OnClick="btnModManodop_Click" CssClass="btn btn-lg btn-primary pull-left" runat="server" Text="Modifica Manodopera" />
                        <asp:Label ID="lblIsManodopInserita" Text="" CssClass="pull-right" runat="server" />
                    </div>
                    <div class="col-md-4">
                        <asp:Button ID="btnAggiornaValManodop" OnClick="btnAggiornaValManodop_Click" CssClass="btn btn-lg btn-primary pull-right" Style="position: relative; top: 6px;" runat="server" Text="Aggiorna Val. Manodop." />
                        <asp:TextBox ID="txtAggiornaValManodop" placeholder="Nuovo Valore Manodopera" CssClass="form-control" Style="width: 50%; float: right; position: relative; top: 10px; margin-right: 10px;" runat="server"></asp:TextBox>
                    </div>
                </div>

                <asp:HiddenField ID="hidManodop" runat="server" />

                <asp:Panel ID="pnlFiltriManodop" CssClass="col-md-12" Style="margin-top: 20px;" runat="server">
                    <div class="col-md-offset-3 col-md-6">
                        <div class="col-md-3">
                            <asp:Label ID="lblFiltroManodopCodArt" runat="server" Text="Filtro Cod Art"></asp:Label>
                            <asp:TextBox ID="txtFiltroManodopCodArt" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblFiltroManodopDescriCodArt" runat="server" Text="Filtro Descri Cod Art"></asp:Label>
                            <asp:TextBox ID="txtFiltroManodopDescriCodArt" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblFiltroManodopProtocollo" runat="server" Text="Filtro Protocollo"></asp:Label>
                            <asp:TextBox ID="txtFiltroManodopProtocollo" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnFiltraGrdManodop" OnClick="btnFiltraGrdManodop_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Filtra Record" />
                        </div>
                    </div>
                </asp:Panel>

                <div class="col-md-12 table-responsive tableContainer">
                    <asp:GridView ID="grdManodop" ItemType="GestioneCantieri.Data.MaterialiCantieri" AutoGenerateColumns="false" OnRowCommand="grdManodop_RowCommand" CssClass="table table-striped text-center" runat="server">
                        <Columns>
                            <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:d}" ApplyFormatInEditMode="True" />
                            <asp:BoundField DataField="ProtocolloInterno" HeaderText="Protocollo" />
                            <asp:BoundField DataField="CodArt" HeaderText="Cod. Art" />
                            <asp:BoundField DataField="DescriCodArt" HeaderText="Descr. Cod. Art." />
                            <asp:BoundField DataField="Qta" HeaderText="Quantità" />
                            <asp:BoundField DataField="PzzoUniCantiere" HeaderText="Prezzo Unitario" DataFormatString="{0:0.00}" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnVisualManodop" CommandName="VisualManodop" CommandArgument="<%# BindItem.IdMaterialiCantieri %>" CssClass="btn btn-lg btn-default" runat="server" Text="Visualizza" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnModManodop" CommandName="ModManodop" CommandArgument="<%# BindItem.IdMaterialiCantieri %>" CssClass="btn btn-lg btn-default" runat="server" Text="Modifica" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnElimManodop" CommandName="ElimManodop" CommandArgument="<%# BindItem.IdMaterialiCantieri %>"
                                        CssClass="btn btn-lg btn-default" runat="server" Text="Elimina" OnClientClick="return confirm('Vuoi veramente eliminare questo record?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
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
                        <asp:TextBox ID="txtOperQta" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtOperQta_TextChanged" runat="server"></asp:TextBox>
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
                        <asp:TextBox ID="txtOperNote1" TextMode="MultiLine" Rows="2" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="lblOperNote2" Text="Note 2" runat="server" />
                        <asp:TextBox ID="txtOperNote2" TextMode="MultiLine" Rows="2" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblOperVisibile" Text="Visibile" runat="server" />
                        <asp:CheckBox ID="chkOperVisibile" CssClass="form-control" runat="server" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblOperRicalcolo" Text="Ricalcolo" runat="server" />
                        <asp:CheckBox ID="chkOperRicalcolo" CssClass="form-control" Enabled="false" runat="server" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblOperRicaricoSiNo" Text="Ricarico Si/No" runat="server" />
                        <asp:CheckBox ID="chkOperRicaricoSiNo" CssClass="form-control" runat="server" />
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnInsOper" OnClick="btnInsOper_Click" CssClass="btn btn-lg btn-primary pull-left" runat="server" Text="Inserisci Operaio" />
                        <asp:Button ID="btnModOper" OnClick="btnModOper_Click" CssClass="btn btn-lg btn-primary pull-left" runat="server" Text="Modifica Operaio" />
                        <asp:Label ID="lblIsOperInserita" Text="" CssClass="pull-right" runat="server" />
                    </div>
                    <div class="col-md-4">
                        <asp:Button ID="btnNuovoCostoOperaio" OnClick="btnNuovoCostoOperaio_Click" CssClass="btn btn-lg btn-primary pull-right" Style="position: relative; top: 6px;" runat="server" Text="Aggiorna Costo Operaio" />
                        <asp:TextBox ID="txtNuovoCostoOperaio" placeholder="Nuovo Costo Operaio" CssClass="form-control" Style="width: 50%; float: right; position: relative; top: 10px; margin-right: 10px;" runat="server"></asp:TextBox>
                    </div>
                </div>

                <asp:HiddenField ID="hidOper" runat="server" />

                <asp:Panel ID="pnlFiltriOper" CssClass="col-md-12" Style="margin-top: 20px" runat="server">
                    <div class="col-md-offset-3 col-md-6">
                        <div class="col-md-3">
                            <asp:Label ID="lblFiltroOperCodArt" runat="server" Text="Filtro Cod Art"></asp:Label>
                            <asp:TextBox ID="txtFiltroOperCodArt" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblFiltroOperDescriCodArt" runat="server" Text="Filtro Descri Cod Art"></asp:Label>
                            <asp:TextBox ID="txtFiltroOperDescriCodArt" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblFiltroOperProtocollo" runat="server" Text="Filtro Protocollo"></asp:Label>
                            <asp:TextBox ID="txtFiltroOperProtocollo" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnOperFiltraGrd" OnClick="btnOperFiltraGrd_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Filtra Record" />
                        </div>
                    </div>
                </asp:Panel>

                <div class="col-md-12 table-responsive tableContainer">
                    <asp:GridView ID="grdOperai" ItemType="GestioneCantieri.Data.MaterialiCantieri" AutoGenerateColumns="false" OnRowCommand="grdOperai_RowCommand" CssClass="table table-striped text-center" runat="server">
                        <Columns>
                            <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:d}" ApplyFormatInEditMode="True" />
                            <asp:BoundField DataField="ProtocolloInterno" HeaderText="Protocollo" />
                            <asp:BoundField DataField="CodArt" HeaderText="Cod. Art" />
                            <asp:BoundField DataField="DescriCodArt" HeaderText="Descr. Cod. Art." />
                            <asp:BoundField DataField="Qta" HeaderText="Quantità" />
                            <asp:BoundField DataField="PzzoUniCantiere" HeaderText="Prezzo Unitario" DataFormatString="{0:0.00}" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnVisualOper" CommandName="VisualOper" CommandArgument="<%# BindItem.IdMaterialiCantieri %>" CssClass="btn btn-lg btn-default" runat="server" Text="Visualizza" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnModOper" CommandName="ModOper" CommandArgument="<%# BindItem.IdMaterialiCantieri %>" CssClass="btn btn-lg btn-default" runat="server" Text="Modifica" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnElimOper" CommandName="ElimOper" CommandArgument="<%# BindItem.IdMaterialiCantieri %>"
                                        CssClass="btn btn-lg btn-default" runat="server" Text="Elimina" OnClientClick="return confirm('Vuoi veramente eliminare questo record?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
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
                            <asp:TextBox ID="txtArrotQta" CssClass="form-control" runat="server"></asp:TextBox>
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
                            <asp:CheckBox ID="chkArrotVisibile" CssClass="form-control" runat="server" />
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblArrotRicalcolo" Text="Ricalcolo" runat="server" />
                            <asp:CheckBox ID="chkArrotRicalcolo" CssClass="form-control" Enabled="false" runat="server" />
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblArrotRicaricoSiNo" Text="Ricarico Si/No" runat="server" />
                            <asp:CheckBox ID="chkArrotRicaricoSiNo" CssClass="form-control" runat="server" />
                        </div>
                        <div class="col-md-6">
                            <asp:Button ID="btnInsArrot" OnClick="btnInsArrot_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Arrotondamento" />
                            <asp:Button ID="btnModArrot" OnClick="btnModArrot_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Modifica Arrotondamento" />
                            <asp:Label ID="lblIsArrotondInserito" Text="" CssClass="pull-right" runat="server" />
                        </div>
                    </div>
                </div>

                <asp:HiddenField ID="hidArrot" runat="server" />

                <asp:Panel ID="pnlFiltriArrot" CssClass="col-md-12" Style="margin-top: 20px" runat="server">
                    <div class="col-md-offset-3 col-md-6">
                        <div class="col-md-3">
                            <asp:Label ID="lblFiltroArrotCodArt" runat="server" Text="Filtro Cod Art"></asp:Label>
                            <asp:TextBox ID="txtFiltroArrotCodArt" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblFiltroArrotDescriCodArt" runat="server" Text="Filtro Descri Cod Art"></asp:Label>
                            <asp:TextBox ID="txtFiltroArrotDescriCodArt" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblFiltroArrotProtocollo" runat="server" Text="Filtro Protocollo"></asp:Label>
                            <asp:TextBox ID="txtFiltroArrotProtocollo" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnArrotFiltraGrd" OnClick="btnArrotFiltraGrd_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Filtra Record" />
                        </div>
                    </div>
                </asp:Panel>

                <div class="col-md-12 table-responsive tableContainer">
                    <asp:GridView ID="grdArrot" ItemType="GestioneCantieri.Data.MaterialiCantieri" AutoGenerateColumns="false" OnRowCommand="grdArrot_RowCommand" CssClass="table table-striped text-center" runat="server">
                        <Columns>
                            <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:d}" ApplyFormatInEditMode="True" />
                            <asp:BoundField DataField="ProtocolloInterno" HeaderText="Protocollo" />
                            <asp:BoundField DataField="CodArt" HeaderText="Cod. Art" />
                            <asp:BoundField DataField="DescriCodArt" HeaderText="Descr. Cod. Art." />
                            <asp:BoundField DataField="Qta" HeaderText="Quantità" />
                            <asp:BoundField DataField="PzzoUniCantiere" HeaderText="Prezzo Unitario" DataFormatString="{0:0.00}" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnVisualArrot" CommandName="VisualArrot" CommandArgument="<%# BindItem.IdMaterialiCantieri %>" CssClass="btn btn-lg btn-default" runat="server" Text="Visualizza" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnModArrot" CommandName="ModArrot" CommandArgument="<%# BindItem.IdMaterialiCantieri %>" CssClass="btn btn-lg btn-default" runat="server" Text="Modifica" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnElimArrot" CommandName="ElimArrot" CommandArgument="<%# BindItem.IdMaterialiCantieri %>"
                                        CssClass="btn btn-lg btn-default" runat="server" Text="Elimina" OnClientClick="return confirm('Vuoi veramente eliminare questo record?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>

            <!-- Maschera Gestione A Chiamata -->
            <asp:Panel ID="pnlGestChiamata" CssClass="col-md-12" runat="server">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblChiamCodArt" Text="Codice Articolo" runat="server" />
                        <asp:TextBox ID="txtChiamCodArt" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtChiamCodArt_TextChanged" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblChiamDescriCodArt" Text="Descrizione Codice Articolo" runat="server" />
                        <asp:TextBox ID="txtChiamDescriCodArt" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtChiamDescriCodArt_TextChanged" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblChiamDescrMate" Text="Descrizione Materiale" runat="server" />
                        <asp:TextBox ID="txtChiamDescrMate" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblChiamNote" Text="Note" runat="server" />
                        <asp:TextBox ID="txtChiamNote" TextMode="MultiLine" Rows="2" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblChiamNote2" Text="Note 2" runat="server" />
                        <asp:TextBox ID="txtChiamNote2" TextMode="MultiLine" Rows="2" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblChiamQta" Text="Quantità" runat="server" />
                        <asp:TextBox ID="txtChiamQta" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblChiamPzzoNetto" Text="Prezzo Netto Mef" runat="server" />
                        <asp:TextBox ID="txtChiamPzzoNetto" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblChiamPzzoUnit" Text="Prezzo Unitario" runat="server" />
                        <asp:TextBox ID="txtChiamPzzoUnit" Text="0.00" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblChiamPzzoFinCli" Text="Prezzo Finale Cliente" runat="server" />
                        <asp:TextBox ID="txtChiamPzzoFinCli" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-md-1">
                        <asp:Label ID="lblChiamVisibile" Text="Visibile" runat="server" />
                        <asp:CheckBox ID="chkChiamVisibile" CssClass="form-control" Checked="true" runat="server" />
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="lblChiamRicalcolo" Text="Ricalcolo" runat="server" />
                        <asp:CheckBox ID="chkChiamRicalcolo" CssClass="form-control" Checked="false" runat="server" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblChiamRicaricoSiNo" Text="Ricarico Si/No" runat="server" />
                        <asp:CheckBox ID="chkChiamRicaricoSiNo" CssClass="form-control" Checked="false" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <asp:Button ID="btnCalcolaPzzoUnitAChiam" OnClick="btnCalcolaPzzoUnitAChiam_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Calcola Prezzo Unitario" />
                    </div>
                    <div class="col-md-6">
                        <asp:Button ID="btnInsAChiam" OnClick="btnInsAChiam_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci A Chiamata" />
                        <asp:Button ID="btnModAChiam" OnClick="btnModAChiam_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Modifica A Chiamata" />
                        <asp:Label ID="lblIsAChiamInserita" Text="" CssClass="pull-right" runat="server" />
                    </div>
                </div>

                <asp:HiddenField ID="hidAChiamata" runat="server" />

                <asp:Panel ID="pnlFiltriGrdAChiam" CssClass="col-md-12" Style="margin-top: 20px" runat="server">
                    <div class="col-md-offset-3 col-md-6">
                        <div class="col-md-3">
                            <asp:Label ID="lblFiltroAChiamCodArt" runat="server" Text="Filtro Cod Art"></asp:Label>
                            <asp:TextBox ID="txtFiltroAChiamCodArt" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblFiltroAChiamDescriCodArt" runat="server" Text="Filtro Descri Cod Art"></asp:Label>
                            <asp:TextBox ID="txtFiltroAChiamDescriCodArt" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblFiltroAChiamProtocollo" runat="server" Text="Filtro Protocollo"></asp:Label>
                            <asp:TextBox ID="txtFiltroAChiamProtocollo" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnFiltraGrdAChiam" OnClick="btnFiltraGrdAChiam_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Filtra Record" />
                        </div>
                    </div>
                </asp:Panel>

                <div class="col-md-12 table-responsive tableContainer">
                    <asp:GridView ID="grdAChiam" ItemType="GestioneCantieri.Data.MaterialiCantieri" AutoGenerateColumns="false" OnRowCommand="grdAChiam_RowCommand" CssClass="table table-striped text-center" runat="server">
                        <Columns>
                            <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:d}" ApplyFormatInEditMode="True" />
                            <asp:BoundField DataField="ProtocolloInterno" HeaderText="Protocollo" />
                            <asp:BoundField DataField="Fornitore" HeaderText="Fornitore" />
                            <asp:BoundField DataField="CodArt" HeaderText="Cod. Art" />
                            <asp:BoundField DataField="DescriCodArt" HeaderText="Descr. Cod. Art." />
                            <asp:BoundField DataField="Qta" HeaderText="Quantità" />
                            <asp:BoundField DataField="PzzoUniCantiere" HeaderText="Prezzo Unitario" DataFormatString="{0:0.00}" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnVisualChiam" CommandName="VisualChiam" CommandArgument="<%# BindItem.IdMaterialiCantieri %>" CssClass="btn btn-lg btn-default" runat="server" Text="Visualizza" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnModChiam" CommandName="ModChiam" CommandArgument="<%# BindItem.IdMaterialiCantieri %>" CssClass="btn btn-lg btn-default" runat="server" Text="Modifica" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnElimChiam" CommandName="ElimChiam" CommandArgument="<%# BindItem.IdMaterialiCantieri %>"
                                        CssClass="btn btn-lg btn-default" runat="server" Text="Elimina" OnClientClick="return confirm('Vuoi veramente eliminare questo record?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>

            <!-- Maschera di Gestione Spese -->
            <asp:Panel ID="pnlGestSpese" CssClass="col-md-12" runat="server">
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <asp:Label ID="lblScegliSpesa" Text="Scegli Spesa" runat="server" />
                            <asp:DropDownList ID="ddlScegliSpesa" CssClass="form-control" AutoPostBack="true" OnTextChanged="ddlScegliSpesa_TextChanged" runat="server"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblSpeseCodArt" Text="Codice Articolo" runat="server" />
                            <asp:TextBox ID="txtSpeseCodArt" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtSpeseCodArt_TextChanged" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblSpeseDescriCodArt" Text="Descrizione Codice Articolo" runat="server" />
                            <asp:TextBox ID="txtSpeseDescriCodArt" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtSpeseDescriCodArt_TextChanged" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblSpeseQta" Text="Quantità" runat="server" />
                            <asp:TextBox ID="txtSpeseQta" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblSpesaPrezzo" Text="Prezzo" runat="server" />
                            <asp:TextBox ID="txtSpesaPrezzo" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblSpesaPrezzoCalcolato" Text="Prezzo Calcolato" runat="server" />
                            <asp:TextBox ID="txtSpesaPrezzoCalcolato" Text="0.00" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblSpesaVisibile" Text="Visibile" runat="server" />
                            <asp:CheckBox ID="chkSpesaVisibile" CssClass="form-control" Checked="false" runat="server" />
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="lblSpesaRicalcolo" Text="Ricalcolo" runat="server" />
                            <asp:CheckBox ID="chkSpesaRicalcolo" CssClass="form-control" Checked="false" Enabled="false" runat="server" />
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblSpesaRicarico" Text="Ricarico Si/No" runat="server" />
                            <asp:CheckBox ID="chkSpesaRicarico" CssClass="form-control" Checked="false" runat="server" />
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-8">
                            <asp:Button ID="btnCalcolaPzzoUnitSpese" OnClick="btnCalcolaPzzoUnitSpese_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Calcola Prezzo Spesa" />
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="btnInsSpesa" OnClick="btnInsSpesa_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Spesa" />
                            <asp:Button ID="btnModSpesa" OnClick="btnModSpesa_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Modifica Spesa" />
                            <asp:Label ID="lblIsSpesaInserita" Text="" CssClass="pull-right" runat="server" />
                        </div>
                    </div>
                </div>

                <asp:HiddenField ID="hidIdSpesa" runat="server" />

                <asp:Panel ID="pnlFiltriGrdSpese" CssClass="col-md-12" Style="margin-top: 20px" runat="server">
                    <div class="col-md-offset-3 col-md-6">
                        <div class="col-md-3">
                            <asp:Label ID="lblFiltroSpeseCodArt" runat="server" Text="Filtro Cod Art"></asp:Label>
                            <asp:TextBox ID="txtFiltroSpeseCodArt" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblFiltroSpeseDescriCodArt" runat="server" Text="Filtro Descri Cod Art"></asp:Label>
                            <asp:TextBox ID="txtFiltroSpeseDescriCodArt" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblFiltroSpeseProtocollo" runat="server" Text="Filtro Protocollo"></asp:Label>
                            <asp:TextBox ID="txtFiltroSpeseProtocollo" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnFiltraGrdSpese" OnClick="btnFiltraGrdSpese_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Filtra Record" Style="position: relative; top: 10px;" />
                        </div>
                    </div>
                </asp:Panel>

                <div class="col-md-12 table-responsive tableContainer">
                    <asp:GridView ID="grdSpese" ItemType="GestioneCantieri.Data.MaterialiCantieri" AutoGenerateColumns="false" OnRowCommand="grdSpese_RowCommand" CssClass="table table-striped text-center" runat="server">
                        <Columns>
                            <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:d}" ApplyFormatInEditMode="True" />
                            <asp:BoundField DataField="ProtocolloInterno" HeaderText="Protocollo" />
                            <asp:BoundField DataField="Fornitore" HeaderText="Fornitore" />
                            <asp:BoundField DataField="CodArt" HeaderText="Cod. Art" />
                            <asp:BoundField DataField="DescriCodArt" HeaderText="Descr. Cod. Art." />
                            <asp:BoundField DataField="Qta" HeaderText="Quantità" />
                            <asp:BoundField DataField="PzzoUniCantiere" HeaderText="Prezzo Unitario" DataFormatString="{0:0.00}" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnVisualSpesa" CommandName="VisualSpesa" CommandArgument="<%# BindItem.IdMaterialiCantieri %>" CssClass="btn btn-lg btn-default" runat="server" Text="Visualizza" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnModSpesa" CommandName="ModSpesa" CommandArgument="<%# BindItem.IdMaterialiCantieri %>" CssClass="btn btn-lg btn-default" runat="server" Text="Modifica" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnElimSpesa" CommandName="ElimSpesa" CommandArgument="<%# BindItem.IdMaterialiCantieri %>"
                                        CssClass="btn btn-lg btn-default" runat="server" Text="Elimina" OnClientClick="return confirm('Vuoi veramente eliminare questo record?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
