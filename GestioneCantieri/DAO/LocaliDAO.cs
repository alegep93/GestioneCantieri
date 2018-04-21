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
        public static string GetNomeLocale(int idLocale)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string sql = "";
            string nomeLocale = "";

            try
            {
                sql = "SELECT NomeLocale FROM TblLocali WHERE IdLocali = @idLocale ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("idLocale", idLocale));
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    nomeLocale = (dr.IsDBNull(0) ? null : dr.GetString(0));
                }

                return nomeLocale;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero del nome di un locale", ex);
            }
            finally { cn.Close(); }
        }
        public static bool InserisciLocale(string nomeLocale)
        {
            SqlConnection cn = GetConnection();
            string sql = "";
            bool ret = false;

            try
            {
                sql = "INSERT INTO TblLocali(NomeLocale) VALUES (@nomeLocale)";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("nomeLocale", nomeLocale));
                cmd.ExecuteNonQuery();
                ret = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un nuovo locale", ex);
            }
            finally { cn.Close(); }

            return ret;
        }
        public static bool ModificaLocale(int idLocale, string nomeLocale)
        {
            SqlConnection cn = GetConnection();
            string sql = "";
            bool ret = false;

            try
            {
                sql = "UPDATE TblLocali SET NomeLocale = @nomeLocale WHERE IdLocali = @idLocale";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("idLocale", idLocale));
                cmd.Parameters.Add(new SqlParameter("nomeLocale", nomeLocale));
                cmd.ExecuteNonQuery();
                ret = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'aggiornamento del nome di un locale", ex);
            }
            finally { cn.Close(); }

            return ret;
        }
        public static bool EliminaLocale(int idLocale)
        {
            SqlConnection cn = GetConnection();
            string sql = "";
            bool ret = false;

            try
            {
                sql = "DELETE FROM TblLocali WHERE IdLocali = @idLocale";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("idLocale", idLocale));
                cmd.ExecuteNonQuery();
                ret = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione di un locale", ex);
            }
            finally { cn.Close(); }

            return ret;
        }
    }
}