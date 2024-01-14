using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibWEB.ViewModel
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string? Patronymic { get; set; }

        public List<string> SelectedRoles { get; set; }

        public List<SelectListItem>? RoleOptions { get; set; }
    }
}
