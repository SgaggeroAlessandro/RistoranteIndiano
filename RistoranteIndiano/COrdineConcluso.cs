using System;
using System.Collections.Generic;
using System.Text;

namespace RistoranteIndiano
{
    public class COrdineConcluso
    {
        private int tavolo;
        public int Tavolo
        {
            get
            {
                return tavolo;
            }
            set
            {
                if (value < 1)
                    throw new ArgumentException("Inserisci il numero del tavolo");
                tavolo = value;
            }
        }

        private DateTime orapagamento;
        public DateTime OraPagamento
        {
            get
            {
                return orapagamento;
            }
            set
            {
                orapagamento = value;
            }
        }

        private int persone;
        public int Persone
        {
            get
            {
                return persone;
            }
            set
            {
                if (value < 1)
                    throw new ArgumentException("Inserisci quante persone erano sedute al tavolo al momento del pagamento");
                persone = value;
            }
        }
        private COrdine ordine;
        public COrdine Ordine
        {
            get
            {
                return ordine;
            }
            set
            {
                ordine = value;
            }
        }

        private double totale;
        public double Totale
        {
            get
            {
                return totale;
            }
            set
            {
                totale = value;
            }
        }
        public COrdineConcluso() { }
        public COrdineConcluso(int Tavolo, int Persone, COrdine Ordine)
        {
            this.Tavolo = Tavolo;
            this.Persone = Persone;
            this.Ordine = Ordine;
            OraPagamento = DateTime.Now;
            Totale = Ordine.CalcolaTotale();
        }

        public override string ToString()
        {
            string testo = $"{OraPagamento:dd/MM/yyyy HH:mm} - Tavolo: {Tavolo} - Persone: {Persone}\n";

            foreach(string riga in Ordine.RigheLista())
            {
                testo += "  " + riga + "\n"; 
            }
            testo += $"TOTALE: {Totale}€";
            return testo;
        }
    }
}
