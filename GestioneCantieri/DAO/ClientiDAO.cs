using Dapper;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace GestioneCantieri.DAO
{
    public class ClientiDAO : BaseDAO
    {
        //SELECT
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
        public static List<Clienti> GetClienti(string filtro)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            filtro = "%" + filtro + "%";

            try
            {
                sql = "SELECT IdCliente, RagSocCli " +
                      "FROM TblClienti " +
                      "WHERE RagSocCli LIKE @RagSocCli ";

                return cn.Query<Clienti>(sql, new { RagSocCli = filtro }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'applicazione dei filtri sui clienti", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
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
            string sql = "";

            try
            {
                sql = "SELECT IdCliente, RagSocCli, Indirizzo, cap, Città, Tel1, " +
                      "Cell1, PartitaIva, CodFiscale, Data, Provincia, Note " +
                      "FROM TblClienti " +
                      "WHERE IdCliente = @IdCliente " +
                      "ORDER BY RagSocCli ASC ";

                return cn.Query<Clienti>(sql, new { IdCliente = idCliente }).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei clienti", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static List<Clienti> GetClientiIdAndName()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT IdCliente, RagSocCli " +
                      "FROM TblClienti " +
                      "ORDER BY RagSocCli ASC ";

                return cn.Query<Clienti>(sql).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante GetClientiIdAndName", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
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
                      "VALUES (@RagSocCli,@Indirizzo,@Cap,@Città,@Provincia,@Tel1,@Cell1,@PartitaIva,@CodFiscale,CONVERT(date,@Data),@Note) ";

                int ret = cn.Execute(sql, c);

                if (ret > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un nuovo cliente", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }

        // UPDATE
        public static bool UpdateCliente(Clienti c)
        {
            SqlConnection cn = GetConnection();
            string sql = "";
            try
            {
                sql = "UPDATE TblClienti " +
                      "SET RagSocCli = @RagSocCli, " +
                      "Indirizzo = @Indirizzo, " +
                      "cap = @cap, " +
                      "Città = @Città, " +
                      "Tel1 = @Tel1, " +
                      "Cell1 = @Cell1, " +
                      "PartitaIva = @PartitaIva, " +
                      "CodFiscale = @CodFiscale, " +
                      "Data = CONVERT(date,@Data), " +
                      "Provincia = @Provincia, " +
                      "Note = @Note " +
                      "WHERE IdCliente = @IdCliente ";

                int row = cn.Execute(sql, c);

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'update di un cliente", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }

        // DELETE
        public static bool EliminaCliente(int idCliente)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "IF NOT EXISTS(SELECT IdTblClienti FROM TblCantieri where IdTblClienti = @IdTblClienti) " +
                        "DELETE FROM TblClienti WHERE IdCliente = @IdCliente ";

                int row = cn.Execute(sql, new { pId = idCliente });

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione di un fornitore", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
    }
}