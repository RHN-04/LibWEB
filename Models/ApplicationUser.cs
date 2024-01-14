using Microsoft.AspNetCore.Identity;

namespace LibWEB.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string? Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
