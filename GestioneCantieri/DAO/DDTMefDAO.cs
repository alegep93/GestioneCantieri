using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GestioneCantieri.Data;
using System.Data.SqlClient;
using System.Data;

namespace GestioneCantieri.DAO
{
    public class DDTMefDAO : BaseDAO
    {
        /*** Lista completa dei DDT ***/
        public static List<DDTMef> getDDTList()
        {
            List<DDTMef> retList = new List<DDTMef>();
            string sql = "";
            SqlDataReader dr = null;
            SqlConnection cn = GetConnection();
            DateTime emptyData = new DateTime();
            try
            {
                sql = "SELECT TOP 500 IdDDTMef, Anno, Data, N_DDT, CodArt, " + 
                      "DescriCodArt, Qta, Importo, Acquirente, PrezzoUnitario, AnnoN_DDT " + 
                      "FROM TblDDTMef ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                dr = cmd.ExecuteReader(); //Esegue il comando e lo inserisce nel DataReader

                while (dr.Read())
                { //Restituisce FALSE quando non ci sono più record da leggere
                    DDTMef tmpDDTMef = new DDTMef();
                    tmpDDTMef.Id             = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    tmpDDTMef.Anno           = (dr.IsDBNull(1) ? -1 : dr.GetInt32(1));
                    tmpDDTMef.Data           = (dr.IsDBNull(2) ? emptyData : dr.GetDateTime(2));
                    tmpDDTMef.N_ddt          = (dr.IsDBNull(3) ? -1 : dr.GetInt32(3));
                    tmpDDTMef.CodArt         = (dr.IsDBNull(4) ? null : dr.GetString(4));
                    tmpDDTMef.DescriCodArt    = (dr.IsDBNull(5) ? null : dr.GetString(5));
                    tmpDDTMef.Qta            = (dr.IsDBNull(6) ? -1 : dr.GetInt32(6));
                    tmpDDTMef.Importo        = (dr.IsDBNull(7) ? -1m : dr.GetDecimal(7));
                    tmpDDTMef.Acquirente     = (dr.IsDBNull(8) ? null : dr.GetString(8));
                    tmpDDTMef.PrezzoUnitario = (dr.IsDBNull(9) ? -1m : dr.GetDecimal(9));
                    tmpDDTMef.AnnoN_ddt      = (dr.IsDBNull(10) ? -1 : dr.GetInt32(10));
                    retList.Add(tmpDDTMef);
                }
                return retList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dell'elenco dei DDT", ex);
            }
        }
        public static DataTable GetDDT(string anno, string n_ddt)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            anno = "%" + anno + "%";
            n_ddt = "%" + n_ddt + "%";

