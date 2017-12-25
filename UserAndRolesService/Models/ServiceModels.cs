using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserAndRolesService.Models
{

    public class UserAndRolesModel
    {
        public int URId { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
    }

    public class UserModel
    {
        public UserModel()
        {

        }
        public UserModel(User usr)
        {
            this.ID = usr.UserID;
            this.Name = usr.UserName;
            this.Surname = usr.UserSurname;
            this.Password = usr.UserPassword;
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
    }

    public class RoleWithUsers : RolesModel
    {
        public List<UserModel> Users { get; set; }
    }

    public class RoleOperations
    {
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }

    public class UserDetailModel : UserModel
    {
        public List<RolesDetailModel> URoles { get; set; }
    }

    public class RolesDetailModel : RolesModel
    {
        public int URId { get; set; }
    }

    public class RolesModel
    {
        public RolesModel()
        {

        }

        public RolesModel(Roles rl)
        {
            this.RId = rl.RoleID;
            this.RName = rl.RoleName;
            this.RDescription = rl.RoleDescription;
        }
        public int? RId { get; set; }
        public string RName { get; set; }
        public string RDescription { get; set; }
    }

    public class UserRoles
    {
        public int URID { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
