using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using UserAndRolesService.Models;

namespace UserAndRolesService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IUser, IRoles
    {
        public string AddUserRole(RoleOperations ar)
        {
            
            using (DBEntities db = new DBEntities())
            {
                if (!db.User.Any(x => x.UserID == ar.UserId))
                    return "User does not exist.";
                if (!db.Roles.Any(x => x.RoleID == ar.RoleId))
                    return "This role does not exist.";
                if (db.UserRoles.Any(x => x.UserId == ar.UserId && x.RoleId == ar.RoleId))
                    return "User has already this role.";
                var userRole = new UserRoles
                {
                    RoleId = ar.RoleId,
                    UserId = ar.UserId
                };
                db.UserRoles.Add(userRole);
                db.SaveChanges();
                return "Role has been added.";

            }
        }

        public UserModel Create(UserModel usr)
        {
            var newUser = new User
            {
                UserName = usr.Name,
                UserPassword = usr.Password,
                UserSurname = usr.Surname
            };

            using (DBEntities db = new DBEntities())
            {
                db.User.Add(newUser);
                db.SaveChanges();
            }

            var data = new UserModel(newUser);

            return data;
        }

        public RolesModel CreateRole(RolesModel rl)
        {
            var data = new Roles
            {
                RoleDescription = rl.RDescription,
                RoleName = rl.RName
            };
            using (DBEntities db= new DBEntities())
            {
                db.Roles.Add(data);
                db.SaveChanges();
            }
            var createdRole = new RolesModel(data);
            return createdRole;
        }

        public string Delete(int usrId)
        {
            using (DBEntities db = new DBEntities())
            {
                var user = db.User.FirstOrDefault(x => x.UserID == usrId);
                if (user == null)
                    return "User does not exist.";
                if (db.UserRoles.Any(x => x.UserId == usrId))
                    return "User has role(s), please delete role(s) first.";
                db.User.Remove(user);
                db.SaveChanges();

                return "User has been removed.";
            }
        }

        public string DeleteRole(int rId)
        {
            using(DBEntities db = new DBEntities())
            {
                var data = db.Roles.FirstOrDefault(s => s.RoleID == rId);
                if (data == null)
                    return "Role does not exist.";
                if (db.UserRoles.Any(x => x.RoleId == rId))
                    return "Role has been in use, first remove roles.";
                db.Roles.Remove(data);
                db.SaveChanges();
                return "Role has been removed.";
            }
        }

        public List<UserModel> GetAllList()
        {
            using (DBEntities db = new DBEntities())
            {
                return db.User.Select(s => new UserModel
                {
                    ID = s.UserID,
                    Name = s.UserName,
                    Surname = s.UserSurname,
                    Password = s.UserPassword
                }).ToList();
            }
        }

        public List<RolesModel> GetAllRoleList()
        {
            using(DBEntities db = new DBEntities())
            {
                return db.Roles.Select(s => new RolesModel
                {
                    RDescription = s.RoleDescription,
                    RId = s.RoleID,
                    RName = s.RoleName
                }).ToList();
            }
        }

        public List<UserDetailModel> GetById(int usrId)
        {
            using (DBEntities db = new DBEntities())
            {
                var data = db.User.Where(x => x.UserID == usrId).Select(s => new UserDetailModel
                {
                    ID = s.UserID,
                    Name = s.UserName,
                    Surname = s.UserSurname,
                    Password = s.UserPassword,
                    URoles = db.UserRoles.Where(x => x.UserId == usrId).Select(c => new RolesDetailModel
                    {
                        URId = c.UserRolesID,
                        RId = c.RoleId,
                        RName = c.Roles.RoleName,
                        RDescription = c.Roles.RoleDescription
                    }).ToList()
                }).ToList();
                return data;
            }
        }

        public List<RoleWithUsers> GetRoleById(int rId)
        {
            using(DBEntities db= new DBEntities())
            {
                return db.Roles.Where(x => x.RoleID == rId).Select(s => new RoleWithUsers
                {
                    RId = s.RoleID,
                    RDescription = s.RoleDescription,
                    RName = s.RoleName,
                    Users = db.UserRoles.Where(x => x.RoleId == rId).Select(c => new UserModel
                    {
                        ID = c.UserId.Value,
                        Name = c.User.UserName,
                        Surname = c.User.UserSurname
                    }).ToList()
                }).ToList();
            }
            
        }

        public string RemoveUserRole(RoleOperations ar)
        {
            using (DBEntities db = new DBEntities())
            {
                if (ar.UserRoleId > 0)
                {
                   var usrRole =  db.UserRoles.FirstOrDefault(x => x.UserRolesID == ar.UserRoleId);
                    if (usrRole != null)
                        db.UserRoles.Remove(usrRole);
                    db.SaveChanges();
                    return "Role has been removed.";
                }
                var userRole = db.UserRoles.FirstOrDefault(x => x.UserId == ar.UserId && x.RoleId == ar.RoleId);
                if (userRole == null)
                    return "Role/User does not exist.";
                db.UserRoles.Remove(userRole);
                db.SaveChanges();
                return "Role has been removed.";
            }
        }

        public string Update(UserModel usr)
        {
            using (DBEntities db = new DBEntities())
            {
                var data = db.User.FirstOrDefault(x => x.UserID == usr.ID);
                if (data == null)
                    return "User does not exist.";
                data.UserName = usr.Name;
                data.UserPassword = usr.Password;
                data.UserSurname = usr.Surname;
                db.SaveChanges();
                return "User has been updated.";

            }
        }

        public string UpdateRole(RolesModel rl)
        {
            using(DBEntities db = new DBEntities())
            {
                var data = db.Roles.FirstOrDefault(s => s.RoleID == rl.RId);
                if (data != null)
                {
                    data.RoleName = rl.RName;
                    data.RoleDescription = rl.RDescription;
                    db.SaveChanges();
                    return "Role has been updated.";
                }
                else
                    return "Role does not exist.";
            }
        }

        public string UpdateUserRole(RoleOperations ar)
        {
            using (DBEntities db = new DBEntities())
            {
                var userRole = db.UserRoles.FirstOrDefault(x => x.UserRolesID == ar.UserRoleId);
                if (userRole == null)
                    return "User does not exist.";
                if (db.UserRoles.Any(x => x.UserId == ar.UserId && x.RoleId == ar.RoleId))
                    return "User has this role.";
                userRole.RoleId = ar.RoleId;
                db.SaveChanges();
                return "Role has been updated.";
            }
        }

        public List<UserAndRolesModel> UserAndRolesList()
        {
             using(DBEntities db = new DBEntities())
            {
                var allList = db.UserRoles.Select(s => new UserAndRolesModel
                {
                    URId = s.UserRolesID,
                    FullName = s.User.UserName + " " + s.User.UserSurname,
                    RoleName = s.Roles.RoleName
                }).ToList();

                return allList;
            }
        }
    }
}
