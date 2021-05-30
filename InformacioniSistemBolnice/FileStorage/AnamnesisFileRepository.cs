﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace InformacioniSistemBolnice.FileStorage
{
    public class AnamnesisFileRepository
    {
        private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "anamnesis.json";

        public static List<Anamnesis> GetAll()
        {
            if (!File.Exists(startupPath))
            {
                var tmp = File.OpenWrite(startupPath);
                tmp.Close();
            }
            List<Anamnesis> anamneses;
            String procitano = File.ReadAllText(startupPath);
            if (procitano.Equals(""))
            {
                anamneses = new List<Anamnesis>();
            }
            else
            {
                anamneses = JsonConvert.DeserializeObject<List<Anamnesis>>(procitano);
            }
            return anamneses;
        }



        public static Boolean AddAnamnesis(Anamnesis newAnamnesis)
        {
            List<Anamnesis> anamneses = GetAll();
            anamneses.Add(newAnamnesis);
            Save(anamneses);
            return true;
        }

        public static Boolean UpdateAnamnesis(int idOfAnamnesis, Anamnesis newAnamnesis)
        {
            List<Anamnesis> anamneses = GetAll();
            foreach (Anamnesis a in anamneses)
            {
                if (a.IdOfAnamnesis.Equals(idOfAnamnesis))
                {
                    anamneses[anamneses.IndexOf(a)] = newAnamnesis;
                    Save(anamneses);
                    return true;
                }
            }
            return false;
        }

        private static void Save(List<Anamnesis> lista)
        {
            string upis = JsonConvert.SerializeObject(lista);
            File.WriteAllText(startupPath, upis);
        }

        public static Anamnesis GetOneById(int idOfAnamnesis)
        {
            List<Anamnesis> anamneses = new List<Anamnesis>();
            foreach (Anamnesis a in anamneses)
            {
                if (a.IdOfAnamnesis.Equals(idOfAnamnesis))
                    return anamneses[anamneses.IndexOf(a)];
            }
            return null;
        }
    }
}
