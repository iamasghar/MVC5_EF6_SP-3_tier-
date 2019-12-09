using MVC5_EF6_SP_3_tier_.Enitiies;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5_EF6_SP_3_tier_.Controllers
{
    public class ProductController : Controller
    {
        internal OfcDbMVCEntities db = new OfcDbMVCEntities();
        // GET: Product
        public ActionResult Index()
        {
            var data = db.SP_PrdSelect();
            return View(data);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var data = db.Database.SqlQuery<product>("exec SP_PrdSearch @prdId", new SqlParameter("@prdId", id)).SingleOrDefault();
            return View(data);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(product c, HttpPostedFileBase upload)
        {
            try
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    string ts = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string img = ts + Path.GetFileName(upload.FileName);
                    var path = Path.Combine(Server.MapPath("~/uploads/"), img);
                    upload.SaveAs(path);
                    var data = db.Database.ExecuteSqlCommand("SP_PrdInsert @prdTitle, @prdPrice, @prdStock," +
                    " @prdExpiry, @prdManuf, @prdImg",
                    new SqlParameter("@prdTitle", c.prd_title),
                    new SqlParameter("@prdPrice", c.prd_price),
                    new SqlParameter("@prdStock", c.prd_stock),
                    new SqlParameter("@prdExpiry", c.prd_expiry),
                    new SqlParameter("@prdManuf", c.prd_manufacturer),
                    new SqlParameter("@prdImg", img));
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                string s = e.Message;
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var data = db.Database.SqlQuery<product>("exec SP_PrdSearch @prdId", new SqlParameter("@prdId", id)).SingleOrDefault();
            ViewBag.prd_id = data.prd_id;
            return View(data);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, product c, HttpPostedFileBase upload)
        {
            try
            {
                string img = "";
                if (upload != null && upload.ContentLength > 0)
                {
                    string ts = DateTime.Now.ToString("yyyyMMddHHmmss");
                    img = ts + Path.GetFileName(upload.FileName);
                    var path = Path.Combine(Server.MapPath("~/uploads/"), img);
                    upload.SaveAs(path);
                }
                else
                {
                    img = c.img;
                }
                var data = db.Database.ExecuteSqlCommand("SP_PrdUpdate @prdTitle, @prdPrice, @prdStock" +
                       ", @prdExpiry, @prdManuf, @prdImg , @prdId",
                       new SqlParameter("@prdTitle", c.prd_title),
                       new SqlParameter("@prdPrice", c.prd_price),
                       new SqlParameter("@prdStock", c.prd_stock),
                       new SqlParameter("@prdExpiry", c.prd_expiry),
                       new SqlParameter("@prdManuf", c.prd_manufacturer),
                       new SqlParameter("@prdImg", img),
                       new SqlParameter("@prdId", c.prd_id));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            var data = db.Database.SqlQuery<product>("exec SP_PrdSearch @prdId", new SqlParameter("@prdId", id)).SingleOrDefault();
            return View(data);
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var data = db.Database.ExecuteSqlCommand(" SP_PrdDelete @prdId", new SqlParameter("@prdId", id));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
