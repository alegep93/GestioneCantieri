using Dapper;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

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
            finally { CloseResouces(cn, null); }
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
            finally { CloseResouces(cn, null); }
        }
        public static List<Fornitori> GetFornitori()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT IdFornitori,RagSocForni,Indirizzo,cap, " +
                      "Città,Tel1,Cell1,PartitaIva,CodFiscale,Abbreviato " +
                      "FROM TblForitori " +
                      "ORDER BY RagSocForni ASC ";

                return cn.Query<Fornitori>(sql).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei fornitori", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static int GetIdFornitore(string ragSoc)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT IdFornitori FROM TblForitori WHERE RagSocForni = @RagSocForni";

                return cn.Query<int>(sql, new { RagSocForni = ragSoc }).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dell'idFornitore", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static string GetRagSocFornitore(int id)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT RagSocForni FROM TblForitori WHERE IdFornitori = @IdFornitori";
                return cn.Query<string>(sql, new { IdFornitori = id }).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero della Ragione Sociale del Fornitore " + id, ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static Fornitori GetSingleFornitore(int idFornitore)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT IdFornitori,RagSocForni,Indirizzo,cap, " +
                      "Città,Tel1,Cell1,PartitaIva,CodFiscale,Abbreviato " +
                      "FROM TblForitori " +
                      "WHERE IdFornitori = @IdFornitori " +
                      "ORDER BY RagSocForni ASC ";

                return cn.Query<Fornitori>(sql, new { IdFornitori = idFornitore }).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero di un singolo operaio", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }

        public static bool InserisciFornitore(Fornitori f)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblForitori(RagSocForni, Indirizzo, cap, Città, Tel1, Cell1, PartitaIva, CodFiscale, Abbreviato) " +
                      "VALUES (@RagSocForni, @Indirizzo, @cap, @Città, @Tel1, @Cell1, @PartitaIva, @CodFiscale, @Abbreviato) ";

                int ret = cn.Execute(sql, f);

                if (ret > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un nuovo Fornitore", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static bool UpdateFornitore(Fornitori f)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "UPDATE TblForitori " +
                      "SET RagSocForni = @RagSocForni, " +
                      "Indirizzo = @Indirizzo, " +
                      "cap = @cap, " +
                      "Città = @Città, " +
                      "Tel1 = @Tel1, " +
                      "Cell1 = @Cell1, " +
                      "PartitaIva = @PartitaIva, " +
                      "CodFiscale = @CodFiscale, " +
                      "Abbreviato = @Abbreviato " +
                      "WHERE IdFornitori = @IdFornitori ";

                int row = cn.Execute(sql, f);

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'update di un fornitore", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static bool EliminaFornitore(int idFornit)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "DELETE FROM TblForitori " +
                      "WHERE IdFornitori = @IdFornitori ";

                int row = cn.Execute(sql, new { IdFornitori = idFornit });

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