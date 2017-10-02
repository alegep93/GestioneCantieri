using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestioneCantieri.Data
{
    public class StampaOrdFrutCantLoc
    {
        int qta;
        string descr001, nomeLocale, nomeGruppo;

        public int Qta
        {
            get { return qta; }
            set { qta = value; }
        }

        public string Descr001
        {
            get { return descr001; }
            set { descr001 = value; }
        }

        public string NomeLocale
        {
            get { return nomeLocale; }
            set { nomeLocale = value; }
        }

        public string NomeGruppo
        {
            get { return nomeGruppo; }
            set { nomeGruppo = value; }
        }

        public StampaOrdFrutCantLoc()
        {
            this.Qta = -1;
            this.Descr001 = this.NomeLocale = this.NomeGruppo = "";
        }

        public StampaOrdFrutCantLoc(int qta, string descrFrutto, string nomeLocale, string nomeGruppo)
        {
            this.Qta = qta;
            this.Descr001 = descrFrutto;
            this.NomeLocale = nomeLocale;
            this.NomeGruppo = nomeGruppo;
        }
    }
}