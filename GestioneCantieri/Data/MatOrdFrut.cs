using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestioneCantieri.Data
{
    public class MatOrdFrut
    {
        int id, idCantiere, idGruppiFrutti, idLocali;
        DateTime dataOrdine;
        string appartamento, nomeGruppo, descrizione;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public int IdCantiere
        {
            get
            {
                return idCantiere;
            }

            set
            {
                idCantiere = value;
            }
        }

        public int IdGruppiFrutti
        {
            get
            {
                return idGruppiFrutti;
            }

            set
            {
                idGruppiFrutti = value;
            }
        }

        public int IdLocali
        {
            get
            {
                return idLocali;
            }

            set
            {
                idLocali = value;
            }
        }

        public DateTime DataOrdine
        {
            get
            {
                return dataOrdine;
            }

            set
            {
                dataOrdine = value;
            }
        }

        public string Appartamento
        {
            get
            {
                return appartamento;
            }

            set
            {
                appartamento = value;
            }
        }

        public string NomeGruppo
        {
            get
            {
                return nomeGruppo;
            }

            set
            {
                nomeGruppo = value;
            }
        }

        public string Descrizione
        {
            get
            {
                return descrizione;
            }

            set
            {
                descrizione = value;
            }
        }

        public MatOrdFrut()
        {
            Id = IdCantiere = IdGruppiFrutti = IdLocali = -1;
            DataOrdine = new DateTime();
            Appartamento = NomeGruppo = Descrizione = "";
        }

        public MatOrdFrut(int id, int idCantiere, int idGruppiFrutti, int idLocali, DateTime dataOrdine, string appartamento, string nomeGruppo, string descrizione)
        {
            this.Id = id;
            this.IdCantiere = idCantiere;
            this.IdGruppiFrutti = idGruppiFrutti;
            this.IdLocali = idLocali;
            this.DataOrdine = dataOrdine;
            this.Appartamento = appartamento;
            this.NomeGruppo = nomeGruppo;
            this.Descrizione = descrizione;
        }
    }
}