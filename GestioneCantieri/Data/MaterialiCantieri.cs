using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestioneCantieri.Data
{
    public class MaterialiCantieri
    {
        int idMaterialiCantieri, idTblCantieri, fascia, numeroBolla, protocolloInterno;
        string descriMateriali, codArt, descriCodArt, unitàDiMisura, zOldNumeroBolla, mate, acquirente, fornitore, note;
        decimal qta, pzzoUniCantiere, pzzoTemp;
        bool visibile, ricalcolo, ricaricoSiNo, rientro;
        DateTime data;

        public MaterialiCantieri()
        {
            this.idMaterialiCantieri = this.idTblCantieri = this.fascia = this.numeroBolla = this.protocolloInterno = -1;
            this.descriMateriali = this.codArt = this.descriCodArt = this.unitàDiMisura = this.zOldNumeroBolla = this.mate = "";
            this.acquirente = this.fornitore = this.note = "";
            this.qta = this.pzzoUniCantiere = this.pzzoTemp = -1m;
            this.visibile = this.ricalcolo = this.ricaricoSiNo = this.rientro = false;
            this.data = new DateTime();
        }

        public MaterialiCantieri(int idMaterialiCantieri, int idTblCantieri, int fascia, int numeroBolla, int protocolloInterno, string descriMateriali, string codArt, string descriCodArt, string unitàDiMisura, string zOldNumeroBolla, string mate, string acquirente, string fornitore, string note, decimal qta, decimal pzzoUniCantiere, decimal pzzoTemp, bool visibile, bool ricalcolo, bool ricaricoSiNo, bool rientro, DateTime data)
        {
            this.idMaterialiCantieri = idMaterialiCantieri;
            this.idTblCantieri = idTblCantieri;
            this.fascia = fascia;
            this.numeroBolla = numeroBolla;
            this.protocolloInterno = protocolloInterno;
            this.descriMateriali = descriMateriali;
            this.codArt = codArt;
            this.descriCodArt = descriCodArt;
            this.unitàDiMisura = unitàDiMisura;
            this.zOldNumeroBolla = zOldNumeroBolla;
            this.mate = mate;
            this.acquirente = acquirente;
            this.fornitore = fornitore;
            this.note = note;
            this.qta = qta;
            this.pzzoUniCantiere = pzzoUniCantiere;
            this.pzzoTemp = pzzoTemp;
            this.visibile = visibile;
            this.ricalcolo = ricalcolo;
            this.ricaricoSiNo = ricaricoSiNo;
            this.rientro = rientro;
            this.data = data;
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

        public int NumeroBolla
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

        public string UnitàDiMisura
        {
            get
            {
                return unitàDiMisura;
            }

            set
            {
                unitàDiMisura = value;
            }
        }

        public string ZOldNumeroBolla
        {
            get
            {
                return zOldNumeroBolla;
            }

            set
            {
                zOldNumeroBolla = value;
            }
        }

        public string Mate
        {
            get
            {
                return mate;
            }

            set
            {
                mate = value;
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

        public decimal Qta
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

        public decimal PzzoTemp
        {
            get
            {
                return pzzoTemp;
            }

            set
            {
                pzzoTemp = value;
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
    }
}