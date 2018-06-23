using Dapper;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace GestioneCantieri.DAO
{
    public class DDTFornitoriDAO : BaseDAO
    {
        // SELECT
        public static List<DDTFornitori> GetAllDDT()
        {
            List<DDTFornitori> retList = new List<DDTFornitori>();
            string sql = "";
            SqlConnection cn = GetConnection();
            try
            {
                sql = "SELECT A.Id, B.RagSocForni 'ragSocFornitore', A.Data, A.Protocollo, A.NumeroDDT, A.Articolo, A.DescrizioneFornitore, A.DescrizioneMau, A.Qta, A.Valore " +
                      "FROM TblDDTFornitori AS A " +
                      "INNER JOIN TblForitori AS B ON A.IdFornitore = B.IdFornitori " +
                      "ORDER BY B.RagSocForni, A.Data, A.Protocollo ";

                return cn.Query<DDTFornitori>(sql).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dell'elenco dei DDT dei Fornitori", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static List<DDTFornitori> GetAllDDT(DDTFornitori filters)
        {
            List<DDTFornitori> retList = new List<DDTFornitori>();
            string sql = "";
            SqlConnection cn = GetConnection();

            filters.NumeroDdt = "%" + filters.NumeroDdt + "%";
            filters.Articolo = "%" + filters.Articolo + "%";
            filters.DescrizioneFornitore = "%" + filters.DescrizioneFornitore + "%";
            filters.DescrizioneMau  = "%" + filters.DescrizioneMau + "%";

            try
            {
                sql = "SELECT A.Id, B.RagSocForni 'ragSocFornitore', A.Data, A.Protocollo, A.NumeroDDT, A.Articolo, A.DescrizioneFornitore, A.DescrizioneMau, A.Qta, A.Valore " +
                      "FROM TblDDTFornitori AS A " +
                      "INNER JOIN TblForitori AS B ON A.IdFornitore = B.IdFornitori " +
                      "WHERE A.NumeroDDT LIKE @NumeroDDT AND A.Articolo LIKE @Articolo AND A.DescrizioneFornitore LIKE @DescrizioneFornitore AND A.DescrizioneMau LIKE @DescrizioneMau ";

                if (filters.IdFornitore != -1)
                    sql += "AND A.IdFornitore = @IdFornitore ";
                if (filters.Protocollo != -1)
                    sql += "AND A.Protocollo = @Protocollo ";
                if (filters.Qta != -1)
                    sql += "AND A.Qta = @Qta ";

                sql += "ORDER BY B.RagSocForni, A.Data, A.Protocollo ";

                retList = cn.Query<DDTFornitori>(sql, filters).ToList();
                return retList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dell'elenco filtrato dei DDT dei Fornitori", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static DDTFornitori GetDDT(int id)
        {
            DDTFornitori ddt = new DDTFornitori();
            string sql = "";
            //SqlDataReader dr = null;
            SqlConnection cn = GetConnection();
            try
            {
                sql = "SELECT Id, IdFornitore, Data, Protocollo, NumeroDDT, Articolo, DescrizioneFornitore, DescrizioneMau, Qta, Valore " +
                      "FROM TblDDTFornitori " +
                      "WHERE Id = @Id ";

                return cn.Query<DDTFornitori>(sql, new { Id = id }).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero del DDT Fornitori con id = " + id, ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }

        // INSERT
        public static bool InsertNewFornitore(DDTFornitori ddt)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblDDTFornitori (IdFornitore, Data, Protocollo, NumeroDDT, Articolo, DescrizioneFornitore, DescrizioneMau, Qta, Valore) " +
                      "VALUES (@IdFornitore, @Data, @Protocollo, @NumeroDDT, @Articolo, @DescrizioneFornitore, @DescrizioneMau, @Qta, @Valore) ";

                int rows = cn.Execute(sql, ddt);

                if (rows > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un nuovo DDT Fornitore ", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }

        // UPDATE
        public static bool UpdateDDTFornitore(DDTFornitori ddt)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "UPDATE TblDDTFornitori SET IdFornitore = @IdFornitore, Data = @Data, Protocollo = @Protocollo, NumeroDDT = @NumeroDDT, Articolo = @Articolo, " +
                      "DescrizioneFornitore = @DescrizioneFornitore, DescrizioneMau = @DescrizioneMau, Qta = @Qta, Valore = @Valore " +
                      "WHERE Id = @Id ";

                int rows = cn.Execute(sql, ddt);

                if (rows > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'aggiornamento del DDT Fornitore " + ddt.Id, ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }

        // DELETE
        public static bool DeleteDDTFornitore(int id)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "DELETE FROM TblDDTFornitori WHERE Id = @Id ";

                int rows = cn.Execute(sql, new { Id = id });

                if (rows > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione del DDT Fornitore con id = " + id, ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
    }
}