﻿using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using 作業客戶管理.ActionFilter;
using 作業客戶管理.Models;
using 作業客戶管理.Models.ViewModel.客戶聯絡人VM;

namespace 作業客戶管理.Controllers
{
    [MyFilterAttribute]
    public class 客戶聯絡人Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();
        private readonly 客戶聯絡人Repository 客戶聯絡人Repository;

        public 客戶聯絡人Controller()
        {
            this.客戶聯絡人Repository = RepositoryHelper.Get客戶聯絡人Repository();
        }

        // GET: 客戶聯絡人
        public ActionResult Index()
        {
            var data = 客戶聯絡人Repository.All().Where(p => p.IsDelete != true).ToList();
            客戶聯絡人SearchViewModel 客戶聯絡人SVM = new 客戶聯絡人SearchViewModel();
            客戶聯絡人SVM.客戶聯絡人列表 = data;
            return View(客戶聯絡人SVM);
        }

        [HttpPost]
        public ActionResult Index(客戶聯絡人SearchViewModel 客戶聯絡人SVM)
        {
            var data = 客戶聯絡人Repository.SearchList(客戶聯絡人SVM);
            return View(data);
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = 客戶聯絡人Repository.GetDataById(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(客戶聯絡人CreateViewModel 客戶聯絡人CVM)
        {
            var hasEmail = 客戶聯絡人Repository.All().Where(p => p.Email == 客戶聯絡人CVM.Email && p.客戶Id == 客戶聯絡人CVM.客戶Id).Any();
            if (hasEmail)
            {
                ModelState.AddModelError("Email", "E-mail不可重複");
            }


            if (ModelState.IsValid)
            {
                客戶聯絡人Repository.Create(客戶聯絡人CVM);
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人CVM.客戶Id);
            return View(客戶聯絡人CVM);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = 客戶聯絡人Repository.GetDataById(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            var hasEmail = 客戶聯絡人Repository.All().Where(p => p.Email == 客戶聯絡人.Email && p.Id != 客戶聯絡人.Id).Any();
            if (hasEmail)
            {
                ModelState.AddModelError("Email", "E-mail不可重複");
            }

            var data = 客戶聯絡人Repository.GetDataById(客戶聯絡人.Id);
            if (TryUpdateModel<客戶聯絡人>(data))
            {
                客戶聯絡人Repository.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = 客戶聯絡人Repository.GetDataById(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = 客戶聯絡人Repository.GetDataById(id);
            客戶聯絡人.IsDelete = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                客戶聯絡人Repository.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult EditList()
        {
            List<客戶聯絡人ListEditViewModel> customerList = new List<客戶聯絡人ListEditViewModel>();
            TryUpdateModel<IList<客戶聯絡人ListEditViewModel>>(customerList, "CS$<>8__locals1.data");
            客戶聯絡人Repository.EditList(customerList);
            return RedirectToAction("Index");
        }

        public ActionResult EditList2(int id)
        {
            List<客戶聯絡人ListEditViewModel> customerList = new List<客戶聯絡人ListEditViewModel>();
            TryUpdateModel<IList<客戶聯絡人ListEditViewModel>>(customerList, "CS$<>8__locals1.data");
            客戶聯絡人Repository.EditList(customerList);
            return RedirectToAction("Details", "客戶資料", new { id = id });
        }

        public ActionResult Orderby(int i, int tableNameNum, string name, string job)
        {
            var data = 客戶聯絡人Repository.OrderBy(i, tableNameNum, name, job);
            return View("Index", data);
        }

        public ActionResult ExcelFile()
        {
            IWorkbook wb = new XSSFWorkbook();
            ISheet ws;
            ws = wb.CreateSheet("客戶聯絡人資料表");
            var data = 客戶聯絡人Repository.All().Where(p => p.IsDelete != true).ToList();
            var dataarray = data.ToArray();

            ws.CreateRow(0).CreateCell(0).SetCellValue("職稱");
            ws.GetRow(0).CreateCell(1).SetCellValue("姓名");
            ws.GetRow(0).CreateCell(2).SetCellValue("Email");
            ws.GetRow(0).CreateCell(3).SetCellValue("手機");
            ws.GetRow(0).CreateCell(4).SetCellValue("電話");
            ws.GetRow(0).CreateCell(5).SetCellValue("客戶名稱");

            for (int i = 1; i <= dataarray.Length; i++)
            {
                ws.CreateRow(i).CreateCell(0).SetCellValue(dataarray[i - 1].職稱);
                ws.GetRow(i).CreateCell(1).SetCellValue(dataarray[i - 1].姓名);
                ws.GetRow(i).CreateCell(2).SetCellValue(dataarray[i - 1].Email);
                ws.GetRow(i).CreateCell(3).SetCellValue(dataarray[i - 1].手機);
                ws.GetRow(i).CreateCell(4).SetCellValue(dataarray[i - 1].電話);
                ws.GetRow(i).CreateCell(5).SetCellValue(dataarray[i - 1].客戶資料.客戶名稱);
            }
            var file = new FileStream(@"d:\客戶聯絡人.xlsx", FileMode.Create);
            wb.Write(file);
            file.Close();
            return RedirectToAction("Index");
        }
    }
}
