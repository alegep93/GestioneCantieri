using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestioneCantieri.Data
{
    public class Operai
    {
        int id;
        string nome, descr, suffisso, operaio;

        public Operai()
        {
            this.id = -1;
            this.nome = this.descr = "";
            this.suffisso = this.operaio = "";
        }

        public Operai(int id, string nome, string descr, string suffisso, string operaio)
        {
            this.id = id;
            this.nome = nome;
            this.descr = descr;
            this.suffisso = suffisso;
            this.operaio = operaio;
        }

        public string Descr
        {
            get
            {
                return descr;
            }

            set
            {
                descr = value;
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

        public string Nome
        {
            get
            {
                return nome;
            }

            set
            {
                nome = value;
            }
        }

        public string Operaio
        {
            get
            {
                return operaio;
            }

            set
            {
                operaio = value;
            }
        }

        public string Suffisso
        {
            get
            {
                return suffisso;
            }

            set
            {
                suffisso = value;
            }
        }
    }
}