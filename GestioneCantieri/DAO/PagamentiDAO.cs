using Dapper;
using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace GestioneCantieri.DAO
{
    public class PagamentiDAO : BaseDAO
    {
        //SELECT
        public static Pagamenti GetSinglePagamento(int idPagam)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT IdTblCantieri, data, Imporo, DescriPagamenti, Acconto, Saldo " +
                      "FROM TblPagamenti " +
                      "WHERE IdPagamenti = @IdPagamenti ";

                return cn.Query<Pagamenti>(sql, new { IdPagamenti = idPagam }).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero del singolo pagamento", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static List<Pagamenti> GetPagamenti(string idCant)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT IdTblCantieri,data,Imporo,DescriPagamenti,Acconto,Saldo " +
                      "FROM TblPagamenti " +
                      "WHERE IdTblCantieri = @IdTblCantieri ";

                return cn.Query<Pagamenti>(sql, new { IdTblCantieri = idCant }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei pagamenti", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
        public static List<Pagamenti> GetPagamenti(string idCant, string descrizione)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            descrizione = "%" + descrizione + "%";

            try
            {
                sql = "SELECT IdPagamenti,IdTblCantieri,data,Imporo,DescriPagamenti,Acconto,Saldo " +
                      "FROM TblPagamenti " +
                      "WHERE IdTblCantieri = @IdTblCantieri AND ISNULL(DescriPagamenti,'') LIKE @DescriPagamenti ";

                return cn.Query<Pagamenti>(sql, new { IdTblCantieri = idCant, DescriPagamenti = descrizione }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei pagamenti", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }

        //INSERT
        public static bool InserisciPagamento(Pagamenti pag)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblPagamenti (IdTblCantieri, data, Imporo, DescriPagamenti, Acconto, Saldo) " +
                      "VALUES (@IdTblCantieri, @data, @Imporo, @DescriPagamenti, @Acconto, @Saldo)";

                int row = cn.Execute(sql, pag);

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un pagamento", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }

        //UPDATE
        public static bool UpdatePagamento(Pagamenti pag)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "UPDATE TblPagamenti " +
                      "SET IdTblCantieri = @IdTblCantieri, data = @data, Imporo = @Imporo, " +
                      "DescriPagamenti = @DescriPagamenti, Acconto = @Acconto, Saldo = @Saldo " +
                      "WHERE IdPagamenti = @IdPagamenti";

                int row = cn.Execute(sql, pag);

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la modifica di un pagamento", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }

        //DELETE
        public static bool DeletePagamento(int idPagam)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "DELETE FROM TblPagamenti WHERE IdPagamenti = @IdPagamenti ";

                int row = cn.Execute(sql, new { IdPagamenti = idPagam });

                if (row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione di un pagamento", ex);
            }
            finally
            {
                CloseResouces(cn, null);
            }
        }
    }
}