using GestioneCantieri.DAO;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;

namespace GestioneCantieri
{
    public partial class ResocontoOperaio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDdlScegliAcquirente();
                txtDataDa.Text = "2010-01-01";
                txtDataA.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtDataDa.TextMode = txtDataA.TextMode = TextBoxMode.Date;

                btnPagaOperaio.Visible = false;
            }
        }

        /* HELPERS */
        protected void FillDdlScegliAcquirente()
        {
            DataTable dt = OperaiDAO.GetOperai();
            List<Operai> listOperai = dt.DataTableToList<Operai>();

            ddlScegliOperaio.Items.Clear();
            ddlScegliOperaio.Items.Add(new ListItem("", "-1"));

            foreach (Operai op in listOperai)
            {
                string show = op.Operaio + " - " + op.NomeOp + " - " + op.DescrOp;
                ddlScegliOperaio.Items.Add(new ListItem(show, op.IdOperaio.ToString()));
            }
        }
        protected void BindGrid(bool isFiltered)
        {
            decimal valore = 0m, totValore = 0m;
            int totOre = 0;
            List<MaterialiCantieri> matCantList = new List<MaterialiCantieri>();

            if (!isFiltered)
                matCantList = MaterialiCantieriDAO.GetMatCantPerResocontoOperaio(txtDataDa.Text, txtDataA.Text, ddlScegliOperaio.SelectedItem.Value);
            else
                matCantList = MaterialiCantieriDAO.GetMatCantPerResocontoOperaio(txtDataDa.Text, txtDataA.Text, ddlScegliOperaio.SelectedItem.Value, txtFiltroCantiere.Text, ChkFiltroOperaioPagato.Checked);
            grdResocontoOperaio.DataSource = matCantList;
            grdResocontoOperaio.DataBind();

            //Imposto la colonna del valore
            for (int i = 0; i < grdResocontoOperaio.Rows.Count; i++)
            {
                valore = Convert.ToDecimal(grdResocontoOperaio.Rows[i].Cells[4].Text) * Convert.ToDecimal(grdResocontoOperaio.Rows[i].Cells[5].Text);
                grdResocontoOperaio.Rows[i].Cells[6].Text = Math.Round(valore, 2).ToString();
                totOre += Convert.ToInt32(Convert.ToDecimal(grdResocontoOperaio.Rows[i].Cells[4].Text));
                totValore += Convert.ToDecimal(grdResocontoOperaio.Rows[i].Cells[6].Text);
            }

            lblTotali.Text = "Totale Ore: " + totOre.ToString() + " ||" + "Totale Valore: " + totValore.ToString();
        }

        protected void btnStampaResoconto_Click(object sender, EventArgs e)
        {
            BindGrid(false);
            btnPagaOperaio.Visible = true;
        }
        protected void btnPagaOperaio_Click(object sender, EventArgs e)
        {
            bool isUpdated = MaterialiCantieriDAO.UpdateOperaioPagato(txtDataDa.Text, txtDataA.Text, ddlScegliOperaio.SelectedItem.Value);

            if (isUpdated)
            {
                lblIsOperaioPagato.Text = "Campo \"OperaioPagato\" aggiornato con successo";
                lblIsOperaioPagato.ForeColor = Color.Blue;
            }
            else
            {
                lblIsOperaioPagato.Text = "Impossibile aggiornare il campo \"OperaioPagato\"";
                lblIsOperaioPagato.ForeColor = Color.Red;
            }

            BindGrid(false);
        }

        protected void btnFiltra_Click(object sender, EventArgs e)
        {
            BindGrid(true);
        }
    }
}