using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using 作業客戶管理.ActionFilter;
using 作業客戶管理.Models;

namespace 作業客戶管理.Controllers
{
    [MyFilterAttribute]
    public class HomeController : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        // GET: 客戶統計
        public ActionResult Index()
        {
            return View(db.客戶統計.ToList());
        }

        // GET: 客戶統計/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶統計 客戶統計 = db.客戶統計.Find(id);
            if (客戶統計 == null)
            {
                return HttpNotFound();
            }
            return View(客戶統計);
        }

        // GET: 客戶統計/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶統計/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "客戶名稱,聯絡人數量,銀行帳戶數量")] 客戶統計 客戶統計)
        {
            if (ModelState.IsValid)
            {
                db.客戶統計.Add(客戶統計);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(客戶統計);
        }

        // GET: 客戶統計/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶統計 客戶統計 = db.客戶統計.Find(id);
            if (客戶統計 == null)
            {
                return HttpNotFound();
            }
            return View(客戶統計);
        }

        // POST: 客戶統計/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "客戶名稱,聯絡人數量,銀行帳戶數量")] 客戶統計 客戶統計)
        {
            if (ModelState.IsValid)
            {
                db.Entry(客戶統計).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(客戶統計);
        }

        // GET: 客戶統計/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶統計 客戶統計 = db.客戶統計.Find(id);
            if (客戶統計 == null)
            {
                return HttpNotFound();
            }
            return View(客戶統計);
        }

        // POST: 客戶統計/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            客戶統計 客戶統計 = db.客戶統計.Find(id);
            db.客戶統計.Remove(客戶統計);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
