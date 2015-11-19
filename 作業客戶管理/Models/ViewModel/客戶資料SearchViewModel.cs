using System.Collections.Generic;
using 作業客戶管理.Models.Enum;

namespace 作業客戶管理.Models.ViewModel
{
    public class 客戶資料SearchViewModel
    {
        public Enum客戶分類? 客戶分類 { get; set; }

        public string Search { get; set; }

        public List<客戶資料> 客戶資料列表 { get; set; }
    }
}