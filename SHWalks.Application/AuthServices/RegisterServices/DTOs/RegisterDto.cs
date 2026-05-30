using System.ComponentModel.DataAnnotations;

namespace SHWalks.Application.AuthServices.RegisterServices.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[] Roles { get; set; }
    }
}
