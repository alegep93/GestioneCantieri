using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GestioneCantieri.DAO
{
    public class PagamentiDAO : BaseDAO
    {
        //SELECT
        public static Pagamenti GetSinglePagamento(int idPagam)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            Pagamenti p = new Pagamenti();
            string sql = "";

            try
            {
                sql = "SELECT IdTblCantieri,data,Imporo,DescriPagamenti,Acconto,Saldo " +
                      "FROM TblPagamenti " +
                      "WHERE IdPagamenti = @pId ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pId", idPagam));

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    p.IdTblCantieri = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    p.Data = (dr.IsDBNull(1) ? new DateTime() : dr.GetDateTime(1));
                    p.Imporo = (dr.IsDBNull(2) ? 0m : dr.GetDecimal(2));
                    p.DescriPagamenti = (dr.IsDBNull(3) ? "" : dr.GetString(3));
                    p.Acconto = (dr.IsDBNull(4) ? false : dr.GetBoolean(4));
                    p.Saldo = (dr.IsDBNull(5) ? false : dr.GetBoolean(5));
                }

                return p;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero del singolo pagamento", ex);
            }
            finally { cn.Close(); dr.Close(); }
        }
        public static List<Pagamenti> GetPagamenti(string idCant)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            List<Pagamenti> pagList = new List<Pagamenti>();
            string sql = "";

            try
            {
                sql = "SELECT IdTblCantieri,data,Imporo,DescriPagamenti,Acconto,Saldo " +
                      "FROM TblPagamenti " +
                      "WHERE IdTblCantieri = @pIdCant ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", idCant));

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Pagamenti p = new Pagamenti();
                    p.IdTblCantieri = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    p.Data = (dr.IsDBNull(1) ? new DateTime() : dr.GetDateTime(1));
                    p.Imporo = (dr.IsDBNull(2) ? 0m : dr.GetDecimal(2));
                    p.DescriPagamenti = (dr.IsDBNull(3) ? null : dr.GetString(3));
                    p.Acconto = (dr.IsDBNull(4) ? false : dr.GetBoolean(4));
                    p.Saldo = (dr.IsDBNull(5) ? false : dr.GetBoolean(5));

                    pagList.Add(p);
                }

                return pagList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei pagamenti", ex);
            }
            finally { cn.Close(); dr.Close(); }
        }
        public static List<Pagamenti> GetPagamenti(string idCant, string descrizione)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            List<Pagamenti> pagList = new List<Pagamenti>();
            string sql = "";

            descrizione = "%" + descrizione + "%";

            try
            {
                sql = "SELECT IdPagamenti,IdTblCantieri,data,Imporo,DescriPagamenti,Acconto,Saldo " +
                      "FROM TblPagamenti " +
                      "WHERE IdTblCantieri = @idCant AND ISNULL(DescriPagamenti,'') LIKE @descrizione ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("idCant", idCant));
                cmd.Parameters.Add(new SqlParameter("descrizione", descrizione));

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Pagamenti p = new Pagamenti();
                    p.IdPagamenti = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    p.IdTblCantieri = (dr.IsDBNull(1) ? -1 : dr.GetInt32(1));
                    p.Data = (dr.IsDBNull(2) ? new DateTime() : dr.GetDateTime(2));
                    p.Imporo = (dr.IsDBNull(3) ? 0m : dr.GetDecimal(3));
                    p.DescriPagamenti = (dr.IsDBNull(4) ? "" : dr.GetString(4));
                    p.Acconto = (dr.IsDBNull(5) ? false : dr.GetBoolean(5));
                    p.Saldo = (dr.IsDBNull(6) ? false : dr.GetBoolean(6));

                    pagList.Add(p);
                }

                return pagList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei pagamenti", ex);
            }
            finally { cn.Close(); dr.Close(); }
        }

        //INSERT
        public static bool InserisciPagamento(Pagamenti pag)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblPagamenti (IdTblCantieri,data,Imporo,DescriPagamenti,Acconto,Saldo) " +
                      "VALUES (@pIdCant,@pData,@pImporo,@pDescrPag,@pAcconto,@pSaldo)";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", pag.IdTblCantieri));
                cmd.Parameters.Add(new SqlParameter("pData", pag.Data));
                cmd.Parameters.Add(new SqlParameter("pImporo", pag.Imporo));
                cmd.Parameters.Add(new SqlParameter("pDescrPag", pag.DescriPagamenti));
                cmd.Parameters.Add(new SqlParameter("pAcconto", pag.Acconto));
                cmd.Parameters.Add(new SqlParameter("pSaldo", pag.Saldo));

                int row = cmd.ExecuteNonQuery();

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un pagamento", ex);
            }
            finally { cn.Close(); }
        }

        //UPDATE
        public static bool UpdatePagamento(string idPagam, Pagamenti pag)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "UPDATE TblPagamenti " +
                      "SET IdTblCantieri = @pIdCant, data = @pData, Imporo = @pImporo, " +
                      "DescriPagamenti = @pDescrPag, Acconto = @pAcconto, Saldo = @pSaldo " +
                      "WHERE IdPagamenti = @id";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", pag.IdTblCantieri));
                cmd.Parameters.Add(new SqlParameter("pData", pag.Data));
                cmd.Parameters.Add(new SqlParameter("pImporo", pag.Imporo));
                cmd.Parameters.Add(new SqlParameter("pDescrPag", pag.DescriPagamenti));
                cmd.Parameters.Add(new SqlParameter("pAcconto", pag.Acconto));
                cmd.Parameters.Add(new SqlParameter("pSaldo", pag.Saldo));
                cmd.Parameters.Add(new SqlParameter("id", idPagam));

                int row = cmd.ExecuteNonQuery();

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la modifica di un pagamento", ex);
            }
            finally { cn.Close(); }
        }

        //DELETE
        public static bool DeletePagamento(int idPagam)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "DELETE FROM TblPagamenti WHERE IdPagamenti = @id ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("id", idPagam));

                int row = cmd.ExecuteNonQuery();

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione di un pagamento", ex);
            }
            finally { cn.Close(); }
        }
    }
}