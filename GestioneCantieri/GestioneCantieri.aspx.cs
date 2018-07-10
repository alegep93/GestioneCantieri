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
    public partial class GestioneCantieri : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillAllDdl();
                ShowPanels(false, false, false, false, false, false, false);
                pnlSubIntestazione.Visible = pnlMascheraGestCant.Visible = false;
                grdMatCant.Visible = grdRientro.Visible = false;
                btnModMatCant.Visible = btnModRientro.Visible = false;
            }

            Page.MaintainScrollPositionOnPostBack = true;
        }

        /* HELPERS */
        protected void SvuotaIntestazione()
        {
            string tipol = txtTipDatCant.Text;

            //Svuoto tutti i TextBox
            foreach (Control c in pnlFiltriSceltaCant.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Text = "";
                if (c is CheckBox)
                    ((CheckBox)c).Checked = false;
            }
            foreach (Control c in pnlSubIntestazione.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Text = "";

                if (ddlScegliDDTMef.SelectedIndex != -1)
                    ddlScegliDDTMef.SelectedIndex = 0;

                ddlScegliFornit.SelectedIndex = 0;
            }

            //Textbox Tipologia sempre Disabilitato
            txtTipDatCant.Enabled = false;
            txtTipDatCant.Text = tipol;
        }
        protected void SvuotaCampi(Panel pnl)
        {
            //Svuoto tutti i TextBox
            foreach (Control c in pnl.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Text = "";
            }

            //Svuoto il DDL del listino solamente se è stato popolato
            if (ddlScegliListino.SelectedIndex != -1)
                ddlScegliListino.SelectedIndex = 0;

            //Visibile TRUE
            chkVisibile.Checked = chkManodopVisibile.Checked = chkOperVisibile.Checked = chkChiamVisibile.Checked = true;
            //Ricalcolo TRUE
            chkRicalcolo.Checked = true;
            //RicaricoSiNo TRUE
            chkRicarico.Checked = chkOperRicaricoSiNo.Checked = true;

            //Visibile FALSE
            chkArrotVisibile.Checked = false;
            //Ricalcolo FALSE
            chkManodopRicalcolo.Checked = chkOperRicalcolo.Checked = chkArrotRicalcolo.Checked = false;
            //RicaricoSiNo FALSE
            chkManodopRicaricoSiNo.Checked = chkArrotRicaricoSiNo.Checked = false;

            //Reimposto i textbox ai valori di default
            txtQta.Text = txtManodopQta.Text = txtOperQta.Text = txtArrotQta.Text = txtChiamQta.Text = txtSpesaPrezzo.Text = "";
            txtPzzoUnit.Text = txtChiamPzzoUnit.Text = txtSpesaPrezzoCalcolato.Text = "0.00";

            //Textbox Tipologia sempre Disabilitato
            txtTipDatCant.Enabled = false;

            //Reimposto il pzzoFinCli
            txtPzzoFinCli.Text = txtChiamPzzoFinCli.Text = "0.00";

            //Reimposto il campo Prezzo manodopera
            Cantieri cant = CantieriDAO.GetCantiere(ddlScegliCant.SelectedItem.Value);
            txtPzzoManodop.Text = cant.PzzoManodopera.ToString("N2");

            //Reimposto il DDLScegliOperaio del pannello GestioneOperaio
            ddlScegliOperaio.SelectedIndex = 0;
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
            foreach (Control c in pnlSubIntestazione.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Enabled = enableControls;
                else if (c is DropDownList)
                    ((DropDownList)c).Enabled = enableControls;
            }

            //Textbox Tipologia sempre Disabilitato
            txtTipDatCant.Enabled = false;
        }
        protected void ChooseFornitore(string nomeFornitore)
        {
            int i = 0;
            foreach (ListItem li in ddlScegliFornit.Items)
            {
                if (li.Text == nomeFornitore)
                {
                    ddlScegliFornit.SelectedIndex = i;
                    return;
                }
                i++;
            }
        }
        protected void BindAllGrid()
        {
            BindGridMatCant();
            BindGridManodop();
            BindGridOper();
            BindGridArrot();
            BindGridChiamata();
            BindGridSpese();
        }
        protected void HideMessageLabels()
        {
            lblIsRecordInserito.Text = lblIsManodopInserita.Text = lblIsOperInserita.Text =
               lblIsArrotondInserito.Text = lblIsSpesaInserita.Text = lblIsAChiamInserita.Text = "";
        }

        //Fill Ddl
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
        protected void FillDdlScegliAcquirente()
        {
            int i = 0;
            DataTable dt = OperaiDAO.GetOperai();
            List<Operai> listOperai = dt.DataTableToList<Operai>();

            ddlScegliAcquirente.Items.Clear();
            ddlScegliAcquirente.Items.Add(new ListItem("", "-1"));

            foreach (Operai op in listOperai)
            {
                string show = op.NomeOp + " - " + op.DescrOp;
                ddlScegliAcquirente.Items.Add(new ListItem(show, op.IdOperaio.ToString()));

                i++;
                if (op.NomeOp == "Maurizio" || op.NomeOp == "Mau" || op.NomeOp == "MAU")
                {
                    ddlScegliAcquirente.SelectedIndex = i;
                }
            }
        }
        protected void FillDdlScegliFornit()
        {
            DataTable dt = FornitoriDAO.GetFornitoriDataTable();
            List<Fornitori> listFornitori = dt.DataTableToList<Fornitori>();

            ddlScegliFornit.Items.Clear();
            ddlScegliFornit.Items.Add(new ListItem("", "-1"));

            foreach (Fornitori f in listFornitori)
            {
                string show = f.RagSocForni;
                ddlScegliFornit.Items.Add(new ListItem(show, f.IdFornitori.ToString()));
            }
        }
        protected void FillDdlScegliDdtMef()
        {
            DataTable dt = DDTMefDAO.GetDDT(txtFiltroAnnoDDT.Text, txtFiltroN_DDT.Text);
            List<DDTMef> listDDT = dt.DataTableToList<DDTMef>();

            ddlScegliDDTMef.Items.Clear();
            ddlScegliDDTMef.Items.Add(new ListItem("", "-1"));

            foreach (DDTMef ddt in listDDT)
            {
                string show = ddt.Data.ToString().Split(' ')[0] + " - " + ddt.N_ddt;
                ddlScegliDDTMef.Items.Add(new ListItem(show, ddt.Id.ToString()));
            }
        }
        protected void FillDddlScegliListino()
        {
            List<Mamg0> listMamg0 = Mamg0DAO.GetListino(txtFiltroCodFSS.Text, txtFiltroAA_Des.Text);

            ddlScegliListino.Items.Clear();
            ddlScegliListino.Items.Add(new ListItem("", "-1"));

            foreach (Mamg0 mmg in listMamg0)
            {
                string show = String.Format("{0,-18} | {1,-30} | {2,-8} | {3,-8} | {4,-3} | {5,-3} | {6,-3}",
                    mmg.CodArt, mmg.Desc, mmg.PrezzoNetto, mmg.PrezzoListino, mmg.Sconto1, mmg.Sconto2, mmg.Sconto3);
                ddlScegliListino.Items.Add(new ListItem(show, mmg.CodArt.ToString()));
            }
        }
        protected void FillDdlScegliMatCant()
        {
            List<MaterialiCantieri> listMatCant = MaterialiCantieriDAO.GetMaterialeCantiere(ddlScegliCant.SelectedItem.Value, txtFiltroMatCantCodArt.Text, txtFiltroMatCantDescriCodArt.Text);

            ddlScegliMatCant.Items.Clear();
            ddlScegliMatCant.Items.Add(new ListItem("", "-1"));

            foreach (MaterialiCantieri mc in listMatCant)
            {
                string show = mc.CodArt + " | " + mc.DescriCodArt + " | " + mc.Qta + " | " + mc.PzzoUniCantiere + " | " + mc.PzzoFinCli;
                ddlScegliMatCant.Items.Add(new ListItem(show, mc.IdMaterialiCantieri.ToString()));
            }
        }

        //Ogni Helper "Fill" va aggiunto qua dentro per essere richiamato all'apertura dell'applicazione
        protected void FillAllDdl()
        {
            FillDdlScegliCant();
            FillDdlScegliAcquirente();
            FillDdlScegliFornit();
            FillDdlScegliMatCant();

            //Per la sezione Gestione Operaio
            FillDdlScegliOperaio();

            //Per la sezione Gestione Spese
            FillDdlScegliSpesa();
        }

        //Mostra/Nasconde pannelli
        protected void ShowPanels(bool pnlMatDaDDT, bool pnlMatCant, bool pnlManodop, bool pnlOper, bool pnlArrotond, bool pnlSpese, bool pnlChiam)
        {
            pnlMascheraMaterialiDaDDT.Visible = pnlMatDaDDT;
            pnlMascheraGestCant.Visible = pnlMatCant;
            pnlManodopera.Visible = pnlManodop;
            pnlGestioneOperaio.Visible = pnlOper;
            pnlGestArrotond.Visible = pnlArrotond;
            pnlGestSpese.Visible = pnlSpese;
            pnlGestChiamata.Visible = pnlChiam;
        }

        //Switcha classi css per bottoni di scelta pannello
        //protected void RemoveActiveClass()
        //{
        //    foreach (var b in pnlScegliMaschera.Controls)
        //    {
        //        if (b.GetType().Name == "Button")
        //        {
        //            (((Button)b).CssClass).Replace(" active", "");
        //        }
        //    }
        //}

        //Controlla che l'intestazione sia completamente compilata prima di inserire il record

        protected bool isIntestazioneCompilata()
        {
            if (ddlScegliCant.SelectedIndex != 0 && ddlScegliAcquirente.SelectedIndex != 0 && ddlScegliFornit.SelectedIndex != 0 && (ddlScegliDDTMef.SelectedIndex != 0 || txtNumBolla.Text != "") && txtDataDDT.Text != "" && txtFascia.Text != "" && txtProtocollo.Text != "")
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
                lblIsRecordInserito.Text = lblIsManodopInserita.Text = lblIsOperInserita.Text = lblIsArrotondInserito.Text = lblIsAChiamInserita.Text = lblIsSpesaInserita.Text = "Inserire un valore per la data";
                lblIsRecordInserito.ForeColor = lblIsManodopInserita.ForeColor = lblIsOperInserita.ForeColor = lblIsArrotondInserito.ForeColor = lblIsAChiamInserita.ForeColor = lblIsSpesaInserita.ForeColor = Color.Red;
                return true;
            }
            return false;
        }

        /* EVENTI CLICK */
        protected void btnFiltroCant_Click(object sender, EventArgs e)
        {
            FillDdlScegliCant();
            pnlSubIntestazione.Visible = false;
        }
        protected void btnSvuotaIntestazione_Click(object sender, EventArgs e)
        {
            SvuotaIntestazione();
        }
        protected void btnGenetaNumBolla_Click(object sender, EventArgs e)
        {
            if (txtDataDDT.Text != "")
            {
                if (txtProtocollo.Text != "")
                {
                    string[] datePart = txtDataDDT.Text.Split('-');
                    txtNumBolla.Text = datePart[0].Trim() + datePart[1].Trim() + datePart[2].Trim() + txtProtocollo.Text;
                }
                else
                {
                    lblErroreGeneraNumBolla.Text = "Devi prima compilare il campo Protocollo";
                    lblErroreGeneraNumBolla.ForeColor = Color.Red;
                }
            }
            else
            {
                lblErroreGeneraNumBolla.Text = "Devi prima compilare il campo Data DDT";
                lblErroreGeneraNumBolla.ForeColor = Color.Red;
            }
        }
        protected void btnCalcolaPrezzoUnit_Click(object sender, EventArgs e)
        {
            if (txtPzzoNettoMef.Text != "")
                txtPzzoUnit.Text = Math.Round(Convert.ToDecimal(txtPzzoNettoMef.Text.Replace(".", ",")), 2).ToString();
            else
            {
                lblIsRecordInserito.Text = "Inserire un valore nella casella 'Prezzo Netto Mef' per calcolare il 'Prezzo Unitario'";
                lblIsRecordInserito.ForeColor = Color.Red;
            }

            if (txtTipDatCant.Text == "MATERIALE")
                btnInserisciMatCant.Focus();
            else if (txtTipDatCant.Text == "RIENTRO")
                btnInserisciRientro.Focus();
        }

        /* EVENTI TEXT-CHANGED */
        protected void ddlScegliCant_TextChanged(object sender, EventArgs e)
        {
            Cantieri cant = CantieriDAO.GetCantiere(ddlScegliCant.SelectedItem.Value);
            txtPzzoManodop.Text = cant.PzzoManodopera.ToString("N2");
            txtFascia.Text = cant.FasciaTblCantieri.ToString();

            if (ddlScegliCant.SelectedIndex != 0)
            {
                pnlSubIntestazione.Visible = true;
            }
            else
            {
                pnlSubIntestazione.Visible = false;
                pnlMascheraGestCant.Visible = false;
            }

            FillDdlScegliMatCant();
            FillDdlScegliAcquirente();
            BindAllGrid();
        }
        protected void txtFiltroAnnoDDT_TextChanged(object sender, EventArgs e)
        {
            FillDdlScegliDdtMef();
        }
        protected void txtFiltroN_DDT_TextChanged(object sender, EventArgs e)
        {
            FillDdlScegliDdtMef();
        }
        protected void ddlScegliDDTMef_TextChanged(object sender, EventArgs e)
        {
            if (ddlScegliDDTMef.SelectedIndex != 0)
            {
                txtNumBolla.Enabled = false;
                BindGridMatDaDDT();
            }
            else
            {
                txtNumBolla.Enabled = true;
            }
        }

        #region Materiali da DDT
        /* HELPERS */
        private void BindGridMatDaDDT()
        {
            if (ddlScegliDDTMef.SelectedItem != null && ddlScegliDDTMef.SelectedItem.Text != "" && ddlScegliDDTMef.SelectedIndex != 0)
            {
                string nDDT = ddlScegliDDTMef.SelectedItem.Text.Split('-')[1].Trim();
                List<DDTMef> ddtList = DDTMefDAO.GetDDTByNumDDT(nDDT);
                grdMostraDDTDaInserire.DataSource = ddtList;
                grdMostraDDTDaInserire.DataBind();

                if (ddtList.Count > 0)
                    btnInsMatDaDDT.Enabled = true;
            }
        }
        private MaterialiCantieri PopolaMcObject(int numRiga)
        {
            MaterialiCantieri mc = new MaterialiCantieri();
            mc.IdTblCantieri = Convert.ToInt32(ddlScegliCant.SelectedItem.Value);
            mc.DescriMateriali = grdMostraDDTDaInserire.Rows[numRiga].Cells[3].Text;
            mc.Qta = Convert.ToInt32(grdMostraDDTDaInserire.Rows[numRiga].Cells[4].Text);
            mc.Data = Convert.ToDateTime(grdMostraDDTDaInserire.Rows[numRiga].Cells[0].Text);
            mc.PzzoUniCantiere = Convert.ToDecimal(grdMostraDDTDaInserire.Rows[numRiga].Cells[5].Text);
            mc.CodArt = grdMostraDDTDaInserire.Rows[numRiga].Cells[2].Text;
            mc.DescriCodArt = grdMostraDDTDaInserire.Rows[numRiga].Cells[3].Text;
            mc.Tipologia = txtTipDatCant.Text;
            mc.Fascia = Convert.ToInt32(txtFascia.Text);

            // Recupero l'id dell'acquirente dal nome
            string acquirente = grdMostraDDTDaInserire.Rows[numRiga].Cells[6].Text;
            mc.Acquirente = OperaiDAO.GetIdAcquirente(acquirente);

            mc.Fornitore = ddlScegliFornit.SelectedItem.Value;
            mc.NumeroBolla = grdMostraDDTDaInserire.Rows[numRiga].Cells[1].Text;
            mc.ProtocolloInterno = Convert.ToInt32(txtProtocollo.Text);
            mc.Visibile = mc.Ricalcolo = mc.RicaricoSiNo = true;
            return mc;
        }

        /* EVENTI CLICK*/
        protected void btnMatCantFromDDT_Click(object sender, EventArgs e)
        {
            lblTitoloMaschera.Text = "Materiali da DDT";
            txtTipDatCant.Text = "MATERIALE";
            grdMostraDDTDaInserire.Visible = true;
            ShowPanels(true, false, false, false, false, false, false);
            grdMatCant.Visible = true;
            grdRientro.Visible = false;
            BindGridMatCant();
            BindGridMatDaDDT();
            EnableDisableControls(true, pnlMascheraMaterialiDaDDT);
            SvuotaCampi(pnlMascheraMaterialiDaDDT);
            ChooseFornitore("Mef");
            HideMessageLabels();

            if (grdMostraDDTDaInserire.Rows.Count == 0)
            {
                btnInsMatDaDDT.Enabled = false;
            }
        }
        protected void btnInsMatDaDDT_Click(object sender, EventArgs e)
        {
            if (txtProtocollo.Text != "")
            {
                for (int i = 0; i < grdMostraDDTDaInserire.Rows.Count; i++)
                {
                    bool daInserire = ((CheckBox)grdMostraDDTDaInserire.Rows[i].FindControl("chkDDTSelezionato")).Checked;

                    if (daInserire)
                    {
                        MaterialiCantieri mc = PopolaMcObject(i);
                        MaterialiCantieriDAO.InserisciMaterialeCantiere(mc);

                        lblInsMatDaDDT.Text = "Materiali inseriti con successo";
                        lblInsMatDaDDT.ForeColor = Color.Blue;
                    }
                }
            }
            else
            {
                lblInsMatDaDDT.Text = "È necessario specificare il protocollo prima di inserire i materiali";
                lblInsMatDaDDT.ForeColor = Color.Red;
            }
        }
        protected void btnSelezionaTutto_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grdMostraDDTDaInserire.Rows.Count; i++)
            {
                ((CheckBox)grdMostraDDTDaInserire.Rows[i].FindControl("chkDDTSelezionato")).Checked = true;
            }
        }
        protected void btnDeselezionaTutto_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grdMostraDDTDaInserire.Rows.Count; i++)
            {
                ((CheckBox)grdMostraDDTDaInserire.Rows[i].FindControl("chkDDTSelezionato")).Checked = false;
            }
        }
        #endregion

        #region Materiali Cantieri e Rientro
        decimal maxQtaRientro = -1;

        /* HELPERS */
        protected void FillMatCant(MaterialiCantieri mc)
        {
            mc.IdTblCantieri = Convert.ToInt32(ddlScegliCant.SelectedItem.Value);
            mc.DescriMateriali = txtDescrMat.Text;
            mc.Visibile = chkVisibile.Checked;
            mc.Ricalcolo = chkRicalcolo.Checked;
            mc.RicaricoSiNo = chkRicarico.Checked;
            mc.Data = Convert.ToDateTime(txtDataDDT.Text);
            mc.PzzoUniCantiere = Convert.ToDecimal(txtPzzoUnit.Text);
            mc.CodArt = txtCodArt.Text;
            mc.DescriCodArt = txtDescriCodArt.Text;
            mc.Tipologia = txtTipDatCant.Text;
            mc.Acquirente = ddlScegliAcquirente.SelectedItem.Value;
            mc.Fornitore = ddlScegliFornit.SelectedItem.Value;
            mc.Note = txtNote.Text;
            mc.Note2 = txtNote_2.Text;

            if (mc.Tipologia == "MATERIALE")
                mc.Qta = Convert.ToDouble(txtQta.Text);
            else if (mc.Tipologia == "RIENTRO")
                mc.Qta = (Convert.ToDouble(txtQta.Text)) * (-1);

            if (txtFascia.Text != "")
                mc.Fascia = Convert.ToInt32(txtFascia.Text);
            else
                mc.Fascia = 0;

            if (txtProtocollo.Text != "")
                mc.ProtocolloInterno = Convert.ToInt32(txtProtocollo.Text);
            else
                mc.ProtocolloInterno = 0;

            if (txtNumBolla.Enabled && txtNumBolla.Text != "")
                mc.NumeroBolla = txtNumBolla.Text;
            else if (ddlScegliDDTMef.SelectedIndex != -1)
                mc.NumeroBolla = (ddlScegliDDTMef.SelectedItem.Text).Split('-')[3];
            else
                mc.NumeroBolla = "";

            if (txtPzzoFinCli.Text != "")
                mc.PzzoFinCli = Convert.ToDecimal(txtPzzoFinCli.Text);
            else
                mc.PzzoFinCli = 0.0m;
        }
        protected void ShowForMatCant()
        {
            lblScegliListino.Visible = true;
            ddlScegliListino.Visible = true;
            lblFiltroCod_FSS.Visible = true;
            txtFiltroCodFSS.Visible = true;
            lblFiltroAA_Des.Visible = true;
            txtFiltroAA_Des.Visible = true;
            lblScegliMatCant.Visible = false;
            ddlScegliMatCant.Visible = false;
            btnInserisciMatCant.Visible = true;
            btnInserisciRientro.Visible = false;
            lblFiltroMatCantCodArt.Visible = txtFiltroMatCantCodArt.Visible = lblFiltroMatCantDescriCodArt.Visible = txtFiltroMatCantDescriCodArt.Visible = false;
        }
        protected void ShowForRientro()
        {
            lblScegliListino.Visible = false;
            ddlScegliListino.Visible = false;
            lblFiltroCod_FSS.Visible = false;
            txtFiltroCodFSS.Visible = false;
            lblFiltroAA_Des.Visible = false;
            txtFiltroAA_Des.Visible = false;
            lblScegliMatCant.Visible = true;
            ddlScegliMatCant.Visible = true;
            btnInserisciMatCant.Visible = false;
            btnInserisciRientro.Visible = true;
            lblFiltroMatCantCodArt.Visible = txtFiltroMatCantCodArt.Visible = lblFiltroMatCantDescriCodArt.Visible = txtFiltroMatCantDescriCodArt.Visible = true;
        }
        protected void BindGridMatCant()
        {
            List<MaterialiCantieri> mcList = MaterialiCantieriDAO.GetMaterialeCantiereForGridView(ddlScegliCant.SelectedItem.Value, txtFiltroCodArtGrdMatCant.Text,
                txtFiltroDescriCodArtGrdMatCant.Text, txtFiltroProtocolloGrdMatCant.Text, txtFiltroFornitoreGrdMatCant.Text, "MATERIALE");
            grdMatCant.DataSource = mcList;
            grdMatCant.DataBind();
            CalcolaTotaleValore(mcList);
        }
        protected void BindGridRientro()
        {
            List<MaterialiCantieri> mcList = MaterialiCantieriDAO.GetMaterialeCantiereForGridView(ddlScegliCant.SelectedItem.Value, txtFiltroCodArtGrdMatCant.Text,
                txtFiltroDescriCodArtGrdMatCant.Text, txtFiltroProtocolloGrdMatCant.Text, txtFiltroFornitoreGrdMatCant.Text, "RIENTRO");
            grdRientro.DataSource = mcList;
            grdRientro.DataBind();
            CalcolaTotaleValore(mcList);
        }
        private void CalcolaTotaleValore(List<MaterialiCantieri> mcList)
        {
            double valore = 0;

            for (int i = 0; i < mcList.Count; i++)
            {
                valore += Convert.ToDouble(grdMatCant.Rows[i].Cells[5].Text) * Convert.ToDouble(grdMatCant.Rows[i].Cells[6].Text);
            }

            lblTotaleValoreMatCant_Rientro.Text = "Totale Valore: " + valore.ToString("N2") + "€";
        }

        /* EVENTI CLICK */
        protected void btnInserisciMatCant_Click(object sender, EventArgs e)
        {
            bool isInserito = false;

            string idCant = ddlScegliCant.SelectedItem.Value;
            string acquirente = ddlScegliAcquirente.SelectedItem.Value;
            string fornitore = ddlScegliFornit.SelectedItem.Value;
            string numeroBolla = "";

            if (isDateNotSet())
                return;

            MaterialiCantieri mc = new MaterialiCantieri();
            FillMatCant(mc);

            if ((Convert.ToDecimal(txtQta.Text) > 0 && txtQta.Text != "") && Convert.ToDecimal(txtPzzoUnit.Text) > 0)
            {
                if (ddlScegliDDTMef.SelectedItem == null || ddlScegliDDTMef.SelectedItem.Text == "")
                {
                    if (txtNumBolla.Text != "")
                        numeroBolla = txtNumBolla.Text;
                    else
                    {
                        lblIsRecordInserito.Text = "Scegliere un DDT dal menù a discesa o compilare il campo \"Numero Bolla\"";
                        lblIsRecordInserito.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    numeroBolla = (ddlScegliDDTMef.SelectedItem.Text).Split('-')[3];
                }

                if (isIntestazioneCompilata())
                {
                    if (txtCodArt.Text != "" && txtDescriCodArt.Text != "")
                    {
                        isInserito = MaterialiCantieriDAO.InserisciMaterialeCantiere(mc);
                    }
                    else
                    {
                        lblIsRecordInserito.Text = "Codice Articolo e Descrizione Codice Articolo obbligatori!";
                        lblIsRecordInserito.ForeColor = Color.Red;
                        return;
                    }
                }

                if (isInserito)
                {
                    lblIsRecordInserito.Text = "Record inserito con successo";
                    lblIsRecordInserito.ForeColor = Color.Blue;
                }
                else
                {
                    lblIsRecordInserito.Text = "Errore durante l'inserimento del record. L'intestazione deve essere interamente compilata.";
                    lblIsRecordInserito.ForeColor = Color.Red;
                }
            }
            else
            {
                lblIsRecordInserito.Text = "Quantità e/o Prezzo Unitario devono essere maggiori di 0";
                lblIsRecordInserito.ForeColor = Color.Red;
            }

            BindGridMatCant();
            SvuotaCampi(pnlMascheraGestCant);

            txtFiltroCodFSS.Focus();
        }
        protected void btnInserisciRientro_Click(object sender, EventArgs e)
        {
            bool isInserito = false;

            string idCant = ddlScegliCant.SelectedItem.Value;
            string acquirente = ddlScegliAcquirente.SelectedItem.Value;
            string fornitore = ddlScegliFornit.SelectedItem.Value;
            string numeroBolla = "";

            if (isDateNotSet())
                return;

            MaterialiCantieri mc = new MaterialiCantieri();
            FillMatCant(mc);

            if ((txtQta.Text != "" && txtQta.Text != "0") && Convert.ToDecimal(txtPzzoUnit.Text) > 0)
            {
                if (ddlScegliDDTMef.SelectedItem == null || ddlScegliDDTMef.SelectedItem.Text == "")
                {
                    if (txtNumBolla.Text != "")
                        numeroBolla = txtNumBolla.Text;
                    else
                    {
                        lblIsRecordInserito.Text = "Scegliere un DDT dal menù a discesa o compilare il campo \"Numero Bolla\"";
                        lblIsRecordInserito.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    numeroBolla = (ddlScegliDDTMef.SelectedItem.Text).Split('-')[1];
                }

                if (isIntestazioneCompilata())
                {
                    string[] partiMatCant = ddlScegliMatCant.SelectedItem.Text.Split('|');
                    maxQtaRientro = Convert.ToDecimal(partiMatCant[2].Trim());

                    if (Convert.ToInt32(txtQta.Text) <= maxQtaRientro)
                    {
                        isInserito = MaterialiCantieriDAO.InserisciMaterialeCantiere(mc);
                    }
                    else
                    {
                        lblIsRecordInserito.Text = "La quantità non deve superare quella specificata nel record di materiale cantiere";
                        lblIsRecordInserito.ForeColor = Color.Red;
                        return;
                    }
                }

                if (isInserito)
                {
                    lblIsRecordInserito.Text = "Record inserito con successo";
                    lblIsRecordInserito.ForeColor = Color.Blue;
                }
                else
                {
                    lblIsRecordInserito.Text = "Errore durante l'inserimento del record. L'intestazione deve essere interamente compilata";
                    lblIsRecordInserito.ForeColor = Color.Red;
                }
            }
            else
            {
                lblIsRecordInserito.Text = "Quantità e/o Prezzo Unitario devono essere maggiori di 0";
                lblIsRecordInserito.ForeColor = Color.Red;
            }

            BindGridRientro();
            SvuotaCampi(pnlMascheraGestCant);

            txtFiltroCodFSS.Focus();
        }
        protected void btnFiltraGrdMatCant_Click(object sender, EventArgs e)
        {
            if (txtTipDatCant.Text == "MATERIALE")
                BindGridMatCant();
            else if (txtTipDatCant.Text == "RIENTRO")
                BindGridRientro();
        }
        //Visibilità pannelli
        protected void btnMatCant_Click(object sender, EventArgs e)
        {
            lblTitoloMaschera.Text = "Inserisci Materiali Cantieri";
            txtTipDatCant.Text = "MATERIALE";
            ShowForMatCant();
            ShowPanels(false, true, false, false, false, false, false);
            grdMatCant.Visible = true;
            grdRientro.Visible = false;
            btnModMatCant.Visible = false;
            BindGridMatCant();
            EnableDisableControls(true, pnlMascheraGestCant);
            SvuotaCampi(pnlMascheraGestCant);
            ChooseFornitore("Mef");
            HideMessageLabels();
        }
        protected void btnRientro_Click(object sender, EventArgs e)
        {
            lblTitoloMaschera.Text = "Inserisci Rientro Materiali";
            txtTipDatCant.Text = "RIENTRO";
            FillDdlScegliMatCant();
            ShowForRientro();
            ShowPanels(false, true, false, false, false, false, false);
            grdMatCant.Visible = false;
            grdRientro.Visible = true;
            btnModMatCant.Visible = btnInserisciMatCant.Visible = btnModRientro.Visible = false;
            BindGridRientro();
            EnableDisableControls(true, pnlMascheraGestCant);
            SvuotaCampi(pnlMascheraGestCant);
            ChooseFornitore("Rientro");
            HideMessageLabels();
        }

        /* EVENTI TEXT-CHANGED */
        protected void txtFiltroCodFSS_TextChanged(object sender, EventArgs e)
        {
            FillDddlScegliListino();
        }
        protected void txtFiltroAA_Des_TextChanged(object sender, EventArgs e)
        {
            FillDddlScegliListino();
        }
        protected void ddlScegliListino_TextChanged(object sender, EventArgs e)
        {
            if (ddlScegliListino.SelectedIndex != 0)
            {
                string[] partiListino = ddlScegliListino.SelectedItem.Text.Split('|');
                txtCodArt.Text = partiListino[0].Trim();
                txtDescriCodArt.Text = partiListino[1].Trim();
                txtPzzoNettoMef.Text = partiListino[2].Trim();
                txtPzzoUnit.Text = "0.00";
            }
            else
            {
                txtCodArt.Text = txtDescriCodArt.Text = txtPzzoNettoMef.Text = "";
                txtPzzoUnit.Text = "0.00";
            }

            HideMessageLabels();
            txtQta.Focus();
        }
        protected void ddlScegliMatCant_TextChanged(object sender, EventArgs e)
        {
            if (ddlScegliListino.SelectedIndex != 0)
            {
                string[] partiMatCant = ddlScegliMatCant.SelectedItem.Text.Split('|');
                txtCodArt.Text = partiMatCant[0].Trim();
                txtDescriCodArt.Text = partiMatCant[1].Trim();
                txtQta.Text = partiMatCant[2].Trim();
                txtPzzoNettoMef.Text = partiMatCant[3].Trim();
                txtPzzoUnit.Text = "0.00";
                txtPzzoFinCli.Text = partiMatCant[4].Trim();
            }
            else
            {
                txtCodArt.Text = txtDescriCodArt.Text = txtPzzoNettoMef.Text = txtPzzoFinCli.Text = "";
                txtPzzoUnit.Text = "0.00";
            }

            HideMessageLabels();
            txtQta.Focus();
        }
        protected void txtFiltroMatCantCodArt_TextChanged(object sender, EventArgs e)
        {
            FillDdlScegliMatCant();
        }
        protected void txtFiltroMatCantDescriCodArt_TextChanged(object sender, EventArgs e)
        {
            FillDdlScegliMatCant();
        }
        protected void txtCodArt_TextChanged(object sender, EventArgs e)
        {
            HideMessageLabels();
        }
        protected void txtDescriCodArt_TextChanged(object sender, EventArgs e)
        {
            HideMessageLabels();
        }

        /* EVENTI PER LA GESTIONE DEI ROWCOMMAND */
        //MatCant
        protected void grdMatCant_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idMatCant = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName == "VisualMatCant")
                VisualizzaDatiMatCant(idMatCant);
            else if (e.CommandName == "ModMatCant")
                ModificaDatiMatCant(idMatCant);
            else if (e.CommandName == "ElimMatCant")
                EliminaMatCant(idMatCant);
        }
        private void PopolaCampiMatCant(int id, bool enableControls)
        {
            MaterialiCantieri mc = MaterialiCantieriDAO.GetSingleMaterialeCantiere(id);

            //Rendo i textbox abilitati/disabilitati
            EnableDisableControls(enableControls, pnlMascheraGestCant);

            ddlScegliAcquirente.SelectedValue = mc.Acquirente;
            ddlScegliFornit.SelectedValue = mc.Fornitore;
            txtTipDatCant.Text = mc.Tipologia;
            txtNumBolla.Text = mc.NumeroBolla.ToString();
            txtDataDDT.Text = mc.Data.ToString("yyyy-MM-dd");
            txtDataDDT.TextMode = TextBoxMode.Date;
            txtFascia.Text = mc.Fascia.ToString();
            txtProtocollo.Text = mc.ProtocolloInterno.ToString();
            txtCodArt.Text = mc.CodArt;
            txtDescriCodArt.Text = mc.DescriCodArt;
            txtDescrMat.Text = mc.DescriMateriali;
            txtNote.Text = mc.Note;
            txtNote_2.Text = mc.Note2;
            txtPzzoUnit.Text = mc.PzzoUniCantiere.ToString();
            txtPzzoFinCli.Text = mc.PzzoFinCli.ToString();
            chkVisibile.Checked = mc.Visibile;
            chkRicalcolo.Checked = mc.Ricalcolo;
            chkRicarico.Checked = mc.RicaricoSiNo;
            txtPzzoFinCli.Text = mc.PzzoFinCli.ToString();

            if (txtTipDatCant.Text == "MATERIALE")
                txtQta.Text = mc.Qta.ToString();
            else if (txtTipDatCant.Text == "RIENTRO")
                txtQta.Text = (mc.Qta * (-1)).ToString();
        }
        private void PopolaObjMatCant(MaterialiCantieri mc)
        {
            mc.IdTblCantieri = Convert.ToInt32(ddlScegliCant.SelectedItem.Value);
            mc.Acquirente = ddlScegliAcquirente.SelectedItem.Value;
            mc.Fornitore = ddlScegliFornit.SelectedItem.Value;
            mc.Tipologia = txtTipDatCant.Text;
            mc.NumeroBolla = txtNumBolla.Text;
            mc.Data = Convert.ToDateTime(txtDataDDT.Text);
            mc.Fascia = Convert.ToInt32(txtFascia.Text);
            mc.ProtocolloInterno = Convert.ToInt32(txtProtocollo.Text);
            mc.CodArt = txtCodArt.Text;
            mc.DescriCodArt = txtDescriCodArt.Text;
            mc.DescriMateriali = txtDescrMat.Text;
            mc.Note = txtNote.Text;
            mc.Note2 = txtNote_2.Text;
            mc.PzzoUniCantiere = Convert.ToDecimal(txtPzzoUnit.Text);
            mc.PzzoFinCli = Convert.ToDecimal(txtPzzoFinCli.Text);
            mc.Visibile = chkVisibile.Checked;
            mc.Ricalcolo = chkRicalcolo.Checked;
            mc.RicaricoSiNo = chkRicarico.Checked;
            mc.PzzoFinCli = Convert.ToDecimal(txtPzzoFinCli.Text);

            if (mc.Tipologia == "MATERIALE")
                mc.Qta = Convert.ToDouble(txtQta.Text);
            else if (mc.Tipologia == "RIENTRO")
                mc.Qta = (Convert.ToDouble(txtQta.Text)) * (-1);
        }
        private void VisualizzaDatiMatCant(int id)
        {
            lblTitoloMaschera.Text = "Visualizza Materiali Cantieri";
            btnInserisciMatCant.Visible = btnModMatCant.Visible = false;
            PopolaCampiMatCant(id, false);
            HideMessageLabels();
        }
        private void ModificaDatiMatCant(int id)
        {
            lblTitoloMaschera.Text = "Modifica Materiali Cantieri";
            btnInserisciMatCant.Visible = false;
            btnModRientro.Visible = false;
            btnModMatCant.Visible = true;
            PopolaCampiMatCant(id, true);
            BindGridMatCant();
            hidIdMatCant.Value = id.ToString();
            HideMessageLabels();
        }
        private void EliminaMatCant(int id)
        {
            bool isDeleted = MaterialiCantieriDAO.DeleteMatCant(id);

            if (isDeleted)
            {
                lblIsRecordInserito.Text = "Record eliminato con successo";
                lblIsRecordInserito.ForeColor = Color.Blue;
            }
            else
            {
                lblIsRecordInserito.Text = "Errore durante l'eliminazione del record";
                lblIsRecordInserito.ForeColor = Color.Red;
            }

            BindGridMatCant();
        }
        protected void btnModMatCant_Click(object sender, EventArgs e)
        {
            if ((Convert.ToDecimal(txtQta.Text) > 0 && txtQta.Text != "") && Convert.ToDecimal(txtPzzoUnit.Text) > 0)
            {
                MaterialiCantieri mc = new MaterialiCantieri();
                PopolaObjMatCant(mc);
                bool isUpdated = MaterialiCantieriDAO.UpdateMatCant(hidIdMatCant.Value, mc);

                if (isUpdated)
                {
                    lblIsRecordInserito.Text = "Record modificato con successo";
                    lblIsRecordInserito.ForeColor = Color.Blue;
                }
                else
                {
                    lblIsRecordInserito.Text = "Errore durante la modifica del record";
                    lblIsRecordInserito.ForeColor = Color.Red;
                }

                BindGridMatCant();
                SvuotaCampi(pnlMascheraGestCant);

                btnInserisciMatCant.Visible = true;
                btnModMatCant.Visible = false;
                lblTitoloMaschera.Text = "Inserisci Materiali Cantieri";
            }
            else
            {
                lblIsRecordInserito.Text = "Quantità e/o Prezzo Unitario devono essere maggiori di 0";
                lblIsRecordInserito.ForeColor = Color.Red;
            }
        }

        //Rientro
        protected void grdRientro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idRientro = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName == "VisualRientro")
                VisualizzaDatiRientro(idRientro);
            else if (e.CommandName == "ModRientro")
                ModificaDatiRientro(idRientro);
            else if (e.CommandName == "ElimRientro")
                EliminaRientro(idRientro);
        }
        private void VisualizzaDatiRientro(int idRientro)
        {
            lblTitoloMaschera.Text = "Visualizza Rientro Materiali";
            btnInserisciRientro.Visible = btnModRientro.Visible = false;
            PopolaCampiMatCant(idRientro, false);
            HideMessageLabels();
        }
        private void ModificaDatiRientro(int idRientro)
        {
            lblTitoloMaschera.Text = "Modifica Rientro Materiali";
            btnInserisciRientro.Visible = false;
            btnModRientro.Visible = true;
            btnModMatCant.Visible = false;
            PopolaCampiMatCant(idRientro, true);
            hidIdMatCant.Value = idRientro.ToString();
            HideMessageLabels();
        }
        private void EliminaRientro(int idRientro)
        {
            bool isDeleted = MaterialiCantieriDAO.DeleteMatCant(idRientro);

            if (isDeleted)
            {
                lblIsRecordInserito.Text = "Record eliminato con successo";
                lblIsRecordInserito.ForeColor = Color.Blue;
            }
            else
            {
                lblIsRecordInserito.Text = "Errore durante l'eliminazione del record";
                lblIsRecordInserito.ForeColor = Color.Red;
            }

            BindGridRientro();
        }
        protected void btnModRientro_Click(object sender, EventArgs e)
        {
            if ((txtQta.Text != "" && txtQta.Text != "0") && Convert.ToDecimal(txtPzzoUnit.Text) > 0)
            {
                MaterialiCantieri mc = new MaterialiCantieri();
                PopolaObjMatCant(mc);
                bool isUpdated = MaterialiCantieriDAO.UpdateMatCant(hidIdMatCant.Value, mc);

                if (isUpdated)
                {
                    lblIsRecordInserito.Text = "Record modificato con successo";
                    lblIsRecordInserito.ForeColor = Color.Blue;
                }
                else
                {
                    lblIsRecordInserito.Text = "Errore durante la modifica del record";
                    lblIsRecordInserito.ForeColor = Color.Red;
                }

                BindGridRientro();
                SvuotaCampi(pnlMascheraGestCant);

                btnInserisciRientro.Visible = true;
                btnModRientro.Visible = false;
                lblTitoloMaschera.Text = "Inserisci Rientro Materiali";
            }
            else
            {
                lblIsRecordInserito.Text = "Quantità e/o Prezzo Unitario devono essere maggiori di 0";
                lblIsRecordInserito.ForeColor = Color.Red;
            }
        }
        #endregion

        #region Manodopera
        /* HELPERS */
        protected void FillManodopMatCant(MaterialiCantieri mc)
        {
            Operai op = OperaiDAO.GetOperaio(ddlScegliAcquirente.SelectedItem.Value);
            mc.CodArt = "Manodopera" + op.Operaio;
            mc.DescriCodArt = "Manodopera" + op.Operaio;

            mc.IdTblCantieri = Convert.ToInt32(ddlScegliCant.SelectedItem.Value);
            mc.Acquirente = ddlScegliAcquirente.SelectedItem.Value;
            mc.Fornitore = ddlScegliFornit.SelectedItem.Value;
            mc.Qta = Convert.ToDouble(txtManodopQta.Text.Replace(".", ","));
            mc.Tipologia = txtTipDatCant.Text;
            mc.ProtocolloInterno = Convert.ToInt32(txtProtocollo.Text);
            mc.DescriMateriali = txtDescrManodop.Text;
            mc.Data = Convert.ToDateTime(txtDataDDT.Text);
            mc.Note = txtNote1.Text;
            mc.Note2 = txtNote2.Text;
            mc.Visibile = chkManodopVisibile.Checked;
            mc.Ricalcolo = chkManodopRicalcolo.Checked;
            mc.RicaricoSiNo = chkManodopRicaricoSiNo.Checked;
            mc.NumeroBolla = txtNumBolla.Text;
            mc.Fascia = Convert.ToInt32(txtFascia.Text);

            if (txtPzzoManodop.Text != "")
                mc.PzzoUniCantiere = Convert.ToDecimal(txtPzzoManodop.Text.Replace(".", ","));
            else
                mc.PzzoUniCantiere = 0;
        }
        protected void BindGridManodop()
        {
            List<MaterialiCantieri> mcList = MaterialiCantieriDAO.GetMaterialeCantiereForGridView(ddlScegliCant.SelectedItem.Value, txtFiltroManodopCodArt.Text,
                txtFiltroManodopDescriCodArt.Text, txtFiltroProtocolloGrdMatCant.Text, txtFiltroFornitoreGrdMatCant.Text, "MANODOPERA");
            grdManodop.DataSource = mcList;
            grdManodop.DataBind();
        }

        /* EVENTI CLICK */
        protected void btnInsManodop_Click(object sender, EventArgs e)
        {
            bool isInserito = false;

            if (isDateNotSet())
                return;

            MaterialiCantieri mc = new MaterialiCantieri();
            FillManodopMatCant(mc);

            if (txtDataDDT.Text != "")
            {
                if ((Convert.ToDecimal(txtManodopQta.Text) > 0 && txtManodopQta.Text != ""))
                {
                    if (isIntestazioneCompilata())
                        isInserito = MaterialiCantieriDAO.InserisciMaterialeCantiere(mc);

                    if (isInserito)
                    {
                        lblIsManodopInserita.Text = "Record inserito con successo";
                        lblIsManodopInserita.ForeColor = Color.Blue;
                    }
                    else
                    {
                        lblIsManodopInserita.Text = "Errore durante l'inserimento del record. L'intestazione deve essere interamente compilata.";
                        lblIsManodopInserita.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblIsManodopInserita.Text = "La quantità deve essere maggiore di '0'";
                    lblIsManodopInserita.ForeColor = Color.Red;
                }
            }
            else
            {
                lblIsManodopInserita.Text = "Inserire un valore per la data";
                lblIsManodopInserita.ForeColor = Color.Red;
            }

            BindGridManodop();
            SvuotaCampi(pnlManodopera);
        }

        //Visibilità pannello
        protected void btnManodop_Click(object sender, EventArgs e)
        {
            lblTitoloMaschera.Text = "Manodopera";
            txtTipDatCant.Text = "MANODOPERA";
            ShowPanels(false, false, true, false, false, false, false);
            btnInsManodop.Visible = true;
            btnModManodop.Visible = false;
            BindGridManodop();
            EnableDisableControls(true, pnlManodopera);
            SvuotaCampi(pnlManodopera);
            ChooseFornitore("Manodopera");
            HideMessageLabels();

            //Popolo il campo PzzoManodopera a partire dal prezzo scritto nella tabella Cantieri
            Cantieri c = CantieriDAO.GetCantiere(ddlScegliCant.SelectedItem.Value);
            txtPzzoManodop.Text = c.PzzoManodopera.ToString();
        }

        /* EVENTI PER IL ROWCOMMAND */
        protected void btnFiltraGrdManodop_Click(object sender, EventArgs e)
        {
            BindGridManodop();
        }
        protected void grdManodop_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idManodop = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName == "VisualManodop")
                VisualizzaDatiManodop(idManodop);
            else if (e.CommandName == "ModManodop")
                ModificaDatiManodop(idManodop);
            else if (e.CommandName == "ElimManodop")
                EliminaManodop(idManodop);
        }
        private void PopolaCampiManodop(int idManodop, bool enableControls)
        {
            MaterialiCantieri mc = MaterialiCantieriDAO.GetSingleMaterialeCantiere(idManodop);

            //Rendo i textbox abilitati/disabilitati
            EnableDisableControls(enableControls, pnlManodopera);

            ddlScegliAcquirente.SelectedValue = mc.Acquirente;
            ddlScegliFornit.SelectedValue = mc.Fornitore;
            txtTipDatCant.Text = mc.Tipologia;
            txtNumBolla.Text = mc.NumeroBolla.ToString();
            txtDataDDT.Text = mc.Data.ToString("yyyy-MM-dd");
            txtDataDDT.TextMode = TextBoxMode.Date;
            txtFascia.Text = mc.Fascia.ToString();
            txtProtocollo.Text = mc.ProtocolloInterno.ToString();
            txtManodopQta.Text = mc.Qta.ToString();
            txtPzzoManodop.Text = mc.PzzoUniCantiere.ToString();
            chkManodopVisibile.Checked = mc.Visibile;
            chkManodopRicalcolo.Checked = mc.Ricalcolo;
            chkManodopRicaricoSiNo.Checked = mc.RicaricoSiNo;
            txtNote1.Text = mc.Note;
            txtNote2.Text = mc.Note2;

            if (mc.DescriMateriali.ToString() != "NULL")
                txtDescrManodop.Text = mc.DescriMateriali.ToString();
            else
                txtDescrManodop.Text = "";
        }
        private void PopolaObjManodop(MaterialiCantieri mc)
        {
            Operai op = OperaiDAO.GetOperaio(ddlScegliAcquirente.SelectedItem.Value);
            mc.CodArt = "Manodopera" + op.Operaio;
            mc.DescriCodArt = "Manodopera" + op.Operaio;

            mc.IdTblCantieri = Convert.ToInt32(ddlScegliCant.SelectedItem.Value);
            mc.Acquirente = ddlScegliAcquirente.SelectedItem.Value;
            mc.Fornitore = ddlScegliFornit.SelectedItem.Value;
            mc.Tipologia = txtTipDatCant.Text;
            mc.NumeroBolla = txtNumBolla.Text;
            mc.Data = Convert.ToDateTime(txtDataDDT.Text);
            mc.Fascia = Convert.ToInt32(txtFascia.Text);
            mc.ProtocolloInterno = Convert.ToInt32(txtProtocollo.Text);
            mc.DescriMateriali = txtDescrManodop.Text;
            mc.Note = txtNote1.Text;
            mc.Note2 = txtNote2.Text;
            mc.Qta = Convert.ToDouble(txtManodopQta.Text.Replace(".", ","));
            mc.PzzoUniCantiere = Convert.ToDecimal(txtPzzoManodop.Text.Replace(".", ","));
            mc.DescriMateriali = txtDescrManodop.Text;
            mc.Visibile = chkManodopVisibile.Checked;
            mc.Ricalcolo = chkManodopRicalcolo.Checked;
            mc.RicaricoSiNo = chkManodopRicaricoSiNo.Checked;
        }
        private void VisualizzaDatiManodop(int idManodop)
        {
            lblTitoloMaschera.Text = "Visualizza Manodopera";
            btnInsManodop.Visible = btnModManodop.Visible = false;
            PopolaCampiManodop(idManodop, false);
            HideMessageLabels();
        }
        private void ModificaDatiManodop(int idManodop)
        {
            lblTitoloMaschera.Text = "Modifica Manodopera";
            btnInsManodop.Visible = false;
            btnModManodop.Visible = true;
            PopolaCampiManodop(idManodop, true);
            hidManodop.Value = idManodop.ToString();
            HideMessageLabels();
        }
        private void EliminaManodop(int idManodop)
        {
            bool isDeleted = MaterialiCantieriDAO.DeleteMatCant(idManodop);

            if (isDeleted)
            {
                lblIsManodopInserita.Text = "Record eliminato con successo";
                lblIsManodopInserita.ForeColor = Color.Blue;
            }
            else
            {
                lblIsManodopInserita.Text = "Errore durante l'eliminazione del record";
                lblIsManodopInserita.ForeColor = Color.Red;
            }

            BindGridManodop();
        }
        protected void btnModManodop_Click(object sender, EventArgs e)
        {
            if ((Convert.ToDecimal(txtManodopQta.Text) > 0 && txtManodopQta.Text != ""))
            {
                MaterialiCantieri mc = new MaterialiCantieri();
                PopolaObjManodop(mc);
                bool isUpdated = MaterialiCantieriDAO.UpdateMatCant(hidManodop.Value, mc);

                if (isUpdated)
                {
                    lblIsManodopInserita.Text = "Record modificato con successo";
                    lblIsManodopInserita.ForeColor = Color.Blue;
                }
                else
                {
                    lblIsManodopInserita.Text = "Errore durante la modifica del record";
                    lblIsManodopInserita.ForeColor = Color.Red;
                }

                BindGridManodop();
                SvuotaCampi(pnlManodopera);

                btnInsManodop.Visible = true;
                btnModManodop.Visible = false;
                lblTitoloMaschera.Text = "Inserisci Manodopera";
            }
            else
            {
                lblIsManodopInserita.Text = "La Quantità deve essere maggiore di 0";
                lblIsManodopInserita.ForeColor = Color.Red;
            }
        }

        /* EVENTI TEXT-CHANGED */
        protected void txtManodopQta_TextChanged(object sender, EventArgs e)
        {
            HideMessageLabels();
        }

        //Aggiornamento del valore della manodopera per il cantiere corrente
        protected void btnAggiornaValManodop_Click(object sender, EventArgs e)
        {
            if (txtAggiornaValManodop.Text != "" && txtAggiornaValManodop.Text != "0")
            {
                bool isUpdated = MaterialiCantieriDAO.UpdateValoreManodopera(ddlScegliCant.SelectedItem.Value, txtAggiornaValManodop.Text);
                if (isUpdated)
                {
                    lblIsManodopInserita.Text = "Valore della manodopera modificato con successo";
                    lblIsManodopInserita.ForeColor = Color.Blue;
                }
                else
                {
                    lblIsManodopInserita.Text = "Errore durante la modifica del valore della manodopera";
                    lblIsManodopInserita.ForeColor = Color.Red;
                }
            }
            else
            {
                lblIsManodopInserita.Text = "Il campo \"Nuovo Valore Manodopera\" NON può essere nè vuoto nè 0";
                lblIsManodopInserita.ForeColor = Color.Red;
            }

            BindGridManodop();
        }
        #endregion

        #region Operaio
        /* HELPERS */
        protected void FillOperMatCant(MaterialiCantieri mc)
        {
            Operai op = OperaiDAO.GetOperaio(ddlScegliOperaio.SelectedItem.Value);
            mc.CodArt = "Manodopera" + op.Operaio;
            mc.DescriCodArt = "Manodopera" + op.Operaio;

            mc.IdTblCantieri = Convert.ToInt32(ddlScegliCant.SelectedItem.Value);
            mc.Acquirente = ddlScegliAcquirente.SelectedItem.Value;
            mc.Fornitore = ddlScegliFornit.SelectedItem.Value;
            mc.DescriMateriali = txtDescrOper.Text;
            mc.Qta = Convert.ToDouble(txtOperQta.Text);
            mc.Visibile = chkOperVisibile.Checked;
            mc.Ricalcolo = chkOperRicalcolo.Checked;
            mc.RicaricoSiNo = chkOperRicaricoSiNo.Checked;
            mc.Tipologia = txtTipDatCant.Text;
            mc.ProtocolloInterno = Convert.ToInt32(txtProtocollo.Text);
            mc.PzzoUniCantiere = Convert.ToDecimal(txtPzzoOper.Text);
            mc.Data = Convert.ToDateTime(txtDataDDT.Text);
            mc.Note = txtOperNote1.Text;
            mc.Note2 = txtOperNote2.Text;
            mc.NumeroBolla = txtNumBolla.Text;
            mc.Fascia = Convert.ToInt32(txtFascia.Text);
            mc.IdOperaio = Convert.ToInt32(ddlScegliOperaio.SelectedItem.Value);
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
        protected void BindGridOper()
        {
            List<MaterialiCantieri> mcList = MaterialiCantieriDAO.GetMaterialeCantiereForGridView(ddlScegliCant.SelectedItem.Value, txtFiltroOperCodArt.Text,
                txtFiltroOperDescriCodArt.Text, txtFiltroProtocolloGrdMatCant.Text, txtFiltroFornitoreGrdMatCant.Text, "OPERAIO");
            grdOperai.DataSource = mcList;
            grdOperai.DataBind();
        }

        /* EVENTI CLICK */
        protected void btnInsOper_Click(object sender, EventArgs e)
        {
            bool isInserito = false;
            string idCant = ddlScegliCant.SelectedItem.Value;
            string acquirente = ddlScegliAcquirente.SelectedItem.Value;

            if (isDateNotSet())
                return;

            MaterialiCantieri mc = new MaterialiCantieri();
            FillOperMatCant(mc);

            if ((Convert.ToDecimal(txtOperQta.Text) > 0 && txtOperQta.Text != ""))
            {
                if (isIntestazioneCompilata())
                    isInserito = MaterialiCantieriDAO.InserisciOperaio(mc);

                if (isInserito)
                {
                    lblIsOperInserita.Text = "Record inserito con successo";
                    lblIsOperInserita.ForeColor = Color.Blue;
                }
                else
                {
                    lblIsOperInserita.Text = "Errore durante l'inserimento del record. L'intestazione deve essere interamente compilata.";
                    lblIsOperInserita.ForeColor = Color.Red;
                }
            }
            else
            {
                lblIsOperInserita.Text = "Il valore della quantità deve essere maggiore di '0'";
                lblIsOperInserita.ForeColor = Color.Red;
            }

            BindGridOper();
            SvuotaCampi(pnlGestioneOperaio);
        }
        //Visibilità pannello
        protected void btnGestOper_Click(object sender, EventArgs e)
        {
            Operai op = OperaiDAO.GetOperaio(ddlScegliOperaio.SelectedItem.Value);
            txtPzzoOper.Text = op != null ? op.CostoOperaio.ToString("N2") : "";

            lblTitoloMaschera.Text = "Gestione Operaio";
            txtTipDatCant.Text = "OPERAIO";
            ShowPanels(false, false, false, true, false, false, false);
            btnModOper.Visible = false;
            BindGridOper();
            EnableDisableControls(true, pnlGestioneOperaio);
            SvuotaCampi(pnlGestioneOperaio);
            ChooseFornitore("Operaio");
            HideMessageLabels();
        }

        /* EVENTI TEXT-CHANGED */
        protected void ddlScegliOperaio_TextChanged(object sender, EventArgs e)
        {
            Operai op = OperaiDAO.GetOperaio(ddlScegliOperaio.SelectedItem.Value);
            txtPzzoOper.Text = op.CostoOperaio.ToString();
        }

        /* EVENTI PER IL ROWCOMMAND */
        protected void btnOperFiltraGrd_Click(object sender, EventArgs e)
        {
            BindGridOper();
        }
        protected void grdOperai_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idOper = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName == "VisualOper")
                VisualizzaDatiOper(idOper);
            else if (e.CommandName == "ModOper")
                ModificaDatiOper(idOper);
            else if (e.CommandName == "ElimOper")
                EliminaOper(idOper);
        }
        private void PopolaCampiOper(int idOper, bool enableControls)
        {
            MaterialiCantieri mc = MaterialiCantieriDAO.GetSingleMaterialeCantiere(idOper);

            //Rendo i textbox abilitati/disabilitati
            EnableDisableControls(enableControls, pnlGestioneOperaio);

            ddlScegliAcquirente.SelectedValue = mc.Acquirente;
            ddlScegliFornit.SelectedValue = mc.Fornitore;
            ddlScegliOperaio.SelectedValue = mc.IdOperaio.ToString();
            txtTipDatCant.Text = mc.Tipologia;
            txtNumBolla.Text = mc.NumeroBolla.ToString();
            txtDataDDT.Text = mc.Data.ToString("yyyy-MM-dd");
            txtDataDDT.TextMode = TextBoxMode.Date;
            txtFascia.Text = mc.Fascia.ToString();
            txtProtocollo.Text = mc.ProtocolloInterno.ToString();
            txtOperQta.Text = mc.Qta.ToString();
            txtPzzoOper.Text = mc.PzzoUniCantiere.ToString();
            chkOperVisibile.Checked = mc.Visibile;
            chkOperRicalcolo.Checked = mc.Ricalcolo;
            chkOperRicaricoSiNo.Checked = mc.RicaricoSiNo;
            txtNote1.Text = mc.Note;
            txtNote2.Text = mc.Note2;

            if (mc.DescriMateriali.ToString() != "NULL")
                txtDescrOper.Text = mc.DescriMateriali.ToString();
            else
                txtDescrOper.Text = "";
        }
        private void PopolaObjOper(MaterialiCantieri mc)
        {
            Operai op = OperaiDAO.GetOperaio(ddlScegliOperaio.SelectedItem.Value);
            mc.CodArt = "Manodopera" + op.Operaio;
            mc.DescriCodArt = "Manodopera" + op.Operaio;

            mc.IdTblCantieri = Convert.ToInt32(ddlScegliCant.SelectedItem.Value);
            mc.IdOperaio = Convert.ToInt32(ddlScegliOperaio.SelectedItem.Value);
            mc.Acquirente = ddlScegliAcquirente.SelectedItem.Value;
            mc.Fornitore = ddlScegliFornit.SelectedItem.Value;
            mc.Tipologia = txtTipDatCant.Text;
            mc.NumeroBolla = txtNumBolla.Text;
            mc.Data = Convert.ToDateTime(txtDataDDT.Text);
            mc.Fascia = Convert.ToInt32(txtFascia.Text);
            mc.ProtocolloInterno = Convert.ToInt32(txtProtocollo.Text);
            mc.DescriMateriali = txtDescrOper.Text;
            mc.Note = txtNote1.Text;
            mc.Note2 = txtNote2.Text;
            mc.Qta = Convert.ToDouble(txtOperQta.Text);
            mc.PzzoUniCantiere = Convert.ToDecimal(txtPzzoOper.Text);
            mc.DescriMateriali = txtDescrOper.Text;
            mc.Visibile = chkOperVisibile.Checked;
            mc.Ricalcolo = chkOperRicalcolo.Checked;
            mc.RicaricoSiNo = chkOperRicaricoSiNo.Checked;
        }
        private void VisualizzaDatiOper(int idOper)
        {
            lblTitoloMaschera.Text = "Visualizza Operaio";
            btnInsOper.Visible = btnModOper.Visible = false;
            PopolaCampiOper(idOper, false);
            HideMessageLabels();
        }
        private void ModificaDatiOper(int idOper)
        {
            lblTitoloMaschera.Text = "Modifica Operaio";
            btnInsOper.Visible = false;
            btnModOper.Visible = true;
            PopolaCampiOper(idOper, true);
            hidOper.Value = idOper.ToString();
            HideMessageLabels();
        }
        private void EliminaOper(int idOper)
        {
            bool isDeleted = MaterialiCantieriDAO.DeleteMatCant(idOper);

            if (isDeleted)
            {
                lblIsOperInserita.Text = "Record eliminato con successo";
                lblIsOperInserita.ForeColor = Color.Blue;
            }
            else
            {
                lblIsOperInserita.Text = "Errore durante l'eliminazione del record";
                lblIsOperInserita.ForeColor = Color.Red;
            }

            BindGridOper();
        }
        protected void btnModOper_Click(object sender, EventArgs e)
        {
            if ((Convert.ToDecimal(txtOperQta.Text) > 0 && txtOperQta.Text != ""))
            {
                MaterialiCantieri mc = new MaterialiCantieri();
                PopolaObjOper(mc);
                bool isUpdated = MaterialiCantieriDAO.UpdateOperaio(hidOper.Value, mc);

                if (isUpdated)
                {
                    lblIsOperInserita.Text = "Record modificato con successo";
                    lblIsOperInserita.ForeColor = Color.Blue;
                }
                else
                {
                    lblIsOperInserita.Text = "Errore durante la modifica del record";
                    lblIsOperInserita.ForeColor = Color.Red;
                }

                BindGridOper();
                SvuotaCampi(pnlGestioneOperaio);

                btnInsOper.Visible = true;
                btnModOper.Visible = false;
                lblTitoloMaschera.Text = "Inserisci Operaio";
            }
            else
            {
                lblIsOperInserita.Text = "La Quantità deve essere maggiore di 0";
                lblIsOperInserita.ForeColor = Color.Red;
            }
        }

        //Aggiornamento Costo Operaio
        protected void btnNuovoCostoOperaio_Click(object sender, EventArgs e)
        {
            if (ddlScegliOperaio.SelectedIndex != 0)
            {
                if (txtNuovoCostoOperaio.Text != "" && txtNuovoCostoOperaio.Text != "0")
                {
                    bool isUpdated = MaterialiCantieriDAO.UpdateCostoOperaio(ddlScegliCant.SelectedItem.Value, txtNuovoCostoOperaio.Text, ddlScegliOperaio.SelectedItem.Value);
                    if (isUpdated)
                    {
                        lblIsOperInserita.Text = "Costo operaio modificato con successo";
                        lblIsOperInserita.ForeColor = Color.Blue;
                    }
                    else
                    {
                        lblIsOperInserita.Text = "Errore durante la modifica del costo operaio";
                        lblIsOperInserita.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblIsOperInserita.Text = "Il campo \"Nuovo Costo Operaio\" NON può essere nè vuoto nè 0";
                    lblIsOperInserita.ForeColor = Color.Red;
                }
            }
            else
            {
                lblIsOperInserita.Text = "È necessario scegliere un Operaio prima di modificarne il costo";
                lblIsOperInserita.ForeColor = Color.Red;
            }

            BindGridOper();
        }

        /* TEXT CHANGED */
        protected void txtOperQta_TextChanged(object sender, EventArgs e)
        {
            HideMessageLabels();
        }

        #endregion

        #region Arrotondamento
        /* HELPERS */
        protected void FillArrotMatCant(MaterialiCantieri mc)
        {
            mc.IdTblCantieri = Convert.ToInt32(ddlScegliCant.SelectedItem.Value);
            mc.Qta = Convert.ToDouble(txtArrotQta.Text);
            mc.Tipologia = txtTipDatCant.Text;
            mc.CodArt = txtArrotCodArt.Text;
            mc.DescriCodArt = txtArrotDescriCodArt.Text;
            mc.Data = Convert.ToDateTime(txtDataDDT.Text);
            mc.ProtocolloInterno = Convert.ToInt32(txtProtocollo.Text);
            mc.NumeroBolla = txtNumBolla.Text;
            mc.Fascia = Convert.ToInt32(txtFascia.Text);
            mc.Acquirente = ddlScegliAcquirente.SelectedItem.Value;
            mc.Fornitore = ddlScegliFornit.SelectedItem.Value;

            if (txtArrotPzzoUnit.Text != "")
                mc.PzzoUniCantiere = Convert.ToDecimal(txtArrotPzzoUnit.Text.Replace('.', ','));
            else
                mc.PzzoUniCantiere = 0;
        }
        protected void BindGridArrot()
        {
            List<MaterialiCantieri> mcList = MaterialiCantieriDAO.GetMaterialeCantiereForGridView(ddlScegliCant.SelectedItem.Value, txtFiltroArrotCodArt.Text,
                txtFiltroArrotDescriCodArt.Text, txtFiltroProtocolloGrdMatCant.Text, txtFiltroFornitoreGrdMatCant.Text, "ARROTONDAMENTO");
            grdArrot.DataSource = mcList;
            grdArrot.DataBind();
        }

        /* EVENTI CLICK */
        protected void btnInsArrot_Click(object sender, EventArgs e)
        {
            bool isInserito = false;

            if (isDateNotSet())
                return;

            MaterialiCantieri mc = new MaterialiCantieri();
            FillArrotMatCant(mc);

            if ((Convert.ToDecimal(txtArrotQta.Text) > 0 && txtArrotQta.Text != ""))
            {
                if (isIntestazioneCompilata())
                    isInserito = MaterialiCantieriDAO.InserisciMaterialeCantiere(mc);

                if (isInserito)
                {
                    lblIsArrotondInserito.Text = "Record inserito con successo";
                    lblIsArrotondInserito.ForeColor = Color.Blue;
                }
                else
                {
                    lblIsArrotondInserito.Text = "Errore durante l'inserimento del record. L'intestazione deve essere interamente compilata.";
                    lblIsArrotondInserito.ForeColor = Color.Red;
                }
            }
            else
            {
                lblIsArrotondInserito.Text = "Il valore della quantità deve essere maggiore di '0'";
                lblIsArrotondInserito.ForeColor = Color.Red;
            }

            BindGridArrot();
            SvuotaCampi(pnlGestArrotond);
        }
        //Visibilità pannello
        protected void btnGestArrot_Click(object sender, EventArgs e)
        {
            lblTitoloMaschera.Text = "Gestione Arrotondamenti";
            txtTipDatCant.Text = "ARROTONDAMENTO";
            ShowPanels(false, false, false, false, true, false, false);
            btnModArrot.Visible = false;
            BindGridArrot();
            EnableDisableControls(true, pnlGestArrotond);
            SvuotaCampi(pnlGestArrotond);
            ChooseFornitore("Arrotondamento");
            HideMessageLabels();
        }

        /* EVENTI PER IL ROWCOMMAND */
        protected void btnArrotFiltraGrd_Click(object sender, EventArgs e)
        {
            BindGridArrot();
        }
        protected void grdArrot_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idArrot = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName == "VisualArrot")
                VisualizzaDatiArrot(idArrot);
            else if (e.CommandName == "ModArrot")
                ModificaDatiArrot(idArrot);
            else if (e.CommandName == "ElimArrot")
                EliminaArrot(idArrot);
        }
        private void PopolaCampiArrot(int idArrot, bool enableControls)
        {
            MaterialiCantieri mc = MaterialiCantieriDAO.GetSingleMaterialeCantiere(idArrot);

            //Rendo i textbox abilitati/disabilitati
            EnableDisableControls(enableControls, pnlGestArrotond);

            ddlScegliAcquirente.SelectedValue = mc.Acquirente;
            ddlScegliFornit.SelectedValue = mc.Fornitore;
            txtTipDatCant.Text = mc.Tipologia;
            txtNumBolla.Text = mc.NumeroBolla.ToString();
            txtDataDDT.Text = mc.Data.ToString("yyyy-MM-dd");
            txtDataDDT.TextMode = TextBoxMode.Date;
            txtFascia.Text = mc.Fascia.ToString();
            txtArrotCodArt.Text = mc.CodArt;
            txtArrotDescriCodArt.Text = mc.DescriCodArt;
            txtProtocollo.Text = mc.ProtocolloInterno.ToString();
            txtArrotQta.Text = mc.Qta.ToString();
            txtArrotPzzoUnit.Text = mc.PzzoUniCantiere.ToString();
            chkVisibile.Checked = mc.Visibile;
            chkRicalcolo.Checked = mc.Ricalcolo;
            chkRicarico.Checked = mc.RicaricoSiNo;
            txtNote1.Text = mc.Note;
            txtNote2.Text = mc.Note2;
        }
        private void PopolaObjArrot(MaterialiCantieri mc)
        {
            mc.IdTblCantieri = Convert.ToInt32(ddlScegliCant.SelectedItem.Value);
            mc.Acquirente = ddlScegliAcquirente.SelectedItem.Value;
            mc.Fornitore = ddlScegliFornit.SelectedItem.Value;
            mc.Tipologia = txtTipDatCant.Text;
            mc.NumeroBolla = txtNumBolla.Text;
            mc.Data = Convert.ToDateTime(txtDataDDT.Text);
            mc.Fascia = Convert.ToInt32(txtFascia.Text);
            mc.ProtocolloInterno = Convert.ToInt32(txtProtocollo.Text);
            mc.Note = txtNote1.Text;
            mc.Note2 = txtNote2.Text;
            mc.Qta = Convert.ToDouble(txtArrotQta.Text);
            mc.PzzoUniCantiere = Convert.ToDecimal(txtArrotPzzoUnit.Text.Replace('.', ','));
            mc.CodArt = txtArrotCodArt.Text;
            mc.DescriCodArt = txtArrotDescriCodArt.Text;
            mc.Visibile = chkVisibile.Checked;
            mc.Ricalcolo = chkRicalcolo.Checked;
            mc.RicaricoSiNo = chkRicarico.Checked;
        }
        private void VisualizzaDatiArrot(int idArrot)
        {
            lblTitoloMaschera.Text = "Visualizza Arrotondamento";
            btnInsArrot.Visible = btnModArrot.Visible = false;
            PopolaCampiArrot(idArrot, false);
            btnInsArrot.Visible = btnModArrot.Visible = false;
            HideMessageLabels();
        }
        private void ModificaDatiArrot(int idArrot)
        {
            lblTitoloMaschera.Text = "Modifica Arrotondamento";
            btnInsArrot.Visible = false;
            btnModArrot.Visible = true;
            PopolaCampiArrot(idArrot, true);
            hidArrot.Value = idArrot.ToString();
            HideMessageLabels();
        }
        private void EliminaArrot(int idArrot)
        {
            bool isDeleted = MaterialiCantieriDAO.DeleteMatCant(idArrot);

            if (isDeleted)
            {
                lblIsArrotondInserito.Text = "Record eliminato con successo";
                lblIsArrotondInserito.ForeColor = Color.Blue;
            }
            else
            {
                lblIsArrotondInserito.Text = "Errore durante l'eliminazione del record";
                lblIsArrotondInserito.ForeColor = Color.Red;
            }

            BindGridArrot();
        }
        protected void btnModArrot_Click(object sender, EventArgs e)
        {
            if ((Convert.ToDecimal(txtArrotQta.Text) > 0 && txtArrotQta.Text != ""))
            {
                MaterialiCantieri mc = new MaterialiCantieri();
                PopolaObjArrot(mc);
                bool isUpdated = MaterialiCantieriDAO.UpdateMatCant(hidArrot.Value, mc);

                if (isUpdated)
                {
                    lblIsArrotondInserito.Text = "Record modificato con successo";
                    lblIsArrotondInserito.ForeColor = Color.Blue;
                }
                else
                {
                    lblIsArrotondInserito.Text = "Errore durante la modifica del record";
                    lblIsArrotondInserito.ForeColor = Color.Red;
                }

                BindGridArrot();
                SvuotaCampi(pnlGestArrotond);

                btnModArrot.Visible = false;
                btnInsArrot.Visible = true;
                lblTitoloMaschera.Text = "Inserisci Arrotondamento";
            }
            else
            {
                lblIsArrotondInserito.Text = "La Quantità deve essere maggiore di 0";
                lblIsArrotondInserito.ForeColor = Color.Red;
            }
        }
        #endregion

        #region A Chiamata
        /* HELPERS */
        protected void BindGridChiamata()
        {
            List<MaterialiCantieri> mcList = MaterialiCantieriDAO.GetMaterialeCantiereForGridView(ddlScegliCant.SelectedItem.Value, txtFiltroAChiamCodArt.Text,
                txtFiltroAChiamDescriCodArt.Text, txtFiltroProtocolloGrdMatCant.Text, txtFiltroFornitoreGrdMatCant.Text, "A CHIAMATA");
            grdAChiam.DataSource = mcList;
            grdAChiam.DataBind();
        }
        protected void FillMatCantChiamata(MaterialiCantieri mc)
        {
            mc.IdTblCantieri = Convert.ToInt32(ddlScegliCant.SelectedItem.Value);
            mc.DescriMateriali = txtChiamDescrMate.Text;
            mc.Visibile = chkChiamVisibile.Checked;
            mc.Ricalcolo = chkChiamRicalcolo.Checked;
            mc.RicaricoSiNo = chkChiamRicaricoSiNo.Checked;
            mc.Data = Convert.ToDateTime(txtDataDDT.Text);
            mc.PzzoUniCantiere = Convert.ToDecimal(txtChiamPzzoUnit.Text);
            mc.CodArt = txtChiamCodArt.Text;
            mc.DescriCodArt = txtChiamDescriCodArt.Text;
            mc.Tipologia = txtTipDatCant.Text;
            mc.Acquirente = ddlScegliAcquirente.SelectedItem.Value;
            mc.Fornitore = ddlScegliFornit.SelectedItem.Value;
            mc.Note = txtChiamNote.Text;
            mc.Note2 = txtChiamNote2.Text;
            mc.Qta = Convert.ToDouble(txtChiamQta.Text);

            if (txtFascia.Text != "")
                mc.Fascia = Convert.ToInt32(txtFascia.Text);
            else
                mc.Fascia = 0;

            if (txtProtocollo.Text != "")
                mc.ProtocolloInterno = Convert.ToInt32(txtProtocollo.Text);
            else
                mc.ProtocolloInterno = 0;

            if (txtNumBolla.Enabled && txtNumBolla.Text != "")
                mc.NumeroBolla = txtNumBolla.Text;
            else if (ddlScegliDDTMef.SelectedIndex != -1)
                mc.NumeroBolla = (ddlScegliDDTMef.SelectedItem.Text).Split('-')[3];
            else
                mc.NumeroBolla = "";

            if (txtChiamPzzoFinCli.Text != "")
                mc.PzzoFinCli = Convert.ToDecimal(txtChiamPzzoFinCli.Text);
            else
                mc.PzzoFinCli = 0.0m;
        }
        private void PopolaCampiChiamata(int id, bool enableControls)
        {
            MaterialiCantieri mc = MaterialiCantieriDAO.GetSingleMaterialeCantiere(id);

            //Rendo i textbox abilitati/disabilitati
            EnableDisableControls(enableControls, pnlGestChiamata);

            ddlScegliAcquirente.SelectedValue = mc.Acquirente;
            ddlScegliFornit.SelectedValue = mc.Fornitore;
            txtTipDatCant.Text = mc.Tipologia;
            txtNumBolla.Text = mc.NumeroBolla.ToString();
            txtDataDDT.Text = mc.Data.ToString("yyyy-MM-dd");
            txtDataDDT.TextMode = TextBoxMode.Date;
            txtFascia.Text = mc.Fascia.ToString();
            txtProtocollo.Text = mc.ProtocolloInterno.ToString();
            txtChiamCodArt.Text = mc.CodArt;
            txtChiamDescriCodArt.Text = mc.DescriCodArt;
            txtChiamDescrMate.Text = mc.DescriMateriali;
            txtChiamNote.Text = mc.Note;
            txtChiamNote.Text = mc.Note2;
            txtChiamQta.Text = mc.Qta.ToString();
            txtChiamPzzoUnit.Text = mc.PzzoUniCantiere.ToString();
            txtChiamPzzoFinCli.Text = mc.PzzoFinCli.ToString();
            chkChiamVisibile.Checked = mc.Visibile;
            chkChiamRicalcolo.Checked = mc.Ricalcolo;
            chkChiamRicaricoSiNo.Checked = mc.RicaricoSiNo;
        }
        private void PopolaObjChiamata(MaterialiCantieri mc)
        {
            mc.IdTblCantieri = Convert.ToInt32(ddlScegliCant.SelectedItem.Value);
            mc.Acquirente = ddlScegliAcquirente.SelectedItem.Value;
            mc.Fornitore = ddlScegliFornit.SelectedItem.Value;
            mc.Tipologia = txtTipDatCant.Text;
            mc.NumeroBolla = txtNumBolla.Text;
            mc.Data = Convert.ToDateTime(txtDataDDT.Text);
            mc.Fascia = Convert.ToInt32(txtFascia.Text);
            mc.ProtocolloInterno = Convert.ToInt32(txtProtocollo.Text);
            mc.CodArt = txtChiamCodArt.Text;
            mc.DescriCodArt = txtChiamDescriCodArt.Text;
            mc.DescriMateriali = txtChiamDescrMate.Text;
            mc.Note = txtChiamNote.Text;
            mc.Note2 = txtChiamNote2.Text;
            mc.Qta = Convert.ToDouble(txtChiamQta.Text);
            mc.PzzoUniCantiere = Convert.ToDecimal(txtChiamPzzoUnit.Text);
            mc.PzzoFinCli = Convert.ToDecimal(txtChiamPzzoFinCli.Text);
            mc.Visibile = chkChiamVisibile.Checked;
            mc.Ricalcolo = chkChiamRicalcolo.Checked;
            mc.RicaricoSiNo = chkChiamRicaricoSiNo.Checked;
        }

        /* EVENTI CLICK */
        protected void btnCalcolaPzzoUnitAChiam_Click(object sender, EventArgs e)
        {
            if (txtChiamPzzoNetto.Text != "")
                txtChiamPzzoUnit.Text = Math.Round(Convert.ToDecimal(txtChiamPzzoNetto.Text.Replace(".", ",")), 2).ToString();
            else
            {
                lblIsAChiamInserita.Text = "Inserire un valore nella casella 'Prezzo Netto Mef' per calcolare il 'Prezzo Unitario'";
                lblIsAChiamInserita.ForeColor = Color.Red;
            }
        }
        protected void btnInsAChiam_Click(object sender, EventArgs e)
        {
            bool isInserito = false;

            string idCant = ddlScegliCant.SelectedItem.Value;
            string acquirente = ddlScegliAcquirente.SelectedItem.Value;
            string fornitore = ddlScegliFornit.SelectedItem.Value;
            string numeroBolla = "";

            if (isDateNotSet())
                return;

            MaterialiCantieri mc = new MaterialiCantieri();
            FillMatCantChiamata(mc);

            if ((Convert.ToDecimal(txtChiamQta.Text) > 0 && txtChiamQta.Text != "") && Convert.ToDecimal(txtChiamPzzoUnit.Text) > 0)
            {
                if (ddlScegliDDTMef.SelectedItem == null || ddlScegliDDTMef.SelectedItem.Text == "")
                {
                    if (txtNumBolla.Text != "")
                        numeroBolla = txtNumBolla.Text;
                    else
                    {
                        lblIsAChiamInserita.Text = "Scegliere un DDT dal menù a discesa o compilare il campo \"Numero Bolla\"";
                        lblIsAChiamInserita.ForeColor = Color.Red;
                        return;
                    }
                }
                else
                {
                    numeroBolla = (ddlScegliDDTMef.SelectedItem.Text).Split('-')[3];
                }

                if (isIntestazioneCompilata())
                {
                    if (txtChiamCodArt.Text != "" && txtChiamDescriCodArt.Text != "")
                    {
                        isInserito = MaterialiCantieriDAO.InserisciMaterialeCantiere(mc);
                    }
                    else
                    {
                        lblIsAChiamInserita.Text = "Codice Articolo e Descrizione Codice Articolo obbligatori!";
                        lblIsAChiamInserita.ForeColor = Color.Red;
                        return;
                    }
                }

                if (isInserito)
                {
                    lblIsAChiamInserita.Text = "Record inserito con successo";
                    lblIsAChiamInserita.ForeColor = Color.Blue;
                }
                else
                {
                    lblIsAChiamInserita.Text = "Errore durante l'inserimento del record. L'intestazione deve essere interamente compilata.";
                    lblIsAChiamInserita.ForeColor = Color.Red;
                }
            }
            else
            {
                lblIsAChiamInserita.Text = "Quantità e/o Prezzo Unitario devono essere maggiori di 0";
                lblIsAChiamInserita.ForeColor = Color.Red;
            }

            BindGridChiamata();
            SvuotaCampi(pnlGestChiamata);
        }
        protected void btnGestChiam_Click(object sender, EventArgs e)
        {
            lblTitoloMaschera.Text = "Inserisci A Chiamata";
            txtTipDatCant.Text = "A CHIAMATA";
            ShowPanels(false, false, false, false, false, false, true);
            btnInsAChiam.Visible = true;
            btnModAChiam.Visible = false;
            BindGridChiamata();
            EnableDisableControls(true, pnlGestChiamata);
            SvuotaCampi(pnlGestChiamata);
            ChooseFornitore("A Chiamata");
            HideMessageLabels();
        }

        /* Eventi Per il RowCommand */
        protected void grdAChiam_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idChiamata = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName == "VisualChiam")
                VisualizzaDatiChiam(idChiamata);
            else if (e.CommandName == "ModChiam")
                ModificaDatiChiam(idChiamata);
            else if (e.CommandName == "ElimChiam")
                EliminaChiam(idChiamata);
        }
        private void VisualizzaDatiChiam(int idChiamata)
        {
            lblTitoloMaschera.Text = "Visualizza A Chiamata";
            btnInsAChiam.Visible = btnModAChiam.Visible = false;
            PopolaCampiChiamata(idChiamata, false);
            HideMessageLabels();
        }
        private void ModificaDatiChiam(int idChiamata)
        {
            lblTitoloMaschera.Text = "Modifica A Chiamata";
            btnInsAChiam.Visible = false;
            btnModAChiam.Visible = true;
            PopolaCampiChiamata(idChiamata, true);
            hidAChiamata.Value = idChiamata.ToString();
            HideMessageLabels();
        }
        private void EliminaChiam(int idChiamata)
        {
            bool isDeleted = MaterialiCantieriDAO.DeleteMatCant(idChiamata);

            if (isDeleted)
            {
                lblIsAChiamInserita.Text = "Record eliminato con successo";
                lblIsAChiamInserita.ForeColor = Color.Blue;
            }
            else
            {
                lblIsAChiamInserita.Text = "Errore durante l'eliminazione del record";
                lblIsAChiamInserita.ForeColor = Color.Red;
            }

            BindGridChiamata();
        }
        protected void btnFiltraGrdAChiam_Click(object sender, EventArgs e)
        {
            BindGridChiamata();
        }
        protected void btnModAChiam_Click(object sender, EventArgs e)
        {
            if ((Convert.ToDecimal(txtChiamQta.Text) > 0 && txtChiamQta.Text != "") && Convert.ToDecimal(txtChiamPzzoUnit.Text) > 0)
            {
                MaterialiCantieri mc = new MaterialiCantieri();
                PopolaObjChiamata(mc);
                bool isUpdated = MaterialiCantieriDAO.UpdateMatCant(hidAChiamata.Value, mc);

                if (isUpdated)
                {
                    lblIsAChiamInserita.Text = "Record modificato con successo";
                    lblIsAChiamInserita.ForeColor = Color.Blue;
                }
                else
                {
                    lblIsAChiamInserita.Text = "Errore durante la modifica del record";
                    lblIsAChiamInserita.ForeColor = Color.Red;
                }

                BindGridChiamata();
                SvuotaCampi(pnlGestChiamata);

                btnInsAChiam.Visible = true;
                btnModAChiam.Visible = false;
                lblTitoloMaschera.Text = "Inserisci A Chiamata";

            }
            else
            {
                lblIsAChiamInserita.Text = "Quantità e/o Prezzo Unitario devono essere maggiori di 0";
                lblIsAChiamInserita.ForeColor = Color.Red;
            }
        }

        /* TEXT-CHANGED */
        protected void txtChiamCodArt_TextChanged(object sender, EventArgs e)
        {
            HideMessageLabels();
        }
        protected void txtChiamDescriCodArt_TextChanged(object sender, EventArgs e)
        {
            HideMessageLabels();
        }
        #endregion

        #region Spese
        /* HELPERS */
        protected void BindGridSpese()
        {
            List<MaterialiCantieri> mcList = MaterialiCantieriDAO.GetMaterialeCantiereForGridView(ddlScegliCant.SelectedItem.Value, txtFiltroAChiamCodArt.Text,
                txtFiltroAChiamDescriCodArt.Text, txtFiltroProtocolloGrdMatCant.Text, txtFiltroFornitoreGrdMatCant.Text, "SPESE");
            grdSpese.DataSource = mcList;
            grdSpese.DataBind();
        }
        protected void FillDdlScegliSpesa()
        {
            DataTable dt = SpeseDAO.GetSpese();
            List<Spese> listSpese = dt.DataTableToList<Spese>();

            ddlScegliSpesa.Items.Clear();
            ddlScegliSpesa.Items.Add(new ListItem("", "-1"));

            foreach (Spese s in listSpese)
            {
                string show = s.Descrizione;
                ddlScegliSpesa.Items.Add(new ListItem(show, s.IdSpesa.ToString()));
            }
        }
        protected void FillMatCantSpese(MaterialiCantieri mc)
        {
            mc.IdTblCantieri = Convert.ToInt32(ddlScegliCant.SelectedItem.Value);
            mc.DescriMateriali = txtSpeseDescriCodArt.Text;
            mc.Visibile = chkSpesaVisibile.Checked;
            mc.Ricalcolo = chkSpesaRicalcolo.Checked;
            mc.RicaricoSiNo = chkSpesaRicarico.Checked;
            mc.Data = Convert.ToDateTime(txtDataDDT.Text);
            mc.PzzoUniCantiere = Convert.ToDecimal(txtSpesaPrezzoCalcolato.Text);
            mc.CodArt = txtSpeseCodArt.Text;
            mc.DescriCodArt = txtSpeseDescriCodArt.Text;
            mc.Tipologia = txtTipDatCant.Text;
            mc.Acquirente = ddlScegliAcquirente.SelectedItem.Value;
            mc.Fornitore = ddlScegliFornit.SelectedItem.Value;

            if (txtSpeseQta.Text != "")
                mc.Qta = Convert.ToDouble(txtSpeseQta.Text);
            else
            {
                mc.Qta = 0;
                txtSpeseQta.Text = "0";
            }

            if (txtFascia.Text != "")
                mc.Fascia = Convert.ToInt32(txtFascia.Text);
            else
                mc.Fascia = 0;

            if (txtProtocollo.Text != "")
                mc.ProtocolloInterno = Convert.ToInt32(txtProtocollo.Text);
            else
                mc.ProtocolloInterno = 0;

            if (txtNumBolla.Enabled && txtNumBolla.Text != "")
                mc.NumeroBolla = txtNumBolla.Text;
            else if (ddlScegliDDTMef.SelectedIndex != -1)
                mc.NumeroBolla = (ddlScegliDDTMef.SelectedItem.Text).Split('-')[3];
            else
                mc.NumeroBolla = "";
        }
        private void PopolaCampiSpese(int id, bool enableControls)
        {
            MaterialiCantieri mc = MaterialiCantieriDAO.GetSingleMaterialeCantiere(id);

            //Rendo i textbox abilitati/disabilitati
            EnableDisableControls(enableControls, pnlGestSpese);

            ddlScegliAcquirente.SelectedValue = mc.Acquirente;
            ddlScegliFornit.SelectedValue = mc.Fornitore;
            txtTipDatCant.Text = mc.Tipologia;
            txtNumBolla.Text = mc.NumeroBolla.ToString();
            txtDataDDT.Text = mc.Data.ToString("yyyy-MM-dd");
            txtDataDDT.TextMode = TextBoxMode.Date;
            txtFascia.Text = mc.Fascia.ToString();
            txtProtocollo.Text = mc.ProtocolloInterno.ToString();
            txtSpeseCodArt.Text = mc.CodArt;
            txtSpeseDescriCodArt.Text = mc.DescriCodArt;
            txtSpeseQta.Text = mc.Qta.ToString();
            txtSpesaPrezzoCalcolato.Text = mc.PzzoUniCantiere.ToString();
            chkSpesaVisibile.Checked = mc.Visibile;
            chkSpesaRicalcolo.Checked = mc.Ricalcolo;
            chkSpesaRicarico.Checked = mc.RicaricoSiNo;
        }
        private void PopolaObjSpesa(MaterialiCantieri mc)
        {
            mc.IdTblCantieri = Convert.ToInt32(ddlScegliCant.SelectedItem.Value);
            mc.Acquirente = ddlScegliAcquirente.SelectedItem.Value;
            mc.Fornitore = ddlScegliFornit.SelectedItem.Value;
            mc.Tipologia = txtTipDatCant.Text;
            mc.NumeroBolla = txtNumBolla.Text;
            mc.Data = Convert.ToDateTime(txtDataDDT.Text);
            mc.Fascia = Convert.ToInt32(txtFascia.Text);
            mc.ProtocolloInterno = Convert.ToInt32(txtProtocollo.Text);
            mc.CodArt = txtSpeseCodArt.Text;
            mc.DescriCodArt = txtSpeseDescriCodArt.Text;
            mc.Qta = Convert.ToDouble(txtSpeseQta.Text);
            mc.PzzoUniCantiere = Convert.ToDecimal(txtSpesaPrezzoCalcolato.Text);
            mc.Visibile = chkSpesaVisibile.Checked;
            mc.Ricalcolo = chkSpesaRicalcolo.Checked;
            mc.RicaricoSiNo = chkSpesaRicarico.Checked;
        }

        /* EVENTI CLICK */
        protected void btnGestSpese_Click(object sender, EventArgs e)
        {
            lblTitoloMaschera.Text = "Inserisci Spese";
            txtTipDatCant.Text = "SPESE";
            ShowPanels(false, false, false, false, false, true, false);
            btnInsSpesa.Visible = true;
            btnModSpesa.Visible = false;
            BindGridSpese();
            EnableDisableControls(true, pnlGestSpese);
            SvuotaCampi(pnlGestSpese);
            ChooseFornitore("Spese");
            HideMessageLabels();
        }
        protected void btnFiltraGrdSpese_Click(object sender, EventArgs e)
        {
            BindGridSpese();
        }
        protected void btnCalcolaPzzoUnitSpese_Click(object sender, EventArgs e)
        {
            if (txtSpesaPrezzoCalcolato.Text != "")
                txtSpesaPrezzoCalcolato.Text = Math.Round(Convert.ToDecimal(txtSpesaPrezzo.Text.Replace(".", ",")), 2).ToString();
            else
            {
                lblIsSpesaInserita.Text = "Inserire un valore nella casella 'Prezzo' per calcolare il 'Prezzo Calcolato'";
                lblIsSpesaInserita.ForeColor = Color.Red;
            }

            btnInsAChiam.Focus();
        }
        protected void btnInsSpesa_Click(object sender, EventArgs e)
        {
            bool isInserito = false;

            if (isDateNotSet())
                return;

            MaterialiCantieri mc = new MaterialiCantieri();
            FillMatCantSpese(mc);

            if ((Convert.ToDecimal(txtSpeseQta.Text) > 0 && txtSpeseQta.Text != "") && Convert.ToDecimal(txtSpesaPrezzoCalcolato.Text) > 0)
            {
                if (isIntestazioneCompilata())
                    isInserito = MaterialiCantieriDAO.InserisciMaterialeCantiere(mc);

                if (isInserito)
                {
                    lblIsSpesaInserita.Text = "Record inserito con successo";
                    lblIsSpesaInserita.ForeColor = Color.Blue;
                }
                else
                {
                    lblIsSpesaInserita.Text = "Errore durante l'inserimento del record. L'intestazione deve essere interamente compilata.";
                    lblIsSpesaInserita.ForeColor = Color.Red;
                    return;
                }
            }
            else
            {
                lblIsSpesaInserita.Text = "Il valore della quantità e/o del prezzo devono essere maggiori di '0'";
                lblIsSpesaInserita.ForeColor = Color.Red;
                return;
            }

            BindGridSpese();
            SvuotaCampi(pnlGestSpese);
            ddlScegliSpesa.SelectedIndex = 0;
        }

        /* EVENTI TEXT-CHANGED */
        protected void ddlScegliSpesa_TextChanged(object sender, EventArgs e)
        {
            Spese spesa = new Spese();
            spesa = SpeseDAO.GetDettagliSpesa(ddlScegliSpesa.SelectedItem.Value);
            txtSpeseCodArt.Text = txtSpeseDescriCodArt.Text = spesa.Descrizione;
            txtSpesaPrezzo.Text = spesa.Prezzo.ToString("N2");
            HideMessageLabels();
        }
        protected void txtSpeseCodArt_TextChanged(object sender, EventArgs e)
        {
            HideMessageLabels();
        }
        protected void txtSpeseDescriCodArt_TextChanged(object sender, EventArgs e)
        {
            HideMessageLabels();
        }

        /* EVENTI ROW-COMMAND */
        protected void grdSpese_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idSpesa = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName == "VisualSpesa")
                VisualizzaDatiSpesa(idSpesa);
            else if (e.CommandName == "ModSpesa")
                ModificaDatiSpesa(idSpesa);
            else if (e.CommandName == "ElimSpesa")
                EliminaSpesa(idSpesa);
        }
        private void VisualizzaDatiSpesa(int idSpesa)
        {
            lblTitoloMaschera.Text = "Visualizza Spese";
            btnInsSpesa.Visible = btnModSpesa.Visible = false;
            PopolaCampiSpese(idSpesa, false);
            HideMessageLabels();
        }
        private void ModificaDatiSpesa(int idSpesa)
        {
            lblTitoloMaschera.Text = "Modifica Spese";
            btnInsSpesa.Visible = false;
            btnModSpesa.Visible = true;
            PopolaCampiSpese(idSpesa, true);
            hidIdSpesa.Value = idSpesa.ToString();
            HideMessageLabels();
        }
        private void EliminaSpesa(int idSpesa)
        {
            bool isDeleted = MaterialiCantieriDAO.DeleteMatCant(idSpesa);

            if (isDeleted)
            {
                lblIsSpesaInserita.Text = "Record eliminato con successo";
                lblIsSpesaInserita.ForeColor = Color.Blue;
            }
            else
            {
                lblIsSpesaInserita.Text = "Errore durante l'eliminazione del record";
                lblIsSpesaInserita.ForeColor = Color.Red;
            }

            BindGridSpese();
        }
        protected void btnModSpesa_Click(object sender, EventArgs e)
        {
            if ((Convert.ToDecimal(txtSpeseQta.Text) > 0 && txtSpeseQta.Text != ""))
            {
                MaterialiCantieri mc = new MaterialiCantieri();
                PopolaObjSpesa(mc);
                bool isUpdated = MaterialiCantieriDAO.UpdateMatCant(hidIdSpesa.Value, mc);

                if (isUpdated)
                {
                    lblIsSpesaInserita.Text = "Record modificato con successo";
                    lblIsSpesaInserita.ForeColor = Color.Blue;
                }
                else
                {
                    lblIsSpesaInserita.Text = "Errore durante la modifica del record";
                    lblIsSpesaInserita.ForeColor = Color.Red;
                }

                BindGridSpese();
                SvuotaCampi(pnlGestSpese);

                btnInsSpesa.Visible = true;
                btnModSpesa.Visible = false;
                lblTitoloMaschera.Text = "Inserisci Spese";
            }
            else
            {
                lblIsSpesaInserita.Text = "La Quantità deve essere maggiore di 0";
                lblIsSpesaInserita.ForeColor = Color.Red;
            }
        }
        #endregion
    }
}