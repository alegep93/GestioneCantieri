<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="DDT-Mef.aspx.cs" Inherits="GestioneCantieri.Default" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    <title>Gestione DDT Mef</title>
    <style>
        .tableContainer {
            max-height: 500px;
            overflow: hidden;
            overflow-y: auto;
        }

        #body_spinnerImg {
            width: 20px;
            height: auto;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#body_spinnerImg").hide();
        });

        function ShowHideLoader() {
            $("#body_spinnerImg").show();
        }
    </script>
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>DDT Mef</h1>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12 text-center">
                <div class="col-md-offset-1 col-md-10">
                    <div class="col-md-2">
                        <%--<h1 id="filterToggle" class="btn btn-default btn-lg"></h1>--%>
                        <asp:Label ID="lblAcquirente" runat="server" Text="Acquirente"></asp:Label>
                        <asp:TextBox ID="txtAcquirente" CssClass="form-control" runat="server" Text="Mau"></asp:TextBox><br />
                        <asp:Label ID="lblFornitore" runat="server" Text="Fornitore"></asp:Label>
                        <asp:DropDownList ID="ddlFornitore" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btn_GeneraDdtDaDbf" class="btn btn-info btn-lg" OnClick="btn_GeneraDdtDaDbf_Click" OnClientClick="javascript:ShowHideLoader()" Text="Importa DBF" runat="server" />
                        <img id="spinnerImg" src="Images/spinner.gif" alt="spinner" runat="server" />
                    </div>
                    <div class="col-md-8 recapContainer">
                        <div class="col-md-12">
                            <!-- Media prezzo unitario -->
                            <div class="col-md-4">
                                <asp:Label ID="lblMedia" runat="server" Text="Media Prezzo Unitario"></asp:Label>
                                <asp:TextBox ID="txtMedia" Enabled="false" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <!-- Imponibile,Iva,Totale DDT -->
                            <div class="col-md-4">
                                <asp:Label ID="lblImponibileDDT" runat="server" Text="Imponibile DDT"></asp:Label>
                                <asp:TextBox ID="txtImponibileDDT" Enabled="false" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:Label ID="lblIvaDDT" runat="server" Text="Iva DDT"></asp:Label>
                                <asp:TextBox ID="txtIvaDDT" Enabled="false" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:Label ID="lblTotDDT" runat="server" Text="Totale DDT"></asp:Label>
                                <asp:TextBox ID="txtTotDDT" Enabled="false" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Panel ID="pnlFiltriDDT" DefaultButton="btnSearch" runat="server">
                <div id="filterContainer">
                    <!-- Ricerca Per Anno -->
                    <div class="searchFilterContainer col-md-2">
                        <asp:Label ID="lblCercaAnno" runat="server" Text="Cerca per anno"></asp:Label>
                        <div class="col-md-12">
                            <asp:Label ID="lblAnnoInizio" runat="server" Text="Anno Iniziale"></asp:Label>
                            <asp:TextBox ID="txtAnnoInizio" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <asp:Label ID="lblAnnoFine" runat="server" Text="Anno Finale"></asp:Label>
                            <asp:TextBox ID="txtAnnoFine" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Ricerca Per Data -->
                    <div class="searchFilterContainer col-md-2">
                        <asp:Label ID="lblCercaData" runat="server" Text="Cerca per data"></asp:Label>
                        <div class="col-md-12">
                            <asp:Label ID="lblDataInizio" runat="server" Text="Data Inizio"></asp:Label>
                            <asp:TextBox ID="txtDataInizio" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <asp:Label ID="lblDataFine" runat="server" Text="Data Fine"></asp:Label>
                            <asp:TextBox ID="txtDataFine" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Ricerca Per Quantità -->
                    <div class="searchFilterContainer col-md-3">
                        <asp:Label ID="lblCercaQta" runat="server" Text="Cerca per Qta o N_DDT"></asp:Label>
                        <div class="col-md-12">
                            <asp:Label ID="lblQta" runat="server" Text="Quantità"></asp:Label>
                            <asp:TextBox ID="txtQta" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <asp:Label ID="lblN_DDT" runat="server" Text="N_DDT"></asp:Label>
                            <asp:TextBox ID="txtN_DDT" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Ricerca Per CodArt e DescriCodArt -->
                    <div class="searchFilterContainer col-md-3">
                        <asp:Label ID="Label1" runat="server" Text="CodArt & DescriCodArt"></asp:Label>
                        <div class="col-md-12">
                            <asp:Label ID="lblCercaCodArt" runat="server" Text="Cerca per codice articolo"></asp:Label>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtCodArt1" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtCodArt2" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtCodArt3" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:Label ID="lblCercaDescriCodArt" runat="server" Text="Cerca per Descrizione Cod. Art."></asp:Label>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtDescriCodArt1" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtDescriCodArt2" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtDescriCodArt3" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <!-- Svuota e Media -->
                    <div class="searchFilterContainer col-md-2 text-center">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" CssClass="btn btn-primary btn-lg" Text="Cerca" />
                        </div>
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSvuotaTxt" runat="server" OnClick="btnSvuotaTxt_Click" Text="Svuota Caselle di Testo" CssClass="btn btn-default btn-lg" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            <div class="col-md-12 tableContainer">
                <asp:GridView ID="grdListaDDTMef" runat="server" OnRowDataBound="grdListaDDTMef_RowDataBound" ItemType="GestioneCantieri.Data.DDTMef"
                    AutoGenerateColumns="False" OnRowCommand="grdListaDDTMef_RowCommand"
                    PageSize="20" OnPageIndexChanging="grdListaDDTMef_PageIndexChanging" CssClass="table table-striped table-responsive text-center">
                    <Columns>
                        <%--<asp:BoundField DataField="Id" HeaderText="ID DDT Mef" />--%>
                        <asp:BoundField DataField="Anno" HeaderText="Anno" />
                        <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:d}" ApplyFormatInEditMode="True" />
                        <asp:BoundField DataField="N_ddt" HeaderText="N_DDT" />
                        <asp:BoundField DataField="CodArt" HeaderText="Codice Articolo" />
                        <asp:BoundField DataField="DescriCodArt" HeaderText="Descrizione Codice Articolo" />
                        <asp:BoundField DataField="Qta" HeaderText="Quantità" />
                        <asp:BoundField DataField="Importo" HeaderText="Importo" DataFormatString="{0:0.00}" />
                        <asp:BoundField DataField="Acquirente" HeaderText="Acquirente" />
                        <asp:BoundField DataField="PrezzoUnitario" HeaderText="Prezzo Unitario" DataFormatString="{0:0.00}" />
                        <asp:BoundField DataField="AnnoN_ddt" HeaderText="Anno N_DDT" />
                    </Columns>
                    <PagerSettings Mode="Numeric" Position="Bottom" PageButtonCount="20" />
                    <PagerStyle ForeColor="#333" BorderWidth="0" BorderColor="Transparent" BorderStyle="None" CssClass="text-center pagination-container" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
