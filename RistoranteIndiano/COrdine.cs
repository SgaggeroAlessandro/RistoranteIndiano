using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace RistoranteIndiano
{
    public class COrdine
    {
        public List<CPiatto> Ordine { get; set; }

        public COrdine()
        {
            Ordine = new List<CPiatto>();
        }

        public void AggiungiPiatto(CPiatto piatto, int quantita)
        {
            for (int i = 0; i < quantita; i++)
            {
                CPiatto nuovoPiatto = new CPiatto(piatto.Nome, piatto.Categoria, piatto.Prezzo);
                Ordine.Add(nuovoPiatto);
            }
        }

        public double CalcolaTotale()
        {
            double totale = 0;
            foreach (CPiatto p in Ordine)
            {
                totale += p.Prezzo;
            }
            return totale;
        }

        public int ContaPorzioni(string nome)
        {
            int porzioni = 0;
            foreach (CPiatto p in Ordine)
            {
                if (nome.ToLower() == p.Nome.ToLower())
                {
                    porzioni++;
                }
            }
            return porzioni;
        }

        public void RimuoverePiatto(string nome)
        {
            CPiatto piatto = null;
            bool trovato = false;
            foreach (CPiatto p in Ordine)
            {
                if (p.Nome.ToLower() == nome.ToLower())
                {
                    piatto = p;
                    trovato = true;
                    break;
                }
            }
            if (trovato == true)
            {
                Ordine.Remove(piatto);
            }
        }

        public List<string> NomiOrdini()
        {
            List<string> nomi = new List<string>();

            foreach (CPiatto p in Ordine)
            {
                bool presente = false;
                foreach (string n in nomi)
                {
                    if (n.ToLower() == p.Nome.ToLower())
                    {
                        presente = true;
                        break;
                    }
                }
                if (presente == false)
                {
                    nomi.Add(p.Nome);
                }
            }

            return nomi;
        }

        public List<string> RigheLista()
        {
            List<string> righe = new List<string>();
            List<string> nomi = NomiOrdini();

            foreach (string nome in nomi)
            {
                int porzioni = ContaPorzioni(nome);

                double prezzo = 0;
                foreach (CPiatto p in Ordine)
                {
                    if (p.Nome.ToLower() == nome.ToLower())
                    {
                        prezzo = p.Prezzo;
                        break;
                    }
                }

                double totale = prezzo * porzioni;
                righe.Add($"{porzioni} x {nome} - {totale}€");

            }
            return righe;
        }
    }
}



