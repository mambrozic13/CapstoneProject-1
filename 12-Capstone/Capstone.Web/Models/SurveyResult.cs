using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class SurveyResult
    {
        public int SurveyId { get; set; }

        public string ParkCode { get; set; }

        public string EmailAddress { get; set; }

        public string ActivityLevel { get; set; }
    }
}
