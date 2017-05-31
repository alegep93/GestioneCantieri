using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestioneCantieri.Data
{
    public class Clienti
    {
        int id, telefono, cellulare;
        string ragSocCli, indirizzo, cap, citta, pIva, codFisc, prov, note;
        DateTime dataInserimento;

        public Clienti()
        {
            this.id = this.telefono = this.cellulare = -1;
            this.ragSocCli = this.indirizzo = this.cap = "";
            this.citta = this.pIva = this.codFisc = this.prov = "";
            this.note = "";
            this.dataInserimento = new DateTime();
        }

        public Clienti(int id, int telefono, int cellulare, string ragSocCli, string indirizzo, string cap, string citta, string pIva, string codFisc, string prov, string note, DateTime dataInserimento)
        {
            this.id = id;
            this.telefono = telefono;
            this.cellulare = cellulare;
            this.ragSocCli = ragSocCli;
            this.indirizzo = indirizzo;
            this.cap = cap;
            this.citta = citta;
            this.pIva = pIva;
            this.codFisc = codFisc;
            this.prov = prov;
            this.note = note;
            this.dataInserimento = dataInserimento;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        public int Cellulare
        {
            get { return cellulare; }
            set { cellulare = value; }
        }

        public string RagSocCli
        {
            get { return ragSocCli; }
            set { ragSocCli = value; }
        }

        public string Indirizzo
        {
            get { return indirizzo; }
            set { indirizzo = value; }
        }

        public string Cap
        {
            get { return cap; }
            set { cap = value; }
        }

        public string Citta
        {
            get { return citta; }
            set { citta = value; }
        }

        public string PIva
        {
            get { return pIva; }
            set { pIva = value; }
        }

        public string CodFisc
        {
            get { return codFisc; }
            set { codFisc = value; }
        }

        public string Prov
        {
            get { return prov; }
            set { prov = value; }
        }

        public string Note
        {
            get { return note; }
            set { note = value; }
        }

        public DateTime DataInserimento
        {
            get { return dataInserimento; }
            set { dataInserimento = value; }
        }
    }
}