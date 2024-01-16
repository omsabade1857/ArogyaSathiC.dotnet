using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AarogyaSaathi.Models
{
    public class Doctor
    {
       
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Only uppercase and lowercase alphabets are allowed")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^[6-9][0-9]{9}$", ErrorMessage = "Invalid Mobile Number")]
        public string MobileNo { get; set; }
        //public string Email { get; set; }
        //public string Password { get; set; }
        [Required(ErrorMessage = "Qualification is required.")]
        [RegularExpression(@"^[a-zA-Z ,.\-_]+$", ErrorMessage = "Only uppercase and lowercase alphabets are allowed")]
        public string Qualification { get; set; }

        [Required(ErrorMessage = "Specialization is required.")]
        [RegularExpression(@"^[a-zA-Z ,.\-_]+$", ErrorMessage = "Only uppercase and lowercase alphabets are allowed")]
        public string Specialization { get; set; }  
    }
}
