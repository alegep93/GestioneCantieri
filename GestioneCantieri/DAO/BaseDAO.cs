using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestioneCantieri.DAO
{
    public class BaseDAO
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection cn = null;
            try {
                string connectionString = ConfigurationManager.ConnectionStrings["GestioneCantieri"].ConnectionString;
                cn = new SqlConnection(connectionString);
                cn.Open();
            }
            catch (Exception ex) {
                throw new Exception("Si è verificato un errore durante la creazione della connessione col DB", ex);
            }
            return cn;
        }
    }
}