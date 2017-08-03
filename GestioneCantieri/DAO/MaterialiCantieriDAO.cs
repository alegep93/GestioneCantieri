using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestioneCantieri.DAO
{
    public class MaterialiCantieriDAO : BaseDAO
    {
        //SELECT
        public static List<MaterialiCantieri> GetMaterialeCantiere(string id, string codArt, string descr)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            List<MaterialiCantieri> matList = new List<MaterialiCantieri>();
            string sql = "";

            codArt = "%" + codArt + "%";
            descr = "%" + descr + "%";

            try
            {
                sql = "SELECT TOP 1000 IdMaterialiCantiere,IdTblCantieri,DescriMateriali,Qta,Visibile,Ricalcolo, " +
                      "ricaricoSiNo,Data,PzzoUniCantiere,CodArt,DescriCodArt,Tipologia,Fascia,Acquirente,Fornitore, " +
                      "NumeroBolla,ProtocolloInterno,Note,PzzoFinCli " +
                      "FROM TblMaterialiCantieri " +
                      "WHERE CodArt LIKE @codArt AND DescriCodArt LIKE @descriCodArt ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("CodArt", codArt));
                cmd.Parameters.Add(new SqlParameter("descriCodArt", descr));

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    MaterialiCantieri mc = new MaterialiCantieri();
                    mc.IdMaterialiCantieri = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    mc.IdTblCantieri = (dr.IsDBNull(1) ? -1 : dr.GetInt32(1));
                    mc.DescriMateriali = (dr.IsDBNull(2) ? null : dr.GetString(2));
                    mc.Qta = (dr.IsDBNull(3) ? -1.0d : dr.GetDouble(3));
                    mc.Visibile = (dr.IsDBNull(4) ? false : dr.GetBoolean(4));
                    mc.Ricalcolo = (dr.IsDBNull(5) ? false : dr.GetBoolean(5));
                    mc.RicaricoSiNo = (dr.IsDBNull(6) ? false : dr.GetBoolean(6));
                    mc.Data = (dr.IsDBNull(7) ? new DateTime() : dr.GetDateTime(7));
                    mc.PzzoUniCantiere = (dr.IsDBNull(8) ? -1.0m : dr.GetDecimal(8));
                    mc.CodArt = (dr.IsDBNull(9) ? null : dr.GetString(9));
                    mc.DescriCodArt = (dr.IsDBNull(10) ? null : dr.GetString(10));
                    mc.Tipologia = (dr.IsDBNull(11) ? null : dr.GetString(11));
                    mc.Fascia = (dr.IsDBNull(12) ? -1 : dr.GetInt32(12));
                    mc.Acquirente = (dr.IsDBNull(13) ? null : dr.GetString(13));
                    mc.Fornitore = (dr.IsDBNull(14) ? null : dr.GetString(14));
                    mc.NumeroBolla = (dr.IsDBNull(15) ? -1 : dr.GetInt32(15));
                    mc.ProtocolloInterno = (dr.IsDBNull(16) ? -1 : dr.GetInt32(16));
                    mc.Note = (dr.IsDBNull(17) ? null : dr.GetString(17));
                    mc.PzzoFinCli = (dr.IsDBNull(18) ? -1.0m : dr.GetDecimal(18));
                    matList.Add(mc);
                }

                return matList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei materiali di cantiere", ex);
            }
            finally { cn.Close(); dr.Close(); }
        }
        public static List<MaterialiCantieri> GetMaterialeCantiere(string id)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            List<MaterialiCantieri> matList = new List<MaterialiCantieri>();
            string sql = "";

            try
            {
                sql = "SELECT IdMaterialiCantiere,IdTblCantieri,DescriMateriali,Qta,Visibile,Ricalcolo, " +
                      "ricaricoSiNo,Data,PzzoUniCantiere,CodArt,DescriCodArt,Tipologia,Fascia,Acquirente,Fornitore, " +
                      "NumeroBolla,ProtocolloInterno,Note,PzzoFinCli " +
                      "FROM TblMaterialiCantieri " +
                      "WHERE IdTblCantieri = @Id ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("Id", id));

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    MaterialiCantieri mc = new MaterialiCantieri();
                    mc.IdMaterialiCantieri = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    mc.IdTblCantieri = (dr.IsDBNull(1) ? -1 : dr.GetInt32(1));
                    mc.DescriMateriali = (dr.IsDBNull(2) ? null : dr.GetString(2));
                    mc.Qta = (dr.IsDBNull(3) ? -1.0d : dr.GetDouble(3));
                    mc.Visibile = (dr.IsDBNull(4) ? false : dr.GetBoolean(4));
                    mc.Ricalcolo = (dr.IsDBNull(5) ? false : dr.GetBoolean(5));
                    mc.RicaricoSiNo = (dr.IsDBNull(6) ? false : dr.GetBoolean(6));
                    mc.Data = (dr.IsDBNull(7) ? new DateTime() : dr.GetDateTime(7));
                    mc.PzzoUniCantiere = (dr.IsDBNull(8) ? -1.0m : dr.GetDecimal(8));
                    mc.CodArt = (dr.IsDBNull(9) ? null : dr.GetString(9));
                    mc.DescriCodArt = (dr.IsDBNull(10) ? null : dr.GetString(10));
                    mc.Tipologia = (dr.IsDBNull(11) ? null : dr.GetString(11));
                    mc.Fascia = (dr.IsDBNull(12) ? -1 : dr.GetInt32(12));
                    mc.Acquirente = (dr.IsDBNull(13) ? null : dr.GetString(13));
                    mc.Fornitore = (dr.IsDBNull(14) ? null : dr.GetString(14));
                    mc.NumeroBolla = (dr.IsDBNull(15) ? -1 : dr.GetInt32(15));
                    mc.ProtocolloInterno = (dr.IsDBNull(16) ? -1 : dr.GetInt32(16));
                    mc.Note = (dr.IsDBNull(17) ? null : dr.GetString(17));
                    mc.PzzoFinCli = (dr.IsDBNull(18) ? -1.0m : dr.GetDecimal(18));
                    matList.Add(mc);
                }

                return matList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei materiali di cantiere per singolo cantiere", ex);
            }
            finally { cn.Close(); dr.Close(); }
        }
        public static List<MaterialiCantieri> GetMaterialeCantiere(string dataInizio, string dataFine, string acquirente, string fornitore, string n_ddt)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            List<MaterialiCantieri> matList = new List<MaterialiCantieri>();
            string sql = "";

            fornitore = "%" + fornitore + "%";
            acquirente = "%" + acquirente + "%";
            n_ddt = "%" + n_ddt + "%";

            try
            {
                sql = "SELECT IdMaterialiCantiere,D.CodCant,DescriMateriali,Qta,Visibile,Ricalcolo, " +
                      "ricaricoSiNo,A.Data,PzzoUniCantiere,CodArt,DescriCodArt,Tipologia,Fascia,C.NomeOp AS 'Acquirente',B.RagSocForni AS 'Fornitore', " +
                      "NumeroBolla,ProtocolloInterno,Note,PzzoFinCli " +
                      "FROM TblMaterialiCantieri AS A " +
                      "LEFT JOIN TblForitori AS B ON(A.Fornitore = B.IdFornitori) " +
                      "LEFT JOIN TblOperaio AS C ON(A.Acquirente = C.IdOperaio) " +
                      "LEFT JOIN TblCantieri AS D ON (A.IdTblCantieri = D.IdCantieri)" +
                      "WHERE (A.Data BETWEEN Convert(date,@pDataInizio) AND Convert(date,@pDataFine)) AND C.NomeOp LIKE @pAcquirente AND B.RagSocForni LIKE @pFornitore AND NumeroBolla LIKE @pN_DDT  ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pDataInizio", dataInizio));
                cmd.Parameters.Add(new SqlParameter("pDataFine", dataFine));
                cmd.Parameters.Add(new SqlParameter("pAcquirente", acquirente));
                cmd.Parameters.Add(new SqlParameter("pFornitore", fornitore));
                cmd.Parameters.Add(new SqlParameter("pN_DDT", n_ddt));

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    MaterialiCantieri mc = new MaterialiCantieri();
                    mc.IdMaterialiCantieri = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    mc.CodCant = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    mc.DescriMateriali = (dr.IsDBNull(2) ? null : dr.GetString(2));
                    mc.Qta = (dr.IsDBNull(3) ? -1.0d : dr.GetDouble(3));
                    mc.Visibile = (dr.IsDBNull(4) ? false : dr.GetBoolean(4));
                    mc.Ricalcolo = (dr.IsDBNull(5) ? false : dr.GetBoolean(5));
                    mc.RicaricoSiNo = (dr.IsDBNull(6) ? false : dr.GetBoolean(6));
                    mc.Data = (dr.IsDBNull(7) ? new DateTime() : dr.GetDateTime(7));
                    mc.PzzoUniCantiere = (dr.IsDBNull(8) ? -1.0m : dr.GetDecimal(8));
                    mc.CodArt = (dr.IsDBNull(9) ? null : dr.GetString(9));
                    mc.DescriCodArt = (dr.IsDBNull(10) ? null : dr.GetString(10));
                    mc.Tipologia = (dr.IsDBNull(11) ? null : dr.GetString(11));
                    mc.Fascia = (dr.IsDBNull(12) ? -1 : dr.GetInt32(12));
                    mc.Acquirente = (dr.IsDBNull(13) ? null : dr.GetString(13));
                    mc.Fornitore = (dr.IsDBNull(14) ? null : dr.GetString(14));
                    mc.NumeroBolla = (dr.IsDBNull(15) ? -1 : dr.GetInt32(15));
                    mc.ProtocolloInterno = (dr.IsDBNull(16) ? -1 : dr.GetInt32(16));
                    mc.Note = (dr.IsDBNull(17) ? null : dr.GetString(17));
                    mc.PzzoFinCli = (dr.IsDBNull(18) ? -1.0m : dr.GetDecimal(18));
                    matList.Add(mc);
                }

                return matList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei materiali di cantiere per singolo cantiere", ex);
            }
            finally { cn.Close(); dr.Close(); }
        }
        public static List<MaterialiCantieri> GetMaterialeCantierePerNomeCant(string nomeCant)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            List<MaterialiCantieri> matList = new List<MaterialiCantieri>();
            string sql = "";

            nomeCant = "%" + nomeCant + "%";

            try
            {
                sql = "SELECT IdMaterialiCantiere,B.DescriCodCAnt,DescriMateriali,Qta,Visibile,Ricalcolo, " +
                      "ricaricoSiNo,A.Data,PzzoUniCantiere,CodArt,DescriCodArt,Tipologia,Fascia,Acquirente,Fornitore, " +
                      "NumeroBolla,ProtocolloInterno,Note,PzzoFinCli,B.CodCant " +
                      "FROM TblMaterialiCantieri AS A " +
                      "LEFT JOIN TblCantieri AS B ON (A.IdTblCantieri = B.IdCantieri) " +
                      "WHERE B.DescriCodCAnt LIKE @nomeCant ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("nomeCant", nomeCant));

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    MaterialiCantieri mc = new MaterialiCantieri();
                    mc.IdMaterialiCantieri = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    mc.DescriCodCant = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    mc.DescriMateriali = (dr.IsDBNull(2) ? null : dr.GetString(2));
                    mc.Qta = (dr.IsDBNull(3) ? -1.0d : dr.GetDouble(3));
                    mc.Visibile = (dr.IsDBNull(4) ? false : dr.GetBoolean(4));
                    mc.Ricalcolo = (dr.IsDBNull(5) ? false : dr.GetBoolean(5));
                    mc.RicaricoSiNo = (dr.IsDBNull(6) ? false : dr.GetBoolean(6));
                    mc.Data = (dr.IsDBNull(7) ? new DateTime() : dr.GetDateTime(7));
                    mc.PzzoUniCantiere = (dr.IsDBNull(8) ? -1.0m : dr.GetDecimal(8));
                    mc.CodArt = (dr.IsDBNull(9) ? null : dr.GetString(9));
                    mc.DescriCodArt = (dr.IsDBNull(10) ? null : dr.GetString(10));
                    mc.Tipologia = (dr.IsDBNull(11) ? null : dr.GetString(11));
                    mc.Fascia = (dr.IsDBNull(12) ? -1 : dr.GetInt32(12));
                    mc.Acquirente = (dr.IsDBNull(13) ? null : dr.GetString(13));
                    mc.Fornitore = (dr.IsDBNull(14) ? null : dr.GetString(14));
                    mc.NumeroBolla = (dr.IsDBNull(15) ? -1 : dr.GetInt32(15));
                    mc.ProtocolloInterno = (dr.IsDBNull(16) ? -1 : dr.GetInt32(16));
                    mc.Note = (dr.IsDBNull(17) ? null : dr.GetString(17));
                    mc.PzzoFinCli = (dr.IsDBNull(18) ? -1.0m : dr.GetDecimal(18));
                    mc.CodCant = (dr.IsDBNull(19) ? null : dr.GetString(19));
                    matList.Add(mc);
                }

                return matList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei materiali di cantiere per singolo cantiere filtrato per nome", ex);
            }
            finally { cn.Close(); dr.Close(); }
        }
        public static List<MaterialiCantieri> GetMatCantPerResocontoOperaio(string dataInizio, string dataFine, string acquirente)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            List<MaterialiCantieri> matCantList = new List<MaterialiCantieri>();
            string sql = "";

            try
            {
                sql = "SELECT A.Data,C.NomeOp,B.CodCant,B.DescriCodCAnt,A.Qta,C.CostoOperaio,A.OperaioPagato " +
                      "FROM TblMaterialiCantieri AS A " +
                      "LEFT JOIN TblCantieri AS B ON(A.IdTblCantieri = B.IdCantieri) " +
                      "LEFT JOIN TblOperaio AS C ON(A.Acquirente = C.IdOperaio) " +
                      "WHERE (A.Data BETWEEN Convert(date, @pDataInizio) AND Convert(date, @pDataFine)) AND C.NomeOp LIKE @pAcquirente AND A.OperaioPagato = 0 ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.Parameters.Add(new SqlParameter("pDataInizio", dataInizio));
                cmd.Parameters.Add(new SqlParameter("pDataFine", dataFine));
                cmd.Parameters.Add(new SqlParameter("pAcquirente", acquirente));

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    MaterialiCantieri mc = new MaterialiCantieri();
                    mc.Data = (dr.IsDBNull(0) ? new DateTime() : dr.GetDateTime(0));
                    mc.Acquirente = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    mc.CodCant = (dr.IsDBNull(2) ? null : dr.GetString(2));
                    mc.DescriCodCant = (dr.IsDBNull(3) ? null : dr.GetString(3));
                    mc.Qta = (dr.IsDBNull(4) ? -0d : dr.GetDouble(4));
                    mc.CostoOperaio = (dr.IsDBNull(5) ? -0m : dr.GetDecimal(5));
                    mc.OperaioPagato = (dr.IsDBNull(6) ? false : dr.GetBoolean(6));
                    matCantList.Add(mc);
                }

                return matCantList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei dati per il resoconto operaio", ex);
            }
            finally { cn.Close(); dr.Close(); }
        }

        //Calcolo Totali
        public static decimal TotMaterialeVisibile(string idCant)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            MaterialiCantieri mc = new MaterialiCantieri();
            decimal totMatVisibile = 0m;
            string sql;

            try
            {
                sql = "SELECT IdTblCantieri,PzzoUniCantiere,Qta,Visibile " +
                      "FROM TblMaterialiCantieri " +
                      "WHERE Tipologia = 'MATERIALE' AND Visibile = 1 AND Ricalcolo = 1 AND IdTblCantieri = @pIdCant ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", idCant));

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    mc.PzzoUniCantiere = (dr.IsDBNull(1) ? 0m : dr.GetDecimal(1));
                    mc.Qta = (dr.IsDBNull(2) ? 0d : dr.GetDouble(2));
                    totMatVisibile += mc.PzzoUniCantiere * Convert.ToInt32(mc.Qta);
                }

                return totMatVisibile;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il calcolo del materiale visibile", ex);
            }
            finally { cn.Close(); dr.Close(); }
        }
        public static decimal TotNascosto(string idCant)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            MaterialiCantieri mc = new MaterialiCantieri();
            decimal totMatNascosto = 0m;
            string sql;

            try
            {
                sql = "SELECT IdTblCantieri,PzzoUniCantiere,Qta,Visibile " +
                      "FROM TblMaterialiCantieri " +
                      "WHERE Visibile = 0 AND IdTblCantieri = @pIdCant ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", idCant));
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    mc.PzzoUniCantiere = (dr.IsDBNull(1) ? 0m : dr.GetDecimal(1));
                    mc.Qta = (dr.IsDBNull(2) ? 0d : dr.GetDouble(2));
                    totMatNascosto += mc.PzzoUniCantiere * Convert.ToInt32(mc.Qta);
                }

                return totMatNascosto;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il calcolo del materiale visibile", ex);
            }
            finally { cn.Close(); dr.Close(); }
        }

        //Valore Ricalcolo
        public static List<decimal> CalcolaValoreRicalcolo(string idCant, decimal perc)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            List<decimal> decList = new List<decimal>();
            string sql;

            try
            {
                sql = "SELECT ((PzzoUniCantiere * @pPerc)/100) AS 'Valore Ricalcolo' " +
                      "FROM TblMaterialiCantieri " +
                      "WHERE Visibile = 1 AND Ricalcolo = 1 AND IdTblCantieri = @pIdCant ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", idCant));
                cmd.Parameters.Add(new SqlParameter("pPerc", perc));
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    MaterialiCantieri mc = new MaterialiCantieri();
                    mc.ValoreRicalcolo = (dr.IsDBNull(0) ? 0m : dr.GetDecimal(0));
                    decList.Add(mc.ValoreRicalcolo);
                }

                return decList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il calcolo del materiale visibile", ex);
            }
            finally { cn.Close(); dr.Close(); }
        }
        //Valore Ricarico
        public static List<decimal> CalcolaValoreRicarico(string idCant)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            List<decimal> decList = new List<decimal>();
            string sql;

            try
            {
                sql = "SELECT (((A.PzzoUniCantiere * B.Ricarico)/100)) AS 'Valore Ricarico' " +
                      "FROM TblMaterialiCantieri AS A " +
                      "LEFT JOIN TblCantieri AS B ON(A.IdTblCantieri = B.IdCantieri) " +
                      "WHERE Visibile = 1 AND ricaricoSiNo = 1 AND IdTblCantieri = @pIdCant ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", idCant));
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    MaterialiCantieri mc = new MaterialiCantieri();
                    mc.ValoreRicarico = (dr.IsDBNull(0) ? 0m : dr.GetDecimal(0));
                    decList.Add(mc.ValoreRicarico);
                }

                return decList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il calcolo del materiale visibile", ex);
            }
            finally { cn.Close(); dr.Close(); }
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
                      "FROM TblMaterialiCantieri AS A " +
                      "LEFT JOIN TblCantieri AS B ON(A.IdTblCantieri = B.IdCantieri) " +
                      "LEFT JOIN TblClienti AS C ON(B.IdTblClienti = C.IdCliente) " +
                      "WHERE IdTblCantieri = @pIdCant ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.Parameters.Add(new SqlParameter("pIdCant", idCant));

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    mc.RagSocCli = (dr.IsDBNull(0) ? null : dr.GetString(0));
                    mc.CodCant = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    mc.DescriCodCant = (dr.IsDBNull(2) ? null : dr.GetString(2));
                }

                return mc;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei dati per l'intestazione del conto fin. cli.", ex);
            }
            finally { cn.Close(); dr.Close(); }
        }

        //INSERT
        public static bool InserisciMaterialeCantiere(MaterialiCantieri mc)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblMaterialiCantieri (IdTblCantieri,DescriMateriali,Qta,Visibile,Ricalcolo,ricaricoSiNo,Data, " +
                      "PzzoUniCantiere,CodArt,DescriCodArt,Tipologia,Fascia,Acquirente,Fornitore,NumeroBolla,ProtocolloInterno,Note,pzzoFinCli) " +
                      "VALUES (@pIdCant,@pDescrMat,@pQta,@pVisibile,@pRicalcolo,@pRicarico,@pData,@pPzzoUnit,@pCodArt,@pDescriCodArt,@pTipologia,@pFascia, " +
                      "@pAcquirente,@pFornitore,@pNumBolla,@pProtocollo,@pNote,@pPzzoFinCli)";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", mc.IdTblCantieri));
                cmd.Parameters.Add(new SqlParameter("pDescrMat", mc.DescriMateriali));
                cmd.Parameters.Add(new SqlParameter("pQta", mc.Qta));
                cmd.Parameters.Add(new SqlParameter("pVisibile", mc.Visibile));
                cmd.Parameters.Add(new SqlParameter("pRicalcolo", mc.Ricalcolo));
                cmd.Parameters.Add(new SqlParameter("pRicarico", mc.RicaricoSiNo));
                cmd.Parameters.Add(new SqlParameter("pData", mc.Data));
                cmd.Parameters.Add(new SqlParameter("pPzzoUnit", mc.PzzoUniCantiere));
                cmd.Parameters.Add(new SqlParameter("pCodArt", mc.CodArt));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt", mc.DescriCodArt));
                cmd.Parameters.Add(new SqlParameter("pTipologia", mc.Tipologia));
                cmd.Parameters.Add(new SqlParameter("pFascia", mc.Fascia));
                cmd.Parameters.Add(new SqlParameter("pAcquirente", mc.Acquirente));
                cmd.Parameters.Add(new SqlParameter("pFornitore", mc.Fornitore));
                cmd.Parameters.Add(new SqlParameter("pNumBolla", mc.NumeroBolla));
                cmd.Parameters.Add(new SqlParameter("pProtocollo", mc.ProtocolloInterno));
                cmd.Parameters.Add(new SqlParameter("pNote", mc.Note));
                cmd.Parameters.Add(new SqlParameter("pPzzoFinCli", mc.PzzoFinCli));

                int row = cmd.ExecuteNonQuery();

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un materiale cantiere", ex);
            }
            finally { cn.Close(); }
        }
        public static bool InserisciOperaio(MaterialiCantieri mc)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblMaterialiCantieri (IdTblCantieri,DescriMateriali,Qta,Tipologia,Visibile,Ricalcolo,ricaricoSiNo,Data,Note,PzzoFinCli) " +
                      "VALUES (@pIdCant,@pDescrMat,@pQta,@pTipol,@pVisibile,@pRicalcolo,@pRicarico,@pData,@pNote,'')";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", mc.IdTblCantieri));
                cmd.Parameters.Add(new SqlParameter("pDescrMat", mc.DescriMateriali));
                cmd.Parameters.Add(new SqlParameter("pQta", mc.Qta));
                cmd.Parameters.Add(new SqlParameter("pTipol", mc.Tipologia));
                cmd.Parameters.Add(new SqlParameter("pVisibile", mc.Visibile));
                cmd.Parameters.Add(new SqlParameter("pRicarico", mc.RicaricoSiNo));
                cmd.Parameters.Add(new SqlParameter("pData", mc.Data));
                cmd.Parameters.Add(new SqlParameter("pNote", mc.Note));

                if (mc.Ricalcolo == false)
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
        public static bool InserisciArrotondamento(MaterialiCantieri mc)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblMaterialiCantieri (IdTblCantieri,Qta,Visibile,Tipologia,Ricalcolo,ricaricoSiNo,Data,PzzoUniCantiere,CodArt,DescriCodArt,PzzoFinCli) " +
                      "VALUES (@pIdCant,@pQta,@pVisibile,@pTipol,@pRicalcolo,@pRicarico,@pData,@pPzzoUnit,@pCodArt,@pDescrCodArt,'')";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", mc.IdTblCantieri));
                cmd.Parameters.Add(new SqlParameter("pPzzoUnit", mc.PzzoUniCantiere));
                cmd.Parameters.Add(new SqlParameter("pCodArt", mc.CodArt));
                cmd.Parameters.Add(new SqlParameter("pDescrCodArt", mc.DescriCodArt));
                cmd.Parameters.Add(new SqlParameter("pData", mc.Data));
                cmd.Parameters.Add(new SqlParameter("pQta", mc.Qta));
                cmd.Parameters.Add(new SqlParameter("pTipol", mc.Tipologia));

                if (mc.Visibile == false)
                    cmd.Parameters.Add(new SqlParameter("pVisibile", DBNull.Value));
                if (mc.Ricalcolo == false)
                    cmd.Parameters.Add(new SqlParameter("pRicalcolo", DBNull.Value));
                if (mc.RicaricoSiNo == false)
                    cmd.Parameters.Add(new SqlParameter("pRicarico", DBNull.Value));

                int row = cmd.ExecuteNonQuery();

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un arrotondamento", ex);
            }
            finally { cn.Close(); }
        }
        public static bool InserisciPagamento(string idCant, string operaio, string qta, string tipologia, string pzzoManodop, string descrManodop,
            string note1, string note2, bool visibile, bool ricaricoSiNo, bool? ricalcolo = null)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblMaterialiCantieri (IdTblCantieri,DescriMateriali,Qta,Tipologia,Visibile,Ricalcolo,ricaricoSiNo,Note,PzzoFinCli) " +
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
        public static bool InserisciManodopera(MaterialiCantieri mc)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblMaterialiCantieri (IdTblCantieri,DescriMateriali,Qta,Tipologia,Visibile,Ricalcolo,ricaricoSiNo,Data,Note,PzzoFinCli) " +
                      "VALUES (@pIdCant,@pDescrMat,@pQta,@pTipologia,@pVisibile,@pRicalcolo,@pRicarico,@pData,@pNote,'')";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", mc.IdTblCantieri));
                cmd.Parameters.Add(new SqlParameter("pDescrMat", mc.DescriMateriali));
                cmd.Parameters.Add(new SqlParameter("pQta", mc.Qta));
                cmd.Parameters.Add(new SqlParameter("pTipologia", mc.Tipologia));
                cmd.Parameters.Add(new SqlParameter("pVisibile", mc.Visibile));
                cmd.Parameters.Add(new SqlParameter("pData", mc.Data));
                cmd.Parameters.Add(new SqlParameter("pNote", mc.Note));

                if (mc.Ricalcolo == false)
                    cmd.Parameters.Add(new SqlParameter("pRicalcolo", DBNull.Value));
                if (mc.RicaricoSiNo == false)
                    cmd.Parameters.Add(new SqlParameter("pRicarico", DBNull.Value));

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
        public static bool InserisciSpesa(MaterialiCantieri mc)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblMaterialiCantieri (IdTblCantieri,DescriMateriali,Qta,Tipologia,Visibile,Ricalcolo,ricaricoSiNo,Note,PzzoFinCli) " +
                      "VALUES (@pIdCant,@pDescrMat,@pQta,@pTipologia,@pVisibile,@pRicalcolo,@pRicarico,@pNote,'')";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", mc.IdTblCantieri));
                cmd.Parameters.Add(new SqlParameter("pDescrMat", mc.DescriMateriali));
                cmd.Parameters.Add(new SqlParameter("pQta", mc.Qta));
                cmd.Parameters.Add(new SqlParameter("pTipologia", mc.Tipologia));
                cmd.Parameters.Add(new SqlParameter("pNote", mc.Note));

                if (mc.Visibile == false)
                    cmd.Parameters.Add(new SqlParameter("pVisibile", DBNull.Value));
                if (mc.Ricalcolo == false)
                    cmd.Parameters.Add(new SqlParameter("pRicalcolo", DBNull.Value));
                if (mc.RicaricoSiNo == false)
                    cmd.Parameters.Add(new SqlParameter("pRicarico", DBNull.Value));

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

        //UPDATE
        public static bool UpdateOperaioPagato(string dataInizio, string dataFine, string idOperaio)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "UPDATE TblMaterialiCantieri " +
                      "SET OperaioPagato=1 " +
                      "WHERE (Data BETWEEN Convert(date, @pDataInizio) AND Convert(date, @pDataFine)) AND Acquirente=@pIdOperaio ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pDataInizio", dataInizio));
                cmd.Parameters.Add(new SqlParameter("pDataFine", dataFine));
                cmd.Parameters.Add(new SqlParameter("pIdOperaio", idOperaio));

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'update del campo OperaioPagato", ex);
            }
            finally { cn.Close(); }
        }
    }
}