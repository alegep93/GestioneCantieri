using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestioneCantieri.Data
{
    public class MaterialiCantieri
    {
        int idMaterialiCantieri, idTblCantieri, idOperaio, fascia, protocolloInterno;
        string descriMateriali, codArt, descriCodArt, tipologia, acquirente, numeroBolla, fornitore, note, note2, codCant, descriCodCant, ragSocCli;
        double qta;
        decimal pzzoUniCantiere, pzzoFinCli, valore, costoOperaio, valoreRicarico, valoreRicalcolo;
        bool visibile, ricalcolo, ricaricoSiNo, rientro, operaioPagato;
        DateTime data;

        public MaterialiCantieri()
        {
            this.idMaterialiCantieri = this.idTblCantieri = this.IdOperaio = this.fascia = this.protocolloInterno = -1;
            this.descriMateriali = this.codArt = this.descriCodArt = this.tipologia = this.numeroBolla = "";
            this.acquirente = this.fornitore = this.note = this.note2 = this.CodCant = this.DescriCodCant = this.RagSocCli = "";
            this.qta = -1d;
            this.pzzoUniCantiere = this.PzzoFinCli = this.Valore = this.costoOperaio = this.valoreRicarico = this.valoreRicalcolo = 0m;
            this.visibile = this.ricalcolo = this.ricaricoSiNo = this.rientro = this.operaioPagato = false;
            this.data = new DateTime();
        }

        public MaterialiCantieri(int idMaterialiCantieri, int idTblCantieri, int fascia, string numeroBolla, int protocolloInterno, string descriMateriali, string codArt, string descriCodArt, string unitàDiMisura, string zOldNumeroBolla, string mate, string acquirente, string fornitore, string note, string note2, double qta, decimal pzzoUniCantiere, decimal pzzoTemp, bool visibile, bool ricalcolo, bool ricaricoSiNo, bool rientro, DateTime data, decimal pzzoFinCli, string codCant, decimal valore, string descriCodCant, bool operaioPagato, decimal costoOperaio)
        {
            this.idMaterialiCantieri = idMaterialiCantieri;
            this.idTblCantieri = idTblCantieri;
            this.fascia = fascia;
            this.numeroBolla = numeroBolla;
            this.protocolloInterno = protocolloInterno;
            this.descriMateriali = descriMateriali;
            this.codArt = codArt;
            this.descriCodArt = descriCodArt;
            this.tipologia = mate;
            this.acquirente = acquirente;
            this.fornitore = fornitore;
            this.note = note;
            this.note2 = note2;
            this.PzzoFinCli = pzzoFinCli;
            this.qta = qta;
            this.pzzoUniCantiere = pzzoUniCantiere;
            this.visibile = visibile;
            this.ricalcolo = ricalcolo;
            this.ricaricoSiNo = ricaricoSiNo;
            this.rientro = rientro;
            this.data = data;
            this.codCant = codCant;
            this.valore = valore;
            this.descriCodCant = descriCodCant;
            this.operaioPagato = operaioPagato;
            this.costoOperaio = costoOperaio;
        }

        public int IdMaterialiCantieri
        {
            get
            {
                return idMaterialiCantieri;
            }

            set
            {
                idMaterialiCantieri = value;
            }
        }

        public int IdTblCantieri
        {
            get
            {
                return idTblCantieri;
            }

            set
            {
                idTblCantieri = value;
            }
        }

        public int Fascia
        {
            get
            {
                return fascia;
            }

            set
            {
                fascia = value;
            }
        }

        public string NumeroBolla
        {
            get
            {
                return numeroBolla;
            }

            set
            {
                numeroBolla = value;
            }
        }

        public int ProtocolloInterno
        {
            get
            {
                return protocolloInterno;
            }

            set
            {
                protocolloInterno = value;
            }
        }

        public string DescriMateriali
        {
            get
            {
                return descriMateriali;
            }

            set
            {
                descriMateriali = value;
            }
        }

        public string CodArt
        {
            get
            {
                return codArt;
            }

            set
            {
                codArt = value;
            }
        }

        public string DescriCodArt
        {
            get
            {
                return descriCodArt;
            }

            set
            {
                descriCodArt = value;
            }
        }

        public string Tipologia
        {
            get
            {
                return tipologia;
            }

            set
            {
                tipologia = value;
            }
        }

        public string Acquirente
        {
            get
            {
                return acquirente;
            }

            set
            {
                acquirente = value;
            }
        }

        public string Fornitore
        {
            get
            {
                return fornitore;
            }

            set
            {
                fornitore = value;
            }
        }

        public string Note
        {
            get
            {
                return note;
            }

            set
            {
                note = value;
            }
        }

        public double Qta
        {
            get
            {
                return qta;
            }

            set
            {
                qta = value;
            }
        }

        public decimal PzzoUniCantiere
        {
            get
            {
                return pzzoUniCantiere;
            }

            set
            {
                pzzoUniCantiere = value;
            }
        }

        public bool Visibile
        {
            get
            {
                return visibile;
            }

            set
            {
                visibile = value;
            }
        }

        public bool Ricalcolo
        {
            get
            {
                return ricalcolo;
            }

            set
            {
                ricalcolo = value;
            }
        }

        public bool RicaricoSiNo
        {
            get
            {
                return ricaricoSiNo;
            }

            set
            {
                ricaricoSiNo = value;
            }
        }

        public bool Rientro
        {
            get
            {
                return rientro;
            }

            set
            {
                rientro = value;
            }
        }

        public DateTime Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }
        }

        public decimal PzzoFinCli
        {
            get
            {
                return pzzoFinCli;
            }

            set
            {
                pzzoFinCli = value;
            }
        }

        public string CodCant { get => codCant; set => codCant = value; }
        public decimal Valore { get => valore; set => valore = value; }
        public string DescriCodCant { get => descriCodCant; set => descriCodCant = value; }
        public bool OperaioPagato { get => operaioPagato; set => operaioPagato = value; }
        public decimal CostoOperaio { get => costoOperaio; set => costoOperaio = value; }
        public decimal ValoreRicarico { get => valoreRicarico; set => valoreRicarico = value; }
        public decimal ValoreRicalcolo { get => valoreRicalcolo; set => valoreRicalcolo = value; }
        public string RagSocCli { get => ragSocCli; set => ragSocCli = value; }
        public string Note2 { get => note2; set => note2 = value; }
        public int IdOperaio { get => idOperaio; set => idOperaio = value; }
    }
}