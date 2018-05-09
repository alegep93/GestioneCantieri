using Dapper;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace GestioneCantieri.DAO
{
    public class LocaliDAO : BaseDAO
    {
        public static List<Locali> GetListLocali()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT IdLocali 'Id', NomeLocale FROM TblLocali ORDER BY NomeLocale ASC ";
                return cn.Query<Locali>(sql).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei locali", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static string GetNomeLocale(int idLocale)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT NomeLocale FROM TblLocali WHERE IdLocali = @IdLocali ";
                return cn.Query<string>(sql, new { IdLocali = idLocale }).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero del nome di un locale", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static bool InserisciLocale(string nomeLocale)
        {
            SqlConnection cn = GetConnection();
            string sql = "";
            bool ret = false;

            try
            {
                sql = "INSERT INTO TblLocali(NomeLocale) VALUES (@NomeLocale)";
                cn.Execute(sql, new { NomeLocale = nomeLocale });
                ret = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un nuovo locale", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }

            return ret;
        }
        public static bool ModificaLocale(int idLocale, string nomeLocale)
        {
            SqlConnection cn = GetConnection();
            string sql = "";
            bool ret = false;

            try
            {
                sql = "UPDATE TblLocali SET NomeLocale = @NomeLocale WHERE IdLocali = @IdLocali";
                cn.Execute(sql, new { IdLocali = idLocale, NomeLocale = nomeLocale });
                ret = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'aggiornamento del nome di un locale", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }

            return ret;
        }
        public static bool EliminaLocale(int idLocale)
        {
            SqlConnection cn = GetConnection();
            string sql = "";
            bool ret = false;

            try
            {
                sql = "DELETE FROM TblLocali WHERE IdLocali = @IdLocali";
                cn.Execute(sql, new { IdLocali = idLocale });
                ret = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione di un locale", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }

            return ret;
        }
    }
}