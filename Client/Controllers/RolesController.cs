using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Client.Models;

namespace Client.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult Index()
        {
            UserAndRolesService.RolesClient role = new UserAndRolesService.RolesClient();
            var data = role.GetAllRoleList().Select(s => new Role
            {
                Id = s.RId,
                RoleDesciription = s.RDescription,
                RoleName = s.RName
            });
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Role model)
        {
            UserAndRolesService.RolesClient role = new UserAndRolesService.RolesClient();

            var data = new UserAndRolesService.RolesModel
            {
                RDescription = model.RoleDesciription,
                RName = model.RoleName
            };
            role.CreateRole(data);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            UserAndRolesService.RolesClient role = new UserAndRolesService.RolesClient();
            var roleModel = role.GetRoleById(id).Select(S => new Role
            {
                Id = S.RId,
                RoleDesciription = S.RDescription,
                RoleName = S.RName
            }).First();
            return View(roleModel);
        }
        [HttpPost]
        public ActionResult Edit(Role model)
        {
            UserAndRolesService.RolesClient role = new UserAndRolesService.RolesClient();
            var data = new UserAndRolesService.RolesModel
            {
                RDescription = model.RoleDesciription,
                RId = model.Id,
                RName = model.RoleName
            };
            role.UpdateRole(data);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {

            UserAndRolesService.RolesClient role = new UserAndRolesService.RolesClient();
            TempData["Message"] = role.DeleteRole(id);
            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {
            UserAndRolesService.RolesClient role = new UserAndRolesService.RolesClient();
            var data = role.GetRoleById(id).Select(s => new RoleDetail
            {
                Id = s.RId,
                RoleName = s.RName,
                RoleDesciription = s.RDescription,
                RoleUsers = s.Users.Select(c => new User
                {
                    Id = c.ID,
                    Name = c.Name,
                    Surname = c.Surname,
                    Password = c.Password
                }).ToList()
            }).First();
            return View(data);
        }
    }
}