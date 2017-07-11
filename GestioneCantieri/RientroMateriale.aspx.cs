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
            if (!IsPostBack)
            {
                FillAllDdl();
                pnlSubIntestazione.Visible = false;
                pnlRientroMatCant.Visible = false;
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
            mc.PzzoUniCantiere = Convert.ToDecimal(txtPzzoUnit.Text);
            mc.CodArt = txtCodArt.Text;
            mc.DescriCodArt = txtDescriCodArt.Text;
            mc.Tipologia = txtTipologia.Text;
            mc.Acquirente = ddlScegliAcquirente.SelectedItem.Value;
            mc.Fornitore = ddlScegliFornit.SelectedItem.Value;            
            mc.Note = txtNote.Text;

            if (txtDataDDT.Text != "")
                mc.Data = Convert.ToDateTime(txtDataDDT.Text);
            else
                mc.Data = DateTime.Now;

            if (txtFascia.Text != "")
                mc.Fascia = Convert.ToInt32(txtFascia.Text);
            else
                mc.Fascia = -1;

            if (txtProtocollo.Text != "")
                mc.ProtocolloInterno = Convert.ToInt32(txtProtocollo.Text);
            else
                mc.ProtocolloInterno = -1;

            if (txtNumBolla.Text != "")
                mc.NumeroBolla = Convert.ToInt32(txtNumBolla.Text);
            else
                mc.NumeroBolla = -1;

            if (txtPzzoFinCli.Text != "")
                mc.PzzoFinCli = Convert.ToDecimal(txtPzzoFinCli.Text);
            else
                mc.PzzoFinCli = -1;
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
        protected void FillDdlScegliListino()
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
            string idCant = ddlScegliCant.SelectedItem.Value;
            List<MaterialiCantieri> listMatCant = MaterialiCantieriDAO.GetMaterialeCantiere(idCant, txtFiltroCodArt.Text, txtFiltroDescriCodArt.Text);

            ddlScegliMatCant.Items.Clear();
            ddlScegliMatCant.Items.Add(new ListItem("", "-1"));

            foreach (MaterialiCantieri mc in listMatCant)
            {
                string data = ((mc.Data).ToString()).Split(' ')[0];
                string show = mc.CodArt + " - " + mc.DescriCodArt + " - " + mc.Qta + " - " + mc.PzzoUniCantiere + " - " + data;
                ddlScegliMatCant.Items.Add(new ListItem(show, mc.IdMaterialiCantieri.ToString()));
            }
        }

        //Ogni Helper "Fill" va aggiunto qua dentro per essere richiamato all'avvio
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
            int maxQta = Convert.ToInt32((ddlScegliMatCant.SelectedItem.Text).Split('-')[2]);

            MaterialiCantieri mc = new MaterialiCantieri();
            FillMatCant(mc);

            if (Convert.ToDecimal(txtPzzoUnit.Text) > 0)
            {
                if (Convert.ToInt32(txtQta.Text) > 0 && Convert.ToInt32(txtQta.Text) <= maxQta)
                {
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
                    lblIsRecordInserito.Text = "La quantità è 0 o maggiore di quella del materiale scelto";
                    lblIsRecordInserito.ForeColor = Color.Red;
                }
            }
            else
            {
                lblIsRecordInserito.Text = "Inserire un valore maggiore di '0' per il campo Prezzo Unitario";
                lblIsRecordInserito.ForeColor = Color.Red;
            }
        }

        /* EVENTI TEXT-CHANGED */
        protected void ddlScegliCant_TextChanged(object sender, EventArgs e)
        {
            if (ddlScegliCant.SelectedIndex != 0)
            {
                pnlSubIntestazione.Visible = true;
                FillDdlScegliMatCant();
            }
            else
            {
                pnlSubIntestazione.Visible = false;
            }
        }
        protected void txtFiltroCodFSS_TextChanged(object sender, EventArgs e)
        {
            FillDdlScegliListino();
        }
        protected void txtFiltroAA_Des_TextChanged(object sender, EventArgs e)
        {
            FillDdlScegliListino();
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
            if(txtPzzoNettoMef.Text!="")
                txtPzzoUnit.Text = Math.Round(Convert.ToDecimal(txtPzzoNettoMef.Text), 2).ToString();
            else
            {
                lblIsRecordInserito.Text = "Inserire un valore nella casella 'Prezzo Netto Mef' per calcolare il 'Prezzo Unitario'";
                lblIsRecordInserito.ForeColor = Color.Red;
            }
        }
        protected void txtFiltroDescriCodArt_TextChanged(object sender, EventArgs e)
        {
            FillDdlScegliMatCant();
        }
        protected void txtFiltroCodArt_TextChanged(object sender, EventArgs e)
        {
            FillDdlScegliMatCant();
        }
        protected void ddlScegliMatCant_TextChanged(object sender, EventArgs e)
        {
            if (ddlScegliMatCant.SelectedIndex != 0)
            {
                pnlRientroMatCant.Visible = true;
                string[] partiMateriale = (ddlScegliMatCant.SelectedItem.Text).Split('-');
                int qta = Convert.ToInt32(partiMateriale[2]);
            }
            else
                pnlRientroMatCant.Visible = false;
        }
    }
}