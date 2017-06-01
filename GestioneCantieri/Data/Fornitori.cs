using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestioneCantieri.Data
{
    public class Fornitori
    {
        int id, tel, cel;
        float pIva;
        string ragSoc, indirizzo, cap, citta, codFisc, abbreviato;

        public Fornitori()
        {
            this.id = this.tel = this.cel = -1;
            this.pIva = -1f;
            this.ragSoc = this.indirizzo = this.cap = "";
            this.citta = this.codFisc = this.abbreviato = "";
        }

        public Fornitori(int id, int tel, int cel, float pIva, string ragSoc, string indirizzo, string cap, string citta, string codFisc, string abbreviato)
        {
            this.id = id;
            this.tel = tel;
            this.cel = cel;
            this.pIva = pIva;
            this.ragSoc = ragSoc;
            this.indirizzo = indirizzo;
            this.cap = cap;
            this.citta = citta;
            this.codFisc = codFisc;
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

        public int Cel
        {
            get
            {
                return cel;
            }

            set
            {
                cel = value;
            }
        }

        public string Citta
        {
            get
            {
                return citta;
            }

            set
            {
                citta = value;
            }
        }

        public string CodFisc
        {
            get
            {
                return codFisc;
            }

            set
            {
                codFisc = value;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
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

        public float PIva
        {
            get
            {
                return pIva;
            }

            set
            {
                pIva = value;
            }
        }

        public string RagSoc
        {
            get
            {
                return ragSoc;
            }

            set
            {
                ragSoc = value;
            }
        }

        public int Tel
        {
            get
            {
                return tel;
            }

            set
            {
                tel = value;
            }
        }
    }
}