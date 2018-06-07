using Dapper;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace GestioneCantieri.DAO
{
    public class CompGruppoFrutDAO : BaseDAO
    {
        // SELECT
        public static List<CompGruppoFrut> getCompGruppo(int idGruppo)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT CGF.Id, F.descr001 'NomeFrutto', Qta " +
                      "FROM TblCompGruppoFrut AS CGF " +
                      "JOIN TblFrutti AS F ON (CGF.IdTblFrutto = F.ID1) " +
                      "WHERE IdTblGruppo = @IdTblGruppo " +
                      "ORDER BY CGF.Id ASC ";

                return cn.Query<CompGruppoFrut>(sql, new { IdTblGruppo = idGruppo }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei componeti del gruppo", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static List<StampaFruttiPerGruppi> GetFruttiInGruppi(string idGruppo)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT GF.NomeGruppo, F.descr001 'NomeFrutto', CGF.Qta " +
                      "FROM TblCompGruppoFrut AS CGF " +
                      "JOIN TblGruppiFrutti AS GF ON(CGF.IdTblGruppo = GF.Id) " +
                      "JOIN TblFrutti AS F ON(CGF.IdTblFrutto = F.ID1) ";

                if (idGruppo != null && idGruppo != "")
                    sql += "WHERE Gf.Id = " + idGruppo + " ";

                sql += "GROUP BY GF.NomeGruppo, F.descr001, CGF.Qta " +
                       "ORDER BY GF.NomeGruppo ";

                return cn.Query<StampaFruttiPerGruppi>(sql).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la selezione dei frutti nei vari gruppi", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }

        // INSERT
        public static bool InserisciCompGruppo(int gruppo, int frutto, string qta)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblCompGruppoFrut(IdTblGruppo,IdTblFrutto,Qta) VALUES (@IdTblGruppo,@IdTblFrutto,@Qta) ";

                int rowNumber = cn.Execute(sql, new { IdTblGruppo = gruppo, IdTblFrutto = frutto, Qta = qta });

                if (rowNumber > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un frutto in un gruppo", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }

        // DELETE
        public static bool DeleteGruppo(int idGruppo)
        {
            SqlConnection cn = GetConnection();
            string sql = "";
            bool ret = false;

            try
            {
                sql = "DELETE FROM TblCompGruppoFrut WHERE IdTblGruppo = @IdTblGruppo";
                cn.Execute(sql, new { IdTblGruppo = idGruppo });
                ret = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione di un gruppo dalla CompGruppoFrut", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }

            return ret;
        }
        public static bool DeleteCompGruppo(int idCompGruppo)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "DELETE FROM TblCompGruppoFrut WHERE Id = @Id ";

                int rowNumber = cn.Execute(sql, new { Id = idCompGruppo });

                if (rowNumber > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione di un componente di un gruppo frutto", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
    }
}