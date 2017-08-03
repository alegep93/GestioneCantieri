using GestioneCantieri.DAO;
using GestioneCantieri.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestioneCantieri
{
    public partial class StampaVerificaCantiere : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDdlScegliCantiere();
                btnStampaVerificaCant.Visible = false;
            }
        }

        /* HELPERS */
        protected void FillDdlScegliCantiere()
        {
            DataTable dt = CantieriDAO.GetCantieri(txtAnno.Text, txtCodCant.Text, "", chkChiuso.Checked, chkRiscosso.Checked);
            List<Cantieri> listCantieri = dt.DataTableToList<Cantieri>();

            ddlScegliCant.Items.Clear();
            ddlScegliCant.Items.Add(new System.Web.UI.WebControls.ListItem("", "-1"));

            foreach (Cantieri c in listCantieri)
            {
                string show = c.CodCant + " - " + c.DescriCodCAnt;
                ddlScegliCant.Items.Add(new System.Web.UI.WebControls.ListItem(show, c.IdCantieri.ToString()));
            }
        }
        protected void BindGrid()
        {
            decimal valore = 0m;
            decimal totMate = 0m;
            decimal totRientro = 0m;
            decimal totManodop = 0m;
            decimal totOper = 0m;
            decimal totArrot = 0m;

            List<MaterialiCantieri> matCantList = MaterialiCantieriDAO.GetMaterialeCantiere(ddlScegliCant.SelectedItem.Value);
            grdStampaVerificaCant.DataSource = matCantList;
            grdStampaVerificaCant.DataBind();

            MaterialiCantieri mc = MaterialiCantieriDAO.GetDataPerIntestazione(ddlScegliCant.SelectedItem.Value);

            lblIntestStampa.Text = "<strong>CodCant</strong>: " + mc.CodCant + " --- " +
                "<strong>DescriCodCant</strong>: " + mc.DescriCodCant + " --- " +
                "<strong>Cliente</strong>: " + mc.RagSocCli;

            Cantieri c = CantieriDAO.GetCantiere(ddlScegliCant.SelectedItem.Value);
            lblTotContoCliente.Text = "<strong>Tot. Conto/Preventivo</strong>: ";

            if (c.Preventivo)
                lblTotContoCliente.Text += c.ValorePreventivo;
            else
                lblTotContoCliente.Text += RicalcoloConti.totRicalcoloConti.ToString();

            for (int i = 0; i < matCantList.Count; i++)
            {
                valore = Convert.ToInt32(grdStampaVerificaCant.Rows[i].Cells[4].Text) * Convert.ToDecimal(grdStampaVerificaCant.Rows[i].Cells[5].Text);
                grdStampaVerificaCant.Rows[i].Cells[6].Text = valore.ToString();
            }

            foreach (MaterialiCantieri matCant in matCantList)
            {
                decimal val = Convert.ToInt32(matCant.Qta) * matCant.PzzoUniCantiere;
                switch (matCant.Tipologia)
                {
                    case "MATERIALE":
                        totMate += val;
                        break;
                    case "RIENTRO":
                        totRientro += val;
                        break;
                    case "MANODOPERA":
                        totManodop += val;
                        break;
                    case "OPERAIO":
                        totOper += val;
                        break;
                    case "ARROTONDAMENTO":
                        totArrot += val;
                        break;
                }
            }

            lblTotMate.Text = "<strong>Tot. Materiale</strong>: " + String.Format("{0:n}", totMate).ToString();
            lblTotRientro.Text = "<strong>Tot. Rientro</strong>: " + String.Format("{0:n}", totRientro).ToString();
            lblTotOper.Text = "<strong>Tot. Operaio</strong>: " + String.Format("{0:n}", totOper).ToString();
            lblTotArrot.Text = "<strong>Tot. Arrotondamento</strong>: " + String.Format("{0:n}", totArrot).ToString();

            decimal sommaTotPerTipol = totMate + totRientro + totOper + totArrot;
            decimal contoFinCli = Convert.ToDecimal(lblTotContoCliente.Text.Split(':')[1].Trim());
            decimal totGuadagno = Convert.ToDecimal(String.Format("{0:n}", contoFinCli - sommaTotPerTipol));
            totManodop = Convert.ToDecimal(String.Format("{0:n}", totManodop));

            lblTotGuadagno.Text = "<strong>Totale Guadagno</strong>: " + totGuadagno;
            lblTotManodop.Text = "<strong>Tot. Manodopera</strong>: " + totManodop;


            lblTotGuadagnoConManodop.Text = "<strong>Tot. Guadagno Con Manodopopera</strong>: " + (totGuadagno + totManodop);
        }

        /* EVENTI CLICK */
        protected void btnFiltraCantieri_Click(object sender, EventArgs e)
        {
            FillDdlScegliCantiere();
        }
        protected void btnStampaVerificaCant_Click(object sender, EventArgs e)
        {
            //Ricreo i passaggi della "Stampa Ricalcolo Conti" per ottenere il valore del "Totale Ricalcolo"
            MaterialiCantieri mc = MaterialiCantieriDAO.GetDataPerIntestazione(ddlScegliCant.SelectedItem.Value);
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

            BindGrid();
            GroupGridViewCells();
        }

        /* EVENTI TEXT-CHANGED */
        protected void ddlScegliCant_TextChanged(object sender, EventArgs e)
        {
            if (ddlScegliCant.SelectedIndex != 0)
            {
                btnStampaVerificaCant.Visible = true;
            }
            else
            {
                btnStampaVerificaCant.Visible = false;
            }
        }

        //Metodi per la gridView con intestazioni dinamiche
        protected void GroupGridViewCells()
        {
            GridViewHelper helper = new GridViewHelper(grdStampaVerificaCant);
            helper.RegisterGroup("Tipologia", true, true);
            helper.ApplyGroupSort();
        }
        /* Necessario per la creazione della GridView con intestazioni dinamiche */
        /* Definisce l'ordinamento dei dati presenti nella GridView */
        protected void grdStampaVerificaCant_Sorting(object sender, GridViewSortEventArgs e)
        {
            BindGrid();
        }
    }
}