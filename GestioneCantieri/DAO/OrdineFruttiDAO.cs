using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestioneCantieri.DAO
{
    public class OrdineFruttiDAO : BaseDAO
    {
        public static List<Cantieri> GetListCantieri()
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string sql = "";
            List<Cantieri> cantieriList = new List<Cantieri>();

            try
            {
                sql = "SELECT IdCantieri,CodCant,DescriCodCAnt FROM TblCantieri " +
                      "WHERE Chiuso = 0 " +
                      "ORDER BY CodCant ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Cantieri c = new Cantieri();
                    c.IdCantieri = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    c.CodCant = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    c.DescriCodCAnt = (dr.IsDBNull(2) ? null : dr.GetString(2));
                    cantieriList.Add(c);
                }

                return cantieriList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei cantieri", ex);
            }
            finally { cn.Close(); }
        }
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
        public static List<GruppiFrutti> GetGruppiWithSearch(string filter1, string filter2, string filter3)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string sql = "";
            List<GruppiFrutti> gruppiFruttiList = new List<GruppiFrutti>();

            filter1 = "%" + filter1 + "%";
            filter2 = "%" + filter2 + "%";
            filter3 = "%" + filter3 + "%";

            try
            {
                sql = "SELECT Id,NomeGruppo,Descrizione FROM TblGruppiFrutti " +
                      "WHERE NomeGruppo LIKE @pFilter1 AND NomeGruppo LIKE @pFilter2 AND NomeGruppo LIKE @pFilter3 " +
                      "ORDER BY NomeGruppo ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pFilter1", filter1));
                cmd.Parameters.Add(new SqlParameter("pFilter2", filter2));
                cmd.Parameters.Add(new SqlParameter("pFilter3", filter3));
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    GruppiFrutti gf = new GruppiFrutti();
                    gf.Id = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    gf.NomeGruppo = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    gf.Descr = (dr.IsDBNull(2) ? null : dr.GetString(2));
                    gruppiFruttiList.Add(gf);
                }

                return gruppiFruttiList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei gruppi cercati", ex);
            }
            finally { cn.Close(); }
        }
        public static bool InserisciGruppo(string idCantiere, string idGruppoFrutto, string idLocale)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblMatOrdFrut(IdCantiere,IdGruppiFrutti,IdLocale) " +
                      "VALUES (@pIdCantiere,@pIdGruppoFrutto,@pIdLocale) ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@pIdCantiere", idCantiere));
                cmd.Parameters.Add(new SqlParameter("@pIdGruppoFrutto", idGruppoFrutto));
                cmd.Parameters.Add(new SqlParameter("@pIdLocale", idLocale));

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un gruppo", ex);
            }
            finally { cn.Close(); }
        }
        public static bool InserisciFruttoNonInGruppo(string idCantiere, string idLocale, string idFrutto, string qtaFrutti)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblMatOrdFrut(IdCantiere,IdLocale,IdFrutto,QtaFrutti) " +
                      "VALUES (@pIdCantiere,@pIdLocale,@idFrutto,@qtaFrutti) ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@pIdCantiere", idCantiere));
                cmd.Parameters.Add(new SqlParameter("@pIdLocale", idLocale));
                cmd.Parameters.Add(new SqlParameter("@idFrutto", idFrutto));
                cmd.Parameters.Add(new SqlParameter("@qtaFrutti", qtaFrutti));

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un frutto non appartenente ad un gruppo", ex);
            }
            finally { cn.Close(); }
        }
        public static List<MatOrdFrut> getGruppi(string idCantiere, string idLocale)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string sql = "";
            List<MatOrdFrut> gruppiFruttiList = new List<MatOrdFrut>();

            try
            {
                sql = "SELECT GF.NomeGruppo,GF.Descrizione " +
                      "FROM TblMatOrdFrut AS MOF " +
                      "JOIN TblGruppiFrutti AS GF ON (MOF.IdGruppiFrutti = GF.Id) " +
                      "WHERE IdCantiere = @pIdCant AND IdLocale = @pIdLocale " +
                      "ORDER BY NomeGruppo ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", idCantiere));
                cmd.Parameters.Add(new SqlParameter("pIdLocale", idLocale));
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    MatOrdFrut mof = new MatOrdFrut();
                    mof.NomeGruppo = (dr.IsDBNull(0) ? null : dr.GetString(0));
                    mof.Descrizione = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    gruppiFruttiList.Add(mof);
                }

                return gruppiFruttiList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei gruppi", ex);
            }
            finally { cn.Close(); }
        }
        public static List<MatOrdFrut> GetFruttiNonInGruppo(string idCant)
        {
            SqlConnection cn = GetConnection();
            List<MatOrdFrut> list = new List<MatOrdFrut>();
            SqlDataReader dr = null;
            try
            {
                string sql = "select F.descr001, SUM(MOF.QtaFrutti) " +
                             "FROM TblMatOrdFrut AS MOF " +
                             "LEFT JOIN TblFrutti AS F ON(MOF.IdFrutto = F.ID1) " +
                             "where IdCantiere = @pIdCant AND MOF.idFrutto IS NOT NULL AND MOF.QtaFrutti IS NOT NULL " +
                             "GROUP BY F.descr001 " +
                             "ORDER BY F.descr001 ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", idCant));
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    MatOrdFrut mof = new MatOrdFrut();
                    mof.Descrizione = (dr.IsDBNull(0) ? "" : dr.GetString(0));
                    mof.QtaFrutti = (dr.IsDBNull(1) ? -1 : dr.GetInt32(1));

                    list.Add(mof);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la stampa dei frutti in un locale.", ex);
            }
            finally { cn.Close(); }
        }
        public static List<MatOrdFrut> printFruttiQta(string idCantiere, string idLocale)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string sql = "";
            List<MatOrdFrut> gruppiFruttiList = new List<MatOrdFrut>();

            try
            {
                sql = "SELECT GF.NomeGruppo, SUM(CGF.Qta), F.descr001 " +
                      "FROM TblMatOrdFrut AS MOF " +
                      "JOIN TblGruppiFrutti AS GF ON(MOF.IdGruppiFrutti = GF.Id) " +
                      "JOIN TblCompGruppoFrut AS CGF ON(CGF.IdTblGruppo = GF.Id) " +
                      "JOIN TblFrutti AS F ON(CGF.IdTblFrutto = F.ID1) " +
                      "WHERE IdCantiere = @pIdCant AND IdLocale = @pIdLocale " +
                      "GROUP BY NomeGruppo,qta,descr001 " +
                      "ORDER BY NomeGruppo ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", idCantiere));
                cmd.Parameters.Add(new SqlParameter("pIdLocale", idLocale));
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    MatOrdFrut mof = new MatOrdFrut();
                    mof.NomeGruppo = (dr.IsDBNull(0) ? null : dr.GetString(0));
                    mof.Descrizione = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    gruppiFruttiList.Add(mof);
                }

                return gruppiFruttiList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la stampa dei frutti", ex);
            }
            finally { cn.Close(); }
        }
    }
}