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
                pnlSubIntestazione.Visible = pnlMascheraGestCant.Visible = false;
            }
        }

        /* HELPERS */
        //Popolo un oggetto di tipo MaterialeCantiere per fare la insert
        protected void FillMatCant(MaterialiCantieri mc)
        {
            mc.IdTblCantieri = Convert.ToInt32(ddlScegliCant.SelectedItem.Value);
            mc.DescriMateriali = txtDescrMat.Text;
            mc.Qta = Convert.ToDouble(txtQta.Text);
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

            if (txtFascia.Text != "")
                mc.Fascia = Convert.ToInt32(txtFascia.Text);
            else
                mc.Fascia = -1;

            if (txtProtocollo.Text != "")
                mc.ProtocolloInterno = Convert.ToInt32(txtProtocollo.Text);
            else
                mc.ProtocolloInterno = -1;

            if (txtNumBolla.Enabled && txtNumBolla.Text != "")
                mc.NumeroBolla = Convert.ToInt32(txtNumBolla.Text);
            else if (ddlScegliDDTMef.SelectedIndex != -1)
                mc.NumeroBolla = Convert.ToInt32((ddlScegliDDTMef.SelectedItem.Text).Split('-')[3]);
            else
                mc.NumeroBolla = -1;

            if (txtPzzoFinCli.Text != "")
                mc.PzzoFinCli = Convert.ToDecimal(txtPzzoFinCli.Text);
            else
                mc.PzzoFinCli = 0.0m;
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

        //Ogni Helper "Fill" va aggiunto qua dentro per essere richiamato all'apertura dell'applicazione
        protected void FillAllDdl()
        {
            FillDdlScegliCant();
            FillDdlScegliAcquirente();
            FillDdlScegliFornit();
        }

        /* EVENTI CLICK */
        protected void btnFiltroCant_Click(object sender, EventArgs e)
        {
            FillDdlScegliCant();
            pnlSubIntestazione.Visible = false;
        }
        protected void btnInserisci_Click(object sender, EventArgs e)
        {
            string idCant = ddlScegliCant.SelectedItem.Value;
            string acquirente = ddlScegliAcquirente.SelectedItem.Value;
            string fornitore = ddlScegliFornit.SelectedItem.Value;
            string numeroBolla = "";

            if (txtDataDDT.Text == "")
            {
                lblIsRecordInserito.Text = "Inserire un valore per la data";
                lblIsRecordInserito.ForeColor = Color.Red;
                return;
            }

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

                bool isInserito = MaterialiCantieriDAO.InserisciMaterialeCantiere(mc);

                if (isInserito)
                {
                    lblIsRecordInserito.Text = "Record inserito con successo";
                    lblIsRecordInserito.ForeColor = Color.Blue;
                }
                else
                {
                    lblIsRecordInserito.Text = "Errore durante l'inserimento del record";
                    lblIsRecordInserito.ForeColor = Color.Red;
                }
            }
            else
            {
                lblIsRecordInserito.Text = "Quantità e/o Prezzo Unitario devono essere maggiori di 0";
                lblIsRecordInserito.ForeColor = Color.Red;
            }
        }

        /* EVENTI TEXT-CHANGED */
        protected void ddlScegliCant_TextChanged(object sender, EventArgs e)
        {
            if (ddlScegliCant.SelectedIndex != 0)
            {
                pnlSubIntestazione.Visible = true;
                pnlMascheraGestCant.Visible = true;
            }
            else
            {
                pnlSubIntestazione.Visible = false;
                pnlMascheraGestCant.Visible = false;
            }
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
                txtCodArt.Text = partiListino[0];
                txtDescriCodArt.Text = partiListino[1];
                txtPzzoNettoMef.Text = partiListino[2];
                txtPzzoUnit.Text = "0.00";
            }
            else
            {
                txtCodArt.Text = txtDescriCodArt.Text = txtPzzoNettoMef.Text = "";
                txtPzzoUnit.Text = "0.00";
            }
        }
        protected void btnCalcolaPrezzoUnit_Click(object sender, EventArgs e)
        {
            if (txtPzzoNettoMef.Text != "")
                txtPzzoUnit.Text = Math.Round(Convert.ToDecimal(txtPzzoNettoMef.Text), 2).ToString();
            else
            {
                lblIsRecordInserito.Text = "Inserire un valore nella casella 'Prezzo Netto Mef' per calcolare il 'Prezzo Unitario'";
                lblIsRecordInserito.ForeColor = Color.Red;
            }
        }
    }
}