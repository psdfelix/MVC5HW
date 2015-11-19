using System;
using System.Collections.Generic;
using 作業客戶管理.Models;

namespace 作業客戶管理.Models.ViewModel.客戶聯絡人VM
{
    public class 客戶聯絡人SearchViewModel
    {
        public string Name { get; set; }

        public string 職稱 { get; set; }

        public List<客戶聯絡人> 客戶聯絡人列表 { get; set; }
    }
}