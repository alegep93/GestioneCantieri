using GestioneCantieri.DAO;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace GestioneCantieri
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        /*** INIZIO EVENTI CLICK ***/
        protected void btnSvuotaTxt_Click(object sender, EventArgs e)
        {
            txtAnnoInizio.Text = "";
            txtAnnoFine.Text = "";
            txtDataInizio.Text = "";
            txtDataFine.Text = "";
            txtQta.Text = "";
            txtN_DDT.Text = "";
            txtCodArt1.Text = "";
            txtCodArt2.Text = "";
            txtCodArt3.Text = "";
            txtDescriCodArt1.Text = "";
            txtDescriCodArt2.Text = "";
            txtDescriCodArt3.Text = "";
            BindGrid();
            GetBasicValuesForRecap();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtAnnoInizio.Text != "" || txtAnnoFine.Text != "")
            {
                txtDataInizio.Text = "";
                txtDataFine.Text = "";
            }
            else if (txtDataInizio.Text != "" || txtDataFine.Text != "")
            {
                txtAnnoInizio.Text = "";
                txtAnnoFine.Text = "";
            }
            BindGridWithSearch();
        }
        protected void btn_GeneraDdtDaDbf_Click(object sender, EventArgs e)
        {
            string pathFile = "";

            pathFile = @"C:\MEF\ORDINI\";

            int idFornitore = FornitoriDAO.GetIdFornitore("Mef");

            //spinnerImg.Visible = true;

            // Genero una lista a partire dai dati contenuti nel nuovo file DBF
            List<DDTMef> ddtList = DDTMefDAO.GetDdtFromDBF(pathFile, txtAcquirente.Text, idFornitore);

            // Popolo la tabella temporanea
            InsertIntoDdtTemp(ddtList);

            //Prendo la lista dei DDT non presenti sulla tabella TblDDTMef
            List<DDTMef> ddtMancanti = DDTMefDAO.GetNewDDT();

            foreach (DDTMef ddt in ddtMancanti)
            {
                // Inserisco i nuovi DDT
                DDTMefDAO.InsertNewDdt(ddt);
            }

            //Aggiorno i prezzi del mese corrente
            UpdatePrezzi();

            BindGrid();
        }

        /*** INIZIO EVENTI GRIDVIEW ***/
        protected void grdListaDDTMef_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void grdListaDDTMef_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void grdListaDDTMef_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdListaDDTMef.PageIndex = e.NewPageIndex;
            if (txtAnnoInizio.Text == "" && txtAnnoFine.Text == "" && txtDataInizio.Text == "" && txtDataFine.Text == "" &&
                txtCodArt1.Text == "" && txtCodArt2.Text == "" && txtCodArt3.Text == "" &&
                txtDescriCodArt1.Text == "" && txtDescriCodArt3.Text == "" && txtDescriCodArt3.Text == "")
                BindGrid();
            else
                BindGridWithSearch();
        }

        /*** INIZIO METODI PER AGGIORNAMENTO GRIDVIEW ***/
        protected void BindGrid()
        {
            List<DDTMef> listaDDT = new List<DDTMef>();
            listaDDT = DDTMefDAO.getDDTList();
            grdListaDDTMef.DataSource = listaDDT;
            grdListaDDTMef.DataBind();
            GetBasicValuesForRecap();
        }

        private void GetBasicValuesForRecap()
        {
            txtMedia.Text = DDTMefDAO.calcolaMediaPrezzoUnitario().ToString("0.00") + " €";
            txtTotDDT.Text = DDTMefDAO.GetTotalDDT().ToString("N2") + " €";
            txtImponibileDDT.Text = DDTMefDAO.GetImponibileDDT().ToString("N2") + " €";
            txtIvaDDT.Text = DDTMefDAO.GetIvaDDT().ToString("N2") + " €";
        }

        protected void BindGridWithSearch()
        {
            List<DDTMef> listaDDT = new List<DDTMef>();
            DDTMefObject ddt = FillDdtObject();

            // Rigenero la griglia
            listaDDT = DDTMefDAO.searchFilter(ddt);
            grdListaDDTMef.DataSource = listaDDT;
            grdListaDDTMef.DataBind();

            //Rigenero il valore della media dei prezzi unitari
            ddt = FillDdtObject();
            txtMedia.Text = DDTMefDAO.calcolaMediaPrezzoUnitarioWithSearch(ddt).ToString("0.00") + " €";
            ddt = FillDdtObject();
            txtTotDDT.Text = DDTMefDAO.GetTotalDDT(ddt).ToString("N2") + " €";
            ddt = FillDdtObject();
            txtImponibileDDT.Text = DDTMefDAO.GetImponibileDDT(ddt).ToString("N2") + " €";
            ddt = FillDdtObject();
            txtIvaDDT.Text = DDTMefDAO.GetIvaDDT(ddt).ToString("N2") + " €";
        }

        /*** HELPERS ***/
        protected DDTMefObject FillDdtObject()
        {
            DDTMefObject ddt = new DDTMefObject();
            ddt.AnnoInizio = txtAnnoInizio.Text;
            ddt.AnnoFine = txtAnnoFine.Text;
            ddt.DataInizio = txtDataInizio.Text;
            ddt.DataFine = txtDataFine.Text;
            ddt.Qta = txtQta.Text;
            ddt.NDdt = txtN_DDT.Text;
            ddt.CodArt1 = txtCodArt1.Text;
            ddt.CodArt2 = txtCodArt2.Text;
            ddt.CodArt3 = txtCodArt3.Text;
            ddt.DescriCodArt1 = txtDescriCodArt1.Text;
            ddt.DescriCodArt2 = txtDescriCodArt2.Text;
            ddt.DescriCodArt3 = txtDescriCodArt3.Text;
            return ddt;
        }
        protected void FillDdlClienti()
        {
            List<Fornitori> listClienti = FornitoriDAO.GetFornitori();

            ddlFornitore.Items.Clear();
            ddlFornitore.Items.Add(new ListItem("", "-1"));

            foreach (Fornitori f in listClienti)
                ddlFornitore.Items.Add(new ListItem(f.RagSocForni, f.IdFornitori.ToString()));
        }
        protected void InsertIntoDdtTemp(List<DDTMef> ddtList)
        {
            // Svuoto la tabella DDTMefTemp
            DDTMefDAO.DeleteFromDdtTemp();

            // Per ogni elemento della lista
            foreach (DDTMef ddt in ddtList)
            {
                // Popolo la tabella temporanea con i nuovi dati
                DDTMefDAO.InsertIntoDdtTemp(ddt);
            }
        }
        private void UpdatePrezzi()
        {
            DDTMefDAO.UpdateDdt();
        }
    }
}