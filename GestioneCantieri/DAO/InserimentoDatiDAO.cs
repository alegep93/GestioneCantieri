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

        /* Fornitori */
        public static DataTable GetAllFornitori()
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
        public static bool InserisciFornitore(string ragSoc, string citta, string indir,
            string cap, string tel, string cel, string codFisc, string pIva, string abbrev)
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
                cmd.Parameters.Add(new SqlParameter("pRagSoc", ragSoc));
                cmd.Parameters.Add(new SqlParameter("pIndir", indir));
                cmd.Parameters.Add(new SqlParameter("pCap", cap));
                cmd.Parameters.Add(new SqlParameter("pCitta", citta));
                cmd.Parameters.Add(new SqlParameter("pTel", tel));
                cmd.Parameters.Add(new SqlParameter("pCel", cel));
                cmd.Parameters.Add(new SqlParameter("pPIva", pIva));
                cmd.Parameters.Add(new SqlParameter("pCodFisc", codFisc));
                cmd.Parameters.Add(new SqlParameter("pAbbrev", abbrev));
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
        /* Fine Fornitori */

        /* Operai */
        public static DataTable GetAllOperai()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT IdOperaio,NomeOp,DescrOP,Suffisso,Operaio " +
                      "FROM TblOperaio " +
                      "ORDER BY NomeOp ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero degli operai", ex);
            }
            finally { cn.Close(); }
        }
        public static bool InserisciOperaio(string nome, string descr, string suff, string operaio)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblOperaio " +
                      "(NomeOp, DescrOP, Suffisso, Operaio) " +
                      "VALUES (@pNome,@pDescr,@pSuff,@pOperaio) ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@pNome", nome));
                cmd.Parameters.Add(new SqlParameter("@pDescr", descr));
                cmd.Parameters.Add(new SqlParameter("@pSuff", suff));
                cmd.Parameters.Add(new SqlParameter("@pOperaio", operaio));

                int ret = cmd.ExecuteNonQuery();

                if (ret > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un nuovo operaio", ex);
            }
            finally { cn.Close(); }
        }
        /* Fine Operai */

        /* Cantieri */
        public static DataTable GetAllCantieri()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT Cant.IdCantieri, Cli.RagSocCli, Cant.CodCant, Cant.DescriCodCAnt, " +
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
        public static Cantieri GetSingleCantiere(int idCant)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            Cantieri c = new Cantieri();
            string sql = "";

            try
            {
                sql = "SELECT Cant.IdCantieri, Cli.RagSocCli, Cant.CodCant, Cant.DescriCodCAnt, " +
                      "Cant.Data, Cant.Indirizzo, Cant.Città, Cant.Ricarico, " +
                      "Cant.PzzoManodopera, Cant.Chiuso, Cant.Riscosso, Cant.Numero, " +
                      "Cant.ValorePreventivo, Cant.IVA, Cant.Anno, Cant.Preventivo, " +
                      "Cant.FasciaTblCantieri, Cant.DaDividere, Cant.Diviso, Cant.Fatturato " +
                      "FROM TblCantieri AS Cant " +
                      "JOIN TblClienti AS Cli ON(Cant.IdTblClienti = Cli.IdCliente) " +
                      "WHERE Cant.IdCantieri = @pIdCant " +
                      "ORDER BY Cli.RagSocCli ASC, Cant.CodCant ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", idCant));
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    c.IdCantieri = dr.GetInt32(0);
                    c.RagSocCli = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    c.CodCant = (dr.IsDBNull(2) ? null : dr.GetString(2));
                    c.DescriCodCAnt = (dr.IsDBNull(3) ? null : dr.GetString(3));
                    c.Data = (dr.IsDBNull(4) ? new DateTime() : dr.GetDateTime(4));
                    c.Indirizzo = (dr.IsDBNull(5) ? null : dr.GetString(5));
                    c.Città = (dr.IsDBNull(6) ? null : dr.GetString(6));
                    c.Ricarico = (dr.IsDBNull(7) ? -1 : dr.GetInt32(7));
                    c.PzzoManodopera = (dr.IsDBNull(8) ? -1m : dr.GetDecimal(8));
                    c.Chiuso = (dr.IsDBNull(9) ? false : dr.GetBoolean(9));
                    c.Riscosso = (dr.IsDBNull(10) ? false : dr.GetBoolean(10));
                    c.Numero = (dr.IsDBNull(11) ? null : dr.GetString(11));
                    c.ValorePreventivo = (dr.IsDBNull(12) ? -1m : dr.GetDecimal(12));
                    c.Iva = (dr.IsDBNull(13) ? -1 : dr.GetInt32(13));
                    c.Anno = (dr.IsDBNull(14) ? -1 : dr.GetInt32(14));
                    c.Preventivo = (dr.IsDBNull(15) ? false : dr.GetBoolean(15));
                    c.FasciaTblCantieri = (dr.IsDBNull(16) ? -1 : dr.GetInt32(16));
                    c.DaDividere = (dr.IsDBNull(17) ? false : dr.GetBoolean(17));
                    c.Diviso = (dr.IsDBNull(18) ? false : dr.GetBoolean(18));
                    c.Fatturato = (dr.IsDBNull(19) ? false : dr.GetBoolean(19));
                }

                return c;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero di un singolo cantiere", ex);
            }
            finally { cn.Close(); dr.Close(); }
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
        public static DataTable FiltraCantieri(string anno, string codCant, string descr, string cliente,
            bool chiuso, bool riscosso)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            anno = "%" + anno + "%";
            codCant = "%" + codCant + "%";
            descr = "%" + descr + "%";
            cliente = "%" + cliente + "%";
            /*chiuso = "%" + chiuso + "%";
            riscosso = "%" + riscosso + "%";*/

            try
            {
                sql = "SELECT Cant.IdCantieri, Cli.RagSocCli, Cant.CodCant, Cant.DescriCodCAnt, " +
                      "Cant.Data, Cant.Indirizzo, Cant.Città, Cant.Ricarico, " +
                      "Cant.PzzoManodopera, Cant.Chiuso, Cant.Riscosso, Cant.Numero, " +
                      "Cant.ValorePreventivo, Cant.IVA, Cant.Anno, Cant.Preventivo, " +
                      "Cant.FasciaTblCantieri, Cant.DaDividere, Cant.Diviso, Cant.Fatturato " +
                      "FROM TblCantieri AS Cant " +
                      "JOIN TblClienti AS Cli ON(Cant.IdTblClienti = Cli.IdCliente) " +
                      "WHERE Anno LIKE @pAnno AND CodCant LIKE @pCodCant AND DescriCodCAnt LIKE @pDescr AND Cli.RagSocCli LIKE @pRagSocCli " +
                      "AND Chiuso LIKE @pChiuso AND Riscosso LIKE @pRiscosso " +
                      "ORDER BY Cli.RagSocCli ASC, Cant.CodCant ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pAnno", anno));
                cmd.Parameters.Add(new SqlParameter("pCodCant", codCant));
                cmd.Parameters.Add(new SqlParameter("pDescr", descr));
                cmd.Parameters.Add(new SqlParameter("pRagSocCli", cliente));
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
        public static bool UpdateCantiere(string idCant, string idCliente, string data, string codCant,
            string descrCodCant, string indirizzo, string citta, string ricarico, string pzzoManodop,
            string chiuso, string riscosso, string numeroCant, string valPrev, string iva, string anno,
            string preventivo, string daDividere, string diviso, string fatturato, string fasciaCantiere)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "UPDATE TblCantieri " +
                      "SET IdTblClienti = @pIdClienti, Data = @pData, " +
                      "CodCant = @pCodCant, DescriCodCAnt = @pDescrCant, " +
                      "Indirizzo = @pIndir, Città = @pCitta, " +
                      "Ricarico = @pRicarico, PzzoManodopera = @pPrezzoManodop, " +
                      "Chiuso = @pChiuso, Riscosso = @pRiscosso, " +
                      "Numero = @pNumero, ValorePreventivo = @pValPrev, " +
                      "IVA = @pIva, Anno = @pAnno, " +
                      "Preventivo = @pPrev, FasciaTblCantieri = @pFascia, " +
                      "DaDividere = @pDaDividere, Diviso = @pDiviso, " +
                      "Fatturato = @pFatturato " +
                      "WHERE IdCantieri = @pId ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdClienti", idCliente));
                cmd.Parameters.Add(new SqlParameter("pData", data));
                cmd.Parameters.Add(new SqlParameter("pCodCant", codCant));
                cmd.Parameters.Add(new SqlParameter("pDescrCant", descrCodCant));
                cmd.Parameters.Add(new SqlParameter("pIndir", indirizzo));
                cmd.Parameters.Add(new SqlParameter("pCitta", citta));
                cmd.Parameters.Add(new SqlParameter("pRicarico", ricarico));
                cmd.Parameters.Add(new SqlParameter("pPrezzoManodop", pzzoManodop));
                cmd.Parameters.Add(new SqlParameter("pChiuso", chiuso));
                cmd.Parameters.Add(new SqlParameter("pRiscosso", riscosso));
                cmd.Parameters.Add(new SqlParameter("pNumero", numeroCant));
                cmd.Parameters.Add(new SqlParameter("pValPrev", valPrev));
                cmd.Parameters.Add(new SqlParameter("pIva", iva));
                cmd.Parameters.Add(new SqlParameter("pAnno", anno));
                cmd.Parameters.Add(new SqlParameter("pPrev", preventivo));
                cmd.Parameters.Add(new SqlParameter("pFascia", fasciaCantiere));
                cmd.Parameters.Add(new SqlParameter("pDaDividere", daDividere));
                cmd.Parameters.Add(new SqlParameter("pDiviso", diviso));
                cmd.Parameters.Add(new SqlParameter("pFatturato", fatturato));
                cmd.Parameters.Add(new SqlParameter("pId", idCant));

                int row = cmd.ExecuteNonQuery();

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione del cantiere", ex);
            }
            finally { cn.Close(); }
        }
        public static bool EliminaCantiere(int idCant)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "DELETE FROM TblCantieri " +
                      "WHERE IdCantieri = @pId ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pId", idCant));

                int row = cmd.ExecuteNonQuery();

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione del cantiere", ex);
            }
            finally { cn.Close(); }
        }
        /* Fine Cantieri */
    }
}