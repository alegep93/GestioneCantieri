using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestioneCantieri.DAO
{
    public class StampaFruttiPerGruppiDAO : BaseDAO
    {
        public static List<StampaFruttiPerGruppi> GetFruttiInGruppi(string idGruppo)
        {
            List<StampaFruttiPerGruppi> list = new List<StampaFruttiPerGruppi>();
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string sql = "";

            try
            {
                sql = "SELECT GF.NomeGruppo, F.descr001, CGF.Qta " +
                      "FROM TblCompGruppoFrut AS CGF " +
                      "JOIN TblGruppiFrutti AS GF ON(CGF.IdTblGruppo = GF.Id) " +
                      "JOIN TblFrutti AS F ON(CGF.IdTblFrutto = F.ID1) ";

                if (idGruppo != null && idGruppo != "")
                    sql += "WHERE Gf.Id = " + idGruppo + " ";

                sql += "GROUP BY GF.NomeGruppo, F.descr001, CGF.Qta " +
                       "ORDER BY GF.NomeGruppo ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    StampaFruttiPerGruppi fpg = new StampaFruttiPerGruppi();
                    fpg.NomeGruppo = (dr.IsDBNull(0) ? null : dr.GetString(0));
                    fpg.NomeFrutto = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    fpg.Qta = (dr.IsDBNull(2) ? -1 : dr.GetInt32(2));

                    list.Add(fpg);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la selezione dei frutti nei vari gruppi", ex);
            }
        }
        public static List<GruppiFrutti> getGruppi()
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string sql = "";
            List<GruppiFrutti> gruppiFruttiList = new List<GruppiFrutti>();

            try
            {
                sql = "SELECT Id, NomeGruppo FROM TblGruppiFrutti " +
                      "ORDER BY NomeGruppo ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    GruppiFrutti gf = new GruppiFrutti();
                    gf.Id = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    gf.NomeGruppo = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    gruppiFruttiList.Add(gf);
                }

                return gruppiFruttiList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei gruppi", ex);
            }
            finally { cn.Close(); }
        }
    }
}