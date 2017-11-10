using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestioneCantieri.DAO
{
    public class RicalcoloContiStampaPdfDAO : BaseDAO
    {
        public static List<RicalcoloContiStampaPDF> GetRicalcoloContiStampaPDF()
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            List<RicalcoloContiStampaPDF> rcsList = new List<RicalcoloContiStampaPDF>();
            string sql = "";

            try
            {
                sql = "SELECT Data_O_Note,DescriCodArt,Qta,PzzoUniCantiere,Valore " +
                      "FROM TblRicalcoloContiStampaPDF ";

                SqlCommand cmd = new SqlCommand(sql, cn);

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    RicalcoloContiStampaPDF rcs = new RicalcoloContiStampaPDF();
                    rcs.Data_O_Note = (dr.IsDBNull(0) ? null : dr.GetString(0));
                    rcs.DescrCodArt = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    rcs.Qta = (dr.IsDBNull(2) ? -1.0d : dr.GetDouble(2));
                    rcs.PzzoUniCantiere = (dr.IsDBNull(3) ? -1.0m : dr.GetDecimal(3));
                    rcs.Valore = (dr.IsDBNull(4) ? -1.0m : dr.GetDecimal(4));

                    rcsList.Add(rcs);
                }

                return rcsList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei record della tabella RicalcoloContiStampPDF", ex);
            }
            finally { cn.Close(); dr.Close(); }
        }

        public static DataTable GetRicalcoloContiStampaPDFDataTable()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT Data_O_Note,DescriCodArt,Qta,PzzoUniCantiere,Valore " +
                      "FROM TblRicalcoloContiStampaPDF ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei record della tabella RicalcoloContiStampPDF", ex);
            }
            finally { cn.Close(); }
        }

        public static bool InserisciRicalcoloContiStampaPDF(RicalcoloContiStampaPDF rcs)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblRicalcoloContiStampaPDF (Data_O_Note,DescriCodArt,Qta,PzzoUniCantiere,Valore) " +
                      "VALUES (@data_note,@descriCodArt,@qta,@pzzoUniCant,@val) ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@data_note", rcs.Data_O_Note));
                cmd.Parameters.Add(new SqlParameter("@descriCodArt", rcs.DescrCodArt));
                cmd.Parameters.Add(new SqlParameter("@qta", rcs.Qta));
                cmd.Parameters.Add(new SqlParameter("@pzzoUniCant", rcs.PzzoUniCantiere));
                cmd.Parameters.Add(new SqlParameter("@val", rcs.Valore));
                int row = cmd.ExecuteNonQuery();

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei record della tabella RicalcoloContiStampPDF", ex);
            }
            finally { cn.Close(); }
        }

        public static bool EliminaRicalcoloContiStampaPDF()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "DELETE FROM TblRicalcoloContiStampaPDF ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                int row = cmd.ExecuteNonQuery();

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei record della tabella RicalcoloContiStampPDF", ex);
            }
            finally { cn.Close(); }
        }
    }
}