using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestioneCantieri.DAO
{
    public class StampaOrdFrutCantLocDAO : BaseDAO
    {
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
                    Cantieri c = new Cantieri()
                    {
                        IdCantieri = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0)),
                        CodCant = (dr.IsDBNull(1) ? null : dr.GetString(1)),
                        DescriCodCAnt = (dr.IsDBNull(2) ? null : dr.GetString(2))
                    };
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
    }
}