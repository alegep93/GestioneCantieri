using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestioneCantieri.DAO
{
    public class CantieriDAO : BaseDAO
    {
        public static Cantieri GetCantiere(string id)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string sql = "";
            Cantieri cant = new Cantieri();

            try
            {
                sql = "SELECT Cant.IdCantieri, Cli.RagSocCli, Cant.CodCant, Cant.DescriCodCAnt, " +
                      "Cant.Data, Cant.Indirizzo, Cant.Città, Cant.Ricarico, " +
                      "Cant.PzzoManodopera, Cant.Chiuso, Cant.Riscosso, Cant.Numero, " +
                      "Cant.ValorePreventivo, Cant.IVA, Cant.Anno, Cant.Preventivo, " +
                      "Cant.FasciaTblCantieri, Cant.DaDividere, Cant.Diviso, Cant.Fatturato " +
                      "FROM TblCantieri AS Cant " +
                      "JOIN TblClienti AS Cli ON(Cant.IdTblClienti = Cli.IdCliente) " +
                      "WHERE Cant.IdCantieri = @idCant " +
                      "ORDER BY Cant.CodCant ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("idCant", id));

                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    cant.IdCantieri = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    cant.RagSocCli = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    cant.CodCant = (dr.IsDBNull(2) ? null : dr.GetString(2));
                    cant.DescriCodCAnt = (dr.IsDBNull(3) ? null : dr.GetString(3));
                    cant.Data = (dr.IsDBNull(4) ? new DateTime() : dr.GetDateTime(4));
                    cant.Indirizzo = (dr.IsDBNull(5) ? null : dr.GetString(5));
                    cant.Città = (dr.IsDBNull(6) ? null : dr.GetString(6));
                    cant.Ricarico = (dr.IsDBNull(7) ? -1 : dr.GetInt32(7));
                    cant.PzzoManodopera = (dr.IsDBNull(8) ? 0.0m : dr.GetDecimal(8));
                    cant.Chiuso = (dr.IsDBNull(9) ? false : dr.GetBoolean(9));
                    cant.Riscosso = (dr.IsDBNull(10) ? false : dr.GetBoolean(10));
                    cant.Numero = (dr.IsDBNull(11) ? null : dr.GetString(11));
                    cant.ValorePreventivo = (dr.IsDBNull(12) ? 0.0m : dr.GetDecimal(12));
                    cant.Iva = (dr.IsDBNull(13) ? -1 : dr.GetInt32(13));
                    cant.Anno = (dr.IsDBNull(14) ? -1 : dr.GetInt32(14));
                    cant.Preventivo = (dr.IsDBNull(15) ? false : dr.GetBoolean(15));
                    cant.FasciaTblCantieri = (dr.IsDBNull(16) ? -1 : dr.GetInt32(16));
                    cant.DaDividere = (dr.IsDBNull(17) ? false : dr.GetBoolean(17));
                    cant.Diviso = (dr.IsDBNull(18) ? false : dr.GetBoolean(18));
                    cant.Fatturato = (dr.IsDBNull(19) ? false : dr.GetBoolean(19));
                }

                return cant;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei dati del singolo cantiere", ex);
            }
            finally { cn.Close(); }
        }
        public static DataTable GetCantieri(string anno, string codCant, string descr, bool chiuso, bool riscosso)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            anno = "%" + anno + "%";
            codCant = "%" + codCant + "%";
            descr = "%" + descr + "%";

            try
            {
                sql = "SELECT Cant.IdCantieri, Cli.RagSocCli, Cant.CodCant, Cant.DescriCodCAnt, " +
                      "Cant.Data, Cant.Indirizzo, Cant.Città, Cant.Ricarico, " +
                      "Cant.PzzoManodopera, Cant.Chiuso, Cant.Riscosso, Cant.Numero, " +
                      "Cant.ValorePreventivo, Cant.IVA, Cant.Anno, Cant.Preventivo, " +
                      "Cant.FasciaTblCantieri, Cant.DaDividere, Cant.Diviso, Cant.Fatturato " +
                      "FROM TblCantieri AS Cant " +
                      "JOIN TblClienti AS Cli ON(Cant.IdTblClienti = Cli.IdCliente) " +
                      "WHERE Anno LIKE @pAnno AND CodCant LIKE @pCodCant AND DescriCodCAnt LIKE @pDescr " +
                      "AND Chiuso LIKE @pChiuso AND Riscosso LIKE @pRiscosso " +
                      "ORDER BY Cant.CodCant ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pAnno", anno));
                cmd.Parameters.Add(new SqlParameter("pCodCant", codCant));
                cmd.Parameters.Add(new SqlParameter("pDescr", descr));
                cmd.Parameters.Add(new SqlParameter("pChiuso", chiuso));
                cmd.Parameters.Add(new SqlParameter("pRiscosso", riscosso));

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'applicazione dei filtri sui cantieri", ex);
            }
            finally { cn.Close(); }
        }
        public static DataTable GetCantieri(string anno, string codCant, bool fatturato, bool chiuso, bool riscosso)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            anno = "%" + anno + "%";
            codCant = "%" + codCant + "%";

            try
            {
                sql = "SELECT Cant.IdCantieri, Cli.RagSocCli, Cant.CodCant, Cant.DescriCodCAnt, " +
                      "Cant.Data, Cant.Indirizzo, Cant.Città, Cant.Ricarico, " +
                      "Cant.PzzoManodopera, Cant.Chiuso, Cant.Riscosso, Cant.Numero, " +
                      "Cant.ValorePreventivo, Cant.IVA, Cant.Anno, Cant.Preventivo, " +
                      "Cant.FasciaTblCantieri, Cant.DaDividere, Cant.Diviso, Cant.Fatturato " +
                      "FROM TblCantieri AS Cant " +
                      "JOIN TblClienti AS Cli ON(Cant.IdTblClienti = Cli.IdCliente) " +
                      "WHERE Anno LIKE @pAnno AND CodCant LIKE @pCodCant " +
                      "AND Chiuso = @pChiuso AND Riscosso = @pRiscosso AND Fatturato = @pFatturato " +
                      "ORDER BY Cant.CodCant ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pAnno", anno));
                cmd.Parameters.Add(new SqlParameter("pCodCant", codCant));
                cmd.Parameters.Add(new SqlParameter("pFatturato", fatturato));
                cmd.Parameters.Add(new SqlParameter("pChiuso", chiuso));
                cmd.Parameters.Add(new SqlParameter("pRiscosso", riscosso));

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'applicazione dei filtri sui cantieri", ex);
            }
            finally { cn.Close(); }
        }
    }
}