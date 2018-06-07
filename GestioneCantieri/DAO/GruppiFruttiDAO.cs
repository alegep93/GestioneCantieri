using Dapper;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

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

                int rowNumber = cn.Execute(sql, new { pNomeGruppo = nomeGruppo, pDescr = descr });
                if (rowNumber > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la creazione di un nuovo gruppo", ex);
            }
            finally { CloseResouces(cn, null); }
        }
        public static List<GruppiFrutti> getGruppi()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT Id,NomeGruppo,Descrizione FROM TblGruppiFrutti " +
                      "ORDER BY NomeGruppo ASC ";

                return cn.Query<GruppiFrutti>(sql).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei gruppi", ex);
            }
            finally { CloseResouces(cn, null); }
        }
        public static List<GruppiFrutti> getGruppiNonCompletati()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT Id,NomeGruppo,Descrizione FROM TblGruppiFrutti " +
                      "WHERE Completato = 0 " +
                      "ORDER BY NomeGruppo ASC ";

                return cn.Query<GruppiFrutti>(sql).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei gruppi non compleatati", ex);
            }
            finally { CloseResouces(cn, null); }
        }
        public static List<GruppiFrutti> getGruppiNonControllati()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT Id,NomeGruppo,Descrizione,Completato,Controllato FROM TblGruppiFrutti " +
                      "WHERE Controllato = 0 " +
                      "ORDER BY NomeGruppo ASC ";

                return cn.Query<GruppiFrutti>(sql).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei gruppi non controllati", ex);
            }
            finally { CloseResouces(cn, null); }
        }
        public static List<GruppiFrutti> GetGruppiWithSearch(string filter1, string filter2, string filter3)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            filter1 = "%" + filter1 + "%";
            filter2 = "%" + filter2 + "%";
            filter3 = "%" + filter3 + "%";

            try
            {
                sql = "SELECT Id,NomeGruppo,Descrizione FROM TblGruppiFrutti " +
                      "WHERE NomeGruppo LIKE @pFilter1 AND NomeGruppo LIKE @pFilter2 AND NomeGruppo LIKE @pFilter3 " +
                      "ORDER BY NomeGruppo ASC ";

                return cn.Query<GruppiFrutti>(sql, new { pFilter1 = filter1, pFilter2 = filter2, pFilter3 = filter3 }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei gruppi cercati", ex);
            }
            finally { CloseResouces(cn, null); }
        }
        public static string getDescrGruppo(int idGruppo)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT Descrizione " +
                      "FROM TblGruppiFrutti " +
                      "WHERE Id = @pIdGruppo ";

                return cn.Query<string>(sql, new { pIdGruppo = idGruppo }).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero della descrizione del gruppo", ex);
            }
            finally { CloseResouces(cn, null); }
        }
        public static string getNomeGruppo(int idGruppo)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT NomeGruppo " +
                      "FROM TblGruppiFrutti " +
                      "WHERE Id = @pIdGruppo ";

                return cn.Query<string>(sql, new { pIdGruppo = idGruppo }).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero del nome del gruppo", ex);
            }
            finally { CloseResouces(cn, null); }
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

                int rowNumber = cn.Execute(sql, new { pNomeGruppo = nome, pDescr = descr, pId = idGruppo });

                if (rowNumber > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'update di un gruppo", ex);
            }
            finally { CloseResouces(cn, null); }
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

                int rowNumber = cn.Execute(sql, new { pDescr = descr, pId = idFrutto });

                if (rowNumber > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'update di un frutto", ex);
            }
            finally { CloseResouces(cn, null); }
        }
        public static bool DeleteGruppo(int idGruppo)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "IF NOT EXISTS(SELECT Id FROM TblCompGruppoFrut WHERE IdTblGruppo = @pId) " +
                        "DELETE FROM TblGruppiFrutti WHERE Id = @pId ";

                int rows = cn.Execute(sql, new { pId = idGruppo });

                if (rows > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione di un gruppo", ex);
            }
            finally { CloseResouces(cn, null); }
        }
        public static bool DeleteFrutto(int idFrutto)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "IF NOT EXISTS(SELECT Id FROM TblCompGruppoFrut WHERE IdTblGruppo = @pId) " +
                        "DELETE FROM TblFrutti WHERE ID1 = @pId ";

                int rowNumber = cn.Execute(sql, new { pId = idFrutto });

                if (rowNumber > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione di un frutto", ex);
            }
            finally { CloseResouces(cn, null); }
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

                int rowNumber = cn.Execute(sql, new { pIdGruppo = idGruppo, completa });

                if (rowNumber > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il completamento di un gruppo", ex);
            }
            finally { CloseResouces(cn, null); }
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
            finally { CloseResouces(cn, dr); }
        }
        public static bool UpdateFlagControllato(int idGruppo)
        {
            SqlConnection cn = GetConnection();
            string sql = "";
            bool ret = false;

            try
            {
                sql = "UPDATE TblGruppiFrutti SET Controllato = 1 WHERE Id = @idGruppo";

                int rows = cn.Execute(sql, new { idGruppo });

                if (rows > 0)
                    ret = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'aggiornamento del flag controllato del gruppo " + idGruppo, ex);
            }
            finally { CloseResouces(cn, null); }

            return ret;
        }
        public static int GetNumeroGruppiNonControllati()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT COUNT(id) " +
                      "FROM TblGruppiFrutti " +
                      "WHERE Controllato = 0 ";

                return cn.Query<int>(sql).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero del totale gruppi non controllati", ex);
            }
            finally { CloseResouces(cn, null); }
        }
    }
}