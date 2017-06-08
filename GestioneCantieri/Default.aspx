<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="Default.aspx.cs" Inherits="GestioneCantieri.Default1" %>

<asp:Content ID="head" ContentPlaceHolderID="title" runat="server">
    <title>Home Page</title>
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <div class="container-fluid">
        <div class="row">
            <!-- Riga 1 -->
            <div class="col-md-4">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><a href="DDT-Mef.aspx">DDT Mef</a></h3>
                    </div>
                    <div class="panel-body">
                        Mostra la lista dei DDT presenti alla Mef.
                        <br />
                        E' possibile filtrare i risultati in base a vari campi
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><a href="Listino.aspx">Listino</a></h3>
                    </div>
                    <div class="panel-body">
                        Mostra il listino MEF, preso dalla tabella Mamg0.
                        <br />
                        E' possibile filtrare i risultati in base a codice e descrizione articolo.
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><a href="GestisciGruppiFrutti.aspx">Gestisci Gruppi Frutti</a></h3>
                    </div>
                    <div class="panel-body">
                        Consente di possibile creare / modificare / eliminare un gruppo. E' anche possibile aggiungere/eliminare frutti ai vari gruppi
                    </div>
                </div>
            </div>

            <!-- Riga 2 -->
            <div class="col-md-4">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><a href="GestisciFrutti.aspx">Gestisci Frutti</a></h3>
                    </div>
                    <div class="panel-body">
                        Consente di creare / modificare / eliminare un frutto dall'elenco.
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><a href="OrdineFrutti.aspx">Ordine Frutti</a></h3>
                    </div>
                    <div class="panel-body">
                        Questa sezione consente di aggiungere un gruppo di frutti al materiale necessario per ogni cantiere, suddiviso per locale.
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><a href="StampaOrdFrutCantExcel.aspx">Stampa Ord Frut Cant Excel</a></h3>
                    </div>
                    <div class="panel-body">
                        Dopo aver scelto un cantiere, visualizza i frutti necessari. Inoltre è possibile creare un foglio excel a partire da tali dati
                    </div>
                </div>
            </div>

            <!-- Riga 3-->
            <div class="col-md-4">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><a href="StampaOrdFrutLocale.aspx">Stampa Ord Frut Loc</a></h3>
                    </div>
                    <div class="panel-body">
                        E' possibile visualizzare, a partire da un cantiere, i gruppi contenuti in un locale (divisi per nome locale)
                        e il numero di frutti in tale cantiere, raggruppati per nome frutto.
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><a href="StampaFruttiGruppi.aspx">Stampa Frutti Gruppi</a></h3>
                    </div>
                    <div class="panel-body">
                        Visualizza i frutti contenuti nei gruppi, divisi per nome del gruppo.
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><a href="InserimentoDati.aspx">Inserimento Dati</a></h3>
                    </div>
                    <div class="panel-body">
                        Consente l'inserimento, la modifica, l'eliminazione e la visualizzazione di Clienti / Fornitori / Operai / Cantieri.
                    </div>
                </div>
            </div>
        </div>
        <!-- Fine div.row -->
    </div>
    <!-- Fine div.container-fluid -->
</asp:Content>
