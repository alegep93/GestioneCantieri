using System;

namespace GestioneCantieri.Data
{
    public class MatOrdFrut
    {
        int id = 0, idCantiere = 0, idGruppiFrutti = 0, idLocali = 0, idFrutto = 0, qtaFrutti = 0;
        DateTime dataOrdine = new DateTime();
        string appartamento = "", nomeGruppo = "", nomeFrutto = "", descrizione = "", descrCant = "";

        public int Id { get => id; set => id = value; }
        public int IdCantiere { get => idCantiere; set => idCantiere = value; }
        public int IdGruppiFrutti { get => idGruppiFrutti; set => idGruppiFrutti = value; }
        public int IdLocali { get => idLocali; set => idLocali = value; }
        public int IdFrutto { get => idFrutto; set => idFrutto = value; }
        public int QtaFrutti { get => qtaFrutti; set => qtaFrutti = value; }
        public DateTime DataOrdine { get => dataOrdine; set => dataOrdine = value; }
        public string Appartamento { get => appartamento; set => appartamento = value; }
        public string NomeGruppo { get => nomeGruppo; set => nomeGruppo = value; }
        public string Descrizione { get => descrizione; set => descrizione = value; }
        public string DescrCant { get => descrCant; set => descrCant = value; }
        public string NomeFrutto { get => nomeFrutto; set => nomeFrutto = value; }
    }
}