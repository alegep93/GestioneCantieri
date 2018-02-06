using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GestioneCantieri.Data;
using GestioneCantieri.DAO;
using System.Data.OleDb;
using System.Data;

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
            txtMedia.Text = DDTMefDAO.calcolaMediaPrezzoUnitario().ToString("0.00");
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
            string pathFile = @"C:\MEF\ORDINI\D_DDT.DBF";
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
            UpdatePrezzi(ddtList);

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
            txtMedia.Text = DDTMefDAO.calcolaMediaPrezzoUnitario().ToString("0.00");
        }
        protected void BindGridWithSearch()
        {
            List<DDTMef> listaDDT = new List<DDTMef>();
            listaDDT = DDTMefDAO.searchFilter(txtAnnoInizio.Text, txtAnnoFine.Text, txtDataInizio.Text, txtDataFine.Text, txtQta.Text, txtN_DDT.Text,
                                              txtCodArt1.Text, txtCodArt2.Text, txtCodArt3.Text, txtDescriCodArt1.Text,
                                              txtDescriCodArt2.Text, txtDescriCodArt3.Text);
            grdListaDDTMef.DataSource = listaDDT;
            grdListaDDTMef.DataBind();
            txtMedia.Text = DDTMefDAO.calcolaMediaPrezzoUnitarioWithSearch(txtAnnoInizio.Text, txtAnnoFine.Text, txtDataInizio.Text, txtDataFine.Text,
                                                                           txtQta.Text, txtN_DDT.Text, txtCodArt1.Text, txtCodArt2.Text, txtCodArt3.Text,
                                                                           txtDescriCodArt1.Text, txtDescriCodArt2.Text, txtDescriCodArt3.Text).ToString("0.00");
        }

        /*** HELPERS ***/
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
        private void UpdatePrezzi(List<DDTMef> ddtList)
        {
            foreach(DDTMef ddt in ddtList)
            {
                if(ddt.Anno == DateTime.Now.Year && ddt.Data.Month == DateTime.Now.Month)
                {
                    // Aggiorno il prezzo di ogni DDT appartenente al mese e all'anno corrente
                    DDTMefDAO.UpdateDdt(ddt);
                }
            }

        }
    }
}