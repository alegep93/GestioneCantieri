using GestioneCantieri.DAO;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestioneCantieri
{
    public partial class Listino : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SvuotaTxt();
            }
        }

        /* EVENTI CLICK */
        protected void btnSvuotaTxt_Click(object sender, EventArgs e)
        {
            SvuotaTxt();
        }

        /* HELPERS */

        protected void SvuotaTxt()
        {
            txtCodArt1.Text = "";
        }

        protected void btnEliminaListino_Click(object sender, EventArgs e)
        {
            Mamg0DAO.EliminaListino();
            Response.Redirect("~/Listino.aspx");
        }
    }
}