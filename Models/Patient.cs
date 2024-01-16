using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AarogyaSaathi.Models
{
    public class Patient 
    {
       
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Only uppercase and lowercase alphabets are allowed")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only uppercase and lowercase alphabets")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        //[CustomDateValidation(ErrorMessage = "Enter a valid date")]
        public string DateOfBirth { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Only uppercase and lowercase alphabets are allowed.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^[6-9][0-9]{9}$", ErrorMessage = "Invalid Mobile Number")]
        public string MobileNo { get; set; }
        //public string? email { get; set; }
        //public string Password { get; set; }
        public class CustomDateValidationAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (value is DateTime date)
                {
                    return date < DateTime.Now;
                }
                return false;
            }
        }




    }
}
