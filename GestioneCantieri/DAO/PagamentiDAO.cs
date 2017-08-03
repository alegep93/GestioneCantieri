using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestioneCantieri.DAO
{
    public class PagamentiDAO : BaseDAO
    {
        //SELECT
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

                if (pag.Acconto == false)
                    cmd.Parameters.Add(new SqlParameter("pAcconto", DBNull.Value));
                if (pag.Saldo == false)
                    cmd.Parameters.Add(new SqlParameter("pSaldo", DBNull.Value));

                int row = cmd.ExecuteNonQuery();

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un arrotondamento", ex);
            }
            finally { cn.Close(); }
        }
    }
}