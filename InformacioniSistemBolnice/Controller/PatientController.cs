﻿using InformacioniSistemBolnice.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Controller
{
    
    public class PatientController
    {
        private PatientService _patientService = new PatientService();


        public void Register(Patient patient)
        {
            _patientService.Register(patient);
        }
        public void Remove(Patient patient)
        {
            _patientService.Remove(patient);
        }

        public void Update(string initialUsername, Patient patient)
        {
            _patientService.Update(initialUsername, patient);
        }

        public void Unban(Patient patient)
        {
            _patientService.Unban(patient);
        }

        public void AddAllergen(Patient patient, Ingredient allergen)
        {
            _patientService.AddAllergen(patient, allergen);
        }

        public void RemoveAllergen(Patient patient, Ingredient allergen)
        {
            _patientService.RemoveAllergen(patient, allergen);
        }

        public Boolean CheckStatusOfPatient(Patient patient)
        {
            return _patientService.CheckStatusOfPatient(patient);
        }

        public List<Therapy> GetTherapiesFromMedicalRecord(Patient patient)
        {
            return _patientService.GetTherapiesFromRecord(patient);
        }
    }
}
