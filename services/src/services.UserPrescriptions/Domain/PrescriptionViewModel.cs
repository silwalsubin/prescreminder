using System;
using System.Collections.Generic;

namespace services.UserPrescriptions.Domain
{
    public class PrescriptionViewModel
    {
        public PrescriptionViewModel()
        {
            TimesOfDay = new List<TimeOfDay>();
        }
        public Guid PrescriptionId { get; set; }
        public string Name { get; set; }
        public string UnitDose { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public int TotalQuantity { get; set; }
        public List<TimeOfDay> TimesOfDay { get; set; }
    }
}