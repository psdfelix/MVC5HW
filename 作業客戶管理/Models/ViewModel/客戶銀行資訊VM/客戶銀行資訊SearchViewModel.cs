using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 作業客戶管理.Models.ViewModel.客戶銀行資訊VM
{
    public class 客戶銀行資訊SearchViewModel
    {
        public string BankName { get; set; }

        public List<客戶銀行資訊> 客戶銀行資訊列表 { get; set; }
    }
}