using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestioneCantieri.Data
{
    public class RicalcoloContiStampaPDF
    {
        string data_O_Note, descrCodArt;
        double qta;
        decimal pzzoUniCantiere, valore;

        public RicalcoloContiStampaPDF() { }

        public RicalcoloContiStampaPDF(string data_o_note, string descrCodArt, double qta, decimal pzzoUniCantiere, decimal valore)
        {
            this.Data_O_Note = data_o_note;
            this.DescrCodArt = descrCodArt;
            this.Qta = qta;
            this.PzzoUniCantiere = pzzoUniCantiere;
            this.Valore = valore;
        }

        public string Data_O_Note { get { return data_O_Note; } set { data_O_Note = value; } }
        public string DescrCodArt { get { return descrCodArt; } set { descrCodArt = value; } }
        public double Qta { get { return qta; } set { qta = value; } }
        public decimal PzzoUniCantiere { get { return pzzoUniCantiere; } set { pzzoUniCantiere = value; } }
        public decimal Valore { get { return valore; } set { valore = value; } }
    }
}