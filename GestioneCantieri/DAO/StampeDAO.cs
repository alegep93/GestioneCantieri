using System;
using System.Data;
using System.Data.SqlClient;

namespace GestioneCantieri.DAO
{
    public class StampeDAO : BaseDAO
    {
        public static DataTable GetNomiStampe()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT id,nomeStampa " +
                      "FROM TblStampe " +
                      "ORDER BY id ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei nomi delle stampe", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
    }
}