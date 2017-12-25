using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Client.Models
{
    public class User
    {
        public int? Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
    
    public class Role
    {
        public int? Id { get; set; }

        [Required(ErrorMessage ="Name is required.")]
        public string RoleName { get; set; }
        [Required(ErrorMessage = "Desciription is required")]
        public string RoleDesciription { get; set; }
    }

    public class UserDetail : User
    {
        public List<Role> UserRoles { get; set; }
    }

    public class RoleDetail : Role
    {
        public List<User> RoleUsers { get; set; }
    }

    public class UserAndRoles
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
    }
}