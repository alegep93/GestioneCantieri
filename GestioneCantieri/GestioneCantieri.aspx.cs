using GestioneCantieri.DAO;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
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
                PopolaDdlScegliCant();
            }
        }

        /* HELPERS */
        protected void PopolaDdlScegliCant()
        {
            DataTable dt = GestioneCantieriDAO.GetCantieri(txtFiltroCantAnno.Text, txtFiltroCantCodCant.Text, txtFiltroCantDescrCodCant.Text, chkFiltroCantChiuso.Checked, chkFiltroCantRiscosso.Checked);
            List<Cantieri> listCantieri = dt.DataTableToList<Cantieri>();

            ddlScegliCant.Items.Clear();
            ddlScegliCant.Items.Add(new ListItem("", "-1"));

            foreach (Cantieri c in listCantieri)
            {
                string show = c.Anno + " - " + c.CodCant + " - " + c.DescriCodCAnt;
                ddlScegliCant.Items.Add(new ListItem(show, c.IdCantieri.ToString()));
            }
        }

        /* EVENTI CLICK */
        protected void btnFiltroCant_Click(object sender, EventArgs e)
        {
            PopolaDdlScegliCant();
        }

        /* EVENTI TEXT-CHANGED */
        protected void ddlScegliCant_TextChanged(object sender, EventArgs e)
        {

        }
    }
}