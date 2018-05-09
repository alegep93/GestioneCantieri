<%@ Page Title="" Language="C#" MasterPageFile="~/layout.Master" AutoEventWireup="true" CodeBehind="GestioneLocali.aspx.cs" Inherits="GestioneCantieri.GestioneLocali" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    <title>Gestione Locali</title>
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

        table#body_grdLocali {
            text-align: center;
        }

            table#body_grdLocali tr td:first-child {
                width: 900px;
            }
    </style>
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>Gestione Locali</h1>
    <div class="container-fluid">
        <asp:HiddenField ID="hfIdLocale" runat="server" />
        <div class="row">
            <div class="col-md-offset-3 col-md-6">
                <asp:Label ID="lblNomeLocale" Text="Nome Locale" runat="server"></asp:Label>
                <asp:TextBox ID="txtNomeLocale" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:Button ID="btnInserisciLocale" CssClass="btn btn-lg btn-primary pull-right" OnClick="btnInserisciLocale_Click" Text="Inserisci Locale" runat="server" />
                <asp:Button ID="btnModificaLocale" CssClass="btn btn-lg btn-primary pull-right" OnClick="btnModificaLocale_Click" Text="Modifica Locale" runat="server" />
                <asp:Label ID="lblError" Text="" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-offset-2 col-md-8 table-container">
                <asp:GridView ID="grdLocali" AutoGenerateColumns="false" OnRowCommand="grdLocali_RowCommand" ItemType="GestioneCantieri.Data.Locali" runat="server" CssClass="table table-striped table-responsive">
                    <Columns>
                        <asp:BoundField HeaderText="Nome Locale" DataField="NomeLocale" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnModificaLocale" CommandName="ModificaLocale" CommandArgument="<%# BindItem.Id %>" CssClass="btn btn-lg btn-default" runat="server" Text="Modifica" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnElimLocale" CommandName="EliminaLocale" CommandArgument="<%# BindItem.Id %>"
                                    CssClass="btn btn-lg btn-default" runat="server" Text="Elimina" OnClientClick="return confirm('Vuoi veramente eliminare questo locale?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
