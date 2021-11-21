using System;
using System.Collections.Generic;

namespace services.UserPrescriptions.Payloads
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

    public class TimeOfDay
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
    }

    public class PrescriptionViewModal
    {
        public PrescriptionViewModal()
        {
            TimesOfDay = new List<TimeOfDay>();
        }

        public Guid PrescriptionId { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public List<TimeOfDay> TimesOfDay { get; set; }
    }
}