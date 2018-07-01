using System;

namespace GestioneCantieri.Data
{
    public class DDTMef
    {
        int id = 0, anno = 0, n_ddt = 0, qta = 0, annoN_ddt = 0, idFornitore = 0;
        DateTime data = new DateTime();
        string codArt = "", descriCodArt = "", acquirente = "";
        decimal importo = 0, prezzoUnitario = 0, valore = 0;
        bool daInserire = false;

        public int Id { get => id; set => id = value; }
        public int Anno { get => anno; set => anno = value; }
        public int N_ddt { get => n_ddt; set => n_ddt = value; }
        public int Qta { get => qta; set => qta = value; }
        public int AnnoN_ddt { get => annoN_ddt; set => annoN_ddt = value; }
        public int IdFornitore { get => idFornitore; set => idFornitore = value; }
        public DateTime Data { get => data; set => data = value; }
        public string CodArt { get => codArt; set => codArt = value; }
        public string DescriCodArt { get => descriCodArt; set => descriCodArt = value; }
        public string Acquirente { get => acquirente; set => acquirente = value; }
        public decimal Importo { get => importo; set => importo = value; }
        public decimal PrezzoUnitario { get => prezzoUnitario; set => prezzoUnitario = value; }
        public decimal Valore { get => valore; set => valore = value; }
        public bool DaInserire { get => daInserire; set => daInserire = value; }
    }
}