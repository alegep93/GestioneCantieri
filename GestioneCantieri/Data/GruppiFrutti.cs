
namespace GestioneCantieri.Data
{
    public class GruppiFrutti
    {
        int id = 0;
        string nomeGruppo = "", descr = "";
        bool completato = false, controllato = false;

        public int Id { get => id; set => id = value; }
        public string NomeGruppo { get => nomeGruppo; set => nomeGruppo = value; }
        public string Descr { get => descr; set => descr = value; }
        public bool Completato { get => completato; set => completato = value; }
        public bool Controllato { get => controllato; set => controllato = value; }
    }
}