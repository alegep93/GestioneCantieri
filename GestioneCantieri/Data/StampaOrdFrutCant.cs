using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestioneCantieri.Data
{
    public class StampaOrdFrutCant
    {
        int qta;
        string descrFrutto;

        public int Qta
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

        public string DescrFrutto
        {
            get
            {
                return descrFrutto;
            }

            set
            {
                descrFrutto = value;
            }
        }

        public StampaOrdFrutCant()
        {
            this.Qta = -1;
            this.DescrFrutto = "";
        }

        public StampaOrdFrutCant(int qta, string descrFrutto)
        {
            this.Qta = qta;
            this.DescrFrutto = descrFrutto;
        }
    }
}