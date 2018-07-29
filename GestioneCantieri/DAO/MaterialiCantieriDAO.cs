using Dapper;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace GestioneCantieri.DAO
{
    public class MaterialiCantieriDAO : BaseDAO
    {
        //SELECT
        public static List<MaterialiCantieri> GetMaterialeCantiere(string id)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT IdMaterialiCantiere,IdTblCantieri,DescriMateriali,Qta,Visibile,Ricalcolo, " +
                      "ricaricoSiNo,Data,PzzoUniCantiere,CodArt,DescriCodArt,Tipologia,Fascia,Acquirente,Fornitore, " +
                      "NumeroBolla,ProtocolloInterno,Note,PzzoFinCli " +
                      "FROM TblMaterialiCantieri " +
                      "WHERE IdTblCantieri = @Id " +
                      "ORDER BY Tipologia, Data";

                return cn.Query<MaterialiCantieri>(sql, new { Id = id }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei materiali di cantiere per singolo cantiere", ex);
            }
            finally { CloseResouces(cn, null); }
        }
        public static List<MaterialiCantieri> GetMaterialeCantiere(string id, string codArt, string descr)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            codArt = "%" + codArt + "%";
            descr = "%" + descr + "%";

            try
            {
                sql = "SELECT TOP 1000 IdMaterialiCantiere,IdTblCantieri,DescriMateriali,Qta,Visibile,Ricalcolo, " +
                      "ricaricoSiNo,Data,PzzoUniCantiere,CodArt,DescriCodArt,Tipologia,Fascia,Acquirente,Fornitore, " +
                      "NumeroBolla,ProtocolloInterno,Note,PzzoFinCli " +
                      "FROM TblMaterialiCantieri " +
                      "WHERE IdTblCantieri = @pIdCant AND CodArt LIKE @codArt AND DescriCodArt LIKE @descriCodArt ";

                return cn.Query<MaterialiCantieri>(sql, new { pIdCant = id, CodArt = codArt, descriCodArt = descr }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei materiali di cantiere", ex);
            }
            finally { CloseResouces(cn, null); }
        }
        public static List<MaterialiCantieri> GetMaterialeCantiereForGridView(string idCant, string codArt, string descr, string protocollo, string fornitore, string tipol)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            List<MaterialiCantieri> matList = new List<MaterialiCantieri>();
            string sql = "";

            codArt = "%" + codArt + "%";
            descr = "%" + descr + "%";
            fornitore = "%" + fornitore + "%";

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
                       "NumeroBolla,ProtocolloInterno,Note,PzzoFinCli,B.DescriCodCAnt,IdTblOperaio " +
                       "FROM TblMaterialiCantieri AS A " +
                       "LEFT JOIN TblCantieri AS B ON(A.IdTblCantieri = b.IdCantieri) " +
                       "LEFT JOIN TblOperaio AS C ON(A.Acquirente = C.IdOperaio) " +
                       "LEFT JOIN TblForitori AS D ON(A.Fornitore = D.IdFornitori) " +
                       "WHERE A.IdTblCantieri = @idCant AND ISNULL(A.CodArt,'') LIKE @codArt " +
                       "AND ISNULL(A.DescriCodArt,'') LIKE @descriCodArt AND ISNULL(A.ProtocolloInterno,'') LIKE @protocollo AND ISNULL(D.RagSocForni,'') LIKE @fornitore AND Tipologia = @tipol ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("idCant", idCant));
                cmd.Parameters.Add(new SqlParameter("codArt", codArt));
                cmd.Parameters.Add(new SqlParameter("descriCodArt", descr));
                cmd.Parameters.Add(new SqlParameter("tipol", tipol));
                cmd.Parameters.Add(new SqlParameter("fornitore", fornitore));

                if (protocollo == "")
                    cmd.Parameters.Add(new SqlParameter("protocollo", "%%"));
                else
                    cmd.Parameters.Add(new SqlParameter("protocollo", protocollo));

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
                    mc.NumeroBolla = (dr.IsDBNull(15) ? "" : dr.GetString(15));
                    mc.ProtocolloInterno = (dr.IsDBNull(16) ? -1 : dr.GetInt32(16));
                    mc.Note = (dr.IsDBNull(17) ? "" : dr.GetString(17));
                    mc.PzzoFinCli = (dr.IsDBNull(18) ? -1.0m : dr.GetDecimal(18));
                    mc.DescriCodCant = (dr.IsDBNull(19) ? "" : dr.GetString(19));
                    mc.IdOperaio = (dr.IsDBNull(20) ? -1 : dr.GetInt32(20));
                    matList.Add(mc);
                }

                return matList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei materiali di cantiere", ex);
            }
            finally { CloseResouces(cn, null); }
        }
        public static List<MaterialiCantieri> GetMaterialeCantiere(string dataInizio, string dataFine, string acquirente, string fornitore, string n_ddt)
        {
            SqlConnection cn = GetConnection();
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
                      "WHERE (A.Data BETWEEN Convert(date,@pDataInizio) AND Convert(date,@pDataFine)) AND C.NomeOp LIKE @pAcquirente AND B.RagSocForni LIKE @pFornitore AND NumeroBolla LIKE @pN_DDT " +
                      "ORDER BY A.Data, A.NumeroBolla";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pDataInizio", dataInizio));
                cmd.Parameters.Add(new SqlParameter("pDataFine", dataFine));
                cmd.Parameters.Add(new SqlParameter("pAcquirente", acquirente));
                cmd.Parameters.Add(new SqlParameter("pFornitore", fornitore));
                cmd.Parameters.Add(new SqlParameter("pN_DDT", n_ddt));

                return cn.Query<MaterialiCantieri>(sql, new { pDataInizio = dataInizio, pDataFine = dataFine, pAcquirente = acquirente, pFornitore = fornitore, pN_DDT = n_ddt }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei materiali di cantiere per singolo cantiere", ex);
            }
            finally { CloseResouces(cn, null); }
        }
        public static List<MaterialiCantieri> GetMatCantPerResocontoOperaio(string dataInizio, string dataFine, string idOperaio)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT A.Data,C.NomeOp,B.CodCant,B.DescriCodCAnt,A.Qta,A.PzzoUniCantiere,A.OperaioPagato " +
                      "FROM TblMaterialiCantieri AS A " +
                      "LEFT JOIN TblCantieri AS B ON (A.IdTblCantieri = B.IdCantieri) " +
                      "LEFT JOIN TblOperaio AS C ON (A.Acquirente = C.IdOperaio) " +
                      "WHERE (A.Data BETWEEN Convert(date, @pDataInizio) AND Convert(date, @pDataFine)) AND A.IdTblOperaio LIKE @pIdOper AND (A.OperaioPagato = 0 OR A.OperaioPagato IS NULL) ";

                return cn.Query<MaterialiCantieri>(sql, new { pDataInizio = dataInizio, pDataFine = dataFine, pIdOper = idOperaio }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei dati per il resoconto operaio", ex);
            }
            finally { CloseResouces(cn, null); }
        }
        public static List<MaterialiCantieri> GetMatCantPerResocontoOperaio(string dataInizio, string dataFine, string idOperaio, string codCant, bool isOperaioPagato)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            codCant = "%" + codCant + "%";

            try
            {
                sql = "SELECT A.Data,C.NomeOp,B.CodCant,B.DescriCodCAnt,A.Qta,A.PzzoUniCantiere,A.OperaioPagato " +
                      "FROM TblMaterialiCantieri AS A " +
                      "LEFT JOIN TblCantieri AS B ON (A.IdTblCantieri = B.IdCantieri) " +
                      "LEFT JOIN TblOperaio AS C ON (A.Acquirente = C.IdOperaio) " +
                      "WHERE (A.Data BETWEEN Convert(date, @pDataInizio) AND Convert(date, @pDataFine)) AND A.IdTblOperaio LIKE @pIdOper " +
                      "AND B.CodCant LIKE @codCant AND (A.OperaioPagato = @isOperaioPagato OR A.OperaioPagato IS NULL)  ";

                return cn.Query<MaterialiCantieri>(sql, new { pDataInizio = dataInizio, pDataFine = dataFine, pIdOper = idOperaio, isOperaioPagato, codCant }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei dati per il resoconto operaio", ex);
            }
            finally { CloseResouces(cn, null); }
        }
        public static MaterialiCantieri GetSingleMaterialeCantiere(int id)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT IdMaterialiCantiere,IdTblCantieri,DescriMateriali,Qta,Visibile,Ricalcolo, " +
                      "ricaricoSiNo,A.Data,PzzoUniCantiere,CodArt,DescriCodArt,Tipologia,Fascia,A.Acquirente,A.Fornitore, " +
                      "NumeroBolla,ProtocolloInterno,Note,Note2,PzzoFinCli,IdTblOperaio AS IdOperaio " +
                      "FROM TblMaterialiCantieri AS A " +
                      "LEFT JOIN TblCantieri AS B ON(A.IdTblCantieri = b.IdCantieri) " +
                      "LEFT JOIN TblOperaio AS C ON(A.Acquirente = C.IdOperaio) " +
                      "LEFT JOIN TblForitori AS D ON(A.Fornitore = D.IdFornitori) " +
                      "WHERE idMaterialiCantiere = @id ";

                return cn.Query<MaterialiCantieri>(sql, new { id }).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero del singolo record dei Materiali Cantieri", ex);
            }
            finally { CloseResouces(cn, null); }
        }
        public static List<MaterialiCantieri> GetMaterialeCantiereForRicalcoloConti(string id)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT IdMaterialiCantiere,IdTblCantieri,DescriMateriali,Qta,Visibile,Ricalcolo, " +
                      "ricaricoSiNo,Data,PzzoUniCantiere,CodArt,DescriCodArt,Tipologia,Fascia,Acquirente,Fornitore, " +
                      "NumeroBolla,ProtocolloInterno,Note,Note2,PzzoFinCli " +
                      "FROM TblMaterialiCantieri " +
                      "WHERE IdTblCantieri = @Id AND Visibile != 0 " +
                      "ORDER BY Data";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("Id", id));

                return cn.Query<MaterialiCantieri>(sql, new { Id = id }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei materiali di cantiere per singolo cantiere", ex);
            }
            finally { CloseResouces(cn, null); }
        }

        //Recupero i record in base alla tipologia passata come parametro di ingresso
        public static List<MaterialiCantieri> GetMaterialeCantierePerTipologia(string idCant, string dataDa, string dataA, string idOper, string tipologia)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT IdMaterialiCantiere,B.DescriCodCAnt,DescriMateriali,Qta,Visibile,Ricalcolo, " +
                      "ricaricoSiNo,A.Data,PzzoUniCantiere,CodArt,DescriCodArt,Tipologia,Fascia,D.NomeOp,Fornitore, " +
                      "NumeroBolla,ProtocolloInterno,A.Note,PzzoFinCli,B.CodCant,C.RagSocCli " +
                      "FROM TblMaterialiCantieri AS A " +
                      "LEFT JOIN TblCantieri AS B ON (A.IdTblCantieri = B.IdCantieri) " +
                      "LEFT JOIN TblClienti AS C ON (B.IdTblClienti = C.IdCliente) " +
                      "LEFT JOIN TblOperaio AS D ON (A.Acquirente = D.IdOperaio) " +
                      "WHERE Tipologia = @pTipol ";

                if (idCant != "-1")
                    sql += " AND IdTblCantieri = @pIdCant ";
                else
                    sql += " AND A.Data BETWEEN CONVERT(date, @dataDa) AND CONVERT(date, @dataA) ";

                if (idOper != "-1")
                    sql += " AND A.IdTblOperaio = @pIdOper ";

                return cn.Query<MaterialiCantieri>(sql, new { pTipol = tipologia, pIdCant = idCant, dataDa, dataA, pIdOper = idOper }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei materiali di cantiere filtrati per tipologia", ex);
            }
            finally { CloseResouces(cn, null); }
        }

        //Calcolo Totali
        public static decimal TotMaterialeVisibile(string idCant)
        {
            SqlConnection cn = GetConnection();
            string sql;

            try
            {
                sql = "SELECT ISNULL(SUM(PzzoUniCantiere * Qta), 0) AS MatVis " +
                      "FROM TblMaterialiCantieri " +
                      "WHERE (Tipologia = 'MATERIALE' OR Tipologia = 'A CHIAMATA') AND Visibile = 1 AND Ricalcolo = 1 AND PzzoFinCli = 0 AND IdTblCantieri = @pIdCant ";

                return cn.Query<decimal>(sql, new { pIdCant = idCant }).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il calcolo del materiale visibile", ex);
            }
            finally { CloseResouces(cn, null); }
        }
        public static decimal TotNascosto(string idCant)
        {
            SqlConnection cn = GetConnection();
            string sql;

            try
            {
                sql = "SELECT ISNULL(SUM(PzzoUniCantiere * Qta), 0) AS MatNasc " +
                      "FROM TblMaterialiCantieri " +
                      "WHERE Visibile = 0 AND IdTblCantieri = @pIdCant ";

                return cn.Query<decimal>(sql, new { pIdCant = idCant }).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il calcolo del materiale visibile", ex);
            }
            finally { CloseResouces(cn, null); }
        }

        //Valore Ricalcolo
        public static List<decimal> CalcolaValoreRicalcolo(string idCant, decimal perc)
        {
            SqlConnection cn = GetConnection();
            string sql;

            try
            {
                sql = "SELECT ((PzzoUniCantiere * @pPerc)/100) AS 'Valore Ricalcolo' " +
                      "FROM TblMaterialiCantieri " +
                      "WHERE Visibile = 1 AND Ricalcolo = 1 AND IdTblCantieri = @pIdCant " +
                      "ORDER BY Data";

                return cn.Query<decimal>(sql, new { pIdCant = idCant, pPerc = perc }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il calcolo del materiale visibile", ex);
            }
            finally { CloseResouces(cn, null); }
        }
        //Valore Ricarico
        public static List<decimal> CalcolaValoreRicarico(string idCant)
        {
            SqlConnection cn = GetConnection();
            string sql;

            try
            {
                sql = "SELECT (((A.PzzoUniCantiere * B.Ricarico)/100)) AS 'Valore Ricarico' " +
                      "FROM TblMaterialiCantieri AS A " +
                      "LEFT JOIN TblCantieri AS B ON (A.IdTblCantieri = B.IdCantieri) " +
                      "WHERE Visibile = 1 AND ricaricoSiNo = 1 AND IdTblCantieri = @pIdCant " +
                      "ORDER BY A.Data";

                return cn.Query<decimal>(sql, new { pIdCant = idCant }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il calcolo del materiale visibile", ex);
            }
            finally { CloseResouces(cn, null); }
        }

        //INSERT
        public static bool InserisciMaterialeCantiere(MaterialiCantieri mc)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblMaterialiCantieri (IdTblCantieri,DescriMateriali,Qta,Visibile,Ricalcolo,ricaricoSiNo,Data, " +
                      "PzzoUniCantiere,CodArt,DescriCodArt,Tipologia,Fascia,Acquirente,Fornitore,NumeroBolla,ProtocolloInterno,Note,Note2,pzzoFinCli) " +
                      "VALUES (@IdTblCantieri,@DescriMateriali,@Qta,@Visibile,@Ricalcolo,@ricaricoSiNo,@Data,@PzzoUniCantiere,@CodArt,@DescriCodArt,@Tipologia,@Fascia, " +
                      "@Acquirente,@Fornitore,@NumeroBolla,@ProtocolloInterno,@Note,@Note2,@pzzoFinCli)";

                int row = cn.Execute(sql, mc);

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un materiale cantiere", ex);
            }
            finally { CloseResouces(cn, null); }
        }
        public static bool InserisciOperaio(MaterialiCantieri mc)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblMaterialiCantieri (IdTblCantieri,IdTblOperaio,DescriMateriali,Qta,Visibile,Ricalcolo,ricaricoSiNo,Data, " +
                      "PzzoUniCantiere,CodArt,DescriCodArt,Tipologia,Fascia,Acquirente,Fornitore,NumeroBolla,ProtocolloInterno,Note,Note2,pzzoFinCli) " +
                      "VALUES (@IdTblCantieri,@idOperaio,@DescriMateriali,@Qta,@Visibile,@Ricalcolo,@ricaricoSiNo,@Data,@PzzoUniCantiere,@CodArt,@DescriCodArt,@Tipologia,@Fascia, " +
                      "@Acquirente,@Fornitore,@NumeroBolla,@ProtocolloInterno,@Note,@Note2,@pzzoFinCli)";

                int row = cn.Execute(sql, mc);

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un materiale cantiere", ex);
            }
            finally { CloseResouces(cn, null); }
        }

        //UPDATE
        public static bool UpdateOperaioPagato(string dataInizio, string dataFine, string idOperaio)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "UPDATE TblMaterialiCantieri " +
                      "SET OperaioPagato = 1 " +
                      "WHERE (Data BETWEEN Convert(date, @pDataInizio) AND Convert(date, @pDataFine)) AND IdTblOperaio = @pIdOperaio ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pDataInizio", dataInizio));
                cmd.Parameters.Add(new SqlParameter("pDataFine", dataFine));
                cmd.Parameters.Add(new SqlParameter("pIdOperaio", idOperaio));

                int rows = cn.Execute(sql, new { pDataInizio = dataInizio, pDataFine = dataFine, pIdOperaio = idOperaio });

                if (rows > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'update del campo OperaioPagato", ex);
            }
            finally { CloseResouces(cn, null); }
        }
        public static bool UpdateOperaio(string id, MaterialiCantieri mc)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "UPDATE TblMaterialiCantieri " +
                      "SET IdTblCantieri = @idCant " +
                      ",IdTblOperaio = @idOper " +
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
                cmd.Parameters.Add(new SqlParameter("idOper", mc.IdOperaio));
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
            finally { CloseResouces(cn, null); }
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
                      ",PzzoFinCli = @pzzoFinCli " +
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
                cmd.Parameters.Add(new SqlParameter("pzzoFinCli", mc.PzzoFinCli));

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'update del record MatCant", ex);
            }
            finally { CloseResouces(cn, null); }
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

                int rows = cn.Execute(sql, new { id, pzzoManodop = valManodop });

                if (rows > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'update del valore della manodopera", ex);
            }
            finally { CloseResouces(cn, null); }
        }
        public static bool UpdateCostoOperaio(string idCant, string costoOperaio, string idOper)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "UPDATE TblMaterialiCantieri " +
                      "SET PzzoUniCantiere = @pzzoOper " +
                      "WHERE IdTblCantieri = @id AND IdTblOperaio = @idOper AND Tipologia = 'OPERAIO' ";

                int rows = cn.Execute(sql, new { id = idCant, pzzoOper = costoOperaio, idOper });

                if (rows > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'update del costo Operaio", ex);
            }
            finally { CloseResouces(cn, null); }
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

                int rows = cn.Execute(sql, new { id = idMatCant });

                if (rows > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione di un record per i materialiCantieri", ex);
            }
            finally { CloseResouces(cn, null); }
        }
    }
}