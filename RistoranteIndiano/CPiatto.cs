using System;
using System.Collections.Generic;
using System.Text;

namespace RistoranteIndiano
{
    public enum CategoriaPiatto
    {
        Antipasto,
        PiattoPrincipale,
        Contorno,
        Pane,
        Dolce,
        Bevanda
    }
    public class CPiatto
    {
        private string nome = "";
        public string Nome
        {
            get
            {
                return nome;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Inserisci il nome del piatto");
                nome = value;
            }
        }

        private CategoriaPiatto categoria;
        public CategoriaPiatto Categoria
        {
            get
            {
                return categoria;
            }
            set
            {
                categoria = value;
            }
        }

        private double prezzo;
        public double Prezzo
        {
            get
            {
                return prezzo;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Il prezzo del singolo piatto non può essere negativo o nullo");
                prezzo = value;
            }
        }

        public CPiatto(string Nome, CategoriaPiatto Categoria, double Prezzo)
        {
            this.Nome = Nome;
            this.Categoria = Categoria;
            this.Prezzo = Prezzo;
        }

        public override string ToString()
        {
            string categoriaString = "";
            if (Categoria == CategoriaPiatto.PiattoPrincipale)
            {
                categoriaString = "Piatto principale";
            }
            else
            {
                categoriaString = Categoria.ToString();
            }

            return $"{categoriaString}: {Nome} - {Prezzo}€";
        }
    }
}
