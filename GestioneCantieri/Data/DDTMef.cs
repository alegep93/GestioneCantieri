using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestioneCantieri.Data
{
    public class DDTMef
    {
        int id, anno, n_ddt, qta, annoN_ddt;
        DateTime data;
        string codArt, descriCodArt, acquirente;
        decimal importo, prezzoUnitario, valore;

        public DDTMef()
        {
            id = anno = n_ddt = qta = annoN_ddt = 0;
            data = new DateTime();
            codArt = descriCodArt = acquirente = null;
            importo = prezzoUnitario = Valore = 0m;
        }

        public DDTMef(int id, int anno, int n_ddt, int qta, int annoN_ddt, DateTime data, string codArt, string descrCodArt, string acquirente, decimal importo, decimal prezzoUnitario, decimal valore)
        {
            this.id = id;
            this.anno = anno;
            this.n_ddt = n_ddt;
            this.qta = qta;
            this.annoN_ddt = annoN_ddt;
            this.data = data;
            this.codArt = codArt;
            this.descriCodArt = descrCodArt;
            this.acquirente = acquirente;
            this.importo = importo;
            this.prezzoUnitario = prezzoUnitario;
            this.Valore = valore;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Anno
        {
            get { return anno; }
            set { anno = value; }
        }

        public int N_ddt
        {
            get { return n_ddt; }
            set { n_ddt = value; }
        }

        public int Qta
        {
            get { return qta; }
            set { qta = value; }
        }

        public int AnnoN_ddt
        {
            get { return annoN_ddt; }
            set { annoN_ddt = value; }
        }

        public DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        public string CodArt
        {
            get { return codArt; }
            set { codArt = value; }
        }

        public string DescriCodArt
        {
            get { return descriCodArt; }
            set { descriCodArt = value; }
        }

        public string Acquirente
        {
            get { return acquirente; }
            set { acquirente = value; }
        }

        public decimal Importo
        {
            get { return importo; }
            set { importo = value; }
        }

        public decimal PrezzoUnitario
        {
            get { return prezzoUnitario; }
            set { prezzoUnitario = value; }
        }

        public decimal Valore { get => valore; set => valore = value; }
    }
}