using GestioneCantieri.DAO;
using GestioneCantieri.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;

namespace GestioneCantieri
{
    public partial class StampaValoriCantieri : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDdlScegliCantiere();
                btnStampaValoriCantieri.Visible = false;
                pnlRisultati.Visible = false;
            }
        }

        #region Helpers
        protected void FillDdlScegliCantiere()
        {
            DataTable dt = CantieriDAO.GetCantieri(txtAnno.Text, txtCodCant.Text, chkFatturato.Checked, chkChiuso.Checked, chkRiscosso.Checked);
            List<Cantieri> listCantieri = dt.DataTableToList<Cantieri>();

            ddlScegliCant.Items.Clear();
            ddlScegliCant.Items.Add(new System.Web.UI.WebControls.ListItem("", "-1"));

            foreach (Cantieri c in listCantieri)
            {
                string show = c.CodCant + " - " + c.DescriCodCAnt;
                ddlScegliCant.Items.Add(new System.Web.UI.WebControls.ListItem(show, c.IdCantieri.ToString()));
            }
        }
        protected void CompilaCampi(string idCantiere)
        {
            //Popolo il campo Conto/Preventivo
            Cantieri c = CantieriDAO.GetCantiere(idCantiere);
            if (c.Preventivo)
                txtContoPreventivo.Text += String.Format("{0:n}", c.ValorePreventivo);
            else
                txtContoPreventivo.Text += Math.Round(RicalcoloConti.totRicalcoloConti, 2).ToString();

            //Popolo il campo Tot. Acconti
            decimal totAcconti = 0m;
            List<Pagamenti> pagList = PagamentiDAO.GetPagamenti(idCantiere);
            foreach (Pagamenti p in pagList)
            {
                totAcconti += p.Imporo;
            }
            txtTotPagamenti.Text = String.Format("{0:n}", totAcconti).ToString();

            //Popolo il campo Tot. Finale
            decimal totContoPreventivo = Convert.ToDecimal(txtContoPreventivo.Text);
            decimal totFin = totContoPreventivo - totAcconti;
            txtTotFinale.Text = String.Format("{0:n}", totFin).ToString();
        }
        #endregion

        #region Eventi Click
        protected void btnFiltraCantieri_Click(object sender, EventArgs e)
        {
            FillDdlScegliCantiere();
        }
        protected void btnStampaContoCliente_Click(object sender, EventArgs e)
        {
            //Ricreo i passaggi della "Stampa Ricalcolo Conti" per ottenere il valore del "Totale Ricalcolo"
            string idCantiere = ddlScegliCant.SelectedItem.Value;
            MaterialiCantieri mc = CantieriDAO.GetDataPerIntestazione(idCantiere);
            RicalcoloConti rc = new RicalcoloConti();
            decimal totale = 0m;
            PdfPTable pTable = rc.InitializePdfTableDDT(grdStampaMateCantPDF);
            Document pdfDoc = new Document(PageSize.A4, 8f, 2f, 2f, 2f);
            pdfDoc.Open();
            rc.idCant = ddlScegliCant.SelectedItem.Value;
            rc.BindGrid(grdStampaMateCant);
            rc.BindGridPDF(grdStampaMateCant, grdStampaMateCantPDF);
            rc.GeneraPDFPerContoFinCli(pdfDoc, mc, pTable, grdStampaMateCantPDF, totale);
            pdfDoc.Close();

            //Popolo i campi di riepilogo con i dati necessari
            CompilaCampi(idCantiere);
        }
        #endregion

        #region Eventi Text-Changed
        protected void ddlScegliCant_TextChanged(object sender, EventArgs e)
        {
            if (ddlScegliCant.SelectedIndex != 0)
            {
                btnStampaValoriCantieri.Visible = true;
                pnlRisultati.Visible = true;
                txtContoPreventivo.Text = txtTotPagamenti.Text = txtTotFinale.Text = "";
            }
            else
            {
                btnStampaValoriCantieri.Visible = false;
                pnlRisultati.Visible = false;
            }
        }
        #endregion
    }
}