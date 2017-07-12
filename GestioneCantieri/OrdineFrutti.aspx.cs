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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlInserisciDati.Visible = pnlScegliGruppo.Visible = false;
                pnlMostraGruppiInseriti.Visible = btnInserisciGruppo.Visible = false;
                FillDdlScegliCantiere();
                FillDdlScegliLocale();
                FillDdlGruppi();
            }
        }

        /* EVENTI CLICK */
        protected void btnFiltroGruppi_Click(object sender, EventArgs e)
        {
            FillDdlGruppi();
        }
        protected void btnInserisciGruppo_Click(object sender, EventArgs e)
        {
            bool isAggiunto = OrdineFruttiDAO.InserisciGruppo(ddlScegliCantiere.SelectedItem.Value, ddlScegliGruppo.SelectedItem.Value, ddlScegliLocale.SelectedItem.Value, txtDataOrdine.Text, txtAppartamento.Text);

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

            compList = OrdineFruttiDAO.getGruppi(ddlScegliCantiere.SelectedItem.Value, ddlScegliLocale.SelectedItem.Value);
            ddlScegliGruppo.SelectedIndex = 0;
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
                btnInserisciGruppo.Visible = true;
                lblIsGruppoInserito.Text = "";
            }
            else
            {
                compList = OrdineFruttiDAO.getGruppi(ddlScegliCantiere.SelectedItem.Value, ddlScegliLocale.SelectedItem.Value);
                btnInserisciGruppo.Visible = false;
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
    }
}