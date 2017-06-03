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
    public partial class InserimentoDati : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MostraPannello(true, false, false, false);
                lblTitoloInserimento.Text = "Inserimento Clienti";
                BindGridClienti();
                PopolaDataInserimento();
            }
        }

        /* EVENTI CLICK */
        /* Mostra/Nascondi pannelli */
        protected void btnShowInsClienti_Click(object sender, EventArgs e)
        {
            lblTitoloInserimento.Text = "Inserimento Clienti";
            lblIsClienteInserito.Text = "";
            MostraPannello(true, false, false, false);
            BindGridClienti();
            PopolaDataInserimento();
        }
        protected void btnShowInsFornitori_Click(object sender, EventArgs e)
        {
            BindGridFornitori();
            lblTitoloInserimento.Text = "Inserimento Fornitori";
            MostraPannello(false, true, false, false);
        }
        protected void btnShowInsOperai_Click(object sender, EventArgs e)
        {
            BindGridOperai();
            lblTitoloInserimento.Text = "Inserimento Operai";
            MostraPannello(false, false, true, false);
        }
        protected void btnShowInsCantieri_Click(object sender, EventArgs e)
        {
            MostraPannello(false, false, false, true);
            lblTitoloInserimento.Text = "Inserimento Cantieri";
            lblIsCantInserito.Text = "";
            BindGridCantieri();
            FillDdlClienti();
            PopolaDataInserimento();
        }

        /* Click dei bottoni di inserimento */
        protected void btnInsCliente_Click(object sender, EventArgs e)
        {
            if (txtRagSocCli.Text != "")
            {
                bool isInserito = InserimentoDatiDAO.InserisciCliente(txtRagSocCli.Text, txtIndirizzo.Text, txtCap.Text, txtCitta.Text, txtProvincia.Text, txtTelefono.Text, txtCellulare.Text, txtPartitaIva.Text, txtCodiceFiscale.Text, DateTime.Now.ToString("yyyy-MM-dd"), txtNote.Text);

                if (isInserito)
                {
                    lblIsClienteInserito.Text = "Cliente '" + txtRagSocCli.Text + "' inserito correttamente";
                    lblIsClienteInserito.ForeColor = Color.Blue;
                }
                else
                {
                    lblIsClienteInserito.Text = "Errore durante l'inserimento del cliente '" + txtRagSocCli.Text + "'";
                    lblIsClienteInserito.ForeColor = Color.Red;
                }

                SvuotaTxtBox(pnlInsClienti);
                BindGridClienti();
            }
            else
            {
                lblIsClienteInserito.Text = "Il campo 'Ragione Sociale' deve essere compilato";
                lblIsClienteInserito.ForeColor = Color.Red;
            }
        }
        protected void btnInsFornit_Click(object sender, EventArgs e)
        {
            if (txtRagSocFornit.Text != "")
            {
                bool isInserito = InserimentoDatiDAO.InserisciFornitore(txtRagSocFornit.Text, txtCittaFornit.Text, txtIndirFornit.Text, txtCapFornit.Text, txtTelFornit.Text, txtCelFornit.Text, txtCodFiscFornit.Text, txtPartIvaFornit.Text, txtAbbrevFornit.Text);
                if (isInserito)
                {
                    lblIsFornitoreInserito.Text = "Fornitore '" + txtRagSocFornit.Text + "' inserito correttamente";
                    lblIsFornitoreInserito.ForeColor = Color.Blue;
                }
                else
                {
                    lblIsFornitoreInserito.Text = "Errore durante l'inserimento del cliente '" + txtRagSocFornit.Text + "'";
                    lblIsFornitoreInserito.ForeColor = Color.Red;
                }
                SvuotaTxtBox(pnlInsFornitori);
                BindGridFornitori();
            }
            else
            {
                lblIsFornitoreInserito.Text = "Il campo 'Ragione Sociale Fornitore' deve essere compilato";
                lblIsFornitoreInserito.ForeColor = Color.Red;
            }
        }
        protected void btnInsOper_Click(object sender, EventArgs e)
        {
            if (txtNomeOper.Text != "")
            {
                bool isInserito = InserimentoDatiDAO.InserisciOperaio(txtNomeOper.Text, txtDescrOper.Text, txtSuffOper.Text, txtOperaio.Text);

                if (isInserito)
                {
                    lblIsOperaioInserito.Text = "Operaio '" + txtNomeOper.Text + "' inserito con successo";
                    lblIsOperaioInserito.ForeColor = Color.Blue;
                }
                else
                {
                    lblIsOperaioInserito.Text = "Errore durante l'inserimento dell'operaio '" + txtNomeOper.Text + "'";
                    lblIsOperaioInserito.ForeColor = Color.Red;
                }

                BindGridOperai();
                SvuotaTxtBox(pnlInsOperai);
            }
            else
            {
                lblIsOperaioInserito.Text = "Il campo 'Nome Operaio' deve essere compilato";
                lblIsOperaioInserito.ForeColor = Color.Red;
            }
        }
        protected void btnInsCantiere_Click(object sender, EventArgs e)
        {
            if (ddlScegliClientePerCantiere.SelectedIndex != 0)
            {
                string chiuso = Convert.ToInt32(chkCantChiuso.Checked).ToString();
                string riscosso = Convert.ToInt32(chkCantRiscosso.Checked).ToString();
                string preventivo = Convert.ToInt32(chkPreventivo.Checked).ToString();
                string daDividere = Convert.ToInt32(chkDaDividere.Checked).ToString();
                string diviso = Convert.ToInt32(chkDiviso.Checked).ToString();
                string fatturato = Convert.ToInt32(chkFatturato.Checked).ToString();

                bool isInserito = InserimentoDatiDAO.InserisciCantiere(ddlScegliClientePerCantiere.SelectedValue, DateTime.Now.ToString("yyyy-MM-dd"),
                    txtCodCant.Text, txtDescrCodCant.Text, txtIndirizzoCant.Text, txtCittaCant.Text,
                    txtRicaricoCant.Text, txtPzzoManodopCant.Text, chiuso, riscosso,
                    txtNumeroCant.Text, txtValPrevCant.Text, txtIvaCant.Text, txtAnnoCant.Text,
                    preventivo, daDividere, diviso, fatturato, txtFasciaCant.Text);

                if (isInserito)
                {
                    lblIsCantInserito.Text = "Cantiere '" + txtDescrCodCant.Text + "' inserito con successo";
                    lblIsCantInserito.ForeColor = Color.Blue;
                }
                else
                {
                    lblIsCantInserito.Text = "Errore durante l'inserimento del cantiere '" + txtDescrCodCant.Text + "'";
                    lblIsCantInserito.ForeColor = Color.Red;
                }

                BindGridCantieri();
                SvuotaTxtBox(pnlInsCantieri);
            }
            else
            {
                lblIsCantInserito.Text = "Devi scegliere un cliente da associare al nuovo cantiere";
                lblIsCantInserito.ForeColor = Color.Red;
            }
        }

        /* HELPERS */
        protected void MostraPannello(bool pnlClienti, bool pnlFornitori, bool pnlOperai, bool pnlCantieri)
        {
            pnlInsClienti.Visible = pnlClienti;
            pnlInsFornitori.Visible = pnlFornitori;
            pnlInsOperai.Visible = pnlOperai;
            pnlInsCantieri.Visible = pnlCantieri;
        }
        protected void BindGridClienti()
        {
            DataTable dt = InserimentoDatiDAO.GetAllClienti();
            grdClienti.DataSource = dt;
            grdClienti.DataBind();
        }
        protected void BindGridFornitori()
        {
            DataTable dt = InserimentoDatiDAO.GetAllFornitori();
            grdFornitori.DataSource = dt;
            grdFornitori.DataBind();
        }
        protected void BindGridOperai()
        {
            DataTable dt = InserimentoDatiDAO.GetAllOperai();
            grdOperai.DataSource = dt;
            grdOperai.DataBind();
        }
        protected void BindGridCantieri()
        {
            DataTable dt = InserimentoDatiDAO.GetAllCantieri();
            grdCantieri.DataSource = dt;
            grdCantieri.DataBind();
        }
        protected void SvuotaTxtBox(Control container)
        {
            foreach (var control in container.Controls)
            {
                if (control is TextBox)
                    ((TextBox)control).Text = string.Empty;
            }
        }
        protected void FillDdlClienti()
        {
            List<Clienti> listClienti = InserimentoDatiDAO.GetClientiIdAndName();

            ddlScegliClientePerCantiere.Items.Clear();
            ddlScegliClientePerCantiere.Items.Add(new ListItem("", "-1"));

            foreach (Clienti c in listClienti)
                ddlScegliClientePerCantiere.Items.Add(new ListItem(c.RagSocCli, c.Id.ToString()));
        }
        protected void PopolaDataInserimento()
        {
            txtDataInserimento.Text = txtDataInserCant.Text = DateTime.Now.ToString().Split(' ')[0];
        }

        /* Mostro Si/No invece di True/False sulla GrdView */
        protected void grdCantieri_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    DataRow dr = ((DataRowView)e.Row.DataItem).Row;
            //    if (Convert.ToBoolean(dr["Chiuso"]))
            //        ((Label)e.Row.FindControl("lblChiusoYesNo")).Text = "Si";
            //    else
            //        ((Label)e.Row.FindControl("lblChiusoYesNo")).Text = "No";

            //    if (Convert.ToBoolean(dr["Riscosso"]))
            //        ((Label)e.Row.FindControl("lblRiscossoYesNo")).Text = "Si";
            //    else
            //        ((Label)e.Row.FindControl("lblRiscossoYesNo")).Text = "No";

            //    if (Convert.ToBoolean(dr["Preventivo"]))
            //        ((Label)e.Row.FindControl("lblPreventivoYesNo")).Text = "Si";
            //    else
            //        ((Label)e.Row.FindControl("lblPreventivoYesNo")).Text = "No";

            //    if (Convert.ToBoolean(dr["DaDividere"]))
            //        ((Label)e.Row.FindControl("lblDaDividereYesNo")).Text = "Si";
            //    else
            //        ((Label)e.Row.FindControl("lblDaDividereYesNo")).Text = "No";

            //    if (Convert.ToBoolean(dr["Diviso"]))
            //        ((Label)e.Row.FindControl("lblDivisoYesNo")).Text = "Si";
            //    else
            //        ((Label)e.Row.FindControl("lblDivisoYesNo")).Text = "No";

            //    if (Convert.ToBoolean(dr["Fatturato"]))
            //        ((Label)e.Row.FindControl("lblFatturatoYesNo")).Text = "Si";
            //    else
            //        ((Label)e.Row.FindControl("lblFatturatoYesNo")).Text = "No";
            //}
        }

        protected void btnModCant_Click(object sender, EventArgs e)
        {

        }

        protected void btnVisualCant_Click(object sender, EventArgs e)
        {

        }

        protected void btnElimCant_Click(object sender, EventArgs e)
        {

        }
    }
}