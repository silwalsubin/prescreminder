namespace services.UserPrescriptions.Domain
{
    public class MedicationInfoViewModel
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
    }
}