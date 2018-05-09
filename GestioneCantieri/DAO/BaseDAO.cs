using Microsoft.Win32;
using System;
using System.Data.SqlClient;

namespace GestioneCantieri.DAO
{
    public class BaseDAO
    {
        private static string GetInstanceName()
        {
            RegistryView registryView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;
            string instanceName = "";

            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView))
            {
                RegistryKey instanceKey = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL", false);
                if (instanceKey != null)
                {
                    string[] instances = instanceKey.GetValueNames();
                    if (instances.Length == 1)
                        instanceName = instanceKey.GetValueNames()[0];
                    else
                    {
                        foreach (string instance in instances)
                        {
                            if (!instance.Contains("express") && !instance.Contains("Express") && !instance.Contains("EXPRESS"))
                            {
                                instanceName = instance;
                            }
                        }
                    }
                }
            }
            
            if (instanceName != "MSSQLSERVER")
                instanceName = "\\" + instanceName;
            else
                instanceName = "";

            return instanceName;
        }
        private static string ConfigureConnectionString()
        {
            string dataSource = Environment.MachineName + GetInstanceName();
            string dbName = "GestioneCantieri";
            string connectionString = "Data Source=" + dataSource + ";Initial Catalog=" + dbName + ";Integrated Security=True";

            return connectionString;
        }
        protected static SqlConnection GetConnection()
        {
            SqlConnection cn = null;
            try
            {
                //string connectionString = ConfigurationManager.ConnectionStrings["GestioneCantieri"].ConnectionString;
                string connectionString = ConfigureConnectionString();
                cn = new SqlConnection(connectionString);
                cn.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("Si è verificato un errore durante la creazione della connessione col DB", ex);
            }
            return cn;
        }
        protected static void CloseResouces(SqlConnection cn, SqlDataReader dr)
        {
            if (cn != null && cn.State != System.Data.ConnectionState.Closed)
            {
                cn.Close();
            }
            if (dr != null)
            {
                dr.Close();
            }
        }
    }
}