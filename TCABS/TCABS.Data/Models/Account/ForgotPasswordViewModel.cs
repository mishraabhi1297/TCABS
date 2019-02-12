using System.ComponentModel.DataAnnotations;

namespace TCABS.Data.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
