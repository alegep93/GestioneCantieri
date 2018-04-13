using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GestioneCantieri.DAO
{
    public class LocaliDAO : BaseDAO
    {
        public static List<Locali> GetListLocali()
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string sql = "";
            List<Locali> localiList = new List<Locali>();

            try
            {
                sql = "SELECT IdLocali, NomeLocale FROM TblLocali ORDER BY NomeLocale ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Locali l = new Locali();
                    l.Id = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    l.NomeLocale = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    localiList.Add(l);
                }

                return localiList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei locali", ex);
            }
            finally { cn.Close(); }
        }
    }
}