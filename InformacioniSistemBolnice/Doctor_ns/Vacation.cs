﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.Doctor_ns
{
    public class Vacation
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        [JsonIgnore]
        public string StartDisplay
        {
            get
            {
                return Start.ToString("dd/MM/yyyy");
            }
            set { }
        }
        public int DurationInBusinessDays { get; set; }
        [JsonIgnore]
        public string EndDisplay
        {
            get
            {
                return End.ToString("dd/MM/yyyy");
            }
            set { }
        }

        public Vacation(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
            DurationInBusinessDays = 0;
            DateTime i;
            for (i = Start.Date; i <= End.Date; i = i.AddDays(1))
            {
                if (i.DayOfWeek == DayOfWeek.Saturday || i.DayOfWeek == DayOfWeek.Sunday)
                    continue;
                DurationInBusinessDays++;
            }
        }
        public bool Overlaps(List<Vacation> vacations)
        {
            if (Start.Equals(End))
                return false;
            bool retVal = false;
            foreach (Vacation vacation in vacations)
            {
                
                if (Start >= vacation.Start && Start <= vacation.End)
                {
                    retVal = true;
                    break;
                }
                if (End >= vacation.Start && End <= vacation.End)
                {
                    retVal = true;
                    break;
                }
                if (Start <= vacation.Start && End >= vacation.End)
                {
                    retVal = true;
                    break;
                }
            }
            return retVal;
        }
    }
}