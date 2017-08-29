<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="GestionePagamenti.aspx.cs" Inherits="GestioneCantieri.GestionePagamenti" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    <title>Gestione Pagamenti</title>
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

        select, input {
            font-size: 20px !important;
            min-height: 40px;
        }

        h2 {
            margin-top: 0 !important;
        }
    </style>
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>Gestione Pagamenti</h1>
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
    </asp:Panel>

    <!-- Maschera Gestione Pagamenti-->
    <asp:Panel ID="pnlGestPagam" CssClass="col-md-12" runat="server">
        <div class="row">
            <!-- Titolo della maschera -->
            <div class="col-md-12 text-center">
                <h2>
                    <asp:Label ID="lblTitoloMaschera" runat="server" Text=""></asp:Label>
                </h2>
            </div>

            <!-- Data -->
            <div class="col-md-offset-5 col-md-2">
                <asp:Label ID="lblDataDDT" Text="Data DDT" runat="server" />
                <asp:TextBox ID="txtDataDDT" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <!-- Campi per l'inserimento dei valori -->
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
                    <asp:CheckBox ID="chkAcconto" CssClass="form-control" runat="server" />
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblSaldo" Text="Saldo" runat="server" />
                    <asp:CheckBox ID="chkSaldo" CssClass="form-control" runat="server" />
                </div>
                <div class="col-md-8">
                    <asp:Button ID="btnInsPagam" OnClick="btnInsPagam_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Inserisci Pagamento" />
                    <asp:Button ID="btnModPagam" OnClick="btnModPagam_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Modifica Pagamento" />
                    <asp:Label ID="lblIsPagamInserito" Text="" CssClass="pull-right" runat="server" />
                </div>
            </div>
        </div>

        <asp:HiddenField ID="hidPagamenti" runat="server" />

        <asp:Panel ID="pnlFiltriGrdPagam" CssClass="col-md-12" runat="server">
            <div class="col-md-offset-3 col-md-6">
                <div class="col-md-3">
                    <asp:Label ID="lblFiltroPagamDescri" runat="server" Text="Filtro Descrizione"></asp:Label>
                    <asp:TextBox ID="txtFiltroPagamDescri" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnFiltraPagam" OnClick="btnFiltraPagam_Click" CssClass="btn btn-lg btn-primary pull-right" runat="server" Text="Filtra Record" />
                </div>
            </div>
        </asp:Panel>

        <div class="col-md-12 table-responsive tableContainer">
            <asp:GridView ID="grdPagamenti" ItemType="GestioneCantieri.Data.Pagamenti" AutoGenerateColumns="false" OnRowCommand="grdPagamenti_RowCommand" CssClass="table table-striped text-center" runat="server">
                <Columns>
                    <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:d}" ApplyFormatInEditMode="True" />
                    <asp:BoundField DataField="Imporo" HeaderText="Importo" DataFormatString="{0:0.00}" />
                    <asp:BoundField DataField="DescriPagamenti" HeaderText="Descriz. Pagam." />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnVisualPagam" CommandName="VisualPagam" CommandArgument="<%# BindItem.IdPagamenti %>" CssClass="btn btn-lg btn-default" runat="server" Text="Visualizza" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnModPagam" CommandName="ModPagam" CommandArgument="<%# BindItem.IdPagamenti %>" CssClass="btn btn-lg btn-default" runat="server" Text="Modifica" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnElimPagam" CommandName="ElimPagam" CommandArgument="<%# BindItem.IdPagamenti %>"
                                CssClass="btn btn-lg btn-default" runat="server" Text="Elimina" OnClientClick="return confirm('Vuoi veramente eliminare questo record?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>
</asp:Content>
