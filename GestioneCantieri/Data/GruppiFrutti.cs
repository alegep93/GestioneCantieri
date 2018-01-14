using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestioneCantieri.Data
{
    public class GruppiFrutti
    {
        int id;
        string nomeGruppo, descr;
        bool completato, controllato;

        public GruppiFrutti()
        {
            id = -1;
            nomeGruppo = descr = "";
        }

        public GruppiFrutti(int id, string nomeGruppo, string descr)
        {
            this.id = id;
            this.nomeGruppo = nomeGruppo;
            this.descr = descr;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string NomeGruppo
        {
            get { return nomeGruppo; }
            set { nomeGruppo = value; }
        }

        public string Descr { get; set; }
        public bool Completato { get => completato; set => completato = value; }
        public bool Controllato { get => controllato; set => controllato = value; }
    }
}