            try
            {
                sql = "SELECT Data, N_DDT " +
                      "FROM TblDDTMef " +
                      "WHERE Anno LIKE @pAnno AND N_DDT LIKE @pN_DDT " +
                      "GROUP BY N_DDT, Data " +
                      "ORDER BY Data ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pAnno", anno));
                cmd.Parameters.Add(new SqlParameter("pN_DDT", n_ddt));
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei DDT Mef", ex);
            }
        }
        public static DataTable GetDDTForPDF(string dataInizio, string dataFine, string acquirente, string n_ddt)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            acquirente = "%" + acquirente + "%";
            n_ddt = "%" + n_ddt + "%";

            try
            {
                /* Senza Filtro */
                sql = "SELECT IdDDTMef, Anno, Data, N_DDT, CodArt, " +
                      "DescriCodArt, Qta, Importo, Acquirente, PrezzoUnitario, AnnoN_DDT " +
                      "FROM TblDDTMef " +
                      "WHERE (Data BETWEEN Convert(date,@pDataInizio) AND Convert(date,@pDataFine)) AND Acquirente LIKE @pAcquirente AND N_DDT LIKE @pN_DDT " +
                      "ORDER BY N_DDT, CodArt";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pDataInizio", dataInizio));
                cmd.Parameters.Add(new SqlParameter("pDataFine", dataFine));
                cmd.Parameters.Add(new SqlParameter("pAcquirente", acquirente));
                cmd.Parameters.Add(new SqlParameter("pN_DDT", n_ddt));
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei DDT Mef per la stampa in PDF", ex);
            }
        }

        /*** Mostro la lista dei DDT in base ai campi compilati ***/
        public static List<DDTMef> searchFilter(string inizio, string fine, string dataInizio, string dataFine, string qta,
            string codArt1, string codArt2, string codArt3, string descriCodArt1, string descriCodArt2, string descriCodArt3)
        {
            List<DDTMef> retList = new List<DDTMef>();
            string sql = "";
            SqlDataReader dr = null;
            SqlConnection cn = GetConnection();
            DateTime emptyData = new DateTime();

            codArt1 = "%" + codArt1 + "%";
            codArt2 = "%" + codArt2 + "%";
            codArt3 = "%" + codArt3 + "%";
            descriCodArt1 = "%" + descriCodArt1 + "%";            
            descriCodArt2 = "%" + descriCodArt2 + "%";            
            descriCodArt3 = "%" + descriCodArt3 + "%";

            try
            {
                /* Senza Filtro */
                sql = "SELECT IdDDTMef, Anno, Data, N_DDT, CodArt, " +
                      "DescriCodArt, Qta, Importo, Acquirente, PrezzoUnitario, AnnoN_DDT " +
                      "FROM TblDDTMef ";

                //Controllo i casi in cui entrambi gli anni o le date siano
                //state valorizzate, oppure quanto tutti quanti sono vuoti
                //altrimenti faccio una where generica per tutti gli altri casi
                if (inizio != "" && fine != "")
                {
                    sql += "WHERE (ANNO BETWEEN @pAnnoInizio AND @pAnnoFine) " +
                           "AND CodArt LIKE @pCodArt1 AND CodArt LIKE @pCodArt2 AND CodArt LIKE @pCodArt3 " +
                           "AND DescriCodArt LIKE @pDescriCodArt1 AND DescriCodArt LIKE @pDescriCodArt2 AND DescriCodArt LIKE @pDescriCodArt3 ";
                }
                else if (dataInizio != "" && dataFine != ""){
                    sql += "WHERE (Data BETWEEN CONVERT(Date,@pDataInizio) AND CONVERT(Date,@pDataFine)) " +
                           "AND Qta LIKE @pQta "+
                           "AND CodArt LIKE @pCodArt1 AND CodArt LIKE @pCodArt2 AND CodArt LIKE @pCodArt3 " +
                           "AND DescriCodArt LIKE @pDescriCodArt1 AND DescriCodArt LIKE @pDescriCodArt2 AND DescriCodArt LIKE @pDescriCodArt3 ";
                }
                else if (inizio == "" && fine == "" && dataInizio == "" && dataFine == "")
                {
                    inizio = "%" + inizio + "%";
                    fine = "%" + fine + "%";
                    dataInizio = "2010-01-01";
                    dataFine = DateTime.Now.ToString();

                    sql += "WHERE Qta LIKE @pQta " +
                           "AND CodArt LIKE @pCodArt1 AND CodArt LIKE @pCodArt2 AND CodArt LIKE @pCodArt3 " +
                           "AND DescriCodArt LIKE @pDescriCodArt1 AND DescriCodArt LIKE @pDescriCodArt2 AND DescriCodArt LIKE @pDescriCodArt3 ";
                }
                else { 
                    sql += "WHERE ((ANNO = @pAnnoInizio OR Anno = @pAnnoFine) OR (Data = CONVERT(Date,@pDataInizio) OR Data = CONVERT(Date,@pDataFine))) " +
                           "AND Qta LIKE @pQta " +
                           "AND CodArt LIKE @pCodArt1 AND CodArt LIKE @pCodArt2 AND CodArt LIKE @pCodArt3 " +
                           "AND DescriCodArt LIKE @pDescriCodArt1 AND DescriCodArt LIKE @pDescriCodArt2 AND DescriCodArt LIKE @pDescriCodArt3 ";
                }

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pAnnoInizio", inizio));
                cmd.Parameters.Add(new SqlParameter("pAnnoFine", fine));
                cmd.Parameters.Add(new SqlParameter("pCodArt1", codArt1));
                cmd.Parameters.Add(new SqlParameter("pCodArt2", codArt2));
                cmd.Parameters.Add(new SqlParameter("pCodArt3", codArt3));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt1", descriCodArt1));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt2", descriCodArt2));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt3", descriCodArt3));

                if(qta == "")
                    cmd.Parameters.Add(new SqlParameter("pQta", "%%"));
                else
                    cmd.Parameters.Add(new SqlParameter("pQta", qta));

                if (dataInizio != "" && dataFine != "")
                {
                    cmd.Parameters.Add(new SqlParameter("pDataInizio", Convert.ToDateTime(dataInizio)));
                    cmd.Parameters.Add(new SqlParameter("pDataFine", Convert.ToDateTime(dataFine)));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("pDataInizio", dataInizio));
                    cmd.Parameters.Add(new SqlParameter("pDataFine", dataFine));
                }

                dr = cmd.ExecuteReader(); //Esegue il comando e lo inserisce nel DataReader

                while (dr.Read()) //Restituisce FALSE quando non ci sono più record da leggere
                { 
                    DDTMef tmpDDTMef = new DDTMef();
                    tmpDDTMef.Id = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    tmpDDTMef.Anno = (dr.IsDBNull(1) ? -1 : dr.GetInt32(1));
                    tmpDDTMef.Data = (dr.IsDBNull(2) ? emptyData : dr.GetDateTime(2));
                    tmpDDTMef.N_ddt = (dr.IsDBNull(3) ? -1 : dr.GetInt32(3));
                    tmpDDTMef.CodArt = (dr.IsDBNull(4) ? null : dr.GetString(4));
                    tmpDDTMef.DescriCodArt = (dr.IsDBNull(5) ? null : dr.GetString(5));
                    tmpDDTMef.Qta = (dr.IsDBNull(6) ? -1 : dr.GetInt32(6));
                    tmpDDTMef.Importo = (dr.IsDBNull(7) ? -1m : dr.GetDecimal(7));
                    tmpDDTMef.Acquirente = (dr.IsDBNull(8) ? null : dr.GetString(8));
                    tmpDDTMef.PrezzoUnitario = (dr.IsDBNull(9) ? -1m : dr.GetDecimal(9));
                    tmpDDTMef.AnnoN_ddt = (dr.IsDBNull(10) ? -1 : dr.GetInt32(10));
                    retList.Add(tmpDDTMef);
                }
                return retList;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante la ricerca dei DDT Mef", ex);
            }
        }

        /*** Media del prezzo unitario ***/
        public static decimal calcolaMediaPrezzoUnitario()
        {
            decimal media = 0m;
            string sql = "";
            SqlDataReader dr = null;
            SqlConnection cn = GetConnection();

            try
            {
                sql = "SELECT (SUM(PrezzoUnitario)) / (COUNT(PrezzoUnitario)) " +
                      "FROM TblDDTMef ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                dr = cmd.ExecuteReader(); //Esegue il comando e lo inserisce nel DataReader

                if (dr.Read())
                    media = (dr.IsDBNull(0) ? -1m : dr.GetDecimal(0));

                return media;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il calcolo della media dei prezzi unitari totali", ex);
            }
        }
        
        /*** Media dei prezzi con filtro ***/
        public static decimal calcolaMediaPrezzoUnitarioWithSearch(string inizio, string fine, string dataInizio, string dataFine,
            string codArt1, string codArt2, string codArt3, string descriCodArt1, string descriCodArt2, string descriCodArt3)
        {
            decimal media = 0m;
            string sql = "";
            SqlDataReader dr = null;
            SqlConnection cn = GetConnection();

            codArt1 = "%" + codArt1 + "%";
            codArt2 = "%" + codArt2 + "%";
            codArt3 = "%" + codArt3 + "%";
            descriCodArt1 = "%" + descriCodArt1 + "%";
            descriCodArt2 = "%" + descriCodArt2 + "%";
            descriCodArt3 = "%" + descriCodArt3 + "%";

            try
            {
                sql = "SELECT (SUM(PrezzoUnitario)) / (COUNT(PrezzoUnitario)) " +
                        "FROM TblDDTMef ";

                if (inizio != "" && fine != "")
                {
                    sql += "WHERE (ANNO BETWEEN @pAnnoInizio AND @pAnnoFine)" +
                           "AND CodArt LIKE @pCodArt1 AND CodArt LIKE @pCodArt2 AND CodArt LIKE @pCodArt3 " +
                           "AND DescriCodArt LIKE @pDescriCodArt1 AND DescriCodArt LIKE @pDescriCodArt2 AND DescriCodArt LIKE @pDescriCodArt3 ";
                }
                else if (dataInizio != "" && dataFine != "")
                {
                    sql += "WHERE (Data BETWEEN CONVERT(Date,@pDataInizio) AND CONVERT(Date,@pDataFine)) " +
                           "AND CodArt LIKE @pCodArt1 AND CodArt LIKE @pCodArt2 AND CodArt LIKE @pCodArt3 " +
                           "AND DescriCodArt LIKE @pDescriCodArt1 AND DescriCodArt LIKE @pDescriCodArt2 AND DescriCodArt LIKE @pDescriCodArt3 ";
                }
                else if (inizio == "" && fine == "" && dataInizio == "" && dataFine == "")
                {
                    inizio = "%" + inizio + "%";
                    fine = "%" + fine + "%";
                    dataInizio = "2010-01-01";
                    dataFine = DateTime.Now.ToString();

                    sql += "WHERE CodArt LIKE @pCodArt1 AND CodArt LIKE @pCodArt2 AND CodArt LIKE @pCodArt3 " +
                           "AND DescriCodArt LIKE @pDescriCodArt1 AND DescriCodArt LIKE @pDescriCodArt2 AND DescriCodArt LIKE @pDescriCodArt3 ";
                }
                else
                {
                    sql += "WHERE ((ANNO = @pAnnoInizio OR Anno = @pAnnoFine) OR (Data = CONVERT(Date,@pDataInizio) OR Data = CONVERT(Date,@pDataFine))) " +
                           "AND CodArt LIKE @pCodArt1 AND CodArt LIKE @pCodArt2 AND CodArt LIKE @pCodArt3 " +
                           "AND DescriCodArt LIKE @pDescriCodArt1 AND DescriCodArt LIKE @pDescriCodArt2 AND DescriCodArt LIKE @pDescriCodArt3 ";
                }


                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pAnnoInizio", inizio));
                cmd.Parameters.Add(new SqlParameter("pAnnoFine", fine));
                cmd.Parameters.Add(new SqlParameter("pCodArt1", codArt1));
                cmd.Parameters.Add(new SqlParameter("pCodArt2", codArt2));
                cmd.Parameters.Add(new SqlParameter("pCodArt3", codArt3));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt1", descriCodArt1));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt2", descriCodArt2));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt3", descriCodArt3));

                if (dataInizio != "" && dataFine != "")
                {
                    cmd.Parameters.Add(new SqlParameter("pDataInizio", Convert.ToDateTime(dataInizio)));
                    cmd.Parameters.Add(new SqlParameter("pDataFine", Convert.ToDateTime(dataFine)));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("pDataInizio", dataInizio));
                    cmd.Parameters.Add(new SqlParameter("pDataFine", dataFine));
                }

                dr = cmd.ExecuteReader(); //Esegue il comando e lo inserisce nel DataReader

                if (dr.Read())
                    media = (dr.IsDBNull(0) ? -1m : dr.GetDecimal(0));

                return media;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il calcolo della media con filtro per Descrizione Codice Articolo", ex);
            }
        }
    }
}