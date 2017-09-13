using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestioneCantieri.Data
{
    public class Spese
    {
        int idSpesa;
        string descrizione;
        decimal prezzo;

        public Spese()
        {
            this.idSpesa = 0;
            this.descrizione = "";
            this.prezzo = 0.00m;
        }

        public Spese(int idSpesa, string descrizione, decimal prezzo)
        {
            this.idSpesa = idSpesa;
            this.descrizione = descrizione;
            this.prezzo = prezzo;
        }

        public int IdSpesa { get => idSpesa; set => idSpesa = value; }
        public string Descrizione { get => descrizione; set => descrizione = value; }
        public decimal Prezzo { get => prezzo; set => prezzo = value; }
    }
}