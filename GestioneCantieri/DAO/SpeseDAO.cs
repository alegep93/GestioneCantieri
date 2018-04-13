using GestioneCantieri.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace GestioneCantieri.DAO
{
    public class SpeseDAO : BaseDAO
    {
        //SELECT
        public static DataTable GetSpese()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT IdSpesa,Descrizione,Prezzo FROM TblSpese ORDER BY Descrizione";

                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero delle spese", ex);
            }
            finally { cn.Close(); }
        }
        public static DataTable GetSpeseFromDescr(string descr)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            descr = "%" + descr + "%";

            try
            {
                sql = "SELECT IdSpesa,Descrizione,Prezzo FROM TblSpese WHERE Descrizione LIKE @descr";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("descr", descr));
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero delle spese con filtro sulla descrizione", ex);
            }
            finally { cn.Close(); }
        }
        public static Spese GetDettagliSpesa(string idSpesa)
        {
            SqlConnection cn = GetConnection();
            Spese s = new Spese();
            SqlDataReader dr = null;
            string sql = "";

            try
            {
                sql = "SELECT IdSpesa, Descrizione,Prezzo FROM TblSpese where IdSpesa = @id ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("id", idSpesa));

                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    s.IdSpesa = (dr.IsDBNull(0) ? 0 : dr.GetInt32(0));
                    s.Descrizione = (dr.IsDBNull(1) ? "" : dr.GetString(1));
                    s.Prezzo = (dr.IsDBNull(2) ? 0.00m : dr.GetDecimal(2));
                }

                return s;
                
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero delle spese", ex);
            }
            finally { cn.Close(); dr.Close(); }
        }

        //INSERT
        public static bool InsertSpesa(Spese s)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblSpese (Descrizione, Prezzo) " +
                      "VALUES (@descr, @prezzo) ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("descr", s.Descrizione));
                cmd.Parameters.Add(new SqlParameter("prezzo", s.Prezzo));

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di una spesa", ex);
            }
            finally { cn.Close(); }
        }

        //UPDATE
        public static bool UpdateSpesa(string idSpesa, Spese s)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "UPDATE TblSpese " +
                      "SET Descrizione = @descr, Prezzo = @prezzo " +
                      "WHERE idSpesa = @id";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("id", idSpesa));
                cmd.Parameters.Add(new SqlParameter("descr", s.Descrizione));
                cmd.Parameters.Add(new SqlParameter("prezzo", s.Prezzo));

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'aggiornamento di una spesa", ex);
            }
            finally { cn.Close(); }
        }

        //DELETE
        public static bool DeleteSpesa(int idSpesa)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "DELETE FROM TblSpese WHERE idSpesa = @id";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("id", idSpesa));

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione di una spesa", ex);
            }
            finally { cn.Close(); }
        }
    }
}