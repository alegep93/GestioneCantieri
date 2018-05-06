using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestioneCantieri.DAO
{
    public class DDTFornitoriDAO : BaseDAO
    {
        // SELECT
        public static List<DDTFornitori> GetAllDDT()
        {
            List<DDTFornitori> retList = new List<DDTFornitori>();
            string sql = "";
            SqlDataReader dr = null;
            SqlConnection cn = GetConnection();
            try
            {
                sql = "SELECT A.Id, B.RagSocForni, A.Data, A.Protocollo, A.NumeroDDT, A.Articolo, A.DescrizioneFornitore, A.DescrizioneMau, A.Quantità, A.PrezzoUnitario " +
                      "FROM TblDDTFornitori AS A " +
                      "INNER JOIN TblForitori AS B ON A.IdFornitore = B.IdFornitori " +
                      "ORDER BY B.RagSocForni, A.Data, A.Protocollo ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    DDTFornitori ddt = new DDTFornitori();
                    ddt.Id = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    ddt.RagSocFornitore = (dr.IsDBNull(1) ? "" : dr.GetString(1));
                    ddt.Data = (dr.IsDBNull(2) ? DateTime.Now.Date : dr.GetDateTime(2));
                    ddt.Protocollo = (dr.IsDBNull(3) ? -1 : dr.GetInt64(3));
                    ddt.NumeroDdt = (dr.IsDBNull(4) ? "" : dr.GetString(4));
                    ddt.Articolo = (dr.IsDBNull(5) ? "" : dr.GetString(5));
                    ddt.DescrizioneFornitore = (dr.IsDBNull(6) ? "" : dr.GetString(6));
                    ddt.DescrizioneMau = (dr.IsDBNull(7) ? "" : dr.GetString(7));
                    ddt.Qta = (dr.IsDBNull(8) ? -1 : dr.GetInt32(8));
                    ddt.PrezzoUnitario = (dr.IsDBNull(9) ? -1m : dr.GetDecimal(9));
                    retList.Add(ddt);
                }

                return retList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dell'elenco dei DDT dei Fornitori", ex);
            }
            finally
            {
                cn.Close();
                dr.Close();
            }
        }
        public static DDTFornitori GetDDT(int id)
        {
            DDTFornitori ddt = new DDTFornitori();
            string sql = "";
            SqlDataReader dr = null;
            SqlConnection cn = GetConnection();
            try
            {
                sql = "SELECT Id, IdFornitore, Data, Protocollo, NumeroDDT, Articolo, DescrizioneFornitore, DescrizioneMau, Quantità, PrezzoUnitario " +
                      "FROM TblDDTFornitori " +
                      "WHERE Id = @Id ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ddt.Id = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    ddt.IdFornitore = (dr.IsDBNull(1) ? 0 : dr.GetInt32(1));
                    ddt.Data = (dr.IsDBNull(2) ? DateTime.Now.Date : dr.GetDateTime(2));
                    ddt.Protocollo = (dr.IsDBNull(3) ? -1 : dr.GetInt64(3));
                    ddt.NumeroDdt = (dr.IsDBNull(4) ? "" : dr.GetString(4));
                    ddt.Articolo = (dr.IsDBNull(5) ? "" : dr.GetString(5));
                    ddt.DescrizioneFornitore = (dr.IsDBNull(6) ? "" : dr.GetString(6));
                    ddt.DescrizioneMau = (dr.IsDBNull(7) ? "" : dr.GetString(7));
                    ddt.Qta = (dr.IsDBNull(8) ? -1 : dr.GetInt32(8));
                    ddt.PrezzoUnitario = (dr.IsDBNull(9) ? -1m : dr.GetDecimal(9));
                }

                return ddt;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero del DDT Fornitori con id = " + id, ex);
            }
            finally
            {
                cn.Close();
                dr.Close();
            }
        }

        // INSERT
        public static bool InsertNewFornitore(DDTFornitori ddt)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblDDTFornitori (IdFornitore, Data, Protocollo, NumeroDDT, Articolo, DescrizioneFornitore, DescrizioneMau, Quantità, PrezzoUnitario) " +
                      "VALUES (@IdFornitore,@Data,@Protocollo,@NumeroDDT,@Articolo,@DescrizioneFornitore,@DescrizioneMau,@Quantità,@PrezzoUnitario) ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@IdFornitore", ddt.IdFornitore));
                cmd.Parameters.Add(new SqlParameter("@Data", ddt.Data));
                cmd.Parameters.Add(new SqlParameter("@Protocollo", ddt.Protocollo));
                cmd.Parameters.Add(new SqlParameter("@NumeroDDT", ddt.NumeroDdt));
                cmd.Parameters.Add(new SqlParameter("@Articolo", ddt.Articolo));
                cmd.Parameters.Add(new SqlParameter("@DescrizioneFornitore", ddt.DescrizioneFornitore));
                cmd.Parameters.Add(new SqlParameter("@DescrizioneMau", ddt.DescrizioneMau));
                cmd.Parameters.Add(new SqlParameter("@Quantità", ddt.Qta));
                cmd.Parameters.Add(new SqlParameter("@PrezzoUnitario", ddt.PrezzoUnitario));
                int rows = cmd.ExecuteNonQuery();

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
                cn.Close();
            }
        }

        // UPDATE
        public static bool UpdateDDTFornitore(int id, DDTFornitori ddt)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "UPDATE TblDDTFornitori SET IdFornitore = @IdFornitore, Data = @Data, Protocollo = @Protocollo, NumeroDDT = @NumeroDDT, Articolo = @Articolo, " +
                      "DescrizioneFornitore = @DescrizioneFornitore, DescrizioneMau = @DescrizioneMau, Quantità = @Quantità, PrezzoUnitario = @PrezzoUnitario " +
                      "WHERE Id = @Id ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@IdFornitore", ddt.IdFornitore));
                cmd.Parameters.Add(new SqlParameter("@Data", ddt.Data));
                cmd.Parameters.Add(new SqlParameter("@Protocollo", ddt.Protocollo));
                cmd.Parameters.Add(new SqlParameter("@NumeroDDT", ddt.NumeroDdt));
                cmd.Parameters.Add(new SqlParameter("@Articolo", ddt.Articolo));
                cmd.Parameters.Add(new SqlParameter("@DescrizioneFornitore", ddt.DescrizioneFornitore));
                cmd.Parameters.Add(new SqlParameter("@DescrizioneMau", ddt.DescrizioneMau));
                cmd.Parameters.Add(new SqlParameter("@Quantità", ddt.Qta));
                cmd.Parameters.Add(new SqlParameter("@PrezzoUnitario", ddt.PrezzoUnitario));
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'aggiornamento del DDT Fornitore " + id, ex);
            }
            finally
            {
                cn.Close();
            }
        }

        // DELETE
        public static bool DeleteDDTFornitore(int id)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "DELETE FROM TblDDTFornitori WHERE Id = @id ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                int rows = cmd.ExecuteNonQuery();

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
                cn.Close();
            }
        }
    }
}