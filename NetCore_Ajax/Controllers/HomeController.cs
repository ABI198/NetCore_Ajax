using Microsoft.AspNetCore.Mvc;
using NetCore_Ajax.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetCore_Ajax.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll()
        {
            using var context = new Context();
            var userList = context.Users.ToList();
            var jsonUserListData = JsonConvert.SerializeObject(userList);
            return Json(jsonUserListData);
        }

        public IActionResult GetById(int id)
        {
            using var context = new Context();
            var user = context.Users.FirstOrDefault(x => x.Id == id);
            var jsonUserData = JsonConvert.SerializeObject(user);
            return Json(jsonUserData);
        }

        public void Update(User user)
        {
            //Thread.Sleep(3000);
            using var context = new Context();
            var updatedUser = context.Users.Find(user.Id);
            if(user.File != null)
            {
                var uniqueName = Guid.NewGuid() + Path.GetExtension(user.File.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", uniqueName);
                var stream = new FileStream(path, FileMode.Create);
                user.File.CopyTo(stream);
                updatedUser.ImageName = uniqueName;
            }
            updatedUser.FullName = user.FullName;
            context.Users.Update(updatedUser);
            context.SaveChanges();
        }
    }
}
