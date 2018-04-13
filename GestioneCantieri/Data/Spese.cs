
namespace GestioneCantieri.Data
{
    public class Spese
    {
        int idSpesa = 0;
        string descrizione = "";
        decimal prezzo = 0;

        public int IdSpesa { get => idSpesa; set => idSpesa = value; }
        public string Descrizione { get => descrizione; set => descrizione = value; }
        public decimal Prezzo { get => prezzo; set => prezzo = value; }
    }
}