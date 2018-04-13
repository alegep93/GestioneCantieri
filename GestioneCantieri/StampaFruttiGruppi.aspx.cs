using GestioneCantieri.DAO;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace GestioneCantieri
{
    public partial class StampaFruttiGruppi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDdlScegliGruppo();
                BindGrid();
                GroupGridViewCells();
            }
        }

        /* EVENTI TEXT-CHANGED */
        protected void ddlScegliGruppo_TextChanged(object sender, EventArgs e)
        {
            BindGrid();
            GroupGridViewCells();
        }

        /* HELPERS */
        protected void BindGrid()
        {
            List<StampaFruttiPerGruppi> listFrutti = new List<StampaFruttiPerGruppi>();

            if (ddlScegliGruppo.SelectedIndex != 0)
                listFrutti = CompGruppoFrutDAO.GetFruttiInGruppi(ddlScegliGruppo.SelectedItem.Value);
            else
                listFrutti = CompGruppoFrutDAO.GetFruttiInGruppi(null);

            grdFruttiInGruppo.DataSource = listFrutti;
            grdFruttiInGruppo.DataBind();
        }
        protected void FillDdlScegliGruppo()
        {
            List<GruppiFrutti> listGruppi = GruppiFruttiDAO.getGruppi();

            ddlScegliGruppo.Items.Clear();
            ddlScegliGruppo.Items.Add(new ListItem("", "-1"));

            foreach (GruppiFrutti g in listGruppi)
                ddlScegliGruppo.Items.Add(new ListItem(g.NomeGruppo, g.Id.ToString()));
        }
        protected void GroupGridViewCells()
        {
            GridViewHelper helper = new GridViewHelper(grdFruttiInGruppo);
            helper.RegisterGroup("NomeGruppo", true, true);
            helper.ApplyGroupSort();
        }

        /* Necessario per la creazione della GridView con intestazioni dinamiche */
        /* Definisce l'ordinamento dei dati presenti nella GridView */
        protected void grdFruttiInGruppo_Sorting(object sender, GridViewSortEventArgs e)
        {
            BindGrid();
        }
    }
}