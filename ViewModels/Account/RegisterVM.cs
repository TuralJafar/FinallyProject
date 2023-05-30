using System.ComponentModel.DataAnnotations;

namespace FinallyProject.ViewModels.Account
{
    public class RegisterVM
    {
        [Required]
        [MinLength(3,ErrorMessage ="Adiniz minumum 3simvoldan ibaret olmalidir")]
        public string Name { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Soyadiniz minumum 3simvoldan ibaret olmalidir")]
        public string Surname { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password),Compare(nameof(ConfirmPassword))]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string ConfirmPassword { get; set; }
    }
}
