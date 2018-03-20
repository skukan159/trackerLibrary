﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trackerLibrary
{
    public class PrizeModel
    {
        public int PrizeId { get; set; }
        public int PlaceNumber { get; set; }
        public string PlaceName { get; set; }
        public decimal PrizeAmmount { get; set; }
        public double PrizePercentage { get; set; }
    }
}
