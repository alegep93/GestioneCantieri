using System;

namespace GestioneCantieri.Data
{
    public class DDTFornitori
    {
        int id = 0, idFornitore=0, qta=0;
        long protocollo = 0;
        string numeroDdt = "", articolo = "", descrizioneFornitore = "", descrizioneMau = "", ragSocFornitore = "";
        decimal valore = 0, prezzoUnitario = 0;
        DateTime data = new DateTime();

        public int Id { get => id; set => id = value; }
        public int IdFornitore { get => idFornitore; set => idFornitore = value; }
        public int Qta { get => qta; set => qta = value; }
        public long Protocollo { get => protocollo; set => protocollo = value; }
        public string NumeroDdt { get => numeroDdt; set => numeroDdt = value; }
        public string Articolo { get => articolo; set => articolo = value; }
        public string DescrizioneFornitore { get => descrizioneFornitore; set => descrizioneFornitore = value; }
        public string DescrizioneMau { get => descrizioneMau; set => descrizioneMau = value; }
        public string RagSocFornitore { get => ragSocFornitore; set => ragSocFornitore = value; }
        public decimal Valore { get => valore; set => valore = value; }
        public decimal PrezzoUnitario { get => prezzoUnitario; set => prezzoUnitario = value; }
        public DateTime Data { get => data; set => data = value; }
    }
}