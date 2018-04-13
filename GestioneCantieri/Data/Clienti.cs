using System;

namespace GestioneCantieri.Data
{
    public class Clienti
    {
        int idCliente = 0;
        string ragSocCli = "", indirizzo = "", cap = "", città = "", tel1 = "", cell1 = "", partitaIva = "", codFiscale = "", provincia = "", note = "";
        DateTime data = new DateTime();

        public int IdCliente { get => idCliente; set => idCliente = value; }
        public string RagSocCli { get => ragSocCli; set => ragSocCli = value; }
        public string Indirizzo { get => indirizzo; set => indirizzo = value; }
        public string Cap { get => cap; set => cap = value; }
        public string Città { get => città; set => città = value; }
        public string Tel1 { get => tel1; set => tel1 = value; }
        public string Cell1 { get => cell1; set => cell1 = value; }
        public string PartitaIva { get => partitaIva; set => partitaIva = value; }
        public string CodFiscale { get => codFiscale; set => codFiscale = value; }
        public string Provincia { get => provincia; set => provincia = value; }
        public string Note { get => note; set => note = value; }
        public DateTime Data { get => data; set => data = value; }
    }
}