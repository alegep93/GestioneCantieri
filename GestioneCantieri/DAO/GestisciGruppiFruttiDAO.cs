using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestioneCantieri.DAO
{
    public class GestisciGruppiFruttiDAO : BaseDAO
    {
        public static bool CreaGruppo(string nomeGruppo, string descr)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "IF NOT EXISTS(SELECT NomeGruppo FROM TblGruppiFrutti WHERE NomeGruppo = @pNomeGruppo) " +
                        "INSERT INTO TblGruppiFrutti(NomeGruppo,Descrizione,Completato) VALUES (@pNomeGruppo,@pDescr,0) ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pNomeGruppo", nomeGruppo));
                cmd.Parameters.Add(new SqlParameter("pDescr", descr));

                int rowNumber = cmd.ExecuteNonQuery();
                if (rowNumber > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la creazione di un nuovo gruppo", ex);
            }
            finally { cn.Close(); }
        }
        public static bool InserisciFrutto(string nomeFrutto)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "IF NOT EXISTS(SELECT descr001 FROM TblFrutti WHERE descr001 = @pNomeFrutto) " +
                        "INSERT INTO TblFrutti(descr001) VALUES (@pNomeFrutto) ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pNomeFrutto", nomeFrutto));

                int rowNumber = cmd.ExecuteNonQuery();
                if (rowNumber > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un frutto", ex);
            }
            finally { cn.Close(); }
        }
        public static List<Frutti> getFrutti()
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string sql = "";
            List<Frutti> fruttiList = new List<Frutti>();

            try
            {
                sql = "SELECT ID1,descr001 FROM TblFrutti " +
                      "ORDER BY descr001 ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Frutti f = new Frutti();
                    f.Id = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    f.Descr = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    fruttiList.Add(f);
                }

                return fruttiList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei frutti", ex);
            }
            finally { cn.Close(); }
        }
        public static List<Frutti> getFruttiWithSearch(string f1, string f2, string f3)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string sql = "";
            List<Frutti> fruttiList = new List<Frutti>();

            f1 = "%" + f1 + "%";
            f2 = "%" + f2 + "%";
            f3 = "%" + f3 + "%";

            try
            {
                sql = "SELECT ID1,descr001 FROM TblFrutti " +
                      "WHERE descr001 LIKE @pF1 AND descr001 LIKE @pF2 AND descr001 LIKE @pF3 " +
                      "ORDER BY descr001 ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@pF1", f1));
                cmd.Parameters.Add(new SqlParameter("@pF2", f2));
                cmd.Parameters.Add(new SqlParameter("@pF3", f3));
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Frutti f = new Frutti();
                    f.Id = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    f.Descr = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    fruttiList.Add(f);
                }

                return fruttiList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la ricerca dei frutti", ex);
            }
            finally { cn.Close(); }
        }
        public static List<GruppiFrutti> getGruppi()
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string sql = "";
            List<GruppiFrutti> gruppiFruttiList = new List<GruppiFrutti>();

            try
            {
                sql = "SELECT Id,NomeGruppo,Descrizione FROM TblGruppiFrutti " +
                      "ORDER BY NomeGruppo ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    GruppiFrutti gf = new GruppiFrutti();
                    gf.Id = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    gf.NomeGruppo = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    gf.Descr = (dr.IsDBNull(2) ? null : dr.GetString(2));
                    gruppiFruttiList.Add(gf);
                }

                return gruppiFruttiList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei frutti", ex);
            }
            finally { cn.Close(); }
        }
        public static List<GruppiFrutti> getGruppiWithSearch(string f1, string f2, string f3)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string sql = "";
            List<GruppiFrutti> gruppiFruttiList = new List<GruppiFrutti>();

            f1 = "%" + f1 + "%";
            f2 = "%" + f2 + "%";
            f3 = "%" + f3 + "%";

            try
            {
                sql = "SELECT NomeGruppo FROM TblGruppiFrutti " +
                      "WHERE NomeGruppo LIKE @pF1 AND NomeGruppo LIKE @pF2 AND NomeGruppo LIKE @pF3 " +
                      "ORDER BY NomeGruppo ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@pF1", f1));
                cmd.Parameters.Add(new SqlParameter("@pF2", f2));
                cmd.Parameters.Add(new SqlParameter("@pF3", f3));
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    GruppiFrutti gf = new GruppiFrutti();
                    gf.NomeGruppo = (dr.IsDBNull(0) ? null : dr.GetString(0));
                    gruppiFruttiList.Add(gf);
                }

                return gruppiFruttiList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei gruppi inseriti", ex);
            }
            finally { cn.Close(); }
        }
        public static List<GruppiFrutti> getGruppiNonCompletati()
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string sql = "";
            List<GruppiFrutti> gruppiFruttiList = new List<GruppiFrutti>();

            try
            {
                sql = "SELECT Id,NomeGruppo,Descrizione FROM TblGruppiFrutti " +
                      "WHERE Completato = 0 " +
                      "ORDER BY NomeGruppo ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    GruppiFrutti gf = new GruppiFrutti();
                    gf.Id = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    gf.NomeGruppo = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    gf.Descr = (dr.IsDBNull(2) ? null : dr.GetString(2));
                    gruppiFruttiList.Add(gf);
                }

                return gruppiFruttiList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei gruppi non compleatati", ex);
            }
            finally { cn.Close(); }
        }
        public static List<GruppiFrutti> getGruppiNonControllati()
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string sql = "";
            List<GruppiFrutti> gruppiFruttiList = new List<GruppiFrutti>();

            try
            {
                sql = "SELECT Id,NomeGruppo,Descrizione,Completato,Controllato FROM TblGruppiFrutti " +
                      "WHERE Controllato = 0 " +
                      "ORDER BY NomeGruppo ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    GruppiFrutti gf = new GruppiFrutti();
                    gf.Id = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    gf.NomeGruppo = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    gf.Descr = (dr.IsDBNull(2) ? null : dr.GetString(2));
                    gf.Completato = (dr.IsDBNull(3) ? false : dr.GetBoolean(3));
                    gf.Controllato = (dr.IsDBNull(4) ? false : dr.GetBoolean(4));
                    gruppiFruttiList.Add(gf);
                }

                return gruppiFruttiList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei gruppi non controllati", ex);
            }
            finally { cn.Close(); }
        }
        public static bool InserisciCompGruppo(int gruppo, int frutto, string qta)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblCompGruppoFrut(IdTblGruppo,IdTblFrutto,Qta) VALUES (@pGruppo,@pFrutto,@pQta) ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pGruppo", gruppo));
                cmd.Parameters.Add(new SqlParameter("pFrutto", frutto));
                cmd.Parameters.Add(new SqlParameter("pQta", qta));

                int rowNumber = cmd.ExecuteNonQuery();

                if (rowNumber > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un frutto in un gruppo", ex);
            }
            finally { cn.Close(); }
        }
        public static List<CompGruppoFrut> getCompGruppo(int idGruppo)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string sql = "";
            List<CompGruppoFrut> compList = new List<CompGruppoFrut>();

            try
            {
                sql = "SELECT CGF.Id,F.descr001,Qta " +
                      "FROM TblCompGruppoFrut AS CGF " +
                      "JOIN TblFrutti AS F ON (CGF.IdTblFrutto = F.ID1) " +
                      "WHERE IdTblGruppo = @pIdGruppo " +
                      "ORDER BY CGF.Id ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdGruppo", idGruppo));
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    CompGruppoFrut cgf = new CompGruppoFrut();
                    cgf.Id = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    cgf.NomeFrutto = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    cgf.Qta = (dr.IsDBNull(2) ? -1 : dr.GetInt32(2));
                    compList.Add(cgf);
                }

                return compList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei componeti del gruppo", ex);
            }
            finally { cn.Close(); }
        }
        public static string getDescrGruppo(int idGruppo)
        {
            GruppiFrutti gf = new GruppiFrutti();
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string sql = "";

            try
            {
                sql = "SELECT Descrizione " +
                      "FROM TblGruppiFrutti " +
                      "WHERE Id = @pIdGruppo ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdGruppo", idGruppo));
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return gf.Descr = (dr.IsDBNull(0) ? null : dr.GetString(0));
                }
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero della descrizione del gruppo", ex);
            }
            finally { cn.Close(); }
        }
        public static bool UpdateGruppo(int idGruppo, string nome, string descr)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "UPDATE TblGruppiFrutti " +
                      "SET NomeGruppo = @pNomeGruppo, Descrizione = @pDescr " +
                      "WHERE Id = @pId; ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pNomeGruppo", nome));
                cmd.Parameters.Add(new SqlParameter("pDescr", descr));
                cmd.Parameters.Add(new SqlParameter("pId", idGruppo));

                int rowNumber = cmd.ExecuteNonQuery();

                if (rowNumber > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'update di un gruppo", ex);
            }
            finally { cn.Close(); }
        }
        public static bool UpdateFrutto(int idFrutto, string descr)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "UPDATE TblFrutti " +
                      "SET descr001 = @pDescr " +
                      "WHERE ID1 = @pId; ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pDescr", descr));
                cmd.Parameters.Add(new SqlParameter("pId", idFrutto));

                int rowNumber = cmd.ExecuteNonQuery();

                if (rowNumber > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'update di un frutto", ex);
            }
            finally { cn.Close(); }
        }
        public static bool DeleteGruppo(int idGruppo)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "IF NOT EXISTS(SELECT Id FROM TblCompGruppoFrut WHERE IdTblGruppo = @pId) " +
                        "DELETE FROM TblGruppiFrutti WHERE Id = @pId ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pId", idGruppo));

                int rowNumber = cmd.ExecuteNonQuery();

                if (rowNumber > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione di un gruppo", ex);
            }
            finally { cn.Close(); }
        }
        public static bool DeleteFrutto(int idFrutto)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "IF NOT EXISTS(SELECT Id FROM TblCompGruppoFrut WHERE IdTblGruppo = @pId) " +
                        "DELETE FROM TblFrutti WHERE ID1 = @pId ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pId", idFrutto));

                int rowNumber = cmd.ExecuteNonQuery();

                if (rowNumber > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione di un frutto", ex);
            }
            finally { cn.Close(); }
        }
        public static bool DeleteCompGruppo(int idCompGruppo)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "DELETE FROM TblCompGruppoFrut WHERE Id = @pId ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pId", idCompGruppo));

                int rowNumber = cmd.ExecuteNonQuery();

                if (rowNumber > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione di un componente di un gruppo frutto", ex);
            }
            finally { cn.Close(); }
        }
        public static bool CompletaRiapriGruppo(string idGruppo, bool isOpen)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                if (isOpen)
                    sql = "UPDATE TblGruppiFrutti SET Completato = 1 WHERE Id = @pIdGruppo ";
                else
                    sql = "UPDATE TblGruppiFrutti SET Completato = 0 WHERE Id = @pIdGruppo ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdGruppo", idGruppo));

                int rowNumber = cmd.ExecuteNonQuery();

                if (rowNumber > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il completamento di un gruppo", ex);
            }
            finally { cn.Close(); }
        }
        public static bool isGruppoAperto(string idGruppo)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string sql = "";
            bool ret = false;

            try
            {
                sql = "SELECT Completato FROM TblGruppiFrutti WHERE Id = @pId ";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pId", idGruppo));
                dr = cmd.ExecuteReader();

                if (dr.Read())
                    ret = dr.GetBoolean(0);

                return !ret;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il controllo su gruppo aperto/chiuso", ex);
            }
            finally { cn.Close(); dr.Close(); }
        }
        public static bool UpdateFlagControllato(int idGruppo)
        {
            SqlConnection cn = GetConnection();
            string sql = "";
            bool ret = false;

            try
            {
                sql = "UPDATE TblGruppiFrutti SET Controllato = 1 WHERE Id = @idGruppo";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@idGruppo", idGruppo));
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    ret = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'aggiornamento del flag controllato del gruppo " + idGruppo, ex);
            }
            finally { cn.Close(); }

            return ret;
        }
        public static int GetNumeroGruppiNonControllati()
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string sql = "";
            int numGruppi = 0;

            try
            {
                sql = "SELECT COUNT(id) " +
                      "FROM TblGruppiFrutti " +
                      "WHERE Controllato = 0 ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    numGruppi = dr.GetInt32(0); 
                }

                return numGruppi;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero del totale gruppi non controllati", ex);
            }
            finally { cn.Close(); dr.Close(); }
        }
    }
}