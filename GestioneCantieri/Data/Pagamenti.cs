using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestioneCantieri.Data
{
    public class Pagamenti
    {
        int idPagamenti, idTblCantieri;
        DateTime data;
        decimal imporo;
        string descriPagamenti, codCant, descriCodCant;
        bool acconto, saldo;

        public Pagamenti()
        {
            this.idPagamenti = this.idTblCantieri = -1;
            this.data = new DateTime();
            this.imporo = -1m;
            this.descriPagamenti = "";
            this.acconto = this.saldo = false;
        }

        public Pagamenti(int idPagamenti, int idTblCantieri, DateTime data, decimal imporo, string descriPagamenti, bool acconto, bool saldo, string codCant, string descriCodCant)
        {
            this.idPagamenti = idPagamenti;
            this.idTblCantieri = idTblCantieri;
            this.data = data;
            this.imporo = imporo;
            this.descriPagamenti = descriPagamenti;
            this.acconto = acconto;
            this.saldo = saldo;
            this.codCant = codCant;
            this.descriCodCant = descriCodCant;
        }

        public int IdPagamenti
        {
            get
            {
                return idPagamenti;
            }

            set
            {
                idPagamenti = value;
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

        public decimal Imporo
        {
            get
            {
                return imporo;
            }

            set
            {
                imporo = value;
            }
        }

        public string DescriPagamenti
        {
            get
            {
                return descriPagamenti;
            }

            set
            {
                descriPagamenti = value;
            }
        }

        public bool Acconto
        {
            get
            {
                return acconto;
            }

            set
            {
                acconto = value;
            }
        }

        public bool Saldo
        {
            get
            {
                return saldo;
            }

            set
            {
                saldo = value;
            }
        }

        public string CodCant { get => codCant; set => codCant = value; }
        public string DescriCodCant { get => descriCodCant; set => descriCodCant = value; }
    }
}