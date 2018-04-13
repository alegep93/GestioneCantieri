using System;

namespace GestioneCantieri.Data
{
    public class Pagamenti
    {
        int idPagamenti = 0, idTblCantieri = 0;
        DateTime data = new DateTime();
        decimal imporo = 0;
        string descriPagamenti = "", codCant = "", descriCodCant = "";
        bool acconto = false, saldo = false;

        public int IdPagamenti { get => idPagamenti; set => idPagamenti = value; }
        public int IdTblCantieri { get => idTblCantieri; set => idTblCantieri = value; }
        public DateTime Data { get => data; set => data = value; }
        public decimal Imporo { get => imporo; set => imporo = value; }
        public string DescriPagamenti { get => descriPagamenti; set => descriPagamenti = value; }
        public string CodCant { get => codCant; set => codCant = value; }
        public string DescriCodCant { get => descriCodCant; set => descriCodCant = value; }
        public bool Acconto { get => acconto; set => acconto = value; }
        public bool Saldo { get => saldo; set => saldo = value; }
    }
}