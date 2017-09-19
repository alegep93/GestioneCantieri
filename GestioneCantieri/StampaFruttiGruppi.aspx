<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="StampaFruttiGruppi.aspx.cs" Inherits="GestioneCantieri.StampaFruttiGruppi" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    <title>Stampa Frutti Gruppi</title>
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $(".groupHeader").css("font-weight", "bold")
            $("table.table-striped td:not(.groupHeader)").css("padding-left", 40)
        });
    </script>
    <style>
        .table-container {
            position: relative;
            top: 20px;
        }

            .table-container > div {
                max-height: 500px;
                overflow: hidden;
                overflow-y: auto;
            }

    </style>
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>Stampa Frutti Gruppi</h1>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-6">
                <asp:Label ID="lblScegliGruppo" runat="server" Text="Scegli Gruppo"></asp:Label>
                <asp:DropDownList ID="ddlScegliGruppo" CssClass="form-control" OnTextChanged="ddlScegliGruppo_TextChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
            </div>

            <div class="col-md-6 table-container">
                <asp:GridView ID="grdFruttiInGruppo" AllowSorting="true" OnSorting="grdFruttiInGruppo_Sorting"
                    AutoGenerateColumns="false" ItemType="GestioneCantieri.Data.StampaFruttiPerGruppi"
                    runat="server" CssClass="table table-striped table-responsive">
                    <Columns>
                        <asp:BoundField HeaderText="Nome Gruppo" DataField="NomeGruppo" />
                        <asp:BoundField HeaderText="Nome Gruppo / Nome Frutto" DataField="NomeFrutto" />
                        <asp:BoundField HeaderText="Quantità" DataField="Qta" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
