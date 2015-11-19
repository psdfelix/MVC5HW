using System;
using System.Linq;
using System.Collections.Generic;
using 作業客戶管理.Models.ViewModel.客戶銀行資訊VM;
using AutoMapper;

namespace 作業客戶管理.Models
{
    public class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
    {
        public 客戶銀行資訊SearchViewModel SearchList(客戶銀行資訊SearchViewModel 客戶銀行資訊SVM)
        {
            var data = this.All().Where(p => p.IsDelete != true);

            if (客戶銀行資訊SVM.BankName != null)
            {
                data = data.Where(p => p.銀行名稱.Contains(客戶銀行資訊SVM.BankName));
            }

            var result = data.ToList();
            客戶銀行資訊SVM.客戶銀行資訊列表 = result;
            return 客戶銀行資訊SVM;
        }

        public 客戶銀行資訊 GetDataById(int id)
        {
            var result = this.Where(p => p.Id == id).FirstOrDefault();
            return result;
        }

        public void Create(客戶銀行資訊CreateViewModel 客戶銀行資訊CVM)
        {
            Mapper.CreateMap<客戶銀行資訊CreateViewModel, 客戶銀行資訊>();
            客戶銀行資訊 customerData = Mapper.Map<客戶銀行資訊>(客戶銀行資訊CVM);
            this.Add(customerData);
            this.UnitOfWork.Commit();
        }
    }

    public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}