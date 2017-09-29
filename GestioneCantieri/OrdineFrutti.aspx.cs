using GestioneCantieri.DAO;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestioneCantieri
{
    public partial class OrdineFrutti : System.Web.UI.Page
    {
        /* Lista pubblica per la visualizzazione dinamica di record */
        public List<MatOrdFrut> compList = new List<MatOrdFrut>();
        public List<MatOrdFrut> fruttiList = new List<MatOrdFrut>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlInserisciDati.Visible = pnlScegliGruppo.Visible = false;
                pnlMostraGruppiInseriti.Visible = btnInserisciGruppo.Visible = false;
                lblQtaFrutto.Visible = txtQtaFrutto.Visible = btnInserisciFrutto.Visible = false;
                FillDdlScegliCantiere();
                FillDdlScegliLocale();
                FillDdlGruppi();
                FillDdlFrutti();
            }
        }

        /* EVENTI CLICK */
        protected void btnFiltroGruppi_Click(object sender, EventArgs e)
        {
            FillDdlGruppi();
        }
        protected void btnInserisciGruppo_Click(object sender, EventArgs e)
        {
            bool isAggiunto = OrdineFruttiDAO.InserisciGruppo(ddlScegliCantiere.SelectedItem.Value, ddlScegliGruppo.SelectedItem.Value, ddlScegliLocale.SelectedItem.Value);

            if (isAggiunto)
            {
                lblIsGruppoInserito.Text = "Componente '" + ddlScegliGruppo.SelectedItem.Text + "' aggiunto correttamente!";
                lblIsGruppoInserito.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                lblIsGruppoInserito.Text = "Errore durante l'inserimento del gruppo '" + ddlScegliGruppo.SelectedItem.Text + "'";
                lblIsGruppoInserito.ForeColor = System.Drawing.Color.Red;
            }

            fruttiList = OrdineFruttiDAO.GetFruttiNonInGruppo(ddlScegliCantiere.SelectedItem.Value);
            compList = OrdineFruttiDAO.getGruppi(ddlScegliCantiere.SelectedItem.Value, ddlScegliLocale.SelectedItem.Value);
            ddlScegliGruppo.SelectedIndex = 0;
        }
        protected void btnInserisciFrutto_Click(object sender, EventArgs e)
        {
            bool isInserito = OrdineFruttiDAO.InserisciFruttoNonInGruppo(ddlScegliCantiere.SelectedItem.Value, ddlScegliLocale.SelectedItem.Value, ddlScegliFrutto.SelectedItem.Value, txtQtaFrutto.Text);

            if (isInserito)
            {
                lblIsFruttoInserito.Text = "Frutto inserito con successo";
                lblIsFruttoInserito.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                lblIsFruttoInserito.Text = "Errore durante l'inserimento del frutto";
                lblIsFruttoInserito.ForeColor = System.Drawing.Color.Red;
            }

            fruttiList = OrdineFruttiDAO.GetFruttiNonInGruppo(ddlScegliCantiere.SelectedItem.Value);
            compList = OrdineFruttiDAO.getGruppi(ddlScegliCantiere.SelectedItem.Value, ddlScegliLocale.SelectedItem.Value);
            ddlScegliFrutto.SelectedIndex = 0;
            txtQtaFrutto.Text = "";
        }

        /* EVENTI TEXT-CHANGED */
        protected void ddlScegliCantiere_TextChanged(object sender, EventArgs e)
        {
            if (ddlScegliCantiere.SelectedItem.Value != "")
                pnlInserisciDati.Visible = true;
        }
        protected void ddlScegliLocale_TextChanged(object sender, EventArgs e)
        {
            if (ddlScegliLocale.SelectedItem.Value != "")
            {
                pnlScegliGruppo.Visible = true;
                pnlMostraGruppiInseriti.Visible = true;
                fruttiList = OrdineFruttiDAO.GetFruttiNonInGruppo(ddlScegliCantiere.SelectedItem.Value);
                compList = OrdineFruttiDAO.getGruppi(ddlScegliCantiere.SelectedItem.Value, ddlScegliLocale.SelectedItem.Value);
            }
        }
        /*protected void txtFiltroGruppo1_TextChanged(object sender, EventArgs e)
        {
            FillDdlGruppi();
        }
        protected void txtFiltroGruppo2_TextChanged(object sender, EventArgs e)
        {
            FillDdlGruppi();
        }
        protected void txtFiltroGruppo3_TextChanged(object sender, EventArgs e)
        {
            FillDdlGruppi();
        }*/
        protected void ddlScegliGruppo_TextChanged(object sender, EventArgs e)
        {
            if (ddlScegliGruppo.SelectedItem.Text != "")
            {
                compList = OrdineFruttiDAO.getGruppi(ddlScegliCantiere.SelectedItem.Value, ddlScegliLocale.SelectedItem.Value);
                fruttiList = OrdineFruttiDAO.GetFruttiNonInGruppo(ddlScegliCantiere.SelectedItem.Value);
                btnInserisciGruppo.Visible = true;
                lblIsGruppoInserito.Text = "";
            }
            else
            {
                compList = OrdineFruttiDAO.getGruppi(ddlScegliCantiere.SelectedItem.Value, ddlScegliLocale.SelectedItem.Value);
                fruttiList = OrdineFruttiDAO.GetFruttiNonInGruppo(ddlScegliCantiere.SelectedItem.Value);
                btnInserisciGruppo.Visible = false;
            }
        }
        protected void ddlScegliFrutto_TextChanged(object sender, EventArgs e)
        {
            if (ddlScegliFrutto.SelectedItem.Text != "")
            {
                lblQtaFrutto.Visible = txtQtaFrutto.Visible = btnInserisciFrutto.Visible = true;
                fruttiList = OrdineFruttiDAO.GetFruttiNonInGruppo(ddlScegliCantiere.SelectedItem.Value);
                compList = OrdineFruttiDAO.getGruppi(ddlScegliCantiere.SelectedItem.Value, ddlScegliLocale.SelectedItem.Value);
            }
            else
            {
                lblQtaFrutto.Visible = txtQtaFrutto.Visible = btnInserisciFrutto.Visible = false;
                fruttiList = OrdineFruttiDAO.GetFruttiNonInGruppo(ddlScegliCantiere.SelectedItem.Value);
                compList = OrdineFruttiDAO.getGruppi(ddlScegliCantiere.SelectedItem.Value, ddlScegliLocale.SelectedItem.Value);
            }
        }

        /* HELPERS */
        protected void FillDdlScegliCantiere()
        {
            List<Cantieri> listCantieri = OrdineFruttiDAO.GetListCantieri();

            ddlScegliCantiere.Items.Clear();

            //Il primo parametro ("") corrisponde al valore e il secondo alla chiave (il valore è quello che viene visualizzato nella form)
            ddlScegliCantiere.Items.Add(new ListItem("", "-1"));

            foreach (Cantieri c in listCantieri)
            {
                string cantiere = c.CodCant + " - " + c.DescriCodCAnt;
                ddlScegliCantiere.Items.Add(new ListItem(cantiere, c.IdCantieri.ToString())); //new ListItem(valore, chiave);
            }
        }
        protected void FillDdlScegliLocale()
        {
            List<Locali> listLocali = OrdineFruttiDAO.GetListLocali();

            ddlScegliLocale.Items.Clear();

            //Il primo parametro ("") corrisponde al valore e il secondo alla chiave (il valore è quello che viene visualizzato nella form)
            ddlScegliLocale.Items.Add(new ListItem("", "-1"));

            foreach (Locali l in listLocali)
            {
                ddlScegliLocale.Items.Add(new ListItem(l.NomeLocale, l.Id.ToString())); //new ListItem(valore, chiave);
            }
        }
        protected void FillDdlGruppi()
        {
            List<GruppiFrutti> listGruppiFrutti = OrdineFruttiDAO.GetGruppiWithSearch(txtFiltroGruppo1.Text, txtFiltroGruppo2.Text, txtFiltroGruppo3.Text);

            ddlScegliGruppo.Items.Clear();

            //Il primo parametro ("") corrisponde al valore e il secondo alla chiave (il valore è quello che viene visualizzato nella form)
            ddlScegliGruppo.Items.Add(new ListItem("", "-1"));

            foreach (GruppiFrutti gf in listGruppiFrutti)
            {
                string nomeDescrGruppo = gf.NomeGruppo + " - " + gf.Descr;
                ddlScegliGruppo.Items.Add(new ListItem(nomeDescrGruppo, gf.Id.ToString())); //new ListItem(valore, chiave);
            }
        }
        protected void FillDdlFrutti()
        {
            List<Frutti> listFrutti = GestisciGruppiFruttiDAO.getFrutti();
            ddlScegliFrutto.Items.Clear();

            ddlScegliFrutto.Items.Add(new ListItem("", "-1"));

            foreach (Frutti f in listFrutti)
                ddlScegliFrutto.Items.Add(new ListItem(f.Descr, f.Id.ToString()));
        }
    }
}