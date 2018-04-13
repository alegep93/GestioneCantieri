using GestioneCantieri.DAO;
using GestioneCantieri.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;

namespace GestioneCantieri
{
    public partial class StampaValoriCantieriConOpzioni : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDdlScegliCliente();
            }
        }

        #region Helpers
        protected void FillDdlScegliCliente()
        {
            List<Clienti> listaClienti = ClientiDAO.GetClienti(txtFiltraCliente.Text);

            ddlScegliCliente.Items.Clear();
            ddlScegliCliente.Items.Add(new System.Web.UI.WebControls.ListItem("", "-1"));

            foreach (Clienti c in listaClienti)
            {
                ddlScegliCliente.Items.Add(new System.Web.UI.WebControls.ListItem(c.RagSocCli, c.IdCliente.ToString()));
            }
        }
        protected void CompilaCampi(string idCantiere, MaterialiCantieri mc, ref List<Data.StampaValoriCantieriConOpzioni> listForGridview)
        {
            // Popolo l'oggetto della stampa con i valori iniziali del cantiere
            Data.StampaValoriCantieriConOpzioni objStampa = new Data.StampaValoriCantieriConOpzioni();
            objStampa.CodCant = mc.CodCant;
            objStampa.DescriCodCAnt = mc.DescriCodCant;
            objStampa.RagSocCli = mc.RagSocCli;

            objStampa.TotaleAcconti = objStampa.TotaleConto = objStampa.TotaleFinale = 0m;

            //Popolo il campo Conto/Preventivo
            Cantieri c = CantieriDAO.GetCantiere(idCantiere);
            if (c.Preventivo)
                objStampa.TotaleConto = c.ValorePreventivo;
            else
                objStampa.TotaleConto = Math.Round(RicalcoloConti.totRicalcoloConti, 2);

            //Popolo il campo Tot. Acconti
            decimal totAcconti = 0m;
            List<Pagamenti> pagList = PagamentiDAO.GetPagamenti(idCantiere);
            foreach (Pagamenti p in pagList)
            {
                totAcconti += p.Imporo;
            }
            objStampa.TotaleAcconti = totAcconti;

            //Popolo il campo Tot. Finale
            decimal totContoPreventivo = objStampa.TotaleConto;
            decimal totFin = totContoPreventivo - totAcconti;
            objStampa.TotaleFinale = totFin;

            //Aggiungo l'oggetto alla lista
            listForGridview.Add(objStampa);
        }
        #endregion

        #region Eventi Click
        protected void btnStampaContoCliente_Click(object sender, EventArgs e)
        {
            List<Cantieri> listaCantieri = CantieriDAO.GetCantieri(txtAnno.Text, Convert.ToInt32(ddlScegliCliente.SelectedValue), chkFatturato.Checked, chkChiuso.Checked, chkRiscosso.Checked);
            List<Data.StampaValoriCantieriConOpzioni> listForGridview = new List<Data.StampaValoriCantieriConOpzioni>();

            foreach (Cantieri c in listaCantieri)
            {
                //Ricreo i passaggi della "Stampa Ricalcolo Conti" per ottenere il valore del "Totale Ricalcolo"
                MaterialiCantieri mc = CantieriDAO.GetDataPerIntestazione(c.IdCantieri.ToString());
                RicalcoloConti rc = new RicalcoloConti();
                decimal totale = 0m;
                PdfPTable pTable = rc.InitializePdfTableDDT(grdStampaMateCantPDF);
                Document pdfDoc = new Document(PageSize.A4, 8f, 2f, 2f, 2f);
                pdfDoc.Open();
                rc.idCant = c.IdCantieri.ToString();
                rc.BindGrid(grdStampaMateCant);
                rc.BindGridPDF(grdStampaMateCant, grdStampaMateCantPDF);
                rc.GeneraPDFPerContoFinCli(pdfDoc, mc, pTable, grdStampaMateCantPDF, totale);
                pdfDoc.Close();

                //Popolo i campi di riepilogo con i dati necessari
                CompilaCampi(c.IdCantieri.ToString(), mc, ref listForGridview);
            }

            grdStampaConOpzioni.DataSource = listForGridview;
            grdStampaConOpzioni.DataBind();

            // Assegno il valore alla label che mostra il totale generale
            decimal totGen = 0m;

            for (int i = 0; i < grdStampaConOpzioni.Rows.Count; i++)
            {
                totGen += Convert.ToDecimal(grdStampaConOpzioni.Rows[i].Cells[5].Text);

                if (grdStampaConOpzioni.Rows[i].Cells[5].Text == "0")
                {
                    grdStampaConOpzioni.Rows[i].Visible = false;
                }
            }

            lblTotaleGeneraleStampa.Text = "Totale: " + totGen.ToString("N2");
        }
        protected void btnFiltraCantieri_Click(object sender, EventArgs e)
        {
            FillDdlScegliCliente();
        }
        #endregion
    }
}