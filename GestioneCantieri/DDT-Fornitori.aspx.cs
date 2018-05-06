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
            ddlScegliFornitore.Items.Add(new ListItem("", "-1"));

            foreach (Fornitori f in listClienti)
            {
                ddlScegliFornitore.Items.Add(new ListItem(f.RagSocForni, f.IdFornitori.ToString()));
            }
        }
        protected void BindGrid()
        {
            List<DDTFornitori> ddtFornList = DDTFornitoriDAO.GetAllDDT();
            grdListaDDTFornitori.DataSource = ddtFornList;
            grdListaDDTFornitori.DataBind();
        }
        protected DDTFornitori FillDdtFornitoriObj()
        {
            DDTFornitori ddt = new DDTFornitori();
            ddt.IdFornitore = Convert.ToInt32(ddlScegliFornitore.SelectedValue);
            ddt.Data = Convert.ToDateTime(txtInsData.Text);
            ddt.Protocollo = Convert.ToInt64(txtInsProtocollo.Text);
            ddt.NumeroDdt = txtInsNumeroDdt.Text;
            ddt.Articolo = txtInsArticolo.Text;
            ddt.DescrizioneFornitore = txtInsDescrForn.Text;
            ddt.DescrizioneMau = txtInsDescrMau.Text;
            ddt.Qta = Convert.ToInt32(txtInsQta.Text);
            ddt.PrezzoUnitario = Convert.ToDecimal(txtInsPrezzoUnit.Text.Replace(",", "."));
            return ddt;
        }
        protected void SvuotaCampi()
        {
            ddlScegliFornitore.SelectedIndex = 0;
            txtInsData.Text = "";
            txtInsProtocollo.Text = "";
            txtInsNumeroDdt.Text = "";
            txtInsArticolo.Text = "";
            txtInsDescrForn.Text = "";
            txtInsDescrMau.Text = "";
            txtInsQta.Text = "";
            txtInsPrezzoUnit.Text = "";
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
            txtInsPrezzoUnit.Text = ddt.PrezzoUnitario.ToString();
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
                    lblError.Text = "DDT Fornitore " + id + " eliminato con successo";
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
            DDTFornitori ddt = FillDdtFornitoriObj();
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
        protected void btnModificaDDT_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hfIdDDT.Value);
            DDTFornitori ddt = FillDdtFornitoriObj();
            bool isUpdated = DDTFornitoriDAO.UpdateDDTFornitore(id, ddt);
            if (isUpdated)
            {
                lblError.Text = "DDT Fornitore " + id + " aggiornato con successo";
                lblError.ForeColor = Color.Blue;
            }
            else
            {
                lblError.Text = "NON è stato possibile aggiornare il DDT Fornitore con id = " + id;
                lblError.ForeColor = Color.Red;
            }
            BindGrid();
            SvuotaCampi();
        }
        #endregion
    }
}