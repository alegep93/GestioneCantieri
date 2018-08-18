using Dapper;
using GestioneCantieri.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

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
                sql = "SELECT IdSpesa,Descrizione,Prezzo FROM TblSpese WHERE Descrizione LIKE @Descrizione";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@Descrizione", descr));
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
            string sql = "";

            try
            {
                sql = "SELECT IdSpesa, Descrizione, Prezzo FROM TblSpese where IdSpesa = @IdSpesa ";
                return cn.Query<Spese>(sql, new { IdSpesa = idSpesa }).SingleOrDefault();

            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero delle spese", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }

        //INSERT
        public static bool InsertSpesa(Spese s)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblSpese (Descrizione, Prezzo) " +
                      "VALUES (@Descrizione, @Prezzo) ";

                int rows = cn.Execute(sql, s);

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
        public static bool UpdateSpesa(Spese s)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "UPDATE TblSpese " +
                      "SET Descrizione = @Descrizione, Prezzo = @Prezzo " +
                      "WHERE idSpesa = @idSpesa";

                int rows = cn.Execute(sql, s);

                if (rows > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'aggiornamento di una spesa", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }

        //DELETE
        public static bool DeleteSpesa(int id)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "DELETE FROM TblSpese WHERE idSpesa = @idSpesa";

                int rows = cn.Execute(sql, new { idSpesa = id });

                if (rows > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione di una spesa", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
    }
}