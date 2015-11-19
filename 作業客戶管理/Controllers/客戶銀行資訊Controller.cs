using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using 作業客戶管理.Models;
using 作業客戶管理.Models.ViewModel.客戶銀行資訊VM;

namespace 作業客戶管理.Controllers
{
    public class 客戶銀行資訊Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();
        private readonly 客戶銀行資訊Repository 客戶銀行資訊Repository;

        public 客戶銀行資訊Controller()
        {
            this.客戶銀行資訊Repository = RepositoryHelper.Get客戶銀行資訊Repository();
        }

        // GET: 客戶銀行資訊
        public ActionResult Index()
        {
            var data = 客戶銀行資訊Repository.All().Where(p => p.IsDelete != true).ToList();
            客戶銀行資訊SearchViewModel 客戶銀行資訊SVM = new 客戶銀行資訊SearchViewModel();
            客戶銀行資訊SVM.客戶銀行資訊列表 = data;
            return View(客戶銀行資訊SVM);
        }

        [HttpPost]
        public ActionResult Index(客戶銀行資訊SearchViewModel 客戶銀行資訊SVM)
        {
            var data = 客戶銀行資訊Repository.SearchList(客戶銀行資訊SVM);
            return View(data);
        }

        // GET: 客戶銀行資訊/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = 客戶銀行資訊Repository.GetDataById(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶銀行資訊/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(客戶銀行資訊CreateViewModel 客戶銀行資訊CVM)
        {
            if (ModelState.IsValid)
            {
                客戶銀行資訊Repository.Create(客戶銀行資訊CVM);
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶銀行資訊CVM.客戶Id);
            return View(客戶銀行資訊CVM);
        }

        // GET: 客戶銀行資訊/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = 客戶銀行資訊Repository.GetDataById(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // POST: 客戶銀行資訊/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            var data = 客戶銀行資訊Repository.GetDataById(客戶銀行資訊.Id);
            if (ModelState.IsValid)
            {
                if (TryUpdateModel<客戶銀行資訊>(data))
                {
                    客戶銀行資訊Repository.UnitOfWork.Commit();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = 客戶銀行資訊Repository.GetDataById(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // POST: 客戶銀行資訊/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶銀行資訊 客戶銀行資訊 = 客戶銀行資訊Repository.GetDataById(id);
            客戶銀行資訊.IsDelete = true;
            客戶銀行資訊Repository.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                客戶銀行資訊Repository.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ExcelFile()
        {
            IWorkbook wb = new XSSFWorkbook();
            ISheet ws;
            ws = wb.CreateSheet("客戶銀行資料表");
            var data = 客戶銀行資訊Repository.All().Where(p => p.IsDelete != true).ToList();
            var dataarray = data.ToArray();

            ws.CreateRow(0).CreateCell(0).SetCellValue("銀行名稱");
            ws.GetRow(0).CreateCell(1).SetCellValue("銀行代碼");
            ws.GetRow(0).CreateCell(2).SetCellValue("分行代碼");
            ws.GetRow(0).CreateCell(3).SetCellValue("帳戶名稱");
            ws.GetRow(0).CreateCell(4).SetCellValue("帳戶號碼");
            ws.GetRow(0).CreateCell(5).SetCellValue("客戶名稱");

            for (int i = 1; i <= dataarray.Length; i++)
            {
                ws.CreateRow(i).CreateCell(0).SetCellValue(dataarray[i - 1].銀行名稱);
                ws.GetRow(i).CreateCell(1).SetCellValue(dataarray[i - 1].銀行代碼);
                ws.GetRow(i).CreateCell(2).SetCellValue(dataarray[i - 1].分行代碼.Value);
                ws.GetRow(i).CreateCell(3).SetCellValue(dataarray[i - 1].帳戶名稱);
                ws.GetRow(i).CreateCell(4).SetCellValue(dataarray[i - 1].帳戶號碼);
                ws.GetRow(i).CreateCell(5).SetCellValue(dataarray[i - 1].客戶資料.客戶名稱);
            }
            var file = new FileStream(@"d:\客戶銀行資訊.xlsx", FileMode.Create);
            wb.Write(file);
            file.Close();
            return RedirectToAction("Index");
        }
    }
}
