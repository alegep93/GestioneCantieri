﻿using GestioneCantieri.DAO;
using GestioneCantieri.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace GestioneCantieri
{
    public partial class StampeVarie : System.Web.UI.Page
    {
        protected DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillAllDdl();
                pnlCampiStampaDDT.Visible = false;
                txtDataDa.Text = "2010-01-01";
                txtDataA.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtDataDa.TextMode = txtDataA.TextMode = TextBoxMode.Date;
            }
        }

        /* HELPERS */
        protected void FillDdlScegliStampa()
        {
            DataTable dt = StampeDAO.GetNomiStampe();
            List<Stampe> listStampe = dt.DataTableToList<Stampe>();

            ddlScegliStampa.Items.Clear();
            ddlScegliStampa.Items.Add(new System.Web.UI.WebControls.ListItem("", "-1"));

            foreach (Stampe st in listStampe)
            {
                ddlScegliStampa.Items.Add(new System.Web.UI.WebControls.ListItem(st.NomeStampa, st.Id.ToString()));
            }
        }
        protected void FillDdlScegliFornitore()
        {
            DataTable dt = FornitoriDAO.GetFornitori();
            List<Fornitori> listFornitori = dt.DataTableToList<Fornitori>();

            ddlScegliFornitore.Items.Clear();
            ddlScegliFornitore.Items.Add(new System.Web.UI.WebControls.ListItem("", "-1"));

            foreach (Fornitori f in listFornitori)
            {
                ddlScegliFornitore.Items.Add(new System.Web.UI.WebControls.ListItem(f.RagSocForni, f.IdFornitori.ToString()));
            }
        }
        protected void FillDdlScegliAcquirente()
        {
            DataTable dt = OperaiDAO.GetOperai();
            List<Operai> listOperai = dt.DataTableToList<Operai>();

            ddlScegliAcquirente.Items.Clear();
            ddlScegliAcquirente.Items.Add(new System.Web.UI.WebControls.ListItem("", "-1"));

            foreach (Operai op in listOperai)
            {
                ddlScegliAcquirente.Items.Add(new System.Web.UI.WebControls.ListItem(op.NomeOp, op.IdOperaio.ToString()));
            }
        }
        protected void FillAllDdl()
        {
            FillDdlScegliStampa();
            FillDdlScegliFornitore();
            FillDdlScegliAcquirente();
        }

        protected void BindGrid()
        {
            dt = DDTMefDAO.GetDDTForPDF(txtDataDa.Text, txtDataA.Text, ddlScegliAcquirente.SelectedItem.Text, txtNumDDT.Text);
            List<DDTMef> ddtList = dt.DataTableToList<DDTMef>();
            grdStampaDDT.DataSource = ddtList;
            grdStampaDDT.DataBind();
        }

        protected PdfPTable InitializePdfTable()
        {
            float[] columns = { 150f, 220f, 340f, 100f, 150f, 120f };
            PdfPTable table = new PdfPTable(grdStampaDDT.Columns.Count);
            table.WidthPercentage = 100;
            table.SetTotalWidth(columns);

            return table;
        }

        protected void ExportToPdf()
        {
            decimal totale = 0m;
            int numDdtAttuale = 0;
            dt = DDTMefDAO.GetDDTForPDF(txtDataDa.Text, txtDataA.Text, ddlScegliAcquirente.SelectedItem.Text, txtNumDDT.Text);
            List<DDTMef> ddtList = dt.DataTableToList<DDTMef>();

            //Apro lo stream verso il file PDF
            Document pdfDoc = new Document(PageSize.A4, 8f, 2f, 2f, 2f);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();

            PdfPTable table = InitializePdfTable();

            Phrase title = new Phrase(txtNomeFile.Text, FontFactory.GetFont("Arial", 20, iTextSharp.text.Font.BOLD, BaseColor.RED));
            pdfDoc.Add(title);

            GeneraPDFPerNumDDT(pdfDoc, ddtList, title, table, totale, numDdtAttuale);

            pdfDoc.Close();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + txtNomeFile.Text + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();
        }

        protected void GeneraPDFPerNumDDT(Document pdfDoc, List<DDTMef> ddtList, Phrase title, PdfPTable table, decimal totale, int numDdtAttuale)
        {
            Phrase intestazione = new Phrase();
            for (int k = 0; k < ddtList.Count; k++)
            {
                if (numDdtAttuale != ddtList[k].N_ddt)
                {
                    numDdtAttuale = ddtList[k].N_ddt;
                    intestazione = GeneraIntestazione(ddtList, k);

                    //Transfer rows from GridView to table
                    for (int i = 0; i < grdStampaDDT.Columns.Count; i++)
                    {
                        Phrase cellText = new Phrase(Server.HtmlDecode(grdStampaDDT.Columns[i].HeaderText), FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                        PdfPCell cell = new PdfPCell(cellText);
                        cell.BorderWidth = 0;
                        cell.BorderWidthBottom = 1;
                        cell.BorderColorBottom = BaseColor.BLUE;
                        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        table.AddCell(cell);

                        if (i == 4 || i == 5)
                        {
                            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        }
                    }

                    for (int i = 0; i < grdStampaDDT.Rows.Count; i++)
                    {
                        if (grdStampaDDT.Rows[i].RowType == DataControlRowType.DataRow)
                        {
                            for (int j = 0; j < grdStampaDDT.Columns.Count; j++)
                            {
                                if (grdStampaDDT.Rows[i].Cells[0].Text == numDdtAttuale.ToString())
                                {
                                    if (j != 5)
                                    {
                                        Phrase cellText = new Phrase();

                                        if (j == 4)
                                        {
                                            cellText = new Phrase(Server.HtmlDecode(Math.Round(Convert.ToDecimal(grdStampaDDT.Rows[i].Cells[j].Text), 2).ToString()), FontFactory.GetFont("Arial", 10, BaseColor.BLACK));
                                        }
                                        else
                                        {
                                            cellText = new Phrase(Server.HtmlDecode(grdStampaDDT.Rows[i].Cells[j].Text), FontFactory.GetFont("Arial", 10, BaseColor.BLACK));
                                        }

                                        PdfPCell cell = new PdfPCell(cellText);
                                        cell.BorderWidth = 0;

                                        switch (j)
                                        {
                                            case 3:
                                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                                break;
                                            case 4:
                                            case 5:
                                                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                                                break;
                                        }

                                        //Set Color of Alternating row
                                        if (i % 2 != 0)
                                        {
                                            cell.BackgroundColor = new BaseColor(ColorTranslator.FromHtml("#F7F7F7"));
                                        }
                                        table.AddCell(cell);
                                    }
                                    else
                                    {
                                        decimal valore = Convert.ToInt32(Server.HtmlDecode(grdStampaDDT.Rows[i].Cells[3].Text)) * Convert.ToDecimal(Server.HtmlDecode(grdStampaDDT.Rows[i].Cells[4].Text));
                                        Phrase val = new Phrase(Math.Round(valore, 2).ToString(), FontFactory.GetFont("Arial", 10, BaseColor.BLACK));
                                        PdfPCell valCel = new PdfPCell(val);
                                        valCel.HorizontalAlignment = Element.ALIGN_RIGHT;

                                        grdStampaDDT.Rows[i].Cells[5].Text = Math.Round(valore, 2).ToString();
                                        valCel.BorderWidth = 0;

                                        table.AddCell(valCel);
                                        totale += Math.Round(valore, 2);
                                    }
                                }
                                else { break; }
                            }
                        }
                    }

                    PdfPTable tblTotali = new PdfPTable(1);
                    tblTotali.WidthPercentage = 100;

                    GeneraTotali(tblTotali, totale);

                    pdfDoc.Add(new Paragraph(""));
                    pdfDoc.Add(intestazione);
                    pdfDoc.Add(table);
                    pdfDoc.Add(new Paragraph(""));
                    pdfDoc.Add(tblTotali);

                    table = InitializePdfTable();
                    totale = 0m;
                }
                else
                {
                    continue;
                }
            }
        }

        protected Phrase GeneraIntestazione(List<DDTMef> ddtList, int counter)
        {
            string n_ddt = "N_DDT: " + ddtList[counter].N_ddt;
            string acquirente = "Acquirente: " + ddtList[counter].Acquirente;
            string data = "Data: " + ddtList[counter].Data.ToString().Split(' ')[0];
            string intestazioneObj = n_ddt + "    -    " + acquirente + "    -    " + data;

            Phrase intestazione = new Phrase(intestazioneObj, FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.ITALIC, BaseColor.BLUE));

            return intestazione;
        }
        protected void GeneraTotali(PdfPTable tblTotali, decimal totale)
        {
            //Creazione Totali
            //Totale No Iva
            Phrase totaleNoIva = new Phrase("Tot. DDT: " + totale.ToString(), FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.ITALIC, BaseColor.BLUE));
            PdfPCell totNoIvaCell = new PdfPCell(totaleNoIva);

            //IVA
            decimal iva = Math.Round(((totale * 22) / 100), 2);
            Phrase ivaPhrase = new Phrase("IVA: 22%", FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.ITALIC, BaseColor.BLUE));
            PdfPCell ivaCell = new PdfPCell(ivaPhrase);

            //Totale + IVA
            Phrase totaleConIva = new Phrase("Imponibile: " + (totale + iva).ToString(), FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.ITALIC, BaseColor.BLUE));
            PdfPCell totaleConIvaCell = new PdfPCell(totaleConIva);

            //Stile celle totali
            totNoIvaCell.HorizontalAlignment = ivaCell.HorizontalAlignment = totaleConIvaCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            totNoIvaCell.BorderWidth = ivaCell.BorderWidth = totaleConIvaCell.BorderWidth = 0;
            totNoIvaCell.BorderWidthTop = 1;
            totNoIvaCell.BorderColorTop = BaseColor.BLUE;

            tblTotali.AddCell(totNoIvaCell);
            tblTotali.AddCell(ivaCell);
            tblTotali.AddCell(totaleConIvaCell);
        }

        /* EVENTI CLICK */
        protected void btnStampaDDT_Click(object sender, EventArgs e)
        {
            if (txtNomeFile.Text != "") { 
            BindGrid();
            ExportToPdf();
            }
            else
            {
                lblIsNomeFileInserito.Text = "Campo \"Nome File\" obbligatorio!";
                lblIsNomeFileInserito.ForeColor = Color.Red;
            }
        }

        /* EVENTI TEXT-CHANGED */
        protected void ddlScegliStampa_TextChanged(object sender, EventArgs e)
        {
            if (ddlScegliStampa.SelectedIndex != 0)
            {
                pnlCampiStampaDDT.Visible = true;
            }
            else
            {
                pnlCampiStampaDDT.Visible = false;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //Do nothing
        }
    }
}