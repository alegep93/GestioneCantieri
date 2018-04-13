namespace GestioneCantieri.Data
{
    public class StampaFruttiPerGruppi
    {
        int qta = 0;
        string nomeGruppo = "", nomeFrutto = "";

        public int Qta { get => qta; set => qta = value; }
        public string NomeGruppo { get => nomeGruppo; set => nomeGruppo = value; }
        public string NomeFrutto { get => nomeFrutto; set => nomeFrutto = value; }
    }
}