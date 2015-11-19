using System;
using System.Linq;
using System.Collections.Generic;
using 作業客戶管理.Models.ViewModel;
using AutoMapper;

namespace 作業客戶管理.Models
{
    public class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
    {
        public 客戶資料 GetDataById(int id)
        {
            var result = this.Where(p => p.Id == id).FirstOrDefault();
            return result;
        }

        public void Create(客戶資料CreateViewModel 客戶資料CVM)
        {
            Mapper.CreateMap<客戶資料CreateViewModel, 客戶資料>();
            客戶資料 customerData = Mapper.Map<客戶資料>(客戶資料CVM);
            this.Add(customerData);
            this.UnitOfWork.Commit();
        }

        public 客戶資料SearchViewModel SearchList(客戶資料SearchViewModel 客戶資料VM)
        {
            var data = this.All().Where(p => p.IsDelete != true);

            if (客戶資料VM.Search != null)
            {
                data = data.Where(p => p.客戶名稱.Contains(客戶資料VM.Search));
            }

            if (客戶資料VM.客戶分類 != null)
            {
                data = data.Where(p => p.客戶分類 == (int)客戶資料VM.客戶分類);
            }

            var result = data.ToList();
            客戶資料VM.客戶資料列表 = result;
            return 客戶資料VM;
        }
    }

    public interface I客戶資料Repository : IRepository<客戶資料>
    {

    }
}