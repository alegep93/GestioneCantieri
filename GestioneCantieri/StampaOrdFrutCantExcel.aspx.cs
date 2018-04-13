using GestioneCantieri.DAO;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestioneCantieri
{
    public partial class StampaExcell : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnPrint.Visible = false;
                ddlScegliCantiere.SelectedIndex = 0;
                FillDdlScegliCantiere();
            }
        }

        #region Eventi Click
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            CreateExcel();
        }
        #endregion

        #region Eventi Text-Changed
        protected void ddlScegliCantiere_TextChanged(object sender, EventArgs e)
        {
            BindGrid();

            if (ddlScegliCantiere.SelectedIndex != 0)
                btnPrint.Visible = true;
            else
                btnPrint.Visible = false;
        }
        #endregion

        #region Helpers
        protected void BindGrid()
        {
            //int i = 0;
            //int c = 0;
            //int numberOfRows = 0;
            //int lastRow = 0;
            //List<int> indiciFruttiDaInserire = new List<int>();

            //List<StampaOrdFrutCantLoc> listFrutti = StampaOrdFrutCantLocDAO.GetAllFruttiInLocale(ddlScegliCantiere.SelectedItem.Value);
            //grdFruttiInLocale.DataSource = listFrutti;
            //grdFruttiInLocale.DataBind();

            //List<StampaOrdFrutCantLoc> listFruttiNonInGruppo = StampaOrdFrutCantLocDAO.GetAllFruttiNonInGruppo(ddlScegliCantiere.SelectedItem.Value);
            //grdFruttiNonInGruppo.DataSource = listFruttiNonInGruppo;
            //grdFruttiNonInGruppo.DataBind();

            //DataTable dt = StampaOrdFrutCantLocDAO.GetAllFruttiInLocaleDataTable(ddlScegliCantiere.SelectedItem.Value);

            //lastRow = grdFruttiInLocale.Rows.Count - 1;

            //while (i < grdFruttiNonInGruppo.Rows.Count)
            //{
            //    if (CheckIfFruttoIsInListaFrutti(grdFruttiNonInGruppo.Rows[i].Cells[0].Text))
            //    {
            //        //Sommo le quantità della griglia "FruttiNonInGruppo" con le qta della griglia "FruttiInLocale" se la descrizione del frutto è la stessa
            //        for (int j = 0; j < grdFruttiInLocale.Rows.Count; j++)
            //        {
            //            string testoGrdFruttiInLocale = grdFruttiInLocale.Rows[j].Cells[0].Text;
            //            string testoGrdFruttiNonInGruppo = grdFruttiNonInGruppo.Rows[i].Cells[0].Text;

            //            if (testoGrdFruttiInLocale == testoGrdFruttiNonInGruppo)
            //            {
            //                string totQta = (Convert.ToInt32(grdFruttiInLocale.Rows[j].Cells[1].Text) + Convert.ToInt32(grdFruttiNonInGruppo.Rows[i].Cells[1].Text)).ToString();
            //                grdFruttiInLocale.Rows[j].Cells[1].Text = totQta;
            //                i++;

            //                break;
            //            }
            //            else
            //            {
            //                continue;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        //Aggiungo una nuova riga
            //        DataRow dr = dt.NewRow();
            //        dt.Rows.Add(dr);
            //        dt.AcceptChanges();
            //        grdFruttiInLocale.DataSource = dt;
            //        grdFruttiInLocale.DataBind();

            //        indiciFruttiDaInserire.Add(i);

            //        //Incremento i contatori
            //        i++;
            //        numberOfRows++;
            //    }
            //}

            //while (numberOfRows != 0)
            //{
            //    while (c < indiciFruttiDaInserire.Count)
            //    {
            //        //Popolo le righe vuote con descr e qta del frutto
            //        grdFruttiInLocale.Rows[lastRow + numberOfRows].Cells[0].Text = grdFruttiNonInGruppo.Rows[indiciFruttiDaInserire[c]].Cells[0].Text;
            //        grdFruttiInLocale.Rows[lastRow + numberOfRows].Cells[1].Text = grdFruttiNonInGruppo.Rows[indiciFruttiDaInserire[c]].Cells[1].Text;
            //        c++;

            //        break;
            //    }

            //    numberOfRows--;
            //}

            grdFruttiInLocale.DataSource = OrdineFruttiDAO.GetFruttiPerStampaExcel(ddlScegliCantiere.SelectedItem.Value);
            grdFruttiInLocale.DataBind();
        }
        protected void CreateExcel()
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=OrdFrut-" + ddlScegliCantiere.SelectedItem.Text + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();

            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            htmlWrite.WriteLine("<strong><font size='4'>" + ddlScegliCantiere.SelectedItem.Text + "</font></strong>");

            // viene reindirizzato il rendering verso la stringa in uscita
            grdFruttiInLocale.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());

            Response.End();
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
        protected void FillDdlScegliCantiere()
        {
            List<Cantieri> listCantieri = CantieriDAO.GetListCantieri();

            ddlScegliCantiere.Items.Clear();

            //Il primo parametro ("") corrisponde al valore e il secondo alla chiave (il valore è quello che viene visualizzato nella form)
            ddlScegliCantiere.Items.Add(new ListItem("", "-1"));

            foreach (Cantieri c in listCantieri)
            {
                string cantiere = c.CodCant + " - " + c.DescriCodCAnt;
                ddlScegliCantiere.Items.Add(new ListItem(cantiere, c.IdCantieri.ToString())); //new ListItem(valore, chiave);
            }
        }
        #endregion

        /* Override per il corretto funzionamento della creazione del foglio Excel */
        public override void VerifyRenderingInServerForm(Control control)
        {
            //Do nothing
        }
    }

}