using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
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
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt", mc.DescriMateriali));
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
    }
}