using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestioneCantieri.DAO
{
    public class GestionePagamentiDAO : BaseDAO
    {
        //SELECT
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

        //INSERT
        public static bool InserisciPagamento(string idCant, string operaio, string qta, string tipologia, string pzzoManodop, string descrManodop,
            string note1, string note2, bool visibile, bool ricaricoSiNo, bool? ricalcolo = null)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblMaterialiCantieri ([IdTblCantieri],[DescriMateriali],[Qta],[Tipologia],[Visibile],[Ricalcolo],[ricaricoSiNo],[Note],[PzzoFinCli]) " +
                      "VALUES (@pIdCant,@pDescrMat,@pQta,@pTipol,@pVisibile,@pRicalcolo,@pRicarico,@pNote,'')";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", idCant));
                cmd.Parameters.Add(new SqlParameter("pDescrMat", descrManodop));
                cmd.Parameters.Add(new SqlParameter("pQta", qta));
                cmd.Parameters.Add(new SqlParameter("pTipol", tipologia));
                cmd.Parameters.Add(new SqlParameter("pVisibile", visibile));
                cmd.Parameters.Add(new SqlParameter("pRicarico", ricaricoSiNo));
                cmd.Parameters.Add(new SqlParameter("pNote", note1 + " - " + note2));

                if (ricalcolo == null)
                    cmd.Parameters.Add(new SqlParameter("pRicalcolo", DBNull.Value));

                int row = cmd.ExecuteNonQuery();

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di una manodopera", ex);
            }
            finally { cn.Close(); }
        }
    }
}