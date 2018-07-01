using Dapper;
using GestioneCantieri.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace GestioneCantieri.DAO
{
    public class OperaiDAO : BaseDAO
    {
        public static Operai GetOperaio(string id)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT IdOperaio,NomeOp,DescrOp,Suffisso,Operaio,CostoOperaio " +
                      "FROM TblOperaio " +
                      "WHERE IdOperaio = @IdOperaio " +
                      "ORDER BY NomeOp ASC ";

                return cn.Query<Operai>(sql, new { IdOperaio = id }).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero degli operai", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static DataTable GetOperai()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT IdOperaio,NomeOp,DescrOP,Suffisso,Operaio,CostoOperaio " +
                      "FROM TblOperaio " +
                      "ORDER BY NomeOp ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero degli operai", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static DataTable GetAllOperai()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT IdOperaio,NomeOp,DescrOP,Suffisso,Operaio,CostoOperaio " +
                      "FROM TblOperaio " +
                      "ORDER BY NomeOp ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero degli operai", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static Operai GetSingleOperaio(int idOperaio)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT IdOperaio,NomeOp,DescrOp,Suffisso,Operaio,CostoOperaio " +
                      "FROM TblOperaio " +
                      "WHERE IdOperaio = @IdOperaio " +
                      "ORDER BY NomeOp ASC ";

                return cn.Query<Operai>(sql, new { IdOperaio = idOperaio }).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero di un singolo operaio", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static string GetIdAcquirente(string NomeOperaio)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT IdOperaio " +
                      "FROM TblOperaio " +
                      "WHERE NomeOp = @NomeOp ";

                return cn.Query<int>(sql, new { NomeOp = NomeOperaio }).SingleOrDefault().ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero di un singolo operaio", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static bool InserisciOperaio(string nome, string descr, string suff, string operaio, string costoOp)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblOperaio " +
                      "(NomeOp, DescrOp, Suffisso, Operaio, CostoOperaio) " +
                      "VALUES (@NomeOp,@DescrOp,@Suffisso,@Operaio,@CostoOperaio) ";

                int ret = cn.Execute(sql, new { NomeOp = nome, DescrOp = descr, Suffisso = suff, Operaio = operaio, CostoOperaio = costoOp });

                if (ret > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un nuovo operaio", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static bool UpdateOperaio(string idOper, string nome, string descr, string suff, string oper, string costoOp)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "UPDATE TblOperaio " +
                      "SET NomeOp = @NomeOp, " +
                      "DescrOp = @DescrOp, " +
                      "Suffisso = @Suffisso, " +
                      "Operaio = @Operaio, " +
                      "CostoOperaio = @CostoOperaio " +
                      "WHERE IdOperaio = @IdOperaio ";

                int row = cn.Execute(sql, new { IdOperaio = idOper, NomeOp = nome, DescrOp = descr, Suffisso = suff, Operaio = oper, CostoOperaio = costoOp });

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'update di un operaio", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static bool EliminaOperaio(int idOper)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "DELETE FROM TblOperaio " +
                      "WHERE IdOperaio = @IdOperaio ";

                int row = cn.Execute(sql, new { IdOperaio = idOper });

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione dell'operaio", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
    }
}