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