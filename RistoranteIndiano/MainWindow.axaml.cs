using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;

namespace RistoranteIndiano;

public partial class MainWindow : Window
{
    private CRistorante ristorante;


    public MainWindow()
    {
        InitializeComponent();
        ristorante = new CRistorante();
        ListaMenu.ItemsSource = ristorante.Menu;
        Aggiorna();
    }
    private void Aggiorna()
    {
        int indice = ListaTavoli.SelectedIndex;
        ListaTavoli.ItemsSource = null;
        ListaTavoli.ItemsSource = ristorante.Tavoli;
        ListaTavoli.SelectedIndex = indice;

        ListaStorico.ItemsSource = null;
        ListaStorico.ItemsSource = ristorante.Storico;

        int clienti = 0;
        double incasso = 0;
        foreach (COrdineConcluso c in ristorante.Storico)
        {
            if (c.OraPagamento.Date == DateTime.Today)
            {
                clienti += c.Persone;
                incasso += c.Totale;
            }
        }

        TestoStorico.Text = $"Oggi: {clienti} clienti serviti - incasso totale: {incasso}€";
        AggiornaDettagli();
    }

    private void AggiornaDettagli()
    {
        PannelloConto.IsVisible = false;
        CTavolo t = ListaTavoli.SelectedItem as CTavolo;

        if (t == null)
        {
            PannelloOccupa.IsVisible = false;
            TitoloOrdine.Text = "Seleziona un tavolo";
            RigheOrdine.ItemsSource = null;
            TotaleOrdine.Text = "";
        }
        else if (t.Occupato == false)
        {
            PannelloOccupa.IsVisible = true;
            TestoOccupa.Text = $"Tavolo {t.Numero} libero con {t.Posti} posti";
            ErroreOccupa.Text = "";
            TitoloOrdine.Text = $"Tavolo {t.Numero} libero";
            RigheOrdine.ItemsSource = null;
            TotaleOrdine.Text = "";
        }
        else
        {
            PannelloOccupa.IsVisible = false;
            TitoloOrdine.Text = $"Ordine del tavolo {t.Numero}";
            RigheOrdine.ItemsSource = t.Ordine.RigheLista();
            TotaleOrdine.Text = $"Totale: {t.Ordine.CalcolaTotale()}€";
        }
    }
    private void ListaTavoli_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        AggiornaDettagli();
    }

    private void BtnOccupa_Click(object? sender, RoutedEventArgs e)
    {

        CTavolo t = ListaTavoli.SelectedItem as CTavolo;
        if (t == null)
        {
            return;
        }
        int persone;
        if (NumPersone.Value == null)
        {
            persone = 0;
        }
        else
        {
            persone = (int)NumPersone.Value;
        }

        if (persone < 1)
        {
            ErroreOccupa.Text = "Inserisci almeno 1 persona";
            return;
        }
        try
        {
            ristorante.Occupa(t.Numero, persone);
        }
        catch (Exception ex)
        {
            ErroreOccupa.Text = ex.Message;
            return;
        }
        Aggiorna();
    }

    private void BtnRimuovi_Click(object? sender, RoutedEventArgs e)
    {
        CTavolo t = ListaTavoli.SelectedItem as CTavolo;
        if (t == null || t.Occupato == false)
        {
            return;
        }

        int indice = RigheOrdine.SelectedIndex;
        if(indice == -1)
        {
            return;
        }
        List<string> nomi = t.Ordine.NomiOrdini();
        string nome = nomi[indice];
        ristorante.Rimuovi(t.Numero, nome);
        Aggiorna();
    }

    private void BtnChiudi_Click(object? sender, RoutedEventArgs e)
    {
        CTavolo t = ListaTavoli.SelectedItem as CTavolo;
        if (t == null || t.Occupato == false)
        {
            return;
        }

        if(t.Ordine.Ordine.Count == 0)
        {
            TotaleOrdine.Text = "Il conto è vuoto";
            return;
        }
        string testo = "";
        foreach(string righe in t.Ordine.RigheLista())
        {
            testo += righe + "\n";
        }
        testo += $" TOTALE: {t.Ordine.CalcolaTotale()}";
        TestoConto.Text = testo;
        PannelloConto.IsVisible = true;
    }

    private void BtnConferma_Click(object? sender, RoutedEventArgs e)
    {
        CTavolo t = ListaTavoli.SelectedItem as CTavolo;
        if (t == null || t.Occupato == false)
        {
            return;
        }

        ristorante.ChiudiConto(t.Numero);
        Aggiorna();
    }

    private void BtnAggiungi_Click(object? sender, RoutedEventArgs e)
    {
        CTavolo t = ListaTavoli.SelectedItem as CTavolo;
        if (t == null || t.Occupato == false)
        {
            return;
        }

        CPiatto piatto = ListaMenu.SelectedItem as CPiatto;
        if(piatto == null)
        {
            return;
        }

        int quantita;
        if (NumQuantita.Value < 1)
        {
            return;
        }
        if(NumQuantita.Value == null)
        {
            quantita = 1;
        }
        else
        {
            quantita = (int)NumQuantita.Value;
        }
        ristorante.Aggiungi(t.Numero, piatto, quantita);
        Aggiorna();
    }
    private void BtnAnnulla_Click(object? sender, RoutedEventArgs e)
    {
        PannelloConto.IsVisible = false;
    }
}