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
    public partial class Gestione_Arrotondamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDdlScegliCant();
                pnlGestArrotond.Visible = false;
            }
        }

        /* HELPERS */
        protected void FillDdlScegliCant()
        {
            DataTable dt = GestioneArrotondamentoDAO.GetCantieri(txtFiltroCantAnno.Text, txtFiltroCantCodCant.Text, txtFiltroCantDescrCodCant.Text, chkFiltroCantChiuso.Checked, chkFiltroCantRiscosso.Checked);
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
            pnlGestArrotond.Visible = false;
        }
        protected void btnInserisci_Click(object sender, EventArgs e)
        {
            string idCant = ddlScegliCant.SelectedItem.Value;

            if (Convert.ToInt32(txtQta.Text) > 0)
            {
                bool isInserito = GestioneArrotondamentoDAO.InserisciArrotondamento(idCant, txtQta.Text, txtCodArt.Text, txtDescriCodArt.Text, txtPzzoUnit.Text);
                if (isInserito)
                {
                    lblIsArrotondInserito.Text = "Record inserito con successo";
                    lblIsArrotondInserito.ForeColor = Color.Blue;
                }
                else
                {
                    lblIsArrotondInserito.Text = "Errore durante l'inserimento del record";
                    lblIsArrotondInserito.ForeColor = Color.Red;
                }
            }
        }

        /* EVENTI TEXT-CHANGED */
        protected void ddlScegliCant_TextChanged(object sender, EventArgs e)
        {
            if (ddlScegliCant.SelectedIndex != 0)
            {
                pnlGestArrotond.Visible = true;
            }
            else
            {
                pnlGestArrotond.Visible = false;
            }
        }
    }
}