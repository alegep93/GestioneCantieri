using GestioneCantieri.DAO;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestioneCantieri
{
    public partial class StampaExcell : System.Web.UI.Page
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

        /* EVENTI CLICK */
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            CreateExcel();
        }

        /* EVENTI TEXT-CHANGED */
        protected void ddlScegliCantiere_TextChanged(object sender, EventArgs e)
        {
            BindGrid();

            if (ddlScegliCantiere.SelectedIndex != 0)
                btnPrint.Visible = true;
            else
                btnPrint.Visible = false;
        }

        /* HELPERS */
        protected void BindGrid()
        {
            List<StampaOrdFrutCant> list = StampaOrdFrutCantDAO.getAllFruttiInCantiere(ddlScegliCantiere.SelectedItem.Value);
            grdGruppi.DataSource = list;
            grdGruppi.DataBind();
        }
        protected void CreateExcel()
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=OrdFrut-"+ ddlScegliCantiere.SelectedItem.Text +".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();

            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            htmlWrite.WriteLine("<strong><font size='4'>"+ ddlScegliCantiere.SelectedItem.Text +"</font></strong>");

            // viene reindirizzato il rendering verso la stringa in uscita
            grdGruppi.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());

            Response.End();
        }
        protected void FillDdlScegliCantiere()
        {
            List<Cantieri> listCantieri = OrdineFruttiDAO.GetListCantieri();

            ddlScegliCantiere.Items.Clear();

            //Il primo parametro ("") corrisponde al valore e il secondo alla chiave (il valore è quello che viene visualizzato nella form)
            ddlScegliCantiere.Items.Add(new ListItem("", "-1"));

            foreach (Cantieri c in listCantieri)
            {
                string cantiere = c.CodCantiere + " - " + c.DescrCantiere;
                ddlScegliCantiere.Items.Add(new ListItem(cantiere, c.IdCantiere.ToString())); //new ListItem(valore, chiave);
            }
        }

        /* Override per il corretto funzionamento della creazione del foglio Excel */
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
            //Do nothing
        }
    }

}