using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestioneCantieri.Data
{
    public class Clienti
    {
        int idCliente, tel1, cell1;
        string ragSocCli, indirizzo, cap, città, partitaIva, codFiscale, provincia, note;
        DateTime data;

        public Clienti()
        {
            this.idCliente = this.tel1 = this.cell1 = -1;
            this.ragSocCli = this.indirizzo = this.cap = this.città = this.partitaIva = "";
            this.codFiscale = this.provincia = this.note = "";
            this.data = new DateTime();
        }

        public Clienti(int idCliente, int tel1, int cell1, string ragSocCli, string indirizzo, string cap, string città, string partitaIva, string codFiscale, string provincia, string note, DateTime data)
        {
            this.idCliente = idCliente;
            this.tel1 = tel1;
            this.cell1 = cell1;
            this.ragSocCli = ragSocCli;
            this.indirizzo = indirizzo;
            this.cap = cap;
            this.città = città;
            this.partitaIva = partitaIva;
            this.codFiscale = codFiscale;
            this.provincia = provincia;
            this.note = note;
            this.data = data;
        }

        public int IdCliente
        {
            get
            {
                return idCliente;
            }

            set
            {
                idCliente = value;
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

        public string RagSocCli
        {
            get
            {
                return ragSocCli;
            }

            set
            {
                ragSocCli = value;
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

        public string PartitaIva
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

        public string Provincia
        {
            get
            {
                return provincia;
            }

            set
            {
                provincia = value;
            }
        }

        public string Note
        {
            get
            {
                return note;
            }

            set
            {
                note = value;
            }
        }

        public DateTime Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }
        }
    }
}