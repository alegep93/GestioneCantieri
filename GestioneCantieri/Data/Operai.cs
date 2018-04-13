namespace GestioneCantieri.Data
{
    public class Operai
    {
        int idOperaio = 0;
        string nomeOp = "", descrOp = "", suffisso = "", operaio = "";
        decimal costoOperaio = 0;

        public int IdOperaio { get => idOperaio; set => idOperaio = value; }
        public string NomeOp { get => nomeOp; set => nomeOp = value; }
        public string DescrOp { get => descrOp; set => descrOp = value; }
        public string Suffisso { get => suffisso; set => suffisso = value; }
        public string Operaio { get => operaio; set => operaio = value; }
        public decimal CostoOperaio { get => costoOperaio; set => costoOperaio = value; }
    }
}