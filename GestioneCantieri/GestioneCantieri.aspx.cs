using GestioneCantieri.DAO;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
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
                ShowPanels(false, false, false, false, false);
                pnlSubIntestazione.Visible = pnlMascheraGestCant.Visible = false;
            }
        }

        /* HELPERS */
        //Fill Ddl
        protected void FillDdlScegliCant()
        {
            DataTable dt = CantieriDAO.GetCantieri(txtFiltroCantAnno.Text, txtFiltroCantCodCant.Text, txtFiltroCantDescrCodCant.Text, chkFiltroCantChiuso.Checked, chkFiltroCantRiscosso.Checked);
            List<Cantieri> listCantieri = dt.DataTableToList<Cantieri>();

            ddlScegliCant.Items.Clear();
            ddlScegliCant.Items.Add(new ListItem("", "-1"));

            foreach (Cantieri c in listCantieri)
            {
                string show = c.Anno + " - " + c.CodCant + " - " + c.DescriCodCAnt;
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
            DataTable dt = FornitoriDAO.GetFornitori();
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
                string show = ddt.Anno + " - " + ddt.CodArt + " - " + ddt.DescriCodArt + " - " + ddt.N_ddt;
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
                string show = mmg.CodArt + " | " + mmg.Desc + " | " + mmg.PrezzoNetto + " | " + mmg.PrezzoListino + " | " + mmg.Sconto1 + " | " + mmg.Sconto2 + " | " + mmg.Sconto3;
                ddlScegliListino.Items.Add(new ListItem(show, mmg.CodArt.ToString()));
            }
        }
        protected void FillDdlScegliMatCant()
        {
            List<MaterialiCantieri> listMatCant = MaterialiCantieriDAO.GetMaterialeCantiere(ddlScegliCant.SelectedItem.Value);

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
        }

        //Mostra/Nasconde pannelli
        protected void ShowPanels(bool pnlMatCant, bool pnlManodop, bool pnlOper, bool pnlArrotond, bool pnlPagam)
        {
            pnlMascheraGestCant.Visible = pnlMatCant;
            pnlManodopera.Visible = pnlManodop;
            pnlGestioneOperaio.Visible = pnlOper;
            pnlGestArrotond.Visible = pnlArrotond;
            pnlGestPagam.Visible = pnlPagam;
        }

        //Switcha classi css per bottoni di scelta pannello
        protected void RemoveActiveClass()
        {
            foreach (var b in pnlScegliMaschera.Controls)
            {
                if (b.GetType().Name == "Button")
                {
                    (((Button)b).CssClass).Replace(" active", "");
                }
            }
        }

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
                lblIsRecordInserito.Text = lblIsManodopInserita.Text = lblIsOperInserita.Text = lblIsManodopInserita.Text = lblIsPagamInserito.Text = "Inserire un valore per la data";
                lblIsRecordInserito.ForeColor = lblIsManodopInserita.ForeColor = lblIsOperInserita.ForeColor = lblIsManodopInserita.ForeColor = lblIsPagamInserito.ForeColor = Color.Red;
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

        /* EVENTI TEXT-CHANGED */
        protected void ddlScegliCant_TextChanged(object sender, EventArgs e)
        {
            Cantieri cant = CantieriDAO.GetCantiere(ddlScegliCant.SelectedItem.Value);
            txtPzzoManodop.Text = cant.PzzoManodopera.ToString();

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
            }
            else
            {
                txtNumBolla.Enabled = true;
            }
        }
        protected void btnCalcolaPrezzoUnit_Click(object sender, EventArgs e)
        {
            if (txtPzzoNettoMef.Text != "")
                txtPzzoUnit.Text = Math.Round(Convert.ToDecimal(txtPzzoNettoMef.Text.Replace(".",",")), 2).ToString();
            else
            {
                lblIsRecordInserito.Text = "Inserire un valore nella casella 'Prezzo Netto Mef' per calcolare il 'Prezzo Unitario'";
                lblIsRecordInserito.ForeColor = Color.Red;
            }
        }

        #region Materiali Cantieri e Rientro
        int maxQtaRientro = -1;

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

            if (mc.Tipologia == "MATERIALE")
                mc.Qta = Convert.ToDouble(txtQta.Text);
            else if (mc.Tipologia == "MATERIALE")
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
                mc.NumeroBolla = Convert.ToInt32(txtNumBolla.Text);
            else if (ddlScegliDDTMef.SelectedIndex != -1)
                mc.NumeroBolla = Convert.ToInt32((ddlScegliDDTMef.SelectedItem.Text).Split('-')[3]);
            else
                mc.NumeroBolla = 0;

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

            if (Convert.ToInt32(txtQta.Text) > 0 && Convert.ToDecimal(txtPzzoUnit.Text) > 0)
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

            if (Convert.ToInt32(txtQta.Text) > 0 && Convert.ToDecimal(txtPzzoUnit.Text) > 0)
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
        }
        //Visibilità pannelli
        protected void btnMatCant_Click(object sender, EventArgs e)
        {
            lblTitoloMaschera.Text = "Materiali Cantieri";
            txtTipDatCant.Text = "MATERIALE";
            ShowForMatCant();
            ShowPanels(true, false, false, false, false);
        }
        protected void btnRientro_Click(object sender, EventArgs e)
        {
            lblTitoloMaschera.Text = "Rientro Materiali";
            txtTipDatCant.Text = "RIENTRO";
            FillDdlScegliMatCant();
            ShowForRientro();
            ShowPanels(true, false, false, false, false);
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
        }
        protected void ddlScegliMatCant_TextChanged(object sender, EventArgs e)
        {
            if (ddlScegliListino.SelectedIndex != 0)
            {
                string[] partiMatCant = ddlScegliMatCant.SelectedItem.Text.Split('|');
                txtCodArt.Text = partiMatCant[0].Trim();
                txtDescriCodArt.Text = partiMatCant[1].Trim();
                txtQta.Text = partiMatCant[2].Trim();
                maxQtaRientro = Convert.ToInt32(partiMatCant[2].Trim());
                txtPzzoNettoMef.Text = partiMatCant[3].Trim();
                txtPzzoUnit.Text = "0.00";
                txtPzzoFinCli.Text = partiMatCant[4].Trim();
            }
            else
            {
                txtCodArt.Text = txtDescriCodArt.Text = txtPzzoNettoMef.Text = txtPzzoFinCli.Text = "";
                txtPzzoUnit.Text = "0.00";
            }
        }
        #endregion

        #region Manodopera
        /* HELPERS */
        protected void FillManodopMatCant(MaterialiCantieri mc)
        {
            Operai op = OperaiDAO.GetOperaio(ddlScegliAcquirente.SelectedItem.Value);
            mc.CodArt = op.Operaio;
            mc.DescriCodArt = op.DescrOp;

            mc.IdTblCantieri = Convert.ToInt32(ddlScegliCant.SelectedItem.Value);
            mc.Acquirente = ddlScegliAcquirente.SelectedItem.Value;
            mc.Qta = Convert.ToDouble(txtManodopQta.Text);
            mc.Tipologia = txtTipDatCant.Text;
            mc.DescriMateriali = txtDescrManodop.Text;
            mc.Data = Convert.ToDateTime(txtDataDDT.Text);
            mc.Note = txtNote1.Text + " - " + txtNote2.Text;
            mc.Visibile = chkManodopVisibile.Checked;
            mc.Ricalcolo = chkManodopRicalcolo.Checked;
            mc.RicaricoSiNo = chkManodopRicaricoSiNo.Checked;

            if (txtPzzoManodop.Text != "")
                mc.PzzoUniCantiere = Convert.ToDecimal(txtPzzoManodop.Text);
            else
                mc.PzzoUniCantiere = 0;
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
                if (Convert.ToInt32(txtManodopQta.Text) > 0)
                {
                    if (isIntestazioneCompilata())
                        isInserito = MaterialiCantieriDAO.InserisciManodopera(mc);

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
        }

        //Visibilità pannello
        protected void btnManodop_Click(object sender, EventArgs e)
        {
            lblTitoloMaschera.Text = "Manodopera";
            txtTipDatCant.Text = "MANODOPERA";
            ShowPanels(false, true, false, false, false);
        }
        #endregion

        #region Operaio
        /* HELPERS */
        protected void FillOperMatCant(MaterialiCantieri mc)
        {
            Operai op = OperaiDAO.GetOperaio(ddlScegliOperaio.SelectedItem.Value);
            mc.CodArt = op.Operaio;
            mc.DescriCodArt = op.DescrOp;

            mc.IdTblCantieri = Convert.ToInt32(ddlScegliCant.SelectedItem.Value);
            mc.DescriMateriali = txtDescrOper.Text;
            mc.Qta = Convert.ToDouble(txtOperQta.Text);
            mc.Visibile = chkOperVisibile.Checked;
            mc.Ricalcolo = chkOperRicalcolo.Checked;
            mc.RicaricoSiNo = chkOperRicaricoSiNo.Checked;
            mc.Tipologia = txtTipDatCant.Text;
            mc.Data = Convert.ToDateTime(txtDataDDT.Text);
            mc.Note = txtOperNote1.Text + " - " + txtOperNote2.Text;
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

            if (Convert.ToInt32(txtOperQta.Text) > 0)
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
        }
        //Visibilità pannello
        protected void btnGestOper_Click(object sender, EventArgs e)
        {
            Operai op = OperaiDAO.GetOperaio(ddlScegliOperaio.SelectedItem.Value);
            txtPzzoOper.Text = op.CostoOperaio.ToString();

            lblTitoloMaschera.Text = "Gestione Operaio";
            txtTipDatCant.Text = "OPERAIO";
            ShowPanels(false, false, true, false, false);
        }

        /* EVENTI TEXT-CHANGED */
        protected void ddlScegliOperaio_TextChanged(object sender, EventArgs e)
        {
            Operai op = OperaiDAO.GetOperaio(ddlScegliOperaio.SelectedItem.Value);
            txtPzzoOper.Text = op.CostoOperaio.ToString();
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

            if (txtArrotPzzoUnit.Text != "")
                mc.PzzoUniCantiere = Convert.ToDecimal(txtArrotPzzoUnit.Text);
            else
                mc.PzzoUniCantiere = 0;
        }

        /* EVENTI CLICK */
        protected void btnInsArrot_Click(object sender, EventArgs e)
        {
            bool isInserito = false;

            if (isDateNotSet())
                return;

            MaterialiCantieri mc = new MaterialiCantieri();
            FillArrotMatCant(mc);

            if (Convert.ToInt32(txtArrotQta.Text) > 0)
            {
                if (isIntestazioneCompilata())
                    isInserito = MaterialiCantieriDAO.InserisciArrotondamento(mc);

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
        }
        //Visibilità pannello
        protected void btnGestArrot_Click(object sender, EventArgs e)
        {
            lblTitoloMaschera.Text = "Gestione Arrotondamenti";
            txtTipDatCant.Text = "ARROTONDAMENTO";
            ShowPanels(false, false, false, true, false);
        }
        #endregion

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
        }
        //Visibilità pannello
        protected void btnGestPagam_Click(object sender, EventArgs e)
        {
            lblTitoloMaschera.Text = "Gestione Pagamenti";
            txtTipDatCant.Text = "PAGAMENTI";
            ShowPanels(false, false, false, false, true);
        }
        #endregion
    }
}