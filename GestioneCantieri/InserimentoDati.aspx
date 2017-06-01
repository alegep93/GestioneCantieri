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
        span.form-control{
            border: none;
            background-color: transparent;
            box-shadow: none;
            -webkit-box-shadow: none;
        }
        input[type="checkbox"]{
            width: 20px;
            height: 20px;
            position: relative;
            left: -10px;
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
                <div class="col-md-3">
                    <asp:Button ID="btnShowInsClienti" OnClick="btnShowInsClienti_Click" CssClass="btn btn-lg btn-default" runat="server" Text="Clienti" />
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnShowInsFornitori" OnClick="btnShowInsFornitori_Click" CssClass="btn btn-lg btn-default" runat="server" Text="Fornitori" />
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnShowInsOperai" OnClick="btnShowInsOperai_Click" CssClass="btn btn-lg btn-default" runat="server" Text="Operai" />
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnShowInsCantieri" OnClick="btnShowInsCantieri_Click" CssClass="btn btn-lg btn-default" runat="server" Text="Cantieri" />
                </div>
            </div>
            <!-- Fine Pulsanti per lo switch dei pannelli -->
        </div>
        <div class="row">
            <!-- Pannello inserimento Clienti -->
            <asp:Panel ID="pnlInsClienti" CssClass="panel-container col-md-12" runat="server" Style="margin-top: 20px;">
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
                    <asp:TextBox ID="txtCap" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-3 form-group">
                    <asp:Label ID="lblCitta" runat="server" Text="Città" />
                    <asp:TextBox ID="txtCitta" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="col-md-3 form-group">
                    <asp:Label ID="lblProvincia" runat="server" Text="Provincia" />
                    <asp:TextBox ID="txtProvincia" CssClass="form-control" runat="server"></asp:TextBox>
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
                    <asp:TextBox ID="txtPartitaIva" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="col-md-3 form-group">
                    <asp:Label ID="lblCodiceFiscale" runat="server" Text="Codice Fiscale" />
                    <asp:TextBox ID="txtCodiceFiscale" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-3 form-group">
                    <asp:Label ID="lblDataInserimento" runat="server" Text="Data Inserimento" />
                    <asp:TextBox ID="txtDataInserimento" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-6 form-group">
                    <asp:Label ID="lblNote" runat="server" Text="Note" />
                    <asp:TextBox ID="txtNote" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="col-md-12 form-group">
                    <asp:Button ID="btnInsCliente" OnClick="btnInsCliente_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Cliente" />
                    <asp:Label ID="lblIsClienteInserito" CssClass="pull-right labelConferma" runat="server" Text=""></asp:Label>
                </div>

                <!-- Griglia di visualizzazione record -->
                <div class="col-md-12 table-container">
                    <asp:GridView ID="grdClienti" AutoGenerateColumns="false"
                        ItemType="GestioneCantieri.Data.Clienti" runat="server" CssClass="table table-striped table-responsive text-center">
                        <Columns>
                            <asp:BoundField HeaderText="Ragione Sociale" DataField="RagSocCli" />
                            <asp:BoundField HeaderText="Indirizzo" DataField="Indirizzo" />
                            <asp:BoundField HeaderText="Cap" DataField="Cap" />
                            <asp:BoundField HeaderText="Città" DataField="Città" />
                            <asp:BoundField HeaderText="Provincia" DataField="Provincia" />
                            <asp:BoundField HeaderText="Telefono" DataField="Tel1" />
                            <asp:BoundField HeaderText="Cellulare" DataField="Cell1" />
                            <asp:BoundField HeaderText="Partita Iva" DataField="PartitaIva" />
                            <asp:BoundField HeaderText="Codice Fiscale" DataField="CodFiscale" />
                            <asp:BoundField HeaderText="Data Inserimento" DataField="Data" DataFormatString="{0:d}" />
                            <asp:BoundField HeaderText="Note" DataField="Note" />
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
                    <asp:TextBox ID="txtCapFornit" CssClass="form-control" runat="server"></asp:TextBox>
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
                    <asp:TextBox ID="txtCodFiscFornit" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="lblPartIvaFornit" runat="server" Text="Partita Iva" />
                    <asp:TextBox ID="txtPartIvaFornit" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-4 form-group">
                    <asp:Label ID="lblAbbrevFornit" runat="server" Text="Abbreviato" />
                    <asp:TextBox ID="txtAbbrevFornit" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="col-md-12 form-group">
                    <asp:Button ID="btnInsFornit" OnClick="btnInsFornit_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Fornitore" />
                    <asp:Label ID="lblIsFornitoreInserito" CssClass="pull-right labelConferma" runat="server" Text=""></asp:Label>
                </div>

                <!-- Griglia di visualizzazione record -->
                <div class="col-md-12 table-container">
                    <asp:GridView ID="grdFornitori" AutoGenerateColumns="false"
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
                <div class="col-md-3 form-group">
                    <asp:Label ID="lblSuffOper" runat="server" Text="Suffisso" />
                    <asp:TextBox ID="txtSuffOper" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-3 form-group">
                    <asp:Label ID="lblOperaio" runat="server" Text="Operaio" />
                    <asp:TextBox ID="txtOperaio" CssClass="form-control" runat="server"></asp:TextBox>
                </div>

                <div class="col-md-12 form-group">
                    <asp:Button ID="btnInsOper" OnClick="btnInsOper_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Operaio" />
                    <asp:Label ID="lblIsOperaioInserito" CssClass="pull-right labelConferma" runat="server" Text=""></asp:Label>
                </div>

                <!-- Griglia di visualizzazione record -->
                <div class="col-md-12 table-container">
                    <asp:GridView ID="grdOperai" AutoGenerateColumns="false"
                        ItemType="GestioneCantieri.Data.Operai" runat="server" CssClass="table table-striped table-responsive text-center">
                        <Columns>
                            <asp:BoundField HeaderText="Nome" DataField="NomeOp" />
                            <asp:BoundField HeaderText="Descrizione" DataField="DescrOp" />
                            <asp:BoundField HeaderText="Suffisso" DataField="Suffisso" />
                            <asp:BoundField HeaderText="Operaio" DataField="Operaio" />
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
            <!-- Fine Pannello inserimento Operai -->

            <!-- Pannello inserimento Cantieri -->
            <asp:Panel ID="pnlInsCantieri" CssClass="panel-container col-md-12" runat="server">
                <div class="col-md-2 form-group">
                    <asp:Label ID="lblScegliCliente" runat="server" Text="Scegli Cliente" />
                    <asp:DropDownList ID="ddlScegliClientePerCantiere" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
                <div class="col-md-2 form-group">
                    <asp:Label ID="lblDataInserimentoCantiere" runat="server" Text="Data Inserimento" />
                    <asp:TextBox ID="txtDataInserCant" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2 form-group">
                    <asp:Label ID="lblCodCant" runat="server" Text="Codice Cantiere" />
                    <asp:TextBox ID="txtCodCant" CssClass="form-control" runat="server"></asp:TextBox>
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
                    <asp:TextBox ID="txtNumeroCant" CssClass="form-control" runat="server"></asp:TextBox>
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
                    <asp:TextBox ID="txtAnnoCant" CssClass="form-control" runat="server"></asp:TextBox>
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

                <div class="col-md-12 form-group">
                    <asp:Button ID="btnInsCantiere" OnClick="btnInsCantiere_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Cantiere" />
                    <asp:Label ID="lblIsCantInserito" CssClass="pull-right labelConferma" runat="server" Text=""></asp:Label>
                </div>

                <!-- Griglia di visualizzazione record -->
                <div class="col-md-12 table-container">
                    <asp:GridView ID="grdCantieri" AutoGenerateColumns="false" OnRowDataBound="grdCantieri_RowDataBound"
                        ItemType="GestioneCantieri.Data.Cantieri" runat="server" CssClass="table table-striped table-responsive text-center">
                        <Columns>
                            <asp:BoundField HeaderText="Cliente" DataField="RagSocCli" />
                            <asp:BoundField HeaderText="Data Inserimento" DataField="Data" DataFormatString="{0:d}" />
                            <asp:BoundField HeaderText="Codice Cantiere" DataField="CodCant" />
                            <asp:BoundField HeaderText="Descrizione Cantiere" DataField="DescriCodCant" />
                            <asp:BoundField HeaderText="Indirizzo" DataField="Indirizzo" />
                            <asp:BoundField HeaderText="Città" DataField="Città" />
                            <asp:BoundField HeaderText="Ricarico" DataField="Ricarico" />
                            <asp:BoundField HeaderText="Prezzo Manodopera" DataField="PzzoManodopera" />
                            <asp:TemplateField HeaderText="Chiuso">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblChiusoYesNo"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Riscosso">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblRiscossoYesNo"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Numero" DataField="Numero" />
                            <asp:BoundField HeaderText="Valore Preventivo" DataField="ValorePreventivo" />
                            <asp:BoundField HeaderText="Iva" DataField="IVA" />
                            <asp:BoundField HeaderText="Anno" DataField="Anno" />
                            <asp:TemplateField HeaderText="Preventivo">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPreventivoYesNo"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Da Dividere">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDaDividereYesNo"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Diviso">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDivisoYesNo"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fatturato">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblFatturatoYesNo"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Fascia Cantiere" DataField="FasciaTblCantieri" />
                        </Columns>
                    </asp:GridView>
                </div>
                <!-- Fine Griglia di visualizzazione record -->
            </asp:Panel>
            <!-- Fine Pannello inserimento Cantieri -->
        </div>
    </div>
</asp:Content>
