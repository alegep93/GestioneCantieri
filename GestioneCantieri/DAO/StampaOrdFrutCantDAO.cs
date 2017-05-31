using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestioneCantieri.DAO
{
    public class StampaOrdFrutCantDAO : BaseDAO
    {
        public static List<StampaOrdFrutCant> getAllFruttiInCantiere(string idCant)
        {
            SqlConnection cn = GetConnection();
            List<StampaOrdFrutCant> list = new List<StampaOrdFrutCant>();
            SqlDataReader dr = null;
            try
            {
                string sql = "SELECT F.descr001, SUM(CGF.Qta) " +
                             "FROM TblMatOrdFrut AS MOF " +
                             "JOIN TblGruppiFrutti AS GF ON(MOF.IdGruppiFrutti = GF.Id) " +
                             "JOIN TblCompGruppoFrut AS CGF ON(CGF.IdTblGruppo = GF.Id) " +
                             "JOIN TblFrutti AS F ON(CGF.IdTblFrutto = F.ID1) " +
                             "WHERE IdCantiere = @pIdCant " +
                             "GROUP BY descr001 " +
                             "ORDER BY F.descr001 ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", idCant));
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    StampaOrdFrutCant sc = new StampaOrdFrutCant();
                    sc.DescrFrutto = (dr.IsDBNull(0) ? null : dr.GetString(0));
                    sc.Qta = (dr.IsDBNull(1) ? -1: dr.GetInt32(1));
                    list.Add(sc);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la stampa dei frutti di un cantiere.",ex);
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
                    Cantieri c = new Cantieri();
                    c.IdCantiere = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    c.CodCantiere = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    c.DescrCantiere = (dr.IsDBNull(2) ? null : dr.GetString(2));
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