using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestioneCantieri.Data
{
    public class Cantieri
    {
        int idCantieri, idtblClienti, ricarico, iva, anno, fasciaTblCantieri;
        string codCant, descriCodCAnt, indirizzo, città, numero, ragSocCli, codRiferCant;
        decimal pzzoManodopera, valorePreventivo;
        bool chiuso, riscosso, preventivo, daDividere, diviso, fatturato;
        DateTime data;

        public Cantieri()
        {
            this.idCantieri = this.idtblClienti = this.ricarico = this.iva = this.anno = this.fasciaTblCantieri = -1;
            this.codCant = this.descriCodCAnt = this.indirizzo = this.città = this.numero = this.ragSocCli = this.CodRiferCant = "";
            this.pzzoManodopera = this.valorePreventivo = -1m;
            this.chiuso = this.riscosso = this.preventivo = this.daDividere = this.diviso = this.fatturato = false;
            this.data = new DateTime();
        }

        public Cantieri(int idCantieri, int idtblClienti, int ricarico, int iva, int anno, int fasciaTblCantieri, string codCant, string descriCodCAnt, string indirizzo, string città, string numero, string ragSocCli, string codRiferCant, decimal pzzoManodopera, decimal valorePreventivo, bool chiuso, bool riscosso, bool preventivo, bool daDividere, bool diviso, bool fatturato, DateTime data)
        {
            this.idCantieri = idCantieri;
            this.idtblClienti = idtblClienti;
            this.ricarico = ricarico;
            this.iva = iva;
            this.anno = anno;
            this.fasciaTblCantieri = fasciaTblCantieri;
            this.codCant = codCant;
            this.descriCodCAnt = descriCodCAnt;
            this.indirizzo = indirizzo;
            this.città = città;
            this.numero = numero;
            this.ragSocCli = ragSocCli;
            this.CodRiferCant = codRiferCant;
            this.pzzoManodopera = pzzoManodopera;
            this.valorePreventivo = valorePreventivo;
            this.chiuso = chiuso;
            this.riscosso = riscosso;
            this.preventivo = preventivo;
            this.daDividere = daDividere;
            this.diviso = diviso;
            this.fatturato = fatturato;
            this.data = data;
        }

        public int IdCantieri
        {
            get
            {
                return idCantieri;
            }

            set
            {
                idCantieri = value;
            }
        }

        public int IdtblClienti
        {
            get
            {
                return idtblClienti;
            }

            set
            {
                idtblClienti = value;
            }
        }

        public int Ricarico
        {
            get
            {
                return ricarico;
            }

            set
            {
                ricarico = value;
            }
        }

        public int Iva
        {
            get
            {
                return iva;
            }

            set
            {
                iva = value;
            }
        }

        public int Anno
        {
            get
            {
                return anno;
            }

            set
            {
                anno = value;
            }
        }

        public int FasciaTblCantieri
        {
            get
            {
                return fasciaTblCantieri;
            }

            set
            {
                fasciaTblCantieri = value;
            }
        }

        public string CodCant
        {
            get
            {
                return codCant;
            }

            set
            {
                codCant = value;
            }
        }

        public string DescriCodCAnt
        {
            get
            {
                return descriCodCAnt;
            }

            set
            {
                descriCodCAnt = value;
            }
        }

        public string Indirizzo
        {
            get
            {
                return indirizzo;
            }

            set
            {
                indirizzo = value;
            }
        }

        public string Città
        {
            get
            {
                return città;
            }

            set
            {
                città = value;
            }
        }

        public string Numero
        {
            get
            {
                return numero;
            }

            set
            {
                numero = value;
            }
        }

        public string RagSocCli
        {
            get
            {
                return ragSocCli;
            }

            set
            {
                ragSocCli = value;
            }
        }

        public decimal PzzoManodopera
        {
            get
            {
                return pzzoManodopera;
            }

            set
            {
                pzzoManodopera = value;
            }
        }

        public decimal ValorePreventivo
        {
            get
            {
                return valorePreventivo;
            }

            set
            {
                valorePreventivo = value;
            }
        }

        public bool Chiuso
        {
            get
            {
                return chiuso;
            }

            set
            {
                chiuso = value;
            }
        }

        public bool Riscosso
        {
            get
            {
                return riscosso;
            }

            set
            {
                riscosso = value;
            }
        }

        public bool Preventivo
        {
            get
            {
                return preventivo;
            }

            set
            {
                preventivo = value;
            }
        }

        public bool DaDividere
        {
            get
            {
                return daDividere;
            }

            set
            {
                daDividere = value;
            }
        }

        public bool Diviso
        {
            get
            {
                return diviso;
            }

            set
            {
                diviso = value;
            }
        }

        public bool Fatturato
        {
            get
            {
                return fatturato;
            }

            set
            {
                fatturato = value;
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

        public string CodRiferCant
        {
            get
            {
                return codRiferCant;
            }

            set
            {
                codRiferCant = value;
            }
        }
    }
}