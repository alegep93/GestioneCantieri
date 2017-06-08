using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestioneCantieri.Data
{
    public class Fornitori
    {
        int idFornitori, tel1, cell1;
        double partitaIva;
        string ragSocForni, indirizzo, cap, città, codFiscale, abbreviato;

        public Fornitori()
        {
            this.idFornitori = this.tel1 = this.cell1 = -1;
            this.partitaIva = -1f;
            this.ragSocForni = this.indirizzo = this.cap = this.città = "";
            this.codFiscale = this.abbreviato = "";
        }

        public Fornitori(int idFornitori, int tel1, int cell1, double partitaIva, string ragSocForni, string indirizzo, string cap, string città, string codFiscale, string abbreviato)
        {
            this.idFornitori = idFornitori;
            this.tel1 = tel1;
            this.cell1 = cell1;
            this.partitaIva = partitaIva;
            this.ragSocForni = ragSocForni;
            this.indirizzo = indirizzo;
            this.cap = cap;
            this.città = città;
            this.codFiscale = codFiscale;
            this.abbreviato = abbreviato;
        }

        public string Abbreviato
        {
            get
            {
                return abbreviato;
            }

            set
            {
                abbreviato = value;
            }
        }

        public string Cap
        {
            get
            {
                return cap;
            }

            set
            {
                cap = value;
            }
        }

        public int Cell1
        {
            get
            {
                return cell1;
            }

            set
            {
                cell1 = value;
            }
        }

        public string Città
        {
            get
            {
                return città;
            }

            set
            {
                città = value;
            }
        }

        public string CodFiscale
        {
            get
            {
                return codFiscale;
            }

            set
            {
                codFiscale = value;
            }
        }

        public int IdFornitori
        {
            get
            {
                return idFornitori;
            }

            set
            {
                idFornitori = value;
            }
        }

        public string Indirizzo
        {
            get
            {
                return indirizzo;
            }

            set
            {
                indirizzo = value;
            }
        }

        public double PartitaIva
        {
            get
            {
                return partitaIva;
            }

            set
            {
                partitaIva = value;
            }
        }

        public string RagSocForni
        {
            get
            {
                return ragSocForni;
            }

            set
            {
                ragSocForni = value;
            }
        }

        public int Tel1
        {
            get
            {
                return tel1;
            }

            set
            {
                tel1 = value;
            }
        }
    }
}