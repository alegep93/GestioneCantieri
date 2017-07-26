using GestioneCantieri.DAO;
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
                btnStampaDDT.Visible = false;
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

        protected void ExportToPdf(DataTable dt)
        {
            PdfPTable table = new PdfPTable(grdStampaDDT.Columns.Count);
            table.WidthPercentage = 95;

            //Transfer rows from GridView to table
            for (int i = 0; i < grdStampaDDT.Columns.Count; i++)
            {
                Phrase cellText = new Phrase(Server.HtmlDecode(grdStampaDDT.Columns[i].HeaderText), FontFactory.GetFont("Arial", 12, BaseColor.WHITE));
                PdfPCell cell = new PdfPCell(cellText);
                cell.BackgroundColor = new BaseColor(ColorTranslator.FromHtml("#B2DAFF"));
                table.AddCell(cell);
            }

            for (int i = 0; i < grdStampaDDT.Rows.Count; i++)
            {
                if (grdStampaDDT.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    for (int j = 0; j < grdStampaDDT.Columns.Count; j++)
                    {
                        Phrase cellText = new Phrase(Server.HtmlDecode(grdStampaDDT.Rows[i].Cells[j].Text), FontFactory.GetFont("Arial", 10, BaseColor.BLACK));
                        PdfPCell cell = new PdfPCell(cellText);

                        //Set Color of Alternating row
                        if (i % 2 != 0)
                        {
                            cell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#F7F7F7"));
                        }
                        table.AddCell(cell);
                    }
                }
            }

            Document pdfDoc = new Document(PageSize.A4, 2f, 2f, 2f, 2f);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            pdfDoc.Add(table);
            pdfDoc.Close();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + txtNomeFile.Text + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();
        }

        /* EVENTI CLICK */
        protected void btnShowGridView_Click(object sender, EventArgs e)
        {
            BindGrid();
            btnStampaDDT.Visible = true;
        }
        protected void btnStampaDDT_Click(object sender, EventArgs e)
        {
            ExportToPdf(dt);
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