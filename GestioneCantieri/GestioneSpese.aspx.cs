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
    public partial class GestioneSpese : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillAllDdl();
                pnlSubIntestazione.Visible = false;
            }
        }

        /* HELPERS */
        //Fill Ddl
        protected void FillDdlScegliCant()
        {
            DataTable dt = GestioneSpeseDAO.GetCantieri(txtFiltroCantAnno.Text, txtFiltroCantCodCant.Text, txtFiltroCantDescrCodCant.Text, chkFiltroCantChiuso.Checked, chkFiltroCantRiscosso.Checked);
            List<Cantieri> listCantieri = dt.DataTableToList<Cantieri>();

            ddlScegliCant.Items.Clear();
            ddlScegliCant.Items.Add(new ListItem("", "-1"));

            foreach (Cantieri c in listCantieri)
            {
                string show = c.Anno + " - " + c.CodCant + " - " + c.DescriCodCAnt;
                ddlScegliCant.Items.Add(new ListItem(show, c.IdCantieri.ToString()));
            }
        }
        protected void FillDdlScegliAcquirente()
        {
            int i = 0;
            DataTable dt = GestioneCantieriDAO.GetOperai();
            List<Operai> listOperai = dt.DataTableToList<Operai>();

            ddlScegliSpesa.Items.Clear();
            ddlScegliSpesa.Items.Add(new ListItem("", "-1"));

            foreach (Operai op in listOperai)
            {
                string show = op.NomeOp + " - " + op.DescrOp;
                ddlScegliSpesa.Items.Add(new ListItem(show, op.IdOperaio.ToString()));
                i++;

                if (op.NomeOp == "Maurizio" || op.NomeOp == "Mau" || op.NomeOp == "MAU")
                {
                    ddlScegliSpesa.SelectedIndex = i;
                }
            }
        }
        //Ogni Helper "Fill" va aggiunto qua dentro per funzionare
        protected void FillAllDdl()
        {
            FillDdlScegliCant();
            FillDdlScegliAcquirente();
        }

        /* EVENTI CLICK */
        protected void btnFiltroCant_Click(object sender, EventArgs e)
        {
            FillDdlScegliCant();
            pnlSubIntestazione.Visible = false;
        }

        /* EVENTI TEXT-CHANGED */
        protected void ddlScegliCant_TextChanged(object sender, EventArgs e)
        {
            if (ddlScegliCant.SelectedIndex != 0)
            {
                pnlSubIntestazione.Visible = true;
            }
            else
            {
                pnlSubIntestazione.Visible = false;
            }
        }

        protected void btnInserisci_Click(object sender, EventArgs e)
        {
            string idCant = ddlScegliCant.SelectedItem.Value;
            string spesa = ddlScegliSpesa.SelectedItem.Value;

            if (Convert.ToInt32(txtQta.Text) > 0)
            {
                bool isInserito = GestioneSpeseDAO.InserisciSpesa(idCant, acquirente, txtQta.Text, "SPE", txtPzzoManodop.Text,
                    txtDescrManodop.Text, txtNote1.Text, txtNote2.Text);

                if (isInserito)
                {
                    lblIsManodopInserita.Text = "Record inserito con successo";
                    lblIsManodopInserita.ForeColor = Color.Blue;
                }
                else
                {
                    lblIsManodopInserita.Text = "Errore durante l'inserimento del record";
                    lblIsManodopInserita.ForeColor = Color.Red;
                }
            }
            else
            {
                lblIsManodopInserita.Text = "La quantità deve essere maggiore di '0'";
                lblIsManodopInserita.ForeColor = Color.Red;
            }
        }
    }
}