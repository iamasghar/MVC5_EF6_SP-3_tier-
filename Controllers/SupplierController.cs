using MVC5_EF6_SP_3_tier_.Enitiies;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace MVC5_EF6_SP_3_tier_.Controllers
{
    public class SupplierController : Controller
    {
        internal OfcDbMVCEntities db = new OfcDbMVCEntities();
        // GET: Supplier
        public ActionResult Index()
        {
            var data = db.SP_SupSelect();
            return View(data);
        }

        // GET: Supplier/Details/5
        public ActionResult Details(int id)
        {
            var data = db.Database.SqlQuery<supplyer>("exec SP_SupSearch @supId", new SqlParameter("@supId", id)).SingleOrDefault();
            return View(data);
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        [HttpPost]
        public ActionResult Create(supplyer c)
        {
            try
            {
                var data = db.Database.ExecuteSqlCommand("SP_SupCreate @supName, @supCell, @supAddress"
                    , new SqlParameter("@supName", c.name),
                    new SqlParameter("@supCell", c.cell),
                    new SqlParameter("@supAddress", c.address));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(int id)
        {
            var data = db.Database.SqlQuery<supplyer>("exec SP_SupSearch @supId", new SqlParameter("@supId", id)).SingleOrDefault();
            return View(data);
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, supplyer c)
        {
            try
            {
                var data = db.Database.ExecuteSqlCommand("SP_SupUpdate @supName, @supCell, @supAddress, @supId"
                       , new SqlParameter("@supName", c.name),
                       new SqlParameter("@supCell", c.cell),
                       new SqlParameter("@supAddress", c.address),
                       new SqlParameter("@supId", id));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(int id)
        {
            var data = db.Database.SqlQuery<supplyer>("exec SP_SupSearch @supId", new SqlParameter("@supId", id)).SingleOrDefault();
            return View(data);
        }

        // POST: Supplier/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, supplyer c)
        {
            try
            {
                var data = db.Database.ExecuteSqlCommand("SP_SupDelete @supId", new SqlParameter("@supId", id));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
