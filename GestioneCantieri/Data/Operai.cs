using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestioneCantieri.Data
{
    public class Operai
    {
        int idOperaio;
        string nomeOp, descrOp, suffisso, operaio;
        decimal costoOperaio;

        public Operai()
        {
            this.idOperaio = -1;
            this.nomeOp = this.descrOp = this.suffisso = this.operaio = "";
            this.costoOperaio = 0.0m;
        }

        public Operai(int idOperaio, string nomeOp, string descrOp, string suffisso, string operaio, decimal costoOperaio)
        {
            this.idOperaio = idOperaio;
            this.nomeOp = nomeOp;
            this.descrOp = descrOp;
            this.suffisso = suffisso;
            this.operaio = operaio;
            this.costoOperaio = costoOperaio;
        }

        public string DescrOp
        {
            get
            {
                return descrOp;
            }

            set
            {
                descrOp = value;
            }
        }

        public int IdOperaio
        {
            get
            {
                return idOperaio;
            }

            set
            {
                idOperaio = value;
            }
        }

        public string NomeOp
        {
            get
            {
                return nomeOp;
            }

            set
            {
                nomeOp = value;
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

        public decimal CostoOperaio
        {
            get { return costoOperaio; }
            set { costoOperaio = value; }
        }
    }
}