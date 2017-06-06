using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestioneCantieri.Data
{
    public class Mamg0
    {
        
        float pezzo, prezzoListino, prezzoNetto, sconto1, sconto2, sconto3;
        string codArt, desc, unitMis;

        public float Pezzo
        {
            get
            {
                return pezzo;
            }

            set
            {
                pezzo = value;
            }
        }

        public float PrezzoListino
        {
            get
            {
                return prezzoListino;
            }

            set
            {
                prezzoListino = value;
            }
        }

        public float PrezzoNetto
        {
            get
            {
                return prezzoNetto;
            }

            set
            {
                prezzoNetto = value;
            }
        }

        public float Sconto1
        {
            get
            {
                return sconto1;
            }

            set
            {
                sconto1 = value;
            }
        }

        public float Sconto2
        {
            get
            {
                return sconto2;
            }

            set
            {
                sconto2 = value;
            }
        }

        public float Sconto3
        {
            get
            {
                return sconto3;
            }

            set
            {
                sconto3 = value;
            }
        }

        public string CodArt
        {
            get
            {
                return codArt;
            }

            set
            {
                codArt = value;
            }
        }

        public string Desc
        {
            get
            {
                return desc;
            }

            set
            {
                desc = value;
            }
        }

        public string UnitMis
        {
            get
            {
                return unitMis;
            }

            set
            {
                unitMis = value;
            }
        }

        public Mamg0()
        {
            this.PrezzoListino = this.PrezzoNetto = this.Pezzo = this.Sconto1 = this.Sconto2 = this.Sconto3 = 0.0F;
            this.CodArt = this.Desc = this.UnitMis = "";
        }

        public Mamg0(float pezzo, float sconto1, float sconto2, float sconto3, string codArt, string desc, string unitMis, float prezzoListino, float prezzoNetto)
        {
            this.Pezzo = pezzo;
            this.Sconto1 = sconto1;
            this.Sconto2 = sconto2;
            this.Sconto3 = sconto3;
            this.CodArt = codArt;
            this.Desc = desc;
            this.UnitMis = unitMis;
            this.PrezzoListino = prezzoListino;
            this.PrezzoNetto = prezzoNetto;
        }
    }
}