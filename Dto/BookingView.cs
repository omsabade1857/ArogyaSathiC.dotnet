namespace AarogyaSaathi.Dto
{
    public class BookingView
    {
        public int Id { get; set; }
        public string DoctorId { get; set; }

        public string Symptoms { get; set; }
        public DateTime? BookingDate { get; set; }
    }
}
