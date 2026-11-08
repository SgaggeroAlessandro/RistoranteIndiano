using System;
using System.Collections.Generic;
using System.Text;

namespace RistoranteIndiano
{
    public class CTavolo
    {
        private int numero;
        public int Numero
        {
            get
            {
                return numero;
            }
            set
            {
                if (value < 1)
                    throw new ArgumentException("Inserisci il numero del tavolo");
                numero = value;
            }
        }
        private int posti;
        public int Posti
        {
            get
            {
                return posti;
            }
            set
            {
                if (value < 1)
                    throw new ArgumentException("Il tavolo deve contenere almeno un posto");
                posti = value;
            }
        }
        private bool occupato;
        public bool Occupato
        {
            get
            {
                return occupato;
            }
            set
            {
                occupato = value;
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
                if (value < 0)
                    throw new ArgumentException("Inserisci quante persone sono sedute al tavolo");
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
        public CTavolo() { } //costruttore per il JSON
        public CTavolo(int Numero, int Posti)
        {
            this.Numero = Numero;
            this.Posti = Posti;
            Occupato = false;
            Persone = 0;
            Ordine = null;
        }
        public void OccupaTavolo(int nuovePersone)
        {
            if (Occupato == true)
            {
                throw new ArgumentException("Il tavolo è già occupato");

            }
            if (nuovePersone > Posti)
            {
                throw new ArgumentException($"Il tavolo {Numero} ha solo {Posti} posti");
            }

            
            Persone = nuovePersone;
            Ordine = new COrdine();
            Occupato = true;
        }
        public void LiberaTavolo()
        {
            Persone = 0;
            Ordine = null;
            Occupato = false;
        }
        public override string ToString()
        {
            
            string testo = "";
            if (Occupato == false)
            {
                testo = $"Tavolo {Numero} - LIBERO - {Posti} posti";
            }
            else
            {
                double totale = Ordine.CalcolaTotale();
                testo = $"Tavolo {Numero} - OCCUPATO da {Persone} su {Posti} posti disponibili - {totale}€";
            }
            return testo;
        }

    }
}
