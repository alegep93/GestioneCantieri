namespace GestioneCantieri.Data
{
    public class DDTMefObject
    {
        string annoInizio = "";
        string annoFine = "";
        string dataInizio = "";
        string dataFine = "";
        string qta = "";
        string nDdt = "";

        string codArt1= "";
        string codArt2= "";
        string codArt3 = "";

        string descriCodArt1 = "";
        string descriCodArt2 = "";
        string descriCodArt3 = "";

        public string AnnoInizio { get => annoInizio; set => annoInizio = value; }
        public string AnnoFine { get => annoFine; set => annoFine = value; }
        public string DataInizio { get => dataInizio; set => dataInizio = value; }
        public string DataFine { get => dataFine; set => dataFine = value; }
        public string Qta { get => qta; set => qta = value; }
        public string NDdt { get => nDdt; set => nDdt = value; }
        public string CodArt1 { get => codArt1; set => codArt1 = value; }
        public string CodArt2 { get => codArt2; set => codArt2 = value; }
        public string CodArt3 { get => codArt3; set => codArt3 = value; }
        public string DescriCodArt1 { get => descriCodArt1; set => descriCodArt1 = value; }
        public string DescriCodArt2 { get => descriCodArt2; set => descriCodArt2 = value; }
        public string DescriCodArt3 { get => descriCodArt3; set => descriCodArt3 = value; }
    }
}