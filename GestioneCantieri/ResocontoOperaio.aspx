<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/layout.Master" CodeBehind="ResocontoOperaio.aspx.cs" Inherits="GestioneCantieri.ResocontoOperaio" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    <title>Resoconto Operaio</title>
    <style>
        span.pull-right {
            position: relative;
            top: 10px;
            right: 10px;
            font-size: 20px;
        }

        input[type="checkbox"] {
            width: 20px;
            height: 20px;
            position: relative;
            left: 0px !important;
        }

        .tableContainer {
            max-height: 500px;
            overflow: hidden;
            overflow-y: scroll;
        }

        span.lblTotali {
            font-size: 30px;
            font-style: italic;
        }

        .btnPagaOperaio {
            position: relative;
            right: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>Resoconto Operaio</h1>
    <div class="container-fluid">
        <div class="row">
            <asp:Panel ID="pnlResocontoOperaio" DefaultButton="btnStampaResoconto" runat="server">
                <div class="col-md-4">
                    <asp:Label ID="lblDataDa" runat="server" Text="Data Da:"></asp:Label>
                    <asp:TextBox ID="txtDataDa" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="lblDataA" runat="server" Text="Data A:"></asp:Label>
                    <asp:TextBox ID="txtDataA" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="lblScegliOperaio" runat="server" Text="Scegli Operaio"></asp:Label>
                    <asp:DropDownList ID="ddlScegliOperaio" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
                <div class="col-md-12">
                    <asp:Button ID="btnStampaResoconto" CssClass="btn btn-lg btn-primary pull-right" OnClick="btnStampaResoconto_Click" runat="server" Text="Stampa Resoconto" />
                    <asp:Button ID="btnPagaOperaio" CssClass="btn btn-lg btn-primary pull-right btnPagaOperaio" OnClick="btnPagaOperaio_Click" runat="server" Text="Paga Operaio" />
                    <asp:Label ID="lblIsOperaioPagato" runat="server" Text=""></asp:Label>
                </div>
            </asp:Panel>
        </div>

        <div class="row filtriResocontoOperaio">
            <asp:Panel ID="pnlFiltri" DefaultButton="btnFiltra" CssClass="col-md-offset-3 col-md-6" runat="server">
                <div class="col-md-8">
                    <asp:Label ID="lblFiltroCantiere" Text="Filtra Cantiere" runat="server"></asp:Label>
                    <asp:TextBox ID="txtFiltroCantiere" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblFiltroOperaioPagato" Text="Operaio Pagato" runat="server"></asp:Label>
                    <asp:CheckBox ID="ChkFiltroOperaioPagato" CssClass="form-control" runat="server"></asp:CheckBox>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnFiltra" Text="Filtra" CssClass="btn btn-lg btn-primary" OnClick="btnFiltra_Click" runat="server"></asp:Button>
                </div>
            </asp:Panel>
        </div>

        <div class="row">
            <div class="col-md-12 tableContainer table-responsive">
                <asp:GridView ID="grdResocontoOperaio" runat="server" ItemType="GestioneCantieri.Data.MaterialiCantieri" AutoGenerateColumns="False" CssClass="table table-striped text-center">
                    <Columns>
                        <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:d}" ApplyFormatInEditMode="True" />
                        <asp:BoundField DataField="Acquirente" HeaderText="Acquirente" />
                        <asp:BoundField DataField="CodCant" HeaderText="CodCant" />
                        <asp:BoundField DataField="DescriCodCant" HeaderText="DescriCodCant" />
                        <asp:BoundField DataField="Qta" HeaderText="Qta" />
                        <asp:BoundField DataField="PzzoUniCantiere" HeaderText="Pzzo Unit. Cant" DataFormatString="{0:0.00}" />
                        <asp:BoundField DataField="Valore" HeaderText="Valore" />
                        <asp:BoundField DataField="OperaioPagato" HeaderText="Operaio Pagato" />
                    </Columns>
                </asp:GridView>
            </div>
            <div class="col-md-offset-3 col-md-6 text-center">
                <asp:Label ID="lblTotali" runat="server" Text="" CssClass="lblTotali"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
