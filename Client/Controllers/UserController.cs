using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Client.Models;

namespace Client.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            UserAndRolesService.UserClient usr = new UserAndRolesService.UserClient();
            IEnumerable<User> UserList = usr.GetAllList().Select(s => new Models.User
            {
                Id = s.ID,
                Name = s.Name,
                Password = s.Password,
                Surname = s.Surname
            });
            return View(UserList);
        }

        public ActionResult Edit(int id)
        {
            UserAndRolesService.UserClient usr = new UserAndRolesService.UserClient();
            var user = usr.GetById(id).Select(s=>new User
            {
                Id = s.ID,
                Name = s.Name,
                Password = s.Password,
                Surname = s.Surname
            }).FirstOrDefault();
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User model)
        {
            UserAndRolesService.UserClient usr = new UserAndRolesService.UserClient();
            var data = new UserAndRolesService.UserModel
            {
                ID = model.Id.Value,
                Name = model.Name,
                Password = model.Password,
                Surname = model.Surname
            };
            usr.Update(data);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult AddRole (int usrId, int rId)
        {
            UserAndRolesService.UserClient usr = new UserAndRolesService.UserClient();
            var addRole = new UserAndRolesService.RoleOperations
            {
                UserRoleId = 0,
                RoleId = rId,
                UserId = usrId
            };
            return Json(usr.AddUserRole(addRole));

        }


        [HttpPost]
        public JsonResult RemoveRole(int id=0,int usrId=0, int rId=0)
        {
            UserAndRolesService.UserClient usr = new UserAndRolesService.UserClient();
            var addRole = new UserAndRolesService.RoleOperations
            {
                UserRoleId = id,
                RoleId = rId,
                UserId = usrId
            };
            return Json(usr.RemoveUserRole(addRole));
        }

        public ActionResult Details(int id)
        {
            UserAndRolesService.RolesClient rl = new UserAndRolesService.RolesClient();
            var RoleList = rl.GetAllRoleList().Select(s => new SelectListItem
            {
                Text = s.RName,
                Value = s.RId.ToString()
            });
            ViewBag.RoleList = RoleList;
            UserAndRolesService.UserClient usr = new UserAndRolesService.UserClient();
            var data = usr.GetById(id);
            var usrDetail = data.Select(s => new UserDetail
            {
                Id = s.ID,
                Name = s.Name,
                Password = s.Password,
                Surname = s.Surname,
                UserRoles = s.URoles.Select(c => new Role
                {
                    Id = c.RId,
                    RoleDesciription = c.RDescription,
                    RoleName = c.RName
                }).ToList()
            }).First();
            

            return View(usrDetail);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserAndRolesService.UserModel model)
        {
            UserAndRolesService.UserClient usr = new UserAndRolesService.UserClient();
            var data = usr.Create(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            UserAndRolesService.UserClient usr = new UserAndRolesService.UserClient();
            TempData["Message"] = usr.Delete(id);
            return RedirectToAction("Index");
        }


        public ActionResult UserAndRoles()
        {
            UserAndRolesService.RolesClient usr = new UserAndRolesService.RolesClient();
            var data = usr.UserAndRolesList().Select(S => new UserAndRoles
            {
                FullName = S.FullName,
                Id = S.URId,
                RoleName = S.RoleName
            }).ToList();
            return View(data);
        }
    }
}