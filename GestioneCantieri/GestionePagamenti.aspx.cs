using GestioneCantieri.DAO;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestioneCantieri
{
    public partial class GestionePagamenti : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDdlScegliCant();
                pnlGestPagam.Visible = false;
                btnModPagam.Visible = false;
            }
        }

        /* HELPERS */
        protected void FillDdlScegliCant()
        {
            DataTable dt = CantieriDAO.GetCantieri(txtFiltroCantAnno.Text, txtFiltroCantCodCant.Text, txtFiltroCantDescrCodCant.Text, chkFiltroCantChiuso.Checked, chkFiltroCantRiscosso.Checked);
            List<Cantieri> listCantieri = dt.DataTableToList<Cantieri>();

            ddlScegliCant.Items.Clear();
            ddlScegliCant.Items.Add(new ListItem("", "-1"));

            foreach (Cantieri c in listCantieri)
            {
                string show = c.CodCant + " - " + c.DescriCodCAnt;
                ddlScegliCant.Items.Add(new ListItem(show, c.IdCantieri.ToString()));
            }
        }
        protected void SvuotaCampi(Panel pnl)
        {
            //Svuoto tutti i TextBox
            foreach (Control c in pnl.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Text = "";
            }

            //Acconto e Saldo FALSE
            chkSaldo.Checked = chkAcconto.Checked = false;
            txtImportoPagam.Text = "0";
        }
        protected void EnableDisableControls(bool enableControls, Panel panelName)
        {
            foreach (Control c in panelName.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Enabled = enableControls;
                else if (c is DropDownList)
                    ((DropDownList)c).Enabled = enableControls;
            }
            foreach (Control c in pnlIntestazione.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Enabled = enableControls;
                else if (c is DropDownList)
                    ((DropDownList)c).Enabled = enableControls;
            }
        }
        protected void SvuotaIntestazione()
        {
            //Svuoto tutti i TextBox
            foreach (Control c in pnlIntestazione.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Text = "";
                if (c is DropDownList)
                    ((DropDownList)c).SelectedIndex = 0;
            }

            txtDataDDT.Text = "";
            txtDataDDT.TextMode = TextBoxMode.Date;
        }

        //Controlla che l'intestazione sia completamente compilata prima di inserire il record
        protected bool isIntestazioneCompilata()
        {
            if (ddlScegliCant.SelectedIndex != 0 && txtDataDDT.Text != "")
            {
                return true;
            }

            return false;
        }

        //Controlla che sia stata impostata la data
        protected bool isDateNotSet()
        {
            if (txtDataDDT.Text == "")
            {
                lblIsPagamInserito.Text = "Inserire un valore per la data";
                lblIsPagamInserito.ForeColor = Color.Red;
                return true;
            }
            return false;
        }

        /* EVENTI CLICK */
        protected void btnFiltroCant_Click(object sender, EventArgs e)
        {
            FillDdlScegliCant();
        }
        protected void btnSvuotaIntestazione_Click(object sender, EventArgs e)
        {
            SvuotaIntestazione();
        }
        protected void ddlScegliCant_TextChanged(object sender, EventArgs e)
        {
            if (ddlScegliCant.SelectedIndex != 0)
            {
                pnlGestPagam.Visible = true;
                btnModPagam.Visible = false;
                BindGridPagam();
            }
            else
                pnlGestPagam.Visible = false;

        }

        #region Pagamenti
        /* HELPERS */
        private void FillPagamento(Pagamenti pag)
        {
            pag.IdTblCantieri = Convert.ToInt32(ddlScegliCant.SelectedItem.Value);
            pag.Data = Convert.ToDateTime(txtDataDDT.Text);
            pag.DescriPagamenti = txtDescrPagam.Text;
            pag.Acconto = chkAcconto.Checked;
            pag.Saldo = chkSaldo.Checked;

            if (txtImportoPagam.Text != "")
                pag.Imporo = Convert.ToDecimal(txtImportoPagam.Text);
            else
                pag.Imporo = 0;
        }

        /* EVENTI CLICK */
        protected void btnInsPagam_Click(object sender, EventArgs e)
        {
            bool isInserito = false;

            if (isDateNotSet())
                return;

            Pagamenti pag = new Pagamenti();
            FillPagamento(pag);

            if (isIntestazioneCompilata())
                isInserito = PagamentiDAO.InserisciPagamento(pag);

            if (isInserito)
            {
                lblIsPagamInserito.Text = "Record inserito con successo";
                lblIsPagamInserito.ForeColor = Color.Blue;
            }
            else
            {
                lblIsPagamInserito.Text = "Errore durante l'inserimento del record. L'intestazione deve essere interamente compilata.";
                lblIsPagamInserito.ForeColor = Color.Red;
            }

            BindGridPagam();
        }
        //Visibilità pannello
        protected void btnGestPagam_Click(object sender, EventArgs e)
        {
            lblTitoloMaschera.Text = "Gestione Pagamenti";
            btnModPagam.Visible = false;
            btnInsPagam.Visible = true;
            BindGridPagam();
            EnableDisableControls(true, pnlGestPagam);
            SvuotaCampi(pnlGestPagam);
        }

        /* EVENTI PER IL ROWCOMMAND */
        protected void BindGridPagam()
        {
            List<Pagamenti> pagList = PagamentiDAO.GetPagamenti(ddlScegliCant.SelectedItem.Value, txtFiltroPagamDescri.Text);
            grdPagamenti.DataSource = pagList;
            grdPagamenti.DataBind();
        }
        protected void btnFiltraPagam_Click(object sender, EventArgs e)
        {
            BindGridPagam();
        }
        protected void grdPagamenti_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idPagam = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName == "VisualPagam")
                VisualizzaDatiPagam(idPagam);
            else if (e.CommandName == "ModPagam")
                ModificaDatiPagam(idPagam);
            else if (e.CommandName == "ElimPagam")
                EliminaPagam(idPagam);
        }
        private void PopolaCampiPagam(int idPagam, bool enableControls)
        {
            Pagamenti p = PagamentiDAO.GetSinglePagamento(idPagam);

            //Rendo i textbox abilitati/disabilitati
            EnableDisableControls(enableControls, pnlGestPagam);

            ddlScegliCant.SelectedItem.Value = p.IdTblCantieri.ToString();
            txtDataDDT.Text = p.Data.ToString("yyyy-MM-dd");
            txtDataDDT.TextMode = TextBoxMode.Date;
            txtImportoPagam.Text = p.Imporo.ToString();
            txtDescrPagam.Text = p.DescriPagamenti.ToString();
            chkSaldo.Checked = p.Saldo;
            chkAcconto.Checked = p.Acconto;
        }
        private void PopolaObjPagam(Pagamenti p)
        {
            p.IdTblCantieri = Convert.ToInt32(ddlScegliCant.SelectedItem.Value);
            p.Data = Convert.ToDateTime(txtDataDDT.Text);
            p.Imporo = Convert.ToDecimal(txtImportoPagam.Text);
            p.DescriPagamenti = txtDescrPagam.Text;
            p.Saldo = chkSaldo.Checked;
            p.Acconto = chkAcconto.Checked;
        }
        private void VisualizzaDatiPagam(int idPagam)
        {
            lblTitoloMaschera.Text = "Visualizza Pagamento";
            PopolaCampiPagam(idPagam, false);
            btnInsPagam.Visible = btnModPagam.Visible = false;
        }
        private void ModificaDatiPagam(int idPagam)
        {
            lblTitoloMaschera.Text = "Modifica Pagamento";
            btnInsPagam.Visible = false;
            btnModPagam.Visible = true;
            PopolaCampiPagam(idPagam, true);
            hidPagamenti.Value = idPagam.ToString();
        }
        private void EliminaPagam(int idPagam)
        {
            bool isDeleted = PagamentiDAO.DeletePagamento(idPagam);

            if (isDeleted)
            {
                lblIsPagamInserito.Text = "Record eliminato con successo";
                lblIsPagamInserito.ForeColor = Color.Blue;
            }
            else
            {
                lblIsPagamInserito.Text = "Errore durante l'eliminazione del record";
                lblIsPagamInserito.ForeColor = Color.Red;
            }

            BindGridPagam();
        }
        protected void btnModPagam_Click(object sender, EventArgs e)
        {
            Pagamenti p = new Pagamenti();
            PopolaObjPagam(p);
            p.IdPagamenti = Convert.ToInt32(hidPagamenti.Value);
            bool isUpdated = PagamentiDAO.UpdatePagamento(p);

            if (isUpdated)
            {
                lblIsPagamInserito.Text = "Record modificato con successo";
                lblIsPagamInserito.ForeColor = Color.Blue;
            }
            else
            {
                lblIsPagamInserito.Text = "Errore durante la modifica del record";
                lblIsPagamInserito.ForeColor = Color.Red;
            }

            BindGridPagam();
            SvuotaCampi(pnlGestPagam);
        }
        #endregion
    }
}