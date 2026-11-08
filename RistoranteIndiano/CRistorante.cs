using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
namespace RistoranteIndiano
{
    public class CRistorante
    {
        public List<CTavolo> Tavoli { get; set; }
        public List<CPiatto> Menu { get; set; }
        public List<COrdineConcluso> Storico { get; set; }

        public CRistorante()
        {
            Tavoli = new List<CTavolo>();
            Menu = new List<CPiatto>();
            Storico = new List<COrdineConcluso>();

            Carica();
            if(Tavoli.Count == 0)
            {
                Crea();
                Salva();
            }
            
        }
        public void Crea()
        {
            CTavolo tavolo1 = new CTavolo(1, 2);
            CTavolo tavolo2 = new CTavolo(2, 2);
            CTavolo tavolo3 = new CTavolo(3, 4);
            CTavolo tavolo4 = new CTavolo(4, 4);
            CTavolo tavolo5 = new CTavolo(5, 4);
            CTavolo tavolo6 = new CTavolo(6, 6);
            CTavolo tavolo7 = new CTavolo(7, 6);
            CTavolo tavolo8 = new CTavolo(8, 8);

            Tavoli.Add(tavolo1);
            Tavoli.Add(tavolo2);
            Tavoli.Add(tavolo3);
            Tavoli.Add(tavolo4);
            Tavoli.Add(tavolo5);
            Tavoli.Add(tavolo6);
            Tavoli.Add(tavolo7);
            Tavoli.Add(tavolo8);

            CPiatto piatto1 = new CPiatto("Samosa", CategoriaPiatto.Antipasto, 4.50);
            CPiatto piatto2 = new CPiatto("Pakora", CategoriaPiatto.Antipasto, 5.00);
            CPiatto piatto3 = new CPiatto("Chicken Tikka", CategoriaPiatto.Antipasto, 8.50);
            CPiatto piatto4 = new CPiatto("Butter Chicken", CategoriaPiatto.PiattoPrincipale, 12.00);
            CPiatto piatto5 = new CPiatto("Chicken Curry", CategoriaPiatto.PiattoPrincipale, 11.50);
            CPiatto piatto6 = new CPiatto("Palak Paneer", CategoriaPiatto.PiattoPrincipale, 10.50);
            CPiatto piatto7 = new CPiatto("Biryani", CategoriaPiatto.PiattoPrincipale, 13.00);
            CPiatto piatto8 = new CPiatto("Dal", CategoriaPiatto.Contorno, 6.50);
            CPiatto piatto9 = new CPiatto("Riso Basmati", CategoriaPiatto.Contorno, 3.00);
            CPiatto piatto10 = new CPiatto("Naan", CategoriaPiatto.Pane, 2.50);
            CPiatto piatto11 = new CPiatto("Gulab Jamun", CategoriaPiatto.Dolce, 4.50);
            CPiatto piatto12 = new CPiatto("Mango Lassi", CategoriaPiatto.Bevanda, 3.50);
            CPiatto piatto13 = new CPiatto("Chai", CategoriaPiatto.Bevanda, 2.50);

            Menu.Add(piatto1);
            Menu.Add(piatto2);
            Menu.Add(piatto3);
            Menu.Add(piatto4);
            Menu.Add(piatto5);
            Menu.Add(piatto6);
            Menu.Add(piatto7);
            Menu.Add(piatto8);
            Menu.Add(piatto9);
            Menu.Add(piatto10);
            Menu.Add(piatto11);
            Menu.Add(piatto12);
            Menu.Add(piatto13);
        }
        public CTavolo CercaTavolo(int NumeroCercato)
        {

            CTavolo tavoloTrovato = null;
            foreach (CTavolo t in Tavoli)
            {
                if (t.Numero == NumeroCercato)
                {

                    tavoloTrovato = t;
                    break;
                }

            }
            return tavoloTrovato;
        }

        public void Occupa(int numeroTavolo, int numeroPersone)
        {
            CTavolo t = CercaTavolo(numeroTavolo);
            if (t == null)
            {
                throw new ArgumentException($"Nessun tavolo {numeroTavolo} trovato");
            }
            else
            {
                t.OccupaTavolo(numeroPersone);
            }
            Salva();
            
        }
        public void Aggiungi(int numeroTavolo, CPiatto piatto, int quantita)
        {
            CTavolo t = TavoloOccupato(numeroTavolo);
            t.Ordine.AggiungiPiatto(piatto, quantita);
            Salva();
        }

        public void Rimuovi(int numeroTavolo, string nomePiatto)
        {
            CTavolo t = TavoloOccupato(numeroTavolo);
            t.Ordine.RimuoverePiatto(nomePiatto);
            Salva();
        }

        private CTavolo TavoloOccupato(int numeroTavolo)
        {
            CTavolo t = CercaTavolo(numeroTavolo);
            if (t == null)
            {
                throw new ArgumentException($"Nessun tavolo {numeroTavolo} trovato");
            }
            if (t.Occupato == false)
            {
                throw new ArgumentException($"Il tavolo {numeroTavolo} è libero");
            }
            return t;
        }

        public COrdineConcluso ChiudiConto(int numeroTavolo)
        {
            CTavolo t = TavoloOccupato(numeroTavolo);
            if (t.Ordine.Ordine.Count == 0)
            {
                throw new ArgumentException("Il conto è vuoto");
            }

            COrdineConcluso ordine = new COrdineConcluso(t.Numero, t.Persone, t.Ordine);
            Storico.Add(ordine);
            t.LiberaTavolo();
            Salva();
            return ordine;
        }

        private void Salva()
        {
            string jsonTavoli = JsonSerializer.Serialize(Tavoli);
            File.WriteAllText("tavoli.json", jsonTavoli);

            string jsonMenu = JsonSerializer.Serialize(Menu);
            File.WriteAllText("menu.json", jsonMenu);

            string jsonStorico = JsonSerializer.Serialize(Storico);
            File.WriteAllText("storico.json", jsonStorico);
        }

        private void Carica()
        {
            if(!File.Exists("tavoli.json") || !File.Exists("menu.json") || !File.Exists("storico.json"))
            {
                return;
            }

            string jsonTavoli = File.ReadAllText("tavoli.json");
            string jsonMenu = File.ReadAllText("menu.json");
            string jsonStorico = File.ReadAllText("storico.json");

            List<CTavolo> tavoli = JsonSerializer.Deserialize<List<CTavolo>>(jsonTavoli);
            List<CPiatto> menu = JsonSerializer.Deserialize<List<CPiatto>>(jsonMenu);
            List<COrdineConcluso> storico = JsonSerializer.Deserialize<List<COrdineConcluso>>(jsonStorico);

            if(tavoli == null || menu == null || storico == null)
            {
                return;
            }
            Tavoli = tavoli;
            Menu = menu;
            Storico = storico;
        }
    }
}
