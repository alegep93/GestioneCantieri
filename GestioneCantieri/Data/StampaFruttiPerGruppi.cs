using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestioneCantieri.Data
{
    public class StampaFruttiPerGruppi
    {
        int qta;
        string nomeGruppo, nomeFrutto;

        public StampaFruttiPerGruppi()
        {
            this.qta = -1;
            this.nomeGruppo = this.nomeFrutto = "";
        }

        public StampaFruttiPerGruppi(int qta, string nomeGruppo, string nomeFrutto)
        {
            this.qta = qta;
            this.nomeGruppo = nomeGruppo;
            this.nomeFrutto = nomeFrutto;
        }

        public int Qta
        {
            get { return qta; }
            set { qta = value; }
        }

        public string NomeGruppo
        {
            get { return nomeGruppo; }
            set { nomeGruppo = value; }
        }

        public string NomeFrutto
        {
            get { return nomeFrutto; }
            set { nomeFrutto = value; }
        }
    }
}