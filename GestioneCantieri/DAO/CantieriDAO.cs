using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GestioneCantieri.DAO
{
    public class CantieriDAO : BaseDAO
    {
        //SELECT
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
                    cant.Numero = (dr.IsDBNull(11) ? -1 : dr.GetInt32(11));
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
            finally { cn.Close(); dr.Close(); }
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

        public static List<Cantieri> GetCantieri(string anno, int idCliente, bool fatturato, bool chiuso, bool riscosso)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            List<Cantieri> list = new List<Cantieri>();
            string sql = "";

            anno = "%" + anno + "%";

            try
            {
                sql = "SELECT Cant.IdCantieri, Cli.RagSocCli, Cant.CodCant, Cant.DescriCodCAnt, " +
                      "Cant.Data, Cant.Indirizzo, Cant.Città, Cant.Ricarico, " +
                      "Cant.PzzoManodopera, Cant.Chiuso, Cant.Riscosso, Cant.Numero, " +
                      "Cant.ValorePreventivo, Cant.IVA, Cant.Anno, Cant.Preventivo, " +
                      "Cant.FasciaTblCantieri, Cant.DaDividere, Cant.Diviso, Cant.Fatturato " +
                      "FROM TblCantieri AS Cant " +
                      "JOIN TblClienti AS Cli ON (Cant.IdTblClienti = Cli.IdCliente) " +
                      "WHERE Anno LIKE @pAnno " +
                      "AND Chiuso = @pChiuso AND Riscosso = @pRiscosso AND Fatturato = @pFatturato ";

                if (idCliente != -1)
                {
                    sql += "AND IdTblClienti = @idCliente ";
                }

                sql += "ORDER BY Cant.CodCant ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pAnno", anno));
                cmd.Parameters.Add(new SqlParameter("pFatturato", fatturato));
                cmd.Parameters.Add(new SqlParameter("pChiuso", chiuso));
                cmd.Parameters.Add(new SqlParameter("pRiscosso", riscosso));
                cmd.Parameters.Add(new SqlParameter("idCliente", idCliente));

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Cantieri c = new Cantieri();
                    c.IdCantieri = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    c.RagSocCli = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    c.CodCant = (dr.IsDBNull(2) ? null : dr.GetString(2));
                    c.DescriCodCAnt = (dr.IsDBNull(3) ? null : dr.GetString(3));
                    c.Data = (dr.IsDBNull(4) ? new DateTime() : dr.GetDateTime(4));
                    c.Indirizzo = (dr.IsDBNull(5) ? null : dr.GetString(5));
                    c.Città = (dr.IsDBNull(6) ? null : dr.GetString(6));
                    c.Ricarico = (dr.IsDBNull(7) ? -1 : dr.GetInt32(7));
                    c.PzzoManodopera = (dr.IsDBNull(8) ? 0.0m : dr.GetDecimal(8));
                    c.Chiuso = (dr.IsDBNull(9) ? false : dr.GetBoolean(9));
                    c.Riscosso = (dr.IsDBNull(10) ? false : dr.GetBoolean(10));
                    c.Numero = (dr.IsDBNull(11) ? -1 : dr.GetInt32(11));
                    c.ValorePreventivo = (dr.IsDBNull(12) ? 0.0m : dr.GetDecimal(12));
                    c.Iva = (dr.IsDBNull(13) ? -1 : dr.GetInt32(13));
                    c.Anno = (dr.IsDBNull(14) ? -1 : dr.GetInt32(14));
                    c.Preventivo = (dr.IsDBNull(15) ? false : dr.GetBoolean(15));
                    c.FasciaTblCantieri = (dr.IsDBNull(16) ? -1 : dr.GetInt32(16));
                    c.DaDividere = (dr.IsDBNull(17) ? false : dr.GetBoolean(17));
                    c.Diviso = (dr.IsDBNull(18) ? false : dr.GetBoolean(18));
                    c.Fatturato = (dr.IsDBNull(19) ? false : dr.GetBoolean(19));

                    list.Add(c);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero della lista dei cantieri", ex);
            }
            finally { cn.Close(); }

            return list;
        }
        public static List<Cantieri> GetListCantieri()
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string sql = "";
            List<Cantieri> cantieriList = new List<Cantieri>();

            try
            {
                sql = "SELECT IdCantieri,CodCant,DescriCodCAnt FROM TblCantieri " +
                      "WHERE Chiuso = 0 " +
                      "ORDER BY CodCant ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Cantieri c = new Cantieri();
                    c.IdCantieri = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    c.CodCant = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    c.DescriCodCAnt = (dr.IsDBNull(2) ? null : dr.GetString(2));
                    cantieriList.Add(c);
                }

                return cantieriList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei cantieri", ex);
            }
            finally { cn.Close(); }
        }

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
        public static DataTable GetAllCantieri(bool chiuso, bool riscosso)
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
                      "WHERE Cant.Chiuso = 0 AND Cant.Riscosso = 0 " +
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
                    c.Numero = (dr.IsDBNull(11) ? -1 : dr.GetInt32(11));
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
        public static DataTable FiltraCantieri(string anno, string codCant, string descr, string cliente, bool chiuso, bool riscosso)
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

        //Estrazione dati per creazione intestazione Conto Finale Cliente e Verifica Cantieri
        public static MaterialiCantieri GetDataPerIntestazione(string idCant)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            MaterialiCantieri mc = new MaterialiCantieri();
            string sql = "";

            try
            {
                sql = "SELECT C.RagSocCli,B.CodCant,B.DescriCodCAnt " +
                      "FROM TblCantieri AS B " +
                      "LEFT JOIN TblClienti AS C ON (B.IdTblClienti = C.IdCliente) " +
                      "WHERE B.IdCantieri = @pIdCant ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.Parameters.Add(new SqlParameter("pIdCant", idCant));

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    mc.RagSocCli = (dr.IsDBNull(0) ? "" : dr.GetString(0));
                    mc.CodCant = (dr.IsDBNull(1) ? "" : dr.GetString(1));
                    mc.DescriCodCant = (dr.IsDBNull(2) ? "" : dr.GetString(2));
                }

                return mc;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei dati per l'intestazione del conto fin. cli.", ex);
            }
            finally { cn.Close(); dr.Close(); }
        }
    }
}