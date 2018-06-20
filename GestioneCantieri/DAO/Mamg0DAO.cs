using Dapper;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace GestioneCantieri.DAO
{
    public class Mamg0DAO : BaseDAO
    {
        public static List<Mamg0> getAll()
        {
            string sql = "";
            SqlConnection cn = GetConnection();

            try
            {
                sql = "SELECT TOP 500 (AA_SIGF + AA_CODF) AS CodArt, AA_DES AS 'Desc', AA_UM AS UnitMis, AA_PZ AS Pezzo, AA_PRZ AS PrezzoListino, " +
                      "AA_SCONTO1 AS Sconto1, AA_SCONTO2 AS Sconto2, AA_SCONTO3 AS Sconto3, AA_PRZ1 AS PrezzoNetto " +
                      "FROM MAMG0 " +
                      "ORDER BY CodArt ASC ";

                return cn.Query<Mamg0>(sql).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero del listino", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static List<Mamg0> GetListino(string codArt1, string desc1)
        {
            string sql = "";
            SqlConnection cn = GetConnection();

            codArt1 = "%" + codArt1 + "%";
            desc1 = "%" + desc1 + "%";

            try
            {
                if (codArt1 == "%%" && desc1 == "%%")
                {
                    sql = "SELECT TOP 300 (AA_SIGF + AA_CODF) AS CodArt, AA_DES AS 'desc', AA_UM AS unitMis, AA_PZ AS pezzo, AA_PRZ AS prezzoListino, " +
                          "AA_SCONTO1 AS sconto1, AA_SCONTO2 AS sconto2, AA_SCONTO3 AS sconto3, AA_PRZ1 AS prezzoNetto " +
                          "FROM MAMG0 " +
                          "ORDER BY CodArt ASC ";
                }
                else
                {
                    sql = "SELECT (AA_SIGF + AA_CODF) AS CodArt, AA_DES AS 'desc', AA_UM AS unitMis, AA_PZ AS pezzo, AA_PRZ AS prezzoListino, " +
                          "AA_SCONTO1 AS sconto1, AA_SCONTO2 AS sconto2, AA_SCONTO3 AS sconto3, AA_PRZ1 AS prezzoNetto " +
                          "FROM MAMG0 " +
                          "WHERE (AA_SIGF + AA_CODF) LIKE @CodArt " +
                          "AND AA_DES LIKE @desc " +
                          "ORDER BY CodArt ASC ";
                }

                return cn.Query<Mamg0>(sql, new { CodArt = codArt1, desc = desc1 }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero del listino con i filtri", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static List<Mamg0> GetListino(string codArt1, string codArt2, string codArt3, string desc1, string desc2, string desc3)
        {
            string sql = "";
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
                    sql = "SELECT TOP 500 (AA_SIGF + AA_CODF) AS CodArt, AA_DES AS 'Desc', AA_UM AS UnitMis, AA_PZ AS Pezzo, AA_PRZ AS PrezzoListino, " +
                          "AA_SCONTO1 AS Sconto1, AA_SCONTO2 AS Sconto2, AA_SCONTO3 AS Sconto3, AA_PRZ1 AS PrezzoNetto " +
                          "FROM MAMG0 " +
                          "ORDER BY CodArt ASC ";
                }
                else
                {
                    sql = "SELECT (AA_SIGF + AA_CODF) AS CodArt, AA_DES AS 'Desc', AA_UM AS UnitMis, AA_PZ AS Pezzo, AA_PRZ AS PrezzoListino, " +
                          "AA_SCONTO1 AS Sconto1, AA_SCONTO2 AS Sconto2, AA_SCONTO3 AS Sconto3, AA_PRZ1 AS PrezzoNetto " +
                          "FROM MAMG0 " +
                          "WHERE (AA_SIGF + AA_CODF) LIKE @pCodArt1 AND (AA_SIGF + AA_CODF) LIKE @pCodArt2 AND (AA_SIGF + AA_CODF) LIKE @pCodArt3 " +
                          "AND AA_DES LIKE @pDescriCodArt1 AND AA_DES LIKE @pDescriCodArt2 AND AA_DES LIKE @pDescriCodArt3 " +
                          "ORDER BY CodArt ASC ";
                }

                return cn.Query<Mamg0>(sql, new { pCodArt1 = codArt1, pCodArt2 = codArt2, pCodArt3 = codArt3, pDescriCodArt1 = desc1, pDescriCodArt2 = desc2, pDescriCodArt3 = desc3 }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero del listino con i filtri", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }

        //DELETE
        public static bool EliminaListino()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "DELETE FROM MAMG0";

                int rows = cn.Execute(sql);

                if (rows > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione del listino", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
    }
}