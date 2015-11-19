using System;
using System.Linq;
using System.Collections.Generic;
using 作業客戶管理.Models.ViewModel.客戶聯絡人VM;
using AutoMapper;

namespace 作業客戶管理.Models
{
    public class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
    {
        public 客戶聯絡人SearchViewModel SearchList(客戶聯絡人SearchViewModel 客戶聯絡人SVM)
        {
            var data = this.All().Where(p => p.IsDelete != true);

            if (客戶聯絡人SVM.Name != null)
            {
                data = data.Where(p => p.姓名.Contains(客戶聯絡人SVM.Name));
            }

            if (客戶聯絡人SVM.職稱 != null)
            {
                data = data.Where(p => p.職稱.Contains(客戶聯絡人SVM.職稱));
            }

            var result = data.ToList();
            客戶聯絡人SVM.客戶聯絡人列表 = result;
            return 客戶聯絡人SVM;
        }

        public 客戶聯絡人 GetDataById(int id)
        {
            var result = this.Where(p => p.Id == id).FirstOrDefault();
            return result;
        }

        public void Create(客戶聯絡人CreateViewModel 客戶聯絡人CVM)
        {
            Mapper.CreateMap<客戶聯絡人CreateViewModel, 客戶聯絡人>();
            客戶聯絡人 customerData = Mapper.Map<客戶聯絡人>(客戶聯絡人CVM);
            this.Add(customerData);
            this.UnitOfWork.Commit();
        }

        public void EditList(List<客戶聯絡人ListEditViewModel> customerList)
        {
            foreach (var item in customerList)
            {
                var data = GetDataById(item.Id);
                Mapper.CreateMap<客戶聯絡人ListEditViewModel, 客戶聯絡人>();
                Mapper.Map(item,data);
                UnitOfWork.Commit();
            }
        }

        public 客戶聯絡人SearchViewModel OrderBy(int i, int tableNameNum, string name, string job)
        {
            var data = All().Where(p => p.IsDelete != true);

            if (!string.IsNullOrEmpty(name))
            {
                data = data.Where(p => p.姓名.Contains(name));
            }

            if (!string.IsNullOrEmpty(job))
            {
                data = data.Where(p => p.職稱.Contains(job));
            }

            if (i == 1)
            {
                switch (tableNameNum)
                {
                    case 1:
                        data = data.OrderBy(p => p.職稱);
                        break;
                    case 2:
                        data = data.OrderBy(p => p.姓名);
                        break;
                    case 3:
                        data = data.OrderBy(p => p.Email);
                        break;
                    case 4:
                        data = data.OrderBy(p => p.手機);
                        break;
                    case 5:
                        data = data.OrderBy(p => p.電話);
                        break;
                }
            }
            else
            {
                switch (tableNameNum)
                {
                    case 1:
                        data = data.OrderByDescending(p => p.職稱);
                        break;
                    case 2:
                        data = data.OrderByDescending(p => p.姓名);
                        break;
                    case 3:
                        data = data.OrderByDescending(p => p.Email);
                        break;
                    case 4:
                        data = data.OrderByDescending(p => p.手機);
                        break;
                    case 5:
                        data = data.OrderByDescending(p => p.電話);
                        break;
                }
            }

            var result = data.ToList();
            客戶聯絡人SearchViewModel 客戶聯絡人SVM = new 客戶聯絡人SearchViewModel()
            {
                Name = name,
                職稱 = job
            };
            客戶聯絡人SVM.客戶聯絡人列表 = result;
            return 客戶聯絡人SVM;

        }
    }

    public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}