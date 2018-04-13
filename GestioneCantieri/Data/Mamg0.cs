
namespace GestioneCantieri.Data
{
    public class Mamg0
    {
        float pezzo = 0, prezzoListino = 0, prezzoNetto = 0, sconto1 = 0, sconto2 = 0, sconto3 = 0;
        string codArt = "", desc = "", unitMis = "";

        public float Pezzo { get => pezzo; set => pezzo = value; }
        public float PrezzoListino { get => prezzoListino; set => prezzoListino = value; }
        public float PrezzoNetto { get => prezzoNetto; set => prezzoNetto = value; }
        public float Sconto1 { get => sconto1; set => sconto1 = value; }
        public float Sconto2 { get => sconto2; set => sconto2 = value; }
        public float Sconto3 { get => sconto3; set => sconto3 = value; }
        public string CodArt { get => codArt; set => codArt = value; }
        public string Desc { get => desc; set => desc = value; }
        public string UnitMis { get => unitMis; set => unitMis = value; }
    }
}