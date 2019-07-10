﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Survey
    {

        public int SurveyId { get; set; }

        [DisplayName("Favorite National Park: ")]
        public string ParkCode { get; set; }

        public string EmailAddress { get; set; }

        public string State { get; set; }

        public string ActivityLevel { get; set; }
    }
}
