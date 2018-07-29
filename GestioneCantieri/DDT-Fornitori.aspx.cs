using GestioneCantieri.DAO;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestioneCantieri
{
    public partial class DDT_Fornitori : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDdlFornitori();
                BindGrid();
                lblError.Text = "";
                SvuotaCampi();
            }
        }

        #region Helpers
        protected void FillDdlFornitori()
        {
            List<Fornitori> listClienti = FornitoriDAO.GetFornitori();

            ddlScegliFornitore.Items.Clear();
            ddlFiltraFornitore.Items.Clear();
            ddlScegliFornitore.Items.Add(new ListItem("", "-1"));
            ddlFiltraFornitore.Items.Add(new ListItem("", "-1"));

            foreach (Fornitori f in listClienti)
            {
                ddlScegliFornitore.Items.Add(new ListItem(f.RagSocForni, f.IdFornitori.ToString()));
                ddlFiltraFornitore.Items.Add(new ListItem(f.RagSocForni, f.IdFornitori.ToString()));
            }
        }
        protected void BindGrid()
        {
            List<DDTFornitori> ddtFornList = DDTFornitoriDAO.GetAllDDT();
            grdListaDDTFornitori.DataSource = ddtFornList;
            grdListaDDTFornitori.DataBind();
            GeneraPrezzoUnitario(ddtFornList);
        }

        private void GeneraPrezzoUnitario(List<DDTFornitori> ddtFornList)
        {
            // Popolo la cella del "Prezzo Finale"
            for (int i = 0; i < ddtFornList.Count; i++)
            {
                ddtFornList[i].PrezzoUnitario = ddtFornList[i].Valore / Convert.ToDecimal(ddtFornList[i].Qta);
                grdListaDDTFornitori.Rows[i].Cells[9].Text = ddtFornList[i].PrezzoUnitario.ToString("N2");
            }
        }

        protected DDTFornitori FillDdtFornitoriObj()
        {
            DDTFornitori ddt = new DDTFornitori();
            ddt.IdFornitore = Convert.ToInt32(ddlScegliFornitore.SelectedValue);
            ddt.RagSocFornitore = FornitoriDAO.GetRagSocFornitore(ddt.IdFornitore);
            ddt.Data = Convert.ToDateTime(txtInsData.Text);
            ddt.Protocollo = Convert.ToInt64(txtInsProtocollo.Text);
            ddt.NumeroDdt = txtInsNumeroDdt.Text;
            ddt.Articolo = txtInsArticolo.Text;
            ddt.DescrizioneFornitore = txtInsDescrForn.Text;
            ddt.DescrizioneMau = txtInsDescrMau.Text;
            ddt.Qta = Convert.ToInt32(txtInsQta.Text);

            try
            {
                ddt.Valore = Convert.ToDecimal(txtInsValore.Text.Replace(".", ","));
            }
            catch
            {
                lblError.Text = "NON è possibile scrivere lettere o caratteri speciali nel \"Valore\"";
                lblError.ForeColor = Color.Red;
            }
            return ddt;
        }
        protected DDTFornitori FillObjForSearch()
        {
            DDTFornitori ddt = new DDTFornitori();
            ddt.IdFornitore = ddlFiltraFornitore.SelectedValue != "" ? Convert.ToInt32(ddlFiltraFornitore.SelectedValue) : -1;
            ddt.Protocollo = txtFiltraProtocollo.Text != "" ? Convert.ToInt64(txtFiltraProtocollo.Text) : -1;
            ddt.NumeroDdt = txtFiltraNumeroDdt.Text != "" ? txtFiltraNumeroDdt.Text : "";
            ddt.Articolo = txtFiltraArticolo.Text != "" ? txtFiltraArticolo.Text : "";
            ddt.Qta = txtFiltraQta.Text != "" ? Convert.ToInt32(txtFiltraQta.Text) : -1;
            ddt.DescrizioneFornitore = txtFiltroDescrForn.Text != "" ? txtFiltroDescrForn.Text : "";
            ddt.DescrizioneMau = txtFiltroDescrMau.Text != "" ? txtFiltroDescrMau.Text : "";
            return ddt;
        }
        protected void SvuotaCampi()
        {
            //ddlScegliFornitore.SelectedIndex = 0;
            //txtInsData.Text = "";
            //txtInsProtocollo.Text = "";
            //txtInsNumeroDdt.Text = "";
            txtInsArticolo.Text = "";
            txtInsDescrForn.Text = "";
            txtInsDescrMau.Text = "";
            txtInsQta.Text = "";
            txtInsValore.Text = "";
            btnModificaDDT.Visible = false;
            btnInserisciDDT.Visible = true;
        }
        protected void ModificaDDT(int id)
        {
            btnModificaDDT.Visible = true;
            btnInserisciDDT.Visible = false;
            DDTFornitori ddt = DDTFornitoriDAO.GetDDT(id);
            ddlScegliFornitore.SelectedValue = ddt.IdFornitore.ToString();
            txtInsData.Text = ddt.Data.ToString("dd/MM/yyyy");
            txtInsProtocollo.Text = ddt.Protocollo.ToString();
            txtInsNumeroDdt.Text = ddt.NumeroDdt.ToString();
            txtInsArticolo.Text = ddt.Articolo.ToString();
            txtInsDescrForn.Text = ddt.DescrizioneFornitore.ToString();
            txtInsDescrMau.Text = ddt.DescrizioneMau.ToString();
            txtInsQta.Text = ddt.Qta.ToString();
            txtInsValore.Text = ddt.Valore.ToString();
        }
        #endregion

        #region Eventi RowCommand
        protected void grdListaDDTFornitori_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());
            hfIdDDT.Value = id.ToString();

            if (e.CommandName == "ModDDT")
            {
                ModificaDDT(id);
            }
            else if (e.CommandName == "ElimDDT")
            {
                bool isDeleted = DDTFornitoriDAO.DeleteDDTFornitore(id);
                if (isDeleted)
                {
                    lblError.Text = "DDT Fornitore con id = " + id + ", eliminato con successo";
                    lblError.ForeColor = Color.Blue;
                }
                else
                {
                    lblError.Text = "NON è stato possibile eliminare il DDT Fornitore con id = " + id;
                    lblError.ForeColor = Color.Red;
                }
            }

            BindGrid();
        }
        #endregion

        #region Eventi Click
        protected void btnInserisciDDT_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            DDTFornitori ddt = FillDdtFornitoriObj();

            if (lblError.Text == "")
            {
                bool isInserted = DDTFornitoriDAO.InsertNewFornitore(ddt);
                if (isInserted)
                {
                    lblError.Text = "Nuovo DDT Fornitore inserito correttamente";
                    lblError.ForeColor = Color.Blue;
                }
                else
                {
                    lblError.Text = "NON è stato possibile inserire il nuovo DDT Fornitore";
                    lblError.ForeColor = Color.Red;
                }

                BindGrid();
                SvuotaCampi();
            }
        }
        protected void btnModificaDDT_Click(object sender, EventArgs e)
        {

            lblError.Text = "";
            DDTFornitori ddt = FillDdtFornitoriObj();
            ddt.Id = Convert.ToInt32(hfIdDDT.Value);
            if (lblError.Text == "")
            {
                bool isUpdated = DDTFornitoriDAO.UpdateDDTFornitore(ddt);
                if (isUpdated)
                {
                    lblError.Text = ddt.RagSocFornitore + " aggiornato con successo";
                    lblError.ForeColor = Color.Blue;
                }
                else
                {
                    lblError.Text = "NON è stato possibile aggiornare il record con fornitore = " + ddt.RagSocFornitore;
                    lblError.ForeColor = Color.Red;
                }
                BindGrid();
                SvuotaCampi();
            }
        }
        protected void btnFiltra_Click(object sender, EventArgs e)
        {
            if (ddlFiltraFornitore.SelectedIndex != 0 || txtFiltraProtocollo.Text != "" || txtFiltraNumeroDdt.Text != "" || txtFiltraArticolo.Text != "" || txtFiltraQta.Text != "" || txtFiltroDescrForn.Text != "" || txtFiltroDescrMau.Text != "")
            {
                DDTFornitori ddt = FillObjForSearch();
                List<DDTFornitori> ddtList = DDTFornitoriDAO.GetAllDDT(ddt);
                grdListaDDTFornitori.DataSource = ddtList;
                grdListaDDTFornitori.DataBind();
                GeneraPrezzoUnitario(ddtList);
            }
            else
            {
                BindGrid();
            }
        }
        #endregion
    }
}