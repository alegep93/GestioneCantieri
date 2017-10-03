using GestioneCantieri.DAO;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace GestioneCantieri
{
    public partial class StampaOrdFrutLocale : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlScegliCantiere.SelectedIndex = 0;
                FillDdlScegliCantiere();
            }
        }

        /* EVENTI TEXT-CHANGED */
        protected void ddlScegliCantiere_TextChanged(object sender, EventArgs e)
        {
            BindGrid();
            GroupGridViewCells();
        }

        /* HELPERS */
        protected void BindGrid()
        {
            int i = 0;
            int c = 0;
            int numberOfRows = 0;
            int lastRow = 0;
            List<int> indiciFruttiDaInserire = new List<int>();

            List<StampaOrdFrutCantLoc> listGruppi = StampaOrdFrutCantLocDAO.GetAllGruppiInLocale(ddlScegliCantiere.SelectedItem.Value);
            grdGruppiInLocale.DataSource = listGruppi;
            grdGruppiInLocale.DataBind();

            List<StampaOrdFrutCantLoc> listFrutti = StampaOrdFrutCantLocDAO.GetAllFruttiInLocale(ddlScegliCantiere.SelectedItem.Value);
            grdFruttiInLocale.DataSource = listFrutti;
            grdFruttiInLocale.DataBind();

            List<StampaOrdFrutCantLoc> listFruttiNonInGruppo = StampaOrdFrutCantLocDAO.GetAllFruttiNonInGruppo(ddlScegliCantiere.SelectedItem.Value);
            grdFruttiNonInGruppo.DataSource = listFruttiNonInGruppo;
            grdFruttiNonInGruppo.DataBind();

            DataTable dt = StampaOrdFrutCantLocDAO.GetAllFruttiInLocaleDataTable(ddlScegliCantiere.SelectedItem.Value);

            lastRow = grdFruttiInLocale.Rows.Count - 1;

            while (i < grdFruttiNonInGruppo.Rows.Count)
            {
                if (CheckIfFruttoIsInListaFrutti(grdFruttiNonInGruppo.Rows[i].Cells[0].Text))
                {
                    //Sommo le quantità della griglia "FruttiNonInGruppo" con le qta della griglia "FruttiInLocale" se la descrizione del frutto è la stessa
                    for (int j = 0; j < grdFruttiInLocale.Rows.Count; j++)
                    {
                        string testoGrdFruttiInLocale = grdFruttiInLocale.Rows[j].Cells[0].Text;
                        string testoGrdFruttiNonInGruppo = grdFruttiNonInGruppo.Rows[i].Cells[0].Text;

                        if (testoGrdFruttiInLocale == testoGrdFruttiNonInGruppo)
                        {
                            string totQta = (Convert.ToInt32(grdFruttiInLocale.Rows[j].Cells[1].Text) + Convert.ToInt32(grdFruttiNonInGruppo.Rows[i].Cells[1].Text)).ToString();
                            grdFruttiInLocale.Rows[j].Cells[1].Text = totQta;
                            i++;

                            break;
                        }
                        else { break; }
                    }
                }
                else
                {
                    //Aggiungo una nuova riga
                    DataRow dr = dt.NewRow();
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                    grdFruttiInLocale.DataSource = dt;
                    grdFruttiInLocale.DataBind();

                    indiciFruttiDaInserire.Add(i);

                    //Incremento i contatori
                    i++;
                    numberOfRows++;
                }
            }

            while(numberOfRows != 0)
            {
                while (c < indiciFruttiDaInserire.Count)
                {
                    //Popolo le righe vuote con descr e qta del frutto
                    grdFruttiInLocale.Rows[lastRow + numberOfRows].Cells[0].Text = grdFruttiNonInGruppo.Rows[indiciFruttiDaInserire[c]].Cells[0].Text;
                    grdFruttiInLocale.Rows[lastRow + numberOfRows].Cells[1].Text = grdFruttiNonInGruppo.Rows[indiciFruttiDaInserire[c]].Cells[1].Text;
                    c++;

                    break;
                }

                numberOfRows--;
            }
        }
        protected void FillDdlScegliCantiere()
        {
            List<Cantieri> listCantieri = OrdineFruttiDAO.GetListCantieri();

            ddlScegliCantiere.Items.Clear();

            //Il primo parametro ("") corrisponde al valore e il secondo alla chiave (il valore è quello che viene visualizzato nella form)
            ddlScegliCantiere.Items.Add(new ListItem("", "-1"));

            foreach (Cantieri c in listCantieri)
            {
                string cantiere = c.CodCant + " - " + c.DescriCodCAnt;
                ddlScegliCantiere.Items.Add(new ListItem(cantiere, c.IdCantieri.ToString())); //new ListItem(valore, chiave);
            }
        }
        protected void GroupGridViewCells()
        {
            GridViewHelper helper = new GridViewHelper(grdGruppiInLocale);
            helper.RegisterGroup("NomeLocale", true, true);
            helper.ApplyGroupSort();
        }
        protected bool CheckIfFruttoIsInListaFrutti(string nomeFrutto)
        {
            for (int j = 0; j < grdFruttiInLocale.Rows.Count; j++)
            {
                string testoGrdFruttiInLocale = grdFruttiInLocale.Rows[j].Cells[0].Text;
                if (testoGrdFruttiInLocale == nomeFrutto)
                    return true;
            }
            return false;
        }

        /* Override per il corretto funzionamento di tutte le funzionalità della pagina */
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
            //Do nothing
        }

        /* Necessario per la creazione della GridView con intestazioni dinamiche */
        /* Definisce l'ordinamento dei dati presenti nella GridView */
        protected void grdGruppiInLocale_Sorting(object sender, GridViewSortEventArgs e)
        {
            BindGrid();
        }
    }
}