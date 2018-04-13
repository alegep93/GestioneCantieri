using System;

namespace GestioneCantieri.Data
{
    public class MaterialiCantieri
    {
        int idMaterialiCantieri = 0, idTblCantieri = 0, idOperaio = 0, fascia = 0, protocolloInterno = 0;
        string descriMateriali = "", codArt = "", descriCodArt = "", tipologia = "", acquirente = "", numeroBolla = "";
        string fornitore = "", note = "", note2 = "", codCant = "", descriCodCant = "", ragSocCli = "";
        double qta = 0;
        decimal pzzoUniCantiere = 0, pzzoFinCli = 0, valore = 0, costoOperaio = 0, valoreRicarico = 0, valoreRicalcolo = 0;
        bool visibile = false, ricalcolo = false, ricaricoSiNo = false, rientro = false, operaioPagato = false;
        DateTime data = new DateTime();

        public int IdMaterialiCantieri { get => idMaterialiCantieri; set => idMaterialiCantieri = value; }
        public int IdTblCantieri { get => idTblCantieri; set => idTblCantieri = value; }
        public int IdOperaio { get => idOperaio; set => idOperaio = value; }
        public int Fascia { get => fascia; set => fascia = value; }
        public int ProtocolloInterno { get => protocolloInterno; set => protocolloInterno = value; }
        public string DescriMateriali { get => descriMateriali; set => descriMateriali = value; }
        public string CodArt { get => codArt; set => codArt = value; }
        public string DescriCodArt { get => descriCodArt; set => descriCodArt = value; }
        public string Tipologia { get => tipologia; set => tipologia = value; }
        public string Acquirente { get => acquirente; set => acquirente = value; }
        public string NumeroBolla { get => numeroBolla; set => numeroBolla = value; }
        public string Fornitore { get => fornitore; set => fornitore = value; }
        public string Note { get => note; set => note = value; }
        public string Note2 { get => note2; set => note2 = value; }
        public string CodCant { get => codCant; set => codCant = value; }
        public string DescriCodCant { get => descriCodCant; set => descriCodCant = value; }
        public string RagSocCli { get => ragSocCli; set => ragSocCli = value; }
        public double Qta { get => qta; set => qta = value; }
        public decimal PzzoUniCantiere { get => pzzoUniCantiere; set => pzzoUniCantiere = value; }
        public decimal PzzoFinCli { get => pzzoFinCli; set => pzzoFinCli = value; }
        public decimal Valore { get => valore; set => valore = value; }
        public decimal CostoOperaio { get => costoOperaio; set => costoOperaio = value; }
        public decimal ValoreRicarico { get => valoreRicarico; set => valoreRicarico = value; }
        public decimal ValoreRicalcolo { get => valoreRicalcolo; set => valoreRicalcolo = value; }
        public bool Visibile { get => visibile; set => visibile = value; }
        public bool Ricalcolo { get => ricalcolo; set => ricalcolo = value; }
        public bool RicaricoSiNo { get => ricaricoSiNo; set => ricaricoSiNo = value; }
        public bool Rientro { get => rientro; set => rientro = value; }
        public bool OperaioPagato { get => operaioPagato; set => operaioPagato = value; }
        public DateTime Data { get => data; set => data = value; }
    }
}