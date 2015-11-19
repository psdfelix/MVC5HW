using AutoMapper;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using 作業客戶管理.Models;
using 作業客戶管理.Models.ViewModel;

namespace 作業客戶管理.Controllers
{
    public class 客戶資料Controller : Controller
    {
        private readonly 客戶資料Repository customerData;

        public 客戶資料Controller()
        {
            this.customerData = RepositoryHelper.Get客戶資料Repository();
        }

        // GET: 客戶資料
        public ActionResult Index()
        {
            var data = customerData.All().Where(p => p.IsDelete != true).ToList();
            客戶資料SearchViewModel result = new 客戶資料SearchViewModel();
            result.客戶資料列表 = data;
            return View(result);
        }

        [HttpPost]
        public ActionResult Index(客戶資料SearchViewModel 客戶資料VM)
        {
            var data = customerData.SearchList(客戶資料VM);
            return View(data);
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            客戶資料 客戶資料 = customerData.GetDataById(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(客戶資料CreateViewModel 客戶資料CVM)
        {
            if (ModelState.IsValid)
            {
                customerData.Create(客戶資料CVM);
                return RedirectToAction("Index");
            }

            return View(客戶資料CVM);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = customerData.GetDataById(id.Value);
            Mapper.CreateMap<客戶資料,客戶資料CreateViewModel >();
            客戶資料CreateViewModel 客戶資料EVM = Mapper.Map<客戶資料CreateViewModel>(客戶資料);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料EVM);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            var data = customerData.GetDataById(客戶資料.Id);
            if (TryUpdateModel<客戶資料>(data))
            {
                customerData.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = customerData.GetDataById(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = customerData.GetDataById(id);
            客戶資料.IsDelete = true;
            customerData.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                customerData.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
