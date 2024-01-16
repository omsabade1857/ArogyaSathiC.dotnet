using System.ComponentModel.DataAnnotations;

namespace AarogyaSaathi.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string DoctorId {  get; set; }

        //[Required(ErrorMessage = "Symptoms are required.")]
        //[RegularExpression(@"^[a-zA-Z , ]+$", ErrorMessage = "Only uppercase and lowercase alphabets are allowed")]
        public string Symptoms {  get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime? BookingDate { get; set; }
        public string Status { get; set; }
        public string PatientId {  get; set; }
    }
}
