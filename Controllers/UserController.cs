using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5_EF6_SP_3_tier_.Enitiies;

namespace MVC5_EF6_SP_3_tier_.Controllers
{
    public class UserController : Controller
    {
        internal OfcDbMVCEntities db = new OfcDbMVCEntities();
        // GET: User
        public ActionResult Index()
        {
            var data = db.SP_UserSelect();
            return View(data.ToList());
        }

        // GET: User/Admins
        public ActionResult Admin()
        {
            var data = db.Database.SqlQuery<user>("exec SP_UserAccess '1'");
            return View(data);
        }

        // GET: User/Employees
        public ActionResult Employee()
        {
            var data = db.Database.SqlQuery<user>("exec SP_UserAccess '2'");
            return View(data);
        }

        // GET: User/Customers
        public ActionResult Customer()
        {
            var data = db.Database.SqlQuery<user>("exec SP_UserAccess '3'");
            return View(data);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            var data = db.Database.SqlQuery<user>("exec SP_UserSearch @userId", new SqlParameter("@userId", id)).SingleOrDefault();
            return View(data);
        }

        // GET: User/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(user c)
        {
            try
            {
                var data = db.Database.ExecuteSqlCommand("SP_UserInsert  @userName," +
                    " @userCell, @userCnic, @userAddress, @access_level, @userEmail, @userPass",
                    new SqlParameter("@userName", c.name),
                    new SqlParameter("@userEmail", c.email),
                    new SqlParameter("@userCell", c.cell),
                    new SqlParameter("@userCnic", c.cnic),
                    new SqlParameter("@userPass", c.password),
                    new SqlParameter("@userAddress", c.address),
                    new SqlParameter("@access_level", c.access_level));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var data = db.Database.SqlQuery<user>("exec SP_UserSearch @userId", new SqlParameter("@userId", id)).SingleOrDefault();
            return View(data);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, user c)
        {
            try
            {
                var data = db.Database.ExecuteSqlCommand("SP_UserUpdate  @userName," +
                    " @userCell, @userCnic, @userAddress, @access_level, @userEmail, @userPass",
                    new SqlParameter("@userName", c.name),
                    new SqlParameter("@userCell", c.cell),
                    new SqlParameter("@userEmail", c.email),
                    new SqlParameter("@userCnic", c.cnic),
                    new SqlParameter("@userPass", c.password),
                    new SqlParameter("@access_level", c.access_level),
                    new SqlParameter("@userAddress", c.address),
                    new SqlParameter("@userId", id));

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
            var data = db.Database.SqlQuery<user>("exec SP_UserSearch @userId", new SqlParameter("@userId", id)).SingleOrDefault();
            return View(data);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var data = db.Database.ExecuteSqlCommand("SP_UserDelete @userId", new SqlParameter("@userId", id));
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
