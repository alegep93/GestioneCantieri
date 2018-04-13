
namespace GestioneCantieri.Data
{
    public class StampaOrdFrutCantLoc
    {
        int qta=0;
        string descr001="", nomeLocale = "", nomeGruppo = "";

        public int Qta { get => qta; set => qta = value; }
        public string Descr001 { get => descr001; set => descr001 = value; }
        public string NomeLocale { get => nomeLocale; set => nomeLocale = value; }
        public string NomeGruppo { get => nomeGruppo; set => nomeGruppo = value; }
    }
}