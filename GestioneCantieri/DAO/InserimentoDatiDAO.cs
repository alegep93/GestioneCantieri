using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestioneCantieri.DAO
{
    public class InserimentoDatiDAO : BaseDAO
    {
        /* Clienti */
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
                    c.Id = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
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
        public static bool InserisciCliente(string ragSoc, string indirizzo, string cap,
            string citta, string prov, string tel, string cel, string pIva,
            string codFisc, string data, string note)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblClienti " +
                      "(RagSocCli,Indirizzo,Cap,Città,Provincia,Tel1,Cell1,PartitaIva,CodFiscale,Data,Note) " +
                      "VALUES (@pRagSoc,@pIndir,@pCap,@pCitta,@pProvincia,@pTel,@pCel,@pPartIva,@pCodFisc,@pData,@pNote) ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pRagSoc", ragSoc));
                cmd.Parameters.Add(new SqlParameter("pIndir", indirizzo));
                cmd.Parameters.Add(new SqlParameter("pCap", cap));
                cmd.Parameters.Add(new SqlParameter("pCitta", citta));
                cmd.Parameters.Add(new SqlParameter("pProvincia", prov));
                cmd.Parameters.Add(new SqlParameter("pTel", tel));
                cmd.Parameters.Add(new SqlParameter("pCel", cel));
                cmd.Parameters.Add(new SqlParameter("pPartIva", pIva));
                cmd.Parameters.Add(new SqlParameter("pCodFisc", codFisc));
                cmd.Parameters.Add(new SqlParameter("pData", data));
                cmd.Parameters.Add(new SqlParameter("pNote", note));

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
        /* Fine Clienti */

        /* Cantieri */
        public static DataTable GetAllCantieri()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT Cli.RagSocCli, Cant.CodCant, Cant.DescriCodCAnt, " +
                      "Cant.Data, Cant.Indirizzo, Cant.Città, Cant.Ricarico, " +
                      "Cant.PzzoManodopera, Cant.Chiuso, Cant.Riscosso, Cant.Numero, " +
                      "Cant.ValorePreventivo, Cant.IVA, Cant.Anno, Cant.Preventivo, " +
                      "Cant.FasciaTblCantieri, Cant.DaDividere, Cant.Diviso, Cant.Fatturato " +
                      "FROM TblCantieri AS Cant " +
                      "JOIN TblClienti AS Cli ON(Cant.IdTblClienti = Cli.IdCliente) " +
                      "ORDER BY Cli.RagSocCli ASC, Cant.CodCant ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei cantieri", ex);
            }
            finally { cn.Close(); }
        }
        public static bool InserisciCantiere(string idCliente, string data, string codCant,
            string descrCodCant, string indirizzo, string citta, string ricarico, string pzzoManodop,
            string chiuso, string riscosso, string numeroCant, string valPrev, string iva, string anno,
            string preventivo, string daDividere, string diviso, string fatturato, string fasciaCantiere)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblCantieri " +
                      "(IdTblClienti,Data,CodCant,DescriCodCAnt,Indirizzo,Città,Ricarico, " +
                      "PzzoManodopera,Chiuso,Riscosso,Numero,ValorePreventivo,IVA,Anno,Preventivo, " +
                      "FasciaTblCantieri,DaDividere,Diviso,Fatturato) " +
                      "VALUES (@pIdTblClienti,@pData,@pCodCant,@pDescCodCant,@pIndir,@pCitta,@pRicar, " +
                      "@pPzzoManod,@pChiuso,@pRiscosso,@pNumero,@pValPrev,@pIva,@pAnno,@pPrev,@pFasciaCant, " +
                      "@pDaDividere,@pDiviso,@pFatturato) ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdTblClienti", idCliente));
                cmd.Parameters.Add(new SqlParameter("pData", data));
                cmd.Parameters.Add(new SqlParameter("pCodCant", codCant));
                cmd.Parameters.Add(new SqlParameter("pDescCodCant", descrCodCant));
                cmd.Parameters.Add(new SqlParameter("pIndir", indirizzo));
                cmd.Parameters.Add(new SqlParameter("pCitta", citta));
                cmd.Parameters.Add(new SqlParameter("pRicar", ricarico));
                cmd.Parameters.Add(new SqlParameter("pPzzoManod", pzzoManodop));
                cmd.Parameters.Add(new SqlParameter("pChiuso", chiuso));
                cmd.Parameters.Add(new SqlParameter("pRiscosso", riscosso));
                cmd.Parameters.Add(new SqlParameter("pNumero", numeroCant));
                cmd.Parameters.Add(new SqlParameter("pValPrev", valPrev));
                cmd.Parameters.Add(new SqlParameter("pIva", iva));
                cmd.Parameters.Add(new SqlParameter("pAnno", anno));
                cmd.Parameters.Add(new SqlParameter("pPrev", preventivo));
                cmd.Parameters.Add(new SqlParameter("pFasciaCant", fasciaCantiere));
                cmd.Parameters.Add(new SqlParameter("pDaDividere", daDividere));
                cmd.Parameters.Add(new SqlParameter("pDiviso", diviso));
                cmd.Parameters.Add(new SqlParameter("pFatturato", fatturato));

                int ret = cmd.ExecuteNonQuery();

                if (ret > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un nuovo cantiere", ex);
            }
            finally { cn.Close(); }
        }
        /* Fine Cantieri */
    }
}