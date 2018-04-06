<%@ Page Title="" Language="C#" MasterPageFile="~/layout.Master" AutoEventWireup="true" CodeBehind="ControlloGruppi.aspx.cs" Inherits="GestioneCantieri.ControlloGruppi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    <title>Controllo Gruppi</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-md-10">
            <asp:GridView ID="grdFruttiNonControllati" ItemType="GestioneCantieri.Data.GruppiFrutti" AutoGenerateColumns="false" OnRowDataBound="grdFruttiNonControllati_RowDataBound" OnRowCommand="grdFruttiNonControllati_RowCommand" CssClass="table table-striped text-center" runat="server">
                <Columns>
                    <asp:BoundField DataField="NomeGruppo" HeaderText="Nome Gruppo" />
                    <asp:BoundField DataField="Descr" HeaderText="Descrizione" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkGruppoCompletato" Enabled="false" Checked="<%# BindItem.Completato %>" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkGruppoControllato" Enabled="false" Checked="<%# BindItem.Controllato %>" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnMostraCompGruppo" CommandName="MostraCompGruppo" CommandArgument='<%# BindItem.Id %>' CssClass="btn btn-lg btn-default" runat="server" Text="Visualizza Componenti" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnControllaGruppo" CommandName="ControllaGruppo" CommandArgument="<%# BindItem.Id %>" CssClass="btn btn-lg btn-default" runat="server" Text="Controllato" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <div class="col-md-2 compGruppoFixed">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <asp:Label ID="lblPanelTitleGroupName" runat="server"></asp:Label></h3>
                </div>
                <div class="panel-body">
                    <asp:Label ID="lblGruppiNonControllati" runat="server" Text="Componenti Gruppo"></asp:Label>
                    <ul class="list-group">
                        <% foreach (var item in componentiGruppo)
                            {%>
                        <li class="list-group-item"><%= item.Qta + " - " + item.NomeFrutto %></li>
                        <%} %>
                    </ul>
                </div>
                <div class="panel-footer">
                    <asp:Label ID="lblNumGruppiNonControllati" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
