using Dapper;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace GestioneCantieri.DAO
{
    public class FruttiDAO : BaseDAO
    {
        public static bool InserisciFrutto(string nomeFrutto)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "IF NOT EXISTS (SELECT descr001 FROM TblFrutti WHERE descr001 = @pNomeFrutto) " +
                        "INSERT INTO TblFrutti(descr001) VALUES (@pNomeFrutto) ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pNomeFrutto", nomeFrutto));

                int rowNumber = cmd.ExecuteNonQuery();
                if (rowNumber > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un frutto", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static List<Frutti> getFrutti()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT ID1 AS 'Id', descr001 AS 'Descr' FROM TblFrutti " +
                      "ORDER BY descr001 ASC ";

                return cn.Query<Frutti>(sql).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei frutti", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static List<Frutti> getFruttiWithSearch(string f1, string f2, string f3)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string sql = "";
            List<Frutti> fruttiList = new List<Frutti>();

            f1 = "%" + f1 + "%";
            f2 = "%" + f2 + "%";
            f3 = "%" + f3 + "%";

            try
            {
                sql = "SELECT ID1,descr001 FROM TblFrutti " +
                      "WHERE descr001 LIKE @pF1 AND descr001 LIKE @pF2 AND descr001 LIKE @pF3 " +
                      "ORDER BY descr001 ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@pF1", f1));
                cmd.Parameters.Add(new SqlParameter("@pF2", f2));
                cmd.Parameters.Add(new SqlParameter("@pF3", f3));
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Frutti f = new Frutti();
                    f.Id = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    f.Descr = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    fruttiList.Add(f);
                }

                return fruttiList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la ricerca dei frutti", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
    }
}