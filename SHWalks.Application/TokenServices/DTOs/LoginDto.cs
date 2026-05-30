using System.ComponentModel.DataAnnotations;

namespace SHWalks.Application.TokenServices.DTOs
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
