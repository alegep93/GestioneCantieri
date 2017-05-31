using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestioneCantieri.Data
{
    public class Frutti
    {
        int id;
        string descr;

        public Frutti()
        {
            this.id = -1;
            this.descr = "";
        }

        public Frutti(int id, string descr)
        {
            this.id = id;
            this.descr = descr;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Descr
        {
            get { return descr; }
            set { descr = value; }
        }
    }
}