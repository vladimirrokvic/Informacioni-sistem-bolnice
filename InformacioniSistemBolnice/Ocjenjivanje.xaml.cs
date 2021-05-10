﻿using InformacioniSistemBolnice.FileStorage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InformacioniSistemBolnice
{
    /// <summary>
    /// Interaction logic for Ocjenjivanje.xaml
    /// </summary>
    public partial class Ocjenjivanje : Window
    {
        public PocetnaPacijent Parentp { get; set; }
        private Termin selektovan;
        public Button kojiJePritisnut;
        public Ocjenjivanje(PocetnaPacijent pp)
        {
            Parentp = pp;
            InitializeComponent();
            imePacijenta.Text = pp.Pacijent.ime +" " + pp.Pacijent.prezime;
            PretraziTermine();
            DataContext = this;
            rate.IsEnabled = false;
            rateHospital.Visibility = Visibility.Hidden;
            Provjera();
        }

        private void Provjera() {
            List<Anketa> ocjenjivanjeBolnice = new List<Anketa>();

            foreach (Anketa a in AnketaFileStorage.GetAll()) {
                if (a.KorisnickoImeLekara==null && a.IdTermina==0) {
                    if (a.KorisnickoImePacijenta.Equals(Parentp.Pacijent.korisnickoIme))
                    {
                        ocjenjivanjeBolnice.Add(a);
                    }
                }
            }

            DateTime posljednjaNapisana = DateTime.Parse("1970-01-01" + " " + "00:00:00");
            if (ocjenjivanjeBolnice.Count != 0)
            {
                posljednjaNapisana = ocjenjivanjeBolnice.ElementAt(0).NastanakAnkete;
            }

            foreach (Anketa a in ocjenjivanjeBolnice) {
                if (posljednjaNapisana < a.NastanakAnkete) {
                    posljednjaNapisana = a.NastanakAnkete;
                    
                }
            }

            if (ocjenjivanjeBolnice.Count == 0 || posljednjaNapisana.AddMinutes(3)<DateTime.Now) {
                rateHospital.Visibility = Visibility.Visible;
            }

        }

        private void logOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            Application.Current.MainWindow = m;
            m.Show();
            this.Close();
            return;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            PocetnaPacijent m = new PocetnaPacijent(Parentp.Pacijent);
            Application.Current.MainWindow = m;
            m.Show();
            this.Close();
            return;
        }

        private void PretraziTermine() 
        {
            

            foreach (Termin t in TerminFileStorage.GetAll()) {
                if (t.status==StatusTermina.zakazan && !AnketaFileStorage.Contains(t.iDTermina)) {
                    if (DateTime.Now.AddDays(-10) < t.datumZakazivanja && t.datumZakazivanja.Date < DateTime.Now) { 
                        PrikazPregleda.Items.Add(t);
                    }
                }
            
            }
        
        }

        private void rate_Click(object sender, RoutedEventArgs e)
        {
            kojiJePritisnut = rate;
            selektovan = (Termin)PrikazPregleda.SelectedItem;
            PopunjavanjeAnkete pa = new PopunjavanjeAnkete(this,selektovan);
            Application.Current.MainWindow = pa;
            pa.Show();
            return;
        }

        private void PrikazPregleda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            rate.IsEnabled = true;
        }

        public void UpdateTable() {

            PrikazPregleda.Items.Remove(selektovan);
        }

        private void rateHospital_Click(object sender, RoutedEventArgs e)
        {
            kojiJePritisnut = rateHospital;
            PopunjavanjeAnkete pa = new PopunjavanjeAnkete(this, null);
            Application.Current.MainWindow = pa;
            pa.Show();
            return;

        }
    }
}