﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatasetGenerator.ScenarioCreator
{
    public class PlaceSettings
    {
        public Places Place { get; set; }
    }

    public enum Places
    {
        DesertAirField,
        MilitaryBase
    }
}
