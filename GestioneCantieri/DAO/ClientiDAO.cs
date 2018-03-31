using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestioneCantieri.DAO
{
    public class ClientiDAO : BaseDAO
    {
        //SELECT
        public static List<Clienti> GetClienti(string filtro)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            List<Clienti> list = new List<Clienti>();
            string sql = "";

            filtro = "%" + filtro + "%";

            try
            {
                sql = "SELECT IdCliente, RagSocCli " +
                      "FROM TblClienti " +
                      "WHERE RagSocCli LIKE @filtro ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("filtro", filtro));
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Clienti c = new Clienti();
                    c.IdCliente = dr.IsDBNull(0) ? -1 : dr.GetInt32(0);
                    c.RagSocCli = dr.IsDBNull(1) ? "" : dr.GetString(1);
                    list.Add(c);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'applicazione dei filtri sui cantieri", ex);
            }
            finally { cn.Close();}

            return list;
        }
    }
}