using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestioneCantieri.Data
{
    public class CompGruppoFrut
    {
        int id, idGruppo, idFrutto, qta;
        string nomeFrutto;

        public CompGruppoFrut()
        {
            id = idGruppo = idFrutto = qta = -1;
            nomeFrutto = "";
        }

        public CompGruppoFrut(int id, int idGruppo, int idFrutto, int qta, string nomeFrutto)
        {
            this.id = id;
            this.idGruppo = idGruppo;
            this.idFrutto = idFrutto;
            this.qta = qta;
            this.nomeFrutto = nomeFrutto;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int IdGruppo
        {
            get { return idGruppo; }
            set { idGruppo = value; }
        }

        public int IdFrutto
        {
            get { return idFrutto; }
            set { idFrutto = value; }
        }

        public int Qta
        {
            get { return qta; }
            set { qta = value; }
        }

        public string NomeFrutto { get; set; }
    }
}