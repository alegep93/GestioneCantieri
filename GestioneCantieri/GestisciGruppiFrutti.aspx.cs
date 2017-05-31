using GestioneCantieri.DAO;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace GestioneCantieri
{
    public partial class DistintaBase : System.Web.UI.Page
    {
        /* Liste pubbliche per la visualizzazione dinamica di record */
        public List<CompGruppoFrut> compList = new List<CompGruppoFrut>();
        public List<GruppiFrutti> gruppiList = new List<GruppiFrutti>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtNomeGruppo.Text = "";
                lblTitle.Text = "Inserisci Gruppo";
                nuovoFruttoPanel.Visible = false;
                pnlModifica.Visible = pnlElimina.Visible = false;
                pnlModGruppo.Visible = pnlDelCompGrup.Visible = false;
                FillDdlFrutti();
                FillDdlGruppi();
                FillDdlGruppiNonCompletati();
            }
            MostraListaGruppiInseriti();
        }

        /* EVENTI CLICK */
        protected void btnCreaGruppo_Click(object sender, EventArgs e)
        {
            if (txtNomeGruppo.Text != "")
            {
                bool insert = GestisciGruppiFruttiDAO.CreaGruppo(txtNomeGruppo.Text, txaDescr.Text);

                if (insert)
                {
                    lblInserimento.Text = "Gruppo '" + txtNomeGruppo.Text + "' creato con successo";
                    lblInserimento.ForeColor = Color.Blue;
                }
                else
                {
                    lblInserimento.Text = "Esiste già un gruppo con nome '" + txtNomeGruppo.Text + "'";
                    lblInserimento.ForeColor = Color.Red;
                }
                FillDdlGruppiNonCompletati();
                MostraListaGruppiInseriti();
                txtNomeGruppo.Text = txaDescr.Text = "";
            }
            else
            {
                lblInserimento.Text = "Il campo 'Nome Gruppo' deve essere compilato";
                lblInserimento.ForeColor = Color.Red;
            }
        }
        protected void btnInsCompgruppo_Click(object sender, EventArgs e)
        {
            bool isAggiunto = GestisciGruppiFruttiDAO.InserisciCompGruppo(Convert.ToInt32(ddlGruppi.SelectedItem.Value), Convert.ToInt32(ddlFrutti.SelectedItem.Value), txtQta.Text);

            if (isAggiunto)
            {
                lblFruttoAggiungo.Text = "Componente '" + ddlFrutti.SelectedItem.Text + "' aggiunto correttamente!";
                lblFruttoAggiungo.ForeColor = Color.Blue;
            }
            else
            {
                lblFruttoAggiungo.Text = "Errore durante l'inserimento del componente";
                lblFruttoAggiungo.ForeColor = Color.Red;
            }

            compList = GestisciGruppiFruttiDAO.getCompGruppo(Convert.ToInt32(ddlGruppi.SelectedItem.Value));
            ddlFrutti.SelectedIndex = 0;
            txtQta.Text = "";
        }
        protected void btnApriInserisci_Click(object sender, EventArgs e)
        {
            pnlInserisci.Visible = true;
            pnlModifica.Visible = false;
            pnlElimina.Visible = false;
            lblTitle.Text = "Inserisci Gruppo";
            lblInserimento.Text = "";
        }
        protected void btnApriModifica_Click(object sender, EventArgs e)
        {
            pnlInserisci.Visible = pnlElimina.Visible = pnlModGruppo.Visible = false;
            pnlModifica.Visible = true;
            FillDdlGruppi();
            lblTitle.Text = "Modifica Gruppo";
            txtModNomeGruppo.Text = txtModDescrGruppo.Text = "";
            btnSaveModGruppo.Visible = false;
            lblSaveModGruppo.Text = "";
        }
        protected void btnApriElimina_Click(object sender, EventArgs e)
        {
            pnlInserisci.Visible = false;
            pnlModifica.Visible = false;
            pnlElimina.Visible = true;
            FillDdlGruppi();
            lblTitle.Text = "Elimina Gruppo";
            btnDelGruppo.Visible = false;
            txtDelDescrGruppo.Text = "";
            ddlDelCompGrup.SelectedIndex = -1;
            btnDelCompGruppo.Visible = false;
            lblIsDelGruppo.Text = lblIsDelCompGruppo.Text = "";
        }
        protected void btnSaveModGruppo_Click(object sender, EventArgs e)
        {
            bool isSaved = GestisciGruppiFruttiDAO.UpdateGruppo(Convert.ToInt32(ddlModScegliGruppo.SelectedItem.Value), txtModNomeGruppo.Text, txtModDescrGruppo.Text);
            if (isSaved)
            {
                lblSaveModGruppo.Text = "Nome gruppo modificato con successo in '" + txtModNomeGruppo.Text + "'";
                lblSaveModGruppo.ForeColor = Color.Blue;
            }
            else
            {
                lblSaveModGruppo.Text = "Errore durante la modifica del nome gruppo";
                lblSaveModGruppo.ForeColor = Color.Red;
            }
            FillDdlGruppi();
            MostraListaGruppiInseriti();
            txtModNomeGruppo.Text = txtModDescrGruppo.Text = "";
            ddlModScegliGruppo.SelectedIndex = -1;
        }
        protected void btnDelGruppo_Click(object sender, EventArgs e)
        {
            bool isDeleted = GestisciGruppiFruttiDAO.DeleteGruppo(Convert.ToInt32(ddlDelGruppo.SelectedItem.Value));
            if (isDeleted)
            {
                lblIsDelGruppo.Text = "Gruppo '" + ddlDelGruppo.SelectedItem.Text + "' eliminato con successo";
                lblIsDelGruppo.ForeColor = Color.Blue;
            }
            else
            {
                lblIsDelGruppo.Text = "Impossibile eliminare il gruppo '" + ddlDelGruppo.SelectedItem.Text + "' in quanto è referenziato in altre tabelle";
                lblIsDelGruppo.ForeColor = Color.Red;
            }
            FillDdlGruppi();
            MostraListaGruppiInseriti();
            txtDelDescrGruppo.Text = "";
        }
        protected void btnDelCompGruppo_Click(object sender, EventArgs e)
        {
            bool isDeleted = GestisciGruppiFruttiDAO.DeleteCompGruppo(Convert.ToInt32(ddlDelCompGrup.SelectedItem.Value));
            if (isDeleted)
            {
                lblIsDelCompGruppo.Text = "Componente '" + ddlDelCompGrup.SelectedItem.Text + "' eliminato con successo dal gruppo '" + ddlDelNomeGruppo.SelectedItem.Text + "'";
                lblIsDelCompGruppo.ForeColor = Color.Blue;
            }
            else
            {
                lblIsDelCompGruppo.Text = "Impossibile eliminare il componente del gruppo '" + ddlDelCompGrup.SelectedItem.Text + "'";
                lblIsDelCompGruppo.ForeColor = Color.Red;
            }
            compList = GestisciGruppiFruttiDAO.getCompGruppo(Convert.ToInt32(ddlDelNomeGruppo.SelectedItem.Value));
            ddlDelCompGrup.SelectedItem.Text = "";
        }
        protected void btnCompletaGruppo_Click(object sender, EventArgs e)
        {
            bool isClosed = GestisciGruppiFruttiDAO.CompletaRiapriGruppo(ddlGruppi.SelectedItem.Value, true);
            if (isClosed)
            {
                lblFruttoAggiungo.Text = "Gruppo '" + ddlGruppi.SelectedItem.Text + "' chiuso con successo";
                lblFruttoAggiungo.ForeColor = Color.Blue;
            }
            else
            {
                lblFruttoAggiungo.Text = "Non è stato possibile chiudere il gruppo '" + ddlGruppi.SelectedItem.Text + "'";
                lblFruttoAggiungo.ForeColor = Color.Red;
            }
            FillDdlGruppiNonCompletati();
            txaShowDescrGruppo.Text = "";
        }
        protected void btnRiapriGruppo_Click(object sender, EventArgs e)
        {
            bool isOpen = GestisciGruppiFruttiDAO.CompletaRiapriGruppo(ddlModScegliGruppo.SelectedItem.Value, false);
            if (isOpen)
            {
                lblSaveModGruppo.Text = "Gruppo '" + ddlModScegliGruppo.SelectedItem.Text + "' aperto con successo";
                lblSaveModGruppo.ForeColor = Color.Blue;
            }
            else
            {
                lblSaveModGruppo.Text = "Non è stato possibile aprire il gruppo '" + ddlModScegliGruppo.SelectedItem.Text + "'";
                lblSaveModGruppo.ForeColor = Color.Red;
            }
            FillDdlGruppiNonCompletati();
            txtModNomeGruppo.Text = txtModDescrGruppo.Text = "";
        }

        /* EVENTI TEXT-CHANGED */
        protected void txtFiltroGruppi3_TextChanged(object sender, EventArgs e)
        {
            MostraListaGruppiInseriti();
        }
        protected void txtFiltroGruppi1_TextChanged(object sender, EventArgs e)
        {
            MostraListaGruppiInseriti();
        }
        protected void txtFiltroGruppi2_TextChanged(object sender, EventArgs e)
        {
            MostraListaGruppiInseriti();
        }
        protected void ddlGruppi_TextChanged(object sender, EventArgs e)
        {

            if (ddlGruppi.SelectedItem.Text == "")
                nuovoFruttoPanel.Visible = false;
            else
            {
                txaShowDescrGruppo.Text = GestisciGruppiFruttiDAO.getDescrGruppo(Convert.ToInt32(ddlGruppi.SelectedItem.Value));
                nuovoFruttoPanel.Visible = true;
                compList = GestisciGruppiFruttiDAO.getCompGruppo(Convert.ToInt32(ddlGruppi.SelectedItem.Value));
            }
        }
        protected void ddlModMostraGruppi_TextChanged(object sender, EventArgs e)
        {
            if (ddlModScegliGruppo.SelectedItem.Value != "")
            {
                btnSaveModGruppo.Visible = true;
                pnlModGruppo.Visible = true;
                txtModNomeGruppo.Text = ddlModScegliGruppo.SelectedItem.Text;
                txtModDescrGruppo.Text = GestisciGruppiFruttiDAO.getDescrGruppo(Convert.ToInt32(ddlModScegliGruppo.SelectedItem.Value));
                compList = GestisciGruppiFruttiDAO.getCompGruppo(Convert.ToInt32(ddlModScegliGruppo.SelectedItem.Value));

                bool isOpen = GestisciGruppiFruttiDAO.isGruppoAperto(ddlModScegliGruppo.SelectedItem.Value);
                if (isOpen)
                    btnRiapriGruppo.Visible = false;
                else
                    btnRiapriGruppo.Visible = true;
            }
        }
        protected void ddlDelNomeGruppo_TextChanged(object sender, EventArgs e)
        {
            if (ddlDelNomeGruppo.SelectedItem.Value != "")
            {
                pnlDelCompGrup.Visible = true;
                txtDelDescrGruppo.Text = GestisciGruppiFruttiDAO.getDescrGruppo(Convert.ToInt32(ddlDelNomeGruppo.SelectedItem.Value));
                fillDdlCompGruppo();
                compList = GestisciGruppiFruttiDAO.getCompGruppo(Convert.ToInt32(ddlDelNomeGruppo.SelectedItem.Value));
                btnDelCompGruppo.Visible = false;
            }
            else
            {
                btnDelCompGruppo.Visible = true;
            }
        }
        protected void ddlDelGruppo_TextChanged(object sender, EventArgs e)
        {
            if (ddlDelGruppo.SelectedItem.Text != "")
            {
                btnDelGruppo.Visible = true;
            }
            else
            {
                btnDelGruppo.Visible = false;
            }
        }
        protected void ddlDelCompGrup_TextChanged(object sender, EventArgs e)
        {
            if (ddlDelCompGrup.SelectedItem.Text != "")
                btnDelCompGruppo.Visible = true;
            else
                btnDelCompGruppo.Visible = false;
        }

        /* HELPERS */
        protected void FillDdlFrutti()
        {
            List<Frutti> listFrutti = GestisciGruppiFruttiDAO.getFrutti();

            ddlFrutti.Items.Clear();

            //Il primo parametro ("") corrisponde al valore e il secondo alla chiave (il valore è quello che viene visualizzato nella form)
            ddlFrutti.Items.Add(new ListItem("", "-1"));

            foreach (Frutti f in listFrutti)
            {
                string descrFrutto = f.Descr;
                ddlFrutti.Items.Add(new ListItem(descrFrutto, f.Id.ToString())); //new ListItem(valore, chiave);
            }
        }
        protected void FillDdlGruppi()
        {
            List<GruppiFrutti> listGruppiFrutti = GestisciGruppiFruttiDAO.getGruppi();

            ddlModScegliGruppo.Items.Clear();
            ddlDelGruppo.Items.Clear();
            ddlDelNomeGruppo.Items.Clear();

            //Il primo parametro ("") corrisponde al valore e il secondo alla chiave (il valore è quello che viene visualizzato nella form)
            ddlModScegliGruppo.Items.Add(new ListItem("", "-1"));
            ddlDelGruppo.Items.Add(new ListItem("", "-1"));
            ddlDelNomeGruppo.Items.Add(new ListItem("", "-1"));

            foreach (GruppiFrutti gf in listGruppiFrutti)
            {
                string nomeGruppo = gf.NomeGruppo;
                ddlModScegliGruppo.Items.Add(new ListItem(nomeGruppo, gf.Id.ToString())); //new ListItem(valore, chiave);
                ddlDelGruppo.Items.Add(new ListItem(nomeGruppo, gf.Id.ToString())); //new ListItem(valore, chiave);
                ddlDelNomeGruppo.Items.Add(new ListItem(nomeGruppo, gf.Id.ToString())); //new ListItem(valore, chiave);
            }
        }
        protected void FillDdlGruppiNonCompletati()
        {
            List<GruppiFrutti> listGruppiFrutti = GestisciGruppiFruttiDAO.getGruppiNonCompletati();

            ddlGruppi.Items.Clear();

            //Il primo parametro ("") corrisponde al valore e il secondo alla chiave (il valore è quello che viene visualizzato nella form)
            ddlGruppi.Items.Add(new ListItem("", "-1"));

            foreach (GruppiFrutti gf in listGruppiFrutti)
            {
                string nomeGruppo = gf.NomeGruppo;
                ddlGruppi.Items.Add(new ListItem(nomeGruppo, gf.Id.ToString())); //new ListItem(valore, chiave);
            }
        }
        protected void fillDdlCompGruppo()
        {
            List<CompGruppoFrut> listFrutti = GestisciGruppiFruttiDAO.getCompGruppo(Convert.ToInt32(ddlDelNomeGruppo.SelectedItem.Value));

            ddlDelCompGrup.Items.Clear();

            //Il primo parametro ("") corrisponde al valore e il secondo alla chiave (il valore è quello che viene visualizzato nella form)
            ddlDelCompGrup.Items.Add(new ListItem("", "-1"));

            foreach (CompGruppoFrut f in listFrutti)
            {
                string nomeFrutto = f.NomeFrutto;
                ddlDelCompGrup.Items.Add(new ListItem(nomeFrutto, f.Id.ToString())); //new ListItem(valore, chiave);
            }
        }
        protected void MostraListaGruppiInseriti()
        {
            gruppiList = GestisciGruppiFruttiDAO.getGruppiWithSearch(txtFiltroGruppi1.Text, txtFiltroGruppi2.Text, txtFiltroGruppi3.Text);
        }
    }
}