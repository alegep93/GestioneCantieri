<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="InserimentoDati.aspx.cs" Inherits="GestioneCantieri.InserimentoDati" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    <title>Inserimento Dati</title>
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

        .labelConferma {
            position: relative;
            top: 14px;
            right: 10px;
        }

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

        .table-container {
            max-height: 500px;
            overflow: scroll;
            overflow-y: auto;
        }
    </style>
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <div class="container-fluid">
        <div class="row">
            <!-- Titolo pagina dinamico -->
            <h1>
                <asp:Label ID="lblTitoloInserimento" runat="server" Text="Inserisci Clienti" />
            </h1>

            <!-- Pulsanti per lo switch dei pannelli -->
            <div class="col-md-offset-3 col-md-6 text-center">
                <div class="col-md-2">
                    <asp:Button ID="btnShowInsClienti" OnClick="btnShowInsClienti_Click" CssClass="btn btn-lg btn-default" runat="server" Text="Clienti" />
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnShowInsFornitori" OnClick="btnShowInsFornitori_Click" CssClass="btn btn-lg btn-default" runat="server" Text="Fornitori" />
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnShowInsOperai" OnClick="btnShowInsOperai_Click" CssClass="btn btn-lg btn-default" runat="server" Text="Operai" />
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnShowInsCantieri" OnClick="btnShowInsCantieri_Click" CssClass="btn btn-lg btn-default" runat="server" Text="Cantieri" />
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnShowInsSpese" OnClick="btnShowInsSpese_Click" CssClass="btn btn-lg btn-default" runat="server" Text="Spese" />
                </div>
            </div>
            <!-- Fine Pulsanti per lo switch dei pannelli -->
        </div>
        <div class="row">
            <!-- Pannello inserimento Clienti -->
            <asp:Panel ID="pnlInsClienti" DefaultButton="btnInsCliente" CssClass="panel-container col-md-12" runat="server" Style="margin-top: 20px;">
                <div class="col-md-3 form-group">
                    <asp:Label ID="lblRagSocCLi" runat="server" Text="Ragione Sociale Cliente" />
                    <asp:TextBox ID="txtRagSocCli" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-3 form-group">
                    <asp:Label ID="lblIndirizzo" runat="server" Text="Indirizzo" />
                    <asp:TextBox ID="txtIndirizzo" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-3 form-group">
                    <asp:Label ID="lblCap" runat="server" Text="Cap" />
                    <asp:TextBox ID="txtCap" CssClass="form-control" MaxLength="5" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-3 form-group">
                    <asp:Label ID="lblCitta" runat="server" Text="Città" />
                    <asp:TextBox ID="txtCitta" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="col-md-3 form-group">
                    <asp:Label ID="lblProvincia" runat="server" Text="Provincia" />
                    <asp:TextBox ID="txtProvincia" CssClass="form-control" MaxLength="4" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-3 form-group">
                    <asp:Label ID="lblTelefono" runat="server" Text="Telefono" />
                    <asp:TextBox ID="txtTelefono" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-3 form-group">
                    <asp:Label ID="lblCellulare" runat="server" Text="Cellulare" />
                    <asp:TextBox ID="txtCellulare" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-3 form-group">
                    <asp:Label ID="lblPartitaIva" runat="server" Text="Partita Iva" />
                    <asp:TextBox ID="txtPartitaIva" CssClass="form-control" MaxLength="11" runat="server"></asp:TextBox>
                </div>

                <div class="col-md-3 form-group">
                    <asp:Label ID="lblCodiceFiscale" runat="server" Text="Codice Fiscale" />
                    <asp:TextBox ID="txtCodiceFiscale" CssClass="form-control" MaxLength="16" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-3 form-group">
                    <asp:Label ID="lblDataInserimento" runat="server" Text="Data Inserimento" />
                    <asp:TextBox ID="txtDataInserimento" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-6 form-group">
                    <asp:Label ID="lblNote" runat="server" Text="Note" />
                    <asp:TextBox ID="txtNote" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <asp:HiddenField ID="hidIdClienti" runat="server" />

                <div class="col-md-12 form-group">
                    <asp:Button ID="btnModCliente" OnClick="btnModCliente_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Modifica Cliente" />
                    <asp:Button ID="btnInsCliente" OnClick="btnInsCliente_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Cliente" />
                    <asp:Label ID="lblIsClienteInserito" CssClass="pull-right labelConferma" runat="server" Text=""></asp:Label>
                </div>

                <asp:Panel ID="pnlFiltriCliente" CssClass="col-md-12" runat="server" Style="margin-top: 20px;">
                    <div class="col-md-2">
                        <asp:Label ID="lblFiltroRagSocCli" runat="server" Text="Ragione Sociale Cliente"></asp:Label>
                        <asp:TextBox ID="txtFiltroRagSocCli" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnFiltraClienti" OnClick="btnFiltraClienti_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Filtra" />
                        <asp:Button ID="btnSvuotaFiltriClienti" OnClick="btnSvuotaFiltriClienti_Click" CssClass="btn btn-default pull-right" runat="server" Text="Svuota" Style="margin-right: 5px;" />
                    </div>
                </asp:Panel>

                <!-- Griglia di visualizzazione record -->
                <div class="col-md-12 table-container">
                    <asp:GridView ID="grdClienti" OnRowCommand="grdClienti_RowCommand" AutoGenerateColumns="false"
                        ItemType="GestioneCantieri.Data.Clienti" runat="server" CssClass="table table-striped table-responsive text-center">
                        <Columns>
                            <asp:BoundField HeaderText="Ragione Sociale" DataField="RagSocCli" />
                            <asp:BoundField HeaderText="Indirizzo" DataField="Indirizzo" />
                            <asp:BoundField HeaderText="Cap" DataField="Cap" />
                            <asp:BoundField HeaderText="Città" DataField="Città" />
                            <%--<asp:BoundField HeaderText="Provincia" DataField="Provincia" />
                            <asp:BoundField HeaderText="Telefono" DataField="Tel1" />
                            <asp:BoundField HeaderText="Cellulare" DataField="Cell1" />--%>
                            <asp:BoundField HeaderText="Partita Iva" DataField="PartitaIva" />
                            <asp:BoundField HeaderText="Codice Fiscale" DataField="CodFiscale" />
                            <%--<asp:BoundField HeaderText="Data Inserimento" DataField="Data" DataFormatString="{0:d}" />
                            <asp:BoundField HeaderText="Note" DataField="Note" />--%>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnVisualCli" CommandName="VisualCli" CommandArgument="<%# BindItem.IdCliente %>" CssClass="btn btn-lg btn-default" runat="server" Text="Visualizza" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnModCli" CommandName="ModCli" CommandArgument="<%# BindItem.IdCliente %>" CssClass="btn btn-lg btn-default" runat="server" Text="Modifica" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnElimCli" CommandName="ElimCli" CommandArgument="<%# BindItem.IdCliente %>"
                                        CssClass="btn btn-lg btn-default" runat="server" Text="Elimina" OnClientClick="return confirm('Vuoi veramente eliminare questo cliente?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <!-- Fine Griglia di visualizzazione record -->
            </asp:Panel>
            <!-- Fine Pannello inserimento Clienti -->

            <!-- Pannello inserimento Fornitori -->
            <asp:Panel ID="pnlInsFornitori" CssClass="panel-container col-md-12" runat="server">
                <div class="col-md-4 form-group">
                    <asp:Label ID="lblRagSocFornit" runat="server" Text="Ragione Sociale Fornitore" />
                    <asp:TextBox ID="txtRagSocFornit" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="lblCittaFornit" runat="server" Text="Citta" />
                    <asp:TextBox ID="txtCittaFornit" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="lblIndirFornit" runat="server" Text="Indirizzo" />
                    <asp:TextBox ID="txtIndirFornit" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="col-md-4 form-group">
                    <asp:Label ID="lblCapFornit" runat="server" Text="Cap" />
                    <asp:TextBox ID="txtCapFornit" CssClass="form-control" MaxLength="5" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="lblTelFornit" runat="server" Text="Telefono" />
                    <asp:TextBox ID="txtTelFornit" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="lblCelFornit" runat="server" Text="Cellulare" />
                    <asp:TextBox ID="txtCelFornit" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="col-md-4 form-group">
                    <asp:Label ID="lblCodFiscFornit" runat="server" Text="Codice Fiscale" />
                    <asp:TextBox ID="txtCodFiscFornit" CssClass="form-control" MaxLength="16" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="lblPartIvaFornit" runat="server" Text="Partita Iva" />
                    <asp:TextBox ID="txtPartIvaFornit" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="lblAbbrevFornit" runat="server" Text="Abbreviato" />
                    <asp:TextBox ID="txtAbbrevFornit" CssClass="form-control" MaxLength="3" runat="server"></asp:TextBox>
                </div>

                <asp:HiddenField ID="hidIdFornit" runat="server" />

                <div class="col-md-12 form-group">
                    <asp:Button ID="btnModFornit" OnClick="btnModFornit_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Modifica Fornitore" />
                    <asp:Button ID="btnInsFornit" OnClick="btnInsFornit_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Fornitore" />
                    <asp:Label ID="lblIsFornitoreInserito" CssClass="pull-right labelConferma" runat="server" Text=""></asp:Label>
                </div>

                <!-- Filtri su griglia Fornitori -->
                <div class="col-md-12">
                    <div class="col-md-4">
                        <asp:Label ID="lblFiltroRagSocForni" runat="server" Text="Filtro Rag. Soc. Fornitore" />
                        <asp:TextBox ID="txtFiltroRagSocForni" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <asp:Button ID="btnFiltraGrdFornitori" OnClick="btnFiltraGrdFornitori_Click" CssClass="btn btn-lg btn-primary pull-left" runat="server" Text="Filtra Griglia" Style="position: relative; top: 10px;" />
                    </div>
                </div>

                <!-- Griglia di visualizzazione record -->
                <div class="col-md-12 table-container">
                    <asp:GridView ID="grdFornitori" OnRowCommand="grdFornitori_RowCommand" AutoGenerateColumns="false"
                        ItemType="GestioneCantieri.Data.Fornitori" runat="server" CssClass="table table-striped table-responsive text-center">
                        <Columns>
                            <asp:BoundField HeaderText="Ragione Sociale" DataField="RagSocForni" />
                            <asp:BoundField HeaderText="Città" DataField="Città" />
                            <asp:BoundField HeaderText="Indirizzo" DataField="Indirizzo" />
                            <asp:BoundField HeaderText="Cap" DataField="cap" />
                            <asp:BoundField HeaderText="Telefono" DataField="Tel1" />
                            <asp:BoundField HeaderText="Cellulare" DataField="Cell1" />
                            <asp:BoundField HeaderText="Codice Fiscale" DataField="CodFiscale" />
                            <asp:BoundField HeaderText="Partita Iva" DataField="PartitaIva" />
                            <asp:BoundField HeaderText="Abbreviato" DataField="Abbreviato" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnVisualFornit" CommandName="VisualFornit" CommandArgument="<%# BindItem.IdFornitori %>" CssClass="btn btn-lg btn-default" runat="server" Text="Visualizza" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnModFornit" CommandName="ModFornit" CommandArgument="<%# BindItem.IdFornitori %>" CssClass="btn btn-lg btn-default" runat="server" Text="Modifica" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnElimFornit" CommandName="ElimFornit" CommandArgument="<%# BindItem.IdFornitori %>"
                                        CssClass="btn btn-lg btn-default" runat="server" Text="Elimina" OnClientClick="return confirm('Vuoi veramente eliminare questo fornitore?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
            <!-- Fine Pannello inserimento Fornitori -->

            <!-- Pannello inserimento Operai -->
            <asp:Panel ID="pnlInsOperai" CssClass="panel-container col-md-12" runat="server">
                <div class="col-md-3 form-group">
                    <asp:Label ID="lblNomeOper" runat="server" Text="Nome Operaio" />
                    <asp:TextBox ID="txtNomeOper" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-3 form-group">
                    <asp:Label ID="lblDescrOper" runat="server" Text="Descrizione" />
                    <asp:TextBox ID="txtDescrOper" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2 form-group">
                    <asp:Label ID="lblSuffOper" runat="server" Text="Suffisso" />
                    <asp:TextBox ID="txtSuffOper" CssClass="form-control" MaxLength="4" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2 form-group">
                    <asp:Label ID="lblOperaio" runat="server" Text="Operaio" />
                    <asp:TextBox ID="txtOperaio" CssClass="form-control" MaxLength="4" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2 form-group">
                    <asp:Label ID="lblCostoOperaio" runat="server" Text="Costo Operaio" />
                    <asp:TextBox ID="txtCostoOperaio" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <asp:HiddenField ID="hidIdOper" runat="server" />

                <div class="col-md-12 form-group">
                    <asp:Button ID="btnModOper" OnClick="btnModOper_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Modifica Operaio" />
                    <asp:Button ID="btnInsOper" OnClick="btnInsOper_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Operaio" />
                    <asp:Label ID="lblIsOperaioInserito" CssClass="pull-right labelConferma" runat="server" Text=""></asp:Label>
                </div>

                <!-- Griglia di visualizzazione record -->
                <div class="col-md-12 table-container">
                    <asp:GridView ID="grdOperai" AutoGenerateColumns="false"
                        ItemType="GestioneCantieri.Data.Operai" OnRowCommand="grdOperai_RowCommand" runat="server" CssClass="table table-striped table-responsive text-center">
                        <Columns>
                            <asp:BoundField HeaderText="Nome" DataField="NomeOp" />
                            <asp:BoundField HeaderText="Descrizione" DataField="DescrOp" />
                            <asp:BoundField HeaderText="Suffisso" DataField="Suffisso" />
                            <asp:BoundField HeaderText="Operaio" DataField="Operaio" />
                            <asp:BoundField HeaderText="Costo Operaio" DataField="CostoOperaio" DataFormatString="{0:0.00}" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnVisualOper" CommandName="VisualOper" CommandArgument="<%# BindItem.IdOperaio %>" CssClass="btn btn-lg btn-default" runat="server" Text="Visualizza" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnModOper" CommandName="ModOper" CommandArgument="<%# BindItem.IdOperaio %>" CssClass="btn btn-lg btn-default" runat="server" Text="Modifica" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnElimOper" CommandName="ElimOper" CommandArgument="<%# BindItem.IdOperaio %>"
                                        CssClass="btn btn-lg btn-default" runat="server" Text="Elimina" OnClientClick="return confirm('Vuoi veramente eliminare questo operaio?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
            <!-- Fine Pannello inserimento Operai -->

            <!-- Pannello inserimento Cantieri -->
            <asp:Panel ID="pnlInsCantieri" CssClass="panel-container col-md-12" runat="server">
                <asp:Panel ID="pnlTxtBoxCantContainer" CssClass="col-md-12" runat="server">
                    <div class="col-md-2 form-group">
                        <asp:Label ID="lblScegliCliente" runat="server" Text="Scegli Cliente" />
                        <asp:DropDownList ID="ddlScegliClientePerCantiere" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <div class="col-md-2 form-group">
                        <asp:Label ID="lblDataInserimentoCantiere" runat="server" Text="Data Inserimento" />
                        <asp:TextBox ID="txtDataInserCant" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2 form-group">
                        <asp:Label ID="lblCodCant" runat="server" Text="Codice Cantiere" />
                        <asp:TextBox ID="txtCodCant" CssClass="form-control" MaxLength="10" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2 form-group">
                        <asp:Label ID="lblDescrCodCant" runat="server" Text="Descrizione Cantiere" />
                        <asp:TextBox ID="txtDescrCodCant" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2 form-group">
                        <asp:Label ID="lblIndirizzoCant" runat="server" Text="Indirizzo" />
                        <asp:TextBox ID="txtIndirizzoCant" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2 form-group">
                        <asp:Label ID="lblCittaCant" runat="server" Text="Città" />
                        <asp:TextBox ID="txtCittaCant" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-md-2 form-group">
                        <asp:Label ID="lblRicaricoCant" runat="server" Text="Ricarico" />
                        <asp:TextBox ID="txtRicaricoCant" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2 form-group">
                        <asp:Label ID="lblPzzoManodopCant" runat="server" Text="Prezzo Manodopera" />
                        <asp:TextBox ID="txtPzzoManodopCant" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-1 form-group">
                        <asp:Label ID="lblCantChiuso" runat="server" Text="Chiuso" />
                        <asp:CheckBox ID="chkCantChiuso" CssClass="form-control" Checked="false" runat="server" />
                    </div>
                    <div class="col-md-1 form-group">
                        <asp:Label ID="lblCantRiscosso" runat="server" Text="Riscosso" />
                        <asp:CheckBox ID="chkCantRiscosso" CssClass="form-control" Checked="false" runat="server" />
                    </div>
                    <div class="col-md-2 form-group">
                        <asp:Label ID="lblNumeroCant" runat="server" Text="Numero" />
                        <asp:TextBox ID="txtNumeroCant" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtNumeroCant_TextChanged" MaxLength="5" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2 form-group">
                        <asp:Label ID="lblValPrevCant" runat="server" Text="Valore Preventivo Cantiere" />
                        <asp:TextBox ID="txtValPrevCant" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2 form-group">
                        <asp:Label ID="lblIvaCant" runat="server" Text="Iva" />
                        <asp:TextBox ID="txtIvaCant" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="col-md-4 form-group">
                        <asp:Label ID="lblAnnoCant" runat="server" Text="Anno" />
                        <asp:TextBox ID="txtAnnoCant" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtAnnoCant_TextChanged" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-1 form-group">
                        <asp:Label ID="lblPrevCant" runat="server" Text="Preventivo" />
                        <asp:CheckBox ID="chkPreventivo" CssClass="form-control" Checked="false" runat="server" />
                    </div>
                    <div class="col-md-1 form-group">
                        <asp:Label ID="lblDaDividere" runat="server" Text="Da Dividere" />
                        <asp:CheckBox ID="chkDaDividere" CssClass="form-control" Checked="false" runat="server" />
                    </div>
                    <div class="col-md-1 form-group">
                        <asp:Label ID="lblDiviso" runat="server" Text="Diviso" />
                        <asp:CheckBox ID="chkDiviso" CssClass="form-control" Checked="false" runat="server" />
                    </div>
                    <div class="col-md-1 form-group">
                        <asp:Label ID="lblFatturato" runat="server" Text="Fatturato" />
                        <asp:CheckBox ID="chkFatturato" CssClass="form-control" Checked="false" runat="server" />
                    </div>
                    <div class="col-md-4 form-group">
                        <asp:Label ID="lblFasciaCant" runat="server" Text="Fascia Cantiere" />
                        <asp:TextBox ID="txtFasciaCant" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <asp:HiddenField ID="hidIdCant" runat="server" />

                    <div class="col-md-12 form-group">
                        <asp:Button ID="btnInsCantiere" OnClick="btnInsCantiere_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Cantiere" />
                        <asp:Button ID="btnModCantiere" OnClick="btnModCantiere_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Modifica Cantiere" />
                        <asp:Label ID="lblIsCantInserito" CssClass="pull-right labelConferma" runat="server" Text=""></asp:Label>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlFiltriCant" CssClass="col-md-12" runat="server" Style="margin-top: 20px;">
                    <div class="col-md-1">
                        <asp:Label ID="lblFiltroAnno" runat="server" Text="Anno"></asp:Label>
                        <asp:TextBox ID="txtFiltroAnno" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblFiltroCodCant" runat="server" Text="Codice Cantiere"></asp:Label>
                        <asp:TextBox ID="txtFiltroCodCant" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblFiltroDescr" runat="server" Text="Descrizione"></asp:Label>
                        <asp:TextBox ID="txtFiltroDescr" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblFiltroCliente" runat="server" Text="Cliente"></asp:Label>
                        <asp:TextBox ID="txtFiltroCliente" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="lblFiltroChiuso" runat="server" Text="Chiuso"></asp:Label>
                        <asp:CheckBox ID="chkFiltroChiuso" CssClass="form-control" Checked="false" runat="server" />
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="lblFiltroRiscosso" runat="server" Text="Riscosso"></asp:Label>
                        <asp:CheckBox ID="chkFiltroRiscosso" CssClass="form-control" Checked="false" runat="server" />
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="lblFiltroFatturato" runat="server" Text="Fatturato"></asp:Label>
                        <asp:CheckBox ID="chkFiltroFatturato" CssClass="form-control" Checked="false" runat="server" />
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnFiltraCant" OnClick="btnFiltraCant_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Filtra" />
                        <asp:Button ID="btnSvuotaFiltri" OnClick="btnSvuotaFiltri_Click" CssClass="btn btn-default pull-right" runat="server" Text="Svuota" Style="margin-right: 5px;" />
                    </div>
                </asp:Panel>

                <!-- Griglia di visualizzazione record -->
                <div class="col-md-12 table-container">
                    <asp:GridView ID="grdCantieri" OnRowCommand="grdCantieri_RowCommand" AutoGenerateColumns="false"
                        ItemType="GestioneCantieri.Data.Cantieri" runat="server" CssClass="table table-striped table-responsive text-center">
                        <Columns>
                            <asp:BoundField HeaderText="Codice Cantiere" DataField="CodCant" />
                            <asp:BoundField HeaderText="Descrizione Cantiere" DataField="DescriCodCAnt" />
                            <asp:BoundField HeaderText="Cliente" DataField="RagSocCli" />
                            <asp:BoundField HeaderText="Data Inserimento" DataField="Data" DataFormatString="{0:d}" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnVisualCant" CommandName="VisualCant" CommandArgument="<%# BindItem.IdCantieri %>" CssClass="btn btn-lg btn-default" runat="server" Text="Visualizza" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnModCant" CommandName="ModCant" CommandArgument="<%# BindItem.IdCantieri %>" CssClass="btn btn-lg btn-default" runat="server" Text="Modifica" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnElimCant" CommandName="ElimCant" CommandArgument="<%# BindItem.IdCantieri %>"
                                        CssClass="btn btn-lg btn-default" runat="server" Text="Elimina" OnClientClick="return confirm('Vuoi veramente eliminare questo cantiere?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <!-- Fine Griglia di visualizzazione record -->
            </asp:Panel>
            <!-- Fine Pannello inserimento Cantieri -->

            <!-- Pannello inserimento spese -->
            <asp:Panel ID="pnlInsSpese" CssClass="panel-container col-md-12" runat="server">
                <div class="col-md-6 form-group">
                    <asp:Label ID="lblSpeseDescr" runat="server" Text="Descrizione" />
                    <asp:TextBox ID="txtSpeseDescr" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-6 form-group">
                    <asp:Label ID="lblSpesePrezzo" runat="server" Text="Prezzo" />
                    <asp:TextBox ID="txtSpesePrezzo" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <asp:HiddenField ID="hidSpese" runat="server" />

                <div class="col-md-12 form-group">
                    <asp:Button ID="btnModSpesa" OnClick="btnModSpesa_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Modifica Spesa" />
                    <asp:Button ID="btnInsSpesa" OnClick="btnInsSpesa_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Spesa" />
                    <asp:Label ID="lblIsSpesaInserita" CssClass="pull-right labelConferma" runat="server" Text=""></asp:Label>
                </div>

                <!-- Filtri su griglia Spese -->
                <asp:Panel ID="pnlFiltriSpese" DefaultButton="btnFiltraGrdSpese" runat="server">
                    <div class="col-md-12">
                        <div class="col-md-4">
                            <asp:Label ID="lblFiltroSpesaDescr" runat="server" Text="Filtro Descrizione Spesa" />
                            <asp:TextBox ID="txtFiltroSpesaDescr" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="btnFiltraGrdSpese" OnClick="btnFiltraGrdSpese_Click" CssClass="btn btn-lg btn-primary pull-left" runat="server" Text="Filtra Spese" Style="position: relative; top: 10px;" />
                        </div>
                    </div>
                </asp:Panel>

                <!-- Griglia di visualizzazione record Spese -->
                <div class="col-md-12 table-container">
                    <asp:GridView ID="grdSpese" OnRowCommand="grdSpese_RowCommand" AutoGenerateColumns="false"
                        ItemType="GestioneCantieri.Data.Spese" runat="server" CssClass="table table-striped table-responsive text-center">
                        <Columns>
                            <asp:BoundField HeaderText="Descrizione" DataField="Descrizione" />
                            <asp:BoundField HeaderText="Prezzo" DataField="Prezzo" DataFormatString="{0:0.00}" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnVisualSpesa" CommandName="VisualSpesa" CommandArgument="<%# BindItem.IdSpesa %>" CssClass="btn btn-lg btn-default" runat="server" Text="Visualizza" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnModSpesa" CommandName="ModSpesa" CommandArgument="<%# BindItem.IdSpesa %>" CssClass="btn btn-lg btn-default" runat="server" Text="Modifica" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnElimSpesa" CommandName="ElimSpesa" CommandArgument="<%# BindItem.IdSpesa %>"
                                        CssClass="btn btn-lg btn-default" runat="server" Text="Elimina" OnClientClick="return confirm('Vuoi veramente eliminare questa spesa?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
            <!-- Fine Pannello inserimento spese-->
        </div>
    </div>
</asp:Content>
