using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GestioneCantieri.Data;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;

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
                      "FROM TblDDTMef " +
                      "ORDER BY Anno, Data, N_DDT, CodArt";

                SqlCommand cmd = new SqlCommand(sql, cn);
                dr = cmd.ExecuteReader(); //Esegue il comando e lo inserisce nel DataReader

                while (dr.Read())
                { //Restituisce FALSE quando non ci sono più record da leggere
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
                      "ORDER BY Data, N_DDT ";

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
        public static List<DDTMef> searchFilter(DDTMefObject ddt)
        {
            List<DDTMef> retList = new List<DDTMef>();
            string sql = "";
            SqlDataReader dr = null;
            SqlConnection cn = GetConnection();
            DateTime emptyData = new DateTime();

            ddt.CodArt1 = "%" + ddt.CodArt1 + "%";
            ddt.CodArt2 = "%" + ddt.CodArt2 + "%";
            ddt.CodArt3 = "%" + ddt.CodArt3 + "%";
            ddt.DescriCodArt1 = "%" + ddt.DescriCodArt1 + "%";
            ddt.DescriCodArt2 = "%" + ddt.DescriCodArt2 + "%";
            ddt.DescriCodArt3 = "%" + ddt.DescriCodArt3 + "%";

            try
            {
                /* Senza Filtro */
                sql = "SELECT IdDDTMef, Anno, Data, N_DDT, CodArt, " +
                      "DescriCodArt, Qta, Importo, Acquirente, PrezzoUnitario, AnnoN_DDT " +
                      "FROM TblDDTMef ";

                //Controllo i casi in cui entrambi gli anni o le date siano
                //state valorizzate, oppure quanto tutti quanti sono vuoti
                //altrimenti faccio una where generica per tutti gli altri casi
                if (ddt.AnnoInizio != "" && ddt.AnnoFine != "")
                {
                    sql += "WHERE (ANNO BETWEEN @pAnnoInizio AND @pAnnoFine) " +
                           "AND Qta LIKE @pQta AND N_DDT LIKE @pN_DDT " +
                           "AND CodArt LIKE @pCodArt1 AND CodArt LIKE @pCodArt2 AND CodArt LIKE @pCodArt3 " +
                           "AND DescriCodArt LIKE @pDescriCodArt1 AND DescriCodArt LIKE @pDescriCodArt2 AND DescriCodArt LIKE @pDescriCodArt3 ";
                }
                else if (ddt.DataInizio != "" && ddt.DataFine != "")
                {
                    sql += "WHERE (Data BETWEEN CONVERT(Date,@pDataInizio) AND CONVERT(Date,@pDataFine)) " +
                           "AND Qta LIKE @pQta AND N_DDT LIKE @pN_DDT " +
                           "AND CodArt LIKE @pCodArt1 AND CodArt LIKE @pCodArt2 AND CodArt LIKE @pCodArt3 " +
                           "AND DescriCodArt LIKE @pDescriCodArt1 AND DescriCodArt LIKE @pDescriCodArt2 AND DescriCodArt LIKE @pDescriCodArt3 ";
                }
                else if (ddt.AnnoInizio == "" && ddt.AnnoFine == "" && ddt.DataInizio == "" && ddt.DataFine == "")
                {
                    ddt.AnnoInizio = "%" + ddt.AnnoInizio + "%";
                    ddt.AnnoFine = "%" + ddt.AnnoFine + "%";
                    ddt.DataInizio = "2010-01-01";
                    ddt.DataFine = DateTime.Now.ToString();

                    sql += "WHERE Qta LIKE @pQta AND N_DDT LIKE @pN_DDT " +
                           "AND CodArt LIKE @pCodArt1 AND CodArt LIKE @pCodArt2 AND CodArt LIKE @pCodArt3 " +
                           "AND DescriCodArt LIKE @pDescriCodArt1 AND DescriCodArt LIKE @pDescriCodArt2 AND DescriCodArt LIKE @pDescriCodArt3 ";
                }
                else
                {
                    sql += "WHERE ((ANNO = @pAnnoInizio OR Anno = @pAnnoFine) OR (Data = CONVERT(Date,@pDataInizio) OR Data = CONVERT(Date,@pDataFine))) " +
                           "AND Qta LIKE @pQta AND N_DDT LIKE @pN_DDT " +
                           "AND CodArt LIKE @pCodArt1 AND CodArt LIKE @pCodArt2 AND CodArt LIKE @pCodArt3 " +
                           "AND DescriCodArt LIKE @pDescriCodArt1 AND DescriCodArt LIKE @pDescriCodArt2 AND DescriCodArt LIKE @pDescriCodArt3 ";
                }

                sql += "ORDER BY Anno, Data, N_DDT, CodArt ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pAnnoInizio", ddt.AnnoInizio));
                cmd.Parameters.Add(new SqlParameter("pAnnoFine", ddt.AnnoFine));
                cmd.Parameters.Add(new SqlParameter("pCodArt1", ddt.CodArt1));
                cmd.Parameters.Add(new SqlParameter("pCodArt2", ddt.CodArt2));
                cmd.Parameters.Add(new SqlParameter("pCodArt3", ddt.CodArt3));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt1", ddt.DescriCodArt1));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt2", ddt.DescriCodArt2));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt3", ddt.DescriCodArt3));

                if (ddt.Qta == "")
                    cmd.Parameters.Add(new SqlParameter("pQta", "%%"));
                else
                    cmd.Parameters.Add(new SqlParameter("pQta", ddt.Qta));

                if (ddt.NDdt == "")
                    cmd.Parameters.Add(new SqlParameter("pN_DDT", "%%"));
                else
                    cmd.Parameters.Add(new SqlParameter("pN_DDT", ddt.NDdt));

                if (ddt.DataInizio != "" && ddt.DataFine != "")
                {
                    cmd.Parameters.Add(new SqlParameter("pDataInizio", Convert.ToDateTime(ddt.DataInizio)));
                    cmd.Parameters.Add(new SqlParameter("pDataFine", Convert.ToDateTime(ddt.DataFine)));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("pDataInizio", ddt.DataInizio));
                    cmd.Parameters.Add(new SqlParameter("pDataFine", ddt.DataFine));
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
        public static decimal calcolaMediaPrezzoUnitarioWithSearch(DDTMefObject ddt)
        {
            decimal media = 0m;
            string sql = "";
            SqlDataReader dr = null;
            SqlConnection cn = GetConnection();

            ddt.CodArt1 = "%" + ddt.CodArt1 + "%";
            ddt.CodArt2 = "%" + ddt.CodArt2 + "%";
            ddt.CodArt3 = "%" + ddt.CodArt3 + "%";
            ddt.DescriCodArt1 = "%" + ddt.DescriCodArt1 + "%";
            ddt.DescriCodArt2 = "%" + ddt.DescriCodArt2 + "%";
            ddt.DescriCodArt3 = "%" + ddt.DescriCodArt3 + "%";

            try
            {
                sql = "SELECT (SUM(PrezzoUnitario)) / (COUNT(PrezzoUnitario)) " +
                        "FROM TblDDTMef ";

                if (ddt.AnnoInizio != "" && ddt.AnnoFine != "")
                {
                    sql += "WHERE (ANNO BETWEEN @pAnnoInizio AND @pAnnoFine)" +
                           "AND Qta LIKE @pQta AND N_DDT LIKE @pN_DDT " +
                           "AND CodArt LIKE @pCodArt1 AND CodArt LIKE @pCodArt2 AND CodArt LIKE @pCodArt3 " +
                           "AND DescriCodArt LIKE @pDescriCodArt1 AND DescriCodArt LIKE @pDescriCodArt2 AND DescriCodArt LIKE @pDescriCodArt3 ";
                }
                else if (ddt.DataInizio != "" && ddt.DataFine != "")
                {
                    sql += "WHERE (Data BETWEEN CONVERT(Date,@pDataInizio) AND CONVERT(Date,@pDataFine)) " +
                           "AND Qta LIKE @pQta AND N_DDT LIKE @pN_DDT " +
                           "AND CodArt LIKE @pCodArt1 AND CodArt LIKE @pCodArt2 AND CodArt LIKE @pCodArt3 " +
                           "AND DescriCodArt LIKE @pDescriCodArt1 AND DescriCodArt LIKE @pDescriCodArt2 AND DescriCodArt LIKE @pDescriCodArt3 ";
                }
                else if (ddt.AnnoInizio == "" && ddt.AnnoFine == "" && ddt.DataInizio == "" && ddt.DataFine == "")
                {
                    ddt.AnnoInizio = "%" + ddt.AnnoInizio + "%";
                    ddt.AnnoFine = "%" + ddt.AnnoFine + "%";
                    ddt.DataInizio = "2010-01-01";
                    ddt.DataFine = DateTime.Now.ToString();

                    sql += "WHERE CodArt LIKE @pCodArt1 AND CodArt LIKE @pCodArt2 AND CodArt LIKE @pCodArt3 " +
                           "AND Qta LIKE @pQta AND N_DDT LIKE @pN_DDT " +
                           "AND DescriCodArt LIKE @pDescriCodArt1 AND DescriCodArt LIKE @pDescriCodArt2 AND DescriCodArt LIKE @pDescriCodArt3 ";
                }
                else
                {
                    sql += "WHERE ((ANNO = @pAnnoInizio OR Anno = @pAnnoFine) OR (Data = CONVERT(Date,@pDataInizio) OR Data = CONVERT(Date,@pDataFine))) " +
                           "AND Qta LIKE @pQta AND N_DDT LIKE @pN_DDT " +
                           "AND CodArt LIKE @pCodArt1 AND CodArt LIKE @pCodArt2 AND CodArt LIKE @pCodArt3 " +
                           "AND DescriCodArt LIKE @pDescriCodArt1 AND DescriCodArt LIKE @pDescriCodArt2 AND DescriCodArt LIKE @pDescriCodArt3 ";
                }


                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pAnnoInizio", ddt.AnnoInizio));
                cmd.Parameters.Add(new SqlParameter("pAnnoFine", ddt.AnnoFine));
                cmd.Parameters.Add(new SqlParameter("pCodArt1", ddt.CodArt1));
                cmd.Parameters.Add(new SqlParameter("pCodArt2", ddt.CodArt2));
                cmd.Parameters.Add(new SqlParameter("pCodArt3", ddt.CodArt3));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt1", ddt.DescriCodArt1));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt2", ddt.DescriCodArt2));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt3", ddt.DescriCodArt3));

                if (ddt.Qta == "")
                    cmd.Parameters.Add(new SqlParameter("pQta", "%%"));
                else
                    cmd.Parameters.Add(new SqlParameter("pQta", ddt.Qta));

                if (ddt.NDdt == "")
                    cmd.Parameters.Add(new SqlParameter("pN_DDT", "%%"));
                else
                    cmd.Parameters.Add(new SqlParameter("pN_DDT", ddt.NDdt));

                if (ddt.DataInizio != "" && ddt.DataFine != "")
                {
                    cmd.Parameters.Add(new SqlParameter("pDataInizio", Convert.ToDateTime(ddt.DataInizio)));
                    cmd.Parameters.Add(new SqlParameter("pDataFine", Convert.ToDateTime(ddt.DataFine)));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("pDataInizio", ddt.DataInizio));
                    cmd.Parameters.Add(new SqlParameter("pDataFine", ddt.DataFine));
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

        public static List<DDTMef> GetDdtFromDBF(string pathFile, string acquirente, int idFornitore)
        {
            string excelConnectionString = "Provider = vfpoledb; Data Source = " + pathFile + "; Collating Sequence = machine";
            string commandText = "SELECT FTANNO, FTDT, FTNR, FTAFO, FTDEX1, FTQTA, FTPU FROM " + pathFile + "\\D_DDT.DBF";
            OleDbConnection ExcelConection = null;
            List<DDTMef> list = new List<DDTMef>();

            try
            {
                OleDbConnectionStringBuilder OleStringBuilder = new OleDbConnectionStringBuilder(excelConnectionString);
                OleStringBuilder.DataSource = pathFile;
                ExcelConection = new OleDbConnection();
                ExcelConection.ConnectionString = OleStringBuilder.ConnectionString;

                using (OleDbDataAdapter adaptor = new OleDbDataAdapter(commandText, ExcelConection))
                {
                    DataSet ds = new DataSet();
                    adaptor.Fill(ds);
                    ExcelConection.Open();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        if (Convert.ToInt32(row.ItemArray[5]) != 0)
                        {
                            DateTime date = Convert.ToDateTime(row.ItemArray[1].ToString().Substring(0, 4) + "-" + row.ItemArray[1].ToString().Substring(4, 2) + "-" + row.ItemArray[1].ToString().Substring(6, 2));

                            decimal prezzoUnitario = Convert.ToDecimal(row.ItemArray[6]) / Convert.ToInt32(row.ItemArray[5].ToString() == "0" ? 1 : row.ItemArray[5]);
                            int annoN_ddt = Convert.ToInt32(row.ItemArray[0].ToString() + row.ItemArray[2].ToString());

                            DDTMef ddt = new DDTMef();
                            ddt.Anno = Convert.ToInt32(row.ItemArray[0]);
                            ddt.Data = date;
                            ddt.N_ddt = Convert.ToInt32(row.ItemArray[2]);
                            ddt.CodArt = row.ItemArray[3].ToString().Trim();
                            ddt.DescriCodArt = row.ItemArray[4].ToString().Trim();
                            ddt.Qta = Convert.ToInt32(row.ItemArray[5]);
                            ddt.Importo = Convert.ToDecimal(row.ItemArray[6]);
                            ddt.Acquirente = acquirente;
                            ddt.PrezzoUnitario = prezzoUnitario;
                            ddt.AnnoN_ddt = annoN_ddt;
                            ddt.IdFornitore = idFornitore;

                            list.Add(ddt);
                        }
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'importazione del DBF per il DDT MEF", ex);
            }
            finally
            {
            }
        }
        public static bool CheckIfRowExist(int anno, int nDdt, string codArt)
        {
            string sql = "";
            SqlDataReader dr = null;
            SqlConnection cn = GetConnection();

            try
            {
                sql = "SELECT AnnoN_DDT " +
                      "FROM TblDDTMef " +
                      "WHERE Anno = @anno AND N_DDT = @nDdt AND CodArt = @codArt ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@anno", anno));
                cmd.Parameters.Add(new SqlParameter("@nDdt", nDdt));
                cmd.Parameters.Add(new SqlParameter("@codArt", codArt));
                dr = cmd.ExecuteReader();

                return dr.Read();
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il controllo della presenza di un record del DDTMef", ex);
            }
            finally
            {
                cn.Close();
            }
        }
        public static bool InsertNewDdt(DDTMef ddt)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblDDTMef (Anno,Data,N_DDT,CodArt,DescriCodArt,Qta,Importo,Acquirente,PrezzoUnitario,AnnoN_DDT) " +
                      "VALUES (@anno,@data,@nDdt,@codArt,@descriCodArt,@qta,@importo,@acquirente,@prezzoUni,@annoNddt)";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@anno", ddt.Anno));
                cmd.Parameters.Add(new SqlParameter("@data", ddt.Data));
                cmd.Parameters.Add(new SqlParameter("@nDdt", ddt.N_ddt));
                cmd.Parameters.Add(new SqlParameter("@codArt", ddt.CodArt));
                cmd.Parameters.Add(new SqlParameter("@descriCodArt", ddt.DescriCodArt));
                cmd.Parameters.Add(new SqlParameter("@qta", ddt.Qta));
                cmd.Parameters.Add(new SqlParameter("@importo", ddt.Importo));
                cmd.Parameters.Add(new SqlParameter("@acquirente", ddt.Acquirente));
                cmd.Parameters.Add(new SqlParameter("@prezzoUni", ddt.PrezzoUnitario));
                cmd.Parameters.Add(new SqlParameter("@annoNddt", ddt.AnnoN_ddt));
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un nuovo record per il DDTMef ", ex);
            }
            finally
            {
                cn.Close();
            }
        }
        public static bool UpdateDdt(DDTMef ddt)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "UPDATE TblDDTMef SET Importo = @importo " +
                      "WHERE Anno = @anno AND DATEPART(MONTH, data) = @mese AND N_DDT = @nDdt AND CodArt = @codArt ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@anno", ddt.Anno));
                cmd.Parameters.Add(new SqlParameter("@mese", DateTime.Now.Month));
                cmd.Parameters.Add(new SqlParameter("@nDdt", ddt.N_ddt));
                cmd.Parameters.Add(new SqlParameter("@codArt", ddt.CodArt));
                cmd.Parameters.Add(new SqlParameter("@importo", ddt.Importo));
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'aggiornamento di un record del DDTMef", ex);
            }
            finally
            {
                cn.Close();
            }
        }

        public static List<DDTMef> GetNewDDT()
        {
            string sql = "";
            SqlDataReader dr = null;
            SqlConnection cn = GetConnection();
            List<DDTMef> list = new List<DDTMef>();

            try
            {
                sql = "SELECT A.IdDDTMef, A.Anno, A.Data, A.N_DDT, A.CodArt, A.DescriCodArt, A.Qta, A.Importo, A.Acquirente, A.PrezzoUnitario, A.AnnoN_DDT " +
                      "FROM TblDDTMefTemp AS A " +
                      "LEFT JOIN TblDDTMef AS B ON A.AnnoN_DDT = B.AnnoN_DDT " +
                      "WHERE B.IdDDTMef IS NULL ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    DDTMef ddt = new DDTMef();
                    ddt.Id = (dr.IsDBNull(0) ? -1 : dr.GetInt32(0));
                    ddt.Anno = (dr.IsDBNull(1) ? -1 : dr.GetInt32(1));
                    ddt.Data = (dr.IsDBNull(2) ? DateTime.Now : dr.GetDateTime(2));
                    ddt.N_ddt = (dr.IsDBNull(3) ? -1 : dr.GetInt32(3));
                    ddt.CodArt = (dr.IsDBNull(4) ? null : dr.GetString(4));
                    ddt.DescriCodArt = (dr.IsDBNull(5) ? null : dr.GetString(5));
                    ddt.Qta = (dr.IsDBNull(6) ? -1 : dr.GetInt32(6));
                    ddt.Importo = (dr.IsDBNull(7) ? -1m : dr.GetDecimal(7));
                    ddt.Acquirente = (dr.IsDBNull(8) ? null : dr.GetString(8));
                    ddt.PrezzoUnitario = (dr.IsDBNull(9) ? -1m : dr.GetDecimal(9));
                    ddt.AnnoN_ddt = (dr.IsDBNull(10) ? -1 : dr.GetInt32(10));
                    list.Add(ddt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero dei nuovi DDT da aggiungere all'anagrafica", ex);
            }
            finally
            {
                cn.Close();
            }

            return list;
        }

        // Metodi per la tabella DDT Temporanea
        public static bool InsertIntoDdtTemp(DDTMef ddt)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblDDTMefTemp (Anno,Data,N_DDT,CodArt,DescriCodArt,Qta,Importo,Acquirente,PrezzoUnitario,AnnoN_DDT) " +
                      "VALUES (@anno,@data,@nDdt,@codArt,@descriCodArt,@qta,@importo,@acquirente,@prezzoUni,@annoNddt) ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("@anno", ddt.Anno));
                cmd.Parameters.Add(new SqlParameter("@data", ddt.Data));
                cmd.Parameters.Add(new SqlParameter("@nDdt", ddt.N_ddt));
                cmd.Parameters.Add(new SqlParameter("@codArt", ddt.CodArt));
                cmd.Parameters.Add(new SqlParameter("@descriCodArt", ddt.DescriCodArt));
                cmd.Parameters.Add(new SqlParameter("@qta", ddt.Qta));
                cmd.Parameters.Add(new SqlParameter("@importo", ddt.Importo));
                cmd.Parameters.Add(new SqlParameter("@acquirente", ddt.Acquirente));
                cmd.Parameters.Add(new SqlParameter("@prezzoUni", ddt.PrezzoUnitario));
                cmd.Parameters.Add(new SqlParameter("@annoNddt", ddt.AnnoN_ddt));
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un nuovo record nella tabella TblDDTMefTemp ", ex);
            }
            finally
            {
                cn.Close();
            }
        }
        public static bool DeleteFromDdtTemp()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "DELETE FROM TblDDTMefTemp ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'eliminazione della tabella TblDDTMefTemp ", ex);
            }
            finally
            {
                cn.Close();
            }
        }
    }
}