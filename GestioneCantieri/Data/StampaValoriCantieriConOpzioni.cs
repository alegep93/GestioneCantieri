using System;

namespace GestioneCantieri.Data
{
    public class StampaValoriCantieriConOpzioni
    {
        int idCantieri = -1, idtblClienti = -1, ricarico = -1, iva = -1, anno = -1, fasciaTblCantieri = -1, numero = -1;
        string codCant = "", descriCodCAnt = "", indirizzo = "", città = "", ragSocCli = "", codRiferCant = "";
        decimal pzzoManodopera = -1, valorePreventivo = -1, totaleConto = -1, totaleAcconti = -1, totaleFinale = -1;
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
        public decimal TotaleConto { get => totaleConto; set => totaleConto = value; }
        public decimal TotaleAcconti { get => totaleAcconti; set => totaleAcconti = value; }
        public decimal TotaleFinale { get => totaleFinale; set => totaleFinale = value; }
        public bool Chiuso { get => chiuso; set => chiuso = value; }
        public bool Riscosso { get => riscosso; set => riscosso = value; }
        public bool Preventivo { get => preventivo; set => preventivo = value; }
        public bool DaDividere { get => daDividere; set => daDividere = value; }
        public bool Diviso { get => diviso; set => diviso = value; }
        public bool Fatturato { get => fatturato; set => fatturato = value; }
        public DateTime Data { get => data; set => data = value; }
    }
}