using Dapper;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace GestioneCantieri.DAO
{
    public class FruttiDAO : BaseDAO
    {
        public static bool InserisciFrutto(string nomeFrutto)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "IF NOT EXISTS (SELECT descr001 FROM TblFrutti WHERE descr001 = @pNomeFrutto) " +
                        "INSERT INTO TblFrutti(descr001) VALUES (@pNomeFrutto) ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pNomeFrutto", nomeFrutto));

                int rowNumber = cn.Execute(sql, new { pNomeFrutto = nomeFrutto });
                if (rowNumber > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un frutto", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static List<Frutti> getFrutti()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT ID1 AS 'Id', descr001 AS 'Descr' FROM TblFrutti " +
                      "ORDER BY descr001 ASC ";

                return cn.Query<Frutti>(sql).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei frutti", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static List<Frutti> getFruttiWithSearch(string f1, string f2, string f3)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            f1 = "%" + f1 + "%";
            f2 = "%" + f2 + "%";
            f3 = "%" + f3 + "%";

            try
            {
                sql = "SELECT ID1 AS 'Id',descr001 AS 'Descr' FROM TblFrutti " +
                      "WHERE descr001 LIKE @pF1 AND descr001 LIKE @pF2 AND descr001 LIKE @pF3 " +
                      "ORDER BY descr001 ASC ";

                return cn.Query<Frutti>(sql, new { pF1 = f1, pF2 = f2, pF3 = f3 }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la ricerca dei frutti", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
    }
}