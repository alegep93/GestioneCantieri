using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestioneCantieri.Data
{
    public class TipDatCant
    {
        int idTipologia;
        string descrizione, abbreviato;

        public TipDatCant()
        {
            this.idTipologia = -1;
            this.descrizione = this.abbreviato = "";
        }

        public TipDatCant(int idTipologia, string descrizione, string abbreviato)
        {
            this.idTipologia = idTipologia;
            this.descrizione = descrizione;
            this.abbreviato = abbreviato;
        }

        public int IdTipologia
        {
            get
            {
                return idTipologia;
            }

            set
            {
                idTipologia = value;
            }
        }

        public string Descrizione
        {
            get
            {
                return descrizione;
            }

            set
            {
                descrizione = value;
            }
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
    }
}