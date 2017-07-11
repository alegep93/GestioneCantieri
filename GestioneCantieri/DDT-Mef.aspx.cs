using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GestioneCantieri.Data;
using GestioneCantieri.DAO;

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
            listaDDT = DDTMefDAO.searchFilter(txtAnnoInizio.Text, txtAnnoFine.Text, txtDataInizio.Text, txtDataFine.Text, txtQta.Text,
                                              txtCodArt1.Text, txtCodArt2.Text, txtCodArt3.Text, txtDescriCodArt1.Text, txtDescriCodArt2.Text, txtDescriCodArt3.Text);
            grdListaDDTMef.DataSource = listaDDT;
            grdListaDDTMef.DataBind();
            txtMedia.Text = DDTMefDAO.calcolaMediaPrezzoUnitarioWithSearch(txtAnnoInizio.Text, txtAnnoFine.Text, txtDataInizio.Text, txtDataFine.Text, 
                                                                           txtCodArt1.Text, txtCodArt2.Text, txtCodArt3.Text,
                                                                           txtDescriCodArt1.Text, txtDescriCodArt2.Text, txtDescriCodArt3.Text).ToString("0.00");
        }
    }
}