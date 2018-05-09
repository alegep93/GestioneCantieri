using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GestioneCantieri.DAO
{
    public class ClientiDAO : BaseDAO
    {
        //SELECT
        public static List<Clienti> GetClienti(string filtro)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            List<Clienti> list = new List<Clienti>();
            string sql = "";

            filtro = "%" + filtro + "%";

            try
            {
                sql = "SELECT IdCliente, RagSocCli " +
                      "FROM TblClienti " +
                      "WHERE RagSocCli LIKE @filtro ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("filtro", filtro));
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Clienti c = new Clienti();
                    c.IdCliente = dr.GetInt32(0);
                    c.RagSocCli = dr.IsDBNull(1) ? "" : dr.GetString(1);
                    list.Add(c);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'applicazione dei filtri sui cantieri", ex);
            }
            finally { cn.Close();}

            return list;
        }
        public static DataTable GetAllClienti()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT IdCliente, RagSocCli, Indirizzo, cap, Città, Tel1, " +
                      "Cell1, PartitaIva, CodFiscale, Data, Provincia, Note " +
                      "FROM TblClienti " +
                      "ORDER BY RagSocCli ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei clienti", ex);
            }
            finally { cn.Close(); }
        }
        public static DataTable FiltraClienti(string ragSoc)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            ragSoc = "%" + ragSoc + "%";

            try
            {
                sql = "SELECT IdCliente, RagSocCli, Indirizzo, cap, Città, Tel1, " +
                      "Cell1, PartitaIva, CodFiscale, Data, Provincia, Note " +
                      "FROM TblClienti " +
                      "WHERE RagSocCli LIKE @pRagSoc " +
                      "ORDER BY RagSocCli ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@pRagSoc", ragSoc));
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il filtro dei clienti", ex);
            }
            finally { cn.Close(); }
        }
        public static Clienti GetSingleCliente(int idCliente)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            Clienti cliente = new Clienti();
            string sql = "";

            try
            {
                sql = "SELECT IdCliente, RagSocCli, Indirizzo, cap, Città, Tel1, " +
                      "Cell1, PartitaIva, CodFiscale, Data, Provincia, Note " +
                      "FROM TblClienti " +
                      "WHERE IdCliente = @pId " +
                      "ORDER BY RagSocCli ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pId", idCliente));
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    cliente.IdCliente = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    cliente.RagSocCli = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    cliente.Indirizzo = (dr.IsDBNull(2) ? null : dr.GetString(2));
                    cliente.Cap = (dr.IsDBNull(3) ? null : dr.GetString(3));
                    cliente.Città = (dr.IsDBNull(4) ? null : dr.GetString(4));
                    cliente.Tel1 = (dr.IsDBNull(5) ? "" : dr.GetString(5));
                    cliente.Cell1 = (dr.IsDBNull(6) ? "" : dr.GetString(6));
                    cliente.PartitaIva = (dr.IsDBNull(7) ? null : dr.GetString(7));
                    cliente.CodFiscale = (dr.IsDBNull(8) ? null : dr.GetString(8));
                    cliente.Data = (dr.IsDBNull(9) ? new DateTime() : dr.GetDateTime(9));
                    cliente.Provincia = (dr.IsDBNull(10) ? null : dr.GetString(10));
                    cliente.Note = (dr.IsDBNull(11) ? null : dr.GetString(11));
                }

                return cliente;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei clienti", ex);
            }
            finally { cn.Close(); }
        }
        public static List<Clienti> GetClientiIdAndName()
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            List<Clienti> cList = new List<Clienti>();
            string sql = "";

            try
            {
                sql = "SELECT IdCliente, RagSocCli " +
                      "FROM TblClienti " +
                      "ORDER BY RagSocCli ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Clienti c = new Clienti();
                    c.IdCliente = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    c.RagSocCli = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    cList.Add(c);
                }

                return cList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante GetClientiIdAndName", ex);
            }
            finally { cn.Close(); dr.Close(); }
        }

        // INSERT
        public static bool InserisciCliente(Clienti c)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblClienti " +
                      "(RagSocCli,Indirizzo,Cap,Città,Provincia,Tel1,Cell1,PartitaIva,CodFiscale,Data,Note) " +
                      "VALUES (@pRagSoc,@pIndir,@pCap,@pCitta,@pProvincia,@pTel,@pCel,@pPartIva,@pCodFisc,CONVERT(date,@pData),@pNote) ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pRagSoc", c.RagSocCli));
                cmd.Parameters.Add(new SqlParameter("pIndir", c.Indirizzo));
                cmd.Parameters.Add(new SqlParameter("pCap", c.Cap));
                cmd.Parameters.Add(new SqlParameter("pCitta", c.Città));
                cmd.Parameters.Add(new SqlParameter("pProvincia", c.Provincia));
                cmd.Parameters.Add(new SqlParameter("pTel", c.Tel1));
                cmd.Parameters.Add(new SqlParameter("pCel", c.Cell1));
                cmd.Parameters.Add(new SqlParameter("pPartIva", c.PartitaIva));
                cmd.Parameters.Add(new SqlParameter("pCodFisc", c.CodFiscale));
                cmd.Parameters.Add(new SqlParameter("pData", c.Data));
                cmd.Parameters.Add(new SqlParameter("pNote", c.Note));

                int ret = cmd.ExecuteNonQuery();

                if (ret > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un nuovo cliente", ex);
            }
            finally { cn.Close(); }
        }

        // UPDATE
        public static bool UpdateCliente(string idCliente, Clienti c)
        {
            SqlConnection cn = GetConnection();
            string sql = "";
            try
            {
                sql = "UPDATE TblClienti " +
                      "SET RagSocCli = @pRagSoc, " +
                      "Indirizzo = @pIndir, " +
                      "cap = @pCap, " +
                      "Città = @pCitta, " +
                      "Tel1 = @pTel, " +
                      "Cell1 = @pCel, " +
                      "PartitaIva = @pPartIva, " +
                      "CodFiscale = @pCodFisc, " +
                      "Data = CONVERT(date,@pData), " +
                      "Provincia = @pProv, " +
                      "Note = @pNote " +
                      "WHERE IdCliente = @pId ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pRagSoc", c.RagSocCli));
                cmd.Parameters.Add(new SqlParameter("pIndir", c.Indirizzo));
                cmd.Parameters.Add(new SqlParameter("pCap", c.Cap));
                cmd.Parameters.Add(new SqlParameter("pCitta", c.Città));
                cmd.Parameters.Add(new SqlParameter("pTel", c.Tel1));
                cmd.Parameters.Add(new SqlParameter("pCel", c.Cell1));
                cmd.Parameters.Add(new SqlParameter("pPartIva", c.PartitaIva));
                cmd.Parameters.Add(new SqlParameter("pCodFisc", c.CodFiscale));
                cmd.Parameters.Add(new SqlParameter("pData", c.Data));
                cmd.Parameters.Add(new SqlParameter("pProv", c.Provincia));
                cmd.Parameters.Add(new SqlParameter("pNote", c.Note));
                cmd.Parameters.Add(new SqlParameter("pId", idCliente));

                int row = cmd.ExecuteNonQuery();

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'update di un cliente", ex);
            }
            finally { cn.Close(); }
        }

        // DELETE
        public static bool EliminaCliente(int idCliente)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "IF NOT EXISTS(SELECT IdTblClienti FROM TblCantieri where IdTblClienti = @pId) " +
                        "DELETE FROM TblClienti WHERE IdCliente = @pId ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pId", idCliente));

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