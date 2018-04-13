
namespace GestioneCantieri.Data
{
    public class Fornitori
    {
        int idFornitori = 0, tel1 = 0, cell1 = 0;
        double partitaIva = 0;
        string ragSocForni = "", indirizzo = "", cap = "", città = "", codFiscale = "", abbreviato = "";

        public int IdFornitori { get => idFornitori; set => idFornitori = value; }
        public int Tel1 { get => tel1; set => tel1 = value; }
        public int Cell1 { get => cell1; set => cell1 = value; }
        public double PartitaIva { get => partitaIva; set => partitaIva = value; }
        public string RagSocForni { get => ragSocForni; set => ragSocForni = value; }
        public string Indirizzo { get => indirizzo; set => indirizzo = value; }
        public string Cap { get => cap; set => cap = value; }
        public string Città { get => città; set => città = value; }
        public string CodFiscale { get => codFiscale; set => codFiscale = value; }
        public string Abbreviato { get => abbreviato; set => abbreviato = value; }
    }
}