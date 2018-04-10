using GestioneCantieri.DAO;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace GestioneCantieri
{
    public partial class StampaPerTipologia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDdlScegliCantiere();
                FillDdlScegliOperaio();
            }
        }

        #region Helpers
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
        protected void FillDdlScegliOperaio()
        {
            int i = 0;
            DataTable dt = OperaiDAO.GetOperai();
            List<Operai> listOperai = dt.DataTableToList<Operai>();

            ddlScegliOperaio.Items.Clear();
            ddlScegliOperaio.Items.Add(new ListItem("", "-1"));

            foreach (Operai op in listOperai)
            {
                string show = op.NomeOp + " - " + op.DescrOp;
                ddlScegliOperaio.Items.Add(new ListItem(show, op.IdOperaio.ToString()));

                i++;
                if (op.NomeOp == "Maurizio" || op.NomeOp == "Mau" || op.NomeOp == "MAU")
                {
                    ddlScegliOperaio.SelectedIndex = i;
                }
            }
        }
        protected void BindGrid()
        {
            decimal totale = 0m;
            decimal totaleOre = 0m;
            string idCantiere = ddlScegliCant.SelectedItem.Value;
            string idOperaio = ddlScegliOperaio.SelectedItem.Value;
            List<MaterialiCantieri> mcList = new List<MaterialiCantieri>();

            if (rdbManodop.Checked)
                mcList = MaterialiCantieriDAO.GetMaterialeCantierePerTipologia(idCantiere, txtDataDa.Text, txtDataA.Text, idOperaio, "MANODOPERA");
            else if (rdbOper.Checked)
                mcList = MaterialiCantieriDAO.GetMaterialeCantierePerTipologia(idCantiere, txtDataDa.Text, txtDataA.Text, idOperaio, "OPERAIO");

            grdStampaPerTipologia.DataSource = mcList;
            grdStampaPerTipologia.DataBind();

            for (int i = 0; i < grdStampaPerTipologia.Rows.Count; i++)
            {
                decimal qta = Convert.ToDecimal(grdStampaPerTipologia.Rows[i].Cells[4].Text);
                decimal prezzoUnitario = Convert.ToDecimal(grdStampaPerTipologia.Rows[i].Cells[5].Text);
                decimal valore = qta * prezzoUnitario;

                totaleOre += qta;
                totale += valore;
            }

            lblTotOre.Text = "<strong>Totale Ore</strong>: " + Math.Round(totaleOre, 2);
            lblTotale.Text = "<strong>Totale Valore</strong>: " + Math.Round(totale, 2);
        }
        #endregion

        #region Eventi Click
        protected void btnFiltraCantieri_Click(object sender, EventArgs e)
        {
            FillDdlScegliCantiere();
        }
        protected void btnStampaVerificaCant_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion
    }
}