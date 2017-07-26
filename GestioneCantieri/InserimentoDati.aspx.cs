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
                btnModCliente.Visible = btnModFornit.Visible = false;
                btnModOper.Visible = btnModCantiere.Visible = false;
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
            ResettaCampi(pnlInsClienti);
            btnInsCliente.Visible = true;
            btnModCliente.Visible = false;
        }
        protected void btnShowInsFornitori_Click(object sender, EventArgs e)
        {
            BindGridFornitori();
            lblTitoloInserimento.Text = "Inserimento Fornitori";
            lblIsFornitoreInserito.Text = "";
            MostraPannello(false, true, false, false);
            ResettaCampi(pnlInsFornitori);
            btnInsFornit.Visible = true;
            btnModFornit.Visible = false;
        }
        protected void btnShowInsOperai_Click(object sender, EventArgs e)
        {
            BindGridOperai();
            lblTitoloInserimento.Text = "Inserimento Operai";
            lblIsOperaioInserito.Text = "";
            MostraPannello(false, false, true, false);
            ResettaCampi(pnlInsOperai);
            btnInsOper.Visible = true;
            btnModOper.Visible = false;
        }
        protected void btnShowInsCantieri_Click(object sender, EventArgs e)
        {
            MostraPannello(false, false, false, true);
            lblTitoloInserimento.Text = "Inserimento Cantieri";
            lblIsCantInserito.Text = "";
            BindGridCantieri();
            FillDdlClienti();
            ResettaCampi(pnlTxtBoxCantContainer);
            txtAnnoCant.Text = DateTime.Now.Year.ToString();
            PopolaCodCantAnnoNumero();
            txtCodCant.Enabled = false;
            btnModCantiere.Visible = false;
            btnInsCantiere.Visible = true;
        }

        /* Click dei bottoni di inserimento/modifica */
        //Clienti
        protected void btnInsCliente_Click(object sender, EventArgs e)
        {
            if (txtRagSocCli.Text != "")
            {
                bool isInserito = InserimentoDatiDAO.InserisciCliente(txtRagSocCli.Text, 
                    txtIndirizzo.Text, txtCap.Text, txtCitta.Text, txtProvincia.Text, 
                    txtTelefono.Text, txtCellulare.Text, txtPartitaIva.Text, 
                    txtCodiceFiscale.Text, txtDataInserimento.Text, txtNote.Text);

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

                ResettaCampi(pnlInsClienti);
                BindGridClienti();
            }
            else
            {
                lblIsClienteInserito.Text = "Il campo 'Ragione Sociale' deve essere compilato";
                lblIsClienteInserito.ForeColor = Color.Red;
            }
        }
        protected void btnModCliente_Click(object sender, EventArgs e)
        {
            bool isUpdated = InserimentoDatiDAO.UpdateCliente(hidIdClienti.Value, txtRagSocCli.Text,
                    txtIndirizzo.Text, txtCap.Text, txtCitta.Text, txtProvincia.Text,
                    txtTelefono.Text, txtCellulare.Text, txtPartitaIva.Text,
                    txtCodiceFiscale.Text, txtDataInserimento.Text, txtNote.Text);

            if (isUpdated)
            {
                lblIsClienteInserito.Text = "Cliente '" + txtRagSocCli.Text + "' modificato con successo";
                lblIsClienteInserito.ForeColor = Color.Blue;
            }
            else
            {
                lblIsClienteInserito.Text = "Errore durante la modifica del cliente '" + txtRagSocCli.Text + "'";
                lblIsClienteInserito.ForeColor = Color.Red;
            }

            BindGridClienti();
        }
        protected void btnFiltraClienti_Click(object sender, EventArgs e)
        {
            if (!(txtFiltroRagSocCli.Text == ""))
                BindGridClientiWithSearch();
            else
                BindGridClienti();
        }
        protected void btnSvuotaFiltriClienti_Click(object sender, EventArgs e)
        {
            ResettaCampi(pnlFiltriCliente);
            BindGridClienti();
        }
        //Fornitori
        protected void btnInsFornit_Click(object sender, EventArgs e)
        {
            if (txtRagSocFornit.Text != "")
            {
                bool isInserito = InserimentoDatiDAO.InserisciFornitore(txtRagSocFornit.Text, 
                    txtCittaFornit.Text, txtIndirFornit.Text, txtCapFornit.Text, txtTelFornit.Text, 
                    txtCelFornit.Text, txtCodFiscFornit.Text, txtPartIvaFornit.Text, txtAbbrevFornit.Text);
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
                ResettaCampi(pnlInsFornitori);
                BindGridFornitori();
            }
            else
            {
                lblIsFornitoreInserito.Text = "Il campo 'Ragione Sociale Fornitore' deve essere compilato";
                lblIsFornitoreInserito.ForeColor = Color.Red;
            }
        }
        protected void btnModFornit_Click(object sender, EventArgs e)
        {
            bool isUpdated = InserimentoDatiDAO.UpdateFornitore(hidIdFornit.Value, txtRagSocFornit.Text,
                    txtCittaFornit.Text, txtIndirFornit.Text, txtCapFornit.Text, txtTelFornit.Text,
                    txtCelFornit.Text, txtCodFiscFornit.Text, txtPartIvaFornit.Text, txtAbbrevFornit.Text);

            if (isUpdated)
            {
                lblIsFornitoreInserito.Text = "Fornitore '" + txtRagSocFornit.Text + "' modificato con successo";
                lblIsFornitoreInserito.ForeColor = Color.Blue;
            }
            else
            {
                lblIsFornitoreInserito.Text = "Errore durante la modifica del fornitore '" + txtRagSocFornit.Text + "'";
                lblIsFornitoreInserito.ForeColor = Color.Red;
            }

            BindGridFornitori();
        }
        //Operai
        protected void btnInsOper_Click(object sender, EventArgs e)
        {
            if (txtNomeOper.Text != "")
            {
                bool isInserito = InserimentoDatiDAO.InserisciOperaio(txtNomeOper.Text, txtDescrOper.Text, txtSuffOper.Text, txtOperaio.Text, txtCostoOperaio.Text);

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
                ResettaCampi(pnlInsOperai);
            }
            else
            {
                lblIsOperaioInserito.Text = "Il campo 'Nome Operaio' deve essere compilato";
                lblIsOperaioInserito.ForeColor = Color.Red;
            }
        }
        protected void btnModOper_Click(object sender, EventArgs e)
        {
            bool isUpdated = InserimentoDatiDAO.UpdateOperaio(hidIdOper.Value, txtNomeOper.Text,
                txtDescrOper.Text, txtSuffOper.Text, txtOperaio.Text, txtCostoOperaio.Text);

            if (isUpdated)
            {
                lblIsOperaioInserito.Text = "Operaio '" + txtDescrOper.Text + "' modificato con successo";
                lblIsOperaioInserito.ForeColor = Color.Blue;
            }
            else
            {
                lblIsOperaioInserito.Text = "Errore durante la modifica dell'operaio '" + txtDescrOper.Text + "'";
                lblIsOperaioInserito.ForeColor = Color.Red;
            }

            BindGridOperai();
        }
        //Cantieri
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
                string codRiferCant = CostruisceCodRiferCant();

                bool isInserito = InserimentoDatiDAO.InserisciCantiere(ddlScegliClientePerCantiere.SelectedValue, txtDataInserCant.Text,
                    txtCodCant.Text, txtDescrCodCant.Text, txtIndirizzoCant.Text, txtCittaCant.Text,
                    txtRicaricoCant.Text, txtPzzoManodopCant.Text, chiuso, riscosso,
                    txtNumeroCant.Text, txtValPrevCant.Text, txtIvaCant.Text, txtAnnoCant.Text,
                    preventivo, daDividere, diviso, fatturato, txtFasciaCant.Text, codRiferCant);

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
                ResettaCampi(pnlInsCantieri);
                PopolaCodCantAnnoNumero();
            }
            else
            {
                lblIsCantInserito.Text = "Devi scegliere un cliente da associare al nuovo cantiere";
                lblIsCantInserito.ForeColor = Color.Red;
            }
        }
        protected void btnModCantiere_Click(object sender, EventArgs e)
        {
            string chiuso = Convert.ToInt32(chkCantChiuso.Checked).ToString();
            string riscosso = Convert.ToInt32(chkCantRiscosso.Checked).ToString();
            string preventivo = Convert.ToInt32(chkPreventivo.Checked).ToString();
            string daDividere = Convert.ToInt32(chkDaDividere.Checked).ToString();
            string diviso = Convert.ToInt32(chkDiviso.Checked).ToString();
            string fatturato = Convert.ToInt32(chkFatturato.Checked).ToString();

            bool isUpdated = InserimentoDatiDAO.UpdateCantiere(hidIdCant.Value, ddlScegliClientePerCantiere.SelectedValue, txtDataInserCant.Text,
                    txtCodCant.Text, txtDescrCodCant.Text, txtIndirizzoCant.Text, txtCittaCant.Text,
                    txtRicaricoCant.Text, txtPzzoManodopCant.Text, chiuso, riscosso,
                    txtNumeroCant.Text, txtValPrevCant.Text, txtIvaCant.Text, txtAnnoCant.Text,
                    preventivo, daDividere, diviso, fatturato, txtFasciaCant.Text);

            if (isUpdated)
            {
                lblIsCantInserito.Text = "Cantiere '" + txtDescrCodCant.Text + "' modificato con successo";
                lblIsCantInserito.ForeColor = Color.Blue;
            }
            else
            {
                lblIsCantInserito.Text = "Errore durante la modifica del cantiere '" + txtDescrCodCant.Text + "'";
                lblIsCantInserito.ForeColor = Color.Red;
            }

            if (!(txtFiltroAnno.Text == "" && txtFiltroCodCant.Text == "" && txtFiltroDescr.Text == "" && txtFiltroCliente.Text == "" && chkFiltroChiuso.Checked == false && chkFiltroRiscosso.Checked == false))
                BindGridCantieriWithSearch();
            else
                BindGridCantieri();
        }
        protected void btnFiltraCant_Click(object sender, EventArgs e)
        {
            if (!(txtFiltroAnno.Text == "" && txtFiltroCodCant.Text == "" && txtFiltroDescr.Text == "" && txtFiltroCliente.Text == "" && chkFiltroChiuso.Checked == false && chkFiltroRiscosso.Checked == false))
                BindGridCantieriWithSearch();
            else
                BindGridCantieri();
        }
        protected void btnSvuotaFiltri_Click(object sender, EventArgs e)
        {
            ResettaCampi(pnlFiltriCant);
            chkFiltroChiuso.Checked = chkFiltroRiscosso.Checked = false;
            BindGridCantieri();
        }

        /* HELPERS */
        protected void MostraPannello(bool pnlClienti, bool pnlFornitori, bool pnlOperai, bool pnlCantieri)
        {
            pnlInsClienti.Visible = pnlClienti;
            pnlInsFornitori.Visible = pnlFornitori;
            pnlInsOperai.Visible = pnlOperai;
            pnlInsCantieri.Visible = pnlCantieri;
        }
        protected void ResettaCampi(Control container)
        {
            foreach (var control in container.Controls)
            {
                if (control is TextBox) { 
                    ((TextBox)control).Text = string.Empty;
                    ((TextBox)control).Enabled = true;
                }
                if (control is CheckBox)
                {
                    ((CheckBox)control).Checked = false;
                    ((CheckBox)control).Enabled = true;
                }
                if (control is DropDownList)
                    ((DropDownList)control).Enabled = true;
            }
        }
        //Clienti
        protected void BindGridClienti()
        {
            DataTable dt = InserimentoDatiDAO.GetAllClienti();
            List<Clienti> clientiList = dt.DataTableToList<Clienti>();
            grdClienti.DataSource = clientiList;
            grdClienti.DataBind();
        }
        protected void BindGridClientiWithSearch()
        {
            DataTable dt = InserimentoDatiDAO.FiltraClienti(txtFiltroRagSocCli.Text);
            List<Clienti> clientiList = dt.DataTableToList<Clienti>();
            grdClienti.DataSource = clientiList;
            grdClienti.DataBind();
        }
        protected void VisualizzaDatiCliente(int idCli)
        {
            lblTitoloInserimento.Text = "Visualizza Cliente";
            lblIsClienteInserito.Text = "";
            PopolaCampiCliente(idCli, false);
            btnInsCliente.Visible = btnModCliente.Visible = false;
        }
        protected void ModificaDatiCliente(int idCli)
        {
            lblTitoloInserimento.Text = "Modifica Cliente";
            lblIsClienteInserito.Text = "";
            btnModCliente.Visible = true;
            btnInsCliente.Visible = false;
            PopolaCampiCliente(idCli, true);
            hidIdClienti.Value = idCli.ToString();
        }
        protected void EliminaCliente(int idCli)
        {
            bool isEliminato = InserimentoDatiDAO.EliminaCliente(idCli);
            if (isEliminato)
            {
                lblIsClienteInserito.Text = "Cliente eliminato con successo";
                lblIsClienteInserito.ForeColor = Color.Blue;
            }
            else
            {
                lblIsClienteInserito.Text = "Errore durante l'eliminazione del cliente, potrebbe avere delle referenze in altre tabelle";
                lblIsClienteInserito.ForeColor = Color.Red;
            }

            BindGridClienti();

            ResettaCampi(pnlInsClienti);
            btnModCliente.Visible = false;
            btnInsCliente.Visible = true;
            lblTitoloInserimento.Text = "Inserimento Clienti";
        }
        protected void PopolaCampiCliente(int idCli, bool isControlEnabled)
        {
            Clienti cli = InserimentoDatiDAO.GetSingleCliente(idCli);

            //Rendo i textbox disabilitati
            foreach (Control c in pnlInsClienti.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Enabled = isControlEnabled;
            }

            //Popolo i textbox
            txtRagSocCli.Text = cli.RagSocCli;
            txtIndirizzo.Text = cli.Indirizzo;
            txtCap.Text = cli.Cap;
            txtCitta.Text = cli.Città;
            txtTelefono.Text = cli.Tel1.ToString();
            txtCellulare.Text = cli.Cell1.ToString();
            txtPartitaIva.Text = cli.PartitaIva;
            txtCodiceFiscale.Text = cli.CodFiscale;
            txtDataInserimento.Text = cli.Data.ToString();
            txtProvincia.Text = cli.Provincia;
            txtNote.Text = cli.Note;
        }
        //Fornitori
        protected void BindGridFornitori()
        {
            DataTable dt = InserimentoDatiDAO.GetAllFornitori();
            List<TipDatCant> fornitList = dt.DataTableToList<TipDatCant>();
            grdFornitori.DataSource = fornitList;
            grdFornitori.DataBind();
        }
        protected void VisualizzaDatiFornitore(int idFornitore)
        {
            lblTitoloInserimento.Text = "Visualizza Fornitore";
            lblIsFornitoreInserito.Text = "";
            PopolaCampiFornitore(idFornitore, false);
            btnInsFornit.Visible = btnModFornit.Visible = false;
        }
        protected void ModificaDatiFornitore(int idFornitore)
        {
            lblTitoloInserimento.Text = "Modifica Fornitore";
            lblIsFornitoreInserito.Text = "";
            btnModFornit.Visible = true;
            btnInsFornit.Visible = false;
            PopolaCampiFornitore(idFornitore, true);
            hidIdFornit.Value = idFornitore.ToString();
        }
        protected void EliminaFornitore(int idFornitore)
        {
            bool isEliminato = InserimentoDatiDAO.EliminaFornitore(idFornitore);
            if (isEliminato)
            {
                lblIsFornitoreInserito.Text = "Fornitore eliminato con successo";
                lblIsFornitoreInserito.ForeColor = Color.Blue;
            }
            else
            {
                lblIsFornitoreInserito.Text = "Errore durante l'eliminazione del fornitore";
                lblIsFornitoreInserito.ForeColor = Color.Red;
            }

            BindGridFornitori();

            ResettaCampi(pnlInsFornitori);
            btnModFornit.Visible = false;
            btnInsFornit.Visible = true;
            lblTitoloInserimento.Text = "Inserimento Fornitori";
        }
        protected void PopolaCampiFornitore(int idFornitore, bool isControlEnabled)
        {
            Fornitori fornitore = InserimentoDatiDAO.GetSingleFornitore(idFornitore);

            //Rendo i textbox disabilitati
            foreach (Control c in pnlInsFornitori.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Enabled = isControlEnabled;
            }

            //Popolo i textbox
            txtRagSocFornit.Text = fornitore.RagSocForni;
            txtIndirFornit.Text = fornitore.Indirizzo;
            txtCapFornit.Text = fornitore.Cap;
            txtCittaFornit.Text = fornitore.Città;
            txtTelFornit.Text = fornitore.Tel1.ToString();
            txtCelFornit.Text = fornitore.Cell1.ToString();
            txtPartIvaFornit.Text = fornitore.PartitaIva.ToString();
            txtCodFiscFornit.Text = fornitore.CodFiscale;
            txtAbbrevFornit.Text = fornitore.Abbreviato;
        }
        //Operai
        protected void BindGridOperai()
        {
            DataTable dt = InserimentoDatiDAO.GetAllOperai();
            List<Operai> opList = dt.DataTableToList<Operai>();
            grdOperai.DataSource = opList;
            grdOperai.DataBind();
        }
        protected void VisualizzaDatiOperaio(int idOperaio)
        {
            lblTitoloInserimento.Text = "Visualizza Operaio";
            lblIsOperaioInserito.Text = "";
            PopolaCampiOperaio(idOperaio, false);
            btnInsOper.Visible = btnModOper.Visible = false;
        }
        protected void ModificaDatiOperaio(int idOperaio)
        {
            lblTitoloInserimento.Text = "Modifica Operaio";
            lblIsOperaioInserito.Text = "";
            btnModOper.Visible = true;
            btnInsOper.Visible = false;
            PopolaCampiOperaio(idOperaio, true);
            hidIdOper.Value = idOperaio.ToString();
        }
        protected void EliminaOperaio(int idOperaio)
        {
            bool isEliminato = InserimentoDatiDAO.EliminaOperaio(idOperaio);
            if (isEliminato)
            {
                lblIsOperaioInserito.Text = "Operaio eliminato con successo";
                lblIsOperaioInserito.ForeColor = Color.Blue;
            }
            else
            {
                lblIsOperaioInserito.Text = "Errore durante l'eliminazione dell'operaio";
                lblIsOperaioInserito.ForeColor = Color.Red;
            }

            BindGridOperai();

            ResettaCampi(pnlInsOperai);
            btnModOper.Visible = false;
            btnInsOper.Visible = true;
            lblTitoloInserimento.Text = "Inserimento Operai";
        }
        protected void PopolaCampiOperaio(int idOperaio, bool isControlEnabled)
        {
            Operai operaio = InserimentoDatiDAO.GetSingleOperaio(idOperaio);

            //Rendo i textbox disabilitati
            foreach (Control c in pnlInsOperai.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Enabled = isControlEnabled;
            }

            //Popolo i textbox
            txtNomeOper.Text = operaio.NomeOp;
            txtDescrOper.Text = operaio.DescrOp;
            txtSuffOper.Text = operaio.Suffisso;
            txtOperaio.Text = operaio.Operaio;
            txtCostoOperaio.Text = operaio.CostoOperaio.ToString();
        }
        //Cantieri
        protected void BindGridCantieri()
        {
            DataTable dt = InserimentoDatiDAO.GetAllCantieri();
            List<Cantieri> cantList = dt.DataTableToList<Cantieri>();
            grdCantieri.DataSource = cantList;
            grdCantieri.DataBind();
        }
        protected void BindGridCantieriWithSearch()
        {
            DataTable dt = InserimentoDatiDAO.FiltraCantieri(txtFiltroAnno.Text, txtFiltroCodCant.Text, txtFiltroDescr.Text, txtFiltroCliente.Text, chkFiltroChiuso.Checked, chkFiltroRiscosso.Checked);
            List<Cantieri> cantList = dt.DataTableToList<Cantieri>();
            grdCantieri.DataSource = cantList;
            grdCantieri.DataBind();
        }
        protected void FillDdlClienti()
        {
            List<Clienti> listClienti = InserimentoDatiDAO.GetClientiIdAndName();

            ddlScegliClientePerCantiere.Items.Clear();
            ddlScegliClientePerCantiere.Items.Add(new ListItem("", "-1"));

            foreach (Clienti c in listClienti)
                ddlScegliClientePerCantiere.Items.Add(new ListItem(c.RagSocCli, c.IdCliente.ToString()));
        }
        protected void VisualizzaDatiCant(int idCant)
        {
            lblTitoloInserimento.Text = "Visualizza Cantiere";
            lblIsCantInserito.Text = "";
            PopolaCampiCantiere(idCant, false);
            btnInsCantiere.Visible = btnModCantiere.Visible = false;
        }
        protected void ModificaDatiCant(int idCant)
        {
            lblTitoloInserimento.Text = "Modifica Cantiere";
            lblIsCantInserito.Text = "";
            btnModCantiere.Visible = true;
            btnInsCantiere.Visible = false;
            PopolaCampiCantiere(idCant, true);
            hidIdCant.Value = idCant.ToString();
        }
        protected void EliminaCantiere(int idCant)
        {
            bool isEliminato = InserimentoDatiDAO.EliminaCantiere(idCant);
            if (isEliminato)
            {
                lblIsCantInserito.Text = "Cantiere eliminato con successo";
                lblIsCantInserito.ForeColor = Color.Blue;
            }
            else
            {
                lblIsCantInserito.Text = "Errore durante l'eliminazione del cantiere";
                lblIsCantInserito.ForeColor = Color.Red;
            }

            if (!(txtFiltroAnno.Text == "" && txtFiltroCodCant.Text == "" && txtFiltroDescr.Text == "" && txtFiltroCliente.Text == "" && chkFiltroChiuso.Checked == false && chkFiltroRiscosso.Checked == false))
                BindGridCantieriWithSearch();
            else
                BindGridCantieri();

            ResettaCampi(pnlTxtBoxCantContainer);
            txtCodCant.Enabled = false;
            btnModCantiere.Visible = false;
            btnInsCantiere.Visible = true;
            lblTitoloInserimento.Text = "Inserimento Cantieri";
        }
        protected void PopolaCampiCantiere(int idCant, bool isControlEnabled)
        {
            Cantieri cant = InserimentoDatiDAO.GetSingleCantiere(idCant);
            ListItem selectedListItem = ddlScegliClientePerCantiere.Items.FindByText(cant.RagSocCli);

            //Rendo i textbox disabilitati
            foreach (Control c in pnlTxtBoxCantContainer.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Enabled = isControlEnabled;
                else if (c is DropDownList)
                    ((DropDownList)c).Enabled = isControlEnabled;
                else if (c is CheckBox)
                    ((CheckBox)c).Enabled = isControlEnabled;
            }

            //Deseleziono tutti gli elementi della dropdownlist
            foreach (ListItem item in ddlScegliClientePerCantiere.Items)
                item.Selected = false;

            //Seleziono solamente l'item che mi interessa dalla DDL
            if (selectedListItem != null)
                selectedListItem.Selected = true;

            //Popolo i textbox
            txtDataInserCant.Text = cant.Data.ToString("dd-MM-yyyy");
            txtCodCant.Text = cant.CodCant;
            txtDescrCodCant.Text = cant.DescriCodCAnt;
            txtIndirizzoCant.Text = cant.Indirizzo;
            txtCittaCant.Text = cant.Città;
            txtRicaricoCant.Text = cant.Ricarico.ToString();
            txtPzzoManodopCant.Text = cant.PzzoManodopera.ToString();
            txtNumeroCant.Text = cant.Numero;
            txtValPrevCant.Text = cant.ValorePreventivo.ToString();
            txtIvaCant.Text = cant.Iva.ToString();
            txtAnnoCant.Text = cant.Anno.ToString();
            txtFasciaCant.Text = cant.FasciaTblCantieri.ToString();

            //Spunto i checkbox se necessario
            if (cant.Chiuso)
                chkCantChiuso.Checked = true;
            if (cant.Riscosso)
                chkCantRiscosso.Checked = true;
            if (cant.Preventivo)
                chkPreventivo.Checked = true;
            if (cant.DaDividere)
                chkDaDividere.Checked = true;
            if (cant.Diviso)
                chkDiviso.Checked = true;
            if (cant.Fatturato)
                chkFatturato.Checked = true;
        }
        protected void PopolaCodCantAnnoNumero(string num = "")
        {
            string numCant = "";
            if (num == "") { 
                txtNumeroCant.Text = InserimentoDatiDAO.GetLastNumCantForYear(txtAnnoCant.Text);
                numCant = txtNumeroCant.Text;
            }
            else
               numCant = num;

            DateTime date = DateTime.Now;
            string year = date.ToString("yy");
            string suffisso = "Ma";
            if(numCant.Length == 1)
                txtCodCant.Text = year + "00" + numCant + suffisso;
            else if(numCant.Length == 2)
                txtCodCant.Text = year + "0" + numCant + suffisso;
            else if (numCant.Length == 3)
                txtCodCant.Text = year + numCant + suffisso;
        }
        protected string CostruisceCodRiferCant()
        {
            DateTime date = DateTime.Now;

            int numCant = Convert.ToInt32(InserimentoDatiDAO.GetNumCantPerAnno(txtAnnoCant.Text));
            int descrLength = txtDescrCodCant.Text.Trim().Length;
            char firstDescrLetter = txtDescrCodCant.Text[0];
            string lastYearDigits = date.ToString("yy");
            int ddlLength = ddlScegliClientePerCantiere.SelectedItem.Text.Length;
            char lastRagSocLetter = ddlScegliClientePerCantiere.SelectedItem.Text[ddlLength-1];
            string dayOfYear = date.DayOfYear.ToString();

            string codRiferCant = Convert.ToString(numCant + descrLength) +
                firstDescrLetter + lastYearDigits + lastRagSocLetter + dayOfYear;

            return codRiferCant;
        }

        /* EVENTI ROW-COMMAND */
        /* In base al pulsante premuto eseguo azioni diversificate sul record selezionato */
        protected void grdClienti_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idCli = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName == "VisualCli")
                VisualizzaDatiCliente(idCli);
            else if (e.CommandName == "ModCli")
                ModificaDatiCliente(idCli);
            else if (e.CommandName == "ElimCli")
                EliminaCliente(idCli);
        }
        protected void grdFornitori_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idFornitore = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName == "VisualFornit")
                VisualizzaDatiFornitore(idFornitore);
            else if (e.CommandName == "ModFornit")
                ModificaDatiFornitore(idFornitore);
            else if (e.CommandName == "ElimFornit")
                EliminaFornitore(idFornitore);
        }
        protected void grdOperai_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idOperaio = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName == "VisualOper")
                VisualizzaDatiOperaio(idOperaio);
            else if (e.CommandName == "ModOper")
                ModificaDatiOperaio(idOperaio);
            else if (e.CommandName == "ElimOper")
                EliminaOperaio(idOperaio);
        }
        protected void grdCantieri_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idCant = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName == "VisualCant")
                VisualizzaDatiCant(idCant);
            else if (e.CommandName == "ModCant")
                ModificaDatiCant(idCant);
            else if (e.CommandName == "ElimCant")
                EliminaCantiere(idCant);
        }

        /* EVENTI TEXT-CHANGED */
        protected void txtNumeroCant_TextChanged(object sender, EventArgs e)
        {
            PopolaCodCantAnnoNumero(txtNumeroCant.Text);
        }
        protected void txtAnnoCant_TextChanged(object sender, EventArgs e)
        {
            PopolaCodCantAnnoNumero();
        }
    }
}