using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

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
    }
}