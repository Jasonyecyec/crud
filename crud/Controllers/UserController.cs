using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using crud.Models;

namespace crud.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            List<user> userList = new List<user>();
            using(User_Model userModel = new User_Model())
            {
                userList = userModel.users.ToList<user>();
            }

            if(TempData["edited"] != null)
            {
                ViewBag.edited = TempData["edited"].ToString();
            }

    
            return View(userList);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            user userModel = new user();
            using (User_Model userObject = new User_Model())
            {
                userModel = userObject.users.Where(x => x.userID == id).FirstOrDefault();
            }
            return View(userModel);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View(new user());
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(user userModel)
        {

            //to save in database, create a new object, then call the "users.Add()" method
            // pass the userModel in "Add()", userModel consist of data from the form
            using (User_Model userObject = new User_Model())
            {
   
                userObject.users.Add(userModel);
                userObject.SaveChanges();

            }
            
            //then after saving, redirect to index
            return RedirectToAction("Index");
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            user userModel = new user();
            using (User_Model userObject = new User_Model())
            {
                userModel = userObject.users.Where(x => x.userID == id).FirstOrDefault();
            }

       
            return View(userModel);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(user userModel)
        {
            try
            {
                using (User_Model userObject = new User_Model())
                {
                    userObject.Entry(userModel).State = System.Data.Entity.EntityState.Modified;
                    userObject.SaveChanges();
                }

                TempData["edited"] = "Updated Succesfully!";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            user userModel = new user();
            using (User_Model userObject = new User_Model())
            {
                userModel = userObject.users.Where(x => x.userID == id).FirstOrDefault();
            }
            return View(userModel);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (User_Model userObject = new User_Model())
                {
                    user userModel = userObject.users.Where(x => x.userID == id).FirstOrDefault();
                    userObject.users.Remove(userModel);
                    userObject.SaveChanges();

                   
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
