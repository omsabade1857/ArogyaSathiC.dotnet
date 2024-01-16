using System.ComponentModel.DataAnnotations;

namespace AarogyaSaathi.Models
{
    public class PatientHistory
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Only uppercase and lowercase alphabets are allowed")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Gender is required.")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Only uppercase and lowercase alphabets are allowed")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Invalid Mobile Number")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime visitDate { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression(@"^[a-zA-Z .-_]+$", ErrorMessage = "Only uppercase and lowercase alphabets are allowed")]
        public string doctorName { get; set; }
        [Required(ErrorMessage = "Symptoms is required.")]
        [RegularExpression(@"^[a-zA-Z .-_]+$", ErrorMessage = "Only uppercase and lowercase alphabets are allowed")]
        public string symptoms { get; set; }

        public string medicine { get; set; }
        public string remark { get; set; }
       // public string PatientId { get; set; }
    }
}
