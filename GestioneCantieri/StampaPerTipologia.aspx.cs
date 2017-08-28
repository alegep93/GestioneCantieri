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
            decimal totaleOre = 0m;
            List<MaterialiCantieri> mcList = new List<MaterialiCantieri>();

            if (rdbManodop.Checked)
                mcList = MaterialiCantieriDAO.GetMaterialeCantierePerTipologia(ddlScegliCant.SelectedItem.Value, txtDataDa.Text, txtDataA.Text, "MANODOPERA");
            else if (rdbOper.Checked)
                mcList = MaterialiCantieriDAO.GetMaterialeCantierePerTipologia(ddlScegliCant.SelectedItem.Value, txtDataDa.Text, txtDataA.Text, "OPERAIO");

            grdStampaPerTipologia.DataSource = mcList;
            grdStampaPerTipologia.DataBind();

            for (int i = 0; i < grdStampaPerTipologia.Rows.Count; i++)
            {
                decimal valore = Convert.ToDecimal(grdStampaPerTipologia.Rows[i].Cells[4].Text) * Convert.ToDecimal(grdStampaPerTipologia.Rows[i].Cells[5].Text);
                totaleOre += Convert.ToDecimal(grdStampaPerTipologia.Rows[i].Cells[4].Text);
                totale += valore;
            }

            lblTotOre.Text = "<strong>Totale Ore</strong>: " + Math.Round(totaleOre, 2);
            lblTotale.Text = "<strong>Totale Valore</strong>: " + Math.Round(totale, 2);
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