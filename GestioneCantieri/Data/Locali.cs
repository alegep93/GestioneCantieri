using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestioneCantieri.Data
{
    public class Locali
    {
        int id;
        string nomeLocale;

        public Locali()
        {
            this.id = -1;
            this.nomeLocale = "";
        }

        public Locali(int id, string nomeLocale)
        {
            this.id = id;
            this.nomeLocale = nomeLocale;
        }

        public int Id { get; set; }
        public string NomeLocale { get; set; }
    }
}