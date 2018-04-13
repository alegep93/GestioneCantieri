using GestioneCantieri.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace GestioneCantieri.DAO
{
    public class OperaiDAO : BaseDAO
    {
        public static Operai GetOperaio(string id)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            Operai op = new Operai();
            string sql = "";

            try
            {
                sql = "SELECT IdOperaio,NomeOp,DescrOP,Suffisso,Operaio,CostoOperaio " +
                      "FROM TblOperaio " +
                      "WHERE IdOperaio = @id " +
                      "ORDER BY NomeOp ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("id", id));
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    op.IdOperaio = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    op.NomeOp = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    op.DescrOp = (dr.IsDBNull(2) ? null : dr.GetString(2));
                    op.Suffisso = (dr.IsDBNull(3) ? null : dr.GetString(3));
                    op.Operaio = (dr.IsDBNull(4) ? null : dr.GetString(4));
                    op.CostoOperaio = (dr.IsDBNull(5) ? 0.0m : dr.GetDecimal(5));
                }

                return op;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero degli operai", ex);
            }
            finally { cn.Close(); }
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
            finally { cn.Close(); }
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
            finally { cn.Close(); }
        }
        public static Operai GetSingleOperaio(int idOperaio)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            Operai op = new Operai();
            string sql = "";

            try
            {
                sql = "SELECT IdOperaio,NomeOp,DescrOP,Suffisso,Operaio,CostoOperaio " +
                      "FROM TblOperaio " +
                      "WHERE IdOperaio = @pId " +
                      "ORDER BY NomeOp ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pId", idOperaio));
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    op.IdOperaio = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    op.NomeOp = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    op.DescrOp = (dr.IsDBNull(2) ? null : dr.GetString(2));
                    op.Suffisso = (dr.IsDBNull(3) ? null : dr.GetString(3));
                    op.Operaio = (dr.IsDBNull(4) ? null : dr.GetString(4));
                    op.CostoOperaio = (dr.IsDBNull(5) ? 0.0m : dr.GetDecimal(5));
                }

                return op;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero di un singolo operaio", ex);
            }
            finally { cn.Close(); dr.Close(); }
        }
        public static bool InserisciOperaio(string nome, string descr, string suff, string operaio, string costoOp)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblOperaio " +
                      "(NomeOp, DescrOP, Suffisso, Operaio, CostoOperaio) " +
                      "VALUES (@pNome,@pDescr,@pSuff,@pOperaio,@pCostoOp) ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@pNome", nome));
                cmd.Parameters.Add(new SqlParameter("@pDescr", descr));
                cmd.Parameters.Add(new SqlParameter("@pSuff", suff));
                cmd.Parameters.Add(new SqlParameter("@pOperaio", operaio));
                cmd.Parameters.Add(new SqlParameter("@pCostoOp", costoOp));

                int ret = cmd.ExecuteNonQuery();

                if (ret > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un nuovo operaio", ex);
            }
            finally { cn.Close(); }
        }
        public static bool UpdateOperaio(string idOper, string nome, string descr, string suff, string oper, string costoOp)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "UPDATE TblOperaio " +
                      "SET NomeOp = @pNome, " +
                      "DescrOP = @pDescr, " +
                      "Suffisso = @pSuff, " +
                      "Operaio = @pOper, " +
                      "CostoOperaio = @pCostoOp " +
                      "WHERE IdOperaio = @pId ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pNome", nome));
                cmd.Parameters.Add(new SqlParameter("pDescr", descr));
                cmd.Parameters.Add(new SqlParameter("pSuff", suff));
                cmd.Parameters.Add(new SqlParameter("pOper", oper));
                cmd.Parameters.Add(new SqlParameter("pCostoOp", costoOp));
                cmd.Parameters.Add(new SqlParameter("pId", idOper));

                int row = cmd.ExecuteNonQuery();

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'update di un operaio", ex);
            }
            finally { cn.Close(); }
        }
        public static bool EliminaOperaio(int idOper)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "DELETE FROM TblOperaio " +
                      "WHERE IdOperaio = @pId ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pId", idOper));

                int row = cmd.ExecuteNonQuery();

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione dell'operaio", ex);
            }
            finally { cn.Close(); }
        }
    }
}