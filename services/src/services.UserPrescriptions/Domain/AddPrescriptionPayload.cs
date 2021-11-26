﻿using System;
using System.Collections.Generic;

namespace services.UserPrescriptions.Domain
{
    public class AddPrescriptionPayload
    {
        public AddPrescriptionPayload()
        {
            TimesOfDay = new List<TimeOfDay>();
        }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public List<TimeOfDay> TimesOfDay { get; set; }
    }
}