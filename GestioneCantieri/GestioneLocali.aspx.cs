using GestioneCantieri.DAO;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestioneCantieri
{
    public partial class GestioneLocali : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                btnModificaLocale.Visible = false;
                btnInserisciLocale.Visible = true;
            }
        }

        #region Helpers
        private void BindGrid()
        {
            List<Locali> localiList = LocaliDAO.GetListLocali();
            grdLocali.DataSource = localiList;
            grdLocali.DataBind();
        }
        private void ModificaLocale(int idLocale)
        {
            hfIdLocale.Value = idLocale.ToString();
            btnModificaLocale.Visible = true;
            btnInserisciLocale.Visible = false;
            txtNomeLocale.Text = LocaliDAO.GetNomeLocale(idLocale);
        }
        private void EliminaLocale(int idLocale)
        {
            bool isEliminato = LocaliDAO.EliminaLocale(idLocale);
            if (isEliminato)
            {
                lblError.Text = "Locale eliminato correttamente";
                lblError.ForeColor = Color.Blue;
            }
            else
            {
                lblError.Text = "Impossibile eliminare il locale selezionato";
                lblError.ForeColor = Color.Red;
            }

            BindGrid();
            btnModificaLocale.Visible = false;
            btnInserisciLocale.Visible = true;
        }
        #endregion

        #region Eventi Click
        protected void btnInserisciLocale_Click(object sender, EventArgs e)
        {
            bool isInserito = LocaliDAO.InserisciLocale(txtNomeLocale.Text);
            if (isInserito)
            {
                lblError.Text = "Nuovo locale inserito correttamente";
                lblError.ForeColor = Color.Blue;
                txtNomeLocale.Text = "";
            }
            else
            {
                lblError.Text = "Impossibile inserire il locale " + txtNomeLocale.Text;
                lblError.ForeColor = Color.Red;
            }

            BindGrid();
        }
        protected void btnModificaLocale_Click(object sender, EventArgs e)
        {
            int idLocale = Convert.ToInt32(hfIdLocale.Value);
            bool isModificato = LocaliDAO.ModificaLocale(idLocale, txtNomeLocale.Text);
            if (isModificato)
            {
                lblError.Text = "Nuovo locale modificato correttamente in " + txtNomeLocale.Text;
                lblError.ForeColor = Color.Blue;
                txtNomeLocale.Text = "";
            }
            else
            {
                lblError.Text = "Impossibile modificare il nome del locale in " + txtNomeLocale.Text;
                lblError.ForeColor = Color.Red;
            }

            BindGrid();
            btnModificaLocale.Visible = false;
            btnInserisciLocale.Visible = true;
        }
        #endregion

        #region Eventi RowCommand
        protected void grdLocali_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idLocale = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName == "ModificaLocale")
            {
                ModificaLocale(idLocale);
            }
            else if (e.CommandName == "EliminaLocale")
            {
                EliminaLocale(idLocale);
            }
        }
        #endregion
    }
}