using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestioneCantieri.Data
{
    public class Cantieri
    {
        int idCantiere, idCliente, ricarico, iva, anno, fasciaCantiere;
        string codCantiere, descrCantiere, indirizzo, citta, numero;
        decimal prezzoManodopera, valorePreventivo;
        bool chiuso, riscosso, preventivo, daDividere, diviso, fatturato;
        DateTime data;

        public Cantieri()
        {
            this.IdCantiere = -1;
            this.CodCantiere = this.DescrCantiere = "";
        }

        public Cantieri(int idCantiere, string codCantiere, string descrCantiere)
        {
            this.IdCantiere = -1;
            this.CodCantiere = codCantiere;
            this.DescrCantiere = descrCantiere;
        }

        public Cantieri(decimal prezzoManodopera, decimal valorePreventivo, bool chiuso, bool riscosso, bool preventivo, bool daDividere, bool diviso, bool fatturato, DateTime data)
        {
            this.PrezzoManodopera = prezzoManodopera;
            this.ValorePreventivo = valorePreventivo;
            this.Chiuso = chiuso;
            this.Riscosso = riscosso;
            this.Preventivo = preventivo;
            this.DaDividere = daDividere;
            this.Diviso = diviso;
            this.Fatturato = fatturato;
            this.Data = data;
        }

        public int IdCantiere
        {
            get { return idCantiere; }
            set { idCantiere = value; }
        }

        public string CodCantiere
        {
            get { return codCantiere; }
            set { codCantiere = value; }
        }

        public string DescrCantiere
        {
            get { return descrCantiere; }
            set { descrCantiere = value; }
        }

        public int IdCliente
        {
            get
            {
                return IdCliente1;
            }

            set
            {
                IdCliente1 = value;
            }
        }

        public int Ricarico
        {
            get
            {
                return Ricarico1;
            }

            set
            {
                Ricarico1 = value;
            }
        }

        public int Iva
        {
            get
            {
                return Iva1;
            }

            set
            {
                Iva1 = value;
            }
        }

        public int Anno
        {
            get
            {
                return Anno1;
            }

            set
            {
                Anno1 = value;
            }
        }

        public int FasciaCantiere
        {
            get
            {
                return FasciaCantiere1;
            }

            set
            {
                FasciaCantiere1 = value;
            }
        }

        public string Indirizzo
        {
            get
            {
                return Indirizzo1;
            }

            set
            {
                Indirizzo1 = value;
            }
        }

        public string Citta
        {
            get
            {
                return Citta1;
            }

            set
            {
                Citta1 = value;
            }
        }

        public string Numero
        {
            get
            {
                return Numero1;
            }

            set
            {
                Numero1 = value;
            }
        }

        public decimal PrezzoManodopera
        {
            get
            {
                return prezzoManodopera;
            }

            set
            {
                prezzoManodopera = value;
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

        public string Indirizzo1
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

        public string Citta1
        {
            get
            {
                return citta;
            }

            set
            {
                citta = value;
            }
        }

        public string Numero1
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

        public int Ricarico1
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

        public int Iva1
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

        public int Anno1
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

        public int FasciaCantiere1
        {
            get
            {
                return fasciaCantiere;
            }

            set
            {
                fasciaCantiere = value;
            }
        }

        public int IdCliente1
        {
            get
            {
                return idCliente;
            }

            set
            {
                idCliente = value;
            }
        }
    }
}