using AutoMapper;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using 作業客戶管理.Models;
using 作業客戶管理.Models.Enum;
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

        public ActionResult Orderby(int i, int tableNameNum, string search, Enum客戶分類? classnum)
        {
            var data = customerData.OrderBy(i, tableNameNum, search, classnum);
            return View("Index", data);
        }

        public ActionResult ExcelFile()
        {
            IWorkbook wb = new XSSFWorkbook();
            ISheet ws;
            ws = wb.CreateSheet("客戶資料表");
            var data = customerData.All().Where(p => p.IsDelete != true).ToList();
            var dataarray = data.ToArray();

            ws.CreateRow(0).CreateCell(0).SetCellValue("客戶分類");
            ws.GetRow(0).CreateCell(1).SetCellValue("客戶名稱");
            ws.GetRow(0).CreateCell(2).SetCellValue("統一編號");
            ws.GetRow(0).CreateCell(3).SetCellValue("電話");
            ws.GetRow(0).CreateCell(4).SetCellValue("傳真");
            ws.GetRow(0).CreateCell(5).SetCellValue("地址");
            ws.GetRow(0).CreateCell(6).SetCellValue("Email");

            for (int i = 1; i <= dataarray.Length; i++)
            {
                ws.CreateRow(i).CreateCell(0).SetCellValue(dataarray[i - 1].客戶分類.Value);
                ws.GetRow(i).CreateCell(1).SetCellValue(dataarray[i-1].客戶名稱);
                ws.GetRow(i).CreateCell(2).SetCellValue(dataarray[i-1].統一編號);
                ws.GetRow(i).CreateCell(3).SetCellValue(dataarray[i-1].電話);
                ws.GetRow(i).CreateCell(4).SetCellValue(dataarray[i-1].傳真);
                ws.GetRow(i).CreateCell(5).SetCellValue(dataarray[i-1].地址);
                ws.GetRow(i).CreateCell(6).SetCellValue(dataarray[i-1].Email);
            }
            var file = new FileStream(@"d:\客戶分類.xlsx", FileMode.Create);
            wb.Write(file);
            file.Close();
            return RedirectToAction("Index");
        }
    }
}
