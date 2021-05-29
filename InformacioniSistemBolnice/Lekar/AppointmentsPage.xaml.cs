﻿using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using InformacioniSistemBolnice.Controller;
using Microsoft.Win32;

namespace InformacioniSistemBolnice.Lekar
{
    public partial class AppointmentsPage : Page
    {
        public DoctorWindow parent;
        public Doctor Doctor;
        private static AppointmentsPage instance;
        private AppointmentController _appointmentController = new AppointmentController();

        public AppointmentsPage(DoctorWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
            UpdateTable();
        }

        public static AppointmentsPage GetPage(DoctorWindow parent)
        {
            if (instance == null)
                instance = new AppointmentsPage(parent);
            return instance;
        }

        //dodavanje
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoctorAddAppointmentWindow addWindow = new DoctorAddAppointmentWindow(parent);
            Application.Current.MainWindow = addWindow;
            addWindow.Show();
        }

        //izmena
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (AppointmentsDataGrid.SelectedItem != null)
            {
                DoctorEditAppointmentWindow editWindow = new DoctorEditAppointmentWindow(_appointmentController.GetOne((Termin)AppointmentsDataGrid.SelectedItem), parent);
                Application.Current.MainWindow = editWindow;
                editWindow.Show();
            }
        }

        //brisanje
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (AppointmentsDataGrid.SelectedItem != null)
            {
                _appointmentController.Remove(((Termin)AppointmentsDataGrid.SelectedItem));
                UpdateTable();
            }
        }

        public void UpdateTable()
        {
            AppointmentsDataGrid.Items.Clear();
            foreach (Termin appointment in _appointmentController.GetAll())
            {
                if (appointment.status == StatusTermina.zakazan)
                    AppointmentsDataGrid.Items.Add(appointment);
            }
        }
    }
}
