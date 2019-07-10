using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Park
    {
        public IList<Park> Parks { get; set; }

        public string ParkCode { get; set; }

        public string ParkName { get; set; }

        public string State { get; set; }

        public int Acerage { get; set; }

        public int ElevationInFeet { get; set; }

        public double MilesOfTrail { get; set; }

        public int NumberOfCampSites { get; set; }

        public string Climate { get; set; }

        public int YearFounded { get; set; }

        public int AnnualVisitorCount { get; set; }

        public string InspirationalQuote { get; set; }

        public string InspirationalQuoteSource { get; set; }

        public string ParkDescription { get; set; }

        public decimal EntryFee { get; set; }

        public int NumberOfAnimalSpecies { get; set; }

        public string Image { get; set; }

    }
}
