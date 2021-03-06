﻿using GestioneCantieri.DAO;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestioneCantieri
{
    public partial class ControlloGruppi : System.Web.UI.Page
    {
        public List<GruppiFrutti> gruppiNonControllatiList = new List<GruppiFrutti>();
        public List<CompGruppoFrut> componentiGruppo = new List<CompGruppoFrut>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                MostraNumeroGruppiNonControllati();
            }

            Page.MaintainScrollPositionOnPostBack = true;
        }

        private void MostraNumeroGruppiNonControllati()
        {
            lblNumGruppiNonControllati.Text = "Numero di gruppi da controllare: " + GestisciGruppiFruttiDAO.GetNumeroGruppiNonControllati().ToString();
        }

        private void BindGrid()
        {
            gruppiNonControllatiList = GestisciGruppiFruttiDAO.getGruppiNonControllati();
            grdFruttiNonControllati.DataSource = gruppiNonControllatiList;
            grdFruttiNonControllati.DataBind();
        }

        protected void MostraComponentiGruppo(int id)
        {
            componentiGruppo = GestisciGruppiFruttiDAO.getCompGruppo(id);
        }

        protected void grdFruttiNonControllati_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName == "MostraCompGruppo")
            {
                MostraComponentiGruppo(id);
            }
            else if (e.CommandName == "ControllaGruppo")
            {
                GestisciGruppiFruttiDAO.UpdateFlagControllato(id);
                BindGrid();
                MostraNumeroGruppiNonControllati();
            }
        }

        protected void grdFruttiNonControllati_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Text = "Completato";
                e.Row.Cells[3].Text = "Controllato";
            }
        }
    }
}