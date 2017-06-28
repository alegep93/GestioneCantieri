﻿using GestioneCantieri.DAO;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestioneCantieri
{
    public partial class GestionePagamenti : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDdlScegliCant();
                pnlGestPagam.Visible = false;
            }
        }

        /* HELPERS */
        protected void FillDdlScegliCant()
        {
            DataTable dt = CantieriDAO.GetCantieri(txtFiltroCantAnno.Text, txtFiltroCantCodCant.Text, txtFiltroCantDescrCodCant.Text, chkFiltroCantChiuso.Checked, chkFiltroCantRiscosso.Checked);
            List<Cantieri> listCantieri = dt.DataTableToList<Cantieri>();

            ddlScegliCant.Items.Clear();
            ddlScegliCant.Items.Add(new ListItem("", "-1"));

            foreach (Cantieri c in listCantieri)
            {
                string show = c.Anno + " - " + c.CodCant + " - " + c.DescriCodCAnt;
                ddlScegliCant.Items.Add(new ListItem(show, c.IdCantieri.ToString()));
            }
        }

        /* EVENTI CLICK */
        protected void btnFiltroCant_Click(object sender, EventArgs e)
        {
            FillDdlScegliCant();
            pnlGestPagam.Visible = false;
        }
        protected void btnInserisci_Click(object sender, EventArgs e)
        {
            string idCant = ddlScegliCant.SelectedItem.Value;

            /*************************** ????????????????????????? ***************************/
            /*bool isInserito = GestionePagamentiDAO.InserisciPagamento(idCant,
                txtQta.Text, "PAGA", txtCodArt.Text, txtDescriCodArt.Text, txtPzzoUnit.Text);
            if (isInserito)
            {
                lblIsPagamInserito.Text = "Record inserito con successo";
                lblIsPagamInserito.ForeColor = Color.Blue;
            }
            else
            {
                lblIsPagamInserito.Text = "Errore durante l'inserimento del record";
                lblIsPagamInserito.ForeColor = Color.Red;
            }*/
        }

        /* EVENTI TEXT-CHANGED */
        protected void ddlScegliCant_TextChanged(object sender, EventArgs e)
        {
            if (ddlScegliCant.SelectedIndex != 0)
            {
                pnlGestPagam.Visible = true;
            }
            else
            {
                pnlGestPagam.Visible = false;
            }
        }
    }
}