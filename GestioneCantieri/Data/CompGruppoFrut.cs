namespace GestioneCantieri.Data
{
    public class CompGruppoFrut
    {
        int id = 0, idGruppo = 0, idFrutto = 0, qta = 0;
        string nomeFrutto = "";

        public int Id { get => id; set => id = value; }
        public int IdGruppo { get => idGruppo; set => idGruppo = value; }
        public int IdFrutto { get => idFrutto; set => idFrutto = value; }
        public int Qta { get => qta; set => qta = value; }
        public string NomeFrutto { get => nomeFrutto; set => nomeFrutto = value; }
    }
}