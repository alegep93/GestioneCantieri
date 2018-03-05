using GestioneCantieri.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace GestioneCantieri.WebServices
{
    /// <summary>
    /// Summary description for ListinoService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ListinoService : WebService
    {
        [WebMethod]
        public void GetListino()
        {
            List<Mamg0> mamgoList = new List<Mamg0>();
            SqlConnection cn = DAO.BaseDAO.GetConnection();
            SqlDataReader dr = null;
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT (AA_SIGF + AA_CODF) AS CodArt, AA_DES, AA_UM, AA_PZ, AA_PRZ, AA_SCONTO1, AA_SCONTO2, AA_SCONTO3, AA_PRZ1 ");
            sql.Append("FROM MAMG0 ");
            sql.Append("ORDER BY CodArt ASC ");

            SqlCommand cmd = new SqlCommand(sql.ToString(), cn);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Mamg0 m = new Mamg0();
                m.CodArt = (dr.IsDBNull(0) ? null : dr.GetString(0));
                m.Desc = (dr.IsDBNull(1) ? null : dr.GetString(1));
                m.UnitMis = (dr.IsDBNull(2) ? null : dr.GetString(2));
                m.Pezzo = (dr.IsDBNull(3) ? (float)0.0 : (float)dr.GetDouble(3));
                m.PrezzoListino = (dr.IsDBNull(4) ? (float)0.0 : (float)dr.GetDouble(4));
                m.Sconto1 = (dr.IsDBNull(5) ? (float)0.0 : (float)dr.GetDouble(5));
                m.Sconto2 = (dr.IsDBNull(6) ? (float)0.0 : (float)dr.GetDouble(6));
                m.Sconto3 = (dr.IsDBNull(7) ? (float)0.0 : (float)dr.GetDouble(7));
                m.PrezzoNetto = (dr.IsDBNull(8) ? (float)0.0 : (float)dr.GetDouble(8));
                mamgoList.Add(m);
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(mamgoList));
        }
    }
}
