using GestioneCantieri.DAO;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace GestioneCantieri
{
    public partial class StampaPerTipologia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDdlScegliCantiere();
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
            decimal totale = 0m;

            List<MaterialiCantieri> mcList = new List<MaterialiCantieri>();
            if (rdbManodop.Checked)
                mcList = MaterialiCantieriDAO.GetMaterialeCantierePerTipologia(ddlScegliCant.SelectedItem.Value, "MANODOPERA");
            else if (rdbOper.Checked)
                mcList = MaterialiCantieriDAO.GetMaterialeCantierePerTipologia(ddlScegliCant.SelectedItem.Value, "OPERAIO");

            grdStampaPerTipologia.DataSource = mcList;
            grdStampaPerTipologia.DataBind();

            for (int i = 0; i < grdStampaPerTipologia.Rows.Count; i++)
            {
                decimal valore = Convert.ToInt32(grdStampaPerTipologia.Rows[i].Cells[4].Text) * Convert.ToDecimal(grdStampaPerTipologia.Rows[i].Cells[5].Text);
                totale += valore;
            }

            lblTotale.Text = "<strong>Totale</strong>: " + Math.Round(totale,2);
        }

        protected void btnFiltraCantieri_Click(object sender, EventArgs e)
        {
            FillDdlScegliCantiere();
        }
        protected void btnStampaVerificaCant_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}