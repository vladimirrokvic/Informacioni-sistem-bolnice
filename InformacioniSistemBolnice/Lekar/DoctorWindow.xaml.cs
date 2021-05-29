﻿using System;
using System.Collections.Generic;
using System.Globalization;
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
using InformacioniSistemBolnice.Lekar;

namespace InformacioniSistemBolnice
{
    public partial class DoctorWindow : Window
    {
        public Doctor Doctor;
        public DoctorWindow(Doctor doctor)
        {
            this.Doctor = doctor;
            InitializeComponent();
            this.Title = doctor.ime + " " + doctor.prezime;
            Main.Content = ProfilePage.GetPage(this);
        }

        private void KartonClick(object sender, RoutedEventArgs e)
        {
            Main.Content = DoctorPatientsPage.GetPage(this);
        }

        private void PreglediClick(object sender, RoutedEventArgs e)
        {
            Main.Content = AppointmentsPage.GetPage(this);
        }

        private void LekoviClick(object sender, RoutedEventArgs e)
        {
            Main.Content = DrugsPage.GetPage(this);
        }

        private void ProfilClick(object sender, RoutedEventArgs e)
        {
            Main.Content = ProfilePage.GetPage(this);
        }

        private void OdjavaClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
            this.Close();
        }
    }
}