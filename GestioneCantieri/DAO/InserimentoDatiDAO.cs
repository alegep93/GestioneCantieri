using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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
        /* Fine Clienti */

        /* Fornitori */
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
        /* Fine Fornitori */

        /* Operai */
        public static DataTable GetAllOperai()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT IdOperaio,NomeOp,DescrOP,Suffisso,Operaio,CostoOperaio " +
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
        public static Operai GetSingleOperaio(int idOperaio)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            Operai op = new Operai();
            string sql = "";

            try
            {
                sql = "SELECT IdOperaio,NomeOp,DescrOP,Suffisso,Operaio,CostoOperaio " +
                      "FROM TblOperaio " +
                      "WHERE IdOperaio = @pId " +
                      "ORDER BY NomeOp ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pId", idOperaio));
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    op.IdOperaio = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    op.NomeOp = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    op.DescrOp = (dr.IsDBNull(2) ? null : dr.GetString(2));
                    op.Suffisso = (dr.IsDBNull(3) ? null : dr.GetString(3));
                    op.Operaio = (dr.IsDBNull(4) ? null : dr.GetString(4));
                    op.CostoOperaio = (dr.IsDBNull(5) ? 0.0m : dr.GetDecimal(5));
                }

                return op;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero di un singolo operaio", ex);
            }
            finally { cn.Close(); dr.Close(); }
        }
        public static bool InserisciOperaio(string nome, string descr, string suff, string operaio, string costoOp)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblOperaio " +
                      "(NomeOp, DescrOP, Suffisso, Operaio, CostoOperaio) " +
                      "VALUES (@pNome,@pDescr,@pSuff,@pOperaio,@pCostoOp) ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@pNome", nome));
                cmd.Parameters.Add(new SqlParameter("@pDescr", descr));
                cmd.Parameters.Add(new SqlParameter("@pSuff", suff));
                cmd.Parameters.Add(new SqlParameter("@pOperaio", operaio));
                cmd.Parameters.Add(new SqlParameter("@pCostoOp", costoOp));

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
        public static bool UpdateOperaio(string idOper, string nome, string descr, string suff, string oper, string costoOp)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "UPDATE TblOperaio " +
                      "SET NomeOp = @pNome, " +
                      "DescrOP = @pDescr, " +
                      "Suffisso = @pSuff, " +
                      "Operaio = @pOper, " +
                      "CostoOperaio = @pCostoOp " +
                      "WHERE IdOperaio = @pId ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pNome", nome));
                cmd.Parameters.Add(new SqlParameter("pDescr", descr));
                cmd.Parameters.Add(new SqlParameter("pSuff", suff));
                cmd.Parameters.Add(new SqlParameter("pOper", oper));
                cmd.Parameters.Add(new SqlParameter("pCostoOp", costoOp));
                cmd.Parameters.Add(new SqlParameter("pId", idOper));

                int row = cmd.ExecuteNonQuery();

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'update di un operaio", ex);
            }
            finally { cn.Close(); }
        }
        public static bool EliminaOperaio(int idOper)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "DELETE FROM TblOperaio " +
                      "WHERE IdOperaio = @pId ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pId", idOper));

                int row = cmd.ExecuteNonQuery();

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione dell'operaio", ex);
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
                      "ORDER BY Cant.CodCant ASC ";

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
                      "Cant.FasciaTblCantieri, Cant.DaDividere, Cant.Diviso, Cant.Fatturato, Cant.CodRiferCant " +
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
                    c.CodRiferCant = (dr.IsDBNull(20) ? null : dr.GetString(20));
                }

                return c;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero di un singolo cantiere", ex);
            }
            finally { cn.Close(); dr.Close(); }
        }
        public static string GetLastNumCantForYear(string anno)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string sql = "";
            int num = 0;
            string ret = "";

            try
            {
                sql = "SELECT (MAX(Numero)+1) FROM TblCantieri WHERE Anno = @pAnno ";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@pAnno", anno));
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    num = (dr.IsDBNull(0) ? 1 : dr.GetInt32(0));
                }

                ret = num.ToString();

                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recuper dell'ultimo numero cantiere", ex);
            }
            finally { cn.Close(); dr.Close(); }
        }
        public static string GetNumCantPerAnno(string anno)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string sql = "";
            int num = 0;
            string ret = "";

            try
            {
                sql = "SELECT COUNT(*) FROM TblCantieri WHERE Anno = @pAnno ";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@pAnno", anno));
                dr = cmd.ExecuteReader();

                if (dr.Read())
                    num = (dr.IsDBNull(0) ? 1 : dr.GetInt32(0));

                ret = num.ToString();

                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recuper dell'ultimo numero cantiere", ex);
            }
            finally { cn.Close(); dr.Close(); }
        }
        public static bool InserisciCantiere(Cantieri c)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblCantieri " +
                      "(IdTblClienti,Data,CodCant,DescriCodCAnt,Indirizzo,Città,Ricarico, " +
                      "PzzoManodopera,Chiuso,Riscosso,Numero,ValorePreventivo,IVA,Anno,Preventivo, " +
                      "FasciaTblCantieri,DaDividere,Diviso,Fatturato,CodRiferCant) " +
                      "VALUES (@pIdTblClienti,CONVERT(date,@pData),@pCodCant,@pDescCodCant,@pIndir,@pCitta,@pRicar, " +
                      "@pPzzoManod,@pChiuso,@pRiscosso,@pNumero,@pValPrev,@pIva,@pAnno,@pPrev,@pFasciaCant, " +
                      "@pDaDividere,@pDiviso,@pFatturato,@pCodRiferCant) ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdTblClienti", c.IdtblClienti));
                cmd.Parameters.Add(new SqlParameter("pData", c.Data));
                cmd.Parameters.Add(new SqlParameter("pCodCant", c.CodCant));
                cmd.Parameters.Add(new SqlParameter("pDescCodCant", c.DescriCodCAnt));
                cmd.Parameters.Add(new SqlParameter("pIndir", c.Indirizzo));
                cmd.Parameters.Add(new SqlParameter("pCitta", c.Città));
                cmd.Parameters.Add(new SqlParameter("pRicar", c.Ricarico));
                cmd.Parameters.Add(new SqlParameter("pPzzoManod", c.PzzoManodopera));
                cmd.Parameters.Add(new SqlParameter("pChiuso", c.Chiuso));
                cmd.Parameters.Add(new SqlParameter("pRiscosso", c.Riscosso));
                cmd.Parameters.Add(new SqlParameter("pNumero", c.Numero));
                cmd.Parameters.Add(new SqlParameter("pValPrev", c.ValorePreventivo));
                cmd.Parameters.Add(new SqlParameter("pIva", c.Iva));
                cmd.Parameters.Add(new SqlParameter("pAnno", c.Anno));
                cmd.Parameters.Add(new SqlParameter("pPrev", c.Preventivo));
                cmd.Parameters.Add(new SqlParameter("pFasciaCant", c.FasciaTblCantieri));
                cmd.Parameters.Add(new SqlParameter("pDaDividere", c.DaDividere));
                cmd.Parameters.Add(new SqlParameter("pDiviso", c.Diviso));
                cmd.Parameters.Add(new SqlParameter("pFatturato", c.Fatturato));
                cmd.Parameters.Add(new SqlParameter("pCodRiferCant", c.CodRiferCant));

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
                      "ORDER BY Cant.CodCant ASC ";

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
        public static bool UpdateCantiere(string idCant, Cantieri c)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "UPDATE TblCantieri " +
                      "SET IdTblClienti = @pIdClienti, Data = CONVERT(date,@pData), " +
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
                cmd.Parameters.Add(new SqlParameter("pIdClienti", c.IdtblClienti));
                cmd.Parameters.Add(new SqlParameter("pData", c.Data));
                cmd.Parameters.Add(new SqlParameter("pCodCant", c.CodCant));
                cmd.Parameters.Add(new SqlParameter("pDescrCant", c.DescriCodCAnt));
                cmd.Parameters.Add(new SqlParameter("pIndir", c.Indirizzo));
                cmd.Parameters.Add(new SqlParameter("pCitta", c.Città));
                cmd.Parameters.Add(new SqlParameter("pRicarico", c.Ricarico));
                cmd.Parameters.Add(new SqlParameter("pPrezzoManodop", c.PzzoManodopera));
                cmd.Parameters.Add(new SqlParameter("pChiuso", c.Chiuso));
                cmd.Parameters.Add(new SqlParameter("pRiscosso", c.Riscosso));
                cmd.Parameters.Add(new SqlParameter("pNumero", c.Numero));
                cmd.Parameters.Add(new SqlParameter("pValPrev", c.ValorePreventivo));
                cmd.Parameters.Add(new SqlParameter("pIva", c.Iva));
                cmd.Parameters.Add(new SqlParameter("pAnno", c.Anno));
                cmd.Parameters.Add(new SqlParameter("pPrev", c.Preventivo));
                cmd.Parameters.Add(new SqlParameter("pFascia", c.FasciaTblCantieri));
                cmd.Parameters.Add(new SqlParameter("pDaDividere", c.DaDividere));
                cmd.Parameters.Add(new SqlParameter("pDiviso", c.Diviso));
                cmd.Parameters.Add(new SqlParameter("pFatturato", c.Fatturato));
                cmd.Parameters.Add(new SqlParameter("pId", idCant));

                int row = cmd.ExecuteNonQuery();

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'aggiornamento del cantiere " + idCant, ex);
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