using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GestioneCantieri.DAO
{
    public class CompGruppoFrutDAO : BaseDAO
    {
        public static List<CompGruppoFrut> getCompGruppo(int idGruppo)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string sql = "";
            List<CompGruppoFrut> compList = new List<CompGruppoFrut>();

            try
            {
                sql = "SELECT CGF.Id,F.descr001,Qta " +
                      "FROM TblCompGruppoFrut AS CGF " +
                      "JOIN TblFrutti AS F ON (CGF.IdTblFrutto = F.ID1) " +
                      "WHERE IdTblGruppo = @pIdGruppo " +
                      "ORDER BY CGF.Id ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdGruppo", idGruppo));
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    CompGruppoFrut cgf = new CompGruppoFrut();
                    cgf.Id = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    cgf.NomeFrutto = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    cgf.Qta = (dr.IsDBNull(2) ? -1 : dr.GetInt32(2));
                    compList.Add(cgf);
                }

                return compList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei componeti del gruppo", ex);
            }
            finally { cn.Close(); }
        }

        public static bool InserisciCompGruppo(int gruppo, int frutto, string qta)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblCompGruppoFrut(IdTblGruppo,IdTblFrutto,Qta) VALUES (@pGruppo,@pFrutto,@pQta) ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pGruppo", gruppo));
                cmd.Parameters.Add(new SqlParameter("pFrutto", frutto));
                cmd.Parameters.Add(new SqlParameter("pQta", qta));

                int rowNumber = cmd.ExecuteNonQuery();

                if (rowNumber > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un frutto in un gruppo", ex);
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
                sql = "DELETE FROM TblCompGruppoFrut WHERE IdTblGruppo = @pId";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pId", idGruppo));

                cmd.ExecuteNonQuery();

                ret = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione di un gruppo dalla CompGruppoFrut", ex);
            }
            finally { cn.Close(); }

            return ret;
        }

        public static bool DeleteCompGruppo(int idCompGruppo)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "DELETE FROM TblCompGruppoFrut WHERE Id = @pId ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pId", idCompGruppo));

                int rowNumber = cmd.ExecuteNonQuery();

                if (rowNumber > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione di un componente di un gruppo frutto", ex);
            }
            finally { cn.Close(); }
        }

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
    }
}