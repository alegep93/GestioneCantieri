using GestioneCantieri.DAO;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI.WebControls;

namespace GestioneCantieri
{
    public partial class GestisciFrutti : System.Web.UI.Page
    {
        public List<Frutti> fruttiList = new List<Frutti>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDdlFrutti();
                pnlModifica.Visible = pnlElimina.Visible = false;
                pnlModFrutto.Visible = false;
                MostraListaFruttiInseriti();
                lblTitle.Text = "Inserisci Frutto";
                FillDdlFrutti();
                txtInsNomeFrutto.Text = "";
                btnDelFrutto.Visible = false;
                lblIsFruttoInserito.Text = lblSaveModFrutto.Text = lblIsDelFrutto.Text = "";
            }
            MostraListaFruttiInseriti();
        }

        /* EVENTI CLICK */
        protected void btnInsFrutto_Click(object sender, EventArgs e)
        {
            if (txtInsNomeFrutto.Text != "")
            {
                bool isAggiunto = FruttiDAO.InserisciFrutto(txtInsNomeFrutto.Text);

                if (isAggiunto)
                {
                    lblIsFruttoInserito.Text = "Frutto '" + txtInsNomeFrutto.Text + "' inserito correttamente!";
                    lblIsFruttoInserito.ForeColor = Color.Blue;
                }
                else
                {
                    lblIsFruttoInserito.Text = "Esiste già un frutto con lo stesso nome";
                    lblIsFruttoInserito.ForeColor = Color.Red;
                }

                txtInsNomeFrutto.Text = "";
                FillDdlFrutti();
                MostraListaFruttiInseriti();
            }
            else
            {
                lblIsFruttoInserito.Text = "Il campo 'Nome Frutto' deve essere compilato";
                lblIsFruttoInserito.ForeColor = Color.Red;
            }
        }
        protected void btnApriInserisci_Click(object sender, EventArgs e)
        {
            pnlInserisci.Visible = true;
            pnlModifica.Visible = false;
            pnlElimina.Visible = false;
            MostraListaFruttiInseriti();
            lblTitle.Text = "Inserisci Frutto";
            txtInsNomeFrutto.Text = lblIsFruttoInserito.Text = "";
        }
        protected void btnApriModifica_Click(object sender, EventArgs e)
        {
            pnlInserisci.Visible = pnlElimina.Visible = pnlModFrutto.Visible = false;
            pnlModifica.Visible = true;
            MostraListaFruttiInseriti();
            lblTitle.Text = "Modifica Frutto";
            ddlModScegliFrutto.SelectedIndex = -1;
            txtModNomeFrutto.Text = "";
            lblSaveModFrutto.Text = "";
        }
        protected void btnApriElimina_Click(object sender, EventArgs e)
        {
            pnlInserisci.Visible = false;
            pnlModifica.Visible = false;
            pnlElimina.Visible = true;
            MostraListaFruttiInseriti();
            lblTitle.Text = "Elimina Frutto";
            ddlDelFrutto.SelectedIndex = -1;
            btnDelFrutto.Visible = false;
            lblIsDelFrutto.Text = "";
        }
        protected void btnSaveModFrutto_Click(object sender, EventArgs e)
        {
            if (txtModNomeFrutto.Text != "")
            {
                bool isSaved = GruppiFruttiDAO.UpdateFrutto(Convert.ToInt32(ddlModScegliFrutto.SelectedItem.Value), txtModNomeFrutto.Text);
                if (isSaved)
                {
                    lblSaveModFrutto.Text = "Frutto modificato con successo in '" + txtModNomeFrutto.Text + "'";
                    lblSaveModFrutto.ForeColor = Color.Blue;
                }
                else
                {
                    lblSaveModFrutto.Text = "Errore durante la modifica del frutto";
                    lblSaveModFrutto.ForeColor = Color.Red;
                }
                txtModNomeFrutto.Text = "";
                FillDdlFrutti();
                MostraListaFruttiInseriti();
            }
            else
            {
                lblSaveModFrutto.Text = "Il campo 'Nome Frutto' deve essere compilato";
                lblSaveModFrutto.ForeColor = Color.Red;
            }
        }
        protected void btnDelFrutto_Click(object sender, EventArgs e)
        {
            bool isDeleted = GruppiFruttiDAO.DeleteFrutto(Convert.ToInt32(ddlDelFrutto.SelectedItem.Value));
            if (isDeleted)
            {
                lblIsDelFrutto.Text = "Frutto '"+ ddlDelFrutto.SelectedItem.Text +"' eliminato con successo";
                lblIsDelFrutto.ForeColor = Color.Blue;
            }
            else
            {
                lblIsDelFrutto.Text = "Impossibile eliminare il frutto in quanto è referenziato in altre tabelle";
                lblIsDelFrutto.ForeColor = Color.Red;
            }
            ddlDelFrutto.SelectedIndex = -1;
            FillDdlFrutti();
            MostraListaFruttiInseriti();
            btnDelFrutto.Visible = false;
        }

        /* EVENTI TEXT-CAHNGED */
        protected void ddlModScegliFrutto_TextChanged(object sender, EventArgs e)
        {
            if (ddlModScegliFrutto.SelectedIndex != 0)
            {
                pnlModFrutto.Visible = true;
                txtModNomeFrutto.Text = ddlModScegliFrutto.SelectedItem.Text;
            }
            else
                pnlModFrutto.Visible = false;

            lblSaveModFrutto.Text = "";
        }
        protected void txtFiltroFrutti1_TextChanged(object sender, EventArgs e)
        {
            MostraListaFruttiInseriti();
        }
        protected void txtFiltroFrutti2_TextChanged(object sender, EventArgs e)
        {
            MostraListaFruttiInseriti();
        }
        protected void txtFiltroFrutti3_TextChanged(object sender, EventArgs e)
        {
            MostraListaFruttiInseriti();
        }
        protected void ddlDelFrutto_TextChanged(object sender, EventArgs e)
        {
            if (ddlDelFrutto.SelectedItem.Text != "")
                btnDelFrutto.Visible = true;
            else
                btnDelFrutto.Visible = false;

            lblIsDelFrutto.Text = "";
        }

        /* HELPERS */
        protected void FillDdlFrutti()
        {
            List<Frutti> listFrutti = FruttiDAO.getFrutti();
            ddlModScegliFrutto.Items.Clear();
            ddlDelFrutto.Items.Clear();

            //Il primo parametro ("") corrisponde al valore e il secondo alla chiave (il valore è quello che viene visualizzato nella form)
            ddlModScegliFrutto.Items.Add(new ListItem("", "-1"));
            ddlDelFrutto.Items.Add(new ListItem("", "-1"));

            foreach (Frutti f in listFrutti)
            {
                string descrFrutto = f.Descr;
                ddlModScegliFrutto.Items.Add(new ListItem(descrFrutto, f.Id.ToString())); //new ListItem(valore, chiave);
                ddlDelFrutto.Items.Add(new ListItem(descrFrutto, f.Id.ToString())); //new ListItem(valore, chiave);
            }
        }
        protected void MostraListaFruttiInseriti()
        {
            fruttiList = FruttiDAO.getFruttiWithSearch(txtFiltroFrutti1.Text, txtFiltroFrutti2.Text, txtFiltroFrutti3.Text);
        }
    }
}