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
                    mc.DescriMateriali = (dr.IsDBNull(2) ? "" : dr.GetString(2));
                    mc.Qta = (dr.IsDBNull(3) ? -1.0d : dr.GetDouble(3));
                    mc.Visibile = (dr.IsDBNull(4) ? false : dr.GetBoolean(4));
                    mc.Ricalcolo = (dr.IsDBNull(5) ? false : dr.GetBoolean(5));
                    mc.RicaricoSiNo = (dr.IsDBNull(6) ? false : dr.GetBoolean(6));
                    mc.Data = (dr.IsDBNull(7) ? new DateTime() : dr.GetDateTime(7));
                    mc.PzzoUniCantiere = (dr.IsDBNull(8) ? -1.0m : dr.GetDecimal(8));
                    mc.CodArt = (dr.IsDBNull(9) ? "" : dr.GetString(9));
                    mc.DescriCodArt = (dr.IsDBNull(10) ? "" : dr.GetString(10));
                    mc.Tipologia = (dr.IsDBNull(11) ? "" : dr.GetString(11));
                    mc.Fascia = (dr.IsDBNull(12) ? -1 : dr.GetInt32(12));
                    mc.Acquirente = (dr.IsDBNull(13) ? "" : dr.GetString(13));
                    mc.Fornitore = (dr.IsDBNull(14) ? "" : dr.GetString(14));
                    mc.NumeroBolla = (dr.IsDBNull(15) ? -1 : dr.GetInt32(15));
                    mc.ProtocolloInterno = (dr.IsDBNull(16) ? -1 : dr.GetInt32(16));
                    mc.Note = (dr.IsDBNull(17) ? "" : dr.GetString(17));
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
                    mc.DescriMateriali = (dr.IsDBNull(2) ? "" : dr.GetString(2));
                    mc.Qta = (dr.IsDBNull(3) ? -1.0d : dr.GetDouble(3));
                    mc.Visibile = (dr.IsDBNull(4) ? false : dr.GetBoolean(4));
                    mc.Ricalcolo = (dr.IsDBNull(5) ? false : dr.GetBoolean(5));
                    mc.RicaricoSiNo = (dr.IsDBNull(6) ? false : dr.GetBoolean(6));
                    mc.Data = (dr.IsDBNull(7) ? new DateTime() : dr.GetDateTime(7));
                    mc.PzzoUniCantiere = (dr.IsDBNull(8) ? -1.0m : dr.GetDecimal(8));
                    mc.CodArt = (dr.IsDBNull(9) ? "" : dr.GetString(9));
                    mc.DescriCodArt = (dr.IsDBNull(10) ? "" : dr.GetString(10));
                    mc.Tipologia = (dr.IsDBNull(11) ? "" : dr.GetString(11));
                    mc.Fascia = (dr.IsDBNull(12) ? -1 : dr.GetInt32(12));
                    mc.Acquirente = (dr.IsDBNull(13) ? "" : dr.GetString(13));
                    mc.Fornitore = (dr.IsDBNull(14) ? "" : dr.GetString(14));
                    mc.NumeroBolla = (dr.IsDBNull(15) ? -1 : dr.GetInt32(15));
                    mc.ProtocolloInterno = (dr.IsDBNull(16) ? -1 : dr.GetInt32(16));
                    mc.Note = (dr.IsDBNull(17) ? "" : dr.GetString(17));
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
        public static List<MaterialiCantieri> GetMaterialeCantiereForGridView(string idCant, string codArt, string descr, string tipol)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            List<MaterialiCantieri> matList = new List<MaterialiCantieri>();
            string sql = "";

            codArt = "%" + codArt + "%";
            descr = "%" + descr + "%";

            try
            {
                if (codArt != "%%" || descr != "%%")
                {
                    sql = "SELECT ";
                }
                else
                {
                    sql = "SELECT TOP 500 ";
                }

                sql += "IdMaterialiCantiere,B.CodCant,DescriMateriali,Qta,Visibile,Ricalcolo, " +
                       "ricaricoSiNo,A.Data,PzzoUniCantiere,CodArt,DescriCodArt,Tipologia,Fascia,C.NomeOp,D.RagSocForni, " +
                       "NumeroBolla,ProtocolloInterno,Note,PzzoFinCli,B.DescriCodCAnt " +
                       "FROM TblMaterialiCantieri AS A " +
                       "LEFT JOIN TblCantieri AS B ON(A.IdTblCantieri = b.IdCantieri) " +
                       "LEFT JOIN TblOperaio AS C ON(A.Acquirente = C.IdOperaio) " +
                       "LEFT JOIN TblForitori AS D ON(A.Fornitore = D.IdFornitori) " +
                       "WHERE A.IdTblCantieri = @idCant AND ISNULL(A.CodArt,'') LIKE @codArt AND ISNULL(A.DescriCodArt,'') LIKE @descriCodArt AND Tipologia = @tipol ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("idCant", idCant));
                cmd.Parameters.Add(new SqlParameter("codArt", codArt));
                cmd.Parameters.Add(new SqlParameter("descriCodArt", descr));
                cmd.Parameters.Add(new SqlParameter("tipol", tipol));

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    MaterialiCantieri mc = new MaterialiCantieri();
                    mc.IdMaterialiCantieri = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    mc.CodCant = (dr.IsDBNull(1) ? "" : dr.GetString(1));
                    mc.DescriMateriali = (dr.IsDBNull(2) ? "" : dr.GetString(2));
                    mc.Qta = (dr.IsDBNull(3) ? -1.0d : dr.GetDouble(3));
                    mc.Visibile = (dr.IsDBNull(4) ? false : dr.GetBoolean(4));
                    mc.Ricalcolo = (dr.IsDBNull(5) ? false : dr.GetBoolean(5));
                    mc.RicaricoSiNo = (dr.IsDBNull(6) ? false : dr.GetBoolean(6));
                    mc.Data = (dr.IsDBNull(7) ? new DateTime() : dr.GetDateTime(7));
                    mc.PzzoUniCantiere = (dr.IsDBNull(8) ? -1.0m : dr.GetDecimal(8));
                    mc.CodArt = (dr.IsDBNull(9) ? "" : dr.GetString(9));
                    mc.DescriCodArt = (dr.IsDBNull(10) ? "" : dr.GetString(10));
                    mc.Tipologia = (dr.IsDBNull(11) ? "" : dr.GetString(11));
                    mc.Fascia = (dr.IsDBNull(12) ? -1 : dr.GetInt32(12));
                    mc.Acquirente = (dr.IsDBNull(13) ? "" : dr.GetString(13));
                    mc.Fornitore = (dr.IsDBNull(14) ? "" : dr.GetString(14));
                    mc.NumeroBolla = (dr.IsDBNull(15) ? -1 : dr.GetInt32(15));
                    mc.ProtocolloInterno = (dr.IsDBNull(16) ? -1 : dr.GetInt32(16));
                    mc.Note = (dr.IsDBNull(17) ? "" : dr.GetString(17));
                    mc.PzzoFinCli = (dr.IsDBNull(18) ? -1.0m : dr.GetDecimal(18));
                    mc.DescriCodCant = (dr.IsDBNull(19) ? "" : dr.GetString(19));
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
                    mc.CodCant = (dr.IsDBNull(1) ? "" : dr.GetString(1));
                    mc.DescriMateriali = (dr.IsDBNull(2) ? "" : dr.GetString(2));
                    mc.Qta = (dr.IsDBNull(3) ? -1.0d : dr.GetDouble(3));
                    mc.Visibile = (dr.IsDBNull(4) ? false : dr.GetBoolean(4));
                    mc.Ricalcolo = (dr.IsDBNull(5) ? false : dr.GetBoolean(5));
                    mc.RicaricoSiNo = (dr.IsDBNull(6) ? false : dr.GetBoolean(6));
                    mc.Data = (dr.IsDBNull(7) ? new DateTime() : dr.GetDateTime(7));
                    mc.PzzoUniCantiere = (dr.IsDBNull(8) ? -1.0m : dr.GetDecimal(8));
                    mc.CodArt = (dr.IsDBNull(9) ? "" : dr.GetString(9));
                    mc.DescriCodArt = (dr.IsDBNull(10) ? "" : dr.GetString(10));
                    mc.Tipologia = (dr.IsDBNull(11) ? "" : dr.GetString(11));
                    mc.Fascia = (dr.IsDBNull(12) ? -1 : dr.GetInt32(12));
                    mc.Acquirente = (dr.IsDBNull(13) ? "" : dr.GetString(13));
                    mc.Fornitore = (dr.IsDBNull(14) ? "" : dr.GetString(14));
                    mc.NumeroBolla = (dr.IsDBNull(15) ? -1 : dr.GetInt32(15));
                    mc.ProtocolloInterno = (dr.IsDBNull(16) ? -1 : dr.GetInt32(16));
                    mc.Note = (dr.IsDBNull(17) ? "" : dr.GetString(17));
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

            string descriCant = nomeCant.Split('-')[1].Trim();
            descriCant = "%" + descriCant + "%";

            try
            {
                sql = "SELECT IdMaterialiCantiere,B.DescriCodCAnt,DescriMateriali,Qta,Visibile,Ricalcolo, " +
                      "ricaricoSiNo,A.Data,PzzoUniCantiere,CodArt,DescriCodArt,Tipologia,Fascia,Acquirente,Fornitore, " +
                      "NumeroBolla,ProtocolloInterno,Note,PzzoFinCli,B.CodCant " +
                      "FROM TblMaterialiCantieri AS A " +
                      "LEFT JOIN TblCantieri AS B ON (A.IdTblCantieri = B.IdCantieri) " +
                      "WHERE B.DescriCodCAnt LIKE @nomeCant " +
                      "ORDER BY A.Data ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("nomeCant", descriCant));

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    MaterialiCantieri mc = new MaterialiCantieri();
                    mc.IdMaterialiCantieri = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    mc.DescriCodCant = (dr.IsDBNull(1) ? "" : dr.GetString(1));
                    mc.DescriMateriali = (dr.IsDBNull(2) ? "" : dr.GetString(2));
                    mc.Qta = (dr.IsDBNull(3) ? -1.0d : dr.GetDouble(3));
                    mc.Visibile = (dr.IsDBNull(4) ? false : dr.GetBoolean(4));
                    mc.Ricalcolo = (dr.IsDBNull(5) ? false : dr.GetBoolean(5));
                    mc.RicaricoSiNo = (dr.IsDBNull(6) ? false : dr.GetBoolean(6));
                    mc.Data = (dr.IsDBNull(7) ? new DateTime() : dr.GetDateTime(7));
                    mc.PzzoUniCantiere = (dr.IsDBNull(8) ? -1.0m : dr.GetDecimal(8));
                    mc.CodArt = (dr.IsDBNull(9) ? "" : dr.GetString(9));
                    mc.DescriCodArt = (dr.IsDBNull(10) ? "" : dr.GetString(10));
                    mc.Tipologia = (dr.IsDBNull(11) ? "" : dr.GetString(11));
                    mc.Fascia = (dr.IsDBNull(12) ? -1 : dr.GetInt32(12));
                    mc.Acquirente = (dr.IsDBNull(13) ? "" : dr.GetString(13));
                    mc.Fornitore = (dr.IsDBNull(14) ? "" : dr.GetString(14));
                    mc.NumeroBolla = (dr.IsDBNull(15) ? -1 : dr.GetInt32(15));
                    mc.ProtocolloInterno = (dr.IsDBNull(16) ? -1 : dr.GetInt32(16));
                    mc.Note = (dr.IsDBNull(17) ? "" : dr.GetString(17));
                    mc.PzzoFinCli = (dr.IsDBNull(18) ? -1.0m : dr.GetDecimal(18));
                    mc.CodCant = (dr.IsDBNull(19) ? "" : dr.GetString(19));
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
                sql = "SELECT A.Data,C.NomeOp,B.CodCant,B.DescriCodCAnt,A.Qta,A.PzzoUniCantiere,A.OperaioPagato " +
                      "FROM TblMaterialiCantieri AS A " +
                      "LEFT JOIN TblCantieri AS B ON(A.IdTblCantieri = B.IdCantieri) " +
                      "LEFT JOIN TblOperaio AS C ON(A.Acquirente = C.IdOperaio) " +
                      "WHERE (A.Data BETWEEN Convert(date, @pDataInizio) AND Convert(date, @pDataFine)) AND C.IdOperaio LIKE @pAcquirente AND (A.OperaioPagato = 0 || A.OperaioPagato IS NULL) ";

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
                    mc.Acquirente = (dr.IsDBNull(1) ? "" : dr.GetString(1));
                    mc.CodCant = (dr.IsDBNull(2) ? "" : dr.GetString(2));
                    mc.DescriCodCant = (dr.IsDBNull(3) ? "" : dr.GetString(3));
                    mc.Qta = (dr.IsDBNull(4) ? -0d : dr.GetDouble(4));
                    mc.PzzoUniCantiere = (dr.IsDBNull(5) ? 0m : dr.GetDecimal(5));
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

        public static MaterialiCantieri GetSingleMaterialeCantiere(int id)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            MaterialiCantieri mc = new MaterialiCantieri();
            string sql = "";

            try
            {
                sql = "SELECT IdMaterialiCantiere,IdTblCantieri,DescriMateriali,Qta,Visibile,Ricalcolo, " +
                      "ricaricoSiNo,A.Data,PzzoUniCantiere,CodArt,DescriCodArt,Tipologia,Fascia,A.Acquirente,A.Fornitore, " +
                      "NumeroBolla,ProtocolloInterno,Note,Note2,PzzoFinCli " +
                      "FROM TblMaterialiCantieri AS A " +
                      "LEFT JOIN TblCantieri AS B ON(A.IdTblCantieri = b.IdCantieri) " +
                      "LEFT JOIN TblOperaio AS C ON(A.Acquirente = C.IdOperaio) " +
                      "LEFT JOIN TblForitori AS D ON(A.Fornitore = D.IdFornitori) " +
                      "WHERE idMaterialiCantiere = @id ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("id", id));

                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    mc.IdMaterialiCantieri = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    mc.IdTblCantieri = (dr.IsDBNull(1) ? -1 : dr.GetInt32(1));
                    mc.DescriMateriali = (dr.IsDBNull(2) ? "" : dr.GetString(2));
                    mc.Qta = (dr.IsDBNull(3) ? -1.0d : dr.GetDouble(3));
                    mc.Visibile = (dr.IsDBNull(4) ? false : dr.GetBoolean(4));
                    mc.Ricalcolo = (dr.IsDBNull(5) ? false : dr.GetBoolean(5));
                    mc.RicaricoSiNo = (dr.IsDBNull(6) ? false : dr.GetBoolean(6));
                    mc.Data = (dr.IsDBNull(7) ? new DateTime() : dr.GetDateTime(7));
                    mc.PzzoUniCantiere = (dr.IsDBNull(8) ? -1.0m : dr.GetDecimal(8));
                    mc.CodArt = (dr.IsDBNull(9) ? "" : dr.GetString(9));
                    mc.DescriCodArt = (dr.IsDBNull(10) ? "" : dr.GetString(10));
                    mc.Tipologia = (dr.IsDBNull(11) ? "" : dr.GetString(11));
                    mc.Fascia = (dr.IsDBNull(12) ? -1 : dr.GetInt32(12));
                    mc.Acquirente = (dr.IsDBNull(13) ? "" : dr.GetString(13));
                    mc.Fornitore = (dr.IsDBNull(14) ? "" : dr.GetString(14));
                    mc.NumeroBolla = (dr.IsDBNull(15) ? -1 : dr.GetInt32(15));
                    mc.ProtocolloInterno = (dr.IsDBNull(16) ? -1 : dr.GetInt32(16));
                    mc.Note = (dr.IsDBNull(17) ? "" : dr.GetString(17));
                    mc.Note2 = (dr.IsDBNull(18) ? "" : dr.GetString(18));
                    mc.PzzoFinCli = (dr.IsDBNull(19) ? -1.0m : dr.GetDecimal(19));
                }

                return mc;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero del singolo record dei Materiali Cantieri", ex);
            }
            finally { cn.Close(); dr.Close(); }
        }


        public static List<MaterialiCantieri> GetMaterialeCantiereForRicalcoloConti(string id)
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
                      "WHERE IdTblCantieri = @Id AND Visibile != 0 ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("Id", id));

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    MaterialiCantieri mc = new MaterialiCantieri();
                    mc.IdMaterialiCantieri = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    mc.IdTblCantieri = (dr.IsDBNull(1) ? -1 : dr.GetInt32(1));
                    mc.DescriMateriali = (dr.IsDBNull(2) ? "" : dr.GetString(2));
                    mc.Qta = (dr.IsDBNull(3) ? -1.0d : dr.GetDouble(3));
                    mc.Visibile = (dr.IsDBNull(4) ? false : dr.GetBoolean(4));
                    mc.Ricalcolo = (dr.IsDBNull(5) ? false : dr.GetBoolean(5));
                    mc.RicaricoSiNo = (dr.IsDBNull(6) ? false : dr.GetBoolean(6));
                    mc.Data = (dr.IsDBNull(7) ? new DateTime() : dr.GetDateTime(7));
                    mc.PzzoUniCantiere = (dr.IsDBNull(8) ? -1.0m : dr.GetDecimal(8));
                    mc.CodArt = (dr.IsDBNull(9) ? "" : dr.GetString(9));
                    mc.DescriCodArt = (dr.IsDBNull(10) ? "" : dr.GetString(10));
                    mc.Tipologia = (dr.IsDBNull(11) ? "" : dr.GetString(11));
                    mc.Fascia = (dr.IsDBNull(12) ? -1 : dr.GetInt32(12));
                    mc.Acquirente = (dr.IsDBNull(13) ? "" : dr.GetString(13));
                    mc.Fornitore = (dr.IsDBNull(14) ? "" : dr.GetString(14));
                    mc.NumeroBolla = (dr.IsDBNull(15) ? -1 : dr.GetInt32(15));
                    mc.ProtocolloInterno = (dr.IsDBNull(16) ? -1 : dr.GetInt32(16));
                    mc.Note = (dr.IsDBNull(17) ? "" : dr.GetString(17));
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

        //Recupero i record in base alla tipologia passata come parametro di ingresso
        public static List<MaterialiCantieri> GetMaterialeCantierePerTipologia(string idCant, string dataDa, string dataA, string tipologia)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            List<MaterialiCantieri> matList = new List<MaterialiCantieri>();
            string sql = "";

            try
            {
                sql = "SELECT IdMaterialiCantiere,B.DescriCodCAnt,DescriMateriali,Qta,Visibile,Ricalcolo, " +
                      "ricaricoSiNo,A.Data,PzzoUniCantiere,CodArt,DescriCodArt,Tipologia,Fascia,Acquirente,Fornitore, " +
                      "NumeroBolla,ProtocolloInterno,A.Note,PzzoFinCli,B.CodCant,C.RagSocCli " +
                      "FROM TblMaterialiCantieri AS A " +
                      "LEFT JOIN TblCantieri AS B ON (A.IdTblCantieri = B.IdCantieri) " +
                      "LEFT JOIN TblClienti AS C ON (B.IdTblClienti = C.IdCliente) " +
                      "WHERE Tipologia = @pTipol ";

                if (idCant != "-1")
                    sql += " AND IdTblCantieri = @pIdCant ";
                else
                    sql += " AND A.Data BETWEEN CONVERT(date, @dataDa) AND CONVERT(date, @dataA) ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pTipol", tipologia));
                cmd.Parameters.Add(new SqlParameter("pIdCant", idCant));
                cmd.Parameters.Add(new SqlParameter("dataDa", dataDa));
                cmd.Parameters.Add(new SqlParameter("dataA", dataA));

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    MaterialiCantieri mc = new MaterialiCantieri();
                    mc.IdMaterialiCantieri = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    mc.DescriCodCant = (dr.IsDBNull(1) ? "" : dr.GetString(1));
                    mc.DescriMateriali = (dr.IsDBNull(2) ? "" : dr.GetString(2));
                    mc.Qta = (dr.IsDBNull(3) ? -1.0d : dr.GetDouble(3));
                    mc.Visibile = (dr.IsDBNull(4) ? false : dr.GetBoolean(4));
                    mc.Ricalcolo = (dr.IsDBNull(5) ? false : dr.GetBoolean(5));
                    mc.RicaricoSiNo = (dr.IsDBNull(6) ? false : dr.GetBoolean(6));
                    mc.Data = (dr.IsDBNull(7) ? new DateTime() : dr.GetDateTime(7));
                    mc.PzzoUniCantiere = (dr.IsDBNull(8) ? -1.0m : dr.GetDecimal(8));
                    mc.CodArt = (dr.IsDBNull(9) ? "" : dr.GetString(9));
                    mc.DescriCodArt = (dr.IsDBNull(10) ? "" : dr.GetString(10));
                    mc.Tipologia = (dr.IsDBNull(11) ? "" : dr.GetString(11));
                    mc.Fascia = (dr.IsDBNull(12) ? -1 : dr.GetInt32(12));
                    mc.Acquirente = (dr.IsDBNull(13) ? "" : dr.GetString(13));
                    mc.Fornitore = (dr.IsDBNull(14) ? "" : dr.GetString(14));
                    mc.NumeroBolla = (dr.IsDBNull(15) ? -1 : dr.GetInt32(15));
                    mc.ProtocolloInterno = (dr.IsDBNull(16) ? -1 : dr.GetInt32(16));
                    mc.Note = (dr.IsDBNull(17) ? "" : dr.GetString(17));
                    mc.PzzoFinCli = (dr.IsDBNull(18) ? -1.0m : dr.GetDecimal(18));
                    mc.CodCant = (dr.IsDBNull(19) ? "" : dr.GetString(19));
                    mc.RagSocCli = (dr.IsDBNull(20) ? "" : dr.GetString(20));
                    matList.Add(mc);
                }

                return matList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei materiali di cantiere filtrati per tipologia", ex);
            }
            finally { cn.Close(); dr.Close(); }
        }

        //Recupero le tipologie della tabella materiali cantieri
        public static List<string> GetTipologie()
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            List<string> matList = new List<string>();
            string sql = "";

            try
            {
                sql = "SELECT Tipologia FROM TblMaterialiCantieri ";

                SqlCommand cmd = new SqlCommand(sql, cn);

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    MaterialiCantieri mc = new MaterialiCantieri();
                    mc.Tipologia = (dr.IsDBNull(0) ? "" : dr.GetString(0));
                    matList.Add(mc.Tipologia);
                }

                return matList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero delle tipologie", ex);
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
                sql = "INSERT INTO TblMaterialiCantieri (IdTblCantieri,DescriMateriali,Qta,Tipologia,PzzoUniCantiere,ProtocolloInterno,Visibile,Ricalcolo,ricaricoSiNo,Data,Note,PzzoFinCli) " +
                      "VALUES (@pIdCant,@pDescrMat,@pQta,@pTipol,@pzzoUnit,@protocollo,@pVisibile,@pRicalcolo,@pRicarico,@pData,@pNote,'')";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", mc.IdTblCantieri));
                cmd.Parameters.Add(new SqlParameter("pDescrMat", mc.DescriMateriali));
                cmd.Parameters.Add(new SqlParameter("pQta", mc.Qta));
                cmd.Parameters.Add(new SqlParameter("pTipol", mc.Tipologia));
                cmd.Parameters.Add(new SqlParameter("pzzoUnit", mc.PzzoUniCantiere));
                cmd.Parameters.Add(new SqlParameter("protocollo", mc.ProtocolloInterno));
                cmd.Parameters.Add(new SqlParameter("pVisibile", mc.Visibile));
                cmd.Parameters.Add(new SqlParameter("pRicalcolo", mc.Ricalcolo));
                cmd.Parameters.Add(new SqlParameter("pRicarico", mc.RicaricoSiNo));
                cmd.Parameters.Add(new SqlParameter("pData", mc.Data));
                cmd.Parameters.Add(new SqlParameter("pNote", mc.Note));

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
                sql = "INSERT INTO TblMaterialiCantieri (IdTblCantieri,Qta,Visibile,Tipologia,Ricalcolo,ricaricoSiNo,Data,PzzoUniCantiere,ProtocolloInterno,CodArt,DescriCodArt,PzzoFinCli) " +
                      "VALUES (@pIdCant,@pQta,@pVisibile,@pTipol,@pRicalcolo,@pRicarico,@pData,@pPzzoUnit,@protocollo,@pCodArt,@pDescrCodArt,'')";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", mc.IdTblCantieri));
                cmd.Parameters.Add(new SqlParameter("pPzzoUnit", mc.PzzoUniCantiere));
                cmd.Parameters.Add(new SqlParameter("protocollo", mc.ProtocolloInterno));
                cmd.Parameters.Add(new SqlParameter("pCodArt", mc.CodArt));
                cmd.Parameters.Add(new SqlParameter("pDescrCodArt", mc.DescriCodArt));
                cmd.Parameters.Add(new SqlParameter("pVisibile", mc.Visibile));
                cmd.Parameters.Add(new SqlParameter("pRicalcolo", mc.Ricalcolo));
                cmd.Parameters.Add(new SqlParameter("pRicarico", mc.RicaricoSiNo));
                cmd.Parameters.Add(new SqlParameter("pData", mc.Data));
                cmd.Parameters.Add(new SqlParameter("pQta", mc.Qta));
                cmd.Parameters.Add(new SqlParameter("pTipol", mc.Tipologia));

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
                cmd.Parameters.Add(new SqlParameter("pRicalcolo", ricalcolo));
                cmd.Parameters.Add(new SqlParameter("pRicarico", ricaricoSiNo));
                cmd.Parameters.Add(new SqlParameter("pNote", note1 + " - " + note2));

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
                sql = "INSERT INTO TblMaterialiCantieri (IdTblCantieri,DescriMateriali,CodArt,DescriCodArt,Qta,Tipologia,PzzoUniCantiere, " +
                      "ProtocolloInterno,NumeroBolla,Fascia,Visibile,Ricalcolo,ricaricoSiNo,Data,Note,Note2,PzzoFinCli) " +
                      "VALUES (@pIdCant,@pCodArt,@DescriCodArt,@pDescrMat,@pQta,@pTipologia,@pzzoUnit,@protocollo,@bolla,@fascia,@pVisibile,@pRicalcolo,@pRicarico,@pData,@pNote,@note2,'')";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", mc.IdTblCantieri));
                cmd.Parameters.Add(new SqlParameter("pCodArt", mc.CodArt));
                cmd.Parameters.Add(new SqlParameter("DescriCodArt", mc.DescriCodArt));
                cmd.Parameters.Add(new SqlParameter("pDescrMat", mc.DescriMateriali));
                cmd.Parameters.Add(new SqlParameter("pQta", mc.Qta));
                cmd.Parameters.Add(new SqlParameter("pTipologia", mc.Tipologia));
                cmd.Parameters.Add(new SqlParameter("pzzoUnit", mc.PzzoUniCantiere));
                cmd.Parameters.Add(new SqlParameter("protocollo", mc.ProtocolloInterno));
                cmd.Parameters.Add(new SqlParameter("bolla", mc.NumeroBolla));
                cmd.Parameters.Add(new SqlParameter("fascia", mc.Fascia));
                cmd.Parameters.Add(new SqlParameter("pVisibile", mc.Visibile));
                cmd.Parameters.Add(new SqlParameter("pRicalcolo", mc.Ricalcolo));
                cmd.Parameters.Add(new SqlParameter("pRicarico", mc.RicaricoSiNo));
                cmd.Parameters.Add(new SqlParameter("pData", mc.Data));
                cmd.Parameters.Add(new SqlParameter("pNote", mc.Note));
                cmd.Parameters.Add(new SqlParameter("note2", mc.Note2));

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
                sql = "INSERT INTO TblMaterialiCantieri (IdTblCantieri,DescriMateriali,CodArt,DescriCodArt,Qta,Tipologia,PzzoUniCantiere, " +
                      "ProtocolloInterno,NumeroBolla,Fascia,Visibile,Ricalcolo,ricaricoSiNo,Data,Note,Note2,PzzoFinCli) " +
                      "VALUES (@pIdCant,@pDescrMat,@pQta,@pTipologia,@pzzoUnit,@protocollo,@bolla,@fascia,@pVisibile,@pRicalcolo,@pRicarico,@pData,@pNote,@note2,'')";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", mc.IdTblCantieri));
                cmd.Parameters.Add(new SqlParameter("pDescrMat", mc.CodArt));
                cmd.Parameters.Add(new SqlParameter("DescriCodArt", mc.DescriCodArt));
                cmd.Parameters.Add(new SqlParameter("pDescrMat", mc.DescriMateriali));
                cmd.Parameters.Add(new SqlParameter("pQta", mc.Qta));
                cmd.Parameters.Add(new SqlParameter("pTipologia", mc.Tipologia));
                cmd.Parameters.Add(new SqlParameter("pzzoUnit", mc.PzzoUniCantiere));
                cmd.Parameters.Add(new SqlParameter("protocollo", mc.ProtocolloInterno));
                cmd.Parameters.Add(new SqlParameter("bolla", mc.NumeroBolla));
                cmd.Parameters.Add(new SqlParameter("fascia", mc.Fascia));
                cmd.Parameters.Add(new SqlParameter("pVisibile", mc.Visibile));
                cmd.Parameters.Add(new SqlParameter("pRicalcolo", mc.Ricalcolo));
                cmd.Parameters.Add(new SqlParameter("pRicarico", mc.RicaricoSiNo));
                cmd.Parameters.Add(new SqlParameter("pData", mc.Data));
                cmd.Parameters.Add(new SqlParameter("pNote", mc.Note));
                cmd.Parameters.Add(new SqlParameter("note2", mc.Note2));

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
        public static bool UpdateMatCant(string id, MaterialiCantieri mc)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "UPDATE TblMaterialiCantieri " +
                      "SET IdTblCantieri = @idCant " +
                      ",DescriMateriali = @descrMat " +
                      ",Qta = @qta " +
                      ",Visibile = @visibile " +
                      ",Ricalcolo = @ricalcolo " +
                      ",ricaricoSiNo = @ricarico " +
                      ",Data = @data " +
                      ",PzzoUniCantiere = @pzzoUni " +
                      ",CodArt = @codArt " +
                      ",DescriCodArt = @descriCodArt " +
                      ",Fascia = @fascia " +
                      ",NumeroBolla = @numBolla " +
                      ",ProtocolloInterno = @protocollo " +
                      ",Note = @note " +
                      ",Tipologia = @tipol " +
                      ",PzzoFinCli = @pzzoFinCli " +
                      ",Acquirente = @acquir " +
                      ",Fornitore = @fornit " +
                      ",Note2 = @note2 " +
                      "WHERE IdMaterialiCantiere = @id ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("id", id));
                cmd.Parameters.Add(new SqlParameter("idCant", mc.IdTblCantieri));
                cmd.Parameters.Add(new SqlParameter("descrMat", mc.DescriMateriali));
                cmd.Parameters.Add(new SqlParameter("qta", mc.Qta));
                cmd.Parameters.Add(new SqlParameter("visibile", mc.Visibile));
                cmd.Parameters.Add(new SqlParameter("ricalcolo", mc.Ricalcolo));
                cmd.Parameters.Add(new SqlParameter("ricarico", mc.RicaricoSiNo));
                cmd.Parameters.Add(new SqlParameter("data", mc.Data));
                cmd.Parameters.Add(new SqlParameter("pzzoUni", mc.PzzoUniCantiere));
                cmd.Parameters.Add(new SqlParameter("codArt", mc.CodArt));
                cmd.Parameters.Add(new SqlParameter("descriCodArt", mc.DescriCodArt));
                cmd.Parameters.Add(new SqlParameter("fascia", mc.Fascia));
                cmd.Parameters.Add(new SqlParameter("numBolla", mc.NumeroBolla));
                cmd.Parameters.Add(new SqlParameter("protocollo", mc.ProtocolloInterno));
                cmd.Parameters.Add(new SqlParameter("note", mc.Note));
                cmd.Parameters.Add(new SqlParameter("tipol", mc.Tipologia));
                cmd.Parameters.Add(new SqlParameter("pzzoFinCli", mc.PzzoFinCli));
                cmd.Parameters.Add(new SqlParameter("acquir", mc.Acquirente));
                cmd.Parameters.Add(new SqlParameter("fornit", mc.Fornitore));
                cmd.Parameters.Add(new SqlParameter("note2", mc.Note2));

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'update del record MatCant", ex);
            }
            finally { cn.Close(); }
        }
        public static bool UpdateManodop(string id, MaterialiCantieri mc)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "UPDATE TblMaterialiCantieri " +
                      "SET IdTblCantieri = @idCant " +
                      ",DescriMateriali = @descrMat " +
                      ",CodArt = @codArt " +
                      ",DescriCodArt = @descriCodArt " +
                      ",Qta = @qta " +
                      ",Visibile = @visibile " +
                      ",Ricalcolo = @ricalcolo " +
                      ",ricaricoSiNo = @ricarico " +
                      ",Data = @data " +
                      ",PzzoUniCantiere = @pzzoUni " +
                      ",Fascia = @fascia " +
                      ",NumeroBolla = @numBolla " +
                      ",ProtocolloInterno = @protocollo " +
                      ",Note = @note " +
                      ",Tipologia = @tipol " +
                      ",Acquirente = @acquir " +
                      ",Fornitore = @fornit " +
                      ",Note2 = @note2 " +
                      "WHERE IdMaterialiCantiere = @id ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("id", id));
                cmd.Parameters.Add(new SqlParameter("idCant", mc.IdTblCantieri));
                cmd.Parameters.Add(new SqlParameter("descrMat", mc.DescriMateriali));
                cmd.Parameters.Add(new SqlParameter("codArt", mc.CodArt));
                cmd.Parameters.Add(new SqlParameter("DescriCodArt", mc.DescriCodArt));
                cmd.Parameters.Add(new SqlParameter("qta", mc.Qta));
                cmd.Parameters.Add(new SqlParameter("visibile", mc.Visibile));
                cmd.Parameters.Add(new SqlParameter("ricalcolo", mc.Ricalcolo));
                cmd.Parameters.Add(new SqlParameter("ricarico", mc.RicaricoSiNo));
                cmd.Parameters.Add(new SqlParameter("data", mc.Data));
                cmd.Parameters.Add(new SqlParameter("pzzoUni", mc.PzzoUniCantiere));
                cmd.Parameters.Add(new SqlParameter("fascia", mc.Fascia));
                cmd.Parameters.Add(new SqlParameter("numBolla", mc.NumeroBolla));
                cmd.Parameters.Add(new SqlParameter("protocollo", mc.ProtocolloInterno));
                cmd.Parameters.Add(new SqlParameter("note", mc.Note));
                cmd.Parameters.Add(new SqlParameter("tipol", mc.Tipologia));
                cmd.Parameters.Add(new SqlParameter("acquir", mc.Acquirente));
                cmd.Parameters.Add(new SqlParameter("fornit", mc.Fornitore));
                cmd.Parameters.Add(new SqlParameter("note2", mc.Note2));

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'update del record MatCant", ex);
            }
            finally { cn.Close(); }
        }
        public static bool UpdateValoreManodopera(string id, string valManodop)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "UPDATE TblMaterialiCantieri " +
                      "SET PzzoUniCantiere = @pzzoManodop " +
                      "WHERE IdTblCantieri = @id AND Tipologia = 'MANODOPERA'";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("id", id));
                cmd.Parameters.Add(new SqlParameter("pzzoManodop", valManodop));

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'update del valore della manodopera", ex);
            }
            finally { cn.Close(); }
        }
        public static bool UpdateCostoOperaio(string idCant, string costoOperaio, string idOper)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "UPDATE TblMaterialiCantieri " +
                      "SET PzzoUniCantiere = @pzzoOper " +
                      "WHERE IdTblCantieri = @id AND Acquirente = @idOper AND Tipologia = 'OPERAIO' ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("id", idCant));
                cmd.Parameters.Add(new SqlParameter("pzzoOper", costoOperaio));
                cmd.Parameters.Add(new SqlParameter("idOper", idOper));

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'update del costo Operaio", ex);
            }
            finally { cn.Close(); }
        }

        //DELETE
        public static bool DeleteMatCant(int idMatCant)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "DELETE FROM TblMaterialiCantieri " +
                      "WHERE IdMaterialiCantiere = @id ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("id", idMatCant));

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione di un record per i materialiCantieri", ex);
            }
            finally { cn.Close(); }
        }
    }
}