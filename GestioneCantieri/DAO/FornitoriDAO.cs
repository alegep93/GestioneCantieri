using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestioneCantieri.DAO
{
    public class FornitoriDAO : BaseDAO
    {
        public static DataTable GetFornitoriDataTable()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT IdFornitori,RagSocForni,Indirizzo,cap, " +
                      "Città,Tel1,Cell1,PartitaIva,CodFiscale,Abbreviato " +
                      "FROM TblForitori " +
                      "ORDER BY RagSocForni ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei fornitori", ex);
            }
            finally { cn.Close(); }
        }

        public static List<Fornitori> GetFornitori()
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            List<Fornitori> list = new List<Fornitori>();
            string sql = "";

            try
            {
                sql = "SELECT IdFornitori,RagSocForni,Indirizzo,cap, " +
                      "Città,Tel1,Cell1,PartitaIva,CodFiscale,Abbreviato " +
                      "FROM TblForitori " +
                      "ORDER BY RagSocForni ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Fornitori f = new Fornitori();
                    f.IdFornitori = dr.GetInt32(0);
                    f.RagSocForni = dr.GetString(1);
                    f.Indirizzo = dr.GetString(2);
                    f.Cap = dr.GetString(3);
                    f.Città = dr.GetString(4);
                    f.Tel1 = dr.GetInt32(5);
                    f.Cell1 = dr.GetInt32(6);
                    f.PartitaIva = dr.GetDouble(7);
                    f.CodFiscale = dr.GetString(8);
                    f.Abbreviato = dr.GetString(8);
                    list.Add(f);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei fornitori", ex);
            }
            finally { cn.Close(); }
        }

        public static int GetIdFornitore(string ragSoc)
        {
            SqlConnection cn = GetConnection();
            SqlDataReader dr = null;
            int id = -1;
            string sql = "";

            try
            {
                sql = "SELECT IdFornitori FROM TblForitori WHERE RagSocForni = @ragSoc";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@ragSoc", ragSoc));
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    id = dr.GetInt32(0);
                }

                return id;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dell'idFornitore", ex);
            }
            finally { cn.Close(); }
        }
    }
}