﻿using InformacioniSistemBolnice.FileStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InformacioniSistemBolnice.Service
{
    public class MedicineService
    {
        public void AddMedicine(Medicine medicine)
        {
            if (!IsIdunique(medicine.MedicineId))
            {
                MessageBox.Show("Uneti ID leka već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }
            if (!IsNameUnique(medicine.Name))
            {
                MessageBox.Show("Uneto ime leka već postoji u sistemu", "Podaci nisu unikatni", MessageBoxButton.OK);
                return;
            }
            MedicineFileRepository.AddMedicine(medicine);
        }

        public void UpdateMedicine(Medicine medicine)
        {
            MedicineFileRepository.UpdateMedicine(medicine.MedicineId, medicine);
        }

        public void RemoveMedicine(Medicine medicine)
        {
            MedicineFileRepository.RemoveMedicine(medicine.MedicineId);
        }

        public void SendMedicineForRemovingValidation(Medicine medicine)
        {
            medicine.MedicineStatus = MedicineStatus.waitingForValidation;
            UpdateMedicine(medicine);
        }

        public Medicine GetOneByname(String name)
        {
            return MedicineFileRepository.GetOneByName(name);
        }
        public bool IsIdunique(String medicineId)
        {
            if (MedicineFileRepository.GetOne(medicineId) == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsNameUnique(String name)
        {
            if (MedicineFileRepository.GetOne(name) == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Medicine> GetAllMedicines()
        {
            return MedicineFileRepository.GetAll();
        }

        public List<Ingredient> GetMedicineIngredients(Medicine medicine)
        {
            List<Ingredient> ingredientsList = new List<Ingredient>();
            foreach(Ingredient ingredient in IngredientFileStorage.GetAll())
            {
                if (medicine.IngredientsList.Contains(ingredient))
                {
                    ingredientsList.Add(ingredient);
                }
            }
            return ingredientsList;
        }

        public List<Ingredient> AddIngredients(Medicine medicine, Ingredient ingredient)
        {
            foreach(Ingredient ing in IngredientFileStorage.GetAll())
            {
                if (!medicine.IngredientsList.Contains(ingredient) && ingredient.Name.Equals(ing.Name))
                {
                    medicine.IngredientsList.Add(ingredient);
                }
            }
            return medicine.IngredientsList;
            
        }

        public ObservableCollection<Ingredient> AddIngredientsToNewMedicine(Ingredient ingredient)
        {
            ObservableCollection<Ingredient> ingredients = new ObservableCollection<Ingredient>();
            foreach (Ingredient ing in IngredientFileStorage.GetAll())
            {
                if (ing.Name.Equals(ingredient.Name))
                {
                    ingredients.Add(ing);
                }
            }
            return ingredients;
        }

        public ObservableCollection<Ingredient> RemoveIngredient(Ingredient ingredient)
        {
            ObservableCollection<Ingredient> ingredients = new ObservableCollection<Ingredient>();
            foreach (Ingredient ing in IngredientFileStorage.GetAll())
            {
                if (ing.Name.Equals(ingredient.Name))
                {
                    ingredients.Remove(ing);
                }
            }
            return ingredients;
        }

        public List<Ingredient> GetAllIngredients()
        {
            return IngredientFileStorage.GetAll();
        }
    }
}