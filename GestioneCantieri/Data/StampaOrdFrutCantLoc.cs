using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestioneCantieri.Data
{
    public class StampaOrdFrutCantLoc
    {
        int qta;
        string descrFrutto, nomeLocale, nomeGruppo;

        public int Qta
        {
            get { return qta; }
            set { qta = value; }
        }

        public string DescrFrutto
        {
            get { return descrFrutto; }
            set { descrFrutto = value; }
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
            this.DescrFrutto = this.NomeLocale = this.NomeGruppo = "";
        }

        public StampaOrdFrutCantLoc(int qta, string descrFrutto, string nomeLocale, string nomeGruppo)
        {
            this.Qta = qta;
            this.DescrFrutto = descrFrutto;
            this.NomeLocale = nomeLocale;
            this.NomeGruppo = nomeGruppo;
        }

        //public int Qta { get => qta; set => qta = value; }
        //public string DescrFrutto { get => descrFrutto; set => descrFrutto = value; }
        //public string NomeLocale { get => nomeLocale; set => nomeLocale = value; }
        //public string NomeGruppo { get => nomeGruppo; set => nomeGruppo = value; }
    }
}