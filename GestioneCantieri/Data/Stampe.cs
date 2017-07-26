using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestioneCantieri.Data
{
    public class Stampe
    {
        int id;
        string nomeStampa;

        public Stampe()
        {
            this.id = -1;
            this.nomeStampa = "";
        }

        public Stampe(int id, string nomeStampa)
        {
            this.id = id;
            this.nomeStampa = nomeStampa;
        }

        public int Id { get { return id; } set { id = value; } }
        public string NomeStampa { get { return nomeStampa; } set { nomeStampa = value; } }
    }
}