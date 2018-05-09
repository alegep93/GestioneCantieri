using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GestioneCantieri.DAO
{
    public class FornitoriDAO : BaseDAO
    {
        public static DataTable GetFornitoriDataTable()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT IdFornitori,RagSocForni,Indirizzo,cap, " +
                      "Città,Tel1,Cell1,PartitaIva,CodFiscale,Abbreviato " +
                      "FROM TblForitori " +
                      "ORDER BY RagSocForni ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei fornitori", ex);
            }
            finally { cn.Close(); }
        }
        public static List<Fornitori> GetFornitori()
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            List<Fornitori> list = new List<Fornitori>();
            string sql = "";

            try
            {
                sql = "SELECT IdFornitori,RagSocForni,Indirizzo,cap, " +
                      "Città,Tel1,Cell1,PartitaIva,CodFiscale,Abbreviato " +
                      "FROM TblForitori " +
                      "ORDER BY RagSocForni ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Fornitori f = new Fornitori();
                    f.IdFornitori = (dr.IsDBNull(0) ? 0 : dr.GetInt32(0));
                    f.RagSocForni = (dr.IsDBNull(1) ? "" : dr.GetString(1));
                    f.Indirizzo = (dr.IsDBNull(2) ? "" : dr.GetString(2));
                    f.Cap = (dr.IsDBNull(3) ? "" : dr.GetString(3));
                    f.Città = (dr.IsDBNull(4) ? "" : dr.GetString(4));
                    f.Tel1 = (dr.IsDBNull(5) ? 0 : dr.GetInt32(5));
                    f.Cell1 = (dr.IsDBNull(6) ? 0 : dr.GetInt32(6));
                    f.PartitaIva = (dr.IsDBNull(7) ? 0 : dr.GetDouble(7));
                    f.CodFiscale = (dr.IsDBNull(8) ? "" : dr.GetString(8));
                    f.Abbreviato = (dr.IsDBNull(9) ? "" : dr.GetString(9));
                    list.Add(f);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei fornitori", ex);
            }
            finally { cn.Close(); }
        }
        public static int GetIdFornitore(string ragSoc)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            int id = -1;
            string sql = "";

            try
            {
                sql = "SELECT IdFornitori FROM TblForitori WHERE RagSocForni = @ragSoc";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@ragSoc", ragSoc));
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    id = dr.GetInt32(0);
                }

                return id;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dell'idFornitore", ex);
            }
            finally { cn.Close(); }
        }
        public static string GetRagSocFornitore(int id)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string ragSoc = "";
            string sql = "";

            try
            {
                sql = "SELECT RagSocForni FROM TblForitori WHERE IdFornitori = @id";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    ragSoc = dr.GetString(0);
                }

                return ragSoc;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero della Ragione Sociale del Fornitore " + id, ex);
            }
            finally { cn.Close(); }
        }
        public static DataTable GetFornitori(string ragSoc)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            ragSoc = "%" + ragSoc + "%";

            try
            {
                sql = "SELECT IdFornitori,RagSocForni,Indirizzo,cap, " +
                      "Città,Tel1,Cell1,PartitaIva,CodFiscale,Abbreviato " +
                      "FROM TblForitori " +
                      "WHERE RagSocForni LIKE @pRagSoc " +
                      "ORDER BY RagSocForni ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pRagSoc", ragSoc));
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei fornitori", ex);
            }
            finally { cn.Close(); }
        }
        public static Fornitori GetSingleFornitore(int idFornitore)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            Fornitori fornitore = new Fornitori();
            string sql = "";

            try
            {
                sql = "SELECT IdFornitori,RagSocForni,Indirizzo,cap, " +
                      "Città,Tel1,Cell1,PartitaIva,CodFiscale,Abbreviato " +
                      "FROM TblForitori " +
                      "WHERE IdFornitori = @pId " +
                      "ORDER BY RagSocForni ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pId", idFornitore));
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    fornitore.IdFornitori = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    fornitore.RagSocForni = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    fornitore.Indirizzo = (dr.IsDBNull(2) ? null : dr.GetString(2));
                    fornitore.Cap = (dr.IsDBNull(3) ? null : dr.GetString(3));
                    fornitore.Città = (dr.IsDBNull(4) ? null : dr.GetString(4));
                    fornitore.Tel1 = (dr.IsDBNull(5) ? -1 : dr.GetInt32(5));
                    fornitore.Cell1 = (dr.IsDBNull(6) ? -1 : dr.GetInt32(6));
                    fornitore.PartitaIva = (dr.IsDBNull(7) ? -1.0d : dr.GetDouble(7));
                    fornitore.CodFiscale = (dr.IsDBNull(8) ? null : dr.GetString(8));
                    fornitore.Abbreviato = (dr.IsDBNull(9) ? null : dr.GetString(9));
                }

                return fornitore;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero di un singolo operaio", ex);
            }
            finally { cn.Close(); dr.Close(); }
        }
        public static bool InserisciFornitore(Fornitori f)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblForitori " +
                      "(RagSocForni,Indirizzo,cap,Città,Tel1,Cell1, " +
                      "PartitaIva,CodFiscale,Abbreviato) " +
                      "VALUES (@pRagSoc, @pIndir, @pCap, @pCitta, @pTel, @pCel, @pPIva, @pCodFisc, @pAbbrev) ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pRagSoc", f.RagSocForni));
                cmd.Parameters.Add(new SqlParameter("pIndir", f.Indirizzo));
                cmd.Parameters.Add(new SqlParameter("pCap", f.Cap));
                cmd.Parameters.Add(new SqlParameter("pCitta", f.Città));
                cmd.Parameters.Add(new SqlParameter("pTel", f.Tel1));
                cmd.Parameters.Add(new SqlParameter("pCel", f.Cell1));
                cmd.Parameters.Add(new SqlParameter("pPIva", f.PartitaIva));
                cmd.Parameters.Add(new SqlParameter("pCodFisc", f.CodFiscale));
                cmd.Parameters.Add(new SqlParameter("pAbbrev", f.Abbreviato));
                int ret = cmd.ExecuteNonQuery();

                if (ret > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un nuovo Fornitore", ex);
            }
            finally { cn.Close(); }
        }
        public static bool UpdateFornitore(string idFornit, Fornitori f)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "UPDATE TblForitori " +
                      "SET RagSocForni = @pRagSoc, " +
                      "Indirizzo = @pIndir, " +
                      "cap = @pCap, " +
                      "Città = @pCitta, " +
                      "Tel1 = @pTel, " +
                      "Cell1 = @pCel, " +
                      "PartitaIva = @pPIva, " +
                      "CodFiscale = @pCodFisc, " +
                      "Abbreviato = @pAbbrev " +
                      "WHERE IdFornitori = @pId ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pRagSoc", f.RagSocForni));
                cmd.Parameters.Add(new SqlParameter("pIndir", f.Indirizzo));
                cmd.Parameters.Add(new SqlParameter("pCap", f.Cap));
                cmd.Parameters.Add(new SqlParameter("pCitta", f.Città));
                cmd.Parameters.Add(new SqlParameter("pTel", f.Tel1));
                cmd.Parameters.Add(new SqlParameter("pCel", f.Cell1));
                cmd.Parameters.Add(new SqlParameter("pPIva", f.PartitaIva));
                cmd.Parameters.Add(new SqlParameter("pCodFisc", f.CodFiscale));
                cmd.Parameters.Add(new SqlParameter("pAbbrev", f.Abbreviato));
                cmd.Parameters.Add(new SqlParameter("pId", idFornit));

                int row = cmd.ExecuteNonQuery();

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'update di un fornitore", ex);
            }
            finally { cn.Close(); }
        }
        public static bool EliminaFornitore(int idFornit)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "DELETE FROM TblForitori " +
                      "WHERE IdFornitori = @pId ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pId", idFornit));

                int row = cmd.ExecuteNonQuery();

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione di un fornitore", ex);
            }
            finally { cn.Close(); }
        }
    }
}