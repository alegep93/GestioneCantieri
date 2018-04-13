using System;

namespace GestioneCantieri.Data
{
    public class Cantieri
    {
        int idCantieri = 0, idtblClienti = 0, ricarico = 0, iva = 0, anno = 0, fasciaTblCantieri = 0, numero = 0;
        string codCant = "", descriCodCAnt = "", indirizzo = "", città = "", ragSocCli = "", codRiferCant = "";
        decimal pzzoManodopera = 0, valorePreventivo = 0;
        bool chiuso = false, riscosso = false, preventivo = false, daDividere = false, diviso = false, fatturato = false;
        DateTime data = new DateTime();

        public int IdCantieri { get => idCantieri; set => idCantieri = value; }
        public int IdtblClienti { get => idtblClienti; set => idtblClienti = value; }
        public int Ricarico { get => ricarico; set => ricarico = value; }
        public int Iva { get => iva; set => iva = value; }
        public int Anno { get => anno; set => anno = value; }
        public int FasciaTblCantieri { get => fasciaTblCantieri; set => fasciaTblCantieri = value; }
        public int Numero { get => numero; set => numero = value; }
        public string CodCant { get => codCant; set => codCant = value; }
        public string DescriCodCAnt { get => descriCodCAnt; set => descriCodCAnt = value; }
        public string Indirizzo { get => indirizzo; set => indirizzo = value; }
        public string Città { get => città; set => città = value; }
        public string RagSocCli { get => ragSocCli; set => ragSocCli = value; }
        public string CodRiferCant { get => codRiferCant; set => codRiferCant = value; }
        public decimal PzzoManodopera { get => pzzoManodopera; set => pzzoManodopera = value; }
        public decimal ValorePreventivo { get => valorePreventivo; set => valorePreventivo = value; }
        public bool Chiuso { get => chiuso; set => chiuso = value; }
        public bool Riscosso { get => riscosso; set => riscosso = value; }
        public bool Preventivo { get => preventivo; set => preventivo = value; }
        public bool DaDividere { get => daDividere; set => daDividere = value; }
        public bool Diviso { get => diviso; set => diviso = value; }
        public bool Fatturato { get => fatturato; set => fatturato = value; }
        public DateTime Data { get => data; set => data = value; }
    }
}