using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GestioneCantieri.DAO
{
    public class GruppiFruttiDAO : BaseDAO
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
                throw new Exception("Errore durante il recupero dei gruppi", ex);
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
        public static string getNomeGruppo(int idGruppo)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string sql = "";
            string ret = "";

            try
            {
                sql = "SELECT NomeGruppo " +
                      "FROM TblGruppiFrutti " +
                      "WHERE Id = @pIdGruppo ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdGruppo", idGruppo));
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    ret = (dr.IsDBNull(0) ? "" : dr.GetString(0));
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero del nome del gruppo", ex);
            }
            finally { cn.Close(); }

            return ret;
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
            bool ret = false;

            try
            {
                sql = "IF NOT EXISTS(SELECT Id FROM TblCompGruppoFrut WHERE IdTblGruppo = @pId) " +
                        "DELETE FROM TblGruppiFrutti WHERE Id = @pId ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pId", idGruppo));

                cmd.ExecuteNonQuery();

                ret = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione di un gruppo", ex);
            }
            finally { cn.Close(); }

            return ret;
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
        public static bool CompletaRiapriGruppo(string idGruppo, bool completa)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "UPDATE TblGruppiFrutti SET Completato = @completa WHERE Id = @pIdGruppo ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@pIdGruppo", idGruppo));
                cmd.Parameters.Add(new SqlParameter("@completa", completa));

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
        public static bool isGruppoAperto(int idGruppo)
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

        public static List<GruppiFrutti> GetGruppiWithSearch(string filter1, string filter2, string filter3)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            string sql = "";
            List<GruppiFrutti> gruppiFruttiList = new List<GruppiFrutti>();

            filter1 = "%" + filter1 + "%";
            filter2 = "%" + filter2 + "%";
            filter3 = "%" + filter3 + "%";

            try
            {
                sql = "SELECT Id,NomeGruppo,Descrizione FROM TblGruppiFrutti " +
                      "WHERE NomeGruppo LIKE @pFilter1 AND NomeGruppo LIKE @pFilter2 AND NomeGruppo LIKE @pFilter3 " +
                      "ORDER BY NomeGruppo ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pFilter1", filter1));
                cmd.Parameters.Add(new SqlParameter("pFilter2", filter2));
                cmd.Parameters.Add(new SqlParameter("pFilter3", filter3));
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
                throw new Exception("Errore durante il recupero dei gruppi cercati", ex);
            }
            finally { cn.Close(); }
        }
        
    }
}