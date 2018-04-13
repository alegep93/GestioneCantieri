using GestioneCantieri.DAO;
using GestioneCantieri.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestioneCantieri
{
    public partial class StampeVarie : Page
    {
        protected DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime firstDayOfCurrentYear = new DateTime(DateTime.Now.Year, 1, 1);
                FillAllDdl();
                pnlCampiStampaDDT_MatCant.Visible = false;
                pnlCampiStampaCliente.Visible = false;
                txtDataDa.Text = firstDayOfCurrentYear.ToString("yyyy-MM-dd");
                txtDataA.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtDataDa.TextMode = txtDataA.TextMode = TextBoxMode.Date;
            }
        }

        #region Helpers
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
            DataTable dt = FornitoriDAO.GetFornitoriDataTable();
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
        protected void FillAllDdl()
        {
            FillDdlScegliStampa();
            FillDdlScegliFornitore();
            FillDdlScegliAcquirente();
            FillDdlScegliCantiere();
        }

        //Popola la griglia con i dati da SQL
        protected void BindGridStampaDDT()
        {
            dt = DDTMefDAO.GetDDTForPDF(txtDataDa.Text, txtDataA.Text, ddlScegliAcquirente.SelectedItem.Text, txtNumDDT.Text);
            List<DDTMef> ddtList = dt.DataTableToList<DDTMef>();
            grdStampaDDT.DataSource = ddtList;
            grdStampaDDT.DataBind();
        }
        protected void BindGridStampaMatCant()
        {
            List<MaterialiCantieri> matCantList = MaterialiCantieriDAO.GetMaterialeCantiere(txtDataDa.Text, txtDataA.Text, ddlScegliAcquirente.SelectedItem.Text, ddlScegliFornitore.SelectedItem.Text, txtNumDDT.Text);
            grdStampaMateCant.DataSource = matCantList;
            grdStampaMateCant.DataBind();
        }
        #endregion

        #region Stampa PDF
        protected PdfPTable InitializePdfTableDDT()
        {
            float[] columns = { 150f, 220f, 340f, 100f, 150f, 120f };
            PdfPTable table = new PdfPTable(grdStampaDDT.Columns.Count);
            table.WidthPercentage = 100;
            table.SetTotalWidth(columns);

            return table;
        }
        protected PdfPTable InitializePdfTableMatCant()
        {
            float[] columns = { 170f, 130f, 170f, 160f, 140f, 240f, 110f, 150f, 130f };
            PdfPTable table = new PdfPTable(grdStampaMateCant.Columns.Count);
            table.WidthPercentage = 100;
            table.SetTotalWidth(columns);

            return table;
        }

        protected void AddPageNumber()
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1];
            byte[] bytes = File.ReadAllBytes(@"C:\\Users\\" + userName + "\\Downloads\\" + txtNomeFile.Text + ".pdf");
            iTextSharp.text.Font blackFont = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            using (MemoryStream stream = new MemoryStream())
            {
                PdfReader reader = new PdfReader(bytes);
                using (PdfStamper stamper = new PdfStamper(reader, stream))
                {
                    int pages = reader.NumberOfPages;
                    for (int i = 1; i <= pages; i++)
                    {
                        ColumnText.ShowTextAligned(stamper.GetOverContent(i), Element.ALIGN_RIGHT, new Phrase(i.ToString() + " / " + pages.ToString(), blackFont), 570f, 15f, 0);
                    }
                }
                bytes = stream.ToArray();
            }
            File.WriteAllBytes(@"C:\\Users\\" + userName + "\\Downloads\\" + txtNomeFile.Text + ".pdf", bytes);
        }

        protected void ExportToPdfPerDDT()
        {
            decimal totale = 0m;
            decimal totaleFinale = 0m;
            int numDdtAttuale = 0;

            dt = DDTMefDAO.GetDDTForPDF(txtDataDa.Text, txtDataA.Text, ddlScegliAcquirente.SelectedItem.Text, txtNumDDT.Text);
            List<DDTMef> ddtList = dt.DataTableToList<DDTMef>();

            //Apro lo stream verso il file PDF
            Document pdfDoc = new Document(PageSize.A4, 8f, 2f, 0f, 10f);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();

            PdfPTable table = InitializePdfTableDDT();

            Phrase title = new Phrase(txtNomeFile.Text, FontFactory.GetFont("Arial", 22, iTextSharp.text.Font.BOLD, BaseColor.RED));
            pdfDoc.Add(title);

            GeneraPDFPerNumDDT(pdfDoc, ddtList, title, table, totale, numDdtAttuale, totaleFinale);

            pdfDoc.Close();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + txtNomeFile.Text + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();
        }
        protected void ExportToPdfPerMatCant()
        {
            decimal totale = 0m;
            decimal totaleFinale = 0m;
            int numDdtAttuale = 0;
            List<MaterialiCantieri> matCantList = MaterialiCantieriDAO.GetMaterialeCantiere(txtDataDa.Text, txtDataA.Text, ddlScegliAcquirente.SelectedItem.Text, ddlScegliFornitore.SelectedItem.Text, txtNumDDT.Text);

            //Apro lo stream verso il file PDF
            Document pdfDoc = new Document(PageSize.A4, 8f, 2f, 2f, 2f);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();

            PdfPTable table = InitializePdfTableMatCant();

            Phrase title = new Phrase(txtNomeFile.Text, FontFactory.GetFont("Arial", 24, iTextSharp.text.Font.BOLD, BaseColor.RED));
            pdfDoc.Add(title);

            GeneraPDFPerMatCant(pdfDoc, matCantList, title, table, totale, numDdtAttuale, totaleFinale);

            pdfDoc.Close();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + txtNomeFile.Text + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();
        }
        protected void GeneraPDFPerNumDDT(Document pdfDoc, List<DDTMef> ddtList, Phrase title, PdfPTable table, decimal totale, int numDdtAttuale, decimal totFin)
        {
            PdfPTable tblTotali = null;
            Phrase intestazione = new Phrase();
            for (int k = 0; k < ddtList.Count; k++)
            {
                if (numDdtAttuale != ddtList[k].N_ddt)
                {
                    numDdtAttuale = ddtList[k].N_ddt;
                    intestazione = GeneraIntestazioneDDT(ddtList, k);

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
                                        decimal valore = Convert.ToDecimal(Server.HtmlDecode(grdStampaDDT.Rows[i].Cells[3].Text)) * Convert.ToDecimal(Server.HtmlDecode(grdStampaDDT.Rows[i].Cells[4].Text));
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

                    tblTotali = new PdfPTable(1);
                    tblTotali.WidthPercentage = 100;

                    GeneraTotalePerNumDDT(tblTotali, totale);

                    pdfDoc.Add(new Paragraph(""));
                    pdfDoc.Add(intestazione);
                    pdfDoc.Add(table);
                    pdfDoc.Add(new Paragraph(""));
                    pdfDoc.Add(tblTotali);

                    totFin += totale;
                    table = InitializePdfTableDDT();
                    totale = 0m;
                }
                else
                {
                    continue;
                }
            }

            tblTotali = new PdfPTable(1);
            tblTotali.WidthPercentage = 100;
            GeneraTotaliFinali(tblTotali, totFin);
            pdfDoc.Add(new Paragraph(""));
            pdfDoc.Add(tblTotali);
        }
        protected void GeneraPDFPerMatCant(Document pdfDoc, List<MaterialiCantieri> matCantList, Phrase title, PdfPTable table, decimal totale, int numDdtAttuale, decimal totFin)
        {
            PdfPTable tblTotali = null;
            Phrase intestazione = new Phrase();
            for (int k = 0; k < matCantList.Count; k++)
            {
                if (numDdtAttuale != Convert.ToInt32(matCantList[k].NumeroBolla))
                {
                    numDdtAttuale = Convert.ToInt32(matCantList[k].NumeroBolla);
                    intestazione = GeneraIntestazioneMatCant(matCantList, k);

                    //Transfer rows from GridView to table
                    for (int i = 0; i < grdStampaMateCant.Columns.Count; i++)
                    {
                        Phrase cellText = new Phrase(Server.HtmlDecode(grdStampaMateCant.Columns[i].HeaderText), FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
                        PdfPCell cell = new PdfPCell(cellText);
                        cell.BorderWidth = 0;
                        cell.BorderWidthBottom = 1;
                        cell.BorderColorBottom = BaseColor.BLUE;
                        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        table.AddCell(cell);

                        if (i == 8 || i == 9)
                        {
                            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                        }
                    }

                    for (int i = 0; i < grdStampaMateCant.Rows.Count; i++)
                    {
                        if (grdStampaMateCant.Rows[i].RowType == DataControlRowType.DataRow)
                        {
                            for (int j = 0; j < grdStampaMateCant.Columns.Count; j++)
                            {
                                if (grdStampaMateCant.Rows[i].Cells[0].Text == numDdtAttuale.ToString())
                                {
                                    if (j != 8)
                                    {
                                        Phrase cellText = new Phrase();

                                        if (j == 7)
                                        {
                                            cellText = new Phrase(Server.HtmlDecode(String.Format("{0:n}", Math.Round(Convert.ToDecimal(grdStampaMateCant.Rows[i].Cells[j].Text)), 2).ToString()), FontFactory.GetFont("Arial", 10, BaseColor.BLACK));
                                        }
                                        else
                                        {
                                            cellText = new Phrase(Server.HtmlDecode(grdStampaMateCant.Rows[i].Cells[j].Text), FontFactory.GetFont("Arial", 10, BaseColor.BLACK));
                                        }

                                        PdfPCell cell = new PdfPCell(cellText);
                                        cell.BorderWidth = 0;

                                        switch (j)
                                        {
                                            case 6:
                                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                                break;
                                            case 7:
                                            case 8:
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
                                        decimal valore = Convert.ToDecimal(Server.HtmlDecode(grdStampaMateCant.Rows[i].Cells[6].Text)) * Convert.ToDecimal(Server.HtmlDecode(grdStampaMateCant.Rows[i].Cells[7].Text));
                                        Phrase val = new Phrase(String.Format("{0:n}", Math.Round(valore, 2)), FontFactory.GetFont("Arial", 10, BaseColor.BLACK));
                                        PdfPCell valCel = new PdfPCell(val);
                                        valCel.HorizontalAlignment = Element.ALIGN_RIGHT;

                                        grdStampaMateCant.Rows[i].Cells[8].Text = String.Format("{0:n}", Math.Round(valore, 2));
                                        valCel.BorderWidth = 0;

                                        table.AddCell(valCel);
                                        totale += Math.Round(valore, 2);
                                    }
                                }
                                else { break; }
                            }
                        }
                    }

                    tblTotali = new PdfPTable(1);
                    tblTotali.WidthPercentage = 100;

                    GeneraTotalePerNumDDT(tblTotali, totale);

                    pdfDoc.Add(new Paragraph(""));
                    pdfDoc.Add(intestazione);
                    pdfDoc.Add(table);
                    pdfDoc.Add(new Paragraph(""));
                    pdfDoc.Add(tblTotali);

                    totFin += totale;
                    table = InitializePdfTableMatCant();
                    totale = 0m;
                }
                else
                {
                    continue;
                }
            }

            /* Genero il Totale finale */
            //tblTotali = new PdfPTable(1);
            //tblTotali.WidthPercentage = 100;
            //GeneraTotaliFinali(tblTotali, totFin);
            //pdfDoc.Add(new Paragraph(""));
            //pdfDoc.Add(tblTotali);
        }

        //Intestazione PDF
        protected Phrase GeneraIntestazioneDDT(List<DDTMef> ddtList, int counter)
        {
            string n_ddt = "N_DDT: " + ddtList[counter].N_ddt;
            string acquirente = "Acquirente: " + ddtList[counter].Acquirente;
            string data = "Data: " + ddtList[counter].Data.ToString().Split(' ')[0];
            string intestazioneObj = n_ddt + "    -    " + acquirente + "    -    " + data;

            Phrase intestazione = new Phrase(intestazioneObj, FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.ITALIC, BaseColor.BLUE));

            return intestazione;
        }
        protected Phrase GeneraIntestazioneMatCant(List<MaterialiCantieri> ddtList, int counter)
        {
            string n_ddt = "Num Bolla: " + ddtList[counter].NumeroBolla;
            string data = "Data: " + ddtList[counter].Data.ToString().Split(' ')[0];
            string intestazioneObj = n_ddt + "    -    " + data;

            Phrase intestazione = new Phrase(intestazioneObj, FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.ITALIC, BaseColor.BLUE));

            return intestazione;
        }

        protected void GeneraTotalePerNumDDT(PdfPTable tblTotali, decimal totale)
        {
            //Creazione Totali
            //Totale No Iva
            Phrase totalePerNumDDT = new Phrase("Totale: " + String.Format("{0:n}", totale), FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.ITALIC, BaseColor.BLUE));
            PdfPCell totPerNumDDTCell = new PdfPCell(totalePerNumDDT);

            totPerNumDDTCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            totPerNumDDTCell.BorderWidth = 0;
            totPerNumDDTCell.BorderWidthTop = 1;
            totPerNumDDTCell.BorderColorTop = BaseColor.BLUE;
            totPerNumDDTCell.PaddingBottom = 20;

            tblTotali.AddCell(totPerNumDDTCell);
        }
        protected void GeneraTotaliFinali(PdfPTable tblTotali, decimal totaleFinale)
        {
            Phrase totFinNoIva = new Phrase("Tot: " + String.Format("{0:n}", totaleFinale), FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.ITALIC, BaseColor.BLUE));
            PdfPCell totFinNoIvaCell = new PdfPCell(totFinNoIva);

            //IVA
            decimal iva = Math.Round(((totaleFinale * 22) / 100), 2);
            Phrase ivaPhrase = new Phrase("IVA 22%: " + iva, FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.ITALIC, BaseColor.BLUE));
            PdfPCell ivaCell = new PdfPCell(ivaPhrase);

            //Totale + IVA
            Phrase totaleConIva = new Phrase("Imponibile: " + String.Format("{0:n}", (totaleFinale + iva)), FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.ITALIC, BaseColor.BLUE));
            PdfPCell totaleConIvaCell = new PdfPCell(totaleConIva);

            //Stile celle totali
            totFinNoIvaCell.HorizontalAlignment = ivaCell.HorizontalAlignment = totaleConIvaCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            totFinNoIvaCell.BorderWidth = ivaCell.BorderWidth = totaleConIvaCell.BorderWidth = 0;
            totFinNoIvaCell.BorderColorTop = BaseColor.BLUE;
            totFinNoIvaCell.BorderWidthTop = 1;

            tblTotali.AddCell(totFinNoIvaCell);
            tblTotali.AddCell(ivaCell);
            tblTotali.AddCell(totaleConIvaCell);
        }
        #endregion

        #region Eventi Click
        /* EVENTI CLICK */
        protected void btnStampaDDT_Click(object sender, EventArgs e)
        {
            lblIsNomeFileInserito.Text = "";
            if (txtNomeFile.Text != "")
            {
                if (txtNumDDT.Text != "")
                {
                    if (DDTMefDAO.CheckIfDdtExistBetweenData(txtNumDDT.Text, txtDataDa.Text, txtDataA.Text))
                    {
                        BindGridStampaDDT();
                        ExportToPdfPerDDT();
                    }
                    else
                    {
                        lblIsNomeFileInserito.Text = "Il DDT cercato NON è presente nella lista dei DDT";
                        lblIsNomeFileInserito.ForeColor = Color.Red;
                    }
                }
                else
                {
                    BindGridStampaDDT();
                    ExportToPdfPerDDT();
                }
            }
            else
            {
                lblIsNomeFileInserito.Text = "Campo \"Nome File\" obbligatorio!";
                lblIsNomeFileInserito.ForeColor = Color.Red;
            }
        }
        protected void btnStampaMatCant_Click(object sender, EventArgs e)
        {
            if (txtNomeFile.Text != "")
            {
                BindGridStampaMatCant();
                ExportToPdfPerMatCant();
                AddPageNumber();
            }
            else
            {
                lblIsNomeFileInserito.Text = "Campo \"Nome File\" obbligatorio!";
                lblIsNomeFileInserito.ForeColor = Color.Red;
            }
        }
        protected void btnFiltraCantieri_Click(object sender, EventArgs e)
        {
            FillDdlScegliCantiere();
        }
        protected void btnAggiungiNumPagine_Click(object sender, EventArgs e)
        {
            AddPageNumber();
        }

        /*********************** INUTILIZZATO ******************************/
        protected void btnStampaCliente_Click(object sender, EventArgs e)
        {
            //if (txtNomeFileStampaCliente.Text != "")
            //{
            //    BindGridStampaCliente();
            //    ExportToPdfPerCliente();
            //}
            //else
            //{
            //    lblIsNomeFileInserito.Text = "Campo \"Nome File\" obbligatorio!";
            //    lblIsNomeFileInserito.ForeColor = Color.Red;
            //}
        }
        #endregion

        #region Eventi TextChanged
        /* EVENTI TEXT-CHANGED */
        protected void ddlScegliStampa_TextChanged(object sender, EventArgs e)
        {
            int index = ddlScegliStampa.SelectedIndex;
            switch (index)
            {
                case 0:
                    pnlCampiStampaDDT_MatCant.Visible = false;
                    pnlCampiStampaCliente.Visible = false;
                    break;
                case 1:
                    pnlCampiStampaDDT_MatCant.Visible = true;
                    pnlCampiStampaCliente.Visible = false;
                    btnStampaDDT.Visible = true;
                    btnStampaMatCant.Visible = false;
                    break;
                case 2:
                    pnlCampiStampaDDT_MatCant.Visible = true;
                    pnlCampiStampaCliente.Visible = false;
                    btnStampaDDT.Visible = false;
                    btnStampaMatCant.Visible = true;
                    break;
                case 3:
                    pnlCampiStampaCliente.Visible = true;
                    pnlCampiStampaDDT_MatCant.Visible = false;
                    btnStampaDDT.Visible = false;
                    btnStampaMatCant.Visible = false;
                    break;
            }
        }
        #endregion

        public override void VerifyRenderingInServerForm(Control control)
        {
            //Do nothing
        }
    }
}