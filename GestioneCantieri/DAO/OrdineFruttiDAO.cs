using Dapper;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

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
                      "VALUES (@IdCantiere,@IdGruppiFrutti,@IdLocale) ";

                int rows = cn.Execute(sql, new { IdCantiere = idCantiere, IdGruppiFrutti = idGruppoFrutto, IdLocale = idLocale });

                if (rows > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un gruppo", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static bool InserisciFruttoNonInGruppo(string idCantiere, string idLocale, string idFrutto, string qtaFrutti)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblMatOrdFrut(IdCantiere,IdLocale,IdFrutto,QtaFrutti) " +
                      "VALUES (@IdCantiere,@IdLocale,@IdFrutto,@QtaFrutti) ";

                int rows = cn.Execute(sql, new { IdCantiere = idCantiere, IdLocale = idLocale, IdFrutto = idFrutto, QtaFrutti = qtaFrutti });

                if (rows > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un frutto non appartenente ad un gruppo", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static List<MatOrdFrut> getGruppi(string idCantiere, string idLocale)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT GF.NomeGruppo, GF.Descrizione " +
                      "FROM TblMatOrdFrut AS MOF " +
                      "JOIN TblGruppiFrutti AS GF ON (MOF.IdGruppiFrutti = GF.Id) " +
                      "WHERE IdCantiere = @IdCantiere AND IdLocale = @IdLocale " +
                      "ORDER BY NomeGruppo ASC ";

                return cn.Query<MatOrdFrut>(sql, new { IdCantiere = idCantiere, IdLocale = idLocale }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei gruppi", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static List<MatOrdFrut> GetFruttiNonInGruppo(string idCant, string idLocale)
        {
            SqlConnection cn = GetConnection();
            try
            {
                string sql = "select F.descr001, SUM(MOF.QtaFrutti) " +
                             "FROM TblMatOrdFrut AS MOF " +
                             "LEFT JOIN TblFrutti AS F ON(MOF.IdFrutto = F.ID1) " +
                             "where IdCantiere = @IdCantiere AND IdLocale = @IdLocale AND MOF.idFrutto IS NOT NULL AND MOF.QtaFrutti IS NOT NULL " +
                             "GROUP BY F.descr001 " +
                             "ORDER BY F.descr001 ASC ";

                return cn.Query<MatOrdFrut>(sql, new { IdCantiere = idCant, IdLocale = idLocale }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la stampa dei frutti in un locale.", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static bool DeleteGruppo(int idGruppo)
        {
            SqlConnection cn = GetConnection();
            string sql = "";
            bool ret = false;

            try
            {
                sql = "DELETE FROM TblMatOrdFrut WHERE IdGruppiFrutti = @IdGruppiFrutti";
                cn.Execute(sql, new { IdGruppiFrutti = idGruppo });
                ret = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione di un gruppo da un ordine", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }

            return ret;
        }
        public static bool DeleteItem(int itemId)
        {
            SqlConnection cn = GetConnection();
            string sql = "";
            bool ret = false;

            try
            {
                sql = "DELETE FROM TblMatOrdFrut WHERE Id = @Id";
                cn.Execute(sql, new { Id = itemId });
                ret = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione di un record da un ordine", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }

            return ret;
        }
        public static List<StampaOrdFrutCantLoc> GetAllGruppiInLocale(string idCant)
        {
            SqlConnection cn = GetConnection();

            try
            {
                string sql = "SELECT L.NomeLocale, GF.NomeGruppo, COUNT(Gf.NomeGruppo) AS 'Qta' " +
                             "FROM TblMatOrdFrut AS MOF " +
                             "JOIN TblLocali AS L ON(MOF.IdLocale = L.IdLocali) " +
                             "JOIN TblGruppiFrutti AS GF ON(MOF.IdGruppiFrutti = GF.Id) " +
                             "WHERE IdCantiere = @IdCantiere " +
                             "GROUP BY L.NomeLocale, GF.NomeGruppo " +
                             "ORDER BY NomeLocale ASC ";

                return cn.Query<StampaOrdFrutCantLoc>(sql, new { IdCantiere = idCant }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la stampa dei gruppi in un locale.", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }

        }
        public static List<StampaOrdFrutCantLoc> GetAllFruttiInLocale(string idCant)
        {
            SqlConnection cn = GetConnection();
            try
            {
                string sql = "SELECT F.descr001 AS 'Descr001', SUM(CGF.Qta) AS Qta " +
                             "FROM TblMatOrdFrut AS MOF " +
                             "JOIN TblLocali AS L ON(MOF.IdLocale = L.IdLocali) " +
                             "JOIN TblGruppiFrutti AS GF ON(MOF.IdGruppiFrutti = GF.Id) " +
                             "JOIN TblCompGruppoFrut AS CGF ON(CGF.IdTblGruppo = GF.Id) " +
                             "JOIN TblFrutti AS F ON(CGF.IdTblFrutto = F.ID1) " +
                             "WHERE IdCantiere = @IdCantiere " +
                             "GROUP BY F.descr001 " +
                             "ORDER BY F.descr001 ASC ";

                return cn.Query<StampaOrdFrutCantLoc>(sql, new { IdCantiere = idCant }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la stampa dei frutti in un locale.", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }

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
            finally
            {
                CloseResouces(cn, null);
            }

        }
        public static List<StampaOrdFrutCantLoc> GetAllFruttiNonInGruppo(string idCant)
        {
            SqlConnection cn = GetConnection();

            try
            {
                string sql = "SELECT F.descr001 AS 'Descr001', SUM(MOF.QtaFrutti) AS Qta " +
                             "FROM TblMatOrdFrut AS MOF " +
                             "LEFT JOIN TblFrutti AS F ON(MOF.IdFrutto = F.ID1) " +
                             "where IdCantiere = @IdCantiere AND MOF.idFrutto IS NOT NULL AND MOF.QtaFrutti IS NOT NULL " +
                             "GROUP BY F.descr001 " +
                             "ORDER BY F.descr001 ASC ";

                return cn.Query<StampaOrdFrutCantLoc>(sql, new { IdCantiere = idCant }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la stampa dei frutti in un locale.", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }

        }
        public static List<StampaOrdFrutCantLoc> GetFruttiPerStampaExcel(string idCant)
        {
            SqlConnection cn = GetConnection();

            try
            {
                string sql = "SELECT descr AS 'descr001', SUM(Qta) AS 'Qta' FROM (" +
                                "SELECT F.descr001 AS descr, SUM(CGF.Qta) As Qta " +
                                "FROM TblMatOrdFrut AS MOF " +
                                "JOIN TblLocali AS L ON(MOF.IdLocale = L.IdLocali) " +
                                "JOIN TblGruppiFrutti AS GF ON(MOF.IdGruppiFrutti = GF.Id) " +
                                "JOIN TblCompGruppoFrut AS CGF ON(CGF.IdTblGruppo = GF.Id) " +
                                "JOIN TblFrutti AS F ON(CGF.IdTblFrutto = F.ID1) " +
                                "WHERE IdCantiere = @IdCantiere " +
                                "GROUP BY F.descr001 " +
                                "UNION " +
                                "SELECT F.descr001 AS descr, SUM(MOF.QtaFrutti) AS Qta " +
                                "FROM TblMatOrdFrut AS MOF " +
                                "LEFT JOIN TblFrutti AS F ON MOF.IdFrutto = F.ID1 " +
                                "WHERE IdCantiere = @IdCantiere AND MOF.idFrutto IS NOT NULL AND MOF.QtaFrutti IS NOT NULL " +
                                "GROUP BY F.descr001" +
                             ") AS A " +
                             "GROUP BY descr " +
                             "ORDER BY descr ";

                return cn.Query<StampaOrdFrutCantLoc>(sql, new { IdCantiere = idCant }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la stampa dei gruppi in un locale.", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }

        }
        public static List<MatOrdFrut> GetInfoForCantiereAndLocale(string idCant, string idLocale)
        {
            SqlConnection cn = GetConnection();

            try
            {
                string sql = "SELECT A.id, B.DescriCodCAnt 'DescrCant', C.NomeLocale 'Appartamento', D.NomeGruppo, E.descr001 'NomeFrutto', A.QtaFrutti " +
                             "FROM TblMatOrdFrut AS A " +
                             "LEFT JOIN TblCantieri AS B ON A.IdCantiere = B.IdCantieri " +
                             "LEFT JOIN TblLocali AS C ON A.IdLocale = C.IdLocali " +
                             "LEFT JOIN TblGruppiFrutti AS D ON A.IdGruppiFrutti = D.Id " +
                             "LEFT JOIN TblFrutti AS E ON A.IdFrutto = E.ID1 " +
                             "WHERE B.IdCantieri = @IdCantieri AND C.IdLocali = @IdLocali";

                return cn.Query<MatOrdFrut>(sql, new { IdCantieri = idCant, IdLocali = idLocale }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero delle informazioni dei gruppiFrutti per un locale di un cantiere", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
    }
}