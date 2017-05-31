using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestioneCantieri.DAO
{
    public class Mamg0DAO : BaseDAO
    {
        public static List<Mamg0> getAll()
        {
            List<Mamg0> list = new List<Mamg0>();
            string sql = "";
            SqlDataReader dr = null;
            SqlConnection cn = GetConnection();

            try
            {
                sql = "SELECT TOP 500 (AA_SIGF + AA_CODF) AS CodArt, AA_DES, AA_UM, AA_PZ, AA_PRZ, AA_SCONTO1, AA_SCONTO2, AA_SCONTO3, AA_PRZ1 " +
                      "FROM MAMG0 " +
                      "ORDER BY CodArt ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                dr = cmd.ExecuteReader(); //Esegue il comando e lo inserisce nel DataReader

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
                    list.Add(m);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero del listino", ex);
            }
            finally { cn.Close(); }
        }
        public static List<Mamg0> searchListino(string codArt1, string codArt2, string codArt3, string desc1, string desc2, string desc3)
        {
            List<Mamg0> list = new List<Mamg0>();
            string sql = "";
            SqlDataReader dr = null;
            SqlConnection cn = GetConnection();

            codArt1 = "%" + codArt1 + "%";
            codArt2 = "%" + codArt2 + "%";
            codArt3 = "%" + codArt3 + "%";
            desc1 = "%" + desc1 + "%";
            desc2 = "%" + desc2 + "%";
            desc3 = "%" + desc3 + "%";

            try
            {
                if (codArt1 == "%%" && codArt2 == "%%" && codArt3 == "%%" && desc1 == "%%" && desc2 == "%%" && desc3 == "%%")
                {
                    sql = "SELECT TOP 500 (AA_SIGF + AA_CODF) AS CodArt, AA_DES, AA_UM, AA_PZ, AA_PRZ, AA_SCONTO1, AA_SCONTO2, AA_SCONTO3, AA_PRZ1 " +
                          "FROM MAMG0 " +
                          "ORDER BY CodArt ASC ";
                }
                else
                {
                    sql = "SELECT (AA_SIGF + AA_CODF) AS CodArt, AA_DES, AA_UM, AA_PZ, AA_PRZ, AA_SCONTO1, AA_SCONTO2, AA_SCONTO3, AA_PRZ1 " +
                          "FROM MAMG0 " +
                          "WHERE (AA_SIGF + AA_CODF) LIKE @pCodArt1 AND (AA_SIGF + AA_CODF) LIKE @pCodArt2 AND (AA_SIGF + AA_CODF) LIKE @pCodArt3 " +
                          "AND AA_DES LIKE @pDescriCodArt1 AND AA_DES LIKE @pDescriCodArt2 AND AA_DES LIKE @pDescriCodArt3 " +
                          "ORDER BY CodArt ASC ";
                }

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pCodArt1", codArt1));
                cmd.Parameters.Add(new SqlParameter("pCodArt2", codArt2));
                cmd.Parameters.Add(new SqlParameter("pCodArt3", codArt3));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt1", desc1));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt2", desc2));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt3", desc3));
                dr = cmd.ExecuteReader(); //Esegue il comando e lo inserisce nel DataReader

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
                    list.Add(m);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero del listino con i filtri", ex);
            }
            finally { cn.Close(); }
        }
    }
}