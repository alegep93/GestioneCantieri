using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GestioneCantieri.DAO
{
    public class OrdineFruttiDAO : BaseDAO
    {
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
        public static List<MatOrdFrut> GetFruttiNonInGruppo(string idCant, string idLocale)
        {
            SqlConnection cn = GetConnection();
            List<MatOrdFrut> list = new List<MatOrdFrut>();
            SqlDataReader dr = null;
            try
            {
                string sql = "select F.descr001, SUM(MOF.QtaFrutti) " +
                             "FROM TblMatOrdFrut AS MOF " +
                             "LEFT JOIN TblFrutti AS F ON(MOF.IdFrutto = F.ID1) " +
                             "where IdCantiere = @pIdCant AND IdLocale = @pIdLocale AND MOF.idFrutto IS NOT NULL AND MOF.QtaFrutti IS NOT NULL " +
                             "GROUP BY F.descr001 " +
                             "ORDER BY F.descr001 ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", idCant));
                cmd.Parameters.Add(new SqlParameter("pIdLocale", idLocale));
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
        public static bool DeleteGruppo(int idGruppo)
        {
            SqlConnection cn = GetConnection();
            string sql = "";
            bool ret = false;

            try
            {
                sql = "DELETE FROM TblMatOrdFrut WHERE IdGruppiFrutti = @idGruppo";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@idGruppo", idGruppo));

                cmd.ExecuteNonQuery();

                ret = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione di un gruppo da un ordine", ex);
            }
            finally { cn.Close(); }

            return ret;
        }
        public static List<StampaOrdFrutCantLoc> GetAllGruppiInLocale(string idCant)
        {
            SqlConnection cn = GetConnection();
            List<StampaOrdFrutCantLoc> list = new List<StampaOrdFrutCantLoc>();
            SqlDataReader dr = null;
            try
            {
                string sql = "SELECT L.NomeLocale, GF.NomeGruppo, COUNT(Gf.NomeGruppo) " +
                             "FROM TblMatOrdFrut AS MOF " +
                             "JOIN TblLocali AS L ON(MOF.IdLocale = L.IdLocali) " +
                             "JOIN TblGruppiFrutti AS GF ON(MOF.IdGruppiFrutti = GF.Id) " +
                             "WHERE IdCantiere = @pIdCant " +
                             "GROUP BY L.NomeLocale, GF.NomeGruppo " +
                             "ORDER BY NomeLocale ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", idCant));
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    StampaOrdFrutCantLoc cantLoc = new StampaOrdFrutCantLoc()
                    {
                        NomeLocale = (dr.IsDBNull(0) ? null : dr.GetString(0)),
                        NomeGruppo = (dr.IsDBNull(1) ? null : dr.GetString(1)),
                        Qta = (dr.IsDBNull(2) ? -1 : dr.GetInt32(2))
                    };
                    list.Add(cantLoc);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la stampa dei gruppi in un locale.", ex);
            }
            finally { cn.Close(); }

        }
        public static List<StampaOrdFrutCantLoc> GetAllFruttiInLocale(string idCant)
        {
            SqlConnection cn = GetConnection();
            List<StampaOrdFrutCantLoc> list = new List<StampaOrdFrutCantLoc>();
            SqlDataReader dr = null;
            try
            {
                string sql = "SELECT F.descr001, SUM(CGF.Qta) As Qta " +
                             "FROM TblMatOrdFrut AS MOF " +
                             "JOIN TblLocali AS L ON(MOF.IdLocale = L.IdLocali) " +
                             "JOIN TblGruppiFrutti AS GF ON(MOF.IdGruppiFrutti = GF.Id) " +
                             "JOIN TblCompGruppoFrut AS CGF ON(CGF.IdTblGruppo = GF.Id) " +
                             "JOIN TblFrutti AS F ON(CGF.IdTblFrutto = F.ID1) " +
                             "WHERE IdCantiere = @pIdCant " +
                             "GROUP BY F.descr001 " +
                             "ORDER BY F.descr001 ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", idCant));
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    StampaOrdFrutCantLoc scLoc = new StampaOrdFrutCantLoc();
                    scLoc.Descr001 = (dr.IsDBNull(0) ? null : dr.GetString(0));
                    scLoc.Qta = (dr.IsDBNull(1) ? -1 : dr.GetInt32(1));

                    list.Add(scLoc);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la stampa dei frutti in un locale.", ex);
            }
            finally { cn.Close(); }

        }
        public static DataTable GetAllFruttiInLocaleDataTable(string idCant)
        {
            SqlConnection cn = GetConnection();
            try
            {
                string sql = "SELECT F.descr001, SUM(CGF.Qta) AS Qta " +
                             "FROM TblMatOrdFrut AS MOF " +
                             "JOIN TblLocali AS L ON(MOF.IdLocale = L.IdLocali) " +
                             "JOIN TblGruppiFrutti AS GF ON(MOF.IdGruppiFrutti = GF.Id) " +
                             "JOIN TblCompGruppoFrut AS CGF ON(CGF.IdTblGruppo = GF.Id) " +
                             "JOIN TblFrutti AS F ON(CGF.IdTblFrutto = F.ID1) " +
                             "WHERE IdCantiere = @pIdCant " +
                             "GROUP BY F.descr001 " +
                             "ORDER BY F.descr001 ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", idCant));
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la stampa dei frutti in un locale.", ex);
            }
            finally { cn.Close(); }

        }
        public static List<StampaOrdFrutCantLoc> GetAllFruttiNonInGruppo(string idCant)
        {
            SqlConnection cn = GetConnection();
            List<StampaOrdFrutCantLoc> list = new List<StampaOrdFrutCantLoc>();
            SqlDataReader dr = null;
            try
            {
                string sql = "SELECT F.descr001, SUM(MOF.QtaFrutti) AS Qta " +
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
                    StampaOrdFrutCantLoc scLoc = new StampaOrdFrutCantLoc();
                    scLoc.Descr001 = (dr.IsDBNull(0) ? null : dr.GetString(0));
                    scLoc.Qta = (dr.IsDBNull(1) ? -1 : dr.GetInt32(1));

                    list.Add(scLoc);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la stampa dei frutti in un locale.", ex);
            }
            finally { cn.Close(); }

        }
        public static List<StampaOrdFrutCantLoc> GetFruttiPerStampaExcel(string idCant)
        {
            SqlConnection cn = GetConnection();
            List<StampaOrdFrutCantLoc> list = new List<StampaOrdFrutCantLoc>();
            SqlDataReader dr = null;

            try
            {
                string sql = "SELECT descr, SUM(Qta) FROM (" +
                                "SELECT F.descr001 AS descr, SUM(CGF.Qta) As Qta " +
                                "FROM TblMatOrdFrut AS MOF " +
                                "JOIN TblLocali AS L ON(MOF.IdLocale = L.IdLocali) " +
                                "JOIN TblGruppiFrutti AS GF ON(MOF.IdGruppiFrutti = GF.Id) " +
                                "JOIN TblCompGruppoFrut AS CGF ON(CGF.IdTblGruppo = GF.Id) " +
                                "JOIN TblFrutti AS F ON(CGF.IdTblFrutto = F.ID1) " +
                                "WHERE IdCantiere = @pIdCant " +
                                "GROUP BY F.descr001 " +
                                "UNION " +
                                "SELECT F.descr001 AS descr, SUM(MOF.QtaFrutti) AS Qta " +
                                "FROM TblMatOrdFrut AS MOF " +
                                "LEFT JOIN TblFrutti AS F ON MOF.IdFrutto = F.ID1 " +
                                "WHERE IdCantiere = @pIdCant AND MOF.idFrutto IS NOT NULL AND MOF.QtaFrutti IS NOT NULL " +
                                "GROUP BY F.descr001" +
                             ") AS A " +
                             "GROUP BY descr " +
                             "ORDER BY descr ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@pIdCant", idCant));
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    StampaOrdFrutCantLoc scLoc = new StampaOrdFrutCantLoc();
                    scLoc.Descr001 = (dr.IsDBNull(0) ? null : dr.GetString(0));
                    scLoc.Qta = (dr.IsDBNull(1) ? -1 : dr.GetInt32(1));

                    list.Add(scLoc);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la stampa dei gruppi in un locale.", ex);
            }
            finally { cn.Close(); }

        }
    }
}