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
    }

    public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}