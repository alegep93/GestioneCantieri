using GestioneCantieri.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestioneCantieri.DAO
{
    public class GestioneCantieriDAO : BaseDAO
    {
        //SELECT
        public static DataTable GetCantieri(string anno, string codCant, string descr, bool chiuso, bool riscosso)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            anno = "%" + anno + "%";
            codCant = "%" + codCant + "%";
            descr = "%" + descr + "%";

            try
            {
                sql = "SELECT Cant.IdCantieri, Cli.RagSocCli, Cant.CodCant, Cant.DescriCodCAnt, " +
                      "Cant.Data, Cant.Indirizzo, Cant.Città, Cant.Ricarico, " +
                      "Cant.PzzoManodopera, Cant.Chiuso, Cant.Riscosso, Cant.Numero, " +
                      "Cant.ValorePreventivo, Cant.IVA, Cant.Anno, Cant.Preventivo, " +
                      "Cant.FasciaTblCantieri, Cant.DaDividere, Cant.Diviso, Cant.Fatturato " +
                      "FROM TblCantieri AS Cant " +
                      "JOIN TblClienti AS Cli ON(Cant.IdTblClienti = Cli.IdCliente) " +
                      "WHERE Anno LIKE @pAnno AND CodCant LIKE @pCodCant AND DescriCodCAnt LIKE @pDescr " +
                      "AND Chiuso LIKE @pChiuso AND Riscosso LIKE @pRiscosso " +
                      "ORDER BY Cant.CodCant ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pAnno", anno));
                cmd.Parameters.Add(new SqlParameter("pCodCant", codCant));
                cmd.Parameters.Add(new SqlParameter("pDescr", descr));
                cmd.Parameters.Add(new SqlParameter("pChiuso", chiuso));
                cmd.Parameters.Add(new SqlParameter("pRiscosso", riscosso));

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'applicazione dei filtri sui cantieri", ex);
            }
            finally { cn.Close(); }
        }
        public static DataTable GetOperai()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT IdOperaio,NomeOp,DescrOP,Suffisso,Operaio " +
                      "FROM TblOperaio " +
                      "ORDER BY NomeOp ASC ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero degli operai", ex);
            }
            finally { cn.Close(); }
        }
        public static DataTable GetFornitori()
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
        public static DataTable GetTipDatCant()
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "SELECT idTipologia, descrizione, abbreviato FROM TipDatCant ";

                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                adapter.Fill(table);

                return table;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero delle tipologie", ex);
            }
            finally { cn.Close(); }
        }
        public static DataTable GetDDT(string anno, string n_ddt)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            anno = "%" + anno + "%";
            n_ddt = "%" + n_ddt + "%";

            try
            {
                /* Senza Filtro */
                sql = "SELECT IdDDTMef, Anno, Data, N_DDT, CodArt, " +
                      "DescriCodArt, Qta, Importo, Acquirente, PrezzoUnitario, AnnoN_DDT " +
                      "FROM TblDDTMef " +
                      "WHERE Anno LIKE @pAnno AND N_DDT LIKE @pN_DDT ";

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
        public static List<Mamg0> GetListino(string codArt1, string desc1)
        {
            List<Mamg0> list = new List<Mamg0>();
            string sql = "";
            SqlDataReader dr = null;
            SqlConnection cn = GetConnection();

            codArt1 = "%" + codArt1 + "%";
            desc1 = "%" + desc1 + "%";

            try
            {
                if (codArt1 == "%%" && desc1 == "%%")
                {
                    sql = "SELECT TOP 300 (AA_SIGF + AA_CODF) AS CodArt, AA_DES, AA_UM, AA_PZ, AA_PRZ, AA_SCONTO1, AA_SCONTO2, AA_SCONTO3, AA_PRZ1 " +
                          "FROM MAMG0 " +
                          "ORDER BY CodArt ASC ";
                }
                else
                {
                    sql = "SELECT (AA_SIGF + AA_CODF) AS CodArt, AA_DES, AA_UM, AA_PZ, AA_PRZ, AA_SCONTO1, AA_SCONTO2, AA_SCONTO3, AA_PRZ1 " +
                          "FROM MAMG0 " +
                          "WHERE (AA_SIGF + AA_CODF) LIKE @pCodArt1 " +
                          "AND AA_DES LIKE @pDescriCodArt1 " +
                          "ORDER BY CodArt ASC ";
                }

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pCodArt1", codArt1));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt1", desc1));
                dr = cmd.ExecuteReader(); //Esegue il comando e lo inserisce nel DataReader

                while (dr.Read())
                {
                    Mamg0 m = new Mamg0();
                    m.CodArt = (dr.IsDBNull(0) ? null : dr.GetString(0));
                    m.Desc = (dr.IsDBNull(1) ? null : dr.GetString(1));
                    m.UnitMis = (dr.IsDBNull(2) ? null : dr.GetString(2));
                    m.Pezzo = (dr.IsDBNull(3) ? (float)0.0 : (float)dr.GetDouble(3));
                    m.PrezzoListino = (dr.IsDBNull(4) ? (float)0.0 : (float)dr.GetDouble(4));
                    m.Sconto1 = (dr.IsDBNull(5) ? (float)0.0 : (float)dr.GetDouble(5));
                    m.Sconto2 = (dr.IsDBNull(6) ? (float)0.0 : (float)dr.GetDouble(6));
                    m.Sconto3 = (dr.IsDBNull(7) ? (float)0.0 : (float)dr.GetDouble(7));
                    m.PrezzoNetto = (dr.IsDBNull(8) ? (float)0.0 : (float)dr.GetDouble(8));
                    list.Add(m);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante il recupero del listino con i filtri", ex);
            }
            finally { cn.Close(); }
        }

        //INSERT
        public static bool InserisciMaterialeCantiere(string idCant, string descrMate, string qta, bool visibile, bool ricalcolo, bool ricarico, string data, string pzzoUni,
            string rientro, string codArt, string descriCodArt, string unitMis, string zOldNumBolla, string mate, string fascia, string pzzoTemp, string acquirente,
            string fornitore, string numeroBolla, string protocollo, string note)
        {
            SqlConnection cn = GetConnection();
            string sql = "";

            try
            {
                sql = "INSERT INTO TblMaterialiCantieri ([IdTblCantieri],[DescriMateriali],[Qta],[Visibile],[Ricalcolo],[ricaricoSiNo],[Data],[PzzoUniCantiere], " +
                      "[Rientro],[CodArt],[DescriCodArt],[UnitàDiMisura],[ZOldNumeroBolla],[Mate],[Fascia],[pzzoTemp],[Acquirente],[Fornitore],[NumeroBolla], " +
                      "[ProtocolloInterno],[Note]) " +
                      "VALUES (@pIdCant,@pDescrMat,@pQta,@pVisibile,@pRicalcolo,@pRicarico,@pData,@pPzzoUnit,@pRientro,@pCodArt,@pDescriCodArt,@pUnitMis, " +
                      "@pZOldNumBolla,@pMate,@pFascia,@pPzzoTemp,@pAcquirente,@pFornitore,@pNumBolla,@pProtocollo,@pNote)";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Add(new SqlParameter("pIdCant", idCant));
                cmd.Parameters.Add(new SqlParameter("pDescrMat", descrMate));
                cmd.Parameters.Add(new SqlParameter("pQta", qta));
                cmd.Parameters.Add(new SqlParameter("pVisibile", visibile));
                cmd.Parameters.Add(new SqlParameter("pRicalcolo", ricalcolo));
                cmd.Parameters.Add(new SqlParameter("pRicarico", ricarico));
                cmd.Parameters.Add(new SqlParameter("pData", data));
                cmd.Parameters.Add(new SqlParameter("pPzzoUnit", pzzoUni));
                cmd.Parameters.Add(new SqlParameter("pRientro", rientro));
                cmd.Parameters.Add(new SqlParameter("pCodArt", codArt));
                cmd.Parameters.Add(new SqlParameter("pDescriCodArt", descriCodArt));
                cmd.Parameters.Add(new SqlParameter("pUnitMis", unitMis));
                cmd.Parameters.Add(new SqlParameter("pZOldNumBolla", zOldNumBolla));
                cmd.Parameters.Add(new SqlParameter("pMate", mate));
                cmd.Parameters.Add(new SqlParameter("pFascia", fascia));
                cmd.Parameters.Add(new SqlParameter("pPzzoTemp", pzzoTemp));
                cmd.Parameters.Add(new SqlParameter("pAcquirente", acquirente));
                cmd.Parameters.Add(new SqlParameter("pFornitore", fornitore));
                cmd.Parameters.Add(new SqlParameter("pNumBolla", numeroBolla));
                cmd.Parameters.Add(new SqlParameter("pProtocollo", protocollo));
                cmd.Parameters.Add(new SqlParameter("pNote", note));

                int row = cmd.ExecuteNonQuery();

                if(row > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'inserimento di un materiale cantiere",ex);
            }
            finally { cn.Close(); }
        }
    }
}