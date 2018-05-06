<%@ Page Title="" Language="C#" MasterPageFile="~/layout.Master" AutoEventWireup="true" CodeBehind="DDT-Fornitori.aspx.cs" Inherits="GestioneCantieri.DDT_Fornitori" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    <title>DDT Fornitori</title>
    <style>
        .btn.btn-lg.btn-primary {
            position: relative;
            top: 13px;
            width: 20%;
        }

        .errorLabel {
            position: relative;
            top: 13px;
            left: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <h1>DDT Fornitori</h1>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-2">
                    <asp:Label ID="lblScegliFornitore" Text="Scegli Fornitore" runat="server"></asp:Label>
                    <asp:DropDownList ID="ddlScegliFornitore" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblInsData" Text="Data" runat="server"></asp:Label>
                    <asp:TextBox ID="txtInsData" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblInsProtocollo" Text="Protocollo" runat="server"></asp:Label>
                    <asp:TextBox ID="txtInsProtocollo" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblInsNumeroDdt" Text="Numero DDT" runat="server"></asp:Label>
                    <asp:TextBox ID="txtInsNumeroDdt" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblInsArticolo" Text="Articolo" runat="server"></asp:Label>
                    <asp:TextBox ID="txtInsArticolo" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblInsDescrForn" Text="Descr. Fornitore" runat="server"></asp:Label>
                    <asp:TextBox ID="txtInsDescrForn" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblInsDescrMau" Text="Descr. Mau" runat="server"></asp:Label>
                    <asp:TextBox ID="txtInsDescrMau" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblInsQta" Text="Quantità" runat="server"></asp:Label>
                    <asp:TextBox ID="txtInsQta" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblInsPrezzoUnit" Text="Prezzo Unitario" runat="server"></asp:Label>
                    <asp:TextBox ID="txtInsPrezzoUnit" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <asp:Button ID="btnInserisciDDT" CssClass="btn btn-lg btn-primary" OnClick="btnInserisciDDT_Click" Text="Inserisci" runat="server"></asp:Button>
                    <asp:Button ID="btnModificaDDT" CssClass="btn btn-lg btn-primary" OnClick="btnModificaDDT_Click" Text="Modifica" runat="server"></asp:Button>
                    <asp:Label ID="lblError" Text="" CssClass="errorLabel" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfIdDDT" runat="server" />
        <div class="row">
            <div class="col-md-12 tableContainer">
                <asp:GridView ID="grdListaDDTFornitori" runat="server" ItemType="GestioneCantieri.Data.DDTFornitori" AutoGenerateColumns="False" OnRowCommand="grdListaDDTFornitori_RowCommand" CssClass="table table-striped table-responsive text-center">
                    <Columns>
                        <asp:BoundField DataField="IdFornitore" HeaderText="Fornitore" />
                        <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:d}" ApplyFormatInEditMode="True" />
                        <asp:BoundField DataField="Protocollo" HeaderText="Protocollo" />
                        <asp:BoundField DataField="NumeroDdt" HeaderText="Numero DDT" />
                        <asp:BoundField DataField="Articolo" HeaderText="Articolo" />
                        <asp:BoundField DataField="DescrizioneFornitore" HeaderText="Descrizione Fornitore" />
                        <asp:BoundField DataField="DescrizioneMau" HeaderText="Descrizione Mau" />
                        <asp:BoundField DataField="Qta" HeaderText="Quantità" />
                        <asp:BoundField DataField="PrezzoUnitario" HeaderText="Prezzo Unitario" DataFormatString="{0:0.00}" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnMod" CommandName="ModDDT" CommandArgument="<%# BindItem.Id %>" CssClass="btn btn-lg btn-default" runat="server" Text="Modifica" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnElim" CommandName="ElimDDT" CommandArgument="<%# BindItem.Id %>"
                                    CssClass="btn btn-lg btn-default" runat="server" Text="Elimina" OnClientClick="return confirm('Vuoi veramente eliminare questo DDT Fornitore?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
