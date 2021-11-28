namespace services.UserMedicationIntakeHistories.Domain
{
    public class AddUserMedicationIntakeHistoryPayload
    {
        public string PrescriptionName { get; set; }
        public string Quantity { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
    }
}