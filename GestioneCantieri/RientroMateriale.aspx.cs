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
    public partial class RientroMateriale : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /* HELPERS */
        //Fill Ddl
        protected void FillDdlScegliCant()
        {
            DataTable dt = RientroMaterialeDAO.GetCantieri(txtFiltroCantAnno.Text, txtFiltroCantCodCant.Text, txtFiltroCantDescrCodCant.Text, chkFiltroCantChiuso.Checked, chkFiltroCantRiscosso.Checked);
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
            DataTable dt = RientroMaterialeDAO.GetOperai();
            List<Operai> listOperai = dt.DataTableToList<Operai>();

            ddlScegliAcquirente.Items.Clear();
            ddlScegliAcquirente.Items.Add(new ListItem("", "-1"));

            foreach (Operai op in listOperai)
            {
                string show = op.NomeOp + " - " + op.DescrOp;
                ddlScegliAcquirente.Items.Add(new ListItem(show, op.IdOperaio.ToString()));
            }
        }
        protected void FillDdlScegliFornit()
        {
            DataTable dt = RientroMaterialeDAO.GetFornitori();
            List<Fornitori> listFornitori = dt.DataTableToList<Fornitori>();

            ddlScegliFornit.Items.Clear();
            ddlScegliFornit.Items.Add(new ListItem("", "-1"));

            foreach (Fornitori f in listFornitori)
            {
                string show = f.RagSocForni;
                ddlScegliFornit.Items.Add(new ListItem(show, f.IdFornitori.ToString()));
            }
        }
        protected void FillDdlTipDatCant()
        {
            DataTable dt = RientroMaterialeDAO.GetTipDatCant();
            List<TipDatCant> listTipologie = dt.DataTableToList<TipDatCant>();

            ddlTipDatCant.Items.Clear();
            ddlTipDatCant.Items.Add(new ListItem("", "-1"));

            foreach (TipDatCant t in listTipologie)
            {
                string show = t.Descrizione + " - " + t.Abbreviato;
                ddlTipDatCant.Items.Add(new ListItem(show, t.IdTipologia.ToString()));
            }
        }
        protected void FillDddlScegliListino()
        {
            List<Mamg0> listMamg0 = RientroMaterialeDAO.GetListino(txtFiltroCodFSS.Text, txtFiltroAA_Des.Text);

            ddlScegliListino.Items.Clear();
            ddlScegliListino.Items.Add(new ListItem("", "-1"));

            foreach (Mamg0 mmg in listMamg0)
            {
                string show = mmg.CodArt + " - " + mmg.Desc + " - " + mmg.PrezzoNetto + " - " + mmg.PrezzoListino + " - " + mmg.Sconto1 + " - " + mmg.Sconto2 + " - " + mmg.Sconto3;
                ddlScegliListino.Items.Add(new ListItem(show, mmg.CodArt.ToString()));
            }
        }
        protected void FillDddlScegliMatCant()
        {
            string idCant = ddlScegliCant.SelectedItem.Value;
            DataTable dt = RientroMaterialeDAO.GetMaterialeCant(idCant, txtFiltroCodArt.Text, txtFiltroDescriCodArt.Text);
            List<MaterialiCantieri> listMatCant = dt.DataTableToList<MaterialiCantieri>();

            ddlScegliMatCant.Items.Clear();
            ddlScegliMatCant.Items.Add(new ListItem("", "-1"));

            foreach (MaterialiCantieri mc in listMatCant)
            {
                string show = mc.CodArt + " - " + mc.DescriCodArt + " - " + mc.Qta + " - " + mc.PzzoUniCantiere+ " - " + mc.Data;
                ddlScegliMatCant.Items.Add(new ListItem(show, mc.IdMaterialiCantieri.ToString()));
            }
        }
        //Ogni Helper "Fill" va aggiunto qua dentro per funzionare
        protected void FillAllDdl()
        {
            FillDdlScegliCant();
            FillDdlScegliAcquirente();
            FillDdlScegliFornit();
            FillDdlTipDatCant();
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
            if (ddlScegliCant.SelectedIndex != 0)
            {
                pnlSubIntestazione.Visible = true;
            }
            else
            {
                pnlSubIntestazione.Visible = false;
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
                string[] partiListino = ddlScegliListino.SelectedItem.Text.Split('-');
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
            txtPzzoUnit.Text = Math.Round(Convert.ToDecimal(txtPzzoNettoMef.Text), 2).ToString();
        }

        protected void btnInserisci_Click(object sender, EventArgs e)
        {
            string idCant = ddlScegliCant.SelectedItem.Value;
            string acquirente = ddlScegliAcquirente.SelectedItem.Value;
            string fornitore = ddlScegliFornit.SelectedItem.Value;

            if (Convert.ToInt32(txtQta.Text) > 0 && Convert.ToDecimal(txtPzzoUnit.Text) > 0)
            {
                bool isInserito = RientroMaterialeDAO.InserisciMaterialeCantiere(idCant, txtDescrMat.Text, txtQta.Text, chkVisibile.Checked, chkRicalcolo.Checked,
                    chkRicarico.Checked, txtDataDDT.Text, txtPzzoUnit.Text, "", txtCodArt.Text, txtDescriCodArt.Text, "", "", "", "", "",
                    acquirente, fornitore, txtNumBolla.Text, txtProtocollo.Text, txtNote.Text);

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
        }

        protected void txtFiltroDescriCodArt_TextChanged(object sender, EventArgs e)
        {
            FillDddlScegliMatCant();
        }

        protected void txtFiltroCodArt_TextChanged(object sender, EventArgs e)
        {
            FillDddlScegliMatCant();
        }

        protected void ddlScegliMatCant_TextChanged(object sender, EventArgs e)
        {
            if (ddlScegliMatCant.SelectedIndex != 0)
                pnlRientroMatCant.Visible = true;
            else
                pnlRientroMatCant.Visible = false;
        }
    }
}