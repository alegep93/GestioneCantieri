using GestioneCantieri.DAO;
using GestioneCantieri.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
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
    public partial class RicalcoloConti : System.Web.UI.Page
    {
        public static decimal totRicalcoloConti = 0m;
        public string idCant = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnStampaContoCliente.Visible = false;
                FillDdlScegliCantiere();
            }
        }

        /* HELPERS */
        protected decimal CalcolaPercentualeTotaleMaterialiNascosti()
        {
            decimal matVisibile = MaterialiCantieriDAO.TotMaterialeVisibile(idCant);
            decimal matNascosto = MaterialiCantieriDAO.TotNascosto(idCant);
            decimal percentuale = Math.Round(((matNascosto * 100) / matVisibile), 2);

            return percentuale;
        }
        protected void RipartisciPercentuale()
        {
            decimal perc = CalcolaPercentualeTotaleMaterialiNascosti();

            for (int i = 0; i < grdStampaMateCant.Rows.Count; i++)
            {
                //string visibile = 
                //string ricalcolo
            }
        }
        protected void AggiungiRicarico()
        {
        }
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
        public void BindGrid(GridView grd)
        {
            decimal perc = CalcolaPercentualeTotaleMaterialiNascosti();
            decimal valore = 0m;
            List<decimal> decListRicalcolo = MaterialiCantieriDAO.CalcolaValoreRicalcolo(idCant, perc);
            List<decimal> decListRicarico = MaterialiCantieriDAO.CalcolaValoreRicarico(idCant);

            List<MaterialiCantieri> matCantList = MaterialiCantieriDAO.GetMaterialeCantiere(idCant);
            grd.DataSource = matCantList;
            grd.DataBind();

            //Imposto la colonna del valore
            for (int i = 0; i < grd.Rows.Count; i++)
            {
                string visibile = grd.Rows[i].Cells[8].Text;
                string ricalcolo = grd.Rows[i].Cells[9].Text;
                string ricaricoSiNo = grd.Rows[i].Cells[10].Text;
                decimal pzzoUnit = 0m, valRicarico = 0m, valRicalcolo = 0m;
                int cRicalcolo = 0, cRicarico = 0;

                pzzoUnit = Convert.ToDecimal(grd.Rows[i].Cells[3].Text);

                if (visibile == "True" && ricalcolo == "True")
                {
                    grd.Rows[i].Cells[5].Text = Math.Round(decListRicalcolo[cRicalcolo], 2).ToString();
                    valRicalcolo = Convert.ToDecimal(grd.Rows[i].Cells[5].Text);
                    cRicalcolo++;
                }

                if (visibile == "True" && ricaricoSiNo == "True")
                {
                    grd.Rows[i].Cells[4].Text = Math.Round(decListRicarico[cRicarico], 2).ToString();
                    valRicarico = Convert.ToDecimal(grd.Rows[i].Cells[4].Text);
                    cRicarico++;
                }

                grd.Rows[i].Cells[6].Text = (pzzoUnit + valRicalcolo + valRicarico).ToString();

                valore = Convert.ToInt32(grd.Rows[i].Cells[2].Text) * Convert.ToDecimal(grd.Rows[i].Cells[6].Text);
                grd.Rows[i].Cells[7].Text = Math.Round(valore, 2).ToString();
            }
        }
        public void BindGridPDF(GridView grd, GridView grdPDF)
        {
            grdPDF.DataSource = grd.DataSource;
            grdPDF.DataBind();

            //Imposto la colonna del valore
            for (int i = 0; i < grd.Rows.Count; i++)
            {
                grdPDF.Rows[i].Cells[0].Text = grd.Rows[i].Cells[0].Text;
                grdPDF.Rows[i].Cells[1].Text = grd.Rows[i].Cells[1].Text;
                grdPDF.Rows[i].Cells[2].Text = grd.Rows[i].Cells[2].Text;
                grdPDF.Rows[i].Cells[3].Text = grd.Rows[i].Cells[3].Text;
                grdPDF.Rows[i].Cells[4].Text = grd.Rows[i].Cells[7].Text;
            }
        }
        protected decimal CalcolaTotAcconti()
        {
            decimal totAcconti = 0m;
            List<Pagamenti> pagList = PagamentiDAO.GetPagamenti(idCant);

            foreach(Pagamenti p in pagList)
            {
                totAcconti += p.Imporo;
            }

            return totAcconti;
        }

        //Stampa PDF
        public PdfPTable InitializePdfTableDDT(GridView grd)
        {
            float[] columns = { 150f, 380f, 65f, 140f, 85f };
            PdfPTable table = new PdfPTable(grd.Columns.Count);
            table.WidthPercentage = 100;
            table.SetTotalWidth(columns);

            return table;
        }
        public void ExportToPdfPerContoFinCli(GridView grd)
        {
            decimal totale = 0m;
            MaterialiCantieri mc = MaterialiCantieriDAO.GetDataPerIntestazione(idCant);

            //Apro lo stream verso il file PDF
            Document pdfDoc = new Document(PageSize.A4, 8f, 2f, 2f, 2f);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();

            PdfPTable table = InitializePdfTableDDT(grdStampaMateCantPDF);

            Phrase title = new Phrase("Ragione Sociale Cliente: " + mc.RagSocCli, FontFactory.GetFont("Arial", 20, iTextSharp.text.Font.BOLD, BaseColor.RED));
            pdfDoc.Add(title);

            GeneraPDFPerContoFinCli(pdfDoc, mc, table, grd, totale);

            pdfDoc.Close();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + mc.RagSocCli + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();
        }
        public void GeneraPDFPerContoFinCli(Document pdfDoc, MaterialiCantieri mc, PdfPTable table, GridView grd, decimal totale)
        {
            PdfPTable tblTotali = null;
            Phrase intestazione = new Phrase();
            intestazione = GeneraIntestazioneContoFinCli(mc);

            //Transfer rows from GridView to table
            for (int i = 0; i < grd.Columns.Count; i++)
            {
                Phrase cellText = new Phrase(Server.HtmlDecode(grd.Columns[i].HeaderText), FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                PdfPCell cell = new PdfPCell(cellText);
                cell.BorderWidth = 0;
                cell.BorderWidthBottom = 1;
                cell.BorderColorBottom = BaseColor.BLUE;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(cell);

                if (i == 2 || i == 3 || i == 4)
                {
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                }
            }

            for (int i = 0; i < grd.Rows.Count; i++)
            {
                if (grd.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    for (int j = 0; j < grd.Columns.Count; j++)
                    {
                        Phrase cellText = new Phrase();

                        if (j == 3 || j == 4)
                        {
                            cellText = new Phrase(Server.HtmlDecode(String.Format("{0:n}", Convert.ToDecimal(grd.Rows[i].Cells[j].Text)).ToString()), FontFactory.GetFont("Arial", 10, BaseColor.BLACK));
                        }
                        else
                        {
                            cellText = new Phrase(Server.HtmlDecode(grd.Rows[i].Cells[j].Text), FontFactory.GetFont("Arial", 10, BaseColor.BLACK));
                        }

                        PdfPCell cell = new PdfPCell(cellText);
                        cell.BorderWidth = 0;

                        switch (j)
                        {
                            case 2:
                            case 3:
                            case 4:
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                break;
                        }

                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell.BackgroundColor = new BaseColor(ColorTranslator.FromHtml("#F7F7F7"));
                        }
                        table.AddCell(cell);
                    }
                    totale += Convert.ToDecimal(grd.Rows[i].Cells[4].Text);
                }
            }

            tblTotali = new PdfPTable(1);
            tblTotali.WidthPercentage = 100;

            GeneraTotalePerContoFinCli(tblTotali, totale);

            pdfDoc.Add(new Paragraph(""));
            pdfDoc.Add(intestazione);
            pdfDoc.Add(table);
            pdfDoc.Add(new Paragraph(""));
            pdfDoc.Add(tblTotali);

            table = InitializePdfTableDDT(grd);
            totale = 0m;
        }
        protected Phrase GeneraIntestazioneContoFinCli(MaterialiCantieri mc)
        {
            string codCant = "Codice Cantiere: " + mc.CodCant;
            string descriCodCant = "Descrizione Codice Cantiere: " + mc.DescriCodCant;
            string intestazioneObj = codCant + "    -    " + descriCodCant;

            Phrase intestazione = new Phrase(intestazioneObj, FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.ITALIC, BaseColor.BLUE));

            return intestazione;
        }
        protected void GeneraTotalePerContoFinCli(PdfPTable tblTotali, decimal totale)
        {
            decimal totValAcconti = CalcolaTotAcconti();
            totRicalcoloConti = totale - totValAcconti;

            //Totale No Iva
            Phrase totContoFinCli = new Phrase("Totale Senza IVA: " + String.Format("{0:n}", totale), FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.ITALIC, BaseColor.BLUE));
            PdfPCell totContoFinCliCell = new PdfPCell(totContoFinCli);

            //Totale Acconti
            Phrase totAcconti = new Phrase("Totale Acconti : " + String.Format("{0:n}", totValAcconti), FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.ITALIC, BaseColor.BLUE));
            PdfPCell totAccontiCell = new PdfPCell(totAcconti);
            Phrase totaleFinale = new Phrase("Totale Finale: " + String.Format("{0:n}", totRicalcoloConti), FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.ITALIC, BaseColor.BLUE));
            PdfPCell totaleFinaleCell = new PdfPCell(totaleFinale);

            totContoFinCliCell.HorizontalAlignment = totAccontiCell.HorizontalAlignment = totaleFinaleCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            totContoFinCliCell.BorderWidth = totAccontiCell.BorderWidth = totaleFinaleCell.BorderWidth = 0;
            totContoFinCliCell.BorderWidthTop = 1;
            totContoFinCliCell.BorderColorTop = BaseColor.BLUE;
            totaleFinaleCell.PaddingBottom = 20;

            tblTotali.AddCell(totContoFinCliCell);
            tblTotali.AddCell(totAccontiCell);
            tblTotali.AddCell(totaleFinaleCell);
        }

        /* EVENTI CLICK*/
        protected void btnStampaContoCliente_Click(object sender, EventArgs e)
        {
            idCant = ddlScegliCant.SelectedItem.Value;
            BindGrid(grdStampaMateCant);
            BindGridPDF(grdStampaMateCant,grdStampaMateCantPDF);
            ExportToPdfPerContoFinCli(grdStampaMateCantPDF);
        }
        protected void btnFiltraCantieri_Click(object sender, EventArgs e)
        {
            FillDdlScegliCantiere();
        }

        /* EVENTI TEXT-CHANGED */
        protected void ddlScegliCant_TextChanged(object sender, EventArgs e)
        {
            if (ddlScegliCant.SelectedIndex != 0)
            {
                btnStampaContoCliente.Visible = true;
            }
            else
            {
                btnStampaContoCliente.Visible = false;
            }
        }
    }
}